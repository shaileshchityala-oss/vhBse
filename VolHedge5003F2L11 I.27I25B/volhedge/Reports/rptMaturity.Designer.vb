<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptMaturity
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptMaturity))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbentry = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.grdtrad = New System.Windows.Forms.DataGridView()
        Me.script = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.instrument = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cpf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mdate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.strike = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ATP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.entrydate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.exp_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.grdtrad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.Controls.Add(Me.cmdShow)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.cmbentry)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Location = New System.Drawing.Point(2, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1016, 43)
        Me.Panel1.TabIndex = 41
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmdShow.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdShow.FlatAppearance.BorderSize = 3
        Me.cmdShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdShow.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Location = New System.Drawing.Point(334, 7)
        Me.cmdShow.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.Size = New System.Drawing.Size(126, 28)
        Me.cmdShow.TabIndex = 39
        Me.cmdShow.Text = "&Show"
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label8.Location = New System.Drawing.Point(13, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 13)
        Me.Label8.TabIndex = 38
        Me.Label8.Text = "Bhavcopy Date"
        '
        'cmbentry
        '
        Me.cmbentry.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cmbentry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbentry.FormattingEnabled = True
        Me.cmbentry.Location = New System.Drawing.Point(115, 11)
        Me.cmbentry.Name = "cmbentry"
        Me.cmbentry.Size = New System.Drawing.Size(210, 21)
        Me.cmbentry.TabIndex = 37
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.WindowText
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.FlatAppearance.BorderSize = 3
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.Control
        Me.Button1.Location = New System.Drawing.Point(516, 7)
        Me.Button1.Margin = New System.Windows.Forms.Padding(1)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(126, 28)
        Me.Button1.TabIndex = 36
        Me.Button1.Text = "Export F&&O(F11)"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'grdtrad
        '
        Me.grdtrad.AllowUserToAddRows = False
        Me.grdtrad.AllowUserToDeleteRows = False
        Me.grdtrad.AllowUserToOrderColumns = True
        Me.grdtrad.AllowUserToResizeColumns = False
        Me.grdtrad.AllowUserToResizeRows = False
        Me.grdtrad.BackgroundColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdtrad.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdtrad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdtrad.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.script, Me.instrument, Me.company, Me.cpf, Me.mdate, Me.strike, Me.qty, Me.ATP, Me.entrydate, Me.exp_date})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdtrad.DefaultCellStyle = DataGridViewCellStyle6
        Me.grdtrad.GridColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.grdtrad.Location = New System.Drawing.Point(-3, 44)
        Me.grdtrad.Name = "grdtrad"
        Me.grdtrad.ReadOnly = True
        Me.grdtrad.RowHeadersVisible = False
        Me.grdtrad.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdtrad.Size = New System.Drawing.Size(1024, 558)
        Me.grdtrad.TabIndex = 39
        '
        'script
        '
        Me.script.DataPropertyName = "script"
        Me.script.HeaderText = "Scrip"
        Me.script.Name = "script"
        Me.script.ReadOnly = True
        Me.script.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.script.Width = 300
        '
        'instrument
        '
        Me.instrument.DataPropertyName = "instrument"
        Me.instrument.HeaderText = "Instrument"
        Me.instrument.Name = "instrument"
        Me.instrument.ReadOnly = True
        Me.instrument.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'company
        '
        Me.company.DataPropertyName = "company"
        Me.company.HeaderText = "Security"
        Me.company.Name = "company"
        Me.company.ReadOnly = True
        Me.company.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.company.Width = 130
        '
        'cpf
        '
        Me.cpf.DataPropertyName = "cpf"
        Me.cpf.HeaderText = "CPF"
        Me.cpf.Name = "cpf"
        Me.cpf.ReadOnly = True
        Me.cpf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cpf.Width = 40
        '
        'mdate
        '
        Me.mdate.DataPropertyName = "mdate"
        DataGridViewCellStyle2.Format = "dd-MMM"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.mdate.DefaultCellStyle = DataGridViewCellStyle2
        Me.mdate.HeaderText = "Exp. Date"
        Me.mdate.Name = "mdate"
        Me.mdate.ReadOnly = True
        Me.mdate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.mdate.Width = 80
        '
        'strike
        '
        Me.strike.DataPropertyName = "strike"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.strike.DefaultCellStyle = DataGridViewCellStyle3
        Me.strike.HeaderText = "Strike"
        Me.strike.Name = "strike"
        Me.strike.ReadOnly = True
        Me.strike.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.strike.Width = 70
        '
        'qty
        '
        Me.qty.DataPropertyName = "qty"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.qty.DefaultCellStyle = DataGridViewCellStyle4
        Me.qty.HeaderText = "Qty"
        Me.qty.Name = "qty"
        Me.qty.ReadOnly = True
        Me.qty.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'ATP
        '
        Me.ATP.DataPropertyName = "atp"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.ATP.DefaultCellStyle = DataGridViewCellStyle5
        Me.ATP.HeaderText = "ATP"
        Me.ATP.Name = "ATP"
        Me.ATP.ReadOnly = True
        Me.ATP.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'entrydate
        '
        Me.entrydate.DataPropertyName = "entrydate"
        Me.entrydate.HeaderText = "Entrydate"
        Me.entrydate.Name = "entrydate"
        Me.entrydate.ReadOnly = True
        Me.entrydate.Visible = False
        '
        'exp_date
        '
        Me.exp_date.DataPropertyName = "mdate"
        Me.exp_date.HeaderText = "exp_date"
        Me.exp_date.Name = "exp_date"
        Me.exp_date.ReadOnly = True
        Me.exp_date.Visible = False
        '
        'rptMaturity
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(952, 602)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdtrad)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "rptMaturity"
        Me.Text = "Maturity Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grdtrad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents grdtrad As System.Windows.Forms.DataGridView
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbentry As System.Windows.Forms.ComboBox
    Friend WithEvents cmdShow As System.Windows.Forms.Button
    Friend WithEvents script As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents instrument As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cpf As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mdate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents strike As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ATP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents entrydate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents exp_date As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
