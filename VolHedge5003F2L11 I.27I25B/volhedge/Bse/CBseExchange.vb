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

    Dim mHt_Fo As Hashtable
    Dim mHt_Eq As Hashtable
    Dim mHt_Buy As Hashtable
    Dim mHt_Sell As Hashtable

    Public mDtBhavCopy As DataTable

    Public mDtFOContractRaw As DataTable
    Public mDtFOContract As DataTable
    Public mDtFoSymbols As DataTable
    Public mDtFoInstrument As DataTable

    Public mDtEqContractRaw As DataTable
    Public mDtEqContract As DataTable

    Public mDtCurContract As DataTable

    Public mRatesQue As ConcurrentQueue(Of CBseRates)
    Public mContractFo As CBseContractFo
    Public mContractEq As CBseContractEq
    Public mBhavCopy As CBseBhavCopy

    Sub New()
        mContractFo = New CBseContractFo()
        mContractEq = New CBseContractEq()
        mBhavCopy = New CBseBhavCopy()
        mRatesQue = New ConcurrentQueue(Of CBseRates)

        InitEqRate()
        InitFoRate()
        InitIndexRate()

    End Sub

#Region "BroadCast"

    Dim mUdpFoBroardCast As CBseUdpReader
    Dim mUdpEqBroardCast As CBseUdpReader

    Public mIpFo As String
    Public mIpEq As String

    Public mIpPortFo As String
    Public mIpPortEq As String

    Public Sub CreateBroadCast(pHtFo As Hashtable, pHtEq As Hashtable, pHtBuy As Hashtable, pHtSell As Hashtable)

        'Dim portEQ As Int32 = CUtils.StringToInt(mIpPortEq)
        'mUdpEqBroardCast = New CBseUdpReader(mIpEq, portEQ)
        ''mUdpEqBroardCast = New CBseUdpReader("233.1.2.3", 1003)
        ''mUdpFoBroardCast = New CBseUdpReader("233.1.2.4", 1004)

        mHt_Fo = pHtFo
        mHt_Eq = pHtEq

        mHt_Sell = pHtSell
        mHt_Buy = pHtBuy

        StartFo()
        StartEq()

        'Task.Run(Sub() FoThread())
        'Task.Run(Sub() EqThread())
        'Task.Run(Sub() ThreadApplyRate())

    End Sub
    Private Sub ProcessMarketPicture(UdpReader As CBseUdpReader)
        UdpReader.mMpHeader.Reset()
        Using mMemStream = New MemoryStream(UdpReader.mPacketBuff)
            Using mBinReader = New BigEndianBinaryReader(mMemStream)
                UdpReader.mMpHeader.Read(mBinReader)
                If UdpReader.mMpHeader.mNoOfRecords < 1 Then
                    UdpReader.MarkPacketBuffReset()
                    Return
                End If
                UdpReader.mPacketCount += 1
                For i As Integer = 0 To UdpReader.mMpHeader.mNoOfRecords - 1
                    UdpReader.mInstrument.Reset()
                    UdpReader.mInstrumentCode = mBinReader.ReadInt32()
                    Dim lnInst As Long = UdpReader.mInstrumentCode
                    Dim inst As CBseMarketPictureInstrument = UdpReader.mInstrument
                    inst.mInstrumentCode = UdpReader.mInstrumentCode
                    If Not CheckToken(lnInst) Then
                        Return
                    End If
                    Dim filepath = "D:\BseTest\Dat-" + lnInst.ToString() + Now.Hour.ToString() + "-" + Now.Minute.ToString() + "-" + Now.Second.ToString()
                    'File.WriteAllBytes(filepath, UdpReader.mPacketBuff)
                    inst.Read(mBinReader)
                    If inst.mNoOfPricePoints < 1 Or inst.mNoOfPricePoints > 5 Then
                        UdpReader.MarkPacketBuffReset()
                        Return
                    End If

                    Dim baseLtp As Int32 = inst.mLTP
                    Dim baseQty As Int32 = inst.mLastTradeQty

                    ' Read bids
                    For j As Integer = 0 To inst.mNoOfPricePoints - 1
                        'Dim binOfferRec As New CBseMarketPictureBidOffer()
                        'inst.mListBidOffer.Add(binOfferRec)

                        If Not inst.mListBidOffer(j).ReadBids(mBinReader, baseLtp, baseQty) Then
                            Exit For
                        End If
                        baseLtp = inst.mListBidOffer(j).mBestBidRate
                        baseQty = inst.mListBidOffer(j).mTotalBidQty
                    Next

                    baseLtp = inst.mLTP
                    baseQty = inst.mLastTradeQty

                    ' Read offers
                    For j As Integer = 0 To inst.mNoOfPricePoints - 1
                        If Not inst.mListBidOffer(j).ReadOffers(mBinReader, baseLtp, baseQty) Then
                            Exit For
                        End If
                        baseLtp = inst.mListBidOffer(j).mBestOfferRate
                        baseQty = inst.mListBidOffer(j).mTotalOfferQty
                    Next

                    If inst.mNoOfPricePoints > 0 Then
                        'Dim data1 As String =
                        '    inst.mInstrumentCode.ToString() & ": Ltp :" & inst.mLTP.ToString() &
                        '    ": Buy :" & inst.mListBidOffer(0).mBestBidRate.ToString() &
                        '    ": Offer :" & inst.mListBidOffer(0).mBestOfferRate.ToString()
                        'Console.WriteLine(data1)

                        Dim dt As New DateTime(
                                DateTime.Now.Year,
                                DateTime.Now.Month,
                                DateTime.Now.Day,
                               UdpReader.mMpHeader.mHour,
                             UdpReader.mMpHeader.mMinute,
                            UdpReader.mMpHeader.mSecond)

                        Dim dblLtp As Double = inst.mLTP / 100
                        AddUpdateRates(lnInst, dblLtp)

                        BseSetFilterRates(lnInst,
                                           inst.mLTP,
                                           inst.mListBidOffer(0).mBestBidRate,
                                           inst.mListBidOffer(0).mBestOfferRate,
                                           inst.mOpenRate,
                                           inst.mCloseRate,
                                           inst.mHighRate,
                                           inst.mLowRate, dt)
                    End If
                Next
            End Using
        End Using
    End Sub

    Private Sub AddUpdateRates(key As Long, value As Double)
        mDictRates.AddOrUpdate(
        key,
        value,
        Function(k, oldValue) value
    )
    End Sub
    Public Function CheckToken(pToken As Long) As Boolean
        If mHt_Fo.ContainsKey(pToken) Then
            Return True
        End If

        If mHt_Eq.ContainsKey(pToken) Then
            Return True
        End If

        Return False

    End Function
    Public Sub BseSetFilterRates(pInstru As Long,
                               pLtp As Int32,
                               pBuy As Int32,
                               pSell As Int32,
                               pOpening As Int32,
                               pClosing As Int32,
                               pHigh As Int32,
                               pLow As Int32,
                               pDateTime As DateTime)


        Dim dblLtp As Double = pLtp / 100.0
        Dim dblBuy As Double = pBuy / 100.0
        Dim dblSell As Double = pSell / 100.0
        Dim dblOpening As Double = pOpening / 100.0
        Dim dblClosing As Double = pClosing / 100.0
        Dim dblHigh As Double = pHigh / 100.0
        Dim dblLow As Double = pLow / 100.0

        'AddScriptRate(pInstru, dblLtp, dblBuy, dblSell, dblClosing)

        'If dblLtp > 0 Then ltpprice(pInstru) = dblLtp
        'If dblBuy > 0 Then mHt_Buy(pInstru) = dblBuy
        'If dblSell > 0 Then mHt_Sell(pInstru) = dblSell

        'If dblLtp > 0 Then eltpprice(pInstru) = dblLtp
        'If dblBuy > 0 Then ebuyprice(pInstru) = dblBuy
        'If dblSell > 0 Then esaleprice(pInstru) = dblSell

        'If dblLtp > 0 Then MKTltpprice(pInstru) = dblLtp
        'If dblLtp > 0 Then fltpprice(pInstru) = dblLtp
        'If dblLtp > 0 Then Currfltpprice(pInstru) = dblLtp

        Dim rates As CBseRates = New CBseRates()
        rates.mInstrument = pInstru
        If dblLtp > 0 Then rates.mLtp = dblLtp
        If dblBuy > 0 Then rates.mBuy = dblBuy
        If dblSell > 0 Then rates.mSell = dblSell
        If dblOpening > 0 Then rates.mOpen = dblOpening
        If dblClosing > 0 Then rates.mClose = dblClosing
        If dblHigh > 0 Then rates.mHigh = dblHigh
        If dblLow > 0 Then rates.mLow = dblLow
        AddScriptRate(rates.mInstrument, rates.mLtp, rates.mBuy, rates.mSell, rates.mClose)
        ' Debug.WriteLine($"[{Now:HH:mm:ss}] {rates.mInstrument}: LTP={rates.mLtp}, Buy={rates.mBuy}, Sell={rates.mSell}, Close={rates.mClose}")
        'mRatesQue.Enqueue(rates)

    End Sub
    Public Sub AddToHashIdx(pHast As Hashtable, pToken As String, pVal As String)
        If pHast.Contains(pToken) Then
            pHast.Item(pToken) = pVal
        Else
            pHast.Add(pToken, pVal)
        End If
    End Sub
    Public Sub AddToHash(pHast As Hashtable, pToken As Long, pVal As String)
        If pHast.Contains(pToken) Then
            pHast.Item(pToken) = pVal
        Else
            pHast.Add(pToken, pVal)
        End If
    End Sub


    Public Sub AddIndexRate(pToken As Long, pLtp As Double, pBid As Double, pOffer As Double, pClose As Double)
        If FlgBcastStop = True Then
            Return
        End If

        Dim dblLtp As Double = pLtp
        Dim lnScript As Long = pToken
        Dim script As String = ""

        If pToken = 1 Then
            script = "SENSEX"
        ElseIf pToken = 12 Then
            script = "BANKEX"
        End If

        If dblLtp > 0 And script.Length > 0 Then
            eIdxprice(script) = dblLtp
        End If

    End Sub

    Public Sub AddScriptRate(pToken As Long, pLtp As Double, pBid As Double, pOffer As Double, pClose As Double)
        If FlgBcastStop = True Then
            Return
        End If

        Dim dblLtp As Double = pLtp
        Dim dblBid As Double = pBid
        Dim dblOff As Double = pOffer
        Dim dblClose As Double = pClose

        Dim lnScript As Long = pToken
        'If mSkConn.mTokenData.mDictSkIndex.ContainsKey(lnScript) Then
        '    If dblLtp > 0 Then
        '        eIdxprice(mSkConn.mTokenData.mDictVhIndex(lnScript)) = dblLtp
        '    End If
        'End If

        If dblLtp > 0 Then

            AddToHash(ltpprice, lnScript, dblLtp)
            AddToHash(eltpprice, lnScript, dblLtp)
            AddToHash(fltpprice, lnScript, dblLtp)
            AddToHash(Currfltpprice, lnScript, dblLtp)
            AddToHash(currMKTltpprice, lnScript, dblLtp)
            AddToHash(MKTltpprice, lnScript, dblLtp)
        End If

        If dblBid > 0 Then
            AddToHash(ebuyprice, lnScript, dblBid)
            AddToHash(fltpprice, lnScript, dblBid)
            AddToHash(fbuyprice, lnScript, dblBid)
        End If

        If dblOff > 0 Then
            AddToHash(esaleprice, lnScript, dblOff)
            AddToHash(fsaleprice, lnScript, dblOff)
        End If

        If dblClose > 0 Then AddToHash(closeprice, lnScript, dblClose)

        Return

        If eltpprice.Contains(pToken) Then
            If FlgBcastStop = False Then
                If dblBid > 0 Then ebuyprice.Item(lnScript) = pBid
                If dblOff > 0 Then esaleprice.Item(lnScript) = pOffer
                If dblLtp > 0 Then eltpprice.Item(lnScript) = pLtp
            End If
        Else
            If dblBid > 0 Then ebuyprice.Add(lnScript, pBid)
            If dblOff > 0 Then esaleprice.Add(lnScript, pOffer)
            If dblLtp > 0 Then eltpprice.Add(lnScript, pLtp)
        End If

        'Dim fltppr As Double
        If fltpprice.Contains(pToken) Then
            If FlgBcastStop = False Then
                If dblBid > 0 Then fbuyprice.Item(lnScript) = pBid
                If dblOff > 0 Then fsaleprice.Item(lnScript) = pOffer
                If dblLtp > 0 Then fltpprice.Item(lnScript) = pLtp
            End If
        Else
            If dblBid > 0 Then fbuyprice.Add(lnScript, pBid)
            If dblOff > 0 Then fsaleprice.Add(lnScript, pOffer)
            If dblLtp > 0 Then fltpprice.Add(lnScript, pLtp)
        End If

        If closeprice.Contains(pToken) Then
            If FlgBcastStop = False Then
                If dblClose > 0 Then closeprice.Item(lnScript) = pClose
            End If
        Else
            If dblClose > 0 Then closeprice.Add(lnScript, pClose)
        End If


        If Currfltpprice.Contains(lnScript) Then
            Currfbuyprice.Item(lnScript) = dblBid
            Currfsaleprice.Item(lnScript) = dblOff
            Currfltpprice.Item(lnScript) = dblLtp
            currMKTltpprice.Item(lnScript) = dblClose
        Else
            Currfbuyprice.Add(lnScript, dblBid)
            Currfsaleprice.Add(lnScript, dblOff)
            Currfltpprice.Add(lnScript, dblLtp)
            currMKTltpprice.Add(lnScript, dblLtp)
        End If

    End Sub

#End Region

End Class