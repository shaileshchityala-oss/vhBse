Option Strict On
Option Explicit On


Public Class CExposureMarginCalculator

    ' ================= DEPENDENCIES =================
    Private ReadOnly GdtBhavcopy As DataTable
    Private ReadOnly mTblExposureDb As DataTable
    Private ReadOnly mTblAelContracts As DataTable

    ' ================= CONSTANTS =================
    Private Const IDX_CALL_OTM_MULT As Decimal = 1.1D
    Private Const IDX_PUT_OTM_MULT As Decimal = 0.9D
    Private Const STK_CALL_OTM_MULT As Decimal = 1.3D
    Private Const STK_PUT_OTM_MULT As Decimal = 0.7D

    ' ================= INDEX MARGINS =================
    Public ReadOnly INDEX_OTM_OPTION As Decimal
    Public ReadOnly INDEX_OTH_OPTION As Decimal
    Public ReadOnly INDEX_FAR_MONTH_OPTION As Decimal

    ' ================= CONSTRUCTOR =================
    Public Sub New(
        bhavcopy As DataTable,
        exposureDb As DataTable,
        aelContracts As DataTable,
        idxOtm As Decimal,
        idxOth As Decimal,
        idxFar As Decimal
    )
        GdtBhavcopy = bhavcopy
        mTblExposureDb = exposureDb
        mTblAelContracts = aelContracts
        INDEX_OTM_OPTION = idxOtm
        INDEX_OTH_OPTION = idxOth
        INDEX_FAR_MONTH_OPTION = idxFar
    End Sub

    ' ================= PUBLIC API =================
    Public Function GetExposure(
        calcOption As Integer,
        baseMargin As Decimal,
        script As String,
        symbol As String,
        expiry As Date,
        strike As Double,
        optType As String
    ) As Decimal

        Select Case calcOption
            Case 1
                Return baseMargin

            Case 2
                Return CalculateSpanExposure(script, symbol, expiry, strike, optType)

            Case 3
                Return GetAelMargin(script, symbol, expiry, strike, optType)

            Case Else
                Return baseMargin
        End Select

    End Function

    ' ================= CORE LOGIC =================
    Private Function CalculateSpanExposure(
        script As String,
        symbol As String,
        expiry As Date,
        strike As Double,
        optType As String
    ) As Decimal

        If GdtBhavcopy.Rows.Count = 0 Then Return 0D

        Dim bhavDate As Date =
            CDate(GdtBhavcopy.Compute("MAX(entry_date)", String.Empty))

        Dim ltp As Decimal = GetLtp(symbol, bhavDate)
        Dim farMonth As Boolean = IsFarMonth(expiry)

        ' ---------- FUT ----------
        If optType = "F" Then
            If script.Substring(3, 3) = "IDX" Then
                Return INDEX_OTH_OPTION
            End If
            Return GetDbMargin(symbol, "OTH")
        End If

        ' ---------- INDEX OPTIONS ----------
        If script.Substring(3, 3) = "IDX" Then
            If farMonth Then Return INDEX_FAR_MONTH_OPTION

            If optType = "C" Then
                Return If(CDec(strike) >= ltp * IDX_CALL_OTM_MULT,
                          INDEX_OTM_OPTION,
                          INDEX_OTH_OPTION)

            ElseIf optType = "P" Then
                Return If(CDec(strike) <= ltp * IDX_PUT_OTM_MULT,
                          INDEX_OTM_OPTION,
                          INDEX_OTH_OPTION)
            End If
        End If

        ' ---------- STOCK OPTIONS ----------
        If script.Substring(3, 3) = "STK" Then
            If optType = "C" Then
                Return If(CDec(strike) >= ltp * STK_CALL_OTM_MULT,
                          GetDbMargin(symbol, "OTM"),
                          GetDbMargin(symbol, "OTH"))

            ElseIf optType = "P" Then
                Return If(CDec(strike) <= ltp * STK_PUT_OTM_MULT,
                          GetDbMargin(symbol, "OTM"),
                          GetDbMargin(symbol, "OTH"))
            End If
        End If

        Return 0D
    End Function

    ' ================= HELPERS =================
    Private Function GetLtp(symbol As String, bhavDate As Date) As Decimal
        Dim rows() As DataRow =
            GdtBhavcopy.Select(
                $"symbol='{symbol}' AND option_type='XX' AND entry_date='{bhavDate:yyyy-MM-dd}'"
            )

        If rows.Length = 0 Then Return 0D
        Return CDec(rows(0)("ltp"))
    End Function

    Private Function IsFarMonth(expiry As Date) As Boolean
        Return DateDiff(DateInterval.Month, Today, expiry) >= 9
    End Function

    Private Function GetDbMargin(symbol As String, insType As String) As Decimal
        Dim rows() As DataRow =
            mTblExposureDb.Select($"symbol='{symbol}' AND InsType='{insType}'")

        If rows.Length = 0 Then Return 0D
        Return CDec(rows(0)("Total_Margin"))
    End Function

    Private Function GetAelMargin(
        script As String,
        symbol As String,
        expiry As Date,
        strike As Double,
        optType As String
    ) As Decimal

        Dim ot As String = optType
        If ot = "C" Then ot = "CE"
        If ot = "P" Then ot = "PE"
        If ot = "F" Then ot = "XX"

        Dim rows() As DataRow =
            mTblAelContracts.Select(
                $"InsType='{script.Substring(0, 6)}' AND " &
                $"symbol='{symbol}' AND StrikePrice='{strike}' AND " &
                $"option_type='{ot}' AND ExpDate=#{expiry:MM/dd/yyyy}#"
            )

        If rows.Length = 0 Then Return 0D
        Return CDec(rows(0)("ELMPer"))
    End Function

