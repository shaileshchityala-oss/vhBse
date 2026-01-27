Public Class rptExpense
    Dim maintable As New DataTable
    Dim objTrading As New trading
    Dim objExp As New expenses
    Dim expTable As New DataTable
    Dim rptTable As New DataTable
    Dim fifo_avg As String


    ''' <summary>
    '''  Load Data in the MainTable from Database 
    ''' fill exptable DataTable from DataBase
    ''' after Calculation of expense assign Data to datagrid view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rptExpense_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Try
        'fill all trades
        maintable = objTrading.select_all_trade
        'grdtrad.DataSource = maintable
        expTable = objExp.Select_Expenses
        Dim dtEntrydate As New DataTable
        dtEntrydate = maintable.DefaultView.ToTable(True, "entry_date")
        rptTable.Columns.Add("Script", GetType(String))
        rptTable.Columns.Add("Instrument", GetType(String))
        rptTable.Columns.Add("Security", GetType(String))
        rptTable.Columns.Add("CPF", GetType(String))
        'rptTable.Columns.Add("Exp_Date", GetType(Date))
        rptTable.Columns.Add("strike", GetType(Double))
        For Each row As DataRow In dtEntrydate.Rows
            Try
                rptTable.Columns.Add(row("entry_date"), GetType(String))
            Catch ex As Exception
            End Try
            'rptTable.Columns(str).CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        Next


        Dim arr(2) As String
        arr(0) = "script"
        arr(1) = "entry_date"
        arr(2) = "Company"

        Dim drow As DataRow
        Dim mrow() As DataRow
        Dim dtemp As New DataTable
        dtemp = maintable.DefaultView.ToTable(True, arr)
        For Each row As DataRow In dtemp.Rows
            drow = rptTable.NewRow
            drow("Script") = row("script")
            mrow = maintable.Select("script='" & row("script") & "' and entry_date='" & row("entry_date") & "' And company='" & row("company") & "'")
            drow("Instrument") = mrow(0).Item("instrumentname")
            drow("Security") = mrow(0).Item("company")
            drow("CPF") = mrow(0).Item("eq")
            ' drow("Exp_date") = mrow(0).Item("mdate")
            drow("strike") = Val(mrow(0).Item("strikerate"))
            Dim stexp, stexp1, ndst, dst, exppr, expto As Double
            stexp = 0
            stexp1 = 0
            dst = 0
            ndst = 0
            exppr = 0
            expto = 0


            'Futre #################################################################
            If mrow(0).Item("eq") = "F" Or mrow(0).Item("eq") = "X" Then
                stexp = 0
                stexp1 = 0
                expto = 0
                stexp = Val(IIf(Not IsDBNull(maintable.Compute("sum(tot)", "eq not in ('C','P') and script='" & row("script") & "' and qty > 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'")), maintable.Compute("sum(tot)", "eq not in ('C','P') and script='" & row("script") & "'  and qty > 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'"), 0))
                stexp1 = Math.Abs(Val(IIf(Not IsDBNull(maintable.Compute("sum(tot)", "eq not in ('C','P') and script='" & row("script") & "' and qty < 0 and entry_date = #" & fDate(row("entry_date")) & "#And company='" & row("company") & "'")), maintable.Compute("sum(tot)", "eq not in ('C','P') and script='" & row("script") & "'  and qty < 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'"), 0)))

                expto += ((stexp * expTable.Compute("max(futl)", "")) / expTable.Compute("max(futlp)", ""))
                expto += ((stexp1 * expTable.Compute("max(futs)", "")) / expTable.Compute("max(futsp)", ""))
            ElseIf mrow(0).Item("eq") = "C" Or mrow(0).Item("eq") = "P" Then
                'Option ####################################################################

                If Val(expTable.Compute("max(spl)", "")) <> 0 Then
                    stexp = 0
                    stexp1 = 0
                    expto = 0
                    stexp = Val(IIf(Not IsDBNull(maintable.Compute("sum(tot)", "eq  in ('C','P') and script='" & row("script") & "' and qty > 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'")), maintable.Compute("sum(tot)", "eq  in ('C','P') and script='" & row("script") & "'  and qty > 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'"), 0))
                    stexp1 = Math.Abs(Val(IIf(Not IsDBNull(maintable.Compute("sum(tot)", "eq  in ('C','P') and script='" & row("script") & "' and qty < 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'")), maintable.Compute("sum(tot)", "eq  in ('C','P') and script='" & row("script") & "'  and qty < 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'"), 0)))

                    expto += ((stexp * expTable.Compute("max(spl)", "")) / expTable.Compute("max(splp)", ""))
                    expto += ((stexp1 * expTable.Compute("max(sps)", "")) / expTable.Compute("max(spsp)", ""))
                Else
                    stexp = 0
                    stexp1 = 0
                    expto = 0
                    stexp = Val(IIf(Not IsDBNull(maintable.Compute("sum(tot)", "eq  in ('C','P') and script='" & row("script") & "' and qty > 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'")), maintable.Compute("sum(tot)", "eq  in ('C','P') and script='" & row("script") & "'  and qty > 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'"), 0))
                    stexp1 = Math.Abs(Val(IIf(Not IsDBNull(maintable.Compute("sum(tot)", "eq  in ('C','P') and script='" & row("script") & "' and qty < 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'")), maintable.Compute("sum(tot)", "eq  in ('C','P') and script='" & row("script") & "'  and qty < 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'"), 0)))

                    expto += ((stexp * expTable.Compute("max(prel)", "")) / expTable.Compute("max(prelp)", ""))
                    expto += ((stexp1 * expTable.Compute("max(pres)", "")) / expTable.Compute("max(presp)", ""))
                End If
                'Equity ##################################################################
            Else

                stexp = 0
                stexp1 = 0
                dst = 0
                ndst = 0
                expto = 0
                stexp = Math.Round(Val(IIf(Not IsDBNull(maintable.Compute("sum(tot)", "script='" & row("script") & "' and qty > 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'")), maintable.Compute("sum(tot)", "script='" & row("script") & "' and qty > 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'"), 0)), 2)
                stexp1 = Math.Abs(Math.Round(Val(IIf(Not IsDBNull(maintable.Compute("sum(tot)", "script='" & row("script") & "' and qty < 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'")), maintable.Compute("sum(tot)", "script='" & row("script") & "' and qty < 0 and entry_date = #" & fDate(row("entry_date")) & "# And company='" & row("company") & "'"), 0)), 2))
                dst = stexp - stexp1
                If dst > 0 Then
                    ndst = stexp1
                    'If CDate(row("entry_date")) = Now.Date() Then
                    '    expto += ((dst * expTable.Compute("max(ndbl)", "")) / expTable.Compute("max(ndblp)", ""))
                    'Else
                    expto += ((dst * expTable.Compute("max(dbl)", "")) / expTable.Compute("max(dblp)", ""))
                    ' End If
                    expto += ((stexp1 * expTable.Compute("max(ndbs)", "")) / expTable.Compute("max(ndbsp)", ""))
                    expto += ((stexp1 * expTable.Compute("max(ndbl)", "")) / expTable.Compute("max(ndblp)", ""))
                Else
                    ndst = stexp
                    dst = -dst

                    expto += ((dst * expTable.Compute("max(dbs)", "")) / expTable.Compute("max(dbsp)", ""))
                    expto += ((stexp * expTable.Compute("max(ndbl)", "")) / expTable.Compute("max(ndblp)", ""))
                    expto += ((stexp * expTable.Compute("max(ndbs)", "")) / expTable.Compute("max(ndbsp)", ""))
                End If

            End If
            drow(row("entry_date")) = Math.Abs(Math.Round(expto, 2))
            'txttexp.Text = Math.Abs(Math.Round(expto, RoundExpense))
            'txttotexp.Text = Math.Round(val(txtprexp.Text) + val(txttexp.Text), RoundExpense)
            rptTable.Rows.Add(drow)
        Next
        grdtrad.DataSource = rptTable
        Dim i As Integer
        grdtrad.Columns(1).Width = 65
        grdtrad.Columns(3).Width = 30
        grdtrad.Columns(4).Width = 50
        grdtrad.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        For i = 5 To grdtrad.Columns.Count - 1
            grdtrad.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grdtrad.Columns(i).Width = 70
        Next
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try
    End Sub


    ''' <summary>
    ''' Export Grid View Data in the Excel File
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)

        If (EXPORT_IMPORT_POSITION = 2) Then
            If grdtrad.Rows.Count > 0 Then
                Dim savedi As New SaveFileDialog
                savedi.Filter = "Files(*.XLS)|*.XLS"
                If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim grd(0) As DataGridView
                    grd(0) = grdtrad
                    Dim sname(0) As String
                    sname(0) = "expense"
                    exporttoexcel(grd, savedi.FileName, sname, "other")

                    MsgBox("Export Successfully")
                    OPEN_Export_File(savedi.FileName)
                End If
            End If
        ElseIf (EXPORT_IMPORT_POSITION = 1) Then

            Dim savedi As New SaveFileDialog
            savedi.Filter = "File(*.csv)|*.Csv"
            If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim dt As DataTable
                Dim dtgrd As DataTable
                dt = CType(grdtrad.DataSource, DataTable)

                Dim name(dt.Columns.Count) As String


                exporttocsv(dt, savedi.FileName, "other")
                MsgBox("Export Successfully")
                OPEN_Export_File(savedi.FileName)
            End If
        End If


    End Sub

    Private Sub rptExpense_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.F11 Then
            Button1_Click(sender, e)
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class
