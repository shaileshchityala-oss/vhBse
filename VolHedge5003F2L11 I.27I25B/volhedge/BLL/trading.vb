Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL

Public Class trading

#Region "variable"
    Dim _uid As Integer
    Dim _SettingName As String
    Dim _Settingkey As String

#End Region
#Region "Property"
    Public Property Uid() As Integer
        Get
            Return _uid
        End Get
        Set(ByVal value As Integer)
            _uid = value
        End Set
    End Property
    Public Property SettingName() As String
        Get
            Return _SettingName
        End Get
        Set(ByVal value As String)
            _SettingName = value
        End Set
    End Property
    Public Property SettingKey() As String
        Get
            Return _Settingkey
        End Get
        Set(ByVal value As String)
            _Settingkey = value
        End Set
    End Property
#End Region
#Region "SP"

    Private Const SP_SELECT_COMPANY As String = "select_company"
    Private Const SP_SELECT_COMPANY_old As String = "select_company_old"
    Private Const SP_SELECT_COMPANY_SUMMARY As String = "select_company_summary"
    Private Const SP_SELECT_TRADING As String = "select_trading"
    Private Const SP_SELECT_TRADING_old As String = "select_trading_Old"

    'Private Const SP_Select_ECurTrd As String = "Select_ECurTrd"
    Private Const SP_Select_EFOTrd As String = "Select_EFOTrd"
    Private Const SP_SELECT_TRADING_LTP As String = "select_maturity"
    Private Const SP_INSERT_TODAY As String = "insert_today"
    Private Const SP_SELECT_SETTING As String = "select_settings"
    'Private Const SP_SELECT_SCRIPT As String = "generate_script"
    Private Const SP_SELECT_SCRIPT As String = "generate_script"
    Private Const SP_SELECT_SECURITY As String = "selected_security"
    Private Const SP_SELECT_EQUITYSCRIPT As String = "selected_security"

    Private Const SP_Update_Import_Setting As String = "UpdateImportSetting"
    Private Const SP_SELECT_UPPERTOKENS As String = "Select_UpperToken"
    Private Const SP_SELECT_UPPERTOKENS_Curr As String = "Select_UpperToken_Curr"

    'Private Const SP_SELECT_DOWNTOKENS As String = "Select_DownToken"
    'Private Const SP_SELECT_DOWNTOKENS_Curr As String = "Select_DownToken_Curr"

    Private Const SP_Script_Insert As String = "script_insert" ''For Call put & fut File Process 

    Private Const SP_INSERT_EQUITY As String = "insert_equity" '' for Equity file process
    Private Const SP_SELECT_EQUITY As String = "SELECT_equity"

    Private Const SP_SELECTED_Currency_Contract As String = "SELECTED_Currency_Contract"
    Private Const SP_INSERT_Currency_Trading As String = "INSERT_Currency_Trading" '' for Equity file process
    Private Const SP_SELECT_Currency_Trading As String = "SELECT_Currency_Trading"

    Private Const SP_SELECT_PREBAL As String = "select_prebal"
    Private Const SP_INSERT_PREBAL As String = "insert_prebal"
    Private Const SP_UPDATE_PREBAL As String = "update_prebal"

    Private Const SP_SELECT_EXPOSURE_MARGIN As String = "select_exposure_margin"
    Private Const SP_insert_exposure_margin As String = "insert_exposure_margin"
    Private Const SP_update_exposure_margin As String = "update_exposure_margin"
    Private Const SP_delete_exposure_margin As String = "delete_exposure_margin"
    Private Const SP_delete_exposure_margin_select As String = "delete_exposure_margin_select"

    Private Const SP_SELECT_EXPOSURE_MARGIN_NEW As String = "select_exposure_margin_new"
    Private Const SP_insert_exposure_margin_New As String = "insert_exposure_margin_new"
    Private Const SP_update_exposure_margin_New As String = "update_exposure_margin_new"
    Private Const SP_delete_exposure_margin_New As String = "delete_exposure_margin_new"
    Private Const SP_delete_exposure_margin_New_select As String = "delete_exposure_margin_new_select"

    Private Const SP_select_ael_contracts As String = "select_ael_contracts"
    Private Const SP_insert_ael_contracts As String = "insert_ael_contracts"
    Private Const SP_update_ael_contracts As String = "update_ael_contracts"
    Private Const SP_delete_ael_contracts As String = "delete_ael_contracts"

    Private Const SP_Delete_CFBalance As String = "Delete_CFBalance"
    Private Const SP_Insert_CFBalance As String = "Insert_CFBalance"
    Private Const SP_Select_CFBalance As String = "Select_CFBalance"
    Private Const SP_Update_CFBalance As String = "Update_CFBalance"

    Private Const SP_Pr_balace_date As String = "delete_prbal_date"
    Private Const SP_Pr_balace_name As String = "delete_prbal_name"

    Private Const sp_insert_setting As String = "insert_setting"
    Private Const sp_update_setting As String = "update_setting"
    Private Const sp_update_setting_TCP As String = "Update_Setting_TCP"


    Private Const sp_update_liq As String = "update_trading_isliq"


    Private Const SP_insert_company_analysis As String = "insert_company_analysis"
    Private Const SP_update_company_analysis As String = "update_company_analysis"
    Private Const SP_select_company_analysis As String = "select_company_analysis"
    Private Const sp_delete_company_analysis As String = "delete_company_analysis"
    Private Const sp_delete_company_analysis_company As String = "delete_company_analysis_company"

    Private Const SP_select_All_trade As String = "select_all_trade"


    Private Const SP_Delete_TblRefreshSymbol As String = "Delete_TblRefreshSymbol"
    Private Const SP_Insert_TblRefreshSymbol As String = "Insert_TblRefreshSymbol"
    Private Const sp_Select_TblRefreshSymbol As String = "Select_TblRefreshSymbol"
    Private Const sp_Select_TblRefreshSymbol_all As String = "select_chklistbox_symbol"

    Private Const SP_select_TCP_Connection As String = "select_TCP_Connection"
    Private Const SP_Delete_tblTcpConnectuon_data As String = "Delete_tblTcpConnectuon_data"
    Private Const SP_Insert_tblTcpConnectuon_data As String = "Insert_tblTcpConnectuon_data"

    Private Const SP_SELECT_CURRENCY_EXPOSURE_MARGIN As String = "select_Currency_exposure_margin"
    Private Const SP_insert_CURRENCY_EXPOSURE_MARGIN As String = "insert_Currency_exposure_margin"
    Private Const SP_update_CURRENCY_EXPOSURE_MARGIN As String = "update_Currency_exposure_margin"
    Private Const SP_delete_CURRENCY_EXPOSURE_MARGIN As String = "delete_Currency_exposure_margin"
    Private Const SP_delete_CURRENCY_EXPOSURE_MARGIN_Select As String = "delete_Currency_exposure_margin_select"

    Private Const SP_Update_equity_trading_Strategy_Name As String = "Update_equity_trading_Strategy_Name"
    Private Const SP_Update_trading_Strategy_Name As String = "Update_trading_Strategy_Name"

    Private Const SP_Update_equity_trading_Strategy_Name_Null As String = "Update_equity_trading_Strategy_Name_Null"
    Private Const SP_Update_trading_Strategy_Name_Null As String = "Update_trading_Strategy_Name_Null"

    Private Const SP_Select_All_Strategy_Companywise As String = "Select_All_Strategy_Companywise"
    Private Const SP_Select_All_Trade_New As String = "Select_All_Trade_New"

    Private Const SP_Select_Import_Setting As String = "Select_Import_Setting"
    Private Const SP_Insert_Import_Setting As String = "Insert_Import_Setting"
    Private Const SP_Delete_Import_Setting As String = "Delete_Import_Setting"

    Private Const SP_Select_Import_Type As String = "Select_Import_Type"

    Private Const SP_Select_Expense_Data As String = "Select_Expense_Data"
    Private Const SP_Insert_Expense_Data As String = "Insert_Expense_Data"
    Private Const SP_Delete_Expense_Data_All As String = "Delete_Expense_Data_All"

    Private Const SP_Select_Patch_Expense As String = "Select_Patch_Expense"
    Private Const SP_Update_Patch_Expense As String = "Update_Patch_Expense"

    Public Const SP_Select_DataGrid_Column_Setting As String = "Select_DataGrid_Column_Setting"
    Private Const SP_Insert_DataGrid_Column_Setting As String = "Insert_DataGrid_Column_Setting"
    Private Const SP_Update_DataGrid_Column_Setting As String = "Update_DataGrid_Column_Setting"
    Private Const Sp_Insert_RangeData As String = "Insert_tblRange_Data"
    Private Const Sp_Update_RangeData As String = "Update_tblRange_Data"
    Public Const SP_Select_DataGrid_Column_Profile As String = "Select_DataGrid_Column_Profile"
    Public Const SP_Delete_DataGrid_Column_Profile As String = "Delete_DataGrid_Column_Profile"


    Private Const SP_Select_DataGrid_summaryColumn_Setting As String = "Select_DataGrid_SummaryColumn_Setting"
    Private Const SP_Insert_DataGrid_summaryColumn_Setting As String = "Insert_DataGrid_SummaryColumn_Setting"
    Private Const SP_Update_DataGrid_summaryColumn_Setting As String = "Update_DataGrid_SummaryColumn_Setting"

    Private Const SP_SELECT_EQHTABLE As String = "QEQHtable"
    Private Const SP_SELECT_FOHTABlE As String = "QFOHtable"
    Private Const SP_SELECT_CURHTABLE As String = "QCurHTable"

    'Private Const SP_Update_User_Master As String = "Update_User_Master"
    Private Const SP_delete_trading_script As String = "delete_trading_script"
    Private Const SP_delete_Currency_trading_Script As String = "delete_Currency_trading_Script"
    Private Const SP_delete_security_script As String = "delete_security_script"
    Private Const SP_delete_prbalance_company As String = "delete_prBalance_company"
    Private Const SP_delete_patch_expense_company As String = "delete_patch_Expense_company"
    Private Const SP_delete_CFPnL_company As String = "SP_delete_CFPnL_company"
#End Region
#Region "Property"


