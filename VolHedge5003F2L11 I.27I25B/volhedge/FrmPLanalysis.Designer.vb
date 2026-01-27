<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPLanalysis
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
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.grdtrad = New System.Windows.Forms.DataGridView()
        Me.company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.delta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.vega = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.theta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TodayMTM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Diff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.curGrossMTM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPrevSavedOn = New System.Windows.Forms.TextBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.grdtrad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.grdtrad)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtPrevSavedOn)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Size = New System.Drawing.Size(927, 384)
        Me.SplitContainer1.SplitterDistance = 339
        Me.SplitContainer1.TabIndex = 8
        '
        'grdtrad
        '
        Me.grdtrad.AllowUserToAddRows = False
        Me.grdtrad.AllowUserToDeleteRows = False
        Me.grdtrad.AllowUserToOrderColumns = True
        Me.grdtrad.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdtrad.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdtrad.BackgroundColor = System.Drawing.Color.Silver
        Me.grdtrad.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdtrad.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdtrad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdtrad.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.company, Me.delta, Me.vega, Me.theta, Me.Total, Me.TodayMTM, Me.Diff, Me.curGrossMTM})
        Me.grdtrad.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdtrad.GridColor = System.Drawing.Color.Silver
        Me.grdtrad.Location = New System.Drawing.Point(0, 0)
        Me.grdtrad.MultiSelect = False
        Me.grdtrad.Name = "grdtrad"
        Me.grdtrad.ReadOnly = True
        Me.grdtrad.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdtrad.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.grdtrad.RowHeadersVisible = False
        Me.grdtrad.RowHeadersWidth = 15
        Me.grdtrad.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdtrad.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdtrad.Size = New System.Drawing.Size(927, 339)
        Me.grdtrad.TabIndex = 4
        '
        'company
        '
        Me.company.DataPropertyName = "company"
        Me.company.HeaderText = "Security"
        Me.company.Name = "company"
        Me.company.ReadOnly = True
        Me.company.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.company.Width = 150
        '
        'delta
        '
        Me.delta.DataPropertyName = "delta"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.delta.DefaultCellStyle = DataGridViewCellStyle3
        Me.delta.HeaderText = "Delta"
        Me.delta.Name = "delta"
        Me.delta.ReadOnly = True
        Me.delta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.delta.Width = 110
        '
        'vega
        '
        Me.vega.DataPropertyName = "vega"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.vega.DefaultCellStyle = DataGridViewCellStyle4
        Me.vega.HeaderText = "Vega"
        Me.vega.Name = "vega"
        Me.vega.ReadOnly = True
        Me.vega.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.vega.Width = 110
        '
        'theta
        '
        Me.theta.DataPropertyName = "theta"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.theta.DefaultCellStyle = DataGridViewCellStyle5
        Me.theta.HeaderText = "Theta"
        Me.theta.Name = "theta"
        Me.theta.ReadOnly = True
        Me.theta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.theta.Width = 110
        '
        'Total
        '
        Me.Total.DataPropertyName = "Total"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.Total.DefaultCellStyle = DataGridViewCellStyle6
        Me.Total.HeaderText = "Total"
        Me.Total.Name = "Total"
        Me.Total.ReadOnly = True
        Me.Total.Width = 150
        '
        'TodayMTM
        '
        Me.TodayMTM.DataPropertyName = "TodayMTM"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        Me.TodayMTM.DefaultCellStyle = DataGridViewCellStyle7
        Me.TodayMTM.HeaderText = "TodayMTM"
        Me.TodayMTM.Name = "TodayMTM"
        Me.TodayMTM.ReadOnly = True
        Me.TodayMTM.Visible = False
        '
        'Diff
        '
        Me.Diff.DataPropertyName = "Diff"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N2"
        Me.Diff.DefaultCellStyle = DataGridViewCellStyle8
        Me.Diff.HeaderText = "Diff"
        Me.Diff.Name = "Diff"
        Me.Diff.ReadOnly = True
        Me.Diff.Visible = False
        Me.Diff.Width = 110
        '
        'curGrossMTM
        '
        Me.curGrossMTM.DataPropertyName = "curGrossMTM"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        Me.curGrossMTM.DefaultCellStyle = DataGridViewCellStyle9
        Me.curGrossMTM.HeaderText = "GrossMTM"
        Me.curGrossMTM.Name = "curGrossMTM"
        Me.curGrossMTM.ReadOnly = True
        Me.curGrossMTM.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(73, 27)
        Me.Button1.TabIndex = 35
        Me.Button1.Text = "Export(F11)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(290, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Prev. Saved On:"
        '
        'txtPrevSavedOn
        '
        Me.txtPrevSavedOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrevSavedOn.Enabled = False
        Me.txtPrevSavedOn.Location = New System.Drawing.Point(387, 13)
        Me.txtPrevSavedOn.Name = "txtPrevSavedOn"
        Me.txtPrevSavedOn.Size = New System.Drawing.Size(122, 20)
        Me.txtPrevSavedOn.TabIndex = 37
        '
        'FrmPLanalysis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(927, 384)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "FrmPLanalysis"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Profit & Loss  Analysis"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.grdtrad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Public WithEvents grdtrad As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents delta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vega As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents theta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TodayMTM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Diff As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents curGrossMTM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtPrevSavedOn As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
