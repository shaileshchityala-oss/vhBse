' ---------------------------------------------------------------------------
' Campari Software
'
' DownloadInfo.cs
'
' Provides the DownloadInfo class used internally by the FileDownloader class. This
' class provides the actual implementation details for downloading a file from a URI.
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
Imports System.Globalization
Imports System.IO
Imports System.Net

Namespace UpdaterCore
#Region "class DownloadInfo"
    ''' <summary>
    ''' Provides instance methods for the starting and resuming the download of a file.
    ''' This class cannot be inherited.  
    ''' </summary>
    Friend NotInheritable Class DownloadInfo
        Implements IDisposable
#Region "events"

#End Region

#Region "class-wide fields"
        Private m_credentials As ICredentials
        Private m_disposed As Boolean
        Private m_downloadHtml As Boolean
        Private m_downloadStream As Stream
        Private m_length As Long
        Private m_preAuthenticate As Boolean
        Private m_proxy As IWebProxy
        Private m_response As WebResponse
        Private start As Long
        Private m_userAgent As String = Nothing
#End Region

#Region "private and internal properties and methods"

#Region "properties"

#End Region

#Region "methods"

#Region "CheckDisposed"
        ''' <summary>
        ''' Protects against the <see cref="DownloadInfo"/> from being used
        ''' after it has been Disposed.
        ''' </summary>
        Private Sub CheckDisposed()
            If Me.Disposed Then
                Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
            End If
        End Sub
#End Region

#Region "Dispose"
        ''' <summary>
        ''' Releases the resources, other than memory, that are used by the <see cref="DownloadInfo"/>. 
        ''' </summary>
        ''' <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        Private Sub Dispose(ByVal disposing As Boolean)
            ' Dispose(bool disposing) executes in two distinct scenarios. If 
            ' disposing equals true, the method has been called directly or
            ' indirectly by a user's code. Managed and unmanaged resources 
            ' can be disposed.
            '
            ' If disposing equals false, the method has been called by the 
            ' runtime from inside the finalizer and you should not reference 
            ' other objects. Only unmanaged resources can be disposed.
            '
            ' Note that this is not thread safe. Another thread could start 
            ' disposing the object after the managed resources are disposed,
            ' but before the disposed flag is set to true. If thread safety 
            ' is necessary, it must be implemented by the caller.

            If Not m_disposed Then
                If disposing Then
                    ' Dispose managed resources.
                    If Me.m_downloadStream IsNot Nothing AndAlso Me.m_downloadStream IsNot Stream.Null Then
                        Me.m_downloadStream.Close()
                    End If

                    If Me.m_response IsNot Nothing Then
                        Me.m_response.Close()
                    End If

                    ' There are no unmanaged resources to release, but
                    ' if we add them, they need to be released here.
                End If
            End If
            m_disposed = True
        End Sub
#End Region

#Region "GetFileSize"
        ''' <summary>
        ''' Gets the size of the file being downloaded.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file being downloaded.</param>
        ''' <returns>The size of the file in bytes. If the size cannot be
        ''' determined, a -1 is returned.</returns>
        Private Function GetFileSize(ByVal url As Uri) As Long
            Dim size As Long = -1

            Using response As WebResponse = GetRequest(url).GetResponse()
                size = response.ContentLength
            End Using

            Return size
        End Function
#End Region