#End Region
#Region "Method"
    Public Function Comapany() As DataTable
        Try
            data_access.ParamClear()


            If gstr_ProductName = "OMI" Then

                data_access.Cmd_Text = SP_SELECT_COMPANY_old
            Else
                data_access.Cmd_Text = SP_SELECT_COMPANY
            End If

            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub select_company_expiry(ByVal sSql As String)
        Dim StrSql As String
        Try
            data_access.cmd_type = CommandType.Text
            'StrSql = "INSERT INTO trading (instrumentname, company, mdate, strikerate, cp, script, qty, rate, entrydate, entryno, token1, isliq, orderno, lActivityTime, FileFlag )" & _
            '         " SELECT instrumentname,company,mdate,strikerate,cp,script,Qty,Rate,entrydate,entryno,Token1,IsLiq,orderno,lActivityTime,FileFlag  " & _
            '         " FROM (" & sSql & ") as tlb; "

            StrSql = "INSERT INTO trading (instrumentname, company, mdate, strikerate, cp, script, qty, rate, entrydate, entryno, token1, isliq, orderno, lActivityTime, FileFlag,Dealer, token,mo, tot, entry_date, tot2, cpf, BuyQty, SaleQty, BuyVal, SaleVal )" & _
                     " SELECT instrumentname,company,mdate,strikerate,cp,script,Qty,Rate,entrydate,entryno,Token1,IsLiq,orderno,lActivityTime,FileFlag,OrgDealer, token,mo, tot, entry_date, tot2, cpf, BuyQty, SaleQty, BuyVal, SaleVal " & _
                    " FROM (" & sSql & ") as tlb; "
            data_access.ParamClear()
            data_access.ExecuteNonQuery(StrSql, CommandType.Text)
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox("Error in inserting fo. trade." & vbCrLf & ex.Message)
        End Try
    End Sub
    Public Function Select_DataGrid_Column_Setting() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Select_DataGrid_Column_Setting
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Expense :: Select_Client_Expenses ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub Insert_DataGrid_SummaryColumn_Setting(ByVal dtable As DataTable)
        data_access.cmd_type = CommandType.StoredProcedure
        data_access.ParamClear()

        For Each Dr As DataRow In dtable.Rows
            data_access.AddParam("VarFormName", OleDbType.VarChar, 255, Dr("FormName").ToString)
            data_access.AddParam("VarColumnName", OleDbType.VarChar, 255, Dr("ColumnName").ToString)
            data_access.AddParam("VarDisplayIndex", OleDbType.Integer, 18, Val(Dr("DisplayIndex") & ""))
            data_access.AddParam("VarDisplayText", OleDbType.VarChar, 255, Dr("DisplayText").ToString)
            data_access.AddParam("VarWidth", OleDbType.Integer, 18, Val(Dr("Width") & ""))
            data_access.AddParam("VarVisible", OleDbType.Boolean, 18, CBool(Dr("IsVisible")))
            'data_access.Cmd_Text = SP_Insert_DataGrid_Column_Setting
            'data_access.ExecuteNonQuery()
        Next
        data_access.Cmd_Text = SP_Insert_DataGrid_summaryColumn_Setting
        data_access.ExecuteMultiple(6)
    End Sub
    Public Sub Update_DataGrid_summaryColumn_Setting_OnWidthIndex(ByVal DtColProfile As DataTable)
        data_access.cmd_type = CommandType.StoredProcedure
        data_access.ParamClear()
        For Each Dr As DataRow In DtColProfile.Rows
            data_access.AddParam("@VarDisplayIndex", OleDbType.Integer, 18, Dr("DisplayIndex"))
            data_access.AddParam("@VarDisplayText", OleDbType.VarChar, 255, Dr("DisplayText"))
            data_access.AddParam("@VarWidth", OleDbType.Integer, 18, Dr("Width"))
            data_access.AddParam("@IsVisible", OleDbType.Boolean, 18, CBool(Dr("IsVisible")))
            data_access.AddParam("@VarFormName", OleDbType.VarChar, 255, Dr("FormName"))
            data_access.AddParam("@VarColumnName", OleDbType.VarChar, 255, Dr("ColumnName"))

        Next
        data_access.Cmd_Text = SP_Update_DataGrid_summaryColumn_Setting
        data_access.ExecuteMultiple(6)
    End Sub
    Public Function GFun_SetGridSummaryColumnSetting(ByVal DGrid As DataGridView) As DataTable
        '===== REM Change BY Payal Patel For Save Grid index to datatable==========
        Dim DT As New DataTable
        'Dim I As Integer
lbl:
        data_access.ParamClear()
        data_access.Cmd_Text = SP_Select_DataGrid_summaryColumn_Setting
        DT = data_access.FillList()
        If DT.Rows.Count = 0 Then
            Dim DtG As New DataTable
            DtG.Columns.Add("FormName")
            DtG.Columns.Add("ColumnName")
            DtG.Columns.Add("DisplayIndex")
            DtG.Columns.Add("DisplayText")
            DtG.Columns.Add("Width")
            DtG.Columns.Add("IsVisible")
            For cnt As Integer = 0 To DGrid.ColumnCount - 1
                'If DGrid.Columns(cnt).Visible = True Then
                DtG.Rows.Add("allcompany", DGrid.Columns(cnt).Name, DGrid.Columns(cnt).DisplayIndex, DGrid.Columns(cnt).HeaderText, DGrid.Columns(cnt).Width, DGrid.Columns(cnt).Visible)
                'I = I + 1
                'End If
            Next
            Insert_DataGrid_SummaryColumn_Setting(DtG)
            GoTo lbl
        End If
        'gtdChild.VisibleColumns.Clear()

        'For Each Dr As DataRow In DT.Rows
        '    DGrid.Columns(Dr("ColumnName").ToString).HeaderText = Dr("DisplayText").ToString
        '    DGrid.Columns(Dr("ColumnName").ToString).Width = Dr("width")
        'Next
        Return DT
        '===== REM:End==========
    End Function
    'Public Function Comapany_Net() As DataSet
    '    Dim ds As New DataSet
    '    Try

    '        Dim dtcompany As New DataTable
    '        dtcompany = comptable.Copy
    '        If dtcompany.Rows.Count <= 60 Then
    '            Dim dtResult As New DataTable
    '            dtResult.Columns.Add(New DataColumn("company", GetType(String)))
    '            For Each drow As DataRow In dtcompany.Rows
    '                drow("company") = GetSymbol(drow("company").ToString)
    '            Next
    '            dtcompany.AcceptChanges()
    '            Dim dv As DataView
    '            dv = New DataView(dtcompany)
    '            For Each drow As DataRow In dv.ToTable(True, "company").Rows
    '                dtResult.Rows.Add(drow("company"))
    '            Next
    '            dtResult.AcceptChanges()
    '            ds.Tables.Add(dtResult)
    '        Else
    '            Dim a As Integer = Math.Ceiling(dtcompany.Rows.Count / 60) - 1
    '            Dim dtResult(a) As DataTable

    '            For Each drow As DataRow In dtcompany.Rows
    '                drow("company") = GetSymbol(drow("company").ToString)
    '            Next
    '            dtcompany.AcceptChanges()

    '            For i As Integer = 0 To a
    '                dtResult(i) = New DataTable
    '                dtResult(i).Columns.Add(New DataColumn("company", GetType(String)))
    '            Next
    '            Dim j As Integer = 0

    '            Dim dv As DataView
    '            dv = New DataView(dtcompany)
    '            For Each drow As DataRow In dv.ToTable(True, "company").Rows
    '                j = j + 1
    '                dtResult(Math.Ceiling(j / 60) - 1).Rows.Add(drow("company"))
    '                dtResult(Math.Ceiling(j / 60) - 1).AcceptChanges()
    '            Next

    '            For i As Integer = 0 To a
    '                ds.Tables.Add(dtResult(i))
    '            Next

    '        End If
    '        Return ds
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        ds.Tables.Add(comptable.Copy)
    '        Return ds
    '    End Try
    'End Function
    Public Function Comapany_Net() As DataSet
        Dim ds As New DataSet
        Try

            Dim dtcompany As New DataTable
            dtcompany = comptable.Copy
            If dtcompany.Rows.Count <= 60 Then
                Dim dtResult As New DataTable
                dtResult.Columns.Add(New DataColumn("company", GetType(String)))
                For Each drow As DataRow In dtcompany.Rows
                    'If IsDBNull(Currencymaster.Compute("max(Symbol)", "symbol='" & GetSymbol(drow("company").ToString) & "'")) Then
                    drow("company") = GetSymbol(drow("company").ToString)
                    'End If

                Next
                dtcompany.AcceptChanges()
                Dim dv As DataView
                dv = New DataView(dtcompany)
                For Each drow As DataRow In dv.ToTable(True, "company").Rows
                    'If IsDBNull(Currencymaster.Compute("max(Symbol)", "symbol='" & GetSymbol(drow("company").ToString) & "'")) Then
                    dtResult.Rows.Add(drow("company"))
                    'End If
                Next
                dtResult.AcceptChanges()
                ds.Tables.Add(dtResult)
            Else
                Dim a As Integer = Math.Ceiling(dtcompany.Rows.Count / 60) - 1
                Dim dtResult(a) As DataTable

                For Each drow As DataRow In dtcompany.Rows
                    'If IsDBNull(Currencymaster.Compute("max(Symbol)", "symbol='" & GetSymbol(drow("company").ToString) & "'")) Then
                    drow("company") = GetSymbol(drow("company").ToString)
                    'End If
                Next
                dtcompany.AcceptChanges()

                For i As Integer = 0 To a
                    dtResult(i) = New DataTable
                    dtResult(i).Columns.Add(New DataColumn("company", GetType(String)))
                Next
                Dim j As Integer = 0

                Dim dv As DataView
                dv = New DataView(dtcompany)
                For Each drow As DataRow In dv.ToTable(True, "company").Rows
                    'If IsDBNull(Currencymaster.Compute("max(Symbol)", "symbol='" & GetSymbol(drow("company").ToString) & "'")) Then
                    j = j + 1
                    dtResult(Math.Ceiling(j / 60) - 1).Rows.Add(drow("company"))
                    dtResult(Math.Ceiling(j / 60) - 1).AcceptChanges()
                    'End If
                Next

                For i As Integer = 0 To a
                    ds.Tables.Add(dtResult(i))
                Next

            End If
            Return ds
        Catch ex As Exception
            MsgBox(ex.ToString)
            ds.Tables.Add(comptable.Copy)
            Return ds
        End Try
    End Function

    Public Function Comapany_summary() As DataTable
        Try

            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_COMPANY_SUMMARY
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub Update_setting(ByVal SettingKey As String, ByVal SettingName As String, ByVal Uid As Integer)
        data_access.cmd_type = CommandType.StoredProcedure
        data_access.ParamClear()
        data_access.AddParam("@SettingKey", OleDbType.VarChar, 3000, SettingKey)
        data_access.AddParam("@SettingName", OleDbType.VarChar, 250, SettingName)
        data_access.AddParam("@uid", OleDbType.Integer, 18, Uid)
        data_access.Cmd_Text = sp_update_setting
        data_access.ExecuteNonQuery()
    End Sub
    Public Function Selectmax_tradingtime() As DataTable
        Dim dt As New DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = "select max(entrydate1) as entrydate  from(select MAX(entrydate) as entrydate1 from trading" & _
            " UNION " & _
            "select MAX(entrydate) as entrydate1 from equity_trading" & _
