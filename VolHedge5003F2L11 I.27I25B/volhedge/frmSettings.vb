Imports System.IO
Imports System.Configuration
Imports System.Net.Sockets
Imports System.Net
Public Class frmSettings
    Dim bDefaultInx As Boolean = False
    Dim objTrad As New trading
    Dim DtSetting As New DataTable
    Dim objExp As New expenses

    Public port As Integer
    Dim DtTCPConnSetting As New DataTable
    Dim DtImportType As DataTable
    Dim DtImport_Setting As New DataTable
    Dim VarIsFrmLoad As Boolean = False

    Private allowCoolMove As Boolean = False
    Private myCoolPoint As New Point
    Dim isformseetingload As Boolean = False
    Dim objUser As New user_master
    Dim objDel As New deletedata
    Dim stripaddress1 As String
    Dim stripaddress2 As String
    Dim stripaddress3 As String
    Dim stripaddress4 As String
    Dim strFOudpport As String
    Dim strudpip1 As String
    Dim strudpip2 As String
    Dim strudpip3 As String
    Dim strudpip4 As String
    Dim strCMudpport As String
    Dim strcurrencyipaddress1 As String
    Dim strcurrencyipaddress2 As String
    Dim strcurrencyipaddress3 As String
    Dim strcurrencyipaddress4 As String
    Dim strcurrencyudpport As String
    Dim objAna As New analysisprocess
    Public CustomFlag As Boolean

    Private Sub txtExitExp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butExitExp.Click
        Me.Close()
    End Sub
    Private Sub FillConnectionDataTocomboBox()

        'Try

        '    If IsDBNull(TCP_CON_NAME) = True Then
        '        TCP_CON_NAME = GdtSettings.Compute("max(SettingKey)", "SettingName='TCP_CON_NAME'").ToString.Trim
        '        cmbServerName.Text = TCP_CON_NAME
        '    Else
        '        DtTCPConnSetting = SelectdataofTcpConnection()
        '        cmbServerName.DataSource = DtTCPConnSetting
        '        cmbServerName.DisplayMember = "ConnectionName"
        '        'TCP_CON_NAME = GdtSettings.Compute("max(SettingKey)", "SettingName='TCP_CON_NAME'").ToString.Trim
        '        cmbServerName.Text = TCP_CON_NAME
        '    End If
        'Catch ex As Exception

        'End Try

        If IsDBNull(TCP_CON_NAME) = True Then
            TCP_CON_NAME = GdtSettings.Compute("max(SettingKey)", "SettingName='TCP_CON_NAME'").ToString.Trim
            cmbServerName.Text = TCP_CON_NAME
        Else
            'DtTCPConnSetting = SelectdataofTcpConnection()
            Try
                cmbServerName.DataSource = Nothing
                'cmbServerName.Items.Clear()
            DtTCPConnSetting = objTrad.select_TCP_Connection()
            cmbServerName.DataSource = DtTCPConnSetting
            cmbServerName.DisplayMember = "ConnectionName"
            'TCP_CON_NAME = GdtSettings.Compute("max(SettingKey)", "SettingName='TCP_CON_NAME'").ToString.Trim
                cmbServerName.Text = TCP_CON_NAME
            Catch ex As Exception

            End Try
        End If


    End Sub
    Public Function SelectdataofTcpConnection() As DataTable
        Try

            Dim Query As String = "select * from tblTCPConnection Where Visible='True'"
            DAL.data_access_sql.Cmd_Text = Query

            Return DAL.data_access_sql.FillListfin()
        Catch ex As Exception
            ' MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function SelectdataofAPIConnection() As DataTable
        Try

            Dim Query As String = "select * from tblTCPConnection Where Visible='True' And ConnectionName='API'"
            DAL.data_access_sql.Cmd_Text = Query

            Return DAL.data_access_sql.FillListfin()
        Catch ex As Exception
            ' MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function SelectdataofREGConnection() As DataTable
        Try

            Dim Query As String = "select * from tblREGConnection Where Visible='True'"
            DAL.data_access_sql.Cmd_Text = Query

            Return DAL.data_access_sql.FillListfinREG()
        Catch ex As Exception
            ' MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function SelectdataofTcpConnection_maxno() As DataTable
        Try

            Dim Query As String = "select max(MaxNo) as maxno from tblTCPConnection Where Visible='True'"
            DAL.data_access_sql.Cmd_Text = Query

            Return DAL.data_access_sql.FillListfin()
        Catch ex As Exception
            ' MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function Delete_TCPConnection() As Integer
        Try

            Dim qry As String

            qry = "DELETE  FROM tblTCPConnection;"

            DAL.data_access.ExecuteQuery(qry)

        Catch ex As Exception


        End Try


    End Function
    Private Sub btnExitMarg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExitMarg.Click
        Me.Close()
    End Sub

    Private Sub btnExitRound_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExitRound.Click
        Me.Close()
    End Sub

    Private Sub btnExitConn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExitConn.Click
        Me.Close()
    End Sub

    Private Sub btnExitImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExitImport.Click
        Me.Close()
    End Sub

    Private Sub butExitGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butExitGen.Click
        Me.Close()
    End Sub

    Private Sub frmSettings_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Sub ShowForm(ByVal DefaultInx As Integer)
        bDefaultInx = True
        TabControl1.SelectTab(DefaultInx)
        Me.ShowDialog()
    End Sub
    Private Sub FillSymbolToChklistboxsymbol()

        chkLBsymbol.Items.Clear()
        Dim Allsymbol As New DataTable
        Allsymbol = objTrad.select_chklistbox_symbol_all()


        Dim Htable As New Hashtable()
        Dim boolAll As Boolean = True
        Dim dtsymbol As New DataTable
        dtsymbol = objTrad.select_chklistbox_symbol()

        For Each item As DataRow In dtsymbol.Select()

            Htable.Add(item("Symbol").ToString(), item("Symbol").ToString())
        Next

        If Htable.Count > 0 Then
            boolAll = False
        End If

        For Each dritem As DataRow In Allsymbol.Select()
            If boolAll Then
                chkLBsymbol.Items.Add(dritem("Symbol").ToString(), boolAll)
            Else
                If Htable.Contains(dritem("Symbol").ToString()) Then
                    chkLBsymbol.Items.Add(dritem("Symbol").ToString(), True)
                Else
                    chkLBsymbol.Items.Add(dritem("Symbol").ToString(), False)
                End If

            End If

        Next





    End Sub
    Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''  port = txtFOudpport.Text
        If verVersion = "MI" Then
            grpInstance.Enabled = True
        Else

            grpInstance.Enabled = False
        End If
        isformseetingload = True
        If NetMode = "TCP" Then
            FillConnectionDataTocomboBox()
        End If
        If NetMode = "JL" Then
            FillConnectionDataTocomboBox()
        End If
        Call FillSymbolToChklistboxsymbol()
        Call FillGeneralDataToTextBox()
        Call FillExpenseDataToTextBox()
        Call FillImportSettingDataToTextBox()
        Call FillConnectionDataToTextBox()
        Call FillRoundingDataToTextBox()
        Call FillMarginDataToTextBox()
        Call FillGreekNeutralsetting()
        'Call FillSqlConnInformation()
        fill_company()
        fill_company_summary()
        Me.WindowState = FormWindowState.Normal
        Me.Refresh()
        VarIsFrmLoad = True
        If bDefaultInx = False Then
            TabControl1.SelectedIndex = 0
        End If
        If FLG_REG_SERVER_CONN = True Then
            OptUdp.Enabled = False
            OptJL.Enabled = False
            OptAPI.Enabled = False
            GroupBoxUDPSetting.Enabled = False
        End If
        If AppLicMode = "INDLIC" Then
            GroupBoxUDPSetting.Visible = True
            'GrpBoxSqlServer.Visible = True
            OptUdp.Visible = True
            'OptTcp.Visible = True
            OptINet.Visible = True
            GBInternet.Visible = True
        ElseIf AppLicMode = "SERLIC" Then
            GroupBoxUDPSetting.Visible = False
            'GrpBoxSqlServer.Visible = True
            OptUdp.Visible = True
            'OptTcp.Visible = True
            OptINet.Visible = True
            GBInternet.Visible = True
        ElseIf AppLicMode = "NETLIC" Then
            ' GroupBoxUDPSetting.Visible = False
            cboBoxInstance.Enabled = False
            GrpBoxSqlServer.Visible = False
            '   OptUdp.Visible = False
            OptTcp.Visible = False
            OptINet.Visible = True
            OptINet.Checked = True
            GBInternet.Visible = True
            If clsGlobal.InternetVersionFlag = True Then
                If clsGlobal.FlagTCP = 1 And NetMode = "TCP" Then
                    OptINet.Visible = True
                    OptINet.Checked = False
                    OptTcp.Checked = True
                    'OptTcp.Visible = True
                    OptTcp.Enabled = True
                    'GrpBoxSqlServer.Visible = True
                ElseIf clsGlobal.FlagTCP = 1 And NetMode = "NET" Then
                    OptINet.Visible = True
                    OptINet.Checked = True
                    OptTcp.Checked = False
                    'OptTcp.Visible = True
                    OptTcp.Enabled = True
                    GBInternet.Visible = True
                    'GrpBoxSqlServer.Visible = True
                ElseIf NetMode = "JL" Then
                    OptJL.Checked = True
                    GBInternet.Visible = False
                    ' OptTcp.Enabled = False
                    OptINet.Checked = False
                    OptUdp.Checked = False
                ElseIf NetMode = "API" Then
                    OptAPI.Checked = True
                    OptINet.Checked = False
                    GBInternet.Visible = False
                    OptJL.Checked = False
                ElseIf NetMode = "UDP" Then


                    OptAPI.Checked = False
                    OptINet.Checked = False
                    GBInternet.Visible = False
                    OptJL.Checked = False
                    OptUdp.Checked = True
                    GroupBoxUDPSetting.Visible = True
                    ' GrpBoxSqlServer.Visible = True
                    OptUdp.Visible = True
                    '   OptTcp.Visible = True
                    OptINet.Visible = True
                Else

                    OptTcp.Enabled = False
                End If
                'Else
                '    GroupBoxUDPSetting.Visible = True
                '    GrpBoxSqlServer.Visible = True
                '    OptUdp.Visible = True
                '    OptTcp.Visible = True
                '    OptINet.Visible = True
            End If
            'If NetMode = "TCP" Then
            '    FillConnectionDataTocomboBox()
            'End If
        End If
        isformseetingload = False

    End Sub
    'Private Sub FillSqlConnInformation()
    '    If IO.File.Exists(Application.StartupPath & "\SQL Server Connection.txt") = False Then
    '        Exit Sub
    '    End If
    '    Dim FR As New IO.StreamReader(Application.StartupPath & "\SQL Server Connection.txt")
    '    Dim Str As String = ""
    '    Str = FR.ReadLine()
    '    txtServerName.Text = Str.Substring("Server Name ::".Length)
    '    Str = FR.ReadLine()
    '    txtDatabase.Text = Str.Substring("Database Name ::".Length)
    '    Str = FR.ReadLine()
    '    If Str.Substring("Authentication ::".Length) = "Windows" Then
    '        rbtnWindows.Checked = True
    'Else
    '        rbtnSQLServer.Checked = True
    '        Str = FR.ReadLine()
    '        txtUserName.Text = Str.Substring("User Name ::".Length)
    '        Str = FR.ReadLine()
    '        txtPassword.Text = Str.Substring("Password ::".Length)
    'End If
    '    FR.Close()

    '    'If GVarIsSQLConn = True Then
    '    rbtnSQLServer.Checked = True
    '    'Else
    '    'rbtnWindows.Checked = True
    '    'End If
    'End Sub
    Private Sub butSaveGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSaveGen.Click
        Call SaveGeneral()
    End Sub

