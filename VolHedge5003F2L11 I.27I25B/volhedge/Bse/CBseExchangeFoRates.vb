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

    Public mFoTokenFilter As ConcurrentDictionary(Of Long, String)
    Public mDictRates As ConcurrentDictionary(Of Long, Double)

    Public Sub InitFoRate()

        mFoTokenFilter = New ConcurrentDictionary(Of Long, String)
        mDictRates = New ConcurrentDictionary(Of Long, Double)

    End Sub
    Public Sub AddFoTokkenFilter(pToken As Long)
        mFoTokenFilter.TryAdd(pToken, "")
    End Sub

    Public Sub AddUpdateFoTokkenFilter(pToken As Long)
        Dim val As Double = 0
        'If Not mFoTokenFilter.ContainsKey(pToken) Then
        '    mFoTokenFilter(pToken) = val
        'End If

        If Not mHt_Fo.ContainsKey(pToken) Then
            mHt_Fo.Add(pToken, 0)
        End If

    End Sub


    Public Function TempCheckFoToken(pToken As Long) As Boolean
        Return mFoTokenFilter.ContainsKey(pToken)
    End Function

    Public Function GetRate(pToken As Long) As Double

        Dim dblRate As Double
        If mDictRates.TryGetValue(pToken, dblRate) Then
            Return dblRate
        Else
            Return 0
        End If

    End Function
    Private Sub StartFo1()
        Dim portFO As Int32 = CUtils.StringToInt(mIpPortFo)
        If portFO = 0 Then
            Return
        End If

        If mIpFo.Length < 1 Or mIpFo Is Nothing Then
            Return
        End If

        Dim ipReader As CBseUdpReader = New CBseUdpReader(mIpFo, portFO)
        ipReader.mLogFileName = "D:\BseFoBroadLog" & DateTime.Now.ToShortDateString() & ".txt"
        ipReader.ResetBuff()
        ipReader.mMpHeader.Reset()
        ipReader.mMpHeader = New CBseMarketPictureHeader()
        ipReader.mInstrument = New CBseMarketPictureInstrument()
        ipReader.Init()

        ipReader.mThread = New Thread(Sub() ListenLoopFo(ipReader))
        ipReader.mThread.IsBackground = True
        ipReader.mThread.Start()


    End Sub
    Dim mFoReader As CBseUdpReader
    Dim mFoThread As Thread
    Public Sub StartFo()
        Dim portFO As Integer = CUtils.StringToInt(mIpPortFo)
        If portFO = 0 Then Return
        If String.IsNullOrEmpty(mIpFo) Then Return

        ' Create new reader
        mFoReader = New CBseUdpReader(mIpFo, portFO)
        mFoReader.mLogFileName = "D:\BseFoBroadLog" & DateTime.Now.ToShortDateString() & ".txt"
        mFoReader.ResetBuff()
        mFoReader.mMpHeader.Reset()
        mFoReader.mMpHeader = New CBseMarketPictureHeader()
        mFoReader.mInstrument = New CBseMarketPictureInstrument()

        ' Initialize socket + join multicast
        mFoReader.Init()

        ' Start listening thread
        mFoThread = New Thread(Sub() ListenLoopFo(mFoReader))
        mFoThread.IsBackground = True
        mFoThread.Start()
    End Sub
    Public Sub StopFo()
        Try
            If mFoReader IsNot Nothing Then
                mFoReader.mIsRunning = False
            End If

            ' Close socket to break ReceiveFrom() immediately
            If mFoReader IsNot Nothing AndAlso mFoReader.mSocket IsNot Nothing Then
                Try : mFoReader.mSocket.Close() : Catch : End Try
            End If

            ' Wait for thread to exit
            If mFoThread IsNot Nothing AndAlso mFoThread.IsAlive Then
                mFoThread.Join(300)   ' small timeout
            End If

        Catch
            ' ignore errors
        End Try
    End Sub
    Public Function ListenLoopFo(pUdpReader As CBseUdpReader)
        pUdpReader.mIsRunning = True
        'Await Task.Run(Sub()
        Dim remoteEP As EndPoint = New IPEndPoint(IPAddress.Any, 0)
        While pUdpReader.mIsRunning
            Try
                Dim received As Integer = pUdpReader.mSocket.ReceiveFrom(pUdpReader.mPacketBuff, remoteEP)
                '  Console.WriteLine($"[Async] Received {received} bytes from {remoteEP}")
                FoProcess(pUdpReader)
                ' TODO: process buffer(0..received-1)
            Catch ex As SocketException
                Console.WriteLine("Socket error: " & ex.Message)
                Exit While
            End Try
        End While
        '               End Sub)
    End Function
    Public Async Sub FoProcess(UdpReader As CBseUdpReader)

        'Dim UdpReader As CBseUdpReader = mUdpFoBroardCast

        Try

            If UdpReader.mPacketBuff Is Nothing Then
                UdpReader.MarkPacketBuffReset()
                Return
            End If

            If Not UdpReader.mMpHeader.ValidatePacket(UdpReader.mPacketBuff) Then
                UdpReader.MarkPacketBuffReset()
                Return
            End If

            UdpReader.mPacketCount += 1


            If UdpReader.mMpHeader.mMessageType = 2011 OrElse UdpReader.mMpHeader.mMessageType = 2012 Then
                ProcessIndexRates(UdpReader.mIndexHeader, UdpReader.mIndexInstrument, UdpReader.mPacketBuff)
            Else
                ProcessMarketPicture(UdpReader)
            End If



            'Debug.WriteLine(UdpReader.mMpHeader.mMessageType.ToString() & " : " & UdpReader.mPacketBuff.Length & " : " & UdpReader.mPacketCount)


            ''Return
            'UdpReader.mMpHeader.Reset()

            'Using mMemStream = New MemoryStream(UdpReader.mPacketBuff)
            '    Using mBinReader = New BigEndianBinaryReader(mMemStream)
            '        UdpReader.mMpHeader.Read(mBinReader)
            '        If UdpReader.mMpHeader.mNoOfRecords < 1 Then
            '            UdpReader.MarkPacketBuffReset()
            '            Return
            '        End If
            '        UdpReader.mPacketCount += 1
            '        For i As Integer = 0 To UdpReader.mMpHeader.mNoOfRecords - 1
            '            UdpReader.mInstrument.Reset()
            '            'If UdpReader.mMpHeader.mMessageType = 2020 Then
            '            '    UdpReader.mInstrumentCode = mBinReader.ReadInt32()
            '            'Else
            '            '    UdpReader.mInstrumentCode = mBinReader.ReadInt64()
            '            'End If
            '            UdpReader.mInstrumentCode = mBinReader.ReadInt32()
            '            Dim lnInst As Long = UdpReader.mInstrumentCode
            '            Dim inst As CBseMarketPictureInstrument = UdpReader.mInstrument
            '            If Not CheckToken(lnInst) Then
            '                Return
            '            End If

            '            inst.Read(mBinReader)
            '            If inst.mNoOfPricePoints < 0 AndAlso inst.mNoOfPricePoints > 5 Then
            '                UdpReader.MarkPacketBuffReset()
            '                Return
            '            End If

            '            Dim baseLtp As Int32 = inst.mLTP
            '            Dim baseQty As Int32 = inst.mLastTradeQty

            '            ' Read bids
            '            For j As Integer = 0 To inst.mNoOfPricePoints - 1
            '                'Dim binOfferRec As New CBseMarketPictureBidOffer()
            '                'inst.mListBidOffer.Add(binOfferRec)
            '                If Not inst.mListBidOffer(j).ReadBids(mBinReader, baseLtp, baseQty) Then
            '                    Exit For
            '                End If
            '                baseLtp = inst.mListBidOffer(j).mBestBidRate
            '                baseQty = inst.mListBidOffer(j).mTotalBidQty
            '            Next

            '            baseLtp = inst.mLTP
            '            baseQty = inst.mLastTradeQty

            '            ' Read offers
            '            For j As Integer = 0 To inst.mNoOfPricePoints - 1
            '                If Not inst.mListBidOffer(j).ReadOffers(mBinReader, baseLtp, baseQty) Then
            '                    Exit For
            '                End If
            '                baseLtp = inst.mListBidOffer(j).mBestOfferRate
            '                baseQty = inst.mListBidOffer(j).mTotalOfferQty
            '            Next

            '            If inst.mNoOfPricePoints > 0 Then
            '                'Dim data1 As String =
            '                '    inst.mInstrumentCode.ToString() & ": Ltp :" & inst.mLTP.ToString() &
            '                '    ": Buy :" & inst.mListBidOffer(0).mBestBidRate.ToString() &
            '                '    ": Offer :" & inst.mListBidOffer(0).mBestOfferRate.ToString()
            '                'Console.WriteLine(data1)

            '                Dim dt As New DateTime(
            '                        DateTime.Now.Year,
            '                        DateTime.Now.Month,
            '                        DateTime.Now.Day,
            '                       UdpReader.mMpHeader.mHour,
            '                     UdpReader.mMpHeader.mMinute,
            '                    UdpReader.mMpHeader.mSecond)

            '                Dim dblLtp As Double = inst.mLTP / 100.0

            '                mDictRates.TryAdd(lnInst, dblLtp)

            '                BseSetFilterRates(lnInst,
            '                                   inst.mLTP,
            '                                   inst.mListBidOffer(0).mBestBidRate,
            '                                   inst.mListBidOffer(0).mBestOfferRate,
            '                                   inst.mOpenRate,
            '                                   inst.mCloseRate,
            '                                   inst.mHighRate,
            '                                   inst.mLowRate, dt)
            '            End If
            '        Next
            '    End Using
            'End Using
        Catch ex As Exception
        End Try
    End Sub



End Class
