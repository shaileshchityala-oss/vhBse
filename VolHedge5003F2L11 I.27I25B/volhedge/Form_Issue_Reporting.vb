Imports VolHedge.DAL
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.HttpRequestHeader
Imports System.Net
Imports System.Text
Imports System.linq





Public Class Form_Issue_Reporting

    Private Const sp_insert_mail As String = "Sp_InsMailData"
    Private Sub Form_Issue_Reporting_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtclientname.Text = client_name
        txtmobileno.Text = client_mobile

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        'opfile.Filter = "JPEG Files|*.jpg"

        opfile.Filter = "PNG Files|*.png"

        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If txtpath1.Text = "" Then
                txtpath1.Text = opfile.FileName
            ElseIf txtpath2.Text = "" Then
                txtpath2.Text = opfile.FileName
            ElseIf txtpath3.Text = "" Then
                txtpath3.Text = opfile.FileName
            ElseIf txtpath4.Text = "" Then
                txtpath4.Text = opfile.FileName
            ElseIf txtpath5.Text = "" Then
                txtpath5.Text = opfile.FileName


            End If
        End If



        If picsnapshot.Image Is Nothing Then
            picsnapshot.Image = Image.FromFile(txtpath1.Text)
        ElseIf picbox2.Image Is Nothing Then
                picbox2.Image = Image.FromFile(txtpath2.Text)
            ElseIf picbox3.Image Is Nothing Then
                picbox3.Image = Image.FromFile(txtpath3.Text)

            ElseIf picbox4.Image Is Nothing Then
                picbox4.Image = Image.FromFile(txtpath4.Text)
            ElseIf picbox5.Image Is Nothing Then
                picbox5.Image = Image.FromFile(txtpath5.Text)
            End If



    End Sub



    Private Sub btnremove1_Click(sender As Object, e As EventArgs) Handles btnremove1.Click
        If picbox5.Image IsNot Nothing Then
            picbox5.Image = Nothing
            txtpath5.Text = ""
        ElseIf picbox4.Image IsNot Nothing Then
            txtpath4.Text = ""
            picbox4.Image = Nothing
        ElseIf picbox3.Image IsNot Nothing Then
            txtpath3.Text = ""
            picbox3.Image = Nothing
        ElseIf picbox2.Image IsNot Nothing Then
            picbox2.Image = Nothing
            txtpath2.Text = ""
        ElseIf picsnapshot.Image IsNot Nothing Then
            picsnapshot.Image = Nothing
            txtpath1.Text = ""

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Try
            Dim imageBytes1 As Byte()
            Dim imageBytes2 As Byte()
            Dim imageBytes3 As Byte()
            Dim imageBytes4 As Byte()
            Dim imageBytes5 As Byte()

            If Not txtpath1.Text = "" Then
                imageBytes1 = File.ReadAllBytes(txtpath1.Text)

            Else
                imageBytes1 = New Byte() {}
            End If

            If Not txtpath2.Text = "" Then
                imageBytes2 = File.ReadAllBytes(txtpath2.Text)
            Else
                imageBytes2 = New Byte() {}
            End If
            If Not txtpath3.Text = "" Then
                imageBytes3 = File.ReadAllBytes(txtpath3.Text)
            Else
                imageBytes3 = New Byte() {}
            End If
            If Not txtpath4.Text = "" Then
                imageBytes4 = File.ReadAllBytes(txtpath4.Text)
            Else
                imageBytes4 = New Byte() {}
            End If
            If Not txtpath5.Text = "" Then
                imageBytes5 = File.ReadAllBytes(txtpath5.Text)
            Else
                imageBytes5 = New Byte() {}
            End If


            Dim str As String
            Dim destinationPath As String = "https://support.finideas.com/img/"


            Str = ""
            Str = "<!DOCTYPE HTML>" & vbCrLf
            str = str & "<html lang='en-US'>" & vbCrLf
            str = str & "<head>" & vbCrLf
            str = str & "<meta charset='UTF-8'>" & vbCrLf
            str = str & "<title></title>" & vbCrLf
            str = str & "</head>" & vbCrLf
            str = str & "<body>" & vbCrLf
            str = str & "<p><b>===Issue Reporting Detail====</b></p>" & vbCrLf


            str = str & "<p>dear Recipient,<br/> </p>" & vbCrLf
            str = str & "<p>I hope this email finds you well. I would like to report an issue that I have encountered while using VolHedge. Below are the details of the issue:</p>" & vbCrLf
            str = str & "<p> " & Trim(txtcomment.Text) & "<br/> </p>" & vbCrLf
            str = str & "<p>Client Name : <b>" & Trim(txtclientname.Text) & "</b><br/> </p>" & vbCrLf
            str = str & "<p>Mobile Number :<b> " & Trim(txtmobileno.Text) & "</b><br/> </p>" & vbCrLf

            'str = str & "<p>Best regards,<br/> </p>" & vbCrLf
            'str = str & "<p>FinIdeas Investment Advisor PVT. LTD<br/> </p>" & vbCrLf
            str = str & "</body>" & vbCrLf
            str = str & "</html>" & vbCrLf


            DA_SQL.ParamClear()
            DA_SQL.AddParam("@Attachment1", SqlDbType.VarBinary, imageBytes1.Length + 10, imageBytes1)
            DA_SQL.AddParam("@Attachment2", SqlDbType.VarBinary, imageBytes2.Length + 10, imageBytes2)
            DA_SQL.AddParam("@Attachment3", SqlDbType.VarBinary, imageBytes3.Length + 10, imageBytes3)
            DA_SQL.AddParam("@Attachment4", SqlDbType.VarBinary, imageBytes4.Length + 10, imageBytes4)
            DA_SQL.AddParam("@Attachment5", SqlDbType.VarBinary, imageBytes5.Length + 10, imageBytes5)
            DA_SQL.AddParam("@Body", SqlDbType.NVarChar, 10000, str.ToString)
            DA_SQL.Cmd_Text = "dbo.Sp_Ins_issuedata"
            DA_SQL.ExecuteNonQuery()
            MsgBox("Issue Reported..")







            picsnapshot.Image = Nothing
            picbox2.Image = Nothing
            picbox3.Image = Nothing
            picbox4.Image = Nothing
            picbox5.Image = Nothing
            txtpath1.Text = ""
            txtpath2.Text = ""
            txtpath3.Text = ""
            txtpath4.Text = ""
            txtpath5.Text = ""
            txtcomment.Text = ""


        Catch ex As Exception
            MsgBox("Issue Not Reported..")
        End Try




    End Sub

    Public Sub UploadImage(ByVal imagePath As String, ByVal serverUrl As String)
        Try
            Using client As New WebClient()
                ' Upload the image file to the server
                client.UploadFile(serverUrl, imagePath)
            End Using
        Catch ex As Exception
            Console.WriteLine("Error: {ex.Message}")
        End Try
    End Sub


End Class