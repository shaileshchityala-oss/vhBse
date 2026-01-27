Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL
Public Class deletedata
#Region "variable"
    Dim _loginid As String
    Dim _entrydate As Date
    Dim objana As New analysisprocess
    Dim ObjSceDetail As New scenarioDetail
#End Region
#Region "Property"
    Public Property Loginid() As String
        Get
            Return _loginid
        End Get
        Set(ByVal value As String)
            _loginid = value
        End Set
    End Property
    Public Property Entrydate() As Date
        Get
            Return _entrydate
        End Get
        Set(ByVal value As Date)
            _entrydate = value
        End Set
    End Property
#End Region
#Region "SP"
    Private Const SP_delete_volatility As String = "delete_volatility"
    Private Const sp_delete_company_analysis As String = "delete_company_analysis"
    Private Const SP_delete_trading As String = "delete_trading"
    Private Const SP_delete_trading_script As String = "delete_trading_script"
    Private Const SP_delete_security As String = "delete_security"
    Private Const SP_delete_security_script As String = "delete_security_script"
    Private Const SP_delete_prbalance As String = "delete_prbalance"
    Private Const SP_delete_prbalance_company As String = "delete_prBalance_company"
    Private Const SP_delete_patch_expense_company As String = "delete_patch_Expense_company"
    Private Const SP_delete_CFPnL_company As String = "SP_delete_CFPnL_company"



    Private Const SP_delete_equitytrading As String = "delete_equitytrading"
    Private Const SP_delete_Currency_trading As String = "delete_Currency_trading"
    Private Const SP_delete_Currency_trading_Script As String = "delete_Currency_trading_Script"

    Private Const SP_delete_bhavcopy_Date As String = "delete_bhavcopy_Date"
    Private Const SP_delete_bhavcopy As String = "delete_bhavcopy"
    Private Const SP_contract_delete As String = "contract_delete"
    Private Const SP_Insert_deletedata As String = "insert_deletedata"

    Private Const SP_today_trading As String = "delete_today_trading"
    Private Const SP_today_equity As String = "delete_today_equity"
    Private Const SP_today_currency As String = "delete_today_currency"
    Private Const SP_today_Prevbal As String = "delete_prbal_date"
    Private Const Sp_today_CFPnL As String = "Sp_Delete_CFPnL_Date"

    Private Const SP_delete_alertentry As String = "delete_alertentry"

    Private Const SP_delete_patch_expense As String = "Delete_patch_expense"
    Private Const Sp_Delete_CFPnL_All As String = "Sp_Delete_CFPnL_All"

    Private Const SP_SELECT_COMPANY As String = "select_all_company"
    Private Const SP_SELECT_COMPANY_SUMMARY As String = "select_all_company_summary"
    Private Const sp_update_displaycompany As String = "update_displaycompany_analysis"
    Private Const sp_update_eqdisplaycompany As String = "update_eqdisplaycompany_analysis"
    Private Const sp_update_displaySummary As String = "update_displaySummary_analysis"
    Private Const sp_update_eqdisplaySummary As String = "update_eqdisplaySummary_analysis"
