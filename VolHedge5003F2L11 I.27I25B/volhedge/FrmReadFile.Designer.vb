<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReadFile
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
        Me.ButReadSecFile = New System.Windows.Forms.Button
        Me.txtpath = New System.Windows.Forms.TextBox
        Me.ButReadDatFile = New System.Windows.Forms.Button
        Me.ButReadFullDataFile = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ButReadSecFile
        '
        Me.ButReadSecFile.Location = New System.Drawing.Point(38, 83)
        Me.ButReadSecFile.Name = "ButReadSecFile"
        Me.ButReadSecFile.Size = New System.Drawing.Size(107, 23)
        Me.ButReadSecFile.TabIndex = 0
        Me.ButReadSecFile.Text = "&Read Sec File"
        Me.ButReadSecFile.UseVisualStyleBackColor = True
        '
        'txtpath
        '
        Me.txtpath.Location = New System.Drawing.Point(58, 44)
        Me.txtpath.Name = "txtpath"
        Me.txtpath.Size = New System.Drawing.Size(191, 20)
        Me.txtpath.TabIndex = 1
        Me.txtpath.Text = "D:\FinIdeas Projects\FAO\14.sec"
        '
        'ButReadDatFile
        '
        Me.ButReadDatFile.Location = New System.Drawing.Point(151, 83)
        Me.ButReadDatFile.Name = "ButReadDatFile"
        Me.ButReadDatFile.Size = New System.Drawing.Size(98, 23)
        Me.ButReadDatFile.TabIndex = 2
        Me.ButReadDatFile.Text = "&Read Dat File"
        Me.ButReadDatFile.UseVisualStyleBackColor = True
        '
        'ButReadFullDataFile
        '
        Me.ButReadFullDataFile.Location = New System.Drawing.Point(100, 127)
        Me.ButReadFullDataFile.Name = "ButReadFullDataFile"
        Me.ButReadFullDataFile.Size = New System.Drawing.Size(112, 23)
        Me.ButReadFullDataFile.TabIndex = 3
        Me.ButReadFullDataFile.Text = "&Read Full Dat File"
        Me.ButReadFullDataFile.UseVisualStyleBackColor = True
        '
        'FrmReadFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(328, 183)
        Me.Controls.Add(Me.ButReadFullDataFile)
        Me.Controls.Add(Me.ButReadDatFile)
        Me.Controls.Add(Me.txtpath)
        Me.Controls.Add(Me.ButReadSecFile)
        Me.Name = "FrmReadFile"
        Me.Text = "FrmReadFile"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButReadSecFile As System.Windows.Forms.Button
    Friend WithEvents txtpath As System.Windows.Forms.TextBox
    Friend WithEvents ButReadDatFile As System.Windows.Forms.Button
    Friend WithEvents ButReadFullDataFile As System.Windows.Forms.Button
End Class