#Region "GetRequest"
        ''' <summary>
        ''' Creates a <see cref="WebRequest"/> corresponding to the
        ''' <see cref="Uri"/> specified.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> used to create the request.</param>
        ''' <returns>A <see cref="WebRequest"/> corresponding to 
        ''' <see cref="Uri"/>.</returns>
        Private Function GetRequest(ByVal url As Uri) As WebRequest
            Dim request As WebRequest = WebRequest.Create(url)

            Dim webRequest__1 As HttpWebRequest = TryCast(request, HttpWebRequest)
            If webRequest__1 IsNot Nothing Then
                request.Credentials = Me.Credentials
                request.PreAuthenticate = Me.PreAuthenticate

                ' The default for the HttpWebRequest is for this value to
                ' be null, so only do this if the UserAgent value is not
                ' null or empty.
                If Not [String].IsNullOrEmpty(Me.UserAgent) Then
                    webRequest__1.UserAgent = Me.UserAgent
                End If
            End If
            request.Proxy = Me.Proxy
            Return request
        End Function

#End Region

#Region "GetServerLastModified"
        ''' <summary>
        ''' Gets the last modified date of the file from the server.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file.</param>
        ''' <returns>The <see cref="DateTime"/> the file was last modified on
        ''' the server, in Universal Time. If the last modified date is not 
        ''' able to be determined, a <see langword="null"/> is returned.</returns>
        ''' <remarks>If the <see cref="Uri"/> specified uses the HTTP or HTTPS
        ''' schemes, the last modified date is what is returned in the
        ''' <see cref="HttpWebResponse"/>. If the <see cref="Uri"/> uses the
        ''' FTP scheme, the last modified date is returned from the 
        ''' <see cref="WebRequestMethods.Ftp.GetDateTimestamp"/> request
        ''' method.</remarks>
        Private Function GetServerLastModified(ByVal url As Uri) As System.Nullable(Of DateTime)
            Dim lastModified As System.Nullable(Of DateTime) = Nothing
            Dim request As WebRequest = Nothing

            If url.Scheme = Uri.UriSchemeHttp OrElse url.Scheme = Uri.UriSchemeHttps Then
                request = TryCast(GetRequest(url), HttpWebRequest)
                If request IsNot Nothing Then
                    Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
                        ' Check to make sure the response isn't an error. If it is this method
                        ' will throw exceptions.
                        ValidateResponse(response, url)

                        lastModified = response.LastModified.ToUniversalTime()
                    End Using
                End If
            ElseIf url.Scheme = Uri.UriSchemeFtp Then
                request = TryCast(GetRequest(url), FtpWebRequest)
                If request IsNot Nothing Then
                    request.Method = WebRequestMethods.Ftp.GetDateTimestamp

                    Using response As FtpWebResponse = TryCast(request.GetResponse(), FtpWebResponse)
                        ' Check to make sure the response isn't an error. If it is this method
                        ' will throw exceptions.
                        ValidateResponse(response, url)

                        lastModified = response.LastModified.ToUniversalTime()
                    End Using
                End If
            End If
            Return lastModified
        End Function
#End Region

#Region "ValidateResponse"
        ''' <summary>
        ''' Validates the response recieved.
        ''' </summary>
        ''' <param name="response">The <see cref="WebResponse"/> received.</param>
        ''' <param name="url">The <see cref="Uri"/> of the request.</param>
        ''' <exception cref="ArgumentException">
        ''' <para>If the response received is an
        ''' <see cref="HttpWebResponse"/> that contains HTML data and the
        ''' <see cref="DownloadHtml"/> property is <see langword="false"/>.</para>
        ''' <para>-or-</para>
        ''' <para>The response recieved is an <see cref="FtpWebResponse"/> and the
        ''' <see cref="FtpWebResponse.StatusCode"/> is 
        ''' <see cref="FtpStatusCode.ConnectionClosed"/>.</para>
        ''' </exception>
        Private Sub ValidateResponse(ByVal response As WebResponse, ByVal url As Uri)
            If response.[GetType]() Is GetType(HttpWebResponse) Then
                Dim httpResponse As HttpWebResponse = TryCast(response, HttpWebResponse)

                ' If it's an HTML page, it's probably an error page. If we aren't
                ' allowing HTML pages to be downloaded, throw the error.
                If httpResponse.ContentType.Contains("text/html") OrElse httpResponse.StatusCode = HttpStatusCode.NotFound Then
                    If Not Me.DownloadHtml Then
                        'Throw New ArgumentException([String].Format(CultureInfo.CurrentUICulture, Properties.Resources.DownloadInfoInvalidResponseReceived, url))
                    End If
                End If
            ElseIf response.[GetType]() Is GetType(FtpWebResponse) Then
                Dim ftpResponse As FtpWebResponse = TryCast(response, FtpWebResponse)
                If ftpResponse.StatusCode = FtpStatusCode.ConnectionClosed Then
                    'Throw New ArgumentException([String].Format(CultureInfo.CurrentUICulture, Properties.Resources.DownloadInfoConnectionClosed, url))
                End If
            End If
            ' FileWebResponse doesn't have a status code to check.
        End Sub
