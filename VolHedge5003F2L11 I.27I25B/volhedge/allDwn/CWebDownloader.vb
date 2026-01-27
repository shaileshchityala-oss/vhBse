Imports System.Net
Imports System.Windows.Forms
Imports System.Threading
Imports System.IO

Public Class CWebDownloader
    Public WithEvents webClient As WebClient
    Private statusLabel As Label
    Private timeoutTimer As System.Threading.Timer    ' System.Threading.Timer - lightweight timer
    Private timeoutMs As Integer
    Private currentFilePath As String

    Public Event DownloadCompleted(sender As Object, success As Boolean, errorMessage As String)

    Public Sub New(Optional lbl As Label = Nothing, Optional timeoutSeconds As Integer = 60)
        webClient = New WebClient()
        AddHandler webClient.DownloadProgressChanged, AddressOf OnProgressChanged
        AddHandler webClient.DownloadFileCompleted, AddressOf OnDownloadCompleted

        statusLabel = lbl
        timeoutMs = timeoutSeconds * 1000
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
    End Sub

    Public Function IsBusy() As Boolean
        Return webClient.IsBusy
    End Function

    Public Sub StartDownload(url As String, destinationPath As String)
        Try
            ' Store file path for cleanup
            currentFilePath = destinationPath

            ' Ensure directory exists
            Dim dir As String = Path.GetDirectoryName(destinationPath)
            If Not Directory.Exists(dir) Then
                Directory.CreateDirectory(dir)
            End If

            ' Check if file already exists and is locked
            If File.Exists(destinationPath) Then
                If IsFileLocked(destinationPath) Then
                    Throw New IOException($"File is locked by another process: {destinationPath}")
                End If
                ' Try to delete existing file
                Try
                    File.Delete(destinationPath)
                Catch ex As Exception
                    Throw New IOException($"Cannot delete existing file: {ex.Message}")
                End Try
            End If

            ' Start timeout timer
            timeoutTimer = New System.Threading.Timer(AddressOf TimeoutReached, Nothing, timeoutMs, Timeout.Infinite)

            webClient.DownloadFileAsync(New Uri(url), destinationPath)
        Catch ex As Exception
            UpdateStatus($"Error: {ex.Message}")
            RaiseEvent DownloadCompleted(Me, False, ex.Message)
        End Try
    End Sub

    Private Sub TimeoutReached(state As Object)
        If webClient.IsBusy Then
            webClient.CancelAsync()
            UpdateStatus($"Error: Timed out after {timeoutMs \ 1000} seconds")
            RaiseEvent DownloadCompleted(Me, False, $"Timed out after {timeoutMs \ 1000} seconds")
        End If
        DisposeTimer()
    End Sub

    Public Sub CancelDownload()
        If webClient.IsBusy Then
            webClient.CancelAsync()
            UpdateStatus("Download cancelled.")
        End If
        DisposeTimer()
    End Sub

    Private Sub OnProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs)
        UpdateStatus($"Downloaded {e.ProgressPercentage}%")
    End Sub

    Private Sub OnDownloadCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs)
        DisposeTimer()

        ' Ensure WebClient releases file handle
        Try
            If webClient IsNot Nothing Then
                ' Force disposal of internal streams
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End If
        Catch
            ' Ignore cleanup errors
        End Try

        If e.Cancelled Then
            UpdateStatus("Download cancelled.")
            ' Clean up partial file
            CleanupPartialFile()
            RaiseEvent DownloadCompleted(Me, False, "Cancelled")
        ElseIf e.Error IsNot Nothing Then
            UpdateStatus($"Error: {e.Error.Message}")
            ' Clean up partial file
            CleanupPartialFile()
            RaiseEvent DownloadCompleted(Me, False, e.Error.Message)
        Else
            UpdateStatus("Download completed successfully!")
            ' Verify file is accessible
            If Not String.IsNullOrEmpty(currentFilePath) AndAlso File.Exists(currentFilePath) Then
                If IsFileLocked(currentFilePath) Then
                    UpdateStatus("Warning: File may still be locked")
                    Thread.Sleep(100) ' Give OS time to release
                End If
            End If
            RaiseEvent DownloadCompleted(Me, True, Nothing)
        End If

        currentFilePath = Nothing
    End Sub

    Private Sub UpdateStatus(text As String)
        If statusLabel IsNot Nothing Then
            If statusLabel.InvokeRequired Then
                statusLabel.Invoke(New Action(Sub() statusLabel.Text = text))
            Else
                statusLabel.Text = text
            End If
        End If
        Debug.WriteLine(text)
    End Sub

    Private Sub DisposeTimer()
        If timeoutTimer IsNot Nothing Then
            timeoutTimer.Dispose()
            timeoutTimer = Nothing
        End If
    End Sub

    ' Check if file is locked by another process
    Private Function IsFileLocked(filePath As String) As Boolean
        If Not File.Exists(filePath) Then
            Return False
        End If

        Dim stream As FileStream = Nothing
        Try
            stream = New FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            Return False ' File is not locked
        Catch ex As IOException
            Return True ' File is locked
        Catch ex As Exception
            Return False ' Other error, assume not locked
        Finally
            If stream IsNot Nothing Then
                stream.Close()
                stream.Dispose()
            End If
        End Try
    End Function

    ' Clean up partial download file
    Private Sub CleanupPartialFile()
        If String.IsNullOrEmpty(currentFilePath) Then
            Return
        End If

        Try
            If File.Exists(currentFilePath) Then
                ' Wait a bit for handles to release
                Thread.Sleep(100)

                ' Try to delete partial file
                Dim retries As Integer = 3
                While retries > 0
                    Try
                        If Not IsFileLocked(currentFilePath) Then
                            File.Delete(currentFilePath)
                            Debug.WriteLine($"Cleaned up partial file: {currentFilePath}")
                            Exit While
                        End If
                        Thread.Sleep(50)
                        retries -= 1
                    Catch ex As Exception
                        Debug.WriteLine($"Cleanup attempt failed: {ex.Message}")
                        retries -= 1
                        Thread.Sleep(50)
                    End Try
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine($"Failed to cleanup partial file: {ex.Message}")
        End Try
    End Sub

    ' Verify file handle is released
    Public Function IsFileReady(filePath As String) As Boolean
        Return Not IsFileLocked(filePath)
    End Function

    ' Wait for file to be accessible
    Public Function WaitForFileReady(filePath As String, Optional timeoutMs As Integer = 5000) As Boolean
        Dim startTime As DateTime = DateTime.Now

        While (DateTime.Now - startTime).TotalMilliseconds < timeoutMs
            If Not IsFileLocked(filePath) Then
                Return True
            End If
            Thread.Sleep(100)
        End While

        Return False ' Timeout
    End Function

    Public Sub Dispose()
        DisposeTimer()

        ' Cancel any ongoing download
        If webClient IsNot Nothing AndAlso webClient.IsBusy Then
            Try
                webClient.CancelAsync()
                Thread.Sleep(100) ' Give it time to cancel
            Catch
                ' Ignore errors during disposal
            End Try
        End If

        ' Dispose WebClient
        If webClient IsNot Nothing Then
            webClient.Dispose()
            webClient = Nothing
        End If

        ' Force garbage collection to release file handles
        GC.Collect()
        GC.WaitForPendingFinalizers()

        currentFilePath = Nothing
    End Sub
End Class
