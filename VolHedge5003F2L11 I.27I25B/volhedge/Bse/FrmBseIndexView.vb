Imports System
Imports System.Configuration
Imports System.IO
Imports System.Threading
Imports System.Net
Imports System.Data

Public Class FrmBseIndexView

    Private mBseExch As CBseExchange
    Private mBseIndexLiveData As CBseIndexLiveDataTable

    Dim mDtNse As DataTable
    Protected Overrides Sub OnLoad(e As EventArgs)

        mBseExch = clsGlobal.mBseExchange
        mBseIndexLiveData = New CBseIndexLiveDataTable()
        dgBseIndex.DataSource = mBseIndexLiveData.mDtIndex
        Timer1.Interval = 1000
        Timer1.Start()
        dgBseIndex.Columns()("Code").Visible = True
        dgBseIndex.Columns()("Code").Width = 5
        dgBseIndex.Columns()("Open").Visible = False
        dgBseIndex.Columns()("High").Visible = False
        dgBseIndex.Columns()("Low").Visible = False
        dgBseIndex.Columns()("Close").Visible = False
        dgBseIndex.Columns("Ltp").DefaultCellStyle.Format = "0.00"

        mDtNse = New DataTable()
        mDtNse.Clear()
        mDtNse.Columns.Add("Code", GetType(String))
        mDtNse.Columns.Add("Index", GetType(String))
        mDtNse.Columns.Add("Ltp", GetType(Double))

        mDtNse.Columns.Add("Open", GetType(Double))
        mDtNse.Columns.Add("High", GetType(Double))
        mDtNse.Columns.Add("Low", GetType(Double))
        mDtNse.Columns.Add("Close", GetType(Double))
        mDtNse.PrimaryKey = New DataColumn() {mDtNse.Columns("Code")}


        dgvNseIndex.DataSource = mDtNse
        dgvNseIndex.Columns()("Code").Visible = True
        dgvNseIndex.Columns()("Code").Width = 5
        dgvNseIndex.Columns()("Open").Visible = False
        dgvNseIndex.Columns()("High").Visible = False
        dgvNseIndex.Columns()("Low").Visible = False
        dgvNseIndex.Columns()("Close").Visible = False

        dgvNseIndex.Columns("Ltp").DefaultCellStyle.Format = "0.00"

        NseAddNewRow(mDtNse, "NIFTY", "NIFTY", 0, 0, 0, 0, 0)
        NseAddNewRow(mDtNse, "BANKNIFTY", "BANKNIFTY", 0, 0, 0, 0, 0)
        NseAddNewRow(mDtNse, "FINNIFTY", "FINNIFTY", 0, 0, 0, 0, 0)
        NseAddNewRow(mDtNse, "MIDCPNIFTY", "MIDCPNIFTY", 0, 0, 0, 0, 0)

        MyBase.OnLoad(e)
    End Sub

    Public Sub NseAddNewRow(pDt As DataTable, pCode As String, pIndexName As String, pLtp As Double, pOpen As Double, pHigh As Double, pLow As Double, pClose As Double)
        Dim row As DataRow = pDt.NewRow()
        row("Code") = pCode
        row("Index") = pIndexName
        row("Ltp") = pLtp
        row("Open") = pOpen
        row("High") = pHigh
        row("Low") = pLow
        row("Close") = pClose
        pDt.Rows.Add(row)
    End Sub

    Public Sub NseUpdateLtp(pDt As DataTable, pCode As String, pLtp As Double)
        Dim row As DataRow = pDt.Rows.Find(pCode)
        If row IsNot Nothing Then
            row("Ltp") = pLtp
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '  Private mDictRates As ConcurrentDictionary(Of Long, Double)        

        Dim dt As DataTable = mBseIndexLiveData.mDtIndex
        Dim rate As Double
        For Each index As KeyValuePair(Of Long, Double) In mBseExch.mDictRates
            rate = index.Value
            Dim lnToken As Long = index.Key
            Dim strsm As String

            If lnToken > 200 Then
                Continue For
            End If

            If Not mBseIndexLiveData.IsExistRow(dt, lnToken) Then
                mBseIndexLiveData.AddOrUpdateIndexData(dt, lnToken, "-", rate, 0, 0, 0, 0)
                mBseIndexLiveData.UpdateName(dt, lnToken, mBseExch.GetIndexName(lnToken))

                If HT_TokenSymbol.ContainsKey(lnToken) Then
                    strsm = HT_TokenSymbol(lnToken)
                End If

                If lnToken > 100 Then

                    If strsm IsNot Nothing Then
                        mBseIndexLiveData.UpdateName(dt, lnToken, strsm)
                    End If

                End If
            Else
                mBseIndexLiveData.UpdateLtp(dt, lnToken, rate)
            End If
        Next

        UdpateFromHash(dt)
        clsGlobal.mPerf.Push("edix index view")
        '' print eIdxprice
        clsGlobal.mPerf.Pop()

        rate = NseGetRate("Nifty 50")
        NseUpdateLtp(mDtNse, "NIFTY", rate)
        rate = NseGetRate("Nifty Bank")
        NseUpdateLtp(mDtNse, "BANKNIFTY", rate)
        rate = NseGetRate("NiftyFinService")
        NseUpdateLtp(mDtNse, "FINNIFTY", rate)
        rate = NseGetRate("Nifty Midcap 50")
        NseUpdateLtp(mDtNse, "MIDCPNIFTY", rate)
    End Sub

    Private Function NseGetRate(pString As String) As Double
        If eIdxprice(pString) Is Nothing Then
            Return 0
        Else
            Return eIdxprice(pString)
        End If
    End Function

    Private Sub UdpateFromHash(dt As DataTable)

        '  ProcessPrices(eltpprice)
        '  ProcessPrices(ltpprice)
        '  ProcessPrices(fltpprice)

        Try
            '    For Each entry As DictionaryEntry In eltpprice
            '        ' Console.WriteLine("Key: " & entry.Key & "  Value: " & entry.Value)
            '        Dim rate As Double = entry.Value
            '        Dim lnToken As Long = entry.Key
            '        Dim strsm As String
            '        If Not mBseIndexLiveData.IsExistRow(dt, lnToken) Then
            '            mBseIndexLiveData.AddOrUpdateIndexData(dt, lnToken, "-", rate, 0, 0, 0, 0)
            '            mBseIndexLiveData.UpdateName(dt, lnToken, mBseExch.GetIndexName(lnToken))
            '            If HT_TokenSymbol.ContainsKey(lnToken) Then
            '                strsm = HT_TokenSymbol(lnToken)
            '            End If

            '            'strsm = HT_TokenSymbol(40340)
            '            If lnToken > 100 Then
            '                If strsm IsNot Nothing Then
            '                    mBseIndexLiveData.UpdateName(dt, lnToken, strsm)
            '                End If

            '            End If
            '        Else
            '            mBseIndexLiveData.UpdateLtp(dt, lnToken, rate)
            '        End If
            '    Next

            'For Each entry As DictionaryEntry In ltpprice
            '    'Console.WriteLine("Key: " & entry.Key & "  Value: " & entry.Value)
            '    Dim rate As Double = entry.Value
            '    Dim lnToken As Long = entry.Key
            '    Dim strsm As String
            '    If Not mBseIndexLiveData.IsExistRow(dt, lnToken) Then
            '        mBseIndexLiveData.AddOrUpdateIndexData(dt, lnToken, "-", rate, 0, 0, 0, 0)
            '        mBseIndexLiveData.UpdateName(dt, lnToken, mBseExch.GetIndexName(lnToken))
            '        strsm = HT_TokenSymbol(lnToken)
            '        'strsm = HT_TokenSymbol(40340)
            '        If lnToken > 100 Then
            '            If strsm IsNot Nothing Then
            '                mBseIndexLiveData.UpdateName(dt, lnToken, strsm)
            '            End If

            '        End If
            '    Else
            '        mBseIndexLiveData.UpdateLtp(dt, lnToken, rate)
            '    End If
            'Next

            'For Each entry As DictionaryEntry In fltpprice
            '    'Console.WriteLine("Key: " & entry.Key & "  Value: " & entry.Value)
            '    Dim rate As Double = entry.Value
            '    Dim lnToken As Long = entry.Key
            '    Dim strsm As String
            '    If Not mBseIndexLiveData.IsExistRow(dt, lnToken) Then
            '        mBseIndexLiveData.AddOrUpdateIndexData(dt, lnToken, "-", rate, 0, 0, 0, 0)
            '        mBseIndexLiveData.UpdateName(dt, lnToken, mBseExch.GetIndexName(lnToken))
            '        strsm = HT_TokenSymbol(lnToken)
            '        'strsm = HT_TokenSymbol(40340)
            '        If lnToken > 100 Then
            '            If strsm IsNot Nothing Then
            '                mBseIndexLiveData.UpdateName(dt, lnToken, strsm)
            '            End If

            '        End If
            '    Else
            '        mBseIndexLiveData.UpdateLtp(dt, lnToken, rate)
            '    End If
            'Next

        Catch ex As Exception

        End Try
    End Sub

    Public Sub ProcessPrices(pHtPrices As Hashtable)

        Dim dt As DataTable = mBseIndexLiveData.mDtIndex

        For Each entry As KeyValuePair(Of Long, Double) In pHtPrices

            Dim lnToken As Long = entry.Key
            Dim rate As Double = entry.Value
            Dim strsm As String = Nothing

            ' Check if token exists in your main table
            If Not mBseIndexLiveData.IsExistRow(dt, lnToken) Then

                ' Insert new row
                mBseIndexLiveData.AddOrUpdateIndexData(dt, lnToken, "-", rate, 0, 0, 0, 0)

                ' Update name from exchange
                mBseIndexLiveData.UpdateName(dt, lnToken, mBseExch.GetIndexName(lnToken))

                ' Safely fetch symbol from hashtable
                If HT_TokenSymbol.ContainsKey(lnToken) Then
                    strsm = HT_TokenSymbol(lnToken)
                End If

                ' Update name again if symbol exists
                If lnToken > 100 AndAlso Not String.IsNullOrEmpty(strsm) Then
                    mBseIndexLiveData.UpdateName(dt, lnToken, strsm)
                End If

            Else
                ' Existing row → Just update LTP
                mBseIndexLiveData.UpdateLtp(dt, lnToken, rate)
            End If

        Next

    End Sub

    Private Sub chkOnTop_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnTop.CheckedChanged
        TopMost = chkOnTop.Checked
    End Sub
End Class