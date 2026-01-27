<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class import_master
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(import_master))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblcount = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtpath = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdsave = New System.Windows.Forms.Button()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.cmdsave2 = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.cmdeqclear = New System.Windows.Forms.Button()
        Me.cmdeqimport2 = New System.Windows.Forms.Button()
        Me.cmdeqimport = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblcount1 = New System.Windows.Forms.Label()
        Me.cmdeqbr = New System.Windows.Forms.Button()
        Me.txteqpath = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.btnClearCurrency = New System.Windows.Forms.Button()
        Me.btnImportCurrency2 = New System.Windows.Forms.Button()
        Me.btnImportCurrency = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lblCurrencyCounter = New System.Windows.Forms.Label()
        Me.btnBrowseCurrency = New System.Windows.Forms.Button()
        Me.txtCurrencyPath = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(506, 36)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Import Contract Master "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(508, 38)
        Me.Panel1.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.lblcount)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.txtpath)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Location = New System.Drawing.Point(0, 38)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(508, 61)
        Me.Panel2.TabIndex = 4
        '
        'lblcount
        '
        Me.lblcount.AutoSize = True
        Me.lblcount.Location = New System.Drawing.Point(48, 44)
        Me.lblcount.Name = "lblcount"
        Me.lblcount.Size = New System.Drawing.Size(13, 13)
        Me.lblcount.TabIndex = 7
        Me.lblcount.Text = "0"
        Me.lblcount.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(425, 14)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Browse"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtpath
        '
        Me.txtpath.BackColor = System.Drawing.SystemColors.Window
        Me.txtpath.Enabled = False
        Me.txtpath.Location = New System.Drawing.Point(63, 16)
        Me.txtpath.Name = "txtpath"
        Me.txtpath.ReadOnly = True
        Me.txtpath.Size = New System.Drawing.Size(350, 20)
        Me.txtpath.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(48, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(12, 14)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = ":"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 14)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "File"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdsave
        '
        Me.cmdsave.Location = New System.Drawing.Point(338, 13)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(75, 23)
        Me.cmdsave.TabIndex = 0
        Me.cmdsave.Text = "Import"
        Me.cmdsave.UseVisualStyleBackColor = True
        '
        'cmdclear
        '
        Me.cmdclear.CausesValidation = False
        Me.cmdclear.Location = New System.Drawing.Point(419, 13)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(75, 23)
        Me.cmdclear.TabIndex = 1
        Me.cmdclear.Text = "Clear"
        Me.cmdclear.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.Control
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.cmdclear)
        Me.Panel4.Controls.Add(Me.cmdsave2)
        Me.Panel4.Controls.Add(Me.cmdsave)
        Me.Panel4.Location = New System.Drawing.Point(0, 99)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(508, 48)
        Me.Panel4.TabIndex = 6
        '
        'cmdsave2
        '
        Me.cmdsave2.BackgroundImage = CType(resources.GetObject("cmdsave2.BackgroundImage"), System.Drawing.Image)
        Me.cmdsave2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdsave2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdsave2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsave2.Location = New System.Drawing.Point(11, 5)
        Me.cmdsave2.Name = "cmdsave2"
        Me.cmdsave2.Size = New System.Drawing.Size(152, 36)
        Me.cmdsave2.TabIndex = 0
        Me.cmdsave2.Text = "Download  Import"
        Me.cmdsave2.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.cmdeqclear)
        Me.Panel3.Controls.Add(Me.cmdeqimport2)
        Me.Panel3.Controls.Add(Me.cmdeqimport)
        Me.Panel3.Location = New System.Drawing.Point(-1, 247)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(508, 48)
        Me.Panel3.TabIndex = 9
        '
        'cmdeqclear
        '
        Me.cmdeqclear.CausesValidation = False
        Me.cmdeqclear.Location = New System.Drawing.Point(418, 13)
        Me.cmdeqclear.Name = "cmdeqclear"
        Me.cmdeqclear.Size = New System.Drawing.Size(75, 23)
        Me.cmdeqclear.TabIndex = 1
        Me.cmdeqclear.Text = "Clear"
        Me.cmdeqclear.UseVisualStyleBackColor = True
        '
        'cmdeqimport2
        '
        Me.cmdeqimport2.BackgroundImage = CType(resources.GetObject("cmdeqimport2.BackgroundImage"), System.Drawing.Image)
        Me.cmdeqimport2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdeqimport2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdeqimport2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdeqimport2.Location = New System.Drawing.Point(12, 5)
        Me.cmdeqimport2.Name = "cmdeqimport2"
        Me.cmdeqimport2.Size = New System.Drawing.Size(152, 36)
        Me.cmdeqimport2.TabIndex = 0
        Me.cmdeqimport2.Text = "Download  Import"
        Me.cmdeqimport2.UseVisualStyleBackColor = True
        '
        'cmdeqimport
        '
        Me.cmdeqimport.Location = New System.Drawing.Point(337, 13)
        Me.cmdeqimport.Name = "cmdeqimport"
        Me.cmdeqimport.Size = New System.Drawing.Size(75, 23)
        Me.cmdeqimport.TabIndex = 0
        Me.cmdeqimport.Text = "Import"
        Me.cmdeqimport.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.Control
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.lblcount1)
        Me.Panel5.Controls.Add(Me.cmdeqbr)
        Me.Panel5.Controls.Add(Me.txteqpath)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Location = New System.Drawing.Point(-1, 186)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(508, 61)
        Me.Panel5.TabIndex = 8
        '
        'lblcount1
        '
        Me.lblcount1.AutoSize = True
        Me.lblcount1.Location = New System.Drawing.Point(48, 44)
        Me.lblcount1.Name = "lblcount1"
        Me.lblcount1.Size = New System.Drawing.Size(13, 13)
        Me.lblcount1.TabIndex = 7
        Me.lblcount1.Text = "0"
        Me.lblcount1.Visible = False
        '
        'cmdeqbr
        '
        Me.cmdeqbr.Location = New System.Drawing.Point(425, 14)
        Me.cmdeqbr.Name = "cmdeqbr"
        Me.cmdeqbr.Size = New System.Drawing.Size(75, 23)
        Me.cmdeqbr.TabIndex = 6
        Me.cmdeqbr.Text = "Browse"
        Me.cmdeqbr.UseVisualStyleBackColor = True
        '
        'txteqpath
        '
        Me.txteqpath.BackColor = System.Drawing.SystemColors.Window
        Me.txteqpath.Enabled = False
        Me.txteqpath.Location = New System.Drawing.Point(63, 16)
        Me.txteqpath.Name = "txteqpath"
        Me.txteqpath.ReadOnly = True
        Me.txteqpath.Size = New System.Drawing.Size(350, 20)
        Me.txteqpath.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(48, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(12, 14)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = ":"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(11, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 14)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "File"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(-1, 148)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(506, 36)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Import Security Master "
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.SystemColors.Control
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.btnClearCurrency)
        Me.Panel6.Controls.Add(Me.btnImportCurrency2)
        Me.Panel6.Controls.Add(Me.btnImportCurrency)
        Me.Panel6.Location = New System.Drawing.Point(-1, 394)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(508, 48)
        Me.Panel6.TabIndex = 12
        '
        'btnClearCurrency
        '
        Me.btnClearCurrency.CausesValidation = False
        Me.btnClearCurrency.Location = New System.Drawing.Point(418, 13)
        Me.btnClearCurrency.Name = "btnClearCurrency"
        Me.btnClearCurrency.Size = New System.Drawing.Size(75, 23)
        Me.btnClearCurrency.TabIndex = 1
        Me.btnClearCurrency.Text = "Clear"
        Me.btnClearCurrency.UseVisualStyleBackColor = True
        '
        'btnImportCurrency2
        '
        Me.btnImportCurrency2.BackgroundImage = CType(resources.GetObject("btnImportCurrency2.BackgroundImage"), System.Drawing.Image)
        Me.btnImportCurrency2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnImportCurrency2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImportCurrency2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImportCurrency2.Location = New System.Drawing.Point(12, 7)
        Me.btnImportCurrency2.Name = "btnImportCurrency2"
        Me.btnImportCurrency2.Size = New System.Drawing.Size(152, 36)
        Me.btnImportCurrency2.TabIndex = 0
        Me.btnImportCurrency2.Text = "Download  Import"
        Me.btnImportCurrency2.UseVisualStyleBackColor = True
        '
        'btnImportCurrency
        '
        Me.btnImportCurrency.Location = New System.Drawing.Point(337, 13)
        Me.btnImportCurrency.Name = "btnImportCurrency"
        Me.btnImportCurrency.Size = New System.Drawing.Size(75, 23)
        Me.btnImportCurrency.TabIndex = 0
        Me.btnImportCurrency.Text = "Import"
        Me.btnImportCurrency.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.SystemColors.Control
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.lblCurrencyCounter)
        Me.Panel7.Controls.Add(Me.btnBrowseCurrency)
        Me.Panel7.Controls.Add(Me.txtCurrencyPath)
        Me.Panel7.Controls.Add(Me.Label8)
        Me.Panel7.Controls.Add(Me.Label9)
        Me.Panel7.Location = New System.Drawing.Point(-1, 333)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(508, 61)
        Me.Panel7.TabIndex = 11
        '
        'lblCurrencyCounter
        '
        Me.lblCurrencyCounter.AutoSize = True
        Me.lblCurrencyCounter.Location = New System.Drawing.Point(48, 44)
        Me.lblCurrencyCounter.Name = "lblCurrencyCounter"
        Me.lblCurrencyCounter.Size = New System.Drawing.Size(13, 13)
        Me.lblCurrencyCounter.TabIndex = 7
        Me.lblCurrencyCounter.Text = "0"
        Me.lblCurrencyCounter.Visible = False
        '
        'btnBrowseCurrency
        '
        Me.btnBrowseCurrency.Location = New System.Drawing.Point(425, 14)
        Me.btnBrowseCurrency.Name = "btnBrowseCurrency"
        Me.btnBrowseCurrency.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseCurrency.TabIndex = 6
        Me.btnBrowseCurrency.Text = "Browse"
        Me.btnBrowseCurrency.UseVisualStyleBackColor = True
        '
        'txtCurrencyPath
        '
        Me.txtCurrencyPath.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrencyPath.Enabled = False
        Me.txtCurrencyPath.Location = New System.Drawing.Point(63, 16)
        Me.txtCurrencyPath.Name = "txtCurrencyPath"
        Me.txtCurrencyPath.ReadOnly = True
        Me.txtCurrencyPath.Size = New System.Drawing.Size(350, 20)
        Me.txtCurrencyPath.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(48, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(12, 14)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = ":"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(11, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 14)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "File"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(-1, 295)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(506, 36)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Import Currency Master "
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'import_master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.ClientSize = New System.Drawing.Size(509, 443)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "import_master"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtpath As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdsave As System.Windows.Forms.Button
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblcount As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents cmdeqclear As System.Windows.Forms.Button
    Friend WithEvents cmdeqimport As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblcount1 As System.Windows.Forms.Label
    Friend WithEvents cmdeqbr As System.Windows.Forms.Button
    Friend WithEvents txteqpath As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnClearCurrency As System.Windows.Forms.Button
    Friend WithEvents btnImportCurrency As System.Windows.Forms.Button
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents lblCurrencyCounter As System.Windows.Forms.Label
    Friend WithEvents btnBrowseCurrency As System.Windows.Forms.Button
    Friend WithEvents txtCurrencyPath As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmdsave2 As System.Windows.Forms.Button
    Friend WithEvents cmdeqimport2 As System.Windows.Forms.Button
    Friend WithEvents btnImportCurrency2 As System.Windows.Forms.Button
End Class