#Region "Fill tabs"
    Private Sub FillGeneralDataToTextBox()
        For Each DR As DataRow In GdtSettings.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim Setting_Key As String = DR("Settingkey").ToString
            Select Case DR("SettingName").ToString.ToUpper
                Case "BACKUP_PATH"
                    txtbackuppath.Text = Setting_Key
                Case "ZERO_QTY_ANALYSIS"
                    If Setting_Key = "0" Then
                        cmbzero.SelectedIndex = 0
                    Else
                        cmbzero.SelectedIndex = 1
                    End If
                Case "TIMER_CALCULATION_INTERVAL"
                    txtbtimer.Text = Setting_Key

                Case "CDELAY"
                    TxtCDelay.Text = Setting_Key
                Case "NDELAY"
                    TxtNDelay.Text = Setting_Key
                Case "FDELAY"
                    TxtFDelay.Text = Setting_Key

                Case "CDATE"
                    DtCDate.Value = CDate(Setting_Key)
                Case "NDATE"
                    DtNDate.Value = CDate(Setting_Key)
                Case "FDATE"
                    DtFDate.Text = CDate(Setting_Key)

                Case "SPAN_TIMER"
                    TextBox1.Text = Setting_Key
                Case "MATURITY_FAR_MONTH"
                    cmbFarMonth.Text = Setting_Key
                Case "NEUTRALIZE_MONTH"
                    cbnutltpmon.Text = Setting_Key
                Case "AUTO CONTRACT REFRESH"
                    ChkAutoContract.Checked = Boolean.Parse(Setting_Key)
                Case "CONTRACT FILE PATH"
                    txtContractFilePath.Text = Setting_Key
                Case "CONTRACT FILE NAME"
                    lblContractFileName.Text = Setting_Key
                Case "AUTO SECURITY REFRESH"
                    ChkAutoSecurity.Checked = Boolean.Parse(Setting_Key)
                Case "SECURITY FILE PATH"
                    txtSecurityFilePath.Text = Setting_Key
                Case "SECURITY FILE NAME"
                    lblSecurityFileName.Text = Setting_Key

                Case "AUTO_BHAVCOPY_REFRESH"
                    ChkAutoBhavCopy.Checked = Boolean.Parse(Setting_Key)
                Case "CAL_SYN_ON_EXPIRY"
                    If Setting_Key = 1 Then
                        chksynonexpiry.Checked = True
                    Else
                        chksynonexpiry.Checked = False
                    End If
                Case "WAVV_SETTING"
                    If Setting_Key = 1 Then
                        CHKwavv.Checked = True
                    Else
                        CHKwavv.Checked = False
                    End If
                    'Case "NEW_CM_BROADCAST"
                    '    If Setting_Key = 1 Then
                    '        chkcmbroadcast.Checked = True
                    '    Else
                    '        chkcmbroadcast.Checked = False
                    '    End If
                Case "TRADINGVOL_SETTING"
                    If Setting_Key = 1 Then
                        chktradingVol.Checked = True
                    Else
                        chktradingVol.Checked = False
                    End If
                Case "NEW_BHAVCOPY"
                    If Setting_Key = 1 Then
                        chkbhavcopynew.Checked = True
                    Else
                        chkbhavcopynew.Checked = False
                    End If

                Case "BHAVCOPY_FILE_PATH"
                    txtBhavCopyFilePath.Text = Setting_Key

                Case "ANALYSIS FILE PATH"
                    txtAnalysisFilePath.Text = Setting_Key
                Case "ANALYSIS FILE NAME"
                    lblAnalysisFileName.Text = Setting_Key


                Case "CURRECNY CONTRACT PATH"
                    txtcurrencyFilePath.Text = Setting_Key
                Case "CURRECNY CONTRACT NAME"
                    lblcurrencyFileName.Text = Setting_Key
                Case "AUTO CURRENCY CONTRACT REFRESH"
                    ChkAutoCurrency.Checked = Boolean.Parse(Setting_Key)

                Case "MODE"
                    cmbMode.Text = Setting_Key
                Case "ATMDELTA_MIN"
                    TxtFAtm.Text = Setting_Key
                Case "ATMDELTA_MAX"
                    TxtTAtm.Text = Setting_Key
                Case "SELECTION_TYPE"
                    If CType(Setting_Key, Setting_SELECTION_TYPE) = Setting_SELECTION_TYPE.STRIKE Then
                        OptStrikes.Checked = True
                        OptAtm.Checked = False
                    Else
                        OptStrikes.Checked = False
                        OptAtm.Checked = True
                    End If
                Case "VOLATITLIY"
                    TxtVolPer.Text = Setting_Key
                Case "PREDATA_AUTO_OFF"
                    If Val(Setting_Key) = 1 Then
                        ChkPreDataAutoOff.Checked = True
                    Else
                        ChkPreDataAutoOff.Checked = False
                    End If


                Case "SPANTIMERCHECK"
                    If Setting_Key = 1 Then
                        ChkSpanDisable.Checked = True
                    Else
                        ChkSpanDisable.Checked = False

                    End If
                Case "EXPORT_IMPORT_POSITION"
                    If Setting_Key = 1 Then
                        rbcsv.Checked = True
                    ElseIf Setting_Key = 2 Then
                        rbexcel.Checked = True
                    End If
                Case "CAL_USING_EQ_WITHINDEX"
                    If Setting_Key = 1 Then
                        chkcalusingEqWithindex.Checked = True
                    Else
                        chkcalusingEqWithindex.Checked = False
                    End If
                Case "DAYTIME_VOLANDGREEK_CAL"
                    If Setting_Key = 1 Then
                        chkDaytimevolandgreekcal.Checked = True
                    Else
                        chkDaytimevolandgreekcal.Checked = False
                    End If
                Case "INCLUDEEXPIRY_VOLANDGREEK_CAL"
                    If Setting_Key = 1 Then
                        chkIncludeExpiryvolandgreekcal.Checked = True
                    Else
                        chkIncludeExpiryvolandgreekcal.Checked = False
                    End If


                Case "FIXVOL_BACKCOLOR"

                    Dim MyColor As System.Drawing.Color
                    If Setting_Key = "" Then
                        btnfixvol.BackColor = Color.Indigo
                    Else
                        Dim VarColorCode As String() = Setting_Key.Split(","c)

                        MyColor = Color.FromArgb(Int16.Parse(VarColorCode(0)), Int16.Parse(VarColorCode(1)), Int16.Parse(VarColorCode(2)), Int16.Parse(VarColorCode(3)))
                        btnfixvol.BackColor = MyColor
                    End If

                Case "GAMMAMULTI_BACKCOLOR"

                    Dim MyColor As System.Drawing.Color
                    If Setting_Key = "" Then
                        btnmultipliercolor.BackColor = Color.Chocolate
                    Else
                        Dim VarColorCode As String() = Setting_Key.Split(","c)

                        MyColor = Color.FromArgb(Int16.Parse(VarColorCode(0)), Int16.Parse(VarColorCode(1)), Int16.Parse(VarColorCode(2)), Int16.Parse(VarColorCode(3)))
                        btnmultipliercolor.BackColor = MyColor
                    End If
                Case "DB_BACKUP_ON_EXIT"
                    If Setting_Key = 1 Then
                        chkautodbbackup.Checked = True
                    Else
                        chkautodbbackup.Checked = False
                    End If
                Case "OPEN_ANALYSIS"
                    If Setting_Key = 1 Then
                        ChkOPAna.Checked = True
                    Else
                        ChkOPAna.Checked = False
                    End If
                Case "POSITION_BACKUP_ON_EXIT"
                    If Setting_Key = 1 Then
                        chkautopositionbackup.Checked = True
                    Else
                        chkautopositionbackup.Checked = False
                    End If

                Case "FLGCSVCONTRACT"
                    If Setting_Key = 1 Then
                        chkcsvContract.Checked = True
                    Else
                        chkcsvContract.Checked = False
                    End If

                Case "CHANGE_IN_OI"
                    If Setting_Key = 1 Then
                        RBCIODAY.Checked = True
                    ElseIf Setting_Key = 2 Then
                        RBCIOREG.Checked = True
                    End If
                Case "DEFAULT_EXPO_MARGIN"

                    txtexpomargin.Text = Setting_Key
                Case "OPPOS_ENTRYDATE"
                    If Setting_Key = 1 Then
                        chkToday.Checked = True
                    Else
                        chkToday.Checked = False
                    End If
                Case "RECALCULATE_POSITION"
                    If Setting_Key = 1 Then
                        chkrecalculateposition.Checked = True
                    Else
                        chkrecalculateposition.Checked = False
                    End If
                Case "BEP_STRIKE"
                    txtbepstrike.Text = Setting_Key
                Case "BEP_INTERVAL"
                    txtbepinterval.Text = Setting_Key
                Case "MULTIPLIER"
                    txtmultiplier.Text = Setting_Key
                Case "BEP_VISIBLE"
                    If Setting_Key = 1 Then
                        chkbepvisible.Checked = True
                    Else
                        chkbepvisible.Checked = False
                    End If

                Case "BEP_STIKEDIFF"
                    If Setting_Key = 1 Then
                        chkBepstrikediff.Checked = True
                    Else
                        chkBepstrikediff.Checked = False
                    End If

                Case "BEP_GMTM"
                    If Setting_Key = 1 Then
                        chkGMTMBEP.Checked = True
                    Else
                        chkGMTMBEP.Checked = False
                    End If

                Case "BHAVCOPYPROCESSDAY"
                    txtbhavcopyProcessday.Text = Setting_Key
                Case "SET_BHAVCOPY_ON_LTP_STOP"
                    If Setting_Key = 1 Then
                        chkSetBhavLTPOnStop.Checked = True
                    Else
                        chkSetBhavLTPOnStop.Checked = False
                    End If
                    txtbhavcopyProcessday.Text = Setting_Key
                Case "CAL_GREEK_WITH_BCASTDATE"
                    If Setting_Key = 1 Then
                        chkBcastDate.Checked = True
                    Else
                        chkBcastDate.Checked = False
                    End If
                Case "OPEN_EXCEL"
                    If Setting_Key = 1 Then
                        ChkOpenExcel.Checked = True
                    Else
                        ChkOpenExcel.Checked = False
                    End If
                Case "CONTRACT_NOTIFICATION"
                    If Setting_Key = 1 Then
                        CBcontractnotification.Checked = True
                    Else
                        CBcontractnotification.Checked = False
                    End If

                Case "INSTANCE"
                    cboBoxInstance.Text = Setting_Key.ToString()

                Case "SYNTH_ATM_UPDOWNSTRIKE"
                    If Setting_Key = 1 Then
                        RBsynatmstrike.Checked = True
                        RBSynUpdownStrikeAvg.Checked = False
                        chksynfutniftyskip.Checked = False
                    ElseIf Setting_Key = 0 Then
                        RBsynatmstrike.Checked = False
                        RBSynUpdownStrikeAvg.Checked = True
                        chksynfutniftyskip.Checked = False
                    ElseIf Setting_Key = 2 Then
                        RBsynatmstrike.Checked = False
                        RBSynUpdownStrikeAvg.Checked = True
                        chksynfutniftyskip.Checked = True
                    End If

                Case "SAVE_DATA_AUTO"
                    If Setting_Key = 1 Then
                        ChkSaveDataAuto.Checked = True
                    Else
                        ChkSaveDataAuto.Checked = False
                    End If

                Case "SAVE_ANA_DATA_FILE"
                    If Setting_Key = 1 Then
                        ChkSaveAnaDataFile.Checked = True
                    Else
                        ChkSaveAnaDataFile.Checked = False
                    End If
                Case "ISEXCLUDE"
                    If Setting_Key = 1 Then
                        txtIsExclude.Checked = True
                    Else
                        txtIsExclude.Checked = False
                    End If

            End Select
        Next
    End Sub
    Private Sub FillExpenseDataToTextBox()
        txtndbl.Text = ndbl
        txtndblp.Text = ndblp
        txtndbs.Text = ndbs
        txtndbsp.Text = ndbsp

        txtdbl.Text = dbl
        txtdblp.Text = dblp
        txtdbs.Text = dbs
        txtdbsp.Text = dbsp

        txtfutl.Text = futl
        txtfutlp.Text = futlp
        txtfuts.Text = futs
        txtfutsp.Text = futsp

        txtspl.Text = spl
        txtsplp.Text = splp
        txtsps.Text = sps
        txtspsp.Text = spsp

        txtpl.Text = prel
        txtplp.Text = prelp
        txtps.Text = pres
        txtpsp.Text = presp

        'currency
        txtcfutl.Text = currfutl
        txtcfutlp.Text = currfutlp
        txtcfuts.Text = currfuts
        txtcfutsp.Text = currfutsp

        txtcspl.Text = currspl
        txtcsplp.Text = currsplp
        txtcsps.Text = currsps
        txtcspsp.Text = currspsp

        txtcpl.Text = currprel
        txtcplp.Text = currprelp
        txtcps.Text = currpres
        txtcpsp.Text = currpresp

        If Val(txtspl.Text) = 0 Then
            rbtnPremium.Checked = True
        Else
            rbtnStrikePremium.Checked = True
        End If

        If Val(txtcspl.Text) = 0 Then
            rbtncPremium.Checked = True
        Else
            rbtncStrikePremium.Checked = True
        End If
        txtineq.Text = eqInterest
        txtinfo.Text = FoInterest
        txtsttrate.Text = sttRate
    End Sub
    Private Sub FillImportSettingDataToTextBox()
        DGTextFile.Rows.Clear()
        DGSQLServer.Rows.Clear()

        If Not (GdtSettings.Select("SettingName='NETMODE'").Length > 0) Then
            objTrad.SettingName = "NETMODE"
            objTrad.SettingKey = "UDP"
            objTrad.Insert_setting()
            GdtSettings = objTrad.Settings
        End If

        DtSetting = GdtSettings
        With DtSetting
            For Each DR As DataRow In DtSetting.Rows
                Select Case DR("SETTINGNAME").ToString.ToUpper
                    Case "REFRESH_TIME"
                        txtreftime.Text = DR("SETTINGKEY").ToString
                    Case "NSE_CCODE"
                        txtccode.Text = DR("SETTINGKEY").ToString
                    Case "UDP_SELECTED_TOKEN"
                        If DR("SETTINGKEY").ToString = 1 Then
                            chkudpselectedToken.Checked = True
                        Else
                            chkudpselectedToken.Checked = False
                        End If
                    Case "ORIGINALPATH"
                        If DR("SETTINGKEY").ToString = 1 Then
                            chkOriginalFormate.Checked = True
                        Else
                            chkOriginalFormate.Checked = False
                        End If

                End Select
            Next
        End With
        DtImportType = objTrad.Select_Import_type
        CType(DGTextFile.Columns("Text_Type"), DataGridViewComboBoxColumn).Items.Clear()
        CType(DGSQLServer.Columns("Server_Type"), DataGridViewComboBoxColumn).Items.Clear()
        For Each Dr As DataRow In DtImportType.Select("Import_Type='TEXT File'")
            CType(DGTextFile.Columns("Text_Type"), DataGridViewComboBoxColumn).Items.Add(Dr("Text_Type"))
        Next
        For Each Dr As DataRow In DtImportType.Select("Import_Type='Custom TEXT File'")
            CType(DGTextFile.Columns("Text_Type"), DataGridViewComboBoxColumn).Items.Add(Dr("Text_Type"))
        Next
        For Each Dr As DataRow In DtImportType.Select("Import_Type='SQL Server'")
            CType(DGSQLServer.Columns("Server_Type"), DataGridViewComboBoxColumn).Items.Add(Dr("Server_Type"))
        Next
        DtImport_Setting = objTrad.Select_Import_Setting
        For Each Dr As DataRow In DtImport_Setting.Select("Import_Type='TEXT File'")
            Dim RowIdx As Integer = DGTextFile.Rows.Add()
            DGTextFile.Rows(RowIdx).Cells("Text_Type").Value = Dr("Text_Type")
            DGTextFile.Rows(RowIdx).Cells("File_Path").Value = Dr("File_Path")
            DGTextFile.Rows(RowIdx).Cells("FileName_Format").Value = Dr("FileName_Format")
            DGTextFile.Rows(RowIdx).Cells("File_Code").Value = Dr("File_Code")
            DGTextFile.Rows(RowIdx).Cells("Text_Auto_Import").Value = Dr("Auto_Import")
            DGTextFile.Rows(RowIdx).Cells("Text_Manual_Import").Value = Dr("Manual_Import")
        Next
        For Each Dr As DataRow In DtImport_Setting.Select("Import_Type='SQL Server'")
            Dim RowIdx As Integer = DGSQLServer.Rows.Add()
            DGSQLServer.Rows(RowIdx).Cells("Server_Type").Value = Dr("Server_Type")
            DGSQLServer.Rows(RowIdx).Cells("Server_Name").Value = Dr("Server_Name")
            DGSQLServer.Rows(RowIdx).Cells("Database_Name").Value = Dr("Database_Name")
            DGSQLServer.Rows(RowIdx).Cells("User_Name").Value = Dr("User_Name")
            DGSQLServer.Rows(RowIdx).Cells("Pwd").Value = Dr("Pwd")
            DGSQLServer.Rows(RowIdx).Cells("Table_Name").Value = Dr("Table_Name")
            DGSQLServer.Rows(RowIdx).Cells("SQL_Auto_Import").Value = Dr("Auto_Import")
            DGSQLServer.Rows(RowIdx).Cells("SQL_Manual_Import").Value = Dr("Manual_Import")
        Next
        For Each Dr As DataRow In DtImport_Setting.Select("Import_Type='Custom TEXT File'")
            Dim RowIdx As Integer = DGTextFile.Rows.Add()
            DGTextFile.Rows(RowIdx).Cells("Text_Type").Value = Dr("Text_Type")
            DGTextFile.Rows(RowIdx).Cells("File_Path").Value = Dr("File_Path")
            DGTextFile.Rows(RowIdx).Cells("FileName_Format").Value = Dr("FileName_Format")
            DGTextFile.Rows(RowIdx).Cells("File_Code").Value = Dr("File_Code")
            DGTextFile.Rows(RowIdx).Cells("Text_Auto_Import").Value = Dr("Auto_Import")
            DGTextFile.Rows(RowIdx).Cells("Text_Manual_Import").Value = Dr("Manual_Import")
        Next
    End Sub
    Private Sub FillConnectionDataToTextBox()
        Dim setting_name As String
        Dim Setting_Key As String
        For Each DR As DataRow In DtSetting.Rows
            setting_name = DR("SettingName").ToString.ToUpper
            Setting_Key = DR("Settingkey").ToString
            Select Case setting_name
                Case "FO_UDP_IP"
                    Dim ip_fo() As String
                    ip_fo = DR("Settingkey").ToString.Split(".")
                    txtipaddress1.Text = ip_fo(0)
                    txtipaddress2.Text = ip_fo(1)
                    txtipaddress3.Text = ip_fo(2)
                    txtipaddress4.Text = ip_fo(3)
                    stripaddress1 = ip_fo(0)
                    stripaddress2 = ip_fo(1)
                    stripaddress3 = ip_fo(2)
                    stripaddress4 = ip_fo(3)
                Case "FO_UDP_PORT"
                    txtFOudpport.Text = DR("Settingkey")
                    strFOudpport = (DR("Settingkey"))
                Case "CM_UDP_IP"
                    Dim ip_cm() As String
                    ip_cm = DR("Settingkey").ToString.Split(".")
                    txtudpip1.Text = ip_cm(0)
                    txtudpip2.Text = ip_cm(1)
                    txtudpip3.Text = ip_cm(2)
                    txtudpip4.Text = ip_cm(3)
                    strudpip1 = ip_cm(0)
                    strudpip2 = ip_cm(1)
                    strudpip3 = ip_cm(2)
                    strudpip4 = ip_cm(3)
                Case "CM_UDP_PORT"
                    txtCMudpport.Text = DR("Settingkey")
                    strCMudpport = DR("Settingkey")
                Case "CURRENCY_UDP_IP"
                    Dim ip_currency() As String
                    ip_currency = DR("Settingkey").ToString.Split(".")
                    txtcurrencyipaddress1.Text = ip_currency(0)
                    txtcurrencyipaddress2.Text = ip_currency(1)
                    txtcurrencyipaddress3.Text = ip_currency(2)
                    txtcurrencyipaddress4.Text = ip_currency(3)
                    strcurrencyipaddress1 = ip_currency(0)
                    strcurrencyipaddress2 = ip_currency(1)
                    strcurrencyipaddress3 = ip_currency(2)
                    strcurrencyipaddress4 = ip_currency(3)
                Case "CURRENCY_UDP_PORT"
                    txtcurrencyudpport.Text = DR("Settingkey")
                    strcurrencyudpport = DR("Settingkey")
                Case "NETMODE"
                    If DR("Settingkey").ToString() = "TCP" Then
                        'OptTcp.Checked = True
                        OptINet.Checked = True
                    ElseIf DR("Settingkey").ToString() = "UDP" Then
                        OptUdp.Checked = True
                    ElseIf DR("Settingkey").ToString() = "NET" Then
                        OptINet.Checked = True
                    ElseIf DR("Settingkey").ToString() = "API" Then
                        OptAPI.Checked = True

                    ElseIf DR("Settingkey").ToString() = "JL" Then
                        OptJL.Checked = True
                    End If
                Case "NETDATAMODE"
                    If DR("Settingkey").ToString() = "NSE" Then
                        OptNSE.Checked = True
                    ElseIf DR("Settingkey").ToString() = "MCX" Then
                        OptMCXSX.Checked = True
                    End If
                Case "SQLSERVER"
                    txtServerName.Text = DR("Settingkey").ToString
                Case "DATABASE"
                    txtDatabase.Text = DR("Settingkey").ToString
                Case "AUTHANTICATION"
                    If DR("Settingkey").ToString.ToUpper = "WINDOWS" Then
                        rbtnWindows.Checked = True
                    Else
                        rbtnSQLServer.Checked = True
                    End If
                Case "USERNAME"
                    txtUserName.Text = DR("Settingkey").ToString
                Case "PASSWORD"
                    txtPassword.Text = DR("Settingkey").ToString

                Case "INTERNETDATA_LINK"
                    ' If DR("Settingkey").ToString.ToUpper = "LINK1" Then
                    RBLink1.Checked = True
                    'ElseIf DR("Settingkey").ToString.ToUpper = "LINK2" Then
                    'RBLink2.Checked = True
                    'End If

                Case "NEW_CM_BROADCAST_MT"
                    If Setting_Key = 1 Then
                        chkcmbroadcastMT.Checked = True
                    Else
                        chkcmbroadcastMT.Checked = False
                    End If
                Case CSetting.mKeyCompactMdbAuto
                    If Setting_Key = 1 Then
                        chkCompactMdbAuto.Checked = True
                    Else
                        chkCompactMdbAuto.Checked = False
                    End If
                Case CSetting.mKeyCompactMdbDays
                    txtCompactMdbDays.Text = DR("Settingkey").ToString()

            End Select
        Next
    End Sub
    Private Sub FillGreekNeutralsetting()

        For Each DR As DataRow In GdtSettings.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim setting_key As String = DR("Settingkey").ToString
            Select Case DR("SettingName").ToString.ToUpper
                REM Get Setting Of Delta Calc Base On Eq Or Curr
                Case "EQ_DELTA_BASE"
                    If CType(setting_key, Setting_Greeks_BASE) = Setting_Greeks_BASE.Lot Then
                        OptEqLot.Checked = True
                        OptEqUnit.Checked = False
                    Else
                        OptEqLot.Checked = False
                        OptEqUnit.Checked = True
                    End If
                Case "CURR_DELTA_BASE"
                    If CType(setting_key, Setting_Greeks_BASE) = Setting_Greeks_BASE.Lot Then
                        OptCurrLot.Checked = True
                        OptCurrUnit.Checked = False
                    Else
                        OptCurrLot.Checked = False
                        OptCurrUnit.Checked = True
                    End If

                    'Gamma
                    REM Get Setting Of GAMMA Calc Base On Eq Or Curr
                Case "EQ_GAMMA_BASE"
                    If CType(setting_key, Setting_Greeks_BASE) = Setting_Greeks_BASE.Lot Then
                        OptEqLotGa.Checked = True
                        OptEqUnitGa.Checked = False
                    Else
                        OptEqLotGa.Checked = False
                        OptEqUnitGa.Checked = True
                    End If
                Case "CURR_GAMMA_BASE"
                    If CType(setting_key, Setting_Greeks_BASE) = Setting_Greeks_BASE.Lot Then
                        OptCurrLotGa.Checked = True
                        OptCurrUnitGa.Checked = False
                    Else
                        OptCurrLotGa.Checked = False
                        OptCurrUnitGa.Checked = True
                    End If

                    'Vega
                    REM Get Setting Of Vega Calc Base On Eq Or Curr
                Case "EQ_VEGA_BASE"
                    If CType(setting_key, Setting_Greeks_BASE) = Setting_Greeks_BASE.Lot Then
                        OptEqLotVe.Checked = True
                        OptEqUnitVe.Checked = False
                    Else
                        OptEqLotVe.Checked = False
                        OptEqUnitVe.Checked = True
                    End If
                Case "CURR_VEGA_BASE"
                    If CType(setting_key, Setting_Greeks_BASE) = Setting_Greeks_BASE.Lot Then
                        OptCurrLotVe.Checked = True
                        OptCurrUnitVe.Checked = False
                    Else
                        OptCurrLotVe.Checked = False
                        OptCurrUnitVe.Checked = True
                    End If

                    'Theta
                    REM Get Setting Of Theta Calc Base On Eq Or Curr
                Case "EQ_THETA_BASE"
                    If CType(setting_key, Setting_Greeks_BASE) = Setting_Greeks_BASE.Lot Then
                        OptEqLotTh.Checked = True
                        OptEqUnitTh.Checked = False
                    Else
                        OptEqLotTh.Checked = False
                        OptEqUnitTh.Checked = True
                    End If
                Case "CURR_THETA_BASE"
                    If CType(setting_key, Setting_Greeks_BASE) = Setting_Greeks_BASE.Lot Then
                        OptCurrLotTh.Checked = True
                        OptCurrUnitTh.Checked = False
                    Else
                        OptCurrLotTh.Checked = False
                        OptCurrUnitTh.Checked = True
                    End If

                    'Volga
                    REM Get Setting Of Volga Calc Base On Eq Or Curr
                Case "EQ_VOLGA_BASE"
                    If CType(setting_key, Setting_Greeks_BASE) = Setting_Greeks_BASE.Lot Then
                        OptEqLotVo.Checked = True
                        OptEqUnitVo.Checked = False
                    Else
                        OptEqLotVo.Checked = False
                        OptEqUnitVo.Checked = True
                    End If
                Case "CURR_VOLGA_BASE"
                    If CType(setting_key, Setting_Greeks_BASE) = Setting_Greeks_BASE.Lot Then
                        OptCurrLotVo.Checked = True
                        OptCurrUnitVo.Checked = False
                    Else
                        OptCurrLotVo.Checked = False
                        OptCurrUnitVo.Checked = True
                    End If

                    'Vanna
                    REM Get Setting Of Delta Calc Base On Eq Or Curr
                Case "EQ_VANNA_BASE"
                    If CType(setting_key, Setting_Greeks_BASE) = Setting_Greeks_BASE.Lot Then
                        OptEqLotVa.Checked = True
                        OptEqUnitVa.Checked = False
                    Else
                        OptEqLotVa.Checked = False
                        OptEqUnitVa.Checked = True
                    End If
                Case "CURR_VANNA_BASE"
                    If CType(setting_key, Setting_Greeks_BASE) = Setting_Greeks_BASE.Lot Then
                        OptCurrLotVa.Checked = True
                        OptCurrUnitVa.Checked = False
                    Else
                        OptCurrLotVa.Checked = False
                        OptCurrUnitVa.Checked = True
                    End If
                Case "GREEK_NEUTRAL"
                    If setting_key = 1 Then
                        chkGREEK_NEUTRAL.Checked = True
                    Else
                        chkGREEK_NEUTRAL.Checked = False
                    End If
            End Select

        Next


    End Sub
    Private Sub FillRoundingDataToTextBox()
        For Each DR As DataRow In DtSetting.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim Setting_Key As String = DR("Settingkey").ToString
            Select Case DR("SettingName").ToString.ToUpper
                Case "ROUNDGAMMA"
                    cmbgamma.SelectedItem = Setting_Key
                Case "ROUNDVEGA"
                    cmbvega.SelectedItem = Setting_Key
                Case "ROUNDTHETA"
                    cmbtheta.SelectedItem = Setting_Key
                Case "ROUNDDELTA"
                    cmbdelta.SelectedItem = Setting_Key
                Case "ROUNDVOLGA"
                    CmbVolga.SelectedItem = Setting_Key
                Case "ROUNDVANNA"
                    CmbVanna.SelectedItem = Setting_Key
                Case "ROUNDGAMMA_VAL"
                    cmbgammaval.SelectedItem = Setting_Key
                Case "ROUNDVEGA_VAL"
                    cmbvegaval.SelectedItem = Setting_Key
                Case "ROUNDTHETA_VAL"
                    cmbthetaval.SelectedItem = Setting_Key
                Case "ROUNDDELTA_VAL"
                    cmbdeltaval.SelectedItem = Setting_Key
                Case "ROUNDVOLGA_VAL"
                    CmbVolgaVal.SelectedItem = Setting_Key
                Case "ROUNDVANNA_VAL"
                    CmbVannaVal.SelectedItem = Setting_Key
                Case "ROUNDGROSSMTM"
                    cmbgrossmtm.SelectedItem = Setting_Key
                Case "ROUNDEXPENSE"
                    cmbexp.SelectedItem = Setting_Key
                Case "ROUNDNETMTM"
                    cmbnetmtm.SelectedItem = Setting_Key
                Case "ROUNDSQUAREEXP"
                    cmbSqOffExp.SelectedItem = Setting_Key
                Case "ROUNDSQUAREMTM"
                    cmbsquaremtm.SelectedItem = Setting_Key
                Case "ROUNDINMARG"
                    cmbinmarg.SelectedItem = Setting_Key
                Case "ROUNDEXMARG"
                    cmbexmarg.SelectedItem = Setting_Key
                Case "ROUNDREALMARG"
                    cmbreal.SelectedItem = Setting_Key
                Case "ROUNDUNMARG"
                    cmbunreal.SelectedItem = Setting_Key
                Case "ROUNDREALTOT"
                    cmbtot.SelectedItem = Setting_Key
                Case "ROUNDEQUITY"
                    cmbequity.SelectedItem = Setting_Key

                Case "ROUNDCURRENCYNETPRICE"
                    cmbcurrnetprice.SelectedItem = Setting_Key
                Case "ROUNDCURRENCYLTP"
                    cmbcurrltp.SelectedItem = Setting_Key
                Case "ROUNDNEUTRALIZE"
                    cmbNeutralize.SelectedItem = Setting_Key

                    ''''''''''''''''''Devang Start'''''''''''''''''''
                Case "ROUNDOPENINT"
                    cboOpenInt.SelectedItem = Setting_Key
                Case "ROUNDCALENDER"
                    cboCalendar.SelectedItem = Setting_Key
                Case "ROUNDBF"
                    cboButterfly.SelectedItem = Setting_Key
                Case "ROUNDVOL"
                    cboVolatility.SelectedItem = Setting_Key
                Case "ROUNDBF2"
                    cboButterfly2.SelectedItem = Setting_Key
                Case "ROUNDPCP"
                    cboPCP.SelectedItem = Setting_Key
                Case "ROUNDRATIO"
                    cboRatio.SelectedItem = Setting_Key
                Case "ROUNDSTRADDLE"
                    cboStraddle.SelectedItem = Setting_Key
                Case "ROUNDVOLUME"
                    cboVolume.SelectedItem = Setting_Key
                Case "ROUNDLTP"
                    cboLTP.SelectedItem = Setting_Key
                Case "ROUNDBULLSPREAD"
                    cboBullSpread.SelectedItem = Setting_Key
                Case "ROUNDBEARSPREAD"
                    cboBearSpread.SelectedItem = Setting_Key
                Case "ROUNDC2C"
                    cboC2C.SelectedItem = Setting_Key
                Case "ROUNDC2P"
                    cboC2P.SelectedItem = Setting_Key
                Case "ROUNDP2P"
                    cboP2P.SelectedItem = Setting_Key

                    ''''''''''''''''''Devang End'''''''''''''''''''
            End Select
        Next
    End Sub
    Private Sub FillMarginDataToTextBox()
        For Each DR As DataRow In DtSetting.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim Setting_Key As String = DR("Settingkey").ToString
            Select Case setting_name
                Case "SPAN_PATH"
                    txtspanpath.Text = Setting_Key
                Case "RATEOFINTEREST"
                    txtrateofint.Text = Format(Val(Setting_Key), "#0.00")

                Case "CURRENCY SPAN PATH"
                    txtcurrencyspanpath.Text = Setting_Key
                Case "CURRENCY RATE OF INTEREST"
                    txtcurrencyrateofint.Text = Format(Val(Setting_Key), "#0.00")
                Case "CALMARGINSPAN"
                    If Setting_Key = 1 Then
                        rbInternetmargin.Checked = False

                        rbspan.Checked = True
                    Else
                        rbInternetmargin.Checked = True

                        rbspan.Checked = False
                    End If
                    'Case "CALMARGINWITHOLDMETHOD"
                    '    If Setting_Key = 1 Then
                    '        Cbmarginmethod.Checked = True

                    '    Else
                    '        Cbmarginmethod.Checked = False
                    '    End If
                Case "CALMARGINWITH_AEL_EXPO"
                    If Setting_Key = 1 Then
                        chkaeladdexpo.Checked = True

                    Else
                        chkaeladdexpo.Checked = False
                    End If
                Case "CURRENCY_MARGIN_QTY"
                    If Setting_Key = 1 Then
                        rbqty.Checked = True

                        rblots.Checked = False
                    Else
                        rbqty.Checked = False

                        rblots.Checked = True
                    End If

                Case "AELOPTION"
                    If Setting_Key = 1 Then
                        rbAEL.Checked = True
                        rbNewAEL.Checked = False
                        rbAELContracts.Checked = False
                    ElseIf Setting_Key = 2 Then
                        rbAEL.Checked = False
                        rbNewAEL.Checked = True
                        rbAELContracts.Checked = False
                    ElseIf Setting_Key = 2 Then
                        rbAEL.Checked = False
                        rbNewAEL.Checked = False
                        rbAELContracts.Checked = True
                    End If
                Case "INDEX_FAR_MONTH_OPTION"
                    txtIndexFarMonth.Text = Format(Val(Setting_Key), "#0.00")
                Case "INDEX_OTM_OPTION"
                    txtIndexOTM.Text = Format(Val(Setting_Key), "#0.00")
                Case "INDEX_OTH_OPTION"
                    txtIndexOTH.Text = Format(Val(Setting_Key), "#0.00")
                    'txtcurrencyrateofint.Text = Format(Val(Setting_Key), "#0.00")
            End Select
        Next
    End Sub
