Public Class displaytrans
    Dim dtable As DataTable = New DataTable
    Dim objTrad As trading = New trading
    Dim scripttable As New DataTable
    Dim objScr As script = New script
    Public token As Long
    Public script As String
    Public cpfe As String
    Public VarScriptType As String 'CURRENCY,FO,EQ
    Public token1 As Long
    Public instrumentname As String
    Public company As String
    Public mdate As Date
    Public strikerate As Double
    Public isliq As Boolean
    Public obj As Object
    Dim count1 As New ArrayList
    Dim objExp As New expenses
    Public StrategyName As String = ""

    Dim objAna As New analysisprocess
    Private Sub init_table()
        dtable = New DataTable
        With dtable.Columns
            .Add("Qty", GetType(Double))
            .Add("Rate", GetType(Double))
            .Add("entrydate", GetType(Date))
            .Add("entryno", GetType(Integer))
        End With
    End Sub

    Private Sub displaytrans_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub
    Private Sub displaytrans_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtp_EntryDate.MaxDate = Now
        OPPOS_ENTRYDATE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='OPPOS_ENTRYDATE'").ToString)
        If OPPOS_ENTRYDATE = 1 Then
            dtp_EntryDate.Value = CDate(Now.Date)
        Else
            dtp_EntryDate.Value = CDate(DateAdd(DateInterval.Day, -1, Now.Date))
        End If
        Call process_data()
        count1 = New ArrayList
        If VarScriptType = "CURRENCY" Then
            DGdisplayTrades.Columns("Rate").DefaultCellStyle.Format = CurrencyNetPriceStr
        Else
            DGdisplayTrades.Columns("Rate").DefaultCellStyle.Format = "#0.00"
        End If

        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub

    Private Sub process_data()
        Dim table As New DataTable
        'GdtFOTrades = objTrad.Trading
        'GdtEQTrades = objTrad.select_equity
        If VarScriptType = "EQ" Then
            ' GdtEQTrades = objTrad.select_equity
            table = New DataView(GdtEQTrades).ToTable 'objTrad.select_equity
            '  cmdsave.Enabled = False
        ElseIf VarScriptType = "CURRENCY" Then
            '   GdtCurrencyTrades = objTrad.select_Currency_Trading
            table = New DataView(GdtCurrencyTrades).ToTable 'objTrad.select_equity
            Varmultiplier = Currencymaster.Compute("MAX(multiplier)", "Script='" & script & "'")
        ElseIf VarScriptType = "FO" Then
            '  GdtFOTrades = objTrad.Trading
            table = New DataView(GdtFOTrades).ToTable 'objTrad.Trading
            Varlotsize = cpfmaster.Compute("MAX(lotsize)", "script = '" & script & "'")
        End If
        cmdsave.Enabled = True
        'scripttable = New DataTable
        'scripttable = objTrad.Script
        lbltoken.Text = token
        lblscript.Text = script
        'lblscript.Text = objTrad.Script.Compute("max(script)", "token=" & CLng(token) & "")
        Dim VarCondition As String = ""

        VarCondition = "script='" & lblscript.Text & "' And company='" & company & "'"
        If UCase(StrategyName) <> "TOTAL" And IsDate(StrategyName) = False And StrategyName <> "" Then
            VarCondition &= " AND Strategy_Name='" & StrategyName & "' "
        End If

        Dim dv As DataView = New DataView(table, VarCondition, "entrydate DESC", DataViewRowState.CurrentRows)
        Dim str(5) As String
        str(0) = "qty"
        str(1) = "rate"
        str(2) = "entrydate"
        str(3) = "entryno"
        str(4) = "uid"
        str(5) = "Dealer"
        dtable = dv.ToTable(False, str)
        If VarScriptType = "CURRENCY" Then
            dtable.Columns.Add("Lots", GetType(Double))
            For Each Dr As DataRow In dtable.Rows
                Dr("Lots") = Dr("Qty") / Varmultiplier
            Next
        ElseIf VarScriptType = "FO" Then
            dtable.Columns.Add("Lots", GetType(Double))
            For Each Dr As DataRow In dtable.Rows
                Dr("Lots") = Dr("Qty") / Varlotsize
            Next
        Else
            DGdisplayTrades.Columns("Lots").Visible = False
        End If
        DGdisplayTrades.DataSource = dtable
    End Sub
    Dim Varmultiplier As Double
    Dim Varlotsize As Double
    '50051E
    '--------------------------------------
    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        If AppLicMode = "SERLIC" Then
            If bool_IsServerConnected = False Then Exit Sub
        End If

        If Val(txtunit.Text) = 0 Then
            MsgBox("Enter Units!!", MsgBoxStyle.Exclamation)
            txtunit.Focus()
            Exit Sub
        End If
        If Val(txtrate.Text) = 0 Then
            MsgBox("Enter Rate!!", MsgBoxStyle.Exclamation)
            txtrate.Focus()
            Exit Sub
        End If
        If VarScriptType = "EQ" Then
            GVarMAXEQTradingOrderNo = GVarMAXEQTradingOrderNo + 1
            objScr.Company = company
            objScr.Script = script.Trim
            objScr.CP = instrumentname
            objScr.Units = Val(txtunit.Text)
            objScr.Rate = Val(txtrate.Text)
            objScr.EntryDate = CDate(dtp_EntryDate.Value.Date.ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt")) 'dtp_EntryDate.Value.Date
            'objscr.orderno =0
            objScr.orderno = GVarMAXEQTradingOrderNo
            objScr.Dealer = "OP"
            'insert EQ trade to database
            objScr.insert_equity()
            'select uid of currenlt inserted trade

            Dim dtEQuid As New DataTable
            dtEQuid = objScr.select_equity_uid()

            REM 2 :insert Eq trade to globle dtEqtrades datatable
            Dim DtTempEQ_trad As New DataTable
            DtTempEQ_trad = GdtEQTrades.Clone

            Dim temprow As DataRow = DtTempEQ_trad.NewRow
            temprow("uid") = Val(dtEQuid.Rows(0)("uid").ToString)
            temprow("script") = script.Trim
            temprow("company") = company
            temprow("eq") = UCase(cpfe) 'Val(txt.Text)
            temprow("qty") = Val(txtunit.Text)
            temprow("rate") = Val(txtrate.Text)
            temprow("entrydate") = CDate(dtp_EntryDate.Value.Date.ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt")) 'dtp_EntryDate.Value.Date
            temprow("entry_date") = Format(dtp_EntryDate.Value.Date, "MMM/dd/yyyy")
            temprow("tot") = Val(txtunit.Text) * Val(txtrate.Text)
            temprow("tot2") = Val(txtunit.Text) * Val(txtrate.Text)
            temprow("entryno") = 0
            temprow("orderno") = GVarMAXEQTradingOrderNo
            DtTempEQ_trad.Rows.Add(temprow)

            Call insert_EQTradeToGlobalTable(DtTempEQ_trad)
            GdtEQTrades.Rows(GdtEQTrades.Rows.Count - 1).Item("Uid") = temprow("uid")
            Call GSub_CalculateExpense(DtTempEQ_trad, "EQ", True)
            objTrad.Delete_Expense_Data_All()
            objTrad.Insert_Expense_Data(G_DTExpenseData)
            REM 2  END



            ' If dteqent.Value.Date < Now.Date Then
            Dim prExp, toExp As Double
            'caluclate expense of inserted position
            'cal_prebal(dtp_EntryDate.Value.Date, company, "E", Val(txtunit.Text), Val(txtrate.Text), prExp, toExp)

            'insert position to database's analysis table also
            Dim dtAna As New DataTable
            dtAna = objAna.fill_equity_process(UCase(script.Trim), CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp, dtp_EntryDate.Value.Date, company)



            '***********************************************************************************
            'insert EQ trade to analysis table
            objScr.insert_EQTrade_in_maintable(script.Trim, dtAna, prExp, toExp, dtp_EntryDate.Value.Date, company)
        ElseIf VarScriptType = "CURRENCY" Then
            GVarMAXCURRTradingOrderNo = GVarMAXCURRTradingOrderNo + 1
            objScr.InstrumentName = instrumentname
            objScr.Company = company
            objScr.Mdate = CDate(mdate)
            objScr.StrikeRate = Val(strikerate)
            If UCase(cpfe) = "F" Then
                objScr.CP = "F"
            Else
                objScr.CP = UCase(cpfe)
            End If
            objScr.Script = script.Trim
            objScr.Units = Val(txtunit.Text) * Varmultiplier
            objScr.Rate = Val(txtrate.Text)
            objScr.EntryDate = CDate(dtp_EntryDate.Value.Date.ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt")) 'dtp_EntryDate.Value.Date
            objScr.Token = token1
            objScr.Isliq = isliq ' "Yes"
            objScr.Dealer = "OP"
            'objScr.orderno = 0
            objScr.orderno = GVarMAXCURRTradingOrderNo
            'insert FO trade to database
            objScr.Insert_Currency_Trading()
            'select uid of currantly inserted trade
            Dim dtCurrUid As New DataTable
            dtCurrUid = objScr.select_Currency_trading_uid
            '*****************************************************8
            'insert FO trade to global datatable
            Dim DtTempCurr_trad As New DataTable
            DtTempCurr_trad = GdtCurrencyTrades.Clone


            Dim temprow As DataRow = DtTempCurr_trad.NewRow
            temprow("uid") = Val(dtCurrUid.Rows(0)("uid").ToString)
            temprow("entryno") = 0
            temprow("instrumentname") = instrumentname
            temprow("company") = company
            temprow("mdate") = CDate(mdate)
            temprow("strikerate") = Val(strikerate)
            If UCase(cpfe) = "F" Then
                temprow("cp") = "F"
            Else
                temprow("cp") = UCase(cpfe)
            End If
            temprow("qty") = Val(txtunit.Text) * Varmultiplier
            temprow("rate") = Val(txtrate.Text)
            temprow("entrydate") = CDate(dtp_EntryDate.Value.Date.ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt")) 'dtp_EntryDate.Value.Date
            temprow("entry_date") = Format(dtp_EntryDate.Value.Date, "MMM/dd/yyyy")
            temprow("script") = script.Trim
            temprow("token1") = token1
            temprow("isliq") = isliq
            temprow("orderno") = GVarMAXCURRTradingOrderNo
            temprow("lActivityTime") = 0
            temprow("FileFlag") = ""
            temprow("tot") = Val(txtunit.Text) * Val(txtrate.Text) * Varmultiplier
            temprow("tot2") = Val(txtunit.Text) * ((Val(txtrate.Text) * Varmultiplier) + Val(strikerate))
            DtTempCurr_trad.Rows.Add(temprow)

            Call insert_CurrencyTradeToGlobalTable(DtTempCurr_trad)
            GdtCurrencyTrades.Rows(GdtCurrencyTrades.Rows.Count - 1).Item("Uid") = temprow("uid")
            Call GSub_CalculateExpense(DtTempCurr_trad, "CURR", True)
            objTrad.Delete_Expense_Data_All()
            objTrad.Insert_Expense_Data(G_DTExpenseData)
            '*****************************************************

            'caluclate expense of inserted position
            Dim prExp, toExp As Double
            Dim optype As String = "C" & temprow("cp")
            'Call cal_prebal(dtp_EntryDate.Value.Date, company, optype, CInt(txtunit.Text) * Varmultiplier, Val(txtrate.Text), prExp, toExp)

            'insert position to database's analysis table also
            Dim dtAna As New DataTable
            dtAna = objAna.fill_table_process(script.Trim, CInt(txtunit.Text) * Varmultiplier, Val(txtrate.Text), prExp, toExp, dtp_EntryDate.Value.Date, company)

            'insert FO trade to analysis table
            objScr.insert_CurrencyTrade_in_maintable(script.Trim, dtAna, prExp, toExp, dtp_EntryDate.Value.Date, company)

        ElseIf VarScriptType = "FO" Then
            GVarMAXFOTradingOrderNo = GVarMAXFOTradingOrderNo + 1
            objScr.InstrumentName = instrumentname
            objScr.Company = company
            objScr.Mdate = CDate(mdate)
            objScr.StrikeRate = Val(strikerate)
            If UCase(cpfe) = "F" Then
                objScr.CP = "F"
            Else
                objScr.CP = UCase(cpfe)

            End If
            objScr.Script = script.Trim
            objScr.Units = Val(txtunit.Text)
            objScr.Rate = Val(txtrate.Text)
            objScr.EntryDate = CDate(dtp_EntryDate.Value.Date.ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt")) 'dtp_EntryDate.Value.Date
            objScr.Token = token1
            objScr.Isliq = isliq ' "Yes"
            objScr.Dealer = "OP"
            'objScr.orderno = 0

            objScr.orderno = GVarMAXFOTradingOrderNo

            'insert FO trade to database
            objScr.Insert()
            'select uid of currantly inserted trade
            Dim dtEQUid As New DataTable
            dtEQUid = objScr.select_trading_uid
            '*****************************************************8
            'insert FO trade to global datatable
            Dim DtTempFO_trad As New DataTable
            DtTempFO_trad = GdtFOTrades.Clone


            Dim temprow As DataRow
            temprow = DtTempFO_trad.NewRow
            temprow("uid") = Val(dtEQUid.Rows(0)("uid").ToString)
            temprow("entryno") = 0
            temprow("instrumentname") = instrumentname
            temprow("company") = company
            temprow("mdate") = CDate(mdate)
            temprow("strikerate") = Val(strikerate)
            If UCase(cpfe) = "F" Then
                temprow("cp") = "F"
            Else
                temprow("cp") = UCase(cpfe)
            End If
            temprow("qty") = Val(txtunit.Text)
            temprow("rate") = Val(txtrate.Text)
            temprow("entrydate") = CDate(dtp_EntryDate.Value.Date.ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt")) 'dtp_EntryDate.Value.Date
            temprow("entry_date") = Format(dtp_EntryDate.Value.Date, "MMM/dd/yyyy")
            temprow("script") = script.Trim
            temprow("token1") = token1
            temprow("isliq") = isliq
            temprow("orderno") = GVarMAXFOTradingOrderNo
            temprow("lActivityTime") = 0
            temprow("FileFlag") = ""
            temprow("tot") = Val(txtunit.Text) * Val(txtrate.Text)
            temprow("tot2") = Val(txtunit.Text) * (Val(txtrate.Text) + Val(strikerate))
            DtTempFO_trad.Rows.Add(temprow)
            Call insert_FOTradeToGlobalTable(DtTempFO_trad)
            GdtFOTrades.Rows(GdtFOTrades.Rows.Count - 1).Item("Uid") = temprow("uid")
            Call GSub_CalculateExpense(DtTempFO_trad, "FO", True)
            objTrad.Delete_Expense_Data_All()
            objTrad.Insert_Expense_Data(G_DTExpenseData)
            '*****************************************************8
            'caluclate expense of inserted position
            Dim prExp, toExp As Double

            'cal_prebal(dtp_EntryDate.Value.Date, company, temprow("cp"), CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp)


            'insert position to database's analysis table also
            Dim dtAna As New DataTable
            dtAna = objAna.fill_table_process(script.Trim, CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp, dtp_EntryDate.Value.Date, company)

            'insert FO trade to analysis table
            objScr.insert_FOTrade_in_maintable(script.Trim, dtAna, prExp, toExp, dtp_EntryDate.Value.Date, company)

        End If

        Call process_data()
        txtunit.Text = "0"
        txtrate.Text = "0"
        count1.Add(True)
        'If dtdate.Value.Date < Now.Date Then
        '    cal_prebal(dtdate.Value.Date, company)

        'End If
        'Viral_2017: Rem because of Very Slow... =======================
        'Dim objanalysis As New analysis
        'objanalysis.fill_tabpages()
        '=============================================
    End Sub

    Private Sub displaytrans_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If count1.Count > 0 Then
            ' Viral_2017 System.Threading.Thread.Sleep(400)
            System.Threading.Thread.Sleep(100)
            REM Refreshing Global Database 
            'Call GSub_Fill_GDt_AllTrades()
            'Call GSub_Fill_GDt_Strategy()
            REM End
            obj.chkpro = True
            'ana.Dispose()
        End If
        Call analysis.searchcompany()
        If GdtFOTrades.Rows.Count = 0 And GdtEQTrades.Rows.Count = 0 And GdtCurrencyTrades.Rows.Count = 0 Then
            MDI.ToolStripcompanyCombo.Visible = False
            MDI.ToolStripMenuSearchComp.Visible = False
        End If
    End Sub
    '50051E 
    '--------------------------------------------------------
    Private Sub grddisplay_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGdisplayTrades.KeyDown
        Try

        
        Dim dtAna As New DataTable
        If e.KeyCode = Keys.Delete Then
            With DGdisplayTrades.CurrentRow

                If Val(DGdisplayTrades.CurrentRow.Cells("entryno").Value) = 0 Then
                    If MsgBox("Are you sure to delete selected script?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                        If VarScriptType = "EQ" Then
                            objScr.Delete_eqtrad(script, Val(.Cells("uid").Value))
                            Dim prExp, toExp As Double
                            'calculate expense of inserted position

                            Dim drow As DataRow()
                            REM 1: delete trade from global dtEqtrade datatable
                            drow = GdtEQTrades.Select("uid = " & Val(.Cells("uid").Value), "")

                            Dim dtTemp_EQTrad As New DataTable
                            dtTemp_EQTrad = GdtEQTrades.Clone
                            dtTemp_EQTrad.ImportRow(drow(0))

                            GdtEQTrades.Rows.Remove(drow(0))
                            GdtEQTrades.AcceptChanges()
                            REM 1: END

                            REM Calc. Expense
                            Call GSub_CalculateExpense(dtTemp_EQTrad, "EQ", False)
                            objTrad.Delete_Expense_Data_All()
                            objTrad.Insert_Expense_Data(G_DTExpenseData)
                            REM End


                            ' If dteqent.Value.Date < Now.Date Then
                            'insert position to database's analysis table also
                            dtAna = objAna.fill_equity_process(UCase(script.Trim), -CInt(.Cells("qty").Value), Val(.Cells("rate").Value), prExp, toExp, CDate(.Cells("entrydate").Value), company)
                            '***********************************************************************************
                            'insert EQ trade to analysis table
                            If dtAna.Rows.Count > 0 Then
                                objScr.insert_EQTrade_in_maintable(script.Trim, dtAna, prExp, toExp, CDate(.Cells("entrydate").Value), company)
                            Else
                                Dim mrow As DataRow()
                                mrow = maintable.Select("script = '" & script & "' And company='" & company & "'", "")
                                maintable.Rows.Remove(mrow(0))
                            End If
                        ElseIf VarScriptType = "FO" Then
                            objScr.Delete_trad(script, Val(DGdisplayTrades.CurrentRow.Cells("uid").Value))
                            Dim drow As DataRow()
                            'calulate exp of trade to be deleted
                            Dim prExp, toExp As Double
                            'cal_prebal(CDate(.Cells("entrydate").Value), company, cpfe, Val(.Cells("qty").Value), -Val(.Cells("rate").Value), prExp, toExp)

                            REM 1: delete trade from global dtFOtrade datatable
                            drow = GdtFOTrades.Select("uid = " & Val(.Cells("uid").Value), "")
                              
                            Dim dtTemp_FOTrad As New DataTable
                            dtTemp_FOTrad = GdtFOTrades.Clone
                            dtTemp_FOTrad.ImportRow(drow(0))


                            GdtFOTrades.Rows.Remove(drow(0))
                            GdtFOTrades.AcceptChanges()

                            REM 1: END

                            REM Calc. Expense
                            Call GSub_CalculateExpense(dtTemp_FOTrad, "FO", False)
                            objTrad.Delete_Expense_Data_All()
                            objTrad.Insert_Expense_Data(G_DTExpenseData)
                            REM End

                            'insert position to database's analysis table also
                            dtAna = objAna.fill_table_process(script.Trim, -CInt(.Cells("qty").Value), Val(.Cells("rate").Value), prExp, toExp, CDate(.Cells("entrydate").Value), company)
                            'caluclate expense of inserted position
                            'insert FO trade to analysis table
                            If dtAna.Rows.Count > 0 Then
                                objScr.insert_FOTrade_in_maintable(script.Trim, dtAna, prExp, toExp, CDate(.Cells("entrydate").Value), company)
                            Else
                                Dim mrow As DataRow()
                                    mrow = maintable.Select("script = '" & script & "' And company='" & company & "'", "")
                                    If NetMode = "API" Then
                                        Dim str As String = HT_GetIdentifierFromTokan(CLng(mrow(0).Item("Tokanno")))
                                        UnRegIdentifier(str)
                                    End If
                                   
                                maintable.Rows.Remove(mrow(0))
                            End If
                            REM 2:recalculate position in analysis table and store to database
                        ElseIf VarScriptType = "CURRENCY" Then
                            objScr.Delete_Currency_Trading_byUID(script, Val(DGdisplayTrades.CurrentRow.Cells("uid").Value))
                            Dim drow As DataRow()
                            'calulate exp of trade to be deleted
                            Dim prExp, toExp As Double
                            'Call cal_prebal(CDate(.Cells("entrydate").Value), company, "C" & cpfe, Val(.Cells("qty").Value), -Val(.Cells("rate").Value), prExp, toExp)

                            REM 1: delete trade from global dtFOtrade datatable
                            drow = GdtCurrencyTrades.Select("uid = " & Val(.Cells("uid").Value), "")

                            Dim dtTemp_CurrTrad As New DataTable
                            dtTemp_CurrTrad = GdtCurrencyTrades.Clone
                            dtTemp_CurrTrad.ImportRow(drow(0))


                            GdtCurrencyTrades.Rows.Remove(drow(0))
                            GdtCurrencyTrades.AcceptChanges()
                            REM 1: END

                            REM Calc. Expense
                            Call GSub_CalculateExpense(dtTemp_CurrTrad, "CURR", False)
                            objTrad.Delete_Expense_Data_All()
                            objTrad.Insert_Expense_Data(G_DTExpenseData)
                            REM End

                            'insert position to database's analysis table also
                            dtAna = objAna.fill_table_process(script.Trim, -CInt(.Cells("qty").Value), Val(.Cells("rate").Value), prExp, toExp, CDate(.Cells("entrydate").Value), company)
                            'caluclate expense of inserted position
                            'insert FO trade to analysis table
                            If dtAna.Rows.Count > 0 Then
                                objScr.insert_CurrencyTrade_in_maintable(script.Trim, dtAna, prExp, toExp, CDate(.Cells("entrydate").Value), company)
                            Else
                                Dim mrow As DataRow()
                                mrow = maintable.Select("script = '" & script & "' And company='" & company & "'", "")
                                maintable.Rows.Remove(mrow(0))
                            End If
                            REM 2:recalculate position in analysis table and store to database
                        End If
                        Threading.Thread.Sleep(500)
                        ' cal_prebal(CDate(.Cells(2).Value), company)

                        Call process_data()
                        count1.Add(True)
                    End If
                End If
            End With

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub displaytrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    'Private Sub cal_prebal(ByVal date1 As Date, ByVal compname As String)
    '    Dim addprebal As New DataTable
    '    addprebal = New DataTable
    '    With addprebal.Columns
    '        .Add("tdate", GetType(Date))
    '        .Add("stbal", GetType(Double))
    '        .Add("futbal", GetType(Double))
    '        .Add("optbal", GetType(Double))
    '        .Add("company", GetType(String))
    '    End With
    '    Dim prow As DataRow
    '    Dim cpf As New DataTable
    '    Dim stk As New DataTable
    '    Dim exptable As New DataTable
    '    Dim company As New DataTable
    '    exptable = objExp.select_exp
    '    company = objTrad.Comapany
    '    cpf = dtFOTrades ' objTrad.Trading
    '    stk = dtEQTrades ' objTrad.select_equity

    '    Dim dv As DataView = New DataView(cpf)
    '    dv.RowFilter = "entry_date = #" & date1.Date & "#"
    '    Dim dv1 As DataView = New DataView(stk)
    '    dv1.RowFilter = "entry_date = #" & date1.Date & "#"
    '    Dim stexp, stexp1, ndst, dst, exppr, expto As Double

    '    For Each crow As DataRow In company.Select("company='" & compname & "'")
    '        dv.RowFilter = " entry_date = #" & date1.Date & "# and company='" & compname & "'"
    '        dv.Sort = "entry_date"
    '        Dim ttable As New DataTable
    '        ttable = dv.ToTable(True, "entry_date")

    '        For Each row As DataRow In ttable.Rows
    '            prow = addprebal.NewRow
    '            prow("tdate") = CDate(row("entry_date")).Date
    '            prow("stbal") = 0
    '            prow("futbal") = 0
    '            prow("optbal") = 0
    '            prow("company") = compname
    '            addprebal.Rows.Add(prow)
    '            stexp = 0
    '            stexp1 = 0
    '            dst = 0
    '            ndst = 0
    '            exppr = 0
    '            expto = 0
    '            'Equity ##################################################################
    '            stexp = Math.Round(Val(IIf(Not IsDBNull(stk.Compute("sum(tot)", "company='" & compname & "' and qty > 0 and entry_date = #" & CDate(row("entry_date")).Date & "#")), stk.Compute("sum(tot)", "company='" & compname & "' and qty > 0 and entry_date = #" & CDate(row("entry_date")).Date & "#"), 0)), 2)
    '            stexp1 = Math.Abs(Math.Round(Val(IIf(Not IsDBNull(stk.Compute("sum(tot)", "company='" & compname & "' and qty < 0 and entry_date = #" & CDate(row("entry_date")).Date & "#")), stk.Compute("sum(tot)", "company='" & compname & "' and qty < 0 and entry_date = #" & CDate(row("entry_date")).Date & "#"), 0)), 2))
    '            dst = stexp - stexp1
    '            If dst > 0 Then
    '                ndst = stexp1
    '                prow("stbal") = Val(prow("stbal")) + Val((dst * exptable.Compute("max(dbl)", "")) / exptable.Compute("max(dblp)", "")) + Val((stexp1 * exptable.Compute("max(ndbs)", "")) / exptable.Compute("max(ndbsp)", "")) + Val((stexp * exptable.Compute("max(ndbl)", "")) / exptable.Compute("max(ndblp)", ""))
    '            Else
    '                ndst = stexp
    '                prow("stbal") = Val(prow("stbal")) + Val((dst * exptable.Compute("max(dbs)", "")) / exptable.Compute("max(dbsp)", "")) + Val((stexp * exptable.Compute("max(ndbl)", "")) / exptable.Compute("max(ndblp)", "")) + Val((stexp1 * exptable.Compute("max(ndbs)", "")) / exptable.Compute("max(ndbsp)", ""))
    '            End If

    '            'Futre #################################################################
    '            stexp = 0
    '            stexp1 = 0

    '            stexp = Val(IIf(Not IsDBNull(cpf.Compute("sum(tot)", "cp not in ('C','P') and company='" & compname & "' and qty > 0 and entry_date = #" & CDate(row("entry_date")).Date & "#")), cpf.Compute("sum(tot)", "cp not in ('C','P') and company='" & compname & "'  and qty > 0 and entry_date = #" & CDate(row("entry_date")).Date & "#"), 0))
    '            stexp1 = Math.Abs(Val(IIf(Not IsDBNull(cpf.Compute("sum(tot)", "cp not in ('C','P') and company='" & compname & "' and qty < 0 and entry_date = #" & CDate(row("entry_date")).Date & "#")), cpf.Compute("sum(tot)", "cp not in ('C','P') and company='" & compname & "'  and qty < 0 and entry_date = #" & CDate(row("entry_date")).Date & "#"), 0)))

    '            prow("futbal") = Val(prow("futbal")) + Val((stexp * exptable.Compute("max(futl)", "")) / exptable.Compute("max(futlp)", "")) + Val((stexp1 * exptable.Compute("max(futs)", "")) / exptable.Compute("max(futsp)", ""))
    '            'Option ####################################################################
    '            stexp = 0
    '            stexp1 = 0
    '            stexp = Val(IIf(Not IsDBNull(cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "' and qty > 0 and entry_date = #" & CDate(row("entry_date")).Date & "#")), cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "'  and qty > 0 and entry_date = #" & CDate(row("entry_date")).Date & "#"), 0))
    '            stexp1 = Math.Abs(Val(IIf(Not IsDBNull(cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "' and qty < 0 and entry_date = #" & CDate(row("entry_date")).Date & "#")), cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "'  and qty < 0 and entry_date = #" & CDate(row("entry_date")).Date & "#"), 0)))


    '            If Val(exptable.Compute("max(spl)", "")) <> 0 Then
    '                prow("optbal") = Val(prow("optbal")) + Val((stexp * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", "")) + Val((stexp1 * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", ""))
    '            Else
    '                prow("optbal") = Val(prow("optbal")) + Val((stexp * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", "")) + Val((stexp1 * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", ""))
    '            End If

    '        Next
    '    Next
    '    objTrad.Delete_prBal(date1.Date, compname)
    '    objTrad.insert_prebal(addprebal)
    'End Sub
    Private Sub cal_prebal(ByVal date1 As Date, ByVal compname As String, ByVal optype As String, ByVal qty As Integer, ByVal rate As Double, ByRef prExp As Double, ByRef toExp As Double, ByRef StrikeRate As Double)
        Dim flgNew As Boolean = False
        Dim prow As DataRow() = prebal.Select("tdate =#" & Format(date1, "dd-MMM-yyyy") & "# and company='" & compname & "'")
        If Not prow Is Nothing Then
            If prow.Length = 0 Then
                ReDim prow(1)
                prow(0) = prebal.NewRow
                prow(0)("company") = compname
                prow(0)("tdate") = date1
                prow(0)("stbal") = 0
                prow(0)("futbal") = 0
                prow(0)("optbal") = 0
                flgNew = True
            End If
        End If
        Dim exp As Double = 0
        'calculate expense 
        '*****************************************************
        If optype = "E" Then 'for equity
            Dim stexp, stexp1, dst, ndst As Double
            'If date1 = Today.Date Then 'delivery base expense
            stexp = Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "qty > 0 and company = '" & compname & "' AND (entrydate >= #" & Format(date1, "dd-MMM-yyyy") & "# AND entrydate < #" & Format(date1.AddDays(1), "dd-MMM-yyyy") & "#)").ToString), 2)
            stexp1 = Math.Abs(Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "qty < 0 and company = '" & compname & "' AND (entrydate >= #" & Format(date1, "dd-MMM-yyyy") & "# AND entrydate < #" & Format(date1.AddDays(1), "dd-MMM-yyyy") & "#)").ToString), 2))
            dst = stexp - stexp1
            If dst > 0 Then
                ndst = stexp1
                exp = ((dst * dbl) / dblp)
                exp += ((stexp1 * ndbs) / ndbsp)
                exp += ((stexp1 * ndbl) / ndblp)
            Else
                ndst = stexp
                dst = -dst
                exp += ((dst * dbs) / dbsp)
                exp += ((stexp * ndbl) / ndblp)
                exp += ((stexp * ndbs) / ndbsp)
            End If
            'after trade deletion expense
            Dim eqExp As Double
            eqExp = prow(0)("stbal")
            prow(0)("stbal") = exp
            'deleted trades expense
            If rate < 0 Then
                exp = -(eqExp - exp)
            Else
                exp = exp - eqExp
            End If
            'Else 'for delivery base
            '    If qty > 0 Then 'long expense
            '        exp = (qty * rate * dbl) / dblp
            '    Else 'short expense
            '        exp = -(qty * rate * dbs) / dbsp
            '    End If
            '    prow(0)("stbal") = Val(prow(0)("stbal")) + exp
            '    exp = prow(0)("stbal")
            ' End If
            '*******************************************************
        ElseIf (optype = "X" Or optype = "" Or optype = "F") Then 'for future
            exp = 0
            If qty > 0 Then
                exp = (qty * rate * futl) / futlp
            Else
                exp = -(qty * rate * futs) / futsp
            End If
            prow(0)("futbal") = Val(prow(0)("futbal")) + exp
            '************************************************************
        ElseIf (optype = "C" Or optype = "P" Or optype = "CE" Or optype = "PE" Or optype = "CA" Or optype = "PA") Then 'for option
            exp = 0
            If Val(spl) <> 0 Then
                REM Rate = rate + StrikeRate  Update By Viral
                If qty > 0 Then
                    exp = Val((qty * (rate + StrikeRate) * spl) / splp)
                Else
                    qty = -qty
                    exp = Val((qty * (rate + StrikeRate) * sps) / spsp)
                End If
            Else
                If qty > 0 Then
                    exp = Val((qty * rate * prel) / prelp)
                Else
                    qty = -qty
                    exp = Val((qty * rate * pres) / presp)
                End If
            End If
            prow(0)("optbal") = Val(prow(0)("optbal")) + exp
        End If


        If (optype = "CX" Or optype = "" Or optype = "CF") Then 'for currency
            exp = 0
            If qty > 0 Then
                exp = (qty * rate * currfutl) / currfutlp
            Else
                exp = -(qty * rate * currfuts) / currfutsp
            End If
            prow(0)("futbal") = Val(prow(0)("futbal")) + exp
            '************************************************************
        ElseIf (optype = "CP" Or optype = "CC") Then
            exp = 0
            If Val(currspl) <> 0 Then
                REM Rate = rate + StrikeRate  Update By Viral
                If qty > 0 Then
                    exp = Val((qty * (rate + StrikeRate) * currspl) / currsplp)
                Else
                    qty = -qty
                    exp = Val((qty * (rate + StrikeRate) * currsps) / currspsp)
                End If
            Else
                If qty > 0 Then
                    exp = Val((qty * rate * currprel) / currprelp)
                Else
                    qty = -qty
                    exp = Val((qty * rate * currpres) / currpresp)
                End If
            End If
            prow(0)("optbal") = Val(prow(0)("optbal")) + exp
        End If

        prow(0)("tot") = Val(prow(0)("stbal")) + Val(prow(0)("futbal")) + Val(prow(0)("optbal"))
        prebal.AcceptChanges()

        'for today expense
        If date1 = Today.Date Then
            toExp = exp
        Else
            prExp = exp
        End If

        If flgNew = True Then 'add new row
            prebal.Rows.Add(prow(0))
            objTrad.insert_prebal(prow(0))
        Else 'update expense
            objTrad.Delete_prBal(date1.Date, compname)
            objTrad.insert_prebal(prow(0))
            'With prow(0)
            '    objTrad.update_prebal(date1.Date, .Item("stbal"), .Item("futbal"), .Item("optbal"), .Item("company"))
            'End With

        End If

        'If date1 < Now.Date Then ' previous trades expense calulation
        '    Dim prebalance As New DataTable
        '    prebalance = objTrad.prebal
        '    Dim addprebal As New DataTable
        '    addprebal = New DataView(prebalance, "company = '" & compname & "' and tdate=#" & date1 & "#", "", DataViewRowState.CurrentRows).ToTable
        '    'if no prebalance for position then add new position
        '    If addprebal.Rows.Count = 0 Then
        '        'With addprebal.Columns
        '        '    ' .Add("tdate", GetType(Date))
        '        '    .Add("stbal", GetType(Double))
        '        '    .Add("futbal", GetType(Double))
        '        '    .Add("optbal", GetType(Double))
        '        '    '.Add("company", GetType(String))
        '        'End With
        '        Dim drow As DataRow = addprebal.NewRow
        '        addprebal.Rows.Add(drow)
        '        addprebal.Rows(0)("company") = compname
        '        addprebal.Rows(0)("tdate") = date1
        '        addprebal.Rows(0)("stbal") = 0
        '        addprebal.Rows(0)("futbal") = 0
        '        addprebal.Rows(0)("optbal") = 0
        '    End If
        '    addprebal.AcceptChanges()

        '    If optype = "E" Then
        '        If qty > 0 Then
        '            prExp = Val((qty * rate * dbl) / dblp)
        '            addprebal.Rows(0)("stbal") += prExp
        '        Else
        '            qty = -qty
        '            prExp = Val((qty * rate * dbs) / dbsp)
        '            addprebal.Rows(0)("stbal") += prExp
        '        End If
        '    ElseIf optype = "X" Or optype = "F" Or optype = "" Then
        '        If qty > 0 Then
        '            prExp = Val((qty * rate * futl) / futlp)
        '            addprebal.Rows(0)("futbal") += prExp
        '        Else
        '            qty = -qty
        '            prExp = Val((qty * rate * futs) / futsp)
        '            addprebal.Rows(0)("futbal") += prExp
        '        End If
        '    Else 'option type
        '        If Val(spl) <> 0 Then
        '            If qty > 0 Then
        '                prExp = Val((qty * rate * spl) / splp)
        '                addprebal.Rows(0)("optbal") += prExp
        '            Else
        '                qty = -qty
        '                prExp = Val((qty * rate * sps) / spsp)
        '                addprebal.Rows(0)("optbal") += prExp
        '            End If
        '        Else
        '            If qty > 0 Then
        '                prExp = Val((qty * rate * prel) / prelp)
        '                addprebal.Rows(0)("optbal") += prExp
        '            Else
        '                qty = -qty
        '                prExp = Val((qty * rate * pres) / presp)
        '                addprebal.Rows(0)("optbal") += prExp
        '            End If
        '        End If
        '    End If
        '    addprebal.Rows(0)("tot") = Math.Abs(Val(addprebal.Rows(0)("stbal")) + Val(addprebal.Rows(0)("futbal")) + Val(addprebal.Rows(0)("optbal")))
        '    objTrad.Delete_prBal(date1.Date, compname)
        '    objTrad.insert_prebal(addprebal)
        '    prExp = Val(addprebal.Rows(0)("tot"))
        'Else 'calculate today expense
        '    Dim stexp, stexp1, dst, ndst As Double
        '    'FOR Equity
        '    stexp = Math.Round(Val(dtEQTrades.Compute("sum(tot)", "qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2)
        '    stexp1 = Math.Abs(Math.Round(Val(dtEQTrades.Compute("sum(qty)", "qty < 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2))
        '    dst = stexp - stexp1
        '    If dst > 0 Then
        '        ndst = stexp1
        '        toExp += ((dst * ndbl) / ndblp)
        '        toExp += ((stexp1 * ndbs) / ndbsp)
        '        toExp += ((stexp1 * ndbl) / ndblp)
        '    Else
        '        ndst = stexp
        '        dst = -dst
        '        toExp += ((dst * dbs) / dbsp)
        '        toExp += ((stexp * ndbl) / ndblp)
        '        toExp += ((stexp * ndbs) / ndbsp)
        '    End If
        '    'for FUTURe
        '    stexp = 0
        '    stexp1 = 0
        '    stexp = Val(dtFOTrades.Compute("sum(tot)", "cp not in ('C','P') and company = '" & compname & "' and qty > 0 and entry_date =  #" & date1 & "#").ToString)
        '    stexp1 = Math.Abs(Val(dtFOTrades.Compute("sum(tot)", "cp not in ('C','P') and company = '" & compname & "' and qty < 0 and entry_date =  #" & date1 & "#").ToString))
        '    toExp += ((stexp * futl) / futlp)
        '    toExp += ((stexp1 * futs) / futsp)

        '    'OPTION
        '    If Val(spl) <> 0 Then
        '        stexp = 0
        '        stexp1 = 0
        '        stexp = Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString)
        '        stexp1 = Math.Abs(Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and company = '" & compname & "' qty < 0 and entry_date =  #" & date1 & "#").ToString))
        '        toExp += ((stexp * spl) / splp)
        '        toExp += ((stexp1 * sps) / spsp)
        '    Else
        '        stexp = 0
        '        stexp1 = 0
        '        stexp = Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString)
        '        stexp1 = Math.Abs(Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and company = '" & compname & "' and qty < 0 and entry_date =  #" & date1 & "#").ToString))

        '        toExp += ((stexp * prel) / prelp)
        '        toExp += ((stexp1 * pres) / presp)
        '    End If
        'End If

    End Sub

End Class