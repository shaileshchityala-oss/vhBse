Public Class resultfrm
    Public temptable As New DataTable

    Private Sub resultfrm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub resultfrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        grdtrad.DataSource = temptable
        grdtrad.Refresh()
    End Sub
    Private Sub grdtrad_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdtrad.CellFormatting
        If e.ColumnIndex > 7 And e.RowIndex > -1 Then
            If Val(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf Val(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.DarkBlue
            Else
                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Black
            End If


            grdtrad.Rows(e.RowIndex).Cells("delta").Style.Format = scenario1.DecimalSetting.sDelta
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.Format = scenario1.DecimalSetting.sDeltaval

            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.Format = scenario1.DecimalSetting.sGamma
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.Format = scenario1.DecimalSetting.sGammaval

            grdtrad.Rows(e.RowIndex).Cells("vega").Style.Format = scenario1.DecimalSetting.sVega
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.Format = scenario1.DecimalSetting.sVegaval

            grdtrad.Rows(e.RowIndex).Cells("theta").Style.Format = scenario1.DecimalSetting.sTheta
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.Format = scenario1.DecimalSetting.sThetaval

            grdtrad.Rows(e.RowIndex).Cells("volga").Style.Format = scenario1.DecimalSetting.sVolga
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.Format = scenario1.DecimalSetting.sVolgaval

            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.Format = scenario1.DecimalSetting.sVanna
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.Format = scenario1.DecimalSetting.sVannaval

        End If

        If grdtrad.Rows(e.RowIndex).Cells("CPf").Value.ToString.ToUpper = "TOTAL" Then
            'If grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor <> Color.Black Then
            '    grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Black
            'End If
            grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.LightBlue
            grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Bold, GraphicsUnit.World))
        End If
    End Sub

    Public Sub ShowForm(ByVal Typ As String)
        grdtrad.Columns("delta").Visible = False
        grdtrad.Columns("deltaval").Visible = False
        grdtrad.Columns("theta").Visible = False
        grdtrad.Columns("thetaval").Visible = False
        grdtrad.Columns("vega").Visible = False
        grdtrad.Columns("vgval").Visible = False
        grdtrad.Columns("gamma").Visible = False
        grdtrad.Columns("gmval").Visible = False
        grdtrad.Columns("volga").Visible = False
        grdtrad.Columns("volgaval").Visible = False
        grdtrad.Columns("vanna").Visible = False
        grdtrad.Columns("vannaval").Visible = False
        grdtrad.Columns("pl").Visible = False

        Select Case (Typ.ToUpper)
            Case "PROFIT"
                grdtrad.Columns("pl").Visible = True
            Case "DELTA"
                grdtrad.Columns("delta").Visible = True
                grdtrad.Columns("deltaval").Visible = True
                grdtrad.Columns("pl").Visible = True

            Case "GAMMA"
                grdtrad.Columns("gamma").Visible = True
                grdtrad.Columns("gmval").Visible = True
                grdtrad.Columns("pl").Visible = True
            Case "VEGA"
                grdtrad.Columns("vega").Visible = True
                grdtrad.Columns("vgval").Visible = True
                grdtrad.Columns("pl").Visible = True
            Case "THETA"
                grdtrad.Columns("theta").Visible = True
                grdtrad.Columns("thetaval").Visible = True
                grdtrad.Columns("pl").Visible = True
            Case "VOLGA"
                grdtrad.Columns("volga").Visible = True
                grdtrad.Columns("volgaval").Visible = True
                grdtrad.Columns("pl").Visible = True
            Case "VANNA"
                grdtrad.Columns("vanna").Visible = True
                grdtrad.Columns("vannaval").Visible = True
                grdtrad.Columns("pl").Visible = True
        End Select


        Me.ShowDialog()
    End Sub
End Class