#End Region

#End Region

#End Region

#Region "public properties and methods"

#Region "properties"

#Region "Credentials"
        ''' <summary>
        ''' Gets or sets the network credentials used for authenticating the the request with the Internet resource.
        ''' </summary>
        ''' <value>The <see cref="ICredentials"/> containing the authentication credentials associated with the request. 
        ''' The default value is a <see langword="null"/>.</value>
        ''' <remarks>The <see cref="Credentials"/> property contains the authentication credentials required to access
        ''' the Internet resource..
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

#Region "Disposed"
        ''' <summary>
        ''' Gets a value that indicates whether the provider is disposed.
        ''' </summary>
        ''' <value><b>true</b> if the object is disposed; otherwise, <b>false</b>. </value>
        Public ReadOnly Property Disposed() As Boolean
            Get
                SyncLock Me
                    Return m_disposed
                End SyncLock
            End Get
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

#Region "DownloadStream"
        ''' <summary>
        ''' Gets the underlying <see cref="System.IO.Stream"/> associated with the download.
        ''' </summary>
        ''' <value>A <see cref="System.IO.Stream"/> or a <see cref="System.IO.Stream.Null"/>.</value>
        Public ReadOnly Property DownloadStream() As Stream
            Get
                Me.CheckDisposed()
                If Me.start = Me.m_length Then
                    Return Stream.Null
                End If

                If Me.m_downloadStream Is Nothing Then
                    Me.m_downloadStream = Me.m_response.GetResponseStream()
                End If

                Return Me.m_downloadStream
            End Get
        End Property
#End Region

#Region "IsProgressKnown"
        ''' <summary>
        ''' Gets a value indicating if download progress information is able to be determined.
        ''' </summary>
        ''' <value>A <see cref="System.Boolean"/> value indicating if the progress information can be determined.</value>
        Public ReadOnly Property IsProgressKnown() As Boolean
            Get
                ' If the size of the remote url is -1, that means we
                ' couldn't determine it, and so we don't know
                ' progress information.
                Me.CheckDisposed()
                Return Me.m_length > -1
            End Get
        End Property
#End Region

#Region "Length"
        ''' <summary>
        ''' Gets the size of the current file.
        ''' </summary>
        ''' <value>The size of the current file.</value>
        ''' <remarks>This property value is a <see langword="null"/> if the file system containing the file does not support this information.</remarks>
        Public ReadOnly Property Length() As Long
            Get
                Me.CheckDisposed()
                Return Me.m_length
            End Get
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

#Region "Response"
        ''' <summary>
        ''' Gets the <see cref="System.Net.WebResponse"/> associated with the download.
        ''' </summary>
        ''' <value>The <see cref="System.Net.WebResponse"/> associated with the download.</value>
        Public ReadOnly Property Response() As WebResponse
            Get
                Me.CheckDisposed()
                Return m_response
            End Get
        End Property
#End Region

#Region "StartPoint"
        ''' <summary>
        ''' Gets the starting byte offset in the destination file.
        ''' </summary>
        ''' <value>A <see cref="Int64"/> value indicating the starting offset in the destination file.</value>
        Public ReadOnly Property StartPoint() As Long
            Get
                Me.CheckDisposed()
                Return Me.start
            End Get
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
        ''' Initializes a new instance of the <see cref="DownloadInfo"/> class.
        ''' </summary>
        Public Sub New()
            ' Set the default web proxy
            Me.m_proxy = WebRequest.DefaultWebProxy
        End Sub
#End Region

