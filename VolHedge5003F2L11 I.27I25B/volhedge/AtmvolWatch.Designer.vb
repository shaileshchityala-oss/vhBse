<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AtmvolWatch
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grvvolwatch = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbexpiry = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.RBsynfut = New System.Windows.Forms.CheckBox()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SAVE = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.btnrefresh = New System.Windows.Forms.Button()
        Me.cmbcondition = New System.Windows.Forms.ComboBox()
        Me.txtalert = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        CType(Me.grvvolwatch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grvvolwatch
        '
        Me.grvvolwatch.BackgroundColor = System.Drawing.Color.Gray
        Me.grvvolwatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grvvolwatch.GridColor = System.Drawing.Color.Gainsboro
        Me.grvvolwatch.Location = New System.Drawing.Point(12, 103)
        Me.grvvolwatch.Name = "grvvolwatch"
        Me.grvvolwatch.RowHeadersWidth = 62
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.grvvolwatch.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grvvolwatch.Size = New System.Drawing.Size(802, 559)
        Me.grvvolwatch.TabIndex = 183
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 15)
        Me.Label1.TabIndex = 182
        Me.Label1.Text = "Select Symbol:"
        '
        'cmbexpiry
        '
        Me.cmbexpiry.FormattingEnabled = True
        Me.cmbexpiry.Items.AddRange(New Object() {"Current Month", "Next Month", "Far Month"})
        Me.cmbexpiry.Location = New System.Drawing.Point(160, 37)
        Me.cmbexpiry.Name = "cmbexpiry"
        Me.cmbexpiry.Size = New System.Drawing.Size(121, 21)
        Me.cmbexpiry.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button1.Location = New System.Drawing.Point(486, 34)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(49, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "ADD"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'RBsynfut
        '
        Me.RBsynfut.AutoSize = True
        Me.RBsynfut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBsynfut.ForeColor = System.Drawing.Color.Black
        Me.RBsynfut.Location = New System.Drawing.Point(680, 72)
        Me.RBsynfut.Name = "RBsynfut"
        Me.RBsynfut.Size = New System.Drawing.Size(119, 17)
        Me.RBsynfut.TabIndex = 186
        Me.RBsynfut.Text = "Synthatic Future"
        Me.RBsynfut.UseVisualStyleBackColor = True
        '
        'cmbCompany
        '
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Location = New System.Drawing.Point(11, 37)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(121, 21)
        Me.cmbCompany.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(159, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 15)
        Me.Label2.TabIndex = 184
        Me.Label2.Text = "Expiry :"
        '
        'SAVE
        '
        Me.SAVE.BackColor = System.Drawing.SystemColors.Control
        Me.SAVE.Location = New System.Drawing.Point(11, 70)
        Me.SAVE.Name = "SAVE"
        Me.SAVE.Size = New System.Drawing.Size(59, 23)
        Me.SAVE.TabIndex = 187
        Me.SAVE.Text = "SAVE"
        Me.SAVE.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.Control
        Me.Button2.Location = New System.Drawing.Point(541, 35)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(70, 23)
        Me.Button2.TabIndex = 188
        Me.Button2.Text = "REMOVE"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.Control
        Me.Button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button3.Location = New System.Drawing.Point(536, 68)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(49, 23)
        Me.Button3.TabIndex = 189
        Me.Button3.Text = "STOP"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.SystemColors.Control
        Me.Button4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button4.Location = New System.Drawing.Point(466, 68)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(64, 23)
        Me.Button4.TabIndex = 190
        Me.Button4.Text = "START"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'btnrefresh
        '
        Me.btnrefresh.BackColor = System.Drawing.SystemColors.Control
        Me.btnrefresh.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnrefresh.Location = New System.Drawing.Point(591, 68)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(74, 23)
        Me.btnrefresh.TabIndex = 191
        Me.btnrefresh.Text = "Refresh"
        Me.btnrefresh.UseVisualStyleBackColor = False
        '
        'cmbcondition
        '
        Me.cmbcondition.FormattingEnabled = True
        Me.cmbcondition.Items.AddRange(New Object() {"<", ">", "<=", ">=", "="})
        Me.cmbcondition.Location = New System.Drawing.Point(300, 37)
        Me.cmbcondition.Name = "cmbcondition"
        Me.cmbcondition.Size = New System.Drawing.Size(51, 21)
        Me.cmbcondition.TabIndex = 2
        '
        'txtalert
        '
        Me.txtalert.Location = New System.Drawing.Point(370, 37)
        Me.txtalert.Name = "txtalert"
        Me.txtalert.Size = New System.Drawing.Size(98, 20)
        Me.txtalert.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(367, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 15)
        Me.Label3.TabIndex = 194
        Me.Label3.Text = "Vol Alert :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(297, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 15)
        Me.Label4.TabIndex = 195
        Me.Label4.Text = "Opertor:"
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.SystemColors.Control
        Me.Button5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button5.Location = New System.Drawing.Point(76, 70)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(99, 23)
        Me.Button5.TabIndex = 196
        Me.Button5.Text = "DELETE ALL"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'AtmvolWatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.ClientSize = New System.Drawing.Size(826, 675)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtalert)
        Me.Controls.Add(Me.cmbcondition)
        Me.Controls.Add(Me.btnrefresh)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.SAVE)
        Me.Controls.Add(Me.grvvolwatch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbexpiry)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.RBsynfut)
        Me.Controls.Add(Me.cmbCompany)
        Me.Controls.Add(Me.Label2)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.KeyPreview = True
        Me.Name = "AtmvolWatch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "AtmvolWatch"
        CType(Me.grvvolwatch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents grvvolwatch As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbexpiry As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents RBsynfut As CheckBox
    Friend WithEvents cmbCompany As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents SAVE As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents btnrefresh As Button
    Friend WithEvents cmbcondition As ComboBox
    Friend WithEvents txtalert As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Button5 As Button
End Class
