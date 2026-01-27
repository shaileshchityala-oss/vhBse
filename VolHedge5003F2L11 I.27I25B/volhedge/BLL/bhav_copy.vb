Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL
Public Class bhav_copy
    Dim objtrd As New trading
#Region "SP"
    Private Const SP_bhavcopy_select As String = "select_bhavcopy"
    Private Const SP_bhavcopy_date_select As String = "select_bhavcopy_date"
    Private Const SP_bhavcopy_Insert As String = "insert_bhavcopy"
    Private Const SP_select_TblBHavCopy As String = "Select_TblBhavcopy"
    Private Const SP_select_Contract As String = "selected_contract"
    Private Const SP_select_Contract_tokenscript As String = "select_Contract_tokenscript"

    Private Const SP_delete_bhavcopy_Date As String = "delete_bhavcopy_Date"
    Private Const SP_bhavcopy_Delete As String = "Delete_bhavcopy"
    Private Const SP_BCastBhavcopy_Delete As String = "Delete_BCastBhavcopy"
    Private Const SP_BCastBhavcopy_Insert As String = "Insert_BCastBhavcopy"
    Private Const Sp_Select_SettelBhavCopy As String = "Select_SettelBhavCopy"

    Private Const SP_SettelMentBhav_Delete As String = "Delete_SettelMentBhav"
    Private Const SP_SettelMentBhav_Insert As String = "Insert_SettelMentBhav"

    Private Const Sp_Insert_CFPnL As String = "Sp_Insert_CFPnL"
    Private Const Sp_Select_CFPnL As String = "Select_CFPnL"
    Private Const Sp_Delete_CFPnL As String = "Sp_Delete_CFPnL"

