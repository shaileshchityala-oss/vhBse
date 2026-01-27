
Public Class frmappSetting
    Inherits System.Windows.Forms.Form
#Region "Init"

    Dim mMode As String
    Const dColSrNo As Int16 = 0
    Const dColUid As Int16 = 1
    Const dColSettingName As Int16 = 2
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtbackuppath As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtspanpath As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtport As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents rbavg As System.Windows.Forms.RadioButton
    Friend WithEvents rbfifo As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdsave As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cmbvega As System.Windows.Forms.ComboBox
    Friend WithEvents cmbgamma As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmbdelta As System.Windows.Forms.ComboBox
    Friend WithEvents cmbtheta As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cmbdeltaval As System.Windows.Forms.ComboBox
    Friend WithEvents cmbthetaval As System.Windows.Forms.ComboBox
    Friend WithEvents cmbvegaval As System.Windows.Forms.ComboBox
    Friend WithEvents cmbgammaval As System.Windows.Forms.ComboBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbsquaremtm As System.Windows.Forms.ComboBox
    Friend WithEvents cmbexp As System.Windows.Forms.ComboBox
    Friend WithEvents cmbnetmtm As System.Windows.Forms.ComboBox
    Friend WithEvents cmbgrossmtm As System.Windows.Forms.ComboBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents rbequity As System.Windows.Forms.RadioButton
    Friend WithEvents rbfut As System.Windows.Forms.RadioButton
    Friend WithEvents cmbalert As System.Windows.Forms.ComboBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtnoday As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Const dColSettingKey As Int16 = 3
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtudpport As System.Windows.Forms.TextBox
    Friend WithEvents txtrateofint As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents txtudpip3 As System.Windows.Forms.TextBox
    Friend WithEvents txtipaddress3 As System.Windows.Forms.TextBox
    Friend WithEvents txtudpip2 As System.Windows.Forms.TextBox
    Friend WithEvents txtipaddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtudpip1 As System.Windows.Forms.TextBox
    Friend WithEvents txtipaddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtudpip4 As System.Windows.Forms.TextBox
    Friend WithEvents txtipaddress4 As System.Windows.Forms.TextBox
    Friend WithEvents cmbunreal As System.Windows.Forms.ComboBox
    Friend WithEvents cmbreal As System.Windows.Forms.ComboBox
    Friend WithEvents cmbexmarg As System.Windows.Forms.ComboBox
    Friend WithEvents cmbinmarg As System.Windows.Forms.ComboBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents cmbtot As System.Windows.Forms.ComboBox
    Friend WithEvents cmbequity As System.Windows.Forms.ComboBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents cmbzero As System.Windows.Forms.ComboBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents txtpvol As System.Windows.Forms.TextBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents txtcvol As System.Windows.Forms.TextBox
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents txtbtimer As System.Windows.Forms.TextBox
    Friend WithEvents dtpmaturity_far_month As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents chkMessage As System.Windows.Forms.CheckBox
    Friend WithEvents chkaddanov As System.Windows.Forms.CheckBox
    Friend WithEvents txtSpanTimer As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
