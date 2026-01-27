Public Class TradeExpenseCalculator

    ' ===== PUBLIC ENTRY POINT =====
    Public Sub CalculateExpense(
        ByVal DtTrad As DataTable,
        ByVal VarTradeType As String,
        ByVal VarIsAddRow As Boolean
    )

        If DtTrad Is Nothing OrElse DtTrad.Rows.Count = 0 Then Exit Sub

        Try
            Select Case VarTradeType.ToUpper()
                Case "FO"
                    PrepareFOViews()
                    ProcessFO(DtTrad, VarIsAddRow)

                Case "CURR"
                    PrepareCURRViews()
                    ProcessCURR(DtTrad, VarIsAddRow)

                Case "EQ"
                    PrepareEQViews()
                    ProcessEQ(DtTrad, VarIsAddRow)
            End Select

        Catch ex As Exception
            ' TODO: Log exception
        End Try

    End Sub

#Region "===== PREPARE DATAVIEWS ====="

    Private dtBuyFO As DataTable
    Private dtSaleFO As DataTable
    Private dtBuyEQ As DataTable
    Private dtSaleEQ As DataTable
    Private dtBuyCURR As DataTable
    Private dtSaleCURR As DataTable

    Private Sub PrepareFOViews()
        dtBuyFO = New DataView(GdtFOTrades, "qty > 0", "", DataViewRowState.CurrentRows).ToTable()
        dtSaleFO = New DataView(GdtFOTrades, "qty < 0", "", DataViewRowState.CurrentRows).ToTable()
    End Sub

    Private Sub PrepareEQViews()
        dtBuyEQ = New DataView(GdtEQTrades, "qty > 0", "", DataViewRowState.CurrentRows).ToTable()
        dtSaleEQ = New DataView(GdtEQTrades, "qty < 0", "", DataViewRowState.CurrentRows).ToTable()
    End Sub

    Private Sub PrepareCURRViews()
        dtBuyCURR = New DataView(GdtCurrencyTrades, "qty > 0", "", DataViewRowState.CurrentRows).ToTable()
        dtSaleCURR = New DataView(GdtCurrencyTrades, "qty < 0", "", DataViewRowState.CurrentRows).ToTable()
    End Sub

#End Region

#Region "===== PROCESSORS ====="

    Private Sub ProcessFO(ByVal DtTrad As DataTable, ByVal VarIsAddRow As Boolean)

        For Each dr As DataRow In GetDistinctFO(DtTrad)

            If Not VarIsAddRow AndAlso Not TradeExists(GdtFOTrades, dr) Then
                RemoveExpenseRow(dr)
                Continue For
            End If

            Dim expense As Double = CalculateFOExpense(dr)
            AddUpdateExpenseRow(dr("entry_date"), dr("company"), dr("script"), dr("mdate"), expense)

        Next

    End Sub

    Private Sub ProcessCURR(ByVal DtTrad As DataTable, ByVal VarIsAddRow As Boolean)

        For Each dr As DataRow In GetDistinctFO(DtTrad)

            If Not VarIsAddRow AndAlso Not TradeExists(GdtCurrencyTrades, dr) Then
                RemoveExpenseRow(dr)
                Continue For
            End If

            Dim expense As Double = CalculateCURRExpense(dr)
            AddUpdateExpenseRow(dr("entry_date"), dr("company"), dr("script"), dr("mdate"), expense)

        Next

    End Sub

    Private Sub ProcessEQ(ByVal DtTrad As DataTable, ByVal VarIsAddRow As Boolean)

        For Each dr As DataRow In GetDistinctEQ(DtTrad)

            If Not VarIsAddRow AndAlso Not TradeExists(GdtEQTrades, dr) Then
                RemoveExpenseRow(dr)
                Continue For
            End If

            Dim expense As Double = CalculateEQExpense(dr)
            AddUpdateExpenseRow(dr("entry_date"), dr("company"), dr("script"), CDate("01-01-1980"), expense)

        Next

    End Sub

#End Region

