Imports System
Imports System.io
Imports System.Data
Imports System.Data.OleDb
Public Class scriptgenrate
    Dim objScript As script = New script
    Dim masterdata As DataTable = New DataTable
    Dim dtUid As New DataTable
    Dim eqmasterdata As DataTable = New DataTable
    Dim objTrad As New trading

    Dim DTCurrencyContract As DataTable = New DataTable

    Dim cmbheight As Boolean = False
    Dim cmbh As Integer
    Dim objAna As New analysisprocess
    Public openposition As Boolean
    Dim Varmultiplier As Double = 0
    Private Sub scriptgenrate_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub

    Private Sub scriptgenrate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If openposition = False Then
            'If analysis.chkanalysis Then
            '    analysis.Close()
            'End If
            Panel6.Enabled = True
            ' Panel4.Enabled = True

            cmbcomp.Enabled = False
            cmbinstrument.Enabled = False
            cmbcp.Enabled = False
            cmbstrike.Enabled = False
            cmbdate.Enabled = False
            txtscript.Enabled = False


            Panel3.Enabled = False
            cmbeqcomp.Enabled = False
            cmbeqopt.Enabled = False
            txteqscript.Enabled = False
            Panel7.Enabled = False
        Else
            Panel6.Enabled = False
            'Panel4.Enabled = False

            cmbcomp.Enabled = True
            cmbinstrument.Enabled = True
            cmbcp.Enabled = True
            cmbstrike.Enabled = True
            cmbdate.Enabled = True
            'txtscript.Enabled = True
            Panel3.Enabled = True
            cmbeqcomp.Enabled = True
            cmbeqopt.Enabled = True
            ' txteqscript.Enabled = True
            Panel7.Enabled = True
        End If
        dtent.MaxDate = Now
        dteqent.MaxDate = Now
        DTPCurrencyEntryDate.MaxDate = Now
        dtent.Value = DateAdd(DateInterval.Day, -1, Now.Date)
        dteqent.Value = DateAdd(DateInterval.Day, -1, Now.Date)
        DTPCurrencyEntryDate.Value = DateAdd(DateInterval.Day, -1, Now.Date)
        masterdata = New DataTable
        masterdata = cpfmaster

        DTCurrencyContract = New DataTable
        DTCurrencyContract = Currencymaster

        Dim dv As DataView = New DataView(masterdata, "", "symbol", DataViewRowState.CurrentRows)
        cmbcomp.DataSource = dv.ToTable(True, "symbol")
        cmbcomp.DisplayMember = "symbol"
        cmbcomp.ValueMember = "symbol"
        'cmbcomp.Refresh()
        If dv.ToTable(True, "symbol").Compute("count(symbol)", "symbol='NIFTY'") > 0 Then
            cmbcomp.SelectedValue = "NIFTY"
        End If

        eqmasterdata = eqmaster
        Dim dv1 As DataView = New DataView(eqmasterdata, "", "symbol", DataViewRowState.CurrentRows)
        cmbeqcomp.DataSource = dv1.ToTable(True, "symbol")
        cmbeqcomp.DisplayMember = "symbol"
        cmbeqcomp.ValueMember = "symbol"
        ' cmbeqcomp.Refresh()
        cmbh = cmbcomp.Height


        Dim dvCurr As DataView = New DataView(DTCurrencyContract, "", "symbol", DataViewRowState.CurrentRows)
        cmbCurrencyComp.DataSource = dvCurr.ToTable(True, "symbol")
        cmbCurrencyComp.DisplayMember = "symbol"
        cmbCurrencyComp.ValueMember = "symbol"

    End Sub
    Private Sub cmbcomp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcomp.Enter
        'cmbcomp.DroppedDown = True
        'cmbheight = True
        cmbcomp.Height = 150
        cmbcomp.BringToFront()
    End Sub
    Private Sub cmbcomp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcomp.Leave
        cmbcomp.Height = cmbh
        If cmbcomp.Text.Trim <> "" And cmbcomp.Items.Count > 0 Then

            Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "'", "InstrumentName", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid Security")
                cmbcomp.Text = ""
                cmbcomp.Focus()
                Exit Sub
            End If
            cmbinstrument.DataSource = dv.ToTable(True, "InstrumentName")
            cmbinstrument.DisplayMember = "InstrumentName"
            cmbinstrument.ValueMember = "InstrumentName"
            'cmbinstrument.Refresh()
            cmbinstrument.SelectedIndex = 0
        End If

        If cmbcomp.Text.Trim = "" Then
            cmbinstrument.DataSource = Nothing
            cmbcp.DataSource = Nothing
            cmbstrike.DataSource = Nothing
            cmbdate.DataSource = Nothing
        End If

    End Sub

    Private Sub cmbinstrument_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbinstrument.Enter
        cmbinstrument.Height = 150
        cmbinstrument.BringToFront()
        If cmbcomp.Text.Trim <> "" And cmbcomp.Items.Count > 0 And cmbinstrument.Items.Count <= 0 Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "'", "InstrumentName", DataViewRowState.CurrentRows)
            cmbinstrument.DataSource = dv.ToTable(True, "InstrumentName")
            cmbinstrument.DisplayMember = "InstrumentName"
            cmbinstrument.ValueMember = "InstrumentName"
            'cmbinstrument.Refresh()
        End If
    End Sub
    Private Sub cmbinstrument_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbinstrument.Leave
        cmbcp.Enabled = True
        cmbstrike.Enabled = True
        cmbinstrument.Height = cmbh
        If cmbinstrument.Text.Trim <> "" And cmbinstrument.Items.Count > 0 Then
            cmbinstrument.Text = cmbinstrument.SelectedText
            If (UCase(cmbinstrument.Text) = "FUTIDX" Or UCase(cmbinstrument.Text) = "FUTSTK" Or UCase(cmbinstrument.Text) = "OPTIDX" Or UCase(cmbinstrument.Text) = "OPTSTK") Then

                Dim dv As DataView = New DataView(masterdata, "InstrumentName='" & cmbinstrument.Text & "'", "option_type", DataViewRowState.CurrentRows)
                cmbcp.DataSource = dv.ToTable(True, "option_type")
                cmbcp.DisplayMember = "option_type"
                cmbcp.ValueMember = "option_type"
                'cmbcp.Refresh()
                If cmbcp.Items.Count > 0 Then
                    cmbcp.SelectedIndex = 0
                End If
                If Not cmbinstrument Is Nothing And cmbinstrument.Items.Count > 0 Then
                    If cmbinstrument.Text <> "" Then
                        If UCase(Mid(cmbinstrument.Text, 1, 3)) = "FUT" Then
                            cmbcp.Enabled = False
                            cmbstrike.DataSource = Nothing

                            cmbstrike.Enabled = False
                            cmbstrike.Text = 0
                            dv = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "' AND expdate1 >='" & Now.Date & "' ", "strike_price", DataViewRowState.CurrentRows)
                            cmbdate.DataSource = dv.ToTable(True, "expdate")
                            cmbdate.DisplayMember = "expdate"
                            cmbdate.ValueMember = "expdate"
                            '  cmbdate.Refresh()
                            ' cmbdate.Focus()
                        Else
                            cmbcp.Enabled = True
                            cmbstrike.Enabled = True
                            ' cmbcp.Focus()
                        End If
                    End If
                End If
            Else
                MsgBox("Select valid instrument name")
                cmbinstrument.Text = ""
                cmbinstrument.Focus()
            End If

        End If
        If cmbinstrument.Text.Trim = "" Then
            cmbcp.DataSource = Nothing
            cmbstrike.DataSource = Nothing
            cmbdate.DataSource = Nothing
        End If

    End Sub
    Private Sub cmbinstrument_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbinstrument.SelectedValueChanged
        If cmbinstrument.Text.Trim <> "" And cmbinstrument.Items.Count > 0 Then
            Dim dv As DataView = New DataView(masterdata, "InstrumentName='" & cmbinstrument.Text & "'", "option_type", DataViewRowState.CurrentRows)
            cmbcp.DataSource = dv.ToTable(True, "option_type")
            cmbcp.DisplayMember = "option_type"
            cmbcp.ValueMember = "option_type"
            'cmbcp.Refresh()
            If cmbcp.Items.Count > 0 Then
                cmbcp.SelectedIndex = 0
            End If
            If Not cmbinstrument Is Nothing And cmbinstrument.Items.Count > 0 Then
                If cmbinstrument.Text <> "" Then
                    If UCase(Mid(cmbinstrument.Text, 1, 3)) = "FUT" Then
                        cmbstrike.DataSource = Nothing
                        cmbcp.Enabled = False
                        cmbstrike.Enabled = False
                        cmbstrike.Text = 0
                        dv = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
                        cmbdate.DataSource = dv.ToTable(True, "expdate")
                        cmbdate.DisplayMember = "expdate"
                        cmbdate.ValueMember = "expdate"
                        'cmbdate.Refresh()
                        'cmbdate.Focus()
                    Else
                        cmbcp.Enabled = True
                        cmbstrike.Enabled = True
                        'cmbcp.Focus()
                    End If
                End If
            End If
        End If

    End Sub
    Private Sub cmbcp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcp.Enter
        'cmbcp.Enabled = True
        ' cmbstrike.Enabled = True
        If cmbinstrument.Text.Trim <> "" And cmbinstrument.Items.Count > 0 And cmbcp.Items.Count <= 0 Then
            If (UCase(cmbinstrument.Text) = "FUTIDX" Or UCase(cmbinstrument.Text) = "FUTSTK" Or UCase(cmbinstrument.Text) = "OPTIDX" Or UCase(cmbinstrument.Text) = "OPTSTK") Then

                Dim dv As DataView = New DataView(masterdata, "InstrumentName='" & cmbinstrument.Text & "'", "option_type", DataViewRowState.CurrentRows)
                cmbcp.DataSource = dv.ToTable(True, "option_type")
                cmbcp.DisplayMember = "option_type"
                cmbcp.ValueMember = "option_type"
                '  cmbcp.Refresh()
                If cmbcp.Items.Count > 0 Then
                    cmbcp.SelectedIndex = 0
                End If
                If Not cmbinstrument Is Nothing And cmbinstrument.Items.Count > 0 Then
                    If cmbinstrument.Text <> "" Then
                        If UCase(Mid(cmbinstrument.Text, 1, 3)) = "FUT" Then
                            cmbstrike.DataSource = Nothing

                            cmbcp.Enabled = False
                            cmbstrike.Enabled = False
                            cmbstrike.Text = 0
                            dv = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
                            cmbdate.DataSource = dv.ToTable(True, "expdate")
                            cmbdate.DisplayMember = "expdate"
                            cmbdate.ValueMember = "expdate"
                            '  cmbdate.Refresh()
                            'cmbdate.Focus()
                        Else
                            cmbcp.Enabled = True
                            cmbstrike.Enabled = True
                            'cmbcp.Focus()
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub cmbcp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcp.Leave
        If cmbinstrument.Text.Trim <> "" And cmbcp.Text <> "".Trim And cmbcp.Items.Count > 0 Then
            If (UCase(cmbcp.Text) = "CE" Or UCase(cmbcp.Text) = "PE" Or UCase(cmbcp.Text) = "CA" Or UCase(cmbcp.Text) = "PA") Then
                Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "'", "strike_price", DataViewRowState.CurrentRows)
                'cmbstrike.Items.Clear()
                cmbstrike.DataSource = dv.ToTable(True, "strike_price")
                cmbstrike.DisplayMember = "strike_price"
                cmbstrike.ValueMember = "strike_price"
                ' cmbstrike.Refresh()
                cmbstrike.SelectedIndex = 0
            Else
                MsgBox("Select valid option type")
                cmbcp.Text = ""
                cmbcp.Focus()
            End If
        End If

    End Sub

   
    Private Sub cmbstrike_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstrike.Enter
        cmbstrike.Height = 150
        cmbstrike.BringToFront()
        If cmbcp.Text <> "" And cmbcp.Items.Count > 0 And cmbstrike.Items.Count <= 0 Then
            If (UCase(cmbcp.Text) = "CE" Or UCase(cmbcp.Text) = "PE" Or UCase(cmbcp.Text) = "CA" Or UCase(cmbcp.Text) = "PA") Then
                Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "'", "strike_price", DataViewRowState.CurrentRows)
                cmbstrike.DataSource = dv.ToTable(True, "strike_price")
                cmbstrike.DisplayMember = "strike_price"
                cmbstrike.ValueMember = "strike_price"
                ' cmbstrike.Refresh()
                cmbstrike.SelectedIndex = 0
            End If
        End If
    End Sub
    Private Sub cmbstrike_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstrike.Leave
        cmbstrike.Height = cmbh
        If cmbstrike.Text.Trim = "" Then
            cmbstrike.Text = 0
        End If
        If (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") And cmbstrike.Text <> "0" Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "' and strike_price=" & Val(cmbstrike.Text) & " ", "strike_price", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid strike rate")
                cmbstrike.Text = ""
                cmbstrike.Focus()
                Exit Sub
            End If
            cmbdate.DataSource = dv.ToTable(True, "expdate")
            cmbdate.DisplayMember = "expdate"
            cmbdate.ValueMember = "expdate"
            'cmbdate.Refresh()
            If cmbdate.Items.Count > 0 Then
                cmbdate.SelectedIndex = 0
            End If
        Else
            Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
            'If dv.ToTable.Rows.Count <= 0 Then
            '    MsgBox("Select valid strike rate")
            '    cmbstrike.Text = ""
            '    cmbstrike.Focus()
            '    Exit Sub
            'End If
            cmbdate.DataSource = dv.ToTable(True, "expdate")
            cmbdate.DisplayMember = "expdate"
            cmbdate.ValueMember = "expdate"
            ' cmbdate.Refresh()
        End If

    End Sub
    Private Sub cmbdate_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdate.Enter
        If cmbstrike.Text.Trim = "" Then
            cmbstrike.Text = 0
        End If
        If (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") And cmbstrike.Text <> "0" And cmbstrike.Items.Count > 0 Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "' and strike_price=" & Val(cmbstrike.Text) & "  and expdate1 >='" & Now.Date & "' ", "strike_price", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid strike rate")
                cmbstrike.Text = ""
                cmbstrike.Focus()
                Exit Sub
            End If
            cmbdate.DataSource = dv.ToTable(True, "expdate")
            cmbdate.DisplayMember = "expdate"
            cmbdate.ValueMember = "expdate"
            'cmbdate.Refresh()
            If cmbdate.Items.Count > 0 Then
                cmbdate.SelectedIndex = 0
            End If
        ElseIf cmbcp.Text <> "" And cmbcp.Items.Count > 0 Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "'  and expdate1 >='" & Now.Date & "'  ", "strike_price", DataViewRowState.CurrentRows)
            cmbdate.DataSource = dv.ToTable(True, "expdate")
            cmbdate.DisplayMember = "expdate"
            cmbdate.ValueMember = "expdate"
            ' cmbdate.Refresh()
        End If
    End Sub
    Private Sub txtunit_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtunit.Enter
        If cmbcomp.Text.Trim = "" Then
            MsgBox("Enter Security Name", MsgBoxStyle.Information)
            cmbcomp.Focus()
            Exit Sub
        End If
        If cmbinstrument.Text.Trim = "" Then
            MsgBox("Select Instrument Name", MsgBoxStyle.Information)
            cmbinstrument.Focus()
            Exit Sub
        End If
       
        If cmbcp.SelectedValue.Trim = "" Then
            MsgBox("Select Call/Put/Futre", MsgBoxStyle.Information)
            cmbcp.Focus()
            Exit Sub
        End If
        If cmbstrike.Text.Trim = "0" And (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate", MsgBoxStyle.Information)
            cmbstrike.Focus()
            Exit Sub
        End If
        If cmbdate.Text = "" Then
            MsgBox("Select date", MsgBoxStyle.Information)
            cmbdate.Focus()
            Exit Sub
        End If
        Dim cp As String
        cp = ""
        If Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P" Then
            cp = UCase(cmbcp.SelectedValue)
        Else
            cp = ""
        End If
        'If UCase(cmbcp.Items.ToString) = "CALL" And UCase(cmbinstrument.Items.ToString) = "OPTSTK" Then
        '    cp = "CA"
        'ElseIf UCase(cmbcp.SelectedItem.ToString) = "CALL" And UCase(cmbinstrument.SelectedItem.ToString) = "OPTIDX" Then
        '    cp = "CE"
        'ElseIf UCase(cmbcp.SelectedItem.ToString) = "PUT" And UCase(cmbinstrument.SelectedItem.ToString) = "OPTSTK" Then
        '    cp = "PA"
        'ElseIf UCase(cmbcp.SelectedItem.ToString) = "PUT" And UCase(cmbinstrument.SelectedItem.ToString) = "OPTIDX" Then
        '    cp = "PE"
        'Else
        '    cp = ""
        'End If

        Dim script As String
        If cp = "" Then
            script = cmbinstrument.SelectedValue & "  " & cmbcomp.SelectedValue & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy")
        Else
            script = cmbinstrument.SelectedValue & "  " & cmbcomp.SelectedValue & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy") & "  " & Format(val(cmbstrike.Text), "###0.00") & "  " & cp

        End If
        txtscript.Text = script.Trim
        If masterdata.Compute("count(symbol)", "script='" & txtscript.Text.Trim & "'") <= 0 Then
            MsgBox("Not valid Script")
            form_clear()
            Exit Sub
        End If

    End Sub



    Private Sub txtstrikerate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Char.IsDigit(e.KeyChar) = False Then
            If Char.IsLetter(e.KeyChar) = True Then
                e.Handled = True
            End If

            If Char.IsWhiteSpace(e.KeyChar) = False Then
                If e.KeyChar <> "." And Asc(e.KeyChar) <> 8 Then
                    e.Handled = True
                End If
            End If
        End If

    End Sub
    Private Sub txtunit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtunit.KeyPress
        numonly(e)
        'If Char.IsDigit(e.KeyChar) = False Then
        '    If Char.IsLetter(e.KeyChar) = True Then
        '        e.Handled = True
        '    End If

        '    If Char.IsWhiteSpace(e.KeyChar) = False Then
        '        If e.KeyChar <> "." And Asc(e.KeyChar) <> 8 Then
        '            e.Handled = True
        '        End If
        '    End If
        'End If
    End Sub
    Private Sub txtrate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrate.KeyPress
        numonly(e)
        'If Char.IsDigit(e.KeyChar) = False Then
        '    If Char.IsLetter(e.KeyChar) = True Then
        '        e.Handled = True
        '    End If

        '    If Char.IsWhiteSpace(e.KeyChar) = False Then
        '        If e.KeyChar <> "." And Asc(e.KeyChar) <> 8 Then
        '            e.Handled = True
        '        End If
        '    End If
        'End If
    End Sub
    Private Function form_validation() As Boolean
        If cmbinstrument.Text.Trim = "" Then
            MsgBox("Select Instrument Name", MsgBoxStyle.Information)
            cmbinstrument.Focus()
            form_validation = False
            Exit Function
        End If
        If cmbcomp.Text.Trim = "" Then
            MsgBox("Enter Company Name", MsgBoxStyle.Information)
            cmbcomp.Focus()
            form_validation = False
            Exit Function
        End If
        If cmbstrike.Text.Trim = "0" And (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate", MsgBoxStyle.Information)
            cmbstrike.Focus()
            form_validation = False
            Exit Function
        End If
        If cmbcp.Text = "" Then
            MsgBox("Select Call/Put/Futre", MsgBoxStyle.Information)
            cmbcp.Focus()
            form_validation = False
            Exit Function
        End If
        If Not IsNumeric(txtunit.Text) Then
            MsgBox("Enter Units", MsgBoxStyle.Information)
            txtunit.Focus()
            form_validation = False
            Exit Function
        End If
        If Not IsNumeric(txtrate.Text) Then
            MsgBox("Enter Rate", MsgBoxStyle.Information)
            txtrate.Focus()
            form_validation = False
            Exit Function
        End If
        If txtunit.Text = "0" Then
            MsgBox("Enter Units", MsgBoxStyle.Information)
            txtunit.Focus()
            form_validation = False
            Exit Function
        End If
        If txtrate.Text = "0" Then
            MsgBox("Enter Rate", MsgBoxStyle.Information)
            txtrate.Focus()
            form_validation = False
            Exit Function
        End If

        form_validation = True
    End Function
    Private Sub form_clear()
        cmbinstrument.Text = ""
        cmbcomp.Text = ""

        cmbstrike.Text = "0"
        txtscript.Text = ""
        txtunit.Text = "0"
        txtrate.Text = "0"
        cmbcomp.SelectedIndex = 0
        cmbcomp.Focus()
    End Sub

    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        Dim strDate As String
        Try
            If txtunit.Text = "" Then
                txtunit.Text = 0
            End If
            If txtrate.Text = "" Then
                txtrate.Text = 0
            End If
            If form_validation() Then
                Dim tkk As Long = CLng(Val(objTrad.Trading.Compute("max(token)", "script='" & txtscript.Text & "'").ToString))
                If tkk > 0 Then
                    MsgBox(txtscript.Text & " script already exist in Traded")
                    Exit Sub
                End If
                Dim a, a1, script1 As String
                Dim tk As Long
                GVarMAXFOTradingOrderNo = GVarMAXFOTradingOrderNo + 1
                'insert trade to Trading table
                objScript.InstrumentName = cmbinstrument.Text
                objScript.Company = cmbcomp.Text
                objScript.Mdate = CDate(cmbdate.Text).Date
                objScript.StrikeRate = Val(cmbstrike.Text)
                objScript.CP = UCase(Mid(cmbcp.SelectedValue, 1, 1))
                objScript.Script = txtscript.Text.Trim
                objScript.Units = Val(txtunit.Text)
                objScript.Rate = Val(txtrate.Text)
                objScript.EntryDate = dtent.Value.Date
                If UCase(Mid(cmbcp.SelectedValue, 1, 1)) = "C" Or UCase(Mid(cmbcp.SelectedValue, 1, 1)) = "P" Then
                    a = Mid(txtscript.Text, Len(txtscript.Text) - 1, 1)
                    a1 = Mid(txtscript.Text, Len(txtscript.Text), 1)
                    If a = "C" Then
                        script1 = Mid(txtscript.Text, 1, Len(txtscript.Text) - 2) & "P" & a1
                    Else
                        script1 = Mid(txtscript.Text, 1, Len(txtscript.Text) - 2) & "C" & a1
                    End If
                    tk = CLng(masterdata.Compute("max(token)", "script='" & script1 & "'").ToString)
                    objScript.Token = tk
                    For Each row As DataRow In GdtFOTrades.Select("script='" & txtscript.Text & "'")
                        If row("isliq") = True Then
                            objScript.Isliq = True ' "Yes"
                        Else
                            objScript.Isliq = False '"No"
                        End If
                        Exit For
                    Next
                Else
                    objScript.Token = 0
                    objScript.asset_tokan = 0
                    objScript.Isliq = False ' "No"
                End If
                objScript.orderno = GVarMAXFOTradingOrderNo
                Dim tk1 As Long = CLng(Val(masterdata.Compute("max(token)", "script='" & txtscript.Text & "'").ToString))
                If tk1 = 0 Then
                    MsgBox(txtscript.Text & " does not exist in contract")
                End If
                'save trade to database trade table
                Dim DTDuplicate As DataTable
                DTDuplicate = New DataTable
                DTDuplicate = objScript.Insert()
                If DTDuplicate.Rows.Count - 1 > 0 Then
                    MsgBox("Contract Exist")
                    Exit Sub
                End If
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                'get uid from trading table
                dtUid = objScript.select_trading_uid
                'Save to analysis's dtFOTrading table
                Dim tprow As DataRow
                tprow = GdtFOTrades.NewRow
                tprow("uid") = dtUid.Rows(0)("uid")
                tprow("token") = 0
                tprow("mo") = Format(CDate(cmbdate.Text).Date, "MM/yyyy")
                tprow("instrumentname") = cmbinstrument.Text
                tprow("company") = cmbcomp.Text
                tprow("mdate") = CDate(cmbdate.Text).Date
                tprow("Strikerate") = Val(cmbstrike.Text)
                tprow("CP") = IIf(UCase(Mid(cmbcp.SelectedValue, 1, 1)) = "X", "F", UCase(Mid(cmbcp.SelectedValue, 1, 1)))
                tprow("script") = txtscript.Text.Trim
                tprow("qty") = Val(txtunit.Text)
                tprow("Rate") = Val(txtrate.Text)
                strDate = Format(dtent.Value.Date, "MMM/dd/yyyy")
                tprow("EntryDate") = Format(CDate(strDate).Date, "MMM/dd/yyyy")
                tprow("entry_date") = Format(CDate(strDate).Date, "MMM/dd/yyyy")
                tprow("isliq") = objScript.Isliq
                tprow("token1") = tk1
                tprow("orderno") = GVarMAXFOTradingOrderNo
                tprow("entryno") = 0
                tprow("tot") = Val(txtunit.Text) * Val(txtrate.Text)
                tprow("issummary") = True
                tprow("isdisplay") = True
                GdtFOTrades.Rows.Add(tprow)
                'calculate expense of inserted position
                Dim prExp, toExp As Double
                cal_prebal(dtent.Value.Date, cmbcomp.Text.Trim, UCase(Mid(cmbcp.SelectedValue, 1, 1)), CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp)
                'insert position to database's analysis table also
                Dim dtAna As New DataTable
                dtAna = objAna.fill_table_process(txtscript.Text.Trim, CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp, dtent.Value.Date)

                'insert FO trade to analysis table
                objScript.insert_FOTrade_in_maintable(txtscript.Text.Trim, dtAna, prExp, toExp, dtent.Value.Date)
                MsgBox("Script saved successfully", MsgBoxStyle.Information)

                LastOpenPosition = cmbcomp.Text

            End If

            '***********************************************************************************                objAna.fill_table_process(txtscript.Text.Trim)
            ' If dtent.Value.Date < Now.Date Then

            'End If
            txtrate.Text = "0"
            txtunit.Text = "0"
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        form_clear()
    End Sub

    Private Sub scriptgenrate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If

    End Sub
    Private Sub txtequnit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtequnit.KeyPress
        numonly(e)
    End Sub
    Private Sub txteqrate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txteqrate.KeyPress
        numonly(e)
    End Sub
    Private Sub cmbeqcomp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbeqcomp.Enter
        cmbeqcomp.Height = 150
        cmbeqcomp.BringToFront()
    End Sub
    Private Sub cmbeqcomp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbeqcomp.Leave
        cmbeqcomp.Height = cmbh
        Dim dv1 As DataView = New DataView(eqmasterdata, "symbol='" & cmbeqcomp.Text & "'", "series", DataViewRowState.CurrentRows)
        cmbeqopt.DataSource = dv1.ToTable(True, "series")
        cmbeqopt.DisplayMember = "series"
        cmbeqopt.ValueMember = "series"
        If cmbeqopt.Items.Count > 0 Then
            If New DataView(eqmasterdata, "symbol='" & cmbeqcomp.Text & "' and series='EQ'", "series", DataViewRowState.CurrentRows).ToTable.Rows.Count > 0 Then
                cmbeqopt.SelectedValue = "EQ"
            Else
                cmbeqopt.SelectedIndex = 0
            End If
        End If
    End Sub
    Private Function eqform_validation() As Boolean

        If cmbeqcomp.Text.Trim = "" Then
            MsgBox("Enter Company Name", MsgBoxStyle.Information)
            cmbeqcomp.Focus()
            eqform_validation = False
            Exit Function
        End If
        If cmbeqopt.Text.Trim = "" Then
            MsgBox("Enter Option", MsgBoxStyle.Information)
            cmbeqopt.Focus()
            eqform_validation = False
            Exit Function
        End If

        If txtequnit.Text = "0" Then
            MsgBox("Enter Units", MsgBoxStyle.Information)
            txtequnit.Focus()
            eqform_validation = False
            Exit Function
        End If
        If txteqrate.Text = "0" Then
            MsgBox("Enter Rate", MsgBoxStyle.Information)
            txteqrate.Focus()
            eqform_validation = False
            Exit Function
        End If
        eqform_validation = True
    End Function
    Private Sub eqform_clear()
        cmbeqcomp.Text = ""
        txteqscript.Text = ""
        txtequnit.Text = "0"
        txteqrate.Text = "0"
        cmbeqopt.Text = ""
        cmbeqcomp.SelectedIndex = 0
        cmbeqcomp.Focus()
    End Sub
    Private Sub cmdeqsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqsave.Click
        Try
            If txtequnit.Text = "" Then
                txtunit.Text = 0
            End If
            If txteqrate.Text = "" Then
                txtrate.Text = 0
            End If
            If eqform_validation() Then
                'Dim tkk As Long = CLng(IIf(IsDBNull(objTrad.select_equity.Compute("max(uid)", "script='" & txteqscript.Text & "'")), 0, objTrad.select_equity.Compute("max(uid)", "script='" & txteqscript.Text & "'")))
                'If tkk > 0 Then
                '    MsgBox(txteqscript.Text & " script already exist in Traded")
                '    Exit Sub
                'End If
                GVarMAXEQTradingOrderNo = GVarMAXEQTradingOrderNo + 1
                objScript.Company = cmbeqcomp.Text
                objScript.Script = UCase(txteqscript.Text.Trim)
                objScript.CP = cmbeqopt.Text
                objScript.Units = Val(txtequnit.Text)
                objScript.Rate = Val(txteqrate.Text)
                objScript.EntryDate = dteqent.Value.Date
                objScript.orderno = GVarMAXEQTradingOrderNo
                objScript.insert_equity()
                'objAna.fill_equity_process(UCase(txteqscript.Text.Trim))
                'If dteqent.Value.Date < Now.Date Then
                ' cal_prebal(dteqent.Value.Date, cmbeqcomp.Text.Trim)
                'End If
                'get uid from equity_trading
                dtUid = objScript.select_equity_uid

                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                'Save to analysis's tptable (equity trading) table
                Dim tprow As DataRow
                tprow = GdtEQTrades.NewRow
                tprow("uid") = dtUid.Rows(0)("uid")
                tprow("company") = cmbeqcomp.Text
                tprow("eq") = cmbeqopt.Text
                tprow("script") = UCase(txteqscript.Text.Trim)
                tprow("qty") = Val(txtequnit.Text)
                tprow("Rate") = Val(txteqrate.Text)
                Dim strdate As String
                strdate = Format(dteqent.Value.Date, "MMM/dd/yyyy")
                tprow("EntryDate") = Format(CDate(strdate).Date, "MMM/dd/yyyy")
                tprow("entry_date") = Format(CDate(strdate).Date, "MMM/dd/yyyy")
                tprow("orderno") = GVarMAXEQTradingOrderNo
                tprow("entryno") = 0
                tprow("issummary") = True
                tprow("isdisplay") = True
                tprow("tot") = Val(txtequnit.Text) * Val(txteqrate.Text)
                GdtEQTrades.Rows.Add(tprow)
                ' If dteqent.Value.Date < Now.Date Then
                Dim prExp, toExp As Double
                'caluclate expense of inserted position
                cal_prebal(dteqent.Value.Date, cmbeqcomp.Text.Trim, "E", CInt(txtequnit.Text), Val(txteqrate.Text), prExp, toExp)

                'insert position to database's analysis table also
                Dim dtAna As New DataTable
                dtAna = objAna.fill_equity_process(UCase(txteqscript.Text.Trim), CInt(txtequnit.Text), Val(txteqrate.Text), prExp, toExp, dteqent.Value.Date)

                '***********************************************************************************
                'insert FO trade to analysis table
                objScript.insert_EQTrade_in_maintable(txteqscript.Text.Trim, dtAna, prExp, toExp, dteqent.Value.Date)
                'End If
                MsgBox("Script saved successfully", MsgBoxStyle.Information)

                LastOpenPosition = cmbeqcomp.Text

                txtequnit.Text = "0"
                txteqrate.Text = "0"
                'eqform_clear()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub cmdeqclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqclear.Click
        eqform_clear()
    End Sub
    Private Sub cmdeqexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqexit.Click
        Me.Close()
    End Sub


    'Private Sub read_file()
    '    Try
    '        Dim tempdata As New DataTable

    '        Dim fpath As String
    '        fpath = CStr(txtpath.Text.Trim)
    '        If fpath <> "" Then
    '            Dim a, a1, script1 As String
    '            Dim tk As Long
    '            tempdata = New DataTable
    '            'tempdata.Columns.Add("strike")
    '            'tempdata.Columns.Add("option_type")
    '            'tempdata.Columns.Add("Exp_Date")
    '            'tempdata.Columns.Add("Company")
    '            'tempdata.Columns.Add("Qty")
    '            'tempdata.Columns.Add("Rate")
    '            'tempdata.Columns.Add("Instrument")
    '            'tempdata.Columns.Add("entrydate")

    '            Dim fi As New FileInfo(fpath)
    '            Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" & fpath
    '            Dim objConn As New OleDbConnection(sConnectionString)
    '            objConn.Open()
    '            Dim objCmdSelect As New OleDbCommand("SELECT * FROM [Trading$] where security <> ''", objConn)
    '            'Dim objCmdSelect As New OleDbCommand("SELECT strike,option_type,Exp_Date,Company,Qty,Rate,Instrument,entrydate FROM " & fi.Name, objConn)
    '            Dim objAdapter1 As New OleDbDataAdapter
    '            objAdapter1.SelectCommand = objCmdSelect
    '            objAdapter1.Fill(tempdata)
    '            objConn.Close()
    '            'fi.Delete()
    '            tempdata.Columns.Add("script")
    '            tempdata.Columns.Add("token1", GetType(Long))
    '            tempdata.Columns.Add("isliq")
    '            tempdata.AcceptChanges()
    '            Dim drow As DataRow
    '            Dim script As String
    '            Dim dv1 As New DataView(masterdata)
    '            Dim cp As String
    '            For Each drow In tempdata.Select("security<>''")
    '                If Not IsDBNull(drow("security")) Then
    '                    If Not IsDBNull(drow("cpf")) Then
    '                        If Mid(drow("CPF"), 1, 1) = "C" Or Mid(drow("CPF"), 1, 1) = "P" Then
    '                            For Each cprow As DataRow In dv1.ToTable().Select("InstrumentName='" & UCase(drow("Instrument")) & "' and symbol='" & UCase(drow("security")) & "'")
    '                                If UCase(Mid(cprow("option_type"), 1, 1)) = UCase(Mid(drow("CPF"), 1, 1)) Then
    '                                    cp = cprow("option_type")
    '                                    Exit For
    '                                End If
    '                            Next

    '                            script = drow("Instrument") & "  " & drow("security") & "  " & Format(CDate(drow("Exp_Date")), "ddMMMyyyy") & "  " & CStr(Format(val(drow("strike")), "###0.00")) & "  " & cp
    '                            drow("script") = UCase(script.Trim)
    '                            a = Mid(drow("script"), Len(drow("script")) - 1, 1)
    '                            a1 = Mid(drow("script"), Len(drow("script")), 1)
    '                            If a = "C" Then
    '                                script1 = Mid(drow("script"), 1, Len(drow("script")) - 2) & "P" & a1
    '                            Else
    '                                script1 = Mid(drow("script"), 1, Len(drow("script")) - 2) & "C" & a1
    '                            End If
    '                            tk = CLng(IIf(IsDBNull(masterdata.Compute("max(token)", "script='" & script1 & "'")), 0, masterdata.Compute("max(token)", "script='" & script1 & "'")))
    '                            drow("token1") = tk
    '                            drow("isliq") = False ' "No"
    '                            For Each row As DataRow In temptable.Select("script='" & drow("script") & "'")
    '                                If row("isliq") = True Then
    '                                    drow("isliq") = True ' "Yes"
    '                                Else

    '                                    drow("isliq") = False ' "No"
    '                                End If
    '                                Exit For
    '                            Next

    '                        Else
    '                            script = drow("Instrument") & "  " & drow("security") & "  " & Format(CDate(drow("exp_date")), "ddMMMyyyy")
    '                            drow("script") = UCase(script.Trim)
    '                            drow("token1") = 0
    '                            drow("isliq") = False ' "No"
    '                        End If
    '                    Else
    '                        script = drow("Instrument") & "  " & drow("security") & "  " & Format(CDate(drow("exp_date")), "ddMMMyyyy")
    '                        drow("script") = UCase(script.Trim)
    '                        drow("token1") = 0
    '                        drow("isliq") = False ' "No"
    '                        drow("cpf") = ""
    '                    End If
    '                    Dim tk1 As Long = CLng(IIf(IsDBNull(masterdata.Compute("max(token)", "script='" & drow("script") & "'")), 0, masterdata.Compute("max(token)", "script='" & drow("script") & "'")))

    '                    If tk1 = 0 Then
    '                        MsgBox(txtscript.Text & " does not exist in contract")
    '                    End If
    '                End If
    '            Next
    '            If tempdata.Rows.Count > 0 Then
    '                objScript.Insert_trading(tempdata)
    '                Dim str(1) As String
    '                str(0) = "security"
    '                str(1) = "entrydate"
    '                Dim dv As New DataView(tempdata)
    '                For Each row As DataRow In dv.ToTable(True, str).Rows
    '                    If CDate(row("entrydate")).Date < Now.Date Then
    '                        cal_prebal(CDate(row("entrydate")).Date, row("security"))
    '                    End If
    '                Next
    '                objAna.process_data()

    '                MsgBox("File Processed Successfully")
    '                txtpath.Text = ""
    '            Else
    '                MsgBox("File not Processed")
    '            End If

    '        End If
    '    Catch ex As Exception
    '        MsgBox("File  not processed")

    '        'MsgBox(ex.ToString)

    '    End Try




    'End Sub


    'Private Sub equity_read_file()
    '    Try
    '        Dim tempdata As New DataTable

    '        Dim fpath As String
    '        fpath = CStr(txteqpath.Text.Trim)
    '        If fpath <> "" Then
    '            Dim fi As New FileInfo(fpath)

    '            Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" & fpath
    '            'Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName

    '            Dim objConn As New OleDbConnection(sConnectionString)

    '            objConn.Open()

    '            Dim objCmdSelect As New OleDbCommand("SELECT * FROM [Equity$] where security<>''", objConn)
    '            'Dim objCmdSelect As New OleDbCommand("SELECT security,eq,Qty,atp,entrydate FROM " & fi.Name, objConn)

    '            Dim objAdapter1 As New OleDbDataAdapter

    '            objAdapter1.SelectCommand = objCmdSelect

    '            tempdata = New DataTable

    '            'tempdata.Columns.Add("Company")
    '            'tempdata.Columns.Add("eq")
    '            'tempdata.Columns.Add("Qty")
    '            'tempdata.Columns.Add("Rate")
    '            'tempdata.Columns.Add("entrydate")
    '            tempdata.AcceptChanges()
    '            objAdapter1.Fill(tempdata)
    '            objConn.Close()
    '            tempdata.Columns.Add("script")
    '            tempdata.AcceptChanges()
    '            Dim drow As DataRow
    '            Dim script As String
    '            For Each drow In tempdata.Rows
    '                script = drow("security") & "  " & drow("eq")
    '                drow("script") = UCase(script.Trim)
    '            Next

    '            If tempdata.Rows.Count > 0 Then
    '                objScript.Insert_eqtrading(tempdata)
    '                Dim str(1) As String
    '                str(0) = "security"
    '                str(1) = "entrydate"
    '                Dim dv As New DataView(tempdata)
    '                For Each row As DataRow In dv.ToTable(True, str).Rows
    '                    If CDate(row("entrydate")).Date < Now.Date Then
    '                        cal_prebal(CDate(row("entrydate")).Date, row("security"))
    '                    End If
    '                Next
    '                objAna.process_data()

    '                MsgBox("File Processed Successfully")
    '                txteqpath.Text = ""

    '            Else
    '                MsgBox("File Not Processed")
    '            End If

    '            'MsgBox("File Process Successfully")
    '        End If
    '    Catch ex As Exception
    '        MsgBox("File Not Processed")
    '        ' MsgBox(ex.ToString)
    '    End Try




    'End Sub
    Private Sub cmdtrbr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdtrbr.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.xls)|*.xls"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtpath.Text = opfile.FileName
        End If
    End Sub
    Private Sub cmdeqbr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.xls)|*.xls"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ' txteqpath.Text = opfile.FileName
        End If
    End Sub
    Private Sub cmdeqcl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'txteqpath.Text = ""
    End Sub
    Private Sub cmdtrcl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdtrcl.Click
        txtpath.Text = ""
    End Sub
    Private Sub cmdtrimp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdtrimp.Click
        Me.Cursor = Cursors.WaitCursor
        'read_file()
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub cmdeqimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.WaitCursor
        ' equity_read_file()
        Me.Cursor = Cursors.Default
    End Sub

    'Private Sub cal_prebal(ByVal date1 As Date, ByVal compname As String, ByVal optype As String, ByVal qty As Integer, ByVal rate As Double, ByRef prExp As Double, ByRef toExp As Double)
    '    If date1 < Now.Date Then ' previous trades expense calulation
    '        Dim addprebal As New DataTable
    '        Dim prebalance As New DataTable
    '        prebalance = objTrad.prebal
    '        addprebal = New DataView(prebalance, "company = '" & compname & "' and tdate=#" & date1 & "#", "", DataViewRowState.CurrentRows).ToTable
    '        'if no prebalance for position then add new position
    '        If addprebal.Rows.Count = 0 Then
    '            'With addprebal.Columns
    '            '    ' .Add("tdate", GetType(Date))
    '            '    .Add("stbal", GetType(Double))
    '            '    .Add("futbal", GetType(Double))
    '            '    .Add("optbal", GetType(Double))
    '            '    '.Add("company", GetType(String))
    '            'End With
    '            Dim drow As DataRow = addprebal.NewRow
    '            addprebal.Rows.Add(drow)
    '            addprebal.Rows(0)("company") = compname
    '            addprebal.Rows(0)("tdate") = date1
    '            addprebal.Rows(0)("stbal") = 0
    '            addprebal.Rows(0)("futbal") = 0
    '            addprebal.Rows(0)("optbal") = 0
    '            addprebal.AcceptChanges()
    '        End If

    '        If optype = "E" Then
    '            If qty > 0 Then
    '                addprebal.Rows(0)("stbal") += Val((qty * rate * dbl) / dblp)
    '            Else
    '                qty = -qty
    '                addprebal.Rows(0)("stbal") += Val((qty * rate * dbs) / dbsp)
    '            End If
    '        ElseIf optype = "X" Then
    '            If qty > 0 Then
    '                addprebal.Rows(0)("futbal") += Val((qty * rate * futl) / futlp)
    '            Else
    '                qty = -qty
    '                addprebal.Rows(0)("futbal") += Val((qty * rate * futs) / futsp)
    '            End If
    '        Else 'option type
    '            If Val(spl) <> 0 Then
    '                If qty > 0 Then
    '                    addprebal.Rows(0)("optbal") += Val((qty * rate * spl) / splp)
    '                Else
    '                    qty = -qty
    '                    addprebal.Rows(0)("optbal") += Val((qty * rate * sps) / spsp)
    '                End If
    '            Else
    '                If qty > 0 Then
    '                    addprebal.Rows(0)("optbal") += Val((qty * rate * prel) / prelp)
    '                Else
    '                    qty = -qty
    '                    addprebal.Rows(0)("optbal") += Val((qty * rate * pres) / presp)
    '                End If
    '            End If
    '        End If
    '        addprebal.Rows(0)("tot") = Math.Abs(Val(addprebal.Rows(0)("stbal")) + Val(addprebal.Rows(0)("futbal")) + Val(addprebal.Rows(0)("optbal")))
    '        objTrad.Delete_prBal(date1.Date, compname)
    '        objTrad.insert_prebal(addprebal)
    '        prExp = Val(addprebal.Rows(0)("tot"))
    '    Else 'calculate today expense
    '        Dim stexp, stexp1, dst, ndst As Double
    '        'FOR Equity
    '        stexp = Math.Round(Val(dtEQTrades.Compute("sum(tot)", "qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2)
    '        stexp1 = Math.Abs(Math.Round(Val(dtEQTrades.Compute("sum(qty)", "qty < 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2))
    '        dst = stexp - stexp1
    '        If dst > 0 Then
    '            ndst = stexp1
    '            toExp += ((dst * ndbl) / ndblp)
    '            toExp += ((stexp1 * ndbs) / ndbsp)
    '            toExp += ((stexp1 * ndbl) / ndblp)
    '        Else
    '            ndst = stexp
    '            dst = -dst
    '            toExp += ((dst * dbs) / dbsp)
    '            toExp += ((stexp * ndbl) / ndblp)
    '            toExp += ((stexp * ndbs) / ndbsp)
    '        End If
    '        'for FUTURe
    '        stexp = 0
    '        stexp1 = 0
    '        stexp = Val(dtFOTrades.Compute("sum(tot)", "cp not in ('C','P') and company = '" & compname & "' and qty > 0 and entry_date =  #" & date1 & "#").ToString)
    '        stexp1 = Math.Abs(Val(dtFOTrades.Compute("sum(tot)", "cp not in ('C','P') and company = '" & compname & "' and qty < 0 and entry_date =  #" & date1 & "#").ToString))
    '        toExp = ((stexp * futl) / futlp)
    '        toExp = ((stexp1 * futs) / futsp)

    '        'OPTION
    '        If Val(spl) <> 0 Then
    '            stexp = 0
    '            stexp1 = 0
    '            stexp = Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString)
    '            stexp1 = Math.Abs(Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and company = '" & compname & "' qty < 0 and entry_date =  #" & date1 & "#").ToString))
    '            toExp += ((stexp * spl) / splp)
    '            toExp += ((stexp1 * sps) / spsp)
    '        Else
    '            stexp = 0
    '            stexp1 = 0
    '            stexp = Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString)
    '            stexp1 = Math.Abs(Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and company = '" & compname & "' and qty < 0 and entry_date =  #" & date1 & "#").ToString))

    '            toExp += ((stexp * prel) / prelp)
    '            toExp += ((stexp1 * pres) / presp)
    '        End If
    '    End If

    'End Sub

    'Private Sub cal_prebal(ByVal date1 As Date, ByVal compname As String, ByVal optype As String, ByVal qty As Integer, ByVal rate As Double, ByRef prExp As Double, ByRef toExp As Double)
    '    Dim flgNew As Boolean = False
    '    Dim prow As DataRow() = prebal.Select("tdate =#" & date1 & "# and company='" & compname & "'")
    '    If Not prow Is Nothing Then
    '        If prow.Length = 0 Then
    '            ReDim prow(1)
    '            prow(0) = prebal.NewRow
    '            prow(0)("company") = compname
    '            prow(0)("tdate") = date1
    '            prow(0)("stbal") = 0
    '            prow(0)("futbal") = 0
    '            prow(0)("optbal") = 0
    '            flgNew = True
    '        End If
    '    End If
    '    Dim exp As Double = 0
    '    'calculate expense 
    '    '*****************************************************
    '    If optype = "E" Then 'for equity
    '        Dim stexp, stexp1, dst, ndst As Double
    '        'If date1 = Today.Date Then 'delivery base expense
    '        stexp = Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2)
    '        stexp1 = Math.Abs(Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "qty < 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2))
    '        dst = stexp - stexp1
    '        If dst > 0 Then
    '            ndst = stexp1
    '            exp = ((dst * dbl) / dblp)
    '            exp += ((stexp1 * ndbs) / ndbsp)
    '            exp += ((stexp1 * ndbl) / ndblp)
    '        Else
    '            ndst = stexp
    '            dst = -dst
    '            exp += ((dst * dbs) / dbsp)
    '            exp += ((stexp * ndbl) / ndblp)
    '            exp += ((stexp * ndbs) / ndbsp)
    '        End If
    '        prow(0)("stbal") = exp
    '        'Else 'for delivery base
    '        '    If qty > 0 Then 'long expense
    '        '        exp = (qty * rate * dbl) / dblp
    '        '    Else 'short expense
    '        '        exp = -(qty * rate * dbs) / dbsp
    '        '    End If
    '        '    prow(0)("stbal") = Val(prow(0)("stbal")) + exp
    '        '    exp = prow(0)("stbal")
    '        ' End If
    '        '*******************************************************
    '    ElseIf (optype = "X" Or optype = "" Or optype = "F") Then 'for future
    '        exp = 0
    '        If qty > 0 Then
    '            exp = (qty * rate * futl) / futlp
    '        Else
    '            exp = -(qty * rate * futs) / futsp
    '        End If
    '        prow(0)("futbal") = Val(prow(0)("futbal")) + exp
    '        '************************************************************
    '    Else 'for option
    '        exp = 0
    '        If Val(spl) <> 0 Then
    '            If qty > 0 Then
    '                exp = Val((qty * rate * spl) / splp)
    '            Else
    '                qty = -qty
    '                exp = Val((qty * rate * sps) / spsp)
    '            End If
    '        Else
    '            If qty > 0 Then
    '                exp = Val((qty * rate * prel) / prelp)
    '            Else
    '                qty = -qty
    '                exp = Val((qty * rate * pres) / presp)
    '            End If
    '        End If
    '        prow(0)("optbal") = Val(prow(0)("optbal")) + exp
    '    End If
    '    prow(0)("tot") = Val(prow(0)("stbal")) + Val(prow(0)("futbal")) + Val(prow(0)("optbal"))
    '    prebal.AcceptChanges()

    '    'for today expense
    '    If date1 = Today.Date Then
    '        toExp = exp
    '    Else
    '        prExp = exp
    '    End If

    '    If flgNew = True Then 'add new row
    '        prebal.Rows.Add(prow(0))
    '        objTrad.insert_prebal(prow(0))
    '    Else 'update expense
    '        objTrad.Delete_prBal(date1.Date, compname)
    '        objTrad.insert_prebal(prow(0))
    '        'With prow(0)
    '        '    objTrad.update_prebal(date1.Date, .Item("stbal"), .Item("futbal"), .Item("optbal"), .Item("company"))
    '        'End With

    '    End If

    '    'If date1 < Now.Date Then ' previous trades expense calulation
    '    '    Dim prebalance As New DataTable
    '    '    prebalance = objTrad.prebal
    '    '    Dim addprebal As New DataTable
    '    '    addprebal = New DataView(prebalance, "company = '" & compname & "' and tdate=#" & date1 & "#", "", DataViewRowState.CurrentRows).ToTable
    '    '    'if no prebalance for position then add new position
    '    '    If addprebal.Rows.Count = 0 Then
    '    '        'With addprebal.Columns
    '    '        '    ' .Add("tdate", GetType(Date))
    '    '        '    .Add("stbal", GetType(Double))
    '    '        '    .Add("futbal", GetType(Double))
    '    '        '    .Add("optbal", GetType(Double))
    '    '        '    '.Add("company", GetType(String))
    '    '        'End With
    '    '        Dim drow As DataRow = addprebal.NewRow
    '    '        addprebal.Rows.Add(drow)
    '    '        addprebal.Rows(0)("company") = compname
    '    '        addprebal.Rows(0)("tdate") = date1
    '    '        addprebal.Rows(0)("stbal") = 0
    '    '        addprebal.Rows(0)("futbal") = 0
    '    '        addprebal.Rows(0)("optbal") = 0
    '    '    End If
    '    '    addprebal.AcceptChanges()

    '    '    If optype = "E" Then
    '    '        If qty > 0 Then
    '    '            prExp = Val((qty * rate * dbl) / dblp)
    '    '            addprebal.Rows(0)("stbal") += prExp
    '    '        Else
    '    '            qty = -qty
    '    '            prExp = Val((qty * rate * dbs) / dbsp)
    '    '            addprebal.Rows(0)("stbal") += prExp
    '    '        End If
    '    '    ElseIf optype = "X" Or optype = "F" Or optype = "" Then
    '    '        If qty > 0 Then
    '    '            prExp = Val((qty * rate * futl) / futlp)
    '    '            addprebal.Rows(0)("futbal") += prExp
    '    '        Else
    '    '            qty = -qty
    '    '            prExp = Val((qty * rate * futs) / futsp)
    '    '            addprebal.Rows(0)("futbal") += prExp
    '    '        End If
    '    '    Else 'option type
    '    '        If Val(spl) <> 0 Then
    '    '            If qty > 0 Then
    '    '                prExp = Val((qty * rate * spl) / splp)
    '    '                addprebal.Rows(0)("optbal") += prExp
    '    '            Else
    '    '                qty = -qty
    '    '                prExp = Val((qty * rate * sps) / spsp)
    '    '                addprebal.Rows(0)("optbal") += prExp
    '    '            End If
    '    '        Else
    '    '            If qty > 0 Then
    '    '                prExp = Val((qty * rate * prel) / prelp)
    '    '                addprebal.Rows(0)("optbal") += prExp
    '    '            Else
    '    '                qty = -qty
    '    '                prExp = Val((qty * rate * pres) / presp)
    '    '                addprebal.Rows(0)("optbal") += prExp
    '    '            End If
    '    '        End If
    '    '    End If
    '    '    addprebal.Rows(0)("tot") = Math.Abs(Val(addprebal.Rows(0)("stbal")) + Val(addprebal.Rows(0)("futbal")) + Val(addprebal.Rows(0)("optbal")))
    '    '    objTrad.Delete_prBal(date1.Date, compname)
    '    '    objTrad.insert_prebal(addprebal)
    '    '    prExp = Val(addprebal.Rows(0)("tot"))
    '    'Else 'calculate today expense
    '    '    Dim stexp, stexp1, dst, ndst As Double
    '    '    'FOR Equity
    '    '    stexp = Math.Round(Val(dtEQTrades.Compute("sum(tot)", "qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2)
    '    '    stexp1 = Math.Abs(Math.Round(Val(dtEQTrades.Compute("sum(qty)", "qty < 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString), 2))
    '    '    dst = stexp - stexp1
    '    '    If dst > 0 Then
    '    '        ndst = stexp1
    '    '        toExp += ((dst * ndbl) / ndblp)
    '    '        toExp += ((stexp1 * ndbs) / ndbsp)
    '    '        toExp += ((stexp1 * ndbl) / ndblp)
    '    '    Else
    '    '        ndst = stexp
    '    '        dst = -dst
    '    '        toExp += ((dst * dbs) / dbsp)
    '    '        toExp += ((stexp * ndbl) / ndblp)
    '    '        toExp += ((stexp * ndbs) / ndbsp)
    '    '    End If
    '    '    'for FUTURe
    '    '    stexp = 0
    '    '    stexp1 = 0
    '    '    stexp = Val(dtFOTrades.Compute("sum(tot)", "cp not in ('C','P') and company = '" & compname & "' and qty > 0 and entry_date =  #" & date1 & "#").ToString)
    '    '    stexp1 = Math.Abs(Val(dtFOTrades.Compute("sum(tot)", "cp not in ('C','P') and company = '" & compname & "' and qty < 0 and entry_date =  #" & date1 & "#").ToString))
    '    '    toExp += ((stexp * futl) / futlp)
    '    '    toExp += ((stexp1 * futs) / futsp)

    '    '    'OPTION
    '    '    If Val(spl) <> 0 Then
    '    '        stexp = 0
    '    '        stexp1 = 0
    '    '        stexp = Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString)
    '    '        stexp1 = Math.Abs(Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and company = '" & compname & "' qty < 0 and entry_date =  #" & date1 & "#").ToString))
    '    '        toExp += ((stexp * spl) / splp)
    '    '        toExp += ((stexp1 * sps) / spsp)
    '    '    Else
    '    '        stexp = 0
    '    '        stexp1 = 0
    '    '        stexp = Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and qty > 0 and company = '" & compname & "' and entry_date =  #" & date1 & "#").ToString)
    '    '        stexp1 = Math.Abs(Val(dtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and company = '" & compname & "' and qty < 0 and entry_date =  #" & date1 & "#").ToString))

    '    '        toExp += ((stexp * prel) / prelp)
    '    '        toExp += ((stexp1 * pres) / presp)
    '    '    End If
    '    'End If

    'End Sub

    Private Sub cmbstrike_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbstrike.KeyDown

    End Sub

    Private Sub cmbstrike_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbstrike.KeyPress
        numonly(e)
    End Sub

    Private Sub cmbeqopt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbeqopt.Leave
        Try


            If cmbeqcomp.Text <> "" And cmbeqopt.Text <> "" Then
                cmbeqopt.Text = UCase(cmbeqopt.Text.Trim)
                If eqmasterdata.Compute("count(symbol)", "SERIES='" & cmbeqopt.Text.Trim & "' AND SYMBOL='" & cmbeqcomp.Text.Trim & "'") <= 0 Then
                    MsgBox("Not valid Series")
                    cmbeqopt.Text = ""
                    txteqscript.Text = ""
                    cmbeqopt.Focus()
                    Exit Sub
                End If
                txteqscript.Text = cmbeqcomp.SelectedValue.ToString & Space(2) & cmbeqopt.Text
                If eqmasterdata.Compute("count(symbol)", "script='" & txteqscript.Text.Trim & "'") <= 0 Then
                    MsgBox("Not valid Script")
                    eqform_clear()

                    Exit Sub
                End If

            End If
        Catch ex As Exception
            MsgBox("Invalid script")
            ' cmbeqcomp.Select()
        End Try
    End Sub



    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub scriptgenrate_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        REM Refreshing Global Database 
        'Call GSub_Fill_GDt_AllTrades()
        'Call GSub_Fill_GDt_Strategy()
        REM End
    End Sub
#Region "Currency Trading"
    Private Sub btnCurrencyExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCurrencyExit.Click
        Me.Close()
    End Sub
    Private Sub form_Currency_clear()
        cmbCurrencyInstrument.Text = ""
        cmbCurrencyComp.Text = ""
        cmbCurrencyStrike.Text = "0"
        txtCurrencyscript.Text = ""
        txtCurrencyunit.Text = "0"
        txtCurrencyrate.Text = "0"
        cmbCurrencyComp.SelectedIndex = 0
        cmbCurrencyComp.Focus()
    End Sub
    Private Sub btnCurrencyClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCurrencyClear.Click
        Call form_Currency_clear()
    End Sub

#End Region

    Private Sub btnCurrencySave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCurrencySave.Click
        Dim strDate As String
        Try
            If txtCurrencyunit.Text = "" Then
                txtCurrencyunit.Text = 0
            End If
            If txtCurrencyrate.Text = "" Then
                txtCurrencyrate.Text = 0
            End If
            If form_Currency_validation() = True Then
                Varmultiplier = Currencymaster.Compute("MAX(multiplier)", "Script='" & txtCurrencyscript.Text & "'")
                Dim tkk As Long = CLng(Val(objTrad.select_Currency_Trading.Compute("MAX(token)", "script='" & txtCurrencyscript.Text & "'").ToString))
                If tkk > 0 Then
                    MsgBox(txtCurrencyscript.Text & " script already exist in Traded")
                    Exit Sub
                End If
                Dim a, a1, script1 As String
                Dim tk As Long
                GVarMAXCURRTradingOrderNo = GVarMAXCURRTradingOrderNo + 1
                'insert trade to Trading table
                objScript.InstrumentName = cmbCurrencyInstrument.Text
                objScript.Company = cmbCurrencyComp.Text
                objScript.Mdate = CDate(cmbCurrencyExpdate.Text).Date
                objScript.StrikeRate = Val(cmbCurrencyStrike.Text)
                objScript.CP = UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1))
                objScript.Script = txtCurrencyscript.Text.Trim
                objScript.Units = Val(txtCurrencyunit.Text) * Varmultiplier
                objScript.Rate = Val(txtCurrencyrate.Text)
                objScript.EntryDate = DTPCurrencyEntryDate.Value.Date
                If UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "C" Or UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "P" Then
                    a = Mid(txtCurrencyscript.Text, Len(txtCurrencyscript.Text) - 1, 1)
                    a1 = Mid(txtCurrencyscript.Text, Len(txtCurrencyscript.Text), 1)
                    If a = "C" Then
                        script1 = Mid(txtCurrencyscript.Text, 1, Len(txtCurrencyscript.Text) - 2) & "P" & a1
                    Else
                        script1 = Mid(txtCurrencyscript.Text, 1, Len(txtCurrencyscript.Text) - 2) & "C" & a1
                    End If
                    tk = CLng(DTCurrencyContract.Compute("max(token)", "script='" & script1 & "'").ToString)
                    objScript.Token = tk
                    For Each row As DataRow In GdtFOTrades.Select("script='" & txtCurrencyscript.Text & "'")
                        If row("isliq") = True Then
                            objScript.Isliq = True ' "Yes"
                        Else
                            objScript.Isliq = False '"No"
                        End If
                        Exit For
                    Next
                Else
                    objScript.Token = 0
                    objScript.asset_tokan = 0
                    objScript.Isliq = False ' "No"
                End If
                objScript.orderno = GVarMAXCURRTradingOrderNo
                Dim tk1 As Long = CLng(Val(Currencymaster.Compute("max(token)", "script='" & txtCurrencyscript.Text & "'").ToString))
                If tk1 = 0 Then
                    MsgBox(txtCurrencyscript.Text & " does not exist in contract")
                End If
                objScript.Units = Val(txtCurrencyunit.Text) * Currencymaster.Compute("max(multiplier)", "script='" & txtCurrencyscript.Text & "'")

                'save trade to database trade table
                objScript.Insert_Currency_Trading()

                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                'get uid from trading table
                dtUid = objScript.select_Currency_trading_uid

                'Save to analysis's dtFOTrading table
                Dim tprow As DataRow
                tprow = GdtCurrencyTrades.NewRow
                tprow("uid") = dtUid.Rows(0)("uid")
                tprow("token") = 0
                tprow("mo") = Format(CDate(cmbCurrencyExpdate.Text).Date, "MM/yyyy")
                tprow("instrumentname") = cmbCurrencyInstrument.Text
                tprow("company") = cmbCurrencyComp.Text
                tprow("mdate") = CDate(cmbCurrencyExpdate.Text).Date
                tprow("Strikerate") = Val(cmbCurrencyStrike.Text)
                tprow("CP") = IIf(UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "X", "F", UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)))
                tprow("script") = txtCurrencyscript.Text.Trim
                tprow("qty") = Val(txtCurrencyunit.Text) * Varmultiplier
                tprow("Rate") = Val(txtCurrencyrate.Text)
                strDate = Format(DTPCurrencyEntryDate.Value.Date, "MMM/dd/yyyy")
                tprow("EntryDate") = Format(CDate(strDate).Date, "MMM/dd/yyyy")
                tprow("entry_date") = Format(CDate(strDate).Date, "MMM/dd/yyyy")
                tprow("isliq") = objScript.Isliq
                tprow("token1") = tk1
                tprow("orderno") = GVarMAXCURRTradingOrderNo
                tprow("entryno") = 0
                tprow("tot") = Val(txtCurrencyunit.Text) * Val(txtCurrencyrate.Text) * Varmultiplier
                tprow("issummary") = True
                tprow("isdisplay") = True
                GdtCurrencyTrades.Rows.Add(tprow)
                'calculate expense of inserted position
                Dim prExp, toExp As Double
                Call cal_prebal(DTPCurrencyEntryDate.Value.Date, cmbCurrencyComp.Text.Trim, UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)), CInt(txtCurrencyunit.Text) * Varmultiplier, Val(txtCurrencyrate.Text), prExp, toExp)
                'insert position to database's analysis table also
                Dim dtAna As New DataTable
                dtAna = objAna.fill_table_process(txtCurrencyscript.Text.Trim, CInt(txtCurrencyunit.Text) * Varmultiplier, Val(txtCurrencyrate.Text), prExp, toExp, DTPCurrencyEntryDate.Value.Date)

                'insert Currency trade to analysis table
                objScript.insert_CurrencyTrade_in_maintable(txtCurrencyscript.Text.Trim, dtAna, prExp, toExp, DTPCurrencyEntryDate.Value.Date)
                MsgBox("Script saved successfully", MsgBoxStyle.Information)

                LastOpenPosition = cmbCurrencyComp.Text
            End If

            '***********************************************************************************                objAna.fill_table_process(txtscript.Text.Trim)
            txtCurrencyrate.Text = "0"
            txtCurrencyunit.Text = "0"
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Function form_Currency_validation() As Boolean
        If cmbCurrencyInstrument.Text.Trim = "" Then
            MsgBox("Select Instrument Name", MsgBoxStyle.Information)
            cmbCurrencyInstrument.Focus()
            Return False
        End If
        If cmbCurrencyComp.Text.Trim = "" Then
            MsgBox("Enter Company Name", MsgBoxStyle.Information)
            cmbCurrencyComp.Focus()
            Return False
        End If
        If cmbCurrencyStrike.Text.Trim = "0" And (Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate", MsgBoxStyle.Information)
            cmbCurrencyStrike.Focus()
            Return False
        End If
        If cmbCurrencyCP.Text = "" Then
            MsgBox("Select Call/Put/Futre", MsgBoxStyle.Information)
            cmbCurrencyCP.Focus()
            Return False
        End If
        If Not IsNumeric(txtCurrencyunit.Text) Then
            MsgBox("Enter Units", MsgBoxStyle.Information)
            txtCurrencyunit.Focus()
            Return False
        End If
        If Not IsNumeric(txtCurrencyrate.Text) Then
            MsgBox("Enter Rate", MsgBoxStyle.Information)
            txtCurrencyrate.Focus()
            Return False
        End If
        If txtCurrencyunit.Text = "0" Then
            MsgBox("Enter Units", MsgBoxStyle.Information)
            txtCurrencyunit.Focus()
            Return False
        End If
        If txtCurrencyrate.Text = "0" Then
            MsgBox("Enter Rate", MsgBoxStyle.Information)
            txtCurrencyrate.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub cmbCurrencyComp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyComp.Enter
        cmbCurrencyComp.Height = 150
        cmbCurrencyComp.BringToFront()
    End Sub

    Private Sub cmbCurrencyinstrument_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyInstrument.Leave
        cmbCurrencyCP.Enabled = True
        cmbCurrencyStrike.Enabled = True
        cmbCurrencyInstrument.Height = cmbh
        If cmbCurrencyInstrument.Text.Trim <> "" And cmbCurrencyInstrument.Items.Count > 0 Then
            If (UCase(cmbCurrencyInstrument.Text) = "FUTCUR" Or UCase(cmbCurrencyInstrument.Text) = "FUTIRD" Or UCase(cmbCurrencyInstrument.Text) = "INDEX" Or UCase(cmbCurrencyInstrument.Text) = "UNDCUR" Or UCase(cmbCurrencyInstrument.Text) = "UNDIRD" Or UCase(cmbCurrencyInstrument.Text) = "OPTCUR") Then
                Dim dv As DataView = New DataView(DTCurrencyContract, "InstrumentName='" & cmbCurrencyInstrument.Text & "'", "option_type", DataViewRowState.CurrentRows)
                cmbCurrencyCP.DataSource = dv.ToTable(True, "option_type")
                cmbCurrencyCP.DisplayMember = "option_type"
                cmbCurrencyCP.ValueMember = "option_type"
                'cmbcp.Refresh()
                If cmbCurrencyCP.Items.Count > 0 Then
                    cmbCurrencyCP.SelectedIndex = 0
                End If
                If Not cmbCurrencyInstrument Is Nothing And cmbCurrencyInstrument.Items.Count > 0 Then
                    If cmbCurrencyInstrument.Text <> "" Then
                        If UCase(cmbCurrencyInstrument.Text) <> "OPTCUR" Then
                            cmbCurrencyCP.Enabled = False
                            cmbCurrencyStrike.DataSource = Nothing

                            cmbCurrencyStrike.Enabled = False
                            cmbCurrencyStrike.Text = 0
                            dv = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' AND InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "' AND expdate1 >='" & Now.Date & "' ", "expdate1", DataViewRowState.CurrentRows)
                            cmbCurrencyExpdate.DataSource = dv.ToTable(True, "expdate")
                            cmbCurrencyExpdate.DisplayMember = "expdate"
                            cmbCurrencyExpdate.ValueMember = "expdate"
                            '  cmbdate.Refresh()
                            ' cmbdate.Focus()
                        Else
                            cmbCurrencyCP.Enabled = True
                            cmbCurrencyStrike.Enabled = True
                            ' cmbcp.Focus()
                        End If
                    End If
                End If
            Else
                MsgBox("Select valid instrument name")
                cmbCurrencyInstrument.Text = ""
                cmbCurrencyInstrument.Focus()
            End If

        End If
        If cmbCurrencyInstrument.Text.Trim = "" Then
            cmbCurrencyCP.DataSource = Nothing
            cmbCurrencyStrike.DataSource = Nothing
            cmbCurrencyExpdate.DataSource = Nothing
        End If

    End Sub
    Private Sub cmbCurrencyinstrument_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyInstrument.SelectedValueChanged
        If cmbCurrencyInstrument.Text.Trim <> "" And cmbCurrencyInstrument.Items.Count > 0 Then
            Dim dv As DataView = New DataView(DTCurrencyContract, "InstrumentName='" & cmbCurrencyInstrument.Text & "'", "option_type", DataViewRowState.CurrentRows)
            cmbCurrencyCP.DataSource = dv.ToTable(True, "option_type")
            cmbCurrencyCP.DisplayMember = "option_type"
            cmbCurrencyCP.ValueMember = "option_type"
            'cmbcp.Refresh()
            If cmbCurrencyCP.Items.Count > 0 Then
                cmbCurrencyCP.SelectedIndex = 0
            End If
            If Not cmbCurrencyInstrument Is Nothing And cmbCurrencyInstrument.Items.Count > 0 Then
                If cmbCurrencyInstrument.Text <> "" Then
                    If UCase(cmbCurrencyInstrument.Text) <> "OPTCUR" Then
                        cmbCurrencyStrike.DataSource = Nothing
                        cmbCurrencyCP.Enabled = False
                        cmbCurrencyStrike.Enabled = False
                        cmbCurrencyStrike.Text = 0
                        dv = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' AND InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
                        cmbCurrencyExpdate.DataSource = dv.ToTable(True, "expdate")
                        cmbCurrencyExpdate.DisplayMember = "expdate"
                        cmbCurrencyExpdate.ValueMember = "expdate"
                        'cmbdate.Refresh()
                        'cmbdate.Focus()
                    Else
                        cmbCurrencyCP.Enabled = True
                        cmbCurrencyStrike.Enabled = True
                        'cmbcp.Focus()
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub cmbCurrencycp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyCP.Enter
        'cmbcp.Enabled = True
        ' cmbstrike.Enabled = True
        If cmbCurrencyInstrument.Text.Trim <> "" And cmbCurrencyInstrument.Items.Count > 0 And cmbCurrencyCP.Items.Count <= 0 Then
            If (UCase(cmbCurrencyInstrument.Text) = "FUTIDX" Or UCase(cmbCurrencyInstrument.Text) = "FUTSTK" Or UCase(cmbCurrencyInstrument.Text) = "OPTIDX" Or UCase(cmbCurrencyInstrument.Text) = "OPTSTK") Then

                Dim dv As DataView = New DataView(DTCurrencyContract, "InstrumentName='" & cmbCurrencyInstrument.Text & "'", "option_type", DataViewRowState.CurrentRows)
                cmbCurrencyCP.DataSource = dv.ToTable(True, "option_type")
                cmbCurrencyCP.DisplayMember = "option_type"
                cmbCurrencyCP.ValueMember = "option_type"
                '  cmbcp.Refresh()
                If cmbCurrencyCP.Items.Count > 0 Then
                    cmbCurrencyCP.SelectedIndex = 0
                End If
                If Not cmbCurrencyInstrument Is Nothing And cmbCurrencyInstrument.Items.Count > 0 Then
                    If cmbCurrencyInstrument.Text <> "" Then
                        If UCase(Mid(cmbCurrencyInstrument.Text, 1, 3)) = "FUT" Then
                            cmbCurrencyStrike.DataSource = Nothing

                            cmbCurrencyCP.Enabled = False
                            cmbCurrencyStrike.Enabled = False
                            cmbCurrencyStrike.Text = 0
                            dv = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' AND option_type='" & cmbCurrencyCP.Text & "'", "strike_price", DataViewRowState.CurrentRows)
                            cmbCurrencyExpdate.DataSource = dv.ToTable(True, "expdate")
                            cmbCurrencyExpdate.DisplayMember = "expdate"
                            cmbCurrencyExpdate.ValueMember = "expdate"
                        Else
                            cmbCurrencyCP.Enabled = True
                            cmbCurrencyStrike.Enabled = True
                            'cmbcp.Focus()
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub cmbcurrencycp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyCP.Leave
        If cmbCurrencyInstrument.Text.Trim <> "" And cmbCurrencyCP.Text <> "".Trim And cmbCurrencyCP.Items.Count > 0 Then
            If (UCase(cmbCurrencyCP.Text) = "CE" Or UCase(cmbCurrencyCP.Text) = "PE" Or UCase(cmbCurrencyCP.Text) = "CA" Or UCase(cmbCurrencyCP.Text) = "PA") Then
                Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "'", "strike_price", DataViewRowState.CurrentRows)
                'cmbstrike.Items.Clear()
                cmbCurrencyStrike.DataSource = dv.ToTable(True, "strike_price")
                cmbCurrencyStrike.DisplayMember = "strike_price"
                cmbCurrencyStrike.ValueMember = "strike_price"
                ' cmbstrike.Refresh()
                cmbCurrencyStrike.SelectedIndex = 0
            Else
                MsgBox("Select valid option type")
                cmbCurrencyCP.Text = ""
                cmbCurrencyCP.Focus()
            End If
        End If

    End Sub
    Private Sub cmbcurrencystrike_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyStrike.Enter
        cmbCurrencyStrike.Height = 150
        cmbCurrencyStrike.BringToFront()
        If cmbCurrencyCP.Text <> "" And cmbCurrencyCP.Items.Count > 0 And cmbCurrencyStrike.Items.Count <= 0 Then
            If (UCase(cmbCurrencyCP.Text) = "CE" Or UCase(cmbCurrencyCP.Text) = "PE" Or UCase(cmbCurrencyCP.Text) = "CA" Or UCase(cmbCurrencyCP.Text) = "PA") Then
                Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "'", "strike_price", DataViewRowState.CurrentRows)
                cmbCurrencyStrike.DataSource = dv.ToTable(True, "strike_price")
                cmbCurrencyStrike.DisplayMember = "strike_price"
                cmbCurrencyStrike.ValueMember = "strike_price"
                ' cmbstrike.Refresh()
                cmbstrike.SelectedIndex = 0
            End If
        End If
    End Sub
    Private Sub cmbcurrencystrike_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyStrike.Leave
        cmbCurrencyStrike.Height = cmbh
        If cmbCurrencyStrike.Text.Trim = "" Then
            cmbCurrencyStrike.Text = 0
        End If
        If (Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P") And cmbCurrencyStrike.Text <> "0" Then
            Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "' and strike_price=" & Val(cmbCurrencyStrike.Text) & " ", "strike_price", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid strike rate")
                cmbCurrencyStrike.Text = ""
                cmbCurrencyStrike.Focus()
                Exit Sub
            End If
            cmbCurrencyExpdate.DataSource = dv.ToTable(True, "expdate")
            cmbCurrencyExpdate.DisplayMember = "expdate"
            cmbCurrencyExpdate.ValueMember = "expdate"
            'cmbdate.Refresh()
            If cmbCurrencyExpdate.Items.Count > 0 Then
                cmbCurrencyExpdate.SelectedIndex = 0
            End If
        Else
            Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
            'If dv.ToTable.Rows.Count <= 0 Then
            '    MsgBox("Select valid strike rate")
            '    cmbstrike.Text = ""
            '    cmbstrike.Focus()
            '    Exit Sub
            'End If
            cmbCurrencyExpdate.DataSource = dv.ToTable(True, "expdate")
            cmbCurrencyExpdate.DisplayMember = "expdate"
            cmbCurrencyExpdate.ValueMember = "expdate"
            ' cmbdate.Refresh()
        End If

    End Sub
    Private Sub cmbCurrencyExpdate_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyExpdate.Enter
        If cmbCurrencyStrike.Text.Trim = "" Then
            cmbCurrencyStrike.Text = 0
        End If
        If (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") And cmbstrike.Text <> "0" And cmbstrike.Items.Count > 0 Then
            Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "' and strike_price=" & Val(cmbCurrencyStrike.Text) & " AND expdate1 >='" & Now.Date & "' ", "expdate1", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid strike rate")
                cmbCurrencyStrike.Text = ""
                cmbCurrencyStrike.Focus()
                Exit Sub
            End If
            cmbCurrencyExpdate.DataSource = dv.ToTable(True, "expdate")
            cmbCurrencyExpdate.DisplayMember = "expdate"
            cmbCurrencyExpdate.ValueMember = "expdate"
            'cmbdate.Refresh()
            If cmbCurrencyExpdate.Items.Count > 0 Then
                cmbCurrencyExpdate.SelectedIndex = 0
            End If
        ElseIf cmbCurrencyCP.Text <> "" And cmbCurrencyCP.Items.Count > 0 Then
            Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "' and InstrumentName='" & cmbCurrencyInstrument.Text & "' and option_type='" & cmbCurrencyCP.Text & "'  AND expdate1 >='" & Now.Date & "' ", "expdate1", DataViewRowState.CurrentRows)
            cmbCurrencyExpdate.DataSource = dv.ToTable(True, "expdate")
            cmbCurrencyExpdate.DisplayMember = "expdate"
            cmbCurrencyExpdate.ValueMember = "expdate"
            ' cmbdate.Refresh()
        End If
    End Sub
    Private Sub txtCurrencyunit_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCurrencyunit.Enter
        If cmbCurrencyComp.Text.Trim = "" Then
            MsgBox("Enter Security Name", MsgBoxStyle.Information)
            cmbCurrencyComp.Focus()
            Exit Sub
        End If
        If cmbCurrencyInstrument.Text.Trim = "" Then
            MsgBox("Select Instrument Name", MsgBoxStyle.Information)
            cmbCurrencyInstrument.Focus()
            Exit Sub
        End If

        If cmbCurrencyCP.SelectedValue.Trim = "" Then
            MsgBox("Select Call/Put/Futre", MsgBoxStyle.Information)
            cmbCurrencyCP.Focus()
            Exit Sub
        End If
        If cmbCurrencyStrike.Text.Trim = "0" And (Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate", MsgBoxStyle.Information)
            cmbCurrencyStrike.Focus()
            Exit Sub
        End If
        If cmbCurrencyExpdate.Text = "" Then
            MsgBox("Select date", MsgBoxStyle.Information)
            cmbCurrencyExpdate.Focus()
            Exit Sub
        End If
        Dim cp As String
        cp = ""
        If Mid(cmbCurrencyCP.Text, 1, 1) = "C" Or Mid(cmbCurrencyCP.Text, 1, 1) = "P" Then
            cp = UCase(cmbCurrencyCP.SelectedValue)
        Else
            cp = ""
        End If

        Dim script As String
        If cp = "" Then
            script = cmbCurrencyInstrument.SelectedValue & "  " & cmbCurrencyComp.SelectedValue & "  " & Format(CDate(cmbCurrencyExpdate.Text), "ddMMMyyyy")
        Else
            script = cmbCurrencyInstrument.SelectedValue & "  " & cmbCurrencyComp.SelectedValue & "  " & Format(CDate(cmbCurrencyExpdate.Text), "ddMMMyyyy") & "  " & Format(Val(cmbCurrencyStrike.Text), "###0.0000") & "  " & cp

        End If
        txtCurrencyscript.Text = script.Trim
        If DTCurrencyContract.Compute("count(symbol)", "script='" & txtCurrencyscript.Text.Trim & "'") <= 0 Then
            MsgBox("Not valid Script")
            form_Currency_clear()
            Exit Sub
        End If
    End Sub



    Private Sub txtCurrencyunit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrencyunit.KeyPress
        Call numonly(e)
    End Sub
    Private Sub txtCurrencyrate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrencyrate.KeyPress
        Call numonly(e)
    End Sub
    Private Sub cmbCurrencycomp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCurrencyComp.Leave
        cmbCurrencyComp.Height = cmbh
        If cmbCurrencyComp.Text.Trim <> "" And cmbCurrencyComp.Items.Count > 0 Then
            Dim dv As DataView = New DataView(DTCurrencyContract, "symbol='" & cmbCurrencyComp.Text & "'", "InstrumentName", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid Security")
                cmbCurrencyComp.Text = ""
                cmbCurrencyComp.Focus()
                Exit Sub
            End If
            cmbCurrencyInstrument.DataSource = dv.ToTable(True, "InstrumentName")
            cmbCurrencyInstrument.DisplayMember = "InstrumentName"
            cmbCurrencyInstrument.ValueMember = "InstrumentName"
            'cmbinstrument.Refresh()
            cmbCurrencyInstrument.SelectedIndex = 0
        End If
        If cmbCurrencyComp.Text.Trim = "" Then
            cmbCurrencyInstrument.DataSource = Nothing
            cmbCurrencyCP.DataSource = Nothing
            cmbCurrencyStrike.DataSource = Nothing
            cmbCurrencyExpdate.DataSource = Nothing
        End If
    End Sub

  

    
    Private Sub cmbcomp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcomp.SelectedIndexChanged

    End Sub

    Private Sub cmbcomp_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbcomp.KeyDown
        'If e.KeyCode = Keys.Enter Then

        'End If

    End Sub
End Class