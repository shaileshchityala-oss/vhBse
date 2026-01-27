Imports System
Imports System.Configuration
Imports System.IO
Imports System.Threading
Imports System.Net
Imports System.Data

Public Class CBseIndexLiveDataTable

    Public mDtIndex As DataTable

    Public Sub New()
        mDtIndex = New DataTable()
        AddColumns(mDtIndex)
    End Sub

    Private Sub AddColumns(pDt As DataTable)
        pDt.Clear()
        pDt.Columns.Add("Code", GetType(Long))
        pDt.Columns.Add("Index Name", GetType(String))
        pDt.Columns.Add("Ltp", GetType(Double))
        pDt.Columns.Add("Open", GetType(Double))
        pDt.Columns.Add("High", GetType(Double))
        pDt.Columns.Add("Low", GetType(Double))
        pDt.Columns.Add("Close", GetType(Double))
        pDt.PrimaryKey = New DataColumn() {pDt.Columns("Code")}
    End Sub

    Private Sub UpdateRow(pDt As DataTable, pCode As Long, pIndexName As String, pLtp As Double, pOpen As Double, pHigh As Double, pLow As Double, pClose As Double)
        Dim row As DataRow = pDt.NewRow()
        row("Code") = pCode
        row("Index Name") = pIndexName
        row("Ltp") = pLtp
        row("Open") = pOpen
        row("High") = pHigh
        row("Low") = pLow
        row("Close") = pClose
        pDt.Rows.Add(row)
    End Sub


    Public Sub AddOrUpdateIndexData(pDt As DataTable, pCode As Long, pIndexName As String, pLtp As Double, pOpen As Double, pHigh As Double, pLow As Double, pClose As Double)

        If IsExistRow(pDt, pCode) Then
            UpdateRow(pDt, pCode, pIndexName, pLtp, pOpen, pHigh, pLow, pClose)
        Else
            AddNewRow(pDt, pCode, pIndexName, pLtp, pOpen, pHigh, pLow, pClose)
        End If

    End Sub


    Private Sub AddNewRow(pDt As DataTable, pCode As Long, pIndexName As String, pLtp As Double, pOpen As Double, pHigh As Double, pLow As Double, pClose As Double)
        Dim row As DataRow = pDt.NewRow()
        row("Code") = pCode
        row("Index Name") = pIndexName
        row("Ltp") = pLtp
        row("Open") = pOpen
        row("High") = pHigh
        row("Low") = pLow
        row("Close") = pClose
        pDt.Rows.Add(row)
    End Sub

    Public Function IsExistRow(pDt As DataTable, pCode As Long) As Boolean
        Dim rw As DataRow = pDt.Rows.Find(pCode)
        If rw Is Nothing Then
            Return False
        End If
        Return True
    End Function

    Public Sub UpdateName(pDt As DataTable, pCode As Long, pName As String)
        Dim row As DataRow = pDt.Rows.Find(pCode)
        row("Index Name") = pName
    End Sub


    Public Sub UpdateLtp(pDt As DataTable, pCode As Long, pLtp As Double)
        Dim row As DataRow = pDt.Rows.Find(pCode)
        If row IsNot Nothing Then
            row("Ltp") = pLtp
        End If

    End Sub

    Public Sub UpdateOpen(pDt As DataTable, pCode As Long, pOpen As Double)
        Dim row As DataRow = pDt.Rows.Find(pCode)
        row("Open") = pOpen
    End Sub

    Public Sub UpdateHigh(pDt As DataTable, pCode As Long, pHigh As Double)
        Dim row As DataRow = pDt.Rows.Find(pCode)
        row("High") = pHigh
    End Sub

    Public Sub UpdateLow(pDt As DataTable, pCode As Long, pLow As Double)
        Dim row As DataRow = pDt.Rows.Find(pCode)
        row("Low") = pLow
    End Sub

    Public Sub UpdateClose(pDt As DataTable, pCode As Long, pClose As Double)
        Dim row As DataRow = pDt.Rows.Find(pCode)
        row("Close") = pClose
    End Sub

    Public Sub UpdateIndexData(pDt As DataTable, pCode As Long, pIndexName As String, pLtp As Double, pOpen As Double, pHigh As Double, pLow As Double, pClose As Double)
        Dim row As DataRow = pDt.Rows.Find(pCode)
        row("Index Name") = pIndexName
        row("Ltp") = pLtp
        row("Open") = pOpen
        row("High") = pHigh
        row("Low") = pLow
        row("Close") = pClose
    End Sub

End Class