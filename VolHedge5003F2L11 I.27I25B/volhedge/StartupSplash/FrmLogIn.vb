Public Class FrmLogIn
    Dim DtUserMaster As DataTable
    Dim ObjLoginData As New ClsLoginData
    Public Function ShowForm() As Boolean
        Me.ShowDialog()
        'Return GIsUser_Admin
    End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If MsgBox("Are you sure to Exit From Application ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If
        Me.Close()
    End Sub
    Private Function CheckValidate() As Boolean

        '    If UCase(txtUserName.Text) <> UCase(Decry(G_DTSettingData.Select("SettingName='LOGIN USER NAME'")(0).Item("SettingKey"))) Then
        '        MsgBox("Please enter valid user name & Password !!", MsgBoxStyle.Exclamation)
        '        txtUserName.Focus()
        '        Exit Function
        '    End If

        '    If txtPassword.Text <> Decry(G_DTSettingData.Select("SettingName='LOGIN PASSWORD'")(0).Item("SettingKey")) Then
        '        MsgBox("Please enter valid user name & Password !!", MsgBoxStyle.Exclamation)
        '        txtUserName.Focus()
        '        Exit Function
        '    End If

        
        If txtUserName.Text <> "" And txtPassword.Text <> "" Then
            ObjLoginData.Userid = txtUserName.Text
            ObjLoginData.pwd = txtPassword.Text
            DtUserMaster = ObjLoginData.Select_User_Master(ObjLoginData._Userid, True)
            If DtUserMaster.Rows.Count > 0 Then
                If DtUserMaster.Rows(0)("F3").ToString <> ObjLoginData._pwd Then
                    MsgBox("Invalid Password", MsgBoxStyle.Information)
                    txtPassword.Focus()
                    Return False
                    Exit Function
                Else

                    GVar_LogIn_User = ObjLoginData.Userid
                    Try
                        'GIsUser_Admin = CBool(clsEnDe.FDec(DtUserMaster.Rows(0)("F11").ToString))
                    Catch ex As Exception
                        MsgBox("invalid Value in isAdmin Flsg", MsgBoxStyle.Information)
                    End Try

                End If
            Else
                MsgBox("Invalid User Name", MsgBoxStyle.Information)
                Return False
                Exit Function
            End If
        Else
            MsgBox("Enter User Name And Password", MsgBoxStyle.Information)
            txtUserName.Focus()
            Return False
            Exit Function
        End If
        Return True
    End Function
    Private Sub btnLogIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogIn.Click
        WriteLog("Login try by " & txtUserName.Text)
        If CheckValidate() = False Then
            WriteLog("Login fail by " & txtUserName.Text & "")
            Exit Sub
        End If
        'If GIsUser_Admin = True Then
        '    WriteLog("Login successes by " & txtUserName.Text & " as Admin")
        '    Call MainMenuEnableDisable(True)
        'Else
        WriteLog("Login successes by " & txtUserName.Text & "")
        Call MainMenuEnableDisable(False)
        'End If

        Me.Close()
    End Sub

    Private Sub FrmLogIn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Call btnExit_Click(sender, e)
        End If
    End Sub
    Private Sub MainMenuEnableDisable(ByVal Flg As Boolean)
        'MDI.GroupBox1.Enabled = Flg
    End Sub

End Class