#End Region
    Public Sub Insert()


        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_delete_volatility
            data_access.ExecuteNonQuery()

            data_access.ParamClear()
            data_access.Cmd_Text = SP_delete_trading
            data_access.ExecuteNonQuery()

            data_access.ParamClear()
            data_access.Cmd_Text = SP_delete_Currency_trading
            data_access.ExecuteNonQuery()

            data_access.ParamClear()
            data_access.Cmd_Text = sp_delete_company_analysis
            data_access.ExecuteNonQuery()

            'data_access.ParamClear()
            'data_access.Cmd_Text = SP_delete_security
            'data_access.ExecuteNonQuery()


            data_access.ParamClear()
            data_access.Cmd_Text = SP_delete_prbalance
            data_access.ExecuteNonQuery()

            data_access.ParamClear()
            data_access.Cmd_Text = SP_delete_equitytrading
            data_access.ExecuteNonQuery()

            'data_access.ParamClear()
            'data_access.Cmd_Text = SP_delete_bhavcopy
            'data_access.ExecuteNonQuery()


            data_access.ParamClear()
            data_access.Cmd_Text = SP_delete_alertentry
            data_access.ExecuteNonQuery()

            'data_access.ParamClear()
            'data_access.Cmd_Text = SP_contract_delete
            'data_access.ExecuteNonQuery()

            objana.delete_analysis()

            data_access.ParamClear()
            data_access.Cmd_Text = SP_delete_patch_expense
            data_access.ExecuteNonQuery()

            data_access.ParamClear()
            data_access.Cmd_Text = Sp_Delete_CFPnL_All
            data_access.ExecuteNonQuery()

            data_access.ParamClear()
            data_access.AddParam("@login", OleDbType.VarChar, 50, (Loginid))
            data_access.AddParam("@entrydate", OleDbType.Date, 50, (Entrydate))
            data_access.Cmd_Text = SP_Insert_deletedata
            data_access.ExecuteNonQuery()

            '            ObjSceDetail.Delete_scenarioAll()



        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub Delete_datedata(ByVal dt As Date)
        data_access.ParamClear()
        data_access.AddParam("@date1", OleDbType.Date, 50, (dt))
        data_access.Cmd_Text = SP_today_trading
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        data_access.AddParam("@date1", OleDbType.Date, 50, (dt))
        data_access.Cmd_Text = SP_today_equity
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        data_access.AddParam("@date1", OleDbType.Date, 50, (dt))
        data_access.Cmd_Text = SP_today_currency
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        data_access.AddParam("@ExpiryDate", OleDbType.Date, 50, (dt))
        data_access.Cmd_Text = Sp_today_CFPnL
        data_access.ExecuteNonQuery()

        objana.process_data()
    End Sub
    Public Function GetExpiryfo() As DataTable
        Dim slab As String
        'Dim slabno As String
        Dim Eqdt As DataTable
        Try
            slab = "SELECT DISTINCT mdate FROM  trading where month(mdate)< Month(Now());"

            data_access.ParamClear()
            data_access.Cmd_Text = slab
            data_access.cmd_type = CommandType.Text

            '   data_access.ExecuteNonQuery()

            data_access_sql.Cmd_Text = slab
            Eqdt = data_access.FillList()
            data_access.cmd_type = CommandType.StoredProcedure
            Return Eqdt


        Catch ex As Exception
        End Try
    End Function
    Public Function GetExpirycurr() As DataTable
        Dim CUR As String
        'Dim slabno As String
        Dim Eqdt As DataTable
        Try
            ' CUR = "SELECT DISTINCT mdate FROM  Currency_trading ;"
            CUR = " SELECT DISTINCT mdate FROM  Currency_trading where month(mdate)< Month(Now());"
            data_access.ParamClear()
            data_access.Cmd_Text = CUR
            data_access.cmd_type = CommandType.Text

            '   data_access.ExecuteNonQuery()

            data_access_sql.Cmd_Text = CUR
            Eqdt = data_access.FillList()
            data_access.cmd_type = CommandType.StoredProcedure
            Return Eqdt


        Catch ex As Exception
        End Try
    End Function
    Public Function DeleteExpiryFo(ByVal list) As Integer
        Try
            For Each dr As DataRow In list.Rows
                Dim qry As String
                qry = "DELETE  FROM trading WHERE cdate(trading.mdate)=#" & CDate(dr("mdate")) & "#;"
                data_access.ExecuteQuery(qry)
            Next
        Catch ex As Exception
            MsgBox("Error to Delete Data")

        End Try

        MsgBox("Successfully Delete...")
    End Function
    Public Function DeleteExpiryCurr(ByVal list) As Integer

        Try
            For Each dr As DataRow In List.Rows
                Dim qry As String
                qry = "DELETE  FROM Currency_trading WHERE Currency_trading.mdate=#" & CDate(dr("mdate")) & "#;"
                data_access.ExecuteQuery(qry)

            Next
        Catch ex As Exception
            MsgBox("Error to Delete Data")

        End Try

        MsgBox("Successfully Delete...")
    End Function
    Public Sub Delete_todaydata()
        data_access.ParamClear()
        data_access.AddParam("@date1", OleDbType.Date, 50, (Now.Date))
        data_access.Cmd_Text = SP_today_trading
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        data_access.AddParam("@date1", OleDbType.Date, 50, (Now.Date))
        data_access.Cmd_Text = SP_today_equity
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        data_access.AddParam("@date1", OleDbType.Date, 50, (Now.Date))
        data_access.Cmd_Text = SP_today_currency
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        data_access.AddParam("@ExpiryDate", OleDbType.Date, 50, (Now.Date))
        data_access.Cmd_Text = Sp_today_CFPnL
        data_access.ExecuteNonQuery()



        objana.process_data()
    End Sub

    Public Sub Delete_Data_Bhavcopy(ByVal ddate As Date)
        data_access.ParamClear()
        data_access.AddParam("@date1", OleDbType.Date, 50, (ddate))
        data_access.Cmd_Text = SP_delete_bhavcopy_Date
        data_access.ExecuteNonQuery()
    End Sub

    Public Sub Delete_PrevBalance_OnDate(ByVal dt1 As Date)
        data_access.ParamClear()
        data_access.AddParam("Vardate1", OleDbType.Date, 50, dt1)
        data_access.Cmd_Text = SP_today_Prevbal
        data_access.ExecuteNonQuery()
    End Sub
    Public Sub delete_prbalance()
        data_access.ParamClear()
        data_access.Cmd_Text = SP_delete_prbalance
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

    Public Sub Delete_Data_scenario(ByVal Scenario As String)

        ObjSceDetail.ScenarioName = Scenario
        ObjSceDetail.Delete_scenario()

    End Sub
    Public Function Comapany() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_SELECT_COMPANY
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
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
    Public Sub update_Comapany(ByVal chklist As CheckedListBox)
        Try
            For i As Integer = 0 To chklist.Items.Count - 1

                If chklist.GetItemChecked(i) = True Then
                    data_access.ParamClear()
                    data_access.AddParam("@isdisplay", OleDbType.Boolean, 50, True)
                    data_access.AddParam("@company", OleDbType.VarChar, 50, chklist.GetItemText(chklist.Items.Item(i)).Trim)
                    data_access.Cmd_Text = sp_update_displaycompany
                    data_access.ExecuteNonQuery()
                    data_access.Cmd_Text = sp_update_eqdisplaycompany
                    data_access.ExecuteNonQuery()
                Else
                    data_access.ParamClear()
                    data_access.AddParam("@isdisplay", OleDbType.Boolean, 50, False)
                    data_access.AddParam("@company", OleDbType.VarChar, 50, chklist.GetItemText(chklist.Items.Item(i)).Trim)
                    data_access.Cmd_Text = sp_update_displaycompany
                    data_access.ExecuteNonQuery()
                    data_access.Cmd_Text = sp_update_eqdisplaycompany
                    data_access.ExecuteNonQuery()
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
            'Return Nothing
        End Try
    End Sub
    Public Sub update_Comapany_sammary(ByVal chklist As CheckedListBox)
        Try
            For i As Integer = 0 To chklist.Items.Count - 1

                If chklist.GetItemChecked(i) = True Then
                    data_access.ParamClear()
                    data_access.AddParam("@issummary", OleDbType.Boolean, 50, True)
                    data_access.AddParam("@company", OleDbType.VarChar, 50, chklist.GetItemText(chklist.Items.Item(i)).Trim)
                    data_access.Cmd_Text = sp_update_displaySummary
                    data_access.ExecuteNonQuery()
                    data_access.Cmd_Text = sp_update_eqdisplaySummary
                    data_access.ExecuteNonQuery()
                Else
                    data_access.ParamClear()
                    data_access.AddParam("@issummary", OleDbType.Boolean, 50, False)
                    data_access.AddParam("@company", OleDbType.VarChar, 50, chklist.GetItemText(chklist.Items.Item(i)).Trim)
                    data_access.Cmd_Text = sp_update_displaySummary
                    data_access.ExecuteNonQuery()
                    data_access.Cmd_Text = sp_update_eqdisplaySummary
                    data_access.ExecuteNonQuery()
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
            'Return Nothing
        End Try


    End Sub
End Class
