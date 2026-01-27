<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserMaster
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
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dtpExpDate = New System.Windows.Forms.DateTimePicker
        Me.TxtBranch = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.btnReSetPass = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtPwd = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.ChkUserAllowed = New System.Windows.Forms.CheckBox
        Me.ChkUserLimited = New System.Windows.Forms.CheckBox
        Me.ChkUserAdmin = New System.Windows.Forms.CheckBox
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.DGUserMaster = New System.Windows.Forms.DataGridView
        Me.UserId = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Pwd = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Username = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Branch = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Product = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Allowed = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Limited = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ExDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IsAdmin = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtSearchUserId = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtSearchUserName = New System.Windows.Forms.TextBox
        Me.txtSearchBranch = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TableLayoutPanel5.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DGUserMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.36496!))
        Me.TableLayoutPanel5.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(617, 3)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(271, 588)
        Me.TableLayoutPanel5.TabIndex = 22
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnNew)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 556)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(265, 29)
        Me.Panel1.TabIndex = 5
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnExit.Location = New System.Drawing.Point(201, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(62, 26)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "E&xit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Location = New System.Drawing.Point(134, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(62, 26)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.Location = New System.Drawing.Point(67, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(62, 26)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnNew.Location = New System.Drawing.Point(0, 3)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(62, 26)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = "&New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtpExpDate)
        Me.GroupBox1.Controls.Add(Me.TxtBranch)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtUserName)
        Me.GroupBox1.Controls.Add(Me.btnReSetPass)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TxtPwd)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ChkUserAllowed)
        Me.GroupBox1.Controls.Add(Me.ChkUserLimited)
        Me.GroupBox1.Controls.Add(Me.ChkUserAdmin)
        Me.GroupBox1.Controls.Add(Me.txtUserId)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(4, 4)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(263, 545)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'dtpExpDate
        '
        Me.dtpExpDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpDate.Location = New System.Drawing.Point(104, 152)
        Me.dtpExpDate.Name = "dtpExpDate"
        Me.dtpExpDate.Size = New System.Drawing.Size(106, 21)
        Me.dtpExpDate.TabIndex = 10
        '
        'TxtBranch
        '
        Me.TxtBranch.Location = New System.Drawing.Point(78, 98)
        Me.TxtBranch.Name = "TxtBranch"
        Me.TxtBranch.Size = New System.Drawing.Size(132, 21)
        Me.TxtBranch.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Branch"
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(78, 71)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(132, 21)
        Me.txtUserName.TabIndex = 5
        '
        'btnReSetPass
        '
        Me.btnReSetPass.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReSetPass.Location = New System.Drawing.Point(216, 44)
        Me.btnReSetPass.Name = "btnReSetPass"
        Me.btnReSetPass.Size = New System.Drawing.Size(45, 21)
        Me.btnReSetPass.TabIndex = 1
        Me.btnReSetPass.Text = "Reset"
        Me.btnReSetPass.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "User Name"
        '
        'TxtPwd
        '
        Me.TxtPwd.Location = New System.Drawing.Point(78, 44)
        Me.TxtPwd.Name = "TxtPwd"
        Me.TxtPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPwd.Size = New System.Drawing.Size(132, 21)
        Me.TxtPwd.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Password"
        '
        'ChkUserAllowed
        '
        Me.ChkUserAllowed.AutoSize = True
        Me.ChkUserAllowed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkUserAllowed.Location = New System.Drawing.Point(24, 129)
        Me.ChkUserAllowed.Name = "ChkUserAllowed"
        Me.ChkUserAllowed.Size = New System.Drawing.Size(70, 17)
        Me.ChkUserAllowed.TabIndex = 8
        Me.ChkUserAllowed.Text = "Allowed"
        Me.ChkUserAllowed.UseVisualStyleBackColor = True
        '
        'ChkUserLimited
        '
        Me.ChkUserLimited.AutoSize = True
        Me.ChkUserLimited.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkUserLimited.Location = New System.Drawing.Point(27, 154)
        Me.ChkUserLimited.Name = "ChkUserLimited"
        Me.ChkUserLimited.Size = New System.Drawing.Size(67, 17)
        Me.ChkUserLimited.TabIndex = 9
        Me.ChkUserLimited.Text = "Limited"
        Me.ChkUserLimited.UseVisualStyleBackColor = True
        '
        'ChkUserAdmin
        '
        Me.ChkUserAdmin.AutoSize = True
        Me.ChkUserAdmin.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkUserAdmin.Location = New System.Drawing.Point(23, 178)
        Me.ChkUserAdmin.Name = "ChkUserAdmin"
        Me.ChkUserAdmin.Size = New System.Drawing.Size(73, 17)
        Me.ChkUserAdmin.TabIndex = 11
        Me.ChkUserAdmin.Text = "IsAdmin"
        Me.ChkUserAdmin.UseVisualStyleBackColor = True
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(78, 17)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(132, 21)
        Me.txtUserId.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "User Id"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.DGUserMaster, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox2, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.05442!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.94558!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(608, 588)
        Me.TableLayoutPanel2.TabIndex = 19
        '
        'DGUserMaster
        '
        Me.DGUserMaster.AllowUserToAddRows = False
        Me.DGUserMaster.AllowUserToDeleteRows = False
        Me.DGUserMaster.ColumnHeadersHeight = 20
        Me.DGUserMaster.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.UserId, Me.Pwd, Me.Username, Me.Branch, Me.Product, Me.Allowed, Me.Limited, Me.ExDate, Me.Status, Me.IsAdmin})
        Me.DGUserMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGUserMaster.EnableHeadersVisualStyles = False
        Me.DGUserMaster.Location = New System.Drawing.Point(3, 67)
        Me.DGUserMaster.Name = "DGUserMaster"
        Me.DGUserMaster.ReadOnly = True
        Me.DGUserMaster.RowHeadersWidth = 21
        Me.DGUserMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGUserMaster.Size = New System.Drawing.Size(602, 518)
        Me.DGUserMaster.StandardTab = True
        Me.DGUserMaster.TabIndex = 6
        '
        'UserId
        '
        Me.UserId.DataPropertyName = "F2"
        Me.UserId.HeaderText = "UserId"
        Me.UserId.Name = "UserId"
        Me.UserId.ReadOnly = True
        '
        'Pwd
        '
        Me.Pwd.DataPropertyName = "F3"
        Me.Pwd.HeaderText = "Pwd"
        Me.Pwd.Name = "Pwd"
        Me.Pwd.ReadOnly = True
        Me.Pwd.Visible = False
        '
        'Username
        '
        Me.Username.DataPropertyName = "F4"
        Me.Username.HeaderText = "Username"
        Me.Username.Name = "Username"
        Me.Username.ReadOnly = True
        '
        'Branch
        '
        Me.Branch.DataPropertyName = "F5"
        Me.Branch.HeaderText = "Branch"
        Me.Branch.Name = "Branch"
        Me.Branch.ReadOnly = True
        '
        'Product
        '
        Me.Product.DataPropertyName = "F6"
        Me.Product.HeaderText = "Product"
        Me.Product.Name = "Product"
        Me.Product.ReadOnly = True
        '
        'Allowed
        '
        Me.Allowed.DataPropertyName = "F7"
        Me.Allowed.HeaderText = "Allowed"
        Me.Allowed.Name = "Allowed"
        Me.Allowed.ReadOnly = True
        '
        'Limited
        '
        Me.Limited.DataPropertyName = "F8"
        Me.Limited.HeaderText = "Limited"
        Me.Limited.Name = "Limited"
        Me.Limited.ReadOnly = True
        '
        'ExDate
        '
        Me.ExDate.DataPropertyName = "F9"
        Me.ExDate.HeaderText = "ExDate"
        Me.ExDate.Name = "ExDate"
        Me.ExDate.ReadOnly = True
        '
        'Status
        '
        Me.Status.DataPropertyName = "F10"
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Visible = False
        '
        'IsAdmin
        '
        Me.IsAdmin.DataPropertyName = "F11"
        Me.IsAdmin.HeaderText = "IsAdmin"
        Me.IsAdmin.Name = "IsAdmin"
        Me.IsAdmin.ReadOnly = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtSearchUserId)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtSearchUserName)
        Me.GroupBox2.Controls.Add(Me.txtSearchBranch)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(602, 58)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Searching"
        '
        'txtSearchUserId
        '
        Me.txtSearchUserId.Location = New System.Drawing.Point(8, 35)
        Me.txtSearchUserId.Name = "txtSearchUserId"
        Me.txtSearchUserId.Size = New System.Drawing.Size(124, 21)
        Me.txtSearchUserId.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(319, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Branch "
        '
        'txtSearchUserName
        '
        Me.txtSearchUserName.Location = New System.Drawing.Point(138, 35)
        Me.txtSearchUserName.Name = "txtSearchUserName"
        Me.txtSearchUserName.Size = New System.Drawing.Size(178, 21)
        Me.txtSearchUserName.TabIndex = 3
        '
        'txtSearchBranch
        '
        Me.txtSearchBranch.Location = New System.Drawing.Point(322, 35)
        Me.txtSearchBranch.Name = "txtSearchBranch"
        Me.txtSearchBranch.Size = New System.Drawing.Size(143, 21)
        Me.txtSearchBranch.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(5, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "User id"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(135, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "User Name"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 277.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel5, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(891, 594)
        Me.TableLayoutPanel3.TabIndex = 20
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "UserId"
        Me.DataGridViewTextBoxColumn1.HeaderText = "UserId"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Pwd"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Pwd"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Visible = False
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Username"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Username"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Branch"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Branch"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Product"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Product"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Allowed"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Allowed"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Limited"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Limited"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "ExDate"
        Me.DataGridViewTextBoxColumn8.HeaderText = "ExDate"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Visible = False
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "IsAdmin"
        Me.DataGridViewTextBoxColumn10.HeaderText = "IsAdmin"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        '
        'FrmUserMaster
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(891, 594)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "FrmUserMaster"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Master "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.DGUserMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpExpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents TxtBranch As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents btnReSetPass As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtPwd As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ChkUserAllowed As System.Windows.Forms.CheckBox
    Friend WithEvents ChkUserLimited As System.Windows.Forms.CheckBox
    Friend WithEvents ChkUserAdmin As System.Windows.Forms.CheckBox
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DGUserMaster As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearchUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtSearchUserName As System.Windows.Forms.TextBox
    Friend WithEvents txtSearchBranch As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents UserId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pwd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Username As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Branch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Product As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Allowed As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Limited As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsAdmin As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
