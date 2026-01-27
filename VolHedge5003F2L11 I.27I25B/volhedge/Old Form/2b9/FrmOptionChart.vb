Public Class FrmOptionChart
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
    Public Sub ShowForm(ByVal inx As Integer)
        SelInx = inx
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

    Private Sub chtcallperm_ChartSelected(ByVal sender As System.Object, ByVal e As AxMSChart20Lib._DMSChartEvents_ChartSelectedEvent) Handles chtcallperm.ChartSelected

    End Sub

    Private Sub chtcallperm_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtcallperm.DblClick, chtputprem.DblClick, chtcalldelta.DblClick, chtputdelta.DblClick, chtgamma.DblClick, chtvega.DblClick, chtcalltheta.DblClick, chtputtheta.DblClick, chtcallroh.DblClick, chtputroh.DblClick
        Me.Close()
    End Sub
End Class