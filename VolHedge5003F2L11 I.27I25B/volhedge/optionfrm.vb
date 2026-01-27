
Imports MSChart20Lib
Imports OptionG
Public Class optionfrm
    Public Shared chkoptionfrm As Boolean
    'Dim mObjData As New DataAnalysis.AnalysisData
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdrefresh.Click
        'init_cpgrid()
        'init_maingrid()
    End Sub
    Private Sub init_maingrid()
        If grdmain.Rows.Count <= 0 And grdmain.Columns.Count <= 0 Then
            If CInt(txtcol.Text) > 0 Then
                grdmain.Columns.Clear()
                Dim arr As New ArrayList
                arr.Add("Spot")
                arr.Add("Sptrike")
                arr.Add("Volatility")
                arr.Add("Rate of Interast")
                arr.Add("TimeI")
                arr.Add("TimeII")
                arr.Add("t")
                arr.Add("Days")
                Dim gcol As DataGridViewTextBoxColumn
                gcol = New DataGridViewTextBoxColumn
                gcol.DefaultCellStyle.NullValue = 0
                gcol.HeaderText = "Change"
                gcol.Width = 30
                gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Regular)
                gcol.ReadOnly = False
                grdmain.Columns.Add(gcol)

                gcol = New DataGridViewTextBoxColumn
                gcol.ReadOnly = True
                gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Regular)
                gcol.HeaderText = "Events"
                gcol.Width = 75
                grdmain.Columns.Add(gcol)
                Dim i As Integer
                For i = 0 To CInt(txtcol.Text) - 1
                    gcol = New DataGridViewTextBoxColumn
                    gcol.DefaultCellStyle.NullValue = ""
                    gcol.HeaderText = i + 1
                    gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Regular)
                    gcol.ReadOnly = True
                    gcol.Width = 75
                    grdmain.Columns.Add(gcol)
                Next
                grdmain.Refresh()

                Dim grow As DataGridViewRow
                If grdmain.Columns.Count > 2 Then
                    For i = 0 To arr.Count - 1
                        grow = New DataGridViewRow
                        grdmain.Rows.Add(grow)
                        grdmain.Refresh()
                    Next
                    i = 0
                    For Each grow In grdmain.Rows
                        For Each col As DataGridViewColumn In grdmain.Columns
                            If col.Index = 0 Then
                                grow.Cells(col.Index).Value = 0
                            ElseIf col.Index = 1 Then
                                grow.Cells(col.Index).Value = arr(i)
                            Else
                                If i = 0 Then
                                    grow.Cells(col.Index).Value = val(txtspot.Text)
                                ElseIf i = 1 Then
                                    grow.Cells(col.Index).Value = val(txtstrike.Text)
                                ElseIf i = 2 Then
                                    grow.Cells(col.Index).Value = val(txtvol.Text)
                                ElseIf i = 3 Then
                                    grow.Cells(col.Index).Value = val(txtintrate.Text)
                                ElseIf i = 4 Then
                                    grow.Cells(col.Index).Value = Format(dttimeI.Value.Date, "dd-MMM")
                                ElseIf i = 5 Then
                                    grow.Cells(col.Index).Value = Format(dttimeII.Value.Date, "dd-MMM")
                                ElseIf i = 6 Then
                                    grow.Cells(col.Index).Value = Math.Round(IIf(val(grow.Cells(0).Value) = 0, val(DateDiff(DateInterval.Day, dttimeI.Value.Date, dttimeII.Value.Date)) / 365, val(txtdays.Text) / 365), 6)
                                ElseIf i = 7 Then
                                    grow.Cells(col.Index).Value = val(txtdays.Text)
                                End If
                                'cal_d1(grow.Index, col.Index)
                            End If
                        Next
                        i += 1
                    Next

                End If
            End If
        Else
            MsgBox("df")
        End If
    End Sub
    Private Sub init_cpgrid()
        Dim i As Integer
        If grdcp.Rows.Count <= 0 And grdcp.Columns.Count <= 0 Then
            Dim arr As New ArrayList
            arr.Add("div Yield")
            arr.Add("Call")
            arr.Add("Put")
            arr.Add("Call Delta")
            arr.Add("Put Delta")
            arr.Add("Call Theta")
            arr.Add("Put Theta")
            arr.Add("Gamma")
            arr.Add("Vega")
            arr.Add("Call Rho")
            arr.Add("Put Rho")
            arr.Add("d1")
            arr.Add("d2")
            arr.Add("N'(d1)")
            arr.Add("N'(d2)")

            Dim gcol As DataGridViewTextBoxColumn
            gcol = New DataGridViewTextBoxColumn
            gcol.DefaultCellStyle.NullValue = 0
            gcol.Width = 30
            gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Regular)
            gcol.ReadOnly = True
            grdcp.Columns.Add(gcol)

            gcol = New DataGridViewTextBoxColumn
            gcol.ReadOnly = True
            gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Regular)
            gcol.Width = 60
            grdcp.Columns.Add(gcol)

            For i = 0 To CInt(txtcol.Text) - 1
                gcol = New DataGridViewTextBoxColumn
                gcol.DefaultCellStyle.NullValue = ""
                gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Regular)
                gcol.ReadOnly = True
                gcol.Width = 75
                grdcp.Columns.Add(gcol)
            Next
            grdcp.Refresh()


            Dim grow As DataGridViewRow
            If grdcp.Columns.Count > 2 Then
                For i = 0 To arr.Count - 1
                    grow = New DataGridViewRow
                    grdcp.Rows.Add(grow)
                    grdcp.Refresh()
                Next
                i = 0
                For Each grow In grdcp.Rows
                    For Each col As DataGridViewColumn In grdcp.Columns
                        If col.Index = 0 Then

                        ElseIf col.Index = 1 Then
                            grow.Cells(col.Index).Value = arr(i)
                        Else
                            grow.Cells(col.Index).Value = 0
                            'If i = 0 Then
                            '    grow.Cells(col.Index).Value = val(txtspot.Text)
                            'ElseIf i = 1 Then
                            '    grow.Cells(col.Index).Value = val(txtstrike.Text)
                            'ElseIf i = 2 Then
                            '    grow.Cells(col.Index).Value = val(txtvol.Text)
                            'ElseIf i = 3 Then
                            '    grow.Cells(col.Index).Value = val(txtintrate.Text)
                            'ElseIf i = 4 Then
                            '    grow.Cells(col.Index).Value = Format(dttimeI.Value.Date, "dd-MMM")
                            'ElseIf i = 5 Then
                            '    grow.Cells(col.Index).Value = Format(dttimeII.Value.Date, "dd-MMM")
                            'ElseIf i = 6 Then
                            '    grow.Cells(col.Index).Value = val(txtt.Text)
                            'ElseIf i = 7 Then
                            '    grow.Cells(col.Index).Value = val(txtdays.Text)
                            'End If
                        End If
                    Next
                    i += 1
                Next

            End If
        Else
            If val(txtcol.Text) > grdcp.Columns.Count Then

            End If
        End If

    End Sub

    Private Sub optionfrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Maximized
        'Me.Refresh()
        Me.Icon = My.Resources.volhedge_icon
    End Sub

    Private Sub optionfrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        chkoptionfrm = False
    End Sub
    Private Sub optionfrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'init_maingrid()
        ' chkoptionfrm = True
        'Me.WindowState = FormWindowState.Maximized
        'Me.Refresh()
    End Sub
    Private Sub grdmain_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdmain.DataError

    End Sub
    Private Sub grdmain_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdmain.CellEndEdit
        If e.ColumnIndex = 0 And grdmain.Columns.Count > 2 Then
            Dim gind As Integer
            gind = e.RowIndex
            Dim j As Integer
            Select Case gind
                Case 0
                    j = 0
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        grdmain.Rows(gind).Cells(i).Value = (val(txtspot.Text) + val(j * val(grdmain.Rows(gind).Cells(0).Value)))
                        j += 1
                        cal_d1(gind, i)
                    Next
                Case 1

                    j = 0
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        grdmain.Rows(gind).Cells(i).Value = (val(txtstrike.Text) + val(j * val(grdmain.Rows(gind).Cells(0).Value)))
                        j += 1
                        cal_d1(gind, i)
                    Next
                Case 2

                    j = 0
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        grdmain.Rows(gind).Cells(i).Value = (val(txtvol.Text) + val(j * val(grdmain.Rows(gind).Cells(0).Value)))
                        j += 1
                        cal_d1(gind, i)
                    Next
                Case 3

                    j = 0
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        grdmain.Rows(gind).Cells(i).Value = (val(txtintrate.Text) + val(j * val(grdmain.Rows(gind).Cells(0).Value)))
                        j += 1
                        cal_d1(gind, i)
                    Next
                Case 4

                    j = 0
                    Dim date1 As Date
                    Dim date2 As Date
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        Dim cal As Double
                        cal = 0
                        date1 = DateAdd(DateInterval.Day, val(j * val(grdmain.Rows(gind).Cells(0).Value)), dttimeI.Value.Date)
                        date2 = DateAdd(DateInterval.Day, val(j * val(grdmain.Rows(5).Cells(0).Value)), dttimeII.Value.Date)
                        grdmain.Rows(gind).Cells(i).Value = Format(date1, "dd-MMM")
                        If CInt(grdmain.Rows(6).Cells(0).Value) = 0 Then
                            cal = Math.Round(val(DateDiff(DateInterval.Day, date1, date2)) / 365, 6)
                            If CDate(date1) = CDate(date2) Then
                                cal = 0.0001
                            End If
                            grdmain.Rows(6).Cells(i).Value = cal
                        End If
                        cal_d1(gind, i)
                        j += 1
                    Next
                Case 5
                    Dim date1 As Date
                    Dim date2 As Date
                    j = 0
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        Dim cal As Double
                        cal = 0
                        date1 = DateAdd(DateInterval.Day, val(j * val(grdmain.Rows(4).Cells(0).Value)), dttimeI.Value.Date)
                        date2 = DateAdd(DateInterval.Day, val(j * val(grdmain.Rows(gind).Cells(0).Value)), dttimeII.Value.Date)

                        grdmain.Rows(gind).Cells(i).Value = Format(date2, "dd-MMM")
                        If CInt(grdmain.Rows(6).Cells(0).Value) = 0 Then
                            cal = Math.Round(val(DateDiff(DateInterval.Day, date1, date2)) / 365, 6)
                            If CDate(date1) = CDate(date2) Then
                                cal = 0.0001
                            End If
                            grdmain.Rows(6).Cells(i).Value = cal
                        End If
                        cal_d1(gind, i)
                        j += 1
                    Next
                Case 6
                    If val(grdmain.Rows(6).Cells(0).Value) < 0 Then
                        MsgBox("change value of t cannot be less then 0.")
                        grdmain.Rows(6).Cells(0).Value = 0

                    End If
                    j = 0
                    Dim date1 As Date
                    Dim date2 As Date
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        Dim cal As Double
                        cal = 0
                        date1 = DateAdd(DateInterval.Day, val(j * val(grdmain.Rows(4).Cells(0).Value)), dttimeI.Value.Date)
                        date2 = DateAdd(DateInterval.Day, val(j * val(grdmain.Rows(5).Cells(0).Value)), dttimeII.Value.Date)

                        If CInt(grdmain.Rows(gind).Cells(0).Value) = 0 Then
                            cal = Math.Round(val(DateDiff(DateInterval.Day, date1, date2)) / 365, 6)
                            If CDate(date1) = CDate(date2) Then
                                cal = 0.0001
                            End If
                        Else
                            cal = Math.Round(val(grdmain.Rows(7).Cells(i).Value) / 365, 6)
                        End If
                        grdmain.Rows(gind).Cells(i).Value = cal
                        j += 1
                        cal_d1(gind, i)
                    Next
                Case 7
                    j = 0
                    Dim cal As Double
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        cal = 0
                        grdmain.Rows(gind).Cells(i).Value = (val(txtdays.Text) - val(j * val(grdmain.Rows(gind).Cells(0).Value)))
                        If CInt(grdmain.Rows(6).Cells(0).Value) = 1 Then
                            cal = Math.Round(val(grdmain.Rows(gind).Cells(i).Value) / 365, 6)
                            grdmain.Rows(6).Cells(i).Value = cal
                        End If
                        j += 1
                        cal_d1(gind, i)
                    Next

            End Select
        End If
        grdmain.EndEdit()
    End Sub
    Private Sub grdmain_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdmain.EditingControlShowing
        AddHandler e.Control.KeyPress, AddressOf CheckCell
    End Sub
    Private Sub CheckCell(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'Dim KeyAscii As Short = Asc(e.KeyChar)
        If grdmain.CurrentCell.ColumnIndex = 0 Then
            Dim gind As Integer
            gind = grdmain.CurrentCell.RowIndex
            Select Case gind
                Case 0
                    numonly(e)
                Case 1
                    numonly(e)
                Case 2
                    numonly(e)
                Case 3
                    numonly(e)
                Case 4
                    numonly(e)
                Case 5
                    numonly(e)
                Case 6
                    numonly(e)
                Case 7
                    numonly(e)
            End Select
        End If


    End Sub
#Region "Event"
    Private Sub txtcol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcol.KeyPress
        numonly(e)
    End Sub
    Private Sub txtspot_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtspot.KeyPress
        numonly(e)
    End Sub
    Private Sub txtstrike_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtstrike.KeyPress
        numonly(e)
    End Sub
    Private Sub txtvol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtvol.KeyPress
        numonly(e)
    End Sub
    Private Sub txtintrate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtintrate.KeyPress
        numonly(e)
    End Sub
    Private Sub txtdays_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdays.KeyPress
        numonly(e)
    End Sub
    Private Sub txtt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        numonly(e)
    End Sub
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Private Sub txtspot_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtspot.Leave
        If grdmain.Columns.Count > 2 And grdmain.Rows.Count > 0 Then
            If val(grdmain.Rows(0).Cells(2).Value) <> val(txtspot.Text) Then
                For Each gcol As DataGridViewColumn In grdmain.Columns
                    If gcol.Index > 1 Then
                        grdmain.Rows(0).Cells(gcol.Index).Value = (val(grdmain.Rows(0).Cells(0).Value) * (val(gcol.HeaderText) - 1)) + val(txtspot.Text)
                    End If
                Next
                grdmain.EndEdit()
                cal_d1(0, 0, True)
            End If
        End If
    End Sub
    Private Sub txtstrike_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtstrike.Leave
        If grdmain.Columns.Count > 2 And grdmain.Rows.Count > 0 Then
            If val(grdmain.Rows(1).Cells(2).Value) <> val(txtstrike.Text) Then
                For Each gcol As DataGridViewColumn In grdmain.Columns
                    If gcol.Index > 1 Then
                        grdmain.Rows(1).Cells(gcol.Index).Value = (val(grdmain.Rows(1).Cells(0).Value) * (val(gcol.HeaderText) - 1)) + val(txtstrike.Text)
                    End If
                Next
                grdmain.EndEdit()
                cal_d1(0, 0, True)

            End If
        End If
    End Sub
    Private Sub txtvol_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvol.Leave
        If grdmain.Columns.Count > 2 And grdmain.Rows.Count > 0 Then
            If val(grdmain.Rows(2).Cells(2).Value) <> val(txtvol.Text) Then
                For Each gcol As DataGridViewColumn In grdmain.Columns
                    If gcol.Index > 1 Then
                        grdmain.Rows(2).Cells(gcol.Index).Value = (val(grdmain.Rows(2).Cells(0).Value) * (val(gcol.HeaderText) - 1)) + val(txtvol.Text)
                    End If
                Next
                grdmain.EndEdit()
                cal_d1(0, 0, True)

            End If
        End If
    End Sub
    Private Sub txtintrate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtintrate.Leave
        If grdmain.Columns.Count > 2 And grdmain.Rows.Count > 0 Then
            If val(grdmain.Rows(3).Cells(2).Value) <> val(txtintrate.Text) Then
                For Each gcol As DataGridViewColumn In grdmain.Columns
                    If gcol.Index > 1 Then
                        grdmain.Rows(3).Cells(gcol.Index).Value = (val(grdmain.Rows(3).Cells(0).Value) * (val(gcol.HeaderText) - 1)) + val(txtintrate.Text)
                    End If
                Next
                grdmain.EndEdit()
                cal_d1(0, 0, True)

            End If
        End If
    End Sub
    Private Sub dttimeI_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dttimeI.Leave
        If grdmain.Columns.Count > 2 And grdmain.Rows.Count > 0 Then
            If (grdmain.Rows(4).Cells(2).Value) <> Format(dttimeI.Value, "dd-MMM") Then
                cal_t()
                grdmain.EndEdit()
                cal_d1(0, 0, True)
            End If

        End If
    End Sub
    Private Sub dttimeII_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dttimeII.Leave
        If grdmain.Columns.Count > 2 And grdmain.Rows.Count > 0 Then
            If (grdmain.Rows(5).Cells(2).Value) <> Format(dttimeII.Value, "dd-MMM") Then
                cal_t()
                grdmain.EndEdit()
                cal_d1(0, 0, True)
            End If
        End If
    End Sub
    Private Sub txtdays_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdays.Leave
        If grdmain.Columns.Count > 2 And grdmain.Rows.Count > 0 Then
            If val(grdmain.Rows(7).Cells(2).Value) <> val(txtdays.Text) Then
                For Each gcol As DataGridViewColumn In grdmain.Columns
                    If gcol.Index > 1 Then
                        grdmain.Rows(7).Cells(gcol.Index).Value = val(txtdays.Text) - (val(grdmain.Rows(7).Cells(0).Value) * (val(gcol.HeaderText) - 1))
                    End If
                Next
                cal_t()
                grdmain.EndEdit()
                cal_d1(0, 0, True)

            End If
        End If
    End Sub
    Private Sub cal_t()
        Dim j As Integer = 0
        Dim date1 As Date
        Dim date2 As Date
        For i As Integer = 2 To grdmain.ColumnCount - 1
            Dim cal As Double
            cal = 0
            date1 = CDate(DateAdd(DateInterval.Day, val(j * val(grdmain.Rows(4).Cells(0).Value)), dttimeI.Value.Date))
            date2 = CDate(DateAdd(DateInterval.Day, val(j * val(grdmain.Rows(5).Cells(0).Value)), dttimeII.Value.Date))
            If CInt(grdmain.Rows(6).Cells(0).Value) = 0 Then
                cal = Math.Round(val(DateDiff(DateInterval.Day, date1, date2)) / 365, 6)
                If (CDate(date1)) = CDate(date2) Then
                    cal = 0.0001
                End If
            Else
                cal = Math.Round(val(txtdays.Text) / 365, 6)
            End If
            grdmain.Rows(6).Cells(i).Value = cal
            grdmain.Rows(4).Cells(i).Value = Format(date1, "dd-MMM")
            grdmain.Rows(5).Cells(i).Value = Format(date2, "dd-MMM")
            j += 1
            'cal_d1(gind, i)
        Next
    End Sub
    Private Sub txtcol_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcol.Leave
        Dim i As Integer
        Dim j As Integer
        If txtcol.Text.Trim = "" Then
            txtcol.Text = "0"
        Else
            If val(txtcol.Text) < 0 Then
                txtcol.Text = 0
            End If
            If val(txtcol.Text) > 125 Then
                txtcol.Text = 125
            End If
            Dim gcol As DataGridViewTextBoxColumn
            If grdmain.Columns.Count > 0 And grdmain.Rows.Count > 0 Then
                If val(grdmain.Columns.Count - 2) < val(txtcol.Text) Then
                    Dim tem As Integer = val(txtcol.Text) - val(grdmain.Columns.Count - 2)
                    For i = val(grdmain.Columns.Count - 2) To (val(grdmain.Columns.Count - 2) + (CInt(tem) - 1))
                        gcol = New DataGridViewTextBoxColumn
                        gcol.DefaultCellStyle.NullValue = ""
                        gcol.HeaderText = i + 1
                        gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                        gcol.ReadOnly = True
                        gcol.Width = 75
                        grdmain.Columns.Add(gcol)

                        gcol = New DataGridViewTextBoxColumn
                        gcol.DefaultCellStyle.NullValue = ""
                        gcol.HeaderText = i + 1
                        gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                        gcol.ReadOnly = True
                        gcol.Width = 75
                        grdcp.Columns.Add(gcol)

                        grdmain.Rows(0).Cells(gcol.Index).Value = (val(grdmain.Rows(0).Cells(0).Value) * (val(gcol.HeaderText) - 1)) + val(txtspot.Text)
                        grdmain.Rows(1).Cells(gcol.Index).Value = (val(grdmain.Rows(1).Cells(0).Value) * (val(gcol.HeaderText) - 1)) + val(txtstrike.Text)
                        grdmain.Rows(2).Cells(gcol.Index).Value = (val(grdmain.Rows(2).Cells(0).Value) * (val(gcol.HeaderText) - 1)) + val(txtvol.Text)
                        grdmain.Rows(3).Cells(gcol.Index).Value = (val(grdmain.Rows(3).Cells(0).Value) * (val(gcol.HeaderText) - 1)) + val(txtintrate.Text)
                        grdmain.Rows(4).Cells(gcol.Index).Value = Format(CDate(DateAdd(DateInterval.Day, (val(grdmain.Rows(4).Cells(0).Value) * (val(gcol.HeaderText) - 1)), dttimeI.Value)), "dd-MMM")
                        grdmain.Rows(5).Cells(gcol.Index).Value = Format(CDate(DateAdd(DateInterval.Day, (val(grdmain.Rows(5).Cells(0).Value) * (val(gcol.HeaderText) - 1)), dttimeII.Value)), "dd-MMM")
                        grdmain.Rows(6).Cells(gcol.Index).Value = Math.Round(IIf(val(grdmain.Rows(6).Cells(0).Value) = 0, val(DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, (val(grdmain.Rows(4).Cells(0).Value) * (val(gcol.HeaderText) - 1)), dttimeI.Value)), CDate(DateAdd(DateInterval.Day, (val(grdmain.Rows(5).Cells(0).Value) * (val(gcol.HeaderText) - 1)), dttimeII.Value)))) / 365, val(txtdays.Text) / 365), 6)
                        grdmain.Rows(7).Cells(gcol.Index).Value = val(txtdays.Text) - (val(grdmain.Rows(7).Cells(0).Value) * (val(gcol.HeaderText) - 1))

                    Next
                    cal_d1(0, 0, True)
                    grdmain.EndEdit()

                ElseIf val(grdmain.Columns.Count - 2) > val(txtcol.Text) Then
                    Dim tem As Integer = val(grdmain.Columns.Count - 2) - val(txtcol.Text)
                    i = (CInt(tem))
                    While i > 0
                        Dim l As Integer = val(grdmain.Columns.Count - 1)
                        grdmain.Columns.RemoveAt(l)
                        grdcp.Columns.RemoveAt(l)
                        i -= 1
                    End While
                    cal_d1(0, 0, True)
                    grdmain.EndEdit()
                End If
            Else
                If CInt(txtcol.Text) > 0 Then
                    grdmain.Columns.Clear()
                    Dim arr As New ArrayList
                    arr.Add("Spot")
                    arr.Add("Strike")
                    arr.Add("Vol%")
                    arr.Add("r%")
                    arr.Add("TimeI")
                    arr.Add("TimeII")
                    arr.Add("t")
                    arr.Add("Days")

                    gcol = New DataGridViewTextBoxColumn
                    gcol.DefaultCellStyle.NullValue = 0
                    gcol.HeaderText = "Change"
                    gcol.Width = 30
                    gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                    gcol.DefaultCellStyle.BackColor = Color.Transparent
                    gcol.DefaultCellStyle.ForeColor = Color.White
                    gcol.ReadOnly = False
                    grdmain.Columns.Add(gcol)

                    gcol = New DataGridViewTextBoxColumn
                    gcol.ReadOnly = True
                    gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                    gcol.HeaderText = "Events"
                    gcol.Width = 75
                    grdmain.Columns.Add(gcol)

                    For i = 0 To CInt(txtcol.Text) - 1
                        gcol = New DataGridViewTextBoxColumn
                        gcol.DefaultCellStyle.NullValue = ""
                        gcol.HeaderText = i + 1
                        gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                        gcol.ReadOnly = True
                        gcol.Width = 75
                        grdmain.Columns.Add(gcol)
                    Next
                    Dim grow As DataGridViewRow
                    For i = 0 To arr.Count - 1
                        grow = New DataGridViewRow
                        grdmain.Rows.Add(grow)
                        grdmain.Refresh()
                        grdmain.Rows(i).Cells(0).Value = 0
                        grdmain.Rows(i).Cells(1).Value = arr(i)
                        For j = 2 To CInt(txtcol.Text) + 1
                            If i = 4 Then
                                grdmain.Rows(i).Cells(j).Value = Format(dttimeI.Value, "dd-MMM")
                            ElseIf i = 5 Then
                                grdmain.Rows(i).Cells(j).Value = Format(dttimeII.Value, "dd-MMM")
                            Else
                                grdmain.Rows(i).Cells(j).Value = 0
                            End If

                        Next
                    Next
                    grdmain.EndEdit()


                    arr = New ArrayList
                    'arr.Add("div Yield")
                    arr.Add("Call")
                    arr.Add("Put")
                    arr.Add("Call Delta")
                    arr.Add("Put Delta")
                    arr.Add("Call Theta")
                    arr.Add("Put Theta")
                    arr.Add("Gamma")
                    arr.Add("Vega")
                    arr.Add("Call Rho")
                    arr.Add("Put Rho")
                    'arr.Add("d1")
                    'arr.Add("d2")
                    'arr.Add("N'(d1)")
                    'arr.Add("N'(d2)")


                    gcol = New DataGridViewTextBoxColumn
                    gcol.DefaultCellStyle.NullValue = 0
                    gcol.Width = 30
                    gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                    gcol.ReadOnly = True
                    grdcp.Columns.Add(gcol)

                    gcol = New DataGridViewTextBoxColumn
                    gcol.ReadOnly = True
                    gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                    gcol.Width = 75
                    grdcp.Columns.Add(gcol)

                    For i = 0 To CInt(txtcol.Text) - 1
                        gcol = New DataGridViewTextBoxColumn
                        gcol.DefaultCellStyle.NullValue = ""
                        gcol.HeaderText = i + 1
                        gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                        gcol.ReadOnly = True
                        gcol.Width = 75
                        grdcp.Columns.Add(gcol)
                    Next
                    For i = 0 To arr.Count - 1
                        grow = New DataGridViewRow
                        grdcp.Rows.Add(grow)
                        grdcp.Refresh()

                        grdcp.Rows(i).Cells(1).Value = arr(i)
                        For j = 2 To CInt(txtcol.Text) + 1
                            grdcp.Rows(i).Cells(j).Value = 0
                        Next
                    Next

                End If
            End If
        End If

    End Sub
    Private Sub dttimeII_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dttimeII.Enter

    End Sub
    Private Sub dttimeI_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dttimeI.ValueChanged

    End Sub
    Private Sub grdmain_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdmain.CellFormatting
        If e.ColumnIndex > 1 And (e.RowIndex = 0 Or e.RowIndex = 1 Or e.RowIndex = 2 Or e.RowIndex = 3 Or e.RowIndex = 6 Or e.RowIndex = 7) Then
            If val(grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf val(grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
            Else
                grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
        ElseIf e.ColumnIndex = 0 Then
            If val(grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.LightSkyBlue
            Else
                grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Black
            End If
        End If
    End Sub
    Private Sub grdcp_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdcp.CellFormatting
        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
            If val(grdcp.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grdcp.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf val(grdcp.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdcp.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
            Else
                grdcp.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
        End If
    End Sub
#End Region
    Private Sub CalData(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal mT As Integer, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal grow As DataGridViewRow, ByVal qty As Double, ByVal lv As Double, ByVal Mrateofinterast As Double)

        Dim mDelta As Double
        Dim mGama As Double
        Dim mVega As Double
        Dim mThita As Double
        Dim mRah As Double
        Dim mVolatility As Double
        Dim tmpcpprice As Double = 0
        tmpcpprice = cpprice
        'Dim mIsCal As Boolean
        'Dim mIsPut As Boolean
        'Dim mIsFut As Boolean

        mDelta = 0
        mGama = 0
        mVega = 0
        mThita = 0
        mRah = 0
        Dim _mt As Double
        'IF MATURITY IS 0 THEN _MT = 0.00001 
        If mT = 0 Then
            _mt = 0.00001
        Else
            _mt = (mT) / 365

        End If
        'mVolatility = mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
        mVolatility = lv
        Try

            'If Not mIsFut Then
            mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))

            mGama = mGama + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))

            mVega = mVega + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))

            mThita = mThita + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))

            mRah = mRah + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))

            'Else
            ''mDelta = mDelta + (1 * drow("Qty"))
            'End If




            'grow.Cells(8).Value = Math.Round(mDelta, 2)
            'grow.Cells(9).Value = Math.Round(mDelta * qty, 2)
            'grow.Cells(10).Value = Math.Round(mThita, 2)
            'grow.Cells(11).Value = Math.Round(mThita * qty, 2)
            'grow.Cells(12).Value = Math.Round(mVega, 2)
            'grow.Cells(13).Value = Math.Round(mVega * qty, 2)
            'grow.Cells(14).Value = Math.Round(mGama, 5)
            'grow.Cells(15).Value = Math.Round(mGama * qty, 5)
            'grdtrad.EndEdit()
        Catch ex As Exception

        End Try

        ''MsgBox(mDelta)


    End Sub
    Private Sub grdcp_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdcp.DataError

    End Sub
    Private Sub cal_d1(ByVal gind As Integer, ByVal gcol As Integer, Optional ByVal check As Boolean = False)
        'Dim d1 As Double
        'Dim nd1 As Double
        'Dim d2 As Double
        'Dim nd2 As Double
        Dim putroh As Double
        Dim callroh As Double
        Dim vega As Double
        Dim gamma As Double
        Dim puttheta As Double
        Dim calltheta As Double
        Dim calldelta As Double
        Dim putdelta As Double
        Dim put As Double
        Dim cal As Double
        Dim d4 As Double
        Dim d5 As Double
        Dim d6 As Double
        Dim d7 As Double
        Dim d10 As Double
        'Dim r As Double
        If check = False Then

            d4 = val(grdmain.Rows(0).Cells(gcol).Value)
            d5 = val(grdmain.Rows(1).Cells(gcol).Value)
            d6 = val(grdmain.Rows(2).Cells(gcol).Value) / 100
            d7 = val(grdmain.Rows(3).Cells(gcol).Value) / 100
            d10 = val(grdmain.Rows(6).Cells(gcol).Value)
            If d4 > 0 And d5 > 0 Then
                'If d10 > 0 Then
                'd1 = Math.Round(((Math.Log(d4 / d5)) + ((d7 + (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10)), 6)
                'd2 = ((Math.Log(d4 / d5)) + ((d7 - (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10))
                'nd1 = Math.Exp((d1 ^ 2) / 2) / (Math.Sqrt(2 * Math.PI))
                'nd2 = -Math.Exp((d2 ^ 2) / 2) / (Math.Sqrt(2 * Math.PI))
                'putroh = (-d5 * d10 * Math.Exp(-d7 * d10) * (1 - NormProb(d2))) / 100
                'callroh = (d5 * d10 * Math.Exp(-d7 * d10) * NormProb(d2)) / 100
                'vega = d4 * Math.Sqrt(d10) * (Math.Exp(-((((Math.Log(d4 / d5)) + ((d7 + (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10))) ^ 2) / 2) / (Math.Sqrt(2 * Math.PI))) / 100
                'gamma = nd1 / (d4 * d6 * Math.Sqrt(d10))
                'puttheta = (-((d4 * nd1 * d6) / (2 * Math.Sqrt(d10))) + ((d7 * d5 * Math.Exp(-d7 * d10) * (1 - NormProb(d2))))) / 365
                'calltheta = (-((d4 * nd1 * d6) / (2 * Math.Sqrt(d10))) - ((d7 * d5 * Math.Exp(-d7 * d10) * NormProb(d2)))) / 365
                'calldelta = NormProb(((Math.Log(d4 / d5)) + ((d7 + (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10)))
                'putdelta = calldelta - 1
                'put = (d5 * Math.Exp(d10 * d7 * -1) * (NormProb(-((Math.Log(d4 / d5)) + ((d7 - (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10))))) - (d4 * NormProb(-((Math.Log(d4 / d5)) + ((d7 + (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10))))
                'cal = (d4 * NormProb(((Math.Log(d4 / d5)) + ((d7 + (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10)))) - (d5 * Math.Exp(d10 * d7 * -1) * (NormProb(((Math.Log(d4 / d5)) + ((d7 - (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10)))))

                calldelta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 1))
                putdelta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 1))
                calltheta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 4))
                puttheta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 4))
                gamma = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 2))
                vega = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 3))
                callroh = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 5))
                putroh = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 5))
                cal = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 0))
                put = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 0))

                grdcp.Rows(0).Cells(gcol).Value = Math.Round(cal, 2)
                grdcp.Rows(1).Cells(gcol).Value = Math.Round(put, 2)
                grdcp.Rows(2).Cells(gcol).Value = Math.Round(calldelta, roundDelta)
                grdcp.Rows(3).Cells(gcol).Value = Math.Round(putdelta, roundDelta)
                grdcp.Rows(4).Cells(gcol).Value = Math.Round(calltheta, roundTheta)
                grdcp.Rows(5).Cells(gcol).Value = Math.Round(puttheta, roundTheta)
                grdcp.Rows(6).Cells(gcol).Value = Math.Round(gamma, roundGamma)
                grdcp.Rows(7).Cells(gcol).Value = Math.Round(vega, roundVega)
                grdcp.Rows(8).Cells(gcol).Value = Math.Round(callroh, 3)
                grdcp.Rows(9).Cells(gcol).Value = Math.Round(putroh, 3)
                'Else
                '    grdcp.Rows(1).Cells(gcol).Value = Math.Round(0, 6)
                '    grdcp.Rows(2).Cells(gcol).Value = Math.Round(0, 6)
                '    grdcp.Rows(3).Cells(gcol).Value = Math.Round(0, 6)
                '    grdcp.Rows(4).Cells(gcol).Value = Math.Round(0, 6)
                '    grdcp.Rows(5).Cells(gcol).Value = Math.Round(0, 6)
                '    grdcp.Rows(6).Cells(gcol).Value = Math.Round(0, 6)
                '    grdcp.Rows(7).Cells(gcol).Value = Math.Round(0, 6)
                '    grdcp.Rows(8).Cells(gcol).Value = Math.Round(0, 6)
                '    grdcp.Rows(9).Cells(gcol).Value = Math.Round(0, 6)
                '    grdcp.Rows(10).Cells(gcol).Value = Math.Round(0, 6)
                'End If
                'grdcp.Rows(11).Cells(gcol).Value = Math.Round(d1, 6)
                'grdcp.Rows(12).Cells(gcol).Value = Math.Round(d2, 6)
                'grdcp.Rows(13).Cells(gcol).Value = Math.Round(nd1, 6)
                'grdcp.Rows(14).Cells(gcol).Value = Math.Round(nd2, 6)
            End If
        Else
            For Each col As DataGridViewColumn In grdmain.Columns
                If col.Index > 1 Then
                    d4 = val(grdmain.Rows(0).Cells(col.Index).Value)
                    d5 = val(grdmain.Rows(1).Cells(col.Index).Value)
                    d6 = val(grdmain.Rows(2).Cells(col.Index).Value) / 100
                    d7 = val(grdmain.Rows(3).Cells(col.Index).Value) / 100
                    d10 = val(grdmain.Rows(6).Cells(col.Index).Value)
                    'If d10 > 0 Then
                    'd1 = Math.Round(((Math.Log(d4 / d5)) + ((d7 + (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10)), 6)
                    'd2 = ((Math.Log(d4 / d5)) + ((d7 - (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10))
                    'nd1 = Math.Exp((d1 ^ 2) / 2) / (Math.Sqrt(2 * Math.PI))
                    'nd2 = -Math.Exp((d2 ^ 2) / 2) / (Math.Sqrt(2 * Math.PI))
                    'putroh = (-d5 * d10 * Math.Exp(-d7 * d10) * (1 - NormProb(d2))) / 100
                    'callroh = (d5 * d10 * Math.Exp(-d7 * d10) * NormProb(d2)) / 100
                    'vega = d4 * Math.Sqrt(d10) * (Math.Exp(-((((Math.Log(d4 / d5)) + ((d7 + (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10))) ^ 2) / 2) / (Math.Sqrt(2 * Math.PI))) / 100
                    'gamma = nd1 / (d4 * d6 * Math.Sqrt(d10))
                    'puttheta = (-((d4 * nd1 * d6) / (2 * Math.Sqrt(d10))) + ((d7 * d5 * Math.Exp(-d7 * d10) * (1 - NormProb(d2))))) / 365
                    'calltheta = (-((d4 * nd1 * d6) / (2 * Math.Sqrt(d10))) - ((d7 * d5 * Math.Exp(-d7 * d10) * NormProb(d2)))) / 365
                    'calldelta = NormProb(((Math.Log(d4 / d5)) + ((d7 + (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10)))
                    'putdelta = calldelta - 1
                    'put = (d5 * Math.Exp(d10 * d7 * -1) * (NormProb(-((Math.Log(d4 / d5)) + ((d7 - (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10))))) - (d4 * NormProb(-((Math.Log(d4 / d5)) + ((d7 + (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10))))
                    'cal = (d4 * NormProb(((Math.Log(d4 / d5)) + ((d7 + (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10)))) - (d5 * Math.Exp(d10 * d7 * -1) * (NormProb(((Math.Log(d4 / d5)) + ((d7 - (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10)))))
                    If d4 > 0 And d5 > 0 Then
                        calldelta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 1))
                        putdelta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 1))
                        calltheta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 4))
                        puttheta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 4))
                        gamma = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 2))
                        vega = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 3))
                        callroh = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 5))
                        putroh = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 5))
                        cal = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 0))
                        put = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 0))

                    End If
                    'grdcp.Rows(0).Cells(col.Index).Value = 0
                    grdcp.Rows(0).Cells(col.Index).Value = Math.Round(cal, 2)
                    grdcp.Rows(1).Cells(col.Index).Value = Math.Round(put, 2)
                    grdcp.Rows(2).Cells(col.Index).Value = Math.Round(calldelta, roundDelta)
                    grdcp.Rows(3).Cells(col.Index).Value = Math.Round(putdelta, roundDelta)
                    grdcp.Rows(4).Cells(col.Index).Value = Math.Round(calltheta, roundTheta)
                    grdcp.Rows(5).Cells(col.Index).Value = Math.Round(puttheta, roundTheta)
                    grdcp.Rows(6).Cells(col.Index).Value = Math.Round(gamma, roundGamma)
                    grdcp.Rows(7).Cells(col.Index).Value = Math.Round(vega, roundVega)
                    grdcp.Rows(8).Cells(col.Index).Value = Math.Round(callroh, 3)
                    grdcp.Rows(9).Cells(col.Index).Value = Math.Round(putroh, 3)
                    'Else
                    '    grdcp.Rows(1).Cells(gcol).Value = Math.Round(0, 6)
                    '    grdcp.Rows(2).Cells(gcol).Value = Math.Round(0, 6)
                    '    grdcp.Rows(3).Cells(gcol).Value = Math.Round(0, 6)
                    '    grdcp.Rows(4).Cells(gcol).Value = Math.Round(0, 6)
                    '    grdcp.Rows(5).Cells(gcol).Value = Math.Round(0, 6)
                    '    grdcp.Rows(6).Cells(gcol).Value = Math.Round(0, 6)
                    '    grdcp.Rows(7).Cells(gcol).Value = Math.Round(0, 6)
                    '    grdcp.Rows(8).Cells(gcol).Value = Math.Round(0, 6)
                    '    grdcp.Rows(9).Cells(gcol).Value = Math.Round(0, 6)
                    '    grdcp.Rows(10).Cells(gcol).Value = Math.Round(0, 6)
                    'End If
                End If
            Next
        End If
        call_premium_chart()
        put_premium_chart()
        call_delta_chart()
        put_delta_chart()
        call_theta_chart()
        put_theta_chart()
        Gamma_chart()
        vega_chart()
        call_Roh_chart()
        put_Roh_chart()
    End Sub
    'Private Sub cal_d2(ByVal gind As Integer, ByVal gcol As Integer)
    '    Dim d2 As Double
    '    Dim d4 As Double
    '    Dim d5 As Double
    '    Dim d6 As Double
    '    Dim d7 As Double
    '    Dim d10 As Double

    '    d4 = val(grdmain.Rows(0).Cells(gcol).Value)
    '    d5 = val(grdmain.Rows(1).Cells(gcol).Value)
    '    d6 = val(grdmain.Rows(2).Cells(gcol).Value)
    '    d7 = val(grdmain.Rows(3).Cells(gcol).Value)
    '    d10 = val(grdmain.Rows(6).Cells(gcol).Value)

    '    d2 = ((Math.Log(d4 / d5)) + ((d7 - (d6 * d6) / 2) * (d10))) / (d6 * Math.Sqrt(d10))
    '    grdcp.Rows(12).Cells(gcol).Value = d2


    'End Sub
    Public Function NormProb(ByVal x As Double) As Double
        ' Note: Approximation not sufficiently accurate
        ' for targeting of nuclear warheads.
        ' Please use with caution.

        Dim t As Double
        Const b1 As Double = 0.31938153
        Const b2 As Double = -0.356563782
        Const b3 As Double = 1.781477937
        Const b4 As Double = -1.821255978
        Const b5 As Double = 1.330274429
        Const p As Double = 0.2316419
        Const c As Double = 0.39894228

        If x >= 0 Then
            t = 1.0# / (1.0# + p * x)
            NormProb = (1.0# - c * Math.Exp(-x * x / 2.0#) * t * _
            (t * (t * (t * (t * b5 + b4) + b3) + b2) + b1))
        Else
            t = 1.0# / (1.0# - p * x)
            NormProb = (c * Math.Exp(-x * x / 2.0#) * t * _
            (t * (t * (t * (t * b5 + b4) + b3) + b2) + b1))
        End If
    End Function
#Region "Chart"
    Private Sub call_premium_chart()
        Dim co As Double

        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(0).Cells(col.Index).Value)) <> "0" Then
                    co += 1
                End If

            End If
        Next
        If co > 0 Then
            chtcallperm.ColumnCount = 0
            chtcallperm.ColumnLabelCount = 0
            chtcallperm.ColumnLabelIndex = 0
            chtcallperm.AutoIncrement = False

            chtcallperm.RowCount = co - 1
            chtcallperm.RowLabelCount = 1
            chtcallperm.RowLabelIndex = 1

            chtcallperm.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            
            Dim cha(co - 1, 1) As Object
            Dim r As Integer
            r = 0
            For Each col As DataGridViewColumn In grdcp.Columns
                If col.Index > 1 Then
                    If UCase(CStr(grdcp.Rows(0).Cells(col.Index).Value)) <> "0" Then
                        cha(r, 0) = grdcp.Rows(0).Cells(col.Index).Value
                        r += 1
                    End If

                End If
            Next

            chtcallperm.ChartData = cha

            r = 1
            For Each col As DataGridViewColumn In grdmain.Columns
                'For i As Integer = 2 To grdmain.Columns.Count - 1
                If col.Index > 1 Then
                    chtcallperm.Row = r
                    chtcallperm.RowLabel = r
                    r += 1
                    If r > chtcallperm.RowCount Then
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub put_premium_chart()
        Dim co As Double

        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(1).Cells(col.Index).Value)) <> "0" Then
                    co += 1
                End If

            End If
        Next
        If co > 0 Then
            chtputprem.ColumnCount = 0
            chtputprem.ColumnLabelCount = 0
            chtputprem.ColumnLabelIndex = 0
            chtputprem.AutoIncrement = False
            chtputprem.RowCount = co - 1
            chtputprem.RowLabelCount = 1
            chtputprem.RowLabelIndex = 1
            chtputprem.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

            Dim cha(co - 1, 1) As Object
            Dim r As Integer
            r = 0
            For Each col As DataGridViewColumn In grdcp.Columns
                If col.Index > 1 Then
                    If UCase(CStr(grdcp.Rows(1).Cells(col.Index).Value)) <> "0" Then
                        cha(r, 0) = grdcp.Rows(1).Cells(col.Index).Value
                        r += 1
                    End If
                End If
            Next
            chtputprem.ChartData = cha

            r = 1
            For Each col As DataGridViewColumn In grdmain.Columns

                If col.Index > 1 Then
                    chtputprem.Row = r
                    chtputprem.RowLabel = r

                    r += 1
                    If r > chtputprem.RowCount Then
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub call_delta_chart()
        Dim co As Double

        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(2).Cells(col.Index).Value)) <> "0" Then
                    co += 1
                End If

            End If
        Next
        If co > 0 Then
            chtcalldelta.ColumnCount = 0
            chtcalldelta.ColumnLabelCount = 0
            chtcalldelta.ColumnLabelIndex = 0
            chtcalldelta.AutoIncrement = False
            chtcalldelta.RowCount = co - 1
            chtcalldelta.RowLabelCount = 1
            chtcalldelta.RowLabelIndex = 1

            chtcalldelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(chtcalldelta.RowCount, 1) As Object
            Dim r As Integer
            r = 0

            For Each col As DataGridViewColumn In grdcp.Columns
                If col.Index > 1 Then
                    If UCase(CStr(grdcp.Rows(2).Cells(col.Index).Value)) <> "0" Then
                        cha(r, 0) = grdcp.Rows(2).Cells(col.Index).Value
                        r += 1
                    End If
                End If
            Next
            chtcalldelta.ChartData = cha

            r = 1
            For Each col As DataGridViewColumn In grdmain.Columns

                If col.Index > 1 Then
                    chtcalldelta.Row = r
                    chtcalldelta.RowLabel = r

                    r += 1
                    If r > chtcalldelta.RowCount Then
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub put_delta_chart()
        Dim co As Double

        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(3).Cells(col.Index).Value)) <> "0" Then
                    co += 1
                End If

            End If
        Next
        If co > 0 Then
            chtputdelta.ColumnCount = 0
            chtputdelta.ColumnLabelCount = 0
            chtputdelta.ColumnLabelIndex = 0
            chtputdelta.AutoIncrement = False
            chtputdelta.RowCount = co - 1
            chtputdelta.RowLabelCount = 1
            chtputdelta.RowLabelIndex = 1

            chtputdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(co - 1, 1) As Object
            Dim r As Integer
            r = 0
            For Each col As DataGridViewColumn In grdcp.Columns
                If col.Index > 1 Then
                    If UCase(CStr(grdcp.Rows(3).Cells(col.Index).Value)) <> "0" Then
                        cha(r, 0) = grdcp.Rows(3).Cells(col.Index).Value
                        r += 1
                    End If

                End If
            Next
            chtputdelta.ChartData = cha

            r = 1
            For Each col As DataGridViewColumn In grdmain.Columns

                If col.Index > 1 Then
                    chtputdelta.Row = r
                    chtputdelta.RowLabel = r

                    r += 1
                    If r > chtputdelta.RowCount Then
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub call_theta_chart()
        Dim co As Double

        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(4).Cells(col.Index).Value)) <> "0" Then
                    co += 1
                End If

            End If
        Next
        If co > 0 Then
            chtcalltheta.ColumnCount = 0
            chtcalltheta.ColumnLabelCount = 0
            chtcalltheta.ColumnLabelIndex = 0
            chtcalltheta.AutoIncrement = False
            chtcalltheta.RowCount = co - 1
            chtcalltheta.RowLabelCount = 1
            chtcalltheta.RowLabelIndex = 1
            chtcalltheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(co - 1, 1) As Object
            Dim r As Integer
            r = 0
            For Each col As DataGridViewColumn In grdcp.Columns
                If col.Index > 1 Then
                    If UCase(CStr(grdcp.Rows(4).Cells(col.Index).Value)) <> "0" Then
                        cha(r, 0) = grdcp.Rows(4).Cells(col.Index).Value
                        r += 1
                    End If
                End If
            Next
            chtcalltheta.ChartData = cha
            r = 1
            For Each col As DataGridViewColumn In grdmain.Columns

                If col.Index > 1 Then
                    chtcalltheta.Row = r
                    chtcalltheta.RowLabel = r

                    r += 1
                    If r > chtcalltheta.RowCount Then
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub put_theta_chart()
        Dim co As Double

        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(5).Cells(col.Index).Value)) <> "0" Then
                    co += 1
                End If

            End If
        Next
        If co > 0 Then
            chtputtheta.ColumnCount = 0
            chtputtheta.ColumnLabelCount = 0
            chtputtheta.ColumnLabelIndex = 0
            chtputtheta.AutoIncrement = False
            chtputtheta.RowCount = co - 1
            chtputtheta.RowLabelCount = 1
            chtputtheta.RowLabelIndex = 1

            chtputtheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(co - 1, 1) As Object
            Dim r As Integer
            r = 0
            For Each col As DataGridViewColumn In grdcp.Columns
                If col.Index > 1 Then
                    If UCase(CStr(grdcp.Rows(5).Cells(col.Index).Value)) <> "0" Then
                        cha(r, 0) = grdcp.Rows(5).Cells(col.Index).Value
                        r += 1
                    End If
                End If
            Next
            chtputtheta.ChartData = cha

            r = 1
            For Each col As DataGridViewColumn In grdmain.Columns

                If col.Index > 1 Then
                    chtputtheta.Row = r
                    chtputtheta.RowLabel = r

                    r += 1
                    If r > chtputtheta.RowCount Then
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub Gamma_chart()
        Dim co As Double

        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(6).Cells(col.Index).Value)) <> "0" Then
                    co += 1
                End If

            End If
        Next
        If co > 0 Then
            chtgamma.ColumnCount = 0
            chtgamma.ColumnLabelCount = 0
            chtgamma.ColumnLabelIndex = 0
            chtgamma.AutoIncrement = False
            chtgamma.RowCount = co - 1
            chtgamma.RowLabelCount = 1
            chtgamma.RowLabelIndex = 1

            chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(co - 1, 1) As Object
            Dim r As Integer
            r = 0
            For Each col As DataGridViewColumn In grdcp.Columns
                If col.Index > 1 Then
                    If UCase(CStr(grdcp.Rows(6).Cells(col.Index).Value)) <> "0" Then
                        cha(r, 0) = grdcp.Rows(6).Cells(col.Index).Value
                        r += 1
                    End If
                End If
            Next
            chtgamma.ChartData = cha

            r = 1
            For Each col As DataGridViewColumn In grdmain.Columns

                If col.Index > 1 Then
                    chtgamma.Row = r
                    chtgamma.RowLabel = r

                    r += 1
                    If r > chtgamma.RowCount Then
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub vega_chart()
        Dim co As Double

        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(7).Cells(col.Index).Value)) <> "0" Then
                    co += 1
                End If

            End If
        Next
        If co > 0 Then
            chtvega.ColumnCount = 0
            chtvega.ColumnLabelCount = 0
            chtvega.ColumnLabelIndex = 0
            chtvega.AutoIncrement = False
            chtvega.RowCount = co - 1
            chtvega.RowLabelCount = 1
            chtvega.RowLabelIndex = 1

            chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(co - 1, 1) As Object
            Dim r As Integer
            r = 0
            For Each col As DataGridViewColumn In grdcp.Columns
                If col.Index > 1 Then
                    If UCase(CStr(grdcp.Rows(7).Cells(col.Index).Value)) <> "0" Then
                        cha(r, 0) = grdcp.Rows(7).Cells(col.Index).Value
                        r += 1
                    End If
                End If
            Next
            chtvega.ChartData = cha

            r = 1
            For Each col As DataGridViewColumn In grdmain.Columns

                If col.Index > 1 Then
                    chtvega.Row = r
                    chtvega.RowLabel = r

                    r += 1
                    If r > chtvega.RowCount Then
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub call_Roh_chart()
        Dim co As Double

        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(8).Cells(col.Index).Value)) <> "0" Then
                    co += 1
                End If

            End If
        Next
        If co > 0 Then
            chtcallroh.ColumnCount = 0
            chtcallroh.ColumnLabelCount = 0
            chtcallroh.ColumnLabelIndex = 0
            chtcallroh.AutoIncrement = False
            chtcallroh.RowCount = co - 1
            chtcallroh.RowLabelCount = 1
            chtcallroh.RowLabelIndex = 1

            chtcallroh.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(co - 1, 1) As Object
            Dim r As Integer
            r = 0
            For Each col As DataGridViewColumn In grdcp.Columns
                If col.Index > 1 Then
                    If UCase(CStr(grdcp.Rows(8).Cells(col.Index).Value)) <> "0" Then
                        cha(r, 0) = grdcp.Rows(8).Cells(col.Index).Value
                        r += 1
                    End If
                End If
            Next
            chtcallroh.ChartData = cha

            r = 1
            For Each col As DataGridViewColumn In grdmain.Columns

                If col.Index > 1 Then
                    chtcallroh.Row = r
                    chtcallroh.RowLabel = r

                    r += 1
                    If r > chtcallroh.RowCount Then
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub put_Roh_chart()
        Dim co As Double

        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(9).Cells(col.Index).Value)) <> "0" Then
                    co += 1
                End If

            End If
        Next
        If co > 0 Then
            chtputroh.ColumnCount = 0
            chtputroh.ColumnLabelCount = 0
            chtputroh.ColumnLabelIndex = 0
            chtputroh.AutoIncrement = False
            chtputroh.RowCount = co - 1
            chtputroh.RowLabelCount = 1
            chtputroh.RowLabelIndex = 1

            chtputroh.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(co - 1, 1) As Object
            Dim r As Integer
            r = 0
            For Each col As DataGridViewColumn In grdcp.Columns
                If col.Index > 1 Then
                    If UCase(CStr(grdcp.Rows(9).Cells(col.Index).Value)) <> "0" Then
                        cha(r, 0) = grdcp.Rows(9).Cells(col.Index).Value
                        r += 1
                    End If
                End If
            Next
            chtputroh.ChartData = cha

            r = 1
            For Each col As DataGridViewColumn In grdmain.Columns

                If col.Index > 1 Then
                    chtputroh.Row = r
                    chtputroh.RowLabel = r

                    r += 1
                    If r > chtputroh.RowCount Then
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub
#End Region
    
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
End Class