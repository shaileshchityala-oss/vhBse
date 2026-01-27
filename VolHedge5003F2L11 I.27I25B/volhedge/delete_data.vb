Imports System.Data.odbc
Imports System.IO
Imports System.Configuration
Public Class delete_data
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
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtpass As System.Windows.Forms.TextBox
    Friend WithEvents txtlogid As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdtoday As System.Windows.Forms.Button
    Friend WithEvents dtp1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdDeleteDateData As System.Windows.Forms.Button
    Friend WithEvents chkPass As System.Windows.Forms.CheckBox
    Friend WithEvents cmbScrip As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ButResetPassword As System.Windows.Forms.Button
    Friend WithEvents cmbScenario As System.Windows.Forms.ComboBox
    Friend WithEvents BtnScenarioDelete As System.Windows.Forms.Button
    Friend WithEvents cmbentry As System.Windows.Forms.ComboBox
    Friend WithEvents btnDelBhavCopy As System.Windows.Forms.Button
    Friend WithEvents btnexpiryfo As System.Windows.Forms.Button
    Friend WithEvents chkexpiryfo As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkexpirycurr As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnexpirycurr As System.Windows.Forms.Button
    Friend WithEvents cmdsave As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(delete_data))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkexpirycurr = New System.Windows.Forms.CheckedListBox()
        Me.btnexpirycurr = New System.Windows.Forms.Button()
        Me.chkexpiryfo = New System.Windows.Forms.CheckedListBox()
        Me.btnexpiryfo = New System.Windows.Forms.Button()
        Me.cmbentry = New System.Windows.Forms.ComboBox()
        Me.btnDelBhavCopy = New System.Windows.Forms.Button()
        Me.cmbScenario = New System.Windows.Forms.ComboBox()
        Me.BtnScenarioDelete = New System.Windows.Forms.Button()
        Me.cmbScrip = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dtp1 = New System.Windows.Forms.DateTimePicker()
        Me.cmdDeleteDateData = New System.Windows.Forms.Button()
        Me.cmdtoday = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.cmdsave = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButResetPassword = New System.Windows.Forms.Button()
        Me.chkPass = New System.Windows.Forms.CheckBox()
        Me.txtpass = New System.Windows.Forms.TextBox()
        Me.txtlogid = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.chkexpirycurr)
        Me.Panel2.Controls.Add(Me.btnexpirycurr)
        Me.Panel2.Controls.Add(Me.chkexpiryfo)
        Me.Panel2.Controls.Add(Me.btnexpiryfo)
        Me.Panel2.Controls.Add(Me.cmbentry)
        Me.Panel2.Controls.Add(Me.btnDelBhavCopy)
        Me.Panel2.Controls.Add(Me.cmbScenario)
        Me.Panel2.Controls.Add(Me.BtnScenarioDelete)
        Me.Panel2.Controls.Add(Me.cmbScrip)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.dtp1)
        Me.Panel2.Controls.Add(Me.cmdDeleteDateData)
        Me.Panel2.Controls.Add(Me.cmdtoday)
        Me.Panel2.Controls.Add(Me.cmdexit)
        Me.Panel2.Controls.Add(Me.cmdsave)
        Me.Panel2.Location = New System.Drawing.Point(7, 152)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(430, 367)
        Me.Panel2.TabIndex = 1
        '
        'chkexpirycurr
        '
        Me.chkexpirycurr.FormattingEnabled = True
        Me.chkexpirycurr.Location = New System.Drawing.Point(216, 282)
        Me.chkexpirycurr.Name = "chkexpirycurr"
        Me.chkexpirycurr.Size = New System.Drawing.Size(200, 64)
        Me.chkexpirycurr.TabIndex = 12
        '
        'btnexpirycurr
        '
        Me.btnexpirycurr.BackColor = System.Drawing.Color.White
        Me.btnexpirycurr.Font = New System.Drawing.Font("Palatino Linotype", 11.25!)
        Me.btnexpirycurr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnexpirycurr.Location = New System.Drawing.Point(1, 276)
        Me.btnexpirycurr.Name = "btnexpirycurr"
        Me.btnexpirycurr.Size = New System.Drawing.Size(201, 32)
        Me.btnexpirycurr.TabIndex = 11
        Me.btnexpirycurr.Text = "Delete OLD Data (CURR)"
        Me.btnexpirycurr.UseVisualStyleBackColor = False
        '
        'chkexpiryfo
        '
        Me.chkexpiryfo.FormattingEnabled = True
        Me.chkexpiryfo.Location = New System.Drawing.Point(217, 212)
        Me.chkexpiryfo.Name = "chkexpiryfo"
        Me.chkexpiryfo.Size = New System.Drawing.Size(200, 64)
        Me.chkexpiryfo.TabIndex = 10
        '
        'btnexpiryfo
        '
        Me.btnexpiryfo.BackColor = System.Drawing.Color.White
        Me.btnexpiryfo.Font = New System.Drawing.Font("Palatino Linotype", 11.25!)
        Me.btnexpiryfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnexpiryfo.Location = New System.Drawing.Point(2, 214)
        Me.btnexpiryfo.Name = "btnexpiryfo"
        Me.btnexpiryfo.Size = New System.Drawing.Size(201, 32)
        Me.btnexpiryfo.TabIndex = 9
        Me.btnexpiryfo.Text = "Delete OLD Data (FO)"
        Me.btnexpiryfo.UseVisualStyleBackColor = False
        '
        'cmbentry
        '
        Me.cmbentry.AllowDrop = True
        Me.cmbentry.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbentry.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbentry.FormattingEnabled = True
        Me.cmbentry.Location = New System.Drawing.Point(217, 182)
        Me.cmbentry.Name = "cmbentry"
        Me.cmbentry.Size = New System.Drawing.Size(197, 21)
        Me.cmbentry.TabIndex = 8
        '
        'btnDelBhavCopy
        '
        Me.btnDelBhavCopy.BackColor = System.Drawing.Color.White
        Me.btnDelBhavCopy.Font = New System.Drawing.Font("Palatino Linotype", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelBhavCopy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelBhavCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelBhavCopy.Location = New System.Drawing.Point(3, 176)
        Me.btnDelBhavCopy.Name = "btnDelBhavCopy"
        Me.btnDelBhavCopy.Size = New System.Drawing.Size(201, 32)
        Me.btnDelBhavCopy.TabIndex = 7
        Me.btnDelBhavCopy.Text = "Delete Bhavcopy"
        Me.btnDelBhavCopy.UseVisualStyleBackColor = False
        '
        'cmbScenario
        '
        Me.cmbScenario.AllowDrop = True
        Me.cmbScenario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbScenario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbScenario.FormattingEnabled = True
        Me.cmbScenario.Location = New System.Drawing.Point(217, 143)
        Me.cmbScenario.Name = "cmbScenario"
        Me.cmbScenario.Size = New System.Drawing.Size(197, 21)
        Me.cmbScenario.TabIndex = 6
        '
        'BtnScenarioDelete
        '
        Me.BtnScenarioDelete.BackColor = System.Drawing.Color.White
        Me.BtnScenarioDelete.Font = New System.Drawing.Font("Palatino Linotype", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnScenarioDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnScenarioDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnScenarioDelete.Location = New System.Drawing.Point(3, 135)
        Me.BtnScenarioDelete.Name = "BtnScenarioDelete"
        Me.BtnScenarioDelete.Size = New System.Drawing.Size(201, 32)
        Me.BtnScenarioDelete.TabIndex = 5
        Me.BtnScenarioDelete.Text = "Delete Scenario"
        Me.BtnScenarioDelete.UseVisualStyleBackColor = False
        '
        'cmbScrip
        '
        Me.cmbScrip.AllowDrop = True
        Me.cmbScrip.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbScrip.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbScrip.FormattingEnabled = True
        Me.cmbScrip.Location = New System.Drawing.Point(217, 100)
        Me.cmbScrip.Name = "cmbScrip"
        Me.cmbScrip.Size = New System.Drawing.Size(197, 21)
        Me.cmbScrip.TabIndex = 6
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Font = New System.Drawing.Font("Palatino Linotype", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(3, 92)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(201, 32)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Delete Security"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'dtp1
        '
        Me.dtp1.CustomFormat = "dd/MM/yyyy"
        Me.dtp1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp1.Location = New System.Drawing.Point(217, 58)
        Me.dtp1.Name = "dtp1"
        Me.dtp1.Size = New System.Drawing.Size(197, 20)
        Me.dtp1.TabIndex = 4
        '
        'cmdDeleteDateData
        '
        Me.cmdDeleteDateData.BackColor = System.Drawing.Color.White
        Me.cmdDeleteDateData.Font = New System.Drawing.Font("Palatino Linotype", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeleteDateData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDeleteDateData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdDeleteDateData.Location = New System.Drawing.Point(3, 51)
        Me.cmdDeleteDateData.Name = "cmdDeleteDateData"
        Me.cmdDeleteDateData.Size = New System.Drawing.Size(201, 32)
        Me.cmdDeleteDateData.TabIndex = 3
        Me.cmdDeleteDateData.Text = "Delete Data of Date"
        Me.cmdDeleteDateData.UseVisualStyleBackColor = False
        '
        'cmdtoday
        '
        Me.cmdtoday.BackColor = System.Drawing.Color.White
        Me.cmdtoday.Font = New System.Drawing.Font("Palatino Linotype", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdtoday.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdtoday.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdtoday.Location = New System.Drawing.Point(217, 8)
        Me.cmdtoday.Name = "cmdtoday"
        Me.cmdtoday.Size = New System.Drawing.Size(151, 32)
        Me.cmdtoday.TabIndex = 1
        Me.cmdtoday.Text = "Delete today Data "
        Me.cmdtoday.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.White
        Me.cmdexit.CausesValidation = False
        Me.cmdexit.Font = New System.Drawing.Font("Palatino Linotype", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdexit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdexit.Location = New System.Drawing.Point(370, 8)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(43, 32)
        Me.cmdexit.TabIndex = 2
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'cmdsave
        '
        Me.cmdsave.BackColor = System.Drawing.Color.White
        Me.cmdsave.Font = New System.Drawing.Font("Palatino Linotype", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdsave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdsave.Location = New System.Drawing.Point(3, 8)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(201, 32)
        Me.cmdsave.TabIndex = 0
        Me.cmdsave.Text = "Delete All Data"
        Me.cmdsave.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.ButResetPassword)
        Me.Panel1.Controls.Add(Me.chkPass)
        Me.Panel1.Controls.Add(Me.txtpass)
        Me.Panel1.Controls.Add(Me.txtlogid)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Location = New System.Drawing.Point(7, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(430, 138)
        Me.Panel1.TabIndex = 0
        '
        'ButResetPassword
        '
        Me.ButResetPassword.BackColor = System.Drawing.Color.White
        Me.ButResetPassword.Font = New System.Drawing.Font("Palatino Linotype", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButResetPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ButResetPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButResetPassword.Location = New System.Drawing.Point(148, 96)
        Me.ButResetPassword.Name = "ButResetPassword"
        Me.ButResetPassword.Size = New System.Drawing.Size(156, 32)
        Me.ButResetPassword.TabIndex = 39
        Me.ButResetPassword.Text = "Reset Password"
        Me.ButResetPassword.UseVisualStyleBackColor = False
        '
        'chkPass
        '
        Me.chkPass.AutoSize = True
        Me.chkPass.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.chkPass.Location = New System.Drawing.Point(152, 75)
        Me.chkPass.Name = "chkPass"
        Me.chkPass.Size = New System.Drawing.Size(124, 17)
        Me.chkPass.TabIndex = 3
        Me.chkPass.Text = "Save Password"
        Me.chkPass.UseVisualStyleBackColor = True
        '
        'txtpass
        '
        Me.txtpass.BackColor = System.Drawing.SystemColors.Window
        Me.txtpass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpass.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpass.Location = New System.Drawing.Point(152, 44)
        Me.txtpass.Name = "txtpass"
        Me.txtpass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpass.Size = New System.Drawing.Size(152, 22)
        Me.txtpass.TabIndex = 1
        '
        'txtlogid
        '
        Me.txtlogid.BackColor = System.Drawing.Color.White
        Me.txtlogid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtlogid.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlogid.Location = New System.Drawing.Point(152, 15)
        Me.txtlogid.Name = "txtlogid"
        Me.txtlogid.Size = New System.Drawing.Size(152, 22)
        Me.txtlogid.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label7.Location = New System.Drawing.Point(132, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(21, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = ": -"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label9.Location = New System.Drawing.Point(132, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(21, 13)
        Me.Label9.TabIndex = 34
        Me.Label9.Text = ": -"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label4.Location = New System.Drawing.Point(8, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Login ID "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label5.Location = New System.Drawing.Point(8, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Password"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'delete_data
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.ClientSize = New System.Drawing.Size(449, 515)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "delete_data"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Delete Data"
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim objUser As New user_master
    Dim objDel As New deletedata
    Dim objAna As New analysisprocess
    Dim objScenario As New scenarioDetail
    Dim flgPass As Boolean = False
    Dim dtUser As New DataTable
    Dim mFillUserPass As Boolean

    Dim ObjSceDetail As New scenarioDetail
    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        Try
            Dim trd As New trading
            'If txtlogid.Text = "" Or txtpass.Text = "" Or txtcpass.Text = "" Or txtopass.Text = "" Then
            txtlogid.Text = txtlogid.Text.Trim
            txtpass.Text = txtpass.Text.Trim

            mFillUserPass = True
            If txtlogid.Text = "" Or txtpass.Text = "" Then
                MsgBox("Please Enter Login Id and Password!!", MsgBoxStyle.Exclamation)
            Else
                Dim pass As String
                pass = ""

                For Each drow As DataRow In objUser.Selectdata.Select("loginid = '" & Encry(txtlogid.Text) & "' and pass = '" & Encry(txtpass.Text) & "'")
                    pass = drow("pass")
                    Exit For
                Next
                If pass = "" Then
                    MsgBox("Not Valid User!!", MsgBoxStyle.Exclamation)
                Else
                    If MsgBox("Are you sure to Delete All Data?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        Exit Sub
                    End If
                    REM Reset Password
                    'ResetPassword()

                    'backup old database
                    Dim str_folder_path As String
                    str_folder_path = System.Windows.Forms.Application.StartupPath() & "\backup_" & Format(Now(), "ddMMyy")
                    If Not Directory.Exists(str_folder_path) Then
                        Directory.CreateDirectory(str_folder_path)
                    End If
                    Dim str As String = ConfigurationSettings.AppSettings("dbname")
                    Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str

                    Dim cur_date_str As String
                    cur_date_str = Format(Now, "ddMMMyyyy_HHmm")
                    Dim tstr As String = str_folder_path & "\greek_backup_" & cur_date_str & ".mdb"

                    FileCopy(_connection_string, tstr)
                    '============================end of backup
                    objDel.Loginid = Encry(txtlogid.Text)
                    objDel.Entrydate = Now()
                    objDel.Insert()
                    Dim dtScenario As New DataTable
                    dtScenario = ObjSceDetail.Select_Scenario()
                    For Each dr As DataRow In dtScenario.Rows
                        Dim cnstAnalysisScenario As String = "Analysis-Scenario"
                        'cmbScenario.Items.Add(dtScenario.Rows(i)("ScenarioName").ToString)
                        'objScenario.Delete_scenario(dr("ScenarioName").ToString)
                        '  If cnstAnalysisScenario <> dr("ScenarioName").ToString Then
                        objDel.Delete_Data_scenario(dr("ScenarioName").ToString)
                        ' End If

                    Next


                    GdtFOTrades.Clear()
                    GdtEQTrades.Clear()
                    GdtCurrencyTrades.Rows.Clear()
                    'Alpesh  20/04/2011
                    G_DTExpenseData.Rows.Clear()
                    GPatchExpDiff = 0

                    comptable.Rows.Clear()
                    maintable.Clear()
                    hashOrder.Clear()
                    ''refresh bhavcopy global table 
                    Dim Objbhavcopy As New bhav_copy
                    GdtBhavcopy = Objbhavcopy.select_data()

                    If GVarIsNewBhavcopy = True Then
                        GVarIsNewBhavcopy = False
                    End If
                    Fill_HT_RefreshTrde()
                    trd.Reset_CFBalance()
                    MsgBox("Data Deleted Successfully.", MsgBoxStyle.Information)

                End If
                'hashOrder.Clear()
                txtlogid.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        'If MsgBox("Do you want to Exit", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then
        If chkPass.Checked = True Then
            Dim setting_name, setting_key As String
            Dim objTrad As New trading
            For Each drow As DataRow In GdtSettings.Select("SettingName='LOGINID' or SettingName='LOGINPASSWD'", "")
                setting_name = ""
                setting_key = ""
                If drow("SettingName") = "LOGINID" Then
                    setting_name = "LOGINID"
                    setting_key = Encry(txtlogid.Text)
                    drow("SettingKey") = Encry(txtlogid.Text)
                ElseIf drow("SettingName") = "LOGINPASSWD" Then
                    setting_name = "LOGINPASSWD"
                    setting_key = Encry(txtpass.Text)
                    drow("SettingKey") = Encry(txtpass.Text)
                End If
                If setting_name <> "" Then
                    objTrad.SettingName = setting_name
                    objTrad.SettingKey = setting_key
                    objTrad.Uid = CInt(drow("uid"))
                    objTrad.Update_setting()

                End If
            Next
        End If
        Me.Close()

        'End If
    End Sub

    Private Sub delete_data_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'If mFillUserPass = False Then
        '    dtUser = objUser.Selectdata
        '    txtlogid.Text = Decry(GdtSettings.Compute("max(SettingKey)", "SettingName='LOGINID'").ToString)
        '    txtpass.Text = Decry(GdtSettings.Compute("max(SettingKey)", "SettingName='LOGINPASSWD'").ToString)
        'End If
        ' '' ''MsgBox(txtpass.Text)
        '' ''If txtlogid.Text <> "" And txtpass.Text <> "" Then
        '' ''    chkPass.Checked = True
        '' ''End If

        '' ''Dim i As Integer
        '' ''For i = 0 To comptable.Rows.Count - 1
        '' ''    cmbScrip.Items.Add(comptable.Rows(i)("company").ToString)
        '' ''Next
    End Sub

    Private Sub frmusermaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtUser = objUser.Selectdata
        txtlogid.Text = Decry(GdtSettings.Compute("max(SettingKey)", "SettingName='LOGINID'").ToString)
        txtpass.Text = Decry(GdtSettings.Compute("max(SettingKey)", "SettingName='LOGINPASSWD'").ToString)

        If txtlogid.Text <> "" And txtpass.Text <> "" Then
            chkPass.Checked = True
        End If

        Dim i As Integer
        For i = 0 To comptable.Rows.Count - 1
            cmbScrip.Items.Add(comptable.Rows(i)("company").ToString)
        Next

        Dim dtScenario As New DataTable
        dtScenario = ObjSceDetail.Select_Scenario()
        For i = 0 To dtScenario.Rows.Count - 1
            cmbScenario.Items.Add(dtScenario.Rows(i)("ScenarioName").ToString)
        Next

        Dim Dv As DataView
        Dv = New DataView(GdtBhavcopy, "option_type<>'XX'", "symbol", DataViewRowState.OriginalRows)
        cmbentry.DataSource = Dv.ToTable(True, "entry_date")
        cmbentry.DisplayMember = "entry_date"
        cmbentry.ValueMember = "entry_date"


        Me.WindowState = FormWindowState.Normal
        Me.Refresh()
        FlushAllTrd()




        Loaddatafo()
        LoaddaLoaddatafoCurr()

    End Sub
    Public Sub Loaddatafo()
        chkexpiryfo.Items.Clear()

        Dim fodata As DataTable
        fodata = objDel.GetExpiryfo()
        Dim dv2 As DataView = New DataView(fodata, "", "mdate", DataViewRowState.CurrentRows)
        For Each rowView As DataRowView In dv2
            Dim dr As DataRow = rowView.Row
            ' Do something '
            chkexpiryfo.Items.Add(dr("mdate"))
        Next

    End Sub
    Public Sub LoaddaLoaddatafoCurr()
        chkexpirycurr.Items.Clear()

        Dim currdata As DataTable
        currdata = objDel.GetExpirycurr()
        Dim dv3 As DataView = New DataView(currdata, "", "mdate", DataViewRowState.CurrentRows)
        For Each rowView As DataRowView In dv3
            Dim dr As DataRow = rowView.Row
            ' Do something '
            chkexpirycurr.Items.Add(dr("mdate"))
        Next
    End Sub
    Private Function Delete_Trd(ByVal sTrd As String) As String
        Return "Delete * From " & sTrd & ";"
    End Function


    Public Sub FlushAllTrd()
        Try
            DAL.data_access.ParamClear()
            Dim TableList() As String
            Dim i As Integer
            TableList = "GetFoTrd,GetCurTrd,GetEqTrd,NowFoTrd,NowCurTrd,NowEqTrd,NotFoTrd,NotCurTrd,NotEqTrd,OdiFoTrd,OdiCurTrd,OdiEqTrd,NseFoTrd,NseCurTrd,NeaFoTrd,NeaCurTrd".Split(",")
            For i = 0 To UBound(TableList)
                DAL.data_access.ExecuteNonQuery(Delete_Trd(TableList(i)), CommandType.Text)
            Next
            DAL.data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdtoday.Click
        Try
            mFillUserPass = True
            'If txtlogid.Text = "" Or txtpass.Text = "" Or txtcpass.Text = "" Or txtopass.Text = "" Then
            txtlogid.Text = txtlogid.Text.Trim
            txtpass.Text = txtpass.Text.Trim
            If txtlogid.Text = "" Or txtpass.Text = "" Then
                MsgBox("Enter Login Id and Password!!", MsgBoxStyle.Exclamation)
            Else
                Dim pass As String
                pass = ""

                'Dim NewPass As String
                'NewPass = ""

                'NewPass = Encry(txtlogid.Text)
                'If NewPassVerification(pass, NewPass) = False Then
                '    MsgBox("New Password Can Not Be Match With Password.", MsgBoxStyle.Exclamation)
                '    Exit Sub
                'End If

                For Each drow As DataRow In objUser.Selectdata.Select("loginid = '" & Encry(txtlogid.Text) & "' and pass = '" & Encry(txtpass.Text) & "'")
                    pass = drow("pass")
                    Exit For
                Next
                If pass = "" Then
                    MsgBox("Not Valid User!!!", MsgBoxStyle.Exclamation)
                Else
                    'ResetPassword()
                    'If MsgBox("Are you sure to delete today data", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'backup old database
                    Dim str_folder_path As String
                    str_folder_path = System.Windows.Forms.Application.StartupPath() & "\backup_DELTODAYDATA_" & Format(Now(), "ddMMyy")
                    If Not Directory.Exists(str_folder_path) Then
                        Directory.CreateDirectory(str_folder_path)
                    End If
                    Dim str As String = ConfigurationSettings.AppSettings("dbname")
                    Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str

                    Dim cur_date_str As String
                    cur_date_str = Format(Now, "ddMMMyyyy_HHmm")
                    Dim tstr As String = str_folder_path & "\greek_backup_" & cur_date_str & ".mdb"

                    FileCopy(_connection_string, tstr)
                    '============================end of backup

                    objDel.Delete_todaydata()
                    objDel.Delete_PrevBalance_OnDate(Today)
                    objScenario.Delete_scenario()
                    objAna.process_data()
                    'End If
                    MsgBox("Data Deleted Successfully.", MsgBoxStyle.Information)
                End If

                txtlogid.Focus()
            End If
        Catch ex As Exception

            MsgBox(ex.ToString)
        End Try


    End Sub


    Private Sub delete_data_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub cmdDeleteDateData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteDateData.Click
        Try
            mFillUserPass = True
            'If txtlogid.Text = "" Or txtpass.Text = "" Or txtcpass.Text = "" Or txtopass.Text = "" Then
            txtlogid.Text = txtlogid.Text.Trim
            txtpass.Text = txtpass.Text.Trim
            If txtlogid.Text = "" Or txtpass.Text = "" Then
                MsgBox("Enter Login Id and Password!!", MsgBoxStyle.Exclamation)
            Else
                Dim pass As String
                pass = ""

                'Dim NewPass As String
                'NewPass = ""

                'NewPass = Encry(txtlogid.Text)
                'If NewPassVerification(pass, NewPass) = False Then
                '    MsgBox("New Password Can Not Be Match With Password.", MsgBoxStyle.Exclamation)
                '    Exit Sub
                'End If

                For Each drow As DataRow In objUser.Selectdata.Select("loginid = '" & Encry(txtlogid.Text) & "' and pass = '" & Encry(txtpass.Text) & "'")
                    pass = drow("pass")
                    Exit For
                Next
                If pass = "" Then
                    MsgBox("Not Valid User!!", MsgBoxStyle.Exclamation)
                Else
                    'ResetPassword()
                    'If MsgBox("Are you sure to delete today data", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'backup old database
                    Dim str_folder_path As String
                    str_folder_path = System.Windows.Forms.Application.StartupPath() & "\backup_DELDDATAOF_" & Format(Now(), "ddMMyy")
                    If Not Directory.Exists(str_folder_path) Then
                        Directory.CreateDirectory(str_folder_path)
                    End If
                    Dim str As String = ConfigurationSettings.AppSettings("dbname")
                    Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str

                    Dim cur_date_str As String
                    cur_date_str = Format(Now, "ddMMMyyyy_HHmm")
                    Dim tstr As String = str_folder_path & "\greek_backup_" & cur_date_str & ".mdb"

                    FileCopy(_connection_string, tstr)
                    '============================end of backup
                    objDel.Delete_datedata(dtp1.Value.Date)
                    objDel.Delete_PrevBalance_OnDate(dtp1.Value.Date)
                    objScenario.Delete_scenario()
                    objAna.process_data()
                    'End If
                    MsgBox("Data Deleted Successfully.", vbInformation)
                End If

                txtlogid.Focus()
            End If
        Catch ex As Exception

            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mFillUserPass = True
        txtlogid.Text = txtlogid.Text.Trim
        txtpass.Text = txtpass.Text.Trim

        If cmbScrip.Text = "" Then
            MsgBox("Please Select Scrip!!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim i As Integer
        Dim mrows() As DataRow
        Dim company As String


        REM Delete Data - Security is Delete Require correct Password is enter
        If txtlogid.Text = "" Or txtpass.Text = "" Then
            MsgBox("Enter Login Id and Password!!", MsgBoxStyle.Exclamation)
        Else
            Dim pass As String
            pass = ""

            For Each drow As DataRow In objUser.Selectdata.Select("loginid = '" & Encry(txtlogid.Text) & "' and pass = '" & Encry(txtpass.Text) & "'")
                pass = drow("pass")
                Exit For
            Next
            If pass = "" Then
                MsgBox("Not Valid User!!", MsgBoxStyle.Exclamation)
            Else

                company = cmbScrip.SelectedItem.ToString.Trim

                objDel.Delete_Data_script(company)

                'delete company from maintable
                mrows = maintable.Select("company='" & company & "'")
                For i = 0 To mrows.Length - 1
                    maintable.Rows.Remove(mrows(i))
                Next
                maintable.AcceptChanges()
                'delete from analysis datatable
                objAna.delete_analysis_company(company)


                mrows = GdtFOTrades.Select("company='" & company & "'")
                If mrows.Length > 0 Then
                    For i = 0 To mrows.Length - 1
                        GdtFOTrades.Rows.Remove(mrows(i))
                    Next
                End If
                GdtFOTrades.AcceptChanges()

                mrows = GdtEQTrades.Select("company='" & company & "'")
                If mrows.Length > 0 Then
                    For i = 0 To mrows.Length - 1
                        GdtEQTrades.Rows.Remove(mrows(i))
                    Next
                End If
                GdtEQTrades.AcceptChanges()


                'Alpesh 20/04/2011
                mrows = G_DTExpenseData.Select("company='" & company & "'")
                If mrows.Length > 0 Then
                    For i = 0 To mrows.Length - 1
                        G_DTExpenseData.Rows.Remove(mrows(i))
                    Next
                End If
                G_DTExpenseData.AcceptChanges()
                objAna.process_data()
                'Dim objTrad As New trading
                'dtEQTrades = objTrad.select_equity()

                'mrows = dtEQTrades.Select("company='" & company & "'")
                'If mrows.Length > 0 Then dtFOTrades = objTrad.Trading()

                MsgBox("Data Deleted Successfully.", MsgBoxStyle.Information)
            End If
        End If
    End Sub

    Private Sub delete_data_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim objTrad As New trading
        comptable = objTrad.Comapany
        comptable_Net = objTrad.Comapany_Net
        REM Refresh Global Table
        'Call GSub_Fill_GDt_AllTrades()
        'Call GSub_Fill_GDt_Strategy()
        REM End
    End Sub
    Private Sub ResetPassword()
        'If txtNewPass.Text.Trim <> "" Then
        '    'objUser.Loginid=
        '    objUser.Update(Encry(txtlogid.Text), Encry(txtNewPass.Text))
        'End If
    End Sub
    Private Sub ButResetPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButResetPassword.Click
        REM For Reset Password 
        mFillUserPass = False
        Dim ResetPass As New ResetPasssword
        ResetPass.ShowDialog()
    End Sub


    Private Sub BtnScenarioDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnScenarioDelete.Click
        mFillUserPass = True
        txtlogid.Text = txtlogid.Text.Trim
        txtpass.Text = txtpass.Text.Trim
        If cmbScenario.Text = "" Then
            MsgBox("Please Select Scenario!!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim Scenario As String


        REM Delete Data - Security is Delete Require correct Password is enter
        If txtlogid.Text = "" Or txtpass.Text = "" Then
            MsgBox("Enter Login Id and Password!!", MsgBoxStyle.Exclamation)
        Else
            Dim pass As String
            pass = ""

            For Each drow As DataRow In objUser.Selectdata.Select("loginid = '" & Encry(txtlogid.Text) & "' and pass = '" & Encry(txtpass.Text) & "'")
                pass = drow("pass")
                Exit For
            Next
            If pass = "" Then
                MsgBox("Not Valid User!!", MsgBoxStyle.Exclamation)
            Else

                Scenario = cmbScenario.SelectedItem.ToString.Trim

                objDel.Delete_Data_scenario(Scenario)

                MsgBox("Scenario Data Deleted Successfully.", MsgBoxStyle.Information)
            End If
        End If
    End Sub

    Private Sub btnDelBhavCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelBhavCopy.Click
        mFillUserPass = True

        txtlogid.Text = txtlogid.Text.Trim
        txtpass.Text = txtpass.Text.Trim

        If cmbentry.Text = "" Then
            MsgBox("Please Select Date!!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim EntryDate As Date


        REM Delete Data - Security is Delete Require correct Password is enter
        If txtlogid.Text = "" Or txtpass.Text = "" Then
            MsgBox("Enter Login Id and Password!!", MsgBoxStyle.Exclamation)
        Else
            Dim pass As String
            pass = ""

            For Each drow As DataRow In objUser.Selectdata.Select("loginid = '" & Encry(txtlogid.Text) & "' and pass = '" & Encry(txtpass.Text) & "'")
                pass = drow("pass")
                Exit For
            Next
            If pass = "" Then
                MsgBox("Not Valid User!!", MsgBoxStyle.Exclamation)
            Else

                EntryDate = CDate(cmbentry.Text)

                objDel.Delete_Data_Bhavcopy(EntryDate)
                Dim Objbhavcopy As New bhav_copy
                GdtBhavcopy = Objbhavcopy.select_data()

                If GVarIsNewBhavcopy = True Then
                    GVarIsNewBhavcopy = False
                End If

                Dim Dv As DataView
                Dv = New DataView(GdtBhavcopy, "option_type<>'XX'", "symbol", DataViewRowState.OriginalRows)
                cmbentry.DataSource = Dv.ToTable(True, "entry_date")
                cmbentry.DisplayMember = "entry_date"
                cmbentry.ValueMember = "entry_date"

                MsgBox("Bhavcopy Data Deleted Successfully.", MsgBoxStyle.Information)
            End If
        End If
    End Sub

    Private Sub btnexpiryfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexpiryfo.Click
        If chkexpiryfo.CheckedItems.Count > 0 Then
            If MsgBox("Are you sure to Delete OLD Data(FO)?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                'backup old database
                Dim str_folder_path As String
                str_folder_path = System.Windows.Forms.Application.StartupPath() & "\backup_DELOLDData(FO)_" & Format(Now(), "ddMMyy")
                If Not Directory.Exists(str_folder_path) Then
                    Directory.CreateDirectory(str_folder_path)
                End If
                Dim str As String = ConfigurationSettings.AppSettings("dbname")
                Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str

                Dim cur_date_str As String
                cur_date_str = Format(Now, "ddMMMyyyy_HHmm")
                Dim tstr As String = str_folder_path & "\greek_backup_" & cur_date_str & ".mdb"

                FileCopy(_connection_string, tstr)
                '============================end of backup

                Dim list As New DataTable
                list = New DataTable
                list.Columns.Add("mdate", GetType(String))
                For i As Integer = 0 To chkexpiryfo.CheckedItems.Count - 1
                    Dim dr As DataRow = list.NewRow()
                    dr("mdate") = chkexpiryfo.CheckedItems(i)
                    list.Rows.Add(dr)
                Next
                list.AcceptChanges()
                '  Dim Security As String

                objDel.DeleteExpiryFo(list)
                Loaddatafo()


            End If
        Else
            MsgBox("OLDData(FO) List Is Empty")
        End If
    End Sub

    Private Sub btnexpirycurr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexpirycurr.Click
        If chkexpirycurr.CheckedItems.Count > 0 Then
            If MsgBox("Are you sure to Delete OLD Data(CURR)?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                'backup old database
                Dim str_folder_path As String
                str_folder_path = System.Windows.Forms.Application.StartupPath() & "\backup_DELOLDData(CURR)_" & Format(Now(), "ddMMyy")
                If Not Directory.Exists(str_folder_path) Then
                    Directory.CreateDirectory(str_folder_path)
                End If
                Dim str As String = ConfigurationSettings.AppSettings("dbname")
                Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str

                Dim cur_date_str As String
                cur_date_str = Format(Now, "ddMMMyyyy_HHmm")
                Dim tstr As String = str_folder_path & "\greek_backup_" & cur_date_str & ".mdb"

                FileCopy(_connection_string, tstr)
                '============================end of backup

                Dim list As New DataTable
                list = New DataTable
                list.Columns.Add("mdate", GetType(String))
                For i As Integer = 0 To chkexpirycurr.CheckedItems.Count - 1
                    Dim dr As DataRow = list.NewRow()
                    dr("mdate") = chkexpirycurr.CheckedItems(i)
                    list.Rows.Add(dr)
                Next


                list.AcceptChanges()
                '  Dim Security As String

                objDel.DeleteExpiryCurr(list)
                LoaddaLoaddatafoCurr()
            End If
        Else
            MsgBox("OLDData(CURR) List Is Empty")
        End If
    End Sub
End Class
