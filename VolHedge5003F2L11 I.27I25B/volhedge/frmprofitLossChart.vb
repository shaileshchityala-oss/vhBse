Imports System.Drawing
Public Class frmprofitLossChart

    Dim ColorSpot As Color
    Dim ColorMidSpot As Color

    Private Sub frmprofitLossChart_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If scenario1.chartName = "profit" Then
            tbcon.SelectedIndex = 0
            Label1.Text = scenario1.ValLBLprofit
        ElseIf scenario1.chartName = "delta" Then
            tbcon.SelectedIndex = 2
            lbldelta.Text = scenario1.ValLBLdelta
        ElseIf scenario1.chartName = "gamma" Then
            tbcon.SelectedIndex = 4
            lblgamma.Text = scenario1.ValLBLgamma
        ElseIf scenario1.chartName = "vega" Then
            tbcon.SelectedIndex = 6
            lblvega.Text = scenario1.ValLBLvega
        ElseIf scenario1.chartName = "theta" Then
            tbcon.SelectedIndex = 8
            lbltheta.Text = scenario1.ValLBLtheta
        ElseIf scenario1.chartName = "volga" Then
            tbcon.SelectedIndex = 10
            lblvolga.Text = scenario1.ValLBLvolga
        ElseIf scenario1.chartName = "vanna" Then
            tbcon.SelectedIndex = 12
            lblvanna.Text = scenario1.ValLBLVanna
        End If
    End Sub

    Private Sub tbcon_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles tbcon.DrawItem
        Dim g As Graphics = e.Graphics
        Dim tp As TabPage = tbcon.TabPages(e.Index)
        Dim br As System.Drawing.Brush
        Dim sf As New StringFormat
        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width + 2, e.Bounds.Height - 2)
        sf.Alignment = StringAlignment.Near
        Dim strTitle As String = tp.Text
        If tbcon.SelectedIndex = e.Index Then
            Dim f As Font = New Font(tbcon.Font.Name, tbcon.Font.Size, FontStyle.Regular, tbcon.Font.Unit)
            br = New SolidBrush(Color.Black)
            g.FillRectangle(br, e.Bounds)
            '  g.FillRectangle(br, e.Bounds)
            br = New SolidBrush(Color.White)
            g.DrawString(strTitle, f, br, r, sf)
        Else
            Dim f As Font = New Font(tbcon.Font.Name, tbcon.Font.Size, FontStyle.Regular, tbcon.Font.Unit)
            br = New SolidBrush(Color.WhiteSmoke)
            g.FillRectangle(br, e.Bounds)
            'g.FillRectangle(br, e.Bounds)
            br = New SolidBrush(Color.Black)
            g.DrawString(strTitle, f, br, r, sf)
        End If
        tp.Refresh()
    End Sub
    Private Sub chtprofit_DblClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Hide()
    End Sub

    Private Sub frmprofitLossChart_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
        End If
    End Sub
    Private Function Dt2String(pStr As String) As String
        Try
            Return CDate(pStr).ToString("dd-MMM-yyyy")
        Catch ex As Exception
            Return ""
        End Try

    End Function



    Public Sub ShowForm()
        'grdprofit.DataSource = scenario1.profit
        'grddelta.DataSource = scenario1.deltatable
        'grdgamma.DataSource = scenario1.gammatable
        'grdvega.DataSource = scenario1.vegatable
        'grdtheta.DataSource = scenario1.thetatable
        'grdvolga.DataSource = scenario1.volgatable
        'grdvanna.DataSource = scenario1.vannatable

        On Error Resume Next
        For Each c As DataGridViewColumn In grdprofit.Columns
            'c.HeaderText = CDate(c.HeaderText).ToString("dd-MMM-yyyy")
            c.HeaderText = Dt2String(c.HeaderText)
        Next


        For Each c As DataGridViewColumn In grddelta.Columns
            'c.HeaderText = CDate(c.HeaderText).ToString("dd-MMM-yyyy")
            c.HeaderText = Dt2String(c.HeaderText)
        Next


        For Each c As DataGridViewColumn In grdgamma.Columns
            'c.HeaderText = CDate(c.HeaderText).ToString("dd-MMM-yyyy")
            c.HeaderText = Dt2String(c.HeaderText)
        Next


        For Each c As DataGridViewColumn In grdvega.Columns
            'c.HeaderText = CDate(c.HeaderText).ToString("dd-MMM-yyyy")
            c.HeaderText = Dt2String(c.HeaderText)
        Next


        For Each c As DataGridViewColumn In grdtheta.Columns
            'c.HeaderText = CDate(c.HeaderText).ToString("dd-MMM-yyyy")
            c.HeaderText = Dt2String(c.HeaderText)
        Next


        For Each c As DataGridViewColumn In grdvolga.Columns
            'c.HeaderText = CDate(c.HeaderText).ToString("dd-MMM-yyyy")
            c.HeaderText = Dt2String(c.HeaderText)
        Next


        For Each c As DataGridViewColumn In grdvanna.Columns
            'c.HeaderText = CDate(c.HeaderText).ToString("dd-MMM-yyyy")
            c.HeaderText = Dt2String(c.HeaderText)
        Next

        Me.Show()
    End Sub

    Private Sub grdprofit_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdprofit.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex = 1 Then
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & 0
                e.Value = d.ToString(str)
            ElseIf e.ColumnIndex > 1 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                If Val(grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & RoundGrossMTM
                e.Value = d.ToString(str)
            End If
        End If

    End Sub
    Private Sub grdprofit_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdprofit.CellEnter, grddelta.CellEnter, grdgamma.CellEnter, grdtheta.CellEnter, grdvolga.CellEnter, grdvega.CellEnter, grdvanna.CellEnter
        'For Each grow As DataGridViewRow In CType(sender, DataGridView).Rows
        '    For i As Integer = 1 To CType(sender, DataGridView).Columns.Count - 1
        '        If grow.Selected = True Then
        '            grow.Cells(i).Style.Font = (New Font("Microsoft Sans Serif", 15, FontStyle.Bold, GraphicsUnit.World))
        '        Else
        '            grow.Cells(i).Style.Font = (New Font("Microsoft Sans Serif", 15, FontStyle.Regular, GraphicsUnit.World))
        '        End If
        '    Next
        'Next
    End Sub
    Private Sub grddelta_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grddelta.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex = 1 Then
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & 0
                e.Value = d.ToString(str)

            ElseIf e.ColumnIndex > 1 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                If Val(grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                '  grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = DecimalSetting.sDeltaval
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & scenario1.DecimalSetting.iRDeltaval
                e.Value = d.ToString(str)
            End If
        End If
    End Sub
    Private Sub grdgamma_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdgamma.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex = 1 Then
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & 0
                e.Value = d.ToString(str)

            ElseIf e.ColumnIndex > 1 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                If Val(grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                'grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = DecimalSetting.sGammaval
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & scenario1.DecimalSetting.iRGammaval
                e.Value = d.ToString(str)

            End If
        End If
    End Sub
    Private Sub grdvega_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdvega.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex = 1 Then
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & 0
                e.Value = d.ToString(str)

            ElseIf e.ColumnIndex > 1 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                If Val(grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                'grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = DecimalSetting.sVegaval
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & scenario1.DecimalSetting.iRVegaval
                e.Value = d.ToString(str)
            End If
        End If
    End Sub
    Private Sub grdtheta_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdtheta.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex = 1 Then
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & 0
                e.Value = d.ToString(str)
            ElseIf e.ColumnIndex > 1 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                If Val(grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                ' grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = DecimalSetting.sThetaval
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & scenario1.DecimalSetting.iRThetaval
                e.Value = d.ToString(str)
            End If
        End If
    End Sub
    Private Sub grdvolga_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdvolga.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex = 1 Then
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & 0
                e.Value = d.ToString(str)
            ElseIf e.ColumnIndex > 1 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                If Val(grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & scenario1.DecimalSetting.iRVolgaval
                e.Value = d.ToString(str)
            End If
        End If
    End Sub
    Private Sub grdvanna_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdvanna.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex = 1 Then
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & 0
                e.Value = d.ToString(str)
            ElseIf e.ColumnIndex > 1 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                If Val(grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & scenario1.DecimalSetting.iRVannaval
                e.Value = d.ToString(str)
            End If
        End If
    End Sub

    Private Sub grdprofit_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdprofit.CellDoubleClick, grddelta.CellDoubleClick, grdgamma.CellDoubleClick, grdvega.CellDoubleClick, grdtheta.CellDoubleClick, grdvolga.CellDoubleClick, grdvanna.CellDoubleClick
        'Dim Typ As String = CType(sender, DataGridView).Tag.ToString
        'If e.ColumnIndex > 1 And e.RowIndex > -1 Then
        '    scenario1.fill_result(0, e.RowIndex, e.ColumnIndex)
        '    Dim res As New resultfrm
        '    res.temptable = scenario1.rtable
        '    res.ShowForm(Typ)
        'End If
    End Sub

    Private Sub grdprofit_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdprofit.CellContentClick

    End Sub

    Private Sub tbcon_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbcon.SelectedIndexChanged
        If tbcon.SelectedIndex = 0 Then
            scenario1.chartName = "profit"
            Label1.Text = scenario1.ValLBLprofit
        ElseIf tbcon.SelectedIndex = 2 Then
            scenario1.chartName = "delta"
            lbldelta.Text = scenario1.ValLBLdelta
        ElseIf tbcon.SelectedIndex = 4 Then
            scenario1.chartName = "gamma"
            lblgamma.Text = scenario1.ValLBLgamma
        ElseIf tbcon.SelectedIndex = 6 Then
            scenario1.chartName = "vega"
            lblvega.Text = scenario1.ValLBLvega
        ElseIf tbcon.SelectedIndex = 8 Then
            scenario1.chartName = "theta"
            lbltheta.Text = scenario1.ValLBLtheta
        ElseIf tbcon.SelectedIndex = 10 Then
            scenario1.chartName = "volga"
            lblvolga.Text = scenario1.ValLBLvolga
        ElseIf tbcon.SelectedIndex = 12 Then
            scenario1.chartName = "vanna"
            lblvanna.Text = scenario1.ValLBLVanna
        End If
    End Sub

    Public chartDataCol As New CChartCollection
    Public Sub New()
        Try
            ' This call is required by the Windows Form Designer.
            InitializeComponent()


            ' Add any initialization after the InitializeComponent() call.
            ColorSpot = Color.FromArgb(161, 75, 10)
            ColorMidSpot = Color.FromArgb(254, 127, 0)


            chartPNL.Dock = DockStyle.Fill
            chartDelta.Dock = DockStyle.Fill
            chartGamma.Dock = DockStyle.Fill
            chartTheta.Dock = DockStyle.Fill
            chartVega.Dock = DockStyle.Fill
            chartVolga.Dock = DockStyle.Fill
            chartVanna.Dock = DockStyle.Fill

            ChartInit(chartDataCol)
            AddHandler chartPNL.DoubleClick, AddressOf chtprofit_DblClick
            AddHandler chartDelta.DoubleClick, AddressOf chtprofit_DblClick
            AddHandler chartGamma.DoubleClick, AddressOf chtprofit_DblClick
            AddHandler chartTheta.DoubleClick, AddressOf chtprofit_DblClick
            AddHandler chartVega.DoubleClick, AddressOf chtprofit_DblClick
            AddHandler chartVolga.DoubleClick, AddressOf chtprofit_DblClick
            AddHandler chartVanna.DoubleClick, AddressOf chtprofit_DblClick

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ChartInit(pChartColl As CChartCollection)
        '------------------------------------------------------------------------
        pChartColl.mChartDataProfitLoss.mTitle = "PROFIT & lOSS"
        pChartColl.mChartDataProfitLoss.mXSeriesText = "SPOT PRICE"
        pChartColl.mChartDataProfitLoss.mYSeriesText = "PROFIT & lOSS"
        pChartColl.mChartDataProfitLoss.mXSeriesToolTipText = "SPOT:"
        pChartColl.mChartDataProfitLoss.mYSeriesToolTipText = "PNL"
        pChartColl.mChartDataProfitLoss.mFormPlot = chartPNL
        pChartColl.CreatePlot(pChartColl.mChartDataProfitLoss)

        '------------------------------------------------------------------------
        pChartColl.mChartDataDelta.mTitle = "DELTA"
        pChartColl.mChartDataDelta.mXSeriesText = "SPOT PRICE"
        pChartColl.mChartDataDelta.mYSeriesText = "DELTA"
        pChartColl.mChartDataDelta.mXSeriesToolTipText = "SPOT:"
        pChartColl.mChartDataDelta.mYSeriesToolTipText = "DELTA:"
        pChartColl.mChartDataDelta.mFormPlot = chartDelta
        pChartColl.CreatePlot(pChartColl.mChartDataDelta)

        '------------------------------------------------------------------------
        pChartColl.mChartDataGama.mTitle = "GAMMA"
        pChartColl.mChartDataGama.mYSeriesText = pChartColl.mChartDataGama.mTitle
        pChartColl.mChartDataGama.mYSeriesToolTipText = pChartColl.mChartDataGama.mTitle + ":"
        pChartColl.mChartDataGama.mXSeriesText = "SPOT PRICE"
        pChartColl.mChartDataGama.mXSeriesToolTipText = "SPOT:"
        pChartColl.mChartDataGama.mFormPlot = chartGamma
        pChartColl.CreatePlot(pChartColl.mChartDataGama)

        '------------------------------------------------------------------------
        pChartColl.mChartDataTheta.mTitle = "THETA"
        pChartColl.mChartDataTheta.mYSeriesText = pChartColl.mChartDataTheta.mTitle
        pChartColl.mChartDataTheta.mYSeriesToolTipText = pChartColl.mChartDataTheta.mTitle + ":"
        pChartColl.mChartDataTheta.mXSeriesText = "SPOT PRICE"
        pChartColl.mChartDataTheta.mXSeriesToolTipText = "SPOT:"
        pChartColl.mChartDataTheta.mFormPlot = chartTheta
        pChartColl.CreatePlot(pChartColl.mChartDataTheta)

        '------------------------------------------------------------------------
        pChartColl.mChartDataVega.mTitle = "VEGA"
        pChartColl.mChartDataVega.mYSeriesText = pChartColl.mChartDataVega.mTitle
        pChartColl.mChartDataVega.mYSeriesToolTipText = pChartColl.mChartDataVega.mTitle + ":"
        pChartColl.mChartDataVega.mXSeriesText = "SPOT PRICE"
        pChartColl.mChartDataVega.mXSeriesToolTipText = "SPOT:"
        pChartColl.mChartDataVega.mFormPlot = chartVega
        pChartColl.CreatePlot(pChartColl.mChartDataVega)

        '------------------------------------------------------------------------
        pChartColl.mChartDataVolga.mTitle = "VOLGA"
        pChartColl.mChartDataVolga.mYSeriesText = pChartColl.mChartDataVolga.mTitle
        pChartColl.mChartDataVolga.mYSeriesToolTipText = pChartColl.mChartDataVolga.mTitle + ":"
        pChartColl.mChartDataVolga.mXSeriesText = "SPOT PRICE"
        pChartColl.mChartDataVolga.mXSeriesToolTipText = "SPOT:"
        pChartColl.mChartDataVolga.mFormPlot = chartVolga
        pChartColl.CreatePlot(pChartColl.mChartDataVolga)

        '------------------------------------------------------------------------
        pChartColl.mChartDataVanna.mTitle = "VANNA"
        pChartColl.mChartDataVanna.mYSeriesText = pChartColl.mChartDataVanna.mTitle
        pChartColl.mChartDataVanna.mYSeriesToolTipText = pChartColl.mChartDataVanna.mTitle + ":"
        pChartColl.mChartDataVanna.mXSeriesText = "SPOT PRICE"
        pChartColl.mChartDataVanna.mXSeriesToolTipText = "SPOT:"
        pChartColl.mChartDataVanna.mFormPlot = chartVanna
        pChartColl.CreatePlot(pChartColl.mChartDataVanna)

    End Sub

End Class