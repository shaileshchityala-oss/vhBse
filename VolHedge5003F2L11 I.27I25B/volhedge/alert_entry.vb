Public Class alert_entry
    Dim masterdata As DataTable
    Dim objTrad As New trading
    Dim dv As DataView
    Dim dv1 As DataView
    Dim currdv As DataView
    Dim currdv1 As DataView
    Dim cmbh As Integer
    Private objAl As New alertentry
    Dim dtable As New DataTable
    Public compname As String

    Public currcompname As String

    Public fieldname As String

    Public currfieldname As String

    Public val1 As Double

    Public currval1 As Double

    Dim trading As DataTable
    Dim currency As DataTable

    Private Sub alert_entry_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub
    Private Sub alert_entry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        masterdata = New DataTable
        masterdata = cpfmaster
        currency = New DataTable
        currency = Currencymaster
        trading = New DataTable
        trading = objTrad.Comapany
        ' dv1 = New DataView(masterdata, "option_type in ('CA','CE','PA','PE')", "symbol", DataViewRowState.CurrentRows)
        'dv1 = New DataView(trading, "option_type in ('CA','CE','PA','PE')", "symbol", DataViewRowState.CurrentRows)
        dv1 = New DataView(currency, "option_type in ('XX')", "symbol", DataViewRowState.CurrentRows)
        cmbcurrcompany.DataSource = dv1.ToTable(True, "symbol")
        cmbcurrcompany.DisplayMember = "symbol"
        cmbcurrcompany.ValueMember = "symbol"
        cmbcurrcompany.Refresh()

        dv = New DataView(currency, "option_type in ('CA','CE','PA','PE')", "symbol", DataViewRowState.CurrentRows)
        cmbcurrcomp.DataSource = dv.ToTable(True, "symbol")
        cmbcurrcomp.DisplayMember = "symbol"
        cmbcurrcomp.ValueMember = "symbol"
        cmbcurrcomp.Refresh()

        dv = New DataView(masterdata, "option_type in ('CA','CE','PA','PE')", "symbol", DataViewRowState.CurrentRows)
        cmbcomp.DataSource = dv.ToTable(True, "symbol")
        cmbcomp.DisplayMember = "symbol"
        cmbcomp.ValueMember = "symbol"
        cmbcomp.Refresh()
        cmbh = cmbcomp.Height
        If trading.Rows.Count > 0 Then
            cmbcompany.Items.Add("Select")
            For Each drow As DataRow In trading.Rows
                cmbcompany.Items.Add(drow("company"))
            Next
        Else
            cmbcompany.Items.Add("Na")
        End If

        cmbcompany.SelectedIndex = 0
        objAl.Status = 1
        fill_data()
        cmbcompany.SelectedIndex = 0
        cmbcopt.SelectedIndex = 0
        cmbcfield.SelectedIndex = 0
        If compname <> "" Then
            cmbcompany.SelectedItem = UCase(compname)
            cmbcfield.SelectedItem = fieldname
            txtcsvalue.Text = val1
        End If

        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub
    'Private Sub cmbcomp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'cmbcomp.DroppedDown = True
    '    'cmbheight = True
    '    cmbcomp.Height = 150
    '    cmbcomp.BringToFront()
    'End Sub
    'Private Sub cmbcomp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'If cmbheight = True Then
    '    cmbcomp.Height = cmbh
    '    '    cmbheight = False
    '    'End If
    '    If cmbcomp.Text <> "" And cmbcomp.Items.Count > 0 Then
    '        Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and option_type in ('CA','CE','PA','PE') ", "option_type", DataViewRowState.CurrentRows)
    '        cmbcp.DataSource = dv.ToTable(True, "option_type")
    '        cmbcp.DisplayMember = "option_type"
    '        cmbcp.ValueMember = "option_type"
    '        cmbcp.Refresh()
    '        cmbcp.Text = 0
    '        cmbstrike.DataSource = dv.ToTable(True, "strike_price")
    '        cmbstrike.DisplayMember = "strike_price"
    '        cmbstrike.ValueMember = "strike_price"
    '        cmbstrike.Refresh()
    '        cmbdate.DataSource = dv.ToTable(True, "expdate1")
    '        cmbdate.DisplayMember = "expdate1"
    '        cmbdate.ValueMember = "expdate1"
    '        cmbdate.Refresh()
    '        cmbcp.Text = 0
    '        fill_script()
    '    End If
    'End Sub

    Private Sub rdbcomp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbcomp.CheckedChanged
        If rdbcomp.Checked = True Then
            pancomp.Enabled = True
            panscript.Enabled = False
            objAl.Status = 1
            If cmbcopt.Items.Count > 0 Then
                cmbcopt.SelectedIndex = 0
            End If
            If cmbcfield.Items.Count > 0 Then
                cmbcfield.SelectedIndex = 0
            End If
            fill_data()
        Else
            pancomp.Enabled = False
            panscript.Enabled = True
        End If
    End Sub
    Private Sub rdbscript_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbscript.CheckedChanged
        If rdbscript.Checked = True Then
            pancomp.Enabled = False
            panscript.Enabled = True
            objAl.Status = 2
            If cmbcomp.Items.Count > 0 Then
                cmbcomp.SelectedIndex = 0

            End If
            If cmbsopt.Items.Count > 0 Then
                cmbsopt.SelectedIndex = 0
            End If
            If cmbsfield.Items.Count > 0 Then
                cmbsfield.SelectedIndex = 0
            End If
            cmbcomp.Focus()
            fill_data()
        Else
            pancomp.Enabled = True
            panscript.Enabled = False
        End If
    End Sub
    Private Sub cmbcomp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcomp.Enter
        'cmbcomp.DroppedDown = True
        'cmbheight = True
        cmbcomp.Height = 150
        cmbcomp.BringToFront()
    End Sub
    Private Sub cmbcomp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcomp.Leave
        'If cmbheight = True Then
        cmbcomp.Height = cmbh
        '    cmbheight = False
        'End If
        If cmbcomp.Text <> "" And cmbcomp.Items.Count > 0 Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and option_type in ('CE','CA','PE','PA') ", "InstrumentName", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid Security.")
                cmbcomp.Text = ""
                cmbcomp.Focus()
                Exit Sub
            End If
            cmbinstrument.DataSource = dv.ToTable(True, "InstrumentName")
            cmbinstrument.DisplayMember = "InstrumentName"
            cmbinstrument.ValueMember = "InstrumentName"
            cmbinstrument.Refresh()
        End If
    End Sub
    Private Sub cmbinstrument_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbinstrument.Enter
        cmbinstrument.Height = 150
        cmbinstrument.BringToFront()
    End Sub

    Private Sub cmbinstrument_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbinstrument.Leave
        If cmbinstrument.Text <> "" Then
            If (UCase(cmbinstrument.Text) = "OPTIDX" Or UCase(cmbinstrument.Text) = "OPTSTK") Then

                cmbinstrument.Height = cmbh
                Dim dv As DataView = New DataView(masterdata, "InstrumentName='" & cmbinstrument.Text & "' and option_type in ('CE','CA','PE','PA') ", "option_type", DataViewRowState.CurrentRows)
                cmbcp.DataSource = dv.ToTable(True, "option_type")
                cmbcp.DisplayMember = "option_type"
                cmbcp.ValueMember = "option_type"
                cmbcp.Refresh()
                cmbcp.SelectedIndex = 0
                If Not cmbinstrument Is Nothing And cmbinstrument.Items.Count > 0 Then
                    If cmbinstrument.Text <> "" Then
                        If UCase(Mid(cmbinstrument.Text, 1, 3)) = "FUT" Then
                            cmbcp.Enabled = False
                            cmbstrike.Enabled = False

                        Else
                            cmbcp.Enabled = True
                            cmbstrike.Enabled = True
                        End If
                    End If
                End If
            Else
                MsgBox("Select valid InstrumentName.")
                cmbinstrument.Text = ""
                cmbinstrument.Focus()
            End If
        End If
    End Sub
    Private Sub cmbcp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcp.Leave
        If cmbcp.Text <> "" Then
            If (UCase(cmbcp.Text) = "CA" Or UCase(cmbcp.Text) = "CE" Or UCase(cmbcp.Text) = "PE" Or UCase(cmbcp.Text) = "PA") Then
                Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "' and   option_type in ('CE','CA','PE','PA') ", "strike_price", DataViewRowState.CurrentRows)
                cmbstrike.DataSource = dv.ToTable(True, "strike_price")
                cmbstrike.DisplayMember = "strike_price"
                cmbstrike.ValueMember = "strike_price"
                cmbstrike.Refresh()
                cmbstrike.SelectedIndex = 0
            Else
                MsgBox("Select valid Option type.")
                cmbcp.Text = ""
                cmbcp.Focus()
            End If

        End If
    End Sub
    Private Sub cmbstrike_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstrike.Leave
        cmbstrike.Height = cmbh
        If cmbstrike.Text.Trim = "" Then
            cmbstrike.Text = 0
        End If
        If (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") And cmbstrike.Text <> "0" Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "' and strike_price=" & val(cmbstrike.Text) & " and  option_type in ('CE','CA','PE','PA')  ", "strike_price", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid Strike rate.")
                cmbstrike.Text = ""
                cmbstrike.Focus()
                Exit Sub
            End If
            'dv.ToTable.Columns("mdate").DataType = GetType(Date)
            cmbdate.Items.Clear()
            For Each drow As DataRow In dv.ToTable(True, "expdate1").Rows
                cmbdate.Items.Add(Format(CDate(drow("expdate1")), "dd/MMM/yyyy"))
            Next
            ' cmbdate.DataSource = dv.ToTable(True, "mdate")
            'cmbdate.DisplayMember = "mdate"
            'cmbdate.ValueMember = "mdate"
            If cmbdate.Items.Count > 0 Then
                cmbdate.Refresh()
                cmbdate.SelectedIndex = 0
            End If
            'Else
            '    Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
            '    cmbdate.DataSource = dv.ToTable(True, "expdate")
            '    cmbdate.DisplayMember = "expdate"
            '    cmbdate.ValueMember = "expdate"
            '    cmbdate.Refresh()
        End If


    End Sub
    Private Sub cmbdate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdate.Leave
        If cmbcomp.Text.Trim = "" Then
            MsgBox("Enter Security Name.", MsgBoxStyle.Information)
            cmbcomp.Focus()
            Exit Sub
        End If
        If cmbinstrument.Text.Trim = "" Then
            MsgBox("Select Instrument Name.", MsgBoxStyle.Information)
            cmbinstrument.Focus()
            Exit Sub
        End If

        If cmbcp.Text.Trim = "" Then
            MsgBox("Select Call/Put/Futre.", MsgBoxStyle.Information)
            cmbcp.Focus()
            Exit Sub
        End If
        If cmbstrike.Text.Trim = "0" And (Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate.", MsgBoxStyle.Information)
            cmbstrike.Focus()
            Exit Sub
        End If
        If cmbdate.Text = "" Then
            MsgBox("Select Date.", MsgBoxStyle.Information)
            cmbdate.Focus()
            Exit Sub
        End If
        Dim cp As String
        cp = ""

        REM For Add Alert Scriptwise   - For  OPTSTK Remove "CA" And "PA"  
        If Mid(cmbcp.Text, 1, 1) = "C" Or Mid(cmbcp.Text, 1, 1) = "P" Then
            If Mid(cmbcp.Text, 1, 1) = "C" And cmbinstrument.Text.Trim = "OPTIDX" Then
                cp = UCase("CE")
            ElseIf Mid(cmbcp.Text, 1, 1) = "C" And cmbinstrument.Text.Trim = "OPTSTK" Then
                'cp = UCase("CA")
                cp = UCase("CE")
            ElseIf Mid(cmbcp.Text, 1, 1) = "P" And cmbinstrument.Text.Trim = "OPTIDX" Then
                cp = UCase("PE")
            ElseIf Mid(cmbcp.Text, 1, 1) = "P" And cmbinstrument.Text.Trim = "OPTSTK" Then
                'cp = UCase("PA")
                cp = UCase("PE")
            End If
        Else
            cp = ""
        End If
        ''If UCase(cmbcp.Items.ToString) = "CALL" And UCase(cmbinstrument.Items.ToString) = "OPTSTK" Then
        ''    cp = "CA"
        ''ElseIf UCase(cmbcp.Text.ToString) = "CALL" And UCase(cmbinstrument.Text.ToString) = "OPTIDX" Then
        ''    cp = "CE"
        ''ElseIf UCase(cmbcp.Text.ToString) = "PUT" And UCase(cmbinstrument.Text.ToString) = "OPTSTK" Then
        ''    cp = "PA"
        ''ElseIf UCase(cmbcp.Text.ToString) = "PUT" And UCase(cmbinstrument.Text.ToString) = "OPTIDX" Then
        ''    cp = "PE"
        ''Else
        ''    cp = ""
        ''End If

        Dim script As String
        'If cp = "" Then
        '    script = cmbinstrument.SelectedValue & "  " & cmbcomp.SelectedValue & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy")
        'Else
        '    script = cmbinstrument.SelectedValue & "  " & cmbcomp.SelectedValue & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy") & "  " & Format(val(cmbstrike.Text), "###0.00") & "  " & cp
        'End If

        script = cmbinstrument.Text & "  " & cmbcomp.Text & "  " & Format(CDate(cmbdate.Text), "ddMMMyyyy")

        If cp <> "" Then
            script = script & "  " & Format(Val(cmbstrike.Text), "###0.00") & "  " & cp
        End If

        txtscript.Text = script
    End Sub
    
    Private Sub cmbcopt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcopt.SelectedIndexChanged
        'txtcsvalue.Text = "0"
        'txtcevalue.Text = "0"
        If cmbcopt.SelectedIndex = 1 Then
            lblcvalue2.Visible = True
            'lblcval2.Visible = True
            txtcevalue.Visible = True
            lblcvalue1.Text = "Value-1"
        Else

            lblcvalue2.Visible = False
            'lblcval2.Visible = False
            txtcevalue.Visible = False
            lblcvalue1.Text = "Value"
        End If
    End Sub
    Private Sub cmbsfield_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsfield.SelectedIndexChanged

    End Sub
    Private Sub cmbsopt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsopt.SelectedIndexChanged
        txtsevalue.Text = "0"
        txtssvalue.Text = "0"
        If cmbsopt.SelectedIndex = 1 Then
            lblsvalue2.Visible = True
            '  lblsval2.Visible = True
            txtsevalue.Visible = True
            lblsvalue1.Text = "Value-1"
        Else

            lblsvalue2.Visible = False
            ' lblsval2.Visible = False
            txtsevalue.Visible = False
            lblsvalue1.Text = "Value"
        End If

    End Sub
    Private Sub script_clear()
        cmbinstrument.Text = ""
        cmbcomp.Text = ""
        cmbstrike.Text = "0"
        txtscript.Text = ""
        txtsevalue.Text = "0"
        txtssvalue.Text = "0"
        cmbcp.SelectedIndex = 0
        cmbsopt.SelectedIndex = 0
        cmbsfield.SelectedIndex = 0
        lblsvalue2.Visible = False
        ' lblsval2.Visible = False
        txtsevalue.Visible = False
        lblsvalue1.Text = "Value"
    End Sub
    Private Sub Currscript_clear()
        cmbcurrinstrument.Text = ""
        cmbcurrcomp.Text = ""
        cmbcurrstrike.Text = "0"
        txtcurrscript.Text = ""
        txtcurrsevalue.Text = "0"
        txtcurrssvalue.Text = "0"
        cmbcurrcp.SelectedIndex = 0
        cmbcurrsopt.SelectedIndex = 0
        cmbcurrsfield.SelectedIndex = 0
        lblcurrsvalue2.Visible = False
        ' lblsval2.Visible = False
        txtcurrsevalue.Visible = False
        lblcurrsvalue1.Text = "Value"
    End Sub
    Private Sub company_clear()
        cmbcompany.SelectedIndex = 0
        cmbcopt.SelectedIndex = 0
        cmbcfield.SelectedIndex = 0
        txtcsvalue.Text = "0"
        txtcevalue.Text = "0"
        lblcvalue2.Visible = False
        ' lblcval2.Visible = False
        txtcevalue.Visible = False
        lblcvalue1.Text = "Value"
    End Sub
    Private Sub Currcompany_clear()
        cmbcurrcompany.SelectedIndex = 0
        cmbcurrcopt.SelectedIndex = 0
        cmbcurrcfield.SelectedIndex = 0
        txtcurrcsvalue.Text = "0"
        txtcurrcevalue.Text = "0"
        lblcurrcvalue2.Visible = False
        ' lblcval2.Visible = False
        txtcurrcevalue.Visible = False
        lblcurrcvalue1.Text = "Value"
    End Sub
    Private Function form_validation() As Boolean
        If rdbcomp.Checked = True Then
            If cmbcompany.SelectedItem.ToString = "" Then
                MsgBox("Select Company Name.", MsgBoxStyle.Information)
                cmbcompany.Focus()
                form_validation = False
                Exit Function
            End If
            If cmbcopt.SelectedItem.ToString = "" Then
                MsgBox("Enter Operator.", MsgBoxStyle.Information)
                cmbcopt.Focus()
                form_validation = False
                Exit Function
            End If

            If cmbcfield.SelectedItem.ToString = "" Then
                MsgBox("Select Field.", MsgBoxStyle.Information)
                cmbcfield.Focus()
                form_validation = False
                Exit Function
            End If
            If txtcsvalue.Text = "0" Then
                MsgBox("Enter Value-1.", MsgBoxStyle.Information)
                txtcsvalue.Focus()
                form_validation = False
                Exit Function
            End If
            If cmbcopt.SelectedIndex = 1 Then
                If txtcevalue.Text = "0" Then
                    MsgBox("Enter Value-2.", MsgBoxStyle.Information)
                    txtcevalue.Focus()
                    form_validation = False
                    Exit Function
                Else
                    If val(txtcsvalue.Text) > val(txtcevalue.Text) Then
                        MsgBox("Enter Value-2 greater than Value-1.", MsgBoxStyle.Information)
                        txtcevalue.Focus()
                        form_validation = False
                        Exit Function
                    End If
                End If
            End If
            form_validation = True
        Else
            If txtscript.Text.Trim = "" Then
                MsgBox("Select Script.", MsgBoxStyle.Information)
                cmbcomp.Focus()
                form_validation = False
                Exit Function
            End If
            If cmbsopt.SelectedItem.ToString = "" Then
                MsgBox("Enter Operator.", MsgBoxStyle.Information)
                cmbsopt.Focus()
                form_validation = False
                Exit Function
            End If

            If cmbsfield.SelectedItem.ToString = "" Then
                MsgBox("Select Field.", MsgBoxStyle.Information)
                cmbsfield.Focus()
                form_validation = False
                Exit Function
            End If
            If txtssvalue.Text = "0" Then
                MsgBox("Enter Value-1.", MsgBoxStyle.Information)
                txtssvalue.Focus()
                form_validation = False
                Exit Function
            End If
            If cmbcopt.SelectedIndex = 1 Then
                If txtsevalue.Text = "0" Then
                    MsgBox("Enter Value-2.", MsgBoxStyle.Information)
                    txtsevalue.Focus()
                    form_validation = False
                    Exit Function
                Else
                    If val(txtssvalue.Text) > val(txtsevalue.Text) Then
                        MsgBox("Enter Value-2 greater than Value-1.", MsgBoxStyle.Information)
                        txtsevalue.Focus()
                        form_validation = False
                        Exit Function
                    End If
                End If
            End If
            form_validation = True
        End If
    End Function
    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        If form_validation() Then
            Dim chk As Boolean = False
            If rdbcomp.Checked = True Then
                If UCase(cmbcompany.SelectedItem.ToString) = "NA" Or UCase(cmbcompany.SelectedItem.ToString) = "SELECT" Then
                    MsgBox("Select Company.")
                    cmbcompany.Focus()
                    Exit Sub
                End If
                If UCase(cmbcfield.SelectedItem.ToString) = "NA" Or UCase(cmbcfield.SelectedItem.ToString) = "SELECT" Then
                    MsgBox("Select Field.")
                    cmbcfield.Focus()
                    Exit Sub
                End If
                If UCase(cmbcopt.SelectedItem.ToString) = "NA" Or UCase(cmbcopt.SelectedItem.ToString) = "SELECT" Then
                    MsgBox("Select Operator.")
                    cmbcopt.Focus()
                    Exit Sub
                End If
                For Each drow As DataRow In dtable.Select("comp_script='" & cmbcompany.SelectedItem & "'  and field='" & cmbcfield.SelectedItem & "' and opt='" & cmbcopt.SelectedItem & "'")
                    chk = True
                    Exit For
                Next
                If chk = True And CInt(lbluid.Text) = 0 Then
                    MsgBox("Security and field already added.")
                    Exit Sub
                End If
                If chk = False Then
                    objAl.Comp_Script = cmbcompany.SelectedItem.ToString
                    objAl.Opt = cmbcopt.SelectedItem.ToString
                    objAl.Field = cmbcfield.SelectedItem.ToString
                    objAl.Value1 = Val(txtcsvalue.Text)
                    objAl.Value2 = Val(txtcevalue.Text)
                    objAl.Entrydate = Now.Date
                    objAl.Status = 1
                    objAl.Units = 0
                    objAl.Insert()

                Else
                    objAl.Comp_Script = cmbcompany.SelectedItem.ToString
                    objAl.Opt = cmbcopt.SelectedItem.ToString
                    objAl.Field = cmbcfield.SelectedItem.ToString
                    objAl.Value1 = Val(txtcsvalue.Text)
                    objAl.Value2 = Val(txtcevalue.Text)
                    objAl.Entrydate = Now.Date
                    objAl.Status = 1
                    objAl.Units = 0
                    objAl.update(CInt(lbluid.Text))
                End If
                company_clear()
            Else
                If UCase(cmbinstrument.SelectedValue) = "NA" Or UCase(cmbinstrument.SelectedValue) = "SELECT" Then
                    MsgBox("Select Instrument Name.")
                    cmbinstrument.Focus()
                    Exit Sub
                End If
                If UCase(cmbcp.SelectedValue) = "NA" Or UCase(cmbcp.SelectedValue) = "SELECT" Then
                    MsgBox("Select Call/Put.")
                    cmbcp.Focus()
                    Exit Sub
                End If
                If UCase(cmbstrike.SelectedValue) = "NA" Or UCase(cmbstrike.SelectedValue) = "SELECT" Then
                    MsgBox("Select Strike Price.")
                    cmbstrike.Focus()
                    Exit Sub
                End If
                If UCase(cmbdate.SelectedItem.ToString) = "NA" Or UCase(cmbdate.SelectedItem.ToString) = "SELECT" Then
                    MsgBox("Select Expiry.")
                    cmbdate.Focus()
                    Exit Sub
                End If
                If UCase(cmbsfield.SelectedItem.ToString) = "NA" Or UCase(cmbsfield.SelectedItem.ToString) = "SELECT" Then
                    MsgBox("Select Field.")
                    cmbsfield.Focus()
                    Exit Sub
                End If
                If UCase(cmbsopt.SelectedItem.ToString) = "NA" Or UCase(cmbsopt.SelectedItem.ToString) = "SELECT" Then
                    MsgBox("Select Operator.")
                    cmbsopt.Focus()
                    Exit Sub
                End If
                'If txtunits.Text.Trim = "" Then
                '    txtunits.Text = 0
                'End If
                'If Not IsNumeric(txtunits.Text) Then
                '    MsgBox("Enter Units", MsgBoxStyle.Information)
                '    txtunits.Focus()
                '    Exit Sub
                'End If
                'If txtunits.Text = "0" Then
                '    MsgBox("Enter Units", MsgBoxStyle.Information)
                '    txtunits.Focus()
                '    Exit Sub
                'End If
                For Each drow As DataRow In dtable.Select("comp_script='" & txtscript.Text & "' and field='" & cmbsfield.SelectedItem & "' and opt='" & cmbsopt.SelectedItem & "'")
                    chk = True
                    Exit For
                Next
                Dim tk1 As Long = CLng(IIf(IsDBNull(masterdata.Compute("max(token)", "script='" & txtscript.Text & "'")), 0, masterdata.Compute("max(token)", "script='" & txtscript.Text & "'")))

                If tk1 = 0 Then
                    MsgBox(txtscript.Text & " does not exist in contract")
                    chk = True
                End If
                If chk = False Then
                    objAl.Comp_Script = txtscript.Text
                    objAl.Opt = cmbsopt.SelectedItem.ToString
                    objAl.Field = cmbsfield.SelectedItem.ToString
                    objAl.Value1 = Val(txtssvalue.Text)
                    objAl.Value2 = Val(txtsevalue.Text)
                    objAl.Entrydate = Now.Date
                    objAl.Units = Val(txtunits.Text)
                    objAl.Status = 2
                    objAl.Insert()
                Else
                    objAl.Comp_Script = txtscript.Text
                    objAl.Opt = cmbsopt.SelectedItem.ToString
                    objAl.Field = cmbsfield.SelectedItem.ToString
                    objAl.Value1 = Val(txtssvalue.Text)
                    objAl.Value2 = Val(txtsevalue.Text)
                    objAl.Entrydate = Now.Date
                    objAl.Units = Val(txtunits.Text)
                    objAl.Status = 2
                    objAl.update(CInt(lbluid.Text))
                End If
                script_clear()

            End If


            fill_data()
        End If
    End Sub
    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        If rdbcomp.Checked = True Then
            company_clear()
        Else
            script_clear()
        End If
    End Sub
    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
    Private Sub cmbdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdelete.Click
        grdalert.EndEdit()
        'For Each grow As DataGridViewRow In grdalert.Rows
        '    MsgBox(grow.Cells(0).Value)
        'Next
        objAl.delete(dtable, grdalert)
        fill_data()
    End Sub
    Private Sub chkcheck_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If grdalert.RowCount > 0 Then
            If chkcheck.Checked = False Then
                For i As Integer = 0 To grdalert.RowCount - 1
                    grdalert.Rows(i).Cells(0).Value = False
                Next
            Else
                For i As Integer = 0 To grdalert.RowCount - 1
                    grdalert.Rows(i).Cells(0).Value = True
                Next
            End If
        End If
    End Sub
    Private Sub fill_data()
        If rdbcomp.Checked = True Then
            objAl.Status = 1
        Else
            objAl.Status = 2
        End If
        dtable = New DataTable
        dtable = objAl.select_data
        'dtable.Columns.Add("status", GetType(Boolean))
        'dtable.AcceptChanges()
        'For Each drow As DataRow In dtable.Rows
        '    drow("status") = False
        'Next
        grdalert.DataSource = dtable
    End Sub
    Private Sub fill_Curr_data()
        If rdbcurrcomp.Checked = True Then
            objAl.Status = 3
        Else
            objAl.Status = 4
        End If
        dtable = New DataTable
        dtable = objAl.select_data
        grdalert.DataSource = dtable
    End Sub
    Private Sub grdalert_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)

    End Sub
    Private Sub grdalert_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdalert.CellEndEdit
        If e.ColumnIndex = 0 Then
            grdalert.EndEdit()
        End If
    End Sub
    Private Sub grdalert_CellLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdalert.CellLeave
        'If e.ColumnIndex = 7 Then
        '    dtable.Rows(e.RowIndex)("status") = grdalert.Rows(e.RowIndex).Cells(7).Value
        '    'grdalert.EndEdit()
        'End If
    End Sub

    Private Sub cmbstrike_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstrike.Enter
        cmbstrike.Height = 150
        cmbstrike.BringToFront()
    End Sub

    Private Sub grdalert_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdalert.CellDoubleClick
        If objAl.Status = 1 Or objAl.Status = 2 Then
            If rdbcomp.Checked = True Then
                Dim uid As Integer
                uid = grdalert.Rows(e.RowIndex).Cells(1).Value
                lbluid.Text = uid
                For Each drow As DataRow In dtable.Select("uid=" & uid & "")
                    cmbcompany.SelectedItem = drow("comp_script")
                    cmbcfield.SelectedItem = drow("field")
                    cmbcopt.SelectedItem = drow("opt")
                    txtcsvalue.Text = drow("value1")
                    txtcevalue.Text = drow("value2")
                Next
                ' objAl.Status = 1
            Else
                Dim uid As Integer
                uid = grdalert.Rows(e.RowIndex).Cells(1).Value
                lbluid.Text = uid
                For Each drow As DataRow In dtable.Select("uid=" & uid & "")
                    txtscript.Text = drow("comp_script")
                    cmbsfield.SelectedItem = drow("field")
                    cmbsopt.SelectedItem = drow("opt")
                    txtssvalue.Text = drow("value1")
                    txtsevalue.Text = drow("value2")
                    txtunits.Text = drow("units")
                Next
                For Each drow As DataRow In masterdata.Select("script='" & txtscript.Text & "'")
                    cmbcomp.SelectedValue = drow("symbol")
                    cmbcomp.Focus()
                    cmbinstrument.Focus()
                    cmbinstrument.SelectedValue = drow("instrumentname")
                    cmbcp.Focus()
                    cmbcp.SelectedValue = drow("option_type")
                    cmbstrike.Focus()
                    cmbstrike.SelectedValue = drow("strike_price")
                    cmbdate.Focus()
                    cmbdate.SelectedItem = Format(CDate(drow("expdate1")), "dd/MMM/yyyy")
                    txtunits.Focus()
                Next
                ' objAl.Status = 2
            End If
        Else
            If rdbcurrcomp.Checked = True Then
                Dim uid As Integer
                uid = grdalert.Rows(e.RowIndex).Cells(1).Value
                lblcurruid.Text = uid
                For Each drow As DataRow In dtable.Select("uid=" & uid & "")
                    cmbcurrcompany.SelectedItem = drow("comp_script")
                    cmbcurrcfield.SelectedItem = drow("field")
                    cmbcurrcopt.SelectedItem = drow("opt")
                    txtcurrcsvalue.Text = drow("value1")
                    txtcurrcevalue.Text = drow("value2")
                Next
                ' objAl.Status = 1
            Else
                Dim uid As Integer
                uid = grdalert.Rows(e.RowIndex).Cells(1).Value
                lblcurruid.Text = uid
                For Each drow As DataRow In dtable.Select("uid=" & uid & "")
                    txtcurrscript.Text = drow("comp_script")
                    cmbcurrsfield.SelectedItem = drow("field")
                    cmbcurrsopt.SelectedItem = drow("opt")
                    txtcurrssvalue.Text = drow("value1")
                    txtcurrsevalue.Text = drow("value2")
                    txtcurrunits.Text = drow("units")
                Next
                For Each drow As DataRow In currency.Select("script='" & txtcurrscript.Text & "'")
                    cmbcurrcomp.SelectedValue = drow("symbol")
                    cmbcurrcomp.Focus()
                    cmbcurrinstrument.Focus()
                    cmbcurrinstrument.SelectedValue = drow("instrumentname")
                    cmbcurrcp.Focus()
                    cmbcurrcp.SelectedValue = drow("option_type")
                    cmbcurrstrike.Focus()
                    cmbcurrstrike.SelectedValue = drow("strike_price")
                    cmbcurrdate.Focus()
                    cmbcurrdate.SelectedItem = Format(CDate(drow("expdate1")), "dd/MMM/yyyy")
                    txtcurrunits.Focus()
                Next
                ' objAl.Status = 2
            End If
        End If
    End Sub


    Private Sub chkcheck_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcheck.CheckedChanged
        If grdalert.RowCount > 0 Then
            If chkcheck.Checked = False Then
                For i As Integer = 0 To grdalert.RowCount - 1
                    grdalert.Rows(i).Cells(0).Value = False
                Next
            Else
                For i As Integer = 0 To grdalert.RowCount - 1
                    grdalert.Rows(i).Cells(0).Value = True
                Next
            End If
        End If
    End Sub

    Private Sub alert_entry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub cmbstrike_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbstrike.KeyPress
        numonly(e)
    End Sub

    Private Sub txtcsvalue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcsvalue.KeyPress
        numonly(e)
    End Sub

    Private Sub txtcevalue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcevalue.KeyPress
        numonly(e)
    End Sub

    Private Sub txtssvalue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtssvalue.KeyPress
        numonly(e)

    End Sub

    Private Sub txtsevalue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsevalue.KeyPress
        numonly(e)
    End Sub
    Private Sub pancomp_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pancomp.Paint

    End Sub

    Private Sub rdbcurrcomp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbcurrcomp.CheckedChanged
        If rdbcurrcomp.Checked = True Then
            pancurrcomp.Enabled = True
            pancurrscript.Enabled = False
            objAl.Status = 3
            If cmbcurrcopt.Items.Count > 0 Then
                cmbcurrcopt.SelectedIndex = 0
            End If
            If cmbcurrcfield.Items.Count > 0 Then
                cmbcurrcfield.SelectedIndex = 0
            End If
            fill_Curr_data()
        Else
            pancurrcomp.Enabled = False
            pancurrscript.Enabled = True
        End If
    End Sub

    Private Sub rdbcurrscript_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbcurrscript.CheckedChanged
        If rdbcurrscript.Checked = True Then
            pancurrcomp.Enabled = False
            pancurrscript.Enabled = True
            objAl.Status = 4
            If cmbcurrcomp.Items.Count > 0 Then
                cmbcurrcomp.SelectedIndex = 0
            End If
            If cmbcurrsopt.Items.Count > 0 Then
                cmbcurrsopt.SelectedIndex = 0
            End If
            If cmbcurrsfield.Items.Count > 0 Then
                cmbcurrsfield.SelectedIndex = 0
            End If
            cmbcurrcomp.Focus()
            fill_Curr_data()
        Else
            pancurrcomp.Enabled = True
            pancurrscript.Enabled = False
        End If
    End Sub

    Private Sub cmbcurrcopt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcurrcopt.SelectedIndexChanged
        If cmbcurrcopt.SelectedIndex = 1 Then
            lblcurrcvalue2.Visible = True
            'lblcval2.Visible = True
            txtcurrcevalue.Visible = True
            lblcurrcvalue1.Text = "Value-1"
        Else
            lblcurrcvalue2.Visible = False
            'lblcval2.Visible = False
            txtcurrcevalue.Visible = False
            lblcurrcvalue1.Text = "Value"
        End If
    End Sub

    Private Sub cmbcurrcomp_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcurrcomp.Enter
        cmbcurrcomp.Height = 150
        cmbcurrcomp.BringToFront()
    End Sub

    Private Sub cmbcurrcomp_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcurrcomp.Leave
        'If cmbheight = True Then
        cmbcurrcomp.Height = cmbh
        '    cmbheight = False
        'End If
        If cmbcurrcomp.Text <> "" And cmbcurrcomp.Items.Count > 0 Then
            Dim dv As DataView = New DataView(currency, "symbol='" & cmbcurrcomp.Text & "' and option_type in ('CE','CA','PE','PA') ", "InstrumentName", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select Valid Security.")
                cmbcomp.Text = ""
                cmbcomp.Focus()
                Exit Sub
            End If
            cmbcurrinstrument.DataSource = dv.ToTable(True, "InstrumentName")
            cmbcurrinstrument.DisplayMember = "InstrumentName"
            cmbcurrinstrument.ValueMember = "InstrumentName"
            cmbcurrinstrument.Refresh()
        End If
    End Sub
    Private Sub cmbcurrinstrument_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcurrinstrument.Enter
        cmbcurrinstrument.Height = 150
        cmbcurrinstrument.BringToFront()
    End Sub

    Private Sub cmbcurrinstrument_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcurrinstrument.Leave
        If cmbcurrinstrument.Text <> "" Then
            If (UCase(cmbcurrinstrument.Text) = "OPTCUR" Or UCase(cmbcurrinstrument.Text) = "OPTCUR") Then
                cmbcurrinstrument.Height = cmbh
                Dim dv As DataView = New DataView(currency, "InstrumentName='" & cmbcurrinstrument.Text & "' and option_type in ('CE','CA','PE','PA') ", "option_type", DataViewRowState.CurrentRows)
                cmbcurrcp.DataSource = dv.ToTable(True, "option_type")
                cmbcurrcp.DisplayMember = "option_type"
                cmbcurrcp.ValueMember = "option_type"
                cmbcurrcp.Refresh()
                cmbcurrcp.SelectedIndex = 0
                If Not cmbcurrinstrument Is Nothing And cmbcurrinstrument.Items.Count > 0 Then
                    If cmbcurrinstrument.Text <> "" Then
                        If UCase(Mid(cmbcurrinstrument.Text, 1, 3)) = "FUT" Then
                            cmbcurrcp.Enabled = False
                            cmbcurrstrike.Enabled = False
                        Else
                            cmbcurrcp.Enabled = True
                            cmbcurrstrike.Enabled = True
                        End If
                    End If
                End If
            Else
                MsgBox("Select valid instrument name.")
                cmbcurrinstrument.Text = ""
                cmbcurrinstrument.Focus()
            End If
        End If
    End Sub

    Private Sub cmbcurrcp_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcurrcp.Leave
        If cmbcurrcp.Text <> "" Then
            If (UCase(cmbcurrcp.Text) = "CA" Or UCase(cmbcurrcp.Text) = "CE" Or UCase(cmbcurrcp.Text) = "PE" Or UCase(cmbcurrcp.Text) = "PA") Then
                Dim dv As DataView = New DataView(currency, "symbol='" & cmbcurrcomp.Text & "' and InstrumentName='" & cmbcurrinstrument.Text & "' and option_type='" & cmbcurrcp.Text & "' and   option_type in ('CE','CA','PE','PA') ", "strike_price", DataViewRowState.CurrentRows)
                cmbcurrstrike.DataSource = dv.ToTable(True, "strike_price")
                cmbcurrstrike.DisplayMember = "strike_price"
                cmbcurrstrike.ValueMember = "strike_price"
                cmbcurrstrike.Refresh()
                cmbcurrstrike.SelectedIndex = 0
            Else
                MsgBox("Select valid option type.")
                cmbcurrcp.Text = ""
                cmbcurrcp.Focus()
            End If
        End If
    End Sub

    Private Sub cmbcurrstrike_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcurrstrike.Enter
        cmbcurrstrike.Height = 150
        cmbcurrstrike.BringToFront()
    End Sub

    Private Sub cmbcurrstrike_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcurrstrike.Leave
        cmbcurrstrike.Height = cmbh
        If cmbcurrstrike.Text.Trim = "" Then
            cmbcurrstrike.Text = 0
        End If
        If (Mid(cmbcurrcp.Text, 1, 1) = "C" Or Mid(cmbcurrcp.Text, 1, 1) = "P") And cmbcurrstrike.Text <> "0" Then
            Dim dv As DataView = New DataView(currency, "symbol='" & cmbcurrcomp.Text & "' and InstrumentName='" & cmbcurrinstrument.Text & "' and option_type='" & cmbcurrcp.Text & "' and strike_price=" & Val(cmbcurrstrike.Text) & " and  option_type in ('CE','CA','PE','PA')  ", "strike_price", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid strike rate.")
                cmbcurrstrike.Text = ""
                cmbcurrstrike.Focus()
                Exit Sub
            End If
            'dv.ToTable.Columns("mdate").DataType = GetType(Date)
            cmbcurrdate.Items.Clear()
            For Each drow As DataRow In dv.ToTable(True, "expdate").Rows
                cmbcurrdate.Items.Add(Format(CDate(drow("expdate")), "dd/MMM/yyyy"))
            Next
            ' cmbdate.DataSource = dv.ToTable(True, "mdate")
            'cmbdate.DisplayMember = "mdate"
            'cmbdate.ValueMember = "mdate"
            'If cmbcurrdate.Items.Count > 0 Then
            '    tabpage.Refresh()
            '    tabpage.SelectedIndex = 0
            'End If
            'Else
            '    Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "'  ", "strike_price", DataViewRowState.CurrentRows)
            '    cmbdate.DataSource = dv.ToTable(True, "expdate")
            '    cmbdate.DisplayMember = "expdate"
            '    cmbdate.ValueMember = "expdate"
            '    cmbdate.Refresh()
        End If


    End Sub

    Private Sub cmbcurrdate_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcurrdate.Leave
        If cmbcurrcomp.Text.Trim = "" Then
            MsgBox("Enter Security Name.", MsgBoxStyle.Information)
            cmbcurrcomp.Focus()
            Exit Sub
        End If
        If cmbcurrinstrument.Text.Trim = "" Then
            MsgBox("Select Instrument Name.", MsgBoxStyle.Information)
            cmbcurrinstrument.Focus()
            Exit Sub
        End If

        If cmbcurrcp.Text.Trim = "" Then
            MsgBox("Select Call/Put/Futre.", MsgBoxStyle.Information)
            cmbcurrcp.Focus()
            Exit Sub
        End If
        If cmbcurrstrike.Text.Trim = "0" And (Mid(cmbcurrcp.Text, 1, 1) = "C" Or Mid(cmbcurrcp.Text, 1, 1) = "P") Then
            MsgBox("Enter Strike Rate.", MsgBoxStyle.Information)
            cmbcurrstrike.Focus()
            Exit Sub
        End If
        If cmbcurrdate.Text = "" Then
            MsgBox("Select date.", MsgBoxStyle.Information)
            cmbcurrdate.Focus()
            Exit Sub
        End If
        Dim cp As String
        cp = ""

        REM For Add Alert Scriptwise   - For  OPTSTK Remove "CA" And "PA" 
        If Mid(cmbcurrcp.Text, 1, 1) = "C" Or Mid(cmbcurrcp.Text, 1, 1) = "P" Then
            If Mid(cmbcurrcp.Text, 1, 1) = "C" And cmbcurrinstrument.Text.Trim = "OPTCUR" Then
                cp = UCase("CE")
            ElseIf Mid(cmbcurrcp.Text, 1, 1) = "C" And cmbcurrinstrument.Text.Trim = "OPTCUR" Then
                'cp = UCase("CA")
                cp = UCase("CE")
            ElseIf Mid(cmbcurrcp.Text, 1, 1) = "P" And cmbcurrinstrument.Text.Trim = "OPTCUR" Then
                cp = UCase("PE")
            ElseIf Mid(cmbcurrcp.Text, 1, 1) = "P" And cmbcurrinstrument.Text.Trim = "OPTCUR" Then
                'cp = UCase("PA")
                cp = UCase("PE")
            End If
        Else
            cp = ""
        End If
        ''If UCase(cmbcp.Items.ToString) = "CALL" And UCase(cmbinstrument.Items.ToString) = "OPTSTK" Then
        ''    cp = "CA"
        ''ElseIf UCase(cmbcp.Text.ToString) = "CALL" And UCase(cmbinstrument.Text.ToString) = "OPTIDX" Then
        ''    cp = "CE"
        ''ElseIf UCase(cmbcp.Text.ToString) = "PUT" And UCase(cmbinstrument.Text.ToString) = "OPTSTK" Then
        ''    cp = "PA"
        ''ElseIf UCase(cmbcp.Text.ToString) = "PUT" And UCase(cmbinstrument.Text.ToString) = "OPTIDX" Then
        ''    cp = "PE"
        ''Else
        ''    cp = ""
        ''End If

        Dim script As String

        'If cp = "" Then
        '    script = cmbcurrinstrument.SelectedValue & "  " & cmbcurrcomp.SelectedValue & "  " & Format(CDate(cmbcurrdate.Text), "ddMMMyyyy")
        'Else
        '    script = cmbcurrinstrument.SelectedValue & "  " & cmbcurrcomp.SelectedValue & "  " & Format(CDate(cmbcurrdate.Text), "ddMMMyyyy") & "  " & Format(Val(cmbcurrstrike.Text), "###0.0000") & "  " & cp
        'End If

        script = cmbcurrinstrument.Text & "  " & cmbcurrcomp.Text & "  " & Format(CDate(cmbcurrdate.Text), "ddMMMyyyy")
        If cp <> "" Then
            script = script & "  " & Format(Val(cmbcurrstrike.Text), "###0.0000") & "  " & cp
        End If

        txtcurrscript.Text = script
    End Sub
    Private Function Currform_validation() As Boolean
        If rdbcurrcomp.Checked = True Then
            If cmbcurrcompany.SelectedItem.ToString = "" Then
                MsgBox("Select Company Name.", MsgBoxStyle.Information)
                cmbcurrcompany.Focus()
                Currform_validation = False
                Exit Function
            End If
            If cmbcurrcopt.SelectedItem.ToString = "" Then
                MsgBox("Enter Operator.", MsgBoxStyle.Information)
                cmbcurrcopt.Focus()
                Currform_validation = False
                Exit Function
            End If

            If cmbcurrcfield.SelectedItem.ToString = "" Then
                MsgBox("Select Field.", MsgBoxStyle.Information)
                cmbcurrcfield.Focus()
                Currform_validation = False
                Exit Function
            End If
            If txtcurrcsvalue.Text = "0" Then
                MsgBox("Enter Value-1.", MsgBoxStyle.Information)
                txtcurrcsvalue.Focus()
                Currform_validation = False
                Exit Function
            End If
            If cmbcurrcopt.SelectedIndex = 1 Then
                If txtcurrcevalue.Text = "0" Then
                    MsgBox("Enter Value-2.", MsgBoxStyle.Information)
                    txtcurrcevalue.Focus()
                    Currform_validation = False
                    Exit Function
                Else
                    If Val(txtcurrcsvalue.Text) > Val(txtcurrcevalue.Text) Then
                        MsgBox("Enter Value-2 greater than Value-1.", MsgBoxStyle.Information)
                        txtcurrcevalue.Focus()
                        Currform_validation = False
                        Exit Function
                    End If
                End If
            End If
            Currform_validation = True
        Else
            If txtcurrscript.Text.Trim = "" Then
                MsgBox("Select Script.", MsgBoxStyle.Information)
                cmbcurrcomp.Focus()
                Currform_validation = False
                Exit Function
            End If
            If cmbcurrsopt.SelectedItem.ToString = "" Then
                MsgBox("Enter Operator.", MsgBoxStyle.Information)
                cmbcurrsopt.Focus()
                Currform_validation = False
                Exit Function
            End If

            If cmbcurrsfield.SelectedItem.ToString = "" Then
                MsgBox("Select Field.", MsgBoxStyle.Information)
                cmbcurrsfield.Focus()
                Currform_validation = False
                Exit Function
            End If
            If txtcurrssvalue.Text = "0" Then
                MsgBox("Enter Value-1.", MsgBoxStyle.Information)
                txtcurrssvalue.Focus()
                Currform_validation = False
                Exit Function
            End If
            If cmbcurrcopt.SelectedIndex = 1 Then
                If txtcurrsevalue.Text = "0" Then
                    MsgBox("Enter Value-2.", MsgBoxStyle.Information)
                    txtcurrsevalue.Focus()
                    Currform_validation = False
                    Exit Function
                Else
                    If Val(txtcurrssvalue.Text) > Val(txtcurrsevalue.Text) Then
                        MsgBox("Enter Value-2 greater than Value-1.", MsgBoxStyle.Information)
                        txtcurrsevalue.Focus()
                        Currform_validation = False
                        Exit Function
                    End If
                End If
            End If
            Currform_validation = True
        End If
    End Function
    Private Sub cmdcurrsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcurrsave.Click
        If Currform_validation() Then
            Dim chk As Boolean = False
            If rdbcurrcomp.Checked = True Then
                If UCase(cmbcurrcompany.SelectedItem.ToString) = "NA" Or UCase(cmbcurrcompany.SelectedItem.ToString) = "SELECT" Then
                    MsgBox("Select Company.")
                    cmbcurrcompany.Focus()
                    Exit Sub
                End If
                If UCase(cmbcurrcfield.SelectedItem.ToString) = "NA" Or UCase(cmbcurrcfield.SelectedItem.ToString) = "SELECT" Then
                    MsgBox("Select Field.")
                    cmbcurrcfield.Focus()
                    Exit Sub
                End If
                If UCase(cmbcurrcopt.SelectedItem.ToString) = "NA" Or UCase(cmbcurrcopt.SelectedItem.ToString) = "SELECT" Then
                    MsgBox("Select Operator.")
                    cmbcurrcopt.Focus()
                    Exit Sub
                End If
                For Each drow As DataRow In dtable.Select("comp_script='" & cmbcurrcompany.SelectedValue.ToString & "'  and field='" & cmbcurrcfield.SelectedItem & "' and opt='" & cmbcurrcopt.SelectedItem & "'")
                    chk = True
                    Exit For
                Next
                If chk = True And CInt(lblcurruid.Text) = 0 Then
                    MsgBox("Security and field already added.")
                    Exit Sub
                End If
                If chk = False Then
                    objAl.Comp_Script = cmbcurrcompany.SelectedValue.ToString
                    objAl.Opt = cmbcurrcopt.SelectedItem.ToString
                    objAl.Field = cmbcurrcfield.SelectedItem.ToString
                    objAl.Value1 = Val(txtcurrcsvalue.Text)
                    objAl.Value2 = Val(txtcurrcevalue.Text)
                    objAl.Entrydate = Now.Date
                    objAl.Status = 3
                    objAl.Units = 0
                    objAl.Insert()

                Else
                    objAl.Comp_Script = cmbcurrcompany.SelectedItem.ToString
                    objAl.Opt = cmbcurrcopt.SelectedItem.ToString
                    objAl.Field = cmbcurrcfield.SelectedItem.ToString
                    objAl.Value1 = Val(txtcurrcsvalue.Text)
                    objAl.Value2 = Val(txtcurrcevalue.Text)
                    objAl.Entrydate = Now.Date
                    objAl.Status = 3
                    objAl.Units = 0
                    objAl.update(CInt(lblcurruid.Text))
                End If
                Currcompany_clear()
            Else
                If UCase(cmbcurrinstrument.SelectedValue) = "NA" Or UCase(cmbcurrinstrument.SelectedValue) = "SELECT" Then
                    MsgBox("Select Instrument Name.")
                    cmbcurrinstrument.Focus()
                    Exit Sub
                End If
                If UCase(cmbcurrcp.SelectedValue) = "NA" Or UCase(cmbcurrcp.SelectedValue) = "SELECT" Then
                    MsgBox("Select Call/Put.")
                    cmbcurrcp.Focus()
                    Exit Sub
                End If
                If UCase(cmbcurrstrike.SelectedValue) = "NA" Or UCase(cmbcurrstrike.SelectedValue) = "SELECT" Then
                    MsgBox("Select Strike Price.")
                    cmbcurrstrike.Focus()
                    Exit Sub
                End If
                If UCase(cmbcurrdate.SelectedItem.ToString) = "NA" Or UCase(cmbcurrdate.SelectedItem.ToString) = "SELECT" Then
                    MsgBox("Select Expiry.")
                    cmbcurrdate.Focus()
                    Exit Sub
                End If
                If UCase(cmbcurrsfield.SelectedItem.ToString) = "NA" Or UCase(cmbcurrsfield.SelectedItem.ToString) = "SELECT" Then
                    MsgBox("Select Field.")
                    cmbcurrsfield.Focus()
                    Exit Sub
                End If
                If UCase(cmbcurrsopt.SelectedItem.ToString) = "NA" Or UCase(cmbcurrsopt.SelectedItem.ToString) = "SELECT" Then
                    MsgBox("Select Operator.")
                    cmbcurrsopt.Focus()
                    Exit Sub
                End If
                'If txtunits.Text.Trim = "" Then
                '    txtunits.Text = 0
                'End If
                'If Not IsNumeric(txtunits.Text) Then
                '    MsgBox("Enter Units", MsgBoxStyle.Information)
                '    txtunits.Focus()
                '    Exit Sub
                'End If
                'If txtunits.Text = "0" Then
                '    MsgBox("Enter Units", MsgBoxStyle.Information)
                '    txtunits.Focus()
                '    Exit Sub
                'End If
                For Each drow As DataRow In dtable.Select("comp_script='" & txtcurrscript.Text & "' and field='" & cmbcurrsfield.SelectedItem & "' and opt='" & cmbcurrsopt.SelectedItem & "'")
                    chk = True
                    Exit For
                Next
                Dim tk1 As Long = CLng(IIf(IsDBNull(currency.Compute("max(token)", "script='" & txtcurrscript.Text & "'")), 0, currency.Compute("max(token)", "script='" & txtcurrscript.Text & "'")))

                If tk1 = 0 Then
                    MsgBox(txtcurrscript.Text & " does not exist in contract.")
                    chk = True
                End If
                If chk = False Then
                    objAl.Comp_Script = txtcurrscript.Text
                    objAl.Opt = cmbcurrsopt.SelectedItem.ToString
                    objAl.Field = cmbcurrsfield.SelectedItem.ToString
                    objAl.Value1 = Val(txtcurrssvalue.Text)
                    objAl.Value2 = Val(txtcurrsevalue.Text)
                    objAl.Entrydate = Now.Date
                    objAl.Units = Val(txtcurrunits.Text)
                    objAl.Status = 4
                    objAl.Insert()
                Else
                    objAl.Comp_Script = txtcurrscript.Text
                    objAl.Opt = cmbcurrsopt.SelectedItem.ToString
                    objAl.Field = cmbcurrsfield.SelectedItem.ToString
                    objAl.Value1 = Val(txtcurrssvalue.Text)
                    objAl.Value2 = Val(txtcurrsevalue.Text)
                    objAl.Entrydate = Now.Date
                    objAl.Units = Val(txtcurrunits.Text)
                    objAl.Status = 4
                    objAl.update(CInt(lblcurruid.Text))
                End If
                Currscript_clear()
            End If
            fill_Curr_data()
        End If
    End Sub

    Private Sub cmdcurrclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcurrclear.Click
        If rdbcurrcomp.Checked = True Then
            Currcompany_clear()
        Else
            Currscript_clear()
        End If
    End Sub

    Private Sub cmdcurrexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcurrexit.Click
        Me.Close()
    End Sub

    Private Sub cmbcurrdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcurrdelete.Click
        grdalert.EndEdit()
        'For Each grow As DataGridViewRow In grdalert.Rows
        '    MsgBox(grow.Cells(0).Value)
        'Next
        objAl.delete(dtable, grdalert)
        fill_Curr_data()
    End Sub
    Private Sub cmbcurrstrike_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbstrike.KeyPress
        numonly(e)
    End Sub

    Private Sub txtcurrcsvalue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcsvalue.KeyPress
        numonly(e)
    End Sub

    Private Sub txtcurrcevalue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcevalue.KeyPress
        numonly(e)
    End Sub

    Private Sub txtcurrssvalue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtssvalue.KeyPress
        numonly(e)
    End Sub

    Private Sub txtcurrsevalue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsevalue.KeyPress
        numonly(e)
    End Sub

    Private Sub cmbcurrsopt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcurrsopt.SelectedIndexChanged
        txtcurrsevalue.Text = "0"
        txtcurrssvalue.Text = "0"
        If cmbcurrsopt.SelectedIndex = 1 Then
            lblcurrsvalue2.Visible = True
            '  lblsval2.Visible = True
            txtcurrsevalue.Visible = True
            lblcurrsvalue1.Text = "Value-1"
        Else

            lblcurrsvalue2.Visible = False
            ' lblsval2.Visible = False
            txtcurrsevalue.Visible = False
            lblcurrsvalue1.Text = "Value"
        End If

    End Sub

  

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        For i As Integer = 0 To grdalert.RowCount - 1
            If grdalert.Rows(i).Cells(0).Value = True Then
                objAl.update(Val(grdalert.Rows(i).Cells(1).Value), Today.Date)
                For Each drow As DataRow In alerttable.Select("comp_script='" & grdalert.Rows(i).Cells(2).Value & "' and field ='" & grdalert.Rows(i).Cells(4).Value & "' and uid='" & grdalert.Rows(i).Cells(1).Value & "'")
                    drow("entrydate") = Today.Date
                    drow("status") = "0"
                Next
            End If
        Next
        fill_data()

        MsgBox("Alert Updated Successfully.")
    End Sub
End Class