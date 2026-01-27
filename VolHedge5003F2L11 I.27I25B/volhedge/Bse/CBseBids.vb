Imports System
Imports System.IO
Public Class CBseMarketPictureBidOffer
    Public mBestBidRate As Int32
    Public mTotalBidQty As Int32
    Public mNoOfBid As Int32
    Public mImpliedBuyQuantity As Int32
    Public mBidReservedField As Int32

    Public mBestOfferRate As Int32
    Public mTotalOfferQty As Int32
    Public mNoOfAsk As Int32
    Public mImpliedSellQuantity As Int32
    Public mOfferReservedField As Int32

    Public Sub New()
        Reset()
    End Sub

    Public Sub Reset()
        mBestBidRate = 0
        mTotalBidQty = 0
        mNoOfBid = 0
        mImpliedBuyQuantity = 0
        mBidReservedField = 0

        mBestOfferRate = 0
        mTotalOfferQty = 0
        mNoOfAsk = 0
        mImpliedSellQuantity = 0
        mOfferReservedField = 0
    End Sub

    Public Function ReadBids(reader As BigEndianBinaryReader, pBaseRate As Int32, pBaseQty As Int32) As Boolean
        mBestBidRate = ReadDecompressed(reader, pBaseRate)
        If mBestBidRate = 32766 Then
            Return False
        End If

        mTotalBidQty = ReadDecompressed(reader, pBaseQty)
        mNoOfBid = ReadDecompressed(reader, pBaseRate)
        mImpliedBuyQuantity = ReadDecompressed(reader, pBaseQty)
        'mBidReservedField = ReadDecompressed(reader, pBaseQty)

        Return True
    End Function

    Private Function ReadDecompressed(pReader As BigEndianBinaryReader, pBase As Int32) As Int32
        Dim val As Int32 = 0
        Dim chval As Int16 = pReader.ReadInt16()

        If (chval = 32766) OrElse (chval = -32766) Then
            Return chval
        End If

        If chval = 32767 Then
            val = pReader.ReadInt32()
        Else
            val = pBase + chval
        End If

        Return val
    End Function

    Public Function ReadOffers(reader As BigEndianBinaryReader, pBaseRate As Int32, pBaseQty As Int32) As Boolean
        mBestOfferRate = ReadDecompressed(reader, pBaseRate)
        If mBestBidRate = -32766 Then
            Return False
        End If

        mTotalOfferQty = ReadDecompressed(reader, pBaseQty)
        mNoOfAsk = ReadDecompressed(reader, pBaseQty)
        mImpliedSellQuantity = ReadDecompressed(reader, pBaseQty)
        ' mOfferReservedField = ReadDecompressed(reader, pBaseQty)

        Return True
    End Function
End Class