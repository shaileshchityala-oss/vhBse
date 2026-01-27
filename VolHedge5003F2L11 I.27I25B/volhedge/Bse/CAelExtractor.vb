Imports System.Xml
Imports System.IO
Imports System.Data

Public Class AelSpanXmlReader

    Private ReadOnly expTbl As DataTable
    Private ReadOnly spanCalcTbl As DataTable
    Private ReadOnly spanOutTbl As DataTable

    Public Sub New(exp As DataTable, spanCalc As DataTable, spanOut As DataTable)
        expTbl = exp
        spanCalcTbl = spanCalc
        spanOutTbl = spanOut
    End Sub

    Public Sub ClearTables()
        expTbl.Rows.Clear()
        spanCalcTbl.Rows.Clear()
        spanOutTbl.Rows.Clear()
    End Sub

    ' ================= ENTRY =================
    Public Sub ReadFile(xmlFile As String)

        Using sr As New StreamReader(xmlFile)
            Using r As XmlReader = XmlReader.Create(sr,
                New XmlReaderSettings With {
                    .IgnoreWhitespace = True,
                    .IgnoreComments = True
                })

                While r.Read()
                    If r.NodeType <> XmlNodeType.Element Then Continue While

                    Select Case r.Name
                        Case "phyPf" : ParsePhyPf(r)
                        Case "futPf" : ParseFutPf(r)
                        Case "oopPf" : ParseOopPf(r)
                        Case "portfolio" : ParsePortfolio(r)
                    End Select
                End While

            End Using
        End Using

    End Sub

    ' ================= PHY =================


    Private Sub ParsePhyPf(r As XmlReader)

        Dim compName As String = ""

        While r.Read()

            If r.NodeType = XmlNodeType.Element Then
                Select Case r.Name

                    Case "pfCode"
                        compName = r.ReadElementContentAsString()

                    Case "phy"
                        Dim row As DataRow = expTbl.NewRow()   ' ✅ NEW ROW
                        row("CompName") = compName
                        row("fut_opt") = "OPT"
                        ParsePhy(r, row)
                End Select
            End If

            If r.NodeType = XmlNodeType.EndElement AndAlso r.Name = "phyPf" Then Exit While
        End While

    End Sub


    Private Sub ParsePhyPf1(r As XmlReader)

        Dim row As DataRow = expTbl.NewRow()
        row("fut_opt") = "OPT"

        While r.Read()

            If r.NodeType = XmlNodeType.Element Then
                Select Case r.Name
                    Case "pfId"
                        r.ReadElementContentAsString()

                    Case "pfCode"
                        row("CompName") = r.ReadElementContentAsString()

                    Case "phy"
                        ParsePhy(r, row)
                End Select
            End If

            If r.NodeType = XmlNodeType.EndElement AndAlso r.Name = "phyPf" Then Exit While
        End While
    End Sub



    Private Sub ParsePhy(r As XmlReader, row As DataRow)

        While r.Read()

            If r.NodeType = XmlNodeType.Element Then
                Select Case r.Name
                    Case "cId", "pe"
                        r.ReadElementContentAsString()

                    Case "p"
                        row("p") = Val(r.ReadElementContentAsString())
                End Select
            End If

            If r.NodeType = XmlNodeType.EndElement AndAlso r.Name = "phy" Then
                expTbl.Rows.Add(row)
                Exit While
            End If
        End While
    End Sub

    ' ================= FUT =================
    'Private Sub ParseFutPf(r As XmlReader)

    '    Dim compName As String = ""

    '    While r.Read()

    '        If r.NodeType = XmlNodeType.Element Then
    '            Select Case r.Name
    '                Case "pfId"
    '                    r.ReadElementContentAsString()

    '                Case "pfCode"
    '                    compName = r.ReadElementContentAsString()

    '                Case "fut"
    '                    ParseFut(r, compName)
    '            End Select
    '        End If

    '        If r.NodeType = XmlNodeType.EndElement AndAlso r.Name = "futPf" Then Exit While
    '    End While
    'End Sub

    Private Sub ParseFutPf(r As XmlReader)

        Dim compName As String = Nothing

        While r.Read()

            If r.NodeType = XmlNodeType.Element Then
                Select Case r.Name

                    Case "pfCode"
                        compName = r.ReadElementContentAsString()

                    Case "fut"
                        ParseFut(r, compName)
                End Select
            End If

            If r.NodeType = XmlNodeType.EndElement AndAlso r.Name = "futPf" Then Exit While
        End While

    End Sub

    Private Sub ParseFut(r As XmlReader, compName As String)

        Dim row As DataRow = expTbl.NewRow()
        row("CompName") = compName
        row("fut_opt") = "FUT"

        While r.Read()

            If r.NodeType = XmlNodeType.Element Then
                Select Case r.Name

                    Case "cId"
                        r.ReadElementContentAsString()

                    Case "pe"
                        Dim pe = r.ReadElementContentAsString()
                        row("mat_month") = CInt(Mid(pe, 5, 2))

                    Case "p"
                        row("p") = Val(r.ReadElementContentAsString())
                End Select
            End If

            If r.NodeType = XmlNodeType.EndElement AndAlso r.Name = "fut" Then
                expTbl.Rows.Add(row)
                Exit While
            End If
        End While

    End Sub


    ' ================= OPTIONS =================

    Private Sub ParseOopPf(r As XmlReader)

        Dim compName As String = Nothing

        ' FIRST: get pfCode
        While r.Read()
            If r.NodeType = XmlNodeType.Element AndAlso r.Name = "pfCode" Then
                compName = r.ReadElementContentAsString()
                Exit While
            End If

            If r.NodeType = XmlNodeType.EndElement AndAlso r.Name = "oopPf" Then Exit Sub
        End While

        ' SECOND: process series
        While r.Read()

            If r.NodeType = XmlNodeType.Element AndAlso r.Name = "series" Then
                ParseSeries(r, compName)
            End If

            If r.NodeType = XmlNodeType.EndElement AndAlso r.Name = "oopPf" Then Exit While
        End While

    End Sub


    'Private Sub ParseOopPf(r As XmlReader)

    '    Dim compName As String = ""

    '    While r.Read()

    '        If r.NodeType = XmlNodeType.Element Then
    '            Select Case r.Name
    '                Case "pfId"
    '                    r.ReadElementContentAsString()

    '                Case "pfCode"
    '                    compName = r.ReadElementContentAsString()

    '                Case "series"
    '                    ParseSeries(r, compName)
    '            End Select
    '        End If

    '        If r.NodeType = XmlNodeType.EndElement AndAlso r.Name = "oopPf" Then Exit While
    '    End While
    'End Sub

    ' Private Sub ParseSeries(r As XmlReader, compName As String)

    '     Dim expDate As String = ""

    '     While r.Read()

    '         If r.NodeType = XmlNodeType.Element Then
    '             Select Case r.Name
    '                 Case "setlDate"
    '                     Dim raw = r.ReadElementContentAsString()
    '                     expDate = Mid(raw, 7, 2) &
    '                               Format(CDate(Mid(raw, 5, 2) & "/01/2000"), "MMM") &
    '                               Mid(raw, 1, 4)

    '                 Case "opt"
    '                     ParseOpt(r, compName, expDate)
    '             End Select
    '         End If

    '         If r.NodeType = XmlNodeType.EndElement AndAlso r.Name = "series" Then Exit While
    '     End While
    ' End Sub


    Private Sub ParseSeries(r As XmlReader, compName As String)

        Dim expDate As String = ""

        While r.Read()

            If r.NodeType = XmlNodeType.Element Then
                Select Case r.Name

                    Case "setlDate"
                        Dim raw = r.ReadElementContentAsString()
                        expDate = Mid(raw, 7, 2) &
                                Format(CDate(Mid(raw, 5, 2) & "/01/2000"), "MMM") &
                                Mid(raw, 1, 4)

                        ' 🔥 ORIGINAL FUT ROW
                        AddSpan(compName, "FUT", "", expDate)

                    Case "opt"
                        ParseOpt(r, compName, expDate)
                End Select
            End If

            If r.NodeType = XmlNodeType.EndElement AndAlso r.Name = "series" Then Exit While
        End While
    End Sub


    Private Sub ParseOpt(r As XmlReader, compName As String, expDate As String)

        Dim strike As String = ""
        Dim oType As String = ""

        While r.Read()

            If r.NodeType = XmlNodeType.Element Then
                Select Case r.Name
                    Case "cId"
                        r.ReadElementContentAsString()

                    Case "o"
                        oType = r.ReadElementContentAsString()

                    Case "k"
                        strike = r.ReadElementContentAsString()
                End Select
            End If

            If r.NodeType = XmlNodeType.EndElement AndAlso r.Name = "opt" Then
                If oType = "C" Then
                    AddSpan(compName, "CAL", strike, expDate)
                    AddSpan(compName, "PUT", strike, expDate)
                End If
                Exit While
            End If
        End While
    End Sub

    'Private Sub AddSpan(comp As String, cpf As String, strike As String, exp As String)
    '    Dim r As DataRow = spanCalcTbl.NewRow()
    '    r("compname") = comp
    '    r("cal_put_fut") = cpf
    '    r("strike_price") = strike
    '    r("exp_date") = exp
    '    r("description") = comp & cpf & exp & strike
    '    spanCalcTbl.Rows.Add(r)
    'End Sub


    Private Sub AddSpan(comp As String, cpf As String, strike As String, exp As String)

        Dim r As DataRow = spanCalcTbl.NewRow()

        r("compname") = comp
        r("cal_put_fut") = cpf
        r("strike_price") = strike
        r("exp_date") = exp

        ' Match OLD concat_scrip behavior
        r("description") =
        If(cpf = "FUT",
           "FUTSTK " & comp & " " & exp,
           "OPTSTK " & comp & " " & exp & " " & strike & " " & cpf)

        spanCalcTbl.Rows.Add(r)

    End Sub



    ' ================= PORTFOLIO =================


    Private Sub ParsePortfolio(r As XmlReader)

        Dim row As DataRow = spanOutTbl.NewRow()
        Dim gotSpanReq As Boolean = False
        Dim startDepth As Integer = r.Depth   ' 👈 CRITICAL

        While r.Read()

            If r.NodeType <> XmlNodeType.Element AndAlso
           r.NodeType <> XmlNodeType.EndElement Then Continue While

            If r.NodeType = XmlNodeType.Element Then
                Select Case r.Name

                    Case "firm"
                        row("ClientCode") = r.ReadElementContentAsString()

                    Case "lfv"
                        row("lfv") = Val(r.ReadElementContentAsString())

                    Case "sfv"
                        row("sfv") = Val(r.ReadElementContentAsString())

                    Case "lov"
                        row("lov") = Val(r.ReadElementContentAsString())

                    Case "sov"
                        row("sov") = Val(r.ReadElementContentAsString())

                    Case "spanReq"
                        If Not gotSpanReq Then
                            row("spanreq") = Val(r.ReadElementContentAsString())
                            row("anov") = Val(r.ReadElementContentAsString())

                            If (row("spanreq") - row("anov")) <= 0 Then
                                row("spanreq") = 0
                                row("anov") = 0
                            End If

                            gotSpanReq = True
                        End If
                End Select
            End If

            ' ✅ EXIT ONLY WHEN THIS portfolio CLOSES
            If r.NodeType = XmlNodeType.EndElement AndAlso
           r.Name = "portfolio" AndAlso
           r.Depth = startDepth Then

                row("exposure_margin") = 0
                spanOutTbl.Rows.Add(row)
                Exit While
            End If

        End While
    End Sub

End Class