" union " & _
"select MAX(entrydate) as entrydate1 from Currency_trading)"

            data_access.cmd_type = CommandType.Text
            dt = data_access.FillList()
            'data_access.ExecuteMultiple(16)
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return dt
    End Function
    Public Function SelectFO_tradingtime() As DataTable
        Dim dt As New DataTable
        Try
            data_access.ParamClear()
            '            data_access.Cmd_Text = "select max(entrydate1) as entrydate  from(select MAX(entrydate) as entrydate1 from trading" & _
            '            " UNION " & _
            '            "select MAX(entrydate) as entrydate1 from equity_trading" & _
            '" union " & _
            '"select MAX(entrydate) as entrydate1 from Currency_trading)"
            'data_access.Cmd_Text = "select MAX(entrydate) as entrydate1 from trading"
            'data_access.cmd_type = CommandType.Text
            'dt = data_access.FillList()
            dt = data_access.ExecuteNonQuery("select MAX(entrydate) as entrydate1 from trading", CommandType.Text)
            'data_access.ExecuteMultiple(16)
            'data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return dt
    End Function
    Public Function SelectEQ_tradingtime() As DataTable
        Dim dt As New DataTable
        Try
            data_access.ParamClear()
            'data_access.Cmd_Text = "select MAX(entrydate) as entrydate1 from equity_trading"
            'data_access.cmd_type = CommandType.Text
            'dt = data_access.FillList()
            dt = data_access.ExecuteNonQuery("select MAX(entrydate) as entrydate1 from equity_trading", CommandType.Text)
            'data_access.ExecuteMultiple(16)
            'data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return dt
    End Function
    Public Function SelectCURR_tradingtime() As DataTable
        Dim dt As New DataTable
        Try
            data_access.ParamClear()
            'data_access.Cmd_Text = "select MAX(entrydate) as entrydate1 from Currency_trading"
            'data_access.cmd_type = CommandType.Text
            'dt = data_access.FillList()
            dt = data_access.ExecuteNonQuery("select MAX(entrydate) as entrydate1 from Currency_trading", CommandType.Text)
            'data_access.ExecuteMultiple(16)
            ' data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return dt
    End Function

    Public Sub select_EQHTable(ByRef dt As DataTable, ByRef company As String)
        Try
            data_access.ParamClear()
            data_access.AddParam("@Company", OleDbType.VarChar, 100, company)
            data_access.Cmd_Text = SP_SELECT_EQHTABLE
            data_access.FillList(dt)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub select_FOHTable(ByRef dt As DataTable, ByRef company As String)
        Try
            data_access.ParamClear()
            data_access.AddParam("@Company", OleDbType.VarChar, 100, company)
            data_access.Cmd_Text = SP_SELECT_FOHTABlE
            data_access.FillList(dt)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub select_CURHTable(ByRef dt As DataTable)
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_CURHTABLE
            data_access.FillList(dt)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    'REM:Change by Payal Patel For Update Import Setting on date-17-06-2014
    Public Sub Update_Import_Setting(ByVal VarAuto_Import As Boolean, ByVal VarManual_Import As Boolean, ByVal id As Integer)
        data_access.ParamClear()

        data_access.AddParam("@Auto_Import", OleDbType.Boolean, 1, VarAuto_Import)
        data_access.AddParam("@Manual_Import", OleDbType.Boolean, 1, VarManual_Import)
        data_access.AddParam("@id", OleDbType.Integer, 1, id)
        data_access.Cmd_Text = SP_Update_Import_Setting
        data_access.ExecuteNonQuery()
    End Sub
    'nouse comment by payalpatel
    'Public Function Select_ECurTrd() As DataTable
    '    Try
    '        data_access.ParamClear()
    '        data_access.Cmd_Text = SP_Select_ECurTrd
    '        Return data_access.FillList()
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        Return Nothing
    '    End Try
    'End Function

    Public Function Select_EFOTrd(ByVal dtEXp As Date) As DataTable
        Try
            data_access.ParamClear()
            data_access.AddParam("@ExpiryDate", OleDbType.Date, 8, dtEXp)
            data_access.Cmd_Text = "Select_ExpPnl" 'SP_Select_EFOTrd
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    'Public Sub Trading(ByRef dt As DataTable)
    '    Try
    '        data_access.ParamClear()
    '        data_access.Cmd_Text = SP_SELECT_TRADING
    '        data_access.FillList(dt)

    '    Catch ex As Exception
    '        MsgBox(ex.ToString)

    '    End Try
    'End Sub
    'Create By Viral 2-Aug-11
    Public Sub Trading(ByRef dt As DataTable)
        Try
            data_access.ParamClear()
            If gstr_ProductName = "OMI" Then
                data_access.Cmd_Text = SP_SELECT_TRADING_old
            Else
                data_access.Cmd_Text = SP_SELECT_TRADING
            End If

            data_access.FillList(dt)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
 
    Public Function Trading_Ltp(ByVal dt As Date) As DataTable
        Try
            data_access.ParamClear()
            data_access.AddParam("entry_date", OleDbType.Date, 8, dt)
            data_access.Cmd_Text = SP_SELECT_TRADING_LTP
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function select_security() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_SECURITY
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    'By Viral 01-Aug-11
    Public Sub select_security(ByRef dt As DataTable)
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_SECURITY
            data_access.FillList(dt)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub Exec_Qry(ByVal StrQry)
        data_access.cmd_type = CommandType.Text
        data_access.ParamClear()
        'data_access.AddParam("@isliq", OleDbType.Boolean, 15, isliq)
        'data_access.AddParam("@token1", OleDbType.Integer, 18, token1)
        'data_access.AddParam("@script", OleDbType.VarChar, 250, Script)
        'data_access.AddParam("@company", OleDbType.VarChar, 250, sCompname)
        data_access.cmd_type = CommandType.Text
        data_access.Cmd_Text = StrQry
        data_access.ExecuteNonQuery()
        data_access.cmd_type = CommandType.StoredProcedure
    End Sub
    Public Function select_Query(ByVal StrQry As String) As DataTable
        Dim dt As New DataTable
        Try

            data_access.ParamClear()
            data_access.Cmd_Text = StrQry
            data_access.cmd_type = CommandType.Text
            data_access.FillList(dt)
            data_access.cmd_type = CommandType.StoredProcedure
            Return dt
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return dt
        End Try

    End Function
    Public Sub Insert(ByVal tokenno As Integer, ByVal buyprice As Double, ByVal saleprice As Double, ByVal itp As Double)
        data_access.ParamClear()
        data_access.AddParam("@token", OleDbType.Integer, 18, tokenno)
        data_access.AddParam("@buyprice", OleDbType.Double, 18, buyprice)
        data_access.AddParam("@saleprice", OleDbType.Double, 18, saleprice)
        data_access.AddParam("@ltp", OleDbType.Double, 18, itp)
        data_access.Cmd_Text = SP_INSERT_TODAY
        data_access.ExecuteNonQuery()
    End Sub

    Public Shared Function Insert_FOTrading(ByVal dtable As DataTable) As DataTable
        Dim DtPrimartExpRow As DataTable
        Dim Dt As New DataTable
        Dt.Columns.Add("entryno", GetType(Integer))
        Dt.Columns.Add("orderno", GetType(String))
        Dim DrOrderNo As DataRow = Dt.NewRow
        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@instrumentname", OleDbType.VarChar, 50, CStr(drow("InstrumentName")))
            data_access.AddParam("@company", OleDbType.VarChar, 50, CStr(drow("Company")))
            data_access.AddParam("@mdate", OleDbType.Date, 18, CDate(drow("Mdate")))
            data_access.AddParam("@strikerate", OleDbType.Double, 18, Val(drow("StrikeRate")))
            data_access.AddParam("@cp", OleDbType.VarChar, 18, CStr(drow("CP")))
            data_access.AddParam("@script", OleDbType.VarChar, 100, CStr(drow("Script")))
            data_access.AddParam("@qty", OleDbType.Double, 18, Val(drow("qty")))
            data_access.AddParam("@rate", OleDbType.Double, 18, Val(drow("Rate")))
            data_access.AddParam("@entrydate", OleDbType.Date, 18, CDate(drow("EntryDate")))
            data_access.AddParam("@entryno", OleDbType.Integer, 18, CInt(drow("entryno")))
            data_access.AddParam("@token1", OleDbType.Integer, 18, CInt(drow("token1")))
            data_access.AddParam("@isliq", OleDbType.Boolean, 18, (drow("isliq")))
            data_access.AddParam("@orderno", OleDbType.VarChar, 30, CStr(drow("orderno")))
            data_access.AddParam("@lActivityTime", OleDbType.Integer, 30, CStr(drow("lActivityTime")))
            data_access.AddParam("@FileFlag", OleDbType.VarChar, 30, CStr(drow("FileFlag")))
            data_access.AddParam("@Dealer", OleDbType.VarChar, 30, CStr(drow("Dealer")))


            Dim tot As Double
            Dim BuyQty As Double
            Dim SaleQty As Double
            Dim BuyVal As Double
            Dim SaleVal As Double
            Dim tot2 As Double
            If drow("qty") = 0 Then
                tot = drow("rate")
            Else
                tot = drow("qty") * drow("rate")
            End If


            If drow("qty") = 0 Then
                tot2 = Val(drow("StrikeRate")) + drow("rate")
            Else
                tot2 = drow("qty") * (Val(drow("StrikeRate")) + drow("rate"))
            End If
            
            Dim cpf As String
            If drow("CP") = "X" Or drow("CP") = "" Then
                cpf = "F"
            Else
                cpf = drow("CP")
            End If

         
            If drow("qty") > 0 Then
                BuyQty = drow("qty")
            Else
                BuyQty = 0
            End If

            If drow("qty") < 0 Then
                SaleQty = drow("qty")
            Else
                SaleQty = 0
            End If

            If drow("qty") > 0 Then
                BuyVal = drow("qty") * drow("rate")
            Else
                BuyVal = 0
            End If

            If drow("qty") < 0 Then
                SaleVal = drow("qty") * drow("rate")
            Else
                SaleVal = 0
            End If
            data_access.AddParam("@mo", OleDbType.VarChar, 30, CStr(Format(CDate(drow("Mdate")), "mm/yyyy")))
            data_access.AddParam("@token", OleDbType.Integer, 18, Val(0))
            data_access.AddParam("@tot", OleDbType.Double, 18, Val(tot))
            data_access.AddParam("@entry_date", OleDbType.Date, 18, CDate(CDate(drow("EntryDate")).ToString("dd/MMM/yyyy")))
            data_access.AddParam("@tot2", OleDbType.Double, 18, Val(tot2))
            data_access.AddParam("@cpf", OleDbType.VarChar, 30, cpf)
            data_access.AddParam("@BuyQty", OleDbType.Integer, 30, Val(BuyQty))
            data_access.AddParam("@SaleQty", OleDbType.Integer, 30, Val(SaleQty))
            data_access.AddParam("@BuyVal", OleDbType.Double, 30, Val(BuyVal))
            data_access.AddParam("@SaleVal", OleDbType.Double, 30, Val(SaleVal))

        Next
        data_access.Cmd_Text = SP_Script_Insert
        DtPrimartExpRow = data_access.ExecuteMultiple(26)
        For Each Dr As DataRow In New DataView(DtPrimartExpRow, "(ParameterName='Varorderno' OR ParameterName='Varentryno')", "", DataViewRowState.CurrentRows).ToTable.Rows
            If Dr("ParameterName") = "Varentryno" Then
                DrOrderNo = Dt.NewRow
                DrOrderNo("entryno") = Dr("Value")
            ElseIf Dr("ParameterName") = "Varorderno" Then
                DrOrderNo("orderno") = Dr("Value")
                Dt.Rows.Add(DrOrderNo)
            End If
        Next
        Return Dt
    End Function
    
    Public Function Script() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_SCRIPT
            data_access.cmd_type = CommandType.StoredProcedure
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function ScriptToken() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_SCRIPT
            data_access.cmd_type = CommandType.StoredProcedure
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    'BY Viral 2-Aug-11
    Public Sub Script(ByRef Dt As DataTable)
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_SCRIPT
            data_access.FillList(Dt)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Function Equity_Script() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_EQUITYSCRIPT
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function UpperToken_Script(ByVal Expiry As Date, ByVal symbol As String) As DataTable
        Try
            data_access.ParamClear()
            data_access.AddParam("@MDate", OleDbType.Date, 50, Expiry)
            'data_access.AddParam("@Ltp", OleDbType.Double, 50, ltp)
            data_access.AddParam("@Symbol", OleDbType.VarChar, 18, symbol)
            data_access.Cmd_Text = SP_SELECT_UPPERTOKENS
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    'noUse Commenyby payalpatel
    'Public Function DownToken_Script(ByVal Expiry As Date, ByVal ltp As Double, ByVal symbol As String) As DataTable
    '    Try
    '        data_access.ParamClear()
    '        data_access.AddParam("@MDate", OleDbType.Date, 50, Expiry)
    '        data_access.AddParam("@Ltp", OleDbType.Double, 50, ltp)
    '        data_access.AddParam("@Symbol", OleDbType.VarChar, 18, symbol)
    '        data_access.Cmd_Text = SP_SELECT_DOWNTOKENS
    '        Return data_access.FillList()
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        Return Nothing
    '    End Try
    'End Function

    Public Function UpperToken_Script_Curr(ByVal Expiry As Date, ByVal symbol As String) As DataTable
        Try
            data_access.ParamClear()
            data_access.AddParam("@MDate", OleDbType.Date, 50, Expiry)
            'data_access.AddParam("@Ltp", OleDbType.Double, 50, ltp)
            data_access.AddParam("@Symbol", OleDbType.VarChar, 18, symbol)
            data_access.Cmd_Text = SP_SELECT_UPPERTOKENS_Curr
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    'Public Function DownToken_Script_Curr(ByVal Expiry As Date, ByVal ltp As Double, ByVal symbol As String) As DataTable
    '    Try
    '        data_access.ParamClear()
    '        data_access.AddParam("@MDate", OleDbType.Date, 50, Expiry)
    '        data_access.AddParam("@Ltp", OleDbType.Double, 50, ltp)
    '        data_access.AddParam("@Symbol", OleDbType.VarChar, 18, symbol)
    '        data_access.Cmd_Text = SP_SELECT_DOWNTOKENS_Curr
    '        Return data_access.FillList()
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        Return Nothing
    '    End Try
    'End Function

    Public Sub insert_EQTrading(ByVal dtable As DataTable)
        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@script", OleDbType.VarChar, 50, CStr(drow("script")))
            data_access.AddParam("@company", OleDbType.VarChar, 50, CStr(drow("Company")))
            data_access.AddParam("@eq", OleDbType.VarChar, 18, CStr(drow("eq")))
            data_access.AddParam("@qty", OleDbType.Double, 18, Val(drow("qty")))
            data_access.AddParam("@rate", OleDbType.Double, 18, Val(drow("Rate")))
            data_access.AddParam("@entrydate", OleDbType.Date, 18, CDate(drow("EntryDate")))
            data_access.AddParam("@entryno", OleDbType.Integer, 18, CInt(drow("entryno")))
            data_access.AddParam("@orderno", OleDbType.VarChar, 30, CStr(drow("orderno")))
            data_access.AddParam("@lActivityTime", OleDbType.Integer, 30, CStr(drow("lActivityTime")))
            data_access.AddParam("@FileFlag", OleDbType.VarChar, 30, CStr(drow("FileFlag")))
            data_access.AddParam("@Dealer", OleDbType.VarChar, 30, CStr(drow("Dealer")))
        Next
        data_access.Cmd_Text = SP_INSERT_EQUITY
        data_access.ExecuteMultiple(11)
    End Sub
    Public Function select_equity() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_EQUITY
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    'Create By Viral 2-Aug-11
    Public Sub select_equity(ByRef dt As DataTable)
        Try
            data_access.ParamClear()
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.Cmd_Text = SP_SELECT_EQUITY
            data_access.FillList(dt)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Function Select_Import_Setting() As DataTable
        Try
            data_access.ParamClear()
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.Cmd_Text = SP_Select_Import_Setting
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub Delete_Import_Setting()
        data_access.ParamClear()
        data_access.cmd_type = CommandType.StoredProcedure
        data_access.Cmd_Text = SP_Delete_Import_Setting
        data_access.ExecuteNonQuery()
    End Sub
    Public Function Select_Import_type() As DataTable
        Try
            data_access.ParamClear()
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.Cmd_Text = SP_Select_Import_Type
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Sub Insert_Import_Setting(ByVal VarImport_Type As String, ByVal VarText_Type As String, ByVal VarServer_Type As String, ByVal VarServer_Name As String, ByVal VarDatabase_Name As String, ByVal VarUser_Name As String, ByVal VarPwd As String, ByVal VarTable_Name As String, ByVal VarFile_Path As String, ByVal VarFileName_Format As String, ByVal VarFile_Code As String, ByVal VarAuto_Import As Boolean, ByVal VarManual_Import As Boolean)
        data_access.ParamClear()
        data_access.AddParam("@Import_Type", OleDbType.VarChar, 150, VarImport_Type)
        data_access.AddParam("@Text_Type", OleDbType.VarChar, 150, VarText_Type)
        data_access.AddParam("@Server_Type", OleDbType.VarChar, 150, VarServer_Type)
        data_access.AddParam("@Server_Name", OleDbType.VarChar, 150, VarServer_Name)
        data_access.AddParam("@Database_Name", OleDbType.VarChar, 150, VarDatabase_Name)
        data_access.AddParam("@User_Name", OleDbType.VarChar, 150, VarUser_Name)
        data_access.AddParam("@Pwd", OleDbType.VarChar, 150, VarPwd)
        data_access.AddParam("@Table_Name", OleDbType.VarChar, 150, VarTable_Name)
        data_access.AddParam("@File_Path", OleDbType.VarChar, 150, VarFile_Path)
        data_access.AddParam("@FileName_Format", OleDbType.VarChar, 150, VarFileName_Format)
        data_access.AddParam("@File_Code", OleDbType.VarChar, 150, VarFile_Code)
        data_access.AddParam("@Auto_Import", OleDbType.Boolean, 1, VarAuto_Import)
        data_access.AddParam("@Manual_Import", OleDbType.Boolean, 1, VarManual_Import)
        data_access.Cmd_Text = SP_Insert_Import_Setting
        data_access.ExecuteNonQuery()
    End Sub

