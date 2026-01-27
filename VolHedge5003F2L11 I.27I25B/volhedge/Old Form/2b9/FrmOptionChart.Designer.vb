<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOptionChart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmOptionChart))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.chtcallperm = New AxMSChart20Lib.AxMSChart
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.chtputprem = New AxMSChart20Lib.AxMSChart
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.chtcalldelta = New AxMSChart20Lib.AxMSChart
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.chtputdelta = New AxMSChart20Lib.AxMSChart
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.chtgamma = New AxMSChart20Lib.AxMSChart
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.chtvega = New AxMSChart20Lib.AxMSChart
        Me.TabPage7 = New System.Windows.Forms.TabPage
        Me.chtcalltheta = New AxMSChart20Lib.AxMSChart
        Me.TabPage8 = New System.Windows.Forms.TabPage
        Me.chtputtheta = New AxMSChart20Lib.AxMSChart
        Me.TabPage9 = New System.Windows.Forms.TabPage
        Me.chtcallroh = New AxMSChart20Lib.AxMSChart
        Me.TabPage10 = New System.Windows.Forms.TabPage
        Me.chtputroh = New AxMSChart20Lib.AxMSChart
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.chtcallperm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.chtputprem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.chtcalldelta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.chtputdelta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.chtgamma, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        CType(Me.chtvega, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage7.SuspendLayout()
        CType(Me.chtcalltheta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage8.SuspendLayout()
        CType(Me.chtputtheta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage9.SuspendLayout()
        CType(Me.chtcallroh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage10.SuspendLayout()
        CType(Me.chtputroh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Controls.Add(Me.TabPage8)
        Me.TabControl1.Controls.Add(Me.TabPage9)
        Me.TabControl1.Controls.Add(Me.TabPage10)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(843, 608)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage1.Controls.Add(Me.chtcallperm)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(835, 582)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Call Premium  "
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'chtcallperm
        '
        Me.chtcallperm.DataSource = Nothing
        Me.chtcallperm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chtcallperm.Location = New System.Drawing.Point(0, 0)
        Me.chtcallperm.Name = "chtcallperm"
        Me.chtcallperm.OcxState = CType(resources.GetObject("chtcallperm.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtcallperm.Size = New System.Drawing.Size(835, 582)
        Me.chtcallperm.TabIndex = 3
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage2.Controls.Add(Me.chtputprem)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(835, 582)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Put Premium  "
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chtputprem
        '
        Me.chtputprem.DataSource = Nothing
        Me.chtputprem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chtputprem.Location = New System.Drawing.Point(0, 0)
        Me.chtputprem.Name = "chtputprem"
        Me.chtputprem.OcxState = CType(resources.GetObject("chtputprem.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtputprem.Size = New System.Drawing.Size(835, 582)
        Me.chtputprem.TabIndex = 4
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage3.Controls.Add(Me.chtcalldelta)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(835, 582)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Call Delta  "
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'chtcalldelta
        '
        Me.chtcalldelta.DataSource = Nothing
        Me.chtcalldelta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chtcalldelta.Location = New System.Drawing.Point(0, 0)
        Me.chtcalldelta.Name = "chtcalldelta"
        Me.chtcalldelta.OcxState = CType(resources.GetObject("chtcalldelta.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtcalldelta.Size = New System.Drawing.Size(835, 582)
        Me.chtcalldelta.TabIndex = 4
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage4.Controls.Add(Me.chtputdelta)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(835, 582)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Put Delta  "
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'chtputdelta
        '
        Me.chtputdelta.DataSource = Nothing
        Me.chtputdelta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chtputdelta.Location = New System.Drawing.Point(0, 0)
        Me.chtputdelta.Name = "chtputdelta"
        Me.chtputdelta.OcxState = CType(resources.GetObject("chtputdelta.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtputdelta.Size = New System.Drawing.Size(835, 582)
        Me.chtputdelta.TabIndex = 4
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage5.Controls.Add(Me.chtgamma)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(835, 582)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Gamma  "
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'chtgamma
        '
        Me.chtgamma.DataSource = Nothing
        Me.chtgamma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chtgamma.Location = New System.Drawing.Point(0, 0)
        Me.chtgamma.Name = "chtgamma"
        Me.chtgamma.OcxState = CType(resources.GetObject("chtgamma.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtgamma.Size = New System.Drawing.Size(835, 582)
        Me.chtgamma.TabIndex = 4
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage6.Controls.Add(Me.chtvega)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(835, 582)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Vega  "
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'chtvega
        '
        Me.chtvega.DataSource = Nothing
        Me.chtvega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chtvega.Location = New System.Drawing.Point(0, 0)
        Me.chtvega.Name = "chtvega"
        Me.chtvega.OcxState = CType(resources.GetObject("chtvega.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtvega.Size = New System.Drawing.Size(835, 582)
        Me.chtvega.TabIndex = 4
        '
        'TabPage7
        '
        Me.TabPage7.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage7.Controls.Add(Me.chtcalltheta)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(835, 582)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "Call Theta  "
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'chtcalltheta
        '
        Me.chtcalltheta.DataSource = Nothing
        Me.chtcalltheta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chtcalltheta.Location = New System.Drawing.Point(0, 0)
        Me.chtcalltheta.Name = "chtcalltheta"
        Me.chtcalltheta.OcxState = CType(resources.GetObject("chtcalltheta.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtcalltheta.Size = New System.Drawing.Size(835, 582)
        Me.chtcalltheta.TabIndex = 4
        '
        'TabPage8
        '
        Me.TabPage8.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage8.Controls.Add(Me.chtputtheta)
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(835, 582)
        Me.TabPage8.TabIndex = 7
        Me.TabPage8.Text = "Put Theta  "
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'chtputtheta
        '
        Me.chtputtheta.DataSource = Nothing
        Me.chtputtheta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chtputtheta.Location = New System.Drawing.Point(0, 0)
        Me.chtputtheta.Name = "chtputtheta"
        Me.chtputtheta.OcxState = CType(resources.GetObject("chtputtheta.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtputtheta.Size = New System.Drawing.Size(835, 582)
        Me.chtputtheta.TabIndex = 4
        '
        'TabPage9
        '
        Me.TabPage9.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage9.Controls.Add(Me.chtcallroh)
        Me.TabPage9.Location = New System.Drawing.Point(4, 22)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Size = New System.Drawing.Size(835, 582)
        Me.TabPage9.TabIndex = 8
        Me.TabPage9.Text = "Call Rho  "
        Me.TabPage9.UseVisualStyleBackColor = True
        '
        'chtcallroh
        '
        Me.chtcallroh.DataSource = Nothing
        Me.chtcallroh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chtcallroh.Location = New System.Drawing.Point(0, 0)
        Me.chtcallroh.Name = "chtcallroh"
        Me.chtcallroh.OcxState = CType(resources.GetObject("chtcallroh.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtcallroh.Size = New System.Drawing.Size(835, 582)
        Me.chtcallroh.TabIndex = 4
        '
        'TabPage10
        '
        Me.TabPage10.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage10.Controls.Add(Me.chtputroh)
        Me.TabPage10.Location = New System.Drawing.Point(4, 22)
        Me.TabPage10.Name = "TabPage10"
        Me.TabPage10.Size = New System.Drawing.Size(835, 582)
        Me.TabPage10.TabIndex = 9
        Me.TabPage10.Text = "Put Rho  "
        Me.TabPage10.UseVisualStyleBackColor = True
        '
        'chtputroh
        '
        Me.chtputroh.DataSource = Nothing
        Me.chtputroh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chtputroh.Location = New System.Drawing.Point(0, 0)
        Me.chtputroh.Name = "chtputroh"
        Me.chtputroh.OcxState = CType(resources.GetObject("chtputroh.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtputroh.Size = New System.Drawing.Size(835, 582)
        Me.chtputroh.TabIndex = 4
        '
        'FrmOptionChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(843, 608)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmOptionChart"
        Me.Text = "Option Chart"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.chtcallperm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.chtputprem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.chtcalldelta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.chtputdelta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        CType(Me.chtgamma, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        CType(Me.chtvega, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage7.ResumeLayout(False)
        CType(Me.chtcalltheta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage8.ResumeLayout(False)
        CType(Me.chtputtheta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage9.ResumeLayout(False)
        CType(Me.chtcallroh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage10.ResumeLayout(False)
        CType(Me.chtputroh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    'Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    'Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    'Friend WithEvents chtcallperm As AxMSChart20Lib.AxMSChart
    'Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    'Friend WithEvents chtputprem As AxMSChart20Lib.AxMSChart
    'Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    'Friend WithEvents chtcalldelta As AxMSChart20Lib.AxMSChart
    'Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    'Friend WithEvents chtputdelta As AxMSChart20Lib.AxMSChart
    'Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    'Friend WithEvents chtgamma As AxMSChart20Lib.AxMSChart
    'Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    'Friend WithEvents chtvega As AxMSChart20Lib.AxMSChart
    'Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    'Friend WithEvents chtcalltheta As AxMSChart20Lib.AxMSChart
    'Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
    'Friend WithEvents chtputtheta As AxMSChart20Lib.AxMSChart
    'Friend WithEvents TabPage9 As System.Windows.Forms.TabPage
    'Friend WithEvents chtcallroh As AxMSChart20Lib.AxMSChart
    'Friend WithEvents TabPage10 As System.Windows.Forms.TabPage
    'Friend WithEvents chtputroh As AxMSChart20Lib.AxMSChart
    Public WithEvents TabControl1 As System.Windows.Forms.TabControl
    Public WithEvents TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents chtcallperm As AxMSChart20Lib.AxMSChart
    Public WithEvents TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents chtputprem As AxMSChart20Lib.AxMSChart
    Public WithEvents TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents chtcalldelta As AxMSChart20Lib.AxMSChart
    Public WithEvents TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents chtputdelta As AxMSChart20Lib.AxMSChart
    Public WithEvents TabPage5 As System.Windows.Forms.TabPage
    Public WithEvents chtgamma As AxMSChart20Lib.AxMSChart
    Public WithEvents TabPage6 As System.Windows.Forms.TabPage
    Public WithEvents chtvega As AxMSChart20Lib.AxMSChart
    Public WithEvents TabPage7 As System.Windows.Forms.TabPage
    Public WithEvents chtcalltheta As AxMSChart20Lib.AxMSChart
    Public WithEvents TabPage8 As System.Windows.Forms.TabPage
    Public WithEvents chtputtheta As AxMSChart20Lib.AxMSChart
    Public WithEvents TabPage9 As System.Windows.Forms.TabPage
    Public WithEvents chtcallroh As AxMSChart20Lib.AxMSChart
    Public WithEvents TabPage10 As System.Windows.Forms.TabPage
    Public WithEvents chtputroh As AxMSChart20Lib.AxMSChart
End Class
