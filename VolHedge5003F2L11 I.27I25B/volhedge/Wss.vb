'Imports System.Net
'Imports System.Net.WebSockets
'Imports System.Text
'Imports System.Threading
'Imports System.Threading.Tasks
'Imports System.Net.Security
'Imports System.Security.Authentication
'Imports WebSocketSharp
'Imports System.Security.Cryptography.X509Certificates


'Public Class WebSocketListener
'    Private WithEvents ws As WebSocketSharp.WebSocket

'    Public Event Connected()
'    Public Event MessageReceived(message As String)
'    Public Event Disconnected(reason As String)
'    Public Event ErrorOccurred(errorMessage As String)

'    Public Function ValidateServerCertificate(sender As Object, certificate As Object, chain As Object, sslPolicyErrors As SslPolicyErrors) As Boolean
'        Return True
'    End Function

'    Public Sub Connect(url As String, Optional bypassCertValidation As Boolean = False)

'        'Login: 3118480 | Password : Surat@1(terminal pass)
'        'API Key:  9M2sDYxbsVocVyT8mrH2ivPW0jAfuGwd
'        'API Secure key: AUCAnKvSrH7FWtRQBfRYXoYl7nMaeEf5

'        Dim token As String = ""
'        Dim apiKey As String = ""

'        url = "wss://stream.sharekhan.com/skstream/api/stream?ACCESS_TOKEN=" & token & "&API_KEY=" & apiKey

'        Try
'            ws = New WebSocketSharp.WebSocket(url)

'            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls
'            ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateServerCertificate
'            '' Configure SSL settings for WebSocket (.NET 4.6 compatible)
'            ws.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12 Or SslProtocols.Tls
'            ws.SslConfiguration.CheckCertificateRevocation = False
'            ws.SslConfiguration.ClientCertificateSelectionCallback = Nothing
'            ws.SslConfiguration.ServerCertificateValidationCallback = AddressOf ValidateServerCertificate


'            ' Attach event handlers
'            AddHandler ws.OnOpen, Sub(sender, e)
'                                      RaiseEvent Connected()
'                                  End Sub

'            AddHandler ws.OnMessage, Sub(sender, e)
'                                         RaiseEvent MessageReceived(e.Data)
'                                     End Sub

'            AddHandler ws.OnClose, Sub(sender, e)
'                                       RaiseEvent Disconnected(e.Reason)
'                                   End Sub

'            AddHandler ws.OnError, Sub(sender, e)
'                                       RaiseEvent ErrorOccurred(e.Message)
'                                   End Sub

'            ws.Connect()
'        Catch ex As Exception
'        End Try
'    End Sub


'    'Private Sub ws_OnOpen(sender As Object, e As MessageEventArgs) Handles ws.OnOpen

'    'End Sub
'    'Private Sub ws_OnMessage(sender As Object, e As MessageEventArgs) Handles ws.OnMessage

'    'End Sub

'    'Private Sub ws_OnClose(sender As Object, e As CloseEventArgs) Handles ws.OnClose
'    'End Sub

'    'Private Sub ws_OnError(sender As Object, e As ErrorEventArgs) Handles ws.OnError
'    'End Sub
'End Class

'Public Class test
'    Private WithEvents listener As New WebSocketListener()
'End Class