#Region "===== CALCULATIONS ====="

    Private Function CalculateFOExpense(ByVal dr As DataRow) As Double

        Dim filter As String = TradeFilter(dr)
        Dim buyVal As Double
        Dim saleVal As Double

        If IsFuture(dr("cp").ToString()) Then
            buyVal = GetSum(dtBuyFO, "tot", filter)
            saleVal = GetSum(dtSaleFO, "tot", filter)
            Return (buyVal * futl) / futlp + (saleVal * futs) / futsp
        End If

        If Val(spl) <> 0 Then
            buyVal = GetSum(dtBuyFO, "tot2", filter)
            saleVal = GetSum(dtSaleFO, "tot2", filter)
            Return (buyVal * spl) / splp + (saleVal * sps) / spsp
        End If

        buyVal = GetSum(dtBuyFO, "tot", filter)
        saleVal = GetSum(dtSaleFO, "tot", filter)
        Return (buyVal * prel) / prelp + (saleVal * pres) / presp

    End Function


    Private Function CalculateCURRExpense(ByVal dr As DataRow) As Double

        Dim filter As String = TradeFilter(dr)
        Dim buyVal As Double
        Dim saleVal As Double

        If IsFuture(dr("cp").ToString()) Then
            buyVal = GetSum(dtBuyCURR, "tot", filter)
            saleVal = GetSum(dtSaleCURR, "tot", filter)
            Return (buyVal * currfutl) / currfutlp + (saleVal * currfuts) / currfutsp
        End If

        If Val(currspl) <> 0 Then
            buyVal = GetSum(dtBuyCURR, "tot2", filter)
            saleVal = GetSum(dtSaleCURR, "tot2", filter)
            Return (buyVal * currspl) / currsplp + (saleVal * currsps) / currspsp
        End If

        buyVal = GetSum(dtBuyCURR, "tot", filter)
        saleVal = GetSum(dtSaleCURR, "tot", filter)
        Return (buyVal * currprel) / currprelp + (saleVal * currpres) / currpresp

    End Function


    Private Function CalculateEQExpense(ByVal dr As DataRow) As Double

        Dim filter As String = TradeFilter(dr)

        Dim buyVal As Double = GetSum(dtBuyEQ, "tot", filter)
        Dim saleVal As Double = GetSum(dtSaleEQ, "tot", filter)

        Dim diff As Double = buyVal - saleVal

        If diff > 0 Then
            Return (diff * dbl) / dblp +
                   (saleVal * ndbs) / ndbsp +
                   (saleVal * ndbl) / ndblp
        Else
            diff = Math.Abs(diff)
            Return (diff * dbs) / dbsp +
                   (buyVal * ndbs) / ndbsp +
                   (buyVal * ndbl) / ndblp
        End If

    End Function

#End Region

#Region "===== HELPERS ====="

    Private Function GetDistinctFO(ByVal dt As DataTable) As DataRow()
        Return dt.DefaultView.ToTable(True, "entry_date", "company", "script", "mdate", "cp").Select()
    End Function

    Private Function GetDistinctEQ(ByVal dt As DataTable) As DataRow()
        Return dt.DefaultView.ToTable(True, "entry_date", "company", "script").Select()
    End Function

    Private Function TradeFilter(ByVal dr As DataRow) As String
        Return $"script='{dr("script")}' AND entry_date=#{fDate(dr("entry_date"))}# AND company='{dr("company")}'"
    End Function

    Private Function TradeExists(ByVal dt As DataTable, ByVal dr As DataRow) As Boolean
        Return dt.Select(TradeFilter(dr)).Length > 0
    End Function

    Private Sub RemoveExpenseRow(ByVal dr As DataRow)
        Dim rows = G_DTExpenseData.Select(TradeFilter(dr))
        If rows.Length > 0 Then G_DTExpenseData.Rows.Remove(rows(0))
    End Sub

    Private Function GetSum(ByVal dt As DataTable, ByVal field As String, ByVal filter As String) As Double
        Return Math.Abs(Val(dt.Compute($"SUM({field})", filter)))
    End Function

    Private Function IsFuture(ByVal cp As String) As Boolean
        cp = cp.Trim().ToUpper()
        Return cp = "" OrElse cp = "X" OrElse cp = "F" OrElse cp = "XX"
    End Function

    Private Sub AddUpdateExpenseRow(
    ByVal VarEntryDate As Date,
    ByVal VarCompany As String,
    ByVal VarScript As String,
    ByVal Exp_Date As Date,
    ByVal VarExpense As Double
)

        Dim filter As String =
            $"entry_date=#{Format(VarEntryDate, "dd-MMM-yyyy")}# " &
            $"AND company='{VarCompany}' AND script='{VarScript}'"

        Dim expRows() As DataRow = G_DTExpenseData.Select(filter)

        If expRows.Length > 0 Then
            expRows(0)("Expense") = VarExpense
        Else
            Dim dr As DataRow = G_DTExpenseData.NewRow()
            dr("Entry_Date") = Format(VarEntryDate, "dd-MMM-yyyy")
            dr("Company") = VarCompany
            dr("Script") = VarScript
            dr("exp_date") = Exp_Date
            dr("Expense") = VarExpense
            G_DTExpenseData.Rows.Add(dr)
        End If

    End Sub

#End Region

End Class
