<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AllCompanySummary
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
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle39 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle40 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle31 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle32 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle33 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle34 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle35 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle36 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle37 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle38 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.chkExpiryWise = New System.Windows.Forms.CheckBox()
        Me.BtnrefreshScenario = New System.Windows.Forms.Button()
        Me.btnScenario = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DGVSummary = New System.Windows.Forms.DataGridView()
        Me.company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NetMTM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Expense = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.delta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gamma = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.vega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.theta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.volga = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.vanna = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.deltaRs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grossmtm = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Scenario1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Scenario2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.initMargin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpoMargin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalMargin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DGVSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'chkExpiryWise
        '
        Me.chkExpiryWise.AutoSize = True
        Me.chkExpiryWise.Location = New System.Drawing.Point(93, 12)
        Me.chkExpiryWise.Name = "chkExpiryWise"
        Me.chkExpiryWise.Size = New System.Drawing.Size(78, 17)
        Me.chkExpiryWise.TabIndex = 1
        Me.chkExpiryWise.Text = "ExpiryWise"
        Me.chkExpiryWise.UseVisualStyleBackColor = True
        '
        'BtnrefreshScenario
        '
        Me.BtnrefreshScenario.Location = New System.Drawing.Point(576, 8)
        Me.BtnrefreshScenario.Name = "BtnrefreshScenario"
        Me.BtnrefreshScenario.Size = New System.Drawing.Size(100, 23)
        Me.BtnrefreshScenario.TabIndex = 36
        Me.BtnrefreshScenario.Text = "Refresh Scenario"
        Me.BtnrefreshScenario.UseVisualStyleBackColor = True
        '
        'btnScenario
        '
        Me.btnScenario.Location = New System.Drawing.Point(682, 8)
        Me.btnScenario.Name = "btnScenario"
        Me.btnScenario.Size = New System.Drawing.Size(75, 23)
        Me.btnScenario.TabIndex = 37
        Me.btnScenario.Text = "Scenario"
        Me.btnScenario.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(9, 191)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(78, 23)
        Me.Button1.TabIndex = 38
        Me.Button1.Text = "Export (F11)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DGVSummary)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnScenario)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnrefreshScenario)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkExpiryWise)
        Me.SplitContainer1.Size = New System.Drawing.Size(774, 447)
        Me.SplitContainer1.SplitterDistance = 403
        Me.SplitContainer1.TabIndex = 39
        '
        'DGVSummary
        '
        Me.DGVSummary.AllowUserToAddRows = False
        Me.DGVSummary.AllowUserToDeleteRows = False
        Me.DGVSummary.AllowUserToOrderColumns = True
        Me.DGVSummary.AllowUserToResizeRows = False
        DataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DGVSummary.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle21
        Me.DGVSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGVSummary.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DGVSummary.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVSummary.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle22
        Me.DGVSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DGVSummary.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.company, Me.CP, Me.NetMTM, Me.Expense, Me.delta, Me.gamma, Me.vega, Me.theta, Me.volga, Me.vanna, Me.deltaRs, Me.grossmtm, Me.Scenario1, Me.Scenario2, Me.initMargin, Me.ExpoMargin, Me.TotalMargin})
        DataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle39.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle39.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle39.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle39.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVSummary.DefaultCellStyle = DataGridViewCellStyle39
        Me.DGVSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVSummary.GridColor = System.Drawing.SystemColors.Control
        Me.DGVSummary.Location = New System.Drawing.Point(0, 0)
        Me.DGVSummary.MultiSelect = False
        Me.DGVSummary.Name = "DGVSummary"
        Me.DGVSummary.ReadOnly = True
        Me.DGVSummary.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle40.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle40.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle40.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle40.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle40.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle40.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVSummary.RowHeadersDefaultCellStyle = DataGridViewCellStyle40
        Me.DGVSummary.RowHeadersVisible = False
        Me.DGVSummary.RowHeadersWidth = 15
        Me.DGVSummary.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DGVSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVSummary.Size = New System.Drawing.Size(774, 403)
        Me.DGVSummary.TabIndex = 6
        '
        'company
        '
        Me.company.DataPropertyName = "company"
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.company.DefaultCellStyle = DataGridViewCellStyle23
        Me.company.HeaderText = "Security"
        Me.company.Name = "company"
        Me.company.ReadOnly = True
        Me.company.Width = 70
        '
        'CP
        '
        Me.CP.DataPropertyName = "CP"
        Me.CP.HeaderText = "CP"
        Me.CP.Name = "CP"
        Me.CP.ReadOnly = True
        Me.CP.Visible = False
        Me.CP.Width = 46
        '
        'NetMTM
        '
        Me.NetMTM.DataPropertyName = "NetMTM"
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.NetMTM.DefaultCellStyle = DataGridViewCellStyle24
        Me.NetMTM.HeaderText = "NetMTM"
        Me.NetMTM.Name = "NetMTM"
        Me.NetMTM.ReadOnly = True
        Me.NetMTM.Width = 74
        '
        'Expense
        '
        Me.Expense.DataPropertyName = "Expense"
        DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Expense.DefaultCellStyle = DataGridViewCellStyle25
        Me.Expense.HeaderText = "Expense"
        Me.Expense.Name = "Expense"
        Me.Expense.ReadOnly = True
        Me.Expense.Width = 73
        '
        'delta
        '
        Me.delta.DataPropertyName = "delta"
        DataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle26.Format = "N2"
        DataGridViewCellStyle26.NullValue = Nothing
        Me.delta.DefaultCellStyle = DataGridViewCellStyle26
        Me.delta.HeaderText = "Delta"
        Me.delta.Name = "delta"
        Me.delta.ReadOnly = True
        Me.delta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.delta.Width = 38
        '
        'gamma
        '
        Me.gamma.DataPropertyName = "gamma"
        DataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle27.Format = "N2"
        DataGridViewCellStyle27.NullValue = Nothing
        Me.gamma.DefaultCellStyle = DataGridViewCellStyle27
        Me.gamma.HeaderText = "Gamma"
        Me.gamma.Name = "gamma"
        Me.gamma.ReadOnly = True
        Me.gamma.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gamma.Width = 49
        '
        'vega
        '
        Me.vega.DataPropertyName = "vega"
        DataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle28.Format = "N2"
        DataGridViewCellStyle28.NullValue = Nothing
        Me.vega.DefaultCellStyle = DataGridViewCellStyle28
        Me.vega.HeaderText = "Vega"
        Me.vega.Name = "vega"
        Me.vega.ReadOnly = True
        Me.vega.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.vega.Width = 38
        '
        'theta
        '
        Me.theta.DataPropertyName = "theta"
        DataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle29.Format = "N2"
        DataGridViewCellStyle29.NullValue = Nothing
        Me.theta.DefaultCellStyle = DataGridViewCellStyle29
        Me.theta.HeaderText = "Theta"
        Me.theta.Name = "theta"
        Me.theta.ReadOnly = True
        Me.theta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.theta.Width = 41
        '
        'volga
        '
        Me.volga.DataPropertyName = "volga"
        DataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle30.Format = "N2"
        DataGridViewCellStyle30.NullValue = Nothing
        Me.volga.DefaultCellStyle = DataGridViewCellStyle30
        Me.volga.HeaderText = "Volga"
        Me.volga.Name = "volga"
        Me.volga.ReadOnly = True
        Me.volga.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.volga.Width = 40
        '
        'vanna
        '
        Me.vanna.DataPropertyName = "vanna"
        DataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle31.Format = "N2"
        DataGridViewCellStyle31.NullValue = Nothing
        Me.vanna.DefaultCellStyle = DataGridViewCellStyle31
        Me.vanna.HeaderText = "Vanna"
        Me.vanna.Name = "vanna"
        Me.vanna.ReadOnly = True
        Me.vanna.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.vanna.Width = 44
        '
        'deltaRs
        '
        Me.deltaRs.DataPropertyName = "deltaRS"
        DataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle32.Format = "N2"
        Me.deltaRs.DefaultCellStyle = DataGridViewCellStyle32
        Me.deltaRs.HeaderText = "Delta (Rs)"
        Me.deltaRs.Name = "deltaRs"
        Me.deltaRs.ReadOnly = True
        Me.deltaRs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.deltaRs.Width = 60
        '
        'grossmtm
        '
        Me.grossmtm.DataPropertyName = "grossmtm"
        DataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle33.Format = "N2"
        DataGridViewCellStyle33.NullValue = Nothing
        Me.grossmtm.DefaultCellStyle = DataGridViewCellStyle33
        Me.grossmtm.HeaderText = "Gross MTM"
        Me.grossmtm.Name = "grossmtm"
        Me.grossmtm.ReadOnly = True
        Me.grossmtm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.grossmtm.Width = 68
        '
        'Scenario1
        '
        Me.Scenario1.DataPropertyName = "Scenario1"
        DataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Scenario1.DefaultCellStyle = DataGridViewCellStyle34
        Me.Scenario1.HeaderText = "Scenario1"
        Me.Scenario1.Name = "Scenario1"
        Me.Scenario1.ReadOnly = True
        Me.Scenario1.Width = 80
        '
        'Scenario2
        '
        Me.Scenario2.DataPropertyName = "Scenario2"
        DataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Scenario2.DefaultCellStyle = DataGridViewCellStyle35
        Me.Scenario2.HeaderText = "Scenario2"
        Me.Scenario2.Name = "Scenario2"
        Me.Scenario2.ReadOnly = True
        Me.Scenario2.Width = 80
        '
        'initMargin
        '
        Me.initMargin.DataPropertyName = "initMargin"
        DataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.initMargin.DefaultCellStyle = DataGridViewCellStyle36
        Me.initMargin.HeaderText = "initMargin"
        Me.initMargin.Name = "initMargin"
        Me.initMargin.ReadOnly = True
        Me.initMargin.Width = 77
        '
        'ExpoMargin
        '
        Me.ExpoMargin.DataPropertyName = "ExpoMargin"
        DataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ExpoMargin.DefaultCellStyle = DataGridViewCellStyle37
        Me.ExpoMargin.HeaderText = "ExpoMargin"
        Me.ExpoMargin.Name = "ExpoMargin"
        Me.ExpoMargin.ReadOnly = True
        Me.ExpoMargin.Width = 88
        '
        'TotalMargin
        '
        Me.TotalMargin.DataPropertyName = "TotalMargin"
        DataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.TotalMargin.DefaultCellStyle = DataGridViewCellStyle38
        Me.TotalMargin.HeaderText = "TotalMargin"
        Me.TotalMargin.Name = "TotalMargin"
        Me.TotalMargin.ReadOnly = True
        Me.TotalMargin.Width = 88
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(9, 8)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(78, 23)
        Me.Button2.TabIndex = 39
        Me.Button2.Text = "Export (F11)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'AllCompanySummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(774, 447)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "AllCompanySummary"
        Me.Text = "Summary"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DGVSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents chkExpiryWise As System.Windows.Forms.CheckBox
    Friend WithEvents BtnrefreshScenario As System.Windows.Forms.Button
    Friend WithEvents btnScenario As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Public WithEvents DGVSummary As System.Windows.Forms.DataGridView
    Friend WithEvents company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NetMTM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Expense As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents delta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gamma As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vega As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents theta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents volga As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vanna As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents deltaRs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grossmtm As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Scenario1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Scenario2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents initMargin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExpoMargin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalMargin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
