<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class findvol
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(findvol))
        Me.grdvol = New System.Windows.Forms.DataGridView
        Me.company = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cpf = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.mdate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.strike = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.buyprice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.buyvol = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.futbuy = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.saleprice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.salevol = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.futsale = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ltp = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ltpvol = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.futltp = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.instrument = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.script = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.uid = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.portfolio = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.token = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ftoken = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ordseq = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txtportfolio = New System.Windows.Forms.TextBox
        Me.cmbport = New System.Windows.Forms.ComboBox
        Me.chkport = New System.Windows.Forms.CheckBox
        Me.cmdport = New System.Windows.Forms.Button
        Me.cmdstart = New System.Windows.Forms.Button
        Me.cmdord = New System.Windows.Forms.Button
        Me.grdmvol = New System.Windows.Forms.DataGridView
        Me.Symbol = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cpf1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Expiry = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Strike1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.buyprice2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.buyvol1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.futbuy1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.saleprice2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.salevol1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.futsale1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ltp1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ltpvol1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.futltp1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.instrument1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.script1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.uid1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.portfolio1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.token1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ftoken1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ordseq1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Timer_Calculation = New System.Windows.Forms.Timer(Me.components)
        CType(Me.grdvol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdmvol, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdvol
        '
        Me.grdvol.AllowUserToAddRows = False
        Me.grdvol.AllowUserToDeleteRows = False
        Me.grdvol.AllowUserToResizeRows = False
        Me.grdvol.BackgroundColor = System.Drawing.SystemColors.ControlText
        Me.grdvol.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvol.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdvol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdvol.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.company, Me.cpf, Me.mdate, Me.strike, Me.buyprice, Me.buyvol, Me.futbuy, Me.saleprice, Me.salevol, Me.futsale, Me.ltp, Me.ltpvol, Me.futltp, Me.instrument, Me.script, Me.uid, Me.portfolio, Me.token, Me.ftoken, Me.ordseq})
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdvol.DefaultCellStyle = DataGridViewCellStyle15
        Me.grdvol.GridColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.grdvol.Location = New System.Drawing.Point(1, 38)
        Me.grdvol.MultiSelect = False
        Me.grdvol.Name = "grdvol"
        Me.grdvol.RowHeadersVisible = False
        Me.grdvol.RowHeadersWidth = 15
        Me.grdvol.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdvol.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdvol.Size = New System.Drawing.Size(758, 627)
        Me.grdvol.TabIndex = 4
        '
        'company
        '
        Me.company.DataPropertyName = "company"
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.company.DefaultCellStyle = DataGridViewCellStyle2
        Me.company.HeaderText = "Security"
        Me.company.Name = "company"
        Me.company.ReadOnly = True
        Me.company.Width = 130
        '
        'cpf
        '
        Me.cpf.DataPropertyName = "cpf"
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cpf.DefaultCellStyle = DataGridViewCellStyle3
        Me.cpf.HeaderText = "CPF"
        Me.cpf.Name = "cpf"
        Me.cpf.ReadOnly = True
        Me.cpf.Width = 40
        '
        'mdate
        '
        Me.mdate.DataPropertyName = "mdate"
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.Format = "d"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.mdate.DefaultCellStyle = DataGridViewCellStyle4
        Me.mdate.HeaderText = "Expiry"
        Me.mdate.Name = "mdate"
        Me.mdate.ReadOnly = True
        Me.mdate.Width = 80
        '
        'strike
        '
        Me.strike.DataPropertyName = "strike"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.strike.DefaultCellStyle = DataGridViewCellStyle5
        Me.strike.HeaderText = "Strike Rate"
        Me.strike.Name = "strike"
        Me.strike.ReadOnly = True
        Me.strike.Width = 70
        '
        'buyprice
        '
        Me.buyprice.DataPropertyName = "buyprice"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.buyprice.DefaultCellStyle = DataGridViewCellStyle6
        Me.buyprice.HeaderText = "Bid"
        Me.buyprice.Name = "buyprice"
        Me.buyprice.ReadOnly = True
        Me.buyprice.Width = 75
        '
        'buyvol
        '
        Me.buyvol.DataPropertyName = "buyvol"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.buyvol.DefaultCellStyle = DataGridViewCellStyle7
        Me.buyvol.HeaderText = "Bid Vol(%)"
        Me.buyvol.Name = "buyvol"
        Me.buyvol.ReadOnly = True
        Me.buyvol.Width = 70
        '
        'futbuy
        '
        Me.futbuy.DataPropertyName = "futbuy"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N2"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.futbuy.DefaultCellStyle = DataGridViewCellStyle8
        Me.futbuy.HeaderText = "Fut Buy"
        Me.futbuy.Name = "futbuy"
        Me.futbuy.ReadOnly = True
        Me.futbuy.Visible = False
        Me.futbuy.Width = 70
        '
        'saleprice
        '
        Me.saleprice.DataPropertyName = "saleprice"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.saleprice.DefaultCellStyle = DataGridViewCellStyle9
        Me.saleprice.HeaderText = "Ask"
        Me.saleprice.Name = "saleprice"
        Me.saleprice.ReadOnly = True
        Me.saleprice.Width = 80
        '
        'salevol
        '
        Me.salevol.DataPropertyName = "salevol"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N2"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.salevol.DefaultCellStyle = DataGridViewCellStyle10
        Me.salevol.HeaderText = "Ask Vol(%)"
        Me.salevol.Name = "salevol"
        Me.salevol.ReadOnly = True
        Me.salevol.Width = 80
        '
        'futsale
        '
        Me.futsale.DataPropertyName = "futsale"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N2"
        Me.futsale.DefaultCellStyle = DataGridViewCellStyle11
        Me.futsale.HeaderText = "Fut Sale"
        Me.futsale.Name = "futsale"
        Me.futsale.ReadOnly = True
        Me.futsale.Visible = False
        Me.futsale.Width = 80
        '
        'ltp
        '
        Me.ltp.DataPropertyName = "ltp"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N2"
        Me.ltp.DefaultCellStyle = DataGridViewCellStyle12
        Me.ltp.HeaderText = "LTP"
        Me.ltp.Name = "ltp"
        Me.ltp.ReadOnly = True
        Me.ltp.Width = 70
        '
        'ltpvol
        '
        Me.ltpvol.DataPropertyName = "ltpvol"
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N2"
        Me.ltpvol.DefaultCellStyle = DataGridViewCellStyle13
        Me.ltpvol.HeaderText = "LTP Vol(%)"
        Me.ltpvol.Name = "ltpvol"
        Me.ltpvol.ReadOnly = True
        Me.ltpvol.Width = 70
        '
        'futltp
        '
        Me.futltp.DataPropertyName = "futltp"
        DataGridViewCellStyle14.Format = "N2"
        Me.futltp.DefaultCellStyle = DataGridViewCellStyle14
        Me.futltp.HeaderText = "Futltp"
        Me.futltp.Name = "futltp"
        Me.futltp.ReadOnly = True
        Me.futltp.Visible = False
        Me.futltp.Width = 70
        '
        'instrument
        '
        Me.instrument.DataPropertyName = "instrument"
        Me.instrument.HeaderText = "instrument"
        Me.instrument.Name = "instrument"
        Me.instrument.ReadOnly = True
        Me.instrument.Visible = False
        Me.instrument.Width = 5
        '
        'script
        '
        Me.script.DataPropertyName = "script"
        Me.script.HeaderText = "script"
        Me.script.Name = "script"
        Me.script.ReadOnly = True
        Me.script.Visible = False
        Me.script.Width = 5
        '
        'uid
        '
        Me.uid.DataPropertyName = "uid"
        Me.uid.HeaderText = "uid"
        Me.uid.Name = "uid"
        Me.uid.ReadOnly = True
        Me.uid.Visible = False
        Me.uid.Width = 5
        '
        'portfolio
        '
        Me.portfolio.DataPropertyName = "portfolio"
        Me.portfolio.HeaderText = "portfolio"
        Me.portfolio.Name = "portfolio"
        Me.portfolio.ReadOnly = True
        Me.portfolio.Visible = False
        '
        'token
        '
        Me.token.DataPropertyName = "token"
        Me.token.HeaderText = "token"
        Me.token.Name = "token"
        Me.token.ReadOnly = True
        Me.token.Visible = False
        '
        'ftoken
        '
        Me.ftoken.DataPropertyName = "ftoken"
        Me.ftoken.HeaderText = "ftoken"
        Me.ftoken.Name = "ftoken"
        Me.ftoken.ReadOnly = True
        Me.ftoken.Visible = False
        '
        'ordseq
        '
        Me.ordseq.DataPropertyName = "ordseq"
        Me.ordseq.HeaderText = "ordseq"
        Me.ordseq.Name = "ordseq"
        Me.ordseq.ReadOnly = True
        Me.ordseq.Visible = False
        Me.ordseq.Width = 5
        '
        'txtportfolio
        '
        Me.txtportfolio.BackColor = System.Drawing.Color.White
        Me.txtportfolio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtportfolio.Enabled = False
        Me.txtportfolio.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtportfolio.Location = New System.Drawing.Point(148, 6)
        Me.txtportfolio.Name = "txtportfolio"
        Me.txtportfolio.Size = New System.Drawing.Size(217, 22)
        Me.txtportfolio.TabIndex = 1
        '
        'cmbport
        '
        Me.cmbport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbport.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbport.FormattingEnabled = True
        Me.cmbport.Location = New System.Drawing.Point(369, 6)
        Me.cmbport.Name = "cmbport"
        Me.cmbport.Size = New System.Drawing.Size(228, 22)
        Me.cmbport.TabIndex = 2
        '
        'chkport
        '
        Me.chkport.AutoSize = True
        Me.chkport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkport.ForeColor = System.Drawing.SystemColors.WindowText
        Me.chkport.Location = New System.Drawing.Point(3, 9)
        Me.chkport.Name = "chkport"
        Me.chkport.Size = New System.Drawing.Size(143, 17)
        Me.chkport.TabIndex = 0
        Me.chkport.Text = "Create New Portfolio"
        Me.chkport.UseVisualStyleBackColor = True
        '
        'cmdport
        '
        Me.cmdport.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmdport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdport.FlatAppearance.BorderSize = 3
        Me.cmdport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdport.ForeColor = System.Drawing.SystemColors.Control
        Me.cmdport.Location = New System.Drawing.Point(601, 5)
        Me.cmdport.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdport.Name = "cmdport"
        Me.cmdport.Size = New System.Drawing.Size(81, 28)
        Me.cmdport.TabIndex = 3
        Me.cmdport.Text = "Add Portfolio"
        Me.cmdport.UseVisualStyleBackColor = False
        '
        'cmdstart
        '
        Me.cmdstart.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmdstart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdstart.FlatAppearance.BorderSize = 3
        Me.cmdstart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdstart.ForeColor = System.Drawing.SystemColors.Control
        Me.cmdstart.Location = New System.Drawing.Point(684, 4)
        Me.cmdstart.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdstart.Name = "cmdstart"
        Me.cmdstart.Size = New System.Drawing.Size(71, 28)
        Me.cmdstart.TabIndex = 5
        Me.cmdstart.Text = "Stop"
        Me.cmdstart.UseVisualStyleBackColor = False
        '
        'cmdord
        '
        Me.cmdord.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmdord.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdord.FlatAppearance.BorderSize = 3
        Me.cmdord.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdord.ForeColor = System.Drawing.SystemColors.Control
        Me.cmdord.Location = New System.Drawing.Point(756, 4)
        Me.cmdord.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdord.Name = "cmdord"
        Me.cmdord.Size = New System.Drawing.Size(81, 28)
        Me.cmdord.TabIndex = 6
        Me.cmdord.Text = "Apply Order"
        Me.cmdord.UseVisualStyleBackColor = False
        '
        'grdmvol
        '
        Me.grdmvol.AllowUserToAddRows = False
        Me.grdmvol.AllowUserToDeleteRows = False
        Me.grdmvol.AllowUserToResizeColumns = False
        Me.grdmvol.AllowUserToResizeRows = False
        Me.grdmvol.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.grdmvol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdmvol.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Symbol, Me.cpf1, Me.Expiry, Me.Strike1, Me.buyprice2, Me.buyvol1, Me.futbuy1, Me.saleprice2, Me.salevol1, Me.futsale1, Me.ltp1, Me.ltpvol1, Me.futltp1, Me.instrument1, Me.script1, Me.uid1, Me.portfolio1, Me.token1, Me.ftoken1, Me.ordseq1})
        Me.grdmvol.Location = New System.Drawing.Point(2, 583)
        Me.grdmvol.MultiSelect = False
        Me.grdmvol.Name = "grdmvol"
        Me.grdmvol.RowHeadersVisible = False
        Me.grdmvol.RowHeadersWidth = 15
        Me.grdmvol.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdmvol.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdmvol.Size = New System.Drawing.Size(16, 82)
        Me.grdmvol.TabIndex = 7
        Me.grdmvol.Visible = False
        '
        'Symbol
        '
        Me.Symbol.DataPropertyName = "company"
        Me.Symbol.HeaderText = "Symbol"
        Me.Symbol.Name = "Symbol"
        Me.Symbol.ReadOnly = True
        '
        'cpf1
        '
        Me.cpf1.DataPropertyName = "cpf"
        Me.cpf1.HeaderText = "CPF"
        Me.cpf1.Name = "cpf1"
        Me.cpf1.ReadOnly = True
        '
        'Expiry
        '
        Me.Expiry.DataPropertyName = "mdate"
        Me.Expiry.HeaderText = "Expiry"
        Me.Expiry.Name = "Expiry"
        Me.Expiry.ReadOnly = True
        '
        'Strike1
        '
        Me.Strike1.DataPropertyName = "strike"
        Me.Strike1.HeaderText = "Strikeprice"
        Me.Strike1.Name = "Strike1"
        Me.Strike1.ReadOnly = True
        '
        'buyprice2
        '
        Me.buyprice2.DataPropertyName = "buyprice"
        Me.buyprice2.HeaderText = "Buy price"
        Me.buyprice2.Name = "buyprice2"
        Me.buyprice2.ReadOnly = True
        '
        'buyvol1
        '
        Me.buyvol1.DataPropertyName = "buyvol"
        Me.buyvol1.HeaderText = "Buy Vol"
        Me.buyvol1.Name = "buyvol1"
        Me.buyvol1.ReadOnly = True
        '
        'futbuy1
        '
        Me.futbuy1.DataPropertyName = "futbuy"
        Me.futbuy1.HeaderText = "Fut Buy"
        Me.futbuy1.Name = "futbuy1"
        Me.futbuy1.ReadOnly = True
        '
        'saleprice2
        '
        Me.saleprice2.DataPropertyName = "saleprice"
        Me.saleprice2.HeaderText = "Sale Price"
        Me.saleprice2.Name = "saleprice2"
        Me.saleprice2.ReadOnly = True
        '
        'salevol1
        '
        Me.salevol1.DataPropertyName = "salevol"
        Me.salevol1.HeaderText = "Sale Vol"
        Me.salevol1.Name = "salevol1"
        Me.salevol1.ReadOnly = True
        '
        'futsale1
        '
        Me.futsale1.DataPropertyName = "futsal"
        Me.futsale1.HeaderText = "Fut Vol"
        Me.futsale1.Name = "futsale1"
        Me.futsale1.ReadOnly = True
        '
        'ltp1
        '
        Me.ltp1.DataPropertyName = "ltp"
        Me.ltp1.HeaderText = "LTP"
        Me.ltp1.Name = "ltp1"
        Me.ltp1.ReadOnly = True
        '
        'ltpvol1
        '
        Me.ltpvol1.DataPropertyName = "ltpvol"
        Me.ltpvol1.HeaderText = "LTP Vol"
        Me.ltpvol1.Name = "ltpvol1"
        Me.ltpvol1.ReadOnly = True
        '
        'futltp1
        '
        Me.futltp1.DataPropertyName = "futltp"
        Me.futltp1.HeaderText = "Fut Ltp"
        Me.futltp1.Name = "futltp1"
        Me.futltp1.ReadOnly = True
        '
        'instrument1
        '
        Me.instrument1.DataPropertyName = "instrument"
        Me.instrument1.HeaderText = "Instrument"
        Me.instrument1.Name = "instrument1"
        Me.instrument1.ReadOnly = True
        Me.instrument1.Visible = False
        '
        'script1
        '
        Me.script1.DataPropertyName = "script"
        Me.script1.HeaderText = "Script1"
        Me.script1.Name = "script1"
        Me.script1.ReadOnly = True
        Me.script1.Visible = False
        '
        'uid1
        '
        Me.uid1.DataPropertyName = "uid"
        Me.uid1.HeaderText = "Uid"
        Me.uid1.Name = "uid1"
        Me.uid1.ReadOnly = True
        Me.uid1.Visible = False
        '
        'portfolio1
        '
        Me.portfolio1.DataPropertyName = "portfolio"
        Me.portfolio1.HeaderText = "Portfolio"
        Me.portfolio1.Name = "portfolio1"
        Me.portfolio1.ReadOnly = True
        Me.portfolio1.Visible = False
        '
        'token1
        '
        Me.token1.DataPropertyName = "token"
        Me.token1.HeaderText = "Token"
        Me.token1.Name = "token1"
        Me.token1.ReadOnly = True
        Me.token1.Visible = False
        '
        'ftoken1
        '
        Me.ftoken1.DataPropertyName = "ftoken"
        Me.ftoken1.HeaderText = "Ftoken"
        Me.ftoken1.Name = "ftoken1"
        Me.ftoken1.ReadOnly = True
        Me.ftoken1.Visible = False
        '
        'ordseq1
        '
        Me.ordseq1.DataPropertyName = "ordseq"
        Me.ordseq1.HeaderText = "ordseq"
        Me.ordseq1.Name = "ordseq1"
        Me.ordseq1.ReadOnly = True
        Me.ordseq1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.Controls.Add(Me.chkport)
        Me.Panel1.Controls.Add(Me.cmdord)
        Me.Panel1.Controls.Add(Me.txtportfolio)
        Me.Panel1.Controls.Add(Me.cmdstart)
        Me.Panel1.Controls.Add(Me.cmbport)
        Me.Panel1.Controls.Add(Me.cmdport)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(839, 33)
        Me.Panel1.TabIndex = 8
        '
        'Timer_Calculation
        '
        Me.Timer_Calculation.Interval = 1000
        '
        'findvol
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.WindowText
        Me.ClientSize = New System.Drawing.Size(839, 671)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdvol)
        Me.Controls.Add(Me.grdmvol)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "findvol"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Market Watch"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdvol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdmvol, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtportfolio As System.Windows.Forms.TextBox
    Friend WithEvents cmbport As System.Windows.Forms.ComboBox
    Friend WithEvents chkport As System.Windows.Forms.CheckBox
    Friend WithEvents cmdport As System.Windows.Forms.Button
    Friend WithEvents cmdstart As System.Windows.Forms.Button
    Friend WithEvents cmdord As System.Windows.Forms.Button
    Friend WithEvents grdmvol As System.Windows.Forms.DataGridView
    Friend WithEvents Symbol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cpf1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Expiry As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Strike1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents buyprice2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents buyvol1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents futbuy1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents saleprice2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents salevol1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents futsale1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ltp1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ltpvol1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents futltp1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents instrument1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents script1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents uid1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents portfolio1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents token1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ftoken1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ordseq1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents grdvol As System.Windows.Forms.DataGridView
    Friend WithEvents company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cpf As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mdate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents strike As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents buyprice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents buyvol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents futbuy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents saleprice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents salevol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents futsale As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ltp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ltpvol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents futltp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents instrument As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents script As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents uid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents portfolio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ftoken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ordseq As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Timer_Calculation As System.Windows.Forms.Timer
End Class
