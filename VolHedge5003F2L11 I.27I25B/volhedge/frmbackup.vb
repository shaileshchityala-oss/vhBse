Imports System.IO
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration

Public Class frmbackup
    Inherits System.Windows.Forms.Form
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
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdsave As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CmdBrowse As System.Windows.Forms.Button
    Friend WithEvents txtpath As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmbackup))
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.cmdsave = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtpath = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CmdBrowse = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.White
        Me.cmdexit.CausesValidation = False
        Me.cmdexit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdexit.Font = New System.Drawing.Font("Palatino Linotype", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.DarkRed
        Me.cmdexit.Image = CType(resources.GetObject("cmdexit.Image"), System.Drawing.Image)
        Me.cmdexit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdexit.Location = New System.Drawing.Point(325, 54)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(112, 32)
        Me.cmdexit.TabIndex = 3
        Me.cmdexit.Text = "  E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'cmdsave
        '
        Me.cmdsave.BackColor = System.Drawing.Color.White
        Me.cmdsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdsave.Font = New System.Drawing.Font("Palatino Linotype", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsave.ForeColor = System.Drawing.Color.DarkRed
        Me.cmdsave.Image = CType(resources.GetObject("cmdsave.Image"), System.Drawing.Image)
        Me.cmdsave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdsave.Location = New System.Drawing.Point(62, 54)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(112, 32)
        Me.cmdsave.TabIndex = 2
        Me.cmdsave.Text = "   &Backup"
        Me.cmdsave.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Palatino Linotype", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(0, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(501, 32)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Data Backup"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(168, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Path for backup folder on server : "
        Me.Label2.Visible = False
        '
        'txtpath
        '
        Me.txtpath.Location = New System.Drawing.Point(176, 31)
        Me.txtpath.Name = "txtpath"
        Me.txtpath.ReadOnly = True
        Me.txtpath.Size = New System.Drawing.Size(240, 20)
        Me.txtpath.TabIndex = 6
        Me.txtpath.Text = "E:\Timeattendance_backup"
        Me.txtpath.Visible = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Palatino Linotype", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.DarkRed
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(185, 54)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(118, 32)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "   &Restore"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'CmdBrowse
        '
        Me.CmdBrowse.Location = New System.Drawing.Point(422, 29)
        Me.CmdBrowse.Name = "CmdBrowse"
        Me.CmdBrowse.Size = New System.Drawing.Size(75, 23)
        Me.CmdBrowse.TabIndex = 8
        Me.CmdBrowse.Text = "&Browse"
        Me.CmdBrowse.UseVisualStyleBackColor = True
        Me.CmdBrowse.Visible = False
        '
        'frmbackup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.ClientSize = New System.Drawing.Size(502, 92)
        Me.ControlBox = False
        Me.Controls.Add(Me.CmdBrowse)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtpath)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdsave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmbackup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Dim dfile As File
    Dim str_folder_path As String
    'Dim ObjFSO As Object
    Dim str_newfolder As String
    Dim objTrad As trading = New trading
    REM For Database restore File Select Or Not
    Dim mBrowse As Boolean
    Dim mBackupPath As String
    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        'str_folder_path = txtpath.Text
        'ObjFSO = CreateObject("Scripting.FileSystemObject")

        Dim opfile As FolderBrowserDialog
        opfile = New FolderBrowserDialog
        'opfile.InitialDirectory = txtpath.Text
        'opfile.Filter = "Files(*.mdb)|*.mdb"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtpath.Text = opfile.SelectedPath
        End If
        str_folder_path = txtpath.Text

        Try

            If Not Directory.Exists(txtpath.Text) Then
                'Directory.CreateDirectory(txtpath.Text)
                MessageBox.Show("please Select Valid File..")
            End If
            Dim str As String = ConfigurationSettings.AppSettings("dbname")
            Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str

            'Dim conn As OleDb.OleDbConnection
            'conn = New OleDb.OleDbConnection(_connection_string)
            'Dim com As OleDb.OleDbCommand
            'Dim temp_str As String
            Dim cur_date_str As String
            cur_date_str = Format(Now, "ddMMMyyyy_HHmm")
            Dim tstr As String = str_folder_path & "\greek_backup_" & cur_date_str & ".mdb"

            FileCopy(_connection_string, tstr)

            'conn.Open()
            ' temp_str = "backup database brc2 to disk = '" & str_folder_path & "\timeattandance_backup_" & cur_date_str & ".bak'"
            'temp_str = "backup database Strategic to disk = '" & str_folder_path & "\greek_backup_" & cur_date_str & ".bak'"

            'com = New OleDb.OleDbCommand(temp_str, conn)
            'com.CommandType = CommandType.Text
            'com.CommandTimeout = 0
            'com.ExecuteNonQuery()
            'conn.Close()
            MsgBox("Backup Generation Completed.", MsgBoxStyle.Information)
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        'ObjFSO = Nothing

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'str_folder_path = txtpath.Text

        'Dim strpath As String = Format(File.GetLastAccessTime(str_folder_path), "ddMMMyyyy_HHmm")
        'Dim tstr As String = str_folder_path & "\greek_backup_" & strpath & ".mdb"

        REM Change For Database Restore Not Working

        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.InitialDirectory = txtpath.Text
        opfile.Filter = "Files(*.mdb)|*.mdb"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtpath.Text = opfile.FileName
            mBrowse = True
        End If

        If mBrowse = True Then
            Dim tstr As String = txtpath.Text
            Me.Cursor = Cursors.WaitCursor
            If File.Exists(tstr) Then
                Dim str As String = ConfigurationSettings.AppSettings("dbname")
                Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str
                FileCopy(tstr, _connection_string)

                Call fill_token()
                Call Rounddata()
                Call init_datatable() 'initaialize all global datatable of analysis
                Call fill_trades()
                'changes done by Nasima on 10th Aug
                Call fill_equity_dtable()
                Me.Cursor = Cursors.Default
                MsgBox("Database File : - " & txtpath.Text & " restored Successfully.", MsgBoxStyle.Information)
                txtpath.Text = mBackupPath
                mBrowse = False
            End If
        Else
            MsgBox("First Select File For Restore.", MsgBoxStyle.Information)
            CmdBrowse.Focus()
        End If
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub frmbackup_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub

    Private Sub frmbackup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Normal
        'txtpath.Text = CStr(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")), "D:\", objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")))
        mBackupPath = CStr(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")), "D:\", objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")))
        txtpath.Text = mBackupPath
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub
    Public Sub CompactDB()

        'CommandBars("Menu Bar"). _
        'Controls("Tools"). _
        'Controls("Database utilities"). _
        'Controls("Compact and repair database..."). _
        'accDoDefaultAction()

    End Sub

    Private Sub frmbackup_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub CmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdBrowse.Click
        REM For Restore Database File Selection
        'Dim opfile As OpenFileDialog
        'opfile = New OpenFileDialog
        'opfile.InitialDirectory = txtpath.Text
        'opfile.Filter = "Files(*.mdb)|*.mdb"
        'Dim opfile As FolderBrowserDialog
        'opfile = New FolderBrowserDialog

        'If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '    'txtpath.Text = opfile.FileName
        '    txtpath.Text = opfile.SelectedPath
        '    mBrowse = True
        'End If
    End Sub
End Class
