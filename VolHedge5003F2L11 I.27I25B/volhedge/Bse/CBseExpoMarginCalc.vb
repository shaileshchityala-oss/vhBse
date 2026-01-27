Option Strict On
Option Explicit On

Imports System.Data

Public Class CBseExpMargCalc

#Region "Dependencies (Injected)"


    Public mSpanReader As CSpanReader
    Private ReadOnly mCurTable As DataTable
    Private ReadOnly mTblExposureComp As DataTable
    Private ReadOnly mTblSpanOutput As DataTable
    Private ReadOnly mTblExposureDatabase As DataTable
    Private ReadOnly GdtSettings As DataTable

    Private ReadOnly AELOPTION As Integer

#End Region

#Region "Constructor"

    Public Sub New(curTable As DataTable,
                   exposureComp As DataTable,
                   spanOutput As DataTable,
                   exposureDatabase As DataTable,
                   settings As DataTable,
                   aeOption As Integer)

        mCurTable = curTable
        mTblExposureComp = exposureComp
        mTblSpanOutput = spanOutput
        mTblExposureDatabase = exposureDatabase
        GdtSettings = settings
        AELOPTION = aeOption

    End Sub

#End Region

#Region "Internal Model"

    Private Class PositionSlot
        Public Qty As Integer
        Public SetQty As Integer
        Public RemQty As Integer
        Public Month As Integer
    End Class

#End Region

#Region "Public Entry Point"

    Public Sub Calculate()

        Dim dv As DataView = mCurTable.DefaultView
        dv.RowFilter = "cp='F'"
        dv.Sort = "company, mdate"

        Dim i As Integer = 0

        While i < dv.Count

            Dim row1 As DataRowView = dv(i)
            Dim comp As String = GetSymbol(row1("company").ToString())

            Dim nearExpiry As Boolean =
                (UDDateDiff(DateInterval.Day, Today.Date, CDate(row1("mdate"))) <= 2)

            Dim p1 As PositionSlot = CreateSlot(row1)
            i += 1

            Dim p2 As PositionSlot = Nothing
            Dim p3 As PositionSlot = Nothing

            If i < dv.Count AndAlso GetSymbol(dv(i)("company").ToString()) = comp Then
                p2 = CreateSlot(dv(i))
                OffsetPair(p1, p2, nearExpiry)
                i += 1
            End If

            If i < dv.Count AndAlso GetSymbol(dv(i)("company").ToString()) = comp Then
                p3 = CreateSlot(dv(i))
                OffsetTriple(p1, p2, p3, nearExpiry)
                i += 1
            End If

            ApplyExposure(comp, row1, p1, 0R)

            If p2 IsNot Nothing Then
                ApplyExposure(comp, row1, p2, 1 / 3)
            End If

            If p3 IsNot Nothing Then
                ApplyExposure(comp, row1, p3, 1 / 3)
            End If

        End While

    End Sub

#End Region

#Region "Slot Creation"

    Private Function CreateSlot(r As DataRowView) As PositionSlot
        Return New PositionSlot With {
            .Qty = CInt(r("units")),
            .RemQty = CInt(r("units")),
            .SetQty = 0,
            .Month = CDate(r("mdate")).Month
        }
    End Function

#End Region

