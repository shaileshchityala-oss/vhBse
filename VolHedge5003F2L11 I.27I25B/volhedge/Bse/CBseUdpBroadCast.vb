Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading.Tasks
Imports System.Threading

Public Class CBseUdpReader

    Private mMulticastIP As String
    Private mMulticastPort As Integer
    Public mSocket As Socket
    Private mEndPoint As EndPoint
    Private mIpEndPoint As IPEndPoint
    Public mIsRunning As Boolean
    Public mThread As Thread

    Public mIndexHeader As CBseIndexMsgHeader
    Public mIndexInstrument As CIndexMsgInstrument

    Public Sub New(pMulticastIP As String, pMulticastPort As Integer)

        mMulticastIP = pMulticastIP
        mMulticastPort = pMulticastPort

        mPacketCount = 0
        mMpHeader = New CBseMarketPictureHeader()
        mPacketBuff = New Byte(2000) {}
        mIndexHeader = New CBseIndexMsgHeader()
        mIndexInstrument = New CIndexMsgInstrument()
        mInstrument = New CBseMarketPictureInstrument()

    End Sub


    Public mMpHeader As CBseMarketPictureHeader
    Public mFilterInstruments As Dictionary(Of Int32, CBseRates)
    Public mLogFileName As String
    Public mPacketCount As Int64
    Public mInstrumentCode As Long
    Public mInstrument As CBseMarketPictureInstrument

    Private mMemStream As MemoryStream
    Private mBinReader As BigEndianBinaryReader
    Public mPacketBuff As Byte()
    Private mMarkPacketBuffReset As Boolean

    Public Sub MarkPacketBuffReset()
        mMarkPacketBuffReset = True
    End Sub

    Public Sub ResetBuff()
        If mMarkPacketBuffReset Then
            Array.Clear(mPacketBuff, 0, mPacketBuff.Length)
            mMarkPacketBuffReset = False
        End If
    End Sub

    Public Sub Init()
        ' Create UDP socket
        mSocket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)

        ' Allow multiple apps to listen on same port
        mSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, True)

        ' Bind to local port
        Dim localEP As New IPEndPoint(IPAddress.Any, mMulticastPort)
        mSocket.Bind(localEP)

        ' Join multicast group
        Dim mcastAddr As IPAddress = IPAddress.Parse(mMulticastIP)
        Dim ipBytes As Byte() = mcastAddr.GetAddressBytes()
        If ipBytes(0) >= 224 AndAlso ipBytes(0) <= 239 Then
            Dim mcOption As New MulticastOption(mcastAddr, IPAddress.Any)
            mSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, mcOption)
            mSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback, False)
        End If
    End Sub
    Public Sub StopListening()
        mIsRunning = False
        If mSocket IsNot Nothing Then
            mSocket.Close()
        End If
    End Sub

End Class


