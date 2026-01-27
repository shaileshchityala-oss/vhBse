<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmprofitLossChart
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmprofitLossChart))
        Me.tbcon = New System.Windows.Forms.TabControl()
        Me.tbprofitchart = New System.Windows.Forms.TabPage()
        Me.chartPNL = New ScottPlot.WinForms.FormsPlot()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblprofit = New System.Windows.Forms.Label()
        Me.tbgrdProfit = New System.Windows.Forms.TabPage()
        Me.grdprofit = New System.Windows.Forms.DataGridView()
        Me.tbchdelta = New System.Windows.Forms.TabPage()
        Me.chartDelta = New ScottPlot.WinForms.FormsPlot()
        Me.lbldelta = New System.Windows.Forms.Label()
        Me.tbgrdDelta = New System.Windows.Forms.TabPage()
        Me.grddelta = New System.Windows.Forms.DataGridView()
        Me.tbchgamma = New System.Windows.Forms.TabPage()
        Me.chartGamma = New ScottPlot.WinForms.FormsPlot()
        Me.lblgamma = New System.Windows.Forms.Label()
        Me.tbgrdGamma = New System.Windows.Forms.TabPage()
        Me.grdgamma = New System.Windows.Forms.DataGridView()
        Me.tbchvega = New System.Windows.Forms.TabPage()
        Me.chartVega = New ScottPlot.WinForms.FormsPlot()
        Me.lblvega = New System.Windows.Forms.Label()
        Me.tbgrdVega = New System.Windows.Forms.TabPage()
        Me.grdvega = New System.Windows.Forms.DataGridView()
        Me.tbchtheta = New System.Windows.Forms.TabPage()
        Me.chartTheta = New ScottPlot.WinForms.FormsPlot()
        Me.lbltheta = New System.Windows.Forms.Label()
        Me.tbgrdTheta = New System.Windows.Forms.TabPage()
        Me.grdtheta = New System.Windows.Forms.DataGridView()
        Me.tbchvolga = New System.Windows.Forms.TabPage()
        Me.chartVolga = New ScottPlot.WinForms.FormsPlot()
        Me.lblvolga = New System.Windows.Forms.Label()
        Me.tbgrdVolga = New System.Windows.Forms.TabPage()
        Me.grdvolga = New System.Windows.Forms.DataGridView()
        Me.tbchvanna = New System.Windows.Forms.TabPage()
        Me.chartVanna = New ScottPlot.WinForms.FormsPlot()
        Me.lblvanna = New System.Windows.Forms.Label()
        Me.tbgrdVanna = New System.Windows.Forms.TabPage()
        Me.grdvanna = New System.Windows.Forms.DataGridView()
        Me.tbcon.SuspendLayout()
        Me.tbprofitchart.SuspendLayout()
        Me.tbgrdProfit.SuspendLayout()
        CType(Me.grdprofit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbchdelta.SuspendLayout()
        Me.tbgrdDelta.SuspendLayout()
        CType(Me.grddelta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbchgamma.SuspendLayout()
        Me.tbgrdGamma.SuspendLayout()
        CType(Me.grdgamma, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbchvega.SuspendLayout()
        Me.tbgrdVega.SuspendLayout()
        CType(Me.grdvega, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbchtheta.SuspendLayout()
        Me.tbgrdTheta.SuspendLayout()
        CType(Me.grdtheta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbchvolga.SuspendLayout()
        Me.tbgrdVolga.SuspendLayout()
        CType(Me.grdvolga, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbchvanna.SuspendLayout()
        Me.tbgrdVanna.SuspendLayout()
        CType(Me.grdvanna, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbcon
        '
        Me.tbcon.Controls.Add(Me.tbprofitchart)
        Me.tbcon.Controls.Add(Me.tbgrdProfit)
        Me.tbcon.Controls.Add(Me.tbchdelta)
        Me.tbcon.Controls.Add(Me.tbgrdDelta)
        Me.tbcon.Controls.Add(Me.tbchgamma)
        Me.tbcon.Controls.Add(Me.tbgrdGamma)
        Me.tbcon.Controls.Add(Me.tbchvega)
        Me.tbcon.Controls.Add(Me.tbgrdVega)
        Me.tbcon.Controls.Add(Me.tbchtheta)
        Me.tbcon.Controls.Add(Me.tbgrdTheta)
        Me.tbcon.Controls.Add(Me.tbchvolga)
        Me.tbcon.Controls.Add(Me.tbgrdVolga)
        Me.tbcon.Controls.Add(Me.tbchvanna)
        Me.tbcon.Controls.Add(Me.tbgrdVanna)
        Me.tbcon.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcon.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.tbcon.ItemSize = New System.Drawing.Size(70, 18)
        Me.tbcon.Location = New System.Drawing.Point(0, 0)
        Me.tbcon.Name = "tbcon"
        Me.tbcon.SelectedIndex = 0
        Me.tbcon.Size = New System.Drawing.Size(805, 540)
        Me.tbcon.TabIndex = 2
        '
        'tbprofitchart
        '
        Me.tbprofitchart.AutoScroll = True
        Me.tbprofitchart.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbprofitchart.Controls.Add(Me.chartPNL)
        Me.tbprofitchart.Controls.Add(Me.Label1)
        Me.tbprofitchart.Controls.Add(Me.lblprofit)
        Me.tbprofitchart.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbprofitchart.Location = New System.Drawing.Point(4, 22)
        Me.tbprofitchart.Name = "tbprofitchart"
        Me.tbprofitchart.Size = New System.Drawing.Size(797, 514)
        Me.tbprofitchart.TabIndex = 5
        Me.tbprofitchart.Text = "Profit & Loss Chart "
        Me.tbprofitchart.UseVisualStyleBackColor = True
        '
        'chartPNL
        '
        Me.chartPNL.DisplayScale = 0!
        Me.chartPNL.Location = New System.Drawing.Point(33, 146)
        Me.chartPNL.Name = "chartPNL"
        Me.chartPNL.Size = New System.Drawing.Size(691, 222)
        Me.chartPNL.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.LightCoral
        Me.Label1.Location = New System.Drawing.Point(703, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "-"
        '
        'lblprofit
        '
        Me.lblprofit.AutoSize = True
        Me.lblprofit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblprofit.ForeColor = System.Drawing.Color.LightCoral
        Me.lblprofit.Location = New System.Drawing.Point(703, 0)
        Me.lblprofit.Name = "lblprofit"
        Me.lblprofit.Size = New System.Drawing.Size(11, 13)
        Me.lblprofit.TabIndex = 1
        Me.lblprofit.Text = "-"
        '
        'tbgrdProfit
        '
        Me.tbgrdProfit.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbgrdProfit.Controls.Add(Me.grdprofit)
        Me.tbgrdProfit.Location = New System.Drawing.Point(4, 22)
        Me.tbgrdProfit.Name = "tbgrdProfit"
        Me.tbgrdProfit.Size = New System.Drawing.Size(797, 514)
        Me.tbgrdProfit.TabIndex = 12
        Me.tbgrdProfit.Text = "P & L Value"
        Me.tbgrdProfit.UseVisualStyleBackColor = True
        '
        'grdprofit
        '
        Me.grdprofit.AllowUserToAddRows = False
        Me.grdprofit.AllowUserToDeleteRows = False
        Me.grdprofit.AllowUserToResizeColumns = False
        Me.grdprofit.AllowUserToResizeRows = False
        Me.grdprofit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.grdprofit.BackgroundColor = System.Drawing.SystemColors.WindowText
        Me.grdprofit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdprofit.DefaultCellStyle = DataGridViewCellStyle1
        Me.grdprofit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdprofit.Location = New System.Drawing.Point(0, 0)
        Me.grdprofit.Name = "grdprofit"
        Me.grdprofit.ReadOnly = True
        Me.grdprofit.RowHeadersVisible = False
        Me.grdprofit.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grdprofit.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdprofit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdprofit.Size = New System.Drawing.Size(797, 514)
        Me.grdprofit.TabIndex = 4
        Me.grdprofit.Tag = "PROFIT"
        '
        'tbchdelta
        '
        Me.tbchdelta.AutoScroll = True
        Me.tbchdelta.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbchdelta.Controls.Add(Me.chartDelta)
        Me.tbchdelta.Controls.Add(Me.lbldelta)
        Me.tbchdelta.Location = New System.Drawing.Point(4, 22)
        Me.tbchdelta.Name = "tbchdelta"
        Me.tbchdelta.Size = New System.Drawing.Size(797, 514)
        Me.tbchdelta.TabIndex = 6
        Me.tbchdelta.Text = "Delta Chart"
        Me.tbchdelta.UseVisualStyleBackColor = True
        '
        'chartDelta
        '
        Me.chartDelta.DisplayScale = 0!
        Me.chartDelta.Location = New System.Drawing.Point(53, 146)
        Me.chartDelta.Name = "chartDelta"
        Me.chartDelta.Size = New System.Drawing.Size(691, 222)
        Me.chartDelta.TabIndex = 5
        '
        'lbldelta
        '
        Me.lbldelta.AutoSize = True
        Me.lbldelta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldelta.ForeColor = System.Drawing.Color.LightCoral
        Me.lbldelta.Location = New System.Drawing.Point(703, 2)
        Me.lbldelta.Name = "lbldelta"
        Me.lbldelta.Size = New System.Drawing.Size(11, 13)
        Me.lbldelta.TabIndex = 2
        Me.lbldelta.Text = "-"
        '
        'tbgrdDelta
        '
        Me.tbgrdDelta.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbgrdDelta.Controls.Add(Me.grddelta)
        Me.tbgrdDelta.Location = New System.Drawing.Point(4, 22)
        Me.tbgrdDelta.Name = "tbgrdDelta"
        Me.tbgrdDelta.Size = New System.Drawing.Size(797, 514)
        Me.tbgrdDelta.TabIndex = 13
        Me.tbgrdDelta.Text = "Delta Value"
        Me.tbgrdDelta.UseVisualStyleBackColor = True
        '
        'grddelta
        '
        Me.grddelta.AllowUserToAddRows = False
        Me.grddelta.AllowUserToDeleteRows = False
        Me.grddelta.AllowUserToResizeColumns = False
        Me.grddelta.AllowUserToResizeRows = False
        Me.grddelta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.grddelta.BackgroundColor = System.Drawing.SystemColors.WindowText
        Me.grddelta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grddelta.DefaultCellStyle = DataGridViewCellStyle2
        Me.grddelta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grddelta.Location = New System.Drawing.Point(0, 0)
        Me.grddelta.Name = "grddelta"
        Me.grddelta.ReadOnly = True
        Me.grddelta.RowHeadersVisible = False
        Me.grddelta.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grddelta.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grddelta.Size = New System.Drawing.Size(797, 514)
        Me.grddelta.TabIndex = 7
        Me.grddelta.Tag = "DELTA"
        '
        'tbchgamma
        '
        Me.tbchgamma.AutoScroll = True
        Me.tbchgamma.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbchgamma.Controls.Add(Me.chartGamma)
        Me.tbchgamma.Controls.Add(Me.lblgamma)
        Me.tbchgamma.Location = New System.Drawing.Point(4, 22)
        Me.tbchgamma.Name = "tbchgamma"
        Me.tbchgamma.Size = New System.Drawing.Size(797, 514)
        Me.tbchgamma.TabIndex = 7
        Me.tbchgamma.Text = "Gamma Chart "
        Me.tbchgamma.UseVisualStyleBackColor = True
        '
        'chartGamma
        '
        Me.chartGamma.DisplayScale = 0!
        Me.chartGamma.Location = New System.Drawing.Point(53, 146)
        Me.chartGamma.Name = "chartGamma"
        Me.chartGamma.Size = New System.Drawing.Size(691, 222)
        Me.chartGamma.TabIndex = 6
        '
        'lblgamma
        '
        Me.lblgamma.AutoSize = True
        Me.lblgamma.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgamma.ForeColor = System.Drawing.Color.LightCoral
        Me.lblgamma.Location = New System.Drawing.Point(703, 2)
        Me.lblgamma.Name = "lblgamma"
        Me.lblgamma.Size = New System.Drawing.Size(11, 13)
        Me.lblgamma.TabIndex = 3
        Me.lblgamma.Text = "-"
        '
        'tbgrdGamma
        '
        Me.tbgrdGamma.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbgrdGamma.Controls.Add(Me.grdgamma)
        Me.tbgrdGamma.Location = New System.Drawing.Point(4, 22)
        Me.tbgrdGamma.Name = "tbgrdGamma"
        Me.tbgrdGamma.Size = New System.Drawing.Size(797, 514)
        Me.tbgrdGamma.TabIndex = 14
        Me.tbgrdGamma.Text = "Gamma Value"
        Me.tbgrdGamma.UseVisualStyleBackColor = True
        '
        'grdgamma
        '
        Me.grdgamma.AllowUserToAddRows = False
        Me.grdgamma.AllowUserToDeleteRows = False
        Me.grdgamma.AllowUserToResizeColumns = False
        Me.grdgamma.AllowUserToResizeRows = False
        Me.grdgamma.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.grdgamma.BackgroundColor = System.Drawing.SystemColors.WindowText
        Me.grdgamma.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdgamma.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdgamma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdgamma.Location = New System.Drawing.Point(0, 0)
        Me.grdgamma.Name = "grdgamma"
        Me.grdgamma.ReadOnly = True
        Me.grdgamma.RowHeadersVisible = False
        Me.grdgamma.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grdgamma.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdgamma.Size = New System.Drawing.Size(797, 514)
        Me.grdgamma.TabIndex = 6
        Me.grdgamma.Tag = "GAMMA"
        '
        'tbchvega
        '
        Me.tbchvega.AutoScroll = True
        Me.tbchvega.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbchvega.Controls.Add(Me.chartVega)
        Me.tbchvega.Controls.Add(Me.lblvega)
        Me.tbchvega.Location = New System.Drawing.Point(4, 22)
        Me.tbchvega.Name = "tbchvega"
        Me.tbchvega.Size = New System.Drawing.Size(797, 514)
        Me.tbchvega.TabIndex = 8
        Me.tbchvega.Text = "Vega Chart"
        Me.tbchvega.UseVisualStyleBackColor = True
        '
        'chartVega
        '
        Me.chartVega.DisplayScale = 0!
        Me.chartVega.Location = New System.Drawing.Point(53, 146)
        Me.chartVega.Name = "chartVega"
        Me.chartVega.Size = New System.Drawing.Size(691, 222)
        Me.chartVega.TabIndex = 7
        '
        'lblvega
        '
        Me.lblvega.AutoSize = True
        Me.lblvega.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvega.ForeColor = System.Drawing.Color.LightCoral
        Me.lblvega.Location = New System.Drawing.Point(703, 2)
        Me.lblvega.Name = "lblvega"
        Me.lblvega.Size = New System.Drawing.Size(11, 13)
        Me.lblvega.TabIndex = 4
        Me.lblvega.Text = "-"
        '
        'tbgrdVega
        '
        Me.tbgrdVega.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbgrdVega.Controls.Add(Me.grdvega)
        Me.tbgrdVega.Location = New System.Drawing.Point(4, 22)
        Me.tbgrdVega.Name = "tbgrdVega"
        Me.tbgrdVega.Size = New System.Drawing.Size(797, 514)
        Me.tbgrdVega.TabIndex = 15
        Me.tbgrdVega.Text = "Vega Value"
        Me.tbgrdVega.UseVisualStyleBackColor = True
        '
        'grdvega
        '
        Me.grdvega.AllowUserToAddRows = False
        Me.grdvega.AllowUserToDeleteRows = False
        Me.grdvega.AllowUserToResizeColumns = False
        Me.grdvega.AllowUserToResizeRows = False
        Me.grdvega.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.grdvega.BackgroundColor = System.Drawing.SystemColors.WindowText
        Me.grdvega.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdvega.DefaultCellStyle = DataGridViewCellStyle4
        Me.grdvega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdvega.Location = New System.Drawing.Point(0, 0)
        Me.grdvega.Name = "grdvega"
        Me.grdvega.ReadOnly = True
        Me.grdvega.RowHeadersVisible = False
        Me.grdvega.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grdvega.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdvega.Size = New System.Drawing.Size(797, 514)
        Me.grdvega.TabIndex = 7
        Me.grdvega.Tag = "VEGA"
        '
        'tbchtheta
        '
        Me.tbchtheta.AutoScroll = True
        Me.tbchtheta.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbchtheta.Controls.Add(Me.chartTheta)
        Me.tbchtheta.Controls.Add(Me.lbltheta)
        Me.tbchtheta.Location = New System.Drawing.Point(4, 22)
        Me.tbchtheta.Name = "tbchtheta"
        Me.tbchtheta.Size = New System.Drawing.Size(797, 514)
        Me.tbchtheta.TabIndex = 9
        Me.tbchtheta.Text = "Theta Chart"
        Me.tbchtheta.UseVisualStyleBackColor = True
        '
        'chartTheta
        '
        Me.chartTheta.DisplayScale = 0!
        Me.chartTheta.Location = New System.Drawing.Point(53, 146)
        Me.chartTheta.Name = "chartTheta"
        Me.chartTheta.Size = New System.Drawing.Size(691, 222)
        Me.chartTheta.TabIndex = 8
        '
        'lbltheta
        '
        Me.lbltheta.AutoSize = True
        Me.lbltheta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltheta.ForeColor = System.Drawing.Color.LightCoral
        Me.lbltheta.Location = New System.Drawing.Point(703, 2)
        Me.lbltheta.Name = "lbltheta"
        Me.lbltheta.Size = New System.Drawing.Size(11, 13)
        Me.lbltheta.TabIndex = 4
        Me.lbltheta.Text = "-"
        '
        'tbgrdTheta
        '
        Me.tbgrdTheta.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbgrdTheta.Controls.Add(Me.grdtheta)
        Me.tbgrdTheta.Location = New System.Drawing.Point(4, 22)
        Me.tbgrdTheta.Name = "tbgrdTheta"
        Me.tbgrdTheta.Size = New System.Drawing.Size(797, 514)
        Me.tbgrdTheta.TabIndex = 16
        Me.tbgrdTheta.Text = "Theta Value"
        Me.tbgrdTheta.UseVisualStyleBackColor = True
        '
        'grdtheta
        '
        Me.grdtheta.AllowUserToAddRows = False
        Me.grdtheta.AllowUserToDeleteRows = False
        Me.grdtheta.AllowUserToResizeColumns = False
        Me.grdtheta.AllowUserToResizeRows = False
        Me.grdtheta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.grdtheta.BackgroundColor = System.Drawing.SystemColors.WindowText
        Me.grdtheta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdtheta.DefaultCellStyle = DataGridViewCellStyle5
        Me.grdtheta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdtheta.Location = New System.Drawing.Point(0, 0)
        Me.grdtheta.Name = "grdtheta"
        Me.grdtheta.ReadOnly = True
        Me.grdtheta.RowHeadersVisible = False
        Me.grdtheta.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grdtheta.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdtheta.Size = New System.Drawing.Size(797, 514)
        Me.grdtheta.TabIndex = 7
        Me.grdtheta.Tag = "THETA"
        '
        'tbchvolga
        '
        Me.tbchvolga.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbchvolga.Controls.Add(Me.chartVolga)
        Me.tbchvolga.Controls.Add(Me.lblvolga)
        Me.tbchvolga.Location = New System.Drawing.Point(4, 22)
        Me.tbchvolga.Name = "tbchvolga"
        Me.tbchvolga.Size = New System.Drawing.Size(797, 514)
        Me.tbchvolga.TabIndex = 10
        Me.tbchvolga.Text = "Volga Chart"
        Me.tbchvolga.UseVisualStyleBackColor = True
        '
        'chartVolga
        '
        Me.chartVolga.DisplayScale = 0!
        Me.chartVolga.Location = New System.Drawing.Point(53, 146)
        Me.chartVolga.Name = "chartVolga"
        Me.chartVolga.Size = New System.Drawing.Size(691, 222)
        Me.chartVolga.TabIndex = 9
        '
        'lblvolga
        '
        Me.lblvolga.AutoSize = True
        Me.lblvolga.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvolga.ForeColor = System.Drawing.Color.LightCoral
        Me.lblvolga.Location = New System.Drawing.Point(703, 2)
        Me.lblvolga.Name = "lblvolga"
        Me.lblvolga.Size = New System.Drawing.Size(11, 13)
        Me.lblvolga.TabIndex = 5
        Me.lblvolga.Text = "-"
        '
        'tbgrdVolga
        '
        Me.tbgrdVolga.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbgrdVolga.Controls.Add(Me.grdvolga)
        Me.tbgrdVolga.Location = New System.Drawing.Point(4, 22)
        Me.tbgrdVolga.Name = "tbgrdVolga"
        Me.tbgrdVolga.Size = New System.Drawing.Size(797, 514)
        Me.tbgrdVolga.TabIndex = 17
        Me.tbgrdVolga.Text = "Volga Value"
        Me.tbgrdVolga.UseVisualStyleBackColor = True
        '
        'grdvolga
        '
        Me.grdvolga.AllowUserToAddRows = False
        Me.grdvolga.AllowUserToDeleteRows = False
        Me.grdvolga.AllowUserToResizeColumns = False
        Me.grdvolga.AllowUserToResizeRows = False
        Me.grdvolga.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.grdvolga.BackgroundColor = System.Drawing.SystemColors.WindowText
        Me.grdvolga.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdvolga.DefaultCellStyle = DataGridViewCellStyle6
        Me.grdvolga.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdvolga.Location = New System.Drawing.Point(0, 0)
        Me.grdvolga.Name = "grdvolga"
        Me.grdvolga.ReadOnly = True
        Me.grdvolga.RowHeadersVisible = False
        Me.grdvolga.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grdvolga.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdvolga.Size = New System.Drawing.Size(797, 514)
        Me.grdvolga.TabIndex = 8
        Me.grdvolga.Tag = "VOLGA"
        '
        'tbchvanna
        '
        Me.tbchvanna.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbchvanna.Controls.Add(Me.chartVanna)
        Me.tbchvanna.Controls.Add(Me.lblvanna)
        Me.tbchvanna.Location = New System.Drawing.Point(4, 22)
        Me.tbchvanna.Name = "tbchvanna"
        Me.tbchvanna.Size = New System.Drawing.Size(797, 514)
        Me.tbchvanna.TabIndex = 11
        Me.tbchvanna.Text = "Vanna Chart"
        Me.tbchvanna.UseVisualStyleBackColor = True
        '
        'chartVanna
        '
        Me.chartVanna.DisplayScale = 0!
        Me.chartVanna.Location = New System.Drawing.Point(53, 146)
        Me.chartVanna.Name = "chartVanna"
        Me.chartVanna.Size = New System.Drawing.Size(691, 222)
        Me.chartVanna.TabIndex = 10
        '
        'lblvanna
        '
        Me.lblvanna.AutoSize = True
        Me.lblvanna.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvanna.ForeColor = System.Drawing.Color.LightCoral
        Me.lblvanna.Location = New System.Drawing.Point(703, 2)
        Me.lblvanna.Name = "lblvanna"
        Me.lblvanna.Size = New System.Drawing.Size(11, 13)
        Me.lblvanna.TabIndex = 5
        Me.lblvanna.Text = "-"
        '
        'tbgrdVanna
        '
        Me.tbgrdVanna.BackColor = System.Drawing.SystemColors.WindowText
        Me.tbgrdVanna.Controls.Add(Me.grdvanna)
        Me.tbgrdVanna.Location = New System.Drawing.Point(4, 22)
        Me.tbgrdVanna.Name = "tbgrdVanna"
        Me.tbgrdVanna.Size = New System.Drawing.Size(797, 514)
        Me.tbgrdVanna.TabIndex = 18
        Me.tbgrdVanna.Text = "Vanna Value"
        Me.tbgrdVanna.UseVisualStyleBackColor = True
        '
        'grdvanna
        '
        Me.grdvanna.AllowUserToAddRows = False
        Me.grdvanna.AllowUserToDeleteRows = False
        Me.grdvanna.AllowUserToResizeColumns = False
        Me.grdvanna.AllowUserToResizeRows = False
        Me.grdvanna.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.grdvanna.BackgroundColor = System.Drawing.SystemColors.WindowText
        Me.grdvanna.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdvanna.DefaultCellStyle = DataGridViewCellStyle7
        Me.grdvanna.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdvanna.Location = New System.Drawing.Point(0, 0)
        Me.grdvanna.Name = "grdvanna"
        Me.grdvanna.ReadOnly = True
        Me.grdvanna.RowHeadersVisible = False
        Me.grdvanna.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grdvanna.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdvanna.Size = New System.Drawing.Size(797, 514)
        Me.grdvanna.TabIndex = 9
        Me.grdvanna.Tag = "VANNA"
        '
        'frmprofitLossChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(805, 540)
        Me.ControlBox = False
        Me.Controls.Add(Me.tbcon)
        Me.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmprofitLossChart"
        Me.Text = "Scenario Charts"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tbcon.ResumeLayout(False)
        Me.tbprofitchart.ResumeLayout(False)
        Me.tbprofitchart.PerformLayout()
        Me.tbgrdProfit.ResumeLayout(False)
        CType(Me.grdprofit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbchdelta.ResumeLayout(False)
        Me.tbchdelta.PerformLayout()
        Me.tbgrdDelta.ResumeLayout(False)
        CType(Me.grddelta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbchgamma.ResumeLayout(False)
        Me.tbchgamma.PerformLayout()
        Me.tbgrdGamma.ResumeLayout(False)
        CType(Me.grdgamma, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbchvega.ResumeLayout(False)
        Me.tbchvega.PerformLayout()
        Me.tbgrdVega.ResumeLayout(False)
        CType(Me.grdvega, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbchtheta.ResumeLayout(False)
        Me.tbchtheta.PerformLayout()
        Me.tbgrdTheta.ResumeLayout(False)
        CType(Me.grdtheta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbchvolga.ResumeLayout(False)
        Me.tbchvolga.PerformLayout()
        Me.tbgrdVolga.ResumeLayout(False)
        CType(Me.grdvolga, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbchvanna.ResumeLayout(False)
        Me.tbchvanna.PerformLayout()
        Me.tbgrdVanna.ResumeLayout(False)
        CType(Me.grdvanna, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbcon As System.Windows.Forms.TabControl
    Friend WithEvents tbprofitchart As System.Windows.Forms.TabPage
    Friend WithEvents lblprofit As System.Windows.Forms.Label
    Friend WithEvents tbchdelta As System.Windows.Forms.TabPage
    Friend WithEvents lbldelta As System.Windows.Forms.Label
    Friend WithEvents tbchgamma As System.Windows.Forms.TabPage
    Friend WithEvents lblgamma As System.Windows.Forms.Label
    Friend WithEvents tbchvega As System.Windows.Forms.TabPage
    Friend WithEvents lblvega As System.Windows.Forms.Label
    Friend WithEvents tbchtheta As System.Windows.Forms.TabPage
    Friend WithEvents lbltheta As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbchvolga As System.Windows.Forms.TabPage
    Friend WithEvents tbchvanna As System.Windows.Forms.TabPage
    Friend WithEvents lblvolga As System.Windows.Forms.Label
    Friend WithEvents lblvanna As System.Windows.Forms.Label
    Friend WithEvents tbgrdProfit As System.Windows.Forms.TabPage
    Friend WithEvents tbgrdDelta As System.Windows.Forms.TabPage
    Friend WithEvents tbgrdGamma As System.Windows.Forms.TabPage
    Friend WithEvents tbgrdVega As System.Windows.Forms.TabPage
    Friend WithEvents tbgrdTheta As System.Windows.Forms.TabPage
    Friend WithEvents tbgrdVolga As System.Windows.Forms.TabPage
    Friend WithEvents tbgrdVanna As System.Windows.Forms.TabPage
    Friend WithEvents grdprofit As System.Windows.Forms.DataGridView
    Friend WithEvents grddelta As System.Windows.Forms.DataGridView
    Friend WithEvents grdgamma As System.Windows.Forms.DataGridView
    Friend WithEvents grdvega As System.Windows.Forms.DataGridView
    Friend WithEvents grdtheta As System.Windows.Forms.DataGridView
    Friend WithEvents grdvolga As System.Windows.Forms.DataGridView
    Friend WithEvents grdvanna As System.Windows.Forms.DataGridView
    Friend WithEvents chartPNL As ScottPlot.WinForms.FormsPlot
    Friend WithEvents chartDelta As ScottPlot.WinForms.FormsPlot
    Friend WithEvents chartGamma As ScottPlot.WinForms.FormsPlot
    Friend WithEvents chartVega As ScottPlot.WinForms.FormsPlot
    Friend WithEvents chartTheta As ScottPlot.WinForms.FormsPlot
    Friend WithEvents chartVolga As ScottPlot.WinForms.FormsPlot
    Friend WithEvents chartVanna As ScottPlot.WinForms.FormsPlot
End Class