#End Region
#Region "Method"

    Public Sub insertSettelBhav(ByVal dtable As DataTable, ByVal ddate As Date)
        data_access.ParamClear()
        data_access.Cmd_Text = SP_SettelMentBhav_Delete
        data_access.AddParam("@ExpiryDate", OleDbType.Date, 18, ddate)
        data_access.cmd_type = CommandType.StoredProcedure
        data_access.ExecuteNonQuery()


        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@company", OleDbType.VarChar, 50, CStr(drow("company")))
            data_access.AddParam("@Settel_PR", OleDbType.Double, 18, Val(drow("Settel_PR") & ""))
            data_access.AddParam("@ExpiryDate", OleDbType.Date, 18, CDate(drow("ExpiryDate")))
        Next
        data_access.Cmd_Text = SP_SettelMentBhav_Insert
        data_access.cmd_type = CommandType.StoredProcedure
        data_access.ExecuteMultiple(3)
    End Sub

    Public Sub insertBCastBC(ByVal dtable As DataTable, ByVal ddate As Date)

        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_BCastBhavcopy_Delete
            data_access.AddParam("@entry_date", OleDbType.Date, 18, ddate)
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.ExecuteNonQuery()
        Catch ex As Exception

        End Try


        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows

            data_access.AddParam("@script", OleDbType.VarChar, 50, CStr(drow("script")))
            data_access.AddParam("@INSTRUMENT", OleDbType.VarChar, 50, CStr(drow("INSTRUMENTname")))
            data_access.AddParam("@symbol", OleDbType.VarChar, 50, CStr(drow("symbol")))
            data_access.AddParam("@exp_date", OleDbType.Date, 18, CDate(drow("Expiry_Dt")))
            data_access.AddParam("@strike", OleDbType.Double, 18, Val(drow("strikePrice") & ""))
            data_access.AddParam("@option_type", OleDbType.VarChar, 5, CStr(drow("optiontype") & ""))
            data_access.AddParam("@CALavel", OleDbType.Double, 18, Val(drow("CALevel")))
            data_access.AddParam("@MarketType", OleDbType.Integer, 8, Val(drow("MarketType")))
            data_access.AddParam("@OpenPrice", OleDbType.Double, 18, Val(drow("OpenPrice")))
            data_access.AddParam("@HighPrice", OleDbType.Double, 18, Val(drow("HighPrice")))
            data_access.AddParam("@LowPrice", OleDbType.Double, 18, Val(drow("LowPrice")))
            data_access.AddParam("@ClosingPrice", OleDbType.Double, 18, Val(drow("ClosingPrice")))
            data_access.AddParam("@SettlePrice", OleDbType.Double, 18, Val(drow("SETTLE_PR")))
            data_access.AddParam("@ltp", OleDbType.Double, 18, Val(drow("SETTLE_PR")))
            data_access.AddParam("@contract", OleDbType.Double, 18, Val("0"))
            data_access.AddParam("@val_inlakh", OleDbType.Double, 18, Val(drow("TotalQuantityTraded")))
            data_access.AddParam("@vol", OleDbType.Double, 18, Val(drow("vol")))
            data_access.AddParam("@futval", OleDbType.Double, 18, Val(drow("futval")))
            data_access.AddParam("@mt", OleDbType.Double, 18, Val(drow("mt")))
            data_access.AddParam("@TotalQtyTrade", OleDbType.Double, 18, Val(drow("TotalQuantityTraded")))
            data_access.AddParam("@TotalValTrade", OleDbType.Double, 18, Val(drow("TotalValueTraded")))
            data_access.AddParam("@PreviousClose", OleDbType.Double, 18, Val(drow("PreviousClosePrice")))
            data_access.AddParam("@OI", OleDbType.Double, 18, Val(drow("OPENINTEREST")))
            data_access.AddParam("@CHG_IN_OI", OleDbType.Double, 18, Val(drow("CHGOPENINTEREST")))
            data_access.AddParam("@entry_date", OleDbType.Date, 18, CDate(drow("TIMESTAMP")))
        Next
        data_access.Cmd_Text = SP_BCastBhavcopy_Insert
        data_access.cmd_type = CommandType.StoredProcedure
        data_access.ExecuteMultiple(25)
    End Sub

    '''<summary>
    '''REM Create By Viral 6-08-11 
    '''</summary>
    Public Sub insertNew(ByVal dtable As DataTable)
        Try
            Dim ddate As Date
            ddate = CDate(dtable.Compute("Max(TIMESTAMP)", "").ToString.Replace("-", "/"))
            data_access.ParamClear()
            data_access.AddParam("@date1", OleDbType.Date, 50, (ddate))
            data_access.Cmd_Text = SP_delete_bhavcopy_Date
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.ExecuteNonQuery()
        Catch ex As Exception

        End Try


        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@script", OleDbType.VarChar, 50, CStr(drow("script")).Trim)
            data_access.AddParam("@INSTRUMENT", OleDbType.VarChar, 50, CStr(drow("INSTRUMENT")))
            data_access.AddParam("@symbol", OleDbType.VarChar, 50, CStr(drow("symbol")))

            data_access.AddParam("@exp_date", OleDbType.Date, 18, CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")))
            data_access.AddParam("@strike", OleDbType.Double, 18, Val(drow("STRIKE_PR")))
            data_access.AddParam("@option_type", OleDbType.VarChar, 20, CStr(drow("OPTION_TYP")))
            data_access.AddParam("@ltp", OleDbType.Double, 20, Val(drow("SETTLE_PR")))
            data_access.AddParam("@contract", OleDbType.Double, 18, Val(drow("CONTRACTS")))
            data_access.AddParam("@val_inlakh", OleDbType.Double, 18, Val(drow("VAL_INLAKH")))
            data_access.AddParam("@vol", OleDbType.Double, 18, Val(drow("vol")))
            data_access.AddParam("@entry_date", OleDbType.Date, 18, CDate(drow("TIMESTAMP").ToString.Replace("-", "/")))
            data_access.AddParam("@futval", OleDbType.Double, 18, Val(drow("futval")))
            data_access.AddParam("@mt", OleDbType.Double, 18, Val(drow("mt")))
        Next
        data_access.Cmd_Text = SP_bhavcopy_Insert
        data_access.cmd_type = CommandType.StoredProcedure
        data_access.ExecuteMultiple(13)
    End Sub
    Public Sub insert(ByVal dtable As DataTable)
        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@script", OleDbType.VarChar, 50, CStr(drow("script")).Trim)
            data_access.AddParam("@INSTRUMENT", OleDbType.VarChar, 50, CStr(drow("INSTRUMENT")))
            data_access.AddParam("@symbol", OleDbType.VarChar, 50, CStr(drow("symbol")))
            data_access.AddParam("@exp_date", OleDbType.Date, 18, CDate(drow("EXPIRY_DT")))
            data_access.AddParam("@strike", OleDbType.Double, 18, Val(drow("STRIKE_PR")))
            data_access.AddParam("@option_type", OleDbType.VarChar, 20, CStr(drow("OPTION_TYP")))
            data_access.AddParam("@ltp", OleDbType.Double, 20, Val(drow("SETTLE_PR")))
            data_access.AddParam("@contract", OleDbType.Double, 18, Val(drow("CONTRACTS")))
            data_access.AddParam("@val_inlakh", OleDbType.Double, 18, Val(drow("VAL_INLAKH")))
            data_access.AddParam("@vol", OleDbType.Double, 18, Val(drow("vol")))
            data_access.AddParam("@entry_date", OleDbType.Date, 18, CDate(drow("TIMESTAMP")))
            data_access.AddParam("@futval", OleDbType.Double, 18, Val(drow("futval")))
            data_access.AddParam("@mt", OleDbType.Double, 18, Val(drow("mt")))
        Next
        data_access.Cmd_Text = SP_bhavcopy_Insert
        data_access.cmd_type = CommandType.StoredProcedure
        data_access.ExecuteMultiple(13)
    End Sub
    Public Function select_data() As DataTable
        data_access.ParamClear()
        data_access.Cmd_Text = SP_bhavcopy_select
        data_access.cmd_type = CommandType.StoredProcedure
        Return data_access.FillList
    End Function
    Public Sub select_data(ByRef Dt As DataTable)
        data_access.ParamClear()
        data_access.Cmd_Text = SP_bhavcopy_select
        data_access.cmd_type = CommandType.StoredProcedure
        data_access.FillList(Dt)
    End Sub
    Public Function select_Date() As DataTable
        data_access.ParamClear()
        data_access.Cmd_Text = SP_bhavcopy_date_select
        data_access.cmd_type = CommandType.StoredProcedure
        Return data_access.FillList

    End Function
    'Public Sub select_TblBhavCopy(ByRef Dt As DataTable)
    '    Try
    '        data_access.ParamClear()
    '        data_access.Cmd_Text = SP_select_TblBHavCopy
    '        data_access.FillList(Dt)


    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    'End Sub
    ''' <summary>
    ''' Get Bhavcopy data as in Bhavcopy.csv From Greek.mdb Database
    ''' By Viral
    ''' </summary>
    ''' <returns>
    ''' Return Bhavcopy.csv Data Read  by ImportData.ImportOperation Class
    ''' </returns>
    ''' <remarks></remarks>
    Public Function select_TblBhavCopy() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_select_TblBHavCopy
            data_access.cmd_type = CommandType.StoredProcedure
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("bhavcopy :: select_TblBhavCopy() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function select_Contract() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_select_Contract_tokenscript
            data_access.cmd_type = CommandType.StoredProcedure
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Trading :: selected_contract ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub ProcessBCastBCopy(ByVal dt As DataTable)
        'Dim Mrateofinterast As Double
        'REM Change In processing Bhavcopy and selecting parameters, Decimal Strike Prices are properly displayed for the records 
        'Try

        '    Mrateofinterast = Val(IIf(IsDBNull(GdtSettings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")), 0, GdtSettings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))


        '    Dim dv As DataView
        '    Dim tempdata As DataTable
        '    tempdata = New DataTable

        '    tempdata.Columns.Add("script")
        '    tempdata.Columns.Add("vol")
        '    tempdata.Columns.Add("futval")
        '    tempdata.Columns.Add("mt")
        '    tempdata.Columns.Add("iscall")
        '    tempdata.Columns.Add("EXPIRY_DT")
        '    tempdata.Columns.Add("SETTLE_PR")
        '    tempdata.Columns.Add("TIMESTAMP")
        '    tempdata.AcceptChanges()


        '    tempdata.Merge(dt)




        '    Dim mt As Double
        '    Dim futval As Double
        '    Dim iscall As Boolean
        '    Dim drow As DataRow

        '    Dim strscript As String

        '    For Each drow In tempdata.Rows
        '        drow("EXPIRY_DT") = DateAdd(DateInterval.Second, Val(drow("ExpiryDate") & ""), CDate("1-1-1980"))
        '        If drow("optiontype") = "XX" Then
        '            If drow("INSTRUMENTNAME").ToString.ToUpper = "CM" Then
        '                strscript = drow("SYMBOL").ToString.Trim & "  " & drow("optiontype").ToString.Trim
        '            Else
        '                strscript = drow("INSTRUMENTNAME").ToString.Trim & "  " & drow("SYMBOL").ToString.Trim & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy")
        '            End If
        '        Else
        '            If drow("INSTRUMENTNAME").ToString.ToUpper = "CM" Then
        '                strscript = drow("SYMBOL").ToString.Trim & "  " & drow("optiontype").ToString.Trim
        '            Else
        '                strscript = drow("INSTRUMENTNAME").ToString.Trim & "  " & drow("SYMBOL").ToString.Trim & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice")), "###0.00") & "  " & drow("optiontype").ToString.Trim
        '            End If
        '        End If

        '        If drow("INSTRUMENTNAME").ToString.ToUpper = "CM" Then
        '            drow("SETTLE_PR") = drow("ClosingPrice")
        '        Else
        '            Dim AToken As Long
        '            Dim EqScript As Long
        '            AToken = cpfmaster.Compute("Max(Asset_tokan)", "script='" & strscript & "'")
        '            EqScript = eqmaster.Compute("Max(symbol)", "token=" & AToken & "")
        '            drow("SETTLE_PR") = tempdata.Compute("Max(ClosingPrice)", "symbol='" & EqScript & "' And INSTRUMENTNAME = 'CM'")
        '        End If

        '        drow("TIMESTAMP") = Date.Now.Date
        '    Next
        '    'objAdapter1.Fill(tempdata)
        '    'objConn.Close()

        '    'dv = New DataView(tempdata, "optiontype='XX'", "", DataViewRowState.OriginalRows)
        '    dv = New DataView(tempdata, "optiontype='XX'", "", DataViewRowState.CurrentRows)
        '    'dv = New DataView(Dt, "optiontype='XX'", "", DataViewRowState.OriginalRows)
        '    Dim str(2) As String
        '    str(0) = "EXPIRY_DT"
        '    str(1) = "SETTLE_PR"
        '    str(2) = "SYMBOL"
        '    Dim tdata As New DataTable
        '    tdata = dv.ToTable(True, str)
        '    Dim row As DataRow
        '    Dim script As String
        '    For Each drow In tempdata.Rows
        '        If drow("optiontype") = "XX" Then
        '            If drow("INSTRUMENTNAME").ToString.ToUpper = "CM" Then
        '                script = drow("SYMBOL").ToString.Trim & "  " & drow("optiontype").ToString.Trim
        '            Else
        '                script = drow("INSTRUMENTNAME").ToString.Trim & "  " & drow("SYMBOL").ToString.Trim & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy")
        '            End If

        '            drow("script") = UCase(script.Trim)
        '            drow("vol") = 0
        '            drow("futval") = 0
        '            drow("mt") = 0
        '        Else
        '            If drow("INSTRUMENTNAME").ToString.ToUpper = "CM" Then
        '                script = drow("SYMBOL").ToString.Trim & "  " & drow("optiontype").ToString.Trim
        '            Else
        '                script = drow("INSTRUMENTNAME").ToString.Trim & "  " & drow("SYMBOL").ToString.Trim & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice")), "###0.00") & "  " & drow("optiontype").ToString.Trim
        '            End If
        '            'script = drow("INSTRUMENTNAME") & "  " & drow("SYMBOL") & "  " & Format(drow("EXPIRY_DT"), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice")), "####.##") & "  " & drow("optiontype")
        '            drow("script") = UCase(script.Trim)
        '            futval = 0
        '            drow("vol") = 0
        '            For Each row In tdata.Select(" EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' ")
        '                futval = row("SETTLE_PR")
        '            Next
        '            'futval = Val(tempdata.Compute("Max(SETTLE_PR)", " EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' And optiontype='XX'").ToString() & "")
        '            'row("SETTLE_PR")

        '            If Mid(drow("optiontype"), 1, 1) = "C" Then
        '                iscall = True
        '            Else
        '                iscall = False
        '            End If
        '            Dim ccdate As Date = CDate(drow("TIMESTAMP").ToString.Replace("-", "/"))
        '            mt = UDDateDiff(DateInterval.Day, ccdate.Date, CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")).Date)
        '            If ccdate.Date = CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")).Date Then
        '                mt = 0.5
        '            End If
        '            If mt = 0 Then
        '                mt = 0.0001
        '            Else
        '                mt = (mt) / 365
        '            End If
        '            If futval > 0 Then
        '                'drow("vol") = Vol(futval, Val(drow("StrikePrice")), Val(drow("SETTLE_PR")), mt, iscall, True) * 100
        '                Dim tmpcpprice As Double = 0
        '                Dim mVolatility As Double
        '                tmpcpprice = Val(drow("SETTLE_PR"))
        '                'IF MATURITY IS 0 THEN _MT = 0.00001
        '                mVolatility = OptionG.Greeks.Black_Scholes(futval, Val(drow("StrikePrice")), Mrateofinterast, 0, tmpcpprice, mt, iscall, True, 0, 6)

        '                drow("vol") = mVolatility * 100 'Vol(futval, Val(drow("StrikePrice")), Val(drow("SETTLE_PR")), mt, iscall, True) * 100
        '            End If
        '            drow("futval") = futval
        '            drow("mt") = mt
        '            drow("iscall") = iscall
        '        End If
        '    Next
        '    tempdata.AcceptChanges()
        '    insertBCastBC(tempdata, Date.Now.ToString("dd-MMM-yyyy"))
        '    PnLCalcExpiry(tempdata)

        '    MsgBox("Bhavcopy Processed Successfully.", MsgBoxStyle.Information)

        'Catch ex As Exception
        '    MsgBox("Bhavcopy Not Processed.", MsgBoxStyle.Information)
        '    'MsgBox("Select valid file")
        '    '/MsgBox(ex.ToString)
        'End Try
    End Sub

    Public Function PnLExpirycalc(ByVal dExpiryDate As Date) As DataTable
        Dim calctable As New DataTable
        Dim FOTable As DataTable 'Fut & Cur 

        With calctable.Columns
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
            .Add("STT", GetType(Double))
        End With

        FOTable = objtrd.Select_EFOTrd(dExpiryDate)
        'GdtFOTrades.Copy 'New DataView(GdtFOTrades, "entrydate <= #" & dtp1.Value.Date & "# and entrydate >= #" & dtp2.Value.Date & "#", "entry_date,script", DataViewRowState.CurrentRows).ToTable
        'EQTable = GdtEQTrades.Copy 'New DataView(GdtEQTrades, "", "entry_date,script", DataViewRowState.CurrentRows).ToTable
        'CurTable = 'objtrd.Select_ECurTrd    'GdtCurrencyTrades.Copy

        calctable.Clear()
        Dim dtScript As New DataTable
        Dim trow As DataRow
        'Dim brate As Double

        REM ==========================FO===================================

        'for FO trades
        dtScript = FOTable.DefaultView.ToTable()
        For Each frow As DataRow In dtScript.Rows
            Dim VarOptionType As String = frow("cpf").ToString 'IIf(UCase(Mid(frow("script"), 1, 3)) = "FUT", "F", UCase(Mid(Strings.Right(frow("script"), 2), 1, 1))) 'mid(frow("script"), frow("script").Len, -2)))
            trow = calctable.NewRow
            dExpiryDate = CDate(frow("ExpiryDate"))
            trow("script") = frow("script")
            trow("company") = frow("company")
            trow("buyqty") = Val(frow("BuyQty") & "") 'CInt(Val(FOTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty>0 ").ToString))
            trow("buyvalue") = Val(frow("BuyVal") & "") 'CInt(Val(FOTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot > 0 ").ToString))

            If trow("buyqty") = 0 Then
                trow("buyrate") = Format(trow("buyvalue"), "#0.00")
            Else
                trow("buyrate") = Format(Val(trow("buyvalue") / trow("buyqty")), "#0.00")
            End If

            trow("sellqty") = Val(frow("SaleQty") & "") 'CInt(Val(FOTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty<0 ").ToString))
            trow("sellvalue") = Val(frow("SaleVal") & "") 'CInt(Val(FOTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot < 0 ").ToString))

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

            trow("option_type") = VarOptionType


            Dim VarLTPPrice As Double = 0
            Dim ValPrice As Double = 0
            Dim ValSpot As Double = 0
            ValSpot = frow("Settel_PR")

            If trow("option_type") = "F" Or trow("option_type") = "X" Then
                ValPrice = ValSpot
            ElseIf trow("option_type") = "C" Then
                ValPrice = Math.Max((ValSpot - Val(frow("StrikeRate") & "")), 0)
            ElseIf trow("option_type") = "P" Then
                ValPrice = Math.Max((Val(frow("StrikeRate") & "") - ValSpot), 0)
            End If

            trow("ltp") = ValPrice
            trow("STT") = 0
            If ValPrice > 0 Then
                If trow("netqty") > 0 Then
                    If frow("cpf").ToString.ToUpper = "C" Or frow("cpf").ToString.ToUpper = "P" Then
                        trow("STT") = (((frow("Strikerate") + ValPrice) * trow("netqty")) * sttRate) / 100
                    End If
                End If
            End If
            '-----------------------------------Expense calc
            'expense calculation
            Dim stexp, stexp1, exp As Double
            If frow("Typ").ToString.ToUpper = "FO" Then
                exp = 0
                stexp1 = 0
                stexp = 0
                exp = 0
                If frow("script").ToString.Substring(0, 3) = "FUT" Then
                    stexp = Val(trow("buyvalue")) 'Val(FOTable.Compute("sum(tot)", "cpf not in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
                    stexp1 = Math.Abs(Val(trow("sellvalue"))) 'Math.Abs(Val(FOTable.Compute("sum(tot)", "cpf not in ('C','P') and script='" & frow("script") & "' and qty < 0 ").ToString))
                    exp += ((stexp * futl) / futlp)
                    exp += ((stexp1 * futs) / futsp)

                    stexp1 = 0
                    stexp = 0
                    If Val(trow("netvalue")) > 0 Then
                        stexp1 = Val(trow("netvalue"))
                    Else
                        stexp = Math.Abs(Val(trow("netvalue")))
                    End If
                    exp += ((stexp * futl) / futlp)
                    exp += ((stexp1 * futs) / futsp)

                ElseIf frow("script").ToString.Substring(0, 3) = "OPT" Then
                    If spl <> 0 Then
                        'stexp = Val(FOTable.Compute("sum(tot)", "cpf in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
                        'stexp1 = Math.Abs(Val(FOTable.Compute("sum(tot)", "cpf  in ('C','P') and script='" & frow("script") & "' ").ToString))
                        'exp += ((stexp * spl) / splp)
                        'exp += ((stexp1 * sps) / spsp)

                        stexp = (Val(trow("buyrate")) + Val(frow("Strikerate"))) * Val(trow("buyqty")) 'Val(FOTable.Compute("sum(tot)", "cpf in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
                        stexp1 = Math.Abs((Val(trow("sellrate")) + Val(frow("Strikerate"))) * Val(trow("sellqty"))) 'Math.Abs(Val(FOTable.Compute("sum(tot)", "cpf  in ('C','P') and script='" & frow("script") & "' ").ToString))
                        exp += ((stexp * spl) / splp)
                        exp += ((stexp1 * sps) / spsp)

                        stexp1 = 0
                        stexp = 0
                        If (Val(trow("netrate")) + Val(frow("Strikerate"))) * Val(trow("netqty")) > 0 Then
                            stexp1 = (Val(trow("netrate")) + Val(frow("Strikerate"))) * Val(trow("netqty"))
                        Else
                            stexp = Math.Abs((Val(trow("netrate")) + Val(frow("Strikerate"))) * Val(trow("netqty")))
                        End If
                        exp += ((stexp * spl) / splp)
                        exp += ((stexp1 * sps) / spsp)

                    Else
                        'stexp = Val(FOTable.Compute("sum(tot)", "cpf in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
                        'stexp1 = Math.Abs(Val(FOTable.Compute("sum(tot)", "cpf  in ('C','P') and script='" & frow("script") & "' and qty < 0 ").ToString))
                        'exp += ((stexp * prel) / prelp)
                        'exp += ((stexp1 * pres) / presp)

                        stexp = Val(trow("buyvalue")) 'Val(FOTable.Compute("sum(tot)", "cpf in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
                        stexp1 = Math.Abs(Val(trow("sellvalue"))) 'Math.Abs(Val(FOTable.Compute("sum(tot)", "cpf  in ('C','P') and script='" & frow("script") & "' and qty < 0 ").ToString))
                        exp += ((stexp * prel) / prelp)
                        exp += ((stexp1 * pres) / presp)

                        stexp1 = 0
                        stexp = 0
                        If Val(trow("netvalue")) > 0 Then
                            stexp1 = Val(trow("netvalue"))
                        Else
                            stexp = Math.Abs(Val(trow("netvalue")))
                        End If
                        exp += ((stexp * prel) / prelp)
                        exp += ((stexp1 * pres) / presp)

                    End If
                End If
                trow("expense") = Format(exp, "#0.00")
            ElseIf frow("Typ").ToString.ToUpper = "CUR" Then

                exp = 0
                stexp1 = 0
                stexp = 0
                exp = 0
                If frow("script").ToString.Substring(0, 3) = "FUT" Then
                    stexp = Val(trow("buyvalue")) 'Val(FOTable.Compute("sum(tot)", "cpf not in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
                    stexp1 = Math.Abs(Val(trow("sellvalue"))) 'Math.Abs(Val(FOTable.Compute("sum(tot)", "cpf not in ('C','P') and script='" & frow("script") & "' and qty < 0 ").ToString))
                    exp += ((stexp * currfutl) / currfutlp)
                    exp += ((stexp1 * currfuts) / currfutsp)

                    stexp1 = 0
                    stexp = 0
                    If Val(trow("netvalue")) > 0 Then
                        stexp1 = Val(trow("netvalue"))
                    Else
                        stexp = Math.Abs(Val(trow("netvalue")))
                    End If
                    exp += ((stexp * currfutl) / currfutlp)
                    exp += ((stexp1 * currfuts) / currfutsp)

                ElseIf frow("script").ToString.Substring(0, 3) = "OPT" Then
                    If currspl <> 0 Then
                        'stexp = Val(FOTable.Compute("sum(tot)", "cpf in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
                        'stexp1 = Math.Abs(Val(FOTable.Compute("sum(tot)", "cpf  in ('C','P') and script='" & frow("script") & "' ").ToString))
                        'exp += ((stexp * spl) / splp)
                        'exp += ((stexp1 * sps) / spsp)

                        stexp = (Val(trow("buyrate")) + Val(frow("Strikerate"))) * Val(trow("buyqty")) 'Val(FOTable.Compute("sum(tot)", "cpf in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
                        stexp1 = Math.Abs((Val(trow("sellrate")) + Val(frow("Strikerate"))) * Val(trow("sellqty"))) 'Math.Abs(Val(FOTable.Compute("sum(tot)", "cpf  in ('C','P') and script='" & frow("script") & "' ").ToString))
                        exp += ((stexp * currspl) / currsplp)
                        exp += ((stexp1 * currsps) / currspsp)

                        stexp1 = 0
                        stexp = 0
                        If (Val(trow("netrate")) + Val(frow("Strikerate"))) * Val(trow("netqty")) > 0 Then
                            stexp1 = (Val(trow("netrate")) + Val(frow("Strikerate"))) * Val(trow("netqty"))
                        Else
                            stexp = Math.Abs((Val(trow("netrate")) + Val(frow("Strikerate"))) * Val(trow("netqty")))
                        End If
                        exp += ((stexp * currspl) / currsplp)
                        exp += ((stexp1 * currsps) / currspsp)

                    Else
                        'stexp = Val(FOTable.Compute("sum(tot)", "cpf in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
                        'stexp1 = Math.Abs(Val(FOTable.Compute("sum(tot)", "cpf  in ('C','P') and script='" & frow("script") & "' and qty < 0 ").ToString))
                        'exp += ((stexp * prel) / prelp)
                        'exp += ((stexp1 * pres) / presp)

                        stexp = Val(trow("buyvalue")) 'Val(FOTable.Compute("sum(tot)", "cpf in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
                        stexp1 = Math.Abs(Val(trow("sellvalue"))) 'Math.Abs(Val(FOTable.Compute("sum(tot)", "cpf  in ('C','P') and script='" & frow("script") & "' and qty < 0 ").ToString))
                        exp += ((stexp * currprel) / currprelp)
                        exp += ((stexp1 * currpres) / currpresp)

                        stexp1 = 0
                        stexp = 0
                        If Val(trow("netvalue")) > 0 Then
                            stexp1 = Val(trow("netvalue"))
                        Else
                            stexp = Math.Abs(Val(trow("netvalue")))
                        End If
                        exp += ((stexp * currprel) / currprelp)
                        exp += ((stexp1 * currpres) / currpresp)

                    End If
                End If
                trow("expense") = Format(exp, "#0.00")

            End If
            '----End Expense-----------------------------------------------------------------------------------
lblexp:
            'divyesh
            'trow("option_type") = frow("cp")

            If trow("netqty") = 0 Then
                trow("grossmtm") = Format(-trow("netvalue"), "#0.00") 'Format(trow("netrate"), "#0.00")
            Else
                'trow("grossmtm") = Format(Val(trow("ltp") - trow("netrate")) * trow("netqty"), "#0.00")
                trow("grossmtm") = Format(Val(trow("ltp") - trow("netrate")) * trow("netqty"), "#0.00")
            End If
            trow("netmtm") = Format(Val(trow("grossmtm") - (trow("expense") + trow("STT"))), "#0.00")

            calctable.Rows.Add(trow)
        Next

        '==================================================================================================





        REM' '' ''For Eq trades
        '' ''Dim arr(2) As String
        '' ''arr(0) = "script"
        '' ''arr(1) = "eq"
        '' ''arr(2) = "company"
        '' ''dtScript = EQTable.DefaultView.ToTable(True, arr)
        '' ''For Each frow As DataRow In dtScript.Rows
        '' ''    trow = calctable.NewRow
        '' ''    'trow("entry_date") = frow("entry_date")
        '' ''    trow("script") = frow("script")
        '' ''    trow("company") = frow("company")
        '' ''    trow("buyqty") = CInt(Val(EQTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty>0").ToString))
        '' ''    brate = Math.Abs(Val(EQTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot > 0 ").ToString))
        '' ''    If trow("buyqty") = 0 Then
        '' ''        trow("buyrate") = Format(brate, "#0.00")
        '' ''    Else
        '' ''        trow("buyrate") = Format(Val(brate / trow("buyqty")), "#0.00")
        '' ''    End If
        '' ''    trow("buyrate") = Format(Math.Round(trow("buyrate"), 2), "#0.00")
        '' ''    trow("buyvalue") = Format(Val(trow("buyqty") * trow("buyrate")), "#0.00")
        '' ''    trow("sellqty") = CInt(Val(EQTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty<0 ").ToString))
        '' ''    brate = Math.Abs(Val(EQTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot < 0 ").ToString))
        '' ''    If trow("sellqty") = 0 Then
        '' ''        trow("sellrate") = Format(brate, "#0.00")
        '' ''    Else
        '' ''        trow("sellrate") = Format(Val(Math.Abs(brate / trow("sellqty"))), "#0.00")
        '' ''    End If

        '' ''    trow("sellvalue") = Format(Val(trow("sellqty") * trow("sellrate")), "#0.00")

        '' ''    trow("netqty") = Val(trow("buyqty") + trow("sellqty"))
        '' ''    trow("netvalue") = Format(Val(trow("buyvalue") + trow("sellvalue")), "#0.00")

        '' ''    If trow("netqty") = 0 Then
        '' ''        trow("netrate") = Format(Val(trow("netvalue")), "#0.00")
        '' ''    Else
        '' ''        trow("netrate") = Format(Val(trow("netvalue") / trow("netqty")), "#0.00")
        '' ''    End If


        '' ''    'expense
        '' ''    Dim stexp, stexp1, exp, ndst, dst As Double
        '' ''    exp = 0
        '' ''    stexp = Math.Round(Val(EQTable.Compute("sum(tot)", "script='" & frow("script") & "' and qty > 0 ").ToString), 2)
        '' ''    stexp1 = Math.Abs(Math.Round(Val(EQTable.Compute("sum(tot)", "script='" & frow("script") & "' and qty < 0 ").ToString), 2))
        '' ''    dst = stexp - stexp1
        '' ''    If dst > 0 Then
        '' ''        ndst = stexp1
        '' ''        'If CDate(frow("entry_date")) = Now.Date() Then
        '' ''        '    exp += ((dst * ndbl) / ndblp)
        '' ''        'Else
        '' ''        exp += ((dst * dbl) / dblp)
        '' ''        ' End If
        '' ''        exp += ((stexp1 * ndbs) / ndbsp)
        '' ''        exp += ((stexp1 * ndbl) / ndblp)
        '' ''    Else
        '' ''        ndst = stexp
        '' ''        dst = -dst
        '' ''        exp += ((dst * dbs) / dbsp)
        '' ''        exp += ((stexp * ndbl) / ndblp)
        '' ''        exp += ((stexp * ndbs) / ndbsp)
        '' ''    End If
        '' ''    trow("expense") = Format(exp, "#0.00")

        '' ''    'divyesh
        '' ''    trow("option_type") = frow("eq")

        '' ''    Dim VarLTPPrice As Double = 0

        '' ''    Dim Edr() As DataRow = eqmaster.Select("script='" & trow("script") & "'")
        '' ''    Dim token_no As Long
        '' ''    If Edr.Length > 0 Then
        '' ''        token_no = Edr(0)("token")
        '' ''    End If

        '' ''    VarLTPPrice = Format(IIf(eltpprice.Contains(token_no) = True, eltpprice.Item(token_no), 0), "#0.00")
        '' ''    If VarLTPPrice = 0 And GVarIsNewBhavcopy = True Then
        '' ''        'Dim Drow() As DataRow = dtSettelBhav.Select("company='" & frow("company") & "'", "ExpiryDate")
        '' ''        'If Drow.Length > 0 Then
        '' ''        'VarLTPPrice = Format(Drow(0)("ltp"), "#0.00")
        '' ''        'End If
        '' ''    End If

        '' ''    trow("ltp") = VarLTPPrice

        '' ''    If trow("netqty") = 0 Then
        '' ''        trow("grossmtm") = Format(-trow("netvalue"), "#0.00")
        '' ''    Else
        '' ''        trow("grossmtm") = Format(Val(trow("ltp") - trow("netrate")) * trow("netqty"), "#0.00")
        '' ''    End If
        '' ''    trow("netmtm") = Format(Val(trow("grossmtm") - trow("expense")), "#0.00")
        '' ''    calctable.Rows.Add(trow)
        '' ''Next

        Dim gprofit As Double ' = Format(IIf(IsDBNull(calctable.Compute("sum(grossmtm)", "")) = False, calctable.Compute("sum(grossmtm)", ""), 0), "#0.00")
        Dim nprofit As Double '= Format(IIf(IsDBNull(calctable.Compute("sum(netmtm)", "")) = False, calctable.Compute("sum(netmtm)", ""), 0), "#0.00")
        Dim expense As Double '= Format(IIf(IsDBNull(calctable.Compute("sum(expense)", "")) = False, calctable.Compute("sum(expense)", ""), 0), "#0.00")



        Dim DV As DataView
        Try

            DV = New DataView(calctable.Copy, "", "", DataViewRowState.CurrentRows)
            For Each dr As DataRow In DV.ToTable(True, "company").Select

                gprofit = Format(IIf(IsDBNull(calctable.Compute("sum(grossmtm)", "company='" & dr("company") & "'")) = False, calctable.Compute("sum(grossmtm)", "company='" & dr("company") & "'"), 0), "#0.00")
                nprofit = Format(IIf(IsDBNull(calctable.Compute("sum(netmtm)", "company='" & dr("company") & "'")) = False, calctable.Compute("sum(netmtm)", "company='" & dr("company") & "'"), 0), "#0.00")
                expense = Format(IIf(IsDBNull(calctable.Compute("sum(expense)", "company='" & dr("company") & "'")) = False, calctable.Compute("sum(expense)", "company='" & dr("company") & "'"), 0), "#0.00")



                data_access.ParamClear()
                data_access.Cmd_Text = Sp_Delete_CFPnL
                data_access.cmd_type = CommandType.StoredProcedure
                data_access.AddParam("@company", OleDbType.VarChar, 18, dr("company").ToString)
                data_access.AddParam("@ExpiryDate", OleDbType.Date, 18, dExpiryDate)
                data_access.ExecuteNonQuery()


                data_access.ParamClear()
                data_access.Cmd_Text = Sp_Insert_CFPnL
                data_access.cmd_type = CommandType.StoredProcedure
                data_access.AddParam("@gprofit", OleDbType.Double, 8, gprofit)
                data_access.AddParam("@nprofit", OleDbType.Double, 8, nprofit)
                data_access.AddParam("@expense", OleDbType.Double, 8, expense)
                data_access.AddParam("@company", OleDbType.VarChar, 18, dr("company").ToString)
                data_access.AddParam("@ExpiryDate", OleDbType.Date, 8, dExpiryDate)
                data_access.ExecuteNonQuery()

            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return calctable
    End Function

    '    Public Shared Sub PnLCalcExpiry(ByVal dtBhavcopy As DataTable)
    '        Dim calctable As New DataTable
    '        Dim FOTable As DataTable
    '        Dim EQTable As DataTable
    '        With calctable.Columns
    '            .Add("script")
    '            .Add("buyqty", GetType(Integer))
    '            .Add("buyrate", GetType(Double))
    '            .Add("buyvalue", GetType(Double))
    '            .Add("sellqty", GetType(Integer))
    '            .Add("sellrate", GetType(Double))
    '            .Add("sellvalue", GetType(Double))
    '            .Add("netqty", GetType(Integer))
    '            .Add("netrate", GetType(Double))
    '            .Add("netvalue", GetType(Double))
    '            .Add("expense", GetType(Double))

    '            .Add("ltp", GetType(Double))
    '            .Add("grossmtm", GetType(Double))
    '            .Add("netmtm", GetType(Double))
    '            .Add("company")
    '            .Add("option_type")
    '        End With
    '        FOTable = GdtFOTrades 'New DataView(GdtFOTrades, "entrydate <= #" & dtp1.Value.Date & "# and entrydate >= #" & dtp2.Value.Date & "#", "entry_date,script", DataViewRowState.CurrentRows).ToTable
    '        EQTable = GdtEQTrades 'New DataView(GdtEQTrades, "", "entry_date,script", DataViewRowState.CurrentRows).ToTable
    '        ''divyesh
    '        ''bhavcopy = Objbhavcopy.select_data()

    '        calctable.Clear()
    '        Dim dtScript As New DataTable
    '        Dim trow As DataRow
    '        Dim brate As Double

    '        Dim arr(3) As String
    '        arr(0) = "script"
    '        arr(1) = "company"
    '        'arr(2) = "cp"
    '        arr(2) = "mdate"
    '        arr(3) = "StrikeRate"
    '        'for FO trades
    '        dtScript = FOTable.DefaultView.ToTable(True, arr)
    '        For Each frow As DataRow In dtScript.Rows
    '            Dim VarOptionType As String = IIf(UCase(Mid(frow("script"), 1, 3)) = "FUT", "F", UCase(Mid(Strings.Right(frow("script"), 2), 1, 1))) 'mid(frow("script"), frow("script").Len, -2)))
    '            trow = calctable.NewRow
    '            trow("script") = frow("script")
    '            trow("company") = frow("company")
    '            trow("buyqty") = CInt(Val(FOTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty>0 ").ToString))
    '            trow("buyvalue") = CInt(Val(FOTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot > 0 ").ToString))
    '            If trow("buyqty") = 0 Then
    '                trow("buyrate") = Format(trow("buyvalue"), "#0.00")
    '            Else
    '                trow("buyrate") = Format(Val(trow("buyvalue") / trow("buyqty")), "#0.00")
    '            End If

    '            trow("sellqty") = CInt(Val(FOTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty<0 ").ToString))
    '            trow("sellvalue") = CInt(Val(FOTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot < 0 ").ToString))
    '            If trow("sellqty") = 0 Then
    '                trow("sellrate") = Format(trow("sellvalue"), "#0.00")
    '            Else
    '                trow("sellrate") = Format(Val(trow("sellvalue") / trow("sellqty")), "#0.00")
    '            End If

    '            trow("netqty") = Val(trow("buyqty") + trow("sellqty"))
    '            trow("netvalue") = Val(trow("buyvalue") + trow("sellvalue"))

    '            If trow("netqty") = 0 Then
    '                trow("netrate") = Format(Val(trow("netvalue")), "#0.00")
    '            Else
    '                trow("netrate") = Format(Val(trow("netvalue") / trow("netqty")), "#0.00")
    '            End If


    '            'expense calculation
    '            Dim stexp, stexp1, exp As Double
    '            exp = 0
    '            If frow("script").ToString.Substring(0, 3) = "FUT" Then
    '                stexp = Val(FOTable.Compute("sum(tot)", "cp not in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
    '                stexp1 = Math.Abs(Val(FOTable.Compute("sum(tot)", "cp not in ('C','P') and script='" & frow("script") & "' and qty < 0 ").ToString))
    '                exp += ((stexp * futl) / futlp)
    '                exp += ((stexp1 * futs) / futsp)
    '            ElseIf frow("script").ToString.Substring(0, 3) = "OPT" Then
    '                'Option ####################################################################

    '                Dim Dr() As DataRow = dtBhavcopy.Select("script='" & frow("script") & "'")
    '                If Dr.Length > 0 Then
    '                    If frow("mdate") <= Dr(0)("entry_date") And Dr(0)("ltp") <> 0 Then
    '                        If Math.Max(Dr(0)("futval") - Dr(0)("strike"), 0) <> 0 Then
    '                            trow("expense") = Val(Dr(0)("strike") + (Dr(0)("futval") * (exptable.Rows(0).Item("sttrate")) / 100) * trow("netqty"))
    '                            GoTo lblexp
    '                        End If
    '                    End If
    '                End If

    '                If spl <> 0 Then
    '                    stexp = Val(FOTable.Compute("sum(tot)", "cp in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
    '                    stexp1 = Math.Abs(Val(FOTable.Compute("sum(tot)", "cp  in ('C','P') and script='" & frow("script") & "' ").ToString))
    '                    exp += ((stexp * spl) / splp)
    '                    exp += ((stexp1 * sps) / spsp)
    '                Else
    '                    stexp = Val(FOTable.Compute("sum(tot)", "cp  in ('C','P') and script='" & frow("script") & "' and qty > 0 ").ToString)
    '                    stexp1 = Math.Abs(Val(FOTable.Compute("sum(tot)", "cp  in ('C','P') and script='" & frow("script") & "' and qty < 0 ").ToString))
    '                    exp += ((stexp * prel) / prelp)
    '                    exp += ((stexp1 * pres) / presp)
    '                End If
    '            End If
    '            trow("expense") = Format(exp, "#0.00")
    'lblexp:
    '            'divyesh
    '            'trow("option_type") = frow("cp")
    '            trow("option_type") = VarOptionType

    '            Dim VarLTPPrice As Double = 0
    '            Dim cpfdr() As DataRow = cpfmaster.Select("script='" & trow("script") & "'")
    '            Dim token_no As Long
    '            If cpfdr.Length > 0 Then
    '                token_no = cpfdr(0)("token")
    '            End If

    '            If trow("option_type") = "F" Or trow("option_type") = "X" Then
    '                'LastBhavcopyDate = DTBhavCopy.Compute("MAX(entry_date)", "")
    '                VarLTPPrice = Format(Val(IIf(fltpprice.Contains(token_no) = True, fltpprice.Item(token_no), 0)), "#0.00")
    '                If VarLTPPrice = 0 Or GVarIsNewBhavcopy = True Then
    '                    If dtBhavcopy.Rows.Count > 0 Then
    '                        Dim Dr() As DataRow = dtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and symbol='" & frow("company") & "' and option_type='XX' and exp_date='" & frow("mdate") & "'")
    '                        If Dr.Length > 0 Then
    '                            VarLTPPrice = Format(Val(Dr(0)("ltp")), "#0.00")
    '                        End If
    '                    Else
    '                        VarLTPPrice = 0
    '                    End If
    '                End If
    '            ElseIf trow("option_type") = "C" Or trow("option_type") = "P" Then
    '                VarLTPPrice = Format(IIf(ltpprice.Contains(token_no) = True, ltpprice.Item(token_no), 0), "#0.00")
    '                If VarLTPPrice = 0 Or GVarIsNewBhavcopy = True Then
    '                    If dtBhavcopy.Rows.Count > 0 Then
    '                        Dim Dr() As DataRow = dtBhavcopy.Select("Script='" & trow("Script") & "'")
    '                        'Dim Dr() As DataRow = DTBhavCopy.Select("symbol='" & frow("company") & "' and option_type in (" & IIf(trow("option_type") = "C", "'CE','CA'", "'PE','PA'") & ") AND exp_date='" & frow("mdate") & "'")
    '                        If Dr.Length > 0 Then
    '                            If frow("mdate") = Dr(0)("entry_date") Then
    '                                Dim VarFLTP As Double = 0
    '                                Dim FDRow() As DataRow = dtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and symbol='" & frow("company") & "' and option_type='XX' and exp_date='" & frow("mdate") & "'")
    '                                VarFLTP = FDRow(0)("ltp")
    '                                If trow("option_type") = "C" Then
    '                                    VarLTPPrice = Math.Max(VarFLTP - frow("Strikerate"), 0)
    '                                ElseIf trow("option_type") = "P" Then
    '                                    VarLTPPrice = Math.Max(frow("Strikerate") - VarFLTP, 0)
    '                                End If
    '                            Else
    '                                VarLTPPrice = Format(Val(Dr(0)("ltp")), "#0.00")
    '                            End If
    '                        End If
    '                    Else
    '                        VarLTPPrice = 0
    '                    End If
    '                End If
    '            End If

    '            trow("ltp") = VarLTPPrice
    '            If trow("netqty") = 0 Then
    '                trow("grossmtm") = Format(-trow("netvalue"), "#0.00") 'Format(trow("netrate"), "#0.00")
    '            Else
    '                trow("grossmtm") = Format(Val(trow("ltp") - trow("netrate")) * trow("netqty"), "#0.00")
    '            End If
    '            trow("netmtm") = Format(Val(trow("grossmtm") - trow("expense")), "#0.00")
    '            calctable.Rows.Add(trow)
    '        Next

    '        'For Eq trades
    '        ReDim arr(2)
    '        arr(0) = "script"
    '        arr(1) = "eq"
    '        arr(2) = "company"
    '        dtScript = EQTable.DefaultView.ToTable(True, arr)
    '        For Each frow As DataRow In dtScript.Rows
    '            trow = calctable.NewRow
    '            'trow("entry_date") = frow("entry_date")
    '            trow("script") = frow("script")
    '            trow("company") = frow("company")
    '            trow("buyqty") = CInt(Val(EQTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty>0").ToString))
    '            brate = Math.Abs(Val(EQTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot > 0 ").ToString))
    '            If trow("buyqty") = 0 Then
    '                trow("buyrate") = Format(brate, "#0.00")
    '            Else
    '                trow("buyrate") = Format(Val(brate / trow("buyqty")), "#0.00")
    '            End If
    '            trow("buyrate") = Format(Math.Round(trow("buyrate"), 2), "#0.00")
    '            trow("buyvalue") = Format(Val(trow("buyqty") * trow("buyrate")), "#0.00")
    '            trow("sellqty") = CInt(Val(EQTable.Compute("sum(qty)", "script='" & frow("script") & "' and qty<0 ").ToString))
    '            brate = Math.Abs(Val(EQTable.Compute("sum(tot)", "script='" & frow("script") & "' and tot < 0 ").ToString))
    '            If trow("sellqty") = 0 Then
    '                trow("sellrate") = Format(brate, "#0.00")
    '            Else
    '                trow("sellrate") = Format(Val(Math.Abs(brate / trow("sellqty"))), "#0.00")
    '            End If

    '            trow("sellvalue") = Format(Val(trow("sellqty") * trow("sellrate")), "#0.00")

    '            trow("netqty") = Val(trow("buyqty") + trow("sellqty"))
    '            trow("netvalue") = Format(Val(trow("buyvalue") + trow("sellvalue")), "#0.00")

    '            If trow("netqty") = 0 Then
    '                trow("netrate") = Format(Val(trow("netvalue")), "#0.00")
    '            Else
    '                trow("netrate") = Format(Val(trow("netvalue") / trow("netqty")), "#0.00")
    '            End If


    '            'expense
    '            Dim stexp, stexp1, exp, ndst, dst As Double
    '            exp = 0
    '            stexp = Math.Round(Val(EQTable.Compute("sum(tot)", "script='" & frow("script") & "' and qty > 0 ").ToString), 2)
    '            stexp1 = Math.Abs(Math.Round(Val(EQTable.Compute("sum(tot)", "script='" & frow("script") & "' and qty < 0 ").ToString), 2))
    '            dst = stexp - stexp1
    '            If dst > 0 Then
    '                ndst = stexp1
    '                'If CDate(frow("entry_date")) = Now.Date() Then
    '                '    exp += ((dst * ndbl) / ndblp)
    '                'Else
    '                exp += ((dst * dbl) / dblp)
    '                ' End If
    '                exp += ((stexp1 * ndbs) / ndbsp)
    '                exp += ((stexp1 * ndbl) / ndblp)
    '            Else
    '                ndst = stexp
    '                dst = -dst
    '                exp += ((dst * dbs) / dbsp)
    '                exp += ((stexp * ndbl) / ndblp)
    '                exp += ((stexp * ndbs) / ndbsp)
    '            End If
    '            trow("expense") = Format(exp, "#0.00")

    '            'divyesh
    '            trow("option_type") = frow("eq")

    '            Dim VarLTPPrice As Double = 0

    '            Dim Edr() As DataRow = eqmaster.Select("script='" & trow("script") & "'")
    '            Dim token_no As Long
    '            If Edr.Length > 0 Then
    '                token_no = Edr(0)("token")
    '            End If

    '            VarLTPPrice = Format(IIf(eltpprice.Contains(token_no) = True, eltpprice.Item(token_no), 0), "#0.00")
    '            If VarLTPPrice = 0 And GVarIsNewBhavcopy = True Then
    '                Dim Dr() As DataRow = dtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and symbol='" & frow("company") & "' and option_type='XX'", "exp_date")
    '                If Dr.Length > 0 Then
    '                    VarLTPPrice = Format(Dr(0)("ltp"), "#0.00")
    '                End If
    '            End If

    '            trow("ltp") = VarLTPPrice

    '            If trow("netqty") = 0 Then
    '                trow("grossmtm") = Format(-trow("netvalue"), "#0.00")
    '            Else
    '                trow("grossmtm") = Format(Val(trow("ltp") - trow("netrate")) * trow("netqty"), "#0.00")
    '            End If
    '            trow("netmtm") = Format(Val(trow("grossmtm") - trow("expense")), "#0.00")
    '            calctable.Rows.Add(trow)
    '        Next

    '        Dim gprofit As Double ' = Format(IIf(IsDBNull(calctable.Compute("sum(grossmtm)", "")) = False, calctable.Compute("sum(grossmtm)", ""), 0), "#0.00")
    '        Dim nprofit As Double '= Format(IIf(IsDBNull(calctable.Compute("sum(netmtm)", "")) = False, calctable.Compute("sum(netmtm)", ""), 0), "#0.00")
    '        Dim expense As Double '= Format(IIf(IsDBNull(calctable.Compute("sum(expense)", "")) = False, calctable.Compute("sum(expense)", ""), 0), "#0.00")

    '        Dim DV As DataView
    '        Try

    '            DV = New DataView(calctable.Copy, "", "", DataViewRowState.CurrentRows)
    '            For Each dr As DataRow In DV.ToTable(True, "company").Select

    '                gprofit = Format(IIf(IsDBNull(calctable.Compute("sum(grossmtm)", "company='" & dr("company") & "'")) = False, calctable.Compute("sum(grossmtm)", "company='" & dr("company") & "'"), 0), "#0.00")
    '                nprofit = Format(IIf(IsDBNull(calctable.Compute("sum(netmtm)", "company='" & dr("company") & "'")) = False, calctable.Compute("sum(netmtm)", "company='" & dr("company") & "'"), 0), "#0.00")
    '                expense = Format(IIf(IsDBNull(calctable.Compute("sum(expense)", "company='" & dr("company") & "'")) = False, calctable.Compute("sum(expense)", "company='" & dr("company") & "'"), 0), "#0.00")


    '                data_access.ParamClear()
    '                data_access.Cmd_Text = Sp_Insert_CFPnL
    '                data_access.AddParam("@gprofit", OleDbType.Double, 8, gprofit)
    '                data_access.AddParam("@nprofit", OleDbType.Double, 8, nprofit)
    '                data_access.AddParam("@expense", OleDbType.Double, 8, expense)
    '                data_access.AddParam("@company", OleDbType.VarChar, 18, dr("company").ToString)
    '                data_access.ExecuteNonQuery()

    '            Next
    '        Catch ex As Exception
    '            MsgBox(ex.ToString)
    '        End Try
    '    End Sub

    Public Sub LoadCFProfit(ByVal sCompany As String)
        Dim tmpdt As New DataTable
        Try
            data_access.ParamClear()
            data_access.AddParam("@company", OleDbType.VarChar, 18, sCompany)
            data_access.Cmd_Text = Sp_Select_CFPnL

            data_access.cmd_type = CommandType.StoredProcedure
            tmpdt = data_access.FillList()

            CFgprofit = Format(IIf(IsDBNull(tmpdt.Compute("sum(gprofit)", "company='" & sCompany & "' And ExpiryDate < #" & fDate(Today.Date) & "#")) = False, tmpdt.Compute("Sum(gprofit)", "company='" & sCompany & "' And ExpiryDate < #" & fDate(Today.Date) & "#"), 0), "#0.00")
            CFnprofit = Format(IIf(IsDBNull(tmpdt.Compute("Sum(nprofit)", "company='" & sCompany & "' And ExpiryDate < #" & fDate(Today.Date) & "#")) = False, tmpdt.Compute("Sum(nprofit)", "company='" & sCompany & "' And ExpiryDate < #" & fDate(Today.Date) & "#"), 0), "#0.00")
            CFexpense = Format(IIf(IsDBNull(tmpdt.Compute("sum(expense)", "company='" & sCompany & "' And ExpiryDate < #" & fDate(Today.Date) & "#")) = False, tmpdt.Compute("Sum(expense)", "company='" & sCompany & "' And ExpiryDate < #" & fDate(Today.Date) & "#"), 0), "#0.00")

        Catch ex As Exception
            CFgprofit = 0
            CFnprofit = 0
            CFexpense = 0

            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Function Select_SettelBhavCopy(ByVal EntryDate As Date) As DataTable
        Try
            data_access.ParamClear()
            data_access.AddParam("@entry_date", OleDbType.VarChar, 18, Format(EntryDate, "dd-MMM-yyyy"))
            data_access.Cmd_Text = Sp_Select_SettelBhavCopy
            data_access.cmd_type = CommandType.StoredProcedure
            Return data_access.FillList()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region
End Class
