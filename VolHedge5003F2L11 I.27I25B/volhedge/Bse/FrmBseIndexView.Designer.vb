<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBseIndexView
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBseIndexView))
        Me.dgBseIndex = New System.Windows.Forms.DataGridView()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnTest = New System.Windows.Forms.Button()
        Me.dgvNseIndex = New System.Windows.Forms.DataGridView()
        Me.chkOnTop = New System.Windows.Forms.CheckBox()
        CType(Me.dgBseIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvNseIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgBseIndex
        '
        Me.dgBseIndex.AllowUserToAddRows = False
        Me.dgBseIndex.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgBseIndex.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgBseIndex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgBseIndex.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgBseIndex.Location = New System.Drawing.Point(14, 44)
        Me.dgBseIndex.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgBseIndex.Name = "dgBseIndex"
        Me.dgBseIndex.ReadOnly = True
        Me.dgBseIndex.RowHeadersVisible = False
        Me.dgBseIndex.RowHeadersWidth = 62
        Me.dgBseIndex.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgBseIndex.Size = New System.Drawing.Size(377, 391)
        Me.dgBseIndex.TabIndex = 0
        '
        'Timer1
        '
        '
        'btnTest
        '
        Me.btnTest.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTest.Location = New System.Drawing.Point(524, 866)
        Me.btnTest.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(146, 40)
        Me.btnTest.TabIndex = 1
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'dgvNseIndex
        '
        Me.dgvNseIndex.AllowUserToAddRows = False
        Me.dgvNseIndex.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvNseIndex.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvNseIndex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvNseIndex.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvNseIndex.Location = New System.Drawing.Point(399, 44)
        Me.dgvNseIndex.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgvNseIndex.Name = "dgvNseIndex"
        Me.dgvNseIndex.ReadOnly = True
        Me.dgvNseIndex.RowHeadersVisible = False
        Me.dgvNseIndex.RowHeadersWidth = 62
        Me.dgvNseIndex.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvNseIndex.Size = New System.Drawing.Size(356, 391)
        Me.dgvNseIndex.TabIndex = 0
        '
        'chkOnTop
        '
        Me.chkOnTop.AutoSize = True
        Me.chkOnTop.Checked = True
        Me.chkOnTop.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOnTop.Location = New System.Drawing.Point(28, 13)
        Me.chkOnTop.Name = "chkOnTop"
        Me.chkOnTop.Size = New System.Drawing.Size(22, 21)
        Me.chkOnTop.TabIndex = 2
        Me.chkOnTop.UseVisualStyleBackColor = True
        '
        'FrmBseIndexView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(766, 449)
        Me.Controls.Add(Me.chkOnTop)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.dgvNseIndex)
        Me.Controls.Add(Me.dgBseIndex)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "FrmBseIndexView"
        Me.Text = "Index Watch6"
        Me.TopMost = True
        CType(Me.dgBseIndex, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvNseIndex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgBseIndex As DataGridView
    Friend WithEvents Timer1 As Timer
    Friend WithEvents btnTest As Button
    Friend WithEvents dgvNseIndex As DataGridView
    Friend WithEvents chkOnTop As CheckBox
End Class
