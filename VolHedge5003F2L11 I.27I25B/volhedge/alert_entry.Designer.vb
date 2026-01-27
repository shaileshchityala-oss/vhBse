<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class alert_entry
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(alert_entry))
        Me.pstatus = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.comp_script = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.field = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.units = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.opt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.value2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.value1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.entrydate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.uid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tabpage = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.panscript = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtunits = New System.Windows.Forms.TextBox()
        Me.cmbdate = New System.Windows.Forms.ComboBox()
        Me.cmbstrike = New System.Windows.Forms.ComboBox()
        Me.cmbcomp = New System.Windows.Forms.ComboBox()
        Me.cmbcp = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtscript = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cmbinstrument = New System.Windows.Forms.ComboBox()
        Me.lblsvalue2 = New System.Windows.Forms.Label()
        Me.txtsevalue = New System.Windows.Forms.TextBox()
        Me.cmbsopt = New System.Windows.Forms.ComboBox()
        Me.lblsvalue1 = New System.Windows.Forms.Label()
        Me.txtssvalue = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbsfield = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.cmdsave = New System.Windows.Forms.Button()
        Me.cmbdelete = New System.Windows.Forms.Button()
        Me.pancomp = New System.Windows.Forms.Panel()
        Me.lbluid = New System.Windows.Forms.Label()
        Me.lblcvalue2 = New System.Windows.Forms.Label()
        Me.lblcvalue1 = New System.Windows.Forms.Label()
        Me.txtcevalue = New System.Windows.Forms.TextBox()
        Me.txtcsvalue = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbcopt = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbcfield = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbcompany = New System.Windows.Forms.ComboBox()
        Me.rdbscript = New System.Windows.Forms.RadioButton()
        Me.rdbcomp = New System.Windows.Forms.RadioButton()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.cmdcurrexit = New System.Windows.Forms.Button()
        Me.cmdcurrclear = New System.Windows.Forms.Button()
        Me.pancurrscript = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtcurrunits = New System.Windows.Forms.TextBox()
        Me.cmbcurrdate = New System.Windows.Forms.ComboBox()
        Me.cmbcurrstrike = New System.Windows.Forms.ComboBox()
        Me.cmbcurrcomp = New System.Windows.Forms.ComboBox()
        Me.cmbcurrcp = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtcurrscript = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbcurrinstrument = New System.Windows.Forms.ComboBox()
        Me.lblcurrsvalue2 = New System.Windows.Forms.Label()
        Me.txtcurrsevalue = New System.Windows.Forms.TextBox()
        Me.cmbcurrsopt = New System.Windows.Forms.ComboBox()
        Me.lblcurrsvalue1 = New System.Windows.Forms.Label()
        Me.txtcurrssvalue = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbcurrsfield = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmdcurrsave = New System.Windows.Forms.Button()
        Me.cmbcurrdelete = New System.Windows.Forms.Button()
        Me.pancurrcomp = New System.Windows.Forms.Panel()
        Me.lblcurruid = New System.Windows.Forms.Label()
        Me.lblcurrcvalue2 = New System.Windows.Forms.Label()
        Me.lblcurrcvalue1 = New System.Windows.Forms.Label()
        Me.txtcurrcevalue = New System.Windows.Forms.TextBox()
        Me.txtcurrcsvalue = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cmbcurrcopt = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.cmbcurrcfield = New System.Windows.Forms.ComboBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.cmbcurrcompany = New System.Windows.Forms.ComboBox()
        Me.rdbcurrscript = New System.Windows.Forms.RadioButton()
        Me.rdbcurrcomp = New System.Windows.Forms.RadioButton()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn26 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdalert = New System.Windows.Forms.DataGridView()
        Me.colStatus = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn29 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn30 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn31 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn32 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn33 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn34 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn35 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn36 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkcheck = New System.Windows.Forms.CheckBox()
        Me.tabpage.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.panscript.SuspendLayout()
        Me.pancomp.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.pancurrscript.SuspendLayout()
        Me.pancurrcomp.SuspendLayout()
        CType(Me.grdalert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pstatus
        '
        Me.pstatus.DataPropertyName = "status"
        Me.pstatus.HeaderText = ""
        Me.pstatus.Name = "pstatus"
        Me.pstatus.Width = 35
        '
        'comp_script
        '
        Me.comp_script.DataPropertyName = "comp_script"
        Me.comp_script.HeaderText = "Security/Scrip"
        Me.comp_script.Name = "comp_script"
        Me.comp_script.ReadOnly = True
        Me.comp_script.Width = 230
        '
        'field
        '
        Me.field.DataPropertyName = "field"
        Me.field.HeaderText = "Field"
        Me.field.Name = "field"
        Me.field.ReadOnly = True
        '
        'units
        '
        Me.units.DataPropertyName = "units"
        Me.units.HeaderText = "units"
        Me.units.Name = "units"
        Me.units.ReadOnly = True
        Me.units.Width = 60
        '
        'opt
        '
        Me.opt.DataPropertyName = "opt"
        Me.opt.HeaderText = "Operator"
        Me.opt.Name = "opt"
        Me.opt.ReadOnly = True
        Me.opt.Width = 180
        '
        'cs
        '
        Me.cs.DataPropertyName = "cs"
        Me.cs.HeaderText = "cs"
        Me.cs.Name = "cs"
        Me.cs.ReadOnly = True
        Me.cs.Visible = False
        Me.cs.Width = 5
        '
        'value2
        '
        Me.value2.DataPropertyName = "value2"
        Me.value2.HeaderText = "Value-2"
        Me.value2.Name = "value2"
        Me.value2.ReadOnly = True
        Me.value2.Width = 60
        '
        'value1
        '
        Me.value1.DataPropertyName = "value1"
        Me.value1.HeaderText = "Value-1"
        Me.value1.Name = "value1"
        Me.value1.ReadOnly = True
        Me.value1.Width = 60
        '
        'entrydate
        '
        Me.entrydate.DataPropertyName = "entrydate"
        Me.entrydate.HeaderText = "entrydate"
        Me.entrydate.Name = "entrydate"
        Me.entrydate.ReadOnly = True
        Me.entrydate.Visible = False
        '
        'uid
        '
        Me.uid.DataPropertyName = "uid"
        Me.uid.HeaderText = "uid"
        Me.uid.Name = "uid"
        Me.uid.ReadOnly = True
        Me.uid.Visible = False
        Me.uid.Width = 60
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "status"
        Me.DataGridViewCheckBoxColumn1.HeaderText = ""
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.Width = 35
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "comp_script"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Security/Scrip"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 230
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "field"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Field"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "units"
        Me.DataGridViewTextBoxColumn3.HeaderText = "units"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 60
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "opt"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Operator"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 180
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "cs"
        Me.DataGridViewTextBoxColumn5.HeaderText = "cs"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 5
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "value2"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Value-2"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 60
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "value1"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Value-1"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 60
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "entrydate"
        Me.DataGridViewTextBoxColumn8.HeaderText = "entrydate"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "uid"
        Me.DataGridViewTextBoxColumn9.HeaderText = "uid"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Visible = False
        Me.DataGridViewTextBoxColumn9.Width = 60
        '
        'tabpage
        '
        Me.tabpage.Controls.Add(Me.TabPage1)
        Me.tabpage.Controls.Add(Me.TabPage2)
        Me.tabpage.Location = New System.Drawing.Point(12, 9)
        Me.tabpage.Name = "tabpage"
        Me.tabpage.SelectedIndex = 0
        Me.tabpage.Size = New System.Drawing.Size(757, 335)
        Me.tabpage.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TabPage1.Controls.Add(Me.cmdexit)
        Me.TabPage1.Controls.Add(Me.cmdclear)
        Me.TabPage1.Controls.Add(Me.panscript)
        Me.TabPage1.Controls.Add(Me.btnUpdate)
        Me.TabPage1.Controls.Add(Me.cmdsave)
        Me.TabPage1.Controls.Add(Me.cmbdelete)
        Me.TabPage1.Controls.Add(Me.pancomp)
        Me.TabPage1.Controls.Add(Me.rdbscript)
        Me.TabPage1.Controls.Add(Me.rdbcomp)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(749, 309)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Future & Option"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmdexit.CausesValidation = False
        Me.cmdexit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdexit.FlatAppearance.BorderSize = 3
        Me.cmdexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdexit.ForeColor = System.Drawing.SystemColors.Window
        Me.cmdexit.Location = New System.Drawing.Point(631, 278)
        Me.cmdexit.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(48, 27)
        Me.cmdexit.TabIndex = 15
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmdclear.CausesValidation = False
        Me.cmdclear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdclear.FlatAppearance.BorderSize = 3
        Me.cmdclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdclear.ForeColor = System.Drawing.SystemColors.Window
        Me.cmdclear.Location = New System.Drawing.Point(581, 278)
        Me.cmdclear.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(48, 27)
        Me.cmdclear.TabIndex = 14
        Me.cmdclear.Text = "Clear"
        Me.cmdclear.UseVisualStyleBackColor = False
        '
        'panscript
        '
        Me.panscript.BackColor = System.Drawing.Color.Transparent
        Me.panscript.Controls.Add(Me.Label2)
        Me.panscript.Controls.Add(Me.txtunits)
        Me.panscript.Controls.Add(Me.cmbdate)
        Me.panscript.Controls.Add(Me.cmbstrike)
        Me.panscript.Controls.Add(Me.cmbcomp)
        Me.panscript.Controls.Add(Me.cmbcp)
        Me.panscript.Controls.Add(Me.Label19)
        Me.panscript.Controls.Add(Me.Label20)
        Me.panscript.Controls.Add(Me.Label21)
        Me.panscript.Controls.Add(Me.Label22)
        Me.panscript.Controls.Add(Me.txtscript)
        Me.panscript.Controls.Add(Me.Label23)
        Me.panscript.Controls.Add(Me.Label24)
        Me.panscript.Controls.Add(Me.cmbinstrument)
        Me.panscript.Controls.Add(Me.lblsvalue2)
        Me.panscript.Controls.Add(Me.txtsevalue)
        Me.panscript.Controls.Add(Me.cmbsopt)
        Me.panscript.Controls.Add(Me.lblsvalue1)
        Me.panscript.Controls.Add(Me.txtssvalue)
        Me.panscript.Controls.Add(Me.Label7)
        Me.panscript.Controls.Add(Me.cmbsfield)
        Me.panscript.Controls.Add(Me.Label9)
        Me.panscript.Location = New System.Drawing.Point(2, 101)
        Me.panscript.Name = "panscript"
        Me.panscript.Size = New System.Drawing.Size(742, 173)
        Me.panscript.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(324, 147)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 14)
        Me.Label2.TabIndex = 68
        Me.Label2.Text = "Units"
        Me.Label2.Visible = False
        '
        'txtunits
        '
        Me.txtunits.Location = New System.Drawing.Point(371, 145)
        Me.txtunits.Name = "txtunits"
        Me.txtunits.Size = New System.Drawing.Size(67, 20)
        Me.txtunits.TabIndex = 6
        Me.txtunits.Text = "0"
        Me.txtunits.Visible = False
        '
        'cmbdate
        '
        Me.cmbdate.BackColor = System.Drawing.SystemColors.Window
        Me.cmbdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbdate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbdate.Location = New System.Drawing.Point(593, 35)
        Me.cmbdate.Name = "cmbdate"
        Me.cmbdate.Size = New System.Drawing.Size(115, 22)
        Me.cmbdate.TabIndex = 4
        '
        'cmbstrike
        '
        Me.cmbstrike.BackColor = System.Drawing.SystemColors.Window
        Me.cmbstrike.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbstrike.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbstrike.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbstrike.Location = New System.Drawing.Point(465, 35)
        Me.cmbstrike.Name = "cmbstrike"
        Me.cmbstrike.Size = New System.Drawing.Size(122, 22)
        Me.cmbstrike.TabIndex = 3
        '
        'cmbcomp
        '
        Me.cmbcomp.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcomp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbcomp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcomp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcomp.Items.AddRange(New Object() {"Select Instrument", "OPTIDX", "OPTSTK", "FUTIDX", "FUTSTK", "FUTINT", "FUTIVX"})
        Me.cmbcomp.Location = New System.Drawing.Point(6, 35)
        Me.cmbcomp.Name = "cmbcomp"
        Me.cmbcomp.Size = New System.Drawing.Size(192, 22)
        Me.cmbcomp.TabIndex = 0
        '
        'cmbcp
        '
        Me.cmbcp.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcp.Location = New System.Drawing.Point(348, 35)
        Me.cmbcp.Name = "cmbcp"
        Me.cmbcp.Size = New System.Drawing.Size(111, 22)
        Me.cmbcp.TabIndex = 2
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label19.Location = New System.Drawing.Point(358, 15)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(62, 14)
        Me.Label19.TabIndex = 66
        Me.Label19.Text = "Call/Put"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label20.Location = New System.Drawing.Point(68, 15)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(62, 14)
        Me.Label20.TabIndex = 58
        Me.Label20.Text = "Security"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label21.Location = New System.Drawing.Point(626, 15)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(49, 14)
        Me.Label21.TabIndex = 59
        Me.Label21.Text = "Expiry"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label22.Location = New System.Drawing.Point(211, 15)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(122, 14)
        Me.Label22.TabIndex = 56
        Me.Label22.Text = "Instrument Name"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtscript
        '
        Me.txtscript.BackColor = System.Drawing.SystemColors.Window
        Me.txtscript.Enabled = False
        Me.txtscript.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtscript.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtscript.Location = New System.Drawing.Point(4, 104)
        Me.txtscript.Name = "txtscript"
        Me.txtscript.Size = New System.Drawing.Size(336, 22)
        Me.txtscript.TabIndex = 5
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label23.Location = New System.Drawing.Point(484, 15)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(85, 14)
        Me.Label23.TabIndex = 61
        Me.Label23.Text = "Strike Price"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label24.Location = New System.Drawing.Point(149, 87)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(41, 14)
        Me.Label24.TabIndex = 65
        Me.Label24.Text = "Scrip"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbinstrument
        '
        Me.cmbinstrument.BackColor = System.Drawing.SystemColors.Window
        Me.cmbinstrument.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbinstrument.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbinstrument.Location = New System.Drawing.Point(202, 35)
        Me.cmbinstrument.Name = "cmbinstrument"
        Me.cmbinstrument.Size = New System.Drawing.Size(140, 22)
        Me.cmbinstrument.TabIndex = 1
        '
        'lblsvalue2
        '
        Me.lblsvalue2.AutoSize = True
        Me.lblsvalue2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsvalue2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblsvalue2.Location = New System.Drawing.Point(676, 87)
        Me.lblsvalue2.Name = "lblsvalue2"
        Me.lblsvalue2.Size = New System.Drawing.Size(59, 14)
        Me.lblsvalue2.TabIndex = 54
        Me.lblsvalue2.Text = "Value-2"
        Me.lblsvalue2.Visible = False
        '
        'txtsevalue
        '
        Me.txtsevalue.BackColor = System.Drawing.SystemColors.Window
        Me.txtsevalue.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtsevalue.Location = New System.Drawing.Point(672, 107)
        Me.txtsevalue.Name = "txtsevalue"
        Me.txtsevalue.Size = New System.Drawing.Size(67, 20)
        Me.txtsevalue.TabIndex = 10
        Me.txtsevalue.Text = "0"
        Me.txtsevalue.Visible = False
        '
        'cmbsopt
        '
        Me.cmbsopt.BackColor = System.Drawing.SystemColors.Window
        Me.cmbsopt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsopt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbsopt.Items.AddRange(New Object() {"Select", "Between", "Equal to", "Greater than", "Less than", "Greater than or equal to", "Less than or equal to"})
        Me.cmbsopt.Location = New System.Drawing.Point(341, 105)
        Me.cmbsopt.Name = "cmbsopt"
        Me.cmbsopt.Size = New System.Drawing.Size(162, 21)
        Me.cmbsopt.TabIndex = 7
        '
        'lblsvalue1
        '
        Me.lblsvalue1.AutoSize = True
        Me.lblsvalue1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsvalue1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblsvalue1.Location = New System.Drawing.Point(616, 87)
        Me.lblsvalue1.Name = "lblsvalue1"
        Me.lblsvalue1.Size = New System.Drawing.Size(44, 14)
        Me.lblsvalue1.TabIndex = 50
        Me.lblsvalue1.Text = "Value"
        '
        'txtssvalue
        '
        Me.txtssvalue.BackColor = System.Drawing.SystemColors.Window
        Me.txtssvalue.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtssvalue.Location = New System.Drawing.Point(602, 107)
        Me.txtssvalue.Name = "txtssvalue"
        Me.txtssvalue.Size = New System.Drawing.Size(66, 20)
        Me.txtssvalue.TabIndex = 9
        Me.txtssvalue.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label7.Location = New System.Drawing.Point(533, 87)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 14)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Field"
        '
        'cmbsfield
        '
        Me.cmbsfield.BackColor = System.Drawing.SystemColors.Window
        Me.cmbsfield.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsfield.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbsfield.Items.AddRange(New Object() {"Select", "Delta", "Theta", "Vega", "Gamma", "Volga", "Vanna", "Vol"})
        Me.cmbsfield.Location = New System.Drawing.Point(506, 105)
        Me.cmbsfield.Name = "cmbsfield"
        Me.cmbsfield.Size = New System.Drawing.Size(93, 21)
        Me.cmbsfield.TabIndex = 8
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label9.Location = New System.Drawing.Point(382, 87)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 14)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Operator"
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.SystemColors.WindowText
        Me.btnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnUpdate.FlatAppearance.BorderSize = 3
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdate.ForeColor = System.Drawing.SystemColors.Window
        Me.btnUpdate.Location = New System.Drawing.Point(5, 278)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(1)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(85, 27)
        Me.btnUpdate.TabIndex = 13
        Me.btnUpdate.Text = "Update Alerts"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'cmdsave
        '
        Me.cmdsave.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmdsave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdsave.FlatAppearance.BorderSize = 3
        Me.cmdsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdsave.ForeColor = System.Drawing.SystemColors.Window
        Me.cmdsave.Location = New System.Drawing.Point(530, 278)
        Me.cmdsave.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(49, 27)
        Me.cmdsave.TabIndex = 13
        Me.cmdsave.Text = "Save"
        Me.cmdsave.UseVisualStyleBackColor = False
        '
        'cmbdelete
        '
        Me.cmbdelete.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmbdelete.CausesValidation = False
        Me.cmbdelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbdelete.FlatAppearance.BorderSize = 3
        Me.cmbdelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbdelete.ForeColor = System.Drawing.SystemColors.Window
        Me.cmbdelete.Location = New System.Drawing.Point(681, 278)
        Me.cmbdelete.Margin = New System.Windows.Forms.Padding(1)
        Me.cmbdelete.Name = "cmbdelete"
        Me.cmbdelete.Size = New System.Drawing.Size(56, 27)
        Me.cmbdelete.TabIndex = 16
        Me.cmbdelete.Text = "Delete"
        Me.cmbdelete.UseVisualStyleBackColor = False
        '
        'pancomp
        '
        Me.pancomp.BackColor = System.Drawing.Color.Transparent
        Me.pancomp.Controls.Add(Me.lbluid)
        Me.pancomp.Controls.Add(Me.lblcvalue2)
        Me.pancomp.Controls.Add(Me.lblcvalue1)
        Me.pancomp.Controls.Add(Me.txtcevalue)
        Me.pancomp.Controls.Add(Me.txtcsvalue)
        Me.pancomp.Controls.Add(Me.Label5)
        Me.pancomp.Controls.Add(Me.cmbcopt)
        Me.pancomp.Controls.Add(Me.Label3)
        Me.pancomp.Controls.Add(Me.cmbcfield)
        Me.pancomp.Controls.Add(Me.Label1)
        Me.pancomp.Controls.Add(Me.cmbcompany)
        Me.pancomp.Location = New System.Drawing.Point(2, 39)
        Me.pancomp.Name = "pancomp"
        Me.pancomp.Size = New System.Drawing.Size(742, 61)
        Me.pancomp.TabIndex = 11
        '
        'lbluid
        '
        Me.lbluid.AutoSize = True
        Me.lbluid.Location = New System.Drawing.Point(676, 36)
        Me.lbluid.Name = "lbluid"
        Me.lbluid.Size = New System.Drawing.Size(13, 13)
        Me.lbluid.TabIndex = 10
        Me.lbluid.Text = "0"
        Me.lbluid.Visible = False
        '
        'lblcvalue2
        '
        Me.lblcvalue2.AutoSize = True
        Me.lblcvalue2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcvalue2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblcvalue2.Location = New System.Drawing.Point(525, 9)
        Me.lblcvalue2.Name = "lblcvalue2"
        Me.lblcvalue2.Size = New System.Drawing.Size(59, 14)
        Me.lblcvalue2.TabIndex = 56
        Me.lblcvalue2.Text = "Value-2"
        Me.lblcvalue2.Visible = False
        '
        'lblcvalue1
        '
        Me.lblcvalue1.AutoSize = True
        Me.lblcvalue1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcvalue1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblcvalue1.Location = New System.Drawing.Point(456, 9)
        Me.lblcvalue1.Name = "lblcvalue1"
        Me.lblcvalue1.Size = New System.Drawing.Size(44, 14)
        Me.lblcvalue1.TabIndex = 53
        Me.lblcvalue1.Text = "Value"
        '
        'txtcevalue
        '
        Me.txtcevalue.BackColor = System.Drawing.SystemColors.Window
        Me.txtcevalue.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtcevalue.Location = New System.Drawing.Point(519, 33)
        Me.txtcevalue.Name = "txtcevalue"
        Me.txtcevalue.Size = New System.Drawing.Size(70, 20)
        Me.txtcevalue.TabIndex = 4
        Me.txtcevalue.Text = "0"
        Me.txtcevalue.Visible = False
        '
        'txtcsvalue
        '
        Me.txtcsvalue.BackColor = System.Drawing.SystemColors.Window
        Me.txtcsvalue.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtcsvalue.Location = New System.Drawing.Point(443, 33)
        Me.txtcsvalue.Name = "txtcsvalue"
        Me.txtcsvalue.Size = New System.Drawing.Size(70, 20)
        Me.txtcsvalue.TabIndex = 3
        Me.txtcsvalue.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label5.Location = New System.Drawing.Point(207, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 14)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Field"
        '
        'cmbcopt
        '
        Me.cmbcopt.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcopt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcopt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcopt.Items.AddRange(New Object() {"Select", "Between", "Equal to", "Greater than", "Less than", "Greater than or equal to", "Less than or equal to"})
        Me.cmbcopt.Location = New System.Drawing.Point(279, 33)
        Me.cmbcopt.Name = "cmbcopt"
        Me.cmbcopt.Size = New System.Drawing.Size(157, 21)
        Me.cmbcopt.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label3.Location = New System.Drawing.Point(324, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Operator"
        '
        'cmbcfield
        '
        Me.cmbcfield.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcfield.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcfield.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcfield.Items.AddRange(New Object() {"Select", "Delta", "Theta", "Vega", "Gamma", "Volga", "Vanna", "GrossMTM", "Vol"})
        Me.cmbcfield.Location = New System.Drawing.Point(180, 33)
        Me.cmbcfield.Name = "cmbcfield"
        Me.cmbcfield.Size = New System.Drawing.Size(93, 21)
        Me.cmbcfield.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(57, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 14)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Security"
        '
        'cmbcompany
        '
        Me.cmbcompany.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcompany.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcompany.Location = New System.Drawing.Point(3, 33)
        Me.cmbcompany.Name = "cmbcompany"
        Me.cmbcompany.Size = New System.Drawing.Size(171, 21)
        Me.cmbcompany.TabIndex = 0
        '
        'rdbscript
        '
        Me.rdbscript.AutoSize = True
        Me.rdbscript.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.rdbscript.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbscript.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdbscript.Location = New System.Drawing.Point(153, 6)
        Me.rdbscript.Name = "rdbscript"
        Me.rdbscript.Size = New System.Drawing.Size(86, 17)
        Me.rdbscript.TabIndex = 10
        Me.rdbscript.Text = "Scrip Wise"
        Me.rdbscript.UseVisualStyleBackColor = False
        '
        'rdbcomp
        '
        Me.rdbcomp.AutoSize = True
        Me.rdbcomp.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.rdbcomp.Checked = True
        Me.rdbcomp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbcomp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdbcomp.Location = New System.Drawing.Point(43, 6)
        Me.rdbcomp.Name = "rdbcomp"
        Me.rdbcomp.Size = New System.Drawing.Size(103, 17)
        Me.rdbcomp.TabIndex = 9
        Me.rdbcomp.TabStop = True
        Me.rdbcomp.Text = "Security Wise"
        Me.rdbcomp.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TabPage2.Controls.Add(Me.cmdcurrexit)
        Me.TabPage2.Controls.Add(Me.cmdcurrclear)
        Me.TabPage2.Controls.Add(Me.pancurrscript)
        Me.TabPage2.Controls.Add(Me.cmdcurrsave)
        Me.TabPage2.Controls.Add(Me.cmbcurrdelete)
        Me.TabPage2.Controls.Add(Me.pancurrcomp)
        Me.TabPage2.Controls.Add(Me.rdbcurrscript)
        Me.TabPage2.Controls.Add(Me.rdbcurrcomp)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(749, 309)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Currency"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cmdcurrexit
        '
        Me.cmdcurrexit.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmdcurrexit.CausesValidation = False
        Me.cmdcurrexit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdcurrexit.FlatAppearance.BorderSize = 3
        Me.cmdcurrexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdcurrexit.ForeColor = System.Drawing.SystemColors.Window
        Me.cmdcurrexit.Location = New System.Drawing.Point(631, 277)
        Me.cmdcurrexit.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdcurrexit.Name = "cmdcurrexit"
        Me.cmdcurrexit.Size = New System.Drawing.Size(48, 27)
        Me.cmdcurrexit.TabIndex = 15
        Me.cmdcurrexit.Text = "Exit"
        Me.cmdcurrexit.UseVisualStyleBackColor = False
        '
        'cmdcurrclear
        '
        Me.cmdcurrclear.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmdcurrclear.CausesValidation = False
        Me.cmdcurrclear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdcurrclear.FlatAppearance.BorderSize = 3
        Me.cmdcurrclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdcurrclear.ForeColor = System.Drawing.SystemColors.Window
        Me.cmdcurrclear.Location = New System.Drawing.Point(581, 277)
        Me.cmdcurrclear.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdcurrclear.Name = "cmdcurrclear"
        Me.cmdcurrclear.Size = New System.Drawing.Size(48, 27)
        Me.cmdcurrclear.TabIndex = 14
        Me.cmdcurrclear.Text = "Clear"
        Me.cmdcurrclear.UseVisualStyleBackColor = False
        '
        'pancurrscript
        '
        Me.pancurrscript.BackColor = System.Drawing.Color.Transparent
        Me.pancurrscript.Controls.Add(Me.Label4)
        Me.pancurrscript.Controls.Add(Me.txtcurrunits)
        Me.pancurrscript.Controls.Add(Me.cmbcurrdate)
        Me.pancurrscript.Controls.Add(Me.cmbcurrstrike)
        Me.pancurrscript.Controls.Add(Me.cmbcurrcomp)
        Me.pancurrscript.Controls.Add(Me.cmbcurrcp)
        Me.pancurrscript.Controls.Add(Me.Label6)
        Me.pancurrscript.Controls.Add(Me.Label8)
        Me.pancurrscript.Controls.Add(Me.Label10)
        Me.pancurrscript.Controls.Add(Me.Label11)
        Me.pancurrscript.Controls.Add(Me.txtcurrscript)
        Me.pancurrscript.Controls.Add(Me.Label12)
        Me.pancurrscript.Controls.Add(Me.Label13)
        Me.pancurrscript.Controls.Add(Me.cmbcurrinstrument)
        Me.pancurrscript.Controls.Add(Me.lblcurrsvalue2)
        Me.pancurrscript.Controls.Add(Me.txtcurrsevalue)
        Me.pancurrscript.Controls.Add(Me.cmbcurrsopt)
        Me.pancurrscript.Controls.Add(Me.lblcurrsvalue1)
        Me.pancurrscript.Controls.Add(Me.txtcurrssvalue)
        Me.pancurrscript.Controls.Add(Me.Label16)
        Me.pancurrscript.Controls.Add(Me.cmbcurrsfield)
        Me.pancurrscript.Controls.Add(Me.Label17)
        Me.pancurrscript.Location = New System.Drawing.Point(2, 100)
        Me.pancurrscript.Name = "pancurrscript"
        Me.pancurrscript.Size = New System.Drawing.Size(742, 173)
        Me.pancurrscript.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(324, 146)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 14)
        Me.Label4.TabIndex = 68
        Me.Label4.Text = "Units"
        Me.Label4.Visible = False
        '
        'txtcurrunits
        '
        Me.txtcurrunits.Location = New System.Drawing.Point(371, 144)
        Me.txtcurrunits.Name = "txtcurrunits"
        Me.txtcurrunits.Size = New System.Drawing.Size(67, 20)
        Me.txtcurrunits.TabIndex = 6
        Me.txtcurrunits.Text = "0"
        Me.txtcurrunits.Visible = False
        '
        'cmbcurrdate
        '
        Me.cmbcurrdate.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcurrdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcurrdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcurrdate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcurrdate.Location = New System.Drawing.Point(593, 35)
        Me.cmbcurrdate.Name = "cmbcurrdate"
        Me.cmbcurrdate.Size = New System.Drawing.Size(115, 22)
        Me.cmbcurrdate.TabIndex = 4
        '
        'cmbcurrstrike
        '
        Me.cmbcurrstrike.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcurrstrike.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbcurrstrike.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcurrstrike.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcurrstrike.Location = New System.Drawing.Point(465, 35)
        Me.cmbcurrstrike.Name = "cmbcurrstrike"
        Me.cmbcurrstrike.Size = New System.Drawing.Size(122, 22)
        Me.cmbcurrstrike.TabIndex = 3
        '
        'cmbcurrcomp
        '
        Me.cmbcurrcomp.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcurrcomp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbcurrcomp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcurrcomp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcurrcomp.Items.AddRange(New Object() {"Select Instrument", "OPTIDX", "OPTSTK", "FUTIDX", "FUTSTK", "FUTINT", "FUTIVX"})
        Me.cmbcurrcomp.Location = New System.Drawing.Point(3, 35)
        Me.cmbcurrcomp.Name = "cmbcurrcomp"
        Me.cmbcurrcomp.Size = New System.Drawing.Size(192, 22)
        Me.cmbcurrcomp.TabIndex = 0
        '
        'cmbcurrcp
        '
        Me.cmbcurrcp.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcurrcp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcurrcp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcurrcp.Location = New System.Drawing.Point(348, 35)
        Me.cmbcurrcp.Name = "cmbcurrcp"
        Me.cmbcurrcp.Size = New System.Drawing.Size(111, 22)
        Me.cmbcurrcp.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label6.Location = New System.Drawing.Point(358, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 14)
        Me.Label6.TabIndex = 66
        Me.Label6.Text = "Call/Put"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label8.Location = New System.Drawing.Point(68, 15)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(62, 14)
        Me.Label8.TabIndex = 58
        Me.Label8.Text = "Security"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label10.Location = New System.Drawing.Point(626, 15)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(49, 14)
        Me.Label10.TabIndex = 59
        Me.Label10.Text = "Expiry"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label11.Location = New System.Drawing.Point(211, 15)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(122, 14)
        Me.Label11.TabIndex = 56
        Me.Label11.Text = "Instrument Name"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtcurrscript
        '
        Me.txtcurrscript.BackColor = System.Drawing.SystemColors.Window
        Me.txtcurrscript.Enabled = False
        Me.txtcurrscript.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcurrscript.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtcurrscript.Location = New System.Drawing.Point(3, 104)
        Me.txtcurrscript.Name = "txtcurrscript"
        Me.txtcurrscript.Size = New System.Drawing.Size(336, 22)
        Me.txtcurrscript.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label12.Location = New System.Drawing.Point(484, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(85, 14)
        Me.Label12.TabIndex = 61
        Me.Label12.Text = "Strike Price"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label13.Location = New System.Drawing.Point(149, 87)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(41, 14)
        Me.Label13.TabIndex = 65
        Me.Label13.Text = "Scrip"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbcurrinstrument
        '
        Me.cmbcurrinstrument.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcurrinstrument.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcurrinstrument.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcurrinstrument.Location = New System.Drawing.Point(202, 35)
        Me.cmbcurrinstrument.Name = "cmbcurrinstrument"
        Me.cmbcurrinstrument.Size = New System.Drawing.Size(140, 22)
        Me.cmbcurrinstrument.TabIndex = 1
        '
        'lblcurrsvalue2
        '
        Me.lblcurrsvalue2.AutoSize = True
        Me.lblcurrsvalue2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcurrsvalue2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblcurrsvalue2.Location = New System.Drawing.Point(676, 87)
        Me.lblcurrsvalue2.Name = "lblcurrsvalue2"
        Me.lblcurrsvalue2.Size = New System.Drawing.Size(59, 14)
        Me.lblcurrsvalue2.TabIndex = 54
        Me.lblcurrsvalue2.Text = "Value-2"
        Me.lblcurrsvalue2.Visible = False
        '
        'txtcurrsevalue
        '
        Me.txtcurrsevalue.BackColor = System.Drawing.SystemColors.Window
        Me.txtcurrsevalue.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtcurrsevalue.Location = New System.Drawing.Point(672, 107)
        Me.txtcurrsevalue.Name = "txtcurrsevalue"
        Me.txtcurrsevalue.Size = New System.Drawing.Size(67, 20)
        Me.txtcurrsevalue.TabIndex = 10
        Me.txtcurrsevalue.Text = "0"
        Me.txtcurrsevalue.Visible = False
        '
        'cmbcurrsopt
        '
        Me.cmbcurrsopt.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcurrsopt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcurrsopt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcurrsopt.Items.AddRange(New Object() {"Select", "Between", "Equal to", "Greater than", "Less than", "Greater than or equal to", "Less than or equal to"})
        Me.cmbcurrsopt.Location = New System.Drawing.Point(341, 105)
        Me.cmbcurrsopt.Name = "cmbcurrsopt"
        Me.cmbcurrsopt.Size = New System.Drawing.Size(162, 21)
        Me.cmbcurrsopt.TabIndex = 7
        '
        'lblcurrsvalue1
        '
        Me.lblcurrsvalue1.AutoSize = True
        Me.lblcurrsvalue1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcurrsvalue1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblcurrsvalue1.Location = New System.Drawing.Point(616, 87)
        Me.lblcurrsvalue1.Name = "lblcurrsvalue1"
        Me.lblcurrsvalue1.Size = New System.Drawing.Size(44, 14)
        Me.lblcurrsvalue1.TabIndex = 50
        Me.lblcurrsvalue1.Text = "Value"
        '
        'txtcurrssvalue
        '
        Me.txtcurrssvalue.BackColor = System.Drawing.SystemColors.Window
        Me.txtcurrssvalue.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtcurrssvalue.Location = New System.Drawing.Point(602, 107)
        Me.txtcurrssvalue.Name = "txtcurrssvalue"
        Me.txtcurrssvalue.Size = New System.Drawing.Size(66, 20)
        Me.txtcurrssvalue.TabIndex = 9
        Me.txtcurrssvalue.Text = "0"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label16.Location = New System.Drawing.Point(533, 87)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(39, 14)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "Field"
        '
        'cmbcurrsfield
        '
        Me.cmbcurrsfield.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcurrsfield.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcurrsfield.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcurrsfield.Items.AddRange(New Object() {"Select", "Delta", "Theta", "Vega", "Gamma", "Volga", "Vanna"})
        Me.cmbcurrsfield.Location = New System.Drawing.Point(506, 105)
        Me.cmbcurrsfield.Name = "cmbcurrsfield"
        Me.cmbcurrsfield.Size = New System.Drawing.Size(93, 21)
        Me.cmbcurrsfield.TabIndex = 8
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label17.Location = New System.Drawing.Point(382, 87)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(67, 14)
        Me.Label17.TabIndex = 10
        Me.Label17.Text = "Operator"
        '
        'cmdcurrsave
        '
        Me.cmdcurrsave.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmdcurrsave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdcurrsave.FlatAppearance.BorderSize = 3
        Me.cmdcurrsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdcurrsave.ForeColor = System.Drawing.SystemColors.Window
        Me.cmdcurrsave.Location = New System.Drawing.Point(530, 277)
        Me.cmdcurrsave.Margin = New System.Windows.Forms.Padding(1)
        Me.cmdcurrsave.Name = "cmdcurrsave"
        Me.cmdcurrsave.Size = New System.Drawing.Size(49, 27)
        Me.cmdcurrsave.TabIndex = 13
        Me.cmdcurrsave.Text = "Save"
        Me.cmdcurrsave.UseVisualStyleBackColor = False
        '
        'cmbcurrdelete
        '
        Me.cmbcurrdelete.BackColor = System.Drawing.SystemColors.WindowText
        Me.cmbcurrdelete.CausesValidation = False
        Me.cmbcurrdelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbcurrdelete.FlatAppearance.BorderSize = 3
        Me.cmbcurrdelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbcurrdelete.ForeColor = System.Drawing.SystemColors.Window
        Me.cmbcurrdelete.Location = New System.Drawing.Point(681, 277)
        Me.cmbcurrdelete.Margin = New System.Windows.Forms.Padding(1)
        Me.cmbcurrdelete.Name = "cmbcurrdelete"
        Me.cmbcurrdelete.Size = New System.Drawing.Size(56, 27)
        Me.cmbcurrdelete.TabIndex = 16
        Me.cmbcurrdelete.Text = "Delete"
        Me.cmbcurrdelete.UseVisualStyleBackColor = False
        '
        'pancurrcomp
        '
        Me.pancurrcomp.BackColor = System.Drawing.Color.Transparent
        Me.pancurrcomp.Controls.Add(Me.lblcurruid)
        Me.pancurrcomp.Controls.Add(Me.lblcurrcvalue2)
        Me.pancurrcomp.Controls.Add(Me.lblcurrcvalue1)
        Me.pancurrcomp.Controls.Add(Me.txtcurrcevalue)
        Me.pancurrcomp.Controls.Add(Me.txtcurrcsvalue)
        Me.pancurrcomp.Controls.Add(Me.Label27)
        Me.pancurrcomp.Controls.Add(Me.cmbcurrcopt)
        Me.pancurrcomp.Controls.Add(Me.Label28)
        Me.pancurrcomp.Controls.Add(Me.cmbcurrcfield)
        Me.pancurrcomp.Controls.Add(Me.Label29)
        Me.pancurrcomp.Controls.Add(Me.cmbcurrcompany)
        Me.pancurrcomp.Location = New System.Drawing.Point(2, 38)
        Me.pancurrcomp.Name = "pancurrcomp"
        Me.pancurrcomp.Size = New System.Drawing.Size(742, 61)
        Me.pancurrcomp.TabIndex = 11
        '
        'lblcurruid
        '
        Me.lblcurruid.AutoSize = True
        Me.lblcurruid.Location = New System.Drawing.Point(676, 36)
        Me.lblcurruid.Name = "lblcurruid"
        Me.lblcurruid.Size = New System.Drawing.Size(13, 13)
        Me.lblcurruid.TabIndex = 10
        Me.lblcurruid.Text = "0"
        Me.lblcurruid.Visible = False
        '
        'lblcurrcvalue2
        '
        Me.lblcurrcvalue2.AutoSize = True
        Me.lblcurrcvalue2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcurrcvalue2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblcurrcvalue2.Location = New System.Drawing.Point(525, 9)
        Me.lblcurrcvalue2.Name = "lblcurrcvalue2"
        Me.lblcurrcvalue2.Size = New System.Drawing.Size(59, 14)
        Me.lblcurrcvalue2.TabIndex = 56
        Me.lblcurrcvalue2.Text = "Value-2"
        Me.lblcurrcvalue2.Visible = False
        '
        'lblcurrcvalue1
        '
        Me.lblcurrcvalue1.AutoSize = True
        Me.lblcurrcvalue1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcurrcvalue1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblcurrcvalue1.Location = New System.Drawing.Point(456, 9)
        Me.lblcurrcvalue1.Name = "lblcurrcvalue1"
        Me.lblcurrcvalue1.Size = New System.Drawing.Size(44, 14)
        Me.lblcurrcvalue1.TabIndex = 53
        Me.lblcurrcvalue1.Text = "Value"
        '
        'txtcurrcevalue
        '
        Me.txtcurrcevalue.BackColor = System.Drawing.SystemColors.Window
        Me.txtcurrcevalue.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtcurrcevalue.Location = New System.Drawing.Point(519, 33)
        Me.txtcurrcevalue.Name = "txtcurrcevalue"
        Me.txtcurrcevalue.Size = New System.Drawing.Size(70, 20)
        Me.txtcurrcevalue.TabIndex = 4
        Me.txtcurrcevalue.Text = "0"
        Me.txtcurrcevalue.Visible = False
        '
        'txtcurrcsvalue
        '
        Me.txtcurrcsvalue.BackColor = System.Drawing.SystemColors.Window
        Me.txtcurrcsvalue.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtcurrcsvalue.Location = New System.Drawing.Point(443, 33)
        Me.txtcurrcsvalue.Name = "txtcurrcsvalue"
        Me.txtcurrcsvalue.Size = New System.Drawing.Size(70, 20)
        Me.txtcurrcsvalue.TabIndex = 3
        Me.txtcurrcsvalue.Text = "0"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label27.Location = New System.Drawing.Point(207, 9)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(39, 14)
        Me.Label27.TabIndex = 13
        Me.Label27.Text = "Field"
        '
        'cmbcurrcopt
        '
        Me.cmbcurrcopt.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcurrcopt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcurrcopt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcurrcopt.Items.AddRange(New Object() {"Select", "Between", "Equal to", "Greater than", "Less than", "Greater than or equal to", "Less than or equal to"})
        Me.cmbcurrcopt.Location = New System.Drawing.Point(279, 33)
        Me.cmbcurrcopt.Name = "cmbcurrcopt"
        Me.cmbcurrcopt.Size = New System.Drawing.Size(157, 21)
        Me.cmbcurrcopt.TabIndex = 2
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label28.Location = New System.Drawing.Point(324, 9)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(67, 14)
        Me.Label28.TabIndex = 10
        Me.Label28.Text = "Operator"
        '
        'cmbcurrcfield
        '
        Me.cmbcurrcfield.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcurrcfield.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcurrcfield.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcurrcfield.Items.AddRange(New Object() {"Select", "Delta", "Theta", "Vega", "Gamma", "Volga", "Vanna", "GrossMTM"})
        Me.cmbcurrcfield.Location = New System.Drawing.Point(180, 33)
        Me.cmbcurrcfield.Name = "cmbcurrcfield"
        Me.cmbcurrcfield.Size = New System.Drawing.Size(93, 21)
        Me.cmbcurrcfield.TabIndex = 1
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label29.Location = New System.Drawing.Point(57, 9)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(62, 14)
        Me.Label29.TabIndex = 7
        Me.Label29.Text = "Security"
        '
        'cmbcurrcompany
        '
        Me.cmbcurrcompany.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcurrcompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcurrcompany.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcurrcompany.Location = New System.Drawing.Point(3, 33)
        Me.cmbcurrcompany.Name = "cmbcurrcompany"
        Me.cmbcurrcompany.Size = New System.Drawing.Size(171, 21)
        Me.cmbcurrcompany.TabIndex = 0
        '
        'rdbcurrscript
        '
        Me.rdbcurrscript.AutoSize = True
        Me.rdbcurrscript.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.rdbcurrscript.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbcurrscript.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdbcurrscript.Location = New System.Drawing.Point(153, 5)
        Me.rdbcurrscript.Name = "rdbcurrscript"
        Me.rdbcurrscript.Size = New System.Drawing.Size(86, 17)
        Me.rdbcurrscript.TabIndex = 10
        Me.rdbcurrscript.Text = "Scrip Wise"
        Me.rdbcurrscript.UseVisualStyleBackColor = False
        '
        'rdbcurrcomp
        '
        Me.rdbcurrcomp.AutoSize = True
        Me.rdbcurrcomp.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.rdbcurrcomp.Checked = True
        Me.rdbcurrcomp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbcurrcomp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdbcurrcomp.Location = New System.Drawing.Point(43, 5)
        Me.rdbcurrcomp.Name = "rdbcurrcomp"
        Me.rdbcurrcomp.Size = New System.Drawing.Size(103, 17)
        Me.rdbcurrcomp.TabIndex = 9
        Me.rdbcurrcomp.TabStop = True
        Me.rdbcurrcomp.Text = "Security Wise"
        Me.rdbcurrcomp.UseVisualStyleBackColor = False
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "value2"
        Me.DataGridViewTextBoxColumn10.HeaderText = "Value-2"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 60
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "value1"
        Me.DataGridViewTextBoxColumn11.HeaderText = "Value-1"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Width = 60
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "cs"
        Me.DataGridViewTextBoxColumn12.HeaderText = "cs"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Visible = False
        Me.DataGridViewTextBoxColumn12.Width = 5
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "entrydate"
        Me.DataGridViewTextBoxColumn13.HeaderText = "entrydate"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Visible = False
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "uid"
        Me.DataGridViewTextBoxColumn14.HeaderText = "uid"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Visible = False
        Me.DataGridViewTextBoxColumn14.Width = 60
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "opt"
        Me.DataGridViewTextBoxColumn15.HeaderText = "Operator"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.Width = 180
        '
        'DataGridViewCheckBoxColumn2
        '
        Me.DataGridViewCheckBoxColumn2.DataPropertyName = "status"
        Me.DataGridViewCheckBoxColumn2.HeaderText = ""
        Me.DataGridViewCheckBoxColumn2.Name = "DataGridViewCheckBoxColumn2"
        Me.DataGridViewCheckBoxColumn2.Width = 35
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "field"
        Me.DataGridViewTextBoxColumn16.HeaderText = "Field"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "units"
        Me.DataGridViewTextBoxColumn17.HeaderText = "units"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Width = 60
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "comp_script"
        Me.DataGridViewTextBoxColumn18.HeaderText = "Security/Scrip"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        Me.DataGridViewTextBoxColumn18.Width = 230
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.DataPropertyName = "value2"
        Me.DataGridViewTextBoxColumn19.HeaderText = "Value-2"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.ReadOnly = True
        Me.DataGridViewTextBoxColumn19.Width = 60
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.DataPropertyName = "value1"
        Me.DataGridViewTextBoxColumn20.HeaderText = "Value-1"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.ReadOnly = True
        Me.DataGridViewTextBoxColumn20.Width = 60
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.DataPropertyName = "cs"
        Me.DataGridViewTextBoxColumn21.HeaderText = "cs"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.ReadOnly = True
        Me.DataGridViewTextBoxColumn21.Visible = False
        Me.DataGridViewTextBoxColumn21.Width = 5
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.DataPropertyName = "entrydate"
        Me.DataGridViewTextBoxColumn22.HeaderText = "entrydate"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.ReadOnly = True
        Me.DataGridViewTextBoxColumn22.Visible = False
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.DataPropertyName = "uid"
        Me.DataGridViewTextBoxColumn23.HeaderText = "uid"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.ReadOnly = True
        Me.DataGridViewTextBoxColumn23.Visible = False
        Me.DataGridViewTextBoxColumn23.Width = 60
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.DataPropertyName = "opt"
        Me.DataGridViewTextBoxColumn24.HeaderText = "Operator"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.ReadOnly = True
        Me.DataGridViewTextBoxColumn24.Width = 180
        '
        'DataGridViewCheckBoxColumn3
        '
        Me.DataGridViewCheckBoxColumn3.DataPropertyName = "status"
        Me.DataGridViewCheckBoxColumn3.HeaderText = ""
        Me.DataGridViewCheckBoxColumn3.Name = "DataGridViewCheckBoxColumn3"
        Me.DataGridViewCheckBoxColumn3.Width = 35
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.DataPropertyName = "field"
        Me.DataGridViewTextBoxColumn25.HeaderText = "Field"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.ReadOnly = True
        '
        'DataGridViewTextBoxColumn26
        '
        Me.DataGridViewTextBoxColumn26.DataPropertyName = "units"
        Me.DataGridViewTextBoxColumn26.HeaderText = "units"
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.ReadOnly = True
        Me.DataGridViewTextBoxColumn26.Width = 60
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.DataPropertyName = "comp_script"
        Me.DataGridViewTextBoxColumn27.HeaderText = "Security/Scrip"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.ReadOnly = True
        Me.DataGridViewTextBoxColumn27.Width = 230
        '
        'grdalert
        '
        Me.grdalert.AllowUserToAddRows = False
        Me.grdalert.AllowUserToDeleteRows = False
        Me.grdalert.AllowUserToResizeColumns = False
        Me.grdalert.AllowUserToResizeRows = False
        Me.grdalert.BackgroundColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdalert.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdalert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdalert.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colStatus, Me.DataGridViewTextBoxColumn28, Me.DataGridViewTextBoxColumn29, Me.DataGridViewTextBoxColumn30, Me.DataGridViewTextBoxColumn31, Me.DataGridViewTextBoxColumn32, Me.DataGridViewTextBoxColumn33, Me.DataGridViewTextBoxColumn34, Me.DataGridViewTextBoxColumn35, Me.DataGridViewTextBoxColumn36})
        Me.grdalert.GridColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.grdalert.Location = New System.Drawing.Point(12, 349)
        Me.grdalert.MultiSelect = False
        Me.grdalert.Name = "grdalert"
        Me.grdalert.RowHeadersVisible = False
        Me.grdalert.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdalert.Size = New System.Drawing.Size(757, 178)
        Me.grdalert.TabIndex = 8
        '
        'colStatus
        '
        Me.colStatus.DataPropertyName = "status"
        Me.colStatus.HeaderText = ""
        Me.colStatus.Name = "colStatus"
        Me.colStatus.Width = 35
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.DataPropertyName = "comp_script"
        Me.DataGridViewTextBoxColumn28.HeaderText = "Security/Scrip"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.ReadOnly = True
        Me.DataGridViewTextBoxColumn28.Width = 230
        '
        'DataGridViewTextBoxColumn29
        '
        Me.DataGridViewTextBoxColumn29.DataPropertyName = "units"
        Me.DataGridViewTextBoxColumn29.HeaderText = "Units"
        Me.DataGridViewTextBoxColumn29.Name = "DataGridViewTextBoxColumn29"
        Me.DataGridViewTextBoxColumn29.ReadOnly = True
        Me.DataGridViewTextBoxColumn29.Width = 60
        '
        'DataGridViewTextBoxColumn30
        '
        Me.DataGridViewTextBoxColumn30.DataPropertyName = "field"
        Me.DataGridViewTextBoxColumn30.HeaderText = "Field"
        Me.DataGridViewTextBoxColumn30.Name = "DataGridViewTextBoxColumn30"
        Me.DataGridViewTextBoxColumn30.ReadOnly = True
        '
        'DataGridViewTextBoxColumn31
        '
        Me.DataGridViewTextBoxColumn31.DataPropertyName = "opt"
        Me.DataGridViewTextBoxColumn31.HeaderText = "Operator"
        Me.DataGridViewTextBoxColumn31.Name = "DataGridViewTextBoxColumn31"
        Me.DataGridViewTextBoxColumn31.ReadOnly = True
        Me.DataGridViewTextBoxColumn31.Width = 180
        '
        'DataGridViewTextBoxColumn32
        '
        Me.DataGridViewTextBoxColumn32.DataPropertyName = "value1"
        Me.DataGridViewTextBoxColumn32.HeaderText = "Value-1"
        Me.DataGridViewTextBoxColumn32.Name = "DataGridViewTextBoxColumn32"
        Me.DataGridViewTextBoxColumn32.ReadOnly = True
        Me.DataGridViewTextBoxColumn32.Width = 60
        '
        'DataGridViewTextBoxColumn33
        '
        Me.DataGridViewTextBoxColumn33.DataPropertyName = "value2"
        Me.DataGridViewTextBoxColumn33.HeaderText = "Value-2"
        Me.DataGridViewTextBoxColumn33.Name = "DataGridViewTextBoxColumn33"
        Me.DataGridViewTextBoxColumn33.ReadOnly = True
        Me.DataGridViewTextBoxColumn33.Width = 60
        '
        'DataGridViewTextBoxColumn34
        '
        Me.DataGridViewTextBoxColumn34.DataPropertyName = "cs"
        Me.DataGridViewTextBoxColumn34.HeaderText = "cs"
        Me.DataGridViewTextBoxColumn34.Name = "DataGridViewTextBoxColumn34"
        Me.DataGridViewTextBoxColumn34.ReadOnly = True
        Me.DataGridViewTextBoxColumn34.Visible = False
        Me.DataGridViewTextBoxColumn34.Width = 5
        '
        'DataGridViewTextBoxColumn35
        '
        Me.DataGridViewTextBoxColumn35.DataPropertyName = "uid"
        Me.DataGridViewTextBoxColumn35.HeaderText = "uid"
        Me.DataGridViewTextBoxColumn35.Name = "DataGridViewTextBoxColumn35"
        Me.DataGridViewTextBoxColumn35.ReadOnly = True
        Me.DataGridViewTextBoxColumn35.Visible = False
        Me.DataGridViewTextBoxColumn35.Width = 60
        '
        'DataGridViewTextBoxColumn36
        '
        Me.DataGridViewTextBoxColumn36.DataPropertyName = "entrydate"
        Me.DataGridViewTextBoxColumn36.HeaderText = "entrydate"
        Me.DataGridViewTextBoxColumn36.Name = "DataGridViewTextBoxColumn36"
        Me.DataGridViewTextBoxColumn36.ReadOnly = True
        Me.DataGridViewTextBoxColumn36.Visible = False
        '
        'chkcheck
        '
        Me.chkcheck.Location = New System.Drawing.Point(24, 354)
        Me.chkcheck.Name = "chkcheck"
        Me.chkcheck.Size = New System.Drawing.Size(13, 13)
        Me.chkcheck.TabIndex = 9
        Me.chkcheck.UseVisualStyleBackColor = True
        '
        'alert_entry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.ClientSize = New System.Drawing.Size(779, 538)
        Me.Controls.Add(Me.chkcheck)
        Me.Controls.Add(Me.grdalert)
        Me.Controls.Add(Me.tabpage)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "alert_entry"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Alert Entry"
        Me.tabpage.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.panscript.ResumeLayout(False)
        Me.panscript.PerformLayout()
        Me.pancomp.ResumeLayout(False)
        Me.pancomp.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.pancurrscript.ResumeLayout(False)
        Me.pancurrscript.PerformLayout()
        Me.pancurrcomp.ResumeLayout(False)
        Me.pancurrcomp.PerformLayout()
        CType(Me.grdalert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pstatus As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents comp_script As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents field As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents units As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents opt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents value2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents value1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents entrydate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents uid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tabpage As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents panscript As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtunits As System.Windows.Forms.TextBox
    Friend WithEvents cmbdate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbstrike As System.Windows.Forms.ComboBox
    Friend WithEvents cmbcomp As System.Windows.Forms.ComboBox
    Friend WithEvents cmbcp As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtscript As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents cmbinstrument As System.Windows.Forms.ComboBox
    Friend WithEvents lblsvalue2 As System.Windows.Forms.Label
    Friend WithEvents txtsevalue As System.Windows.Forms.TextBox
    Friend WithEvents cmbsopt As System.Windows.Forms.ComboBox
    Friend WithEvents lblsvalue1 As System.Windows.Forms.Label
    Friend WithEvents txtssvalue As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbsfield As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmdsave As System.Windows.Forms.Button
    Friend WithEvents cmbdelete As System.Windows.Forms.Button
    Friend WithEvents pancomp As System.Windows.Forms.Panel
    Friend WithEvents lbluid As System.Windows.Forms.Label
    Friend WithEvents lblcvalue2 As System.Windows.Forms.Label
    Friend WithEvents lblcvalue1 As System.Windows.Forms.Label
    Friend WithEvents txtcevalue As System.Windows.Forms.TextBox
    Friend WithEvents txtcsvalue As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbcopt As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbcfield As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbcompany As System.Windows.Forms.ComboBox
    Friend WithEvents rdbscript As System.Windows.Forms.RadioButton
    Friend WithEvents rdbcomp As System.Windows.Forms.RadioButton
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdcurrexit As System.Windows.Forms.Button
    Friend WithEvents cmdcurrclear As System.Windows.Forms.Button
    Friend WithEvents pancurrscript As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtcurrunits As System.Windows.Forms.TextBox
    Friend WithEvents cmbcurrdate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbcurrstrike As System.Windows.Forms.ComboBox
    Friend WithEvents cmbcurrcomp As System.Windows.Forms.ComboBox
    Friend WithEvents cmbcurrcp As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtcurrscript As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbcurrinstrument As System.Windows.Forms.ComboBox
    Friend WithEvents lblcurrsvalue2 As System.Windows.Forms.Label
    Friend WithEvents txtcurrsevalue As System.Windows.Forms.TextBox
    Friend WithEvents cmbcurrsopt As System.Windows.Forms.ComboBox
    Friend WithEvents lblcurrsvalue1 As System.Windows.Forms.Label
    Friend WithEvents txtcurrssvalue As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbcurrsfield As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmdcurrsave As System.Windows.Forms.Button
    Friend WithEvents cmbcurrdelete As System.Windows.Forms.Button
    Friend WithEvents pancurrcomp As System.Windows.Forms.Panel
    Friend WithEvents lblcurruid As System.Windows.Forms.Label
    Friend WithEvents lblcurrcvalue2 As System.Windows.Forms.Label
    Friend WithEvents lblcurrcvalue1 As System.Windows.Forms.Label
    Friend WithEvents txtcurrcevalue As System.Windows.Forms.TextBox
    Friend WithEvents txtcurrcsvalue As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents cmbcurrcopt As System.Windows.Forms.ComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents cmbcurrcfield As System.Windows.Forms.ComboBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents cmbcurrcompany As System.Windows.Forms.ComboBox
    Friend WithEvents rdbcurrscript As System.Windows.Forms.RadioButton
    Friend WithEvents rdbcurrcomp As System.Windows.Forms.RadioButton
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn3 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdalert As System.Windows.Forms.DataGridView
    Friend WithEvents chkcheck As System.Windows.Forms.CheckBox
    Friend WithEvents colStatus As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn36 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
End Class
