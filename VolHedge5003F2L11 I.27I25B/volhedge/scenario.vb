'Imports System.Windows.Forms.DataVisualization.Charting
Imports MSChart20Lib
Imports System.io
Imports System.Data
Imports System.Data.OleDb
Imports OptionG
Public Class scenario
    Public objProfitLossChart As New frmprofitLossChart
    Public VarIsCurrency As Boolean = False

    Dim VarIsFrmLoad As Boolean = False
    Dim VarIsFirstLoad As Boolean = False
    Dim FirstTime As Boolean = True
    Dim mAllCV As String = ""
    Dim FirstDgt As Integer
#Region "Variable"
    Dim XResolution As Integer
    Public Shared cellno As Integer
    Public isVolCal As Boolean = False
    Public Shared chartName As String
    Public Shared ValLBLprofit As String
    Public Shared ValLBLgamma As String
    Public Shared ValLBLtheta As String
    Public Shared ValLBLvega As String
    Public Shared ValLBLdelta As String
    Public Shared ValLBLvolga As String
    Public Shared ValLBLVanna As String
    Public Shared chkscenario As Boolean
    Dim objExp As New expenses
    Dim objScenarioDetail As New scenarioDetail
    'Dim cpdtable As New DataTable
    Dim fdtable As New DataTable
    Dim profit As New DataTable
    Dim deltatable As New DataTable
    Dim gammatable As New DataTable
    Dim vegatable As New DataTable
    Dim thetatable As New DataTable
    Dim volgatable As New DataTable
    Dim vannatable As New DataTable
    'Dim mObjData As New DataAnalysis.AnalysisData
    Dim Mrateofinterast As Double = 0
    Dim objTrad As trading = New trading
    Dim rtable As DataTable
    Dim flgUnderline As Boolean = False
    Public trscen As Boolean = False
    Public time1, time2 As Date
    Public mvalue As Double
    'Dim roundGamma As Integer
    'Dim roundTheta As Integer
    'Dim roundDelta As Integer
    'Dim roundVega As Integer
    Dim gcheck As Boolean = False
    Public scname As String
    'Public mAllCV As String
    Dim txtmid As Double
    Dim interval As Double
    Dim checkall As Boolean = True
    Dim grossmtm As Double
    Dim contMenu() As ContextMenuStrip
    Dim contMenuExpiry() As ContextMenuStrip
    Dim frmFlg As Boolean = False

    Dim involflag As Boolean = False
    Dim decvolflag As Boolean = False
#End Region
    Private Sub scenario_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If Not objanalysis Is Nothing Then
            objanalysis.refreshstarted = True
        End If
        Me.Icon = My.Resources.volhedge_icon
        'Me.WindowState = FormWindowState.Maximized
        'Me.Refresh()
    End Sub
    Private Sub scenario_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        chkscenario = False
        txtmkt.Text = 0
        'If Not objanalysis Is Nothing Then
        '    objanalysis.refreshstarted = True
        'End If
        If MsgBox("Do you want to save Interval and No. of Strike??", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Save") = MsgBoxResult.Yes Then
            objScenarioDetail.Delete_scenario()
            objScenarioDetail.Interval = Val(txtinterval.Text)
            objScenarioDetail.Strike = Val(txtllimit.Text)
            objScenarioDetail.Interval_type = IIf(chkint.Checked = True, "Per", "Value")
            objScenarioDetail.Insert_scenario()
        End If
        Call analysis.searchcompany()
    End Sub
    Private Sub scenario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        VarIsFrmLoad = True
        VarIsFirstLoad = True
        Me.WindowState = FormWindowState.Maximized
        If Not objanalysis Is Nothing Then
            objanalysis.refreshstarted = False
        End If
        chkscenario = True
        Me.Text = Me.Text & "-" & scname
        If scenariotable.Rows.Count > 0 Then
            grossmtm = scenariotable.Rows(0)("grossMTM")
        End If
        If VarIsCurrency = False Then
            txtinterast.Text = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
        ElseIf VarIsCurrency = True Then
            txtinterast.Text = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='CurrrencyRateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
        End If
        Mrateofinterast = Val(txtinterast.Text)
        'roundTheta = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundTheta'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundTheta'")))
        'roundDelta = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundDelta'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundDelta'")))
        'roundVega = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundVega'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundVega'")))
        'roundGamma = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundGamma'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundGamma'")))
        txtcvol.Text = 0
        txtpvol.Text = 0
        txtmid = 0
        If trscen = True Then
            chkcheck.Checked = True
            gcheck = True
            dttoday.Value = time1
            dtexp.Value = time2
            txtdays.Text = DateDiff(DateInterval.Day, time1.Date, time2.Date)
            If dttoday.Value.Date < dtexp.Value.Date Then
                REM This code commented because of Day difference plus 1 days display
                'txtdays.Text = Val(txtdays.Text) + 1
            ElseIf dttoday.Value.Date = dtexp.Value.Date Then
                txtdays.Text = Val(txtdays.Text) + 0.5
            End If
            txtmkt.Text = Math.Round(mvalue, 0)
            txtmid = mvalue
            Dim grow As New DataGridViewRow
            For Each drow As DataRow In scenariotable.Rows
                grow = New DataGridViewRow
                grdtrad.Rows.Add(grow)
                'grdtrad.Refresh()
            Next
            Dim i As Integer = 0
            txtunits.Text = 0
            txtdelval.Text = 0
            txtthval.Text = 0
            txtvgval.Text = 0
            txtgmval.Text = 0

            ''divyesh
            txtunits1.Text = 0
            txtdelval1.Text = 0
            txtthval1.Text = 0
            txtvgval1.Text = 0
            txtgmval1.Text = 0
            For Each drow As DataRow In scenariotable.Select("", "cpf")
                grdtrad.Rows(i).Cells("Active").Value = CBool(drow("status"))
                grdtrad.Rows(i).Cells("TimeI").Value = CDate(drow("time1"))
                grdtrad.Rows(i).Cells("TimeII").Value = CDate(drow("time2"))
                grdtrad.Rows(i).Cells("CPF").Value = CStr(drow("cpf"))
                grdtrad.Rows(i).Cells("spval").Value = CDbl(drow("spot"))
                grdtrad.Rows(i).Cells("Strike").Value = CDbl(drow("strikes"))
                grdtrad.Rows(i).Cells("units").Value = Val(drow("qty"))
                grdtrad.Rows(i).Cells("ltp").Value = Val(drow("ltp"))
                grdtrad.Rows(i).Cells("last").Value = CDbl(drow("rate"))
                If Val(drow("vol")) = 0 Then
                    grdtrad.Rows(i).Cells("lv").Value = CDbl(0.0)
                Else
                    grdtrad.Rows(i).Cells("lv").Value = Val(drow("vol"))
                End If
                grdtrad.Rows(i).Cells("delta").Value = Val(drow("delta"))
                grdtrad.Rows(i).Cells("deltaval").Value = Val(drow("deltaval"))
                grdtrad.Rows(i).Cells("gamma").Value = Val(drow("gamma"))
                grdtrad.Rows(i).Cells("gmval").Value = Val(drow("gmval"))
                grdtrad.Rows(i).Cells("vega").Value = Val(drow("vega"))
                grdtrad.Rows(i).Cells("vgval").Value = Val(drow("vgval"))
                grdtrad.Rows(i).Cells("theta").Value = Val(drow("theta"))
                grdtrad.Rows(i).Cells("thetaval").Value = Val(drow("thetaval"))

                grdtrad.Rows(i).Cells("volga").Value = Val(drow("volga"))
                grdtrad.Rows(i).Cells("volgaval").Value = Val(drow("volgaval"))
                grdtrad.Rows(i).Cells("vanna").Value = Val(drow("vanna"))
                grdtrad.Rows(i).Cells("vannaval").Value = Val(drow("vannaval"))

                grdtrad.Rows(i).Cells("uid").Value = Val(drow("uid"))

                ''divyesh
                grdtrad.Rows(i).Cells("delta1").Value = Val(drow("delta"))
                grdtrad.Rows(i).Cells("gamma1").Value = Val(drow("gamma"))
                grdtrad.Rows(i).Cells("vega1").Value = Val(drow("vega"))
                grdtrad.Rows(i).Cells("theta1").Value = Val(drow("theta"))
                grdtrad.Rows(i).Cells("volga1").Value = Val(drow("volga"))
                grdtrad.Rows(i).Cells("vanna1").Value = Val(drow("vanna"))

                grdtrad.Rows(i).Cells("deltaval1").Value = Val(drow("deltaval1"))
                grdtrad.Rows(i).Cells("gmval1").Value = Val(drow("gmval1"))
                grdtrad.Rows(i).Cells("vgval1").Value = Val(drow("vgval1"))
                grdtrad.Rows(i).Cells("thetaval1").Value = Val(drow("thetaval1"))
                grdtrad.Rows(i).Cells("volgaval1").Value = Val(drow("volgaval1"))
                grdtrad.Rows(i).Cells("vannaval1").Value = Val(drow("vannaval1"))


                grdtrad.Rows(i).Cells("ltp1").Value = Val(drow("ltp1"))

                'txtunits.Text = Math.Round(Val(txtunits.Text) + Val(grdtrad.Rows(i).Cells("ltp").Value), 2)
                txtdelval.Text = Format(Val(txtdelval.Text) + Val(grdtrad.Rows(i).Cells("deltaval").Value), Deltastr_Val)
                txtthval.Text = Format(Val(txtthval.Text) + Val(grdtrad.Rows(i).Cells("thetaval").Value), Thetastr_Val)
                txtvgval.Text = Format(Val(txtvgval.Text) + Val(grdtrad.Rows(i).Cells("vgval").Value), Vegastr_Val)
                txtgmval.Text = Format(Val(txtgmval.Text) + Val(grdtrad.Rows(i).Cells("gmval").Value), Gammastr_Val)
                txtVolgaVal.Text = Format(Val(txtVolgaVal.Text) + Val(grdtrad.Rows(i).Cells("volgaval").Value), Volgastr_Val)
                TxtVannaVal.Text = Format(Val(TxtVannaVal.Text) + Val(grdtrad.Rows(i).Cells("vannaval").Value), Vannastr_Val)


                ''divyesh 
                txtdelval1.Text = Format(Val(txtdelval1.Text) + Val(grdtrad.Rows(i).Cells("deltaval1").Value), Deltastr_Val)
                txtthval1.Text = Format(Val(txtthval1.Text) + Val(grdtrad.Rows(i).Cells("thetaval1").Value), Thetastr_Val)
                txtvgval1.Text = Format(Val(txtvgval1.Text) + Val(grdtrad.Rows(i).Cells("vgval1").Value), Vegastr_Val)
                txtgmval1.Text = Format(Val(txtgmval1.Text) + Val(grdtrad.Rows(i).Cells("gmval1").Value), Gammastr_Val)
                TxtVolgaVal1.Text = Format(Val(TxtVolgaVal1.Text) + Val(grdtrad.Rows(i).Cells("volgaval1").Value), Volgastr_Val)
                TxtVannaVal1.Text = Format(Val(TxtVannaVal1.Text) + Val(grdtrad.Rows(i).Cells("vannaval1").Value), Vannastr_Val)

                i += 1
            Next
            grdtrad.Columns("TimeII").DefaultCellStyle.NullValue = Format(scenariotable.Rows(0).Item("time2"), "MM/dd/yyyy")
            scenariotable.Rows.Clear()
            trscen = False
        End If


        'to get interval,No of strike from database
        Dim dtScenario As New DataTable
        dtScenario = objScenarioDetail.select_scenario
        If dtScenario.Rows.Count > 0 Then
            If dtScenario.Rows(0)("interval_type").ToString = "Per" Then
                chkint.Checked = True
            Else
                chkint.Checked = False
            End If
            txtinterval.Text = Val(dtScenario.Rows(0)("interval").ToString)
            txtllimit.Text = Val(dtScenario.Rows(0)("strike").ToString)
            interval = txtinterval.Text
        Else
            'Call CalInterval()
            'If Val(txtmkt.Text) > 0 Then
            '    txtmid = Val(txtmkt.Text)
            '    If Val(txtmid) < 100 Then
            '        txtinterval.Text = 1
            '    ElseIf Val(txtmid) > 100 And Val(txtmid) < 500 Then
            '        txtinterval.Text = 5
            '    ElseIf Val(txtmid) > 100 And Val(txtmid) < 1000 Then
            '        txtinterval.Text = 10
            '    ElseIf Val(txtmid) > 1000 And Val(txtmid) < 10000 Then
            '        txtinterval.Text = 100
            '    ElseIf Val(txtmid) > 10000 Then
            '        txtinterval.Text = 500
            '    End If
            '    interval = txtinterval.Text
            '    'interval = Math.Round(val(txtmid) * 1 / 100, 0)
            'End If
        End If

        Call CalInterval()

        txtunits.Text = Math.Round(cal_profitLoss, RoundGrossMTM)
        ''divyesh
        txtunits1.Text = Math.Round(cal_FutprofitLoss, RoundGrossMTM)

        txtdays.Text = 0
        If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
        End If
        If dttoday.Value.Date < dtexp.Value.Date Then
            REM This code commented because of Day difference plus 1 days display
            'txtdays.Text = Val(txtdays.Text) + 1
        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
            txtdays.Text = Val(txtdays.Text) + 0.5
        End If

        create_active()
        'create contextmenustrip for vol column cells
        ReDim contMenu(grdtrad.Rows.Count - 1)
        Dim cnt As Integer = 0
        For Each cntmenu As ContextMenuStrip In contMenu
            cntmenu = New ContextMenuStrip
            Dim item As New ToolStripMenuItem("Unfreeze")
            ' item.CheckOnClick = True
            item.Tag = cnt.ToString
            AddHandler item.Click, AddressOf freezVol
            AddHandler cntmenu.Opening, AddressOf contMenuOpen
            cntmenu.Items.Add(item)
            grdtrad.Rows(cnt).Cells("lv").ContextMenuStrip = cntmenu
            cnt += 1
        Next

        'create contextmenustrip for expiry column cells
        ReDim contMenuExpiry(grdtrad.Rows.Count - 1)
        cnt = 0
        For Each cntmenu As ContextMenuStrip In contMenuExpiry
            cntmenu = New ContextMenuStrip
            Dim item As New ToolStripMenuItem("Unfreeze")
            ' item.CheckOnClick = True
            item.Tag = cnt.ToString
            AddHandler item.Click, AddressOf freezExpiry
            AddHandler cntmenu.Opening, AddressOf contMenuOpenExpiry
            cntmenu.Items.Add(item)
            grdtrad.Rows(cnt).Cells("TimeII").ContextMenuStrip = cntmenu
            cnt += 1
        Next

        For Each col As DataGridViewColumn In grdtrad.Columns
            If col.Index >= 9 Then
                col.HeaderCell.Style.BackColor = Color.Gray
            End If
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next

        Call Vol_Click(Vol, e)
        Call expiry_Click(expiry, e)
        If scname <> "" Then
            mAllCV = ""
            result(True)
        End If
        'For Each drow As DataGridViewRow In grdtrad.Rows
        '    drow.Cells("VolORG").Value = drow.Cells("lv").Value
        'Next
        If analysis.chkanalysis = True Then
            MDI.ToolStripcompanyCombo.Visible = False
            MDI.ToolStripMenuSearchComp.Visible = False
        End If

        Try
            grdtrad.Rows(0).Cells(0).Selected = True
        Catch ex As Exception

        End Try

    End Sub
    Private Sub cal_summary()
        txtdelval.Text = 0
        txtthval.Text = 0
        txtvgval.Text = 0
        txtgmval.Text = 0
        txtVolgaVal.Text = 0
        TxtVannaVal.Text = 0

        txtdelval1.Text = 0
        txtthval1.Text = 0
        txtvgval1.Text = 0
        txtgmval1.Text = 0
        TxtVolgaVal1.Text = 0
        TxtVannaVal1.Text = 0

        'SetZeroVal()

        txtunits.Text = Math.Round(cal_profitLoss, RoundGrossMTM)
        txtunits1.Text = Math.Round(cal_FutprofitLoss, RoundGrossMTM)

        For Each grow As DataGridViewRow In grdtrad.Rows
            If grow.Cells("Active").Value = True Then
                txtdelval.Text = Format(Val(txtdelval.Text) + Val(grow.Cells("deltaval").Value), Deltastr_Val)
                txtthval.Text = Format(Val(txtthval.Text) + Val(grow.Cells("thetaval").Value), Thetastr_Val)
                txtvgval.Text = Format(Val(txtvgval.Text) + Val(grow.Cells("vgval").Value), Vegastr_Val)
                txtgmval.Text = Format(Val(txtgmval.Text) + Val(grow.Cells("gmval").Value), Gammastr_Val)
                txtVolgaVal.Text = Format(Val(txtVolgaVal.Text) + Val(grow.Cells("volgaval").Value), Volgastr_Val)
                TxtVannaVal.Text = Format(Val(TxtVannaVal.Text) + Val(grow.Cells("vannaval").Value), Vannastr_Val)


                'txtdelval1.Text = Format(Val(txtdelval1.Text) + Val(grow.Cells("deltaval1").Value), Deltastr_Val)
                'txtthval1.Text = Format(Val(txtthval1.Text) + Val(grow.Cells("thetaval1").Value), Thetastr_Val)
                'txtvgval1.Text = Format(Val(txtvgval1.Text) + Val(grow.Cells("vgval1").Value), Vegastr_Val)
                'txtgmval1.Text = Format(Val(txtgmval1.Text) + Val(grow.Cells("gmval1").Value), Gammastr_Val)
                'TxtVolgaVal1.Text = Format(Val(TxtVolgaVal1.Text) + Val(grow.Cells("volgaval1").Value), Volgastr_Val)
                'TxtVannaVal1.Text = Format(Val(TxtVannaVal1.Text) + Val(grow.Cells("vannaval1").Value), Vannastr_Val)
            End If
        Next
        'result(False)
    End Sub
    'Private Sub SetZeroVal()
    '    txtdelval.Text = 0
    '    txtthval.Text = 0
    '    txtvgval.Text = 0
    '    txtgmval.Text = 0
    '    txtVolgaVal.Text = 0
    '    TxtVannaVal.Text = 0

    '    txtdelval1.Text = 0
    '    txtthval1.Text = 0
    '    txtvgval1.Text = 0
    '    txtgmval1.Text = 0
    '    TxtVolgaVal1.Text = 0
    '    TxtVannaVal1.Text = 0
    'End Sub
    Private Sub contMenuOpen(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        'Dim ind As Integer
        'ind = CType(sender, ContextMenuStrip).Items(0).Tag
        If cellno = -1 Then Exit Sub
        If grdtrad.Rows(cellno).Cells("lv").ReadOnly = True Then
            CType(sender, ContextMenuStrip).Items(0).Text = "Unfreeze"
        Else
            CType(sender, ContextMenuStrip).Items(0).Text = "Freeze"
        End If
    End Sub
    Private Sub contMenuOpenExpiry(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        'Dim ind As Integer
        'ind = CType(sender, ContextMenuStrip).Items(0).Tag
        If cellno = -1 Then Exit Sub
        If grdtrad.Rows(cellno).Cells("TimeII").ReadOnly = True Then
            CType(sender, ContextMenuStrip).Items(0).Text = "Unfreeze"
        Else
            CType(sender, ContextMenuStrip).Items(0).Text = "Freeze"
        End If
    End Sub

    Private Sub freezVol(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If cellno = -1 Then Exit Sub
        If CType(sender, ToolStripMenuItem).Text = "Unfreeze" Then
            grdtrad.Rows(cellno).Cells("lv").ReadOnly = False
            CType(sender, ToolStripMenuItem).Text = "Freeze"
        Else
            grdtrad.Rows(cellno).Cells("lv").ReadOnly = True
            CType(sender, ToolStripMenuItem).Text = "Unfreeze"
        End If
    End Sub
    Private Sub freezExpiry(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If cellno = -1 Then Exit Sub
        If CType(sender, ToolStripMenuItem).Text = "Unfreeze" Then
            grdtrad.Rows(cellno).Cells("TimeII").ReadOnly = False
            CType(sender, ToolStripMenuItem).Text = "Freeze"
        Else
            grdtrad.Rows(cellno).Cells("TimeII").ReadOnly = True
            CType(sender, ToolStripMenuItem).Text = "Unfreeze"
        End If
    End Sub
    Private Sub scenario_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F9 Then
            'grdtrad.EndEdit()
            'grdtrad.Refresh()
            If Val(txtllimit.Text) > 0 And Val(txtllimit.Text > 0) And Val(interval) > 0 And Val(txtmid) > 0 Then
                result(False)
                'init_table()
                'cal_profit()
                'create_chart_profit()
                'create_chart_profit_Full()
                'cal_delta()
                'create_chart_delta()
                'create_chart_delta_Full()
                'cal_gamma()
                'create_chart_gamma()
                'create_chart_gamma_Full()
                'cal_vega()
                'create_chart_vega()
                'create_chart_vega_Full()
                'cal_theta()
                'create_chart_theta()
                'create_chart_theta_Full()

                'grdprofit.DataSource = profit
                'grdprofit.Refresh()
                'grddelta.DataSource = deltatable
                'grddelta.Refresh()
                'grdgamma.DataSource = gammatable
                'grdgamma.Refresh()
                'grdvega.DataSource = vegatable
                'grdvega.Refresh()
                'grdtheta.DataSource = thetatable
                'grdtheta.Refresh()
            End If
        ElseIf e.KeyCode = Keys.F1 Then
            result(True)
        ElseIf e.KeyCode = Keys.F8 Then
            CmdAllCV_Click(sender, e)
        ElseIf e.KeyCode = Keys.F11 Then
            export()
        ElseIf e.KeyCode = Keys.F12 Then
            import()
        ElseIf e.KeyCode = Keys.PageDown Then
            If tbcon.SelectedIndex < 9 Then tbcon.SelectTab(tbcon.SelectedIndex + 1)
        ElseIf e.KeyCode = Keys.PageUp Then
            If tbcon.SelectedIndex > 0 Then tbcon.SelectTab(tbcon.SelectedIndex - 1)
        End If
    End Sub
#Region "Initial"
    Private Sub init_datatable()
        'cpdtable = New DataTable
        'With cpdtable.Columns

        '    .Add("status", GetType(Boolean))
        '    .Add("mdate", GetType(Date))
        '    .Add("spval", GetType(Double))
        '    .Add("strikes", GetType(Double))
        '    .Add("cp")
        '    .Add("units", GetType(Double))
        '    .Add("last", GetType(Double))
        '    .Add("lv", GetType(Double))
        '    .Add("delta", GetType(Double))
        '    .Add("deltaval", GetType(Double))
        '    .Add("theta", GetType(Double))
        '    .Add("thetaval", GetType(Double))
        '    .Add("vega", GetType(Double))
        '    .Add("vgval", GetType(Double))
        '    .Add("gamma", GetType(Double))
        '    .Add("gmval", GetType(Double))
        '    .Add("uid", GetType(Integer))

        'End With
    End Sub
    Private Sub init_table()

        If Val(txtllimit.Text > 0) And Val(interval) > 0 And Val(txtmid) > 0 Then
            profit = New DataTable
            'grdprofit.Columns.Clear()
            deltatable = New DataTable
            gammatable = New DataTable
            vegatable = New DataTable
            thetatable = New DataTable
            volgatable = New DataTable
            vannatable = New DataTable
            Dim i As Integer = 0
            If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
                i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
            End If
            i += 1
            Dim j As Integer
            j = 0
            Dim start As Double
            Dim endd As Double
            start = Val(txtmid) - (Val(txtllimit.Text) * Val(interval))
            endd = Val(txtmid) + (Val(txtllimit.Text) * Val(interval))
            Dim drow As DataRow

            '######################################################################################
            grdprofit.DataSource = Nothing
            'grdprofit.Refresh()
            'grdprofit.Rows.Clear()
            If grdprofit.Columns.Count > 0 Then
                grdprofit.Columns.Clear()
            End If
            Dim style1 As New DataGridViewCellStyle
            style1.Format = "N2"
            Dim acol As DataGridViewTextBoxColumn

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grdprofit.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grdprofit.Columns.Add(acol)


            Dim grow As DataGridViewRow
            For Each grow In grdact.Rows

                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            acol = New DataGridViewTextBoxColumn
                            acol.HeaderText = gcol.HeaderText
                            acol.DataPropertyName = gcol.DataPropertyName
                            acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                            'acol.DefaultCellStyle.Format = RoundGrossMTM

                            grdprofit.Columns.Add(acol)
                            'acol.DefaultCellStyle.Format = "N2"


                        End If
                    End If
                Next
            Next


            With profit.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                .Add(c)
                            End If
                        End If
                    Next
                Next
            End With
            'FOR MINUS LIMIT(STRI) FROM MID
            Dim inter As Double = 0
            Dim int As Double = 0
            For j = 1 To Val(txtllimit.Text) + 1
                drow = profit.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                If drow("spotvalue") < 0 Then Continue For
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100) '& " %"
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)

                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                drow(gcol.DataPropertyName) = 0
                            End If
                        End If
                    Next
                Next
                'If drow("spotvalue") > 0 Then
                profit.Rows.Add(drow)
                'End If
            Next
            'FOR PLUS LIMIT(STRI) FROM MID
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = profit.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100 '& " %"
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                drow(gcol.DataPropertyName) = 0
                            End If
                        End If
                    Next
                Next
                profit.Rows.Add(drow)
            Next


            '######################################################################################

            grddelta.DataSource = Nothing
            'grddelta.Refresh()
            'grdprofit.Rows.Clear()
            If grddelta.Columns.Count > 0 Then
                grddelta.Columns.Clear()
            End If

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grddelta.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grddelta.Columns.Add(acol)

            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            If (IsDate(gcol.DataPropertyName)) Then
                                acol = New DataGridViewTextBoxColumn
                                acol.HeaderText = gcol.HeaderText
                                acol.DataPropertyName = gcol.DataPropertyName
                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                grddelta.Columns.Add(acol)
                            End If
                        End If
                    End If
                Next
            Next


            '''''''''''''''''' Detla Table Initalise

            With deltatable.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                If (IsDate(gcol.DataPropertyName)) Then
                                    .Add(c)
                                End If
                            End If
                        End If
                    Next
                Next
            End With
            inter = 0
            int = 0
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            For j = 1 To Val(txtllimit.Text) + 1
                drow = deltatable.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                drow("Percent(%)") = 0
                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100)
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
                    'drow("Percent(%)") = Math.Round(((val(drow("SPOTVALUE")) / val(txtmid)) - 1) * 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                deltatable.Rows.Add(drow)
            Next
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
            'FOR PLUS LIMIT(STRI) FROM MID
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = deltatable.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                deltatable.Rows.Add(drow)
            Next
            '######################################################################################

            '############# Gamma Table Init
            grdgamma.DataSource = Nothing
            'grdgamma.Refresh()
            'grdprofit.Rows.Clear()
            If grdgamma.Columns.Count > 0 Then
                grdgamma.Columns.Clear()
            End If

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grdgamma.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grdgamma.Columns.Add(acol)

            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            If (IsDate(gcol.DataPropertyName)) Then
                                acol = New DataGridViewTextBoxColumn
                                acol.HeaderText = gcol.HeaderText
                                acol.DataPropertyName = gcol.DataPropertyName
                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                grdgamma.Columns.Add(acol)
                            End If
                        End If
                    End If
                Next
            Next



            With gammatable.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                If (IsDate(gcol.DataPropertyName)) Then
                                    .Add(c)
                                End If
                            End If
                        End If
                    Next
                Next
            End With
            'For j = 1 To val(txtllimit.Text)
            '    drow = gammatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            inter = 0
            int = 0
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            For j = 1 To Val(txtllimit.Text) + 1
                drow = gammatable.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                drow("Percent(%)") = 0
                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100)
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                gammatable.Rows.Add(drow)
            Next
            'For j = 1 To val(txtllimit.Text)
            '    drow = gammatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = gammatable.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                gammatable.Rows.Add(drow)
            Next


            ' Vega Table INit ######################################################################################
            grdvega.DataSource = Nothing
            ' grdvega.Refresh()
            'grdprofit.Rows.Clear()
            If grdvega.Columns.Count > 0 Then
                grdvega.Columns.Clear()
            End If


            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grdvega.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grdvega.Columns.Add(acol)

            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            If (IsDate(gcol.DataPropertyName)) Then
                                acol = New DataGridViewTextBoxColumn
                                acol.HeaderText = gcol.HeaderText
                                acol.DataPropertyName = gcol.DataPropertyName
                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                grdvega.Columns.Add(acol)
                            End If
                        End If
                    End If
                Next
            Next


            With vegatable.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                If (IsDate(gcol.DataPropertyName)) Then
                                    .Add(c)
                                End If
                            End If
                        End If
                    Next
                Next
            End With
            'For j = 1 To val(txtllimit.Text)
            '    drow = vegatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            inter = 0
            int = 0
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            For j = 1 To Val(txtllimit.Text) + 1
                drow = vegatable.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                drow("Percent(%)") = 0
                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100)
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                vegatable.Rows.Add(drow)
            Next
            'For j = 1 To val(txtllimit.Text)
            '    drow = vegatable.NewRow
            'drow("spotvalue") = (val(interval) * j) + val(txtmid)
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = vegatable.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                vegatable.Rows.Add(drow)
            Next
            '######################################################################################
            grdtheta.DataSource = Nothing
            'grdtheta.Refresh()
            'grdprofit.Rows.Clear()
            If grdtheta.Columns.Count > 0 Then
                grdtheta.Columns.Clear()
            End If

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grdtheta.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grdtheta.Columns.Add(acol)

            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            If (IsDate(gcol.DataPropertyName)) Then
                                acol = New DataGridViewTextBoxColumn
                                acol.HeaderText = gcol.HeaderText
                                acol.DataPropertyName = gcol.DataPropertyName
                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                grdtheta.Columns.Add(acol)
                            End If
                        End If
                    End If
                Next
            Next


            With thetatable.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                If (IsDate(gcol.DataPropertyName)) Then
                                    .Add(c)
                                End If
                            End If
                        End If
                    Next
                Next
            End With
            'For j = 1 To val(txtllimit.Text)
            '    drow = thetatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            inter = 0
            int = 0
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            For j = 1 To Val(txtllimit.Text) + 1
                drow = thetatable.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                drow("Percent(%)") = 0
                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100)
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                thetatable.Rows.Add(drow)
            Next
            'For j = 1 To val(txtllimit.Text)
            '    drow = thetatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = thetatable.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0

                                End If
                            End If
                        End If
                    Next
                Next
                thetatable.Rows.Add(drow)
            Next

            '------------------------------------------------------------------------------------------------------
            'For Volga
            grdvolga.DataSource = Nothing
            'grdvolga.Refresh()
            'grdprofit.Rows.Clear()
            If grdvolga.Columns.Count > 0 Then
                grdvolga.Columns.Clear()
            End If

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grdvolga.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grdvolga.Columns.Add(acol)

            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            If (IsDate(gcol.DataPropertyName)) Then
                                acol = New DataGridViewTextBoxColumn
                                acol.HeaderText = gcol.HeaderText
                                acol.DataPropertyName = gcol.DataPropertyName
                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                grdvolga.Columns.Add(acol)
                            End If
                        End If
                    End If
                Next
            Next


            With volgatable.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                If (IsDate(gcol.DataPropertyName)) Then
                                    .Add(c)
                                End If
                            End If
                        End If
                    Next
                Next
            End With
            'For j = 1 To val(txtllimit.Text)
            '    drow = volgatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            inter = 0
            int = 0
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            For j = 1 To Val(txtllimit.Text) + 1
                drow = volgatable.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                drow("Percent(%)") = 0
                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100)
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                volgatable.Rows.Add(drow)
            Next
            'For j = 1 To val(txtllimit.Text)
            '    drow = volgatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = volgatable.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0

                                End If
                            End If
                        End If
                    Next
                Next
                volgatable.Rows.Add(drow)
            Next

            '------------------------------------------------------------------------------------------------------
            'For Vanna
            grdvanna.DataSource = Nothing
            'grdVanna.Refresh()
            'grdprofit.Rows.Clear()
            If grdvanna.Columns.Count > 0 Then
                grdvanna.Columns.Clear()
            End If

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grdvanna.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grdvanna.Columns.Add(acol)

            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            If (IsDate(gcol.DataPropertyName)) Then
                                acol = New DataGridViewTextBoxColumn
                                acol.HeaderText = gcol.HeaderText
                                acol.DataPropertyName = gcol.DataPropertyName
                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                grdvanna.Columns.Add(acol)
                            End If
                        End If
                    End If
                Next
            Next


            With vannatable.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                If (IsDate(gcol.DataPropertyName)) Then
                                    .Add(c)
                                End If
                            End If
                        End If
                    Next
                Next
            End With
            'For j = 1 To val(txtllimit.Text)
            '    drow = Vannatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            inter = 0
            int = 0
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            For j = 1 To Val(txtllimit.Text) + 1
                drow = vannatable.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                drow("Percent(%)") = 0
                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100)
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                vannatable.Rows.Add(drow)
            Next
            'For j = 1 To val(txtllimit.Text)
            '    drow = Vannatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = vannatable.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0

                                End If
                            End If
                        End If
                    Next
                Next
                vannatable.Rows.Add(drow)
            Next

        Else
            MsgBox("enter Value.")
        End If


    End Sub
    Private Sub init_table1()

        'If val(txtllimit.Text) > 0 And val(txtllimit.Text > 0) And val(interval) > 0 And val(txtmid) > 0 Then
        '    profit = New DataTable

        '    deltatable = New DataTable
        '    gammatable = New DataTable
        '    vegatable = New DataTable
        '    thetatable = New DataTable
        '    Dim i As Integer = 0
        '    If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
        '        i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
        '    End If
        '    Dim j As Integer
        '    j = 0
        '    Dim start As Double
        '    Dim endd As Double
        '    start = val(txtmid.Text) - (val(txtllimit.Text) * val(interval))
        '    endd = val(txtmid.Text) + (val(txtllimit.Text) * val(interval))
        '    Dim drow As DataRow
        '    '######################################################################################

        '    With profit.Columns

        '        .Add("SpotValue", GetType(Double))
        '        For Each grow As DataGridViewRow In grdact.Rows
        '            For Each gcol As DataGridViewColumn In grdact.Columns
        '                If grow.Cells(gcol.Index).Value = True Then
        '                    Dim c As String = gcol.HeaderText
        '                    .Add(c)
        '                End If
        '            Next
        '        Next
        '        'For j = 0 To i - 1
        '        '    Dim c As String = Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "" '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")
        '        '    .Add(c)
        '        'Next
        '    End With

        '    For j = 1 To val(txtllimit.Text)
        '        drow = profit.NewRow
        '        drow("spotvalue") = (val(interval) * j) + start
        '        For Each grow As DataGridViewRow In grdact.Rows
        '            For Each gcol As DataGridViewColumn In grdact.Columns
        '                If grow.Cells(gcol.Index).Value = True Then
        '                    drow(gcol.HeaderText) = 0
        '                End If
        '            Next
        '        Next
        '        'For k As Integer = 0 To i - 1
        '        '    drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
        '        'Next
        '        profit.Rows.Add(drow)
        '    Next
        '    For j = 1 To val(txtllimit.Text)
        '        drow = profit.NewRow
        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
        '        For Each grow As DataGridViewRow In grdact.Rows
        '            For Each gcol As DataGridViewColumn In grdact.Columns
        '                If grow.Cells(gcol.Index).Value = True Then
        '                    drow(gcol.HeaderText) = 0
        '                End If
        '            Next
        '        Next
        '        'For k As Integer = 0 To i - 1
        '        '    drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
        '        'Next
        '        profit.Rows.Add(drow)
        '    Next
        '    '######################################################################################

        '    With deltatable.Columns
        '        .Add("SpotValue", GetType(Double))
        '        For j = 0 To i - 1
        '            .Add(Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "") ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy"))
        '        Next
        '    End With
        '    For j = 1 To val(txtllimit.Text)
        '        drow = deltatable.NewRow
        '        drow("spotvalue") = (val(interval) * j) + start
        '        For k As Integer = 0 To i - 1
        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
        '        Next
        '        deltatable.Rows.Add(drow)
        '    Next
        '    For j = 1 To val(txtllimit.Text)
        '        drow = deltatable.NewRow
        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
        '        For k As Integer = 0 To i - 1
        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
        '        Next
        '        deltatable.Rows.Add(drow)
        '    Next
        '    '######################################################################################

        '    With gammatable.Columns
        '        .Add("SpotValue", GetType(Double))
        '        For j = 0 To i - 1
        '            .Add(Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "") '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy"))
        '        Next
        '    End With
        '    For j = 1 To val(txtllimit.Text)
        '        drow = gammatable.NewRow
        '        drow("spotvalue") = (val(interval) * j) + start
        '        For k As Integer = 0 To i - 1
        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
        '        Next
        '        gammatable.Rows.Add(drow)
        '    Next
        '    For j = 1 To val(txtllimit.Text)
        '        drow = gammatable.NewRow
        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
        '        For k As Integer = 0 To i - 1
        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
        '        Next
        '        gammatable.Rows.Add(drow)
        '    Next


        '    '######################################################################################

        '    With vegatable.Columns
        '        .Add("SpotValue", GetType(Double))
        '        For j = 0 To i - 1
        '            .Add(Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "") ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy"))
        '        Next
        '    End With
        '    For j = 1 To val(txtllimit.Text)
        '        drow = vegatable.NewRow
        '        drow("spotvalue") = (val(interval) * j) + start
        '        For k As Integer = 0 To i - 1
        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
        '        Next
        '        vegatable.Rows.Add(drow)
        '    Next
        '    For j = 1 To val(txtllimit.Text)
        '        drow = vegatable.NewRow
        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
        '        For k As Integer = 0 To i - 1
        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
        '        Next
        '        vegatable.Rows.Add(drow)
        '    Next
        '    '######################################################################################

        '    With thetatable.Columns
        '        .Add("SpotValue", GetType(Double))
        '        For j = 0 To i - 1
        '            .Add(Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "") ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy"))
        '        Next
        '    End With
        '    For j = 1 To val(txtllimit.Text)
        '        drow = thetatable.NewRow
        '        drow("spotvalue") = (val(interval) * j) + start
        '        For k As Integer = 0 To i - 1
        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
        '        Next
        '        thetatable.Rows.Add(drow)
        '    Next
        '    For j = 1 To val(txtllimit.Text)
        '        drow = thetatable.NewRow
        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
        '        For k As Integer = 0 To i - 1
        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
        '        Next
        '        thetatable.Rows.Add(drow)
        '    Next





        'Else
        '    MsgBox("enter Value")
        'End If
    End Sub
#End Region
#Region "event"
    Private Sub cmbresult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdresult.Click
        REM In Scenario window For Selected Individual Greeks Chart and GridData on One tab,According to Button press it shows chart or Griddata
        mAllCV = ""
        result(True)

        '  Me.tbcon.DrawMode = TabDrawMode.OwnerDrawFixed
    End Sub
    Private Sub result(ByVal ChangeFlg As Boolean)
        'If val(txtllimit.Text) <= 0 Then
        '    MsgBox("Enter Upper Limit")
        '    txtllimit.Focus()
        '    Exit Sub
        'End If

        If Val(txtllimit.Text) <= 0 Then
            MsgBox("Enter No of Strike +/-  ")
            txtllimit.Focus()
            Exit Sub
        End If
        If Val(interval) <= 0 Then
            MsgBox("Enter Interval.")
            txtinterval.Focus()
            Exit Sub
        End If

        'If val(txtmid) <= 0 Then
        '    MsgBox("Enter Mid Value")
        '    txtmid.Focus()
        '    Exit Sub
        'End If

        REM For First Record Show Charts
        If Val(txtmid) <= 0 Then
            CalInterval()
        End If

        If Val(txtllimit.Text > 0) And Val(interval) > 0 And Val(txtmid) > 0 Then

            init_table()
            If cal_profit() = True Then
                If ChangeFlg = True Then
                    If tbcon.SelectedTab.Name = "tbprofitchart" Then
                        SetResultButtonText(cmdresult, chtprofit, grdprofit, lblprofit, True)
                    End If
                Else
                    If chtprofit.Visible = False Then chtprofit.Visible = True
                End If
            Else
                Exit Sub
            End If
            create_chart_profit()
            create_chart_profit_Full()

            If cal_delta() = True Then
                If ChangeFlg = True Then
                    If tbcon.SelectedTab.Name = "tbchdelta" Then
                        SetResultButtonText(cmdresult, chtdelta, grddelta, lbldelta, True)
                    End If
                End If
            Else
                Exit Sub
            End If
            create_chart_delta()
            create_chart_delta_Full()
            If cal_gamma() = True Then
                If ChangeFlg = True Then
                    If tbcon.SelectedTab.Name = "tbchgamma" Then
                        SetResultButtonText(cmdresult, chtgamma, grdgamma, lblgamma, True)
                    End If
                End If
            Else
                Exit Sub
            End If
            create_chart_gamma()
            Call create_chart_gamma_Full()

            If cal_vega() = True Then
                If ChangeFlg = True Then
                    If tbcon.SelectedTab.Name = "tbchvega" Then
                        SetResultButtonText(cmdresult, chtvega, grdvega, lblvega, True)
                    End If
                End If
            Else
                Exit Sub
            End If
            create_chart_vega()
            create_chart_vega_Full()

            If cal_theta() = True Then
                If ChangeFlg = True Then
                    If tbcon.SelectedTab.Name = "tbchtheta" Then
                        SetResultButtonText(cmdresult, chttheta, grdtheta, lbltheta, True)
                    End If
                End If
            Else
                Exit Sub
            End If
            create_chart_theta()
            create_chart_theta_Full()

            If cal_volga() = True Then
                If ChangeFlg = True Then
                    If tbcon.SelectedTab.Name = "tbchvolga" Then
                        SetResultButtonText(cmdresult, chtvolga, grdvolga, lblvolga, True)
                    End If
                End If
            Else
                Exit Sub
            End If
            create_chart_volga()
            create_chart_volga_Full()


            If cal_vanna() = True Then
                If ChangeFlg = True Then
                    If tbcon.SelectedTab.Name = "tbchvanna" Then
                        SetResultButtonText(cmdresult, chtvanna, grdvanna, lblvanna, True)
                    End If
                End If
            Else
                Exit Sub
            End If
            create_chart_vanna()
            create_chart_vanna_Full()

            'If mAllCV Is Not Nothing Then
            If mAllCV.Trim = "" Then
                If CmdAllCV.Text = "Show &All Charts(F8)" Then
                    CmdAllCV.Text = "Show &All Values(F8)"
                    mAllCV = "Charts"
                Else
                    CmdAllCV.Text = "Show &All Charts(F8)"
                    mAllCV = "Values"
                End If
            End If
            'End If
            'grdprofit.DataSource = profit
            'grdprofit.Refresh()
            'grddelta.DataSource = deltatable
            'grddelta.Refresh()
            'grdgamma.DataSource = gammatable
            'grdgamma.Refresh()
            'grdvega.DataSource = vegatable
            'grdvega.Refresh()
            'grdtheta.DataSource = thetatable
            'grdtheta.Refresh()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
        'Dim analysis As New analysis
        ''analysis.MdiParent = Me
        'analysis.ShowDialog()
    End Sub

    Private Sub txtdays_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdays.KeyPress
        numonly(e)
    End Sub

    Private Sub txtllimit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtllimit.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub
    Private Sub txtllimit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtllimit.KeyPress
        numonly(e)
    End Sub
    Private Sub txtmid_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        numonly(e)
    End Sub
    Private Sub txtulimit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        numonly(e)
    End Sub

    Private Sub txtinterval_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtinterval.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub
    Private Sub txtinterval_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtinterval.KeyPress
        numonly(e)
    End Sub
    Private Sub txtratediff_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        numonly(e)
    End Sub
    Private Sub dttoday_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dttoday.Leave
        txtdays.Text = 0
        If grdtrad.Rows.Count = 2 Then
            grdtrad.Rows(0).Cells("TimeI").Value = dttoday.Value
        ElseIf frmFlg = True Then 'this flg used bcoz first need not to assign expiry date
            For Each grow As DataGridViewRow In grdtrad.Rows
                If grow.Cells("TimeI").ReadOnly = False Then
                    grow.Cells("TimeI").Value = dttoday.Value
                End If
            Next
        End If

        If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
        End If
        If dttoday.Value.Date < dtexp.Value.Date Then
            REM This code commented because of Day difference plus 1 days display
            'txtdays.Text = Val(txtdays.Text) + 1
        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
            txtdays.Text = Val(txtdays.Text) + 0.5
        End If
        create_active()
    End Sub
    Private Sub dtexp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtexp.Leave
        txtdays.Text = 0
        If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
        End If
        If dttoday.Value.Date < dtexp.Value.Date Then
            REM This code commented because of Day difference plus 1 days display
            'txtdays.Text = Val(txtdays.Text) + 1
        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
            txtdays.Text = Val(txtdays.Text) + 0.5
        End If
        create_active()
    End Sub
    Private Sub txtdays_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdays.Leave
        If txtdays.Text.Trim <> "" And txtdays.Text <> "0" Then
            dtexp.Value = DateAdd(DateInterval.Day, CInt(txtdays.Text), dttoday.Value)
        End If
        create_active()
    End Sub
    Private Sub txtinterast_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtinterast.Leave
        Mrateofinterast = Val(txtinterast.Text)
    End Sub
    Private Sub txtinterast_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtinterast.KeyPress
        numonly(e)
    End Sub

    Private Sub txtmkt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmkt.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub
    Private Sub txtmkt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmkt.KeyPress
        numonly(e)
        flgUnderline = True
    End Sub
    Private Sub txtmkt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmkt.Leave
        If txtmkt.Text.Trim = "" Then
            txtmkt.Text = 0
        End If
        If Val(txtmkt.Text) > 0 Then
            If txtinterval.Text = 0 Then
                Call CalInterval()
            End If
            'txtmid = Math.Floor(Val(txtmkt.Text))
            ''If Val(txtmid) < 100 Then
            ''    interval = 1
            ''ElseIf Val(txtmid) > 100 And Val(txtmid) < 500 Then
            ''    interval = 5
            ''ElseIf Val(txtmid) > 100 And Val(txtmid) < 1000 Then
            ''    interval = 10
            ''ElseIf Val(txtmid) > 1000 And Val(txtmid) < 10000 Then
            ''    interval = 100
            ''ElseIf Val(txtmid) > 10000 Then
            ''    interval = 500
            ''End If
            If flgUnderline = False Then Exit Sub
            If grdtrad.Rows.Count > 0 Then
                For i As Integer = 0 To grdtrad.Rows.Count - 2
                    Dim grow As DataGridViewRow
                    grow = grdtrad.Rows(i)
                    grow.Cells("spval").Value = CDbl(txtmkt.Text)
                    If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                        'If IsDBNull(grow.Cells(4).Value) Or grow.Cells(4).Value Is Nothing Or CStr(grow.Cells(4).Value) = "" Then
                        grow.Cells("last").Value = CDbl(txtmkt.Text)
                        'End If
                    End If
                    cal(7, grow, True)
                Next
                grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("spval").Value = CDbl(txtmkt.Text)
                grdtrad.EndEdit()
                ' result()
            End If
        End If
        Call cal_summary()
        flgUnderline = False
    End Sub

    Private Sub cmdcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcheck.Click
        If grdact.ColumnCount > 0 Then
            If grdact.RowCount > 0 Then
                If checkall = True Then
                    checkall = False
                    For Each col As DataGridViewColumn In grdact.Columns
                        grdact.Rows(0).Cells(col.Index).Value = False
                    Next
                    cmdcheck.Text = "CheckAll"
                Else
                    checkall = True
                    For Each col As DataGridViewColumn In grdact.Columns
                        grdact.Rows(0).Cells(col.Index).Value = True
                    Next
                    cmdcheck.Text = "ClearAll"
                End If

            End If
        End If

    End Sub
    Private Sub CmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExport.Click
        export()
    End Sub

    Private Sub chkcheck_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcheck.CheckedChanged

        If grdtrad.RowCount > 1 Then
            If chkcheck.Checked = False Then
                gcheck = False
                For i As Integer = 0 To grdtrad.RowCount - 2
                    grdtrad.Rows(i).Cells("Active").Value = False
                Next
                'For Each grow As DataGridViewRow In grdtrad.Rows
                '    grow.Cells(0).Value = False
                'Next
            Else
                gcheck = True
                For i As Integer = 0 To grdtrad.RowCount - 2
                    grdtrad.Rows(i).Cells("Active").Value = True
                Next
                'For Each grow As DataGridViewRow In grdtrad.Rows
                '    grow.Cells(0).Value = True
                'Next
            End If


        End If

    End Sub
    Private Sub cmdexp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexp.Click
        If grdtrad.Rows.Count > 1 Then
            cal_exp()
        End If
    End Sub
    Private Sub cmdimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdimport.Click
        Me.Cursor = Cursors.WaitCursor
        import()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub txtcvol_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcvol.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub

    Private Sub txtcvol_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcvol.Leave
        If txtcvol.Text.Trim = "" Then
            txtcvol.Text = 0
        End If
        If Val(txtcvol.Text) > 0 Then
            If grdtrad.Rows.Count > 0 Then
                For Each grow As DataGridViewRow In grdtrad.Rows
                    If grow.Cells("CPF").Value = "C" Then
                        If grow.Cells("lv").ReadOnly = False Then
                            grow.Cells("lv").Value = Val(txtcvol.Text)
                        End If
                        Dim mt As Integer
                        Dim mmt As Integer
                        Dim futval As Double

                        mt = (DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date))
                        mmt = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date) - 1

                        futval = Val(grow.Cells("spval").Value)
                        CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, True, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
                        cal_summary()
                    End If
                Next
                'If grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("CPF").Value = "C" Then
                REM When user changes vol in top box, it should be automatically applied to first line vol as well.
                grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("lv").Value = CDbl(txtcvol.Text)
                grdtrad.EndEdit()
                'End If
            End If
        End If
    End Sub
    Private Sub txtcvol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcvol.KeyPress
        numonly(e)
    End Sub

    Private Sub txtpvol_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpvol.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub
    Private Sub txtpvol_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpvol.Leave
        If txtpvol.Text.Trim = "" Then
            txtpvol.Text = 0
        End If
        If Val(txtpvol.Text) > 0 Then
            If grdtrad.Rows.Count > 0 Then
                For Each grow As DataGridViewRow In grdtrad.Rows
                    If grow.Cells("CPF").Value = "P" Then
                        If grow.Cells("lv").ReadOnly = False Then
                            grow.Cells("lv").Value = Val(txtpvol.Text)
                        End If
                        Dim mt As Integer
                        Dim mmt As Integer
                        Dim futval As Double

                        mt = (DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date))
                        mmt = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date) - 1

                        futval = Val(grow.Cells("spval").Value)
                        CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, False, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
                        cal_summary()
                    End If
                Next
                'grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("lv").Value = CDbl(txtpvol.Text)
                grdtrad.EndEdit()
            End If
        End If
    End Sub
    Private Sub txtpvol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpvol.KeyPress
        numonly(e)
    End Sub

    Private Sub tbcon_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles tbcon.DrawItem
        Dim g As Graphics = e.Graphics
        Dim tp As TabPage = tbcon.TabPages(e.Index)
        Dim br As System.Drawing.Brush
        Dim sf As New StringFormat
        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width + 2, e.Bounds.Height - 2)
        sf.Alignment = StringAlignment.Near
        Dim strTitle As String = tp.Text
        If tbcon.SelectedIndex = e.Index Then
            Dim f As Font = New Font(tbcon.Font.Name, tbcon.Font.Size, FontStyle.Regular, tbcon.Font.Unit)
            br = New SolidBrush(Color.Black)
            g.FillRectangle(br, e.Bounds)
            '  g.FillRectangle(br, e.Bounds)
            br = New SolidBrush(Color.White)
            g.DrawString(strTitle, f, br, r, sf)


        Else
            Dim f As Font = New Font(tbcon.Font.Name, tbcon.Font.Size, FontStyle.Regular, tbcon.Font.Unit)
            br = New SolidBrush(Color.WhiteSmoke)
            g.FillRectangle(br, e.Bounds)
            'g.FillRectangle(br, e.Bounds)
            br = New SolidBrush(Color.Black)
            g.DrawString(strTitle, f, br, r, sf)

        End If
        tp.Refresh()
    End Sub
    Private Sub txtunits_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtunits.TextChanged
        If Val(txtunits.Text) > 0 Then
            txtunits.BackColor = Color.MediumSeaGreen
            txtunits.ForeColor = Color.White
        ElseIf Val(txtunits.Text) < 0 Then
            txtunits.BackColor = Color.DarkOrange
            txtunits.ForeColor = Color.White
        Else
            txtunits.BackColor = Color.FromArgb(255, 255, 128)
            txtunits.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtdelval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdelval.TextChanged
        If Val(txtdelval.Text) > 0 Then
            txtdelval.BackColor = Color.MediumSeaGreen
            txtdelval.ForeColor = Color.White
        ElseIf Val(txtdelval.Text) < 0 Then
            txtdelval.BackColor = Color.DarkOrange
            txtdelval.ForeColor = Color.White
        Else
            txtdelval.BackColor = Color.FromArgb(255, 255, 128)
            txtdelval.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtgmval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtgmval.TextChanged
        If Val(txtgmval.Text) > 0 Then
            txtgmval.BackColor = Color.MediumSeaGreen
            txtgmval.ForeColor = Color.White
        ElseIf Val(txtgmval.Text) < 0 Then
            txtgmval.BackColor = Color.DarkOrange
            txtgmval.ForeColor = Color.White
        Else
            txtgmval.BackColor = Color.FromArgb(255, 255, 128)
            txtgmval.ForeColor = Color.Black

        End If
    End Sub
    Private Sub txtvgval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvgval.TextChanged
        If Val(txtvgval.Text) > 0 Then
            txtvgval.BackColor = Color.MediumSeaGreen
            txtvgval.ForeColor = Color.White
        ElseIf Val(txtvgval.Text) < 0 Then
            txtvgval.BackColor = Color.DarkOrange
            txtvgval.ForeColor = Color.White
        Else
            txtvgval.BackColor = Color.FromArgb(255, 255, 128)
            txtvgval.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtthval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtthval.TextChanged
        If Val(txtthval.Text) > 0 Then
            txtthval.BackColor = Color.MediumSeaGreen
            txtthval.ForeColor = Color.White
        ElseIf Val(txtthval.Text) < 0 Then
            txtthval.BackColor = Color.DarkOrange
            txtthval.ForeColor = Color.White
        Else
            txtthval.BackColor = Color.FromArgb(255, 255, 128)
            txtthval.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtunits1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtunits1.TextChanged
        If Val(txtunits1.Text) > 0 Then
            txtunits1.BackColor = Color.MediumSeaGreen
            txtunits1.ForeColor = Color.White
        ElseIf Val(txtunits1.Text) < 0 Then
            txtunits1.BackColor = Color.DarkOrange
            txtunits1.ForeColor = Color.White
        Else
            txtunits1.BackColor = Color.FromArgb(255, 255, 128)
            txtunits1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtdelval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdelval1.TextChanged
        If Val(txtdelval1.Text) > 0 Then
            txtdelval1.BackColor = Color.MediumSeaGreen
            txtdelval1.ForeColor = Color.White
        ElseIf Val(txtdelval1.Text) < 0 Then
            txtdelval1.BackColor = Color.DarkOrange
            txtdelval1.ForeColor = Color.White
        Else
            txtdelval1.BackColor = Color.FromArgb(255, 255, 128)
            txtdelval1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtgmval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtgmval1.TextChanged
        If Val(txtgmval1.Text) > 0 Then
            txtgmval1.BackColor = Color.MediumSeaGreen
            txtgmval1.ForeColor = Color.White
        ElseIf Val(txtgmval1.Text) < 0 Then
            txtgmval1.BackColor = Color.DarkOrange
            txtgmval1.ForeColor = Color.White
        Else
            txtgmval1.BackColor = Color.FromArgb(255, 255, 128)
            txtgmval1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtvgval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvgval1.TextChanged
        If Val(txtvgval1.Text) > 0 Then
            txtvgval1.BackColor = Color.MediumSeaGreen
            txtvgval1.ForeColor = Color.White
        ElseIf Val(txtvgval1.Text) < 0 Then
            txtvgval1.BackColor = Color.DarkOrange
            txtvgval1.ForeColor = Color.White
        Else
            txtvgval1.BackColor = Color.FromArgb(255, 255, 128)
            txtvgval1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtthval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtthval1.TextChanged
        If Val(txtthval1.Text) > 0 Then
            txtthval1.BackColor = Color.MediumSeaGreen
            txtthval1.ForeColor = Color.White
        ElseIf Val(txtthval1.Text) < 0 Then
            txtthval1.BackColor = Color.DarkOrange
            txtthval1.ForeColor = Color.White
        Else
            txtthval1.BackColor = Color.FromArgb(255, 255, 128)
            txtthval1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtVolgaVal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVolgaVal.TextChanged
        If Val(txtVolgaVal.Text) > 0 Then
            txtVolgaVal.BackColor = Color.MediumSeaGreen
            txtVolgaVal.ForeColor = Color.White
        ElseIf Val(txtVolgaVal.Text) < 0 Then
            txtVolgaVal.BackColor = Color.DarkOrange
            txtVolgaVal.ForeColor = Color.White
        Else
            txtVolgaVal.BackColor = Color.FromArgb(255, 255, 128)
            txtVolgaVal.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TxtVolgaVal1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtVolgaVal1.TextChanged
        If Val(TxtVolgaVal1.Text) > 0 Then
            TxtVolgaVal1.BackColor = Color.MediumSeaGreen
            TxtVolgaVal1.ForeColor = Color.White
        ElseIf Val(TxtVolgaVal1.Text) < 0 Then
            TxtVolgaVal1.BackColor = Color.DarkOrange
            TxtVolgaVal1.ForeColor = Color.White
        Else
            TxtVolgaVal1.BackColor = Color.FromArgb(255, 255, 128)
            TxtVolgaVal1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub TxtVannaVal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtVannaVal.TextChanged
        If Val(TxtVannaVal.Text) > 0 Then
            TxtVannaVal.BackColor = Color.MediumSeaGreen
            TxtVannaVal.ForeColor = Color.White
        ElseIf Val(TxtVannaVal.Text) < 0 Then
            TxtVannaVal.BackColor = Color.DarkOrange
            TxtVannaVal.ForeColor = Color.White
        Else
            TxtVannaVal.BackColor = Color.FromArgb(255, 255, 128)
            TxtVannaVal.ForeColor = Color.Black
        End If
    End Sub
    Private Sub TxtVannaVal1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtVannaVal1.TextChanged
        If Val(TxtVannaVal1.Text) > 0 Then
            TxtVannaVal1.BackColor = Color.MediumSeaGreen
            TxtVannaVal1.ForeColor = Color.White
        ElseIf Val(TxtVannaVal1.Text) < 0 Then
            TxtVannaVal1.BackColor = Color.DarkOrange
            TxtVannaVal1.ForeColor = Color.White
        Else
            TxtVannaVal1.BackColor = Color.FromArgb(255, 255, 128)
            TxtVannaVal1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtinterval_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtinterval.Leave
        REM When scenario calculation passes value as negative in ltp, then it gives an overflow in values,For this Set Calulation Of Interval And No Of Strikes, multiplication of interval and no Of Strike must be less than CMP
        If chkint.Checked = True Then
            interval = Val(txtinterval.Text) * 100
        Else
            interval = Val(txtinterval.Text)
        End If
        If Val(txtmkt.Text) > 0 And Val(txtinterval.Text) > 0 And Val(txtllimit.Text) > 0 Then
            If Val(txtinterval.Text) > Math.Round(Val(txtmkt.Text) / Val(txtllimit.Text)) Then
                MsgBox("Interval Must be Less Or Equal To " & Math.Round(Val(txtmkt.Text) / Val(txtllimit.Text), 0), MsgBoxStyle.Information)
                txtinterval.Focus()
                Exit Sub
            End If
        End If
    End Sub
#End Region
#Region "Grid Event"
    Private Sub cal(ByVal cno As Integer, ByVal currow As DataGridViewRow, Optional ByVal check As Boolean = False)
        Dim iscall As Boolean = False
        Dim zero As Double = 0
        Dim one As Double = 1
        Dim count As Integer
        count = 0
        Dim futval As Double
        Dim brate As Double = 0
        Dim srate As Double = 0
        futval = 0
        Dim sumunit As Double = 0
        Dim mt As Double = 0
        Dim mmt As Double = 0
        Dim grow As DataGridViewRow
        If check = True Then
            grow = currow
        Else
            grow = grdtrad.CurrentRow
        End If
        If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
            grow.Cells("CPF").Value = CStr("C")
        Else
            grow.Cells("CPF").Value = UCase(grow.Cells("CPF").Value)
            If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                grow.Cells("delta").Value = one
            Else
                grow.Cells("delta").Value = zero
            End If
        End If

        grdtrad.EndEdit()
        'If IsDBNull(grow.Cells(2).Value) Or Not IsDate(grow.Cells(2).Value) Then
        '    MsgBox("Enter Maturity date")
        '    Exit Sub
        'End If
        If Not IsDBNull(grow.Cells("CPF").Value) And Not grow.Cells("CPF").Value Is Nothing Then
            If UCase(grow.Cells("CPF").Value) = "F" Or UCase(grow.Cells("CPF").Value) = "E" Then
                grow.Cells("deltaval").Value = grow.Cells("units").Value
                grow.Cells("delta").Value = Math.Round(one, 2)
                grow.Cells("deltaval").Value = Math.Round(Val(grow.Cells("deltaval").Value), 2)
                grow.Cells("Strike").Value = Math.Round(zero, 2)
                If Val(grow.Cells("last").Value) = 0 Then
                    If Val(grow.Cells("spval").Value) <> 0 Then
                        grow.Cells("last").Value = Math.Round(CDbl(grow.Cells("spval").Value), 2)
                    Else
                        grow.Cells("last").Value = Math.Round(zero, 2)
                    End If
                End If
                grow.Cells("lv").Value = Math.Round(zero, 2)
                grow.Cells("gamma").Value = Math.Round(zero, 2)
                grow.Cells("gmval").Value = Math.Round(zero, 2)
                grow.Cells("vega").Value = Math.Round(zero, 2)
                grow.Cells("vgval").Value = Math.Round(zero, 2)
                grow.Cells("theta").Value = Math.Round(zero, 2)
                grow.Cells("thetaval").Value = Math.Round(zero, 2)


                grow.Cells("gamma1").Value = Math.Round(zero, 2)
                grow.Cells("gmval1").Value = Math.Round(zero, 2)
                grow.Cells("vega1").Value = Math.Round(zero, 2)
                grow.Cells("vgval1").Value = Math.Round(zero, 2)
                grow.Cells("theta").Value = Math.Round(zero, 2)
                grow.Cells("thetaval1").Value = Math.Round(zero, 2)

                grow.Cells("volga").Value = Math.Round(zero, 2)
                grow.Cells("volgaval1").Value = Math.Round(zero, 2)
                grow.Cells("vanna").Value = Math.Round(zero, 2)
                grow.Cells("vannaval1").Value = Math.Round(zero, 2)

                grdtrad.EndEdit()
            Else
                futval = Val(grow.Cells("spval").Value)
                If IsDBNull(grow.Cells("TimeI").Value) Then
                    grow.Cells("TimeI").Value = dttoday.Value.Date
                    'ElseIf grow.Cells("TimeI").Value.ToString.Trim = "" Then
                    '    grow.Cells("TimeI").Value = dttoday.Value.Date
                ElseIf grow.Cells("TimeI").Value = Nothing Then
                    grow.Cells("TimeI").Value = dttoday.Value.Date
                End If
                If IsDBNull(grow.Cells("TimeII").Value) Then
                    grow.Cells("TimeII").Value = dtexp.Value.Date
                    ' ElseIf grow.Cells("TimeII").Value.ToString.Trim = "" Then
                ElseIf grow.Cells("TimeII").Value = Nothing Then
                    grow.Cells("TimeII").Value = dtexp.Value.Date
                End If
                If CDate(grow.Cells("TimeI").Value) < CDate(grow.Cells("TimeII").Value) Then
                    mt = (DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date))
                    'mmt = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, CInt(txtdays.Text), Now.Date), CDate(grow.Cells("TimeII").Value).Date)
                    'Else
                    '    mt = DateDiff(DateInterval.Day, CDate(grow.Cells(1).Value), CDate(grow.Cells(2).Value))
                End If
                If CDate(grow.Cells("TimeI").Value).Date = CDate(grow.Cells("TimeII").Value).Date Then
                    mt = 0.5
                End If

                'mt = DateDiff(DateInterval.Day, CDate(drow("mdate")), Now())
                If grow.Cells("CPF").Value = "C" Then
                    iscall = True
                Else
                    iscall = False
                End If
                If futval <> 0 Then
                    If cno = 9 Then
                        CalDatastkprice(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, iscall, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
                        '' CalData(futval, Val(grow.Cells(5).Value), Val(grow.Cells(7).Value), mt, iscall, True, grow, Val(grow.Cells(6).Value), (Val(grow.Cells(8).Value) / 100))
                    Else
                        CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, iscall, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
                        isVolCal = True
                    End If
                End If
            End If
        End If
        txtunits.Text = 0
        txtdelval.Text = 0
        txtthval.Text = 0
        txtvgval.Text = 0
        txtgmval.Text = 0
        txtVolgaVal.Text = 0
        TxtVannaVal.Text = 0

        ''divyesh
        txtunits1.Text = 0
        txtdelval1.Text = 0
        txtthval1.Text = 0
        txtvgval1.Text = 0
        txtgmval1.Text = 0
        TxtVolgaVal1.Text = 0
        TxtVannaVal1.Text = 0

        If grdtrad.Rows.Count > 1 Then
            For Each row As DataGridViewRow In grdtrad.Rows
                If row.Cells("Active").Value = True Then
                    txtdelval.Text = Format(Val(txtdelval.Text) + Val(row.Cells("deltaval").Value), Deltastr_Val)
                    txtthval.Text = Format(Val(txtthval.Text) + Val(row.Cells("thetaval").Value), Thetastr_Val)
                    txtvgval.Text = Format(Val(txtvgval.Text) + Val(row.Cells("vgval").Value), Vegastr_Val)
                    txtgmval.Text = Format(Val(txtgmval.Text) + Val(row.Cells("gmval").Value), Gammastr_Val)
                    txtVolgaVal.Text = Format(Val(txtVolgaVal.Text) + Val(row.Cells("volgaval").Value), Volgastr_Val)
                    TxtVannaVal.Text = Format(Val(TxtVannaVal.Text) + Val(row.Cells("vannaval").Value), Vannastr_Val)

                    ''divyesh
                    txtdelval1.Text = Format(Val(txtdelval1.Text) + Val(row.Cells("deltaval1").Value), Deltastr_Val)
                    txtthval1.Text = Format(Val(txtthval1.Text) + Val(row.Cells("thetaval1").Value), Thetastr_Val)
                    txtvgval1.Text = Format(Val(txtvgval1.Text) + Val(row.Cells("vgval1").Value), Vegastr_Val)
                    txtgmval1.Text = Format(Val(txtgmval1.Text) + Val(row.Cells("gmval1").Value), Gammastr_Val)
                    TxtVolgaVal1.Text = Format(Val(TxtVolgaVal1.Text) + Val(row.Cells("volgaval1").Value), Volgastr_Val)
                    TxtVannaVal1.Text = Format(Val(TxtVannaVal1.Text) + Val(row.Cells("vannaval1").Value), Vannastr_Val)
                End If
            Next

            txtunits.Text = Math.Round(cal_profitLoss, RoundGrossMTM)
            ''divyesh
            txtunits1.Text = Math.Round(cal_FutprofitLoss, RoundGrossMTM)
        End If
    End Sub
    'Private Sub grdtrad_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdtrad.CellBeginEdit
    '    'grdtrad.EndEdit()
    '    'grdtrad.CommitEdit(DataGridViewDataErrorContexts.Commit)
    'End Sub
    Private Sub grdtrad_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtrad.CellEndEdit
        'If (e.ColumnIndex = 5 Or e.ColumnIndex = 6 Or e.ColumnIndex =9) Then
        '    cal()
        'Else
        If VarIsFirstLoad = True Then
            VarIsFirstLoad = False
            Exit Sub
        End If
        Dim zero As Double = 0
        Dim one As Double = 1
        If grdtrad.Columns(e.ColumnIndex).Name = "TimeI" Then
            'ElseIf grdtrad.Columns(e.ColumnIndex).Name = "TimeII" Then
            '    grdtrad.Rows(e.RowIndex).Cells("TimeI").Value = dttoday.Value
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "CPF" Then
            Dim grow As DataGridViewRow
            grow = grdtrad.CurrentRow
            If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
                grow.Cells("CPF").Value = "C"
            Else
                Dim arr As New ArrayList
                arr.Add("C")
                arr.Add("F")
                arr.Add("P")
                arr.Add("E")
                If Not arr.Contains(UCase(grow.Cells("CPF").Value)) Then
                    MsgBox("Enter 'C','P','F' or 'E'.")
                    grdtrad.Rows(e.RowIndex).Cells("CPF").Selected = True
                    Exit Sub
                End If
                grow.Cells("CPF").Value = UCase(grow.Cells("CPF").Value)
                If Not IsDBNull(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                    If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "C" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "P" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
                        If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
                            grdtrad.Rows(e.RowIndex).Cells("delta").Value = one
                            grdtrad.Rows(e.RowIndex).Cells("lv").Value = zero
                            grdtrad.Rows(e.RowIndex).Cells("last").Value = CDbl(txtmkt.Text)
                        Else
                            grdtrad.Rows(e.RowIndex).Cells("delta").Value = zero
                            If grow.Cells("lv").Value <> "0" Then
                                If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "C" Then
                                    If txtcvol.Text.Trim <> "" Then
                                        grow.Cells("lv").Value = Val(txtcvol.Text)
                                    Else
                                        grow.Cells("lv").Value = zero
                                    End If
                                    ' grdtrad.Rows(e.RowIndex).Cells(8).Value = val(txtcvol.Text)
                                Else
                                    If txtpvol.Text.Trim <> "" Then
                                        grow.Cells("lv").Value = Val(txtpvol.Text)
                                    Else
                                        grow.Cells("lv").Value = zero
                                    End If
                                    'grdtrad.Rows(e.RowIndex).Cells(8).Value = val(txtpvol.Text)
                                End If
                            End If
                        End If
                    Else
                        'MsgBox("Enter 'C','P' or 'F'")
                        grdtrad.Rows(e.RowIndex).Cells("CPF").Selected = True
                        Exit Sub
                    End If
                End If
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "spval" Then
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("spval").Value) Or grdtrad.Rows(e.RowIndex).Cells("spval").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("spval").Value) = "" Then
                If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
                    grdtrad.Rows(e.RowIndex).Cells("spval").Value = CDbl(txtmkt.Text)
                Else
                    grdtrad.Rows(e.RowIndex).Cells("spval").Value = zero
                End If
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "Strike" Then
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("Strike").Value) Or grdtrad.Rows(e.RowIndex).Cells("Strike").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("Strike").Value) = "" Then
                grdtrad.Rows(e.RowIndex).Cells("Strike").Value = zero
            Else
                grdtrad.Rows(e.RowIndex).Cells("Strike").Value = Math.Abs(CDbl(grdtrad.Rows(e.RowIndex).Cells("Strike").Value))
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "units" Then
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("units").Value) Or grdtrad.Rows(e.RowIndex).Cells("units").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("units").Value) = "" Then
                grdtrad.Rows(e.RowIndex).Cells("units").Value = zero
            ElseIf UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
                grdtrad.Rows(e.RowIndex).Cells("deltaval").Value = grdtrad.Rows(e.RowIndex).Cells("units").Value
                'to calculate deltaval
                cal_summary()
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "ltp" Then
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("ltp").Value) Or grdtrad.Rows(e.RowIndex).Cells("ltp").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("ltp").Value) = "" Then
                grdtrad.Rows(e.RowIndex).Cells("ltp").Value = zero
            End If
            ''divyesh
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("ltp1").Value) Or grdtrad.Rows(e.RowIndex).Cells("ltp").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("ltp1").Value) = "" Then
                grdtrad.Rows(e.RowIndex).Cells("ltp1").Value = zero
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "last" Then
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("last").Value) Or grdtrad.Rows(e.RowIndex).Cells("last").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("last").Value) = "" Then
                grdtrad.Rows(e.RowIndex).Cells("last").Value = zero
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "lv" Then
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("lv").Value) Or grdtrad.Rows(e.RowIndex).Cells("lv").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("lv").Value) = "" Then
                zero = 0.01
                grdtrad.Rows(e.RowIndex).Cells("lv").Value = zero
                zero = 0
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "Active" Then

            Call cal_summary()

            grdtrad.EndEdit()

            Dim grow As DataGridViewRow
            grow = grdtrad.CurrentRow

            If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
                grow.Cells("CPF").Value = CStr("C")
            End If
            If IsDBNull(grow.Cells("spval").Value) Or grow.Cells("spval").Value Is Nothing Then
                grdtrad.Rows(e.RowIndex).Cells("spval").Value = CDbl(txtmkt.Text)
            Else
                grow.Cells("spval").Value = CDbl(grow.Cells("spval").Value)
            End If
            If IsDBNull(grow.Cells("Strike").Value) Or grow.Cells("Strike").Value Is Nothing Then
                grow.Cells("Strike").Value = zero
            End If
            If IsDBNull(grow.Cells("units").Value) Or grow.Cells("units").Value Is Nothing Then
                grow.Cells("units").Value = zero
            End If
            If IsDBNull(grow.Cells("last").Value) Or grow.Cells("last").Value Is Nothing Then
                grow.Cells("last").Value = zero
            End If
            If IsDBNull(grow.Cells("lv").Value) Or grow.Cells("lv").Value Is Nothing Then
                If txtcvol.Text.Trim <> "" Then
                    grow.Cells("lv").Value = CDbl(txtcvol.Text)
                Else
                    grow.Cells("lv").Value = zero
                End If
            End If
            If IsDBNull(grow.Cells("delta").Value) Or grow.Cells("delta").Value Is Nothing Then
                grow.Cells("delta").Value = zero
            End If
            If IsDBNull(grow.Cells("deltaval").Value) Or grow.Cells("deltaval").Value Is Nothing Then
                grow.Cells("deltaval").Value = zero
            End If
            If IsDBNull(grow.Cells("gamma").Value) Or grow.Cells("gamma").Value Is Nothing Then
                grow.Cells("gamma").Value = zero
            End If
            If IsDBNull(grow.Cells("gmval").Value) Or grow.Cells("gmval").Value Is Nothing Then
                grow.Cells("gmval").Value = zero
            End If
            If IsDBNull(grow.Cells("vega").Value) Or grow.Cells("vega").Value Is Nothing Then
                grow.Cells("vega").Value = zero
            End If
            If IsDBNull(grow.Cells("vgval").Value) Or grow.Cells("vgval").Value Is Nothing Then
                grow.Cells("vgval").Value = zero
            End If
            If IsDBNull(grow.Cells("theta").Value) Or grow.Cells("theta").Value Is Nothing Then
                grow.Cells("theta").Value = zero
            End If
            If IsDBNull(grow.Cells("thetaval").Value) Or grow.Cells("thetaval").Value Is Nothing Then
                grow.Cells("thetaval").Value = zero
            End If

            If IsDBNull(grow.Cells("volga").Value) Or grow.Cells("volga").Value Is Nothing Then
                grow.Cells("volga").Value = zero
            End If
            If IsDBNull(grow.Cells("volgaval").Value) Or grow.Cells("volgaval").Value Is Nothing Then
                grow.Cells("volgaval").Value = zero
            End If
            If IsDBNull(grow.Cells("vanna").Value) Or grow.Cells("vanna").Value Is Nothing Then
                grow.Cells("vanna").Value = zero
            End If
            If IsDBNull(grow.Cells("vannaval").Value) Or grow.Cells("vannaval").Value Is Nothing Then
                grow.Cells("vannaval").Value = zero
            End If

            ''divyesh
            If IsDBNull(grow.Cells("delta1").Value) Or grow.Cells("delta1").Value Is Nothing Then
                grow.Cells("delta1").Value = zero
            End If
            If IsDBNull(grow.Cells("gamma1").Value) Or grow.Cells("gamma1").Value Is Nothing Then
                grow.Cells("gamma1").Value = zero
            End If
            If IsDBNull(grow.Cells("vega1").Value) Or grow.Cells("vega1").Value Is Nothing Then
                grow.Cells("vega1").Value = zero
            End If
            If IsDBNull(grow.Cells("theta1").Value) Or grow.Cells("theta1").Value Is Nothing Then
                grow.Cells("theta1").Value = zero
            End If
            If IsDBNull(grow.Cells("vgval1").Value) Or grow.Cells("vgval").Value Is Nothing Then
                grow.Cells("vgval1").Value = zero
            End If
            If IsDBNull(grow.Cells("gmval1").Value) Or grow.Cells("gmval").Value Is Nothing Then
                grow.Cells("gmval1").Value = zero
            End If
            If IsDBNull(grow.Cells("thetaval1").Value) Or grow.Cells("thetaval").Value Is Nothing Then
                grow.Cells("thetaval1").Value = zero
            End If
            If IsDBNull(grow.Cells("deltaval1").Value) Or grow.Cells("deltaval").Value Is Nothing Then
                grow.Cells("deltaval1").Value = zero
            End If

            If IsDBNull(grow.Cells("volgaval1").Value) Or grow.Cells("volgaval").Value Is Nothing Then
                grow.Cells("volgaval1").Value = zero
            End If
            If IsDBNull(grow.Cells("vannaval1").Value) Or grow.Cells("vannaval").Value Is Nothing Then
                grow.Cells("vannaval1").Value = zero
            End If


            grow.Cells("uid").Value = grdtrad.Rows.Count - 1
            'grdtrad.Refresh()
            'to attch context menu to new row
            'If (grdtrad.Rows(e.RowIndex).Cells("ltp").Value = 0) Then

            'REM 1 create contextmenustrip from vol column cells
            ReDim contMenu(grdtrad.Rows.Count - 1)
            contMenu(grdtrad.Rows.Count - 1) = New ContextMenuStrip
            'contMenu(grdtrad.Rows.Count - 1).ShowImageMargin = False
            Dim item As New ToolStripMenuItem("Unfreeze")
            ' item.CheckOnClick = True
            item.Tag = grdtrad.Rows.Count - 1
            AddHandler item.Click, AddressOf freezVol
            AddHandler contMenu(grdtrad.Rows.Count - 1).Opening, AddressOf contMenuOpen
            contMenu(grdtrad.Rows.Count - 1).Items.Add(item)
            grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("lv").ContextMenuStrip = contMenu(grdtrad.Rows.Count - 1)
            REM 1: end

            'REM 1 create contextmenustrip from Expiry column cells
            ReDim contMenuExpiry(grdtrad.Rows.Count - 1)
            contMenuExpiry(grdtrad.Rows.Count - 1) = New ContextMenuStrip
            'contMenuExpiry(grdtrad.Rows.Count - 1).ShowImageMargin = False
            item = New ToolStripMenuItem("Unfreeze")
            ' item.CheckOnClick = True
            item.Tag = grdtrad.Rows.Count - 1
            AddHandler item.Click, AddressOf freezExpiry
            AddHandler contMenu(grdtrad.Rows.Count - 1).Opening, AddressOf contMenuOpenExpiry
            contMenuExpiry(grdtrad.Rows.Count - 1).Items.Add(item)
            grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("TimeII").ContextMenuStrip = contMenuExpiry(grdtrad.Rows.Count - 1)
            REM 1: end
        End If
        grdtrad.EndEdit()
        If grdtrad.Columns(e.ColumnIndex).Name = "lv" Then
            If UCase(grdtrad.CurrentRow.Cells("CPF").Value) = "F" Or UCase(grdtrad.CurrentRow.Cells("CPF").Value) = "E" Then
                If Val(grdtrad.CurrentRow.Cells("last").Value) <> 0 Then
                    cal(8, grdtrad.CurrentRow)
                End If
            Else
                If Val(grdtrad.CurrentRow.Cells("Strike").Value) > 0 Then
                    cal(8, grdtrad.CurrentRow)
                End If
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "last" Then
            cal(9, grdtrad.CurrentRow)
        Else
            If Val(grdtrad.CurrentRow.Cells("Strike").Value) > 0 And Val(grdtrad.CurrentRow.Cells("lv").Value) <> 0 Then
                cal(0, grdtrad.CurrentRow)
            End If

        End If
        'result(False)
    End Sub
    Private Sub grdtrad_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdtrad.EditingControlShowing
        AddHandler e.Control.KeyPress, AddressOf CheckCell
        'grdtrad.EndEdit()
        'AddHandler e.Control.KeyDown, AddressOf cellkeydown
    End Sub
    Private Sub grdtrad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdtrad.KeyDown
        If e.KeyCode = Keys.F2 Then
            If (grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "TimeI" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "TimeII" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "CPF" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "spval" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "Strike" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "units" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "ltp") Then
                grdtrad.CurrentCell.KeyEntersEditMode(e)
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            If grdtrad.CurrentRow.IsNewRow Then
                grdtrad.CancelEdit()
            Else
                grdtrad.Rows.RemoveAt(grdtrad.CurrentRow.Index)
            End If
            'grdtrad.Rows.RemoveAt(grdtrad.CurrentRow.Index)
            grdtrad.CancelEdit()
            'grdtrad.BindingContext.Item(grdtrad.DataSource).CancelCurrentEdit()

        ElseIf e.KeyCode = Keys.Tab Then
            If grdtrad.CurrentCell.OwningColumn.Index = 4 Then
                If grdtrad.CurrentRow.Cells("CPF").Value = "F" Or grdtrad.CurrentRow.Cells("CPF").Value = "E" Then
                    Me.grdtrad.CurrentCell = _
                                             Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index).Cells("units")

                    e.SuppressKeyPress = True
                End If
                'ElseIf grdtrad.CurrentCell.OwningColumn.Index = 9 Then
                '    If grdtrad.CurrentRow.Cells(3).Value = "F" Then
                '        Me.grdtrad.CurrentCell = _
                '                                  Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells(0)

                '        e.SuppressKeyPress = True
                '    End If
            ElseIf grdtrad.CurrentCell.OwningColumn.Index = 10 Then
                If (Me.grdtrad.CurrentRow.Index + 1 <= Me.grdtrad.Rows.Count - 1) Then
                    Me.grdtrad.CurrentCell = Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells("Active")
                    e.SuppressKeyPress = True
                Else
                    Me.grdtrad.CurrentCell = Me.grdtrad.Rows(0).Cells("Active")
                End If
            End If
        End If
    End Sub
    Private Sub grdtrad_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdtrad.DataError

    End Sub
    Private Sub grdtrad_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtrad.CellValueChanged
        If VarIsFrmLoad = False Then Exit Sub
        If (grdtrad.Columns(e.ColumnIndex).Name = "delta" Or grdtrad.Columns(e.ColumnIndex).Name = "deltaval" Or grdtrad.Columns(e.ColumnIndex).Name = "gamma" Or grdtrad.Columns(e.ColumnIndex).Name = "gmval" Or grdtrad.Columns(e.ColumnIndex).Name = "vega" Or grdtrad.Columns(e.ColumnIndex).Name = "vgval" Or grdtrad.Columns(e.ColumnIndex).Name = "theta" Or grdtrad.Columns(e.ColumnIndex).Name = "thetaval" Or grdtrad.Columns(e.ColumnIndex).Name = "volga" Or grdtrad.Columns(e.ColumnIndex).Name = "volgaval" Or grdtrad.Columns(e.ColumnIndex).Name = "vanna" Or grdtrad.Columns(e.ColumnIndex).Name = "vannaval") And e.RowIndex > -1 Then
            'grdtrad.Columns(e.ColumnIndex).Name = "TimeII" Or
            If Val(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf Val(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.DarkBlue
            Else
                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
        End If
        If (grdtrad.Columns(e.ColumnIndex).Name = "TimeII") Then
            If (IsDate(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False) Then
                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dtexp.Value
                'grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
                ' MsgBox("Please Enter Proper Date...")
                ' grdtrad.Rows(e.RowIndex).Cells("TimeI").Value = dttoday.Value
            End If
        End If

        If (grdtrad.Columns(e.ColumnIndex).Name = "Active") Then
            'If (grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True) Then
            grdtrad.EndEdit()
            Call cal_summary()
            'End If
        End If

        If (grdtrad.Columns(e.ColumnIndex).Name = "spval") Or (grdtrad.Columns(e.ColumnIndex).Name = "Strike") Then
            'grdtrad.EndEdit()
        End If

        If grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "c" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Yellow
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Yellow


        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "p" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LimeGreen
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LimeGreen

        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "f" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Orange
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.Orange
        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "e" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LightPink
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Pink
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Pink
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Pink
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Pink
            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LightPink

        End If
        If Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) < 0 Then
            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.Red
        ElseIf Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) > 0 Then
            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.SkyBlue
        Else
            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.White
        End If

        grdtrad.Rows(e.RowIndex).Cells("ltp").Style.Format = "##0.00"
        grdtrad.Rows(e.RowIndex).Cells("last").Style.Format = "##0.00"

        grdtrad.Rows(e.RowIndex).Cells("delta").Style.Format = Deltastr
        grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.Format = Deltastr_Val

        grdtrad.Rows(e.RowIndex).Cells("gamma").Style.Format = Gammastr
        grdtrad.Rows(e.RowIndex).Cells("gmval").Style.Format = Gammastr_Val

        grdtrad.Rows(e.RowIndex).Cells("vega").Style.Format = Vegastr
        grdtrad.Rows(e.RowIndex).Cells("vgval").Style.Format = Vegastr_Val

        grdtrad.Rows(e.RowIndex).Cells("theta").Style.Format = Thetastr
        grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.Format = Thetastr_Val

        grdtrad.Rows(e.RowIndex).Cells("volga").Style.Format = Volgastr
        grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.Format = Volgastr_Val

        grdtrad.Rows(e.RowIndex).Cells("vanna").Style.Format = Vannastr
        grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.Format = Vannastr_Val


    End Sub
    Private Sub grdtrad_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdtrad.RowsAdded
        If VarIsFrmLoad = False Then Exit Sub
        Dim zero As Double = 0

        grdtrad.Rows(e.RowIndex).Cells("TimeI").Value = dttoday.Value
        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dtexp.Value

        If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("spval").Value) Or grdtrad.Rows(e.RowIndex).Cells("spval").Value Is Nothing Then
            If txtmkt.Text.Trim <> "" Then
                grdtrad.Rows(e.RowIndex).Cells("spval").Value = CDbl(txtmkt.Text)
            Else
                grdtrad.Rows(e.RowIndex).Cells("spval").Value = zero
            End If
        End If

        'grdtrad.EndEdit()

        Dim grow As DataGridViewRow
        grow = grdtrad.Rows(e.RowIndex)

        If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
            grow.Cells("CPF").Value = "C"
        End If
        If IsDBNull(grow.Cells("spval").Value) Or grow.Cells("spval").Value Is Nothing Then
            grdtrad.Rows(e.RowIndex).Cells("spval").Value = zero
        Else
            grow.Cells("spval").Value = CDbl(grow.Cells("spval").Value)
        End If
        If IsDBNull(grow.Cells("Strike").Value) Or grow.Cells("Strike").Value Is Nothing Then
            grow.Cells("Strike").Value = zero
        End If
        If IsDBNull(grow.Cells("units").Value) Or grow.Cells("units").Value Is Nothing Then
            grow.Cells("units").Value = zero
        End If
        If IsDBNull(grow.Cells("last").Value) Or grow.Cells("last").Value Is Nothing Then
            grow.Cells("last").Value = zero
        End If
        If IsDBNull(grow.Cells("lv").Value) Or grow.Cells("lv").Value Is Nothing Then
            If txtcvol.Text.Trim <> "" Then
                grow.Cells("lv").Value = Val(txtcvol.Text)
            Else
                grow.Cells("lv").Value = zero
            End If
        End If
        If IsDBNull(grow.Cells("delta").Value) Or grow.Cells("delta").Value Is Nothing Then
            grow.Cells("delta").Value = zero
        End If
        If IsDBNull(grow.Cells("deltaval").Value) Or grow.Cells("deltaval").Value Is Nothing Then
            grow.Cells("deltaval").Value = zero
        End If
        If IsDBNull(grow.Cells("gamma").Value) Or grow.Cells("gamma").Value Is Nothing Then
            grow.Cells("gamma").Value = zero
        End If
        If IsDBNull(grow.Cells("gmval").Value) Or grow.Cells("gmval").Value Is Nothing Then
            grow.Cells("gmval").Value = zero
        End If
        If IsDBNull(grow.Cells("vega").Value) Or grow.Cells("vega").Value Is Nothing Then
            grow.Cells("vega").Value = zero
        End If
        If IsDBNull(grow.Cells("vgval").Value) Or grow.Cells("vgval").Value Is Nothing Then
            grow.Cells("vgval").Value = zero
        End If
        If IsDBNull(grow.Cells("theta").Value) Or grow.Cells("theta").Value Is Nothing Then
            grow.Cells("theta").Value = zero
        End If
        If IsDBNull(grow.Cells("thetaval").Value) Or grow.Cells("thetaval").Value Is Nothing Then
            grow.Cells("thetaval").Value = zero
        End If
        If IsDBNull(grow.Cells("volga").Value) Or grow.Cells("volga").Value Is Nothing Then
            grow.Cells("volga").Value = zero
        End If
        If IsDBNull(grow.Cells("volgaval").Value) Or grow.Cells("volgaval").Value Is Nothing Then
            grow.Cells("volgaval").Value = zero
        End If
        If IsDBNull(grow.Cells("vanna").Value) Or grow.Cells("vanna").Value Is Nothing Then
            grow.Cells("vanna").Value = zero
        End If
        If IsDBNull(grow.Cells("vannaval").Value) Or grow.Cells("vannaval").Value Is Nothing Then
            grow.Cells("vannaval").Value = zero
        End If

        'If (grdtrad.Columns(e.ColumnIndex).Name = "Dummy") And e.RowIndex > -1 Then
        'grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "1"
        'End If
        'grdtrad.Rows(e.RowIndex).Cells("Dummy").Value = 1

        grow.Cells("uid").Value = grdtrad.Rows.Count - 1
    End Sub
    Private Sub grdtrad_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdtrad.CellFormatting
        If grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "c" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Yellow
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Yellow

        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "p" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LimeGreen
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LimeGreen

        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "f" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Orange
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.Orange
        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "e" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LightPink
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LightPink
        End If
        If Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) < 0 Then
            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.Red
        ElseIf Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) > 0 Then
            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.SkyBlue
        Else
            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.White
        End If

        grdtrad.Rows(e.RowIndex).Cells("ltp").Style.Format = "##0.00"
        grdtrad.Rows(e.RowIndex).Cells("last").Style.Format = "##0.00"

        grdtrad.Rows(e.RowIndex).Cells("delta").Style.Format = Deltastr
        grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.Format = Deltastr_Val

        grdtrad.Rows(e.RowIndex).Cells("gamma").Style.Format = Gammastr
        grdtrad.Rows(e.RowIndex).Cells("gmval").Style.Format = Gammastr_Val

        grdtrad.Rows(e.RowIndex).Cells("vega").Style.Format = Vegastr
        grdtrad.Rows(e.RowIndex).Cells("vgval").Style.Format = Vegastr_Val

        grdtrad.Rows(e.RowIndex).Cells("theta").Style.Format = Thetastr
        grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.Format = Thetastr_Val

        grdtrad.Rows(e.RowIndex).Cells("volga").Style.Format = Volgastr
        grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.Format = Volgastr_Val

        grdtrad.Rows(e.RowIndex).Cells("vanna").Style.Format = Vannastr
        grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.Format = Vannastr_Val

    End Sub

    Private Sub grdprofit_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdprofit.CellDoubleClick
        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
            fill_result(0, e.RowIndex, e.ColumnIndex)
            Dim res As New resultfrm
            res.temptable = rtable
            res.ShowDialog()
        End If
    End Sub
    Private Sub grdprofit_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdprofit.CellFormatting
        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
            If Val(grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf Val(grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
            Else
                grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
            'grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N2"
            Dim d As Double = Double.Parse(e.Value.ToString)
            Dim str As String = "N" & RoundGrossMTM
            e.Value = d.ToString(str)
            ' grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N2"
        End If

    End Sub
    Private Sub grddelta_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grddelta.CellFormatting
        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
            If Val(grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf Val(grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
            Else
                grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
            '  grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = Deltastr_Val
            Dim d As Double = Double.Parse(e.Value.ToString)
            Dim str As String = "N" & roundDelta_Val
            e.Value = d.ToString(str)
        End If
    End Sub
    Private Sub grdgamma_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdgamma.CellFormatting
        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
            If Val(grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf Val(grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
            Else
                grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
            'grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = Gammastr_Val
            Dim d As Double = Double.Parse(e.Value.ToString)
            Dim str As String = "N" & roundGamma_Val
            e.Value = d.ToString(str)

        End If
    End Sub
    Private Sub grdvega_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdvega.CellFormatting
        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
            If Val(grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf Val(grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
            Else
                grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
            'grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = Vegastr_Val
            Dim d As Double = Double.Parse(e.Value.ToString)
            Dim str As String = "N" & roundVega_Val
            e.Value = d.ToString(str)
        End If
    End Sub
    Private Sub grdtheta_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdtheta.CellFormatting
        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
            If Val(grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf Val(grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
            Else
                grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
            ' grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = Thetastr_Val
            Dim d As Double = Double.Parse(e.Value.ToString)
            Dim str As String = "N" & roundTheta_Val
            e.Value = d.ToString(str)
        End If
    End Sub
    Private Sub grdvolga_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdvolga.CellFormatting
        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
            If Val(grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf Val(grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
            Else
                grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
            Dim d As Double = Double.Parse(e.Value.ToString)
            Dim str As String = "N" & roundVolga_Val
            e.Value = d.ToString(str)
        End If
    End Sub
    Private Sub grdvanna_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdvanna.CellFormatting
        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
            If Val(grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf Val(grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
            Else
                grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
            Dim d As Double = Double.Parse(e.Value.ToString)
            Dim str As String = "N" & roundVanna_Val
            e.Value = d.ToString(str)
        End If
    End Sub
    Private Sub cellkeydown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Tab Then
            If grdtrad.CurrentCell.OwningColumn.Index = 4 Then
                If grdtrad.CurrentRow.Cells("CPF").Value = "F" Then
                    Me.grdtrad.CurrentCell = _
                                              Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index).Cells("units")

                    e.SuppressKeyPress = True
                End If
            ElseIf grdtrad.CurrentCell.OwningColumn.Index = 6 Then
                If grdtrad.CurrentRow.Cells("CPF").Value = "F" Then
                    Me.grdtrad.CurrentCell = _
                                              Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells("Active")
                    e.SuppressKeyPress = True
                End If
            End If

        End If
    End Sub
    Private Sub CheckCell(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If FirstDgt = 0 Then
            FirstDgt = Val(e.KeyChar.ToString)
        End If
        'Dim KeyAscii As Short = Asc(e.KeyChar)
        Dim col As Integer
        col = grdtrad.CurrentCell.ColumnIndex
        Select Case col
            Case 2
                dateonly(e)
            Case 4
                numonly(e)
            Case 5
                numonly(e)
            Case 6
                numonly(e)

            Case 7
                numonly(e)
            Case 8
                numonly(e)
            Case 10
                numonly(e)
            Case 3
                Dim arr As New ArrayList
                arr.Add(67)
                arr.Add(70)
                arr.Add(80)
                arr.Add(69)
                arr.Add(8)

                If Not arr.Contains(Asc(UCase(e.KeyChar))) Then
                    e.Handled = True
                End If


        End Select
        '... 

        '... code to check the input

        '... 

        'If KeyAscii = 0 Then

        '    e.Handled = True

        'End If

    End Sub

#End Region
#Region "Method"
    Dim profitarr As New ArrayList
    Dim thetaarr As New ArrayList
    Dim deltaarr As New ArrayList
    Dim gammaarr As New ArrayList
    Dim vegaarr As New ArrayList
    Dim volgaarr As New ArrayList
    Dim vannaarr As New ArrayList
    Private Sub CalDatastkprice(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal mT As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal grow As DataGridViewRow, ByVal qty As Double, ByVal lv As Double)
        Try
            Dim mDelta As Double
            Dim mGama As Double
            Dim mVega As Double
            Dim mThita As Double
            Dim mVolga As Double
            Dim mVanna As Double
            Dim mRah As Double
            Dim mVolatility As Double
            Dim tmpcpprice As Double = 0
            tmpcpprice = cpprice

            Dim mD1 As Double
            Dim mD2 As Double

            'Dim mIsCal As Boolean
            'Dim mIsPut As Boolean
            'Dim mIsFut As Boolean

            mDelta = 0
            mGama = 0
            mVega = 0
            mThita = 0
            mVolga = 0
            mVanna = 0
            mRah = 0
            mD1 = 0
            mD2 = 0

            Dim _mt As Double
            'IF MATURITY IS 0 THEN _MT = 0.00001 
            If mT = 0 Then
                _mt = 0.0001
            Else
                _mt = (mT) / 365

            End If
            mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
            'mVolatility = lv
            Try

                'If Not mIsFut Then
                mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))

                mGama = mGama + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))

                mVega = mVega + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))

                mThita = mThita + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))

                mRah = mRah + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))

                mD1 = mD1 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt)
                mD2 = mD2 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt)

                mVolga = mVolga + CalVolga(mVega, mD1, mD2, mVolatility)
                mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mt)

                'Else
                ''mDelta = mDelta + (1 * drow("Qty"))
                'End If



                grow.Cells("lv").Value = Math.Round(mVolatility * 100, 2)
                grow.Cells("delta").Value = Math.Round(mDelta, roundDelta)

                grow.Cells("deltaval").Value = Math.Round(mDelta * qty, roundDelta_Val)

                grow.Cells("gamma").Value = Math.Round(mGama, roundGamma)
                grow.Cells("gmval").Value = Math.Round(mGama * qty, roundGamma_Val)
                grow.Cells("vega").Value = Math.Round(mVega, roundVega)
                grow.Cells("vgval").Value = Math.Round(mVega * qty, roundVega_Val)
                grow.Cells("theta").Value = Math.Round(mThita, roundTheta)
                grow.Cells("thetaval").Value = Math.Round(mThita * qty, roundTheta_Val)
                grow.Cells("volga").Value = Math.Round(mVolga, roundVolga)
                grow.Cells("volgaval").Value = Math.Round(mVolga * qty, roundVolga_Val)
                grow.Cells("vanna").Value = Math.Round(mVanna, roundVanna)
                grow.Cells("vannaval").Value = Math.Round(mVanna * qty, roundVanna_Val)

                'grow.Cells(8).Value = Math.Round(mVolatility * 100, 2)
                'grow.cells(10).value = Format(mDelta, Deltastr)
                'grow.cells(11).value = Format(mDelta * qty, Deltastr_Val)
                'grow.cells(12).value = Format(mGama, Gammastr)
                'grow.cells(13).value = Format(mGama * qty, Gammastr_Val)
                'grow.cells(14).value = Format(mVega, Vegastr)
                'grow.cells(15).value = Format(mVega * qty, Vegastr_Val)
                'grow.cells(16).value = Format(mThita, Thetastr)
                'grow.cells(17).value = Format(mThita * qty, Thetastr_Val)
                grdtrad.EndEdit()
            Catch ex As Exception

            End Try
        Catch ex As Exception
            ' MsgBox(ex.ToString)
        End Try


        ''MsgBox(mDelta)

    End Sub
    Private Sub CalData(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal mT As Double, ByVal mmT As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal grow As DataGridViewRow, ByVal qty As Double, ByVal lv As Double)
        Dim mDelta As Double
        Dim mGama As Double
        Dim mVega As Double
        Dim mThita As Double
        Dim mVolga As Double
        Dim mVanna As Double
        Dim mRah As Double

        Dim mD1 As Double
        Dim mD2 As Double
        Dim mD11 As Double
        Dim mD21 As Double

        ''divyesh
        Dim mDelta1 As Double
        Dim mGama1 As Double
        Dim mVega1 As Double
        Dim mThita1 As Double
        Dim mVolga1 As Double
        Dim mVanna1 As Double
        Dim mRah1 As Double

        Dim mVolatility As Double
        Dim tmpcpprice As Double = 0
        Dim tmpcpprice1 As Double = 0

        Dim cpprice1 As Double = 0

        tmpcpprice = cpprice

        tmpcpprice1 = cpprice1

        'Dim mIsCal As Boolean
        'Dim mIsPut As Boolean
        'Dim mIsFut As Boolean

        mDelta = 0
        mGama = 0
        mVega = 0
        mThita = 0
        mVolga = 0
        mVanna = 0
        mRah = 0

        mDelta1 = 0
        mGama1 = 0
        mVega1 = 0
        mThita1 = 0
        mVolga1 = 0
        mVanna1 = 0
        mRah1 = 0

        Dim _mt, _mt1 As Double
        'IF MATURITY IS 0 THEN _MT = 0.00001 
        If mT = 0 Then
            _mt = 0.0001
            _mt1 = 0.00001
        Else
            _mt = (mT) / 365
            _mt1 = (mmT) / 365
        End If
        'mVolatility = mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
        mVolatility = lv
        'mVolatility1 = lv
        Try
            'If Not mIsFut Then
            tmpcpprice = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 0))
            mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))
            mGama = mGama + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))
            mVega = mVega + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))
            mThita = mThita + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))
            mRah = mRah + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))

            mD1 = mD1 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt)
            mD2 = mD2 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt)

            mVolga = mVolga + CalVolga(mVega, mD1, mD2, mVolatility)
            mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mt)


            tmpcpprice1 = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 0))
            mDelta1 = mDelta1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 1))
            mGama1 = mGama1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 2))
            mVega1 = mVega1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 3))
            mThita1 = mThita1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 4))
            mRah1 = mRah1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 5))


            mD11 = mD11 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt1)
            mD21 = mD21 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt1)

            mVolga1 = mVolga1 + CalVolga(mVega1, mD11, mD21, mVolatility)
            mVanna1 = mVanna1 + CalVanna(futval, mVega, mD11, mD21, mVolatility, _mt1)


            'Else
            ''mDelta = mDelta + (1 * drow("Qty"))
            'End If

            'grow.Cells(9).Value = Math.Round(tmpcpprice, round)
            'grow.cells(10).value = Math.Round(mDelta, round)
            'grow.cells(11).value = Math.Round(mDelta * qty, round)
            'grow.cells(12).value = Math.Round(mThita, round)
            'grow.cells(13).value = Math.Round(mThita * qty, round)
            'grow.cells(14).value = Math.Round(mVega, round)
            'grow.cells(15).value = Math.Round(mVega * qty, round)
            'grow.cells(16).value = Math.Round(mGama, round)
            'grow.cells(17).value = Math.Round(mGama * qty, round)

            grow.Cells("last").Value = Math.Round(tmpcpprice, 2)
            grow.Cells("delta").Value = Math.Round(mDelta, roundDelta)
            grow.Cells("deltaval").Value = Math.Round(mDelta * qty, roundDelta_Val)
            grow.Cells("gamma").Value = Math.Round(mGama, roundGamma)
            grow.Cells("gmval").Value = Math.Round(mGama * qty, roundGamma_Val)
            grow.Cells("vega").Value = Math.Round(mVega, roundVega)
            grow.Cells("vgval").Value = Math.Round(mVega * qty, roundVega_Val)
            grow.Cells("theta").Value = Math.Round(mThita, roundTheta)
            grow.Cells("thetaval").Value = Math.Round(mThita * qty, roundTheta_Val)

            grow.Cells("volga").Value = Math.Round(mVolga, roundVolga)
            grow.Cells("volgaval").Value = Math.Round(mVolga * qty, roundVolga_Val)
            grow.Cells("vanna").Value = Math.Round(mVanna, roundTheta)
            grow.Cells("vannaval").Value = Math.Round(mVanna * qty, roundVanna_Val)


            grow.Cells("ltp1").Value = Math.Round(tmpcpprice1, 2)
            grow.Cells("delta1").Value = Math.Round(mDelta1, roundDelta)
            grow.Cells("deltaval1").Value = Math.Round(mDelta1 * qty, roundDelta_Val)
            grow.Cells("gamma1").Value = Math.Round(mGama1, roundDelta)
            grow.Cells("gmval1").Value = Math.Round(mGama1 * qty, roundGamma_Val)
            grow.Cells("vega1").Value = Math.Round(mVega1, roundDelta)
            grow.Cells("vgval1").Value = Math.Round(mVega1 * qty, roundVega_Val)
            grow.Cells("theta1").Value = Math.Round(mThita1, roundDelta)
            grow.Cells("thetaval1").Value = Math.Round(mThita1 * qty, roundTheta_Val)

            grow.Cells("volga1").Value = Math.Round(mVolga1, roundVolga)
            grow.Cells("volgaval1").Value = Math.Round(mVolga1 * qty, roundVolga_Val)
            grow.Cells("vanna1").Value = Math.Round(mVanna1, roundVanna)
            grow.Cells("vannaval1").Value = Math.Round(mVanna1 * qty, roundVanna_Val)


            'grow.Cells(9).Value = Math.Round(tmpcpprice, 2)
            'grow.cells(10).value = Format(mDelta, Deltastr)
            'grow.cells(11).value = Format(mDelta * qty, Deltastr_Val)
            'grow.cells(12).value = Format(mGama, Gammastr)
            'grow.cells(13).value = Format(mGama * qty, Gammastr_Val)
            'grow.cells(14).value = Format(mVega, Vegastr)
            'grow.cells(15).value = Format(mVega * qty, Vegastr_Val)
            'grow.cells(16).value = Format(mThita, Thetastr)
            'grow.cells(17).value = Format(mThita * qty, Thetastr_Val)
            grdtrad.EndEdit()
        Catch ex As Exception

        End Try

        ''MsgBox(mDelta)


    End Sub

    'Private Function cal_profit() As Boolean
    '    grdprofit.DataSource = Nothing
    '    'grdprofit.Refresh()
    '    Dim totfpr As Double
    '    Dim totcpr As Double
    '    profitarr = New ArrayList
    '    Dim bval As Double
    '    Dim bvol As Double

    '    Dim i As Integer
    '    i = 0
    '    Dim dcount As Integer
    '    dcount = 0
    '    Dim iscall As Boolean
    '    'ReDim profitarr(((profit.Rows.Count) * (profit.Columns.Count - 1)))
    '    For Each dr As DataRow In profit.Rows
    '        totfpr = 0
    '        dcount = 0
    '        For Each col As DataColumn In profit.Columns
    '            If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

    '                For Each grow As DataGridViewRow In grdtrad.Rows
    '                    If CBool(grow.Cells(0).Value) = True Then

    '                        If grow.Cells(3).Value = "F" Then
    '                            If Val(grow.Cells(4).Value) = 0 Then
    '                                MsgBox("Enter Spot Value for Selected Future")
    '                                grdtrad.Focus()
    '                                Return False
    '                            End If
    '                            If Val(grow.Cells(6).Value) = 0 Then
    '                                MsgBox("Enter Quantity Value for Selected Future")
    '                                grdtrad.Focus()
    '                                Return False

    '                            End If
    '                            dr(col.ColumnName) = (Val(dr(col.ColumnName)) + ((Val(dr("SpotValue")) - Val(grow.Cells(4).Value)) * Val(grow.Cells(6).Value))) + grossmtm
    '                            'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + ((val(dr("SpotValue")) - val(grow.Cells(4).Value)) * val(grow.Cells(6).Value)), RoundGrossMTM)
    '                            'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + ((val(dr("SpotValue")) - val(grow.Cells(4).Value)) * val(grow.Cells(6).Value)), GrossMTMstr)

    '                        Else
    '                            If grow.Cells(3).Value = "C" Then
    '                                iscall = True
    '                            Else
    '                                iscall = False
    '                            End If
    '                            If Val(grow.Cells(9).Value) = 0 Then
    '                                MsgBox("Enter Rate Value for Selected Call or Put")
    '                                grdtrad.Focus()

    '                                Return False

    '                            End If
    '                            If Val(grow.Cells(6).Value) = 0 Then
    '                                MsgBox("Enter Quantity Value for Selected Call or Put")
    '                                grdtrad.Focus()
    '                                Return False

    '                            End If
    '                            If Val(grow.Cells(4).Value) = 0 Then
    '                                MsgBox("Enter Spot Value for Selected Call or Put")
    '                                grdtrad.Focus()
    '                                Exit Function
    '                            End If

    '                            If Val(grow.Cells(8).Value) = 0 Then
    '                                MsgBox("Enter Volatality Value for Selected Call or Put")
    '                                grdtrad.Focus()
    '                                Return False

    '                            End If
    '                            totcpr = 0
    '                            Dim cd As Date
    '                            If (IsDate(col.ColumnName)) Then
    '                                cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
    '                            Else
    '                                cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
    '                            End If

    '                            'dcount = DatePart(DateInterval.Day, cd)
    '                            'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(dtexp.Value.Date))
    '                            Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells(2).Value).Date)
    '                            Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells(2).Value).Date)
    '                            If CDate(dttoday.Value.Date) = CDate(grow.Cells(2).Value).Date Then
    '                                _mT1 = 0.5
    '                            End If
    '                            If CDate(cd.Date) = CDate(grow.Cells(2).Value).Date Then
    '                                If (IsDate(col.ColumnName)) Then
    '                                    _mT = 0.5
    '                                Else
    '                                    _mT = 0.5
    '                                End If

    '                            End If
    '                            If _mT = 0 Then
    '                                _mT = 0.00001
    '                                _mT1 = 0.00001
    '                            Else
    '                                _mT = (_mT) / 365
    '                                _mT1 = (_mT1) / 365
    '                            End If
    '                            If (IsDate(col.ColumnName)) Then
    '                                bval = 0
    '                                bvol = 0
    '                                bvol = mObjData.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells(5).Value), Mrateofinterast, 0, (Val(grow.Cells(9).Value)), _mT1, iscall, True, 0, 6)
    '                                bval = mObjData.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells(5).Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 0)
    '                                totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
    '                                dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) + grossmtm
    '                                'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + totcpr, RoundGrossMTM)
    '                                ' dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr,GrossMTMstr)
    '                            Else
    '                                If (iscall = True) Then
    '                                    If ((Val(dr("SpotValue")) - Val(grow.Cells(5).Value)) > 0) Then
    '                                        bval = (Val(dr("SpotValue")) - Val(grow.Cells(5).Value))
    '                                        totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
    '                                        dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) + grossmtm
    '                                    Else
    '                                        bval = 0 ' val(dr(col.ColumnName)) + ((0 - val(grow.Cells(9).Value)) * val(grow.Cells(6).Value))
    '                                        totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
    '                                        dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) + grossmtm
    '                                    End If
    '                                Else
    '                                    If ((Val(grow.Cells(5).Value) - Val(dr("SpotValue"))) > 0) Then
    '                                        bval = (Val(grow.Cells(5).Value) - Val(dr("SpotValue")))
    '                                        totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
    '                                        dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) ' + grossmtm
    '                                    Else
    '                                        bval = 0 'val(dr(col.ColumnName)) + ((0 - val(grow.Cells(9).Value)) * val(grow.Cells(6).Value))
    '                                        totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
    '                                        dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) ' + grossmtm
    '                                    End If
    '                                End If
    '                            End If

    '                        End If

    '                    End If
    '                Next
    '                dcount = dcount + 1
    '            End If
    '            i = i + 1
    '            profitarr.Add(dr(col.ColumnName))

    '        Next
    '    Next
    '    grdprofit.DataSource = profit
    '    Return True

    '    'For Each col As DataColumn In profit.Columns
    '    '    If UCase(col.ColumnName) <> UCase("spotvalue") Then
    '    '        For Each drow As DataRow In profit.Rows

    '    '        Next
    '    '    End If
    '    'Next

    '    'grdprofit.Refresh()
    'End Function
    Private Function cal_profit() As Boolean
        grdprofit.DataSource = Nothing
        'grdprofit.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double
        profitarr = New ArrayList
        Dim bval As Double
        Dim bvol As Double

        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean
        'ReDim profitarr(((profit.Rows.Count) * (profit.Columns.Count - 1)))
        For Each dr As DataRow In profit.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In profit.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        If CBool(grow.Cells("Active").Value) = True Then

                            If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                                If CDbl(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Future.")
                                    grdtrad.Focus()
                                    Return False
                                End If
                                If CDbl(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Future.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + ((CDbl(dr("SpotValue")) - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value)))
                                'dr(col.ColumnName) = Math.Round(CDbl(dr(col.ColumnName)) + ((CDbl(dr("SpotValue")) - CDbl(grow.Cells(4).Value)) * CDbl(grow.Cells(6).Value)), RoundGrossMTM)
                                'dr(col.ColumnName) = Format(CDbl(dr(col.ColumnName)) + ((CDbl(dr("SpotValue")) - CDbl(grow.Cells(4).Value)) * CDbl(grow.Cells(6).Value)), GrossMTMstr)

                            Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                'Comment By Alpesh For Ltp 0 is Allow
                                'If CDbl(grow.Cells("ltp").Value) = 0 Then
                                '    MsgBox("Enter Ltp Value for Selected Call or Put")
                                '    grdtrad.Focus()
                                '    Return False
                                'End If

                                If CDbl(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If CDbl(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Exit Function
                                End If

                                If CDbl(grow.Cells("lv").Value) = 0 Then
                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                totcpr = 0
                                Dim cd As Date
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                Else
                                    cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
                                End If

                                'dcount = DatePart(DateInterval.Day, cd)
                                'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(dtexp.Value.Date))
                                Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                    _mT1 = 0.5
                                End If
                                If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                    If (IsDate(col.ColumnName)) Then
                                        _mT = 0.5
                                    Else
                                        _mT = 0.5
                                    End If

                                End If
                                If _mT = 0 Then
                                    _mT = 0.00001
                                    _mT1 = 0.00001
                                Else
                                    _mT = (_mT) / 365
                                    _mT1 = (_mT1) / 365
                                End If
                                If (IsDate(col.ColumnName)) Then
                                    bval = 0
                                    bvol = 0
                                    'bvol = Greeks.Black_Scholes(CDbl(txtmkt.Text), CDbl(grow.Cells(5).Value), Mrateofinterast, 0, (CDbl(grow.Cells(7).Value)), _mT1, iscall, True, 0, 6)
                                    bval = Greeks.Black_Scholes(CDbl(dr("SpotValue")), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value)) / 100, _mT, iscall, True, 0, 0)
                                    totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                    dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
                                    'dr(col.ColumnName) = Math.Round(CDbl(dr(col.ColumnName)) + totcpr, RoundGrossMTM)
                                    ' dr(col.ColumnName) = Format(CDbl(dr(col.ColumnName)) + totcpr,GrossMTMstr)
                                Else
                                    If (iscall = True) Then
                                        If ((CDbl(dr("SpotValue")) - CDbl(grow.Cells("Strike").Value)) > 0) Then
                                            bval = (CDbl(dr("SpotValue")) - CDbl(grow.Cells("Strike").Value))
                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
                                        Else
                                            bval = 0 ' CDbl(dr(col.ColumnName)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
                                        End If
                                    Else
                                        If ((CDbl(grow.Cells("Strike").Value) - CDbl(dr("SpotValue"))) > 0) Then
                                            bval = (CDbl(grow.Cells("Strike").Value) - CDbl(dr("SpotValue")))
                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
                                        Else
                                            bval = 0 'CDbl(dr(col.ColumnName)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
                                        End If
                                    End If
                                End If

                            End If

                        End If


                    Next
                    dr(col.ColumnName) += grossmtm
                    dcount = dcount + 1
                End If
                i = i + 1


                profitarr.Add(dr(col.ColumnName))

            Next
        Next
        grdprofit.DataSource = profit
        Return True

        'For Each col As DataColumn In profit.Columns
        '    If UCase(col.ColumnName) <> UCase("spotvalue") Then
        '        For Each drow As DataRow In profit.Rows

        '        Next
        '    End If
        'Next

        'grdprofit.Refresh()
    End Function

    Private Function cal_delta() As Boolean


        grddelta.DataSource = Nothing
        'grddelta.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double
        Dim bval As Double
        Dim bvol As Double


        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean

        For Each dr As DataRow In deltatable.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In deltatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then


                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        If CBool(grow.Cells("Active").Value) = True Then

                            If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Future.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Future.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + Val(grow.Cells("deltaval").Value), roundDelta_Val)
                                ' dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + val(grow.cells(11).value), Deltastr_Val)

                            Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                If Val(grow.Cells("last").Value) = 0 Then
                                    MsgBox("Enter Rate Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If

                                If Val(grow.Cells("lv").Value) = 0 Then
                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If

                                totcpr = 0
                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT = 0.5
                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    bval = 0
                                    bvol = 0
                                    bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 1)
                                    totcpr = (bval * Val(grow.Cells("units").Value))
                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, roundDelta_Val)
                                    ' dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, Deltastr_Val)
                                End If
                            End If
                        End If
                    Next
                    dcount = dcount + 1
                End If
                i = i + 1
                deltaarr.Add(dr(col.ColumnName))
            Next
        Next

        grddelta.DataSource = deltatable
        Return True

        'grddelta.Refresh()
    End Function
    Private Function cal_gamma() As Boolean
        grdgamma.DataSource = Nothing
        'grdgamma.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double

        Dim bval As Double
        Dim bvol As Double

        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean

        For Each dr As DataRow In gammatable.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In gammatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        If CBool(grow.Cells("Active").Value) = True Then
                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                '    'If val(grow.Cells(2).Value) = 0 Then
                                '    '    MsgBox("Enter Spot Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'If val(grow.Cells(5).Value) = 0 Then
                                '    '    MsgBox("Enter Units Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

                                'Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                If Val(grow.Cells("last").Value) = 0 Then
                                    MsgBox("Enter Rate Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False


                                End If

                                If Val(grow.Cells("lv").Value) = 0 Then
                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                totcpr = 0
                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT = 0.5
                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    bval = 0
                                    bvol = 0
                                    bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 2)
                                    totcpr = (bval * Val(grow.Cells("units").Value))
                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, roundGamma_Val)
                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, Gammastr_Val)
                                End If
                            End If
                        End If
                    Next
                    dcount = dcount + 1
                End If
                i = i + 1
                gammaarr.Add(dr(col.ColumnName))
            Next
        Next

        grdgamma.DataSource = gammatable
        Return True

        'grdgamma.Refresh()
    End Function
    Private Function cal_vega() As Boolean

        grdvega.DataSource = Nothing
        'grdvega.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double
        Dim bval As Double
        Dim bvol As Double


        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean

        For Each dr As DataRow In vegatable.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In vegatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then


                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        If CBool(grow.Cells("Active").Value) = True Then

                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                '    'If val(grow.Cells(2).Value) = 0 Then
                                '    '    MsgBox("Enter Spot Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'If val(grow.Cells(5).Value) = 0 Then
                                '    '    MsgBox("Enter Units Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

                                'Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                If Val(grow.Cells("last").Value) = 0 Then
                                    MsgBox("Enter Rate Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If

                                If Val(grow.Cells("lv").Value) = 0 Then
                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                totcpr = 0
                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT = 0.5
                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    bval = 0
                                    bvol = 0
                                    bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 3)
                                    totcpr = (bval * Val(grow.Cells("units").Value))
                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, roundVega_Val)
                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, Vegastr_Val)
                                End If
                            End If
                        End If
                    Next
                    dcount = dcount + 1
                End If
                i = i + 1
                vegaarr.Add(dr(col.ColumnName))
            Next
        Next

        grdvega.DataSource = vegatable
        Return True

        'grdvega.Refresh()

    End Function
    Private Function cal_theta() As Boolean
        grdtheta.DataSource = Nothing
        'grdtheta.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double

        thetaarr = New ArrayList
        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean
        Dim bval As Double
        Dim bvol As Double

        For Each dr As DataRow In thetatable.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In thetatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        If CBool(grow.Cells("Active").Value) = True Then

                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                '    'If val(grow.Cells(2).Value) = 0 Then
                                '    '    MsgBox("Enter Spot Value for Selected Future")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'If val(grow.Cells(5).Value) = 0 Then
                                '    '    MsgBox("Enter Units Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

                                'Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                If Val(grow.Cells("last").Value) = 0 Then
                                    MsgBox("Enter Rate Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If

                                If Val(grow.Cells("lv").Value) = 0 Then
                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                totcpr = 0
                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT = 0.5
                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    bval = 0
                                    bvol = 0
                                    bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 4)
                                    totcpr = (bval * Val(grow.Cells("units").Value))
                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, roundTheta_Val)
                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, Thetastr_Val)
                                End If
                            End If
                        End If
                    Next
                    dcount = dcount + 1
                End If
                i = i + 1
                thetaarr.Add(dr(col.ColumnName))
            Next
        Next

        grdtheta.DataSource = thetatable
        Return True

        'grdtheta.Refresh()

    End Function

    Private Function cal_volga() As Boolean
        grdvolga.DataSource = Nothing
        'grdvolga.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double

        volgaarr = New ArrayList
        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean
        Dim bval As Double
        Dim bvol As Double
        Dim mD1 As Double
        Dim mD2 As Double
        Dim mVega As Double

        For Each dr As DataRow In volgatable.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In volgatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        If CBool(grow.Cells("Active").Value) = True Then

                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                '    'If val(grow.Cells(2).Value) = 0 Then
                                '    '    MsgBox("Enter Spot Value for Selected Future")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'If val(grow.Cells(5).Value) = 0 Then
                                '    '    MsgBox("Enter Units Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

                                'Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                If Val(grow.Cells("last").Value) = 0 Then
                                    MsgBox("Enter Rate Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If

                                If Val(grow.Cells("lv").Value) = 0 Then
                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                totcpr = 0
                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT = 0.5
                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    bval = 0
                                    bvol = 0

                                    bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    mVega = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 3)

                                    mD1 = CalD1(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)
                                    mD2 = CalD2(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)

                                    bval = CalVolga(mVega, mD1, mD2, bvol)
                                    'mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mT)

                                    'bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 4)

                                    totcpr = (bval * Val(grow.Cells("units").Value))
                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, roundVolga_Val)
                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, volgastr_Val)
                                End If
                            End If
                        End If
                    Next
                    dcount = dcount + 1
                End If
                i = i + 1
                volgaarr.Add(dr(col.ColumnName))
                'If volgaarr(3) = "NaN" Then MsgBox("A")
            Next
        Next

        grdvolga.DataSource = volgatable
        Return True

        'grdvolga.Refresh()

    End Function
    Private Function cal_vanna() As Boolean
        grdvanna.DataSource = Nothing
        'grdvanna.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double

        vannaarr = New ArrayList
        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean
        Dim bval As Double
        Dim bvol As Double
        Dim mD1 As Double
        Dim mD2 As Double
        Dim mVega As Double

        For Each dr As DataRow In vannatable.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In vannatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        If CBool(grow.Cells("Active").Value) = True Then

                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                '    'If val(grow.Cells(2).Value) = 0 Then
                                '    '    MsgBox("Enter Spot Value for Selected Future")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'If val(grow.Cells(5).Value) = 0 Then
                                '    '    MsgBox("Enter Units Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

                                'Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                If Val(grow.Cells("last").Value) = 0 Then
                                    MsgBox("Enter Rate Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If

                                If Val(grow.Cells("lv").Value) = 0 Then
                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                totcpr = 0
                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT = 0.5
                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    bval = 0
                                    bvol = 0

                                    bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    mVega = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 3)

                                    mD1 = CalD1(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)
                                    mD2 = CalD2(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)

                                    'bval = CalVolga(mVega, mD1, mD2, bvol)
                                    bval = CalVanna(Val(dr("SpotValue")), mVega, mD1, mD2, bvol, _mT)


                                    'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    'bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 4)

                                    totcpr = (bval * Val(grow.Cells("units").Value))
                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, roundVanna_Val)
                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, vannastr_Val)
                                End If
                            End If
                        End If
                    Next
                    dcount = dcount + 1
                End If
                i = i + 1
                vannaarr.Add(dr(col.ColumnName))
            Next
        Next

        grdvanna.DataSource = vannatable
        Return True

        'grdvanna.Refresh()

    End Function


    Private Sub create_chart_profit()
        chtprofit.ColumnCount = 0
        chtprofit.ColumnLabelCount = 0
        chtprofit.ColumnLabelIndex = 0
        chtprofit.AutoIncrement = False
        chtprofit.RowCount = 0
        chtprofit.RowLabelCount = 0
        chtprofit.RowLabelIndex = 0
        chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone


        If profit.Columns.Count > 1 Then
            If profit.Columns.Count < 3 Then
                Exit Sub
            End If
            chtprofit.ColumnCount = profit.Columns.Count - 3
            chtprofit.ColumnLabelCount = 1
            chtprofit.ColumnLabelIndex = 1
            chtprofit.AutoIncrement = False
            chtprofit.RowCount = profit.Rows.Count - 1
            chtprofit.RowLabelCount = 1
            chtprofit.RowLabelIndex = 1
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(profitarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(profitarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            'With chtprofit
            '    Dim div As Double
            '    div = (((Math.Abs(maxval) - Math.Abs(minval))) * 40) / 100
            '    chtprofit.chartType = VtChChartType.VtChChartType2dLine
            '    YAxis = .Plot.Axis(VtChAxisId.VtChAxisIdY, 1)
            '    'Set Axis Scale Min and Max values      
            '    YAxis.ValueScale.Auto = False
            '    YAxis.CategoryScale.Auto = False
            '    YAxis.ValueScale.Minimum = minval
            '    YAxis.ValueScale.Maximum = maxval
            '    YAxis.ValueScale.MajorDivision = Format(((Math.Abs(maxval) - Math.Abs(minval)) \ div), "##0")
            '    YAxis.ValueScale.MinorDivision = 2
            'End With
            'YAxis = Nothing


            chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(profit.Rows.Count - 1, profit.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In profit.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
                    r = 0
                    For Each drow As DataRow In profit.Rows
                        cha(r, c) = Val(drow(col.ColumnName)) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lblprofit.Text = "Rs.in ( " & str & ")"
            Else
                lblprofit.Text = "-"
            End If
            chtprofit.ChartData = cha

            chtprofit.ShowLegend = False
            c = 1
            Dim i As Integer = 0





            For Each col As DataColumn In profit.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
                    chtprofit.Column = c
                    chtprofit.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")

                    i += 1
                    c += 1
                End If
                'If c >= grdprofit.Columns.Count Then
                '    Exit For
                'End If
            Next
            r = 1

            For Each drow As DataRow In profit.Rows
                chtprofit.Row = r
                chtprofit.RowLabel = drow("spotvalue")

                r += 1
            Next
            'MsgBox(chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).AxisGrid.MajorPen.)
            'chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 0).AxisGrid.MajorPen.Style = VtPenStyle.VtPenStyleDotted
            'chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).AxisGrid.MinorPen.Style = VtPenStyle.VtPenStyleDitted
            '  chtprofit.Plot.Axis(0).Labels(0).Format
            'MsgBox(chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).Labels(0))

        End If
    End Sub
    Private Sub create_chart_profit_Full()
        objProfitLossChart.chtprofit.ColumnCount = 0
        objProfitLossChart.chtprofit.ColumnLabelCount = 0
        objProfitLossChart.chtprofit.ColumnLabelIndex = 0
        objProfitLossChart.chtprofit.AutoIncrement = False
        objProfitLossChart.chtprofit.RowCount = 0
        objProfitLossChart.chtprofit.RowLabelCount = 0
        objProfitLossChart.chtprofit.RowLabelIndex = 0
        objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone


        If profit.Columns.Count > 2 Then
            objProfitLossChart.chtprofit.ColumnCount = profit.Columns.Count - 3
            objProfitLossChart.chtprofit.ColumnLabelCount = 1
            objProfitLossChart.chtprofit.ColumnLabelIndex = 1
            objProfitLossChart.chtprofit.AutoIncrement = False
            objProfitLossChart.chtprofit.RowCount = profit.Rows.Count - 1
            objProfitLossChart.chtprofit.RowLabelCount = 1
            objProfitLossChart.chtprofit.RowLabelIndex = 1
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(profitarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(profitarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            'With objProfitLossChart.chtprofit
            '    Dim div As Double
            '    div = (((Math.Abs(maxval) - Math.Abs(minval))) * 40) / 100
            '    objProfitLossChart.chtprofit.chartType = VtChChartType.VtChChartType2dLine
            '    YAxis = .Plot.Axis(VtChAxisId.VtChAxisIdY, 1)
            '    'Set Axis Scale Min and Max values      
            '    YAxis.ValueScale.Auto = False
            '    YAxis.CategoryScale.Auto = False
            '    YAxis.ValueScale.Minimum = minval
            '    YAxis.ValueScale.Maximum = maxval
            '    YAxis.ValueScale.MajorDivision = Format(((Math.Abs(maxval) - Math.Abs(minval)) \ div), "##0")
            '    YAxis.ValueScale.MinorDivision = 2
            'End With
            'YAxis = Nothing


            objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(profit.Rows.Count - 1, profit.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In profit.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
                    r = 0
                    For Each drow As DataRow In profit.Rows
                        cha(r, c) = Val(drow(col.ColumnName)) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lblprofit.Text = "Rs.in ( " & str & ")"
            Else
                lblprofit.Text = "-"
            End If
            objProfitLossChart.chtprofit.ChartData = cha

            objProfitLossChart.chtprofit.ShowLegend = False
            c = 1
            Dim i As Integer = 0





            For Each col As DataColumn In profit.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
                    objProfitLossChart.chtprofit.Column = c
                    objProfitLossChart.chtprofit.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")

                    i += 1
                    c += 1
                End If
                'If c >= grdprofit.Columns.Count Then
                '    Exit For
                'End If
            Next
            r = 1

            For Each drow As DataRow In profit.Rows
                objProfitLossChart.chtprofit.Row = r
                objProfitLossChart.chtprofit.RowLabel = drow("spotvalue")

                r += 1
            Next
            'MsgBox(objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).AxisGrid.MajorPen.)
            'objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 0).AxisGrid.MajorPen.Style = VtPenStyle.VtPenStyleDotted
            'objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).AxisGrid.MinorPen.Style = VtPenStyle.VtPenStyleDitted
            '  objProfitLossChart.chtprofit.Plot.Axis(0).Labels(0).Format
            'MsgBox(objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).Labels(0))

        End If

    End Sub
    Private Sub create_chart_delta()
        chtdelta.ColumnCount = 0
        chtdelta.ColumnLabelCount = 0
        chtdelta.ColumnLabelIndex = 0
        chtdelta.AutoIncrement = False
        chtdelta.RowCount = deltatable.Rows.Count - 1
        chtdelta.RowLabelCount = 0
        chtdelta.RowLabelIndex = 0
        chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If deltatable.Columns.Count > 2 Then
            chtdelta.ColumnCount = deltatable.Columns.Count - 3
            chtdelta.ColumnLabelCount = 1
            chtdelta.ColumnLabelIndex = 1
            chtdelta.AutoIncrement = False
            chtdelta.RowCount = deltatable.Rows.Count - 1
            chtdelta.RowLabelCount = 1
            chtdelta.RowLabelIndex = 1

            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(deltaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(deltaarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(deltatable.Rows.Count - 1, deltatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In deltatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In deltatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            chtdelta.ChartData = cha
            If div > 1 Then
                lbldelta.Text = "Rs.in ( " & str & ")"
            Else
                lbldelta.Text = "-"
            End If
            chtdelta.ShowLegend = False
            c = 1
            Dim i As Integer = 0

            For Each col As DataColumn In deltatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    chtdelta.Column = c
                    chtdelta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1
                    c += 1
                    'If c >= grddelta.Columns.Count Then
                    '    Exit For
                    'End If
                End If
            Next
            r = 1
            For Each drow As DataRow In deltatable.Rows
                chtdelta.Row = r
                chtdelta.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_delta_Full()
        objProfitLossChart.chtdelta.ColumnCount = 0
        objProfitLossChart.chtdelta.ColumnLabelCount = 0
        objProfitLossChart.chtdelta.ColumnLabelIndex = 0
        objProfitLossChart.chtdelta.AutoIncrement = False
        objProfitLossChart.chtdelta.RowCount = deltatable.Rows.Count - 1

        objProfitLossChart.chtdelta.RowLabelCount = 0
        objProfitLossChart.chtdelta.RowLabelIndex = 0
        objProfitLossChart.chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If deltatable.Columns.Count > 2 Then
            objProfitLossChart.chtdelta.ColumnCount = deltatable.Columns.Count - 3
            objProfitLossChart.chtdelta.ColumnLabelCount = 1
            objProfitLossChart.chtdelta.ColumnLabelIndex = 1
            objProfitLossChart.chtdelta.AutoIncrement = False
            objProfitLossChart.chtdelta.RowCount = deltatable.Rows.Count - 1
            objProfitLossChart.chtdelta.RowLabelCount = 1
            objProfitLossChart.chtdelta.RowLabelIndex = 1

            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(deltaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(deltaarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            objProfitLossChart.chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(deltatable.Rows.Count - 1, deltatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In deltatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In deltatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            objProfitLossChart.chtdelta.ChartData = cha
            If div > 1 Then
                lbldelta.Text = "Rs.in ( " & str & ")"
            Else
                lbldelta.Text = "-"
            End If
            objProfitLossChart.chtdelta.ShowLegend = False
            c = 1
            Dim i As Integer = 0

            For Each col As DataColumn In deltatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    objProfitLossChart.chtdelta.Column = c
                    objProfitLossChart.chtdelta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1
                    c += 1
                    'If c >= grddelta.Columns.Count Then
                    '    Exit For
                    'End If
                End If
            Next
            r = 1
            For Each drow As DataRow In deltatable.Rows
                objProfitLossChart.chtdelta.Row = r
                objProfitLossChart.chtdelta.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_gamma()
        chtgamma.ColumnCount = 0
        chtgamma.ColumnLabelCount = 0
        chtgamma.ColumnLabelIndex = 0
        chtgamma.AutoIncrement = False
        chtgamma.RowCount = 0
        chtgamma.RowLabelCount = 0
        chtgamma.RowLabelIndex = 0
        chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If gammatable.Columns.Count > 2 Then
            chtgamma.ColumnCount = gammatable.Columns.Count - 3
            chtgamma.ColumnLabelCount = 1
            chtgamma.ColumnLabelIndex = 1
            chtgamma.AutoIncrement = False
            chtgamma.RowCount = gammatable.Rows.Count - 1
            chtgamma.RowLabelCount = 1
            chtgamma.RowLabelIndex = 1
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(gammaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(gammaarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(gammatable.Rows.Count - 1, gammatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In gammatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In gammatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            chtgamma.ChartData = cha
            If div > 1 Then
                lblgamma.Text = "Rs.in ( " & str & ")"
            Else
                lblgamma.Text = "-"
            End If
            chtgamma.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            For Each col As DataColumn In gammatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    chtgamma.Column = c
                    chtgamma.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1
                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                    'End If
                End If
            Next
            r = 1
            For Each drow As DataRow In gammatable.Rows
                chtgamma.Row = r
                chtgamma.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_gamma_Full()

        objProfitLossChart.chtgamma.ColumnCount = 0
        objProfitLossChart.chtgamma.ColumnLabelCount = 0
        objProfitLossChart.chtgamma.ColumnLabelIndex = 0
        objProfitLossChart.chtgamma.AutoIncrement = False
        objProfitLossChart.chtgamma.RowCount = 0
        objProfitLossChart.chtgamma.RowLabelCount = 0
        objProfitLossChart.chtgamma.RowLabelIndex = 0
        objProfitLossChart.chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If gammatable.Columns.Count > 2 Then
            objProfitLossChart.chtgamma.ColumnCount = gammatable.Columns.Count - 3
            objProfitLossChart.chtgamma.ColumnLabelCount = 1
            objProfitLossChart.chtgamma.ColumnLabelIndex = 1
            objProfitLossChart.chtgamma.AutoIncrement = False
            objProfitLossChart.chtgamma.RowCount = gammatable.Rows.Count - 1
            objProfitLossChart.chtgamma.RowLabelCount = 1
            objProfitLossChart.chtgamma.RowLabelIndex = 1
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(gammaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(gammaarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            objProfitLossChart.chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(gammatable.Rows.Count - 1, gammatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In gammatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In gammatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            objProfitLossChart.chtgamma.ChartData = cha
            If div > 1 Then
                lblgamma.Text = "Rs.in ( " & str & ")"
            Else
                lblgamma.Text = "-"
            End If
            objProfitLossChart.chtgamma.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            For Each col As DataColumn In gammatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    objProfitLossChart.chtgamma.Column = c
                    objProfitLossChart.chtgamma.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1
                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                    'End If
                End If
            Next
            r = 1
            For Each drow As DataRow In gammatable.Rows
                objProfitLossChart.chtgamma.Row = r
                objProfitLossChart.chtgamma.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub

    Private Sub create_chart_vega()
        chtvega.ColumnCount = 0
        chtvega.ColumnLabelCount = 0
        chtvega.ColumnLabelIndex = 0
        chtvega.AutoIncrement = False
        chtvega.RowCount = 0
        chtvega.RowLabelCount = 0
        chtvega.RowLabelIndex = 0
        chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If vegatable.Columns.Count > 2 Then

            chtvega.ColumnCount = vegatable.Columns.Count - 3
            chtvega.ColumnLabelCount = 1
            chtvega.ColumnLabelIndex = 1
            chtvega.AutoIncrement = False
            chtvega.RowCount = vegatable.Rows.Count - 1
            chtvega.RowLabelCount = 1
            chtvega.RowLabelIndex = 1
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(vegaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(vegaarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(vegatable.Rows.Count - 1, vegatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In vegatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In vegatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            chtvega.ChartData = cha
            If div > 1 Then
                lblvega.Text = "Rs.in ( " & str & ")"
            Else
                lblvega.Text = "-"
            End If
            chtvega.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            For Each col As DataColumn In vegatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    chtvega.Column = c
                    chtvega.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1
                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next


            r = 1
            For Each drow As DataRow In vegatable.Rows
                chtvega.Row = r
                chtvega.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_vega_Full()
        objProfitLossChart.chtvega.ColumnCount = 0
        objProfitLossChart.chtvega.ColumnLabelCount = 0
        objProfitLossChart.chtvega.ColumnLabelIndex = 0
        objProfitLossChart.chtvega.AutoIncrement = False
        objProfitLossChart.chtvega.RowCount = 0
        objProfitLossChart.chtvega.RowLabelCount = 0
        objProfitLossChart.chtvega.RowLabelIndex = 0
        objProfitLossChart.chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If vegatable.Columns.Count > 2 Then

            objProfitLossChart.chtvega.ColumnCount = vegatable.Columns.Count - 3
            objProfitLossChart.chtvega.ColumnLabelCount = 1
            objProfitLossChart.chtvega.ColumnLabelIndex = 1
            objProfitLossChart.chtvega.AutoIncrement = False
            objProfitLossChart.chtvega.RowCount = vegatable.Rows.Count - 1
            objProfitLossChart.chtvega.RowLabelCount = 1
            objProfitLossChart.chtvega.RowLabelIndex = 1
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(vegaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(vegaarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            objProfitLossChart.chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(vegatable.Rows.Count - 1, vegatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In vegatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In vegatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            objProfitLossChart.chtvega.ChartData = cha
            If div > 1 Then
                lblvega.Text = "Rs.in ( " & str & ")"
            Else
                lblvega.Text = "-"
            End If
            objProfitLossChart.chtvega.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            For Each col As DataColumn In vegatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    objProfitLossChart.chtvega.Column = c
                    objProfitLossChart.chtvega.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1
                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next


            r = 1
            For Each drow As DataRow In vegatable.Rows
                objProfitLossChart.chtvega.Row = r
                objProfitLossChart.chtvega.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_theta()
        chttheta.ColumnCount = 0
        chttheta.ColumnLabelCount = 0
        chttheta.ColumnLabelIndex = 0
        chttheta.AutoIncrement = False
        chttheta.RowCount = 0
        chttheta.RowLabelCount = 0
        chttheta.RowLabelIndex = 0
        chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If thetatable.Columns.Count > 2 Then
            chttheta.ColumnCount = thetatable.Columns.Count - 3
            chttheta.ColumnLabelCount = 1
            chttheta.ColumnLabelIndex = 1
            chttheta.AutoIncrement = False
            chttheta.RowCount = thetatable.Rows.Count - 1
            chttheta.RowLabelCount = 1
            chttheta.RowLabelIndex = 1
            chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(thetaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(thetaarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            Dim cha(thetatable.Rows.Count - 1, thetatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In thetatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In thetatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lbltheta.Text = "Rs. in ( " & str & ")"
            Else
                lbltheta.Text = "-"
            End If
            chttheta.ChartData = cha

            chttheta.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
            'End If
            For Each col As DataColumn In thetatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    chttheta.Column = c
                    chttheta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1

                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next
            r = 1
            For Each drow As DataRow In thetatable.Rows
                chttheta.Row = r
                chttheta.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_theta_Full()
        objProfitLossChart.chttheta.ColumnCount = 0
        objProfitLossChart.chttheta.ColumnLabelCount = 0
        objProfitLossChart.chttheta.ColumnLabelIndex = 0
        objProfitLossChart.chttheta.AutoIncrement = False
        objProfitLossChart.chttheta.RowCount = 0
        objProfitLossChart.chttheta.RowLabelCount = 0
        objProfitLossChart.chttheta.RowLabelIndex = 0
        objProfitLossChart.chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If thetatable.Columns.Count > 2 Then
            objProfitLossChart.chttheta.ColumnCount = thetatable.Columns.Count - 3
            objProfitLossChart.chttheta.ColumnLabelCount = 1
            objProfitLossChart.chttheta.ColumnLabelIndex = 1
            objProfitLossChart.chttheta.AutoIncrement = False
            objProfitLossChart.chttheta.RowCount = thetatable.Rows.Count - 1
            objProfitLossChart.chttheta.RowLabelCount = 1
            objProfitLossChart.chttheta.RowLabelIndex = 1
            objProfitLossChart.chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(thetaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(thetaarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            Dim cha(thetatable.Rows.Count - 1, thetatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In thetatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In thetatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lbltheta.Text = "Rs. in ( " & str & ")"
            Else
                lbltheta.Text = "-"
            End If
            objProfitLossChart.chttheta.ChartData = cha

            objProfitLossChart.chttheta.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
            'End If
            For Each col As DataColumn In thetatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    objProfitLossChart.chttheta.Column = c
                    objProfitLossChart.chttheta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1

                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next
            r = 1
            For Each drow As DataRow In thetatable.Rows
                objProfitLossChart.chttheta.Row = r
                objProfitLossChart.chttheta.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    '------------
    Private Sub create_chart_volga()
        chtvolga.ColumnCount = 0
        chtvolga.ColumnLabelCount = 0
        chtvolga.ColumnLabelIndex = 0
        chtvolga.AutoIncrement = False
        chtvolga.RowCount = 0
        chtvolga.RowLabelCount = 0
        chtvolga.RowLabelIndex = 0
        chtvolga.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If volgatable.Columns.Count > 2 Then
            chtvolga.ColumnCount = volgatable.Columns.Count - 3
            chtvolga.ColumnLabelCount = 1
            chtvolga.ColumnLabelIndex = 1
            chtvolga.AutoIncrement = False
            chtvolga.RowCount = volgatable.Rows.Count - 1
            chtvolga.RowLabelCount = 1
            chtvolga.RowLabelIndex = 1
            chtvolga.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(volgaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(volgaarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            Dim cha(volgatable.Rows.Count - 1, volgatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In volgatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In volgatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lblvolga.Text = "Rs. in ( " & str & ")"
            Else
                lblvolga.Text = "-"
            End If
            chtvolga.ChartData = cha

            chtvolga.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
            'End If
            For Each col As DataColumn In volgatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    chtvolga.Column = c
                    chtvolga.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1

                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next
            r = 1
            For Each drow As DataRow In volgatable.Rows
                chtvolga.Row = r
                chtvolga.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_volga_Full()
        objProfitLossChart.chtvolga.ColumnCount = 0
        objProfitLossChart.chtvolga.ColumnLabelCount = 0
        objProfitLossChart.chtvolga.ColumnLabelIndex = 0
        objProfitLossChart.chtvolga.AutoIncrement = False
        objProfitLossChart.chtvolga.RowCount = 0
        objProfitLossChart.chtvolga.RowLabelCount = 0
        objProfitLossChart.chtvolga.RowLabelIndex = 0
        objProfitLossChart.chtvolga.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If volgatable.Columns.Count > 2 Then
            objProfitLossChart.chtvolga.ColumnCount = volgatable.Columns.Count - 3
            objProfitLossChart.chtvolga.ColumnLabelCount = 1
            objProfitLossChart.chtvolga.ColumnLabelIndex = 1
            objProfitLossChart.chtvolga.AutoIncrement = False
            objProfitLossChart.chtvolga.RowCount = volgatable.Rows.Count - 1
            objProfitLossChart.chtvolga.RowLabelCount = 1
            objProfitLossChart.chtvolga.RowLabelIndex = 1
            objProfitLossChart.chtvolga.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(volgaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(volgaarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            Dim cha(volgatable.Rows.Count - 1, volgatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In volgatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In volgatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lblvolga.Text = "Rs. in ( " & str & ")"
            Else
                lblvolga.Text = "-"
            End If
            objProfitLossChart.chtvolga.ChartData = cha

            objProfitLossChart.chtvolga.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
            'End If
            For Each col As DataColumn In volgatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    objProfitLossChart.chtvolga.Column = c
                    objProfitLossChart.chtvolga.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1

                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next
            r = 1
            For Each drow As DataRow In volgatable.Rows
                objProfitLossChart.chtvolga.Row = r
                objProfitLossChart.chtvolga.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    '-------------
    Private Sub create_chart_vanna()
        chtvanna.ColumnCount = 0
        chtvanna.ColumnLabelCount = 0
        chtvanna.ColumnLabelIndex = 0
        chtvanna.AutoIncrement = False
        chtvanna.RowCount = 0
        chtvanna.RowLabelCount = 0
        chtvanna.RowLabelIndex = 0
        chtvanna.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If vannatable.Columns.Count > 2 Then
            chtvanna.ColumnCount = vannatable.Columns.Count - 3
            chtvanna.ColumnLabelCount = 1
            chtvanna.ColumnLabelIndex = 1
            chtvanna.AutoIncrement = False
            chtvanna.RowCount = vannatable.Rows.Count - 1
            chtvanna.RowLabelCount = 1
            chtvanna.RowLabelIndex = 1
            chtvanna.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(vannaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(vannaarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            Dim cha(vannatable.Rows.Count - 1, vannatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In vannatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In vannatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lblvanna.Text = "Rs. in ( " & str & ")"
            Else
                lblvanna.Text = "-"
            End If
            chtvanna.ChartData = cha

            chtvanna.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
            'End If
            For Each col As DataColumn In vannatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    chtvanna.Column = c
                    chtvanna.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1

                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next
            r = 1
            For Each drow As DataRow In vannatable.Rows
                chtvanna.Row = r
                chtvanna.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_vanna_Full()
        objProfitLossChart.chtvanna.ColumnCount = 0
        objProfitLossChart.chtvanna.ColumnLabelCount = 0
        objProfitLossChart.chtvanna.ColumnLabelIndex = 0
        objProfitLossChart.chtvanna.AutoIncrement = False
        objProfitLossChart.chtvanna.RowCount = 0
        objProfitLossChart.chtvanna.RowLabelCount = 0
        objProfitLossChart.chtvanna.RowLabelIndex = 0
        objProfitLossChart.chtvanna.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If vannatable.Columns.Count > 2 Then
            objProfitLossChart.chtvanna.ColumnCount = vannatable.Columns.Count - 3
            objProfitLossChart.chtvanna.ColumnLabelCount = 1
            objProfitLossChart.chtvanna.ColumnLabelIndex = 1
            objProfitLossChart.chtvanna.AutoIncrement = False
            objProfitLossChart.chtvanna.RowCount = vannatable.Rows.Count - 1
            objProfitLossChart.chtvanna.RowLabelCount = 1
            objProfitLossChart.chtvanna.RowLabelIndex = 1
            objProfitLossChart.chtvanna.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(vannaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(vannaarr.ToArray))
            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            Dim cha(vannatable.Rows.Count - 1, vannatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In vannatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In vannatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lblvanna.Text = "Rs. in ( " & str & ")"
            Else
                lblvanna.Text = "-"
            End If
            objProfitLossChart.chtvanna.ChartData = cha

            objProfitLossChart.chtvanna.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
            'End If
            For Each col As DataColumn In vannatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    objProfitLossChart.chtvanna.Column = c
                    objProfitLossChart.chtvanna.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1

                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next
            r = 1
            For Each drow As DataRow In vannatable.Rows
                objProfitLossChart.chtvanna.Row = r
                objProfitLossChart.chtvanna.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub cal_exp()
        Dim exptable As New DataTable

        exptable = objExp.Select_Expenses

        Dim texp As Double = 0

        For Each drow As DataGridViewRow In grdtrad.Rows
            If drow.Cells("Active").Value = False Then Continue For
            If Val(drow.Cells("units").Value) > 0 Then
                If drow.Cells("CPF").Value = "F" Then
                    texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(futl)", "")) / exptable.Compute("max(futlp)", "")))
                Else
                    If Val(exptable.Compute("max(spl)", "")) <> 0 Then
                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * (Val(drow.Cells("last").Value) + Val(drow.Cells("last").Value))) * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", "")))
                    Else
                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", "")))
                    End If
                End If
            Else
                If drow.Cells("CPF").Value = "F" Then
                    texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(futs)", "")) / exptable.Compute("max(futsp)", "")))
                Else
                    If Val(exptable.Compute("max(spl)", "")) <> 0 Then
                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * (Val(drow.Cells("last").Value) + Val(drow.Cells("last").Value))) * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", "")))
                    Else
                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", "")))
                    End If
                End If
            End If
        Next
        txtexp.Text = Math.Round(texp, 2)
    End Sub
    Private Sub import()
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.xls)|*.xls"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            read_file(opfile.FileName)
        End If
    End Sub
    Private Sub read_file(ByVal fpath As String)
        Try
            If fpath <> "" Then
                Dim fi As New FileInfo(fpath)
                'Dim dv As DataView
                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" & fpath
                'Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
                Dim drow As DataRow
                Dim objConn As New OleDbConnection(sConnectionString)

                objConn.Open()

                'Dim objCmdSelect As New OleDbCommand("SELECT Staus,'Time I','Time II',C/P/F,Spot,Strike,Qty,Rate,'Vol(%)',Delta,'Del. Val',Theta,Th.Val,Vega,'Vg. Val',Gamma,'Gam. Val',uid FROM " & fi.Name, objConn)
                'Dim objCmdSelect As New OleDbCommand("SELECT * FROM " & fi.Name, objConn)
                Dim objCmdSelect As New OleDbCommand("SELECT * FROM [Scenario$]", objConn)

                Dim objAdapter1 As New OleDbDataAdapter

                objAdapter1.SelectCommand = objCmdSelect
                Dim tempdata As New DataTable
                tempdata = New DataTable
                objAdapter1.Fill(tempdata)
                objConn.Close()
                grdtrad.Rows.Clear()
                txtunits.Text = 0
                txtdelval.Text = 0
                txtthval.Text = 0
                txtvgval.Text = 0
                txtgmval.Text = 0
                txtVolgaVal.Text = 0
                TxtVannaVal.Text = 0
                Dim grow As New DataGridViewRow
                For Each drow In tempdata.Rows
                    grow = New DataGridViewRow
                    grdtrad.Rows.Add(grow)
                Next
                Dim i As Integer = 0
                For Each drow In tempdata.Rows

                    grdtrad.Rows(i).Cells("Active").Value = True
                    grdtrad.Rows(i).Cells("TimeI").Value = CDate(drow("Date"))
                    grdtrad.Rows(i).Cells("TimeII").Value = CDate(drow("Expiry"))
                    grdtrad.Rows(i).Cells("CPF").Value = CStr(drow("C/P/F"))
                    grdtrad.Rows(i).Cells("spval").Value = CDbl(drow("Underlying"))
                    grdtrad.Rows(i).Cells("Strike").Value = CDbl(drow("Strike"))
                    grdtrad.Rows(i).Cells("units").Value = Val(drow("Qty"))
                    grdtrad.Rows(i).Cells("ltp").Value = Val(drow("ltp"))
                    grdtrad.Rows(i).Cells("last").Value = CDbl(drow("Rate"))
                    grdtrad.Rows(i).Cells("lv").Value = Val(drow("Vol(%)"))
                    grdtrad.Rows(i).Cells("delta").Value = Val(drow("Delta"))
                    grdtrad.Rows(i).Cells("deltaval").Value = Val(drow("Del Val"))
                    grdtrad.Rows(i).Cells("theta").Value = Val(drow("Theta"))
                    grdtrad.Rows(i).Cells("thetaval").Value = Val(drow("Th val"))
                    grdtrad.Rows(i).Cells("vega").Value = Val(drow("Vega"))
                    grdtrad.Rows(i).Cells("vgval").Value = Val(drow("Vg val"))
                    grdtrad.Rows(i).Cells("gamma").Value = Val(drow("Gamma"))
                    grdtrad.Rows(i).Cells("gmval").Value = Val(drow("Gam val"))

                    grdtrad.Rows(i).Cells("volga").Value = Val(drow("Volga"))
                    grdtrad.Rows(i).Cells("volgaval").Value = Val(drow("Vo val"))
                    grdtrad.Rows(i).Cells("vanna").Value = Val(drow("Vanna"))
                    grdtrad.Rows(i).Cells("vannaval").Value = Val(drow("Va val"))


                    grdtrad.Rows(i).Cells("uid").Value = Val(drow("uid"))



                    '  txtunits.Text = Val(txtunits.Text) + Val(grdtrad.Rows(i).Cells(6).Value)
                    txtdelval.Text = Val(txtdelval.Text) + Val(grdtrad.Rows(i).Cells("delta").Value)
                    txtthval.Text = Val(txtthval.Text) + Val(grdtrad.Rows(i).Cells("thetaval").Value)
                    txtvgval.Text = Val(txtvgval.Text) + Val(grdtrad.Rows(i).Cells("vega").Value)
                    txtgmval.Text = Val(txtgmval.Text) + Val(grdtrad.Rows(i).Cells("gamma").Value)
                    txtVolgaVal.Text = Val(txtVolgaVal.Text) + Val(grdtrad.Rows(i).Cells("volga").Value)
                    TxtVannaVal.Text = Val(TxtVannaVal.Text) + Val(grdtrad.Rows(i).Cells("vanna").Value)


                    i += 1
                Next


            End If
        Catch ex As Exception
            MsgBox("File Not Processed.")
            ' MsgBox(ex.ToString)
        End Try




    End Sub
    Private Sub init_result()
        rtable = New DataTable
        With rtable.Columns

            '.Add("timeI", GetType(Date))
            '.Add("timeII", GetType(Date))
            '.Add("cpf")
            '.Add("spval", GetType(Double))
            '.Add("strike", GetType(Double))
            '.Add("units", GetType(Double))
            '.Add("last", GetType(Double))
            '.Add("lv", GetType(Double))
            '.Add("delta", GetType(Double))
            '.Add("deltaval", GetType(Double))
            '.Add("theta", GetType(Double))
            '.Add("thetaval", GetType(Double))
            '.Add("vega", GetType(Double))
            '.Add("vgval", GetType(Double))
            '.Add("gamma", GetType(Double))
            '.Add("gmval", GetType(Double))

            .Add("timeI")
            .Add("timeII")
            .Add("cpf")
            .Add("spval", GetType(Double))
            .Add("strike", GetType(Double))
            .Add("units", GetType(Double))
            .Add("last", GetType(Double))
            .Add("Price", GetType(Double))
            .Add("lv", GetType(Double))
            .Add("delta", GetType(Double))
            .Add("deltaval", GetType(Double))
            .Add("theta", GetType(Double))
            .Add("thetaval", GetType(Double))
            .Add("vega", GetType(Double))
            .Add("vgval", GetType(Double))
            .Add("gamma", GetType(Double))
            .Add("gmval", GetType(Double))
            .Add("volga", GetType(Double))
            .Add("volgaval", GetType(Double))
            .Add("vanna", GetType(Double))
            .Add("vannaval", GetType(Double))
            .Add("pl", GetType(Double))


        End With
    End Sub
    Private Sub fill_result(ByVal ind As Integer, ByVal rind As Integer, ByVal cind As Integer)
        REM Grid  Double Click Show Volga and Vanna 
        If rind = -1 Then Exit Sub
        init_result()
        Dim iscall As Boolean = False
        Dim drow As DataRow
        Dim _mT As Double = 0
        Dim mD1 As Double = 0
        Dim mD2 As Double = 0
        Dim mVolatility As Double = 0


        Select Case ind
            Case 0

                'If CDate(profit.Columns(cind).ColumnName) < CDate(dtexp.Text) Then
                '    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName), CDate(dtexp.Text))
                '    'Else
                '    '    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName), CDate(dtexp.Text))
                'End If

                'mt = DateDiff(DateInterval.Day, CDate(drow("mdate")), Now())

                For Each grow As DataGridViewRow In grdtrad.Rows
                    If grow.Cells("Active").Value = False Then Continue For
                    If CBool(grow.Cells("Active").Value) = True Then
                        drow = rtable.NewRow
                        If (IsDate(profit.Columns(cind).ColumnName)) Then
                            drow("timeI") = Format(CDate(profit.Columns(cind).ColumnName), "MMM/dd/yyyy")

                        ElseIf (cind) = profit.Columns.Count - 1 Then
                            drow("timeI") = Format(CDate(Mid(profit.Columns(cind).ColumnName, 7, Len(profit.Columns(cind).ColumnName) - 1)), "MMM/dd/yyyy")
                        Else
                            Exit Sub
                            'drow("timeI") = Format(CDate(Mid(profit.Columns(cind).ColumnName, 1, Len(profit.Columns(cind).ColumnName) - 4)), "MMM/dd/yyyy")
                        End If



                        'drow("timeII") = Format(CDate(dtexp.Text), "MMM/dd/yyyy")
                        'drow("timeI") = Format(CDate(grow.Cells(1).Value), "MMM/dd/yyyy")
                        drow("timeII") = Format(CDate(grow.Cells("TimeII").Value), "MMM/dd/yyyy")
                        drow("cpf") = grow.Cells("CPF").Value
                        drow("spval") = Val(profit.Rows(rind)(0))
                        drow("strike") = Val(grow.Cells("Strike").Value)
                        drow("units") = Val(grow.Cells("units").Value)
                        drow("last") = Val(grow.Cells("ltp").Value)            ' 9 is used
                        drow("lv") = Val(grow.Cells("lv").Value)
                        If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                            drow("delta") = Val(grow.Cells("delta").Value)
                            drow("deltaval") = Val(grow.Cells("deltaval").Value)
                            drow("theta") = Val(0)
                            drow("thetaval") = Val(0)
                            drow("vega") = Val(0)
                            drow("vgval") = Val(0)
                            drow("gamma") = Val(0)
                            drow("gmval") = Val(0)
                            drow("volga") = Val(0)
                            drow("volgaval") = Val(0)
                            drow("vanna") = Val(0)
                            drow("vannaval") = Val(0)
                            drow("pl") = Math.Round(Val(((Val(profit.Rows(rind)(0)) - Val(grow.Cells("spval").Value)) * Val(grow.Cells("units").Value))), 2)
                            drow("Price") = (Val(profit.Rows(rind)(0)) - Val(grow.Cells("spval").Value))
                        Else

                            If CDate(drow("timeI")) < CDate(drow("timeII")) Then
                                If (IsDate(profit.Columns(cind).ColumnName)) Then
                                    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName).Date, CDate(drow("timeII")).Date)
                                Else
                                    _mT = DateDiff(DateInterval.Day, CDate(Mid(profit.Columns(cind).ColumnName, 1, Len(profit.Columns(cind).ColumnName) - 4)).Date, CDate(drow("timeII")).Date)
                                End If
                                'Else
                                '    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName), CDate(dtexp.Text))
                            End If

                            If grow.Cells("CPF").Value = "C" Then
                                iscall = True
                            Else
                                iscall = False
                            End If

                            If _mT = 0 Then
                                _mT = 0.0001
                            Else
                                _mT = (_mT) / 365
                            End If
                            Dim bval As Double
                            bval = 0
                            bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 1)
                            drow("delta") = Math.Round(CDbl(bval), 2)
                            drow("deltaval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)), 2)
                            bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 4)
                            drow("theta") = Math.Round(CDbl(bval), 2)
                            drow("thetaval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)))
                            bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 3)
                            drow("vega") = Math.Round(CDbl(bval), 2)
                            drow("vgval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)), 2)
                            bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 2)
                            drow("gamma") = Math.Round(CDbl(bval), 5)
                            drow("gmval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)), 5)

                            'Volga , Vanna
                            mVolatility = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 6)

                            mD1 = mD1 + CalD1(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, mVolatility, _mT)
                            mD2 = mD2 + CalD2(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, mVolatility, _mT)

                            drow("volga") = CalVolga(drow("vega"), mD1, mD2, mVolatility)
                            drow("vanna") = CalVanna(CDbl(profit.Rows(rind)(0)), drow("vega"), mD1, mD2, mVolatility, _mT)

                            drow("volgaval") = Math.Round(CDbl(drow("volga") * CDbl(grow.Cells("units").Value)), 5)
                            drow("vannaval") = Math.Round(CDbl(drow("vanna") * CDbl(grow.Cells("units").Value)), 5)


                            'bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 2)
                            'drow("gamma") = Math.Round(CDbl(bval), 5)
                            'drow("gmval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)), 5)


                            bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 0)

                            drow("pl") = Math.Round(CDbl(((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))), 2)  ' 9 is used
                            drow("Price") = (bval - CDbl(grow.Cells("ltp").Value))
                        End If
                        rtable.Rows.Add(drow)
                    End If
                Next
                drow = rtable.NewRow
                drow("timeI") = ""
                drow("timeII") = ""
                drow("cpf") = "Total"
                drow("spval") = 0
                drow("strike") = 0
                drow("units") = Val(rtable.Compute("sum(units)", ""))
                drow("last") = 0
                drow("Price") = 0
                drow("lv") = 0
                drow("delta") = 0
                drow("deltaval") = Val(rtable.Compute("sum(deltaval)", ""))
                drow("theta") = Val(0)
                drow("thetaval") = Val(rtable.Compute("sum(thetaval)", ""))
                drow("vega") = Val(0)
                drow("vgval") = Val(rtable.Compute("sum(vgval)", ""))
                drow("gamma") = Val(0)
                drow("gmval") = Val(rtable.Compute("sum(gmval)", ""))
                drow("volga") = Val(0)
                drow("volgaval") = Val(rtable.Compute("sum(volgaval)", ""))
                drow("vanna") = Val(0)
                drow("vannaval") = Val(rtable.Compute("sum(vannaval)", ""))
                drow("pl") = Math.Round(Val(rtable.Compute("sum(pl)", "")), 2) + grossmtm
                rtable.Rows.Add(drow)
        End Select
    End Sub
    Public Function MaxValOfIntArray(ByRef TheArray As Object) As Double
        'This function gives max value of int array without sorting an array
        Dim i As Integer
        Dim MaxIntegersIndex As Integer
        MaxIntegersIndex = 0

        For i = 1 To UBound(TheArray)
            If Val(TheArray(i)) > Val(TheArray(MaxIntegersIndex)) Then
                MaxIntegersIndex = i
            End If
        Next
        'index of max value is MaxValOfIntArray
        MaxValOfIntArray = Val(TheArray(MaxIntegersIndex))
    End Function
    Public Function MinValOfIntArray(ByRef TheArray As Object) As Double
        'This function gives max value of int array without sorting an array
        Dim i As Integer
        Dim MaxIntegersIndex As Integer
        MaxIntegersIndex = 0

        For i = 1 To UBound(TheArray)
            If Val(TheArray(i)) < Val(TheArray(MaxIntegersIndex)) Then
                MaxIntegersIndex = i
            End If
        Next
        'index of max value is MaxValOfIntArray
        MinValOfIntArray = Val(TheArray(MaxIntegersIndex))
    End Function
    Private Sub export()
        Dim savedi As New SaveFileDialog
        savedi.Filter = "Files(*.xls)|*.xls"
        If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim grd(7) As DataGridView
            grd(0) = grdtrad
            grd(1) = grdprofit
            grd(2) = grddelta
            grd(3) = grdtheta
            grd(4) = grdvega
            grd(5) = grdgamma
            grd(6) = grdvolga
            grd(7) = grdvanna
            Dim sname(7) As String
            sname(0) = "Scenario"
            sname(1) = "Profit & Loss"
            sname(2) = "Delta"
            sname(3) = "Theta"
            sname(4) = "Vega"
            sname(5) = "Gamma"
            sname(6) = "Volga"
            sname(7) = "Vanna"
            exporttoexcel(grd, savedi.FileName, sname, "scenario")
            'exporttoexcel(grdprofit)
            'Dim str As String
            'str = Mid(savedi.FileName, 1, Len(savedi.FileName) - 4)
            'exportExcel(grdtrad, str & "_scenario.csv")
            'exportExcel(grdprofit, str & "_ProfitLoss.csv")
            'exportExcel(grddelta, str & "_Delta.csv")
            'exportExcel(grdtheta, str & "_Theta.csv")
            'exportExcel(grdvega, str & "_Vega.csv")
            'exportExcel(grdgamma, str & "_Gamma.csv")
            'MsgBox("Export Complete.", MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub create_active()
        grdact.Columns.Clear()
        Dim i As Integer = 0
        If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
        End If
        i += 1
        Dim gcol As DataGridViewCheckBoxColumn
        Dim temptable As New DataTable
        temptable = New DataTable
        Dim tdate As Date
        For k As Integer = 0 To i - 1
            tdate = Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MMM/yyyy")
            If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
                gcol = New DataGridViewCheckBoxColumn
                gcol.HeaderText = tdate
                gcol.DataPropertyName = tdate
                gcol.Width = 70
                grdact.Columns.Add(gcol)
                temptable.Columns.Add(tdate, GetType(Boolean))
                gcol.DefaultCellStyle.NullValue = False
            End If
        Next

        tdate = Format(dtexp.Value, "dd/MMM/yyyy")
        If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
            gcol = New DataGridViewCheckBoxColumn
            gcol.HeaderText = "Expiry"
            gcol.DataPropertyName = "Expiry " & tdate
            grdact.Columns.Add(gcol)
            temptable.Columns.Add("Expiry " & tdate, GetType(Boolean))
            gcol.DefaultCellStyle.NullValue = False
        End If

        If grdact.Columns.Count > 0 Then
            Dim drow As DataRow
            drow = temptable.NewRow
            'For k As Integer = 0 To i - 1
            tdate = Format(DateAdd(DateInterval.Day, 0, dttoday.Value), "dd/MMM/yyyy")
            If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
                drow(tdate) = True
            End If
            Try
                tdate = Format(DateAdd(DateInterval.Day, (i - 1), dttoday.Value), "dd/MMM/yyyy")
                If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
                    drow(tdate) = True
                    drow("Expiry " & tdate) = True
                End If
            Catch ex As Exception
                MsgBox("Beginning date can't be greater than expiry date.")
                Exit Sub
            End Try
            Dim t As Integer
            t = Math.Round((Val(grdact.ColumnCount - 1)) / 2, 0)
            tdate = Format(DateAdd(DateInterval.Day, t, dttoday.Value), "dd/MMM/yyyy")
            If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
                drow(tdate) = True
            End If
            ' Next
            temptable.Rows.Add(drow)
            grdact.DataSource = temptable
            'grdact.Refresh()
        End If

        'assign expiry date to selected expiry date
        If frmFlg = True Then 'this flg used bcoz first need not to assign expiry date
            For Each grow As DataGridViewRow In grdtrad.Rows
                If grow.Cells("TimeII").ReadOnly = False Then
                    grow.Cells("TimeII").Value = dtexp.Value
                End If
            Next
        End If
        frmFlg = True
        'If grdtrad.Rows(grdtrad.NewRowIndex).Cells("TimeII").ReadOnly = False Then
        '    grdtrad.Rows(grdtrad.NewRowIndex).Cells("TimeII").Value = dtexp.Value
        'End If

        If grdtrad.Rows.Count > 1 Then
            grdtrad.Rows(grdtrad.Rows.Count - 1).Cells(4).Value = grdtrad.Rows(grdtrad.Rows.Count - 2).Cells(4).Value
        End If
    End Sub
#End Region
    Private Sub chkint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkint.CheckedChanged
        If chkint.Checked = True Then
            txtinterval.Text = 2
            Label2.Visible = True
        Else
            Label2.Visible = False
            txtinterval.Text = 100
        End If
    End Sub
    Private Function cal_profitLoss() As Double
        Dim profit As Double = 0
        For Each grow As DataGridViewRow In grdtrad.Rows
            If grow.Cells("Active").Value = False Then Continue For
            If grow.Cells("Active").Value = True Then profit += (grow.Cells("units").Value) * (grow.Cells("last").Value - grow.Cells("ltp").Value)
        Next
        profit += grossmtm
        Return profit
    End Function
    Private Function cal_FutprofitLoss() As Double
        Dim profit As Double = 0
        For Each grow As DataGridViewRow In grdtrad.Rows
            If grow.Cells("Active").Value = False Then Continue For
            If grow.Cells("Active").Value = True Then profit += (grow.Cells("units").Value) * (grow.Cells("last").Value - grow.Cells("ltp1").Value)
        Next
        profit += grossmtm
        Return profit
    End Function
    Private Sub chtprofit_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtprofit.DblClick
        chartName = "profit"
        ValLBLprofit = lblprofit.Text
        objProfitLossChart.Show()
    End Sub
    Private Sub chttheta_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chttheta.DblClick
        chartName = "theta"
        ValLBLtheta = lbltheta.Text
        objProfitLossChart.Show()
    End Sub

    Private Sub chtvega_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtvega.DblClick
        chartName = "vega"
        ValLBLvega = lblvega.Text
        objProfitLossChart.Show()
    End Sub

    Private Sub chtdelta_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtdelta.DblClick
        chartName = "delta"
        ValLBLdelta = lbldelta.Text
        objProfitLossChart.Show()
    End Sub

    Private Sub chtgamma_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtgamma.DblClick
        chartName = "gamma"
        ValLBLgamma = lblgamma.Text
        objProfitLossChart.Show()
    End Sub

    Private Sub chtvolga_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtvolga.DblClick
        chartName = "volga"
        ValLBLvolga = lblvolga.Text
        objProfitLossChart.Show()
    End Sub
    Private Sub chtvanna_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtvanna.DblClick
        chartName = "vanna"
        ValLBLVanna = lblvanna.Text
        objProfitLossChart.Show()
    End Sub


    Private Sub Vol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Vol.Click
        Try
            If Vol.Text = "Freeze Vol Column" Then
                For Each grow As DataGridViewRow In grdtrad.Rows
                    grow.Cells("lv").ReadOnly = True
                    Vol.Text = "Unfreeze Vol Column"
                Next
            Else
                For Each grow As DataGridViewRow In grdtrad.Rows
                    grow.Cells("lv").ReadOnly = False
                    Vol.Text = "Freeze Vol Column"
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub grdtrad_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdtrad.CellMouseDown
        If e.RowIndex <> -1 Then
            cellno = e.RowIndex
            'grdtrad.EndEdit()
            'Call cal_summary()
        End If
    End Sub

    Private Sub scenario_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Not XResolution = Screen.PrimaryScreen.Bounds.Width Then
            XResolution = Screen.PrimaryScreen.Bounds.Width
            Call setGridTrad()
        End If
    End Sub
    Private Sub setGridTrad()
        Dim intX As Integer
        intX = Screen.PrimaryScreen.Bounds.Width
        Dim diff As Double = intX - 1024

        If diff = 0 Then
            Exit Sub
        ElseIf diff > 0 Then
            diff = diff / 16
            Dim i As Integer
            For i = 0 To grdtrad.Columns.Count - 1
                grdtrad.Columns(i).Width += diff + 2
            Next
        Else
            diff = diff / 16
            Dim i As Integer
            For i = 0 To grdtrad.Columns.Count - 1
                grdtrad.Columns(i).Width += diff
            Next
        End If
    End Sub

    Private Sub grdprofit_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdprofit.CellContentClick

    End Sub

    Private Sub expiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles expiry.Click
        Try
            If expiry.Text = "Freeze Expiry Column" Then
                For Each grow As DataGridViewRow In grdtrad.Rows
                    grow.Cells("TimeII").ReadOnly = True
                    expiry.Text = "Unfreeze Expiry Column"
                Next
            Else
                For Each grow As DataGridViewRow In grdtrad.Rows
                    grow.Cells("TimeII").ReadOnly = False
                    expiry.Text = "Freeze Expiry Column"
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnincrease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnincrease.Click
        involflag = True
        Call IncDecVolCal()
    End Sub
    Private Sub btndecrease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndecrease.Click
        decvolflag = True
        Call IncDecVolCal()
    End Sub
    Private Sub IncDecVolCal() ''divyesh
        If txtvol.Text.Trim = "" Then
            txtvol.Text = 0
        End If
        If grdtrad.Rows.Count >= 0 Then
            For Each grow As DataGridViewRow In grdtrad.Rows
                If grow.Cells("CPF").Value = "C" Or grow.Cells("CPF").Value = "P" Then
                    If grow.Cells("lv").ReadOnly = False Then
                        If involflag = True Then
                            grow.Cells("lv").Value = Format(grow.Cells("lv").Value + Val(txtvol.Text), "#0.00")
                        ElseIf decvolflag = True Then
                            If grow.Cells("lv").Value - Val(txtvol.Text) > 0 Then
                                grow.Cells("lv").Value = Format(grow.Cells("lv").Value - Val(txtvol.Text), "#0.00")
                            End If
                        End If
                    End If
                    Dim mt As Double
                    Dim mmt As Double
                    Dim futval As Double

                    mt = DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date)
                    mmt = (DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)) - 1
                    REM For Rate Not To Be Zero  Alpesh & Udipth Sir
                    REM Change Mt=0.5 Because Everywhere Mt=0.5 
                    If mt = 0 Then
                        'mt = 0.05
                        'mmt = 0
                        mt = 0.5
                        mmt = 0
                    End If
                    If mmt = 0 Then
                        'mmt = 0.05
                        mmt = 0.5
                    End If
                    futval = Val(grow.Cells("spval").Value)
                    Dim VarIsCall As Boolean = False
                    If grow.Cells("CPF").Value = "C" Then
                        VarIsCall = True
                    End If
                    CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, VarIsCall, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
                    cal_summary()
                End If
            Next
            involflag = False
            decvolflag = False
            'result(False)
        End If

    End Sub
    Private Sub txtvol_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtvol.KeyPress
        numonly(e)
    End Sub

    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label21.Click

    End Sub

    Private Sub grdtrad_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtrad.CellLeave
        'If (grdtrad.Columns(e.ColumnIndex).Name = "TimeII") Then
        '    If (IsDate(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False) Then
        '        'grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dtexp.Value
        '        grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
        '        MsgBox("Please Enter Proper Date...")
        '        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dttoday.Value
        '    End If
        'End If
        'If (grdtrad.Columns(e.ColumnIndex).Name = "TimeII") Then
        '    If (IsDate(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False) Then
        '        grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dtexp.Value
        '        grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
        '        MsgBox("Please Enter Proper Date...")
        '        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dttoday.Value
        '    Else
        '        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dttoday.Value
        '    End If
        'End If

    End Sub


    Private Sub tbcon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbcon.SelectedIndexChanged
        Select Case UCase(tbcon.SelectedTab.Name)
            Case "TBPROFITCHART"
                SetResultButtonText(cmdresult, chtprofit, grdprofit, lblprofit, False)
            Case "TBCHDELTA"
                SetResultButtonText(cmdresult, chtdelta, grddelta, lbldelta, False)
            Case "TBCHGAMMA"
                SetResultButtonText(cmdresult, chtgamma, grdgamma, lblgamma, False)
            Case "TBCHVEGA"
                SetResultButtonText(cmdresult, chtvega, grdvega, lblvega, False)
            Case "TBCHTHETA"
                SetResultButtonText(cmdresult, chttheta, grdtheta, lbltheta, False)
            Case "TBCHVOLGA"
                SetResultButtonText(cmdresult, chtvolga, grdvolga, lblvolga, False)
            Case "TBCHVANNA"
                SetResultButtonText(cmdresult, chtvanna, grdvanna, lblvanna, False)
        End Select
    End Sub
    'Private Sub SetResultButtonTextOnTabChange(ByVal Cht As AxMSChart20Lib.AxMSChart)
    '    If Cht.Visible = True Then
    '        cmdresult.Text = "G"
    '    Else
    '        cmdresult.Text = "C"
    '    End If
    'End Sub
    Private Sub SetResultButtonText(ByVal Cmd As Button, ByVal cht As AxMSChart20Lib.AxMSChart, ByVal grd As DataGridView, ByVal Lbl As System.Windows.Forms.Label, ByVal SetVisiblity As Boolean)
        If cht.Visible = True Then
            If SetVisiblity = True Then
                Cmd.Text = "&Chart(F1)"
                cht.Visible = False
                Lbl.Visible = False
                grd.Visible = True
            Else
                Cmd.Text = "&Value(F1)"
            End If
        Else
            If SetVisiblity = True Then
                Cmd.Text = "&Value(F1)"
                cht.Visible = True
                Lbl.Visible = True
                grd.Visible = False
            Else
                Cmd.Text = "&Chart(F1)"
            End If
        End If
    End Sub
    Private Sub CmdAllCV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdAllCV.Click
        REM In Scenario window For All Greeks Chart and GridData on One tab,According to Button press it shows chart or Griddata
        Dim mSelIndex As Integer
        mAllCV = ""
        result(True)

        'If CmdAllCV.Text = "Show &All Charts(F8)" Then
        '    CmdAllCV.Text = "Show &All Values(F8)"
        '    mAllCV = "Charts"
        '    'chtprofit.Visible = True
        '    'chtdelta.Visible = True
        '    'chtgamma.Visible = True
        '    'chtvega.Visible = True
        '    'chttheta.Visible = True
        '    'chtvolga.Visible = True
        '    'chtvanna.Visible = True
        'Else
        '    CmdAllCV.Text = "Show &All Charts(F8)"
        '    mAllCV = "Values"
        '    'chtprofit.Visible = True
        '    'chtdelta.Visible = True
        '    'chtgamma.Visible = True
        '    'chtvega.Visible = True
        '    'chttheta.Visible = True
        '    'chtvolga.Visible = True
        '    'chtvanna.Visible = True
        'End If

        If mAllCV.Trim <> "" Then
            mSelIndex = tbcon.SelectedIndex
            For SelIndex As Int16 = 0 To tbcon.TabPages.Count
                tbcon.SelectedIndex = SelIndex
                Select Case UCase(tbcon.SelectedTab.Name)
                    Case "TBPROFITCHART"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chtprofit, grdprofit, lblprofit, True)
                        Else
                            SetChartGridForAll(chtprofit, grdprofit, lblprofit, False)
                        End If
                    Case "TBCHDELTA"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chtdelta, grddelta, lbldelta, True)
                        Else
                            SetChartGridForAll(chtdelta, grddelta, lbldelta, False)
                        End If
                    Case "TBCHGAMMA"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chtgamma, grdgamma, lblgamma, True)
                        Else
                            SetChartGridForAll(chtgamma, grdgamma, lblgamma, False)
                        End If
                    Case "TBCHVEGA"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chtvega, grdvega, lblvega, True)
                        Else
                            SetChartGridForAll(chtvega, grdvega, lblvega, False)
                        End If
                    Case "TBCHTHETA"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chttheta, grdtheta, lbltheta, True)
                        Else
                            SetChartGridForAll(chttheta, grdtheta, lbltheta, False)
                        End If
                    Case "TBCHVOLGA"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chtvolga, grdvolga, lblvolga, True)
                        Else
                            SetChartGridForAll(chtvolga, grdvolga, lblvolga, False)
                        End If
                    Case "TBCHVANNA"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chtvanna, grdvanna, lblvanna, True)
                        Else
                            SetChartGridForAll(chtvanna, grdvanna, lblvanna, False)
                        End If
                End Select
            Next
            tbcon.SelectedIndex = mSelIndex
        End If
        'SetResultButtonText(cmdresult, chtprofit, grdprofit, True, True)
        'SetResultButtonText(cmdresult, chtdelta, grddelta, True, True)
        'SetResultButtonText(cmdresult, chtgamma, grdgamma, True, True)
        'SetResultButtonText(cmdresult, chtvega, grdvega, True, True)
        'SetResultButtonText(cmdresult, chttheta, grdtheta, True, True)
        'SetResultButtonText(cmdresult, chtvolga, grdvolga, True, True)
        'SetResultButtonText(cmdresult, chtvanna, grdvanna, True, True)

        ''Select Case tbcon.SelectedTab.Name
        ''    Case ""
        ''    Case Else
        ''        SetResultButtonText(cmdresult, chtprofit, grdprofit, True)
        ''        SetResultButtonText(cmdresult, chtdelta, grddelta, True)
        ''        SetResultButtonText(cmdresult, chtgamma, grdgamma, True)
        ''        SetResultButtonText(cmdresult, chtvega, grdvega, True)
        ''        SetResultButtonText(cmdresult, chttheta, grdtheta, True)
        ''        SetResultButtonText(cmdresult, chtvolga, grdvolga, True)
        ''        SetResultButtonText(cmdresult, chtvanna, grdvanna, True)
        ''End Select

    End Sub
    Private Sub SetChartGridForAll(ByVal cht As AxMSChart20Lib.AxMSChart, ByVal grd As DataGridView, ByVal lbl As System.Windows.Forms.Label, ByVal Flg As Boolean)
        cht.Visible = Flg
        lbl.Visible = Flg
        grd.Visible = Not Flg
    End Sub
    REM On change of cmp , if interval is 0, it calculate interval
    Private Sub CalInterval()
        If Val(txtmkt.Text) > 0 Then
            txtmid = Val(txtmkt.Text)
            If Val(txtmid) < 100 Then
                txtinterval.Text = 1
            ElseIf Val(txtmid) > 100 And Val(txtmid) < 500 Then
                txtinterval.Text = 5
            ElseIf Val(txtmid) > 100 And Val(txtmid) < 1000 Then
                txtinterval.Text = 10
            ElseIf Val(txtmid) > 1000 And Val(txtmid) < 10000 Then
                txtinterval.Text = 100
            ElseIf Val(txtmid) > 10000 Then
                txtinterval.Text = 500
            End If
            interval = txtinterval.Text
        End If
    End Sub

    Private Sub txtllimit_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtllimit.Leave
        REM When scenario calculation passes value as negative in ltp, then it gives an overflow in values,For this Set Calulation Of Interval And No Of Strikes, multiplication of interval and no Of Strike must be less than CMP
        If Val(txtmkt.Text) > 0 And Val(txtinterval.Text) > 0 And Val(txtllimit.Text) > 0 Then
            If Val(txtllimit.Text) > Math.Round(Val(txtmkt.Text) / Val(txtinterval.Text)) Then
                MsgBox("No. of Strike +/- Must Be Less Than " & Math.Round(Val(txtmkt.Text) / Val(txtinterval.Text), 0), MsgBoxStyle.Information)
                txtllimit.Focus()
                Exit Sub
            End If
        End If
    End Sub
    REM Whenever any line is unchecked delta of those option must be removed from that line as well
    Private Sub grdtrad_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdtrad.CurrentCellDirtyStateChanged
        If grdtrad.IsCurrentCellDirty Then
            grdtrad.CommitEdit(DataGridViewDataErrorContexts.Commit)
            'grdtrad.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange)
            'grdtrad.EndEdit()
        End If
        'If grdtrad.IsCurrentCellInEditMode Then
        'grdtrad.CommitEdit(DataGridViewDataErrorContexts.Commit)
        'End If
    End Sub
    Private Sub ButResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButResult.Click
        REM  For Change In Grid Data Or Volatality Increment Click On Result Button  Show Charts
        result(False)
    End Sub
End Class
'' ''Old Code 15/04/2011
'' ''Imports System.Windows.Forms.DataVisualization.Charting
' ''Imports MSChart20Lib
' ''Imports System.io
' ''Imports System.Data
' ''Imports System.Data.OleDb
' ''Public Class scenario
' ''    Public objProfitLossChart As New frmprofitLossChart
' ''    Public VarIsCurrency As Boolean = False

' ''    Dim VarIsFrmLoad As Boolean = False
' ''    Dim VarIsFirstLoad As Boolean = False
' ''#Region "Variable"
' ''    Dim XResolution As Integer
' ''    Public Shared cellno As Integer
' ''    Public isVolCal As Boolean = False
' ''    Public Shared chartName As String
' ''    Public Shared ValLBLprofit As String
' ''    Public Shared ValLBLgamma As String
' ''    Public Shared ValLBLtheta As String
' ''    Public Shared ValLBLvega As String
' ''    Public Shared ValLBLdelta As String
' ''    Public Shared chkscenario As Boolean
' ''    Dim objExp As New expenses
' ''    Dim objScenarioDetail As New scenarioDetail
' ''    'Dim cpdtable As New DataTable
' ''    Dim fdtable As New DataTable
' ''    Dim profit As New DataTable
' ''    Dim deltatable As New DataTable
' ''    Dim gammatable As New DataTable
' ''    Dim vegatable As New DataTable
' ''    Dim thetatable As New DataTable
' ''    Dim mObjData As New DataAnalysis.AnalysisData
' ''    Dim Mrateofinterast As Double = 0
' ''    Dim objTrad As trading = New trading
' ''    Dim rtable As DataTable
' ''    Dim flgUnderline As Boolean = False
' ''    Public trscen As Boolean = False
' ''    Public time1, time2 As Date
' ''    Public mvalue As Double
' ''    'Dim roundGamma As Integer
' ''    'Dim roundTheta As Integer
' ''    'Dim roundDelta As Integer
' ''    'Dim roundVega As Integer
' ''    Dim gcheck As Boolean = False
' ''    Public scname As String
' ''    Dim txtmid As Double
' ''    Dim interval As Double
' ''    Dim checkall As Boolean = True
' ''    Dim grossmtm As Double
' ''    Dim contMenu() As ContextMenuStrip
' ''    Dim contMenuExpiry() As ContextMenuStrip
' ''    Dim frmFlg As Boolean = False

' ''    Dim involflag As Boolean = False
' ''    Dim decvolflag As Boolean = False
' ''#End Region
' ''    Private Sub scenario_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
' ''        If Not objanalysis Is Nothing Then
' ''            objanalysis.refreshstarted = True
' ''        End If
' ''        Me.Icon = My.Resources.volhedge_icon
' ''        'Me.WindowState = FormWindowState.Maximized
' ''        'Me.Refresh()
' ''    End Sub
' ''    Private Sub scenario_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
' ''        chkscenario = False
' ''        txtmkt.Text = 0
' ''        'If Not objanalysis Is Nothing Then
' ''        '    objanalysis.refreshstarted = True
' ''        'End If
' ''        If MsgBox("Do you want to save Interval and No. of Strike ??   ", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Save") = MsgBoxResult.Yes Then
' ''            objScenarioDetail.Delete_scenario()
' ''            objScenarioDetail.Interval = Val(txtinterval.Text)
' ''            objScenarioDetail.Strike = Val(txtllimit.Text)
' ''            objScenarioDetail.Interval_type = IIf(chkint.Checked = True, "Per", "Value")
' ''            objScenarioDetail.Insert_scenario()
' ''        End If
' ''          Call analysis.searchcompany()
' ''    End Sub
' ''    Private Sub scenario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
' ''        VarIsFrmLoad = True
' ''        VarIsFirstLoad = True
' ''        Me.WindowState = FormWindowState.Maximized
' ''        If Not objanalysis Is Nothing Then
' ''            objanalysis.refreshstarted = False
' ''        End If
' ''        chkscenario = True
' ''        Me.Text = Me.Text & "-" & scname
' ''        If scenariotable.Rows.Count > 0 Then
' ''            grossmtm = scenariotable.Rows(0)("grossMTM")
' ''        End If
' ''        If VarIsCurrency = False Then
' ''            txtinterast.Text = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
' ''        ElseIf VarIsCurrency = True Then
' ''            txtinterast.Text = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='CurrrencyRateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
' ''        End If
' ''        Mrateofinterast = Val(txtinterast.Text)
' ''        'roundTheta = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundTheta'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundTheta'")))
' ''        'roundDelta = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundDelta'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundDelta'")))
' ''        'roundVega = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundVega'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundVega'")))
' ''        'roundGamma = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundGamma'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='RoundGamma'")))
' ''        txtcvol.Text = 0
' ''        txtpvol.Text = 0
' ''        txtmid = 0
' ''        If trscen = True Then
' ''            chkcheck.Checked = True
' ''            gcheck = True
' ''            dttoday.Value = time1
' ''            dtexp.Value = time2
' ''            txtdays.Text = DateDiff(DateInterval.Day, time1.Date, time2.Date)
' ''            If dttoday.Value.Date < dtexp.Value.Date Then
' ''                txtdays.Text = Val(txtdays.Text) + 1
' ''            ElseIf dttoday.Value.Date = dtexp.Value.Date Then
' ''                txtdays.Text = Val(txtdays.Text) + 0.5
' ''            End If
' ''            txtmkt.Text = Math.Round(mvalue, 0)
' ''            txtmid = mvalue
' ''            Dim grow As New DataGridViewRow
' ''            For Each drow As DataRow In scenariotable.Rows
' ''                grow = New DataGridViewRow
' ''                grdtrad.Rows.Add(grow)
' ''                'grdtrad.Refresh()
' ''            Next
' ''            Dim i As Integer = 0
' ''            txtunits.Text = 0
' ''            txtdelval.Text = 0
' ''            txtthval.Text = 0
' ''            txtvgval.Text = 0
' ''            txtgmval.Text = 0

' ''            ''divyesh
' ''            txtunits1.Text = 0
' ''            txtdelval1.Text = 0
' ''            txtthval1.Text = 0
' ''            txtvgval1.Text = 0
' ''            txtgmval1.Text = 0
' ''            For Each drow As DataRow In scenariotable.Select("", "cpf")
' ''                grdtrad.Rows(i).Cells("Active").Value = CBool(drow("status"))
' ''                grdtrad.Rows(i).Cells("TimeI").Value = CDate(drow("time1"))
' ''                grdtrad.Rows(i).Cells("TimeII").Value = CDate(drow("time2"))
' ''                grdtrad.Rows(i).Cells("CPF").Value = CStr(drow("cpf"))
' ''                grdtrad.Rows(i).Cells("spval").Value = CDbl(drow("spot"))
' ''                grdtrad.Rows(i).Cells("Strike").Value = CDbl(drow("strikes"))
' ''                grdtrad.Rows(i).Cells("units").Value = Val(drow("qty"))
' ''                grdtrad.Rows(i).Cells("ltp").Value = Val(drow("ltp"))
' ''                grdtrad.Rows(i).Cells("last").Value = CDbl(drow("rate"))
' ''                If Val(drow("vol")) = 0 Then
' ''                    grdtrad.Rows(i).Cells("lv").Value = CDbl(0.0)
' ''                Else
' ''                    grdtrad.Rows(i).Cells("lv").Value = Val(drow("vol"))
' ''                End If
' ''                grdtrad.Rows(i).Cells("delta").Value = Val(drow("delta"))
' ''                grdtrad.Rows(i).Cells("deltaval").Value = Val(drow("deltaval"))
' ''                grdtrad.Rows(i).Cells("gamma").Value = Val(drow("gamma"))
' ''                grdtrad.Rows(i).Cells("gmval").Value = Val(drow("gmval"))
' ''                grdtrad.Rows(i).Cells("vega").Value = Val(drow("vega"))
' ''                grdtrad.Rows(i).Cells("vgval").Value = Val(drow("vgval"))
' ''                grdtrad.Rows(i).Cells("theta").Value = Val(drow("theta"))
' ''                grdtrad.Rows(i).Cells("thetaval").Value = Val(drow("thetaval"))
' ''                grdtrad.Rows(i).Cells("uid").Value = Val(drow("uid"))

' ''                ''divyesh
' ''                grdtrad.Rows(i).Cells("delta1").Value = Val(drow("delta"))
' ''                grdtrad.Rows(i).Cells("gamma1").Value = Val(drow("gamma"))
' ''                grdtrad.Rows(i).Cells("vega1").Value = Val(drow("vega"))
' ''                grdtrad.Rows(i).Cells("theta1").Value = Val(drow("theta"))

' ''                grdtrad.Rows(i).Cells("deltaval1").Value = Val(drow("deltaval1"))
' ''                grdtrad.Rows(i).Cells("gmval1").Value = Val(drow("gmval1"))
' ''                grdtrad.Rows(i).Cells("vgval1").Value = Val(drow("vgval1"))
' ''                grdtrad.Rows(i).Cells("thetaval1").Value = Val(drow("thetaval1"))


' ''                grdtrad.Rows(i).Cells("ltp1").Value = Val(drow("ltp1"))

' ''                'txtunits.Text = Math.Round(Val(txtunits.Text) + Val(grdtrad.Rows(i).Cells("ltp").Value), 2)
' ''                txtdelval.Text = Format(Val(txtdelval.Text) + Val(grdtrad.Rows(i).Cells("deltaval").Value), Deltastr_Val)
' ''                txtthval.Text = Format(Val(txtthval.Text) + Val(grdtrad.Rows(i).Cells("thetaval").Value), Thetastr_Val)
' ''                txtvgval.Text = Format(Val(txtvgval.Text) + Val(grdtrad.Rows(i).Cells("vgval").Value), Vegastr_Val)
' ''                txtgmval.Text = Format(Val(txtgmval.Text) + Val(grdtrad.Rows(i).Cells("gmval").Value), Gammastr_Val)

' ''                ''divyesh 
' ''                txtdelval1.Text = Format(Val(txtdelval1.Text) + Val(grdtrad.Rows(i).Cells("deltaval1").Value), Deltastr_Val)
' ''                txtthval1.Text = Format(Val(txtthval1.Text) + Val(grdtrad.Rows(i).Cells("thetaval1").Value), Thetastr_Val)
' ''                txtvgval1.Text = Format(Val(txtvgval1.Text) + Val(grdtrad.Rows(i).Cells("vgval1").Value), Vegastr_Val)
' ''                txtgmval1.Text = Format(Val(txtgmval1.Text) + Val(grdtrad.Rows(i).Cells("gmval1").Value), Gammastr_Val)

' ''                i += 1
' ''            Next
' ''            grdtrad.Columns("TimeII").DefaultCellStyle.NullValue = Format(scenariotable.Rows(0).Item("time2"), "MM/dd/yyyy")
' ''            scenariotable.Rows.Clear()
' ''            trscen = False
' ''        End If


' ''        'to get interval,No of strike from database
' ''        Dim dtScenario As New DataTable
' ''        dtScenario = objScenarioDetail.select_scenario
' ''        If dtScenario.Rows.Count > 0 Then
' ''            If dtScenario.Rows(0)("interval_type").ToString = "Per" Then
' ''                chkint.Checked = True
' ''            Else
' ''                chkint.Checked = False
' ''            End If
' ''            txtinterval.Text = Val(dtScenario.Rows(0)("interval").ToString)
' ''            txtllimit.Text = Val(dtScenario.Rows(0)("strike").ToString)
' ''            interval = txtinterval.Text
' ''        Else
' ''            If Val(txtmkt.Text) > 0 Then
' ''                txtmid = Val(txtmkt.Text)
' ''                If Val(txtmid) < 100 Then
' ''                    txtinterval.Text = 1
' ''                ElseIf Val(txtmid) > 100 And Val(txtmid) < 500 Then
' ''                    txtinterval.Text = 5
' ''                ElseIf Val(txtmid) > 100 And Val(txtmid) < 1000 Then
' ''                    txtinterval.Text = 10
' ''                ElseIf Val(txtmid) > 1000 And Val(txtmid) < 10000 Then
' ''                    txtinterval.Text = 100
' ''                ElseIf Val(txtmid) > 10000 Then
' ''                    txtinterval.Text = 500
' ''                End If
' ''                interval = txtinterval.Text
' ''                'interval = Math.Round(val(txtmid) * 1 / 100, 0)
' ''            End If
' ''        End If

' ''        txtunits.Text = Math.Round(cal_profitLoss, RoundGrossMTM)
' ''        ''divyesh
' ''        txtunits1.Text = Math.Round(cal_FutprofitLoss, RoundGrossMTM)

' ''        txtdays.Text = 0
' ''        If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
' ''            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
' ''        End If
' ''        If dttoday.Value.Date < dtexp.Value.Date Then
' ''            txtdays.Text = Val(txtdays.Text) + 1
' ''        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
' ''            txtdays.Text = Val(txtdays.Text) + 0.5
' ''        End If

' ''        create_active()
' ''        'create contextmenustrip for vol column cells
' ''        ReDim contMenu(grdtrad.Rows.Count - 1)
' ''        Dim cnt As Integer = 0
' ''        For Each cntmenu As ContextMenuStrip In contMenu
' ''            cntmenu = New ContextMenuStrip
' ''            Dim item As New ToolStripMenuItem("Unfreeze")
' ''            ' item.CheckOnClick = True
' ''            item.Tag = cnt.ToString
' ''            AddHandler item.Click, AddressOf freezVol
' ''            AddHandler cntmenu.Opening, AddressOf contMenuOpen
' ''            cntmenu.Items.Add(item)
' ''            grdtrad.Rows(cnt).Cells("lv").ContextMenuStrip = cntmenu
' ''            cnt += 1
' ''        Next

' ''        'create contextmenustrip for expiry column cells
' ''        ReDim contMenuExpiry(grdtrad.Rows.Count - 1)
' ''        cnt = 0
' ''        For Each cntmenu As ContextMenuStrip In contMenuExpiry
' ''            cntmenu = New ContextMenuStrip
' ''            Dim item As New ToolStripMenuItem("Unfreeze")
' ''            ' item.CheckOnClick = True
' ''            item.Tag = cnt.ToString
' ''            AddHandler item.Click, AddressOf freezExpiry
' ''            AddHandler cntmenu.Opening, AddressOf contMenuOpenExpiry
' ''            cntmenu.Items.Add(item)
' ''            grdtrad.Rows(cnt).Cells("TimeII").ContextMenuStrip = cntmenu
' ''            cnt += 1
' ''        Next

' ''        For Each col As DataGridViewColumn In grdtrad.Columns
' ''            If col.Index >= 9 Then
' ''                col.HeaderCell.Style.BackColor = Color.Gray
' ''            End If
' ''            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
' ''        Next

' ''        Call Vol_Click(Vol, e)
' ''        Call expiry_Click(expiry, e)
' ''        If scname <> "" Then
' ''            result()
' ''        End If
' ''        'For Each drow As DataGridViewRow In grdtrad.Rows
' ''        '    drow.Cells("VolORG").Value = drow.Cells("lv").Value
' ''        'Next
' ''        If analysis.chkanalysis = True Then
' ''            MDI.ToolStripcompanyCombo.Visible = False
' ''            MDI.ToolStripMenuSearchComp.Visible = False
' ''        End If
' ''    End Sub
' ''    Private Sub cal_summary()
' ''        txtdelval.Text = 0
' ''        txtthval.Text = 0
' ''        txtvgval.Text = 0
' ''        txtgmval.Text = 0

' ''        txtdelval1.Text = 0
' ''        txtthval1.Text = 0
' ''        txtvgval1.Text = 0
' ''        txtgmval1.Text = 0

' ''        txtunits.Text = Math.Round(cal_profitLoss, RoundGrossMTM)
' ''        txtunits1.Text = Math.Round(cal_FutprofitLoss, RoundGrossMTM)

' ''        For Each grow As DataGridViewRow In grdtrad.Rows
' ''            If grow.Cells("Active").Value = True Then
' ''                txtdelval.Text = Format(Val(txtdelval.Text) + Val(grow.Cells("deltaval").Value), Deltastr_Val)
' ''                txtthval.Text = Format(Val(txtthval.Text) + Val(grow.Cells("thetaval").Value), Thetastr_Val)
' ''                txtvgval.Text = Format(Val(txtvgval.Text) + Val(grow.Cells("vgval").Value), Vegastr_Val)
' ''                txtgmval.Text = Format(Val(txtgmval.Text) + Val(grow.Cells("gmval").Value), Gammastr_Val)

' ''                txtdelval1.Text = Format(Val(txtdelval1.Text) + Val(grow.Cells("deltaval1").Value), Deltastr_Val)
' ''                txtthval1.Text = Format(Val(txtthval1.Text) + Val(grow.Cells("thetaval1").Value), Thetastr_Val)
' ''                txtvgval1.Text = Format(Val(txtvgval1.Text) + Val(grow.Cells("vgval1").Value), Vegastr_Val)
' ''                txtgmval1.Text = Format(Val(txtgmval1.Text) + Val(grow.Cells("gmval1").Value), Gammastr_Val)
' ''            End If
' ''        Next
' ''    End Sub
' ''    Private Sub contMenuOpen(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
' ''        'Dim ind As Integer
' ''        'ind = CType(sender, ContextMenuStrip).Items(0).Tag
' ''        If cellno = -1 Then Exit Sub
' ''        If grdtrad.Rows(cellno).Cells("lv").ReadOnly = True Then
' ''            CType(sender, ContextMenuStrip).Items(0).Text = "Unfreeze"
' ''        Else
' ''            CType(sender, ContextMenuStrip).Items(0).Text = "Freeze"
' ''        End If
' ''    End Sub
' ''    Private Sub contMenuOpenExpiry(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
' ''        'Dim ind As Integer
' ''        'ind = CType(sender, ContextMenuStrip).Items(0).Tag
' ''        If cellno = -1 Then Exit Sub
' ''        If grdtrad.Rows(cellno).Cells("TimeII").ReadOnly = True Then
' ''            CType(sender, ContextMenuStrip).Items(0).Text = "Unfreeze"
' ''        Else
' ''            CType(sender, ContextMenuStrip).Items(0).Text = "Freeze"
' ''        End If
' ''    End Sub

' ''    Private Sub freezVol(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If cellno = -1 Then Exit Sub
' ''        If CType(sender, ToolStripMenuItem).Text = "Unfreeze" Then
' ''            grdtrad.Rows(cellno).Cells("lv").ReadOnly = False
' ''            CType(sender, ToolStripMenuItem).Text = "Freeze"
' ''        Else
' ''            grdtrad.Rows(cellno).Cells("lv").ReadOnly = True
' ''            CType(sender, ToolStripMenuItem).Text = "Unfreeze"
' ''        End If
' ''    End Sub
' ''    Private Sub freezExpiry(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If cellno = -1 Then Exit Sub
' ''        If CType(sender, ToolStripMenuItem).Text = "Unfreeze" Then
' ''            grdtrad.Rows(cellno).Cells("TimeII").ReadOnly = False
' ''            CType(sender, ToolStripMenuItem).Text = "Freeze"
' ''        Else
' ''            grdtrad.Rows(cellno).Cells("TimeII").ReadOnly = True
' ''            CType(sender, ToolStripMenuItem).Text = "Unfreeze"
' ''        End If
' ''    End Sub
' ''    Private Sub scenario_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
' ''        If e.KeyCode = Keys.F9 Then
' ''            'grdtrad.EndEdit()
' ''            'grdtrad.Refresh()
' ''            If Val(txtllimit.Text) > 0 And Val(txtllimit.Text > 0) And Val(interval) > 0 And Val(txtmid) > 0 Then
' ''                init_table()
' ''                cal_profit()
' ''                create_chart_profit()
' ''                create_chart_profit_Full()
' ''                cal_delta()
' ''                create_chart_delta()
' ''                create_chart_delta_Full()
' ''                cal_gamma()
' ''                create_chart_gamma()
' ''                create_chart_gamma_Full()
' ''                cal_vega()
' ''                create_chart_vega()
' ''                create_chart_vega_Full()
' ''                cal_theta()
' ''                create_chart_theta()
' ''                create_chart_theta_Full()
' ''                'grdprofit.DataSource = profit
' ''                'grdprofit.Refresh()
' ''                'grddelta.DataSource = deltatable
' ''                'grddelta.Refresh()
' ''                'grdgamma.DataSource = gammatable
' ''                'grdgamma.Refresh()
' ''                'grdvega.DataSource = vegatable
' ''                'grdvega.Refresh()
' ''                'grdtheta.DataSource = thetatable
' ''                'grdtheta.Refresh()
' ''            End If
' ''        ElseIf e.KeyCode = Keys.F1 Then
' ''            result()
' ''        ElseIf e.KeyCode = Keys.F11 Then
' ''            export()
' ''        ElseIf e.KeyCode = Keys.F12 Then
' ''            import()
' ''        ElseIf e.KeyCode = Keys.PageDown Then
' ''            If tbcon.SelectedIndex < 9 Then tbcon.SelectTab(tbcon.SelectedIndex + 1)
' ''        ElseIf e.KeyCode = Keys.PageUp Then
' ''            If tbcon.SelectedIndex > 0 Then tbcon.SelectTab(tbcon.SelectedIndex - 1)
' ''        End If
' ''    End Sub
' ''#Region "Initial"
' ''    Private Sub init_datatable()
' ''        'cpdtable = New DataTable
' ''        'With cpdtable.Columns

' ''        '    .Add("status", GetType(Boolean))
' ''        '    .Add("mdate", GetType(Date))
' ''        '    .Add("spval", GetType(Double))
' ''        '    .Add("strikes", GetType(Double))
' ''        '    .Add("cp")
' ''        '    .Add("units", GetType(Double))
' ''        '    .Add("last", GetType(Double))
' ''        '    .Add("lv", GetType(Double))
' ''        '    .Add("delta", GetType(Double))
' ''        '    .Add("deltaval", GetType(Double))
' ''        '    .Add("theta", GetType(Double))
' ''        '    .Add("thetaval", GetType(Double))
' ''        '    .Add("vega", GetType(Double))
' ''        '    .Add("vgval", GetType(Double))
' ''        '    .Add("gamma", GetType(Double))
' ''        '    .Add("gmval", GetType(Double))
' ''        '    .Add("uid", GetType(Integer))

' ''        'End With
' ''    End Sub
' ''    Private Sub init_table()

' ''        If Val(txtllimit.Text > 0) And Val(interval) > 0 And Val(txtmid) > 0 Then
' ''            profit = New DataTable
' ''            'grdprofit.Columns.Clear()
' ''            deltatable = New DataTable
' ''            gammatable = New DataTable
' ''            vegatable = New DataTable
' ''            thetatable = New DataTable
' ''            Dim i As Integer = 0
' ''            If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
' ''                i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
' ''            End If
' ''            i += 1
' ''            Dim j As Integer
' ''            j = 0
' ''            Dim start As Double
' ''            Dim endd As Double
' ''            start = Val(txtmid) - (Val(txtllimit.Text) * Val(interval))
' ''            endd = Val(txtmid) + (Val(txtllimit.Text) * Val(interval))
' ''            Dim drow As DataRow

' ''            '######################################################################################
' ''            grdprofit.DataSource = Nothing
' ''            'grdprofit.Refresh()
' ''            'grdprofit.Rows.Clear()
' ''            If grdprofit.Columns.Count > 0 Then
' ''                grdprofit.Columns.Clear()
' ''            End If
' ''            Dim style1 As New DataGridViewCellStyle
' ''            style1.Format = "N2"
' ''            Dim acol As DataGridViewTextBoxColumn

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "SpotValue"
' ''            acol.DataPropertyName = "SpotValue"
' ''            acol.Frozen = True
' ''            grdprofit.Columns.Add(acol)

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "Percent(%)"
' ''            acol.DataPropertyName = "Percent(%)"
' ''            acol.Frozen = True
' ''            grdprofit.Columns.Add(acol)


' ''            Dim grow As DataGridViewRow
' ''            For Each grow In grdact.Rows

' ''                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                        If grow.Cells(gcol.Index).Value = True Then
' ''                            acol = New DataGridViewTextBoxColumn
' ''                            acol.HeaderText = gcol.HeaderText
' ''                            acol.DataPropertyName = gcol.DataPropertyName
' ''                            acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
' ''                            'acol.DefaultCellStyle.Format = RoundGrossMTM

' ''                            grdprofit.Columns.Add(acol)
' ''                            'acol.DefaultCellStyle.Format = "N2"


' ''                        End If
' ''                    End If
' ''                Next
' ''            Next


' ''            With profit.Columns
' ''                .Add("SpotValue", GetType(Double))
' ''                .Add("Percent(%)", GetType(Double))
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                Dim c As String = gcol.DataPropertyName
' ''                                .Add(c)
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''            End With
' ''            'FOR MINUS LIMIT(STRI) FROM MID
' ''            Dim inter As Double = 0
' ''            Dim int As Double = 0
' ''            For j = 1 To Val(txtllimit.Text) + 1
' ''                drow = profit.NewRow

' ''                ' drow("spotvalue") = (val(interval) * j) + start
' ''                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
' ''                    drow("Percent(%)") = -(inter * 100) '& " %"
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)

' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                int += 1
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                drow(gcol.DataPropertyName) = 0
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                profit.Rows.Add(drow)
' ''            Next
' ''            'FOR PLUS LIMIT(STRI) FROM MID
' ''            inter = 0
' ''            For j = 1 To Val(txtllimit.Text)
' ''                drow = profit.NewRow
' ''                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = (Val(txtinterval.Text) * Val(j)) / 100
' ''                    drow("Percent(%)") = inter * 100 '& " %"
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                drow(gcol.DataPropertyName) = 0
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                profit.Rows.Add(drow)
' ''            Next


' ''            '######################################################################################

' ''            grddelta.DataSource = Nothing
' ''            'grddelta.Refresh()
' ''            'grdprofit.Rows.Clear()
' ''            If grddelta.Columns.Count > 0 Then
' ''                grddelta.Columns.Clear()
' ''            End If

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "SpotValue"
' ''            acol.DataPropertyName = "SpotValue"
' ''            acol.Frozen = True
' ''            grddelta.Columns.Add(acol)

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "Percent(%)"
' ''            acol.DataPropertyName = "Percent(%)"
' ''            acol.Frozen = True
' ''            grddelta.Columns.Add(acol)

' ''            For Each grow In grdact.Rows
' ''                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                        If grow.Cells(gcol.Index).Value = True Then
' ''                            If (IsDate(gcol.DataPropertyName)) Then
' ''                                acol = New DataGridViewTextBoxColumn
' ''                                acol.HeaderText = gcol.HeaderText
' ''                                acol.DataPropertyName = gcol.DataPropertyName
' ''                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
' ''                                grddelta.Columns.Add(acol)
' ''                            End If
' ''                        End If
' ''                    End If
' ''                Next
' ''            Next


' ''            '''''''''''''''''' Detla Table Initalise

' ''            With deltatable.Columns
' ''                .Add("SpotValue", GetType(Double))
' ''                .Add("Percent(%)", GetType(Double))
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                Dim c As String = gcol.DataPropertyName
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    .Add(c)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''            End With
' ''            inter = 0
' ''            int = 0
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = deltatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            For j = 1 To Val(txtllimit.Text) + 1
' ''                drow = deltatable.NewRow

' ''                ' drow("spotvalue") = (val(interval) * j) + start
' ''                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
' ''                drow("Percent(%)") = 0
' ''                If chkint.Checked = True Then
' ''                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
' ''                    drow("Percent(%)") = -(inter * 100)
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
' ''                    'drow("Percent(%)") = Math.Round(((val(drow("SPOTVALUE")) / val(txtmid)) - 1) * 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                int += 1
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                deltatable.Rows.Add(drow)
' ''            Next
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = deltatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''            'FOR PLUS LIMIT(STRI) FROM MID
' ''            inter = 0
' ''            For j = 1 To Val(txtllimit.Text)
' ''                drow = deltatable.NewRow
' ''                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = (Val(txtinterval.Text) * Val(j)) / 100
' ''                    drow("Percent(%)") = inter * 100
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                deltatable.Rows.Add(drow)
' ''            Next
' ''            '######################################################################################

' ''            '############# Gamma Table Init
' ''            grdgamma.DataSource = Nothing
' ''            'grdgamma.Refresh()
' ''            'grdprofit.Rows.Clear()
' ''            If grdgamma.Columns.Count > 0 Then
' ''                grdgamma.Columns.Clear()
' ''            End If

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "SpotValue"
' ''            acol.DataPropertyName = "SpotValue"
' ''            acol.Frozen = True
' ''            grdgamma.Columns.Add(acol)

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "Percent(%)"
' ''            acol.DataPropertyName = "Percent(%)"
' ''            acol.Frozen = True
' ''            grdgamma.Columns.Add(acol)

' ''            For Each grow In grdact.Rows
' ''                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                        If grow.Cells(gcol.Index).Value = True Then
' ''                            If (IsDate(gcol.DataPropertyName)) Then
' ''                                acol = New DataGridViewTextBoxColumn
' ''                                acol.HeaderText = gcol.HeaderText
' ''                                acol.DataPropertyName = gcol.DataPropertyName
' ''                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
' ''                                grdgamma.Columns.Add(acol)
' ''                            End If
' ''                        End If
' ''                    End If
' ''                Next
' ''            Next



' ''            With gammatable.Columns
' ''                .Add("SpotValue", GetType(Double))
' ''                .Add("Percent(%)", GetType(Double))
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                Dim c As String = gcol.DataPropertyName
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    .Add(c)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''            End With
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = gammatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            inter = 0
' ''            int = 0
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = deltatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            For j = 1 To Val(txtllimit.Text) + 1
' ''                drow = gammatable.NewRow

' ''                ' drow("spotvalue") = (val(interval) * j) + start
' ''                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
' ''                drow("Percent(%)") = 0
' ''                If chkint.Checked = True Then
' ''                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
' ''                    drow("Percent(%)") = -(inter * 100)
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                int += 1
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                gammatable.Rows.Add(drow)
' ''            Next
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = gammatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''            inter = 0
' ''            For j = 1 To Val(txtllimit.Text)
' ''                drow = gammatable.NewRow
' ''                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = (Val(txtinterval.Text) * Val(j)) / 100
' ''                    drow("Percent(%)") = inter * 100
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                gammatable.Rows.Add(drow)
' ''            Next


' ''            ' Vega Table INit ######################################################################################
' ''            grdvega.DataSource = Nothing
' ''            ' grdvega.Refresh()
' ''            'grdprofit.Rows.Clear()
' ''            If grdvega.Columns.Count > 0 Then
' ''                grdvega.Columns.Clear()
' ''            End If


' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "SpotValue"
' ''            acol.DataPropertyName = "SpotValue"
' ''            acol.Frozen = True
' ''            grdvega.Columns.Add(acol)

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "Percent(%)"
' ''            acol.DataPropertyName = "Percent(%)"
' ''            acol.Frozen = True
' ''            grdvega.Columns.Add(acol)

' ''            For Each grow In grdact.Rows
' ''                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                        If grow.Cells(gcol.Index).Value = True Then
' ''                            If (IsDate(gcol.DataPropertyName)) Then
' ''                                acol = New DataGridViewTextBoxColumn
' ''                                acol.HeaderText = gcol.HeaderText
' ''                                acol.DataPropertyName = gcol.DataPropertyName
' ''                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
' ''                                grdvega.Columns.Add(acol)
' ''                            End If
' ''                        End If
' ''                    End If
' ''                Next
' ''            Next


' ''            With vegatable.Columns
' ''                .Add("SpotValue", GetType(Double))
' ''                .Add("Percent(%)", GetType(Double))
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                Dim c As String = gcol.DataPropertyName
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    .Add(c)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''            End With
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = vegatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            inter = 0
' ''            int = 0
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = deltatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            For j = 1 To Val(txtllimit.Text) + 1
' ''                drow = vegatable.NewRow

' ''                ' drow("spotvalue") = (val(interval) * j) + start
' ''                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
' ''                drow("Percent(%)") = 0
' ''                If chkint.Checked = True Then
' ''                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
' ''                    drow("Percent(%)") = -(inter * 100)
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                int += 1
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                vegatable.Rows.Add(drow)
' ''            Next
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = vegatable.NewRow
' ''            'drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''            inter = 0
' ''            For j = 1 To Val(txtllimit.Text)
' ''                drow = vegatable.NewRow
' ''                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = (Val(txtinterval.Text) * Val(j)) / 100
' ''                    drow("Percent(%)") = inter * 100
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                vegatable.Rows.Add(drow)
' ''            Next
' ''            '######################################################################################
' ''            grdtheta.DataSource = Nothing
' ''            'grdtheta.Refresh()
' ''            'grdprofit.Rows.Clear()
' ''            If grdtheta.Columns.Count > 0 Then
' ''                grdtheta.Columns.Clear()
' ''            End If

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "SpotValue"
' ''            acol.DataPropertyName = "SpotValue"
' ''            acol.Frozen = True
' ''            grdtheta.Columns.Add(acol)

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "Percent(%)"
' ''            acol.DataPropertyName = "Percent(%)"
' ''            acol.Frozen = True
' ''            grdtheta.Columns.Add(acol)

' ''            For Each grow In grdact.Rows
' ''                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                        If grow.Cells(gcol.Index).Value = True Then
' ''                            If (IsDate(gcol.DataPropertyName)) Then
' ''                                acol = New DataGridViewTextBoxColumn
' ''                                acol.HeaderText = gcol.HeaderText
' ''                                acol.DataPropertyName = gcol.DataPropertyName
' ''                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
' ''                                grdtheta.Columns.Add(acol)
' ''                            End If
' ''                        End If
' ''                    End If
' ''                Next
' ''            Next


' ''            With thetatable.Columns
' ''                .Add("SpotValue", GetType(Double))
' ''                .Add("Percent(%)", GetType(Double))
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                Dim c As String = gcol.DataPropertyName
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    .Add(c)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''            End With
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = thetatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            inter = 0
' ''            int = 0
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = deltatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            For j = 1 To Val(txtllimit.Text) + 1
' ''                drow = thetatable.NewRow

' ''                ' drow("spotvalue") = (val(interval) * j) + start
' ''                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
' ''                drow("Percent(%)") = 0
' ''                If chkint.Checked = True Then
' ''                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
' ''                    drow("Percent(%)") = -(inter * 100)
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                int += 1
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                thetatable.Rows.Add(drow)
' ''            Next
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = thetatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''            inter = 0
' ''            For j = 1 To Val(txtllimit.Text)
' ''                drow = thetatable.NewRow
' ''                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = (Val(txtinterval.Text) * Val(j)) / 100
' ''                    drow("Percent(%)") = inter * 100
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0

' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                thetatable.Rows.Add(drow)
' ''            Next
' ''        Else
' ''            MsgBox("enter Value")
' ''        End If
' ''    End Sub
' ''    Private Sub init_table1()

' ''        'If val(txtllimit.Text) > 0 And val(txtllimit.Text > 0) And val(interval) > 0 And val(txtmid) > 0 Then
' ''        '    profit = New DataTable

' ''        '    deltatable = New DataTable
' ''        '    gammatable = New DataTable
' ''        '    vegatable = New DataTable
' ''        '    thetatable = New DataTable
' ''        '    Dim i As Integer = 0
' ''        '    If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''        '        i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
' ''        '    End If
' ''        '    Dim j As Integer
' ''        '    j = 0
' ''        '    Dim start As Double
' ''        '    Dim endd As Double
' ''        '    start = val(txtmid.Text) - (val(txtllimit.Text) * val(interval))
' ''        '    endd = val(txtmid.Text) + (val(txtllimit.Text) * val(interval))
' ''        '    Dim drow As DataRow
' ''        '    '######################################################################################

' ''        '    With profit.Columns

' ''        '        .Add("SpotValue", GetType(Double))
' ''        '        For Each grow As DataGridViewRow In grdact.Rows
' ''        '            For Each gcol As DataGridViewColumn In grdact.Columns
' ''        '                If grow.Cells(gcol.Index).Value = True Then
' ''        '                    Dim c As String = gcol.HeaderText
' ''        '                    .Add(c)
' ''        '                End If
' ''        '            Next
' ''        '        Next
' ''        '        'For j = 0 To i - 1
' ''        '        '    Dim c As String = Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "" '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")
' ''        '        '    .Add(c)
' ''        '        'Next
' ''        '    End With

' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = profit.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + start
' ''        '        For Each grow As DataGridViewRow In grdact.Rows
' ''        '            For Each gcol As DataGridViewColumn In grdact.Columns
' ''        '                If grow.Cells(gcol.Index).Value = True Then
' ''        '                    drow(gcol.HeaderText) = 0
' ''        '                End If
' ''        '            Next
' ''        '        Next
' ''        '        'For k As Integer = 0 To i - 1
' ''        '        '    drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        'Next
' ''        '        profit.Rows.Add(drow)
' ''        '    Next
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = profit.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''        '        For Each grow As DataGridViewRow In grdact.Rows
' ''        '            For Each gcol As DataGridViewColumn In grdact.Columns
' ''        '                If grow.Cells(gcol.Index).Value = True Then
' ''        '                    drow(gcol.HeaderText) = 0
' ''        '                End If
' ''        '            Next
' ''        '        Next
' ''        '        'For k As Integer = 0 To i - 1
' ''        '        '    drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        'Next
' ''        '        profit.Rows.Add(drow)
' ''        '    Next
' ''        '    '######################################################################################

' ''        '    With deltatable.Columns
' ''        '        .Add("SpotValue", GetType(Double))
' ''        '        For j = 0 To i - 1
' ''        '            .Add(Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "") ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy"))
' ''        '        Next
' ''        '    End With
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = deltatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + start
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        deltatable.Rows.Add(drow)
' ''        '    Next
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = deltatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        deltatable.Rows.Add(drow)
' ''        '    Next
' ''        '    '######################################################################################

' ''        '    With gammatable.Columns
' ''        '        .Add("SpotValue", GetType(Double))
' ''        '        For j = 0 To i - 1
' ''        '            .Add(Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "") '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy"))
' ''        '        Next
' ''        '    End With
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = gammatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + start
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        gammatable.Rows.Add(drow)
' ''        '    Next
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = gammatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        gammatable.Rows.Add(drow)
' ''        '    Next


' ''        '    '######################################################################################

' ''        '    With vegatable.Columns
' ''        '        .Add("SpotValue", GetType(Double))
' ''        '        For j = 0 To i - 1
' ''        '            .Add(Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "") ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy"))
' ''        '        Next
' ''        '    End With
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = vegatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + start
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        vegatable.Rows.Add(drow)
' ''        '    Next
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = vegatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        vegatable.Rows.Add(drow)
' ''        '    Next
' ''        '    '######################################################################################

' ''        '    With thetatable.Columns
' ''        '        .Add("SpotValue", GetType(Double))
' ''        '        For j = 0 To i - 1
' ''        '            .Add(Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "") ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy"))
' ''        '        Next
' ''        '    End With
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = thetatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + start
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        thetatable.Rows.Add(drow)
' ''        '    Next
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = thetatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        thetatable.Rows.Add(drow)
' ''        '    Next





' ''        'Else
' ''        '    MsgBox("enter Value")
' ''        'End If
' ''    End Sub
' ''#End Region
' ''#Region "event"
' ''    Private Sub cmbresult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbresult.Click

' ''        result()
' ''        '  Me.tbcon.DrawMode = TabDrawMode.OwnerDrawFixed

' ''    End Sub
' ''    Private Sub result()
' ''        'If val(txtllimit.Text) <= 0 Then
' ''        '    MsgBox("Enter Upper Limit")
' ''        '    txtllimit.Focus()
' ''        '    Exit Sub
' ''        'End If
' ''        If Val(txtllimit.Text) <= 0 Then
' ''            MsgBox("Enter No of Strike +/-")
' ''            txtllimit.Focus()
' ''            Exit Sub
' ''        End If
' ''        If Val(interval) <= 0 Then
' ''            MsgBox("Enter Interval")
' ''            txtinterval.Focus()
' ''            Exit Sub
' ''        End If

' ''        'If val(txtmid) <= 0 Then
' ''        '    MsgBox("Enter Mid Value")
' ''        '    txtmid.Focus()
' ''        '    Exit Sub
' ''        'End If

' ''        If Val(txtllimit.Text > 0) And Val(interval) > 0 And Val(txtmid) > 0 Then

' ''            init_table()
' ''            If cal_profit() = True Then
' ''            Else
' ''                Exit Sub
' ''            End If
' ''            create_chart_profit()
' ''            create_chart_profit_Full()
' ''            If cal_delta() = True Then
' ''            Else
' ''                Exit Sub
' ''            End If
' ''            create_chart_delta()
' ''            create_chart_delta_Full()
' ''            If cal_gamma() = True Then
' ''            Else
' ''                Exit Sub
' ''            End If
' ''            create_chart_gamma()
' ''            Call create_chart_gamma_Full()

' ''            If cal_vega() = True Then
' ''            Else
' ''                Exit Sub
' ''            End If
' ''            create_chart_vega()
' ''            create_chart_vega_Full()

' ''            If cal_theta() = True Then
' ''            Else
' ''                Exit Sub
' ''            End If
' ''            create_chart_theta()
' ''            create_chart_theta_Full()

' ''            'grdprofit.DataSource = profit
' ''            'grdprofit.Refresh()
' ''            'grddelta.DataSource = deltatable
' ''            'grddelta.Refresh()
' ''            'grdgamma.DataSource = gammatable
' ''            'grdgamma.Refresh()
' ''            'grdvega.DataSource = vegatable
' ''            'grdvega.Refresh()
' ''            'grdtheta.DataSource = thetatable
' ''            'grdtheta.Refresh()
' ''        End If
' ''    End Sub
' ''    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        Me.Close()
' ''        'Dim analysis As New analysis
' ''        ''analysis.MdiParent = Me
' ''        'analysis.ShowDialog()
' ''    End Sub

' ''    Private Sub txtdays_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdays.KeyPress
' ''        numonly(e)
' ''    End Sub

' ''    Private Sub txtllimit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtllimit.GotFocus
' ''        SendKeys.Send("{HOME}+{END}")
' ''    End Sub
' ''    Private Sub txtllimit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtllimit.KeyPress
' ''        numonly(e)
' ''    End Sub
' ''    Private Sub txtmid_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub
' ''    Private Sub txtulimit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub

' ''    Private Sub txtinterval_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtinterval.GotFocus
' ''        SendKeys.Send("{HOME}+{END}")
' ''    End Sub
' ''    Private Sub txtinterval_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtinterval.KeyPress
' ''        numonly(e)
' ''    End Sub
' ''    Private Sub txtratediff_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub
' ''    Private Sub dttoday_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dttoday.Leave
' ''        txtdays.Text = 0
' ''        If grdtrad.Rows.Count = 2 Then
' ''            grdtrad.Rows(0).Cells("TimeI").Value = dttoday.Value
' ''        ElseIf frmFlg = True Then 'this flg used bcoz first need not to assign expiry date
' ''            For Each grow As DataGridViewRow In grdtrad.Rows
' ''                If grow.Cells("TimeI").ReadOnly = False Then
' ''                    grow.Cells("TimeI").Value = dttoday.Value
' ''                End If
' ''            Next
' ''        End If

' ''        If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
' ''            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
' ''        End If
' ''        If dttoday.Value.Date < dtexp.Value.Date Then
' ''            txtdays.Text = Val(txtdays.Text) + 1
' ''        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
' ''            txtdays.Text = Val(txtdays.Text) + 0.5
' ''        End If
' ''        create_active()
' ''    End Sub
' ''    Private Sub dtexp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtexp.Leave
' ''        txtdays.Text = 0
' ''        If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
' ''        End If
' ''        If dttoday.Value.Date < dtexp.Value.Date Then
' ''            txtdays.Text = Val(txtdays.Text) + 1
' ''        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
' ''            txtdays.Text = Val(txtdays.Text) + 0.5
' ''        End If
' ''        create_active()
' ''    End Sub
' ''    Private Sub txtdays_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdays.Leave
' ''        If txtdays.Text.Trim <> "" And txtdays.Text <> "0" Then
' ''            dtexp.Value = DateAdd(DateInterval.Day, CInt(txtdays.Text), dttoday.Value)
' ''        End If
' ''        create_active()
' ''    End Sub
' ''    Private Sub txtinterast_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtinterast.Leave
' ''        Mrateofinterast = Val(txtinterast.Text)
' ''    End Sub
' ''    Private Sub txtinterast_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtinterast.KeyPress
' ''        numonly(e)
' ''    End Sub

' ''    Private Sub txtmkt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmkt.GotFocus
' ''        SendKeys.Send("{HOME}+{END}")
' ''    End Sub
' ''    Private Sub txtmkt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmkt.KeyPress
' ''        numonly(e)
' ''        flgUnderline = True
' ''    End Sub
' ''    Private Sub txtmkt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmkt.Leave
' ''        If txtmkt.Text.Trim = "" Then
' ''            txtmkt.Text = 0
' ''        End If
' ''        If Val(txtmkt.Text) > 0 Then
' ''            txtmid = Math.Floor(Val(txtmkt.Text))
' ''            If Val(txtmid) < 100 Then
' ''                interval = 1
' ''            ElseIf Val(txtmid) > 100 And Val(txtmid) < 500 Then
' ''                interval = 5
' ''            ElseIf Val(txtmid) > 100 And Val(txtmid) < 1000 Then
' ''                interval = 10
' ''            ElseIf Val(txtmid) > 1000 And Val(txtmid) < 10000 Then
' ''                interval = 100
' ''            ElseIf Val(txtmid) > 10000 Then
' ''                interval = 500
' ''            End If
' ''            If flgUnderline = False Then Exit Sub
' ''            If grdtrad.Rows.Count > 0 Then
' ''                For i As Integer = 0 To grdtrad.Rows.Count - 2
' ''                    Dim grow As DataGridViewRow
' ''                    grow = grdtrad.Rows(i)
' ''                    grow.Cells("spval").Value = CDbl(txtmkt.Text)
' ''                    If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
' ''                        'If IsDBNull(grow.Cells(4).Value) Or grow.Cells(4).Value Is Nothing Or CStr(grow.Cells(4).Value) = "" Then
' ''                        grow.Cells("last").Value = CDbl(txtmkt.Text)
' ''                        'End If
' ''                    End If
' ''                    cal(7, grow, True)
' ''                Next
' ''                grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("spval").Value = CDbl(txtmkt.Text)
' ''                ' result()
' ''            End If
' ''        End If
' ''        Call cal_summary()
' ''        flgUnderline = False
' ''    End Sub

' ''    Private Sub cmdcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcheck.Click
' ''        If grdact.ColumnCount > 0 Then
' ''            If grdact.RowCount > 0 Then
' ''                If checkall = True Then
' ''                    checkall = False
' ''                    For Each col As DataGridViewColumn In grdact.Columns
' ''                        grdact.Rows(0).Cells(col.Index).Value = False
' ''                    Next
' ''                    cmdcheck.Text = "CheckAll"
' ''                Else
' ''                    checkall = True
' ''                    For Each col As DataGridViewColumn In grdact.Columns
' ''                        grdact.Rows(0).Cells(col.Index).Value = True
' ''                    Next
' ''                    cmdcheck.Text = "ClearAll"
' ''                End If

' ''            End If
' ''        End If

' ''    End Sub
' ''    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
' ''        export()
' ''    End Sub

' ''    Private Sub chkcheck_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcheck.CheckedChanged

' ''        If grdtrad.RowCount > 1 Then
' ''            If chkcheck.Checked = False Then
' ''                gcheck = False
' ''                For i As Integer = 0 To grdtrad.RowCount - 2
' ''                    grdtrad.Rows(i).Cells("Active").Value = False
' ''                Next
' ''                'For Each grow As DataGridViewRow In grdtrad.Rows
' ''                '    grow.Cells(0).Value = False
' ''                'Next
' ''            Else
' ''                gcheck = True
' ''                For i As Integer = 0 To grdtrad.RowCount - 2
' ''                    grdtrad.Rows(i).Cells("Active").Value = True
' ''                Next
' ''                'For Each grow As DataGridViewRow In grdtrad.Rows
' ''                '    grow.Cells(0).Value = True
' ''                'Next
' ''            End If


' ''        End If

' ''    End Sub
' ''    Private Sub cmdexp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexp.Click
' ''        If grdtrad.Rows.Count > 1 Then
' ''            cal_exp()
' ''        End If
' ''    End Sub
' ''    Private Sub cmdimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdimport.Click
' ''        Me.Cursor = Cursors.WaitCursor
' ''        import()
' ''        Me.Cursor = Cursors.Default
' ''    End Sub

' ''    Private Sub txtcvol_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcvol.GotFocus
' ''        SendKeys.Send("{HOME}+{END}")
' ''    End Sub

' ''    Private Sub txtcvol_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcvol.Leave
' ''        If txtcvol.Text.Trim = "" Then
' ''            txtcvol.Text = 0
' ''        End If
' ''        If Val(txtcvol.Text) > 0 Then
' ''            If grdtrad.Rows.Count > 0 Then
' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    If grow.Cells("CPF").Value = "C" Then
' ''                        If grow.Cells("lv").ReadOnly = False Then
' ''                            grow.Cells("lv").Value = Val(txtcvol.Text)
' ''                        End If
' ''                        Dim mt As Integer
' ''                        Dim mmt As Integer
' ''                        Dim futval As Double

' ''                        mt = (DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date))
' ''                        mmt = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date) - 1

' ''                        futval = Val(grow.Cells("spval").Value)
' ''                        CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, True, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
' ''                        cal_summary()
' ''                    End If
' ''                Next
' ''            End If
' ''        End If
' ''    End Sub
' ''    Private Sub txtcvol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcvol.KeyPress
' ''        numonly(e)
' ''    End Sub

' ''    Private Sub txtpvol_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpvol.GotFocus
' ''        SendKeys.Send("{HOME}+{END}")
' ''    End Sub
' ''    Private Sub txtpvol_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpvol.Leave
' ''        If txtpvol.Text.Trim = "" Then
' ''            txtpvol.Text = 0
' ''        End If
' ''        If Val(txtpvol.Text) > 0 Then
' ''            If grdtrad.Rows.Count > 0 Then
' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    If grow.Cells("CPF").Value = "P" Then
' ''                        If grow.Cells("lv").ReadOnly = False Then
' ''                            grow.Cells("lv").Value = Val(txtpvol.Text)
' ''                        End If
' ''                        Dim mt As Integer
' ''                        Dim mmt As Integer
' ''                        Dim futval As Double

' ''                        mt = (DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date))
' ''                        mmt = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date) - 1

' ''                        futval = Val(grow.Cells("spval").Value)
' ''                        CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, False, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
' ''                        cal_summary()
' ''                    End If
' ''                Next
' ''            End If
' ''        End If
' ''    End Sub
' ''    Private Sub txtpvol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpvol.KeyPress
' ''        numonly(e)
' ''    End Sub

' ''    Private Sub tbcon_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles tbcon.DrawItem
' ''        Dim g As Graphics = e.Graphics
' ''        Dim tp As TabPage = tbcon.TabPages(e.Index)
' ''        Dim br As System.Drawing.Brush
' ''        Dim sf As New StringFormat
' ''        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width + 2, e.Bounds.Height - 2)
' ''        sf.Alignment = StringAlignment.Near
' ''        Dim strTitle As String = tp.Text
' ''        If tbcon.SelectedIndex = e.Index Then
' ''            Dim f As Font = New Font(tbcon.Font.Name, tbcon.Font.Size, FontStyle.Regular, tbcon.Font.Unit)
' ''            br = New SolidBrush(Color.Black)
' ''            g.FillRectangle(br, e.Bounds)
' ''            '  g.FillRectangle(br, e.Bounds)
' ''            br = New SolidBrush(Color.White)
' ''            g.DrawString(strTitle, f, br, r, sf)


' ''        Else
' ''            Dim f As Font = New Font(tbcon.Font.Name, tbcon.Font.Size, FontStyle.Regular, tbcon.Font.Unit)
' ''            br = New SolidBrush(Color.WhiteSmoke)
' ''            g.FillRectangle(br, e.Bounds)
' ''            'g.FillRectangle(br, e.Bounds)
' ''            br = New SolidBrush(Color.Black)
' ''            g.DrawString(strTitle, f, br, r, sf)

' ''        End If
' ''        tp.Refresh()
' ''    End Sub
' ''    Private Sub txtunits_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtunits.TextChanged
' ''        If Val(txtunits.Text) > 0 Then
' ''            txtunits.BackColor = Color.MediumSeaGreen
' ''            txtunits.ForeColor = Color.White
' ''        ElseIf Val(txtunits.Text) < 0 Then
' ''            txtunits.BackColor = Color.DarkOrange
' ''            txtunits.ForeColor = Color.White
' ''        Else
' ''            txtunits.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtunits.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtdelval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdelval.TextChanged
' ''        If Val(txtdelval.Text) > 0 Then
' ''            txtdelval.BackColor = Color.MediumSeaGreen
' ''            txtdelval.ForeColor = Color.White
' ''        ElseIf Val(txtdelval.Text) < 0 Then
' ''            txtdelval.BackColor = Color.DarkOrange
' ''            txtdelval.ForeColor = Color.White
' ''        Else
' ''            txtdelval.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtdelval.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtgmval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtgmval.TextChanged
' ''        If Val(txtgmval.Text) > 0 Then
' ''            txtgmval.BackColor = Color.MediumSeaGreen
' ''            txtgmval.ForeColor = Color.White
' ''        ElseIf Val(txtgmval.Text) < 0 Then
' ''            txtgmval.BackColor = Color.DarkOrange
' ''            txtgmval.ForeColor = Color.White
' ''        Else
' ''            txtgmval.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtgmval.ForeColor = Color.Black

' ''        End If
' ''    End Sub
' ''    Private Sub txtvgval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvgval.TextChanged
' ''        If Val(txtvgval.Text) > 0 Then
' ''            txtvgval.BackColor = Color.MediumSeaGreen
' ''            txtvgval.ForeColor = Color.White
' ''        ElseIf Val(txtvgval.Text) < 0 Then
' ''            txtvgval.BackColor = Color.DarkOrange
' ''            txtvgval.ForeColor = Color.White
' ''        Else
' ''            txtvgval.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtvgval.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtthval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtthval.TextChanged
' ''        If Val(txtthval.Text) > 0 Then
' ''            txtthval.BackColor = Color.MediumSeaGreen
' ''            txtthval.ForeColor = Color.White
' ''        ElseIf Val(txtthval.Text) < 0 Then
' ''            txtthval.BackColor = Color.DarkOrange
' ''            txtthval.ForeColor = Color.White
' ''        Else
' ''            txtthval.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtthval.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtunits1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtunits1.TextChanged
' ''        If Val(txtunits1.Text) > 0 Then
' ''            txtunits1.BackColor = Color.MediumSeaGreen
' ''            txtunits1.ForeColor = Color.White
' ''        ElseIf Val(txtunits1.Text) < 0 Then
' ''            txtunits1.BackColor = Color.DarkOrange
' ''            txtunits1.ForeColor = Color.White
' ''        Else
' ''            txtunits1.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtunits1.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtdelval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdelval1.TextChanged
' ''        If Val(txtdelval1.Text) > 0 Then
' ''            txtdelval1.BackColor = Color.MediumSeaGreen
' ''            txtdelval1.ForeColor = Color.White
' ''        ElseIf Val(txtdelval1.Text) < 0 Then
' ''            txtdelval1.BackColor = Color.DarkOrange
' ''            txtdelval1.ForeColor = Color.White
' ''        Else
' ''            txtdelval1.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtdelval1.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtgmval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtgmval1.TextChanged
' ''        If Val(txtgmval1.Text) > 0 Then
' ''            txtgmval1.BackColor = Color.MediumSeaGreen
' ''            txtgmval1.ForeColor = Color.White
' ''        ElseIf Val(txtgmval1.Text) < 0 Then
' ''            txtgmval1.BackColor = Color.DarkOrange
' ''            txtgmval1.ForeColor = Color.White
' ''        Else
' ''            txtgmval1.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtgmval1.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtvgval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvgval1.TextChanged
' ''        If Val(txtvgval1.Text) > 0 Then
' ''            txtvgval1.BackColor = Color.MediumSeaGreen
' ''            txtvgval1.ForeColor = Color.White
' ''        ElseIf Val(txtvgval1.Text) < 0 Then
' ''            txtvgval1.BackColor = Color.DarkOrange
' ''            txtvgval1.ForeColor = Color.White
' ''        Else
' ''            txtvgval1.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtvgval1.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtthval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtthval1.TextChanged
' ''        If Val(txtthval1.Text) > 0 Then
' ''            txtthval1.BackColor = Color.MediumSeaGreen
' ''            txtthval1.ForeColor = Color.White
' ''        ElseIf Val(txtthval1.Text) < 0 Then
' ''            txtthval1.BackColor = Color.DarkOrange
' ''            txtthval1.ForeColor = Color.White
' ''        Else
' ''            txtthval1.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtthval1.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtinterval_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtinterval.Leave
' ''        If chkint.Checked = True Then
' ''            interval = Val(txtinterval.Text) * 100
' ''        Else
' ''            interval = Val(txtinterval.Text)
' ''        End If
' ''    End Sub

' ''#End Region
' ''#Region "Grid Event"
' ''    Private Sub cal(ByVal cno As Integer, ByVal currow As DataGridViewRow, Optional ByVal check As Boolean = False)
' ''        Dim iscall As Boolean = False
' ''        Dim zero As Double = 0
' ''        Dim one As Double = 1
' ''        Dim count As Integer
' ''        count = 0
' ''        Dim futval As Double
' ''        Dim brate As Double = 0
' ''        Dim srate As Double = 0
' ''        futval = 0
' ''        Dim sumunit As Double = 0
' ''        Dim mt As Double = 0
' ''        Dim mmt As Double = 0
' ''        Dim grow As DataGridViewRow
' ''        If check = True Then
' ''            grow = currow
' ''        Else
' ''            grow = grdtrad.CurrentRow
' ''        End If
' ''        If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
' ''            grow.Cells("CPF").Value = CStr("C")
' ''        Else
' ''            grow.Cells("CPF").Value = UCase(grow.Cells("CPF").Value)
' ''            If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
' ''                grow.Cells("delta").Value = one
' ''            Else
' ''                grow.Cells("delta").Value = zero
' ''            End If
' ''        End If

' ''        grdtrad.EndEdit()
' ''        'If IsDBNull(grow.Cells(2).Value) Or Not IsDate(grow.Cells(2).Value) Then
' ''        '    MsgBox("Enter Maturity date")
' ''        '    Exit Sub
' ''        'End If
' ''        If Not IsDBNull(grow.Cells("CPF").Value) And Not grow.Cells("CPF").Value Is Nothing Then
' ''            If UCase(grow.Cells("CPF").Value) = "F" Or UCase(grow.Cells("CPF").Value) = "E" Then
' ''                grow.Cells("deltaval").Value = grow.Cells("units").Value
' ''                grow.Cells("delta").Value = Math.Round(one, 2)
' ''                grow.Cells("deltaval").Value = Math.Round(Val(grow.Cells("deltaval").Value), 2)
' ''                grow.Cells("Strike").Value = Math.Round(zero, 2)
' ''                If Val(grow.Cells("last").Value) = 0 Then
' ''                    If Val(grow.Cells("spval").Value) <> 0 Then
' ''                        grow.Cells("last").Value = Math.Round(CDbl(grow.Cells("spval").Value), 2)
' ''                    Else
' ''                        grow.Cells("last").Value = Math.Round(zero, 2)
' ''                    End If
' ''                End If
' ''                grow.Cells("lv").Value = Math.Round(zero, 2)
' ''                grow.Cells("gamma").Value = Math.Round(zero, 2)
' ''                grow.Cells("gmval").Value = Math.Round(zero, 2)
' ''                grow.Cells("vega").Value = Math.Round(zero, 2)
' ''                grow.Cells("vgval").Value = Math.Round(zero, 2)
' ''                grow.Cells("theta").Value = Math.Round(zero, 2)
' ''                grow.Cells("thetaval").Value = Math.Round(zero, 2)


' ''                grow.Cells("gamma1").Value = Math.Round(zero, 2)
' ''                grow.Cells("gmval1").Value = Math.Round(zero, 2)
' ''                grow.Cells("vega1").Value = Math.Round(zero, 2)
' ''                grow.Cells("vgval1").Value = Math.Round(zero, 2)
' ''                grow.Cells("theta").Value = Math.Round(zero, 2)
' ''                grow.Cells("thetaval1").Value = Math.Round(zero, 2)

' ''                grdtrad.EndEdit()
' ''            Else
' ''                futval = Val(grow.Cells("spval").Value)
' ''                If IsDBNull(grow.Cells("TimeI").Value) Then
' ''                    grow.Cells("TimeI").Value = dttoday.Value.Date
' ''                    'ElseIf grow.Cells("TimeI").Value.ToString.Trim = "" Then
' ''                    '    grow.Cells("TimeI").Value = dttoday.Value.Date
' ''                ElseIf grow.Cells("TimeI").Value = Nothing Then
' ''                    grow.Cells("TimeI").Value = dttoday.Value.Date
' ''                End If
' ''                If IsDBNull(grow.Cells("TimeII").Value) Then
' ''                    grow.Cells("TimeII").Value = dtexp.Value.Date
' ''                    ' ElseIf grow.Cells("TimeII").Value.ToString.Trim = "" Then
' ''                ElseIf grow.Cells("TimeII").Value = Nothing Then
' ''                    grow.Cells("TimeII").Value = dtexp.Value.Date
' ''                End If
' ''                If CDate(grow.Cells("TimeI").Value) < CDate(grow.Cells("TimeII").Value) Then
' ''                    mt = (DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date))
' ''                    'mmt = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, CInt(txtdays.Text), Now.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                    'Else
' ''                    '    mt = DateDiff(DateInterval.Day, CDate(grow.Cells(1).Value), CDate(grow.Cells(2).Value))
' ''                End If
' ''                If CDate(grow.Cells("TimeI").Value).Date = CDate(grow.Cells("TimeII").Value).Date Then
' ''                    mt = 0.5
' ''                End If

' ''                'mt = DateDiff(DateInterval.Day, CDate(drow("mdate")), Now())
' ''                If grow.Cells("CPF").Value = "C" Then
' ''                    iscall = True
' ''                Else
' ''                    iscall = False
' ''                End If
' ''                If futval <> 0 Then
' ''                    If cno = 9 Then
' ''                        CalDatastkprice(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, iscall, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
' ''                        '' CalData(futval, Val(grow.Cells(5).Value), Val(grow.Cells(7).Value), mt, iscall, True, grow, Val(grow.Cells(6).Value), (Val(grow.Cells(8).Value) / 100))
' ''                    Else
' ''                        CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, iscall, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
' ''                        isVolCal = True
' ''                    End If
' ''                End If
' ''            End If
' ''        End If
' ''        txtunits.Text = 0
' ''        txtdelval.Text = 0
' ''        txtthval.Text = 0
' ''        txtvgval.Text = 0
' ''        txtgmval.Text = 0

' ''        ''divyesh
' ''        txtunits1.Text = 0
' ''        txtdelval1.Text = 0
' ''        txtthval1.Text = 0
' ''        txtvgval1.Text = 0
' ''        txtgmval1.Text = 0

' ''        If grdtrad.Rows.Count > 1 Then
' ''            For Each row As DataGridViewRow In grdtrad.Rows
' ''                txtdelval.Text = Format(Val(txtdelval.Text) + Val(row.Cells("deltaval").Value), Deltastr_Val)
' ''                txtthval.Text = Format(Val(txtthval.Text) + Val(row.Cells("thetaval").Value), Thetastr_Val)
' ''                txtvgval.Text = Format(Val(txtvgval.Text) + Val(row.Cells("vgval").Value), Vegastr_Val)
' ''                txtgmval.Text = Format(Val(txtgmval.Text) + Val(row.Cells("gmval").Value), Gammastr_Val)

' ''                ''divyesh
' ''                txtdelval1.Text = Format(Val(txtdelval1.Text) + Val(row.Cells("deltaval1").Value), Deltastr_Val)
' ''                txtthval1.Text = Format(Val(txtthval1.Text) + Val(row.Cells("thetaval1").Value), Thetastr_Val)
' ''                txtvgval1.Text = Format(Val(txtvgval1.Text) + Val(row.Cells("vgval1").Value), Vegastr_Val)
' ''                txtgmval1.Text = Format(Val(txtgmval1.Text) + Val(row.Cells("gmval1").Value), Gammastr_Val)
' ''            Next

' ''            txtunits.Text = Math.Round(cal_profitLoss, RoundGrossMTM)
' ''            ''divyesh
' ''            txtunits1.Text = Math.Round(cal_FutprofitLoss, RoundGrossMTM)
' ''        End If
' ''    End Sub
' ''    Private Sub grdtrad_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtrad.CellEndEdit
' ''        'If (e.ColumnIndex = 5 Or e.ColumnIndex = 6 Or e.ColumnIndex =9) Then
' ''        '    cal()
' ''        'Else
' ''        If VarIsFirstLoad = True Then
' ''            VarIsFirstLoad = False
' ''            Exit Sub
' ''        End If
' ''        Dim zero As Double = 0
' ''        Dim one As Double = 1
' ''        If grdtrad.Columns(e.ColumnIndex).Name = "TimeI" Then
' ''            'ElseIf grdtrad.Columns(e.ColumnIndex).Name = "TimeII" Then
' ''            '    grdtrad.Rows(e.RowIndex).Cells("TimeI").Value = dttoday.Value
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "CPF" Then
' ''            Dim grow As DataGridViewRow
' ''            grow = grdtrad.CurrentRow
' ''            If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
' ''                grow.Cells("CPF").Value = "C"
' ''            Else
' ''                Dim arr As New ArrayList
' ''                arr.Add("C")
' ''                arr.Add("F")
' ''                arr.Add("P")
' ''                arr.Add("E")
' ''                If Not arr.Contains(UCase(grow.Cells("CPF").Value)) Then
' ''                    MsgBox("Enter 'C','P','F' or 'E'")
' ''                    grdtrad.Rows(e.RowIndex).Cells("CPF").Selected = True
' ''                    Exit Sub
' ''                End If
' ''                grow.Cells("CPF").Value = UCase(grow.Cells("CPF").Value)
' ''                If Not IsDBNull(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
' ''                    If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "C" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "P" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
' ''                        If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
' ''                            grdtrad.Rows(e.RowIndex).Cells("delta").Value = one
' ''                            grdtrad.Rows(e.RowIndex).Cells("lv").Value = zero
' ''                            grdtrad.Rows(e.RowIndex).Cells("last").Value = CDbl(txtmkt.Text)
' ''                        Else
' ''                            grdtrad.Rows(e.RowIndex).Cells("delta").Value = zero
' ''                            If grow.Cells("lv").Value <> "0" Then
' ''                                If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "C" Then
' ''                                    If txtcvol.Text.Trim <> "" Then
' ''                                        grow.Cells("lv").Value = Val(txtcvol.Text)
' ''                                    Else
' ''                                        grow.Cells("lv").Value = zero
' ''                                    End If
' ''                                    ' grdtrad.Rows(e.RowIndex).Cells(8).Value = val(txtcvol.Text)
' ''                                Else
' ''                                    If txtpvol.Text.Trim <> "" Then
' ''                                        grow.Cells("lv").Value = Val(txtpvol.Text)
' ''                                    Else
' ''                                        grow.Cells("lv").Value = zero
' ''                                    End If
' ''                                    'grdtrad.Rows(e.RowIndex).Cells(8).Value = val(txtpvol.Text)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Else
' ''                        'MsgBox("Enter 'C','P' or 'F'")
' ''                        grdtrad.Rows(e.RowIndex).Cells("CPF").Selected = True
' ''                        Exit Sub
' ''                    End If
' ''                End If
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "spval" Then
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("spval").Value) Or grdtrad.Rows(e.RowIndex).Cells("spval").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("spval").Value) = "" Then
' ''                If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
' ''                    grdtrad.Rows(e.RowIndex).Cells("spval").Value = CDbl(txtmkt.Text)
' ''                Else
' ''                    grdtrad.Rows(e.RowIndex).Cells("spval").Value = zero
' ''                End If
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "Strike" Then
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("Strike").Value) Or grdtrad.Rows(e.RowIndex).Cells("Strike").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("Strike").Value) = "" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("Strike").Value = zero
' ''            Else
' ''                grdtrad.Rows(e.RowIndex).Cells("Strike").Value = Math.Abs(CDbl(grdtrad.Rows(e.RowIndex).Cells("Strike").Value))
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "units" Then
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("units").Value) Or grdtrad.Rows(e.RowIndex).Cells("units").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("units").Value) = "" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("units").Value = zero
' ''            ElseIf UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("deltaval").Value = grdtrad.Rows(e.RowIndex).Cells("units").Value
' ''                'to calculate deltaval
' ''                cal_summary()
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "ltp" Then
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("ltp").Value) Or grdtrad.Rows(e.RowIndex).Cells("ltp").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("ltp").Value) = "" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("ltp").Value = zero
' ''            End If
' ''            ''divyesh
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("ltp1").Value) Or grdtrad.Rows(e.RowIndex).Cells("ltp").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("ltp1").Value) = "" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("ltp1").Value = zero
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "last" Then
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("last").Value) Or grdtrad.Rows(e.RowIndex).Cells("last").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("last").Value) = "" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("last").Value = zero
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "lv" Then
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("lv").Value) Or grdtrad.Rows(e.RowIndex).Cells("lv").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("lv").Value) = "" Then
' ''                zero = 0.01
' ''                grdtrad.Rows(e.RowIndex).Cells("lv").Value = zero
' ''                zero = 0
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "Active" Then

' ''            Call cal_summary()

' ''            grdtrad.EndEdit()

' ''            Dim grow As DataGridViewRow
' ''            grow = grdtrad.CurrentRow

' ''            If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
' ''                grow.Cells("CPF").Value = CStr("C")
' ''            End If
' ''            If IsDBNull(grow.Cells("spval").Value) Or grow.Cells("spval").Value Is Nothing Then
' ''                grdtrad.Rows(e.RowIndex).Cells("spval").Value = CDbl(txtmkt.Text)
' ''            Else
' ''                grow.Cells("spval").Value = CDbl(grow.Cells("spval").Value)
' ''            End If
' ''            If IsDBNull(grow.Cells("Strike").Value) Or grow.Cells("Strike").Value Is Nothing Then
' ''                grow.Cells("Strike").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("units").Value) Or grow.Cells("units").Value Is Nothing Then
' ''                grow.Cells("units").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("last").Value) Or grow.Cells("last").Value Is Nothing Then
' ''                grow.Cells("last").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("lv").Value) Or grow.Cells("lv").Value Is Nothing Then
' ''                If txtcvol.Text.Trim <> "" Then
' ''                    grow.Cells("lv").Value = CDbl(txtcvol.Text)
' ''                Else
' ''                    grow.Cells("lv").Value = zero
' ''                End If
' ''            End If
' ''            If IsDBNull(grow.Cells("delta").Value) Or grow.Cells("delta").Value Is Nothing Then
' ''                grow.Cells("delta").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("deltaval").Value) Or grow.Cells("deltaval").Value Is Nothing Then
' ''                grow.Cells("deltaval").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("gamma").Value) Or grow.Cells("gamma").Value Is Nothing Then
' ''                grow.Cells("gamma").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("gmval").Value) Or grow.Cells("gmval").Value Is Nothing Then
' ''                grow.Cells("gmval").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("vega").Value) Or grow.Cells("vega").Value Is Nothing Then
' ''                grow.Cells("vega").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("vgval").Value) Or grow.Cells("vgval").Value Is Nothing Then
' ''                grow.Cells("vgval").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("theta").Value) Or grow.Cells("theta").Value Is Nothing Then
' ''                grow.Cells("theta").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("thetaval").Value) Or grow.Cells("thetaval").Value Is Nothing Then
' ''                grow.Cells("thetaval").Value = zero
' ''            End If

' ''            ''divyesh
' ''            If IsDBNull(grow.Cells("delta1").Value) Or grow.Cells("delta1").Value Is Nothing Then
' ''                grow.Cells("delta1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("gamma1").Value) Or grow.Cells("gamma1").Value Is Nothing Then
' ''                grow.Cells("gamma1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("vega1").Value) Or grow.Cells("vega1").Value Is Nothing Then
' ''                grow.Cells("vega1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("theta1").Value) Or grow.Cells("theta1").Value Is Nothing Then
' ''                grow.Cells("theta1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("vgval1").Value) Or grow.Cells("vgval").Value Is Nothing Then
' ''                grow.Cells("vgval1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("gmval1").Value) Or grow.Cells("gmval").Value Is Nothing Then
' ''                grow.Cells("gmval1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("thetaval1").Value) Or grow.Cells("thetaval").Value Is Nothing Then
' ''                grow.Cells("thetaval1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("deltaval1").Value) Or grow.Cells("deltaval").Value Is Nothing Then
' ''                grow.Cells("deltaval1").Value = zero
' ''            End If

' ''            grow.Cells("uid").Value = grdtrad.Rows.Count - 1
' ''            'grdtrad.Refresh()
' ''            'to attch context menu to new row
' ''            'If (grdtrad.Rows(e.RowIndex).Cells("ltp").Value = 0) Then

' ''            'REM 1 create contextmenustrip from vol column cells
' ''            ReDim contMenu(grdtrad.Rows.Count - 1)
' ''            contMenu(grdtrad.Rows.Count - 1) = New ContextMenuStrip
' ''            'contMenu(grdtrad.Rows.Count - 1).ShowImageMargin = False
' ''            Dim item As New ToolStripMenuItem("Unfreeze")
' ''            ' item.CheckOnClick = True
' ''            item.Tag = grdtrad.Rows.Count - 1
' ''            AddHandler item.Click, AddressOf freezVol
' ''            AddHandler contMenu(grdtrad.Rows.Count - 1).Opening, AddressOf contMenuOpen
' ''            contMenu(grdtrad.Rows.Count - 1).Items.Add(item)
' ''            grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("lv").ContextMenuStrip = contMenu(grdtrad.Rows.Count - 1)
' ''            REM 1: end

' ''            'REM 1 create contextmenustrip from Expiry column cells
' ''            ReDim contMenuExpiry(grdtrad.Rows.Count - 1)
' ''            contMenuExpiry(grdtrad.Rows.Count - 1) = New ContextMenuStrip
' ''            'contMenuExpiry(grdtrad.Rows.Count - 1).ShowImageMargin = False
' ''            item = New ToolStripMenuItem("Unfreeze")
' ''            ' item.CheckOnClick = True
' ''            item.Tag = grdtrad.Rows.Count - 1
' ''            AddHandler item.Click, AddressOf freezExpiry
' ''            AddHandler contMenu(grdtrad.Rows.Count - 1).Opening, AddressOf contMenuOpenExpiry
' ''            contMenuExpiry(grdtrad.Rows.Count - 1).Items.Add(item)
' ''            grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("TimeII").ContextMenuStrip = contMenuExpiry(grdtrad.Rows.Count - 1)
' ''            REM 1: end
' ''        End If
' ''        grdtrad.EndEdit()
' ''        If grdtrad.Columns(e.ColumnIndex).Name = "lv" Then
' ''            If UCase(grdtrad.CurrentRow.Cells("CPF").Value) = "F" Or UCase(grdtrad.CurrentRow.Cells("CPF").Value) = "E" Then
' ''                If Val(grdtrad.CurrentRow.Cells("last").Value) <> 0 Then
' ''                    cal(8, grdtrad.CurrentRow)
' ''                End If
' ''            Else
' ''                If Val(grdtrad.CurrentRow.Cells("Strike").Value) > 0 Then
' ''                    cal(8, grdtrad.CurrentRow)
' ''                End If
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "last" Then
' ''            cal(9, grdtrad.CurrentRow)
' ''        Else
' ''            If Val(grdtrad.CurrentRow.Cells("Strike").Value) > 0 And Val(grdtrad.CurrentRow.Cells("lv").Value) <> 0 Then
' ''                cal(0, grdtrad.CurrentRow)
' ''            End If

' ''        End If
' ''    End Sub
' ''    Private Sub grdtrad_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdtrad.EditingControlShowing
' ''        AddHandler e.Control.KeyPress, AddressOf CheckCell

' ''        'AddHandler e.Control.KeyDown, AddressOf cellkeydown
' ''    End Sub
' ''    Private Sub grdtrad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdtrad.KeyDown
' ''        If e.KeyCode = Keys.F2 Then
' ''            If (grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "TimeI" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "TimeII" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "CPF" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "spval" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "Strike" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "units" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "ltp") Then
' ''                grdtrad.CurrentCell.KeyEntersEditMode(e)
' ''            End If
' ''        ElseIf e.KeyCode = Keys.Escape Then
' ''            If grdtrad.CurrentRow.IsNewRow Then
' ''                grdtrad.CancelEdit()
' ''            Else
' ''                grdtrad.Rows.RemoveAt(grdtrad.CurrentRow.Index)
' ''            End If
' ''            'grdtrad.Rows.RemoveAt(grdtrad.CurrentRow.Index)
' ''            grdtrad.CancelEdit()
' ''            'grdtrad.BindingContext.Item(grdtrad.DataSource).CancelCurrentEdit()

' ''        ElseIf e.KeyCode = Keys.Tab Then
' ''            If grdtrad.CurrentCell.OwningColumn.Index = 4 Then
' ''                If grdtrad.CurrentRow.Cells("CPF").Value = "F" Or grdtrad.CurrentRow.Cells("CPF").Value = "E" Then
' ''                    Me.grdtrad.CurrentCell = _
' ''                                             Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index).Cells("units")

' ''                    e.SuppressKeyPress = True
' ''                End If
' ''                'ElseIf grdtrad.CurrentCell.OwningColumn.Index = 9 Then
' ''                '    If grdtrad.CurrentRow.Cells(3).Value = "F" Then
' ''                '        Me.grdtrad.CurrentCell = _
' ''                '                                  Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells(0)

' ''                '        e.SuppressKeyPress = True
' ''                '    End If
' ''            ElseIf grdtrad.CurrentCell.OwningColumn.Index = 10 Then
' ''                If (Me.grdtrad.CurrentRow.Index + 1 <= Me.grdtrad.Rows.Count - 1) Then
' ''                    Me.grdtrad.CurrentCell = Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells("Active")
' ''                    e.SuppressKeyPress = True
' ''                Else
' ''                    Me.grdtrad.CurrentCell = Me.grdtrad.Rows(0).Cells("Active")
' ''                End If
' ''            End If
' ''        End If
' ''    End Sub
' ''    Private Sub grdtrad_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdtrad.DataError

' ''    End Sub
' ''    Private Sub grdtrad_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtrad.CellValueChanged
' ''        If VarIsFrmLoad = False Then Exit Sub
' ''        If (grdtrad.Columns(e.ColumnIndex).Name = "delta" Or grdtrad.Columns(e.ColumnIndex).Name = "deltaval" Or grdtrad.Columns(e.ColumnIndex).Name = "gamma" Or grdtrad.Columns(e.ColumnIndex).Name = "gmval" Or grdtrad.Columns(e.ColumnIndex).Name = "vega" Or grdtrad.Columns(e.ColumnIndex).Name = "vgval" Or grdtrad.Columns(e.ColumnIndex).Name = "theta" Or grdtrad.Columns(e.ColumnIndex).Name = "thetaval") And e.RowIndex > -1 Then
' ''            'grdtrad.Columns(e.ColumnIndex).Name = "TimeII" Or
' ''            If Val(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.DarkBlue
' ''            Else
' ''                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''        End If
' ''        If (grdtrad.Columns(e.ColumnIndex).Name = "TimeII") Then
' ''            If (IsDate(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False) Then
' ''                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dtexp.Value
' ''                'grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
' ''                ' MsgBox("Please Enter Proper Date...")
' ''                ' grdtrad.Rows(e.RowIndex).Cells("TimeI").Value = dttoday.Value
' ''            End If
' ''        End If
' ''        If grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "c" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Yellow
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Yellow

' ''        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "p" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LimeGreen
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LimeGreen

' ''        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "f" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Orange
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.Orange
' ''        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "e" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LightPink
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LightPink
' ''        End If
' ''        If Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) < 0 Then
' ''            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.Red
' ''        ElseIf Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) > 0 Then
' ''            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.SkyBlue
' ''        Else
' ''            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.White
' ''        End If

' ''        grdtrad.Rows(e.RowIndex).Cells("ltp").Style.Format = "##0.00"
' ''        grdtrad.Rows(e.RowIndex).Cells("last").Style.Format = "##0.00"

' ''        grdtrad.Rows(e.RowIndex).Cells("delta").Style.Format = Deltastr
' ''        grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.Format = Deltastr_Val

' ''        grdtrad.Rows(e.RowIndex).Cells("gamma").Style.Format = Gammastr
' ''        grdtrad.Rows(e.RowIndex).Cells("gmval").Style.Format = Gammastr_Val

' ''        grdtrad.Rows(e.RowIndex).Cells("vega").Style.Format = Vegastr
' ''        grdtrad.Rows(e.RowIndex).Cells("vgval").Style.Format = Vegastr_Val

' ''        grdtrad.Rows(e.RowIndex).Cells("theta").Style.Format = Thetastr
' ''        grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.Format = Thetastr_Val


' ''    End Sub
' ''    Private Sub grdtrad_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdtrad.RowsAdded
' ''        If VarIsFrmLoad = False Then Exit Sub
' ''        Dim zero As Double = 0

' ''        grdtrad.Rows(e.RowIndex).Cells("TimeI").Value = dttoday.Value
' ''        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dtexp.Value

' ''        If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("spval").Value) Or grdtrad.Rows(e.RowIndex).Cells("spval").Value Is Nothing Then
' ''            If txtmkt.Text.Trim <> "" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("spval").Value = CDbl(txtmkt.Text)
' ''            Else
' ''                grdtrad.Rows(e.RowIndex).Cells("spval").Value = zero
' ''            End If
' ''        End If
' ''        grdtrad.EndEdit()
' ''        Dim grow As DataGridViewRow
' ''        grow = grdtrad.Rows(e.RowIndex)

' ''        If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
' ''            grow.Cells("CPF").Value = "C"
' ''        End If
' ''        If IsDBNull(grow.Cells("spval").Value) Or grow.Cells("spval").Value Is Nothing Then
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Value = zero
' ''        Else
' ''            grow.Cells("spval").Value = CDbl(grow.Cells("spval").Value)
' ''        End If
' ''        If IsDBNull(grow.Cells("Strike").Value) Or grow.Cells("Strike").Value Is Nothing Then
' ''            grow.Cells("Strike").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("units").Value) Or grow.Cells("units").Value Is Nothing Then
' ''            grow.Cells("units").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("last").Value) Or grow.Cells("last").Value Is Nothing Then
' ''            grow.Cells("last").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("lv").Value) Or grow.Cells("lv").Value Is Nothing Then
' ''            If txtcvol.Text.Trim <> "" Then
' ''                grow.Cells("lv").Value = Val(txtcvol.Text)
' ''            Else
' ''                grow.Cells("lv").Value = zero
' ''            End If
' ''        End If
' ''        If IsDBNull(grow.Cells("delta").Value) Or grow.Cells("delta").Value Is Nothing Then
' ''            grow.Cells("delta").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("deltaval").Value) Or grow.Cells("deltaval").Value Is Nothing Then
' ''            grow.Cells("deltaval").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("gamma").Value) Or grow.Cells("gamma").Value Is Nothing Then
' ''            grow.Cells("gamma").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("gmval").Value) Or grow.Cells("gmval").Value Is Nothing Then
' ''            grow.Cells("gmval").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("vega").Value) Or grow.Cells("vega").Value Is Nothing Then
' ''            grow.Cells("vega").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("vgval").Value) Or grow.Cells("vgval").Value Is Nothing Then
' ''            grow.Cells("vgval").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("theta").Value) Or grow.Cells("theta").Value Is Nothing Then
' ''            grow.Cells("theta").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("thetaval").Value) Or grow.Cells("thetaval").Value Is Nothing Then
' ''            grow.Cells("thetaval").Value = zero
' ''        End If
' ''        grow.Cells("uid").Value = grdtrad.Rows.Count - 1
' ''    End Sub
' ''    Private Sub grdtrad_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdtrad.CellFormatting
' ''        If grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "c" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Yellow
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Yellow

' ''        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "p" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LimeGreen
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LimeGreen

' ''        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "f" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Orange
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.Orange
' ''        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "e" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LightPink
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LightPink
' ''        End If
' ''        If Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) < 0 Then
' ''            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.Red
' ''        ElseIf Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) > 0 Then
' ''            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.SkyBlue
' ''        Else
' ''            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.White
' ''        End If

' ''        grdtrad.Rows(e.RowIndex).Cells("ltp").Style.Format = "##0.00"
' ''        grdtrad.Rows(e.RowIndex).Cells("last").Style.Format = "##0.00"

' ''        grdtrad.Rows(e.RowIndex).Cells("delta").Style.Format = Deltastr
' ''        grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.Format = Deltastr_Val

' ''        grdtrad.Rows(e.RowIndex).Cells("gamma").Style.Format = Gammastr
' ''        grdtrad.Rows(e.RowIndex).Cells("gmval").Style.Format = Gammastr_Val

' ''        grdtrad.Rows(e.RowIndex).Cells("vega").Style.Format = Vegastr
' ''        grdtrad.Rows(e.RowIndex).Cells("vgval").Style.Format = Vegastr_Val

' ''        grdtrad.Rows(e.RowIndex).Cells("theta").Style.Format = Thetastr
' ''        grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.Format = Thetastr_Val

' ''    End Sub

' ''    Private Sub grdprofit_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdprofit.CellDoubleClick
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            fill_result(0, e.RowIndex, e.ColumnIndex)
' ''            Dim res As New resultfrm
' ''            res.temptable = rtable
' ''            res.ShowDialog()
' ''        End If
' ''    End Sub
' ''    Private Sub grdprofit_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdprofit.CellFormatting
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            If Val(grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
' ''            Else
' ''                grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''            'grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N2"
' ''            Dim d As Double = Double.Parse(e.Value.ToString)
' ''            Dim str As String = "N" & RoundGrossMTM
' ''            e.Value = d.ToString(str)
' ''            ' grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N2"
' ''        End If

' ''    End Sub
' ''    Private Sub grddelta_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grddelta.CellFormatting
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            If Val(grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
' ''            Else
' ''                grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''            '  grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = Deltastr_Val
' ''            Dim d As Double = Double.Parse(e.Value.ToString)
' ''            Dim str As String = "N" & roundDelta_Val
' ''            e.Value = d.ToString(str)
' ''        End If
' ''    End Sub
' ''    Private Sub grdgamma_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdgamma.CellFormatting
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            If Val(grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
' ''            Else
' ''                grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''            'grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = Gammastr_Val
' ''            Dim d As Double = Double.Parse(e.Value.ToString)
' ''            Dim str As String = "N" & roundGamma_Val
' ''            e.Value = d.ToString(str)

' ''        End If
' ''    End Sub
' ''    Private Sub grdvega_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdvega.CellFormatting
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            If Val(grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
' ''            Else
' ''                grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''            'grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = Vegastr_Val
' ''            Dim d As Double = Double.Parse(e.Value.ToString)
' ''            Dim str As String = "N" & roundVega_Val
' ''            e.Value = d.ToString(str)
' ''        End If
' ''    End Sub
' ''    Private Sub grdtheta_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdtheta.CellFormatting
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            If Val(grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
' ''            Else
' ''                grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''            ' grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = Thetastr_Val
' ''            Dim d As Double = Double.Parse(e.Value.ToString)
' ''            Dim str As String = "N" & roundTheta_Val
' ''            e.Value = d.ToString(str)
' ''        End If
' ''    End Sub

' ''    Private Sub cellkeydown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
' ''        If e.KeyCode = Keys.Tab Then
' ''            If grdtrad.CurrentCell.OwningColumn.Index = 4 Then
' ''                If grdtrad.CurrentRow.Cells("CPF").Value = "F" Then
' ''                    Me.grdtrad.CurrentCell = _
' ''                                              Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index).Cells("units")

' ''                    e.SuppressKeyPress = True
' ''                End If
' ''            ElseIf grdtrad.CurrentCell.OwningColumn.Index = 6 Then
' ''                If grdtrad.CurrentRow.Cells("CPF").Value = "F" Then
' ''                    Me.grdtrad.CurrentCell = _
' ''                                              Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells("Active")
' ''                    e.SuppressKeyPress = True
' ''                End If
' ''            End If

' ''        End If
' ''    End Sub
' ''    Private Sub CheckCell(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

' ''        'Dim KeyAscii As Short = Asc(e.KeyChar)
' ''        Dim col As Integer
' ''        col = grdtrad.CurrentCell.ColumnIndex
' ''        Select Case col
' ''            Case 2
' ''                dateonly(e)
' ''            Case 4
' ''                numonly(e)
' ''            Case 5
' ''                numonly(e)
' ''            Case 6
' ''                numonly(e)

' ''            Case 7
' ''                numonly(e)
' ''            Case 8
' ''                numonly(e)
' ''            Case 10
' ''                numonly(e)
' ''            Case 3
' ''                Dim arr As New ArrayList
' ''                arr.Add(67)
' ''                arr.Add(70)
' ''                arr.Add(80)
' ''                arr.Add(69)
' ''                arr.Add(8)

' ''                If Not arr.Contains(Asc(UCase(e.KeyChar))) Then
' ''                    e.Handled = True
' ''                End If


' ''        End Select
' ''        '... 

' ''        '... code to check the input

' ''        '... 

' ''        'If KeyAscii = 0 Then

' ''        '    e.Handled = True

' ''        'End If

' ''    End Sub

' ''#End Region
' ''#Region "Method"
' ''    Dim profitarr As New ArrayList
' ''    Dim thetaarr As New ArrayList
' ''    Dim deltaarr As New ArrayList
' ''    Dim gammaarr As New ArrayList
' ''    Dim vegaarr As New ArrayList
' ''    Private Sub CalDatastkprice(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal mT As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal grow As DataGridViewRow, ByVal qty As Double, ByVal lv As Double)
' ''        Try
' ''            Dim mDelta As Double
' ''            Dim mGama As Double
' ''            Dim mVega As Double
' ''            Dim mThita As Double
' ''            Dim mRah As Double
' ''            Dim mVolatility As Double
' ''            Dim tmpcpprice As Double = 0
' ''            tmpcpprice = cpprice
' ''            'Dim mIsCal As Boolean
' ''            'Dim mIsPut As Boolean
' ''            'Dim mIsFut As Boolean

' ''            mDelta = 0
' ''            mGama = 0
' ''            mVega = 0
' ''            mThita = 0
' ''            mRah = 0
' ''            Dim _mt As Double
' ''            'IF MATURITY IS 0 THEN _MT = 0.00001 
' ''            If mT = 0 Then
' ''                _mt = 0.0001
' ''            Else
' ''                _mt = (mT) / 365

' ''            End If
' ''            mVolatility = mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
' ''            'mVolatility = lv
' ''            Try

' ''                'If Not mIsFut Then
' ''                mDelta = mDelta + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))

' ''                mGama = mGama + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))

' ''                mVega = mVega + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))

' ''                mThita = mThita + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))

' ''                mRah = mRah + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))

' ''                'Else
' ''                ''mDelta = mDelta + (1 * drow("Qty"))
' ''                'End If



' ''                grow.Cells("lv").Value = Math.Round(mVolatility * 100, 2)
' ''                grow.Cells("delta").Value = Math.Round(mDelta, roundDelta)
' ''                grow.Cells("deltaval").Value = Math.Round(mDelta * qty, roundDelta_Val)
' ''                grow.Cells("gamma").Value = Math.Round(mGama, roundGamma)
' ''                grow.Cells("gmval").Value = Math.Round(mGama * qty, roundGamma_Val)
' ''                grow.Cells("vega").Value = Math.Round(mVega, roundVega)
' ''                grow.Cells("vgval").Value = Math.Round(mVega * qty, roundVega_Val)
' ''                grow.Cells("theta").Value = Math.Round(mThita, roundTheta)
' ''                grow.Cells("thetaval").Value = Math.Round(mThita * qty, roundTheta_Val)
' ''                'grow.Cells(8).Value = Math.Round(mVolatility * 100, 2)
' ''                'grow.cells(10).value = Format(mDelta, Deltastr)
' ''                'grow.cells(11).value = Format(mDelta * qty, Deltastr_Val)
' ''                'grow.cells(12).value = Format(mGama, Gammastr)
' ''                'grow.cells(13).value = Format(mGama * qty, Gammastr_Val)
' ''                'grow.cells(14).value = Format(mVega, Vegastr)
' ''                'grow.cells(15).value = Format(mVega * qty, Vegastr_Val)
' ''                'grow.cells(16).value = Format(mThita, Thetastr)
' ''                'grow.cells(17).value = Format(mThita * qty, Thetastr_Val)
' ''                grdtrad.EndEdit()
' ''            Catch ex As Exception

' ''            End Try
' ''        Catch ex As Exception
' ''            ' MsgBox(ex.ToString)
' ''        End Try


' ''        ''MsgBox(mDelta)

' ''    End Sub
' ''    Private Sub CalData(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal mT As Double, ByVal mmT As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal grow As DataGridViewRow, ByVal qty As Double, ByVal lv As Double)
' ''        Dim mDelta As Double
' ''        Dim mGama As Double
' ''        Dim mVega As Double
' ''        Dim mThita As Double
' ''        Dim mRah As Double

' ''        ''divyesh
' ''        Dim mDelta1 As Double
' ''        Dim mGama1 As Double
' ''        Dim mVega1 As Double
' ''        Dim mThita1 As Double
' ''        Dim mRah1 As Double

' ''        Dim mVolatility As Double
' ''        Dim tmpcpprice As Double = 0
' ''        Dim tmpcpprice1 As Double = 0

' ''        Dim cpprice1 As Double = 0

' ''        tmpcpprice = cpprice

' ''        tmpcpprice1 = cpprice1

' ''        'Dim mIsCal As Boolean
' ''        'Dim mIsPut As Boolean
' ''        'Dim mIsFut As Boolean

' ''        mDelta = 0
' ''        mGama = 0
' ''        mVega = 0
' ''        mThita = 0
' ''        mRah = 0

' ''        mDelta1 = 0
' ''        mGama1 = 0
' ''        mVega1 = 0
' ''        mThita1 = 0
' ''        mRah1 = 0

' ''        Dim _mt, _mt1 As Double
' ''        'IF MATURITY IS 0 THEN _MT = 0.00001 
' ''        If mT = 0 Then
' ''            _mt = 0.0001
' ''            _mt1 = 0.00001
' ''        Else
' ''            _mt = (mT) / 365
' ''            _mt1 = (mmT) / 365
' ''        End If
' ''        'mVolatility = mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
' ''        mVolatility = lv
' ''        'mVolatility1 = lv
' ''        Try
' ''            'If Not mIsFut Then
' ''            tmpcpprice = (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 0))
' ''            mDelta = mDelta + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))
' ''            mGama = mGama + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))
' ''            mVega = mVega + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))
' ''            mThita = mThita + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))
' ''            mRah = mRah + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))

' ''            tmpcpprice1 = (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 0))
' ''            mDelta1 = mDelta1 + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 1))
' ''            mGama1 = mGama1 + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 2))
' ''            mVega1 = mVega1 + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 3))
' ''            mThita1 = mThita1 + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 4))
' ''            mRah1 = mRah1 + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 5))

' ''            'Else
' ''            ''mDelta = mDelta + (1 * drow("Qty"))
' ''            'End If

' ''            'grow.Cells(9).Value = Math.Round(tmpcpprice, round)
' ''            'grow.cells(10).value = Math.Round(mDelta, round)
' ''            'grow.cells(11).value = Math.Round(mDelta * qty, round)
' ''            'grow.cells(12).value = Math.Round(mThita, round)
' ''            'grow.cells(13).value = Math.Round(mThita * qty, round)
' ''            'grow.cells(14).value = Math.Round(mVega, round)
' ''            'grow.cells(15).value = Math.Round(mVega * qty, round)
' ''            'grow.cells(16).value = Math.Round(mGama, round)
' ''            'grow.cells(17).value = Math.Round(mGama * qty, round)

' ''            grow.Cells("last").Value = Math.Round(tmpcpprice, 2)
' ''            grow.Cells("delta").Value = Math.Round(mDelta, roundDelta)
' ''            grow.Cells("deltaval").Value = Math.Round(mDelta * qty, roundDelta_Val)
' ''            grow.Cells("gamma").Value = Math.Round(mGama, roundGamma)
' ''            grow.Cells("gmval").Value = Math.Round(mGama * qty, roundGamma_Val)
' ''            grow.Cells("vega").Value = Math.Round(mVega, roundVega)
' ''            grow.Cells("vgval").Value = Math.Round(mVega * qty, roundVega_Val)
' ''            grow.Cells("theta").Value = Math.Round(mThita, roundTheta)
' ''            grow.Cells("thetaval").Value = Math.Round(mThita * qty, roundTheta_Val)

' ''            grow.Cells("ltp1").Value = Math.Round(tmpcpprice1, 2)
' ''            grow.Cells("delta1").Value = Math.Round(mDelta1, roundDelta)
' ''            grow.Cells("deltaval1").Value = Math.Round(mDelta1 * qty, roundDelta_Val)
' ''            grow.Cells("gamma1").Value = Math.Round(mGama1, roundDelta)
' ''            grow.Cells("gmval1").Value = Math.Round(mGama1 * qty, roundGamma_Val)
' ''            grow.Cells("vega1").Value = Math.Round(mVega1, roundDelta)
' ''            grow.Cells("vgval1").Value = Math.Round(mVega1 * qty, roundVega_Val)
' ''            grow.Cells("theta1").Value = Math.Round(mThita1, roundDelta)
' ''            grow.Cells("thetaval1").Value = Math.Round(mThita1 * qty, roundTheta_Val)

' ''            'grow.Cells(9).Value = Math.Round(tmpcpprice, 2)
' ''            'grow.cells(10).value = Format(mDelta, Deltastr)
' ''            'grow.cells(11).value = Format(mDelta * qty, Deltastr_Val)
' ''            'grow.cells(12).value = Format(mGama, Gammastr)
' ''            'grow.cells(13).value = Format(mGama * qty, Gammastr_Val)
' ''            'grow.cells(14).value = Format(mVega, Vegastr)
' ''            'grow.cells(15).value = Format(mVega * qty, Vegastr_Val)
' ''            'grow.cells(16).value = Format(mThita, Thetastr)
' ''            'grow.cells(17).value = Format(mThita * qty, Thetastr_Val)
' ''            grdtrad.EndEdit()
' ''        Catch ex As Exception

' ''        End Try

' ''        ''MsgBox(mDelta)


' ''    End Sub

' ''    'Private Function cal_profit() As Boolean
' ''    '    grdprofit.DataSource = Nothing
' ''    '    'grdprofit.Refresh()
' ''    '    Dim totfpr As Double
' ''    '    Dim totcpr As Double
' ''    '    profitarr = New ArrayList
' ''    '    Dim bval As Double
' ''    '    Dim bvol As Double

' ''    '    Dim i As Integer
' ''    '    i = 0
' ''    '    Dim dcount As Integer
' ''    '    dcount = 0
' ''    '    Dim iscall As Boolean
' ''    '    'ReDim profitarr(((profit.Rows.Count) * (profit.Columns.Count - 1)))
' ''    '    For Each dr As DataRow In profit.Rows
' ''    '        totfpr = 0
' ''    '        dcount = 0
' ''    '        For Each col As DataColumn In profit.Columns
' ''    '            If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''    '                For Each grow As DataGridViewRow In grdtrad.Rows
' ''    '                    If CBool(grow.Cells(0).Value) = True Then

' ''    '                        If grow.Cells(3).Value = "F" Then
' ''    '                            If Val(grow.Cells(4).Value) = 0 Then
' ''    '                                MsgBox("Enter Spot Value for Selected Future")
' ''    '                                grdtrad.Focus()
' ''    '                                Return False
' ''    '                            End If
' ''    '                            If Val(grow.Cells(6).Value) = 0 Then
' ''    '                                MsgBox("Enter Quantity Value for Selected Future")
' ''    '                                grdtrad.Focus()
' ''    '                                Return False

' ''    '                            End If
' ''    '                            dr(col.ColumnName) = (Val(dr(col.ColumnName)) + ((Val(dr("SpotValue")) - Val(grow.Cells(4).Value)) * Val(grow.Cells(6).Value))) + grossmtm
' ''    '                            'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + ((val(dr("SpotValue")) - val(grow.Cells(4).Value)) * val(grow.Cells(6).Value)), RoundGrossMTM)
' ''    '                            'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + ((val(dr("SpotValue")) - val(grow.Cells(4).Value)) * val(grow.Cells(6).Value)), GrossMTMstr)

' ''    '                        Else
' ''    '                            If grow.Cells(3).Value = "C" Then
' ''    '                                iscall = True
' ''    '                            Else
' ''    '                                iscall = False
' ''    '                            End If
' ''    '                            If Val(grow.Cells(9).Value) = 0 Then
' ''    '                                MsgBox("Enter Rate Value for Selected Call or Put")
' ''    '                                grdtrad.Focus()

' ''    '                                Return False

' ''    '                            End If
' ''    '                            If Val(grow.Cells(6).Value) = 0 Then
' ''    '                                MsgBox("Enter Quantity Value for Selected Call or Put")
' ''    '                                grdtrad.Focus()
' ''    '                                Return False

' ''    '                            End If
' ''    '                            If Val(grow.Cells(4).Value) = 0 Then
' ''    '                                MsgBox("Enter Spot Value for Selected Call or Put")
' ''    '                                grdtrad.Focus()
' ''    '                                Exit Function
' ''    '                            End If

' ''    '                            If Val(grow.Cells(8).Value) = 0 Then
' ''    '                                MsgBox("Enter Volatality Value for Selected Call or Put")
' ''    '                                grdtrad.Focus()
' ''    '                                Return False

' ''    '                            End If
' ''    '                            totcpr = 0
' ''    '                            Dim cd As Date
' ''    '                            If (IsDate(col.ColumnName)) Then
' ''    '                                cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''    '                            Else
' ''    '                                cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
' ''    '                            End If

' ''    '                            'dcount = DatePart(DateInterval.Day, cd)
' ''    '                            'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(dtexp.Value.Date))
' ''    '                            Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells(2).Value).Date)
' ''    '                            Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells(2).Value).Date)
' ''    '                            If CDate(dttoday.Value.Date) = CDate(grow.Cells(2).Value).Date Then
' ''    '                                _mT1 = 0.5
' ''    '                            End If
' ''    '                            If CDate(cd.Date) = CDate(grow.Cells(2).Value).Date Then
' ''    '                                If (IsDate(col.ColumnName)) Then
' ''    '                                    _mT = 0.5
' ''    '                                Else
' ''    '                                    _mT = 0.5
' ''    '                                End If

' ''    '                            End If
' ''    '                            If _mT = 0 Then
' ''    '                                _mT = 0.00001
' ''    '                                _mT1 = 0.00001
' ''    '                            Else
' ''    '                                _mT = (_mT) / 365
' ''    '                                _mT1 = (_mT1) / 365
' ''    '                            End If
' ''    '                            If (IsDate(col.ColumnName)) Then
' ''    '                                bval = 0
' ''    '                                bvol = 0
' ''    '                                bvol = mObjData.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells(5).Value), Mrateofinterast, 0, (Val(grow.Cells(9).Value)), _mT1, iscall, True, 0, 6)
' ''    '                                bval = mObjData.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells(5).Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 0)
' ''    '                                totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
' ''    '                                dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) + grossmtm
' ''    '                                'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + totcpr, RoundGrossMTM)
' ''    '                                ' dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr,GrossMTMstr)
' ''    '                            Else
' ''    '                                If (iscall = True) Then
' ''    '                                    If ((Val(dr("SpotValue")) - Val(grow.Cells(5).Value)) > 0) Then
' ''    '                                        bval = (Val(dr("SpotValue")) - Val(grow.Cells(5).Value))
' ''    '                                        totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
' ''    '                                        dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) + grossmtm
' ''    '                                    Else
' ''    '                                        bval = 0 ' val(dr(col.ColumnName)) + ((0 - val(grow.Cells(9).Value)) * val(grow.Cells(6).Value))
' ''    '                                        totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
' ''    '                                        dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) + grossmtm
' ''    '                                    End If
' ''    '                                Else
' ''    '                                    If ((Val(grow.Cells(5).Value) - Val(dr("SpotValue"))) > 0) Then
' ''    '                                        bval = (Val(grow.Cells(5).Value) - Val(dr("SpotValue")))
' ''    '                                        totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
' ''    '                                        dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) ' + grossmtm
' ''    '                                    Else
' ''    '                                        bval = 0 'val(dr(col.ColumnName)) + ((0 - val(grow.Cells(9).Value)) * val(grow.Cells(6).Value))
' ''    '                                        totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
' ''    '                                        dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) ' + grossmtm
' ''    '                                    End If
' ''    '                                End If
' ''    '                            End If

' ''    '                        End If

' ''    '                    End If
' ''    '                Next
' ''    '                dcount = dcount + 1
' ''    '            End If
' ''    '            i = i + 1
' ''    '            profitarr.Add(dr(col.ColumnName))

' ''    '        Next
' ''    '    Next
' ''    '    grdprofit.DataSource = profit
' ''    '    Return True

' ''    '    'For Each col As DataColumn In profit.Columns
' ''    '    '    If UCase(col.ColumnName) <> UCase("spotvalue") Then
' ''    '    '        For Each drow As DataRow In profit.Rows

' ''    '    '        Next
' ''    '    '    End If
' ''    '    'Next

' ''    '    'grdprofit.Refresh()
' ''    'End Function
' ''    Private Function cal_profit() As Boolean
' ''        grdprofit.DataSource = Nothing
' ''        'grdprofit.Refresh()
' ''        Dim totfpr As Double
' ''        Dim totcpr As Double
' ''        profitarr = New ArrayList
' ''        Dim bval As Double
' ''        Dim bvol As Double

' ''        Dim i As Integer
' ''        i = 0
' ''        Dim dcount As Integer
' ''        dcount = 0
' ''        Dim iscall As Boolean
' ''        'ReDim profitarr(((profit.Rows.Count) * (profit.Columns.Count - 1)))
' ''        For Each dr As DataRow In profit.Rows
' ''            totfpr = 0
' ''            dcount = 0
' ''            For Each col As DataColumn In profit.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
' ''                    For Each grow As DataGridViewRow In grdtrad.Rows
' ''                        If grow.Cells("Active").Value = False Then Continue For
' ''                        If CBool(grow.Cells("Active").Value) = True Then

' ''                            If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
' ''                                If CDbl(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Future")
' ''                                    grdtrad.Focus()
' ''                                    Return False
' ''                                End If
' ''                                If CDbl(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Future")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + ((CDbl(dr("SpotValue")) - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value)))
' ''                                'dr(col.ColumnName) = Math.Round(CDbl(dr(col.ColumnName)) + ((CDbl(dr("SpotValue")) - CDbl(grow.Cells(4).Value)) * CDbl(grow.Cells(6).Value)), RoundGrossMTM)
' ''                                'dr(col.ColumnName) = Format(CDbl(dr(col.ColumnName)) + ((CDbl(dr("SpotValue")) - CDbl(grow.Cells(4).Value)) * CDbl(grow.Cells(6).Value)), GrossMTMstr)

' ''                            Else
' ''                                If grow.Cells("CPF").Value = "C" Then
' ''                                    iscall = True
' ''                                Else
' ''                                    iscall = False
' ''                                End If
' ''                                If CDbl(grow.Cells("ltp").Value) = 0 Then
' ''                                    MsgBox("Enter Ltp Value for Selected Call or Put")
' ''                                    grdtrad.Focus()

' ''                                    Return False

' ''                                End If
' ''                                If CDbl(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If CDbl(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Exit Function
' ''                                End If

' ''                                If CDbl(grow.Cells("lv").Value) = 0 Then
' ''                                    MsgBox("Enter Volatality Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                totcpr = 0
' ''                                Dim cd As Date
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                Else
' ''                                    cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
' ''                                End If

' ''                                'dcount = DatePart(DateInterval.Day, cd)
' ''                                'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(dtexp.Value.Date))
' ''                                Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                    _mT1 = 0.5
' ''                                End If
' ''                                If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                    If (IsDate(col.ColumnName)) Then
' ''                                        _mT = 0.5
' ''                                    Else
' ''                                        _mT = 0.5
' ''                                    End If

' ''                                End If
' ''                                If _mT = 0 Then
' ''                                    _mT = 0.00001
' ''                                    _mT1 = 0.00001
' ''                                Else
' ''                                    _mT = (_mT) / 365
' ''                                    _mT1 = (_mT1) / 365
' ''                                End If
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    bval = 0
' ''                                    bvol = 0
' ''                                    'bvol = mObjData.Black_Scholes(CDbl(txtmkt.Text), CDbl(grow.Cells(5).Value), Mrateofinterast, 0, (CDbl(grow.Cells(7).Value)), _mT1, iscall, True, 0, 6)
' ''                                    bval = mObjData.Black_Scholes(CDbl(dr("SpotValue")), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value)) / 100, _mT, iscall, True, 0, 0)
' ''                                    totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
' ''                                    dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
' ''                                    'dr(col.ColumnName) = Math.Round(CDbl(dr(col.ColumnName)) + totcpr, RoundGrossMTM)
' ''                                    ' dr(col.ColumnName) = Format(CDbl(dr(col.ColumnName)) + totcpr,GrossMTMstr)
' ''                                Else
' ''                                    If (iscall = True) Then
' ''                                        If ((CDbl(dr("SpotValue")) - CDbl(grow.Cells("Strike").Value)) > 0) Then
' ''                                            bval = (CDbl(dr("SpotValue")) - CDbl(grow.Cells("Strike").Value))
' ''                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
' ''                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
' ''                                        Else
' ''                                            bval = 0 ' CDbl(dr(col.ColumnName)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
' ''                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
' ''                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
' ''                                        End If
' ''                                    Else
' ''                                        If ((CDbl(grow.Cells("Strike").Value) - CDbl(dr("SpotValue"))) > 0) Then
' ''                                            bval = (CDbl(grow.Cells("Strike").Value) - CDbl(dr("SpotValue")))
' ''                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
' ''                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
' ''                                        Else
' ''                                            bval = 0 'CDbl(dr(col.ColumnName)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
' ''                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
' ''                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
' ''                                        End If
' ''                                    End If
' ''                                End If

' ''                            End If

' ''                        End If


' ''                    Next
' ''                    dr(col.ColumnName) += grossmtm
' ''                    dcount = dcount + 1
' ''                End If
' ''                i = i + 1


' ''                profitarr.Add(dr(col.ColumnName))

' ''            Next
' ''        Next
' ''        grdprofit.DataSource = profit
' ''        Return True

' ''        'For Each col As DataColumn In profit.Columns
' ''        '    If UCase(col.ColumnName) <> UCase("spotvalue") Then
' ''        '        For Each drow As DataRow In profit.Rows

' ''        '        Next
' ''        '    End If
' ''        'Next

' ''        'grdprofit.Refresh()
' ''    End Function

' ''    Private Function cal_delta() As Boolean


' ''        grddelta.DataSource = Nothing
' ''        'grddelta.Refresh()
' ''        Dim totfpr As Double
' ''        Dim totcpr As Double
' ''        Dim bval As Double
' ''        Dim bvol As Double


' ''        Dim i As Integer
' ''        i = 0
' ''        Dim dcount As Integer
' ''        dcount = 0
' ''        Dim iscall As Boolean

' ''        For Each dr As DataRow In deltatable.Rows
' ''            totfpr = 0
' ''            dcount = 0
' ''            For Each col As DataColumn In deltatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then


' ''                    For Each grow As DataGridViewRow In grdtrad.Rows
' ''                        If grow.Cells("Active").Value = False Then Continue For
' ''                        If CBool(grow.Cells("Active").Value) = True Then

' ''                            If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
' ''                                If Val(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Future")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Future")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + Val(grow.Cells("deltaval").Value), roundDelta_Val)
' ''                                ' dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + val(grow.cells(11).value), Deltastr_Val)

' ''                            Else
' ''                                If grow.Cells("CPF").Value = "C" Then
' ''                                    iscall = True
' ''                                Else
' ''                                    iscall = False
' ''                                End If
' ''                                If Val(grow.Cells("last").Value) = 0 Then
' ''                                    MsgBox("Enter Rate Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If

' ''                                If Val(grow.Cells("lv").Value) = 0 Then
' ''                                    MsgBox("Enter Volatality Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If

' ''                                totcpr = 0
' ''                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
' ''                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT1 = 0.5
' ''                                    End If
' ''                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT = 0.5
' ''                                    End If
' ''                                    If _mT = 0 Then
' ''                                        _mT = 0.00001
' ''                                        _mT1 = 0.00001
' ''                                    Else
' ''                                        _mT = (_mT) / 365
' ''                                        _mT1 = (_mT1) / 365
' ''                                    End If
' ''                                    bval = 0
' ''                                    bvol = 0
' ''                                    bvol = mObjData.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
' ''                                    bval = mObjData.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 1)
' ''                                    totcpr = (bval * Val(grow.Cells("units").Value))
' ''                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, roundDelta_Val)
' ''                                    ' dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, Deltastr_Val)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                    dcount = dcount + 1
' ''                End If
' ''                i = i + 1
' ''                deltaarr.Add(dr(col.ColumnName))
' ''            Next
' ''        Next

' ''        grddelta.DataSource = deltatable
' ''        Return True

' ''        'grddelta.Refresh()
' ''    End Function
' ''    Private Function cal_gamma() As Boolean
' ''        grdgamma.DataSource = Nothing
' ''        'grdgamma.Refresh()
' ''        Dim totfpr As Double
' ''        Dim totcpr As Double

' ''        Dim bval As Double
' ''        Dim bvol As Double

' ''        Dim i As Integer
' ''        i = 0
' ''        Dim dcount As Integer
' ''        dcount = 0
' ''        Dim iscall As Boolean

' ''        For Each dr As DataRow In gammatable.Rows
' ''            totfpr = 0
' ''            dcount = 0
' ''            For Each col As DataColumn In gammatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
' ''                    For Each grow As DataGridViewRow In grdtrad.Rows
' ''                        If grow.Cells("Active").Value = False Then Continue For
' ''                        If CBool(grow.Cells("Active").Value) = True Then
' ''                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
' ''                                '    'If val(grow.Cells(2).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Spot Value for Selected Futre")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'If val(grow.Cells(5).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Units Value for Selected Futre")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

' ''                                'Else
' ''                                If grow.Cells("CPF").Value = "C" Then
' ''                                    iscall = True
' ''                                Else
' ''                                    iscall = False
' ''                                End If
' ''                                If Val(grow.Cells("last").Value) = 0 Then
' ''                                    MsgBox("Enter Rate Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False


' ''                                End If

' ''                                If Val(grow.Cells("lv").Value) = 0 Then
' ''                                    MsgBox("Enter Volatality Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                totcpr = 0
' ''                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
' ''                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT1 = 0.5
' ''                                    End If
' ''                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT = 0.5
' ''                                    End If
' ''                                    If _mT = 0 Then
' ''                                        _mT = 0.00001
' ''                                        _mT1 = 0.00001
' ''                                    Else
' ''                                        _mT = (_mT) / 365
' ''                                        _mT1 = (_mT1) / 365
' ''                                    End If
' ''                                    bval = 0
' ''                                    bvol = 0
' ''                                    bvol = mObjData.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
' ''                                    bval = mObjData.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 2)
' ''                                    totcpr = (bval * Val(grow.Cells("units").Value))
' ''                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, roundGamma_Val)
' ''                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, Gammastr_Val)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                    dcount = dcount + 1
' ''                End If
' ''                i = i + 1
' ''                gammaarr.Add(dr(col.ColumnName))
' ''            Next
' ''        Next

' ''        grdgamma.DataSource = gammatable
' ''        Return True

' ''        'grdgamma.Refresh()
' ''    End Function
' ''    Private Function cal_vega() As Boolean

' ''        grdvega.DataSource = Nothing
' ''        'grdvega.Refresh()
' ''        Dim totfpr As Double
' ''        Dim totcpr As Double
' ''        Dim bval As Double
' ''        Dim bvol As Double


' ''        Dim i As Integer
' ''        i = 0
' ''        Dim dcount As Integer
' ''        dcount = 0
' ''        Dim iscall As Boolean

' ''        For Each dr As DataRow In vegatable.Rows
' ''            totfpr = 0
' ''            dcount = 0
' ''            For Each col As DataColumn In vegatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then


' ''                    For Each grow As DataGridViewRow In grdtrad.Rows
' ''                        If grow.Cells("Active").Value = False Then Continue For
' ''                        If CBool(grow.Cells("Active").Value) = True Then

' ''                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
' ''                                '    'If val(grow.Cells(2).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Spot Value for Selected Futre")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'If val(grow.Cells(5).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Units Value for Selected Futre")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

' ''                                'Else
' ''                                If grow.Cells("CPF").Value = "C" Then
' ''                                    iscall = True
' ''                                Else
' ''                                    iscall = False
' ''                                End If
' ''                                If Val(grow.Cells("last").Value) = 0 Then
' ''                                    MsgBox("Enter Rate Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If

' ''                                If Val(grow.Cells("lv").Value) = 0 Then
' ''                                    MsgBox("Enter Volatality Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                totcpr = 0
' ''                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
' ''                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT1 = 0.5
' ''                                    End If
' ''                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT = 0.5
' ''                                    End If
' ''                                    If _mT = 0 Then
' ''                                        _mT = 0.00001
' ''                                        _mT1 = 0.00001
' ''                                    Else
' ''                                        _mT = (_mT) / 365
' ''                                        _mT1 = (_mT1) / 365
' ''                                    End If
' ''                                    bval = 0
' ''                                    bvol = 0
' ''                                    bvol = mObjData.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
' ''                                    bval = mObjData.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 3)
' ''                                    totcpr = (bval * Val(grow.Cells("units").Value))
' ''                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, roundVega_Val)
' ''                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, Vegastr_Val)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                    dcount = dcount + 1
' ''                End If
' ''                i = i + 1
' ''                vegaarr.Add(dr(col.ColumnName))
' ''            Next
' ''        Next

' ''        grdvega.DataSource = vegatable
' ''        Return True

' ''        'grdvega.Refresh()

' ''    End Function
' ''    Private Function cal_theta() As Boolean
' ''        grdtheta.DataSource = Nothing
' ''        'grdtheta.Refresh()
' ''        Dim totfpr As Double
' ''        Dim totcpr As Double

' ''        thetaarr = New ArrayList
' ''        Dim i As Integer
' ''        i = 0
' ''        Dim dcount As Integer
' ''        dcount = 0
' ''        Dim iscall As Boolean
' ''        Dim bval As Double
' ''        Dim bvol As Double

' ''        For Each dr As DataRow In thetatable.Rows
' ''            totfpr = 0
' ''            dcount = 0
' ''            For Each col As DataColumn In thetatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    For Each grow As DataGridViewRow In grdtrad.Rows
' ''                        If grow.Cells("Active").Value = False Then Continue For
' ''                        If CBool(grow.Cells("Active").Value) = True Then

' ''                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
' ''                                '    'If val(grow.Cells(2).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Spot Value for Selected Future")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'If val(grow.Cells(5).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Units Value for Selected Futre")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

' ''                                'Else
' ''                                If grow.Cells("CPF").Value = "C" Then
' ''                                    iscall = True
' ''                                Else
' ''                                    iscall = False
' ''                                End If
' ''                                If Val(grow.Cells("last").Value) = 0 Then
' ''                                    MsgBox("Enter Rate Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If

' ''                                If Val(grow.Cells("lv").Value) = 0 Then
' ''                                    MsgBox("Enter Volatality Value for Selected Call or Put")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                totcpr = 0
' ''                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
' ''                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT1 = 0.5
' ''                                    End If
' ''                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT = 0.5
' ''                                    End If
' ''                                    If _mT = 0 Then
' ''                                        _mT = 0.00001
' ''                                        _mT1 = 0.00001
' ''                                    Else
' ''                                        _mT = (_mT) / 365
' ''                                        _mT1 = (_mT1) / 365
' ''                                    End If
' ''                                    bval = 0
' ''                                    bvol = 0
' ''                                    bvol = mObjData.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
' ''                                    bval = mObjData.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 4)
' ''                                    totcpr = (bval * Val(grow.Cells("units").Value))
' ''                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, roundTheta_Val)
' ''                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, Thetastr_Val)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                    dcount = dcount + 1
' ''                End If
' ''                i = i + 1
' ''                thetaarr.Add(dr(col.ColumnName))
' ''            Next
' ''        Next

' ''        grdtheta.DataSource = thetatable
' ''        Return True

' ''        'grdtheta.Refresh()

' ''    End Function


' ''    Private Sub create_chart_profit()
' ''        chtprofit.ColumnCount = 0
' ''        chtprofit.ColumnLabelCount = 0
' ''        chtprofit.ColumnLabelIndex = 0
' ''        chtprofit.AutoIncrement = False
' ''        chtprofit.RowCount = 0
' ''        chtprofit.RowLabelCount = 0
' ''        chtprofit.RowLabelIndex = 0
' ''        chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone


' ''        If profit.Columns.Count > 1 Then
' ''            If profit.Columns.Count < 3 Then
' ''                Exit Sub
' ''            End If
' ''            chtprofit.ColumnCount = profit.Columns.Count - 3
' ''            chtprofit.ColumnLabelCount = 1
' ''            chtprofit.ColumnLabelIndex = 1
' ''            chtprofit.AutoIncrement = False
' ''            chtprofit.RowCount = profit.Rows.Count - 1
' ''            chtprofit.RowLabelCount = 1
' ''            chtprofit.RowLabelIndex = 1
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(profitarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(profitarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            'With chtprofit
' ''            '    Dim div As Double
' ''            '    div = (((Math.Abs(maxval) - Math.Abs(minval))) * 40) / 100
' ''            '    chtprofit.chartType = VtChChartType.VtChChartType2dLine
' ''            '    YAxis = .Plot.Axis(VtChAxisId.VtChAxisIdY, 1)
' ''            '    'Set Axis Scale Min and Max values      
' ''            '    YAxis.ValueScale.Auto = False
' ''            '    YAxis.CategoryScale.Auto = False
' ''            '    YAxis.ValueScale.Minimum = minval
' ''            '    YAxis.ValueScale.Maximum = maxval
' ''            '    YAxis.ValueScale.MajorDivision = Format(((Math.Abs(maxval) - Math.Abs(minval)) \ div), "##0")
' ''            '    YAxis.ValueScale.MinorDivision = 2
' ''            'End With
' ''            'YAxis = Nothing


' ''            chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(profit.Rows.Count - 1, profit.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In profit.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
' ''                    r = 0
' ''                    For Each drow As DataRow In profit.Rows
' ''                        cha(r, c) = Val(drow(col.ColumnName)) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            If div > 1 Then
' ''                lblprofit.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lblprofit.Text = "-"
' ''            End If
' ''            chtprofit.ChartData = cha

' ''            chtprofit.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0





' ''            For Each col As DataColumn In profit.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
' ''                    chtprofit.Column = c
' ''                    chtprofit.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")

' ''                    i += 1
' ''                    c += 1
' ''                End If
' ''                'If c >= grdprofit.Columns.Count Then
' ''                '    Exit For
' ''                'End If
' ''            Next
' ''            r = 1

' ''            For Each drow As DataRow In profit.Rows
' ''                chtprofit.Row = r
' ''                chtprofit.RowLabel = drow("spotvalue")

' ''                r += 1
' ''            Next
' ''            'MsgBox(chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).AxisGrid.MajorPen.)
' ''            'chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 0).AxisGrid.MajorPen.Style = VtPenStyle.VtPenStyleDotted
' ''            'chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).AxisGrid.MinorPen.Style = VtPenStyle.VtPenStyleDitted
' ''            '  chtprofit.Plot.Axis(0).Labels(0).Format
' ''            'MsgBox(chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).Labels(0))

' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_profit_Full()
' ''        objProfitLossChart.chtprofit.ColumnCount = 0
' ''        objProfitLossChart.chtprofit.ColumnLabelCount = 0
' ''        objProfitLossChart.chtprofit.ColumnLabelIndex = 0
' ''        objProfitLossChart.chtprofit.AutoIncrement = False
' ''        objProfitLossChart.chtprofit.RowCount = 0
' ''        objProfitLossChart.chtprofit.RowLabelCount = 0
' ''        objProfitLossChart.chtprofit.RowLabelIndex = 0
' ''        objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone


' ''        If profit.Columns.Count > 2 Then
' ''            objProfitLossChart.chtprofit.ColumnCount = profit.Columns.Count - 3
' ''            objProfitLossChart.chtprofit.ColumnLabelCount = 1
' ''            objProfitLossChart.chtprofit.ColumnLabelIndex = 1
' ''            objProfitLossChart.chtprofit.AutoIncrement = False
' ''            objProfitLossChart.chtprofit.RowCount = profit.Rows.Count - 1
' ''            objProfitLossChart.chtprofit.RowLabelCount = 1
' ''            objProfitLossChart.chtprofit.RowLabelIndex = 1
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(profitarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(profitarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            'With objProfitLossChart.chtprofit
' ''            '    Dim div As Double
' ''            '    div = (((Math.Abs(maxval) - Math.Abs(minval))) * 40) / 100
' ''            '    objProfitLossChart.chtprofit.chartType = VtChChartType.VtChChartType2dLine
' ''            '    YAxis = .Plot.Axis(VtChAxisId.VtChAxisIdY, 1)
' ''            '    'Set Axis Scale Min and Max values      
' ''            '    YAxis.ValueScale.Auto = False
' ''            '    YAxis.CategoryScale.Auto = False
' ''            '    YAxis.ValueScale.Minimum = minval
' ''            '    YAxis.ValueScale.Maximum = maxval
' ''            '    YAxis.ValueScale.MajorDivision = Format(((Math.Abs(maxval) - Math.Abs(minval)) \ div), "##0")
' ''            '    YAxis.ValueScale.MinorDivision = 2
' ''            'End With
' ''            'YAxis = Nothing


' ''            objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(profit.Rows.Count - 1, profit.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In profit.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
' ''                    r = 0
' ''                    For Each drow As DataRow In profit.Rows
' ''                        cha(r, c) = Val(drow(col.ColumnName)) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            If div > 1 Then
' ''                lblprofit.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lblprofit.Text = "-"
' ''            End If
' ''            objProfitLossChart.chtprofit.ChartData = cha

' ''            objProfitLossChart.chtprofit.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0





' ''            For Each col As DataColumn In profit.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
' ''                    objProfitLossChart.chtprofit.Column = c
' ''                    objProfitLossChart.chtprofit.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")

' ''                    i += 1
' ''                    c += 1
' ''                End If
' ''                'If c >= grdprofit.Columns.Count Then
' ''                '    Exit For
' ''                'End If
' ''            Next
' ''            r = 1

' ''            For Each drow As DataRow In profit.Rows
' ''                objProfitLossChart.chtprofit.Row = r
' ''                objProfitLossChart.chtprofit.RowLabel = drow("spotvalue")

' ''                r += 1
' ''            Next
' ''            'MsgBox(objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).AxisGrid.MajorPen.)
' ''            'objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 0).AxisGrid.MajorPen.Style = VtPenStyle.VtPenStyleDotted
' ''            'objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).AxisGrid.MinorPen.Style = VtPenStyle.VtPenStyleDitted
' ''            '  objProfitLossChart.chtprofit.Plot.Axis(0).Labels(0).Format
' ''            'MsgBox(objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).Labels(0))

' ''        End If

' ''    End Sub
' ''    Private Sub create_chart_delta()
' ''        chtdelta.ColumnCount = 0
' ''        chtdelta.ColumnLabelCount = 0
' ''        chtdelta.ColumnLabelIndex = 0
' ''        chtdelta.AutoIncrement = False
' ''        chtdelta.RowCount = deltatable.Rows.Count - 1
' ''        chtdelta.RowLabelCount = 0
' ''        chtdelta.RowLabelIndex = 0
' ''        chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If deltatable.Columns.Count > 2 Then
' ''            chtdelta.ColumnCount = deltatable.Columns.Count - 3
' ''            chtdelta.ColumnLabelCount = 1
' ''            chtdelta.ColumnLabelIndex = 1
' ''            chtdelta.AutoIncrement = False
' ''            chtdelta.RowCount = deltatable.Rows.Count - 1
' ''            chtdelta.RowLabelCount = 1
' ''            chtdelta.RowLabelIndex = 1

' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(deltaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(deltaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(deltatable.Rows.Count - 1, deltatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In deltatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In deltatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            chtdelta.ChartData = cha
' ''            If div > 1 Then
' ''                lbldelta.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lbldelta.Text = "-"
' ''            End If
' ''            chtdelta.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0

' ''            For Each col As DataColumn In deltatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    chtdelta.Column = c
' ''                    chtdelta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1
' ''                    c += 1
' ''                    'If c >= grddelta.Columns.Count Then
' ''                    '    Exit For
' ''                    'End If
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In deltatable.Rows
' ''                chtdelta.Row = r
' ''                chtdelta.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_delta_Full()
' ''        objProfitLossChart.chtdelta.ColumnCount = 0
' ''        objProfitLossChart.chtdelta.ColumnLabelCount = 0
' ''        objProfitLossChart.chtdelta.ColumnLabelIndex = 0
' ''        objProfitLossChart.chtdelta.AutoIncrement = False
' ''        objProfitLossChart.chtdelta.RowCount = deltatable.Rows.Count - 1

' ''        objProfitLossChart.chtdelta.RowLabelCount = 0
' ''        objProfitLossChart.chtdelta.RowLabelIndex = 0
' ''        objProfitLossChart.chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If deltatable.Columns.Count > 2 Then
' ''            objProfitLossChart.chtdelta.ColumnCount = deltatable.Columns.Count - 3
' ''            objProfitLossChart.chtdelta.ColumnLabelCount = 1
' ''            objProfitLossChart.chtdelta.ColumnLabelIndex = 1
' ''            objProfitLossChart.chtdelta.AutoIncrement = False
' ''            objProfitLossChart.chtdelta.RowCount = deltatable.Rows.Count - 1
' ''            objProfitLossChart.chtdelta.RowLabelCount = 1
' ''            objProfitLossChart.chtdelta.RowLabelIndex = 1

' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(deltaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(deltaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            objProfitLossChart.chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(deltatable.Rows.Count - 1, deltatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In deltatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In deltatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            objProfitLossChart.chtdelta.ChartData = cha
' ''            If div > 1 Then
' ''                lbldelta.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lbldelta.Text = "-"
' ''            End If
' ''            objProfitLossChart.chtdelta.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0

' ''            For Each col As DataColumn In deltatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    objProfitLossChart.chtdelta.Column = c
' ''                    objProfitLossChart.chtdelta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1
' ''                    c += 1
' ''                    'If c >= grddelta.Columns.Count Then
' ''                    '    Exit For
' ''                    'End If
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In deltatable.Rows
' ''                objProfitLossChart.chtdelta.Row = r
' ''                objProfitLossChart.chtdelta.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_gamma()
' ''        chtgamma.ColumnCount = 0
' ''        chtgamma.ColumnLabelCount = 0
' ''        chtgamma.ColumnLabelIndex = 0
' ''        chtgamma.AutoIncrement = False
' ''        chtgamma.RowCount = 0
' ''        chtgamma.RowLabelCount = 0
' ''        chtgamma.RowLabelIndex = 0
' ''        chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If gammatable.Columns.Count > 2 Then
' ''            chtgamma.ColumnCount = gammatable.Columns.Count - 3
' ''            chtgamma.ColumnLabelCount = 1
' ''            chtgamma.ColumnLabelIndex = 1
' ''            chtgamma.AutoIncrement = False
' ''            chtgamma.RowCount = gammatable.Rows.Count - 1
' ''            chtgamma.RowLabelCount = 1
' ''            chtgamma.RowLabelIndex = 1
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(gammaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(gammaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(gammatable.Rows.Count - 1, gammatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In gammatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In gammatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            chtgamma.ChartData = cha
' ''            If div > 1 Then
' ''                lblgamma.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lblgamma.Text = "-"
' ''            End If
' ''            chtgamma.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            For Each col As DataColumn In gammatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    chtgamma.Column = c
' ''                    chtgamma.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1
' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                    'End If
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In gammatable.Rows
' ''                chtgamma.Row = r
' ''                chtgamma.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_gamma_Full()

' ''        objProfitLossChart.chtgamma.ColumnCount = 0
' ''        objProfitLossChart.chtgamma.ColumnLabelCount = 0
' ''        objProfitLossChart.chtgamma.ColumnLabelIndex = 0
' ''        objProfitLossChart.chtgamma.AutoIncrement = False
' ''        objProfitLossChart.chtgamma.RowCount = 0
' ''        objProfitLossChart.chtgamma.RowLabelCount = 0
' ''        objProfitLossChart.chtgamma.RowLabelIndex = 0
' ''        objProfitLossChart.chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If gammatable.Columns.Count > 2 Then
' ''            objProfitLossChart.chtgamma.ColumnCount = gammatable.Columns.Count - 3
' ''            objProfitLossChart.chtgamma.ColumnLabelCount = 1
' ''            objProfitLossChart.chtgamma.ColumnLabelIndex = 1
' ''            objProfitLossChart.chtgamma.AutoIncrement = False
' ''            objProfitLossChart.chtgamma.RowCount = gammatable.Rows.Count - 1
' ''            objProfitLossChart.chtgamma.RowLabelCount = 1
' ''            objProfitLossChart.chtgamma.RowLabelIndex = 1
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(gammaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(gammaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            objProfitLossChart.chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(gammatable.Rows.Count - 1, gammatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In gammatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In gammatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            objProfitLossChart.chtgamma.ChartData = cha
' ''            If div > 1 Then
' ''                lblgamma.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lblgamma.Text = "-"
' ''            End If
' ''            objProfitLossChart.chtgamma.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            For Each col As DataColumn In gammatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    objProfitLossChart.chtgamma.Column = c
' ''                    objProfitLossChart.chtgamma.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1
' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                    'End If
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In gammatable.Rows
' ''                objProfitLossChart.chtgamma.Row = r
' ''                objProfitLossChart.chtgamma.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub

' ''    Private Sub create_chart_vega()
' ''        chtvega.ColumnCount = 0
' ''        chtvega.ColumnLabelCount = 0
' ''        chtvega.ColumnLabelIndex = 0
' ''        chtvega.AutoIncrement = False
' ''        chtvega.RowCount = 0
' ''        chtvega.RowLabelCount = 0
' ''        chtvega.RowLabelIndex = 0
' ''        chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If vegatable.Columns.Count > 2 Then

' ''            chtvega.ColumnCount = vegatable.Columns.Count - 3
' ''            chtvega.ColumnLabelCount = 1
' ''            chtvega.ColumnLabelIndex = 1
' ''            chtvega.AutoIncrement = False
' ''            chtvega.RowCount = vegatable.Rows.Count - 1
' ''            chtvega.RowLabelCount = 1
' ''            chtvega.RowLabelIndex = 1
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(vegaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(vegaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(vegatable.Rows.Count - 1, vegatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In vegatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In vegatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            chtvega.ChartData = cha
' ''            If div > 1 Then
' ''                lblvega.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lblvega.Text = "-"
' ''            End If
' ''            chtvega.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            For Each col As DataColumn In vegatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    chtvega.Column = c
' ''                    chtvega.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1
' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                End If
' ''            Next


' ''            r = 1
' ''            For Each drow As DataRow In vegatable.Rows
' ''                chtvega.Row = r
' ''                chtvega.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_vega_Full()
' ''        objProfitLossChart.chtvega.ColumnCount = 0
' ''        objProfitLossChart.chtvega.ColumnLabelCount = 0
' ''        objProfitLossChart.chtvega.ColumnLabelIndex = 0
' ''        objProfitLossChart.chtvega.AutoIncrement = False
' ''        objProfitLossChart.chtvega.RowCount = 0
' ''        objProfitLossChart.chtvega.RowLabelCount = 0
' ''        objProfitLossChart.chtvega.RowLabelIndex = 0
' ''        objProfitLossChart.chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If vegatable.Columns.Count > 2 Then

' ''            objProfitLossChart.chtvega.ColumnCount = vegatable.Columns.Count - 3
' ''            objProfitLossChart.chtvega.ColumnLabelCount = 1
' ''            objProfitLossChart.chtvega.ColumnLabelIndex = 1
' ''            objProfitLossChart.chtvega.AutoIncrement = False
' ''            objProfitLossChart.chtvega.RowCount = vegatable.Rows.Count - 1
' ''            objProfitLossChart.chtvega.RowLabelCount = 1
' ''            objProfitLossChart.chtvega.RowLabelIndex = 1
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(vegaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(vegaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            objProfitLossChart.chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(vegatable.Rows.Count - 1, vegatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In vegatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In vegatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            objProfitLossChart.chtvega.ChartData = cha
' ''            If div > 1 Then
' ''                lblvega.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lblvega.Text = "-"
' ''            End If
' ''            objProfitLossChart.chtvega.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            For Each col As DataColumn In vegatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    objProfitLossChart.chtvega.Column = c
' ''                    objProfitLossChart.chtvega.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1
' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                End If
' ''            Next


' ''            r = 1
' ''            For Each drow As DataRow In vegatable.Rows
' ''                objProfitLossChart.chtvega.Row = r
' ''                objProfitLossChart.chtvega.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_theta()
' ''        chttheta.ColumnCount = 0
' ''        chttheta.ColumnLabelCount = 0
' ''        chttheta.ColumnLabelIndex = 0
' ''        chttheta.AutoIncrement = False
' ''        chttheta.RowCount = 0
' ''        chttheta.RowLabelCount = 0
' ''        chttheta.RowLabelIndex = 0
' ''        chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If thetatable.Columns.Count > 2 Then
' ''            chttheta.ColumnCount = thetatable.Columns.Count - 3
' ''            chttheta.ColumnLabelCount = 1
' ''            chttheta.ColumnLabelIndex = 1
' ''            chttheta.AutoIncrement = False
' ''            chttheta.RowCount = thetatable.Rows.Count - 1
' ''            chttheta.RowLabelCount = 1
' ''            chttheta.RowLabelIndex = 1
' ''            chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(thetaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(thetaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            Dim cha(thetatable.Rows.Count - 1, thetatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In thetatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In thetatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            If div > 1 Then
' ''                lbltheta.Text = "Rs. in ( " & str & ")"
' ''            Else
' ''                lbltheta.Text = "-"
' ''            End If
' ''            chttheta.ChartData = cha

' ''            chttheta.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
' ''            'End If
' ''            For Each col As DataColumn In thetatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    chttheta.Column = c
' ''                    chttheta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1

' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In thetatable.Rows
' ''                chttheta.Row = r
' ''                chttheta.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_theta_Full()
' ''        objProfitLossChart.chttheta.ColumnCount = 0
' ''        objProfitLossChart.chttheta.ColumnLabelCount = 0
' ''        objProfitLossChart.chttheta.ColumnLabelIndex = 0
' ''        objProfitLossChart.chttheta.AutoIncrement = False
' ''        objProfitLossChart.chttheta.RowCount = 0
' ''        objProfitLossChart.chttheta.RowLabelCount = 0
' ''        objProfitLossChart.chttheta.RowLabelIndex = 0
' ''        objProfitLossChart.chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If thetatable.Columns.Count > 2 Then
' ''            objProfitLossChart.chttheta.ColumnCount = thetatable.Columns.Count - 3
' ''            objProfitLossChart.chttheta.ColumnLabelCount = 1
' ''            objProfitLossChart.chttheta.ColumnLabelIndex = 1
' ''            objProfitLossChart.chttheta.AutoIncrement = False
' ''            objProfitLossChart.chttheta.RowCount = thetatable.Rows.Count - 1
' ''            objProfitLossChart.chttheta.RowLabelCount = 1
' ''            objProfitLossChart.chttheta.RowLabelIndex = 1
' ''            objProfitLossChart.chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(thetaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(thetaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            Dim cha(thetatable.Rows.Count - 1, thetatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In thetatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In thetatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            If div > 1 Then
' ''                lbltheta.Text = "Rs. in ( " & str & ")"
' ''            Else
' ''                lbltheta.Text = "-"
' ''            End If
' ''            objProfitLossChart.chttheta.ChartData = cha

' ''            objProfitLossChart.chttheta.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
' ''            'End If
' ''            For Each col As DataColumn In thetatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    objProfitLossChart.chttheta.Column = c
' ''                    objProfitLossChart.chttheta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1

' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In thetatable.Rows
' ''                objProfitLossChart.chttheta.Row = r
' ''                objProfitLossChart.chttheta.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub

' ''    Private Sub cal_exp()
' ''        Dim exptable As New DataTable

' ''        exptable = objExp.Select_Expenses

' ''        Dim texp As Double = 0

' ''        For Each drow As DataGridViewRow In grdtrad.Rows
' ''            If drow.Cells("Active").Value = False Then Continue For
' ''            If Val(drow.Cells("units").Value) > 0 Then
' ''                If drow.Cells("CPF").Value = "F" Then
' ''                    texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(futl)", "")) / exptable.Compute("max(futlp)", "")))
' ''                Else
' ''                    If Val(exptable.Compute("max(spl)", "")) <> 0 Then
' ''                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * (Val(drow.Cells("last").Value) + Val(drow.Cells("last").Value))) * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", "")))
' ''                    Else
' ''                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", "")))
' ''                    End If
' ''                End If
' ''            Else
' ''                If drow.Cells("CPF").Value = "F" Then
' ''                    texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(futs)", "")) / exptable.Compute("max(futsp)", "")))
' ''                Else
' ''                    If Val(exptable.Compute("max(spl)", "")) <> 0 Then
' ''                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * (Val(drow.Cells("last").Value) + Val(drow.Cells("last").Value))) * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", "")))
' ''                    Else
' ''                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", "")))
' ''                    End If
' ''                End If
' ''            End If
' ''        Next
' ''        txtexp.Text = Math.Round(texp, 2)
' ''    End Sub
' ''    Private Sub import()
' ''        Dim opfile As OpenFileDialog
' ''        opfile = New OpenFileDialog
' ''        opfile.Filter = "Files(*.xls)|*.xls"
' ''        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
' ''            read_file(opfile.FileName)
' ''        End If
' ''    End Sub
' ''    Private Sub read_file(ByVal fpath As String)
' ''        Try
' ''            If fpath <> "" Then
' ''                Dim fi As New FileInfo(fpath)
' ''                'Dim dv As DataView
' ''                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" & fpath
' ''                'Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
' ''                Dim drow As DataRow
' ''                Dim objConn As New OleDbConnection(sConnectionString)

' ''                objConn.Open()

' ''                'Dim objCmdSelect As New OleDbCommand("SELECT Staus,'Time I','Time II',C/P/F,Spot,Strike,Qty,Rate,'Vol(%)',Delta,'Del. Val',Theta,Th.Val,Vega,'Vg. Val',Gamma,'Gam. Val',uid FROM " & fi.Name, objConn)
' ''                'Dim objCmdSelect As New OleDbCommand("SELECT * FROM " & fi.Name, objConn)
' ''                Dim objCmdSelect As New OleDbCommand("SELECT * FROM [Scenario$]", objConn)

' ''                Dim objAdapter1 As New OleDbDataAdapter

' ''                objAdapter1.SelectCommand = objCmdSelect
' ''                Dim tempdata As New DataTable
' ''                tempdata = New DataTable
' ''                objAdapter1.Fill(tempdata)
' ''                objConn.Close()
' ''                grdtrad.Rows.Clear()
' ''                txtunits.Text = 0
' ''                txtdelval.Text = 0
' ''                txtthval.Text = 0
' ''                txtvgval.Text = 0
' ''                txtgmval.Text = 0
' ''                Dim grow As New DataGridViewRow
' ''                For Each drow In tempdata.Rows
' ''                    grow = New DataGridViewRow
' ''                    grdtrad.Rows.Add(grow)
' ''                Next
' ''                Dim i As Integer = 0
' ''                For Each drow In tempdata.Rows

' ''                    grdtrad.Rows(i).Cells("Active").Value = True
' ''                    grdtrad.Rows(i).Cells("TimeI").Value = CDate(drow("Date"))
' ''                    grdtrad.Rows(i).Cells("TimeII").Value = CDate(drow("Expiry"))
' ''                    grdtrad.Rows(i).Cells("CPF").Value = CStr(drow("C/P/F"))
' ''                    grdtrad.Rows(i).Cells("spval").Value = CDbl(drow("Underlying"))
' ''                    grdtrad.Rows(i).Cells("Strike").Value = CDbl(drow("Strike"))
' ''                    grdtrad.Rows(i).Cells("units").Value = Val(drow("Qty"))
' ''                    grdtrad.Rows(i).Cells("ltp").Value = Val(drow("ltp"))
' ''                    grdtrad.Rows(i).Cells("last").Value = CDbl(drow("Rate"))
' ''                    grdtrad.Rows(i).Cells("lv").Value = Val(drow("Vol(%)"))
' ''                    grdtrad.Rows(i).Cells("delta").Value = Val(drow("Delta"))
' ''                    grdtrad.Rows(i).Cells("deltaval").Value = Val(drow("Del Val"))
' ''                    grdtrad.Rows(i).Cells("theta").Value = Val(drow("Theta"))
' ''                    grdtrad.Rows(i).Cells("thetaval").Value = Val(drow("Th val"))
' ''                    grdtrad.Rows(i).Cells("vega").Value = Val(drow("Vega"))
' ''                    grdtrad.Rows(i).Cells("vgval").Value = Val(drow("Vg val"))
' ''                    grdtrad.Rows(i).Cells("gamma").Value = Val(drow("Gamma"))
' ''                    grdtrad.Rows(i).Cells("gmval").Value = Val(drow("Gam val"))
' ''                    grdtrad.Rows(i).Cells("uid").Value = Val(drow("uid"))



' ''                    '  txtunits.Text = Val(txtunits.Text) + Val(grdtrad.Rows(i).Cells(6).Value)
' ''                    txtdelval.Text = Val(txtdelval.Text) + Val(grdtrad.Rows(i).Cells("delta").Value)
' ''                    txtthval.Text = Val(txtthval.Text) + Val(grdtrad.Rows(i).Cells("thetaval").Value)
' ''                    txtvgval.Text = Val(txtvgval.Text) + Val(grdtrad.Rows(i).Cells("vega").Value)
' ''                    txtgmval.Text = Val(txtgmval.Text) + Val(grdtrad.Rows(i).Cells("gamma").Value)

' ''                    i += 1
' ''                Next


' ''            End If
' ''        Catch ex As Exception
' ''            MsgBox("File Not Processed")
' ''            ' MsgBox(ex.ToString)
' ''        End Try




' ''    End Sub
' ''    Private Sub init_result()
' ''        rtable = New DataTable
' ''        With rtable.Columns

' ''            '.Add("timeI", GetType(Date))
' ''            '.Add("timeII", GetType(Date))
' ''            '.Add("cpf")
' ''            '.Add("spval", GetType(Double))
' ''            '.Add("strike", GetType(Double))
' ''            '.Add("units", GetType(Double))
' ''            '.Add("last", GetType(Double))
' ''            '.Add("lv", GetType(Double))
' ''            '.Add("delta", GetType(Double))
' ''            '.Add("deltaval", GetType(Double))
' ''            '.Add("theta", GetType(Double))
' ''            '.Add("thetaval", GetType(Double))
' ''            '.Add("vega", GetType(Double))
' ''            '.Add("vgval", GetType(Double))
' ''            '.Add("gamma", GetType(Double))
' ''            '.Add("gmval", GetType(Double))

' ''            .Add("timeI")
' ''            .Add("timeII")
' ''            .Add("cpf")
' ''            .Add("spval", GetType(Double))
' ''            .Add("strike", GetType(Double))
' ''            .Add("units", GetType(Double))
' ''            .Add("last", GetType(Double))
' ''            .Add("lv", GetType(Double))
' ''            .Add("delta", GetType(Double))
' ''            .Add("deltaval", GetType(Double))
' ''            .Add("theta", GetType(Double))
' ''            .Add("thetaval", GetType(Double))
' ''            .Add("vega", GetType(Double))
' ''            .Add("vgval", GetType(Double))
' ''            .Add("gamma", GetType(Double))
' ''            .Add("gmval", GetType(Double))
' ''            .Add("pl", GetType(Double))


' ''        End With
' ''    End Sub
' ''    Private Sub fill_result(ByVal ind As Integer, ByVal rind As Integer, ByVal cind As Integer)
' ''        If rind = -1 Then Exit Sub
' ''        init_result()
' ''        Dim iscall As Boolean = False
' ''        Dim drow As DataRow
' ''        Dim _mT As Double = 0

' ''        Select Case ind
' ''            Case 0

' ''                'If CDate(profit.Columns(cind).ColumnName) < CDate(dtexp.Text) Then
' ''                '    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName), CDate(dtexp.Text))
' ''                '    'Else
' ''                '    '    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName), CDate(dtexp.Text))
' ''                'End If

' ''                'mt = DateDiff(DateInterval.Day, CDate(drow("mdate")), Now())

' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    If grow.Cells("Active").Value = False Then Continue For
' ''                    If CBool(grow.Cells("Active").Value) = True Then
' ''                        drow = rtable.NewRow
' ''                        If (IsDate(profit.Columns(cind).ColumnName)) Then
' ''                            drow("timeI") = Format(CDate(profit.Columns(cind).ColumnName), "MMM/dd/yyyy")

' ''                        ElseIf (cind) = profit.Columns.Count - 1 Then
' ''                            drow("timeI") = Format(CDate(Mid(profit.Columns(cind).ColumnName, 7, Len(profit.Columns(cind).ColumnName) - 1)), "MMM/dd/yyyy")
' ''                        Else
' ''                            Exit Sub
' ''                            'drow("timeI") = Format(CDate(Mid(profit.Columns(cind).ColumnName, 1, Len(profit.Columns(cind).ColumnName) - 4)), "MMM/dd/yyyy")
' ''                        End If



' ''                        'drow("timeII") = Format(CDate(dtexp.Text), "MMM/dd/yyyy")
' ''                        'drow("timeI") = Format(CDate(grow.Cells(1).Value), "MMM/dd/yyyy")
' ''                        drow("timeII") = Format(CDate(grow.Cells("TimeII").Value), "MMM/dd/yyyy")
' ''                        drow("cpf") = grow.Cells("CPF").Value
' ''                        drow("spval") = Val(profit.Rows(rind)(0))
' ''                        drow("strike") = Val(grow.Cells("Strike").Value)
' ''                        drow("units") = Val(grow.Cells("units").Value)
' ''                        drow("last") = Val(grow.Cells("ltp").Value)            ' 9 is used
' ''                        drow("lv") = Val(grow.Cells("lv").Value)
' ''                        If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
' ''                            drow("delta") = Val(grow.Cells("delta").Value)
' ''                            drow("deltaval") = Val(grow.Cells("deltaval").Value)
' ''                            drow("theta") = Val(0)
' ''                            drow("thetaval") = Val(0)
' ''                            drow("vega") = Val(0)
' ''                            drow("vgval") = Val(0)
' ''                            drow("gamma") = Val(0)
' ''                            drow("gmval") = Val(0)
' ''                            drow("pl") = Math.Round(Val(((Val(profit.Rows(rind)(0)) - Val(grow.Cells("spval").Value)) * Val(grow.Cells("units").Value))), 2)
' ''                        Else

' ''                            If CDate(drow("timeI")) < CDate(drow("timeII")) Then
' ''                                If (IsDate(profit.Columns(cind).ColumnName)) Then
' ''                                    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName).Date, CDate(drow("timeII")).Date)
' ''                                Else
' ''                                    _mT = DateDiff(DateInterval.Day, CDate(Mid(profit.Columns(cind).ColumnName, 1, Len(profit.Columns(cind).ColumnName) - 4)).Date, CDate(drow("timeII")).Date)
' ''                                End If
' ''                                'Else
' ''                                '    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName), CDate(dtexp.Text))
' ''                            End If

' ''                            If grow.Cells("CPF").Value = "C" Then
' ''                                iscall = True
' ''                            Else
' ''                                iscall = False
' ''                            End If

' ''                            If _mT = 0 Then
' ''                                _mT = 0.0001
' ''                            Else
' ''                                _mT = (_mT) / 365
' ''                            End If
' ''                            Dim bval As Double
' ''                            bval = 0
' ''                            bval = mObjData.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 1)
' ''                            drow("delta") = Math.Round(CDbl(bval), 2)
' ''                            drow("deltaval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)), 2)
' ''                            bval = mObjData.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 4)
' ''                            drow("theta") = Math.Round(CDbl(bval), 2)
' ''                            drow("thetaval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)))
' ''                            bval = mObjData.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 3)
' ''                            drow("vega") = Math.Round(CDbl(bval), 2)
' ''                            drow("vgval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)), 2)
' ''                            bval = mObjData.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 2)
' ''                            drow("gamma") = Math.Round(CDbl(bval), 5)
' ''                            drow("gmval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)), 5)
' ''                            bval = mObjData.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 0)

' ''                            drow("pl") = Math.Round(CDbl(((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))), 2)  ' 9 is used
' ''                        End If

' ''                        rtable.Rows.Add(drow)
' ''                    End If
' ''                Next
' ''                drow = rtable.NewRow
' ''                drow("timeI") = ""
' ''                drow("timeII") = ""
' ''                drow("cpf") = "Total"
' ''                drow("spval") = 0
' ''                drow("strike") = 0
' ''                drow("units") = Val(rtable.Compute("sum(units)", ""))
' ''                drow("last") = 0
' ''                drow("lv") = 0
' ''                drow("delta") = 0
' ''                drow("deltaval") = Val(rtable.Compute("sum(deltaval)", ""))
' ''                drow("theta") = Val(0)
' ''                drow("thetaval") = Val(rtable.Compute("sum(thetaval)", ""))
' ''                drow("vega") = Val(0)
' ''                drow("vgval") = Val(rtable.Compute("sum(vgval)", ""))
' ''                drow("gamma") = Val(0)
' ''                drow("gmval") = Val(rtable.Compute("sum(gmval)", ""))
' ''                drow("pl") = Math.Round(Val(rtable.Compute("sum(pl)", "")), 2) + grossmtm
' ''                rtable.Rows.Add(drow)
' ''        End Select
' ''    End Sub
' ''    Public Function MaxValOfIntArray(ByRef TheArray As Object) As Double
' ''        'This function gives max value of int array without sorting an array
' ''        Dim i As Integer
' ''        Dim MaxIntegersIndex As Integer
' ''        MaxIntegersIndex = 0

' ''        For i = 1 To UBound(TheArray)
' ''            If Val(TheArray(i)) > Val(TheArray(MaxIntegersIndex)) Then
' ''                MaxIntegersIndex = i
' ''            End If
' ''        Next
' ''        'index of max value is MaxValOfIntArray
' ''        MaxValOfIntArray = Val(TheArray(MaxIntegersIndex))
' ''    End Function
' ''    Public Function MinValOfIntArray(ByRef TheArray As Object) As Double
' ''        'This function gives max value of int array without sorting an array
' ''        Dim i As Integer
' ''        Dim MaxIntegersIndex As Integer
' ''        MaxIntegersIndex = 0

' ''        For i = 1 To UBound(TheArray)
' ''            If Val(TheArray(i)) < Val(TheArray(MaxIntegersIndex)) Then
' ''                MaxIntegersIndex = i
' ''            End If
' ''        Next
' ''        'index of max value is MaxValOfIntArray
' ''        MinValOfIntArray = Val(TheArray(MaxIntegersIndex))
' ''    End Function
' ''    Private Sub export()
' ''        Dim savedi As New SaveFileDialog
' ''        savedi.Filter = "Files(*.xls)|*.xls"
' ''        If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
' ''            Dim grd(5) As DataGridView
' ''            grd(0) = grdtrad
' ''            grd(1) = grdprofit
' ''            grd(2) = grddelta
' ''            grd(3) = grdtheta
' ''            grd(4) = grdvega
' ''            grd(5) = grdgamma
' ''            Dim sname(5) As String
' ''            sname(0) = "Scenario"
' ''            sname(1) = "Profit & Loss"
' ''            sname(2) = "Delta"
' ''            sname(3) = "Theta"
' ''            sname(4) = "Vega"
' ''            sname(5) = "Gamma"
' ''            exporttoexcel(grd, savedi.FileName, sname, "scenario")
' ''            'exporttoexcel(grdprofit)
' ''            'Dim str As String
' ''            'str = Mid(savedi.FileName, 1, Len(savedi.FileName) - 4)
' ''            'exportExcel(grdtrad, str & "_scenario.csv")
' ''            'exportExcel(grdprofit, str & "_ProfitLoss.csv")
' ''            'exportExcel(grddelta, str & "_Delta.csv")
' ''            'exportExcel(grdtheta, str & "_Theta.csv")
' ''            'exportExcel(grdvega, str & "_Vega.csv")
' ''            'exportExcel(grdgamma, str & "_Gamma.csv")
' ''            'MsgBox("Export Complete.", MsgBoxStyle.Information)
' ''        End If
' ''    End Sub
' ''    Private Sub create_active()
' ''        grdact.Columns.Clear()
' ''        Dim i As Integer = 0
' ''        If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''            i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
' ''        End If
' ''        i += 1
' ''        Dim gcol As DataGridViewCheckBoxColumn
' ''        Dim temptable As New DataTable
' ''        temptable = New DataTable
' ''        Dim tdate As Date
' ''        For k As Integer = 0 To i - 1
' ''            tdate = Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MMM/yyyy")
' ''            If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
' ''                gcol = New DataGridViewCheckBoxColumn
' ''                gcol.HeaderText = tdate
' ''                gcol.DataPropertyName = tdate
' ''                gcol.Width = 70
' ''                grdact.Columns.Add(gcol)
' ''                temptable.Columns.Add(tdate, GetType(Boolean))
' ''                gcol.DefaultCellStyle.NullValue = False
' ''            End If
' ''        Next

' ''        tdate = Format(dtexp.Value, "dd/MMM/yyyy")
' ''        If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
' ''            gcol = New DataGridViewCheckBoxColumn
' ''            gcol.HeaderText = "Expiry"
' ''            gcol.DataPropertyName = "Expiry " & tdate
' ''            grdact.Columns.Add(gcol)
' ''            temptable.Columns.Add("Expiry " & tdate, GetType(Boolean))
' ''            gcol.DefaultCellStyle.NullValue = False
' ''        End If

' ''        If grdact.Columns.Count > 0 Then
' ''            Dim drow As DataRow
' ''            drow = temptable.NewRow
' ''            'For k As Integer = 0 To i - 1
' ''            tdate = Format(DateAdd(DateInterval.Day, 0, dttoday.Value), "dd/MMM/yyyy")
' ''            If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
' ''                drow(tdate) = True
' ''            End If
' ''            Try
' ''                tdate = Format(DateAdd(DateInterval.Day, (i - 1), dttoday.Value), "dd/MMM/yyyy")
' ''                If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
' ''                    drow(tdate) = True
' ''                    drow("Expiry " & tdate) = True
' ''                End If
' ''            Catch ex As Exception
' ''                MsgBox("Beginning date can't be greater than expiry")
' ''                Exit Sub
' ''            End Try
' ''            Dim t As Integer
' ''            t = Math.Round((Val(grdact.ColumnCount - 1)) / 2, 0)
' ''            tdate = Format(DateAdd(DateInterval.Day, t, dttoday.Value), "dd/MMM/yyyy")
' ''            If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
' ''                drow(tdate) = True
' ''            End If
' ''            ' Next
' ''            temptable.Rows.Add(drow)
' ''            grdact.DataSource = temptable
' ''            'grdact.Refresh()
' ''        End If

' ''        'assign expiry date to selected expiry date
' ''        If frmFlg = True Then 'this flg used bcoz first need not to assign expiry date
' ''            For Each grow As DataGridViewRow In grdtrad.Rows
' ''                If grow.Cells("TimeII").ReadOnly = False Then
' ''                    grow.Cells("TimeII").Value = dtexp.Value
' ''                End If
' ''            Next
' ''        End If
' ''        frmFlg = True
' ''        'If grdtrad.Rows(grdtrad.NewRowIndex).Cells("TimeII").ReadOnly = False Then
' ''        '    grdtrad.Rows(grdtrad.NewRowIndex).Cells("TimeII").Value = dtexp.Value
' ''        'End If

' ''        If grdtrad.Rows.Count > 1 Then
' ''            grdtrad.Rows(grdtrad.Rows.Count - 1).Cells(4).Value = grdtrad.Rows(grdtrad.Rows.Count - 2).Cells(4).Value
' ''        End If
' ''    End Sub
' ''#End Region
' ''    Private Sub chkint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkint.CheckedChanged
' ''        If chkint.Checked = True Then
' ''            txtinterval.Text = 2
' ''            Label2.Visible = True
' ''        Else
' ''            Label2.Visible = False
' ''            txtinterval.Text = 100
' ''        End If
' ''    End Sub
' ''    Private Function cal_profitLoss() As Double
' ''        Dim profit As Double = 0
' ''        For Each grow As DataGridViewRow In grdtrad.Rows
' ''            If grow.Cells("Active").Value = False Then Continue For
' ''            If grow.Cells("Active").Value = True Then profit += (grow.Cells("units").Value) * (grow.Cells("last").Value - grow.Cells("ltp").Value)
' ''        Next
' ''        profit += grossmtm
' ''        Return profit
' ''    End Function
' ''    Private Function cal_FutprofitLoss() As Double
' ''        Dim profit As Double = 0
' ''        For Each grow As DataGridViewRow In grdtrad.Rows
' ''            If grow.Cells("Active").Value = False Then Continue For
' ''            If grow.Cells("Active").Value = True Then profit += (grow.Cells("units").Value) * (grow.Cells("last").Value - grow.Cells("ltp1").Value)
' ''        Next
' ''        profit += grossmtm
' ''        Return profit
' ''    End Function

' ''    Private Sub txtmkt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmkt.TextChanged

' ''    End Sub
' ''    Private Sub grdact_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdact.CellContentClick

' ''    End Sub

' ''    Private Sub chtprofit_ChartSelected(ByVal sender As System.Object, ByVal e As AxMSChart20Lib._DMSChartEvents_ChartSelectedEvent) Handles chtprofit.ChartSelected

' ''    End Sub

' ''    Private Sub chtprofit_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtprofit.DblClick
' ''        chartName = "profit"
' ''        ValLBLprofit = lblprofit.Text
' ''        objProfitLossChart.Show()
' ''    End Sub

' ''    Private Sub tbcon_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbcon.SelectedIndexChanged

' ''    End Sub

' ''    Private Sub chtgamma_ChartSelected(ByVal sender As System.Object, ByVal e As AxMSChart20Lib._DMSChartEvents_ChartSelectedEvent) Handles chtgamma.ChartSelected

' ''    End Sub

' ''    Private Sub chttheta_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chttheta.DblClick
' ''        chartName = "theta"
' ''        ValLBLtheta = lbltheta.Text
' ''        objProfitLossChart.Show()
' ''    End Sub

' ''    Private Sub chtvega_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtvega.DblClick
' ''        chartName = "vega"
' ''        ValLBLvega = lblvega.Text
' ''        objProfitLossChart.Show()
' ''    End Sub

' ''    Private Sub chtdelta_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtdelta.DblClick
' ''        chartName = "delta"
' ''        ValLBLdelta = lbldelta.Text
' ''        objProfitLossChart.Show()
' ''    End Sub

' ''    Private Sub chtgamma_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtgamma.DblClick
' ''        chartName = "gamma"
' ''        ValLBLgamma = lblgamma.Text
' ''        objProfitLossChart.Show()
' ''    End Sub



' ''    Private Sub Vol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Vol.Click
' ''        Try
' ''            If Vol.Text = "Freeze Vol Column" Then
' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    grow.Cells("lv").ReadOnly = True
' ''                    Vol.Text = "Unfreeze Vol Column"
' ''                Next
' ''            Else
' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    grow.Cells("lv").ReadOnly = False
' ''                    Vol.Text = "Freeze Vol Column"
' ''                Next
' ''            End If

' ''        Catch ex As Exception
' ''            MsgBox(ex.ToString)
' ''        End Try
' ''    End Sub

' ''    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

' ''    End Sub
' ''    Private Sub grdtrad_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdtrad.CellMouseDown
' ''        If e.RowIndex <> -1 Then
' ''            cellno = e.RowIndex
' ''        End If
' ''    End Sub
' ''    Private Sub txtinterval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtinterval.TextChanged

' ''    End Sub
' ''    Private Sub txtllimit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

' ''    End Sub

' ''    Private Sub txtexp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

' ''    End Sub

' ''    Private Sub dttoday_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dttoday.ValueChanged

' ''    End Sub

' ''    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

' ''    End Sub

' ''    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

' ''    End Sub

' ''    Private Sub dtexp_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtexp.ValueChanged

' ''    End Sub

' ''    Private Sub label30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles label30.Click

' ''    End Sub

' ''    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

' ''    End Sub

' ''    Private Sub Label29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label29.Click

' ''    End Sub

' ''    Private Sub Label27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label27.Click

' ''    End Sub

' ''    Private Sub Label17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label17.Click

' ''    End Sub

' ''    Private Sub TableLayoutPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel1.Paint

' ''    End Sub

' ''    Private Sub txtdays_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdays.TextChanged

' ''    End Sub

' ''    Private Sub label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles label1.Click

' ''    End Sub

' ''    Private Sub scenario_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
' ''        If Not XResolution = Screen.PrimaryScreen.Bounds.Width Then
' ''            XResolution = Screen.PrimaryScreen.Bounds.Width
' ''            Call setGridTrad()
' ''        End If
' ''    End Sub
' ''    Private Sub setGridTrad()
' ''        Dim intX As Integer
' ''        intX = Screen.PrimaryScreen.Bounds.Width
' ''        Dim diff As Double = intX - 1024

' ''        If diff = 0 Then
' ''            Exit Sub
' ''        ElseIf diff > 0 Then
' ''            diff = diff / 16
' ''            Dim i As Integer
' ''            For i = 0 To grdtrad.Columns.Count - 1
' ''                grdtrad.Columns(i).Width += diff + 2
' ''            Next
' ''        Else
' ''            diff = diff / 16
' ''            Dim i As Integer
' ''            For i = 0 To grdtrad.Columns.Count - 1
' ''                grdtrad.Columns(i).Width += diff
' ''            Next
' ''        End If
' ''    End Sub

' ''    Private Sub grdprofit_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdprofit.CellContentClick

' ''    End Sub

' ''    Private Sub expiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles expiry.Click
' ''        Try
' ''            If expiry.Text = "Freeze Expiry Column" Then
' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    grow.Cells("TimeII").ReadOnly = True
' ''                    expiry.Text = "Unfreeze Expiry Column"
' ''                Next
' ''            Else
' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    grow.Cells("TimeII").ReadOnly = False
' ''                    expiry.Text = "Freeze Expiry Column"
' ''                Next
' ''            End If
' ''        Catch ex As Exception
' ''            MsgBox(ex.ToString)
' ''        End Try
' ''    End Sub

' ''    Private Sub btnincrease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnincrease.Click
' ''        involflag = True
' ''        Call IncDecVolCal()
' ''    End Sub
' ''    Private Sub btndecrease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndecrease.Click
' ''        decvolflag = True
' ''        Call IncDecVolCal()
' ''    End Sub
' ''    Private Sub IncDecVolCal() ''divyesh
' ''        If txtvol.Text.Trim = "" Then
' ''            txtvol.Text = 0
' ''        End If
' ''        If grdtrad.Rows.Count >= 0 Then
' ''            For Each grow As DataGridViewRow In grdtrad.Rows
' ''                If grow.Cells("CPF").Value = "C" Or grow.Cells("CPF").Value = "P" Then
' ''                    If grow.Cells("lv").ReadOnly = False Then
' ''                        If involflag = True Then
' ''                            grow.Cells("lv").Value = Format(grow.Cells("lv").Value + Val(txtvol.Text), "#0.00")
' ''                        ElseIf decvolflag = True Then
' ''                            If grow.Cells("lv").Value - Val(txtvol.Text) > 0 Then
' ''                                grow.Cells("lv").Value = Format(grow.Cells("lv").Value - Val(txtvol.Text), "#0.00")
' ''                            End If
' ''                        End If
' ''                    End If
' ''                    Dim mt As Integer
' ''                    Dim mmt As Integer
' ''                    Dim futval As Double

' ''                    mt = DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date)
' ''                    mmt = (DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)) - 1
' ''                    If mmt = 0 Or mt = 0 Then
' ''                        mt = 0.05
' ''                        mmt = 0.05
' ''                    End If
' ''                    futval = Val(grow.Cells("spval").Value)
' ''                    Dim VarIsCall As Boolean = False
' ''                    If grow.Cells("CPF").Value = "C" Then
' ''                        VarIsCall = True
' ''                    End If
' ''                    CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, VarIsCall, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
' ''                    cal_summary()
' ''                End If
' ''            Next
' ''            involflag = False
' ''            decvolflag = False
' ''        End If

' ''    End Sub
' ''    Private Sub txtvol_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtvol.KeyPress
' ''        numonly(e)
' ''    End Sub

' ''    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label21.Click

' ''    End Sub

' ''    Private Sub grdtrad_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtrad.CellLeave
' ''        'If (grdtrad.Columns(e.ColumnIndex).Name = "TimeII") Then
' ''        '    If (IsDate(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False) Then
' ''        '        'grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dtexp.Value
' ''        '        grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
' ''        '        MsgBox("Please Enter Proper Date...")
' ''        '        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dttoday.Value
' ''        '    End If
' ''        'End If
' ''        'If (grdtrad.Columns(e.ColumnIndex).Name = "TimeII") Then
' ''        '    If (IsDate(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False) Then
' ''        '        grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dtexp.Value
' ''        '        grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
' ''        '        MsgBox("Please Enter Proper Date...")
' ''        '        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dttoday.Value
' ''        '    Else
' ''        '        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dttoday.Value
' ''        '    End If
' ''        'End If

' ''    End Sub


' ''    Private Sub grdtrad_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtrad.CellContentClick

' ''    End Sub
' ''End Class