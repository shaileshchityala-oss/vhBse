Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL
Public Class Mastergenrate
#Region "SP"
    Private Const SP_Contract_Delete As String = "contract_delete"
    Private Const SP_Contract_Insert As String = "contract_insert"

    Private Const SP_Security_Delete As String = "delete_security"
    Private Const SP_Security_Insert As String = "Security_insert"

    Private Const SP_Delete_Currency_Contract As String = "Delete_Currency_Contract"
    Private Const SP_Insert_Currency_Contract As String = "Insert_Currency_Contract"

#End Region
#Region "Method"
    Public Sub insert(ByVal dtable As DataTable)
        data_access.ParamClear()
        data_access.Cmd_Text = SP_Contract_Delete
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@token", OleDbType.Integer, 20, CInt(drow("Token")))
            '=============================keval(16-2-10)
            data_access.AddParam("@asset_tokan", OleDbType.Integer, 20, CInt(drow("Asset_Tokan")))
            '=============================
            data_access.AddParam("@instrumentname", OleDbType.VarChar, 50, CStr(drow("InstrumentName")))
            data_access.AddParam("@symbol", OleDbType.VarChar, 50, CStr(drow("Symbol")))
            data_access.AddParam("@siries", OleDbType.VarChar, 50, CStr(drow("Siries")))
            data_access.AddParam("@expiry_date", OleDbType.Integer, 20, CInt(drow("ExpiryDate")))
            data_access.AddParam("@strike_price", OleDbType.Double, 20, CDbl(drow("StrikePrice")))
            data_access.AddParam("@option_type", OleDbType.VarChar, 50, CStr(drow("OptionType")))
            data_access.AddParam("@script", OleDbType.VarChar, 50, CStr(drow("script")))
            data_access.AddParam("@lotsize", OleDbType.Integer, 20, CInt(drow("lotsize")))
        Next
        data_access.Cmd_Text = SP_Contract_Insert
        '=================keval(16-2-10) changed(9-10) no in function
        data_access.ExecuteMultiple(10)
    End Sub

    REM By Viral on 11-July-11
    Public Sub insert(ByVal dtable As DataTable, ByVal isDirect As Boolean)
        Dim date1 As Date = "1/1/1980"
        data_access.ParamClear()
        data_access.Cmd_Text = SP_Contract_Delete
        data_access.ExecuteNonQuery()
        Try
            data_access.ParamClear()
            For Each drow As DataRow In dtable.Rows
                data_access.AddParam("@token", OleDbType.Integer, 20, Val(drow("Token") & ""))
                data_access.AddParam("@asset_tokan", OleDbType.Integer, 20, Val(drow("Asset_Tokan") & ""))
                data_access.AddParam("@instrumentname", OleDbType.VarChar, 50, CStr(IIf(IsDBNull(drow("InstrumentName")), "", drow("InstrumentName"))))
                data_access.AddParam("@symbol", OleDbType.VarChar, 50, CStr(IIf(IsDBNull(drow("Symbol")), "", drow("Symbol"))))
                data_access.AddParam("@siries", OleDbType.VarChar, 50, CStr(IIf(IsDBNull(drow("Siries")), "", drow("Siries"))))
                data_access.AddParam("@expiry_date", OleDbType.Integer, 20, Val(drow("ExpiryDate") & ""))
                data_access.AddParam("@strike_price", OleDbType.Double, 20, Val(drow("StrikePrice") & ""))
                data_access.AddParam("@option_type", OleDbType.VarChar, 50, CStr(IIf(IsDBNull(drow("OptionType")), "", drow("OptionType"))))
                If Not IsDBNull(drow("OptionType")) Then
                    If Mid(UCase(drow("OptionType")), 1, 1) = "C" Or Mid(UCase(drow("OptionType")), 1, 1) = "P" Then
                        drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, Val(drow("ExpiryDate") & ""), date1), "ddMMMyyyy") & "  " & CStr(Format(Val(drow("StrikePrice")), "###0.00")) & "  " & drow("OptionType")
                    Else
                        drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, Val(drow("ExpiryDate") & ""), date1), "ddMMMyyyy")
                    End If
                Else
                    drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, Val(drow("ExpiryDate") & ""), date1), "ddMMMyyyy")
                End If
                data_access.AddParam("@script", OleDbType.VarChar, 50, CStr(drow("script")).ToUpper)
                data_access.AddParam("@lotsize", OleDbType.Integer, 20, Val(drow("lotsize") & ""))
            Next
            data_access.Cmd_Text = SP_Contract_Insert
            data_access.ExecuteMultiple(10)
        Catch ex As Exception
            'MsgBox("gggg")
        End Try
    End Sub

    Public Sub Equity_insert(ByVal dtable As DataTable)
        data_access.ParamClear()
        data_access.Cmd_Text = SP_Security_Delete
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@token", OleDbType.Integer, 20, CInt(drow("Token")))
            data_access.AddParam("@symbol", OleDbType.VarChar, 50, CStr(drow("Symbol")))
            data_access.AddParam("@series", OleDbType.VarChar, 50, CStr(drow("series")))
            data_access.AddParam("@isin", OleDbType.VarChar, 50, CStr(drow("isin")))
            data_access.AddParam("@script", OleDbType.VarChar, 50, CStr(drow("script")))
        Next

        data_access.Cmd_Text = SP_Security_Insert
        '==============================keval(16-2-10) 5 -6 in argument
        data_access.ExecuteMultiple(5)
    End Sub

    Public Sub Insert_Currency_Contract(ByVal DtCurrency As DataTable)
        data_access.ParamClear()
        data_access.Cmd_Text = SP_Delete_Currency_Contract
        data_access.ExecuteNonQuery()
        data_access.ParamClear()
        For Each drow As DataRow In DtCurrency.Rows
            data_access.AddParam("@token", OleDbType.Integer, 20, CInt(drow("Token")))
            '=============================keval(16-2-10)
            data_access.AddParam("@asset_tokan", OleDbType.Integer, 20, CInt(drow("Asset_Tokan")))
            '=============================
            data_access.AddParam("@instrumentname", OleDbType.VarChar, 50, CStr(drow("InstrumentName")))
            data_access.AddParam("@symbol", OleDbType.VarChar, 50, CStr(drow("Symbol")))
            data_access.AddParam("@siries", OleDbType.VarChar, 50, CStr(drow("Siries")))
            data_access.AddParam("@expiry_date", OleDbType.Integer, 20, CInt(drow("ExpiryDate")))
            data_access.AddParam("@strike_price", OleDbType.Double, 20, CDbl(drow("StrikePrice")))
            data_access.AddParam("@option_type", OleDbType.VarChar, 50, CStr(drow("OptionType")))
            data_access.AddParam("@script", OleDbType.VarChar, 50, CStr(drow("script")))
            data_access.AddParam("@lotsize", OleDbType.Integer, 20, CInt(drow("lotsize")))
            data_access.AddParam("@multiplier", OleDbType.Integer, 20, CInt(drow("multiplier")))
        Next
        data_access.Cmd_Text = SP_Insert_Currency_Contract
        '=================keval(16-2-10) changed(9-10) no in function
        data_access.ExecuteMultiple(11)
    End Sub
#End Region
End Class
