Imports System
Imports System.io
Imports System.Data
Imports System.Data.OleDb

Public Class rptMarginReport
    Dim dtmarginreport As New DataTable
    Public Sub ShowForm(ByVal dtMargin As DataTable)

        dtmarginreport = dtMargin

        Dim dv As DataView
        dv = New DataView(dtmarginreport)
        If dtmarginreport.Rows.Count > 0 Then


            Dim dt As DataTable = dv.ToTable(True, "company", "lfv", "sfv", "lov", "sov", "spanreq", "anov", "ShortOption", "Price", "Exp", "ExpMargin", "InitMargin", "TotalMargin", "BuyPrice", "BuyUnit", "BuyPer", "SellPrice", "SellUnit", "SellPer")
            Dim sumdt As New DataTable
            sumdt = dt.Clone
            sumdt.Clear()
            Dim row As DataRow
            row = sumdt.NewRow()

            'row("Expiry") = ""
            row("company") = "Total :"
            row("lfv") = dt.Compute("Sum(lfv)", "")
            row("sfv") = dt.Compute("Sum(sfv)", "")
            row("lov") = dt.Compute("Sum(lov)", "")
            row("sov") = dt.Compute("Sum(sov)", "")
            row("spanreq") = dt.Compute("Sum(spanreq)", "")
            row("anov") = dt.Compute("Sum(anov)", "")
            row("ShortOption") = dt.Compute("Sum(ShortOption)", "")
            row("Price") = 0 'dt.Compute("Sum(Price)", "")
            row("Exp") = 0 'dt.Compute("Sum(Exp)", "")
            row("ExpMargin") = dt.Compute("Sum(ExpMargin)", "")
            row("InitMargin") = dt.Compute("Sum(InitMargin)", "")
            row("TotalMargin") = dt.Compute("Sum(TotalMargin)", "")

            sumdt.Rows.Add(row)

            DGFOTrading.DataSource = dt
            DataGridView1.DataSource = sumdt


            For index As Integer = 0 To DGFOTrading.Columns.Count - 1
                DataGridView1.Columns(index).Width = DGFOTrading.Columns(index).Width
            Next
        End If

        Me.ShowDialog()
    End Sub

    Private Sub DGFOTrading_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGFOTrading.CellContentClick

    End Sub

    Private Sub DGFOTrading_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DGFOTrading.DataError

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError

    End Sub

    Private Sub DataGridView1_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DataGridView1.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            DGFOTrading.HorizontalScrollingOffset = e.NewValue()
            'ClsScrollbar.GetSBarPos(grdmain)
            'ClsScrollbar.SetSBarPos(grdcp)
            'grdcp_Scroll(sender, e)
        End If
    End Sub

    Private Sub txtDealer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDealer.TextChanged

        If txtDealer.Text = "" Then
            Return
        End If

        Dim dv As DataView
        dv = New DataView(dtmarginreport, "company like '" & txtDealer.Text & "%'", "", DataViewRowState.CurrentRows)
        Dim dt As DataTable = dv.ToTable(True, "company", "lfv", "sfv", "lov", "sov", "spanreq", "anov", "ShortOption", "Price", "Exp", "ExpMargin", "InitMargin", "TotalMargin")
        Dim sumdt As New DataTable
        sumdt = dt.Clone
        sumdt.Clear()
        Dim row As DataRow
        row = sumdt.NewRow()

        'row("Expiry") = ""
        row("company") = "Total :"
        row("lfv") = dt.Compute("Sum(lfv)", "")
        row("sfv") = dt.Compute("Sum(sfv)", "")
        row("lov") = dt.Compute("Sum(lov)", "")
        row("sov") = dt.Compute("Sum(sov)", "")
        row("spanreq") = dt.Compute("Sum(spanreq)", "")
        row("anov") = dt.Compute("Sum(anov)", "")
        row("ShortOption") = dt.Compute("Sum(ShortOption)", "")
        row("Price") = 0 'dt.Compute("Sum(Price)", "")
        row("Exp") = 0 'dt.Compute("Sum(Exp)", "")
        row("ExpMargin") = dt.Compute("Sum(ExpMargin)", "")
        row("InitMargin") = dt.Compute("Sum(InitMargin)", "")
        row("TotalMargin") = dt.Compute("Sum(TotalMargin)", "")

        sumdt.Rows.Add(row)

        DGFOTrading.DataSource = dt
        DataGridView1.DataSource = sumdt

        For index As Integer = 0 To DGFOTrading.Columns.Count - 1
            DataGridView1.Columns(index).Width = DGFOTrading.Columns(index).Width
        Next

    End Sub

    Private Sub DGFOTrading_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DGFOTrading.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            DataGridView1.HorizontalScrollingOffset = e.NewValue()
            'ClsScrollbar.GetSBarPos(grdmain)
            'ClsScrollbar.SetSBarPos(grdcp)
            'grdcp_Scroll(sender, e)
        End If
    End Sub


    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'grdtrad.DataSource = dtable
        'grdtrad.Refresh()
        'grdeq.DataSource = eqtable
        'grdeq.Refresh()
        If DGFOTrading.Rows.Count > 0 Then
            Dim savedi As New SaveFileDialog
            savedi.Filter = "Files(*.XLS)|*.XLS"
            If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                'exportExcel(grdtrad, savedi.FileName)
                'exportExcel(grdeq, savedi.FileName)
                Dim grd(0) As DataGridView
                grd(0) = DGFOTrading
                Dim sname(0) As String
                sname(0) = "Trading"
                Dim ArrColList As New ArrayList

                'ArrColList.Add("Instrument")
                'ArrColList.Add("company")
                'ArrColList.Add("CPF")
                'ArrColList.Add("MDate")
                'ArrColList.Add("EntryDate")
                'ArrColList.Add("strike")
                'ArrColList.Add("Qty")
                'ArrColList.Add("traded")
                'ArrColList.Add("Dealer")

                '   ArrColList.Add("Expiry")
                ArrColList.Add("script")
                ArrColList.Add("lfv")
                ArrColList.Add("sfv")
                ArrColList.Add("lov")
                ArrColList.Add("sov")
                ArrColList.Add("spanreq")
                ArrColList.Add("anov")
                ArrColList.Add("ShortOption")
                ArrColList.Add("price")
                ArrColList.Add("Exp")
                ArrColList.Add("ExpMargin")
                ArrColList.Add("InitMargin")
                ArrColList.Add("TotalMargin")


                Call exporttoexcel(grd, savedi.FileName, sname, "FO", ArrColList)
                MsgBox("Future & Option Trade Export to Excel Successfully  ", MsgBoxStyle.Information)
            End If
        End If
    End Sub

   
    Private Sub rptMarginReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class