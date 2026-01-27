<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCurrencyExposureMargin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCurrencyExposureMargin))
        Me.grdexp = New System.Windows.Forms.DataGridView()
        Me.compname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.expmag = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.uid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtcomp = New System.Windows.Forms.TextBox()
        Me.txtcurrencyexposure = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.DeleteButton = New System.Windows.Forms.Button()
        CType(Me.grdexp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.grdexp.Location = New System.Drawing.Point(5, 54)
        Me.grdexp.Name = "grdexp"
        Me.grdexp.ReadOnly = True
        Me.grdexp.RowHeadersVisible = False
        Me.grdexp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdexp.Size = New System.Drawing.Size(341, 389)
        Me.grdexp.TabIndex = 19
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
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(2, 455)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(440, 33)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Note :- While importing exposure margin from excel file, data must be included wi" &
    "th heading of columns from HTML file."
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Button2.Location = New System.Drawing.Point(354, 89)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 24)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "E&xit"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Button1.Location = New System.Drawing.Point(354, 29)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 24)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "&Save"
        '
        'txtcomp
        '
        Me.txtcomp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcomp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcomp.Location = New System.Drawing.Point(5, 29)
        Me.txtcomp.Name = "txtcomp"
        Me.txtcomp.Size = New System.Drawing.Size(183, 22)
        Me.txtcomp.TabIndex = 0
        '
        'txtcurrencyexposure
        '
        Me.txtcurrencyexposure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcurrencyexposure.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcurrencyexposure.Location = New System.Drawing.Point(186, 29)
        Me.txtcurrencyexposure.Name = "txtcurrencyexposure"
        Me.txtcurrencyexposure.Size = New System.Drawing.Size(160, 22)
        Me.txtcurrencyexposure.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(183, 24)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Security Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(186, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(160, 24)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Exposure Margin (%)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.Button3.Location = New System.Drawing.Point(354, 419)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(80, 24)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "&Import"
        '
        'DeleteButton
        '
        Me.DeleteButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeleteButton.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.DeleteButton.Location = New System.Drawing.Point(354, 59)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(80, 24)
        Me.DeleteButton.TabIndex = 3
        Me.DeleteButton.Text = "&Delete"
        '
        'frmCurrencyExposureMargin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.ClientSize = New System.Drawing.Size(442, 488)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.grdexp)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtcomp)
        Me.Controls.Add(Me.txtcurrencyexposure)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmCurrencyExposureMargin"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Currency Exposure Margin"
        CType(Me.grdexp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdexp As System.Windows.Forms.DataGridView
    Friend WithEvents compname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents expmag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents uid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtcomp As System.Windows.Forms.TextBox
    Friend WithEvents txtcurrencyexposure As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
End Class
