Imports System
Imports System.IO

Public Class addprevious_datetrading
    Dim objTrad As trading = New trading
    Dim objExp As New expenses
    Dim temptable As New DataTable
    Dim entrydate As String
    Dim entryno As String
    Dim eqentrydate As String
    Dim eqentryno As String
    Dim tedtable As DataTable
    Dim tptable As DataTable
    Dim tempdate As Date
    Dim tempdate1 As Date
    Dim scripttable As New DataTable
    Dim eqtable As New DataTable
    Dim objAna As New analysisprocess
    Dim entarr As New ArrayList

    Dim DTTMPPrevFOImportDate As New DataTable
    Dim DTTMPPrevEQImportDate As New DataTable
    Dim DTTMPPrevCurrencyDate As New DataTable
    Dim ImportStatus As Boolean = False

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdfobr.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.txt)|*.txt"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtpath.Text = opfile.FileName
        End If
    End Sub

    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        'If txtpath.Text.Trim <> "" Then
		'Rem  Object Define For New Method of Importing Trade By Viral
        Dim ObjImpData As New import_Data
        Dim ObjIO As New ImportData.ImportOperation
        'Rem Set DealerCode in Global Variable B'Code of it is used When Import trade in New Method By Viral
        GVar_DealerCode = ""
        If txtFoDealer.Text.Contains(",") Then
            Dim StrArrNeatClient() As String = txtFoDealer.Text.Split(",")
            For Each Str As String In StrArrNeatClient
                If Str Is Nothing OrElse Str = "" Then Continue For
                If GVar_DealerCode <> "" Then
                    GVar_DealerCode &= ","
                End If
                GVar_DealerCode &= "'" & Str & "'"
            Next
        Else
            GVar_DealerCode = "'" & txtFoDealer.Text.ToString() & "'"
        End If
       
        
        '   GVar_DealerCode = txtFoDealer.Text

        Call cmdeqclear_Click(sender, e)
        Call cmdCurrclear_Click(sender, e)
        If Validation() = True Then
            If File.Exists(txtpath.Text) Then
                Me.Cursor = Cursors.WaitCursor
                DTTMPPrevFOImportDate = New DataTable
                DTTMPPrevEQImportDate = New DataTable
                DTTMPPrevCurrencyDate = New DataTable
                Dim objTradPrev As New trading
                ' process_file()
                Select Case cmbFO.Text.Trim
                    Case "NEAT"
                        'ImportStatus = proc_data_FromNeatFOTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtFoDealer.Text, "", txtpath.Text)
                        ImportStatus = ObjImpData.FromNeaFOTEXT(False, DTTMPPrevFOImportDate, "", txtpath.Text, ObjIO)
                    Case "NOTIS"
                        'ImportStatus = proc_data_FromNotisFOTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtFoDealer.Text, "", txtpath.Text)
                        ImportStatus = ObjImpData.FromNotisFOTEXT(False, DTTMPPrevFOImportDate, "", txtpath.Text, ObjIO)
                    Case "NOW"
                        'ImportStatus = proc_data_FromNowFOTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtFoDealer.Text, "", txtpath.Text)
                        ImportStatus = ObjImpData.FromNowFOTEXT(False, DTTMPPrevFOImportDate, "", txtpath.Text, ObjIO)
                    Case "ODIN"
                        'ImportStatus = proc_data_FromODINFOTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtFoDealer.Text, "", txtpath.Text)
                        ImportStatus = ObjImpData.FromOdinFOTEXT(False, DTTMPPrevFOImportDate, "", txtpath.Text, ObjIO)
                    Case "FIST"
                        ImportStatus = ObjImpData.FromFistFOTEXT(False, DTTMPPrevFOImportDate, "", txtpath.Text, ObjIO)
                    Case "FIST ADMIN"
                        ImportStatus = ObjImpData.FromFadmFOTEXT(False, DTTMPPrevFOImportDate, "", txtpath.Text, ObjIO)
                    Case "GETS"
                        'ImportStatus = proc_data_FromGetsFOTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtFoDealer.Text, "", txtpath.Text)
                        ImportStatus = ObjImpData.FromGetsFOTEXT(False, DTTMPPrevFOImportDate, "", txtpath.Text, ObjIO)
                    Case "NSE"
                        'ImportStatus = proc_data_FromNSEFOTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtFoDealer.Text, "", txtpath.Text)
                        ImportStatus = ObjImpData.FromNseFOTEXT(False, DTTMPPrevFOImportDate, "", txtpath.Text, ObjIO)
                End Select 
               'Rem ReSet Dealer From Setting Table
                GetDealerCode()

                ObjImpData = Nothing
                ObjIO = Nothing

                If ImportStatus = False And DTTMPPrevFOImportDate.Rows.Count = 0 Then
                    MsgBox("File already imported.", MsgBoxStyle.Exclamation)
                Else
                    VarFileImport = True
                    comptable = objTrad.Comapany
                    comptable_Net = objTrad.Comapany_Net
                    Call GSub_CalculateExpense(DTTMPPrevFOImportDate, "FO", True)
                    objTrad.Delete_Expense_Data_All()
                    objTrad.Insert_Expense_Data(G_DTExpenseData)
                    'Dim VarDTDate As Date = DTTMPPrevFOImportDate.Rows(0)("entrydate")
                    'objTrad.Delete_prBal(VarDTDate.Date)
                    'Call cal_prebal(VarDTDate.Date)
                    'prebal = objTrad.prebal
                    MsgBox("F&O Trade File Processed Successfully.", MsgBoxStyle.Information)
                    ImportStatus = False
                End If
                'Call GSub_Fill_GDt_AllTrades()
                'Call GSub_Fill_GDt_Strategy()
                Me.Cursor = Cursors.Default
            End If
        End If
    End Sub
    'Rem To Get Dealercode From Setting And ReSet To Global Variable  By Viral
    'Rem It is used : After  Importing Manualy Import previous Trade  By Viral 
    Private Sub GetDealerCode()
        GVar_DealerCode = GdtSettings.Compute("max(SettingKey)", "SettingName='Nse_ccode'").ToString
        Dim StrArrNeatClient() As String = GVar_DealerCode.Split(",")
        GVar_DealerCode = ""
        For Each Str As String In StrArrNeatClient
            If Str Is Nothing OrElse Str = "" Then Continue For
            If GVar_DealerCode <> "" Then
                GVar_DealerCode &= ","
            End If
            GVar_DealerCode &= "'" & Str & "'"
        Next
    End Sub
    Private Sub addprevious_datetrading_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub

    Private Sub addprevious_datetrading_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objTrad.Trading(temptable)
        eqtable = New DataTable
        eqtable = eqmaster
        tptable = New DataTable
        tptable = objTrad.select_equity
        scripttable = New DataTable
        scripttable = cpfmaster
        Me.WindowState = FormWindowState.Normal
        Me.Refresh()
    End Sub
    'Public Sub cal_prebal(ByVal date1 As Date)
    '    Dim addprebal As New DataTable
    '    addprebal = New DataTable
    '    With addprebal.Columns
    '        .Add("tdate", GetType(Date))
    '        .Add("stbal", GetType(Double))
    '        .Add("futbal", GetType(Double))
    '        .Add("optbal", GetType(Double))
    '        .Add("company", GetType(String))
    '    End With
    '    Dim prow As DataRow
    '    Dim cpf As New DataTable
    '    Dim stk As New DataTable
    '    Dim currtrd As New DataTable

    '    Dim exptable As New DataTable
    '    Dim company As New DataTable

    '    exptable = objExp.Select_Expenses
    '    company = objTrad.Comapany

    '    cpf = objTrad.Trading
    '    stk = objTrad.select_equity
    '    currtrd = objTrad.select_Currency_Trading

    '    Dim dv As DataView = New DataView(cpf)
    '    dv.RowFilter = "entrydate >= #" & date1.Date & "# AND entrydate < #" & date1.Date.AddDays(1) & "#"
    '    Dim dv1 As DataView = New DataView(stk)
    '    dv1.RowFilter = "entrydate >= #" & date1.Date & "# AND entrydate < #" & date1.Date.AddDays(1) & "#"
    '    Dim dv2 As DataView = New DataView(currtrd)
    '    dv2.RowFilter = "entrydate >= #" & date1.Date & "# AND entrydate < #" & date1.Date.AddDays(1) & "#"

    '    Dim stexp, stexp1, ndst, dst, exppr, expto As Double

    '    For Each crow As DataRow In company.Rows
    '        dv.RowFilter = " entry_date = #" & date1.Date & "# and company='" & crow("company") & "'"
    '        dv.Sort = "entry_date"
    '        Dim ttable As New DataTable
    '        ttable = dv.ToTable(True, "entry_date")
    '        If ttable.Rows.Count <= 0 Then
    '            dv1.RowFilter = "entry_date = #" & date1.Date & "# and company='" & crow("company") & "'"
    '            dv1.Sort = "entry_date"
    '            ttable = dv1.ToTable(True, "entry_date")
    '        End If

    '        ''for currency
    '        If ttable.Rows.Count <= 0 Then
    '            dv2.RowFilter = "entry_date = #" & date1.Date & "# and company='" & crow("company") & "'"
    '            dv2.Sort = "entry_date"
    '            ttable = dv2.ToTable(True, "entry_date")
    '        End If

    '        For Each row As DataRow In ttable.Rows
    '            prow = addprebal.NewRow
    '            prow("tdate") = CDate(row("entry_date")).Date
    '            prow("stbal") = 0
    '            prow("futbal") = 0
    '            prow("optbal") = 0
    '            prow("company") = crow("company")
    '            addprebal.Rows.Add(prow)
    '            stexp = 0
    '            stexp1 = 0
    '            dst = 0
    '            ndst = 0
    '            exppr = 0
    '            expto = 0

    '            'Equity ##################################################################
    '            stexp = Math.Round(Val(IIf(Not IsDBNull(stk.Compute("sum(tot)", "company='" & crow("company") & "' and tot > 0 and (entrydate >= #" & CDate(row("entry_date")).Date & "# AND entrydate < #" & CDate(row("entry_date")).Date.AddDays(1) & "#)")), stk.Compute("sum(tot)", "company='" & crow("company") & "' and tot > 0 and entry_date = #" & CDate(row("entry_date")).Date & "#"), 0)), 2)
    '            stexp1 = Math.Abs(Math.Round(Val(IIf(Not IsDBNull(stk.Compute("sum(tot)", "company='" & crow("company") & "' and tot < 0 and (entrydate >= #" & CDate(row("entry_date")).Date & "# AND entrydate < #" & CDate(row("entry_date")).Date.AddDays(1) & "#)")), stk.Compute("sum(tot)", "company='" & crow("company") & "' and tot < 0 and entry_date = #" & CDate(row("entry_date")).Date & "#"), 0)), 2))
    '            dst = stexp - stexp1
    '            If dst > 0 Then
    '                ndst = stexp1
    '                prow("stbal") = Val(prow("stbal")) + Val((dst * dbl) / dblp) + Val((ndst * ndbs) / ndbsp) + Val((ndst * ndbl) / ndblp)
    '            Else
    '                ndst = stexp
    '                dst = -dst
    '                prow("stbal") = Val(prow("stbal")) + Val((dst * dbs) / dbsp) + Val((ndst * ndbs) / ndbsp) + Val((ndst * ndbl) / ndblp)
    '            End If

    '            'Futre #################################################################
    '            stexp = 0
    '            stexp1 = 0
    '            stexp = Val(cpf.Compute("sum(tot)", "cp not in ('C','P') and company='" & crow("company") & "' AND tot > 0 and (entrydate >= #" & CDate(row("entry_date")).Date & "# AND entrydate < #" & CDate(row("entry_date")).Date.AddDays(1) & "#)").ToString)
    '            stexp1 = Math.Abs(Val(cpf.Compute("sum(tot)", "cp not in ('C','P') and company='" & crow("company") & "' and tot < 0 and entry_date = #" & CDate(row("entry_date")).Date & "#").ToString))
    '            prow("futbal") = Val(prow("futbal")) + Val((stexp * exptable.Compute("max(futl)", "")) / exptable.Compute("max(futlp)", "")) + Val((stexp1 * exptable.Compute("max(futs)", "")) / exptable.Compute("max(futsp)", ""))
    '            'Option ####################################################################
    '            stexp = 0
    '            stexp1 = 0
    '            stexp = Val(cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & crow("company") & "' and tot > 0 and entry_date = #" & CDate(row("entry_date")).Date & "#").ToString)
    '            stexp1 = Math.Abs(Val(cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & crow("company") & "' and tot < 0 and entry_date = #" & CDate(row("entry_date")).Date & "#").ToString))
    '            If Val(exptable.Compute("max(spl)", "")) <> 0 Then
    '                prow("optbal") = Val(prow("optbal")) + Val((stexp * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", "")) + Val((stexp1 * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", ""))
    '            Else
    '                prow("optbal") = Val(prow("optbal")) + Val((stexp * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", "")) + Val((stexp1 * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", ""))
    '            End If

    '            'Currency Futre #################################################################
    '            stexp = 0
    '            stexp1 = 0
    '            stexp = Val(currtrd.Compute("sum(tot)", "cp not in ('C','P') and company='" & crow("company") & "' AND tot > 0 and (entrydate >= #" & CDate(row("entry_date")).Date & "# AND entrydate < #" & CDate(row("entry_date")).Date.AddDays(1) & "#)").ToString)
    '            stexp1 = Math.Abs(Val(currtrd.Compute("sum(tot)", "cp not in ('C','P') and company='" & crow("company") & "' and tot < 0 and (entrydate >= #" & CDate(row("entry_date")).Date & "# AND entrydate < #" & CDate(row("entry_date")).Date.AddDays(1) & "#)").ToString))
    '            prow("futbal") = Val(prow("futbal")) + Val((stexp * exptable.Compute("max(futl)", "")) / exptable.Compute("max(currfutlp)", "")) + Val((stexp1 * exptable.Compute("max(currfuts)", "")) / exptable.Compute("max(currfutsp)", ""))
    '            'Currency Option ####################################################################
    '            stexp = 0
    '            stexp1 = 0
    '            stexp = Val(currtrd.Compute("sum(tot)", "cp  in ('C','P') and company='" & crow("company") & "' and tot > 0 and (entrydate >= #" & CDate(row("entry_date")).Date & "# AND entrydate < #" & CDate(row("entry_date")).Date.AddDays(1) & "#)").ToString)
    '            stexp1 = Math.Abs(Val(currtrd.Compute("sum(tot)", "cp  in ('C','P') and company='" & crow("company") & "' and tot< 0 and (entrydate >= #" & CDate(row("entry_date")).Date & "# AND entrydate < #" & CDate(row("entry_date")).Date.AddDays(1) & "#)").ToString))
    '            If Val(exptable.Compute("max(spl)", "")) <> 0 Then
    '                prow("optbal") = Val(prow("optbal")) + Val((stexp * exptable.Compute("max(currspl)", "")) / exptable.Compute("max(currsplp)", "")) + Val((stexp1 * exptable.Compute("max(currsps)", "")) / exptable.Compute("max(currspsp)", ""))
    '            Else
    '                prow("optbal") = Val(prow("optbal")) + Val((stexp * exptable.Compute("max(currprel)", "")) / exptable.Compute("max(currprelp)", "")) + Val((stexp1 * exptable.Compute("max(currpres)", "")) / exptable.Compute("max(currpresp)", ""))
    '            End If

    '        Next
    '    Next
    '    objTrad.insert_prebal(addprebal)
    'End Sub

    Private Sub cmdeqsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqsave.Click
        'Rem  Object Define For New Method of Importing Trade By Viral
        Dim ObjImpData As New import_Data
        Dim ObjIO As New ImportData.ImportOperation
        'Rem Set DealerCode in Global Variable B'Code of it is used When Import trade in New Method By Viral
        'GVar_DealerCode = txtEqDealer.Text
        GVar_DealerCode = ""
        If txtEqDealer.Text.Contains(",") Then
            Dim StrArrNeatClient() As String = txtEqDealer.Text.Split(",")
            For Each Str As String In StrArrNeatClient
                If Str Is Nothing OrElse Str = "" Then Continue For
                If GVar_DealerCode <> "" Then
                    GVar_DealerCode &= ","
                End If
                GVar_DealerCode &= "'" & Str & "'"
            Next
        Else
            GVar_DealerCode = "'" & txtEqDealer.Text.ToString() & "'"
        End If

        'If txteqpath.Text.Trim <> "" Then
        Call cmdclear_Click(sender, e)
        Call cmdCurrclear_Click(sender, e)
        If Validation() = True Then
            If File.Exists(txteqpath.Text) Then
                Me.Cursor = Cursors.WaitCursor
                DTTMPPrevFOImportDate = New DataTable
                DTTMPPrevEQImportDate = New DataTable
                Dim objTradPrev As New trading
                Select Case cmbEquity.Text
                    Case "GETS"
                        'ImportStatus = proc_data_FromGETSEqTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtEqDealer.Text, "", txteqpath.Text)
                        ImportStatus = ObjImpData.FromGetsEQTEXT(False, DTTMPPrevEQImportDate, "", txteqpath.Text, ObjIO)
                    Case "ODIN"
                        'ImportStatus = proc_data_FromODINEqTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtEqDealer.Text, "", txteqpath.Text)
                        ImportStatus = ObjImpData.FromOdinEQTEXT(False, DTTMPPrevEQImportDate, "", txteqpath.Text, ObjIO)
                    Case "NOW"
                        'ImportStatus = proc_data_FromNowEQTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtEqDealer.Text, "", txteqpath.Text)
                        ImportStatus = ObjImpData.FromNowEQTEXT(False, DTTMPPrevEQImportDate, "", txteqpath.Text, ObjIO)
                    Case "NOTIS"
                        'ImportStatus = proc_data_FromNotisEQTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtEqDealer.Text, "", txteqpath.Text)
                        ImportStatus = ObjImpData.FromNotisEQTEXT(False, DTTMPPrevEQImportDate, "", txteqpath.Text, ObjIO)
                    Case "NSE"
                        ImportStatus = proc_data_FromNSEEqTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtEqDealer.Text, "", txteqpath.Text)
                End Select
                'Rem ReSet Dealer From Setting Table
                GetDealerCode()

                ObjImpData = Nothing
                ObjIO = Nothing
                If ImportStatus = False And DTTMPPrevEQImportDate.Rows.Count = 0 Then
                    MsgBox("File already imported.", MsgBoxStyle.Exclamation)
                    'Exit Sub
                Else
                    VarFileImport = True
                    comptable = objTrad.Comapany
                    comptable_Net = objTrad.Comapany_Net
                    Call GSub_CalculateExpense(DTTMPPrevEQImportDate, "EQ", True)
                    objTrad.Delete_Expense_Data_All()
                    objTrad.Insert_Expense_Data(G_DTExpenseData)
                    'Dim VarDTDate As Date = DTTMPPrevEQImportDate.Rows(0)("entrydate")
                    'objTrad.Delete_prBal(VarDTDate.Date)
                    'Call cal_prebal(VarDTDate.Date)
                    'prebal = objTrad.prebal
                    MsgBox("Equity Trade File Processed Successfully.", MsgBoxStyle.Information)
                    ImportStatus = False
                End If
                Me.Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdproc.Click
        'objTrad.Delete_prBal(dtproc.Value.Date)
        'cal_prebal()
        'MsgBox("Previous Balance Process Successfully")

    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdeqclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqclear.Click
        txteqpath.Text = ""
        txtEqDealer.Text = ""
        cmbEquity.Text = ""
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        txtpath.Text = ""
        txtFoDealer.Text = ""
        cmbFO.Text = ""
    End Sub

    Private Sub cmdeqbr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqbr.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.txt)|*.txt"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txteqpath.Text = opfile.FileName
        End If
    End Sub

    Private Sub dtproc_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub addprevious_datetrading_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            'ElseIf e.KeyCode = Keys.F11 Then
            '    If txtpath.Text.Trim <> "" Then
            '        If File.Exists(txtpath.Text) Then
            '            process_file()

            '        End If
            '    End If
            'ElseIf e.KeyCode = Keys.F12 Then
            '    If txteqpath.Text.Trim <> "" Then
            '        If File.Exists(txteqpath.Text) Then
            '            equity_process()

            '        End If
            '    End If
        End If
    End Sub
    Private Sub cmdCurrbr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCurrbr.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.txt)|*.txt"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCurrpath.Text = opfile.FileName
        End If
    End Sub
    Private Sub cmdCurrsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCurrsave.Click
        'Rem  Object Define For New Method of Importing Trade By Viral  
        Dim ObjImpData As New import_Data
        Dim ObjIO As New ImportData.ImportOperation
        'Rem Set DealerCode in Global Variable B'Code of it is used When Import trade in New Method By Viral
        '  GVar_DealerCode = txtCurrDealer.Text
        GVar_DealerCode = ""
        If txtCurrDealer.Text.Contains(",") Then
            Dim StrArrNeatClient() As String = txtCurrDealer.Text.Split(",")
            For Each Str As String In StrArrNeatClient
                If Str Is Nothing OrElse Str = "" Then Continue For
                If GVar_DealerCode <> "" Then
                    GVar_DealerCode &= ","
                End If
                GVar_DealerCode &= "'" & Str & "'"
            Next
        Else
            GVar_DealerCode = "'" & txtCurrDealer.Text.ToString() & "'"
        End If
        'If txtCurrpath.Text.Trim <> "" Then
        Call cmdeqclear_Click(sender, e)
        Call cmdclear_Click(sender, e)
        If Validation() = True Then
            If File.Exists(txtCurrpath.Text) Then
                Me.Cursor = Cursors.WaitCursor
                ' process_file()
                DTTMPPrevFOImportDate = New DataTable
                DTTMPPrevEQImportDate = New DataTable
                Dim objTradPrev As New trading
                Select Case cmbCurr.Text.Trim
                    Case "NEAT"
                        'ImportStatus = proc_data_FromNeatCurrTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtCurrDealer.Text, "", txtCurrpath.Text)
                        ImportStatus = ObjImpData.FromNeaCurrTEXT(False, DTTMPPrevCurrencyDate, "", txtCurrpath.Text, ObjIO)
                    Case "NOTIS"
                        'ImportStatus = proc_data_FromNotisCurrTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtCurrDealer.Text, "", txtCurrpath.Text)
                        ImportStatus = ObjImpData.FromNotisCurrTEXT(False, DTTMPPrevCurrencyDate, "", txtCurrpath.Text, ObjIO)
                    Case "NOW"
                        'ImportStatus = proc_data_FromNowCurrTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtCurrDealer.Text, "", txtCurrpath.Text)
                        ImportStatus = ObjImpData.FromNowCurrTEXT(False, DTTMPPrevCurrencyDate, "", txtCurrpath.Text, ObjIO)
                    Case "ODIN"
                        'ImportStatus = proc_data_FromODINCurrTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtCurrDealer.Text, "", txtCurrpath.Text)
                        ImportStatus = ObjImpData.FromOdinCurrTEXT(False, DTTMPPrevCurrencyDate, "", txtCurrpath.Text, ObjIO)
                    Case "MCX-ODIN"
                        'ImportStatus = proc_data_FromODINCurrTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtCurrDealer.Text, "", txtCurrpath.Text)
                        ImportStatus = ObjImpData.FromMCXOdinCurrTEXT(False, DTTMPPrevCurrencyDate, "", txtCurrpath.Text, ObjIO)
                    Case "GETS"
                        'ImportStatus = proc_data_FromGetsCurrTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtCurrDealer.Text, "", txtCurrpath.Text)
                        ImportStatus = ObjImpData.FromGetsCurrTEXT(False, DTTMPPrevCurrencyDate, "", txtCurrpath.Text, ObjIO)
                    Case "NSE"
                        'ImportStatus = proc_data_FromNSECurrTEXT(False, DTTMPPrevFOImportDate, DTTMPPrevEQImportDate, DTTMPPrevCurrencyDate, objTradPrev, txtCurrDealer.Text, "", txtCurrpath.Text)
                        ImportStatus = ObjImpData.FromNseCurrTEXT(False, DTTMPPrevCurrencyDate, "", txtCurrpath.Text, ObjIO)
                End Select
                'Rem ReSet Dealer From Setting Table
                GetDealerCode()

                ObjImpData = Nothing
                ObjIO = Nothing
                If ImportStatus = False And DTTMPPrevCurrencyDate.Rows.Count = 0 Then
                    MsgBox("File already imported.", MsgBoxStyle.Exclamation)
                    'Exit Sub
                Else
                    VarFileImport = True
                    comptable = objTrad.Comapany
                    comptable_Net = objTrad.Comapany_Net
                    Call GSub_CalculateExpense(DTTMPPrevCurrencyDate, "CURR", True)
                    objTrad.Delete_Expense_Data_All()
                    objTrad.Insert_Expense_Data(G_DTExpenseData)
                    'Dim VarDTDate As Date = DTTMPPrevCurrencyDate.Rows(0)("entrydate")
                    'objTrad.Delete_prBal(VarDTDate.Date)
                    'Call cal_prebal(VarDTDate.Date)
                    'prebal = objTrad.prebal
                    MsgBox("Currency Trade File Processed Successfully.", MsgBoxStyle.Information)
                    ImportStatus = False
                End If

                'If ImportStatus = True Then
                '    VarFileImport = True
                '    MsgBox("Currency Trading File Process Successfully", MsgBoxStyle.Information)
                '    ImportStatus = False
                'Else
                '    MsgBox("File already imported", MsgBoxStyle.Exclamation)
                'End If
                'Call GSub_Fill_GDt_AllTrades()
                ' Call GSub_Fill_GDt_Strategy()
                Me.Cursor = Cursors.Default
            End If
        End If
    End Sub
    Private Function Validation() As Boolean
        If cmbFO.Text = "" And cmbCurr.Text = "" And cmbEquity.Text = "" Then
            MsgBox("Please Select File Type!")
            Return False
            Exit Function
        End If
        If cmbFO.Text = "NOTIS" Or cmbFO.Text = "NEAT" Or cmbEquity.Text = "NOTIS" Or cmbCurr.Text = "NEAT" Or cmbCurr.Text = "NOTIS" Then
            If txtEqDealer.Text = "" And txtFoDealer.Text = "" And txtCurrDealer.Text = "" Then
                MsgBox("Please Insert Dealer Code!")
                Return False
                Exit Function
            End If
        End If
        If txteqpath.Text = "" And txtpath.Text = "" And txtCurrpath.Text = "" Then
            MsgBox("Please Select File Path!")
            Return False
            Exit Function
        End If
        Return True
    End Function

    Private Sub cmdCurrclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCurrclear.Click
        txtCurrpath.Text = ""
        txtCurrDealer.Text = ""
        cmbCurr.Text = ""
    End Sub
