Public Class ProcessRangeCalc
    Public Shared chkfindRange As Boolean
    Dim ClsLoginData1 As New ClsLoginData
    Dim Thr As System.Threading.Thread
    Public Delegate Sub GDelegate_ProcessData()
    Dim obj_DelProcessData As New GDelegate_ProcessData(AddressOf stopProcess)

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.xls)|*.xls"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtpath.Text = opfile.FileName
        End If
    End Sub

    Private Sub cmdprocess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdprocess.Click

        If txtpath.Text = "" Then
            MsgBox("Path not found!! Try Again!!")
            txtpath.Enabled = True
            txtpath.Focus()
            Exit Sub
        Else
            'ProgressBar1.Visible = True
            cmdprocess.Enabled = False
            Button1.Enabled = False
            Me.Cursor = Cursors.WaitCursor
            Thr = New System.Threading.Thread(AddressOf Process_Data)
            Thr.Name = "thr_rng_ProcData"
            Thr.Start()
            'Process_Data(dt)                    
        End If
    End Sub
    Private Function RemoveEmptyRowsFromDataTable(ByVal dt As DataTable) As DataTable
        For i As Integer = dt.Rows.Count - 1 To 0 Step -1
            If dt.Rows(i)(1).ToString() = "" Then
                dt.Rows(i).Delete()
            End If
        Next
        dt.AcceptChanges()
        Return dt
    End Function
    Public Sub Process_Data()
        Dim dt As New DataTable
        Try
            dt = ImportData(txtpath.Text)
            dt = RemoveEmptyRowsFromDataTable(dt)
            ClsLoginData1.Insert_Range_Data(dt)
            'Me.Close()  
            ClsLoginData1.update_local_Range_Data(dt)
            obj_DelProcessData.Invoke()
            MsgBox("Data Successfully Uploaded!!")
        Catch ex As Exception
        End Try
    End Sub

    Public Sub stopProcess()
        On Error Resume Next
        ' ProgressBar1.Visible = False
        txtpath.Enabled = True
        cmdprocess.Enabled = True
        Button1.Enabled = True
        'lblMessage.Text = "Data Downloaded Successfully!!"
        Me.Cursor = Cursors.Default
    End Sub

    Public Function ImportData(ByVal PrmPathExcelFile As String) As DataTable
        Dim dt1 As New DataTable
        Dim MyConnection As System.Data.OleDb.OleDbConnection

        Try
            ''''''' Fetch Data from Excel
            Dim DtSet As System.Data.DataSet
            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter

            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " & _
                            "data source='" & PrmPathExcelFile & " '; " & "Extended Properties=Excel 8.0;")
            ' Select the data from Sheet1 of the workbook.

            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [VolData$]", MyConnection)
            MyCommand.TableMappings.Add("Table", "VolData")
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)
            MyConnection.Close()
            dt1 = DtSet.Tables(0)

        Catch ex As Exception
        End Try

        Return dt1
    End Function

    Private Sub ProcessRangeCalc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        chkfindRange = True


        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ProcessRangeCalc_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        chkfindRange = False
    End Sub

    Private Sub ProcessRangeCalc_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        chkfindRange = False
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If txtPassword.Text = "fin123" Then
            Panel2.Visible = True
            Panel1.Visible = False
            cmdprocess.Visible = True
        Else
            MsgBox("Invalid Password! Try Again!")
            txtPassword.Text = ""
        End If
    End Sub

    Private Sub btnSubmit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnSubmit.KeyDown
        If e.KeyCode = Keys.Enter Then

        End If
    End Sub
End Class