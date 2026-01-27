
Public Class FrmUserDefScenario
    Dim ObjUserDefTag As New UserDefScenario
    Dim dtSymbol As DataTable

    Public Function ShowForm(ByVal sTag As String) As UserDefScenario
        ObjUserDefTag.sTagName = sTag
        ObjUserDefTag.bIsValid = False
        TxtTabName.Text = ObjUserDefTag.sTagName
        Me.ShowDialog()
        Return ObjUserDefTag
    End Function

    Private Sub FrmUserDefScenario_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        TxtTabName.Focus()
    End Sub

    Private Sub FrmUserDefTag_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ObjUserDefTag.bIsValid = False
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            Call BtnApply_Click(BtnApply, New System.EventArgs)
        End If
    End Sub

    Private Sub FrmUserDefTag_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Private Sub BtnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnApply.Click

        If TxtTabName.Text.Trim = "" Then
            MsgBox("Invalid Tab Name.", MsgBoxStyle.Information)
            TxtTabName.Focus()
            Return
        End If


        ObjUserDefTag.sTagName = TxtTabName.Text.Trim
        ObjUserDefTag.bIsValid = True
        MsgBox("Applied Successfully..", MsgBoxStyle.Information)
        Me.Close()
    End Sub

    Private Sub FrmUserDefScenario_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub
End Class