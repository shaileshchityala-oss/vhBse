Imports System.Data.OleDb
Imports System.IO
Imports System.Threading

Public Class frm_exposure_margin_entry
    Inherits System.Windows.Forms.Form

    Dim cmd As OleDbCommand
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents btnNewAEL As System.Windows.Forms.Button
    Friend WithEvents btnAELContract As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents grdaelcontracts As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InsType2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Symbol3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExpDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StrikePrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OptType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CALevel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ELMPer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents grdnewexp As System.Windows.Forms.DataGridView
    Friend WithEvents uid1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Symbol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InsType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Norm_Margin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Add_Margin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_Margin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grdexp As System.Windows.Forms.DataGridView
    Friend WithEvents compname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents expmag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents uid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtcomp As System.Windows.Forms.TextBox
    Friend WithEvents txtexposure As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TimerAel As System.Windows.Forms.Timer
    Friend WithEvents txtAELImported As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Dim dread As OleDbDataReader

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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    ' Friend WithEvents grddatabase As SourceGrid2.Grid
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_exposure_margin_entry))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.btnNewAEL = New System.Windows.Forms.Button()
        Me.btnAELContract = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtAELImported = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.grdaelcontracts = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InsType2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Symbol3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StrikePrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OptType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CALevel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ELMPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.grdnewexp = New System.Windows.Forms.DataGridView()
        Me.uid1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Symbol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InsType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Norm_Margin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Add_Margin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_Margin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.grdexp = New System.Windows.Forms.DataGridView()
        Me.compname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.expmag = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.uid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtcomp = New System.Windows.Forms.TextBox()
        Me.txtexposure = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TimerAel = New System.Windows.Forms.Timer(Me.components)
        Me.Button5 = New System.Windows.Forms.Button()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdaelcontracts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdnewexp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdexp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(212, 463)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(56, 26)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "&Save"
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(487, 513)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 36)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "E&xit"
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(110, 463)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(94, 27)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "&Import Excel"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 513)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(440, 33)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Note :- While importing exposure margin from excel file, data must be included wi" & _
            "th heading of columns from HTML file."
        '
        'DeleteButton
        '
        Me.DeleteButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeleteButton.Location = New System.Drawing.Point(281, 463)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(51, 26)
        Me.DeleteButton.TabIndex = 5
        Me.DeleteButton.Text = "&Delete"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(14, 463)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(92, 28)
        Me.Button4.TabIndex = 11
        Me.Button4.Text = "Import AEL"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'btnNewAEL
        '
        Me.btnNewAEL.Location = New System.Drawing.Point(6, 469)
        Me.btnNewAEL.Name = "btnNewAEL"
        Me.btnNewAEL.Size = New System.Drawing.Size(98, 28)
        Me.btnNewAEL.TabIndex = 12
        Me.btnNewAEL.Text = "Import New AEL"
        Me.btnNewAEL.UseVisualStyleBackColor = True
        '
        'btnAELContract
        '
        Me.btnAELContract.Location = New System.Drawing.Point(156, 463)
        Me.btnAELContract.Name = "btnAELContract"
        Me.btnAELContract.Size = New System.Drawing.Size(98, 26)
        Me.btnAELContract.TabIndex = 13
        Me.btnAELContract.Text = "Imp AEL Contract"
        Me.btnAELContract.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtAELImported)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.btnAELContract)
        Me.GroupBox3.Controls.Add(Me.TextBox3)
        Me.GroupBox3.Controls.Add(Me.TextBox4)
        Me.GroupBox3.Controls.Add(Me.grdaelcontracts)
        Me.GroupBox3.Location = New System.Drawing.Point(715, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(360, 543)
        Me.GroupBox3.TabIndex = 19
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "AEL Contracts"
        '
        'txtAELImported
        '
        Me.txtAELImported.Location = New System.Drawing.Point(108, 503)
        Me.txtAELImported.Name = "txtAELImported"
        Me.txtAELImported.Size = New System.Drawing.Size(221, 20)
        Me.txtAELImported.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(25, 509)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Total Imported:"
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(201, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(152, 24)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Exposure Margin (%)"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(9, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(192, 24)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Security name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox3
        '
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Enabled = False
        Me.TextBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(201, 45)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(152, 22)
        Me.TextBox3.TabIndex = 2
        '
        'TextBox4
        '
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox4.Enabled = False
        Me.TextBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(9, 45)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(192, 22)
        Me.TextBox4.TabIndex = 1
        '
        'grdaelcontracts
        '
        Me.grdaelcontracts.AllowUserToAddRows = False
        Me.grdaelcontracts.AllowUserToDeleteRows = False
        Me.grdaelcontracts.AllowUserToResizeColumns = False
        Me.grdaelcontracts.AllowUserToResizeRows = False
        Me.grdaelcontracts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdaelcontracts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn6, Me.InsType2, Me.Symbol3, Me.ExpDate, Me.StrikePrice, Me.OptType, Me.CALevel, Me.ELMPer})
        Me.grdaelcontracts.Location = New System.Drawing.Point(12, 70)
        Me.grdaelcontracts.Name = "grdaelcontracts"
        Me.grdaelcontracts.ReadOnly = True
        Me.grdaelcontracts.RowHeadersVisible = False
        Me.grdaelcontracts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdaelcontracts.Size = New System.Drawing.Size(341, 389)
        Me.grdaelcontracts.TabIndex = 10
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "uid"
        Me.DataGridViewTextBoxColumn6.HeaderText = "uid"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        '
        'InsType2
        '
        Me.InsType2.DataPropertyName = "InsType"
        Me.InsType2.HeaderText = "InsType"
        Me.InsType2.Name = "InsType2"
        Me.InsType2.ReadOnly = True
        '
        'Symbol3
        '
        Me.Symbol3.DataPropertyName = "Symbol"
        Me.Symbol3.HeaderText = "Symbol"
        Me.Symbol3.Name = "Symbol3"
        Me.Symbol3.ReadOnly = True
        '
        'ExpDate
        '
        Me.ExpDate.DataPropertyName = "ExpDate"
        Me.ExpDate.HeaderText = "ExpDate"
        Me.ExpDate.Name = "ExpDate"
        Me.ExpDate.ReadOnly = True
        '
        'StrikePrice
        '
        Me.StrikePrice.DataPropertyName = "StrikePrice"
        Me.StrikePrice.HeaderText = "StrikePrice"
        Me.StrikePrice.Name = "StrikePrice"
        Me.StrikePrice.ReadOnly = True
        '
        'OptType
        '
        Me.OptType.DataPropertyName = "OptType"
        Me.OptType.HeaderText = "OptType"
        Me.OptType.Name = "OptType"
        Me.OptType.ReadOnly = True
        '
        'CALevel
        '
        Me.CALevel.DataPropertyName = "CALevel"
        Me.CALevel.HeaderText = "CALevel"
        Me.CALevel.Name = "CALevel"
        Me.CALevel.ReadOnly = True
        '
        'ELMPer
        '
        Me.ELMPer.DataPropertyName = "ELMPer"
        Me.ELMPer.HeaderText = "Margin Per"
        Me.ELMPer.Name = "ELMPer"
        Me.ELMPer.ReadOnly = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.btnNewAEL)
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.TextBox2)
        Me.GroupBox2.Controls.Add(Me.grdnewexp)
        Me.GroupBox2.Location = New System.Drawing.Point(378, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(326, 503)
        Me.GroupBox2.TabIndex = 18
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "New AEL"
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(201, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(118, 24)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Exposure Margin (%)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(192, 24)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Security name"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(201, 45)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(118, 22)
        Me.TextBox1.TabIndex = 2
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Enabled = False
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(9, 45)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(192, 22)
        Me.TextBox2.TabIndex = 1
        '
        'grdnewexp
        '
        Me.grdnewexp.AllowUserToAddRows = False
        Me.grdnewexp.AllowUserToDeleteRows = False
        Me.grdnewexp.AllowUserToResizeColumns = False
        Me.grdnewexp.AllowUserToResizeRows = False
        Me.grdnewexp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdnewexp.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.uid1, Me.Symbol, Me.InsType, Me.Norm_Margin, Me.Add_Margin, Me.Total_Margin})
        Me.grdnewexp.Location = New System.Drawing.Point(12, 70)
        Me.grdnewexp.Name = "grdnewexp"
        Me.grdnewexp.ReadOnly = True
        Me.grdnewexp.RowHeadersVisible = False
        Me.grdnewexp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdnewexp.Size = New System.Drawing.Size(306, 389)
        Me.grdnewexp.TabIndex = 10
        '
        'uid1
        '
        Me.uid1.DataPropertyName = "uid"
        Me.uid1.HeaderText = "uid"
        Me.uid1.Name = "uid1"
        Me.uid1.ReadOnly = True
        Me.uid1.Visible = False
        '
        'Symbol
        '
        Me.Symbol.DataPropertyName = "Symbol"
        Me.Symbol.HeaderText = "Symbol"
        Me.Symbol.Name = "Symbol"
        Me.Symbol.ReadOnly = True
        '
        'InsType
        '
        Me.InsType.DataPropertyName = "InsType"
        Me.InsType.HeaderText = "InsType"
        Me.InsType.Name = "InsType"
        Me.InsType.ReadOnly = True
        '
        'Norm_Margin
        '
        Me.Norm_Margin.DataPropertyName = "Norm_Margin"
        Me.Norm_Margin.HeaderText = "Norm Margin"
        Me.Norm_Margin.Name = "Norm_Margin"
        Me.Norm_Margin.ReadOnly = True
        Me.Norm_Margin.Visible = False
        '
        'Add_Margin
        '
        Me.Add_Margin.DataPropertyName = "Add_Margin"
        Me.Add_Margin.HeaderText = "Add Margin"
        Me.Add_Margin.Name = "Add_Margin"
        Me.Add_Margin.ReadOnly = True
        Me.Add_Margin.Visible = False
        '
        'Total_Margin
        '
        Me.Total_Margin.DataPropertyName = "Total_Margin"
        Me.Total_Margin.HeaderText = "Total Margin"
        Me.Total_Margin.Name = "Total_Margin"
        Me.Total_Margin.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.grdexp)
        Me.GroupBox1.Controls.Add(Me.txtcomp)
        Me.GroupBox1.Controls.Add(Me.txtexposure)
        Me.GroupBox1.Controls.Add(Me.DeleteButton)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(360, 504)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Old OEL"
        '
        'grdexp
        '
        Me.grdexp.AllowUserToAddRows = False
        Me.grdexp.AllowUserToDeleteRows = False
        Me.grdexp.AllowUserToResizeColumns = False
        Me.grdexp.AllowUserToResizeRows = False
        Me.grdexp.BackgroundColor = System.Drawing.Color.Gray
        Me.grdexp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdexp.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.compname, Me.expmag, Me.uid})
        Me.grdexp.Location = New System.Drawing.Point(12, 68)
        Me.grdexp.Name = "grdexp"
        Me.grdexp.ReadOnly = True
        Me.grdexp.RowHeadersVisible = False
        Me.grdexp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdexp.Size = New System.Drawing.Size(341, 389)
        Me.grdexp.TabIndex = 15
        '
        'compname
        '
        Me.compname.DataPropertyName = "compname"
        Me.compname.HeaderText = "Security"
        Me.compname.Name = "compname"
        Me.compname.ReadOnly = True
        Me.compname.Width = 180
        '
        'expmag
        '
        Me.expmag.DataPropertyName = "exposure_margin"
        Me.expmag.HeaderText = "Exposure Margin (%)"
        Me.expmag.Name = "expmag"
        Me.expmag.ReadOnly = True
        Me.expmag.Width = 140
        '
        'uid
        '
        Me.uid.DataPropertyName = "uid"
        Me.uid.HeaderText = "uid"
        Me.uid.Name = "uid"
        Me.uid.ReadOnly = True
        Me.uid.Visible = False
        '
        'txtcomp
        '
        Me.txtcomp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcomp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcomp.Location = New System.Drawing.Point(12, 43)
        Me.txtcomp.Name = "txtcomp"
        Me.txtcomp.Size = New System.Drawing.Size(192, 22)
        Me.txtcomp.TabIndex = 13
        '
        'txtexposure
        '
        Me.txtexposure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtexposure.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtexposure.Location = New System.Drawing.Point(204, 43)
        Me.txtexposure.Name = "txtexposure"
        Me.txtexposure.Size = New System.Drawing.Size(152, 22)
        Me.txtexposure.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(192, 24)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Security name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(204, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(152, 24)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Exposure Margin (%)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TimerAel
        '
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(178, 464)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(101, 37)
        Me.Button5.TabIndex = 13
        Me.Button5.Text = "Download And Import New AEL"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'frm_exposure_margin_entry
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.LightGray
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(1082, 555)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frm_exposure_margin_entry"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Exposure Margin"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.grdaelcontracts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdnewexp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdexp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim margin_table As New DataTable
    Dim margin_table_new As New DataTable
    Dim AEL_Contracts As New DataTable
    Dim objTrad As New trading

    Dim Thr_AELContract As Thread

    Dim objTrad_Thr As New Cls_AEL
    Dim AELContractPath As String
    Public Totalcnt As Integer
    
    Dim aelfiledownloadzip As String
    ''' <summary>
    ''' Call Initialize table methode init_table 
    ''' Call fill_grid Methode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_script_entry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        init_table()
        fill_grid()

        init_ExposureMargin_New()
        fill_newgrid()

        init_ael_contracts()
        fill_aelcontracts()
        'FillTblFrmDB()

        'fill_griddatabase()
    End Sub

    ''' <summary>
    ''' Initialize Data table
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub init_table()
        margin_table = New DataTable
        With margin_table.Columns
            .Add("uid")
            .Add("compname")
            .Add("exposure_margin")
        End With
    End Sub
    ''' <summary>
    '''  Fill Margin_table from database
    ''' and this Margin_table assign to grid view data source
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fill_grid()
        margin_table.Rows.Clear()
        margin_table = objTrad.Exposure_margin()
        grdexp.DataSource = margin_table

    End Sub

    Private Sub fill_newgrid()
        margin_table_new.Rows.Clear()
        margin_table_new = objTrad.Exposure_Margin_New()
        grdnewexp.DataSource = margin_table_new
    End Sub
    Private Sub fill_aelcontracts()
        AEL_Contracts.Rows.Clear()
        AEL_Contracts = objTrad.AEL_Contracts()
        grdaelcontracts.DataSource = AEL_Contracts
    End Sub
    ''' <summary>
    '''  it checks validation first if it returns true then
    ''' selected company checked true and update its value which is given Company Text box and its value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If validate_data() = True Then
                Dim chk As Boolean = False
                For Each mrow As DataRow In margin_table.Select("compname='" & txtcomp.Text.Trim & "'")
                    chk = True
                    Exit For
                Next
                If chk = False Then
                    objTrad.Insert_Exposure_margin(txtcomp.Text.Trim, Val(txtexposure.Text))
                Else
                    objTrad.update_Exposure_margin(txtcomp.Text.Trim, Val(txtexposure.Text))
                End If
                'strquery = "select * from exposure_margin where compname='" & txtcomp.Text.Trim & "'"
                'cmd = New OleDbCommand(strquery, con)
                'dread = cmd.ExecuteReader
                'If dread.Read Then
                '    strquery = "update exposure_margin set exposure_margin=" & txtexposure.Text.Trim & " where compname='" & txtcomp.Text.Trim & "'"
                'Else
                '    strquery = "insert into exposure_margin (compname,exposure_margin) values ('" & txtcomp.Text.Trim & "'," & txtexposure.Text.Trim & ")"
                'End If
                'dread.Close()
                'cmd.Dispose()
                'dread = Nothing

                'cmd = New OleDbCommand(strquery, con)
                'cmd.ExecuteNonQuery()
                'cmd.Dispose()

                'FillTblFrmDB()

                'fill_griddatabase()
                fill_grid()
                txtcomp.Text = ""
                txtexposure.Text = ""

                MsgBox("Data saved.")
            Else
                MsgBox("Fill all data.")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
    ''' <summary>
    '''  its a validation Function which check validation for company text box text and exposure Margin text box
    ''' it one of them get blank then it returns false and cursor exit function
    ''' otherwise returns true
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function validate_data() As Boolean
        If txtcomp.Text = "" Then
            txtcomp.Focus()
            validate_data = False
            Exit Function
        End If
        If txtexposure.Text = "" Then
            txtexposure.Focus()
            validate_data = False
            Exit Function
        End If
        validate_data = True
    End Function
    'Private Sub FillTblFrmDB()
    '    Dim cmd As OleDb.OleDbCommand
    '    Dim dread As OleDb.OleDbDataReader
    '    Dim da As OleDb.OleDbDataAdapter

    '    mTbl_exposure_database.Rows.Clear()
    '    cmd = New OleDb.OleDbCommand("select * from exposure_margin order by compname", con)
    '    da = New OleDb.OleDbDataAdapter(cmd)

    '    da.Fill(mTbl_exposure_database)

    '    da.Dispose()
    '    cmd.Dispose()
    'End Sub
    'Private Sub fill_griddatabase()
    '    Dim i As Integer = 1
    '    Dim drow As DataRow

    '    With grddatabase
    '        If .Rows.Count > 1 Then
    '            .Rows.RemoveRange(1, .Rows.Count - 1)
    '        End If

    '        For Each drow In mTbl_exposure_database.Rows
    '            .Rows.Insert(CInt(i))
    '            .Rows(CInt(i)).Height = 19

    '            .Item(i, 0) = New SourceGrid2.Cells.Real.Cell(drow("uid"))
    '            .Item(i, 1) = New SourceGrid2.Cells.Real.Cell(drow("compname"))
    '            .Item(i, 2) = New SourceGrid2.Cells.Real.Cell(drow("exposure_margin"))

    '            i += 1
    '        Next
    '    End With
    'End Sub
    'Private Sub initialize_griddatabase()
    '    With grddatabase
    '        .BorderStyle = BorderStyle.FixedSingle
    '        .ColumnsCount = 3
    '        .FixedRows = 1

    '        .Selection.SelectionMode = SourceGrid2.GridSelectionMode.Row

    '        .Rows.Insert(0) '''Adding Fix ROw
    '        .Rows(0).Height = 25 '''Setting Fix Row Height

    '        .Columns(0).Width = 0
    '        .Item(0, 0) = New SourceGrid2.Cells.Real.ColumnHeader("")

    '        .Columns(1).Width = 192
    '        .Item(0, 1) = New SourceGrid2.Cells.Real.ColumnHeader("Comp Name")

    '        .Columns(2).Width = 132
    '        .Item(0, 2) = New SourceGrid2.Cells.Real.ColumnHeader("Exposure Margin")

    '    End With
    'End Sub
    ''' <summary>
    ''' close the Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Close()
    End Sub
    'Private Sub grddatabase_CellGotFocus(ByVal sender As Object, ByVal e As SourceGrid2.PositionCancelEventArgs) Handles grddatabase.CellGotFocus
    '    With grddatabase
    '        txtcomp.Text = .Item(e.Position.Row, 1).Value
    '        txtexposure.Text = .Item(e.Position.Row, 2).Value
    '    End With
    'End Sub

    'Private Sub txtexposure_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtexposure.TextChanged

    'End Sub

    ''' <summary>
    '''  it call numonly function
    ''' which takes only numeric vaue
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtexposure_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Dim KeyAscii As Short = Asc(e.KeyChar)
        'If NumOnly(KeyAscii) = 0 Then
        '    e.Handled = True
        'End If
        numonly(e)
    End Sub

    ''' <summary>
    ''' This button click export data to datagrid view from excel file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        OpenFileDialog1.FileName = "*.xls"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Try
                Dim tempdata As New DataTable
                Dim fpath As String
                fpath = OpenFileDialog1.FileName
                ' Dim fi As New FileInfo(fpath)
                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" & fpath
                Dim objConn As New OleDbConnection(sConnectionString)
                objConn.Open()
                Dim objCmdSelect As New OleDbCommand("SELECT * FROM [sheet1$] where [company name] <> ''", objConn)
                'Dim objCmdSelect As New OleDbCommand("SELECT strike,option_type,Exp_Date,Company,Qty,Rate,Instrument,entrydate FROM " & fi.Name, objConn)
                Dim objAdapter1 As New OleDbDataAdapter
                objAdapter1.SelectCommand = objCmdSelect
                objAdapter1.Fill(tempdata)
                objConn.Close()
                'Dim msrno As Integer
                Dim msymbol As String
                Dim mEM As String
                If tempdata.Rows.Count > 0 Then
                    For Each drow As DataRow In tempdata.Rows



                        msymbol = CStr(drow(1))
                        mEM = Val(drow(2))



                        Dim chk As Boolean = False
                        For Each mrow As DataRow In margin_table.Select("compname='" & msymbol & "'")
                            chk = True
                            Exit For
                        Next
                        If chk = False Then
                            objTrad.Insert_Exposure_margin(msymbol, Val(mEM))
                        Else
                            objTrad.update_Exposure_margin(msymbol, Val(mEM))
                        End If
                        '   objTrad.Insert_Exposure_margin(msymbol, Val(mEM))
                    Next
                    fill_grid()
                    MsgBox("Import Completed.", MsgBoxStyle.Information)
                Else
                    MsgBox("Import Failed.", MsgBoxStyle.Critical)
                End If
                'Dim rs As New ADODB.Recordset

                'Try
                '    '//get excel record in recordset
                '    rs.CursorLocation = ADODB.CursorLocationEnum.adUseClient
                '    'MsgBox("Provider=MSDASQL;Driver={Microsoft Excel Driver (*.xls)};DBQ=" & txtarbitrage_file_path.Text)
                '    rs.Open("[sheet1$]", "Provider=MSDASQL;Driver={Microsoft Excel Driver (*.xls)};DBQ=" & OpenFileDialog1.FileName)     'connection String For excell

                '    '''Fetching Data And Segrigate it in Sheet1

                '    'Dim mStartRow As Integer
                '    Dim msrno As Integer
                '    Dim msymbol As String
                '    Dim mEM As String
                '    'Dim strquery As String

                '    'If CStr(rs(0).Value) <> "1" Then
                '    '    rs.MoveNext()
                '    'End If

                '    objTrad.delete_Exposure_margin()


                '    msrno = 1
                '    Try
                '        While Not rs.EOF
                '            If Not IsDBNull(rs(0).Value) Then
                '                If msrno.ToString = CStr(rs(0).Value) Then
                '                    msymbol = CStr(rs(1).Value)
                '                    mEM = val(rs(2).Value)
                '                    objTrad.Insert_Exposure_margin(msymbol, val(mEM))
                '                    'strquery = "insert into exposure_margin(compname,exposure_margin) values ('" & msymbol & "'," & mEM & ")"
                '                    'com = New OleDbCommand(strquery, con, tra)
                '                    'com.ExecuteNonQuery()

                '                    msrno += 1
                '                End If
                '            End If
                '            rs.MoveNext()
                '        End While

                '        rs.Close()
                '        rs = Nothing

                'If msrno > 1 Then
                '    fill_grid()
                '    MsgBox("Import Completed......", MsgBoxStyle.Information)
                'Else
                '    MsgBox("Import Failed......", MsgBoxStyle.Critical)
                'End If

                '    Catch ex As Exception

                '    rs.Close()
                '    rs = Nothing
                '    MsgBox("Import Failed......", MsgBoxStyle.Critical)

            Catch ex As Exception
                '
            End Try

        End If
    End Sub

    ''' <summary>
    ''' this double click of retrive company name and exposure margin value on the text box respectivly
    ''' and on delete key press it remove that selected record
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdexp_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Dim compname As String
        compname = grdexp.Rows(e.RowIndex).Cells(1).Value
        For Each drow As DataRow In margin_table.Select("compname='" & compname & "'")
            ' MsgBox(drow("compname").ToString.Length)
            txtcomp.Text = drow("compname")
            txtexposure.Text = drow("exposure_margin")
        Next
    End Sub

    ''' <summary>
    '''  on escap key it close the form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_exposure_margin_entry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub DeleteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        Try
            For Each Row As DataGridViewRow In grdexp.SelectedRows
                objTrad.Delete_Exposure_margin_Select(Row.Cells(1).Value.ToString.Trim, Val(Row.Cells(2).Value))
                grdexp.Rows.Remove(Row)
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub txtcomp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dv As New DataView(margin_table)
        'dv.RowFilter = "Compname like '%" + txtcomp.Text + "%'"
        dv.RowFilter = "Compname like '" + txtcomp.Text + "%'"
        grdexp.DataSource = dv
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        OpenFileDialog1.FileName = "*.csv"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Try
                Dim tempdata As New DataTable
                Dim fpath As String
                fpath = OpenFileDialog1.FileName


                Dim fi As New FileInfo(fpath)
                Dim dv As DataView
                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName

                Dim objConn As New OleDbConnection(sConnectionString)

                objConn.Open()

                'Dim objCmdSelect As New OleDbCommand
                Dim objAdapter1 As New OleDbDataAdapter("SELECT * FROM " & fi.Name, objConn)
                'objAdapter1.SelectCommand = objCmdSelect

                tempdata = New DataTable
                objAdapter1.Fill(tempdata)
                objConn.Close()


                'Dim msrno As Integer
                Dim msymbol As String
                Dim mEM As String
                If tempdata.Rows.Count > 0 Then
                    For Each drow As DataRow In tempdata.Rows

                        msymbol = CStr(drow(1))
                        mEM = Val(drow(4))



                        Dim chk As Boolean = False
                        For Each mrow As DataRow In margin_table.Select("compname='" & msymbol & "'")
                            chk = True
                            Exit For
                        Next
                        If chk = False Then
                            objTrad.Insert_Exposure_margin(msymbol, Val(mEM))
                        Else
                            objTrad.update_Exposure_margin(msymbol, Val(mEM))
                        End If
                        '   objTrad.Insert_Exposure_margin(msymbol, Val(mEM))
                    Next
                    fill_grid()
                    MsgBox("Import Completed.", MsgBoxStyle.Information)
                Else
                    MsgBox("Import Failed.", MsgBoxStyle.Critical)
                End If
                'Dim rs As New ADODB.Recordset

                'Try
                '    '//get excel record in recordset
                '    rs.CursorLocation = ADODB.CursorLocationEnum.adUseClient
                '    'MsgBox("Provider=MSDASQL;Driver={Microsoft Excel Driver (*.xls)};DBQ=" & txtarbitrage_file_path.Text)
                '    rs.Open("[sheet1$]", "Provider=MSDASQL;Driver={Microsoft Excel Driver (*.xls)};DBQ=" & OpenFileDialog1.FileName)     'connection String For excell

                '    '''Fetching Data And Segrigate it in Sheet1

                '    'Dim mStartRow As Integer
                '    Dim msrno As Integer
                '    Dim msymbol As String
                '    Dim mEM As String
                '    'Dim strquery As String

                '    'If CStr(rs(0).Value) <> "1" Then
                '    '    rs.MoveNext()
                '    'End If

                '    objTrad.delete_Exposure_margin()


                '    msrno = 1
                '    Try
                '        While Not rs.EOF
                '            If Not IsDBNull(rs(0).Value) Then
                '                If msrno.ToString = CStr(rs(0).Value) Then
                '                    msymbol = CStr(rs(1).Value)
                '                    mEM = val(rs(2).Value)
                '                    objTrad.Insert_Exposure_margin(msymbol, val(mEM))
                '                    'strquery = "insert into exposure_margin(compname,exposure_margin) values ('" & msymbol & "'," & mEM & ")"
                '                    'com = New OleDbCommand(strquery, con, tra)
                '                    'com.ExecuteNonQuery()

                '                    msrno += 1
                '                End If
                '            End If
                '            rs.MoveNext()
                '        End While

                '        rs.Close()
                '        rs = Nothing

                'If msrno > 1 Then
                '    fill_grid()
                '    MsgBox("Import Completed......", MsgBoxStyle.Information)
                'Else
                '    MsgBox("Import Failed......", MsgBoxStyle.Critical)
                'End If

                '    Catch ex As Exception

                '    rs.Close()
                '    rs = Nothing
                '    MsgBox("Import Failed......", MsgBoxStyle.Critical)

            Catch ex As Exception
                '
            End Try

        End If
    End Sub

    Private Sub btnNewAEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewAEL.Click
        OpenFileDialog1.FileName = "*.csv"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Try
                Dim tempdata As New DataTable
                Dim fpath As String
                fpath = OpenFileDialog1.FileName


                Dim fi As New FileInfo(fpath)

                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName

                Dim objConn As New OleDbConnection(sConnectionString)

                objConn.Open()

                'Dim objCmdSelect As New OleDbCommand
                Dim objAdapter1 As New OleDbDataAdapter("SELECT * FROM " & fi.Name, objConn)
                'objAdapter1.SelectCommand = objCmdSelect

                tempdata = New DataTable
                objAdapter1.Fill(tempdata)
                objConn.Close()

                margin_table_new.Rows.Clear()
                objTrad.delete_Exposure_margin_new()


                'Dim msrno As Integer
                Dim mSymbol As String
                Dim mInsType As String
                Dim mNorm_Margin As String
                Dim mAdd_Margin As String
                Dim mTotal_Margin As String

                If tempdata.Rows.Count > 0 Then
                    For Each drow As DataRow In tempdata.Rows

                        mSymbol = CStr(drow(1))
                        mInsType = CStr(drow(2))
                        mNorm_Margin = Val(drow(3))
                        mAdd_Margin = Val(drow(4))
                        mTotal_Margin = Val(drow(5))

                        'Dim chk As Boolean = False
                        'For Each mrow As DataRow In margin_table_new.Select("Symbol='" & mSymbol & "'")
                        '    chk = True
                        '    Exit For
                        'Next
                        'If chk = False Then
                        objTrad.Insert_Exposure_margin_new(mSymbol, mInsType, Val(mNorm_Margin), Val(mAdd_Margin), Val(mTotal_Margin))
                        'Else
                        ' objTrad.update_Exposure_margin_new(mSymbol, mInsType, Val(mNorm_Margin), Val(mAdd_Margin), Val(mTotal_Margin))
                        ' End If

                    Next
                    'fill_grid()
                    margin_table_new.Rows.Clear()
                    margin_table_new = objTrad.Exposure_Margin_New()

                    MsgBox("Import Completed.", MsgBoxStyle.Information)
                Else
                    MsgBox("Import Failed.", MsgBoxStyle.Critical)
                End If

            Catch ex As Exception
                '
            End Try

        End If
    End Sub

    Private Sub init_ExposureMargin_New()
        margin_table_new = New DataTable
        With margin_table_new.Columns
            .Add("uid")
            .Add("Symbol")
            .Add("InsType")
            .Add("Norm_Margin")
            .Add("Add_Margin")
            .Add("Total_Margin")
        End With
    End Sub

    Private Sub init_ael_contracts()
        AEL_Contracts = New DataTable
        With AEL_Contracts.Columns
            .Add("uid")
            .Add("InsType")
            .Add("Symbol")
            .Add("Exp_Date")
            .Add("Strike_Price")
            .Add("Opt_Type")
            .Add("CA_Level")
            .Add("ELM_Per")
        End With
    End Sub

    Private Sub btnAELContract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAELContract.Click
        OpenFileDialog1.FileName = "*.csv"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Try

                AELContractPath = OpenFileDialog1.FileName
                grdaelcontracts.DataSource = Nothing
                AEL_Contracts.Rows.Clear()
                objTrad.delete_AEL_Contracts()

                Thr_AELContract = New Thread(AddressOf ThrAELContracts)
                Thr_AELContract.Name = "ThrAELContract"
                Thr_AELContract.Start()

                TimerAel.Interval = 1000
                TimerAel.Enabled = True

            Catch ex As Exception
                '
            End Try

        End If
    End Sub

    Public Sub ThrAELContracts()
        Try



        Dim tempdata As New DataTable
            ' Dim fpath As String
            'fpath = OpenFileDialog1.FileName


            Dim fi As New FileInfo(AELContractPath)

        Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName

        Dim objConn As New OleDbConnection(sConnectionString)

        objConn.Open()

        'Dim objCmdSelect As New OleDbCommand
        Dim objAdapter1 As New OleDbDataAdapter("SELECT * FROM " & fi.Name, objConn)
        'objAdapter1.SelectCommand = objCmdSelect

        tempdata = New DataTable
        objAdapter1.Fill(tempdata)
        objConn.Close()

            'Dim cnt As Integer

            Totalcnt = tempdata.Rows.Count

            If tempdata.Rows.Count > 0 Then

                'InsType, Symbol, ExpDate, StrikePrice, OptType, CALevel, ELMPer
                tempdata.Columns(0).ColumnName = "InsType"
                tempdata.Columns(1).ColumnName = "Symbol"
                tempdata.Columns(2).ColumnName = "ExpDate"
                tempdata.Columns(3).ColumnName = "StrikePrice"
                tempdata.Columns(4).ColumnName = "OptType"
                tempdata.Columns(5).ColumnName = "CALevel"
                tempdata.Columns(6).ColumnName = "ELMPer"

                tempdata.AcceptChanges()

                'For Each drow As DataRow In tempdata.Rows
                '    mInsType = CStr(drow(0))
                '    mSymbol = CStr(drow(1))

                '    mExpDate = CDate(drow(2))
                '    mStrikePrice = Val(drow(3))
                '    mOptType = CStr(drow(4))
                '    mCALevel = Val(drow(5))
                '    mELMPer = Val(drow(6))

                '    'Dim chk As Boolean = False
                '    'For Each mrow As DataRow In AEL_Contracts.Select("Symbol='" & mSymbol & "',  InsType='" & mInsType & "'," & mStrikePrice & ", '" & mOptType & "', #" & mExpDate & "#")
                '    '    chk = True
                '    '    Exit For
                '    'Next
                '        ' If chk = False Then
                '            objTrad_Thr.Insert_AEL_Contracts(mInsType, mSymbol, mExpDate, Val(mStrikePrice), mOptType, Val(mCALevel), Val(mELMPer))
                '        cnt = cnt + 1
                '        countAEL = cnt
                '        'Else
                '        ' objTrad.update_AEL_Contracts(mSymbol, mInsType, mExpDate, Val(mStrikePrice), mOptType, Val(mCALevel), Val(mELMPer))
                '        'End If

                '    Next
                ''fill_grid()    

                'objTrad_Thr.UpDataDB(tempdata)
                'objTrad_Thr.CopyDatatableToAccess(tempdata) 'UpDataDB(tempdata)
                objTrad_Thr.insert(tempdata)

                MsgBox("Import Completed.", MsgBoxStyle.Information)
            Else
                MsgBox("Import Failed.", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception

        End Try


    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerAel.Tick
        txtAELImported.Text = "Completed  " & objTrad_Thr.countAEL & "  Out of  " & Totalcnt
        If objTrad_Thr.countAEL = Totalcnt Then
            AEL_Contracts.Rows.Clear()
            AEL_Contracts = objTrad.AEL_Contracts()
            grdaelcontracts.DataSource = AEL_Contracts

        End If

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try


            Dim fnamecsv As String = "ael_" + DateTime.Now.ToString("ddMMyyyy") + ".csv"
            Dim filepath As String = Application.StartupPath + "\" + "DownloadAELFile\" + fnamecsv
            aelfiledownloadzip = fnamecsv
            DownloadSpanFile(fnamecsv)
            fnamecsv = aelfiledownloadzip
            filepath = Application.StartupPath + "\" + "DownloadAELFile\" + fnamecsv
            Dim fi As New FileInfo(filepath)

            Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName

            Dim objConn As New OleDbConnection(sConnectionString)

            objConn.Open()

            'Dim objCmdSelect As New OleDbCommand
            Dim objAdapter1 As New OleDbDataAdapter("SELECT * FROM " & fi.Name, objConn)
            'objAdapter1.SelectCommand = objCmdSelect
            Dim tempdata As DataTable
            tempdata = New DataTable
            objAdapter1.Fill(tempdata)
            objConn.Close()

            margin_table_new.Rows.Clear()
            objTrad.delete_Exposure_margin_new()


            'Dim msrno As Integer
            Dim mSymbol As String
            Dim mInsType As String
            Dim mNorm_Margin As String
            Dim mAdd_Margin As String
            Dim mTotal_Margin As String

            If tempdata.Rows.Count > 0 Then
                For Each drow As DataRow In tempdata.Rows

                    mSymbol = CStr(drow(1))
                    mInsType = CStr(drow(2))
                    mNorm_Margin = Val(drow(3))
                    mAdd_Margin = Val(drow(4))
                    mTotal_Margin = Val(drow(5))

                    'Dim chk As Boolean = False
                    'For Each mrow As DataRow In margin_table_new.Select("Symbol='" & mSymbol & "'")
                    '    chk = True
                    '    Exit For
                    'Next
                    'If chk = False Then
                    objTrad.Insert_Exposure_margin_new(mSymbol, mInsType, Val(mNorm_Margin), Val(mAdd_Margin), Val(mTotal_Margin))
                    'Else
                    ' objTrad.update_Exposure_margin_new(mSymbol, mInsType, Val(mNorm_Margin), Val(mAdd_Margin), Val(mTotal_Margin))
                    ' End If

                Next
                'fill_grid()
                margin_table_new.Rows.Clear()
                margin_table_new = objTrad.Exposure_Margin_New()

                MsgBox("Import Completed.", MsgBoxStyle.Information)
            Else
                MsgBox("Import Failed.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub DownloadSpanFile(ByVal Fname As String)
        Dim url As String = ""
        'If Type = "FO" Then
        '    '//https://www1.nseindia.com/archives/nsccl/span/nsccl.20200106.i4.zip
        '    url = Convert.ToString("https://www1.nseindia.com/archives/nsccl/span/") & Fname
        'ElseIf Type = "CURR" Then
        '    url = Convert.ToString("https://www1.nseindia.com/archives/cd/span/") & Fname
        'End If



        'url = "ftp://strategybuilder.finideas.com/AEL"
        url = "https://support.finideas.com/AEL/"



        Dim filepath As String = Application.StartupPath + "\" + "DownloadAELFile\"


        Dim filename As String = filepath & Fname


        Dim filepathdir As String = Application.StartupPath + "\" + "DownloadAELFile\"
        If System.IO.Directory.Exists(filepathdir) Then
            Dim directory As New System.IO.DirectoryInfo(filepathdir)


            For Each file As System.IO.FileInfo In directory.GetFiles()
                Try
                    file.Delete()

                Catch ex As Exception

                End Try

            Next
        End If

        If Not System.IO.Directory.Exists(filepathdir) Then
            System.IO.Directory.CreateDirectory(filepathdir)
        End If

        Dim i As Integer = 0
aa:
        DownloadFileFTP(Fname, url, Application.StartupPath + "\" + "DownloadAELFile\")
        Dim info2 As New FileInfo(Application.StartupPath + "\" + "DownloadAELFile\" + Fname)
        Dim length2 As Long = info2.Length

        If Not System.IO.File.Exists(Application.StartupPath + "\" + "DownloadAELFile\" + Fname) Or length2 = 0 Then
            i = i + 1

            'Fname = "nsccl." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i1.zip"
            Fname = "ael_" + DateTime.Now.AddDays(-1 * i).ToString("ddMMyyyy") + ".csv"
            aelfiledownloadzip = Fname
            'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
            'spanfiledownload = "nsccl." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i01.spn"


            GoTo aa
        End If





    End Sub
End Class
