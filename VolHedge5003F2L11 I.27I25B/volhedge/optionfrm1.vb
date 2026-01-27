Imports AxSTOCKCHARTXLib
Imports AxMSChart20Lib
Imports MSChart20Lib
Imports VolHedge.OptionG
Public Class optionfrm1
    Dim IsExit As Boolean = False
    Dim FrmOpChart As New FrmOptionChart
    Public Shared ObjSelectChart As New FrmSelectChart
    Public Shared chkoptionfrm As Boolean
    'Dim Greeks As New DataAnalysis.AnalysisData
    'Dim mObjData As New OptGreeks.Calc
    ' Dim mObjData As New OptionG.Greeks
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
                gcol.Frozen = True
                grdmain.Columns.Add(gcol)

                gcol = New DataGridViewTextBoxColumn
                gcol.ReadOnly = True
                gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Regular)
                gcol.HeaderText = "Events"
                gcol.Width = 75
                gcol.Frozen = True
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
                                    grow.Cells(col.Index).Value = Val(txtspot.Text)
                                ElseIf i = 1 Then
                                    grow.Cells(col.Index).Value = Val(txtstrike.Text)
                                ElseIf i = 2 Then
                                    grow.Cells(col.Index).Value = Val(txtvol.Text)
                                ElseIf i = 3 Then
                                    grow.Cells(col.Index).Value = Val(txtintrate.Text)
                                ElseIf i = 4 Then
                                    grow.Cells(col.Index).Value = Format(dttimeI.Value.Date, "dd-MMM")
                                ElseIf i = 5 Then
                                    grow.Cells(col.Index).Value = Format(dttimeII.Value.Date, "dd-MMM")
                                ElseIf i = 6 Then
                                    grow.Cells(col.Index).Value = Math.Round(IIf(Val(grow.Cells(0).Value) = 0, Val(DateDiff(DateInterval.Day, dttimeI.Value.Date, dttimeII.Value.Date)) / 365, Val(txtdays.Text) / 365), 6)
                                ElseIf i = 7 Then
                                    grow.Cells(col.Index).Value = Val(txtdays.Text)
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
            arr.Add("Volga")
            arr.Add("Vanna")
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
            gcol.Frozen = True
            grdcp.Columns.Add(gcol)

            gcol = New DataGridViewTextBoxColumn
            gcol.ReadOnly = True
            gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Regular)
            gcol.Width = 60
            gcol.Frozen = True
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
            If Val(txtcol.Text) > grdcp.Columns.Count Then

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

    Private Sub optionfrm1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        IsExit = True
        If MsgBox("Do you want to save Settings?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            clsOptionPera.dttimeI = dttimeI.Value
            clsOptionPera.dttimeII = dttimeII.Value

            clsOptionPera.txtcol = txtcol.Text
            clsOptionPera.txtspot = txtspot.Text
            clsOptionPera.txtstrike = txtstrike.Text
            clsOptionPera.txtvol = txtvol.Text
            clsOptionPera.txtintrate = txtintrate.Text

            Try


                clsOptionPera.arr(0) = Val(grdmain.Item(0, 0).Value) '("Spot")
                clsOptionPera.arr(1) = Val(grdmain.Item(0, 1).Value) '("Strike")
                clsOptionPera.arr(2) = Val(grdmain.Item(0, 2).Value) '("Vol%")
                clsOptionPera.arr(3) = Val(grdmain.Item(0, 3).Value) '("Interest%")
                clsOptionPera.arr(4) = Val(grdmain.Item(0, 4).Value) '("TimeI")
                clsOptionPera.arr(5) = Val(grdmain.Item(0, 5).Value) '("TimeII")
                clsOptionPera.arr(6) = Val(grdmain.Item(0, 6).Value) '("t")
                clsOptionPera.arr(7) = Val(grdmain.Item(0, 7).Value) '("Days")
            Catch ex As Exception

            End Try

        End If
    End Sub
    Private Sub optionfrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If clsOptionPera.txtcol <> "" Then
            If MsgBox("Do you want to load previous setting?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                dttimeI.Value = clsOptionPera.dttimeI
                dttimeII.Value = clsOptionPera.dttimeII

                txtcol.Text = clsOptionPera.txtcol
                Call txtcol_Leave(sender, e)
                txtspot.Text = clsOptionPera.txtspot
                Call txtspot_Leave(sender, e)
                txtstrike.Text = clsOptionPera.txtstrike
                Call txtstrike_Leave(sender, e)
                txtvol.Text = clsOptionPera.txtvol
                Call txtvol_Leave(sender, e)
                txtintrate.Text = clsOptionPera.txtintrate
                Call txtintrate_Leave(sender, e)

                grdmain.Item(0, 0).Value = clsOptionPera.arr(0) '("Spot")
                grdmainCellEndEdit(0, 0)
                grdmain.Item(0, 1).Value = clsOptionPera.arr(1) '("Strike")
                grdmainCellEndEdit(0, 1)
                grdmain.Item(0, 2).Value = clsOptionPera.arr(2) '("Vol%")
                grdmainCellEndEdit(0, 2)
                grdmain.Item(0, 3).Value = clsOptionPera.arr(3) '("Interest%")
                grdmainCellEndEdit(0, 3)
                grdmain.Item(0, 4).Value = clsOptionPera.arr(4) '("TimeI")
                grdmainCellEndEdit(0, 4)
                grdmain.Item(0, 5).Value = clsOptionPera.arr(5) '("TimeII")
                grdmainCellEndEdit(0, 5)
                grdmain.Item(0, 6).Value = clsOptionPera.arr(6) '("t")
                grdmainCellEndEdit(0, 6)
                grdmain.Item(0, 7).Value = clsOptionPera.arr(7) '("Days")
                grdmainCellEndEdit(0, 7)
            Else
                Call dttimeI_ValueChanged(sender, e)
            End If
        Else
            Call dttimeI_ValueChanged(sender, e)
        End If
        'init_maingrid()
        ' chkoptionfrm = True
        'Me.WindowState = FormWindowState.Maximized
        'Me.Refresh()
        'AddHandler grdmain.Scroll, AddressOf grdcp.

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
                        grdmain.Rows(gind).Cells(i).Value = (Val(txtspot.Text) + Val(j * Val(grdmain.Rows(gind).Cells(0).Value)))
                        j += 1
                        cal_d1(gind, i)
                    Next
                Case 1

                    j = 0
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        grdmain.Rows(gind).Cells(i).Value = (Val(txtstrike.Text) + Val(j * Val(grdmain.Rows(gind).Cells(0).Value)))
                        j += 1
                        cal_d1(gind, i)
                    Next
                Case 2

                    j = 0
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        grdmain.Rows(gind).Cells(i).Value = (Val(txtvol.Text) + Val(j * Val(grdmain.Rows(gind).Cells(0).Value)))
                        j += 1
                        cal_d1(gind, i)
                    Next
                Case 3

                    j = 0
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        grdmain.Rows(gind).Cells(i).Value = (Val(txtintrate.Text) + Val(j * Val(grdmain.Rows(gind).Cells(0).Value)))
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
                        date1 = DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(gind).Cells(0).Value)), dttimeI.Value.Date)
                        date2 = DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(5).Cells(0).Value)), dttimeII.Value.Date)
                        grdmain.Rows(gind).Cells(i).Value = Format(date1, "dd-MMM")
                        If CInt(grdmain.Rows(6).Cells(0).Value) = 0 Then
                            cal = Math.Round(Val(DateDiff(DateInterval.Day, date1, date2)) / 365, 6)
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
                        date1 = DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(4).Cells(0).Value)), dttimeI.Value.Date)
                        date2 = DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(gind).Cells(0).Value)), dttimeII.Value.Date)

                        grdmain.Rows(gind).Cells(i).Value = Format(date2, "dd-MMM")
                        If CInt(grdmain.Rows(6).Cells(0).Value) = 0 Then
                            cal = Math.Round(Val(DateDiff(DateInterval.Day, date1, date2)) / 365, 6)
                            If CDate(date1) = CDate(date2) Then
                                cal = 0.0001
                            End If
                            grdmain.Rows(6).Cells(i).Value = cal
                        End If
                        cal_d1(gind, i)
                        j += 1
                    Next
                Case 6
                    If Val(grdmain.Rows(6).Cells(0).Value) < 0 Then
                        MsgBox("change value of t can not be less then 0")
                        grdmain.Rows(6).Cells(0).Value = 0

                    End If
                    j = 0
                    Dim date1 As Date
                    Dim date2 As Date
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        Dim cal As Double
                        cal = 0
                        date1 = DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(4).Cells(0).Value)), dttimeI.Value.Date)
                        date2 = DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(5).Cells(0).Value)), dttimeII.Value.Date)

                        If CInt(grdmain.Rows(gind).Cells(0).Value) = 0 Then
                            cal = Math.Round(Val(DateDiff(DateInterval.Day, date1, date2)) / 365, 6)
                            If CDate(date1) = CDate(date2) Then
                                cal = 0.0001
                            End If
                        Else
                            cal = Math.Round(Val(grdmain.Rows(7).Cells(i).Value) / 365, 6)
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
                        grdmain.Rows(gind).Cells(i).Value = (Val(txtdays.Text) - Val(j * Val(grdmain.Rows(gind).Cells(0).Value)))
                        If CInt(grdmain.Rows(6).Cells(0).Value) = 1 Then
                            cal = Math.Round(Val(grdmain.Rows(gind).Cells(i).Value) / 365, 6)
                            grdmain.Rows(6).Cells(i).Value = cal
                        End If
                        j += 1
                        cal_d1(gind, i)
                    Next

            End Select
        End If
        grdmain.EndEdit()
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
        Volga_chart()
        Vanna_chart()
    End Sub
    Private Sub grdmainCellEndEdit(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        If ColumnIndex = 0 And grdmain.Columns.Count > 2 Then
            Dim gind As Integer
            gind = RowIndex
            Dim j As Integer
            Select Case gind
                Case 0
                    j = 0
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        grdmain.Rows(gind).Cells(i).Value = (Val(txtspot.Text) + Val(j * Val(grdmain.Rows(gind).Cells(0).Value)))
                        j += 1
                        cal_d1(gind, i)
                    Next
                Case 1

                    j = 0
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        grdmain.Rows(gind).Cells(i).Value = (Val(txtstrike.Text) + Val(j * Val(grdmain.Rows(gind).Cells(0).Value)))
                        j += 1
                        cal_d1(gind, i)
                    Next
                Case 2

                    j = 0
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        grdmain.Rows(gind).Cells(i).Value = (Val(txtvol.Text) + Val(j * Val(grdmain.Rows(gind).Cells(0).Value)))
                        j += 1
                        cal_d1(gind, i)
                    Next
                Case 3

                    j = 0
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        grdmain.Rows(gind).Cells(i).Value = (Val(txtintrate.Text) + Val(j * Val(grdmain.Rows(gind).Cells(0).Value)))
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
                        date1 = DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(gind).Cells(0).Value)), dttimeI.Value.Date)
                        date2 = DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(5).Cells(0).Value)), dttimeII.Value.Date)
                        grdmain.Rows(gind).Cells(i).Value = Format(date1, "dd-MMM")
                        If CInt(grdmain.Rows(6).Cells(0).Value) = 0 Then
                            cal = Math.Round(Val(DateDiff(DateInterval.Day, date1, date2)) / 365, 6)
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
                        date1 = DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(4).Cells(0).Value)), dttimeI.Value.Date)
                        date2 = DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(gind).Cells(0).Value)), dttimeII.Value.Date)

                        grdmain.Rows(gind).Cells(i).Value = Format(date2, "dd-MMM")
                        If CInt(grdmain.Rows(6).Cells(0).Value) = 0 Then
                            cal = Math.Round(Val(DateDiff(DateInterval.Day, date1, date2)) / 365, 6)
                            If CDate(date1) = CDate(date2) Then
                                cal = 0.0001
                            End If
                            grdmain.Rows(6).Cells(i).Value = cal
                        End If
                        cal_d1(gind, i)
                        j += 1
                    Next
                Case 6
                    If Val(grdmain.Rows(6).Cells(0).Value) < 0 Then
                        MsgBox("change value of t can not be less then 0")
                        grdmain.Rows(6).Cells(0).Value = 0

                    End If
                    j = 0
                    Dim date1 As Date
                    Dim date2 As Date
                    For i As Integer = 2 To grdmain.ColumnCount - 1
                        Dim cal As Double
                        cal = 0
                        date1 = DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(4).Cells(0).Value)), dttimeI.Value.Date)
                        date2 = DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(5).Cells(0).Value)), dttimeII.Value.Date)

                        If CInt(grdmain.Rows(gind).Cells(0).Value) = 0 Then
                            cal = Math.Round(Val(DateDiff(DateInterval.Day, date1, date2)) / 365, 6)
                            If CDate(date1) = CDate(date2) Then
                                cal = 0.0001
                            End If
                        Else
                            cal = Math.Round(Val(grdmain.Rows(7).Cells(i).Value) / 365, 6)
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
                        grdmain.Rows(gind).Cells(i).Value = (Val(txtdays.Text) - Val(j * Val(grdmain.Rows(gind).Cells(0).Value)))
                        If CInt(grdmain.Rows(6).Cells(0).Value) = 1 Then
                            cal = Math.Round(Val(grdmain.Rows(gind).Cells(i).Value) / 365, 6)
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
            If Val(grdmain.Rows(0).Cells(2).Value) <> Val(txtspot.Text) Then
                For Each gcol As DataGridViewColumn In grdmain.Columns
                    If gcol.Index > 1 Then
                        grdmain.Rows(0).Cells(gcol.Index).Value = (Val(grdmain.Rows(0).Cells(0).Value) * (Val(gcol.HeaderText) - 1)) + Val(txtspot.Text)
                    End If
                Next
                grdmain.EndEdit()
                cal_d1(0, 0, True)
            End If
        End If
        RefreshChart()
    End Sub
    Private Sub txtstrike_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtstrike.Leave
        If grdmain.Columns.Count > 2 And grdmain.Rows.Count > 0 Then
            If Val(grdmain.Rows(1).Cells(2).Value) <> Val(txtstrike.Text) Then
                For Each gcol As DataGridViewColumn In grdmain.Columns
                    If gcol.Index > 1 Then
                        grdmain.Rows(1).Cells(gcol.Index).Value = (Val(grdmain.Rows(1).Cells(0).Value) * (Val(gcol.HeaderText) - 1)) + Val(txtstrike.Text)
                    End If
                Next
                grdmain.EndEdit()
                cal_d1(0, 0, True)

            End If
        End If
        RefreshChart()
    End Sub
    Private Sub txtvol_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvol.Leave
        If grdmain.Columns.Count > 2 And grdmain.Rows.Count > 0 Then
            If Val(grdmain.Rows(2).Cells(2).Value) <> Val(txtvol.Text) Then
                For Each gcol As DataGridViewColumn In grdmain.Columns
                    If gcol.Index > 1 Then
                        grdmain.Rows(2).Cells(gcol.Index).Value = (Val(grdmain.Rows(2).Cells(0).Value) * (Val(gcol.HeaderText) - 1)) + Val(txtvol.Text)
                    End If
                Next
                grdmain.EndEdit()
                cal_d1(0, 0, True)

            End If
        End If
        RefreshChart()
    End Sub
    Private Sub txtintrate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtintrate.Leave
        If grdmain.Columns.Count > 2 And grdmain.Rows.Count > 0 Then
            If Val(grdmain.Rows(3).Cells(2).Value) <> Val(txtintrate.Text) Then
                For Each gcol As DataGridViewColumn In grdmain.Columns
                    If gcol.Index > 1 Then
                        grdmain.Rows(3).Cells(gcol.Index).Value = (Val(grdmain.Rows(3).Cells(0).Value) * (Val(gcol.HeaderText) - 1)) + Val(txtintrate.Text)
                    End If
                Next
                grdmain.EndEdit()
                cal_d1(0, 0, True)

            End If
        End If
        RefreshChart()
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
            If Val(grdmain.Rows(7).Cells(2).Value) <> Val(txtdays.Text) Then
                For Each gcol As DataGridViewColumn In grdmain.Columns
                    If gcol.Index > 1 Then
                        grdmain.Rows(7).Cells(gcol.Index).Value = Val(txtdays.Text) - (Val(grdmain.Rows(7).Cells(0).Value) * (Val(gcol.HeaderText) - 1))
                    End If
                Next
                cal_t()
                grdmain.EndEdit()
                cal_d1(0, 0, True)

            End If
        End If
        RefreshChart()
    End Sub
    Private Sub cal_t()
        Dim j As Integer = 0
        Dim date1 As Date
        Dim date2 As Date
        For i As Integer = 2 To grdmain.ColumnCount - 1
            Dim cal As Double
            cal = 0
            date1 = CDate(DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(4).Cells(0).Value)), dttimeI.Value.Date))
            date2 = CDate(DateAdd(DateInterval.Day, Val(j * Val(grdmain.Rows(5).Cells(0).Value)), dttimeII.Value.Date))
            If CInt(grdmain.Rows(6).Cells(0).Value) = 0 Then
                cal = Math.Round(Val(DateDiff(DateInterval.Day, date1, date2)) / 365, 6)
                If (CDate(date1)) = CDate(date2) Then
                    cal = 0.0001
                End If
            Else
                cal = Math.Round(Val(txtdays.Text) / 365, 6)
            End If
            grdmain.Rows(6).Cells(i).Value = cal
            grdmain.Rows(4).Cells(i).Value = Format(date1, "dd-MMM")
            grdmain.Rows(5).Cells(i).Value = Format(date2, "dd-MMM")
            j += 1
            'cal_d1(gind, i)
        Next
    End Sub

    Private Sub txtcol_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles txtcol.Layout

    End Sub
    Private Sub txtcol_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcol.Leave
        If IsExit = True Then
            Exit Sub
        End If
        Dim i As Integer
        Dim j As Integer
        If txtcol.Text.Trim = "" Then
            txtcol.Text = "0"
        Else
            If Val(txtcol.Text) < 0 Then
                txtcol.Text = 0
            End If
            If Val(txtcol.Text) > 125 Then
                txtcol.Text = 125
            End If
            Dim gcol As DataGridViewTextBoxColumn
            If grdmain.Columns.Count > 0 And grdmain.Rows.Count > 0 Then
                If Val(grdmain.Columns.Count - 2) < Val(txtcol.Text) Then
                    Dim tem As Integer = Val(txtcol.Text) - Val(grdmain.Columns.Count - 2)
                    For i = Val(grdmain.Columns.Count - 2) To (Val(grdmain.Columns.Count - 2) + (CInt(tem) - 1))
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
                        gcol.DefaultCellStyle.Format = "N4"
                        gcol.ReadOnly = True
                        gcol.Width = 75
                        grdcp.Columns.Add(gcol)

                        grdmain.Rows(0).Cells(gcol.Index).Value = (Val(grdmain.Rows(0).Cells(0).Value) * (Val(gcol.HeaderText) - 1)) + Val(txtspot.Text)
                        grdmain.Rows(1).Cells(gcol.Index).Value = (Val(grdmain.Rows(1).Cells(0).Value) * (Val(gcol.HeaderText) - 1)) + Val(txtstrike.Text)
                        grdmain.Rows(2).Cells(gcol.Index).Value = (Val(grdmain.Rows(2).Cells(0).Value) * (Val(gcol.HeaderText) - 1)) + Val(txtvol.Text)
                        grdmain.Rows(3).Cells(gcol.Index).Value = (Val(grdmain.Rows(3).Cells(0).Value) * (Val(gcol.HeaderText) - 1)) + Val(txtintrate.Text)
                        grdmain.Rows(4).Cells(gcol.Index).Value = Format(CDate(DateAdd(DateInterval.Day, (Val(grdmain.Rows(4).Cells(0).Value) * (Val(gcol.HeaderText) - 1)), dttimeI.Value)), "dd-MMM")
                        grdmain.Rows(5).Cells(gcol.Index).Value = Format(CDate(DateAdd(DateInterval.Day, (Val(grdmain.Rows(5).Cells(0).Value) * (Val(gcol.HeaderText) - 1)), dttimeII.Value)), "dd-MMM")
                        grdmain.Rows(6).Cells(gcol.Index).Value = Math.Round(IIf(Val(grdmain.Rows(6).Cells(0).Value) = 0, Val(DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, (Val(grdmain.Rows(4).Cells(0).Value) * (Val(gcol.HeaderText) - 1)), dttimeI.Value)), CDate(DateAdd(DateInterval.Day, (Val(grdmain.Rows(5).Cells(0).Value) * (Val(gcol.HeaderText) - 1)), dttimeII.Value)))) / 365, Val(txtdays.Text) / 365), 6)
                        grdmain.Rows(7).Cells(gcol.Index).Value = Val(txtdays.Text) - (Val(grdmain.Rows(7).Cells(0).Value) * (Val(gcol.HeaderText) - 1))

                    Next
                    cal_d1(0, 0, True)
                    grdmain.EndEdit()

                ElseIf Val(grdmain.Columns.Count - 2) > Val(txtcol.Text) Then
                    Dim tem As Integer = Val(grdmain.Columns.Count - 2) - Val(txtcol.Text)
                    i = (CInt(tem))
                    While i > 0
                        Dim l As Integer = Val(grdmain.Columns.Count - 1)
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
                    arr.Add("Interest%")
                    arr.Add("TimeI")
                    arr.Add("TimeII")
                    arr.Add("t")
                    arr.Add("Days")

                    gcol = New DataGridViewTextBoxColumn
                    gcol.DefaultCellStyle.NullValue = 0
                    gcol.HeaderText = "Change"
                    gcol.Width = 100
                    gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                    gcol.DefaultCellStyle.BackColor = Color.Transparent
                    gcol.DefaultCellStyle.ForeColor = Color.White
                    gcol.ReadOnly = False
                    gcol.Frozen = True
                    grdmain.Columns.Add(gcol)

                    gcol = New DataGridViewTextBoxColumn
                    gcol.ReadOnly = True
                    gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                    gcol.HeaderText = "Events"
                    gcol.Width = 75
                    gcol.Frozen = True
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
                            ElseIf i = 7 Then
                                'grdmain.Rows(i).Cells(j).Value = DateDiff(DateInterval.Day, dttimeI.Value, dttimeII.Value)
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
                    arr.Add("Volga")
                    arr.Add("Vanna")
                    'arr.Add("d1")
                    'arr.Add("d2")
                    'arr.Add("N'(d1)")
                    'arr.Add("N'(d2)")


                    gcol = New DataGridViewTextBoxColumn
                    gcol.DefaultCellStyle.NullValue = 0
                    gcol.Width = 100
                    gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                    gcol.ReadOnly = True
                    gcol.Frozen = True
                    grdcp.Columns.Add(gcol)

                    gcol = New DataGridViewTextBoxColumn
                    gcol.ReadOnly = True
                    gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                    gcol.Width = 75
                    gcol.Frozen = True
                    grdcp.Columns.Add(gcol)

                    For i = 0 To CInt(txtcol.Text) - 1
                        gcol = New DataGridViewTextBoxColumn
                        gcol.DefaultCellStyle.NullValue = ""
                        gcol.HeaderText = i + 1
                        gcol.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                        gcol.DefaultCellStyle.Format = "N4"
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

        RefreshChart()

    End Sub
    Private Sub RefreshChart()

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
        Volga_chart()
        Vanna_chart()

        ReFreshSChart()
        'PlotAllStockChart(StockChart1)
        'PlotAllStockChart(FrmOpChart.StockChart1)
    End Sub
    Private Sub dttimeII_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dttimeII.Enter

    End Sub
    Private Sub dttimeI_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dttimeI.ValueChanged
        dttimeII.Value = DateAdd(DateInterval.Day, 30, dttimeI.Value)
    End Sub
    Private Sub grdmain_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdmain.CellFormatting
        If e.ColumnIndex > 1 And (e.RowIndex = 0 Or e.RowIndex = 1 Or e.RowIndex = 2 Or e.RowIndex = 3 Or e.RowIndex = 6 Or e.RowIndex = 7) Then
            If Math.Abs(Val(grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) < Math.Abs(Val(grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value)) Then
                grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf Math.Abs(Val(grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) > Math.Abs(Val(grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value)) Then
                grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
            Else
                grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
        ElseIf e.ColumnIndex = 0 Then
            If Val(grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.SkyBlue
            ElseIf Val(grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.OrangeRed
            Else
                grdmain.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Black
            End If
        End If

        If e.RowIndex <> 4 And e.RowIndex <> 5 And e.ColumnIndex > 1 Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'Select Case grdcp.Item(1, e.RowIndex).Value.ToString.ToUpper()
            '    Case "CALL" 'Call
            '        e.CellStyle.Format = "N2"
            '    Case "PUT" 'Put
            '        e.CellStyle.Format = "N2"
            '    Case "CALL DELTA" 'Put
            '        e.CellStyle.Format = "N4"
            '    Case "PUT DELTA" 'Put
            '        e.CellStyle.Format = "N4"
            '    Case "CALL THETA" 'Put
            '        e.CellStyle.Format = "N4"
            '    Case "PUT THETA" 'Put
            '        e.CellStyle.Format = "N4"
            '    Case "GAMMA" 'Put
            '        e.CellStyle.Format = "N4"
            '    Case "VEGA" 'Put
            '        e.CellStyle.Format = "N4"
            '    Case "CALL RHO" 'Put
            '        e.CellStyle.Format = "N4"
            '    Case "PUT RHO" 'Put
            '        e.CellStyle.Format = "N4"
            'End Select
        End If

    End Sub
    Private Sub grdcp_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdcp.CellFormatting
        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
            If Math.Abs(Val(grdcp.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) < Math.Abs(Val(grdcp.Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value)) Then
                grdcp.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf Math.Abs(Val(grdcp.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)) > Math.Abs(Val(grdcp.Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value)) Then
                grdcp.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
            Else
                grdcp.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
        End If
        If Not grdcp.Item(1, e.RowIndex).Value Is Nothing Then
            If e.RowIndex >= 0 And e.ColumnIndex > 1 Then
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Select Case grdcp.Item(1, e.RowIndex).Value.ToString.ToUpper()
                    Case "CALL" 'Call
                        e.CellStyle.Format = "N2"
                    Case "PUT" 'Put
                        e.CellStyle.Format = "N2"
                    Case "CALL DELTA" 'Put
                        e.CellStyle.Format = "N4"
                    Case "PUT DELTA" 'Put
                        e.CellStyle.Format = "N4"
                    Case "CALL THETA" 'Put
                        e.CellStyle.Format = "N4"
                    Case "PUT THETA" 'Put
                        e.CellStyle.Format = "N4"
                    Case "GAMMA" 'Put
                        e.CellStyle.Format = "N4"
                    Case "VEGA" 'Put
                        e.CellStyle.Format = "N4"
                    Case "CALL RHO" 'Put
                        e.CellStyle.Format = "N4"
                    Case "PUT RHO" 'Put
                        e.CellStyle.Format = "N4"
                    Case "Volga" 'Put
                        e.CellStyle.Format = "N4"
                    Case "Vanna" 'Put
                        e.CellStyle.Format = "N4"
                End Select
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
        'mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
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

    Private Sub cal_d1(ByVal gind As Integer, ByVal gcol As Integer, Optional ByVal check As Boolean = False)

        'call options will always have positive rho... so make it absolute & put rho negative is fine


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
        Dim Volga As Double
        Dim Vanna As Double
        'Dim r As Double
        If check = False Then

            d4 = Val(grdmain.Rows(0).Cells(gcol).Value)
            d5 = Val(grdmain.Rows(1).Cells(gcol).Value)
            d6 = Val(grdmain.Rows(2).Cells(gcol).Value) / 100
            d7 = Val(grdmain.Rows(3).Cells(gcol).Value) / 100
            d10 = Val(grdmain.Rows(6).Cells(gcol).Value)
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
                Try


                    calldelta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 1))
                    putdelta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 1))
                    calltheta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 4))
                    puttheta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 4))
                    gamma = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 2))
                    vega = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 3))
                    callroh = Math.Abs(Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 5))
                    putroh = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 5))
                    cal = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 0))
                    put = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 0))
                    Volga = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 8))
                    Vanna = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 7))


                Catch ex As Exception

                End Try
                'grdcp.Rows(0).Cells(gcol).Value = Math.Round(cal, 2)
                'grdcp.Rows(1).Cells(gcol).Value = Math.Round(put, 2)
                'grdcp.Rows(2).Cells(gcol).Value = Math.Round(calldelta, roundDelta)
                'grdcp.Rows(3).Cells(gcol).Value = Math.Round(putdelta, roundDelta)
                'grdcp.Rows(4).Cells(gcol).Value = Math.Round(calltheta, roundTheta)
                'grdcp.Rows(5).Cells(gcol).Value = Math.Round(puttheta, roundTheta)
                'grdcp.Rows(6).Cells(gcol).Value = Math.Round(gamma, roundGamma)
                'grdcp.Rows(7).Cells(gcol).Value = Math.Round(vega, roundVega)
                'grdcp.Rows(8).Cells(gcol).Value = Math.Round(callroh, 3)
                'grdcp.Rows(9).Cells(gcol).Value = Math.Round(putroh, 3)
                grdcp.Rows(0).Cells(gcol).Value = Math.Round(cal, 2)
                grdcp.Rows(1).Cells(gcol).Value = Math.Round(put, 2)
                grdcp.Rows(2).Cells(gcol).Value = calldelta
                grdcp.Rows(3).Cells(gcol).Value = putdelta
                grdcp.Rows(4).Cells(gcol).Value = calltheta
                grdcp.Rows(5).Cells(gcol).Value = puttheta
                grdcp.Rows(6).Cells(gcol).Value = gamma
                grdcp.Rows(7).Cells(gcol).Value = vega
                grdcp.Rows(8).Cells(gcol).Value = callroh
                grdcp.Rows(9).Cells(gcol).Value = putroh
                grdcp.Rows(10).Cells(gcol).Value = Volga
                grdcp.Rows(11).Cells(gcol).Value = Vanna
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
                    d4 = Val(grdmain.Rows(0).Cells(col.Index).Value)
                    d5 = Val(grdmain.Rows(1).Cells(col.Index).Value)
                    d6 = Val(grdmain.Rows(2).Cells(col.Index).Value) / 100
                    d7 = Val(grdmain.Rows(3).Cells(col.Index).Value) / 100
                    d10 = Val(grdmain.Rows(6).Cells(col.Index).Value)
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
                        Try
                            calldelta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 1))
                            putdelta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 1))
                            calltheta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 4))
                            puttheta = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 4))
                            gamma = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 2))
                            vega = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 3))
                            callroh = Math.Abs(Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 5))
                            putroh = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 5))
                            cal = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 0))
                            put = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, False, True, 0, 0))

                            Volga = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 8))
                            Vanna = (Greeks.Black_Scholes(d4, d5, d7, 0, d6, d10, True, True, 0, 7))
                        Catch ex As Exception

                        End Try
                    End If
                    'grdcp.Rows(0).Cells(col.Index).Value = 0
                    'grdcp.Rows(0).Cells(col.Index).Value = Math.Round(cal, 2)
                    'grdcp.Rows(1).Cells(col.Index).Value = Math.Round(put, 2)
                    'grdcp.Rows(2).Cells(col.Index).Value = Math.Round(calldelta, roundDelta)
                    'grdcp.Rows(3).Cells(col.Index).Value = Math.Round(putdelta, roundDelta)
                    'grdcp.Rows(4).Cells(col.Index).Value = Math.Round(calltheta, roundTheta)
                    'grdcp.Rows(5).Cells(col.Index).Value = Math.Round(puttheta, roundTheta)
                    'grdcp.Rows(6).Cells(col.Index).Value = Math.Round(gamma, roundGamma)
                    'grdcp.Rows(7).Cells(col.Index).Value = Math.Round(vega, roundVega)
                    'grdcp.Rows(8).Cells(col.Index).Value = Math.Round(callroh, 3)
                    'grdcp.Rows(9).Cells(col.Index).Value = Math.Round(putroh, 3)
                    grdcp.Rows(0).Cells(col.Index).Value = Math.Round(cal, 2)
                    grdcp.Rows(1).Cells(col.Index).Value = Math.Round(put, 2)
                    grdcp.Rows(2).Cells(col.Index).Value = calldelta
                    grdcp.Rows(3).Cells(col.Index).Value = putdelta
                    grdcp.Rows(4).Cells(col.Index).Value = calltheta
                    grdcp.Rows(5).Cells(col.Index).Value = puttheta
                    grdcp.Rows(6).Cells(col.Index).Value = gamma
                    grdcp.Rows(7).Cells(col.Index).Value = vega
                    grdcp.Rows(8).Cells(col.Index).Value = callroh
                    grdcp.Rows(9).Cells(col.Index).Value = putroh
                    grdcp.Rows(10).Cells(col.Index).Value = Volga
                    grdcp.Rows(11).Cells(col.Index).Value = Vanna
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
    'Public Function NormProb(ByVal x As Double) As Double
    '    ' Note: Approximation not sufficiently accurate
    '    ' for targeting of nuclear warheads.
    '    ' Please use with caution.

    '    Dim t As Double
    '    Const b1 As Double = 0.31938153
    '    Const b2 As Double = -0.356563782
    '    Const b3 As Double = 1.781477937
    '    Const b4 As Double = -1.821255978
    '    Const b5 As Double = 1.330274429
    '    Const p As Double = 0.2316419
    '    Const c As Double = 0.39894228

    '    If x >= 0 Then
    '        t = 1.0# / (1.0# + p * x)
    '        NormProb = (1.0# - c * Math.Exp(-x * x / 2.0#) * t * _
    '        (t * (t * (t * (t * b5 + b4) + b3) + b2) + b1))
    '    Else
    '        t = 1.0# / (1.0# - p * x)
    '        NormProb = (c * Math.Exp(-x * x / 2.0#) * t * _
    '        (t * (t * (t * (t * b5 + b4) + b3) + b2) + b1))
    '    End If
    'End Function
