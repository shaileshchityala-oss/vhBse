Public Class frmSelectColumns
    Public ArrColSeries As New ArrayList
    Public IsChange As Boolean = False
    Private Sub ChkCallOI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallOI.CheckedChanged
        IsChange = True
        If ChkCallOI.Checked Then
            If Not ArrColSeries.Contains("col1CallOI") Then
                ArrColSeries.Add("col1CallOI")
            End If

            If Not ArrColSeries.Contains("col2CallOI") Then
                ArrColSeries.Add("col2CallOI")
            End If

            If Not ArrColSeries.Contains("col3CallOI") Then
                ArrColSeries.Add("col3CallOI")
            End If

        Else
            If ArrColSeries.Contains("col1CallOI") Then
                ArrColSeries.Remove("col1CallOI")
            End If

            If ArrColSeries.Contains("col2CallOI") Then
                ArrColSeries.Remove("col2CallOI")
            End If

            If ArrColSeries.Contains("col3CallOI") Then
                ArrColSeries.Remove("col3CallOI")
            End If

        End If
    End Sub


    Private Sub ChkPutOI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutOI.CheckedChanged
        IsChange = True
        If ChkPutOI.Checked Then
            If Not ArrColSeries.Contains("col1PutOI") Then
                ArrColSeries.Add("col1PutOI")
            End If



            If Not ArrColSeries.Contains("col2PutOI") Then
                ArrColSeries.Add("col2PutOI")
            End If


            If Not ArrColSeries.Contains("col3PutOI") Then
                ArrColSeries.Add("col3PutOI")
            End If
        Else
            If ArrColSeries.Contains("col1PutOI") Then
                ArrColSeries.Remove("col1PutOI")
            End If
            If ArrColSeries.Contains("col2PutOI") Then
                ArrColSeries.Remove("col2PutOI")
            End If
            If ArrColSeries.Contains("col3PutOI") Then
                ArrColSeries.Remove("col3PutOI")
            End If


        End If
    End Sub

    Private Sub ChkCallBF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallBF.CheckedChanged
        IsChange = True
        If ChkCallBF.Checked Then
            If Not ArrColSeries.Contains("col1CallBF") Then
                ArrColSeries.Add("col1CallBF")
            End If

            If Not ArrColSeries.Contains("col2CallBF") Then
                ArrColSeries.Add("col2CallBF")
            End If


            If Not ArrColSeries.Contains("col3CallBF") Then
                ArrColSeries.Add("col3CallBF")
            End If
        Else
            If ArrColSeries.Contains("col1CallBF") Then
                ArrColSeries.Remove("col1CallBF")
            End If

            If ArrColSeries.Contains("col2CallBF") Then
                ArrColSeries.Remove("col2CallBF")
            End If

            If ArrColSeries.Contains("col3CallBF") Then
                ArrColSeries.Remove("col3CallBF")
            End If
        End If
    End Sub

    Private Sub ChkPutBF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutBF.CheckedChanged
        IsChange = True
        If ChkPutBF.Checked Then
            If Not ArrColSeries.Contains("col1PutBF") Then
                ArrColSeries.Add("col1PutBF")
            End If

            If Not ArrColSeries.Contains("col2PutBF") Then
                ArrColSeries.Add("col2PutBF")
            End If

            If Not ArrColSeries.Contains("col3PutBF") Then
                ArrColSeries.Add("col3PutBF")
            End If
        Else
            If ArrColSeries.Contains("col1PutBF") Then
                ArrColSeries.Remove("col1PutBF")
            End If

            If ArrColSeries.Contains("col2PutBF") Then
                ArrColSeries.Remove("col2PutBF")
            End If

            If ArrColSeries.Contains("col3PutBF") Then
                ArrColSeries.Remove("col3PutBF")
            End If
        End If
    End Sub

    Private Sub ChkCallBF2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallBF2.CheckedChanged
        IsChange = True
        If ChkCallBF2.Checked Then
            If Not ArrColSeries.Contains("col1CallBF2") Then
                ArrColSeries.Add("col1CallBF2")
            End If

            If Not ArrColSeries.Contains("col2CallBF2") Then
                ArrColSeries.Add("col2CallBF2")
            End If

            If Not ArrColSeries.Contains("col3CallBF2") Then
                ArrColSeries.Add("col3CallBF2")
            End If
        Else
            If ArrColSeries.Contains("col1CallBF2") Then
                ArrColSeries.Remove("col1CallBF2")
            End If

            If ArrColSeries.Contains("col2CallBF2") Then
                ArrColSeries.Remove("col2CallBF2")
            End If

            If ArrColSeries.Contains("col3CallBF2") Then
                ArrColSeries.Remove("col3CallBF2")
            End If
        End If
    End Sub

    Private Sub ChkPutBF2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutBF2.CheckedChanged
        IsChange = True
        If ChkPutBF2.Checked Then
            If Not ArrColSeries.Contains("col1PutBF2") Then
                ArrColSeries.Add("col1PutBF2")
            End If

            If Not ArrColSeries.Contains("col2PutBF2") Then
                ArrColSeries.Add("col2PutBF2")

                If Not ArrColSeries.Contains("col3PutBF2") Then
                    ArrColSeries.Add("col3PutBF2")
                End If
            End If
        Else
            If ArrColSeries.Contains("col1PutBF2") Then
                ArrColSeries.Remove("col1PutBF2")
            End If

            If ArrColSeries.Contains("col2PutBF2") Then
                ArrColSeries.Remove("col2PutBF2")
            End If
            If ArrColSeries.Contains("col3PutBF2") Then
                ArrColSeries.Remove("col3PutBF2")
            End If
        End If
    End Sub

    Private Sub ChkCallRatio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallRatio.CheckedChanged
        IsChange = True
        If ChkCallRatio.Checked Then
            If Not ArrColSeries.Contains("col1CallRatio") Then
                ArrColSeries.Add("col1CallRatio")
            End If

            If Not ArrColSeries.Contains("col2CallRatio") Then
                ArrColSeries.Add("col2CallRatio")
            End If

            If Not ArrColSeries.Contains("col3CallRatio") Then
                ArrColSeries.Add("col3CallRatio")
            End If
        Else
            If ArrColSeries.Contains("col1CallRatio") Then
                ArrColSeries.Remove("col1CallRatio")
            End If

            If ArrColSeries.Contains("col2CallRatio") Then
                ArrColSeries.Remove("col2CallRatio")
            End If

            If ArrColSeries.Contains("col3CallRatio") Then
                ArrColSeries.Remove("col3CallRatio")
            End If
        End If
    End Sub

    Private Sub ChkPutRatio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutRatio.CheckedChanged
        IsChange = True
        If ChkPutRatio.Checked Then
            If Not ArrColSeries.Contains("col1PutRatio") Then
                ArrColSeries.Add("col1PutRatio")
            End If

            If Not ArrColSeries.Contains("col2PutRatio") Then
                ArrColSeries.Add("col2PutRatio")
            End If

            If Not ArrColSeries.Contains("col3PutRatio") Then
                ArrColSeries.Add("col3PutRatio")
            End If
        Else
            If ArrColSeries.Contains("col1PutRatio") Then
                ArrColSeries.Remove("col1PutRatio")
            End If

            If ArrColSeries.Contains("col2PutRatio") Then
                ArrColSeries.Remove("col2PutRatio")
            End If

            If ArrColSeries.Contains("col3PutRatio") Then
                ArrColSeries.Remove("col3PutRatio")
            End If
        End If
    End Sub

    Private Sub ChkCallVolume_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallVolume.CheckedChanged
        IsChange = True
        If ChkCallVolume.Checked Then
            If Not ArrColSeries.Contains("col1CallVolume") Then
                ArrColSeries.Add("col1CallVolume")
            End If
            If Not ArrColSeries.Contains("col2CallVolume") Then
                ArrColSeries.Add("col2CallVolume")
            End If
            If Not ArrColSeries.Contains("col3CallVolume") Then
                ArrColSeries.Add("col3CallVolume")
            End If
        Else
            If ArrColSeries.Contains("col1CallVolume") Then
                ArrColSeries.Remove("col1CallVolume")
            End If
            If ArrColSeries.Contains("col2CallVolume") Then
                ArrColSeries.Remove("col2CallVolume")
            End If
            If ArrColSeries.Contains("col3CallVolume") Then
                ArrColSeries.Remove("col3CallVolume")
            End If
        End If
    End Sub

    Private Sub ChkPutVolume_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutVolume.CheckedChanged
        IsChange = True
        If ChkPutVolume.Checked Then
            If Not ArrColSeries.Contains("col1PutVolume") Then
                ArrColSeries.Add("col1PutVolume")
            End If
            If Not ArrColSeries.Contains("col2PutVolume") Then
                ArrColSeries.Add("col2PutVolume")
            End If
            If Not ArrColSeries.Contains("col3PutVolume") Then
                ArrColSeries.Add("col3PutVolume")
            End If
        Else
            If ArrColSeries.Contains("col1PutVolume") Then
                ArrColSeries.Remove("col1PutVolume")
            End If

            If ArrColSeries.Contains("col2PutVolume") Then
                ArrColSeries.Remove("col2PutVolume")
            End If

            If ArrColSeries.Contains("col3PutVolume") Then
                ArrColSeries.Remove("col3PutVolume")
            End If
        End If
    End Sub

    Private Sub ChkCallDelta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallDelta.CheckedChanged
        IsChange = True
        If ChkCallDelta.Checked Then
            If Not ArrColSeries.Contains("col1CallDelta") Then
                ArrColSeries.Add("col1CallDelta")
            End If

            If Not ArrColSeries.Contains("col2CallDelta") Then
                ArrColSeries.Add("col2CallDelta")
            End If
            If Not ArrColSeries.Contains("col3CallDelta") Then
                ArrColSeries.Add("col3CallDelta")
            End If
        Else
            If ArrColSeries.Contains("col1CallDelta") Then
                ArrColSeries.Remove("col1CallDelta")
            End If
            If ArrColSeries.Contains("col2CallDelta") Then
                ArrColSeries.Remove("col2CallDelta")
            End If
            If ArrColSeries.Contains("col3CallDelta") Then
                ArrColSeries.Remove("col3CallDelta")
            End If
        End If
    End Sub

    Private Sub ChkPutDelta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutDelta.CheckedChanged

        IsChange = True
        If ChkPutDelta.Checked Then
            If Not ArrColSeries.Contains("col1PutDelta") Then
                ArrColSeries.Add("col1PutDelta")
            End If

            If Not ArrColSeries.Contains("col2PutDelta") Then
                ArrColSeries.Add("col2PutDelta")
            End If
            If Not ArrColSeries.Contains("col3PutDelta") Then
                ArrColSeries.Add("col3PutDelta")
            End If
        Else
            If ArrColSeries.Contains("col1PutDelta") Then
                ArrColSeries.Remove("col1PutDelta")
            End If
            If ArrColSeries.Contains("col2PutDelta") Then
                ArrColSeries.Remove("col2PutDelta")
            End If
            If ArrColSeries.Contains("col3PutDelta") Then
                ArrColSeries.Remove("col3PutDelta")
            End If
        End If
    End Sub

    Private Sub ChkCallGamma_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallGamma.CheckedChanged
        IsChange = True
        If ChkCallGamma.Checked Then
            If Not ArrColSeries.Contains("col1CallGamma") Then
                ArrColSeries.Add("col1CallGamma")
            End If
            If Not ArrColSeries.Contains("col2CallGamma") Then
                ArrColSeries.Add("col2CallGamma")
            End If
            If Not ArrColSeries.Contains("col3CallGamma") Then
                ArrColSeries.Add("col3CallGamma")
            End If
        Else
            If ArrColSeries.Contains("col1CallGamma") Then
                ArrColSeries.Remove("col1CallGamma")
            End If
            If ArrColSeries.Contains("col2CallGamma") Then
                ArrColSeries.Remove("col2CallGamma")
            End If
            If ArrColSeries.Contains("col3CallGamma") Then
                ArrColSeries.Remove("col3CallGamma")
            End If
        End If
    End Sub

    Private Sub ChkCallVega_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallVega.CheckedChanged
        IsChange = True
        If ChkCallVega.Checked Then
            If Not ArrColSeries.Contains("col1CallVega") Then
                ArrColSeries.Add("col1CallVega")
            End If
            If Not ArrColSeries.Contains("col2CallVega") Then
                ArrColSeries.Add("col2CallVega")
            End If
            If Not ArrColSeries.Contains("col3CallVega") Then
                ArrColSeries.Add("col3CallVega")
            End If
        Else
            If ArrColSeries.Contains("col1CallVega") Then
                ArrColSeries.Remove("col1CallVega")
            End If
            If ArrColSeries.Contains("col2CallVega") Then
                ArrColSeries.Remove("col2CallVega")
            End If
            If ArrColSeries.Contains("col3CallVega") Then
                ArrColSeries.Remove("col3CallVega")
            End If
        End If
    End Sub

    Private Sub ChkPutVega_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutVega.CheckedChanged
        IsChange = True
        If ChkPutVega.Checked Then
            If Not ArrColSeries.Contains("col1PutVega") Then
                ArrColSeries.Add("col1PutVega")
            End If
            If Not ArrColSeries.Contains("col2PutVega") Then
                ArrColSeries.Add("col2PutVega")
            End If
            If Not ArrColSeries.Contains("col3PutVega") Then
                ArrColSeries.Add("col3PutVega")
            End If
        Else
            If ArrColSeries.Contains("col1PutVega") Then
                ArrColSeries.Remove("col1PutVega")
            End If
            If ArrColSeries.Contains("col2PutVega") Then
                ArrColSeries.Remove("col2PutVega")
            End If
            If ArrColSeries.Contains("col3PutVega") Then
                ArrColSeries.Remove("col3PutVega")
            End If
        End If
    End Sub

    Private Sub ChkCallTheta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallTheta.CheckedChanged
        IsChange = True
        If ChkCallTheta.Checked Then
            If Not ArrColSeries.Contains("col1CallTheta") Then
                ArrColSeries.Add("col1CallTheta")
            End If
            If Not ArrColSeries.Contains("col2CallTheta") Then
                ArrColSeries.Add("col2CallTheta")
            End If
            If Not ArrColSeries.Contains("col3CallTheta") Then
                ArrColSeries.Add("col3CallTheta")
            End If
        Else
            If ArrColSeries.Contains("col1CallTheta") Then
                ArrColSeries.Remove("col1CallTheta")
            End If
            If ArrColSeries.Contains("col2CallTheta") Then
                ArrColSeries.Remove("col2CallTheta")
            End If
            If ArrColSeries.Contains("col3CallTheta") Then
                ArrColSeries.Remove("col3CallTheta")
            End If
        End If
    End Sub

    Private Sub ChkPutTheta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutTheta.CheckedChanged
        IsChange = True
        If ChkPutTheta.Checked Then
            If Not ArrColSeries.Contains("col1PutTheta") Then
                ArrColSeries.Add("col1PutTheta")
            End If
            If Not ArrColSeries.Contains("col2PutTheta") Then
                ArrColSeries.Add("col2PutTheta")
            End If
            If Not ArrColSeries.Contains("col3PutTheta") Then
                ArrColSeries.Add("col3PutTheta")
            End If
        Else
            If ArrColSeries.Contains("col1PutTheta") Then
                ArrColSeries.Remove("col1PutTheta")
            End If
            If ArrColSeries.Contains("col2PutTheta") Then
                ArrColSeries.Remove("col2PutTheta")
            End If
            If ArrColSeries.Contains("col3PutTheta") Then
                ArrColSeries.Remove("col3PutTheta")
            End If
        End If
    End Sub

    Private Sub ChkCallVolChg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallVolChg.CheckedChanged
        IsChange = True
        If ChkCallVolChg.Checked Then
            If Not ArrColSeries.Contains("col1CallVolChg") Then
                ArrColSeries.Add("col1CallVolChg")
            End If

            If Not ArrColSeries.Contains("col2CallVolChg") Then
                ArrColSeries.Add("col2CallVolChg")
            End If
            If Not ArrColSeries.Contains("col3CallVolChg") Then
                ArrColSeries.Add("col3CallVolChg")
            End If
        Else
            If ArrColSeries.Contains("col1CallVolChg") Then
                ArrColSeries.Remove("col1CallVolChg")
            End If
            If ArrColSeries.Contains("col2CallVolChg") Then
                ArrColSeries.Remove("col2CallVolChg")
            End If
            If ArrColSeries.Contains("col3CallVolChg") Then
                ArrColSeries.Remove("col3CallVolChg")
            End If
        End If
    End Sub


    Private Sub ChkPutVolChg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutVolChg.CheckedChanged
        IsChange = True
        If ChkPutVolChg.Checked Then
            If Not ArrColSeries.Contains("col1PutVolChg") Then
                ArrColSeries.Add("col1PutVolChg")
            End If
            If Not ArrColSeries.Contains("col2PutVolChg") Then
                ArrColSeries.Add("col2PutVolChg")
            End If
            If Not ArrColSeries.Contains("col3PutVolChg") Then
                ArrColSeries.Add("col3PutVolChg")
            End If
        Else
            If ArrColSeries.Contains("col1PutVolChg") Then
                ArrColSeries.Remove("col1PutVolChg")
            End If
            If ArrColSeries.Contains("col2PutVolChg") Then
                ArrColSeries.Remove("col2PutVolChg")
            End If
            If ArrColSeries.Contains("col3PutVolChg") Then
                ArrColSeries.Remove("col3PutVolChg")
            End If
        End If
    End Sub

    Private Sub ChkCallVol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallVol.CheckedChanged
        IsChange = True
        If ChkCallVol.Checked Then
            If Not ArrColSeries.Contains("col1CallVol") Then
                ArrColSeries.Add("col1CallVol")
            End If
            If Not ArrColSeries.Contains("col2CallVol") Then
                ArrColSeries.Add("col2CallVol")
            End If
            If Not ArrColSeries.Contains("col3CallVol") Then
                ArrColSeries.Add("col3CallVol")
            End If
        Else
            If ArrColSeries.Contains("col1CallVol") Then
                ArrColSeries.Remove("col1CallVol")
            End If
            If ArrColSeries.Contains("col2CallVol") Then
                ArrColSeries.Remove("col2CallVol")
            End If
            If ArrColSeries.Contains("col3CallVol") Then
                ArrColSeries.Remove("col3CallVol")
            End If
        End If
    End Sub

    Private Sub ChkPutVol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutVol.CheckedChanged
        IsChange = True
        If ChkPutVol.Checked Then
            If Not ArrColSeries.Contains("col1PutVol") Then
                ArrColSeries.Add("col1PutVol")
            End If

            If Not ArrColSeries.Contains("col2PutVol") Then
                ArrColSeries.Add("col2PutVol")
            End If
            If Not ArrColSeries.Contains("col3PutVol") Then
                ArrColSeries.Add("col3PutVol")
            End If
        Else
            If ArrColSeries.Contains("col1PutVol") Then
                ArrColSeries.Remove("col1PutVol")
            End If
            If ArrColSeries.Contains("col2PutVol") Then
                ArrColSeries.Remove("col2PutVol")
            End If
            If ArrColSeries.Contains("col3PutVol") Then
                ArrColSeries.Remove("col3PutVol")
            End If
        End If
    End Sub

    Private Sub ChkCE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCE.CheckedChanged
        IsChange = True
        If ChkCE.Checked Then
            If Not ArrColSeries.Contains("col1CE") Then
                ArrColSeries.Add("col1CE")
                ' ArrColSeries.Add("col1CallChg")
            End If
            If Not ArrColSeries.Contains("col2CE") Then
                ArrColSeries.Add("col2CE")
                ' ArrColSeries.Add("col2CallChg")
            End If
            If Not ArrColSeries.Contains("col3CE") Then
                ArrColSeries.Add("col3CE")
                ' ArrColSeries.Add("col3CallChg")
            End If
        Else
            If ArrColSeries.Contains("col1CE") Then
                ArrColSeries.Remove("col1CE")
                '  ArrColSeries.Add("col1CallChg")
            End If

            If ArrColSeries.Contains("col2CE") Then
                ArrColSeries.Remove("col2CE")
                ' ArrColSeries.Add("col2CallChg")
            End If
            If ArrColSeries.Contains("col3CE") Then
                ArrColSeries.Remove("col3CE")
                '  ArrColSeries.Add("col3CallChg")
            End If
        End If
    End Sub

    Private Sub ChkPE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPE.CheckedChanged
        IsChange = True
        If ChkPE.Checked Then
            If Not ArrColSeries.Contains("col1PE") Then
                ArrColSeries.Add("col1PE")
                '   ArrColSeries.Add("col1PutChg")
            End If

            If Not ArrColSeries.Contains("col2PE") Then
                ArrColSeries.Add("col2PE")
                '  ArrColSeries.Add("col2PutChg")
            End If
            If Not ArrColSeries.Contains("col3PE") Then
                ArrColSeries.Add("col3PE")
                'ArrColSeries.Add("col3PutChg")
            End If
        Else
            If ArrColSeries.Contains("col1PE") Then
                ArrColSeries.Remove("col1PE")
                ' ArrColSeries.Add("col1PutChg")
            End If
            If ArrColSeries.Contains("col2PE") Then
                ArrColSeries.Remove("col2PE")
                ' ArrColSeries.Add("col2PutChg")
            End If
            If ArrColSeries.Contains("col3PE") Then
                ArrColSeries.Remove("col3PE")
                ' ArrColSeries.Add("col3PutChg")
            End If
        End If
    End Sub

    Private Sub ChkPCP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPCP.CheckedChanged
        IsChange = True
        If ChkPCP.Checked Then
            If Not ArrColSeries.Contains("col1PCP") Then
                ArrColSeries.Add("col1PCP")
            End If
            If Not ArrColSeries.Contains("col2PCP") Then
                ArrColSeries.Add("col2PCP")
            End If
            If Not ArrColSeries.Contains("col3PCP") Then
                ArrColSeries.Add("col3PCP")
            End If

            'Bid
            If Not ArrColSeries.Contains("col1PCPB") Then
                ArrColSeries.Add("col1PCPB")
            End If
            If Not ArrColSeries.Contains("col2PCPB") Then
                ArrColSeries.Add("col2PCPB")
            End If
            If Not ArrColSeries.Contains("col3PCPB") Then
                ArrColSeries.Add("col3PCPB")
            End If

            'Ask
            If Not ArrColSeries.Contains("col1PCPA") Then
                ArrColSeries.Add("col1PCPA")
            End If
            If Not ArrColSeries.Contains("col2PCPA") Then
                ArrColSeries.Add("col2PCPA")
            End If
            If Not ArrColSeries.Contains("col3PCPA") Then
                ArrColSeries.Add("col3PCPA")
            End If
        Else
            If ArrColSeries.Contains("col1PCP") Then
                ArrColSeries.Remove("col1PCP")
            End If
            If ArrColSeries.Contains("col2PCP") Then
                ArrColSeries.Remove("col2PCP")
            End If
            If ArrColSeries.Contains("col3PCP") Then
                ArrColSeries.Remove("col3PCP")
            End If

            'Bid
            If ArrColSeries.Contains("col1PCPB") Then
                ArrColSeries.Remove("col1PCPB")
            End If
            If ArrColSeries.Contains("col2PCPB") Then
                ArrColSeries.Remove("col2PCPB")
            End If
            If ArrColSeries.Contains("col3PCPB") Then
                ArrColSeries.Remove("col3PCPB")
            End If

            'Ask
            If ArrColSeries.Contains("col1PCPA") Then
                ArrColSeries.Remove("col1PCPA")
            End If
            If ArrColSeries.Contains("col2PCPA") Then
                ArrColSeries.Remove("col2PCPA")
            End If
            If ArrColSeries.Contains("col3PCPA") Then
                ArrColSeries.Remove("col3PCPA")
            End If
        End If
    End Sub

    Private Sub ChkC2P_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkC2P.CheckedChanged
        IsChange = True
        If ChkC2P.Checked Then
            If Not ArrColSeries.Contains("col1C2P") Then
                ArrColSeries.Add("col1C2P")
            End If
            If Not ArrColSeries.Contains("col2C2P") Then
                ArrColSeries.Add("col2C2P")
            End If
            If Not ArrColSeries.Contains("col3C2P") Then
                ArrColSeries.Add("col3C2P")
            End If
        Else
            If ArrColSeries.Contains("col1C2P") Then
                ArrColSeries.Remove("col1C2P")
            End If
            If ArrColSeries.Contains("col2C2P") Then
                ArrColSeries.Remove("col2C2P")
            End If
            If ArrColSeries.Contains("col3C2P") Then
                ArrColSeries.Remove("col3C2P")
            End If
        End If
    End Sub

    Private Sub ChkP2P_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkP2P.CheckedChanged
        IsChange = True
        If ChkP2P.Checked Then
            If Not ArrColSeries.Contains("col1P2P") Then
                ArrColSeries.Add("col1P2P")
            End If
            If Not ArrColSeries.Contains("col2P2P") Then
                ArrColSeries.Add("col2P2P")
            End If
            If Not ArrColSeries.Contains("col3P2P") Then
                ArrColSeries.Add("col3P2P")
            End If
        Else
            If ArrColSeries.Contains("col1P2P") Then
                ArrColSeries.Remove("col1P2P")
            End If
            If ArrColSeries.Contains("col2P2P") Then
                ArrColSeries.Remove("col2P2P")
            End If
            If ArrColSeries.Contains("col3P2P") Then
                ArrColSeries.Remove("col3P2P")
            End If
        End If
    End Sub

    Private Sub ChkC2C_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkC2C.CheckedChanged
        IsChange = True
        If ChkC2C.Checked Then
            If Not ArrColSeries.Contains("col1C2C") Then
                ArrColSeries.Add("col1C2C")
            End If
            If Not ArrColSeries.Contains("col2C2C") Then
                ArrColSeries.Add("col2C2C")
            End If
            If Not ArrColSeries.Contains("col3C2C") Then
                ArrColSeries.Add("col3C2C")
            End If
        Else
            If ArrColSeries.Contains("col1C2C") Then
                ArrColSeries.Remove("col1C2C")
            End If
            If ArrColSeries.Contains("col2C2C") Then
                ArrColSeries.Remove("col2C2C")
            End If
            If ArrColSeries.Contains("col3C2C") Then
                ArrColSeries.Remove("col3C2C")
            End If
        End If
    End Sub

    Private Sub ChkCalender_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallCalender.CheckedChanged
        IsChange = True
        If ChkCallCalender.Checked Then
            If Not ArrColSeries.Contains("col1CallCalender") Then
                ArrColSeries.Add("col1CallCalender")
            End If
            If Not ArrColSeries.Contains("col2CallCalender") Then
                ArrColSeries.Add("col2CallCalender")
            End If
            If Not ArrColSeries.Contains("col3CallCalender") Then
                ArrColSeries.Add("col3CallCalender")
            End If
        Else
            If ArrColSeries.Contains("col1CallCalender") Then
                ArrColSeries.Remove("col1CallCalender")
            End If
            If ArrColSeries.Contains("col2CallCalender") Then
                ArrColSeries.Remove("col2CallCalender")
            End If
            If ArrColSeries.Contains("col3CallCalender") Then
                ArrColSeries.Remove("col3CallCalender")
            End If
        End If
    End Sub



    Private Sub ChkCallBullSpread_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallBullSpread.CheckedChanged
        IsChange = True
        If ChkCallBullSpread.Checked Then
            If Not ArrColSeries.Contains("col1CallBullSpread") Then
                ArrColSeries.Add("col1CallBullSpread")
            End If
            If Not ArrColSeries.Contains("col2CallBullSpread") Then
                ArrColSeries.Add("col2CallBullSpread")
            End If
            If Not ArrColSeries.Contains("col3CallBullSpread") Then
                ArrColSeries.Add("col3CallBullSpread")
            End If
        Else
            If ArrColSeries.Contains("col1CallBullSpread") Then
                ArrColSeries.Remove("col1CallBullSpread")
            End If
            If ArrColSeries.Contains("col2CallBullSpread") Then
                ArrColSeries.Remove("col2CallBullSpread")
            End If

            If ArrColSeries.Contains("col3CallBullSpread") Then
                ArrColSeries.Remove("col3CallBullSpread")
            End If

        End If
    End Sub

    Private Sub ChkPutBearSpread_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutBearSpread.CheckedChanged
        IsChange = True
        If ChkPutBearSpread.Checked Then
            If Not ArrColSeries.Contains("col1PutBearSpread") Then
                ArrColSeries.Add("col1PutBearSpread")
            End If
            If Not ArrColSeries.Contains("col2PutBearSpread") Then
                ArrColSeries.Add("col2PutBearSpread")
            End If
            If Not ArrColSeries.Contains("col3PutBearSpread") Then
                ArrColSeries.Add("col3PutBearSpread")
            End If
        Else
            If ArrColSeries.Contains("col1PutBearSpread") Then
                ArrColSeries.Remove("col1PutBearSpread")
            End If
            If ArrColSeries.Contains("col2PutBearSpread") Then
                ArrColSeries.Remove("col2PutBearSpread")
            End If
            If ArrColSeries.Contains("col3PutBearSpread") Then
                ArrColSeries.Remove("col3PutBearSpread")
            End If
        End If
    End Sub

    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click

        Me.Close()
    End Sub

    Private Sub frmSelectColumns_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Function ShowForm() As Boolean
        Me.ShowDialog()
        Return IsChange
    End Function

    Private Sub ChkPutCallender_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutCallender.CheckedChanged
        IsChange = True
        If ChkPutCallender.Checked Then
            If Not ArrColSeries.Contains("col1PutCalender") Then
                ArrColSeries.Add("col1PutCalender")
            End If
            If Not ArrColSeries.Contains("col2PutCalender") Then
                ArrColSeries.Add("col2PutCalender")
            End If
            If Not ArrColSeries.Contains("col3PutCalender") Then
                ArrColSeries.Add("col3PutCalender")
            End If
        Else
            If ArrColSeries.Contains("col1PutCalender") Then
                ArrColSeries.Remove("col1PutCalender")
            End If
            If ArrColSeries.Contains("col2PutCalender") Then
                ArrColSeries.Remove("col2PutCalender")
            End If
            If ArrColSeries.Contains("col3PutCalender") Then
                ArrColSeries.Remove("col3PutCalender")
            End If
        End If
    End Sub

    Private Sub frmSelectColumns_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        IsChange = False

        If ArrColSeries.Contains("col1CallOI") Then
            ChkCallOI.Checked = True
        Else
            ChkCallOI.Checked = False
        End If
        If ArrColSeries.Contains("CR1") Then
            CBCR.Checked = True
        Else
            CBCR.Checked = True = False
        End If
        If ArrColSeries.Contains("col2CallOI") Then
            ChkCallOI.Checked = True
        Else
            ChkCallOI.Checked = False
        End If
        If ArrColSeries.Contains("col1CallOI") Then
            ChkCallOI.Checked = True
        Else
            ChkCallOI.Checked = False
        End If


        If ArrColSeries.Contains("col1CallStraddle") Then
            ChkStraddle.Checked = True
        Else
            ChkStraddle.Checked = False
        End If

        If ArrColSeries.Contains("col1PutOI") Then
            ChkPutOI.Checked = True
        Else
            ChkPutOI.Checked = False
        End If

        If ArrColSeries.Contains("col1CallBF") Then
            ChkCallBF.Checked = True
        Else
            ChkCallBF.Checked = False
        End If

        If ArrColSeries.Contains("col1PutBF") Then
            ChkPutBF.Checked = True
        Else
            ChkPutBF.Checked = False
        End If

        If ArrColSeries.Contains("col1CallBF2") Then
            ChkCallBF2.Checked = True
        Else
            ChkCallBF2.Checked = False
        End If

        If ArrColSeries.Contains("col1PutBF2") Then
            ChkPutBF2.Checked = True
        Else
            ChkPutBF2.Checked = False
        End If

        If ArrColSeries.Contains("col1CallRatio") Then
            ChkCallRatio.Checked = True
        Else
            ChkCallRatio.Checked = False
        End If

        If ArrColSeries.Contains("col1PutRatio") Then
            ChkPutRatio.Checked = True
        Else
            ChkPutRatio.Checked = False
        End If

        If ArrColSeries.Contains("col1CallVolume") Then
            ChkCallVolume.Checked = True
        Else
            ChkCallVolume.Checked = False
        End If

        If ArrColSeries.Contains("col1PutVolume") Then
            ChkPutVolume.Checked = True
        Else
            ChkPutVolume.Checked = False
        End If

        If ArrColSeries.Contains("col1CallDelta") Then
            ChkCallDelta.Checked = True
        Else
            ChkCallDelta.Checked = False
        End If

        If ArrColSeries.Contains("col1PutDelta") Then
            ChkPutDelta.Checked = True
        Else
            ChkPutDelta.Checked = False
        End If

        If ArrColSeries.Contains("col1CallGamma") Then
            ChkCallGamma.Checked = True
        Else
            ChkCallGamma.Checked = False
        End If

        If ArrColSeries.Contains("col1PutGamma") Then
            ChkPutGamma.Checked = True
        Else
            ChkPutGamma.Checked = False
        End If

        If ArrColSeries.Contains("col1CallVega") Then
            ChkCallVega.Checked = True
        Else
            ChkCallVega.Checked = False
        End If

        If ArrColSeries.Contains("col1PutVega") Then
            ChkPutVega.Checked = True
        Else
            ChkPutVega.Checked = False
        End If

        If ArrColSeries.Contains("col1CallTheta") Then
            ChkCallTheta.Checked = True
        Else
            ChkCallTheta.Checked = False
        End If

        If ArrColSeries.Contains("col1PutTheta") Then
            ChkPutTheta.Checked = True
        Else
            ChkPutTheta.Checked = False
        End If

        If ArrColSeries.Contains("col1CallVolChg") Then
            ChkCallVolChg.Checked = True
        Else
            ChkCallVolChg.Checked = False
        End If

        If ArrColSeries.Contains("col1PutVolChg") Then
            ChkPutVolChg.Checked = True
        Else
            ChkPutVolChg.Checked = False
        End If

        If ArrColSeries.Contains("col1CallVol") Then
            ChkCallVol.Checked = True
        Else
            ChkCallVol.Checked = False
        End If

        If ArrColSeries.Contains("col1PutVol") Then
            ChkPutVol.Checked = True
        Else
            ChkPutVol.Checked = False
        End If

        If ArrColSeries.Contains("col1CE") Then
            ChkCE.Checked = True
        Else
            ChkCE.Checked = False
        End If

        If ArrColSeries.Contains("col1PE") Then
            ChkPE.Checked = True
        Else
            ChkPE.Checked = False
        End If

        If ArrColSeries.Contains("col1PCP") Then
            ChkPCP.Checked = True
        Else
            ChkPCP.Checked = False
        End If

        If ArrColSeries.Contains("col1C2P") Then
            ChkC2P.Checked = True
        Else
            ChkC2P.Checked = False
        End If


        If ArrColSeries.Contains("col1P2P") Then
            ChkP2P.Checked = True
        Else
            ChkP2P.Checked = False
        End If

        If ArrColSeries.Contains("col1C2C") Then
            ChkC2C.Checked = True
        Else
            ChkC2C.Checked = False
        End If

        If ArrColSeries.Contains("col1CallCalender") Then
            ChkCallCalender.Checked = True
        Else
            ChkCallCalender.Checked = False
        End If

        If ArrColSeries.Contains("col1CallBullSpread") Then
            ChkCallBullSpread.Checked = True
        Else
            ChkCallBullSpread.Checked = False
        End If

        If ArrColSeries.Contains("col1PutBearSpread") Then
            ChkPutBearSpread.Checked = True
        Else
            ChkPutBearSpread.Checked = False
        End If

        If ArrColSeries.Contains("col1PutCalender") Then
            ChkPutCallender.Checked = True
        Else
            ChkPutCallender.Checked = False
        End If

        If ArrColSeries.Contains("col1TotalOI") Then
            ChkTotalOI.Checked = True
        Else
            ChkTotalOI.Checked = False
        End If

        If ArrColSeries.Contains("col1TotalOIPer") Then
            ChkTotalOIPer.Checked = True
        Else
            ChkTotalOIPer.Checked = False
        End If


        If ArrColSeries.Contains("col1CallTrend") Then
            ChkCallTrend.Checked = True
        Else
            ChkCallTrend.Checked = False
        End If



        If ArrColSeries.Contains("col1CallStopLoss") Then
            ChkCallStopLoss.Checked = True
        Else
            ChkCallStopLoss.Checked = False
        End If



        If ArrColSeries.Contains("col1PutTrend") Then
            ChkPutTrend1.Checked = True
        Else
            ChkPutTrend1.Checked = False
        End If




        If ArrColSeries.Contains("col1PutStopLoss") Then
            ChkPutStopLoss.Checked = True
        Else
            ChkPutStopLoss.Checked = False
        End If
        If ArrColSeries.Contains("col1CallVol1") Then
            ChkCallVol1.Checked = True
        Else
            ChkCallVol1.Checked = False
        End If
        If ArrColSeries.Contains("col1PutVol1") Then
            ChkPutVol1.Checked = True
        Else
            ChkPutVol1.Checked = False
        End If


        If ArrColSeries.Contains("col1Strike1") Then
            ChkStrike1.Checked = True
        Else
            ChkStrike1.Checked = False
        End If
        If ArrColSeries.Contains("col1Strike2") Then
            ChkStrike2.Checked = True
        Else
            ChkStrike2.Checked = False
        End If
        If ArrColSeries.Contains("col1Strike3") Then
            ChkStrike3.Checked = True
        Else
            ChkStrike3.Checked = False
        End If

        If ArrColSeries.Contains("col1CallOIChg") Then
            chkCallOIChg.Checked = True
        Else
            chkCallOIChg.Checked = False
        End If

        If ArrColSeries.Contains("col1CallOIPer") Then
            chkPutOIPer.Checked = True
        Else
            chkPutOIPer.Checked = False
        End If

        If ArrColSeries.Contains("col1PutOIPer") Then
            chkPutOIPer.Checked = True
        Else
            chkPutOIPer.Checked = False
        End If

        If ArrColSeries.Contains("col1PutOIChg") Then
            chkPutOIChg.Checked = True
        Else
            chkPutOIChg.Checked = False
        End If


        If ArrColSeries.Contains("col1CallChg") Then
            chkCallChg.Checked = True
        Else
            chkCallChg.Checked = False
        End If

        If ArrColSeries.Contains("col1PutChg") Then
            chkPutChg.Checked = True
        Else
            chkPutChg.Checked = False
        End If

        If ArrColSeries.Contains("col1CallOIPer") Then
            chkCallOIPer.Checked = True
        Else
            chkCallOIPer.Checked = False
        End If


        If ArrColSeries.Contains("col1PutTotalOIPer") Then
            ChkTotalOIPer.Checked = True
        Else
            ChkTotalOIPer.Checked = False
        End If

        IsChange = False

        'col1CallVol1()
    End Sub

    Private Sub ChkStraddle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkStraddle.CheckedChanged
        IsChange = True
        If ChkStraddle.Checked Then
            If Not ArrColSeries.Contains("col1CallStraddle") Then
                ArrColSeries.Add("col1CallStraddle")
            End If
            If Not ArrColSeries.Contains("col2CallStraddle") Then
                ArrColSeries.Add("col2CallStraddle")
            End If
            If Not ArrColSeries.Contains("col3CallStraddle") Then
                ArrColSeries.Add("col3CallStraddle")
            End If
        Else
            If ArrColSeries.Contains("col1CallStraddle") Then
                ArrColSeries.Remove("col1CallStraddle")
            End If
            If ArrColSeries.Contains("col2CallStraddle") Then
                ArrColSeries.Remove("col2CallStraddle")
            End If
            If ArrColSeries.Contains("col3CallStraddle") Then
                ArrColSeries.Remove("col3CallStraddle")
            End If
        End If
    End Sub

    Private Sub ChkTotalOI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTotalOI.CheckedChanged
        IsChange = True
        If ChkTotalOI.Checked Then
            If Not ArrColSeries.Contains("col1TotalOI") Then
                ArrColSeries.Add("col1TotalOI")
                If Not ArrColSeries.Contains("col2TotalOI") Then
                    ArrColSeries.Add("col2TotalOI")
                End If
                If Not ArrColSeries.Contains("col3TotalOI") Then
                    ArrColSeries.Add("col3TotalOI")
                End If
            End If
        Else
            If ArrColSeries.Contains("col1TotalOI") Then
                ArrColSeries.Remove("col1TotalOI")
            End If
            If ArrColSeries.Contains("col2TotalOI") Then
                ArrColSeries.Remove("col2TotalOI")
            End If
            If ArrColSeries.Contains("col3TotalOI") Then
            End If
        End If
    End Sub

    Private Sub ChkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSelectAll.CheckedChanged
        IsChange = True
        If ChkSelectAll.Checked Then
            ChkCallOI.Checked = True
            ChkPutOI.Checked = True
            ChkCallBF.Checked = True
            ChkPutBF.Checked = True
            ChkCallBF2.Checked = True
            ChkPutBF2.Checked = True
            ChkCallRatio.Checked = True
            ChkPutRatio.Checked = True
            ChkCallVolume.Checked = True
            ChkPutVolume.Checked = True
            ChkCallDelta.Checked = True
            ChkPutDelta.Checked = True
            ChkCallGamma.Checked = True
            ChkPutGamma.Checked = True
            ChkCallVega.Checked = True
            ChkPutVega.Checked = True
            ChkCallTheta.Checked = True
            ChkPutTheta.Checked = True
            ChkCallVolChg.Checked = True
            ChkPutVolChg.Checked = True
            ChkCallVol.Checked = True
            ChkPutVol.Checked = True
            ChkCE.Checked = True
            ChkPE.Checked = True
            ChkPCP.Checked = True
            ChkC2P.Checked = True
            ChkP2P.Checked = True
            ChkC2C.Checked = True
            ChkCallCalender.Checked = True
            ChkCallBullSpread.Checked = True
            ChkPutBearSpread.Checked = True
            ChkPutCallender.Checked = True
            ChkTotalOI.Checked = True
            ChkStraddle.Checked = True
            ChkCallTrend.Checked = True
            ChkCallStopLoss.Checked = True
            ChkPutTrend1.Checked = True
            ChkPutStopLoss.Checked = True
            ChkCallVol1.Checked = True
            ChkPutVol1.Checked = True
            ChkStrike1.Checked = True
            ChkStrike2.Checked = True
            ChkStrike3.Checked = True
            chkCallOIChg.Checked = True
            chkCallOIPer.Checked = True

            chkPutOIChg.Checked = True
            chkPutOIPer.Checked = True

            ChkTotalOIPer.Checked = True
            chkCallChg.Checked = True
            chkPutChg.Checked = True
        Else
            ChkCallOI.Checked = False
            ChkPutOI.Checked = False
            ChkCallBF.Checked = False
            ChkPutBF.Checked = False
            ChkCallBF2.Checked = False
            ChkPutBF2.Checked = False
            ChkCallRatio.Checked = False
            ChkPutRatio.Checked = False
            ChkCallVolume.Checked = False
            ChkPutVolume.Checked = False
            ChkCallDelta.Checked = False
            ChkPutDelta.Checked = False
            ChkCallGamma.Checked = False
            ChkPutGamma.Checked = False
            ChkCallVega.Checked = False
            ChkPutVega.Checked = False
            ChkCallTheta.Checked = False
            ChkPutTheta.Checked = False
            ChkCallVolChg.Checked = False
            ChkPutVolChg.Checked = False
            ChkCallVol.Checked = False
            ChkPutVol.Checked = False
            ChkCE.Checked = False
            ChkPE.Checked = False
            ChkPCP.Checked = False
            ChkC2P.Checked = False
            ChkP2P.Checked = False
            ChkC2C.Checked = False
            ChkCallCalender.Checked = False
            ChkCallBullSpread.Checked = False
            ChkPutBearSpread.Checked = False
            ChkPutCallender.Checked = False
            ChkTotalOI.Checked = False
            ChkStraddle.Checked = False
            ChkCallTrend.Checked = False
            ChkCallStopLoss.Checked = False
            ChkPutTrend1.Checked = False
            ChkPutStopLoss.Checked = False
            ChkCallVol1.Checked = False
            ChkPutVol1.Checked = False
            ChkStrike1.Checked = False
            ChkStrike2.Checked = False
            ChkStrike3.Checked = False
            chkCallOIChg.Checked = False
            chkCallOIPer.Checked = False

            chkPutOIChg.Checked = False
            chkPutOIPer.Checked = False
            chkCallChg.Checked = False
            chkPutChg.Checked = False
            ChkTotalOIPer.Checked = False
        End If
    End Sub

    Private Sub ChkPutGamma_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutGamma.CheckedChanged
        IsChange = True
        If ChkPutGamma.Checked Then
            If Not ArrColSeries.Contains("col1PutGamma") Then
                ArrColSeries.Add("col1PutGamma")
            End If
            If Not ArrColSeries.Contains("col2PutGamma") Then
                ArrColSeries.Add("col2PutGamma")
            End If
            If Not ArrColSeries.Contains("col3PutGamma") Then
                ArrColSeries.Add("col3PutGamma")
            End If
        Else
            If ArrColSeries.Contains("col1PutGamma") Then
                ArrColSeries.Remove("col1PutGamma")
            End If
            If ArrColSeries.Contains("col2PutGamma") Then
                ArrColSeries.Remove("col2PutGamma")
            End If
            If ArrColSeries.Contains("col3PutGamma") Then
                ArrColSeries.Remove("col3PutGamma")
            End If
        End If
    End Sub


    Private Sub ChkCallTrend_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallTrend.CheckedChanged
        IsChange = True
        If ChkCallTrend.Checked Then
            If Not ArrColSeries.Contains("col1CallTrend") Then
                ArrColSeries.Add("col1CallTrend")
            End If
            If Not ArrColSeries.Contains("col2CallTrend") Then
                ArrColSeries.Add("col2CallTrend")
            End If
            If Not ArrColSeries.Contains("col3CallTrend") Then
                ArrColSeries.Add("col3CallTrend")
            End If
        Else
            If ArrColSeries.Contains("col1CallTrend") Then
                ArrColSeries.Remove("col1CallTrend")
            End If
            If ArrColSeries.Contains("col2CallTrend") Then
                ArrColSeries.Remove("col2CallTrend")
            End If
            If ArrColSeries.Contains("col3CallTrend") Then
                ArrColSeries.Remove("col3CallTrend")
            End If
        End If
    End Sub

    Private Sub ChkCallStopLoss_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallStopLoss.CheckedChanged
        IsChange = True
        If ChkCallStopLoss.Checked Then
            If Not ArrColSeries.Contains("col1CallStopLoss") Then
                ArrColSeries.Add("col1CallStopLoss")
            End If
            If Not ArrColSeries.Contains("col2CallStopLoss") Then
                ArrColSeries.Add("col2CallStopLoss")
            End If
            If Not ArrColSeries.Contains("col3CallStopLoss") Then
                ArrColSeries.Add("col3CallStopLoss")
            End If
        Else
            If ArrColSeries.Contains("col1CallStopLoss") Then
                ArrColSeries.Remove("col1CallStopLoss")
            End If
            If ArrColSeries.Contains("col2CallStopLoss") Then
                ArrColSeries.Remove("col2CallStopLoss")
            End If
            If ArrColSeries.Contains("col3CallStopLoss") Then
                ArrColSeries.Remove("col3CallStopLoss")
            End If
        End If
    End Sub

    'Private Sub ChkPutTrend_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutTrend.CheckedChanged
    '    If ChkPutTrend.Checked Then
    '        If Not ArrColSeries.Contains("col1PutTrend") Then
    '            ArrColSeries.Add("col1PutTrend")
    '        End If
    '    Else
    '        If ArrColSeries.Contains("col1PutTrend") Then
    '            ArrColSeries.Remove("col1PutTrend")
    '        End If
    '    End If
    'End Sub

    Private Sub ChkPutStopLoss_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutStopLoss.CheckedChanged
        IsChange = True
        If ChkPutStopLoss.Checked Then
            If Not ArrColSeries.Contains("col1PutStopLoss") Then
                ArrColSeries.Add("col1PutStopLoss")
            End If
            If Not ArrColSeries.Contains("col2PutStopLoss") Then
                ArrColSeries.Add("col2PutStopLoss")
            End If
            If Not ArrColSeries.Contains("col3PutStopLoss") Then
                ArrColSeries.Add("col3PutStopLoss")
            End If
        Else
            If ArrColSeries.Contains("col1PutStopLoss") Then
                ArrColSeries.Remove("col1PutStopLoss")
            End If
            If ArrColSeries.Contains("col2PutStopLoss") Then
                ArrColSeries.Remove("col2PutStopLoss")
            End If
            If ArrColSeries.Contains("col3PutStopLoss") Then
                ArrColSeries.Remove("col3PutStopLoss")
            End If
        End If
    End Sub

    Private Sub TableLayoutPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)


    End Sub


    Private Sub ChkPutTrend1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutTrend1.CheckedChanged
        IsChange = True
        If ChkPutTrend1.Checked Then
            If Not ArrColSeries.Contains("col1PutTrend") Then
                ArrColSeries.Add("col1PutTrend")
            End If
            If Not ArrColSeries.Contains("col2PutTrend") Then
                ArrColSeries.Add("col2PutTrend")
            End If
            If Not ArrColSeries.Contains("col3PutTrend") Then
                ArrColSeries.Add("col3PutTrend")
            End If
        Else
            If ArrColSeries.Contains("col1PutTrend") Then
                ArrColSeries.Remove("col1PutTrend")
            End If
            If Not ArrColSeries.Contains("col2PutTrend") Then
                ArrColSeries.Remove("col2PutTrend")
            End If
            If Not ArrColSeries.Contains("col3PutTrend") Then
                ArrColSeries.Remove("col3PutTrend")
            End If
        End If
    End Sub


    Private Sub ChkCallVol1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCallVol1.CheckedChanged

        IsChange = True
        If ChkCallVol1.Checked Then
            If Not ArrColSeries.Contains("col1CallVol1") Then
                ArrColSeries.Add("col1CallVol1")
            End If
            If Not ArrColSeries.Contains("col2CallVol1") Then
                ArrColSeries.Add("col2CallVol1")
            End If
            If Not ArrColSeries.Contains("col3CallVol1") Then
                ArrColSeries.Add("col3CallVol1")
            End If
        Else
            If ArrColSeries.Contains("col1CallVol1") Then
                ArrColSeries.Remove("col1CallVol1")
            End If
            If Not ArrColSeries.Contains("col2CallVol1") Then
                ArrColSeries.Remove("col2CallVol1")
            End If
            If Not ArrColSeries.Contains("col3CallVol1") Then
                ArrColSeries.Remove("col3CallVol1")
            End If
        End If
    End Sub

    Private Sub ChkPutVol1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPutVol1.CheckedChanged
        IsChange = True
        If ChkPutVol1.Checked Then
            If Not ArrColSeries.Contains("col1PutVol1") Then
                ArrColSeries.Add("col1PutVol1")
            End If
            If Not ArrColSeries.Contains("col2PutVol1") Then
                ArrColSeries.Add("col2PutVol1")
            End If
            If Not ArrColSeries.Contains("col3PutVol1") Then
                ArrColSeries.Add("col3PutVol1")
            End If
        Else
            If ArrColSeries.Contains("col1PutVol1") Then
                ArrColSeries.Remove("col1PutVol1")
            End If
            If Not ArrColSeries.Contains("col2PutVol1") Then
                ArrColSeries.Remove("col2PutVol1")
            End If
            If Not ArrColSeries.Contains("col3PutVol1") Then
                ArrColSeries.Remove("col3PutVol1")
            End If
        End If
    End Sub






    Private Sub ChkStrike1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkStrike1.CheckedChanged
        IsChange = True
        If ChkStrike1.Checked Then
            If Not ArrColSeries.Contains("col1Strike1") Then
                ArrColSeries.Add("col1Strike1")
                'ArrColSeries.Add("col2Strike1")
                'ArrColSeries.Add("col3Strike1")
            End If
        Else
            If ArrColSeries.Contains("col1Strike1") Then
                ArrColSeries.Remove("col1Strike1")
                'ArrColSeries.Remove("col2Strike1")
                'ArrColSeries.Remove("col3Strike1")
            End If
        End If
    End Sub

    Private Sub ChkStrike2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkStrike2.CheckedChanged
        IsChange = True
        If ChkStrike2.Checked Then
            If Not ArrColSeries.Contains("col1Strike2") Then
                ArrColSeries.Add("col1Strike2")
                'ArrColSeries.Add("col2Strike2")
                'ArrColSeries.Add("col2Strike3")

            End If
        Else
            If ArrColSeries.Contains("col1Strike2") Then
                ArrColSeries.Remove("col1Strike2")
                'ArrColSeries.Remove("col2Strike2")
                'ArrColSeries.Remove("col2Strike3")
            End If
        End If
    End Sub

    Private Sub ChkStrike3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkStrike3.CheckedChanged
        IsChange = True
        If ChkStrike3.Checked Then
            If Not ArrColSeries.Contains("col1Strike3") Then
                ArrColSeries.Add("col1Strike3")
                'ArrColSeries.Add("col3Strike2")
                'ArrColSeries.Add("col3Strike3")

            End If
        Else
            If ArrColSeries.Contains("col1Strike3") Then
                ArrColSeries.Remove("col1Strike3")
                'ArrColSeries.Remove("col3Strike2")
                'ArrColSeries.Remove("col3Strike3")
            End If
        End If
    End Sub

    Private Sub chkCallOIChg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCallOIChg.CheckedChanged
        IsChange = True
        If chkCallOIChg.Checked Then
            If Not ArrColSeries.Contains("col1CallOIChg") Then
                ArrColSeries.Add("col1CallOIChg")
            End If
        Else
            If ArrColSeries.Contains("col1CallOIChg") Then
                ArrColSeries.Remove("col1CallOIChg")
            End If
        End If

    End Sub




    Private Sub chkPutOIChg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPutOIChg.CheckedChanged
        IsChange = True
        If chkPutOIChg.Checked Then
            If Not ArrColSeries.Contains("col1PutOIChg") Then
                ArrColSeries.Add("col1PutOIChg")
            End If
        Else
            If ArrColSeries.Contains("col1PutOIChg") Then
                ArrColSeries.Remove("col1PutOIChg")
            End If
        End If
    End Sub

    Private Sub chkPutOIPer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPutOIPer.CheckedChanged
        IsChange = True
        If chkPutOIPer.Checked Then
            If Not ArrColSeries.Contains("col1PutOIPer") Then
                ArrColSeries.Add("col1PutOIPer")
            End If
        Else
            If ArrColSeries.Contains("col1PutOIPer") Then
                ArrColSeries.Remove("col1PutOIPer")
            End If
        End If
    End Sub


    Private Sub chkCallOIPer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCallOIPer.CheckedChanged
        IsChange = True
        If chkCallOIPer.Checked Then
            If Not ArrColSeries.Contains("col1CallOIPer") Then
                ArrColSeries.Add("col1CallOIPer")
            End If
        Else
            If ArrColSeries.Contains("col1CallOIPer") Then
                ArrColSeries.Remove("col1CallOIPer")
            End If
        End If
    End Sub

    Private Sub chkCallChg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCallChg.CheckedChanged
        IsChange = True
        If chkCallChg.Checked Then
            If Not ArrColSeries.Contains("col1CallChg") Then
                ArrColSeries.Add("col1CallChg")
            End If
        Else
            If ArrColSeries.Contains("col1CallChg") Then
                ArrColSeries.Remove("col1CallChg")
            End If
        End If
    End Sub

    Private Sub chkPutChg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPutChg.CheckedChanged
        IsChange = True
        If chkPutChg.Checked Then
            If Not ArrColSeries.Contains("col1PutChg") Then
                ArrColSeries.Add("col1PutChg")
            End If
        Else
            If ArrColSeries.Contains("col1PutChg") Then
                ArrColSeries.Remove("col1PutChg")
            End If
        End If
    End Sub


    Private Sub ChkTotalOIPer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTotalOIPer.CheckedChanged
        IsChange = True
        If ChkTotalOI.Checked Then
            If Not ArrColSeries.Contains("col1PutTotalOIPer") Then
                ArrColSeries.Add("col1PutTotalOIPer")
            End If
        Else
            If ArrColSeries.Contains("col1PutTotalOIPer") Then
                ArrColSeries.Remove("col1PutTotalOIPer")
            End If
        End If
    End Sub

    Private Sub CBCR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCR.CheckedChanged
        IsChange = True
        If CBCR.Checked Then
            If Not ArrColSeries.Contains("col1CR") Then
                ArrColSeries.Add("col1CR")
            End If
            If Not ArrColSeries.Contains("col2CR") Then
                ArrColSeries.Add("col2CR")
            End If
            If Not ArrColSeries.Contains("col3CR") Then
                ArrColSeries.Add("col3CR")
            End If
        Else
            If ArrColSeries.Contains("col1CR") Then
                ArrColSeries.Remove("col1CR")
            End If
            If ArrColSeries.Contains("col2CR") Then
                ArrColSeries.Remove("col2CR")
            End If
            If ArrColSeries.Contains("col3CR") Then
                ArrColSeries.Remove("col3CR")
            End If
        End If
    End Sub
End Class