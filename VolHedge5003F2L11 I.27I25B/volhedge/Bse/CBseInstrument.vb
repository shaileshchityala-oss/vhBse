Imports System
Imports System.Collections.Generic
Imports System.IO
Public Class CBseRates
    Public mInstrument As Int32
    Public mLtp As Double
    Public mBuy As Double
    Public mSell As Double

    Public mOpen As Double
    Public mClose As Double

    Public mHigh As Double
    Public mLow As Double

    Public mRxDateTime As DateTime

    Public Sub New()
        mInstrument = 0
        mLtp = 0
        mBuy = 0
        mSell = 0

        mOpen = 0
        mClose = 0

        mHigh = 0
        mLow = 0

        mRxDateTime = DateTime.MinValue
    End Sub
End Class

Public Class CBseMarketPictureInstrument
    Public mInstrumentCode As Int32
    Public mNoOfTrades As UInt32
    Public mVolume As Long
    Public mValue As Long
    Public mTradeValueFlag As Char
    Public mTrend As Char
    Public mSixLakhFlag As Char
    Public mReservedField1 As Char
    Public mMarketType As Short
    Public mSessionNumber As Short
    Public mLTPHour As Char
    Public mLTPMinute As Char
    Public mLTPSecond As Char
    Public mLTPMillisecond() As Char
    Public mReservedField2() As Char
    Public mReservedField10 As Short
    Public mReservedField11 As Long

    Public mNoOfPricePoints As Short
    Public mTimestamp As Long
    Public mCloseRate As Int32
    Public mLastTradeQty As Long
    Public mLTP As Int32

    Public mOpenRate As Int32
    Public mPreviousCloseRate As Int32
    Public mHighRate As Int32
    Public mLowRate As Int32
    Public mReservedField5 As Int32
    Public mIndicativeEquilibriumPrice As Int32
    Public mIndicativeEquilibriumQty As Long
    Public mTotalBidQty As Long
    Public mTotalOfferQty As Long
    Public mLowerCircuitLimit As Int32
    Public mUpperCircuitLimit As Int32
    Public mWeightedAverage As Int32

    Public mListBidOffer As List(Of CBseMarketPictureBidOffer)

    ' Constructor
    Public Sub New()
        mListBidOffer = New List(Of CBseMarketPictureBidOffer)()
        mListBidOffer.Add(New CBseMarketPictureBidOffer())
        mListBidOffer.Add(New CBseMarketPictureBidOffer())
        mListBidOffer.Add(New CBseMarketPictureBidOffer())
        mListBidOffer.Add(New CBseMarketPictureBidOffer())
        mListBidOffer.Add(New CBseMarketPictureBidOffer())

        mLTPMillisecond = New Char(2) {}
        mReservedField2 = New Char(1) {}
        Reset()
    End Sub

    Public Sub Reset()
        mInstrumentCode = 0
        mNoOfTrades = 0
        mVolume = 0
        mValue = 0
        mTradeValueFlag = ChrW(0)
        mTrend = ChrW(0)
        mSixLakhFlag = ChrW(0)
        mReservedField1 = ChrW(0)
        mMarketType = 0
        mSessionNumber = 0
        mLTPHour = ChrW(0)
        mLTPMinute = ChrW(0)
        mLTPSecond = ChrW(0)
        mLTPMillisecond(0) = ChrW(0)
        mLTPMillisecond(1) = ChrW(0)
        mLTPMillisecond(2) = ChrW(0)
        mReservedField2(0) = ChrW(0)
        mReservedField2(1) = ChrW(0)
        mReservedField10 = 0
        mReservedField11 = 0
        mNoOfPricePoints = 0
        mTimestamp = 0
        mCloseRate = 0
        mLastTradeQty = 0
        mLTP = 0
        mOpenRate = 0
        mPreviousCloseRate = 0
        mHighRate = 0
        mLowRate = 0
        mReservedField5 = 0
        mIndicativeEquilibriumPrice = 0
        mIndicativeEquilibriumQty = 0
        mTotalBidQty = 0
        mTotalOfferQty = 0
        mLowerCircuitLimit = 0
        mUpperCircuitLimit = 0
        mWeightedAverage = 0

        'mListBidOffer.Clear()
    End Sub

    Public Sub Read(reader As BigEndianBinaryReader)
        mNoOfTrades = reader.ReadUInt32()
        mVolume = reader.ReadInt64()
        mValue = reader.ReadInt64()
        mTradeValueFlag = reader.ReadChar()
        mTrend = reader.ReadChar()
        mSixLakhFlag = reader.ReadChar()
        mReservedField1 = reader.ReadChar()
        mMarketType = reader.ReadInt16()
        mSessionNumber = reader.ReadInt16()
        mLTPHour = reader.ReadChar()
        mLTPMinute = reader.ReadChar()
        mLTPSecond = reader.ReadChar()
        Dim ch As Char
        ch = reader.ReadChar()
        mLTPMillisecond(0) = ch
        ch = reader.ReadChar()
        mLTPMillisecond(1) = ch
        ch = reader.ReadChar()
        mLTPMillisecond(2) = ch
        ch = reader.ReadChar()
        mReservedField2(0) = ch
        ch = reader.ReadChar()
        mReservedField2(1) = ch
        mReservedField10 = reader.ReadInt16()
        mReservedField11 = reader.ReadInt64()
        mNoOfPricePoints = reader.ReadInt16()
        mTimestamp = reader.ReadInt64()
        mCloseRate = reader.ReadInt32()
        mLastTradeQty = reader.ReadInt64()
        mLTP = reader.ReadInt32()

        mOpenRate = ReadDecompressed(reader, mLTP)
        mPreviousCloseRate = ReadDecompressed(reader, mLTP)
        mHighRate = ReadDecompressed(reader, mLTP)
        mLowRate = ReadDecompressed(reader, mLTP)
        mReservedField5 = ReadDecompressed(reader, mLTP)
        mIndicativeEquilibriumPrice = ReadDecompressed(reader, mLTP)
        mIndicativeEquilibriumQty = ReadDecompressed(reader, mLastTradeQty)
        mTotalBidQty = ReadDecompressed(reader, mLastTradeQty)
        mTotalOfferQty = ReadDecompressed(reader, mLastTradeQty)
        mLowerCircuitLimit = ReadDecompressed(reader, mLastTradeQty)
        mUpperCircuitLimit = ReadDecompressed(reader, mLastTradeQty)
        mWeightedAverage = ReadDecompressed(reader, mLastTradeQty)
    End Sub

    Private Function ReadDecompressed(pReader As BigEndianBinaryReader, pBase As Int32) As Int32
        Dim val As Int32 = 0
        Dim chval As Int16 = pReader.ReadInt16()
        If chval = 32767 Then
            val = pReader.ReadInt32()
        Else
            val = pBase + chval
        End If
        Return val
    End Function

    Public Function ValidateInstrument(pInstrumentCode As UInt32) As Boolean
        If mInstrumentCode <> pInstrumentCode Then
            Return False
        End If
        Return True
    End Function
End Class

