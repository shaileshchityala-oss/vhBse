Public Class rptProfitLossSummary
    Dim temptable As New DataTable
    Dim foTable As New DataTable
    Dim eqTable As New DataTable
    'Dim bhavcopy As New DataTable
    'Dim Objbhavcopy As New bhav_copy

    'Dim DTBhavCopy As New DataTable
    Private Sub initTable()
        With temptable.Columns
            .Add("script")
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

            .Add("ltp", GetType(Double))
            .Add("grossmtm", GetType(Double))
            .Add("netmtm", GetType(Double))
            .Add("company")
            .Add("option_type")
        End With
        foTable = GdtFOTrades 'New DataView(GdtFOTrades, "entrydate <= #" & dtp1.Value.Date & "# and entrydate >= #" & dtp2.Value.Date & "#", "entry_date,script", DataViewRowState.CurrentRows).ToTable
        eqTable = GdtEQTrades 'New DataView(GdtEQTrades, "", "entry_date,script", DataViewRowState.CurrentRows).ToTable
        ''divyesh
        ''bhavcopy = Objbhavcopy.select_data()
    End Sub
    Private Sub fill_table()
        temptable.Clear()
        Dim dtScript As New DataTable
        Dim trow As DataRow
        Dim brate As Double

        Dim arr(3) As String
        arr(0) = "script"
        arr(1) = "company"
        'arr(2) = "cp"
        arr(2) = "mdate"
        arr(3) = "StrikeRate"
        'for FO trades
        dtScript = foTable.DefaultView.ToTable(True, arr)
        For Each frow As DataRow In dtScript.Rows
            Dim VarOptionType As String = IIf(UCase(Mid(frow("script"), 1, 3)) = "FUT", "F", UCase(Mid(Strings.Right(frow("script"), 2), 1, 1))) 'mid(frow("script"), frow("script").Len, -2)))
            trow = temptable.NewRow
            trow("script") = frow("script")

            trow("buyqty") = CInt(Val(foTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty>0  And company='" & frow("company") & "'").ToString))
            trow("buyvalue") = CInt(Val(foTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot > 0  And company='" & frow("company") & "'").ToString))
            If trow("buyqty") = 0 Then
                trow("buyrate") = Format(trow("buyvalue"), "#0.00")
            Else
                trow("buyrate") = Format(Val(trow("buyvalue") / trow("buyqty")), "#0.00")
            End If

            trow("sellqty") = CInt(Val(foTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty<0  And company='" & frow("company") & "'").ToString))
            trow("sellvalue") = CInt(Val(foTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot < 0  And company='" & frow("company") & "'").ToString))
            If trow("sellqty") = 0 Then
                trow("sellrate") = Format(trow("sellvalue"), "#0.00")
            Else
                trow("sellrate") = Format(Val(trow("sellvalue") / trow("sellqty")), "#0.00")
            End If

            trow("netqty") = Val(trow("buyqty") + trow("sellqty"))
            trow("netvalue") = Val(trow("buyvalue") + trow("sellvalue"))

            If trow("netqty") = 0 Then
                trow("netrate") = Format(Val(trow("netvalue")), "#0.00")
            Else
                trow("netrate") = Format(Val(trow("netvalue") / trow("netqty")), "#0.00")
            End If


            'expense calculation
            Dim stexp, stexp1, exp As Double
            exp = 0
            If frow("script").ToString.Substring(0, 3) = "FUT" Then
                stexp = Val(foTable.Compute("sum(tot)", "cp not in ('C','P') and script='" & frow("script") & "' and qty > 0  And company='" & frow("company") & "'").ToString)
                stexp1 = Math.Abs(Val(foTable.Compute("sum(tot)", "cp not in ('C','P') and script='" & frow("script") & "' and qty < 0  And company='" & frow("company") & "'").ToString))
                exp += ((stexp * futl) / futlp)
                exp += ((stexp1 * futs) / futsp)
            ElseIf frow("script").ToString.Substring(0, 3) = "OPT" Then
                'Option ####################################################################

                Dim Dr() As DataRow = GdtBhavcopy.Select("script='" & frow("script") & "'")
                If Dr.Length > 0 Then
                    If frow("mdate") <= Dr(0)("entry_date") And Dr(0)("ltp") <> 0 Then
                        If Math.Max(Dr(0)("futval") - Dr(0)("strike"), 0) <> 0 Then
                            trow("expense") = Val(Dr(0)("strike") + (Dr(0)("futval") * (exptable.Rows(0).Item("sttrate")) / 100) * trow("netqty"))
                            GoTo lblexp
                        End If
                    End If
                End If

                If spl <> 0 Then
                    stexp = Val(foTable.Compute("sum(tot)", "cp in ('C','P') and script='" & frow("script") & "' and qty > 0  And company='" & frow("company") & "'").ToString)
                    stexp1 = Math.Abs(Val(foTable.Compute("sum(tot)", "cp  in ('C','P') and script='" & frow("script") & "'  And company='" & frow("company") & "'").ToString))
                    exp += ((stexp * spl) / splp)
                    exp += ((stexp1 * sps) / spsp)
                Else
                    stexp = Val(foTable.Compute("sum(tot)", "cp  in ('C','P') and script='" & frow("script") & "' and qty > 0  And company='" & frow("company") & "'").ToString)
                    stexp1 = Math.Abs(Val(foTable.Compute("sum(tot)", "cp  in ('C','P') and script='" & frow("script") & "' and qty < 0  And company='" & frow("company") & "'").ToString))
                    exp += ((stexp * prel) / prelp)
                    exp += ((stexp1 * pres) / presp)
                End If
            End If
            trow("expense") = Format(exp, "#0.00")
lblexp:
            'divyesh
            'trow("option_type") = frow("cp")
            trow("option_type") = VarOptionType

            Dim VarLTPPrice As Double = 0
            Dim cpfdr() As DataRow = cpfmaster.Select("script='" & trow("script") & "'")
            Dim token_no As Long
            If cpfdr.Length > 0 Then
                token_no = cpfdr(0)("token")
            End If

            If trow("option_type") = "F" Or trow("option_type") = "X" Then
                'LastBhavcopyDate = DTBhavCopy.Compute("MAX(entry_date)", "")
                VarLTPPrice = Format(Val(IIf(fltpprice.Contains(token_no) = True, fltpprice.Item(token_no), 0)), "#0.00")
                If VarLTPPrice = 0 Or GVarIsNewBhavcopy = True Then
                    If GdtBhavcopy.Rows.Count > 0 Then
                        Dim Dr() As DataRow = GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and symbol='" & GetSymbol(frow("company")) & "' and option_type='XX' and exp_date='" & frow("mdate") & "'")
                        If Dr.Length > 0 Then
                            VarLTPPrice = Format(Val(Dr(0)("ltp")), "#0.00")
                        End If
                    Else
                        VarLTPPrice = 0
                    End If
                End If
            ElseIf trow("option_type") = "C" Or trow("option_type") = "P" Then
                VarLTPPrice = Format(IIf(ltpprice.Contains(token_no) = True, ltpprice.Item(token_no), 0), "#0.00")
                If VarLTPPrice = 0 Or GVarIsNewBhavcopy = True Then
                    If GdtBhavcopy.Rows.Count > 0 Then
                        Dim Dr() As DataRow = GdtBhavcopy.Select("Script='" & trow("Script") & "'")
                        'Dim Dr() As DataRow = DTBhavCopy.Select("symbol='" & frow("company") & "' and option_type in (" & IIf(trow("option_type") = "C", "'CE','CA'", "'PE','PA'") & ") AND exp_date='" & frow("mdate") & "'")
                        If Dr.Length > 0 Then
                            If frow("mdate") = Dr(0)("entry_date") Then
                                Dim VarFLTP As Double = 0
                                Dim FDRow() As DataRow = GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and symbol='" & GetSymbol(frow("company")) & "' and option_type='XX' and exp_date='" & frow("mdate") & "'")
                                VarFLTP = FDRow(0)("ltp")
                                If trow("option_type") = "C" Then
                                    VarLTPPrice = Math.Max(VarFLTP - frow("Strikerate"), 0)
                                ElseIf trow("option_type") = "P" Then
                                    VarLTPPrice = Math.Max(frow("Strikerate") - VarFLTP, 0)
                                End If
                            Else
                                VarLTPPrice = Format(Val(Dr(0)("ltp")), "#0.00")
                            End If
                        End If
                    Else
                        VarLTPPrice = 0
                    End If
                End If
            End If

            trow("ltp") = VarLTPPrice
            If trow("netqty") = 0 Then
                trow("grossmtm") = Format(-trow("netvalue"), "#0.00") 'Format(trow("netrate"), "#0.00")
            Else
                trow("grossmtm") = Format(Val(trow("ltp") - trow("netrate")) * trow("netqty"), "#0.00")
            End If
            trow("netmtm") = Format(Val(trow("grossmtm") - trow("expense")), "#0.00")
            temptable.Rows.Add(trow)
        Next

        'For Eq trades
        ReDim arr(2)
        arr(0) = "script"
        arr(1) = "eq"
        arr(2) = "company"
        dtScript = eqTable.DefaultView.ToTable(True, arr)
        For Each frow As DataRow In dtScript.Rows
            trow = temptable.NewRow
            'trow("entry_date") = frow("entry_date")
            trow("script") = frow("script")
            trow("buyqty") = CInt(Val(eqTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty>0 And company='" & frow("company") & "'").ToString))
            brate = Math.Abs(Val(eqTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot > 0  And company='" & frow("company") & "'").ToString))
            If trow("buyqty") = 0 Then
                trow("buyrate") = Format(brate, "#0.00")
            Else
                trow("buyrate") = Format(Val(brate / trow("buyqty")), "#0.00")
            End If
            trow("buyrate") = Format(Math.Round(trow("buyrate"), 2), "#0.00")
            trow("buyvalue") = Format(Val(trow("buyqty") * trow("buyrate")), "#0.00")
            trow("sellqty") = CInt(Val(eqTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty<0  And company='" & frow("company") & "'").ToString))
            brate = Math.Abs(Val(eqTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot < 0  And company='" & frow("company") & "'").ToString))
            If trow("sellqty") = 0 Then
                trow("sellrate") = Format(brate, "#0.00")
            Else
                trow("sellrate") = Format(Val(Math.Abs(brate / trow("sellqty"))), "#0.00")
            End If

            trow("sellvalue") = Format(Val(trow("sellqty") * trow("sellrate")), "#0.00")

            trow("netqty") = Val(trow("buyqty") + trow("sellqty"))
            trow("netvalue") = Format(Val(trow("buyvalue") + trow("sellvalue")), "#0.00")

            If trow("netqty") = 0 Then
                trow("netrate") = Format(Val(trow("netvalue")), "#0.00")
            Else
                trow("netrate") = Format(Val(trow("netvalue") / trow("netqty")), "#0.00")
            End If


            'expense
            Dim stexp, stexp1, exp, ndst, dst As Double
            exp = 0
            stexp = Math.Round(Val(eqTable.Compute("sum(tot)", "script='" & frow("script") & "' and qty > 0  And company='" & frow("company") & "'").ToString), 2)
            stexp1 = Math.Abs(Math.Round(Val(eqTable.Compute("sum(tot)", "script='" & frow("script") & "' and qty < 0  And company='" & frow("company") & "'").ToString), 2))
            dst = stexp - stexp1
            If dst > 0 Then
                ndst = stexp1
                'If CDate(frow("entry_date")) = Now.Date() Then
                '    exp += ((dst * ndbl) / ndblp)
                'Else
                exp += ((dst * dbl) / dblp)
                ' End If
                exp += ((stexp1 * ndbs) / ndbsp)
                exp += ((stexp1 * ndbl) / ndblp)
            Else
                ndst = stexp
                dst = -dst
                exp += ((dst * dbs) / dbsp)
                exp += ((stexp * ndbl) / ndblp)
                exp += ((stexp * ndbs) / ndbsp)
            End If
            trow("expense") = Format(exp, "#0.00")

            'divyesh
            trow("option_type") = frow("eq")

            Dim VarLTPPrice As Double = 0

            Dim Edr() As DataRow = eqmaster.Select("script='" & trow("script") & "'")
            Dim token_no As Long
            If Edr.Length > 0 Then
                token_no = Edr(0)("token")
            End If

            VarLTPPrice = Format(IIf(eltpprice.Contains(token_no) = True, eltpprice.Item(token_no), 0), "#0.00")
            If VarLTPPrice = 0 And GVarIsNewBhavcopy = True Then
                Dim Dr() As DataRow = GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and symbol='" & GetSymbol(frow("company")) & "' and option_type='XX'", "exp_date")
                If Dr.Length > 0 Then
                    VarLTPPrice = Format(Dr(0)("ltp"), "#0.00")
                End If
            End If

            trow("ltp") = VarLTPPrice

            If trow("netqty") = 0 Then
                trow("grossmtm") = Format(-trow("netvalue"), "#0.00")
            Else
                trow("grossmtm") = Format(Val(trow("ltp") - trow("netrate")) * trow("netqty"), "#0.00")
            End If
            trow("netmtm") = Format(Val(trow("grossmtm") - trow("expense")), "#0.00")
            temptable.Rows.Add(trow)
        Next
    End Sub


    Private Sub resultfrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        'If IsDBNull(GdtBhavcopy.Compute("MAX(entry_date)", "")) = False Then
        '    LastBhavcopyDate = GdtBhavcopy.Compute("MAX(entry_date)", "")
        '    DTBhavCopy = New DataView(GdtBhavcopy, "entry_date=#" & LastBhavcopyDate & "#", "", DataViewRowState.CurrentRows).ToTable
        'End If
        'grdtrad.Rows.Add(1)
        initTable()
        fill_table()
        grdtrad.DataSource = temptable
        txtgprofit.Text = Format(IIf(IsDBNull(temptable.Compute("sum(grossmtm)", "")) = False, temptable.Compute("sum(grossmtm)", ""), 0), "#0.00")
        txtnprofit.Text = Format(IIf(IsDBNull(temptable.Compute("sum(netmtm)", "")) = False, temptable.Compute("sum(netmtm)", ""), 0), "#0.00")
        txtexpense.Text = Format(IIf(IsDBNull(temptable.Compute("sum(expense)", "")) = False, temptable.Compute("sum(expense)", ""), 0), "#0.00")


        'dtp1.Value = Now.Date
        'dtp2.Value = Now.Date
        'dtp1.MaxDate = Today.Date
        'dtp2.MaxDate = Today.Date
    End Sub

    Private Sub grdtrad_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)

    End Sub

    Private Sub grdtrad_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'foTable = New DataView(GdtFOTrades, "entry_date >= #" & dtp1.Value.Date & "# and entry_date <= #" & dtp2.Value.Date & "#", "entry_date,script", DataViewRowState.CurrentRows).ToTable
        'eqTable = New DataView(GdtEQTrades, "entry_date >= #" & dtp1.Value.Date & "# and entry_date <= #" & dtp2.Value.Date & "#", "entry_date,script", DataViewRowState.CurrentRows).ToTable
        'fill_table()
        'grdtrad.DataSource = temptable
    End Sub

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
                    sname(0) = "Profit & Loss summary"

                    ''divyesh
                    Dim ArrColList As New ArrayList
                    ArrColList.Add("scrip")
                    ArrColList.Add("buyqty")
                    ArrColList.Add("buyrate")
                    ArrColList.Add("buyvalue")
                    ArrColList.Add("sellqty")
                    ArrColList.Add("sellrate")
                    ArrColList.Add("sellvalue")
                    ArrColList.Add("netqty")
                    ArrColList.Add("netrate")
                    ArrColList.Add("netvalue")
                    ArrColList.Add("ltp")
                    ArrColList.Add("grossmtm")
                    ArrColList.Add("expense")
                    ArrColList.Add("netmtm")
                    Call exporttoexcel(grd, savedi.FileName, sname, "other", ArrColList)
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
                    '.Add("scrip")
                    '.Add("buyqty")
                    '.Add("buyrate")
                    '.Add("buyvalue")
                    '.Add("sellqty")
                    '.Add("sellrate")
                    '.Add("sellvalue")
                    '.Add("netqty")
                    '.Add("netrate")
                    '.Add("netvalue")
                    '.Add("ltp")
                    '.Add("grossmtm")
                    '.Add("expense")
                    '.Add("netmtm")
                    .Add("Script")
                    .Add("Buy Qty", GetType(Integer))
                    .Add("Buy Rate", GetType(Double))
                    .Add("Buy Value", GetType(Double))
                    .Add("Sell Qty", GetType(Integer))
                    .Add("Sell Rate", GetType(Double))
                    .Add("Sell Value", GetType(Double))
                    .Add("Net Qty", GetType(Integer))
                    .Add("Net Rate", GetType(Double))
                    .Add("Net Value", GetType(Double))
                    .Add("LTP", GetType(Double))
                    .Add("Gross Profit", GetType(Double))
                    .Add("Expense", GetType(Double))
                    .Add("Net Profit", GetType(Double))
                End With

                Dim cal As DataRow
                dr = dtgrd.NewRow()
                For Each dr5 As DataRow In dt.Rows
                    cal = dtgrd.NewRow()

                    cal("Script") = dr5("Script")
                    cal("Buy Qty") = dr5("buyqty")
                    cal("Buy Rate") = dr5("buyrate")
                    cal("Buy Value") = dr5("buyvalue")
                    cal("Sell Qty") = dr5("sellqty")
                    cal("Sell Rate") = dr5("sellrate")
                    cal("Sell Value") = dr5("sellvalue")
                    cal("Net Qty") = dr5("netqty")
                    cal("Net Rate") = dr5("netrate")
                    cal("Net Value") = dr5("netvalue")
                    cal("LTP") = dr5("ltp")
                    cal("Gross Profit") = dr5("grossmtm")
                    cal("Expense") = dr5("expense")
                    cal("Net Profit") = dr5("netmtm")

                    dtgrd.Rows.Add(cal)

                    dtgrd.AcceptChanges()

                Next
                exporttocsv(dtgrd, savedi.FileName, "other")
                MsgBox("Export Successfully")
                OPEN_Export_File(savedi.FileName)
            End If
        End If
    End Sub


    Private Sub rptProfitLossSummary_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class