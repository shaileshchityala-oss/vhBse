Public Class FrmDateSelection
    Dim dtFromDate As Date
    Dim dtToDate As Date
    Dim dTblSelDate As New DataTable
    Dim tbl As New DataTable
    Dim dtScenario As New DataTable

    Public Function ShowForm(ByVal FDate As Date, ByVal TDate As Date, ByVal dtFromScenario As DataTable) As DataTable
        dtFromDate = FDate
        dtToDate = TDate
        dtScenario = dtFromScenario
        dTblSelDate.Columns.Add("Date", GetType(Date))


        Me.ShowDialog()
        Return dTblSelDate
    End Function

    Private Function NOofDays(ByVal Mnth As Integer, ByVal Yr As Integer) As Integer
        Dim FirstDate As Date
        FirstDate = DateSerial(Yr, Mnth, 1)
        Return DateDiff(DateInterval.Day, FirstDate, DateAdd(DateInterval.Month, 1, FirstDate))
    End Function

    Private Sub FrmDateSelection_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Call BtnSelect_Click(sender, e)
    End Sub
    Private Sub FrmDateSelection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim dt As Date = dtFromDate
        Dim Int As Integer = DateDiff(DateInterval.Month, dtFromDate, dtToDate)

        Dim i As Integer = 0
        Dim Col As DataColumn

        Col = New DataColumn("Month", GetType(Date))
        Col.AllowDBNull = True
        Col.Caption = "Month"
        Col.ColumnName = "Month"
        Col.DefaultValue = DBNull.Value
        tbl.Columns.Add(Col)

        For i = 1 To 31
            Col = New DataColumn("Column" & i.ToString, GetType(Single))
            Col.AllowDBNull = False
            Col.ColumnName = "Column" & i.ToString
            Col.DefaultValue = 2
            tbl.Columns.Add(Col)
        Next

        For i = 0 To Int
            dt = DateSerial(dt.Year, (dt.Month + Val(IIf(i = 0, 0, 1))), 1)
            tbl.Rows.Add(GetMonthRow(dt))
        Next
        '   foreach (GridViewRow row in Chartarr)
        '    {
        '        DataRow datarw;
        '        datarw = dt.NewRow();
        '        for (int i = 0; i < row.Cells.Count; i++)
        '        {
        '            datarw[i] = row.Cells[i].Text;
        '        }

        '        dt.Rows.Add(datarw);
        '}

        If dtScenario Is Nothing = True Then
            Exit Sub
        End If

        For Each drow As DataRow In dtScenario.Rows
            For Each dcol As DataColumn In dtScenario.Columns
                If CBool(IIf(IsDBNull(drow(dcol.ColumnName)), False, drow(dcol.ColumnName))) = True Then
                    For Each dr As DataRow In tbl.Rows
                        For Each dc As DataColumn In tbl.Columns
                            If dc.ColumnName <> "Month" And IsDate(dcol.ColumnName) = True Then
                                If CDate(dcol.ColumnName) = DateSerial(CDate(dr("Month")).Year, CDate(dr("Month")).Month, Replace(dc.ColumnName, "Column", "")) Then
                                    dr(dc.ColumnName) = 1
                                End If
                            End If
                        Next
                    Next
                End If
            Next
        Next

        grdact.DataSource = tbl

        If Int = 0 Then Int = 1
        Me.Height = 40 * Int + (100)
    End Sub

    Private Function GetMonthRow(ByVal dat As Date) As DataRow
        Dim drow As DataRow = tbl.NewRow()
        Dim dat1 As String = dat.ToString("dd-MMM-yy")
        Dim dat2 As Date = dat1

        drow("Month") = dat2.ToString("dd-MMM-yy")

        For j As Integer = 1 To NOofDays(dat.Month, dat.Year)
            'If UCase(WeekdayName(Weekday(DateSerial(dat.Year, dat.Month, j)))) <> "SUNDAY" And UCase(WeekdayName(Weekday(DateSerial(dat.Year, dat.Month, j)))) <> "SATURDAY" Then
            '    drow("Column" & j.ToString) = "0"
            'Else
            '    drow("Column" & j.ToString) = "0"
            'End If
            drow("Column" & j.ToString) = 0
        Next
        Return drow
    End Function

    Private Sub grdact_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdact.CellContentClick

    End Sub

    Private Sub grdact_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdact.CellFormatting
        grdact.Rows(e.RowIndex).Cells("Month").Style.Format = "MMM-yy"
        'grdact.Columns("Month").DefaultCellStyle.Format = "MMM-yy"
        Dim dat As Date = CDate(grdact.Rows(e.RowIndex).Cells("Month").Value)
        '   Write_Error_scenario("ScnarionDate=" & dat)

        'grdact.Rows(e.RowIndex).Cells("Month").Style.Format = "M"
        If e.ColumnIndex >= 1 Then
            If DateSerial(dat.Year, dat.Month, grdact.Columns(e.ColumnIndex).HeaderText).Month <> dat.Month Then
                ' Write_Error_scenario("ScnarionDate1=" & DateSerial(dat.Year, dat.Month, grdact.Columns(e.ColumnIndex).HeaderText).Month & "month=" & dat.Month)
                grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Yellow
                If grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> 0 Then
                    grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0
                    grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Black
                End If
            Else
                If Not (DateSerial(dat.Year, dat.Month, grdact.Columns(e.ColumnIndex).HeaderText) >= dtFromDate And DateSerial(dat.Year, dat.Month, grdact.Columns(e.ColumnIndex).HeaderText) <= dtToDate) Then
                    '   Write_Error_scenario("ScnarionDate2=" & dtFromDate)
                    grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                    grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Yellow
                    If grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> 0 Then
                        grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0
                        grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Black
                    End If
                Else
                    '=============
                    Dim oldDate As Date = DateSerial(dat.Year, dat.Month, grdact.Columns(e.ColumnIndex).HeaderText)

                    Dim oldWeekDayName As String
                    'oldDate = DateSerial(dat.Year, dat.Month, grdact.Columns(e.ColumnIndex + 1).HeaderText)
                    'oldWeekDayName = WeekdayName(Weekday(oldDate))
                    'Dim dt As Date = "05/08/2016"
                    oldWeekDayName = oldDate.DayOfWeek.ToString()
                    '   oldWeekDayName = DateAndTime.WeekdayName(oldDate.DayOfWeek)
                    'If UCase(WeekdayName(Weekday(DateSerial(dat.Year, dat.Month, grdact.Columns(e.ColumnIndex).HeaderText)))) <> "SUNDAY" And UCase(WeekdayName(Weekday(DateSerial(dat.Year, dat.Month, grdact.Columns(e.ColumnIndex).HeaderText)))) <> "SATURDAY" Then
                    If UCase(oldWeekDayName) <> "SUNDAY" And UCase(oldWeekDayName) <> "SATURDAY" Then
                        grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = False
                        If grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor <> Color.Yellow And grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 2 Then
                            'MsgBox(grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                            grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0
                            grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Black
                        ElseIf grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0 Then
                            grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Black
                        End If
                    Else
                        '  grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = False 'True
                        grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True  'True
                        grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Red
                    End If
                    '================
                End If

            End If


            'If grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected And grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = False Then
            '    If grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> 1 Then
            '        grdact.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 1
            '    End If
            'End If
        End If
    End Sub

    Private Sub BtnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelect.Click
        dTblSelDate.Rows.Clear()
        Dim selectedCellCount As Integer = _
        grdact.GetCellCount(DataGridViewElementStates.Selected)

        If selectedCellCount > 0 Then

            Dim sb As New System.Text.StringBuilder()
            For Each grow As DataGridViewRow In grdact.Rows
                For Each col As DataGridViewCell In grow.Cells
                    If col.Selected And col.ColumnIndex > 0 Then
                        'If WeekdayName(Weekday(DateSerial(CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Year, CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Month, grdact.Columns(col.ColumnIndex).HeaderText))).ToUpper <> "SUNDAY" And WeekdayName(Weekday(DateSerial(CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Year, CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Month, grdact.Columns(col.ColumnIndex).HeaderText))).ToUpper <> "SATURDAY" Then
                        If DateSerial(CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Year, CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Month, grdact.Columns(col.ColumnIndex).HeaderText) >= dtFromDate And DateSerial(CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Year, CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Month, grdact.Columns(col.ColumnIndex).HeaderText) <= dtToDate Then
                            Dim dr As DataRow = dTblSelDate.NewRow
                            dr("Date") = DateSerial(CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Year, CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Month, grdact.Columns(col.ColumnIndex).HeaderText)

                            col.Value = 1
                            dTblSelDate.Rows.Add(dr)
                            dTblSelDate.AcceptChanges()
                        End If
                        'End If
                    Else
                        If col.ColumnIndex > 0 And col.Style.BackColor <> Color.Yellow Then
                            If col.Value = 1 Then
                                If DateSerial(CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Year, CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Month, grdact.Columns(col.ColumnIndex).HeaderText) >= dtFromDate And DateSerial(CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Year, CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Month, grdact.Columns(col.ColumnIndex).HeaderText) <= dtToDate Then
                                    Dim dr As DataRow = dTblSelDate.NewRow
                                    dr("Date") = DateSerial(CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Year, CDate(grdact.Rows(col.RowIndex).Cells("Month").Value).Month, grdact.Columns(col.ColumnIndex).HeaderText)

                                    col.Value = 1
                                    dTblSelDate.Rows.Add(dr)
                                    dTblSelDate.AcceptChanges()
                                Else
                                    col.Value = 0
                                End If
                            End If
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub grdact_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdact.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            For Each grow As DataGridViewRow In grdact.Rows
                For Each col As DataGridViewCell In grow.Cells
                    If col.Selected And col.ColumnIndex > 0 And col.ReadOnly = False Then
                        col.Value = 0
                        If col.Style.BackColor <> Color.Red And col.Style.BackColor <> Color.Yellow Then
                            col.Style.BackColor = Color.Black
                        End If
                    End If
                Next
            Next
        Else
            For Each grow As DataGridViewRow In grdact.Rows
                For Each col As DataGridViewCell In grow.Cells
                    If col.Selected And col.ColumnIndex > 0 And col.ReadOnly = False Then
                        col.Value = 1
                        If col.Style.BackColor <> Color.Red And col.Style.BackColor <> Color.Yellow Then
                            col.Style.BackColor = Color.White
                        End If
                    End If
                Next
            Next
        End If
        
    End Sub

End Class