Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL



Public Class UserDefTag

    Private Const SP_Select_Symbol As String = "Select_Symbol"

    Private Const SP_update_analysis_company As String = "update_analysis_company"
    Private Const SP_update_trading_company As String = "update_trading_company"
    Private Const SP_update_currency_trading_company As String = "update_currency_trading_company"
    Private Const SP_update_equity_trading_company As String = "update_equity_trading_company"
    Private Const SP_update_expense_data_company As String = "update_expense_data_company"

    Public sTagName As String
    Public bIsValid As Boolean = False

    Public Function Select_Symbol() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Select_Symbol
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function


    Public Sub Update_Symbol(ByVal Oldcompany As String, ByVal Newcompany As String)
        Try
            data_access.ParamClear()
            data_access.AddParam("@Company", OleDbType.VarChar, 31, Newcompany)
            data_access.AddParam("@OldCompany", OleDbType.VarChar, 31, Oldcompany)
            data_access.Cmd_Text = SP_update_analysis_company
            data_access.ExecuteNonQuery()

            data_access.ParamClear()
            data_access.AddParam("@Company", OleDbType.VarChar, 31, Newcompany)
            data_access.AddParam("@OldCompany", OleDbType.VarChar, 31, Oldcompany)
            data_access.Cmd_Text = SP_update_trading_company
            data_access.ExecuteNonQuery()

            data_access.ParamClear()
            data_access.AddParam("@Company", OleDbType.VarChar, 31, Newcompany)
            data_access.AddParam("@OldCompany", OleDbType.VarChar, 31, Oldcompany)
            data_access.Cmd_Text = SP_update_currency_trading_company
            data_access.ExecuteNonQuery()

            data_access.ParamClear()
            data_access.AddParam("@Company", OleDbType.VarChar, 31, Newcompany)
            data_access.AddParam("@OldCompany", OleDbType.VarChar, 31, Oldcompany)
            data_access.Cmd_Text = SP_update_equity_trading_company
            data_access.ExecuteNonQuery()

            data_access.ParamClear()
            data_access.AddParam("@Company", OleDbType.VarChar, 31, Newcompany)
            data_access.AddParam("@OldCompany", OleDbType.VarChar, 31, Oldcompany)
            data_access.Cmd_Text = SP_update_expense_data_company
            data_access.ExecuteNonQuery()



        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    

End Class
