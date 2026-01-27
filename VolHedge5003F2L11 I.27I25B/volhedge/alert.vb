Public Class alert
    Public temptable As New DataTable
    Dim objAlt As New alertentry
    Public comp As String = ""
    Public fi As String = ""
    Public uid As Integer = 0

    Private Sub alert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'grdalert.DataSource = temptable
        System.Media.SystemSounds.Beep.Play()
        For Each drow As DataRow In temptable.Rows
            lblcompscript.Text = drow("comp_script")
            lblopt.Text = drow("opt")
            lblfield.Text = drow("field")
            lblv1.Text = drow("value1")
            lblv2.Text = drow("value2")
            lblcurr.Text = Math.Round(Val(drow("current")), 2)
        Next
        If UCase(lblopt.Text) = "BETWEEN" Then
            lblv2.Visible = True
            Label5.Visible = True
            Label11.Visible = True
        Else
            lblv2.Visible = False
            Label5.Visible = False
            Label11.Visible = False

        End If
        'Dim p As Point = New Point(450, 50)
        'Me.PointToScreen(p)
        'alertmsg = True
    End Sub

    Private Sub cmdyes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdyes.Click
        For Each drow As DataRow In alerttable.Select("comp_script='" & comp & "' and field ='" & fi & "'")
            drow("status") = 0
            drow(fi) = 0
        Next
        Me.Close()
        'al.alertreturn(True, comp, fi)
        'alertmsg = False
    End Sub

    Private Sub cmdno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdno.Click
        objAlt.update(uid, CDate(DateAdd(DateInterval.Day, -1, Now.Date)).Date)
        For Each drow As DataRow In alerttable.Select("comp_script='" & comp & "' and field ='" & fi & "' and uid='" & uid & "'")
            drow("entrydate") = CDate(DateAdd(DateInterval.Day, -1, Now.Date)).Date
        Next
        Me.Close()
    End Sub
End Class