#Region "Currency Trading"

    Public Function SELECTED_Currency_Contract() As DataTable
        Try

            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECTED_Currency_Contract
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    'by Viral 1-Aug-11
    Public Sub SELECTED_Currency_Contract(ByRef dt As DataTable)
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECTED_Currency_Contract
            data_access.FillList(dt)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub insert_Currency_Trading(ByVal dtable As DataTable)
        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@instrumentname", OleDbType.VarChar, 50, CStr(drow("InstrumentName")))
            data_access.AddParam("@company", OleDbType.VarChar, 50, CStr(drow("Company")))
            data_access.AddParam("@mdate", OleDbType.Date, 18, CDate(drow("Mdate")))
            data_access.AddParam("@strikerate", OleDbType.Double, 18, Val(drow("StrikeRate")))
            data_access.AddParam("@cp", OleDbType.VarChar, 18, CStr(drow("CP")))
            data_access.AddParam("@script", OleDbType.VarChar, 100, CStr(drow("Script")))
            data_access.AddParam("@qty", OleDbType.Double, 18, Val(drow("qty")))
            data_access.AddParam("@rate", OleDbType.Double, 18, Val(drow("Rate")))
            data_access.AddParam("@entrydate", OleDbType.Date, 18, CDate(drow("EntryDate")))
            data_access.AddParam("@entryno", OleDbType.Integer, 18, CInt(drow("entryno")))
            data_access.AddParam("@token1", OleDbType.Integer, 18, CInt(drow("token1")))
            data_access.AddParam("@isliq", OleDbType.Boolean, 18, (drow("isliq")))
            data_access.AddParam("@orderno", OleDbType.VarChar, 30, CStr(drow("orderno")))
            data_access.AddParam("@lActivityTime", OleDbType.Integer, 30, CStr(drow("lActivityTime")))
            data_access.AddParam("@FileFlag", OleDbType.VarChar, 30, CStr(drow("FileFlag")))
            data_access.AddParam("@Dealer", OleDbType.VarChar, 30, CStr(drow("Dealer")))
        Next
        data_access.Cmd_Text = SP_INSERT_Currency_Trading
        data_access.ExecuteMultiple(16)
    End Sub
    Public Function select_Currency_Trading() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_Currency_Trading
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    'Create By Viral 2-Aug-11
    Public Sub select_Currency_Trading(ByVal dt As DataTable)
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_Currency_Trading
            data_access.FillList(dt)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
#End Region


