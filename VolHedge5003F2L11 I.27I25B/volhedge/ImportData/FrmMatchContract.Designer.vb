<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMatchContract
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
        Me.DGV_MatchCon = New System.Windows.Forms.DataGridView()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.mdate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nmdate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DGV_MatchCon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_MatchCon
        '
        Me.DGV_MatchCon.AllowUserToAddRows = False
        Me.DGV_MatchCon.AllowUserToDeleteRows = False
        Me.DGV_MatchCon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_MatchCon.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.mdate, Me.nmdate})
        Me.DGV_MatchCon.Location = New System.Drawing.Point(12, 32)
        Me.DGV_MatchCon.Name = "DGV_MatchCon"
        Me.DGV_MatchCon.RowHeadersVisible = False
        Me.DGV_MatchCon.Size = New System.Drawing.Size(457, 165)
        Me.DGV_MatchCon.TabIndex = 0
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.White
        Me.btnUpdate.Location = New System.Drawing.Point(154, 216)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 1
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(267, 216)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'mdate
        '
        Me.mdate.DataPropertyName = "mdate"
        Me.mdate.HeaderText = "Expiry Date"
        Me.mdate.Name = "mdate"
        Me.mdate.ReadOnly = True
        Me.mdate.Width = 200
        '
        'nmdate
        '
        Me.nmdate.DataPropertyName = "nmdate"
        Me.nmdate.HeaderText = "New ExpiryDate [MM/dd/yyyy]"
        Me.nmdate.Name = "nmdate"
        Me.nmdate.Width = 200
        '
        'FrmMatchContract
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(481, 266)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.DGV_MatchCon)
        Me.Name = "FrmMatchContract"
        Me.Text = "Match Contract"
        CType(Me.DGV_MatchCon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGV_MatchCon As System.Windows.Forms.DataGridView
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents mdate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents nmdate As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
