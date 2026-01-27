<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCFBalance
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGCFBalance = New System.Windows.Forms.DataGridView()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Symbol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Balance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DGCFBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGCFBalance
        '
        Me.DGCFBalance.AllowUserToAddRows = False
        Me.DGCFBalance.AllowUserToDeleteRows = False
        Me.DGCFBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGCFBalance.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Symbol, Me.Balance})
        Me.DGCFBalance.Location = New System.Drawing.Point(0, 12)
        Me.DGCFBalance.Name = "DGCFBalance"
        Me.DGCFBalance.RowHeadersVisible = False
        Me.DGCFBalance.Size = New System.Drawing.Size(315, 445)
        Me.DGCFBalance.TabIndex = 0
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(240, 463)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 1
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Symbol"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Symbol"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Balance"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Balance"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'Symbol
        '
        Me.Symbol.DataPropertyName = "Symbol"
        Me.Symbol.HeaderText = "Symbol"
        Me.Symbol.Name = "Symbol"
        Me.Symbol.ReadOnly = True
        '
        'Balance
        '
        Me.Balance.DataPropertyName = "Balance"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Balance.DefaultCellStyle = DataGridViewCellStyle1
        Me.Balance.HeaderText = "Balance"
        Me.Balance.Name = "Balance"
        '
        'FrmCFBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(322, 489)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.DGCFBalance)
        Me.Name = "FrmCFBalance"
        Me.Text = "CFBalance"
        CType(Me.DGCFBalance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGCFBalance As System.Windows.Forms.DataGridView
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Symbol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Balance As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
