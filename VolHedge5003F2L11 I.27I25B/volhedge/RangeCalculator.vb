Public Class RangeCalculator
    Public Shared chkRangeCalc As Boolean
    Dim DtRange As New DataTable
    Dim DtData As New DataTable
    Dim objtrading As New trading

    Private Sub RangeCalculator_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        chkRangeCalc = False
    End Sub

    Private Sub RangeCalculator_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        chkRangeCalc = False
    End Sub
    Private Sub RangeCalculator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        chkRangeCalc = True

        txtInteration.Text = 30     'to set Interaction by default
        txtChange.Text = 1          'to set Change by default

        DtData.Columns.Add("Date", GetType(Date))
        DtData.Columns.Add("Change", GetType(Double))
        DtData.Columns.Add("1Sigma68Lower", GetType(Double))
        DtData.Columns.Add("1Sigma68Upper", GetType(Double))
        DtData.Columns.Add("2Sigma95Lower", GetType(Double))
        DtData.Columns.Add("2Sigma95Upper", GetType(Double))
        DtData.Columns.Add("3Sigma99Lower", GetType(Double))
        DtData.Columns.Add("3Sigma99Upper", GetType(Double))
        DtData.Columns.Add("68Change", GetType(Double))
        DtData.Columns.Add("95Change", GetType(Double))
        DtData.Columns.Add("99Change", GetType(Double))
        DtData.Columns.Add("Maturity", GetType(Double))

        DtRange = objtrading.Select_tblRangeData()
        Dim dv As DataView
        dv = New DataView(DtRange, "", "", DataViewRowState.CurrentRows)
        For Each drow As DataRow In dv.ToTable(True, "Symbol").Select
            cmbScript.Items.Add(drow("Symbol"))
        Next
        'If cmbScript.Items.Contains("Nifty") Then
        cmbScript.Text = "Nifty"
        'End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Dim dChange As Integer
        Dim dCMP, dCurVol As New Double
        Dim nextday As String
        Dim date1 As New Date
        dCurVol = ((Val(DtRange.Compute("max(CurDayVol)", "Symbol ='" & cmbScript.Text & "' AND BDate=#" & dtpDate.Value.ToString("dd/MMM/yyyy") & "# ") & "")) * 100).ToString("0.00")
        txtCurVol.Text = Math.Round(dCurVol * Math.Sqrt(365), 2)
        txtCMP.Text = Val(DtRange.Compute("max(FutClosePrice)", "Symbol ='" & cmbScript.Text & "' AND BDate=#" & dtpDate.Value.ToString("dd/MMM/yyyy") & "# ") & "")
        'txtChange.Text = Val(DtRange.Compute("max(ClosePrice)", "Symbol ='" & cmbScript.Text & "' AND Maturity='" & dtpDate.Value & "' ") & "")

        DtData.Clear()

        

        dChange = CInt(txtChange.Text)
        dCMP = CDbl(txtCMP.Text)
        'dCurVol = CDbl(txtCurVol.Text)
        nextday = DateAdd(DateInterval.Day, 1, dtpDate.Value).ToString("ddd")

        'for Date column start

        For i As Integer = 1 To CInt(txtInteration.Text)
            Dim dr As DataRow
            dr = DtData.NewRow

            If i = 1 Then
                dr("Date") = DateAdd(DateInterval.Day, 1, dtpDate.Value).ToString("dd-MMM-yy")
            ElseIf i = 2 Then
                If dChange > 0 Then
                    dr("Date") = DateAdd(DateInterval.Day, dChange, date1).ToString("dd-MMM-yy")
                Else
                    If nextday = "Sun" Then
                        dr("Date") = DateAdd(DateInterval.Day, 4, date1).ToString("dd-MMM-yy")
                    ElseIf nextday = "Mon" Then
                        dr("Date") = DateAdd(DateInterval.Day, 3, date1).ToString("dd-MMM-yy")
                    ElseIf nextday = "Tue" Then
                        dr("Date") = DateAdd(DateInterval.Day, 2, date1).ToString("dd-MMM-yy")
                    ElseIf nextday = "Wed" Then
                        dr("Date") = DateAdd(DateInterval.Day, 1, date1).ToString("dd-MMM-yy")
                    ElseIf nextday = "Thu" Then
                        dr("Date") = DateAdd(DateInterval.Day, 7, date1).ToString("dd-MMM-yy")
                    ElseIf nextday = "Fri" Then
                        dr("Date") = DateAdd(DateInterval.Day, 6, date1).ToString("dd-MMM-yy")
                    ElseIf nextday = "Sat" Then
                        dr("Date") = DateAdd(DateInterval.Day, 5, date1).ToString("dd-MMM-yy")
                    Else
                        dr("Date") = dr("Date") = DateAdd(DateInterval.Day, dChange, date1).ToString("dd-MMM-yy")
                    End If
                End If
            Else
                If dChange > 0 Then
                    dr("Date") = DateAdd(DateInterval.Day, dChange, date1).ToString("dd-MMM-yy")
                Else
                    dr("Date") = DateAdd(DateInterval.Day, 7, date1).ToString("dd-MMM-yy")
                End If
            End If
            'for Date column end
            dr("Change") = ((dCurVol) * Math.Sqrt(DateDiff(DateInterval.Day, dtpDate.Value.Date, dr("date"))))  'for change column
            dr("1Sigma68Lower") = Math.Round((dCMP - (dCMP * dr("Change") / 100)), 0)  'for Sigma 68% lower
            dr("1Sigma68Upper") = Math.Round((dCMP + (dCMP * dr("Change") / 100)), 0)  'for Sigma 68% upper
            dr("2Sigma95Lower") = Math.Round((dCMP - (dCMP * dr("Change") / 100) * 2), 0)  'for Sigma 95% lower
            dr("2Sigma95Upper") = Math.Round((dCMP + (dCMP * dr("Change") / 100) * 2), 0)  'for Sigma 95% upper
            dr("3Sigma99Lower") = Math.Round((dCMP - (dCMP * dr("Change") / 100) * 3), 0)  'for Sigma 99% lower
            dr("3Sigma99Upper") = Math.Round((dCMP + (dCMP * dr("Change") / 100) * 3), 0)  'for Sigma 99% upper
            dr("68Change") = Math.Round((dCMP * (dr("Change") / 100)), 0)  'for 0.68 Change
            dr("95Change") = Math.Round((dCMP * (dr("Change") / 100) * 2), 0)  'for 0.95 Change
            dr("99Change") = Math.Round((dCMP * (dr("Change") / 100) * 3), 0)  'for 0.99 Change

            date1 = dr("Date")
            DtData.Rows.Add(dr)
        Next

        For i As Integer = 0 To DtData.Rows.Count - 1
            Dim nextdate As Date
            If (i = DtData.Rows.Count - 1) Then
                If dChange > 0 Then
                    nextdate = DateAdd(DateInterval.Day, dChange, date1).ToString("dd-MMM-yy")
                Else
                    nextdate = DateAdd(DateInterval.Day, 7, date1).ToString("dd-MMM-yy")
                End If

                If (CDate(DtData.Rows(i)("Date")).ToString("MMM") = nextdate.ToString("MMM")) Then
                    DtData.Rows(i)("Maturity") = 0
                Else
                    DtData.Rows(i)("Maturity") = 1
                End If
            Else
                If (CDate(DtData.Rows(i)("Date")).ToString("MMM") = CDate(DtData.Rows(i + 1)("Date")).ToString("MMM")) Then
                    DtData.Rows(i)("Maturity") = 0
                Else
                    DtData.Rows(i)("Maturity") = 1
                End If
            End If
        Next
        dgvRange.DataSource = DtData
    End Sub
End Class