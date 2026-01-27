Imports System.Collections.Concurrent


Public Class CBseIndexCodeNames
    Private mDictionary As ConcurrentDictionary(Of Integer, String)

    Public Sub New()
        mDictionary = New ConcurrentDictionary(Of Integer, String)()
        LoadIndexData()
    End Sub

    Private Sub LoadIndexData()
        ' Add all the index codes and names
        mDictionary.TryAdd(1, "SENSEX")
        mDictionary.TryAdd(2, "BSE100")
        mDictionary.TryAdd(3, "BSE200")
        mDictionary.TryAdd(4, "BSE500")
        mDictionary.TryAdd(7, "BSE CG")
        mDictionary.TryAdd(8, "BSE CD")
        mDictionary.TryAdd(10, "BSEPSU")
        mDictionary.TryAdd(12, "BANKEX")
        mDictionary.TryAdd(37, "COMDTY")
        mDictionary.TryAdd(38, "CONDIS")
        mDictionary.TryAdd(39, "ENERGY")
        mDictionary.TryAdd(40, "FINSER")
        mDictionary.TryAdd(41, "INDSTR")
        mDictionary.TryAdd(42, "LRGCAP")
        mDictionary.TryAdd(43, "MIDSEL")
        mDictionary.TryAdd(44, "SMLSEL")
        mDictionary.TryAdd(45, "TELCOM")
        mDictionary.TryAdd(47, "SNSX50")
        mDictionary.TryAdd(66, "BSEPBI")
        mDictionary.TryAdd(67, "BSESER")
        mDictionary.TryAdd(68, "SNXN30")
        mDictionary.TryAdd(69, "SNSX60")
        mDictionary.TryAdd(70, "SS6535")
        mDictionary.TryAdd(71, "POWENE")
        mDictionary.TryAdd(72, "200EQW")
        mDictionary.TryAdd(73, "INTECO")
        mDictionary.TryAdd(74, "CAPINS")
    End Sub

    ' Add a new index code and name
    Public Function AddIndex(id As Integer, value As String) As Boolean
        Return mDictionary.TryAdd(id, value)
    End Function

    ' Update an existing index or add if it doesn't exist
    Public Sub AddOrUpdateIndex(id As Integer, value As String)
        mDictionary.AddOrUpdate(id, value, Function(key, oldValue) value)
    End Sub

    ' Get index name by code
    Public Function GetIndexName(id As Integer) As String
        Dim value As String = Nothing
        If mDictionary.TryGetValue(id, value) Then
            Return value
        End If
        Return Nothing
    End Function

End Class



Public Class IndexRatesDictionary
    Private ReadOnly mDictRates As ConcurrentDictionary(Of Long, Double)

    Public Sub New()
        mDictRates = New ConcurrentDictionary(Of Long, Double)()
    End Sub

    Public Function AddRate(indexId As Long, rate As Double) As Boolean
        Return mDictRates.TryAdd(indexId, rate)
    End Function

    Public Sub AddOrUpdateRate(indexId As Long, rate As Double)
        mDictRates.AddOrUpdate(indexId, rate, Function(key, oldValue) rate)
    End Sub

    Public Function GetRate(indexId As Long) As Double?
        Dim rate As Double = Nothing
        If mDictRates.TryGetValue(indexId, rate) Then
            Return rate
        End If
        Return Nothing
    End Function
End Class
