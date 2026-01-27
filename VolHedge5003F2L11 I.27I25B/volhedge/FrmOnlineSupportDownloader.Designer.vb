<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOnlineSupportDownloader
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
        Me.tlpUltra = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.txtUrl = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.tlpUltra.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpUltra
        '
        Me.tlpUltra.AutoSize = True
        Me.tlpUltra.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpUltra.ColumnCount = 2
        Me.tlpUltra.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpUltra.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpUltra.Controls.Add(Me.btnDownload, 1, 2)
        Me.tlpUltra.Controls.Add(Me.txtUrl, 1, 0)
        Me.tlpUltra.Controls.Add(Me.Label2, 0, 1)
        Me.tlpUltra.Controls.Add(Me.CheckBox1, 0, 0)
        Me.tlpUltra.Controls.Add(Me.lblProgress, 1, 1)
        Me.tlpUltra.Location = New System.Drawing.Point(16, 6)
        Me.tlpUltra.Name = "tlpUltra"
        Me.tlpUltra.RowCount = 3
        Me.tlpUltra.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpUltra.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpUltra.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpUltra.Size = New System.Drawing.Size(856, 71)
        Me.tlpUltra.TabIndex = 0
        '
        'btnDownload
        '
        Me.btnDownload.Location = New System.Drawing.Point(80, 45)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(125, 23)
        Me.btnDownload.TabIndex = 1
        Me.btnDownload.Text = "Download"
        Me.btnDownload.UseVisualStyleBackColor = True
        '
        'txtUrl
        '
        Me.txtUrl.Location = New System.Drawing.Point(80, 3)
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(773, 20)
        Me.txtUrl.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Progress"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(3, 3)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(47, 20)
        Me.CheckBox1.TabIndex = 1
        Me.CheckBox1.Text = "Url"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(80, 26)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(39, 13)
        Me.lblProgress.TabIndex = 0
        Me.lblProgress.Text = "Label1"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(883, 255)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.tlpUltra)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(875, 229)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "UltraViewer"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'FrmOnlineSupportDownloader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(883, 255)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "FrmOnlineSupportDownloader"
        Me.Text = "Online Support Downloader"
        Me.tlpUltra.ResumeLayout(False)
        Me.tlpUltra.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tlpUltra As TableLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents lblProgress As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents txtUrl As TextBox
    Friend WithEvents btnDownload As Button
End Class
