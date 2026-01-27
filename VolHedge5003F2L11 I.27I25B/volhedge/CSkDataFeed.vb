Imports Microsoft.Win32
Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports VolHedge.DAL
Imports System.Data
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.IO
Imports System.Globalization
Imports System.Net.WebSockets
Imports System
Imports WebSocketSharp
Imports System.Security.Authentication
Imports System.Net.Security
Imports Newtonsoft.Json

Public Class ACKRequest
    Public mAction As String
    Public mKey() As String
    Public mValue() As String
End Class

Public Class SubscribeRequest
    Public Property action As String
    Public Property key As String()
    Public Property value As String()
End Class



Public Class CSkDataFeed
    Dim mApiKey As String
    Dim mSecureKey As String
    Dim mCustomerID As String
    Dim mToken As String

    Dim mSocket As System.Net.WebSockets.ClientWebSocket

    Public Function SubScribe(ws As WebSocketSharp.WebSocket) As Integer

        Dim aCKRequest As New SubscribeRequest With {
           .action = "subscribe",
            .key = New String() {"ack", "feed"},
            .value = New String() {""}
        }

        ws.Send(JsonConvert.SerializeObject(aCKRequest))

    End Function
    Public Function Authenticate() As String

        Return ""
    End Function
    Public Function RegisterTokken()

    End Function

    Public Function ReadFeed()

    End Function

    'Public Function Connect() As Boolean
    '    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls12

    '    Dim url As String = "wss://stream.sharekhan.com/skstream/api/stream?ACCESS_TOKEN=" & token & "&API_KEY=" & apiKey
    '    Dim apiUri As New Uri(url)


    '    Dim ws As WebSocket
    '    ws = mSocket
    '    ' Configure SSL

    '    ws.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12 Or SslProtocols.Tls
    '    ws.SslConfiguration.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(AddressOf ValidateCert)

    '    ' WebSocket Open Event
    '    AddHandler ws.OnOpen, AddressOf OnWSOpen
    '    AddHandler ws.OnMessage, AddressOf OnWSMessage


    '    'ws.Connect()
    'End Function





End Class