End Class

Public Class ExposureMarginApplier

    ' ===== Dependencies =====
    Private ReadOnly _exposureComp As DataTable
    Private ReadOnly _spanOutput As DataTable
    Private ReadOnly _exposureDb As DataTable
    Private ReadOnly _settings As DataTable
    Private ReadOnly _calculator As ExposureMarginCalculator

    ' ===== Constructor =====
    Public Sub New(
        exposureComp As DataTable,
        spanOutput As DataTable,
        exposureDb As DataTable,
        settings As DataTable,
        calculator As ExposureMarginCalculator
    )
        _exposureComp = exposureComp
        _spanOutput = spanOutput
        _exposureDb = exposureDb
        _settings = settings
        _calculator = calculator
    End Sub

    ' ===== Public API =====
    Public Sub Apply(dtable As DataTable)

        Dim defaultExpoMargin As Decimal =
            CDec(_settings.Compute(
                "MAX(SettingKey)",
                "SettingName='DEFAULT_EXPO_MARGIN'"
            )) / 100D

        For Each drow As DataRow In
            dtable.Select("(cp='F') OR (cp<>'F' AND units < 0) AND cp <> 'E'")

            ProcessRow(drow, defaultExpoMargin)

        Next

    End Sub

    ' ===== Core Processing =====
    Private Sub ProcessRow(
        drow As DataRow,
        defaultExpoMargin As Decimal
    )

        Dim company As String = drow("company").ToString()
        Dim symbol As String = GetSymbol(company)
        Dim cp As String = drow("cp").ToString().ToUpperInvariant()
        Dim units As Decimal = CDec(drow("units"))
        Dim expiry As Date = CDate(drow("mdate"))
        Dim strike As Double = CDbl(drow("strikes"))
        Dim isCurrency As Boolean = CBool(drow("isCurrency"))
        Dim futOpt As String = If(cp = "F", "FUT", "OPT")

        Dim compRows() As DataRow =
            _exposureComp.Select(
                $"CompName='{symbol}' AND fut_opt='{futOpt}'" &
                If(futOpt = "FUT", $" AND mat_month={expiry.Month}", "")
            )

        If compRows.Length = 0 Then Exit Sub

        Dim baseMargin As Decimal =
            CDec(_exposureDb.Compute(
                "SUM(exposure_margin)",
                $"compname='{symbol}'"
            ))

        Dim finalMargin As Decimal = baseMargin

        If Not isCurrency Then
            finalMargin =
                _calculator.GetExposure(
                    AELOPTION,
                    baseMargin,
                    drow("script").ToString(),
                    symbol,
                    expiry,
                    strike,
                    cp
                )
        End If

        Dim marginRate As Decimal =
            If(finalMargin > 0D, finalMargin / 100D, defaultExpoMargin)

        ApplyToSpan(company, units, marginRate, compRows)

    End Sub

    ' ===== Span Accumulation =====
    Private Sub ApplyToSpan(
        company As String,
        units As Decimal,
        marginRate As Decimal,
        compRows() As DataRow
    )

        Dim qty As Decimal = Math.Abs(units)

        For Each spanRow As DataRow In
            _spanOutput.Select($"clientcode='{company}'")

            For Each compRow As DataRow In compRows
                spanRow("exposure_margin") =
                    CDec(spanRow("exposure_margin")) +
                    (CDec(compRow("p")) * qty * marginRate)
            Next

        Next

    End Sub

End Class
