Imports System.Data.OleDb
Imports System.IO
Public Class frmCurrencyExposureMargin
    Inherits System.Windows.Forms.Form
    Dim cmd As OleDbCommand

    Friend WithEvents grdexp As System.Windows.Forms.DataGridView
    Friend WithEvents compname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents expmag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents uid As System.Windows.Forms.DataGridViewTextBoxColumn

    Dim dread As OleDbDataReader
    Dim margin_table As New DataTable
    Dim objTrad As New trading
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    ' Friend WithEvents grddatabase As SourceGrid2.Grid
    Friend WithEvents txtcomp As System.Windows.Forms.TextBox
    Friend WithEvents txtexposure As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtcomp = New System.Windows.Forms.TextBox
        Me.txtexposure = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdexp = New System.Windows.Forms.DataGridView
        Me.compname = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.expmag = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.uid = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdexp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(192, 24)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Security name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(192, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(152, 24)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Exposure Margin (%)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtcomp
        '
        Me.txtcomp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcomp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcomp.Location = New System.Drawing.Point(0, 24)
        Me.txtcomp.Name = "txtcomp"
        Me.txtcomp.Size = New System.Drawing.Size(192, 22)
        Me.txtcomp.TabIndex = 1
        '
        'txtexposure
        '
        Me.txtcurrencyexposure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcurrencyexposure.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcurrencyexposure.Location = New System.Drawing.Point(192, 24)
        Me.txtcurrencyexposure.Name = "txtcurrencyexposure"
        Me.txtcurrencyexposure.Size = New System.Drawing.Size(152, 22)
        Me.txtcurrencyexposure.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(352, 24)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 24)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "&Save"
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(352, 56)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 24)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "E&xit"
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(352, 440)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(80, 24)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "&Import"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 464)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(440, 33)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Note :- While importing exposure margin from excel file, data must be included wi" & _
            "th heading of columns from HTML file."
        '
        'grdexp
        '
        Me.grdexp.AllowUserToAddRows = False
        Me.grdexp.AllowUserToDeleteRows = False
        Me.grdexp.AllowUserToResizeColumns = False
        Me.grdexp.AllowUserToResizeRows = False
        Me.grdexp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdexp.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.compname, Me.expmag, Me.uid})
        Me.grdexp.Location = New System.Drawing.Point(3, 49)
        Me.grdexp.Name = "grdexp"
        Me.grdexp.ReadOnly = True
        Me.grdexp.RowHeadersVisible = False
        Me.grdexp.Size = New System.Drawing.Size(341, 389)
        Me.grdexp.TabIndex = 10
        '
        'compname
        '
        Me.compname.DataPropertyName = "compname"
        Me.compname.HeaderText = "Security"
        Me.compname.Name = "compname"
        Me.compname.ReadOnly = True
        Me.compname.Width = 180
        '
        'expmag
        '
        Me.expmag.DataPropertyName = "exposure_margin"
        Me.expmag.HeaderText = "Exposure Margin (%)"
        Me.expmag.Name = "expmag"
        Me.expmag.ReadOnly = True
        Me.expmag.Width = 140
        '
        'uid
        '
        Me.uid.DataPropertyName = "uid"
        Me.uid.HeaderText = "uid"
        Me.uid.Name = "uid"
        Me.uid.ReadOnly = True
        Me.uid.Visible = False
        '
        'frm_exposure_margin_entry
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(457, 499)
        Me.Controls.Add(Me.grdexp)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtcomp)
        Me.Controls.Add(Me.txtexposure)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button3)
        Me.KeyPreview = True
        Me.Name = "frmCurrencyExposureMargin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Currency Exposure Margin"
        CType(Me.grdexp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private Sub frmCurrencyExposureMargin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        init_table()
        fill_grid()
    End Sub
    Private Sub init_table()
        margin_table = New DataTable
        With margin_table.Columns
            .Add("uid")
            .Add("compname")
            .Add("currency_exposure_margin")
        End With
    End Sub
    Private Sub fill_grid()
        margin_table.Rows.Clear()
        margin_table = objTrad.Currency_Exposure_margin
        grdexp.DataSource = margin_table
    End Sub
    Private Sub grdexp_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdexp.CellDoubleClick
        Dim compname As String
        compname = grdexp.Rows(e.RowIndex).Cells(1).Value
        For Each drow As DataRow In margin_table.Select("compname='" & compname & "'")
            ' MsgBox(drow("compname").ToString.Length)
            txtcomp.Text = drow("compname")
            txtcurrencyexposure.Text = drow("exposure_margin")
        Next
    End Sub

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

                MsgBox("Data saved.....")
            Else
                MsgBox("Fill all data")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
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
                Dim msrno As Integer
                Dim msymbol As String
                Dim mEM As String
                If tempdata.Rows.Count > 0 Then
                    For Each drow As DataRow In tempdata.Rows
                        msymbol = CStr(drow(1))
                        mEM = Val(drow(2))
                        objTrad.Insert_Exposure_margin(msymbol, Val(mEM))
                    Next
                    fill_grid()
                    MsgBox("Import Completed......", MsgBoxStyle.Information)
                Else
                    MsgBox("Import Failed......", MsgBoxStyle.Critical)
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
End Class