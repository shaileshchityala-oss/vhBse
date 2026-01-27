Namespace Tester
	Partial Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(Form1))
			Me.progressBar1 = New System.Windows.Forms.ProgressBar()
			Me.button1 = New System.Windows.Forms.Button()
			Me.textBox1 = New System.Windows.Forms.TextBox()
			Me.textBox6 = New System.Windows.Forms.TextBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.button2 = New System.Windows.Forms.Button()
			Me.button3 = New System.Windows.Forms.Button()
			Me.pictureBox1 = New System.Windows.Forms.PictureBox()
			DirectCast(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' progressBar1
			' 
			Me.progressBar1.Location = New System.Drawing.Point(50, 149)
			Me.progressBar1.Name = "progressBar1"
			Me.progressBar1.Size = New System.Drawing.Size(272, 23)
			Me.progressBar1.TabIndex = 3
			' 
			' button1
			' 
			Me.button1.Location = New System.Drawing.Point(110, 109)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(154, 34)
			Me.button1.TabIndex = 2
			Me.button1.Text = "Update"
			Me.button1.UseVisualStyleBackColor = True
			AddHandler Me.button1.Click, New System.EventHandler(Me.button1_Click)
			' 
			' textBox1
			' 
			Me.textBox1.Location = New System.Drawing.Point(68, 22)
			Me.textBox1.Name = "textBox1"
			Me.textBox1.Size = New System.Drawing.Size(426, 20)
			Me.textBox1.TabIndex = 1
			Me.textBox1.Text = "http://www.finideas.com/FinIdeasBackUp/FinMarketWatch.exe"
			Me.textBox1.Visible = False
			' 
			' textBox6
			' 
			Me.textBox6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.textBox6.Location = New System.Drawing.Point(12, 178)
			Me.textBox6.Multiline = True
			Me.textBox6.Name = "textBox6"
			Me.textBox6.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.textBox6.Size = New System.Drawing.Size(375, 123)
			Me.textBox6.TabIndex = 4
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(14, 25)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(48, 13)
			Me.label1.TabIndex = 0
			Me.label1.Text = "File URL"
			Me.label1.Visible = False
			' 
			' button2
			' 
			Me.button2.Location = New System.Drawing.Point(354, 144)
			Me.button2.Name = "button2"
			Me.button2.Size = New System.Drawing.Size(75, 23)
			Me.button2.TabIndex = 5
			Me.button2.Text = "Async"
			Me.button2.UseVisualStyleBackColor = True
			Me.button2.Visible = False
			AddHandler Me.button2.Click, New System.EventHandler(Me.button2_Click)
			' 
			' button3
			' 
			Me.button3.Location = New System.Drawing.Point(354, 173)
			Me.button3.Name = "button3"
			Me.button3.Size = New System.Drawing.Size(75, 23)
			Me.button3.TabIndex = 6
			Me.button3.Text = "Cancel"
			Me.button3.UseVisualStyleBackColor = True
			Me.button3.Visible = False
			AddHandler Me.button3.Click, New System.EventHandler(Me.button3_Click)
			' 
			' pictureBox1
			' 
			Me.pictureBox1.Image = Global.Updater.Properties.Resources.F_5
			Me.pictureBox1.Location = New System.Drawing.Point(29, 25)
			Me.pictureBox1.Name = "pictureBox1"
			Me.pictureBox1.Size = New System.Drawing.Size(324, 78)
			Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
			Me.pictureBox1.TabIndex = 7
			Me.pictureBox1.TabStop = False
			' 
			' Form1
			' 
			Me.ClientSize = New System.Drawing.Size(399, 313)
			Me.Controls.Add(Me.pictureBox1)
			Me.Controls.Add(Me.button3)
			Me.Controls.Add(Me.button2)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.progressBar1)
			Me.Controls.Add(Me.textBox6)
			Me.Controls.Add(Me.textBox1)
			Me.Controls.Add(Me.button1)
			Me.Icon = DirectCast(resources.GetObject("$this.Icon"), System.Drawing.Icon)
			Me.Name = "Form1"
			Me.Text = "Update FinMarketwatch"
			Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.Form1_FormClosing)
			DirectCast(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private button1 As System.Windows.Forms.Button
		Private textBox1 As System.Windows.Forms.TextBox
		Private progressBar1 As System.Windows.Forms.ProgressBar
		Private textBox6 As System.Windows.Forms.TextBox
		Private label1 As System.Windows.Forms.Label
		Private button2 As System.Windows.Forms.Button
		Private button3 As System.Windows.Forms.Button
		Private pictureBox1 As System.Windows.Forms.PictureBox
	End Class
End Namespace

