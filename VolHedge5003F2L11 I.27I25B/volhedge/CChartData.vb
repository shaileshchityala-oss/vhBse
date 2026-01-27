Imports ScottPlot.Plottables
Imports ScottPlot.NamedColor

Public Class CChartData
    Public mFormPlot As ScottPlot.WinForms.FormsPlot

    Public mXData() As Double
    Public mYDataLines(,) As Double

    Public mLegendList As New List(Of String)
    Public mScatterList As New List(Of ScottPlot.Plottables.Scatter)
    Public mCrosshair As ScottPlot.Plottables.Crosshair
    Public mHighlightMarker As ScottPlot.Plottables.Marker
    Public mHighlightText As ScottPlot.Plottables.Text
    Public mTitle As String

    Public mXSeriesText As String
    Public mYSeriesText As String

    Public mXSeriesToolTipText As String
    Public mYSeriesToolTipText As String

    Public Sub FormsPlot1_MouseMove(sender As Object, e As MouseEventArgs)

        Dim mousePixel As New ScottPlot.Pixel(e.Location.X, e.Location.Y)
        Dim mouseLocation As ScottPlot.Coordinates = mFormPlot.Plot.GetCoordinates(mousePixel)

        ' get the nearest point of each scatter
        Dim nearestPoints As New Dictionary(Of Integer, ScottPlot.DataPoint)()
        For i As Integer = 0 To mScatterList.Count - 1
            Dim nearestPoint As ScottPlot.DataPoint = mScatterList(i).Data.GetNearest(mouseLocation, mFormPlot.Plot.LastRender)
            nearestPoints.Add(i, nearestPoint)
        Next

        ' determine which scatter's nearest point is nearest to the mouse
        Dim pointSelected As Boolean = False
        Dim scatterIndex As Integer = -1
        Dim smallestDistance As Double = Double.MaxValue

        For i As Integer = 0 To nearestPoints.Count - 1
            If nearestPoints(i).IsReal Then
                ' calculate the distance of the point to the mouse
                Dim distance As Double = nearestPoints(i).Coordinates.Distance(mouseLocation)
                If distance < smallestDistance Then
                    scatterIndex = i
                    pointSelected = True
                    smallestDistance = distance
                End If
            End If
        Next

        ' place the crosshair, marker, and text over the selected point
        If pointSelected Then
            Dim scatter As ScottPlot.Plottables.Scatter = mScatterList(scatterIndex)
            Dim point As ScottPlot.DataPoint = nearestPoints(scatterIndex)

            mCrosshair.IsVisible = True
            mCrosshair.Position = point.Coordinates
            mCrosshair.LineColor = scatter.MarkerStyle.FillColor

            mHighlightMarker.IsVisible = True
            mHighlightMarker.Location = point.Coordinates
            mHighlightMarker.MarkerStyle.LineColor = scatter.MarkerStyle.FillColor

            mHighlightText.IsVisible = True
            mHighlightText.Location = point.Coordinates
            If mLegendList.Count > scatterIndex Then
                mHighlightText.LabelText = mLegendList(scatterIndex).ToString() & vbNewLine & mXSeriesToolTipText & point.X.ToString("0.00") & vbNewLine & mYSeriesToolTipText & point.Y.ToString("0.00")
                'mHighlightText.LabelText = mXSeriesToolTipText & point.X.ToString("0.00") & vbNewLine + mYSeriesToolTipText & point.Y.ToString("0.00")
            End If
            'MyHighlightText.LabelFontColor = scatter.MarkerStyle.FillColor

        End If


        Static lastIndex As Integer = -1
        If pointSelected AndAlso scatterIndex <> lastIndex Then
            mFormPlot.Refresh()
            lastIndex = scatterIndex
        End If
        'mFormPlot.re
        'mFormPlot.Refresh()
    End Sub


End Class

