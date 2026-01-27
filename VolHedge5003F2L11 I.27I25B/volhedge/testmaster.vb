Imports System.Data.Odbc
Imports System.Management
Public Class testmaster
    Inherits System.Windows.Forms.Form
    Dim tmstate As String
    Public MothrBoardSrNo As String = ""
    Public HDDSrNo As String = ""
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblsecondcode As System.Windows.Forms.Label
    Friend WithEvents lblfirstcode As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Public ProcessorSrNoStr As String = ""

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtcompcode As System.Windows.Forms.TextBox
    Friend WithEvents cmdsave As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents txtcompname As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(testmaster))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtcompcode = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtcompname = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cmdexit = New System.Windows.Forms.Button
        Me.cmdsave = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblfirstcode = New System.Windows.Forms.Label
        Me.lblsecondcode = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Palatino Linotype", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(7, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(651, 25)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = " Software Registration"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtcompcode)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.lblsecondcode)
        Me.Panel1.Controls.Add(Me.lblfirstcode)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtcompname)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Location = New System.Drawing.Point(7, 39)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(651, 132)
        Me.Panel1.TabIndex = 0
        '
        'txtcompcode
        '
        Me.txtcompcode.BackColor = System.Drawing.SystemColors.Window
        Me.txtcompcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcompcode.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcompcode.Location = New System.Drawing.Point(184, 56)
        Me.txtcompcode.Name = "txtcompcode"
        Me.txtcompcode.Size = New System.Drawing.Size(462, 23)
        Me.txtcompcode.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(59, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "User Code No."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(163, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(21, 13)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = ": -"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtcompname
        '
        Me.txtcompname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcompname.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcompname.Location = New System.Drawing.Point(184, 96)
        Me.txtcompname.Name = "txtcompname"
        Me.txtcompname.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtcompname.Size = New System.Drawing.Size(462, 21)
        Me.txtcompname.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(1, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(156, 13)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Authorization Code No."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(163, 96)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(21, 13)
        Me.Label9.TabIndex = 34
        Me.Label9.Text = ": -"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.SlateGray
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.cmdexit)
        Me.Panel2.Controls.Add(Me.cmdsave)
        Me.Panel2.Location = New System.Drawing.Point(7, 177)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(651, 40)
        Me.Panel2.TabIndex = 1
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Palatino Linotype", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Navy
        Me.cmdexit.Location = New System.Drawing.Point(327, 4)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(85, 27)
        Me.cmdexit.TabIndex = 5
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'cmdsave
        '
        Me.cmdsave.Font = New System.Drawing.Font("Palatino Linotype", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsave.ForeColor = System.Drawing.Color.Navy
        Me.cmdsave.Location = New System.Drawing.Point(236, 4)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(85, 27)
        Me.cmdsave.TabIndex = 5
        Me.cmdsave.Text = " &Authorize"
        Me.cmdsave.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(163, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = ": -"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(59, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 13)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "First Code No."
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(163, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(21, 13)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = ": -"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(42, 33)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(115, 13)
        Me.Label8.TabIndex = 38
        Me.Label8.Text = "Second Code No."
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblfirstcode
        '
        Me.lblfirstcode.AutoSize = True
        Me.lblfirstcode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfirstcode.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblfirstcode.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblfirstcode.Location = New System.Drawing.Point(190, 7)
        Me.lblfirstcode.Name = "lblfirstcode"
        Me.lblfirstcode.Size = New System.Drawing.Size(15, 13)
        Me.lblfirstcode.TabIndex = 38
        Me.lblfirstcode.Text = "X"
        Me.lblfirstcode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblsecondcode
        '
        Me.lblsecondcode.AutoSize = True
        Me.lblsecondcode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsecondcode.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblsecondcode.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblsecondcode.Location = New System.Drawing.Point(190, 33)
        Me.lblsecondcode.Name = "lblsecondcode"
        Me.lblsecondcode.Size = New System.Drawing.Size(15, 13)
        Me.lblsecondcode.TabIndex = 38
        Me.lblsecondcode.Text = "Y"
        Me.lblsecondcode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'testmaster
        '
        Me.AllowDrop = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(666, 246)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "testmaster"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub clearform()
        txtcompcode.Text = ""
        txtcompname.Text = ""
    End Sub
    Private Function validatefrm() As Boolean
        Try

            If txtcompcode.Text = "" Then
                MsgBox("Enter Company Code.", MsgBoxStyle.Information)
                txtcompcode.Focus()
                validatefrm = False
                Exit Function
            End If
            If txtcompname.Text = "" Then
                MsgBox("Enter Company Name.", MsgBoxStyle.Information)
                txtcompname.Focus()
                validatefrm = False
                Exit Function
            End If
            validatefrm = True
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Function
    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        Try
            If validatefrm() Then
                '   If Client_encry.Client_encry.check_server_key(Client_encry.Client_encry.get_client_key("R8HUM1T3Q15R9L", getmbserial), txtcompname.Text.Trim) = False Then
                If Client_encry.Client_encry.check_server_key(Client_encry.Client_encry.get_client_key(HDDSrNo, MothrBoardSrNo, ProcessorSrNoStr, "S10XQ6BJ4G8S26JSN"), txtcompname.Text.Trim) = False Then
                    MsgBox("Code not matched.", MsgBoxStyle.Critical)
                Else
                    Dim mstream_writer As System.IO.StreamWriter
                    mstream_writer = New System.IO.StreamWriter(Application.StartupPath & "\code.dll", False)
                    mstream_writer.WriteLine(txtcompname.Text.Trim)
                    mstream_writer.Close()

                    MsgBox("Thank you for registration. Please restart the software.", MsgBoxStyle.Information)
                    'Application.Exit()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub


    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
        'If MsgBox("Do you want to Exit", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then
        ' Application.Exit()
        'Me.Close()
        'End If
    End Sub
    Private Sub frmcitymaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'clearform()
        'txtcompcode.Text = gen_code("comp_code", "comp_master")
        'cmdnew.Text = "  &Cancel"
        'cmdsave.Text = "  &Save"
        'cmdsave.Enabled = True
        'txtcompcode.Enabled = False
        'txtcompname.Focus()
    End Sub
    Private Sub frmcitymaster_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        e.Cancel = False
    End Sub
    Private Sub compmaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Public Function getmbserial() As String
        'Dim objs As Object

        'Dim Obj As Object
        'Dim WMI As Object
        'Dim sAns As String


        'WMI = GetObject("WinMgmts:")
        'objs = WMI.InstancesOf("Win32_baseboard")
        'For Each Obj In objs
        '    sAns = sAns & Obj.SerialNumber
        '    'If sAns < objs.Count Then sAns = sAns & ","
        'Next
        'getmbserial = sAns




        Dim searcher As New ManagementObjectSearcher("SELECT  SerialNumber FROM Win32_BaseBoard")

        Dim information As ManagementObjectCollection = searcher.[Get]()

        For Each obj As ManagementObject In information

            For Each data As PropertyData In obj.Properties
                'Console.WriteLine("{0} = {1}", data.Name, data.Value)
                'Return data.Value
                Return data.Value
            Next
        Next
        searcher.Dispose()
    End Function

    Private Sub cmdnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class