#Region "Previous Balance"
    Public Function prebal() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_PREBAL
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try

    End Function
    Public Sub insert_prebal(ByVal drow As DataRow)
        data_access.ParamClear()
        'For Each drow As DataRow In dtable.Rows
        data_access.AddParam("@tdate", OleDbType.Date, 18, CDate(drow("tdate")))
        data_access.AddParam("@stbal", OleDbType.Double, 18, Val(drow("stbal")))
        data_access.AddParam("@futbal", OleDbType.Double, 18, Val(drow("futbal")))
        data_access.AddParam("@optbal", OleDbType.Double, 18, Val(drow("optbal")))
        data_access.AddParam("@company", OleDbType.VarChar, 50, CStr(drow("company")))
        'Next
        data_access.Cmd_Text = SP_INSERT_PREBAL
        data_access.ExecuteMultiple(5)
    End Sub
    Public Sub insert_prebal(ByVal dtable As DataTable)
        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@tdate", OleDbType.Date, 18, CDate(drow("tdate")))
            data_access.AddParam("@stbal", OleDbType.Double, 18, Val(drow("stbal")))
            data_access.AddParam("@futbal", OleDbType.Double, 18, Val(drow("futbal")))
            data_access.AddParam("@optbal", OleDbType.Double, 18, Val(drow("optbal")))
            data_access.AddParam("@company", OleDbType.VarChar, 50, CStr(drow("company")))
        Next
        data_access.Cmd_Text = SP_INSERT_PREBAL
        data_access.ExecuteMultiple(5)
    End Sub
    Public Sub update_prebal(ByVal tdate As Date, ByVal stbal As Double, ByVal futbal As Double, ByVal optbal As Double, ByVal compname As String)
        data_access.ParamClear()
        data_access.AddParam("@tdate1", OleDbType.Date, 18, CDate(tdate))
        data_access.AddParam("@stbal", OleDbType.Double, 18, stbal)
        data_access.AddParam("@futbal", OleDbType.Double, 18, futbal)
        data_access.AddParam("@optbal", OleDbType.Double, 18, optbal)
        data_access.AddParam("@company", OleDbType.VarChar, 50, compname)
        data_access.Cmd_Text = SP_UPDATE_PREBAL
        data_access.ExecuteMultiple(5)

    End Sub
    Public Sub Delete_prBal(ByVal date1 As Date)
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Pr_balace_date
            data_access.AddParam("@date1", OleDbType.Date, 18, CDate(date1))
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


    End Sub
    Public Sub Delete_profile(ByVal FormName As String)
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Delete_DataGrid_Column_Profile
            data_access.AddParam("@FormName", OleDbType.VarChar, 18, FormName)
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


    End Sub
    Public Sub Delete_prBal(ByVal date1 As Date, ByVal compname As String)
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Pr_balace_name
            data_access.AddParam("@date1", OleDbType.Date, 18, CDate(date1))
            data_access.AddParam("@company", OleDbType.VarChar, 50, compname)
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Delete_prBal() ::" & ex.ToString)
        End Try
    End Sub
#End Region
#Region "CFBalance"
    Public Sub Reset_CFBalance()
        Try
            Dim tblCfbalancedata As DataTable = Select_CFBalance()
            For Each dr As DataRow In tblCfbalancedata.Rows
                If dr("symbol").ToString.Contains("/") Then
                    dr.Delete()
                Else
                    dr("Balance") = 0
                End If

            Next
            tblCfbalancedata.AcceptChanges()
            update_CFBalance1(tblCfbalancedata)
        Catch ex As Exception
            MsgBox("Trading :: Reset_CFBalance() ::" & ex.ToString)
            ' Return Nothing
        End Try
    End Sub
    Public Function Select_CFBalance() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Select_CFBalance
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Trading :: Select_CFBalance() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub Insert_CFBalance(ByVal Symbol As String, ByVal Balance As Double)
        Try
            data_access.ParamClear()
            data_access.AddParam("@Symbol", OleDbType.VarChar, 50, CStr(Symbol))
            data_access.AddParam("@Balance", OleDbType.Double, 18, Val(Balance))
            data_access.Cmd_Text = SP_Insert_CFBalance
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Insert_CFBalance() ::" & ex.ToString)
        End Try
    End Sub
    Public Sub update_CFBalance(ByVal dtable As DataTable)
        Try

            For Each drow As DataRow In dtable.Rows
                data_access.ParamClear()
                data_access.AddParam("@Symbol", OleDbType.VarChar, 50, CStr(Trim(drow("Symbol"))))
                data_access.AddParam("@Balance", OleDbType.Double, 18, Val(drow("Balance")))
                data_access.Cmd_Text = SP_Insert_CFBalance
                data_access.ExecuteNonQuery()
                'data_access.Cmd_Text = SP_Update_CFBalance
                'data_access.ExecuteMultiple(2)
            Next

        Catch ex As Exception
            MsgBox("Trading :: update_CFBalance() ::" & ex.ToString)

        End Try
    End Sub
    Public Sub Delete_TblCFBalance()
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Delete_CFBalance
            data_access.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub update_CFBalance1(ByVal dtable As DataTable)
        Try

            Delete_TblCFBalance()
            data_access.ParamClear()
            For Each drow As DataRow In dtable.Rows
                data_access.AddParam("@Symbol", OleDbType.VarChar, 50, CStr(Trim(drow("Symbol"))))
                data_access.AddParam("@Balance", OleDbType.Double, 18, Val(drow("Balance")))
            Next
            data_access.Cmd_Text = SP_Insert_CFBalance
            data_access.ExecuteMultiple(2)

        Catch ex As Exception
            MsgBox("Trading :: update_CFBalance() ::" & ex.ToString)

        End Try
    End Sub

#End Region

#Region "Exposure Margin"
    Public Function Exposure_margin() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_EXPOSURE_MARGIN
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Trading :: Exposure_margin() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub delete_Exposure_margin()
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_delete_exposure_margin
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Delete_Exposure_margin() ::" & ex.ToString)

        End Try
    End Sub
    Public Sub Insert_Exposure_margin(ByVal compname As String, ByVal expmarg As Double)
        Try
            data_access.ParamClear()
            data_access.AddParam("@compname", OleDbType.VarChar, 50, CStr(compname))
            data_access.AddParam("@exposure_margin", OleDbType.Double, 18, Val(expmarg))
            data_access.Cmd_Text = SP_insert_exposure_margin
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Insert_Exposure_margin() ::" & ex.ToString)
        End Try
    End Sub


    'Public Sub update_Currency_exposure_margin(ByVal compname As String, ByVal InstrumentType As String, ByVal Normal_exposure As Double, ByVal Additional_exposure As Double, ByVal expmarg As Double)
    '    Try
    '        data_access.ParamClear()
    '        data_access.AddParam("@expmarg", OleDbType.Double, 18, Val(expmarg))
    '        data_access.AddParam("@compname", OleDbType.VarChar, 50, CStr(compname))
    '        data_access.Cmd_Text = SP_update_exposure_margin
    '        data_access.ExecuteNonQuery()
    '    Catch ex As Exception
    '        MsgBox("Trading :: update_Exposure_margin() ::" & ex.ToString)

    '    End Try
    'End Sub
    Public Sub update_Exposure_margin(ByVal compname As String, ByVal expmarg As Double)
        Try
            data_access.ParamClear()
            data_access.AddParam("@expmarg", OleDbType.Double, 18, Val(expmarg))
            data_access.AddParam("@compname", OleDbType.VarChar, 50, CStr(compname))

            data_access.Cmd_Text = SP_update_exposure_margin
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: update_Exposure_margin() ::" & ex.ToString)

        End Try
    End Sub
    Public Sub Delete_Exposure_margin_Select(ByVal compname As String, ByVal expmarg As Double)
        Try
            data_access.ParamClear()
            data_access.AddParam("@compname", OleDbType.VarChar, 50, CStr(compname))
            data_access.AddParam("@expmarg", OleDbType.Double, 18, Val(expmarg))

            data_access.Cmd_Text = SP_delete_exposure_margin_select
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Delete_Select_Exposure_margin() ::" & ex.ToString)

        End Try
    End Sub

    Public Sub Insert_AEL_Contracts(ByVal Symbol As String, ByVal InsType As String, ByVal ExpDate As Date, ByVal StrikePrice As Double, ByVal OptType As String, ByVal CALevel As Double, ByVal ELM_Per As Double)
        Try
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.ParamClear()
            data_access.AddParam("@Symbol", OleDbType.VarChar, 50, CStr(Symbol))
            data_access.AddParam("@InsType", OleDbType.VarChar, 50, CStr(InsType))

            data_access.AddParam("@ExpDate", OleDbType.Date, 18, CDate(ExpDate))
            data_access.AddParam("@StrikePrice", OleDbType.Double, 18, Val(StrikePrice))
            data_access.AddParam("@OptType", OleDbType.VarChar, 50, CStr(OptType))
            data_access.AddParam("@CALevel", OleDbType.Double, 18, Val(CALevel))
            data_access.AddParam("@ELM_Per", OleDbType.Double, 18, Val(ELM_Per))

            data_access.Cmd_Text = SP_insert_ael_contracts
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Insert_Ael_Contracts() ::" & ex.ToString)
        End Try
    End Sub

    Public Sub update_AEL_Contracts(ByVal Symbol As String, ByVal InsType As String, ByVal ExpDate As Date, ByVal StrikePrice As Double, ByVal OptType As String, ByVal CALevel As Double, ByVal ELM_Per As Double)
        Try
            data_access.ParamClear()
            data_access.AddParam("@Symbol", OleDbType.VarChar, 50, CStr(Symbol))
            data_access.AddParam("@InsType", OleDbType.VarChar, 50, CStr(InsType))

            data_access.AddParam("@ExpDate", OleDbType.Date, 18, CDate(ExpDate))
            data_access.AddParam("@StrikePrice", OleDbType.Double, 18, Val(StrikePrice))
            data_access.AddParam("@OptType", OleDbType.VarChar, 50, CStr(OptType))
            data_access.AddParam("@CALevel", OleDbType.Double, 18, Val(CALevel))
            data_access.AddParam("@ELM_Per", OleDbType.Double, 18, Val(ELM_Per))

            data_access.Cmd_Text = SP_update_ael_contracts
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: update_Ael_Contracts() ::" & ex.ToString)

        End Try
    End Sub

    Public Sub delete_AEL_Contracts()
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_delete_ael_contracts
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Delete_ael_contracts() ::" & ex.ToString)

        End Try
    End Sub

    Public Function AEL_Contracts() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_select_ael_contracts
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Trading :: AEL_Contracts() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function Exposure_Margin_New() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_EXPOSURE_MARGIN_NEW
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Trading :: Exposure_margin_new() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Sub Insert_Exposure_margin_new(ByVal Symbol As String, ByVal InsType As String, ByVal Norm_Margin As Double, ByVal Add_Margin As Double, ByVal Total_Margin As Double)
        Try
            data_access.ParamClear()
            data_access.AddParam("@Symbol", OleDbType.VarChar, 50, CStr(Symbol))
            data_access.AddParam("@InsType", OleDbType.VarChar, 50, CStr(InsType))

            data_access.AddParam("@Norm_Margin", OleDbType.Double, 18, Val(Norm_Margin))
            data_access.AddParam("@Add_Margin", OleDbType.Double, 18, Val(Add_Margin))
            data_access.AddParam("@Total_Margin", OleDbType.Double, 18, Val(Total_Margin))

            data_access.Cmd_Text = SP_insert_exposure_margin_New
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Insert_Exposure_margin_new() ::" & ex.ToString)
        End Try
    End Sub

    Public Sub update_Exposure_margin_new(ByVal Symbol As String, ByVal InsType As String, ByVal Norm_Margin As Double, ByVal Add_Margin As Double, ByVal Total_Margin As Double)
        Try
            data_access.ParamClear()
            data_access.AddParam("@Symbol", OleDbType.VarChar, 50, CStr(Symbol))
            data_access.AddParam("@InsType", OleDbType.VarChar, 50, CStr(InsType))

            data_access.AddParam("@Norm_Margin", OleDbType.Double, 18, Val(Norm_Margin))
            data_access.AddParam("@Add_Margin", OleDbType.Double, 18, Val(Add_Margin))
            data_access.AddParam("@Total_Margin", OleDbType.Double, 18, Val(Total_Margin))
            data_access.Cmd_Text = SP_update_exposure_margin_New
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: update_Exposure_margin_new() ::" & ex.ToString)

        End Try
    End Sub

    Public Sub delete_Exposure_margin_new()
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_delete_exposure_margin_New
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Delete_Exposure_margin() ::" & ex.ToString)

        End Try
    End Sub

