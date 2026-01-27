<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAdditionalExpo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.txtAELImported = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.grdaelcontracts = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InsType2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Symbol3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StrikePrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OptType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CALevel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ELMPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.TimerAel = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdaelcontracts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Button6)
        Me.GroupBox3.Controls.Add(Me.txtAELImported)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.TextBox3)
        Me.GroupBox3.Controls.Add(Me.TextBox4)
        Me.GroupBox3.Controls.Add(Me.grdaelcontracts)
        Me.GroupBox3.Location = New System.Drawing.Point(41, 22)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(360, 543)
        Me.GroupBox3.TabIndex = 20
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Additional Exposure"
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(215, 471)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(138, 26)
        Me.Button6.TabIndex = 16
        Me.Button6.Text = "Additional Exposure"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'txtAELImported
        '
        Me.txtAELImported.Location = New System.Drawing.Point(109, 503)
        Me.txtAELImported.Name = "txtAELImported"
        Me.txtAELImported.Size = New System.Drawing.Size(221, 20)
        Me.txtAELImported.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(25, 509)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Total Imported:"
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(201, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(152, 24)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Exposure Margin (%)"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(9, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(192, 24)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Security name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox3
        '
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Enabled = False
        Me.TextBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(201, 45)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(152, 22)
        Me.TextBox3.TabIndex = 2
        '
        'TextBox4
        '
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox4.Enabled = False
        Me.TextBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(9, 45)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(192, 22)
        Me.TextBox4.TabIndex = 1
        '
        'grdaelcontracts
        '
        Me.grdaelcontracts.AllowUserToAddRows = False
        Me.grdaelcontracts.AllowUserToDeleteRows = False
        Me.grdaelcontracts.AllowUserToResizeColumns = False
        Me.grdaelcontracts.AllowUserToResizeRows = False
        Me.grdaelcontracts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdaelcontracts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn6, Me.InsType2, Me.Symbol3, Me.ExpDate, Me.StrikePrice, Me.OptType, Me.CALevel, Me.ELMPer})
        Me.grdaelcontracts.Location = New System.Drawing.Point(12, 70)
        Me.grdaelcontracts.Name = "grdaelcontracts"
        Me.grdaelcontracts.ReadOnly = True
        Me.grdaelcontracts.RowHeadersVisible = False
        Me.grdaelcontracts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdaelcontracts.Size = New System.Drawing.Size(341, 389)
        Me.grdaelcontracts.TabIndex = 10
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "uid"
        Me.DataGridViewTextBoxColumn6.HeaderText = "uid"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        '
        'InsType2
        '
        Me.InsType2.DataPropertyName = "InsType"
        Me.InsType2.HeaderText = "InsType"
        Me.InsType2.Name = "InsType2"
        Me.InsType2.ReadOnly = True
        '
        'Symbol3
        '
        Me.Symbol3.DataPropertyName = "Symbol"
        Me.Symbol3.HeaderText = "Symbol"
        Me.Symbol3.Name = "Symbol3"
        Me.Symbol3.ReadOnly = True
        '
        'ExpDate
        '
        Me.ExpDate.DataPropertyName = "ExpDate"
        Me.ExpDate.HeaderText = "ExpDate"
        Me.ExpDate.Name = "ExpDate"
        Me.ExpDate.ReadOnly = True
        '
        'StrikePrice
        '
        Me.StrikePrice.DataPropertyName = "StrikePrice"
        Me.StrikePrice.HeaderText = "StrikePrice"
        Me.StrikePrice.Name = "StrikePrice"
        Me.StrikePrice.ReadOnly = True
        '
        'OptType
        '
        Me.OptType.DataPropertyName = "OptType"
        Me.OptType.HeaderText = "OptType"
        Me.OptType.Name = "OptType"
        Me.OptType.ReadOnly = True
        '
        'CALevel
        '
        Me.CALevel.DataPropertyName = "CALevel"
        Me.CALevel.HeaderText = "CALevel"
        Me.CALevel.Name = "CALevel"
        Me.CALevel.ReadOnly = True
        '
        'ELMPer
        '
        Me.ELMPer.DataPropertyName = "ELMPer"
        Me.ELMPer.HeaderText = "Margin Per"
        Me.ELMPer.Name = "ELMPer"
        Me.ELMPer.ReadOnly = True
        '
        'TimerAel
        '
        '
        'FormAdditionalExpo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(462, 597)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "FormAdditionalExpo"
        Me.Text = "FormAdditionalExpo"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.grdaelcontracts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Button6 As Button
    Friend WithEvents txtAELImported As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents grdaelcontracts As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents InsType2 As DataGridViewTextBoxColumn
    Friend WithEvents Symbol3 As DataGridViewTextBoxColumn
    Friend WithEvents ExpDate As DataGridViewTextBoxColumn
    Friend WithEvents StrikePrice As DataGridViewTextBoxColumn
    Friend WithEvents OptType As DataGridViewTextBoxColumn
    Friend WithEvents CALevel As DataGridViewTextBoxColumn
    Friend WithEvents ELMPer As DataGridViewTextBoxColumn
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents TimerAel As Timer
End Class
