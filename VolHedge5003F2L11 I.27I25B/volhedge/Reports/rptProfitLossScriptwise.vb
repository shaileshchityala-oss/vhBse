Public Class rptProfitLossScriptwise
    Dim temptable As New DataTable
    Dim foTable As New DataTable
    Dim eqTable As New DataTable
    ''' <summary>
    ''' Intialize the Temp Data Table assign value fotable and Eqtable from respectevlly GdtFOTrades and GdtEQTrades
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initTable()
        With temptable.Columns
            .Add("entry_date", GetType(Date))
            .Add("scrip")
            .Add("buyqty", GetType(Integer))
            .Add("buyrate", GetType(Double))
            .Add("buyvalue", GetType(Double))
            .Add("sellqty", GetType(Integer))
            .Add("sellrate", GetType(Double))
            .Add("sellvalue", GetType(Double))
            .Add("netqty", GetType(Integer))
            .Add("netrate", GetType(Double))
            .Add("netvalue", GetType(Double))
            .Add("expense", GetType(Double))
        End With
        foTable = New DataView(GdtFOTrades, "", "entry_date,script,company", DataViewRowState.CurrentRows).ToTable
        eqTable = New DataView(GdtEQTrades, "", "entry_date,script,company", DataViewRowState.CurrentRows).ToTable
    End Sub
    ''' <summary>
    '''  Fill DataTables FOTrades,EQTrades and Expense Calculation..
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fill_table()
        temptable.Clear()
        Dim dtScript As New DataTable
        Dim trow As DataRow
        Dim brate As Double
        Dim arr(2) As String
        arr(0) = "script"
        arr(1) = "entry_date"
        arr(2) = "company"
        'for FO trades
        dtScript = foTable.DefaultView.ToTable(True, arr)
        For Each frow As DataRow In dtScript.Rows
            trow = temptable.NewRow
            trow("entry_date") = frow("entry_date")
            trow("scrip") = frow("script")
            trow("buyqty") = CInt(Val(foTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty>0 and entry_date=# " & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString))
            brate = Math.Abs(Val(foTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot> 0 and entry_date=# " & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString))
            If trow("buyqty") = 0 Then
                trow("buyrate") = brate
            Else
                trow("buyrate") = brate / trow("buyqty")
            End If
            trow("buyrate") = Math.Round(trow("buyrate"), 2)
            trow("buyvalue") = trow("buyqty") * trow("buyrate")

            trow("sellqty") = CInt(Val(foTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty<0 and entry_date=# " & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString))
            brate = Math.Abs(Val(foTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot< 0 and entry_date=# " & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString))
            If trow("sellqty") = 0 Then
                trow("sellrate") = brate
            Else
                trow("sellrate") = Math.Abs(brate / trow("sellqty"))
            End If
            trow("sellvalue") = trow("sellqty") * trow("sellrate")

            trow("netqty") = trow("buyqty") + trow("sellqty")
            trow("netrate") = trow("buyrate") - trow("sellrate")
            trow("netvalue") = trow("netqty") * trow("netrate")

            'expense calculation
            Dim stexp, stexp1, exp As Double
            exp = 0
            If frow("script").ToString.Substring(0, 3) = "FUT" Then
                stexp = Val(foTable.Compute("sum(tot)", "cp not in ('C','P') and script='" & frow("script") & "' and tot > 0 and entry_date = #" & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString)
                stexp1 = Math.Abs(Val(foTable.Compute("sum(tot)", "cp not in ('C','P') and script='" & frow("script") & "' and tot < 0 and entry_date = #" & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString))
                exp += ((stexp * futl) / futlp)
                exp += ((stexp1 * futs) / futsp)
            ElseIf frow("script").ToString.Substring(0, 3) = "OPT" Then
                'Option ####################################################################
                If spl <> 0 Then
                    stexp = Val(foTable.Compute("sum(tot)", "cp in ('C','P') and script='" & frow("script") & "' and tot > 0 and entry_date = #" & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString)
                    stexp1 = Math.Abs(Val(foTable.Compute("sum(tot)", "cp  in ('C','P') and script='" & frow("script") & "' and tot < 0 and entry_date = #" & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString))
                    exp += ((stexp * spl) / splp)
                    exp += ((stexp1 * sps) / spsp)
                Else
                    stexp = Val(foTable.Compute("sum(tot)", "cp  in ('C','P') and script='" & frow("script") & "' and tot > 0 and entry_date = #" & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString)
                    stexp1 = Math.Abs(Val(foTable.Compute("sum(tot)", "cp  in ('C','P') and script='" & frow("script") & "' and tot < 0 and entry_date = #" & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString))
                    exp += ((stexp * prel) / prelp)
                    exp += ((stexp1 * pres) / presp)
                End If
            End If
            trow("expense") = exp
            temptable.Rows.Add(trow)
        Next

        'For Eq trades
        dtScript = eqTable.DefaultView.ToTable(True, arr)
        For Each frow As DataRow In dtScript.Rows
            trow = temptable.NewRow
            trow("entry_date") = frow("entry_date")
            trow("scrip") = frow("script")
            trow("buyqty") = CInt(Val(eqTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty>0 and entry_date=# " & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString))
            brate = Math.Abs(Val(eqTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot> 0 and entry_date=# " & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString))
            If trow("buyqty") = 0 Then
                trow("buyrate") = brate
            Else
                trow("buyrate") = brate / trow("buyqty")
            End If
            trow("buyrate") = Math.Round(trow("buyrate"), 2)
            trow("buyvalue") = trow("buyqty") * trow("buyrate")
            trow("sellqty") = CInt(Val(eqTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty<0 and entry_date=# " & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString))
            brate = Math.Abs(Val(eqTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot< 0 and entry_date=# " & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString))
            If trow("sellqty") = 0 Then
                trow("sellrate") = brate
            Else
                trow("sellrate") = Math.Abs(brate / trow("sellqty"))
            End If
            trow("sellvalue") = trow("sellqty") * trow("sellrate")
            trow("netqty") = trow("buyqty") + trow("sellqty")
            trow("netrate") = trow("buyrate") - trow("sellrate")
            trow("netvalue") = trow("netqty") * trow("netrate")
            'expense
            Dim stexp, stexp1, exp, ndst, dst As Double
            exp = 0
            stexp = Math.Round(Val(eqTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot > 0 and entry_date = #" & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString), 2)
            stexp1 = Math.Abs(Math.Round(Val(eqTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot < 0 and entry_date = #" & fDate(frow("entry_date")) & "# And company='" & frow("company") & "'").ToString), 2))
            dst = stexp - stexp1
            If dst > 0 Then
                ndst = stexp1
                'If CDate(frow("entry_date")) = Now.Date() Then
                '    exp += ((dst * ndbl) / ndblp)
                'Else
                exp += ((dst * dbl) / dblp)
                'End If
                exp += ((stexp1 * ndbs) / ndbsp)
                exp += ((stexp1 * ndbl) / ndblp)
            Else
                ndst = stexp
                dst = -dst
                exp += ((dst * dbs) / dbsp)
                exp += ((stexp * ndbl) / ndblp)
                exp += ((stexp * ndbs) / ndbsp)
            End If
            trow("expense") = exp
            temptable.Rows.Add(trow)
        Next
    End Sub
    ''' <summary>
    ''' call InitTable Methode
    ''' call Fill_table  methode
    ''' assign Temptable data to Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub resultfrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        initTable()
        fill_table()
        grdtrad.DataSource = temptable
    End Sub

    Private Sub grdtrad_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)

    End Sub

    Private Sub grdtrad_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    ''' <summary>
    ''' datasouce assign to dataGrid View after filtering Data by Entry Date and Scrip for FO and EQ Trades...
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If dtp1.Value.Date > dtp2.Value.Date Then
            MsgBox("Invalid Date Selected.", MsgBoxStyle.Information)
            dtp2.Focus()
            Exit Sub
        End If
        foTable = New DataView(GdtFOTrades, "entry_date >= #" & fDate(dtp1.Value.Date) & "# and entry_date <= #" & fDate(dtp2.Value.Date) & "#", "entry_date,script,company", DataViewRowState.CurrentRows).ToTable
        eqTable = New DataView(GdtEQTrades, "entry_date >= #" & fDate(dtp1.Value.Date) & "# and entry_date <= #" & fDate(dtp2.Value.Date) & "#", "entry_date,script,company", DataViewRowState.CurrentRows).ToTable

        fill_table()
        grdtrad.DataSource = temptable
    End Sub

    ''' <summary>
    ''' Export Data in the Excel File Of Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)

        If (EXPORT_IMPORT_POSITION = 2) Then
            If grdtrad.Rows.Count > 0 Then
                Dim savedi As New SaveFileDialog
                savedi.Filter = "Files(*.XLS)|*.XLS"
                If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim grd(0) As DataGridView
                    grd(0) = grdtrad
                    Dim sname(0) As String
                    sname(0) = "Profit & Loss ScripWise"
                    exporttoexcel(grd, savedi.FileName, sname, "other")
                    MsgBox("Export Completed.")
                    OPEN_Export_File(savedi.FileName)
                End If
            End If
        ElseIf (EXPORT_IMPORT_POSITION = 1) Then

            Dim savedi As New SaveFileDialog
            savedi.Filter = "File(*.csv)|*.Csv"
            If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim dt As DataTable
                dt = CType(grdtrad.DataSource, DataTable)
                Dim dtgrd As DataTable
                Dim name(dt.Columns.Count) As String


                Dim dr As DataRow
                dtgrd = New DataTable
                With dtgrd.Columns
                    .Add("Date", GetType(Date))
                    .Add("scrip")
                    .Add("Buy Qty", GetType(Integer))
                    .Add("Buy Rate", GetType(Double))
                    .Add("Buy Value", GetType(Double))
                    .Add("Sell Qty", GetType(Integer))
                    .Add("Sell Rate", GetType(Double))
                    .Add("Sell Value", GetType(Double))
                    .Add("Net Qty", GetType(Integer))
                    .Add("Net Rate", GetType(Double))
                    .Add("Net Value", GetType(Double))
                    .Add("expense", GetType(Double))
                End With

                Dim cal As DataRow
                dr = dtgrd.NewRow()
                For Each dr5 As DataRow In dt.Rows
                    cal = dtgrd.NewRow()

                    cal("Date") = dr5("entry_date")
                    cal("scrip") = dr5("scrip")
                    cal("Buy Qty") = dr5("buyqty")
                    cal("Buy Rate") = dr5("buyrate")
                    cal("Buy Value") = dr5("buyvalue")
                    cal("Sell Qty") = dr5("sellqty")
                    cal("Sell Rate") = dr5("sellrate")
                    cal("Sell Value") = dr5("sellvalue")
                    cal("Net Qty") = dr5("netqty")
                    cal("Net Rate") = dr5("netrate")
                    cal("Net Value") = dr5("netvalue")
                    cal("expense") = dr5("expense")

                    dtgrd.Rows.Add(cal)

                    dtgrd.AcceptChanges()

                Next
                exporttocsv(dtgrd, savedi.FileName, "other")
                MsgBox("Export Successfully")
                OPEN_Export_File(savedi.FileName)
            End If
        End If
    End Sub
  
    Private Sub rptProfitLossScriptwise_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If


    End Sub
End Class