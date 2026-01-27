' ---------------------------------------------------------------------------
' Campari Software
'
' FileDownloader.cs
'
' Provides the implementation of a FileDownloader. This class encapsulates
' all of the communication required to download a file from a URI.
'
' ---------------------------------------------------------------------------
' Copyright (C) 2006-2007 Campari Software
' All rights reserved.
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
' FITNESS FOR A PARTICULAR PURPOSE.
' ---------------------------------------------------------------------------
Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Threading


Namespace UpdaterCore
#Region "class FileDownloader"
    ''' <summary>
    ''' Download a file from a URI.
    ''' </summary>
    Public Class FileDownloader
        Inherits Component
        Implements IDisposable
#Region "events"

        ''' <summary>
        ''' Represents the method that will handle the <see cref="FileDownloader.DownloadCompleted"/> event of a <see cref="FileDownloader"/> download request.
        ''' </summary>
        ''' <remarks>When you create a <see cref="System.EventHandler"/> delegate, you identify the method that 
        ''' will handle the event. To associate the event with your event handler, add an instance of the delegate
        ''' to the event. The event handler is called whenever the event occurs, unless you remove the delegate.
        ''' For more information about event handler delegates, see Events and Delegates.</remarks>
        Public Event DownloadCompleted As EventHandler(Of FileDownloadCompletedEventArgs)

        ''' <summary>
        ''' Represents the method that will handle the <see cref="FileDownloader.DownloadProgressChanged"/> event of a <see cref="FileDownloader"/> download request.
        ''' </summary>
        ''' <remarks>When you create a <see cref="System.EventHandler"/> delegate, you identify the method that 
        ''' will handle the event. To associate the event with your event handler, add an instance of the delegate
        ''' to the event. The event handler is called whenever the event occurs, unless you remove the delegate.
        ''' For more information about event handler delegates, see Events and Delegates.</remarks>
        Public Event DownloadProgressChanged As EventHandler(Of FileDownloadProgressChangedEventArgs)

        ''' <summary>
        ''' Represents the method that will handle the <see cref="FileDownloader.DownloadStatusChanged"/> event of a <see cref="FileDownloader"/> download request.
        ''' </summary>
        ''' <remarks>When you create a <see cref="System.EventHandler"/> delegate, you identify the method that 
        ''' will handle the event. To associate the event with your event handler, add an instance of the delegate
        ''' to the event. The event handler is called whenever the event occurs, unless you remove the delegate.
        ''' For more information about event handler delegates, see Events and Delegates.</remarks>
        Public Event DownloadStatusChanged As EventHandler(Of FileDownloadStatusChangedEventArgs)

#End Region

#Region "class-wide fields"
        Private Const DefaultBlockSize As Integer = 1024

        Private backgroundWorker As BackgroundWorker
        Private m_blockSize As Integer = FileDownloader.DefaultBlockSize
        Private m_credentials As ICredentials
        Private disposed As Boolean
        Private m_downloadHtml As Boolean
        Private m_downloadingTo As String
        Private m_overwrite As Boolean
        Private m_preAuthenticate As Boolean
        Private m_proxy As IWebProxy
        Private syncEvent As AutoResetEvent
        Private m_timeout As TimeSpan = FileDownloader.IndefiniteTimeout
        Private m_userAgent As String = Nothing
#End Region

#Region "private and internal properties and methods"

#Region "properties"

#End Region

#Region "methods"

#Region "CheckDisposed"
        ''' <summary>
        ''' Protects against the <see cref="FileDownloader"/> from being used
        ''' after it has been Disposed.
        ''' </summary>
        Private Sub CheckDisposed()
            If Me.disposed Then
                Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
            End If
        End Sub
#End Region

