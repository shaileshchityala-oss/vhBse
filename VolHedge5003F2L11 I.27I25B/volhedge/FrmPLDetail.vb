Public Class FrmPLDetail
    Public Sub ShowForm(ByVal dt As DataTable, ByVal sCmpName As String)
        If dt.Rows.Count > 0 Then
            grdtrad.DataSource = dt
            grdtrad.Columns("company").Visible = False
            Me.Text = "Profit & Loss Detail [" & sCmpName & "]"
            Me.ShowDialog()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub FrmPLDetail_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If Asc(e.KeyChar) = 27 Then
            Me.Close()
        End If
    End Sub


End Class