#End Region
#Region "saving tabs"

    Private Sub SaveGeneral()
        For Each DR As DataRow In GdtSettings.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim setting_key As String = "Nothing"
            Select Case DR("SettingName").ToString.ToUpper
                Case "BACKUP_PATH"
                    setting_key = txtbackuppath.Text
                Case "SPAN_TIMER"
                    setting_key = TextBox1.Text
                Case "ZERO_QTY_ANALYSIS"
                    If cmbzero.SelectedIndex = 0 Then
                        setting_key = "0"
                    Else
                        setting_key = "1"
                    End If
                Case "TIMER_CALCULATION_INTERVAL"
                    setting_key = txtbtimer.Text
                Case "CDELAY"
                    setting_key = CInt(Val(TxtCDelay.Text))
                Case "NDELAY"
                    setting_key = CInt(Val(TxtNDelay.Text))
                Case "FDELAY"
                    setting_key = CInt(Val(TxtFDelay.Text))

                Case "CDATE"
                    setting_key = DtCDate.Value.ToString("dd/MMM/yyyy")
                Case "NDATE"
                    setting_key = DtNDate.Value.ToString("dd/MMM/yyyy")
                Case "FDATE"
                    setting_key = DtFDate.Value.ToString("dd/MMM/yyyy")

                Case "MATURITY_FAR_MONTH"
                    setting_key = cmbFarMonth.Text
                Case "NEUTRALIZE_MONTH"
                    setting_key = cbnutltpmon.Text

                Case "AUTO CONTRACT REFRESH"
                    setting_key = ChkAutoContract.Checked.ToString
                Case "CONTRACT FILE PATH"
                    setting_key = txtContractFilePath.Text
                Case "CONTRACT FILE NAME"
                    setting_key = lblContractFileName.Text
                Case "AUTO SECURITY REFRESH"
                    setting_key = ChkAutoContract.Checked.ToString
                Case "SECURITY FILE PATH"
                    setting_key = txtSecurityFilePath.Text
                Case "SECURITY FILE NAME"
                    setting_key = lblSecurityFileName.Text

                Case "AUTO_BHAVCOPY_REFRESH"
                    setting_key = ChkAutoBhavCopy.Checked.ToString
                Case "WAVV_SETTING"
                    '  setting_key = CHKwavv.Checked.ToString
                    If CHKwavv.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                    'Case "NEW_CM_BROADCAST"
                    '    '  setting_key = CHKwavv.Checked.ToString
                    '    If chkcmbroadcast.Checked = True Then
                    '        setting_key = 1
                    '    Else
                    '        setting_key = 0
                    '    End If
                Case "CAL_SYN_ON_EXPIRY"
                    '  setting_key = CHKwavv.Checked.ToString
                    If chksynonexpiry.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "TRADINGVOL_SETTING"
                    '  setting_key = CHKwavv.Checked.ToString
                    If chktradingVol.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "NEW_BHAVCOPY"
                    '  setting_key = CHKwavv.Checked.ToString
                    If chkbhavcopynew.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "BHAVCOPY_FILE_PATH"
                    setting_key = txtBhavCopyFilePath.Text

                Case "ANALYSIS FILE PATH"
                    setting_key = txtAnalysisFilePath.Text
                Case "ANALYSIS FILE NAME"
                    setting_key = lblAnalysisFileName.Text

                Case "CURRECNY CONTRACT PATH"
                    setting_key = txtcurrencyFilePath.Text
                Case "CURRECNY CONTRACT NAME"
                    setting_key = lblcurrencyFileName.Text
                Case "AUTO CURRENCY CONTRACT REFRESH"
                    setting_key = ChkAutoCurrency.Checked.ToString

                Case "MODE"
                    setting_key = cmbMode.Text
                Case "ATMDELTA_MIN"
                    setting_key = TxtFAtm.Text
                Case "ATMDELTA_MAX"
                    setting_key = TxtTAtm.Text
                Case "SELECTION_TYPE"
                    setting_key = IIf(OptStrikes.Checked = True, Setting_SELECTION_TYPE.STRIKE, Setting_SELECTION_TYPE.ITM_ATM_OTM)
                Case "VOLATITLIY"
                    setting_key = TxtVolPer.Text
                Case "PREDATA_AUTO_OFF"
                    setting_key = IIf((ChkPreDataAutoOff.Checked = True), 1, 0)

                Case "SPANTIMERCHECK"
                    setting_key = IIf((ChkSpanDisable.Checked = True), 1, 0)
                Case "EXPORT_IMPORT_POSITION"
                    If rbcsv.Checked = True Then
                        setting_key = IIf((rbcsv.Checked = True), 1, 0)
                    ElseIf rbexcel.Checked = True Then
                        setting_key = IIf((rbexcel.Checked = True), 2, 0)
                    End If
                Case "CAL_USING_EQ_WITHINDEX"
                    setting_key = IIf((chkcalusingEqWithindex.Checked = True), 1, 0)
                Case "DAYTIME_VOLANDGREEK_CAL"
                    setting_key = IIf((chkDaytimevolandgreekcal.Checked = True), 1, 0)
                Case "INCLUDEEXPIRY_VOLANDGREEK_CAL"
                    setting_key = IIf((chkIncludeExpiryvolandgreekcal.Checked = True), 1, 0)

                Case "FIXVOL_BACKCOLOR"

                    setting_key = btnfixvol.BackColor.A.ToString() + "," + btnfixvol.BackColor.R.ToString() + "," + btnfixvol.BackColor.G.ToString() + "," + btnfixvol.BackColor.B.ToString()

                Case "GAMMAMULTI_BACKCOLOR"

                    setting_key = btnmultipliercolor.BackColor.A.ToString() + "," + btnmultipliercolor.BackColor.R.ToString() + "," + btnmultipliercolor.BackColor.G.ToString() + "," + btnmultipliercolor.BackColor.B.ToString()

                Case "DB_BACKUP_ON_EXIT"
                    If chkautodbbackup.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "OPEN_ANALYSIS"
                    If ChkOPAna.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If

                Case "POSITION_BACKUP_ON_EXIT"
                    If chkautopositionbackup.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "FLGCSVCONTRACT"
                    If chkcsvContract.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If

                Case "DEFAULT_EXPO_MARGIN"

                    setting_key = txtexpomargin.Text
                Case "CHANGE_IN_OI"
                    If RBCIODAY.Checked = True Then
                        setting_key = IIf((RBCIODAY.Checked = True), 1, 0)
                    ElseIf RBCIOREG.Checked = True Then
                        setting_key = IIf((RBCIOREG.Checked = True), 2, 0)
                    End If
                Case "OPPOS_ENTRYDATE"
                    If chkToday.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "RECALCULATE_POSITION"
                    If chkrecalculateposition.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "BEP_STRIKE"
                    setting_key = txtbepstrike.Text
                Case "BEP_INTERVAL"
                    setting_key = txtbepinterval.Text
                Case "MULTIPLIER"
                    setting_key = txtmultiplier.Text

                Case "BEP_VISIBLE"
                    If chkbepvisible.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "BEP_STIKEDIFF"
                    If chkBepstrikediff.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If

                Case "BEP_GMTM"
                    If chkGMTMBEP.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If

                Case "BHAVCOPYPROCESSDAY"

                    setting_key = txtbhavcopyProcessday.Text
                Case "SET_BHAVCOPY_ON_LTP_STOP"
                    If chkSetBhavLTPOnStop.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "CAL_GREEK_WITH_BCASTDATE"
                    If chkBcastDate.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "OPEN_EXCEL"
                    If ChkOpenExcel.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "CONTRACT_NOTIFICATION"
                    If CBcontractnotification.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "INSTANCE"
                    setting_key = cboBoxInstance.Text

                Case "SYNTH_ATM_UPDOWNSTRIKE"
                    If RBsynatmstrike.Checked = True Then
                        setting_key = 1
                    ElseIf RBSynUpdownStrikeAvg.Checked = True Then
                        setting_key = 0
                    End If

                    If RBSynUpdownStrikeAvg.Checked = True And chksynfutniftyskip.Checked = True Then
                        setting_key = 2
                    End If
                Case "SAVE_DATA_AUTO"
                    If ChkSaveDataAuto.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If

                Case "SAVE_ANA_DATA_FILE"
                    If ChkSaveAnaDataFile.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case "ISEXCLUDE"
                    If txtIsExclude.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
            End Select
            If setting_key <> "Nothing" Then
                objTrad.SettingName = setting_name
                objTrad.SettingKey = setting_key
                objTrad.Uid = CInt(DR("uid"))
                objTrad.Update_setting()
            End If
        Next
        MsgBox("Settings Applied Successfully.", MsgBoxStyle.Information)
        Call Rounddata()
    End Sub
    Private Sub SaveExpense()
        objExp.NDBL = Val(txtndbl.Text)
        objExp.NDBLP = IIf(Val(txtndblp.Text) = 0, 1, Val(txtndblp.Text))
        objExp.NDBS = Val(txtndbs.Text)
        objExp.NDBSP = IIf(Val(txtndbsp.Text) = 0, 1, Val(txtndbsp.Text))

        objExp.DBL = Val(txtdbl.Text)
        objExp.DBLP = IIf(Val(txtdblp.Text) = 0, 1, Val(txtdblp.Text))
        objExp.DBS = Val(txtdbs.Text)
        objExp.DBSP = IIf(Val(txtdbsp.Text) = 0, 1, Val(txtdbsp.Text))

        objExp.FUTL = Val(txtfutl.Text)
        objExp.FUTLP = IIf(Val(txtfutlp.Text) = 0, 1, Val(txtfutlp.Text))
        objExp.FUTS = Val(txtfuts.Text)
        objExp.FUTSP = IIf(Val(txtfutsp.Text) = 0, 1, Val(txtfutsp.Text))

        objExp.SPL = Val(txtspl.Text)
        objExp.SPLP = IIf(Val(txtsplp.Text) = 0, 1, Val(txtsplp.Text))
        objExp.SPS = Val(txtsps.Text)
        objExp.SPSP = IIf(Val(txtspsp.Text) = 0, 1, Val(txtspsp.Text))

        objExp.PL = Val(txtpl.Text)
        objExp.PLP = Val(txtplp.Text)
        objExp.PS = Val(txtps.Text)
        objExp.PSP = Val(txtpsp.Text)

        'currency
        objExp.CURRFUTL = Val(txtcfutl.Text)
        objExp.CURRFUTLP = IIf(Val(txtcfutlp.Text) = 0, 1, Val(txtcfutlp.Text))
        objExp.CURRFUTS = Val(txtcfuts.Text)
        objExp.CURRFUTSP = IIf(Val(txtcfutsp.Text) = 0, 1, Val(txtcfutsp.Text))

        objExp.CURRSPL = Val(txtcspl.Text)
        objExp.CURRSPLP = IIf(Val(txtcsplp.Text) = 0, 1, Val(txtcsplp.Text))
        objExp.CURRSPS = Val(txtcsps.Text)
        objExp.CURRSPSP = IIf(Val(txtcspsp.Text) = 0, 1, Val(txtcspsp.Text))

        objExp.CURRPL = Val(txtcpl.Text)
        objExp.CURRPLP = IIf(Val(txtcplp.Text) = 0, 1, Val(txtcplp.Text))
        objExp.CURRPS = Val(txtcps.Text)
        objExp.CURRPSP = IIf(Val(txtcpsp.Text) = 0, 1, Val(txtcpsp.Text))

        objExp.EQUITY = Val(txtineq.Text)
        objExp.FO = Val(txtinfo.Text)

        objExp.STTRATE = Val(txtsttrate.Text)

        objExp.update()

        MsgBox("Data Updated Successfully.", MsgBoxStyle.Information)
        fill_expense()
    End Sub
    Private Sub SaveImportFileData()
        If txtreftime.Text.Trim = "" Then
            txtreftime.Text = 30
        End If
        If txtreftime.Text <= 0 Then
            MsgBox("You cannot enter refresh timer value less then zero or zero.")
            txtreftime.Text = 30
            txtreftime.Focus()
            Exit Sub
        End If
        For Each DR As DataRow In DtSetting.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim setting_key As String = "Nothing"
            Select Case DR("SettingName").ToString.ToUpper
                Case "REFRESH_TIME"
                    setting_key = txtreftime.Text
                Case "NSE_CCODE"
                    Try

                        objTrad.SettingName = "NSE_CCODE"
                        objTrad.SettingKey = ""
                        objTrad.Uid = CInt(DR("uid"))
                        objTrad.Update_setting()
                        setting_key = txtccode.Text
                    Catch ex As Exception

                    End Try
                Case "UDP_SELECTED_TOKEN"
                    Try

                        objTrad.SettingName = "UDP_SELECTED_TOKEN"
                        objTrad.SettingKey = ""
                        objTrad.Uid = CInt(DR("uid"))
                        objTrad.Update_setting()
                        If chkudpselectedToken.Checked = True Then
                            setting_key = 1
                        Else
                            setting_key = 0
                        End If
                        ' setting_key = chkudpselectedToken.Checked.ToString
                    Catch ex As Exception

                    End Try
                Case CSetting.mKeyCompactMdbAuto
                    If chkCompactMdbAuto.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                Case CSetting.mKeyCompactMdbDays
                    setting_key = txtCompactMdbDays.Text

                Case "ORIGINALPATH"
                    objTrad.SettingName = "originalpath"
                    If chkOriginalFormate.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
                    objTrad.Uid = CInt(DR("uid"))
                    objTrad.Update_setting()

            End Select
            If setting_key <> "Nothing" Then
                objTrad.SettingName = setting_name
                objTrad.SettingKey = setting_key
                objTrad.Uid = CInt(DR("uid"))
                objTrad.Update_setting()
            End If


        Next
        Call Rounddata()
        objTrad.Delete_Import_Setting()
        Dim VarImport_Type, VarText_Type, VarServer_Type, VarServer_Name, VarDatabase_Name, VarUser_Name, VarPwd, VarTable_Name, VarFile_Path, VarFileName_Format, VarFile_Code As String
        Dim VarManual_Import, VarAuto_Import As Boolean
        For Each DGRow As DataGridViewRow In DGTextFile.Rows

            For Each Dr As DataRow In DtImportType.Select("Text_type='" + DGRow.Cells("Text_Type").Value + "'")
                VarImport_Type = Dr("Import_type")
                If VarImport_Type = "Custom TEXT File" Then
                    CustomFlag = True
                Else
                    CustomFlag = False
                End If

            Next


            If CustomFlag = True Then
                VarImport_Type = "Custom TEXT File"
                CustomFlag = False
            ElseIf (DGRow.Cells("Text_Type").Value) = Nothing Then
                Exit For
            Else
                VarImport_Type = "TEXT File"
            End If

            'VarImport_Type = "TEXT File"
            VarText_Type = IIf(IsDBNull(DGRow.Cells("Text_Type").Value) = True, "", DGRow.Cells("Text_Type").Value)
            VarServer_Type = ""
            VarServer_Name = ""
            VarDatabase_Name = ""
            VarUser_Name = ""
            VarPwd = ""
            VarTable_Name = ""
            If DGRow.Cells("File_Path").Value Is Nothing Then
                VarFile_Path = ""
            Else
                VarFile_Path = DGRow.Cells("File_Path").Value
            End If
            If DGRow.Cells("File_Code").Value Is Nothing Then
                VarFile_Code = ""
            Else
                VarFile_Code = DGRow.Cells("File_Code").Value
            End If
            'VarFile_Path = IIf(IsDBNull(DGRow.Cells("File_Path").Value) = True, "", DGRow.Cells("File_Path").Value)
            VarFileName_Format = IIf(IsDBNull(DGRow.Cells("FileName_Format").Value) = True, "", DGRow.Cells("FileName_Format").Value)
            'VarFile_Code = IIf(IsDBNull(DGRow.Cells("File_Code").Value) = True, "", DGRow.Cells("File_Code").Value)
            VarManual_Import = DGRow.Cells("Text_Manual_Import").Value
            VarAuto_Import = DGRow.Cells("Text_Auto_Import").Value
            If Not VarText_Type Is Nothing Then
                objTrad.Insert_Import_Setting(VarImport_Type, VarText_Type, VarServer_Type, VarServer_Name, VarDatabase_Name, VarUser_Name, VarPwd, VarTable_Name, VarFile_Path, VarFileName_Format, VarFile_Code, VarAuto_Import, VarManual_Import)
            End If
        Next
        For Each DGRow As DataGridViewRow In DGSQLServer.Rows
            VarImport_Type = "SQL Server"
            VarText_Type = ""
            VarServer_Type = IIf(IsDBNull(DGRow.Cells("Server_Type").Value) = True, "", DGRow.Cells("Server_Type").Value)
            VarServer_Name = IIf(IsDBNull(DGRow.Cells("Server_Name").Value) = True, "", DGRow.Cells("Server_Name").Value)
            VarDatabase_Name = IIf(IsDBNull(DGRow.Cells("Database_Name").Value) = True, "", DGRow.Cells("Database_Name").Value)
            VarUser_Name = IIf(IsDBNull(DGRow.Cells("User_Name").Value) = True, "", DGRow.Cells("User_Name").Value)
            VarPwd = IIf(IsDBNull(DGRow.Cells("Pwd").Value) = True, "", DGRow.Cells("Pwd").Value)
            VarTable_Name = IIf(IsDBNull(DGRow.Cells("Table_Name").Value) = True, "", DGRow.Cells("Table_Name").Value)
            VarFile_Path = ""
            VarFileName_Format = ""
            VarFile_Code = ""
            VarManual_Import = DGRow.Cells("SQL_Manual_Import").Value
            VarAuto_Import = DGRow.Cells("SQL_Auto_Import").Value
            If Not VarServer_Type Is Nothing Then
                objTrad.Insert_Import_Setting(VarImport_Type, VarText_Type, VarServer_Type, VarServer_Name, VarDatabase_Name, VarUser_Name, VarPwd, VarTable_Name, VarFile_Path, VarFileName_Format, VarFile_Code, VarAuto_Import, VarManual_Import)
            End If
        Next
        MsgBox("Settings Applied Successfully.", MsgBoxStyle.Information)
        'Try
        '    If txtreftime.Text.Trim = "" Then
        '        txtreftime.Text = 30
        '    End If
        '    If txtreftime.Text <= 0 Then
        '        MsgBox("You cannot enter refresh timer value less then zero or zero")
        '        txtreftime.Text = 30
        '        txtreftime.Focus()
        '        Exit Sub
        '    End If
        '    For Each DR As DataRow In DtSetting.Rows
        '        Dim setting_name As String = DR("SettingName").ToString.ToUpper
        '        Dim setting_key As String = "Nothing"
        '        Select Case DR("SettingName").ToString.ToUpper
        '            Case "NEAT FO FILE PATH"
        '                setting_key = txtNeatFoFilePath.Text
        '            Case "NEAT FO FILE CODE"
        '                setting_key = txtNeatFoFileCode.Text
        '            Case "NEAT FO FILE FORMAT"
        '                setting_key = lblNeatFileFormat.Text

        '            Case "GETS FO FILE PATH"
        '                setting_key = txtGetsFoFilePath.Text
        '            Case "GETS FO FILE FORMAT"
        '                setting_key = lblGetsFOFileFormat.Text
        '            Case "GETS EQ FILE PATH"
        '                setting_key = txtGetsEqFilePath.Text
        '            Case "GETS EQ FILE FORMAT"
        '                setting_key = lblGetsEQFileFormat.Text

        '            Case "ODIN FO FILE PATH"
        '                setting_key = txtodinFoFilePath.Text
        '            Case "ODIN FO FILE FORMAT"
        '                setting_key = lblOdinFOFileFormat.Text
        '            Case "ODIN EQ FILE PATH"
        '                setting_key = txtOdinEqFilePath.Text
        '            Case "ODIN EQ FILE FORMAT"
        '                setting_key = lblOdinEQFileFormat.Text

        '            Case "NOW FO FILE PATH"
        '                setting_key = txtNowFOFilePath.Text
        '            Case "NOW FO FILE FORMAT"
        '                setting_key = lblNowFOFileFormat.Text
        '            Case "NOW EQ FILE PATH"
        '                setting_key = txtNowEQFilePath.Text
        '            Case "NOW EQ FILE FORMAT"
        '                setting_key = lblNowEQFileFormat.Text


        '            Case "NOTIS FO FILE PATH"
        '                setting_key = txtNotisFoFilePath.Text
        '            Case "NOTIS FO FILE FORMAT"
        '                setting_key = lblNotisFOFormat.Text
        '            Case "NOTIS EQ FILE PATH"
        '                setting_key = txtNotisEQFilePath.Text
        '            Case "NOTIS EQ FILE FORMAT"
        '                setting_key = lblNotisEQFormat.Text

        '            Case "GETS SERVER NAME"
        '                setting_key = txtGETSServerName.Text
        '            Case "GETS DATABASE NAME"
        '                setting_key = txtGETSDatabase.Text
        '            Case "GETS USER NAME"
        '                setting_key = txtGETSUserName.Text
        '            Case "GETS PASSWORD"
        '                setting_key = txtGETSPassword.Text
        '            Case "ODIN SERVER NAME"
        '                setting_key = txtODINServerName.Text
        '            Case "ODIN DATABASE NAME"
        '                setting_key = txtODINDatabase.Text
        '            Case "ODIN USER NAME"
        '                setting_key = txtODINUserName.Text
        '            Case "ODIN PASSWORD"
        '                setting_key = txtODINPassword.Text

        '                ''Check Box Setting Apply
        '            Case "AUTO NEAT FO"
        '                setting_key = ChkNeatFO.Checked
        '            Case "AUTO GETS FO"
        '                setting_key = ChkGetsFO.Checked
        '            Case "AUTO GETS EQ"
        '                setting_key = ChkGetsEQ.Checked
        '            Case "AUTO ODIN FO"
        '                setting_key = ChkOdinFO.Checked
        '            Case "AUTO ODIN EQ"
        '                setting_key = ChkOdinEQ.Checked
        '            Case "AUTO NOW FO"
        '                setting_key = ChkNowFO.Checked
        '            Case "AUTO NOW EQ"
        '                setting_key = ChkNowEQ.Checked
        '            Case "AUTO GETS DATABASE"
        '                setting_key = ChkGetsDatabase.Checked
        '            Case "AUTO ODIN DATABASE"
        '                setting_key = ChkODINDatabase.Checked
        '            Case "NSE_CCODE"
        '                setting_key = txtccode.Text
        '            Case "AUTO NOTIS FO"
        '                setting_key = chkNotisFo.Checked
        '            Case "AUTO NOTIS EQ"
        '                setting_key = chkNotisEq.Checked

        '                'MENUAL CHECK BOX SETTING APPLY
        '            Case "AUTO NEAT FOM"
        '                setting_key = ChkNeatFOm.Checked
        '            Case "AUTO GETS FOM"
        '                setting_key = ChkGetsFOm.Checked
        '            Case "AUTO GETS EQM"
        '                setting_key = ChkGetsEQm.Checked
        '            Case "AUTO ODIN FOM"
        '                setting_key = ChkOdinFOm.Checked
        '            Case "AUTO ODIN EQM"
        '                setting_key = ChkOdinEQm.Checked
        '            Case "AUTO NOW FOM"
        '                setting_key = ChkNowFOm.Checked
        '            Case "AUTO NOW EQM"
        '                setting_key = ChkNowEQm.Checked
        '            Case "AUTO GETS DATABASEM"
        '                setting_key = ChkGetsDatabasem.Checked
        '            Case "AUTO ODIN DATABASEM"
        '                setting_key = ChkODINDatabasem.Checked
        '            Case "AUTO NOTIS FOM"
        '                setting_key = chkNotisFom.Checked
        '            Case "AUTO NOTIS EQM"
        '                setting_key = chkNotisEqm.Checked
        '            Case "REFRESH_TIME"
        '                setting_key = txtreftime.Text

        '            Case "NSE_FILE_NAME_FO"
        '                setting_key = lblNseFOFormat.Text
        '            Case "NSE_FILE_NAME_EQ"
        '                setting_key = lblNseEQFormat.Text
        '            Case "NSE_FILE_PATH_FO"
        '                setting_key = txtNSEFoFilePath.Text
        '            Case "NSE_FILE_PATH_EQ"
        '                setting_key = txtNSEEQFilePath.Text
        '            Case "AUTO NSE FO"
        '                setting_key = chkNSEFo.Checked
        '            Case "AUTO NSE EQ"
        '                setting_key = chkNSEFo.Checked
        '            Case "AUTO NSE FOM"
        '                setting_key = chkNSEFoM.Checked
        '            Case "AUTO NSE EQM"
        '                setting_key = chkNSEEqM.Checked

        '        End Select
        '        If setting_key <> "Nothing" Then
        '            objTrad.SettingName = setting_name
        '            objTrad.SettingKey = setting_key
        '            objTrad.Uid = CInt(DR("uid"))
        '            objTrad.Update_setting()
        '        End If
        '    Next
        '    MsgBox("Setting Apply Successfully ", MsgBoxStyle.Information)
        '    Call Rounddata()
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try

    End Sub
    Private Sub SaveConnectionlink()
        For Each DR As DataRow In DtSetting.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim setting_key As String = "Nothing"
            Select Case DR("SettingName").ToString.ToUpper

                Case "INTERNETDATA_LINK"
                    '  If RBLink1.Checked = True Then
                    setting_key = "LINK1"
                    '  ElseIf RBLink2.Checked = True Then
                    ' setting_key = "LINK2"
                    '  End If
            End Select
            If setting_key <> "Nothing" Then
                objTrad.SettingName = setting_name
                objTrad.SettingKey = setting_key
                objTrad.Uid = CInt(DR("uid"))
                objTrad.Update_setting()
            End If
        Next

        Call Rounddata()
    End Sub
    Private Sub SaveConnection()
        For Each DR As DataRow In DtSetting.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim setting_key As String = "Nothing"
            Select Case DR("SettingName").ToString.ToUpper
                Case "FO_UDP_IP"
                    setting_key = CInt(txtipaddress1.Text) & "." & CInt(txtipaddress2.Text) & "." & CInt(txtipaddress3.Text) & "." & CInt(txtipaddress4.Text)
                Case "FO_UDP_PORT"
                    setting_key = txtFOudpport.Text
                Case "CM_UDP_IP"
                    setting_key = CInt(txtudpip1.Text) & "." & CInt(txtudpip2.Text) & "." & CInt(txtudpip3.Text) & "." & CInt(txtudpip4.Text)
                Case "CM_UDP_PORT"
                    setting_key = txtCMudpport.Text

                Case "CURRENCY_UDP_IP"
                    setting_key = CInt(txtcurrencyipaddress1.Text) & "." & CInt(txtcurrencyipaddress2.Text) & "." & CInt(txtcurrencyipaddress3.Text) & "." & CInt(txtcurrencyipaddress4.Text)
                Case "CURRENCY_UDP_PORT"
                    setting_key = txtcurrencyudpport.Text
                Case "NETMODE"
                    If OptUdp.Checked = True Then
                        setting_key = "UDP"
                    ElseIf OptTcp.Checked = True Then
                        setting_key = "NET" '"TCP"
                    ElseIf OptINet.Checked = True Then
                        setting_key = "NET"
                    ElseIf OptAPI.Checked = True Then
                        setting_key = "API"
                    ElseIf OptJL.Checked = True Then
                        setting_key = "JL"

                    End If
                Case "NETDATAMODE"
                    If OptNSE.Checked = True Then
                        setting_key = "NSE"
                    ElseIf OptMCXSX.Checked = True Then
                        setting_key = "MCX"
                    End If

                Case "SQLSERVER"
                    setting_key = txtServerName.Text
                Case "DATABASE"
                    setting_key = txtDatabase.Text
                Case "AUTHANTICATION"
                    setting_key = IIf(rbtnWindows.Checked, "WINDOWS", "SQL")
                Case "USERNAME"
                    setting_key = txtUserName.Text
                Case "PASSWORD"
                    setting_key = txtPassword.Text
                Case "TCP_CON_NAME"
                    setting_key = cmbServerName.Text
                Case "INTERNETDATA_LINK"
                    ' If RBLink1.Checked = True Then
                    setting_key = "LINK1"
                    'ElseIf RBLink2.Checked = True Then
                    'setting_key = "LINK2"
                    'End If

                Case "NEW_CM_BROADCAST_MT"
                    '  setting_key = CHKwavv.Checked.ToString
                    If chkcmbroadcastMT.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
            End Select
            If setting_key <> "Nothing" Then
                objTrad.SettingName = setting_name
                objTrad.SettingKey = setting_key
                objTrad.Uid = CInt(DR("uid"))
                objTrad.Update_setting()
            End If
        Next
        MsgBox("Settings Applied Successfully.", MsgBoxStyle.Information)
        Call Rounddata()
    End Sub
    Private Sub SaveRounding()
        For Each DR As DataRow In DtSetting.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim setting_key As String = "Nothing"
            Select Case DR("SettingName").ToString.ToUpper
                Case "ROUNDGAMMA"
                    setting_key = cmbgamma.SelectedItem.ToString
                Case "ROUNDVEGA"
                    setting_key = cmbvega.SelectedItem.ToString
                Case "ROUNDTHETA"
                    setting_key = cmbtheta.SelectedItem.ToString
                Case "ROUNDDELTA"
                    setting_key = cmbdelta.SelectedItem.ToString
                Case "ROUNDVOLGA"
                    setting_key = CmbVolga.SelectedItem.ToString
                Case "ROUNDVANNA"
                    setting_key = CmbVanna.SelectedItem.ToString
                Case "ROUNDGAMMA_VAL"
                    setting_key = cmbgammaval.SelectedItem.ToString
                Case "ROUNDVEGA_VAL"
                    setting_key = cmbvegaval.SelectedItem.ToString
                Case "ROUNDTHETA_VAL"
                    setting_key = cmbthetaval.SelectedItem.ToString
                Case "ROUNDDELTA_VAL"
                    setting_key = cmbdeltaval.SelectedItem.ToString
                Case "ROUNDVOLGA_VAL"
                    setting_key = CmbVolgaVal.SelectedItem.ToString
                Case "ROUNDVANNA_VAL"
                    setting_key = CmbVannaVal.SelectedItem.ToString
                Case "ROUNDGROSSMTM"
                    setting_key = cmbgrossmtm.SelectedItem.ToString
                Case "ROUNDEXPENSE"
                    setting_key = cmbexp.SelectedItem.ToString
                Case "ROUNDNETMTM"
                    setting_key = cmbnetmtm.SelectedItem.ToString
                Case "ROUNDSQUAREEXP"
                    setting_key = cmbSqOffExp.SelectedItem.ToString
                Case "ROUNDSQUAREMTM"
                    setting_key = cmbsquaremtm.SelectedItem.ToString
                Case "ROUNDINMARG"
                    setting_key = cmbinmarg.SelectedItem.ToString
                Case "ROUNDEXMARG"
                    setting_key = cmbexmarg.SelectedItem.ToString
                Case "ROUNDREALMARG"
                    setting_key = cmbreal.SelectedItem.ToString
                Case "ROUNDUNMARG"
                    setting_key = cmbunreal.SelectedItem.ToString
                Case "ROUNDREALTOT"
                    setting_key = cmbtot.SelectedItem.ToString
                Case "ROUNDEQUITY"
                    setting_key = cmbequity.SelectedItem.ToString

                Case "ROUNDCURRENCYNETPRICE"
                    setting_key = cmbcurrnetprice.SelectedItem.ToString
                Case "ROUNDCURRENCYLTP"
                    setting_key = cmbcurrltp.SelectedItem.ToString
                Case "ROUNDNEUTRALIZE"
                    setting_key = cmbNeutralize.SelectedItem.ToString

                    ''''''''''''''Devang Start''''''''''''''''''
                Case "ROUNDOPENINT"
                    setting_key = cboOpenInt.SelectedItem.ToString
                Case "ROUNDCALENDER"
                    setting_key = cboCalendar.SelectedItem.ToString
                Case "ROUNDBF"
                    setting_key = cboButterfly.SelectedItem.ToString
                Case "ROUNDVOL"
                    setting_key = cboVolatility.SelectedItem.ToString
                Case "ROUNDBF2"
                    setting_key = cboButterfly2.SelectedItem.ToString
                Case "ROUNDPCP"
                    setting_key = cboPCP.SelectedItem.ToString
                Case "ROUNDRATIO"
                    setting_key = cboRatio.SelectedItem.ToString
                Case "ROUNDSTRADDLE"
                    setting_key = cboStraddle.SelectedItem.ToString
                Case "ROUNDVOLUME"
                    setting_key = cboVolume.SelectedItem.ToString
                Case "ROUNDLTP"
                    setting_key = cboLTP.SelectedItem.ToString
                Case "ROUNDBULLSPREAD"
                    setting_key = cboBullSpread.SelectedItem.ToString
                Case "ROUNDBEARSPREAD"
                    setting_key = cboBearSpread.SelectedItem.ToString
                Case "ROUNDC2C"
                    setting_key = cboC2C.SelectedItem.ToString
                Case "ROUNDC2P"
                    setting_key = cboC2P.SelectedItem.ToString
                Case "ROUNDPCP"
                    setting_key = cboP2P.SelectedItem.ToString

                    ''''''''''''''Devang End''''''''''''''''''
            End Select
            If setting_key <> "Nothing" Then
                objTrad.SettingName = setting_name
                objTrad.SettingKey = setting_key
                objTrad.Uid = CInt(DR("uid"))
                objTrad.Update_setting()
            End If
        Next
        MsgBox("Settings Applied Successfully.", MsgBoxStyle.Information)
        Call Rounddata()
    End Sub
    Private Sub SaveMargin()
        For Each DR As DataRow In DtSetting.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim setting_key As String = "Nothing"
            Select Case DR("SettingName").ToString.ToUpper
                Case "SPAN_PATH"
                    setting_key = txtspanpath.Text
                Case "RATEOFINTEREST"
                    setting_key = Format(Val(txtrateofint.Text), "#0.00")

                Case "CURRENCY SPAN PATH"
                    setting_key = txtcurrencyspanpath.Text
                Case "CURRENCY RATE OF INTEREST"
                    setting_key = Format(Val(txtcurrencyrateofint.Text), "#0.00")
                Case "CURRENCY_MARGIN_QTY"
                    If rblots.Checked = True Then
                        setting_key = 0
                    Else
                        setting_key = 1
                    End If
                    '   setting_key = Format(Val(txtcurrencyrateofint.Text), "#0.00")
                Case "CALMARGINSPAN"
                    If rbInternetmargin.Checked = True Then
                        setting_key = 0
                    Else
                        setting_key = 1
                    End If

                Case "AELOPTION"
                    If rbAEL.Checked = True Then
                        setting_key = 1
                    ElseIf rbNewAEL.Checked = True Then
                        setting_key = 2
                    ElseIf rbAELContracts.Checked = True Then
                        setting_key = 3
                    End If
                Case "INDEX_FAR_MONTH_OPTION"
                    setting_key = Format(Val(txtIndexFarMonth.Text), "#0.00")
                Case "INDEX_OTM_OPTION"
                    setting_key = Format(Val(txtIndexOTM.Text), "#0.00")
                Case "INDEX_OTH_OPTION"
                    setting_key = Format(Val(txtIndexOTH.Text), "#0.00")
                    'Case "CALMARGINWITHOLDMETHOD"
                    '    If Cbmarginmethod.Checked = True Then
                    '        setting_key = 1
                    '    Else
                    '        setting_key = 0
                    '    End If
                Case "CALMARGINWITH_AEL_EXPO"
                    If chkaeladdexpo.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
            End Select
            If setting_key <> "Nothing" Then
                objTrad.SettingName = setting_name
                objTrad.SettingKey = setting_key
                objTrad.Uid = CInt(DR("uid"))
                objTrad.Update_setting()
            End If
        Next
        MsgBox("Settings Applied Successfully.", MsgBoxStyle.Information)
        Call Rounddata()
    End Sub
    Private Sub SaveGreekNeutralsetting()
        For Each DR As DataRow In GdtSettings.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim setting_key As String = "Nothing"
            Select Case DR("SettingName").ToString.ToUpper
                Case "EQ_DELTA_BASE"
                    setting_key = IIf((OptEqLot.Checked = True), Setting_Greeks_BASE.Lot, Setting_Greeks_BASE.Unit)
                Case "CURR_DELTA_BASE"
                    setting_key = IIf((OptCurrLot.Checked = True), Setting_Greeks_BASE.Lot, Setting_Greeks_BASE.Unit)

                Case "EQ_GAMMA_BASE"
                    setting_key = IIf((OptEqLotGa.Checked = True), Setting_Greeks_BASE.Lot, Setting_Greeks_BASE.Unit)
                Case "CURR_GAMMA_BASE"
                    setting_key = IIf((OptCurrLotGa.Checked = True), Setting_Greeks_BASE.Lot, Setting_Greeks_BASE.Unit)

                Case "EQ_VEGA_BASE"
                    setting_key = IIf((OptEqLotVe.Checked = True), Setting_Greeks_BASE.Lot, Setting_Greeks_BASE.Unit)
                Case "CURR_VEGA_BASE"
                    setting_key = IIf((OptCurrLotVe.Checked = True), Setting_Greeks_BASE.Lot, Setting_Greeks_BASE.Unit)

                Case "EQ_THETA_BASE"
                    setting_key = IIf((OptEqLotTh.Checked = True), Setting_Greeks_BASE.Lot, Setting_Greeks_BASE.Unit)
                Case "CURR_THETA_BASE"
                    setting_key = IIf((OptCurrLotTh.Checked = True), Setting_Greeks_BASE.Lot, Setting_Greeks_BASE.Unit)

                Case "EQ_VOLGA_BASE"
                    setting_key = IIf((OptEqLotVo.Checked = True), Setting_Greeks_BASE.Lot, Setting_Greeks_BASE.Unit)
                Case "CURR_VOLGA_BASE"
                    setting_key = IIf((OptCurrLotVo.Checked = True), Setting_Greeks_BASE.Lot, Setting_Greeks_BASE.Unit)

                Case "EQ_VANNA_BASE"
                    setting_key = IIf((OptEqLotVa.Checked = True), Setting_Greeks_BASE.Lot, Setting_Greeks_BASE.Unit)
                Case "CURR_VANNA_BASE"
                    setting_key = IIf((OptCurrLotVa.Checked = True), Setting_Greeks_BASE.Lot, Setting_Greeks_BASE.Unit)
                Case "GREEK_NEUTRAL"
                    If chkGREEK_NEUTRAL.Checked = True Then
                        setting_key = 1
                    Else
                        setting_key = 0
                    End If
            End Select
            If setting_key <> "Nothing" Then
                objTrad.SettingName = setting_name
                objTrad.SettingKey = setting_key
                objTrad.Uid = CInt(DR("uid"))
                objTrad.Update_setting()
            End If
        Next
        MsgBox("Settings Applied Successfully.", MsgBoxStyle.Information)
        Call Rounddata()
    End Sub



