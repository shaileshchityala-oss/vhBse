Imports System
Imports System.io
Imports System.Data
Imports System.Data.OleDb
Public Class FrmPLanalysis
    Public Shared chkPLanalysis As Boolean
    Dim ObjPLAna As New PLAnaProcess
    Dim dtSelect_Sum_PnL As DataTable
    Dim dtSelect_PnL As DataTable
    Private Sub FrmPLanalysis_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        chkPLanalysis = False
    End Sub

    Private Sub FrmPLanalysis_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            Exit Sub
        End If
        If e.KeyCode = Keys.F11 Then
            Call Button1_Click(sender, e)
        End If
    End Sub

    Private Sub FrmPLanalysis_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtSelect_Sum_PnL = ObjPLAna.Select_Sum_PnL
        dtSelect_PnL = ObjPLAna.Select_PnL

        If dtSelect_Sum_PnL.Rows.Count = 0 Then Exit Sub
        grdtrad.DataSource = dtSelect_Sum_PnL
        grdtrad.Rows(0).DefaultCellStyle.BackColor = Color.LightSkyBlue
        grdtrad.Rows(0).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold, GraphicsUnit.Point)
        grdtrad.Rows(0).Frozen = True
        'VarFrmIsLoad = True


        txtPrevSavedOn.Text = ""
        If Not dtanalysisData.Rows.Count = 0 Then
            Dim preDate As Date
            preDate = dtanalysisData.Compute("MAX(preDate)", "")
            txtPrevSavedOn.Text = preDate.ToString("dd-MMM-yyyy")
        End If


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)

        If (EXPORT_IMPORT_POSITION = 2) Then
            If grdtrad.Rows.Count > 0 Then

                Dim savedi As New SaveFileDialog
                savedi.Filter = "Files(*.XLS)|*.XLS"
                If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim grd(0) As DataGridView
                    grd(0) = grdtrad
                    Dim sname(0) As String
                    sname(0) = "PLAnalysis"

                    exporttoexcel(grd, savedi.FileName, sname, "other")
                    MsgBox("Export Successfully")
                    OPEN_Export_File(savedi.FileName)
                End If
            End If
        ElseIf (EXPORT_IMPORT_POSITION = 1) Then

            Dim savedi As New SaveFileDialog
            savedi.Filter = "File(*.csv)|*.Csv"
            If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim dt As DataTable
                dt = CType(grdtrad.DataSource, DataTable)
                Dim dtgrd As DataTable
                Dim name(dt.Columns.Count) As String


                Dim dr As DataRow
                dtgrd = New DataTable
                With dtgrd.Columns

                    .Add("Security")
                    .Add("Delta", GetType(Double))
                    .Add("Vega", GetType(Double))
                    .Add("Theta", GetType(Double))
                    .Add("Total", GetType(Integer))
                    .Add("TodayMTM", GetType(Double))
                    .Add("Diff", GetType(Double))
                    .Add("GrossMTM", GetType(Double))

                End With

                Dim cal As DataRow
                dr = dtgrd.NewRow()
                For Each dr5 As DataRow In dt.Rows
                    cal = dtgrd.NewRow()

                    cal("Security") = dr5("Company")
                    cal("Delta") = dr5("Delta")
                    cal("Vega") = dr5("Vega")
                    cal("Theta") = dr5("Theta")
                    cal("Total") = dr5("Total")
                    cal("TodayMTM") = dr5("TodayMTM")
                    cal("Diff") = dr5("Diff")
                    cal("GrossMTM") = dr5("curGrossMTM")

                    dtgrd.Rows.Add(cal)

                    dtgrd.AcceptChanges()

                Next
                exporttocsv(dtgrd, savedi.FileName, "other")
                MsgBox("Export Successfully")
                OPEN_Export_File(savedi.FileName)
            End If
        End If
    End Sub

    Private Sub grdtrad_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtrad.CellContentClick

    End Sub

    Private Sub grdtrad_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtrad.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        Dim Dv As DataView
        Dim objPnlDet As New FrmPLDetail
        Dv = New DataView(dtSelect_PnL, "company ='" & grdtrad.Item("company", e.RowIndex).Value & "'", "script", DataViewRowState.CurrentRows)
        objPnlDet.ShowForm(Dv.ToTable, grdtrad.Item("company", e.RowIndex).Value)
    End Sub

    Private Sub FrmPLanalysis_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class