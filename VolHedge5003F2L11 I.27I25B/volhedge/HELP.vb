Public Class HELP

    Private Sub HELP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PictureBox1.Visible = True
        Button1.BackColor = Color.DeepSkyBlue
        Button3.BackColor = Color.DimGray
        Button2.BackColor = Color.DimGray
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PictureBox1.Visible = True
        PictureBox2.Visible = False
        PictureBox3.Visible = False
        Button1.BackColor = Color.DeepSkyBlue
        Button3.BackColor = Color.DimGray
        Button2.BackColor = Color.DimGray
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        PictureBox1.Visible = False
        PictureBox2.Visible = True
        PictureBox3.Visible = False
        Button2.BackColor = Color.DeepSkyBlue
        Button1.BackColor = Color.DimGray
        Button3.BackColor = Color.DimGray


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        PictureBox1.Visible = False
        PictureBox2.Visible = False
        PictureBox3.Visible = True
        Button3.BackColor = Color.DeepSkyBlue
        Button1.BackColor = Color.DimGray
        Button2.BackColor = Color.DimGray

    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

    End Sub
End Class