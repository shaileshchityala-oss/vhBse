Imports System.Data.OleDb
Imports System.IO
Public Class frmCurrencyExposureMargin
    Inherits System.Windows.Forms.Form
    Dim cmd As OleDbCommand

    Dim dread As OleDbDataReader
    Dim margin_table As New DataTable
    Dim objTrad As New trading

    ''' <summary>
    ''' on escap key it close the form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCurrencyExposureMargin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Call Initialize table methode init_table 
    ''' Call fill_grid Methode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCurrencyExposureMargin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        init_table()
        fill_grid()
    End Sub
    ''' <summary>
    ''' Initialize Data table
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub init_table()
        margin_table = New DataTable
        With margin_table.Columns
            .Add("uid")
            .Add("compname")
            .Add("currency_exposure_margin")
        End With
    End Sub
    ''' <summary>
    ''' Fill Margin_table from database
    ''' and this Margin_table assign to grid view data source
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fill_grid()
        margin_table.Rows.Clear()
        margin_table = objTrad.Select_Currency_Exposure_margin
        grdexp.DataSource = margin_table
    End Sub
    ''' <summary>
    '''  this double click of retrive company name and exposure margin value on the text box respectivly
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdexp_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdexp.CellDoubleClick
        Dim compname As String
        compname = grdexp.Rows(e.RowIndex).Cells(1).Value
        For Each drow As DataRow In margin_table.Select("compname='" & compname & "'")
            ' MsgBox(drow("compname").ToString.Length)
            txtcomp.Text = drow("compname")
            txtcurrencyexposure.Text = drow("exposure_margin")
        Next
    End Sub

    ''' <summary>
    ''' it checks validation first if it returns true then
    ''' selected company checked true and update its value which is given Company Text box and its value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If validate_data() = True Then
                Dim chk As Boolean = False
                For Each mrow As DataRow In margin_table.Select("compname='" & txtcomp.Text.Trim & "'")
                    chk = True
                    Exit For
                Next
                If chk = False Then
                    objTrad.Insert_Currency_Exposure_margin(txtcomp.Text.Trim, Val(txtcurrencyexposure.Text))
                Else
                    objTrad.update_Exposure_margin(txtcomp.Text.Trim, Val(txtcurrencyexposure.Text))
                End If
                'strquery = "select * from exposure_margin where compname='" & txtcomp.Text.Trim & "'"
                'cmd = New OleDbCommand(strquery, con)
                'dread = cmd.ExecuteReader
                'If dread.Read Then
                '    strquery = "update exposure_margin set exposure_margin=" & txtexposure.Text.Trim & " where compname='" & txtcomp.Text.Trim & "'"
                'Else
                '    strquery = "insert into exposure_margin (compname,exposure_margin) values ('" & txtcomp.Text.Trim & "'," & txtexposure.Text.Trim & ")"
                'End If
                'dread.Close()
                'cmd.Dispose()
                'dread = Nothing

                'cmd = New OleDbCommand(strquery, con)
                'cmd.ExecuteNonQuery()
                'cmd.Dispose()

                'FillTblFrmDB()

                'fill_griddatabase()
                fill_grid()
                txtcomp.Text = ""
                txtcurrencyexposure.Text = ""

                MsgBox("Data saved.")
            Else
                MsgBox("Fill all data.")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' its a validation Function which check validation for company text box text and exposure Margin text box
    ''' it one of them get blank then it returns false and cursor exit function
    ''' otherwise returns true
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function validate_data() As Boolean
        If txtcomp.Text = "" Then
            txtcomp.Focus()
            validate_data = False
            Exit Function
        End If
        If txtcurrencyexposure.Text = "" Then
            txtcurrencyexposure.Focus()
            validate_data = False
            Exit Function
        End If
        validate_data = True
    End Function

    ''' <summary>
    ''' This button click export data to datagrid view from excel file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        OpenFileDialog1.FileName = "*.xls"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Try
                Dim tempdata As New DataTable
                Dim fpath As String
                fpath = OpenFileDialog1.FileName
                ' Dim fi As New FileInfo(fpath)
                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" & fpath
                Dim objConn As New OleDbConnection(sConnectionString)
                objConn.Open()

                Dim objCmdSelect As New OleDbCommand("SELECT * FROM [sheet1$] where [company name] <> ''", objConn)
                'Dim objCmdSelect As New OleDbCommand("SELECT strike,option_type,Exp_Date,Company,Qty,Rate,Instrument,entrydate FROM " & fi.Name, objConn)
                Dim objAdapter1 As New OleDbDataAdapter
                objAdapter1.SelectCommand = objCmdSelect
                objAdapter1.Fill(tempdata)
                objConn.Close()
                'Dim msrno As Integer
                Dim msymbol As String
                Dim mEM As String
                If tempdata.Rows.Count > 0 Then
                    For Each drow As DataRow In tempdata.Rows
                        msymbol = CStr(drow(1))
                        mEM = Val(drow(2))
                        If margin_table.Select("compname='" & msymbol & "'").Length > 0 Then
                            objTrad.delete_Cuurency_Exposure_margin(msymbol)
                        End If
                        objTrad.Insert_Currency_Exposure_margin(msymbol, Val(mEM))
                    Next
                    fill_grid()
                    MsgBox("Import Completed.", MsgBoxStyle.Information)
                Else
                    MsgBox("Import Failed.", MsgBoxStyle.Critical)
                End If
                'Dim rs As New ADODB.Recordset

                'Try
                '    '//get excel record in recordset
                '    rs.CursorLocation = ADODB.CursorLocationEnum.adUseClient
                '    'MsgBox("Provider=MSDASQL;Driver={Microsoft Excel Driver (*.xls)};DBQ=" & txtarbitrage_file_path.Text)
                '    rs.Open("[sheet1$]", "Provider=MSDASQL;Driver={Microsoft Excel Driver (*.xls)};DBQ=" & OpenFileDialog1.FileName)     'connection String For excell

                '    '''Fetching Data And Segrigate it in Sheet1

                '    'Dim mStartRow As Integer
                '    Dim msrno As Integer
                '    Dim msymbol As String
                '    Dim mEM As String
                '    'Dim strquery As String

                '    'If CStr(rs(0).Value) <> "1" Then
                '    '    rs.MoveNext()
                '    'End If

                '    objTrad.delete_Exposure_margin()


                '    msrno = 1
                '    Try
                '        While Not rs.EOF
                '            If Not IsDBNull(rs(0).Value) Then
                '                If msrno.ToString = CStr(rs(0).Value) Then
                '                    msymbol = CStr(rs(1).Value)
                '                    mEM = val(rs(2).Value)
                '                    objTrad.Insert_Exposure_margin(msymbol, val(mEM))
                '                    'strquery = "insert into exposure_margin(compname,exposure_margin) values ('" & msymbol & "'," & mEM & ")"
                '                    'com = New OleDbCommand(strquery, con, tra)
                '                    'com.ExecuteNonQuery()

                '                    msrno += 1
                '                End If
                '            End If
                '            rs.MoveNext()
                '        End While

                '        rs.Close()
                '        rs = Nothing

                'If msrno > 1 Then
                '    fill_grid()
                '    MsgBox("Import Completed......", MsgBoxStyle.Information)
                'Else
                '    MsgBox("Import Failed......", MsgBoxStyle.Critical)
                'End If

                '    Catch ex As Exception

                '    rs.Close()
                '    rs = Nothing
                '    MsgBox("Import Failed......", MsgBoxStyle.Critical)

            Catch ex As Exception
                '
            End Try
        End If
    End Sub

    ''' <summary>
    ''' this double click of retrive company name and exposure margin value on the text box respectivly
    ''' and on delete key press it remove that selected record
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdexp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdexp.KeyDown
        If e.KeyCode = Keys.Delete Then
            Dim symbol As String
            If grdexp.SelectedRows.Count > 0 Then
                If MsgBox("Are you Sure to Delete Security?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                For i As Integer = 0 To grdexp.SelectedRows.Count - 1
                    symbol = grdexp.SelectedRows.Item(i).Cells(1).Value
                    objTrad.delete_Cuurency_Exposure_margin(symbol)
                Next
                fill_grid()
                MsgBox("Security Deleted Successfully.")
            End If
        End If
    End Sub

    Private Sub DeleteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        REM  Add Delete Button For Multiple Security at a time
        Try
            For Each Row As DataGridViewRow In grdexp.SelectedRows
                objTrad.Delete_Currency_Exposure_margin_Select(Row.Cells(1).Value.ToString.Trim, Val(Row.Cells(2).Value))
                grdexp.Rows.Remove(Row)
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class