#End Region

    Private Sub btnBrowseBackupPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseBackupPath.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtbackuppath.Text) = True Then
            opfile.SelectedPath = txtbackuppath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtbackuppath.Text = str
        End If
    End Sub

    Private Sub btnBrowseContractPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseContractPath.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtContractFilePath.Text) = True Then
            opfile.SelectedPath = txtContractFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtContractFilePath.Text = str
        End If
    End Sub

    Private Sub btnBrowseSecurityPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseSecurityPath.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtSecurityFilePath.Text) = True Then
            opfile.SelectedPath = txtSecurityFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtSecurityFilePath.Text = str
        End If
    End Sub

    Private Sub butSaveExp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSaveExp.Click
        Call SaveExpense()
    End Sub

    Private Sub btnSaveImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveImport.Click
        'If GFun_CheckLicDealerCount(txtccode.Text.Split(",").Length) = False Then
        If GFun_CheckLicDealerCountFOEQCURR(txtccode.Text.ToString()) = False Then
            Exit Sub
        End If
        Call SaveImportFileData()
        Rounddata()

    End Sub

    Private Sub btnSaveConn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveConn.Click
        If NetMode = "UDP" And OptUdp.Checked = True Then
            If txtipaddress1.Text = stripaddress1 And txtipaddress2.Text = stripaddress2 And txtipaddress3.Text = stripaddress3 And txtipaddress4.Text = stripaddress4 And txtFOudpport.Text = strFOudpport And txtudpip1.Text = strudpip1 And txtudpip2.Text = strudpip2 And txtudpip3.Text = strudpip3 And txtudpip4.Text = strudpip4 And txtCMudpport.Text = strCMudpport And txtcurrencyipaddress1.Text = strcurrencyipaddress1 And txtcurrencyipaddress2.Text = strcurrencyipaddress2 And txtcurrencyipaddress3.Text = strcurrencyipaddress3 And txtcurrencyipaddress4.Text = strcurrencyipaddress4 And txtcurrencyudpport.Text = strcurrencyudpport Then
                MsgBox("Save Successfully..")
                Return
            End If


        ElseIf NetMode = "TCP" And OptTcp.Checked = True Then
            If TCP_CON_NAME = cmbServerName.Text Then
                MsgBox("Save Successfully..")
                Return
            End If
        ElseIf NetMode = "NET" And OptINet.Checked = True Then
            SaveConnectionlink()
            MsgBox("Save Successfully..")
            Return
        ElseIf OptJL.Checked = True Then
            'txtServerName.Text = "192.168.98.11,1400"
            'txtDatabase.Text = "SQLDB01B"
            'txtUserName.Text = "finideas"
            'txtPassword.Text = "147258"
            'txtServerName.Text = "192.168.105.91,1406"
            'txtDatabase.Text = "SQLDB01B"
            'txtUserName.Text = "finideas"
            'txtPassword.Text = "123456"
            ReadJLConnection()

            txtServerName.Text = JL_sSQLSERVER
            txtDatabase.Text = JL_sDATABASE
            txtUserName.Text = JL_sUSERNAME
            txtPassword.Text = JL_sPASSWORD



        End If
        If MsgBox("VolHedge Restarting ... " & vbCrLf & "Select Yes To  Save Connection-Setting.. And Restart VolHedge.." & vbCrLf & " Select No To Discard Connection-Setting.", MsgBoxStyle.YesNo, "VolHedge Connection-Setting") = MsgBoxResult.No Then
            Call FillConnectionDataToTextBox()
        Else

            If OptTcp.Checked = True Then
                If ChkSQLConn() = False Then
                    MsgBox("SQL Server Connection Fail !! ", MsgBoxStyle.Critical)
                    Exit Sub
                End If
                '    Dim FW As New IO.StreamWriter(Application.StartupPath & "\SQL Server Connection.txt")
                '    FW.WriteLine("Server Name ::" & txtServerName.Text)
                '    FW.WriteLine("Database Name ::" & txtDatabase.Text)
                '    If rbtnWindows.Checked = True Then
                '        FW.WriteLine("Authentication ::Windows")
                '    Else
                '        FW.WriteLine("Authentication ::SQL Server")
                '        FW.WriteLine("User Name ::" & txtUserName.Text)
                '        FW.WriteLine("Password ::" & txtPassword.Text)
                '    End If

                '    FW.Close()
            End If

            Call SaveConnection()

            DAL.DA_SQL.Close_Connection()
            DAL.DA_SQL.Close_Fo_Connection()
            DAL.DA_SQL.Close_Eq_Connection()
            DAL.DA_SQL.Close_Cur_Connection()



            Dim analysis1 As New frmSettings
            'Dim i = Me.MdiChildren.Length - 1
            'While i >= 0
            '    'If UCase(Me.MdiChildren(i).Name) <> "CONTACT" Then

            '    Me.MdiChildren(i).Close()
            '    i -= 1
            '    'End If
            'End While
            For Each frm As Form In Me.MdiChildren
                frm.Close()
            Next
            Me.Dispose()
            Try
                Process.Start(Application.StartupPath + "\VolHedge.exe")
            Catch ex As Exception

            End Try
            Dim _proceses As Process()
            Try
                _proceses = Process.GetProcessesByName("VolHedge")
                For Each proces As Process In _proceses
                    proces.Kill()
                    Process.Start(Application.StartupPath + "\VolHedge.exe")
                Next
            Catch ex As Exception
                MessageBox.Show("Error in process")
            End Try
            'Application.Restart()
            Process.Start(Application.StartupPath + "\VolHedge.exe")
            'Dim plist As Process() = Process.GetProcesses()
            'For Each p As Process In plist
            '    Try
            '        If p.MainModule.ModuleName = "VolHedge.exe" Then
            '            p.Kill()
            '            Process.Start(Application.StartupPath + "\VolHedge.exe")
            '        End If

            '    Catch
            '        'seems listing modules for some processes fails, so better ignore any exceptions here
            '    End Try
            'Next


        End If
    End Sub

    Private Sub cmbAllRoundSetting_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAllRoundSetting.SelectedIndexChanged
        cmbdelta.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbgamma.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbvega.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbtheta.SelectedItem = cmbAllRoundSetting.SelectedItem
        CmbVolga.SelectedItem = cmbAllRoundSetting.SelectedItem
        CmbVanna.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbdeltaval.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbgammaval.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbvegaval.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbthetaval.SelectedItem = cmbAllRoundSetting.SelectedItem
        CmbVolgaVal.SelectedItem = cmbAllRoundSetting.SelectedItem
        CmbVannaVal.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbinmarg.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbexmarg.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbreal.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbunreal.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbequity.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbtot.SelectedItem = cmbAllRoundSetting.SelectedItem

        cmbgrossmtm.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbexp.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbnetmtm.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbSqOffExp.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbsquaremtm.SelectedItem = cmbAllRoundSetting.SelectedItem


        cmbNeutralize.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbcurrnetprice.SelectedItem = cmbAllRoundSetting.SelectedItem
        cmbcurrltp.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboOpenInt.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboCalendar.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboButterfly.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboVolatility.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboButterfly2.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboPCP.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboRatio.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboStraddle.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboVolume.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboLTP.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboBullSpread.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboBearSpread.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboC2C.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboC2P.SelectedItem = cmbAllRoundSetting.SelectedItem
        cboP2P.SelectedItem = cmbAllRoundSetting.SelectedItem
    End Sub

    Private Sub btnSaveRound_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRound.Click
        Call SaveRounding()
    End Sub

    Private Sub btnSaveMarg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveMarg.Click
        Call SaveMargin()
    End Sub

    Private Sub btnBrowseSpanPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseSpanPath.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtspanpath.Text) = True Then
            opfile.SelectedPath = txtspanpath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtspanpath.Text = str
        End If
    End Sub

    '    '#Region "Save import fil'ssssssssssssssssssssssssss'''''''''''''''''''''''''''''s"

    '    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Dim opfile As New FolderBrowserDialog
    '        If System.IO.Directory.Exists(txtNeatFoFilePath.Text) = True Then
    '            opfile.SelectedPath = txtNeatFoFilePath.Text
    '        End If
    '        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            Dim str As String = opfile.SelectedPath
    '            str = Mid(opfile.SelectedPath, Len(str), 1)
    '            If str = "\" Then
    '                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
    '            Else
    '                str = opfile.SelectedPath
    '            End If
    '            txtNeatFoFilePath.Text = str
    '        End If
    '    End Sub

    '    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Dim opfile As New FolderBrowserDialog
    '        If System.IO.Directory.Exists(txtGetsFoFilePath.Text) = True Then
    '            opfile.SelectedPath = txtGetsFoFilePath.Text
    '        End If
    '        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            Dim str As String = opfile.SelectedPath
    '            str = Mid(opfile.SelectedPath, Len(str), 1)
    '            If str = "\" Then
    '                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
    '            Else
    '                str = opfile.SelectedPath
    '            End If
    '            txtGetsFoFilePath.Text = str
    '        End If
    '    End Sub
    '    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Dim opfile As New FolderBrowserDialog
    '        If System.IO.Directory.Exists(txtGetsEqFilePath.Text) = True Then
    '            opfile.SelectedPath = txtGetsEqFilePath.Text
    '        End If
    '        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            Dim str As String = opfile.SelectedPath
    '            str = Mid(opfile.SelectedPath, Len(str), 1)
    '            If str = "\" Then
    '                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
    '            Else
    '                str = opfile.SelectedPath
    '            End If
    '            txtGetsEqFilePath.Text = str
    '        End If

    '    End Sub

    '    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Dim opfile As New FolderBrowserDialog
    '        If System.IO.Directory.Exists(txtodinFoFilePath.Text) = True Then
    '            opfile.SelectedPath = txtodinFoFilePath.Text
    '        End If
    '        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            Dim str As String = opfile.SelectedPath
    '            str = Mid(opfile.SelectedPath, Len(str), 1)
    '            If str = "\" Then
    '                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
    '            Else
    '                str = opfile.SelectedPath
    '            End If
    '            txtodinFoFilePath.Text = str
    '        End If

    '    End Sub

    '    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Dim opfile As New FolderBrowserDialog
    '        If System.IO.Directory.Exists(txtOdinEqFilePath.Text) = True Then
    '            opfile.SelectedPath = txtOdinEqFilePath.Text
    '        End If

    '        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            Dim str As String = opfile.SelectedPath
    '            str = Mid(opfile.SelectedPath, Len(str), 1)
    '            If str = "\" Then
    '                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
    '            Else
    '                str = opfile.SelectedPath
    '            End If
    '            txtOdinEqFilePath.Text = str
    '        End If

    '    End Sub

    '    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Dim opfile As New FolderBrowserDialog
    '        If System.IO.Directory.Exists(txtNowFOFilePath.Text) = True Then
    '            opfile.SelectedPath = txtNowFOFilePath.Text
    '        End If
    '        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            Dim str As String = opfile.SelectedPath
    '            str = Mid(opfile.SelectedPath, Len(str), 1)
    '            If str = "\" Then
    '                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
    '            Else
    '                str = opfile.SelectedPath
    '            End If
    '            txtNowFOFilePath.Text = str
    '        End If
    '    End Sub

    '    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Dim opfile As New FolderBrowserDialog
    '        If System.IO.Directory.Exists(txtNowEQFilePath.Text) = True Then
    '            opfile.SelectedPath = txtNowEQFilePath.Text
    '        End If
    '        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            Dim str As String = opfile.SelectedPath
    '            str = Mid(opfile.SelectedPath, Len(str), 1)
    '            If str = "\" Then
    '                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
    '            Else
    '                str = opfile.SelectedPath
    '            End If
    '            txtNowEQFilePath.Text = str
    '        End If
    '    End Sub

    '    Private Sub txtNeatFoFilePath_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNowEQFilePath.LostFocus
    '        If CType(sender, TextBox).Text = "" Then
    '            Exit Sub
    '        End If
    '        If System.IO.Directory.Exists((CType(sender, TextBox).Text)) = False Then
    '            MsgBox("Invalid File Path !!", MsgBoxStyle.Exclamation)
    '            CType(sender, TextBox).Text = ""
    '            CType(sender, TextBox).Focus()
    '        End If
    '    End Sub

    '    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Me.Cursor = Cursors.WaitCursor
    '        Dim ConTest As New System.Data.SqlClient.SqlConnection(" Data Source=" & txtGETSServerName.Text & ";Initial Catalog=" & txtGETSDatabase.Text & ";User ID=" & txtGETSUserName.Text & ";Password=" & txtGETSPassword.Text)
    '        Try
    '            ConTest.Open()
    '            ConTest.Close()
    '            MsgBox("Test Connection Succeeded  ", MsgBoxStyle.Information)
    '        Catch ex As Exception
    '            MsgBox("Test Connection Fail !! ", MsgBoxStyle.Critical)
    '        End Try
    '        ConTest.Dispose()
    '        Me.Cursor = Cursors.Default
    '    End Sub

    '    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Me.Cursor = Cursors.WaitCursor
    '        Dim ConTest As New System.Data.SqlClient.SqlConnection(" Data Source=" & txtODINServerName.Text & ";Initial Catalog=" & txtODINDatabase.Text & ";User ID=" & txtODINUserName.Text & ";Password=" & txtODINPassword.Text)
    '        Try
    '            ConTest.Open()
    '            ConTest.Close()
    '            MsgBox("Test Connection Succeeded  ", MsgBoxStyle.Information)
    '        Catch ex As Exception
    '            MsgBox("Test Connection Fail !! ", MsgBoxStyle.Critical)
    '        End Try
    '        ConTest.Dispose()
    '        Me.Cursor = Cursors.Default
    '    End Sub

    '    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Dim opfile As New FolderBrowserDialog
    '        If System.IO.Directory.Exists(txtNotisFoFilePath.Text) = True Then
    '            opfile.SelectedPath = txtNotisFoFilePath.Text
    '        End If
    '        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            Dim str As String = opfile.SelectedPath
    '            str = Mid(opfile.SelectedPath, Len(str), 1)
    '            If str = "\" Then
    '                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
    '            Else
    '                str = opfile.SelectedPath
    '            End If
    '            txtNotisFoFilePath.Text = str
    '        End If
    '    End Sub

    '    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Dim opfile As New FolderBrowserDialog
    '        If System.IO.Directory.Exists(txtNotisEQFilePath.Text) = True Then
    '            opfile.SelectedPath = txtNotisEQFilePath.Text
    '        End If
    '        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            Dim str As String = opfile.SelectedPath
    '            str = Mid(opfile.SelectedPath, Len(str), 1)
    '            If str = "\" Then
    '                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
    '            Else
    '                str = opfile.SelectedPath
    '            End If
    '            txtNotisEQFilePath.Text = str
    '        End If
    '    End Sub

    '    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Dim opfile As New FolderBrowserDialog
    '        If System.IO.Directory.Exists(txtNSEFoFilePath.Text) = True Then
    '            opfile.SelectedPath = txtNSEFoFilePath.Text
    '        End If
    '        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            Dim str As String = opfile.SelectedPath
    '            str = Mid(opfile.SelectedPath, Len(str), 1)
    '            If str = "\" Then
    '                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
    '            Else
    '                str = opfile.SelectedPath
    '            End If
    '            txtNSEFoFilePath.Text = str
    '        End If
    '    End Sub

    '    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Dim opfile As New FolderBrowserDialog
    '        If System.IO.Directory.Exists(txtNSEEQFilePath.Text) = True Then
    '            opfile.SelectedPath = txtNSEEQFilePath.Text
    '        End If
    '        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            Dim str As String = opfile.SelectedPath
    '            str = Mid(opfile.SelectedPath, Len(str), 1)
    '            If str = "\" Then
    '                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
    '            Else
    '                str = opfile.SelectedPath
    '            End If
    '            txtNSEEQFilePath.Text = str
    '        End If
    '    End Sub
    '#End Region


    Private Sub rbtnStrikePremium_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnStrikePremium.CheckedChanged
        If rbtnStrikePremium.Checked = True Then
            txtpl.Text = 0
            txtps.Text = 0
            GBStrikePremium.Enabled = True
            GBPremium.Enabled = False
        Else
            txtspl.Text = 0
            txtsps.Text = 0

            GBStrikePremium.Enabled = False
            GBPremium.Enabled = True
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If chkListSummery.CheckedItems.Count > 0 Then
                objDel.update_Comapany_sammary(chkListSummery)
            End If
            fill_company_summary()
            MsgBox("Saved Successfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmdcomp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcomp.Click
        Try
            'If txtlogid.Text = "" Or txtpass.Text = "" Or txtcpass.Text = "" Or txtopass.Text = "" Then

            If chklist.CheckedItems.Count > 0 Then
                objDel.update_Comapany(chklist)

            End If
            fill_company()
            MsgBox("Saved Successfully.", MsgBoxStyle.Information)
        Catch ex As Exception

            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll11.CheckedChanged
        If chkAll11.Checked = True Then
            For i As Integer = 0 To chklist.Items.Count - 1
                chklist.SetItemChecked(i, True)
            Next
        Else
            For i As Integer = 0 To chklist.Items.Count - 1
                chklist.SetItemChecked(i, False)
            Next
        End If
    End Sub
    Private Sub fill_company()
        Dim comptable As New DataTable
        comptable = objDel.Comapany


        chklist.Items.Clear()

        For Each drow As DataRow In comptable.Rows
            chklist.Items.Add(drow("company"), CBool(drow("isdisplay")))
        Next

    End Sub
    Private Sub fill_company_summary()
        Dim comptable As New DataTable
        comptable = objDel.Comapany_summary
        chkListSummery.Items.Clear()
        For Each drow As DataRow In comptable.Rows
            chkListSummery.Items.Add(drow("company"), CBool(drow("issummary")))
        Next
    End Sub

    Private Sub chkAll1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll1.CheckedChanged
        If chkAll1.Checked = True Then
            For i As Integer = 0 To chkListSummery.Items.Count - 1
                chkListSummery.SetItemChecked(i, True)
            Next
        Else
            For i As Integer = 0 To chkListSummery.Items.Count - 1
                chkListSummery.SetItemChecked(i, False)
            Next
        End If
    End Sub

    Private Sub btnCurrencyBrowseSecurityPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCurrencyBrowseSecurityPath.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtSecurityFilePath.Text) = True Then
            opfile.SelectedPath = txtcurrencyFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtcurrencyFilePath.Text = str
        End If
    End Sub

    Private Sub btnBrowseCurrencySpanPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseCurrencySpanPath.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtcurrencyspanpath.Text) = True Then
            opfile.SelectedPath = txtcurrencyspanpath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtcurrencyspanpath.Text = str
        End If
    End Sub

    Private Sub rbtncStrikePremium_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtncStrikePremium.CheckedChanged
        If rbtncStrikePremium.Checked = True Then
            txtcpl.Text = 0
            txtcps.Text = 0
            GBCurrencyStrikePremium.Enabled = True
            GBCurrencyPremium.Enabled = False
        Else
            txtcspl.Text = 0
            txtcsps.Text = 0
            GBCurrencyStrikePremium.Enabled = False
            GBCurrencyPremium.Enabled = True
        End If
    End Sub

    Private Sub DGTextFile_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGTextFile.CellContentClick
        If e.ColumnIndex = DGTextFile.Columns("Browse").Index Then
            If DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NOTICE FO Trade File" Or DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NOTICE EQ Trade File" Or DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NOTICE CURRENCY Trade File" Or DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NSE FO Trade File" Or DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NSE EQ Trade File" Or DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NSE FO Trade File" Or DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NSE CURRENCY Trade File" Then
                Dim opfiles As New OpenFileDialog
                opfiles.Filter = "Files(*.txt)|*.txt"
                If System.IO.Directory.Exists(DGTextFile.Rows(e.RowIndex).Cells("File_Path").Value & "/") = True Then
                    opfiles.InitialDirectory = DGTextFile.Rows(e.RowIndex).Cells("File_Path").Value & "\" '& txtNoticeFOFileFormat.Text
                End If
                If opfiles.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    DGTextFile.Rows(e.RowIndex).Cells("File_Path").Value = System.IO.Path.GetDirectoryName(opfiles.FileName)
                    DGTextFile.Rows(e.RowIndex).Cells("FileName_Format").Value = System.IO.Path.GetFileName(opfiles.FileName)
                End If
            Else
                Dim opfile As New FolderBrowserDialog
                If System.IO.Directory.Exists(DGTextFile.Rows(e.RowIndex).Cells("File_Path").Value) = True Then
                    opfile.SelectedPath = DGTextFile.Rows(e.RowIndex).Cells("File_Path").Value
                End If
                If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Dim str As String = opfile.SelectedPath
                    str = Mid(opfile.SelectedPath, Len(str), 1)
                    If str = "\" Then
                        str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
                    Else
                        str = opfile.SelectedPath
                    End If
                    DGTextFile.Rows(e.RowIndex).Cells("File_Path").Value = str
                End If

            End If
        End If
    End Sub

    Private Sub DGSQLServer_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGSQLServer.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
        If e.ColumnIndex = DGSQLServer.Columns("Test").Index Then
            Me.Cursor = Cursors.WaitCursor
            Dim ConTest As New System.Data.SqlClient.SqlConnection(" Data Source=" & DGSQLServer.Rows(e.RowIndex).Cells("Server_Name").Value & ";Initial Catalog=" & DGSQLServer.Rows(e.RowIndex).Cells("Database_Name").Value & ";User ID=" & DGSQLServer.Rows(e.RowIndex).Cells("User_Name").Value & ";Password=" & DGSQLServer.Rows(e.RowIndex).Cells("pwd").Value & ";Application Name=" & "VH_" & DGSQLServer.Rows(e.RowIndex).Cells("Server_Name").Value & "_Test")
            Try
                ConTest.Open()
                ConTest.Close()
            Catch ex As Exception
                MsgBox("Test Connection Fail!!", MsgBoxStyle.Critical)
                ConTest.Dispose()
                Me.Cursor = Cursors.Default
                Exit Sub
            End Try
            Try
                Dim Da As New SqlClient.SqlDataAdapter("SELECT TOP 1 * FROM " & DGSQLServer.Rows(e.RowIndex).Cells("Table_Name").Value, ConTest)
                Dim Dt As New DataTable
                Da.Fill(Dt)
            Catch ex As Exception
                MsgBox("Table does not found in Database!!", MsgBoxStyle.Critical)
                ConTest.Dispose()
                Me.Cursor = Cursors.Default
                Exit Sub
            End Try
            MsgBox("Test Connection Successful.", MsgBoxStyle.Information)
            ConTest.Dispose()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub ChkAutoAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAutoAll.CheckedChanged
        For Each DGRow As DataGridViewRow In DGTextFile.Rows
            DGRow.Cells("Text_Auto_Import").Value = ChkAutoAll.Checked
        Next
        For Each DGRow As DataGridViewRow In DGSQLServer.Rows
            DGRow.Cells("SQL_Auto_Import").Value = ChkAutoAll.Checked
        Next
    End Sub

    Private Sub ChkManualAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkManualAll.CheckedChanged
        For Each DGRow As DataGridViewRow In DGTextFile.Rows
            DGRow.Cells("Text_Manual_Import").Value = ChkManualAll.Checked
        Next
        For Each DGRow As DataGridViewRow In DGSQLServer.Rows
            DGRow.Cells("SQL_Manual_Import").Value = ChkManualAll.Checked
        Next
    End Sub

    Private Sub DGTextFile_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGTextFile.CellValueChanged
        'If VarIsFrmLoad = False Then Exit Sub
        'If e.ColumnIndex = DGTextFile.Columns("Text_Type").Index Then
        '    DGTextFile.Rows(e.RowIndex).Cells("File_Code").ReadOnly = True
        '    DGTextFile.Rows(e.RowIndex).Cells("FileName_Format").ReadOnly = True
        '    If DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NOTICE FO Trade File" Or DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NOTICE EQ Trade File" Then
        '        DGTextFile.Rows(e.RowIndex).Cells("File_Code").Value = ""
        '        DGTextFile.Rows(e.RowIndex).Cells("FileName_Format").Value = ""
        '    Else
        '        If DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NEAT FO Trade File" Then
        '            DGTextFile.Rows(e.RowIndex).Cells("File_Code").ReadOnly = False
        '        End If
        '        DGTextFile.Rows(e.RowIndex).Cells("FileName_Format").Value = DtImportType.Compute("MAX(FileName_Format)", "Text_Type='" & DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value & "'")
        '    End If
        'End If
        If VarIsFrmLoad = False Then Exit Sub
        If e.ColumnIndex = DGTextFile.Columns("Text_Type").Index Then
            DGTextFile.Rows(e.RowIndex).Cells("File_Code").ReadOnly = False
            DGTextFile.Rows(e.RowIndex).Cells("FileName_Format").ReadOnly = False
            'DGTextFile.Rows(e.RowIndex).Cells("File_Code").ReadOnly = True
            'DGTextFile.Rows(e.RowIndex).Cells("FileName_Format").ReadOnly = True
            If DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NOTICE FO Trade File" Or DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NOTICE EQ Trade File" Then
                DGTextFile.Rows(e.RowIndex).Cells("File_Code").Value = ""
                DGTextFile.Rows(e.RowIndex).Cells("FileName_Format").Value = ""
            Else
                If DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = "NEAT FO Trade File" Then
                    DGTextFile.Rows(e.RowIndex).Cells("File_Code").ReadOnly = False
                End If

                Dim HT_Text_Type As New Hashtable
                For Each DGRow As DataGridViewRow In DGTextFile.Rows

                    Dim str As String = DGRow.Cells("Text_Type").Value
                    Dim idx As Integer = DGRow.Cells("Text_Type").RowIndex
                    Dim COLidx As Integer = DGRow.Cells("Text_Type").ColumnIndex
                    If str <> "" Then
                        If HT_Text_Type.ContainsValue(str) = False Then
                            HT_Text_Type.Add(idx, str)

                        Else
                            MsgBox("'" & str & "', Already Exits ")
                            DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value = ""

                        End If
                    End If
                Next




            End If
            DGTextFile.Rows(e.RowIndex).Cells("FileName_Format").Value = DtImportType.Compute("MAX(FileName_Format)", "Text_Type='" & DGTextFile.Rows(e.RowIndex).Cells("Text_Type").Value & "'")

        End If
    End Sub

    Private Sub txtccode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtccode.KeyPress
        'If Char.IsDigit(e.KeyChar) = False Then
        'If Char.IsLetter(e.KeyChar) = True Then
        '    e.Handled = True
        'End If

        'If Char.IsWhiteSpace(e.KeyChar) = False Then
        '    Dim arr As New ArrayList
        '    arr.Add(Asc("-"))
        '    arr.Add(Asc("+"))
        '    arr.Add(Asc("."))
        '    arr.Add(Asc(","))
        '    arr.Add(8)
        '    If Not arr.Contains(Asc(e.KeyChar)) Then
        '        e.Handled = True
        '    End If

        'End If
        ' End If
    End Sub
    Private Sub txtccode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtccode.Validating
        'If txtccode.Text.Split(",").Length > G_VarNoOfDealer Then
        '    MsgBox("According to License your Dealer Limit is :: " & G_VarNoOfDealer & vbCrLf & " Dealer limit can't be Exceed !!", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If
        txtccode.Text = txtccode.Text.ToString.TrimEnd(",")
        If GFun_CheckLicDealerCount(txtccode.Text.Split(",").Length) = False Then
            Exit Sub
        End If
    End Sub

    Private Sub TxtFAtm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFAtm.KeyPress
        numonly(e)
    End Sub

    Private Sub TxtTAtm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTAtm.KeyPress
        numonly(e)
    End Sub

    Private Sub TxtVolPer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtVolPer.KeyPress
        numonly(e)
    End Sub

    Private Sub TxtFAtm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFAtm.TextChanged
        If Val(TxtFAtm.Text) < 0 Or Val(TxtFAtm.Text) > 1 Then
            TxtFAtm.Text = "0.40"
        End If
    End Sub

    Private Sub TxtTAtm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtTAtm.TextChanged
        If Val(TxtTAtm.Text) < 0 Or Val(TxtTAtm.Text) > 1 Then
            TxtTAtm.Text = "0.60"
        End If
    End Sub

    Private Sub TxtVolPer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtVolPer.TextChanged
        If Val(TxtVolPer.Text) < 0 Or Val(TxtVolPer.Text) > 100 Then
            TxtVolPer.Text = "20"
        End If
    End Sub

    Private Sub TxtFAtm_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtFAtm.Validating
        If Val(TxtFAtm.Text) > Val(TxtTAtm.Text) Or Val(TxtFAtm.Text) = 0 Then
            e.Cancel = True
            Call MsgBox("Invalid Range.")
        End If
    End Sub

    Private Sub TxtTAtm_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtTAtm.Validating
        If Val(TxtFAtm.Text) > Val(TxtTAtm.Text) Or Val(TxtTAtm.Text) = 0 Then
            e.Cancel = True
            Call MsgBox("Invalid Range.")
        End If
    End Sub


    Private Sub TxtVolPer_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtVolPer.Validating
        If Val(TxtVolPer.Text) = 0 Then
            e.Cancel = True
            Call MsgBox("Invalid Vol%.")
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub txtbtimer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbtimer.KeyPress, TxtNDelay.KeyPress, TxtFDelay.KeyPress, TxtCDelay.KeyPress
        numonly(e)
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        numonly(e)
    End Sub

    Private Sub txtndbl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtndbl.KeyPress, txtndbs.KeyPress, txtdbl.KeyPress, txtdbs.KeyPress, txtfutl.KeyPress, txtfuts.KeyPress, txtspl.KeyPress, txtsps.KeyPress, txtpl.KeyPress, txtps.KeyPress, txtcfutl.KeyPress, txtcfuts.KeyPress, txtcspl.KeyPress, txtcsps.KeyPress, txtcpl.KeyPress, txtcps.KeyPress
        numonly(e)
    End Sub

    Private Sub txtndblp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtndblp.KeyPress, txtndbsp.KeyPress, txtdblp.KeyPress, txtdbsp.KeyPress, txtfutlp.KeyPress, txtfutsp.KeyPress, txtsplp.KeyPress, txtspsp.KeyPress, txtplp.KeyPress, txtpsp.KeyPress, txtcfutlp.KeyPress, txtcfutsp.KeyPress, txtcsplp.KeyPress, txtcspsp.KeyPress, txtcplp.KeyPress, txtcpsp.KeyPress
        numonly(e)
    End Sub

    Private Sub txtsttrate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsttrate.KeyPress
        numonly(e)
    End Sub

    Private Sub txtreftime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtreftime.KeyPress, txtCompactMdbDays.KeyPress
        numonly(e)
    End Sub

    Private Sub txtreftime_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtreftime.Validating, txtCompactMdbDays.Validating
        If Val(txtreftime.Text) <= 0 Then
            e.Cancel = True
        End If
    End Sub



    Private Sub txtipaddress1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtipaddress1.KeyPress, txtipaddress2.KeyPress, txtipaddress3.KeyPress, txtipaddress4.KeyPress, txtudpip1.KeyPress, txtudpip2.KeyPress, txtudpip3.KeyPress, txtudpip4.KeyPress, txtcurrencyipaddress1.KeyPress, txtcurrencyipaddress2.KeyPress, txtcurrencyipaddress3.KeyPress, txtcurrencyipaddress4.KeyPress
        numonly(e)
    End Sub


    Private Sub txtipaddress1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtipaddress1.Validating, txtipaddress2.Validating, txtipaddress3.Validating, txtipaddress4.Validating, txtudpip1.Validating, txtudpip2.Validating, txtudpip3.Validating, txtudpip4.Validating, txtcurrencyipaddress1.Validating, txtcurrencyipaddress2.Validating, txtcurrencyipaddress3.Validating, txtcurrencyipaddress4.Validating
        If Val(CType(sender, TextBox).Text) > 255 Then
            MsgBox("Invalid Input", MsgBoxStyle.Information)
            CType(sender, TextBox).SelectAll()
            e.Cancel = True
        End If
    End Sub

    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        Me.Cursor = Cursors.WaitCursor
        If ChkSQLConn() = True Then
            MsgBox("Test Connection Successful", MsgBoxStyle.Information)
        Else
            MsgBox("Test Connection Fail !! ", MsgBoxStyle.Critical)
        End If
        Me.Cursor = Cursors.Default
    End Sub
    Dim skt_multicastListener_bc_test As Socket
    Private Sub Sub_UDP_Connection_Test(ByVal Str_IP As String, ByVal Str_Port As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            skt_multicastListener_bc_test = New Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
            skt_multicastListener_bc_test.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1)
            skt_multicastListener_bc_test.Bind(New IPEndPoint(IPAddress.Any, CInt(Val(Str_Port))))
            skt_multicastListener_bc_test.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(IPAddress.Parse(Str_IP), IPAddress.Any))

            Try
                Dim bteReceiveData_UDP(512) As Byte
                skt_multicastListener_bc_test.ReceiveTimeout = 1000
                skt_multicastListener_bc_test.Receive(bteReceiveData_UDP)
                If bteReceiveData_UDP.Length > 0 Then
                    MsgBox("Test Connection Successful  ", MsgBoxStyle.Information)
                Else
                    MsgBox("Test Connection Fail !! ", MsgBoxStyle.Critical)
                End If
            Catch EX1 As Exception
                MsgBox("Test Connection Fail !! ", MsgBoxStyle.Critical)
            End Try

            If skt_multicastListener_bc_test.Connected = True Then
                skt_multicastListener_bc_test.Disconnect(True)
                skt_multicastListener_bc_test = Nothing
            End If

        Catch ex As Exception
            MsgBox("IP Address not found !!", MsgBoxStyle.Critical)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Function ChkSQLConn() As Boolean
        Dim StrConn As String = ""
        Dim servername As String = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='SQLSERVER'").ToString)
        Dim DATABASE As String = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='DATABASE'").ToString)
        Dim USERNAME As String = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='USERNAME'").ToString)
        Dim PASSWORD As String = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='USERNAME'").ToString)
        If rbtnWindows.Checked = True Then
            StrConn = " Data Source=" & txtServerName.Text & ";Initial Catalog=" & txtDatabase.Text & ";Integrated Security=True"
        ElseIf rbtnSQLServer.Checked = True Then
            StrConn = " Data Source=" & txtServerName.Text & ";Initial Catalog=" & txtDatabase.Text & ";User ID=" & txtUserName.Text & ";Password=" & txtPassword.Text & ";Application Name=" & "VH_" & txtServerName.Text & "_Test"
        End If
        Dim ConTest As New System.Data.SqlClient.SqlConnection(StrConn)
        Try
            ConTest.Open()
            ConTest.Close()
            ConTest.Dispose()
            Return True

        Catch ex As Exception
            ConTest.Dispose()
            Return False
        End Try
        'Else
        'Return False
        'End If
        ConTest.Close()
    End Function

    Public Function ChkSQLConn1() As Boolean
        Dim StrConn As String = ""
        Dim servername As String = GdtSettings.Compute("max(SettingKey)", "SettingName='SQLSERVER'")
        Dim DATABASE As String = GdtSettings.Compute("max(SettingKey)", "SettingName='DATABASE'").ToString
        Dim USERNAME As String = GdtSettings.Compute("max(SettingKey)", "SettingName='USERNAME'").ToString
        Dim PASSWORD As String = GdtSettings.Compute("max(SettingKey)", "SettingName='PASSWORD'").ToString
        Dim AUTHANTICATION As String = GdtSettings.Compute("max(SettingKey)", "SettingName='AUTHANTICATION'").ToString
        If AUTHANTICATION = "WINDOWS" Then
            StrConn = " Data Source=" & servername & ";Initial Catalog=" & DATABASE & ";Integrated Security=True"
        ElseIf AUTHANTICATION = "SQL" Then
            StrConn = " Data Source=" & servername & ";Initial Catalog=" & DATABASE & ";User ID=" & USERNAME & ";Password=" & PASSWORD & ";Application Name=" & "VH_" & servername & "_Test"
        End If
        Dim ConTest As New System.Data.SqlClient.SqlConnection(StrConn)
        Try
            ConTest.Open()
            ConTest.Close()
            ConTest.Dispose()
            Return True

        Catch ex As Exception
            ConTest.Dispose()
            Return False
        End Try
        ConTest.Close()
        'Else
        'Return False
        'End If
    End Function
    Private Sub OptUdp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptUdp.CheckedChanged
        If OptUdp.Checked = True Then
            GroupBoxUDPSetting.Enabled = True
            GrpBoxSqlServer.Enabled = False
            GBInternet.Visible = False
        ElseIf OptTcp.Checked = True Then
            GroupBoxUDPSetting.Enabled = False
            GrpBoxSqlServer.Enabled = True
            GBInternet.Visible = False
        ElseIf OptINet.Checked = True Then
            GroupBoxUDPSetting.Enabled = False
            GrpBoxSqlServer.Enabled = False
            GBInternet.Visible = True
        End If
    End Sub

    Private Sub OptTcp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptTcp.CheckedChanged, OptINet.CheckedChanged
        If OptUdp.Checked = True Then
            grpboxINet.Visible = False
            GroupBoxUDPSetting.Enabled = True
            GrpBoxSqlServer.Enabled = False
            GBInternet.Visible = False
        ElseIf OptTcp.Checked = True Then
            If RegServerIP.Trim = "" Then
                SetRegServer()
            End If
            If isformseetingload = False And NetMode <> "TCP" Then
                If flgFillTCPConnectionToSqlTOAcess = False Then

                    FillTCPConnectionToSqlTOAcess()
                End If
                FillConnectionDataTocomboBox()
            End If

            grpboxINet.Visible = False
            GroupBoxUDPSetting.Enabled = False
            GrpBoxSqlServer.Enabled = True
            GBInternet.Visible = False
        ElseIf OptINet.Checked = True Then
            grpboxINet.Visible = False
            GroupBoxUDPSetting.Enabled = False
            GrpBoxSqlServer.Enabled = False
            GBInternet.Visible = True
        End If
    End Sub


    Private Sub txtFOudpport_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFOudpport.KeyPress
        numonly(e)
    End Sub

    Private Sub txtCMudpport_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCMudpport.KeyPress
        numonly(e)
    End Sub

    Private Sub txtcurrencyudpport_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcurrencyudpport.KeyPress
        numonly(e)
    End Sub


    Private Sub btnanalysisBrowseSecurityPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnanalysisBrowseSecurityPath.Click
        Dim opfile As New FolderBrowserDialog
        'If System.IO.Directory.Exists(txtSecurityFilePath.Text) = True Then
        '    opfile.SelectedPath = txtAnalysisFilePath.Text
        'End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtAnalysisFilePath.Text = str
        End If
    End Sub





    Private Sub cmbServerName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbServerName.TextChanged
        If DtTCPConnSetting Is Nothing = False Then


            If DtTCPConnSetting.Rows.Count > 0 Then

                If IsDBNull(cmbServerName.Text) = False Then
                    Dim dv As DataView
                    dv = New DataView(DtTCPConnSetting, "ConnectionName ='" & cmbServerName.Text & "'", "ConnectionName", DataViewRowState.CurrentRows)
                    Dim dt As DataTable = dv.ToTable()
                    For Each dr As DataRow In dt.Rows
                        txtServerName.Text = dr("Server")
                        txtDatabase.Text = dr("DBName")
                        txtUserName.Text = dr("UserName")
                        txtPassword.Text = dr("Password")
                    Next
                End If
            Else

            End If
            'TCP_CON_NAME = GdtSettings.Compute("max(SettingKey)", "SettingName='TCP_CON_NAME'").ToString.Trim
            'cmbServerName.Text = TCP_CON_NAME
        End If
    End Sub


    Private Sub btnUPDTestFO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUPDTestFO.Click
        Dim StrIP As String = txtipaddress1.Text.Trim & "." & txtipaddress2.Text.Trim & "." & txtipaddress3.Text.Trim & "." & txtipaddress4.Text.Trim
        Call Sub_UDP_Connection_Test(StrIP, txtFOudpport.Text)
    End Sub

    Private Sub btnUPDTestEQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUPDTestEQ.Click
        Dim StrIP As String = txtudpip1.Text.Trim & "." & txtudpip2.Text.Trim & "." & txtudpip3.Text.Trim & "." & txtudpip4.Text.Trim
        Call Sub_UDP_Connection_Test(StrIP, txtCMudpport.Text)
    End Sub

    Private Sub btnUPDTestCURR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUPDTestCURR.Click
        Dim StrIP As String = txtcurrencyipaddress1.Text.Trim & "." & txtcurrencyipaddress2.Text.Trim & "." & txtcurrencyipaddress3.Text.Trim & "." & txtcurrencyipaddress4.Text.Trim
        Call Sub_UDP_Connection_Test(StrIP, txtcurrencyudpport.Text)
    End Sub







    Private Sub btnCompactDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompactDatabase.Click


        Try
            ' Initialize the trading object
            Dim objTrad As trading = New trading()

            ' Get the backup path from settings, or use default if not available
            Dim mBackupPath As String = CStr(IIf(IsDBNull(GdtSettings.Compute("max(SettingKey)", "SettingName='Backup_path'")), "D:\", GdtSettings.Compute("max(SettingKey)", "SettingName='Backup_path'")))

            ' Check if the backup path exists
            If Not Directory.Exists(mBackupPath) Then
                ' If the path doesn't exist, use the application's startup path
                Dim appStartupPath As String = System.Windows.Forms.Application.StartupPath()

                ' Define the backup directory name
                mBackupPath = Path.Combine(appStartupPath, "DbBackup")

                ' Create the directory if it doesn't exist
                If Not Directory.Exists(mBackupPath) Then
                    Directory.CreateDirectory(mBackupPath)
                End If
            End If

            ' Get the database connection string and prepare the backup filename
            Dim str As String = ConfigurationSettings.AppSettings("dbname")
            Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str

            Dim cur_date_str As String = Format(Now, "ddMMMyyyy_HHmm")
            Dim tstr As String = Path.Combine(mBackupPath, "greek_backup_" & cur_date_str & ".mdb")

            ' Perform the file copy operation
            FileCopy(_connection_string, tstr)

        Catch ex As Exception
            ' Reset the cursor and show any errors
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try

        Try


            'setting_key = txtccode.Text
            REM For Compact MDB
            Dim File_Path, compact_file As String
            'Dim Dac As data_access
            'Original file path that u want to compact
            Dim str As String = ConfigurationSettings.AppSettings("dbname")
            Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str
            File_Path = _connection_string

            'File_Path = Dac.Connection_string

            'compact file path, a temp file
            compact_file = AppDomain.CurrentDomain.BaseDirectory & "db1.mdb"
            'First check the file u want to compact exists or not
            If File.Exists(File_Path) Then
                Dim db As New dao.DBEngine()
                'db.DefaultPassword = "FintEstpwD"
                'CompactDatabase has two parameters, creates a copy of 
                'compact DB at the Destination path
                db.CompactDatabase(File_Path, compact_file, , , ";pwd=" & clsGlobal.glbAcessPassWord & "")
            End If
            'restore the original file from the compacted file
            If File.Exists(compact_file) Then
                File.Delete(File_Path)
                File.Move(compact_file, File_Path)
            End If
            MessageBox.Show("Compact Database Successfully..")
        Catch ex As Exception
            MessageBox.Show("Compact Database Error..")
        End Try
    End Sub

    Private Sub frmSettings_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        MDI.SAVEIMPORTSETTING()
    End Sub

    Private Sub Chkall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chkall.CheckedChanged
        For l As Integer = 0 To chkLBsymbol.Items.Count - 1
            chkLBsymbol.SetItemChecked(l, Chkall.Checked)
        Next
    End Sub

    Private Sub btnSelectSymbol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectSymbol.Click
        Call FillSymbolToChklistboxsymbol()
        pnlSymbol.Visible = True
        pnlSymbol.Location = New Point(134, 121)
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Dim dt As New DataTable()
        dt.Columns.Add("Symbol", GetType(String))
        Dim dr As DataRow

        For Each item As Object In chkLBsymbol.CheckedItems
            dr = dt.NewRow()
            dr("Symbol") = item.ToString()
            dt.Rows.Add(dr)

        Next

        dt.AcceptChanges()
        objTrad.TblRefreshSymbol(dt)
        MessageBox.Show("Save Successfully..")
        pnlSymbol.Visible = False
    End Sub


    Private Sub pnlSymbol_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlSymbol.MouseDown
        allowCoolMove = True
        myCoolPoint = New Point(e.X, e.Y)
        Me.Cursor = Cursors.SizeAll
    End Sub

    Private Sub pnlSymbol_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlSymbol.MouseMove
        If allowCoolMove = True Then
            pnlSymbol.Location = New Point(pnlSymbol.Location.X + e.X - myCoolPoint.X, pnlSymbol.Location.Y + e.Y - myCoolPoint.Y)
        End If
    End Sub

    Private Sub pnlSymbol_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlSymbol.MouseUp
        allowCoolMove = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub chkaddanov_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkaddanov.CheckedChanged

    End Sub


    Private Sub btnApplyGreekNeutral_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyGreekNeutral.Click
        Call SaveGreekNeutralsetting()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfixvol.Click
        If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            btnfixvol.BackColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub Label197_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtbhavcopyProcessday_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbhavcopyProcessday.KeyPress
        numonly(e)
    End Sub

    Private Sub txtbhavcopyProcessday_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbhavcopyProcessday.TextChanged
        If txtbhavcopyProcessday.Text.ToString() = "" Then
            Return
        End If
        If txtbhavcopyProcessday.Text.ToString() = 0 Then
            txtbhavcopyProcessday.Text = 1
        End If

    End Sub


    Private Sub GroupBox17_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub OptAPI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptAPI.CheckedChanged
        If OptINet.Checked = True Then
            GBInternet.Visible = True
        Else
            GBInternet.Visible = False
        End If
    End Sub

    Private Sub OptJL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptJL.CheckedChanged
     
        If OptINet.Checked = True Then
            GBInternet.Visible = True
        Else
            GBInternet.Visible = False
        End If
    End Sub

    Private Sub cboBoxInstance_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBoxInstance.SelectedIndexChanged

    End Sub

    Private Sub grpInstance_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpInstance.Enter

    End Sub

    Private Sub CBcontractnotification_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBcontractnotification.CheckedChanged

    End Sub

    Private Sub ChkOPAna_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkOPAna.CheckedChanged

    End Sub

    Private Sub chkcalusingEqWithindex_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcalusingEqWithindex.CheckedChanged

    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub Label129_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label129.Click

    End Sub

    Private Sub Label128_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label128.Click

    End Sub

    Private Sub txtContractFilePath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContractFilePath.TextChanged

    End Sub

    Private Sub txtSecurityFilePath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSecurityFilePath.TextChanged

    End Sub

    Private Sub lblSecurityFileName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblSecurityFileName.Click

    End Sub

    Private Sub lblContractFileName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblContractFileName.Click

    End Sub

    Private Sub Label117_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label117.Click

    End Sub

    Private Sub Label116_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label116.Click

    End Sub

    Private Sub Label113_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label113.Click

    End Sub

    Private Sub Label68_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label68.Click

    End Sub

    Private Sub ChkAutoContract_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAutoContract.CheckedChanged

    End Sub

    Private Sub ChkAutoSecurity_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAutoSecurity.CheckedChanged

    End Sub

    Private Sub txtcurrencyFilePath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcurrencyFilePath.TextChanged

    End Sub

    Private Sub Label96_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label96.Click

    End Sub

    Private Sub Label98_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label98.Click

    End Sub

    Private Sub Label100_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label100.Click

    End Sub

    Private Sub lblcurrencyFileName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblcurrencyFileName.Click

    End Sub

    Private Sub ChkAutoCurrency_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAutoCurrency.CheckedChanged

    End Sub

    Private Sub Label252_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label252.Click

    End Sub

    Private Sub txtAnalysisFilePath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAnalysisFilePath.TextChanged

    End Sub

    Private Sub Label253_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label253.Click

    End Sub

    Private Sub lblAnalysisFileName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblAnalysisFileName.Click

    End Sub

    Private Sub Label255_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label255.Click

    End Sub

    Private Sub GroupBox17_Enter_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox17.Enter

    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearHistory.Click
        clearIEhistory()
    End Sub
    'Public Sub regStockChart()
    '    Try
    '        var(clsIdKey = Registry.ClassesRoot.OpenSubKey("rStockChartX.StockChartX"))
    '        If (clsIdKey = null) Then
    '                string ocx = FindExternal("StockChartX.ocx");
    '                if (ocx.Length != 0) RegisterOcx(ocx);
    '            End If



    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Function FindExternal(string baseName) As String
    '    Dim f As String = Application.StartupPath
    '    Try

    '        For i As Integer = 0 To 6

    '        Next


    '         for (int i = 0; i < 6; i++)
    '            {
    '                if (!File.Exists(f + "\\" + baseName)) f = Path.GetDirectoryName(f);
    '            }

    '                If (!File.Exists(f + "\\" + baseName)) Then
    '            {
    '                MessageBox.Show("Could not locate " + baseName + " in: " + f);
    '                return "";
    '            }

    '            f += "\\" + baseName;
    '    Catch ex As Exception

    '    End Try




    '    Return f
    'End Function

   
    
    Private Sub Button2_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            'Shell("regsvr32.exe " & Application.StartupPath & "\MSCHRT20.OCX", AppWinStyle.Hide)
            'Shell("regsvr32.exe " & Application.StartupPath & "\StockChartX.OCX", AppWinStyle.Hide)
            'Shell(regsvr32.exe Application.StartupPath & "\StockChartX.ocx", AppWinStyle.Hide)

            Dim ocxpath As String = ""
            Dim p As New Process()

            Try

                ocxpath = Application.StartupPath & "\MSCHRT20.OCX"


                p.StartInfo.UseShellExecute = False
                p.StartInfo.CreateNoWindow = True
                p.StartInfo.Verb = "runas"
                p.StartInfo.FileName = "regsvr32.exe"
                p.StartInfo.RedirectStandardOutput = True
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                'p.StartInfo.Arguments = ocxpath

                p.StartInfo.Arguments = "/s " & "\" & ocxpath & " \ """
                'p.StartInfo.Arguments = "/s " & "\" & " & ocxPath & " & " \ """

                p.Start()
                p.WaitForExit()
                MessageBox.Show("MSCHRT20.OCX Register Successfully..")
            Catch ex As Exception
                MessageBox.Show("MSCHRT20 Register Error..")
            End Try

            Try
                ocxpath = Application.StartupPath & "\StockChartX.OCX"
                p = New Process()

                p.StartInfo.UseShellExecute = False
                p.StartInfo.CreateNoWindow = True
                p.StartInfo.Verb = "runas"
                p.StartInfo.FileName = "regsvr32.exe"
                p.StartInfo.RedirectStandardOutput = True
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                'p.StartInfo.Arguments = ocxpath
                p.StartInfo.Arguments = "/s " & "\" & ocxpath & " \ """
                'p.StartInfo.Arguments = "/s " & "\" & " & ocxPath & " & " \ """

                p.Start()
                p.WaitForExit()
                MessageBox.Show("StockChartX.OCX Register Successfully..")
            Catch ex As Exception
                MessageBox.Show("StockChartX Register Error..")
            End Try




       
        Catch ex As Exception
            MessageBox.Show("Register Error..")
        End Try
        
    End Sub
    
    Private Sub RBsynatmstrike_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBsynatmstrike.CheckedChanged
        If RBsynatmstrike.Checked = True Then
            RBSynUpdownStrikeAvg.Checked = False
            chksynfutniftyskip.Checked = False
        End If
    End Sub

    Private Sub RBSynUpdownStrikeAvg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSynUpdownStrikeAvg.CheckedChanged
        If RBSynUpdownStrikeAvg.Checked = True Then
            RBsynatmstrike.Checked = False
        End If
    End Sub

    Private Sub chkBcastDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBcastDate.CheckedChanged

    End Sub

    Private Sub btnBrowseBhavCopyPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseBhavCopyPath.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtBhavCopyFilePath.Text) = True Then
            opfile.SelectedPath = txtBhavCopyFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtBhavCopyFilePath.Text = str
        End If
    End Sub

    Private Sub ChkAutoBhavCopy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAutoBhavCopy.CheckedChanged

    End Sub

    Private Sub ChkSaveDataAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSaveDataAuto.CheckedChanged

    End Sub

    Private Sub ChkSaveAnaDataFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSaveAnaDataFile.CheckedChanged

    End Sub

    Private Sub btnBrowseAELPath_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseAELPath.Click

    End Sub

    Private Sub btnmultipliercolor_Click(sender As System.Object, e As System.EventArgs) Handles btnmultipliercolor.Click
        If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            btnmultipliercolor.BackColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim objfrmTradefilesetting As New frmTradefilesetting
        objfrmTradefilesetting.ShowDialog()
        CustomFlag = True
        If frmTradefilesetting.FormateName <> "" Then
            'If frmTradefilesetting.FormateName <> "" Or frmTradefilesetting.DeleteProfileFlag = False Then
            DGTextFile.Rows.Clear()
            DtImportType = objTrad.Select_Import_type
            CType(DGTextFile.Columns("Text_Type"), DataGridViewComboBoxColumn).Items.Clear()
            For Each Dr As DataRow In DtImportType.Select("Import_Type='TEXT File'")
                CType(DGTextFile.Columns("Text_Type"), DataGridViewComboBoxColumn).Items.Add(Dr("Text_Type"))
            Next
            For Each Dr As DataRow In DtImportType.Select("Import_Type='Custom TEXT File'")
                CType(DGTextFile.Columns("Text_Type"), DataGridViewComboBoxColumn).Items.Add(Dr("Text_Type"))
            Next

            For Each dr As DataRow In DtImportType.Rows
                If dr("Text_Type").ToString() <> "" Then
                    If HT_RefreshTrde.Contains(dr("Text_Type")) = False Then
                        HT_RefreshTrde.Add(dr("Text_Type"), "")
                    End If
                End If
            Next

        End If
        Try
            If frmTradefilesetting.FormateName <> "" Then

                If frmTradefilesetting.Formatetype <> "" Then
                    If frmTradefilesetting.Formatetype = "FO" Then
                        DGTextFile.Rows(0).Cells("Text_Type").Value = "CustomFO_" + frmTradefilesetting.FormateName
                    ElseIf frmTradefilesetting.Formatetype <> "EQ" Then
                        DGTextFile.Rows(0).Cells("Text_Type").Value = "CustomEQ_" + frmTradefilesetting.FormateName
                    Else
                        DGTextFile.Rows(0).Cells("Text_Type").Value = "CustomCurr_" + frmTradefilesetting.FormateName
                    End If


                End If

            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub DGTextFile_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DGTextFile.DataError

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub


End Class