#End Region

#Region "Currency Exposure Margin"
    Public Function Select_Currency_Exposure_margin() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_CURRENCY_EXPOSURE_MARGIN
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Trading :: Currency_Exposure_margin() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub delete_Cuurency_Exposure_margin(ByVal compname As String)
        Try
            data_access.ParamClear()
            data_access.AddParam("@compname", OleDbType.VarChar, 50, CStr(compname))

            data_access.Cmd_Text = SP_delete_CURRENCY_EXPOSURE_MARGIN
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: delete_Currency_Exposure_margin() ::" & ex.ToString)

        End Try
    End Sub
    Public Sub Insert_Currency_Exposure_margin(ByVal compname As String, ByVal expmarg As Double)
        Try
            data_access.ParamClear()
            data_access.AddParam("@compname", OleDbType.VarChar, 50, CStr(compname))
            data_access.AddParam("@exposure_margin", OleDbType.Double, 18, Val(expmarg))
            data_access.Cmd_Text = SP_insert_CURRENCY_EXPOSURE_MARGIN
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Insert_Currency_Exposure_margin() ::" & ex.ToString)
        End Try
    End Sub
    Public Sub Update_Currency_Exposure_margin(ByVal compname As String, ByVal expmarg As Double)
        Try
            data_access.ParamClear()

            data_access.AddParam("@expmarg", OleDbType.Double, 18, Val(expmarg))
            data_access.AddParam("@compname", OleDbType.VarChar, 50, CStr(compname))

            data_access.Cmd_Text = SP_update_CURRENCY_EXPOSURE_MARGIN
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: update_Currency_Exposure_margin() ::" & ex.ToString)

        End Try
    End Sub
    Public Sub Delete_Currency_Exposure_margin_Select(ByVal compname As String, ByVal expmarg As Double)
        Try
            data_access.ParamClear()
            data_access.AddParam("@compname", OleDbType.VarChar, 50, CStr(compname))
            data_access.AddParam("@expmarg", OleDbType.Double, 18, Val(expmarg))
            data_access.Cmd_Text = SP_delete_CURRENCY_EXPOSURE_MARGIN_Select
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Delete_Currency_Exposure_margin_Select() ::" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Setting"
    Public Function Settings() As DataTable
        Try
            data_access.ParamClear()
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.Cmd_Text = SP_SELECT_SETTING
            Return data_access.FillList()
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub Insert_setting()
        data_access.ParamClear()
        data_access.AddParam("@SettingName", OleDbType.VarChar, 250, SettingName)
        data_access.AddParam("@SettingKey", OleDbType.VarChar, 3000, SettingKey)
        data_access.Cmd_Text = sp_insert_setting
        data_access.ExecuteNonQuery()
    End Sub
    Public Sub Delete_Data_script(ByVal company As String)
        data_access.ParamClear()
        data_access.AddParam("@company", OleDbType.VarChar, 100, company)
        data_access.Cmd_Text = SP_delete_trading_script
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        data_access.AddParam("@company", OleDbType.VarChar, 100, company)
        data_access.Cmd_Text = SP_delete_Currency_trading_Script
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        data_access.AddParam("@company", OleDbType.VarChar, 100, company)
        data_access.Cmd_Text = SP_delete_security_script
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        data_access.AddParam("@company", OleDbType.VarChar, 100, company)
        data_access.Cmd_Text = SP_delete_prbalance_company
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        data_access.AddParam("@company", OleDbType.VarChar, 100, company)
        data_access.Cmd_Text = SP_delete_patch_expense_company
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        data_access.AddParam("@company", OleDbType.VarChar, 100, company)
        data_access.Cmd_Text = SP_delete_CFPnL_company
        data_access.ExecuteNonQuery()


    End Sub


    Public Sub Update_NewToken()

        Dim StrSql As String

        'Private Updat_tok_Trading As String = "UPDATE trading INNER JOIN contract ON trading.script = contract.script SET trading.token1 =contract.token;"
        'Private Update_tok_Analysis As String = "UPDATE analysis INNER JOIN contract ON analysis.script = contract.script SET analysis.token1 =contract.token"

        StrSql = "UPDATE trading INNER JOIN contract ON trading.script = contract.script SET trading.token1 =contract.token;"
        data_access.ParamClear()
        data_access.ExecuteNonQuery(StrSql, CommandType.Text)
        data_access.cmd_type = CommandType.StoredProcedure



        StrSql = "UPDATE analysis INNER JOIN contract ON analysis.script = contract.script SET analysis.token1 =contract.token"
        data_access.ParamClear()
        data_access.ExecuteNonQuery(StrSql, CommandType.Text)
        data_access.cmd_type = CommandType.StoredProcedure


        
    End Sub
    Public Sub Update_setting()
        data_access.ParamClear()
        data_access.AddParam("@SettingKey", OleDbType.VarChar, 3000, SettingKey)
        data_access.AddParam("@SettingName", OleDbType.VarChar, 250, SettingName)
        data_access.AddParam("@uid", OleDbType.Integer, 18, Uid)
        data_access.Cmd_Text = sp_update_setting
        data_access.ExecuteNonQuery()
    End Sub

    Public Sub Update_setting(ByVal SettingKey As String, ByVal SettingName As String)
        Try


            Dim qry As String

            qry = "UPDATE settings SET SettingKey ='" + SettingKey + "' WHERE SettingName='" + SettingName + "';"
            data_access.ParamClear()
            data_access.Cmd_Text = qry
            data_access.cmd_type = CommandType.Text

            data_access.ExecuteNonQuery()
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception

        End Try
    End Sub
