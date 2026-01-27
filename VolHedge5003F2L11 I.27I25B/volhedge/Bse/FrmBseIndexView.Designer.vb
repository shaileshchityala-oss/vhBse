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
        Me.dgBseIndex = New System.Windows.Forms.DataGridView()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnTest = New System.Windows.Forms.Button()
        CType(Me.dgBseIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgBseIndex
        '
        Me.dgBseIndex.AllowUserToAddRows = False
        Me.dgBseIndex.AllowUserToDeleteRows = False
        Me.dgBseIndex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgBseIndex.Location = New System.Drawing.Point(5, 5)
        Me.dgBseIndex.Name = "dgBseIndex"
        Me.dgBseIndex.ReadOnly = True
        Me.dgBseIndex.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgBseIndex.Size = New System.Drawing.Size(845, 536)
        Me.dgBseIndex.TabIndex = 0
        '
        'Timer1
        '
        '
        'btnTest
        '
        Me.btnTest.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTest.Location = New System.Drawing.Point(349, 563)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(97, 26)
        Me.btnTest.TabIndex = 1
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'FrmBseIndexView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(856, 605)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.dgBseIndex)
        Me.Name = "FrmBseIndexView"
        Me.Text = "Bse Index"
        CType(Me.dgBseIndex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgBseIndex As DataGridView
    Friend WithEvents Timer1 As Timer
    Friend WithEvents btnTest As Button
End Class
