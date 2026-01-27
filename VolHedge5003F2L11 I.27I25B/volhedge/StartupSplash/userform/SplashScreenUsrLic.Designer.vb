<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreenUsrLic
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SplashScreenUsrLic))
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblAMCText = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.Label133 = New System.Windows.Forms.Label
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnLogIn = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.GBoxUserMaster = New System.Windows.Forms.GroupBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.dtpDOBDate = New System.Windows.Forms.DateTimePicker
        Me.TxtReference = New System.Windows.Forms.TextBox
        Me.TxtFirmContactNo = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.TxtMobNo = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtCity = New System.Windows.Forms.TextBox
        Me.TxtAddress = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.TxtFirmAddress = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.TxtFirm = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.TxtName = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtPwdConfirm = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.TxtPwd = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBoxUserMaster.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.White
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(667, 496)
        Me.PictureBox3.TabIndex = 9
        Me.PictureBox3.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 317)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(182, 22)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Loading....................."
        '
        'Timer1
        '
        Me.Timer1.Interval = 300
        '
        'lblAMCText
        '
        Me.lblAMCText.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAMCText.BackColor = System.Drawing.Color.White
        Me.lblAMCText.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMCText.Location = New System.Drawing.Point(473, 317)
        Me.lblAMCText.Name = "lblAMCText"
        Me.lblAMCText.Size = New System.Drawing.Size(182, 22)
        Me.lblAMCText.TabIndex = 11
        Me.lblAMCText.Text = "AMC Text"
        Me.lblAMCText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 304)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "User Lic"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(70, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 14)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "&Password"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPassword
        '
        Me.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtPassword.BackColor = System.Drawing.SystemColors.Window
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(148, 84)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(147, 23)
        Me.txtPassword.TabIndex = 4
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'Label133
        '
        Me.Label133.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label133.AutoSize = True
        Me.Label133.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label133.Location = New System.Drawing.Point(86, 55)
        Me.Label133.Name = "Label133"
        Me.Label133.Size = New System.Drawing.Size(53, 14)
        Me.Label133.TabIndex = 1
        Me.Label133.Text = "&User Id"
        Me.Label133.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUserName
        '
        Me.txtUserName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtUserName.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.Location = New System.Drawing.Point(148, 49)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(147, 23)
        Me.txtUserName.TabIndex = 2
        Me.txtUserName.Text = "User"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.SystemColors.Control
        Me.btnExit.CausesValidation = False
        Me.btnExit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(219, 127)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(76, 27)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "E&xit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnLogIn
        '
        Me.btnLogIn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLogIn.BackColor = System.Drawing.SystemColors.Control
        Me.btnLogIn.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnLogIn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnLogIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogIn.Location = New System.Drawing.Point(137, 127)
        Me.btnLogIn.Name = "btnLogIn"
        Me.btnLogIn.Size = New System.Drawing.Size(76, 27)
        Me.btnLogIn.TabIndex = 5
        Me.btnLogIn.Text = "&LogIn"
        Me.btnLogIn.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(390, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(218, 26)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Don't have an account? "
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.Location = New System.Drawing.Point(435, 88)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 32)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "&Register"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.UseVisualStyleBackColor = False
        '
        'GBoxUserMaster
        '
        Me.GBoxUserMaster.Controls.Add(Me.Label10)
        Me.GBoxUserMaster.Controls.Add(Me.btnCancel)
        Me.GBoxUserMaster.Controls.Add(Me.btnSave)
        Me.GBoxUserMaster.Controls.Add(Me.dtpDOBDate)
        Me.GBoxUserMaster.Controls.Add(Me.TxtReference)
        Me.GBoxUserMaster.Controls.Add(Me.TxtFirmContactNo)
        Me.GBoxUserMaster.Controls.Add(Me.Label15)
        Me.GBoxUserMaster.Controls.Add(Me.TxtMobNo)
        Me.GBoxUserMaster.Controls.Add(Me.Label14)
        Me.GBoxUserMaster.Controls.Add(Me.TxtCity)
        Me.GBoxUserMaster.Controls.Add(Me.TxtAddress)
        Me.GBoxUserMaster.Controls.Add(Me.Label12)
        Me.GBoxUserMaster.Controls.Add(Me.TxtFirmAddress)
        Me.GBoxUserMaster.Controls.Add(Me.Label18)
        Me.GBoxUserMaster.Controls.Add(Me.TxtFirm)
        Me.GBoxUserMaster.Controls.Add(Me.Label11)
        Me.GBoxUserMaster.Controls.Add(Me.Label9)
        Me.GBoxUserMaster.Controls.Add(Me.Label13)
        Me.GBoxUserMaster.Controls.Add(Me.Label5)
        Me.GBoxUserMaster.Controls.Add(Me.txtEmail)
        Me.GBoxUserMaster.Controls.Add(Me.Label17)
        Me.GBoxUserMaster.Controls.Add(Me.TxtName)
        Me.GBoxUserMaster.Controls.Add(Me.Label6)
        Me.GBoxUserMaster.Controls.Add(Me.TxtPwdConfirm)
        Me.GBoxUserMaster.Controls.Add(Me.Label16)
        Me.GBoxUserMaster.Controls.Add(Me.TxtPwd)
        Me.GBoxUserMaster.Controls.Add(Me.Label7)
        Me.GBoxUserMaster.Controls.Add(Me.txtUserId)
        Me.GBoxUserMaster.Controls.Add(Me.Label8)
        Me.GBoxUserMaster.Location = New System.Drawing.Point(14, 161)
        Me.GBoxUserMaster.Margin = New System.Windows.Forms.Padding(4)
        Me.GBoxUserMaster.Name = "GBoxUserMaster"
        Me.GBoxUserMaster.Padding = New System.Windows.Forms.Padding(4)
        Me.GBoxUserMaster.Size = New System.Drawing.Size(640, 491)
        Me.GBoxUserMaster.TabIndex = 0
        Me.GBoxUserMaster.TabStop = False
        Me.GBoxUserMaster.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(1, 70)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(679, 13)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "_________________________________________________________________________________" & _
            "_______________________________"
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Location = New System.Drawing.Point(311, 441)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(62, 26)
        Me.btnCancel.TabIndex = 28
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.Location = New System.Drawing.Point(244, 441)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(62, 26)
        Me.btnSave.TabIndex = 27
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'dtpDOBDate
        '
        Me.dtpDOBDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDOBDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDOBDate.Location = New System.Drawing.Point(78, 214)
        Me.dtpDOBDate.Name = "dtpDOBDate"
        Me.dtpDOBDate.Size = New System.Drawing.Size(106, 20)
        Me.dtpDOBDate.TabIndex = 18
        '
        'TxtReference
        '
        Me.TxtReference.Location = New System.Drawing.Point(430, 167)
        Me.TxtReference.Name = "TxtReference"
        Me.TxtReference.Size = New System.Drawing.Size(132, 20)
        Me.TxtReference.TabIndex = 26
        '
        'TxtFirmContactNo
        '
        Me.TxtFirmContactNo.Location = New System.Drawing.Point(430, 141)
        Me.TxtFirmContactNo.Name = "TxtFirmContactNo"
        Me.TxtFirmContactNo.Size = New System.Drawing.Size(132, 20)
        Me.TxtFirmContactNo.TabIndex = 24
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(366, 170)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(60, 13)
        Me.Label15.TabIndex = 25
        Me.Label15.Text = "Reference."
        '
        'TxtMobNo
        '
        Me.TxtMobNo.Location = New System.Drawing.Point(78, 163)
        Me.TxtMobNo.Name = "TxtMobNo"
        Me.TxtMobNo.Size = New System.Drawing.Size(132, 20)
        Me.TxtMobNo.TabIndex = 14
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(362, 143)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 13)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "Contact No."
        '
        'TxtCity
        '
        Me.TxtCity.Location = New System.Drawing.Point(78, 139)
        Me.TxtCity.Name = "TxtCity"
        Me.TxtCity.Size = New System.Drawing.Size(132, 20)
        Me.TxtCity.TabIndex = 12
        '
        'TxtAddress
        '
        Me.TxtAddress.Location = New System.Drawing.Point(78, 115)
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(132, 20)
        Me.TxtAddress.TabIndex = 10
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(22, 167)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 13)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "*Mob No."
        '
        'TxtFirmAddress
        '
        Me.TxtFirmAddress.Location = New System.Drawing.Point(430, 115)
        Me.TxtFirmAddress.Name = "TxtFirmAddress"
        Me.TxtFirmAddress.Size = New System.Drawing.Size(132, 20)
        Me.TxtFirmAddress.TabIndex = 22
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(46, 143)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(28, 13)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "*City"
        '
        'TxtFirm
        '
        Me.TxtFirm.Location = New System.Drawing.Point(430, 89)
        Me.TxtFirm.Name = "TxtFirm"
        Me.TxtFirm.Size = New System.Drawing.Size(132, 20)
        Me.TxtFirm.TabIndex = 20
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(25, 119)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 13)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "*Address"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 218)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Date of Birth"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(381, 118)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(45, 13)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "Address"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(356, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "*Organisation"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(78, 189)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(244, 20)
        Me.txtEmail.TabIndex = 16
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(39, 193)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(35, 13)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = "E-mail"
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(78, 89)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(132, 20)
        Me.TxtName.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(35, 96)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "*Name"
        '
        'TxtPwdConfirm
        '
        Me.TxtPwdConfirm.Location = New System.Drawing.Point(328, 42)
        Me.TxtPwdConfirm.Name = "TxtPwdConfirm"
        Me.TxtPwdConfirm.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPwdConfirm.Size = New System.Drawing.Size(132, 20)
        Me.TxtPwdConfirm.TabIndex = 5
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(231, 49)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(91, 13)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "Confirm Password"
        '
        'TxtPwd
        '
        Me.TxtPwd.Location = New System.Drawing.Point(78, 44)
        Me.TxtPwd.Name = "TxtPwd"
        Me.TxtPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPwd.Size = New System.Drawing.Size(132, 20)
        Me.TxtPwd.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "*Password"
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(78, 17)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(132, 20)
        Me.txtUserId.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(29, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "*User Id"
        '
        'SplashScreenUsrLic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(667, 496)
        Me.ControlBox = False
        Me.Controls.Add(Me.GBoxUserMaster)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnLogIn)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.Label133)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblAMCText)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SplashScreenUsrLic"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBoxUserMaster.ResumeLayout(False)
        Me.GBoxUserMaster.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblAMCText As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label133 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnLogIn As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GBoxUserMaster As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDOBDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents TxtFirm As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtPwd As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TxtMobNo As System.Windows.Forms.TextBox
    Friend WithEvents TxtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtFirmAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TxtReference As System.Windows.Forms.TextBox
    Friend WithEvents TxtFirmContactNo As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TxtPwdConfirm As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TxtCity As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label

End Class