#End Region

    'Public Sub Update_Liq(ByVal script As String, ByVal token1 As Long, ByVal isliq As Boolean, ByVal sCompName As String)

    '    data_access.ParamClear()
    '    data_access.AddParam("@isliq", OleDbType.Boolean, 15, isliq)
    '    data_access.AddParam("@token1", OleDbType.Integer, 18, token1)
    '    data_access.AddParam("@script", OleDbType.VarChar, 250, script)
    '    data_access.Cmd_Text = sp_update_liq
    '    data_access.ExecuteNonQuery()

    'End Sub

    Public Sub Insert_company_ana(ByVal compname As String, ByVal call_vol As Double, ByVal put_vol As Double, ByVal call_vol1 As Double, ByVal put_vol1 As Double, ByVal call_vol2 As Double, ByVal put_vol2 As Double, ByVal fut1 As Double, ByVal fut2 As Double, ByVal fut3 As Double, ByVal entrydate As DateTime, ByVal exp1 As DateTime, ByVal exp2 As DateTime, ByVal exp3 As DateTime, ByVal eqrate As Double, ByVal ischeck As Boolean)
        data_access.ParamClear()
        data_access.AddParam("@compnay", OleDbType.VarChar, 50, compname)
        data_access.AddParam("@call_vol", OleDbType.Double, 18, call_vol)
        data_access.AddParam("@put_vol", OleDbType.Double, 18, put_vol)
        data_access.AddParam("@call_vol1", OleDbType.Double, 18, call_vol1)
        data_access.AddParam("@put_vol1", OleDbType.Double, 18, put_vol1)
        data_access.AddParam("@call_vol2", OleDbType.Double, 18, call_vol2)
        data_access.AddParam("@put_vol2", OleDbType.Double, 18, put_vol2)
        data_access.AddParam("@fut1", OleDbType.Double, 18, fut1)
        data_access.AddParam("@fut2", OleDbType.Double, 18, fut2)
        data_access.AddParam("@fut3", OleDbType.Double, 18, fut3)
        data_access.AddParam("@entrydate", OleDbType.Date, 18, entrydate)
        data_access.AddParam("@exp1", OleDbType.Date, 18, exp1)
        data_access.AddParam("@exp2", OleDbType.Date, 18, exp2)
        data_access.AddParam("@exp3", OleDbType.Date, 18, exp3)
        data_access.AddParam("@equity_check", OleDbType.Boolean, 18, ischeck)
        data_access.AddParam("@equity", OleDbType.Double, 18, eqrate)
        data_access.Cmd_Text = SP_insert_company_analysis
        data_access.ExecuteNonQuery()
    End Sub
    Public Sub Update_company_ana(ByVal compname As String, ByVal call_vol As Double, ByVal put_vol As Double, ByVal call_vol1 As Double, ByVal put_vol1 As Double, ByVal call_vol2 As Double, ByVal put_vol2 As Double, ByVal fut1 As Double, ByVal fut2 As Double, ByVal fut3 As Double, ByVal entrydate As DateTime, ByVal exp1 As DateTime, ByVal exp2 As DateTime, ByVal exp3 As DateTime, ByVal eqrate As Double, ByVal isCheck As Boolean)
        data_access.ParamClear()
        data_access.AddParam("@call_vol", OleDbType.Double, 18, call_vol)
        data_access.AddParam("@put_vol", OleDbType.Double, 18, put_vol)
        data_access.AddParam("@call_vol1", OleDbType.Double, 18, call_vol1)
        data_access.AddParam("@put_vol1", OleDbType.Double, 18, put_vol1)
        data_access.AddParam("@call_vol2", OleDbType.Double, 18, call_vol2)
        data_access.AddParam("@put_vol2", OleDbType.Double, 18, put_vol2)
        data_access.AddParam("@fut1", OleDbType.Double, 18, fut1)
        data_access.AddParam("@fut2", OleDbType.Double, 18, fut2)
        data_access.AddParam("@fut3", OleDbType.Double, 18, fut3)
        data_access.AddParam("@entrydate", OleDbType.Date, 18, entrydate)
        data_access.AddParam("@exp1", OleDbType.Date, 18, exp1)
        data_access.AddParam("@exp2", OleDbType.Date, 18, exp2)
        data_access.AddParam("@exp3", OleDbType.Date, 18, exp3)
        data_access.AddParam("@equity", OleDbType.Double, 18, eqrate)
        data_access.AddParam("@equity_check", OleDbType.Boolean, 18, isCheck)
        data_access.AddParam("@compnay", OleDbType.VarChar, 50, compname)

        data_access.Cmd_Text = SP_update_company_analysis
        data_access.ExecuteNonQuery()
    End Sub
    Public Sub Delete_company_ana(ByVal compname As String)
        Try
            If compname = "" Then
                data_access.ParamClear()
                data_access.Cmd_Text = sp_delete_company_analysis
                data_access.ExecuteNonQuery()
            Else
                data_access.ParamClear()
                data_access.AddParam("@company", OleDbType.VarChar, 50, compname)
                data_access.Cmd_Text = sp_delete_company_analysis_company
                data_access.ExecuteNonQuery()
            End If

        Catch ex As Exception
            MsgBox("Trading :: Delete_company_ana() ::" & ex.ToString)
        End Try
    End Sub
    Public Function select_company_ana() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_select_company_analysis
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Trading :: select_company_ana() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub Delete_TCP_Connection()
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Delete_tblTcpConnectuon_data
            data_access.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Delete_TblRefreshSymbol()
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Delete_TblRefreshSymbol
            data_access.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub
    Public Function select_chklistbox_symbol_all() As DataTable
        Try
            data_access.ParamClear()
            data_access.cmd_type = CommandType.Text
            data_access.Cmd_Text = "SELECT symbol " & _
                    " FROM (SELECT  Symbol from Contract " & _
                    " Union " & _
                    " SELECT  Symbol from Security" & _
                    " Union " & _
                    " SELECT  Symbol from Currency_Contract)  AS tblaa;"
            'sp_Select_TblRefreshSymbol_all
            Return data_access.FillList()
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox("Trading :: Select_TblRefreshSymbol_all() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function select_chklistbox_symbol() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = sp_Select_TblRefreshSymbol
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Trading :: select_TCP_Connection() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function select_TCP_Connection() As DataTable
        Try
            data_access.ParamClear()
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.Cmd_Text = SP_select_TCP_Connection
            Return data_access.FillList()
        Catch ex As Exception
            ' MsgBox("Trading :: select_TCP_Connection() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub TblRefreshSymbol(ByVal dt As DataTable)
        Delete_TblRefreshSymbol()
        data_access.ParamClear()
        For Each drow As DataRow In dt.Rows
            data_access.AddParam("@Symbol", OleDbType.VarChar, 250, drow("Symbol"))
        Next
        data_access.Cmd_Text = SP_Insert_TblRefreshSymbol
        data_access.ExecuteMultiple(1)
    End Sub

    Public Sub Insert_TCP_Connection(ByVal dt As DataTable)
        Delete_TCP_Connection()
        data_access.ParamClear()
        For Each drow As DataRow In dt.Rows
            data_access.AddParam("@ConnectionName", OleDbType.VarChar, 50, drow("ConnectionName"))
            data_access.AddParam("@Server", OleDbType.VarChar, 50, CStr(drow("Server")))
            data_access.AddParam("@DBName", OleDbType.VarChar, 100, CStr(drow("DBName")))
            data_access.AddParam("@UserName", OleDbType.VarChar, 100, CStr(drow("UserName")))
            data_access.AddParam("@Password", OleDbType.VarChar, 100, CStr(drow("Password")))
            data_access.AddParam("@Visible", OleDbType.VarChar, 18, CStr(drow("Visible")))
            data_access.AddParam("@MaxNo", OleDbType.Integer, 18, Val(drow("MaxNo")))
        Next
        data_access.Cmd_Text = SP_Insert_tblTcpConnectuon_data
        data_access.ExecuteMultiple(7)
    End Sub
    Public Sub select_company_ana(ByRef Dt As DataTable)
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_select_company_analysis
            data_access.FillList(Dt)
        Catch ex As Exception
            MsgBox("Trading :: select_company_ana() ::" & ex.ToString)
        End Try
    End Sub
    Public Function select_all_trade() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_select_All_trade
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Trading :: select_all_trade() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function select_all_trade_New() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Select_All_Trade_New
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Trading :: select_all_trade_New() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
#End Region
#Region "Update Strategy Name In Trading & Equity_Trading Table"
    Public Sub Update_equity_trading_Strategy_Name(ByVal VarUID As Integer, ByVal VarStrategy_Name As String)
        data_access.ParamClear()
        data_access.AddParam("Varuid", OleDbType.Integer, 18, VarUID)
        data_access.AddParam("VarStrategy_Name", OleDbType.VarChar, 255, VarStrategy_Name)
        data_access.Cmd_Text = SP_Update_equity_trading_Strategy_Name
        data_access.ExecuteNonQuery()
    End Sub
    Public Sub Update_trading_Strategy_Name(ByVal VarUID As Integer, ByVal VarStrategy_Name As String)
        data_access.ParamClear()
        data_access.AddParam("Varuid", OleDbType.Integer, 18, VarUID)
        data_access.AddParam("VarStrategy_Name", OleDbType.VarChar, 255, VarStrategy_Name)
        data_access.Cmd_Text = SP_Update_trading_Strategy_Name
        data_access.ExecuteNonQuery()
    End Sub
    Public Sub Update_equity_trading_Strategy_Name_Null(ByVal VarStrategy_Name As String)
        data_access.ParamClear()
        data_access.AddParam("VarStrategy_Name", OleDbType.VarChar, 255, VarStrategy_Name)
        data_access.Cmd_Text = SP_Update_equity_trading_Strategy_Name_Null
        data_access.ExecuteNonQuery()
    End Sub
    Public Sub Update_trading_Strategy_Name_Null(ByVal VarStrategy_Name As String)
        data_access.ParamClear()
        data_access.AddParam("VarStrategy_Name", OleDbType.VarChar, 255, VarStrategy_Name)
        data_access.Cmd_Text = SP_Update_trading_Strategy_Name_Null
        data_access.ExecuteNonQuery()
    End Sub
    Public Function Select_All_Strategy_Companywise() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Select_All_Strategy_Companywise
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Trading :: Select_All_Strategy_Companywise() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Expense Data"
    Public Function Select_Expense_Data() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Select_Expense_Data
            Dim Dttemp As DataTable = data_access.FillList()
            Dttemp.DefaultView.Sort = "company,exp_date,entry_date"
            Return Dttemp
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub Insert_Expense_Data(ByVal dtable As DataTable)
        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@entry_date", OleDbType.Date, 18, drow("entry_date"))
            data_access.AddParam("@company", OleDbType.VarChar, 50, CStr(drow("company")))
            data_access.AddParam("@script", OleDbType.VarChar, 100, CStr(drow("script")))
            data_access.AddParam("@exp_date", OleDbType.Date, 18, drow("exp_date"))
            data_access.AddParam("@expense", OleDbType.Double, 18, Val(drow("expense")))
        Next
        data_access.Cmd_Text = SP_Insert_Expense_Data
        data_access.cmd_type = CommandType.StoredProcedure
        data_access.ExecuteMultiple(5)
    End Sub
    Public Sub Delete_Expense_Data_All()
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Delete_Expense_Data_All
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Delete_Expense_Data() ::" & ex.ToString)
        End Try
    End Sub
#End Region
#Region "Patch Expense Diff"
    Public Function Select_Patch_Expense() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Select_Patch_Expense
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Trading :: Select_Expense_Diff() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub Update_Patch_Expense(ByVal company As String, ByVal PatchDate As Date, ByVal ExpenseDiff As Double, ByVal ExpenseDiffFlg As Boolean)
        data_access.ParamClear()
        data_access.AddParam("@PatchDate", OleDbType.Date, 18, PatchDate)
        data_access.AddParam("@ExpenseDiff", OleDbType.Double, 18, ExpenseDiff)
        data_access.AddParam("@ExpenseDiffFlg", OleDbType.Boolean, 1, ExpenseDiffFlg)
        data_access.AddParam("@Company", OleDbType.VarChar, 50, company)
        data_access.Cmd_Text = SP_Update_Patch_Expense
        data_access.ExecuteNonQuery()
    End Sub
#End Region

#Region "DataGrid Column Setting"
    ''' <summary>
    ''' Gsub_GridColumnSetting
    ''' </summary>
    ''' <remarks>This method call to Set column setting in Grid  read Setting from MDB.</remarks>
    Public Function GFun_SetGridColumnSetting(ByVal DGrid As DataGridView, ByVal sFormName As String) As DataTable
        Dim DT As New DataTable
        Dim DT1 As New DataTable
        'Dim I As Integer
lbl:
        data_access.ParamClear()
        data_access.Cmd_Text = SP_Select_DataGrid_Column_Setting

        DT1 = data_access.FillList()
        'and columnName='col1Strike2' and columnName='col1Strike3'
        Dim Dv As New DataView
        Dv = New DataView(DT1, "FormName='" & sFormName & "' ", "DisplayIndex", DataViewRowState.CurrentRows)
        DT = Dv.ToTable()

        If DT.Rows.Count = 0 Then
            Dim DtG As New DataTable
            DtG.Columns.Add("FormName")
            DtG.Columns.Add("ColumnName")
            DtG.Columns.Add("DisplayIndex")
            DtG.Columns.Add("DisplayText")
            DtG.Columns.Add("Width")
            DtG.Columns.Add("IsVisible")
            For cnt As Integer = 0 To DGrid.ColumnCount - 1
                'If DGrid.Columns(cnt).Visible = True Then
                DtG.Rows.Add(sFormName, DGrid.Columns(cnt).Name, DGrid.Columns(cnt).DisplayIndex, DGrid.Columns(cnt).HeaderText, DGrid.Columns(cnt).Width, DGrid.Columns(cnt).Visible)
                'I = I + 1
                'End If
            Next
            Insert_DataGrid_Column_Setting(DtG)
            GoTo lbl
        End If
        'gtdChild.VisibleColumns.Clear()

        'For Each Dr As DataRow In DT.Rows
        '    DGrid.Columns(Dr("ColumnName").ToString).HeaderText = Dr("DisplayText").ToString
        '    DGrid.Columns(Dr("ColumnName").ToString).Width = Dr("width")
        'Next
        Return DT
    End Function
    Public Function GFun_SetGridProfileseeting(ByVal DGrid As DataGridView, ByVal sFormName As String) As DataTable
        Dim DT As New DataTable
        Dim DT1 As New DataTable
        'Dim I As Integer
lbl:
        data_access.ParamClear()
        data_access.Cmd_Text = SP_Select_DataGrid_Column_Setting


        DT1 = data_access.FillList()

        Dim Dv As New DataView
        Dv = New DataView(DT1, "FormName='" & sFormName & "'", "DisplayIndex", DataViewRowState.CurrentRows)
        DT = Dv.ToTable()

        If DT.Rows.Count = 0 Then
            Dim DtG As New DataTable
            DtG.Columns.Add("FormName")
            DtG.Columns.Add("ColumnName")
            DtG.Columns.Add("DisplayIndex")
            DtG.Columns.Add("DisplayText")
            DtG.Columns.Add("Width")
            DtG.Columns.Add("IsVisible")
            For cnt As Integer = 0 To DGrid.ColumnCount - 1
                'If DGrid.Columns(cnt).Visible = True Then
                DtG.Rows.Add(sFormName, DGrid.Columns(cnt).Name, DGrid.Columns(cnt).DisplayIndex, DGrid.Columns(cnt).HeaderText, DGrid.Columns(cnt).Width, DGrid.Columns(cnt).Visible)
                'I = I + 1
                'End If
            Next
            Insert_DataGrid_Column_Setting(DtG)
            GoTo lbl
        End If
        'gtdChild.VisibleColumns.Clear()

        'For Each Dr As DataRow In DT.Rows
        '    DGrid.Columns(Dr("ColumnName").ToString).HeaderText = Dr("DisplayText").ToString
        '    DGrid.Columns(Dr("ColumnName").ToString).Width = Dr("width")
        'Next
        Return DT
    End Function
    ''' <summary>
    ''' Insert_DataGrid_Column_Setting
    ''' </summary>
    ''' <remarks>Insert DataGrid Column Setting </remarks>

    Public Sub createtable()
        Try
            data_access.ParamClear()


            data_access.Cmd_Text = "CREATE TABLE Additional_AEL_expo (" &
                                    "uid AUTOINCREMENT, " &
                                    "InsType TEXT, " &
                                    "Symbol VARCHAR(50), " &
                                    "ExpDate DATE, " &
                                    "StrikePrice DOUBLE, " &
                                    "OptType TEXT, " &
                                    "CALevel DOUBLE, " &
                                    "ELMPer DOUBLE, " &
                                    "PRIMARY KEY (uid));"
            data_access.ExecuteNonQuery_AEL()

        Catch ex As Exception
        End Try
    End Sub


    Public Function AEL_Additional_expo() As DataTable
        Try
            data_access.ParamClear()

            Dim selectQuery As String = "SELECT * FROM Additional_AEL_expo"
            data_access.Cmd_Text = selectQuery
            'data_access.Cmd_Text = SP_select_ael_additional_expo
            Return data_access.FillList_sql()
        Catch ex As Exception
            MsgBox("Trading :: AEL_Additional_expo() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Sub Insert_DataGrid_Column_Setting(ByVal dtable As DataTable)
        data_access.ParamClear()
        For Each Dr As DataRow In dtable.Rows
            data_access.AddParam("VarFormName", OleDbType.VarChar, 255, Dr("FormName").ToString)
            data_access.AddParam("VarColumnName", OleDbType.VarChar, 255, Dr("ColumnName").ToString)
            data_access.AddParam("VarDisplayIndex", OleDbType.Integer, 18, Val(Dr("DisplayIndex") & ""))
            data_access.AddParam("VarDisplayText", OleDbType.VarChar, 255, Dr("DisplayText").ToString)
            data_access.AddParam("VarWidth", OleDbType.Integer, 18, Val(Dr("Width") & ""))
            data_access.AddParam("VarVisible", OleDbType.Boolean, 18, CBool(Dr("IsVisible")))
            'data_access.Cmd_Text = SP_Insert_DataGrid_Column_Setting
            'data_access.ExecuteNonQuery()
        Next
        data_access.Cmd_Text = SP_Insert_DataGrid_Column_Setting
        data_access.ExecuteMultiple(6)
    End Sub

    ''' <summary>
    ''' Update_DataGrid_Column_Setting_OnWidthIndex
    ''' </summary>
    ''' <param name="DtColProfile">Return Datatable</param>
    ''' <remarks> Use to update record intp DataGrid_Column_setting table according to passing table </remarks>
    Public Sub Update_DataGrid_Column_Setting_OnWidthIndex(ByVal DtColProfile As DataTable)
        Try
            data_access.ParamClear()
            For Each Dr As DataRow In DtColProfile.Rows
                data_access.AddParam("@VarDisplayIndex", OleDbType.Integer, 18, Dr("DisplayIndex"))
                data_access.AddParam("@VarDisplayText", OleDbType.VarChar, 255, Dr("DisplayText"))
                data_access.AddParam("@VarWidth", OleDbType.Integer, 18, Dr("Width"))
                data_access.AddParam("@IsVisible", OleDbType.Boolean, 18, CBool(Dr("IsVisible")))
                data_access.AddParam("@VarFormName", OleDbType.VarChar, 255, Dr("FormName"))
                data_access.AddParam("@VarColumnName", OleDbType.VarChar, 255, Dr("ColumnName"))
            Next
            data_access.Cmd_Text = SP_Update_DataGrid_Column_Setting
            data_access.ExecuteMultiple(6)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub Insert_tblRangeData(ByVal DtRangeData As DataTable)
        data_access.ParamClear()
        For Each Dr As DataRow In DtRangeData.Rows
            data_access.AddParam("@BDate", OleDbType.Date, 18, CDate(Dr("Date")))
            data_access.AddParam("@Symbol", OleDbType.VarChar, 18, Dr("Symbol"))
            data_access.AddParam("@ClosePrice", OleDbType.VarChar, 18, Dr("ClosePrice"))
            data_access.AddParam("@PreClosePrice", OleDbType.VarChar, 18, Dr("PreClosePrice"))
            data_access.AddParam("@LogReturns", OleDbType.VarChar, 18, Dr("LogReturns"))
            data_access.AddParam("@PreDayVol", OleDbType.VarChar, 18, Dr("PreDayVol"))
            data_access.AddParam("@CurDayVol", OleDbType.VarChar, 18, Dr("CurDayVol"))
            data_access.AddParam("@AnnualVol", OleDbType.VarChar, 18, Dr("AnnualVol"))
            data_access.AddParam("@FutClosePrice", OleDbType.VarChar, 18, Dr("FutClosePrice"))
            data_access.AddParam("@FutPreDayClose", OleDbType.VarChar, 18, Dr("FutPreDayClose"))
            data_access.AddParam("@FutLogReturns", OleDbType.VarChar, 18, Dr("FutLogReturns"))
            data_access.AddParam("@PreDayFutVol", OleDbType.VarChar, 18, Dr("PreDayFutVol"))
            data_access.AddParam("@CurDayFutVol", OleDbType.VarChar, 18, Dr("CurDayFutVol"))
            data_access.AddParam("@FutAnnualVol", OleDbType.VarChar, 18, Dr("FutAnnualVol"))
            data_access.AddParam("@AppliDailyVol", OleDbType.VarChar, 18, Dr("AppliDailyVol"))
            data_access.AddParam("@AppliAnnualVol", OleDbType.VarChar, 18, Dr("AppliAnnualVol"))

        Next
        data_access.Cmd_Text = Sp_Insert_RangeData
        data_access.ExecuteMultiple(16)
    End Sub

    Public Sub Update_tblRangeData(ByVal DtRangeData As DataTable)
        Try
            data_access.ParamClear()
            For Each Dr As DataRow In DtRangeData.Rows
                data_access.AddParam("@BDATE", OleDbType.Date, 18, Dr("Date"))
                data_access.AddParam("@SYMBOL", OleDbType.VarChar, 18, Dr("Symbol"))
                data_access.AddParam("@CLOSEPRICE", OleDbType.VarChar, 18, Dr("ClosePrice"))
                data_access.AddParam("@PRECLOSEPRICE", OleDbType.VarChar, 18, Dr("PreClosePrice"))
                data_access.AddParam("@LOGRETURNS", OleDbType.VarChar, 18, Dr("LogReturns"))
                data_access.AddParam("@PREDAYVOL", OleDbType.VarChar, 18, Dr("PreDayVol"))
                data_access.AddParam("@CURDAYVOL", OleDbType.VarChar, 18, Dr("CurDayVol"))
                data_access.AddParam("@ANNUALVOL", OleDbType.VarChar, 18, Dr("AnnualVol"))
                data_access.AddParam("@FUTCLOSEPRICE", OleDbType.VarChar, 18, Dr("FutClosePrice"))
                data_access.AddParam("@FUTPREDAYCLOSE", OleDbType.VarChar, 18, Dr("FutPreDayClose"))
                data_access.AddParam("@FUTLOGRETURNS", OleDbType.VarChar, 18, Dr("FutLogReturns"))
                data_access.AddParam("@PREDAYFUTVOL", OleDbType.VarChar, 18, Dr("PreDayFutVol"))
                data_access.AddParam("@CURDAYFUTVOL", OleDbType.VarChar, 18, Dr("CurDayFutVol"))
                data_access.AddParam("@FUTANNUALVOL", OleDbType.VarChar, 18, Dr("FutAnnualVol"))
                data_access.AddParam("@AppliDailyVol", OleDbType.VarChar, 18, Dr("AppliDailyVol"))
                data_access.AddParam("@AppliAnnualVol", OleDbType.VarChar, 18, Dr("AppliAnnualVol"))
            Next
            data_access.Cmd_Text = Sp_Update_RangeData
            data_access.ExecuteMultiple(16)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Function Select_tblRangeData() As DataTable
        Dim dt As New DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = "select * from tblRange"
            data_access.cmd_type = CommandType.Text
            dt = data_access.FillList()
            data_access.cmd_type = CommandType.StoredProcedure
            'data_access.ExecuteMultiple(16)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return dt
    End Function

#End Region
End Class
