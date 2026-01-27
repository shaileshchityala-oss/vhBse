Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL
Public Class ScriptMapingFunctionality


    Dim objScript As script = New script
    Dim masterdata As DataTable = New DataTable
    Dim dtUid As New DataTable
    Dim eqmasterdata As DataTable = New DataTable
    Dim objTrad As New trading
    Dim drclient As DataRow
    Dim DTCurrencyContract As DataTable = New DataTable
    Dim exp As New expenses

    Dim cmbheight As Boolean = False
    Dim cmbh As Integer
    Dim objAna As New analysisprocess
    Public openposition As Boolean
    Dim Varmultiplier As Double = 0
    Public openposyes As Boolean = False
    Dim IsUserDefTag As Boolean = False
    Dim sUdSymbol As String = ""
    Dim sUdName As String = ""
    Dim sClientCode As String = ""


    Private Sub ScriptMapingFunctionality_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)
        If IsScriptMapper = True Then
            MsgBox("Changes Apply.." & vbCrLf & " Need To Restarte Application.")
            Application.Exit()
        End If
    End Sub



    Private Sub ScriptMapingFunctionality_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '  If masterdata.Rows.Count = 0 Then



        DTCurrencyContract = New DataTable
        DTCurrencyContract = Currencymaster
        CmbSecurityCurr.DisplayMember = "symbol"
        CmbSecurityCurr.ValueMember = "symbol"
        'Dim dvCurr As DataView = New DataView(DTCurrencyContract, "", "symbol", DataViewRowState.CurrentRows)
        Dim dvCurr As DataView = New DataView(DTCurrencyContract, "symbol = '" & sUdSymbol & "' And instrumentName='FUTCUR' or instrumentName='FUTIRD' or instrumentName='INDEX' or instrumentName='UNDCUR' or instrumentName='UNDIRD' or instrumentName='OPTCUR' OR  instrumentName='FUTIRC' OR  instrumentName='FUTIRT' ", "symbol", DataViewRowState.CurrentRows)
        CmbSecurityCurr.DataSource = dvCurr.ToTable(True, "symbol")
        'cmbCurrencyComp.DisplayMember = "symbol"
        'cmbCurrencyComp.ValueMember = "symbol"
        ' cmbeqopt.SelectedValue = "Symbol"
        CmbSecurityCurr.SelectAll()
        '  End If
        '   CmbSecurityCurr.Select()

        masterdata = New DataTable
        masterdata = cpfmaster
        Dim dv As DataView = New DataView(masterdata, "", "symbol", DataViewRowState.CurrentRows)
        CmbSecurityFut.DataSource = dv.ToTable(True, "symbol")
        CmbSecurityFut.DisplayMember = "symbol"
        CmbSecurityFut.ValueMember = "symbol"
        CmbSecurityFut.DataSource = dv.ToTable(True, "symbol")
        If dv.ToTable(True, "symbol").Compute("count(symbol)", "symbol='NIFTY'") > 0 Then
            CmbSecurityFut.SelectedValue = "NIFTY"
            CmbSecurityFut.Select()
        End If
        '  End If


        ' If eqmasterdata.Rows.Count = 0 Then

        'If DTCurrencyContract.Rows.Count = 0 Then

        eqmasterdata = New DataTable
        eqmasterdata = eqmaster
        Application.DoEvents()

        Dim dv1 As DataView = New DataView(eqmasterdata, "", "symbol", DataViewRowState.CurrentRows)

        '     Application.DoEvents()
        'cmbeqcomp.DisplayMember = "symbol"
        'cmbeqcomp.ValueMember = "symbol"
        'If eqmasterdata.Rows.Count > 0 Then
        '    'cmbeqcomp.SelectedValue = dv1.ToTable(True, "Symbol").Rows(1).Item(0).ToString()
        '    'cmbeqcomp.Select()
        '    cmbeqcomp.SelectedIndex = 1 'dv1.ToTable(True, "Symbol").Rows(1).Item(0).ToString()

        'End If
        '   cmbeqcomp.SelectedValue = 

        ' End If
        CheckedListBox1.Items.Clear()

        Dim eqdata As DataTable
        eqdata = getEQscript()
        Dim dv2 As DataView = New DataView(eqdata, "", "Script", DataViewRowState.CurrentRows)
        For Each rowView As DataRowView In dv2
            Dim dr As DataRow = rowView.Row
            ' Do something '
            CheckedListBox1.Items.Add(dr("Script"))
        Next

    End Sub
    Public Function getEQscript() As DataTable
        Dim slab As String
        'Dim slabno As String
        Dim Eqdt As DataTable
        Try
            slab = "SELECT DISTINCT Script FROM equity_trading ;"

            data_access.ParamClear()
            data_access.Cmd_Text = slab
            data_access.cmd_type = CommandType.Text

            '   data_access.ExecuteNonQuery()

            data_access_sql.Cmd_Text = slab
            Eqdt = data_access.FillList()
            Return Eqdt


        Catch ex As Exception
        End Try
    End Function
    Private Sub BtnProcessEq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnProcessEq.Click
        UpdateEquity()
        IsScriptMapper = True
    End Sub
    Public Function UpdateEquity() As Integer
        If CheckedListBox1.CheckedItems.Count > 0 Then
            Dim serious As String
            Dim Security As String
            Dim Script1 As String
            Dim list As New DataTable
            list = New DataTable
            list.Columns.Add("Script", GetType(String))
            For i As Integer = 0 To CheckedListBox1.CheckedItems.Count - 1
                Dim dr As DataRow = list.NewRow()
                dr("Script") = CheckedListBox1.CheckedItems(i)
                list.Rows.Add(dr)
            Next


            list.AcceptChanges()
            '  Dim Security As String
            For Each dr As DataRow In list.Rows


                '' Security = TxtSecurityEq.Text

                Dim strdata As [String]()
                strdata = dr("script").Split(New Char() {" "c})
                'strdata = dr("script").Split("  ", StringSplitOptions.None)
                Security = strdata(0)
                serious = strdata(2)


                'serious = dr("Script")
                'script = Security + "  " + serious
                Script1 = Security & "  " & TxtSeriesEquity.Text
                Dim qry As String
                Try
                    qry = "UPDATE equity_trading SET company = '" + Security + "',eq='" + TxtSeriesEq.Text + "',script='" + Script1 + "' where Script='" + dr("Script") + "'  "

                    data_access.ParamClear()
                    data_access.Cmd_Text = qry
                    data_access.cmd_type = CommandType.Text
                    data_access.ExecuteNonQuery()

                Catch ex As Exception
                    MsgBox("Error to Update Data")

                End Try
            Next
            MsgBox("Successfully Updated...")
        End If
    End Function

    Private Sub BtnProcessFut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnProcessFut.Click
        UpdateFuture()
        IsScriptMapper = True
    End Sub
    Public Function UpdateFuture() As Integer

        Dim Security As String
        Dim Instname As String
        Dim cp As String
        Dim expiry As Date
        Dim expiry1 As Date
        Dim script As String
        Dim script1 As String
        Security = TxtSecurityFut.Text
        Instname = TxtInstnameFut.Text
        cp = TxtCallPutFut.Text
        expiry = TxtExpiryFut.Text


        'If cp = "" Then
        '    script = CmbInstru.Text & "  " & CmbComp.Text & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy")
        'Else
        '    script = CmbInstru.Text & "  " & CmbComp.Text & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy") & "  " & Format(Val(cmbstrike.Text), "###0.00") & "  " & cp
        'End If
        '  Format(drow("mdate"), "MM/yyyy")
        expiry1 = CmbExpiryFut.Text
        script = Instname + "  " + Security + "  " + Format(CDate(CmbExpiryFut.Text), "ddMMMyyyy").ToUpper()
        script1 = CmbInstnameFut.Text & "  " & CmbSecurityFut.Text & "  " & Format(CDate(CmbExpiryFut.Text), "ddMMMyyyy").ToUpper()
        Dim qry As String
        Try
            qry = "UPDATE trading SET company = '" + Security + "',instrumentname='" + Instname + "',cp='" + cp + "',mdate='" + expiry + "',script='" + script + "' where script='" + script1 + "'"



            data_access.ParamClear()
            data_access.Cmd_Text = qry
            data_access.cmd_type = CommandType.Text
            data_access.ExecuteNonQuery()
            data_access.cmd_type = CommandType.StoredProcedure
            MsgBox("Successfully Updated...")
        Catch ex As Exception
            MsgBox("Error to Update Data")

        End Try
    End Function

    Private Sub ScriptMapingFunctionality_FormClosed1(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If IsScriptMapper = True Then
            MsgBox("Changes Apply.." & vbCrLf & " Need To Restarte Application.")
            Application.Exit()
        End If
    End Sub
    Private Sub ScriptMapingFunctionality_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub BtnProcessCurr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnProcessCurr.Click
        UpdateCurrency()
        IsScriptMapper = True
    End Sub


    Private Sub CmbInstnameFut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbInstnameFut.KeyDown
        If e.KeyCode = Keys.Enter Then
            If CmbInstnameFut.SelectedIndex = 0 Then
                CmbInstnameFut.Select()
            Else
                CmbCallPutFut.Select()
            End If
        End If
    End Sub
    Private Sub CmbInstnameFut_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbInstnameFut.Leave
        'CmbCallPutFut.Enabled = True
        ' cmbstrike.Enabled = True
        If CmbInstnameFut.Text.Trim <> "" And CmbInstnameFut.Items.Count > 0 Then
            If (UCase(CmbInstnameFut.Text) = "FUTIDX" Or UCase(CmbInstnameFut.Text) = "FUTSTK" Or UCase(CmbInstnameFut.Text) = "OPTIDX" Or UCase(CmbInstnameFut.Text) = "OPTSTK") Then
                Dim dv As DataView = New DataView(masterdata, "InstrumentName='" & CmbInstnameFut.Text & "'", "option_type", DataViewRowState.CurrentRows)

                CmbCallPutFut.DisplayMember = "option_type"
                CmbCallPutFut.ValueMember = "option_type"
                CmbCallPutFut.DataSource = dv.ToTable(True, "option_type")
                'If cmbcp.Items.Count > 0 Then
                '    cmbcp.SelectedIndex = 0
                'End If
                If Not CmbInstnameFut Is Nothing And CmbInstnameFut.Items.Count > 0 Then
                    If CmbInstnameFut.Text <> "" Then
                        If UCase(Mid(CmbInstnameFut.Text, 1, 3)) = "FUT" Then
                            CmbCallPutFut.Enabled = False
                            ' cmbstrike.Enabled = False
                            'cmbstrike.Text = 0
                            dv = New DataView(masterdata, "symbol='" & CmbSecurityFut.Text & "' and InstrumentName='" & CmbInstnameFut.Text & "'  AND expdate1 >='" & Now.Date & "' ", "strike_price", DataViewRowState.CurrentRows)
                            CmbExpiryFut.DataSource = dv.ToTable(True, "expdate")
                            CmbExpiryFut.DisplayMember = "expdate"
                            CmbExpiryFut.ValueMember = "expdate"
                            'cmbdate.Focus()
                        Else
                            CmbCallPutFut.Enabled = True
                            'cmbstrike.Enabled = True
                        End If
                    End If
                End If
            Else
                'MsgBox("Select valid Instrument Name.")
                'CmbInstnameFut.Text = ""
                'CmbInstnameFut.Focus()
            End If
        End If
        If CmbInstnameFut.Text.Trim = "" Then
            CmbCallPutFut.DataSource = Nothing
            CmbCallPutFut.DataSource = Nothing
            CmbExpiryFut.DataSource = Nothing
        End If
    End Sub
    Private Sub CmbInstnameFut_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbInstnameFut.SelectedIndexChanged
        TxtInstnameFut.Text = CmbInstnameFut.Text()
        If Not CmbInstnameFut Is Nothing And CmbInstnameFut.Items.Count > 0 Then
            If CmbInstnameFut.Text <> "" Then
                If UCase(Mid(CmbInstnameFut.Text, 1, 3)) = "FUT" Then
                    ' cmbstrike.DataSource = Nothing

                    'cmbstrike.Text = 0
                    Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbSecurityFut.Text & "' and InstrumentName='" & CmbInstnameFut.Text & "' and option_type='" & CmbCallPutFut.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
                    CmbExpiryFut.DataSource = dv.ToTable(True, "expdate")
                    CmbExpiryFut.DisplayMember = "expdate"
                    CmbExpiryFut.ValueMember = "expdate"
                    Dim dvCP As DataView = New DataView(masterdata, "InstrumentName='" & CmbInstnameFut.Text & "'", "option_type", DataViewRowState.CurrentRows)
                    CmbCallPutFut.DataSource = dvCP.ToTable(True, "option_type")
                    CmbCallPutFut.DisplayMember = "option_type"
                    CmbCallPutFut.ValueMember = "option_type"
                    CmbCallPutFut.Enabled = False
                    'cmbstrike.Enabled = False
                    'cmbdate.Refresh()
                    'cmbdate.Focus()

                Else
                    Dim dv As DataView = New DataView(masterdata, "InstrumentName='" & CmbInstnameFut.Text & "'", "option_type", DataViewRowState.CurrentRows)
                    CmbCallPutFut.DataSource = dv.ToTable(True, "option_type")
                    CmbCallPutFut.DisplayMember = "option_type"
                    CmbCallPutFut.ValueMember = "option_type"
                    CmbCallPutFut.Enabled = True
                    '  cmbstrike.Enabled = True
                End If
            End If
        End If

    End Sub
    Private Sub CmbSecurityFut_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSecurityFut.Leave
        If CmbSecurityFut.Text.Trim <> "" And CmbSecurityFut.Items.Count > 0 Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & CmbSecurityFut.Text & "'", "InstrumentName", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                '    MsgBox("Select valid Security.")
                '    CmbSecurityFut.Text = ""
                '    CmbSecurityFut.Focus()
                '    Exit Sub
            End If
            CmbInstnameFut.DisplayMember = "InstrumentName"
            CmbInstnameFut.ValueMember = "InstrumentName"
            CmbInstnameFut.DataSource = dv.ToTable(True, "InstrumentName")

            'If CmbComp.SelectedText <> "" Then
            '    CmbInstru.SelectedIndex = 0
            'End If
        End If
        If CmbSecurityFut.Text.Trim = "" Then
            CmbInstnameFut.DataSource = Nothing
            CmbCallPutFut.DataSource = Nothing
            ' cmbstrike.DataSource = Nothing
            CmbExpiryFut.DataSource = Nothing
        End If
    End Sub
    Private Sub CmbSecurityFut_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbSecurityFut.SelectedIndexChanged
        TxtSecurityFut.Text = CmbSecurityFut.Text
    End Sub

    Private Sub CmbSecurityCurr_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbSecurityCurr.KeyDown
        If e.KeyCode = Keys.Enter Then
            CmbInstnameCurr.Select()
        End If
    End Sub

    Private Sub CmbSecurityCurr_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSecurityCurr.Leave
        If CmbSecurityCurr.Text.Trim <> "" And CmbSecurityCurr.Items.Count > 0 Then
            Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & CmbSecurityCurr.Text & "'", "InstrumentName", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                'MsgBox("Select valid Security.")
                'CmbSecurityCurr.Text = ""
                'CmbSecurityCurr.Focus()
                'Exit Sub
            End If

            CmbInstnameCurr.DisplayMember = "InstrumentName"
            CmbInstnameCurr.ValueMember = "InstrumentName"
            CmbInstnameCurr.DataSource = dv.ToTable(True, "InstrumentName")
            'CmbInstru.Refresh()
            'cmbCurrencyInstrument.SelectedIndex = 0
        End If
        If CmbSecurityCurr.Text.Trim = "" Then
            CmbInstnameCurr.DataSource = Nothing
            CmbCallPutCurr.DataSource = Nothing
            '  cmbCurrencyStrike.DataSource = Nothing
            CmbExpiryCurr.DataSource = Nothing
        End If

    End Sub

    Private Sub CmbSecurityCurr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbSecurityCurr.SelectedIndexChanged
        TxtSecurityCurr.Text = CmbSecurityCurr.Text
    End Sub

    Private Sub CmbInstnameCurr_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbInstnameCurr.KeyDown
        If e.KeyCode = Keys.Enter Then
            If CmbCallPutCurr.SelectedIndex = 0 Then
                CmbExpiryCurr.Select()
            Else
                CmbCallPutCurr.Select()
            End If
        End If
    End Sub

    Private Sub CmbInstnameCurr_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbInstnameCurr.Leave
        'cmbc.Enabled = True
        'cmbCurrencyStrike.Enabled = True
        CmbInstnameCurr.Height = cmbh
        If CmbInstnameCurr.Text.Trim <> "" And CmbInstnameCurr.Items.Count > 0 Then
            If (UCase(CmbInstnameCurr.Text) = "FUTCUR" Or UCase(CmbInstnameCurr.Text) = "FUTIRD" Or UCase(CmbInstnameCurr.Text) = "INDEX" Or UCase(CmbInstnameCurr.Text) = "UNDCUR" Or UCase(CmbInstnameCurr.Text) = "UNDIRD" Or UCase(CmbInstnameCurr.Text) = "OPTCUR") Then
                Dim dv As DataView = New DataView(DTCurrencyContract, "InstrumentName='" & CmbInstnameCurr.Text & "'", "option_type", DataViewRowState.CurrentRows)
                CmbCallPutCurr.DataSource = dv.ToTable(True, "option_type")
                CmbCallPutCurr.DisplayMember = "option_type"
                CmbCallPutCurr.ValueMember = "option_type"
                'cmbcp.Refresh()
                If CmbCallPutCurr.Items.Count > 0 Then
                    CmbCallPutCurr.SelectedIndex = 0
                End If
                If Not CmbInstnameCurr Is Nothing And CmbInstnameCurr.Items.Count > 0 Then
                    If CmbInstnameCurr.Text <> "" Then
                        If UCase(CmbInstnameCurr.Text) <> "OPTCUR" Then
                            CmbCallPutCurr.Enabled = False
                            ' cmbCurrencyStrike.DataSource = Nothing

                            ' cmbCurrencyStrike.Enabled = False
                            '  cmbCurrencyStrike.Text = 0
                            dv = New DataView(DTCurrencyContract, "symbol='" & CmbSecurityCurr.Text & "' AND InstrumentName='" & CmbInstnameCurr.Text & "' and option_type='" & CmbCallPutCurr.Text & "' AND expdate1 >='" & Now.Date & "' ", "mdate", DataViewRowState.CurrentRows) 'expdate1
                            CmbExpiryCurr.DataSource = dv.ToTable(True, "expdate")
                            CmbExpiryCurr.DisplayMember = "expdate"
                            CmbExpiryCurr.ValueMember = "expdate"
                            '  cmbdate.Refresh()
                            ' cmbdate.Focus()
                        Else
                            CmbCallPutCurr.Enabled = True
                            ' cmbCurrencyStrike.Enabled = True
                            ' cmbcp.Focus()
                        End If
                    End If
                End If
            Else
                MsgBox("Select valid Instrument Name.")
                CmbInstnameCurr.Text = ""
                CmbInstnameCurr.Focus()
            End If

        End If
        If CmbInstnameCurr.Text.Trim = "" Then
            CmbCallPutCurr.DataSource = Nothing
            ' cmbCurrencyStrike.DataSource = Nothing
            CmbExpiryCurr.DataSource = Nothing
        End If

    End Sub

    Private Sub CmbInstnameCurr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbInstnameCurr.SelectedIndexChanged
        TxtInstnameCurr.Text = CmbInstnameCurr.Text
        If Not CmbInstnameCurr Is Nothing And CmbInstnameCurr.Items.Count > 0 Then
            If CmbInstnameCurr.Text <> "" Then
                If UCase(Mid(CmbInstnameCurr.Text, 1, 3)) = "FUT" Then
                    'cmbCurrencyStrike.DataSource = Nothing

                    ' cmbCurrencyStrike.Text = 0
                    Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & CmbSecurityCurr.Text & "' and InstrumentName='" & CmbInstnameCurr.Text & "' and option_type='" & CmbCallPutCurr.Text & "'  ", "expdate", DataViewRowState.CurrentRows)
                    CmbExpiryCurr.DataSource = dv.ToTable(True, "expdate")
                    CmbExpiryCurr.DisplayMember = "expdate"
                    CmbExpiryCurr.ValueMember = "expdate"
                    Dim dvCP As DataView = New DataView(DTCurrencyContract, "InstrumentName='" & CmbInstnameCurr.Text & "'", "option_type", DataViewRowState.CurrentRows)
                    CmbCallPutCurr.DataSource = dvCP.ToTable(True, "option_type")
                    CmbCallPutCurr.DisplayMember = "option_type"
                    CmbCallPutCurr.ValueMember = "option_type"
                    CmbCallPutCurr.Enabled = False
                    'cmbCurrencyStrike.Enabled = False
                    'cmbdate.Refresh()
                    'cmbdate.Focus()

                Else
                    Dim dv As DataView = New DataView(DTCurrencyContract, "InstrumentName='" & CmbInstnameCurr.Text & "'", "option_type", DataViewRowState.CurrentRows)
                    CmbCallPutCurr.DataSource = dv.ToTable(True, "option_type")
                    CmbCallPutCurr.DisplayMember = "option_type"
                    CmbCallPutCurr.ValueMember = "option_type"
                    CmbCallPutCurr.Enabled = True
                    'cmbCurrencyStrike.Enabled = True
                End If
            End If
        End If

    End Sub





    Private Sub CmbScriptEq_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '   TxtScriptEq.Text = CmbScriptEq.SelectedText
    End Sub

    Private Sub CmbCallPutFut_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCallPutFut.SelectedIndexChanged
        TxtCallPutFut.Text = CmbCallPutFut.Text
    End Sub

    Private Sub CmbExpiryFut_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbExpiryFut.SelectedIndexChanged
        TxtExpiryFut.Text = CmbExpiryFut.Text
    End Sub

    Private Sub CmbCallPutCurr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCallPutCurr.SelectedIndexChanged
        TxtCallPutCurr.Text = CmbCallPutCurr.Text
    End Sub

    Private Sub CmbExpiryCurr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbExpiryCurr.SelectedIndexChanged
        TxtExpiryCurr.Text = CmbExpiryCurr.Text
    End Sub

    Public Function UpdateCurrency() As Integer

        Dim Security As String
        Dim Instname As String
        Dim cp As String
        Dim expiry As Date
        Dim script As String
        Dim script1 As String
        Security = TxtSecurityCurr.Text
        Instname = TxtInstnameCurr.Text
        cp = TxtCallPutCurr.Text
        expiry = TxtExpiryCurr.Text

        script = Instname + "  " + Security + "  " + Format(CDate(TxtExpiryCurr.Text), "ddMMMyyyy").ToUpper()
        script1 = CmbInstnameCurr.Text & "  " & CmbSecurityCurr.Text & "  " & Format(CDate(CmbExpiryCurr.Text), "ddMMMyyyy").ToUpper()
        Dim qry As String
        Try
            qry = "UPDATE Currency_trading SET company = '" + Security + "',instrumentname='" + Instname + "',cp='" + cp + "',mdate='" + expiry + "',script='" + script + "' where script='" + script1 + "' "
            '  and mdate='" + fDate(CmbExpiryFut.SelectedValue) + "'


            data_access.ParamClear()
            data_access.Cmd_Text = qry
            data_access.cmd_type = CommandType.Text
            data_access.ExecuteNonQuery()
            data_access.cmd_type = CommandType.StoredProcedure
            MsgBox("Successfully Updated...")
        Catch ex As Exception
            MsgBox("Error to Update Data")

        End Try
    End Function


   
    Private Sub ScriptMapingFunctionality_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '  If masterdata.Rows.Count = 0 Then



        DTCurrencyContract = New DataTable
        DTCurrencyContract = Currencymaster
        CmbSecurityCurr.DisplayMember = "symbol"
        CmbSecurityCurr.ValueMember = "symbol"
        'Dim dvCurr As DataView = New DataView(DTCurrencyContract, "", "symbol", DataViewRowState.CurrentRows)
        Dim dvCurr As DataView = New DataView(DTCurrencyContract, "instrumentName='FUTCUR' or instrumentName='FUTIRD' or instrumentName='INDEX' or instrumentName='UNDCUR' or instrumentName='UNDIRD' or instrumentName='OPTCUR' OR  instrumentName='FUTIRC' OR  instrumentName='FUTIRT'", "symbol", DataViewRowState.CurrentRows)
        CmbSecurityCurr.DataSource = dvCurr.ToTable(True, "symbol")
        'cmbCurrencyComp.DisplayMember = "symbol"
        'cmbCurrencyComp.ValueMember = "symbol"
        ' cmbeqopt.SelectedValue = "Symbol"
        CmbSecurityCurr.SelectAll()
        '  End If
        '   CmbSecurityCurr.Select()

        masterdata = New DataTable
        masterdata = cpfmaster
        Dim dv As DataView = New DataView(masterdata, "", "symbol", DataViewRowState.CurrentRows)
        CmbSecurityFut.DataSource = dv.ToTable(True, "symbol")
        CmbSecurityFut.DisplayMember = "symbol"
        CmbSecurityFut.ValueMember = "symbol"
        CmbSecurityFut.DataSource = dv.ToTable(True, "symbol")
        If dv.ToTable(True, "symbol").Compute("count(symbol)", "symbol='NIFTY'") > 0 Then
            CmbSecurityFut.SelectedValue = "NIFTY"
            CmbSecurityFut.Select()
        End If
        '  End If


        ' If eqmasterdata.Rows.Count = 0 Then

        'If DTCurrencyContract.Rows.Count = 0 Then

        eqmasterdata = New DataTable
        eqmasterdata = eqmaster
        Application.DoEvents()

        Dim dv1 As DataView = New DataView(eqmasterdata, "", "symbol", DataViewRowState.CurrentRows)

        '     Application.DoEvents()
        'cmbeqcomp.DisplayMember = "symbol"
        'cmbeqcomp.ValueMember = "symbol"
        'If eqmasterdata.Rows.Count > 0 Then
        '    'cmbeqcomp.SelectedValue = dv1.ToTable(True, "Symbol").Rows(1).Item(0).ToString()
        '    'cmbeqcomp.Select()
        '    cmbeqcomp.SelectedIndex = 1 'dv1.ToTable(True, "Symbol").Rows(1).Item(0).ToString()

        'End If
        '   cmbeqcomp.SelectedValue = 

        ' End If
        CheckedListBox1.Items.Clear()

        Dim eqdata As DataTable
        eqdata = getEQscript()
        Dim dv2 As DataView = New DataView(eqdata, "", "Script", DataViewRowState.CurrentRows)
        For Each rowView As DataRowView In dv2
            Dim dr As DataRow = rowView.Row
            ' Do something '
            CheckedListBox1.Items.Add(dr("Script"))
        Next

    End Sub

    Private Sub GroupBox3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox3.Enter

    End Sub
End Class