#End Region

    Dim dtable_set As DataTable
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmappSetting))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtbackuppath = New System.Windows.Forms.TextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.txtrateofint = New System.Windows.Forms.MaskedTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtspanpath = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label50 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtudpport = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtudpip4 = New System.Windows.Forms.TextBox
        Me.txtudpip3 = New System.Windows.Forms.TextBox
        Me.txtipaddress4 = New System.Windows.Forms.TextBox
        Me.txtipaddress3 = New System.Windows.Forms.TextBox
        Me.txtudpip2 = New System.Windows.Forms.TextBox
        Me.txtipaddress2 = New System.Windows.Forms.TextBox
        Me.txtudpip1 = New System.Windows.Forms.TextBox
        Me.txtipaddress1 = New System.Windows.Forms.TextBox
        Me.txtport = New System.Windows.Forms.TextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.rbavg = New System.Windows.Forms.RadioButton
        Me.rbfifo = New System.Windows.Forms.RadioButton
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.txtnoday = New System.Windows.Forms.TextBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkaddanov = New System.Windows.Forms.CheckBox
        Me.chkMessage = New System.Windows.Forms.CheckBox
        Me.dtpmaturity_far_month = New System.Windows.Forms.DateTimePicker
        Me.cmdexit = New System.Windows.Forms.Button
        Me.cmdsave = New System.Windows.Forms.Button
        Me.Label80 = New System.Windows.Forms.Label
        Me.Label81 = New System.Windows.Forms.Label
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.txtSpanTimer = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label73 = New System.Windows.Forms.Label
        Me.Label74 = New System.Windows.Forms.Label
        Me.txtbtimer = New System.Windows.Forms.TextBox
        Me.cmbtot = New System.Windows.Forms.ComboBox
        Me.cmbequity = New System.Windows.Forms.ComboBox
        Me.Label61 = New System.Windows.Forms.Label
        Me.Label62 = New System.Windows.Forms.Label
        Me.Label63 = New System.Windows.Forms.Label
        Me.Label64 = New System.Windows.Forms.Label
        Me.cmbunreal = New System.Windows.Forms.ComboBox
        Me.cmbreal = New System.Windows.Forms.ComboBox
        Me.cmbexmarg = New System.Windows.Forms.ComboBox
        Me.cmbinmarg = New System.Windows.Forms.ComboBox
        Me.Label53 = New System.Windows.Forms.Label
        Me.Label54 = New System.Windows.Forms.Label
        Me.Label55 = New System.Windows.Forms.Label
        Me.Label56 = New System.Windows.Forms.Label
        Me.Label57 = New System.Windows.Forms.Label
        Me.Label58 = New System.Windows.Forms.Label
        Me.Label59 = New System.Windows.Forms.Label
        Me.Label60 = New System.Windows.Forms.Label
        Me.cmbdeltaval = New System.Windows.Forms.ComboBox
        Me.cmbdelta = New System.Windows.Forms.ComboBox
        Me.cmbthetaval = New System.Windows.Forms.ComboBox
        Me.cmbtheta = New System.Windows.Forms.ComboBox
        Me.cmbvegaval = New System.Windows.Forms.ComboBox
        Me.cmbvega = New System.Windows.Forms.ComboBox
        Me.cmbgammaval = New System.Windows.Forms.ComboBox
        Me.cmbgamma = New System.Windows.Forms.ComboBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmbMode = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbzero = New System.Windows.Forms.ComboBox
        Me.Label65 = New System.Windows.Forms.Label
        Me.Label66 = New System.Windows.Forms.Label
        Me.cmbsquaremtm = New System.Windows.Forms.ComboBox
        Me.cmbexp = New System.Windows.Forms.ComboBox
        Me.cmbnetmtm = New System.Windows.Forms.ComboBox
        Me.cmbgrossmtm = New System.Windows.Forms.ComboBox
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label46 = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.Label48 = New System.Windows.Forms.Label
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.Label72 = New System.Windows.Forms.Label
        Me.Label71 = New System.Windows.Forms.Label
        Me.Label69 = New System.Windows.Forms.Label
        Me.Label70 = New System.Windows.Forms.Label
        Me.txtpvol = New System.Windows.Forms.TextBox
        Me.Label67 = New System.Windows.Forms.Label
        Me.Label68 = New System.Windows.Forms.Label
        Me.txtcvol = New System.Windows.Forms.TextBox
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.rbequity = New System.Windows.Forms.RadioButton
        Me.rbfut = New System.Windows.Forms.RadioButton
        Me.cmbalert = New System.Windows.Forms.ComboBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtbackuppath)
        Me.GroupBox1.Controls.Add(Me.TextBox3)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox1.Location = New System.Drawing.Point(1, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(686, 47)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Backup Setting"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(621, 18)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(61, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Browse..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(152, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(12, 14)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = ":"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(61, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 14)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Backup Path"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtbackuppath
        '
        Me.txtbackuppath.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtbackuppath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtbackuppath.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbackuppath.Location = New System.Drawing.Point(170, 18)
        Me.txtbackuppath.Name = "txtbackuppath"
        Me.txtbackuppath.ReadOnly = True
        Me.txtbackuppath.Size = New System.Drawing.Size(402, 23)
        Me.txtbackuppath.TabIndex = 0
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.SystemColors.HighlightText
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(199, -185)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(52, 23)
        Me.TextBox3.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.txtrateofint)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtspanpath)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox2.Location = New System.Drawing.Point(1, 45)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(686, 71)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(621, 41)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(61, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Browse..."
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtrateofint
        '
        Me.txtrateofint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtrateofint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrateofint.Location = New System.Drawing.Point(168, 13)
        Me.txtrateofint.Mask = "#.##"
        Me.txtrateofint.Name = "txtrateofint"
        Me.txtrateofint.Size = New System.Drawing.Size(48, 23)
        Me.txtrateofint.TabIndex = 0
        Me.txtrateofint.Text = "000"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(153, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(12, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = ":"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(154, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(12, 14)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = ":"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(81, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Span Path"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtspanpath
        '
        Me.txtspanpath.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtspanpath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtspanpath.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtspanpath.Location = New System.Drawing.Point(167, 41)
        Me.txtspanpath.Name = "txtspanpath"
        Me.txtspanpath.ReadOnly = True
        Me.txtspanpath.Size = New System.Drawing.Size(403, 23)
        Me.txtspanpath.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(44, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(109, 14)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Rate Of Interest"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.GroupBox3.Controls.Add(Me.Label50)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.Label49)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Label41)
        Me.GroupBox3.Controls.Add(Me.Label38)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.txtudpport)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.txtudpip4)
        Me.GroupBox3.Controls.Add(Me.txtudpip3)
        Me.GroupBox3.Controls.Add(Me.txtipaddress4)
        Me.GroupBox3.Controls.Add(Me.txtipaddress3)
        Me.GroupBox3.Controls.Add(Me.txtudpip2)
        Me.GroupBox3.Controls.Add(Me.txtipaddress2)
        Me.GroupBox3.Controls.Add(Me.txtudpip1)
        Me.GroupBox3.Controls.Add(Me.txtipaddress1)
        Me.GroupBox3.Controls.Add(Me.txtport)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox3.Location = New System.Drawing.Point(1, 117)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(686, 80)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "TCP Server Setting"
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(477, 19)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(12, 14)
        Me.Label50.TabIndex = 1
        Me.Label50.Text = ":"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(154, 23)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(12, 14)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = ":"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(477, 51)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(12, 14)
        Me.Label49.TabIndex = 1
        Me.Label49.Text = ":"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(154, 51)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(12, 14)
        Me.Label16.TabIndex = 1
        Me.Label16.Text = ":"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(349, 19)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(128, 14)
        Me.Label41.TabIndex = 0
        Me.Label41.Text = "CM UDP IP Address"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(420, 49)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(54, 14)
        Me.Label38.TabIndex = 0
        Me.Label38.Text = "Port No"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(23, 23)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(126, 14)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "FO UDP IP Address"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtudpport
        '
        Me.txtudpport.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtudpport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtudpport.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtudpport.Location = New System.Drawing.Point(491, 44)
        Me.txtudpport.Name = "txtudpport"
        Me.txtudpport.Size = New System.Drawing.Size(52, 23)
        Me.txtudpport.TabIndex = 9
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(95, 51)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(54, 14)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Port No"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtudpip4
        '
        Me.txtudpip4.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtudpip4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtudpip4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtudpip4.Location = New System.Drawing.Point(602, 16)
        Me.txtudpip4.MaxLength = 3
        Me.txtudpip4.Name = "txtudpip4"
        Me.txtudpip4.Size = New System.Drawing.Size(34, 23)
        Me.txtudpip4.TabIndex = 8
        Me.txtudpip4.Text = "000"
        '
        'txtudpip3
        '
        Me.txtudpip3.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtudpip3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtudpip3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtudpip3.Location = New System.Drawing.Point(565, 16)
        Me.txtudpip3.MaxLength = 3
        Me.txtudpip3.Name = "txtudpip3"
        Me.txtudpip3.Size = New System.Drawing.Size(34, 23)
        Me.txtudpip3.TabIndex = 7
        Me.txtudpip3.Text = "000"
        '
        'txtipaddress4
        '
        Me.txtipaddress4.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtipaddress4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtipaddress4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtipaddress4.Location = New System.Drawing.Point(280, 19)
        Me.txtipaddress4.MaxLength = 3
        Me.txtipaddress4.Name = "txtipaddress4"
        Me.txtipaddress4.Size = New System.Drawing.Size(34, 23)
        Me.txtipaddress4.TabIndex = 3
        Me.txtipaddress4.Text = "000"
        '
        'txtipaddress3
        '
        Me.txtipaddress3.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtipaddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtipaddress3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtipaddress3.Location = New System.Drawing.Point(243, 19)
        Me.txtipaddress3.MaxLength = 3
        Me.txtipaddress3.Name = "txtipaddress3"
        Me.txtipaddress3.Size = New System.Drawing.Size(34, 23)
        Me.txtipaddress3.TabIndex = 2
        Me.txtipaddress3.Text = "000"
        '
        'txtudpip2
        '
        Me.txtudpip2.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtudpip2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtudpip2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtudpip2.Location = New System.Drawing.Point(528, 16)
        Me.txtudpip2.MaxLength = 3
        Me.txtudpip2.Name = "txtudpip2"
        Me.txtudpip2.Size = New System.Drawing.Size(34, 23)
        Me.txtudpip2.TabIndex = 6
        Me.txtudpip2.Text = "000"
        '
        'txtipaddress2
        '
        Me.txtipaddress2.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtipaddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtipaddress2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtipaddress2.Location = New System.Drawing.Point(206, 19)
        Me.txtipaddress2.MaxLength = 3
        Me.txtipaddress2.Name = "txtipaddress2"
        Me.txtipaddress2.Size = New System.Drawing.Size(34, 23)
        Me.txtipaddress2.TabIndex = 1
        Me.txtipaddress2.Text = "000"
        '
        'txtudpip1
        '
        Me.txtudpip1.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtudpip1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtudpip1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtudpip1.Location = New System.Drawing.Point(491, 16)
        Me.txtudpip1.MaxLength = 3
        Me.txtudpip1.Name = "txtudpip1"
        Me.txtudpip1.Size = New System.Drawing.Size(34, 23)
        Me.txtudpip1.TabIndex = 5
        Me.txtudpip1.Text = "000"
        '
        'txtipaddress1
        '
        Me.txtipaddress1.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtipaddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtipaddress1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtipaddress1.Location = New System.Drawing.Point(169, 20)
        Me.txtipaddress1.MaxLength = 3
        Me.txtipaddress1.Name = "txtipaddress1"
        Me.txtipaddress1.Size = New System.Drawing.Size(34, 23)
        Me.txtipaddress1.TabIndex = 0
        Me.txtipaddress1.Text = "000"
        '
        'txtport
        '
        Me.txtport.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtport.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtport.Location = New System.Drawing.Point(169, 47)
        Me.txtport.Name = "txtport"
        Me.txtport.Size = New System.Drawing.Size(52, 23)
        Me.txtport.TabIndex = 4
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.GroupBox4.Controls.Add(Me.GroupBox5)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.Label37)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.Label35)
        Me.GroupBox4.Controls.Add(Me.txtnoday)
        Me.GroupBox4.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox4.Location = New System.Drawing.Point(1, 195)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(686, 48)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.rbavg)
        Me.GroupBox5.Controls.Add(Me.rbfifo)
        Me.GroupBox5.Location = New System.Drawing.Point(169, 5)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(204, 36)
        Me.GroupBox5.TabIndex = 0
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Visible = False
        '
        'rbavg
        '
        Me.rbavg.AutoSize = True
        Me.rbavg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbavg.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbavg.Location = New System.Drawing.Point(103, 11)
        Me.rbavg.Name = "rbavg"
        Me.rbavg.Size = New System.Drawing.Size(79, 18)
        Me.rbavg.TabIndex = 1
        Me.rbavg.TabStop = True
        Me.rbavg.Text = "Average"
        Me.rbavg.UseVisualStyleBackColor = True
        '
        'rbfifo
        '
        Me.rbfifo.AutoSize = True
        Me.rbfifo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbfifo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbfifo.Location = New System.Drawing.Point(9, 11)
        Me.rbfifo.Name = "rbfifo"
        Me.rbfifo.Size = New System.Drawing.Size(57, 18)
        Me.rbfifo.TabIndex = 0
        Me.rbfifo.TabStop = True
        Me.rbfifo.Text = "FIFO"
        Me.rbfifo.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(149, 18)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(12, 14)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = ":"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label11.Visible = False
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(509, 18)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(12, 14)
        Me.Label37.TabIndex = 1
        Me.Label37.Text = ":"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(50, 18)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(98, 14)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Calculation On"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label12.Visible = False
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(406, 18)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(106, 14)
        Me.Label35.TabIndex = 0
        Me.Label35.Text = "Next Day Count"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtnoday
        '
        Me.txtnoday.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtnoday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtnoday.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnoday.Location = New System.Drawing.Point(524, 14)
        Me.txtnoday.Name = "txtnoday"
        Me.txtnoday.Size = New System.Drawing.Size(52, 23)
        Me.txtnoday.TabIndex = 1
        Me.txtnoday.Text = "1"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.chkaddanov)
        Me.Panel2.Controls.Add(Me.chkMessage)
        Me.Panel2.Controls.Add(Me.dtpmaturity_far_month)
        Me.Panel2.Controls.Add(Me.cmdexit)
        Me.Panel2.Controls.Add(Me.cmdsave)
        Me.Panel2.Controls.Add(Me.Label80)
        Me.Panel2.Controls.Add(Me.Label81)
        Me.Panel2.Location = New System.Drawing.Point(3, 450)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(683, 87)
        Me.Panel2.TabIndex = 7
        '
        'chkaddanov
        '
        Me.chkaddanov.AutoSize = True
        Me.chkaddanov.Checked = True
        Me.chkaddanov.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkaddanov.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.chkaddanov.ForeColor = System.Drawing.Color.Navy
        Me.chkaddanov.Location = New System.Drawing.Point(267, 12)
        Me.chkaddanov.Name = "chkaddanov"
        Me.chkaddanov.Size = New System.Drawing.Size(135, 18)
        Me.chkaddanov.TabIndex = 3
        Me.chkaddanov.Text = "Add ANOV Margin"
        Me.chkaddanov.UseVisualStyleBackColor = True
        '
        'chkMessage
        '
        Me.chkMessage.AutoSize = True
        Me.chkMessage.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.chkMessage.ForeColor = System.Drawing.Color.Navy
        Me.chkMessage.Location = New System.Drawing.Point(23, 45)
        Me.chkMessage.Name = "chkMessage"
        Me.chkMessage.Size = New System.Drawing.Size(379, 18)
        Me.chkMessage.TabIndex = 3
        Me.chkMessage.Text = "Message display to update Contact and Security master"
        Me.chkMessage.UseVisualStyleBackColor = True
        '
        'dtpmaturity_far_month
        '
        Me.dtpmaturity_far_month.CustomFormat = "dd/MMM/yyyy"
        Me.dtpmaturity_far_month.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpmaturity_far_month.Location = New System.Drawing.Point(142, 10)
        Me.dtpmaturity_far_month.Name = "dtpmaturity_far_month"
        Me.dtpmaturity_far_month.Size = New System.Drawing.Size(108, 22)
        Me.dtpmaturity_far_month.TabIndex = 2
        Me.dtpmaturity_far_month.Value = New Date(1980, 1, 1, 0, 0, 1, 0)
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Silver
        Me.cmdexit.CausesValidation = False
        Me.cmdexit.Font = New System.Drawing.Font("Palatino Linotype", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdexit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdexit.Location = New System.Drawing.Point(554, 17)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(105, 37)
        Me.cmdexit.TabIndex = 1
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'cmdsave
        '
        Me.cmdsave.BackColor = System.Drawing.Color.Silver
        Me.cmdsave.Font = New System.Drawing.Font("Palatino Linotype", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdsave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdsave.Location = New System.Drawing.Point(437, 18)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(105, 37)
        Me.cmdsave.TabIndex = 0
        Me.cmdsave.Text = "&Apply"
        Me.cmdsave.UseVisualStyleBackColor = False
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.ForeColor = System.Drawing.Color.Navy
        Me.Label80.Location = New System.Drawing.Point(15, 12)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(115, 14)
        Me.Label80.TabIndex = 0
        Me.Label80.Text = "Far Month Future"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.Location = New System.Drawing.Point(126, 12)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(12, 14)
        Me.Label81.TabIndex = 1
        Me.Label81.Text = ":"
        Me.Label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.GroupBox6.Controls.Add(Me.txtSpanTimer)
        Me.GroupBox6.Controls.Add(Me.Label3)
        Me.GroupBox6.Controls.Add(Me.Label73)
        Me.GroupBox6.Controls.Add(Me.Label74)
        Me.GroupBox6.Controls.Add(Me.txtbtimer)
        Me.GroupBox6.Controls.Add(Me.cmbtot)
        Me.GroupBox6.Controls.Add(Me.cmbequity)
        Me.GroupBox6.Controls.Add(Me.Label61)
        Me.GroupBox6.Controls.Add(Me.Label62)
        Me.GroupBox6.Controls.Add(Me.Label63)
        Me.GroupBox6.Controls.Add(Me.Label64)
        Me.GroupBox6.Controls.Add(Me.cmbunreal)
        Me.GroupBox6.Controls.Add(Me.cmbreal)
        Me.GroupBox6.Controls.Add(Me.cmbexmarg)
        Me.GroupBox6.Controls.Add(Me.cmbinmarg)
        Me.GroupBox6.Controls.Add(Me.Label53)
        Me.GroupBox6.Controls.Add(Me.Label54)
        Me.GroupBox6.Controls.Add(Me.Label55)
        Me.GroupBox6.Controls.Add(Me.Label56)
        Me.GroupBox6.Controls.Add(Me.Label57)
        Me.GroupBox6.Controls.Add(Me.Label58)
        Me.GroupBox6.Controls.Add(Me.Label59)
        Me.GroupBox6.Controls.Add(Me.Label60)
        Me.GroupBox6.Controls.Add(Me.cmbdeltaval)
        Me.GroupBox6.Controls.Add(Me.cmbdelta)
        Me.GroupBox6.Controls.Add(Me.cmbthetaval)
        Me.GroupBox6.Controls.Add(Me.cmbtheta)
        Me.GroupBox6.Controls.Add(Me.cmbvegaval)
        Me.GroupBox6.Controls.Add(Me.cmbvega)
        Me.GroupBox6.Controls.Add(Me.cmbgammaval)
        Me.GroupBox6.Controls.Add(Me.cmbgamma)
        Me.GroupBox6.Controls.Add(Me.Label32)
        Me.GroupBox6.Controls.Add(Me.Label24)
        Me.GroupBox6.Controls.Add(Me.Label31)
        Me.GroupBox6.Controls.Add(Me.Label22)
        Me.GroupBox6.Controls.Add(Me.Label30)
        Me.GroupBox6.Controls.Add(Me.Label23)
        Me.GroupBox6.Controls.Add(Me.Label29)
        Me.GroupBox6.Controls.Add(Me.Label20)
        Me.GroupBox6.Controls.Add(Me.Label28)
        Me.GroupBox6.Controls.Add(Me.Label21)
        Me.GroupBox6.Controls.Add(Me.Label27)
        Me.GroupBox6.Controls.Add(Me.Label17)
        Me.GroupBox6.Controls.Add(Me.Label26)
        Me.GroupBox6.Controls.Add(Me.Label19)
        Me.GroupBox6.Controls.Add(Me.Label25)
        Me.GroupBox6.Controls.Add(Me.Label18)
        Me.GroupBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox6.Location = New System.Drawing.Point(1, 248)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(686, 139)
        Me.GroupBox6.TabIndex = 4
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Round Setting"
        '
        'txtSpanTimer
        '
        Me.txtSpanTimer.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtSpanTimer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpanTimer.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSpanTimer.Location = New System.Drawing.Point(608, 72)
        Me.txtSpanTimer.Name = "txtSpanTimer"
        Me.txtSpanTimer.Size = New System.Drawing.Size(52, 23)
        Me.txtSpanTimer.TabIndex = 30
        Me.txtSpanTimer.Text = "1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(517, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 14)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Span Timer :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(592, 102)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(12, 14)
        Me.Label73.TabIndex = 28
        Me.Label73.Text = ":"
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label74
        '
        Me.Label74.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.Location = New System.Drawing.Point(521, 92)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(76, 38)
        Me.Label74.TabIndex = 26
        Me.Label74.Text = "Broadcast Timer"
        Me.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtbtimer
        '
        Me.txtbtimer.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtbtimer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtbtimer.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbtimer.Location = New System.Drawing.Point(608, 101)
        Me.txtbtimer.Name = "txtbtimer"
        Me.txtbtimer.Size = New System.Drawing.Size(52, 23)
        Me.txtbtimer.TabIndex = 27
        Me.txtbtimer.Text = "1"
        '
        'cmbtot
        '
        Me.cmbtot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtot.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtot.FormattingEnabled = True
        Me.cmbtot.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbtot.Location = New System.Drawing.Point(608, 45)
        Me.cmbtot.Name = "cmbtot"
        Me.cmbtot.Size = New System.Drawing.Size(51, 22)
        Me.cmbtot.TabIndex = 25
        '
        'cmbequity
        '
        Me.cmbequity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbequity.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbequity.FormattingEnabled = True
        Me.cmbequity.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbequity.Location = New System.Drawing.Point(608, 18)
        Me.cmbequity.Name = "cmbequity"
        Me.cmbequity.Size = New System.Drawing.Size(51, 22)
        Me.cmbequity.TabIndex = 24
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(597, 49)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(12, 14)
        Me.Label61.TabIndex = 22
        Me.Label61.Text = ":"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.Location = New System.Drawing.Point(597, 22)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(12, 14)
        Me.Label62.TabIndex = 23
        Me.Label62.Text = ":"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.Location = New System.Drawing.Point(497, 52)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(105, 14)
        Me.Label63.TabIndex = 20
        Me.Label63.Text = "Real. Tot. Value"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(514, 24)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(85, 14)
        Me.Label64.TabIndex = 21
        Me.Label64.Text = "Equity Value"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbunreal
        '
        Me.cmbunreal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbunreal.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbunreal.FormattingEnabled = True
        Me.cmbunreal.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbunreal.Location = New System.Drawing.Point(435, 99)
        Me.cmbunreal.Name = "cmbunreal"
        Me.cmbunreal.Size = New System.Drawing.Size(58, 22)
        Me.cmbunreal.TabIndex = 19
        '
        'cmbreal
        '
        Me.cmbreal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbreal.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbreal.FormattingEnabled = True
        Me.cmbreal.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbreal.Location = New System.Drawing.Point(435, 72)
        Me.cmbreal.Name = "cmbreal"
        Me.cmbreal.Size = New System.Drawing.Size(58, 22)
        Me.cmbreal.TabIndex = 18
        '
        'cmbexmarg
        '
        Me.cmbexmarg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbexmarg.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbexmarg.FormattingEnabled = True
        Me.cmbexmarg.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbexmarg.Location = New System.Drawing.Point(436, 45)
        Me.cmbexmarg.Name = "cmbexmarg"
        Me.cmbexmarg.Size = New System.Drawing.Size(58, 22)
        Me.cmbexmarg.TabIndex = 17
        '
        'cmbinmarg
        '
        Me.cmbinmarg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbinmarg.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbinmarg.FormattingEnabled = True
        Me.cmbinmarg.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbinmarg.Location = New System.Drawing.Point(436, 18)
        Me.cmbinmarg.Name = "cmbinmarg"
        Me.cmbinmarg.Size = New System.Drawing.Size(58, 22)
        Me.cmbinmarg.TabIndex = 16
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(422, 103)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(12, 14)
        Me.Label53.TabIndex = 14
        Me.Label53.Text = ":"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(422, 76)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(12, 14)
        Me.Label54.TabIndex = 15
        Me.Label54.Text = ":"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(313, 103)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(111, 14)
        Me.Label55.TabIndex = 8
        Me.Label55.Text = "Unrealised profit"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(423, 49)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(12, 14)
        Me.Label56.TabIndex = 13
        Me.Label56.Text = ":"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(327, 76)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(97, 14)
        Me.Label57.TabIndex = 9
        Me.Label57.Text = "Realised profit"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(423, 22)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(12, 14)
        Me.Label58.TabIndex = 12
        Me.Label58.Text = ":"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(323, 49)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(104, 14)
        Me.Label59.TabIndex = 11
        Me.Label59.Text = "Ex. Marg. Value"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(319, 22)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(102, 14)
        Me.Label60.TabIndex = 10
        Me.Label60.Text = "In. Marg. Value"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbdeltaval
        '
        Me.cmbdeltaval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdeltaval.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbdeltaval.FormattingEnabled = True
        Me.cmbdeltaval.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbdeltaval.Location = New System.Drawing.Point(252, 102)
        Me.cmbdeltaval.Name = "cmbdeltaval"
        Me.cmbdeltaval.Size = New System.Drawing.Size(58, 22)
        Me.cmbdeltaval.TabIndex = 7
        '
        'cmbdelta
        '
        Me.cmbdelta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdelta.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbdelta.FormattingEnabled = True
        Me.cmbdelta.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbdelta.Location = New System.Drawing.Point(81, 102)
        Me.cmbdelta.Name = "cmbdelta"
        Me.cmbdelta.Size = New System.Drawing.Size(52, 22)
        Me.cmbdelta.TabIndex = 3
        '
        'cmbthetaval
        '
        Me.cmbthetaval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbthetaval.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbthetaval.FormattingEnabled = True
        Me.cmbthetaval.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbthetaval.Location = New System.Drawing.Point(252, 75)
        Me.cmbthetaval.Name = "cmbthetaval"
        Me.cmbthetaval.Size = New System.Drawing.Size(58, 22)
        Me.cmbthetaval.TabIndex = 6
        '
        'cmbtheta
        '
        Me.cmbtheta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtheta.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtheta.FormattingEnabled = True
        Me.cmbtheta.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbtheta.Location = New System.Drawing.Point(81, 75)
        Me.cmbtheta.Name = "cmbtheta"
        Me.cmbtheta.Size = New System.Drawing.Size(52, 22)
        Me.cmbtheta.TabIndex = 2
        '
        'cmbvegaval
        '
        Me.cmbvegaval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbvegaval.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbvegaval.FormattingEnabled = True
        Me.cmbvegaval.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbvegaval.Location = New System.Drawing.Point(253, 48)
        Me.cmbvegaval.Name = "cmbvegaval"
        Me.cmbvegaval.Size = New System.Drawing.Size(58, 22)
        Me.cmbvegaval.TabIndex = 5
        '
        'cmbvega
        '
        Me.cmbvega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbvega.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbvega.FormattingEnabled = True
        Me.cmbvega.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbvega.Location = New System.Drawing.Point(81, 48)
        Me.cmbvega.Name = "cmbvega"
        Me.cmbvega.Size = New System.Drawing.Size(52, 22)
        Me.cmbvega.TabIndex = 1
        '
        'cmbgammaval
        '
        Me.cmbgammaval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbgammaval.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbgammaval.FormattingEnabled = True
        Me.cmbgammaval.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbgammaval.Location = New System.Drawing.Point(253, 21)
        Me.cmbgammaval.Name = "cmbgammaval"
        Me.cmbgammaval.Size = New System.Drawing.Size(58, 22)
        Me.cmbgammaval.TabIndex = 4
        '
        'cmbgamma
        '
        Me.cmbgamma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbgamma.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbgamma.FormattingEnabled = True
        Me.cmbgamma.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbgamma.Location = New System.Drawing.Point(81, 21)
        Me.cmbgamma.Name = "cmbgamma"
        Me.cmbgamma.Size = New System.Drawing.Size(52, 22)
        Me.cmbgamma.TabIndex = 0
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(236, 106)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(12, 14)
        Me.Label32.TabIndex = 1
        Me.Label32.Text = ":"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(64, 106)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(12, 14)
        Me.Label24.TabIndex = 1
        Me.Label24.Text = ":"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(236, 79)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(12, 14)
        Me.Label31.TabIndex = 1
        Me.Label31.Text = ":"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(64, 79)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(12, 14)
        Me.Label22.TabIndex = 1
        Me.Label22.Text = ":"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(157, 106)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(79, 14)
        Me.Label30.TabIndex = 0
        Me.Label30.Text = "Delta Value"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(20, 106)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(40, 14)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "Delta"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(237, 52)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(12, 14)
        Me.Label29.TabIndex = 1
        Me.Label29.Text = ":"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(65, 52)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(12, 14)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = ":"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(154, 79)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(82, 14)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "Theta Value"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(17, 79)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(43, 14)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "Theta"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(237, 25)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(12, 14)
        Me.Label27.TabIndex = 1
        Me.Label27.Text = ":"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(65, 25)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(12, 14)
        Me.Label17.TabIndex = 1
        Me.Label17.Text = ":"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(158, 52)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(78, 14)
        Me.Label26.TabIndex = 0
        Me.Label26.Text = "Vega Value"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(21, 52)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(39, 14)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Vega"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(143, 25)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(93, 14)
        Me.Label25.TabIndex = 0
        Me.Label25.Text = "Gamma Value"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(6, 25)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(54, 14)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Gamma"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox7
        '
        Me.GroupBox7.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.GroupBox7.Controls.Add(Me.Label9)
        Me.GroupBox7.Controls.Add(Me.cmbMode)
        Me.GroupBox7.Controls.Add(Me.Label4)
        Me.GroupBox7.Controls.Add(Me.cmbzero)
        Me.GroupBox7.Controls.Add(Me.Label65)
        Me.GroupBox7.Controls.Add(Me.Label66)
        Me.GroupBox7.Controls.Add(Me.cmbsquaremtm)
        Me.GroupBox7.Controls.Add(Me.cmbexp)
        Me.GroupBox7.Controls.Add(Me.cmbnetmtm)
        Me.GroupBox7.Controls.Add(Me.cmbgrossmtm)
        Me.GroupBox7.Controls.Add(Me.Label39)
        Me.GroupBox7.Controls.Add(Me.Label40)
        Me.GroupBox7.Controls.Add(Me.Label43)
        Me.GroupBox7.Controls.Add(Me.Label44)
        Me.GroupBox7.Controls.Add(Me.Label45)
        Me.GroupBox7.Controls.Add(Me.Label46)
        Me.GroupBox7.Controls.Add(Me.Label47)
        Me.GroupBox7.Controls.Add(Me.Label48)
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox7.Location = New System.Drawing.Point(1, 375)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(686, 69)
        Me.GroupBox7.TabIndex = 5
        Me.GroupBox7.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(572, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(12, 14)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = ":"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbMode
        '
        Me.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMode.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMode.FormattingEnabled = True
        Me.cmbMode.Items.AddRange(New Object() {"Online", "Offline", "Future Update Only"})
        Me.cmbMode.Location = New System.Drawing.Point(590, 45)
        Me.cmbMode.Name = "cmbMode"
        Me.cmbMode.Size = New System.Drawing.Size(92, 22)
        Me.cmbMode.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(428, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(147, 14)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Analysis Startup Mode"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbzero
        '
        Me.cmbzero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbzero.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbzero.FormattingEnabled = True
        Me.cmbzero.Items.AddRange(New Object() {"Yes", "No"})
        Me.cmbzero.Location = New System.Drawing.Point(590, 14)
        Me.cmbzero.Name = "cmbzero"
        Me.cmbzero.Size = New System.Drawing.Size(90, 22)
        Me.cmbzero.TabIndex = 9
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(572, 16)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(12, 14)
        Me.Label65.TabIndex = 8
        Me.Label65.Text = ":"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label66
        '
        Me.Label66.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(452, 14)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(121, 37)
        Me.Label66.TabIndex = 7
        Me.Label66.Text = "Show zero qty in analysis"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbsquaremtm
        '
        Me.cmbsquaremtm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsquaremtm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsquaremtm.FormattingEnabled = True
        Me.cmbsquaremtm.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbsquaremtm.Location = New System.Drawing.Point(336, 41)
        Me.cmbsquaremtm.Name = "cmbsquaremtm"
        Me.cmbsquaremtm.Size = New System.Drawing.Size(69, 22)
        Me.cmbsquaremtm.TabIndex = 3
        '
        'cmbexp
        '
        Me.cmbexp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbexp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbexp.FormattingEnabled = True
        Me.cmbexp.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbexp.Location = New System.Drawing.Point(94, 41)
        Me.cmbexp.Name = "cmbexp"
        Me.cmbexp.Size = New System.Drawing.Size(71, 22)
        Me.cmbexp.TabIndex = 1
        '
        'cmbnetmtm
        '
        Me.cmbnetmtm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbnetmtm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbnetmtm.FormattingEnabled = True
        Me.cmbnetmtm.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbnetmtm.Location = New System.Drawing.Point(336, 14)
        Me.cmbnetmtm.Name = "cmbnetmtm"
        Me.cmbnetmtm.Size = New System.Drawing.Size(69, 22)
        Me.cmbnetmtm.TabIndex = 2
        '
        'cmbgrossmtm
        '
        Me.cmbgrossmtm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbgrossmtm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbgrossmtm.FormattingEnabled = True
        Me.cmbgrossmtm.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5"})
        Me.cmbgrossmtm.Location = New System.Drawing.Point(94, 14)
        Me.cmbgrossmtm.Name = "cmbgrossmtm"
        Me.cmbgrossmtm.Size = New System.Drawing.Size(71, 22)
        Me.cmbgrossmtm.TabIndex = 0
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(320, 45)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(12, 14)
        Me.Label39.TabIndex = 1
        Me.Label39.Text = ":"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(78, 45)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(12, 14)
        Me.Label40.TabIndex = 1
        Me.Label40.Text = ":"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(320, 18)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(12, 14)
        Me.Label43.TabIndex = 1
        Me.Label43.Text = ":"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(78, 18)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(12, 14)
        Me.Label44.TabIndex = 1
        Me.Label44.Text = ":"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(214, 45)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(107, 14)
        Me.Label45.TabIndex = 0
        Me.Label45.Text = "Square off  MTM"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(18, 45)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(61, 14)
        Me.Label46.TabIndex = 0
        Me.Label46.Text = "Expense"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(260, 18)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(62, 14)
        Me.Label47.TabIndex = 0
        Me.Label47.Text = "NET MTM"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(5, 18)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(74, 14)
        Me.Label48.TabIndex = 0
        Me.Label48.Text = "Gross MTM"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox8
        '
        Me.GroupBox8.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.GroupBox8.Controls.Add(Me.Label72)
        Me.GroupBox8.Controls.Add(Me.Label71)
        Me.GroupBox8.Controls.Add(Me.Label69)
        Me.GroupBox8.Controls.Add(Me.Label70)
        Me.GroupBox8.Controls.Add(Me.txtpvol)
        Me.GroupBox8.Controls.Add(Me.Label67)
        Me.GroupBox8.Controls.Add(Me.Label68)
        Me.GroupBox8.Controls.Add(Me.txtcvol)
        Me.GroupBox8.Controls.Add(Me.GroupBox9)
        Me.GroupBox8.Controls.Add(Me.cmbalert)
        Me.GroupBox8.Controls.Add(Me.Label34)
        Me.GroupBox8.Controls.Add(Me.Label36)
        Me.GroupBox8.Controls.Add(Me.Label33)
        Me.GroupBox8.Controls.Add(Me.Label42)
        Me.GroupBox8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox8.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox8.Location = New System.Drawing.Point(5, 522)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(661, 10)
        Me.GroupBox8.TabIndex = 6
        Me.GroupBox8.TabStop = False
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.Location = New System.Drawing.Point(585, 46)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(22, 14)
        Me.Label72.TabIndex = 12
        Me.Label72.Text = "%"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label72.Visible = False
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(585, 18)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(22, 14)
        Me.Label71.TabIndex = 11
        Me.Label71.Text = "%"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label71.Visible = False
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(513, 46)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(12, 14)
        Me.Label69.TabIndex = 9
        Me.Label69.Text = ":"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label69.Visible = False
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.Location = New System.Drawing.Point(454, 46)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(51, 14)
        Me.Label70.TabIndex = 8
        Me.Label70.Text = "Put Vol"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label70.Visible = False
        '
        'txtpvol
        '
        Me.txtpvol.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtpvol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpvol.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpvol.Location = New System.Drawing.Point(528, 42)
        Me.txtpvol.Name = "txtpvol"
        Me.txtpvol.Size = New System.Drawing.Size(52, 23)
        Me.txtpvol.TabIndex = 10
        Me.txtpvol.Text = "27"
        Me.txtpvol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtpvol.Visible = False
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(513, 17)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(12, 14)
        Me.Label67.TabIndex = 6
        Me.Label67.Text = ":"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label67.Visible = False
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.Location = New System.Drawing.Point(454, 17)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(53, 14)
        Me.Label68.TabIndex = 5
        Me.Label68.Text = "Call Vol"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label68.Visible = False
        '
        'txtcvol
        '
        Me.txtcvol.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtcvol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcvol.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcvol.Location = New System.Drawing.Point(528, 13)
        Me.txtcvol.Name = "txtcvol"
        Me.txtcvol.Size = New System.Drawing.Size(52, 23)
        Me.txtcvol.TabIndex = 7
        Me.txtcvol.Text = "25"
        Me.txtcvol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtcvol.Visible = False
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.rbequity)
        Me.GroupBox9.Controls.Add(Me.rbfut)
        Me.GroupBox9.Location = New System.Drawing.Point(212, 7)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(204, 36)
        Me.GroupBox9.TabIndex = 0
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Visible = False
        '
        'rbequity
        '
        Me.rbequity.AutoSize = True
        Me.rbequity.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbequity.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbequity.Location = New System.Drawing.Point(103, 11)
        Me.rbequity.Name = "rbequity"
        Me.rbequity.Size = New System.Drawing.Size(65, 18)
        Me.rbequity.TabIndex = 1
        Me.rbequity.TabStop = True
        Me.rbequity.Text = "Equity"
        Me.rbequity.UseVisualStyleBackColor = True
        '
        'rbfut
        '
        Me.rbfut.AutoSize = True
        Me.rbfut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbfut.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbfut.Location = New System.Drawing.Point(9, 11)
        Me.rbfut.Name = "rbfut"
        Me.rbfut.Size = New System.Drawing.Size(67, 18)
        Me.rbfut.TabIndex = 0
        Me.rbfut.TabStop = True
        Me.rbfut.Text = "Future"
        Me.rbfut.UseVisualStyleBackColor = True
        '
        'cmbalert
        '
        Me.cmbalert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbalert.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbalert.FormattingEnabled = True
        Me.cmbalert.Items.AddRange(New Object() {"Security Wise", "Scrip Wise"})
        Me.cmbalert.Location = New System.Drawing.Point(83, 42)
        Me.cmbalert.Name = "cmbalert"
        Me.cmbalert.Size = New System.Drawing.Size(28, 22)
        Me.cmbalert.TabIndex = 1
        Me.cmbalert.Visible = False
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(196, 18)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(12, 14)
        Me.Label34.TabIndex = 1
        Me.Label34.Text = ":"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label34.Visible = False
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(67, 46)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(12, 14)
        Me.Label36.TabIndex = 1
        Me.Label36.Text = ":"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label36.Visible = False
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(32, 18)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(155, 14)
        Me.Label33.TabIndex = 0
        Me.Label33.Text = "Calculation On Volatility"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label33.Visible = False
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(20, 46)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(43, 14)
        Me.Label42.TabIndex = 0
        Me.Label42.Text = "Alerts"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label42.Visible = False
        '
        'frmappSetting
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.ClientSize = New System.Drawing.Size(689, 544)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmappSetting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim objTrad As trading = New trading

    Private Sub frmappSetting_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub
    Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbdelta.SelectedIndex = 0
        cmbdeltaval.SelectedIndex = 0
        cmbgamma.SelectedIndex = 0
        cmbgammaval.SelectedIndex = 0
        cmbvega.SelectedIndex = 0
        cmbvegaval.SelectedIndex = 0
        cmbtheta.SelectedIndex = 0
        cmbthetaval.SelectedIndex = 0

        FillGrid()
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        Try
            Dim drow As DataRow

            Dim setting_name As String
            Dim setting_key As String
            If txtbtimer.Text.Trim = "" Then
                txtbtimer.Text = 1
            End If
            
            If txtbtimer.Text <= 0 Then
                MsgBox("You cannot enter timer value less then zero or zero")
                txtbtimer.Text = 1
                txtbtimer.Focus()
                Exit Sub
            End If
           
            If Validate_grid() = True Then
                For Each drow In dtable_set.Rows
                    setting_name = ""
                    setting_key = ""
                    If drow("SettingName") = "Backup_path" Then
                        setting_name = "Backup_path"
                        setting_key = txtbackuppath.Text
                        'txtbackuppath.Text = drow("Settingkey")
                    ElseIf drow("SettingName") = "Rateofinterest" Then
                        setting_name = "Rateofinterest"
                        setting_key = Val(txtrateofint.Text)
                        'txtrateofint.Text = drow("Settingkey")
                    ElseIf drow("SettingName") = "SPAN_path" Then
                        setting_name = "SPAN_path"
                        setting_key = txtspanpath.Text
                    ElseIf drow("SettingName") = "FO_UDP_IP" Then
                        setting_name = "FO_UDP_IP"
                        'setting_key = Format((txtipaddress1.Text), "000") & "." & Format(CInt(txtipaddress2.Text), "000") & "." & Format(CInt(txtipaddress3.Text), "000") & "." & Format(CInt(txtipaddress4.Text), "000")
                        setting_key = CInt(txtipaddress1.Text) & "." & CInt(txtipaddress2.Text) & "." & CInt(txtipaddress3.Text) & "." & CInt(txtipaddress4.Text)
                        'txtipaddress.Text = drow("Settingkey")
                    ElseIf drow("SettingName") = "FO_UDP_Port" Then
                        setting_name = "FO_UDP_Port"
                        setting_key = txtport.Text
                        'txtport.Text = drow("Settingkey")
                    ElseIf drow("SettingName") = "CM_UDP_IP" Then
                        setting_name = "CM_UDP_IP"
                        'setting_key = Format(CInt(txtudpip1.Text), "000") & "." & Format(CInt(txtudpip2.Text), "000") & "." & Format(CInt(txtudpip3.Text), "000") & "." & Format(CInt(txtudpip4.Text), "000")
                        setting_key = CInt(txtudpip1.Text) & "." & CInt(txtudpip2.Text) & "." & CInt(txtudpip3.Text) & "." & CInt(txtudpip4.Text)
                        'txtipaddress.Text = drow("Settingkey")
                    ElseIf drow("SettingName") = "CM_UDP_Port" Then
                        setting_name = "CM_UDP_Port"
                        setting_key = txtudpport.Text
                        'txtport.Text = drow("Settingkey")
                    ElseIf drow("SettingName") = "fifo_avg" Then
                        setting_name = "fifo_avg"
                        If rbfifo.Checked = True Then
                            setting_key = "True"
                        Else
                            setting_key = "False"
                        End If
                    ElseIf drow("SettingName") = "NoofDay" Then
                        setting_name = "NoofDay"
                        setting_key = txtnoday.Text
                        'txtnoday.Text = drow("Settingkey")
                    ElseIf drow("SettingName") = "RoundGamma" Then
                        setting_name = "RoundGamma"
                        setting_key = cmbgamma.SelectedItem.ToString
                        'cmbgamma.SelectedIndex = drow("Settingkey")
                    ElseIf drow("SettingName") = "RoundVega" Then
                        setting_name = "RoundVega"
                        setting_key = cmbvega.SelectedItem.ToString
                        'cmbvega.SelectedIndex = drow("Settingkey")
                    ElseIf drow("SettingName") = "RoundTheta" Then
                        setting_name = "RoundTheta"
                        setting_key = cmbtheta.SelectedItem.ToString
                        '    cmbtheta.SelectedIndex = drow("Settingkey")
                    ElseIf drow("SettingName") = "RoundDelta" Then
                        setting_name = "RoundDelta"
                        setting_key = cmbdelta.SelectedItem.ToString
                        'cmbdelta.SelectedIndex = drow("Settingkey")
                    ElseIf drow("SettingName") = "RoundGamma_Val" Then
                        setting_name = "RoundGamma_Val"
                        setting_key = cmbgammaval.SelectedItem.ToString
                        'cmbgammaval.SelectedIndex = drow("Settingkey")
                    ElseIf drow("SettingName") = "RoundVega_Val" Then
                        setting_name = "RoundVega_Val"
                        setting_key = cmbvegaval.SelectedItem.ToString
                        'cmbvegaval.SelectedIndex = drow("Settingkey")
                    ElseIf drow("SettingName") = "RoundTheta_Val" Then
                        setting_name = "RoundTheta_Val"
                        setting_key = cmbthetaval.SelectedItem.ToString
                        'cmbthetaval.SelectedIndex = drow("Settingkey")
                    ElseIf drow("SettingName") = "RoundDelta_Val" Then
                        setting_name = "RoundDelta_Val"
                        setting_key = cmbdeltaval.SelectedItem.ToString
                        'cmbdeltaval.SelectedIndex = drow("Settingkey")
                    ElseIf drow("SettingName") = "RoundGrossMTM" Then
                        setting_name = "RoundGrossMTM"
                        setting_key = cmbgrossmtm.SelectedItem.ToString
                        'cmbgrossmtm.SelectedIndex = drow("Settingkey")
                    ElseIf drow("SettingName") = "RoundExpense" Then
                        setting_name = "RoundExpense"
                        setting_key = cmbexp.SelectedItem.ToString
                        'cmbexp.SelectedIndex = drow("Settingkey")
                    ElseIf drow("SettingName") = "RoundNetMTM" Then
                        setting_name = "RoundNetMTM"
                        setting_key = cmbnetmtm.SelectedItem.ToString
                        'cmbnetmtm.SelectedIndex = drow("Settingkey")
                    ElseIf drow("SettingName") = "RoundSquareMTM" Then
                        setting_name = "RoundSquareMTM"
                        setting_key = cmbsquaremtm.SelectedItem.ToString
                        'cmbsquaremtm.SelectedIndex = drow("Settingkey")

                    ElseIf drow("SettingName") = "Roundinmarg" Then
                        setting_name = "Roundinmarg"
                        setting_key = cmbinmarg.SelectedItem.ToString
                    ElseIf drow("SettingName") = "Roundexmarg" Then
                        setting_name = "Roundexmarg"
                        setting_key = cmbexmarg.SelectedItem.ToString
                    ElseIf drow("SettingName") = "Roundrealmarg" Then
                        setting_name = "Roundrealmarg"
                        setting_key = cmbreal.SelectedItem.ToString
                    ElseIf drow("SettingName") = "Roundunmarg" Then
                        setting_name = "Roundunmarg"
                        setting_key = cmbunreal.SelectedItem.ToString
                    ElseIf drow("SettingName") = "Roundrealtot" Then
                        setting_name = "Roundrealtot"
                        setting_key = cmbtot.SelectedItem.ToString
                    ElseIf drow("SettingName") = "Roundequity" Then
                        setting_name = "Roundequity"
                        setting_key = cmbequity.SelectedItem.ToString
                    ElseIf drow("SettingName") = "Analysis_Fut_Eq" Then
                        setting_name = "Analysis_Fut_Eq"
                        If rbfut.Checked = True Then
                            setting_key = "0"
                        Else
                            setting_key = "1"
                        End If
                    ElseIf drow("SettingName") = "AlertOn" Then
                        setting_name = "AlertOn"
                        If cmbalert.SelectedIndex = 0 Then
                            setting_key = "0"
                        Else
                            setting_key = "1"
                        End If
                    ElseIf drow("SettingName") = "zero_qty_analysis" Then
                        setting_name = "zero_qty_analysis"
                        If cmbzero.SelectedIndex = 0 Then
                            setting_key = "0"
                        Else
                            setting_key = "1"
                        End If
                    ElseIf drow("SettingName") = "Call_Vol" Then
                        setting_name = "Call_Vol"
                        setting_key = txtcvol.Text
                    ElseIf drow("SettingName") = "Put_Vol" Then
                        setting_name = "Put_Vol"
                        setting_key = txtpvol.Text
                    ElseIf drow("SettingName") = "Timer_Calculation_Interval" Then
                        setting_name = "Timer_Calculation_Interval"
                        setting_key = txtbtimer.Text
                    ElseIf drow("settingname") = "Maturity_Far_month" Then
                        setting_name = "Maturity_Far_month"
                        setting_key = Format(dtpmaturity_far_month.Value, "MMM/dd/yyyy")
                        '======================keval chakalasiya(15-02-2010)
                    ElseIf drow("SettingName") = "Update_Contract_Security" Then
                        setting_name = "Update_Contract_Security"
                        If chkMessage.Checked = True Then
                            setting_key = "1"
                        Else
                            setting_key = "0"
                        End If

                    ElseIf drow("SettingName") = "addanovmargin" Then
                        setting_name = "addanovmargin"
                        setting_key = chkaddanov.Checked

                        '==============================================
                    ElseIf drow("SettingName") = "SPAN_TIMER" Then
                        setting_name = "SPAN_TIMER"
                        setting_key = Val(txtSpanTimer.Text)
                    ElseIf drow("SettingName") = "MODE" Then
                        setting_name = "MODE"
                        setting_key = cmbMode.Text.Trim
                    End If
                    If setting_name <> "" Then
                        objTrad.SettingName = setting_name
                        objTrad.SettingKey = setting_key
                        objTrad.Uid = CInt(drow("uid"))
                        objTrad.Update_setting()
                    End If
                Next
            End If
            MsgBox("Entry Updated Succesfully ..")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        FillGrid()
        Rounddata()
    End Sub
    Function Validate_grid() As Boolean
        Validate_grid = False

        Validate_grid = True
    End Function



    Private Sub FillGrid()
        Try
            dtable_set = New DataTable
            Dim drow As DataRow
            dtable_set = objTrad.Settings
            'Dim defoneat As String
            'Dim deeqneat As String
            'Dim nescode As String
            For Each drow In dtable_set.Rows
                If drow("SettingName") = "Backup_path" Then
                    txtbackuppath.Text = drow("Settingkey")
                ElseIf drow("SettingName") = "Rateofinterest" Then
                    txtrateofint.Text = drow("Settingkey")
                ElseIf drow("SettingName") = "SPAN_path" Then
                    txtspanpath.Text = drow("Settingkey")

                ElseIf drow("SettingName") = "FO_UDP_IP" Then
                    Dim ip_fo() As String

                    ip_fo = drow("Settingkey").ToString.Split(".")
                    txtipaddress1.Text = ip_fo(0) 'CInt(IIf(Mid(drow("Settingkey"), 1, 3) = "", 0, Mid(drow("Settingkey"), 1, 3)))
                    txtipaddress2.Text = ip_fo(1) 'CInt(IIf(Mid(drow("Settingkey"), 5, 3) = "", 0, Mid(drow("Settingkey"), 5, 3)))
                    txtipaddress3.Text = ip_fo(2) 'CInt(IIf(Mid(drow("Settingkey"), 9, 3) = "", 0, Mid(drow("Settingkey"), 9, 3)))
                    txtipaddress4.Text = ip_fo(3) 'CInt(IIf(Mid(drow("Settingkey"), 13, 3) = "", 0, Mid(drow("Settingkey"), 13, 3)))
                ElseIf drow("SettingName") = "FO_UDP_Port" Then
                    txtport.Text = drow("Settingkey")
                ElseIf drow("SettingName") = "CM_UDP_IP" Then
                    Dim ip_cm() As String

                    ip_cm = drow("Settingkey").ToString.Split(".")
                    txtudpip1.Text = ip_cm(0) ' CInt(IIf(Mid(drow("Settingkey"), 1, 3) = "", 0, Mid(drow("Settingkey"), 1, 3)))
                    txtudpip2.Text = ip_cm(1) 'CInt(IIf(Mid(drow("Settingkey"), 5, 3) = "", 0, Mid(drow("Settingkey"), 5, 3)))
                    txtudpip3.Text = ip_cm(2) 'CInt(IIf(Mid(drow("Settingkey"), 9, 3) = "", 0, Mid(drow("Settingkey"), 9, 3)))
                    txtudpip4.Text = ip_cm(3) 'CInt(IIf(Mid(drow("Settingkey"), 13, 3) = "", 0, Mid(drow("Settingkey"), 13, 3)))
                ElseIf drow("SettingName") = "CM_UDP_Port" Then
                    txtudpport.Text = drow("Settingkey")
                ElseIf drow("SettingName") = "fifo_avg" Then
                    If drow("Settingkey") = False Then
                        rbfifo.Checked = False
                        rbavg.Checked = True
                    Else
                        rbfifo.Checked = True
                        rbavg.Checked = False
                    End If
                ElseIf drow("SettingName") = "NoofDay" Then
                    txtnoday.Text = drow("Settingkey")
                ElseIf drow("SettingName") = "RoundGamma" Then
                    cmbgamma.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "RoundVega" Then
                    cmbvega.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "RoundTheta" Then
                    cmbtheta.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "RoundDelta" Then
                    cmbdelta.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "RoundGamma_Val" Then
                    cmbgammaval.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "RoundVega_Val" Then
                    cmbvegaval.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "RoundTheta_Val" Then
                    cmbthetaval.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "RoundDelta_Val" Then
                    cmbdeltaval.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "RoundGrossMTM" Then
                    cmbgrossmtm.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "RoundExpense" Then
                    cmbexp.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "RoundNetMTM" Then
                    cmbnetmtm.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "RoundSquareMTM" Then
                    cmbsquaremtm.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "Roundinmarg" Then
                    cmbinmarg.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "Roundexmarg" Then
                    cmbexmarg.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "Roundrealmarg" Then
                    cmbreal.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "Roundunmarg" Then
                    cmbunreal.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "Roundrealtot" Then
                    cmbtot.SelectedIndex = drow("Settingkey")
                ElseIf drow("SettingName") = "Roundequity" Then
                    cmbequity.SelectedIndex = drow("Settingkey")

                ElseIf drow("SettingName") = "Analysis_Fut_Eq" Then
                    If drow("Settingkey") = "0" Then
                        rbfut.Checked = True
                        rbequity.Checked = False
                    Else
                        rbfut.Checked = False
                        rbequity.Checked = True
                    End If
                ElseIf drow("SettingName") = "AlertOn" Then
                    If drow("Settingkey") = 0 Then
                        cmbalert.SelectedIndex = 0
                    Else
                        cmbalert.SelectedIndex = 1
                    End If
                ElseIf drow("SettingName") = "zero_qty_analysis" Then
                    If drow("Settingkey") = 0 Then
                        cmbzero.SelectedIndex = 0
                    Else
                        cmbzero.SelectedIndex = 1
                    End If
                ElseIf drow("SettingName") = "Call_Vol" Then
                    txtcvol.Text = drow("Settingkey")
                ElseIf drow("SettingName") = "Put_Vol" Then
                    txtpvol.Text = drow("Settingkey")
                    txtpvol.Text = drow("Settingkey")
                ElseIf drow("settingName") = "Timer_Calculation_Interval" Then
                    txtbtimer.Text = drow("Settingkey")
               
                ElseIf drow("settingName") = "Maturity_Far_month" Then
                    dtpmaturity_far_month.Value = CDate(drow("Settingkey"))
                    '===========================keval chakalasiya(15-02-2010)
                ElseIf drow("SettingName") = "Update_Contract_Security" Then
                    If drow("Settingkey") = 0 Then
                        chkMessage.Checked = False
                    Else
                        chkMessage.Checked = True
                    End If
                    'ElseIf drow("SettingName") = "TradeFileType" Then
                    '    If drow("Settingkey") = "Gets" Then
                    '        optGets.Checked = True
                    '    Else
                    '        optNormal.Checked = True
                    '    End If
                    'ElseIf drow("settingname") = "addanovmargin" Then
                    Dim str As String = drow("Settingkey").ToString
                    If str = "True" Then
                        chkaddanov.Checked = True
                    Else
                        chkaddanov.Checked = True
                    End If
                ElseIf drow("settingName") = "SPAN_TIMER" Then
                    txtSpanTimer.Text = drow("Settingkey")
                ElseIf drow("SettingName") = "MODE" Then
                    cmbMode.Text = drow("SettingKey")
                End If
                '=============================================
            Next

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub txtrateofint_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        numonly(e)
    End Sub
    Private Sub txtnoday_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnoday.KeyPress
        numonly(e)
    End Sub
    Private Sub txtport_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtport.KeyPress, txtudpport.KeyPress
        numonlywithoutdot(e)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim opfile As FolderBrowserDialog
        opfile = New FolderBrowserDialog
        '  opfile.Filter = "All files (*.*)|*.*"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtbackuppath.Text = opfile.SelectedPath
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim opfile As FolderBrowserDialog
        opfile = New FolderBrowserDialog
        '  opfile.Filter = "All files (*.*)|*.*"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtspanpath.Text = opfile.SelectedPath
        End If
    End Sub

    Private Sub txtipaddress1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtipaddress1.KeyPress
        numonlywithoutdot(e)
    End Sub
    Private Sub txtipaddress2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtipaddress2.KeyPress
        numonlywithoutdot(e)
    End Sub
    Private Sub txtipaddress3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtipaddress3.KeyPress
        numonlywithoutdot(e)
    End Sub

    Private Sub txtipaddress4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtipaddress4.KeyPress
        numonlywithoutdot(e)
    End Sub
    Private Sub txtudpip1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtudpip1.KeyPress
        numonlywithoutdot(e)
    End Sub
    Private Sub txtudpip2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtudpip2.KeyPress
        numonlywithoutdot(e)
    End Sub
    Private Sub txtudpip3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtudpip3.KeyPress
        numonlywithoutdot(e)
    End Sub
    Private Sub txtudpip4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtudpip4.KeyPress
        numonlywithoutdot(e)
    End Sub

    Private Sub frmappSetting_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub txtbtimer_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbtimer.KeyPress
        numonly(e)
    End Sub

    Private Sub txtbtimer_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtbtimer.Validating
        If txtbtimer.Text.Trim = "" Then
            txtbtimer.Text = 1
        End If
        If txtbtimer.Text <= 0 Then
            MsgBox("You cannot enter timer value less then zero or zero")
            txtbtimer.Text = 1
            txtbtimer.Focus()
        End If
    End Sub
    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        numonly(e)

    End Sub

    Private Sub txtSpanTimer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSpanTimer.KeyPress
        numonly(e)
    End Sub

    Private Sub txtSpanTimer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSpanTimer.TextChanged


    End Sub
End Class
