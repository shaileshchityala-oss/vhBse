Imports Microsoft.Win32
Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports VolHedge.DAL
Imports System.Data
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.IO
Imports System.Globalization
Public Class FrmUserRegister

    Dim DTUserMasterde As New DataTable
    Dim emailverification As New DataTable

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
    End Sub

    Private Sub BtnGetOTP_Click(sender As Object, e As EventArgs) Handles BtnGetOTP.Click
        If (txtUserId.Text.Trim().Length <= 0) Then

            MessageBox.Show("Invalid Email !")
            txtEmail.Focus()
            ' lblstatus.Visible = false
            Return

        End If
        DTUserMasterde = ObjLoginData.Select_Regesterd_User(False, clsUEnDe.FEnc(txtUserId.Text.Trim()))
        If (DTUserMasterde.Rows.Count > 0) Then
            MsgBox("Email is already Registered with us..!")
            Return
        End If
        Dim strOTP = generateOTP()
        ObjLoginData.PreOTP = strOTP
        ObjLoginData.Email = txtUserId.Text
        SendOtpEmailreg()
        lblstatus.Text = "OTP Sent to your email"
        lblstatus.ForeColor = Color.Green
    End Sub

    Function SendOtpEmailreg() As Boolean
        Dim Str As String

        Str = "<!DOCTYPE HTML>" & vbCrLf
        Str = Str & "<html lang='en-US'>" & vbCrLf
        Str = Str & "<head>" & vbCrLf
        Str = Str & "<meta charset='UTF-8'>" & vbCrLf
        Str = Str & "<title></title>" & vbCrLf
        Str = Str & "</head>" & vbCrLf
        Str = Str & "<body>" & vbCrLf
        Str = Str & "<p>Dear Customer, </p>" & vbCrLf
        Str = Str & "<p><B>FinIdeas VolHedge OTP is  :</B>" & ObjLoginData.PreOTP & " </p>" & vbCrLf
        Str = Str & "</body>" & vbCrLf
        Str = Str & "</html>" & vbCrLf
        Dim FLAG As Boolean
        Dim email As String = txtUserId.Text

        'clsGlobal.smtpUser = "Software@finideas.com"
        'clsGlobal.smtpPasword = "Software@finideas.com"

        FLAG = send_emailOTP(clsGlobal.smtpUser, clsGlobal.smtpPasword, email, "VolHedge OTP verification", Str)
        If FLAG = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function generateOTP() As String
        Dim alphabets As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim small_alphabets As String = "abcdefghijklmnopqrstuvwxyz"
        Dim numbers As String = "1234567890"

        Dim characters As String = numbers
        Dim alphanumeric As Boolean = False
        If alphanumeric = True Then
            characters += Convert.ToString(alphabets & small_alphabets) & numbers
        End If
        Dim length As Integer = 5 'Integer.Parse(ddlLength.SelectedItem.Value)
        Dim otp As String = String.Empty
        For i As Integer = 0 To length - 1
            Dim character As String = String.Empty
            Do
                Dim index As Integer = New Random().Next(0, characters.Length)
                character = characters.ToCharArray()(index).ToString()
            Loop While otp.IndexOf(character) <> -1
            otp += character
        Next
        Return otp
    End Function

    Function send_emailOTP(ByVal senderemail As String, ByVal senderpassword As String, ByVal receiveremail As String, ByVal subject As String, ByVal message As String) As Boolean

        Try
            Dim mail As New MailMessage()
            Dim SmtpServer As New SmtpClient("smtp.gmail.com")

            mail.From = New MailAddress(senderemail)
            mail.[To].Add(receiveremail)
            mail.Subject = subject
            mail.IsBodyHtml = True
            mail.Body = message

            SmtpServer.Port = 587
            SmtpServer.Credentials = New System.Net.NetworkCredential(senderemail, senderpassword)
            SmtpServer.EnableSsl = True

            SmtpServer.Send(mail)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub BtnVerifyReg_Click(sender As Object, e As EventArgs) Handles BtnVerify.Click
        If (ObjLoginData.PreOTP = onetimepass.Text) Then
            clsGlobal.SetEmailVerified("True")
            lblstatus.Visible = True
            lblstatus.Text = "Verified"
            lblstatus.ForeColor = Color.Green

        Else
            MessageBox.Show("Invalid OTP..")
            onetimepass.Focus()
            clsGlobal.SetEmailVerified("False")
            lblstatus.Visible = True
            lblstatus.Text = "Unverified"
            lblstatus.ForeColor = Color.Red
            Return
        End If
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtUserId.Text.Trim.Length <= 0 Or txtUserId.Text.Trim.ToUpper = "ADMIN" Then
            MsgBox("Invalid UserId!")
            txtUserId.Focus()
            Exit Sub
        End If

        If TxtPwd.Text.Trim.Length <= 0 Then
            MsgBox("Invalid Password!")
            TxtPwd.Focus()
            Exit Sub
        End If

        If TxtPwd.Text <> TxtPwdConfirm.Text Then
            MsgBox("Password Not Match With Confirm Password!")
            Exit Sub
        End If

        If txtEmail.Text.Trim.Length <= 0 Then
            MsgBox("Invalid Email !")
            txtEmail.Focus()
            Exit Sub
        End If

        If TxtMobNo.Text.Trim.Length <= 0 Then
            MsgBox("Invalid User Mobile No.!")
            TxtMobNo.Focus()
            Exit Sub
        End If

        If CheckValidation() = False Then
            Exit Sub
        End If
        loadregisterdata(sender, e)

    End Sub

    Public mMotherBoardSrno As String
    Public mHddSrno As String
    Public mProcessorSrno As String

    Private Function CheckValidation() As Boolean
        Return True
        DTUserMasterde = ObjLoginData.Select_User_Master_all(False, clsUEnDe.FEnc(txtEmail.Text), clsUEnDe.FEnc(mMotherBoardSrno), clsUEnDe.FEnc(mHddSrno), clsUEnDe.FEnc(mProcessorSrno))
        If DTUserMasterde.Select("F21='" & mMotherBoardSrno & "' And F22='" & mHddSrno & "' And F23='" & mProcessorSrno & "' And F6 ='" & gstr_ProductName & "'").Length > 0 Then 'F2='" & txtUserId.Text & "' And 
            MsgBox("Someone already use this PC.  !!!" & vbCrLf & "" & vbCrLf & "")
            WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
            txtEmail.Focus()
            Return False
            Exit Function
        End If

        Return True
    End Function

    Public Sub loadregisterdata(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SetData()
        ObjLoginData.Insert_User_Master()
        WriteLog("Save/Edit User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
    End Sub

    Private Sub SetData()
        REM Data transfer to data class
        ObjLoginData.Userid = txtUserId.Text
        ObjLoginData.pwd = TxtPwd.Text

        ObjLoginData.Username = TxtName.Text
        ObjLoginData.Address = TxtAddress.Text
        ObjLoginData.Mobile = TxtMobNo.Text
        ObjLoginData.Email = txtEmail.Text
        ObjLoginData.DOB = dtpDOBDate.Text

        ObjLoginData.Firm = TxtFirm.Text
        ObjLoginData.FirmAddress = TxtFirmAddress.Text
        ObjLoginData.FirmContactNo = TxtFirmContactNo.Text
        ObjLoginData.Reference = TxtReference.Text

        ObjLoginData.Product = gstr_ProductName
        ObjLoginData.Allowed = False 'Boolean.TrueString.ToString
        ObjLoginData.Limited = Boolean.TrueString.ToString
        ObjLoginData.ExDate = DateAdd(DateInterval.Day, 7, Now.Date).ToString("dd-MMM-yyyy")
        ObjLoginData.Status = "out"

        ObjLoginData.M = mMotherBoardSrno
        ObjLoginData.H = mHddSrno
        ObjLoginData.P = mProcessorSrno
        ObjLoginData.City = TxtCity.Text
    End Sub

End Class