#Region "Chart"


    Private Sub PlotAllStockChart(ByVal StockChart As AxSTOCKCHARTXLib.AxStockChartX)
        Dim Panel As Short
        Dim chrtname As String
        Dim RowInx As Integer

        StockChart.RemoveAllSeries()

        StockChart.ThreeDStyle = True REM To show chart in 3D
        StockChart.RealTimeXLabels = False
        StockChart.DisplayTitles = True REM To Set whether Chart Series display or Not
        StockChart.ShowRecordsForXLabels = True

        '===========================================
        chrtname = "CallPremium"
        RowInx = 0
        Panel = StockChart.AddChartPanel() REM to Get New Panel Index
        StockChart.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)) REM Set Series Line Color
        StockChart.set_SeriesWeight(chrtname, 1) REM Set Series Weight

        Dim VarChrtData As Double = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart.AppendValue(chrtname, StockChart.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next

        '===========================================
        chrtname = "PutPremium"
        RowInx = 1
        Panel = StockChart.AddChartPanel() REM to Get New Panel Index
        StockChart.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gold)) REM Set Series Line Color
        StockChart.set_SeriesWeight(chrtname, 1) REM Set Series Weight

        VarChrtData = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart.AppendValue(chrtname, StockChart.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next


        '===========================================
        chrtname = "CallDelta"
        RowInx = 2
        Panel = StockChart.AddChartPanel() REM to Get New Panel Index
        StockChart.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green)) REM Set Series Line Color
        StockChart.set_SeriesWeight(chrtname, 1) REM Set Series Weight

        VarChrtData = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart.AppendValue(chrtname, StockChart.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next


        '===========================================
        chrtname = "PutDelta"
        RowInx = 3
        Panel = StockChart.AddChartPanel() REM to Get New Panel Index
        StockChart.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)) REM Set Series Line Color
        StockChart.set_SeriesWeight(chrtname, 1) REM Set Series Weight

        VarChrtData = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart.AppendValue(chrtname, StockChart.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next


        '===========================================
        chrtname = "CallTheta"
        RowInx = 4
        Panel = StockChart.AddChartPanel() REM to Get New Panel Index
        StockChart.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Purple)) REM Set Series Line Color
        StockChart.set_SeriesWeight(chrtname, 1) REM Set Series Weight

        VarChrtData = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart.AppendValue(chrtname, StockChart.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next


        '===========================================
        chrtname = "PutTheta"
        RowInx = 5
        Panel = StockChart.AddChartPanel() REM to Get New Panel Index
        StockChart.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Pink)) REM Set Series Line Color
        StockChart.set_SeriesWeight(chrtname, 1) REM Set Series Weight

        VarChrtData = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart.AppendValue(chrtname, StockChart.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next


        '===========================================
        chrtname = "Gamma"
        RowInx = 6
        Panel = StockChart.AddChartPanel() REM to Get New Panel Index
        StockChart.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SkyBlue)) REM Set Series Line Color
        StockChart.set_SeriesWeight(chrtname, 1) REM Set Series Weight

        VarChrtData = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart.AppendValue(chrtname, StockChart.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next


        '===========================================
        chrtname = "Vega"
        RowInx = 7
        Panel = StockChart.AddChartPanel() REM to Get New Panel Index
        StockChart.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.YellowGreen)) REM Set Series Line Color
        StockChart.set_SeriesWeight(chrtname, 1) REM Set Series Weight

        VarChrtData = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart.AppendValue(chrtname, StockChart.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next


        '===========================================
        chrtname = "CallRoh"
        RowInx = 8
        Panel = StockChart.AddChartPanel() REM to Get New Panel Index
        StockChart.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Indigo)) REM Set Series Line Color
        StockChart.set_SeriesWeight(chrtname, 1) REM Set Series Weight

        VarChrtData = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart.AppendValue(chrtname, StockChart.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next


        '===========================================
        chrtname = "PutRoh"
        RowInx = 9
        Panel = StockChart.AddChartPanel() REM to Get New Panel Index
        StockChart.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Tan)) REM Set Series Line Color
        StockChart.set_SeriesWeight(chrtname, 1) REM Set Series Weight

        VarChrtData = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart.AppendValue(chrtname, StockChart.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next


        '===========================================
        chrtname = "Volga"
        RowInx = 10
        Panel = StockChart.AddChartPanel() REM to Get New Panel Index
        StockChart.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SpringGreen)) REM Set Series Line Color
        StockChart.set_SeriesWeight(chrtname, 1) REM Set Series Weight

        VarChrtData = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart.AppendValue(chrtname, StockChart.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next


        '===========================================
        '===========================================
        chrtname = "Vanna"
        RowInx = 11
        Panel = StockChart.AddChartPanel() REM to Get New Panel Index
        StockChart.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.YellowGreen)) REM Set Series Line Color
        StockChart.set_SeriesWeight(chrtname, 1) REM Set Series Weight

        VarChrtData = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    'StockChart.AppendValue(chrtname, col.Index, VarChrtData)
                    StockChart.AppendValue(chrtname, StockChart.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next


        '===========================================

        Dim TotalPanelHeight As Double = StockChart.Height - 20
        For i As Integer = 1 To 10 'ArrSeriesCol.Count
            StockChart.set_PanelY1(i, (TotalPanelHeight / 10) * i)
        Next

        Try
            StockChart.Update()
        Catch ex As Exception

        End Try


    End Sub

    Private Sub PlotStockChart(ByVal StockChart1 As AxSTOCKCHARTXLib.AxStockChartX, ByVal chrtname As String, ByVal RowInx As Integer)
        Dim Panel As Short
        Try
            If StockChart1.SeriesCount > 0 Then
                StockChart1.RemoveAllSeries()
            End If
        Catch ex As Exception
            Exit Sub
        End Try


        Panel = StockChart1.AddChartPanel() REM to Get New Panel Index
        StockChart1.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart1.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)) REM Set Series Line Color
        StockChart1.set_SeriesWeight(chrtname, 1) REM Set Series Weight
        'For i As Integer = 1 To ArrSeriesCol.Count
        'StockChart1.set_PanelY1(i, (TotalPanelHeight / ArrSeriesCol.Count) * i)
        'Next
        StockChart1.ThreeDStyle = True REM To show chart in 3D
        StockChart1.RealTimeXLabels = False
        StockChart1.DisplayTitles = True REM To Set whether Chart Series display or Not
        StockChart1.ShowRecordsForXLabels = True
        'If ArrSeriesCol.Contains("DELTAVAL") = True Then
        'VarLastDeltaval = Val(DtTmpGraphData.Compute("SUM(deltaval)", VarCompanyCondition).ToString)
        'StockChart1.AppendValue("deltaval", VarJData, VarLastDeltaval)
        'End If

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
        StockChart1.Update()

    End Sub
    Private Sub call_premium_chart()
        'Dim co As Double

        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(0).Cells(col.Index).Value)) <> "0" Then
        '            co += 1
        '        End If

        '    End If
        'Next
        'If co > 0 Then

        'FrmOpChart.chtcallperm.ColumnCount = 0
        'FrmOpChart.chtcallperm.ColumnLabelCount = 0
        'FrmOpChart.chtcallperm.ColumnLabelIndex = 0
        'FrmOpChart.chtcallperm.AutoIncrement = False
        'FrmOpChart.chtcallperm.RowCount = co - 1
        'FrmOpChart.chtcallperm.RowLabelCount = 1
        'FrmOpChart.chtcallperm.RowLabelIndex = 1
        'FrmOpChart.chtcallperm.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone


        'Dim cha(co - 1, 1) As Object
        'Dim r As Integer
        'r = 0
        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(0).Cells(col.Index).Value)) <> "0" Then
        '            cha(r, 0) = Math.Round(grdcp.Rows(0).Cells(col.Index).Value, 5)
        '            r += 1
        '        End If
        '    End If
        'Next

        'FrmOpChart.chtcallperm.ChartData = cha
        PlotStockChart(FrmOpChart.Schtcallperm, "CallPremium", 0)
        PlotStockChart(Schtcallperm, "CallPremium", 0)




        'With chtcallperm.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).ValueScale
        '    .Minimum = GetMinVal(0)
        '    .Maximum = GetMaxVal(0)
        'End With

        'r = 1
        'For Each col As DataGridViewColumn In grdmain.Columns
        '    'For i As Integer = 2 To grdmain.Columns.Count - 1
        '    If col.Index > 1 Then
        '        FrmOpChart.chtcallperm.Row = r
        '        FrmOpChart.chtcallperm.RowLabel = r


        '        r += 1
        '        If r > FrmOpChart.chtcallperm.RowCount Then
        '            Exit For
        '        End If
        '    End If
        'Next
        'End If
    End Sub
    Private Sub put_premium_chart()
        'Dim co As Double

        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(1).Cells(col.Index).Value)) <> "0" Then
        '            co += 1
        '        End If

        '    End If
        'Next
        'If co > 0 Then
        '    FrmOpChart.chtputprem.ColumnCount = 0
        '    FrmOpChart.chtputprem.ColumnLabelCount = 0
        '    FrmOpChart.chtputprem.ColumnLabelIndex = 0
        '    FrmOpChart.chtputprem.AutoIncrement = False
        '    FrmOpChart.chtputprem.RowCount = co - 1
        '    FrmOpChart.chtputprem.RowLabelCount = 1
        '    FrmOpChart.chtputprem.RowLabelIndex = 1
        '    FrmOpChart.chtputprem.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone


        '    Dim cha(co - 1, 1) As Object
        '    Dim r As Integer
        '    r = 0
        '    For Each col As DataGridViewColumn In grdcp.Columns
        '        If col.Index > 1 Then
        '            If UCase(CStr(grdcp.Rows(1).Cells(col.Index).Value)) <> "0" Then
        '                cha(r, 0) = Math.Round(grdcp.Rows(1).Cells(col.Index).Value, 5)
        '                r += 1
        '            End If
        '        End If
        '    Next
        'FrmOpChart.chtputprem.ChartData = cha
        PlotStockChart(FrmOpChart.Schtputprem, "PutPremium", 1)
        PlotStockChart(Schtputprem, "PutPremium", 1)


        'With chtputprem.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).ValueScale
        '    .Minimum = GetMinVal(1)
        '    .Maximum = GetMaxVal(1)
        'End With

        'r = 1
        'For Each col As DataGridViewColumn In grdmain.Columns

        '    If col.Index > 1 Then
        '        FrmOpChart.chtputprem.Row = r
        '        FrmOpChart.chtputprem.RowLabel = r



        '        r += 1
        '        If r > FrmOpChart.chtputprem.RowCount Then
        '            Exit For
        '        End If
        '    End If
        'Next
        'End If
    End Sub
    Private Sub call_delta_chart()
        'Dim co As Double

        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(2).Cells(col.Index).Value)) <> "0" Then
        '            co += 1
        '        End If

        '    End If
        'Next
        'If co > 0 Then

        '    FrmOpChart.chtcalldelta.ColumnCount = 0
        '    FrmOpChart.chtcalldelta.ColumnLabelCount = 0
        '    FrmOpChart.chtcalldelta.ColumnLabelIndex = 0
        '    FrmOpChart.chtcalldelta.AutoIncrement = False
        '    FrmOpChart.chtcalldelta.RowCount = co - 1
        '    FrmOpChart.chtcalldelta.RowLabelCount = 1
        '    FrmOpChart.chtcalldelta.RowLabelIndex = 1



        '    FrmOpChart.chtcalldelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        '    'chtcalldelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        '    Dim cha(FrmOpChart.chtcalldelta.RowCount, 1) As Object
        '    Dim r As Integer
        '    r = 0

        '    For Each col As DataGridViewColumn In grdcp.Columns
        '        If col.Index > 1 Then
        '            If UCase(CStr(grdcp.Rows(2).Cells(col.Index).Value)) <> "0" Then
        '                cha(r, 0) = Math.Round(grdcp.Rows(2).Cells(col.Index).Value, 10)
        '                r += 1
        '            End If
        '        End If
        '    Next
        'FrmOpChart.chtcalldelta.ChartData = cha
        PlotStockChart(FrmOpChart.Schtcalldelta, "CallDelta", 2)
        PlotStockChart(Schtcalldelta, "CallDelta", 2)

        'With chtcalldelta.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).ValueScale
        '    .Minimum = GetMinVal(2)
        '    .Maximum = GetMaxVal(2)
        'End With

        'r = 1
        'For Each col As DataGridViewColumn In grdmain.Columns

        '    If col.Index > 1 Then

        '        FrmOpChart.chtcalldelta.Row = r
        '        FrmOpChart.chtcalldelta.RowLabel = r


        '        r += 1
        '        If r > FrmOpChart.chtcalldelta.RowCount Then
        '            Exit For
        '        End If
        '    End If
        'Next
        'End If
    End Sub
    Private Sub put_delta_chart()
        'Dim co As Double

        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(3).Cells(col.Index).Value)) <> "0" Then
        '            co += 1
        '        End If

        '    End If
        'Next
        'If co > 0 Then


        '    FrmOpChart.chtputdelta.ColumnCount = 0
        '    FrmOpChart.chtputdelta.ColumnLabelCount = 0
        '    FrmOpChart.chtputdelta.ColumnLabelIndex = 0
        '    FrmOpChart.chtputdelta.AutoIncrement = False
        '    FrmOpChart.chtputdelta.RowCount = co - 1
        '    FrmOpChart.chtputdelta.RowLabelCount = 1
        '    FrmOpChart.chtputdelta.RowLabelIndex = 1
        '    FrmOpChart.chtputdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    'chtputdelta.ColumnCount = 0
        '    'chtputdelta.ColumnLabelCount = 0
        '    'chtputdelta.ColumnLabelIndex = 0
        '    'chtputdelta.AutoIncrement = False
        '    'chtputdelta.RowCount = co - 1
        '    'chtputdelta.RowLabelCount = 1
        '    'chtputdelta.RowLabelIndex = 1
        '    'chtputdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone


        '    Dim cha(co - 1, 1) As Object
        '    Dim r As Integer
        '    r = 0
        '    For Each col As DataGridViewColumn In grdcp.Columns
        '        If col.Index > 1 Then
        '            If UCase(CStr(grdcp.Rows(3).Cells(col.Index).Value)) <> "0" Then
        '                cha(r, 0) = Math.Round(grdcp.Rows(3).Cells(col.Index).Value, 10)
        '                r += 1
        '            End If

        '        End If
        '    Next
        'FrmOpChart.chtputdelta.ChartData = cha
        PlotStockChart(FrmOpChart.Schtputdelta, "PutDelta", 3)
        PlotStockChart(Schtputdelta, "PutDelta", 3)

        'With chtputdelta.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).ValueScale
        '    .Minimum = GetMinVal(3)
        '    .Maximum = GetMaxVal(3)
        'End With

        'r = 1
        'For Each col As DataGridViewColumn In grdmain.Columns

        '    If col.Index > 1 Then
        '        FrmOpChart.chtputdelta.Row = r
        '        FrmOpChart.chtputdelta.RowLabel = r

        '        'chtputdelta.Row = r
        '        'chtputdelta.RowLabel = r

        '        r += 1
        '        If r > FrmOpChart.chtputdelta.RowCount Then
        '            Exit For
        '        End If
        '    End If
        'Next
        'End If
    End Sub

    Private Sub call_theta_chart()
        'Dim co As Double

        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(4).Cells(col.Index).Value)) <> "0" Then
        '            co += 1
        '        End If

        '    End If
        'Next
        'If co > 0 Then

        '    FrmOpChart.chtcalltheta.ColumnCount = 0
        '    FrmOpChart.chtcalltheta.ColumnLabelCount = 0
        '    FrmOpChart.chtcalltheta.ColumnLabelIndex = 0
        '    FrmOpChart.chtcalltheta.AutoIncrement = False
        '    FrmOpChart.chtcalltheta.RowCount = co - 1
        '    FrmOpChart.chtcalltheta.RowLabelCount = 1
        '    FrmOpChart.chtcalltheta.RowLabelIndex = 1
        '    FrmOpChart.chtcalltheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    'chtcalltheta.ColumnCount = 0
        '    'chtcalltheta.ColumnLabelCount = 0
        '    'chtcalltheta.ColumnLabelIndex = 0
        '    'chtcalltheta.AutoIncrement = False
        '    'chtcalltheta.RowCount = co - 1
        '    'chtcalltheta.RowLabelCount = 1
        '    'chtcalltheta.RowLabelIndex = 1
        '    'chtcalltheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    Dim cha(co - 1, 1) As Object
        '    Dim r As Integer
        '    r = 0
        '    For Each col As DataGridViewColumn In grdcp.Columns
        '        If col.Index > 1 Then
        '            If UCase(CStr(grdcp.Rows(4).Cells(col.Index).Value)) <> "0" Then
        '                cha(r, 0) = Math.Round(grdcp.Rows(4).Cells(col.Index).Value, 10)
        '                r += 1
        '            End If
        '        End If
        '    Next
        'FrmOpChart.chtcalltheta.ChartData = cha
        PlotStockChart(FrmOpChart.Schtcalltheta, "CallTheta", 4)
        PlotStockChart(Schtcalltheta, "CallTheta", 4)

        'r = 1

        ''With chtcalltheta.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).ValueScale
        ''    .Minimum = GetMinVal(4)
        ''    .Maximum = GetMaxVal(4)
        ''End With


        'For Each col As DataGridViewColumn In grdmain.Columns

        '    If col.Index > 1 Then
        '        FrmOpChart.chtcalltheta.Row = r
        '        FrmOpChart.chtcalltheta.RowLabel = r

        '        'chtcalltheta.Row = r
        '        'chtcalltheta.RowLabel = r

        '        r += 1
        '        If r > FrmOpChart.chtcalltheta.RowCount Then
        '            Exit For
        '        End If
        '    End If
        'Next
        'End If
    End Sub
    Private Sub put_theta_chart()
        'Dim co As Double

        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(5).Cells(col.Index).Value)) <> "0" Then
        '            co += 1
        '        End If

        '    End If
        'Next
        'If co > 0 Then
        '    FrmOpChart.chtputtheta.ColumnCount = 0
        '    FrmOpChart.chtputtheta.ColumnLabelCount = 0
        '    FrmOpChart.chtputtheta.ColumnLabelIndex = 0
        '    FrmOpChart.chtputtheta.AutoIncrement = False
        '    FrmOpChart.chtputtheta.RowCount = co - 1
        '    FrmOpChart.chtputtheta.RowLabelCount = 1
        '    FrmOpChart.chtputtheta.RowLabelIndex = 1
        '    FrmOpChart.chtputtheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    'chtputtheta.ColumnCount = 0
        '    'chtputtheta.ColumnLabelCount = 0
        '    'chtputtheta.ColumnLabelIndex = 0
        '    'chtputtheta.AutoIncrement = False
        '    'chtputtheta.RowCount = co - 1
        '    'chtputtheta.RowLabelCount = 1
        '    'chtputtheta.RowLabelIndex = 1
        '    'chtputtheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        '    Dim cha(co - 1, 1) As Object
        '    Dim r As Integer
        '    r = 0
        '    For Each col As DataGridViewColumn In grdcp.Columns
        '        If col.Index > 1 Then
        '            If UCase(CStr(grdcp.Rows(5).Cells(col.Index).Value)) <> "0" Then
        '                cha(r, 0) = Math.Round(grdcp.Rows(5).Cells(col.Index).Value, 10)
        '                r += 1
        '            End If
        '        End If
        '    Next
        'FrmOpChart.chtputtheta.ChartData = cha
        PlotStockChart(FrmOpChart.Schtputtheta, "PutTheta", 5)
        PlotStockChart(Schtputtheta, "PutTheta", 5)

        'With chtputtheta.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).ValueScale
        '    .Minimum = GetMinVal(5)
        '    .Maximum = GetMaxVal(5)
        'End With


        'r = 1
        'For Each col As DataGridViewColumn In grdmain.Columns

        '    If col.Index > 1 Then
        '        FrmOpChart.chtputtheta.Row = r
        '        FrmOpChart.chtputtheta.RowLabel = r

        '        'chtputtheta.Row = r
        '        'chtputtheta.RowLabel = r

        '        r += 1
        '        If r > FrmOpChart.chtputtheta.RowCount Then
        '            Exit For
        '        End If
        '    End If
        'Next
        'End If
    End Sub
    Private Sub Gamma_chart()
        'Dim co As Double

        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(6).Cells(col.Index).Value)) <> "0" Then
        '            co += 1
        '        End If
        '    End If
        'Next

        'If co > 0 Then
        '    FrmOpChart.chtgamma.ColumnCount = 0
        '    FrmOpChart.chtgamma.ColumnLabelCount = 0
        '    FrmOpChart.chtgamma.ColumnLabelIndex = 0
        '    FrmOpChart.chtgamma.AutoIncrement = False
        '    FrmOpChart.chtgamma.RowCount = co - 1
        '    FrmOpChart.chtgamma.RowLabelCount = 1
        '    FrmOpChart.chtgamma.RowLabelIndex = 1
        '    FrmOpChart.chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone



        '    'chtgamma.ColumnCount = 0
        '    'chtgamma.ColumnLabelCount = 0
        '    'chtgamma.ColumnLabelIndex = 0
        '    'chtgamma.AutoIncrement = False
        '    'chtgamma.RowCount = co - 1
        '    'chtgamma.RowLabelCount = 1
        '    'chtgamma.RowLabelIndex = 1
        '    'chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    Dim cha(co - 1, 1) As Object
        '    Dim r As Integer
        '    r = 0
        '    For Each col As DataGridViewColumn In grdcp.Columns
        '        If col.Index > 1 Then
        '            If UCase(CStr(grdcp.Rows(6).Cells(col.Index).Value)) <> "0" Then
        '                cha(r, 0) = Math.Round(Val(grdcp.Rows(6).Cells(col.Index).Value), 10)
        '                r += 1
        '            End If
        '        End If
        '    Next
        'FrmOpChart.chtgamma.ChartData = cha
        PlotStockChart(FrmOpChart.Schtgamma, "Gamma", 6)
        PlotStockChart(Schtgamma, "Gamma", 6)

        'chtgamma.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisScale
        'With chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).ValueScale
        '    .Minimum = GetMinVal(6)
        '    .Maximum = GetMaxVal(6)
        'End With

        'Dim ax As New MSChart20Lib.Axis
        'ax.ValueScale.Minimum = -0.0001
        'ax.ValueScale.Maximum = 0.0009
        'chtgamma.Plot.Axis = ax
        'r = 1
        'For Each col As DataGridViewColumn In grdmain.Columns

        '    If col.Index > 1 Then

        '        FrmOpChart.chtgamma.Row = r
        '        FrmOpChart.chtgamma.RowLabel = r

        '        'chtgamma.Row = r
        '        'chtgamma.RowLabel = r

        '        r += 1
        '        If r > FrmOpChart.chtgamma.RowCount Then
        '            Exit For
        '        End If
        '    End If
        'Next
        'End If
    End Sub

    Private Sub Volga_chart()
        'Dim co As Double

        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(10).Cells(col.Index).Value)) <> "0" Then
        '            co += 1
        '        End If
        '    End If
        'Next

        'If co > 0 Then
        '    FrmOpChart.chtgamma.ColumnCount = 0
        '    FrmOpChart.chtgamma.ColumnLabelCount = 0
        '    FrmOpChart.chtgamma.ColumnLabelIndex = 0
        '    FrmOpChart.chtgamma.AutoIncrement = False
        '    FrmOpChart.chtgamma.RowCount = co - 1
        '    FrmOpChart.chtgamma.RowLabelCount = 1
        '    FrmOpChart.chtgamma.RowLabelIndex = 1
        '    FrmOpChart.chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone



        '    'chtgamma.ColumnCount = 0
        '    'chtgamma.ColumnLabelCount = 0
        '    'chtgamma.ColumnLabelIndex = 0
        '    'chtgamma.AutoIncrement = False
        '    'chtgamma.RowCount = co - 1
        '    'chtgamma.RowLabelCount = 1
        '    'chtgamma.RowLabelIndex = 1
        '    'chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    Dim cha(co - 1, 1) As Object
        '    Dim r As Integer
        '    r = 0
        '    For Each col As DataGridViewColumn In grdcp.Columns
        '        If col.Index > 1 Then
        '            If UCase(CStr(grdcp.Rows(10).Cells(col.Index).Value)) <> "0" Then
        '                cha(r, 0) = Math.Round(Val(grdcp.Rows(10).Cells(col.Index).Value), 10)
        '                r += 1
        '            End If
        '        End If
        '    Next
        'FrmOpChart.chtgamma.ChartData = cha
        PlotStockChart(FrmOpChart.SchtVolga, "Volga", 10)
        PlotStockChart(SchtVolga, "Volga", 10)

        'chtgamma.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisScale
        'With chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).ValueScale
        '    .Minimum = GetMinVal(10)
        '    .Maximum = GetMaxVal(10)
        'End With

        'Dim ax As New MSChart20Lib.Axis
        'ax.ValueScale.Minimum = -0.0001
        'ax.ValueScale.Maximum = 0.0009
        'chtgamma.Plot.Axis = ax
        'r = 1
        'For Each col As DataGridViewColumn In grdmain.Columns

        '    If col.Index > 1 Then

        '        FrmOpChart.chtgamma.Row = r
        '        FrmOpChart.chtgamma.RowLabel = r

        '        'chtgamma.Row = r
        '        'chtgamma.RowLabel = r

        '        r += 1
        '        If r > FrmOpChart.chtgamma.RowCount Then
        '            Exit For
        '        End If
        '    End If
        'Next
        'End If
    End Sub

    Private Sub Vanna_chart()
        'Dim co As Double

        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(11).Cells(col.Index).Value)) <> "0" Then
        '            co += 1
        '        End If
        '    End If
        'Next

        'If co > 0 Then
        '    FrmOpChart.chtgamma.ColumnCount = 0
        '    FrmOpChart.chtgamma.ColumnLabelCount = 0
        '    FrmOpChart.chtgamma.ColumnLabelIndex = 0
        '    FrmOpChart.chtgamma.AutoIncrement = False
        '    FrmOpChart.chtgamma.RowCount = co - 1
        '    FrmOpChart.chtgamma.RowLabelCount = 1
        '    FrmOpChart.chtgamma.RowLabelIndex = 1
        '    FrmOpChart.chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone



        '    'chtgamma.ColumnCount = 0
        '    'chtgamma.ColumnLabelCount = 0
        '    'chtgamma.ColumnLabelIndex = 0
        '    'chtgamma.AutoIncrement = False
        '    'chtgamma.RowCount = co - 1
        '    'chtgamma.RowLabelCount = 1
        '    'chtgamma.RowLabelIndex = 1
        '    'chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    Dim cha(co - 1, 1) As Object
        '    Dim r As Integer
        '    r = 0
        '    For Each col As DataGridViewColumn In grdcp.Columns
        '        If col.Index > 1 Then
        '            If UCase(CStr(grdcp.Rows(11).Cells(col.Index).Value)) <> "0" Then
        '                cha(r, 0) = Math.Round(Val(grdcp.Rows(11).Cells(col.Index).Value), 10)
        '                r += 1
        '            End If
        '        End If
        '    Next
        'FrmOpChart.chtgamma.ChartData = cha
        PlotStockChart(FrmOpChart.SchtVanna, "Vanna", 11)
        PlotStockChart(SchtVanna, "Vanna", 11)

        'chtgamma.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisScale
        'With chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).ValueScale
        '    .Minimum = GetMinVal(11)
        '    .Maximum = GetMaxVal(11)
        'End With

        'Dim ax As New MSChart20Lib.Axis
        'ax.ValueScale.Minimum = -0.0001
        'ax.ValueScale.Maximum = 0.0009
        'chtgamma.Plot.Axis = ax
        'r = 1
        'For Each col As DataGridViewColumn In grdmain.Columns

        '    If col.Index > 1 Then

        '        FrmOpChart.chtgamma.Row = r
        '        FrmOpChart.chtgamma.RowLabel = r

        '        'chtgamma.Row = r
        '        'chtgamma.RowLabel = r

        '        r += 1
        '        If r > FrmOpChart.chtgamma.RowCount Then
        '            Exit For
        '        End If
        '    End If
        'Next
        'End If
    End Sub

    Private Function GetMinVal(ByVal i As Integer) As Double
        Dim dMinVal As Double = 100000000
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(i).Cells(col.Index).Value)) <> "0" Then
                    If dMinVal > Math.Round(Val(grdcp.Rows(i).Cells(col.Index).Value), 10) Then
                        dMinVal = Math.Round(Val(grdcp.Rows(i).Cells(col.Index).Value), 10)
                    End If
                End If
            End If
        Next
        If dMinVal = 100000000 Then
            dMinVal = 0
        End If
        Return dMinVal
    End Function

    Private Function GetMaxVal(ByVal i As Integer) As Double
        Dim dMaxVal As Double = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(i).Cells(col.Index).Value)) <> "0" Then
                    If dMaxVal < Math.Round(Val(grdcp.Rows(i).Cells(col.Index).Value), 10) Then
                        dMaxVal = Math.Round(Val(grdcp.Rows(i).Cells(col.Index).Value), 10)
                    End If
                End If
            End If
        Next
        If dMaxVal = 0 Then
            dMaxVal = 1
        End If
        Return dMaxVal
    End Function

    Private Sub vega_chart()
        'Dim co As Double

        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(7).Cells(col.Index).Value)) <> "0" Then
        '            co += 1
        '        End If
        '    End If
        'Next

        'If co > 0 Then
        '    FrmOpChart.chtvega.ColumnCount = 0
        '    FrmOpChart.chtvega.ColumnLabelCount = 0
        '    FrmOpChart.chtvega.ColumnLabelIndex = 0
        '    FrmOpChart.chtvega.AutoIncrement = False
        '    FrmOpChart.chtvega.RowCount = co - 1
        '    FrmOpChart.chtvega.RowLabelCount = 1
        '    FrmOpChart.chtvega.RowLabelIndex = 1
        '    FrmOpChart.chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    'chtvega.ColumnCount = 0
        '    'chtvega.ColumnLabelCount = 0
        '    'chtvega.ColumnLabelIndex = 0
        '    'chtvega.AutoIncrement = False
        '    'chtvega.RowCount = co - 1
        '    'chtvega.RowLabelCount = 1
        '    'chtvega.RowLabelIndex = 1
        '    'chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    Dim cha(co - 1, 1) As Object
        '    Dim r As Integer
        '    r = 0
        '    For Each col As DataGridViewColumn In grdcp.Columns
        '        If col.Index > 1 Then
        '            If UCase(CStr(grdcp.Rows(7).Cells(col.Index).Value)) <> "0" Then
        '                cha(r, 0) = Math.Round(grdcp.Rows(7).Cells(col.Index).Value, 10)
        '                r += 1
        '            End If
        '        End If
        '    Next
        'FrmOpChart.chtvega.ChartData = cha
        PlotStockChart(FrmOpChart.Schtvega, "Vega", 7)
        PlotStockChart(Schtvega, "Vega", 7)


        'With chtvega.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).ValueScale
        '    .Minimum = GetMinVal(7)
        '    .Maximum = GetMaxVal(7)
        'End With


        'r = 1
        'For Each col As DataGridViewColumn In grdmain.Columns

        '    If col.Index > 1 Then
        '        FrmOpChart.chtvega.Row = r
        '        FrmOpChart.chtvega.RowLabel = r

        '        'chtvega.Row = r
        '        'chtvega.RowLabel = r

        '        r += 1
        '        If r > FrmOpChart.chtvega.RowCount Then
        '            Exit For
        '        End If
        '    End If
        'Next
        'End If
    End Sub
    Private Sub call_Roh_chart()
        'Dim co As Double

        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(8).Cells(col.Index).Value)) <> "0" Then
        '            co += 1
        '        End If

        '    End If
        'Next
        'If co > 0 Then
        '    FrmOpChart.chtcallroh.ColumnCount = 0
        '    FrmOpChart.chtcallroh.ColumnLabelCount = 0
        '    FrmOpChart.chtcallroh.ColumnLabelIndex = 0
        '    FrmOpChart.chtcallroh.AutoIncrement = False
        '    FrmOpChart.chtcallroh.RowCount = co - 1
        '    FrmOpChart.chtcallroh.RowLabelCount = 1
        '    FrmOpChart.chtcallroh.RowLabelIndex = 1
        '    FrmOpChart.chtcallroh.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    'chtcallroh.ColumnCount = 0
        '    'chtcallroh.ColumnLabelCount = 0
        '    'chtcallroh.ColumnLabelIndex = 0
        '    'chtcallroh.AutoIncrement = False
        '    'chtcallroh.RowCount = co - 1
        '    'chtcallroh.RowLabelCount = 1
        '    'chtcallroh.RowLabelIndex = 1
        '    'chtcallroh.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    Dim cha(co - 1, 1) As Object
        '    Dim r As Integer
        '    r = 0
        '    For Each col As DataGridViewColumn In grdcp.Columns
        '        If col.Index > 1 Then
        '            If UCase(CStr(grdcp.Rows(8).Cells(col.Index).Value)) <> "0" Then
        '                cha(r, 0) = Math.Round(grdcp.Rows(8).Cells(col.Index).Value, 10)
        '                r += 1
        '            End If
        '        End If
        '    Next
        'FrmOpChart.chtcallroh.ChartData = cha
        PlotStockChart(FrmOpChart.Schtcallroh, "CallRoh", 8)
        PlotStockChart(Schtcallroh, "CallRoh", 8)

        'With chtcallroh.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).ValueScale
        '    .Minimum = GetMinVal(8)
        '    .Maximum = GetMaxVal(8)
        'End With

        'r = 1
        'For Each col As DataGridViewColumn In grdmain.Columns

        '    If col.Index > 1 Then
        '        FrmOpChart.chtcallroh.Row = r
        '        FrmOpChart.chtcallroh.RowLabel = r

        '        'chtcallroh.Row = r
        '        'chtcallroh.RowLabel = r

        '        r += 1
        '        If r > FrmOpChart.chtcallroh.RowCount Then
        '            Exit For
        '        End If
        '    End If
        'Next
        'End If
    End Sub
    Private Sub put_Roh_chart()
        'Dim co As Double

        'For Each col As DataGridViewColumn In grdcp.Columns
        '    If col.Index > 1 Then
        '        If UCase(CStr(grdcp.Rows(9).Cells(col.Index).Value)) <> "0" Then
        '            co += 1
        '        End If

        '    End If
        'Next
        'If co > 0 Then
        '    FrmOpChart.chtputroh.ColumnCount = 0
        '    FrmOpChart.chtputroh.ColumnLabelCount = 0
        '    FrmOpChart.chtputroh.ColumnLabelIndex = 0
        '    FrmOpChart.chtputroh.AutoIncrement = False
        '    FrmOpChart.chtputroh.RowCount = co - 1
        '    FrmOpChart.chtputroh.RowLabelCount = 1
        '    FrmOpChart.chtputroh.RowLabelIndex = 1
        '    FrmOpChart.chtputroh.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    'chtputroh.ColumnCount = 0
        '    'chtputroh.ColumnLabelCount = 0
        '    'chtputroh.ColumnLabelIndex = 0
        '    'chtputroh.AutoIncrement = False
        '    'chtputroh.RowCount = co - 1
        '    'chtputroh.RowLabelCount = 1
        '    'chtputroh.RowLabelIndex = 1
        '    'chtputroh.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone

        '    Dim cha(co - 1, 1) As Object
        '    Dim r As Integer
        '    r = 0
        '    For Each col As DataGridViewColumn In grdcp.Columns
        '        If col.Index > 1 Then
        '            If UCase(CStr(grdcp.Rows(9).Cells(col.Index).Value)) <> "0" Then
        '                cha(r, 0) = Math.Round(grdcp.Rows(9).Cells(col.Index).Value, 10)
        '                r += 1
        '            End If
        '        End If
        '    Next

        'FrmOpChart.chtputroh.ChartData = cha
        PlotStockChart(FrmOpChart.Schtputroh, "PutRoh", 9)
        PlotStockChart(Schtputroh, "PutRoh", 9)

        'With chtputroh.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).ValueScale
        '    .Minimum = GetMinVal(9)
        '    .Maximum = GetMaxVal(9)
        'End With


        'r = 1
        'For Each col As DataGridViewColumn In grdmain.Columns

        '    If col.Index > 1 Then
        '        FrmOpChart.chtputroh.Row = r
        '        FrmOpChart.chtputroh.RowLabel = r

        '        'chtputroh.Row = r
        '        'chtputroh.RowLabel = r

        '        r += 1
        '        If r > FrmOpChart.chtputroh.RowCount Then
        '            Exit For
        '        End If
        '    End If
        'Next
        'End If
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

    Private Sub grdmain_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles grdmain.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            grdcp.HorizontalScrollingOffset = e.NewValue()
            'ClsScrollbar.GetSBarPos(grdmain)
            'ClsScrollbar.SetSBarPos(grdcp)
            'grdcp_Scroll(sender, e)
        End If
    End Sub

    Private Sub grdcp_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles grdcp.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            grdmain.HorizontalScrollingOffset = e.NewValue()
            'ClsScrollbar.GetSBarPos(grdmain)
            'ClsScrollbar.SetSBarPos(grdcp)
            'grdcp_Scroll(sender, e)
        End If
    End Sub

    Private Sub dttimeII_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dttimeII.ValueChanged
        If dttimeI.Value < dttimeII.Value Then
            txtdays.Text = DateDiff(DateInterval.Day, dttimeI.Value, dttimeII.Value)
        Else
            txtdays.Text = 0
        End If

    End Sub

    Private Sub chtcallperm_DblClick(ByVal sender As Object, ByVal e As System.EventArgs)
        FrmOpChart.ShowForm(TabControl1.SelectedIndex, grdcp) '(chtcallperm, chtputprem, chtcalldelta, chtputdelta, chtgamma, chtvega, chtcalltheta, chtputtheta, chtcallroh, chtputroh)
    End Sub

    Private Sub Schtcallperm_DoubleClickEvent(ByVal sender As Object, ByVal e As System.EventArgs) Handles Schtcallperm.DoubleClickEvent, Schtputprem.DoubleClickEvent, Schtcalldelta.DoubleClickEvent, Schtputdelta.DoubleClickEvent, Schtgamma.DoubleClickEvent, Schtvega.DoubleClickEvent, Schtcalltheta.DoubleClickEvent, Schtputtheta.DoubleClickEvent, Schtcallroh.DoubleClickEvent, Schtputroh.DoubleClickEvent, StockChart1.DoubleClickEvent, SchtVolga.DoubleClickEvent, SchtVanna.DoubleClickEvent
        FrmOpChart.ShowForm(TabControl1.SelectedIndex, grdcp) '(chtcallperm, chtputprem, chtcalldelta, chtputdelta, chtgamma, chtvega, chtcalltheta, chtputtheta, chtcallroh, chtputroh)
        ReFreshSChart()
    End Sub

    Private Sub PlotAllStockChart(ByVal chrtname As String)
        Dim Panel As Short
        Dim Panel1 As Short
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
            Case "Gamma".ToUpper
                RowInx = 6
                Colour = System.Drawing.Color.Cyan
            Case "Vega".ToUpper
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
        Panel1 = FrmOpChart.StockChart1.AddChartPanel() REM to Get New Panel Index

        StockChart1.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel) REM Series into Chart 
        StockChart1.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(Colour)) REM Set Series Line Color
        StockChart1.set_SeriesWeight(chrtname, 2) REM Set Series Weight

        FrmOpChart.StockChart1.AddSeries(chrtname, STOCKCHARTXLib.SeriesType.stLineChart, Panel1) REM Series into Chart 
        FrmOpChart.StockChart1.set_SeriesColor(chrtname, System.Drawing.ColorTranslator.ToOle(Colour)) REM Set Series Line Color
        FrmOpChart.StockChart1.set_SeriesWeight(chrtname, 2) REM Set Series Weight

        Dim VarChrtData As Double = 0
        For Each col As DataGridViewColumn In grdcp.Columns
            If col.Index > 1 Then
                If UCase(CStr(grdcp.Rows(RowInx).Cells(col.Index).Value)) <> "0" Then
                    VarChrtData = Val(grdcp.Rows(RowInx).Cells(col.Index).Value & "")
                    StockChart1.AppendValue(chrtname, StockChart1.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                    FrmOpChart.StockChart1.AppendValue(chrtname, StockChart1.ToJulianDate(col.Index, 1, 1, 1, 1, 1), VarChrtData)
                End If
            End If
        Next

    End Sub

    Private Sub BtnSelectChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectChart.Click
        TabControl1.SelectTab("All")
        'If TabControl1.SelectedTab.Text = "All" Then
        ObjSelectChart.ShowForm()
        ReFreshSChart()
        'End If
    End Sub
    Private Sub ReFreshSChart()
        Try


            ObjSelectChart.ArrSeriesCol.Sort()

            StockChart1.RemoveAllSeries()
            FrmOpChart.StockChart1.RemoveAllSeries()

            'StockChart1.SetYScale
            StockChart1.ThreeDStyle = True REM To show chart in 3D
            StockChart1.RealTimeXLabels = False
            StockChart1.DisplayTitles = True REM To Set whether Chart Series display or Not
            StockChart1.ShowRecordsForXLabels = True

            FrmOpChart.StockChart1.ThreeDStyle = True REM To show chart in 3D
            FrmOpChart.StockChart1.RealTimeXLabels = False
            FrmOpChart.StockChart1.DisplayTitles = True REM To Set whether Chart Series display or Not
            FrmOpChart.StockChart1.ShowRecordsForXLabels = True

            For Each str As Object In ObjSelectChart.ArrSeriesCol
                PlotAllStockChart(str.ToString.Substring(2, str.ToString.Length - 2))
            Next


            Dim TotalPanelHeight As Double = StockChart1.Height - 20
            For i As Integer = 1 To ObjSelectChart.ArrSeriesCol.Count
                StockChart1.set_PanelY1(i, (TotalPanelHeight / ObjSelectChart.ArrSeriesCol.Count) * i)
            Next

            TotalPanelHeight = FrmOpChart.StockChart1.Height - 20
            For i As Integer = 1 To ObjSelectChart.ArrSeriesCol.Count
                FrmOpChart.StockChart1.set_PanelY1(i, (TotalPanelHeight / ObjSelectChart.ArrSeriesCol.Count) * i)
            Next
        Catch ex As Exception

        End Try
        Try
            StockChart1.Update()
            FrmOpChart.StockChart1.Update()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdmain_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdmain.DataError

    End Sub

    Private Sub grdcp_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdcp.DataError

    End Sub

    Private Sub txtcol_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcol.TextChanged

    End Sub

    Private Sub txtcol_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcol.Enter, txtvol.Enter, txtstrike.Enter, txtspot.Enter, txtintrate.Enter, txtdays.Enter
        CType(sender, TextBox).SelectAll()
    End Sub

    Private Sub txtcol_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtcol.MouseClick, txtvol.MouseClick, txtstrike.MouseClick, txtspot.MouseClick, txtintrate.MouseClick, txtdays.MouseClick
        'CType(sender, TextBox).SelectAll()
    End Sub

    Private Sub grdcp_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcp.CellContentClick

    End Sub

    Private Sub txtspot_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtspot.TextChanged

    End Sub
End Class