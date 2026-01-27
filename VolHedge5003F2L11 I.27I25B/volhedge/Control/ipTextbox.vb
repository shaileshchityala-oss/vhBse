Public Class ipTextBox
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl1 overrides dispose to clean up the component list.
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
    Friend WithEvents txtRA As System.Windows.Forms.TextBox
    Friend WithEvents txtRB As System.Windows.Forms.TextBox
    Friend WithEvents txtRC As System.Windows.Forms.TextBox
    Friend WithEvents txtRD As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlBack As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtRA = New System.Windows.Forms.TextBox
        Me.txtRB = New System.Windows.Forms.TextBox
        Me.txtRC = New System.Windows.Forms.TextBox
        Me.txtRD = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnlBack = New System.Windows.Forms.Panel
        Me.pnlBack.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtRA
        '
        Me.txtRA.BackColor = System.Drawing.SystemColors.Window
        Me.txtRA.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRA.Location = New System.Drawing.Point(0, 16)
        Me.txtRA.MaxLength = 3
        Me.txtRA.Name = "txtRA"
        Me.txtRA.TabIndex = 0
        Me.txtRA.Text = "0"
        Me.txtRA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRB
        '
        Me.txtRB.BackColor = System.Drawing.SystemColors.Window
        Me.txtRB.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRB.Location = New System.Drawing.Point(112, 16)
        Me.txtRB.MaxLength = 3
        Me.txtRB.Name = "txtRB"
        Me.txtRB.TabIndex = 1
        Me.txtRB.Text = "0"
        Me.txtRB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRC
        '
        Me.txtRC.BackColor = System.Drawing.SystemColors.Window
        Me.txtRC.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRC.Location = New System.Drawing.Point(224, 16)
        Me.txtRC.MaxLength = 3
        Me.txtRC.Name = "txtRC"
        Me.txtRC.TabIndex = 2
        Me.txtRC.Text = "0"
        Me.txtRC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRD
        '
        Me.txtRD.BackColor = System.Drawing.SystemColors.Window
        Me.txtRD.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRD.Location = New System.Drawing.Point(336, 16)
        Me.txtRD.MaxLength = 3
        Me.txtRD.Name = "txtRD"
        Me.txtRD.Size = New System.Drawing.Size(96, 13)
        Me.txtRD.TabIndex = 3
        Me.txtRD.Text = "0"
        Me.txtRD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Window
        Me.Label1.Location = New System.Drawing.Point(88, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(8, 23)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Window
        Me.Label2.Location = New System.Drawing.Point(152, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(8, 23)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Window
        Me.Label3.Location = New System.Drawing.Point(208, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(8, 23)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlBack
        '
        Me.pnlBack.BackColor = System.Drawing.Color.White
        Me.pnlBack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBack.Controls.Add(Me.Label2)
        Me.pnlBack.Controls.Add(Me.Label1)
        Me.pnlBack.Controls.Add(Me.txtRB)
        Me.pnlBack.Controls.Add(Me.txtRA)
        Me.pnlBack.Controls.Add(Me.txtRD)
        Me.pnlBack.Controls.Add(Me.txtRC)
        Me.pnlBack.Controls.Add(Me.Label3)
        Me.pnlBack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBack.Location = New System.Drawing.Point(0, 0)
        Me.pnlBack.Name = "pnlBack"
        Me.pnlBack.Size = New System.Drawing.Size(136, 24)
        Me.pnlBack.TabIndex = 7
        '
        'ipTextBox
        '
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.Controls.Add(Me.pnlBack)
        Me.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Name = "ipTextBox"
        Me.Size = New System.Drawing.Size(136, 24)
        Me.pnlBack.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim mRangeAMin As Integer = 0
    Dim mRangeAMax As Integer = 255

    Dim mRangeBMin As Integer = 0
    Dim mRangeBMax As Integer = 255

    Dim mRangeCMin As Integer = 0
    Dim mRangeCMax As Integer = 255

    Dim mRangeDMin As Integer = 0
    Dim mRangeDMax As Integer = 255

    Public Event rangeAChanged()
    Public Event rangeBChanged()
    Public Event rangeCChanged()
    Public Event rangeDChanged()


    Public Property RangeAValue() As Integer
        Get
            Return CInt(Me.txtRA.Text)
        End Get
        Set(ByVal Value As Integer)
            Me.txtRA.Text = Value.ToString
            checkValues()
        End Set
    End Property
    Public Property RangeBValue() As Integer
        Get
            Return CInt(Me.txtRB.Text)
        End Get
        Set(ByVal Value As Integer)
            Me.txtRB.Text = Value.ToString
            checkValues()
        End Set
    End Property
    Public Property RangeCValue() As Integer
        Get
            Return CInt(Me.txtRC.Text)
        End Get
        Set(ByVal Value As Integer)
            Me.txtRC.Text = Value.ToString
            checkValues()
        End Set
    End Property
    Public Property RangeDValue() As Integer
        Get
            Return CInt(Me.txtRD.Text)
        End Get
        Set(ByVal Value As Integer)
            Me.txtRD.Text = Value.ToString
            checkValues()
        End Set
    End Property

    Public Property IPaddress() As String
        Get
            Return Me.txtRA.Text & "." & Me.txtRB.Text & "." & Me.txtRC.Text & "." & Me.txtRD.Text
        End Get
        Set(ByVal Value As String)
            If InStr(Value, ".") > 0 Then
                Dim segments() As String = Split(Value, ".")

                If segments.Length > 0 Then
                    Me.txtRA.Text = CInt(IIf(IsNumeric(segments(0)), segments(0), mRangeAMin)).ToString
                Else
                    Me.txtRA.Text = mRangeAMin.ToString
                End If

                If segments.Length > 1 Then
                    Me.txtRB.Text = CInt(IIf(IsNumeric(segments(1)), segments(1), mRangeAMin)).ToString
                Else
                    Me.txtRB.Text = mRangeBMin.ToString
                End If

                If segments.Length > 2 Then
                    Me.txtRC.Text = CInt(IIf(IsNumeric(segments(2)), segments(2), mRangeAMin)).ToString
                Else
                    Me.txtRC.Text = mRangeCMin.ToString
                End If

                If segments.Length > 3 Then
                    Me.txtRD.Text = CInt(IIf(IsNumeric(segments(3)), segments(3), mRangeAMin)).ToString
                Else
                    Me.txtRD.Text = mRangeDMin.ToString
                End If
            Else
                Me.txtRA.Text = mRangeAMin.ToString
                Me.txtRB.Text = mRangeBMin.ToString
                Me.txtRC.Text = mRangeCMin.ToString
                Me.txtRD.Text = mRangeDMin.ToString
            End If
            checkValues()
        End Set
    End Property

    Public Property rangeAMininum() As Integer
        Get
            Return mRangeAMin
        End Get
        Set(ByVal Value As Integer)
            If Value > mRangeAMax Then Value = mRangeAMax
            mRangeAMin = Value
            checkValues()
        End Set
    End Property
    Public Property rangeAMaximun() As Integer
        Get
            Return mRangeAMax
        End Get
        Set(ByVal Value As Integer)
            If Value < mRangeAMin Then Value = mRangeAMin
            mRangeAMax = Value
            checkValues()
        End Set
    End Property


    Public Property rangeDMininum() As Integer
        Get
            Return mRangeDMin
        End Get
        Set(ByVal Value As Integer)
            If Value > mRangeDMax Then Value = mRangeDMax
            mRangeDMin = Value
            checkValues()
        End Set
    End Property
    Public Property rangeDMaximun() As Integer
        Get
            Return mRangeDMax
        End Get
        Set(ByVal Value As Integer)
            If Value < mRangeDMin Then Value = mRangeDMin
            mRangeDMax = Value
            checkValues()
        End Set
    End Property

    Public Property rangeBMininum() As Integer
        Get
            Return mRangeBMin
        End Get
        Set(ByVal Value As Integer)
            If Value > mRangeBMax Then Value = mRangeBMax
            mRangeBMin = Value
            checkValues()
        End Set
    End Property
    Public Property rangeBMaximun() As Integer
        Get
            Return mRangeBMax
        End Get
        Set(ByVal Value As Integer)
            If Value < mRangeBMin Then Value = mRangeBMin
            mRangeBMax = Value
            checkValues()
        End Set
    End Property

    Public Property rangeCMininum() As Integer
        Get
            Return mRangeCMin
        End Get
        Set(ByVal Value As Integer)
            If Value > mRangeCMax Then Value = mRangeCMax
            mRangeCMin = Value
            checkValues()
        End Set
    End Property
    Public Property rangeCMaximun() As Integer
        Get
            Return mRangeCMax
        End Get
        Set(ByVal Value As Integer)
            If Value < mRangeCMin Then Value = mRangeCMin
            mRangeCMax = Value
            checkValues()
        End Set
    End Property

    Public Property rangeAEnabled() As Boolean
        Get
            Return Me.txtRA.Enabled
        End Get
        Set(ByVal Value As Boolean)
            Me.txtRA.Enabled = Value
        End Set
    End Property

    Public Property rangeBEnabled() As Boolean
        Get
            Return Me.txtRB.Enabled
        End Get
        Set(ByVal Value As Boolean)
            Me.txtRB.Enabled = Value
        End Set
    End Property

    Public Property rangeCEnabled() As Boolean
        Get
            Return Me.txtRC.Enabled
        End Get
        Set(ByVal Value As Boolean)
            Me.txtRC.Enabled = Value
        End Set
    End Property

    Public Property rangeDEnabled() As Boolean
        Get
            Return Me.txtRD.Enabled
        End Get
        Set(ByVal Value As Boolean)
            Me.txtRD.Enabled = Value
        End Set
    End Property

    Public Property BorderStyle() As BorderStyle
        Get
            Return Me.pnlBack.BorderStyle
        End Get
        Set(ByVal Value As BorderStyle)
            Me.pnlBack.BorderStyle = Value
        End Set
    End Property

    Private Sub txtAChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtRA.TextChanged
        checkValues()
        RaiseEvent rangeAChanged()
    End Sub
    Private Sub txtBChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtRB.TextChanged
        checkValues()
        RaiseEvent rangeBChanged()
    End Sub
    Private Sub txtCChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtRC.TextChanged
        checkValues()
        RaiseEvent rangeCChanged()
    End Sub
    Private Sub txtDChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtRD.TextChanged
        checkValues()
        RaiseEvent rangeDChanged()
    End Sub

    Private Sub checkValues()
        If IsNumeric(Me.txtRA.Text) Then
            If CInt(Me.txtRA.Text) > mRangeAMax Then Me.txtRA.Text = mRangeAMax.ToString
            If CInt(Me.txtRA.Text) < mRangeAMin Then Me.txtRA.Text = mRangeAMin.ToString
            Me.txtRA.Text = CInt(Me.txtRA.Text).ToString
        Else
            Me.txtRA.Text = mRangeAMin.ToString
        End If

        If IsNumeric(Me.txtRB.Text) Then
            If CInt(Me.txtRB.Text) > mRangeBMax Then Me.txtRB.Text = mRangeBMax.ToString
            If CInt(Me.txtRB.Text) < mRangeBMin Then Me.txtRB.Text = mRangeBMin.ToString
            Me.txtRB.Text = CInt(Me.txtRB.Text).ToString
        Else
            Me.txtRB.Text = mRangeBMin.ToString
        End If

        If IsNumeric(Me.txtRC.Text) Then
            If CInt(Me.txtRC.Text) > mRangeCMax Then Me.txtRC.Text = mRangeCMax.ToString
            If CInt(Me.txtRC.Text) < mRangeCMin Then Me.txtRC.Text = mRangeCMin.ToString
            Me.txtRC.Text = CInt(Me.txtRC.Text).ToString
        Else
            Me.txtRC.Text = mRangeCMin.ToString
        End If

        If IsNumeric(Me.txtRD.Text) Then
            If CInt(Me.txtRD.Text) > mRangeDMax Then Me.txtRD.Text = mRangeDMax.ToString
            If CInt(Me.txtRD.Text) < mRangeDMin Then Me.txtRD.Text = mRangeDMin.ToString
            Me.txtRD.Text = CInt(Me.txtRD.Text).ToString
        Else
            Me.txtRD.Text = mRangeDMin.ToString
        End If

    End Sub

    Private Sub text_keydown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtRA.KeyDown, txtRB.KeyDown, txtRC.KeyDown, txtRD.KeyDown
        If e.KeyCode >= Keys.D0 And e.KeyCode <= Keys.D9 Then

        Else
            If e.KeyCode = Keys.OemPeriod Then
                e.Handled = True
                If CType(sender, TextBox).Name = "txtRA" Then
                    'RaiseEvent rangeAChanged()
                    If Me.txtRB.Enabled Then
                        Me.txtRB.Focus() : Me.txtRB.SelectAll()
                    ElseIf Me.txtRC.Enabled Then
                        Me.txtRC.Focus() : Me.txtRC.SelectAll()
                    Else
                        Me.txtRD.Focus() : Me.txtRD.SelectAll()
                    End If
                End If

                If CType(sender, TextBox).Name = "txtRB" Then
                    'RaiseEvent rangeBChanged()
                    If Me.txtRC.Enabled Then
                        Me.txtRC.Focus() : Me.txtRC.SelectAll()
                    Else
                        Me.txtRD.Focus() : Me.txtRD.SelectAll()
                    End If
                End If

                If CType(sender, TextBox).Name = "txtRC" Then
                    'RaiseEvent rangeCChanged()
                    Me.txtRD.Focus() : Me.txtRD.SelectAll()
                End If

                If CType(sender, TextBox).Name = "txtRD" Then
                    'RaiseEvent rangeDChanged()
                End If


            End If
            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back Then

            Else
                e.Handled = True
            End If

        End If
        checkValues()

    End Sub



    Private Sub txtkeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtRA.KeyUp, txtRB.KeyUp, txtRC.KeyUp, txtRD.KeyUp
        checkValues()

    End Sub

    Private Sub ipTextBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        checkValues()
    End Sub

    Private Sub bgChange(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.BackColorChanged
        Me.pnlBack.BackColor = Me.BackColor
        Me.txtRA.BackColor = Me.BackColor
        Me.txtRB.BackColor = Me.BackColor
        Me.txtRC.BackColor = Me.BackColor
        Me.txtRD.BackColor = Me.BackColor
        Me.Label1.BackColor = Me.BackColor
        Me.Label2.BackColor = Me.BackColor
        Me.Label3.BackColor = Me.BackColor
    End Sub

    Private Sub fgChange(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.ForeColorChanged
        Me.txtRA.ForeColor = Me.ForeColor
        Me.txtRB.ForeColor = Me.ForeColor
        Me.txtRC.ForeColor = Me.ForeColor
        Me.txtRD.ForeColor = Me.ForeColor
        Me.Label1.ForeColor = Me.ForeColor
        Me.Label2.ForeColor = Me.ForeColor
        Me.Label3.ForeColor = Me.ForeColor
    End Sub

    Private Sub fontChanges(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.FontChanged
        Me.txtRA.Font = Me.Font
        Me.txtRB.Font = Me.Font
        Me.txtRC.Font = Me.Font
        Me.txtRD.Font = Me.Font
        Me.Label1.Font = Me.Font
        Me.Label2.Font = Me.Font
        Me.Label3.Font = Me.Font
    End Sub

    Private Sub ipTextBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Dim txtWidth As Integer = CInt((Me.pnlBack.DisplayRectangle.Width - (Me.Label1.Width * 3)) / 4)
        Dim txtTop As Integer = CInt((Me.pnlBack.DisplayRectangle.Height - Me.txtRA.Height) / 2)
        Me.txtRA.Left = 0
        Me.txtRA.Top = txtTop
        Me.txtRA.Width = txtWidth

        Me.txtRB.Left = txtWidth + Me.Label1.Width
        Me.txtRB.Width = txtWidth
        Me.txtRB.Top = txtTop

        Me.txtRC.Left = (txtWidth + Me.Label1.Width) * 2
        Me.txtRC.Width = txtWidth
        Me.txtRC.Top = txtTop

        Me.txtRD.Left = (txtWidth + Me.Label1.Width) * 3
        Me.txtRD.Width = txtWidth
        Me.txtRD.Top = txtTop



        Me.Label1.Top = txtTop
        Me.Label2.Top = txtTop
        Me.Label3.Top = txtTop
        Me.Label2.Width = Me.Label1.Width
        Me.Label3.Width = Me.Label1.Width

        Me.Label1.Height = Me.txtRA.Height
        Me.Label2.Height = Me.txtRA.Height
        Me.Label3.Height = Me.txtRA.Height

        Me.Label1.Left = txtWidth
        Me.Label2.Left = ((txtWidth + Me.Label1.Width) * 2) - Me.Label1.Width
        Me.Label3.Left = ((txtWidth + Me.Label1.Width) * 3) - Me.Label1.Width

    End Sub
End Class
