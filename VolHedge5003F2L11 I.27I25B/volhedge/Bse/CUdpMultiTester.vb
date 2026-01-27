Imports System.Net
Imports System.Net.Sockets

Public Class UdpMulticastTester
    Private Const RECEIVE_BUFFER_SIZE As Integer = 512

    Public Function TestConnection(ip As String, port As Integer, Optional timeoutMs As Integer = 1000) As Boolean
        Dim sock As Socket = Nothing

        Try
            Dim mcastIP = IPAddress.Parse(ip)

            sock = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, True)

            ' Try to bind to the port
            Try
                sock.Bind(New IPEndPoint(IPAddress.Any, port))
            Catch ex As SocketException When ex.SocketErrorCode = SocketError.AddressAlreadyInUse
                ' Another process or thread is already listening
                Return False
            End Try

            ' Join multicast group
            sock.SetSocketOption(SocketOptionLevel.IP,
                                 SocketOptionName.AddMembership,
                                 New MulticastOption(mcastIP, IPAddress.Any))

            ' Receive timeout
            sock.ReceiveTimeout = timeoutMs

            Dim buffer(1024 - 1) As Byte
            Dim received As Integer

            Try
                received = sock.Receive(buffer)
            Catch ex As SocketException When ex.SocketErrorCode = SocketError.TimedOut
                ' Receive timeout reached
                Return False
            End Try

            ' Check if data was received
            Return received > 0

        Catch
            Return False

        Finally
            If sock IsNot Nothing Then
                Try : sock.Close() : Catch : End Try
            End If
        End Try

    End Function


End Class
