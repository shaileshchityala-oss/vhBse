Public Class frmMarketwatch
    Dim Ht_CompanyList As New Hashtable

    Dim strCompany As String
    Dim strInstrument As String
    Dim boolIsCurrency As Boolean = False

    Dim Fltp1 As Double = 0
    Dim Fltp2 As Double = 0
    Dim Fltp3 As Double = 0

    Dim iUpDownstrike As Integer

    Dim MidStrike1 As Double = 0
    Dim MidStrike2 As Double = 0
    Dim MidStrike3 As Double = 0


    Public ArrTokenSeries As New ArrayList        ' To register tokens for TCP Connection

    Dim dtcpfmaster As DataTable                  ' temp Current Contract (Selected Symbol and based on Is weekly Selected 
    Dim DtFMonthDate As DataTable

    Dim dtMarketTable1 As New DataTable
    Dim dtMarketTable2 As New DataTable
    Dim dtMarketTable3 As New DataTable

    Private Sub InitMarketWatchTable()
        Dim dtMarketTable As DataTable = New DataTable
        dtMarketTable.Columns.Add("CallToken", GetType(Long))
        dtMarketTable.Columns.Add("PutToken", GetType(Long))
        dtMarketTable.Columns.Add("Maturity", GetType(Date))
        dtMarketTable.Columns.Add("Symbol", GetType(String))

        dtMarketTable.Columns.Add("Strike1", GetType(Double))
        dtMarketTable.Columns.Add("CallOIChg", GetType(Double))
        dtMarketTable.Columns.Add("CallOI", GetType(Double))
        dtMarketTable.Columns.Add("CallBF", GetType(Double))
        dtMarketTable.Columns.Add("CallBF2", GetType(Double))
        dtMarketTable.Columns.Add("CallStraddle", GetType(Double))
        dtMarketTable.Columns.Add("CallRatio", GetType(Double))
        dtMarketTable.Columns.Add("CallVolume", GetType(Double))
        dtMarketTable.Columns.Add("CallDelta", GetType(Double))
        dtMarketTable.Columns.Add("CallGamma", GetType(Double))
        dtMarketTable.Columns.Add("CallVega", GetType(Double))
        dtMarketTable.Columns.Add("CallTheta", GetType(Double))
        dtMarketTable.Columns.Add("CallChg", GetType(Double))
        dtMarketTable.Columns.Add("CallVolChg", GetType(Double))
        dtMarketTable.Columns.Add("CallVol", GetType(Double))
        dtMarketTable.Columns.Add("CE", GetType(Double))

        dtMarketTable.Columns.Add("Strike2", GetType(Double))
        dtMarketTable.Columns.Add("PE", GetType(Double))
        dtMarketTable.Columns.Add("PutVol", GetType(Double))
        dtMarketTable.Columns.Add("PutVolChg", GetType(Double))
        dtMarketTable.Columns.Add("PutChg", GetType(Double))
        dtMarketTable.Columns.Add("PutDelta", GetType(Double))
        dtMarketTable.Columns.Add("PutGamma", GetType(Double))
        dtMarketTable.Columns.Add("PutVega", GetType(Double))
        dtMarketTable.Columns.Add("PutTheta", GetType(Double))
        dtMarketTable.Columns.Add("PutVolume", GetType(Double))
        dtMarketTable.Columns.Add("PutRatio", GetType(Double))

        dtMarketTable.Columns.Add("Strike3", GetType(Double))
        dtMarketTable.Columns.Add("PutBF", GetType(Double))
        dtMarketTable.Columns.Add("PutBF2", GetType(Double))
        dtMarketTable.Columns.Add("PutOI", GetType(Double))
        dtMarketTable.Columns.Add("PutOIChg", GetType(Double))
        dtMarketTable.Columns.Add("TotalOI", GetType(Double))
        dtMarketTable.Columns.Add("PutTotalOIPer", GetType(Double))
        dtMarketTable.Columns.Add("CallOIPer", GetType(Double))
        dtMarketTable.Columns.Add("PutOIPer", GetType(Double))
        dtMarketTable.Columns.Add("PCPB", GetType(Double))
        dtMarketTable.Columns.Add("PCP", GetType(Double))
        dtMarketTable.Columns.Add("PCPA", GetType(Double))
        dtMarketTable.Columns.Add("C2C", GetType(Double))
        dtMarketTable.Columns.Add("P2P", GetType(Double))
        dtMarketTable.Columns.Add("C2P", GetType(Double))
        dtMarketTable.Columns.Add("CallCalender", GetType(Double))
        dtMarketTable.Columns.Add("PutCalender", GetType(Double))
        dtMarketTable.Columns.Add("CallBullSpread", GetType(Double))
        dtMarketTable.Columns.Add("PutBearSpread", GetType(Double))

        dtMarketTable.Columns.Add("futltp", GetType(Double))

        dtMarketTable.Columns.Add("PutPreToken", GetType(Long))
        dtMarketTable.Columns.Add("PutPre2Token", GetType(Long))
        dtMarketTable.Columns.Add("PutNextToken", GetType(Long))
        dtMarketTable.Columns.Add("PutNext2Token", GetType(Long))

        dtMarketTable.Columns.Add("CallPreToken", GetType(Long))
        dtMarketTable.Columns.Add("CallPre2Token", GetType(Long))
        dtMarketTable.Columns.Add("CallNextToken", GetType(Long))
        dtMarketTable.Columns.Add("CallNext2Token", GetType(Long))
        dtMarketTable.Columns.Add("CallVol1", GetType(Double))
        dtMarketTable.Columns.Add("PutVol1", GetType(Double))

        dtMarketTable.Columns.Add("CEStatus", GetType(String))
        dtMarketTable.Columns.Add("PEStatus", GetType(String))

        dtMarketTable.Columns.Add("CEStoploss", GetType(Double))
        dtMarketTable.Columns.Add("PEStoploss", GetType(Double))
        dtMarketTable.Columns.Add("CR", GetType(Double))
        'dtMarketTable.Columns.Add("Calltrend", GetType(Double))
        'dtMarketTable.Columns.Add("Puttrend", GetType(Double))

        'dtMarketTable.Columns.Add("CallStoploss", GetType(Double))
        'dtMarketTable.Columns.Add("PutStoploss", GetType(Double))

        'to fill companynames into combobox (End)

        dtMarketTable1 = dtMarketTable.Clone
        dtMarketTable2 = dtMarketTable.Clone
        dtMarketTable3 = dtMarketTable.Clone
    End Sub


    Private Sub frmMarketwatch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        FillCboCompany()
        iUpDownstrike = Val(txtNoStrike.Text)
        InitMarketWatchTable()
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub FillCboCompany()
        'Dim Dtcname As New DataTable()
        Dim column As String() = {"symbol"}
        'Dtcname = cpfmaster.DefaultView.ToTable("dd", False, column)
        Dim dtcpfmaster As New DataTable
        dtcpfmaster = cpfmaster.Clone
        dtcpfmaster.AcceptChanges()
        Dim drcpf() As DataRow
        drcpf = cpfmaster.Select("option_type in ('CA','CE','PA','PE')", "symbol")
        For Each dataRow As DataRow In drcpf
            dtcpfmaster.ImportRow(dataRow)
        Next

        Dim Dtcname As New DataTable
        'Dtcname = New DataView(cpfmaster, "option_type in ('CA','CE','PA','PE')", "symbol", DataViewRowState.CurrentRows).ToTable("TRUE", column)
        Dtcname = New DataView(dtcpfmaster, "", "", DataViewRowState.CurrentRows).ToTable("TRUE", column)

        For Each item As DataRow In Dtcname.Select()
            If Not Ht_CompanyList.Contains(item("symbol").ToString()) Then
                Ht_CompanyList.Add(item("symbol").ToString(), False)
            End If
        Next

        'Dtcname = dv.ToTable(True, "symbol")
        Dim dtCurrmaster As New DataTable
        dtCurrmaster = dtCurrmaster.Clone
        Dim drCurr() As DataRow
        Dim Dtcnamecurr As New DataTable
        drCurr = Currencymaster.Select("InstrumentName = 'FUTCUR'", "symbol")
        For Each dataRow1 As DataRow In drCurr
            dtCurrmaster.ImportRow(dataRow1)
            If Not Ht_CompanyList.Contains(dataRow1("symbol").ToString()) Then
                Ht_CompanyList.Add(dataRow1("symbol").ToString(), True)
            End If
        Next
        ' dv = New DataView(Currencymaster, "InstrumentName = 'FUTCUR'", "symbol", DataViewRowState.CurrentRows)
        'Dtcnamecurr = New DataView(Currencymaster, "InstrumentName = 'FUTCUR'", "symbol", DataViewRowState.CurrentRows).ToTable("TRUE", column)
        Dtcnamecurr = New DataView(Currencymaster, "", "", DataViewRowState.CurrentRows).ToTable("TRUE", column)
        Dtcname.Merge(Dtcnamecurr)




        cmbCompany.DataSource = Dtcname
        cmbCompany.DisplayMember = "symbol"
        cmbCompany.ValueMember = "symbol"
        cmbCompany.Refresh()
        cmbCompany.Text = "NIFTY"


        


        'Dim dt As New DataTable
        'dt = cpfmaster.Clone
        'dt.AcceptChanges()
        'Dim dr() As DataRow
        'dr = cpfmaster.Select("Symbol='" & cmbCompany.Text & "' AND  InstrumentName IN ('FUTSTK','FUTIDX','FUTIVX')", "expdate1")
        'For Each dataRow As DataRow In dr
        '    dt.ImportRow(dataRow)
        'Next

        ''DtFMonthDate = New DataView(cpfmaster, "Symbol='" & cmbCompany.Text & "' AND  InstrumentName IN ('FUTSTK','FUTIDX','FUTIVX')", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
        'DtFMonthDate = New DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

        'cmbCompany.Refresh()
        'cmbCompany.Text = "NIFTY"
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        strCompany = cmbCompany.Text

        If Not Ht_CompanyList.Contains(strCompany) Then
            MsgBox("Invalit Symbol Selected")
            Return
        End If

        boolIsCurrency = Ht_CompanyList(strCompany)
        Dim isweekly As Boolean
        If chkIsWeekly.Visible And chkIsWeekly.Checked Then
            isweekly = True
        Else
            isweekly = False
        End If



        'Load First time data for imidiate diasplay
        If (NetMode = "TCP" Or NetMode = "API" Or NetMode = "JL") Then
            Dim strDelTokens As String = ""
            Dim thr_GetRate As New Threading.Thread(AddressOf getrate)
            thr_GetRate.Start()

            Fltp1 = 0
            Fltp2 = 0
            Fltp3 = 0
            Refresh_tmpCpfmasterData(isweekly)
            Refresh_Data(isweekly, True)
            If ArrTokenSeries.Count > 0 Then
                For Each str As String In ArrTokenSeries
                    strDelTokens = strDelTokens & IIf(strDelTokens.Length > 0, ",", "") & str.ToString()
                Next
                Objsql.DeleteFoTokens(strDelTokens)
                ArrTokenSeries.Clear()
                FoRegTokens.Clear()
                EqRegTokens.Clear()
                CurRegTokens.Clear()
            End If

        ElseIf NetMode = "UDP" Then
            Fltp1 = 0
            Fltp2 = 0
            Fltp3 = 0
            Refresh_tmpCpfmasterData(isweekly)
            Refresh_Data(isweekly, True)
        End If



        If boolIsCurrency Then
            dgv_Exp1.Columns("col1CE").DefaultCellStyle.Format = "N4"
            dgv_exp2.Columns("col2CE").DefaultCellStyle.Format = "N4"
            dgv_exp3.Columns("col3CE").DefaultCellStyle.Format = "N4"

            dgv_Exp1.Columns("col1PE").DefaultCellStyle.Format = "N4"
            dgv_exp2.Columns("col2PE").DefaultCellStyle.Format = "N4"
            dgv_exp3.Columns("col3PE").DefaultCellStyle.Format = "N4"


            dgv_Exp1.Columns("col1Strike1").DefaultCellStyle.Format = "N4"
            dgv_Exp1.Columns("col1Strike2").DefaultCellStyle.Format = "N4"
            dgv_Exp1.Columns("col1Strike3").DefaultCellStyle.Format = "N4"

            dgv_exp2.Columns("col2Strike1").DefaultCellStyle.Format = "N4"
            dgv_exp2.Columns("col2Strike2").DefaultCellStyle.Format = "N4"
            dgv_exp2.Columns("col2Strike3").DefaultCellStyle.Format = "N4"

            dgv_exp3.Columns("col3Strike1").DefaultCellStyle.Format = "N4"
            dgv_exp3.Columns("col3Strike2").DefaultCellStyle.Format = "N4"
            dgv_exp3.Columns("col3Strike3").DefaultCellStyle.Format = "N4"

        Else
            dgv_Exp1.Columns("col1CE").DefaultCellStyle.Format = "N2"
            dgv_exp2.Columns("col2CE").DefaultCellStyle.Format = "N2"
            dgv_exp3.Columns("col3CE").DefaultCellStyle.Format = "N2"

            dgv_Exp1.Columns("col1PE").DefaultCellStyle.Format = "N2"
            dgv_exp2.Columns("col2PE").DefaultCellStyle.Format = "N2"
            dgv_exp3.Columns("col3PE").DefaultCellStyle.Format = "N2"


            dgv_Exp1.Columns("col1Strike1").DefaultCellStyle.Format = "N2"
            dgv_Exp1.Columns("col1Strike2").DefaultCellStyle.Format = "N2"
            dgv_Exp1.Columns("col1Strike3").DefaultCellStyle.Format = "N2"

            dgv_exp2.Columns("col2Strike1").DefaultCellStyle.Format = "N2"
            dgv_exp2.Columns("col2Strike2").DefaultCellStyle.Format = "N2"
            dgv_exp2.Columns("col2Strike3").DefaultCellStyle.Format = "N2"

            dgv_exp3.Columns("col3Strike1").DefaultCellStyle.Format = "N2"
            dgv_exp3.Columns("col3Strike2").DefaultCellStyle.Format = "N2"
            dgv_exp3.Columns("col3Strike3").DefaultCellStyle.Format = "N2"
        End If



        



    End Sub

    Private Sub Refresh_tmpCpfmasterData(ByVal isWeekly As Boolean)
        'Prepare CPF Master

        If isWeekly = True Then
            dtcpfmaster = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' ", "", DataViewRowState.CurrentRows).ToTable()
        End If


        strCompany = cmbCompany.Text
        strcompmkt = strCompany
        If strCompany.Contains("INR") Then
            boolIsCurrency = True
            Dim DtFMonthDatecurr As DataTable = New DataView(Currencymaster, "option_type='XX'", "Symbol,Expdate Desc", DataViewRowState.CurrentRows).ToTable(True, "expdate", "token", "Symbol")
            Dim icurr As Integer = 1
            Dim strcurr As String = ""
            Dim HT_CurrNextFar1 As New Hashtable
            Dim HT_CurrNextFarcurr1 As New Hashtable
            HT_CurrNextFar.Clear()
            Dim i As Integer = 1
            Dim str As String = ""
            For Each drow As DataRow In DtFMonthDatecurr.Select("")
                If HT_CurrNextFarcurr1.Contains(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy")) = False Then
                    If i = 1 Then
                        str = drow("Symbol")
                    Else
                        If drow("Symbol") <> str Then
                            i = 1
                            str = drow("Symbol")
                        End If
                    End If

                    '   HT_CurrNextFarcurr.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy"), i)
                    HT_CurrNextFarcurr1.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy"), drow("Token"))
                    i = i + 1
                    str = drow("Symbol")
                End If

            Next

            For Each drow As DataRow In Currencymaster.Rows
                If drow("Ftoken").ToString() = 0 Then
                    drow("ftoken") = Convert.ToDouble(HT_CurrNextFarcurr1(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy")))
                End If
            Next
            tmpCpfmaster = Currencymaster
            strInstrument = "'FUTCUR'"
        Else
            boolIsCurrency = False
            tmpCpfmaster = cpfmaster
            strInstrument = "'FUTSTK','FUTIDX','FUTIVX'"
        End If


        'tmpCpfmaster = cpfmaster
        If tmpCpfmaster.Columns.Contains("StrikeMod") = False Then
            tmpCpfmaster.Columns.Add("StrikeMod", GetType(Double))
        End If
        tmpCpfmaster.Columns("StrikeMod").DefaultValue = 0



        If strCompany = "NIFTY" And chkSkip.Checked = True Then




            For Each drr As DataRow In tmpCpfmaster.Rows
                If drr("InstrumentName").ToString() = "FUTIDX" Then
                    drr("StrikeMod") = 0
                Else
                    drr("StrikeMod") = (drr("Strike_Price") / 100) Mod 1
                End If
            Next







            'If tmpCpfmaster.Columns.Contains("StrikeMod") = False Then
            '    tmpCpfmaster.Columns.Add("StrikeMod", GetType(Double))
            'End If


            'For Each dr As DataRow In tmpCpfmaster.Select("Symbol='" & strCompany & "'")
            '    If dr("InstrumentName").ToString() = "FUTIDX" Then
            '        dr("StrikeMod") = 0
            '    Else
            '        dr("StrikeMod") = (dr("Strike_Price") / 100) Mod 1
            '    End If
            'Next
            Dim dt As New DataTable
            dt = tmpCpfmaster.Clone
            dt.AcceptChanges()
            Dim dr() As DataRow
            dr = tmpCpfmaster.Select("Symbol='" & strCompany & "' And StrikeMod = 0", "")
            For Each dataRow As DataRow In dr
                dt.ImportRow(dataRow)
            Next
            dtcpfmaster = dt

            '  dtcpfmaster = New DataView(tmpCpfmaster, "Symbol='" & strCompany & "' And StrikeMod = 0", "", DataViewRowState.CurrentRows).ToTable()
        Else
            Dim dt As New DataTable
            dt = tmpCpfmaster.Clone
            dt.AcceptChanges()
            Dim dr() As DataRow
            dr = tmpCpfmaster.Select("Symbol='" & strCompany & "'", "")
            For Each dataRow As DataRow In dr
                dt.ImportRow(dataRow)
            Next
            dtcpfmaster = dt
            '  dtcpfmaster = New DataView(tmpCpfmaster, "Symbol='" & strCompany & "'", "", DataViewRowState.CurrentRows).ToTable()
        End If
    End Sub

    Private Sub Refresh_Data(ByVal isWeekly As Boolean, ByVal IsOnLoad As Boolean)
        Dim ObjTrading As New trading
        Dim DtUTokens1, DtUTokens2, DtUTokens3 As DataTable
        Dim DtDTokens1, DtDTokens2, DtDTokens3 As DataTable



        If IsOnLoad Then
            'Viral 10-10-2018
            If (((flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API") Or NetMode = "TCP") Then
                If boolIsCurrency Then
                    Objsql.DeleteCurToken()
                Else
                    Objsql.DeleteFoToken()
                End If
            End If
        End If

        'While (flg_Greek_Calculations)
        '    Thread.Sleep(1000)
        'End While

        Try


            WriteLogMarketwatchlog("MarketWatch Refresh Data Start..")
            'flgrefreshdata = True

            'Write_Log2("Step2A:MarketWatch Refresh_Data Process Start..")
            'strCompany = cmbCompany.Text
            'If strCompany.Contains("INR") Then
            '    boolIsCurrency = True
            '    tmpCpfmaster = Currencymaster
            '    strInstrument = "'FUTCUR'"
            'Else
            '    boolIsCurrency = False
            '    tmpCpfmaster = cpfmaster
            '    strInstrument = "'FUTSTK','FUTIDX','FUTIVX'"
            'End If
            ' and (Strike_Price/100)% 1 = 0
            'If strCompany = "NIFTY" And chkSkip.Checked = True Then
            '    'If tmpCpfmaster.Columns.Contains("StrikeMod") = False Then
            '    '    tmpCpfmaster.Columns.Add("StrikeMod", GetType(Double))
            '    'End If


            '    'For Each dr As DataRow In tmpCpfmaster.Select("Symbol='" & strCompany & "'")
            '    '    If dr("InstrumentName").ToString() = "FUTIDX" Then
            '    '        dr("StrikeMod") = 0
            '    '    Else
            '    '        dr("StrikeMod") = (dr("Strike_Price") / 100) Mod 1
            '    '    End If
            '    'Next

            '    dtcpfmaster = New DataView(tmpCpfmaster, "Symbol='" & strCompany & "' And StrikeMod = 0", "", DataViewRowState.CurrentRows).ToTable()
            'Else
            '    dtcpfmaster = New DataView(tmpCpfmaster, "Symbol='" & strCompany & "'", "", DataViewRowState.CurrentRows).ToTable()
            'End If

            Dim tok As Long = 0
            If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "MIDCPNIFTY" Or strCompany = "USDINR") And RBcalVol.Checked = True Then

                Dim strike As Double
                If strCompany = "NIFTY" Then
                    strike = Val(eIdxprice("Nifty 50"))
                ElseIf strCompany = "BANKNIFTY" Then
                    strike = Val(eIdxprice("Nifty Bank"))
                ElseIf strCompany = "FINNIFTY" Then
                    strike = Val(eIdxprice("NiftyFinService"))
                ElseIf strCompany = "MIDCPNIFTY" Then
                    strike = Val(eIdxprice("NIFTYMIDSELECT"))
                End If

                Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & strike & "  and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                If drmonth2 Is Nothing Then
                    Return
                End If
                If drmonth2.Rows.Count <= 0 Then
                    Return
                End If

                tok = drmonth2.Rows(0)("token")


            End If
            Dim dt As New DataTable
            dt = dtcpfmaster.Clone
            dt.AcceptChanges()
            Dim drmonth() As DataRow
            Dim drmonth22 As DataTable
            drmonth = dtcpfmaster.Select("Symbol='" & strCompany & "' AND  InstrumentName IN (" & strInstrument & ")", "expdate1")
            Dim DtFMonthDatesyn As DataTable
            If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "USDINR") Then
                DtFMonthDatesyn = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName IN (" & strInstrument & ")", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token", "ftoken")
            Else
                DtFMonthDatesyn = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName IN (" & strInstrument & ")", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
            End If



            'drmonth22 = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ")", DataViewRowState.CurrentRows).to
            If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "USDINR") Then
                drmonth22 = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token", "ftoken")
                drmonth = drmonth22.Select()
            End If

            For Each dataRow As DataRow In drmonth
                dt.ImportRow(dataRow)
            Next


            'DtFMonthDate = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName IN (" & strInstrument & ")", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
            If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "USDINR") Then
                DtFMonthDate = New DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "ftoken")
            Else
                DtFMonthDate = New DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
            End If

            'To Search Future Tokens of all 3 months

            'To set Expiry Date on headers

            If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "MIDCPNIFTY" Or strCompany = "USDINR") Then
                Dim strike As Double
                Dim Fltpsyn As Long
                If RBcalVol.Checked = True Then
                    If strCompany = "NIFTY" Then
                        strike = Val(eIdxprice("Nifty 50"))
                    ElseIf strCompany = "BANKNIFTY" Then
                        strike = Val(eIdxprice("Nifty Bank"))
                    ElseIf strCompany = "FINNIFTY" Then
                        strike = Val(eIdxprice("NiftyFinService"))
                    ElseIf strCompany = "MIDCPNIFTY" Then
                        strike = Val(eIdxprice("NIFTYMIDSELECT"))
                    End If
                Else
                    'If boolIsCurrency Then
                    '    Fltpsyn = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))

                    'Else
                    '    Fltpsyn = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))
                    'End If
                    'strike = get_SFut(strCompany, DtFMonthDatesyn.Rows(0)("expdate1"), Convert.ToDouble(Fltpsyn))
                    If boolIsCurrency Then
                        Fltpsyn = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))

                    Else
                        Fltpsyn = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))
                        WriteLogMarketwatchlog("Fltpsyn=" + Fltpsyn.ToString())
                    End If
                    If boolIsCurrency = True Then
                        strike = get_SFutCurr(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltpsyn))
                    Else
                        strike = Val(hashsyn(DtFMonthDate.Rows(0)("expdate1") + "_" + strCompany) & "") ' get_SFut(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltpsyn))
                    End If

                    WriteLogMarketwatchlog("fltpprice=" + fltpprice.Count.ToString() + "strikeFltpsyn=" + strike.ToString() + strCompany.ToString() + DtFMonthDate.Rows(0)("expdate1").ToString() + Fltpsyn.ToString)
                End If

                If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "USDINR") Then
                    DtFMonthDate = New DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "ftoken")
                    'WriteLogMarketwatchlog("DtFMonthDate=" + DtFMonthDate.Rows.Count.ToString())
                Else
                    DtFMonthDate = New DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                End If
                ' DtFMonthDate = New DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

                Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & strike & "  and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                If drmonth2 Is Nothing Then
                    '  MessageBox.Show("1")
                    Return
                End If
                If drmonth2.Rows.Count <= 0 Then
                    '    MessageBox.Show("2,drmonth2=" + drmonth2.Rows.Count.ToString() + strCompany.ToString() + strInstrument.ToString () + strike.ToString ())
                    Return
                End If
                Try


                    tok = drmonth2.Rows(0)("token")

                Catch ex As Exception

                End Try
            End If
            WriteLogMarketwatchlog("MarketWatch All  DataTable Clear..")
            If Fltp1 = 0 Then
                dtMarketTable1.Clear()
            End If
            If Fltp2 = 0 Then
                dtMarketTable2.Clear()
            End If

            If Fltp3 = 0 Then
                dtMarketTable3.Clear()
            End If
            If chkfirst.Checked = True Then
                WriteLogMarketwatchlog("MarketWatch First Grid Data Start..")

                If Fltp1 = 0 And DtFMonthDate.Rows.Count >= 1 Then

                    'While (IsProcess)
                    '    Thread.Sleep(10)
                    'End While
                    dtMarketTable1.Clear()
                    If boolIsCurrency Then
                        DtUTokens1 = ObjTrading.UpperToken_Script_Curr(CDate(DtFMonthDate.Rows(0)("expdate1")), strCompany)
                    Else
                        Dim dtUpr As New DataTable
                        dtUpr = ObjTrading.UpperToken_Script(CDate(DtFMonthDate.Rows(0)("expdate1")), strCompany)

                        If strCompany = "NIFTY" And chkSkip.Checked = True Then
                            If dtUpr.Columns.Contains("StrikeMod") = False Then
                                dtUpr.Columns.Add("StrikeMod", GetType(Double))
                            End If
                            For Each dr As DataRow In dtUpr.Select()
                                'If dr("InstrumentName").ToString() = "FUTIDX" Then
                                'dr("StrikeMod") = 0
                                'Else
                                'dr("StrikeModdr") = (dr("Strike_Price") / 100) Mod 1
                                dr("StrikeMod") = (dr("Strike_Price") / 100) Mod 1
                                'End If
                            Next
                            DtUTokens1 = New DataView(dtUpr, "StrikeMod = 0", "", DataViewRowState.CurrentRows).ToTable()
                        Else
                            DtUTokens1 = dtUpr
                        End If
                    End If
                    If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "MIDCPNIFTY" Or strCompany = "USDINR") Then
                        Dim index1 As Double
                        If RBcalVol.Checked = True Then
                            If strCompany = "NIFTY" Then
                                index1 = Val(eIdxprice("Nifty 50"))
                            ElseIf strCompany = "BANKNIFTY" Then
                                index1 = Val(eIdxprice("Nifty Bank"))
                            ElseIf strCompany = "FINNIFTY" Then
                                index1 = Val(eIdxprice("NiftyFinService"))

                            ElseIf strCompany = "MIDCPNIFTY" Then
                                index1 = Val(eIdxprice("NIFTYMIDSELECT"))

                            End If
                        Else
                            'If boolIsCurrency Then
                            '    Fltp1 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))

                            'Else
                            '    Fltp1 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))
                            'End If
                            'index1 = get_SFut(strCompany, DtFMonthDatesyn.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
                            If boolIsCurrency Then
                                Fltp1 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))

                            Else
                                Fltp1 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))
                            End If
                            If boolIsCurrency = True Then
                                index1 = get_SFutCurr(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
                            Else
                                index1 = Val(hashsyn(DtFMonthDate.Rows(0)("expdate1") + "_" + strCompany) & "") 'get_SFut(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
                            End If


                        End If
                        Dim mT As Double
                        If CAL_GREEK_WITH_BCASTDATE = 1 Then
                            Dim BCast1 As Date
                            If boolIsCurrency Then
                                BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
                            Else
                                BCast1 = DateAdd(DateInterval.Second, Math.Max(VarFoBCurrentDate, VarCurBCurrentDate), CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
                            End If


                            mT = UDDateDiff(DateInterval.Day, BCast1, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()).Date)
                        Else
                            mT = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()).Date)
                        End If


                        Dim index As Double = (((index1 * Rateofinterest) / 365) * mT) + index1
                        Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & index & "  and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

                        tok = drmonth2.Rows(0)("token")
                        If NetMode = "TCP" Or NetMode = "JL" Then
                            If boolIsCurrency Then
                                Objsql.AppendCurTokens(CLng(tok).ToString)
                            Else
                                Objsql.AppendFoTokens(CLng(tok).ToString)
                            End If
                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
                            If boolIsCurrency Then
                                Objsql.AppendCurTokens(CLng(tok).ToString)
                            Else
                                Objsql.AppendFoTokens(CLng(tok).ToString)
                            End If
                        ElseIf NetMode = "API" Then
                            If boolIsCurrency = False Then
                                FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(tok)))
                            End If
                        End If
                    Else

                        If NetMode = "TCP" Or NetMode = "JL" Then
                            If boolIsCurrency Then
                                Objsql.AppendCurTokens(CLng(DtFMonthDate.Rows(0)(1) & "").ToString)
                            Else
                                Objsql.AppendFoTokens(CLng(DtFMonthDate.Rows(0)(1) & "").ToString)

                            End If
                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
                            If boolIsCurrency Then
                                Objsql.AppendCurTokens(CLng(DtFMonthDate.Rows(0)(1) & "").ToString)
                            Else
                                Objsql.AppendFoTokens(CLng(DtFMonthDate.Rows(0)(1) & "").ToString)

                            End If
                        ElseIf NetMode = "API" Then
                            If boolIsCurrency = False Then
                                FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(DtFMonthDate.Rows(0)(1) & "")))
                            End If

                        End If
                    End If


                    'dgv_Exp1.Sort(dgv_Exp1.Columns("col1Strike1"), System.ComponentModel.ListSortDirection.Ascending)
                    If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "MIDCPNIFTY" Or strCompany = "USDINR") Then
                        Dim index1 As Double
                        If RBcalVol.Checked = True Then
                            If strCompany = "NIFTY" Then
                                index1 = Val(eIdxprice("` 50"))
                            ElseIf strCompany = "BANKNIFTY" Then
                                index1 = Val(eIdxprice("Nifty Bank"))
                            ElseIf strCompany = "FINNIFTY" Then
                                index1 = Val(eIdxprice("NiftyFinService"))
                            ElseIf strCompany = "MIDCPNIFTY" Then
                                index1 = Val(eIdxprice("NIFTYMIDSELECT"))
                            End If
                        Else
                            'If boolIsCurrency Then
                            '    Fltp1 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))

                            'Else
                            '    Fltp1 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))
                            'End If
                            'index1 = get_SFut(strCompany, DtFMonthDatesyn.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
                            If boolIsCurrency Then
                                Fltp1 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))

                            Else
                                Fltp1 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))
                            End If
                            If boolIsCurrency = True Then
                                index1 = get_SFutCurr(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
                            Else
                                index1 = Val(hashsyn(DtFMonthDate.Rows(0)("expdate1") + "_" + strCompany) & "") ' get_SFut(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
                            End If


                        End If

                        Dim mT As Double
                        If CAL_GREEK_WITH_BCASTDATE = 1 Then
                            Dim BCast1 As Date
                            If boolIsCurrency Then
                                BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
                            Else
                                BCast1 = DateAdd(DateInterval.Second, Math.Max(VarFoBCurrentDate, VarCurBCurrentDate), CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
                            End If

                            mT = UDDateDiff(DateInterval.Day, BCast1, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()).Date)
                        Else
                            mT = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()).Date)
                        End If

                        Dim index As Double = (((index1 * Rateofinterest) / 365) * mT) + index1
                        Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & index & "  and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

                        tok = drmonth2.Rows(0)("token")
                        search_fields(dtMarketTable1, DtUTokens1, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()), Val(CLng(tok) & ""), MidStrike1, isWeekly, index)
                        WriteLogMarketwatchlog("dtMarketTable1 data=" + dtMarketTable1.Rows.Count.ToString())
                        If boolIsCurrency Then
                            Fltp1 = Val(Currfltpprice(CLng(tok)))
                        Else
                            Fltp1 = Val(fltpprice(CLng(tok)))
                        End If
                    Else
                        search_fields(dtMarketTable1, DtUTokens1, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()), Val(DtFMonthDate.Rows(0)(1) & ""), MidStrike1, isWeekly, (CLng(tok)))
                        WriteLogMarketwatchlog("dtMarketTable1 data=" + dtMarketTable1.Rows.Count.ToString())
                        If boolIsCurrency Then
                            Fltp1 = Val(Currfltpprice(CLng(tok)))

                        Else
                            Fltp1 = Val(fltpprice(CLng(tok)))
                        End If
                        '  Dim dd As Double = get_SFut(strCompany, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()), Convert.ToDouble(Fltp1))
                    End If


                    'Header1 = Header1 & "       FUTURE LTP : " & Fltp1
                    'collapsExpandPanel1.HeaderText = Header1
                End If

                dgv_Exp1.DataSource = dtMarketTable1
                WriteLogMarketwatchlog("MarketWatch First Grid Data Stop..")
            End If
            If chksecond.Checked = True Then

                WriteLogMarketwatchlog("MarketWatch Second Grid Data Start..")
                If Fltp2 = 0 And DtFMonthDate.Rows.Count >= 2 Then
                    'While (IsProcess)
                    '    Thread.Sleep(10)
                    'End While
                    dtMarketTable2.Clear()
                    If boolIsCurrency Then
                        DtUTokens2 = ObjTrading.UpperToken_Script_Curr(CDate(DtFMonthDate.Rows(1)("expdate1")), strCompany)
                    Else
                        Dim dtUpr As New DataTable
                        dtUpr = ObjTrading.UpperToken_Script(CDate(DtFMonthDate.Rows(1)("expdate1")), strCompany)

                        If strCompany = "NIFTY" And chkSkip.Checked = True Then
                            If dtUpr.Columns.Contains("StrikeMod") = False Then
                                dtUpr.Columns.Add("StrikeMod", GetType(Double))
                            End If
                            For Each dr As DataRow In dtUpr.Select()
                                'If dr("InstrumentName").ToString() = "FUTIDX" Then
                                'dr("StrikeMod") = 0
                                'Else
                                dr("StrikeMod") = (dr("Strike_Price") / 100) Mod 1
                                'End If
                            Next
                            DtUTokens2 = New DataView(dtUpr, "StrikeMod = 0", "", DataViewRowState.CurrentRows).ToTable()
                        Else
                            DtUTokens2 = dtUpr
                        End If



                    End If
                    If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "MIDCPNIFTY" Or strCompany = "USDINR") Then
                        Dim index1 As Double
                        If RBcalVol.Checked = True Then
                            If strCompany = "NIFTY" Then
                                index1 = Val(eIdxprice("Nifty 50"))
                            ElseIf strCompany = "BANKNIFTY" Then
                                index1 = Val(eIdxprice("Nifty Bank"))
                            ElseIf strCompany = "FINNIFTY" Then
                                index1 = Val(eIdxprice("NiftyFinService"))
                            ElseIf strCompany = "MIDCPNIFTY" Then
                                index1 = Val(eIdxprice("NIFTYMIDSELECT"))
                            End If
                        Else
                            'DtFMonthDate
                            If boolIsCurrency Then
                                Fltp2 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(1)("token"))))

                            Else
                                Fltp2 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(1)("token"))))
                            End If
                            If boolIsCurrency Then
                                index1 = get_SFutCurr(strCompany, DtFMonthDate.Rows(1)("expdate1"), Convert.ToDouble(Fltp2))
                            Else
                                index1 = Val(hashsyn(DtFMonthDate.Rows(1)("expdate1") + "_" + strCompany) & "") ' get_SFut(strCompany, DtFMonthDate.Rows(1)("expdate1"), Convert.ToDouble(Fltp2))
                            End If

                            'If boolIsCurrency Then
                            '    Fltp2 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(1)("token"))))

                            'Else
                            '    Fltp2 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(1)("token"))))
                            'End If
                            'index1 = get_SFut(strCompany, DtFMonthDatesyn.Rows(1)("expdate1"), Convert.ToDouble(Fltp2))
                        End If
                        Dim mT As Double
                        If CAL_GREEK_WITH_BCASTDATE = 1 Then
                            Dim BCast1 As Date
                            If boolIsCurrency Then
                                BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
                            Else
                                BCast1 = DateAdd(DateInterval.Second, Math.Max(VarFoBCurrentDate, VarCurBCurrentDate), CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
                            End If

                            mT = UDDateDiff(DateInterval.Day, BCast1, CDate(DtFMonthDate.Rows(1)("expdate1").ToString()).Date)
                        Else
                            mT = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(1)("expdate1").ToString()).Date)
                        End If


                        Dim index As Double = (((index1 * Rateofinterest) / 365) * mT) + index1
                        Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & index & "  and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                        Try
                            tok = drmonth2.Rows(0)("token")
                        Catch ex As Exception
                            tok = 0
                        End Try

                        If NetMode = "TCP" Or NetMode = "JL" Then
                            If boolIsCurrency Then
                                Objsql.AppendCurTokens(CLng(tok).ToString)
                            Else
                                Objsql.AppendFoTokens(CLng(tok).ToString)

                            End If
                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
                            If boolIsCurrency Then
                                Objsql.AppendCurTokens(CLng(tok).ToString)
                            Else
                                Objsql.AppendFoTokens(CLng(tok).ToString)

                            End If
                        ElseIf NetMode = "API" Then
                            If boolIsCurrency = False Then
                                FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(tok)))
                            End If

                        End If
                        search_fields(dtMarketTable2, DtUTokens2, CDate(DtFMonthDate.Rows(1)("expdate1").ToString()), Val(tok & ""), MidStrike2, isWeekly, index)
                        WriteLogMarketwatchlog("dtMarketTable2 data=" + dtMarketTable2.Rows.Count.ToString())
                        'dgv_exp2.Sort(dgv_exp2.Columns("col2Strike1"), System.ComponentModel.ListSortDirection.Ascending)
                        If boolIsCurrency Then
                            Fltp2 = Val(Currfltpprice(CLng(tok)))
                        Else
                            Fltp2 = Val(fltpprice(CLng(tok)))
                        End If

                        'Header2 = Header2 & "       FUTURE LTP : " & Fltp2
                        'collapsExpandPanel2.HeaderText = Header2

                    Else
                        If NetMode = "TCP" Or NetMode = "JL" Then
                            If boolIsCurrency Then
                                Objsql.AppendCurTokens(CLng(DtFMonthDate.Rows(1)(1) & "").ToString)
                            Else
                                Objsql.AppendFoTokens(CLng(DtFMonthDate.Rows(1)(1) & "").ToString)

                            End If
                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
                            If boolIsCurrency Then
                                Objsql.AppendCurTokens(CLng(DtFMonthDate.Rows(1)(1) & "").ToString)
                            Else
                                Objsql.AppendFoTokens(CLng(DtFMonthDate.Rows(1)(1) & "").ToString)

                            End If
                        ElseIf NetMode = "API" Then
                            If boolIsCurrency = False Then
                                FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(DtFMonthDate.Rows(1)(1) & "")))
                            End If
                        End If
                        search_fields(dtMarketTable2, DtUTokens2, CDate(DtFMonthDate.Rows(1)("expdate1").ToString()), Val(DtFMonthDate.Rows(1)(1) & ""), MidStrike2, isWeekly, 0)
                        WriteLogMarketwatchlog("dtMarketTable2 data=" + dtMarketTable2.Rows.Count.ToString())
                        'dgv_exp2.Sort(dgv_exp2.Columns("col2Strike1"), System.ComponentModel.ListSortDirection.Ascending)
                        If boolIsCurrency Then
                            Fltp2 = Val(Currfltpprice(CLng(DtFMonthDate.Rows(1)(1))))
                        Else
                            Fltp2 = Val(fltpprice(CLng(DtFMonthDate.Rows(1)(1))))
                        End If

                        'Header2 = Header2 & "       FUTURE LTP : " & Fltp2
                        'collapsExpandPanel2.HeaderText = Header2
                    End If

                End If

                dgv_exp2.DataSource = dtMarketTable2
                WriteLogMarketwatchlog("MarketWatch Second Grid Data Stop..")
            End If
            If chkthird.Checked = True Then

                WriteLogMarketwatchlog("MarketWatch third Grid Data Start..")
                If Fltp3 = 0 And DtFMonthDate.Rows.Count >= 3 Then
                    'While (IsProcess)
                    '    Thread.Sleep(10)
                    'End While
                    dtMarketTable3.Clear()
                    If boolIsCurrency Then
                        DtUTokens3 = ObjTrading.UpperToken_Script_Curr(CDate(DtFMonthDate.Rows(2)("expdate1")), strCompany)
                    Else
                        Dim dtUpr As New DataTable
                        dtUpr = ObjTrading.UpperToken_Script(CDate(DtFMonthDate.Rows(2)("expdate1")), strCompany)
                        If strCompany = "NIFTY" And chkSkip.Checked = True Then
                            If dtUpr.Columns.Contains("StrikeMod") = False Then
                                dtUpr.Columns.Add("StrikeMod", GetType(Double))
                            End If
                            For Each dr As DataRow In dtUpr.Select()
                                'If dr("InstrumentName").ToString() = "FUTIDX" Then
                                'dr("StrikeMod") = 0
                                'Else
                                dr("StrikeMod") = (dr("Strike_Price") / 100) Mod 1
                                'End If
                            Next
                            DtUTokens3 = New DataView(dtUpr, "StrikeMod = 0", "", DataViewRowState.CurrentRows).ToTable()
                        Else
                            DtUTokens3 = dtUpr
                        End If

                    End If
                    'If isWeekly = True And strCompany = "BANKNIFTY" Then
                    '    Dim index1 As Double = Val(eIdxprice("Nifty Bank"))
                    If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "MIDCPNIFTY" Or strCompany = "USDINR") Then
                        Dim index1 As Double
                        If RBcalVol.Checked = True Then
                            If strCompany = "NIFTY" Then
                                index1 = Val(eIdxprice("Nifty 50"))
                            ElseIf strCompany = "BANKNIFTY" Then
                                index1 = Val(eIdxprice("Nifty Bank"))
                            ElseIf strCompany = "FINNIFTY" Then
                                index1 = Val(eIdxprice("NiftyFinService"))
                            ElseIf strCompany = "MIDCPNIFTY" Then
                                index1 = Val(eIdxprice("NIFTYMIDSELECT"))
                            End If
                        Else
                            If boolIsCurrency Then
                                Fltp1 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(2)("token"))))

                            Else
                                Fltp1 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(2)("token"))))
                            End If
                            Dim dtsyn As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND     expdate1 =#" & fDate(CDate(DtFMonthDate.Rows(2)("expdate1"))) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If dtsyn Is Nothing Then
                                Return
                            End If
                            If dtsyn.Rows.Count <= 0 Then
                                Return
                            End If

                            If boolIsCurrency Then
                                index1 = get_SFutCurr(strCompany, dtsyn.Rows(2)("expdate1"), Convert.ToDouble(Fltp1))
                            Else
                                index1 = Val(hashsyn(dtsyn.Rows(2)("expdate1") + "_" + strCompany) & "") ' get_SFut(strCompany, dtsyn.Rows(2)("expdate1"), Convert.ToDouble(Fltp1))
                            End If

                            'If boolIsCurrency Then
                            '    Fltp1 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(2)("token"))))

                            'Else
                            '    Fltp1 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(2)("token"))))
                            'End If
                            'index1 = get_SFut(strCompany, DtFMonthDatesyn.Rows(2)("expdate1"), Convert.ToDouble(Fltp1))
                        End If


                        Dim mT As Double
                        If CAL_GREEK_WITH_BCASTDATE = 1 Then
                            Dim BCast1 As Date
                            If boolIsCurrency Then
                                BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
                            Else
                                BCast1 = DateAdd(DateInterval.Second, Math.Max(VarFoBCurrentDate, VarCurBCurrentDate), CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
                            End If

                            mT = UDDateDiff(DateInterval.Day, BCast1, CDate(DtFMonthDate.Rows(2)("expdate1").ToString()).Date)
                        Else
                            mT = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(2)("expdate1").ToString()).Date)
                        End If
                        Dim index As Double = (((index1 * Rateofinterest) / 365) * mT) + index1
                        Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & index & "  and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

                        tok = drmonth2.Rows(0)("token")
                        If NetMode = "TCP" Or NetMode = "JL" Then
                            If boolIsCurrency Then
                                Objsql.AppendCurTokens(CLng(tok))
                            Else
                                Objsql.AppendFoTokens(CLng(tok))

                            End If
                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
                            If boolIsCurrency Then
                                Objsql.AppendCurTokens(CLng(tok))
                            Else
                                Objsql.AppendFoTokens(CLng(tok))

                            End If
                        ElseIf NetMode = "API" Then
                            If boolIsCurrency = False Then
                                FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(tok)))
                            End If
                        End If
                        search_fields(dtMarketTable3, DtUTokens3, CDate(DtFMonthDate.Rows(2)("expdate1").ToString()), Val(tok & ""), MidStrike3, isWeekly, index)
                        WriteLogMarketwatchlog("dtMarketTable3 data=" + dtMarketTable3.Rows.Count.ToString())
                        'dgv_exp3.Sort(dgv_exp3.Columns("col3Strike1"), System.ComponentModel.ListSortDirection.Ascending)
                        If boolIsCurrency Then
                            Fltp3 = Val(Currfltpprice(CLng(tok)))
                        Else
                            Fltp3 = Val(fltpprice(CLng(tok)))
                        End If

                        'Header3 = Header3 & "       FUTURE LTP : " & Fltp3
                        'collapsExpandPanel3.HeaderText = Header3
                    Else
                        If NetMode = "TCP" Or NetMode = "JL" Then
                            If boolIsCurrency Then
                                Objsql.AppendCurTokens(CLng(DtFMonthDate.Rows(2)(1) & "").ToString)
                            Else
                                Objsql.AppendFoTokens(CLng(DtFMonthDate.Rows(2)(1) & "").ToString)

                            End If
                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
                            If boolIsCurrency Then
                                Objsql.AppendCurTokens(CLng(DtFMonthDate.Rows(2)(1) & "").ToString)
                            Else
                                Objsql.AppendFoTokens(CLng(DtFMonthDate.Rows(2)(1) & "").ToString)

                            End If
                        ElseIf NetMode = "API" Then
                            If boolIsCurrency = False Then
                                FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(DtFMonthDate.Rows(2)(1) & "")))
                            End If
                        End If
                        search_fields(dtMarketTable3, DtUTokens3, CDate(DtFMonthDate.Rows(2)("expdate1").ToString()), Val(DtFMonthDate.Rows(2)(1) & ""), MidStrike3, isWeekly, 0)
                        WriteLogMarketwatchlog("dtMarketTable3 data=" + dtMarketTable3.Rows.Count.ToString())
                        'dgv_exp3.Sort(dgv_exp3.Columns("col3Strike1"), System.ComponentModel.ListSortDirection.Ascending)
                        If boolIsCurrency Then
                            Fltp3 = Val(Currfltpprice(CLng(DtFMonthDate.Rows(2)(1))))
                        Else
                            Fltp3 = Val(fltpprice(CLng(DtFMonthDate.Rows(2)(1))))
                        End If

                        'Header3 = Header3 & "       FUTURE LTP : " & Fltp3
                        'collapsExpandPanel3.HeaderText = Header3
                    End If



                End If

                dgv_exp3.DataSource = dtMarketTable3
                WriteLogMarketwatchlog("MarketWatch third Grid Data Stop..")
            End If
        Catch ex As Exception
            WriteLogMarketwatchlog("MarketWatch All Data Fill error..")
        End Try
        WriteLogMarketwatchlog("MarketWatch All Data Fill..")
        'new header Fill mthod
        WriteLogMarketwatchlog("MarketWatch Fill Header Data Stop..")
        'If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY") Then
        '    If boolIsCurrency Then
        '        If DtFMonthDate.Rows.Count >= 1 Then
        '            Header1 = Header1 & "       [FUTURE LTP : " & Val(Val(eIdxprice(strsymbol))) & "]       [Change in Future: ""]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
        '            collapsExpandPanel1.HeaderText = Header1
        '        Else
        '            collapsExpandPanel1.HeaderText = ""
        '        End If

        '        If DtFMonthDate.Rows.Count >= 2 Then
        '            Header2 = Header2 & "       [FUTURE LTP : " & Val(Val(eIdxprice(strsymbol))) & "]       [Change in Future: ""]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
        '            collapsExpandPanel2.HeaderText = Header2
        '        Else
        '            collapsExpandPanel2.HeaderText = ""
        '        End If

        '        If DtFMonthDate.Rows.Count >= 3 Then
        '            Header3 = Header3 & "       [FUTURE LTP : " & Val(Val(eIdxprice(strsymbol))) & "]       [Change in Future: ""]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
        '            collapsExpandPanel3.HeaderText = Header3
        '        Else
        '            collapsExpandPanel3.HeaderText = ""
        '        End If
        '    Else
        '        If DtFMonthDate.Rows.Count >= 1 Then
        '            Header1 = Header1 & "       [FUTURE LTP : " & Val(Val(eIdxprice(strsymbol))) & "]       [Change in Future: ""]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
        '            collapsExpandPanel1.HeaderText = Header1
        '        Else
        '            collapsExpandPanel1.HeaderText = ""
        '        End If

        '        If DtFMonthDate.Rows.Count >= 2 Then
        '            Header2 = Header2 & "       [FUTURE LTP : " & Val(Val(eIdxprice(strsymbol))) & "]       [Change in Future: ""]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
        '            collapsExpandPanel2.HeaderText = Header2
        '        Else
        '            collapsExpandPanel2.HeaderText = ""
        '        End If

        '        If DtFMonthDate.Rows.Count >= 3 Then
        '            Header3 = Header3 & "       [FUTURE LTP : " & Val(Val(eIdxprice(strsymbol))) & "]       [Change in Future: ""]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
        '            collapsExpandPanel3.HeaderText = Header3
        '        Else
        '            collapsExpandPanel3.HeaderText = ""
        '        End If
        '    End If
        'Else
        '    If boolIsCurrency Then
        '        If DtFMonthDate.Rows.Count >= 1 Then
        '            Header1 = Header1 & "       [FUTURE LTP : " & Val(Currfltpprice(CLng(DtFMonthDate.Rows(0)("token")))) & "]       [Change in Future: " & (Val(Currfltpprice(CLng(DtFMonthDate.Rows(0)("token")))) - Val(closeprice(CLng(DtFMonthDate.Rows(0)("token"))))).ToString("0.00") & "]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
        '            collapsExpandPanel1.HeaderText = Header1
        '        Else
        '            collapsExpandPanel1.HeaderText = ""
        '        End If

        '        If DtFMonthDate.Rows.Count >= 2 Then
        '            Header2 = Header2 & "       [FUTURE LTP : " & Val(Currfltpprice(CLng(DtFMonthDate.Rows(1)("token")))) & "]       [Change in Future: " & (Val(Currfltpprice(CLng(DtFMonthDate.Rows(1)("token")))) - Val(closeprice(CLng(DtFMonthDate.Rows(1)("token"))))).ToString("0.00") & "]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
        '            collapsExpandPanel2.HeaderText = Header2
        '        Else
        '            collapsExpandPanel2.HeaderText = ""
        '        End If

        '        If DtFMonthDate.Rows.Count >= 3 Then
        '            Header3 = Header3 & "       [FUTURE LTP : " & Val(Currfltpprice(CLng(DtFMonthDate.Rows(2)("token")))) & "]       [Change in Future: " & (Val(Currfltpprice(CLng(DtFMonthDate.Rows(2)("token")))) - Val(closeprice(CLng(DtFMonthDate.Rows(2)("token"))))).ToString("0.00") & "]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
        '            collapsExpandPanel3.HeaderText = Header3
        '        Else
        '            collapsExpandPanel3.HeaderText = ""
        '        End If
        '    Else
        '        If DtFMonthDate.Rows.Count >= 1 Then
        '            Header1 = Header1 & "       [FUTURE LTP : " & Val(fltpprice(CLng(DtFMonthDate.Rows(0)("token")))) & "]       [Change in Future: " & (Val(fltpprice(CLng(DtFMonthDate.Rows(0)("token")))) - Val(closeprice(CLng(DtFMonthDate.Rows(0)("token"))))).ToString("0.00") & "]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
        '            collapsExpandPanel1.HeaderText = Header1
        '        Else
        '            collapsExpandPanel1.HeaderText = ""
        '        End If

        '        If DtFMonthDate.Rows.Count >= 2 Then
        '            Header2 = Header2 & "       [FUTURE LTP : " & Val(fltpprice(CLng(DtFMonthDate.Rows(1)("token")))) & "]       [Change in Future: " & (Val(fltpprice(CLng(DtFMonthDate.Rows(1)("token")))) - Val(closeprice(CLng(DtFMonthDate.Rows(1)("token"))))).ToString("0.00") & "]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
        '            collapsExpandPanel2.HeaderText = Header2
        '        Else
        '            collapsExpandPanel2.HeaderText = ""
        '        End If

        '        If DtFMonthDate.Rows.Count >= 3 Then
        '            Header3 = Header3 & "       [FUTURE LTP : " & Val(fltpprice(CLng(DtFMonthDate.Rows(2)("token")))) & "]       [Change in Future: " & (Val(fltpprice(CLng(DtFMonthDate.Rows(2)("token")))) - Val(closeprice(CLng(DtFMonthDate.Rows(2)("token"))))).ToString("0.00") & "]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
        '            collapsExpandPanel3.HeaderText = Header3
        '        Else
        '            collapsExpandPanel3.HeaderText = ""
        '        End If
        '    End If

        'End If

        'flgrefreshdata = False

        'dgv_Exp1.Sort(dgv_Exp1.Columns("col1Strike1"), System.ComponentModel.ListSortDirection.Ascending)
        'dgv_exp2.Sort(dgv_exp2.Columns("col2Strike1"), System.ComponentModel.ListSortDirection.Ascending)
        'dgv_exp3.Sort(dgv_exp3.Columns("col3Strike1"), System.ComponentModel.ListSortDirection.Ascending)
        'Write_Log2("Step2A:MarketWatch Refresh_Data Process Stop..")
        WriteLogMarketwatchlog("MarketWatch Refresh Data Stop..")
    End Sub


    Public Sub search_fields(ByRef dtMarketTable As DataTable, ByVal dtStrike As DataTable, ByVal Mdate As Date, ByVal mtoken As Long, ByRef MidStrike As Double, ByVal isWeekly As Boolean, ByVal index As Double)
        Try


            Dim dr1 As DataRow
            Dim strike As Double
            'Dim MidStrike As Double
            Dim CallHashTokens As New Hashtable
            Dim PutHashTokens As New Hashtable
            If chkIsWeekly.Checked = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "MIDCPNIFTY" Or strCompany = "USDINR") Then
                'If boolIsCurrency Then
                '    If Val(Currfltpprice(CLng(mtoken))) = 0 Then Exit Sub
                'Else
                '    If Val(fltpprice(CLng(mtoken))) = 0 Then Exit Sub
                'End If
                'strike = Val(eIdxprice("Nifty Bank"))
                If RBcalVol.Checked = True Then
                    If strCompany = "NIFTY" Then
                        strike = Val(eIdxprice("Nifty 50"))
                    ElseIf strCompany = "BANKNIFTY" Then
                        strike = Val(eIdxprice("Nifty Bank"))
                    ElseIf strCompany = "FINNIFTY" Then
                        strike = Val(eIdxprice("NiftyFinService"))

                    ElseIf strCompany = "MIDCPNIFTY" Then
                        strike = Val(eIdxprice("NIFTYMIDSELECT"))
                    End If
                Else
                    If strCompany = "NIFTY" Then
                        strike = index 'Val(eIdxprice("Nifty 50"))
                    ElseIf strCompany = "BANKNIFTY" Then
                        strike = index ' Val(eIdxprice("Nifty Bank"))
                    ElseIf strCompany = "FINNIFTY" Then
                        strike = index
                    Else
                        strike = index
                    End If
                End If
                If Val(strike) = 0 Then Exit Sub
                'If boolIsCurrency Then
                '    Fltp1 = Val(Currfltpprice(CLng(dtStrike.Rows(0)("CE_token"))))
                '    Fltp1 = Val(fltpprice(mtoken))
                'Else
                '    Fltp1 = Val(fltpprice(CLng(dtStrike.Rows(0)("CE_token"))))
                '    Fltp1 = Val(fltpprice(mtoken))
                'End If
                'strike = get_SFut(strCompany, Mdate, Convert.ToDouble(Fltp1))
            Else
                If boolIsCurrency Then
                    If Val(Currfltpprice(CLng(mtoken))) = 0 Then Exit Sub
                Else

                    If NetMode = "API" And flgAPI_K = "TRUEDATA" Then
                        FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(mtoken)))
                    End If
                    If Val(fltpprice(CLng(mtoken))) = 0 Then Exit Sub
                End If
            End If


            Dim drow1() As DataRow
            drow1 = dtStrike.Select("", "Strike_Price")

            For i As Integer = 0 To dtStrike.Select("", "Strike_Price").Length - 1
                If boolIsCurrency Then
                    If (drow1(i)("strike_price") > IIf(isWeekly = True, strike, Val(Currfltpprice(CLng(mtoken))))) Then
                        Dim val1, val2 As Double
                        val1 = Math.Abs(Val(Currfltpprice(CLng(mtoken))) - drow1(i)("strike_price"))
                        If drow1.GetLowerBound(0) > (i - 1) Then
                            val2 = Math.Abs(Val(Currfltpprice(CLng(mtoken))) - drow1(i - 1)("strike_price"))
                        End If
                        If (val1 < val2) Then
                            MidStrike = drow1(i)("strike_price")

                            CallHashTokens.Add(drow1(i)("strike_price"), drow1(i)("CE_Token"))
                            PutHashTokens.Add(drow1(i)("strike_price"), drow1(i)("PE_Token"))
                            For j As Integer = 1 To iUpDownstrike
                                If (i - j) >= drow1.GetLowerBound(0) Then
                                    CallHashTokens.Add(drow1(i - j)("strike_price"), drow1(i - j)("CE_Token"))
                                    PutHashTokens.Add(drow1(i - j)("strike_price"), drow1(i - j)("PE_Token"))
                                End If
                            Next
                            For k As Integer = 1 To iUpDownstrike
                                If (i + k) <= drow1.GetUpperBound(0) Then
                                    CallHashTokens.Add(drow1(i + k)("strike_price"), drow1(i + k)("CE_Token"))
                                    PutHashTokens.Add(drow1(i + k)("strike_price"), drow1(i + k)("PE_Token"))
                                End If
                            Next
                            Exit For
                        Else
                            i = i - 1
                            MidStrike = drow1(i)("strike_price")
                            CallHashTokens.Add(drow1(i)("strike_price"), drow1(i)("CE_Token"))
                            PutHashTokens.Add(drow1(i)("strike_price"), drow1(i)("PE_Token"))
                            For j As Integer = 1 To iUpDownstrike
                                If (i - j) >= drow1.GetLowerBound(0) Then
                                    CallHashTokens.Add(drow1(i - j)("strike_price"), drow1(i - j)("CE_Token"))
                                    PutHashTokens.Add(drow1(i - j)("strike_price"), drow1(i - j)("PE_Token"))
                                End If
                            Next
                            For k As Integer = 1 To iUpDownstrike
                                If (i + k) <= drow1.GetUpperBound(0) Then
                                    CallHashTokens.Add(drow1(i + k)("strike_price"), drow1(i + k)("CE_Token"))
                                    PutHashTokens.Add(drow1(i + k)("strike_price"), drow1(i + k)("PE_Token"))
                                End If
                            Next
                            Exit For
                        End If
                    End If
                Else
                    If (drow1(i)("strike_price") > IIf(isWeekly, strike, Val(fltpprice(CLng(mtoken))))) Then
                        Dim val1, val2 As Double
                        val1 = Math.Abs(Val(fltpprice(CLng(mtoken))) - drow1(i)("strike_price"))
                        If drow1.GetLowerBound(0) > (i - 1) Then
                            val2 = Math.Abs(Val(fltpprice(CLng(mtoken))) - drow1(i - 1)("strike_price"))
                        End If

                        If (val1 < val2) Then
                            MidStrike = drow1(i)("strike_price")

                            CallHashTokens.Add(drow1(i)("strike_price"), drow1(i)("CE_Token"))
                            PutHashTokens.Add(drow1(i)("strike_price"), drow1(i)("PE_Token"))
                            For j As Integer = 1 To iUpDownstrike
                                If (i - j) >= drow1.GetLowerBound(0) Then
                                    CallHashTokens.Add(drow1(i - j)("strike_price"), drow1(i - j)("CE_Token"))
                                    PutHashTokens.Add(drow1(i - j)("strike_price"), drow1(i - j)("PE_Token"))
                                End If
                            Next
                            For k As Integer = 1 To iUpDownstrike
                                If (i + k) <= drow1.GetUpperBound(0) Then
                                    CallHashTokens.Add(drow1(i + k)("strike_price"), drow1(i + k)("CE_Token"))
                                    PutHashTokens.Add(drow1(i + k)("strike_price"), drow1(i + k)("PE_Token"))
                                End If
                            Next
                            Exit For
                        Else
                            i = i - 1
                            MidStrike = drow1(i)("strike_price")
                            CallHashTokens.Add(drow1(i)("strike_price"), drow1(i)("CE_Token"))
                            PutHashTokens.Add(drow1(i)("strike_price"), drow1(i)("PE_Token"))
                            For j As Integer = 1 To iUpDownstrike
                                If (i - j) >= drow1.GetLowerBound(0) Then
                                    CallHashTokens.Add(drow1(i - j)("strike_price"), drow1(i - j)("CE_Token"))
                                    PutHashTokens.Add(drow1(i - j)("strike_price"), drow1(i - j)("PE_Token"))
                                End If
                            Next
                            For k As Integer = 1 To iUpDownstrike
                                If (i + k) <= drow1.GetUpperBound(0) Then
                                    CallHashTokens.Add(drow1(i + k)("strike_price"), drow1(i + k)("CE_Token"))
                                    PutHashTokens.Add(drow1(i + k)("strike_price"), drow1(i + k)("PE_Token"))
                                End If
                            Next
                            Exit For
                        End If
                    End If
                End If
            Next

            For Each Entry1 As Collections.DictionaryEntry In CallHashTokens

                dr1 = dtMarketTable.NewRow

                'to add strike prices
                dr1("strike1") = Entry1.Key
                dr1("strike2") = Entry1.Key
                dr1("strike3") = Entry1.Key
                dr1("CE") = 0
                dr1("PE") = 0
                dr1("futltp") = 0
                dr1("CallOI") = 0
                dr1("PutOI") = 0
                dr1("CallToken") = Entry1.Value
                dr1("PutToken") = PutHashTokens(Entry1.Key)

                dr1("CallPreToken") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, -1, "CE")
                dr1("CallPre2Token") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, -2, "CE")
                dr1("CallNextToken") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, 1, "CE")
                dr1("CallNext2Token") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, 2, "CE")

                dr1("PutPreToken") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, -1, "PE")
                dr1("PutPre2Token") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, -2, "PE")
                dr1("PutNextToken") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, 1, "PE")
                dr1("PutNext2Token") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, 2, "PE")

                dr1("Symbol") = strCompany
                dr1("Maturity") = Mdate

                If boolIsCurrency Then
                    If Currltpprice.Contains(CLng(Val(dr1("CallToken") & ""))) Then
                        dr1("CE") = Val(Currltpprice(CLng(dr1("CallToken"))))   'Call Strike Price
                    End If

                    If Currltpprice.Contains(CLng(Val(dr1("PutToken") & ""))) Then
                        dr1("PE") = Val(Currltpprice(CLng(dr1("PutToken"))))    'Put Strike Price
                    End If

                    If Currfltpprice.Contains(mtoken) Then
                        dr1("futltp") = Val(Currfltpprice(mtoken)) 'Future LTP
                        'dblFutPrice1 = dr1("futltp")    ' For ShortStraddle and long Straddle
                    End If
                Else
                    If ltpprice.Contains(CLng(Val(dr1("CallToken") & ""))) Then
                        dr1("CE") = Val(ltpprice(CLng(dr1("CallToken"))))   'Call Strike Price
                    End If
                    If ltpprice.Contains(CLng(Val(dr1("PutToken") & ""))) Then
                        dr1("PE") = Val(ltpprice(CLng(dr1("PutToken"))))    'Put Strike Price
                    End If

                    'Status/StopLoss

                    If TrendStopLoss.Contains(CLng(Val(dr1("CallToken") & ""))) Then
                        dr1("CEStopLoss") = Val(TrendStopLoss(CLng(dr1("CallToken"))))
                    End If
                    If TrendStopLoss.Contains(CLng(Val(dr1("PutToken") & ""))) Then
                        dr1("PEStopLoss") = Val(TrendStopLoss(CLng(dr1("PutToken"))))
                    End If


                    If TrendStatus.Contains(CLng(Val(dr1("CallToken") & ""))) Then
                        dr1("CEStatus") = GetStatus(Val(TrendStatus(CLng(dr1("CallToken")))), Val(TrendStopLoss(CLng(dr1("CallToken")))), Val(ltpprice(CLng(dr1("CallToken")))))
                    End If
                    If TrendStatus.Contains(CLng(Val(dr1("PutToken") & ""))) Then
                        dr1("PEStatus") = GetStatus(Val(TrendStatus(CLng(dr1("PutToken")))), Val(TrendStopLoss(CLng(dr1("PutToken")))), Val(ltpprice(CLng(dr1("PutToken")))))
                    End If
                    If isWeekly = True Then
                        dr1("futltp") = Val(index) 'Future LTP
                        'dblFutPrice1 = dr1("futltp")   '' For ShortStraddle and long Straddle
                    Else
                        If fltpprice.Contains(mtoken) Then
                            dr1("futltp") = Val(fltpprice(mtoken)) 'Future LTP
                            'dblFutPrice1 = dr1("futltp") ' For ShortStraddle and long Straddle
                        End If
                    End If

                End If
                dtMarketTable.Rows.Add(dr1)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            'WriteLogMarketwatchlog("MarketWatch Search_Fields Error..", ex.ToString())
        End Try
    End Sub

    Private Function GetStatus(ByVal i As Integer, ByVal Sl As Double, ByVal ltp As Double) As String
        Dim Retval As String = ""
        If i = 1 Then
            If Sl > ltp Then
                Retval = "BUY SL. triggered."
            Else
                Retval = "BUY"
            End If
        ElseIf i = 2 Then
            If Sl < ltp Then
                Retval = "SELL SL. triggered."
            Else
                Retval = "SELL"
            End If
        End If
        Return Retval
    End Function

    Private Function Get_PreviousStrike(ByVal dStrike As Double, ByVal strExpDate As String, ByVal strCompany As String, ByVal lavel As Integer, ByVal cps As String) As Long
        ' dim DtTemp as New DataTable = New DataView(dtcpfmaster , "Symbol='" & cmbCompany.Text & "' AND  expdate1=" & DtFMonthDate.Rows(0)("expdate1").ToString & " AND option_type = 'CE'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
        Dim dv As DataView
        If lavel < 0 Then
            dv = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  expdate1='" & strExpDate & "' and Option_Type = '" & cps & "' and strike_price < " & dStrike & "", "strike_price Desc", DataViewRowState.CurrentRows)
        Else
            dv = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  expdate1='" & strExpDate & "' and Option_Type = '" & cps & "' and strike_price > " & dStrike & "", "strike_price", DataViewRowState.CurrentRows)
        End If
        Dim dtTemp As DataTable = dv.ToTable()
        Dim lngToken As Long

        If lavel = -1 Then
            If dtTemp.Rows.Count > 0 Then
                lngToken = dtTemp.Rows(0)("Token")
            Else
                lngToken = 0
            End If
        ElseIf lavel = -2 Then
            If dtTemp.Rows.Count > 1 Then
                lngToken = dtTemp.Rows(1)("Token")
            Else
                lngToken = 0
            End If

        ElseIf lavel = 1 Then
            If dtTemp.Rows.Count > 0 Then
                lngToken = dtTemp.Rows(0)("Token")
            Else
                lngToken = 0
            End If
        ElseIf lavel = 2 Then
            If dtTemp.Rows.Count > 1 Then
                lngToken = dtTemp.Rows(1)("Token")
            Else
                lngToken = 0
            End If
        End If

        Return lngToken
    End Function
    'Private Function Get_NextStrike(ByVal dStrike As Double, ByVal strExpDate As String, ByVal strCompany As String, ByVal lavel As Integer) As Double
    '    ' dim DtTemp as New DataTable = New DataView(dtcpfmaster , "Symbol='" & cmbCompany.Text & "' AND  expdate1=" & DtFMonthDate.Rows(0)("expdate1").ToString & " AND option_type = 'CE'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '    Dim dv As DataView
    '    If lavel < 0 Then
    '        dv = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  expdate1='" & strExpDate & "' and Option_Type = '" & "CE" & "' and strike_price < " & dStrike & "", "strike_price Desc", DataViewRowState.CurrentRows)
    '    Else
    '        dv = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  expdate1='" & strExpDate & "' and Option_Type = '" & "CE" & "' and strike_price > " & dStrike & "", "strike_price", DataViewRowState.CurrentRows)
    '    End If
    '    Dim dtTemp As DataTable = dv.ToTable()
    '    Dim lngToken As Double

    '    If lavel = -1 Then
    '        If dtTemp.Rows.Count > 0 Then
    '            lngToken = dtTemp.Rows(0)("strike_price")
    '        Else
    '            lngToken = 0
    '        End If
    '    ElseIf lavel = -2 Then
    '        If dtTemp.Rows.Count > 1 Then
    '            lngToken = dtTemp.Rows(1)("strike_price")
    '        Else
    '            lngToken = 0
    '        End If

    '    ElseIf lavel = 1 Then
    '        If dtTemp.Rows.Count > 0 Then
    '            lngToken = dtTemp.Rows(0)("strike_price")
    '        Else
    '            lngToken = 0
    '        End If
    '    ElseIf lavel = 2 Then
    '        If dtTemp.Rows.Count > 1 Then
    '            lngToken = dtTemp.Rows(1)("strike_price")
    '        Else
    '            lngToken = 0
    '        End If
    '    End If

    '    Return lngToken
    'End Function
    'Private Function Get_MidStrike(ByVal dltp As Double, ByVal strExpDate As String, ByVal strCompany As String, ByRef dGap As Double)
    '    ' dim DtTemp as New DataTable = New DataView(dtcpfmaster , "Symbol='" & cmbCompany.Text & "' AND  expdate1=" & DtFMonthDate.Rows(0)("expdate1").ToString & " AND option_type = 'CE'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '    Dim dv As DataView
    '    dv = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  expdate1='" & strExpDate & "' and Option_Type = 'CE'", "strike_price", DataViewRowState.CurrentRows)
    '    Dim dtTemp As DataTable = dv.ToTable()

    '    Dim dMidStrike As Double
    '    dGap = dtTemp.Rows(1)("strike_Price") - dtTemp.Rows(0)("strike_Price")
    '    For i As Integer = 0 To dtTemp.Rows.Count
    '        If (dltp < dtTemp.Rows(i)("Strike_Price")) Then
    '            If dGap < (dtTemp.Rows(i)("Strike_Price") - dltp) Then
    '                dMidStrike = dtTemp.Rows(i)("Strike_Price")
    '                Exit For
    '            Else
    '                'i = i - 1
    '                If (dGap / 2) > (dtTemp.Rows(i)("Strike_Price") - dltp) Then
    '                    dMidStrike = dtTemp.Rows(i)("Strike_Price")
    '                    Exit For
    '                Else
    '                    i = i - 1
    '                    dMidStrike = dtTemp.Rows(i)("Strike_Price")
    '                    Exit For
    '                End If
    '            End If
    '        End If
    '    Next
    '    Return dMidStrike
    'End Function
    Public Sub getrate()
        Try
            Dim Ojsql As SqlDbProcess = New SqlDbProcess
            Dim Query As String = ""
            Dim ED As New clsEnDe
            Dim dt As New DataTable

            If boolIsCurrency Then
                Query = "select ROUND(((((ROUND(((         F4-9527)*17)/100,4)/10000000)*100)/17) +9527),4) As Rate,f1 as Token  from tbl03006 With(Nolock) "

                dt = Ojsql.Getdatatable(Query)

                For Each drow As DataRow In dt.Rows
                    If Currfltpprice.Contains(CLng(drow("token"))) Then
                        Currfltpprice.Item(CLng(drow("token"))) = Math.Round(ED.DCur(Val(drow("Rate"))), 2)
                    Else
                        Currfltpprice.Add(CLng(drow("token")), Math.Round(ED.DCur(Val(drow("Rate"))), 2))
                    End If

                    If Currltpprice.Contains(CLng(drow("token"))) Then
                        Currltpprice.Item(CLng(drow("token"))) = Math.Round(ED.DCur(Val(drow("Rate"))), 2)
                    Else
                        Currltpprice.Add(CLng(drow("token")), Math.Round(ED.DCur(Val(drow("Rate"))), 2))
                    End If
                Next

            Else

                Query = "select f4 As Rate,f1 as Token  from tbl01004 With(Nolock) inner join Contract on tbl01004.f1=contract.token  Where contract.symbol = '" & strCompany + "'"

                dt = Ojsql.Getdatatable(Query)

                For Each drow As DataRow In dt.Rows
                    If fltpprice.Contains(CLng(drow("token"))) Then
                        fltpprice.Item(CLng(drow("token"))) = Math.Round(ED.DFo(Val(drow("Rate"))), 2)
                    Else
                        fltpprice.Add(CLng(drow("token")), Math.Round(ED.DFo(Val(drow("Rate"))), 2))
                    End If

                    If ltpprice.Contains(CLng(drow("token"))) Then
                        ltpprice.Item(CLng(drow("token"))) = Math.Round(ED.DFo(Val(drow("Rate"))), 2)
                    Else
                        ltpprice.Add(CLng(drow("token")), Math.Round(ED.DFo(Val(drow("Rate"))), 2))
                    End If
                Next


            End If


            Ojsql = Nothing
            Query = ""
            ED = Nothing
            dt = Nothing

        Catch ex As Exception
            'MsgBox(ex.ToString)
            'Return
        End Try

    End Sub

    Private Sub txtNoStrike_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoStrike.TextChanged
        iUpDownstrike = Val(txtNoStrike.Text)
    End Sub
End Class