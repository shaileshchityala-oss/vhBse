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
        Me.Schtcallperm = New AxSTOCKCHARTXLib.AxStockChartX
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Schtputprem = New AxSTOCKCHARTXLib.AxStockChartX
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.Schtcalldelta = New AxSTOCKCHARTXLib.AxStockChartX
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.Schtputdelta = New AxSTOCKCHARTXLib.AxStockChartX
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.Schtgamma = New AxSTOCKCHARTXLib.AxStockChartX
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.Schtvega = New AxSTOCKCHARTXLib.AxStockChartX
        Me.TabPage7 = New System.Windows.Forms.TabPage
        Me.Schtcalltheta = New AxSTOCKCHARTXLib.AxStockChartX
        Me.TabPage8 = New System.Windows.Forms.TabPage
        Me.Schtputtheta = New AxSTOCKCHARTXLib.AxStockChartX
        Me.TabPage9 = New System.Windows.Forms.TabPage
        Me.Schtcallroh = New AxSTOCKCHARTXLib.AxStockChartX
        Me.TabPage10 = New System.Windows.Forms.TabPage
        Me.Schtputroh = New AxSTOCKCHARTXLib.AxStockChartX
        Me.TabPage11 = New System.Windows.Forms.TabPage
        Me.SchtVolga = New AxSTOCKCHARTXLib.AxStockChartX
        Me.TabPage12 = New System.Windows.Forms.TabPage
        Me.SchtVanna = New AxSTOCKCHARTXLib.AxStockChartX
        Me.All = New System.Windows.Forms.TabPage
        Me.StockChart1 = New AxSTOCKCHARTXLib.AxStockChartX
        Me.btnSelectChart = New System.Windows.Forms.Button
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Schtcallperm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Schtputprem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Schtcalldelta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.Schtputdelta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.Schtgamma, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        CType(Me.Schtvega, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage7.SuspendLayout()
        CType(Me.Schtcalltheta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage8.SuspendLayout()
        CType(Me.Schtputtheta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage9.SuspendLayout()
        CType(Me.Schtcallroh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage10.SuspendLayout()
        CType(Me.Schtputroh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage11.SuspendLayout()
        CType(Me.SchtVolga, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage12.SuspendLayout()
        CType(Me.SchtVanna, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.All.SuspendLayout()
        CType(Me.StockChart1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TabControl1.Controls.Add(Me.TabPage11)
        Me.TabControl1.Controls.Add(Me.TabPage12)
        Me.TabControl1.Controls.Add(Me.All)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1016, 686)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage1.Controls.Add(Me.Schtcallperm)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(1008, 660)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Call Premium  "
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Schtcallperm
        '
        Me.Schtcallperm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Schtcallperm.Enabled = True
        Me.Schtcallperm.Location = New System.Drawing.Point(0, 0)
        Me.Schtcallperm.Name = "Schtcallperm"
        Me.Schtcallperm.OcxState = CType(resources.GetObject("Schtcallperm.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Schtcallperm.Size = New System.Drawing.Size(1008, 660)
        Me.Schtcallperm.TabIndex = 2
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage2.Controls.Add(Me.Schtputprem)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(1008, 660)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Put Premium  "
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Schtputprem
        '
        Me.Schtputprem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Schtputprem.Enabled = True
        Me.Schtputprem.Location = New System.Drawing.Point(0, 0)
        Me.Schtputprem.Name = "Schtputprem"
        Me.Schtputprem.OcxState = CType(resources.GetObject("Schtputprem.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Schtputprem.Size = New System.Drawing.Size(1008, 660)
        Me.Schtputprem.TabIndex = 6
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage3.Controls.Add(Me.Schtcalldelta)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1008, 660)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Call Delta  "
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Schtcalldelta
        '
        Me.Schtcalldelta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Schtcalldelta.Enabled = True
        Me.Schtcalldelta.Location = New System.Drawing.Point(0, 0)
        Me.Schtcalldelta.Name = "Schtcalldelta"
        Me.Schtcalldelta.OcxState = CType(resources.GetObject("Schtcalldelta.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Schtcalldelta.Size = New System.Drawing.Size(1008, 660)
        Me.Schtcalldelta.TabIndex = 7
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage4.Controls.Add(Me.Schtputdelta)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(1008, 660)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Put Delta  "
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Schtputdelta
        '
        Me.Schtputdelta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Schtputdelta.Enabled = True
        Me.Schtputdelta.Location = New System.Drawing.Point(0, 0)
        Me.Schtputdelta.Name = "Schtputdelta"
        Me.Schtputdelta.OcxState = CType(resources.GetObject("Schtputdelta.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Schtputdelta.Size = New System.Drawing.Size(1008, 660)
        Me.Schtputdelta.TabIndex = 8
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage5.Controls.Add(Me.Schtgamma)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(1008, 660)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Gamma  "
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Schtgamma
        '
        Me.Schtgamma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Schtgamma.Enabled = True
        Me.Schtgamma.Location = New System.Drawing.Point(0, 0)
        Me.Schtgamma.Name = "Schtgamma"
        Me.Schtgamma.OcxState = CType(resources.GetObject("Schtgamma.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Schtgamma.Size = New System.Drawing.Size(1008, 660)
        Me.Schtgamma.TabIndex = 9
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage6.Controls.Add(Me.Schtvega)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(1008, 660)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Vega  "
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'Schtvega
        '
        Me.Schtvega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Schtvega.Enabled = True
        Me.Schtvega.Location = New System.Drawing.Point(0, 0)
        Me.Schtvega.Name = "Schtvega"
        Me.Schtvega.OcxState = CType(resources.GetObject("Schtvega.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Schtvega.Size = New System.Drawing.Size(1008, 660)
        Me.Schtvega.TabIndex = 10
        '
        'TabPage7
        '
        Me.TabPage7.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage7.Controls.Add(Me.Schtcalltheta)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(1008, 660)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "Call Theta  "
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'Schtcalltheta
        '
        Me.Schtcalltheta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Schtcalltheta.Enabled = True
        Me.Schtcalltheta.Location = New System.Drawing.Point(0, 0)
        Me.Schtcalltheta.Name = "Schtcalltheta"
        Me.Schtcalltheta.OcxState = CType(resources.GetObject("Schtcalltheta.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Schtcalltheta.Size = New System.Drawing.Size(1008, 660)
        Me.Schtcalltheta.TabIndex = 10
        '
        'TabPage8
        '
        Me.TabPage8.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage8.Controls.Add(Me.Schtputtheta)
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(1008, 660)
        Me.TabPage8.TabIndex = 7
        Me.TabPage8.Text = "Put Theta  "
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'Schtputtheta
        '
        Me.Schtputtheta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Schtputtheta.Enabled = True
        Me.Schtputtheta.Location = New System.Drawing.Point(0, 0)
        Me.Schtputtheta.Name = "Schtputtheta"
        Me.Schtputtheta.OcxState = CType(resources.GetObject("Schtputtheta.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Schtputtheta.Size = New System.Drawing.Size(1008, 660)
        Me.Schtputtheta.TabIndex = 10
        '
        'TabPage9
        '
        Me.TabPage9.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage9.Controls.Add(Me.Schtcallroh)
        Me.TabPage9.Location = New System.Drawing.Point(4, 22)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Size = New System.Drawing.Size(1008, 660)
        Me.TabPage9.TabIndex = 8
        Me.TabPage9.Text = "Call Rho  "
        Me.TabPage9.UseVisualStyleBackColor = True
        '
        'Schtcallroh
        '
        Me.Schtcallroh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Schtcallroh.Enabled = True
        Me.Schtcallroh.Location = New System.Drawing.Point(0, 0)
        Me.Schtcallroh.Name = "Schtcallroh"
        Me.Schtcallroh.OcxState = CType(resources.GetObject("Schtcallroh.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Schtcallroh.Size = New System.Drawing.Size(1008, 660)
        Me.Schtcallroh.TabIndex = 10
        '
        'TabPage10
        '
        Me.TabPage10.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage10.Controls.Add(Me.Schtputroh)
        Me.TabPage10.Location = New System.Drawing.Point(4, 22)
        Me.TabPage10.Name = "TabPage10"
        Me.TabPage10.Size = New System.Drawing.Size(1008, 660)
        Me.TabPage10.TabIndex = 9
        Me.TabPage10.Text = "Put Rho  "
        Me.TabPage10.UseVisualStyleBackColor = True
        '
        'Schtputroh
        '
        Me.Schtputroh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Schtputroh.Enabled = True
        Me.Schtputroh.Location = New System.Drawing.Point(0, 0)
        Me.Schtputroh.Name = "Schtputroh"
        Me.Schtputroh.OcxState = CType(resources.GetObject("Schtputroh.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Schtputroh.Size = New System.Drawing.Size(1008, 660)
        Me.Schtputroh.TabIndex = 10
        '
        'TabPage11
        '
        Me.TabPage11.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage11.Controls.Add(Me.SchtVolga)
        Me.TabPage11.Location = New System.Drawing.Point(4, 22)
        Me.TabPage11.Name = "TabPage11"
        Me.TabPage11.Size = New System.Drawing.Size(1008, 660)
        Me.TabPage11.TabIndex = 11
        Me.TabPage11.Text = "Volga"
        Me.TabPage11.UseVisualStyleBackColor = True
        '
        'SchtVolga
        '
        Me.SchtVolga.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SchtVolga.Enabled = True
        Me.SchtVolga.Location = New System.Drawing.Point(0, 0)
        Me.SchtVolga.Name = "SchtVolga"
        Me.SchtVolga.OcxState = CType(resources.GetObject("SchtVolga.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SchtVolga.Size = New System.Drawing.Size(1008, 660)
        Me.SchtVolga.TabIndex = 10
        '
        'TabPage12
        '
        Me.TabPage12.BackColor = System.Drawing.SystemColors.WindowText
        Me.TabPage12.Controls.Add(Me.SchtVanna)
        Me.TabPage12.Location = New System.Drawing.Point(4, 22)
        Me.TabPage12.Name = "TabPage12"
        Me.TabPage12.Size = New System.Drawing.Size(1008, 660)
        Me.TabPage12.TabIndex = 12
        Me.TabPage12.Text = "Vanna"
        Me.TabPage12.UseVisualStyleBackColor = True
        '
        'SchtVanna
        '
        Me.SchtVanna.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SchtVanna.Enabled = True
        Me.SchtVanna.Location = New System.Drawing.Point(0, 0)
        Me.SchtVanna.Name = "SchtVanna"
        Me.SchtVanna.OcxState = CType(resources.GetObject("SchtVanna.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SchtVanna.Size = New System.Drawing.Size(1008, 660)
        Me.SchtVanna.TabIndex = 11
        '
        'All
        '
        Me.All.BackColor = System.Drawing.SystemColors.WindowText
        Me.All.Controls.Add(Me.StockChart1)
        Me.All.Location = New System.Drawing.Point(4, 22)
        Me.All.Name = "All"
        Me.All.Size = New System.Drawing.Size(1008, 660)
        Me.All.TabIndex = 10
        Me.All.Text = "All"
        Me.All.UseVisualStyleBackColor = True
        '
        'StockChart1
        '
        Me.StockChart1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StockChart1.Enabled = True
        Me.StockChart1.Location = New System.Drawing.Point(0, 0)
        Me.StockChart1.Name = "StockChart1"
        Me.StockChart1.OcxState = CType(resources.GetObject("StockChart1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.StockChart1.Size = New System.Drawing.Size(1008, 660)
        Me.StockChart1.TabIndex = 3
        '
        'btnSelectChart
        '
        Me.btnSelectChart.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSelectChart.Location = New System.Drawing.Point(868, 1)
        Me.btnSelectChart.Name = "btnSelectChart"
        Me.btnSelectChart.Size = New System.Drawing.Size(91, 22)
        Me.btnSelectChart.TabIndex = 2
        Me.btnSelectChart.Text = "Select Chart"
        Me.btnSelectChart.UseVisualStyleBackColor = False
        '
        'FrmOptionChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1016, 686)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnSelectChart)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmOptionChart"
        Me.Text = "Option Chart"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Schtcallperm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Schtputprem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Schtcalldelta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.Schtputdelta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        CType(Me.Schtgamma, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        CType(Me.Schtvega, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage7.ResumeLayout(False)
        CType(Me.Schtcalltheta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage8.ResumeLayout(False)
        CType(Me.Schtputtheta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage9.ResumeLayout(False)
        CType(Me.Schtcallroh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage10.ResumeLayout(False)
        CType(Me.Schtputroh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage11.ResumeLayout(False)
        CType(Me.SchtVolga, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage12.ResumeLayout(False)
        CType(Me.SchtVanna, System.ComponentModel.ISupportInitialize).EndInit()
        Me.All.ResumeLayout(False)
        CType(Me.StockChart1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Public WithEvents TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents TabPage5 As System.Windows.Forms.TabPage
    Public WithEvents TabPage6 As System.Windows.Forms.TabPage
    Public WithEvents TabPage7 As System.Windows.Forms.TabPage
    Public WithEvents TabPage8 As System.Windows.Forms.TabPage
    Public WithEvents TabPage9 As System.Windows.Forms.TabPage
    Public WithEvents TabPage10 As System.Windows.Forms.TabPage
    Friend WithEvents Schtcallperm As AxSTOCKCHARTXLib.AxStockChartX
    Friend WithEvents Schtputprem As AxSTOCKCHARTXLib.AxStockChartX
    Friend WithEvents Schtcalldelta As AxSTOCKCHARTXLib.AxStockChartX
    Friend WithEvents Schtputdelta As AxSTOCKCHARTXLib.AxStockChartX
    Friend WithEvents Schtgamma As AxSTOCKCHARTXLib.AxStockChartX
    Friend WithEvents Schtvega As AxSTOCKCHARTXLib.AxStockChartX
    Friend WithEvents Schtcalltheta As AxSTOCKCHARTXLib.AxStockChartX
    Friend WithEvents Schtputtheta As AxSTOCKCHARTXLib.AxStockChartX
    Friend WithEvents Schtcallroh As AxSTOCKCHARTXLib.AxStockChartX
    Friend WithEvents Schtputroh As AxSTOCKCHARTXLib.AxStockChartX
    Friend WithEvents All As System.Windows.Forms.TabPage
    Friend WithEvents StockChart1 As AxSTOCKCHARTXLib.AxStockChartX
    Friend WithEvents btnSelectChart As System.Windows.Forms.Button
    Friend WithEvents TabPage11 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage12 As System.Windows.Forms.TabPage
    Friend WithEvents SchtVolga As AxSTOCKCHARTXLib.AxStockChartX
    Friend WithEvents SchtVanna As AxSTOCKCHARTXLib.AxStockChartX
End Class
