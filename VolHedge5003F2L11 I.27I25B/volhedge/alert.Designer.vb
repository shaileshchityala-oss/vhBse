<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class alert
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(alert))
        Me.cmdyes = New System.Windows.Forms.Button()
        Me.cmdno = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblcompscript = New System.Windows.Forms.Label()
        Me.lblopt = New System.Windows.Forms.Label()
        Me.lblfield = New System.Windows.Forms.Label()
        Me.lblv1 = New System.Windows.Forms.Label()
        Me.lblv2 = New System.Windows.Forms.Label()
        Me.lblcurr = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdyes
        '
        Me.cmdyes.Location = New System.Drawing.Point(191, 125)
        Me.cmdyes.Name = "cmdyes"
        Me.cmdyes.Size = New System.Drawing.Size(51, 23)
        Me.cmdyes.TabIndex = 10
        Me.cmdyes.Text = "Snooze"
        Me.cmdyes.UseVisualStyleBackColor = True
        '
        'cmdno
        '
        Me.cmdno.Location = New System.Drawing.Point(245, 125)
        Me.cmdno.Name = "cmdno"
        Me.cmdno.Size = New System.Drawing.Size(51, 23)
        Me.cmdno.TabIndex = 11
        Me.cmdno.Text = "Stop"
        Me.cmdno.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 14)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Security"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 14)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Operator"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 14)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Field"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 14)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Value-1"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(169, 75)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 14)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Value-2"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 101)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 14)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Current"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(83, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(12, 14)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = ":"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(83, 30)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(12, 14)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = ":"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(83, 52)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(12, 14)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = ":"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(83, 74)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(12, 14)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = ":"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(237, 75)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(12, 14)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = ":"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(83, 101)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(12, 14)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = ":"
        '
        'lblcompscript
        '
        Me.lblcompscript.AutoSize = True
        Me.lblcompscript.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcompscript.Location = New System.Drawing.Point(95, 9)
        Me.lblcompscript.Name = "lblcompscript"
        Me.lblcompscript.Size = New System.Drawing.Size(79, 13)
        Me.lblcompscript.TabIndex = 21
        Me.lblcompscript.Text = "Comp/Script"
        '
        'lblopt
        '
        Me.lblopt.AutoSize = True
        Me.lblopt.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblopt.Location = New System.Drawing.Point(95, 31)
        Me.lblopt.Name = "lblopt"
        Me.lblopt.Size = New System.Drawing.Size(58, 13)
        Me.lblopt.TabIndex = 22
        Me.lblopt.Text = "Operator"
        '
        'lblfield
        '
        Me.lblfield.AutoSize = True
        Me.lblfield.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfield.Location = New System.Drawing.Point(95, 53)
        Me.lblfield.Name = "lblfield"
        Me.lblfield.Size = New System.Drawing.Size(33, 13)
        Me.lblfield.TabIndex = 23
        Me.lblfield.Text = "Field"
        '
        'lblv1
        '
        Me.lblv1.AutoSize = True
        Me.lblv1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblv1.Location = New System.Drawing.Point(95, 75)
        Me.lblv1.Name = "lblv1"
        Me.lblv1.Size = New System.Drawing.Size(51, 13)
        Me.lblv1.TabIndex = 24
        Me.lblv1.Text = "Value-1"
        '
        'lblv2
        '
        Me.lblv2.AutoSize = True
        Me.lblv2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblv2.Location = New System.Drawing.Point(249, 76)
        Me.lblv2.Name = "lblv2"
        Me.lblv2.Size = New System.Drawing.Size(51, 13)
        Me.lblv2.TabIndex = 25
        Me.lblv2.Text = "Value-2"
        '
        'lblcurr
        '
        Me.lblcurr.AutoSize = True
        Me.lblcurr.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcurr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblcurr.Location = New System.Drawing.Point(95, 100)
        Me.lblcurr.Name = "lblcurr"
        Me.lblcurr.Size = New System.Drawing.Size(63, 16)
        Me.lblcurr.TabIndex = 26
        Me.lblcurr.Text = "Current"
        '
        'alert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(327, 150)
        Me.Controls.Add(Me.cmdno)
        Me.Controls.Add(Me.cmdyes)
        Me.Controls.Add(Me.lblcurr)
        Me.Controls.Add(Me.lblv2)
        Me.Controls.Add(Me.lblv1)
        Me.Controls.Add(Me.lblfield)
        Me.Controls.Add(Me.lblopt)
        Me.Controls.Add(Me.lblcompscript)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "alert"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Alert"
        Me.TopMost = True 'comment by payal patel
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdyes As System.Windows.Forms.Button
    Friend WithEvents cmdno As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblcompscript As System.Windows.Forms.Label
    Friend WithEvents lblopt As System.Windows.Forms.Label
    Friend WithEvents lblfield As System.Windows.Forms.Label
    Friend WithEvents lblv1 As System.Windows.Forms.Label
    Friend WithEvents lblv2 As System.Windows.Forms.Label
    Friend WithEvents lblcurr As System.Windows.Forms.Label
End Class
