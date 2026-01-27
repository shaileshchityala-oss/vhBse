Imports System.Data
Imports System.IO
Imports Sylvan.Data.Csv
Imports System.Globalization
Imports System.Threading.Tasks
Imports VolHedge.DAL
Imports System.Collections
Imports System.Collections.Concurrent
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading

Partial Public Class CBseExchange

    Public mEqTokenFilter As ConcurrentDictionary(Of Long, String)

    Public Sub InitEqRate()

        mEqTokenFilter = New ConcurrentDictionary(Of Long, String)

    End Sub
    Public Sub AddEqTokkenFilter(pToken As Long)
        mEqTokenFilter.TryAdd(pToken, "")
    End Sub


    Public Sub AddUpdateEqTokkenFilter(pToken As Long)
        Dim val As Double = 0
        'If Not mEqTokenFilter.ContainsKey(pToken) Then
        '    mEqTokenFilter(pToken) = val
        'End If

        If Not mHt_Eq.ContainsKey(pToken) Then
            mHt_Eq.Add(pToken, 0)
        End If

    End Sub


    Public Function TempCheckeEqToken(pToken As Long) As Boolean
        Return mEqTokenFilter.ContainsKey(pToken)
    End Function

    Private Sub StartEq1()
        Dim portEq As Int32 = CUtils.StringToInt(mIpPortEq)

        If portEq = 0 Then
            Return
        End If

        If mIpEq.Length < 1 Or mIpEq Is Nothing Then
            Return
        End If

        Dim ipReader As CBseUdpReader = New CBseUdpReader(mIpEq, portEq)
        ipReader.mLogFileName = "D:\BseEqBroadLog" & DateTime.Now.ToShortDateString() & ".txt"
        ipReader.ResetBuff()
        ipReader.mMpHeader.Reset()
        ipReader.mMpHeader = New CBseMarketPictureHeader()
        ipReader.mInstrument = New CBseMarketPictureInstrument()

        ipReader.Init()
        ipReader.mThread = New Thread(Sub() ListenLoopEq(ipReader))
        ipReader.mThread.IsBackground = True
        ipReader.mThread.Start()

    End Sub

    Dim mEqReader As CBseUdpReader
    Dim mEqThread As Thread
    Public Sub StartEq()
        Dim portEq As Int32 = CUtils.StringToInt(mIpPortEq)

        If portEq = 0 Then
            Return
        End If

        If mIpEq.Length < 1 Or mIpEq Is Nothing Then
            Return
        End If

        mEqReader = New CBseUdpReader(mIpEq, portEq)
        mEqReader.mLogFileName = "D:\BseEqBroadLog" & DateTime.Now.ToShortDateString() & ".txt"
        mEqReader.ResetBuff()
        mEqReader.mMpHeader.Reset()
        mEqReader.mMpHeader = New CBseMarketPictureHeader()
        mEqReader.mInstrument = New CBseMarketPictureInstrument()

        mEqReader.Init()
        mEqThread = New Thread(Sub() ListenLoopEq(mEqReader))
        mEqThread.IsBackground = True
        mEqThread.Start()

    End Sub
    Public Sub StopEq()
        Try
            If mEqReader IsNot Nothing Then
                mEqReader.mIsRunning = False
            End If

            ' Close socket to break ReceiveFrom() immediately
            If mEqReader IsNot Nothing AndAlso mEqReader.mSocket IsNot Nothing Then
                Try : mEqReader.mSocket.Close() : Catch : End Try
            End If

            ' Wait for thread to exit
            If mEqThread IsNot Nothing AndAlso mEqThread.IsAlive Then
                mEqThread.Join(300)   ' small timeout
            End If

        Catch
            ' ignore errors
        End Try
    End Sub
    Public Function ListenLoopEq(pUdpReader As CBseUdpReader)
        pUdpReader.mIsRunning = True
        'Await Task.Run(Sub()
        Dim remoteEP As EndPoint = New IPEndPoint(IPAddress.Any, 0)
        While pUdpReader.mIsRunning
            Try
                Dim received As Integer = pUdpReader.mSocket.ReceiveFrom(pUdpReader.mPacketBuff, remoteEP)
                ' Console.WriteLine($"[Async] Received {received} bytes from {remoteEP}")
                EqProcess(pUdpReader)
                ' TODO: process buffer(0..received-1)
            Catch ex As SocketException
                Console.WriteLine("Socket error: " & ex.Message)
                Exit While
            End Try
        End While
        '               End Sub)
    End Function
    Public Async Sub EqProcess(UdpReader As CBseUdpReader)
        Try
            If UdpReader.mPacketBuff Is Nothing Then
                UdpReader.MarkPacketBuffReset()
                Return
            End If

            If Not UdpReader.mMpHeader.ValidatePacket(UdpReader.mPacketBuff) Then
                UdpReader.MarkPacketBuffReset()
                Return
            End If

            If UdpReader.mMpHeader.mMessageType = 2011 OrElse UdpReader.mMpHeader.mMessageType = 2012 Then
                ProcessIndexRates(UdpReader.mIndexHeader, UdpReader.mIndexInstrument, UdpReader.mPacketBuff)
            End If

            If UdpReader.mMpHeader.mMessageType = 2020 OrElse UdpReader.mMpHeader.mMessageType = 2021 Then
                ProcessMarketPicture(UdpReader)
            End If

        Catch ex As Exception
        End Try
    End Sub


End Class
