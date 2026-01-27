<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScriptMapingFunctionality
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TxtSeriesEq = New System.Windows.Forms.TextBox()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.BtnProcessEq = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnProcessFut = New System.Windows.Forms.Button()
        Me.TxtExpiryFut = New System.Windows.Forms.TextBox()
        Me.TxtCallPutFut = New System.Windows.Forms.TextBox()
        Me.TxtInstnameFut = New System.Windows.Forms.TextBox()
        Me.TxtSecurityFut = New System.Windows.Forms.TextBox()
        Me.CmbExpiryFut = New System.Windows.Forms.ComboBox()
        Me.CmbCallPutFut = New System.Windows.Forms.ComboBox()
        Me.CmbInstnameFut = New System.Windows.Forms.ComboBox()
        Me.CmbSecurityFut = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.BtnProcessCurr = New System.Windows.Forms.Button()
        Me.TxtExpiryCurr = New System.Windows.Forms.TextBox()
        Me.TxtCallPutCurr = New System.Windows.Forms.TextBox()
        Me.TxtInstnameCurr = New System.Windows.Forms.TextBox()
        Me.TxtSecurityCurr = New System.Windows.Forms.TextBox()
        Me.CmbExpiryCurr = New System.Windows.Forms.ComboBox()
        Me.CmbCallPutCurr = New System.Windows.Forms.ComboBox()
        Me.CmbInstnameCurr = New System.Windows.Forms.ComboBox()
        Me.CmbSecurityCurr = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TxtSeriesEquity = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GroupBox1.Controls.Add(Me.TxtSeriesEq)
        Me.GroupBox1.Controls.Add(Me.CheckedListBox1)
        Me.GroupBox1.Controls.Add(Me.BtnProcessEq)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(589, 160)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'TxtSeriesEq
        '
        Me.TxtSeriesEq.Location = New System.Drawing.Point(375, 34)
        Me.TxtSeriesEq.Name = "TxtSeriesEq"
        Me.TxtSeriesEq.Size = New System.Drawing.Size(121, 20)
        Me.TxtSeriesEq.TabIndex = 20
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(9, 15)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(171, 139)
        Me.CheckedListBox1.TabIndex = 19
        '
        'BtnProcessEq
        '
        Me.BtnProcessEq.Location = New System.Drawing.Point(488, 133)
        Me.BtnProcessEq.Name = "BtnProcessEq"
        Me.BtnProcessEq.Size = New System.Drawing.Size(75, 23)
        Me.BtnProcessEq.TabIndex = 18
        Me.BtnProcessEq.Text = "Process"
        Me.BtnProcessEq.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(218, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(153, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Enter Security Name:"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(7, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(590, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "EQUITY"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(4, 184)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(590, 22)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "FUTURE"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GroupBox2.Controls.Add(Me.BtnProcessFut)
        Me.GroupBox2.Controls.Add(Me.TxtExpiryFut)
        Me.GroupBox2.Controls.Add(Me.TxtCallPutFut)
        Me.GroupBox2.Controls.Add(Me.TxtInstnameFut)
        Me.GroupBox2.Controls.Add(Me.TxtSecurityFut)
        Me.GroupBox2.Controls.Add(Me.CmbExpiryFut)
        Me.GroupBox2.Controls.Add(Me.CmbCallPutFut)
        Me.GroupBox2.Controls.Add(Me.CmbInstnameFut)
        Me.GroupBox2.Controls.Add(Me.CmbSecurityFut)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 206)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(589, 131)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'BtnProcessFut
        '
        Me.BtnProcessFut.Location = New System.Drawing.Point(491, 103)
        Me.BtnProcessFut.Name = "BtnProcessFut"
        Me.BtnProcessFut.Size = New System.Drawing.Size(75, 23)
        Me.BtnProcessFut.TabIndex = 19
        Me.BtnProcessFut.Text = "Process"
        Me.BtnProcessFut.UseVisualStyleBackColor = True
        '
        'TxtExpiryFut
        '
        Me.TxtExpiryFut.Location = New System.Drawing.Point(457, 73)
        Me.TxtExpiryFut.Name = "TxtExpiryFut"
        Me.TxtExpiryFut.Size = New System.Drawing.Size(121, 20)
        Me.TxtExpiryFut.TabIndex = 17
        '
        'TxtCallPutFut
        '
        Me.TxtCallPutFut.Location = New System.Drawing.Point(315, 73)
        Me.TxtCallPutFut.Name = "TxtCallPutFut"
        Me.TxtCallPutFut.Size = New System.Drawing.Size(121, 20)
        Me.TxtCallPutFut.TabIndex = 16
        '
        'TxtInstnameFut
        '
        Me.TxtInstnameFut.Location = New System.Drawing.Point(164, 73)
        Me.TxtInstnameFut.Name = "TxtInstnameFut"
        Me.TxtInstnameFut.Size = New System.Drawing.Size(121, 20)
        Me.TxtInstnameFut.TabIndex = 15
        '
        'TxtSecurityFut
        '
        Me.TxtSecurityFut.Location = New System.Drawing.Point(12, 73)
        Me.TxtSecurityFut.Name = "TxtSecurityFut"
        Me.TxtSecurityFut.Size = New System.Drawing.Size(121, 20)
        Me.TxtSecurityFut.TabIndex = 14
        '
        'CmbExpiryFut
        '
        Me.CmbExpiryFut.FormattingEnabled = True
        Me.CmbExpiryFut.Location = New System.Drawing.Point(457, 45)
        Me.CmbExpiryFut.Name = "CmbExpiryFut"
        Me.CmbExpiryFut.Size = New System.Drawing.Size(121, 21)
        Me.CmbExpiryFut.TabIndex = 12
        '
        'CmbCallPutFut
        '
        Me.CmbCallPutFut.FormattingEnabled = True
        Me.CmbCallPutFut.Location = New System.Drawing.Point(315, 44)
        Me.CmbCallPutFut.Name = "CmbCallPutFut"
        Me.CmbCallPutFut.Size = New System.Drawing.Size(121, 21)
        Me.CmbCallPutFut.TabIndex = 11
        '
        'CmbInstnameFut
        '
        Me.CmbInstnameFut.FormattingEnabled = True
        Me.CmbInstnameFut.Location = New System.Drawing.Point(164, 40)
        Me.CmbInstnameFut.Name = "CmbInstnameFut"
        Me.CmbInstnameFut.Size = New System.Drawing.Size(121, 21)
        Me.CmbInstnameFut.TabIndex = 10
        '
        'CmbSecurityFut
        '
        Me.CmbSecurityFut.FormattingEnabled = True
        Me.CmbSecurityFut.Location = New System.Drawing.Point(12, 40)
        Me.CmbSecurityFut.Name = "CmbSecurityFut"
        Me.CmbSecurityFut.Size = New System.Drawing.Size(121, 21)
        Me.CmbSecurityFut.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(496, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Expiry"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(343, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Call/Put/Fut"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(180, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Instrument Name"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(42, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Security"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(2, 337)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(590, 22)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "CURRENCY"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GroupBox3.Controls.Add(Me.BtnProcessCurr)
        Me.GroupBox3.Controls.Add(Me.TxtExpiryCurr)
        Me.GroupBox3.Controls.Add(Me.TxtCallPutCurr)
        Me.GroupBox3.Controls.Add(Me.TxtInstnameCurr)
        Me.GroupBox3.Controls.Add(Me.TxtSecurityCurr)
        Me.GroupBox3.Controls.Add(Me.CmbExpiryCurr)
        Me.GroupBox3.Controls.Add(Me.CmbCallPutCurr)
        Me.GroupBox3.Controls.Add(Me.CmbInstnameCurr)
        Me.GroupBox3.Controls.Add(Me.CmbSecurityCurr)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Location = New System.Drawing.Point(2, 362)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(589, 132)
        Me.GroupBox3.TabIndex = 20
        Me.GroupBox3.TabStop = False
        '
        'BtnProcessCurr
        '
        Me.BtnProcessCurr.Location = New System.Drawing.Point(493, 105)
        Me.BtnProcessCurr.Name = "BtnProcessCurr"
        Me.BtnProcessCurr.Size = New System.Drawing.Size(75, 23)
        Me.BtnProcessCurr.TabIndex = 19
        Me.BtnProcessCurr.Text = "Process"
        Me.BtnProcessCurr.UseVisualStyleBackColor = True
        '
        'TxtExpiryCurr
        '
        Me.TxtExpiryCurr.Location = New System.Drawing.Point(457, 73)
        Me.TxtExpiryCurr.Name = "TxtExpiryCurr"
        Me.TxtExpiryCurr.Size = New System.Drawing.Size(121, 20)
        Me.TxtExpiryCurr.TabIndex = 17
        '
        'TxtCallPutCurr
        '
        Me.TxtCallPutCurr.Location = New System.Drawing.Point(315, 73)
        Me.TxtCallPutCurr.Name = "TxtCallPutCurr"
        Me.TxtCallPutCurr.Size = New System.Drawing.Size(121, 20)
        Me.TxtCallPutCurr.TabIndex = 16
        '
        'TxtInstnameCurr
        '
        Me.TxtInstnameCurr.Location = New System.Drawing.Point(164, 73)
        Me.TxtInstnameCurr.Name = "TxtInstnameCurr"
        Me.TxtInstnameCurr.Size = New System.Drawing.Size(121, 20)
        Me.TxtInstnameCurr.TabIndex = 15
        '
        'TxtSecurityCurr
        '
        Me.TxtSecurityCurr.Location = New System.Drawing.Point(12, 73)
        Me.TxtSecurityCurr.Name = "TxtSecurityCurr"
        Me.TxtSecurityCurr.Size = New System.Drawing.Size(121, 20)
        Me.TxtSecurityCurr.TabIndex = 14
        '
        'CmbExpiryCurr
        '
        Me.CmbExpiryCurr.FormattingEnabled = True
        Me.CmbExpiryCurr.Location = New System.Drawing.Point(457, 45)
        Me.CmbExpiryCurr.Name = "CmbExpiryCurr"
        Me.CmbExpiryCurr.Size = New System.Drawing.Size(121, 21)
        Me.CmbExpiryCurr.TabIndex = 12
        '
        'CmbCallPutCurr
        '
        Me.CmbCallPutCurr.FormattingEnabled = True
        Me.CmbCallPutCurr.Location = New System.Drawing.Point(315, 44)
        Me.CmbCallPutCurr.Name = "CmbCallPutCurr"
        Me.CmbCallPutCurr.Size = New System.Drawing.Size(121, 21)
        Me.CmbCallPutCurr.TabIndex = 11
        '
        'CmbInstnameCurr
        '
        Me.CmbInstnameCurr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbInstnameCurr.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbInstnameCurr.FormattingEnabled = True
        Me.CmbInstnameCurr.Location = New System.Drawing.Point(164, 40)
        Me.CmbInstnameCurr.Name = "CmbInstnameCurr"
        Me.CmbInstnameCurr.Size = New System.Drawing.Size(121, 21)
        Me.CmbInstnameCurr.TabIndex = 10
        '
        'CmbSecurityCurr
        '
        Me.CmbSecurityCurr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbSecurityCurr.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbSecurityCurr.FormattingEnabled = True
        Me.CmbSecurityCurr.Location = New System.Drawing.Point(12, 40)
        Me.CmbSecurityCurr.Name = "CmbSecurityCurr"
        Me.CmbSecurityCurr.Size = New System.Drawing.Size(121, 21)
        Me.CmbSecurityCurr.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(496, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Expiry"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(343, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(65, 13)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Call/Put/Fut"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(180, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 13)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "Instrument Name"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(42, 20)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 13)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Security"
        '
        'TxtSeriesEquity
        '
        Me.TxtSeriesEquity.Location = New System.Drawing.Point(375, 34)
        Me.TxtSeriesEquity.Name = "TxtSeriesEquity"
        Me.TxtSeriesEquity.Size = New System.Drawing.Size(121, 20)
        Me.TxtSeriesEquity.TabIndex = 20
        '
        'ScriptMapingFunctionality
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 494)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ScriptMapingFunctionality"
        Me.Text = "ScriptMapingFunctionality"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnProcessEq As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnProcessFut As System.Windows.Forms.Button
    Friend WithEvents TxtExpiryFut As System.Windows.Forms.TextBox
    Friend WithEvents TxtCallPutFut As System.Windows.Forms.TextBox
    Friend WithEvents TxtInstnameFut As System.Windows.Forms.TextBox
    Friend WithEvents TxtSecurityFut As System.Windows.Forms.TextBox
    Friend WithEvents CmbExpiryFut As System.Windows.Forms.ComboBox
    Friend WithEvents CmbCallPutFut As System.Windows.Forms.ComboBox
    Friend WithEvents CmbInstnameFut As System.Windows.Forms.ComboBox
    Friend WithEvents CmbSecurityFut As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtSeriesEq As System.Windows.Forms.TextBox
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnProcessCurr As System.Windows.Forms.Button
    Friend WithEvents TxtExpiryCurr As System.Windows.Forms.TextBox
    Friend WithEvents TxtCallPutCurr As System.Windows.Forms.TextBox
    Friend WithEvents TxtInstnameCurr As System.Windows.Forms.TextBox
    Friend WithEvents TxtSecurityCurr As System.Windows.Forms.TextBox
    Friend WithEvents CmbExpiryCurr As System.Windows.Forms.ComboBox
    Friend WithEvents CmbCallPutCurr As System.Windows.Forms.ComboBox
    Friend WithEvents CmbInstnameCurr As System.Windows.Forms.ComboBox
    Friend WithEvents CmbSecurityCurr As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TxtSeriesEquity As System.Windows.Forms.TextBox
End Class
