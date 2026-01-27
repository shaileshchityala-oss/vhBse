<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class display_master
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(display_master))
        Me.cmdscen = New System.Windows.Forms.Button
        Me.cmbdate = New System.Windows.Forms.ComboBox
        Me.cmbstrike = New System.Windows.Forms.ComboBox
        Me.cmbcomp = New System.Windows.Forms.ComboBox
        Me.cmbcp = New System.Windows.Forms.ComboBox
        Me.grdvol = New System.Windows.Forms.DataGridView
        Me.status = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.script = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.company = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.expdate1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.strike = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cpf = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.token = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.instrument = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grdport = New System.Windows.Forms.DataGridView
        Me.pstatus = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.port = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ordseq = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmddelete = New System.Windows.Forms.Button
        Me.chkcomp = New System.Windows.Forms.CheckBox
        Me.chkdate = New System.Windows.Forms.CheckBox
        Me.chkstrike = New System.Windows.Forms.CheckBox
        Me.chkcpf = New System.Windows.Forms.CheckBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblportfolio = New System.Windows.Forms.Label
        Me.chkcheck = New System.Windows.Forms.CheckBox
        Me.chkcheck1 = New System.Windows.Forms.CheckBox
        Me.cmdclear = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        CType(Me.grdvol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdscen
        '
        Me.cmdscen.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.cmdscen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdscen.FlatAppearance.BorderSize = 3
        Me.cmdscen.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmdscen.Location = New System.Drawing.Point(792, 103)
        Me.cmdscen.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdscen.Name = "cmdscen"
        Me.cmdscen.Size = New System.Drawing.Size(81, 26)
        Me.cmdscen.TabIndex = 5
        Me.cmdscen.Text = "Save Portfolio"
        Me.cmdscen.UseVisualStyleBackColor = False
        '
        'cmbdate
        '
        Me.cmbdate.BackColor = System.Drawing.SystemColors.Window
        Me.cmbdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbdate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbdate.Location = New System.Drawing.Point(226, 56)
        Me.cmbdate.Name = "cmbdate"
        Me.cmbdate.Size = New System.Drawing.Size(123, 22)
        Me.cmbdate.TabIndex = 1
        '
        'cmbstrike
        '
        Me.cmbstrike.BackColor = System.Drawing.SystemColors.Window
        Me.cmbstrike.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbstrike.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbstrike.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbstrike.Location = New System.Drawing.Point(354, 56)
        Me.cmbstrike.Name = "cmbstrike"
        Me.cmbstrike.Size = New System.Drawing.Size(118, 22)
        Me.cmbstrike.TabIndex = 2
        '
        'cmbcomp
        '
        Me.cmbcomp.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcomp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbcomp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcomp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcomp.Items.AddRange(New Object() {"Select Instrument", "OPTIDX", "OPTSTK", "FUTIDX", "FUTSTK", "FUTINT", "FUTIVX"})
        Me.cmbcomp.Location = New System.Drawing.Point(45, 56)
        Me.cmbcomp.Name = "cmbcomp"
        Me.cmbcomp.Size = New System.Drawing.Size(175, 22)
        Me.cmbcomp.TabIndex = 0
        '
        'cmbcp
        '
        Me.cmbcp.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcp.Location = New System.Drawing.Point(477, 56)
        Me.cmbcp.Name = "cmbcp"
        Me.cmbcp.Size = New System.Drawing.Size(107, 22)
        Me.cmbcp.TabIndex = 3
        '
        'grdvol
        '
        Me.grdvol.AllowUserToAddRows = False
        Me.grdvol.AllowUserToDeleteRows = False
        Me.grdvol.AllowUserToResizeColumns = False
        Me.grdvol.AllowUserToResizeRows = False
        Me.grdvol.BackgroundColor = System.Drawing.SystemColors.WindowText
        Me.grdvol.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvol.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdvol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdvol.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.status, Me.script, Me.company, Me.expdate1, Me.strike, Me.cpf, Me.token, Me.instrument})
        Me.grdvol.GridColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.grdvol.Location = New System.Drawing.Point(0, 85)
        Me.grdvol.MultiSelect = False
        Me.grdvol.Name = "grdvol"
        Me.grdvol.RowHeadersVisible = False
        Me.grdvol.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdvol.Size = New System.Drawing.Size(789, 273)
        Me.grdvol.TabIndex = 33
        '
        'status
        '
        Me.status.DataPropertyName = "status"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.NullValue = "True"
        Me.status.DefaultCellStyle = DataGridViewCellStyle2
        Me.status.HeaderText = ""
        Me.status.Name = "status"
        Me.status.Width = 35
        '
        'script
        '
        Me.script.DataPropertyName = "script"
        Me.script.HeaderText = "Scrip"
        Me.script.Name = "script"
        Me.script.ReadOnly = True
        Me.script.Width = 250
        '
        'company
        '
        Me.company.DataPropertyName = "symbol"
        Me.company.HeaderText = "Security"
        Me.company.Name = "company"
        Me.company.ReadOnly = True
        Me.company.Width = 200
        '
        'expdate1
        '
        Me.expdate1.DataPropertyName = "expdate1"
        Me.expdate1.HeaderText = "Exp. Date"
        Me.expdate1.Name = "expdate1"
        Me.expdate1.ReadOnly = True
        Me.expdate1.Width = 120
        '
        'strike
        '
        Me.strike.DataPropertyName = "strike_price"
        Me.strike.HeaderText = "Strike"
        Me.strike.Name = "strike"
        Me.strike.ReadOnly = True
        Me.strike.Width = 120
        '
        'cpf
        '
        Me.cpf.DataPropertyName = "option_type"
        Me.cpf.HeaderText = "CPF"
        Me.cpf.Name = "cpf"
        Me.cpf.ReadOnly = True
        Me.cpf.Width = 40
        '
        'token
        '
        Me.token.DataPropertyName = "token"
        Me.token.HeaderText = "token"
        Me.token.Name = "token"
        Me.token.ReadOnly = True
        Me.token.Width = 60
        '
        'instrument
        '
        Me.instrument.DataPropertyName = "instrumentname"
        Me.instrument.HeaderText = "instrument"
        Me.instrument.Name = "instrument"
        Me.instrument.ReadOnly = True
        Me.instrument.Visible = False
        Me.instrument.Width = 5
        '
        'grdport
        '
        Me.grdport.AllowUserToAddRows = False
        Me.grdport.AllowUserToDeleteRows = False
        Me.grdport.AllowUserToResizeColumns = False
        Me.grdport.AllowUserToResizeRows = False
        Me.grdport.BackgroundColor = System.Drawing.SystemColors.WindowText
        Me.grdport.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdport.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdport.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.pstatus, Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.port, Me.ordseq})
        Me.grdport.GridColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.grdport.Location = New System.Drawing.Point(0, 360)
        Me.grdport.MultiSelect = False
        Me.grdport.Name = "grdport"
        Me.grdport.RowHeadersVisible = False
        Me.grdport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdport.Size = New System.Drawing.Size(789, 259)
        Me.grdport.TabIndex = 34
        '
        'pstatus
        '
        Me.pstatus.DataPropertyName = "status"
        Me.pstatus.HeaderText = ""
        Me.pstatus.Name = "pstatus"
        Me.pstatus.Width = 35
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "script"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Scrip"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 250
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "company"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Security"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 200
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "mdate"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Exp. Date"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 120
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "strike"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Strike"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 120
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "cpf"
        Me.DataGridViewTextBoxColumn5.HeaderText = "CPF"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 40
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "token"
        Me.DataGridViewTextBoxColumn6.HeaderText = "token"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 60
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "instrument"
        Me.DataGridViewTextBoxColumn7.HeaderText = "instrument"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        Me.DataGridViewTextBoxColumn7.Width = 5
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "uid"
        Me.DataGridViewTextBoxColumn8.HeaderText = "uid"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 5
        '
        'port
        '
        Me.port.DataPropertyName = "Portfolio"
        Me.port.HeaderText = "Portfolio"
        Me.port.Name = "port"
        Me.port.ReadOnly = True
        Me.port.Visible = False
        '
        'ordseq
        '
        Me.ordseq.DataPropertyName = "ordseq"
        Me.ordseq.HeaderText = "ordseq"
        Me.ordseq.Name = "ordseq"
        Me.ordseq.ReadOnly = True
        Me.ordseq.Visible = False
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.cmddelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmddelete.FlatAppearance.BorderSize = 3
        Me.cmddelete.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmddelete.Location = New System.Drawing.Point(792, 369)
        Me.cmddelete.Margin = New System.Windows.Forms.Padding(1)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(81, 26)
        Me.cmddelete.TabIndex = 7
        Me.cmddelete.Text = "Delete Script"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'chkcomp
        '
        Me.chkcomp.AutoSize = True
        Me.chkcomp.Checked = True
        Me.chkcomp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkcomp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcomp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.chkcomp.Location = New System.Drawing.Point(89, 37)
        Me.chkcomp.Name = "chkcomp"
        Me.chkcomp.Size = New System.Drawing.Size(81, 18)
        Me.chkcomp.TabIndex = 36
        Me.chkcomp.Text = "Security"
        Me.chkcomp.UseVisualStyleBackColor = True
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.Checked = True
        Me.chkdate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.chkdate.Location = New System.Drawing.Point(259, 37)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(68, 18)
        Me.chkdate.TabIndex = 37
        Me.chkdate.Text = "Expiry"
        Me.chkdate.UseVisualStyleBackColor = True
        '
        'chkstrike
        '
        Me.chkstrike.AutoSize = True
        Me.chkstrike.Checked = True
        Me.chkstrike.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkstrike.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkstrike.ForeColor = System.Drawing.SystemColors.WindowText
        Me.chkstrike.Location = New System.Drawing.Point(363, 37)
        Me.chkstrike.Name = "chkstrike"
        Me.chkstrike.Size = New System.Drawing.Size(100, 18)
        Me.chkstrike.TabIndex = 38
        Me.chkstrike.Text = "Strike Rate"
        Me.chkstrike.UseVisualStyleBackColor = True
        '
        'chkcpf
        '
        Me.chkcpf.AutoSize = True
        Me.chkcpf.Checked = True
        Me.chkcpf.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkcpf.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcpf.ForeColor = System.Drawing.SystemColors.WindowText
        Me.chkcpf.Location = New System.Drawing.Point(482, 37)
        Me.chkcpf.Name = "chkcpf"
        Me.chkcpf.Size = New System.Drawing.Size(81, 18)
        Me.chkcpf.TabIndex = 39
        Me.chkcpf.Text = "Call/Put"
        Me.chkcpf.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.FlatAppearance.BorderSize = 3
        Me.Button1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Button1.Location = New System.Drawing.Point(590, 56)
        Me.Button1.Margin = New System.Windows.Forms.Padding(1)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(81, 26)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Show"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 14)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Portfolio"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label2.Location = New System.Drawing.Point(86, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(12, 14)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = ":"
        '
        'lblportfolio
        '
        Me.lblportfolio.AutoSize = True
        Me.lblportfolio.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblportfolio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblportfolio.Location = New System.Drawing.Point(104, 9)
        Me.lblportfolio.Name = "lblportfolio"
        Me.lblportfolio.Size = New System.Drawing.Size(13, 14)
        Me.lblportfolio.TabIndex = 43
        Me.lblportfolio.Text = "-"
        '
        'chkcheck
        '
        Me.chkcheck.Location = New System.Drawing.Point(15, 90)
        Me.chkcheck.Name = "chkcheck"
        Me.chkcheck.Size = New System.Drawing.Size(13, 13)
        Me.chkcheck.TabIndex = 44
        Me.chkcheck.UseVisualStyleBackColor = True
        '
        'chkcheck1
        '
        Me.chkcheck1.Location = New System.Drawing.Point(12, 364)
        Me.chkcheck1.Name = "chkcheck1"
        Me.chkcheck1.Size = New System.Drawing.Size(13, 13)
        Me.chkcheck1.TabIndex = 45
        Me.chkcheck1.UseVisualStyleBackColor = True
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.cmdclear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdclear.FlatAppearance.BorderSize = 3
        Me.cmdclear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmdclear.Location = New System.Drawing.Point(792, 132)
        Me.cmdclear.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(81, 26)
        Me.cmdclear.TabIndex = 6
        Me.cmdclear.Text = "Clear"
        Me.cmdclear.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmdexit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdexit.FlatAppearance.BorderSize = 3
        Me.cmdexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdexit.ForeColor = System.Drawing.SystemColors.Window
        Me.cmdexit.Location = New System.Drawing.Point(794, 593)
        Me.cmdexit.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(81, 26)
        Me.cmdexit.TabIndex = 8
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'display_master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.ClientSize = New System.Drawing.Size(875, 619)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdclear)
        Me.Controls.Add(Me.chkcheck1)
        Me.Controls.Add(Me.chkcheck)
        Me.Controls.Add(Me.lblportfolio)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.chkcpf)
        Me.Controls.Add(Me.chkstrike)
        Me.Controls.Add(Me.chkdate)
        Me.Controls.Add(Me.chkcomp)
        Me.Controls.Add(Me.cmddelete)
        Me.Controls.Add(Me.grdport)
        Me.Controls.Add(Me.grdvol)
        Me.Controls.Add(Me.cmdscen)
        Me.Controls.Add(Me.cmbdate)
        Me.Controls.Add(Me.cmbstrike)
        Me.Controls.Add(Me.cmbcomp)
        Me.Controls.Add(Me.cmbcp)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "display_master"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add/Edit Data to market watch"
        CType(Me.grdvol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdscen As System.Windows.Forms.Button
    Friend WithEvents cmbdate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbstrike As System.Windows.Forms.ComboBox
    Friend WithEvents cmbcomp As System.Windows.Forms.ComboBox
    Friend WithEvents cmbcp As System.Windows.Forms.ComboBox
    Friend WithEvents grdvol As System.Windows.Forms.DataGridView
    Friend WithEvents grdport As System.Windows.Forms.DataGridView
    Friend WithEvents cmddelete As System.Windows.Forms.Button
    Friend WithEvents chkcomp As System.Windows.Forms.CheckBox
    Friend WithEvents chkdate As System.Windows.Forms.CheckBox
    Friend WithEvents chkstrike As System.Windows.Forms.CheckBox
    Friend WithEvents chkcpf As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblportfolio As System.Windows.Forms.Label
    Friend WithEvents chkcheck As System.Windows.Forms.CheckBox
    Friend WithEvents chkcheck1 As System.Windows.Forms.CheckBox
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents status As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents script As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents expdate1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents strike As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cpf As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents instrument As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pstatus As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents port As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ordseq As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
