<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTradefilesetting
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.cmbProfile = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFormateName = New System.Windows.Forms.TextBox()
        Me.dgvTradeFile = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cmbtype = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnimport = New System.Windows.Forms.Button()
        Me.btnBrowseBackupPath = New System.Windows.Forms.Button()
        Me.txtfilepath = New System.Windows.Forms.TextBox()
        Me.btnexport = New System.Windows.Forms.Button()
        CType(Me.dgvTradeFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(555, 459)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(56, 22)
        Me.btnDelete.TabIndex = 13
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'cmbProfile
        '
        Me.cmbProfile.FormattingEnabled = True
        Me.cmbProfile.Location = New System.Drawing.Point(143, 41)
        Me.cmbProfile.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbProfile.Name = "cmbProfile"
        Me.cmbProfile.Size = New System.Drawing.Size(119, 21)
        Me.cmbProfile.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 44)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Select Formate Name"
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(615, 460)
        Me.btnOk.Margin = New System.Windows.Forms.Padding(2)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(73, 21)
        Me.btnOk.TabIndex = 10
        Me.btnOk.Text = "Save"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 57)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Save Formate Name"
        '
        'txtFormateName
        '
        Me.txtFormateName.Location = New System.Drawing.Point(135, 50)
        Me.txtFormateName.Margin = New System.Windows.Forms.Padding(2)
        Me.txtFormateName.Name = "txtFormateName"
        Me.txtFormateName.Size = New System.Drawing.Size(141, 20)
        Me.txtFormateName.TabIndex = 8
        '
        'dgvTradeFile
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTradeFile.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvTradeFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTradeFile.DefaultCellStyle = DataGridViewCellStyle10
        Me.dgvTradeFile.Location = New System.Drawing.Point(8, 91)
        Me.dgvTradeFile.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvTradeFile.Name = "dgvTradeFile"
        Me.dgvTradeFile.RowTemplate.Height = 24
        Me.dgvTradeFile.Size = New System.Drawing.Size(680, 364)
        Me.dgvTradeFile.TabIndex = 7
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(495, 459)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(56, 21)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "New"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cmbtype
        '
        Me.cmbtype.FormattingEnabled = True
        Me.cmbtype.Items.AddRange(New Object() {"FO", "EQ", "CURR"})
        Me.cmbtype.Location = New System.Drawing.Point(142, 12)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(121, 21)
        Me.cmbtype.TabIndex = 14
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 15)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(124, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Select Formate Type"
        '
        'btnimport
        '
        Me.btnimport.Location = New System.Drawing.Point(566, 44)
        Me.btnimport.Margin = New System.Windows.Forms.Padding(2)
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Size = New System.Drawing.Size(101, 21)
        Me.btnimport.TabIndex = 16
        Me.btnimport.Text = "Import Template"
        Me.btnimport.UseVisualStyleBackColor = True
        '
        'btnBrowseBackupPath
        '
        Me.btnBrowseBackupPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseBackupPath.Location = New System.Drawing.Point(601, 12)
        Me.btnBrowseBackupPath.Name = "btnBrowseBackupPath"
        Me.btnBrowseBackupPath.Size = New System.Drawing.Size(66, 23)
        Me.btnBrowseBackupPath.TabIndex = 17
        Me.btnBrowseBackupPath.Text = "Browse"
        Me.btnBrowseBackupPath.UseVisualStyleBackColor = True
        '
        'txtfilepath
        '
        Me.txtfilepath.Location = New System.Drawing.Point(372, 15)
        Me.txtfilepath.Margin = New System.Windows.Forms.Padding(2)
        Me.txtfilepath.Name = "txtfilepath"
        Me.txtfilepath.Size = New System.Drawing.Size(224, 20)
        Me.txtfilepath.TabIndex = 18
        '
        'btnexport
        '
        Me.btnexport.Location = New System.Drawing.Point(390, 459)
        Me.btnexport.Margin = New System.Windows.Forms.Padding(2)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(101, 21)
        Me.btnexport.TabIndex = 19
        Me.btnexport.Text = "Export Template"
        Me.btnexport.UseVisualStyleBackColor = True
        '
        'frmTradefilesetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 485)
        Me.Controls.Add(Me.btnexport)
        Me.Controls.Add(Me.txtfilepath)
        Me.Controls.Add(Me.btnBrowseBackupPath)
        Me.Controls.Add(Me.btnimport)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbtype)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.cmbProfile)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFormateName)
        Me.Controls.Add(Me.dgvTradeFile)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmTradefilesetting"
        Me.Text = "Custom Trade File Setting"
        CType(Me.dgvTradeFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents cmbProfile As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFormateName As System.Windows.Forms.TextBox
    Friend WithEvents dgvTradeFile As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnimport As Button
    Friend WithEvents btnBrowseBackupPath As Button
    Friend WithEvents txtfilepath As TextBox
    Friend WithEvents btnexport As Button
End Class