#Region "DownloadAsyncCompleted"
        ''' <summary>
        ''' Raises the <see cref="DownloadCompleted"/> event by responding to
        ''' the <see cref="BackgroundWorker.RunWorkerCompleted"/> event of 
        ''' the background worker.
        ''' </summary>
        ''' <param name="sender">The background worker that completed.</param>
        ''' <param name="e">The completion data.</param>
        Private Sub DownloadAsyncCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            ' Signal that the work has been completed
            Me.syncEvent.[Set]()

            Me.OnDownloadCompleted(e)
        End Sub
#End Region

#Region "DownloadAsyncCore"
        ''' <summary>
        ''' Downloads the file by responding to the 
        ''' <see cref="BackgroundWorker.DoWork"/> event of the background
        ''' worker.
        ''' </summary>
        ''' <param name="sender">The background worker that requested the work.</param>
        ''' <param name="e">The data required to do the work.</param>
        Private Sub DownloadAsyncCore(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Dim args As Object() = TryCast(e.Argument, Object())
            Dim worker As BackgroundWorker = TryCast(sender, BackgroundWorker)

            If args IsNot Nothing Then
                Dim buffer As Byte() = Nothing
                Dim url As Uri = TryCast(args(0), Uri)
                Dim destinationFolder As String = TryCast(args(1), String)

                Using downloadInfo As New DownloadInfo()
                    downloadInfo.Proxy = Me.Proxy
                    downloadInfo.Credentials = Me.Credentials
                    downloadInfo.PreAuthenticate = Me.PreAuthenticate
                    downloadInfo.DownloadHtml = Me.DownloadHtml
                    downloadInfo.UserAgent = Me.m_userAgent

                    downloadInfo.Initialize(url, destinationFolder)

                    ' Find out the name of the file that the web server gave us.
                    Dim destFileName As String = Path.GetFileName(downloadInfo.Response.ResponseUri.ToString())
                    Me.m_downloadingTo = Path.GetFullPath(Path.Combine(destinationFolder, destFileName))

                    If Me.m_overwrite Then
                        ' This is safe because no exceptions will be thrown
                        ' if the file doesn't exist.
                        File.Delete(Me.m_downloadingTo)
                    End If

                    ' Create the file on disk here, so even if we don't receive any data of the file
                    ' it's still on disk. This allows us to download 0-byte files.
                    If Not File.Exists(Me.m_downloadingTo) Then
                        Dim fs As FileStream = File.Create(Me.m_downloadingTo)
                        fs.Close()
                    End If

                    ' create the download buffer
                    buffer = New Byte(Me.m_blockSize - 1) {}

                    Dim readCount As Integer

                    ' update how many bytes have already been read
                    Dim totalDownloaded As Long = downloadInfo.StartPoint

                    While CInt(InlineAssignHelper(readCount, downloadInfo.DownloadStream.Read(buffer, 0, Me.m_blockSize))) > 0
                        ' break on cancel
                        If worker.CancellationPending Then
                            e.Cancel = True
                            downloadInfo.Close()

                            Exit While
                        End If

                        ' update total bytes read
                        totalDownloaded += readCount

                        ' save block to end of file
                        SaveToFile(buffer, readCount, Me.m_downloadingTo)

                        ' send progress info
                        If downloadInfo.IsProgressKnown Then
                            Dim progress As Integer = CInt(Math.Truncate((CDbl(totalDownloaded) / downloadInfo.Length) * 100))
                            worker.ReportProgress(progress, New Long() {totalDownloaded, downloadInfo.Length})
                        End If
                    End While
                End Using
                e.Result = m_downloadingTo
            End If
        End Sub
#End Region

#Region "DownloadAsyncProgressChanged"
        ''' <summary>
        ''' Raises the <see cref="DownloadProgressChanged"/> event by 
        ''' responding to the <see cref="BackgroundWorker.ProgressChanged"/> 
        ''' event of the background worker.
        ''' </summary>
        ''' <param name="sender">The background worker whose progress changed.</param>
        ''' <param name="e">The progress change data.</param>
        Private Sub DownloadAsyncProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
            Dim args As Long() = TryCast(e.UserState, Long())

            If args IsNot Nothing Then
                Me.OnDownloadProgressChanged(args(0), args(1))
            Else
                ' If the UserState isn't an array of longs, then it should be a status message.
                Dim message As String = TryCast(e.UserState, String)
                Me.OnDownloadStatusChanged(message)
            End If
        End Sub
#End Region

#Region "OnDownloadCompleted"
        ''' <summary>
        ''' Raises the <see cref="DownloadCompleted"/> event.
        ''' </summary>
        ''' <param name="e">The completion data.</param>
        Private Sub OnDownloadCompleted(ByVal e As RunWorkerCompletedEventArgs)
            If Me.CanRaiseEvents Then
                'If Me.DownloadCompleted IsNot Nothing Then
                '    Dim downloadCompleteEventArgs As FileDownloadCompletedEventArgs

                '    If e.[Error] Is Nothing Then
                '        downloadCompleteEventArgs = New FileDownloadCompletedEventArgs(e.Result.ToString(), e.[Error], e.Cancelled)
                '    Else
                '        downloadCompleteEventArgs = New FileDownloadCompletedEventArgs([String].Empty, e.[Error], e.Cancelled)
                '    End If
                '    Me.DownloadCompleted(Me, downloadCompleteEventArgs)
                'End If
            End If
        End Sub
#End Region

#Region "OnDownloadProgressChanged"
        ''' <summary>
        ''' Raises the <see cref="DownloadProgressChanged"/> event.
        ''' </summary>
        ''' <param name="totalDownloaded">The total number of bytes downloaded.</param>
        ''' <param name="totalBytesToReceive">The total number of bytes to be downloaded.</param>
        Private Sub OnDownloadProgressChanged(ByVal totalDownloaded As Long, ByVal totalBytesToReceive As Long)
            'If Me.CanRaiseEvents Then
            '    If Me.DownloadProgressChanged IsNot Nothing Then
            '        Dim eventArgs As New FileDownloadProgressChangedEventArgs(totalDownloaded, totalBytesToReceive)
            '        Me.DownloadProgressChanged(Me, eventArgs)
            '    End If
            'End If
        End Sub
#End Region

#Region "OnDownloadStatusChanged"
        ''' <summary>
        ''' Raises the <see cref="DownloadStatusChanged"/> event.
        ''' </summary>
        ''' <param name="message">The status message.</param>
        Private Sub OnDownloadStatusChanged(ByVal message As String)
            'If Me.CanRaiseEvents Then
            '    If Me.DownloadStatusChanged IsNot Nothing Then
            '        Dim eventArgs As New FileDownloadStatusChangedEventArgs(message)
            '        Me.DownloadStatusChanged(Me, eventArgs)
            '    End If
            'End If
        End Sub
#End Region

#Region "SaveToFile"
        ''' <summary>
        ''' Saves the block of bytes to the specified file.
        ''' </summary>
        ''' <param name="buffer">The <see cref="Byte"/> array to save.</param>
        ''' <param name="count">The maximum number of bytes to be written.</param>
        ''' <param name="fileName">The file to receive the block of bytes.</param>
        Private Shared Sub SaveToFile(ByVal buffer As Byte(), ByVal count As Integer, ByVal fileName As String)
            Using stream As FileStream = File.Open(fileName, FileMode.Append, FileAccess.Write)
                stream.Write(buffer, 0, count)
            End Using
        End Sub
#End Region

#End Region

#End Region

#Region "public properties and methods"

#Region "properties"

#Region "BlockSize"
        ''' <summary>
        ''' Gets or sets the download block size in Kilobytes (KB).
        ''' </summary>
        ''' <value>The block size used by the download buffer.</value>
        ''' <remarks>The value cannot be set to less than the default block size of 1024.</remarks>
        Public Property BlockSize() As Integer
            Get
                Me.CheckDisposed()
                Return Me.m_blockSize
            End Get
            Set(ByVal value As Integer)
                Me.CheckDisposed()
                If Value < FileDownloader.DefaultBlockSize Then
                    Value = FileDownloader.DefaultBlockSize
                End If
                Me.m_blockSize = Value
            End Set
        End Property
#End Region

#Region "Credentials"
        ''' <summary>
        ''' Gets or sets the network credentials used for authenticating the the request with the Internet resource.
        ''' </summary>
        ''' <value>The <see cref="ICredentials"/> containing the authentication credentials associated with the request. 
        ''' The default value is a <see langword="null"/>.</value>
        ''' <remarks>The <see cref="Credentials"/> property contains the authentication credentials required to access
        ''' the Internet resource.
        ''' </remarks>
        Public Property Credentials() As ICredentials
            Get
                Me.CheckDisposed()
                Return Me.m_credentials
            End Get
            Set(ByVal value As ICredentials)
                Me.CheckDisposed()
                Me.m_credentials = Value
            End Set
        End Property
#End Region

#Region "DownloadHtml"
        ''' <summary>
        ''' Gets or sets a value indicating if HTML pages should be allowed
        ''' to be downloaded.
        ''' </summary>
        ''' <value><see langowrd="true"/> to download HTML pages; otherwise,
        ''' <see langword="false"/>.</value>
        Public Property DownloadHtml() As Boolean
            Get
                Me.CheckDisposed()
                Return Me.m_downloadHtml
            End Get
            Set(ByVal value As Boolean)
                Me.CheckDisposed()
                Me.m_downloadHtml = Value
            End Set
        End Property
#End Region

#Region "DownloadingTo"
        ''' <summary>
        ''' Gets the name of the destination file.
        ''' </summary>
        ''' <value>A <see cref="String"/> containing the destination file name.</value>
        ''' <remarks>The <see cref="DownloadingTo"/> property will be <see langword="null"/> until a download
        ''' request has Successfully contacted the server and has begun downloading the file.</remarks>
        Public ReadOnly Property DownloadingTo() As String
            Get
                Me.CheckDisposed()
                Return Me.m_downloadingTo
            End Get
        End Property
#End Region

#Region "IndefiniteTimeout"
        ''' <summary>
        ''' Gets a value that represents an indefinate timeout for a 
        ''' synchronous download operation.
        ''' </summary>
        ''' <value>A <see cref="TimeSpan"/> that represents an indefinate
        ''' timeout.</value>
        Public Shared ReadOnly Property IndefiniteTimeout() As TimeSpan
            Get
                Return New TimeSpan(0, 0, 0, 0, -1)
            End Get
        End Property
#End Region

#Region "IsBusy"
        ''' <summary>
        ''' Gets a value indicating whether the <see cref="FileDownloader"/> is
        ''' running an asynchronous operation.
        ''' </summary>
        ''' <value><see langword="true"/> if an asynchronous operation is
        ''' running; otherwise, <see langword="false"/>.</value>
        Public ReadOnly Property IsBusy() As Boolean
            Get
                Me.CheckDisposed()
                Return Me.backgroundWorker.IsBusy
            End Get
        End Property
#End Region

#Region "Overwrite"
        ''' <summary>
        ''' Gets or sets a value indicating if the existing file should be
        ''' overwritten.
        ''' </summary>
        ''' <value><see langword="true"/> if the file should be overwritten;
        ''' otherwise, <see langword="false"/>.</value>
        Public Property Overwrite() As Boolean
            Get
                Me.CheckDisposed()
                Return Me.m_overwrite
            End Get
            Set(ByVal value As Boolean)
                Me.CheckDisposed()
                Me.m_overwrite = Value
            End Set
        End Property
#End Region

#Region "PreAuthenticate"
        ''' <summary>
        ''' Indicates whether to pre-authenticate the request.
        ''' </summary>
        ''' <value><see langword="true"/> to pre-authenticate; otherwise,
        ''' <see langword="false"/>.</value>
        ''' <remarks>With the exception of the first request, the 
        ''' <see cref="PreAuthenticate"/> property indicates whether to send
        ''' authentication information with subsequent requests without waiting
        ''' to be challenged by the server. When <see cref="PreAuthenticate"/>
        ''' is <see langword="false"/>, the <see cref="WebRequest"/> waits
        ''' for an authentication challenge before sending authentication
        ''' information.</remarks>
        Public Property PreAuthenticate() As Boolean
            Get
                Me.CheckDisposed()
                Return Me.m_preAuthenticate
            End Get
            Set(ByVal value As Boolean)
                Me.CheckDisposed()
                Me.m_preAuthenticate = Value
            End Set
        End Property
#End Region

#Region "Proxy"
        ''' <summary>
        ''' Gets or sets proxy information for the request.
        ''' </summary>
        ''' <value>The <see cref="IWebProxy"/> object to use to proxy the request. The default value is set to the <see cref="WebRequest.DefaultWebProxy"/> property. </value>
        ''' <remarks>The <see cref="Proxy"/> property identifies the WebProxy object to use to process requests to Internet
        ''' resources. To specify that no proxy should be used, set the Proxy property to the proxy instance returned by 
        ''' the <see cref="GlobalProxySelection.GetEmptyWebProxy"/> method.
        ''' </remarks>
        Public Property Proxy() As IWebProxy
            Get
                Me.CheckDisposed()
                Return Me.m_proxy
            End Get
            Set(ByVal value As IWebProxy)
                Me.CheckDisposed()
                Me.m_proxy = Value
            End Set
        End Property
#End Region

#Region "Timeout"
        ''' <summary>
        ''' Gets or sets a value indicating the timeout for asynchronous
        ''' download operations.
        ''' </summary>
        ''' <value>A <see cref="TimeSpan"/> representing the timeout.</value>
        Public Property Timeout() As TimeSpan
            Get
                Me.CheckDisposed()
                Return Me.m_timeout
            End Get
            Set(ByVal value As TimeSpan)
                Me.CheckDisposed()
                Me.m_timeout = Value
            End Set
        End Property
#End Region

#Region "UserAgent"
        ''' <summary>
        ''' Gets or sets the value of the <b>User-agent</b> HTTP header. 
        ''' </summary>
        ''' <value>The value of the <b>User-agent</b> HTTP header. The default
        ''' value is <see langword="null"/>.</value>
        Public Property UserAgent() As String
            Get
                Me.CheckDisposed()
                Return Me.m_userAgent
            End Get
            Set(ByVal value As String)
                Me.CheckDisposed()
                Me.m_userAgent = Value
            End Set
        End Property
#End Region

#End Region

#Region "methods"

#Region "constructor"
        ''' <summary>
        ''' Initializes a new instance of the <see cref="FileDownloader"/> class.
        ''' </summary>
        Public Sub New()
            ' Set the default web proxy
            Me.m_proxy = WebRequest.DefaultWebProxy

            ' Initialize the sync event for synchronous downloads
            Me.syncEvent = New AutoResetEvent(True)

            ' Initialize the background worker for async downloads
            Me.backgroundWorker = New BackgroundWorker()
            Me.backgroundWorker.WorkerReportsProgress = True
            Me.backgroundWorker.WorkerSupportsCancellation = True
            AddHandler Me.backgroundWorker.DoWork, New System.ComponentModel.DoWorkEventHandler(AddressOf Me.DownloadAsyncCore)
            AddHandler Me.backgroundWorker.RunWorkerCompleted, New System.ComponentModel.RunWorkerCompletedEventHandler(AddressOf Me.DownloadAsyncCompleted)
            AddHandler Me.backgroundWorker.ProgressChanged, New System.ComponentModel.ProgressChangedEventHandler(AddressOf Me.DownloadAsyncProgressChanged)
        End Sub
#End Region

#Region "Dispose"
        ''' <summary>
        ''' Releases the resources, other than memory, that are used by the <see cref="FileDownloader"/>. 
        ''' </summary>
        ''' <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If Not disposed Then
                    If disposing Then
                        ' Dispose managed resources.
                        If Me.backgroundWorker IsNot Nothing Then
                            RemoveHandler Me.backgroundWorker.DoWork, AddressOf Me.DownloadAsyncCore
                            RemoveHandler Me.backgroundWorker.RunWorkerCompleted, AddressOf Me.DownloadAsyncCompleted
                            RemoveHandler Me.backgroundWorker.ProgressChanged, AddressOf Me.DownloadAsyncProgressChanged
                            Me.backgroundWorker.Dispose()
                        End If

                        If Me.syncEvent IsNot Nothing Then
                            Me.syncEvent.Close()
                        End If
                    End If
                End If
            Finally
                MyBase.Dispose(disposing)
                disposed = True
            End Try
        End Sub
#End Region

#Region "Download"

#Region "Download(string url)"
        ''' <summary>
        ''' Download the file at the specified url, and save it to the current folder.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file to be downloaded.</param>
        Public Sub Download(ByVal url As String)
            If [String].IsNullOrEmpty(url) Then
                Throw New ArgumentNullException("url")
            End If

            Download(New Uri(url), [String].Empty)
        End Sub
#End Region

#Region "Download(string url, string destinationFolder)"
        ''' <summary>
        ''' Download the file at the specified url, and save it to the specified folder.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file to be downloaded.</param>
        ''' <param name="destinationFolder">The output destination for the file.</param>
        Public Sub Download(ByVal url As String, ByVal destinationFolder As String)
            If [String].IsNullOrEmpty(url) Then
                Throw New ArgumentNullException("url")
            End If

            If destinationFolder Is Nothing Then
                Throw New ArgumentNullException("destinationFolder")
            End If

            Download(New Uri(url), destinationFolder)
        End Sub
#End Region

#Region "Download(Uri url)"
        ''' <summary>
        ''' Download the file at the specified url, and save it to the current folder.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file to be downloaded.</param>
        Public Sub Download(ByVal url As Uri)

            Download(url, [String].Empty)
        End Sub
#End Region

#Region "Download(Uri url, string destinationFolder)"
        ''' <summary>
        ''' Download the file at the specified url, and save it to the specified folder.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file to be downloaded.</param>
        ''' <param name="destinationFolder">The output destination for the file.</param>
        Public Sub Download(ByVal url As Uri, ByVal destinationFolder As String)
            If destinationFolder Is Nothing Then
                Throw New ArgumentNullException("destinationFolder")
            End If

            Me.CheckDisposed()

            Me.backgroundWorker.RunWorkerAsync(New Object() {url, destinationFolder})
            Me.syncEvent.WaitOne(Me.m_timeout, False)
        End Sub
#End Region

#End Region

#Region "DownloadAsync"

#Region "DownloadAsync(string url)"
        ''' <summary>
        ''' Begin downloading the file at the specified url, and save it to the current folder.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file to be downloaded.</param>
        Public Sub DownloadAsync(ByVal url As String)
            If [String].IsNullOrEmpty(url) Then
                Throw New ArgumentNullException("url")
            End If

            DownloadAsync(New Uri(url), [String].Empty)
        End Sub
#End Region

#Region "DownloadAsync(string url, string destinationFolder)"
        ''' <summary>
        ''' Begin downloading the file at the specified url, and save it to the specified folder.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file to be downloaded.</param>
        ''' <param name="destinationFolder">The output destination for the file.</param>
        Public Sub DownloadAsync(ByVal url As String, ByVal destinationFolder As String)
            If [String].IsNullOrEmpty(url) Then
                Throw New ArgumentNullException("url")
            End If

            If destinationFolder Is Nothing Then
                Throw New ArgumentNullException("destinationFolder")
            End If

            DownloadAsync(New Uri(url), destinationFolder)
        End Sub
#End Region

#Region "DownloadAsync(Uri url)"
        ''' <summary>
        ''' Begin downloading the file at the specified url, and save it to the current folder.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file to be downloaded.</param>
        Public Sub DownloadAsync(ByVal url As Uri)
            DownloadAsync(url, [String].Empty)
        End Sub
#End Region

#Region "DownloadAsync(Uri url, string destinationFolder)"
        ''' <summary>
        ''' Begin downloading the file at the specified url, and save it to the specified folder.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file to be downloaded.</param>
        ''' <param name="destinationFolder">The output destination for the file.</param>
        Public Sub DownloadAsync(ByVal url As Uri, ByVal destinationFolder As String)
            If destinationFolder Is Nothing Then
                Throw New ArgumentNullException("destinationFolder")
            End If

            Me.CheckDisposed()

            Me.backgroundWorker.RunWorkerAsync(New Object() {url, destinationFolder})
        End Sub
#End Region

#End Region

#Region "DownloadAsyncCancel"
        ''' <summary>
        ''' Cancel the current download operation.
        ''' </summary>
        Public Sub DownloadAsyncCancel()
            Me.CheckDisposed()

            Me.backgroundWorker.CancelAsync()
        End Sub
#End Region

#Region "GetLastModifiedDate"

#Region "GetLastModifiedDate(string url)"
        ''' <summary>
        ''' Retrieves the last modified date of the file at the specified url.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file.</param>
        ''' <returns>The <see cref="DateTime"/> the file was last modified on
        ''' the server, in Universal Time. If the last modified date is not 
        ''' able to be determined, a <see langword="null"/> is returned.</returns>
        Public Function GetLastModifiedDate(ByVal url As String) As System.Nullable(Of DateTime)
            If [String].IsNullOrEmpty(url) Then
                Throw New ArgumentNullException("url")
            End If

            Return GetLastModifiedDate(New Uri(url))
        End Function
#End Region

#Region "GetLastModifiedDate(Uri url)"
        ''' <summary>
        ''' Retrieves the last modified date of the file at the specified url.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file.</param>
        ''' <returns>The <see cref="DateTime"/> the file was last modified on
        ''' the server, in Universal Time. If the last modified date is not 
        ''' able to be determined, a <see langword="null"/> is returned.</returns>
        Public Function GetLastModifiedDate(ByVal url As Uri) As System.Nullable(Of DateTime)
            Me.CheckDisposed()

            Using downloadInfo As New DownloadInfo()
                downloadInfo.Proxy = Me.Proxy
                downloadInfo.Credentials = Me.Credentials
                downloadInfo.PreAuthenticate = Me.PreAuthenticate
                downloadInfo.DownloadHtml = Me.DownloadHtml

                Return downloadInfo.GetLastModifiedDate(url)
            End Using
        End Function
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
            target = value
            Return value
        End Function
#End Region

#End Region

#End Region

#End Region

        Private Sub FileDownloader_DownloadCompleted(ByVal sender As Object, ByVal e As FileDownloadCompletedEventArgs) Handles Me.DownloadCompleted
            'MsgBox("DownloadHtml Complete")
        End Sub
    End Class
#End Region
End Namespace
