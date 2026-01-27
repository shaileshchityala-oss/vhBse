Imports System.IO
Imports System.Xml
Imports System.Data

Public Class CSpanAelExposureExtractor

    Private ReadOnly mSPAN_path As String
    Private ReadOnly mTbl_exposure_comp As DataTable
    Private ReadOnly mTbl_span_calc As DataTable
    Private ReadOnly mTblSpanOutput As DataTable
    Private ReadOnly mTbl_AEL_Expo_calc As DataTable

    Public Sub New(spanPath As String,
                   exposureComp As DataTable,
                   spanCalc As DataTable,
                   spanOutput As DataTable,
                   aelExpoCalc As DataTable)

        mSPAN_path = spanPath
        mTbl_exposure_comp = exposureComp
        mTbl_span_calc = spanCalc
        mTblSpanOutput = spanOutput
        mTbl_AEL_Expo_calc = aelExpoCalc
    End Sub

    ' =========================================================
    ' ENTRY POINT (REPLACEMENT OF ORIGINAL FUNCTION)
    ' =========================================================
    Public Function ExtractExposureMargin(pExch As String) As Boolean
        Try
            ClearTables()

            Dim spanFile As String = Path.Combine(mSPAN_path, pExch & "output.xml")
            Dim curFile As String = Path.Combine(mSPAN_path, "curoutput.xml")

            If File.Exists(spanFile) Then ParseSpanFile(spanFile)
            If File.Exists(curFile) Then ParseSpanFile(curFile)

            ApplyExposureMargin()
            mTblSpanOutput.AcceptChanges()

            Return True

        Catch ex As Exception
            MsgBox("Error in SpanExposureExtractor :: " & ex.Message)
            Return False
        End Try
    End Function

    ' =========================================================
    ' CORE HELPERS
    ' =========================================================
    Private Sub ClearTables()
        mTbl_exposure_comp.Rows.Clear()
        mTbl_span_calc.Rows.Clear()
        mTblSpanOutput.Rows.Clear()
    End Sub

    Private Function OpenXmlWithRetry(path As String) As XmlTextReader
        While True
            Try
                Return New XmlTextReader(New StreamReader(path))
            Catch
                Threading.Thread.Sleep(100)
            End Try
        End While
    End Function

    Private Sub ParseSpanFile(path As String)
        Using r As XmlTextReader = OpenXmlWithRetry(path)

            MoveToFirstPf(r)

            While r.Read()
                Select Case r.Name
                    Case "phyPf"
                        ReadPhysical(r)

                    Case "futPf"
                        ReadFuture(r)

                    Case "oopPf"
                        ReadOptions(r)

                    Case "portfolio"
                        ReadPortfolio(r)

                    Case "spanFile"
                        Exit While
                End Select
            End While
        End Using
    End Sub

    Private Sub MoveToFirstPf(r As XmlTextReader)
        While r.Read()
            If r.Name = "phyPf" Then Exit While
        End While
    End Sub

    ' =========================================================
    ' SAFE XML READERS (pfId SAFE)
    ' =========================================================
    Private Sub ReadPhysical(r As XmlTextReader)
        r.Read()
        SkipIfPresent(r, "pfId")

        Dim dr As DataRow = mTbl_exposure_comp.NewRow()
        dr("CompName") = ReadRequired(r, "pfCode")
        dr("fut_opt") = "OPT"

        While r.Read()
            If r.Name = "phy" Then
                r.Read()
                SkipIfPresent(r, "cId")
                SkipIfPresent(r, "pe")
                dr("p") = Val(ReadRequired(r, "p"))
                mTbl_exposure_comp.Rows.Add(dr)
            ElseIf r.Name = "phyPf" Then
                Exit While
            End If
        End While
    End Sub

    Private Sub ReadFuture(r As XmlTextReader)
        r.Read()
        SkipIfPresent(r, "pfId")

        Dim comp As String = ReadRequired(r, "pfCode")

        While r.Read()
            If r.Name = "fut" Then
                r.Read()
                Dim dr As DataRow = mTbl_exposure_comp.NewRow()
                dr("CompName") = comp
                dr("fut_opt") = "FUT"

                SkipIfPresent(r, "cId")
                Dim pe As String = ReadRequired(r, "pe")
                dr("mat_month") = CInt(Mid(pe, 5, 2))
                dr("p") = Val(ReadRequired(r, "p"))

                mTbl_exposure_comp.Rows.Add(dr)
            ElseIf r.Name = "futPf" Then
                Exit While
            End If
        End While
    End Sub

    Private Sub ReadOptions(r As XmlTextReader)
        r.Read()
        SkipIfPresent(r, "pfId")

        Dim comp As String = ReadRequired(r, "pfCode")

        While r.Read()
            If r.Name = "series" Then
                r.Read()
                SkipIfPresent(r, "pe")
                SkipIfPresent(r, "v")
                SkipIfPresent(r, "volSrc")

                Dim setl As String = ReadRequired(r, "setlDate")
                Dim exp As String =
                    Mid(setl, 7, 2) &
                    Format(CDate(Mid(setl, 5, 2) & "/01/2000"), "MMM") &
                    Mid(setl, 1, 4)

                AddSpanCalc(comp, "FUT", exp, "")

                While r.Read()
                    If r.Name = "opt" Then
                        r.Read()
                        SkipIfPresent(r, "cId")
                        If ReadRequired(r, "o") = "C" Then
                            Dim strike As String = ReadRequired(r, "k")
                            AddSpanCalc(comp, "CAL", exp, strike)
                            AddSpanCalc(comp, "PUT", exp, strike)
                        End If
                    ElseIf r.Name = "series" Then
                        Exit While
                    End If
                End While

            ElseIf r.Name = "oopPf" Then
                Exit While
            End If
        End While
    End Sub

    Private Sub ReadPortfolio(r As XmlTextReader)
        Dim gotSpan As Boolean = False
        Dim dr As DataRow = Nothing

        While r.Read()
            Select Case r.Name
                Case "firm"
                    gotSpan = False
                    dr = mTblSpanOutput.NewRow()
                    dr("ClientCode") = r.ReadElementString()

                Case "acctId", "acctType", "isCust", "seg",
                     "isNew", "custPortUseLov", "currency",
                     "ledgerBal", "ote", "securities", "lue"
                    r.ReadElementString()

                Case "lfv", "sfv", "lov", "sov"
                    dr(r.Name) = Val(r.ReadElementString())

                Case "spanReq"
                    If Not gotSpan Then
                        dr("spanreq") = Val(ReadRequired(r, "spanReq"))
                        dr("anov") = Val(ReadRequired(r, "anov"))
                        If dr("spanreq") - dr("anov") <= 0 Then
                            dr("spanreq") = 0
                            dr("anov") = 0
                        End If
                        gotSpan = True
                    End If

                Case "portfolio"
                    dr("exposure_margin") = 0
                    mTblSpanOutput.Rows.Add(dr)
                    Exit While
            End Select
        End While
    End Sub

    ' =========================================================
    ' SAFE XML UTILS
    ' =========================================================
    Private Sub SkipIfPresent(r As XmlTextReader, name As String)
        If r.NodeType = XmlNodeType.Element AndAlso r.Name = name Then
            r.ReadElementString()
        End If
    End Sub

    Private Function ReadRequired(r As XmlTextReader, name As String) As String
        If r.NodeType = XmlNodeType.Element AndAlso r.Name = name Then
            Return r.ReadElementString()
        End If
        Return ""
    End Function

    ' =========================================================
    ' BUSINESS LOGIC
    ' =========================================================
    Private Sub AddSpanCalc(comp As String, typ As String, exp As String, strike As String)
        Dim dr As DataRow = mTbl_span_calc.NewRow()
        dr("description") = concat_scrip(comp, typ, exp, strike)
        dr("compname") = comp
        dr("cal_put_fut") = typ
        dr("strike_price") = strike
        dr("exp_date") = exp
        mTbl_span_calc.Rows.Add(dr)
    End Sub

    Private Sub ApplyExposureMargin()
        For Each dr As DataRow In mTblSpanOutput.Rows
            Dim exp As Double = 0
            Dim code As String = dr("ClientCode").ToString()

            For Each r As DataRow In mTbl_AEL_Expo_calc.Select("Company = '" & code & "'")
                exp += CDbl(r("AEL_EXPOSURE"))
            Next

            dr("exposure_margin") = CDbl(dr("exposure_margin")) + exp
        Next
    End Sub

    ' =========================================================
    ' LEGACY CONCAT (UNCHANGED)
    ' =========================================================
    Private Function concat_scrip(comp As String,
                                  typ As String,
                                  exp As String,
                                  strike As String) As String
        If String.IsNullOrEmpty(strike) Then
            Return comp & " " & typ & " " & exp
        Else
            Return comp & " " & typ & " " & exp & " " & strike
        End If
    End Function

End Class

'Dim extractor As New SpanExposureExtractor(
'    mSPAN_path,
'    mTbl_exposure_comp,
'    mTbl_span_calc,
'    mTblSpanOutput,
'    mTbl_AEL_Expo_calc
')

'Dim ok As Boolean = extractor.ExtractExposureMargin("NSE_")
