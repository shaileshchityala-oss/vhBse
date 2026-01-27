Public Enum ETokenType
    None
    Fo_Token
    Eq_Token
    Curr_Token
End Enum

Public Class CContract

    Public mMdbConn As CMdbConnection

    Public Sub New()
        mMdbConn = New CMdbConnection("")
        mMdbConn.mConStr = mMdbConn.ReadGreekConnStr()
    End Sub

    Public Function GetFoSymbolList(pExchange As String) As DataTable
        Dim masterdata As DataTable = cpfmaster
        Dim filter As String = "exchange='" & pExchange & "'"
        Dim dv As DataView = New DataView(masterdata, filter, "symbol", DataViewRowState.CurrentRows)
        Return dv.ToTable(True, "symbol")
    End Function

    Public Function GetEqSymbolList(pExchange As String) As DataTable
        Dim masterdata As DataTable = eqmaster
        Dim filter As String = "exchange='" & pExchange & "'"
        Dim dv As DataView = New DataView(masterdata, filter, "symbol", DataViewRowState.CurrentRows)
        Return dv.ToTable(True, "symbol")
    End Function

    Public Function GetTokenFromScript(pScript As String, pExch As String, pTokenType As ETokenType) As String
        Dim sql As String = ""
        Dim res As String
        If pTokenType = ETokenType.Fo_Token Then
            res = CLng(Val(cpfmaster.Compute("max(token)", "script='" & pScript & "' AND Exchange='" + pExch + "'").ToString))
        ElseIf pTokenType = ETokenType.Eq_Token Then
            res = CLng(Val(eqmaster.Compute("max(token)", "script='" & pScript & "' AND Exchange='" + pExch + "'").ToString))
        Else
            Return ""
        End If
        Return res
    End Function

    Public Function GetExchangeFromToken(pToken As String, pTokenType As ETokenType) As String
        Dim sql As String = ""
        Dim res As String
        If pTokenType = ETokenType.Fo_Token Then
            res = cpfmaster.Compute("max(exchange)", "Token=" & pToken).ToString()
        ElseIf pTokenType = ETokenType.Eq_Token Then
            res = eqmaster.Compute("max(exchange)", "Token=" & pToken).ToString()
        Else
            Return ""
        End If
        Return res

    End Function

    Public Function GetDbExchangeFromToken(pToken As String, pTokenType As ETokenType) As String

        Dim sql As String = ""
        If pTokenType = ETokenType.Fo_Token Then
            sql = "SELECT EXCHANGE FROM CONTRACT WHERE TOKEN=" + pToken
        ElseIf pTokenType = ETokenType.Eq_Token Then
            sql = "SELECT EXCHANGE FROM SECURITY WHERE TOKEN=" + pToken
        Else
            Return ""
        End If
        Return mMdbConn.GetValueStr(sql)
    End Function

    Public Function GetDbTokenFromScript(pScript As String, pExch As String, pTokenType As ETokenType) As String
        Dim sql As String = ""
        If pTokenType = ETokenType.Fo_Token Then
            sql = "SELECT  TOKEN FROM CONTRACT WHERE SCRIPT='" + pScript + "' AND exchange = '" + pExch + "'"
        ElseIf pTokenType = ETokenType.Eq_Token Then
            sql = "SELECT  TOKEN FROM SECURITY WHERE SCRIPT='" + pScript + "' AND exchange = '" + pExch + "'"
        Else
            Return ""
        End If
        Return mMdbConn.GetValueStr(sql)
    End Function


    Public Function GetScriptExchangeFromToken(pToken As String, pTokenType As ETokenType) As String
        Dim resScript As String
        Dim resExchange As String
        If pTokenType = ETokenType.Fo_Token Then
            resExchange = cpfmaster.Compute("max(exchange)", "Token=" & pToken).ToString()
            resScript = cpfmaster.Compute("max(Script)", "Token=" & pToken).ToString()
        ElseIf pTokenType = ETokenType.Eq_Token Then
            resExchange = eqmaster.Compute("max(exchange)", "Token=" & pToken).ToString()
            resScript = eqmaster.Compute("max(Script)", "Token=" & pToken).ToString()
        Else
            Return ""
        End If
        Return resScript + "," + resExchange

    End Function

    Public Function GetScriptFromToken(pToken As String, pTokenType As ETokenType) As String
        Dim resScript As String
        If pTokenType = ETokenType.Fo_Token Then
            resScript = cpfmaster.Compute("max(Script)", "Token=" & pToken).ToString()
        ElseIf pTokenType = ETokenType.Eq_Token Then
            resScript = eqmaster.Compute("max(Script)", "Token=" & pToken).ToString()
        Else
            Return ""
        End If
        Return resScript

    End Function

    Public Function GetScriptObjectFromToken(pToken As String, pTokenType As ETokenType) As script
        Dim res As script = New script()
        Dim rows As DataRow() = cpfmaster.Select("Token=" & pToken, "")
        If pTokenType = ETokenType.Fo_Token Then
            rows = cpfmaster.Select("Token=" & pToken, "")

            For Each r As DataRow In rows
                Dim script As String = r("script")
                Dim token As String = r("Token")
                Dim AssetToken As String = r("Token")
                Dim expiryDate As String = r("expDate1")
                Dim strike As String = r("strike_price")
                Dim Cp As String = r("option_Type")
                Dim exchange As String = r("exchange")
                Dim company As String = r("symbol")
                Dim lotSize As String = r("LotSize")
                Dim ftoken As String = r("ftoken")

                res.Script = script
                res.Company = company
                res.CP = Cp
                res.Token = CUtils.StringToInt(token)
                res.asset_tokan = CUtils.StringToInt(AssetToken)
                res.Exchange = exchange
                res.StrikeRate = CUtils.StringToDouble(strike)
                res.LotSize = CUtils.StringToInt(lotSize)
                res.Mdate = CUtils.StringToDate(expiryDate, "yyyy-MM-dd")
                Return res
                Exit For
            Next


        ElseIf pTokenType = ETokenType.Eq_Token Then
            rows = eqmaster.Select("Token=" & pToken, "")
            For Each r As DataRow In rows
                Dim script As String = r("script")
                Dim token As String = r("Token")
                Dim exchange As String = r("exchange")

                res.Script = script
                res.Token = CUtils.StringToInt(token)
                res.Exchange = exchange
                Return res
                Exit For
            Next
        Else
        End If
        Return Nothing
    End Function

End Class