#Region "Offset Logic"

    Private Sub OffsetPair(p1 As PositionSlot,
                           p2 As PositionSlot,
                           nearExpiry As Boolean)

        If (p1.RemQty > 0 AndAlso p2.Qty > 0) OrElse
           (p1.RemQty < 0 AndAlso p2.Qty < 0) OrElse
           nearExpiry Then

            p2.RemQty = p2.Qty
            Exit Sub
        End If

        p2.SetQty = Math.Min(Math.Abs(p1.RemQty), Math.Abs(p2.Qty))

        p1.RemQty -= Math.Sign(p1.RemQty) * p2.SetQty
        p2.RemQty = p2.Qty - Math.Sign(p2.Qty) * p2.SetQty

    End Sub

    Private Sub OffsetTriple(p1 As PositionSlot,
                             p2 As PositionSlot,
                             p3 As PositionSlot,
                             nearExpiry As Boolean)

        If (p2.RemQty > 0 AndAlso p3.Qty > 0) OrElse
           (p2.RemQty < 0 AndAlso p3.Qty < 0) Then

            p3.RemQty = p3.Qty
            Exit Sub
        End If

        Dim baseQty As Integer =
            If(nearExpiry,
               Math.Abs(p2.RemQty),
               Math.Abs(p1.RemQty + p2.RemQty))

        p3.SetQty = Math.Min(baseQty, Math.Abs(p3.Qty))

        If p1.RemQty <> 0 AndAlso Not nearExpiry Then
            Dim adjust As Integer = Math.Min(Math.Abs(p1.RemQty), p3.SetQty)
            p1.RemQty -= Math.Sign(p1.RemQty) * adjust
            p2.RemQty -= Math.Sign(p2.RemQty) * (p3.SetQty - adjust)
        Else
            p2.RemQty -= Math.Sign(p2.RemQty) * p3.SetQty
        End If

        p3.RemQty = p3.Qty - Math.Sign(p3.Qty) * p3.SetQty

    End Sub

#End Region

#Region "Exposure Margin Application"

    Private Sub ApplyExposure(comp As String,
                            srcRow As DataRowView,
                            slot As PositionSlot,
                            spreadFactor As Double)

        If slot.Qty = slot.RemQty AndAlso slot.SetQty = 0 Then Exit Sub

        Dim expoRow As DataRow =
            mTblExposureComp.Select(
                $"CompName='{comp}' AND fut_opt='fut' AND mat_month={slot.Month}"
            )(0)

        ' Pre-cast values ONCE
        Dim expoP As Double = CDbl(expoRow("p"))
        Dim qtyAbs As Double = Math.Abs(CDbl(slot.Qty))
        Dim remAbs As Double = Math.Abs(CDbl(slot.RemQty))
        Dim setAbs As Double = Math.Abs(CDbl(slot.SetQty))

        For Each spanRow As DataRow In
            mTblSpanOutput.Select($"clientcode='{srcRow("company")}'")

            ' ---- margin lookup
            Dim baseMargin As Object =
                mTblExposureDatabase.Compute(
                    "SUM(exposure_margin)",
                    $"compname='{comp}'"
                )

            If Not CBool(srcRow("isCurrency")) Then
                baseMargin = mSpanReader.GetExposureObject(
                    AELOPTION,
                    baseMargin,
                    srcRow("script").ToString(),
                    comp,
                    CDate(srcRow("mdate")),
                    CDbl(srcRow("strikes")),
                    srcRow("cp").ToString().ToUpper()
                )
            End If

            Dim marginRate As Double =
                If(IsDBNull(baseMargin),
                   CDbl(GdtSettings.Compute("max(SettingKey)", "SettingName='DEFAULT_EXPO_MARGIN'")) / 100,
                   CDbl(baseMargin) / 100)

            ' ---- STRICT-SAFE arithmetic
            Dim curExpo As Double = CDbl(spanRow("exposure_margin"))

            curExpo -= expoP * qtyAbs * marginRate
            curExpo += expoP * remAbs * marginRate

            If slot.SetQty > 0 Then
                curExpo += spreadFactor * expoP * setAbs * marginRate
            End If

            spanRow("exposure_margin") = curExpo

        Next

    End Sub


#End Region

#Region "External Functions (Expected to Exist)"

    ' These are assumed to exist in your project.
    ' They are NOT reimplemented to preserve exact behavior.

    'Private Function GetSymbol(input As String) As String
    '    Throw New NotImplementedException()
    'End Function

    'Private Function UDDateDiff(interval As DateInterval, d1 As Date, d2 As Date) As Long
    '    Throw New NotImplementedException()
    'End Function

    'Private Function GetExposureObject(aeOpt As Integer,
    '                                   baseMargin As Object,
    '                                   script As String,
    '                                   comp As String,
    '                                   mdate As Date,
    '                                   strike As Double,
    '                                   cp As String) As Object
    '    Throw New NotImplementedException()
    'End Function

#End Region

End Class
