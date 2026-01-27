<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMarketwatch
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.flowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.RBcalVol = New System.Windows.Forms.RadioButton()
        Me.RBsynfut = New System.Windows.Forms.RadioButton()
        Me.chkthird = New System.Windows.Forms.CheckBox()
        Me.chksecond = New System.Windows.Forms.CheckBox()
        Me.chkfirst = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNoStrike = New System.Windows.Forms.TextBox()
        Me.chkIsWeekly = New System.Windows.Forms.CheckBox()
        Me.chkSkip = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RBAnalysis = New System.Windows.Forms.RadioButton()
        Me.RBScenario = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtnoofday = New System.Windows.Forms.TextBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnColumnSetting = New System.Windows.Forms.Button()
        Me.lblFuture1 = New System.Windows.Forms.Label()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.txtSymbol = New System.Windows.Forms.TextBox()
        Me.lblBusinessDate = New System.Windows.Forms.Label()
        Me.GBPROFILESAVE = New System.Windows.Forms.GroupBox()
        Me.Profilesavecancel = New System.Windows.Forms.Button()
        Me.txtProfileName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnProfileSave = New System.Windows.Forms.Button()
        Me.GBLOADPROFILE = New System.Windows.Forms.GroupBox()
        Me.profileloadcancel = New System.Windows.Forms.Button()
        Me.DeleteProfile = New System.Windows.Forms.Button()
        Me.cmbprofilename = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnProfileLoad = New System.Windows.Forms.Button()
        Me.collapsExpandPanel1 = New ScrollablePanel.CollapsExpandPanel()
        Me.dgv_Exp1 = New System.Windows.Forms.DataGridView()
        Me.col1Strike1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallOIChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallOI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallBF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallBF2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallStraddle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallRatio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallVolume = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallDelta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallGamma = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallVega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallTheta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallVolChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallVol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1Strike2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutVol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutVolChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutDelta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutGamma = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutVega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutTheta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutVolume = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutRatio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1Strike3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutBF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutBF2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutOI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutOIChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1TotalOI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutTotalOIPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallOIPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutOIPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PCPB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PCP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PCPA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1C2C = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1P2P = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1C2P = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallCalender = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutCalender = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallBullSpread = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutBearSpread = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1Maturity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1Symbol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1futltp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallPreToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallPre2Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallNextToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallNext2Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutPreToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutPre2Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutNextToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutNext2Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallVol1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutVol1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallTrend = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1CallStopLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutTrend = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col1PutStopLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.collapsExpandPanel2 = New ScrollablePanel.CollapsExpandPanel()
        Me.dgv_exp2 = New System.Windows.Forms.DataGridView()
        Me.col2Strike1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallOIChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallOI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallBF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallBF2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallStraddle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallRatio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallVolume = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallDelta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallGamma = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallVega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallTheta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallVolChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallVol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2Strike2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutVol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutVolChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutDelta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutGamma = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutVega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutTheta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutVolume = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutRatio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2Strike3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutBF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutBF2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutOI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutOIChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2TotalOI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutTotalOIPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallOIPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutOIPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PCPB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PCP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PCPA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2C2C = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2P2P = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2C2P = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallCalender = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutCalender = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallBullSpread = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutBearSpread = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2Maturity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2Symbol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2futltp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallPreToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallPre2Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallNextToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallNext2Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutPreToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutPre2Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutNextToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutNext2Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallVol1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PutVol1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col2CallTrend = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CallStopLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col2PutTrend = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col2PutStopLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.collapsExpandPanel3 = New ScrollablePanel.CollapsExpandPanel()
        Me.dgv_exp3 = New System.Windows.Forms.DataGridView()
        Me.col3Strike1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallOIChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallOI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallBF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallBF2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallStraddle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallRatio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallVolume = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallDelta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallGamma = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallVega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallTheta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallVolChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallVol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3Strike2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutVol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutVolChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutDelta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutGamma = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutVega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutTheta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutVolume = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutRatio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3Strike3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutBF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutBF2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutOI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutOIChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3TotalOI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutTotalOIPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallOIPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutOIPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PCPB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PCP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PCPA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3C2C = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3P2P = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3C2P = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallCalender = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutCalender = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallBullSpread = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutBearSpread = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3Maturity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3Symbol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3futltp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallPreToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallPre2Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallNextToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallNext2Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutPreToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutPre2Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutNextToken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutNext2Token = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3CallVol1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col3PutVol1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col3CallTrend = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col3CallStopLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col3PutTrend = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col3PutStopLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.flowLayoutPanel1.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBPROFILESAVE.SuspendLayout()
        Me.GBLOADPROFILE.SuspendLayout()
        Me.collapsExpandPanel1.SuspendLayout()
        CType(Me.dgv_Exp1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.collapsExpandPanel2.SuspendLayout()
        CType(Me.dgv_exp2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.collapsExpandPanel3.SuspendLayout()
        CType(Me.dgv_exp3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'flowLayoutPanel1
        '
        Me.flowLayoutPanel1.AutoScroll = True
        Me.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flowLayoutPanel1.BackColor = System.Drawing.Color.Black
        Me.flowLayoutPanel1.Controls.Add(Me.groupBox1)
        Me.flowLayoutPanel1.Controls.Add(Me.GBPROFILESAVE)
        Me.flowLayoutPanel1.Controls.Add(Me.GBLOADPROFILE)
        Me.flowLayoutPanel1.Controls.Add(Me.collapsExpandPanel1)
        Me.flowLayoutPanel1.Controls.Add(Me.collapsExpandPanel2)
        Me.flowLayoutPanel1.Controls.Add(Me.collapsExpandPanel3)
        Me.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.flowLayoutPanel1.Name = "flowLayoutPanel1"
        Me.flowLayoutPanel1.Size = New System.Drawing.Size(877, 643)
        Me.flowLayoutPanel1.TabIndex = 1
        Me.flowLayoutPanel1.TabStop = True
        Me.flowLayoutPanel1.WrapContents = False
        '
        'groupBox1
        '
        Me.groupBox1.BackColor = System.Drawing.Color.Black
        Me.groupBox1.Controls.Add(Me.RBcalVol)
        Me.groupBox1.Controls.Add(Me.RBsynfut)
        Me.groupBox1.Controls.Add(Me.chkthird)
        Me.groupBox1.Controls.Add(Me.chksecond)
        Me.groupBox1.Controls.Add(Me.chkfirst)
        Me.groupBox1.Controls.Add(Me.Label4)
        Me.groupBox1.Controls.Add(Me.txtNoStrike)
        Me.groupBox1.Controls.Add(Me.chkIsWeekly)
        Me.groupBox1.Controls.Add(Me.chkSkip)
        Me.groupBox1.Controls.Add(Me.GroupBox2)
        Me.groupBox1.Controls.Add(Me.Label3)
        Me.groupBox1.Controls.Add(Me.txtnoofday)
        Me.groupBox1.Controls.Add(Me.btnRefresh)
        Me.groupBox1.Controls.Add(Me.btnStart)
        Me.groupBox1.Controls.Add(Me.btnLoad)
        Me.groupBox1.Controls.Add(Me.btnColumnSetting)
        Me.groupBox1.Controls.Add(Me.lblFuture1)
        Me.groupBox1.Controls.Add(Me.cmbCompany)
        Me.groupBox1.Controls.Add(Me.txtSymbol)
        Me.groupBox1.Controls.Add(Me.lblBusinessDate)
        Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.groupBox1.Location = New System.Drawing.Point(3, 3)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(1326, 50)
        Me.groupBox1.TabIndex = 0
        Me.groupBox1.TabStop = False
        '
        'RBcalVol
        '
        Me.RBcalVol.AutoSize = True
        Me.RBcalVol.ForeColor = System.Drawing.SystemColors.Control
        Me.RBcalVol.Location = New System.Drawing.Point(446, 32)
        Me.RBcalVol.Name = "RBcalVol"
        Me.RBcalVol.Size = New System.Drawing.Size(106, 17)
        Me.RBcalVol.TabIndex = 173
        Me.RBcalVol.TabStop = True
        Me.RBcalVol.Text = "Cal Vol Using EQ"
        Me.RBcalVol.UseVisualStyleBackColor = True
        Me.RBcalVol.Visible = False
        '
        'RBsynfut
        '
        Me.RBsynfut.AutoSize = True
        Me.RBsynfut.ForeColor = System.Drawing.SystemColors.Control
        Me.RBsynfut.Location = New System.Drawing.Point(444, 9)
        Me.RBsynfut.Name = "RBsynfut"
        Me.RBsynfut.Size = New System.Drawing.Size(102, 17)
        Me.RBsynfut.TabIndex = 172
        Me.RBsynfut.TabStop = True
        Me.RBsynfut.Text = "Synthatic Future"
        Me.RBsynfut.UseVisualStyleBackColor = True
        Me.RBsynfut.Visible = False
        '
        'chkthird
        '
        Me.chkthird.AutoSize = True
        Me.chkthird.Checked = True
        Me.chkthird.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkthird.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkthird.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chkthird.Location = New System.Drawing.Point(1150, 21)
        Me.chkthird.Name = "chkthird"
        Me.chkthird.Size = New System.Drawing.Size(89, 17)
        Me.chkthird.TabIndex = 171
        Me.chkthird.Text = "3'rd  Expiry"
        Me.chkthird.UseVisualStyleBackColor = True
        '
        'chksecond
        '
        Me.chksecond.AutoSize = True
        Me.chksecond.Checked = True
        Me.chksecond.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chksecond.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chksecond.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chksecond.Location = New System.Drawing.Point(1055, 21)
        Me.chksecond.Name = "chksecond"
        Me.chksecond.Size = New System.Drawing.Size(88, 17)
        Me.chksecond.TabIndex = 170
        Me.chksecond.Text = "2'nd Expiry"
        Me.chksecond.UseVisualStyleBackColor = True
        '
        'chkfirst
        '
        Me.chkfirst.AutoSize = True
        Me.chkfirst.Checked = True
        Me.chkfirst.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkfirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkfirst.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chkfirst.Location = New System.Drawing.Point(966, 20)
        Me.chkfirst.Name = "chkfirst"
        Me.chkfirst.Size = New System.Drawing.Size(84, 17)
        Me.chkfirst.TabIndex = 169
        Me.chkfirst.Text = "1'st Expiry"
        Me.chkfirst.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(567, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 15)
        Me.Label4.TabIndex = 168
        Me.Label4.Text = "Up/Down Strike:"
        '
        'txtNoStrike
        '
        Me.txtNoStrike.Location = New System.Drawing.Point(667, 19)
        Me.txtNoStrike.Name = "txtNoStrike"
        Me.txtNoStrike.Size = New System.Drawing.Size(21, 20)
        Me.txtNoStrike.TabIndex = 167
        Me.txtNoStrike.Text = "7"
        '
        'chkIsWeekly
        '
        Me.chkIsWeekly.AutoSize = True
        Me.chkIsWeekly.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsWeekly.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chkIsWeekly.Location = New System.Drawing.Point(373, 21)
        Me.chkIsWeekly.Name = "chkIsWeekly"
        Me.chkIsWeekly.Size = New System.Drawing.Size(68, 17)
        Me.chkIsWeekly.TabIndex = 18
        Me.chkIsWeekly.Text = "Weekly"
        Me.chkIsWeekly.UseVisualStyleBackColor = True
        Me.chkIsWeekly.Visible = False
        '
        'chkSkip
        '
        Me.chkSkip.AutoSize = True
        Me.chkSkip.Checked = True
        Me.chkSkip.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSkip.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSkip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chkSkip.Location = New System.Drawing.Point(806, 21)
        Me.chkSkip.Name = "chkSkip"
        Me.chkSkip.Size = New System.Drawing.Size(51, 17)
        Me.chkSkip.TabIndex = 18
        Me.chkSkip.Text = "Skip"
        Me.chkSkip.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RBAnalysis)
        Me.GroupBox2.Controls.Add(Me.RBScenario)
        Me.GroupBox2.Location = New System.Drawing.Point(1244, 20)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(147, 35)
        Me.GroupBox2.TabIndex = 165
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GroupBox2"
        Me.GroupBox2.Visible = False
        '
        'RBAnalysis
        '
        Me.RBAnalysis.AutoSize = True
        Me.RBAnalysis.BackColor = System.Drawing.Color.DarkGray
        Me.RBAnalysis.ForeColor = System.Drawing.Color.Black
        Me.RBAnalysis.Location = New System.Drawing.Point(79, 15)
        Me.RBAnalysis.Name = "RBAnalysis"
        Me.RBAnalysis.Size = New System.Drawing.Size(63, 17)
        Me.RBAnalysis.TabIndex = 20
        Me.RBAnalysis.TabStop = True
        Me.RBAnalysis.Text = "Analysis"
        Me.RBAnalysis.UseVisualStyleBackColor = False
        '
        'RBScenario
        '
        Me.RBScenario.AutoSize = True
        Me.RBScenario.BackColor = System.Drawing.Color.DarkGray
        Me.RBScenario.ForeColor = System.Drawing.Color.Black
        Me.RBScenario.Location = New System.Drawing.Point(152, 15)
        Me.RBScenario.Name = "RBScenario"
        Me.RBScenario.Size = New System.Drawing.Size(67, 17)
        Me.RBScenario.TabIndex = 19
        Me.RBScenario.TabStop = True
        Me.RBScenario.Text = "Scenario"
        Me.RBScenario.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.LightCoral
        Me.Label3.Location = New System.Drawing.Point(858, 20)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(69, 15)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "No of Day"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtnoofday
        '
        Me.txtnoofday.BackColor = System.Drawing.SystemColors.WindowText
        Me.txtnoofday.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnoofday.ForeColor = System.Drawing.SystemColors.Window
        Me.txtnoofday.Location = New System.Drawing.Point(932, 14)
        Me.txtnoofday.Name = "txtnoofday"
        Me.txtnoofday.Size = New System.Drawing.Size(27, 26)
        Me.txtnoofday.TabIndex = 16
        Me.txtnoofday.Text = "0"
        Me.txtnoofday.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRefresh.Location = New System.Drawing.Point(317, 14)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(53, 31)
        Me.btnRefresh.TabIndex = 7
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        Me.btnRefresh.Visible = False
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnStart.Location = New System.Drawing.Point(261, 13)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(52, 31)
        Me.btnStart.TabIndex = 6
        Me.btnStart.Text = "Stop"
        Me.btnStart.UseVisualStyleBackColor = False
        '
        'btnLoad
        '
        Me.btnLoad.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnLoad.Location = New System.Drawing.Point(214, 13)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(46, 31)
        Me.btnLoad.TabIndex = 1
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = False
        '
        'btnColumnSetting
        '
        Me.btnColumnSetting.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnColumnSetting.Location = New System.Drawing.Point(693, 16)
        Me.btnColumnSetting.Name = "btnColumnSetting"
        Me.btnColumnSetting.Size = New System.Drawing.Size(109, 24)
        Me.btnColumnSetting.TabIndex = 2
        Me.btnColumnSetting.Text = "Column Settings"
        Me.btnColumnSetting.UseVisualStyleBackColor = False
        '
        'lblFuture1
        '
        Me.lblFuture1.AutoSize = True
        Me.lblFuture1.Location = New System.Drawing.Point(531, 21)
        Me.lblFuture1.Name = "lblFuture1"
        Me.lblFuture1.Size = New System.Drawing.Size(0, 13)
        Me.lblFuture1.TabIndex = 3
        '
        'cmbCompany
        '
        Me.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompany.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Items.AddRange(New Object() {"NIFTY"})
        Me.cmbCompany.Location = New System.Drawing.Point(7, 16)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(199, 27)
        Me.cmbCompany.TabIndex = 0
        '
        'txtSymbol
        '
        Me.txtSymbol.BackColor = System.Drawing.Color.Yellow
        Me.txtSymbol.Location = New System.Drawing.Point(1224, 10)
        Me.txtSymbol.Name = "txtSymbol"
        Me.txtSymbol.Size = New System.Drawing.Size(100, 20)
        Me.txtSymbol.TabIndex = 5
        Me.txtSymbol.Text = "Nifty"
        Me.txtSymbol.Visible = False
        '
        'lblBusinessDate
        '
        Me.lblBusinessDate.AutoSize = True
        Me.lblBusinessDate.BackColor = System.Drawing.Color.Black
        Me.lblBusinessDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBusinessDate.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.lblBusinessDate.Location = New System.Drawing.Point(4, -1)
        Me.lblBusinessDate.Name = "lblBusinessDate"
        Me.lblBusinessDate.Size = New System.Drawing.Size(112, 20)
        Me.lblBusinessDate.TabIndex = 0
        Me.lblBusinessDate.Text = "Select Script"
        '
        'GBPROFILESAVE
        '
        Me.GBPROFILESAVE.Controls.Add(Me.Profilesavecancel)
        Me.GBPROFILESAVE.Controls.Add(Me.txtProfileName)
        Me.GBPROFILESAVE.Controls.Add(Me.Label2)
        Me.GBPROFILESAVE.Controls.Add(Me.BtnProfileSave)
        Me.GBPROFILESAVE.Location = New System.Drawing.Point(3, 59)
        Me.GBPROFILESAVE.Name = "GBPROFILESAVE"
        Me.GBPROFILESAVE.Size = New System.Drawing.Size(283, 63)
        Me.GBPROFILESAVE.TabIndex = 164
        Me.GBPROFILESAVE.TabStop = False
        Me.GBPROFILESAVE.Text = "GroupBox2"
        Me.GBPROFILESAVE.Visible = False
        '
        'Profilesavecancel
        '
        Me.Profilesavecancel.BackColor = System.Drawing.Color.DarkGray
        Me.Profilesavecancel.Location = New System.Drawing.Point(187, 35)
        Me.Profilesavecancel.Name = "Profilesavecancel"
        Me.Profilesavecancel.Size = New System.Drawing.Size(75, 23)
        Me.Profilesavecancel.TabIndex = 167
        Me.Profilesavecancel.Text = "Cancel"
        Me.Profilesavecancel.UseVisualStyleBackColor = False
        '
        'txtProfileName
        '
        Me.txtProfileName.Location = New System.Drawing.Point(141, 13)
        Me.txtProfileName.Name = "txtProfileName"
        Me.txtProfileName.Size = New System.Drawing.Size(100, 20)
        Me.txtProfileName.TabIndex = 163
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(5, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 13)
        Me.Label2.TabIndex = 162
        Me.Label2.Text = "Enter Profile Name"
        '
        'BtnProfileSave
        '
        Me.BtnProfileSave.BackColor = System.Drawing.Color.DarkGray
        Me.BtnProfileSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnProfileSave.Location = New System.Drawing.Point(108, 34)
        Me.BtnProfileSave.Name = "BtnProfileSave"
        Me.BtnProfileSave.Size = New System.Drawing.Size(75, 23)
        Me.BtnProfileSave.TabIndex = 161
        Me.BtnProfileSave.Text = "save"
        Me.BtnProfileSave.UseVisualStyleBackColor = False
        '
        'GBLOADPROFILE
        '
        Me.GBLOADPROFILE.Controls.Add(Me.profileloadcancel)
        Me.GBLOADPROFILE.Controls.Add(Me.DeleteProfile)
        Me.GBLOADPROFILE.Controls.Add(Me.cmbprofilename)
        Me.GBLOADPROFILE.Controls.Add(Me.Label1)
        Me.GBLOADPROFILE.Controls.Add(Me.BtnProfileLoad)
        Me.GBLOADPROFILE.Location = New System.Drawing.Point(3, 128)
        Me.GBLOADPROFILE.Name = "GBLOADPROFILE"
        Me.GBLOADPROFILE.Size = New System.Drawing.Size(283, 63)
        Me.GBLOADPROFILE.TabIndex = 19
        Me.GBLOADPROFILE.TabStop = False
        Me.GBLOADPROFILE.Text = "GroupBox2"
        Me.GBLOADPROFILE.Visible = False
        '
        'profileloadcancel
        '
        Me.profileloadcancel.BackColor = System.Drawing.Color.DarkGray
        Me.profileloadcancel.Location = New System.Drawing.Point(187, 36)
        Me.profileloadcancel.Name = "profileloadcancel"
        Me.profileloadcancel.Size = New System.Drawing.Size(75, 23)
        Me.profileloadcancel.TabIndex = 167
        Me.profileloadcancel.Text = "Cancel"
        Me.profileloadcancel.UseVisualStyleBackColor = False
        '
        'DeleteProfile
        '
        Me.DeleteProfile.BackColor = System.Drawing.Color.DarkGray
        Me.DeleteProfile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.DeleteProfile.Location = New System.Drawing.Point(99, 34)
        Me.DeleteProfile.Name = "DeleteProfile"
        Me.DeleteProfile.Size = New System.Drawing.Size(75, 23)
        Me.DeleteProfile.TabIndex = 164
        Me.DeleteProfile.Text = "Delete"
        Me.DeleteProfile.UseVisualStyleBackColor = False
        '
        'cmbprofilename
        '
        Me.cmbprofilename.FormattingEnabled = True
        Me.cmbprofilename.Location = New System.Drawing.Point(141, 13)
        Me.cmbprofilename.Name = "cmbprofilename"
        Me.cmbprofilename.Size = New System.Drawing.Size(121, 21)
        Me.cmbprofilename.TabIndex = 163
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(5, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 162
        Me.Label1.Text = "Select Profile Name"
        '
        'BtnProfileLoad
        '
        Me.BtnProfileLoad.BackColor = System.Drawing.Color.DarkGray
        Me.BtnProfileLoad.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnProfileLoad.Location = New System.Drawing.Point(9, 34)
        Me.BtnProfileLoad.Name = "BtnProfileLoad"
        Me.BtnProfileLoad.Size = New System.Drawing.Size(75, 23)
        Me.BtnProfileLoad.TabIndex = 161
        Me.BtnProfileLoad.Text = "OK"
        Me.BtnProfileLoad.UseVisualStyleBackColor = False
        '
        'collapsExpandPanel1
        '
        Me.collapsExpandPanel1.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.collapsExpandPanel1.Controls.Add(Me.dgv_Exp1)
        Me.collapsExpandPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowLayoutPanel1.SetFlowBreak(Me.collapsExpandPanel1, True)
        Me.collapsExpandPanel1.HeaderHeight = 20
        Me.collapsExpandPanel1.HeaderText = "First Expiry Date: "
        Me.collapsExpandPanel1.Location = New System.Drawing.Point(3, 197)
        Me.collapsExpandPanel1.Name = "collapsExpandPanel1"
        Me.collapsExpandPanel1.ScrollInterval = 10
        Me.collapsExpandPanel1.Size = New System.Drawing.Size(1326, 426)
        Me.collapsExpandPanel1.TabIndex = 150
        Me.collapsExpandPanel1.TabStop = False
        '
        'dgv_Exp1
        '
        Me.dgv_Exp1.AllowUserToAddRows = False
        Me.dgv_Exp1.AllowUserToDeleteRows = False
        Me.dgv_Exp1.AllowUserToOrderColumns = True
        Me.dgv_Exp1.AllowUserToResizeRows = False
        Me.dgv_Exp1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.dgv_Exp1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDark
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Exp1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_Exp1.ColumnHeadersHeight = 60
        Me.dgv_Exp1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col1Strike1, Me.col1CallOIChg, Me.col1CallOI, Me.col1CallBF, Me.col1CallBF2, Me.col1CallStraddle, Me.col1CallRatio, Me.col1CallVolume, Me.col1CallDelta, Me.col1CallGamma, Me.col1CallVega, Me.col1CallTheta, Me.col1CallChg, Me.col1CallVolChg, Me.col1CallVol, Me.col1CE, Me.col1Strike2, Me.col1PE, Me.col1PutVol, Me.col1PutVolChg, Me.col1PutChg, Me.col1PutDelta, Me.col1PutGamma, Me.col1PutVega, Me.col1PutTheta, Me.col1PutVolume, Me.col1PutRatio, Me.col1Strike3, Me.col1PutBF, Me.col1PutBF2, Me.col1CR, Me.col1PutOI, Me.col1PutOIChg, Me.col1TotalOI, Me.col1PutTotalOIPer, Me.col1CallOIPer, Me.col1PutOIPer, Me.col1PCPB, Me.col1PCP, Me.col1PCPA, Me.col1C2C, Me.col1P2P, Me.col1C2P, Me.col1CallCalender, Me.col1PutCalender, Me.col1CallBullSpread, Me.col1PutBearSpread, Me.col1CallToken, Me.col1PutToken, Me.col1Maturity, Me.col1Symbol, Me.col1futltp, Me.col1CallPreToken, Me.col1CallPre2Token, Me.col1CallNextToken, Me.col1CallNext2Token, Me.col1PutPreToken, Me.col1PutPre2Token, Me.col1PutNextToken, Me.col1PutNext2Token, Me.col1CallVol1, Me.col1PutVol1, Me.col1CallTrend, Me.col1CallStopLoss, Me.col1PutTrend, Me.col1PutStopLoss})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_Exp1.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_Exp1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_Exp1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_Exp1.EnableHeadersVisualStyles = False
        Me.dgv_Exp1.Location = New System.Drawing.Point(0, 0)
        Me.dgv_Exp1.Name = "dgv_Exp1"
        Me.dgv_Exp1.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_Exp1.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_Exp1.RowHeadersVisible = False
        Me.dgv_Exp1.RowHeadersWidth = 4
        Me.dgv_Exp1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgv_Exp1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_Exp1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgv_Exp1.Size = New System.Drawing.Size(1326, 426)
        Me.dgv_Exp1.TabIndex = 0
        Me.dgv_Exp1.TabStop = False
        '
        'col1Strike1
        '
        Me.col1Strike1.DataPropertyName = "Strike1"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.col1Strike1.DefaultCellStyle = DataGridViewCellStyle2
        Me.col1Strike1.HeaderText = "Strike"
        Me.col1Strike1.Name = "col1Strike1"
        Me.col1Strike1.ReadOnly = True
        Me.col1Strike1.Width = 65
        '
        'col1CallOIChg
        '
        Me.col1CallOIChg.DataPropertyName = "CallOIChg"
        Me.col1CallOIChg.HeaderText = "C.Chg OI"
        Me.col1CallOIChg.Name = "col1CallOIChg"
        Me.col1CallOIChg.ReadOnly = True
        Me.col1CallOIChg.Width = 68
        '
        'col1CallOI
        '
        Me.col1CallOI.DataPropertyName = "CallOI"
        Me.col1CallOI.HeaderText = "C.OI"
        Me.col1CallOI.Name = "col1CallOI"
        Me.col1CallOI.ReadOnly = True
        Me.col1CallOI.Width = 68
        '
        'col1CallBF
        '
        Me.col1CallBF.DataPropertyName = "CallBF"
        Me.col1CallBF.HeaderText = "C.BF"
        Me.col1CallBF.Name = "col1CallBF"
        Me.col1CallBF.ReadOnly = True
        Me.col1CallBF.Width = 58
        '
        'col1CallBF2
        '
        Me.col1CallBF2.DataPropertyName = "CallBF2"
        Me.col1CallBF2.HeaderText = "C.BF2"
        Me.col1CallBF2.Name = "col1CallBF2"
        Me.col1CallBF2.ReadOnly = True
        Me.col1CallBF2.Width = 58
        '
        'col1CallStraddle
        '
        Me.col1CallStraddle.DataPropertyName = "CallStraddle"
        Me.col1CallStraddle.HeaderText = "Straddle"
        Me.col1CallStraddle.Name = "col1CallStraddle"
        Me.col1CallStraddle.ReadOnly = True
        Me.col1CallStraddle.Width = 79
        '
        'col1CallRatio
        '
        Me.col1CallRatio.DataPropertyName = "CallRatio"
        Me.col1CallRatio.HeaderText = "C.Ratio"
        Me.col1CallRatio.Name = "col1CallRatio"
        Me.col1CallRatio.ReadOnly = True
        Me.col1CallRatio.Width = 62
        '
        'col1CallVolume
        '
        Me.col1CallVolume.DataPropertyName = "CallVolume"
        Me.col1CallVolume.HeaderText = "C.Volume"
        Me.col1CallVolume.Name = "col1CallVolume"
        Me.col1CallVolume.ReadOnly = True
        Me.col1CallVolume.Width = 73
        '
        'col1CallDelta
        '
        Me.col1CallDelta.DataPropertyName = "CallDelta"
        Me.col1CallDelta.HeaderText = "C.Delta"
        Me.col1CallDelta.Name = "col1CallDelta"
        Me.col1CallDelta.ReadOnly = True
        Me.col1CallDelta.Width = 62
        '
        'col1CallGamma
        '
        Me.col1CallGamma.DataPropertyName = "CallGamma"
        Me.col1CallGamma.HeaderText = "C.Gamma"
        Me.col1CallGamma.Name = "col1CallGamma"
        Me.col1CallGamma.ReadOnly = True
        Me.col1CallGamma.Width = 73
        '
        'col1CallVega
        '
        Me.col1CallVega.DataPropertyName = "CallVega"
        Me.col1CallVega.HeaderText = "C.Vega"
        Me.col1CallVega.Name = "col1CallVega"
        Me.col1CallVega.ReadOnly = True
        Me.col1CallVega.Width = 61
        '
        'col1CallTheta
        '
        Me.col1CallTheta.DataPropertyName = "CallTheta"
        Me.col1CallTheta.HeaderText = "C.Theta"
        Me.col1CallTheta.Name = "col1CallTheta"
        Me.col1CallTheta.ReadOnly = True
        Me.col1CallTheta.Width = 65
        '
        'col1CallChg
        '
        Me.col1CallChg.DataPropertyName = "CallChg"
        Me.col1CallChg.HeaderText = "C.Chg (Rs.)"
        Me.col1CallChg.Name = "col1CallChg"
        Me.col1CallChg.ReadOnly = True
        Me.col1CallChg.Width = 81
        '
        'col1CallVolChg
        '
        Me.col1CallVolChg.DataPropertyName = "CallVolChg"
        Me.col1CallVolChg.HeaderText = "Call Chg (%)"
        Me.col1CallVolChg.Name = "col1CallVolChg"
        Me.col1CallVolChg.ReadOnly = True
        Me.col1CallVolChg.Width = 62
        '
        'col1CallVol
        '
        Me.col1CallVol.DataPropertyName = "CallVol"
        Me.col1CallVol.HeaderText = "Call Vol"
        Me.col1CallVol.Name = "col1CallVol"
        Me.col1CallVol.ReadOnly = True
        Me.col1CallVol.Width = 53
        '
        'col1CE
        '
        Me.col1CE.DataPropertyName = "CE"
        Me.col1CE.HeaderText = "CE"
        Me.col1CE.Name = "col1CE"
        Me.col1CE.ReadOnly = True
        Me.col1CE.Width = 48
        '
        'col1Strike2
        '
        Me.col1Strike2.DataPropertyName = "Strike2"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.col1Strike2.DefaultCellStyle = DataGridViewCellStyle3
        Me.col1Strike2.HeaderText = "Strike"
        Me.col1Strike2.Name = "col1Strike2"
        Me.col1Strike2.ReadOnly = True
        Me.col1Strike2.Width = 65
        '
        'col1PE
        '
        Me.col1PE.DataPropertyName = "PE"
        Me.col1PE.HeaderText = "PE"
        Me.col1PE.Name = "col1PE"
        Me.col1PE.ReadOnly = True
        Me.col1PE.Width = 48
        '
        'col1PutVol
        '
        Me.col1PutVol.DataPropertyName = "PutVol"
        Me.col1PutVol.HeaderText = "Put Vol"
        Me.col1PutVol.Name = "col1PutVol"
        Me.col1PutVol.ReadOnly = True
        Me.col1PutVol.Width = 52
        '
        'col1PutVolChg
        '
        Me.col1PutVolChg.DataPropertyName = "PutVolChg"
        Me.col1PutVolChg.HeaderText = "Put Chg (%)"
        Me.col1PutVolChg.Name = "col1PutVolChg"
        Me.col1PutVolChg.ReadOnly = True
        Me.col1PutVolChg.Width = 54
        '
        'col1PutChg
        '
        Me.col1PutChg.DataPropertyName = "PutChg"
        Me.col1PutChg.HeaderText = "Chg (Rs.)"
        Me.col1PutChg.Name = "col1PutChg"
        Me.col1PutChg.ReadOnly = True
        Me.col1PutChg.Width = 81
        '
        'col1PutDelta
        '
        Me.col1PutDelta.DataPropertyName = "PutDelta"
        Me.col1PutDelta.HeaderText = "P.Delta"
        Me.col1PutDelta.Name = "col1PutDelta"
        Me.col1PutDelta.ReadOnly = True
        Me.col1PutDelta.Width = 62
        '
        'col1PutGamma
        '
        Me.col1PutGamma.DataPropertyName = "PutGamma"
        Me.col1PutGamma.HeaderText = "P.Gamma"
        Me.col1PutGamma.Name = "col1PutGamma"
        Me.col1PutGamma.ReadOnly = True
        '
        'col1PutVega
        '
        Me.col1PutVega.DataPropertyName = "PutVega"
        Me.col1PutVega.HeaderText = "P.Vega"
        Me.col1PutVega.Name = "col1PutVega"
        Me.col1PutVega.ReadOnly = True
        Me.col1PutVega.Width = 61
        '
        'col1PutTheta
        '
        Me.col1PutTheta.DataPropertyName = "PutTheta"
        Me.col1PutTheta.HeaderText = "P.Theta"
        Me.col1PutTheta.Name = "col1PutTheta"
        Me.col1PutTheta.ReadOnly = True
        Me.col1PutTheta.Width = 65
        '
        'col1PutVolume
        '
        Me.col1PutVolume.DataPropertyName = "PutVolume"
        Me.col1PutVolume.HeaderText = "P.Volume"
        Me.col1PutVolume.Name = "col1PutVolume"
        Me.col1PutVolume.ReadOnly = True
        Me.col1PutVolume.Width = 73
        '
        'col1PutRatio
        '
        Me.col1PutRatio.DataPropertyName = "PutRatio"
        Me.col1PutRatio.HeaderText = "P.Ratio"
        Me.col1PutRatio.Name = "col1PutRatio"
        Me.col1PutRatio.ReadOnly = True
        Me.col1PutRatio.Width = 62
        '
        'col1Strike3
        '
        Me.col1Strike3.DataPropertyName = "Strike3"
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.col1Strike3.DefaultCellStyle = DataGridViewCellStyle4
        Me.col1Strike3.HeaderText = "Strike"
        Me.col1Strike3.Name = "col1Strike3"
        Me.col1Strike3.ReadOnly = True
        Me.col1Strike3.Width = 65
        '
        'col1PutBF
        '
        Me.col1PutBF.DataPropertyName = "PutBF"
        Me.col1PutBF.HeaderText = "P.BF"
        Me.col1PutBF.Name = "col1PutBF"
        Me.col1PutBF.ReadOnly = True
        Me.col1PutBF.Width = 58
        '
        'col1PutBF2
        '
        Me.col1PutBF2.DataPropertyName = "PutBF2"
        Me.col1PutBF2.HeaderText = "P.BF2"
        Me.col1PutBF2.Name = "col1PutBF2"
        Me.col1PutBF2.ReadOnly = True
        Me.col1PutBF2.Width = 58
        '
        'col1CR
        '
        Me.col1CR.DataPropertyName = "CR"
        Me.col1CR.HeaderText = "CR"
        Me.col1CR.Name = "col1CR"
        Me.col1CR.ReadOnly = True
        '
        'col1PutOI
        '
        Me.col1PutOI.DataPropertyName = "PutOI"
        Me.col1PutOI.HeaderText = "P.OI"
        Me.col1PutOI.Name = "col1PutOI"
        Me.col1PutOI.ReadOnly = True
        Me.col1PutOI.Width = 68
        '
        'col1PutOIChg
        '
        Me.col1PutOIChg.DataPropertyName = "PutOIChg"
        Me.col1PutOIChg.HeaderText = "P.Chg OI"
        Me.col1PutOIChg.Name = "col1PutOIChg"
        Me.col1PutOIChg.ReadOnly = True
        Me.col1PutOIChg.Width = 68
        '
        'col1TotalOI
        '
        Me.col1TotalOI.DataPropertyName = "TotalOI"
        Me.col1TotalOI.HeaderText = "P.Total OI"
        Me.col1TotalOI.Name = "col1TotalOI"
        Me.col1TotalOI.ReadOnly = True
        Me.col1TotalOI.Width = 68
        '
        'col1PutTotalOIPer
        '
        Me.col1PutTotalOIPer.DataPropertyName = "PutTotalOIPer"
        Me.col1PutTotalOIPer.HeaderText = "P.Total OI (%)"
        Me.col1PutTotalOIPer.Name = "col1PutTotalOIPer"
        Me.col1PutTotalOIPer.ReadOnly = True
        Me.col1PutTotalOIPer.Width = 68
        '
        'col1CallOIPer
        '
        Me.col1CallOIPer.DataPropertyName = "CallOIPer"
        Me.col1CallOIPer.HeaderText = "Call OI (%)"
        Me.col1CallOIPer.Name = "col1CallOIPer"
        Me.col1CallOIPer.ReadOnly = True
        Me.col1CallOIPer.Width = 68
        '
        'col1PutOIPer
        '
        Me.col1PutOIPer.DataPropertyName = "PutOIPer"
        Me.col1PutOIPer.HeaderText = "Put OI (%)"
        Me.col1PutOIPer.Name = "col1PutOIPer"
        Me.col1PutOIPer.ReadOnly = True
        Me.col1PutOIPer.Width = 68
        '
        'col1PCPB
        '
        Me.col1PCPB.DataPropertyName = "PCPB"
        Me.col1PCPB.HeaderText = "PCP Bid"
        Me.col1PCPB.Name = "col1PCPB"
        Me.col1PCPB.ReadOnly = True
        '
        'col1PCP
        '
        Me.col1PCP.DataPropertyName = "PCP"
        Me.col1PCP.HeaderText = "PCP"
        Me.col1PCP.Name = "col1PCP"
        Me.col1PCP.ReadOnly = True
        Me.col1PCP.Width = 58
        '
        'col1PCPA
        '
        Me.col1PCPA.DataPropertyName = "PCPA"
        Me.col1PCPA.HeaderText = "PCP Ask"
        Me.col1PCPA.Name = "col1PCPA"
        Me.col1PCPA.ReadOnly = True
        '
        'col1C2C
        '
        Me.col1C2C.DataPropertyName = "C2C"
        Me.col1C2C.HeaderText = "C2C"
        Me.col1C2C.Name = "col1C2C"
        Me.col1C2C.ReadOnly = True
        Me.col1C2C.Width = 58
        '
        'col1P2P
        '
        Me.col1P2P.DataPropertyName = "P2P"
        Me.col1P2P.HeaderText = "P2P"
        Me.col1P2P.Name = "col1P2P"
        Me.col1P2P.ReadOnly = True
        Me.col1P2P.Width = 58
        '
        'col1C2P
        '
        Me.col1C2P.DataPropertyName = "C2P"
        Me.col1C2P.HeaderText = "C2P"
        Me.col1C2P.Name = "col1C2P"
        Me.col1C2P.ReadOnly = True
        Me.col1C2P.Width = 58
        '
        'col1CallCalender
        '
        Me.col1CallCalender.DataPropertyName = "CallCalender"
        Me.col1CallCalender.HeaderText = "Call Calender"
        Me.col1CallCalender.Name = "col1CallCalender"
        Me.col1CallCalender.ReadOnly = True
        Me.col1CallCalender.Width = 98
        '
        'col1PutCalender
        '
        Me.col1PutCalender.DataPropertyName = "PutCalender"
        Me.col1PutCalender.HeaderText = "Put Calender"
        Me.col1PutCalender.Name = "col1PutCalender"
        Me.col1PutCalender.ReadOnly = True
        Me.col1PutCalender.Width = 97
        '
        'col1CallBullSpread
        '
        Me.col1CallBullSpread.DataPropertyName = "CallBullSpread"
        Me.col1CallBullSpread.HeaderText = "Call BullSpread"
        Me.col1CallBullSpread.Name = "col1CallBullSpread"
        Me.col1CallBullSpread.ReadOnly = True
        Me.col1CallBullSpread.Width = 76
        '
        'col1PutBearSpread
        '
        Me.col1PutBearSpread.DataPropertyName = "PutBearSpread"
        Me.col1PutBearSpread.HeaderText = "Put BearSpread"
        Me.col1PutBearSpread.Name = "col1PutBearSpread"
        Me.col1PutBearSpread.ReadOnly = True
        Me.col1PutBearSpread.Width = 79
        '
        'col1CallToken
        '
        Me.col1CallToken.DataPropertyName = "CallToken"
        Me.col1CallToken.HeaderText = "Call Token"
        Me.col1CallToken.Name = "col1CallToken"
        Me.col1CallToken.ReadOnly = True
        Me.col1CallToken.Visible = False
        '
        'col1PutToken
        '
        Me.col1PutToken.DataPropertyName = "PutToken"
        Me.col1PutToken.HeaderText = "Put Token"
        Me.col1PutToken.Name = "col1PutToken"
        Me.col1PutToken.ReadOnly = True
        Me.col1PutToken.Visible = False
        '
        'col1Maturity
        '
        Me.col1Maturity.DataPropertyName = "Maturity"
        Me.col1Maturity.HeaderText = "Maturity"
        Me.col1Maturity.Name = "col1Maturity"
        Me.col1Maturity.ReadOnly = True
        Me.col1Maturity.Visible = False
        '
        'col1Symbol
        '
        Me.col1Symbol.DataPropertyName = "Symbol"
        Me.col1Symbol.HeaderText = "Symbol"
        Me.col1Symbol.Name = "col1Symbol"
        Me.col1Symbol.ReadOnly = True
        Me.col1Symbol.Visible = False
        '
        'col1futltp
        '
        Me.col1futltp.DataPropertyName = "futltp"
        Me.col1futltp.HeaderText = "futltp"
        Me.col1futltp.Name = "col1futltp"
        Me.col1futltp.ReadOnly = True
        Me.col1futltp.Visible = False
        '
        'col1CallPreToken
        '
        Me.col1CallPreToken.DataPropertyName = "CallPreToken"
        Me.col1CallPreToken.HeaderText = "CallPreToken"
        Me.col1CallPreToken.Name = "col1CallPreToken"
        Me.col1CallPreToken.ReadOnly = True
        Me.col1CallPreToken.Visible = False
        '
        'col1CallPre2Token
        '
        Me.col1CallPre2Token.DataPropertyName = "CallPre2Token"
        Me.col1CallPre2Token.HeaderText = "CallPre2Token"
        Me.col1CallPre2Token.Name = "col1CallPre2Token"
        Me.col1CallPre2Token.ReadOnly = True
        Me.col1CallPre2Token.Visible = False
        '
        'col1CallNextToken
        '
        Me.col1CallNextToken.DataPropertyName = "CallNextToken"
        Me.col1CallNextToken.HeaderText = "CallNextToken"
        Me.col1CallNextToken.Name = "col1CallNextToken"
        Me.col1CallNextToken.ReadOnly = True
        Me.col1CallNextToken.Visible = False
        '
        'col1CallNext2Token
        '
        Me.col1CallNext2Token.DataPropertyName = "CallNext2Token"
        Me.col1CallNext2Token.HeaderText = "CallNext2Token"
        Me.col1CallNext2Token.Name = "col1CallNext2Token"
        Me.col1CallNext2Token.ReadOnly = True
        Me.col1CallNext2Token.Visible = False
        '
        'col1PutPreToken
        '
        Me.col1PutPreToken.DataPropertyName = "PutPreToken"
        Me.col1PutPreToken.HeaderText = "PutPreToken"
        Me.col1PutPreToken.Name = "col1PutPreToken"
        Me.col1PutPreToken.ReadOnly = True
        Me.col1PutPreToken.Visible = False
        '
        'col1PutPre2Token
        '
        Me.col1PutPre2Token.DataPropertyName = "PutPre2Token"
        Me.col1PutPre2Token.HeaderText = "PutPre2Token"
        Me.col1PutPre2Token.Name = "col1PutPre2Token"
        Me.col1PutPre2Token.ReadOnly = True
        Me.col1PutPre2Token.Visible = False
        '
        'col1PutNextToken
        '
        Me.col1PutNextToken.DataPropertyName = "PutNextToken"
        Me.col1PutNextToken.HeaderText = "PutNextToken"
        Me.col1PutNextToken.Name = "col1PutNextToken"
        Me.col1PutNextToken.ReadOnly = True
        Me.col1PutNextToken.Visible = False
        '
        'col1PutNext2Token
        '
        Me.col1PutNext2Token.DataPropertyName = "PutNext2Token"
        Me.col1PutNext2Token.HeaderText = "PutNext2Token"
        Me.col1PutNext2Token.Name = "col1PutNext2Token"
        Me.col1PutNext2Token.ReadOnly = True
        Me.col1PutNext2Token.Visible = False
        '
        'col1CallVol1
        '
        Me.col1CallVol1.DataPropertyName = "CallVol1"
        Me.col1CallVol1.HeaderText = "CallVol1"
        Me.col1CallVol1.Name = "col1CallVol1"
        Me.col1CallVol1.ReadOnly = True
        '
        'col1PutVol1
        '
        Me.col1PutVol1.DataPropertyName = "PutVol1"
        Me.col1PutVol1.HeaderText = "PutVol1"
        Me.col1PutVol1.Name = "col1PutVol1"
        Me.col1PutVol1.ReadOnly = True
        '
        'col1CallTrend
        '
        Me.col1CallTrend.DataPropertyName = "CEStatus"
        Me.col1CallTrend.HeaderText = "CallTrend"
        Me.col1CallTrend.Name = "col1CallTrend"
        Me.col1CallTrend.ReadOnly = True
        Me.col1CallTrend.Visible = False
        '
        'col1CallStopLoss
        '
        Me.col1CallStopLoss.DataPropertyName = "CEStoploss"
        Me.col1CallStopLoss.HeaderText = "CallStopLoss"
        Me.col1CallStopLoss.Name = "col1CallStopLoss"
        Me.col1CallStopLoss.ReadOnly = True
        '
        'col1PutTrend
        '
        Me.col1PutTrend.DataPropertyName = "PEStatus"
        Me.col1PutTrend.HeaderText = "PutTrend"
        Me.col1PutTrend.Name = "col1PutTrend"
        Me.col1PutTrend.ReadOnly = True
        Me.col1PutTrend.Visible = False
        '
        'col1PutStopLoss
        '
        Me.col1PutStopLoss.DataPropertyName = "PEStoploss"
        Me.col1PutStopLoss.HeaderText = "PutStoploss"
        Me.col1PutStopLoss.Name = "col1PutStopLoss"
        Me.col1PutStopLoss.ReadOnly = True
        '
        'collapsExpandPanel2
        '
        Me.collapsExpandPanel2.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.collapsExpandPanel2.Controls.Add(Me.dgv_exp2)
        Me.collapsExpandPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowLayoutPanel1.SetFlowBreak(Me.collapsExpandPanel2, True)
        Me.collapsExpandPanel2.HeaderHeight = 20
        Me.collapsExpandPanel2.HeaderText = "Second Expiry Date: "
        Me.collapsExpandPanel2.Location = New System.Drawing.Point(3, 629)
        Me.collapsExpandPanel2.Name = "collapsExpandPanel2"
        Me.collapsExpandPanel2.ScrollInterval = 10
        Me.collapsExpandPanel2.Size = New System.Drawing.Size(1326, 415)
        Me.collapsExpandPanel2.TabIndex = 0
        Me.collapsExpandPanel2.TabStop = False
        '
        'dgv_exp2
        '
        Me.dgv_exp2.AllowUserToAddRows = False
        Me.dgv_exp2.AllowUserToDeleteRows = False
        Me.dgv_exp2.AllowUserToOrderColumns = True
        Me.dgv_exp2.AllowUserToResizeRows = False
        Me.dgv_exp2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.dgv_exp2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ControlDark
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_exp2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgv_exp2.ColumnHeadersHeight = 60
        Me.dgv_exp2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col2Strike1, Me.col2CallOIChg, Me.col2CallOI, Me.col2CallBF, Me.col2CallBF2, Me.col2CallStraddle, Me.col2CallRatio, Me.col2CallVolume, Me.col2CallDelta, Me.col2CallGamma, Me.col2CallVega, Me.col2CallTheta, Me.col2CallChg, Me.col2CallVolChg, Me.col2CallVol, Me.col2CE, Me.col2Strike2, Me.col2PE, Me.col2PutVol, Me.col2PutVolChg, Me.col2PutChg, Me.col2PutDelta, Me.col2PutGamma, Me.col2PutVega, Me.col2PutTheta, Me.col2PutVolume, Me.col2PutRatio, Me.col2Strike3, Me.col2PutBF, Me.col2PutBF2, Me.col2CR, Me.col2PutOI, Me.col2PutOIChg, Me.col2TotalOI, Me.col2PutTotalOIPer, Me.col2CallOIPer, Me.col2PutOIPer, Me.col2PCPB, Me.col2PCP, Me.col2PCPA, Me.col2C2C, Me.col2P2P, Me.col2C2P, Me.col2CallCalender, Me.col2PutCalender, Me.col2CallBullSpread, Me.col2PutBearSpread, Me.col2CallToken, Me.col2PutToken, Me.col2Maturity, Me.col2Symbol, Me.col2futltp, Me.col2CallPreToken, Me.col2CallPre2Token, Me.col2CallNextToken, Me.col2CallNext2Token, Me.col2PutPreToken, Me.col2PutPre2Token, Me.col2PutNextToken, Me.col2PutNext2Token, Me.col2CallVol1, Me.col2PutVol1, Me.Col2CallTrend, Me.col2CallStopLoss, Me.Col2PutTrend, Me.Col2PutStopLoss})
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_exp2.DefaultCellStyle = DataGridViewCellStyle11
        Me.dgv_exp2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_exp2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_exp2.EnableHeadersVisualStyles = False
        Me.dgv_exp2.Location = New System.Drawing.Point(0, 0)
        Me.dgv_exp2.Name = "dgv_exp2"
        Me.dgv_exp2.ReadOnly = True
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_exp2.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgv_exp2.RowHeadersVisible = False
        Me.dgv_exp2.RowHeadersWidth = 4
        Me.dgv_exp2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgv_exp2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgv_exp2.Size = New System.Drawing.Size(1326, 415)
        Me.dgv_exp2.TabIndex = 3
        Me.dgv_exp2.TabStop = False
        '
        'col2Strike1
        '
        Me.col2Strike1.DataPropertyName = "Strike1"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.col2Strike1.DefaultCellStyle = DataGridViewCellStyle8
        Me.col2Strike1.HeaderText = "Strike"
        Me.col2Strike1.Name = "col2Strike1"
        Me.col2Strike1.ReadOnly = True
        Me.col2Strike1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.col2Strike1.Width = 65
        '
        'col2CallOIChg
        '
        Me.col2CallOIChg.DataPropertyName = "CallOIChg"
        Me.col2CallOIChg.HeaderText = "C.Chg OI"
        Me.col2CallOIChg.Name = "col2CallOIChg"
        Me.col2CallOIChg.ReadOnly = True
        Me.col2CallOIChg.Width = 68
        '
        'col2CallOI
        '
        Me.col2CallOI.DataPropertyName = "CallOI"
        Me.col2CallOI.HeaderText = "C.OI"
        Me.col2CallOI.Name = "col2CallOI"
        Me.col2CallOI.ReadOnly = True
        Me.col2CallOI.Width = 68
        '
        'col2CallBF
        '
        Me.col2CallBF.DataPropertyName = "CallBF"
        Me.col2CallBF.HeaderText = "C.BF"
        Me.col2CallBF.Name = "col2CallBF"
        Me.col2CallBF.ReadOnly = True
        Me.col2CallBF.Width = 58
        '
        'col2CallBF2
        '
        Me.col2CallBF2.DataPropertyName = "CallBF2"
        Me.col2CallBF2.HeaderText = "C.BF2"
        Me.col2CallBF2.Name = "col2CallBF2"
        Me.col2CallBF2.ReadOnly = True
        Me.col2CallBF2.Width = 58
        '
        'col2CallStraddle
        '
        Me.col2CallStraddle.DataPropertyName = "CallStraddle"
        Me.col2CallStraddle.HeaderText = "Straddle"
        Me.col2CallStraddle.Name = "col2CallStraddle"
        Me.col2CallStraddle.ReadOnly = True
        Me.col2CallStraddle.Width = 79
        '
        'col2CallRatio
        '
        Me.col2CallRatio.DataPropertyName = "CallRatio"
        Me.col2CallRatio.HeaderText = "C.Ratio"
        Me.col2CallRatio.Name = "col2CallRatio"
        Me.col2CallRatio.ReadOnly = True
        Me.col2CallRatio.Width = 62
        '
        'col2CallVolume
        '
        Me.col2CallVolume.DataPropertyName = "CallVolume"
        Me.col2CallVolume.HeaderText = "C.Volume"
        Me.col2CallVolume.Name = "col2CallVolume"
        Me.col2CallVolume.ReadOnly = True
        Me.col2CallVolume.Width = 73
        '
        'col2CallDelta
        '
        Me.col2CallDelta.DataPropertyName = "CallDelta"
        Me.col2CallDelta.HeaderText = "C.Delta"
        Me.col2CallDelta.Name = "col2CallDelta"
        Me.col2CallDelta.ReadOnly = True
        Me.col2CallDelta.Width = 62
        '
        'col2CallGamma
        '
        Me.col2CallGamma.DataPropertyName = "CallGamma"
        Me.col2CallGamma.HeaderText = "C.Gamma"
        Me.col2CallGamma.Name = "col2CallGamma"
        Me.col2CallGamma.ReadOnly = True
        Me.col2CallGamma.Width = 73
        '
        'col2CallVega
        '
        Me.col2CallVega.DataPropertyName = "CallVega"
        Me.col2CallVega.HeaderText = "C.Vega"
        Me.col2CallVega.Name = "col2CallVega"
        Me.col2CallVega.ReadOnly = True
        Me.col2CallVega.Width = 61
        '
        'col2CallTheta
        '
        Me.col2CallTheta.DataPropertyName = "CallTheta"
        Me.col2CallTheta.HeaderText = "C.Theta"
        Me.col2CallTheta.Name = "col2CallTheta"
        Me.col2CallTheta.ReadOnly = True
        Me.col2CallTheta.Width = 65
        '
        'col2CallChg
        '
        Me.col2CallChg.DataPropertyName = "CallChg"
        Me.col2CallChg.HeaderText = "C.Chg (Rs.)"
        Me.col2CallChg.Name = "col2CallChg"
        Me.col2CallChg.ReadOnly = True
        Me.col2CallChg.Width = 81
        '
        'col2CallVolChg
        '
        Me.col2CallVolChg.DataPropertyName = "CallVolChg"
        Me.col2CallVolChg.HeaderText = "Call Chg (%)"
        Me.col2CallVolChg.Name = "col2CallVolChg"
        Me.col2CallVolChg.ReadOnly = True
        Me.col2CallVolChg.Width = 88
        '
        'col2CallVol
        '
        Me.col2CallVol.DataPropertyName = "CallVol"
        Me.col2CallVol.HeaderText = "Call Vol"
        Me.col2CallVol.Name = "col2CallVol"
        Me.col2CallVol.ReadOnly = True
        Me.col2CallVol.Width = 58
        '
        'col2CE
        '
        Me.col2CE.DataPropertyName = "CE"
        Me.col2CE.HeaderText = "CE"
        Me.col2CE.Name = "col2CE"
        Me.col2CE.ReadOnly = True
        Me.col2CE.Width = 48
        '
        'col2Strike2
        '
        Me.col2Strike2.DataPropertyName = "Strike2"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.col2Strike2.DefaultCellStyle = DataGridViewCellStyle9
        Me.col2Strike2.HeaderText = "Strike"
        Me.col2Strike2.Name = "col2Strike2"
        Me.col2Strike2.ReadOnly = True
        Me.col2Strike2.Width = 65
        '
        'col2PE
        '
        Me.col2PE.DataPropertyName = "PE"
        Me.col2PE.HeaderText = "PE"
        Me.col2PE.Name = "col2PE"
        Me.col2PE.ReadOnly = True
        Me.col2PE.Width = 48
        '
        'col2PutVol
        '
        Me.col2PutVol.DataPropertyName = "PutVol"
        Me.col2PutVol.HeaderText = "Put Vol"
        Me.col2PutVol.Name = "col2PutVol"
        Me.col2PutVol.ReadOnly = True
        Me.col2PutVol.Width = 58
        '
        'col2PutVolChg
        '
        Me.col2PutVolChg.DataPropertyName = "PutVolChg"
        Me.col2PutVolChg.HeaderText = "Put Chg (%)"
        Me.col2PutVolChg.Name = "col2PutVolChg"
        Me.col2PutVolChg.ReadOnly = True
        Me.col2PutVolChg.Width = 87
        '
        'col2PutChg
        '
        Me.col2PutChg.DataPropertyName = "PutChg"
        Me.col2PutChg.HeaderText = "P.Chg (Rs.)"
        Me.col2PutChg.Name = "col2PutChg"
        Me.col2PutChg.ReadOnly = True
        Me.col2PutChg.Width = 81
        '
        'col2PutDelta
        '
        Me.col2PutDelta.DataPropertyName = "PutDelta"
        Me.col2PutDelta.HeaderText = "P.Delta"
        Me.col2PutDelta.Name = "col2PutDelta"
        Me.col2PutDelta.ReadOnly = True
        Me.col2PutDelta.Width = 62
        '
        'col2PutGamma
        '
        Me.col2PutGamma.DataPropertyName = "PutGamma"
        Me.col2PutGamma.HeaderText = "P.Gamma"
        Me.col2PutGamma.Name = "col2PutGamma"
        Me.col2PutGamma.ReadOnly = True
        '
        'col2PutVega
        '
        Me.col2PutVega.DataPropertyName = "PutVega"
        Me.col2PutVega.HeaderText = "P.Vega"
        Me.col2PutVega.Name = "col2PutVega"
        Me.col2PutVega.ReadOnly = True
        Me.col2PutVega.Width = 61
        '
        'col2PutTheta
        '
        Me.col2PutTheta.DataPropertyName = "PutTheta"
        Me.col2PutTheta.HeaderText = "P.Theta"
        Me.col2PutTheta.Name = "col2PutTheta"
        Me.col2PutTheta.ReadOnly = True
        Me.col2PutTheta.Width = 65
        '
        'col2PutVolume
        '
        Me.col2PutVolume.DataPropertyName = "PutVolume"
        Me.col2PutVolume.HeaderText = "P.Volume"
        Me.col2PutVolume.Name = "col2PutVolume"
        Me.col2PutVolume.ReadOnly = True
        Me.col2PutVolume.Width = 73
        '
        'col2PutRatio
        '
        Me.col2PutRatio.DataPropertyName = "PutRatio"
        Me.col2PutRatio.HeaderText = "P.Ratio"
        Me.col2PutRatio.Name = "col2PutRatio"
        Me.col2PutRatio.ReadOnly = True
        Me.col2PutRatio.Width = 62
        '
        'col2Strike3
        '
        Me.col2Strike3.DataPropertyName = "Strike3"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.col2Strike3.DefaultCellStyle = DataGridViewCellStyle10
        Me.col2Strike3.HeaderText = "Strike"
        Me.col2Strike3.Name = "col2Strike3"
        Me.col2Strike3.ReadOnly = True
        Me.col2Strike3.Width = 65
        '
        'col2PutBF
        '
        Me.col2PutBF.DataPropertyName = "PutBF"
        Me.col2PutBF.HeaderText = "P.BF"
        Me.col2PutBF.Name = "col2PutBF"
        Me.col2PutBF.ReadOnly = True
        Me.col2PutBF.Width = 58
        '
        'col2PutBF2
        '
        Me.col2PutBF2.DataPropertyName = "PutBF2"
        Me.col2PutBF2.HeaderText = "P.BF2"
        Me.col2PutBF2.Name = "col2PutBF2"
        Me.col2PutBF2.ReadOnly = True
        Me.col2PutBF2.Width = 58
        '
        'col2CR
        '
        Me.col2CR.DataPropertyName = "CR"
        Me.col2CR.HeaderText = "CR"
        Me.col2CR.Name = "col2CR"
        Me.col2CR.ReadOnly = True
        '
        'col2PutOI
        '
        Me.col2PutOI.DataPropertyName = "PutOI"
        Me.col2PutOI.HeaderText = "P.OI"
        Me.col2PutOI.Name = "col2PutOI"
        Me.col2PutOI.ReadOnly = True
        Me.col2PutOI.Width = 58
        '
        'col2PutOIChg
        '
        Me.col2PutOIChg.DataPropertyName = "PutOIChg"
        Me.col2PutOIChg.HeaderText = "P.Chg OI"
        Me.col2PutOIChg.Name = "col2PutOIChg"
        Me.col2PutOIChg.ReadOnly = True
        Me.col2PutOIChg.Width = 65
        '
        'col2TotalOI
        '
        Me.col2TotalOI.DataPropertyName = "TotalOI"
        Me.col2TotalOI.HeaderText = "P.Total OI"
        Me.col2TotalOI.Name = "col2TotalOI"
        Me.col2TotalOI.ReadOnly = True
        Me.col2TotalOI.Width = 65
        '
        'col2PutTotalOIPer
        '
        Me.col2PutTotalOIPer.DataPropertyName = "PutTotalOIPer"
        Me.col2PutTotalOIPer.HeaderText = "P.Total OI (%)"
        Me.col2PutTotalOIPer.Name = "col2PutTotalOIPer"
        Me.col2PutTotalOIPer.ReadOnly = True
        Me.col2PutTotalOIPer.Width = 65
        '
        'col2CallOIPer
        '
        Me.col2CallOIPer.DataPropertyName = "CallOIPer"
        Me.col2CallOIPer.HeaderText = "Call OI (%)"
        Me.col2CallOIPer.Name = "col2CallOIPer"
        Me.col2CallOIPer.ReadOnly = True
        Me.col2CallOIPer.Width = 65
        '
        'col2PutOIPer
        '
        Me.col2PutOIPer.DataPropertyName = "PutOIPer"
        Me.col2PutOIPer.HeaderText = "Put OI (%)"
        Me.col2PutOIPer.Name = "col2PutOIPer"
        Me.col2PutOIPer.ReadOnly = True
        Me.col2PutOIPer.Width = 65
        '
        'col2PCPB
        '
        Me.col2PCPB.DataPropertyName = "PCPB"
        Me.col2PCPB.HeaderText = "PCP Bid"
        Me.col2PCPB.Name = "col2PCPB"
        Me.col2PCPB.ReadOnly = True
        '
        'col2PCP
        '
        Me.col2PCP.DataPropertyName = "PCP"
        Me.col2PCP.HeaderText = "PCP"
        Me.col2PCP.Name = "col2PCP"
        Me.col2PCP.ReadOnly = True
        Me.col2PCP.Width = 56
        '
        'col2PCPA
        '
        Me.col2PCPA.DataPropertyName = "PCPA"
        Me.col2PCPA.HeaderText = "PCP Ask"
        Me.col2PCPA.Name = "col2PCPA"
        Me.col2PCPA.ReadOnly = True
        '
        'col2C2C
        '
        Me.col2C2C.DataPropertyName = "C2C"
        Me.col2C2C.HeaderText = "C2C"
        Me.col2C2C.Name = "col2C2C"
        Me.col2C2C.ReadOnly = True
        Me.col2C2C.Width = 55
        '
        'col2P2P
        '
        Me.col2P2P.DataPropertyName = "P2P"
        Me.col2P2P.HeaderText = "P2P"
        Me.col2P2P.Name = "col2P2P"
        Me.col2P2P.ReadOnly = True
        Me.col2P2P.Width = 55
        '
        'col2C2P
        '
        Me.col2C2P.DataPropertyName = "C2P"
        Me.col2C2P.HeaderText = "C2P"
        Me.col2C2P.Name = "col2C2P"
        Me.col2C2P.ReadOnly = True
        Me.col2C2P.Width = 55
        '
        'col2CallCalender
        '
        Me.col2CallCalender.DataPropertyName = "CallCalender"
        Me.col2CallCalender.HeaderText = "Call Calender"
        Me.col2CallCalender.Name = "col2CallCalender"
        Me.col2CallCalender.ReadOnly = True
        Me.col2CallCalender.Width = 98
        '
        'col2PutCalender
        '
        Me.col2PutCalender.DataPropertyName = "PutCalender"
        Me.col2PutCalender.HeaderText = "Put Calender"
        Me.col2PutCalender.Name = "col2PutCalender"
        Me.col2PutCalender.ReadOnly = True
        Me.col2PutCalender.Width = 97
        '
        'col2CallBullSpread
        '
        Me.col2CallBullSpread.DataPropertyName = "CallBullSpread"
        Me.col2CallBullSpread.HeaderText = "Call BullSpread"
        Me.col2CallBullSpread.Name = "col2CallBullSpread"
        Me.col2CallBullSpread.ReadOnly = True
        '
        'col2PutBearSpread
        '
        Me.col2PutBearSpread.DataPropertyName = "PutBearSpread"
        Me.col2PutBearSpread.HeaderText = "Put BearSpread"
        Me.col2PutBearSpread.Name = "col2PutBearSpread"
        Me.col2PutBearSpread.ReadOnly = True
        '
        'col2CallToken
        '
        Me.col2CallToken.DataPropertyName = "CallToken"
        Me.col2CallToken.HeaderText = "Call Token"
        Me.col2CallToken.Name = "col2CallToken"
        Me.col2CallToken.ReadOnly = True
        Me.col2CallToken.Visible = False
        '
        'col2PutToken
        '
        Me.col2PutToken.DataPropertyName = "PutToken"
        Me.col2PutToken.HeaderText = "Put Token"
        Me.col2PutToken.Name = "col2PutToken"
        Me.col2PutToken.ReadOnly = True
        Me.col2PutToken.Visible = False
        '
        'col2Maturity
        '
        Me.col2Maturity.DataPropertyName = "Maturity"
        Me.col2Maturity.HeaderText = "Maturity"
        Me.col2Maturity.Name = "col2Maturity"
        Me.col2Maturity.ReadOnly = True
        Me.col2Maturity.Visible = False
        '
        'col2Symbol
        '
        Me.col2Symbol.DataPropertyName = "Symbol"
        Me.col2Symbol.HeaderText = "Symbol"
        Me.col2Symbol.Name = "col2Symbol"
        Me.col2Symbol.ReadOnly = True
        Me.col2Symbol.Visible = False
        '
        'col2futltp
        '
        Me.col2futltp.DataPropertyName = "futltp"
        Me.col2futltp.HeaderText = "futltp"
        Me.col2futltp.Name = "col2futltp"
        Me.col2futltp.ReadOnly = True
        Me.col2futltp.Visible = False
        '
        'col2CallPreToken
        '
        Me.col2CallPreToken.DataPropertyName = "CallPreToken"
        Me.col2CallPreToken.HeaderText = "CallPreToken"
        Me.col2CallPreToken.Name = "col2CallPreToken"
        Me.col2CallPreToken.ReadOnly = True
        Me.col2CallPreToken.Visible = False
        '
        'col2CallPre2Token
        '
        Me.col2CallPre2Token.DataPropertyName = "CallPre2Token"
        Me.col2CallPre2Token.HeaderText = "CallPre2Token"
        Me.col2CallPre2Token.Name = "col2CallPre2Token"
        Me.col2CallPre2Token.ReadOnly = True
        Me.col2CallPre2Token.Visible = False
        '
        'col2CallNextToken
        '
        Me.col2CallNextToken.DataPropertyName = "CallNextToken"
        Me.col2CallNextToken.HeaderText = "CallNextToken"
        Me.col2CallNextToken.Name = "col2CallNextToken"
        Me.col2CallNextToken.ReadOnly = True
        Me.col2CallNextToken.Visible = False
        '
        'col2CallNext2Token
        '
        Me.col2CallNext2Token.DataPropertyName = "CallNext2Token"
        Me.col2CallNext2Token.HeaderText = "CallNext2Token"
        Me.col2CallNext2Token.Name = "col2CallNext2Token"
        Me.col2CallNext2Token.ReadOnly = True
        Me.col2CallNext2Token.Visible = False
        '
        'col2PutPreToken
        '
        Me.col2PutPreToken.DataPropertyName = "PutPreToken"
        Me.col2PutPreToken.HeaderText = "PutPreToken"
        Me.col2PutPreToken.Name = "col2PutPreToken"
        Me.col2PutPreToken.ReadOnly = True
        Me.col2PutPreToken.Visible = False
        '
        'col2PutPre2Token
        '
        Me.col2PutPre2Token.DataPropertyName = "PutPre2Token"
        Me.col2PutPre2Token.HeaderText = "PutPre2Token"
        Me.col2PutPre2Token.Name = "col2PutPre2Token"
        Me.col2PutPre2Token.ReadOnly = True
        Me.col2PutPre2Token.Visible = False
        '
        'col2PutNextToken
        '
        Me.col2PutNextToken.DataPropertyName = "PutNextToken"
        Me.col2PutNextToken.HeaderText = "PutNextToken"
        Me.col2PutNextToken.Name = "col2PutNextToken"
        Me.col2PutNextToken.ReadOnly = True
        Me.col2PutNextToken.Visible = False
        '
        'col2PutNext2Token
        '
        Me.col2PutNext2Token.DataPropertyName = "PutNext2Token"
        Me.col2PutNext2Token.HeaderText = "PutNext2Token"
        Me.col2PutNext2Token.Name = "col2PutNext2Token"
        Me.col2PutNext2Token.ReadOnly = True
        Me.col2PutNext2Token.Visible = False
        '
        'col2CallVol1
        '
        Me.col2CallVol1.DataPropertyName = "CallVol1"
        Me.col2CallVol1.HeaderText = "CallVol1"
        Me.col2CallVol1.Name = "col2CallVol1"
        Me.col2CallVol1.ReadOnly = True
        '
        'col2PutVol1
        '
        Me.col2PutVol1.DataPropertyName = "PutVol1"
        Me.col2PutVol1.HeaderText = "PutVol1"
        Me.col2PutVol1.Name = "col2PutVol1"
        Me.col2PutVol1.ReadOnly = True
        '
        'Col2CallTrend
        '
        Me.Col2CallTrend.DataPropertyName = "CEStatus"
        Me.Col2CallTrend.HeaderText = "Call Trend"
        Me.Col2CallTrend.Name = "Col2CallTrend"
        Me.Col2CallTrend.ReadOnly = True
        '
        'col2CallStopLoss
        '
        Me.col2CallStopLoss.DataPropertyName = "CEStopLoss"
        Me.col2CallStopLoss.HeaderText = "Call StopLoss"
        Me.col2CallStopLoss.Name = "col2CallStopLoss"
        Me.col2CallStopLoss.ReadOnly = True
        '
        'Col2PutTrend
        '
        Me.Col2PutTrend.DataPropertyName = "PEStatus"
        Me.Col2PutTrend.HeaderText = "Put Trend"
        Me.Col2PutTrend.Name = "Col2PutTrend"
        Me.Col2PutTrend.ReadOnly = True
        '
        'Col2PutStopLoss
        '
        Me.Col2PutStopLoss.DataPropertyName = "PEStopLoss"
        Me.Col2PutStopLoss.HeaderText = "Put StopLoss"
        Me.Col2PutStopLoss.Name = "Col2PutStopLoss"
        Me.Col2PutStopLoss.ReadOnly = True
        '
        'collapsExpandPanel3
        '
        Me.collapsExpandPanel3.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.collapsExpandPanel3.Controls.Add(Me.dgv_exp3)
        Me.collapsExpandPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowLayoutPanel1.SetFlowBreak(Me.collapsExpandPanel3, True)
        Me.collapsExpandPanel3.HeaderHeight = 20
        Me.collapsExpandPanel3.HeaderText = "Third Expiry Date: "
        Me.collapsExpandPanel3.Location = New System.Drawing.Point(3, 1050)
        Me.collapsExpandPanel3.Name = "collapsExpandPanel3"
        Me.collapsExpandPanel3.ScrollInterval = 10
        Me.collapsExpandPanel3.Size = New System.Drawing.Size(1326, 440)
        Me.collapsExpandPanel3.TabIndex = 2
        Me.collapsExpandPanel3.TabStop = False
        '
        'dgv_exp3
        '
        Me.dgv_exp3.AllowUserToAddRows = False
        Me.dgv_exp3.AllowUserToDeleteRows = False
        Me.dgv_exp3.AllowUserToResizeRows = False
        Me.dgv_exp3.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.dgv_exp3.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.ControlDark
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_exp3.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.dgv_exp3.ColumnHeadersHeight = 60
        Me.dgv_exp3.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col3Strike1, Me.col3CallOIChg, Me.col3CallOI, Me.col3CallBF, Me.col3CallBF2, Me.col3CallStraddle, Me.col3CallRatio, Me.col3CallVolume, Me.col3CallDelta, Me.col3CallGamma, Me.col3CallVega, Me.col3CallTheta, Me.col3CallChg, Me.col3CallVolChg, Me.col3CallVol, Me.col3CE, Me.col3Strike2, Me.col3PE, Me.col3PutVol, Me.col3PutVolChg, Me.col3PutChg, Me.col3PutDelta, Me.col3PutGamma, Me.col3PutVega, Me.col3PutTheta, Me.col3PutVolume, Me.col3PutRatio, Me.col3Strike3, Me.col3PutBF, Me.col3PutBF2, Me.col3CR, Me.col3PutOI, Me.col3PutOIChg, Me.col3TotalOI, Me.col3PutTotalOIPer, Me.col3CallOIPer, Me.col3PutOIPer, Me.col3PCPB, Me.col3PCP, Me.col3PCPA, Me.col3C2C, Me.col3P2P, Me.col3C2P, Me.col3CallCalender, Me.col3PutCalender, Me.col3CallBullSpread, Me.col3PutBearSpread, Me.col3CallToken, Me.col3PutToken, Me.col3Maturity, Me.col3Symbol, Me.col3futltp, Me.col3CallPreToken, Me.col3CallPre2Token, Me.col3CallNextToken, Me.col3CallNext2Token, Me.col3PutPreToken, Me.col3PutPre2Token, Me.col3PutNextToken, Me.col3PutNext2Token, Me.col3CallVol1, Me.col3PutVol1, Me.Col3CallTrend, Me.Col3CallStopLoss, Me.Col3PutTrend, Me.Col3PutStopLoss})
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_exp3.DefaultCellStyle = DataGridViewCellStyle17
        Me.dgv_exp3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_exp3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_exp3.EnableHeadersVisualStyles = False
        Me.dgv_exp3.Location = New System.Drawing.Point(0, 0)
        Me.dgv_exp3.Name = "dgv_exp3"
        Me.dgv_exp3.ReadOnly = True
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_exp3.RowHeadersDefaultCellStyle = DataGridViewCellStyle18
        Me.dgv_exp3.RowHeadersVisible = False
        Me.dgv_exp3.RowHeadersWidth = 4
        Me.dgv_exp3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgv_exp3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgv_exp3.Size = New System.Drawing.Size(1326, 440)
        Me.dgv_exp3.TabIndex = 4
        Me.dgv_exp3.TabStop = False
        '
        'col3Strike1
        '
        Me.col3Strike1.DataPropertyName = "Strike1"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.col3Strike1.DefaultCellStyle = DataGridViewCellStyle14
        Me.col3Strike1.HeaderText = "Strike"
        Me.col3Strike1.Name = "col3Strike1"
        Me.col3Strike1.ReadOnly = True
        Me.col3Strike1.Width = 65
        '
        'col3CallOIChg
        '
        Me.col3CallOIChg.DataPropertyName = "CallOIChg"
        Me.col3CallOIChg.HeaderText = "C.Chg OI"
        Me.col3CallOIChg.Name = "col3CallOIChg"
        Me.col3CallOIChg.ReadOnly = True
        Me.col3CallOIChg.Width = 71
        '
        'col3CallOI
        '
        Me.col3CallOI.DataPropertyName = "CallOI"
        Me.col3CallOI.HeaderText = "C.OI"
        Me.col3CallOI.Name = "col3CallOI"
        Me.col3CallOI.ReadOnly = True
        Me.col3CallOI.Width = 68
        '
        'col3CallBF
        '
        Me.col3CallBF.DataPropertyName = "CallBF"
        Me.col3CallBF.HeaderText = "C.BF"
        Me.col3CallBF.Name = "col3CallBF"
        Me.col3CallBF.ReadOnly = True
        Me.col3CallBF.Width = 58
        '
        'col3CallBF2
        '
        Me.col3CallBF2.DataPropertyName = "CallBF2"
        Me.col3CallBF2.HeaderText = "C.BF2"
        Me.col3CallBF2.Name = "col3CallBF2"
        Me.col3CallBF2.ReadOnly = True
        Me.col3CallBF2.Width = 58
        '
        'col3CallStraddle
        '
        Me.col3CallStraddle.DataPropertyName = "CallStraddle"
        Me.col3CallStraddle.HeaderText = "Straddle"
        Me.col3CallStraddle.Name = "col3CallStraddle"
        Me.col3CallStraddle.ReadOnly = True
        Me.col3CallStraddle.Width = 79
        '
        'col3CallRatio
        '
        Me.col3CallRatio.DataPropertyName = "CallRatio"
        Me.col3CallRatio.HeaderText = "C.Ratio"
        Me.col3CallRatio.Name = "col3CallRatio"
        Me.col3CallRatio.ReadOnly = True
        Me.col3CallRatio.Width = 62
        '
        'col3CallVolume
        '
        Me.col3CallVolume.DataPropertyName = "CallVolume"
        Me.col3CallVolume.HeaderText = "C.Volume"
        Me.col3CallVolume.Name = "col3CallVolume"
        Me.col3CallVolume.ReadOnly = True
        Me.col3CallVolume.Width = 73
        '
        'col3CallDelta
        '
        Me.col3CallDelta.DataPropertyName = "CallDelta"
        Me.col3CallDelta.HeaderText = "C.Delta"
        Me.col3CallDelta.Name = "col3CallDelta"
        Me.col3CallDelta.ReadOnly = True
        Me.col3CallDelta.Width = 62
        '
        'col3CallGamma
        '
        Me.col3CallGamma.DataPropertyName = "CallGamma"
        Me.col3CallGamma.HeaderText = "C.Gamma"
        Me.col3CallGamma.Name = "col3CallGamma"
        Me.col3CallGamma.ReadOnly = True
        Me.col3CallGamma.Width = 73
        '
        'col3CallVega
        '
        Me.col3CallVega.DataPropertyName = "CallVega"
        Me.col3CallVega.HeaderText = "C.Vega"
        Me.col3CallVega.Name = "col3CallVega"
        Me.col3CallVega.ReadOnly = True
        Me.col3CallVega.Width = 61
        '
        'col3CallTheta
        '
        Me.col3CallTheta.DataPropertyName = "CallTheta"
        Me.col3CallTheta.HeaderText = "C.Theta"
        Me.col3CallTheta.Name = "col3CallTheta"
        Me.col3CallTheta.ReadOnly = True
        Me.col3CallTheta.Width = 65
        '
        'col3CallChg
        '
        Me.col3CallChg.DataPropertyName = "CallChg"
        Me.col3CallChg.HeaderText = "C.Chg(Rs.)"
        Me.col3CallChg.Name = "col3CallChg"
        Me.col3CallChg.ReadOnly = True
        Me.col3CallChg.Width = 81
        '
        'col3CallVolChg
        '
        Me.col3CallVolChg.DataPropertyName = "CallVolChg"
        Me.col3CallVolChg.HeaderText = "C.Call Chg (%)"
        Me.col3CallVolChg.Name = "col3CallVolChg"
        Me.col3CallVolChg.ReadOnly = True
        '
        'col3CallVol
        '
        Me.col3CallVol.DataPropertyName = "CallVol"
        Me.col3CallVol.HeaderText = "C.Call Vol"
        Me.col3CallVol.Name = "col3CallVol"
        Me.col3CallVol.ReadOnly = True
        Me.col3CallVol.Width = 75
        '
        'col3CE
        '
        Me.col3CE.DataPropertyName = "CE"
        Me.col3CE.HeaderText = "CE"
        Me.col3CE.Name = "col3CE"
        Me.col3CE.ReadOnly = True
        Me.col3CE.Width = 58
        '
        'col3Strike2
        '
        Me.col3Strike2.DataPropertyName = "Strike2"
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle15.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.col3Strike2.DefaultCellStyle = DataGridViewCellStyle15
        Me.col3Strike2.HeaderText = "Strike"
        Me.col3Strike2.Name = "col3Strike2"
        Me.col3Strike2.ReadOnly = True
        Me.col3Strike2.Width = 65
        '
        'col3PE
        '
        Me.col3PE.DataPropertyName = "PE"
        Me.col3PE.HeaderText = "PE"
        Me.col3PE.Name = "col3PE"
        Me.col3PE.ReadOnly = True
        Me.col3PE.Width = 68
        '
        'col3PutVol
        '
        Me.col3PutVol.DataPropertyName = "PutVol"
        Me.col3PutVol.HeaderText = "Put Vol"
        Me.col3PutVol.Name = "col3PutVol"
        Me.col3PutVol.ReadOnly = True
        Me.col3PutVol.Width = 73
        '
        'col3PutVolChg
        '
        Me.col3PutVolChg.DataPropertyName = "PutVolChg"
        Me.col3PutVolChg.HeaderText = "Put Chg (%)"
        Me.col3PutVolChg.Name = "col3PutVolChg"
        Me.col3PutVolChg.ReadOnly = True
        Me.col3PutVolChg.Width = 98
        '
        'col3PutChg
        '
        Me.col3PutChg.DataPropertyName = "PutChg"
        Me.col3PutChg.HeaderText = "P.Chg(Rs.)"
        Me.col3PutChg.Name = "col3PutChg"
        Me.col3PutChg.ReadOnly = True
        Me.col3PutChg.Width = 81
        '
        'col3PutDelta
        '
        Me.col3PutDelta.DataPropertyName = "PutDelta"
        Me.col3PutDelta.HeaderText = "P.Delta"
        Me.col3PutDelta.Name = "col3PutDelta"
        Me.col3PutDelta.ReadOnly = True
        Me.col3PutDelta.Width = 62
        '
        'col3PutGamma
        '
        Me.col3PutGamma.DataPropertyName = "PutGamma"
        Me.col3PutGamma.HeaderText = "P.Gamma"
        Me.col3PutGamma.Name = "col3PutGamma"
        Me.col3PutGamma.ReadOnly = True
        '
        'col3PutVega
        '
        Me.col3PutVega.DataPropertyName = "PutVega"
        Me.col3PutVega.HeaderText = "P.Vega"
        Me.col3PutVega.Name = "col3PutVega"
        Me.col3PutVega.ReadOnly = True
        Me.col3PutVega.Width = 61
        '
        'col3PutTheta
        '
        Me.col3PutTheta.DataPropertyName = "PutTheta"
        Me.col3PutTheta.HeaderText = "P.Theta"
        Me.col3PutTheta.Name = "col3PutTheta"
        Me.col3PutTheta.ReadOnly = True
        Me.col3PutTheta.Width = 65
        '
        'col3PutVolume
        '
        Me.col3PutVolume.DataPropertyName = "PutVolume"
        Me.col3PutVolume.HeaderText = "P.Volume"
        Me.col3PutVolume.Name = "col3PutVolume"
        Me.col3PutVolume.ReadOnly = True
        Me.col3PutVolume.Width = 73
        '
        'col3PutRatio
        '
        Me.col3PutRatio.DataPropertyName = "PutRatio"
        Me.col3PutRatio.HeaderText = "P.Ratio"
        Me.col3PutRatio.Name = "col3PutRatio"
        Me.col3PutRatio.ReadOnly = True
        Me.col3PutRatio.Width = 62
        '
        'col3Strike3
        '
        Me.col3Strike3.DataPropertyName = "Strike3"
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle16.BackColor = System.Drawing.Color.Black
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.col3Strike3.DefaultCellStyle = DataGridViewCellStyle16
        Me.col3Strike3.HeaderText = "Strike"
        Me.col3Strike3.Name = "col3Strike3"
        Me.col3Strike3.ReadOnly = True
        Me.col3Strike3.Width = 65
        '
        'col3PutBF
        '
        Me.col3PutBF.DataPropertyName = "PutBF"
        Me.col3PutBF.HeaderText = "P.BF"
        Me.col3PutBF.Name = "col3PutBF"
        Me.col3PutBF.ReadOnly = True
        Me.col3PutBF.Width = 58
        '
        'col3PutBF2
        '
        Me.col3PutBF2.DataPropertyName = "PutBF2"
        Me.col3PutBF2.HeaderText = "P.BF2"
        Me.col3PutBF2.Name = "col3PutBF2"
        Me.col3PutBF2.ReadOnly = True
        Me.col3PutBF2.Width = 58
        '
        'col3CR
        '
        Me.col3CR.DataPropertyName = "CR"
        Me.col3CR.HeaderText = "CR"
        Me.col3CR.Name = "col3CR"
        Me.col3CR.ReadOnly = True
        '
        'col3PutOI
        '
        Me.col3PutOI.DataPropertyName = "PutOI"
        Me.col3PutOI.HeaderText = "P.OI"
        Me.col3PutOI.Name = "col3PutOI"
        Me.col3PutOI.ReadOnly = True
        Me.col3PutOI.Width = 68
        '
        'col3PutOIChg
        '
        Me.col3PutOIChg.DataPropertyName = "PutOIChg"
        Me.col3PutOIChg.HeaderText = "P.Chg OI"
        Me.col3PutOIChg.Name = "col3PutOIChg"
        Me.col3PutOIChg.ReadOnly = True
        Me.col3PutOIChg.Width = 68
        '
        'col3TotalOI
        '
        Me.col3TotalOI.DataPropertyName = "TotalOI"
        Me.col3TotalOI.HeaderText = "P.Total OI"
        Me.col3TotalOI.Name = "col3TotalOI"
        Me.col3TotalOI.ReadOnly = True
        Me.col3TotalOI.Width = 78
        '
        'col3PutTotalOIPer
        '
        Me.col3PutTotalOIPer.DataPropertyName = "PutTotalOIPer"
        Me.col3PutTotalOIPer.HeaderText = "P.Total OI (%)"
        Me.col3PutTotalOIPer.Name = "col3PutTotalOIPer"
        Me.col3PutTotalOIPer.ReadOnly = True
        Me.col3PutTotalOIPer.Width = 99
        '
        'col3CallOIPer
        '
        Me.col3CallOIPer.DataPropertyName = "CallOIPer"
        Me.col3CallOIPer.HeaderText = "P.Call OI (%)"
        Me.col3CallOIPer.Name = "col3CallOIPer"
        Me.col3CallOIPer.ReadOnly = True
        Me.col3CallOIPer.Width = 70
        '
        'col3PutOIPer
        '
        Me.col3PutOIPer.DataPropertyName = "PutOIPer"
        Me.col3PutOIPer.HeaderText = "P.Put OI (%)"
        Me.col3PutOIPer.Name = "col3PutOIPer"
        Me.col3PutOIPer.ReadOnly = True
        Me.col3PutOIPer.Width = 68
        '
        'col3PCPB
        '
        Me.col3PCPB.DataPropertyName = "PCPB"
        Me.col3PCPB.HeaderText = "PCP Bid"
        Me.col3PCPB.Name = "col3PCPB"
        Me.col3PCPB.ReadOnly = True
        '
        'col3PCP
        '
        Me.col3PCP.DataPropertyName = "PCP"
        Me.col3PCP.HeaderText = "PCP"
        Me.col3PCP.Name = "col3PCP"
        Me.col3PCP.ReadOnly = True
        Me.col3PCP.Width = 58
        '
        'col3PCPA
        '
        Me.col3PCPA.DataPropertyName = "PCPA"
        Me.col3PCPA.HeaderText = "PCP Ask"
        Me.col3PCPA.Name = "col3PCPA"
        Me.col3PCPA.ReadOnly = True
        '
        'col3C2C
        '
        Me.col3C2C.DataPropertyName = "C2C"
        Me.col3C2C.HeaderText = "C2C"
        Me.col3C2C.Name = "col3C2C"
        Me.col3C2C.ReadOnly = True
        Me.col3C2C.Width = 58
        '
        'col3P2P
        '
        Me.col3P2P.DataPropertyName = "P2P"
        Me.col3P2P.HeaderText = "P2P"
        Me.col3P2P.Name = "col3P2P"
        Me.col3P2P.ReadOnly = True
        Me.col3P2P.Width = 58
        '
        'col3C2P
        '
        Me.col3C2P.DataPropertyName = "C2P"
        Me.col3C2P.HeaderText = "C2P"
        Me.col3C2P.Name = "col3C2P"
        Me.col3C2P.ReadOnly = True
        Me.col3C2P.Width = 58
        '
        'col3CallCalender
        '
        Me.col3CallCalender.DataPropertyName = "CallCalender"
        Me.col3CallCalender.HeaderText = "Call Calender"
        Me.col3CallCalender.Name = "col3CallCalender"
        Me.col3CallCalender.ReadOnly = True
        Me.col3CallCalender.Width = 107
        '
        'col3PutCalender
        '
        Me.col3PutCalender.DataPropertyName = "PutCalender"
        Me.col3PutCalender.HeaderText = "Put Calender"
        Me.col3PutCalender.Name = "col3PutCalender"
        Me.col3PutCalender.ReadOnly = True
        Me.col3PutCalender.Width = 105
        '
        'col3CallBullSpread
        '
        Me.col3CallBullSpread.DataPropertyName = "CallBullSpread"
        Me.col3CallBullSpread.HeaderText = "Call BullSpread"
        Me.col3CallBullSpread.Name = "col3CallBullSpread"
        Me.col3CallBullSpread.ReadOnly = True
        '
        'col3PutBearSpread
        '
        Me.col3PutBearSpread.DataPropertyName = "PutBearSpread"
        Me.col3PutBearSpread.HeaderText = "Put BearSpread"
        Me.col3PutBearSpread.Name = "col3PutBearSpread"
        Me.col3PutBearSpread.ReadOnly = True
        '
        'col3CallToken
        '
        Me.col3CallToken.DataPropertyName = "CallToken"
        Me.col3CallToken.HeaderText = "Call Token"
        Me.col3CallToken.Name = "col3CallToken"
        Me.col3CallToken.ReadOnly = True
        Me.col3CallToken.Visible = False
        '
        'col3PutToken
        '
        Me.col3PutToken.DataPropertyName = "PutToken"
        Me.col3PutToken.HeaderText = "Put Token"
        Me.col3PutToken.Name = "col3PutToken"
        Me.col3PutToken.ReadOnly = True
        Me.col3PutToken.Visible = False
        '
        'col3Maturity
        '
        Me.col3Maturity.DataPropertyName = "Maturity"
        Me.col3Maturity.HeaderText = "Maturity"
        Me.col3Maturity.Name = "col3Maturity"
        Me.col3Maturity.ReadOnly = True
        Me.col3Maturity.Visible = False
        '
        'col3Symbol
        '
        Me.col3Symbol.DataPropertyName = "Symbol"
        Me.col3Symbol.HeaderText = "Symbol"
        Me.col3Symbol.Name = "col3Symbol"
        Me.col3Symbol.ReadOnly = True
        Me.col3Symbol.Visible = False
        '
        'col3futltp
        '
        Me.col3futltp.DataPropertyName = "futltp"
        Me.col3futltp.HeaderText = "futltp"
        Me.col3futltp.Name = "col3futltp"
        Me.col3futltp.ReadOnly = True
        Me.col3futltp.Visible = False
        '
        'col3CallPreToken
        '
        Me.col3CallPreToken.DataPropertyName = "CallPreToken"
        Me.col3CallPreToken.HeaderText = "CallPreToken"
        Me.col3CallPreToken.Name = "col3CallPreToken"
        Me.col3CallPreToken.ReadOnly = True
        Me.col3CallPreToken.Visible = False
        '
        'col3CallPre2Token
        '
        Me.col3CallPre2Token.DataPropertyName = "CallPre2Token"
        Me.col3CallPre2Token.HeaderText = "CallPre2Token"
        Me.col3CallPre2Token.Name = "col3CallPre2Token"
        Me.col3CallPre2Token.ReadOnly = True
        Me.col3CallPre2Token.Visible = False
        '
        'col3CallNextToken
        '
        Me.col3CallNextToken.DataPropertyName = "CallNextToken"
        Me.col3CallNextToken.HeaderText = "CallNextToken"
        Me.col3CallNextToken.Name = "col3CallNextToken"
        Me.col3CallNextToken.ReadOnly = True
        Me.col3CallNextToken.Visible = False
        '
        'col3CallNext2Token
        '
        Me.col3CallNext2Token.DataPropertyName = "CallNext2Token"
        Me.col3CallNext2Token.HeaderText = "CallNext2Token"
        Me.col3CallNext2Token.Name = "col3CallNext2Token"
        Me.col3CallNext2Token.ReadOnly = True
        Me.col3CallNext2Token.Visible = False
        '
        'col3PutPreToken
        '
        Me.col3PutPreToken.DataPropertyName = "PutPreToken"
        Me.col3PutPreToken.HeaderText = "PutPreToken"
        Me.col3PutPreToken.Name = "col3PutPreToken"
        Me.col3PutPreToken.ReadOnly = True
        Me.col3PutPreToken.Visible = False
        '
        'col3PutPre2Token
        '
        Me.col3PutPre2Token.DataPropertyName = "PutPre2Token"
        Me.col3PutPre2Token.HeaderText = "PutPre2Token"
        Me.col3PutPre2Token.Name = "col3PutPre2Token"
        Me.col3PutPre2Token.ReadOnly = True
        Me.col3PutPre2Token.Visible = False
        '
        'col3PutNextToken
        '
        Me.col3PutNextToken.DataPropertyName = "PutNextToken"
        Me.col3PutNextToken.HeaderText = "PutNextToken"
        Me.col3PutNextToken.Name = "col3PutNextToken"
        Me.col3PutNextToken.ReadOnly = True
        Me.col3PutNextToken.Visible = False
        '
        'col3PutNext2Token
        '
        Me.col3PutNext2Token.DataPropertyName = "PutNext2Token"
        Me.col3PutNext2Token.HeaderText = "PutNext2Token"
        Me.col3PutNext2Token.Name = "col3PutNext2Token"
        Me.col3PutNext2Token.ReadOnly = True
        Me.col3PutNext2Token.Visible = False
        '
        'col3CallVol1
        '
        Me.col3CallVol1.DataPropertyName = "CallVol1"
        Me.col3CallVol1.HeaderText = "CallVol1"
        Me.col3CallVol1.Name = "col3CallVol1"
        Me.col3CallVol1.ReadOnly = True
        '
        'col3PutVol1
        '
        Me.col3PutVol1.DataPropertyName = "PutVol1"
        Me.col3PutVol1.HeaderText = "PutVol1"
        Me.col3PutVol1.Name = "col3PutVol1"
        Me.col3PutVol1.ReadOnly = True
        '
        'Col3CallTrend
        '
        Me.Col3CallTrend.DataPropertyName = "CEStatus"
        Me.Col3CallTrend.HeaderText = "Call Trend"
        Me.Col3CallTrend.Name = "Col3CallTrend"
        Me.Col3CallTrend.ReadOnly = True
        '
        'Col3CallStopLoss
        '
        Me.Col3CallStopLoss.DataPropertyName = "CEStoploss"
        Me.Col3CallStopLoss.HeaderText = "Call StopLoss"
        Me.Col3CallStopLoss.Name = "Col3CallStopLoss"
        Me.Col3CallStopLoss.ReadOnly = True
        '
        'Col3PutTrend
        '
        Me.Col3PutTrend.DataPropertyName = "PEStatus"
        Me.Col3PutTrend.HeaderText = "Put Trend"
        Me.Col3PutTrend.Name = "Col3PutTrend"
        Me.Col3PutTrend.ReadOnly = True
        '
        'Col3PutStopLoss
        '
        Me.Col3PutStopLoss.DataPropertyName = "PEStoploss"
        Me.Col3PutStopLoss.HeaderText = "Put StopLoss"
        Me.Col3PutStopLoss.Name = "Col3PutStopLoss"
        Me.Col3PutStopLoss.ReadOnly = True
        '
        'frmMarketwatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(877, 643)
        Me.Controls.Add(Me.flowLayoutPanel1)
        Me.Name = "frmMarketwatch"
        Me.Text = "frmMarketwatch"
        Me.flowLayoutPanel1.ResumeLayout(False)
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBPROFILESAVE.ResumeLayout(False)
        Me.GBPROFILESAVE.PerformLayout()
        Me.GBLOADPROFILE.ResumeLayout(False)
        Me.GBLOADPROFILE.PerformLayout()
        Me.collapsExpandPanel1.ResumeLayout(False)
        CType(Me.dgv_Exp1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.collapsExpandPanel2.ResumeLayout(False)
        CType(Me.dgv_exp2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.collapsExpandPanel3.ResumeLayout(False)
        CType(Me.dgv_exp3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents flowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RBcalVol As System.Windows.Forms.RadioButton
    Friend WithEvents RBsynfut As System.Windows.Forms.RadioButton
    Friend WithEvents chkthird As System.Windows.Forms.CheckBox
    Friend WithEvents chksecond As System.Windows.Forms.CheckBox
    Friend WithEvents chkfirst As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNoStrike As System.Windows.Forms.TextBox
    Friend WithEvents chkIsWeekly As System.Windows.Forms.CheckBox
    Friend WithEvents chkSkip As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RBAnalysis As System.Windows.Forms.RadioButton
    Friend WithEvents RBScenario As System.Windows.Forms.RadioButton
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents txtnoofday As System.Windows.Forms.TextBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnColumnSetting As System.Windows.Forms.Button
    Friend WithEvents lblFuture1 As System.Windows.Forms.Label
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Private WithEvents txtSymbol As System.Windows.Forms.TextBox
    Private WithEvents lblBusinessDate As System.Windows.Forms.Label
    Friend WithEvents GBPROFILESAVE As System.Windows.Forms.GroupBox
    Friend WithEvents Profilesavecancel As System.Windows.Forms.Button
    Friend WithEvents txtProfileName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BtnProfileSave As System.Windows.Forms.Button
    Friend WithEvents GBLOADPROFILE As System.Windows.Forms.GroupBox
    Friend WithEvents profileloadcancel As System.Windows.Forms.Button
    Friend WithEvents DeleteProfile As System.Windows.Forms.Button
    Friend WithEvents cmbprofilename As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnProfileLoad As System.Windows.Forms.Button
    Private WithEvents collapsExpandPanel1 As ScrollablePanel.CollapsExpandPanel
    Private WithEvents dgv_Exp1 As System.Windows.Forms.DataGridView
    Friend WithEvents col1Strike1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallOIChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallOI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallBF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallBF2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallStraddle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallRatio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallVolume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallDelta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallGamma As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallVega As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallTheta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallVolChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallVol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1Strike2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutVol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutVolChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutDelta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutGamma As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutVega As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutTheta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutVolume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutRatio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1Strike3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutBF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutBF2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutOI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutOIChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1TotalOI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutTotalOIPer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallOIPer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutOIPer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PCPB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PCP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PCPA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1C2C As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1P2P As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1C2P As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallCalender As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutCalender As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallBullSpread As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutBearSpread As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1Maturity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1Symbol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1futltp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallPreToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallPre2Token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallNextToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallNext2Token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutPreToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutPre2Token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutNextToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutNext2Token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallVol1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutVol1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallTrend As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1CallStopLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutTrend As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col1PutStopLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents collapsExpandPanel2 As ScrollablePanel.CollapsExpandPanel
    Private WithEvents dgv_exp2 As System.Windows.Forms.DataGridView
    Friend WithEvents col2Strike1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallOIChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallOI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallBF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallBF2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallStraddle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallRatio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallVolume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallDelta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallGamma As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallVega As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallTheta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallVolChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallVol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2Strike2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutVol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutVolChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutDelta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutGamma As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutVega As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutTheta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutVolume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutRatio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2Strike3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutBF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutBF2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutOI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutOIChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2TotalOI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutTotalOIPer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallOIPer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutOIPer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PCPB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PCP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PCPA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2C2C As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2P2P As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2C2P As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallCalender As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutCalender As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallBullSpread As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutBearSpread As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2Maturity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2Symbol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2futltp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallPreToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallPre2Token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallNextToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallNext2Token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutPreToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutPre2Token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutNextToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutNext2Token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallVol1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2PutVol1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col2CallTrend As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2CallStopLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col2PutTrend As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col2PutStopLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents collapsExpandPanel3 As ScrollablePanel.CollapsExpandPanel
    Private WithEvents dgv_exp3 As System.Windows.Forms.DataGridView
    Friend WithEvents col3Strike1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallOIChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallOI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallBF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallBF2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallStraddle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallRatio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallVolume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallDelta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallGamma As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallVega As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallTheta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallVolChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallVol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3Strike2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutVol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutVolChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutDelta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutGamma As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutVega As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutTheta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutVolume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutRatio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3Strike3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutBF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutBF2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutOI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutOIChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3TotalOI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutTotalOIPer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallOIPer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutOIPer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PCPB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PCP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PCPA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3C2C As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3P2P As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3C2P As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallCalender As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutCalender As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallBullSpread As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutBearSpread As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3Maturity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3Symbol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3futltp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallPreToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallPre2Token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallNextToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallNext2Token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutPreToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutPre2Token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutNextToken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutNext2Token As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3CallVol1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col3PutVol1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col3CallTrend As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col3CallStopLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col3PutTrend As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col3PutStopLoss As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
