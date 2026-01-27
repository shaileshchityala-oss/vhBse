Public Class FrmSelectChart

    Public ArrSeriesCol As New ArrayList
    Public Sub ShowForm()
        Me.ShowDialog()
    End Sub
    Private Sub ChkCallPremium_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallPremium.CheckedChanged
        If ChkCallPremium.Checked Then
            If Not ArrSeriesCol.Contains("02CallPremium") Then
                ArrSeriesCol.Add("02CallPremium")
            End If
        Else
            If ArrSeriesCol.Contains("02CallPremium") Then
                ArrSeriesCol.Remove("02CallPremium")
            End If
        End If
    End Sub

    Private Sub ChkPutPremium_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutPremium.CheckedChanged
        If ChkPutPremium.Checked Then
            If Not ArrSeriesCol.Contains("01PutPremium") Then
                ArrSeriesCol.Add("01PutPremium")
            End If
        Else
            If ArrSeriesCol.Contains("01PutPremium") Then
                ArrSeriesCol.Remove("01PutPremium")
            End If
        End If
    End Sub

    Private Sub ChkCallDelta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallDelta.CheckedChanged
        If ChkCallDelta.Checked Then
            If Not ArrSeriesCol.Contains("03CallDelta") Then
                ArrSeriesCol.Add("03CallDelta")
            End If
        Else
            If ArrSeriesCol.Contains("03CallDelta") Then
                ArrSeriesCol.Remove("03CallDelta")
            End If
        End If
    End Sub

    Private Sub ChkPutDelta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutDelta.CheckedChanged
        If ChkPutDelta.Checked Then
            If Not ArrSeriesCol.Contains("04PutDelta") Then
                ArrSeriesCol.Add("04PutDelta")
            End If
        Else
            If ArrSeriesCol.Contains("04PutDelta") Then
                ArrSeriesCol.Remove("04PutDelta")
            End If
        End If
    End Sub

    Private Sub ChkGamma_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkGamma.CheckedChanged
        If ChkGamma.Checked Then
            If Not ArrSeriesCol.Contains("05Gamma") Then
                ArrSeriesCol.Add("05Gamma")
            End If
        Else
            If ArrSeriesCol.Contains("05Gamma") Then
                ArrSeriesCol.Remove("05Gamma")
            End If
        End If
    End Sub

    Private Sub ChkVega_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkVega.CheckedChanged
        If ChkVega.Checked Then
            If Not ArrSeriesCol.Contains("06Vega") Then
                ArrSeriesCol.Add("06Vega")
            End If
        Else
            If ArrSeriesCol.Contains("06Vega") Then
                ArrSeriesCol.Remove("06Vega")
            End If
        End If
    End Sub

    Private Sub ChkCallTheta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallTheta.CheckedChanged
        If ChkCallTheta.Checked Then
            If Not ArrSeriesCol.Contains("07CallTheta") Then
                ArrSeriesCol.Add("07CallTheta")
            End If
        Else
            If ArrSeriesCol.Contains("07CallTheta") Then
                ArrSeriesCol.Remove("07CallTheta")
            End If
        End If
    End Sub

    Private Sub ChkPutTheta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutTheta.CheckedChanged
        If ChkPutTheta.Checked Then
            If Not ArrSeriesCol.Contains("08PutTheta") Then
                ArrSeriesCol.Add("08PutTheta")
            End If
        Else
            If ArrSeriesCol.Contains("08PutTheta") Then
                ArrSeriesCol.Remove("08PutTheta")
            End If
        End If
    End Sub

    Private Sub ChkCallRoh_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallRoh.CheckedChanged
        If ChkCallRoh.Checked Then
            If Not ArrSeriesCol.Contains("09CallRoh") Then
                ArrSeriesCol.Add("09CallRoh")
            End If
        Else
            If ArrSeriesCol.Contains("09CallRoh") Then
                ArrSeriesCol.Remove("09CallRoh")
            End If
        End If
    End Sub

    Private Sub ChkPutRoh_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutRoh.CheckedChanged
        If ChkPutRoh.Checked Then
            If Not ArrSeriesCol.Contains("10PutRoh") Then
                ArrSeriesCol.Add("10PutRoh")
            End If
        Else
            If ArrSeriesCol.Contains("10PutRoh") Then
                ArrSeriesCol.Remove("10PutRoh")
            End If
        End If
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub FrmSelectChart_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Me.Close()
    End Sub

    Private Sub ChkVolga_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkVolga.CheckedChanged
        If ChkVolga.Checked Then
            If Not ArrSeriesCol.Contains("11Volga") Then
                ArrSeriesCol.Add("11Volga")
            End If
        Else
            If ArrSeriesCol.Contains("11Volga") Then
                ArrSeriesCol.Remove("11Volga")
            End If
        End If
    End Sub

    Private Sub ChkVanna_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkVanna.CheckedChanged
        If ChkVanna.Checked Then
            If Not ArrSeriesCol.Contains("12Vanna") Then
                ArrSeriesCol.Add("12Vanna")
            End If
        Else
            If ArrSeriesCol.Contains("12Vanna") Then
                ArrSeriesCol.Remove("12Vanna")
            End If
        End If
    End Sub
End Class