<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OpenPosition
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OpenPosition))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.cmbFoExchange = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbllotsize = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbstrike = New System.Windows.Forms.ComboBox()
        Me.cmbcp = New System.Windows.Forms.ComboBox()
        Me.CmbInstru = New System.Windows.Forms.ComboBox()
        Me.CmbComp = New System.Windows.Forms.ComboBox()
        Me.cmbdate = New System.Windows.Forms.ComboBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.cmdtrcl = New System.Windows.Forms.Button()
        Me.cmdtrimp = New System.Windows.Forms.Button()
        Me.cmdtrbr = New System.Windows.Forms.Button()
        Me.txtpath = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtent = New System.Windows.Forms.DateTimePicker()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.txtrate = New System.Windows.Forms.TextBox()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.cmdsave = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtunit = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtscript = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.cmbEqExchange = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbeqopt = New System.Windows.Forms.ComboBox()
        Me.cmbeqcomp = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txteqscript = New System.Windows.Forms.TextBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dteqent = New System.Windows.Forms.DateTimePicker()
        Me.cmdeqexit = New System.Windows.Forms.Button()
        Me.txteqrate = New System.Windows.Forms.TextBox()
        Me.cmdeqclear = New System.Windows.Forms.Button()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.cmdeqsave = New System.Windows.Forms.Button()
        Me.txtequnit = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.cmbCurrencyStrike = New System.Windows.Forms.ComboBox()
        Me.cmbCurrencyCP = New System.Windows.Forms.ComboBox()
        Me.cmbCurrencyInstrument = New System.Windows.Forms.ComboBox()
        Me.cmbCurrencyComp = New System.Windows.Forms.ComboBox()
        Me.cmbCurrencyExpdate = New System.Windows.Forms.ComboBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.DTPCurrencyEntryDate = New System.Windows.Forms.DateTimePicker()
        Me.btnCurrencyExit = New System.Windows.Forms.Button()
        Me.txtCurrencyrate = New System.Windows.Forms.TextBox()
        Me.btnCurrencyClear = New System.Windows.Forms.Button()
        Me.btnCurrencySave = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtCurrencyunit = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtCurrencyscript = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Window
        Me.Label1.Location = New System.Drawing.Point(1, -4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(895, 36)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Open Position"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(0, 35)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(896, 188)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cmbFoExchange)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.lbllotsize)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.cmbstrike)
        Me.TabPage1.Controls.Add(Me.cmbcp)
        Me.TabPage1.Controls.Add(Me.CmbInstru)
        Me.TabPage1.Controls.Add(Me.CmbComp)
        Me.TabPage1.Controls.Add(Me.cmbdate)
        Me.TabPage1.Controls.Add(Me.Panel6)
        Me.TabPage1.Controls.Add(Me.Panel3)
        Me.TabPage1.Controls.Add(Me.Label17)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.txtscript)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(888, 162)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Future && Option"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'cmbFoExchange
        '
        Me.cmbFoExchange.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbFoExchange.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbFoExchange.FormattingEnabled = True
        Me.cmbFoExchange.Location = New System.Drawing.Point(15, 22)
        Me.cmbFoExchange.Name = "cmbFoExchange"
        Me.cmbFoExchange.Size = New System.Drawing.Size(79, 21)
        Me.cmbFoExchange.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(24, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 14)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Exchange"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbllotsize
        '
        Me.lbllotsize.AutoSize = True
        Me.lbllotsize.Location = New System.Drawing.Point(666, 80)
        Me.lbllotsize.Name = "lbllotsize"
        Me.lbllotsize.Size = New System.Drawing.Size(13, 13)
        Me.lbllotsize.TabIndex = 17
        Me.lbllotsize.Text = "0"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(641, 55)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 14)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "LotSize"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbstrike
        '
        Me.cmbstrike.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbstrike.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbstrike.FormattingEnabled = True
        Me.cmbstrike.Location = New System.Drawing.Point(571, 23)
        Me.cmbstrike.Name = "cmbstrike"
        Me.cmbstrike.Size = New System.Drawing.Size(155, 21)
        Me.cmbstrike.TabIndex = 9
        '
        'cmbcp
        '
        Me.cmbcp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbcp.FormattingEnabled = True
        Me.cmbcp.Location = New System.Drawing.Point(444, 23)
        Me.cmbcp.Name = "cmbcp"
        Me.cmbcp.Size = New System.Drawing.Size(120, 21)
        Me.cmbcp.TabIndex = 7
        '
        'CmbInstru
        '
        Me.CmbInstru.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbInstru.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbInstru.FormattingEnabled = True
        Me.CmbInstru.Location = New System.Drawing.Point(312, 24)
        Me.CmbInstru.Name = "CmbInstru"
        Me.CmbInstru.Size = New System.Drawing.Size(126, 21)
        Me.CmbInstru.TabIndex = 5
        '
        'CmbComp
        '
        Me.CmbComp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbComp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbComp.FormattingEnabled = True
        Me.CmbComp.Location = New System.Drawing.Point(100, 23)
        Me.CmbComp.Name = "CmbComp"
        Me.CmbComp.Size = New System.Drawing.Size(206, 21)
        Me.CmbComp.TabIndex = 3
        '
        'cmbdate
        '
        Me.cmbdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbdate.Location = New System.Drawing.Point(732, 23)
        Me.cmbdate.Name = "cmbdate"
        Me.cmbdate.Size = New System.Drawing.Size(149, 22)
        Me.cmbdate.TabIndex = 11
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.cmdtrcl)
        Me.Panel6.Controls.Add(Me.cmdtrimp)
        Me.Panel6.Controls.Add(Me.cmdtrbr)
        Me.Panel6.Controls.Add(Me.txtpath)
        Me.Panel6.Controls.Add(Me.Label7)
        Me.Panel6.Controls.Add(Me.Label8)
        Me.Panel6.Location = New System.Drawing.Point(-1, 195)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(887, 54)
        Me.Panel6.TabIndex = 8
        '
        'cmdtrcl
        '
        Me.cmdtrcl.CausesValidation = False
        Me.cmdtrcl.Location = New System.Drawing.Point(790, 14)
        Me.cmdtrcl.Name = "cmdtrcl"
        Me.cmdtrcl.Size = New System.Drawing.Size(75, 23)
        Me.cmdtrcl.TabIndex = 3
        Me.cmdtrcl.Text = "Clear"
        Me.cmdtrcl.UseVisualStyleBackColor = True
        '
        'cmdtrimp
        '
        Me.cmdtrimp.Location = New System.Drawing.Point(709, 14)
        Me.cmdtrimp.Name = "cmdtrimp"
        Me.cmdtrimp.Size = New System.Drawing.Size(75, 23)
        Me.cmdtrimp.TabIndex = 2
        Me.cmdtrimp.Text = "Import"
        Me.cmdtrimp.UseVisualStyleBackColor = True
        '
        'cmdtrbr
        '
        Me.cmdtrbr.Location = New System.Drawing.Point(492, 14)
        Me.cmdtrbr.Name = "cmdtrbr"
        Me.cmdtrbr.Size = New System.Drawing.Size(75, 23)
        Me.cmdtrbr.TabIndex = 1
        Me.cmdtrbr.Text = "Browse"
        Me.cmdtrbr.UseVisualStyleBackColor = True
        '
        'txtpath
        '
        Me.txtpath.Enabled = False
        Me.txtpath.Location = New System.Drawing.Point(130, 16)
        Me.txtpath.Name = "txtpath"
        Me.txtpath.ReadOnly = True
        Me.txtpath.Size = New System.Drawing.Size(350, 20)
        Me.txtpath.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(115, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(12, 14)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = ":"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(78, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 14)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "File"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.dtent)
        Me.Panel3.Controls.Add(Me.cmdexit)
        Me.Panel3.Controls.Add(Me.txtrate)
        Me.Panel3.Controls.Add(Me.cmdclear)
        Me.Panel3.Controls.Add(Me.cmdsave)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Controls.Add(Me.txtunit)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Location = New System.Drawing.Point(0, 103)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(888, 53)
        Me.Panel3.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(255, 5)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(77, 14)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Entry Date"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtent
        '
        Me.dtent.CustomFormat = "dd/MMM/yyyy"
        Me.dtent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtent.Location = New System.Drawing.Point(241, 26)
        Me.dtent.Name = "dtent"
        Me.dtent.Size = New System.Drawing.Size(104, 20)
        Me.dtent.TabIndex = 5
        '
        'cmdexit
        '
        Me.cmdexit.CausesValidation = False
        Me.cmdexit.Location = New System.Drawing.Point(792, 24)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(75, 23)
        Me.cmdexit.TabIndex = 8
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'txtrate
        '
        Me.txtrate.BackColor = System.Drawing.SystemColors.Window
        Me.txtrate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrate.Location = New System.Drawing.Point(123, 25)
        Me.txtrate.Name = "txtrate"
        Me.txtrate.Size = New System.Drawing.Size(110, 22)
        Me.txtrate.TabIndex = 3
        '
        'cmdclear
        '
        Me.cmdclear.CausesValidation = False
        Me.cmdclear.Location = New System.Drawing.Point(711, 24)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(75, 23)
        Me.cmdclear.TabIndex = 7
        Me.cmdclear.Text = "Clear"
        Me.cmdclear.UseVisualStyleBackColor = True
        '
        'cmdsave
        '
        Me.cmdsave.Location = New System.Drawing.Point(630, 24)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(75, 23)
        Me.cmdsave.TabIndex = 6
        Me.cmdsave.Text = "Save"
        Me.cmdsave.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(160, 5)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(37, 14)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "Rate"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtunit
        '
        Me.txtunit.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtunit.Location = New System.Drawing.Point(7, 25)
        Me.txtunit.Name = "txtunit"
        Me.txtunit.Size = New System.Drawing.Size(110, 22)
        Me.txtunit.TabIndex = 1
        Me.txtunit.Text = "0"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(42, 5)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(41, 14)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Units"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(441, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(91, 14)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "Call/Put/Fut"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(161, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Security"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(780, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 14)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Expiry"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(279, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Instrument Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtscript
        '
        Me.txtscript.Enabled = False
        Me.txtscript.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtscript.Location = New System.Drawing.Point(12, 75)
        Me.txtscript.Name = "txtscript"
        Me.txtscript.Size = New System.Drawing.Size(605, 22)
        Me.txtscript.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(607, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 14)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Strike Rate"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(291, 54)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(41, 14)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Scrip"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.cmbEqExchange)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.cmbeqopt)
        Me.TabPage2.Controls.Add(Me.cmbeqcomp)
        Me.TabPage2.Controls.Add(Me.Label19)
        Me.TabPage2.Controls.Add(Me.txteqscript)
        Me.TabPage2.Controls.Add(Me.Panel7)
        Me.TabPage2.Controls.Add(Me.Label23)
        Me.TabPage2.Controls.Add(Me.Label28)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(888, 162)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Equity"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cmbEqExchange
        '
        Me.cmbEqExchange.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbEqExchange.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbEqExchange.FormattingEnabled = True
        Me.cmbEqExchange.Location = New System.Drawing.Point(16, 29)
        Me.cmbEqExchange.Name = "cmbEqExchange"
        Me.cmbEqExchange.Size = New System.Drawing.Size(88, 21)
        Me.cmbEqExchange.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(25, 11)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(70, 14)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Exchange"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbeqopt
        '
        Me.cmbeqopt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbeqopt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbeqopt.FormattingEnabled = True
        Me.cmbeqopt.Location = New System.Drawing.Point(291, 29)
        Me.cmbeqopt.Name = "cmbeqopt"
        Me.cmbeqopt.Size = New System.Drawing.Size(108, 21)
        Me.cmbeqopt.TabIndex = 2
        '
        'cmbeqcomp
        '
        Me.cmbeqcomp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbeqcomp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbeqcomp.FormattingEnabled = True
        Me.cmbeqcomp.Location = New System.Drawing.Point(110, 29)
        Me.cmbeqcomp.Name = "cmbeqcomp"
        Me.cmbeqcomp.Size = New System.Drawing.Size(175, 21)
        Me.cmbeqcomp.TabIndex = 1
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(322, 12)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(49, 14)
        Me.Label19.TabIndex = 18
        Me.Label19.Text = "Series"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txteqscript
        '
        Me.txteqscript.Enabled = False
        Me.txteqscript.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txteqscript.Location = New System.Drawing.Point(405, 29)
        Me.txteqscript.Name = "txteqscript"
        Me.txteqscript.Size = New System.Drawing.Size(320, 22)
        Me.txteqscript.TabIndex = 3
        '
        'Panel7
        '
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Label16)
        Me.Panel7.Controls.Add(Me.dteqent)
        Me.Panel7.Controls.Add(Me.cmdeqexit)
        Me.Panel7.Controls.Add(Me.txteqrate)
        Me.Panel7.Controls.Add(Me.cmdeqclear)
        Me.Panel7.Controls.Add(Me.Label31)
        Me.Panel7.Controls.Add(Me.cmdeqsave)
        Me.Panel7.Controls.Add(Me.txtequnit)
        Me.Panel7.Controls.Add(Me.Label33)
        Me.Panel7.Location = New System.Drawing.Point(0, 56)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(889, 55)
        Me.Panel7.TabIndex = 3
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(253, 6)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 14)
        Me.Label16.TabIndex = 20
        Me.Label16.Text = "Entry Date"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dteqent
        '
        Me.dteqent.CustomFormat = "dd/MMM/yyyy"
        Me.dteqent.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dteqent.Location = New System.Drawing.Point(239, 27)
        Me.dteqent.Name = "dteqent"
        Me.dteqent.Size = New System.Drawing.Size(102, 20)
        Me.dteqent.TabIndex = 2
        '
        'cmdeqexit
        '
        Me.cmdeqexit.CausesValidation = False
        Me.cmdeqexit.Location = New System.Drawing.Point(780, 25)
        Me.cmdeqexit.Name = "cmdeqexit"
        Me.cmdeqexit.Size = New System.Drawing.Size(75, 23)
        Me.cmdeqexit.TabIndex = 5
        Me.cmdeqexit.Text = "E&xit"
        Me.cmdeqexit.UseVisualStyleBackColor = True
        '
        'txteqrate
        '
        Me.txteqrate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txteqrate.Location = New System.Drawing.Point(121, 26)
        Me.txteqrate.Name = "txteqrate"
        Me.txteqrate.Size = New System.Drawing.Size(110, 22)
        Me.txteqrate.TabIndex = 1
        Me.txteqrate.Text = "0"
        '
        'cmdeqclear
        '
        Me.cmdeqclear.CausesValidation = False
        Me.cmdeqclear.Location = New System.Drawing.Point(699, 25)
        Me.cmdeqclear.Name = "cmdeqclear"
        Me.cmdeqclear.Size = New System.Drawing.Size(75, 23)
        Me.cmdeqclear.TabIndex = 4
        Me.cmdeqclear.Text = "Clear"
        Me.cmdeqclear.UseVisualStyleBackColor = True
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(158, 6)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(37, 14)
        Me.Label31.TabIndex = 18
        Me.Label31.Text = "Rate"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdeqsave
        '
        Me.cmdeqsave.Location = New System.Drawing.Point(618, 25)
        Me.cmdeqsave.Name = "cmdeqsave"
        Me.cmdeqsave.Size = New System.Drawing.Size(75, 23)
        Me.cmdeqsave.TabIndex = 3
        Me.cmdeqsave.Text = "Save"
        Me.cmdeqsave.UseVisualStyleBackColor = True
        '
        'txtequnit
        '
        Me.txtequnit.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtequnit.Location = New System.Drawing.Point(5, 26)
        Me.txtequnit.Name = "txtequnit"
        Me.txtequnit.Size = New System.Drawing.Size(110, 22)
        Me.txtequnit.TabIndex = 0
        Me.txtequnit.Text = "0"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(40, 6)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(41, 14)
        Me.Label33.TabIndex = 15
        Me.Label33.Text = "Units"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(542, 10)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(41, 14)
        Me.Label23.TabIndex = 12
        Me.Label23.Text = "Scrip"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(147, 12)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(62, 14)
        Me.Label28.TabIndex = 1
        Me.Label28.Text = "Security"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.cmbCurrencyStrike)
        Me.TabPage3.Controls.Add(Me.cmbCurrencyCP)
        Me.TabPage3.Controls.Add(Me.cmbCurrencyInstrument)
        Me.TabPage3.Controls.Add(Me.cmbCurrencyComp)
        Me.TabPage3.Controls.Add(Me.cmbCurrencyExpdate)
        Me.TabPage3.Controls.Add(Me.Panel5)
        Me.TabPage3.Controls.Add(Me.Label32)
        Me.TabPage3.Controls.Add(Me.Label34)
        Me.TabPage3.Controls.Add(Me.Label35)
        Me.TabPage3.Controls.Add(Me.Label36)
        Me.TabPage3.Controls.Add(Me.txtCurrencyscript)
        Me.TabPage3.Controls.Add(Me.Label37)
        Me.TabPage3.Controls.Add(Me.Label38)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(888, 162)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Currency"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'cmbCurrencyStrike
        '
        Me.cmbCurrencyStrike.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCurrencyStrike.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCurrencyStrike.FormattingEnabled = True
        Me.cmbCurrencyStrike.Location = New System.Drawing.Point(570, 25)
        Me.cmbCurrencyStrike.Name = "cmbCurrencyStrike"
        Me.cmbCurrencyStrike.Size = New System.Drawing.Size(155, 21)
        Me.cmbCurrencyStrike.TabIndex = 3
        '
        'cmbCurrencyCP
        '
        Me.cmbCurrencyCP.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCurrencyCP.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCurrencyCP.FormattingEnabled = True
        Me.cmbCurrencyCP.Location = New System.Drawing.Point(409, 25)
        Me.cmbCurrencyCP.Name = "cmbCurrencyCP"
        Me.cmbCurrencyCP.Size = New System.Drawing.Size(155, 21)
        Me.cmbCurrencyCP.TabIndex = 2
        '
        'cmbCurrencyInstrument
        '
        Me.cmbCurrencyInstrument.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCurrencyInstrument.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCurrencyInstrument.FormattingEnabled = True
        Me.cmbCurrencyInstrument.Location = New System.Drawing.Point(215, 25)
        Me.cmbCurrencyInstrument.Name = "cmbCurrencyInstrument"
        Me.cmbCurrencyInstrument.Size = New System.Drawing.Size(188, 21)
        Me.cmbCurrencyInstrument.TabIndex = 1
        '
        'cmbCurrencyComp
        '
        Me.cmbCurrencyComp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCurrencyComp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCurrencyComp.FormattingEnabled = True
        Me.cmbCurrencyComp.Location = New System.Drawing.Point(6, 26)
        Me.cmbCurrencyComp.Name = "cmbCurrencyComp"
        Me.cmbCurrencyComp.Size = New System.Drawing.Size(204, 21)
        Me.cmbCurrencyComp.TabIndex = 0
        '
        'cmbCurrencyExpdate
        '
        Me.cmbCurrencyExpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurrencyExpdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCurrencyExpdate.Location = New System.Drawing.Point(731, 25)
        Me.cmbCurrencyExpdate.Name = "cmbCurrencyExpdate"
        Me.cmbCurrencyExpdate.Size = New System.Drawing.Size(149, 22)
        Me.cmbCurrencyExpdate.TabIndex = 4
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label27)
        Me.Panel5.Controls.Add(Me.DTPCurrencyEntryDate)
        Me.Panel5.Controls.Add(Me.btnCurrencyExit)
        Me.Panel5.Controls.Add(Me.txtCurrencyrate)
        Me.Panel5.Controls.Add(Me.btnCurrencyClear)
        Me.Panel5.Controls.Add(Me.btnCurrencySave)
        Me.Panel5.Controls.Add(Me.Label29)
        Me.Panel5.Controls.Add(Me.txtCurrencyunit)
        Me.Panel5.Controls.Add(Me.Label30)
        Me.Panel5.Location = New System.Drawing.Point(0, 105)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(888, 53)
        Me.Panel5.TabIndex = 25
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(255, 5)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(77, 14)
        Me.Label27.TabIndex = 15
        Me.Label27.Text = "Entry Date"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DTPCurrencyEntryDate
        '
        Me.DTPCurrencyEntryDate.CustomFormat = "dd/MMM/yyyy"
        Me.DTPCurrencyEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPCurrencyEntryDate.Location = New System.Drawing.Point(241, 26)
        Me.DTPCurrencyEntryDate.Name = "DTPCurrencyEntryDate"
        Me.DTPCurrencyEntryDate.Size = New System.Drawing.Size(102, 20)
        Me.DTPCurrencyEntryDate.TabIndex = 8
        '
        'btnCurrencyExit
        '
        Me.btnCurrencyExit.CausesValidation = False
        Me.btnCurrencyExit.Location = New System.Drawing.Point(792, 24)
        Me.btnCurrencyExit.Name = "btnCurrencyExit"
        Me.btnCurrencyExit.Size = New System.Drawing.Size(75, 23)
        Me.btnCurrencyExit.TabIndex = 11
        Me.btnCurrencyExit.Text = "E&xit"
        Me.btnCurrencyExit.UseVisualStyleBackColor = True
        '
        'txtCurrencyrate
        '
        Me.txtCurrencyrate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrencyrate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyrate.Location = New System.Drawing.Point(123, 25)
        Me.txtCurrencyrate.Name = "txtCurrencyrate"
        Me.txtCurrencyrate.Size = New System.Drawing.Size(110, 22)
        Me.txtCurrencyrate.TabIndex = 7
        Me.txtCurrencyrate.Text = "0"
        '
        'btnCurrencyClear
        '
        Me.btnCurrencyClear.CausesValidation = False
        Me.btnCurrencyClear.Location = New System.Drawing.Point(711, 24)
        Me.btnCurrencyClear.Name = "btnCurrencyClear"
        Me.btnCurrencyClear.Size = New System.Drawing.Size(75, 23)
        Me.btnCurrencyClear.TabIndex = 10
        Me.btnCurrencyClear.Text = "Clear"
        Me.btnCurrencyClear.UseVisualStyleBackColor = True
        '
        'btnCurrencySave
        '
        Me.btnCurrencySave.Location = New System.Drawing.Point(630, 24)
        Me.btnCurrencySave.Name = "btnCurrencySave"
        Me.btnCurrencySave.Size = New System.Drawing.Size(75, 23)
        Me.btnCurrencySave.TabIndex = 9
        Me.btnCurrencySave.Text = "Save"
        Me.btnCurrencySave.UseVisualStyleBackColor = True
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(160, 5)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(37, 14)
        Me.Label29.TabIndex = 18
        Me.Label29.Text = "Rate"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCurrencyunit
        '
        Me.txtCurrencyunit.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyunit.Location = New System.Drawing.Point(7, 25)
        Me.txtCurrencyunit.Name = "txtCurrencyunit"
        Me.txtCurrencyunit.Size = New System.Drawing.Size(110, 22)
        Me.txtCurrencyunit.TabIndex = 6
        Me.txtCurrencyunit.Text = "0"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(42, 5)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(35, 14)
        Me.Label30.TabIndex = 15
        Me.Label30.Text = "Lots"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(441, 5)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(91, 14)
        Me.Label32.TabIndex = 27
        Me.Label32.Text = "Call/Put/Fut"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(81, 5)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(62, 14)
        Me.Label34.TabIndex = 18
        Me.Label34.Text = "Security"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(780, 5)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(49, 14)
        Me.Label35.TabIndex = 19
        Me.Label35.Text = "Expiry"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(256, 5)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(122, 14)
        Me.Label36.TabIndex = 16
        Me.Label36.Text = "Instrument Name"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCurrencyscript
        '
        Me.txtCurrencyscript.Enabled = False
        Me.txtCurrencyscript.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyscript.Location = New System.Drawing.Point(12, 77)
        Me.txtCurrencyscript.Name = "txtCurrencyscript"
        Me.txtCurrencyscript.Size = New System.Drawing.Size(605, 22)
        Me.txtCurrencyscript.TabIndex = 5
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(607, 5)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(81, 14)
        Me.Label37.TabIndex = 21
        Me.Label37.Text = "Strike Rate"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(291, 56)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(41, 14)
        Me.Label38.TabIndex = 26
        Me.Label38.Text = "Scrip"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'OpenPosition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(898, 224)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "OpenPosition"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents CmbComp As System.Windows.Forms.ComboBox
    Friend WithEvents cmbdate As System.Windows.Forms.ComboBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents cmdtrcl As System.Windows.Forms.Button
    Friend WithEvents cmdtrimp As System.Windows.Forms.Button
    Friend WithEvents cmdtrbr As System.Windows.Forms.Button
    Friend WithEvents txtpath As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label

    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtent As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents txtrate As System.Windows.Forms.TextBox
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents cmdsave As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtunit As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtscript As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txteqscript As System.Windows.Forms.TextBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents dteqent As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdeqexit As System.Windows.Forms.Button
    Friend WithEvents txteqrate As System.Windows.Forms.TextBox
    Friend WithEvents cmdeqclear As System.Windows.Forms.Button
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents cmdeqsave As System.Windows.Forms.Button
    Friend WithEvents txtequnit As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents DTPCurrencyEntryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnCurrencyExit As System.Windows.Forms.Button
    Friend WithEvents txtCurrencyrate As System.Windows.Forms.TextBox
    Friend WithEvents btnCurrencyClear As System.Windows.Forms.Button
    Friend WithEvents btnCurrencySave As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtCurrencyunit As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtCurrencyscript As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents CmbInstru As System.Windows.Forms.ComboBox
    Friend WithEvents cmbcp As System.Windows.Forms.ComboBox
    Friend WithEvents cmbstrike As System.Windows.Forms.ComboBox
    Friend WithEvents cmbeqopt As System.Windows.Forms.ComboBox
    Friend WithEvents cmbeqcomp As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCurrencyStrike As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCurrencyCP As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCurrencyInstrument As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCurrencyComp As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCurrencyExpdate As System.Windows.Forms.ComboBox
    Friend WithEvents lbllotsize As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbFoExchange As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbEqExchange As ComboBox
    Friend WithEvents Label12 As Label
End Class