#Region "import trade file:"
    'Private Function GSub_ImporttxtFileToTrading(ByVal DTData As DataTable, ByVal VarFileFlag As String, ByVal VarDealerPrefix As String, ByVal Var As String) As Boolean
    '    Dim VarTokenNo As Integer
    '    DTData.Columns.Add("Token1")
    '    DTData.Columns.Add("IsLiq")
    '    DTData.Columns.Add("lActivityTime")
    '    Dim status As Boolean
    '    If Var = "FO" Then
    '        Dim DTTrade As New DataTable
    '        REM 1: create datatable for future and option
    '        With DTTrade.Columns
    '            .Add("entryno", GetType(Integer))
    '            .Add("instrumentname")
    '            .Add("company")
    '            .Add("mdate", GetType(Date))
    '            .Add("strikerate", GetType(Double))
    '            .Add("cp")
    '            .Add("qty", GetType(Double))
    '            .Add("rate", GetType(Double))
    '            .Add("tot", GetType(Double))
    '            .Add("entrydate", GetType(Date))
    '            .Add("script")
    '            .Add("token1", GetType(Long))
    '            .Add("isliq")
    '            .Add("orderno", GetType(Long))
    '            .Add("lActivityTime", GetType(Long))
    '            '   .Add("dealer", GetType(String))
    '            .Add("FileFlag")
    '        End With
    '        REM 1: END
    '        Dim tempdate As Date
    '        tempdate = CDate(DTData.Rows(0)("EntryDate").ToString)
    '        For Each DR As DataRow In DTData.Rows 'Select("Dealer = '" & dealer & "'")
    '            If analysis.hashOrder.ContainsKey(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim) = True Then
    '                Continue For
    '            Else
    '                analysis.hashOrder.Add(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim, "1")
    '            End If

    '            DR("lActivityTime") = DateDiff(DateInterval.Second, CDate("1-1-1980"), CDate(DR("EntryDate")))
    '            If DR("instrumentname") = "FUTSTK" Or DR("instrumentname") = "FUTIDX" Then
    '                DR("CP") = "F"
    '            End If
    '            If DR("CP").ToString.Length > 0 Then
    '                DR("CP") = Mid(DR("CP"), 1, 1)
    '            End If
    '            If DR("CP") = "C" Or DR("CP") = "P" Then
    '                If DR("instrumentname") = "OPTIDX" Then
    '                    DR("Script") = DR("instrumentname") & Space(2) & DR("Company") & Space(2) & Format(CDate(DR("MDate")), "ddMMMyyyy") & Space(2) & DR("StrikeRate") & Space(2) + DR("CP") & "E"
    '                Else
    '                    DR("Script") = DR("instrumentname") & Space(2) & DR("Company") & Space(2) & Format(CDate(DR("MDate")), "ddMMMyyyy") & Space(2) & DR("StrikeRate") & Space(2) + DR("CP") & "A"
    '                End If
    '            ElseIf DR("CP") = "F" Then
    '                DR("Script") = DR("instrumentname") & Space(2) & DR("Company") & Space(2) & Format(CDate(DR("MDate")), "ddMMMyyyy")
    '            End If
    '            'DR("Dealer") = VarDealerPrefix & DR("Dealer")
    '            VarTokenNo = Val(scripttable.Compute("MAX(Token)", "Script='" & DR("Script") & "'").ToString)
    '            If VarTokenNo > 0 And (DR("CP") = "F" Or DR("CP") = "P" Or DR("CP") = "C") Then
    '                DR("Token1") = VarTokenNo
    '                REM 3.1: Process for future and option
    '                Dim DrTr As DataRow = DTTrade.NewRow
    '                DrTr("Instrumentname") = DR("Instrumentname")
    '                DrTr("Company") = DR("Company")
    '                DrTr("MDate") = DR("MDate")
    '                DrTr("StrikeRate") = Val(DR("StrikeRate").ToString)
    '                DrTr("CP") = DR("CP")
    '                DrTr("Script") = DR("Script")
    '                DrTr("Rate") = DR("Rate")
    '                If CInt(DR("buysell")) = 1 Then
    '                    DrTr("Qty") = DR("Qty")
    '                    DrTr("tot") = Val(DR("Qty")) * Val(DR("Rate"))
    '                Else
    '                    DrTr("Qty") = -DR("Qty")
    '                    DrTr("tot") = -(Val(DR("Qty")) * Val(DR("Rate")))
    '                End If


    '                DrTr("EntryDate") = DR("EntryDate")
    '                DrTr("EntryNo") = Val(DR("EntryNo").ToString)
    '                If DR("CP") = "F" Then
    '                    DrTr("Token1") = 0
    '                Else
    '                    Dim a, a1, script1 As String
    '                    a = Mid(DR("Script").ToString, Len(DR("Script").ToString) - 1, 1)
    '                    a1 = Mid(DR("Script").ToString, Len(DR("Script").ToString), 1)
    '                    If a = "C" Then
    '                        script1 = Mid(DR("Script").ToString, 1, Len(DR("Script").ToString) - 2) & "P" & a1
    '                    Else
    '                        script1 = Mid(DR("Script").ToString, 1, Len(DR("Script").ToString) - 2) & "C" & a1
    '                    End If
    '                    VarTokenNo = CLng(Val(scripttable.Compute("max(token)", "script='" & script1 & "'").ToString))
    '                    DrTr("Token1") = VarTokenNo
    '                End If
    '                DrTr("IsLiq") = False
    '                DrTr("OrderNo") = Val(DR("OrderNo").ToString)
    '                DrTr("lActivityTime") = Val(DR("lActivityTime").ToString)
    '                ' DrTr("Dealer") = DR("Dealer")
    '                DrTr("FileFlag") = VarFileFlag
    '                DTTrade.Rows.Add(DrTr)
    '            End If
    '        Next
    '        objTrad.Insert(DTTrade)

    '        'insert to global dtFotrades table
    '        insert_FOTradeToGlobalTable(DTTrade)
    '        status = False
    '        If (DTTrade.Rows.Count > 0) Then
    '            status = True

    '            objTrad.Delete_prBal(tempdate.Date)
    '            cal_prebal(tempdate.Date)
    '            objAna.process_data_FO(DTTrade)

    '            MsgBox("F&O Trading File Process Successfully", MsgBoxStyle.Information)
    '        Else
    '            status = False
    '            MsgBox("File already imported", MsgBoxStyle.Exclamation)
    '        End If

    '    ElseIf Var = "EQ" Then
    '        ' If Text File is Equity
    '        Dim DTEquity As New DataTable
    '        REM 2: create datatable for equity
    '        With DTEquity.Columns
    '            .Add("script")
    '            .Add("company")
    '            .Add("eq")
    '            .Add("qty", GetType(Double))
    '            .Add("rate", GetType(Double))
    '            .Add("tot", GetType(Double))
    '            .Add("entrydate", GetType(Date))
    '            .Add("entryno", GetType(Integer))
    '            .Add("orderno", GetType(Long))
    '            .Add("lActivityTime", GetType(Long))
    '            ' .Add("dealer", GetType(String))
    '            .Add("FileFlag")
    '        End With
    '        REM 2: END
    '        tempdate = CDate(DTData.Rows(0)("EntryDate").ToString)
    '        For Each DR As DataRow In DTData.Rows 'Select("dealer = " & dealer)
    '            If analysis.hashOrder.ContainsKey(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim) = True Then
    '                Continue For
    '            Else
    '                analysis.hashOrder.Add(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim, "1")
    '            End If

    '            DR("lActivityTime") = DateDiff(DateInterval.Second, CDate("1-1-1980"), CDate(DR("EntryDate")))
    '            DR("Script") = DR("Company") & Space(2) & DR("CP")
    '            DR("Dealer") = VarDealerPrefix & DR("Dealer")
    '            VarTokenNo = Val(eqmaster.Compute("MAX(token)", "Script='" & DR("Script") & "'").ToString)
    '            If VarTokenNo > 0 Then

    '                DR("Token1") = VarTokenNo 'Update Token In Datatable
    '                Dim DrEq As DataRow = DTEquity.NewRow
    '                DrEq("Script") = DR("Script")
    '                DrEq("Company") = DR("Company")
    '                DrEq("Eq") = DR("CP")
    '                DrEq("Rate") = DR("Rate")
    '                If CInt(DR("buysell")) = 1 Then
    '                    DrEq("Qty") = DR("Qty")
    '                    DrEq("tot") = Val(DR("Qty")) * Val(DR("Rate"))
    '                Else
    '                    DrEq("Qty") = -DR("Qty")
    '                    DrEq("tot") = -(Val(DR("Qty")) * Val(DR("Rate")))
    '                End If

    '                DrEq("EntryDate") = DR("EntryDate")
    '                DrEq("EntryNo") = Val(DR("EntryNo").ToString)
    '                DrEq("OrderNo") = Val(DR("OrderNo").ToString)
    '                DrEq("lActivityTime") = Val(DR("lActivityTime").ToString)
    '                'DrEq("Dealer") = DR("Dealer")
    '                DrEq("FileFlag") = VarFileFlag
    '                DTEquity.Rows.Add(DrEq)
    '            End If
    '        Next
    '        objTrad.insert_equity(DTEquity)
    '        'insert equity to global Equity trade table
    '        insert_EQTradeToGlobalTable(DTEquity)
    '        status = False
    '        If (DTEquity.Rows.Count > 0) Then
    '            status = True
    '            objTrad.Delete_prBal(tempdate.Date)
    '            cal_prebal(tempdate.Date)
    '            objAna.process_data_EQ(DTEquity)

    '            MsgBox("File imported Successfully", MsgBoxStyle.Information)
    '        Else
    '            status = False
    '            MsgBox("File already imported", MsgBoxStyle.Exclamation)
    '        End If
    '    ElseIf Var = "CURRENCY" Then
    '        Dim DTCurr As New DataTable
    '        REM 1: create datatable for currency
    '        With DTCurr.Columns
    '            .Add("entryno", GetType(Integer))
    '            .Add("instrumentname")
    '            .Add("company")
    '            .Add("mdate", GetType(Date))
    '            .Add("strikerate", GetType(Double))
    '            .Add("cp")
    '            .Add("qty", GetType(Double))
    '            .Add("rate", GetType(Double))
    '            .Add("tot", GetType(Double))
    '            .Add("entrydate", GetType(Date))
    '            .Add("script")
    '            .Add("token1", GetType(Long))
    '            .Add("isliq")
    '            .Add("orderno", GetType(Long))
    '            .Add("lActivityTime", GetType(Long))
    '            '   .Add("dealer", GetType(String))
    '            .Add("FileFlag")
    '        End With
    '        REM 1: END
    '        Dim tempdate As Date
    '        tempdate = CDate(DTData.Rows(0)("EntryDate").ToString)
    '        For Each DR As DataRow In DTData.Rows 'Select("Dealer = '" & dealer & "'")
    '            Dim orderno As String = DR("orderno").ToString.Trim
    '            Dim entryno As String = DR("entryno").ToString.Trim
    '            If GdtCurrencyTrades.Select("orderno='" & orderno & "' and entryno ='" & entryno & "'").Length > 0 Then
    '                Continue For
    '            End If
    '            'If analysis.hashOrder.ContainsKey(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim) = True Then
    '            '    Continue For
    '            'Else
    '            '    analysis.hashOrder.Add(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim, "1")
    '            'End If

    '            DR("lActivityTime") = DateDiff(DateInterval.Second, CDate("1-1-1980"), CDate(DR("EntryDate")))
    '            If DR("instrumentname") <> "OPTCUR" Then
    '                DR("CP") = "F"
    '            End If
    '            If DR("CP").ToString.Length > 0 Then
    '                DR("CP") = Mid(DR("CP"), 1, 1)
    '            End If
    '            If DR("CP") = "C" Or DR("CP") = "P" Then
    '                If DR("instrumentname") = "OPTCUR" Then
    '                    DR("Script") = DR("instrumentname") & Space(2) & DR("Company") & Space(2) & Format(CDate(DR("MDate")), "ddMMMyyyy") & Space(2) & DR("StrikeRate") & Space(2) + DR("CP") & "E"
    '                Else
    '                    DR("Script") = DR("instrumentname") & Space(2) & DR("Company") & Space(2) & Format(CDate(DR("MDate")), "ddMMMyyyy") & Space(2) & DR("StrikeRate") & Space(2) + DR("CP") & "A"
    '                End If
    '            ElseIf DR("CP") = "F" Then
    '                DR("Script") = DR("instrumentname") & Space(2) & DR("Company") & Space(2) & Format(CDate(DR("MDate")), "ddMMMyyyy")
    '            End If
    '            'DR("Dealer") = VarDealerPrefix & DR("Dealer")
    '            Dim DRCurrVal() As DataRow = Currencymaster.Select("Script='" & DR("Script") & "'")

    '            'VarTokenNo = Val(Currencymaster.Compute("MAX(Token)", "Script='" & DR("Script") & "'").ToString)
    '            If DRCurrVal.Length > 0 And (DR("CP") = "F" Or DR("CP") = "P" Or DR("CP") = "C") Then
    '                DR("Token1") = DRCurrVal(0).Item("Token")
    '                REM 3.1: Process for currency
    '                Dim DrCurr As DataRow = DTCurr.NewRow
    '                DrCurr("Instrumentname") = DR("Instrumentname")
    '                DrCurr("Company") = DR("Company")
    '                DrCurr("MDate") = DR("MDate")
    '                DrCurr("StrikeRate") = Val(DR("StrikeRate").ToString)
    '                DrCurr("CP") = DR("CP")
    '                DrCurr("Script") = DR("Script")
    '                DrCurr("Rate") = DR("Rate")

    '                If CInt(DR("buysell")) = 1 Then
    '                    DrCurr("Qty") = DR("Qty") * DRCurrVal(0).Item("multiplier")
    '                    DrCurr("tot") = Val(DrCurr("Qty")) * Val(DR("Rate"))
    '                Else
    '                    'DrCurr("Qty") = -DR("Qty")
    '                    DrCurr("Qty") = -(DR("Qty") * DRCurrVal(0).Item("multiplier"))
    '                    DrCurr("tot") = -(Val(DrCurr("Qty")) * Val(DR("Rate")))
    '                End If


    '                DrCurr("EntryDate") = DR("EntryDate")
    '                DrCurr("EntryNo") = Val(DR("EntryNo").ToString)
    '                If DR("CP") = "F" Then
    '                    DrCurr("Token1") = 0
    '                Else
    '                    Dim a, a1, script1 As String
    '                    a = Mid(DR("Script").ToString, Len(DR("Script").ToString) - 1, 1)
    '                    a1 = Mid(DR("Script").ToString, Len(DR("Script").ToString), 1)
    '                    If a = "C" Then
    '                        script1 = Mid(DR("Script").ToString, 1, Len(DR("Script").ToString) - 2) & "P" & a1
    '                    Else
    '                        script1 = Mid(DR("Script").ToString, 1, Len(DR("Script").ToString) - 2) & "C" & a1
    '                    End If
    '                    VarTokenNo = CLng(Val(scripttable.Compute("max(token)", "script='" & script1 & "'").ToString))
    '                    DrCurr("Token1") = VarTokenNo
    '                End If
    '                DrCurr("IsLiq") = False
    '                DrCurr("OrderNo") = Val(DR("OrderNo").ToString)
    '                DrCurr("lActivityTime") = Val(DR("lActivityTime").ToString)
    '                DrCurr("FileFlag") = VarFileFlag
    '                DTCurr.Rows.Add(DrCurr)
    '            End If
    '        Next
    '        objTrad.insert_Currency_Trading(DTCurr)
    '        insert_CurrencyTradeToGlobalTable(DTCurr)
    '        status = False
    '        If (DTCurr.Rows.Count > 0) Then
    '            status = True
    '            objTrad.Delete_prBal(tempdate.Date)
    '            cal_prebal(tempdate.Date)
    '            objAna.process_data_Currency(DTCurr)
    '            MsgBox("Currency Trading File Process Successfully", MsgBoxStyle.Information)
    '        Else
    '            status = False
    '            MsgBox("File already imported", MsgBoxStyle.Exclamation)
    '        End If
    '    End If
    '    Return status
    'End Function
    'Public Function proc_data_FromNeatFOTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txtpath.Text
    '    Dim Hs As New Hashtable
    '    Dim Var As String = "FO"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Instrumentname", 2)
    '    Hs.Add("Company", 3)
    '    Hs.Add("MDate", 4)
    '    Hs.Add("StrikeRate", 5)
    '    Hs.Add("CP", 6)
    '    Hs.Add("Script", 7)
    '    Hs.Add("Dealer", 11)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("Qty", 14)
    '    Hs.Add("Rate", 15)
    '    Hs.Add("EntryDate", 21)
    '    Hs.Add("orderno", 23)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try
    '    Dtr = New DataView(Dtr, "Dealer = '" & txtFoDealer.Text & "'", "", DataViewRowState.CurrentRows).ToTable
    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If
    '    Call GSub_ImporttxtFileToTrading(Dtr, "NEATFOTEXT", "NEAT-", Var)
    '    Return True
    'End Function
    'Public Function proc_data_FromGetsFOTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txtpath.Text

    '    Dim Hs As New Hashtable
    '    Dim Var As String = "FO"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Instrumentname", 2)
    '    Hs.Add("Company", 3)
    '    Hs.Add("MDate", 4)
    '    Hs.Add("StrikeRate", 5)
    '    Hs.Add("CP", 6)
    '    Hs.Add("Script", 7)
    '    Hs.Add("Dealer", 11)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("Qty", 14)
    '    Hs.Add("Rate", 15)
    '    Hs.Add("EntryDate", 21)
    '    Hs.Add("orderno", 23)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    If Dtr.Rows.Count = 0 Then
    '        Return False
    '        Exit Function
    '    End If

    '    Call GSub_ImporttxtFileToTrading(Dtr, "GETSFOTEXT", "GETS-", Var)
    '    Return True
    'End Function
    'Public Function proc_data_FromGETSEqTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txteqpath.Text

    '    Dim Hs As New Hashtable
    '    Dim Var As String = "EQ"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Company", 2)
    '    Hs.Add("CP", 3)
    '    Hs.Add("Script", 7) 'Temporary Field Assing
    '    Hs.Add("Dealer", 8)
    '    Hs.Add("buysell", 10)
    '    Hs.Add("Qty", 11)
    '    Hs.Add("Rate", 12)
    '    Hs.Add("EntryDate", 19)
    '    Hs.Add("orderno", 21)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If
    '    Call GSub_ImporttxtFileToTrading(Dtr, "GETSEQTEXT", "GETS-", Var)
    '    Return True
    'End Function
    'Public Function proc_data_FromNSEFOTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txtpath.Text

    '    Dim Hs As New Hashtable
    '    Dim Var As String = "FO"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Instrumentname", 2)
    '    Hs.Add("Company", 3)
    '    Hs.Add("MDate", 4)
    '    Hs.Add("StrikeRate", 5)
    '    Hs.Add("CP", 6)
    '    Hs.Add("Script", 7)
    '    Hs.Add("Dealer", 11)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("Qty", 14)
    '    Hs.Add("Rate", 15)
    '    Hs.Add("EntryDate", 21)
    '    Hs.Add("orderno", 23)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    If Dtr.Rows.Count = 0 Then
    '        Return False
    '        Exit Function
    '    End If

    '    Call GSub_ImporttxtFileToTrading(Dtr, "NSEFOTEXT", "NSE-", Var)
    '    Return True
    'End Function
    'Public Function proc_data_FromNSEEqTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txteqpath.Text

    '    Dim Hs As New Hashtable
    '    Dim Var As String = "EQ"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Company", 2)
    '    Hs.Add("CP", 3)
    '    Hs.Add("Script", 7) 'Temporary Field Assing
    '    Hs.Add("Dealer", 8)
    '    Hs.Add("buysell", 10)
    '    Hs.Add("Qty", 11)
    '    Hs.Add("Rate", 12)
    '    Hs.Add("EntryDate", 19)
    '    Hs.Add("orderno", 20)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If
    '    Call GSub_ImporttxtFileToTrading(Dtr, "EQTEXT", "NSE-", Var)
    '    Return True
    'End Function
    'Public Function proc_data_FromODINFOTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txtpath.Text

    '    Dim Hs As New Hashtable
    '    Dim Var As String = "FO"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Instrumentname", 2)
    '    Hs.Add("Company", 3)
    '    Hs.Add("MDate", 4)
    '    Hs.Add("StrikeRate", 5)
    '    Hs.Add("CP", 6)
    '    Hs.Add("Script", 7)
    '    Hs.Add("Dealer", 11)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("Qty", 14)
    '    Hs.Add("Rate", 15)
    '    Hs.Add("EntryDate", 21)
    '    Hs.Add("orderno", 23)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    Call GSub_ImporttxtFileToTrading(Dtr, "ODINFOTEXT", "ODIN-", Var)
    '    Dtr.Columns("Token1").ColumnName = "TokenNo"
    '    Dtr.Columns("Qty").ColumnName = "Unit"

    '    Return True
    'End Function
    'Public Function proc_data_FromODINEqTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txteqpath.Text

    '    Dim Hs As New Hashtable
    '    Dim Var As String = "EQ"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Company", 2)
    '    Hs.Add("CP", 3)
    '    Hs.Add("Script", 7) 'Temporary Field Assing
    '    Hs.Add("Dealer", 8)
    '    Hs.Add("buysell", 10)
    '    Hs.Add("Qty", 11)
    '    Hs.Add("Rate", 12)
    '    Hs.Add("EntryDate", 19)
    '    Hs.Add("orderno", 21)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try
    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    Call GSub_ImporttxtFileToTrading(Dtr, "ODINEQTEXT", "ODIN-", Var)
    '    Return True
    'End Function
    'Public Function proc_data_FromNowFOTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txtpath.Text
    '    Dim Hs As New Hashtable
    '    Dim Var As String = "FO"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Instrumentname", 2)
    '    Hs.Add("Company", 3)
    '    Hs.Add("MDate", 4)
    '    Hs.Add("StrikeRate", 5)
    '    Hs.Add("CP", 6)
    '    Hs.Add("Script", 7)
    '    Hs.Add("Dealer", 11)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("Qty", 14)
    '    Hs.Add("Rate", 15)
    '    Hs.Add("EntryDate", 21)
    '    Hs.Add("orderno", 23)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    Dim status As Boolean
    '    status = GSub_ImporttxtFileToTrading(Dtr, "NOWFOTEXT", "NOW-", Var)
    '    Return status
    'End Function
    'Public Function proc_data_FromNotisFOTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txtpath.Text
    '    Dim Hs As New Hashtable
    '    Dim Var As String = "FO"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Instrumentname", 2)
    '    Hs.Add("Company", 3)
    '    Hs.Add("MDate", 4)
    '    Hs.Add("StrikeRate", 5)
    '    Hs.Add("CP", 6)
    '    Hs.Add("Script", 7)
    '    Hs.Add("Dealer", 11)
    '    Hs.Add("Qty", 14)
    '    Hs.Add("Rate", 15)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("EntryDate", 21)
    '    Hs.Add("orderno", 23)
    '    Hs.Add("Dealer1", 26)
    '    Dim Dtr As New DataTable
    '    'Try
    '    Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    For Each DRow As DataRow In Dtr.Select("Dealer1<>'0'")
    '        If Val(DRow("Dealer1").ToString) <> 0 Then
    '            If DRow("Dealer1").ToString.Length > 12 Then If DRow("Dealer1").ToString.Length > 12 Then DRow("Dealer") = DRow("Dealer1").ToString.Substring(0, 12)
    '        End If
    '    Next
    '    Dtr = New DataView(Dtr, "Dealer='" & txtFoDealer.Text & "'", "EntryDate", DataViewRowState.Added).ToTable
    '    If Dtr.Rows.Count = 0 Then Exit Function
    '    Dim status As Boolean

    '    status = GSub_ImporttxtFileToTrading(Dtr, "NOTICEFOTEXT", "NOTIS-", Var)
    '    Return status
    'End Function
    'Public Function proc_data_FromNotisEQTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txteqpath.Text

    '    Dim Hs As New Hashtable
    '    Dim Var As String = "EQ"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Company", 2)
    '    Hs.Add("CP", 3)
    '    Hs.Add("Script", 7) 'Temporary Field Assing
    '    Hs.Add("Dealer", 8)
    '    Hs.Add("buysell", 10)
    '    Hs.Add("Qty", 11)
    '    Hs.Add("Rate", 12)
    '    Hs.Add("EntryDate", 19)
    '    Hs.Add("orderno", 21)
    '    Hs.Add("Dealer1", 24)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox("NOTIS Text File Eq :: " & ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If
    '    Dim VarlActivityTime As Long
    '    VarlActivityTime = Val(tptable.Compute("max(lactivitytime)", "fileflag='NOWEQTEXT'").ToString)
    '    Dim MyLastActivityDate As Date = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
    '    Dim Dv As New DataView(Dtr, "EntryDate > #" & MyLastActivityDate & "#", "EntryDate", DataViewRowState.Added)

    '    Dtr = Dv.ToTable()

    '    If Dtr.Rows.Count = 0 Then
    '        Return False
    '        Exit Function
    '    End If

    '    For Each DRow As DataRow In Dtr.Select("Dealer1<>'0'")
    '        If Val(DRow("Dealer1").ToString) <> 0 Then
    '            If DRow("Dealer1").ToString.Length > 12 Then DRow("Dealer") = DRow("Dealer1").ToString.Substring(0, 12)
    '        End If
    '    Next
    '    Dtr = New DataView(Dtr, "Dealer='" & txtEqDealer.Text & "'", "EntryDate", DataViewRowState.Added).ToTable
    '    If Dtr.Rows.Count = 0 Then Exit Function

    '    Call GSub_ImporttxtFileToTrading(Dtr, "NOTICEEQTEXT", "NOTICE-", Var)
    '    Return True
    'End Function
    'Public Function proc_data_FromNowEQTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean

    '    Dim VarFilePath As String = txteqpath.Text
    '    Dim Hs As New Hashtable
    '    Dim Var As String = "EQ"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Company", 2)
    '    Hs.Add("CP", 3)
    '    Hs.Add("Script", 7) 'Temporary Field Assing
    '    Hs.Add("Dealer", 8)
    '    Hs.Add("buysell", 10)
    '    Hs.Add("Qty", 11)
    '    Hs.Add("Rate", 12)
    '    Hs.Add("EntryDate", 19)
    '    Hs.Add("orderno", 21)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    Call GSub_ImporttxtFileToTrading(Dtr, "NOWEQTEXT", "NOW-", Var)
    '    Return True
    'End Function
    'Public Function GetDTFromTextFile(ByVal SeparatorChar As String, ByVal FilePath As String, ByVal ColList As Hashtable, ByVal Var As String) As DataTable
    '    Dim DtMy As New DataTable
    '    Dim Item As DictionaryEntry
    '    REM Add Column into datatable according to Parameter Hashtable
    '    For Each Item In ColList
    '        If Item.Key.ToString.ToUpper.Contains("DATE") Then
    '            DtMy.Columns.Add(Item.Key, GetType(DateTime))
    '        Else
    '            DtMy.Columns.Add(Item.Key)
    '        End If
    '    Next
    '    REM Check File exist from Path
    '    If File.Exists(FilePath) = False Then
    '        Throw New ApplicationException("Following file path " & FilePath & " Not Found !!")
    '        Return DtMy
    '        Exit Function
    '    End If
    '    Dim RStream As New System.IO.StreamReader(FilePath)
    '    REM Skeep Starting Line   
    '    '=====================by keval to ignore start line
    '    'For i As Long = 0 To StartLineNo - 1
    '    '    RStream.ReadLine()
    '    '    If RStream.EndOfStream = True Then
    '    '        RStream.Close()
    '    '        Return DtMy
    '    '        Exit Function
    '    '    End If
    '    'Next
    '    '===============================
    '    '' Check File is Equity,Currency or Future
    '    Dim Str As String
    '    Str = RStream.ReadLine()
    '    If Str Is Nothing Or Str = "" Then
    '        RStream.Close()
    '        Return DtMy
    '        Exit Function
    '    End If

    '    Dim StrData1 As String() = Split(Str, SeparatorChar)

    '    If Var = "FO" Then
    '        If IsNumeric(StrData1(ColList("Script"))) = True Then
    '            MsgBox("Traded File Type Miss-Match !!!", MsgBoxStyle.Critical)
    '            RStream.Close()
    '            Return DtMy
    '            Exit Function
    '        End If
    '    ElseIf Var = "EQ" Then
    '        If Not IsNumeric(StrData1(ColList("Script"))) = True Then
    '            MsgBox("Traded File Type Miss-Match !!!", MsgBoxStyle.Critical)
    '            RStream.Close()
    '            Return DtMy
    '            Exit Function
    '        End If
    '    ElseIf Var = "CURR" Then
    '        If Not IsNumeric(StrData1(ColList("Script"))) = True Then
    '            MsgBox("Traded File Type Miss-Match !!!", MsgBoxStyle.Critical)
    '            RStream.Close()
    '            Return DtMy
    '            Exit Function
    '        End If
    '    End If

    '    ''Line by line row Added into Datatable
    '    Do Until Str Is Nothing
    '        Dim Dr As DataRow = DtMy.NewRow
    '        If Str <> "" Then
    '            Dim StrData As String() = Split(Str, SeparatorChar)
    '            For Each Item In ColList
    '                Dr(Item.Key) = Trim(StrData(Item.Value))
    '            Next
    '            DtMy.Rows.Add(Dr)
    '        End If
    '        Str = RStream.ReadLine()
    '    Loop

    '    ''Stream Reader Close
    '    RStream.Close()
    '    Return DtMy
    'End Function

    'Public Function proc_data_FromNeatCurrTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txtCurrpath.Text
    '    Dim Hs As New Hashtable
    '    Dim Var As String = "CURRENCY"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Instrumentname", 2)
    '    Hs.Add("Company", 3)
    '    Hs.Add("MDate", 4)
    '    Hs.Add("StrikeRate", 5)
    '    Hs.Add("CP", 6)
    '    Hs.Add("Script", 7)
    '    Hs.Add("Dealer", 11)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("Qty", 14)
    '    Hs.Add("Rate", 15)
    '    Hs.Add("EntryDate", 21)
    '    Hs.Add("orderno", 23)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try
    '    Dtr = New DataView(Dtr, "Dealer = '" & txtCurrDealer.Text & "'", "", DataViewRowState.CurrentRows).ToTable
    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If
    '    Call GSub_ImporttxtFileToTrading(Dtr, "NEATCURRENCYTEXT", "NEAT-", Var)
    '    Return True
    'End Function


    'Public Function proc_data_FromNotisCurrTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txtCurrpath.Text
    '    Dim Hs As New Hashtable
    '    Dim Var As String = "CURRENCY"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Instrumentname", 2)
    '    Hs.Add("Company", 3)
    '    Hs.Add("MDate", 4)
    '    Hs.Add("StrikeRate", 5)
    '    Hs.Add("CP", 6)
    '    Hs.Add("Script", 7)
    '    Hs.Add("Dealer", 11)
    '    Hs.Add("Qty", 14)
    '    Hs.Add("Rate", 15)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("EntryDate", 21)
    '    Hs.Add("orderno", 23)
    '    Hs.Add("Dealer1", 26)
    '    Dim Dtr As New DataTable
    '    'Try
    '    Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    For Each DRow As DataRow In Dtr.Select("Dealer1<>'0'")
    '        If Val(DRow("Dealer1").ToString) <> 0 Then
    '            If DRow("Dealer1").ToString.Length > 12 Then If DRow("Dealer1").ToString.Length > 12 Then DRow("Dealer") = DRow("Dealer1").ToString.Substring(0, 12)
    '        End If
    '    Next
    '    Dtr = New DataView(Dtr, "Dealer='" & txtCurrDealer.Text & "'", "EntryDate", DataViewRowState.Added).ToTable
    '    If Dtr.Rows.Count = 0 Then Exit Function
    '    Dim status As Boolean

    '    status = GSub_ImporttxtFileToTrading(Dtr, "NOTICECURRENCYTEXT", "NOTIS-", Var)
    '    Return status
    'End Function

    'Public Function proc_data_FromNowCurrTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txtCurrpath.Text
    '    Dim Hs As New Hashtable
    '    Dim Var As String = "CURRENCY"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Instrumentname", 2)
    '    Hs.Add("Company", 3)
    '    Hs.Add("MDate", 4)
    '    Hs.Add("StrikeRate", 5)
    '    Hs.Add("CP", 6)
    '    Hs.Add("Script", 7)
    '    Hs.Add("Dealer", 11)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("Qty", 14)
    '    Hs.Add("Rate", 15)
    '    Hs.Add("EntryDate", 21)
    '    Hs.Add("orderno", 23)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    Dim status As Boolean
    '    status = GSub_ImporttxtFileToTrading(Dtr, "NOWCURRENCYTEXT", "NOW-", Var)
    '    Return status
    'End Function
    'Public Function proc_data_FromODINCurrTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txtCurrpath.Text
    '    Dim Hs As New Hashtable
    '    Dim Var As String = "CURRENCY"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Instrumentname", 2)
    '    Hs.Add("Company", 3)
    '    Hs.Add("MDate", 4)
    '    Hs.Add("StrikeRate", 5)
    '    Hs.Add("CP", 6)
    '    Hs.Add("Script", 7)
    '    Hs.Add("Dealer", 11)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("Qty", 14)
    '    Hs.Add("Rate", 15)
    '    Hs.Add("EntryDate", 21)
    '    Hs.Add("orderno", 23)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    Call GSub_ImporttxtFileToTrading(Dtr, "ODINCURRENCYTEXT", "ODIN-", Var)
    '    Dtr.Columns("Token1").ColumnName = "TokenNo"
    '    Dtr.Columns("Qty").ColumnName = "Unit"
    '    Return True
    'End Function
    'Public Function proc_data_FromGetsCurrTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txtCurrpath.Text

    '    Dim Hs As New Hashtable
    '    Dim Var As String = "CURRENCY"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Instrumentname", 2)
    '    Hs.Add("Company", 3)
    '    Hs.Add("MDate", 4)
    '    Hs.Add("StrikeRate", 5)
    '    Hs.Add("CP", 6)
    '    Hs.Add("Script", 7)
    '    Hs.Add("Dealer", 11)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("Qty", 14)
    '    Hs.Add("Rate", 15)
    '    Hs.Add("EntryDate", 21)
    '    Hs.Add("orderno", 23)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    If Dtr.Rows.Count = 0 Then
    '        Return False
    '        Exit Function
    '    End If

    '    Call GSub_ImporttxtFileToTrading(Dtr, "GETSCURRENCYTEXT", "GETS-", Var)
    '    Return True
    'End Function
    'Public Function proc_data_FromNSECurrTEXT(Optional ByVal ISTimer As Boolean = False) As Boolean
    '    Dim VarFilePath As String = txtCurrpath.Text
    '    Dim Hs As New Hashtable
    '    Dim Var As String = "CURRENCY"
    '    Hs.Add("EntryNo", 0)
    '    Hs.Add("Instrumentname", 2)
    '    Hs.Add("Company", 3)
    '    Hs.Add("MDate", 4)
    '    Hs.Add("StrikeRate", 5)
    '    Hs.Add("CP", 6)
    '    Hs.Add("Script", 7)
    '    Hs.Add("Dealer", 11)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("Qty", 14)
    '    Hs.Add("Rate", 15)
    '    Hs.Add("EntryDate", 21)
    '    Hs.Add("orderno", 23)
    '    Dim Dtr As New DataTable
    '    Try
    '        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    If Dtr.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    If Dtr.Rows.Count = 0 Then
    '        Return False
    '        Exit Function
    '    End If

    '    Call GSub_ImporttxtFileToTrading(Dtr, "NSECURRENCYTEXT", "NSE-", Var)
    '    Return True
    'End Function
#End Region


End Class