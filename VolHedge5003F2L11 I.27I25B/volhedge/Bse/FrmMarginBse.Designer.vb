<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMarginBse
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNseInit = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkOnTop = New System.Windows.Forms.CheckBox()
        Me.btnNSE = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNseExp = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNseTotal = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBseInit = New System.Windows.Forms.TextBox()
        Me.txtBseExp = New System.Windows.Forms.TextBox()
        Me.txBseTotal = New System.Windows.Forms.TextBox()
        Me.btnBSE = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnProcessAll = New System.Windows.Forms.Button()
        Me.txtAllInit = New System.Windows.Forms.TextBox()
        Me.txtAllExp = New System.Windows.Forms.TextBox()
        Me.txtAllTotal = New System.Windows.Forms.TextBox()
        Me.btnGetCurComp = New System.Windows.Forms.Button()
        Me.lblSymbol = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Init Margin"
        '
        'txtNseInit
        '
        Me.txtNseInit.Location = New System.Drawing.Point(91, 67)
        Me.txtNseInit.Name = "txtNseInit"
        Me.txtNseInit.Size = New System.Drawing.Size(100, 20)
        Me.txtNseInit.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.chkOnTop, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnNSE, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtNseInit, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtNseExp, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.txtNseTotal, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtBseInit, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtBseExp, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txBseTotal, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.btnBSE, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnProcessAll, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtAllInit, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtAllExp, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtAllTotal, 3, 4)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(5, 5)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(406, 142)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'chkOnTop
        '
        Me.chkOnTop.AutoSize = True
        Me.chkOnTop.Checked = True
        Me.chkOnTop.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOnTop.Location = New System.Drawing.Point(3, 3)
        Me.chkOnTop.Name = "chkOnTop"
        Me.chkOnTop.Size = New System.Drawing.Size(15, 14)
        Me.chkOnTop.TabIndex = 4
        Me.chkOnTop.UseVisualStyleBackColor = True
        '
        'btnNSE
        '
        Me.btnNSE.Location = New System.Drawing.Point(91, 23)
        Me.btnNSE.Name = "btnNSE"
        Me.btnNSE.Size = New System.Drawing.Size(94, 38)
        Me.btnNSE.TabIndex = 3
        Me.btnNSE.Text = "NSE"
        Me.btnNSE.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Exp Margin"
        '
        'txtNseExp
        '
        Me.txtNseExp.Location = New System.Drawing.Point(91, 93)
        Me.txtNseExp.Name = "txtNseExp"
        Me.txtNseExp.Size = New System.Drawing.Size(100, 20)
        Me.txtNseExp.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 122)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Total In Lacs"
        '
        'txtNseTotal
        '
        Me.txtNseTotal.Location = New System.Drawing.Point(91, 119)
        Me.txtNseTotal.Name = "txtNseTotal"
        Me.txtNseTotal.Size = New System.Drawing.Size(100, 20)
        Me.txtNseTotal.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(125, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "NSE"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(231, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "BSE"
        '
        'txtBseInit
        '
        Me.txtBseInit.Location = New System.Drawing.Point(197, 67)
        Me.txtBseInit.Name = "txtBseInit"
        Me.txtBseInit.Size = New System.Drawing.Size(100, 20)
        Me.txtBseInit.TabIndex = 1
        '
        'txtBseExp
        '
        Me.txtBseExp.Location = New System.Drawing.Point(197, 93)
        Me.txtBseExp.Name = "txtBseExp"
        Me.txtBseExp.Size = New System.Drawing.Size(100, 20)
        Me.txtBseExp.TabIndex = 1
        '
        'txBseTotal
        '
        Me.txBseTotal.Location = New System.Drawing.Point(197, 119)
        Me.txBseTotal.Name = "txBseTotal"
        Me.txBseTotal.Size = New System.Drawing.Size(100, 20)
        Me.txBseTotal.TabIndex = 1
        '
        'btnBSE
        '
        Me.btnBSE.Location = New System.Drawing.Point(197, 23)
        Me.btnBSE.Name = "btnBSE"
        Me.btnBSE.Size = New System.Drawing.Size(94, 38)
        Me.btnBSE.TabIndex = 3
        Me.btnBSE.Text = "BSE"
        Me.btnBSE.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(303, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "All"
        Me.Label6.Visible = False
        '
        'btnProcessAll
        '
        Me.btnProcessAll.Location = New System.Drawing.Point(303, 23)
        Me.btnProcessAll.Name = "btnProcessAll"
        Me.btnProcessAll.Size = New System.Drawing.Size(94, 38)
        Me.btnProcessAll.TabIndex = 3
        Me.btnProcessAll.Text = "All Process"
        Me.btnProcessAll.UseVisualStyleBackColor = True
        Me.btnProcessAll.Visible = False
        '
        'txtAllInit
        '
        Me.txtAllInit.Location = New System.Drawing.Point(303, 67)
        Me.txtAllInit.Name = "txtAllInit"
        Me.txtAllInit.Size = New System.Drawing.Size(100, 20)
        Me.txtAllInit.TabIndex = 1
        Me.txtAllInit.Visible = False
        '
        'txtAllExp
        '
        Me.txtAllExp.Location = New System.Drawing.Point(303, 93)
        Me.txtAllExp.Name = "txtAllExp"
        Me.txtAllExp.Size = New System.Drawing.Size(100, 20)
        Me.txtAllExp.TabIndex = 1
        Me.txtAllExp.Visible = False
        '
        'txtAllTotal
        '
        Me.txtAllTotal.Location = New System.Drawing.Point(303, 119)
        Me.txtAllTotal.Name = "txtAllTotal"
        Me.txtAllTotal.Size = New System.Drawing.Size(100, 20)
        Me.txtAllTotal.TabIndex = 1
        Me.txtAllTotal.Visible = False
        '
        'btnGetCurComp
        '
        Me.btnGetCurComp.Location = New System.Drawing.Point(202, 167)
        Me.btnGetCurComp.Name = "btnGetCurComp"
        Me.btnGetCurComp.Size = New System.Drawing.Size(111, 28)
        Me.btnGetCurComp.TabIndex = 3
        Me.btnGetCurComp.Text = "Get"
        Me.btnGetCurComp.UseVisualStyleBackColor = True
        Me.btnGetCurComp.Visible = False
        '
        'lblSymbol
        '
        Me.lblSymbol.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblSymbol.AutoSize = True
        Me.lblSymbol.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSymbol.Location = New System.Drawing.Point(92, 167)
        Me.lblSymbol.Name = "lblSymbol"
        Me.lblSymbol.Size = New System.Drawing.Size(0, 24)
        Me.lblSymbol.TabIndex = 0
        '
        'FrmMarginBse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(314, 207)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btnGetCurComp)
        Me.Controls.Add(Me.lblSymbol)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrmMarginBse"
        Me.Text = "BSE NSE (Margin Calc)"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtNseInit As TextBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents txtNseExp As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtNseTotal As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btnNSE As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents txtBseInit As TextBox
    Friend WithEvents txtBseExp As TextBox
    Friend WithEvents txBseTotal As TextBox
    Friend WithEvents btnBSE As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents btnProcessAll As Button
    Friend WithEvents txtAllInit As TextBox
    Friend WithEvents txtAllExp As TextBox
    Friend WithEvents txtAllTotal As TextBox
    Friend WithEvents btnGetCurComp As Button
    Friend WithEvents lblSymbol As Label
    Friend WithEvents chkOnTop As CheckBox
End Class
