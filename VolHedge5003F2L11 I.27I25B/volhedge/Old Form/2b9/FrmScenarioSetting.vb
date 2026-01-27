Public Class FrmScenarioSetting
    Public sDelta As String
    Public sGamma As String
    Public sVega As String
    Public sTheta As String
    Public sVolga As String
    Public sVanna As String

    Public sDeltaval As String
    Public sGammaval As String
    Public sVegaval As String
    Public sThetaval As String
    Public sVolgaval As String
    Public sVannaval As String


    Public iRDelta As Integer
    Public iRGamma As Integer
    Public iRVega As Integer
    Public iRTheta As Integer
    Public iRVolga As Integer
    Public iRVanna As Integer

    Public iRDeltaval As Integer
    Public iRGammaval As Integer
    Public iRVegaval As Integer
    Public iRThetaval As Integer
    Public iRVolgaval As Integer
    Public iRVannaval As Integer

    Private Function GetStr(ByVal i As Integer) As String
        For j As Integer = 1 To i
            GetStr = GetStr & 0
        Next
    End Function
    Private Sub PM(ByVal Sender As System.Object, ByVal pm As String)
        If pm = "+" Then
            CType(Sender, TextBox).Text = "##0." & GetStr((CType(Sender, TextBox).Text.Length - 4) + 1)
        Else
            CType(Sender, TextBox).Text = "##0." & GetStr((CType(Sender, TextBox).Text.Length - 4) - 1)
        End If
    End Sub
    Public Sub ShowForm()
        TxtDelta.Text = sDelta
        TxtGamma.Text = sGamma
        TxtVega.Text = sVega
        TxtTheta.Text = sTheta
        TxtVolga.Text = sVolga
        TxtVanna.Text = sVanna

        TxtDeltaVal.Text = sDeltaval
        TxtGammaVal.Text = sGammaval
        TxtVegaVal.Text = sVegaval
        TxtThetaVal.Text = sThetaval
        TxtVolgaVal.Text = sVolgaval
        TxtVannaVal.Text = sVannaval

        Me.ShowDialog()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Gp.Click, vop.Click, Dp.Click, Tp.Click, Vep.Click, vovp.Click, tvp.Click, Gvp.Click, vap.Click, vavp.Click, dvp.Click, Vevp.Click
        'TextBox1.Text = Val(0).ToString(TextBox1.Tag "")
        Select Case CType(sender, Button).Name.ToUpper
            Case "GP"
                TxtGamma.Text = "##0." & GetStr((TxtGamma.Text.Length - 4) + 1)
            Case "VOP"
                TxtVolga.Text = "##0." & GetStr((TxtVolga.Text.Length - 4) + 1)
            Case "DP"
                TxtDelta.Text = "##0." & GetStr((TxtDelta.Text.Length - 4) + 1)
            Case "TP"
                TxtTheta.Text = "##0." & GetStr((TxtTheta.Text.Length - 4) + 1)
            Case "VEP"
                TxtVega.Text = "##0." & GetStr((TxtVega.Text.Length - 4) + 1)
            Case "VOVP"
                TxtVolgaVal.Text = "##0." & GetStr((TxtVolgaVal.Text.Length - 4) + 1)
            Case "TVP"
                TxtThetaVal.Text = "##0." & GetStr((TxtThetaVal.Text.Length - 4) + 1)
            Case "GVP"
                TxtGammaVal.Text = "##0." & GetStr((TxtGammaVal.Text.Length - 4) + 1)
            Case "VAP"
                TxtVanna.Text = "##0." & GetStr((TxtVanna.Text.Length - 4) + 1)
            Case "VAVP"
                TxtVannaVal.Text = "##0." & GetStr((TxtVannaVal.Text.Length - 4) + 1)
            Case "DVP"
                TxtDeltaVal.Text = "##0." & GetStr((TxtDeltaVal.Text.Length - 4) + 1)
            Case "VEVP"
                TxtVegaVal.Text = "##0." & GetStr((TxtVegaVal.Text.Length - 4) + 1)
        End Select
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Gm.Click, dm.Click, tm.Click, Vem.Click, vavm.Click, dvm.Click, Vevm.Click, vam.Click, vom.Click, vovm.Click, tvm.Click, Gvm.Click
        Select Case CType(sender, Button).Name.ToUpper
            Case "GM"
                TxtGamma.Text = "##0." & GetStr((TxtGamma.Text.Length - 4) - 1)
            Case "VOM"
                TxtVolga.Text = "##0." & GetStr((TxtVolga.Text.Length - 4) - 1)
            Case "DM"
                TxtDelta.Text = "##0." & GetStr((TxtDelta.Text.Length - 4) - 1)
            Case "TM"
                TxtTheta.Text = "##0." & GetStr((TxtTheta.Text.Length - 4) - 1)
            Case "VEM"
                TxtVega.Text = "##0." & GetStr((TxtVega.Text.Length - 4) - 1)
            Case "VOVM"
                TxtVolgaVal.Text = "##0." & GetStr((TxtVolgaVal.Text.Length - 4) - 1)
            Case "TVM"
                TxtThetaVal.Text = "##0." & GetStr((TxtThetaVal.Text.Length - 4) - 1)
            Case "GVM"
                TxtGammaVal.Text = "##0." & GetStr((TxtGammaVal.Text.Length - 4) - 1)
            Case "VAM"
                TxtVanna.Text = "##0." & GetStr((TxtVanna.Text.Length - 4) - 1)
            Case "VAVM"
                TxtVannaVal.Text = "##0." & GetStr((TxtVannaVal.Text.Length - 4) - 1)
            Case "DVM"
                TxtDeltaVal.Text = "##0." & GetStr((TxtDeltaVal.Text.Length - 4) - 1)
            Case "VEVM"
                TxtVegaVal.Text = "##0." & GetStr((TxtVegaVal.Text.Length - 4) - 1)
        End Select
    End Sub

    Private Sub FrmScenarioSetting_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sDelta = TxtDelta.Text
        sGamma = TxtGamma.Text
        sVega = TxtVega.Text
        sTheta = TxtTheta.Text
        sVolga = TxtVolga.Text
        sVanna = TxtVanna.Text

        sDeltaval = TxtDeltaVal.Text
        sGammaval = TxtGammaVal.Text
        sVegaval = TxtVegaVal.Text
        sThetaval = TxtThetaVal.Text
        sVolgaval = TxtVolgaVal.Text
        sVannaval = TxtVannaVal.Text

        iRDelta = TxtDelta.Text.Trim.Length - 4
        iRGamma = TxtGamma.Text.Trim.Length - 4
        iRVega = TxtVega.Text.Trim.Length - 4
        iRTheta = TxtTheta.Text.Trim.Length - 4
        iRVolga = TxtVolga.Text.Trim.Length - 4
        iRVanna = TxtVanna.Text.Trim.Length - 4

        iRDeltaval = TxtDeltaVal.Text.Trim.Length - 4
        iRGammaval = TxtGammaVal.Text.Trim.Length - 4
        iRVegaval = TxtVegaVal.Text.Trim.Length - 4
        iRThetaval = TxtThetaVal.Text.Trim.Length - 4
        iRVolgaval = TxtVolgaVal.Text.Trim.Length - 4
        iRVannaval = TxtVannaVal.Text.Trim.Length - 4
    End Sub
End Class


