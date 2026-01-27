Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL
Public Class Version_
    Private Sub Version__Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Cmd As SqlClient.SqlCommand
        Dim Dr As SqlClient.SqlDataReader
        Try
            Dim dtdetails As DataTable
            Dim strqry As String
            strqry = "select * from Tbl_Version_Notes WHERE Type = 'Reguler'"


            data_access.ParamClear()
            data_access.Cmd_Text = strqry
            data_access.cmd_type = CommandType.Text
            dtdetails = data_access.FillList()
            data_access.cmd_type = CommandType.StoredProcedure




            Dim count As Integer = 0

            For Each drow As DataRow In dtdetails.Rows
                If count = 0 Then

                    lblversionname.Text = drow("version").ToString()
                    lblnotes.Text = drow("notes").ToString()
                    count = count + 1
                ElseIf count = 1 Then
                    lblvn2.Visible = True
                    lbln2.Visible = True
                    lblver2.Visible = True
                    lblnotes2.Visible = True

                    lblver2.Text = drow("version").ToString()
                    lblnotes2.Text = drow("notes").ToString()
                    count = count + 1

                ElseIf count = 2 Then
                    lblvn3.Visible = True
                    lbln3.Visible = True
                    lblver3.Visible = True
                    lblnotes3.Visible = True

                    lblver3.Text = drow("version").ToString()
                    lblnotes3.Text = drow("notes").ToString()
                    count = count + 1

                ElseIf count = 3 Then
                    lblvn4.Visible = True
                    lbln4.Visible = True
                    lblver4.Visible = True
                    lblnotes4.Visible = True

                    lblver4.Text = drow("version").ToString()
                    lblnotes4.Text = drow("notes").ToString()
                    count = count + 1

                ElseIf count = 4 Then
                    lblvn5.Visible = True
                    lbln5.Visible = True
                    lblver5.Visible = True
                    lblnotes5.Visible = True

                    lblver5.Text = drow("version").ToString()
                    lblnotes5.Text = drow("notes").ToString()
                    count = count + 1


                End If






            Next



        Catch ex As Exception

        End Try


    End Sub
End Class