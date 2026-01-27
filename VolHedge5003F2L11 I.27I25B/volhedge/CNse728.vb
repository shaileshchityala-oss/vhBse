Imports System.Runtime.InteropServices

' -------------------------
' MBP_INFORMATION (12 bytes)
' -------------------------
<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure MBP_INFORMATION
    Public Quantity As Int32          ' LONG (4) Signed
    Public Price As Int32             ' LONG (4) Signed
    Public NumberOfOrders As Int16    ' SHORT (2) Signed
    Public BbBuySellFlag As Int16     ' SHORT (2) Signed
End Structure

' -------------------------
' INTERACTIVE_ONLY_MBP_DATA (213 bytes)
' -------------------------
<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure INTERACTIVE_ONLY_MBP_DATA
    Public Token As Int32                     ' LONG (4) Signed
    Public BookType As Int16                  ' SHORT (2) Signed
    Public TradingStatus As Int16             ' SHORT (2) Signed
    Public VolumeTradedToday As UInt32        ' UNSIGNED LONG (4) Unsigned
    Public LastTradedPrice As Int32           ' LONG (4) Signed
    Public NetChangeIndicator As SByte        ' CHAR (1) Signed
    Public NetPriceChangeFromClosingPrice As Int32 ' LONG (4) Signed
    Public LastTradeQuantity As Int32         ' LONG (4) Signed
    Public LastTradeTime As Int32             ' LONG (4) Signed
    Public AverageTradePrice As Int32         ' LONG (4) Signed
    Public AuctionNumber As Int16             ' SHORT (2) Signed
    Public AuctionStatus As Int16             ' SHORT (2) Signed
    Public InitiatorType As Int16             ' SHORT (2) Signed
    Public InitiatorPrice As Int32            ' LONG (4) Signed
    Public InitiatorQuantity As Int32         ' LONG (4) Signed
    Public AuctionPrice As Int32              ' LONG (4) Signed
    Public AuctionQuantity As Int32           ' LONG (4) Signed

    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)>
    Public RecordBuffer As MBP_INFORMATION()  ' 10 × 12 bytes = 120

    Public BbTotalBuyFlag As Int16            ' SHORT (2) Signed
    Public BbTotalSellFlag As Int16           ' SHORT (2) Signed
    Public TotalBuyQuantity As Double         ' DOUBLE (8) Signed
    Public TotalSellQuantity As Double        ' DOUBLE (8) Signed

    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public ST_INDICATOR As Byte()             ' STRUCT(2) placeholder

    Public ClosingPrice As Int32              ' LONG (4) Signed
    Public OpenPrice As Int32                 ' LONG (4) Signed
    Public HighPrice As Int32                 ' LONG (4) Signed
    Public LowPrice As Int32                  ' LONG (4) Signed
End Structure

' -------------------------
' BCAST_HEADER (40 bytes)
' -------------------------
<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure BCAST_HEADER
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public Reserved1 As SByte()               ' CHAR(2) Signed

    Public LogTime As Int32                   ' LONG (4) Signed

    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public AlphaChar As SByte()               ' CHAR(2) Signed

    Public TransactionCode As Int16           ' SHORT (2) Signed
    Public ErrorCode As Int16                 ' SHORT (2) Signed
    Public BCSeqNo As Int32                   ' LONG (4) Signed

    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=1)>
    Public Reserved2 As SByte()               ' CHAR(1) Signed

    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)>
    Public Reserved3 As SByte()               ' CHAR(3) Signed

    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)>
    Public TimeStamp2 As SByte()              ' CHAR(8) Signed

    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)>
    Public Filler2 As Byte()                  ' BYTE(8)

    Public MessageLength As Int16             ' SHORT (2) Signed
End Structure

' -------------------------
' MS_BCAST_ONLY_MBP (470 bytes)
' -------------------------
<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure MS_BCAST_ONLY_MBP
    Public Header As BCAST_HEADER                     ' STRUCT (40) @0
    Public NoOfRecords As Int16                       ' SHORT (2) Signed

    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public Records As INTERACTIVE_ONLY_MBP_DATA()     ' 2 × 213 bytes = 426

    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public Reserved As Byte()                         ' Reserved (2)
End Structure
