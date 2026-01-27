Imports Microsoft.Win32
Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports system.Threading
Imports VolHedge.DAL
Imports System.Data
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Net.Mail
Imports System.IO

Public NotInheritable Class SplashScreenUsrLic
    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).
    Dim VarIsAMC As Boolean = False
    Dim VarAppVersion As String = ""
    Dim startFlg As Boolean = False

    Dim IMothrBoardSrNo As String = "0"
    Dim IHDDSrNoStr As String = "0"
    Dim IProcessorSrNoStr As String = "0"


    'Dim DTUserMasterde As New DataTable
    Dim DTUserMasterde As New DataTable

    'Public GVar_Tmp_Expiry As Date = DateAndTime.DateSerial(2016, 8, 2)

    <DllImport("UniqueID.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Function GetProcessorSeialNumber(ByVal str As Boolean) As String
    End Function
    <DllImport("UniqueID.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Function LoadDiskInfo() As String
    End Function

    <DllImport("Code.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Function GenerateKey(ByVal strKey1 As String, ByVal strKey2 As String) As String
    End Function
    <DllImport("Code.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Function GenerateActKey(ByVal str As String) As String
    End Function
    <DllImport("ReadLicence.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Sub ReadServiceFile()
    End Sub
    <DllImport("ReadLicence.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Function GetActkey(ByVal StrUserCode As String) As String
    End Function
    <DllImport("ReadLicence.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Function GetExpiryDate(ByVal StrUserCode As String) As String
    End Function
    <DllImport("ReadLicence.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Function GetAMCCheck(ByVal StrUserCode As String) As String
    End Function
    <DllImport("ReadLicence.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Function GetLicenceVersion(ByVal StrUserCode As String) As String
    End Function
    <DllImport("ReadLicence.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Function IsFound(ByVal StrUserCode As String) As Boolean
    End Function
    <DllImport("ReadLicence.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Function GetNoOfDealer(ByVal StrUserCode As String) As String
    End Function

    'Private Sub wait(ByVal interval As Integer)
    '    Dim sw As New Stopwatch
    '    sw.Start()
    '    Do While sw.ElapsedMilliseconds < interval
    '        ' Allows UI to remain responsive
    '        ' Application.DoEvents()
    '        SendKeys.Flush()
    '    Loop
    '    sw.Stop()
    'End Sub


    ''' <summary>
    '''  getmbserial
    ''' </summary>
    ''' <returns>Return Motherboard Serial No.</returns>
    ''' <remarks>Get Serial Number of Mother Board </remarks>
    Private Function getmbserial() As String
        REM Read Serial No. From Mother-Board
        Dim searcher As New ManagementObjectSearcher("SELECT  SerialNumber FROM Win32_BaseBoard")
        Dim information As ManagementObjectCollection = searcher.[Get]()
        For Each obj As ManagementObject In information
            For Each data As PropertyData In obj.Properties
                'Console.WriteLine("{0} = {1}", data.Name, data.Value)
                'Return data.Value
                Return data.Value
            Next
        Next
        REM End
        searcher.Dispose()
    End Function

    Private Sub SplashScreenUsrLic_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed

    End Sub

    Private Sub SplashScreenUsrLic_ForeColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ForeColorChanged

    End Sub

    Private Sub SplashScreenUsrLic_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            'If GBoxUserMaster.Visible Then
            SendKeys.Send("{Tab}")
            'Else
            '    Call btnLogIn_Click(btnLogIn, New EventArgs)
            'End If

        ElseIf e.KeyCode = Keys.Escape Then
            'If GBoxUserMaster.Visible Then
            Call btnCancel_Click(btnCancel, New EventArgs)
            'Else

            'End If
        End If
    End Sub

    ''' <summary>
    ''' SplashScreen1_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>WHen this method call to Timer Enable</remarks>
    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Call Rounddata()
            If System.IO.File.Exists(Application.StartupPath & "\SqlServerConnection.txt") Then
                chkOMI.Visible = True
                Label24.Visible = True
            Else
                chkOMI.Visible = False
                Label24.Visible = False
            End If
            '    Call Rounddata_Hash()
            'Dim Thr_Filltoken As New Thread(AddressOf fill_token_thr)
            'Thr_Filltoken.Start()
            Write_TimeLog1("VolHedge Start..")
            'GBLOGIN1.Visible = False
            'btnregister.Visible = False
            'Label4.Visible = False
            'wait(1000)
            'Try

            '    If My.Computer.Name.ToUpper.Contains("TEJAS_PATELA") Or My.Computer.Name.ToUpper.Contains("DEVANGA") Or My.Computer.Name.ToUpper.Contains("SHAILESH2A") Or My.Computer.Name.ToUpper.Contains("SAPANA") Or My.Computer.Name.ToUpper.Contains("DHARMESH") Or My.Computer.Name.ToUpper.Contains("MOHSIN") Then


            '        Dim FSErrorLogFile As System.IO.StreamWriter
            '        'Try
            '        IO.File.Delete("C:\WINDOWS\system32\drivers\etc\hosts")
            '        FSErrorLogFile = New IO.StreamWriter("C:\WINDOWS\system32\drivers\etc\hosts", True)
            '        'Catch ex As Exception
            '        'FSErrorLogFile = New StreamWriter(Application.StartupPath & "\EasyRMS Error Log.txt", True)
            '        'End Try
            '        FSErrorLogFile.WriteLine("127.0.0.1 www.facebook.com localhost")
            '        FSErrorLogFile.WriteLine("")
            '        FSErrorLogFile.WriteLine("127.0.0.1 facebook.com localhost")
            '        FSErrorLogFile.WriteLine("127.0.0.1 258.258.258.258 (Sample IP - So Invalid)")

            '        FSErrorLogFile.WriteLine("")

            '        FSErrorLogFile.WriteLine("127.0.0.1 www.Youtube.com localhost")
            '        FSErrorLogFile.WriteLine("")
            '        FSErrorLogFile.WriteLine("127.0.0.1 Youtube.com localhost")
            '        FSErrorLogFile.WriteLine("127.0.0.1 258.258.258.258 (Sample IP - So Invalid)")

            '        FSErrorLogFile.WriteLine("")

            '        FSErrorLogFile.WriteLine("127.0.0.1 www.divyabhaskar.com localhost")
            '        FSErrorLogFile.WriteLine("")
            '        FSErrorLogFile.WriteLine("127.0.0.1 divyabhaskar.com localhost")
            '        FSErrorLogFile.WriteLine("127.0.0.1 258.258.258.258 (Sample IP - So Invalid)")

            '        '127.0.0.1 www.facebook.com localhost

            '        '127.0.0.1 facebook.com localhost
            '        '127.0.0.1 258.258.258.258 (Sample IP - So Invalid)

            '        FSErrorLogFile.Close()
            '    End If
            'Catch ex As Exception
            '    MsgBox("Not OK")
            'End Try
            GVar_Master_Expiry = clsGlobal.Expire_Date
            'If Not File.Exists(Application.StartupPath & "\SqlServerConnection.txt") Then
            '    Dim sqlconn As New IO.StreamWriter(Application.StartupPath & "\SqlServerConnection.txt")
            '    sqlconn.WriteLine("202.71.8.227,1403|finideas|finideas|123456")
            '    sqlconn.WriteLine("60.254.95.18,1403|finideas|finideas|123456")
            '    sqlconn.WriteLine("103.228.79.44,1403|finideas|finideas|123456")
            '    sqlconn.Close()
            'End If

            ''========================================================================================


            If Not System.IO.File.Exists(Application.StartupPath & "\Licence.lic") Then
                'Call clsGlobal.LoadInitializeData()

                SetRegServer()


                'Call Rounddata() 'Commented By Viral

                'lbl:
                '                Try
                '                    Dim client As New Net.Sockets.TcpClient(RegServerIP.Split(",")(0), RegServerIP.Split(",")(1))
                '                Catch ex As Exception

                '                    If MsgBox("Internet is Down.       " & vbCrLf & "Do you want to retry?", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                '                        GoTo lbl
                '                    Else
                '                        Application.Exit()
                '                        End
                '                    End If
                '                End Try

                '                If Not ChkSQLConn() Then
                '                    MsgBox("Connection Error.")
                '                    End
                '                End If


                IMothrBoardSrNo = Trim(getmbserial()) & ""
                If IMothrBoardSrNo = " " Then
                    Exit Sub
                End If
                'HDDSrNoStr = LoadDiskInfo()
                IHDDSrNoStr = GetDriveSerialNumber() & ""
                'Dim Dfinfo As String = GetDriveSerialNumber("C")
                IProcessorSrNoStr = CpuId() & "" '//GetProcessorSeialNumber(False)
                '//IProcessorSrNoStr = SystemSerialNumber()
                'DTUserMasterde = ObjLoginData.Select_User_Master(False)
              
                ''========================================================================================
                If IO.File.Exists(Application.StartupPath & "\LoginInfo.txt") Then
                    'Exit Sub
                    ChkRemLogin.Checked = True

                    Dim FR As New IO.StreamReader(Application.StartupPath & "\LoginInfo.txt")
                    Dim Str As String = ""

                    Str = FR.ReadLine()
                    txtUserName.Text = Str.Substring("LoginId ::".Length)
                    Str = FR.ReadLine()
                    txtPassword.Text = Str.Substring("Password ::".Length)
                    FR.Close()
                Else
                    If NetMode <> "UDP" Then
                        'ChkRemLogin.Checked = False'payal
                        ''///code By payalpatel
                        Try
                            Dim client As New Net.Sockets.TcpClient(RegServerIP.Split(",")(0), RegServerIP.Split(",")(1))
                            client.Client.Disconnect(True)
                            client.Close()
                            client = Nothing

                        Catch ex As Exception
                            If Not ex.Message.Contains("No connection could be made because the target machine actively refused it") Then
                                startFlg = True
                                Dim is3Key As Boolean = System.IO.File.Exists(Application.StartupPath & "\3Key.txt")
                                Call StartInd(is3Key)
                            End If
                        End Try
                    End If
                End If
                ''========================================================================================

                If Check_SingleInstance("VolHedge") = False Then
                    End
                End If
                'DTUserMasterde = ObjLoginData.Select_User_Master1(False, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(IMothrBoardSrNo), clsUEnDe.FEnc(IHDDSrNoStr), clsUEnDe.FEnc(IProcessorSrNoStr))
                Timer1.Enabled = True
                lblAMCText.Text = ""
                lblAMCText.Refresh()
                ' DTUserMasterde = ObjLoginData.Select_User_Master1(False, "", clsUEnDe.FEnc(IMothrBoardSrNo), clsUEnDe.FEnc(IHDDSrNoStr), clsUEnDe.FEnc(IProcessorSrNoStr))
                ' lblSupport.Visible = True
                PictureBox3.Visible = True

                If AppLicMode = "NETLIC" Then

                    'If NetMode <> "UDP" Then
                    'End If
                  
lbl:
                    Try
                        Dim client As New Net.Sockets.TcpClient(RegServerIP.Split(",")(0), RegServerIP.Split(",")(1))
                        client.Client.Disconnect(True)
                        client.Close()
                        client = Nothing
                    Catch ex As Exception
                        Dim sdate() As String
                        Dim unm, pwd As String
                        Dim exp As Date
                        'SaveSetting("GOT", "Load", "ser", ObjLoginData._Userid)
                        'SaveSetting("GOT", "Load", "PD", ObjLoginData._pwd)
                        'SaveSetting("GOT", "Load", "ERY", ObjLoginData._ExDate)

                        ''SaveSetting("VolHedge", "OnLoad", "UserName", ObjLoginData.Userid.ToString)
                        Try


                            unm = clsUEnDe.FDec(GetSetting("GOT", "Load", "ser", ""))
                            pwd = clsUEnDe.FDec(GetSetting("GOT", "Load", "PD", ""))


                            sdate = clsUEnDe.FDec(GetSetting("GOT", "Load", "ERY", "")).ToString.Split("/")
                        Catch
                            If Not ex.Message.Contains("No connection could be made because the target machine actively refused it") Then
                                If MsgBox("Internet is Down.       " & vbCrLf & "Do you want to retry?", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                                    GoTo lbl
                                Else
                                    Application.Exit()
                                    End
                                End If
                            End If

                        End Try
                        If sdate.Length <= 1 Then
                            'ReDim sdate() As String
                            'sdate = sExpire_Date.ToString.Split("-")
                            clsGlobal.Expire_Date = CDate(clsUEnDe.FDec(GetSetting("GOT", "Load", "ERY", "")))
                        Else
                            clsGlobal.Expire_Date = DateSerial(sdate(2), sdate(1), sdate(0))
                        End If

                        exp = clsGlobal.Expire_Date

                        If unm = txtUserName.Text And pwd = txtPassword.Text And exp > Now.Date Then
                            FLG_REG_SERVER_CONN = True

                        Else
                            If Not ex.Message.Contains("No connection could be made because the target machine actively refused it") Then
                                If MsgBox("Internet is Down.       " & vbCrLf & "Do you want to retry?", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                                    GoTo lbl
                                Else
                                    Application.Exit()
                                    End
                                End If
                            End If

                        End If


                        If NetMode <> "NET" Then
                            Dim trd As New trading



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



                    End Try

                    'If Not ChkSQLConn() Then
                    '    MsgBox("Connection Error.")
                    '    End
                    'End If
                    '  End If
                    If FLG_REG_SERVER_CONN = False Then


                        If DTUserMasterde.Rows.Count = 0 Then
                            btnregister.Enabled = True
                            Dim dtsupport As DataTable = ObjLoginData.Select_Supportteam_info(1)
                            Dim EmailId As String
                            Dim Cellno As String
                            If dtsupport.Rows.Count > 0 Then
                                lblSupport.Visible = True
                                PictureBox3.Visible = True
                                For Each dr As DataRow In dtsupport.Rows
                                    EmailId = dr("MailId")
                                    Name = dr("Name")
                                    Cellno = dr("CellNo")
                                Next
                                Dim str As String
                                lblSupport.Text = "For Support Call" + vbNewLine + Name + ":" + Cellno
                            End If
                        Else
                            lblSupport.Visible = True
                            PictureBox3.Visible = True
                            btnregister.Enabled = False
                            Dim DTUserMasterde As New DataTable
                            DTUserMasterde = ObjLoginData.Select_User_Master1(True, clsUEnDe.FEnc(txtUserId.Text), clsUEnDe.FEnc(IMothrBoardSrNo), clsUEnDe.FEnc(IHDDSrNoStr), clsUEnDe.FEnc(IProcessorSrNoStr))
                            Dim EmailId As String = ""
                            Dim dtsupport As DataTable
                            Dim Name As String = ""
                            Dim Cellno As String = ""
                            Dim supportID As Int32 = 0
                            For Each dr As DataRow In DTUserMasterde.Rows
                                supportID = dr("SupportId").ToString
                            Next
                            If supportID <> 0 Then
                                dtsupport = ObjLoginData.Select_Supportteam_info(supportID)
                            End If
                            For Each dr As DataRow In dtsupport.Rows
                                EmailId = dr("MailId")
                                Name = dr("Name")
                                Cellno = dr("CellNo")
                            Next
                            lblSupport.Text = "For Support Call" + vbNewLine + Name + ":" + Cellno
                        End If
                        If ChkRemLogin.Checked = True Then
                            'Call btnLogIn_Click(sender, e)
                        End If
                        ' Me.TopMost = True 'comment by payalpatel
                    Else
                        lblSupport.Visible = False
                        PictureBox3.Visible = False
                    End If
                End If
            Else
                'Dim starttik As Long = System.Environment.TickCount
                'Write_TimeLog("SetRegServer Start..", starttik)
                'If NetMode <> "UDP" Then
                '    SetRegServer()
                'End If

                'Dim Endtik As Long = System.Environment.TickCount
                ' Write_TimeLog("SetRegServer End..(" & Endtik - starttik & ")", Endtik)

                Timer1.Enabled = True

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function GetDriveSerialNumber() As String
        'Dim DriveSerial As Integer
        ''Create a FileSystemObject object
        'Dim fso As Object = CreateObject("Scripting.FileSystemObject")
        'Dim Drv As Object = fso.GetDrive(fso.GetDriveName(Application.StartupPath))
        'With Drv
        '    If .IsReady Then
        '        DriveSerial = .SerialNumber
        '    Else    '"Drive Not Ready!"
        '        DriveSerial = -1
        '    End If
        'End With
        'Return DriveSerial.ToString("X2")
        'Dim hd1 As New ManagementObjectSearcher("SELECT * FROM Win32_Processor")
        'For Each dvs As ManagementObject In hd1.Get()
        '    Dim serial As String = dvs("ProcessorSerialno").ToString()

        'Next
        Try


            Dim hd As New ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia")
            For Each dvs As ManagementObject In hd.Get()
                Dim serial As String = dvs("SerialNumber").ToString()

                Return serial.Trim
            Next
        Catch ex As Exception
            Return ""
        End Try
    End Function




    Private Function CpuId() As String
        'Dim computer As String
        'Dim wmi As Object
        'Dim processors As Object
        'Dim cpu As Object
        'Dim cpu_ids As String

        'computer = "."
        'wmi = GetObject("winmgmts:" & _
        '    "{impersonationLevel=impersonate}!\\" & _
        '    computer & "\root\cimv2")
        'processors = wmi.ExecQuery("Select * from " & _
        '    "Win32_Processor")

        'For Each cpu In processors
        '    cpu_ids = cpu_ids & ", " & cpu.ProcessorId
        'Next cpu
        'If Len(cpu_ids) > 0 Then cpu_ids = Mid$(cpu_ids, 3)

        'CpuId = cpu_ids

        Try
            Dim cpuInfo As String
            Dim managClass As New ManagementClass("win32_processor")
            Dim managCollec As ManagementObjectCollection = managClass.GetInstances()

            For Each managObj As ManagementObject In managCollec
                CpuId = managObj.Properties("processorID").Value.ToString()
                Exit For
            Next
        Catch ex As Exception
            Try
                Write_Log1("CpuId Error Uniqueid")
                CpuId = GetProcessorSeialNumber(False) & ""
            Catch ex1 As Exception
                Write_Log1("Error in uniq id cpu")
                CpuId = "789456000"
            End Try

        End Try


    End Function
    Private Sub StartInd(ByVal is3key As Boolean)

        'Dim Thr_Filltoken As New Thread(AddressOf fill_token)
        'Thr_Filltoken.Start()
        Dim ttik As Int64 = DateTime.Now.Ticks

        Write_TimeLog1("StartInd Start..")
      

        'Write_Log("PictureBox3_Click")
        'Dim IsDynamicKey As Boolean = False
        AppLicMode = "INDLIC"
        Timer1.Enabled = False
        'gVarInstanceID = "V-" & FunG_GetMACAddress()
        G_GetMACAddress = FunG_GetMACAddress()
        gVarInstanceID = "B-" & G_GetMACAddress
        'gVarInstanceID = "C-" & G_GetMACAddress 'change by payal patel
        'Write_Log("gVarInstanceID=" & gVarInstanceID)
        'obj = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MMM/dd/yyyy")
        'Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MM/dd/yyyy")
        '==========================================keval chakalasiya(15-2-2010)

        REM This block Set Master Expiry and Version title to global variable 
        Call clsGlobal.SetExpDate(DateSerial(2020, 12, 31))
        GVar_Master_Expiry = clsGlobal.Expire_Date
        GVar_Version_Title = MDI.Text.Trim
        REM End

        Try
            REM Check Expiry date againest System date
            If Today >= CDate(clsGlobal.Expire_Date) Then
                MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
                Call clsGlobal.Sub_Get_Version_TextFile()
                Application.Exit()
                End
                Exit Sub
            End If
            REM End


            gvarInstanceCode = ""

            Dim MothrBoardSrNoOld As String = getmbserial() & ""
            Dim MothrBoardSrNoOld1 As String = Microsoft.VisualBasic.Left(Trim(MothrBoardSrNoOld), 20) & ""
            Dim MothrBoardSrNo As String = Microsoft.VisualBasic.Left(Trim(MothrBoardSrNoOld), 20) & ""

            If MothrBoardSrNo.Length <= 3 Then
                MothrBoardSrNo = MothrBoardSrNo & "01" & G_GetMACAddress
            Else
                If MothrBoardSrNo.Contains("To be filled by O.E.") Then
                    MothrBoardSrNo = "01" & G_GetMACAddress
                End If
            End If

            Dim HDDSrNoStrOld As String = GetDriveSerialNumber() & "" '//LoadDiskInfo()
            Dim HDDSrNoStrOld1 As String = LoadDiskInfo() '//HDDSrNoStrOld & "" 'Viral _API


            If HDDSrNoStrOld1.Length <= 3 Then
                HDDSrNoStrOld1 = HDDSrNoStrOld1 & "02" & FunG_GetMACAddress()
            End If

            Dim HDDSrNoStr As String = HDDSrNoStrOld & ""

            If HDDSrNoStr.Length <= 3 Then
                HDDSrNoStr = HDDSrNoStr & "02" & G_GetMACAddress
            End If

            Dim ProcessorSrNoStr3key As String = CpuId() & ""
            Dim ProcessorSrNoStrOld As String = GetProcessorSeialNumber(False) & "" 'Viral _API
            Dim ProcessorSrNoStrOld1 As String = ProcessorSrNoStrOld & ""
            Dim ProcessorSrNoStr As String = ProcessorSrNoStrOld & ""

            If ProcessorSrNoStr.Length <= 3 Then
                ProcessorSrNoStr = ProcessorSrNoStr & "03" & G_GetMACAddress
            End If

            If (is3key) Then
                Write_Log1("3Key.txtMB:" & MothrBoardSrNo) 'MothrBoardSrNoOld1
                Write_Log1("3Key.txtHD:" & HDDSrNoStrOld1)
                Write_Log1("3Key.txtPR:" & ProcessorSrNoStrOld1)
                If (MothrBoardSrNo.Trim = "" Or UCase(MothrBoardSrNo.Trim) = UCase("BASE BOARD SERIAL NUMBER")) And HDDSrNoStrOld1.Trim = "" Then
                    MsgBox("Code cannot be generated. 1", MsgBoxStyle.Critical)
                    End
                End If
                If (MothrBoardSrNo.Trim = "" Or UCase(MothrBoardSrNo.Trim) = UCase("BASE BOARD SERIAL NUMBER")) And ProcessorSrNoStrOld1.Trim = "" Then ' ProcessorSrNoStr3key
                    MsgBox("Code cannot be generated. 1", MsgBoxStyle.Critical)
                    End
                End If
                If HDDSrNoStrOld1.Trim = "" And ProcessorSrNoStrOld1.Trim = "" Then 'ProcessorSrNoStr3key
                    MsgBox("Code cannot be generated. 1", MsgBoxStyle.Critical)
                    End
                End If


                Dim VarMDUserCode As String = ""
                Dim VarHDUserCode As String = ""
                Dim VarPSUserCode As String = ""


                VarMDUserCode = Client_encry.Client_encry.get_client_key("", MothrBoardSrNo, "", "S10XQ6BJ4G8S26JSN")
                gvarInstanceCode = VarMDUserCode & "|" & My.Computer.Name & "|" & My.User.Name
                VarHDUserCode = Client_encry.Client_encry.get_client_key(HDDSrNoStrOld1, "", "", "S10XQ6BJ4G8S26JSN")
                gvarInstanceCode = VarHDUserCode & "|" & My.Computer.Name & "|" & My.User.Name
                VarPSUserCode = Client_encry.Client_encry.get_client_key("", "", ProcessorSrNoStrOld1, "S10XQ6BJ4G8S26JSN") 'ProcessorSrNoStr3key
                gvarInstanceCode = VarPSUserCode & "|" & My.Computer.Name & "|" & My.User.Name


                Dim MDVK As Boolean = Varify_Act_Key(VarMDUserCode)
                Dim HDVK As Boolean = Varify_Act_Key(VarHDUserCode)
                Dim PSVK As Boolean = Varify_Act_Key(VarPSUserCode)

                If Not ((MDVK = True And HDVK = True) Or (MDVK = True And PSVK = True) Or (HDVK = True And PSVK = True)) Then
                    MsgBox("Client Not Active...." & vbCrLf & "Send Key.txt from " & Application.StartupPath & " to The Vendor", MsgBoxStyle.Exclamation)
lbl3KeyFile:
                    Call clsGlobal.Sub_Get_Version_TextFile()
                    Dim myHost1 As String = System.Net.Dns.GetHostName
                    Dim myIPs1 As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(myHost1)
                    Dim myIp1 As String = myIPs1.AddressList(0).ToString
                    Dim FSKeyFile As System.IO.StreamWriter
                    FSKeyFile = New IO.StreamWriter(Application.StartupPath & "\Key.txt", False)
                    FSKeyFile.WriteLine("Computer Name:" & My.Computer.Name)
                    FSKeyFile.WriteLine("IP:" & myIp1)

                    'FSKeyFile.WriteLine("Client Key:" & VarUserCode)
                    FSKeyFile.WriteLine("Client Key1:" & VarMDUserCode)
                    FSKeyFile.WriteLine("Client Key2:" & VarHDUserCode)
                    FSKeyFile.WriteLine("Client Key3:" & VarPSUserCode)
                    FSKeyFile.WriteLine("Version:" & GVar_Version_Title)
                    FSKeyFile.Close()
                    fullcontrol()
                    Dim VarExplorerArg As String = "/select, " & Application.StartupPath & "\Key.txt"
                    Shell("Explorer.exe " + VarExplorerArg, AppWinStyle.NormalFocus, True, -1)
                    Application.Exit()
                    End

                End If

                REM This block check whether any version match with licence file
                If VarAppVersion.Trim <> "" Then
                    If VarAppVersion.Trim <> GVar_Version_Title.Trim Then
                        MsgBox("Application Version not valid!!", MsgBoxStyle.Exclamation)
                        GoTo lbl3KeyFile
                    End If
                End If
                REM End


            Else
                'Old
                If (MothrBoardSrNoOld.Trim = "" Or UCase(MothrBoardSrNoOld.Trim) = UCase("BASE BOARD SERIAL NUMBER")) And HDDSrNoStrOld.Trim = "" Then
                    MsgBox("Code cannot be generated. 0", MsgBoxStyle.Critical)
                    End
                End If
                If (MothrBoardSrNoOld.Trim = "" Or UCase(MothrBoardSrNoOld.Trim) = UCase("BASE BOARD SERIAL NUMBER")) And ProcessorSrNoStrOld.Trim = "" Then
                    MsgBox("Code cannot be generated. 0", MsgBoxStyle.Critical)
                    End
                End If
                If HDDSrNoStrOld.Trim = "" And ProcessorSrNoStrOld.Trim = "" Then
                    MsgBox("Code cannot be generated. 0", MsgBoxStyle.Critical)
                    End
                End If

                'Old1
                If (MothrBoardSrNoOld1.Trim = "" Or UCase(MothrBoardSrNoOld1.Trim) = UCase("BASE BOARD SERIAL NUMBER")) And HDDSrNoStrOld1.Trim = "" Then
                    MsgBox("Code cannot be generated. 1", MsgBoxStyle.Critical)
                    End
                End If
                If (MothrBoardSrNoOld1.Trim = "" Or UCase(MothrBoardSrNoOld1.Trim) = UCase("BASE BOARD SERIAL NUMBER")) And ProcessorSrNoStrOld1.Trim = "" Then
                    MsgBox("Code cannot be generated. 1", MsgBoxStyle.Critical)
                    End
                End If
                If HDDSrNoStrOld1.Trim = "" And ProcessorSrNoStrOld1.Trim = "" Then
                    MsgBox("Code cannot be generated. 1", MsgBoxStyle.Critical)
                    End
                End If


                'new
                If (MothrBoardSrNo.Trim = "" Or UCase(MothrBoardSrNo.Trim) = UCase("BASE BOARD SERIAL NUMBER")) And HDDSrNoStr.Trim = "" Then
                    MsgBox("Code cannot be generated.", MsgBoxStyle.Critical)
                    End
                End If
                If (MothrBoardSrNo.Trim = "" Or UCase(MothrBoardSrNo.Trim) = UCase("BASE BOARD SERIAL NUMBER")) And ProcessorSrNoStr.Trim = "" Then
                    MsgBox("Code cannot be generated.", MsgBoxStyle.Critical)
                    End
                End If
                If HDDSrNoStr.Trim = "" And ProcessorSrNoStr.Trim = "" Then
                    MsgBox("Code cannot be generated.", MsgBoxStyle.Critical)
                    End
                End If


                Dim VarUserCodeOld As String = ""
                Dim VarUserCodeOld1 As String = ""
                Dim VarUserCode As String = ""

                REM Generate User code using DSS.dll
                'Write_Log("VarUserCode -> Begin")

                VarUserCodeOld = Client_encry.Client_encry.get_client_key(HDDSrNoStrOld, MothrBoardSrNoOld, ProcessorSrNoStrOld, "S10XQ6BJ4G8S26JSN")
                VarUserCodeOld1 = Client_encry.Client_encry.get_client_key(HDDSrNoStrOld1, MothrBoardSrNoOld1, ProcessorSrNoStrOld1, "S10XQ6BJ4G8S26JSN")
                VarUserCode = Client_encry.Client_encry.get_client_key(HDDSrNoStr, MothrBoardSrNo, ProcessorSrNoStr, "S10XQ6BJ4G8S26JSN")

                gvarInstanceCode = VarUserCodeOld1 & "|" & My.Computer.Name & "|" & My.User.Name
                'Write_Log("VarUserCode ->" & VarUserCode)
                REM End

                REM This block check generated user code exist in Licence file. If exist then continue otherwise display Key.txt file
                'Write_Log("Varify_Act_Key -> Begin")

                'GoTo lblstart Api Testing Viral

                Dim VarUCOld As Boolean = Varify_Act_Key(VarUserCodeOld)
                Dim VarUCOld1 As Boolean = Varify_Act_Key(VarUserCodeOld1)
                Dim VarUC As Boolean = Varify_Act_Key(VarUserCode)


                If VarUCOld = False And VarUCOld1 = False And VarUC = False Then
                    'If Today.Date >= GVar_Tmp_Expiry Then

                    MsgBox("Client Not Active...." & vbCrLf & "Send Key.txt from " & Application.StartupPath & " to The Vendor", MsgBoxStyle.Exclamation)
lblKeyFile:
                    Call clsGlobal.Sub_Get_Version_TextFile()
                    Dim myHost1 As String = System.Net.Dns.GetHostName
                    Dim myIPs1 As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(myHost1)
                    Dim myIp1 As String = myIPs1.AddressList(0).ToString
                    Dim FSKeyFile As System.IO.StreamWriter
                    FSKeyFile = New IO.StreamWriter(Application.StartupPath & "\Key.txt", False)
                    FSKeyFile.WriteLine("Computer Name:" & My.Computer.Name)
                    FSKeyFile.WriteLine("IP:" & myIp1)
                    FSKeyFile.WriteLine("Client Key:" & VarUserCodeOld1)
                    FSKeyFile.WriteLine("Version:" & GVar_Version_Title)
                    FSKeyFile.Close()
                    fullcontrol()
                    Dim VarExplorerArg As String = "/select, " & Application.StartupPath & "\Key.txt"
                    Shell("Explorer.exe " + VarExplorerArg, AppWinStyle.NormalFocus, True, -1)
                    Application.Exit()
                    End
                    'Else
                    '    clsGlobal.Expire_Date = GVar_Tmp_Expiry
                    'End If
                End If
                REM End

                Dim auth As Boolean = False
                If VarUCOld = True Then
                    auth = Varify_Act_Key(VarUserCodeOld)
                ElseIf VarUCOld1 = True Then
                    auth = Varify_Act_Key(VarUserCodeOld1)
                ElseIf VarUC = True Then
                    auth = Varify_Act_Key(VarUserCode)
                End If

                'If Today.Date >= GVar_Tmp_Expiry Then
                If auth = False Then
                    MsgBox("Client Not Active...." & vbCrLf & "Send Key.txt from " & Application.StartupPath & " to The Vendor", MsgBoxStyle.Exclamation)
                    GoTo lblKeyFile
                End If
                '    Else
                '    clsGlobal.Expire_Date = GVar_Tmp_Expiry
                'End If

                REM This block check whether any version match with licence file
                If VarAppVersion.Trim <> "" Then
                    If VarAppVersion = "MI" Then
                        verVersion = "MI"
                    Else
                        If VarAppVersion.Trim <> GVar_Version_Title.Trim Then
                            MsgBox("Application Version not valid!!", MsgBoxStyle.Exclamation)
                            GoTo lblKeyFile
                        End If
                    End If

                 
                End If
                REM End

            End If






            'lblstart: apitesting Viral
            REM This block check whether version is AMC then display AMC version Text
            If VarIsAMC = True Then
                lblAMCText.Text = "AMC Active"
            Else
                lblAMCText.Text = "AMC Not Active"
            End If
            lblAMCText.Refresh()
            REM End

            If VarAppVersion <> "MI" Then

                REM To Check Single instance working
                If Check_SingleInstance("VolHedge") = False Then
                    End
                End If
                REM End
            End If
            REM Check Expiry date Check with System data
            If Today >= CDate(clsGlobal.Expire_Date) Then
                MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
                Call clsGlobal.Sub_Get_Version_TextFile()
                Application.Exit()
                Exit Sub
            End If
            REM End

        Catch ex As Exception
            MsgBox(ex.ToString)
            Application.Exit()
        End Try
        'Else
        'Application.Exit()
        'End
        'End If

        Me.Cursor = Cursors.WaitCursor
       
        Call clsGlobal.LoadInitializeData()
        If NetMode <> "UDP" Then
            CheckTelNet_Connection()
        End If



        If NetMode = "NET" Then
            MDI.Timer_Net.Interval = Timer_Net_Interval
            MDI.Timer_Net.Enabled = True
            MDI.Timer_Net.Start()
            Call MDI.Timer_Net_Tick(MDI.Timer_Net, New EventArgs)
        ElseIf NetMode = "TCP" Or NetMode = "JL" Then
            ' gVarInstanceID = "V-" & FunG_GetMACAddress()
            gVarInstanceID = "B-" & G_GetMACAddress 'change by payal patel
            'gVarInstanceID = "C-" & G_GetMACAddress 'change by payal patel

            'Write_Log("gVarInstanceID=" & gVarInstanceID)

            MDI.Timer_Sql.Interval = Timer_Sql_Interval
            MDI.Timer_Sql.Enabled = True
            MDI.Timer_Sql.Start()
        ElseIf flgAPI_K = "TCP" And NetMode = "API" Then
            ' gVarInstanceID = "V-" & FunG_GetMACAddress()
            gVarInstanceID = "B-" & G_GetMACAddress 'change by payal patel
            'gVarInstanceID = "C-" & G_GetMACAddress 'change by payal patel

            'Write_Log("gVarInstanceID=" & gVarInstanceID)
            If flgAPI_ExpCheck = True Then


                MDI.Timer_Sql.Interval = Timer_Sql_Interval
                MDI.Timer_Sql.Enabled = True
                MDI.Timer_Sql.Start()
            End If
        ElseIf flgAPI_K = "TRUEDATA" And NetMode = "API" Then
            ' gVarInstanceID = "V-" & FunG_GetMACAddress()
            gVarInstanceID = "C-" & G_GetMACAddress 'change by payal patel
            'gVarInstanceID = "C-" & G_GetMACAddress 'change by payal patel

            'Write_Log("gVarInstanceID=" & gVarInstanceID)
            If flgAPI_ExpCheck = True Then


                MDI.Timer_Sql.Interval = Timer_Sql_Interval
                MDI.Timer_Sql.Enabled = True
                MDI.Timer_Sql.Start()
            End If
        ElseIf NetMode = "API" And flgAPI Then
            InitApiThread()
        End If

        Me.Hide()
        MDI.Show()
        Me.Cursor = Cursors.Default

        Dim obj_DelSetDisableMenubar As New GDelegate_MdiStatus(AddressOf MDI.Sub_Disable_Manu_n_Tool_bar)
        obj_DelSetDisableMenubar.Invoke(Boolean.TrueString, "Connected..")

        Dim Endtik As Long = System.Environment.TickCount
        Write_TimeLog1("SplashScreenUsrLic-> End Fun-StartInd" + "|" + TimeSpan.FromTicks(DateTime.Now.Ticks - ttik).TotalSeconds.ToString())

    End Sub

    Private Function Check_EXEName(ByVal AppName As String) As Boolean
        Dim Str() As String = Application.ExecutablePath.Split("\")
        If Str.Length = 0 Then
            Str = Application.ExecutablePath.Split("/")
        End If
        If UCase(Str(Str.Length - 1)) <> UCase(AppName & ".exe") Then
            If AppName = "VolHedge" Then
                MsgBox("Invalid application name, run " & AppName & ".exe to start application", MsgBoxStyle.Information)
                Return False
            End If
        End If
        Return True
    End Function

    Private Function Check_SingleInstance(ByVal AppName As String) As Boolean

        If Check_EXEName(AppName) = False Then
            Return False
        End If
        Dim cnt As Integer = 0
        Dim proc As System.Diagnostics.Process
        For Each proc In System.Diagnostics.Process.GetProcessesByName(AppName)
            cnt += 1
        Next
        'MessageBox.Show("" + cnt.ToString())
        'If AppName = "NotisDBToEasyRMS" Then
        '    If cnt = 0 Then
        '        Process.Start("E:\NotisDB To EasyRMS\Notis File Reader\bin\Debug\NotisDBToEasyRMS.exe")
        '    End If
        'End If

        If cnt > 1 Then
            MsgBox(AppName & " Application already opened !!", MsgBoxStyle.Critical)
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    '''  Varify_Act_Key
    ''' </summary>
    ''' <param name="VarUserCode">To Pass generated user code generated by DSS dll</param>
    ''' <returns></returns>
    ''' <remarks>Find Activation Key Liecnce genrate according to passing User Code. This function read Expiry Date, Version name and AMC flag from Licence file</remarks>
    Private Function Varify_Act_Key(ByVal VarUserCode As String) As Boolean
        'Try


        REM to skip Rendom char generat
        Dim ArrSkipChar As New ArrayList
        ArrSkipChar.Add(2)
        ArrSkipChar.Add(4)
        ArrSkipChar.Add(6)
        ArrSkipChar.Add(8)
        Dim VarActualCode As String = ""
        For i As Integer = 0 To VarUserCode.Length - 1
            If ArrSkipChar.Contains(i + 1) = False Then
                VarActualCode &= VarUserCode.Chars(i)
            End If
        Next
        REM END

        Call ReadServiceFile()
        Dim VarActKey = GenerateActKey(VarUserCode).Substring(0, VarUserCode.Length)
        Dim VarTempActkey As String = ""
        For i As Integer = 0 To VarActKey.Length - 5
            If ArrSkipChar.Contains(i + 1) = False Then
                VarTempActkey &= VarActKey.Chars(i)
            End If
        Next
        If IsFound(VarActualCode) = False Then
            'Write_Log("Varify_Act_Key -> = False")
            Return False
        Else
            Dim VarStrActKey As String = GetActkey(VarActualCode).Substring(0, VarActualCode.Length)
            Dim VarLicActkey As String = ""
            For i As Integer = 0 To VarStrActKey.Length - 1
                If ArrSkipChar.Contains(i + 1) = False Then
                    VarLicActkey &= VarStrActKey.Chars(i)
                End If
            Next
            If VarTempActkey <> VarLicActkey Then
                'Write_Log("Varify_Act_Key -> = False")
                Return False

            Else
                Dim sExpire_Date = GetExpiryDate(VarActualCode)
                Dim sdate() As String
                sdate = sExpire_Date.ToString.Split("/")
                clsGlobal.Expire_Date = DateSerial(sdate(2), sdate(0), sdate(1))
                G_VarExpiryDate = IIf(clsGlobal.Expire_Date > G_VarExpiryDate, G_VarExpiryDate, clsGlobal.Expire_Date)
                G_VarExpiryDate1 = DateDiff(DateInterval.Second, CDate("1-1-1980"), G_VarExpiryDate)
                VarIsAMC = CBool(GetAMCCheck(VarActualCode).Substring(0, 1))
                VarAppVersion = GetLicenceVersion(VarActualCode)
                G_VarNoOfDealer = GetNoOfDealer(VarActualCode)
            End If
        End If
        'Write_Log("Varify_Act_Key -> End = True")
        Return True
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        '    Write_Log("Varify_Act_Key -> End = True")
        '    Return False
        'End Try

    End Function
    Private Function getHDserial() As String
        REM Read Serial No. From Mother-Board
        Dim searcher As New ManagementObjectSearcher("SELECT  * FROM Win32_LogicalDisk")
        Dim information As ManagementObjectCollection = searcher.[Get]()
        For Each obj As ManagementObject In information
            For Each data As PropertyData In obj.Properties
                'Console.WriteLine("{0} = {1}", data.Name, data.Value)
                'Return data.Value
                Return data.Value
            Next
        Next
        REM End
        searcher.Dispose()
    End Function

    Public Function GetHDDSerialNumber(ByVal drive As String) As String
        'check to see if the user provided a drive letter
        'if not default it to "C"
        If drive = "" OrElse drive Is Nothing Then
            drive = "C"
        End If
        'create our ManagementObject, passing it the drive letter to the
        'DevideID using WQL
        Dim disk As New ManagementObject("Win32_LogicalDisk.DeviceID=""" + drive + ":""")
        'bind our management object
        disk.[Get]()
        'return the serial number
        Return disk("VolumeSerialNumber").ToString()
    End Function

    Public Function GetDriveSerialNumber(ByVal DriveLetter As String) As Long

        Dim fso As Object, Drv As Object
        Dim DriveSerial As String
        'Create a FileSystemObject object
        fso = CreateObject("Scripting.FileSystemObject")

        'Assign the current drive letter if not specified
        If DriveLetter <> "" Then
            Drv = fso.GetDrive(DriveLetter)
        Else
            Drv = fso.GetDrive(fso.GetDriveName(Application.StartupPath))
        End If

        With Drv
            If .IsReady Then
                DriveSerial = Math.Abs(.SerialNumber)
            Else    '"Drive Not Ready!"
                DriveSerial = -1
            End If
        End With

        'Clean up
        Drv = Nothing
        fso = Nothing

        GetDriveSerialNumber = DriveSerial

    End Function
    Private Function SystemSerialNumber() As String
        Dim mother_boards As Object
        Dim board As Object
        Dim wmi As Object
        Dim serial_numbers As String = ""

        ' Get the Windows Management Instrumentation object.
        wmi = GetObject("WinMgmts:")

        ' Get the "base boards" (mother boards).
        mother_boards = wmi.InstancesOf("Win32_BaseBoard")
        For Each board In mother_boards
            serial_numbers = serial_numbers & ", " & _
                board.SerialNumber
        Next board
        If Len(serial_numbers) > 0 Then serial_numbers = _
            Mid$(serial_numbers, 3)

        SystemSerialNumber = serial_numbers
    End Function


    Public Shared StartUpExpire_Date As Date = clsGlobal.SetExpDate(DateSerial(2020, 12, 31)) ' CDate("2012-12-31") 'CDate("2011-04-30")

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

    ''' <summary>
    ''' PictureBox3_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Set the Version Type and Expiry date
    ''' Generate User Code using Motherboard Serial Number,Processort Serial Number and Hard Disk Number and checking that user code in Licence file of client version
    ''' Fill Global datatable from database
    ''' All Settings which is already applied after check Version type and expiry date checking
    ''' </remarks>
    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GBlogin.Click

        '   lblSupport.Visible = False
        '     PictureBox3.Visible = False
        clsGlobal.InternetVersionFlag = True
        Timer1.Enabled = False
        G_GetMACAddress = FunG_GetMACAddress()
        'gVarInstanceID = "V-" & FunG_GetMACAddress()
        gVarInstanceID = "B-" & G_GetMACAddress 'change by payal patel
        'gVarInstanceID = "C-" & G_GetMACAddress 'change by payal patel
        'Write_Log("gVarInstanceID=" & gVarInstanceID)

        'obj = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MMM/dd/yyyy")
        'Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MM/dd/yyyy")
        '==========================================keval chakalasiya(15-2-2010)

        REM This block Set Master Expiry and Version title to global variable 
        'GVar_Master_Expiry = clsGlobal.Expire_Date
        GVar_Version_Title = MDI.Text.Trim
        REM End
     
        Try
            REM Check Expiry date againest System date
            If Today >= CDate(clsGlobal.Expire_Date) Then
                MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
                Call clsGlobal.Sub_Get_Version_TextFile()
                Application.Exit()
                End
                Exit Sub
            End If
            REM End
            Dim MothrBoardSrNo As String = Trim(getmbserial())
            If MothrBoardSrNo.Length <= 3 Then
                MothrBoardSrNo = MothrBoardSrNo & "01" & G_GetMACAddress
            End If

            'MsgBox("MothrBoardSrNo : " & MothrBoardSrNo)
            Dim HDDSrNoStr As String = GetDriveSerialNumber() '//LoadDiskInfo()
            If HDDSrNoStr.Length <= 3 Then
                HDDSrNoStr = HDDSrNoStr & "02" & G_GetMACAddress
            End If

            'MsgBox("HDDSrNoStr : " & HDDSrNoStr)
            Dim ProcessorSrNoStr As String = GetProcessorSeialNumber(False)
            If ProcessorSrNoStr.Length <= 3 Then
                ProcessorSrNoStr = ProcessorSrNoStr & "03" & G_GetMACAddress
            End If


            'If (MothrBoardSrNo.Trim = "" Or UCase(MothrBoardSrNo.Trim) = UCase("BASE BOARD SERIAL NUMBER")) And HDDSrNoStr.Trim = "" Then
            '    MsgBox("Code cannot be generated.", MsgBoxStyle.Critical)
            '    End
            'End If
            'If (MothrBoardSrNo.Trim = "" Or UCase(MothrBoardSrNo.Trim) = UCase("BASE BOARD SERIAL NUMBER")) And ProcessorSrNoStr.Trim = "" Then
            '    MsgBox("Code cannot be generated.", MsgBoxStyle.Critical)
            '    End
            'End If
            'If HDDSrNoStr.Trim = "" And ProcessorSrNoStr.Trim = "" Then
            '    MsgBox("Code cannot be generated.", MsgBoxStyle.Critical)
            '    End
            'End If
            Dim VarUserCode As String = ""
            gvarInstanceCode = ""
            REM Generate User code using DSS.dll
            'VarUserCode = Client_encry.Client_encry.get_client_key(HDDSrNoStr, MothrBoardSrNo, ProcessorSrNoStr, "S10XQ6BJ4G8S26JSN")
            REM End
            gvarInstanceCode = gVarInstanceID & "|" & My.Computer.Name & "|" & My.User.Name



        Catch ex As Exception
            MsgBox(ex.Message)
            Application.Exit()
        End Try

        Me.Cursor = Cursors.WaitCursor

        Call clsGlobal.LoadInitializeData()

        Me.Cursor = Cursors.Default

    End Sub



    '''' <summary>
    ''''  Varify_Act_Key
    '''' </summary>
    '''' <param name="VarUserCode">To Pass generated user code generated by DSS dll</param>
    '''' <returns></returns>
    '''' <remarks>Find Activation Key Liecnce genrate according to passing User Code. This function read Expiry Date, Version name and AMC flag from Licence file</remarks>
    'Private Function Varify_Act_Key(ByVal VarUserCode As String) As Boolean
    '    REM to skip Rendom char generat
    '    Dim ArrSkipChar As New ArrayList
    '    ArrSkipChar.Add(2)
    '    ArrSkipChar.Add(4)
    '    ArrSkipChar.Add(6)
    '    ArrSkipChar.Add(8)
    '    Dim VarActualCode As String = ""
    '    For i As Integer = 0 To VarUserCode.Length - 1
    '        If ArrSkipChar.Contains(i + 1) = False Then
    '            VarActualCode &= VarUserCode.Chars(i)
    '        End If
    '    Next
    '    REM END

    '    Call ReadServiceFile()
    '    Dim VarActKey = GenerateActKey(VarUserCode).Substring(0, VarUserCode.Length)
    '    Dim VarTempActkey As String = ""
    '    For i As Integer = 0 To VarActKey.Length - 5
    '        If ArrSkipChar.Contains(i + 1) = False Then
    '            VarTempActkey &= VarActKey.Chars(i)
    '        End If
    '    Next
    '    If IsFound(VarActualCode) = False Then
    '        Return False
    '    Else
    '        Dim VarStrActKey As String = GetActkey(VarActualCode).Substring(0, VarActualCode.Length)
    '        Dim VarLicActkey As String = ""
    '        For i As Integer = 0 To VarStrActKey.Length - 1
    '            If ArrSkipChar.Contains(i + 1) = False Then
    '                VarLicActkey &= VarStrActKey.Chars(i)
    '            End If
    '        Next
    '        If VarTempActkey <> VarLicActkey Then
    '            Return False
    '        Else
    '            Dim sExpire_Date = GetExpiryDate(VarActualCode)
    '            Dim sdate() As String
    '            sdate = sExpire_Date.ToString.Split("/")
    '            clsGlobal.Expire_Date = DateSerial(sdate(2), sdate(0), sdate(1))
    '            G_VarExpiryDate = IIf(clsGlobal.Expire_Date > G_VarExpiryDate, G_VarExpiryDate, clsGlobal.Expire_Date)
    '            G_VarExpiryDate1 = DateDiff(DateInterval.Second, CDate("1-1-1980"), G_VarExpiryDate)
    '            VarIsAMC = CBool(GetAMCCheck(VarActualCode).Substring(0, 1))
    '            VarAppVersion = GetLicenceVersion(VarActualCode)
    '            G_VarNoOfDealer = GetNoOfDealer(VarActualCode)
    '        End If
    '    End If
    '    Return True
    'End Function
    Private Function Varify_User1(ByVal isCheckActive As Boolean) As Boolean
        Dim sExpire_Date As String
        Dim isAllowed As String = "False"

        'If Not DTUserMasterde.Select("F21='" & MothrBoardSrNo & "' And F22='" & HDDSrNoStr & "' And F23='" & ProcessorSrNoStr & "' And F2='" & txtUserName.Text & "' And F3='" & txtPassword.Text & "' And F6='" & gstr_ProductName & "'").Length > 0 Then
        '    MsgBox("Invalid User!!!" & vbCrLf & "Please Register Your UserID. " & vbCrLf & "")
        '    WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
        '    txtUserName.Focus()
        '    Return False
        '    Exit Function
        'End If

        For Each drow As DataRow In DTUserMasterde.Select("F21='" & IMothrBoardSrNo & "' And F22='" & IHDDSrNoStr & "' And F23='" & IProcessorSrNoStr & "' And F2='" & txtUserName.Text & "' And F3='" & txtPassword.Text & "' And F6 = '" & gstr_ProductName & "'")
            sExpire_Date = drow("F9")
            isAllowed = drow("F7")



            ObjLoginData.Userid = drow("F2")
            ObjLoginData.pwd = drow("F3")

            ObjLoginData.Username = drow("F4")
            ObjLoginData.Address = drow("F13")
            ObjLoginData.Mobile = drow("F14")
            ObjLoginData.Email = drow("F15")
            ObjLoginData.DOB = drow("F16")

            ObjLoginData.Firm = drow("F5")
            ObjLoginData.FirmAddress = drow("F18")
            ObjLoginData.FirmContactNo = drow("F19")
            ObjLoginData.Reference = drow("F20")

            ObjLoginData.Product = drow("F6")
            ObjLoginData.Allowed = drow("F7")
            ObjLoginData.Limited = drow("F8")
            ObjLoginData.ExDate = drow("F9")
            ObjLoginData.Status = drow("F10")

            ObjLoginData.M = drow("F21")
            ObjLoginData.H = drow("F22")
            ObjLoginData.P = drow("F23")

            ObjLoginData.City = drow("F24")
            ObjLoginData.BillNo = drow("F26")
            ObjLoginData.Lic = drow("F27")

            ObjLoginData.UsernameServer = drow("F18")
            ObjLoginData.DatabaseName = drow("F17")
            ObjLoginData.Password = drow("F19")

            ObjLoginData.TCP = Math.Abs(CInt(CBool(drow("TCP"))))

            flgAPI = CBool(drow("API").ToString)
            If IsDate(drow("API_Exp").ToString) Then
                flgAPI_Exp = CDate(drow("API_Exp").ToString)
                flgAPI_Expint = DateDiff(DateInterval.Second, CDate("1-1-1980"), flgAPI_Exp)
            Else
                flgAPI_Exp = Now.Date
            End If

            If Today >= CDate(flgAPI_Exp) Or flgAPI = False Then
                flgAPI_ExpCheck = False
            Else
                flgAPI_ExpCheck = True
            End If


            flgAPI_K = drow("API_K").ToString

            Dim sdate() As String
            sdate = sExpire_Date.ToString.Split("/")

            Call Rounddata()
            clsGlobal.PreOTP = ObjLoginData.PreOTP
            clsGlobal.FlagTCP = ObjLoginData.TCP
            If sdate.Length <= 1 Then
                'ReDim sdate() As String
                'sdate = sExpire_Date.ToString.Split("-")
                clsGlobal.Expire_Date = CDate(sExpire_Date)
            Else
                clsGlobal.Expire_Date = DateSerial(sdate(2), sdate(1), sdate(0))
            End If

            G_VarExpiryDate = IIf(clsGlobal.Expire_Date > G_VarExpiryDate, G_VarExpiryDate, clsGlobal.Expire_Date)
            G_VarExpiryDate1 = DateDiff(DateInterval.Second, CDate("1-1-1980"), G_VarExpiryDate)
            'VarIsAMC = CBool(GetAMCCheck(VarActualCode).Substring(0, 1))
            'VarAppVersion = GetLicenceVersion(VarActualCode)
            'G_VarNoOfDealer = GetNoOfDealer(VarActualCode)
        Next





        If isCheckActive Then
            If Convert.ToBoolean(isAllowed) = False Then
                'MsgBox("User is not Active!!!")
                Return False
                Exit Function
            Else
                Return True
                Exit Function
            End If
        End If

        If Convert.ToBoolean(isAllowed) = False Then
            MsgBox("User Deactivated!!!" & vbCrLf & "Contact Your Vendor.")
            Return False
            Exit Function
        End If

        'If GdtSettings.Select("SettingName='APPLICATION_CLOSING_FLAG'")(0).Item("SettingKey") = "TRUE" Then
        'If AppLicMode = "NETLIC" Then
        '    ObjLoginData.LogOutUser()
        'End If
        'If ObjLoginData.Status = "in" Then
        '    MsgBox("User Allready login From another PC!!!" & vbCrLf & "")
        '    Return False
        '    Exit Function
        'End If
        '   End If




        REM Check Expiry date againest Tradedate
        If Today >= CDate(G_VarExpiryDate) Then
            MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
            ObjLoginData.Update()
            Call clsGlobal.Sub_Get_Version_TextFile()
            'Application.Exit()
            Return False
            Exit Function
        End If


        Return True
    End Function
    'Private Function Varify_User(ByVal isCheckActive As Boolean) As Boolean
    '    Dim sExpire_Date As String
    '    Dim isAllowed As String = "False"

    '    If Not DTUserMasterde.Select("F21='" & MothrBoardSrNo & "' And F22='" & HDDSrNoStr & "' And F23='" & ProcessorSrNoStr & "' And F2='" & txtUserName.Text & "' And F3='" & txtPassword.Text & "' And F6='" & gstr_ProductName & "'").Length > 0 Then
    '        MsgBox("Invalid User!!!" & vbCrLf & "Please Register Your UserID. " & vbCrLf & "")
    '        WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
    '        txtUserName.Focus()
    '        Return False
    '        Exit Function
    '    End If

    '    For Each drow As DataRow In DTUserMasterde.Select("F21='" & MothrBoardSrNo & "' And F22='" & HDDSrNoStr & "' And F23='" & ProcessorSrNoStr & "' And F2='" & txtUserName.Text & "' And F3='" & txtPassword.Text & "' And F6 = '" & gstr_ProductName & "'")
    '        sExpire_Date = drow("F9")
    '        isAllowed = drow("F7")



    '        ObjLoginData.Userid = drow("F2")
    '        ObjLoginData.pwd = drow("F3")

    '        ObjLoginData.Username = drow("F4")
    '        ObjLoginData.Address = drow("F13")
    '        ObjLoginData.Mobile = drow("F14")
    '        ObjLoginData.Email = drow("F15")
    '        ObjLoginData.DOB = drow("F16")

    '        ObjLoginData.Firm = drow("F5")
    '        ObjLoginData.FirmAddress = drow("F18")
    '        ObjLoginData.FirmContactNo = drow("F19")
    '        ObjLoginData.Reference = drow("F20")

    '        ObjLoginData.Product = drow("F6")
    '        ObjLoginData.Allowed = drow("F7")
    '        ObjLoginData.Limited = drow("F8")
    '        ObjLoginData.ExDate = drow("F9")
    '        ObjLoginData.Status = drow("F10")

    '        ObjLoginData.M = drow("F21")
    '        ObjLoginData.H = drow("F22")
    '        ObjLoginData.P = drow("F23")

    '        ObjLoginData.City = drow("F24")
    '        ObjLoginData.BillNo = drow("F26")
    '        ObjLoginData.Lic = drow("F27")

    '        ObjLoginData.TCP = Math.Abs(CInt(CBool(drow("TCP"))))



    '        Dim sdate() As String
    '        sdate = sExpire_Date.ToString.Split("/")



    '        clsGlobal.FlagTCP = ObjLoginData.TCP
    '        If sdate.Length <= 1 Then
    '            'ReDim sdate() As String
    '            'sdate = sExpire_Date.ToString.Split("-")
    '            clsGlobal.Expire_Date = CDate(sExpire_Date)
    '        Else
    '            clsGlobal.Expire_Date = DateSerial(sdate(2), sdate(1), sdate(0))
    '        End If

    '        G_VarExpiryDate = IIf(clsGlobal.Expire_Date > G_VarExpiryDate, G_VarExpiryDate, clsGlobal.Expire_Date)
    '        G_VarExpiryDate1 = DateDiff(DateInterval.Second, CDate("1-1-1980"), G_VarExpiryDate)
    '        'VarIsAMC = CBool(GetAMCCheck(VarActualCode).Substring(0, 1))
    '        'VarAppVersion = GetLicenceVersion(VarActualCode)
    '        'G_VarNoOfDealer = GetNoOfDealer(VarActualCode)
    '    Next
    '    If isCheckActive Then
    '        If Convert.ToBoolean(isAllowed) = False Then
    '            'MsgBox("User is not Active!!!")
    '            Return False
    '            Exit Function
    '        Else
    '            Return True
    '            Exit Function
    '        End If
    '    End If

    '    If Convert.ToBoolean(isAllowed) = False Then
    '        MsgBox("User Deactivated!!!" & vbCrLf & "Contact Your Vendor.")
    '        Return False
    '        Exit Function
    '    End If

    '    If GdtSettings.Select("SettingName='APPLICATION_CLOSING_FLAG'")(0).Item("SettingKey") = "TRUE" Then
    '        'If AppLicMode = "NETLIC" Then
    '        '    ObjLoginData.LogOutUser()
    '        'End If
    '        'If ObjLoginData.Status = "in" Then
    '        '    MsgBox("User Allready login From another PC!!!" & vbCrLf & "")
    '        '    Return False
    '        '    Exit Function
    '        'End If
    '    End If
    '    REM Check Expiry date againest Tradedate
    '    If Today >= CDate(G_VarExpiryDate) Then
    '        MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
    '        ObjLoginData.Update()
    '        Call clsGlobal.Sub_Get_Version_TextFile()
    '        'Application.Exit()
    '        Return False
    '        Exit Function
    '    End If
    '    Return True
    'End Function
    ''' <summary>
    '''  Timer1_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>call picture box click event and Timer Stop </remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'Dim Tik As Long = System.Environment.TickCount
        'FSPrcTikLogFile.WriteLine("Change Tab: " & (Math.Floor(System.Environment.TickCount - lngTik) / 1000))
        'lngTik = System.Environment.TickCount
        'Dim Dt As Date = Now
        Dim EmailId As String
        Dim Name As String
        Dim Cellno As String


        Dim is3Key As Boolean = System.IO.File.Exists(Application.StartupPath & "\3Key.txt")
        'Write_Log_Startup("is3Key=" & is3Key)
        If System.IO.File.Exists(Application.StartupPath & "\Licence.lic") Then
            'GBLOGIN1.Visible = False
            'btnregister.Visible = False
            'Label4.Visible = False
            Call StartInd(is3Key)
        Else
            ChkRemLogin.Visible = False
            GBLOGIN1.Visible = True
            btnregister.Visible = True
            Label4.Visible = True
            If startFlg = False Then
                If Not System.IO.File.Exists(Application.StartupPath & "\LoginInfo.txt") Then
                    startFlg = True
                    'Dim strarg As String = Application.StartupPath + "\\VolHedge.exe"
                    'System.Diagnostics.Process.Start(Application.StartupPath & "FullControl.exe", "'" & Application.StartupPath & "', '" & strarg & "'")
                    If MsgBox("Do You Want To Register on InterNet?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        'If MsgBox("Do You Want To Start With OMI?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        '    GBoxUserMaster.Visible = False
                        '    GBLOGIN1.Visible = True
                        'Else
                        Call StartInd(is3Key)
                        'End If

                    Else
                        Button1_Click(sender, e)

                    End If
                End If
            End If
        End If
        Timer1.Enabled = False

        'MsgBox(DateDiff(DateInterval.Second, Dt, Now))
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        AppLicMode = "NETLIC"
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub lblAMCText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblAMCText.Click

    End Sub
    Public Function generateOTP() As String
        Dim alphabets As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim small_alphabets As String = "abcdefghijklmnopqrstuvwxyz"
        Dim numbers As String = "1234567890"

        Dim characters As String = numbers
        Dim alphanumeric As Boolean = False
        'If rbType.SelectedItem.Value = "1" Then
        If alphanumeric = True Then
            characters += Convert.ToString(alphabets & small_alphabets) & numbers
        End If

        'End If
        Dim length As Integer = 5 'Integer.Parse(ddlLength.SelectedItem.Value)
        Dim otp As String = String.Empty
        For i As Integer = 0 To length - 1
            Dim character As String = String.Empty
            Do
                Dim index As Integer = New Random().Next(0, characters.Length)
                character = characters.ToCharArray()(index).ToString()
            Loop While otp.IndexOf(character) <> -1
            otp += character
        Next
        'lblOTP.Text = otp
        Return otp
    End Function
    'Protected Sub GenerateOTP(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim alphabets As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    '    Dim small_alphabets As String = "abcdefghijklmnopqrstuvwxyz"
    '    Dim numbers As String = "1234567890"

    '    Dim characters As String = numbers
    '    'If rbType.SelectedItem.Value = "1" Then
    '    characters += Convert.ToString(alphabets & small_alphabets) & numbers
    '    'End If
    '    Dim length As Integer = 5 'Integer.Parse(ddlLength.SelectedItem.Value)
    '    Dim otp As String = String.Empty
    '    For i As Integer = 0 To length - 1
    '        Dim character As String = String.Empty
    '        Do
    '            Dim index As Integer = New Random().Next(0, characters.Length)
    '            character = characters.ToCharArray()(index).ToString()
    '        Loop While otp.IndexOf(character) <> -1
    '        otp += character
    '    Next
    '    lblOTP.Text = otp
    'End Sub
    Public Function GetTime(ByVal Time As Integer) As String
        Dim Hrs As Integer  'number of hours   '
        Dim Min As Integer  'number of Minutes '
        Dim Sec As Integer  'number of Sec     '

        'Seconds'
        Sec = Time Mod 60

        'Minutes'
        Min = ((Time - Sec) / 60) Mod 60

        'Hours'
        Hrs = ((Time - (Sec + (Min * 60))) / 3600) Mod 60

        Return Format(Hrs, "00") & ":" & Format(Min, "00") & ":" & Format(Sec, "00")
    End Function
    '    Private Sub btnLogIn_Click11(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '        Me.Cursor = Cursors.Default
    '        GBOTP.Visible = True




    '        Call PictureBox3_Click(sender, e)

    'lbl:
    '        Try
    '            Dim client As New Net.Sockets.TcpClient(RegServerIP.Split(",")(0), RegServerIP.Split(",")(1))
    '        Catch ex As Exception
    '            If MsgBox("Internet is Down.       " & vbCrLf & "Do you want to retry?", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
    '                GoTo lbl
    '            Else
    '                Application.Exit()
    '                End
    '            End If
    '        End Try


    '        If ObjLoginData.GetTodayDate().ToString("dd/MMM/yyyy") <> Today.Date.ToString("dd/MMM/yyyy") Then
    '            MsgBox("Please Set Your System Date.", MsgBoxStyle.OkOnly, "VolHedge")
    '            'Application.Exit()
    '            'End
    '            Exit Sub
    '        End If


    '        REM Check Expiry date Check with System data
    '        If Today >= CDate(clsGlobal.Expire_Date) Then
    '            MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
    '            Call clsGlobal.Sub_Get_Version_TextFile()
    '            Application.Exit()
    '            End
    '            Exit Sub
    '        End If
    '        REM End

    '        'DTUserMasterde = ObjLoginData.Select_User_Master(False)
    '        If txtUserName.Text.Trim.Length <= 0 Then
    '            MsgBox("Invalid UserName!!!")
    '            txtUserName.Focus()
    '            Exit Sub
    '        End If
    '        If txtPassword.Text.Trim.Length <= 0 Then
    '            MsgBox("Invalid Password!!!")
    '            txtPassword.Focus()
    '            Exit Sub
    '        End If

    '        Dim strOTP As String = generateOTP()
    '        'If Not DTUserMasterde.Select("F21='" & MothrBoardSrNo & "' And F22='" & HDDSrNoStr & "' And F23='" & ProcessorSrNoStr & "' And F2='" & txtUserName.Text & "'  And F6='" & gstr_ProductName & "'").Length > 0 Then

    '        '    MsgBox("Invalid User!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
    '        '    WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
    '        '    txtUserName.Focus()
    '        '    'Return False
    '        '    Exit Sub
    '        'End If
    '        'If Not DTUserMasterde.Select("F21='" & MothrBoardSrNo & "' And F22='" & HDDSrNoStr & "' And F23='" & ProcessorSrNoStr & "' And  F3='" & txtPassword.Text & "' And F6='" & gstr_ProductName & "'").Length > 0 Then

    '        '    MsgBox("Invalid Password!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
    '        '    WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
    '        '    txtUserName.Focus()
    '        '    'Return False
    '        '    Exit Sub
    '        'End If
    '        If Not DTUserMasterde.Select("F2='" & txtUserName.Text & "'").Length > 0 Then

    '            MsgBox("Invalid User!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
    '            WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
    '            txtUserName.Focus()
    '            'Return False
    '            Exit Sub
    '        End If
    '        If Not DTUserMasterde.Select(" F3='" & txtPassword.Text & "'").Length > 0 Then

    '            MsgBox("Invalid Password!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
    '            WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
    '            txtUserName.Focus()
    '            'Return False
    '            Exit Sub
    '        End If
    '        'If Not DTUserMasterde.Select("F21='" & MothrBoardSrNo & "' And F22='" & HDDSrNoStr & "' And F23='" & ProcessorSrNoStr & "' And F2='" & txtUserName.Text & "' And F3='" & txtPassword.Text & "' And F6='" & gstr_ProductName & "'").Length > 0 Then

    '        '    MsgBox("Invalid User Or Password!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
    '        '    WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
    '        '    txtUserName.Focus()
    '        '    'Return False
    '        '    Exit Sub
    '        'End If

    '        If Varify_User1(True) = False Then
    '            If MsgBox("           User not active!!!                     " & vbCrLf & "           Do you want to activate user?       ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    '                GBoxActivate.Visible = True
    '                txtActivationCode.Focus()

    '            End If
    '            'Application.Exit()
    '            Exit Sub
    '        End If
    '        'DTUserMasterde = ObjLoginData.Select_User_Master(False)
    '        DTUserMasterde = ObjLoginData.Select_User_Master1(False, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(IMothrBoardSrNo), clsUEnDe.FEnc(IHDDSrNoStr), clsUEnDe.FEnc(IProcessorSrNoStr))
    '        REM This block check generated user code exist in Licence file. If exist then continue otherwise display Key.txt file
    '        If Varify_User1(False) = False Then
    '            'Application.Exit()
    '            'Exit Sub
    '            Call StartInd()
    '        End If
    '        REM End

    '        'REM This block check whether version is AMC then display AMC version Text
    '        'If VarIsAMC = True Then
    '        '    lblAMCText.Text = "AMC Active"
    '        'Else
    '        '    lblAMCText.Text = "AMC Not Active"
    '        'End If
    '        'lblAMCText.Refresh()
    '        'REM End


    '        ObjLoginData.SetLoginState("in")




    '        CheckTelNet_Connection()

    '        If NetMode = "NET" Then
    '            MDI.Timer_Net.Interval = Timer_Net_Interval
    '            MDI.Timer_Net.Enabled = True
    '            MDI.Timer_Net.Start()
    '            Call MDI.Timer_Net_Tick(MDI.Timer_Net, New EventArgs)
    '        ElseIf NetMode = "TCP" Then
    '            '      gVarInstanceID = "V-" & FunG_GetMACAddress()
    '            gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel
    '            fill_token()
    '            'Write_Log("gVarInstanceID=" & gVarInstanceID)
    '            MDI.Timer_Sql.Interval = Timer_Sql_Interval
    '            MDI.Timer_Sql.Enabled = True
    '            MDI.Timer_Sql.Start()
    '        End If

    '        '   MDI.Show()
    '        Me.Cursor = Cursors.Default

    '        Dim obj_DelSetDisableMenubar As New GDelegate_MdiStatus(AddressOf MDI.Sub_Disable_Manu_n_Tool_bar)
    '        obj_DelSetDisableMenubar.Invoke(Boolean.TrueString, "Connected..")

    '        If ChkRemLogin.Checked Then
    '            Dim FW As New IO.StreamWriter(Application.StartupPath & "\LoginInfo.txt")
    '            FW.WriteLine("LoginId ::" & txtUserName.Text)
    '            FW.WriteLine("Password ::" & txtPassword.Text)
    '            FW.Close()
    '        Else
    '            IO.File.Delete(Application.StartupPath & "\LoginInfo.txt")
    '        End If

    '        '  Me.Hide()
    '    End Sub
    Private Sub btnLogIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogIn.Click
     

        Me.Cursor = Cursors.WaitCursor
        '        Call PictureBox3_Click(sender, e)

lbl:
        '' ''Try
        '' ''    Dim client As New Net.Sockets.TcpClient(RegServerIP.Split(",")(0), RegServerIP.Split(",")(1))
        '' ''    client.Client.Disconnect(True)
        '' ''    client.Close()
        '' ''    client = Nothing

        '' ''Catch ex As Exception
        '' ''    If Not ex.Message.Contains("No connection could be made because the target machine actively refused it") Then
        '' ''        If MsgBox("Internet is Down.       " & vbCrLf & "Do you want to retry?", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
        '' ''            GoTo lbl
        '' ''        Else
        '' ''            Application.Exit()
        '' ''            End
        '' ''        End If
        '' ''    End If


        '' ''End Try

        Dim dt As String = DateTime.Today.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
        'dt = DateTime.Parse(DateTime.Today.ToString("dd/MMM/yyyy")).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
        If FLG_REG_SERVER_CONN = False Then


            Dim dt2 As String = ObjLoginData.GetTodayDate()
            If dt2 <> dt Then
                MsgBox("Please Set Your System Date.", MsgBoxStyle.OkOnly, "VolHedge")
                Me.Cursor = Cursors.Default
                'Application.Exit()
                'End
                Exit Sub
            End If
        End If
        'If ObjLoginData.GetTodayDate().ToString("dd/MMM/yyyy") <> Today.Date.ToString("dd/MMM/yyyy") Then
        '    MsgBox("Please Set Your System Date.", MsgBoxStyle.OkOnly, "VolHedge")
        '    Me.Cursor = Cursors.Default
        '    'Application.Exit()
        '    'End
        '    Exit Sub
        'End If


        REM Check Expiry date Check with System data
        If Today >= CDate(clsGlobal.Expire_Date) Then
            MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
            Call clsGlobal.Sub_Get_Version_TextFile()
            Me.Cursor = Cursors.Default
            Application.Exit()
            End
            Exit Sub
        End If
        REM End

        'DTUserMasterde = ObjLoginData.Select_User_Master(False)
        If txtUserName.Text.Trim.Length <= 0 Then
            MsgBox("Invalid UserName!!!")
            txtUserName.Focus()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        If txtPassword.Text.Trim.Length <= 0 Then
            MsgBox("Invalid Password!!!")
            Me.Cursor = Cursors.Default
            txtPassword.Focus()
            Exit Sub
        End If

        If chkOMI.Checked = True Then
            gstr_ProductName = "OMI"
        Else
            gstr_ProductName = "VolHedge"

        End If
        'If Not DTUserMasterde.Select("F21='" & MothrBoardSrNo & "' And F22='" & HDDSrNoStr & "' And F23='" & ProcessorSrNoStr & "' And F2='" & txtUserName.Text & "'  And F6='" & gstr_ProductName & "'").Length > 0 Then

        '    MsgBox("Invalid User!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
        '    WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
        '    txtUserName.Focus()
        '    'Return False
        '    Exit Sub
        'End If
        'If Not DTUserMasterde.Select("F21='" & MothrBoardSrNo & "' And F22='" & HDDSrNoStr & "' And F23='" & ProcessorSrNoStr & "' And  F3='" & txtPassword.Text & "' And F6='" & gstr_ProductName & "'").Length > 0 Then

        '    MsgBox("Invalid Password!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
        '    WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
        '    txtUserName.Focus()
        '    'Return False
        '    Exit Sub
        'End If
        If FLG_REG_SERVER_CONN = False Then


            DTUserMasterde = ObjLoginData.Select_User_Master1(False, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(IMothrBoardSrNo), clsUEnDe.FEnc(IHDDSrNoStr), clsUEnDe.FEnc(IProcessorSrNoStr))
            If Not DTUserMasterde.Select("F2='" & txtUserName.Text & "'").Length > 0 Then

                MsgBox("Invalid User!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
                WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
                txtUserName.Focus()
                'Return False
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            If Not DTUserMasterde.Select(" F3='" & txtPassword.Text & "'").Length > 0 Then

                MsgBox("Invalid Password!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
                WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
                txtUserName.Focus()
                Me.Cursor = Cursors.Default
                'Return False
                Exit Sub
            End If
            'If Not DTUserMasterde.Select("F21='" & MothrBoardSrNo & "' And F22='" & HDDSrNoStr & "' And F23='" & ProcessorSrNoStr & "' And F2='" & txtUserName.Text & "' And F3='" & txtPassword.Text & "' And F6='" & gstr_ProductName & "'").Length > 0 Then

            '    MsgBox("Invalid User Or Password!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
            '    WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
            '    txtUserName.Focus()
            '    'Return False
            '    Exit Sub
            'End If


            If Varify_User1(True) = False Then
                'If MsgBox("           User not active!!!                     " & vbCrLf & "           Do you want to activate user?       ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                MsgBox("Please Contact Vendor, Version Expired.")
                Me.Cursor = Cursors.Default
                '    GBoxActivate.Visible = True
                '   txtActivationCode.Focus()
                Dim is3Key As Boolean = System.IO.File.Exists(Application.StartupPath & "\3Key.txt")
                Call StartInd(is3Key)

                'Application.Exit()
                Exit Sub
            End If
            'DTUserMasterde = ObjLoginData.Select_User_Master(False)

            ' DTUserMasterde = ObjLoginData.Select_User_Master1(False, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(IMothrBoardSrNo), clsUEnDe.FEnc(IHDDSrNoStr), clsUEnDe.FEnc(IProcessorSrNoStr))

            REM This block check generated user code exist in Licence file. If exist then continue otherwise display Key.txt file
            If Varify_User1(False) = False Then
                'Application.Exit()
                'Exit Sub
                Dim is3Key As Boolean = System.IO.File.Exists(Application.StartupPath & "\3Key.txt")
                Call StartInd(is3Key)
            End If

            SaveSetting("GOT", "Load", "ser", ObjLoginData._Userid)
            SaveSetting("GOT", "Load", "PD", ObjLoginData._pwd)
            '      SaveSetting("GOT", "Load", "ERY", ObjLoginData._ExDate)
            SaveSetting("GOT", "Load", "ERY", clsUEnDe.FEnc(G_VarExpiryDate))


            SaveSetting("GOT", "Load", "REGSERIP", RegServerIP)
            SaveSetting("GOT", "Load", "REGSERUSER", RegServerUserid)
            SaveSetting("GOT", "Load", "REGSERPWD", RegServerpwd)
            SaveSetting("GOT", "Load", "REGSERDB", RegServerdbnm)


            'RegServerIP = servernm
            'RegServerdbnm = db
            'RegServerUserid = unm
            'RegServerpwd = pwd

            REM End
            Dim trd As New trading
            If INSTANCEname <> "PRIMARY" Then
                trd.Update_setting("PRIMARY", "INSTANCE")
                GdtSettings = trd.Settings
                    INSTANCEname = GdtSettings.Compute("max(SettingKey)", "SettingName='INSTANCE'").ToString.Trim
                
            End If


            'REM This block check whether version is AMC then display AMC version Text
            'If VarIsAMC = True Then
            '    lblAMCText.Text = "AMC Active"
            'Else
            '    lblAMCText.Text = "AMC Not Active"
            'End If
            'lblAMCText.Refresh()
            'REM End


            '' ''Dim DTUserMasterdedate As New DataTable
            '' ''DTUserMasterdedate = ObjLoginData.Select_User_Master1(True, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(IMothrBoardSrNo), clsUEnDe.FEnc(IHDDSrNoStr), clsUEnDe.FEnc(IProcessorSrNoStr))


            '' ''Dim otp As String = DTUserMasterdedate.Rows(0).Item("PreOTP").ToString().Trim()

            '' ''Dim otpdate As String = ""
            '' ''If otp.Contains("_") Then
            '' ''    Dim str() As String = otp.ToString.Split("_")

            '' ''    otp = str(0)
            '' ''    otpdate = str(1)
            '' ''    If otpdate = Now.Date.ToString("ddMMMyyyy") Then
            '' ''        Me.Cursor = Cursors.WaitCursor
            '' ''        Me.Show()
            '' ''        'Me.Hide()
            '' ''        showmdi(sender, e)
            '' ''        Me.Cursor = Cursors.Default
            '' ''        Exit Sub
            '' ''    End If
            '' ''End If


        End If
        If clsGlobal.RagisterFlag = False Then
            clsGlobal.RagisterFlag = True
            Dim strOTP As String = generateOTP()
            ObjLoginData.PreOTP = strOTP
            'ObjLoginData.Update_User_Master()
            '/Changes by payal patel
            'SendOtpmail()
            'SendOTPmessage()
            'MessageBox.Show("OTP Send On your Register Mobileno And EmailAddress Successfully..")
            'GBOTP.Location = New Point(345, 70)
            'GBLOGIN1.Visible = False
            'txtotp.Focus()
            'GBOTP.Visible = True
            '-----------------------------------------
            'ObjLoginData.SetLoginState("in")
            txtotp.Text = strOTP
            btnotp_Click(e, EventArgs.Empty)
        End If
      
        Me.Cursor = Cursors.Default
        Me.Hide()
        'CheckTelNet_Connection()

        'If NetMode = "NET" Then
        '    MDI.Timer_Net.Interval = Timer_Net_Interval
        '    MDI.Timer_Net.Enabled = True
        '    MDI.Timer_Net.Start()
        '    Call MDI.Timer_Net_Tick(MDI.Timer_Net, New EventArgs)
        'ElseIf NetMode = "TCP" Then
        '    '      gVarInstanceID = "V-" & FunG_GetMACAddress()
        '    gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel
        '    fill_token()
        '    'Write_Log("gVarInstanceID=" & gVarInstanceID)
        '    MDI.Timer_Sql.Interval = Timer_Sql_Interval
        '    MDI.Timer_Sql.Enabled = True
        '    MDI.Timer_Sql.Start()
        'End If

        'MDI.Show()
        'Me.Cursor = Cursors.Default

        'Dim obj_DelSetDisableMenubar As New GDelegate_MdiStatus(AddressOf MDI.Sub_Disable_Manu_n_Tool_bar)
        'obj_DelSetDisableMenubar.Invoke(Boolean.TrueString, "Connected..")

        'If ChkRemLogin.Checked Then
        '    Dim FW As New IO.StreamWriter(Application.StartupPath & "\LoginInfo.txt")
        '    FW.WriteLine("LoginId ::" & txtUserName.Text)
        '    FW.WriteLine("Password ::" & txtPassword.Text)
        '    FW.Close()
        'Else
        '    IO.File.Delete(Application.StartupPath & "\LoginInfo.txt")
        'End If

        'Me.Hide()
    End Sub
    Function SendOTPmessagereg()
        Try
            Dim strmessage As String = "Dear Customer. FinIdeas VolHedge OTP is " + "" + ObjLoginData.PreOTP
            'Dim strmessage As String = ObjLoginData.PreOTP + " " + "Is Your Login OTP."
            Dim str As String = "http://173.45.76.226:81/send.aspx?username=flnideas&pass=flnideas123&route=premium&senderid=FINIDE&numbers=" + TxtMobNo.Text + "&message=" + strmessage + ""

            Dim myWebRequest As WebRequest = WebRequest.Create(str)
            'Dim myWebResponse As WebResponse = myWebRequest.GetResponse()
            Dim wbResp As HttpWebResponse = DirectCast(myWebRequest.GetResponse(), HttpWebResponse)

        Catch ex As Exception
            MessageBox.Show("MobileNo Is Invalid..")
        End Try
    End Function

    Function SendOTPmessage()
        Try
            Dim strmessage As String = "Dear Customer. FinIdeas VolHedge OTP is " + "" + ObjLoginData.PreOTP

            'Dim strmessage As String = ObjLoginData.PreOTP + " " + "Is Your Login OTP."
            Dim str As String = "http://173.45.76.226:81/send.aspx?username=flnideas&pass=flnideas123&route=premium&senderid=FINIDE&numbers=" + ObjLoginData.Mobile + "&message=" + strmessage + ""
            Dim myWebRequest As WebRequest = WebRequest.Create(str)
            'Dim myWebResponse As WebResponse = myWebRequest.GetResponse()
            Dim wbResp As HttpWebResponse = DirectCast(myWebRequest.GetResponse(), HttpWebResponse)
        Catch ex As Exception
        End Try
    End Function
    Function SendOtpmailreg() As Boolean
        Dim Str As String

        Str = "<!DOCTYPE HTML>" & vbCrLf
        Str = Str & "<html lang='en-US'>" & vbCrLf
        Str = Str & "<head>" & vbCrLf
        Str = Str & "<meta charset='UTF-8'>" & vbCrLf
        Str = Str & "<title></title>" & vbCrLf
        Str = Str & "</head>" & vbCrLf
        Str = Str & "<body>" & vbCrLf
        Str = Str & "<p>Dear Customer, </p>" & vbCrLf
        Str = Str & "<p><B>FinIdeas VolHedge OTP is  :</B>" & ObjLoginData.PreOTP & " </p>" & vbCrLf
        Str = Str & "</body>" & vbCrLf
        Str = Str & "</html>" & vbCrLf
        Dim FLAG As Boolean
        Dim email As String = txtEmail.Text
        FLAG = send_emailOTP("Software@finideas.com", "Finideas123", email, "VolHedge OTP verification", Str)
        If FLAG = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Function SendOtpmail()
        Dim Str As String
        Str = "<!DOCTYPE HTML>" & vbCrLf
        Str = Str & "<html lang='en-US'>" & vbCrLf
        Str = Str & "<head>" & vbCrLf
        Str = Str & "<meta charset='UTF-8'>" & vbCrLf
        Str = Str & "<title></title>" & vbCrLf
        Str = Str & "</head>" & vbCrLf
        Str = Str & "<body>" & vbCrLf
        Str = Str & "<p>Dear Customer, </p>" & vbCrLf
        Str = Str & "<p><B>FinIdeas VolHedge OTP is  :</B>" & ObjLoginData.PreOTP & " </p>" & vbCrLf
        Str = Str & "</body>" & vbCrLf
        Str = Str & "</html>" & vbCrLf
        Dim email As String = ObjLoginData.Email
        send_emailOTP("Software@finideas.com", "Finideas123", email, "VolHedge OTP verification", Str)
    End Function
    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GBoxUserMaster.Enter

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.Cursor = Cursors.WaitCursor
        If txtUserId.Text.Trim.Length <= 0 Or txtUserId.Text.Trim.ToUpper = "ADMIN" Then
            Me.Cursor = Cursors.Default
            MsgBox("Invalid UserId!")
            txtUserId.Focus()
            Exit Sub
        End If

        If TxtPwd.Text.Trim.Length <= 0 Then
            Me.Cursor = Cursors.Default
            MsgBox("Invalid Password!")
            TxtPwd.Focus()
            Exit Sub
        End If

        If TxtPwd.Text <> TxtPwdConfirm.Text Then
            Me.Cursor = Cursors.Default
            MsgBox("Password Not Match With Confirm Password!")
            Exit Sub
        End If

        'If TxtName.Text.Trim.Length <= 0 Then
        '    Me.Cursor = Cursors.Default
        '    MsgBox("Invalid User Name!")
        '    TxtName.Focus()
        '    Exit Sub
        'End If

        'If TxtAddress.Text.Trim.Length <= 0 Then
        '    Me.Cursor = Cursors.Default
        '    MsgBox("Invalid User Address!")
        '    TxtAddress.Focus()
        '    Exit Sub
        'End If

        If txtEmail.Text.Trim.Length <= 0 Then
            Me.Cursor = Cursors.Default
            MsgBox("Invalid Email !")
            txtEmail.Focus()
            Exit Sub
        End If

        'If TxtCity.Text.Trim.Length <= 0 Then
        '    Me.Cursor = Cursors.Default
        '    MsgBox("Invalid User City!")
        '    TxtCity.Focus()
        '    Exit Sub
        'End If

        If TxtMobNo.Text.Trim.Length <= 0 Then
            Me.Cursor = Cursors.Default
            MsgBox("Invalid User Mobile No.!")
            TxtMobNo.Focus()
            Exit Sub
        End If

        'If TxtFirm.Text.Trim.Length <= 0 Then
        '    Me.Cursor = Cursors.Default
        '    MsgBox("Invalid Organisation!")
        '    TxtFirm.Focus()
        '    Exit Sub
        'End If

        'If MothrBoardSrNo = " " Or HDDSrNoStr = " " Or ProcessorSrNoStr = " " Or gstr_ProductName = " " Then
        '    MessageBox.Show("Code")
        '    Exit Sub
        'End If

        'TUserMasterde = Select_User_Master_all()
        If CheckValidation() = False Then
            Exit Sub
        End If
        loadregisterdata(sender, e)
        'If GB1.Enabled = True Then
        '    If clsGlobal.RagisterFlag = False Then
        '        clsGlobal.RagisterFlag = True
        '        Dim strOTP As String = generateOTP()
        '        ObjLoginData.PreOTP = strOTP
        '        'Dim flag As Boolean
        '        'flag = SendOtpmailreg()

        '        'If flag = False Then
        '        '    If MsgBox("Email Id Not Confirm, Do You Want to continue??", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
        '        '        txtEmail.Focus()
        '        '        clsGlobal.RagisterFlag = False
        '        '        Exit Sub
        '        '    Else

        '        '    End If

        '        'End If

        '        Dim Thr_SendOtpmailreg = New Thread(AddressOf SendOtpmailreg)
        '        Thr_SendOtpmailreg.Name = "Thr_SendOTPmailreg"
        '        Thr_SendOtpmailreg.Start()


        '        Dim Thr_SendOTPmessagereg = New Thread(AddressOf SendOTPmessagereg)
        '        Thr_SendOTPmessagereg.Name = "Thr_SendOTPmessagereg"
        '        Thr_SendOTPmessagereg.Start()



        '        btnresend.Enabled = True
        '        GB1.Enabled = False
        '        GBOTP1.Visible = True
        '        'MessageBox.Show("OTP Send on Your MobileNo And EmailId Successfully..")
        '        MessageBox.Show("OTP Send on Your MobileNo And EmailId Successfully..")
        '        GBOTP1.Visible = True
        '        Me.Cursor = Cursors.Default
        '        Exit Sub
        '    End If
        'End If

        'If txtonetimepwd.Text.Trim.Length <= 0 Then
        '    Me.Cursor = Cursors.Default
        '    MsgBox("Invalid OTP!")
        '    txtonetimepwd.Focus()
        '    Exit Sub
        'End If
        'If clsGlobal.RagisterOTP = False Then
        '    Me.Cursor = Cursors.Default
        '    MsgBox("Please Verify Your Register Mobile No:")
        '    Exit Sub
        'End If
        'If TxtPwd.Text.Trim.Length <= 0 Or TxtName.Text.Trim.Length <= 0 Or TxtCity.Text.Trim.Length <= 0 Or TxtAddress.Text.Trim.Length <= 0 Or TxtMobNo.Text.Trim.Length <= 0 Or TxtFirm.Text.Trim.Length <= 0 Then
        '    MsgBox("please fill all mandatory(*) fields")
        'End If

    End Sub
    Private Sub SendAckMail()
        Dim Str As String

        Str = "<!DOCTYPE HTML>" & vbCrLf
        Str = Str & "<html lang='en-US'>" & vbCrLf
        Str = Str & "<head>" & vbCrLf
        Str = Str & "<meta charset='UTF-8'>" & vbCrLf
        Str = Str & "<title></title>" & vbCrLf
        Str = Str & "</head>" & vbCrLf
        Str = Str & "<body>" & vbCrLf
        Str = Str & "<p>Dear Sir, </p>" & vbCrLf

        Str = Str & "<p>Greetings from FinIdeas !!!</p>" & vbCrLf

        Str = Str & "<p>We welcome you to the world of FinIdeas Softwares.'</p>" & vbCrLf

        Str = Str & "<p><a href='https://www.youtube.com/playlist?list=PLC2D9EZOgO5KppSD-p5efVJSJ1kVl-huB'><b><u>Click Here</a></b></u> for How to Use VolHedge Software</p>" & vbCrLf

        Str = Str & "<p><a href='https://www.youtube.com/watch?t=28&v=DFeTP2WIPeY'><b><u>Click Here </a></b></u>for VolHedge Software Features</p>" & vbCrLf

        Str = Str & "<p><a href='https://www.youtube.com/playlist?list=PLC2D9EZOgO5KqaW1c17Jd9vn0BJNs8tBf'><b><u>Click Here</a></b></u> for VolHedge Software More Features</p>" & vbCrLf

        Str = Str & "<p>Thanks for showing your keen interest in VolHedge Software.</p>" & vbCrLf

        Str = Str & "<p>Please feel free to contact our Support Team.</p>" & vbCrLf
        Str = Str & "<p><B>Your User ID :</B>" & txtUserId.Text & " </p>" & vbCrLf
        Str = Str & "<p><B>Pasword :</B>" & TxtPwd.Text & " </p>" & vbCrLf

        Str = Str & "</body>" & vbCrLf
        Str = Str & "</html>" & vbCrLf

        Dim NextTime As Date = Now
        NextTime = NextTime.AddDays(7)
        Dim email As String = txtEmail.Text
        send_email("Software@finideas.com", "Finideas123", email, "VolHedge Registration Confirmation", Str)
        Str = ""
        Str = "<!DOCTYPE HTML>" & vbCrLf
        Str = Str & "<html lang='en-US'>" & vbCrLf
        Str = Str & "<head>" & vbCrLf
        Str = Str & "<meta charset='UTF-8'>" & vbCrLf
        Str = Str & "<title></title>" & vbCrLf
        Str = Str & "</head>" & vbCrLf
        Str = Str & "<body>" & vbCrLf
        Str = Str & "</p>===Login Client Detail====</p>" & vbCrLf

        Str = Str & "<p><B>UserId :</B>" & txtUserId.Text & " </p>" & vbCrLf
        Str = Str & "<p><B>Pasword :</B>" & TxtPwd.Text & " </p>" & vbCrLf
        Str = Str & "<p><B>Name:</B>" & TxtName.Text & " </p>" & vbCrLf
        Str = Str & "<p><B>Address:</B>" & TxtAddress.Text & " </p>" & vbCrLf
        Str = Str & "<p><B>City:</B>" & TxtCity.Text & " </p>" & vbCrLf
        Str = Str & "<p><B>Mobile No :</B>" & TxtMobNo.Text & " </p>" & vbCrLf
        Str = Str & "<p><B>Email :</B>" & txtEmail.Text & " </p>" & vbCrLf
        Str = Str & "<p><B>Birthdate :</B>" & dtpDOBDate.Text & " </p>" & vbCrLf
        Str = Str & "<p><B>Firm :</B>" & TxtFirm.Text & " </p>" & vbCrLf
        Str = Str & "<p><B>Firm Address :</B>" & TxtFirmAddress.Text & " </p>" & vbCrLf
        Str = Str & "<p><B>Firm Contact NO :</B>" & TxtFirmContactNo.Text & " </p>" & vbCrLf
        Str = Str & "<p><B>Referance :</B>" & TxtReference.Text & " </p>" & vbCrLf

        Str = Str & "<p><B>Expiry Date :</B>" & Format(NextTime, "dd-MMM-yyyy") & " </p>" & vbCrLf

        Str = Str & "</body>" & vbCrLf
        Str = Str & "</html>" & vbCrLf
        send_email("Software@finideas.com", "Finideas123", "Software@finideas.com", "VolHedge Registration Confirmation", Str)

    End Sub
    Public Sub loadregisterdata(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try


            Me.Cursor = Cursors.WaitCursor
            SetData()
            ObjLoginData.Insert_User_Master()
            WriteLog("Save/Edit User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")

            txtUserName.Text = txtUserId.Text
            txtPassword.Text = TxtPwd.Text

            btnDemoActivation_Click(sender, e)

            Dim Thr_SendAckMail = New Thread(AddressOf SendAckMail)
            Thr_SendAckMail.Name = "Thr_SendAckMail"
            Thr_SendAckMail.Start()


            'Application.Restart()
            'If ChkRemLogin.Checked Then

            If IO.File.Exists(Application.StartupPath & "\LoginInfo.txt") Then
                IO.File.Delete(Application.StartupPath & "\LoginInfo.txt")
            End If

            Dim FW As New IO.StreamWriter(Application.StartupPath & "\LoginInfo.txt")
            FW.WriteLine("LoginId ::" & txtUserId.Text)
            FW.WriteLine("Password ::" & TxtPwd.Text)
            FW.Close()
            fullcontrol()

            '  Else
            ' IO.File.Delete(Application.StartupPath & "\LoginInfo.txt")
            '  End If

            If IO.File.Exists(Application.StartupPath & "\LoginInfo.txt") Then
                'Exit Sub
                ChkRemLogin.Checked = True

                Dim FR As New IO.StreamReader(Application.StartupPath & "\LoginInfo.txt")
                Dim Str1 As String = ""

                Str1 = FR.ReadLine()
                txtUserName.Text = Str1.Substring("LoginId ::".Length)
                Str1 = FR.ReadLine()
                txtPassword.Text = Str1.Substring("Password ::".Length)
                FR.Close()
            Else
                'ChkRemLogin.Checked = False'payal
            End If




            Dim FWmail As New IO.StreamWriter(Application.StartupPath & "\MailInfo.txt")
            FWmail.WriteLine("MailDate ::" & Now.Date)
            FWmail.WriteLine("MailFlag ::" & "False")
            FWmail.WriteLine("ClientMail ::" & txtEmail.Text)

            FWmail.Close()
            fullcontrol()

            ObjLoginData.Update_User_Master()

            'Dim DTUserMasterde As New DataTable
            'DTUserMasterde = ObjLoginData.Select_User_Master1(True, clsUEnDe.FEnc(txtUserId.Text), clsUEnDe.FEnc(IMothrBoardSrNo), clsUEnDe.FEnc(IHDDSrNoStr), clsUEnDe.FEnc(IProcessorSrNoStr))
            'Dim EmailId As String = ""
            'Dim dtsupport As DataTable
            'Dim Name As String = ""
            'Dim Cellno As String = ""
            'Dim supportID As Int32 = 0
            'For Each dr As DataRow In DTUserMasterde.Rows
            '    supportID = dr("SupportId").ToString
            'Next
            'If supportID <> 0 Then
            '    dtsupport = ObjLoginData.Select_Supportteam_info(supportID)
            'End If
            'For Each dr As DataRow In dtsupport.Rows
            '    EmailId = dr("MailId")
            '    Name = dr("Name")
            '    Cellno = dr("CellNo")
            'Next
            lblSupport.Visible = True
            PictureBox3.Visible = True

            'Dim Str As String = "Dear" & "Mohsin" & vbCrLf
            'Str = "Yor are Allocated to Support Following Client:" & vbCrLf
            'Str = "Product = " & ProductName & vbCrLf
            'Str = "ClientName = " & TxtName.Text & vbCrLf
            'Str = "ClientNumber = " & TxtMobNo.Text


            ''Str = "SupprtClientNumber =" + TxtMobNo.Text + "SupprtClientName =" + TxtName.Text
            'send_email("Software@finideas.com", "Finideas123", EmailId, "SupprtInformation", Str)
            lblSupport.Text = "For Support Call" + vbNewLine + "Mohsin" + ":" + " +91 93775 73349"
            'lblSupport.Text = "SupprotTeam: " & Name & ":" & vbCrLf & Cellno
            ' MsgBox("Registered Successfully ", MsgBoxStyle.Information)
            ' MsgBox("For More Details Visit Your Register EmailID.. ", MsgBoxStyle.Information)
            Me.Cursor = Cursors.Default
            clsGlobal.RagisterFlag = False
            clsGlobal.FlagTCP = 0

            '//Set Internet Mode on Startup
            Dim trd As New trading
            Dim settingname, settingkey, uid As String
            settingname = "NETMODE"
            settingkey = "NET"
            settingname = "NETMODE"
            uid = "190"
            trd.Update_setting(settingkey, settingname, uid)

            Call Rounddata()
            Me.Hide()
            showmdi(sender, e)
            Me.Hide()
        Catch ex As Exception

        End Try
    End Sub



    Function send_emailOTP(ByVal senderemail As String, ByVal senderpassword As String, ByVal receiveremail As String, ByVal subject As String, ByVal message As String) As Boolean

        Try
            Dim mail As New MailMessage()
            Dim SmtpServer As New SmtpClient("smtp.gmail.com")

            mail.From = New MailAddress(senderemail)
            mail.[To].Add(receiveremail)
            mail.Subject = subject
            mail.IsBodyHtml = True
            mail.Body = message

            SmtpServer.Port = 587
            SmtpServer.Credentials = New System.Net.NetworkCredential(senderemail, senderpassword)
            SmtpServer.EnableSsl = True

            SmtpServer.Send(mail)
            Return True
            'MessageBox.Show(Convert.ToString("Email Sent to ") & receiveremail)
        Catch ex As Exception
            'MessageBox.Show(ex.ToString())
            'MessageBox.Show("Email Is Invalid..")
            'txtEmail.Focus()
            Return False
        End Try

    End Function

    'Private Sub SendMail(ByVal Email)
    '    Try
    '        Dim Smtp_Server As New SmtpClient
    '        Dim e_mail As New MailMessage()
    '        Smtp_Server.UseDefaultCredentials = False
    '        Smtp_Server.Credentials = New Net.NetworkCredential("", "")
    '        Smtp_Server.Port = 587
    '        Smtp_Server.EnableSsl = True
    '        Smtp_Server.Host = "smtp.gmail.com"

    '        e_mail = New MailMessage()
    '        e_mail.From = New MailAddress("payalpatel4646@gmail.com")
    '        e_mail.To.Add("payalpatel4646@gmail.com")
    '        e_mail.Subject = "Email Sending"
    '        e_mail.IsBodyHtml = False
    '        e_mail.Body = "Abc"
    '        Smtp_Server.Send(e_mail)
    '        MsgBox("Mail Sent")

    '    Catch error_t As Exception
    '        MsgBox(error_t.ToString)
    '    End Try

    'End Sub
    Private Sub SetData()
        REM Data transfer to data class
        ObjLoginData.Userid = txtUserId.Text
        ObjLoginData.pwd = TxtPwd.Text

        ObjLoginData.Username = TxtName.Text
        ObjLoginData.Address = TxtAddress.Text
        ObjLoginData.Mobile = TxtMobNo.Text
        ObjLoginData.Email = txtEmail.Text
        ObjLoginData.DOB = dtpDOBDate.Text

        ObjLoginData.Firm = TxtFirm.Text
        ObjLoginData.FirmAddress = TxtFirmAddress.Text
        ObjLoginData.FirmContactNo = TxtFirmContactNo.Text
        ObjLoginData.Reference = TxtReference.Text

        ObjLoginData.Product = gstr_ProductName
        ObjLoginData.Allowed = False 'Boolean.TrueString.ToString
        ObjLoginData.Limited = Boolean.TrueString.ToString
        ObjLoginData.ExDate = DateAdd(DateInterval.Day, 7, Now.Date).ToString("dd-MMM-yyyy")
        ObjLoginData.Status = "out"

        ObjLoginData.M = IMothrBoardSrNo
        ObjLoginData.H = IHDDSrNoStr
        ObjLoginData.P = IProcessorSrNoStr

        ObjLoginData.City = TxtCity.Text

    End Sub

    ''' <summary>
    ''' CheckValidation
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This function use to check validation</remarks>
    ''' Private Function CheckValidation() As Boolean

    REM This block use to Check Same Dealer Name Already Exist or not
    Private Function CheckValidation() As Boolean

        DTUserMasterde = ObjLoginData.Select_User_Master_all(False, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(IMothrBoardSrNo), clsUEnDe.FEnc(IHDDSrNoStr), clsUEnDe.FEnc(IProcessorSrNoStr))
        If DTUserMasterde.Select("F21='" & IMothrBoardSrNo & "' And F22='" & IHDDSrNoStr & "' And F23='" & IProcessorSrNoStr & "' And F6 ='" & gstr_ProductName & "'").Length > 0 Then 'F2='" & txtUserId.Text & "' And 
            MsgBox("Someone already use this PC.  !!!" & vbCrLf & "" & vbCrLf & "")
            WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
            txtUserName.Focus()
            Me.Cursor = Cursors.Default
            Return False
            Exit Function
        End If
        Me.Cursor = Cursors.Default
        'Private Function CheckValidation() As Boolean

        '    REM This block use to Check Same Dealer Name Already Exist or not
        '    If DTUserMasterde.Select("F21='" & MothrBoardSrNo & "' And F22='" & HDDSrNoStr & "' And F23='" & ProcessorSrNoStr & "' And F6 ='" & gstr_ProductName & "'").Length > 0 Then 'F2='" & txtUserId.Text & "' And 
        '        MsgBox("Someone already use this PC.  !!!" & vbCrLf & "" & vbCrLf & "")
        '        WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
        '        txtUserName.Focus()
        '        Return False
        '        Exit Function
        '    End If
        REM End


        'REM Check Allowed User 
        'If ChkUserAllowed.Checked Then
        '    Dim AllowCnt As Integer
        '    AllowCnt = DTUserMasterde.Compute("count(F7)", "F7=true")
        '    If GFun_CheckLicUserCount(AllowCnt) = False Then
        '        Return False
        '        Exit Function
        '    End If
        'End If
        'REM End

        Return True
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnregister.Click
        Label24.Visible = False
        chkOMI.Visible = False
        '  txtUserId.Focus()

        DTUserMasterde = ObjLoginData.Select_User_Master_all(False, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(IMothrBoardSrNo), clsUEnDe.FEnc(IHDDSrNoStr), clsUEnDe.FEnc(IProcessorSrNoStr))
        If DTUserMasterde.Select("F21='" & IMothrBoardSrNo & "' And F22='" & IHDDSrNoStr & "' And F23='" & IProcessorSrNoStr & "' And F6 ='" & gstr_ProductName & "'").Length > 0 Then 'F2='" & txtUserId.Text & "' And 
            'GBoxUserMaster.Enabled = False
            If MsgBox("you are already Registered,you want to see your profile?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                chkOMI.Visible = False
                GBoxUserMaster.Visible = True
                GBoxUserMaster.Top = 5
                GBoxUserMaster.Left = 13
                GBLOGIN1.Visible = False
                GBOTP.Visible = False
                lblpwd.Visible = True
                For Each dr As DataRow In DTUserMasterde.Rows
                    txtUserId.Text = dr("F2")
                    lblpwd.Text = dr("F3")
                    TxtName.Text = dr("F4")
                    TxtAddress.Text = dr("F13")
                    TxtCity.Text = dr("F24")
                    TxtMobNo.Text = dr("F14")
                    txtEmail.Text = dr("F15")
                    dtpDOBDate.Value = dr("F16")
                    TxtFirm.Text = dr("F17")
                    TxtFirmAddress.Text = dr("F18")
                    TxtFirmContactNo.Text = dr("F19")
                    TxtReference.Text = dr("F20")
                Next
                txtUserId.Enabled = False
                lblpwd.Enabled = False
                TxtPwd.Visible = False
                TxtPwdConfirm.Visible = False
                Label16.Visible = False
                TxtName.Enabled = False
                TxtAddress.Enabled = False
                TxtCity.Enabled = False
                TxtMobNo.Enabled = False
                txtEmail.Enabled = False
                dtpDOBDate.Enabled = False
                TxtFirm.Enabled = False
                TxtFirmAddress.Enabled = False
                TxtFirmContactNo.Enabled = False
                TxtReference.Enabled = False
                btnSave.Visible = False
                'MsgBox("Someone already use this PC.  !!!" & vbCrLf & "" & vbCrLf & "")
                'WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
                'txtUserName.Focus()
                Me.Cursor = Cursors.Default
                'Return False
                'Exit Sub
            End If
        Else

            Dim dt As String = DateTime.Today.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            'dt = DateTime.Parse(DateTime.Today.ToString("dd/MMM/yyyy")).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
            If FLG_REG_SERVER_CONN = False Then


                Dim dt2 As String = ObjLoginData.GetTodayDate()
                If dt2 <> dt Then
                    MsgBox("Please Set Your System Date.", MsgBoxStyle.OkOnly, "VolHedge")
                    Me.Cursor = Cursors.Default
                    'Application.Exit()
                    'End
                    Exit Sub
                Else

                End If
            End If

            GBoxUserMaster.Visible = True
            GBoxUserMaster.Top = 5
            GBoxUserMaster.Left = 13
            GBLOGIN1.Visible = False
            GBOTP.Visible = False
            ' lblpwd.Visible = True
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        End
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        '    End
        Label24.Visible = True
        chkOMI.Visible = True
        GBoxUserMaster.Visible = False
        ' GBoxUserMaster.Top = 5
        '   GBoxUserMaster.Left = 13
        GBLOGIN1.Visible = True
        '  GBOTP.Visible = False
    End Sub


    'Private Function ChkSQLConn() As Boolean

    '    Try
    '        Dim telnetServerIp As String = ""
    '        Dim telnetPort As Integer = 23

    '        telnetServerIp = RegServerIP.Split(",")(0)
    '        If RegServerIP.Split(",").Length <= 1 Then
    '            telnetPort = 1433
    '        Else
    '            telnetPort = RegServerIP.Split(",")(1)
    '        End If



    '        Dim client As New TcpClient(telnetServerIp, telnetPort)
    '        client.Client.Disconnect(True)
    '        client.Close()
    '        client = Nothing

    '        'MessageBox.Show("Server is reachable")
    '        'Return True
    '    Catch ex As Exception
    '        Return False

    '    End Try
    '    '====================================================
    '    Dim Result As Boolean
    '    Result = True

    '    If ConState = "SQLCON" Then
    '        Dim StrConn As String = ""
    '        'StrConn = " Data Source=" & RegServerIP & ";Network Library=DBMSSOCN;Initial Catalog=" & "finideas" & ";User ID=" & RegServerUserid & ";Password=" & "finideas#123" & ";"
    '        StrConn = " Data Source=" & RegServerIP & ";Network Library=DBMSSOCN;Initial Catalog=" & RegServerdbnm & ";User ID=" & RegServerUserid & ";Password=" & RegServerpwd & ";Application Name=" & "VH_REG_" & RegServerIP & ";"


    '        'Data Source=190.190.200.100,1433;Network Library=DBMSSOCN;Initial Catalog=myDataBase;User ID=myUsername;Password=myPassword;
    '        Dim ConTest As New System.Data.SqlClient.SqlConnection(StrConn)
    '        Try
    '            ConTest.Open()
    '            ConTest.Close()
    '            ConTest.Dispose()
    '            Return True
    '        Catch ex As Exception
    '            ConTest.Dispose()
    '            Return False
    '        End Try
    '    ElseIf ConState = "WEBCON" Then
    '        Return ObjWebCon.CheckCon()
    '    End If


    'End Function


    Private Sub btnAExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAExit.Click
        End
    End Sub

    Private Sub btnActivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivate.Click
        If txtActivationCode.Text.Trim = "" Then
            MsgBox("Invalid Activation Code.")
            Exit Sub
        End If

        If txtBillNo.Text.Trim = "" Then
            MsgBox("Invalid BillNo.")
            Exit Sub
        End If

        Dim sdate() As String
        Dim tmpdate As Date
        Dim tmpdate1 As String = CDate(ObjLoginData.ExDate).Month.ToString()
        'Dim tmpdate2 As Date = tmpdate1.ToString("dd/MMM/yyyy")
        sdate = ObjLoginData.ExDate.ToString.Split("-")


        If sdate.Length <= 1 Then
            tmpdate = CDate(ObjLoginData.ExDate)
        Else
            tmpdate = DateSerial(sdate(2), tmpdate1, sdate(0)).Date
        End If
        'clsGlobal.Expire_Date = DateSerial(sdate(2), sdate(1), sdate(0))

        If txtBillNo.Text.Trim.ToUpper() = ObjLoginData.GetBillNo(txtActivationCode.Text.Trim).ToUpper() And Now.Date <= tmpdate Then
            ObjLoginData.BillNo = txtBillNo.Text.Trim.ToUpper() 'txtActivationCode.Text.Trim
            If ObjLoginData.GetLicCnt() < Val(ObjLoginData.GetLicNO(txtActivationCode.Text.Trim)) Then
                ObjLoginData.UpdateActivation(True)
                If txtBillNo.Text = "DEMO" Then
                    MsgBox("Demo Activated Successfully.")
                Else
                    MsgBox("Activated Successfully.")
                End If
            Else
                ObjLoginData.UpdateActivation(False)
                MsgBox("Not Activated.")
                End
            End If
        Else
            ObjLoginData.UpdateActivation(False)
            MsgBox("Not Activated.")
            End
        End If
        MsgBox("Registered Successfully ", MsgBoxStyle.Information)
        MsgBox("For More Details Visit Your Register EmailID.. ", MsgBoxStyle.Information)
        'Application.Restart()
        GBoxUserMaster.Visible = False
    End Sub

    Private Sub btnDemoActivation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDemoActivation.Click
        'btnDemoActivation
        txtActivationCode.Visible = False
        txtBillNo.Visible = False

        'txtActivationCode.Text = "1301240343514"
        'txtBillNo.Text = "DEMO"

        txtActivationCode.Text = "1311272337495"
        txtBillNo.Text = "VDEMO"

        Call btnActivate_Click(sender, e)

    End Sub

    Private Sub txtBillNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBillNo.TextChanged

    End Sub

    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label21.Click
        Try
            System.Diagnostics.Process.Start("http://www.youtube.com/playlist?list=PLC2D9EZOgO5KqaW1c17Jd9vn0BJNs8tBf")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Try
            System.Diagnostics.Process.Start("http://www.youtube.com/playlist?list=PLC2D9EZOgO5KqaW1c17Jd9vn0BJNs8tBf")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Label22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label22.Click
        Try
            System.Diagnostics.Process.Start("http://www.youtube.com/playlist?list=PLC2D9EZOgO5LTdBUSGVkoV4RxaV_kgAGL")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Try
            System.Diagnostics.Process.Start("http://www.youtube.com/playlist?list=PLC2D9EZOgO5LTdBUSGVkoV4RxaV_kgAGL")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Label23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim ts As TimeSpan = duration - DateTime.Now.AddSeconds(-1)

    '    lbltime.Text = ts.Minutes.ToString("00") & ":" & ts.Seconds.ToString("00")

    '    If lbltime.Text = "00:00" Then

    '        Timer1.Stop()

    '        MsgBox("Yer Done!")

    '    End If

    'End Sub

    Private Sub lbltime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnotp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnotp.Click
        Try
            'If ObjLoginData.PreOTP = txtotp.Text Then
            '    ObjLoginData.Update_User_Master()
            Call PictureBox3_Click(sender, e)
            '' ''Dim DTUserMasterde As New DataTable
            '' ''DTUserMasterde = ObjLoginData.Select_User_Master1(True, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(IMothrBoardSrNo), clsUEnDe.FEnc(IHDDSrNoStr), clsUEnDe.FEnc(IProcessorSrNoStr))


            '' ''If DTUserMasterde.Rows.Count <= 0 Then
            '' ''    Exit Sub
            '' ''End If

            'Dim otp As String = DTUserMasterde.Rows(0).Item("PreOTP").ToString().Trim()
            'If otp.Contains("_") Then
            '    Dim str() As String = otp.Split("_")
            '    otp = str(0)
            'End If
            'If ObjLoginData.PreOTP <> otp Then
            '    MessageBox.Show("Invalid OTP..")
            '    txtotp.Focus()
            '    Return
            'End If


            'ObjLoginData.SetLoginState("in") by Viral 29-12-17
            'CheckTelNet_Connection()

            If NetMode = "NET" Then
                MDI.Timer_Net.Interval = Timer_Net_Interval
                MDI.Timer_Net.Enabled = True
                MDI.Timer_Net.Start()
                Call MDI.Timer_Net_Tick(MDI.Timer_Net, New EventArgs)
            ElseIf NetMode = "TCP" Or NetMode = "JL" Then
                '      gVarInstanceID = "V-" & FunG_GetMACAddress()
                If gVarInstanceID = "" Then
                    gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel
                    'gVarInstanceID = "C-" & FunG_GetMACAddress() 'change by payal patel
                End If
                fill_token()
                'Write_Log("gVarInstanceID=" & gVarInstanceID)
                MDI.Timer_Sql.Interval = Timer_Sql_Interval
                MDI.Timer_Sql.Enabled = True
                MDI.Timer_Sql.Start()
            ElseIf flgAPI_K = "TCP" And NetMode = "API" Then

                '      gVarInstanceID = "V-" & FunG_GetMACAddress()
                If gVarInstanceID = "" Then
                    gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel
                    'gVarInstanceID = "C-" & FunG_GetMACAddress() 'change by payal patel
                End If

                If flgAPI_ExpCheck = True Then
                    'Write_Log("gVarInstanceID=" & gVarInstanceID)
                    MDI.Timer_Sql.Interval = Timer_Sql_Interval
                    MDI.Timer_Sql.Enabled = True
                    MDI.Timer_Sql.Start()
                End If
            ElseIf flgAPI_K = "TRUEDATA" And NetMode = "API" Then

                '      gVarInstanceID = "V-" & FunG_GetMACAddress()

                gVarInstanceID = "C-" & FunG_GetMACAddress() 'change by payal patel
                'gVarInstanceID = "C-" & FunG_GetMACAddress() 'change by payal patel


                If flgAPI_ExpCheck = True Then
                    'Write_Log("gVarInstanceID=" & gVarInstanceID)
                    MDI.Timer_Sql.Interval = Timer_Sql_Interval
                    MDI.Timer_Sql.Enabled = True
                    MDI.Timer_Sql.Start()
                End If
            ElseIf NetMode = "API" And flgAPI Then
                InitApiThread()
            End If

            '     downloadcontractFromServer()

            Me.Hide()
            MDI.Show()
            Me.Cursor = Cursors.Default

            Dim obj_DelSetDisableMenubar As New GDelegate_MdiStatus(AddressOf MDI.Sub_Disable_Manu_n_Tool_bar)
            obj_DelSetDisableMenubar.Invoke(Boolean.TrueString, "Connected..")

            If ChkRemLogin.Checked Then
                Dim FW As New IO.StreamWriter(Application.StartupPath & "\LoginInfo.txt")
                FW.WriteLine("LoginId ::" & txtUserName.Text)
                FW.WriteLine("Password ::" & txtPassword.Text)
                FW.Close()
                fullcontrol()
            Else
                IO.File.Delete(Application.StartupPath & "\LoginInfo.txt")
            End If

            Me.Hide()
            'Else
            'MsgBox("Invalid OTP..")
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub showmdi(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Call PictureBox3_Click(sender, e)

            SaveSetting("GOT", "Load", "ser", ObjLoginData._Userid)
            SaveSetting("GOT", "Load", "PD", ObjLoginData._pwd)
            SaveSetting("GOT", "Load", "ERY", ObjLoginData._ExDate)


            ObjLoginData.SetLoginState("in")
            CheckTelNet_Connection()

            If NetMode = "NET" Then
                MDI.Timer_Net.Interval = Timer_Net_Interval
                MDI.Timer_Net.Enabled = True
                MDI.Timer_Net.Start()
                Call MDI.Timer_Net_Tick(MDI.Timer_Net, New EventArgs)
            ElseIf NetMode = "TCP" Or NetMode = "JL" Then
                '      gVarInstanceID = "V-" & FunG_GetMACAddress()
                gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel
                'gVarInstanceID = "C-" & FunG_GetMACAddress() 'change by payal patel
                fill_token()
                'Write_Log("gVarInstanceID=" & gVarInstanceID)
                MDI.Timer_Sql.Interval = Timer_Sql_Interval
                MDI.Timer_Sql.Enabled = True
                MDI.Timer_Sql.Start()
            ElseIf flgAPI_K = "TCP" And NetMode = "API" Then
                '      gVarInstanceID = "V-" & FunG_GetMACAddress()
                gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel
                'gVarInstanceID = "C-" & FunG_GetMACAddress() 'change by payal patel
                fill_token()
                If flgAPI_ExpCheck = True Then



                    'Write_Log("gVarInstanceID=" & gVarInstanceID)
                    MDI.Timer_Sql.Interval = Timer_Sql_Interval
                    MDI.Timer_Sql.Enabled = True
                    MDI.Timer_Sql.Start()

                End If
            ElseIf flgAPI_K = "TRUEDATA" And NetMode = "API" Then
                '      gVarInstanceID = "V-" & FunG_GetMACAddress()
                gVarInstanceID = "C-" & FunG_GetMACAddress() 'change by payal patel
                'gVarInstanceID = "C-" & FunG_GetMACAddress() 'change by payal patel
                fill_token()
                If flgAPI_ExpCheck = True Then



                    'Write_Log("gVarInstanceID=" & gVarInstanceID)
                    MDI.Timer_Sql.Interval = Timer_Sql_Interval
                    MDI.Timer_Sql.Enabled = True
                    MDI.Timer_Sql.Start()

                End If
            ElseIf NetMode = "API" Then
                InitApiThread()
            End If









            MDI.Show()
            Me.Cursor = Cursors.Default

            Dim obj_DelSetDisableMenubar As New GDelegate_MdiStatus(AddressOf MDI.Sub_Disable_Manu_n_Tool_bar)
            obj_DelSetDisableMenubar.Invoke(Boolean.TrueString, "Connected..")

            If ChkRemLogin.Checked Then
                Dim FW As New IO.StreamWriter(Application.StartupPath & "\LoginInfo.txt")
                FW.WriteLine("LoginId ::" & txtUserName.Text)
                FW.WriteLine("Password ::" & txtPassword.Text)
                FW.Close()
                fullcontrol()
            Else
                IO.File.Delete(Application.StartupPath & "\LoginInfo.txt")
            End If
            Dim objTrad As New trading
            objTrad.Uid = GdtSettings.Select("SettingName='CALMARGINSPAN'")(0).Item("Uid")
            objTrad.SettingName = "CALMARGINSPAN"
            objTrad.SettingKey = 0
            objTrad.Update_setting()
            Me.Hide()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub downloadcontractFromServer()

        Dim scripttable As DataTable = New DataTable
        scripttable = cpfmaster
        scripttable.Select("") ' This Code Because of Speed-Up as Directed By Alpeshbhai (By Viral 4-aug-11)
        'farDate = scripttable.Compute("max(expdate1)", "expdate1 < #" & fDate(Maturity_Far_month) & "#")

        Dim csymbol As String = "NIFTY"
        Dim coptype As String = "XX"
        Dim ContractDate As Date
        ContractDate = scripttable.Compute("min(expdate1)", "symbol='" & csymbol & "' And Option_Type='" & coptype & "'")
        If CDate(ContractDate) < CDate(Now.Date) Then


            '  If MsgBox("You Want To Update Latest Contract?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            'DownloadContract()
            Dim Cmd As SqlClient.SqlCommand
            Dim Dr As SqlClient.SqlDataReader
            If DAL.DA_SQL._conFo.State = ConnectionState.Closed Then DAL.DA_SQL.open_Fo_connection()
            Try
                If bool_IsTelNet = False Then CheckTelNet()
                If bool_IsTelNet = True Then


                    Dim strqry As String
                    strqry = "select * from Contract"

                    Dim dtcon As New DataTable
                    dtcon = DAL.DA_SQL.FillDatatable(strqry)

                    Dr = Cmd.ExecuteReader()
                End If
            Catch ex As Exception
                flgFoTCPBcast = False
                bool_IsTelNet = False
                'Write_Log2("Step3:ERROR:process_fo_SQL Process..")
                Exit Sub
            End Try

            '    Dim dtfo As DataTable = MDI.GetContractConnection("FO")
            '    Dim dtEQ As DataTable = MDI.GetContractConnection("EQ")
            '    Dim dtCURR As DataTable = MDI.GetContractConnection("CURR")

            '    objMast.insert(dtfo)
            '    objMast.Equity_insert(dtEQ)
            '    objMast.Insert_Currency_Contract(dtCURR)
            '  MessageBox.Show("Contract Import Successfully..")
            'End If
        End If




    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub


    Private Sub TxtMobNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtMobNo.Leave
        If TxtMobNo.Text.Length < 10 Then
            MessageBox.Show("Invalid MobileNo")
            TxtMobNo.Focus()
        End If

    End Sub

    Private Sub txtEmail_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.Leave

    End Sub

    Private Sub TxtFirm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFirm.TextChanged

    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

    End Sub

    Private Sub TxtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtName.TextChanged

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub

    Private Sub GB1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GB1.Enter

    End Sub

    Private Sub TxtFirmAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFirmAddress.TextChanged

    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label13.Click

    End Sub

    Private Sub TxtAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtAddress.TextChanged

    End Sub

    Private Sub Label11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label11.Click

    End Sub

    Private Sub TxtFirmContactNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFirmContactNo.TextChanged

    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label14.Click

    End Sub

    Private Sub TxtCity_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCity.TextChanged

    End Sub

    Private Sub Label18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label18.Click

    End Sub

    Private Sub dtpDOBDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDOBDate.ValueChanged

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        clsGlobal.RagisterFlag = False
        GB1.Enabled = True
        GBOTP1.Visible = False
    End Sub
    Private Sub btnresend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnresend.Click
        'SendOTPmessagereg()
        'SendOtpmailreg()
        Dim Thr_SendOtpmailreg = New Thread(AddressOf SendOtpmailreg)
        Thr_SendOtpmailreg.Name = "Thr_SendOTPmailreg"
        Thr_SendOtpmailreg.Start()


        Dim Thr_SendOTPmessagereg = New Thread(AddressOf SendOTPmessagereg)
        Thr_SendOTPmessagereg.Name = "Thr_SendOTPmessagereg"
        Thr_SendOTPmessagereg.Start()

        MessageBox.Show("Resend OTP Successfully on your Mobileno And EmailId")
    End Sub

    Private Sub TxtMobNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtMobNo.KeyPress

        If e.KeyChar = ControlChars.Back Then
            e.Handled = False
        ElseIf e.KeyChar < "0" Or e.KeyChar > "9" Or e.KeyChar = ChrW(8) Then
            e.Handled = True

        Else
            e.Handled = False
        End If
        'If e.KeyChar = ControlChars.Back Or e.KeyChar = "." Then

        '    e.Handled = False

        'Else

        '    e.Handled = True

        'End If
    End Sub

    Private Sub Button2_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnresend1.Click
        SendOTPmessage()
        SendOtpmail()
        MessageBox.Show("Resend OTP Successfully on your MobileNo And EmailId")
    End Sub

    Private Sub TxtMobNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtMobNo.KeyDown


    End Sub

    Private Sub btnexit1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit1.Click
        End
    End Sub

    Private Sub txtonetimepwd_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtonetimepwd.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnOtp1_Click(sender, e)
        End If
    End Sub

    Private Sub txtotp_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtotp.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnotp_Click(sender, e)
        End If
    End Sub

    Private Sub btnOtp1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOtp1.Click
        If ObjLoginData.PreOTP <> txtonetimepwd.Text Then
            MessageBox.Show("Invalid OTP..")
            txtonetimepwd.Focus()
            clsGlobal.RagisterOTP = False

            Return
        Else
            clsGlobal.RagisterOTP = True
            MessageBox.Show("Varify Successfully..")
            loadregisterdata(sender, e)
        End If
    End Sub

    Private Sub GBLOGIN1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GBLOGIN1.Enter

    End Sub
End Class
