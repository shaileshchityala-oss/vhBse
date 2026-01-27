Imports System
Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Public Class FrmCFBalance
    Dim tblCFBalance As New DataTable
    Dim objTrad As trading = New trading
    Public Sub ShowForm()

        Me.ShowDialog()
    End Sub
    Private Sub FrmCFBalance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        init_table()
        FillGrid()
    End Sub
    Private Sub init_table()
        tblCFBalance = New DataTable
        With tblCFBalance.Columns
            .Add("Symbol")
            .Add("Balance")
        End With
    End Sub
    'Private Sub FillGrid()

    '    Dim masterdata As DataTable
    '    masterdata = cpfmaster
    '    Dim dtdtableView As DataView = New DataView(masterdata, "", "Symbol", DataViewRowState.CurrentRows)
    '    Dim dtsymbol As DataTable = dtdtableView.ToTable(True, "Symbol")

    '    Dim tblCfbalancedata As DataTable = objTrad.Select_CFBalance
    '    Dim drow As DataRow
    '    For Each dr As DataRow In dtsymbol.Rows
    '        Dim drcf() As DataRow = tblCfbalancedata.Select("Symbol='" & dr("Symbol") & "'")
    '        If drcf.Length > 0 Then
    '            drow = tblCFBalance.NewRow()
    '            drow("Symbol") = drcf(0)("Symbol")
    '            drow("Balance") = drcf(0)("Balance")
    '        Else
    '            drow = tblCFBalance.NewRow()
    '            drow("Symbol") = dr("Symbol")
    '            drow("Balance") = "0.00"
    '            objTrad.Insert_CFBalance(dr("Symbol"), drow("Balance"))
    '        End If
    '        tblCFBalance.Rows.Add(drow)
    '    Next
    '    DGCFBalance.DataSource = tblCFBalance


    'End Sub
    Private Sub FillGrid()

        'Dim masterdata As DataTable
        'masterdata = cpfmaster
        'Dim dtdtableView As DataView = New DataView(masterdata, "", "Symbol", DataViewRowState.CurrentRows)
        'Dim dtsymbol As DataTable = dtdtableView.ToTable(True, "Symbol")

        'Dim tblCfbalancedata As DataTable = objTrad.Select_CFBalance
        'Dim drow As DataRow
        'For Each dr As DataRow In dtsymbol.Rows
        '    Dim drcf() As DataRow = tblCfbalancedata.Select("Symbol='" & dr("Symbol") & "'")
        '    If drcf.Length > 0 Then
        '        drow = tblCFBalance.NewRow()
        '        drow("Symbol") = drcf(0)("Symbol")
        '        drow("Balance") = drcf(0)("Balance")
        '    Else
        '        drow = tblCFBalance.NewRow()
        '        drow("Symbol") = dr("Symbol")
        '        drow("Balance") = "0.00"
        '        objTrad.Insert_CFBalance(dr("Symbol"), drow("Balance"))
        '    End If
        '    tblCFBalance.Rows.Add(drow)
        'Next
        'DGCFBalance.DataSource = tblCFBalance


        Dim tblCfbalancedata As DataTable = objTrad.Select_CFBalance
        Dim drow As DataRow
        For Each dr As DataRow In tblCfbalancedata.Rows
            Dim drcf() As DataRow = tblCfbalancedata.Select("Symbol='" & dr("Symbol") & "'")
            If drcf.Length > 0 Then
                drow = tblCFBalance.NewRow()
                drow("Symbol") = drcf(0)("Symbol")
                drow("Balance") = drcf(0)("Balance")
            Else
                drow = tblCFBalance.NewRow()
                drow("Symbol") = dr("Symbol")
                drow("Balance") = "0.00"
                objTrad.Insert_CFBalance(dr("Symbol"), drow("Balance"))
            End If
            tblCFBalance.Rows.Add(drow)
        Next
        DGCFBalance.DataSource = tblCFBalance


    End Sub


    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim dt As DataTable = DGCFBalance.DataSource
        objTrad.update_CFBalance1(dt)
        mmDTCFBalance = dt
        MessageBox.Show("Apply Successfully..")
    End Sub

    Private Sub txtsymbol_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim dv As New DataView(tblCFBalance)
        ''dv.RowFilter = "Compname like '%" + txtcomp.Text + "%'"
        'dv.RowFilter = "Compname like '" + txtsymbol.Text + "%'"
        'DGCFBalance.DataSource = dv
    End Sub

    Private Sub FrmCFBalance_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        FlgCFBalance = True
    End Sub
End Class