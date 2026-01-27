Imports System.Data.odbc
Imports System.IO
Imports System.Configuration
Public Class ResetPasssword

    Dim objUser As New user_master
    Dim objDel As New deletedata
    Dim objAna As New analysisprocess
    Dim objScenario As New scenarioDetail
    Dim flgPass As Boolean = False
    Dim dtUser As New DataTable
    Dim mResetPassword As Boolean

    Private Sub ResetPasssword_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
       
        Dim setting_name, setting_key As String
        Dim objTrad As New trading
        If mResetPassword = True Then
            'If txtlogid.Text <> "" And txtpass.Text <> "" Then
            For Each drow As DataRow In GdtSettings.Select("SettingName='LOGINID' or SettingName='LOGINPASSWD'", "")
                setting_name = ""
                setting_key = ""
                If drow("SettingName") = "LOGINID" Then
                    setting_name = "LOGINID"
                    setting_key = Encry(txtlogid.Text)
                    drow("SettingKey") = Encry(txtlogid.Text)
                    delete_data.txtlogid.Text = setting_key
                ElseIf drow("SettingName") = "LOGINPASSWD" Then
                    setting_name = "LOGINPASSWD"
                    setting_key = Encry(txtNewPass.Text)
                    drow("SettingKey") = Encry(txtNewPass.Text)
                    delete_data.txtpass.Text = setting_key
                End If
                If setting_name <> "" Then
                    objTrad.SettingName = setting_name
                    objTrad.SettingKey = setting_key
                    objTrad.Uid = CInt(drow("uid"))
                    objTrad.Update_setting()
                End If
            Next
            mResetPassword = False
            'End If
        End If
    End Sub
    Private Sub ResetPasssword_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtUser = objUser.Selectdata
        txtlogid.Text = Decry(GdtSettings.Compute("max(SettingKey)", "SettingName='LOGINID'").ToString)
        'txtpass.Text = Decry(GdtSettings.Compute("max(SettingKey)", "SettingName='LOGINPASSWD'").ToString)
    End Sub
    Private Sub ButResetPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButResetPassword.Click
        Try
            If txtlogid.Text = "" Or txtpass.Text = "" Or txtNewPass.Text = "" Or TxtConfirmPassword.Text = "" Then
                MsgBox("Please Enter Login Id , Password , New Password And Confirm Password!!", MsgBoxStyle.Exclamation)
            Else

                Dim pass As String
                pass = ""

                For Each drow As DataRow In objUser.Selectdata.Select("loginid = '" & Encry(txtlogid.Text) & "' and pass = '" & Encry(txtpass.Text) & "'")
                    pass = drow("pass")
                    Exit For
                Next
                If pass = "" Then
                    MsgBox("Password Not Match!!", MsgBoxStyle.Exclamation)
                    txtpass.Focus()
                    Exit Sub
                ElseIf txtNewPass.Text <> TxtConfirmPassword.Text Then
                    MsgBox("New Password And Confirm Password Not Match.", MsgBoxStyle.Information)
                    Exit Sub
                Else
                    'If MsgBox("Are you sure Want To Reset Password", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    '    Exit Sub
                    'End If
                    If txtNewPass.Text.Trim <> "" Then
                        'objUser.Loginid=
                        objUser.Update(Encry(txtlogid.Text), Encry(txtNewPass.Text))
                    End If

                    'backup old database
                    ' '' ''Dim str_folder_path As String
                    ' '' ''str_folder_path = System.Windows.Forms.Application.StartupPath() & "\backup_" & Format(Now(), "ddMMyy")
                    ' '' ''If Not Directory.Exists(str_folder_path) Then
                    ' '' ''    Directory.CreateDirectory(str_folder_path)
                    ' '' ''End If
                    ' '' ''Dim str As String = ConfigurationSettings.AppSettings("dbname")
                    ' '' ''Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str

                    ' '' ''Dim cur_date_str As String
                    ' '' ''cur_date_str = Format(Now, "ddMMMyyyy_HHmm")
                    ' '' ''Dim tstr As String = str_folder_path & "\greek_backup_" & cur_date_str & ".mdb"

                    ' '' ''FileCopy(_connection_string, tstr)
                    '============================end of backup

                    MsgBox("Password Reset Successfully.", vbInformation)
                    mResetPassword = True
                End If

               
                'hashOrder.Clear()
                txtlogid.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class