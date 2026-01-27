<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_UDPSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_UDPSetting))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtUDPPort = New System.Windows.Forms.TextBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtUDPIPAddress = New VolHedge.ipTextBox
        Me.btnTest = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(18, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "UDP IP Address  :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Navy
        Me.Label2.Location = New System.Drawing.Point(39, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "UDP Port No. :"
        '
        'txtUDPPort
        '
        Me.txtUDPPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUDPPort.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.txtUDPPort.Location = New System.Drawing.Point(142, 50)
        Me.txtUDPPort.Name = "txtUDPPort"
        Me.txtUDPPort.Size = New System.Drawing.Size(132, 23)
        Me.txtUDPPort.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnExit.Location = New System.Drawing.Point(226, 122)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(81, 27)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "E&xit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnOK.Location = New System.Drawing.Point(139, 122)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(81, 27)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&Apply"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtUDPIPAddress)
        Me.GroupBox1.Controls.Add(Me.btnTest)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtUDPPort)
        Me.GroupBox1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.GroupBox1.Location = New System.Drawing.Point(12, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(295, 110)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "UDP Setting"
        '
        'txtUDPIPAddress
        '
        Me.txtUDPIPAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtUDPIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUDPIPAddress.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUDPIPAddress.IPaddress = "0.0.0.0"
        Me.txtUDPIPAddress.Location = New System.Drawing.Point(142, 21)
        Me.txtUDPIPAddress.Name = "txtUDPIPAddress"
        Me.txtUDPIPAddress.rangeAEnabled = True
        Me.txtUDPIPAddress.rangeAMaximun = 255
        Me.txtUDPIPAddress.rangeAMininum = 0
        Me.txtUDPIPAddress.RangeAValue = 0
        Me.txtUDPIPAddress.rangeBEnabled = True
        Me.txtUDPIPAddress.rangeBMaximun = 255
        Me.txtUDPIPAddress.rangeBMininum = 0
        Me.txtUDPIPAddress.RangeBValue = 0
        Me.txtUDPIPAddress.rangeCEnabled = True
        Me.txtUDPIPAddress.rangeCMaximun = 255
        Me.txtUDPIPAddress.rangeCMininum = 0
        Me.txtUDPIPAddress.RangeCValue = 0
        Me.txtUDPIPAddress.rangeDEnabled = True
        Me.txtUDPIPAddress.rangeDMaximun = 255
        Me.txtUDPIPAddress.rangeDMininum = 0
        Me.txtUDPIPAddress.RangeDValue = 0
        Me.txtUDPIPAddress.Size = New System.Drawing.Size(132, 23)
        Me.txtUDPIPAddress.TabIndex = 0
        '
        'btnTest
        '
        Me.btnTest.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark
        Me.btnTest.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTest.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTest.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.btnTest.Location = New System.Drawing.Point(231, 79)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(43, 23)
        Me.btnTest.TabIndex = 2
        Me.btnTest.Text = "&Test"
        Me.btnTest.UseVisualStyleBackColor = False
        '
        'Frm_UDPSetting
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(319, 156)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_UDPSetting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "UDP Setting"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUDPPort As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents txtUDPIPAddress As VolHedge.ipTextBox
End Class
