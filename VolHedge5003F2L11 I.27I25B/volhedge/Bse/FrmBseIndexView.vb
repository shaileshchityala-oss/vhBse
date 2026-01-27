Imports System
Imports System.Configuration
Imports System.IO
Imports System.Threading
Imports System.Net
Imports System.Data

Public Class FrmBseIndexView

    Private mBseExch As CBseExchange
    Private mBseIndexLiveData As CBseIndexLiveDataTable


    Protected Overrides Sub OnLoad(e As EventArgs)

        mBseExch = clsGlobal.mBseExchange
        mBseIndexLiveData = New CBseIndexLiveDataTable()
        dgBseIndex.DataSource = mBseIndexLiveData.mDtIndex
        Timer1.Interval = 1000
        Timer1.Start()
        dgBseIndex.Columns()("Code").Visible = True
        dgBseIndex.Columns()("Open").Visible = False
        dgBseIndex.Columns()("High").Visible = False
        dgBseIndex.Columns()("Low").Visible = False
        dgBseIndex.Columns()("Close").Visible = False

        MyBase.OnLoad(e)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '  Private mDictRates As ConcurrentDictionary(Of Long, Double)        

        Dim dt As DataTable = mBseIndexLiveData.mDtIndex

        For Each index As KeyValuePair(Of Long, Double) In mBseExch.mDictRates
            Dim rate As Double = index.Value
            Dim lnToken As Long = index.Key
            Dim strsm As String
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

        '    dgBseIndex.DataSource = mBseIndexLiveData.mDtIndex
        '    dgBseIndex.Invalidate()
        '   dgBseIndex.Refresh()

    End Sub

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

End Class