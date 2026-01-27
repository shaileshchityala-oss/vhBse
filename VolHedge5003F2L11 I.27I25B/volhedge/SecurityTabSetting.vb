Imports System.Data.odbc

Public Class SecurityTabSetting
    Inherits System.Windows.Forms.Form

    Dim objUser As New user_master
    Dim objDel As New deletedata
    Dim objAna As New analysisprocess
    Public Shared chkSecutyTabDisplay As Boolean

    Private Sub SecurityTabSetting_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        chkSecutyTabDisplay = False
    End Sub
    Private Sub SecurityTabSetting_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub frmusermaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fill_company()
        fill_company_summary()
        Me.WindowState = FormWindowState.Normal
        Me.Refresh()
    End Sub
    Private Sub fill_company()
        Dim comptable As New DataTable
        comptable = objDel.Comapany
        chklist.Items.Clear()

        For Each drow As DataRow In comptable.Rows
            chklist.Items.Add(drow("company"), CBool(drow("isdisplay")))
        Next
        
    End Sub
    Private Sub fill_company_summary()
        Dim comptable As New DataTable
        comptable = objDel.Comapany_summary
        chkListSummery.Items.Clear()
        For Each drow As DataRow In comptable.Rows
            chkListSummery.Items.Add(drow("company"), CBool(drow("issummary")))
        Next
    End Sub
    Private Sub chkall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkall.CheckedChanged
        If chkall.Checked = True Then
            For i As Integer = 0 To chklist.Items.Count - 1
                chklist.SetItemChecked(i, True)
            Next
        Else
            For i As Integer = 0 To chklist.Items.Count - 1
                chklist.SetItemChecked(i, False)
            Next
        End If

    End Sub

    Private Sub cmdcomp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcomp.Click
        Try
            'If txtlogid.Text = "" Or txtpass.Text = "" Or txtcpass.Text = "" Or txtopass.Text = "" Then

            If chklist.CheckedItems.Count > 0 Then
                objDel.update_Comapany(chklist)

            End If
            fill_company()
            MsgBox("Successfully Saved")
        Catch ex As Exception

            MsgBox(ex.ToString)
        End Try
    End Sub

    
    Private Sub chkAll1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll1.CheckedChanged
        If chkAll1.Checked = True Then
            For i As Integer = 0 To chkListSummery.Items.Count - 1
                chkListSummery.SetItemChecked(i, True)
            Next
        Else
            For i As Integer = 0 To chkListSummery.Items.Count - 1
                chkListSummery.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If chkListSummery.CheckedItems.Count > 0 Then
                objDel.update_Comapany_sammary(chkListSummery)
            End If
            fill_company_summary()
            MsgBox("Successfully Saved")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

   
End Class