Imports System.Windows.Forms
Imports System.IO
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.Net.Sockets.Socket
Imports VolHedge.DAL
Imports System.Net.NetworkInformation
Imports System.Management
Imports System.Net.Sockets.NetworkStream
Imports System.Data.OleDb
Imports VolHedge.OptionG
Imports System.Runtime.InteropServices

Public Class MDI
    Dim obj_DelSetDisableMenubar As New GDelegate_MdiStatus(AddressOf Sub_Disable_Manu_n_Tool_bar)
    Dim trd As New trading
    Dim frmsetting As New frmSettings
    Dim loadFlag As Boolean
    Public ObjMargin As FrmMargin
    Private threadcount As Thread
    Private Objbhavcopy As bhav_copy
    Private Delegate Sub MyVarifyVersion()
    Dim VarifyRef As MyVarifyVersion
    Public VarMDITitle As String

    Public AutoFilemsg As String = ""

    Public OpenFileDialog1 As System.Windows.Forms.OpenFileDialog

    Public Thr_GetInterNetDat As New Thread(AddressOf analysis.ThrInterNetData)
    Dim strIndex As String
    'Public dtAccess As DataTable
    'Dim multicastListener_fo As System.Net.Sockets.UdpClient
    'Dim multicastListener_cm As System.Net.Sockets.UdpClient
    'Dim multicastListener_Currency As System.Net.Sockets.UdpClient
    Public adpter As NetworkInterface
    Dim Var_Str_Prev_FO_Entry_Text As String REM To store Last assign FO entry date to Menu lable
    Dim Var_Str_Prev_EQ_Entry_Text As String
    Dim Var_Str_Prev_CURR_Entry_Text As String
    Dim spanfiledownload As String
    Dim spanfiledownloadzip As String
    Dim aelfiledownloadzip As String
    Dim downloadbhavcopy As String = ""
    Dim Mrateofinterast As Double = 0

    Dim IMothrBoardSrNo As String = "0"
    Dim IHDDSrNoStr As String = "0"
    Dim IProcessorSrNoStr As String = "0"
    Dim user As String
    Dim password As String
    Dim objsplash As SplashScreenMnUsrLic

    Dim mPerf As CPerfCheck = New CPerfCheck()
    Public mAnalysisCloseCancel As Boolean

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Global.System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub
    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub
    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub
    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private m_ChildFormNumber As Integer = 0

    Dim obj As Object

    Private Sub MDI_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", SplashScreen1.obj)

        Dim objTrad As New trading

        If NetMode = "UDP" Then
            GSub_Stop_Broadcast()
        End If

        REM Refill Expense_Data table
        REM Insert Expense Data To Expense_data
        objTrad.Delete_Expense_Data_All()
        objTrad.Insert_Expense_Data(G_DTExpenseData)
        REM End
        If IsScriptMapper = False Then
            objTrad.Uid = GdtSettings.Select("SettingName='APPLICATION_CLOSING_FLAG'")(0).Item("Uid")
            objTrad.SettingName = "APPLICATION_CLOSING_FLAG"
            objTrad.SettingKey = "TRUE"
            objTrad.Update_setting()
        End If

        If AppLicMode = "NETLIC" Then
            ObjLoginData.LogOutUser()
        End If

        Threading.Thread.Sleep(2000)
        'If FSTimerLogFile Is Nothing = False Then
        '    FSTimerLogFile.WriteLine("=============================" & Now)
        '    FSTimerLogFile.Close()
        'End If

        Write_ErrorLog3("=============================" & Now)
        Dim proc As System.Diagnostics.Process
        For Each proc In System.Diagnostics.Process.GetProcessesByName("VolHedge")
            proc.Kill()
        Next

        'Dim usermaster As New compactdatabase
        'usermaster.MdiParent = Me
        'usermaster.Show()
        'Try
        '    Dim File_Path, compact_file As String
        '    'Original file path that u want to compact
        '    Dim str As String = ConfigurationSettings.AppSettings("dbname")
        '    Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str

        '    File_Path = _connection_string
        '    'compact file path, a temp file
        '    compact_file = AppDomain.CurrentDomain.BaseDirectory & "db1.mdb"
        '    'First check the file u want to compact exists or not
        '    If File.Exists(File_Path) Then
        '        '//Dim db As New dao.DBEngine()
        '        'CompactDatabase has two parameters, creates a copy of 
        '        'compact DB at the Destination path
        '        'db.CompactDatabase(File_Path, compact_file)
        '        '//db.CompactDatabase(File_Path, compact_file, , , ";pwd=" & clsGlobal.glbAcessPassWord & "")
        '    End If
        '    'restore the original file from the compacted file
        '    If File.Exists(compact_file) Then
        '        File.Delete(File_Path)
        '        File.Move(compact_file, File_Path)
        '    End If
        '    'MsgBox("Database Compact Successfully")
        'Catch ex As Exception
        '    'MsgBox(ex.Message)
        'End Try
        End
        'closePort()
        'Try
        '    threadcount.Abort()
        'Catch ex As Exception

        'End Try
        '    SAVEIMPORTSETTING()
    End Sub


    Private Sub wait(ByVal interval As Integer)
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            ' Allows UI to remain responsive
            ' Application.DoEvents()
            SendKeys.Flush()
        Loop
        sw.Stop()
    End Sub
    Private Sub MDI_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'If e.KeyCode = Keys.F1 Then

        '    Dim frm As New ShortCut
        '    frm.ShowDialog()
        'Else
        If e.KeyCode = Keys.F2 Then
            AnalysisToolStripMenuItem_Click(sender, e)
        ElseIf e.KeyCode = Keys.F3 Then
            ScenarioToolStripMenuItem_Click(sender, e)
        ElseIf e.KeyCode = Keys.F4 And e.Alt = False And e.Control = False And scenario1.chkscenario = False Then
            'scenario1.chkscenario = False
            MarketWatchF4ToolStripMenuItem_Click(sender, e)
        ElseIf e.KeyCode = Keys.F6 Then
            FilterBhavCopyF6ToolStripMenuItem_Click(sender, e)
        ElseIf e.Control = True And e.KeyCode = Keys.T Then
            ToolStripcompanyCombo.Visible = True
            ToolStripMenuSearchComp.Visible = True
            ToolStripcompanyCombo.Focus()
            ToolStripcompanyCombo.ToolTipText = "search company"
        ElseIf e.Control = True And e.KeyCode = Keys.S Then
            analysis.Close()
            analysis.chkanalysis = False
            analysis.MdiParent = Me
            frmSettings.ShowDialog()

        ElseIf e.KeyCode = 27 Then
            ToolStripcompanyCombo.Visible = False
            ToolStripMenuSearchComp.Visible = False
        ElseIf e.Control = True And e.KeyCode = Keys.L Then
            Dim str As String
            str = "c:\WINDOWS\system32\calc.exe"
            Shell(str, AppWinStyle.NormalFocus)
        ElseIf e.Control = True And e.KeyCode = Keys.R Then
            Call RefreshLTPCtrlF5ToolStripMenuItem_Click(RefreshLTPCtrlF5ToolStripMenuItem, New EventArgs)
        ElseIf e.Control = True And e.KeyCode = Keys.F12 Then
            'Dim strPass As String
            'strPass = InputBox("Enter Password", "Range Calculator", "")
            'If strPass = "fin123" Then
            UploadRangeDataToolStripMenuItem_Click(Nothing, Nothing)
            'Else
            '    MessageBox.Show("Password Incorrect! Try Again!")
            'End If
        End If
    End Sub
    Public Function SAVEIMPORTSETTING()
        '=========================REM:Coding For UPDATE FIELD OF IMPORTSETTING TABLE  17/06/2014===============

        Dim DTChkAuto As DataTable
        DTChkAuto = G_DTImportSetting
        '  For Each Drow As DataRow In DTChkAuto.Columns

        'And DTChkAuto.Select(" Manual_Import = '" & False & "' And Auto_Import = '" & False & "'").Length <> 0
        If DTChkAuto Is Nothing = False Then


            If DTChkAuto.Select(" Manual_Import = '" & False & "' And Auto_Import = '" & True & "'").Length = 0 And DTChkAuto.Select(" Manual_Import = '" & True & "' And Auto_Import = '" & True & "'").Length = 0 Then
                RBCheck.Checked = False
            Else
                RBCheck.Checked = True
                RBCheck.BackColor = Color.BlueViolet
            End If
            '   Next
        End If

    End Function
    Public Function ChkSQLConnNew(ByVal servername As String, ByVal DATABASE As String, ByVal USERNAME As String, ByVal PASSWORD As String) As Boolean
        Dim StrConn As String = ""
        'Dim servername As String = GdtSettings.Compute("max(SettingKey)", "SettingName='SQLSERVER'")
        'Dim DATABASE As String = GdtSettings.Compute("max(SettingKey)", "SettingName='DATABASE'").ToString
        'Dim USERNAME As String = GdtSettings.Compute("max(SettingKey)", "SettingName='USERNAME'").ToString
        'Dim PASSWORD As String = GdtSettings.Compute("max(SettingKey)", "SettingName='PASSWORD'").ToString
        'Dim AUTHANTICATION As String = GdtSettings.Compute("max(SettingKey)", "SettingName='AUTHANTICATION'").ToString
        'If AUTHANTICATION = "WINDOWS" Then
        '    StrConn = " Data Source=" & servername & ";Initial Catalog=" & DATABASE & ";Integrated Security=True"
        ' ElseIf AUTHANTICATION = "SQL" Then
        StrConn = " Data Source=" & servername & ";Initial Catalog=" & DATABASE & ";User ID=" & USERNAME & ";Password=" & PASSWORD & ";Application Name=" & "VH_" & servername & "_Test"
        'End If
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
    End Function

    Public Function SaveTCPConnection()

        dtAccess = trd.select_TCP_Connection

        'Dim Thr_tcp_Connection As New Thread(AddressOf FillTCPConnectionToSqlTOAcess)
        'Thr_tcp_Connection.Start()

        Dim servernm As String = ""
        Dim db As String = ""
        Dim unm As String = ""
        Dim pwd As String = ""


        TCP_CON_NAME = GdtSettings.Compute("max(SettingKey)", "SettingName='TCP_CON_NAME'").ToString.Trim
        'Dim Thr_tcp_Connection As New Thread(AddressOf FillTCPConnectionToSqlTOAcess)
        'Thr_tcp_Connection.Start()
        FillTCPConnectionToSqlTOAcess()
        Dim dr As DataRow() = dtAccess.Select("ConnectionName='" & TCP_CON_NAME & "' and Visible='True'")


        If dr.Length > 0 Then
            For Each DR1 As DataRow In dr
                servernm = DR1("Server")
                db = DR1("DBName")
                unm = DR1("UserName")
                pwd = DR1("Password")
            Next
            If ChkSQLConnNew(servernm, db, unm, pwd) = True Then
                SaveConnectiontcp(dr)
            Else

                Dim Count As Integer = dtAccess.Rows.Count
                Dim i As Int16 = 0
                For Each draccess As DataRow In dtAccess.Rows
                    i = 1

                    TCP_CON_NAME = draccess("ConnectionName")

                    Dim dracessdata As DataRow() = dtAccess.Select("ConnectionName='" & TCP_CON_NAME & "' and Visible='True'")
                    Dim drAccess1 As DataRow() = dtAccess.Select("ConnectionName='" & TCP_CON_NAME & "' and Visible='True'")
                    For Each DR1 As DataRow In dracessdata
                        servernm = DR1("Server")
                        db = DR1("DBName")
                        unm = DR1("UserName")
                        pwd = DR1("Password")
                    Next
                    If ChkSQLConnNew(servernm, db, unm, pwd) = True Then

                        For Each DR1 As DataRow In dracessdata
                            servernm = draccess("Server")
                            db = DR1("DBName")
                            unm = DR1("UserName")
                            pwd = DR1("Password")
                        Next
                        SaveConnectiontcp(drAccess1)
                        i = 0
                        Exit For
                    End If


                Next
                If i > 0 Then
                    MessageBox.Show("No Connection Available..Plz Try After Some time")
                    WriteLog(servernm & " , " & db)
                    frmsetting.ShowDialog()
                End If
            End If
        Else
            Dim i As Int16 = 0
            If dtAccess.Rows.Count > 0 And dtAccess Is Nothing = False Then
                Dim Count As Integer = dtAccess.Rows.Count

                If Count > 0 Then

                    For Each dr1 As DataRow In dtAccess.Rows
                        i = 1
                        'dt.Clear()
                        TCP_CON_NAME = dr1("ConnectionName")

                        Dim drAccess1 As DataRow() = dtAccess.Select("ConnectionName='" & TCP_CON_NAME & "' and Visible='True'")

                        For Each DRconn As DataRow In drAccess1
                            servernm = DRconn("Server")
                            db = DRconn("DBName")
                            unm = DRconn("UserName")
                            pwd = DRconn("Password")
                        Next
                        If ChkSQLConnNew(servernm, db, unm, pwd) = True Then
                            For Each DRconn1 As DataRow In drAccess1
                                servernm = DRconn1("Server")
                                db = DRconn1("DBName")
                                unm = DRconn1("UserName")
                                pwd = DRconn1("Password")
                            Next
                            SaveConnectiontcp(drAccess1)
                            i = 0
                            Exit For
                        End If
                        i = i + 1

                    Next
                Else
                    i = 1
                End If
            Else
                i = 1
            End If
            If i > 0 Then
                trd.Update_setting("TCP_CON_NAME", " ")
                trd.Update_setting("USERNAME", " ")

                MessageBox.Show("No TCP Connection Available..Plz Try After Some time ")
                WriteLog(servernm & " , " & db)

                frmsetting.ShowDialog()
            End If
        End If




        'If IsDBNull(TCP_CON_NAME) = False And dt Is Nothing = False Then
        '    For Each DR As DataRow In dt.Rows
        '        servernm = DR("Server")
        '        db = DR("DBName")
        '        unm = DR("UserName")
        '        pwd = DR("Password")
        '    Next
        '    If ChkSQLConnNew(servernm, db, unm, pwd) = True Then
        '        SaveConnectiontcp(dt)
        '    Else
        '        'Dim DtTCPConnSetting As DataTable = frmsetting.SelectdataofTcpConnection()
        '        Dim Count As Integer = DtTCPConnSetting.Rows.Count
        '        Dim i As Int16 = 0
        '        For Each dr As DataRow In DtTCPConnSetting.Rows
        '            i = 1
        '            dt.Clear()
        '            TCP_CON_NAME = dr("ConnectionName")
        '            dt = SelectdataofTcpConnection(TCP_CON_NAME)
        '            'SaveConnectiontcp(dt)
        '            For Each DR1 As DataRow In dt.Rows
        '                servernm = DR1("Server")
        '                db = DR1("DBName")
        '                unm = DR1("UserName")
        '                pwd = DR1("Password")
        '            Next
        '            If ChkSQLConnNew(servernm, db, unm, pwd) = True Then
        '                For Each DR1 As DataRow In dt.Rows
        '                    servernm = dr("Server")
        '                    db = DR1("DBName")
        '                    unm = DR1("UserName")
        '                    pwd = DR1("Password")
        '                Next
        '                SaveConnectiontcp(dt)
        '                i = 0
        '                Exit For
        '            End If


        '        Next
        '        If i > 0 Then
        '            MessageBox.Show("No Connection Available..Plz Try After Some time")
        '        End If
        '    End If

        'Else
        '    'Dim DtTCPConnSetting As DataTable = frmsetting.SelectdataofTcpConnection()

        '    Dim i As Int16 = 0
        '    If dt Is Nothing = False And DtTCPConnSetting Is Nothing = False Then
        '        Dim Count As Integer = DtTCPConnSetting.Rows.Count

        '        If Count > 0 Then


        '            For Each dr As DataRow In DtTCPConnSetting.Rows
        '                i = 1
        '                dt.Clear()
        '                TCP_CON_NAME = dr("ConnectionName")
        '                dt = SelectdataofTcpConnection(TCP_CON_NAME)
        '                'SaveConnectiontcp(dt)
        '                For Each DR1 As DataRow In dt.Rows
        '                    servernm = DR1("Server")
        '                    db = DR1("DBName")
        '                    unm = DR1("UserName")
        '                    pwd = DR1("Password")
        '                Next
        '                If ChkSQLConnNew(servernm, db, unm, pwd) = True Then
        '                    For Each DR1 As DataRow In dt.Rows
        '                        servernm = dr("Server")
        '                        db = DR1("DBName")
        '                        unm = DR1("UserName")
        '                        pwd = DR1("Password")
        '                    Next
        '                    SaveConnectiontcp(dt)
        '                    i = 0
        '                    Exit For
        '                End If
        '                i = i + 1

        '            Next
        '        Else
        '            i = 1
        '        End If
        '    Else
        '        i = 1
        '    End If
        '    If i > 0 Then
        '        trd.Update_setting_TCP("TCP_CON_NAME", " ")
        '        trd.Update_setting_TCP("USERNAME", " ")

        '        MessageBox.Show("No TCP Connection Available..Plz Try After Some time ")
        '        frmsetting.ShowDialog()
        '    End If
        '        End If

    End Function
    'Public Function SaveTCPConnection()
    '    Dim dt As DataTable
    '    Dim servernm, db, unm, pwd As String
    '    TCP_CON_NAME = GdtSettings.Compute("max(SettingKey)", "SettingName='TCP_CON_NAME'").ToString.Trim
    '    If IsDBNull(TCP_CON_NAME) = False Then
    '        dt = SelectdataofTcpConnection(TCP_CON_NAME)
    '        'SaveConnectiontcp(dt)

    '        For Each DR As DataRow In dt.Rows
    '            servernm = DR("Server")
    '            db = DR("DBName")
    '            unm = DR("UserName")
    '            pwd = DR("Password")
    '        Next
    '        If ChkSQLConnNew(servernm, db, unm, pwd) = True Then
    '            SaveConnectiontcp(dt)
    '        Else
    '            Dim DtTCPConnSetting As DataTable = frmsetting.SelectdataofTcpConnection()
    '            Dim Count As Integer = DtTCPConnSetting.Rows.Count
    '            Dim i As Int16 = 0
    '            For Each dr As DataRow In DtTCPConnSetting.Rows
    '                i = 1
    '                dt.Clear()
    '                TCP_CON_NAME = dr("ConnectionName")
    '                dt = SelectdataofTcpConnection(TCP_CON_NAME)
    '                'SaveConnectiontcp(dt)
    '                For Each DR1 As DataRow In dt.Rows
    '                    servernm = DR1("Server")
    '                    db = DR1("DBName")
    '                    unm = DR1("UserName")
    '                    pwd = DR1("Password")
    '                Next
    '                If ChkSQLConnNew(servernm, db, unm, pwd) = True Then
    '                    For Each DR1 As DataRow In dt.Rows
    '                        servernm = dr("Server")
    '                        db = DR1("DBName")
    '                        unm = DR1("UserName")
    '                        pwd = DR1("Password")
    '                    Next
    '                    SaveConnectiontcp(dt)
    '                    i = 0
    '                    Exit For
    '                End If


    '            Next
    '            If i > 0 Then
    '                MessageBox.Show("No Connection Available..Plz Try After Some time " & vbCrLf & "  Or" & vbCrLf & " Contact Your Vendor.")
    '            End If
    '        End If

    '      Else
    '        Dim DtTCPConnSetting As DataTable = frmsetting.SelectdataofTcpConnection()
    '        Dim Count As Integer = DtTCPConnSetting.Rows.Count
    '        Dim i As Int16 = 0
    '        For Each dr As DataRow In DtTCPConnSetting.Rows
    '            i = 1
    '            dt.Clear()
    '            TCP_CON_NAME = dr("ConnectionName")
    '            dt = SelectdataofTcpConnection(TCP_CON_NAME)
    '            'SaveConnectiontcp(dt)
    '            For Each DR1 As DataRow In dt.Rows
    '                servernm = DR1("Server")
    '                db = DR1("DBName")
    '                unm = DR1("UserName")
    '                pwd = DR1("Password")
    '            Next
    '            If ChkSQLConnNew(servernm, db, unm, pwd) = True Then
    '                For Each DR1 As DataRow In dt.Rows
    '                    servernm = dr("Server")
    '                    db = DR1("DBName")
    '                    unm = DR1("UserName")
    '                    pwd = DR1("Password")
    '                Next
    '                SaveConnectiontcp(dt)
    '                i = 0
    '                Exit For
    '            End If


    '        Next
    '        If i > 0 Then
    '            MessageBox.Show("No Connection Available..Plz Try After Some time")
    '        End If
    '    End If

    'End Function
    Private Sub SaveConnectiontcp(ByVal dr() As DataRow)

        Dim updateflg As Boolean = False
        Dim servernm As String = ""
        Dim db As String = ""
        Dim unm As String = ""
        Dim pwd As String = ""

        For Each DR1 As DataRow In dr
            servernm = DR1("Server")
            db = DR1("DBName")
            unm = DR1("UserName")
            pwd = DR1("Password")
            trd.Update_setting(servernm, "SQLSERVER")
            trd.Update_setting(db, "DATABASE")
            trd.Update_setting(unm, "USERNAME")
            trd.Update_setting(pwd, "PASSWORD")
            updateflg = True
        Next


        'Dim DtSetting1 As DataTable = GdtSettings
        'For Each DR2 As DataRow In DtSetting1.Rows
        '    Dim setting_name As String = DR2("SettingName").ToString.ToUpper
        '    Dim setting_key As String = "Nothing"
        '    Select Case DR2("SettingName").ToString.ToUpper
        '        Case "SQLSERVER"
        '            setting_key = servernm
        '        Case "DATABASE"
        '            setting_key = db
        '        Case "USERNAME"
        '            setting_key = unm
        '        Case "PASSWORD"
        '            setting_key = pwd


        '    End Select
        '    If setting_key <> "Nothing" Then
        '        Dim trd1 As New trading
        '        trd1.SettingName = setting_name
        '        trd1.SettingKey = setting_key
        '        trd1.Uid = CInt(DR2("uid"))
        '        trd1.Update_setting()
        '        updateflg = True
        '    End If
        'Next
        If updateflg = True Then
            GdtSettings = trd.Settings
        End If

    End Sub
    Public Function SelectdataofTcpConnection(ByVal Conn_Name) As DataTable
        Try

            'Dim Query As String = "select * from tblTCPConnection Where ConnectionName='" & Conn_Name & "'"
            Dim Query As String = "select * from tblTCPConnection Where ConnectionName='" & Conn_Name & "' and Visible='True'"
            DAL.data_access_sql.Cmd_Text = Query

            Return DAL.data_access_sql.FillListfin()

        Catch ex As Exception
            '  MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Shared Function DownloadFile(ByVal FtpUrl As String, ByVal FileNameToDownload As String, ByVal userName As String, ByVal password As String, ByVal tempDirPath As String) As String
        'Dim ResponseDescription As String = ""
        Dim PureFileName As String = New FileInfo(FileNameToDownload).Name
        'Dim DownloadedFilePath As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName
        'Dim downloadUrl As String = [String].Format("{0}/{1}", FtpUrl, FileNameToDownload)
        'Dim req As FtpWebRequest = DirectCast(FtpWebRequest.Create(downloadUrl), FtpWebRequest)
        'req.Method = WebRequestMethods.Ftp.DownloadFile
        'req.Credentials = New NetworkCredential(userName, password)
        'req.UseBinary = True
        'req.Proxy = Nothing
        'Try
        '    Dim response As FtpWebResponse = DirectCast(req.GetResponse(), FtpWebResponse)
        '    Dim stream As Stream = response.GetResponseStream()
        '    Dim buffer As Byte() = New Byte(2047) {}
        '    Dim fs As New FileStream(DownloadedFilePath, FileMode.Create)
        '    Dim ReadCount As Integer = stream.Read(buffer, 0, buffer.Length)
        '    While ReadCount > 0
        '        fs.Write(buffer, 0, ReadCount)
        '        ReadCount = stream.Read(buffer, 0, buffer.Length)
        '    End While
        '    ResponseDescription = response.StatusDescription
        '    fs.Close()
        '    stream.Close()
        'Catch e As Exception
        '    Console.WriteLine(e.Message)
        'End Try
        'Return ResponseDescription

        Try
            Dim url As String = ""

            Dim filename As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName

            '   url = Convert.ToString("https://support.finideas.com/contract/") & FileNameToDownload

            If FLGCSVCONTRACT = True Then
                url = Convert.ToString("https://support.finideas.com/contractcsv/") & FileNameToDownload
            Else
                url = Convert.ToString("https://support.finideas.com/contract/") & FileNameToDownload
            End If

            Dim client As New WebClient()
            Dim uri As New Uri(url)
            client.Headers.Add("Accept: text/html, application/xhtml+xml, */*")
            client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)")
            client.DownloadFileAsync(uri, filename)

            While client.IsBusy
                System.Threading.Thread.Sleep(1000)

            End While
            'Console.WriteLine(ex.Message);

            Return "success"
        Catch ex As UriFormatException
            Return "error"
        End Try

    End Function
    Public Sub AutoFeedBackMail()
        If IO.File.Exists(Application.StartupPath & "\MailInfo.txt") Then
            Dim FR As New IO.StreamReader(Application.StartupPath & "\MailInfo.txt")
            Dim Str1 As String = ""
            Str1 = FR.ReadLine()
            Dim Maildate1 As String = Str1.Substring("MailDate ::".Length)
            'Format(CDate(drow("Exp# Date")), "ddMMMyyyy")
            Dim Maildate As DateTime
            Try
                Maildate = Format(CDate(Str1.Substring("MailDate ::".Length)), "ddMMMyyyy")
            Catch ex As Exception
                Dim dt As String = DateTime.Today.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture)
                Maildate = dt 'Now.Date.ToString("dd-MMM-yyyy")
            End Try

            Str1 = FR.ReadLine()
            Dim MailFlag As String = Str1.Substring("MailFlag ::".Length)
            Str1 = FR.ReadLine()
            Dim ClientMail As String = Str1.Substring("ClientMail ::".Length)
            Dim wD As Int16 = DateDiff(DateInterval.Day, Maildate, Now.Date)
            Dim Str As String
            FR.Close()

            If wD >= 3 Then
                If MailFlag = "False" Then

                    Dim FWmail As New IO.StreamWriter(Application.StartupPath & "\MailInfo.txt")
                    FWmail.WriteLine("MailDate ::" & Now.Date.ToString("dd-MMM-yyyy"))
                    FWmail.WriteLine("MailFlag ::" & "True")
                    FWmail.WriteLine("ClientMail ::" & ClientMail)
                    FWmail.Close()
                    Str = "<!DOCTYPE HTML>" & vbCrLf
                    Str = Str & "<html lang='en-US'>" & vbCrLf
                    Str = Str & "<head>" & vbCrLf
                    Str = Str & "<meta charset='UTF-8'>" & vbCrLf
                    Str = Str & "<title></title>" & vbCrLf
                    Str = Str & "</head>" & vbCrLf
                    Str = Str & "<body>" & vbCrLf
                    Str = Str & "<p>Dear Sir, </p>" & vbCrLf

                    Str = Str & "<p>Greetings from FinIdeas !!!</p>" & vbCrLf

                    Str = Str & "<p>Please take a few moments to provide us with some important feedback about your experience with VolHedge Software. This information will be used to improve the application and efficiency of Software.</p>" & vbCrLf


                    Str = Str & "<p>Please <a href='https://docs.google.com/a/finideas.com/forms/d/10XqQhDI_EVloeJiVPG1N1pHlgtWpX4Qi992v2PQW3h8/viewform'><b><u>Click Here</a></b></u>  for your Valuable Feedback.</p>" & vbCrLf


                    Str = Str & "<p>Thanking You,</p>" & vbCrLf

                    Str = Str & "</body>" & vbCrLf
                    Str = Str & "</html>" & vbCrLf
                    send_email("Software@finideas.com", "Finideas123", ClientMail, "VolHedge FeedBack", Str)
                    MessageBox.Show("Please Check your Mail Id for FeedBack")
                End If
            End If
        End If


    End Sub

    Private Function DownloadContract() As Boolean

        Dim ObjImpData As New import_Data
        Dim ObjIO As New ImportData.ImportOperation
        Me.Cursor = Cursors.WaitCursor
        Dim ftpURL As String = "" '"ftp://strategybuilder.finideas.com/Contract"
        'If FLGCSVCONTRACT = True Then
        '    ftpURL = "ftp://strategybuilder.finideas.com/Contractcsv"
        'Else
        '    ftpURL = "ftp://strategybuilder.finideas.com/Contract"
        'End If
        If FLGCSVCONTRACT = True Then
            ftpURL = "https://support.finideas.com/Contractcsv"
        Else
            ftpURL = "https://support.finideas.com/Contract"
        End If
        'Host URL or address of the FTP serve
        Dim userName As String = "" ' "strategybuilder" '"FI-strategybuilder"
        'User Name of the FTP server
        Dim password As String = "" ' "finideas#123"
        'Password of the FTP server
        'string _ftpDirectory = "FinTesterCSV";          //The directory in FTP server where the files are present
        Dim FileNameToDownload As String = "Contract.zip"
        Dim tempDirPath As String = ""
        Dim StrContract As String = ""
        Dim StrContractfotxt As String = ""
        If FLGCSVCONTRACT = True Then
            tempDirPath = Application.StartupPath + "\Contractcsv\"
            If Not System.IO.Directory.Exists(Application.StartupPath + "\" + "Contractcsv\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\" + "Contractcsv")
            End If
            '-----Download file Fo---
            StrContract = Application.StartupPath & "\Contractcsv\contract.zip"
            StrContractfotxt = Application.StartupPath & "\Contractcsv\contract.csv"

        Else
            tempDirPath = Application.StartupPath + "\Contract\"
            If Not System.IO.Directory.Exists(Application.StartupPath + "\" + "Contract\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\" + "Contract")
            End If
            '-----Download file Fo---
            StrContract = Application.StartupPath & "\Contract\contract.zip"
            StrContractfotxt = Application.StartupPath & "\Contract\contract.txt"



        End If


        If File.Exists(StrContract) Then
            File.Delete(StrContract)
        End If
        If File.Exists(StrContractfotxt) Then
            File.Delete(StrContractfotxt)
        End If
        Dim status As String
        status = DownloadFile(ftpURL, FileNameToDownload, userName, password, tempDirPath)
        If status = "error" Then
            GoTo EQ
        End If

back:
        Threading.Thread.Sleep(1000)
        If File.Exists(StrContract) Then
            'txtpath.Text = "Zip Downloaded."
        Else
            GoTo back
        End If
back2:
        Threading.Thread.Sleep(1000)


        Try
            'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
        Catch ex As Exception
            GoTo back
        End Try
        Dim StrContracttxt As String = "" 'Application.StartupPath & "\Contract\contract.txt"
        If FLGCSVCONTRACT = True Then
            StrContracttxt = Application.StartupPath & "\Contract\contract.csv"
        Else
            StrContracttxt = Application.StartupPath & "\Contract\contract.txt"
        End If

        Dim info2 As New FileInfo(StrContracttxt)
        Dim length2 As Long = info2.Length
        Dim lineCnt As Integer = CUtils.GetCsvRowCount(StrContracttxt)

        If File.Exists(StrContracttxt) And length2 > 0 And lineCnt > 2 Then
            'txtpath.Text = StrContracttxt
            Dim bolfocon As Boolean
            flgimportContract = True
            bolfocon = import_Data.ContractImport(StrContracttxt, ObjIO, True)
            flgimportContract = False
            If bolfocon = True Then
                AutoFilemsg = AutoFilemsg + "Fo Contract Process Successfully"
            Else
                AutoFilemsg = AutoFilemsg + "Fo Contract Process Error"
            End If
            ' objimpmaster.fo(StrContracttxt)

        ElseIf length2 = 0 Or lineCnt < 3 Then
            MsgBox("Contract not process. ")
            GoTo EQ
        Else
            GoTo back2
        End If
EQ:
        '-----Download file Eq---

        If FLGCSVCONTRACT = True Then
            StrContracttxt = Application.StartupPath & "\Contractcsv\security.csv"
            FileNameToDownload = "security.zip"
            StrContract = Application.StartupPath & "\Contractcsv\security.zip"
        Else
            StrContracttxt = Application.StartupPath & "\Contract\security.txt"
            FileNameToDownload = "security.zip"
            StrContract = Application.StartupPath & "\Contract\security.zip"
        End If


        If File.Exists(StrContract) Then
            File.Delete(StrContract)
        End If
        If File.Exists(StrContracttxt) Then
            File.Delete(StrContracttxt)
        End If
        status = DownloadFile(ftpURL, FileNameToDownload, userName, password, tempDirPath)

        If status = "error" Then
            GoTo CURR
        End If

        Try

            'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
        Catch ex As Exception
            GoTo back
        End Try

        Dim length As Long = 0
        If File.Exists(StrContracttxt) Then
            Dim info As New FileInfo(StrContracttxt)
            length = info.Length
        End If

        lineCnt = CUtils.GetCsvRowCount(StrContracttxt)
        If length > 0 And lineCnt > 2 Then
            Dim bolEQcon As Boolean
            flgimportContract = True
            bolEQcon = import_Data.SecurityImport(StrContracttxt, ObjIO, True)
            flgimportContract = False
            If bolEQcon = True Then
                AutoFilemsg = AutoFilemsg + vbNewLine + "EQ Contract Process Successfully"
            Else
                AutoFilemsg = AutoFilemsg + vbNewLine + "EQ Contract Process Error.."
            End If
            'objimpmaster.EQ(StrContracttxt)
        ElseIf length = 0 Or lineCnt < 2 Then
            MsgBox("EQ Contract not process. ")
            GoTo CURR
        Else
            GoTo back2
        End If

        Me.Cursor = Cursors.Default

CURR:

        '-----Download file Curr---

        If FLGCSVCONTRACT = True Then
            StrContracttxt = Application.StartupPath & "\Contractcsv\cd_contract.csv"
            FileNameToDownload = "cd_contract.zip"
            StrContract = Application.StartupPath & "\Contractcsv\cd_contract.zip"
        Else
            StrContracttxt = Application.StartupPath & "\Contract\cd_contract.txt"
            FileNameToDownload = "cd_contract.zip"
            StrContract = Application.StartupPath & "\Contract\cd_contract.zip"
        End If


        If File.Exists(StrContract) Then
            File.Delete(StrContract)
        End If
        If File.Exists(StrContracttxt) Then
            File.Delete(StrContracttxt)
        End If
        status = DownloadFile(ftpURL, FileNameToDownload, userName, password, tempDirPath)
        If status = "error" Then
            Exit Function
        End If
        Try

            'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
        Catch ex As Exception
            GoTo back
        End Try
        'Dim FileNameToDownload1 As String = "csv_Data_Put_" + Fromdate1 + ".rar"

        Dim info1 As New FileInfo(StrContracttxt)
        Dim length1 As Long = info1.Length
        lineCnt = CUtils.GetCsvRowCount(StrContracttxt)
        If File.Exists(StrContracttxt) And length1 > 2 And lineCnt > 2 Then
            Dim bolcurrcon As Boolean
            flgimportContract = True
            bolcurrcon = import_Data.CurrencyImport(StrContracttxt, ObjIO, True)
            If bolcurrcon = True Then
                AutoFilemsg = AutoFilemsg + vbNewLine + "curr Contract Process Successfully"
            Else
                AutoFilemsg = AutoFilemsg + vbNewLine + "curr Contract Process Error"
            End If
            flgimportContract = False
            'objimpmaster.CURR(StrContracttxt)
        ElseIf length1 = 0 Or lineCnt < 2 Then
            MsgBox(" curr Contract not process. ")
            Exit Function
        Else
            GoTo back2
        End If
        fill_token()
        Me.Cursor = Cursors.Default
        Return True
    End Function
    '    Private Function DownloadContract()
    '        Dim ObjImpData As New import_Data
    '        Dim ObjIO As New ImportData.ImportOperation
    '        Me.Cursor = Cursors.WaitCursor
    '        Dim ftpURL As String = "https://support.finideas.com/Contract"
    '        'Host URL or address of the FTP serve
    '        Dim userName As String = "strategybuilder" '"FI-strategybuilder"
    '        'User Name of the FTP server
    '        Dim password As String = "finideas#123"
    '        'Password of the FTP server
    '        'string _ftpDirectory = "FinTesterCSV";          //The directory in FTP server where the files are present
    '        Dim FileNameToDownload As String = "Contract.zip"
    '        Dim tempDirPath As String = Application.StartupPath + "\Contract\"
    '        If Not System.IO.Directory.Exists(Application.StartupPath + "\" + "Contract\") Then
    '            System.IO.Directory.CreateDirectory(Application.StartupPath + "\" + "Contract")
    '        End If
    '        '-----Download file Fo---
    '        Dim StrContract As String = Application.StartupPath & "\Contract\contract.zip"
    '        Dim StrContractfotxt As String = Application.StartupPath & "\Contract\contract.txt"
    '        If File.Exists(StrContract) Then
    '            File.Delete(StrContract)
    '        End If
    '        If File.Exists(StrContractfotxt) Then
    '            File.Delete(StrContractfotxt)
    '        End If

    '        DownloadFile(ftpURL, FileNameToDownload, userName, password, tempDirPath)


    'back:
    '        Threading.Thread.Sleep(1000)
    '        If File.Exists(StrContract) Then
    '            'txtpath.Text = "Zip Downloaded."
    '        Else
    '            GoTo back
    '        End If
    'back2:
    '        Threading.Thread.Sleep(1000)


    '        Try
    '            'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
    '            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
    '        Catch ex As Exception
    '            GoTo back
    '        End Try
    '        Dim StrContracttxt As String = Application.StartupPath & "\Contract\contract.txt"
    '        If File.Exists(StrContracttxt) Then
    '            'txtpath.Text = StrContracttxt



    '            flgimportContract = True
    '            import_Data.ContractImport(StrContracttxt, ObjIO, True)
    '            flgimportContract = False


    '            ' objimpmaster.fo(StrContracttxt)
    '        Else
    '            GoTo back2
    '        End If

    '        '-----Download file Eq---
    '        StrContracttxt = Application.StartupPath & "\Contract\security.txt"
    '        FileNameToDownload = "security.zip"
    '        StrContract = Application.StartupPath & "\Contract\security.zip"
    '        If File.Exists(StrContract) Then
    '            File.Delete(StrContract)
    '        End If
    '        If File.Exists(StrContracttxt) Then
    '            File.Delete(StrContracttxt)
    '        End If
    '        DownloadFile(ftpURL, FileNameToDownload, userName, password, tempDirPath)



    '        Try

    '            'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
    '            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
    '        Catch ex As Exception
    '            GoTo back
    '        End Try

    '        If File.Exists(StrContracttxt) Then

    '            flgimportContract = True
    '            import_Data.SecurityImport(StrContracttxt, ObjIO, True)
    '            flgimportContract = False

    '            'objimpmaster.EQ(StrContracttxt)
    '        Else
    '            GoTo back2
    '        End If

    '        Me.Cursor = Cursors.Default



    '        '-----Download file Curr---
    '        StrContracttxt = Application.StartupPath & "\Contract\cd_contract.txt"
    '        FileNameToDownload = "cd_contract.zip"
    '        StrContract = Application.StartupPath & "\Contract\cd_contract.zip"
    '        If File.Exists(StrContract) Then
    '            File.Delete(StrContract)
    '        End If
    '        If File.Exists(StrContracttxt) Then
    '            File.Delete(StrContracttxt)
    '        End If
    '        DownloadFile(ftpURL, FileNameToDownload, userName, password, tempDirPath)

    '        Try

    '            'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
    '            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
    '        Catch ex As Exception
    '            GoTo back
    '        End Try
    '        'Dim FileNameToDownload1 As String = "csv_Data_Put_" + Fromdate1 + ".rar"
    '        If File.Exists(StrContracttxt) Then
    '            flgimportContract = True
    '            import_Data.CurrencyImport(StrContracttxt, ObjIO, True)
    '            flgimportContract = False
    '            'objimpmaster.CURR(StrContracttxt)
    '        Else
    '            GoTo back2
    '        End If
    '        fill_token()
    '        Me.Cursor = Cursors.Default

    '    End Function

    Public Sub CHECKCONTRACT()

        If Not CUtils.IsConnectedToInternet() Then
            MessageBox.Show("Internet Not Availabe")
            Return
        End If

        If CONTRACT_NOTIFICATION = 0 Then
            Exit Sub
        End If
        Dim scripttable As DataTable = New DataTable
        scripttable = cpfmaster
        scripttable.Select("") ' This Code Because of Speed-Up as Directed By Alpeshbhai (By Viral 4-aug-11)
        'farDate = scripttable.Compute("max(expdate1)", "expdate1 < #" & fDate(Maturity_Far_month) & "#")

        Dim csymbol As String = "NIFTY"
        Dim coptype As String = "XX"
        Dim ContractDate As Date

        'Dim scripttable As DataTable = New DataTable
        'scripttable = cpfmaster
        'scripttable.Select("") ' This Code Because of Speed-Up as Directed By Alpeshbhai (By Viral 4-aug-11)
        ''farDate = scripttable.Compute("max(expdate1)", "expdate1 < #" & fDate(Maturity_Far_month) & "#")

        'Dim csymbol As String = "NIFTY"
        'Dim coptype As String = "XX"
        'If IsDBNull(scripttable.Compute("min(expdate1)", "symbol='" & csymbol & "' And Option_Type='" & coptype & "'")) = True Then
        If IsDBNull(scripttable) = True Or scripttable.Rows.Count = 0 Then
            MsgBox("Downloading Contract..It will Take Some Time")
            DownloadContract()

            ''Thr_ContractImport = New Thread(AddressOf ImportContract)
            ''Thr_ContractImport.Start()
            'Dim dtfo As DataTable = MDI.GetContractConnection("FO")
            'Dim dtEQ As DataTable = MDI.GetContractConnect0ion("EQ")
            'Dim dtCURR As DataTable = MDI.GetContractConnection("CURR")

            'objMast.insert(dtfo)
            'objMast.Equity_insert(dtEQ)
            'objMast.Insert_Currency_Contract(dtCURR)
            'MessageBox.Show("Contract Import Successfully..")
        ElseIf (NetMode = "UDP") Then
            ContractDate = scripttable.Compute("min(expdate1)", "symbol='" & csymbol & "' And Option_Type='" & coptype & "'")

            If CDate(ContractDate) < CDate(Now.Date) Then

                If MsgBox("You Want To Update Latest Contract?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    Dim analysis1 As New import_master
                    analysis1.ShowDialog()
                End If
                Return
            End If

        Else


            'If Month(ContractDate) <> Month(Date.Now) Then

            ContractDate = scripttable.Compute("min(expdate1)", "symbol='" & csymbol & "' And Option_Type='" & coptype & "'")
            If CDate(ContractDate) < CDate(Now.Date) Then


                If MsgBox("You Want To Update Latest Contract?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    DownloadContract()
                    '    Dim dtfo As DataTable = MDI.GetContractConnection("FO")
                    '    Dim dtEQ As DataTable = MDI.GetContractConnection("EQ")
                    '    Dim dtCURR As DataTable = MDI.GetContractConnection("CURR")

                    '    objMast.insert(dtfo)
                    '    objMast.Equity_insert(dtEQ)
                    '    objMast.Insert_Currency_Contract(dtCURR)
                    MessageBox.Show("Contract Import Successfully..")
                End If
            End If
        End If

    End Sub

 Dim mBseExchange As CBseExchange
    Private Sub BseExchangeInit()
        clsGlobal.CreateBseExchange()
        mBseExchange = clsGlobal.mBseExchange
        Dim foIp As String = GdtSettings.Compute("max(SettingKey)", "SettingName='BSE_FO_UDP_IP'").ToString()
        Dim foPort As String = GdtSettings.Compute("max(SettingKey)", "SettingName='BSE_FO_UDP_PORT'").ToString()

        Dim eqIp As String = GdtSettings.Compute("max(SettingKey)", "SettingName='BSE_EQ_UDP_IP'").ToString()
        Dim eqPort As String = GdtSettings.Compute("max(SettingKey)", "SettingName='BSE_EQ_UDP_PORT'").ToString()

        mBseExchange.mIpEq = eqIp
        mBseExchange.mIpPortEq = eqPort
        mBseExchange.mIpFo = foIp
        mBseExchange.mIpPortFo = foPort
        mBseExchange.CreateBroadCast(clsGlobal.H_All_token_FO, clsGlobal.H_All_token_EQ, buyprice, saleprice)
    End Sub









    Public Sub NewSetUpContract()
        If Not IO.File.Exists(Application.StartupPath & "\NewSetup.txt") = True Then

            DownloadContract()
            Dim FSTimerLogFile As System.IO.StreamWriter
            FSTimerLogFile = New StreamWriter(Application.StartupPath & "\NewSetup.txt", True)
        End If
    End Sub

    'Private Function GetDriveSerialNumber() As String
    '    'Dim DriveSerial As Integer
    '    ''Create a FileSystemObject object
    '    'Dim fso As Object = CreateObject("Scripting.FileSystemObject")
    '    'Dim Drv As Object = fso.GetDrive(fso.GetDriveName(Application.StartupPath))
    '    'With Drv
    '    '    If .IsReady Then
    '    '        DriveSerial = .SerialNumber
    '    '    Else    '"Drive Not Ready!"
    '    '        DriveSerial = -1
    '    '    End If
    '    'End With
    '    'Return DriveSerial.ToString("X2")
    '    'Dim hd1 As New ManagementObjectSearcher("SELECT * FROM Win32_Processor")
    '    'For Each dvs As ManagementObject In hd1.Get()
    '    '    Dim serial As String = dvs("ProcessorSerialno").ToString()

    '    'Next
    '    Try


    '        Dim hd As New ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia")
    '        For Each dvs As ManagementObject In hd.Get()
    '            Dim serial As String = dvs("SerialNumber").ToString()

    '            Return serial.Trim
    '        Next
    '    Catch ex As Exception
    '        Return ""
    '    End Try
    'End Function
    'Private Function CpuId() As String
    '    'Dim computer As String
    '    'Dim wmi As Object
    '    'Dim processors As Object
    '    'Dim cpu As Object
    '    'Dim cpu_ids As String

    '    'computer = "."
    '    'wmi = GetObject("winmgmts:" & _
    '    '    "{impersonationLevel=impersonate}!\\" & _
    '    '    computer & "\root\cimv2")
    '    'processors = wmi.ExecQuery("Select * from " & _
    '    '    "Win32_Processor")

    '    'For Each cpu In processors
    '    '    cpu_ids = cpu_ids & ", " & cpu.ProcessorId
    '    'Next cpu
    '    'If Len(cpu_ids) > 0 Then cpu_ids = Mid$(cpu_ids, 3)

    '    'CpuId = cpu_ids

    '    Try
    '        Dim cpuInfo As String
    '        Dim managClass As New ManagementClass("win32_processor")
    '        Dim managCollec As ManagementObjectCollection = managClass.GetInstances()

    '        For Each managObj As ManagementObject In managCollec
    '            CpuId = managObj.Properties("processorID").Value.ToString()
    '            Exit For
    '        Next
    '    Catch ex As Exception
    '        Try
    '            Write_Log1("CpuId Error Uniqueid")
    '            'CpuId = GetProcessorSeialNumber(False) & ""
    '        Catch ex1 As Exception
    '            Write_Log1("Error in uniq id cpu")
    '            CpuId = "789456000"
    '        End Try

    '    End Try


    'End Function

    'Private Function getmbserial() As String
    '    REM Read Serial No. From Mother-Board
    '    Dim searcher As New ManagementObjectSearcher("SELECT  SerialNumber FROM Win32_BaseBoard")
    '    Dim information As ManagementObjectCollection = searcher.[Get]()
    '    For Each obj As ManagementObject In information
    '        For Each data As PropertyData In obj.Properties
    '            'Console.WriteLine("{0} = {1}", data.Name, data.Value)
    '            'Return data.Value
    '            Return data.Value
    '        Next
    '    Next
    '    REM End
    '    searcher.Dispose()
    'End Function
    'Private Function getmbserial() As String
    '    REM Read Serial No. From Mother-Board
    '    Dim searcher As New ManagementObjectSearcher("SELECT  SerialNumber FROM Win32_BaseBoard")
    '    Dim information As ManagementObjectCollection = searcher.[Get]()
    '    For Each obj As ManagementObject In information
    '        For Each data As PropertyData In obj.Properties
    '            'Console.WriteLine("{0} = {1}", data.Name, data.Value)
    '            'Return data.Value
    '            Return data.Value
    '        Next
    '    Next
    '    REM End
    '    searcher.Dispose()
    'End Function
    Private Function getmbserial() As String
        REM Read Serial No. From Motherboard
        Try
            ' Initialize the searcher for the Win32_BaseBoard class
            Dim searcher As New ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard")
            Dim information As ManagementObjectCollection = searcher.Get()

            ' Iterate through the returned collection
            For Each obj As ManagementObject In information
                ' Check if the SerialNumber property exists and retrieve its value
                If obj("SerialNumber") IsNot Nothing Then
                    Return obj("SerialNumber").ToString()
                End If
            Next

            ' Return a message if the serial number is not found
            Return "Serial Number Not Found"

        Catch ex As UnauthorizedAccessException
            ' Handle case where access is denied (user may not have the right permissions)
            Write_TimeLog1("getmbserial: Access Denied - " + ex.Message)
            Return "Access Denied"
        Catch ex As Exception
            ' Log any other exceptions that occur
            Write_TimeLog1("getmbserial: " + ex.Message)
            Return "Error"
        End Try
    End Function
    Public Sub MDI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CSqlConnection.CreateConStr(RegServerIP, RegServerUserid, RegServerpwd)

        Dim str As String = CHardwareFingerprint.GetHardwareFingerprint()

        BseExchangeInit()
        'Dim foExchange As String = clsGlobal.mContract.GetExchangeFromToken("1117307", ETokenType.Fo_Token)
        'Dim BseEqExchange As String = clsGlobal.mContract.GetExchangeFromToken("500325", ETokenType.Eq_Token)
        'Dim NseEqExchange As String = clsGlobal.mContract.GetExchangeFromToken("2885", ETokenType.Eq_Token)

        'Dim foScript As String = clsGlobal.mContract.GetScriptFromToken("1117307", ETokenType.Fo_Token)
        'Dim BseScript As String = clsGlobal.mContract.GetScriptFromToken("500325", ETokenType.Eq_Token)
        'Dim NseScript As String = clsGlobal.mContract.GetScriptFromToken("2885", ETokenType.Eq_Token)

        'foScript = clsGlobal.mContract.GetScriptExchangeFromToken("1117307", ETokenType.Fo_Token)
        'BseScript = clsGlobal.mContract.GetScriptExchangeFromToken("500325", ETokenType.Eq_Token)
        'NseScript = clsGlobal.mContract.GetScriptExchangeFromToken("2885", ETokenType.Eq_Token)

        'Dim scriptObj As script
        'scriptObj = clsGlobal.mContract.GetScriptObjectFromToken("1117307", ETokenType.Fo_Token)
        'scriptObj = clsGlobal.mContract.GetScriptObjectFromToken("500325", ETokenType.Eq_Token)

        ' Dim bsetrade As CBseTrades = New CBseTrades()
        '  bsetrade.FromGetsFOTEXT(New DataTable, "")
        '  bsetrade.FromGetsEQTEXT(New DataTable, "")

9:
        mPerf = New CPerfCheck()
        mPerf.SetFileName("MdiForm")
        If flgAPI_K = "TRUEDATA" And NetMode = "API" Then
            Me.Text = Me.Text.Replace("[B]", "[C]") ' " VolHedge[C] 5.0.0.3 F2L11F      "
        End If

        IMothrBoardSrNo = Trim(getmbserial()) & ""
        If IMothrBoardSrNo = " " Then
            Exit Sub
        End If
        'IHDDSrNoStr = LoadDiskInfo()
        'IHDDSrNoStr = GetDriveSerialNumber() & ""

        'Dim uprint As String = Getprint1()
        'Dim Dfinfo As String = GetDriveSerialNumber("C")
        'IProcessorSrNoStr = GetProcessorSeialNumber(False)

        user = SplashScreenMnUsrLic.txtUserName.Text
        password = SplashScreenMnUsrLic.txtPassword.Text

        'chkinternet.t = internerRefreshtime()
        If FLG_REG_SERVER_CONN = True Then
            chkAPI.Enabled = False
            chkudp.Enabled = False
            chkJL.Enabled = False
        End If
        'If NetMode = "TCP" Then
        '    If dtAccess Is Nothing = True Then
        '        dtAccess = trd.select_TCP_Connection
        '    End If
        '    Dim Thr_tcp_Connection As New Thread(AddressOf FillTCPConnectionToSqlTOAcess)
        '    Thr_tcp_Connection.Start()
        'End If

        '//SplashScreenUsrLic.Hide()   
        Dim ttik As Int64 = DateTime.Now.Ticks
        Write_TimeLog1("Start MdiLoad..")
        'clearIEhistory()

        SplashHide()


        loadFlag = True

        '=========================REM:Coding For Change Back Color of AUTO,TCP,UDP,INTERNET button  17/06/2014===============

        SAVEIMPORTSETTING()
        Read_picmargin()
        If NetMode <> "UDP" Then
            NewSetUpContract()
        End If

        Dim FileInfoFO As New FileInfo(Application.StartupPath & "\Contract\contract.txt")
        Dim FileInfoEQ As New FileInfo(Application.StartupPath & "\Contract\security.txt")

        ' Get the last modified date
        Dim lastModifiedDateFO As DateTime = FileInfoFO.LastWriteTime
        Dim lastModifiedDateEQ As DateTime = FileInfoEQ.LastWriteTime

        If NetMode <> "UDP" And NetMode <> "JL" Then

            If lastModifiedDateFO.Date <> DateTime.Today Or lastModifiedDateEQ.Date <> DateTime.Today Then
                If MsgBox("You Want To Update Latest Contract?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    DownloadContract()
                    'MsgBox("Contract Updated Successfully...")
                End If

            End If
        End If
        If clsGlobal.mSettingObj.GetValueBoolen(CSetting.mKeyCompactMdbAuto) = True Then
            CompactMdb()
        End If

        If gstr_ProductName <> "ThenThenThenOMI" Then
            'viral CHECKCONTRACT()
        End If

        'If clsGlobal.InternetVersionFlag = True Then
        '    chkudp.Enabled = False
        '    If NetMode = "UDP" Then
        '        chkudp.Enabled = False
        '        chkinternet_CheckedChanged(sender, e)
        '    End If
        'ElseIf NetMode = "TCP" Then
        '    chkinternet_CheckedChanged(sender, e)
        'End If

        If NetMode = "API" Then
            ConnectToServerToolStripMenuItem.Text = "Start Broadcast/Restart API"
        Else
            ConnectToServerToolStripMenuItem.Text = "Start Broadcast"
        End If


        If clsGlobal.InternetVersionFlag = True Then
            ' Dim trd1 As New trading
            AutoFeedBackMail()
            Dim dr As DataRow
            Dim ConnSetting As DataTable

            Dim setting, sname As String
            sname = "NETMODE"
            ConnSetting = GdtSettings
            dr = ConnSetting.Select("SettingName='" & sname & "'")(0)
            setting = dr("SettingKey")
            If clsGlobal.FlagTCP = 0 Then
                chktcp.Enabled = False
            Else
                chktcp.Enabled = True
            End If
            If clsGlobal.FlagTCP = 0 Then
                If (setting = "TCP") Then
                    '    CheckTelNet_Connection()

                    'dtAccess = trd1.select_TCP_Connection()
                    Dim settingname, settingkey, uid As String
                    settingname = "NETMODE"
                    settingkey = "NET"
                    settingname = "NETMODE"
                    uid = "190"
                    trd.Update_setting(settingkey, settingname, uid)
                    'MsgBox("setting sucessfully apply")
                    'Application.Restart()
                    Me.Dispose()
                    Application.Restart()
                    Dim plist As Process() = Process.GetProcesses()
                    For Each p As Process In plist
                        Try
                            If p.MainModule.ModuleName = "VolHedge.exe" Then
                                p.Kill()
                                Process.Start(Application.StartupPath + "\VolHedge.exe")
                            End If

                        Catch
                            'seems listing modules for some processes fails, so better ignore any exceptions here
                        End Try
                    Next

                End If
            End If


        End If
        If NetMode = "JL" Then
            CheckTelNet_Connection()
            If bool_IsTelNet = False Then
                MessageBox.Show("No JL Connection Available..Plz Try After Some time ")
                Dim analysis1 As New frmSettings
                For Each frm As Form In Me.MdiChildren
                    frm.Close()
                Next
                'analysis1.ShowDialog()
                analysis1.ShowForm(3)
            End If
            '   CheckJL_Connection()
        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
            CheckTelNet_Connection()
            If bool_IsTelNet = False Then
                MessageBox.Show("No  Connection Available..Plz Try After Some time ")
                Dim analysis1 As New frmSettings
                For Each frm As Form In Me.MdiChildren
                    frm.Close()
                Next
                'analysis1.ShowDialog()
                analysis1.ShowForm(3)
            End If
        End If

        comptable = trd.Comapany
        comptable_Net = trd.Comapany_Net


        If (NetMode = "TCP") Then
            chktcp.BackColor = Color.BlueViolet

            'dtAccess = trd.select_TCP_Connection()
            SaveTCPConnection()
        ElseIf (NetMode = "UDP") Then
            chkudp.BackColor = Color.BlueViolet
        ElseIf (NetMode = "API") Then
            chkAPI.BackColor = Color.BlueViolet
        ElseIf (NetMode = "JL") Then
            chkJL.BackColor = Color.BlueViolet
        Else
            chkinternet.BackColor = Color.BlueViolet
        End If
        '=========================REM:End===============



        ' Try
        'FSTimerLogFile = New StreamWriter("C:\VolHedgeErrorLog.txt", True)
        Write_ErrorLog3(Now)
        'FSTimerLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\VolHedgeErrorLog.txt", True)
        'FSTimerLogFile.WriteLine(Now)
        'Catch ex As Exception
        '    'FSTimerLogFile = New StreamWriter(Application.StartupPath & "\VolHedgeErrorLog.txt", True)
        'End Try


        'If SplashScreen1.version_control = 0 Then
        '    VarMDITitle = Me.Text
        '    Call sub_init_fo_broadcast_for_CheckExpiry()
        '    VarifyRef = AddressOf ChkVarifyTrailVersion
        '    Call TimerCheckExpiry_Tick(sender, e)
        'ElseIf SplashScreen1.version_control = 2 Then
        VarMDITitle = Me.Text + Microsoft.VisualBasic.Left(AppLicMode, 3) + " Full Version"
        If NetMode = "UDP" Then
            Call sub_init_fo_broadcast_for_CheckExpiry()
        End If
        VarifyRef = AddressOf ChkVarifyTrailVersion
        Call TimerCheckExpiry_Tick(sender, e)
        'End If


        'Dim analysis1 As New contact
        'analysis1.MdiParent = Me
        'analysis1.Label7.Visible = True 'trial version
        'analysis1.Show()

        ''Fill the Global datatable

        'Call GSub_Fill_GDt_AllTrades()
        'Call GSub_Fill_GDt_Strategy()
        '  wait(50)
        'Dim analysis As New analysis

        Me.Invoke(VarifyRef)

        Call Init_Gtbl_Summary_Analysis() 'initializaion of summary Global table
        'ToolStripcompanyCombo.Visible = False
        'ToolStripMenuSearchComp.Visible = False

        ToolStripcompanyCombo.Visible = True
        ToolStripMenuSearchComp.Visible = True

        'cmbcomp.DataSource = dv.ToTable(True, "Company")
        'cmbcomp.DisplayMember = "Company"
        'cmbcomp.ValueMember = "Company"

        If OPEN_ANALYSIS = 1 Then


            If NetMode = "UDP" Then
                'Me.Text = Me.Text & " [UDP]"
                analysis.MdiParent = Me
                analysis.Show()

            ElseIf NetMode = "TCP" Then
                'Me.Text = Me.Text & " [TCP]"
                If DA_SQL.IsValidConnection = True Then
                    analysis.MdiParent = Me
                    analysis.Show()

                Else
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
                    analysis1.ShowForm(3)
                End If
            ElseIf NetMode = "NET" Then
                analysis.MdiParent = Me
                analysis.Show()
            ElseIf NetMode = "API" Then
                analysis.MdiParent = Me
                analysis.Show()
            ElseIf NetMode = "JL" Then
                analysis.MdiParent = Me
                analysis.Show()
            End If

        End If

        SummaryF9ToolStripMenuItem.Enabled = True

        GVarMAXFOTradingOrderNo = Val(GdtFOTrades.Compute("MAX(orderno)", "").ToString)
        GVarMAXEQTradingOrderNo = Val(GdtEQTrades.Compute("MAX(orderno)", "").ToString)
        GVarMAXCURRTradingOrderNo = Val(GdtCurrencyTrades.Compute("MAX(orderno)", "").ToString)


        'Dim frmSplash As New SplashScreen1()
        'frmSplash.MdiParent = Me
        'frmSplash.Show()

        'obj = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MMM/dd/yyyy")
        'Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MMM/dd/yyyy")

        ''==========================================keval chakalasiya(15-2-2010)
        'wait(100)
        'Dim secur As security_setting
        'Dim version_control As Integer = 0
        'If version_control = 0 Then
        '    'version_control = 0 trial version
        '    ' Try
        '    If Now.Date >= CDate("06/30/2010") Then
        '        MsgBox("Please Contact Vendor, Trial Version Expired.", MsgBoxStyle.Exclamation)
        '        Application.Exit()
        '    End If
        '    Me.Cursor = Cursors.WaitCursor
        '    fill_token()
        '    Rounddata()
        '    init_datatable() 'initaialize all global datatable of analysis
        '    fill_trades()
        '    fill_equity_dtable()
        '    ' Dim analysis As New analysis
        '    If analysis.chkanalysis = False Then
        '        analysis.MdiParent = Me
        '        analysis.Show()
        '        Me.Cursor = Cursors.Default
        '    Else
        '        analysis.Dispose()
        '    End If
        '    Me.Cursor = Cursors.Default
        '    ''            Catch ex As Exception
        '    'Application.Exit()
        '    '           End Try
        'ElseIf version_control = 1 Then
        '    'version control = 1 full version
        '    ' Try

        '    secur = New security_setting

        '    secur.clientsetting()
        '    If secur.checking_client = False Then
        '        'MsgBox("Your trial period is over,Contact vendor for full version.")
        '        MsgBox("System.InvalidCastException: Conversion from string "" to type 'Integer' is not valid." & vbCr & "  at Microsoft.VisualBasic.CompilerServices.Conversions.ToInt(String Value)")
        '        Application.Exit()
        '    Else
        '        Me.Cursor = Cursors.WaitCursor

        '        fill_token()
        '        Rounddata()
        '        init_datatable() 'initaialize all global datatable of analysis
        '        fill_trades()
        '        fill_equity_dtable()
        '        Dim analysis123 As New analysis
        '        analysis123.MdiParent = Me
        '        analysis123.Show()

        '        'Dim analysis1 As New contact
        '        'analysis1.MdiParent = Me
        '        'analysis1.Show()
        '        Me.Cursor = Cursors.Default
        '    End If
        '    'Catch ex As Exception
        '    ' MsgBox(ex.ToString)
        '    'Application.Exit()
        '    'End Try

        'ElseIf version_control = 2 Then
        '    'version control = 2 client version

        '    Try
        '        '=================================================
        '        'coding for code.dll
        '        Dim mStream_reader As System.IO.StreamReader
        '        Dim Server_code As String
        '        If System.IO.File.Exists(Application.StartupPath & "\code.dll") Then
        '            mStream_reader = New System.IO.StreamReader(Application.StartupPath & "\code.dll")
        '            Server_code = mStream_reader.ReadLine
        '            mStream_reader.Close()

        '            If Client_encry.Client_encry.check_server_key(Client_encry.Client_encry.get_client_key("R8HUM1T3Q15R9L", getmbserial), Server_code) = False Then
        '                'MsgBox("Code not matched", MsgBoxStyle.Critical)
        '                ' MsgBox(Client_encry.Client_encry.get_client_key("R8HUM1T3Q15R9L", getmbserial))
        '                Dim authrization_form As New testmaster
        '                authrization_form.txtcompcode.Text = Client_encry.Client_encry.get_client_key("R8HUM1T3Q15R9L", getmbserial)
        '                authrization_form.ShowDialog()
        '                Application.Exit()
        '                Exit Sub
        '            End If
        '        Else
        '            'MsgBox(Client_encry.Client_encry.get_client_key("R8HUM1T3Q15R9L", getmbserial))
        '            Dim authrization_form As New testmaster
        '            authrization_form.txtcompcode.Text = Client_encry.Client_encry.get_client_key("R8HUM1T3Q15R9L", getmbserial)
        '            authrization_form.ShowDialog()
        '            'MsgBox("Code not matched", MsgBoxStyle.Critical)
        '            Application.Exit()
        '            Exit Sub
        '        End If
        '        'end of coding for code.dll

        '        If Now.Date >= CDate("03/11/2011") Then
        '            MsgBox("Please renew A.M.C.", MsgBoxStyle.Exclamation)
        '            'Application.Exit()
        '        End If

        '        Me.Cursor = Cursors.WaitCursor

        '        fill_token()
        '        Rounddata()
        '        init_datatable() 'initaialize all global datatable of analysis
        '        fill_trades()
        '        fill_equity_dtable()
        '        Dim analysis As New analysis
        '        analysis.MdiParent = Me
        '        analysis.Show()

        '        'Dim analysis1 As New contact
        '        'analysis1.MdiParent = Me
        '        'analysis1.Show()
        '        Me.Cursor = Cursors.Default
        '    Catch ex As Exception
        '        Application.Exit()
        '    End Try
        'End If
        ''frmSplash.Close()

        ''=================================================
        ''coding for code.dll
        'Dim mStream_reader As System.IO.StreamReader
        'Dim Server_code As String
        'If System.IO.File.Exists(Application.StartupPath & "\code.dll") Then
        '    mStream_reader = New System.IO.StreamReader(Application.StartupPath & "\code.dll")
        '    Server_code = mStream_reader.ReadLine
        '    mStream_reader.Close()

        '    If Client_encry.Client_encry.check_server_key(Client_encry.Client_encry.get_client_key("R8HUM1T3Q15R9L", getmbserial), Server_code) = False Then
        '        'MsgBox("Code not matched", MsgBoxStyle.Critical)
        '        MsgBox(Client_encry.Client_encry.get_client_key("R8HUM1T3Q15R9L", getmbserial))
        '        Application.Exit()
        '    End If
        'Else
        '    MsgBox(Client_encry.Client_encry.get_client_key("R8HUM1T3Q15R9L", getmbserial))
        '    'MsgBox("Code not matched", MsgBoxStyle.Critical)
        '    Application.Exit()
        'End If
        ''end of coding for code.dll

        'If Now.Date >= CDate("03/10/2011") Then
        '    MsgBox("Please renew A.M.C.", MsgBoxStyle.Exclamation)
        '    'Application.Exit()
        'End If
        ' '' ''obj = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MMM/dd/yyyy")
        ' '' ''Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MMM/dd/yyyy")
        '' '' ''threadcount = New Thread(AddressOf start_count)
        '' '' ''threadcount.Start()
        ' ''Dim secur As security_setting
        ' ''secur = New security_setting

        '' '' ''If ping_count() < 10 Then
        '' '' ''    openPort()


        '''''''''''''7 or 14 day trial version
        'If Now.Date >= CDate("03/16/2011") Then
        '    MsgBox("Please Contact Vendor, Trial Version Expired.", MsgBoxStyle.Exclamation)
        '    'Application.Exit()
        'End If
        ''secur.setting()
        ''If secur.checking = False Then
        ''    MsgBox("Your trial period is over,Contact vendor for full version.")
        ''    Application.Exit()
        ''Else
        ''**********************************
        'Me.Cursor = Cursors.WaitCursor
        'Dim analysis1 As New contact
        'analysis1.MdiParent = Me
        'analysis1.Label7.Visible = True 'trial version
        'analysis1.Show()
        'fill_token()
        'Rounddata()
        'Dim analysis As New analysis
        'analysis.MdiParent = Me
        'analysis.Show()
        'Me.Cursor = Cursors.Default
        ''***************************
        ''End If

        ''''''''''''''''''end of 14 day trial version

        ' '' '' ''     Else
        ' '' '' ''Application.Exit()
        ' '' '' ''End If

        '''''''''''''''''''for full version
        ''Dim secur As security_setting
        'secur = New security_setting

        'secur.clientsetting()
        'If secur.checking_client = False Then
        '    'MsgBox("Your trial period is over,Contact vendor for full version.")
        '    MsgBox("System.InvalidCastException: Conversion from string "" to type 'Integer' is not valid." & vbCr & "  at Microsoft.VisualBasic.CompilerServices.Conversions.ToInt(String Value)")
        '    Application.Exit()
        'Else
        '    Me.Cursor = Cursors.WaitCursor
        '    Dim analysis1 As New contact
        '    analysis1.MdiParent = Me
        '    analysis1.Show()
        '    fill_token()
        '    Rounddata()
        '    Dim analysis As New analysis
        '    analysis.MdiParent = Me
        '    analysis.Show()
        '    Me.Cursor = Cursors.Default
        'End If
        '''''''''''''''''end of full version



        ' '' '' ''coding for code.dll
        '' '' ''Dim mStream_reader As System.IO.StreamReader
        '' '' ''Dim Server_code As String
        '' '' ''If System.IO.File.Exists(Application.StartupPath & "\code.dll") Then
        '' '' ''    mStream_reader = New System.IO.StreamReader(Application.StartupPath & "\code.dll")
        '' '' ''    Server_code = mStream_reader.ReadLine
        '' '' ''    mStream_reader.Close()

        '' '' ''    If Client_encry.Client_encry.check_server_key(Client_encry.Client_encry.get_client_key("R8HUM1T3Q15R9L", getmbserial), Server_code) = False Then
        '' '' ''        'MsgBox("Code not matched", MsgBoxStyle.Critical)
        '' '' ''        MsgBox(Client_encry.Client_encry.get_client_key("R8HUM1T3Q15R9L", getmbserial))
        '' '' ''        Application.Exit()
        '' '' ''    End If
        '' '' ''Else
        '' '' ''    MsgBox(Client_encry.Client_encry.get_client_key("R8HUM1T3Q15R9L", getmbserial))
        '' '' ''    'MsgBox("Code not matched", MsgBoxStyle.Critical)
        '' '' ''    Application.Exit()
        '' '' ''End If
        ' '' '' ''end of coding for code.dll

        '' '' ''obj = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MMM/dd/yyyy")
        '' '' ''Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MMM/dd/yyyy")
        ' '' '' ''threadcount = New Thread(AddressOf start_count)
        ' '' '' ''threadcount.Start()
        '' '' ''Dim secur As security_setting
        '' '' ''secur = New security_setting

        '' '' ''If Now.Date >= CDate("02/01/2011") Then
        '' '' ''    MsgBox("Please renew A.M.C.", MsgBoxStyle.Exclamation)
        '' '' ''    'Application.Exit()
        '' '' ''End If

        '' '' ''Me.Cursor = Cursors.WaitCursor
        '' '' ''Dim analysis1 As New contact
        '' '' ''analysis1.MdiParent = Me
        '' '' ''analysis1.Show()
        '' '' ''fill_token()
        '' '' ''Rounddata()
        '' '' ''Dim analysis As New analysis
        '' '' ''analysis.MdiParent = Me
        '' '' ''analysis.Show()
        '' '' ''Me.Cursor = Cursors.Default






        '''''''''''''7 or 14 day trial version
        'If Now.Date > CDate("03/17/2010") Then
        '    MsgBox("Your trial period is over,Contact vendor for full version.")
        '    Application.Exit()
        'End If

        '' secur.setting()

        ''If secur.checking_already_used = False Then
        ''    MsgBox("Your trial period is over,Contact vendor for full version.")
        ''    Application.Exit()
        ''End If

        'Me.Cursor = Cursors.WaitCursor
        'Dim analysis1 As New contact
        'analysis1.MdiParent = Me
        'analysis1.Label7.Visible = True 'trial version
        'analysis1.Show()
        'fill_token()
        'Rounddata()
        'Dim analysis As New analysis
        'analysis.MdiParent = Me
        'analysis.Show()
        'Me.Cursor = Cursors.Default
        ''End If

        '''''''''''''''''''end of 14 day trial version

        ''     Else
        ''Application.Exit()
        ''End If

        '''''''''''''''''''for full version
        'Dim secur As security_setting
        'secur = New security_setting
        'secur.clientsetting()
        'If secur.checking_client = False Then
        '    'MsgBox("Your trial period is over,Contact vendor for full version.")
        '    MsgBox("System.InvalidCastException: Conversion from string "" to type 'Integer' is not valid." & vbCr & "  at Microsoft.VisualBasic.CompilerServices.Conversions.ToInt(String Value)")
        '    Application.Exit()
        'Else
        '    Me.Cursor = Cursors.WaitCursor
        '    Dim analysis1 As New contact
        '    analysis1.MdiParent = Me
        '    analysis1.Show()
        '    fill_token()
        '    Rounddata()
        '    Dim analysis As New analysis
        '    analysis.MdiParent = Me
        '    analysis.Show()
        '    Me.Cursor = Cursors.Default
        'End If
        ''''''''''''''''''end of full version

        '     Else
        'Application.Exit()
        'End If
        Write_TimeLog1("get internet connection start")

        ' labelDayDate.Text = Format(Now, "dddd") & ", " & Format(Now, "dd-MM-yyyy")

        REM This Code Execute While Server lic Applyed
        If ifNetModeTcp = True Then
            If NetMode = "TCP" Then
                Timer_Sql.Interval = Timer_Sql_Interval
                Timer_Sql.Enabled = True
                Timer_Sql.Start()
            ElseIf NetMode = "JL" Then
                Timer_Sql.Interval = Timer_Sql_Interval
                Timer_Sql.Enabled = True
                Timer_Sql.Start()
            ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
                If flgAPI_ExpCheck = True Then
                    Timer_Sql.Interval = Timer_Sql_Interval
                    Timer_Sql.Enabled = True
                    Timer_Sql.Start()
                End If


            ElseIf NetMode = "NET" Then
                Timer_Net.Interval = Timer_Net_Interval
                Timer_Net.Enabled = True
                Timer_Net.Start()
                Call Timer_Net_Tick(Timer_Net, New EventArgs)
            End If
        End If
        loadFlag = False
        If NetMode <> "UDP" And NetMode <> "JL" Then
            If IsInternetConnected() = True Then
                Write_TimeLog1("getdate connection")

                getdate()
                Write_TimeLog1("getdate connection finish")

            End If

        End If
        Write_TimeLog1("get internet connection finish")





        Dim Endtik As Long = System.Environment.TickCount
        '   Write_TimeLog("MdiLoad  End..(" & Endtik - starttik & ")", Endtik)
        Write_TimeLog1("End Fun-MdiLoad" + "|" + TimeSpan.FromTicks(DateTime.Now.Ticks - ttik).TotalSeconds.ToString())
    End Sub

    Private Sub CompactMdb()

        'Dim settObj As CSetting = clsGlobal.mSettingObj
        'Dim dttest As DataTable = settObj.Select_All_Settings()

        'Dim strSet As String = "TEST_SET"
        'Dim strVal As String = "INP_VAL"
        'Dim strRetVal As String = ""

        'strRetVal = settObj.Get_Value(strSet)
        'strRetVal = settObj.Get_Value(settObj.mCompactDate)
        'strRetVal = settObj.Get_Value(settObj.mCDate)
        'settObj.Set_Value(strSet, "test1")
        'strRetVal = settObj.Get_Value(strSet)
        'settObj.Set_Value(strSet, "test2")
        'strRetVal = settObj.Get_Value(strSet)
        'settObj.Set_Value(strSet, "test3")
        'strRetVal = settObj.Get_Value(strSet)

        Dim dblDays As Double = clsGlobal.mSettingObj.GetValueInt(CSetting.mKeyCompactMdbDays)
        If dblDays < 2 Then
            Return
        End If

        If COMPACTEDDATE.AddDays(dblDays) < Date.Now Then
            If MsgBox("Do you want to compact your database? This will improve Volhedge performance by deleting old, unused data.", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then

                Try
                    ' Initialize the trading object
                    Dim objTrad As trading = New trading()
                    ' Get the backup path from settings, or use default if not available
                    Dim mBackupPath As String = CStr(IIf(IsDBNull(GdtSettings.Compute("max(SettingKey)", "SettingName='Backup_path'")), "D:\", GdtSettings.Compute("max(SettingKey)", "SettingName='Backup_path'")))
                    Dim appStartupPath As String = System.Windows.Forms.Application.StartupPath()
                    ' Check if the backup path exists
                    If Not Directory.Exists(mBackupPath) Then
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
                    trd.SettingName = "COMPACTEDDATE"
                    trd.SettingKey = Date.Now.ToString("dd-MMM-yyyy")
                    trd.Uid = GdtSettings.Select("SettingName='COMPACTEDDATE'")(0).Item("Uid")
                    trd.Update_setting()
                Catch ex As Exception
                    MessageBox.Show("Compact Database Error..")
                End Try

            End If
        End If
    End Sub

    Private Sub GeneratMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub AnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnalysisF2ToolStripMenuItem.Click
        '   Dim analysis As New analysis
        If analysis.chkanalysis = False Then
            analysis.MdiParent = Me
            analysis.Show()
            SummaryF9ToolStripMenuItem.Enabled = True
            'Else
            '    analysis.Close()
            '    analysis.chkanalysis = False
            '    analysis.MdiParent = Me
            '  analysis.Show()
            'Else
            '    analysistest.Dispose()
        End If
    End Sub

    Private Sub ScenarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScenarioF3ToolStripMenuItem.Click
        Dim analysis1 As New scenario1
        If scenario1.chkscenario = False Then
            analysis1.MdiParent = Me
            'analysis1.Show()
            analysis1.ShowForm(False, "")
        Else
            analysis1.Dispose()
        End If
    End Sub


    Private Sub MarketWatchF4ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarketWatchF4ToolStripMenuItem.Click
        If NetMode = "API" Then


            For Each frm As Form In Me.MdiChildren
                frm.Close()
            Next

        End If
        'Dim mkt1 As New MarketWatch 'findvol

        'If MarketWatch.chkfindMkt = False Then
        '    mkt1.MdiParent = Me
        '    mkt1.Show()
        'Else
        '    mkt1.Dispose()
        'End If

        MarketWatch.MdiParent = Me
        MarketWatch.Show()
    End Sub

    Private Sub FilterBhavCopyF6ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterBhavCopyF6ToolStripMenuItem.Click
        Dim analysis1 As New bhavcopy
        If bhavcopy.chkbhavcopy = False Then
            analysis1.MdiParent = Me
            analysis1.Show()
        Else
            analysis1.Dispose()
        End If
    End Sub

    Private Sub SummaryF9ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryF9ToolStripMenuItem.Click
        If NetMode = "API" Then
            Dim dv As DataView
            dv = New DataView(maintable, "units<>0 and mdate>='" & CDate(CDate(Date.Now).ToString("dd/MMM/yyyy")) & "'", "", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count > 50 Then

            Else

                analysis.flgSummary = True
                alertmsg = False
                analysis.summary()
            End If
        Else
            analysis.flgSummary = True
            alertmsg = False
            analysis.summary()
        End If

        'Dim ac As New allcompany
        'ac.temptable = allcomp
        'ac.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub CalculatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim str As String
        str = "c:\WINDOWS\system32\calc.exe"
        Shell(str, AppWinStyle.NormalFocus)
    End Sub

    Private Sub PositionReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PositionReportToolStripMenuItem.Click
        Dim analysis1 As New rptposition
        If rptposition.chkrptposition = False Then
            analysis1.MdiParent = Me
            analysis1.Show()
        Else
            analysis1.Dispose()
        End If
    End Sub

    Private Sub MaturityReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaturityReportToolStripMenuItem.Click
        Dim analysis1 As New rptMaturity
        If rptMaturity.chkrptMaturity = False Then
            analysis1.MdiParent = Me
            analysis1.Show()
        Else
            analysis1.Dispose()
        End If

    End Sub

    Private Sub DayWiseExpenseReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DayWiseExpenseReportToolStripMenuItem.Click
        Dim analysis1 As New rptExpense
        analysis1.MdiParent = Me
        analysis1.Show()
    End Sub

    Private Sub DeleteDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteDataToolStripMenuItem.Click
        For Each Frm As Form In Me.MdiChildren
            Frm.Close()
        Next
        Dim analysis1 As New delete_data
        analysis1.ShowDialog()
    End Sub

    Private Sub PreviousPositionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviousPositionToolStripMenuItem.Click
        Dim analysis1 As New rptposition
        If rptposition.chkrptposition = False Then
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
            analysis1.MdiParent = Me
            analysis1.Show()
        Else
            analysis1.Dispose()
        End If
    End Sub

    Private Sub PreviousTradeFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviousTradeFileToolStripMenuItem.Click
        Dim analysis1 As New addprevious_datetrading
        'If addprevious_datetrading.chk = False Then
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
        'analysis1.MdiParent = Me
        'analysis1.Show()
        analysis1.ShowDialog()
        'Else
        'analysis1.Dispose()
        'End If
    End Sub

    Private Sub ContractSecurityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContractSecurityToolStripMenuItem.Click
        Dim analysis1 As New import_master
        'If import_master.chkrptposition = False Then
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
        'analysis1.MdiParent = Me
        'analysis1.Show()
        analysis1.ShowDialog()
        ' Else
        ' analysis1.Dispose()
        ' End If
    End Sub

    Private Sub BackUpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackUpToolStripMenuItem.Click
        Dim analysis1 As New frmbackup
        'If frmbackup.chkrptposition = False Then
        'analysis1.MdiParent = Me
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
        analysis1.ShowDialog()
        ' Else
        ' analysis1.Dispose()
        ' End If
    End Sub

    Private Sub ExposureMarginToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExposureMarginToolStripMenuItem.Click
        Dim analysis1 As New frm_exposure_margin_entry
        analysis1.ShowDialog()
    End Sub

    Private Sub TileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TileToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub


    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseAllToolStripMenuItem.Click
        For Each frm As Form In Me.MdiChildren
            frm.Close()
        Next
    End Sub

    Private Sub SettingsF10ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsF10ToolStripMenuItem.Click
        Dim analysis1 As New frmSettings

        For Each frm As Form In Me.MdiChildren
            frm.Close()
        Next

        'Dim i = Me.MdiChildren.Length - 1
        'While i >= 0
        '    'If UCase(Me.MdiChildren(i).Name) <> "CONTACT" Then

        '    Me.MdiChildren(i).Close()
        '    i -= 1
        '    'End If
        'End While

        analysis1.ShowDialog()
        analysis.searchcompany()
        SAVEIMPORTSETTING()
    End Sub

    Private Sub ContentsF1ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContentsF1ToolStripMenuItem.Click
        Dim frm As New ShortCut
        frm.ShowDialog()
    End Sub

    Private Sub ProftLossToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProftLossToolStripMenuItem.Click

    End Sub

    Private Sub ScriptwiseDaywiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScriptwiseDaywiseToolStripMenuItem.Click
        Dim analysis1 As New rptProfitLossScriptwise
        'If addprevious_datetrading.chk = False Then
        analysis1.MdiParent = Me
        analysis1.Show()
    End Sub

    Private Sub DaywiseScriptwiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DaywiseScriptwiseToolStripMenuItem.Click
        Dim analysis1 As New rptProfitLossDaywise
        'If addprevious_datetrading.chk = False Then
        analysis1.MdiParent = Me
        analysis1.Show()
    End Sub

    Private Sub SummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryToolStripMenuItem.Click
        Dim analysis1 As New rptProfitLossSummary
        'If addprevious_datetrading.chk = False Then
        analysis1.MdiParent = Me
        analysis1.Show()
    End Sub

    Private Sub OptionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionToolStripMenuItem.Click
        'Dim analysis1 As New optionfrm
        'If optionfrm.chkoptionfrm = False Then
        '    analysis1.MdiParent = Me
        '    analysis1.Show()
        'Else
        '    analysis1.Dispose()
        'End If
    End Sub

    Private Sub AlertEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlertEntryToolStripMenuItem.Click
        Dim analysis1 As New alert_entry
        'For i As Integer = 0 To Me.MdiChildren.Length - 1
        '    If UCase(Me.MdiChildren(i).Name) <> "CONTACT" Then
        '        Me.MdiChildren(i).WindowState = FormWindowState.Minimized
        '    End If
        'Next
        ' analysis1.MdiParent = Me
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
        analysis1.ShowDialog()
    End Sub

    ''' <summary>
    ''' this Menu Click Compnact the DataBase 
    ''' it decrease the DataBase Size
    ''' First this Code Copy Database and give name is db1.mdb
    ''' then Compact it
    ''' and delete actual Database
    ''' and this Compact DataBase move at that Actual Database location
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CompactDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompactDatabaseToolStripMenuItem.Click
        Try
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
            MsgBox("Database Compacted Successfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub AddNewStrategyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewStrategyToolStripMenuItem.Click
    '    Dim Frm As New FrmSelectStrategy
    '    Frm.btnOK.Text = "&Add"
    '    Frm.ShowDialog()
    'End Sub

    'Private Sub OpenModifyStrategyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenModifyStrategyToolStripMenuItem.Click
    '    Dim Frm As New FrmSelectStrategy
    '    Frm.btnOK.Text = "&Modify"
    '    Frm.ShowDialog()
    'End Sub

    'Private Sub DeleteStrategyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteStrategyToolStripMenuItem.Click
    '    Dim Frm As New FrmSelectStrategy
    '    Frm.btnOK.Text = "&Delete"
    '    Frm.ShowDialog()
    'End Sub

    'Private Sub StrategyAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StrategyAnalysisToolStripMenuItem.Click
    '    FrmStrategyTabAnalysis.MdiParent = Me
    '    FrmStrategyTabAnalysis.Show()
    'End Sub


    Private Sub CurrencyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CurrencyToolStripMenuItem.Click
        Dim frm As New frmCurrencyExposureMargin
        frm.ShowDialog()
    End Sub

    Private Sub ToolStripMenuSearchComp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuSearchComp.Click
        If comptable.Rows.Count <> 0 Then
            Dim dv As DataView = New DataView(comptable, "", "Company", DataViewRowState.CurrentRows)
            If dv.ToTable(True, "Company").Compute("count(Company)", "") > 0 Then
                If analysis.chkanalysis = True Or analysis.Visible = True Then

                    analysis.compname = ToolStripcompanyCombo.Text
                    analysis.tbcomp.SelectedTab = analysis.tbcomp.TabPages(analysis.compname)
                End If
            End If
        End If
    End Sub
    Private Sub ToolStripcompanyCombo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ToolStripcompanyCombo.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            Call ToolStripMenuSearchComp_Click(sender, e)
        End If
        'If e.KeyCode = Keys.Escape Then
        '    ToolStripcompanyCombo.Visible = False
        '    ToolStripMenuSearchComp.Visible = False
        'End If
    End Sub
#Region "Broadcast Time check Againest Expiry date"
    Dim VarBDateCounter As Short = 0
    Dim lzo_fo As New decompress.algorithm()


    Dim skt_multicastListener_fo As Sockets.Socket
    Public Sub sub_init_fo_broadcast_for_CheckExpiry()
        Try
            skt_multicastListener_fo = New Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
            skt_multicastListener_fo.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1)
            skt_multicastListener_fo.Bind(New IPEndPoint(IPAddress.Any, GdtSettings.Compute("max(SettingKey)", "SettingName='FO_UDP_Port'").ToString))
            skt_multicastListener_fo.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(IPAddress.Parse(GdtSettings.Compute("max(SettingKey)", "SettingName='FO_UDP_IP'").ToString), IPAddress.Any))
        Catch ex As Threading.ThreadAbortException
            Try
                Threading.Thread.ResetAbort()
            Catch ex12 As Exception
            End Try
        Catch x As Exception
            'MsgBox("Adminmodule::Gsub_initialize_fo_broadcast() :: " & x.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' sub_ReceiveMessages_fo
    ''' </summary>
    ''' <remarks>This function is used to recive messages of fo</remarks>
    Public Sub sub_ReceiveMessages_fo()
        Try
Recheck:

            If skt_multicastListener_fo Is Nothing Then
                Return
            End If
            Dim bteReceiveData_fo(512) As Byte
            skt_multicastListener_fo.Receive(bteReceiveData_fo)
            'MsgBox("Data Recived")
            Call sub_process_fo_data(bteReceiveData_fo)
            If VarCurrentDate > 0 Then
                'Me.Invoke(VarifyRef)

                'Call ChkVarifyTrailVersion()
                VarIsSetBroadcastDate = True
            Else
                GoTo Recheck

            End If
        Catch ex As Threading.ThreadAbortException
            Try
                Threading.Thread.ResetAbort()
            Catch ex12 As Exception
            End Try
        Catch EX1 As Exception
            'MsgBox("adminmodule :: Gsub_ReceiveMessages_fo()  ::" & EX1.Message)
        End Try
    End Sub

    ''' <summary>
    ''' process_fo_data
    ''' </summary>
    ''' <param name="temp_data">byte array</param>
    ''' <remarks>This function is used to process data which are recived from server</remarks>
    Public Sub sub_process_fo_data(ByVal temp_data() As Byte)
        'MsgBox("Msg1")
        Try
            Dim decompress_data() As Byte
            Dim compressed_length_old As Int16 = 0
            Dim compressed_length As Int16 = 0
            Dim packet_counter As Int16 = 0
            If temp_data(0) = 2 Then
                While packet_counter < IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 2))
                    'Debug.WriteLine(IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 2)))
                    Try
                        compressed_length = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 4 + compressed_length_old + (packet_counter * 2)))
                    Catch ex As Exception
                        'MsgBox("Gsub_Process_fo_data  :: " & ex.Message)
                    End Try
                    If compressed_length > 0 And compressed_length <= 511 Then
                        Dim compressed_data(compressed_length - 1) As Byte
                        Try
                            Array.Copy(temp_data, 6 + compressed_length_old + (packet_counter * 2), compressed_data, 0, compressed_length)
                        Catch ex As Exception
                            Exit While
                        End Try
                        Try
                            decompress_data = lzo_fo.Decompress(compressed_data)
                            If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(decompress_data, 18)) = 7208 Then
                                Call sub_process_fo_7208(decompress_data, True)
                            End If
                        Catch ex As Exception
                            'MsgBox("AdminModule :: Gsub_process_fo_data ::" & ex.ToString)
                            Exit While
                        End Try
                    ElseIf compressed_length = 0 Then
                        If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 7208 Then
                            Call sub_process_fo_7208(temp_data, False)
                        End If
                    End If
                    compressed_length_old += compressed_length
                    packet_counter += 1
                End While
            End If
        Catch ex As Threading.ThreadAbortException
            Try
                Threading.Thread.ResetAbort()
            Catch ex12 As Exception
            End Try
        End Try
    End Sub

    ''' <summary>
    ''' sub_process_fo_7208
    ''' </summary>
    ''' <param name="data">byte array</param>
    ''' <remarks>This function is used to process data</remarks>
    Private Sub sub_process_fo_7208(ByVal data() As Byte, ByVal flgiscompress As Boolean)
        Dim token_no As Long
        Dim pktinc As Integer
        If flgiscompress = True Then
            pktinc = 0
        Else
            pktinc = 6
        End If
        For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 48 + pktinc)) - 1) * 214 Step 214
            token_no = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 50 + j + pktinc))
            If futtoken.Contains(token_no) Then
                Try
                    If VarCurrentDate < IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 76 + j + pktinc)) Then
                        VarCurrentDate = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 76 + j + pktinc))
                    End If
                    'Exit For
                Catch ex As Threading.ThreadAbortException
                    Try
                        Threading.Thread.ResetAbort()
                    Catch ex12 As Exception
                    End Try
                End Try
            End If
        Next
        'analysis.lblmcount.Invoke(mtest)
    End Sub

    Public Sub ChkVarifyTrailVersion()
        Dim BDate As Date
        'If NetMode = "TCP" Or NetMode = "UDP" Then
        If VarCurrentDate > 0 Then
            BDate = DateAdd(DateInterval.Second, VarCurrentDate, CDate("1-1-1980"))
        Else
            BDate = Now
        End If
        'ElseIf NetMode = "NET" Then
        '    BDate = Now
        'End If


        'If Format(BDate, "dd-MMM-yyyy").ToUpper = "02-NOV-2010" Then
        '    MsgBox(VarBCurrentDate)
        'End If

        'If G_VarIsTrailVersion = True Then
        'If SplashScreen1.version_control = 0 Then
        '    Me.Text = VarMDITitle & "  [" & Format(BDate, "dd-MMM-yyyy") & "] " & " Expire on " & Format(G_VarExpiryDate, "dd-MMM-yyyy") & ""
        'ElseIf SplashScreen1.version_control = 2 Then
        If flgAPI Then

            Me.Text = VarMDITitle & "  [" & Format(BDate, "dd-MMM-yyyy") & "]      API Exp:[" & flgAPI_Exp.ToString("dd-MMM-yyyy") & "] "
            Me.Text = Me.Text + "(Data Powered by TRUEDATA)"
        Else

            Me.Text = VarMDITitle & "  [" & Format(BDate, "dd-MMM-yyyy") & "] "
        End If

        'End If

        If BDate >= G_VarExpiryDate Then
            analysis.MdiParent = Me
            analysis.Dispose()
            Call clsGlobal.Sub_Get_Version_TextFile()
            MsgBox("Please Contact Vendor, Trial Version Expired.", MsgBoxStyle.Exclamation)
            MenuStrip1.Enabled = False
            Application.Exit()
            '  ToolStrip1.Enabled = False
        Else
            If G_VarCurrBDate <> BDate Then
                G_VarCurrBDate = BDate.Date
                Call DisplayTrailDayRemainMessage()
                If NetMode = "API" Then
                    DisplayTrailDayRemainMessageAPI()
                End If

            End If

            MenuStrip1.Enabled = True
            ' ToolStrip1.Enabled = True
        End If
        'End If
    End Sub
    Public Sub Payamentpage_live(ByVal IMothrBoardSrNo As String, ByVal IHDDSrNoStr As String, ByVal IProcessorSrNoStr As String, ByVal user As String, ByVal password As String, ByVal gstr_ProductName As String)




        Dim StrMsg As String = "Do You Want to purchase?"
        Dim strresult As Integer = MsgBox(StrMsg, MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Volhedge Demo Update.")
        Try

            If strresult = DialogResult.Yes Then
                Try
                    System.Diagnostics.Process.Start("https://fintester.finideas.com/VolHedgePayment.aspx?M='" + IMothrBoardSrNo + "'&H='" + IHDDSrNoStr + "'&P='" + IProcessorSrNoStr + "'&User='" + user + "'&password='" + password + "'&Pname='" + gstr_ProductName + "'")
                Catch ex As Exception
                End Try


            Else
                Call clsGlobal.Sub_Get_Version_TextFile()


            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub DisplayTrailDayRemainMessage()
        Dim VarDays As Integer = DateDiff(DateInterval.Day, G_VarCurrBDate, G_VarExpiryDate)
        If VarDays <= 5 Then
            MsgBox("Your VolHedge version will expire in " & vbCrLf & "" & VarDays & " days.", MsgBoxStyle.Information)
            'MsgBox(VarDays & " days to expiry", MsgBoxStyle.Information)
            'MsgBox(VarDays & " days remaining to trail version expire  !!", MsgBoxStyle.Information)
        End If
        If VarDays <= 2 Then
            Payamentpage_live(IMothrBoardSrNo, IHDDSrNoStr, IProcessorSrNoStr, user, password, gstr_ProductName)
        End If
    End Sub
    Private Sub DisplayTrailDayRemainMessageAPI()
        Dim VarDays As Integer = DateDiff(DateInterval.Day, G_VarCurrBDate, flgAPI_Exp)
        If VarDays <= 0 Then
            Exit Sub
        End If
        If VarDays <= 5 Then

            MsgBox("Your API data subscription will expire in " & vbCrLf & "" & VarDays & " days.", MsgBoxStyle.Information)
            'MsgBox(VarDays & " days to expiry", MsgBoxStyle.Information)
            'MsgBox(VarDays & " days remaining to trail version expire  !!", MsgBoxStyle.Informati  on)
        End If
        If VarDays <= 2 Then
            Payamentpage_live(IMothrBoardSrNo, IHDDSrNoStr, IProcessorSrNoStr, user, password, gstr_ProductName)
        End If
    End Sub
#End Region
    Dim VarIsSetBroadcastDate As Boolean = False
    Private Sub TimerCheckExpiry_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCheckExpiry.Tick
        VarBDateCounter += 1

        If NetMode = "UDP" Then
            Dim Thr As New Thread(AddressOf sub_ReceiveMessages_fo)
            Thr.Name = "ThrReceiveFoUDP"
            Thr.Start()
        ElseIf NetMode = "TCP" Or NetMode = "NET" Then
            If NetMode = "NET" Then
                VarCurrentDate = DateDiff(DateInterval.Second, CDate("1-1-1980"), DateTime.Now)
            End If
            'Me.Invoke(VarifyRef)
        End If

        If VarBDateCounter = 2 Then
            'Call Win32SetSystemTime(DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")))
            'If NetMode = "UDP" Then
            TimerCheckExpiry.Enabled = False
            'End If
        End If
    End Sub

    Private Sub ToolStripcompanyCombo_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripcompanyCombo.MouseHover
        ToolStripcompanyCombo.ToolTipText = "Search Company (Ctrl + T)"
    End Sub

    Private Sub ToolStripcompanyCombo_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ToolStripcompanyCombo.MouseMove
        ToolStripcompanyCombo.ToolTipText = "Search Company (Ctrl + T)"
    End Sub

    Private Sub CalculatorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalculatorsToolStripMenuItem.Click
        Dim str As String
        str = "c:\WINDOWS\system32\calc.exe"
        Shell(str, AppWinStyle.NormalFocus)
    End Sub
    Dim Obj_Del As New deletedata
    Dim Obj_Scenario As New scenarioDetail
    Dim Obj_Trad As New trading
    Public GTempDTPosition As New DataTable
    Public GTempPreBal As New DataTable
    Dim ArrTMPDate As New ArrayList
    'Comment By Alpesh On 13/04/2011 Because DeleteTodayPositionToolStripMenuItem not in ToolStripMenuItem
    ' ''Private Sub DeleteTodayPositionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteTodayPositionToolStripMenuItem.Click
    ' ''    Try
    ' ''        For Each Open_Frm As Form In Me.MdiChildren
    ' ''            Open_Frm.Close()
    ' ''        Next

    ' ''        Select Case (MsgBox("Are You Sure To Reoptimize Position?", MsgBoxStyle.YesNo + MsgBoxStyle.Question))
    ' ''            Case MsgBoxResult.Yes
    ' ''                Me.Cursor = Cursors.WaitCursor
    ' ''                Obj_Del.Delete_todaydata()
    ' ''                Obj_Trad.Delete_prBal(Now.Date)

    ' ''                GTempPreBal = Obj_Trad.prebal
    ' ''                GTempDTPosition = maintable

    ' ''                REM Delete All Trades
    ' ''                Obj_Del.Insert()
    ' ''                GdtFOTrades.Clear()
    ' ''                GdtEQTrades.Clear()
    ' ''                GdtCurrencyTrades.Rows.Clear()
    ' ''                comptable.Rows.Clear()
    ' ''                hashOrder.Clear()

    ' ''                REM Fill In Trading Table
    ' ''                Call GSub_Fill_TradingTables(GTempDTPosition)
    ' ''                REM Fill Previous Balance
    ' ''                Obj_Trad.insert_prebal(GTempPreBal)

    ' ''                maintable = GTempDTPosition
    ' ''                prebal = GTempPreBal
    ' ''                Try
    ' ''                    Call proc_data(False)
    ' ''                Catch ex As Exception
    ' ''                    MsgBox(ex.Message, MsgBoxStyle.Exclamation)
    ' ''                End Try
    ' ''                MsgBox("Position Optimized Successfully", MsgBoxStyle.Information)
    ' ''                Me.Cursor = Cursors.Default
    ' ''            Case MsgBoxResult.No
    ' ''                Exit Sub
    ' ''        End Select
    ' ''    Catch ex As Exception
    ' ''        MsgBox(ex.ToString)
    ' ''    End Try
    ' ''End Sub
    Private Sub proc_data(ByVal istimer As Boolean)
        'for checking if any new trades are inserted
        'check the length of the order hash table
        VarImportInserted = False
        Dim len As Integer = hashOrder.Keys.Count


        'Dim chk1 As Boolean
        DTAllTableMerge.Rows.Clear()
        ''divyesh
        Dim G_DTImportSetting As New DataTable
        Dim objTradManual As New trading
        Dim DTTMPManualFOImportDate As New DataTable
        Dim DTTMPManualEQImportDate As New DataTable
        Dim DTTMPManualCurrencyDate As New DataTable
        Dim StrImportFlag As String = ""
        G_DTImportSetting = G_DTImportSetting 'Obj_Trad.Select_Import_Setting()
        If istimer = True Then
            Dim DtImportSetting As DataTable = New DataView(G_DTImportSetting, "Auto_Import=TRUE", "", DataViewRowState.CurrentRows).ToTable
            For Each dr As DataRow In DtImportSetting.Rows
                Dim VarFileNameFormat As String
                VarFileNameFormat = dr("FileName_Format").Split(".")(0)
                Dim VarFilePath As String
                Select Case dr("Text_Type").ToString
                    Case "NEAT FO Trade File"
                        VarFilePath = dr("File_Path") & "\Trade" & dr("File_Code") & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromNeatFOTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "GETS FO Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromGetsFOTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "GETS EQ Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromGETSEqTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "ODIN FO Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromODINFOTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "ODIN EQ Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromODINEqTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NOW FO Trade File"
                        Dim VarStr1 As String = VarFileNameFormat.Substring(0, VarFileNameFormat.Length - 14)
                        Dim VarStr2 As String = VarFileNameFormat.Substring(VarFileNameFormat.Length - 14)
                        VarFilePath = dr("File_Path") & "\" & VarStr1 & Format(Today, VarStr2) & ".txt"
                        Call proc_data_FromNowFOTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NOW EQ Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromNowEQTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NOTICE FO Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNotisFOTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NOTICE EQ Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNotisEQTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NSE FO Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNSEFOTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NSE EQ Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNSEEqTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NEAT CURRENCY Trade File"
                        VarFilePath = dr("File_Path") & "\Trade" & dr("File_Code") & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromNeatCurrTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "GETS CURRENCY Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromGetsCurrTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "ODIN CURRENCY Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromODINCurrTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NOW CURRENCY Trade File"
                        Dim VarStr1 As String = VarFileNameFormat.Substring(0, VarFileNameFormat.Length - 14)
                        Dim VarStr2 As String = VarFileNameFormat.Substring(VarFileNameFormat.Length - 14)
                        VarFilePath = dr("File_Path") & "\" & VarStr1 & Format(Today, VarStr2) & ".txt"
                        Call proc_data_FromNowCurrTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NOTICE CURRENCY Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNotisCurrTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NSE CURRENCY Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNSECurrTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "" ' SQL Server Database Type Selected Then
                        Select Case dr("Server_Type").ToString
                            Case "GETS SQL Server Database"
                                Call proc_data_FromGETSdb(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, objTradManual, dr("Server_Name").ToString, dr("Database_Name").ToString, dr("User_Name").ToString, dr("Pwd").ToString, dr("Table_Name").ToString)
                            Case "ODIN SQL Server Database"
                                Call proc_data_FromODINdb(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, objTradManual, dr("Server_Name").ToString, dr("Database_Name").ToString, dr("User_Name").ToString, dr("Pwd").ToString, dr("Table_Name").ToString)
                        End Select
                End Select
            Next
        Else
            Dim DtImportSetting As DataTable = New DataView(G_DTImportSetting, "Manual_Import=TRUE", "", DataViewRowState.CurrentRows).ToTable
            For Each dr As DataRow In DtImportSetting.Rows
                Dim VarFileNameFormat As String
                VarFileNameFormat = dr("FileName_Format").Split(".")(0)
                Dim VarFilePath As String
                Select Case dr("Text_Type").ToString
                    Case "NEAT FO Trade File"
                        VarFilePath = dr("File_Path") & "\Trade" & dr("File_Code") & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromNeatFOTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "GETS FO Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromGetsFOTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "GETS EQ Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromGETSEqTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "ODIN FO Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromODINFOTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "ODIN EQ Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromODINEqTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NOW FO Trade File"
                        Dim VarStr1 As String = VarFileNameFormat.Substring(0, VarFileNameFormat.Length - 14)
                        Dim VarStr2 As String = VarFileNameFormat.Substring(VarFileNameFormat.Length - 14)
                        VarFilePath = dr("File_Path") & "\" & VarStr1 & Format(Today, VarStr2) & ".txt"
                        Call proc_data_FromNowFOTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NOW EQ Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromNowEQTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NOTICE FO Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNotisFOTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NOTICE EQ Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNotisEQTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NSE FO Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNSEFOTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NSE EQ Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNSEEqTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NSE EQ Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNSEEqTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NEAT CURRENCY Trade File"
                        VarFilePath = dr("File_Path") & "\Trade" & dr("File_Code") & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromNeatCurrTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "GETS CURRENCY Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromGetsCurrTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "ODIN CURRENCY Trade File"
                        VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                        Call proc_data_FromODINCurrTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NOW CURRENCY Trade File"
                        Dim VarStr1 As String = VarFileNameFormat.Substring(0, VarFileNameFormat.Length - 14)
                        Dim VarStr2 As String = VarFileNameFormat.Substring(VarFileNameFormat.Length - 14)
                        VarFilePath = dr("File_Path") & "\" & VarStr1 & Format(Today, VarStr2) & ".txt"
                        Call proc_data_FromNowCurrTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NOTICE CURRENCY Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNotisCurrTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "NSE CURRENCY Trade File"
                        VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                        Call proc_data_FromNSECurrTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                    Case "" ' SQL Server Database Type Selected Then
                        Select Case dr("Server_Type").ToString
                            Case "GETS SQL Server Database"
                                Call proc_data_FromGETSdb(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, objTradManual, dr("Server_Name").ToString, dr("Database_Name").ToString, dr("User_Name").ToString, dr("Pwd").ToString, dr("Table_Name").ToString)
                            Case "ODIN SQL Server Database"
                                Call proc_data_FromODINdb(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, objTradManual, dr("Server_Name").ToString, dr("Database_Name").ToString, dr("User_Name").ToString, dr("Pwd").ToString, dr("Table_Name").ToString)
                        End Select
                End Select
            Next
        End If


        If DTTMPManualFOImportDate.Rows.Count = 0 And DTTMPManualEQImportDate.Rows.Count = 0 And DTTMPManualCurrencyDate.Rows.Count = 0 Then
            Me.Cursor = Cursors.Default
            Exit Sub
        Else

            If DTTMPManualFOImportDate.Rows.Count > 0 Then
                DTAllTableMerge.Merge(DTTMPManualFOImportDate)
            End If
            If DTTMPManualEQImportDate.Rows.Count > 0 Then
                DTAllTableMerge.Merge(DTTMPManualEQImportDate)
            End If
            If DTTMPManualCurrencyDate.Rows.Count > 0 Then
                DTAllTableMerge.Merge(DTTMPManualCurrencyDate)
            End If

            DTAllTableMerge.AcceptChanges()
            Write_ErrorLog3(Now.ToString() & "-" & "No. of Trades :" & DTAllTableMerge.Rows.Count)
            'FSTimerLogFile.WriteLine(Now.ToString() & "-" & "No. of Trades :" & DTAllTableMerge.Rows.Count)

            Dim VarDTDate As Date = DTAllTableMerge.Rows(0)("entrydate")

            REM Delete previous balance on company name and date. 19-11-2010 by Divyesh
            Dim ti2 As Long = System.Environment.TickCount
            Dim companyname As DataTable
            companyname = DTAllTableMerge.DefaultView.ToTable(True, "company")
            Dim ArrTMPDate As New ArrayList
            For Each Dr As DataRow In DTAllTableMerge.Rows
                If ArrTMPDate.Contains(Format(Dr("entrydate"), "dd-MMM-yyyy")) = False Then
                    ArrTMPDate.Add(Format(Dr("entrydate"), "dd-MMM-yyyy"))
                End If
            Next
            For Each dr As DataRow In companyname.Rows
                REM Add Company row into Company Table
                If comptable.Select("Company='" & dr("company") & "'").Length = 0 Then
                    comptable.Rows.Add(dr("company"))
                End If
                REM End
                For i1 As Integer = 0 To ArrTMPDate.Count - 1
                    Obj_Trad.Delete_prBal(CDate(ArrTMPDate(i1)), dr("company"))
                Next
            Next
            For i1 As Integer = 0 To ArrTMPDate.Count - 1
                Call cal_prebal_Outdated(CDate(ArrTMPDate(i1)), companyname)
            Next
            'prebal = Obj_Trad.prebal
            'FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Previous Balance Delete and Recalulate :(" & System.Environment.TickCount - ti2 & ")")
            Write_ErrorLog3(Now.ToString() & "-" & "Previous Balance Delete and Recalulate :(" & System.Environment.TickCount - ti2 & ")")
        End If


        'prebal = objTrad.prebal

        'Dim sort As DataGridViewColumn = DGTrading.SortedColumn
        'Dim sortindex As Integer
        'If Not IsNothing(sort) Then
        '    sortindex = sort.Index
        'Else
        '    sortindex = 0
        'End If

        'Dim sortorder As SortOrder
        'If Not sort Is Nothing Then
        '    sortorder = DGTrading.SortedColumn.HeaderCell.SortGlyphDirection
        'End If

        '' If chk1 = True Then
        'If VarImportInserted = True Or VarFileImport = True Then
        '    Dim selectab As String = compname
        '    Dim selectindex As Integer = ind1

        '    Dim i As Integer
        '    i = 0
        '    'tbcomp.TabPages.Clear()
        '    comptable = objTrad.Comapany
        '    compname = ""
        '    ''divyesh
        '    Call AddRemoveTab(comptable)
        '    If tbcomp.TabPages.ContainsKey("NIFTY") = True Then
        '        compname = "NIFTY"
        '        chknifty = True
        '        tbcomp.SelectedTab = tbcomp.TabPages(compname)
        '    Else
        '        If (comptable.Rows.Count <> 0) Then
        '            compname = comptable.Rows(0).Item("company")
        '        End If
        '    End If

        '        'chknifty = False
        '        'For Each drow As DataRow In comptable.Rows
        '        '    tbcomp.TabPages.Add(drow("company"))
        '        '    tbcomp.TabPages.Item(i).Name = drow("company")
        '        '    If UCase(drow("company")) = "NIFTY" Then
        '        '        chknifty = True
        '        '        ind = i
        '        '    End If
        '        '    i += 1
        '        'Next


        '        ''comptable = objTrad.Comapany
        '        ''Call AddRemoveTab(comptable)

        '        objAna.process_data()
        '        ''divyesh
        '        Call fill_equity_dtable() REM maintable Fill From Database

        '        ' ''If (GdtFOTrades.Rows.Count > 0 Or GdtEQTrades.Rows.Count > 0) And tbcomp.TabPages.Count > 0 Then
        '        ' ''    If selectab <> "" Then
        '        ' ''        Dim i1 As Integer = 0
        '        ' ''        For Each tab As TabPage In tbcomp.TabPages
        '        ' ''            If UCase(tab.Name) = UCase(selectab) Then
        '        ' ''                selectindex = i1
        '        ' ''                tab.Select()
        '        ' ''            End If
        '        ' ''            i1 += 1
        '        ' ''        Next
        '        ' ''        compname = selectab
        '        ' ''        tbcomp.SelectedIndex = selectindex
        '        ' ''        tbcomp.TabPages(selectindex).Select()
        '        ' ''        ind1 = selectindex
        '        ' ''    Else
        '        ' ''        If chknifty = True Then
        '        ' ''            compname = "NIFTY"
        '        ' ''            tbcomp.SelectedIndex = ind
        '        ' ''        Else
        '        ' ''            If tbcomp.TabCount > 0 Then compname = tbcomp.SelectedTab.Text
        '        ' ''        End If
        '        ' ''    End If
        '        ' ''Else
        '        ' ''    If chknifty = True Then
        '        ' ''        compname = "NIFTY"
        '        ' ''        tbcomp.SelectedIndex = ind
        '        ' ''    Else
        '        ' ''        If tbcomp.TabCount > 0 Then compname = tbcomp.SelectedTab.Text
        '        ' ''    End If
        '        ' ''End If
        '        ' ''If compname = "" Then
        '        ' ''    If chknifty = True Then
        '        ' ''        compname = "NIFTY"
        '        ' ''    Else
        '        ' ''        If (comptable.Rows.Count <> 0) Then
        '        ' ''            compname = comptable.Rows(0).Item("company")
        '        ' ''        End If
        '        ' ''    End If
        '    ' ''End If

        '    Call change_tab(compname, tbmo)
        '    DGTrading.Refresh()
        '        'End If
        '    If Not sort Is Nothing Then
        '        If sortorder.ToString = "Ascending" Then
        '            DGTrading.Sort(DGTrading.Columns(sortindex), ComponentModel.ListSortDirection.Ascending)
        '        Else
        '            DGTrading.Sort(DGTrading.Columns(sortindex), ComponentModel.ListSortDirection.Descending)
        '        End If
        '    End If
        '        'Call GSub_Fill_GDt_AllTrades()
        '        'Call GSub_Fill_GDt_Strategy()
        '    VarFileImport = False
        'End If

        'Me.Cursor = Cursors.Default
        'refreshstarted = True
    End Sub

    Private Sub AboutVolHedgeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutVolHedgeToolStripMenuItem.Click
        FrmAboutVolHedge1.ShowDialog()
        'Version_.ShowDialog()
    End Sub

    Private Sub ConnectToServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectToServerToolStripMenuItem.Click
        If NetMode = "UDP" Then
            Call connect_server()
        ElseIf NetMode = "TCP" Then
            Timer_Sql.Enabled = True
            Timer_Sql.Start()
        ElseIf NetMode = "NET" Then
            Timer_Net.Enabled = True
            Timer_Net.Start()
        ElseIf NetMode = "API" And flgAPI_K <> "TCP" Then
            flgApiReStart = True
            ApiThreadreset()
            InitApiThread()

        ElseIf NetMode = "JL" Then
            Timer_Sql.Enabled = True
            Timer_Sql.Start()
        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
            If flgAPI_ExpCheck = True Then


                Timer_Sql.Enabled = True
                Timer_Sql.Start()
            End If
        End If

        MsgBox("Start Broadcast Successfully.", MsgBoxStyle.Information)
    End Sub

    Private Sub DisconnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisconnectToolStripMenuItem.Click
        If NetMode = "UDP" Then
            Call disconnect_server()
        ElseIf NetMode = "TCP" Then
            Timer_Sql.Stop()
            Timer_Sql.Enabled = False
        ElseIf NetMode = "NET" Then
            Timer_Net.Stop()
            Timer_Net.Enabled = False
        End If

        MsgBox("Stop Broadcast Successfully.", MsgBoxStyle.Information)
    End Sub

    ''' <summary>
    ''' connect_server
    ''' </summary>
    ''' <remarks>This method call to Init. FO broadcast</remarks>
    Private Sub connect_server()
        Try
            If multicastListener_fo Is Nothing Then
                ' initialize_fo_broadcast()
                initialize_fo_broadcast()
            Else
                multicastListener_fo.Close()
                multicastListener_fo = Nothing
                ThreadReceive_fo.Abort()
                ThreadReceive_fo = Nothing
                initialize_fo_broadcast()

            End If

            If multicastListener_cm Is Nothing Then
                initialize_cm_broadcast()
            Else
                multicastListener_cm.Close()
                multicastListener_cm = Nothing
                ThreadReceive_cm.Abort()
                ThreadReceive_cm = Nothing
                initialize_cm_broadcast()
            End If

            If multicastListener_Currency Is Nothing Then
                initialize_Currency_broadcast()
            Else
                multicastListener_Currency.Close()
                multicastListener_Currency = Nothing
                ThreadReceive_Currency.Abort()
                ThreadReceive_Currency = Nothing
                initialize_Currency_broadcast()
            End If
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' disconnect_server
    ''' </summary>
    ''' <remarks>This method call to stop FO broadcast</remarks>
    Private Sub disconnect_server()
        Try
            'ThreadReceive_fo.Suspend()
            ThreadReceive_fo.Abort()
            If Not multicastListener_fo Is Nothing Then
                multicastListener_fo.Close()
                multicastListener_fo = Nothing
            End If

            ThreadReceive_cm.Abort()
            If Not multicastListener_cm Is Nothing Then
                multicastListener_cm.Close()
                multicastListener_cm = Nothing
            End If

            ThreadReceive_Currency.Abort()
            If Not multicastListener_Currency Is Nothing Then
                multicastListener_Currency.Close()
                multicastListener_Currency = Nothing
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ReOptimizedPositionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReOptimizedPositionToolStripMenuItem.Click
        Try
            For Each Open_Frm As Form In Me.MdiChildren
                Open_Frm.Close()
            Next

            Select Case (MsgBox("Are You Sure To Reoptimize Position?", MsgBoxStyle.YesNo + MsgBoxStyle.Question))

                Case MsgBoxResult.Yes
                    Me.Cursor = Cursors.WaitCursor

                    REM Backup database before performing reoptimization is done
                    'backup old database
                    Dim str_folder_path As String
                    str_folder_path = System.Windows.Forms.Application.StartupPath() & "\backup_" & Format(Now(), "ddMMyy")
                    If Not Directory.Exists(str_folder_path) Then
                        Directory.CreateDirectory(str_folder_path)
                    End If
                    Dim str As String = ConfigurationSettings.AppSettings("dbname")
                    Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str

                    Dim cur_date_str As String
                    cur_date_str = Format(Now, "ddMMMyyyy_HHmm")
                    Dim tstr As String = str_folder_path & "\greek_backup_" & cur_date_str & ".mdb"

                    FileCopy(_connection_string, tstr)
                    '============================end of backup

                    Obj_Del.Delete_todaydata()
                    Obj_Trad.Delete_prBal(Now.Date)

                    GTempPreBal = Obj_Trad.prebal
                    GTempDTPosition = maintable

                    REM Delete All Trades
                    Obj_Del.Insert()
                    GdtFOTrades.Clear()
                    GdtEQTrades.Clear()
                    GdtCurrencyTrades.Rows.Clear()
                    comptable.Rows.Clear()
                    hashOrder.Clear()

                    REM Fill In Trading Table
                    Call GSub_Fill_TradingTables(GTempDTPosition)

                    REM Fill Previous Balance Alpesh
                    'Obj_Trad.insert_prebal(GTempPreBal)

                    maintable = GTempDTPosition
                    'prebal = GTempPreBal
                    Try
                        Call proc_data(False)
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Exclamation)
                    End Try
                    MsgBox("Position Optimized Successfully.", MsgBoxStyle.Information)
                    Me.Cursor = Cursors.Default
                Case MsgBoxResult.No
                    Exit Sub
            End Select
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ReadFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadFileToolStripMenuItem.Click
        FrmReadFile.Show()
    End Sub


    Private Sub ProfitLossAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProfitLossAnalysisToolStripMenuItem.Click

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

        If SAVE_ANA_DATA_FILE = 1 Then

            OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
            OpenFileDialog1.FileName = "*.csv"
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                Try
                    Dim tempdata As New DataTable
                    Dim fpath As String
                    fpath = OpenFileDialog1.FileName

                    Dim fi As New FileInfo(fpath)
                    Dim sConnString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
                    Dim objConn As New OleDbConnection(sConnString)
                    objConn.Open()
                    Dim objAdapter1 As New OleDbDataAdapter("SELECT * FROM " & fi.Name, objConn)
                    tempdata = New DataTable
                    objAdapter1.Fill(tempdata)
                    objConn.Close()

                    For Each drow As DataRow In maintable.Select()
                        Dim entrydate As Date
                        Dim PreSpot As Double
                        Dim PreVol, PreDelVal, PreVegVal, PreTheVal, preTotalMTM, preGrossMTM, preQty As Double

                        If IsDBNull(tempdata.Compute("Max(EntryDate)", "tokanno=" & drow("tokanno"))) Then
                            Continue For
                        End If

                        entrydate = tempdata.Compute("Max(EntryDate)", "tokanno=" & drow("tokanno"))

                        PreSpot = tempdata.Compute("Max(curSpot)", "tokanno=" & drow("tokanno"))
                        PreVol = tempdata.Compute("Max(curVol)", "tokanno=" & drow("tokanno"))
                        PreDelVal = tempdata.Compute("Max(curDelVal)", "tokanno=" & drow("tokanno"))
                        PreVegVal = tempdata.Compute("Max(curVegVal)", "tokanno=" & drow("tokanno"))
                        PreTheVal = tempdata.Compute("Max(curTheVal)", "tokanno=" & drow("tokanno"))

                        preTotalMTM = tempdata.Compute("Max(curTotalMTM)", "tokanno=" & drow("tokanno"))
                        preGrossMTM = tempdata.Compute("Max(curGrossMTM)", "tokanno=" & drow("tokanno"))
                        preQty = tempdata.Compute("Max(units)", "tokanno=" & drow("tokanno"))

                        Dim objanalysisprocess As New analysisprocess
                        objanalysisprocess.UpdPreData_analysis1(entrydate, PreSpot, PreVol, PreDelVal, PreVegVal, PreTheVal, preTotalMTM, preGrossMTM, preQty, drow("tokanno"))

                    Next


                Catch ex As Exception
                    MsgBox(ex.ToString())
                End Try

            End If
        End If

        Dim analysisPl As New FrmPLanalysis
        If FrmPLanalysis.chkPLanalysis = False Then
            analysisPl.MdiParent = Me
            analysisPl.Show()
        Else
            analysisPl.Dispose()
        End If
    End Sub

    Private Sub ManualRefreshF5ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ExportAnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportAnalysisToolStripMenuItem.Click
        'SendKeys.Send("{F10}")
        analysis.cmdExport_Click(New Object, e)
    End Sub

    Private Sub Option1ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Option1ToolStripMenuItem.Click
        Dim analysis1 As New optionfrm1
        If optionfrm1.chkoptionfrm = False Then
            analysis1.MdiParent = Me
            analysis1.Show()
        Else
            analysis1.Dispose()
        End If
    End Sub

    Private Sub AddUserDefineTagToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddUserDefineTagToolStripMenuItem.Click
        analysis.AddUserDefCompany("")
        FlgFixvolgreekComp = True
    End Sub

    Private Sub SettelmentBhavToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettelmentBhavToolStripMenuItem.Click
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
        SettelmentBhav.MdiParent = Me
        SettelmentBhav.Show()
    End Sub

    Private Sub TroubleshootingStepsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TroubleshootingStepsToolStripMenuItem.Click
        'Try
        '    'Dim Str As String = Application.StartupPath & "\VHErrorHelp.pps"
        '    'Shell(Str)



        '    Dim fs As New FileStream(Application.StartupPath & "\FileOpen.bat", FileMode.Create)
        '    Dim sw As StreamWriter
        '    sw = New StreamWriter(fs)
        '    sw.WriteLine(Application.StartupPath & "\VHErrorHelp.pps")
        '    sw.Close()
        '    fs.Close()


        '    Shell(Application.StartupPath & "\FileOpen.bat", AppWinStyle.Hide, True)

        'Catch ex As Exception

        'End Try


        Dim myProcess As New Process

        Try
            myProcess.StartInfo.FileName = Application.StartupPath & "\VHErrorHelp.pps"
            myProcess.StartInfo.CreateNoWindow = True
            myProcess.Start()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



    End Sub

    Public Sub RefreshLTPCtrlF5ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshLTPCtrlF5ToolStripMenuItem.Click
        If NetMode = "NET" Then
            If bool_Thr_GetInterNetDat = False Then
                Timer_Net.Stop()
                Timer_Net_Tick(Timer_Net, New EventArgs)
                Timer_Net.Start()
            End If
        ElseIf NetMode = "TCP" Then

            If bool_IsTelNet = False Then
                Dim Thr_Telnet As New System.Threading.Thread(AddressOf CheckTelNet_Connection)
                Thr_Telnet.Name = "CheckTelnet"
                Thr_Telnet.Start()
                Exit Sub
            End If

            If ss.IsBusy = False Then
                If FlgTcpBCast = False Then
                    Try

                        ss.RunWorkerAsync()
                    Catch ex As Exception

                    End Try
                End If

            Else
                'MsgBox("Busy")
            End If

        End If
    End Sub

    Public Sub Sub_Set_ServerConnect_Status(ByVal IsConnected As Boolean, ByVal Str_Message As String)
        'On Error Resume Next
        Dim str_Connect_Status As String = ""
        Dim bool_Con_status As Boolean = IsConnected
        If bool_Con_status <> bool_IsServerConnected Then
            If bool_Con_status = False Then
                str_Connect_Status = "Server not Connected !!"
                'TimerCheckExpiry.Enabled = False
                'TimerImportData.Enabled = False

            Else
                obj_DelSetDisableMenubar.Invoke(Boolean.TrueString, str_Connect_Status)
                'MenuStrip1.Enabled = False
                str_Connect_Status = "Server Connected"
                If NetMode = "UDP" Then
                    Call sub_init_fo_broadcast_for_CheckExpiry()
                End If


                'TimerCheckExpiry.Enabled = True
                'TimerImportData.Enabled = True
                Call TimerCheckExpiry_Tick(Nothing, Nothing)
            End If
            Call obj_DelSetDisableMenubar.Invoke(bool_Con_status.ToString, str_Connect_Status)
            'MenuStrip1.Enabled = True
            bool_IsServerConnected = IsConnected
        End If
    End Sub

    Public Sub Sub_Disable_Manu_n_Tool_bar(ByVal varBool As String, ByVal str_Connect_Status As String)
        On Error Resume Next

        ViewToolStripMenuItem.Enabled = Boolean.Parse(varBool)
        ToolsToolStripMenuItem.Enabled = Boolean.Parse(varBool)
        ImportExportToolStripMenuItem.Enabled = Boolean.Parse(varBool)
        ReportToolStripMenuItem.Enabled = Boolean.Parse(varBool)
        WindowsToolStripMenuItem.Enabled = Boolean.Parse(varBool)
        ToolStripcompanyCombo.Enabled = Boolean.Parse(varBool)
        ToolStripMenuSearchComp.Enabled = Boolean.Parse(varBool)

        ReconnectToServerToolStripMenuItem.Enabled = Not Boolean.Parse(varBool)

        If Boolean.Parse(varBool) = False Then
            If NetMode = "UDP" Then
                Call GSub_Stop_Broadcast()
            ElseIf NetMode = "TCP" Then
                If ifNetModeTcp = True Then
                    'obj_DelSetTimer.Invoke(False)
                    Timer_Sql.Stop()
                    Timer_Sql.Enabled = False
                Else
                    ifNetModeTcp = True
                End If
            ElseIf NetMode = "NET" Then
                If ifNetModeTcp = True Then
                    Timer_Net.Stop()
                    Timer_Net.Enabled = False
                Else
                    ifNetModeTcp = True
                End If
            ElseIf NetMode = "JL" Then
                Timer_Sql.Enabled = True
                Timer_Sql.Start()
            ElseIf NetMode = "API" And (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") Then
                If flgAPI_ExpCheck = True Then
                    Timer_Sql.Enabled = True
                    Timer_Sql.Start()
                End If

            End If
        Else
            If NetMode = "UDP" Then
                Call GSub_Start_Broadcast()
            ElseIf NetMode = "TCP" Then
                If ifNetModeTcp = True Then
                    'obj_DelSetTimer.Invoke(True)
                    Timer_Sql.Enabled = True
                    Timer_Sql.Start()
                Else
                    ifNetModeTcp = True
                End If
            ElseIf NetMode = "JL" Then
                Timer_Sql.Enabled = True
                Timer_Sql.Start()
            ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
                If flgAPI_ExpCheck = True Then
                    Timer_Sql.Enabled = True
                    Timer_Sql.Start()
                End If


            ElseIf NetMode = "NET" Then
                If ifNetModeTcp = True Then
                    'Timer_Net.Interval = Timer_Net_Interval
                    'Timer_Net.Enabled = True
                    'Timer_Net.Start()
                    'Call Timer_Net_Tick(Timer_Net, New EventArgs)
                Else
                    ifNetModeTcp = True
                End If
            End If
        End If

        'Me.Text = MeText & "[" & str_Connect_Status & "]"
        Sub_Set_Conn_Msg(True, str_Connect_Status)
        MenuStrip1.Refresh()
        If Boolean.Parse(varBool) = False Then
            G_BCastFoIsOn = False
            G_BCastCmIsOn = False
            G_BCastCurrIsOn = False
            G_BCastSqlFoIsOn = False
            G_BCastSqlCmIsOn = False
            G_BCastSqlCurrIsOn = False
            G_bool_IsAuthanticated = False
        End If

        'ToolStrip1.Enabled = Boolean.Parse(varBool)
        'ToolStripcmbShowZero.Enabled = Boolean.Parse(varBool)
        'ToolStripcmbMtoMType.Enabled = Boolean.Parse(varBool)
        'ToolStripcmbDlr_Prev_MtoM.Enabled = Boolean.Parse(varBool)
        'ToolStrip1.Refresh()
    End Sub


    Private Sub Timer_Sql_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Sql.Tick
        'mPerf.WriteLogStr("Timer_Sql_Tick")
        If NetMode = "TCP" Or NetMode = "JL" Or ((flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API") Then
            If (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
                If flgAPI_ExpCheck = False Then
                    Return
                End If
            End If
            If NetMode = "TCP" Then
                Timer_Sql.Interval = 25000
            End If

            Try
                If bool_IsTelNet = False Then
                    Dim Thr_Telnet As New System.Threading.Thread(AddressOf CheckTelNet_Connection)
                    Thr_Telnet.Name = "CheckTelnet"
                    Thr_Telnet.Start()
                    Exit Sub
                End If

                'If DGTrading.Rows.Count = 0 Then Exit Sub


                If ss.IsBusy = False Then
                    If FlgTcpBCast = False Then
                        ss.RunWorkerAsync()
                    End If

                Else
                    'MsgBox("Busy")
                End If
            Catch ex As Exception
                'MsgBox("Error")
            End Try
        Else
            Try
                If bool_IsTelNet = False Then
                    Dim Thr_Telnet As New System.Threading.Thread(AddressOf CheckTelNet_Connection)
                    Thr_Telnet.Name = "CheckTelnet"
                    Thr_Telnet.Start()
                    Exit Sub
                End If
                'comment by payalpatel 1-mar-2018
                'If AppLicMode = "NETLIC" Then
                '    If ObjLoginData.GetUserStatus = "out" Then
                '        End
                '    End If
                'End If

                If comptable_Net.Tables.Count = 0 Then
                    Exit Sub
                Else
                    If comptable_Net.Tables(0).Rows.Count = 0 Then
                        Exit Sub
                    End If
                End If

                If bool_Thr_GetInterNetDat = False And NetMode = "NET" Then
                    Thr_GetInterNetDat = New Thread(AddressOf analysis.ThrInterNetData)
                    Thr_GetInterNetDat.Name = "ThrInterNetData"
                    Thr_GetInterNetDat.Start()
                End If
            Catch ex As Threading.ThreadAbortException
                Threading.Thread.ResetAbort()
            Catch ex As Exception
                MsgBox("Timer_Net_Tick" & vbCrLf & ex.ToString)
            End Try
        End If
        'Try
        '    If bool_IsTelNet = False Then
        '        Dim Thr_Telnet As New System.Threading.Thread(AddressOf CheckTelNet_Connection)
        '        Thr_Telnet.Name = "CheckTelnet"
        '        Thr_Telnet.Start()
        '        Exit Sub
        '    End If

        '    'If DGTrading.Rows.Count = 0 Then Exit Sub
        '    If ss.IsBusy = False Then
        '        If FlgTcpBCast = False Then
        '            ss.RunWorkerAsync()
        '        End If

        '    Else
        '        'MsgBox("Busy")
        '    End If
        'Catch ex As Exception
        '    'MsgBox("Error")
        'End Try



    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles ss.DoWork
        Try
            If bSqlValidated = True Then
                FlgTcpBCast = True

                'resetEvents = New ManualResetEvent(3) {}
                'Dim i As Integer = 0


                'resetEvents(0) = New ManualResetEvent(False)
                'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf process_fo_SQL), DirectCast("0", Object))

                'resetEvents(1) = New ManualResetEvent(False)
                'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf process_cm_Sql), DirectCast("1", Object))

                'resetEvents(2) = New ManualResetEvent(False)
                'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf process_Currency_Sql), DirectCast("2", Object))

                'resetEvents(3) = New ManualResetEvent(False)
                'ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf process_Inx_7207_sql), DirectCast("3", Object))

                ''resetEvents(3) = New ManualResetEvent(False)
                ''ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf GetTrend), DirectCast("3", Object))

                'WaitHandle.WaitAll(resetEvents, New TimeSpan(1000 * 30), True)




                'If MarketWatch.Visible = True Then
                '    '    Timer1Event()
                '    ' MsgBox("MWTrue")
                'Else
                '    'MsgBox("MWFalse")
                'End If


                If Not IsStopBroadCastManually Then

                    Dim obj_AsyncResult(3) As IAsyncResult

                    obj_AsyncResult(0) = obj_Del_Ref_process_fo_SQL.BeginInvoke(DirectCast("0", Object), Nothing, Nothing)
                    obj_AsyncResult(1) = obj_Del_Ref_process_cm_Sql.BeginInvoke(DirectCast("1", Object), Nothing, Nothing)
                    obj_AsyncResult(2) = obj_Del_Ref_process_Currency_Sql.BeginInvoke(DirectCast("2", Object), Nothing, Nothing)
                    obj_AsyncResult(3) = obj_Del_Ref_process_Inx_7207_sql.BeginInvoke(DirectCast("3", Object), Nothing, Nothing)

                    obj_AsyncResult(0).AsyncWaitHandle.WaitOne()
                    obj_AsyncResult(1).AsyncWaitHandle.WaitOne()
                    obj_AsyncResult(2).AsyncWaitHandle.WaitOne()
                    obj_AsyncResult(3).AsyncWaitHandle.WaitOne()

                    'For Each frm As Form In Me.MdiChildren
                    '    If frm.Text = "MarketWatch" Then
                    '        DirectCast(frm, MarketWatch).Timer1Event()
                    '    End If
                    'Next
                End If
                FlgTcpBCast = False

                'Call process_fo_SQL()
                'Call process_cm_Sql()
                'Call process_Currency_Sql()
            End If
        Catch ex As Exception
            ' MsgBox("Span Calculation error: " & ex.ToString)
            FlgTcpBCast = False

            ss.Dispose()
        End Try
    End Sub
    Public Sub Refresh_intermet_time()
        If internerRefreshtime <> "" And internerRefreshtime.Contains("&nbsp;") Then
            ToolTip1.SetToolTip(chkinternet, internerRefreshtime.Replace("&nbsp;", ""))
        End If
    End Sub
    Public Sub Timer_Net_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Net.Tick

        If NetMode <> "NET" Then
            Return
        End If

        Try
            If bool_IsTelNet = False Then
                Dim Thr_Telnet As New System.Threading.Thread(AddressOf CheckTelNet_Connection)
                Thr_Telnet.Name = "CheckTelnet"
                Thr_Telnet.Start()
                Exit Sub
            End If
            'comment by payalpatel 1-mar-2018
            'If AppLicMode = "NETLIC" Then
            '    If ObjLoginData.GetUserStatus = "out" Then
            '        End
            '    End If
            'End If

            If comptable_Net.Tables.Count = 0 Then
                Exit Sub
            Else
                If comptable_Net.Tables(0).Rows.Count = 0 Then
                    Exit Sub
                End If
            End If
            Refresh_intermet_time()
            If bool_Thr_GetInterNetDat = False Then
                Thr_GetInterNetDat = New Thread(AddressOf analysis.ThrInterNetData)
                Thr_GetInterNetDat.Name = "ThrInterNetData"
                Thr_GetInterNetDat.Start()
                'If comptable_Net.Tables(0).Select("company like '*NIFTY*'").Length > 0 Then
                '    'WebBrowser1.Url = New System.Uri("https://nseindia.com/live_market/dynaContent/live_watch/live_index_watch.htm", System.UriKind.Absolute)
                '    WebBrowser1.Navigate("javascript:getliveindexWatchData();")
                '    strIndex = WebBrowser1.Document.Body.InnerHtml
                '    obj_DelIndex.Invoke(strIndex)
                'End If

            End If
        Catch ex As Threading.ThreadAbortException
            WriteLog("error In getInternet data" & ex.ToString)
            Threading.Thread.ResetAbort()
        Catch ex As Exception
            MsgBox("Timer_Net_Tick" & vbCrLf & ex.ToString)
        End Try
    End Sub

    Public Sub ReSetFixVolToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReSetFixVolToolStripMenuItem.Click
        Try
            Dim Item As DictionaryEntry
            Dim ArrFKey As New ArrayList
            Dim ArrCPKey As New ArrayList
            Dim ArrEKey As New ArrayList
            Dim VaLLTPPrice As New Double

            analysis.txtcvol.Text = ""
            analysis.txtpvol.Text = ""
            analysis.txtfut1.Text = ""

            analysis.txtcvol1.Text = ""
            analysis.txtpvol1.Text = ""
            analysis.txtfut2.Text = ""

            analysis.txtcvol2.Text = ""
            analysis.txtpvol2.Text = ""
            analysis.txtfut3.Text = ""

            For Each Item In fltpprice
                ArrFKey.Add(Item.Key)
            Next
            For Each Item In ltpprice
                ArrCPKey.Add(Item.Key)
            Next
            For Each Item In eltpprice
                ArrEKey.Add(Item.Key)
            Next
            For i As Integer = 0 To ArrFKey.Count - 1
                If cpfmaster.Select("token=" & ArrFKey(i) & "").Length > 0 Then
                    Dim VarScript As String = cpfmaster.Select("token=" & ArrFKey(i) & "")(0).Item("Script")
                    If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                        fltpprice(ArrFKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                    End If
                End If
            Next
            For i As Integer = 0 To ArrCPKey.Count - 1
                If cpfmaster.Select("token=" & ArrCPKey(i) & "").Length > 0 Then
                    Dim VarScript As String = cpfmaster.Select("token=" & ArrCPKey(i) & "")(0).Item("Script")
                    If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                        fltpprice(ArrCPKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        ltpprice(ArrCPKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                    End If
                End If
            Next
            For i As Integer = 0 To ArrEKey.Count - 1
                If eqmaster.Select("token=" & ArrEKey(i) & "").Length > 0 Then
                    Dim VarScript As String = eqmaster.Select("token=" & ArrEKey(i) & "")(0).Item("Script")
                    If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                        fltpprice(ArrEKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        eltpprice(ArrEKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                    End If
                End If
            Next

            analysis.txtcvol.Text = ""
            analysis.txtpvol.Text = ""
            analysis.txtfut1.Text = ""

            analysis.txtcvol1.Text = ""
            analysis.txtpvol1.Text = ""
            analysis.txtfut2.Text = ""

            analysis.txtcvol2.Text = ""
            analysis.txtpvol2.Text = ""
            analysis.txtfut3.Text = ""


            If analysis.chkanalysis = True Then
                Obj_Trad.Delete_company_ana(analysis.compname)
                GdtCompanyAnalysis = Obj_Trad.select_company_ana
                analysis.comp_ana = GdtCompanyAnalysis

                GVarIsNewBhavcopy = True
                Call analysis.AssignBhavcopyLTP(True)
            Else
                Obj_Trad.Delete_company_ana("")
                GdtCompanyAnalysis = Obj_Trad.select_company_ana
                analysis.comp_ana = GdtCompanyAnalysis
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub UploadRangeDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadRangeDataToolStripMenuItem.Click
        'If RegServerIP.Trim = "" Then
        'SetRegServer()
        'End If
        SetRegServer()
        Dim RangeCalc1 As New ProcessRangeCalc
        If ProcessRangeCalc.chkfindRange = False Then
            'RangeCalc1.MdiParent = Me
            RangeCalc1.ShowDialog()
        Else
            RangeCalc1.Dispose()
        End If
    End Sub

    Private Sub DownloadRangeDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadRangeDataToolStripMenuItem.Click
        'If RegServerIP.Trim = "" Then
        SetRegServer()
        'End If
        Dim DownloadRangeCalc1 As New DownloadRangeCalc
        If DownloadRangeCalc.chkfindRange1 = False Then
            'RangeCalc1.MdiParent = Me
            DownloadRangeCalc1.ShowDialog()
        Else
            DownloadRangeCalc1.Dispose()
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Dim RangeCalc1 As New RangeCalculator
        'If RangeCalculator.chkRangeCalc = False Then
        '    RangeCalc1.MdiParent = Me
        '    RangeCalc1.Show()
        'Else
        '    RangeCalc1.Dispose()
        'End If
        RangeCalc1.MdiParent = Me
        RangeCalc1.Show()
    End Sub

    Private Sub TeamviewerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TeamviewerToolStripMenuItem.Click
        Dim myProcess As New Process
        Try
            myProcess.StartInfo.FileName = Application.StartupPath & "\FinIdeas-CS.exe"
            myProcess.StartInfo.CreateNoWindow = True
            myProcess.Start()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub AmmyyAdminToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AmmyyAdminToolStripMenuItem.Click
        Dim myProcess As New Process
        Try
            myProcess.StartInfo.FileName = Application.StartupPath & "\AA_v3.exe"
            myProcess.StartInfo.CreateNoWindow = True
            myProcess.Start()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub VideosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VideosToolStripMenuItem.Click

    End Sub

    Private Sub HindiVideosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HindiVideosToolStripMenuItem.Click

        Try
            System.Diagnostics.Process.Start("http://www.youtube.com/playlist?list=PLC2D9EZOgO5KqaW1c17Jd9vn0BJNs8tBf")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub EnglishVideosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnglishVideosToolStripMenuItem.Click

        Try
            System.Diagnostics.Process.Start("http://www.youtube.com/playlist?list=PLC2D9EZOgO5LTdBUSGVkoV4RxaV_kgAGL")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MatchFOContractToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MatchFOContractToolStripMenuItem.Click
        For Each Frm1 As Form In Me.MdiChildren
            Frm1.Close()
        Next
        Dim frm As New FrmMatchContract
        frm.ShowForm("FO")
    End Sub

    Private Sub MatchCurrencyContractToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MatchCurrencyContractToolStripMenuItem.Click
        For Each Frm1 As Form In Me.MdiChildren
            Frm1.Close()
        Next
        Dim frm As New FrmMatchContract
        frm.ShowForm("CUR")
    End Sub

    Private Sub RBCheck_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBCheck.CheckedChanged

        '=========================REM:Coding For UPDATE FIELD OF IMPORTSETTING TABLE  17/06/2014===============
        If loadFlag = False Then


            If (RBCheck.Checked = True) Then
                RBCheck.BackColor = Color.BlueViolet
            Else
                RBCheck.BackColor = Color.AliceBlue
            End If
            Dim VarAuto_Import, VarManual_Import As Boolean
            Dim DTChkAuto As DataTable
            Dim id As Integer
            DTChkAuto = G_DTImportSetting 'trd.Select_Import_Setting()
            If DTChkAuto Is Nothing = False Then

                For Each dr As DataRow In DTChkAuto.Rows
                    If (RBCheck.Checked = True) Then
                        'If ChkAuto.Select("manual_import= '" & True & "' ").Length > 0 Then
                        If (dr("manual_import") = True) Then
                            id = dr("ID")
                            dr("Auto_Import") = True
                            VarAuto_Import = dr("Auto_Import")
                            VarManual_Import = dr("manual_import")

                        Else
                            id = dr("ID")
                            VarAuto_Import = dr("Auto_Import")
                            VarManual_Import = dr("manual_import")

                        End If
                        trd.Update_Import_Setting(VarAuto_Import, VarManual_Import, id)

                    Else

                        If (dr("manual_import") = True) Then
                            id = dr("ID")
                            dr("Auto_Import") = False
                            VarAuto_Import = dr("Auto_Import")
                            VarManual_Import = dr("manual_import")

                        Else
                            id = dr("ID")
                            VarAuto_Import = False 'dr("Auto_Import")
                            VarManual_Import = dr("manual_import")

                        End If
                        trd.Update_Import_Setting(VarAuto_Import, VarManual_Import, id)

                    End If
                    ' End If
                Next

            End If
            '=======================REM:END=======================
        End If
    End Sub

    Private Sub chktcp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktcp.CheckedChanged
        Call chkinternet_CheckedChanged(sender, e)
        Return
        '     frmsetting.FillConnectionDataToTextBox()
        '=========================REM:Coding For NETMODE SETTING IN SETTING TABLE  17/06/2014===============
        Return
        If NetMode <> "TCP" Then

            If frmsetting.ChkSQLConn1() = True Then
                Dim settingname, settingkey, uid As String
                settingname = "NETMODE"
                settingkey = "TCP"
                settingname = "NETMODE"
                uid = "190"
                trd.Update_setting(settingkey, settingname, uid)
                MsgBox("Setting Successfully Apply")



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

                Thr_GetInterNetDat.Abort()
                Me.Dispose()

                Application.Restart()
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
            Else
                'MsgBox("Test Connection Fail !! ")
                MsgBox("Change Connection Settings!! ")
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
                'analysis1.ShowDialog()
                analysis1.ShowForm(3)
                analysis.searchcompany()
                SAVEIMPORTSETTING()
            End If

            '=========================REM:END===============
        End If
    End Sub

    Private Sub chkudp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkudp.CheckedChanged

        '=========================REM:Coding For NETMODE SETTING IN SETTING TABLE  17/06/2014===============

        If NetMode <> "UDP" Then
            Dim flagfo As Boolean
            Dim flagcm As Boolean
            Dim flagcurr As Boolean
            flagfo = CheckUDPfo_Connection(flagfo)
            flagcm = CheckUDPcm_Connection(flagcm)
            flagcurr = CheckUDPcurr_Connection(flagcurr)
            If (flagfo = True Or flagcm = True Or flagcurr = True) Then


                Dim settingname, settingkey, uid As String
                settingname = "NETMODE"
                settingkey = "UDP"
                settingname = "NETMODE"
                uid = "190"
                trd.Update_setting(settingkey, settingname, uid)
                MsgBox("Setting Successfully Apply")
                For Each frm As Form In Me.MdiChildren
                    frm.Close()
                Next
                Me.Dispose()
                CUtils.RestartApplication()
                Return
                Application.Restart()
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
            Else
                MsgBox("Change Connection Settings!! ")
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
                'analysis1.ShowDialog()
                analysis1.ShowForm(3)
                analysis.searchcompany()
                SAVEIMPORTSETTING()
            End If
            '=========================REM:Coding For NETMODE SETTING IN SETTING TABLE  17/06/2014===============
        End If
    End Sub

    Private Sub chkinternet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkinternet.CheckedChanged
        '=========================REM:Coding For NETMODE SETTING IN SETTING TABLE  17/06/2014===============
        If NetMode <> "NET" Then

            CheckTelNet_Connection()
            If (bool_IsTelNet = True) Then
                Dim settingname, settingkey, uid As String
                settingname = "NETMODE"
                settingkey = "NET"
                settingname = "NETMODE"
                uid = "190"
                trd.Update_setting(settingkey, settingname, uid)
                MsgBox("Setting Successfully Apply")
                For Each frm As Form In Me.MdiChildren
                    frm.Close()
                Next
                Me.Dispose()

                CUtils.RestartApplication()
                Return
                Application.Restart()
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
            Else
                MsgBox("Change Connection Settings!! ")
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
                'analysis1.ShowDialog()
                analysis1.ShowForm(3)
                analysis.searchcompany()

                SAVEIMPORTSETTING()
            End If
            '=========================REM:END===============
        End If
    End Sub

    Private Sub ScriptMapperToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScriptMapperToolStripMenuItem.Click
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
        ScriptMapingFunctionality.ShowDialog()
    End Sub

    Private Sub ImportExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportExportToolStripMenuItem.Click

    End Sub


    'Private Sub Form5ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Form5.ShowDialog()
    'End Sub

    Private Sub SuggestionsFeedBackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SuggestionsFeedBackToolStripMenuItem.Click
        Try
            System.Diagnostics.Process.Start("https://docs.google.com/a/finideas.com/forms/d/1zbteCQ8xn-qnso-iYZIsJkn1nyaEZVafcf8IqodcHYg/viewform")
        Catch ex As Exception
        End Try
    End Sub


    Private Sub RecalculatePositionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecalculatePositionToolStripMenuItem.Click
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
        applicationCrash()
    End Sub

    Private Sub ShortCutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim frm As New ShortCut
        frm.ShowDialog()

    End Sub

    Private Sub OnlineUpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlineUpdateToolStripMenuItem.Click
        'Shell(Application.StartupPath & "/OnlineUpdate.exe")
        '  AutoUpdate("ftp://strategybuilder.finideas.com/VolHedge", "ftp://strategybuilder.finideas.com/OnlineUpdate", "VolHedge")
        AutoUpdate("https://support.finideas.com/VolHedge", "https://support.finideas.com/OnlineUpdate", "VolHedge")

    End Sub
    Public Function DownloadFileFTP(ByVal FileNameToDownload As String, ByVal URL As String, ByVal dirpath As String) As String

        'Dim ftpURL As String = URL
        ''Host URL or address of the FTP serve
        'Dim userName As String = "strategybuilder" '"FI-strategybuilder"
        ''User Name of the FTP server
        'Dim password As String = "finideas#123"
        Dim tempDirPath As String = dirpath
        Dim ResponseDescription As String = ""
        Dim PureFileName As String = New FileInfo(FileNameToDownload).Name
        'Dim DownloadedFilePath As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName
        'Dim downloadUrl As String = [String].Format("{0}/{1}", ftpURL, FileNameToDownload)
        'Dim req As FtpWebRequest = DirectCast(FtpWebRequest.Create(downloadUrl), FtpWebRequest)
        'req.Method = WebRequestMethods.Ftp.DownloadFile
        'req.Credentials = New NetworkCredential(userName, password)
        'req.UseBinary = True
        'req.Proxy = Nothing
        'Try
        '    Dim response As FtpWebResponse = DirectCast(req.GetResponse(), FtpWebResponse)
        '    Dim stream As Stream = response.GetResponseStream()
        '    Dim buffer As Byte() = New Byte(2047) {}
        '    Dim fs As New FileStream(DownloadedFilePath, FileMode.Create)
        '    Dim ReadCount As Integer = stream.Read(buffer, 0, buffer.Length)
        '    While ReadCount > 0
        '        fs.Write(buffer, 0, ReadCount)
        '        ReadCount = stream.Read(buffer, 0, buffer.Length)
        '    End While
        '    ResponseDescription = response.StatusDescription
        '    fs.Close()
        '    stream.Close()
        'Catch e As Exception
        '    Console.WriteLine(e.Message)
        'End Try
        'Return ResponseDescription



        Try
            '    Dim url As String = ""

            Dim filename As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName

            URL = URL & FileNameToDownload



            Dim client As New WebClient()
            Dim uri As New Uri(URL)
            client.Headers.Add("Accept: text/html, application/xhtml+xml, */*")
            client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)")
            client.DownloadFileAsync(uri, filename)

            While client.IsBusy
                System.Threading.Thread.Sleep(1000)

            End While
            'Console.WriteLine(ex.Message);


        Catch ex As UriFormatException
        End Try
        Return ""
    End Function
    Private Sub DeleteDirectory(ByVal path As String)
        If Directory.Exists(path) Then
            'Delete all files from the Directory
            For Each filepath As String In Directory.GetFiles(path)
                File.Delete(filepath)
            Next
            'Delete all child Directories
            For Each dir As String In Directory.GetDirectories(path)
                DeleteDirectory(dir)
            Next
            'Delete a Directory
            Directory.Delete(path)
        End If
    End Sub
    Public Function AutoUpdate(ByRef CommandLine As String, ByVal RemotePath As String, ByVal Productname As String) As Boolean
        If Not Directory.Exists(Application.StartupPath & "\OnlineUpdateexe\") Then
            Directory.CreateDirectory(Application.StartupPath & "\OnlineUpdateexe\")
        Else
            DeleteDirectory(Application.StartupPath & "\OnlineUpdateexe\")
            'Directory.Delete(Application.StartupPath & "\OnlineUpdateexe\", True)
            Directory.CreateDirectory(Application.StartupPath & "\OnlineUpdateexe\")
        End If


        Dim StrContract As String = Application.StartupPath & "\OnlineUpdateexe\OnlineUpdate.exe"

        'If File.Exists(StrContract) Then
        '    File.Delete(StrContract)
        'End If

        Dim dirpath As String = Application.StartupPath + "\OnlineUpdateexe\"
        Dim FileNameToDownload As String = "OnlineUpdate.exe"
        DownloadFile("https://support.finideas.com/OnlineUpdate/", dirpath, FileNameToDownload)
        ' DownloadFile("https://support.finideas.com/SPAN/", Path, "PC-Span_4.5_608.msi")
        'Dim pHelp As New ProcessStartInfo
        'pHelp.FileName = Application.StartupPath + "\OnlineUpdateexe\OnlineUpdate.exe'"
        'pHelp.Arguments = "'" + Productname + "','" + CommandLine + "'"
        'pHelp.UseShellExecute = True
        'pHelp.WindowStyle = ProcessWindowStyle.Normal
        'Dim proc As Process = Process.Start(pHelp)


        System.Diagnostics.Process.Start(
          Application.StartupPath & "\OnlineUpdateexe\OnlineUpdate.exe", "" + Productname + ", " + CommandLine + "")

        writePatchFile()

        'Process.Start(Application.StartupPath + "\OnlineUpdateexe\OnlineUpdate.exe", "" + Productname + ", " + CommandLine + "")

        'Dim Key As String = "&**#@!" ' any unique sequence of characters
        '' the file with the update information
        'Dim sfile As String = "PATCH.rar"
        '' the Assembly name 
        'Dim AssemblyName As String = _
        '        System.Reflection.Assembly.GetEntryAssembly.GetName.Name
        '' where are the files for a specific system
        'Dim RemoteUri As String = RemotePath & AssemblyName & "/"
        '' clean up the command line getting rid of the key
        'CommandLine = Replace(Microsoft.VisualBasic.Command(), Key, "")
        '' Verify if was called by the autoupdate
        'If InStr(Microsoft.VisualBasic.Command(), Key) > 0 Then
        '    Try
        '        ' try to delete the AutoUpdate program, 
        '        ' since it is not needed anymore
        '        System.IO.File.Delete(Application.StartupPath & "\OnlineUpdate.exe")
        '    Catch ex As Exception
        '    End Try
        '    ' return false means that no update is needed
        '    Return False
        'Else
        '    ' was called by the user
        '    Dim ret As Boolean = False ' Default - no update needed
        '    Try
        '        Dim myWebClient As New System.Net.WebClient 'the webclient
        '        ' Download the update info file to the memory, 
        '        ' read and close the stream
        '        Dim file As New System.IO.StreamReader( _
        '            myWebClient.OpenRead(RemoteUri & sfile))
        '        Dim Contents As String = file.ReadToEnd()
        '        file.Close()
        '        ' if something was read
        '        If Contents <> "" Then
        '            ' Break the contents 
        '            Dim x() As String = Split(Contents, "|")
        '            ' the first parameter is the version. if it's 
        '            ' greater then the current version starts the 
        '            ' update process
        '            If x(0) > Application.ProductVersion Then
        '                ' assembly the parameter to be passed to the auto 
        '                ' update program
        '                ' x(1) is the files that need to be 
        '                ' updated separated by "?"
        '                Dim arg As String = Application.ExecutablePath & "|" & _
        '                            RemoteUri & "|" & x(1) & "|" & Key & "|" & _
        '                            Microsoft.VisualBasic.Command()
        '                ' Download the auto update program to the application 
        '                ' path, so you always have the last version runing
        '                myWebClient.DownloadFile(RemotePath & "OnlineUpdate.exe", _
        '                    Application.StartupPath & "\OnlineUpdate.exe")
        '                ' Call the auto update program with all the parameters
        'System.Diagnostics.Process.Start( _
        '    Application.StartupPath & "\OnlineUpdate.exe", arg)
        '                ' return true - auto update in progress
        '                ret = True
        '            End If
        '        End If
        '    Catch ex As Exception
        '        ' if there is an error return true, 
        '        ' what means that the application
        '        ' should be closed
        '        ret = True
        '        ' something went wrong... 
        '        MsgBox("There was a problem runing the Auto Update." & vbCr & _
        '            "Please Contact [contact info]" & vbCr & ex.Message, _
        '            MsgBoxStyle.Critical)
        '    End Try
        '    Return ret
        'End If
    End Function

    Private Sub OnlineSupportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlineSupportToolStripMenuItem.Click

    End Sub

    Private Sub UpdateSapnFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateSapnFileToolStripMenuItem.Click
        'Dim fnamecsv As String = Application.StartupPath + "\" + "DownloadFile\" + "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".s.spn" + ".zip"
        'nsccl.20160302.i03.spn
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim mSPAN_path As String = GdtSettings.Compute("max(SettingKey)", "SettingName='SPAN_path'").ToString
            'If System.IO.Directory.Exists(mSPAN_path) Then
            '    MsgBox("Install span software..Because" + mSPAN_path + " Not Found")
            '    Return
            'End If
            If Not Directory.Exists(mSPAN_path) Then 'if not correct span software path
                MsgBox("Enter Correct Path for span in setting.")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            Dim fnamecsv As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i1.zip"
            'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
            Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            spanfiledownload = sourcefname
            spanfiledownloadzip = fnamecsv
            DownloadSpanFile(fnamecsv, "FO")
            sourcefname = spanfiledownload
            fnamecsv = spanfiledownloadzip
            Dim filepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv
            Dim sourcepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
            'If File.Exists(Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv) Then 'if not correct span software path
            '    File.Delete(Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv)
            'End If
            '   ZipHelp.UnZip(filepath, Path.GetDirectoryName(filepath), 4096)
            UnZip1(filepath, Path.GetDirectoryName(filepath), 4096)
            'sourcefname = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            sourcepath = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
            File.Copy(sourcepath, mSPAN_path & "\" & sourcefname, True)
            MsgBox("Span Fo File Updated Successfully on path=" & mSPAN_path)
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            MsgBox("Fo Span  File Update Error..")
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub DownloadSpanFile(ByVal Fname As String, ByVal Type As String)
        Dim url As String = ""
        'If Type = "FO" Then
        '    '//https://www1.nseindia.com/archives/nsccl/span/nsccl.20200106.i4.zip
        '    url = Convert.ToString("https://www1.nseindia.com/archives/nsccl/span/") & Fname
        'ElseIf Type = "CURR" Then
        '    url = Convert.ToString("https://www1.nseindia.com/archives/cd/span/") & Fname
        'End If


        If Type = "FO" Then

            'url = "ftp://strategybuilder.finideas.com/FOSpan"

            url = "https://support.finideas.com/FOSpan/"

        ElseIf Type = "CURR" Then
            'url = "ftp://strategybuilder.finideas.com/CurrSpan/"
            url = "https://support.finideas.com/CurrSpan/"

        End If


        Dim filepath As String = Application.StartupPath + "\" + "DownloadSpanFile\"


        Dim filename As String = filepath & Fname


        Dim filepathdir As String = Application.StartupPath + "\" + "DownloadSpanFile\"
        If System.IO.Directory.Exists(filepathdir) Then
            Dim directory As New System.IO.DirectoryInfo(filepathdir)


            For Each file As System.IO.FileInfo In directory.GetFiles()
                Try
                    file.Delete()

                Catch ex As Exception

                End Try

            Next
        End If

        If Not System.IO.Directory.Exists(filepathdir) Then
            System.IO.Directory.CreateDirectory(filepathdir)
        End If
        Dim i As Integer = 0
aa:
        DownloadFileFTP(Fname, url, Application.StartupPath + "\" + "DownloadSpanFile\")
        Dim info2 As New FileInfo(Application.StartupPath + "\" + "DownloadSpanFile\" + Fname)
        Dim length2 As Long = info2.Length

        If Not System.IO.File.Exists(Application.StartupPath + "\" + "DownloadSpanFile\" + Fname) Or length2 = 0 Then
            i = i + 1
            If Type = "FO" Then
                Fname = "nsccl." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i1.zip"
                spanfiledownloadzip = Fname
                'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
                spanfiledownload = "nsccl." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i01.spn"
            ElseIf Type = "CURR" Then
                Fname = "nsccl_x." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i1.zip"
                spanfiledownloadzip = Fname
                'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
                spanfiledownload = "nsccl_x." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i01.spn"
            End If

            GoTo aa
        End If

        'Dim client As New WebClient()

        'Try
        '    Dim uri As New Uri(url)
        '    'client.Headers.Add("Accept: text/html, application/xhtml+xml, */*")
        '    'client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)")
        '    client.DownloadFileAsync(uri, filename)

        '    While client.IsBusy
        '        System.Threading.Thread.Sleep(1000)

        '    End While
        '    'Console.WriteLine(ex.Message);
        'Catch eex As Exception
        '    MsgBox("Error")
        '    'Catch ex As UriFormatException
        'End Try




    End Sub
    'Public Sub DownloadSpanFile(ByVal Fname As String, ByVal Type As String)
    '    Dim url As String = ""
    '    'If Type = "FO" Then
    '    '    '//https://www1.nseindia.com/archives/nsccl/span/nsccl.20200106.i4.zip
    '    '    url = Convert.ToString("https://www1.nseindia.com/archives/nsccl/span/") & Fname
    '    'ElseIf Type = "CURR" Then
    '    '    url = Convert.ToString("https://www1.nseindia.com/archives/cd/span/") & Fname
    '    'End If


    '    If Type = "FO" Then

    '        url = "ftp://strategybuilder.finideas.com/FOSpan"
    '    ElseIf Type = "CURR" Then
    '        url = "ftp://strategybuilder.finideas.com/CurrSpan"
    '    End If


    '    Dim filepath As String = Application.StartupPath + "\" + "DownloadSpanFile\"


    '    Dim filename As String = filepath & Fname


    '    Dim filepathdir As String = Application.StartupPath + "\" + "DownloadSpanFile\"
    '    If System.IO.Directory.Exists(filepathdir) Then
    '        Dim directory As New System.IO.DirectoryInfo(filepathdir)


    '        For Each file As System.IO.FileInfo In directory.GetFiles()
    '            Try
    '                file.Delete()

    '            Catch ex As Exception

    '            End Try

    '        Next
    '    End If

    '    If Not System.IO.Directory.Exists(filepathdir) Then
    '        System.IO.Directory.CreateDirectory(filepathdir)
    '    End If


    '    DownloadFileFTP(Fname, url, Application.StartupPath + "\" + "DownloadSpanFile\")



    '    'Dim client As New WebClient()

    '    'Try
    '    '    Dim uri As New Uri(url)
    '    '    'client.Headers.Add("Accept: text/html, application/xhtml+xml, */*")
    '    '    'client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)")
    '    '    client.DownloadFileAsync(uri, filename)

    '    '    While client.IsBusy
    '    '        System.Threading.Thread.Sleep(1000)

    '    '    End While
    '    '    'Console.WriteLine(ex.Message);
    '    'Catch eex As Exception
    '    '    MsgBox("Error")
    '    '    'Catch ex As UriFormatException
    '    'End Try




    'End Sub
    Public Shared Function DownloadFile(ByVal ftpURL1 As String, ByVal tempDirPath1 As String, ByVal FileNameToDownload As String) As String

        'Dim ftpURL As String = ftpURL1 ' "ftp://strategybuilder.finideas.com/Contract"
        ''Host URL or address of the FTP serve
        'Dim userName As String = "strategybuilder" ' "FI-strategybuilder"
        ''User Name of the FTP server
        'Dim password As String = "finideas#123"
        Dim tempDirPath As String = tempDirPath1 ' Application.StartupPath + "\Contract\"
        'Dim ResponseDescription As String = ""
        Dim PureFileName As String = New FileInfo(FileNameToDownload).Name
        'Dim DownloadedFilePath As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName
        'Dim downloadUrl As String = [String].Format("{0}/{1}", ftpURL, FileNameToDownload)
        'Dim req As FtpWebRequest = DirectCast(FtpWebRequest.Create(downloadUrl), FtpWebRequest)
        'req.Method = WebRequestMethods.Ftp.DownloadFile
        'req.Credentials = New NetworkCredential(userName, password)
        'req.UseBinary = True
        'req.Proxy = Nothing
        'Try
        '    Dim response As FtpWebResponse = DirectCast(req.GetResponse(), FtpWebResponse)
        '    Dim stream As Stream = response.GetResponseStream()
        '    Dim buffer As Byte() = New Byte(2047) {}
        '    Dim fs As New FileStream(DownloadedFilePath, FileMode.Create)
        '    Dim ReadCount As Integer = stream.Read(buffer, 0, buffer.Length)
        '    While ReadCount > 0
        '        fs.Write(buffer, 0, ReadCount)
        '        ReadCount = stream.Read(buffer, 0, buffer.Length)
        '    End While
        '    ResponseDescription = response.StatusDescription
        '    fs.Close()
        '    stream.Close()
        'Catch e As Exception
        '    Console.WriteLine(e.Message)
        'End Try
        'Return ResponseDescription


        Try
            Dim url As String = ""

            Dim filename As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName

            url = ftpURL1 & FileNameToDownload



            Dim client As New WebClient()
            Dim uri As New Uri(url)
            client.Headers.Add("Accept: text/html, application/xhtml+xml, */*")
            client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)")
            client.DownloadFileAsync(uri, filename)

            While client.IsBusy
                System.Threading.Thread.Sleep(1000)
            End While
            'Console.WriteLine(ex.Message);


        Catch ex As UriFormatException
        End Try
        Return ""
    End Function
    Private Sub UpdateCURRSapnFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateCURRSapnFileToolStripMenuItem.Click
        Try

            Me.Cursor = Cursors.WaitCursor
            'nsccl.20160302.i03.spn
            Dim mSPAN_path As String = GdtSettings.Compute("max(SettingKey)", "SettingName='CURRENCY SPAN PATH'").ToString
            'If System.IO.Directory.Exists(mSPAN_path) Then
            '    MsgBox("Install span software..Because" + mSPAN_path + " Not Found")
            '    Return
            'End If
            If Not Directory.Exists(mSPAN_path) Then 'if not correct span software path
                MsgBox("Enter Correct Path for span in setting.")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim fnamecsv As String = "nsccl_x." + DateTime.Now.ToString("yyyyMMdd") + ".i1.zip"
            'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
            'Dim sourcefname As String = "nsccl.20160303.i01.spn"
            Dim sourcefname As String = "nsccl_x." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            spanfiledownload = sourcefname
            spanfiledownloadzip = fnamecsv
            DownloadSpanFile(fnamecsv, "CURR")
            sourcefname = spanfiledownload
            fnamecsv = spanfiledownloadzip
            Dim filepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv
            Dim sourcepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
            ' ZipHelp.UnZip(filepath, Path.GetDirectoryName(filepath), 4096)
            UnZip1(filepath, Path.GetDirectoryName(filepath), 4096)
            ' sourcefname = "nsccl_x." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            sourcepath = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
            File.Copy(sourcepath, mSPAN_path & "\" & sourcefname, True)
            MsgBox("Currency File Updated Successfully on path=" & mSPAN_path)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MsgBox("Currency Span  File Update Error")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub MDI_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        ' mPerf.SetFileName("MdiFrmCloseDbBackupIssue")
        '  mPerf.WriteLogStr("SETT DB BACKUP :" & DB_BACKUP_ON_EXIT.ToString() & " ANYLSIS CANCEL = " & mAnalysisCloseCancel.ToString())

        If DB_BACKUP_ON_EXIT = 1 AndAlso mAnalysisCloseCancel = False Then
            Me.Cursor = Cursors.WaitCursor
            GBwait.Location = New Point((Me.Width / 2) - (GBwait.Width / 2), (Me.Height / 2) - (GBwait.Height / 2))
            GBwait.Visible = True

            Try
                Dim objTrad As trading = New trading
                Dim mBackupPath As String = CStr(IIf(IsDBNull(GdtSettings.Compute("max(SettingKey)", "SettingName='Backup_path'")), "D:\", GdtSettings.Compute("max(SettingKey)", "SettingName='Backup_path'")))
                If Not Directory.Exists(mBackupPath) Then
                    Try
                        Directory.CreateDirectory(mBackupPath)
                    Catch ex As Exception

                    End Try

                End If
                Dim str As String = ConfigurationSettings.AppSettings("dbname")
                Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str

                Dim cur_date_str As String
                cur_date_str = Format(Now, "ddMMMyyyy_HHmm")
                Dim tstr As String = mBackupPath & "\greek_backup_" & cur_date_str & ".mdb"
                Try
                    FileCopy(_connection_string, tstr)
                Catch ex As Exception

                End Try

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.ToString)
            End Try

        End If
        If POSITION_BACKUP_ON_EXIT = 1 Then
            Me.Cursor = Cursors.WaitCursor
            GBwait.Location = New Point((Me.Width / 2) - (GBwait.Width / 2), (Me.Height / 2) - (GBwait.Height / 2))
            GBwait.Visible = True

            Dim mBackupPath As String = CStr(IIf(IsDBNull(GdtSettings.Compute("max(SettingKey)", "SettingName='Backup_path'")), "D:\", GdtSettings.Compute("max(SettingKey)", "SettingName='Backup_path'")))
            If Not Directory.Exists(mBackupPath) Then
                Try
                    Directory.CreateDirectory(mBackupPath)
                Catch ex As Exception

                End Try

            End If
            Dim analysis1 As New rptposition
            analysis1.Loading(e)
            analysis1.ExportFo(True)
            analysis1.exportEQ(True)
            analysis1.exportcurr(True)
            analysis1 = Nothing
        End If
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub fill_Currency()
        Dim CurrTable As DataTable
        Dim dr As DataRow
        Dim count As Integer
        Dim ar As New ArrayList
        Dim table As New DataTable
        table = trd.select_Currency_Trading
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


    Private Sub CFBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CFBalanceToolStripMenuItem.Click
        'For Each frm As Form In Application.OpenForms
        '    If frm.Name.Equals("analysis") Then
        '        frm.Close()
        '        Exit For
        '    End If
        'Next
        Dim objcfbalance As New FrmCFBalance
        '  objcfbalance.Show()
        objcfbalance.ShowForm()


    End Sub

    Private Sub SummaryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub MDI_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        'Me.Update()
    End Sub

    Private Sub Timer_Index_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Index.Tick
        'Dim chkfindMkt As Boolean
        'Dim chkfindanalysis As Boolean

        'For Each frm As Form In Me.MdiChildren
        '    If frm.Text = "Analysis" Then
        '        chkfindanalysis = True
        '    Else
        '        chkfindanalysis = False

        '    End If
        '    If frm.Text = "MarketWatch" Then
        '        chkfindMkt = True
        '    Else
        '        chkfindMkt = False

        '    End If
        'Next

        'If chkfindanalysis = True And chkfindMkt = True Then
        'https://nseindia.com/live_market/dynaContent/live_watch/live_index_watch.htm
        '    If strcompmkt = "NIFTY" Or strcompmkt = "BANKNIFTY" Then
        '        WebBrowser1.Navigate("javascript:getliveindexWatchData();")
        '        strIndex = WebBrowser1.Document.Body.InnerHtml
        '        obj_DelIndex.Invoke(strIndex)
        '    Else
        '        If comptable_Net.Tables(0).Select("company like '*NIFTY*'").Length > 0 Then
        '            'WebBrowser1.Url = New System.Uri("https://nseindia.com/live_market/dynaContent/live_watch/live_index_watch.htm", System.UriKind.Absolute)
        '            WebBrowser1.Navigate("javascript:getliveindexWatchData();")
        '            strIndex = WebBrowser1.Document.Body.InnerHtml
        '            obj_DelIndex.Invoke(strIndex)
        '        End If
        '    End If
        'Else
        '    If chkfindanalysis = True Then
        '        If comptable_Net.Tables(0).Select("company like '*NIFTY*'").Length > 0 Then
        '            'WebBrowser1.Url = New System.Uri("https://nseindia.com/live_market/dynaContent/live_watch/live_index_watch.htm", System.UriKind.Absolute)
        '            WebBrowser1.Navigate("javascript:getliveindexWatchData();")
        '            strIndex = WebBrowser1.Document.Body.InnerHtml
        '            obj_DelIndex.Invoke(strIndex)
        '        End If
        '    End If

        '    If chkfindMkt = True Then
        '        If strcompmkt = "NIFTY" Or strcompmkt = "BANKNIFTY" Then
        '            WebBrowser1.Navigate("javascript:getliveindexWatchData();")
        '            strIndex = WebBrowser1.Document.Body.InnerHtml
        '            obj_DelIndex.Invoke(strIndex)
        '        End If
        '    End If
        'End If


    End Sub

    Private Sub chkAPI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAPI.CheckedChanged
        If NetMode <> "API" Then
            Dim settingname, settingkey, uid As String
            settingname = "NETMODE"
            settingkey = "API"
            settingname = "NETMODE"
            uid = "190"
            trd.Update_setting(settingkey, settingname, uid)
            MsgBox("Setting Successfully Apply")
            For Each frm As Form In Me.MdiChildren
                frm.Close()
            Next
            Me.Dispose()
            CUtils.RestartApplication()
            Return
            Application.Restart()
            Dim _proceses As Process()
            Try
                _proceses = Process.GetProcessesByName("VolHedge")
                For Each proces As Process In _proceses
                    proces.Kill()
                    System.Threading.Thread.Sleep(5000)
                    Process.Start(Application.StartupPath + "\VolHedge.exe")
                Next
            Catch ex As Exception
                MessageBox.Show("Error in process")
            End Try
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

            '=========================REM:Coding For NETMODE SETTING IN SETTING TABLE  17/06/2014===============
        End If
    End Sub

    Private Sub ReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportToolStripMenuItem.Click

    End Sub

    Private Sub chkJL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If NetMode <> "JL" Then

            If frmsetting.ChkSQLConn1() = True Then
                Dim settingname, settingkey, uid As String
                settingname = "NETMODE"
                settingkey = "JL"
                settingname = "NETMODE"
                uid = "190"
                trd.Update_setting(settingkey, settingname, uid)
                MsgBox("Setting Successfully Apply")



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

                Thr_GetInterNetDat.Abort()
                Me.Dispose()

                Application.Restart()
                Dim plist As Process() = Process.GetProcesses()
                For Each p As Process In plist
                    Try
                        If p.MainModule.ModuleName = "VolHedge.exe" Then
                            p.Kill()
                            Process.Start(Application.StartupPath + "\VolHedge.exe")
                        End If

                    Catch
                        'seems listing modules for some processes fails, so better ignore any exceptions here
                    End Try
                Next
            Else
                'MsgBox("Test Connection Fail !! ")
                MsgBox("Change Connection Settings!! ")
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
                'analysis1.ShowDialog()
                analysis1.ShowForm(3)
                analysis.searchcompany()
                SAVEIMPORTSETTING()
            End If

        End If
    End Sub

    Private Sub chkJL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkJL.CheckedChanged
        If NetMode <> "JL" Then

            If frmsetting.ChkSQLConn1() = True Then
                Dim settingname, settingkey, uid As String
                settingname = "NETMODE"
                settingkey = "JL"
                settingname = "NETMODE"
                uid = "190"
                trd.Update_setting(settingkey, settingname, uid)
                MsgBox("Setting Successfully Apply")



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

                Thr_GetInterNetDat.Abort()
                Me.Dispose()
                CUtils.RestartApplication()
                Return
                Application.Restart()
                Dim _proceses As Process()
                Try
                    _proceses = Process.GetProcessesByName("VolHedge")
                    For Each proces As Process In _proceses
                        proces.Kill()
                        System.Threading.Thread.Sleep(5000)
                        Process.Start(Application.StartupPath + "\VolHedge.exe")
                    Next
                Catch ex As Exception
                    MessageBox.Show("Error in process")
                End Try
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
            Else
                'MsgBox("Test Connection Fail !! ")
                MsgBox("Change Connection Settings!! ")
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
                'analysis1.ShowDialog()
                analysis1.ShowForm(3)
                analysis.searchcompany()
                SAVEIMPORTSETTING()
            End If

        End If
    End Sub

    Private Sub Timer_refreshtrade_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_refreshtrade.Tick
        Try
            Dim obj_Color_Last_FOEQ_Entry As Color
            Dim dtFO As DataTable = trd.SelectFO_tradingtime
            Dim dtEQ As DataTable = trd.SelectEQ_tradingtime
            Dim dtCURR As DataTable = trd.SelectCURR_tradingtime
            Dim dtmax As DataTable = trd.Selectmax_tradingtime
            Dim resultsFO As DataRow() = dtFO.Select("")
            Dim resultsEQ As DataRow() = dtEQ.Select("")
            Dim resultsCURR As DataRow() = dtCURR.Select("")
            Dim resultsmax As DataRow() = dtmax.Select("")
            REM Last FO trade Entry Time in String Format
            Dim GVar_Str_FOLastEntryTime As String
            REM Last EQ trade Entry Time in String Format
            Dim GVar_Str_EQLastEntryTime As String
            Dim GVar_Str_CURRLastEntryTime As String
            If resultsmax.Length > 0 Then
                If resultsmax(0).Item("entrydate").ToString() = "" Then
                    Exit Sub

                End If

            End If
            If resultsFO.Length > 0 Then
                '01-Jan-1980
                If resultsFO(0).Item("entrydate1").ToString() = "" Then
                    GVar_Str_FOLastEntryTime = "01-Jan-1980"
                Else
                    GVar_Str_FOLastEntryTime = resultsFO(0).Item("entrydate1").ToString()
                End If

            End If

            If resultsEQ.Length > 0 Then
                If resultsEQ(0).Item("entrydate1").ToString() = "" Then
                    GVar_Str_EQLastEntryTime = "01-Jan-1980"
                Else
                    GVar_Str_EQLastEntryTime = resultsEQ(0).Item("entrydate1").ToString()
                End If

            End If
            If resultsCURR.Length > 0 Then
                If resultsCURR(0).Item("entrydate1").ToString() = "" Then
                    GVar_Str_CURRLastEntryTime = "01-Jan-1980"
                Else
                    GVar_Str_CURRLastEntryTime = resultsCURR(0).Item("entrydate1").ToString()
                End If
            End If

            Dim GVar_Const_DateTimeFormat As String = "dd-MMM-yy hh:mm:ss tt" REM Datatime Format
            Dim StrText As String
            If GVar_Str_FOLastEntryTime = "01-Jan-1980" Then
                StrText = "FO: 00-000-00 00:00:00 AM"
            Else
                If GVar_Str_FOLastEntryTime IsNot Nothing Then
                    StrText = "FO: " & Format(CDate(GVar_Str_FOLastEntryTime), GVar_Const_DateTimeFormat)
                End If
            End If

            If (StrText <> Var_Str_Prev_FO_Entry_Text) Then
                Var_Str_Prev_FO_Entry_Text = StrText
                FOEntryTimeToolStrip.Text = Var_Str_Prev_FO_Entry_Text
            End If

            If GVar_Str_EQLastEntryTime = "01-Jan-1980" Then
                StrText = "EQ: 00-000-00 00:00:00 AM"
            Else
                If GVar_Str_EQLastEntryTime IsNot Nothing Then
                    StrText = "EQ: " & Format(CDate(GVar_Str_EQLastEntryTime), GVar_Const_DateTimeFormat)
                End If
            End If
            If (StrText <> Var_Str_Prev_EQ_Entry_Text) Then
                Var_Str_Prev_EQ_Entry_Text = StrText
                EQEntryTimeToolStrip.Text = Var_Str_Prev_EQ_Entry_Text
            End If
            If GVar_Str_CURRLastEntryTime = "01-Jan-1980" Then
                StrText = "CURR: 00-000-00 00:00:00 AM"
            Else
                If GVar_Str_CURRLastEntryTime IsNot Nothing Then
                    StrText = "CURR: " & Format(CDate(GVar_Str_CURRLastEntryTime), GVar_Const_DateTimeFormat)
                End If
            End If
            If (StrText <> GVar_Str_CURRLastEntryTime) Then
                GVar_Str_CURRLastEntryTime = StrText
                CURREntryTimeToolStrip.Text = GVar_Str_CURRLastEntryTime
            End If

            Dim trdDate As Date '= CDate(GVar_Str_FOLastEntryTime)
            If resultsmax.Length > 0 Then
                If resultsmax(0).Item("entrydate").ToString() = "" Then
                    Exit Sub
                Else
                    trdDate = resultsmax(0).Item("entrydate").ToString()
                End If

            End If
            'If GVar_Str_EQLastEntryTime <> "" Then
            '    If trdDate < CDate(GVar_Str_EQLastEntryTime) Then
            '        trdDate = CDate(GVar_Str_EQLastEntryTime)
            '    End If
            'End If
            'If GVar_Str_CURRLastEntryTime <> "" Then
            '    If trdDate < CDate(GVar_Str_CURRLastEntryTime) Then
            '        trdDate = CDate(GVar_Str_CURRLastEntryTime)
            '    End If
            'End If

            Dim obj_Color As Color
            REM Check Last Trade time is lass then 5 min of Curr System Time then Highlight Trade Lable as Red Back Color else Green Back Color
            If DateDiff(DateInterval.Minute, trdDate, Now) > 5 Then
                obj_Color = Color.Red
            Else
                obj_Color = Color.Green
            End If
            If obj_Color_Last_FOEQ_Entry <> obj_Color Then
                FOEntryTimeToolStrip.BackColor = obj_Color
                EQEntryTimeToolStrip.BackColor = obj_Color
                CURREntryTimeToolStrip.BackColor = obj_Color
                obj_Color_Last_FOEQ_Entry = obj_Color
            End If

            'If results.Length > 0 Then
            '    lblTradeRefresh.Text = "Trade Refresh Time:" & results(0).Item("entrydate")
            '    lblTradeRefresh.BackColor = Color.Green
            '    lblTradeRefresh.ForeColor = Color.White
            'Else
            '    lblTradeRefresh.Text = "Trade Refresh Time:"
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub MarginReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarginReportToolStripMenuItem.Click

        If analysis.mTbl_SPAN_output.Rows.Count < 1 Then
            MessageBox.Show("Please Process Margin")
            Return
        End If

        Dim dview As DataView
        dview = New DataView(analysis.mTbl_SPAN_output.Copy, "", "", DataViewRowState.CurrentRows)

        Dim dtMarginReport As New DataTable
        dtMarginReport = dview.ToTable()
        '   dtMarginReport.Columns.Add("Expiry", GetType(String))
        dtMarginReport.Columns.Add("Company", GetType(String))
        dtMarginReport.Columns.Add("ShortOption", GetType(Double))
        dtMarginReport.Columns.Add("Price", GetType(Double))
        dtMarginReport.Columns.Add("Exp", GetType(Double))
        dtMarginReport.Columns.Add("BuyPrice", GetType(Double))
        dtMarginReport.Columns.Add("BuyUnit", GetType(Double))
        dtMarginReport.Columns.Add("BuyPer", GetType(Double))
        dtMarginReport.Columns.Add("SellPrice", GetType(Double))
        dtMarginReport.Columns.Add("SellUnit", GetType(Double))
        dtMarginReport.Columns.Add("SellPer", GetType(Double))
        dtMarginReport.Columns.Add("ExpMargin", GetType(Double))
        dtMarginReport.Columns.Add("InitMargin", GetType(Double))
        dtMarginReport.Columns.Add("TotalMargin", GetType(Double))
        dtMarginReport.AcceptChanges()
        For Each drow As DataRow In dtMarginReport.Rows
            Try
                '  drow("Expiry") = drow("ClientCode").ToString.Split("/")(1)
                drow("Company") = drow("ClientCode").ToString() 'drow("ClientCode").ToString.Split("/")(0)
                'drow("ShortOption") = maintable.Compute("sum(units)", "cp<>'F' and Client='" & drow("Client") & "' and company='" & drow("Company") & "' and units < 0")
                drow("ShortOption") = maintable.Compute("sum(units)", "cp<>'F' and  company='" & drow("Company") & "' and units < 0")
                drow("Price") = analysis.mTbl_exposure_comp.Compute("Max(p)", "CompName='" & drow("company") & "'") ' and fut_opt='OPT'

                drow("Exp") = dtMarginReport.Compute("max(exposure_margin)", "ClientCode = '" & drow("ClientCode") & "'")

                drow("BuyPrice") = drow("BuyPrice")
                drow("BuyUnit") = drow("BuyUnit")
                drow("BuyPer") = drow("BuyPer")

                drow("SellPrice") = drow("SellPrice")
                drow("SellUnit") = drow("SellUnit")
                drow("SellPer") = drow("SellPer")


                drow("ExpMargin") = drow("exposure_margin")
                Dim spanreq As Double = dtMarginReport.Compute("max(spanreq)", "ClientCode = '" & drow("ClientCode") & "'")
                Dim anov As Double = dtMarginReport.Compute("max(anov)", "ClientCode = '" & drow("ClientCode") & "'")
                drow("InitMargin") = Format(Val(spanreq.ToString) - Val(anov.ToString), inmargstr)

                drow("TotalMargin") = drow("ExpMargin") + drow("InitMargin")

            Catch ex As Exception

            End Try
        Next

        Dim RptObj As New rptMarginReport
        RptObj.ShowForm(dtMarginReport)
    End Sub

    Private Sub IssueReportingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IssueReportingToolStripMenuItem.Click
        Try
            Form_Issue_Reporting.ShowDialog()
            'System.Diagnostics.Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSdDvRsE7GIBHgxNZo86-GuoW8_LwZrxCcHABX8eRDWs4ym0SA/viewform?c=0&w=1")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DownloadSpanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadSpanToolStripMenuItem.Click
        Dim savedi As New FolderBrowserDialog
        If savedi.ShowDialog() <> Windows.Forms.DialogResult.Cancel Then
            Dim path As String = savedi.SelectedPath
            Me.Cursor = Cursors.WaitCursor
            'DownloadFile("ftp://strategybuilder.finideas.com/SPAN", path, "PC-Span_4.5_608.msi")
            DownloadFile("https://support.finideas.com/SPAN/", path, "PC-Span_4.5_608.msi")
            MessageBox.Show("Sapn Setup Download SuccessFully..")
            Me.Cursor = Cursors.Default
            Dim VarExplorerArg As String = "/select, " & path & "\PC-Span_4.5_608.msi"
            Shell("Explorer.exe " + VarExplorerArg, AppWinStyle.NormalFocus, True, -1)
        End If
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click

    End Sub

    Private Sub MWToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MWToolStripMenuItem.Click
        If NetMode = "API" Then


            For Each frm As Form In Me.MdiChildren
                frm.Close()
            Next

        End If
        'Dim mkt1 As New MarketWatch 'findvol

        'If MarketWatch.chkfindMkt = False Then
        '    mkt1.MdiParent = Me
        '    mkt1.Show()
        'Else
        '    mkt1.Dispose()
        'End If

        frmMarketwatch.MdiParent = Me
        frmMarketwatch.Show()
    End Sub

    Private Sub SaveDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveDataToolStripMenuItem.Click

        Me.Cursor = Cursors.WaitCursor
        applicationCrash()
        Me.Cursor = Cursors.Default
        'If analysis.cmdStart.Text = "Start" Then
        For pageNo = analysis.tbcomp.TabPages.Count - 1 To 0 Step -1
            analysis.tbcomp.SelectedTab = analysis.tbcomp.TabPages(pageNo)
        Next

        analysis.cmdsave_Click()
        analysis.ReFresh_Maintable()
        SaveAnaDataFlag = True
        analysis.save_data(maintable)
        MessageBox.Show("Data Saved Successfully !!")


        'Else
        'MessageBox.Show("Please Stop Broadcast to Save Data !!")
        'End If


        'ReFresh_Maintable()
        'If SAVE_DATA_AUTO = 1 Then
        'Call save_data(maintable)
        'End If

        'chkanalysis = False
        'objanalysis = Nothing
        'refreshstarted = False
    End Sub
    Public Sub DownloadSpanFilebhavcopy(ByVal Fname As String)
        Try


            Dim url As String = ""

            url = "https://support.finideas.com/Bhavcopy/"

            Dim filepath As String = Application.StartupPath + "\" + "DownloadBhavcopy\"


            Dim filename As String = filepath & Fname


            Dim filepathdir As String = Application.StartupPath + "\" + "DownloadBhavcopy\"
            If System.IO.Directory.Exists(filepathdir) Then
                Dim directory As New System.IO.DirectoryInfo(filepathdir)


                For Each file As System.IO.FileInfo In directory.GetFiles()
                    Try
                        file.Delete()

                    Catch ex As Exception

                    End Try

                Next
            End If

            If Not System.IO.Directory.Exists(filepathdir) Then
                System.IO.Directory.CreateDirectory(filepathdir)
            End If
            Dim i As Integer = 1
            Dim sourcefname As String = ""
            If NEW_BHAVCOPY = 1 Then
                sourcefname = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"
            Else
                sourcefname = "fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"

            End If

aa:
            DownloadFileFTP(Fname, url, Application.StartupPath + "\" + "DownloadBhavcopy\")
            Dim info2 As New FileInfo(Application.StartupPath + "\" + "DownloadBhavcopy\" + sourcefname)
            Dim length2 As Long = info2.Length
            'Dim filepath1 As String = Application.StartupPath + "\" + "DownloadBhavcopy\" + fnamecsv
            Dim sourcepath As String = Application.StartupPath + "\" + "DownloadBhavcopy\" + sourcefname
            If Not System.IO.File.Exists(sourcepath) Or length2 = 0 Then
                i = i + 1
                If NEW_BHAVCOPY = 1 Then
                    Fname = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"
                    sourcefname = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"

                Else

                    Fname = "fo" + DateTime.Now.AddDays(-1 * i).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"
                    sourcefname = "fo" + DateTime.Now.AddDays(-1 * i).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"
                End If
                GoTo aa


                'Else
                '    Dim info2 As New FileInfo(Application.StartupPath + "\" + "DownloadBhavcopy\" + sourcefname)
                '    Dim length2 As Long = info2.Length
                '    If length2 = 0 Then
                '        i = i + 1
                '        Fname = "fo" + DateTime.Now.AddDays(-1 * i).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"
                '        sourcefname = "fo" + DateTime.Now.AddDays(-1 * i).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"

                '        GoTo aa
                '    End If
            End If




            downloadbhavcopy = Fname.Replace(".zip", "")
            ' ZipHelp.UnZip(filepath, Path.GetDirectoryName(filepath), 4096)
            UnZip1(sourcepath, Path.GetDirectoryName(sourcepath), 4096)
            sourcepath = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname

        Catch ex As Exception
            MsgBox("Bhavcopy Update Error..")
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Public Sub DownloadSpanFile(ByVal Fname As String)
        Dim url As String = ""
        'If Type = "FO" Then
        '    '//https://www1.nseindia.com/archives/nsccl/span/nsccl.20200106.i4.zip
        '    url = Convert.ToString("https://www1.nseindia.com/archives/nsccl/span/") & Fname
        'ElseIf Type = "CURR" Then
        '    url = Convert.ToString("https://www1.nseindia.com/archives/cd/span/") & Fname
        'End If



        'url = "ftp://strategybuilder.finideas.com/AEL/"
        url = "https://support.finideas.com/AEL/"



        Dim filepath As String = Application.StartupPath + "\" + "DownloadAELFile\"


        Dim filename As String = filepath & Fname


        Dim filepathdir As String = Application.StartupPath + "\" + "DownloadAELFile\"
        If System.IO.Directory.Exists(filepathdir) Then
            Dim directory As New System.IO.DirectoryInfo(filepathdir)


            For Each file As System.IO.FileInfo In directory.GetFiles()
                Try
                    file.Delete()

                Catch ex As Exception

                End Try

            Next
        End If

        If Not System.IO.Directory.Exists(filepathdir) Then
            System.IO.Directory.CreateDirectory(filepathdir)
        End If

        Dim i As Integer = 0
aa:
        DownloadFileFTP(Fname, url, Application.StartupPath + "\" + "DownloadAELFile\")


        If Not System.IO.File.Exists(Application.StartupPath + "\" + "DownloadAELFile\" + Fname) Then
            i = i + 1

            'Fname = "nsccl." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i1.zip"
            Fname = "ael_" + DateTime.Now.AddDays(-1 * i).ToString("ddMMyyyy") + ".csv"
            aelfiledownloadzip = Fname
            'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
            'spanfiledownload = "nsccl." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i01.spn"


            GoTo aa
        End If





    End Sub
    Private Sub UpdateAllFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UpdateAllFileToolStripMenuItem.Click


        Try

            Me.Cursor = Cursors.WaitCursor
            DownloadContract()
            Dim mSPAN_path As String = GdtSettings.Compute("max(SettingKey)", "SettingName='SPAN_path'").ToString
            'If System.IO.Directory.Exists(mSPAN_path) Then
            '    MsgBox("Install span software..Because" + mSPAN_path + " Not Found")
            '    Return
            'End If
            If Not Directory.Exists(mSPAN_path) Then 'if not correct span software path
                MsgBox("Enter Correct Path for span in setting.")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            Dim fnamecsv As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i1.zip"
            'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
            Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            spanfiledownload = sourcefname
            spanfiledownloadzip = fnamecsv
            DownloadSpanFile(fnamecsv, "FO")
            sourcefname = spanfiledownload
            fnamecsv = spanfiledownloadzip
            Dim filepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv
            Dim sourcepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
            'If File.Exists(Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv) Then 'if not correct span software path
            '    File.Delete(Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv)
            'End If
            '   ZipHelp.UnZip(filepath, Path.GetDirectoryName(filepath), 4096)
            UnZip1(filepath, Path.GetDirectoryName(filepath), 4096)
            'sourcefname = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            sourcepath = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
            File.Copy(sourcepath, mSPAN_path & "\" & sourcefname, True)
            '  MsgBox("Span Fo File Updated Successfully on path=" & mSPAN_path)

            AutoFilemsg = AutoFilemsg + vbNewLine + "Span Fo File Updated Successfully on path=" & mSPAN_path
            Me.Cursor = Cursors.Default

        Catch ex As Exception

            'MsgBox("Fo Span  File Update Error..")
            AutoFilemsg = AutoFilemsg + vbNewLine + "Fo Span  File Update Error.."
            Me.Cursor = Cursors.Default
        End Try



        Try

            Me.Cursor = Cursors.WaitCursor
            'nsccl.20160302.i03.spn
            Dim mSPAN_path As String = GdtSettings.Compute("max(SettingKey)", "SettingName='CURRENCY SPAN PATH'").ToString
            'If System.IO.Directory.Exists(mSPAN_path) Then
            '    MsgBox("Install span software..Because" + mSPAN_path + " Not Found")
            '    Return
            'End If
            If Not Directory.Exists(mSPAN_path) Then 'if not correct span software path
                ' MsgBox("Enter Correct Path for span in setting.")
                AutoFilemsg = AutoFilemsg + vbNewLine + "Enter Correct Path for span in setting."
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim fnamecsv As String = "nsccl_x." + DateTime.Now.ToString("yyyyMMdd") + ".i1.zip"
            'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
            'Dim sourcefname As String = "nsccl.20160303.i01.spn"
            Dim sourcefname As String = "nsccl_x." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            spanfiledownload = sourcefname
            spanfiledownloadzip = fnamecsv
            DownloadSpanFile(fnamecsv, "CURR")
            sourcefname = spanfiledownload
            fnamecsv = spanfiledownloadzip
            Dim filepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv
            Dim sourcepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
            ' ZipHelp.UnZip(filepath, Path.GetDirectoryName(filepath), 4096)
            UnZip1(filepath, Path.GetDirectoryName(filepath), 4096)
            ' sourcefname = "nsccl_x." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            sourcepath = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
            File.Copy(sourcepath, mSPAN_path & "\" & sourcefname, True)
            ' MsgBox("Currency File Updated Successfully on path=" & mSPAN_path)
            AutoFilemsg = AutoFilemsg + vbNewLine + "Currency File Updated Successfully on path=" & mSPAN_path

        Catch ex As Exception
            ' MsgBox("Currency Span  File Update Error")
            AutoFilemsg = AutoFilemsg + vbNewLine + "Currency Span  File Update Error"

        End Try


        Try
            Me.Cursor = Cursors.WaitCursor

            Dim fnamecsv As String = "ael_" + DateTime.Now.ToString("ddMMyyyy") + ".csv"
            Dim filepath As String = Application.StartupPath + "\" + "DownloadAELFile\" + fnamecsv
            aelfiledownloadzip = fnamecsv
            DownloadSpanFile(fnamecsv)
            fnamecsv = aelfiledownloadzip
            filepath = Application.StartupPath + "\" + "DownloadAELFile\" + fnamecsv
            Dim fi As New FileInfo(filepath)

            Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName

            Dim objConn As New OleDbConnection(sConnectionString)

            objConn.Open()

            'Dim objCmdSelect As New OleDbCommand
            Dim objAdapter1 As New OleDbDataAdapter("SELECT * FROM " & fi.Name, objConn)
            'objAdapter1.SelectCommand = objCmdSelect
            Dim tempdata As DataTable
            tempdata = New DataTable
            objAdapter1.Fill(tempdata)
            objConn.Close()

            '   margin_table_new.Rows.Clear()
            trd.delete_Exposure_margin_new()


            'Dim msrno As Integer
            Dim mSymbol As String
            Dim mInsType As String
            Dim mNorm_Margin As String
            Dim mAdd_Margin As String
            Dim mTotal_Margin As String

            If tempdata.Rows.Count > 0 Then
                For Each drow As DataRow In tempdata.Rows

                    mSymbol = CStr(drow(1))
                    mInsType = CStr(drow(2))
                    mNorm_Margin = Val(drow(3))
                    mAdd_Margin = Val(drow(4))
                    mTotal_Margin = Val(drow(5))

                    'Dim chk As Boolean = False
                    'For Each mrow As DataRow In margin_table_new.Select("Symbol='" & mSymbol & "'")
                    '    chk = True
                    '    Exit For
                    'Next
                    'If chk = False Then
                    trd.Insert_Exposure_margin_new(mSymbol, mInsType, Val(mNorm_Margin), Val(mAdd_Margin), Val(mTotal_Margin))
                    'Else
                    ' objTrad.update_Exposure_margin_new(mSymbol, mInsType, Val(mNorm_Margin), Val(mAdd_Margin), Val(mTotal_Margin))
                    ' End If

                Next
                'fill_grid()


                ' MsgBox("AEL Import Completed.", MsgBoxStyle.Information)
                AutoFilemsg = AutoFilemsg + vbNewLine + "AEL Import Completed."
            Else
                'MsgBox("AEL Import Failed.", MsgBoxStyle.Critical)
                AutoFilemsg = AutoFilemsg + vbNewLine + "AEL Import Failed."
            End If

            Try

                Me.Cursor = Cursors.WaitCursor

                Dim fnamecsvbhav As String = "fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"
                downloadbhavcopy = fnamecsvbhav
                'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
                'fo16JUN2021bhav.csv.zip()
                DownloadSpanFilebhavcopy(fnamecsvbhav)

                removebhavcopy()
                ' Dim str As String = Application.StartupPath + "\" + "DownloadBhavcopy\" + "fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv"


                read_filebhav(Application.StartupPath + "\" + "DownloadBhavcopy\" + downloadbhavcopy)
            Catch ex As Exception
                Me.Cursor = Cursors.Default
            End Try
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
        Me.Cursor = Cursors.Default
        MsgBox(AutoFilemsg, MsgBoxStyle.Information)



    End Sub
    Public Sub removebhavcopy()
        Dim Objbhavcopy1 As New bhav_copy
        GdtBhavcopy = Objbhavcopy1.select_data()
        Dim dt As DataTable = New DataView(GdtBhavcopy, "", "entry_date ASC", DataViewRowState.CurrentRows).ToTable(True, "entry_date")
        If dt.Rows.Count >= BHAVCOPYPROCESSDAY Then

            For i As Integer = 0 To dt.Rows.Count - BHAVCOPYPROCESSDAY - 1
                Dim entrydatebhav As Date = CDate(dt.Rows(i)(0))
                RemoveBhavcopy(entrydatebhav)
            Next

        End If
    End Sub
    Public Function RemoveBhavcopy(ByVal entry_date As Date) As DataTable
        Dim SP_delete_bhavcopy_Date As String = "delete_bhavcopy_Date"
        Try
            data_access.ParamClear()
            data_access.AddParam("@date1", OleDbType.Date, 50, CDate(entry_date))
            data_access.Cmd_Text = SP_delete_bhavcopy_Date
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            '  MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Private Sub read_filebhav(ByVal path As String)
        REM Change In processing Bhavcopy and selecting parameters, Decimal Strike Prices are properly displayed for the records 
        Dim tempdata As DataTable
        Dim objTrad As trading = New trading
        Dim Mrateofinterast As Double = 0
        Dim DtBCP As DataTable
        Try

            Dim fpath As String
            fpath = CStr(path)
            Mrateofinterast = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
            If fpath <> "" Then
                Dim fi As New FileInfo(fpath)
                Dim dv As DataView
                tempdata = New DataTable
                DtBCP = New DataTable
                tempdata.Columns.Add("script")
                tempdata.Columns.Add("vol")
                tempdata.Columns.Add("futval")
                tempdata.Columns.Add("mt")
                tempdata.Columns.Add("iscall")
                tempdata.AcceptChanges()

                'Call Proc_Data_FromBhavCopyCsv(fpath)
                Dim impBHav As ImportData.ImportOperation
                impBHav = New ImportData.ImportOperation
                If NEW_BHAVCOPY = 1 Then
                    import_Data.CopyToData(fpath, "BHAVCOPYNEW")
                Else
                    import_Data.CopyToData(fpath, "BHAVCOPY")
                End If
                Call impBHav.ImportBhavCopy()

                impBHav = Nothing
                Dim Objbhavcopy1 As New bhav_copy
                DtBCP = Objbhavcopy1.select_TblBhavCopy()

                tempdata.Merge(DtBCP)
                '-----------------------------------------------------------------------
                '' ''Dim fi As New FileInfo(fpath)
                ' ''Dim dv As DataView
                ' ''Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
                '' ''Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;IMEX=1;Data Source=" & fi.DirectoryName
                '' '';HDR=Yes;FMT=Delimited

                ' ''Dim objConn As New OleDbConnection(sConnectionString)

                ' ''objConn.Open()

                ' ''Dim objCmdSelect As New OleDbCommand("SELECT INSTRUMENT,SYMBOL,EXPIRY_DT,STRIKE_PR,OPTION_TYP,SETTLE_PR,CONTRACTS,VAL_INLAKH,TIMESTAMP FROM " & fi.Name, objConn)

                ' ''Dim objAdapter1 As New OleDbDataAdapter

                ' ''objAdapter1.SelectCommand = objCmdSelect

                ' ''tempdata = New DataTable
                ' ''tempdata.Columns.Add("script")
                ' ''tempdata.Columns.Add("vol")
                ' ''tempdata.Columns.Add("futval")
                ' ''tempdata.Columns.Add("mt")
                ' ''tempdata.Columns.Add("iscall")
                ' ''tempdata.AcceptChanges()

                '-----------------------------------------------------------------------

                Dim mt As Double
                Dim futval As Double
                Dim iscall As Boolean
                Dim drow As DataRow
                'objAdapter1.Fill(tempdata)
                'objConn.Close()

                'dv = New DataView(tempdata, "OPTION_TYP='XX'", "", DataViewRowState.OriginalRows)
                dv = New DataView(tempdata, "option_typ='XX'", "", DataViewRowState.CurrentRows)
                'dv = New DataView(Dt, "OPTION_TYP='XX'", "", DataViewRowState.OriginalRows)
                Dim str(2) As String
                str(0) = "EXPIRY_DT"
                str(1) = "SETTLE_PR"
                str(2) = "SYMBOL"
                Dim tdata As New DataTable
                tdata = dv.ToTable(True, str)
                Dim row As DataRow
                Dim script As String
                For Each drow In tempdata.Rows
                    If drow("option_typ") = "XX" Then
                        script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy")
                        drow("script") = UCase(script.Trim)
                        drow("vol") = 0
                        drow("futval") = 0
                        drow("mt") = 0
                    Else
                        script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy") & "  " & Format(Val(drow("STRIKE_PR")), "###0.00") & "  " & drow("OPTION_TYP")
                        'script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(drow("EXPIRY_DT"), "ddMMMyyyy") & "  " & Format(Val(drow("STRIKE_PR")), "####.##") & "  " & drow("OPTION_TYP")
                        drow("script") = UCase(script.Trim)
                        futval = 0
                        drow("vol") = 0
                        For Each row In tdata.Select(" EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' ")
                            futval = row("SETTLE_PR")
                        Next
                        'futval = Val(tempdata.Compute("Max(SETTLE_PR)", " EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' And option_typ='XX'").ToString() & "")
                        'row("SETTLE_PR")

                        If Mid(drow("OPTION_TYP"), 1, 1) = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        Dim ccdate As Date = CDate(drow("TIMESTAMP").ToString.Replace("-", "/"))
                        mt = DateDiff(DateInterval.Day, ccdate.Date, CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")).Date)
                        If ccdate.Date = CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")).Date Then
                            mt = 0.5
                        End If
                        If mt = 0 Then
                            mt = 0.0001
                        Else
                            mt = (mt) / 365
                        End If
                        If futval > 0 Then
                            drow("vol") = Vol(futval, Val(drow("STRIKE_PR")), Val(drow("SETTLE_PR")), mt, iscall, True) * 100
                        End If
                        drow("futval") = futval
                        drow("mt") = mt
                        drow("iscall") = iscall
                    End If
                Next
                tempdata.AcceptChanges()
                Objbhavcopy1.insertNew(tempdata)

                GdtBhavcopy = Objbhavcopy1.select_data()
                GVarIsNewBhavcopy = True
                BhavCopyFlag = True

                Dim Item As DictionaryEntry
                Dim ArrFKey As New ArrayList
                Dim ArrCPKey As New ArrayList
                Dim ArrEKey As New ArrayList
                Dim VaLLTPPrice As New Double
                For Each Item In fltpprice
                    ArrFKey.Add(Item.Key)
                Next
                For Each Item In ltpprice
                    ArrCPKey.Add(Item.Key)
                Next
                For Each Item In eltpprice
                    ArrEKey.Add(Item.Key)
                Next
                For i As Integer = 0 To ArrFKey.Count - 1
                    If cpfmaster.Select("token=" & ArrFKey(i) & "").Length > 0 Then
                        Dim VarScript As String = cpfmaster.Select("token=" & ArrFKey(i) & "")(0).Item("Script")
                        If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                            fltpprice(ArrFKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        End If
                    End If
                Next
                For i As Integer = 0 To ArrCPKey.Count - 1
                    If cpfmaster.Select("token=" & ArrCPKey(i) & "").Length > 0 Then
                        Dim VarScript As String = cpfmaster.Select("token=" & ArrCPKey(i) & "")(0).Item("Script")
                        If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                            fltpprice(ArrCPKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        End If
                    End If
                Next
                For i As Integer = 0 To ArrEKey.Count - 1
                    If eqmaster.Select("token=" & ArrEKey(i) & "").Length > 0 Then
                        Dim VarScript As String = eqmaster.Select("token=" & ArrEKey(i) & "")(0).Item("Script")
                        If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                            fltpprice(ArrEKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        End If
                    End If
                Next

                If analysis.chkanalysis = True Then
                    Call analysis.AssignBhavcopyLTP(True)
                End If


                'MsgBox("Bhavcopy Processed Successfully.", MsgBoxStyle.Information)
                AutoFilemsg = AutoFilemsg + vbNewLine + "Bhavcopy Processed Successfully."
            End If
        Catch ex As Exception

            'MsgBox("Bhavcopy Not Processed.", MsgBoxStyle.Information)
            AutoFilemsg = AutoFilemsg + vbNewLine + "Bhavcopy Not Processed."
            'MsgBox("Select valid file")
            '/MsgBox(ex.ToString)
        End Try
    End Sub
    Private Function Vol(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal _mt As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean) As Double

        Dim tmpcpprice As Double = 0
        Dim mVolatility As Double
        tmpcpprice = cpprice
        'IF MATURITY IS 0 THEN _MT = 0.00001 
        mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
        Return mVolatility
    End Function

    Private Sub GAPUPDownFormToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GAPUPDownFormToolStripMenuItem.Click
        Dim analysis1 As New frmGapUpDown
        analysis1.ShowDialog()
    End Sub


    Private Sub ImportAllFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ImportAllFileToolStripMenuItem.Click
        CloseAllForms()
        Dim analysis1 As New FrmImportAllfile
        analysis1.ShowDialog()
    End Sub

    Private Sub MDI_LocationChanged(sender As Object, e As System.EventArgs) Handles Me.LocationChanged

    End Sub

    Private Sub ATMVOLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ATMVOLToolStripMenuItem.Click
        'Dim analysis1 As New AtmvolWatch
        'analysis1.ShowDialog()
        VolHedge.AtmvolWatch.Show()
    End Sub

    Private Sub VolHedgeAPIPlanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VolHedgeAPIPlanToolStripMenuItem.Click
        Try
            System.Diagnostics.Process.Start("https://fintester.finideas.com/VolHedgePayment.aspx?M='" + IMothrBoardSrNo + "'&H='" + IHDDSrNoStr + "'&P='" + IProcessorSrNoStr + "'&User='" + user + "'&password='" + password + "'&Pname='" + gstr_ProductName + "'")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AdditionalAELExposureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdditionalAELExposureToolStripMenuItem.Click
        Dim analysis1 As New FormAdditionalExpo
        analysis1.ShowDialog()
    End Sub

    Dim IsStopBroadCastManually As Boolean = False
    Private Sub tsmiDataStop_Click(sender As Object, e As EventArgs) Handles tsmiStopData.Click
        IsStopBroadCastManually = Not IsStopBroadCastManually
    End Sub
    Private Sub UniqueLogin_Tick(sender As Object, e As EventArgs) Handles timUserCheck.Tick
        Dim loggedInId As String = clsGlobal.LoginId

        If IsInternetAvailable() Then
            If String.IsNullOrEmpty(clsGlobal.LoginUser) = False Then
                CheckLoginId()
            End If
        End If
    End Sub
    Private Function IsInternetAvailable() As Boolean
        Try
            Dim ping As New Ping()
            Dim reply As PingReply = ping.Send("8.8.8.8", 5000) ' 5 seconds timeout (5000 ms)
            If reply.Status = IPStatus.Success Then
                Return True
            End If
        Catch
            ' Ignore exceptions and return False
        End Try
        Return False
    End Function
    Public Function CheckLoginId() As String

        Dim loginId As String = clsGlobal.LoginId
        Dim Username As String = clsGlobal.UserName1

        Dim dtlogin As DataTable
        dtlogin = ObjLoginData.LoginData(False, Username, clsUEnDe.FEnc(password))

        If dtlogin Is Nothing Then
            Return ""
        End If

        If dtlogin.Rows.Count > 0 Then


            Dim storedLoginId As String = dtlogin.Rows(0)(dtlogin.Columns.Count - 1).ToString()

            'Dim storedLoginId1 As String = clsUEnDe.FEnc(storedLoginId)

            '            mPerf.Write_DiffMs("MultiLogin", " Db LoginID :" & storedLoginId & " : FormLogin ID :" & loginId & " Ms :")

            If storedLoginId <> loginId Then
                timUserCheck.Enabled = False
                timUserCheck.Stop()

                MessageBox.Show("Logged in from another device")
                Console.WriteLine("User logged out due to mismatched login ID.")
                Me.Cursor = Cursors.Default
                clsGlobal.ForceShutdown = True
                Application.Exit()

            End If
        Else

            Console.WriteLine("No user data found.")
        End If



        Return loginId
    End Function


    Dim mFrmOnlineSupportDn As FrmOnlineSupportDownloader
    Private Sub UltraviewerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UltraviewerToolStripMenuItem.Click
        If mFrmOnlineSupportDn Is Nothing Then
            mFrmOnlineSupportDn = New FrmOnlineSupportDownloader()
        Else
            Try
                mFrmOnlineSupportDn.Show()
            Catch ex As Exception
                mFrmOnlineSupportDn = New FrmOnlineSupportDownloader()
            End Try
        End If
        mFrmOnlineSupportDn.Show()
        mFrmOnlineSupportDn.Activate()
    End Sub

    Public Sub CloseAllForms()
        For Each frm As Form In Me.MdiChildren
            frm.Close()
        Next
    End Sub



    Dim mFrmIndexView As FrmBseIndexView
    Private Sub tsmiMarketIndex_Click(sender As Object, e As EventArgs) Handles tsmiMarketIndex.Click

        If mFrmIndexView Is Nothing Then
            mFrmIndexView = New FrmBseIndexView()
        Else
            Try
                mFrmIndexView.Show()
            Catch ex As Exception
                mFrmIndexView = New FrmBseIndexView()
            End Try
        End If
        mFrmIndexView.Show()
        mFrmIndexView.Activate()
    End Sub

    Dim mFrmMarginBse As FrmMarginBse
    'Private Sub tsmiExchangeMargin_Click(sender As Object, e As EventArgs) Handles tsmiExchangeMargin.Click

    '    If mFrmMarginBse Is Nothing Then
    '        mFrmMarginBse = New FrmMarginBse()
    '    Else
    '        Try
    '            mFrmMarginBse.Show()
    '        Catch ex As Exception
    '            mFrmMarginBse = New FrmMarginBse()
    '        End Try
    '    End If
    '    mFrmMarginBse.Show()
    '    mFrmMarginBse.Activate()
    'End Sub


End Class

'Private Sub SynthFutureSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SynthFutureSettingsToolStripMenuItem.Click
'    If mFrmSynthSetting Is Nothing Then
'        mFrmSynthSetting = New FrmSynthFutSettings()
'    Else
'        Try
'            mFrmSynthSetting.Show()
'        Catch ex As Exception
'            mFrmSynthSetting = New FrmSynthFutSettings()
'        End Try
'    End If
'    mFrmSynthSetting.Show()
'    mFrmSynthSetting.Activate()

'End Sub
