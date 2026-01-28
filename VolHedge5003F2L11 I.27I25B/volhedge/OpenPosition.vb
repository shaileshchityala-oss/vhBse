Public Class OpenPosition
    Dim objScript As script = New script
    Dim masterdata As DataTable = New DataTable
    Dim dtUid As New DataTable
    Dim eqmasterdata As DataTable = New DataTable
    Dim objTrad As New trading
    Dim ED As New clsEnDe
    Dim DTCurrencyContract As DataTable = New DataTable

    Dim cmbheight As Boolean = False
    Dim cmbh As Integer
    Dim objAna As New analysisprocess
    Public openposition As Boolean
    Dim Varmultiplier As Double = 0
    Public openposyes As Boolean = False
    Dim IsUserDefTag As Boolean = False
    Dim sUdSymbol As String = ""
    Dim sUdName As String = ""

    Dim mContracts As CContract

    Public Sub ShowForm(ByVal sUDTag As String, ByVal sUDTagName As String)
        sUdSymbol = GetSymbol(sUDTag.Trim)
        sUdName = sUDTagName.Trim

        If sUDTag.Contains("/") Then
            IsUserDefTag = True
        Else
            IsUserDefTag = False
        End If

        Me.ShowDialog()
    End Sub

    Private Sub OpenPosition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mIsLoading = True
        If openposition = False Then
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
        OPPOS_ENTRYDATE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='OPPOS_ENTRYDATE'").ToString)
        If OPPOS_ENTRYDATE = 1 Then
            dtent.Value = Now.Date 'DateAdd(DateInterval.Day, 1, Now.Date)
        Else
            dtent.Value = DateAdd(DateInterval.Day, -1, Now.Date)
        End If
        If OPPOS_ENTRYDATE = 1 Then
            dteqent.Value = Now.Date
        Else
            dteqent.Value = DateAdd(DateInterval.Day, -1, Now.Date)
        End If
        If OPPOS_ENTRYDATE = 1 Then
            DTPCurrencyEntryDate.Value = Now.Date
        Else
            DTPCurrencyEntryDate.Value = DateAdd(DateInterval.Day, -1, Now.Date)
        End If

        TabControl1.SelectedIndex = 0
        openposyes = False

        masterdata = cpfmaster
        eqmasterdata = eqmaster

        'masterdata = New DataTable
        'masterdata = cpfmasterFgetrate


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

        mContracts = New CContract()

        FillExchagesCmb(cmbEqExchange)
        FillExchagesCmb(cmbFoExchange)

        Call TabControl1_Click(sender, e)
        mIsLoading = False
    End Sub
    Dim mIsLoading As Boolean


    Public Sub FillExchagesCmb(pCmb As ComboBox)
        pCmb.Items.Add("NSE")
        pCmb.Items.Add("BSE")
        pCmb.SelectedIndex = 0
        pCmb.DropDownStyle = ComboBoxStyle.DropDownList
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
        Dim exchange As String = cmbFoExchange.Text
        cmbcp.Enabled = True
        cmbstrike.Enabled = True
        If CmbInstru.Text.Trim <> "" And CmbInstru.Items.Count > 0 Then
            If (UCase(CmbInstru.Text) = "FUTIDX" Or UCase(CmbInstru.Text) = "FUTSTK" Or UCase(CmbInstru.Text) = "FUTIVX" Or UCase(CmbInstru.Text) = "OPTIDX" Or UCase(CmbInstru.Text) = "OPTSTK") Then
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
                            If gstr_ProductName = "OMI" Then
                                dv = New DataView(masterdata, "symbol='" & CmbComp.Text & "' and InstrumentName='" & CmbInstru.Text & "'   ", "strike_price", DataViewRowState.CurrentRows)
                            Else
                                dv = New DataView(masterdata, "symbol='" & CmbComp.Text & "' and InstrumentName='" & CmbInstru.Text & "'  AND expdate1 >='" & Now.Date & "' ", "strike_price", DataViewRowState.CurrentRows)
                            End If

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
                getrateFo()
            Else
                MsgBox("Select valid Instrument Name.")
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
    Private Function GetAtmStrike() As Double
        Try
            If NetMode <> "UDP" Then

                ' Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbComp.Text & "' and option_type='XX'", "strike_price", DataViewRowState.CurrentRows)
                Dim fdv() As DataRow
                fdv = masterdata.Select("symbol='" & CmbComp.Text & "' and option_type='XX'", "strike_price  desc")

                Dim token As Integer = Convert.ToInt32(fdv(0).Item("ftoken"))
                Dim strike As Double

                Dim dt As String = getrate(Convert.ToInt64(token))
                Dim futRate As String = Math.Round(ED.DFo(Val(dt.ToString())), 2)

                Dim fdv1() As DataRow
                If fltpprice.Contains(Convert.ToInt64(token)) Then
                    strike = Math.Round(Val(fltpprice(Convert.ToInt64(token))), 2)
                    fdv1 = masterdata.Select("symbol='" & CmbComp.Text & "' and option_type<>'XX' And  Strike_Price < '" & strike & "'", "strike_price  desc")
                    strike = Convert.ToDouble(fdv1(0).Item("strike_price"))
                Else
                    fdv1 = masterdata.Select("symbol='" & CmbComp.Text & "' and option_type<>'XX' And  Strike_Price < '" & futRate & "'", "strike_price  desc")
                    strike = Convert.ToDouble(fdv1(0).Item("strike_price"))
                End If

                Return strike
            End If
        Catch ex As Exception
            Return 0
        End Try
        'Return 0
    End Function
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
                getrateFo()

                Dim strike As Double = GetAtmStrike()
                If strike <> 0 Then
                    cmbstrike.Text = ""
                    cmbstrike.SelectedText = strike
                    cmbstrike.Text = strike
                End If

            Else
                MsgBox("Select valid Option type.")
                cmbcp.Text = ""
                cmbcp.Focus()
            End If
        End If
        'txtrate.Text = 0

    End Sub
    Private Sub cmbcp_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbcp.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbstrike.Select()
        End If
    End Sub
    Private Sub cmbstrike_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstrike.Leave
        Dim exchange As String = cmbFoExchange.Text
        If cmbstrike.Text.Trim = "" Then
            cmbstrike.Text = 0
        End If
        If gstr_ProductName = "OMI" Then
            If (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") And cmbstrike.Text <> "0" Then
                Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbComp.Text & "' and InstrumentName='" & CmbInstru.Text & "' and option_type='" & cmbcp.Text & "' and strike_price=" & Val(cmbstrike.Text) & "  ", "strike_price", DataViewRowState.CurrentRows)
                If dv.ToTable.Rows.Count <= 0 Then
                    MsgBox("Select valid Strike rate.")
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
                Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbComp.Text & "' and InstrumentName='" & CmbInstru.Text & "' and option_type='" & cmbcp.Text & "' AND exchange ='" & exchange & "'", "strike_price", DataViewRowState.CurrentRows)
                cmbdate.DataSource = dv.ToTable(True, "expdate")
                cmbdate.DisplayMember = "expdate"
                cmbdate.ValueMember = "expdate"
            End If
        Else
            If (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") And cmbstrike.Text <> "0" Then
                Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbComp.Text & "' and InstrumentName='" & CmbInstru.Text & "' and option_type='" & cmbcp.Text & "' and strike_price=" & Val(cmbstrike.Text) & " AND expdate1 >='" & Now.Date & "' AND exchange ='" & exchange & "'", "strike_price", DataViewRowState.CurrentRows)
                If dv.ToTable.Rows.Count <= 0 Then
                    MsgBox("Select valid Strike rate.")
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
                Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbComp.Text & "' and InstrumentName='" & CmbInstru.Text & "' and option_type='" & cmbcp.Text & "' AND expdate1 >='" & Now.Date & "' AND exchange ='" & exchange & "'", "strike_price", DataViewRowState.CurrentRows)
                cmbdate.DataSource = dv.ToTable(True, "expdate")
                cmbdate.DisplayMember = "expdate"
                cmbdate.ValueMember = "expdate"
            End If
        End If

        getrateFo()
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
                MsgBox("Select valid Security.")
                CmbComp.Text = ""
                CmbComp.Focus()
                Exit Sub
            End If
            CmbInstru.DisplayMember = "InstrumentName"
            CmbInstru.ValueMember = "InstrumentName"
            CmbInstru.DataSource = dv.ToTable(True, "InstrumentName")

            'If CmbComp.SelectedText <> "" Then
            '    CmbInstru.SelectedIndex = 0
            'End If
            getrateFo()
        End If
        If CmbComp.Text.Trim = "" Then
            CmbInstru.DataSource = Nothing
            cmbcp.DataSource = Nothing
            cmbstrike.DataSource = Nothing
            cmbdate.DataSource = Nothing
        End If
        'txtrate.Text = 0

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
            MsgBox("Enter Security Name.", MsgBoxStyle.Information)
            CmbComp.Focus()
            Exit Sub
        End If
        If CmbInstru.Text.Trim = "" Then
            MsgBox("Select Instrument Name.", MsgBoxStyle.Information)
            CmbInstru.Focus()
            Exit Sub
        End If

        If cmbcp.Text.Trim = "" Then
            MsgBox("Select Call/Put/Futre", MsgBoxStyle.Information)
            cmbcp.Focus()
            Exit Sub
        End If
        If cmbstrike.Text.Trim = "0" And (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate.", MsgBoxStyle.Information)
            cmbstrike.Focus()
            Exit Sub
        End If
        If cmbdate.Text = "" Then
            MsgBox("Select date.", MsgBoxStyle.Information)
            cmbdate.Focus()
            Exit Sub
        End If
        Dim cp As String
        cp = ""
        If Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P" Then
            cp = UCase(cmbcp.Text)
        Else
            cp = ""
        End If
        Dim script As String
        If cp = "" Or CmbInstru.Text.Substring(0, 3) = "FUT" Then
            script = CmbInstru.Text & "  " & CmbComp.Text & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy")
        Else
            script = CmbInstru.Text & "  " & CmbComp.Text & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy") & "  " & Format(Val(cmbstrike.Text), "###0.00") & "  " & cp
        End If
        txtscript.Text = script.Trim
        'MsgBox(txtscript.Text & "-" & masterdata.Rows.Count & "--" & "" & masterdata.Select("Script='" & txtscript.Text & "'").Length) 'masterdata.Compute("count(symbol)", "script='" & txtscript.Text.Trim & "'"))
        If masterdata.Compute("count(symbol)", "script='" & txtscript.Text.Trim & "'") <= 0 Then

            MsgBox("Not valid Script.")

            form_clear()
            Exit Sub
        End If
        Dim dr As DataRow() = masterdata.Select("Script='" & txtscript.Text & "'")
        If Not dr.Length = 0 Then
            lbllotsize.Text = dr(0)("lotsize")
        End If
        getrateFo()

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
            MsgBox("Select Instrument Name.", MsgBoxStyle.Information)
            CmbInstru.Focus()
            form_validation = False
            Exit Function
        End If
        If CmbComp.Text.Trim = "" Then
            MsgBox("Enter Company Name.", MsgBoxStyle.Information)
            CmbComp.Focus()
            form_validation = False
            Exit Function
        End If
        If cmbstrike.Text.Trim = "0" And (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate.", MsgBoxStyle.Information)
            cmbstrike.Focus()
            form_validation = False
            Exit Function
        End If
        If cmbcp.Text = "" Then
            MsgBox("Select Call/Put/Futre.", MsgBoxStyle.Information)
            cmbcp.Focus()
            form_validation = False
            Exit Function
        End If
        If Not IsNumeric(txtunit.Text) Then
            MsgBox("Enter Units.", MsgBoxStyle.Information)
            txtunit.Focus()
            form_validation = False
            Exit Function
        End If
        If Not IsNumeric(txtrate.Text) Then
            MsgBox("Enter Rate.", MsgBoxStyle.Information)
            txtrate.Focus()
            form_validation = False
            Exit Function
        End If
        If Val(txtunit.Text) = "0" Then
            MsgBox("Enter Units.", MsgBoxStyle.Information)
            txtunit.Text = "0"
            txtunit.Select()
            form_validation = False
            Exit Function
        End If
        If Val(txtrate.Text) = "0" Then
            MsgBox("Enter Rate.", MsgBoxStyle.Information)
            txtrate.Text = "0"
            txtrate.Select()
            form_validation = False
            Exit Function
        End If
        form_validation = True
    End Function
    '500051 Code
    '------------------------

    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click

        Dim exchange As String = cmbFoExchange.Text

        Try
            If IsUserDefTag = True Then
                If CmbComp.Text.ToUpper <> GetSymbol(sUdName).ToUpper Then
                    MsgBox("Invalid Security Selected." & vbCrLf & "Try again With : " & GetSymbol(sUdName).ToUpper)
                    Exit Sub
                End If
            End If

            'maintable.Rows.Count = 0
            If txtunit.Text = "" Then
                txtunit.Text = 0
            End If
            If txtrate.Text = "" Then
                txtrate.Text = 0
            End If
            txtscript.Text = txtscript.Text.ToUpper
            If form_validation() Then
                Dim tkk As Long = CLng(Val(GdtFOTrades.Compute("max(token)", "script='" & txtscript.Text & "' AND exchange='" & exchange & "'").ToString))
                If tkk > 0 Then
                    MsgBox(txtscript.Text & " script already exist in Traded")
                    Exit Sub
                End If
                Dim a, a1, script1 As String
                Dim tk As Long
                GVarMAXFOTradingOrderNo = GVarMAXFOTradingOrderNo + 1
                'insert trade to Trading table
                objScript.InstrumentName = CmbInstru.Text
                If IsUserDefTag = True Then
                    objScript.Company = sUdName
                Else
                    objScript.Company = CmbComp.Text
                End If

                objScript.Mdate = CDate(cmbdate.Text).Date
                objScript.StrikeRate = Val(cmbstrike.Text)
                objScript.CP = UCase(Mid(cmbcp.SelectedValue, 1, 1))
                objScript.Script = txtscript.Text.Trim
                objScript.Units = Val(txtunit.Text)
                objScript.Rate = Val(txtrate.Text)
                objScript.Dealer = Convert.ToString("OP")
                objScript.EntryDate = CDate(dtent.Value.Date.ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt"))
                objScript.Exchange = exchange

                If UCase(Mid(cmbcp.SelectedValue, 1, 1)) = "C" Or UCase(Mid(cmbcp.SelectedValue, 1, 1)) = "P" Then
                    a = Mid(txtscript.Text, Len(txtscript.Text) - 1, 1)
                    a1 = Mid(txtscript.Text, Len(txtscript.Text), 1)
                    If a = "C" Then
                        script1 = Mid(txtscript.Text, 1, Len(txtscript.Text) - 2) & "P" & a1
                    Else
                        script1 = Mid(txtscript.Text, 1, Len(txtscript.Text) - 2) & "C" & a1
                    End If
                    script1 = script1.ToUpper
                    Dim strFilter As String = "script='" & script1 & "' AND exchange='" & exchange & "'"
                    If masterdata.Compute("max(token)", strFilter).ToString() = "" Then
                        tk = 0
                    Else
                        tk = CLng(masterdata.Compute("max(token)", "script='" & script1 & "' AND exchange='" & exchange & "'").ToString)
                    End If

                    objScript.Token = tk
                    For Each row As DataRow In GdtFOTrades.Select("script='" & txtscript.Text & "' And company='" & IIf(IsUserDefTag, sUdName, CmbComp.Text) & "'")
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
                    MsgBox("Contract Exist.")
                    Exit Sub
                End If
                If NetMode <> "UDP" Then
                    Objsql.AppendFoTokens(CLng(tk1).ToString)
                End If

                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                'get uid from trading table
                dtUid = objScript.select_trading_uid
                'Add datarow to analysis's dtFOTrading table
                Dim DtTempFO_trad As New DataTable
                DtTempFO_trad = GdtFOTrades.Clone

                Dim tprow As DataRow
                tprow = DtTempFO_trad.NewRow
                tprow("uid") = dtUid.Rows(0)("uid")
                tprow("token") = 0
                tprow("mo") = Format(CDate(cmbdate.Text).Date, "MM/yyyy")
                tprow("instrumentname") = CmbInstru.Text
                If IsUserDefTag = True Then
                    tprow("company") = sUdName
                Else
                    tprow("company") = CmbComp.Text
                End If

                tprow("mdate") = CDate(cmbdate.Text).Date
                tprow("Strikerate") = Val(cmbstrike.Text)
                tprow("CP") = IIf(UCase(Mid(cmbcp.SelectedValue, 1, 1)) = "X", "F", UCase(Mid(cmbcp.SelectedValue, 1, 1)))
                tprow("script") = txtscript.Text.Trim
                tprow("qty") = Val(txtunit.Text)
                tprow("Rate") = Val(txtrate.Text)
                tprow("EntryDate") = CDate(dtent.Value.Date.ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt")) 'Format(dtent.Value.Date, "MMM/dd/yyyy")
                tprow("entry_date") = Format(dtent.Value.Date, "MMM/dd/yyyy")
                tprow("isliq") = objScript.Isliq
                tprow("token1") = tk1
                tprow("orderno") = GVarMAXFOTradingOrderNo
                tprow("entryno") = 0
                tprow("tot") = Val(txtunit.Text) * Val(txtrate.Text)
                tprow("tot2") = Val(txtunit.Text) * (Val(txtrate.Text) + Val(cmbstrike.Text))
                tprow("issummary") = True
                tprow("isdisplay") = True
                tprow("exchange") = exchange
                DtTempFO_trad.Rows.Add(tprow)



                Call insert_FOTradeToGlobalTable(DtTempFO_trad)
                GdtFOTrades.Rows(GdtFOTrades.Rows.Count - 1).Item("Uid") = tprow("uid")
                Call GSub_CalculateExpense(DtTempFO_trad, "FO", True)

                'calculate expense of inserted position
                Dim prExp, toExp As Double
                'cal_prebal(dtent.Value.Date, CmbComp.Text.Trim, UCase(Mid(cmbcp.SelectedValue, 1, 1)), CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp)
                'insert position to database's analysis table also
                Dim dtAna As New DataTable
                'dtAna = objAna.fill_table_process(txtscript.Text.Trim, CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp, dtent.Value.Date)
                'insert FO trade to analysis table
                Dim dtEntdate As Date = IIf(IsDBNull(dtent.Value.Date), Now.Date, dtent.Value.Date)
                If IsUserDefTag = True Then
                    dtAna = objAna.fill_table_process(txtscript.Text.Trim, CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp, dtent.Value.Date, sUdName, exchange)
                    'insert FO trade to analysis table
                    objScript.insert_FOTrade_in_maintable(txtscript.Text.Trim, dtAna, prExp, toExp, dtEntdate, sUdName, exchange)
                Else
                    dtAna = objAna.fill_table_process(txtscript.Text.Trim, CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp, dtent.Value.Date, CmbComp.Text, exchange)
                    'insert FO trade to analysis table
                    objScript.insert_FOTrade_in_maintable(txtscript.Text.Trim, dtAna, prExp, toExp, dtEntdate, CmbComp.Text, exchange)
                End If
                MsgBox("Script saved Successfully.", MsgBoxStyle.Information)
                If IsUserDefTag = True Then
                    LastOpenPosition = sUdName
                Else
                    LastOpenPosition = CmbComp.Text
                End If

            End If
            '***********************************************************************************                objAna.fill_table_process(txtscript.Text.Trim)
            ' If dtent.Value.Date < Now.Date Then
            'End If
            ' txtrate.Text = "0"
            txtunit.Text = "0"
            openposyes = True
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    '---------------------------
    'Old Version Code
    'Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
    '    Dim strDate As String
    '    Try
    '        'maintable.Rows.Count = 0
    '        If txtunit.Text = "" Then
    '            txtunit.Text = 0
    '        End If
    '        If txtrate.Text = "" Then
    '            txtrate.Text = 0
    '        End If
    '        If form_validation() Then
    '            Dim tkk As Long = CLng(Val(objTrad.Trading.Compute("max(token)", "script='" & txtscript.Text & "'").ToString))
    '            If tkk > 0 Then
    '                MsgBox(txtscript.Text & " script already exist in Traded")
    '                Exit Sub
    '            End If
    '            Dim a, a1, script1 As String
    '            Dim tk As Long
    '            GVarMAXFOTradingOrderNo = GVarMAXFOTradingOrderNo + 1
    '            'insert trade to Trading table

    '            objScript.InstrumentName = CmbInstru.Text
    '            objScript.Company = CmbComp.Text
    '            objScript.Mdate = CDate(cmbdate.Text).Date
    '            objScript.StrikeRate = Val(cmbstrike.Text)
    '            objScript.CP = UCase(Mid(cmbcp.SelectedValue, 1, 1))
    '            objScript.Script = txtscript.Text.Trim
    '            objScript.Units = Val(txtunit.Text)
    '            objScript.Rate = Val(txtrate.Text)
    '            objScript.EntryDate = Format(dtent.Value.Date, "dd-MMM-yyyy")
    '            If UCase(Mid(cmbcp.SelectedValue, 1, 1)) = "C" Or UCase(Mid(cmbcp.SelectedValue, 1, 1)) = "P" Then
    '                a = Mid(txtscript.Text, Len(txtscript.Text) - 1, 1)
    '                a1 = Mid(txtscript.Text, Len(txtscript.Text), 1)
    '                If a = "C" Then
    '                    script1 = Mid(txtscript.Text, 1, Len(txtscript.Text) - 2) & "P" & a1
    '                Else
    '                    script1 = Mid(txtscript.Text, 1, Len(txtscript.Text) - 2) & "C" & a1
    '                End If
    '                tk = CLng(masterdata.Compute("max(token)", "script='" & script1 & "'").ToString)
    '                objScript.Token = tk
    '                For Each row As DataRow In GdtFOTrades.Select("script='" & txtscript.Text & "'")
    '                    If row("isliq") = True Then
    '                        objScript.Isliq = True ' "Yes"
    '                    Else
    '                        objScript.Isliq = False '"No"
    '                    End If
    '                    Exit For
    '                Next
    '            Else
    '                objScript.Token = 0
    '                objScript.asset_tokan = 0
    '                objScript.Isliq = False ' "No"
    '            End If
    '            objScript.orderno = GVarMAXFOTradingOrderNo
    '            Dim tk1 As Long = CLng(Val(masterdata.Compute("max(token)", "script='" & txtscript.Text & "'").ToString))
    '            If tk1 = 0 Then
    '                MsgBox(txtscript.Text & " does not exist in contract")
    '            End If
    '            'save trade to database trade table
    '            Dim DTDuplicate As DataTable
    '            DTDuplicate = New DataTable
    '            DTDuplicate = objScript.Insert()
    '            If DTDuplicate.Rows.Count - 1 > 0 Then
    '                MsgBox("Contract Exist")
    '                Exit Sub
    '            End If
    '            '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    '            'get uid from trading table
    '            dtUid = objScript.select_trading_uid
    '            'Save to analysis's dtFOTrading table
    '            Dim tprow As DataRow
    '            tprow = GdtFOTrades.NewRow
    '            tprow("uid") = dtUid.Rows(0)("uid")
    '            tprow("token") = 0
    '            tprow("mo") = Format(CDate(cmbdate.Text).Date, "MM/yyyy")
    '            tprow("instrumentname") = CmbInstru.Text
    '            tprow("company") = CmbComp.Text
    '            tprow("mdate") = CDate(cmbdate.Text).Date
    '            tprow("Strikerate") = Val(cmbstrike.Text)
    '            tprow("CP") = IIf(UCase(Mid(cmbcp.SelectedValue, 1, 1)) = "X", "F", UCase(Mid(cmbcp.SelectedValue, 1, 1)))
    '            tprow("script") = txtscript.Text.Trim
    '            tprow("qty") = Val(txtunit.Text)
    '            tprow("Rate") = Val(txtrate.Text)
    '            strDate = Format(dtent.Value.Date, "MMM/dd/yyyy")
    '            tprow("EntryDate") = Format(CDate(strDate).Date, "MMM/dd/yyyy")
    '            tprow("entry_date") = Format(CDate(strDate).Date, "MMM/dd/yyyy")
    '            tprow("isliq") = objScript.Isliq
    '            tprow("token1") = tk1
    '            tprow("orderno") = GVarMAXFOTradingOrderNo
    '            tprow("entryno") = 0
    '            tprow("tot") = Val(txtunit.Text) * Val(txtrate.Text)
    '            tprow("issummary") = True
    '            tprow("isdisplay") = True
    '            GdtFOTrades.Rows.Add(tprow)
    '            GdtFOTrades.AcceptChanges()

    '            'calculate expense of inserted position
    '            Dim prExp, toExp As Double

    '            cal_prebal(Format(dtent.Value.Date, "dd-MMM-yyyy"), CmbComp.Text.Trim, UCase(Mid(cmbcp.SelectedValue, 1, 1)), CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp)
    '            'insert position to database's analysis table also
    '            Dim dtAna As New DataTable
    '            dtAna = objAna.fill_table_process(txtscript.Text.Trim, CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp, dtent.Value.Date)
    '            'insert FO trade to analysis table
    '            objScript.insert_FOTrade_in_maintable(txtscript.Text.Trim, dtAna, prExp, toExp, dtent.Value.Date)
    '            MsgBox("Script saved Successfully", MsgBoxStyle.Information)
    '            LastOpenPosition = CmbComp.Text
    '        End If
    '        '***********************************************************************************                objAna.fill_table_process(txtscript.Text.Trim)
    '        ' If dtent.Value.Date < Now.Date Then
    '        'End If
    '        txtrate.Text = "0"
    '        txtunit.Text = "0"
    '        openposyes = True
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    'End Sub


    'Private Sub cal_prebal(ByVal date1 As Date, ByVal compname As String, ByVal optype As String, ByVal qty As Integer, ByVal rate As Double, ByRef prExp As Double, ByRef toExp As Double)
    '    Dim flgNew As Boolean = False
    '    Dim prow As DataRow() = prebal.Select("tdate =#" & date1 & "# and company='" & compname & "'")
    '    If Not prow Is Nothing Then
    '        If prow.Length = 0 Then
    '            ReDim prow(1)
    '            prow(0) = prebal.NewRow
    '            prow(0)("company") = compname
    '            prow(0)("tdate") = date1
    '            prow(0)("stbal") = 0
    '            prow(0)("futbal") = 0
    '            prow(0)("optbal") = 0
    '            flgNew = True
    '        End If
    '    End If
    '    Dim exp As Double = 0
    '    'calculate expense 
    '    '*****************************************************
    '    If optype = "E" Then 'for equity
    '        Dim stexp, stexp1, dst, ndst As Double
    '        'If date1 = Today.Date Then 'delivery base expense
    '        stexp = Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2)
    '        stexp1 = Math.Abs(Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "qty < 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2))
    '        dst = stexp - stexp1
    '        If dst > 0 Then
    '            ndst = stexp1
    '            exp = ((dst * dbl) / dblp)
    '            exp += ((stexp1 * ndbs) / ndbsp)
    '            exp += ((stexp1 * ndbl) / ndblp)
    '        Else
    '            ndst = stexp
    '            dst = -dst
    '            exp += ((dst * dbs) / dbsp)
    '            exp += ((stexp * ndbl) / ndblp)
    '            exp += ((stexp * ndbs) / ndbsp)
    '        End If
    '        prow(0)("stbal") = exp
    '        'Else 'for delivery base
    '        '    If qty > 0 Then 'long expense
    '        '        exp = (qty * rate * dbl) / dblp
    '        '    Else 'short expense
    '        '        exp = -(qty * rate * dbs) / dbsp
    '        '    End If
    '        '    prow(0)("stbal") = Val(prow(0)("stbal")) + exp
    '        '    exp = prow(0)("stbal")
    '        ' End If
    '        '*******************************************************
    '    ElseIf (optype = "X" Or optype = "" Or optype = "F") Then 'for future
    '        exp = 0
    '        If qty > 0 Then
    '            exp = (qty * rate * futl) / futlp
    '        Else
    '            exp = -(qty * rate * futs) / futsp
    '        End If
    '        prow(0)("futbal") = Val(prow(0)("futbal")) + exp
    '        '************************************************************
    '    Else 'for option
    '        exp = 0
    '        If Val(spl) <> 0 Then
    '            If qty > 0 Then
    '                exp = Val((qty * rate * spl) / splp)
    '            Else
    '                qty = -qty
    '                exp = Val((qty * rate * sps) / spsp)
    '            End If
    '        Else
    '            If qty > 0 Then
    '                exp = Val((qty * rate * prel) / prelp)
    '            Else
    '                qty = -qty
    '                exp = Val((qty * rate * pres) / presp)
    '            End If
    '        End If
    '        prow(0)("optbal") = Val(prow(0)("optbal")) + exp
    '    End If

    '    If (optype = "CX" Or optype = "" Or optype = "CF") Then ''for currency
    '        exp = 0
    '        If qty > 0 Then
    '            exp = (qty * rate * currfutl) / currfutlp
    '        Else
    '            exp = -(qty * rate * currfuts) / currfutsp
    '        End If
    '        prow(0)("futbal") = Val(prow(0)("futbal")) + exp
    '    ElseIf (optype = "CC" Or optype = "CP") Then
    '        exp = 0
    '        If Val(spl) <> 0 Then
    '            If qty > 0 Then
    '                exp = Val((qty * rate * currspl) / currsplp)
    '            Else
    '                qty = -qty
    '                exp = Val((qty * rate * currsps) / currspsp)
    '            End If
    '        Else
    '            If qty > 0 Then
    '                exp = Val((qty * rate * currprel) / currprelp)
    '            Else
    '                qty = -qty
    '                exp = Val((qty * rate * currpres) / currpresp)
    '            End If
    '        End If
    '        prow(0)("optbal") = Val(prow(0)("optbal")) + exp
    '    End If


    '    prow(0)("tot") = Val(prow(0)("stbal")) + Val(prow(0)("futbal")) + Val(prow(0)("optbal"))
    '    prebal.AcceptChanges()

    '    'for today expense
    '    If date1 = Today.Date Then
    '        toExp = exp
    '    Else
    '        prExp = exp
    '    End If

    '    If flgNew = True Then 'add new row
    '        prebal.Rows.Add(prow(0))
    '        objTrad.insert_prebal(prow(0))
    '    Else 'update expense
    '        objTrad.Delete_prBal(date1.Date, compname)
    '        objTrad.insert_prebal(prow(0))
    '        'With prow(0)
    '        '    objTrad.update_prebal(date1.Date, .Item("stbal"), .Item("futbal"), .Item("optbal"), .Item("company"))
    '        'End With

    '    End If

    '    'If date1 < Now.Date Then ' previous trades expense calulation
    '    '    Dim prebalance As New DataTable
    '    '    prebalance = objTrad.prebal
    '    '    Dim addprebal As New DataTable
    '    '    addprebal = New DataView(prebalance, "company = '" & compname & "' and tdate=#" & date1 & "#", "", DataViewRowState.CurrentRows).ToTable
    '    '    'if no prebalance for position then add new position
    '    '    If addprebal.Rows.Count = 0 Then
    '    '        'With addprebal.Columns
    '    '        '    ' .Add("tdate", GetType(Date))
    '    '        '    .Add("stbal", GetType(Double))
    '    '        '    .Add("futbal", GetType(Double))
    '    '        '    .Add("optbal", GetType(Double))
    '    '        '    '.Add("company", GetType(String))
    '    '        'End With
    '    '        Dim drow As DataRow = addprebal.NewRow
    '    '        addprebal.Rows.Add(drow)
    '    '        addprebal.Rows(0)("company") = compname
    '    '        addprebal.Rows(0)("tdate") = date1
    '    '        addprebal.Rows(0)("stbal") = 0
    '    '        addprebal.Rows(0)("futbal") = 0
    '    '        addprebal.Rows(0)("optbal") = 0
    '    '    End If
    '    '    addprebal.AcceptChanges()

    '    '    If optype = "E" Then
    '    '        If qty > 0 Then
    '    '            prExp = Val((qty * rate * dbl) / dblp)
    '    '            addprebal.Rows(0)("stbal") += prExp
    '    '        Else
    '    '            qty = -qty
    '    '            prExp = Val((qty * rate * dbs) / dbsp)
    '    '            addprebal.Rows(0)("stbal") += prExp
    '    '        End If
    '    '    ElseIf optype = "X" Or optype = "F" Or optype = "" Then
    '    '        If qty > 0 Then
    '    '            prExp = Val((qty * rate * futl) / futlp)
    '    '            addprebal.Rows(0)("futbal") += prExp
    '    '        Else
    '    '            qty = -qty
    '    '            prExp = Val((qty * rate * futs) / futsp)
    '    '            addprebal.Rows(0)("futbal") += prExp
    '    '        End If
    '    '    Else 'option type
    '    '        If Val(spl) <> 0 Then
    '    '            If qty > 0 Then
    '    '                prExp = Val((qty * rate * spl) / splp)
    '    '                addprebal.Rows(0)("optbal") += prExp
    '    '            Else
    '    '                qty = -qty
    '    '                prExp = Val((qty * rate * sps) / spsp)
    '    '                addprebal.Rows(0)("optbal") += prExp
    '    '            End If
    '    '        Else
    '    '            If qty > 0 Then
    '    '                prExp = Val((qty * rate * prel) / prelp)
    '    '                addprebal.Rows(0)("optbal") += prExp
    '    '            Else
    '    '                qty = -qty
    '    '                prExp = Val((qty * rate * pres) / presp)
    '    '                addprebal.Rows(0)("optbal") += prExp
    '    '            End If
    '    '        End If
    '    '    End If
    '    '    addprebal.Rows(0)("tot") = Math.Abs(Val(addprebal.Rows(0)("stbal")) + Val(addprebal.Rows(0)("futbal")) + Val(addprebal.Rows(0)("optbal")))
    '    '    objTrad.Delete_prBal(date1.Date, compname)
    '    '    objTrad.insert_prebal(addprebal)
    '    '    prExp = Val(addprebal.Rows(0)("tot"))
    '    'Else 'calculate today expense
    '    '    Dim stexp, stexp1, dst, ndst As Double
    '    '    'FOR Equity
    '    '    stexp = Math.Round(Val(dtEQTrades.Compute("sum(tot)", "qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2)
    '    '    stexp1 = Math.Abs(Math.Round(Val(dtEQTrades.Compute("sum(qty)", "qty < 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2))
    '    '    dst = stexp - stexp1
    '    '    If dst > 0 Then
    '    '        ndst = stexp1
    '    '        toExp += ((dst * ndbl) / ndblp)
    '    '        toExp += ((stexp1 * ndbs) / ndbsp)
    '    '        toExp += ((stexp1 * ndbl) / ndblp)
    '    '    Else
    '    '        ndst = stexp
    '    '        dst = -dst
    '    '        toExp += ((dst * dbs) / dbsp)
    '    '        toExp += ((stexp * ndbl) / ndblp)
    '    '        toExp += ((stexp * ndbs) / ndbsp)
    '    '    End If
    '    '    'for FUTURe
    '    '    stexp = 0
    '    '    stexp1 = 0
    '    '    stexp = Val(dtFOTrades.Compute("sum(tot)", "cp not in ('C','P') and company = '" & compname & "' and qty > 0 and entry_date =  #" & date1 & "#").ToString)
    '    '    stexp1 = Math.Abs(Val(dtFOTrades.Compute("sum(tot)", "cp not in ('C','P') and company = '" & compname & "' and qty < 0 and entry_date =  #" & date1 & "#").ToString))
    '    '    toExp += ((stexp * futl) / futlp)
    '    '    toExp += ((stexp1 * futs) / futsp)

    '    '    'OPTION
    '    '    If Val(spl) <> 0 Then
    '    '        stexp = 0
    '    '        stexp1 = 0
    '    '        stexp = Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString)
    '    '        stexp1 = Math.Abs(Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and company = '" & compname & "' qty < 0 and entry_date =  #" & date1 & "#").ToString))
    '    '        toExp += ((stexp * spl) / splp)
    '    '        toExp += ((stexp1 * sps) / spsp)
    '    '    Else
    '    '        stexp = 0
    '    '        stexp1 = 0
    '    '        stexp = Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString)
    '    '        stexp1 = Math.Abs(Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and company = '" & compname & "' and qty < 0 and entry_date =  #" & date1 & "#").ToString))

    '    '        toExp += ((stexp * prel) / prelp)
    '    '        toExp += ((stexp1 * pres) / presp)
    '    '    End If
    '    'End If
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
            stexp = Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "qty > 0 and company = '" & compname & "' AND (entrydate >=  #" & Format(date1, "dd-MMM-yyyy") & "# AND entrydate <  #" & Format(date1.AddDays(1), "dd-MMM-yyyy") & "#)").ToString), 2)
            stexp1 = Math.Abs(Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "qty < 0 and company = '" & compname & "' and (entrydate >=  #" & Format(date1, "dd-MMM-yyyy") & "# AND entrydate <  #" & Format(date1.AddDays(1), "dd-MMM-yyyy") & "#)").ToString), 2))
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
        'prebal.AcceptChanges()

        'for today expense
        If date1 = Today.Date Then
            toExp = exp
        Else
            prExp = exp
        End If

        If flgNew = True Then 'add new row
            'prebal.Rows.Add(prow(0))
            objTrad.insert_prebal(prow(0))
        Else 'update expense
            objTrad.Delete_prBal(Format(date1.Date, "dd-MMM-yyyy"), compname)
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
        getrateEQ()
        'txteqrate.Text = 0
    End Sub

    Private Sub cmbeqopt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbeqopt.Leave
        Try
            If cmbeqcomp.Text <> "" And cmbeqopt.Text <> "" Then
                cmbeqopt.Text = UCase(cmbeqopt.Text.Trim)
                If eqmasterdata.Compute("count(symbol)", "SERIES='" & cmbeqopt.Text.Trim & "' AND SYMBOL='" & cmbeqcomp.Text.Trim & "'") <= 0 Then
                    MsgBox("Not valid Series.")
                    cmbeqopt.Text = ""
                    txteqscript.Text = ""
                    cmbeqopt.Focus()
                    Exit Sub
                End If
                txteqscript.Text = cmbeqcomp.SelectedValue.ToString & Space(2) & cmbeqopt.Text
                '& Space(1) & cmbEqExchange.Text
                If eqmasterdata.Compute("count(symbol)", "script='" & txteqscript.Text.Trim & "'") <= 0 Then
                    MsgBox("Not valid Script.")
                    eqform_clear()

                    Exit Sub
                End If
                getrateEQ()
            End If
        Catch ex As Exception
            MsgBox("Invalid script.")
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
            MsgBox("Enter Company Name.", MsgBoxStyle.Information)
            cmbeqcomp.Focus()
            eqform_validation = False
            Exit Function
        End If
        If cmbeqopt.Text.Trim = "" Then
            MsgBox("Enter Option.", MsgBoxStyle.Information)
            cmbeqopt.Focus()
            eqform_validation = False
            Exit Function
        End If

        If Val(txtequnit.Text) = "0" Then
            MsgBox("Enter Units.", MsgBoxStyle.Information)
            txtequnit.Text = "0"
            txtequnit.Select()
            eqform_validation = False
            Exit Function
        End If
        If (txteqrate.Text) = "0" Then
            MsgBox("Enter Rate.", MsgBoxStyle.Information)
            txteqrate.Text = "0"
            txteqrate.Select()
            eqform_validation = False
            Exit Function
        End If
        eqform_validation = True
    End Function
    '50051 Code 
    '-------------------------------
    Private Sub cmdeqsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqsave.Click

        Dim exch As String = cmbEqExchange.Text

        Try

            If IsUserDefTag = True Then
                If cmbeqcomp.Text.ToUpper <> GetSymbol(sUdName).ToUpper Then
                    MsgBox("Invalid Security Selected." & vbCrLf & "Try again With : " & GetSymbol(sUdName).ToUpper)
                    Exit Sub
                End If
            End If


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
                If IsUserDefTag = True Then
                    objScript.Company = sUdName
                Else
                    objScript.Company = cmbeqcomp.Text
                End If

                objScript.Script = UCase(txteqscript.Text.Trim)
                objScript.CP = cmbeqopt.Text
                objScript.Units = Val(txtequnit.Text)
                objScript.Rate = Val(txteqrate.Text)
                objScript.EntryDate = CDate(dteqent.Value.Date.ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt")) 'dteqent.Value.Date
                objScript.orderno = GVarMAXEQTradingOrderNo
                objScript.Dealer = "OP"
                objScript.Exchange = exch
                objScript.insert_equity()
                'objAna.fill_equity_process(UCase(txteqscript.Text.Trim))
                'If dteqent.Value.Date < Now.Date Then
                ' cal_prebal(dteqent.Value.Date, cmbeqcomp.Text.Trim)
                'End If
                'get uid from equity_trading
                dtUid = objScript.select_equity_uid

                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                'Save to analysis's tptable (equity trading) table
                Dim DtTempEQ_trad As New DataTable
                DtTempEQ_trad = GdtEQTrades.Clone

                Dim tprow As DataRow
                tprow = DtTempEQ_trad.NewRow
                tprow("uid") = dtUid.Rows(0)("uid")
                If IsUserDefTag = True Then
                    tprow("company") = sUdName
                Else
                    tprow("company") = cmbeqcomp.Text
                End If

                tprow("eq") = cmbeqopt.Text
                tprow("script") = UCase(txteqscript.Text.Trim)
                tprow("qty") = Val(txtequnit.Text)
                tprow("Rate") = Val(txteqrate.Text)

                tprow("EntryDate") = CDate(dteqent.Value.Date.ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt")) 'Format(dteqent.Value.Date, "MMM/dd/yyyy")
                tprow("entry_date") = Format(dteqent.Value.Date, "MMM/dd/yyyy")
                tprow("orderno") = GVarMAXEQTradingOrderNo
                tprow("entryno") = 0
                tprow("issummary") = True
                tprow("isdisplay") = True
                tprow("tot") = Val(txtequnit.Text) * Val(txteqrate.Text)
                tprow("tot2") = Val(txtequnit.Text) * Val(txteqrate.Text)
                tprow("exchange") = exch
                DtTempEQ_trad.Rows.Add(tprow)

                Call insert_EQTradeToGlobalTable(DtTempEQ_trad)
                GdtEQTrades.Rows(GdtEQTrades.Rows.Count - 1).Item("Uid") = tprow("uid")
                Call GSub_CalculateExpense(DtTempEQ_trad, "EQ", True)

                ' If dteqent.Value.Date < Now.Date Then
                Dim prExp, toExp As Double
                'caluclate expense of inserted position
                'cal_prebal(dteqent.Value.Date, cmbeqcomp.Text.Trim, "E", CInt(txtequnit.Text), Val(txteqrate.Text), prExp, toExp)

                'insert position to database's analysis table also
                Dim dtAna As New DataTable
                'dtAna = objAna.fill_equity_process(UCase(txteqscript.Text.Trim), CInt(txtequnit.Text), Val(txteqrate.Text), prExp, toExp, dteqent.Value.Date)

                '***********************************************************************************
                'insert FO trade to analysis table
                Dim dtEntdate As Date = IIf(IsDBNull(dteqent.Value.Date), Now.Date, dteqent.Value.Date)
                If IsUserDefTag = True Then
                    dtAna = objAna.fill_equity_process(UCase(txteqscript.Text.Trim), CInt(txtequnit.Text), Val(txteqrate.Text), prExp, toExp, dtEntdate, sUdName, exch)
                    'insert FO trade to analysis table													   
                    objScript.insert_EQTrade_in_maintable(txteqscript.Text.Trim, dtAna, prExp, toExp, dtEntdate, sUdName, exch)
                Else
                    dtAna = objAna.fill_equity_process(UCase(txteqscript.Text.Trim), CInt(txtequnit.Text), Val(txteqrate.Text), prExp, toExp, dtEntdate, cmbeqcomp.Text, exch)
                    'insert FO trade to analysis table
                    objScript.insert_EQTrade_in_maintable(txteqscript.Text.Trim, dtAna, prExp, toExp, dtEntdate, cmbeqcomp.Text, exch)
                End If
                MsgBox("Script saved Successfully.", MsgBoxStyle.Information)


                If IsUserDefTag = True Then
                    LastOpenPosition = sUdName
                Else
                    LastOpenPosition = cmbeqcomp.Text
                End If


                txtequnit.Text = "0"
                '  txteqrate.Text = "0"
                openposyes = True
                'eqform_clear()

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    '-------------------------------
    'Old Vesrion Code 
    ' ''Private Sub cmdeqsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqsave.Click
    ' ''    Try
    ' ''        If txtequnit.Text = "" Then
    ' ''            txtunit.Text = 0
    ' ''        End If
    ' ''        If txteqrate.Text = "" Then
    ' ''            txtrate.Text = 0
    ' ''        End If
    ' ''        If eqform_validation() Then
    ' ''            'Dim tkk As Long = CLng(IIf(IsDBNull(objTrad.select_equity.Compute("max(uid)", "script='" & txteqscript.Text & "'")), 0, objTrad.select_equity.Compute("max(uid)", "script='" & txteqscript.Text & "'")))
    ' ''            'If tkk > 0 Then
    ' ''            '    MsgBox(txteqscript.Text & " script already exist in Traded")
    ' ''            '    Exit Sub
    ' ''            'End If
    ' ''            GVarMAXEQTradingOrderNo = GVarMAXEQTradingOrderNo + 1
    ' ''            objScript.Company = cmbeqcomp.Text
    ' ''            objScript.Script = UCase(txteqscript.Text.Trim)
    ' ''            objScript.CP = cmbeqopt.Text
    ' ''            objScript.Units = Val(txtequnit.Text)
    ' ''            objScript.Rate = Val(txteqrate.Text)
    ' ''            objScript.EntryDate = dteqent.Value.Date
    ' ''            objScript.orderno = GVarMAXEQTradingOrderNo
    ' ''            objScript.insert_equity()
    ' ''            'objAna.fill_equity_process(UCase(txteqscript.Text.Trim))
    ' ''            'If dteqent.Value.Date < Now.Date Then
    ' ''            ' cal_prebal(dteqent.Value.Date, cmbeqcomp.Text.Trim)
    ' ''            'End If
    ' ''            'get uid from equity_trading
    ' ''            dtUid = objScript.select_equity_uid

    ' ''            '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    ' ''            'Save to analysis's tptable (equity trading) table
    ' ''            Dim tprow As DataRow
    ' ''            tprow = GdtEQTrades.NewRow
    ' ''            tprow("uid") = dtUid.Rows(0)("uid")
    ' ''            tprow("company") = cmbeqcomp.Text
    ' ''            tprow("eq") = cmbeqopt.Text
    ' ''            tprow("script") = UCase(txteqscript.Text.Trim)
    ' ''            tprow("qty") = Val(txtequnit.Text)
    ' ''            tprow("Rate") = Val(txteqrate.Text)
    ' ''            Dim strdate As String
    ' ''            strdate = Format(dteqent.Value.Date, "MM/dd/yyyy")
    ' ''            tprow("EntryDate") = Format(CDate(strdate).Date, "MM/dd/yyyy")
    ' ''            tprow("entry_date") = Format(CDate(strdate).Date, "MM/dd/yyyy")
    ' ''            tprow("orderno") = GVarMAXEQTradingOrderNo
    ' ''            tprow("entryno") = 0
    ' ''            tprow("issummary") = True
    ' ''            tprow("isdisplay") = True
    ' ''            tprow("tot") = Val(txtequnit.Text) * Val(txteqrate.Text)
    ' ''            GdtEQTrades.Rows.Add(tprow)
    ' ''            GdtEQTrades.AcceptChanges()
    ' ''            ' If dteqent.Value.Date < Now.Date Then
    ' ''            Dim prExp, toExp As Double
    ' ''            'caluclate expense of inserted position
    ' ''            cal_prebal(dteqent.Value.Date, cmbeqcomp.Text.Trim, "E", CInt(txtequnit.Text), Val(txteqrate.Text), prExp, toExp)

    ' ''            'insert position to database's analysis table also
    ' ''            Dim dtAna As New DataTable
    ' ''            dtAna = objAna.fill_equity_process(UCase(txteqscript.Text.Trim), CInt(txtequnit.Text), Val(txteqrate.Text), prExp, toExp, dteqent.Value.Date)

    ' ''            '***********************************************************************************
    ' ''            'insert FO trade to analysis table
    ' ''            objScript.insert_EQTrade_in_maintable(txteqscript.Text.Trim, dtAna, prExp, toExp, dteqent.Value.Date)
    ' ''            'End If
    ' ''            MsgBox("Script saved Successfully", MsgBoxStyle.Information)

    ' ''            LastOpenPosition = cmbeqcomp.Text

    ' ''            txtequnit.Text = "0"
    ' ''            txteqrate.Text = "0"
    ' ''            openposyes = True
    ' ''            'eqform_clear()
    ' ''        End If
    ' ''    Catch ex As Exception
    ' ''        MsgBox(ex.ToString)
    ' ''    End Try
    ' ''End Sub

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
                MsgBox("Select valid Security.")
                cmbCurrencyComp.Text = ""
                cmbCurrencyComp.Focus()
                Exit Sub
            End If
            cmbCurrencyInstrument.DataSource = dv.ToTable(True, "InstrumentName")
            cmbCurrencyInstrument.DisplayMember = "InstrumentName"
            cmbCurrencyInstrument.ValueMember = "InstrumentName"
            'CmbInstru.Refresh()
            'cmbCurrencyInstrument.SelectedIndex = 0
            getrateCurr()
        End If
        If cmbCurrencyComp.Text.Trim = "" Then
            cmbCurrencyInstrument.DataSource = Nothing
            cmbCurrencyCP.DataSource = Nothing
            cmbCurrencyStrike.DataSource = Nothing
            cmbCurrencyExpdate.DataSource = Nothing
        End If

        'txtCurrencyrate.Text = 0

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
            If (UCase(cmbCurrencyInstrument.Text) = "FUTCUR" Or UCase(cmbCurrencyInstrument.Text) = "FUTIRD" Or UCase(cmbCurrencyInstrument.Text) = "INDEX" Or UCase(cmbCurrencyInstrument.Text) = "UNDCUR" Or UCase(cmbCurrencyInstrument.Text) = "UNDIRD" Or UCase(cmbCurrencyInstrument.Text) = "OPTCUR" Or UCase(cmbCurrencyInstrument.Text) = "FUTIRC" Or UCase(cmbCurrencyInstrument.Text) = "FUTIRT") Then
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
                            dv = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' AND InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "' AND expdate1 >='" & Now.Date & "' ", "mdate", DataViewRowState.CurrentRows) 'expdate1
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
                getrateCurr()
            Else
                MsgBox("Select valid Instrument Name.")
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
                'If cmbCurrencyStrike.Items.Count > 0 Then
                If cmbstrike.SelectedText <> "" Then
                    cmbCurrencyStrike.SelectedIndex = 0
                End If
                getrateCurr()
            Else
                MsgBox("Select valid Option type.")
                cmbCurrencyCP.Text = ""
                cmbCurrencyCP.Focus()
            End If
        End If

        'txtCurrencyrate.Text = 0
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
        If DTCurrencyContract Is Nothing = True Or DTCurrencyContract.Rows.Count = 0 Then
            DTCurrencyContract = objTrad.SELECTED_Currency_Contract
        End If
        If (Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P") And cmbCurrencyStrike.Text <> "0" Then
            Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "' and strike_price=" & Val(cmbCurrencyStrike.Text) & " AND expdate1 >='" & Now.Date & "'", "Mdate", DataViewRowState.CurrentRows) 'strike_price
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid Strike rate.")
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
            Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "' AND expdate1 >='" & Now.Date & "' ", "Mdate", DataViewRowState.CurrentRows)
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
        'txtCurrencyrate.Text = 0
        getrateCurr()
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
            MsgBox("Enter Security Name.", MsgBoxStyle.Information)
            cmbCurrencyComp.Focus()
            Exit Sub
        End If
        If cmbCurrencyInstrument.Text.Trim = "" Then
            MsgBox("Select Instrument Name.", MsgBoxStyle.Information)
            cmbCurrencyInstrument.Focus()
            Exit Sub
        End If

        If cmbCurrencyCP.Text.Trim = "" Then
            MsgBox("Select Call/Put/Futre", MsgBoxStyle.Information)
            cmbCurrencyCP.Focus()
            Exit Sub
        End If
        If cmbCurrencyStrike.Text.Trim = "0" And (Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate.", MsgBoxStyle.Information)
            cmbCurrencyStrike.Focus()
            Exit Sub
        End If
        If cmbCurrencyExpdate.Text = "" Then
            MsgBox("Select date.", MsgBoxStyle.Information)
            cmbCurrencyExpdate.Focus()
            Exit Sub
        End If
        Dim cp As String
        cp = ""
        If Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P" Then
            cp = UCase(cmbCurrencyCP.Text)
        Else
            cp = ""
        End If

        Dim script As String
        If cp = "" Then
            script = cmbCurrencyInstrument.Text & "  " & cmbCurrencyComp.Text & "  " & Format(CDate(cmbCurrencyExpdate.Text), "ddMMMyyyy")
        Else
            script = cmbCurrencyInstrument.Text & "  " & cmbCurrencyComp.Text & "  " & Format(CDate(cmbCurrencyExpdate.Text), "ddMMMyyyy") & "  " & Format(Val(cmbCurrencyStrike.Text), "###0.0000") & "  " & cp

        End If
        txtCurrencyscript.Text = script.Trim
        If DTCurrencyContract.Compute("count(symbol)", "script='" & txtCurrencyscript.Text.Trim & "'") <= 0 Then
            MsgBox("Not valid Script.")
            form_Currency_clear()
            Exit Sub
        End If

        getrateCurr()

    End Sub
    Private Sub form_Currency_clear()
        cmbCurrencyInstrument.Text = ""
        cmbCurrencyComp.Text = ""
        cmbCurrencyStrike.Text = "0"
        txtCurrencyscript.Text = ""
        txtCurrencyunit.Text = "0"
        txtCurrencyrate.Text = "0"
        'cmbCurrencyComp.SelectedIndex = 0
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
            MsgBox("Select Instrument Name.", MsgBoxStyle.Information)
            cmbCurrencyInstrument.Focus()
            Return False
        End If
        If cmbCurrencyComp.Text.Trim = "" Then
            MsgBox("Enter Company Name.", MsgBoxStyle.Information)
            cmbCurrencyComp.Focus()
            Return False
        End If
        If cmbCurrencyStrike.Text.Trim = "0" And (Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate.", MsgBoxStyle.Information)
            cmbCurrencyStrike.Focus()
            Return False
        End If
        If cmbCurrencyCP.Text = "" Then
            MsgBox("Select Call/Put/Futre.", MsgBoxStyle.Information)
            cmbCurrencyCP.Focus()
            Return False
        End If
        If Not IsNumeric(txtCurrencyunit.Text) Then
            MsgBox("Enter Units.", MsgBoxStyle.Information)
            txtCurrencyunit.Focus()
            Return False
        End If
        If Not IsNumeric(txtCurrencyrate.Text) Then
            MsgBox("Enter Rate.", MsgBoxStyle.Information)
            txtCurrencyrate.Focus()
            Return False
        End If
        If Val(txtCurrencyunit.Text) = "0" Then
            MsgBox("Enter Units.", MsgBoxStyle.Information)
            txtCurrencyunit.Text = "0"
            txtCurrencyunit.Select()
            Return False
        End If
        If Val(txtCurrencyrate.Text) = "0" Then
            MsgBox("Enter Rate.", MsgBoxStyle.Information)
            txtCurrencyrate.Text = "0"
            txtCurrencyrate.Select()
            Return False
        End If

        Return True
    End Function
    '50051 Code 
    '------------------------------
    Private Sub btnCurrencySave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCurrencySave.Click

        Dim exchange As String = cmbFoExchange.Text

        Try
            If IsUserDefTag = True Then
                If cmbCurrencyComp.Text.ToUpper <> GetSymbol(sUdName).ToUpper Then
                    MsgBox("Invalid Security Selected." & vbCrLf & "Try again With : " & GetSymbol(sUdName).ToUpper)
                    Exit Sub
                End If
            End If

            If txtCurrencyunit.Text = "" Then
                txtCurrencyunit.Text = 0
            End If
            If txtCurrencyrate.Text = "" Then
                txtCurrencyrate.Text = 0
            End If
            txtCurrencyscript.Text = txtCurrencyscript.Text.ToUpper
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
                If IsUserDefTag = True Then
                    objScript.Company = sUdName
                Else
                    objScript.Company = cmbCurrencyComp.Text
                End If

                objScript.Mdate = CDate(cmbCurrencyExpdate.Text).Date
                objScript.StrikeRate = Val(cmbCurrencyStrike.Text)
                'objScript.CP = UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1))
                REM Change By Alpesh For Set CP='F' In Currency Trade Table 
                objScript.CP = IIf(UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "X", "F", UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)))
                objScript.Script = txtCurrencyscript.Text.Trim
                objScript.Units = Val(txtCurrencyunit.Text) * Varmultiplier
                objScript.Rate = Val(txtCurrencyrate.Text)
                objScript.EntryDate = CDate(DTPCurrencyEntryDate.Value.Date.ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt")) 'DTPCurrencyEntryDate.Value.Date
                If UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "C" Or UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "P" Then
                    a = Mid(txtCurrencyscript.Text, Len(txtCurrencyscript.Text) - 1, 1)
                    a1 = Mid(txtCurrencyscript.Text, Len(txtCurrencyscript.Text), 1)
                    If a = "C" Then
                        script1 = Mid(txtCurrencyscript.Text, 1, Len(txtCurrencyscript.Text) - 2) & "P" & a1
                    Else
                        script1 = Mid(txtCurrencyscript.Text, 1, Len(txtCurrencyscript.Text) - 2) & "C" & a1
                    End If

                    script1 = script1.ToUpper
                    tk = CLng(DTCurrencyContract.Compute("max(token)", "script='" & script1 & "'").ToString)
                    objScript.Token = tk

                    For Each row As DataRow In GdtCurrencyTrades.Select("script='" & txtCurrencyscript.Text & "' And company='" & IIf(IsUserDefTag, sUdName, cmbCurrencyComp.Text) & "'")
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
                objScript.Dealer = "OP"

                'save trade to database trade table
                objScript.Insert_Currency_Trading()

                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                'get uid from trading table
                dtUid = objScript.select_Currency_trading_uid

                'Save to analysis's dtFOTrading table

                Dim DtTempCurr_trad As New DataTable
                DtTempCurr_trad = GdtCurrencyTrades.Clone

                Dim tprow As DataRow
                tprow = DtTempCurr_trad.NewRow
                tprow("uid") = dtUid.Rows(0)("uid")
                tprow("token") = 0
                tprow("mo") = Format(CDate(cmbCurrencyExpdate.Text).Date, "MM/yyyy")
                tprow("instrumentname") = cmbCurrencyInstrument.Text
                If IsUserDefTag = True Then
                    tprow("company") = sUdName
                Else
                    tprow("company") = cmbCurrencyComp.Text
                End If

                tprow("mdate") = CDate(cmbCurrencyExpdate.Text).Date
                tprow("Strikerate") = Val(cmbCurrencyStrike.Text)
                tprow("CP") = IIf(UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "X", "F", UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)))
                tprow("script") = txtCurrencyscript.Text.Trim
                tprow("qty") = Val(txtCurrencyunit.Text) * Varmultiplier
                tprow("Rate") = Val(txtCurrencyrate.Text)
                tprow("EntryDate") = CDate(DTPCurrencyEntryDate.Value.Date.ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt")) 'Format(DTPCurrencyEntryDate.Value.Date, "MMM/dd/yyyy")
                tprow("entry_date") = Format(DTPCurrencyEntryDate.Value.Date, "MMM/dd/yyyy")
                tprow("isliq") = objScript.Isliq
                tprow("token1") = tk1
                tprow("orderno") = GVarMAXCURRTradingOrderNo
                tprow("entryno") = 0
                tprow("tot") = Val(txtCurrencyunit.Text) * Val(txtCurrencyrate.Text) * Varmultiplier
                tprow("tot2") = Val(txtCurrencyunit.Text) * ((Val(txtCurrencyrate.Text) * Varmultiplier) + Val(cmbCurrencyStrike.Text))
                tprow("issummary") = True
                tprow("isdisplay") = True
                DtTempCurr_trad.Rows.Add(tprow)

                Call insert_CurrencyTradeToGlobalTable(DtTempCurr_trad)
                GdtCurrencyTrades.Rows(GdtCurrencyTrades.Rows.Count - 1).Item("Uid") = tprow("uid")
                Call GSub_CalculateExpense(DtTempCurr_trad, "CURR", True)


                'calculate expense of inserted position
                Dim prExp, toExp As Double
                Dim optype As String = "C" & UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1))
                'Call cal_prebal(DTPCurrencyEntryDate.Value.Date, cmbCurrencyComp.Text.Trim, optype, CInt(txtCurrencyunit.Text) * Varmultiplier, Val(txtCurrencyrate.Text), prExp, toExp)
                'insert position to database's analysis table also
                Dim dtAna As New DataTable
                'dtAna = objAna.fill_table_process(txtCurrencyscript.Text.Trim, CInt(txtCurrencyunit.Text) * Varmultiplier, Val(txtCurrencyrate.Text), prExp, toExp, DTPCurrencyEntryDate.Value.Date)

                'insert Currency trade to analysis table
                Dim dtEntdate As Date = IIf(IsDBNull(DTPCurrencyEntryDate.Value.Date), Now.Date, DTPCurrencyEntryDate.Value.Date)
                If IsUserDefTag = True Then
                    dtAna = objAna.fill_table_process(txtCurrencyscript.Text.Trim, CInt(txtCurrencyunit.Text) * Varmultiplier, Val(txtCurrencyrate.Text), prExp, toExp, dtEntdate, sUdName, exchange)
                    'insert Currency trade to analysis table
                    objScript.insert_CurrencyTrade_in_maintable(txtCurrencyscript.Text.Trim, dtAna, prExp, toExp, dtEntdate, sUdName)
                Else
                    dtAna = objAna.fill_table_process(txtCurrencyscript.Text.Trim, CInt(txtCurrencyunit.Text) * Varmultiplier, Val(txtCurrencyrate.Text), prExp, toExp, dtEntdate, cmbCurrencyComp.Text, exchange)
                    'insert Currency trade to analysis table
                    objScript.insert_CurrencyTrade_in_maintable(txtCurrencyscript.Text.Trim, dtAna, prExp, toExp, dtEntdate, cmbCurrencyComp.Text)
                End If
                MsgBox("Script saved Successfully.", MsgBoxStyle.Information)
                If IsUserDefTag = True Then
                    LastOpenPosition = sUdName
                Else
                    LastOpenPosition = cmbCurrencyComp.Text
                End If

            End If

            '***********************************************************************************                objAna.fill_table_process(txtscript.Text.Trim)
            'txtCurrencyrate.Text = "0"
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
                    Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & CmbComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "'  ", "expdate", DataViewRowState.CurrentRows)
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
        Dim exchFo As String = cmbFoExchange.Text
        Dim exchEq As String = cmbEqExchange.Text

        If IsUserDefTag = True Then
            If TabControl1.SelectedIndex = 0 Then
                If masterdata.Rows.Count = 0 Then
                    masterdata = New DataTable
                    masterdata = cpfmaster.Copy
                    Dim filter As String = "exchange='" & exchFo & "'"
                    'Dim dv As DataView = New DataView(masterdata, "", "symbol", DataViewRowState.CurrentRows)
                    Dim dv As DataView = New DataView(masterdata, filter, "symbol", DataViewRowState.CurrentRows)
                    '                    Dim dv As DataView = New DataView(masterdata, "symbol = '" & sUdSymbol & "'", "symbol", DataViewRowState.CurrentRows)
                    CmbComp.DataSource = dv.ToTable(True, "symbol")
                    CmbComp.DisplayMember = "symbol"
                    CmbComp.ValueMember = "symbol"

                    If dv.ToTable(True, "symbol").Compute("count(symbol)", "symbol = '" & sUdSymbol & "'") > 0 Then
                        CmbComp.SelectedValue = sUdSymbol
                        CmbComp.Select()
                    End If
                End If

            ElseIf TabControl1.SelectedIndex = 1 Then
                If eqmasterdata.Rows.Count = 0 Then
                    eqmasterdata = New DataTable
                    eqmasterdata = eqmaster.Copy
                    Application.DoEvents()
                    cmbeqcomp.DisplayMember = "symbol"
                    cmbeqcomp.ValueMember = "symbol"
                    'Dim dv1 As DataView = New DataView(eqmasterdata, "symbol = '" & sUdSymbol & "'", "symbol", DataViewRowState.CurrentRows)
                    Dim dv1 As DataView = New DataView(eqmasterdata, "symbol = '" & sUdSymbol & "'", "symbol", DataViewRowState.CurrentRows)
                    cmbeqcomp.DataSource = dv1.ToTable(True, "symbol")
                    'Application.DoEvents()
                    'cmbeqcomp.DisplayMember = "symbol"
                    'cmbeqcomp.ValueMember = "symbol"
                    'If eqmasterdata.Rows.Count > 0 Then
                    '    'cmbeqcomp.SelectedValue = dv1.ToTable(True, "Symbol").Rows(1).Item(0).ToString()
                    '    'cmbeqcomp.Select()
                    '    cmbeqcomp.SelectedIndex = 1 'dv1.ToTable(True, "Symbol").Rows(1).Item(0).ToString()
                    cmbeqcomp.SelectAll()
                    'End If
                    '   cmbeqcomp.SelectedValue = 

                End If
                cmbeqcomp.Select()
            ElseIf TabControl1.SelectedIndex = 2 Then
                If DTCurrencyContract.Rows.Count = 0 Then
                    DTCurrencyContract = New DataTable
                    DTCurrencyContract = Currencymaster.Copy
                    cmbCurrencyComp.DisplayMember = "symbol"
                    cmbCurrencyComp.ValueMember = "symbol"
                    Dim dvCurr As DataView = New DataView(DTCurrencyContract, "symbol = '" & sUdSymbol & "' And instrumentName='FUTCUR' or instrumentName='FUTIRD' or instrumentName='INDEX' or instrumentName='UNDCUR' or instrumentName='UNDIRD' or instrumentName='OPTCUR'  or instrumentName='FUTIRC'  or instrumentName='FUTIRT'", "symbol", DataViewRowState.CurrentRows)
                    cmbCurrencyComp.DataSource = dvCurr.ToTable(True, "symbol")
                    'cmbCurrencyComp.DisplayMember = "symbol"
                    'cmbCurrencyComp.ValueMember = "symbol"
                    ' cmbeqopt.SelectedValue = "Symbol"
                    cmbCurrencyComp.SelectAll()
                End If
                cmbCurrencyComp.Select()
                'cmbCurrencyComp.DataSource = Nothing
            End If
        Else
            If TabControl1.SelectedIndex = 0 Then
                'If masterdata.Rows.Count = 0 Then
                'masterdata = New DataTable
                'masterdata = cpfmaster
                'Dim filter As String = "exchange='" & exchFo & "'"
                ''Dim dv As DataView = New DataView(masterdata, "", "symbol", DataViewRowState.CurrentRows)
                'Dim dv As DataView = New DataView(masterdata, filter, "symbol", DataViewRowState.CurrentRows)
                'CmbComp.DataSource = dv.ToTable(True, "symbol")
                CmbComp.DataSource = mContracts.GetFoSymbolList(cmbFoExchange.Text)
                CmbComp.DisplayMember = "symbol"
                CmbComp.ValueMember = "symbol"

                If cmbFoExchange.Text = "NSE" Then
                    CmbComp.SelectedValue = "NIFTY"
                    CmbComp.Select()
                End If

            ElseIf TabControl1.SelectedIndex = 1 Then
                Me.Cursor = Cursors.WaitCursor
                'If eqmasterdata.Rows.Count = 0 Then
                'eqmasterdata = New DataTable
                'eqmasterdata = eqmaster
                'Application.DoEvents()
                'cmbeqcomp.DisplayMember = "symbol"
                'cmbeqcomp.ValueMember = "symbol"

                'Dim filterStr As String = "exchange='" & exchEq & "'"

                ''Dim dv1 As DataView = New DataView(eqmasterdata, "", "symbol", DataViewRowState.CurrentRows)
                'Dim dv1 As DataView = New DataView(eqmasterdata, filterStr, "symbol", DataViewRowState.CurrentRows)

                'cmbeqcomp.DataSource = dv1.ToTable(True, "symbol")
                cmbeqcomp.DisplayMember = "symbol"
                cmbeqcomp.ValueMember = "symbol"
                cmbeqcomp.DataSource = mContracts.GetEqSymbolList(cmbEqExchange.Text)

                ' Application.DoEvents()
                'cmbeqcomp.DisplayMember = "symbol"
                'cmbeqcomp.ValueMember = "symbol"
                'If eqmasterdata.Rows.Count > 0 Then
                '    'cmbeqcomp.SelectedValue = dv1.ToTable(True, "Symbol").Rows(1).Item(0).ToString()
                '    'cmbeqcomp.Select()
                '    cmbeqcomp.SelectedIndex = 1 'dv1.ToTable(True, "Symbol").Rows(1).Item(0).ToString()
                'cmbeqcomp.SelectAll()

                'End If
                '   cmbeqcomp.SelectedValue = 

                'End If
                'cmbeqcomp.Select()
                Me.Cursor = Cursors.Default
            ElseIf TabControl1.SelectedIndex = 2 Then
                If DTCurrencyContract.Rows.Count = 0 Then
                    DTCurrencyContract = New DataTable
                    DTCurrencyContract = Currencymaster
                    cmbCurrencyComp.DisplayMember = "symbol"
                    cmbCurrencyComp.ValueMember = "symbol"
                    'Dim dvCurr As DataView = New DataView(DTCurrencyContract, "", "symbol", DataViewRowState.CurrentRows)
                    Dim dvCurr As DataView = New DataView(DTCurrencyContract, "instrumentName='FUTCUR' or instrumentName='FUTIRD' or instrumentName='INDEX' or instrumentName='UNDCUR' or instrumentName='UNDIRD' or instrumentName='OPTCUR' or instrumentName='FUTIRC' or instrumentName='FUTIRT'", "symbol", DataViewRowState.CurrentRows)
                    cmbCurrencyComp.DataSource = dvCurr.ToTable(True, "symbol")
                    'cmbCurrencyComp.DisplayMember = "symbol"
                    'cmbCurrencyComp.ValueMember = "symbol"
                    ' cmbeqopt.SelectedValue = "Symbol"
                    cmbCurrencyComp.SelectAll()
                End If
                cmbCurrencyComp.Select()
            End If
        End If

    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 0 Then
            CmbComp.Select()

            'If TabControl1.SelectedIndex = 0 Then
            '    CmbComp.Select()
            '    'ElseIf TabControl1.SelectedIndex = 1 Then
            '    '    cmbeqcomp.Select()
            '    'ElseIf TabControl1.SelectedIndex = 2 Then
            '    '    cmbCurrencyComp.Select()
        End If
        TabControl1_Click(sender, e)
    End Sub

    Private Sub txtunit_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtunit.Enter
        txtunit.Select()
    End Sub

    Private Sub txtrate_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrate.Enter
        txtrate.Select()

    End Sub

    Private Sub cmbdate_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdate.SelectedValueChanged
        'If cmbdate.Text <> "" Then
        '    Dim cp As String
        '    cp = ""
        '    If Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P" Then
        '        cp = UCase(cmbcp.SelectedValue)
        '    Else
        '        cp = ""
        '    End If
        '    Dim script As String
        '    If cp = "" Then
        '        script = CmbInstru.Text & "  " & CmbComp.Text & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy")
        '    Else
        '        script = CmbInstru.Text & "  " & CmbComp.Text & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy") & "  " & Format(Val(cmbstrike.Text), "###0.00") & "  " & cp
        '    End If
        '    txtscript.Text = script.Trim
        '    ' txtunit.Select()
        'End If

    End Sub

    Private Sub cmbCurrencyStrike_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyStrike.SelectedIndexChanged
        If cmbCurrencyExpdate.Text.Trim <> "" And cmbCurrencyStrike.Text <> "" And cmbCurrencyStrike.Text <> "System.Data.DataRowView" Then


            If cmbCurrencyComp.Text.Trim <> "" And cmbCurrencyInstrument.Text.Trim <> "" And cmbCurrencyCP.Text.Trim <> "" And (cmbCurrencyStrike.Text.Trim <> "0" And (Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P")) And cmbCurrencyExpdate.Text <> "" Then

                Dim cp As String
                cp = ""
                If Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P" Then
                    cp = UCase(cmbCurrencyCP.Text)
                Else
                    cp = ""
                End If

                Dim script As String
                If cp = "" Or cmbCurrencyInstrument.Text.Substring(0, 3) = "FUT" Then
                    script = cmbCurrencyInstrument.Text & "  " & cmbCurrencyComp.Text & "  " & Format(CDate(cmbCurrencyExpdate.Text), "ddMMMyyyy")
                Else
                    script = cmbCurrencyInstrument.Text & "  " & cmbCurrencyComp.Text & "  " & Format(CDate(cmbCurrencyExpdate.Text), "ddMMMyyyy") & "  " & Format(Val(cmbCurrencyStrike.Text), "###0.0000") & "  " & cp

                End If
                txtCurrencyscript.Text = script.Trim
                If DTCurrencyContract.Compute("count(symbol)", "script='" & txtCurrencyscript.Text.Trim & "'") <= 0 Then
                    txtCurrencyscript.Text = ""
                End If

            End If



        End If
        '   txtCurrencyscript.Text = ""

        'getrateCurr()
    End Sub

    Private Sub cmbstrike_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstrike.SelectedIndexChanged
        If cmbdate.Text.Trim <> "" And cmbstrike.Text <> "System.Data.DataRowView" Then


            If CmbComp.Text.Trim <> "" And CmbInstru.Text.Trim <> "" And cmbcp.Text.Trim <> "" And (cmbstrike.Text.Trim <> "0" And (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P")) And cmbdate.Text <> "" Then
                Dim cp As String
                cp = ""
                If Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P" Then
                    cp = UCase(cmbcp.Text)
                Else
                    cp = ""
                End If
                Dim script As String
                If cp = "" Or CmbInstru.Text = "FUTIDX" Then
                    script = CmbInstru.Text & "  " & CmbComp.Text & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy")
                Else
                    script = CmbInstru.Text & "  " & CmbComp.Text & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy") & "  " & Format(Val(cmbstrike.Text), "###0.00") & "  " & cp
                End If
                txtscript.Text = script.Trim

            End If





        End If
    End Sub


    Private Sub CmbComp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbComp.SelectedIndexChanged

    End Sub

    Private Sub txtscript_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtscript.Leave

    End Sub
    Private Sub getrateCurr()
        'Dim token As Double
        'Dim dr As DataRow() = DTCurrencyContract.Select("Script='" & txtCurrencyscript.Text & "'")
        'If Not dr.Length = 0 Then
        '    token = dr(0)("token")
        'End If
        'If token = 0 Then
        '    Exit Sub
        'End If
        'If NetMode = "UDP" Then


        '    If Currltpprice.Contains(Convert.ToInt64(token)) Then

        '        txtCurrencyrate.Text = Math.Round(Val(Currltpprice(Convert.ToInt64(token))), 2) / 10000000

        '    End If
        '    If Currfltpprice.Contains(Convert.ToInt64(token)) Then
        '        txtCurrencyrate.Text = Math.Round(Val(Currfltpprice(Convert.ToInt64(token))), 2) / 10000000
        '    End If
        'Else

        '    If Currltpprice.Contains(Convert.ToInt64(token)) Then

        '        txtCurrencyrate.Text = Convert.ToDouble(Math.Round(Val(Currltpprice(Convert.ToInt64(token))), 2) / 10000000).ToString("#0.0000")
        '    Else
        '        Dim dt As String = getrateCurr(Convert.ToInt64(token))
        '        If dt Is Nothing = False Then
        '            If dt <> 0 Then
        '                txtCurrencyrate.Text = Convert.ToDouble(Math.Round(ED.DFo(Val(dt.ToString())), 2) / 10000000).ToString("#0.0000")
        '            End If
        '        End If
        '    End If
        '    'If Currfltpprice.Contains(Convert.ToInt64(token)) Then
        '    '    txtCurrencyrate.Text = Convert.ToDouble(Math.Round(Val(Currfltpprice(Convert.ToInt64(token))), 2) / 10000000).ToString("#0.0000")
        '    'Else
        '    '    Dim dt As DataTable = getrateCurr(Convert.ToInt64(token))
        '    '    If dt Is Nothing = False Then
        '    '        If dt.Rows.Count > 0 Then
        '    '            txtCurrencyrate.Text = Convert.ToDouble(Math.Round(ED.DFo(Val(dt.Rows(0)("Rate").ToString())), 2) / 10000000).ToString("#0.0000")
        '    '        End If
        '    '    End If
        '    'End If
        'End If
    End Sub
    Private Sub getrateFo()
        'Dim dv As DataView = New DataView(masterdata, "Script='" & CmbComp.Text & "'", "InstrumentName", DataViewRowState.CurrentRows)
        Dim token As Double
        Dim dr As DataRow() = masterdata.Select("Script='" & txtscript.Text & "'")
        If Not dr.Length = 0 Then
            token = dr(0)("token")
        End If
        If token = 0 Then
            Exit Sub
        End If
        If NetMode = "UDP" Then


            If ltpprice.Contains(Convert.ToInt64(token)) Then

                txtrate.Text = Math.Round(Val(ltpprice(Convert.ToInt64(token))), 2)

            End If
            If fltpprice.Contains(Convert.ToInt64(token)) Then
                txtrate.Text = Math.Round(Val(fltpprice(Convert.ToInt64(token))), 2)
            End If

        ElseIf NetMode = "TCP" Or NetMode = "API" Or NetMode = "JL" Then
            If ltpprice.Contains(Convert.ToInt64(token)) Then
                txtrate.Text = Math.Round(Val(ltpprice(Convert.ToInt64(token))))
            ElseIf fltpprice.Contains(Convert.ToInt64(token)) Then

                txtrate.Text = Math.Round(Val(fltpprice(Convert.ToInt64(token))), 2)
            Else
                Dim dt As String = getrate(Convert.ToInt64(token))
                If dt Is Nothing = False Then
                    If dt.ToString() <> "0" And dt <> "" Then
                        txtrate.Text = Math.Round(ED.DFo(Val(dt.ToString())), 2)
                    Else
                        txtrate.Text = 0
                    End If
                Else
                    txtrate.Text = 0
                End If

            End If
        ElseIf NetMode = "NET" Or NetMode = "API" Or NetMode = "JL" Then


            If ltpprice.Contains(Convert.ToInt64(token)) Then

                txtrate.Text = Math.Round(Val(ltpprice(Convert.ToInt64(token))), 2)

            End If
            If fltpprice.Contains(Convert.ToInt64(token)) Then
                txtrate.Text = Math.Round(Val(fltpprice(Convert.ToInt64(token))), 2)
            End If
        End If

    End Sub
    Private Sub txtscript_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtscript.TextChanged


    End Sub
    Public Function getrateCurr(ByVal token As Int64) As String
        Try
            Dim Query As String = "select f4 As Rate from tbl03006 With(Nolock) Where f1=" + token.ToString() + ""
            'DAL.DA_SQL.Cmd_Text = Query

            Return DAL.DA_SQL.ExecuteScalar_openposition(Query)

            'Return DAL.DA_SQL.FillList()
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return Nothing
        End Try

    End Function

    Public Function getrate(ByVal token As Int64) As String


        Try
            Dim Query As String = "select f4 As Rate from tbl01004 With(Nolock) Where f1=" + token.ToString() + ""
            'DAL.DA_SQL.Cmd_Text = Query

            Return DAL.DA_SQL.ExecuteScalar_openposition(Query)

            'Return DAL.DA_SQL.FillList()
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return Nothing
        End Try

    End Function
    Public Function getrateEQ(ByVal token As Int64) As String
        Try
            Dim Query As String = "select f4 As Rate from tbl02005 With(Nolock) Where f1=" + token.ToString() + ""
            'DAL.DA_SQL.Cmd_Text = Query

            Return DAL.DA_SQL.ExecuteScalar_openposition(Query)

            'Return DAL.DA_SQL.FillList()
        Catch ex As Exception
            '  MsgBox(ex.ToString)
            Return Nothing
        End Try

    End Function
    Private Sub getrateEQ()
        Dim token As Double
        Dim dr As DataRow() = eqmasterdata.Select("Script='" & txteqscript.Text & "'")
        If Not dr.Length = 0 Then
            token = dr(0)("token")
        End If
        If token = 0 Then
            Exit Sub
        End If
        If NetMode = "UDP" Then
            If eltpprice.Contains(Convert.ToInt64(token)) Then
                txteqrate.Text = Math.Round(Val(eltpprice(Convert.ToInt64(token))), 2).ToString("0.00")

            End If
        ElseIf NetMode = "NET" Or NetMode = "API" Or NetMode = "JL" Then
            If eltpprice.Contains(Convert.ToInt64(token)) Then
                txteqrate.Text = Math.Round(Val(eltpprice(Convert.ToInt64(token))), 2).ToString("0.00")

            End If
        ElseIf NetMode = "TCP" Then
            If eltpprice.Contains(Convert.ToInt64(token)) Then

                txteqrate.Text = Math.Round(Val(eltpprice(Convert.ToInt64(token))), 2).ToString("0.00")
            Else
                Dim dt As String = getrateEQ(Convert.ToInt64(token))
                'tbl02005
                If dt Is Nothing = False Then
                    If dt.ToString() <> "0" And dt <> "" Then
                        txteqrate.Text = Math.Round(ED.DFo(Val(dt.ToString())), 2).ToString("0.00")
                    Else
                        txteqrate.Text = "0.00"
                    End If
                Else
                    txteqrate.Text = "0.00"
                End If
            End If
        End If
        'If efltpprice.Contains(Convert.ToInt64(token)) Then
        '    txteqrate.Text = Val(Currltpprice(Convert.ToInt64(token)))
        'End If
    End Sub
    Private Sub txteqscript_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txteqscript.TextChanged


    End Sub

    Private Sub txteqscript_TextAlignChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txteqscript.TextAlignChanged

    End Sub

    Private Sub txtCurrencyscript_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCurrencyscript.TextChanged




    End Sub


    Private Sub cmbCurrencyExpdate_Layout(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles cmbCurrencyExpdate.Layout

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub CmbInstru_LocationChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbInstru.LocationChanged

    End Sub

    Private Sub cmbcp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcp.SelectedIndexChanged

    End Sub

    Private Sub cmbstrike_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstrike.SelectedValueChanged
        'txtscript.Text = ""
    End Sub

    Private Sub cmbFoExchange_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFoExchange.SelectedIndexChanged
        If mIsLoading = False Then
            TabControl1_Click(sender, e)
        End If
    End Sub

    Private Sub cmbEqExchange_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEqExchange.SelectedIndexChanged
        If mIsLoading = False Then
            TabControl1_Click(sender, e)
        End If
    End Sub

End Class
