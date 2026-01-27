<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class bhavcopy
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(bhavcopy))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Script = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.symbol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.exp_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ltp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.contract = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.val_inlakh = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.vol1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.option_type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.strike = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INSTRUMENT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.entry_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.futval = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.uid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmbsymbol = New System.Windows.Forms.ComboBox()
        Me.cmdshow = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbexp = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbopt = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbentry = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtvol = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtcontract = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtvalue = New System.Windows.Forms.TextBox()
        Me.chksymbol = New System.Windows.Forms.CheckBox()
        Me.chkopt = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtvolb = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtcontractb = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.chkexp = New System.Windows.Forms.CheckBox()
        Me.txtvalueb = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.WindowText
        Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Script, Me.symbol, Me.exp_date, Me.ltp, Me.contract, Me.val_inlakh, Me.vol1, Me.option_type, Me.strike, Me.INSTRUMENT, Me.entry_date, Me.futval, Me.mt, Me.uid})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.DataGridView1.Location = New System.Drawing.Point(0, 115)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(971, 475)
        Me.DataGridView1.TabIndex = 1
        '
        'Script
        '
        Me.Script.DataPropertyName = "Script"
        Me.Script.HeaderText = "Scrip"
        Me.Script.Name = "Script"
        Me.Script.ReadOnly = True
        Me.Script.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Script.Width = 320
        '
        'symbol
        '
        Me.symbol.DataPropertyName = "symbol"
        Me.symbol.HeaderText = "Security"
        Me.symbol.Name = "symbol"
        Me.symbol.ReadOnly = True
        Me.symbol.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.symbol.Width = 160
        '
        'exp_date
        '
        Me.exp_date.DataPropertyName = "exp_date"
        Me.exp_date.HeaderText = "Exp. Date"
        Me.exp_date.Name = "exp_date"
        Me.exp_date.ReadOnly = True
        Me.exp_date.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.exp_date.Width = 90
        '
        'ltp
        '
        Me.ltp.DataPropertyName = "ltp"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ltp.DefaultCellStyle = DataGridViewCellStyle2
        Me.ltp.HeaderText = "LTP"
        Me.ltp.Name = "ltp"
        Me.ltp.ReadOnly = True
        Me.ltp.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ltp.Width = 90
        '
        'contract
        '
        Me.contract.DataPropertyName = "contract"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.contract.DefaultCellStyle = DataGridViewCellStyle3
        Me.contract.HeaderText = "Contract"
        Me.contract.Name = "contract"
        Me.contract.ReadOnly = True
        Me.contract.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'val_inlakh
        '
        Me.val_inlakh.DataPropertyName = "val_inlakh"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.NullValue = "##0.00"
        Me.val_inlakh.DefaultCellStyle = DataGridViewCellStyle4
        Me.val_inlakh.HeaderText = "Val InLakh"
        Me.val_inlakh.Name = "val_inlakh"
        Me.val_inlakh.ReadOnly = True
        Me.val_inlakh.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'vol1
        '
        Me.vol1.DataPropertyName = "vol"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "##0.00"
        Me.vol1.DefaultCellStyle = DataGridViewCellStyle5
        Me.vol1.HeaderText = "Volatility (%)"
        Me.vol1.Name = "vol1"
        Me.vol1.ReadOnly = True
        Me.vol1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.vol1.Width = 105
        '
        'option_type
        '
        Me.option_type.DataPropertyName = "option_type"
        Me.option_type.HeaderText = "Option Type"
        Me.option_type.Name = "option_type"
        Me.option_type.ReadOnly = True
        Me.option_type.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.option_type.Visible = False
        '
        'strike
        '
        Me.strike.DataPropertyName = "strike"
        Me.strike.HeaderText = "Strike"
        Me.strike.Name = "strike"
        Me.strike.ReadOnly = True
        Me.strike.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.strike.Visible = False
        '
        'INSTRUMENT
        '
        Me.INSTRUMENT.DataPropertyName = "INSTRUMENT"
        Me.INSTRUMENT.HeaderText = "INSTRUMENT"
        Me.INSTRUMENT.Name = "INSTRUMENT"
        Me.INSTRUMENT.ReadOnly = True
        Me.INSTRUMENT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.INSTRUMENT.Visible = False
        '
        'entry_date
        '
        Me.entry_date.DataPropertyName = "entry_date"
        Me.entry_date.HeaderText = "Entry Date"
        Me.entry_date.Name = "entry_date"
        Me.entry_date.ReadOnly = True
        Me.entry_date.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.entry_date.Visible = False
        '
        'futval
        '
        Me.futval.DataPropertyName = "futval"
        Me.futval.HeaderText = "Fut Val"
        Me.futval.Name = "futval"
        Me.futval.ReadOnly = True
        Me.futval.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.futval.Visible = False
        '
        'mt
        '
        Me.mt.DataPropertyName = "mt"
        Me.mt.HeaderText = "Mt"
        Me.mt.Name = "mt"
        Me.mt.ReadOnly = True
        Me.mt.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.mt.Visible = False
        '
        'uid
        '
        Me.uid.DataPropertyName = "uid"
        Me.uid.HeaderText = "uid"
        Me.uid.Name = "uid"
        Me.uid.ReadOnly = True
        Me.uid.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.uid.Visible = False
        '
        'cmbsymbol
        '
        Me.cmbsymbol.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cmbsymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsymbol.FormattingEnabled = True
        Me.cmbsymbol.Location = New System.Drawing.Point(183, 12)
        Me.cmbsymbol.Name = "cmbsymbol"
        Me.cmbsymbol.Size = New System.Drawing.Size(112, 21)
        Me.cmbsymbol.TabIndex = 1
        '
        'cmdshow
        '
        Me.cmdshow.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmdshow.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdshow.FlatAppearance.BorderSize = 3
        Me.cmdshow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdshow.ForeColor = System.Drawing.SystemColors.Control
        Me.cmdshow.Location = New System.Drawing.Point(792, 48)
        Me.cmdshow.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdshow.Name = "cmdshow"
        Me.cmdshow.Size = New System.Drawing.Size(65, 27)
        Me.cmdshow.TabIndex = 13
        Me.cmdshow.Text = "Show"
        Me.cmdshow.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label2.Location = New System.Drawing.Point(166, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = ":"
        '
        'cmbexp
        '
        Me.cmbexp.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cmbexp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbexp.FormattingEnabled = True
        Me.cmbexp.Location = New System.Drawing.Point(183, 58)
        Me.cmbexp.Name = "cmbexp"
        Me.cmbexp.Size = New System.Drawing.Size(112, 21)
        Me.cmbexp.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label3.Location = New System.Drawing.Point(166, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = ":"
        '
        'cmbopt
        '
        Me.cmbopt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cmbopt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbopt.FormattingEnabled = True
        Me.cmbopt.Location = New System.Drawing.Point(183, 35)
        Me.cmbopt.Name = "cmbopt"
        Me.cmbopt.Size = New System.Drawing.Size(112, 21)
        Me.cmbopt.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label5.Location = New System.Drawing.Point(166, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = ":"
        '
        'cmbentry
        '
        Me.cmbentry.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cmbentry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbentry.FormattingEnabled = True
        Me.cmbentry.Location = New System.Drawing.Point(183, 81)
        Me.cmbentry.Name = "cmbentry"
        Me.cmbentry.Size = New System.Drawing.Size(112, 21)
        Me.cmbentry.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label8.Location = New System.Drawing.Point(33, 84)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Bhavcopy Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label7.Location = New System.Drawing.Point(166, 84)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(11, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = ":"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label10.Location = New System.Drawing.Point(301, 38)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(116, 13)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "Volatility Above (%)"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label9.Location = New System.Drawing.Point(432, 38)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(11, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = ":"
        '
        'txtvol
        '
        Me.txtvol.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtvol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtvol.Location = New System.Drawing.Point(449, 36)
        Me.txtvol.Name = "txtvol"
        Me.txtvol.Size = New System.Drawing.Size(67, 20)
        Me.txtvol.TabIndex = 9
        Me.txtvol.Text = "0"
        Me.txtvol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label12.Location = New System.Drawing.Point(301, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(95, 13)
        Me.Label12.TabIndex = 17
        Me.Label12.Text = "Contract Above"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label11.Location = New System.Drawing.Point(432, 15)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(11, 13)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = ":"
        '
        'txtcontract
        '
        Me.txtcontract.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtcontract.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcontract.Location = New System.Drawing.Point(449, 14)
        Me.txtcontract.Name = "txtcontract"
        Me.txtcontract.Size = New System.Drawing.Size(67, 20)
        Me.txtcontract.TabIndex = 7
        Me.txtcontract.Text = "0"
        Me.txtcontract.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label14.Location = New System.Drawing.Point(301, 61)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(125, 13)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "Value in Lakh Above"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label13.Location = New System.Drawing.Point(432, 61)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(11, 13)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = ":"
        '
        'txtvalue
        '
        Me.txtvalue.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtvalue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtvalue.Location = New System.Drawing.Point(449, 58)
        Me.txtvalue.Name = "txtvalue"
        Me.txtvalue.Size = New System.Drawing.Size(67, 20)
        Me.txtvalue.TabIndex = 11
        Me.txtvalue.Text = "0"
        Me.txtvalue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chksymbol
        '
        Me.chksymbol.AutoSize = True
        Me.chksymbol.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chksymbol.ForeColor = System.Drawing.SystemColors.WindowText
        Me.chksymbol.Location = New System.Drawing.Point(19, 14)
        Me.chksymbol.Name = "chksymbol"
        Me.chksymbol.Size = New System.Drawing.Size(130, 17)
        Me.chksymbol.TabIndex = 0
        Me.chksymbol.Text = "Check All Security"
        Me.chksymbol.UseVisualStyleBackColor = True
        '
        'chkopt
        '
        Me.chkopt.AutoSize = True
        Me.chkopt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkopt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.chkopt.Location = New System.Drawing.Point(19, 37)
        Me.chkopt.Name = "chkopt"
        Me.chkopt.Size = New System.Drawing.Size(153, 17)
        Me.chkopt.TabIndex = 2
        Me.chkopt.Text = "Check All Option Type"
        Me.chkopt.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label4.Location = New System.Drawing.Point(522, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(114, 13)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "Volatility Below (%)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(651, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = ":"
        '
        'txtvolb
        '
        Me.txtvolb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtvolb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtvolb.Location = New System.Drawing.Point(668, 36)
        Me.txtvolb.Name = "txtvolb"
        Me.txtvolb.Size = New System.Drawing.Size(66, 20)
        Me.txtvolb.TabIndex = 10
        Me.txtvolb.Text = "400"
        Me.txtvolb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label15.Location = New System.Drawing.Point(522, 15)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(93, 13)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "Contract Below"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label6.Location = New System.Drawing.Point(651, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(11, 13)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = ":"
        '
        'txtcontractb
        '
        Me.txtcontractb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtcontractb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcontractb.Location = New System.Drawing.Point(668, 13)
        Me.txtcontractb.Name = "txtcontractb"
        Me.txtcontractb.Size = New System.Drawing.Size(66, 20)
        Me.txtcontractb.TabIndex = 8
        Me.txtcontractb.Text = "50000000"
        Me.txtcontractb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label17.Location = New System.Drawing.Point(522, 61)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(123, 13)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "Value in Lakh Below"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label16.Location = New System.Drawing.Point(651, 61)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(11, 13)
        Me.Label16.TabIndex = 33
        Me.Label16.Text = ":"
        '
        'chkexp
        '
        Me.chkexp.AutoSize = True
        Me.chkexp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkexp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.chkexp.Location = New System.Drawing.Point(19, 60)
        Me.chkexp.Name = "chkexp"
        Me.chkexp.Size = New System.Drawing.Size(122, 17)
        Me.chkexp.TabIndex = 4
        Me.chkexp.Text = "Check All Expiry "
        Me.chkexp.UseVisualStyleBackColor = True
        '
        'txtvalueb
        '
        Me.txtvalueb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtvalueb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtvalueb.Location = New System.Drawing.Point(668, 59)
        Me.txtvalueb.Name = "txtvalueb"
        Me.txtvalueb.Size = New System.Drawing.Size(66, 20)
        Me.txtvalueb.TabIndex = 12
        Me.txtvalueb.Text = "100000000"
        Me.txtvalueb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.WindowText
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.FlatAppearance.BorderSize = 3
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.SystemColors.Control
        Me.Button1.Location = New System.Drawing.Point(859, 48)
        Me.Button1.Margin = New System.Windows.Forms.Padding(1)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(65, 27)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "Export"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.WindowText
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button2.FlatAppearance.BorderSize = 3
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.SystemColors.Control
        Me.Button2.Location = New System.Drawing.Point(792, 18)
        Me.Button2.Margin = New System.Windows.Forms.Padding(1)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(133, 27)
        Me.Button2.TabIndex = 34
        Me.Button2.Text = "Process Bhavcopy"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.DataGridView1)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(973, 592)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Button3)
        Me.Panel2.Controls.Add(Me.chksymbol)
        Me.Panel2.Controls.Add(Me.cmbsymbol)
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Controls.Add(Me.cmdshow)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.txtvalueb)
        Me.Panel2.Controls.Add(Me.cmbexp)
        Me.Panel2.Controls.Add(Me.chkexp)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.cmbopt)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.txtcontractb)
        Me.Panel2.Controls.Add(Me.cmbentry)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.txtvolb)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.txtvol)
        Me.Panel2.Controls.Add(Me.chkopt)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.txtvalue)
        Me.Panel2.Controls.Add(Me.txtcontract)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(971, 115)
        Me.Panel2.TabIndex = 0
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.WindowText
        Me.Button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button3.FlatAppearance.BorderSize = 3
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.ForeColor = System.Drawing.SystemColors.Control
        Me.Button3.Location = New System.Drawing.Point(751, 77)
        Me.Button3.Margin = New System.Windows.Forms.Padding(1)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(211, 27)
        Me.Button3.TabIndex = 36
        Me.Button3.Text = "Download  And  Process Bhavcopy"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'bhavcopy
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.WindowText
        Me.ClientSize = New System.Drawing.Size(973, 592)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "bhavcopy"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bhavcopy"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents cmbsymbol As System.Windows.Forms.ComboBox
    Friend WithEvents cmdshow As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbexp As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbopt As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbentry As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtvol As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtcontract As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtvalue As System.Windows.Forms.TextBox
    Friend WithEvents chksymbol As System.Windows.Forms.CheckBox
    Friend WithEvents chkopt As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtvolb As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtcontractb As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chkexp As System.Windows.Forms.CheckBox
    Friend WithEvents txtvalueb As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Script As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents symbol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents exp_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ltp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents contract As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents val_inlakh As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vol1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents option_type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents strike As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INSTRUMENT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents entry_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents futval As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents uid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
