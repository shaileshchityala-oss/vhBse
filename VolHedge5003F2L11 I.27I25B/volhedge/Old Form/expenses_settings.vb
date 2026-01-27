Public Class expenses_settings
    Dim objExp As New expenses
    Private Sub txtndbl_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtndbl.KeyPress
        numonly(e)
    End Sub

    Private Sub txtndbs_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtndbs.KeyPress
        numonly(e)
    End Sub

    Private Sub txtndblp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtndblp.KeyPress
        numonly(e)
    End Sub

    Private Sub txtndbsp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtndbsp.KeyPress
        numonly(e)
    End Sub

    Private Sub txtdbl_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdbl.KeyPress
        numonly(e)
    End Sub

    Private Sub txtdbs_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdbs.KeyPress
        numonly(e)
    End Sub

    Private Sub txtdblp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdblp.KeyPress
        numonly(e)
    End Sub

    Private Sub txtdbsp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdbsp.KeyPress
        numonly(e)
    End Sub

    Private Sub txtfutl_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfutl.KeyPress
        numonly(e)
    End Sub

    Private Sub txtfuts_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfuts.KeyPress
        numonly(e)
    End Sub

    Private Sub txtfutlp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfutlp.KeyPress
        numonly(e)
    End Sub

    Private Sub txtfutsp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfutsp.KeyPress
        numonly(e)
    End Sub

    Private Sub txtspl_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtspl.KeyPress
        numonly(e)
    End Sub

    Private Sub txtsplp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsplp.KeyPress
        numonly(e)
    End Sub

    Private Sub txtsps_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsps.KeyPress
        numonly(e)
    End Sub

    
    Private Sub txtspsp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtspsp.KeyPress
        numonly(e)
    End Sub

    Private Sub txtpl_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpl.KeyPress
        numonly(e)
    End Sub

    
    Private Sub txtps_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtps.KeyPress
        numonly(e)
    End Sub

    Private Sub txtpsp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpsp.KeyPress
        numonly(e)
    End Sub

    Private Sub txtplp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtplp.KeyPress
        numonly(e)
    End Sub

    Private Sub txtineq_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtineq.KeyPress
        numonly(e)
    End Sub

    Private Sub txtinfo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtinfo.KeyPress
        numonly(e)
    End Sub

    Private Sub txtndbl_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtndbl.Validating
        If txtndbl.Text.Trim = "" Then
            txtndbl.Text = 0
        End If
    End Sub

    Private Sub txtndbs_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtndbs.Validating
        If txtndbs.Text.Trim = "" Then
            txtndbs.Text = 0
        End If
    End Sub

    Private Sub txtndblp_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtndblp.Validating
        If txtndblp.Text.Trim = "" Then
            txtndblp.Text = 0
        End If
    End Sub

    Private Sub txtndbsp_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtndbsp.Validating
        If txtndbsp.Text.Trim = "" Then
            txtndbsp.Text = 0
        End If
    End Sub

    Private Sub txtdbl_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtdbl.Validating
        If txtdbl.Text.Trim = "" Then
            txtdbl.Text = 0
        End If
    End Sub

    Private Sub txtdbs_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtdbs.Validating
        If txtdbs.Text.Trim = "" Then
            txtdbs.Text = 0
        End If
    End Sub

    Private Sub txtdblp_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtdblp.Validating
        If txtdblp.Text.Trim = "" Then
            txtdblp.Text = 0
        End If
    End Sub

    Private Sub txtdbsp_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtdbsp.Validating
        If txtdbsp.Text.Trim = "" Then
            txtdbsp.Text = 0
        End If
    End Sub

    Private Sub txtfutl_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtfutl.Validating
        If txtfutl.Text.Trim = "" Then
            txtfutl.Text = 0
        End If
    End Sub

    Private Sub txtfuts_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtfuts.Validating
        If txtfuts.Text.Trim = "" Then
            txtfuts.Text = 0
        End If
    End Sub

    Private Sub txtfutlp_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtfutlp.Validating
        If txtfutlp.Text.Trim = "" Then
            txtfutlp.Text = 0
        End If
    End Sub

    Private Sub txtfutsp_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtfutsp.Validating
        If txtfutsp.Text.Trim = "" Then
            txtfutsp.Text = 0
        End If
    End Sub

    Private Sub txtspl_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtspl.Validating
        If txtspl.Text.Trim = "" Then
            txtspl.Text = 0
        End If
    End Sub

    Private Sub txtsps_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtsps.Validating
        If txtsps.Text.Trim = "" Then
            txtsps.Text = 0
        End If
    End Sub

    Private Sub txtsplp_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtsplp.Validating
        If txtsplp.Text.Trim = "" Then
            txtsplp.Text = 0
        End If
    End Sub

    Private Sub txtspsp_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtspsp.Validating
        If txtspsp.Text.Trim = "" Then
            txtspsp.Text = 0
        End If
    End Sub

    Private Sub txtpl_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtpl.Validating
        If txtpl.Text.Trim = "" Then
            txtpl.Text = 0
        End If
    End Sub

    Private Sub txtps_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtps.Validating
        If txtps.Text.Trim = "" Then
            txtps.Text = 0
        End If
    End Sub

    Private Sub txtplp_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtplp.Validating
        If txtplp.Text.Trim = "" Then
            txtplp.Text = 0
        End If
    End Sub

    Private Sub txtpsp_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtpsp.Validating
        If txtpsp.Text.Trim = "" Then
            txtpsp.Text = 0
        End If
    End Sub

    Private Sub txtineq_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtineq.Validating
        If txtineq.Text.Trim = "" Then
            txtineq.Text = 0
        End If
    End Sub

    Private Sub txtinfo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtinfo.Validating
        If txtinfo.Text.Trim = "" Then
            txtinfo.Text = 0
        End If
    End Sub

    Private Sub cmdupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdupdate.Click
        objExp.NDBL = val(txtndbl.Text)
        objExp.NDBLP = val(txtndblp.Text)
        objExp.NDBS = val(txtndbs.Text)
        objExp.NDBSP = val(txtndbsp.Text)

        objExp.DBL = val(txtdbl.Text)
        objExp.DBLP = val(txtdblp.Text)
        objExp.DBS = val(txtdbs.Text)
        objExp.DBSP = val(txtdbsp.Text)

        objExp.FUTL = val(txtfutl.Text)
        objExp.FUTLP = val(txtfutlp.Text)
        objExp.FUTS = val(txtfuts.Text)
        objExp.FUTSP = val(txtfutsp.Text)

        objExp.SPL = val(txtspl.Text)
        objExp.SPLP = val(txtsplp.Text)
        objExp.SPS = val(txtsps.Text)
        objExp.SPSP = val(txtspsp.Text)

        objExp.PL = val(txtpl.Text)
        objExp.PLP = val(txtplp.Text)
        objExp.PS = val(txtps.Text)
        objExp.PSP = val(txtpsp.Text)

        objExp.EQUITY = val(txtineq.Text)
        objExp.FO = val(txtinfo.Text)

        objExp.update()

        MsgBox("Data Update Successfully")
        fill_expense()
    End Sub

    Private Sub expenses_settings_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub

    Private Sub expenses_settings_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'Dim md As New MDI
        'For i As Integer = 0 To md.MdiChildren.Length - 1
        '    If UCase(md.MdiChildren(i).Name) <> "CONTACT" Then
        '        md.MdiChildren(i).Activate()
        '        'Me.MdiChildren(i).WindowState = Me.MdiChildren(i).WindowState
        '    End If
        'Next
    End Sub

    Private Sub expenses_settings_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub expenses_settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim tempdata As New DataTable
        tempdata = objExp.Select_Expenses
        For Each drow As DataRow In tempdata.Rows
            txtndbl.Text = val(drow("ndbl"))
            txtndblp.Text = val(drow("ndblp"))
            txtndbs.Text = val(drow("ndbs"))
            txtndbsp.Text = val(drow("ndbsp"))

            txtdbl.Text = val(drow("dbl"))
            txtdblp.Text = val(drow("dblp"))
            txtdbs.Text = val(drow("dbs"))
            txtdbsp.Text = val(drow("dbsp"))

            txtfutl.Text = val(drow("futl"))
            txtfutlp.Text = val(drow("futlp"))
            txtfuts.Text = val(drow("futs"))
            txtfutsp.Text = val(drow("futsp"))

            txtspl.Text = val(drow("spl"))
            txtsplp.Text = val(drow("splp"))
            txtsps.Text = val(drow("sps"))
            txtspsp.Text = val(drow("spsp"))


            txtpl.Text = val(drow("prel"))
            txtplp.Text = val(drow("prelp"))
            txtps.Text = val(drow("pres"))
            txtpsp.Text = val(drow("presp"))

            If val(txtspl.Text) = 0 Then
                RadioButton2.Checked = True
            Else
                RadioButton1.Checked = True
            End If
            txtineq.Text = Val(drow("equity"))
            txtinfo.Text = Val(drow("fo"))

        Next
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            txtpl.Text = 0
            txtps.Text = 0
            GroupBox8.Enabled = True
            GroupBox7.Enabled = False
        Else
            txtspl.Text = 0
            txtsps.Text = 0

            GroupBox8.Enabled = False
            GroupBox7.Enabled = True
        End If
    End Sub

    Private Sub expenses_settings_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged

    End Sub
End Class