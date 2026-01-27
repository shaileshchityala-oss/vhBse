Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Threading
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.IO
Public Class CDownloadManager
    Private downloads As ConcurrentDictionary(Of String, CWebDownloader)
    Private downloadQueue As Queue(Of DownloadTask)
    Private maxConcurrent As Integer
    Private runningCount As Integer
    Private completedTasks As ConcurrentDictionary(Of String, DownloadResult)
    Private checkInternetOnEnqueue As Boolean

    Public Sub New(Optional maxThreads As Integer = 2, Optional enableInternetCheck As Boolean = True)
        downloads = New ConcurrentDictionary(Of String, CWebDownloader)()
        downloadQueue = New Queue(Of DownloadTask)()
        completedTasks = New ConcurrentDictionary(Of String, DownloadResult)()
        Me.maxConcurrent = Math.Max(1, maxThreads)
        Me.checkInternetOnEnqueue = enableInternetCheck
        runningCount = 0
    End Sub

    ' Add to queue
    Public Function EnqueueDownload(key As String, url As String, destinationPath As String, Optional lbl As Label = Nothing, Optional timeoutSeconds As Integer = 60, Optional allowDuplicate As Boolean = False) As Boolean
        ' Check internet connectivity first
        If checkInternetOnEnqueue Then
            If Not IsInternetAvailable() Then
                Console.WriteLine($"Warning: No internet connection. Cannot queue '{key}'")
                Return False
            End If
        End If

        ' IMPORTANT: Remove from completed tasks if re-downloading
        ' This prevents showing old result for new download
        If completedTasks.ContainsKey(key) Then
            Dim removed As DownloadResult = Nothing
            completedTasks.TryRemove(key, removed)
            Console.WriteLine($"Cleared old result for '{key}'")
        End If

        ' Check if key already exists in active downloads
        If downloads.ContainsKey(key) Then
            If Not allowDuplicate Then
                Console.WriteLine($"Warning: Task '{key}' is already downloading. Set allowDuplicate=True to force cancel and restart.")
                Return False
            Else
                ' Cancel existing and re-queue
                CancelDownload(key)
                Thread.Sleep(50) ' Give it time to cancel
            End If
        End If

        ' Check if key already exists in queue
        For Each existingTask In downloadQueue
            If existingTask.Key = key Then
                If Not allowDuplicate Then
                    Console.WriteLine($"Warning: Task '{key}' is already queued")
                    Return False
                Else
                    ' Will be handled - the old one will complete first
                    Console.WriteLine($"Warning: Task '{key}' is already queued, adding again anyway")
                End If
            End If
        Next

        Dim task As New DownloadTask With {
            .Key = key,
            .Url = url,
            .DestinationPath = destinationPath,
            .Label = lbl,
            .TimeoutSeconds = timeoutSeconds
        }
        downloadQueue.Enqueue(task)
        StartNextIfPossible()
        Return True
    End Function

    ' Start new downloads if slots available
    Private Sub StartNextIfPossible()
        While runningCount < maxConcurrent AndAlso downloadQueue.Count > 0
            Dim task As DownloadTask = downloadQueue.Dequeue()
            Dim downloader As New CWebDownloader(task.Label, task.TimeoutSeconds)

            AddHandler downloader.DownloadCompleted,
                Sub(sender, success, errMsg)
                    Console.WriteLine($"Completed download {task.Key}. Active: {runningCount - 1}/{maxConcurrent}")

                    ' Store result for waiting threads
                    Dim result As New DownloadResult With {
                        .Success = success,
                        .ErrorMessage = errMsg,
                        .CompletedTime = DateTime.Now
                    }
                    completedTasks.TryAdd(task.Key, result)

                    Dim removed As CWebDownloader = Nothing
                    downloads.TryRemove(task.Key, removed)
                    Threading.Interlocked.Decrement(runningCount)
                    StartNextIfPossible()
                End Sub

            downloads.TryAdd(task.Key, downloader)
            Threading.Interlocked.Increment(runningCount)
            Console.WriteLine($"Starting download {task.Key}. Active: {runningCount}/{maxConcurrent}")
            downloader.StartDownload(task.Url, task.DestinationPath)
        End While
    End Sub

    ' Cancel specific
    Public Sub CancelDownload(key As String)
        If downloads.ContainsKey(key) Then
            downloads(key).CancelDownload()
        End If
    End Sub

    ' Cancel everything
    Public Sub CancelAll()
        For Each d In downloads.Values
            d.CancelDownload()
        Next
        downloads.Clear()
        downloadQueue.Clear()
        runningCount = 0
    End Sub

    Public Function ActiveCount() As Integer
        Return runningCount
    End Function

    Public Function QueueCount() As Integer
        Return downloadQueue.Count
    End Function

    ' Check task status by key
    Public Function GetTaskStatus(key As String) As TaskStatus
        ' Check if actively downloading (highest priority)
        If downloads.ContainsKey(key) Then
            If downloads(key).IsBusy() Then
                Return TaskStatus.Downloading
            End If
        End If

        ' Check if in queue (second priority)
        For Each task In downloadQueue
            If task.Key = key Then
                Return TaskStatus.Queued
            End If
        Next

        ' Check if completed (only if not queued or downloading)
        If completedTasks.ContainsKey(key) Then
            Dim result As DownloadResult = completedTasks(key)
            If result.Success Then
                Return TaskStatus.Completed
            Else
                Return TaskStatus.Failed
            End If
        End If

        ' Not found
        Return TaskStatus.NotFound
    End Function

    ' Get all active download keys
    Public Function GetActiveKeys() As String()
        Dim keyList As New List(Of String)
        For Each key In downloads.Keys
            keyList.Add(key)
        Next
        Return keyList.ToArray()
    End Function

    ' Check if specific key is busy
    Public Function IsDownloading(key As String) As Boolean
        Return downloads.ContainsKey(key) AndAlso downloads(key).IsBusy()
    End Function

    ' Wait for download to complete (blocking)
    Public Function WaitForDownload(key As String, Optional timeoutMs As Integer = 300000) As DownloadResult
        Dim startTime As DateTime = DateTime.Now

        ' Check if already completed
        If completedTasks.ContainsKey(key) Then
            Return completedTasks(key)
        End If

        ' Wait for completion
        While (DateTime.Now - startTime).TotalMilliseconds < timeoutMs
            ' Check if completed
            If completedTasks.ContainsKey(key) Then
                Return completedTasks(key)
            End If

            ' Check if task exists at all
            Dim status As TaskStatus = GetTaskStatus(key)
            If status = TaskStatus.NotFound Then
                Return New DownloadResult With {
                    .Success = False,
                    .ErrorMessage = "Task not found",
                    .CompletedTime = DateTime.Now
                }
            End If

            Thread.Sleep(100) ' Check every 100ms
        End While

        ' Timeout
        Return New DownloadResult With {
            .Success = False,
            .ErrorMessage = "Wait timeout exceeded",
            .CompletedTime = DateTime.Now
        }
    End Function

    ' Wait for download with callback (non-blocking alternative)
    Public Sub WaitForDownloadAsync(key As String, callback As Action(Of DownloadResult), Optional timeoutMs As Integer = 300000)
        Dim waitThread As New Thread(Sub()
                                         Dim result As DownloadResult = WaitForDownload(key, timeoutMs)
                                         callback(result)
                                     End Sub)
        waitThread.IsBackground = True
        waitThread.Start()
    End Sub

    ' Clear completed results (cleanup)
    Public Sub ClearCompletedResults()
        completedTasks.Clear()
    End Sub

    ' Clear specific result (useful before re-queuing)
    Public Function ClearResult(key As String) As Boolean
        If completedTasks.ContainsKey(key) Then
            Dim removed As DownloadResult = Nothing
            Return completedTasks.TryRemove(key, removed)
        End If
        Return False
    End Function

    ' Get result if available (non-blocking check)
    Public Function TryGetResult(key As String, ByRef result As DownloadResult) As Boolean
        If completedTasks.ContainsKey(key) Then
            result = completedTasks(key)
            Return True
        End If
        Return False
    End Function

    ' Check if download is complete (non-blocking)
    Public Function IsComplete(key As String) As Boolean
        Return completedTasks.ContainsKey(key)
    End Function

    ' Poll for results in loop (non-blocking)
    Public Function PollForResult(key As String, checkIntervalMs As Integer, maxChecks As Integer) As DownloadResult
        Dim checks As Integer = 0

        While checks < maxChecks
            ' Check if result available
            Dim result As DownloadResult = Nothing
            If TryGetResult(key, result) Then
                Return result
            End If

            ' Check if task exists
            Dim status As TaskStatus = GetTaskStatus(key)
            If status = TaskStatus.NotFound Then
                Return New DownloadResult With {
                    .Success = False,
                    .ErrorMessage = "Task not found",
                    .CompletedTime = DateTime.Now
                }
            End If

            ' Wait before next check
            Thread.Sleep(checkIntervalMs)
            checks += 1
        End While

        ' Max checks exceeded
        Return New DownloadResult With {
            .Success = False,
            .ErrorMessage = $"Polling timeout after {maxChecks} checks",
            .CompletedTime = DateTime.Now
        }
    End Function

    ' Wait for all downloads to complete
    Public Function WaitForAll(timeoutMs As Integer) As Boolean
        Dim startTime As DateTime = DateTime.Now

        While (DateTime.Now - startTime).TotalMilliseconds < timeoutMs
            ' Check if any downloads are still running or queued
            If runningCount = 0 AndAlso downloadQueue.Count = 0 Then
                Return True ' All done
            End If
            Thread.Sleep(100)
        End While

        Return False ' Timeout
    End Function

    ' Get all completed results
    Public Function GetAllResults() As Dictionary(Of String, DownloadResult)
        Dim results As New Dictionary(Of String, DownloadResult)
        For Each kvp In completedTasks
            results.Add(kvp.Key, kvp.Value)
        Next
        Return results
    End Function

    Public Sub Dispose()
        CancelAll()
        For Each downloader In downloads.Values
            downloader.Dispose()
        Next
        downloads.Clear()
    End Sub

    ' Internet connectivity checking methods
    Private Function IsInternetAvailable() As Boolean
        Try
            ' Method 1: Ping Google DNS
            Using ping As New Ping()
                Dim reply As PingReply = ping.Send("8.8.8.8", 3000)
                If reply.Status = IPStatus.Success Then
                    Return True
                End If
            End Using

            ' Method 2: Try HTTP request to reliable site
            Try
                Dim request As HttpWebRequest = DirectCast(WebRequest.Create("http://www.google.com"), HttpWebRequest)
                request.Timeout = 3000
                request.Method = "HEAD"
                Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                    Return response.StatusCode = HttpStatusCode.OK
                End Using
            Catch
                ' HTTP method failed, try ping again
            End Try

            ' Method 3: Check network interface
            If NetworkInterface.GetIsNetworkAvailable() Then
                Return True
            End If

        Catch ex As Exception
            Console.WriteLine($"Internet check failed: {ex.Message}")
        End Try

        Return False
    End Function

    ' Check internet with custom timeout
    Public Function CheckInternetConnection(Optional timeoutMs As Integer = 3000) As Boolean
        Try
            Using ping As New Ping()
                Dim reply As PingReply = ping.Send("8.8.8.8", timeoutMs)
                Return reply.Status = IPStatus.Success
            End Using
        Catch
            Return False
        End Try
    End Function

    ' Enable/disable internet checking
    Public Sub SetInternetCheckEnabled(enabled As Boolean)
        checkInternetOnEnqueue = enabled
    End Sub

    ' Check if internet checking is enabled
    Public Function IsInternetCheckEnabled() As Boolean
        Return checkInternetOnEnqueue
    End Function


    Public Function CheckAndDeleteFile(mDownloadFolder As String, mDownloadFile As String) As Boolean
        Dim fullPath As String = System.IO.Path.Combine(mDownloadFolder, mDownloadFile)
        Return CheckAndDeleteFile(fullPath)
    End Function

    Public Function CheckAndDeleteFile(fullPath As String) As Boolean
        Try
            If System.IO.File.Exists(fullPath) Then
                ' Remove read-only or hidden attributes before deleting
                Dim fileInfo As New System.IO.FileInfo(fullPath)
                fileInfo.Attributes = IO.FileAttributes.Normal
                ' Attempt hard delete
                System.IO.File.Delete(fullPath)
                ' Double-check if file is gone
                If System.IO.File.Exists(fullPath) Then
                    Throw New IOException("File could not be deleted (locked or in use).")
                End If
                Return True
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CheckRemoteFileExists(pUrl As String) As Boolean
        Try
            Dim request As HttpWebRequest = CType(WebRequest.Create(pUrl), HttpWebRequest)
            request.Method = "HEAD"
            request.Timeout = 5000 ' 5 seconds timeout
            request.AllowAutoRedirect = True

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Return (response.StatusCode = HttpStatusCode.OK)
            End Using

        Catch ex As WebException
            ' Handle 404 or other HTTP errors
            Dim response As HttpWebResponse = TryCast(ex.Response, HttpWebResponse)
            If response IsNot Nothing Then
                If response.StatusCode = HttpStatusCode.NotFound Then
                    ' mErrorMessage = "Remote file not found (404)."
                Else
                    ' mErrorMessage = "HTTP error: " & response.StatusCode.ToString()
                End If
            Else
                'mErrorMessage = "Connection error: " & ex.Message
            End If
            Return False

        Catch ex As Exception
            'mErrorMessage = "Error checking remote file: " & ex.Message
            Return False
        End Try
    End Function


    Public Sub DeleteFilesByExtension(folderPath As String, extension As String)
        Try
            ' Check folder existence
            If String.IsNullOrWhiteSpace(folderPath) OrElse Not IO.Directory.Exists(folderPath) Then Exit Sub
            If String.IsNullOrWhiteSpace(extension) Then Exit Sub

            ' Normalize extension (ensure it starts with a dot)
            If Not extension.StartsWith(".") Then extension = "." & extension

            ' Choose search option
            Dim searchOpt As IO.SearchOption = If(False, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly)

            ' Get all matching files
            Dim files = IO.Directory.GetFiles(folderPath, "*" & extension, searchOpt)

            For Each f In files
                Try
                    ' ✅ Skip file if it's currently locked/in use
                    If Not IsFileLocked(f) Then
                        IO.File.Delete(f)
                    End If
                Catch
                    ' Ignore deletion failures silently
                End Try
            Next

        Catch ex As Exception
            ' Optional: log or handle error
        End Try
    End Sub

    ''' <summary>
    ''' Checks if the specified file is locked (in use by another process).
    ''' </summary>
    Public Function IsFileLocked(filePath As String) As Boolean
        Try
            Using fs As IO.FileStream = IO.File.Open(filePath, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.None)
                ' If open succeeds, file is not locked
                Return False
            End Using
        Catch ex As IO.IOException
            ' IOException → file is locked or in use
            Return True
        Catch
            ' Other errors (permissions, etc.)
            Return True
        End Try
    End Function



End Class

Public Class DownloadTask
    Public Property Key As String
    Public Property Url As String
    Public Property DestinationPath As String
    Public Property Label As Label
    Public Property TimeoutSeconds As Integer
End Class

Public Enum TaskStatus
    NotFound = 0
    Queued = 1
    Downloading = 2
    Completed = 3
    Failed = 4
    Cancelled = 5
End Enum

Public Class DownloadResult
    Public Property Success As Boolean
    Public Property ErrorMessage As String
    Public Property CompletedTime As DateTime
End Class
