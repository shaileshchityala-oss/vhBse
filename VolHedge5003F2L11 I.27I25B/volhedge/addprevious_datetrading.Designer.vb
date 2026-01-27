<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class addprevious_datetrading
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(addprevious_datetrading))
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.cmdsave = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtFoDealer = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbFO = New System.Windows.Forms.ComboBox()
        Me.cmdfobr = New System.Windows.Forms.Button()
        Me.txtpath = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdeqclear = New System.Windows.Forms.Button()
        Me.cmdeqsave = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtEqDealer = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbEquity = New System.Windows.Forms.ComboBox()
        Me.cmdeqbr = New System.Windows.Forms.Button()
        Me.txteqpath = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdproc = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtCurrDealer = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbCurr = New System.Windows.Forms.ComboBox()
        Me.cmdCurrclear = New System.Windows.Forms.Button()
        Me.cmdCurrbr = New System.Windows.Forms.Button()
        Me.cmdCurrsave = New System.Windows.Forms.Button()
        Me.txtCurrpath = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdclear
        '
        Me.cmdclear.CausesValidation = False
        Me.cmdclear.Location = New System.Drawing.Point(454, 81)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(75, 23)
        Me.cmdclear.TabIndex = 5
        Me.cmdclear.Text = "Clear"
        Me.cmdclear.UseVisualStyleBackColor = True
        '
        'cmdsave
        '
        Me.cmdsave.Location = New System.Drawing.Point(298, 81)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(108, 23)
        Me.cmdsave.TabIndex = 4
        Me.cmdsave.Text = "Import F&&O"
        Me.cmdsave.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.CausesValidation = False
        Me.cmdexit.Location = New System.Drawing.Point(456, 353)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(75, 23)
        Me.cmdexit.TabIndex = 13
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.txtFoDealer)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.cmbFO)
        Me.Panel2.Controls.Add(Me.cmdclear)
        Me.Panel2.Controls.Add(Me.cmdfobr)
        Me.Panel2.Controls.Add(Me.cmdsave)
        Me.Panel2.Controls.Add(Me.txtpath)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Location = New System.Drawing.Point(1, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(536, 109)
        Me.Panel2.TabIndex = 7
        '
        'txtFoDealer
        '
        Me.txtFoDealer.BackColor = System.Drawing.SystemColors.Window
        Me.txtFoDealer.Location = New System.Drawing.Point(405, 16)
        Me.txtFoDealer.Name = "txtFoDealer"
        Me.txtFoDealer.Size = New System.Drawing.Size(124, 20)
        Me.txtFoDealer.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(306, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 14)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Dealer Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(141, 14)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "FO Trade File Type :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbFO
        '
        Me.cmbFO.FormattingEnabled = True
        Me.cmbFO.Items.AddRange(New Object() {"FIST", "FIST ADMIN", "GETS", "NEAT", "NOTIS", "NOW", "ODIN", "NSE"})
        Me.cmbFO.Location = New System.Drawing.Point(150, 16)
        Me.cmbFO.Name = "cmbFO"
        Me.cmbFO.Size = New System.Drawing.Size(147, 21)
        Me.cmbFO.TabIndex = 0
        '
        'cmdfobr
        '
        Me.cmdfobr.Location = New System.Drawing.Point(412, 48)
        Me.cmdfobr.Name = "cmdfobr"
        Me.cmdfobr.Size = New System.Drawing.Size(75, 23)
        Me.cmdfobr.TabIndex = 3
        Me.cmdfobr.Text = "Browse"
        Me.cmdfobr.UseVisualStyleBackColor = True
        '
        'txtpath
        '
        Me.txtpath.BackColor = System.Drawing.SystemColors.Window
        Me.txtpath.Enabled = False
        Me.txtpath.Location = New System.Drawing.Point(56, 50)
        Me.txtpath.Name = "txtpath"
        Me.txtpath.ReadOnly = True
        Me.txtpath.Size = New System.Drawing.Size(350, 20)
        Me.txtpath.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 14)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "File :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdeqclear
        '
        Me.cmdeqclear.CausesValidation = False
        Me.cmdeqclear.Location = New System.Drawing.Point(454, 80)
        Me.cmdeqclear.Name = "cmdeqclear"
        Me.cmdeqclear.Size = New System.Drawing.Size(75, 23)
        Me.cmdeqclear.TabIndex = 11
        Me.cmdeqclear.Text = "Clear"
        Me.cmdeqclear.UseVisualStyleBackColor = True
        '
        'cmdeqsave
        '
        Me.cmdeqsave.Location = New System.Drawing.Point(298, 80)
        Me.cmdeqsave.Name = "cmdeqsave"
        Me.cmdeqsave.Size = New System.Drawing.Size(108, 23)
        Me.cmdeqsave.TabIndex = 10
        Me.cmdeqsave.Text = "Import Equity"
        Me.cmdeqsave.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtEqDealer)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.cmbEquity)
        Me.Panel3.Controls.Add(Me.cmdeqclear)
        Me.Panel3.Controls.Add(Me.cmdeqbr)
        Me.Panel3.Controls.Add(Me.cmdeqsave)
        Me.Panel3.Controls.Add(Me.txteqpath)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Location = New System.Drawing.Point(1, 119)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(536, 109)
        Me.Panel3.TabIndex = 9
        '
        'txtEqDealer
        '
        Me.txtEqDealer.BackColor = System.Drawing.SystemColors.Window
        Me.txtEqDealer.Location = New System.Drawing.Point(405, 16)
        Me.txtEqDealer.Name = "txtEqDealer"
        Me.txtEqDealer.Size = New System.Drawing.Size(124, 20)
        Me.txtEqDealer.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(306, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 14)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Dealer Code:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(163, 14)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Equity Trade File Type :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbEquity
        '
        Me.cmbEquity.FormattingEnabled = True
        Me.cmbEquity.Items.AddRange(New Object() {"GETS", "NOTIS", "NOW", "ODIN", "NSE"})
        Me.cmbEquity.Location = New System.Drawing.Point(172, 16)
        Me.cmbEquity.Name = "cmbEquity"
        Me.cmbEquity.Size = New System.Drawing.Size(125, 21)
        Me.cmbEquity.TabIndex = 6
        '
        'cmdeqbr
        '
        Me.cmdeqbr.Location = New System.Drawing.Point(412, 47)
        Me.cmdeqbr.Name = "cmdeqbr"
        Me.cmdeqbr.Size = New System.Drawing.Size(75, 23)
        Me.cmdeqbr.TabIndex = 9
        Me.cmdeqbr.Text = "Browse"
        Me.cmdeqbr.UseVisualStyleBackColor = True
        '
        'txteqpath
        '
        Me.txteqpath.BackColor = System.Drawing.SystemColors.Window
        Me.txteqpath.Enabled = False
        Me.txteqpath.Location = New System.Drawing.Point(56, 49)
        Me.txteqpath.Name = "txteqpath"
        Me.txteqpath.ReadOnly = True
        Me.txteqpath.Size = New System.Drawing.Size(350, 20)
        Me.txteqpath.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 14)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "File :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdproc
        '
        Me.cmdproc.Location = New System.Drawing.Point(239, 353)
        Me.cmdproc.Name = "cmdproc"
        Me.cmdproc.Size = New System.Drawing.Size(169, 23)
        Me.cmdproc.TabIndex = 12
        Me.cmdproc.Text = "Process Previous Balance"
        Me.cmdproc.UseVisualStyleBackColor = True
        Me.cmdproc.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtCurrDealer)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.cmbCurr)
        Me.Panel1.Controls.Add(Me.cmdCurrclear)
        Me.Panel1.Controls.Add(Me.cmdCurrbr)
        Me.Panel1.Controls.Add(Me.cmdCurrsave)
        Me.Panel1.Controls.Add(Me.txtCurrpath)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Location = New System.Drawing.Point(1, 234)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(536, 109)
        Me.Panel1.TabIndex = 12
        '
        'txtCurrDealer
        '
        Me.txtCurrDealer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrDealer.Location = New System.Drawing.Point(405, 16)
        Me.txtCurrDealer.Name = "txtCurrDealer"
        Me.txtCurrDealer.Size = New System.Drawing.Size(124, 20)
        Me.txtCurrDealer.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(306, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 14)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Dealer Code:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(182, 14)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Currency Trade File Type :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbCurr
        '
        Me.cmbCurr.FormattingEnabled = True
        Me.cmbCurr.Items.AddRange(New Object() {"GETS", "NEAT", "NOTIS", "NOW", "ODIN", "NSE", "MCX-ODIN"})
        Me.cmbCurr.Location = New System.Drawing.Point(191, 16)
        Me.cmbCurr.Name = "cmbCurr"
        Me.cmbCurr.Size = New System.Drawing.Size(106, 21)
        Me.cmbCurr.TabIndex = 6
        '
        'cmdCurrclear
        '
        Me.cmdCurrclear.CausesValidation = False
        Me.cmdCurrclear.Location = New System.Drawing.Point(454, 80)
        Me.cmdCurrclear.Name = "cmdCurrclear"
        Me.cmdCurrclear.Size = New System.Drawing.Size(75, 23)
        Me.cmdCurrclear.TabIndex = 11
        Me.cmdCurrclear.Text = "Clear"
        Me.cmdCurrclear.UseVisualStyleBackColor = True
        '
        'cmdCurrbr
        '
        Me.cmdCurrbr.Location = New System.Drawing.Point(412, 47)
        Me.cmdCurrbr.Name = "cmdCurrbr"
        Me.cmdCurrbr.Size = New System.Drawing.Size(75, 23)
        Me.cmdCurrbr.TabIndex = 9
        Me.cmdCurrbr.Text = "Browse"
        Me.cmdCurrbr.UseVisualStyleBackColor = True
        '
        'cmdCurrsave
        '
        Me.cmdCurrsave.Location = New System.Drawing.Point(298, 80)
        Me.cmdCurrsave.Name = "cmdCurrsave"
        Me.cmdCurrsave.Size = New System.Drawing.Size(108, 23)
        Me.cmdCurrsave.TabIndex = 10
        Me.cmdCurrsave.Text = "Import Currency"
        Me.cmdCurrsave.UseVisualStyleBackColor = True
        '
        'txtCurrpath
        '
        Me.txtCurrpath.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrpath.Enabled = False
        Me.txtCurrpath.Location = New System.Drawing.Point(56, 49)
        Me.txtCurrpath.Name = "txtCurrpath"
        Me.txtCurrpath.ReadOnly = True
        Me.txtCurrpath.Size = New System.Drawing.Size(350, 20)
        Me.txtCurrpath.TabIndex = 8
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 51)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 14)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "File :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'addprevious_datetrading
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(538, 388)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cmdproc)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "addprevious_datetrading"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Previous trade file"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents cmdsave As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmdfobr As System.Windows.Forms.Button
    Friend WithEvents txtpath As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdeqclear As System.Windows.Forms.Button
    Friend WithEvents cmdeqsave As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents cmdeqbr As System.Windows.Forms.Button
    Friend WithEvents txteqpath As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdproc As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbFO As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbEquity As System.Windows.Forms.ComboBox
    Friend WithEvents txtFoDealer As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEqDealer As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtCurrDealer As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbCurr As System.Windows.Forms.ComboBox
    Friend WithEvents cmdCurrclear As System.Windows.Forms.Button
    Friend WithEvents cmdCurrbr As System.Windows.Forms.Button
    Friend WithEvents cmdCurrsave As System.Windows.Forms.Button
    Friend WithEvents txtCurrpath As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