Public Class CChartCollection

    Public mChartDataProfitLoss As New CChartData
    Public mChartDataDelta As New CChartData
    Public mChartDataGama As New CChartData
    Public mChartDataVega As New CChartData
    Public mChartDataTheta As New CChartData
    Public mChartDataVolga As New CChartData
    Public mChartDataVanna As New CChartData

    Dim mPoltDarkModeLineColors As New List(Of ScottPlot.Color)

    Public Sub CreatePlot(ByRef pChartData As CChartData)
        If mPoltDarkModeLineColors.Count < 1 Then
            mPoltDarkModeLineColors.Add(ScottPlot.NamedColors.WebColors.Red)
            mPoltDarkModeLineColors.Add(ScottPlot.NamedColors.WebColors.Yellow)
            mPoltDarkModeLineColors.Add(ScottPlot.NamedColors.WebColors.Cyan)
            mPoltDarkModeLineColors.Add(ScottPlot.NamedColors.WebColors.Salmon)
            mPoltDarkModeLineColors.Add(ScottPlot.NamedColors.WebColors.DeepSkyBlue)
            mPoltDarkModeLineColors.Add(ScottPlot.NamedColors.WebColors.Magenta)
            mPoltDarkModeLineColors.Add(ScottPlot.NamedColors.WebColors.Lime)
        End If

        Dim chartPlot As ScottPlot.Plot = pChartData.mFormPlot.Plot

        '// Colors
        chartPlot.FigureBackground.Color = ScottPlot.Color.FromHex("#181818")
        chartPlot.DataBackground.Color = ScottPlot.Color.FromHex("#1f1f1f")

        '-------------------------------------------------------------------------
        chartPlot.Axes.Color(ScottPlot.Color.FromHex("#d7d7d7"))
        chartPlot.Grid.MajorLineColor = ScottPlot.Color.FromHex("#404040")
        chartPlot.Axes.AutoScale()

        '-------------------------------------------------------------------------
        chartPlot.Legend.BackgroundColor = ScottPlot.Color.FromHex("#404040")
        chartPlot.Legend.FontColor = ScottPlot.Color.FromHex("#d7d7d7")
        chartPlot.Legend.OutlineColor = ScottPlot.Color.FromHex("#d7d7d7")
        '-------------------------------------------------------------------------
        '        chartPlot.ShowLegend(ScottPlot.Edge.Right)
        '        chartPlot.Legend.IsVisible = True

        chartPlot.Legend.Alignment = ScottPlot.Alignment.LowerRight
        chartPlot.Legend.IsVisible = True
        chartPlot.Legend.Orientation = ScottPlot.Orientation.Vertical
        'pPlot.Plot.Legend. = ScottPlot.Layouts.Edge.Right        
        chartPlot.ShowLegend(ScottPlot.Edge.Right)

        '----------------------------------------------------------
        pChartData.mCrosshair = chartPlot.Add.Crosshair(0, 0)
        '----------------------------------------------------------
        Dim marker As Marker '
        marker = chartPlot.Add.Marker(0, 0)
        marker.Shape = ScottPlot.MarkerShape.OpenCircle
        marker.Size = 17
        marker.LineWidth = 2
        pChartData.mHighlightMarker = marker

        '----------------------------------------------------------
        Dim hText As Text
        hText = chartPlot.Add.Text("", 0, 0)
        hText.LabelAlignment = ScottPlot.Alignment.LowerLeft
        hText.LabelBold = True
        hText.OffsetX = 7
        hText.OffsetY = -7
        hText.LabelBackgroundColor = ScottPlot.NamedColors.WindowsVistaColors.ButtonFace
        hText.LabelBorderColor = ScottPlot.NamedColors.XkcdColors.Silver
        hText.LabelPadding = 2
        hText.LabelBorderRadius = 5
        hText.LabelFontColor = ScottPlot.NamedColors.XkcdColors.Black
        'End Sub
        pChartData.mHighlightText = hText

        chartPlot.Add.BackgroundText(pChartData.mTitle)
        chartPlot.Axes.Left.Label.Text = pChartData.mYSeriesText
        chartPlot.Axes.Bottom.Label.Text = pChartData.mXSeriesText

        AddHandler pChartData.mFormPlot.MouseMove, AddressOf pChartData.FormsPlot1_MouseMove
        chartPlot.Benchmark.IsVisible = False
        pChartData.mFormPlot.UserInputProcessor.DoubleLeftClickBenchmark(False)
    End Sub

    Function GenerateSineWave(samples As Integer, frequency As Double, amplitude As Double) As Double()
        Dim sineWave(samples - 1) As Double
        Dim sampleRate As Double = samples

        For i As Integer = 0 To samples - 1
            Dim angle As Double = (i / sampleRate) * 2 * Math.PI * frequency
            sineWave(i) = CDbl(amplitude * Math.Sin(angle))
        Next
        Return sineWave
    End Function


    Public Sub SetPlotData(ByRef pChartData As CChartData)

        Dim chartPlot As ScottPlot.Plot = pChartData.mFormPlot.Plot

        Dim dataLines(,) As Double = pChartData.mYDataLines

        If dataLines Is Nothing Then
            Return
        End If

        Dim rowCount As Integer = dataLines.GetLength(0)
        Dim colCnt As Integer = dataLines.GetLength(1)

        If rowCount <= 0 Or colCnt <= 0 Then
            Return
        End If

        Dim dataLine As Scatter
        Dim scatterStart, scatterEnd As Integer
        scatterStart = chartPlot.PlottableList.Count
        Dim lastX As Double
        Dim myCoord As ScottPlot.Coordinates
        For j As Integer = 0 To colCnt - 1
            Dim dataX(rowCount - 1) As Double
            For i As Integer = 0 To rowCount - 1
                dataX(i) = dataLines(i, j)
                lastX = dataX(i)
            Next
            dataLine = chartPlot.Add.ScatterLine(pChartData.mXData, dataX)
            If pChartData.mLegendList.Count > 0 Then
                dataLine.LegendText = pChartData.mLegendList(j)
            End If
            dataLine.Color = mPoltDarkModeLineColors(j Mod mPoltDarkModeLineColors.Count)
            dataLine.MarkerSize = 5
            dataLine.Smooth = True
            pChartData.mScatterList.Add(dataLine)
            myCoord = dataLine.Data.GetScatterPoints().Item(0)
        Next

        pChartData.mHighlightMarker.Coordinates = myCoord
        pChartData.mHighlightText.Location = myCoord
        pChartData.mFormPlot.Plot.Axes.AutoScale()
        pChartData.mFormPlot.Plot.Benchmark.IsVisible = False
        pChartData.mFormPlot.Refresh()
    End Sub

End Class