Imports System
Imports System.io
Imports System.Data
Imports System.Data.OleDb
Public Class rptposition
    Public Shared chkrptposition As Boolean
    Dim dtable As New DataTable
    Dim eqtable As New DataTable
    Dim CurrTable As New DataTable
    Dim objScript As script = New script
    Dim objTrad As trading = New trading
    Dim syb As String
    Dim masterdata As DataTable = New DataTable
    Dim eqmasterdata As DataTable = New DataTable
    Dim CurrencyMasterData As DataTable = New DataTable
    Dim cmbheight As Boolean = False
    Dim Positionwithexpense As Boolean = False

    Dim cmbh As Integer
    Dim temptable As New DataTable
    Dim objExp As New expenses
    Dim objAna As New analysisprocess
    ''' <summary>
    ''' This Methode Initialize the the below table with its Columns data type
    ''' Initialise DataTable dtable for FO with its columns data type
    ''' Initialise eqtable DataTable for EQ with its Columns Data Type
    ''' Initialise CurrTable DataTable for Currency with its Columns Data Type
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub init_table()
        dtable = New DataTable
        With dtable.Columns
            .Add("script")
            .Add("instrument")
            .Add("company")
            .Add("cpf")
            .Add("mdate", GetType(Date))
            .Add("entrydate", GetType(Date))
            .Add("strike", GetType(Double))
            .Add("qty", GetType(Double))
            .Add("traded", GetType(Double))
            .Add("folots", GetType(Double))
            .Add("Dealer")
        End With

        eqtable = New DataTable
        With eqtable
            .Columns.Add("uid", GetType(Double))
            .Columns.Add("script")
            .Columns.Add("company")
            .Columns.Add("eq")
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("rate", GetType(Double))
            .Columns.Add("entrydate", GetType(Date))
            .Columns.Add("instrument")
            .Columns.Add("Dealer")
        End With

        CurrTable = New DataTable
        With CurrTable.Columns
            .Add("script")
            .Add("instrument")
            .Add("company")
            .Add("cpf")
            .Add("mdate", GetType(Date))
            .Add("entrydate", GetType(Date))
            .Add("strike", GetType(Double))
            .Add("qty", GetType(Double))
            .Add("Lots", GetType(Double))
            .Add("traded", GetType(Double))
            .Add("Dealer")
        End With

    End Sub
    'Private Sub fill_table()
    '    Dim dr As DataRow
    '    Dim count As Integer

    '    Dim ar As New ArrayList
    '    Dim table As New DataTable
    '    table = objTrad.Trading
    '    dtable.Rows.Clear()

    '    Dim dtdtable As DataTable = New DataView(table, " ", "Script", DataViewRowState.CurrentRows).ToTable
    '    Dim dtdtableView As DataView = New DataView(table, "", "Script", DataViewRowState.CurrentRows)
    '    Dim dtScriptdtable As DataTable = dtdtableView.ToTable(True, "Script")



    '    For Each drow As DataRow In dtdtable.Rows
    '        ' count = CInt(table.Compute("count(script)", "script='" & drow("script").ToString.Trim & "' And company='" & drow("company").ToString.Trim & "'"))
    '        Dim drcount() As DataRow = table.Select("script='" & drow("script").ToString.Trim & "' ", "")
    '        count = drcount.Length
    '        Dim dvdata As DataTable

    '        Dim objStr As New Struct_FOContract
    '        objStr = HT_FOContrct(drow("script").ToString().ToUpper())
    '        Dim dblltsize As Double = objStr.lotsize

    '        If count > 1 Then
    '            If Not ar.Contains(drow("script").ToString.Trim.ToUpper) Then
    '                dvdata = New DataView(dtdtable, "script='" & drow("script").ToString.Trim & "'", "", DataViewRowState.CurrentRows).ToTable
    '                Dim brate As Double = 0
    '                Dim srate As Double = 0
    '                ar.Add(drow("script").ToString.Trim.ToUpper)
    '                dr = dtable.NewRow()
    '                drow("script") = drow("script").ToString.Trim
    '                drow("company") = drow("company").ToString.Trim

    '                dr("script") = CStr(drow("script"))
    '                dr("instrument") = CStr(drow("instrumentname"))
    '                dr("strike") = Val(drow("strikerate"))
    '                dr("cpf") = CStr(drow("cp"))
    '                dr("Dealer") = drow("Dealer")
    '                dr("qty") = Val(dvdata.Compute("sum(qty)", ""))
    '                srate = Val(dvdata.Compute("sum(tot)", "tot<0 ").ToString)
    '                brate = Val(dvdata.Compute("sum(tot)", "tot>0 ").ToString)

    '                'For Each row As DataRow In table.Select("script='" & drow("script") & "'")
    '                '    If Val(row("qty")) < 0 Then
    '                '        srate = srate + (-Val(row("tot")))
    '                '    Else
    '                '        brate = brate + Val(row("tot"))
    '                '    End If
    '                'Next
    '                If Val(dr("qty")) = 0 Then
    '                    dr("traded") = Math.Round(Val((brate + srate)), 2)
    '                Else
    '                    dr("traded") = Math.Round(Val((brate + srate) / Val(dr("qty"))), 2)
    '                End If

    '                dr("company") = CStr(drow("company"))
    '                dr("mdate") = CDate(Format(CDate(drow("mdate")), "MMM/dd/yyyy"))
    '                dr("entrydate") = drow("entry_date")

    '                ''divyesh
    '                'Dim dblltsize As Double = IIf(IsDBNull(cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")), 0, cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'"))

    '                If dblltsize = 0 Then
    '                    dr("folots") = 0
    '                Else
    '                    dr("folots") = Val(dr("qty")) / dblltsize
    '                End If


    '                If Val(dr("qty")) <> 0 Then
    '                    dtable.Rows.Add(dr)
    '                End If
    '            End If
    '        Else

    '            dr = dtable.NewRow()
    '            drow("script") = drow("script").ToString.Trim
    '            drow("company") = drow("company").ToString.Trim

    '            dr("script") = CStr(drow("script"))
    '            dr("instrument") = CStr(drow("instrumentname"))
    '            dr("strike") = Val(drow("strikerate"))
    '            dr("cpf") = CStr(drow("cp"))
    '            dr("qty") = Val(drow("qty"))
    '            dr("traded") = Val(drow("rate"))
    '            dr("company") = CStr(drow("company"))
    '            dr("mdate") = CDate(Format(CDate(drow("mdate")), "MMM/dd/yyyy"))
    '            dr("entrydate") = drow("entry_date")
    '            dr("Dealer") = drow("Dealer")
    '            ''divyesh
    '            'Dim dblltsize As Double = cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
    '            If dblltsize = 0 Then
    '                dr("folots") = 0
    '            Else
    '                dr("folots") = Val(dr("qty")) / dblltsize
    '            End If

    '            If Val(dr("qty")) <> 0 Then
    '                dtable.Rows.Add(dr)
    '            End If

    '        End If
    '    Next
    'End Sub
    ''' <summary>
    ''' Fill dtable for Future and Option data. data get from trading table after calculation it assign all the columns value from Temptable which is fill from  tradding data Base table to dtable for all row by row
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fill_table()
        Dim dr As DataRow
        Dim count As Integer

        Dim ar As New ArrayList
        Dim table As New DataTable
        objTrad.Trading(table)
        dtable.Rows.Clear()

        Dim dtdtable As DataTable = New DataView(table, " ", "Script", DataViewRowState.CurrentRows).ToTable
        Dim dtdtableView As DataView = New DataView(table, "", "Script", DataViewRowState.CurrentRows)
        Dim dtScriptdtable As DataTable = dtdtableView.ToTable(True, "Script", "Company")

        For Each drow As DataRow In dtScriptdtable.Rows

            Dim drcount() As DataRow = table.Select("script='" & drow("script").ToString.Trim & "' and Company='" & drow("Company").ToString.Trim & "' ", "")
            count = drcount.Length
            Dim dvdata As DataTable

            Dim objStr As New Struct_FOContract

            Dim dblltsize As Double
            If Not HT_FOContrct(drow("script").ToString().ToUpper()) Is Nothing Then
                objStr = HT_FOContrct(drow("script").ToString().ToUpper())
                dblltsize = objStr.lotsize
            Else
                dblltsize = 0
            End If


            'If drcount(0)("company") = "AXISBANK" Then
            '    dblltsize = objStr.lotsize
            'End If

            If count > 1 Then

                dvdata = New DataView(dtdtable, "script='" & drow("script").ToString.Trim & "'and Company='" & drow("Company").ToString.Trim & "'", "", DataViewRowState.CurrentRows).ToTable
                Dim brate As Double = 0
                Dim srate As Double = 0
                ar.Add(drow("script").ToString.Trim.ToUpper)
                dr = dtable.NewRow()


                dr("script") = CStr(drow("script")).ToString.Trim
                dr("instrument") = CStr(drcount(0)("instrumentname"))
                dr("strike") = Val(drcount(0)("strikerate"))
                dr("cpf") = CStr(drcount(0)("cp"))
                dr("Dealer") = drcount(0)("Dealer")
                dr("qty") = Val(dvdata.Compute("sum(qty)", ""))
                srate = Val(dvdata.Compute("sum(tot)", "tot<0 ").ToString)
                brate = Val(dvdata.Compute("sum(tot)", "tot>0 ").ToString)

                'For Each row As DataRow In table.Select("script='" & drow("script") & "'")
                '    If Val(row("qty")) < 0 Then
                '        srate = srate + (-Val(row("tot")))
                '    Else
                '        brate = brate + Val(row("tot"))
                '    End If
                'Next
                If Val(dr("qty")) = 0 Then
                    dr("traded") = Math.Round(Val((brate + srate)), 2)
                Else
                    dr("traded") = Math.Round(Val((brate + srate) / Val(dr("qty"))), 2)
                End If

                dr("company") = CStr(drcount(0)("company")).ToString.Trim
                dr("mdate") = CDate(Format(CDate(drcount(0)("mdate")), "MMM/dd/yyyy"))
                dr("entrydate") = drcount(0)("entry_date")

                ''divyesh
                'Dim dblltsize As Double = IIf(IsDBNull(cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")), 0, cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'"))

                If dblltsize = 0 Then
                    dr("folots") = 0
                Else
                    dr("folots") = Val(dr("qty")) / dblltsize
                End If
               

                If Val(dr("qty")) <> 0 Then
                    dtable.Rows.Add(dr)
                End If

            Else
                dr = dtable.NewRow()


                dr("script") = CStr(drow("script")).ToString.Trim
                dr("instrument") = CStr(drcount(0)("instrumentname"))
                dr("strike") = Val(drcount(0)("strikerate"))
                dr("cpf") = CStr(drcount(0)("cp"))
                dr("qty") = Val(drcount(0)("qty"))
                dr("traded") = Val(drcount(0)("rate"))
                dr("company") = CStr(drcount(0)("company")).ToString.Trim
                dr("mdate") = CDate(Format(CDate(drcount(0)("mdate")), "MMM/dd/yyyy"))
                dr("entrydate") = drcount(0)("entry_date")
                dr("Dealer") = drcount(0)("Dealer")
                ''divyesh
                'Dim dblltsize As Double = cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                If dblltsize = 0 Then
                    dr("folots") = 0
                Else
                    dr("folots") = Val(dr("qty")) / dblltsize
                End If


              

                If Val(dr("qty")) <> 0 Then
                    dtable.Rows.Add(dr)
                End If

            End If
        Next

        'For Each drow As DataRow In dtdtable.Rows
        ' count = CInt(table.Compute("count(script)", "script='" & drow("script").ToString.Trim & "' And company='" & drow("company").ToString.Trim & "'"))
        '    Dim drcount() As DataRow = table.Select("script='" & drow("script").ToString.Trim & "' ", "")
        '    count = drcount.Length
        '    Dim dvdata As DataTable

        '    Dim objStr As New Struct_FOContract
        '    objStr = HT_FOContrct(drow("script").ToString().ToUpper())
        '    Dim dblltsize As Double = objStr.lotsize

        '    If count > 1 Then
        '        If Not ar.Contains(drow("script").ToString.Trim.ToUpper) Then
        '            dvdata = New DataView(dtdtable, "script='" & drow("script").ToString.Trim & "'", "", DataViewRowState.CurrentRows).ToTable
        '            Dim brate As Double = 0
        '            Dim srate As Double = 0
        '            ar.Add(drow("script").ToString.Trim.ToUpper)
        '            dr = dtable.NewRow()
        '            drow("script") = drow("script").ToString.Trim
        '            drow("company") = drow("company").ToString.Trim

        '            dr("script") = CStr(drow("script"))
        '            dr("instrument") = CStr(drow("instrumentname"))
        '            dr("strike") = Val(drow("strikerate"))
        '            dr("cpf") = CStr(drow("cp"))
        '            dr("Dealer") = drow("Dealer")
        '            dr("qty") = Val(dvdata.Compute("sum(qty)", ""))
        '            srate = Val(dvdata.Compute("sum(tot)", "tot<0 ").ToString)
        '            brate = Val(dvdata.Compute("sum(tot)", "tot>0 ").ToString)

        '            'For Each row As DataRow In table.Select("script='" & drow("script") & "'")
        '            '    If Val(row("qty")) < 0 Then
        '            '        srate = srate + (-Val(row("tot")))
        '            '    Else
        '            '        brate = brate + Val(row("tot"))
        '            '    End If
        '            'Next
        '            If Val(dr("qty")) = 0 Then
        '                dr("traded") = Math.Round(Val((brate + srate)), 2)
        '            Else
        '                dr("traded") = Math.Round(Val((brate + srate) / Val(dr("qty"))), 2)
        '            End If

        '            dr("company") = CStr(drow("company"))
        '            dr("mdate") = CDate(Format(CDate(drow("mdate")), "MMM/dd/yyyy"))
        '            dr("entrydate") = drow("entry_date")

        '            ''divyesh
        '            'Dim dblltsize As Double = IIf(IsDBNull(cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")), 0, cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'"))

        '            If dblltsize = 0 Then
        '                dr("folots") = 0
        '            Else
        '                dr("folots") = Val(dr("qty")) / dblltsize
        '            End If


        '            If Val(dr("qty")) <> 0 Then
        '                dtable.Rows.Add(dr)
        '            End If
        '        End If
        '    Else

        '        dr = dtable.NewRow()
        '        drow("script") = drow("script").ToString.Trim
        '        drow("company") = drow("company").ToString.Trim

        '        dr("script") = CStr(drow("script"))
        '        dr("instrument") = CStr(drow("instrumentname"))
        '        dr("strike") = Val(drow("strikerate"))
        '        dr("cpf") = CStr(drow("cp"))
        '        dr("qty") = Val(drow("qty"))
        '        dr("traded") = Val(drow("rate"))
        '        dr("company") = CStr(drow("company"))
        '        dr("mdate") = CDate(Format(CDate(drow("mdate")), "MMM/dd/yyyy"))
        '        dr("entrydate") = drow("entry_date")
        '        dr("Dealer") = drow("Dealer")
        '        ''divyesh
        '        'Dim dblltsize As Double = cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
        '        If dblltsize = 0 Then
        '            dr("folots") = 0
        '        Else
        '            dr("folots") = Val(dr("qty")) / dblltsize
        '        End If

        '        If Val(dr("qty")) <> 0 Then
        '            dtable.Rows.Add(dr)
        '        End If

        '    End If
        'Next
    End Sub
    ''' <summary>
    ''' Fill Data For Equity  trades data. data get from eqtrades table after calculation it assign all the columns value from temtable which is fill from equity Trading Data base to dtable for all row by row
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub fill_equity()
    '    Dim temtable As New DataTable
    '    temtable = objTrad.select_equity
    '    Dim count As Integer
    '    Dim dr As DataRow
    '    Dim ar As New ArrayList

    '    For Each drow As DataRow In temtable.Rows
    '        count = CInt(temtable.Compute("count(script)", "script='" & drow("script") & "' And company='" & drow("company") & "'"))
    '        If count > 1 Then
    '            If Not ar.Contains(drow("script").ToString.Trim.ToUpper & drow("company").ToString.Trim.ToUpper) Then
    '                Dim brate As Double = 0
    '                Dim srate As Double = 0
    '                ar.Add(drow("script").ToString.Trim.ToUpper & drow("company").ToString.Trim.ToUpper)
    '                dr = eqtable.NewRow()
    '                drow("script") = drow("script").ToString.Trim
    '                drow("company") = drow("company").ToString.Trim

    '                dr("script") = drow("script")
    '                dr("company") = drow("company")
    '                dr("eq") = CStr(drow("eq"))
    '                dr("qty") = Val(temtable.Compute("sum(qty)", "script='" & drow("script") & "' And company='" & drow("company") & "'"))
    '                srate = Val(temtable.Compute("sum(tot)", "script='" & drow("script") & "' AND tot<0 And company='" & drow("company") & "'").ToString)
    '                brate = Val(temtable.Compute("sum(tot)", "script='" & drow("script") & "' AND tot>0 And company='" & drow("company") & "'").ToString)

    '                'For Each row As DataRow In temtable.Select("script='" & drow("script") & "'")
    '                '    If Val(row("qty")) < 0 Then
    '                '        srate = srate + (-Val(row("tot")))
    '                '    Else
    '                '        brate = brate + Val(row("tot"))
    '                '    End If
    '                'Next
    '                If Val(dr("qty")) = 0 Then
    '                    dr("rate") = Math.Round(Val((brate + srate)), 2)
    '                Else
    '                    dr("rate") = Math.Round(Val((brate + srate) / Val(dr("qty"))), 2)
    '                End If

    '                dr("entrydate") = drow("entry_date")
    '                dr("instrument") = "" ' CStr(drow("instrumentname"))
    '                dr("Dealer") = drow("Dealer")
    '                If Val(dr("qty")) <> 0 Then
    '                    eqtable.Rows.Add(dr)
    '                End If
    '            End If
    '        Else
    '            dr = eqtable.NewRow()
    '            drow("script") = drow("script").ToString.Trim
    '            drow("company") = drow("company").ToString.Trim
    '            dr("script") = drow("script")
    '            dr("company") = drow("company")
    '            dr("eq") = CStr(drow("eq"))
    '            dr("qty") = Val(drow("qty"))
    '            dr("rate") = Val(drow("rate"))
    '            dr("entrydate") = drow("entry_date")
    '            dr("instrument") = "" 'CStr(drow("instrumentname"))
    '            dr("Dealer") = drow("Dealer")
    '            If Val(dr("qty")) <> 0 Then
    '                eqtable.Rows.Add(dr)
    '            End If
    '        End If
    '    Next
    'End Sub
    Private Sub fill_equity()
        Dim temtable As New DataTable
        temtable = objTrad.select_equity
        Dim count As Integer
        Dim dr As DataRow
        Dim ar As New ArrayList

        Dim dtdtable As DataTable = New DataView(temtable, " ", "Script", DataViewRowState.CurrentRows).ToTable
        Dim dtdtableView As DataView = New DataView(temtable, "", "Script", DataViewRowState.CurrentRows)
        Dim dtScriptdtable As DataTable = dtdtableView.ToTable(True, "Script", "Company")

        For Each drow As DataRow In dtScriptdtable.Rows
            Dim drcount() As DataRow = temtable.Select("script='" & drow("script").ToString.Trim & "' and Company='" & drow("Company").ToString.Trim & "'  ", "")
            count = drcount.Length
            Dim dvdata As DataTable
            If count > 1 Then
                Dim brate As Double = 0
                Dim srate As Double = 0
                dvdata = New DataView(dtdtable, "script='" & drow("script").ToString.Trim & "' and Company='" & drow("Company").ToString.Trim & "'", "", DataViewRowState.CurrentRows).ToTable
                dr = eqtable.NewRow()

                dr("script") = drow("script").ToString.Trim
                dr("company") = drcount(0)("company").ToString.Trim
                dr("eq") = CStr(drcount(0)("eq"))
                dr("qty") = Val(dvdata.Compute("sum(qty)", "script='" & drow("script") & "'and Company='" & drow("Company").ToString.Trim & "' "))
                srate = Val(dvdata.Compute("sum(tot)", "script='" & drow("script") & "' and Company='" & drow("Company").ToString.Trim & "' AND tot<0 ").ToString)
                brate = Val(dvdata.Compute("sum(tot)", "script='" & drow("script") & "' and Company='" & drow("Company").ToString.Trim & "'AND tot>0 ").ToString)

                'For Each row As DataRow In temtable.Select("script='" & drow("script") & "'")
                '    If Val(row("qty")) < 0 Then
                '        srate = srate + (-Val(row("tot")))
                '    Else
                '        brate = brate + Val(row("tot"))
                '    End If
                'Next
                If Val(dr("qty")) = 0 Then
                    dr("rate") = Math.Round(Val((brate + srate)), 2)
                Else
                    dr("rate") = Math.Round(Val((brate + srate) / Val(dr("qty"))), 2)
                End If

                dr("entrydate") = drcount(0)("entry_date")
                dr("instrument") = "" ' CStr(drow("instrumentname"))
                dr("Dealer") = drcount(0)("Dealer")
                If Val(dr("qty")) <> 0 Then
                    eqtable.Rows.Add(dr)
                End If

            Else
                dr = eqtable.NewRow()
                
                dr("script") = drow("script").ToString.Trim
                dr("company") = drcount(0)("company").ToString.Trim
                dr("eq") = CStr(drcount(0)("eq"))
                dr("qty") = Val(drcount(0)("qty"))
                dr("rate") = Val(drcount(0)("rate"))
                dr("entrydate") = drcount(0)("entry_date")
                dr("instrument") = "" 'CStr(drow("instrumentname"))
                dr("Dealer") = drcount(0)("Dealer")
                If Val(dr("qty")) <> 0 Then
                    eqtable.Rows.Add(dr)
                End If
            End If
        Next

        'Dim drcount() As DataRow = table.Select("script='" & drow("script").ToString.Trim & "' ", "")
        'count = drcount.Length
        'Dim dvdata As DataTable

        'Dim objStr As New Struct_FOContract
        'objStr = HT_FOContrct(drow("script").ToString().ToUpper())
        'Dim dblltsize As Double = objStr.lotsize


       
    End Sub
    ''' <summary>
    ''' Fill Data For Currency trades data. data get from CurrTable table after calculation it assign all the columns value from temtable which is fill from Currency Trading Data base to dtable for all row by row
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fill_Currency()
        Dim dr As DataRow
        Dim count As Integer
        Dim ar As New ArrayList
        Dim table As New DataTable
        table = objTrad.select_Currency_Trading
        CurrTable.Rows.Clear()
        For Each drow As DataRow In table.Rows
            count = CInt(table.Compute("count(script)", "script='" & drow("script") & "' And company='" & drow("company") & "'"))
            If count > 1 Then
                If Not ar.Contains(drow("script").ToString.Trim.ToUpper & drow("company").ToString.Trim.ToUpper) Then
                    Dim brate As Double = 0
                    Dim srate As Double = 0
                    ar.Add(drow("script").ToString.Trim.ToUpper & drow("company").ToString.Trim.ToUpper)
                    dr = CurrTable.NewRow()
                    drow("script") = drow("script").ToString.Trim
                    drow("company") = drow("company").ToString.Trim
                    dr("Dealer") = drow("Dealer")
                    dr("script") = CStr(drow("script"))
                    dr("instrument") = CStr(drow("instrumentname"))
                    dr("strike") = Val(drow("strikerate"))
                    dr("cpf") = CStr(drow("cp"))
                    dr("qty") = Val(table.Compute("sum(qty)", "script='" & drow("script") & "' And company='" & drow("company") & "'"))

                    Dim dblLot As Double = Currencymaster.Compute("MAX(multiplier)", "Script='" & dr("script") & "'")
                    If dblLot = 0 Then
                        dr("Lots") = 0
                    Else
                        dr("Lots") = dr("qty") / dblLot
                    End If

                    srate = Val(table.Compute("sum(tot)", "script='" & drow("script") & "' AND tot<0 And company='" & drow("company") & "'").ToString)
                    brate = Val(table.Compute("sum(tot)", "script='" & drow("script") & "' AND tot>0 And company='" & drow("company") & "'").ToString)

                    'For Each row As DataRow In table.Select("script='" & drow("script") & "'")
                    '    If Val(row("qty")) < 0 Then
                    '        srate = srate + (-Val(row("tot")))
                    '    Else
                    '        brate = brate + Val(row("tot"))
                    '    End If
                    'Next

                    If Val(dr("qty")) = 0 Then
                        dr("traded") = Math.Round(Val((brate + srate)), 2)
                    Else
                        dr("traded") = Math.Round(Val((brate + srate) / Val(dr("qty"))), 2)
                    End If

                    dr("company") = CStr(drow("company"))
                    dr("mdate") = CDate(Format(CDate(drow("mdate")), "MMM/dd/yyyy"))
                    dr("entrydate") = drow("entry_date")
                    If Val(dr("qty")) <> 0 Then
                        CurrTable.Rows.Add(dr)
                    End If
                End If
            Else
                dr = CurrTable.NewRow()
                drow("script") = drow("script").ToString.Trim
                drow("company") = drow("company").ToString.Trim
                dr("script") = CStr(drow("script"))
                dr("instrument") = CStr(drow("instrumentname"))
                dr("strike") = Val(drow("strikerate"))
                dr("cpf") = CStr(drow("cp"))
                dr("qty") = Val(drow("qty"))
                dr("Dealer") = drow("Dealer")
                Dim dblLot As Double = Currencymaster.Compute("MAX(multiplier)", "Script='" & dr("script") & "'")
                If dblLot = 0 Then
                    dr("Lots") = 0
                Else
                    dr("Lots") = dr("qty") / Currencymaster.Compute("MAX(multiplier)", "Script='" & dr("script") & "'")
                End If


                dr("traded") = Val(drow("rate"))
                dr("company") = CStr(drow("company"))
                dr("mdate") = CDate(Format(CDate(drow("mdate")), "MMM/dd/yyyy"))
                dr("entrydate") = drow("entry_date")
                If Val(dr("qty")) <> 0 Then
                    CurrTable.Rows.Add(dr)
                End If
            End If
        Next
    End Sub
    Private Sub rptposition_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Maximized
        'Me.Refresh()
    End Sub

    ''' <summary>
    ''' set chkrptPosition flag false which show that this form is open or close
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rptposition_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        chkrptposition = False
    End Sub
    ''' <summary>
    ''' call all three methods i.e. Fill_table(),Fill_Equity(),Fil_currency() which fill data datatable dtable,eqtabel,Currtable
    ''' from Database tables
    ''' and in this method Filter on Company and get the company form all datatables and merge it in single DataTable
    ''' ans assign value to comptable and comptable is assign to cmbsymbol company combo box
    ''' call cmdShow_click event
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub fill_all_table(ByVal e As System.EventArgs)
        init_table()
        Call fill_table()
        Call fill_equity()
        Call fill_Currency()

        Dim dv As DataView
        dv = New DataView(dtable, "", "company", DataViewRowState.CurrentRows)

        Dim tmptable As DataTable
        tmptable = New DataTable
        tmptable = dv.ToTable(True, "company")

        dv = New DataView(eqtable, "", "company", DataViewRowState.CurrentRows)
        tmptable.Merge(dv.ToTable(True, "company"))

        dv = New DataView(tmptable, "", "company", DataViewRowState.CurrentRows)

        Dim comptable As New DataTable
        comptable = objTrad.Comapany
        cmbsymbol.DataSource = comptable

        cmbsymbol.DisplayMember = "company"
        cmbsymbol.ValueMember = "company"
        chksymbol_CheckedChanged(chksymbol, e)
        Call cmdshow_Click(cmdshow, e)
    End Sub
    ''' <summary>
    ''' set chkrptposition flag value true
    ''' call fill_all_table(e) Method
    ''' assign Data of masterdata to cpfmaster ans assign data to GdtFOTrades
    ''' same for equity and Currency Trades assign from eqmasterdata and CurrencyMaster from database 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rptposition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Loading(e)
    End Sub
    Public Sub Loading(ByVal e As System.EventArgs)
        EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)
        If (EXPORT_IMPORT_POSITION = 1) Then
            Label8.Text = "CSV Trade Path :"
        ElseIf (EXPORT_IMPORT_POSITION = 2) Then
            Label8.Text = " Excel Trade Path :"
        End If
        chkrptposition = True
        Call fill_all_table(e)
        'cmbsymbol.SelectedIndex = 1
        'If cmbsymbol.Items.Count > 0 Then
        '    cmbsymbol.SelectedIndex = 0
        '    syb = "('" & cmbsymbol.SelectedValue.ToString & "')"
        'End If

        masterdata = New DataTable
        masterdata = cpfmaster
        temptable = GdtFOTrades 'objTrad.Trading

        CurrencyMasterData = New DataTable
        CurrencyMasterData = Currencymaster
    End Sub
    ''' <summary>
    ''' when checked chenage is True then it Fill all the company from database to company combo box and 
    ''' whether it is uncheck is if filled combo and it selection Company it filter only that company data to Grid view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chksymbol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chksymbol.CheckedChanged
        If chksymbol.Checked = True Then
            cmbsymbol.Enabled = False
            Dim i As Integer
            If cmbsymbol.Items.Count > 0 Then
                For Each obj As Object In cmbsymbol.Items
                    If i = 0 Then
                        syb = "('" & obj(0).ToString & "'"
                    Else
                        syb = syb & ",'" & obj(0).ToString & "'"
                    End If
                    i += 1
                Next
                syb = syb & ")"
            End If

        Else
            cmbsymbol.Enabled = True
            If cmbsymbol.Items.Count > 0 Then
                cmbsymbol.SelectedIndex = 0
                syb = "( '" & cmbsymbol.SelectedValue & "' )"
            End If
        End If
    End Sub

    ''' <summary>
    ''' set selected Company name from cmbsymbol combo box and set that syb variable as text
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbsymbol_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsymbol.SelectedIndexChanged
        If cmbsymbol.Items.Count > 0 And cmbsymbol.SelectedValue.ToString <> "" Then
            syb = "('" & cmbsymbol.SelectedValue.ToString & "')"
        End If
    End Sub

    ''' <summary>
    ''' this button click display data in the data Grid View after Filteration of company name which get value from syb variable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        If Not syb Is Nothing Then
            Dim dvFO As DataView
            dvFO = New DataView(dtable, "", "company", DataViewRowState.CurrentRows)
            dvFO.RowFilter = " company in " & syb & " "
            DGFOPosition.DataSource = dvFO.ToTable
            DGFOPosition.Refresh()

            Dim dvEQ As New DataView(eqtable, "", "company", DataViewRowState.CurrentRows)
            dvEQ.RowFilter = " company in " & syb & " "
            Dim dt As DataTable = dvEQ.ToTable()
            dt.Columns.Remove("Instrument")
            DGEQPosition.DataSource = dt ' dvEQ.ToTable
            DGEQPosition.Refresh()

            Dim dvCurrency As New DataView(CurrTable, "", "company", DataViewRowState.CurrentRows)
            dvCurrency.RowFilter = " company in " & syb & " "
            DGCurrencyPosition.DataSource = dvCurrency.ToTable
            DGCurrencyPosition.Refresh()

        End If
    End Sub

    ''' <summary>
    ''' This Export data of FO Grid View to Excel File
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub btnExportFO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportFO.Click
        ExportFo(False)
    End Sub

    Public Sub ExportFo(ByVal Auto As Boolean)
        'grdtrad.DataSource = dtable
        'grdtrad.Refresh()
        'grdeq.DataSource = eqtable
        'grdeq.Refresh()
        EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)

        If (EXPORT_IMPORT_POSITION = 2) Then


            If DGFOPosition.Rows.Count > 0 Then
                If Auto = True Then
                    Dim mBackupPath As String = CStr(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")), "D:\", objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")))
                    '   Dim strpath As String = mBackupPath & "\FO_POS_" & Date.Now() & ".XLS"
                    Dim strpath As String = mBackupPath & "\FO_POS_ " & Format(Now, "dd-MMM-yyyy hh mm ss tt") & ".xls"
                    'Dim strpath As String = "FO_POS_" & Date.Now()
                    Dim grd(0) As DataGridView
                    grd(0) = DGFOPosition
                    Dim sname(0) As String
                    sname(0) = "Trading"
                    exporttoexcel(grd, strpath, sname, "FO")
                Else
                    Dim savedi As New SaveFileDialog
                    savedi.Filter = "Files(*.XLS)|*.XLS"
                    If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                        'exportExcel(grdtrad, savedi.FileName)
                        'exportExcel(grdeq, savedi.FileName)
                        Dim grd(0) As DataGridView
                        grd(0) = DGFOPosition
                        Dim sname(0) As String
                        sname(0) = "Trading"
                        exporttoexcel(grd, savedi.FileName, sname, "FO")
                        MsgBox("Export Successfully")
                        OPEN_Export_File(savedi.FileName)
                       
                    End If
                End If
            End If

        ElseIf (EXPORT_IMPORT_POSITION = 1) Then

            If DGFOPosition.Rows.Count > 0 Then
                If Auto = True Then
                    Dim mBackupPath As String = CStr(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")), "D:\", objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")))
                    Dim strpath As String = mBackupPath & "\FO_POS_" & Format(Now, "dd-MMM-yyyy hh mm ss tt") & ".Csv"
                    Dim grd As DataGridView
                    grd = DGFOPosition
                    'Dim sname(0) As String
                    'sname(0) = "Trading"
                    Dim dt As DataTable
                    dt = CType(DGFOPosition.DataSource, DataTable)

                    Dim name(dt.Columns.Count) As String
                    Dim dtgrd As DataTable
                    Dim dr As DataRow
                    dtgrd = New DataTable
                    With dtgrd.Columns
                        .Add("Instrument")
                        .Add("Security")
                        .Add("CPF")
                        .Add("Exp. Date", GetType(String))
                        .Add("Entrydate", GetType(Date))
                        .Add("Strike", GetType(Double))
                        .Add("Qty", GetType(Double))
                        .Add("ATP", GetType(Double))
                        .Add("FO Lots", GetType(Double))
                        .Add("Dealer")
                    End With
                    Dim cal As DataRow
                    ' DgvExpense.DataSource = DtGrd
                    dr = dtgrd.NewRow()
                    For Each dr5 As DataRow In dt.Rows
                        cal = dtgrd.NewRow()
                        cal("Instrument") = dr5("Instrument")
                        cal("Security") = dr5("company")
                        cal("CPF") = dr5("CPF")
                        cal("Exp. Date") = CDate(dr5("MDate")).ToString("dd-MMM-yyyy")
                        cal("Entrydate") = dr5("Entrydate")
                        cal("Strike") = dr5("Strike")
                        cal("Qty") = dr5("Qty")
                        cal("ATP") = dr5("traded")
                        cal("FO Lots") = dr5("FOLots")
                        cal("Dealer") = "'" & dr5("Dealer")

                        dtgrd.Rows.Add(cal)

                        dtgrd.AcceptChanges()


                    Next
                    exporttocsv(dtgrd, strpath, "FO")
                Else
                    Dim savedi As New SaveFileDialog
                    savedi.Filter = "File(*.csv)|*.Csv"
                    If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                        'exportExcel(grdtrad, savedi.FileName)
                        'exportExcel(grdeq, savedi.FileName)
                        Dim grd As DataGridView
                        grd = DGFOPosition
                        'Dim sname(0) As String
                        'sname(0) = "Trading"
                        Dim dt As DataTable
                        dt = CType(DGFOPosition.DataSource, DataTable)

                        Dim name(dt.Columns.Count) As String
                        Dim dtgrd As DataTable
                        Dim dr As DataRow
                        dtgrd = New DataTable
                        With dtgrd.Columns
                            .Add("Instrument")
                            .Add("Security")
                            .Add("CPF")
                            .Add("Exp. Date", GetType(String))
                            .Add("Entrydate", GetType(Date))
                            .Add("Strike", GetType(Double))
                            .Add("Qty", GetType(Double))
                            .Add("ATP", GetType(Double))
                            .Add("FO Lots", GetType(Double))
                            .Add("Dealer")
                        End With
                        Dim cal As DataRow
                        ' DgvExpense.DataSource = DtGrd
                        dr = dtgrd.NewRow()
                        For Each dr5 As DataRow In dt.Rows
                            cal = dtgrd.NewRow()
                            cal("Instrument") = dr5("Instrument")
                            cal("Security") = dr5("company")
                            cal("CPF") = dr5("CPF")
                            cal("Exp. Date") = CDate(dr5("MDate")).ToString("dd-MMM-yyyy")
                            cal("Entrydate") = dr5("Entrydate")
                            cal("Strike") = dr5("Strike")
                            cal("Qty") = dr5("Qty")
                            cal("ATP") = dr5("traded")
                            cal("FO Lots") = dr5("FOLots")
                            cal("Dealer") = "'" & dr5("Dealer")

                            dtgrd.Rows.Add(cal)

                            dtgrd.AcceptChanges()


                        Next
                        exporttocsv(dtgrd, savedi.FileName, "FO")
                        MsgBox("Export Successfully")
                        OPEN_Export_File(savedi.FileName)
                    End If
                End If

            End If
        End If
    End Sub

    'Private Sub btnExportFO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportFO.Click
    '    'grdtrad.DataSource = dtable
    '    'grdtrad.Refresh()
    '    'grdeq.DataSource = eqtable
    '    'grdeq.Refresh()

    '    EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)

    '    If (EXPORT_IMPORT_POSITION = 2) Then


    '        If DGFOPosition.Rows.Count > 0 Then
    '            Dim savedi As New SaveFileDialog
    '            savedi.Filter = "Files(*.XLS)|*.XLS"
    '            If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
    '                'exportExcel(grdtrad, savedi.FileName)
    '                'exportExcel(grdeq, savedi.FileName)
    '                Dim grd(0) As DataGridView
    '                grd(0) = DGFOPosition
    '                Dim sname(0) As String
    '                sname(0) = "Trading"
    '                exporttoexcel(grd, savedi.FileName, sname, "FO")
    '                'MsgBox("Export Sucessfully")
    '            End If
    '        End If
    '    ElseIf (EXPORT_IMPORT_POSITION = 1) Then
    '        If DGFOPosition.Rows.Count > 0 Then
    '            Dim savedi As New SaveFileDialog
    '            savedi.Filter = "File(*.csv)|*.Csv"
    '            If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
    '                'exportExcel(grdtrad, savedi.FileName)
    '                'exportExcel(grdeq, savedi.FileName)
    '                Dim grd As DataGridView
    '                grd = DGFOPosition
    '                'Dim sname(0) As String
    '                'sname(0) = "Trading"


    '                exporttocsv(DGFOPosition, savedi.FileName, "FO")
    '                MsgBox("Export Sucessfully")
    '            End If
    '        End If
    '    End If
    'End Sub

    ''' <summary>
    '''  This Export data of EQ Grid View to Excel File
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub exportEQ(ByVal Auto As Boolean)
        EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)

        If (EXPORT_IMPORT_POSITION = 2) Then

            If Auto = False Then
                If DGEQPosition.Rows.Count > 0 Then
                    Dim savedi As New SaveFileDialog
                    savedi.Filter = "Files(*.XLS)|*.XLS"
                    If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                        'exportExcel(grdeq, savedi.FileName)
                        Dim grd(0) As DataGridView
                        grd(0) = DGEQPosition
                        Dim sname(0) As String
                        sname(0) = "Equity"
                        exporttoexcel(grd, savedi.FileName, sname, "EQ")
                        MsgBox("Export Successfully")
                        OPEN_Export_File(savedi.FileName)
                    End If
                End If
            Else
                Dim mBackupPath As String = CStr(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")), "D:\", objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")))
                Dim strpath As String = mBackupPath & "\EQ_POS_ " & Format(Now, "dd-MMM-yyyy hh mm ss tt") & ".xls"
                Dim grd(0) As DataGridView
                grd(0) = DGEQPosition
                Dim sname(0) As String
                sname(0) = "Equity"
                exporttoexcel(grd, strpath, sname, "EQ")
            End If



        ElseIf (EXPORT_IMPORT_POSITION = 1) Then
            If Auto = False Then
                If DGEQPosition.Rows.Count > 0 Then
                    Dim savedi As New SaveFileDialog
                    savedi.Filter = "File(*.csv)|*.Csv"
                    If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                        'exportExcel(grdtrad, savedi.FileName)
                        'exportExcel(grdeq, savedi.FileName)
                        Dim Dt As New DataTable
                        Dt = CType(DGEQPosition.DataSource, DataTable)
                        Dim name(Dt.Columns.Count) As String
                        Dim j As Integer = 0
                        Dim dtgrd As DataTable
                        Dim dr As DataRow
                        dtgrd = New DataTable
                        With dtgrd.Columns
                            .Add("Security")
                            .Add("EQ")
                            .Add("Qty", GetType(Double))
                            .Add("ATP", GetType(Double))
                            .Add("Entrydate", GetType(Date))
                            .Add("Dealer")
                        End With
                        Dim cal As DataRow
                        ' DgvExpense.DataSource = DtGrd
                        dr = dtgrd.NewRow()
                        For Each dr5 As DataRow In Dt.Rows
                            cal = dtgrd.NewRow()

                            cal("Security") = dr5("company")
                            cal("EQ") = dr5("EQ")
                            cal("Qty") = dr5("Qty")
                            cal("ATP") = dr5("rate")
                            cal("Entrydate") = dr5("Entrydate")
                            cal("Dealer") = "'" & dr5("Dealer")
                            dtgrd.Rows.Add(cal)

                            dtgrd.AcceptChanges()


                        Next

                        exporttocsv(dtgrd, savedi.FileName, "EQ")
                        MsgBox("Export Successfully")
                        OPEN_Export_File(savedi.FileName)
                    End If
                End If
            Else
                Dim mBackupPath As String = CStr(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")), "D:\", objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")))
                Dim strpath As String = mBackupPath & "\EQ_POS_ " & Format(Now, "dd-MMM-yyyy hh mm ss tt") & ".Csv"
                If DGEQPosition.Rows.Count > 0 Then

                    'exportExcel(grdtrad, savedi.FileName)
                    'exportExcel(grdeq, savedi.FileName)
                    Dim Dt As New DataTable
                    Dt = CType(DGEQPosition.DataSource, DataTable)
                    Dim name(Dt.Columns.Count) As String
                    Dim j As Integer = 0
                    Dim dtgrd As DataTable
                    Dim dr As DataRow
                    dtgrd = New DataTable
                    With dtgrd.Columns
                        .Add("Security")
                        .Add("EQ")
                        .Add("Qty", GetType(Double))
                        .Add("ATP", GetType(Double))
                        .Add("Entrydate", GetType(Date))
                        .Add("Dealer")
                    End With
                    Dim cal As DataRow
                    ' DgvExpense.DataSource = DtGrd
                    dr = dtgrd.NewRow()
                    For Each dr5 As DataRow In Dt.Rows
                        cal = dtgrd.NewRow()

                        cal("Security") = dr5("company")
                        cal("EQ") = dr5("EQ")
                        cal("Qty") = dr5("Qty")
                        cal("ATP") = dr5("rate")
                        cal("Entrydate") = dr5("Entrydate")
                        cal("Dealer") = "'" & dr5("Dealer")
                        dtgrd.Rows.Add(cal)

                        dtgrd.AcceptChanges()


                    Next

                    exporttocsv(dtgrd, strpath, "EQ")


                End If
            End If


        End If
    End Sub
    Private Sub btnExportEQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportEQ.Click
        exportEQ(False)

    End Sub
    'Private Sub btnExportEQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportEQ.Click

    '    EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)

    '    If (EXPORT_IMPORT_POSITION = 2) Then
    '        If DGEQPosition.Rows.Count > 0 Then
    '            Dim savedi As New SaveFileDialog
    '            savedi.Filter = "Files(*.XLS)|*.XLS"
    '            If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
    '                'exportExcel(grdeq, savedi.FileName)
    '                Dim grd(0) As DataGridView
    '                grd(0) = DGEQPosition
    '                Dim sname(0) As String
    '                sname(0) = "Equity"
    '                exporttoexcel(grd, savedi.FileName, sname, "EQ")
    '                'MsgBox("Export Sucessfully")
    '            End If
    '        End If

    '    ElseIf (EXPORT_IMPORT_POSITION = 1) Then
    '        If DGEQPosition.Rows.Count > 0 Then
    '            Dim savedi As New SaveFileDialog
    '            savedi.Filter = "File(*.csv)|*.Csv"
    '            If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
    '                'exportExcel(grdtrad, savedi.FileName)
    '                'exportExcel(grdeq, savedi.FileName)


    '                exporttocsvEQ(DGEQPosition, savedi.FileName, "EQ")
    '                MsgBox("Export Sucessfully")
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub rptposition_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'If e.KeyCode = Keys.F11 Then
        '    If DGFOPosition.Rows.Count > 0 Then
        '        Dim savedi As New SaveFileDialog
        '        savedi.Filter = "Files(*.XLS)|*.XLS"
        '        If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
        '            'exportExcel(grdtrad, savedi.FileName)
        '            'exportExcel(grdeq, savedi.FileName)
        '            Dim grd(0) As DataGridView
        '            grd(0) = DGFOPosition
        '            Dim sname(0) As String
        '            sname(0) = "Trading"
        '            exporttoexcel(grd, savedi.FileName, sname, "other")
        '        End If
        '    End If
        'ElseIf e.KeyCode = Keys.F12 Then
        '    If DGEQPosition.Rows.Count > 0 Then
        '        Dim savedi As New SaveFileDialog
        '        savedi.Filter = "Files(*.XLS)|*.XLS"
        '        If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
        '            'exportExcel(grdeq, savedi.FileName)
        '            Dim grd(0) As DataGridView
        '            grd(0) = DGEQPosition
        '            Dim sname(0) As String
        '            sname(0) = "Equity"
        '            exporttoexcel(grd, savedi.FileName, sname, "other")
        '        End If
        '    End If
        'End If
    End Sub


    ''' <summary>
    ''' open browser which opens only excel file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)
        If (EXPORT_IMPORT_POSITION = 2) Then
            Dim opfile As OpenFileDialog
            opfile = New OpenFileDialog
            opfile.Filter = "Files(*.xls)|*.xls"
            If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
                txtpath.Text = opfile.FileName
            End If
        ElseIf (EXPORT_IMPORT_POSITION = 1) Then
            Dim opfile As OpenFileDialog
            opfile = New OpenFileDialog
            opfile.Filter = "Files(*.csv)|*.csv"
            If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then

                txtpath.Text = opfile.FileName


            End If
        End If

    End Sub

    ''' <summary>
    ''' it import excel file and read its data and assign it to tempFo table and after calculation on it assign to grid Fo which display the Data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnImportFO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportFO.Click
        'Dim Dt As Date = Now
        Try
            Dim strresult As Integer = MsgBox("You Want to Import Position With Expense?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)

            If strresult = DialogResult.Yes Then
                Positionwithexpense = True
            Else
                Positionwithexpense = False

            End If
            Me.Cursor = Cursors.WaitCursor
            read_file()
            Me.Cursor = Cursors.Default
            Call fill_all_table(e)
            Call fill_trades("FO")
            Call UpdImpFlag()
            set_importposition_flag()
        Catch ex As Exception
            MsgBox("Invalid File.")
            Me.Cursor = Cursors.Default
        End Try
        'MsgBox(DateDiff(DateInterval.Second, Dt, Now))
    End Sub
    Private Sub UpdImpFlag()
        If GdtSettings.Select("SettingName='IMPORT_PREVIOUS_POSITION_FLAG'").Length = 0 Then
            objTrad.SettingName = "IMPORT_PREVIOUS_POSITION_FLAG"
            objTrad.SettingKey = "TRUE"
            objTrad.Insert_setting()
            GdtSettings = objTrad.Settings
        ElseIf GdtSettings.Select("SettingName='IMPORT_PREVIOUS_POSITION_FLAG'")(0).Item("SettingKey") = "FALSE" Then
            objTrad.Uid = GdtSettings.Select("SettingName='IMPORT_PREVIOUS_POSITION_FLAG'")(0).Item("Uid")
            objTrad.SettingName = "IMPORT_PREVIOUS_POSITION_FLAG"
            objTrad.SettingKey = "TRUE"
            objTrad.Update_setting()
            GdtSettings = objTrad.Settings
        End If
    End Sub
    ''' <summary>
    ''' read data from excel file to tempdata which is use to assign the DataSource of Fo Grid view and calculation on it
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub read_file()
        '  Try


        EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)
        'else If (EXPORT_IMPORT_POSITION = 1) Then
        'Label8.Text = "CSV Trade Path :"
        'ElseIf (EXPORT_IMPORT_POSITION = 2) Then
        'Label8.Text = " Excel Trade Path :"
        'End If




        Dim tempdata As New DataTable
        Dim a, a1, script1 As String
        Dim VarToken1 As Long
        tempdata = New DataTable
        Dim fpath As String
        Dim Payalcsv As String

        fpath = CStr(txtpath.Text.Trim)
        If fpath <> "" Then
            If (EXPORT_IMPORT_POSITION = 2) Then




                'tempdata.Columns.Add("strike")
                'tempdata.Columns.Add("option_type")
                'tempdata.Columns.Add("Exp. Date")
                'tempdata.Columns.Add("Company")
                'tempdata.Columns.Add("Qty")
                'tempdata.Columns.Add("Rate")
                'tempdata.Columns.Add("Instrument")
                'tempdata.Columns.Add("entrydate")

                Dim fi As New FileInfo(fpath)
                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" & fpath
                Dim objConn As New OleDbConnection(sConnectionString)
                objConn.Open()
                Dim objCmdSelect As New OleDbCommand("SELECT * FROM [Trading$] where security <> ''", objConn)
                'Dim objCmdSelect As New OleDbCommand("SELECT strike,option_type,Exp. Date,Company,Qty,Rate,Instrument,entrydate FROM " & fi.Name, objConn)
                Dim objAdapter1 As New OleDbDataAdapter
                objAdapter1.SelectCommand = objCmdSelect
                objAdapter1.Fill(tempdata)
                objConn.Close()
                'fi.Delete()
                tempdata.Columns.Add("script")
                tempdata.Columns.Add("token1", GetType(Long))
                tempdata.Columns.Add("isliq")
                tempdata.Columns.Add("orderno", GetType(String))
                'tempdata.Columns.Add("lActivityTime")
                'tempdata.Columns.Add("FileFlag")
                tempdata.AcceptChanges()
                If tempdata.Columns.Contains("Dealer") = False Then
                    tempdata.Columns.Add("Dealer", GetType(String))
                End If

            ElseIf (EXPORT_IMPORT_POSITION = 1) Then
                'Dim a, a1, script1 As String
                'Dim VarToken1 As Long
                'tempdata = New DataTable
                'Dim strFileName As String

                Dim fName As String = ""


                Dim fi As New FileInfo(fpath)
                Dim strdata As [String]()
                strdata = fpath.Split(New Char() {"\"c})
                'strdata = dr("script").Split("  ", StringSplitOptions.None)


                Dim filename As String = strdata(strdata.Length - 1)
                '  serious = strdata(2)

                ' Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " & fpath
                'Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;" & fpath

                ' Dim objConn As New OleDbConnection(sConnectionString)
                ' objConn.Open()

                '  Dim fi1 As New FileInfo(fpath)


                Dim sConnectionStringz As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
                Dim objConn As New OleDbConnection(sConnectionStringz)
                objConn.Open()
                'DataGridView1.TabIndex = 1
                'Dim objCmdSelect As New OleDbCommand("SELECT * FROM " & fi.Name, objConn)
                'Dim objAdapter1 As New OleDbDataAdapter
                'objAdapter1.SelectCommand = objCmdSelect
                ' Dim objDataset1 As New DataSet
                '  objAdapter1.Fill(objDataset1)
                '--objAdapter1.Update(objDataset1) '--updating
                ' DataGridView1.DataSource = objDataset1.Tables(0).DefaultView

                ' objConn.Close()



                '  Dim cmd As New OleDbCommand("SELECT * FROM [" & strFileName & "]", conn)

                Dim objCmdSelect As New OleDbCommand("SELECT * FROM [" & filename & "] where security <> ''", objConn)
                'Dim objCmdSelect As New OleDbCommand("SELECT strike,option_type,Exp. Date,Company,Qty,Rate,Instrument,entrydate FROM " & fi.Name, objConn)
                Dim objAdapter1 As New OleDbDataAdapter
                objAdapter1.SelectCommand = objCmdSelect
                objAdapter1.Fill(tempdata)
                objConn.Close()
                'fi.Delete()
                tempdata.Columns.Add("script")
                tempdata.Columns.Add("token1", GetType(Long))
                tempdata.Columns.Add("isliq")
                tempdata.Columns.Add("orderno", GetType(String))
                'tempdata.Columns.Add("lActivityTime")
                'tempdata.Columns.Add("FileFlag")
                tempdata.AcceptChanges()
                If tempdata.Columns.Contains("Dealer") = False Then
                    tempdata.Columns.Add("Dealer", GetType(String))
                End If

            End If
            tempdata.Columns.Add("tot", GetType(Double))
            tempdata.Columns.Add("tot2", GetType(Double))
            For Each dr5 As DataRow In tempdata.Rows
                Dim strdata1 As [String]()
                Dim strValue As String


                If dr5("Dealer").Equals(DBNull.Value) = False Then


                    strValue = dr5("Dealer")
                    If strValue.Contains("'") Then
                        strdata1 = strValue.Split(New Char() {"'"c})
                        dr5("Dealer") = strdata1(1)
                    End If

                Else
                    dr5("Dealer") = "OP"
                End If
            Next
            Dim drow As DataRow
            Dim script As String
            'Dim cp As String

            For Each drow In tempdata.Select("security<>''")
                GVarMAXFOTradingOrderNo = GVarMAXFOTradingOrderNo + 1
                Dim VarTokenNo As Long = 0
                If Not IsDBNull(drow("security")) Then
                    If Not IsDBNull(drow("cpf")) Then
                        If Mid(drow("CPF"), 1, 1) = "C" Or Mid(drow("CPF"), 1, 1) = "P" Then
                            Dim DrScriptROw() As DataRow = cpfmaster.Select("instrumentname='" & drow("Instrument") & "' AND symbol='" & GetSymbol(drow("Security")) & "' AND expdate1=#" & fDate(CDate(drow("Exp# Date")).Date) & "# AND strike_price='" & drow("Strike") & "' AND option_type LIKE '" & drow("CPF") & "%'")
                            If DrScriptROw.Length > 0 Then
                                script = DrScriptROw(0)("Script").ToString.ToUpper 'drow("Instrument") & Space(2) & drow("security") & Space(2) & expdt & Space(2) & CStr(Format(Val(drow("strike")), "###0.00")) & Space(2) & cp
                                drow("script") = (script.Trim)
                                VarTokenNo = DrScriptROw(0)("Token")
                                a = Mid(drow("script"), Len(drow("script")) - 1, 1)
                                a1 = Mid(drow("script"), Len(drow("script")), 1)
                                If a = "C" Then
                                    script1 = Mid(drow("script"), 1, Len(drow("script")) - 2) & "P" & a1
                                Else
                                    script1 = Mid(drow("script"), 1, Len(drow("script")) - 2) & "C" & a1
                                End If
                                VarToken1 = CLng(Val(masterdata.Compute("max(token)", "script='" & script1 & "'").ToString))
                                drow("token1") = VarToken1
                                drow("isliq") = False ' "No"

                                For Each row As DataRow In temptable.Select("script='" & drow("script") & "' And company='" & drow("Security") & "'")
                                    If row("isliq") = True Then
                                        drow("isliq") = True ' "Yes"
                                    Else
                                        drow("isliq") = False ' "No"
                                    End If
                                    Exit For
                                Next
                            Else
                                VarToken1 = 0
                            End If

                            'For Each cprow As DataRow In masterdata.Select("InstrumentName='" & UCase(drow("Instrument")) & "' and symbol='" & UCase(drow("security")) & "'")
                            '    If UCase(Mid(cprow("option_type"), 1, 1)) = UCase(Mid(drow("CPF"), 1, 1)) Then
                            '        cp = cprow("option_type")
                            '        Exit For
                            '    End If
                            'Next
                            'Dim expdt As String
                            'expdt = Format(CDate(drow("Exp# Date")), "ddMMMyyyy")
                            'expdt.Substring(3, 1).ToUpper()
                            'expdt.Substring(4, 2).ToLower()
                        Else

                            'Format(CDate(drow("Exp# Date")), "ddMMMyyyy")

                            Dim DrScriptROw() As DataRow = cpfmaster.Select("instrumentname='" & drow("Instrument") & "' AND symbol='" & GetSymbol(drow("Security")) & "' AND expdate1=#" & Format(CDate(drow("Exp# Date")).Date, "ddMMMyyyy") & "#")


                            If DrScriptROw.Length > 0 Then
                                'Dim expdt As String
                                'expdt = Format(CDate(drow("Exp# Date")), "ddMMMyyyy")
                                'expdt.Substring(3, 1).ToUpper()
                                'expdt.Substring(4, 2).ToLower()
                                VarTokenNo = DrScriptROw(0)("token")
                                script = DrScriptROw(0)("Script").ToString.ToUpper 'drow("Instrument") & Space(2) & drow("security") & Space(2) & expdt
                                drow("script") = (script.Trim)
                                drow("token1") = 0
                                drow("isliq") = False ' "No"
                            End If
                        End If

                    Else
                        Dim DrScriptROw() As DataRow = cpfmaster.Select("instrumentname='" & drow("Instrument") & "' AND symbol='" & GetSymbol(drow("Security")) & "' AND expdate1=#" & fDate(CDate(drow("Exp# Date")).Date) & "#")
                        ' VarTokenNo = CLng(Val(masterdata.Compute("max(token)", "script='" & drow("script") & "'").ToString))
                        If DrScriptROw.Length > 0 Then
                            VarTokenNo = DrScriptROw(0)("token")
                            script = DrScriptROw(0)("Script")
                            drow("script") = (script.Trim)
                            drow("token1") = 0
                            drow("isliq") = False ' "No"
                            drow("cpf") = ""
                        End If
                        '    If VarTokenNo <> 0 Then
                        '        Dim expdt As String
                        '        expdt = Format(CDate(drow("Exp# Date")), "ddMMMyyyy")
                        '        expdt.Substring(3, 1).ToUpper()
                        '        expdt.Substring(4, 2).ToLower()
                        '        script = drow("Instrument") & Space(2) & drow("security") & Space(2) & expdt
                        '        drow("script") = (script.Trim)
                        '        drow("token1") = 0
                        '        drow("isliq") = False ' "No"
                        '        drow("cpf") = ""
                        '    End If
                    End If
                    If VarTokenNo = 0 Then
                        MsgBox(drow("script") & " does not exist in contract")
                    End If
                    ' drow("lActivityTime") = 0
                    ' drow("FileFlag") = 0

                End If
                drow("orderno") = GVarMAXFOTradingOrderNo
                drow("tot") = drow("Qty") * drow("ATP")
                drow("tot2") = drow("Qty") * (Val(drow("ATP")) + Val(drow("Strike")))
            Next

            tempdata = New DataView(tempdata, "script<>''", "token1", DataViewRowState.CurrentRows).ToTable
            If tempdata.Rows.Count > 0 Then
                objScript.Insert_trading(tempdata)
                If Positionwithexpense = False Then
                    Dim exp As Double
                    Dim PreBalance As Double
                    Init_G_DTExpenseDataa_Importdata()
                    GSub_CalculateExpense_ImportData(tempdata, "FO", True)
                    mmDTCFBalance = objTrad.Select_CFBalance()
                   
                    For Each Dr As DataRow In G_DTExpenseData_Importdata.DefaultView.ToTable(True, "company").Rows
                        'For Each dr As DataRow In G_DTExpenseData_Importdata.Rows
                        Dim datarow() As DataRow = mmDTCFBalance.Select("Symbol='" & Dr("Company") & "'")
                        If datarow.Length > 0 Then
                            PreBalance = datarow(0)("Balance")
                        Else
                            PreBalance = 0
                        End If
                        exp = Val(G_DTExpenseData_Importdata.Compute("sum(Expense)", "company='" & Dr("Company") & "' ").ToString) + PreBalance
                        UpdateCFBALNCE(exp, Dr("Company"))
                    Next


                End If
                REM (Viral 11-Aug-11)This Calculation is Not Need because Of New Calculation Of Expences  R Implimented 
                ' ''Dim str(1) As String
                ' ''str(0) = "security"
                ' ''str(1) = "entrydate"
                ' ''Dim dv As New DataView(tempdata)
                ' ''For Each row As DataRow In dv.ToTable(True, str).Rows
                ' ''    If CDate(row("entrydate")).Date < Now.Date Then
                ' ''        cal_prebal(CDate(row("entrydate")).Date, row("security"))
                ' ''    End If
                ' ''Next

                REM Commented By Viral 8-Aug-11 Because Of All Calculation Rec. in Analysis Window
                'objAna.process_data()

                MsgBox("File Processed Successfully.", MsgBoxStyle.Information)
                txtpath.Text = ""
            Else
                MsgBox("File not Processed.", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("Enter the file name to be import.", MsgBoxStyle.Exclamation)
        End If
        ' Catch ex As Exception
        'MsgBox("File  not processed")
        'MsgBox(ex.ToString)
        ' End Try
    End Sub
    ''' <summary>
    ''' ''' read data from excel file to tempdata which is use to assign the DataSource of Currency Grid view and calculation on it
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Currency_read_file()
        '  Try


        EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)
        'else If (EXPORT_IMPORT_POSITION = 1) Then
        'Label8.Text = "CSV Trade Path :"
        'ElseIf (EXPORT_IMPORT_POSITION = 2) Then
        'Label8.Text = " Excel Trade Path :"
        'End If
        Dim tempdata As New DataTable
        Dim a, a1, script1 As String
        Dim VarToken1 As Long
        tempdata = New DataTable
        Dim fpath As String
        Dim tk As Long
        fpath = CStr(txtpath.Text.Trim)

        If fpath <> "" Then
            If (EXPORT_IMPORT_POSITION = 2) Then

                tempdata = New DataTable
                Dim fi As New FileInfo(fpath)
                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" & fpath
                Dim objConn As New OleDbConnection(sConnectionString)
                objConn.Open()
                Dim objCmdSelect As New OleDbCommand("SELECT * FROM [Currency$] WHERE security <> ''", objConn)
                'Dim objCmdSelect As New OleDbCommand("SELECT strike,option_type,Exp. Date,Company,Qty,Rate,Instrument,entrydate FROM " & fi.Name, objConn)
                Dim objAdapter1 As New OleDbDataAdapter
                objAdapter1.SelectCommand = objCmdSelect
                objAdapter1.Fill(tempdata)
                objConn.Close()
                'fi.Delete()
                tempdata.Columns.Add("script")
                tempdata.Columns.Add("token1", GetType(Long))
                tempdata.Columns.Add("isliq")
                tempdata.Columns.Add("orderno", GetType(String))

                tempdata.AcceptChanges()

                If tempdata.Columns.Contains("Dealer") = False Then
                    tempdata.Columns.Add("Dealer", GetType(String))
                End If
            ElseIf (EXPORT_IMPORT_POSITION = 1) Then

                Dim fName As String = ""

                Dim fi As New FileInfo(fpath)
                Dim strdata As [String]()
                strdata = fpath.Split(New Char() {"\"c})
                Dim filename As String = strdata(strdata.Length - 1)
                Dim sConnectionStringz As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
                Dim objConn As New OleDbConnection(sConnectionStringz)
                objConn.Open()

                Dim objCmdSelect As New OleDbCommand("SELECT * FROM [" & filename & "] where security <> ''", objConn)
                Dim objAdapter1 As New OleDbDataAdapter
                objAdapter1.SelectCommand = objCmdSelect
                objAdapter1.Fill(tempdata)
                objConn.Close()

                tempdata.Columns.Add("script")
                tempdata.Columns.Add("token1", GetType(Long))
                tempdata.Columns.Add("isliq")
                tempdata.Columns.Add("orderno", GetType(String))
                'tempdata.Columns.Add("lActivityTime")
                'tempdata.Columns.Add("FileFlag")
                tempdata.AcceptChanges()

                If tempdata.Columns.Contains("Dealer") = False Then
                    tempdata.Columns.Add("Dealer", GetType(String))
                End If
            End If


            tempdata.Columns.Add("tot", GetType(Double))
            tempdata.Columns.Add("tot2", GetType(Double))

            For Each dr5 As DataRow In tempdata.Rows
                Dim strdata1 As [String]()
                Dim strValue As String


                If dr5("Dealer").Equals(DBNull.Value) = False Then


                    strValue = dr5("Dealer")
                    If strValue.Contains("'") Then
                        strdata1 = strValue.Split(New Char() {"'"c})
                        dr5("Dealer") = strdata1(1)
                    End If

                Else
                    dr5("Dealer") = "OP"
                End If
            Next

            Dim drow As DataRow
            Dim script As String
            Dim dv1 As New DataView(CurrencyMasterData)
            Dim cp As String
            For Each drow In tempdata.Select("security<>''")
                GVarMAXCURRTradingOrderNo = GVarMAXCURRTradingOrderNo + 1
                If Not IsDBNull(drow("security")) Then
                    If Not IsDBNull(drow("cpf")) Then
                        If Mid(drow("CPF"), 1, 1) = "C" Or Mid(drow("CPF"), 1, 1) = "P" Then
                            For Each cprow As DataRow In dv1.ToTable().Select("InstrumentName='" & UCase(drow("Instrument")) & "' AND symbol='" & GetSymbol(UCase(drow("security"))) & "'")
                                If UCase(Mid(cprow("option_type"), 1, 1)) = UCase(Mid(drow("CPF"), 1, 1)) Then
                                    cp = cprow("option_type")
                                    Exit For
                                End If
                            Next

                            Dim expdt As String
                            expdt = Format(CDate(drow("Exp# Date")), "ddMMMyyyy")
                            expdt.Substring(3, 1).ToUpper()
                            expdt.Substring(4, 2).ToLower()

                            script = drow("Instrument") & Space(2) & GetSymbol(drow("security")) & Space(2) & expdt & Space(2) & CStr(Format(Val(drow("strike")), "###0.0000")) & Space(2) & cp
                            drow("script") = (script.Trim).ToUpper
                            a = Mid(drow("script"), Len(drow("script")) - 1, 1)
                            a1 = Mid(drow("script"), Len(drow("script")), 1)
                            If a = "C" Then
                                script1 = Mid(drow("script"), 1, Len(drow("script")) - 2) & "P" & a1
                            Else
                                script1 = Mid(drow("script"), 1, Len(drow("script")) - 2) & "C" & a1
                            End If
                            tk = CLng(IIf(IsDBNull(Currencymaster.Compute("max(token)", "script='" & script1 & "'")), 0, Currencymaster.Compute("max(token)", "script='" & script1 & "'")))
                            drow("token1") = tk
                            drow("isliq") = False ' "No"
                            For Each row As DataRow In temptable.Select("script='" & drow("script") & "' And company='" & drow("security") & "'")
                                If row("isliq") = True Then
                                    drow("isliq") = True ' "Yes"
                                Else

                                    drow("isliq") = False ' "No"
                                End If
                                Exit For
                            Next
                        Else
                            Dim expdt As String
                            expdt = Format(CDate(drow("Exp# Date")), "ddMMMyyyy")
                            expdt.Substring(3, 1).ToUpper()
                            expdt.Substring(4, 2).ToLower()
                            script = drow("Instrument") & Space(2) & GetSymbol(drow("security")) & Space(2) & expdt
                            drow("script") = (script.Trim).ToUpper
                            drow("token1") = 0
                            drow("isliq") = False ' "No"
                        End If
                    Else
                        Dim expdt As String
                        expdt = Format(CDate(drow("Exp# Date")), "ddMMMyyyy")
                        expdt.Substring(3, 1).ToUpper()
                        expdt.Substring(4, 2).ToLower()
                        script = drow("Instrument") & Space(2) & GetSymbol(drow("security")) & Space(2) & expdt
                        drow("script") = (script.Trim).ToUpper
                        drow("token1") = 0
                        drow("isliq") = False ' "No"
                        drow("cpf") = ""
                    End If
                    drow("tot") = drow("Qty") * drow("ATP")
                    drow("tot2") = drow("Qty") * (Val(drow("ATP")) + Val(drow("Strike")))
                    Dim tk1 As Long = CLng(IIf(IsDBNull(Currencymaster.Compute("max(token)", "script='" & drow("script") & "'")), 0, Currencymaster.Compute("max(token)", "script='" & drow("script") & "'")))

                    If tk1 = 0 Then
                        MsgBox(drow("script") & " does not exist in contract")
                        Continue For
                    End If
                    ' drow("lActivityTime") = 0
                    ' drow("FileFlag") = 0
                    drow("Qty") = drow("Lots") * Currencymaster.Compute("MAX(multiplier)", "Script='" & drow("Script") & "'")
                End If
                drow("orderno") = GVarMAXCURRTradingOrderNo
            Next
            ' tempdata = New DataView(tempdata, "token1 >0", "token1", DataViewRowState.CurrentRows).ToTable
            If tempdata.Rows.Count > 0 Then
                objScript.Insert_Currency_Trading(tempdata)
                If Positionwithexpense = False Then
                    Dim exp As Double
                    Dim PreBalance As Double
                    Init_G_DTExpenseDataa_Importdata()
                    GSub_CalculateExpense_ImportData(tempdata, "CURR", True)
                    mmDTCFBalance = objTrad.Select_CFBalance()

                    For Each Dr As DataRow In G_DTExpenseData_Importdata.DefaultView.ToTable(True, "company").Rows
                        'For Each dr As DataRow In G_DTExpenseData_Importdata.Rows
                        Dim datarow() As DataRow = mmDTCFBalance.Select("Symbol='" & Dr("Company") & "'")
                        If datarow.Length > 0 Then
                            PreBalance = datarow(0)("Balance")
                        Else

                            objTrad.Insert_CFBalance(Dr("Company"), "0")
                            PreBalance = 0
                        End If
                        exp = Val(G_DTExpenseData_Importdata.Compute("sum(Expense)", "company='" & Dr("Company") & "' ").ToString) + PreBalance
                        UpdateCFBALNCE(exp, Dr("Company"))
                    Next
                End If
                REM (Viral 11-Aug-11)This Calculation is Not Need because Of New Calculation Of Expences  R Implimented 
                ' ''Dim str(1) As String
                ' ''str(0) = "security"
                ' ''str(1) = "entrydate"
                ' ''Dim dv As New DataView(tempdata)
                ' ''For Each row As DataRow In dv.ToTable(True, str).Rows
                ' ''    If CDate(row("entrydate")).Date <= Now.Date Then
                ' ''        Call cal_prebal(CDate(row("entrydate")).Date, row("security"))
                ' ''    End If
                ' ''Next

                REM Commented By Viral 8-Aug-11 Because Of All Calculation Rec. in Analysis Window
                'objAna.process_data()
                MsgBox("File Processed Successfully.", MsgBoxStyle.Information)
                txtpath.Text = ""
            Else
                MsgBox("File not Processed.", MsgBoxStyle.Information)
            End If

        Else
            MsgBox("Enter the file name to be import.", MsgBoxStyle.Exclamation)
        End If
        ' Catch ex As Exception
        'MsgBox("File  not processed")
        'MsgBox(ex.ToString)
        ' End Try
    End Sub
    ''' <summary>
    ''' Do Calculation for Previous Balance for existing Data
    ''' </summary>
    ''' <param name="date1"></param>
    ''' <param name="compname"></param>
    ''' <remarks></remarks>
    Private Sub cal_prebal_OutDated(ByVal date1 As Date, ByVal compname As String)
        Dim addprebal As New DataTable
        addprebal = New DataTable
        With addprebal.Columns
            .Add("tdate", GetType(Date))
            .Add("stbal", GetType(Double))
            .Add("futbal", GetType(Double))
            .Add("optbal", GetType(Double))
            .Add("company", GetType(String))
        End With
        Dim prow As DataRow
        Dim cpf As New DataTable
        Dim stk As New DataTable
        Dim currtrd As New DataTable

        Dim exptable As New DataTable
        Dim company As New DataTable
        exptable = objExp.Select_Expenses
        company = objTrad.Comapany
        objTrad.Trading(cpf)
        stk = objTrad.select_equity
        currtrd = objTrad.select_Currency_Trading

        Dim dv As DataView = New DataView(cpf)
        dv.RowFilter = "entry_date = #" & fDate(date1.Date) & "#"
        Dim dv1 As DataView = New DataView(stk)
        dv1.RowFilter = "entry_date = #" & fDate(date1.Date) & "#"
        Dim dv2 As DataView = New DataView(currtrd)
        dv2.RowFilter = "entry_date = #" & fDate(date1.Date) & "#"

        Dim stexp, stexp1, ndst, dst, exppr, expto As Double

        For Each crow As DataRow In company.Select("company='" & compname & "'")
            dv.RowFilter = " entry_date = #" & fDate(date1.Date) & "# and company='" & compname & "'"
            dv.Sort = "entry_date"
            Dim ttable As New DataTable
            ttable = dv.ToTable(True, "entry_date")
            For Each row As DataRow In ttable.Rows
                prow = addprebal.NewRow
                prow("tdate") = CDate(row("entry_date")).Date
                prow("stbal") = 0
                prow("futbal") = 0
                prow("optbal") = 0
                prow("company") = compname
                addprebal.Rows.Add(prow)
                stexp = 0
                stexp1 = 0
                dst = 0
                ndst = 0
                exppr = 0
                expto = 0

                'Equity ##################################################################
                stexp = Math.Round(Val(IIf(Not IsDBNull(stk.Compute("sum(tot)", "company='" & compname & "' and qty > 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#")), stk.Compute("sum(tot)", "company='" & compname & "' and qty > 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#"), 0)), 2)
                stexp1 = Math.Abs(Math.Round(Val(IIf(Not IsDBNull(stk.Compute("sum(tot)", "company='" & compname & "' and qty < 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#")), stk.Compute("sum(tot)", "company='" & compname & "' and qty < 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#"), 0)), 2))
                dst = stexp - stexp1
                If dst > 0 Then
                    ndst = stexp1
                    prow("stbal") = Val(prow("stbal")) + Val((dst * exptable.Compute("max(dbl)", "")) / exptable.Compute("max(dblp)", "")) + Val((stexp1 * exptable.Compute("max(ndbs)", "")) / exptable.Compute("max(ndbsp)", "")) + Val((stexp * exptable.Compute("max(ndbl)", "")) / exptable.Compute("max(ndblp)", ""))
                Else
                    ndst = stexp
                    prow("stbal") = Val(prow("stbal")) + Val((dst * exptable.Compute("max(dbs)", "")) / exptable.Compute("max(dbsp)", "")) + Val((stexp * exptable.Compute("max(ndbl)", "")) / exptable.Compute("max(ndblp)", "")) + Val((stexp1 * exptable.Compute("max(ndbs)", "")) / exptable.Compute("max(ndbsp)", ""))
                End If

                'Futre #################################################################
                stexp = 0
                stexp1 = 0
                stexp = Val(IIf(Not IsDBNull(cpf.Compute("sum(tot)", "cp not in ('C','P') and company='" & compname & "' and qty > 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#")), cpf.Compute("sum(tot)", "cp not in ('C','P') and company='" & compname & "'  and qty > 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#"), 0))
                stexp1 = Math.Abs(Val(IIf(Not IsDBNull(cpf.Compute("sum(tot)", "cp not in ('C','P') and company='" & compname & "' and qty < 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#")), cpf.Compute("sum(tot)", "cp not in ('C','P') and company='" & compname & "'  and qty < 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#"), 0)))
                prow("futbal") = Val(prow("futbal")) + Val((stexp * exptable.Compute("max(futl)", "")) / exptable.Compute("max(futlp)", "")) + Val((stexp1 * exptable.Compute("max(futs)", "")) / exptable.Compute("max(futsp)", ""))
                'Option ####################################################################
                stexp = 0
                stexp1 = 0
                stexp = Val(IIf(Not IsDBNull(cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "' and qty > 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#")), cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "'  and qty > 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#"), 0))
                stexp1 = Math.Abs(Val(IIf(Not IsDBNull(cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "' and qty < 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#")), cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "'  and qty < 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#"), 0)))
                If Val(exptable.Compute("max(spl)", "")) <> 0 Then
                    prow("optbal") = Val(prow("optbal")) + Val((stexp * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", "")) + Val((stexp1 * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", ""))
                Else
                    prow("optbal") = Val(prow("optbal")) + Val((stexp * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", "")) + Val((stexp1 * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", ""))
                End If

            Next
            ttable = dv2.ToTable(True, "entry_date")
            For Each row As DataRow In ttable.Rows
                prow = addprebal.NewRow
                prow("tdate") = CDate(row("entry_date")).Date
                prow("stbal") = 0
                prow("futbal") = 0
                prow("optbal") = 0
                prow("company") = compname
                addprebal.Rows.Add(prow)
                'Currency Futre #################################################################
                stexp = 0
                stexp1 = 0
                stexp = Val(IIf(Not IsDBNull(currtrd.Compute("sum(tot)", "cp not in ('C','P') and company='" & compname & "' and qty > 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#")), currtrd.Compute("sum(tot)", "cp not in ('C','P') and company='" & compname & "'  and qty > 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#"), 0))
                stexp1 = Math.Abs(Val(IIf(Not IsDBNull(currtrd.Compute("sum(tot)", "cp not in ('C','P') and company='" & compname & "' and qty < 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#")), currtrd.Compute("sum(tot)", "cp not in ('C','P') and company='" & compname & "'  and qty < 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#"), 0)))
                prow("futbal") = Val(prow("futbal")) + Val((stexp * exptable.Compute("max(currfutl)", "")) / exptable.Compute("max(currfutlp)", "")) + Val((stexp1 * currfuts) / currfutsp)
                'Currency Option ####################################################################
                stexp = 0
                stexp1 = 0
                stexp = Val(IIf(Not IsDBNull(currtrd.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "' and qty > 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#")), currtrd.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "'  and qty > 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#"), 0))
                stexp1 = Math.Abs(Val(IIf(Not IsDBNull(currtrd.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "' and qty < 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#")), currtrd.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "'  and qty < 0 and entry_date = #" & fDate(CDate(row("entry_date")).Date) & "#"), 0)))
                If Val(currspl) <> 0 Then
                    prow("optbal") = Val(prow("optbal")) + Val((stexp * exptable.Compute("max(currspl)", "")) / exptable.Compute("max(currsplp)", "")) + Val((stexp1 * currsps) / currspsp)
                Else
                    prow("optbal") = Val(prow("optbal")) + Val((stexp * exptable.Compute("max(currprel)", "")) / exptable.Compute("max(currprelp)", "")) + Val((stexp1 * currpres) / currpresp)
                End If
            Next
        Next
        objTrad.Delete_prBal(date1.Date, compname)
        objTrad.insert_prebal(addprebal)
    End Sub
    ''' <summary>
    ''' it import excel file and read its data and assign it to tempEq table and after calculation on it assign to grid EQ which display the Data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnImportEQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportEQ.Click
        Try
            Dim strresult As Integer = MsgBox("You Want to Import Position With Expense?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)

            If strresult = DialogResult.Yes Then
                Positionwithexpense = True
            Else
                Positionwithexpense = False

            End If
            Me.Cursor = Cursors.WaitCursor
            equity_read_file()
            Me.Cursor = Cursors.Default
            Call fill_all_table(e)
            Call fill_trades("EQ")
            Call UpdImpFlag()
            set_importposition_flag()
        Catch ex As Exception
            MsgBox("Invalid File.")
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    ''' <summary>
    ''' read data from excel file to tempdata which is use to assign the DataSource of EQ Grid view and calculation on it
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub equity_read_file()
        ' Try


        EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)
        'else If (EXPORT_IMPORT_POSITION = 1) Then
        'Label8.Text = "CSV Trade Path :"
        'ElseIf (EXPORT_IMPORT_POSITION = 2) Then
        'Label8.Text = " Excel Trade Path :"
        'End If




        Dim tempdata As New DataTable
        Dim a, a1, script1 As String
        Dim VarToken1 As Long
        tempdata = New DataTable
        Dim fpath As String
        Dim Payalcsv As String

        fpath = CStr(txtpath.Text.Trim)


        fpath = CStr(txtpath.Text.Trim)
        If fpath <> "" Then
            If (EXPORT_IMPORT_POSITION = 2) Then
                Dim fi As New FileInfo(fpath)

                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" & fpath
                'Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName

                Dim objConn As New OleDbConnection(sConnectionString)

                objConn.Open()

                Dim objCmdSelect As New OleDbCommand("SELECT * FROM [Equity$] where security<>''", objConn)


                Dim objAdapter1 As New OleDbDataAdapter

                objAdapter1.SelectCommand = objCmdSelect
                tempdata = New DataTable

                'tempdata.Columns.Add("Company")
                'tempdata.Columns.Add("eq")
                'tempdata.Columns.Add("Qty")
                'tempdata.Columns.Add("Rate")
                'tempdata.Columns.Add("entrydate")
                tempdata.AcceptChanges()
                objAdapter1.Fill(tempdata)
                objConn.Close()
                tempdata.Columns.Add("script")
                tempdata.Columns.Add("orderno", GetType(String))

                tempdata.AcceptChanges()

                If tempdata.Columns.Contains("Dealer") = False Then
                    tempdata.Columns.Add("Dealer", GetType(String))
                End If
            ElseIf (EXPORT_IMPORT_POSITION = 1) Then

                Dim fName As String = ""

                Dim fi As New FileInfo(fpath)
                Dim strdata As [String]()
                strdata = fpath.Split(New Char() {"\"c})
                Dim filename As String = strdata(strdata.Length - 1)
                Dim sConnectionStringz As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
                Dim objConn As New OleDbConnection(sConnectionStringz)
                objConn.Open()

                Dim objCmdSelect As New OleDbCommand("SELECT * FROM [" & filename & "] where security <> ''", objConn)
                Dim objAdapter1 As New OleDbDataAdapter
                objAdapter1.SelectCommand = objCmdSelect
                objAdapter1.Fill(tempdata)
                objConn.Close()

                tempdata.Columns.Add("script")
                'tempdata.Columns.Add("token1", GetType(Long))
                'tempdata.Columns.Add("isliq")
                tempdata.Columns.Add("orderno", GetType(String))
                tempdata.Columns.Add("lActivityTime")
                tempdata.Columns.Add("FileFlag")
                tempdata.AcceptChanges()

                If tempdata.Columns.Contains("Dealer") = False Then
                    tempdata.Columns.Add("Dealer", GetType(String))
                End If
            End If

            tempdata.Columns.Add("tot", GetType(Double))
            'tempdata.Columns.Add("tot2", GetType(Double))

            For Each dr5 As DataRow In tempdata.Rows
                Dim strdata1 As [String]()
                Dim strValue As String


                If dr5("Dealer").Equals(DBNull.Value) = False Then


                    strValue = dr5("Dealer")
                    If strValue.Contains("'") Then
                        strdata1 = strValue.Split(New Char() {"'"c})
                        dr5("Dealer") = strdata1(1)
                    End If

                Else
                    dr5("Dealer") = "OP"
                End If
            Next


            Dim drow As DataRow
            Dim script As String
            For Each drow In tempdata.Rows
                GVarMAXEQTradingOrderNo = GVarMAXEQTradingOrderNo + 1
                script = drow("security").trim() & Space(2) & drow("eq").ToString.Trim.ToUpper()
                drow("script") = UCase(script.Trim).ToUpper
                drow("orderno") = GVarMAXEQTradingOrderNo
                drow("tot") = drow("Qty") * drow("ATP")
                drow("entrydate") = CDate(CDate(drow("entrydate")).ToString("dd/MMM/yyyy") & " " & Date.Now.ToString("hh:mm:ss tt"))
                'drow("tot2") = drow("Qty") * (Val(drow("ATP")) + Val(drow("Strike")))
            Next
            If tempdata.Rows.Count > 0 Then
                objScript.Insert_eqtrading(tempdata)
                If Positionwithexpense = False Then
                    Dim exp As Double
                    Dim PreBalance As Double
                    Init_G_DTExpenseDataa_Importdata()
                    GSub_CalculateExpense_ImportData(tempdata, "EQ", True)
                    mmDTCFBalance = objTrad.Select_CFBalance()

                    For Each Dr As DataRow In G_DTExpenseData_Importdata.DefaultView.ToTable(True, "company").Rows
                        'For Each dr As DataRow In G_DTExpenseData_Importdata.Rows
                        Dim datarow() As DataRow = mmDTCFBalance.Select("Symbol='" & Dr("Company") & "'")
                        If datarow.Length > 0 Then
                            PreBalance = datarow(0)("Balance")
                        Else
                          
                            objTrad.Insert_CFBalance(Dr("Company"), "0")
                            PreBalance = 0
                        End If
                        exp = Val(G_DTExpenseData_Importdata.Compute("sum(Expense)", "company='" & Dr("Company") & "' ").ToString) + PreBalance
                        UpdateCFBALNCE(exp, Dr("Company"))
                    Next


                End If
                REM (Viral 11-Aug-11)This Calculation is Not Need because Of New Calculation Of Expences  R Implimented 
                ' ''Dim str(1) As String
                ' ''str(0) = "security"
                ' ''str(1) = "entrydate"
                ' ''Dim dv As New DataView(tempdata)
                ' ''For Each row As DataRow In dv.ToTable(True, str).Rows
                ' ''    If CDate(row("entrydate")).Date < Now.Date Then
                ' ''        cal_prebal(CDate(row("entrydate")).Date, row("security"))
                ' ''    End If
                ' ''Next

                REM Commented By Viral 8-Aug-11 Because Of All Calculation Rec. in Analysis Window
                'objAna.process_data()

                MsgBox("File Processed Successfully.", MsgBoxStyle.Information)
                txtpath.Text = ""
            Else
                MsgBox("File Not Processed.", MsgBoxStyle.Information)
            End If
            'MsgBox("File Process Successfully")
        Else
            MsgBox("Enter the file name to be import.", MsgBoxStyle.Exclamation)
        End If
        ' Catch ex As Exception
        'MsgBox("File Not Processed")
        ' MsgBox(ex.ToString)
        ' End Try
    End Sub

    ''' <summary>
    ''' This Export data of Currency Grid View to Excel File
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub exportcurr(ByVal auto As Boolean)
        EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)

        If (EXPORT_IMPORT_POSITION = 2) Then
            If auto = True Then
                If DGCurrencyPosition.Rows.Count > 0 Then
                    Dim mBackupPath As String = CStr(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")), "D:\", objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")))
                    Dim strpath As String = mBackupPath & "\Curr_POS_ " & Format(Now, "dd-MMM-yyyy hh mm ss tt") & ".xls"

                    'exportExcel(grdeq, savedi.FileName)
                    Dim grd(0) As DataGridView
                    grd(0) = DGCurrencyPosition
                    Dim sname(0) As String
                    sname(0) = "Currency"
                    Call exporttoexcel(grd, strpath, sname, "CURRENCY")

                End If
            Else
                If DGCurrencyPosition.Rows.Count > 0 Then
                    Dim savedi As New SaveFileDialog
                    savedi.Filter = "Files(*.XLS)|*.XLS"
                    If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                        'exportExcel(grdeq, savedi.FileName)
                        Dim grd(0) As DataGridView
                        grd(0) = DGCurrencyPosition
                        Dim sname(0) As String
                        sname(0) = "Currency"
                        Call exporttoexcel(grd, savedi.FileName, sname, "CURRENCY")
                        MsgBox("Export Successfully")
                        OPEN_Export_File(savedi.FileName)
                    End If
                End If
            End If


        ElseIf (EXPORT_IMPORT_POSITION = 1) Then
            If DGCurrencyPosition.Rows.Count > 0 Then
                If auto = True Then
                    Dim mBackupPath As String = CStr(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")), "D:\", objTrad.Settings.Compute("max(SettingKey)", "SettingName='Backup_path'")))
                    Dim strpath As String = mBackupPath & "\Curr_POS_ " & Format(Now, "dd-MMM-yyyy hh mm ss tt") & ".Csv"
                    Dim Dt As New DataTable
                    Dt = CType(DGCurrencyPosition.DataSource, DataTable)
                    Dim name(Dt.Columns.Count) As String
                    Dim j As Integer = 0
                    Dim dtgrd As DataTable
                    Dim dr As DataRow
                    dtgrd = New DataTable
                    With dtgrd.Columns
                        .Add("Scrip")
                        .Add("Instrument")
                        .Add("Security")
                        .Add("CPF")
                        .Add("Exp. Date", GetType(String))
                        .Add("Entrydate", GetType(Date))
                        .Add("Strike", GetType(Double))
                        .Add("Qty", GetType(Double))
                        .Add("Lots", GetType(Double))
                        .Add("ATP", GetType(Double))
                        .Add("exp_date", GetType(Date))
                        .Add("Dealer")
                    End With
                    Dim cal As DataRow
                    ' DgvExpense.DataSource = DtGrd
                    dr = dtgrd.NewRow()
                    For Each dr5 As DataRow In Dt.Rows
                        cal = dtgrd.NewRow()
                        cal("Scrip") = dr5("Script")
                        cal("Instrument") = dr5("Instrument")
                        cal("Security") = dr5("company")
                        cal("CPF") = dr5("CPF")
                        cal("Exp. Date") = CDate(dr5("MDate")).ToString("dd-MMM-yyyy")
                        cal("Entrydate") = dr5("Entrydate")
                        cal("Strike") = dr5("Strike")
                        cal("Qty") = dr5("Qty")
                        cal("Lots") = dr5("Lots")
                        cal("ATP") = dr5("traded")
                        cal("exp_date") = dr5("MDate")
                        cal("Dealer") = "'" & dr5("Dealer")

                        dtgrd.Rows.Add(cal)

                        dtgrd.AcceptChanges()


                    Next

                    'Dim sname(0) As String
                    'sname(0) = "Trading"
                    exporttocsv(dtgrd, strpath, "CURRENCY")
                Else
                    Dim savedi As New SaveFileDialog
                    savedi.Filter = "File(*.csv)|*.Csv"
                    If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                        'exportExcel(grdtrad, savedi.FileName)
                        'exportExcel(grdeq, savedi.FileName)
                        'Dim grd As DataGridView
                        'grd = DGCurrencyPosition

                        Dim Dt As New DataTable
                        Dt = CType(DGCurrencyPosition.DataSource, DataTable)
                        Dim name(Dt.Columns.Count) As String
                        Dim j As Integer = 0
                        Dim dtgrd As DataTable
                        Dim dr As DataRow
                        dtgrd = New DataTable
                        With dtgrd.Columns
                            .Add("Scrip")
                            .Add("Instrument")
                            .Add("Security")
                            .Add("CPF")
                            .Add("Exp. Date", GetType(String))
                            .Add("Entrydate", GetType(Date))
                            .Add("Strike", GetType(Double))
                            .Add("Qty", GetType(Double))
                            .Add("Lots", GetType(Double))
                            .Add("ATP", GetType(Double))
                            .Add("exp_date", GetType(Date))
                            .Add("Dealer")
                        End With
                        Dim cal As DataRow
                        ' DgvExpense.DataSource = DtGrd
                        dr = dtgrd.NewRow()
                        For Each dr5 As DataRow In Dt.Rows
                            cal = dtgrd.NewRow()
                            cal("Scrip") = dr5("Script")
                            cal("Instrument") = dr5("Instrument")
                            cal("Security") = dr5("company")
                            cal("CPF") = dr5("CPF")
                            cal("Exp. Date") = CDate(dr5("MDate")).ToString("dd-MMM-yyyy")
                            cal("Entrydate") = dr5("Entrydate")
                            cal("Strike") = dr5("Strike")
                            cal("Qty") = dr5("Qty")
                            cal("Lots") = dr5("Lots")
                            cal("ATP") = dr5("traded")
                            cal("exp_date") = dr5("MDate")
                            cal("Dealer") = "'" & dr5("Dealer")

                            dtgrd.Rows.Add(cal)

                            dtgrd.AcceptChanges()


                        Next

                        'Dim sname(0) As String
                        'sname(0) = "Trading"
                        exporttocsv(dtgrd, savedi.FileName, "CURRENCY")

                        MsgBox("Export Successfully")
                        OPEN_Export_File(savedi.FileName)
                    End If

                End If
            End If
        End If
    End Sub
    Private Sub btnExportCurrency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportCurrency.Click

        exportcurr(False)
    End Sub
    'Private Sub btnExportCurrency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportCurrency.Click

    '    EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)

    '    If (EXPORT_IMPORT_POSITION = 2) Then
    '        If DGCurrencyPosition.Rows.Count > 0 Then
    '            Dim savedi As New SaveFileDialog
    '            savedi.Filter = "Files(*.XLS)|*.XLS"
    '            If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
    '                'exportExcel(grdeq, savedi.FileName)
    '                Dim grd(0) As DataGridView
    '                grd(0) = DGCurrencyPosition
    '                Dim sname(0) As String
    '                sname(0) = "Currency"
    '                Call exporttoexcel(grd, savedi.FileName, sname, "CURRENCY")
    '                'MsgBox("Export Sucessfully")
    '            End If
    '        End If
    '    ElseIf (EXPORT_IMPORT_POSITION = 1) Then
    '        If DGCurrencyPosition.Rows.Count > 0 Then
    '            Dim savedi As New SaveFileDialog
    '            savedi.Filter = "File(*.csv)|*.Csv"
    '            If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
    '                'exportExcel(grdtrad, savedi.FileName)
    '                'exportExcel(grdeq, savedi.FileName)
    '                Dim grd As DataGridView
    '                grd = DGCurrencyPosition
    '                'Dim sname(0) As String
    '                'sname(0) = "Trading"
    '                exporttocsvcurr(DGCurrencyPosition, savedi.FileName, "CURRENCY")
    '                MsgBox("Export Sucessfully")
    '            End If
    '        End If
    '    End If
    'End Sub

    ''' <summary>
    ''' it import excel file and read its data and assign it to tempCurrency table and after calculation on it assign to grid Currency which display the Data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnImportCurrency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportCurrency.Click
        Try
            Dim strresult As Integer = MsgBox("You Want to Import Position With Expense?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)

            If strresult = DialogResult.Yes Then
                Positionwithexpense = True
            Else
                Positionwithexpense = False

            End If
            Me.Cursor = Cursors.WaitCursor
            Call Currency_read_file()
            Me.Cursor = Cursors.Default
            Call fill_all_table(e)
            Call fill_trades("CURR")
            Call UpdImpFlag()
            set_importposition_flag()
        Catch ex As Exception
            MsgBox("Invalid File.")
            Me.Cursor = Cursors.Default
        End Try

    End Sub



#Region "For Short Cut Key Handles by Manu"
    ''' <summary>
    ''' This Export data of FO Grid View to Excel File
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExportFOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportFOToolStripMenuItem.Click
        Call btnExportFO_Click(sender, e)
    End Sub

    ''' <summary>
    ''' This Export data of EQ Grid View to Excel File
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExportEQToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportEQToolStripMenuItem.Click
        Call btnExportEQ_Click(sender, e)
    End Sub

    ''' <summary>
    ''' This Export data of Currency Grid View to Excel File
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExportCurrencyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportCurrencyToolStripMenuItem.Click
        Call btnExportCurrency_Click(sender, e)
    End Sub

    ''' <summary>
    '''  it import excel file and read its data and assign it to tempFo table and after calculation on it assign to grid Fo which display the Data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ImportFOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportFOToolStripMenuItem.Click
        Call btnImportFO_Click(sender, e)
    End Sub

    ''' <summary>
    '''  it import excel file and read its data and assign it to tempEq table and after calculation on it assign to grid EQ which display the Data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ImportEQToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportEQToolStripMenuItem.Click
        Call btnImportEQ_Click(sender, e)
    End Sub

    ''' <summary>
    ''' it import excel file and read its data and assign it to tempCurrency table and after calculation on it assign to grid Currency which display the Data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ImportCurrencyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportCurrencyToolStripMenuItem.Click
        Call btnImportCurrency_Click(sender, e)
    End Sub
#End Region

    Private Sub btnhelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhelp.Click
        HELP.ShowDialog()

    End Sub

    Private Sub DGEQPosition_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGEQPosition.CellContentClick

    End Sub
End Class