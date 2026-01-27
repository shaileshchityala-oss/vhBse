Public Class OpenPosition
    Dim objScript As script = New script
    Dim masterdata As DataTable = New DataTable
    Dim dtUid As New DataTable
    Dim eqmasterdata As DataTable = New DataTable
    Dim objTrad As New trading

    Dim DTCurrencyContract As DataTable = New DataTable

    Dim cmbheight As Boolean = False
    Dim cmbh As Integer
    Dim objAna As New analysisprocess
    Public openposition As Boolean
    Dim Varmultiplier As Double = 0
    Public openposyes As Boolean = False

    Private Sub OpenPosition_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    End Sub
    Private Sub OpenPosition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If OpenPosition = False Then
            Panel6.Enabled = True
            CmbComp.Enabled = False
            CmbInstru.Enabled = False
            cmbcp.Enabled = False
            cmbstrike.Enabled = False
            cmbdate.Enabled = False
            txtscript.Enabled = False

            Panel3.Enabled = False
            cmbeqcomp.Enabled = False
            cmbeqopt.Enabled = False
            txteqscript.Enabled = False
            Panel7.Enabled = False
        Else
            Panel6.Enabled = False
            CmbComp.Enabled = True
            CmbInstru.Enabled = True
            cmbcp.Enabled = True
            cmbcp.Enabled = True
            cmbdate.Enabled = True
            Panel3.Enabled = True
            cmbeqcomp.Enabled = True
            cmbeqopt.Enabled = True
            Panel7.Enabled = True
        End If
        dtent.MaxDate = Now
        dteqent.MaxDate = Now
        DTPCurrencyEntryDate.MaxDate = Now
        dtent.Value = DateAdd(DateInterval.Day, -1, Now.Date)
        dteqent.Value = DateAdd(DateInterval.Day, -1, Now.Date)
        DTPCurrencyEntryDate.Value = DateAdd(DateInterval.Day, -1, Now.Date)
        TabControl1.SelectedIndex = 0
        openposyes = False
        Call TabControl1_Click(sender, e)
        'masterdata = New DataTable
        'masterdata = cpfmaster

        'DTCurrencyContract = New DataTable
        'DTCurrencyContract = Currencymaster

        'Dim dv As DataView = New DataView(masterdata, "", "symbol", DataViewRowState.CurrentRows)
        'CmbComp.DataSource = dv.ToTable(True, "symbol")
        'CmbComp.DisplayMember = "symbol"
        'CmbComp.ValueMember = "symbol"
        'If dv.ToTable(True, "symbol").Compute("count(symbol)", "symbol='NIFTY'") > 0 Then
        '    CmbComp.SelectedValue = "NIFTY"
        'End If

        'eqmasterdata = eqmaster
        'Dim dv1 As DataView = New DataView(eqmasterdata, "", "symbol", DataViewRowState.CurrentRows)
        'cmbeqcomp.DataSource = dv1.ToTable(True, "symbol")
        'cmbeqcomp.DisplayMember = "symbol"
        'cmbeqcomp.ValueMember = "symbol"
        ' cmbeqcomp.Refresh()
        'cmbh = CmbComp.Height

        'Dim dvCurr As DataView = New DataView(DTCurrencyContract, "", "symbol", DataViewRowState.CurrentRows)
        'cmbCurrencyComp.DataSource = dvCurr.ToTable(True, "symbol")
        'cmbCurrencyComp.DisplayMember = "symbol"
        'cmbCurrencyComp.ValueMember = "symbol"
        'openposyes = False
        'CmbComp.Focus()
    End Sub
    Private Sub CmbInstru_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbInstru.KeyDown
        If e.KeyCode = Keys.Enter Then
            If CmbInstru.SelectedIndex = 0 Then
                cmbdate.Select()
            Else
                cmbcp.Select()
            End If
        End If
    End Sub
    Private Sub CmbInstru_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbInstru.Leave
        cmbcp.Enabled = True
        cmbstrike.Enabled = True
        If CmbInstru.Text.Trim <> "" And CmbInstru.Items.Count > 0 Then
            If (UCase(CmbInstru.Text) = "FUTIDX" Or UCase(CmbInstru.Text) = "FUTSTK" Or UCase(CmbInstru.Text) = "OPTIDX" Or UCase(CmbInstru.Text) = "OPTSTK") Then
                Dim dv As DataView = New DataView(masterdata, "InstrumentName='" & CmbInstru.Text & "'", "option_type", DataViewRowState.CurrentRows)
                cmbcp.DataSource = dv.ToTable(True, "option_type")
                cmbcp.DisplayMember = "option_type"
                cmbcp.ValueMember = "option_type"
                'If cmbcp.Items.Count > 0 Then
                '    cmbcp.SelectedIndex = 0
                'End If
                If Not CmbInstru Is Nothing And CmbInstru.Items.Count > 0 Then
                    If CmbInstru.Text <> "" Then
                        If UCase(Mid(CmbInstru.Text, 1, 3)) = "FUT" Then
                            cmbcp.Enabled = False
                            cmbstrike.Enabled = False
                            cmbstrike.Text = 0
                            dv = New DataView(masterdata, "symbol='" & CmbComp.Text & "' and InstrumentName='" & CmbInstru.Text & "'  AND expdate1 >='" & Now.Date & "' ", "strike_price", DataViewRowState.CurrentRows)
                            cmbdate.DataSource = dv.ToTable(True, "expdate")
                            cmbdate.DisplayMember = "expdate"
                            cmbdate.ValueMember = "expdate"
                            'cmbdate.Focus()
                        Else
                            cmbcp.Enabled = True
                            cmbstrike.Enabled = True
                        End If
                    End If
                End If
            Else
                MsgBox("Select valid instrument name")
                CmbInstru.Text = ""
                CmbInstru.Focus()
            End If
        End If
        If CmbInstru.Text.Trim = "" Then
            cmbcp.DataSource = Nothing
            cmbcp.DataSource = Nothing
            cmbdate.DataSource = Nothing
        End If
    End Sub
   
    Private Sub cmbcp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcp.Leave
        If CmbInstru.Text.Trim <> "" And cmbcp.Text <> "".Trim And cmbcp.Items.Count > 0 Then
            If (UCase(cmbcp.Text) = "CE" Or UCase(cmbcp.Text) = "PE" Or UCase(cmbcp.Text) = "CA" Or UCase(cmbcp.Text) = "PA") Then
                Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbComp.Text & "' and InstrumentName='" & CmbInstru.Text & "' and option_type='" & cmbcp.Text & "'", "strike_price", DataViewRowState.CurrentRows)
                cmbstrike.DataSource = dv.ToTable(True, "strike_price")
                cmbstrike.DisplayMember = "strike_price"
                cmbstrike.ValueMember = "strike_price"
                If cmbstrike.SelectedText <> "" Then
                    cmbstrike.SelectedIndex = 0
                End If
            Else
                MsgBox("Select valid option type")
                cmbcp.Text = ""
                cmbcp.Focus()
            End If
        End If
    End Sub
    Private Sub cmbcp_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbcp.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbstrike.Select()
        End If
    End Sub
    Private Sub cmbstrike_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstrike.Leave
        If cmbstrike.Text.Trim = "" Then
            cmbstrike.Text = 0
        End If
        If (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") And cmbstrike.Text <> "0" Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbComp.Text & "' and InstrumentName='" & CmbInstru.Text & "' and option_type='" & cmbcp.Text & "' and strike_price=" & Val(cmbstrike.Text) & " ", "strike_price", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid strike rate")
                cmbstrike.Text = ""
                cmbstrike.Focus()
                Exit Sub
            End If
            cmbdate.DataSource = dv.ToTable(True, "expdate")
            cmbdate.DisplayMember = "expdate"
            cmbdate.ValueMember = "expdate"
            If cmbdate.Items.Count > 0 Then
                cmbdate.SelectedIndex = 0
            End If
        Else
            Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbComp.Text & "' and InstrumentName='" & CmbInstru.Text & "' and option_type='" & cmbcp.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
            cmbdate.DataSource = dv.ToTable(True, "expdate")
            cmbdate.DisplayMember = "expdate"
            cmbdate.ValueMember = "expdate"
        End If
    End Sub
    Private Sub cmbstrike_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbstrike.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbdate.Select()
        End If
    End Sub
    Private Sub cmbdate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtunit.Select()
        End If
    End Sub
    Private Sub txtunit_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtunit.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtrate.Select()
        End If
    End Sub
    Private Sub txtrate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtrate.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtent.Select()
        End If
    End Sub
    Private Sub dtent_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtent.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdsave.Select()
        End If
    End Sub
    Private Sub CmbComp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbComp.Leave
        If CmbComp.Text.Trim <> "" And CmbComp.Items.Count > 0 Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbComp.Text & "'", "InstrumentName", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid Security")
                CmbComp.Text = ""
                CmbComp.Focus()
                Exit Sub
            End If
            CmbInstru.DataSource = dv.ToTable(True, "InstrumentName")
            CmbInstru.DisplayMember = "InstrumentName"
            CmbInstru.ValueMember = "InstrumentName"
            'If CmbComp.SelectedText <> "" Then
            '    CmbInstru.SelectedIndex = 0
            'End If
        End If
        If CmbComp.Text.Trim = "" Then
            CmbInstru.DataSource = Nothing
            cmbcp.DataSource = Nothing
            cmbstrike.DataSource = Nothing
            cmbdate.DataSource = Nothing
        End If
    End Sub
    Private Sub CmbComp_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbComp.KeyDown
        If e.KeyCode = Keys.Enter Then
             CmbInstru.Select()
        End If
    End Sub
    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click

        Me.Close()
    End Sub
    Private Sub cmbdate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdate.Leave
        If CmbComp.Text.Trim = "" Then
            MsgBox("Enter Security Name", MsgBoxStyle.Information)
            CmbComp.Focus()
            Exit Sub
        End If
        If CmbInstru.Text.Trim = "" Then
            MsgBox("Select Instrument Name", MsgBoxStyle.Information)
            CmbInstru.Focus()
            Exit Sub
        End If

        If cmbcp.SelectedValue.Trim = "" Then
            MsgBox("Select Call/Put/Futre", MsgBoxStyle.Information)
            cmbcp.Focus()
            Exit Sub
        End If
        If cmbstrike.Text.Trim = "0" And (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate", MsgBoxStyle.Information)
            cmbstrike.Focus()
            Exit Sub
        End If
        If cmbdate.Text = "" Then
            MsgBox("Select date", MsgBoxStyle.Information)
            cmbdate.Focus()
            Exit Sub
        End If
        Dim cp As String
        cp = ""
        If Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P" Then
            cp = UCase(cmbcp.SelectedValue)
        Else
            cp = ""
        End If
        Dim script As String
        If cp = "" Then
            script = CmbInstru.SelectedValue & "  " & CmbComp.SelectedValue & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy")
        Else
            script = CmbInstru.SelectedValue & "  " & CmbComp.SelectedValue & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy") & "  " & Format(Val(cmbstrike.Text), "###0.00") & "  " & cp
        End If
        txtscript.Text = script.Trim
        If masterdata.Compute("count(symbol)", "script='" & txtscript.Text.Trim & "'") <= 0 Then
            MsgBox("Not valid Script")
            form_clear()
            Exit Sub
        End If
    End Sub
    Private Sub form_clear() 'FO Trades
        CmbInstru.Text = ""
        CmbComp.Text = ""
        cmbstrike.Enabled = False
        cmbcp.Enabled = False
        cmbstrike.Text = "0"
        txtscript.Text = ""
        txtunit.Text = "0"
        txtrate.Text = "0"
        CmbComp.SelectedIndex = 0
        CmbComp.Focus()
    End Sub
    Private Sub eqform_clear() 'EQ Trades
        cmbeqcomp.Text = ""
        txteqscript.Text = ""
        txtequnit.Text = "0"
        txteqrate.Text = "0"
        cmbeqopt.Text = ""
        cmbeqcomp.SelectedIndex = 0
        cmbeqcomp.Focus()
    End Sub

    Private Function form_validation() As Boolean
        If CmbInstru.Text.Trim = "" Then
            MsgBox("Select Instrument Name", MsgBoxStyle.Information)
            CmbInstru.Focus()
            form_validation = False
            Exit Function
        End If
        If CmbComp.Text.Trim = "" Then
            MsgBox("Enter Company Name", MsgBoxStyle.Information)
            CmbComp.Focus()
            form_validation = False
            Exit Function
        End If
        If cmbstrike.Text.Trim = "0" And (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate", MsgBoxStyle.Information)
            cmbstrike.Focus()
            form_validation = False
            Exit Function
        End If
        If cmbcp.Text = "" Then
            MsgBox("Select Call/Put/Futre", MsgBoxStyle.Information)
            cmbcp.Focus()
            form_validation = False
            Exit Function
        End If
        If Not IsNumeric(txtunit.Text) Then
            MsgBox("Enter Units", MsgBoxStyle.Information)
            txtunit.Focus()
            form_validation = False
            Exit Function
        End If
        If Not IsNumeric(txtrate.Text) Then
            MsgBox("Enter Rate", MsgBoxStyle.Information)
            txtrate.Focus()
            form_validation = False
            Exit Function
        End If
        If txtunit.Text = "0" Then
            MsgBox("Enter Units", MsgBoxStyle.Information)
            txtunit.Focus()
            form_validation = False
            Exit Function
        End If
        If txtrate.Text = "0" Then
            MsgBox("Enter Rate", MsgBoxStyle.Information)
            txtrate.Focus()
            form_validation = False
            Exit Function
        End If
        form_validation = True
    End Function
    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        Dim strDate As String
        Try
            'maintable.Rows.Count = 0
            If txtunit.Text = "" Then
                txtunit.Text = 0
            End If
            If txtrate.Text = "" Then
                txtrate.Text = 0
            End If
            If form_validation() Then
                Dim tkk As Long = CLng(Val(objTrad.Trading.Compute("max(token)", "script='" & txtscript.Text & "'").ToString))
                If tkk > 0 Then
                    MsgBox(txtscript.Text & " script already exist in Traded")
                    Exit Sub
                End If
                Dim a, a1, script1 As String
                Dim tk As Long
                GVarMAXFOTradingOrderNo = GVarMAXFOTradingOrderNo + 1
                'insert trade to Trading table
                objScript.InstrumentName = CmbInstru.Text
                objScript.Company = CmbComp.Text
                objScript.Mdate = CDate(cmbdate.Text).Date
                objScript.StrikeRate = Val(cmbstrike.Text)
                objScript.CP = UCase(Mid(cmbcp.SelectedValue, 1, 1))
                objScript.Script = txtscript.Text.Trim
                objScript.Units = Val(txtunit.Text)
                objScript.Rate = Val(txtrate.Text)
                objScript.EntryDate = dtent.Value.Date
                If UCase(Mid(cmbcp.SelectedValue, 1, 1)) = "C" Or UCase(Mid(cmbcp.SelectedValue, 1, 1)) = "P" Then
                    a = Mid(txtscript.Text, Len(txtscript.Text) - 1, 1)
                    a1 = Mid(txtscript.Text, Len(txtscript.Text), 1)
                    If a = "C" Then
                        script1 = Mid(txtscript.Text, 1, Len(txtscript.Text) - 2) & "P" & a1
                    Else
                        script1 = Mid(txtscript.Text, 1, Len(txtscript.Text) - 2) & "C" & a1
                    End If
                    tk = CLng(masterdata.Compute("max(token)", "script='" & script1 & "'").ToString)
                    objScript.Token = tk
                    For Each row As DataRow In GdtFOTrades.Select("script='" & txtscript.Text & "'")
                        If row("isliq") = True Then
                            objScript.Isliq = True ' "Yes"
                        Else
                            objScript.Isliq = False '"No"
                        End If
                        Exit For
                    Next
                Else
                    objScript.Token = 0
                    objScript.asset_tokan = 0
                    objScript.Isliq = False ' "No"
                End If
                objScript.orderno = GVarMAXFOTradingOrderNo
                Dim tk1 As Long = CLng(Val(masterdata.Compute("max(token)", "script='" & txtscript.Text & "'").ToString))
                If tk1 = 0 Then
                    MsgBox(txtscript.Text & " does not exist in contract")
                End If
                'save trade to database trade table
                Dim DTDuplicate As DataTable
                DTDuplicate = New DataTable
                DTDuplicate = objScript.Insert()
                If DTDuplicate.Rows.Count - 1 > 0 Then
                    MsgBox("Contract Exist")
                    Exit Sub
                End If
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                'get uid from trading table
                dtUid = objScript.select_trading_uid
                'Save to analysis's dtFOTrading table
                Dim tprow As DataRow
                tprow = GdtFOTrades.NewRow
                tprow("uid") = dtUid.Rows(0)("uid")
                tprow("token") = 0
                tprow("mo") = Format(CDate(cmbdate.Text).Date, "MM/yyyy")
                tprow("instrumentname") = CmbInstru.Text
                tprow("company") = CmbComp.Text
                tprow("mdate") = CDate(cmbdate.Text).Date
                tprow("Strikerate") = Val(cmbstrike.Text)
                tprow("CP") = IIf(UCase(Mid(cmbcp.SelectedValue, 1, 1)) = "X", "F", UCase(Mid(cmbcp.SelectedValue, 1, 1)))
                tprow("script") = txtscript.Text.Trim
                tprow("qty") = Val(txtunit.Text)
                tprow("Rate") = Val(txtrate.Text)
                strDate = Format(dtent.Value.Date, "MMM/dd/yyyy")
                tprow("EntryDate") = Format(CDate(strDate).Date, "MMM/dd/yyyy")
                tprow("entry_date") = Format(CDate(strDate).Date, "MMM/dd/yyyy")
                tprow("isliq") = objScript.Isliq
                tprow("token1") = tk1
                tprow("orderno") = GVarMAXFOTradingOrderNo
                tprow("entryno") = 0
                tprow("tot") = Val(txtunit.Text) * Val(txtrate.Text)
                tprow("issummary") = True
                tprow("isdisplay") = True
                GdtFOTrades.Rows.Add(tprow)
                GdtFOTrades.AcceptChanges()

                'calculate expense of inserted position
                Dim prExp, toExp As Double

                cal_prebal(dtent.Value.Date, CmbComp.Text.Trim, UCase(Mid(cmbcp.SelectedValue, 1, 1)), CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp)
                'insert position to database's analysis table also
                Dim dtAna As New DataTable
                dtAna = objAna.fill_table_process(txtscript.Text.Trim, CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp, dtent.Value.Date)
                'insert FO trade to analysis table
                objScript.insert_FOTrade_in_maintable(txtscript.Text.Trim, dtAna, prExp, toExp, dtent.Value.Date)
                MsgBox("Script saved successfully", MsgBoxStyle.Information)
                LastOpenPosition = CmbComp.Text
            End If
            '***********************************************************************************                objAna.fill_table_process(txtscript.Text.Trim)
            ' If dtent.Value.Date < Now.Date Then
            'End If
            txtrate.Text = "0"
            txtunit.Text = "0"
            openposyes = True
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub cal_prebal(ByVal date1 As Date, ByVal compname As String, ByVal optype As String, ByVal qty As Integer, ByVal rate As Double, ByRef prExp As Double, ByRef toExp As Double)
        Dim flgNew As Boolean = False
        Dim prow As DataRow() = prebal.Select("tdate =#" & date1 & "# and company='" & compname & "'")
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
            stexp = Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2)
            stexp1 = Math.Abs(Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "qty < 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2))
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
            prow(0)("stbal") = exp
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
                If qty > 0 Then
                    exp = Val((qty * rate * spl) / splp)
                Else
                    qty = -qty
                    exp = Val((qty * rate * sps) / spsp)
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

        If (optype = "CX" Or optype = "" Or optype = "CF") Then ''for currency
            exp = 0
            If qty > 0 Then
                exp = (qty * rate * currfutl) / currfutlp
            Else
                exp = -(qty * rate * currfuts) / currfutsp
            End If
            prow(0)("futbal") = Val(prow(0)("futbal")) + exp
        ElseIf (optype = "CC" Or optype = "CP") Then
            exp = 0
            If Val(currspl) <> 0 Then
                If qty > 0 Then
                    exp = Val((qty * rate * currspl) / currsplp)
                Else
                    qty = -qty
                    exp = Val((qty * rate * currsps) / currspsp)
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

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Call form_clear()
    End Sub

    Private Sub cmbeqcomp_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbeqcomp.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbeqopt.Select()
        End If
    End Sub

    Private Sub cmbeqcomp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbeqcomp.Leave
        cmbeqcomp.Height = cmbh
        If cmbeqcomp.Text <> "" Then
            Dim dv1 As DataView = New DataView(eqmasterdata, "symbol='" & cmbeqcomp.Text & "'", "series", DataViewRowState.CurrentRows)
            cmbeqopt.DataSource = dv1.ToTable(True, "series")
            cmbeqopt.DisplayMember = "series"
            cmbeqopt.ValueMember = "series"
            If cmbeqopt.Items.Count > 0 Then
                If New DataView(eqmasterdata, "symbol='" & cmbeqcomp.Text & "' and series='EQ'", "series", DataViewRowState.CurrentRows).ToTable.Rows.Count > 0 Then
                    cmbeqopt.SelectedValue = "EQ"
                Else
                    cmbeqopt.SelectedIndex = 0
                End If
            End If
        End If
    End Sub

    Private Sub cmbeqopt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbeqopt.Leave
        Try
            If cmbeqcomp.Text <> "" And cmbeqopt.Text <> "" Then
                cmbeqopt.Text = UCase(cmbeqopt.Text.Trim)
                If eqmasterdata.Compute("count(symbol)", "SERIES='" & cmbeqopt.Text.Trim & "' AND SYMBOL='" & cmbeqcomp.Text.Trim & "'") <= 0 Then
                    MsgBox("Not valid Series")
                    cmbeqopt.Text = ""
                    txteqscript.Text = ""
                    cmbeqopt.Focus()
                    Exit Sub
                End If
                txteqscript.Text = cmbeqcomp.SelectedValue.ToString & Space(2) & cmbeqopt.Text
                If eqmasterdata.Compute("count(symbol)", "script='" & txteqscript.Text.Trim & "'") <= 0 Then
                    MsgBox("Not valid Script")
                    eqform_clear()

                    Exit Sub
                End If

            End If
        Catch ex As Exception
            MsgBox("Invalid script")
            ' cmbeqcomp.Select()
        End Try
    End Sub

    Private Sub txtequnit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtequnit.KeyPress
        Call numonly(e)
    End Sub

    Private Sub txteqrate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txteqrate.KeyPress
        Call numonly(e)
    End Sub

    Private Sub cmbeqopt_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbeqopt.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtequnit.Focus()
        End If
    End Sub

    
    Private Sub txtequnit_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtequnit.KeyDown
        If e.KeyCode = Keys.Enter Then
            txteqrate.Focus()
        End If
    End Sub

    Private Sub txteqrate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txteqrate.KeyDown
        If e.KeyCode = Keys.Enter Then
            dteqent.Focus()
        End If
    End Sub

    Private Sub dteqent_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dteqent.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdeqsave.Focus()
        End If
    End Sub
    Private Function eqform_validation() As Boolean

        If cmbeqcomp.Text.Trim = "" Then
            MsgBox("Enter Company Name", MsgBoxStyle.Information)
            cmbeqcomp.Focus()
            eqform_validation = False
            Exit Function
        End If
        If cmbeqopt.Text.Trim = "" Then
            MsgBox("Enter Option", MsgBoxStyle.Information)
            cmbeqopt.Focus()
            eqform_validation = False
            Exit Function
        End If

        If txtequnit.Text = "0" Then
            MsgBox("Enter Units", MsgBoxStyle.Information)
            txtequnit.Focus()
            eqform_validation = False
            Exit Function
        End If
        If txteqrate.Text = "0" Then
            MsgBox("Enter Rate", MsgBoxStyle.Information)
            txteqrate.Focus()
            eqform_validation = False
            Exit Function
        End If
        eqform_validation = True
    End Function
    Private Sub cmdeqsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqsave.Click
        Try
            If txtequnit.Text = "" Then
                txtunit.Text = 0
            End If
            If txteqrate.Text = "" Then
                txtrate.Text = 0
            End If
            If eqform_validation() Then
                'Dim tkk As Long = CLng(IIf(IsDBNull(objTrad.select_equity.Compute("max(uid)", "script='" & txteqscript.Text & "'")), 0, objTrad.select_equity.Compute("max(uid)", "script='" & txteqscript.Text & "'")))
                'If tkk > 0 Then
                '    MsgBox(txteqscript.Text & " script already exist in Traded")
                '    Exit Sub
                'End If
                GVarMAXEQTradingOrderNo = GVarMAXEQTradingOrderNo + 1
                objScript.Company = cmbeqcomp.Text
                objScript.Script = UCase(txteqscript.Text.Trim)
                objScript.CP = cmbeqopt.Text
                objScript.Units = Val(txtequnit.Text)
                objScript.Rate = Val(txteqrate.Text)
                objScript.EntryDate = dteqent.Value.Date
                objScript.orderno = GVarMAXEQTradingOrderNo
                objScript.insert_equity()
                'objAna.fill_equity_process(UCase(txteqscript.Text.Trim))
                'If dteqent.Value.Date < Now.Date Then
                ' cal_prebal(dteqent.Value.Date, cmbeqcomp.Text.Trim)
                'End If
                'get uid from equity_trading
                dtUid = objScript.select_equity_uid

                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                'Save to analysis's tptable (equity trading) table
                Dim tprow As DataRow
                tprow = GdtEQTrades.NewRow
                tprow("uid") = dtUid.Rows(0)("uid")
                tprow("company") = cmbeqcomp.Text
                tprow("eq") = cmbeqopt.Text
                tprow("script") = UCase(txteqscript.Text.Trim)
                tprow("qty") = Val(txtequnit.Text)
                tprow("Rate") = Val(txteqrate.Text)
                Dim strdate As String
                strdate = Format(dteqent.Value.Date, "MMM/dd/yyyy")
                tprow("EntryDate") = Format(CDate(strdate).Date, "MMM/dd/yyyy")
                tprow("entry_date") = Format(CDate(strdate).Date, "MMM/dd/yyyy")
                tprow("orderno") = GVarMAXEQTradingOrderNo
                tprow("entryno") = 0
                tprow("issummary") = True
                tprow("isdisplay") = True
                tprow("tot") = Val(txtequnit.Text) * Val(txteqrate.Text)
                GdtEQTrades.Rows.Add(tprow)
                GdtEQTrades.AcceptChanges()
                ' If dteqent.Value.Date < Now.Date Then
                Dim prExp, toExp As Double
                'caluclate expense of inserted position
                cal_prebal(dteqent.Value.Date, cmbeqcomp.Text.Trim, "E", CInt(txtequnit.Text), Val(txteqrate.Text), prExp, toExp)

                'insert position to database's analysis table also
                Dim dtAna As New DataTable
                dtAna = objAna.fill_equity_process(UCase(txteqscript.Text.Trim), CInt(txtequnit.Text), Val(txteqrate.Text), prExp, toExp, dteqent.Value.Date)

                '***********************************************************************************
                'insert FO trade to analysis table
                objScript.insert_EQTrade_in_maintable(txteqscript.Text.Trim, dtAna, prExp, toExp, dteqent.Value.Date)
                'End If
                MsgBox("Script saved successfully", MsgBoxStyle.Information)

                LastOpenPosition = cmbeqcomp.Text

                txtequnit.Text = "0"
                txteqrate.Text = "0"
                openposyes = True
                'eqform_clear()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmdeqclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqclear.Click
        Call eqform_clear()

    End Sub

    Private Sub cmdeqexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqexit.Click
        Me.Close()
    End Sub

    Private Sub cmbCurrencyComp_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCurrencyComp.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbCurrencyInstrument.Select()
        End If
    End Sub

    Private Sub cmbCurrencyComp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyComp.Leave
        If cmbCurrencyComp.Text.Trim <> "" And cmbCurrencyComp.Items.Count > 0 Then
            Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "'", "InstrumentName", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid Security")
                cmbCurrencyComp.Text = ""
                cmbCurrencyComp.Focus()
                Exit Sub
            End If
            cmbCurrencyInstrument.DataSource = dv.ToTable(True, "InstrumentName")
            cmbCurrencyInstrument.DisplayMember = "InstrumentName"
            cmbCurrencyInstrument.ValueMember = "InstrumentName"
            'CmbInstru.Refresh()
            'cmbCurrencyInstrument.SelectedIndex = 0
        End If
        If cmbCurrencyComp.Text.Trim = "" Then
            cmbCurrencyInstrument.DataSource = Nothing
            cmbCurrencyCP.DataSource = Nothing
            cmbCurrencyStrike.DataSource = Nothing
            cmbCurrencyExpdate.DataSource = Nothing
        End If
    End Sub

    'Private Sub cmbCurrencyInstrument_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyInstrument.SelectedValueChanged
    '    If cmbCurrencyInstrument.Text.Trim <> "" And cmbCurrencyInstrument.Items.Count > 0 Then
    '        Dim dv As DataView = New DataView(DTCurrencyContract, "InstrumentName='" & cmbCurrencyInstrument.Text & "'", "option_type", DataViewRowState.CurrentRows)
    '        cmbCurrencyCP.DataSource = dv.ToTable(True, "option_type")
    '        cmbCurrencyCP.DisplayMember = "option_type"
    '        cmbCurrencyCP.ValueMember = "option_type"
    '        'cmbcp.Refresh()
    '        If cmbCurrencyCP.Items.Count > 0 Then
    '            cmbCurrencyCP.SelectedIndex = 0
    '        End If
    '        If Not cmbCurrencyInstrument Is Nothing And cmbCurrencyInstrument.Items.Count > 0 Then
    '            If cmbCurrencyInstrument.Text <> "" Then
    '                If UCase(cmbCurrencyInstrument.Text) <> "OPTCUR" Then
    '                    cmbCurrencyStrike.DataSource = Nothing
    '                    cmbCurrencyCP.Enabled = False
    '                    cmbCurrencyStrike.Enabled = False
    '                    cmbCurrencyStrike.Text = 0
    '                    dv = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' AND InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
    '                    cmbCurrencyExpdate.DataSource = dv.ToTable(True, "expdate")
    '                    cmbCurrencyExpdate.DisplayMember = "expdate"
    '                    cmbCurrencyExpdate.ValueMember = "expdate"
    '                    'cmbdate.Refresh()
    '                    'cmbdate.Focus()
    '                Else
    '                    cmbCurrencyCP.Enabled = True
    '                    cmbCurrencyStrike.Enabled = True
    '                    'cmbcp.Focus()
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub cmbCurrencyInstrument_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyInstrument.Leave
        cmbCurrencyCP.Enabled = True
        cmbCurrencyStrike.Enabled = True
        cmbCurrencyInstrument.Height = cmbh
        If cmbCurrencyInstrument.Text.Trim <> "" And cmbCurrencyInstrument.Items.Count > 0 Then
            If (UCase(cmbCurrencyInstrument.Text) = "FUTCUR" Or UCase(cmbCurrencyInstrument.Text) = "FUTIRD" Or UCase(cmbCurrencyInstrument.Text) = "INDEX" Or UCase(cmbCurrencyInstrument.Text) = "UNDCUR" Or UCase(cmbCurrencyInstrument.Text) = "UNDIRD" Or UCase(cmbCurrencyInstrument.Text) = "OPTCUR") Then
                Dim dv As DataView = New DataView(DTCurrencyContract, "InstrumentName='" & cmbCurrencyInstrument.Text & "'", "option_type", DataViewRowState.CurrentRows)
                cmbCurrencyCP.DataSource = dv.ToTable(True, "option_type")
                cmbCurrencyCP.DisplayMember = "option_type"
                cmbCurrencyCP.ValueMember = "option_type"
                'cmbcp.Refresh()
                If cmbCurrencyCP.Items.Count > 0 Then
                    cmbCurrencyCP.SelectedIndex = 0
                End If
                If Not cmbCurrencyInstrument Is Nothing And cmbCurrencyInstrument.Items.Count > 0 Then
                    If cmbCurrencyInstrument.Text <> "" Then
                        If UCase(cmbCurrencyInstrument.Text) <> "OPTCUR" Then
                            cmbCurrencyCP.Enabled = False
                            cmbCurrencyStrike.DataSource = Nothing

                            cmbCurrencyStrike.Enabled = False
                            cmbCurrencyStrike.Text = 0
                            dv = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' AND InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "' AND expdate1 >='" & Now.Date & "' ", "expdate1", DataViewRowState.CurrentRows)
                            cmbCurrencyExpdate.DataSource = dv.ToTable(True, "expdate")
                            cmbCurrencyExpdate.DisplayMember = "expdate"
                            cmbCurrencyExpdate.ValueMember = "expdate"
                            '  cmbdate.Refresh()
                            ' cmbdate.Focus()
                        Else
                            cmbCurrencyCP.Enabled = True
                            cmbCurrencyStrike.Enabled = True
                            ' cmbcp.Focus()
                        End If
                    End If
                End If
            Else
                MsgBox("Select valid instrument name")
                cmbCurrencyInstrument.Text = ""
                cmbCurrencyInstrument.Focus()
            End If

        End If
        If cmbCurrencyInstrument.Text.Trim = "" Then
            cmbCurrencyCP.DataSource = Nothing
            cmbCurrencyStrike.DataSource = Nothing
            cmbCurrencyExpdate.DataSource = Nothing
        End If

    End Sub

    Private Sub cmbCurrencyInstrument_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCurrencyInstrument.KeyDown
        If e.KeyCode = Keys.Enter Then
            If cmbCurrencyInstrument.SelectedIndex = 0 Then
                cmbCurrencyExpdate.Select()
            Else
                cmbCurrencyCP.Select()
            End If
        End If
    End Sub

    Private Sub cmbCurrencyCP_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyCP.Leave
        If cmbCurrencyInstrument.Text.Trim <> "" And cmbCurrencyCP.Text <> "".Trim And cmbCurrencyCP.Items.Count > 0 Then
            If (UCase(cmbCurrencyCP.Text) = "CE" Or UCase(cmbCurrencyCP.Text) = "PE" Or UCase(cmbCurrencyCP.Text) = "CA" Or UCase(cmbCurrencyCP.Text) = "PA") Then
                Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "'", "strike_price", DataViewRowState.CurrentRows)
                'cmbstrike.Items.Clear()
                cmbCurrencyStrike.DataSource = dv.ToTable(True, "strike_price")
                cmbCurrencyStrike.DisplayMember = "strike_price"
                cmbCurrencyStrike.ValueMember = "strike_price"
                ' cmbstrike.Refresh()
                cmbCurrencyStrike.SelectedIndex = 0
            Else
                MsgBox("Select valid option type")
                cmbCurrencyCP.Text = ""
                cmbCurrencyCP.Focus()
            End If
        End If

    End Sub

    Private Sub cmbCurrencyCP_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCurrencyCP.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbCurrencyStrike.Select()
        End If
    End Sub

    Private Sub cmbCurrencyStrike_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCurrencyStrike.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbCurrencyExpdate.Select()
        End If
    End Sub

    Private Sub cmbCurrencyExpdate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCurrencyExpdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCurrencyunit.Select()
        End If
    End Sub

    Private Sub txtCurrencyunit_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCurrencyunit.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCurrencyrate.Select()
        End If
    End Sub

    Private Sub cmbCurrencyStrike_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyStrike.Leave
        If cmbCurrencyStrike.Text.Trim = "" Then
            cmbCurrencyStrike.Text = 0
        End If
        If (Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P") And cmbCurrencyStrike.Text <> "0" Then
            Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "' and strike_price=" & Val(cmbCurrencyStrike.Text) & " ", "strike_price", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid strike rate")
                cmbCurrencyStrike.Text = ""
                cmbCurrencyStrike.Focus()
                Exit Sub
            End If
            cmbCurrencyExpdate.DataSource = dv.ToTable(True, "expdate")
            cmbCurrencyExpdate.DisplayMember = "expdate"
            cmbCurrencyExpdate.ValueMember = "expdate"
            'cmbdate.Refresh()
            If cmbCurrencyExpdate.Items.Count > 0 Then
                cmbCurrencyExpdate.SelectedIndex = 0
            End If
        Else
            Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
            'If dv.ToTable.Rows.Count <= 0 Then
            '    MsgBox("Select valid strike rate")
            '    cmbstrike.Text = ""
            '    cmbstrike.Focus()
            '    Exit Sub
            'End If
            cmbCurrencyExpdate.DataSource = dv.ToTable(True, "expdate")
            cmbCurrencyExpdate.DisplayMember = "expdate"
            cmbCurrencyExpdate.ValueMember = "expdate"
            ' cmbdate.Refresh()
        End If

    End Sub

    Private Sub cmbCurrencyStrike_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbCurrencyStrike.KeyPress
        Call numonly(e)
    End Sub



    Private Sub txtunit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtunit.KeyPress
        Call numonly(e)
    End Sub

    Private Sub txtrate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrate.KeyPress
        Call numonly(e)
    End Sub

    Private Sub txtCurrencyunit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrencyunit.KeyPress
        Call numonly(e)
    End Sub

    Private Sub txtCurrencyrate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrencyrate.KeyPress
        Call numonly(e)
    End Sub

    Private Sub cmbstrike_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbstrike.KeyPress
        Call numonly(e)
    End Sub

    Private Sub cmbCurrencyExpdate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyExpdate.Leave
        If cmbCurrencyComp.Text.Trim = "" Then
            MsgBox("Enter Security Name", MsgBoxStyle.Information)
            cmbCurrencyComp.Focus()
            Exit Sub
        End If
        If cmbCurrencyInstrument.Text.Trim = "" Then
            MsgBox("Select Instrument Name", MsgBoxStyle.Information)
            cmbCurrencyInstrument.Focus()
            Exit Sub
        End If

        If cmbCurrencyCP.SelectedValue.Trim = "" Then
            MsgBox("Select Call/Put/Futre", MsgBoxStyle.Information)
            cmbCurrencyCP.Focus()
            Exit Sub
        End If
        If cmbCurrencyStrike.Text.Trim = "0" And (Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate", MsgBoxStyle.Information)
            cmbCurrencyStrike.Focus()
            Exit Sub
        End If
        If cmbCurrencyExpdate.Text = "" Then
            MsgBox("Select date", MsgBoxStyle.Information)
            cmbCurrencyExpdate.Focus()
            Exit Sub
        End If
        Dim cp As String
        cp = ""
        If Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P" Then
            cp = UCase(cmbCurrencyCP.SelectedValue)
        Else
            cp = ""
        End If

        Dim script As String
        If cp = "" Then
            script = cmbCurrencyInstrument.SelectedValue & "  " & cmbCurrencyComp.SelectedValue & "  " & Format(CDate(cmbCurrencyExpdate.Text), "ddMMMyyyy")
        Else
            script = cmbCurrencyInstrument.SelectedValue & "  " & cmbCurrencyComp.SelectedValue & "  " & Format(CDate(cmbCurrencyExpdate.Text), "ddMMMyyyy") & "  " & Format(Val(cmbCurrencyStrike.Text), "###0.0000") & "  " & cp

        End If
        txtCurrencyscript.Text = script.Trim
        If DTCurrencyContract.Compute("count(symbol)", "script='" & txtCurrencyscript.Text.Trim & "'") <= 0 Then
            MsgBox("Not valid Script")
            form_Currency_clear()
            Exit Sub
        End If
    End Sub
    Private Sub form_Currency_clear()
        cmbCurrencyInstrument.Text = ""
        cmbCurrencyComp.Text = ""
        cmbCurrencyStrike.Text = "0"
        txtCurrencyscript.Text = ""
        txtCurrencyunit.Text = "0"
        txtCurrencyrate.Text = "0"
        cmbCurrencyComp.SelectedIndex = 0
        cmbCurrencyComp.Focus()
    End Sub

    Private Sub txtCurrencyrate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCurrencyrate.KeyDown
        If e.KeyCode = Keys.Enter Then
            DTPCurrencyEntryDate.Select()
        End If
    End Sub

    Private Sub DTPCurrencyEntryDate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DTPCurrencyEntryDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnCurrencySave.Select()
        End If
    End Sub
    Private Function form_Currency_validation() As Boolean
        If cmbCurrencyInstrument.Text.Trim = "" Then
            MsgBox("Select Instrument Name", MsgBoxStyle.Information)
            cmbCurrencyInstrument.Focus()
            Return False
        End If
        If cmbCurrencyComp.Text.Trim = "" Then
            MsgBox("Enter Company Name", MsgBoxStyle.Information)
            cmbCurrencyComp.Focus()
            Return False
        End If
        If cmbCurrencyStrike.Text.Trim = "0" And (Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate", MsgBoxStyle.Information)
            cmbCurrencyStrike.Focus()
            Return False
        End If
        If cmbCurrencyCP.Text = "" Then
            MsgBox("Select Call/Put/Futre", MsgBoxStyle.Information)
            cmbCurrencyCP.Focus()
            Return False
        End If
        If Not IsNumeric(txtCurrencyunit.Text) Then
            MsgBox("Enter Units", MsgBoxStyle.Information)
            txtCurrencyunit.Focus()
            Return False
        End If
        If Not IsNumeric(txtCurrencyrate.Text) Then
            MsgBox("Enter Rate", MsgBoxStyle.Information)
            txtCurrencyrate.Focus()
            Return False
        End If
        If txtCurrencyunit.Text = "0" Then
            MsgBox("Enter Units", MsgBoxStyle.Information)
            txtCurrencyunit.Focus()
            Return False
        End If
        If txtCurrencyrate.Text = "0" Then
            MsgBox("Enter Rate", MsgBoxStyle.Information)
            txtCurrencyrate.Focus()
            Return False
        End If

        Return True
    End Function
    Private Sub btnCurrencySave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCurrencySave.Click
        Dim strDate As String
        Try
            If txtCurrencyunit.Text = "" Then
                txtCurrencyunit.Text = 0
            End If
            If txtCurrencyrate.Text = "" Then
                txtCurrencyrate.Text = 0
            End If
            If form_Currency_validation() = True Then
                Varmultiplier = Currencymaster.Compute("MAX(multiplier)", "Script='" & txtCurrencyscript.Text & "'")
                Dim tkk As Long = CLng(Val(objTrad.select_Currency_Trading.Compute("MAX(token)", "script='" & txtCurrencyscript.Text & "'").ToString))
                If tkk > 0 Then
                    MsgBox(txtCurrencyscript.Text & " script already exist in Traded")
                    Exit Sub
                End If
                Dim a, a1, script1 As String
                Dim tk As Long
                GVarMAXCURRTradingOrderNo = GVarMAXCURRTradingOrderNo + 1
                'insert trade to Trading table
                objScript.InstrumentName = cmbCurrencyInstrument.Text
                objScript.Company = cmbCurrencyComp.Text
                objScript.Mdate = CDate(cmbCurrencyExpdate.Text).Date
                objScript.StrikeRate = Val(cmbCurrencyStrike.Text)
                objScript.CP = UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1))
                objScript.Script = txtCurrencyscript.Text.Trim
                objScript.Units = Val(txtCurrencyunit.Text) * Varmultiplier
                objScript.Rate = Val(txtCurrencyrate.Text)
                objScript.EntryDate = DTPCurrencyEntryDate.Value.Date
                If UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "C" Or UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "P" Then
                    a = Mid(txtCurrencyscript.Text, Len(txtCurrencyscript.Text) - 1, 1)
                    a1 = Mid(txtCurrencyscript.Text, Len(txtCurrencyscript.Text), 1)
                    If a = "C" Then
                        script1 = Mid(txtCurrencyscript.Text, 1, Len(txtCurrencyscript.Text) - 2) & "P" & a1
                    Else
                        script1 = Mid(txtCurrencyscript.Text, 1, Len(txtCurrencyscript.Text) - 2) & "C" & a1
                    End If
                    tk = CLng(DTCurrencyContract.Compute("max(token)", "script='" & script1 & "'").ToString)
                    objScript.Token = tk
                    For Each row As DataRow In GdtCurrencyTrades.Select("script='" & txtCurrencyscript.Text & "'")
                        If row("isliq") = True Then
                            objScript.Isliq = True ' "Yes"
                        Else
                            objScript.Isliq = False '"No"
                        End If
                        Exit For
                    Next
                Else
                    objScript.Token = 0
                    objScript.asset_tokan = 0
                    objScript.Isliq = False ' "No"
                End If
                objScript.orderno = GVarMAXCURRTradingOrderNo
                Dim tk1 As Long = CLng(Val(Currencymaster.Compute("max(token)", "script='" & txtCurrencyscript.Text & "'").ToString))
                If tk1 = 0 Then
                    MsgBox(txtCurrencyscript.Text & " does not exist in contract")
                End If
                objScript.Units = Val(txtCurrencyunit.Text) * Currencymaster.Compute("max(multiplier)", "script='" & txtCurrencyscript.Text & "'")

                'save trade to database trade table
                objScript.Insert_Currency_Trading()

                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                'get uid from trading table
                dtUid = objScript.select_Currency_trading_uid

                'Save to analysis's dtFOTrading table
                Dim tprow As DataRow
                tprow = GdtCurrencyTrades.NewRow
                tprow("uid") = dtUid.Rows(0)("uid")
                tprow("token") = 0
                tprow("mo") = Format(CDate(cmbCurrencyExpdate.Text).Date, "MM/yyyy")
                tprow("instrumentname") = cmbCurrencyInstrument.Text
                tprow("company") = cmbCurrencyComp.Text
                tprow("mdate") = CDate(cmbCurrencyExpdate.Text).Date
                tprow("Strikerate") = Val(cmbCurrencyStrike.Text)
                tprow("CP") = IIf(UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "X", "F", UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)))
                tprow("script") = txtCurrencyscript.Text.Trim
                tprow("qty") = Val(txtCurrencyunit.Text) * Varmultiplier
                tprow("Rate") = Val(txtCurrencyrate.Text)
                strDate = Format(DTPCurrencyEntryDate.Value.Date, "MMM/dd/yyyy")
                tprow("EntryDate") = Format(CDate(strDate).Date, "MMM/dd/yyyy")
                tprow("entry_date") = Format(CDate(strDate).Date, "MMM/dd/yyyy")
                tprow("isliq") = objScript.Isliq
                tprow("token1") = tk1
                tprow("orderno") = GVarMAXCURRTradingOrderNo
                tprow("entryno") = 0
                tprow("tot") = Val(txtCurrencyunit.Text) * Val(txtCurrencyrate.Text) * Varmultiplier
                tprow("issummary") = True
                tprow("isdisplay") = True
                GdtCurrencyTrades.Rows.Add(tprow)
                GdtCurrencyTrades.AcceptChanges()
                'calculate expense of inserted position
                Dim prExp, toExp As Double
                Dim optype As String = "C" & UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1))
                Call cal_prebal(DTPCurrencyEntryDate.Value.Date, cmbCurrencyComp.Text.Trim, optype, CInt(txtCurrencyunit.Text) * Varmultiplier, Val(txtCurrencyrate.Text), prExp, toExp)
                'insert position to database's analysis table also
                Dim dtAna As New DataTable
                dtAna = objAna.fill_table_process(txtCurrencyscript.Text.Trim, CInt(txtCurrencyunit.Text) * Varmultiplier, Val(txtCurrencyrate.Text), prExp, toExp, DTPCurrencyEntryDate.Value.Date)

                'insert Currency trade to analysis table
                objScript.insert_CurrencyTrade_in_maintable(txtCurrencyscript.Text.Trim, dtAna, prExp, toExp, DTPCurrencyEntryDate.Value.Date)
                MsgBox("Script saved successfully", MsgBoxStyle.Information)

                LastOpenPosition = cmbCurrencyComp.Text
            End If

            '***********************************************************************************                objAna.fill_table_process(txtscript.Text.Trim)
            txtCurrencyrate.Text = "0"
            txtCurrencyunit.Text = "0"
            openposyes = True
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnCurrencyClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCurrencyClear.Click
        Call form_Currency_clear()
    End Sub

    Private Sub btnCurrencyExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCurrencyExit.Click
        Me.Close()
    End Sub

    Private Sub OpenPosition_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    

    'Private Sub CmbInstru_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbInstru.Enter
    '    If CmbComp.Text.Trim <> "" And CmbComp.Items.Count > 0 Then

    '        Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbComp.Text & "'", "InstrumentName", DataViewRowState.CurrentRows)
    '        If dv.ToTable.Rows.Count <= 0 Then
    '            MsgBox("Select valid Security")
    '            CmbComp.Text = ""
    '            CmbComp.Focus()
    '            Exit Sub
    '        End If
    '        'CmbInstru.DataSource = dv.ToTable(True, "InstrumentName")
    '        'CmbInstru.DisplayMember = "InstrumentName"
    '        'CmbInstru.ValueMember = "InstrumentName"
    '        'If CmbComp.SelectedText <> "" Then
    '        '    CmbInstru.SelectedIndex = 0

    '        'End If
    '        Dim dvCP As DataView = New DataView(masterdata, "InstrumentName='" & CmbInstru.Text & "'", "option_type", DataViewRowState.CurrentRows)
    '        cmbcp.DataSource = dvCP.ToTable(True, "option_type")
    '        cmbcp.DisplayMember = "option_type"
    '        cmbcp.ValueMember = "option_type"
    '        If cmbcp.SelectedIndex = 0 Then
    '            cmbstrike.Text = 0
    '            cmbcp.Enabled = False
    '            cmbstrike.Enabled = False
    '        Else
    '            'Dim dv As DataView = New DataView(masterdata, "InstrumentName='" & CmbInstru.Text & "'", "option_type", DataViewRowState.CurrentRows)
    '            'cmbcp.DataSource = dv.ToTable(True, "option_type")
    '            'cmbcp.DisplayMember = "option_type"
    '            'cmbcp.ValueMember = "option_type"
    '            cmbcp.Enabled = True
    '            cmbstrike.Enabled = True
    '        End If
    '    End If
    '    If CmbComp.Text.Trim = "" Then
    '        CmbInstru.DataSource = Nothing
    '        cmbcp.DataSource = Nothing
    '        cmbstrike.DataSource = Nothing
    '        cmbdate.DataSource = Nothing
    '    End If
    'End Sub


    Private Sub CmbInstru_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbInstru.SelectedIndexChanged
        If Not CmbInstru Is Nothing And CmbInstru.Items.Count > 0 Then
            If CmbInstru.Text <> "" Then
                If UCase(Mid(CmbInstru.Text, 1, 3)) = "FUT" Then
                    cmbstrike.DataSource = Nothing

                    cmbstrike.Text = 0
                    Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbComp.Text & "' and InstrumentName='" & CmbInstru.Text & "' and option_type='" & cmbcp.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
                    cmbdate.DataSource = dv.ToTable(True, "expdate")
                    cmbdate.DisplayMember = "expdate"
                    cmbdate.ValueMember = "expdate"
                    Dim dvCP As DataView = New DataView(masterdata, "InstrumentName='" & CmbInstru.Text & "'", "option_type", DataViewRowState.CurrentRows)
                    cmbcp.DataSource = dvCP.ToTable(True, "option_type")
                    cmbcp.DisplayMember = "option_type"
                    cmbcp.ValueMember = "option_type"
                    cmbcp.Enabled = False
                    cmbstrike.Enabled = False
                    'cmbdate.Refresh()
                    'cmbdate.Focus()

                Else
                    Dim dv As DataView = New DataView(masterdata, "InstrumentName='" & CmbInstru.Text & "'", "option_type", DataViewRowState.CurrentRows)
                    cmbcp.DataSource = dv.ToTable(True, "option_type")
                    cmbcp.DisplayMember = "option_type"
                    cmbcp.ValueMember = "option_type"
                    cmbcp.Enabled = True
                    cmbstrike.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub cmbCurrencyInstrument_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyInstrument.SelectedIndexChanged
        If Not cmbCurrencyInstrument Is Nothing And cmbCurrencyInstrument.Items.Count > 0 Then
            If cmbCurrencyInstrument.Text <> "" Then
                If UCase(Mid(cmbCurrencyInstrument.Text, 1, 3)) = "FUT" Then
                    cmbCurrencyStrike.DataSource = Nothing

                    cmbCurrencyStrike.Text = 0
                    Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & CmbComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
                    cmbCurrencyExpdate.DataSource = dv.ToTable(True, "expdate")
                    cmbCurrencyExpdate.DisplayMember = "expdate"
                    cmbCurrencyExpdate.ValueMember = "expdate"
                    Dim dvCP As DataView = New DataView(DTCurrencyContract, "InstrumentName='" & cmbCurrencyInstrument.Text & "'", "option_type", DataViewRowState.CurrentRows)
                    cmbCurrencyCP.DataSource = dvCP.ToTable(True, "option_type")
                    cmbCurrencyCP.DisplayMember = "option_type"
                    cmbCurrencyCP.ValueMember = "option_type"
                    cmbCurrencyCP.Enabled = False
                    cmbCurrencyStrike.Enabled = False
                    'cmbdate.Refresh()
                    'cmbdate.Focus()

                Else
                    Dim dv As DataView = New DataView(DTCurrencyContract, "InstrumentName='" & cmbCurrencyInstrument.Text & "'", "option_type", DataViewRowState.CurrentRows)
                    cmbCurrencyCP.DataSource = dv.ToTable(True, "option_type")
                    cmbCurrencyCP.DisplayMember = "option_type"
                    cmbCurrencyCP.ValueMember = "option_type"
                    cmbCurrencyCP.Enabled = True
                    cmbCurrencyStrike.Enabled = True
                End If
            End If
        End If

    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click

        If TabControl1.SelectedIndex = 0 Then
            If masterdata.Rows.Count = 0 Then
                masterdata = New DataTable
                masterdata = cpfmaster
                Dim dv As DataView = New DataView(masterdata, "", "symbol", DataViewRowState.CurrentRows)
                CmbComp.DataSource = dv.ToTable(True, "symbol")
                CmbComp.DisplayMember = "symbol"
                CmbComp.ValueMember = "symbol"
                If dv.ToTable(True, "symbol").Compute("count(symbol)", "symbol='NIFTY'") > 0 Then
                    CmbComp.SelectedValue = "NIFTY"
                    CmbComp.Select()
                End If
            End If
            
        ElseIf TabControl1.SelectedIndex = 1 Then
            If eqmasterdata.Rows.Count = 0 Then
                eqmasterdata = New DataTable
                eqmasterdata = eqmaster
                Application.DoEvents()
                Dim dv1 As DataView = New DataView(eqmasterdata, "", "symbol", DataViewRowState.CurrentRows)
                cmbeqcomp.DataSource = dv1.ToTable(True, "symbol")
                Application.DoEvents()
                cmbeqcomp.DisplayMember = "symbol"
                cmbeqcomp.ValueMember = "symbol"
                'If eqmasterdata.Rows.Count > 0 Then
                '    'cmbeqcomp.SelectedValue = dv1.ToTable(True, "Symbol").Rows(1).Item(0).ToString()
                '    'cmbeqcomp.Select()
                '    cmbeqcomp.SelectedIndex = 1 'dv1.ToTable(True, "Symbol").Rows(1).Item(0).ToString()
                cmbeqcomp.Select()
                'End If
                '   cmbeqcomp.SelectedValue = 

            End If

        ElseIf TabControl1.SelectedIndex = 2 Then
            If DTCurrencyContract.Rows.Count = 0 Then
                DTCurrencyContract = New DataTable
                DTCurrencyContract = Currencymaster
                Dim dvCurr As DataView = New DataView(DTCurrencyContract, "", "symbol", DataViewRowState.CurrentRows)
                cmbCurrencyComp.DataSource = dvCurr.ToTable(True, "symbol")
                cmbCurrencyComp.DisplayMember = "symbol"
                cmbCurrencyComp.ValueMember = "symbol"
                cmbeqopt.SelectedValue = "Symbol"
                cmbCurrencyComp.Select()
            End If

        End If

    End Sub

  
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        'If TabControl1.SelectedIndex = 0 Then
        '    CmbComp.Select()
        '    'ElseIf TabControl1.SelectedIndex = 1 Then
        '    '    cmbeqcomp.Select()
        '    'ElseIf TabControl1.SelectedIndex = 2 Then
        '    '    cmbCurrencyComp.Select()
        'End If
        TabControl1_Click(sender, e)
    End Sub

    Private Sub txtunit_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtunit.Enter
        txtunit.Select()
    End Sub

    Private Sub txtrate_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrate.Enter
        txtrate.Select()

    End Sub

    Private Sub cmbdate_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdate.SelectedValueChanged
        If cmbdate.Text <> "" Then
            Dim cp As String
            cp = ""
            If Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P" Then
                cp = UCase(cmbcp.SelectedValue)
            Else
                cp = ""
            End If
            Dim script As String
            If cp = "" Then
                script = CmbInstru.Text & "  " & CmbComp.Text & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy")
            Else
                script = CmbInstru.Text & "  " & CmbComp.Text & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy") & "  " & Format(Val(cmbstrike.Text), "###0.00") & "  " & cp
            End If
            txtscript.Text = script.Trim
            ' txtunit.Select()
        End If

    End Sub

End Class