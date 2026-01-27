Public Class FrmMargin

    Private Sub ShowForm(ByVal DouIntMrg As Double, ByVal DouExpMrg As Double, ByVal DouEquity As Double, ByVal sMrg As String, ByVal mLeft As Long, ByVal mTop As Long, ByVal obj As Object)
        'txtintmrg.Text = Format(DouIntMrg, inmargstr)
        'txtexpmrg.Text = Format(DouExpMrg, exmargstr)
        'txtequity.Text = Format(DouEquity, equitystr)
        Me.Text = "Margin [" & sMrg & "]"
        Me.Left = mLeft
        Me.Top = mTop
        Me.Show(obj)
        Me.Focus()
    End Sub

    Private Sub txtintmrg_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If Val(txtintmrg.Text) > 0 Then
        '    txtintmrg.BackColor = Color.FromArgb(0, 64, 0)
        'ElseIf Val(txtintmrg.Text) < 0 Then
        '    txtintmrg.BackColor = Color.FromArgb(64, 0, 0)
        'Else
        '    txtintmrg.BackColor = Color.Black
        'End If
    End Sub

    Private Sub txtexpmrg_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If Val(txtexpmrg.Text) > 0 Then
        '    txtexpmrg.BackColor = Color.FromArgb(0, 64, 0)
        'ElseIf Val(txtexpmrg.Text) < 0 Then
        '    txtexpmrg.BackColor = Color.FromArgb(64, 0, 0)
        'Else
        '    txtexpmrg.BackColor = Color.Black
        'End If
    End Sub

    Private Sub txtequity_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If Val(txtequity.Text) > 0 Then
        '    txtequity.BackColor = Color.FromArgb(0, 64, 0)
        'ElseIf Val(txtequity.Text) < 0 Then
        '    txtequity.BackColor = Color.FromArgb(64, 0, 0)
        'Else
        '    txtequity.BackColor = Color.Black
        'End If
    End Sub

    Private Sub FrmMargin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        MDI.ObjMargin = Nothing
    End Sub

    Private Sub FrmMargin_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave

    End Sub

    Private Sub FrmMargin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If Asc(e.KeyChar) = 27 Then
            Me.Close()
        End If
    End Sub


    Private Sub FrmMargin_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        'If e.X >= (Me.Width - 20) Or e.Y >= (Me.Height - 20) Or e.X <= 10 Or e.Y <= 10 Then
        '    Me.Close()
        'End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Close()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        'Me.Opacity = Me.Opacity - (5 / 100)
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Timer2.Enabled = True
        Timer2.Start()
        Timer3.Enabled = False
        Timer3.Stop()
    End Sub

    Private Sub FrmMargin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class