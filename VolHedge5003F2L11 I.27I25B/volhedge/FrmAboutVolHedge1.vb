Public Class FrmAboutVolHedge1

    Private Sub FrmAboutVolHedge1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmAboutVolHedge1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblAPI.Text = ""
        lblVersion.Text = GVar_Version_Title.Trim
        lblMX.Text = Format(GVar_Master_Expiry, "ddMMyyyy").Trim
        lblLX.Text = Format(clsGlobal.Expire_Date, "ddMMyyyy").Trim
        If clsGlobal.Expire_Date > GVar_Master_Expiry Then
            lblDX.Text = DateDiff(DateInterval.Day, Today, GVar_Master_Expiry)
        Else
            lblDX.Text = DateDiff(DateInterval.Day, Today, clsGlobal.Expire_Date)
        End If

        lblDealers.Text = G_VarNoOfDealer
        lblAMC.Visible = G_VarIsAMC

        lblApiExpery.Text = flgAPI_Exp.ToString("dd-MMM-yyyy")
        lblAPISymbol.Text = HT_RegIdentifier.Count

        If NetMode = "API" And flgAPI_K = "TCP" Then
            lblAPI.Text = "Data Powered by TRUEDATA"
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        PicServer.Image = ImageList1.Images.Item(Convert.ToInt32(bool_IsServerConnected))
        PicTelNet.Image = ImageList1.Images.Item(Convert.ToInt32(bool_IsTelNet))
        PicFo.Image = ImageList1.Images.Item(Convert.ToInt32(G_BCastFoIsOn))
        PicCM.Image = ImageList1.Images.Item(Convert.ToInt32(G_BCastCmIsOn))
        PicCurr.Image = ImageList1.Images.Item(Convert.ToInt32(G_BCastCurrIsOn))
        PicSqlFo.Image = ImageList1.Images.Item(Convert.ToInt32(G_BCastSqlFoIsOn))
        PicSqlCm.Image = ImageList1.Images.Item(Convert.ToInt32(G_BCastSqlCmIsOn))
        PicSqlCurr.Image = ImageList1.Images.Item(Convert.ToInt32(G_BCastSqlCurrIsOn))

        PicNetFo.Image = ImageList1.Images.Item(Convert.ToInt32(G_BCastNetFoIsOn))
        PicNetCM.Image = ImageList1.Images.Item(Convert.ToInt32(G_BCastNetCmIsOn))
        PicNetCurr.Image = ImageList1.Images.Item(Convert.ToInt32(G_BCastNetCurrIsOn))

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class