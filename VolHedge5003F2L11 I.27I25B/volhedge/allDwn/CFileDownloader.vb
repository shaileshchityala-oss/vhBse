Imports System.IO
Imports System.Net
Public Class CNetFileDownloadContext
    Public mIsSuccessPreProcess As Boolean
    Public mIsSuccessDownload As Boolean
    Public mIsSuccessPostProcess As Boolean
    Public mIsSuccessAll As Boolean
    Public mErrorMessage As String

    Public mUrl As String
    Public mDownloadFolder As String
    Public mDownloadFile As String
    Public mDownloadedFilePath As String
    Public mDownIDKey As String

    Sub New(pUrl As String, pDnFolder As String, pDnFile As String)
        mUrl = pUrl
        mDownloadFolder = pDnFolder
        mDownloadFile = pDnFile
    End Sub

    'Public Function CheckDownloadFile(Optional deleteIfExists As Boolean = False) As Boolean
    '    Try
    '        Dim fullPath As String = System.IO.Path.Combine(mDownloadFolder, mDownloadFile)

    '        If System.IO.File.Exists(fullPath) Then
    '            If deleteIfExists Then
    '                ' Remove read-only or hidden attributes before deleting
    '                Dim fileInfo As New System.IO.FileInfo(fullPath)
    '                fileInfo.Attributes = IO.FileAttributes.Normal

    '                ' Attempt hard delete
    '                System.IO.File.Delete(fullPath)

    '                ' Double-check if file is gone
    '                If System.IO.File.Exists(fullPath) Then
    '                    Throw New IOException("File could not be deleted (locked or in use).")
    '                End If
    '            End If
    '            Return True
    '        Else
    '            Return False
    '        End If

    '    Catch ex As Exception
    '        mErrorMessage = "Hard delete failed: " & ex.Message
    '        Return False
    '    End Try
    'End Function


End Class