#Region "Close"
        ''' <summary>
        ''' Close the underlying <see cref="System.Net.WebResponse"/>.
        ''' </summary>
        Public Sub Close()
            Me.Dispose(True)

            ' Take ourself off the finalization queue to prevent
            ' finalization from executing a second time.
            GC.SuppressFinalize(Me)
        End Sub
#End Region

#Region "Dispose"
        ''' <summary>
        ''' Releases all resources used by the <see cref="DownloadInfo"/>.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Close()
        End Sub
#End Region

#Region "GetLastModifiedDate"
        ''' <summary>
        ''' Retrieves the last modified date of the file at the specified url.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file.</param>
        ''' <returns>The <see cref="DateTime"/> the file was last modified on
        ''' the server, in Universal Time. If the last modified date is not 
        ''' able to be determined, a <see langword="null"/> is returned.</returns>
        Public Function GetLastModifiedDate(ByVal url As Uri) As System.Nullable(Of DateTime)
            Me.CheckDisposed()

            Return GetServerLastModified(url)
        End Function
#End Region

#Region "Initialize"
        ''' <summary>
        ''' Initializes a new instance of a <see cref="DownloadInfo"/> class.
        ''' </summary>
        ''' <param name="url">The <see cref="Uri"/> of the file to be downloaded.</param>
        ''' <param name="destintationFolder">The destination folder for the download.</param>
        ''' <returns>A <see cref="DownloadInfo"/> that contains the information for the file to be downloaded.</returns>
        Public Sub Initialize(ByVal url As Uri, ByVal destintationFolder As String)
            Me.CheckDisposed()
            Dim urlSize As Long = GetFileSize(url)
            Me.m_length = urlSize

            Dim req As WebRequest = GetRequest(url)
            Me.m_response = DirectCast(req.GetResponse(), WebResponse)

            ' Check to make sure the response isn't an error. If it is this method
            ' will throw exceptions.
            ValidateResponse(Me.m_response, url)

            ' Take the name of the file given to use from the web server.
            Dim fileName As [String] = System.IO.Path.GetFileName(Me.m_response.ResponseUri.ToString())
            Dim downloadTo As [String] = Path.Combine(destintationFolder, fileName)

            ' If we don't know how big the file is supposed to be,
            ' we can't resume, so delete what we already have if something is on disk already.
            If Not Me.IsProgressKnown AndAlso File.Exists(downloadTo) Then
                File.Delete(downloadTo)
            End If

            If Me.IsProgressKnown AndAlso File.Exists(downloadTo) Then
                ' We only support resuming on http requests
                If Not (TypeOf Me.Response Is HttpWebResponse) Then
                    File.Delete(downloadTo)
                Else
                    ' Try and start where the file on disk left off
                    Me.start = New FileInfo(downloadTo).Length

                    ' If we have a file that's bigger than what is online, then something 
                    ' strange happened. Delete it and start again.
                    If Me.start > urlSize Then
                        File.Delete(downloadTo)
                        Me.start = 0
                    ElseIf Me.start = urlSize Then
                        ' The file already exists, so check to see if the version on the
                        ' server has a newer date. If we can't get the last modified date
                        ' from the server we will get a null value back. In that case,
                        ' we leave the existing file alone and don't download it.
                        Dim lastModifiedServer As System.Nullable(Of DateTime) = Me.GetServerLastModified(url)
                        If lastModifiedServer.HasValue Then
                            Dim lastModifiedLocal As DateTime = File.GetLastWriteTime(downloadTo).ToUniversalTime()
                            If lastModifiedServer > lastModifiedLocal Then
                                File.Delete(downloadTo)
                                Me.start = 0
                            End If
                        End If
                    ElseIf Me.start < urlSize Then
                        ' Try and resume by creating a new request with a new start position
                        Me.m_response.Close()
                        req = GetRequest(url)
                        DirectCast(req, HttpWebRequest).AddRange(CInt(Me.start))
                        Me.m_response = req.GetResponse()

                        If DirectCast(Me.Response, HttpWebResponse).StatusCode <> HttpStatusCode.PartialContent Then
                            ' They didn't support our resume request. 
                            File.Delete(downloadTo)
                            Me.start = 0
                        End If
                    End If
                End If
            End If
        End Sub
#End Region

#End Region

#End Region
    End Class
#End Region
End Namespace
