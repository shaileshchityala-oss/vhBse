Option Strict On
Option Explicit On

Public Class ExposureMarginCalculator

    ' ================= DEPENDENCIES =================
    Private ReadOnly _bhavcopy As DataTable
    Private ReadOnly _exposureDb As DataTable
    Private ReadOnly _aelContracts As DataTable

    ' ================= CONSTANTS =================
    Private Const IDX_CALL_OTM As Decimal = 1.1D
    Private Const IDX_PUT_OTM As Decimal = 0.9D
    Private Const STK_CALL_OTM As Decimal = 1.3D
    Private Const STK_PUT_OTM As Decimal = 0.7D

    ' ================= INDEX MARGINS =================
    Private ReadOnly _idxOtm As Decimal
    Private ReadOnly _idxOth As Decimal
    Private ReadOnly _idxFar As Decimal

    ' ================= CONSTRUCTOR =================
    Public Sub New(
        bhavcopy As DataTable,
        exposureDb As DataTable,
        aelContracts As DataTable,
        idxOtm As Decimal,
        idxOth As Decimal,
        idxFar As Decimal
    )
        _bhavcopy = bhavcopy
        _exposureDb = exposureDb
        _aelContracts = aelContracts
        _idxOtm = idxOtm
        _idxOth = idxOth
        _idxFar = idxFar
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

        If _bhavcopy.Rows.Count = 0 Then Return 0D

        Dim bhavDate As Date =
            CDate(_bhavcopy.Compute("MAX(entry_date)", String.Empty))

        Dim ltp As Decimal = GetLtp(symbol, bhavDate)
        Dim isFarMonth As Boolean = DateDiff(DateInterval.Month, Today, expiry) >= 9

        ' ---------- FUT ----------
        If optType = "F" Then
            If script.Substring(3, 3) = "IDX" Then
                Return _idxOth
            End If
            Return GetDbMargin(symbol, "OTH")
        End If

        ' ---------- INDEX OPTIONS ----------
        If script.Substring(3, 3) = "IDX" Then
            If isFarMonth Then Return _idxFar

            If optType = "C" Then
                Return If(CDec(strike) >= ltp * IDX_CALL_OTM, _idxOtm, _idxOth)
            ElseIf optType = "P" Then
                Return If(CDec(strike) <= ltp * IDX_PUT_OTM, _idxOtm, _idxOth)
            End If
        End If

        ' ---------- STOCK OPTIONS ----------
        If script.Substring(3, 3) = "STK" Then
            If optType = "C" Then
                Return If(CDec(strike) >= ltp * STK_CALL_OTM,
                          GetDbMargin(symbol, "OTM"),
                          GetDbMargin(symbol, "OTH"))
            ElseIf optType = "P" Then
                Return If(CDec(strike) <= ltp * STK_PUT_OTM,
                          GetDbMargin(symbol, "OTM"),
                          GetDbMargin(symbol, "OTH"))
            End If
        End If

        Return 0D
    End Function

    ' ================= HELPERS =================
    Private Function GetLtp(symbol As String, bhavDate As Date) As Decimal
        Dim rows() As DataRow =
            _bhavcopy.Select(
                $"symbol='{symbol}' AND option_type='XX' AND entry_date='{bhavDate:yyyy-MM-dd}'"
            )

        If rows.Length = 0 Then Return 0D
        Return CDec(rows(0)("ltp"))
    End Function

    Private Function GetDbMargin(symbol As String, insType As String) As Decimal
        Dim rows() As DataRow =
            _exposureDb.Select($"symbol='{symbol}' AND InsType='{insType}'")

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
            _aelContracts.Select(
                $"InsType='{script.Substring(0, 6)}' AND " &
                $"symbol='{symbol}' AND StrikePrice='{strike}' AND " &
                $"option_type='{ot}' AND ExpDate=#{expiry:MM/dd/yyyy}#"
            )

        If rows.Length = 0 Then Return 0D
        Return CDec(rows(0)("ELMPer"))
    End Function

End Class
