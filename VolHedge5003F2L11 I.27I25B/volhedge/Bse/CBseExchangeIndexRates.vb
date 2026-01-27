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

    Public mIndexTokenFilter As ConcurrentDictionary(Of Long, String)
    Public mDictIndexTokenNames As ConcurrentDictionary(Of Long, String)

    Public Sub InitIndexRate()

        mIndexTokenFilter = New ConcurrentDictionary(Of Long, String)
        mDictIndexTokenNames = New ConcurrentDictionary(Of Long, String)

        AddIndexTokkenFilter(1)
        AddIndexTokkenFilter(2)
        AddIndexTokkenFilter(3)
        AddIndexTokkenFilter(12)
        AddIndexTokkenFilter(40)

        LoadIndexData(mDictIndexTokenNames)

    End Sub

    Private Sub LoadIndexData(pDict As ConcurrentDictionary(Of Long, String))

        pDict.TryAdd(1, "SENSEX")
        pDict.TryAdd(2, "BSE100")
        pDict.TryAdd(3, "BSE200")
        pDict.TryAdd(4, "BSE500")
        pDict.TryAdd(7, "BSE CG")
        pDict.TryAdd(8, "BSE CD")
        pDict.TryAdd(10, "BSEPSU")
        pDict.TryAdd(12, "BANKEX")
        pDict.TryAdd(37, "COMDTY")
        pDict.TryAdd(38, "CONDIS")
        pDict.TryAdd(39, "ENERGY")
        pDict.TryAdd(40, "FINSER")
        pDict.TryAdd(41, "INDSTR")
        pDict.TryAdd(42, "LRGCAP")
        pDict.TryAdd(43, "MIDSEL")
        pDict.TryAdd(44, "SMLSEL")
        pDict.TryAdd(45, "TELCOM")
        pDict.TryAdd(47, "SNSX50")
        pDict.TryAdd(66, "BSEPBI")
        pDict.TryAdd(67, "BSESER")
        pDict.TryAdd(68, "SNXN30")
        pDict.TryAdd(69, "SNSX60")
        pDict.TryAdd(70, "SS6535")
        pDict.TryAdd(71, "POWENE")
        pDict.TryAdd(72, "200EQW")
        pDict.TryAdd(73, "INTECO")
        pDict.TryAdd(74, "CAPINS")
    End Sub

    Public Function GetIndexName(id As Integer) As String
        Dim value As String = Nothing
        If mDictIndexTokenNames.TryGetValue(id, value) Then
            Return value
        End If
        Return Nothing
    End Function

    Public Sub AddIndexTokkenFilter(pToken As Long)
        mIndexTokenFilter.TryAdd(pToken, "")
    End Sub

    Public Function CheckIndexToken(pToken As Long) As Boolean
        Return mIndexTokenFilter.ContainsKey(pToken)
    End Function

    Private Sub ProcessIndexRates(pHeader As CBseIndexMsgHeader, inst As CIndexMsgInstrument, pPacketBuff As Byte())
        pHeader.Reset()
        Using mMemStream As New MemoryStream(pPacketBuff)
            Using mBinReader As New BigEndianBinaryReader(mMemStream)
                pHeader.Read(mBinReader)
                If pHeader.mNoOfRecords < 1 Then
                    pHeader.Reset()
                    Return
                End If
                For i As Integer = 0 To pHeader.mNoOfRecords - 1
                    inst.Reset()
                    inst.Read(mBinReader)
                    'inst.PrintValues(pHeader)
                    'inst.WriteToInstrumentFile("D:\daily\index\", pHeader)
                    Dim lnCode As Long = inst.mInstrumentCode

                    If Not CheckIndexToken(lnCode) Then
                        Continue For
                    End If

                    Dim dblIdx As Double = inst.mIndex / 100
                    Dim dblClose As Double = inst.mClose / 100.0
                    AddUpdateRates(lnCode, dblIdx)
                    'mDictRates.TryAdd(lnCode, dblIdx)
                    AddIndexRate(lnCode, dblIdx, 0, 0, dblClose)
                    'AddScriptRate(lnCode, dblIdx, 0, 0, dblClose)
                Next
            End Using
        End Using
    End Sub


End Class
