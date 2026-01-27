Public Class FrmOptionChart
    Public grdcp As System.Windows.Forms.DataGridView
    Dim SelInx As Integer
    'Public Sub ShowForm(ByVal chtcallperm_p As AxMSChart20Lib.AxMSChart, ByVal chtputprem_p As AxMSChart20Lib.AxMSChart, ByVal chtcalldelta_p As AxMSChart20Lib.AxMSChart, ByVal chtputdelta_p As AxMSChart20Lib.AxMSChart, ByVal chtgamma_p As AxMSChart20Lib.AxMSChart, ByVal chtvega_p As AxMSChart20Lib.AxMSChart, ByVal chtcalltheta_p As AxMSChart20Lib.AxMSChart, ByVal chtputtheta_p As AxMSChart20Lib.AxMSChart, ByVal chtcallroh_p As AxMSChart20Lib.AxMSChart, ByVal chtputroh_p As AxMSChart20Lib.AxMSChart)
    '    'TabControl1 = CType(obj, TabControl)
    '    chtcallperm = chtcallperm_p
    '    chtputprem = chtputprem_p
    '    chtcalldelta = chtcalldelta_p
    '    chtputdelta = chtputdelta_p
    '    chtgamma = chtgamma_p
    '    chtvega = chtvega_p
    '    chtcalltheta = chtcalltheta_p
    '    chtputtheta = chtputtheta_p
    '    chtcallroh = chtcallroh_p
    '    chtputroh = chtputroh_p



    '    Me.ShowDialog()
    'End Sub
    Public Sub ShowForm(ByVal inx As Integer, ByVal grd As System.Windows.Forms.DataGridView)
        SelInx = inx
        grdcp = grd
        'TabControl1 = CType(obj, TabControl)
        Me.ShowDialog()
    End Sub

    Private Sub FrmOptionChart_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        TabControl1.SelectedIndex = SelInx
    End Sub

    Private Sub FrmOptionChart_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmOptionChart_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        'If e.KeyChar = Chr(27) Then
        '    Me.Close()
        'End If
    End Sub

    Private Sub FrmOptionChart_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp

    End Sub

    Private Sub FrmOptionChart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'chtcallperm.Refresh()
        'chtputprem.Refresh()
        'chtcalldelta.Refresh()
        'chtputdelta.Refresh()
        'chtgamma.Refresh()
        'chtvega.Refresh()
        'chtcalltheta.Refresh()
        'chtputtheta.Refresh()
        'chtcallroh.Refresh()
        'chtputroh.Refresh()

        'chtcallperm.Update()
        'chtputprem.Update()
        'chtcalldelta.Update()
        'chtputdelta.Update()
        'chtgamma.Update()
        'chtvega.Update()
        'chtcalltheta.Update()
        'chtputtheta.Update()
        'chtcallroh.Update()
        'chtputroh.Update()

    End Sub

    Private Sub TabControl1_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabControl1.DrawItem
        Dim g As Graphics = e.Graphics
        Dim tp As TabPage = TabControl1.TabPages(e.Index)
        Dim br As System.Drawing.Brush
        Dim sf As New StringFormat
        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width + 2, e.Bounds.Height - 2)
        sf.Alignment = StringAlignment.Near
        Dim strTitle As String = tp.Text
        If TabControl1.SelectedIndex = e.Index Then
            Dim f As Font = New Font(TabControl1.Font.Name, TabControl1.Font.Size, FontStyle.Regular, TabControl1.Font.Unit)
            br = New SolidBrush(Color.Black)
            g.FillRectangle(br, e.Bounds)
            '  g.FillRectangle(br, e.Bounds)
            br = New SolidBrush(Color.White)
            g.DrawString(strTitle, f, br, r, sf)
        Else
            Dim f As Font = New Font(TabControl1.Font.Name, TabControl1.Font.Size, FontStyle.Regular, TabControl1.Font.Unit)
            br = New SolidBrush(Color.WhiteSmoke)
            g.FillRectangle(br, e.Bounds)
            'g.FillRectangle(br, e.Bounds)
            br = New SolidBrush(Color.Black)
            g.DrawString(strTitle, f, br, r, sf)
        End If
        tp.Refresh()
    End Sub

    Private Sub chtcallperm_ChartSelected(ByVal sender As System.Object, ByVal e As AxMSChart20Lib._DMSChartEvents_ChartSelectedEvent)

    End Sub

    Private Sub Schtcallperm_DoubleClickEvent(ByVal sender As Object, ByVal e As System.EventArgs) Handles Schtcallperm.DoubleClickEvent, Schtputprem.DoubleClickEvent, Schtcalldelta.DoubleClickEvent, Schtputdelta.DoubleClickEvent, Schtgamma.DoubleClickEvent, Schtvega.DoubleClickEvent, Schtcalltheta.DoubleClickEvent, Schtputtheta.DoubleClickEvent, Schtcallroh.DoubleClickEvent, Schtputroh.DoubleClickEvent, StockChart1.DoubleClickEvent, SchtVolga.DoubleClickEvent, SchtVanna.DoubleClickEvent
        Me.Close()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        SelInx = TabControl1.SelectedIndex
    End Sub

    Private Sub PlotAllStockChart(ByVal chrtname As String)
        Dim Panel As Short
        Dim RowInx As Integer
        Dim Colour As Color
        Select Case chrtname.ToUpper
            Case "CallPremium".ToUpper
                RowInx = 0
                Colour = System.Drawing.Color.Red
            Case "PutPremium".ToUpper
                RowInx = 1
                Colour = System.Drawing.Color.Blue
            Case "CallDelta".ToUpper
                RowInx = 2
                Colour = System.Drawing.Color.Yellow
            Case "PutDelta".ToUpper
                RowInx = 3
                Colour = System.Drawing.Color.Green
            Case "CallTheta".ToUpper
                RowInx = 4
                Colour = System.Drawing.Color.SkyBlue
            Case "PutTheta".ToUpper
                RowInx = 5
                Colour = System.Drawing.Color.Violet
            Case "CallGamma".ToUpper
                RowInx = 6
                Colour = System.Drawing.Color.Cyan
            Case "PutGamma".ToUpper
                RowInx = 6
                Colour = System.Drawing.Color.Cyan
            Case "CallVega".ToUpper
                RowInx = 7
                Colour = System.Drawing.Color.DarkMagenta
            Case "PutVega".ToUpper
                RowInx = 7
                Colour = System.Drawing.Color.DarkMagenta
            Case "CallRoh".ToUpper
                RowInx = 8
                Colour = System.Drawing.Color.Pink
            Case "PutRoh".ToUpper
                RowInx = 9
                Colour = System.Drawing.Color.Tan
            Case "Volga".ToUpper
                RowInx = 10
                Colour = System.Drawing.Color.SpringGreen
            Case "Vanna".ToUpper
                RowInx = 11
                Colour = System.Drawing.Color.YellowGreen
        End Select






        Panel = StockChart1.AddChartPanel() REM to Get New Panel Index
        StockChart1.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart1.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(Colour)) REM Set Series Line Color
        StockChart1.set_SeriesWeight(chrtname, 2) REM Set Series Weight

        Dim VarChrtData As Double = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart1.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart1.AppendValue(chrtname, StockChart1.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next

    End Sub

    Private Sub btnSelectChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectChart.Click
        TabControl1.SelectTab("All")
        'If TabControl1.SelectedTab.Text = "All" Then
        optionfrm1.ObjSelectChart.ShowForm()
        optionfrm1.ObjSelectChart.ArrSeriesCol.Sort()

        StockChart1.RemoveAllSeries()

        'StockChart1.SetYScale
        StockChart1.ThreeDStyle = True REM To show chart in 3D
        StockChart1.RealTimeXLabels = False
        StockChart1.DisplayTitles = True REM To Set whether Chart Series display or Not
        StockChart1.ShowRecordsForXLabels = True

        For Each str As Object In optionfrm1.ObjSelectChart.ArrSeriesCol
            PlotAllStockChart(str.ToString.Substring(2, str.ToString.Length - 2))
        Next


        Dim TotalPanelHeight As Double = StockChart1.Height - 20
        For i As Integer = 1 To optionfrm1.ObjSelectChart.ArrSeriesCol.Count
            StockChart1.set_PanelY1(i, (TotalPanelHeight / optionfrm1.ObjSelectChart.ArrSeriesCol.Count) * i)
        Next

        Try
            StockChart1.Update()
        Catch ex As Exception

        End Try
        'End If
    End Sub

    Private Sub Schtcallroh_SelectSeries(ByVal sender As System.Object, ByVal e As AxSTOCKCHARTXLib._DStockChartXEvents_SelectSeriesEvent) Handles Schtcallroh.SelectSeries

    End Sub

    Private Sub Schtputroh_SelectSeries(ByVal sender As System.Object, ByVal e As AxSTOCKCHARTXLib._DStockChartXEvents_SelectSeriesEvent) Handles Schtputroh.SelectSeries
				 
    End Sub
End Class