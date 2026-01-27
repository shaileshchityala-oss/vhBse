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


    Dim MothrBoardSrNo As String = "0"
    Dim HDDSrNoStr As String = "0"
    Dim ProcessorSrNoStr As String = "0"


    'Dim DTUserMasterde As New DataTable
    Dim DTUserMasterde As New DataTable

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
            ChkRemLogin.Checked = False
        End If

        If Not System.IO.File.Exists(Application.StartupPath & "\Licence.lic") Then



lbl:
            Try
                Dim client As New Net.Sockets.TcpClient(RegServerIP.Split(",")(0), RegServerIP.Split(",")(1))
            Catch ex As Exception
                If MsgBox("Internet is Down.       " & vbCrLf & "Do you want to retry?", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                    GoTo lbl
                Else
                    Application.Exit()
                    End
                End If
            End Try

            If Not ChkSQLConn() Then
                MsgBox("Connection Error.")
                End
            End If


            MothrBoardSrNo = Trim(getmbserial())
            If MothrBoardSrNo = " " Then
                Exit Sub
            End If
            'HDDSrNoStr = LoadDiskInfo()
            HDDSrNoStr = GetDriveSerialNumber()
            'Dim Dfinfo As String = GetDriveSerialNumber("C")
            ProcessorSrNoStr = GetProcessorSeialNumber(False)
            ProcessorSrNoStr = SystemSerialNumber()
            'DTUserMasterde = ObjLoginData.Select_User_Master(False)

            DTUserMasterde = ObjLoginData.Select_User_Master1(False, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(MothrBoardSrNo), clsUEnDe.FEnc(HDDSrNoStr), clsUEnDe.FEnc(ProcessorSrNoStr))
            Timer1.Enabled = True
            lblAMCText.Text = ""
            lblAMCText.Refresh()

            'If ChkRemLogin.Checked = True Then
            '    Call btnLogIn_Click(sender, e)
            'End If
            'Me.TopMost = True
        Else
            Timer1.Enabled = True
        End If
    End Sub
    Private Function CpuId() As String
        Dim computer As String
        Dim wmi As Object
        Dim processors As Object
        Dim cpu As Object
        Dim cpu_ids As String

        computer = "."
        wmi = GetObject("winmgmts:" & _
            "{impersonationLevel=impersonate}!\\" & _
            computer & "\root\cimv2")
        processors = wmi.ExecQuery("Select * from " & _
            "Win32_Processor")

        For Each cpu In processors
            cpu_ids = cpu_ids & ", " & cpu.ProcessorId
        Next cpu
        If Len(cpu_ids) > 0 Then cpu_ids = Mid$(cpu_ids, 3)

        CpuId = cpu_ids
    End Function
    Private Sub StartInd()
        'Write_Log("PictureBox3_Click")
        'Dim IsDynamicKey As Boolean = False
        AppLicMode = "INDLIC"
        Timer1.Enabled = False
        'gVarInstanceID = "V-" & FunG_GetMACAddress()
        gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payalpatel
        'Write_Log("gVarInstanceID=" & gVarInstanceID)
        'obj = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MMM/dd/yyyy")
        'Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MM/dd/yyyy")
        '==========================================keval chakalasiya(15-2-2010)

        REM This block Set Master Expiry and Version title to global variable 
        Call clsGlobal.SetExpDate(DateSerial(2016, 12, 31))
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

            'Write_Log("MothrBoardSrNo -> Begin")
            'Dim MothrBoardSrNo As String = Microsoft.VisualBasic.Left(Trim(getmbserial()), 20)
            'Dim MothrBoardSrNo As String = Trim(getmbserial())
            'Write_Log("MothrBoardSrNo ->" & MothrBoardSrNo)
            'Write_Log("HDDSrNoStr -> Begin")
            'Dim HDDSrNoStr As String = LoadDiskInfo()
            'Write_Log("HDDSrNoStr ->" & HDDSrNoStr)
            'Write_Log("ProcessorSrNoStr -> Begin")
            'Dim ProcessorSrNoStr As String = GetProcessorSeialNumber(False)
            'Write_Log("ProcessorSrNoStr ->" & ProcessorSrNoStr)
            Dim VarUserCode As String = ""
            gvarInstanceCode = ""

            Dim MothrBoardSrNo As String = Trim(getmbserial())
            If MothrBoardSrNo.Length <= 3 Then
                MothrBoardSrNo = MothrBoardSrNo & "01" & FunG_GetMACAddress()
            End If

            'MsgBox("MothrBoardSrNo : " & MothrBoardSrNo)
            Dim HDDSrNoStr As String = LoadDiskInfo()
            If HDDSrNoStr.Length <= 3 Then
                HDDSrNoStr = HDDSrNoStr & "02" & FunG_GetMACAddress()
            End If

            'MsgBox("HDDSrNoStr : " & HDDSrNoStr)
            Dim ProcessorSrNoStr As String = GetProcessorSeialNumber(False)
            If ProcessorSrNoStr.Length <= 3 Then
                ProcessorSrNoStr = ProcessorSrNoStr & "03" & FunG_GetMACAddress()
            End If


            'InputBox("", "", MothrBoardSrNo & " || " & ProcessorSrNoStr & " || " & HDDSrNoStr)
            'End
            If (MothrBoardSrNo.Trim = "" Or UCase(MothrBoardSrNo.Trim) = UCase("BASE BOARD SERIAL NUMBER")) And HDDSrNoStr.Trim = "" Then
                MsgBox("Code cannot be generated.", MsgBoxStyle.Critical)
                End
                'IsDynamicKey = True
            End If
            If (MothrBoardSrNo.Trim = "" Or UCase(MothrBoardSrNo.Trim) = UCase("BASE BOARD SERIAL NUMBER")) And ProcessorSrNoStr.Trim = "" Then
                MsgBox("Code cannot be generated.", MsgBoxStyle.Critical)
                End
                'IsDynamicKey = True
            End If
            If HDDSrNoStr.Trim = "" And ProcessorSrNoStr.Trim = "" Then
                MsgBox("Code cannot be generated.", MsgBoxStyle.Critical)
                End
                'IsDynamicKey = True
            End If

            ' Dim str As String = get_rand_id(10)

            'If IsDynamicKey = True Then

            '    'End
            '    'Check in Ref is HWprnExist
            '    Dim strPWH As String = GetSetting("MicroSoft", "Windows", "PWH", "")
            '    If strPWH.Length > 0 Then
            '        'GetHWPrn
            '        HDDSrNoStr = strPWH.Split("|")(0)
            '        MothrBoardSrNo = strPWH.Split("|")(1)
            '        ProcessorSrNoStr = strPWH.Split("|")(2)
            '    Else
            '        'SetHWPrn
            '        HDDSrNoStr = get_rand_id(14)
            '        MothrBoardSrNo = get_rand_id(12)
            '        ProcessorSrNoStr = get_rand_id(24)

            '        SaveSetting("MicroSoft", "Windows", "PWH", HDDSrNoStr & "|" & MothrBoardSrNo & "|" & ProcessorSrNoStr)

            '    End If

            'End If

            REM Generate User code using DSS.dll
            'Write_Log("VarUserCode -> Begin")
            VarUserCode = Client_encry.Client_encry.get_client_key(HDDSrNoStr, MothrBoardSrNo, ProcessorSrNoStr, "S10XQ6BJ4G8S26JSN")
            gvarInstanceCode = VarUserCode & "|" & My.Computer.Name & "|" & My.User.Name
            'Write_Log("VarUserCode ->" & VarUserCode)
            REM End

            REM This block check generated user code exist in Licence file. If exist then continue otherwise display Key.txt file
            'Write_Log("Varify_Act_Key -> Begin")
            If Varify_Act_Key(VarUserCode) = False Then
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
                FSKeyFile.WriteLine("Client Key:" & VarUserCode)
                FSKeyFile.WriteLine("Version:" & GVar_Version_Title)
                FSKeyFile.Close()
                Dim VarExplorerArg As String = "/select, " & Application.StartupPath & "\Key.txt"
                Shell("Explorer.exe " + VarExplorerArg, AppWinStyle.NormalFocus, True, -1)
                Application.Exit()
                End
            End If
            REM End

            REM This block check whether any version match with licence file
            If VarAppVersion.Trim <> "" Then
                If VarAppVersion.Trim <> GVar_Version_Title.Trim Then
                    MsgBox("Application Version not valid!!", MsgBoxStyle.Exclamation)
                    GoTo lblKeyFile
                End If
            End If
            REM End

            REM This block check whether version is AMC then display AMC version Text
            If VarIsAMC = True Then
                lblAMCText.Text = "AMC Active"
            Else
                lblAMCText.Text = "AMC Not Active"
            End If
            lblAMCText.Refresh()
            REM End

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

        CheckTelNet_Connection()

        If NetMode = "NET" Then
            MDI.Timer_Net.Interval = Timer_Net_Interval
            MDI.Timer_Net.Enabled = True
            MDI.Timer_Net.Start()
            Call MDI.Timer_Net_Tick(MDI.Timer_Net, New EventArgs)
        ElseIf NetMode = "TCP" Then
            ' gVarInstanceID = "V-" & FunG_GetMACAddress()
            gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel

            'Write_Log("gVarInstanceID=" & gVarInstanceID)

            MDI.Timer_Sql.Interval = Timer_Sql_Interval
            MDI.Timer_Sql.Enabled = True
            MDI.Timer_Sql.Start()
        End If


        MDI.Show()
        Me.Cursor = Cursors.Default

        Dim obj_DelSetDisableMenubar As New GDelegate_MdiStatus(AddressOf MDI.Sub_Disable_Manu_n_Tool_bar)
        obj_DelSetDisableMenubar.Invoke(Boolean.TrueString, "Connected..")

        Me.Hide()
    End Sub

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

    Private Function GetDriveSerialNumber() As String
        '    Dim DriveSerial As Integer
        '    'Create a FileSystemObject object
        '    Dim fso As Object = CreateObject("Scripting.FileSystemObject")
        '    Dim Drv As Object = fso.GetDrive(fso.GetDriveName(Application.StartupPath))
        '    With Drv
        '        If .IsReady Then
        '            DriveSerial = .SerialNumber
        '        Else    '"Drive Not Ready!"
        '            DriveSerial = -1
        '        End If
        '    End With
        '    Return DriveSerial.ToString("X2")
        'Dim hd1 As New ManagementObjectSearcher("SELECT * FROM Win32_Processor")
        'For Each dvs As ManagementObject In hd1.Get()
        '    Dim serial As String = dvs("ProcessorSerialno").ToString()

        'Next
        Dim hd As New ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia")
        For Each dvs As ManagementObject In hd.Get()
            Dim serial As String = dvs("SerialNumber").ToString()

            Return serial.Trim
        Next

    End Function
    Public Shared StartUpExpire_Date As Date = clsGlobal.SetExpDate(DateSerial(2016, 12, 31)) ' CDate("2012-12-31") 'CDate("2011-04-30")



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
        clsGlobal.InternetVersionFlag = True
        Timer1.Enabled = False
        'gVarInstanceID = "V-" & FunG_GetMACAddress()
        gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel
        'Write_Log("gVarInstanceID=" & gVarInstanceID)

        'obj = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MMM/dd/yyyy")
        'Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MM/dd/yyyy")
        '==========================================keval chakalasiya(15-2-2010)

        REM This block Set Master Expiry and Version title to global variable 
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
            Dim MothrBoardSrNo As String = Trim(getmbserial())
            If MothrBoardSrNo.Length <= 3 Then
                MothrBoardSrNo = MothrBoardSrNo & "01" & FunG_GetMACAddress()
            End If

            'MsgBox("MothrBoardSrNo : " & MothrBoardSrNo)
            Dim HDDSrNoStr As String = LoadDiskInfo()
            If HDDSrNoStr.Length <= 3 Then
                HDDSrNoStr = HDDSrNoStr & "02" & FunG_GetMACAddress()
            End If

            'MsgBox("HDDSrNoStr : " & HDDSrNoStr)
            Dim ProcessorSrNoStr As String = GetProcessorSeialNumber(False)
            If ProcessorSrNoStr.Length <= 3 Then
                ProcessorSrNoStr = ProcessorSrNoStr & "03" & FunG_GetMACAddress()
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

        For Each drow As DataRow In DTUserMasterde.Select("F21='" & MothrBoardSrNo & "' And F22='" & HDDSrNoStr & "' And F23='" & ProcessorSrNoStr & "' And F2='" & txtUserName.Text & "' And F3='" & txtPassword.Text & "' And F6 = '" & gstr_ProductName & "'")
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

            ObjLoginData.TCP = Math.Abs(CInt(CBool(drow("TCP"))))



            Dim sdate() As String
            sdate = sExpire_Date.ToString.Split("/")


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
        If System.IO.File.Exists(Application.StartupPath & "\Licence.lic") Then
            Call StartInd()
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
    Private Sub btnLogIn_Click11(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Cursor = Cursors.Default
        GBOTP.Visible = True




        Call PictureBox3_Click(sender, e)

lbl:
        Try
            Dim client As New Net.Sockets.TcpClient(RegServerIP.Split(",")(0), RegServerIP.Split(",")(1))
        Catch ex As Exception
            If MsgBox("Internet is Down.       " & vbCrLf & "Do you want to retry?", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                GoTo lbl
            Else
                Application.Exit()
                End
            End If
        End Try


        If ObjLoginData.GetTodayDate().ToString("dd/MMM/yyyy") <> Today.Date.ToString("dd/MMM/yyyy") Then
            MsgBox("Please Set Your System Date.", MsgBoxStyle.OkOnly, "VolHedge")
            'Application.Exit()
            'End
            Exit Sub
        End If


        REM Check Expiry date Check with System data
        If Today >= CDate(clsGlobal.Expire_Date) Then
            MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
            Call clsGlobal.Sub_Get_Version_TextFile()
            Application.Exit()
            End
            Exit Sub
        End If
        REM End

        'DTUserMasterde = ObjLoginData.Select_User_Master(False)
        If txtUserName.Text.Trim.Length <= 0 Then
            MsgBox("Invalid UserName!!!")
            txtUserName.Focus()
            Exit Sub
        End If
        If txtPassword.Text.Trim.Length <= 0 Then
            MsgBox("Invalid Password!!!")
            txtPassword.Focus()
            Exit Sub
        End If

        Dim strOTP As String = generateOTP()
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
        If Not DTUserMasterde.Select("F2='" & txtUserName.Text & "'").Length > 0 Then

            MsgBox("Invalid User!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
            WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
            txtUserName.Focus()
            'Return False
            Exit Sub
        End If
        If Not DTUserMasterde.Select(" F3='" & txtPassword.Text & "'").Length > 0 Then

            MsgBox("Invalid Password!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
            WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
            txtUserName.Focus()
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
            If MsgBox("           User not active!!!                     " & vbCrLf & "           Do you want to activate user?       ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                GBoxActivate.Visible = True
                txtActivationCode.Focus()

            End If
            'Application.Exit()
            Exit Sub
        End If
        'DTUserMasterde = ObjLoginData.Select_User_Master(False)
        DTUserMasterde = ObjLoginData.Select_User_Master1(False, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(MothrBoardSrNo), clsUEnDe.FEnc(HDDSrNoStr), clsUEnDe.FEnc(ProcessorSrNoStr))
        REM This block check generated user code exist in Licence file. If exist then continue otherwise display Key.txt file
        If Varify_User1(False) = False Then
            'Application.Exit()
            'Exit Sub
            Call StartInd()
        End If
        REM End

        'REM This block check whether version is AMC then display AMC version Text
        'If VarIsAMC = True Then
        '    lblAMCText.Text = "AMC Active"
        'Else
        '    lblAMCText.Text = "AMC Not Active"
        'End If
        'lblAMCText.Refresh()
        'REM End


        ObjLoginData.SetLoginState("in")




        CheckTelNet_Connection()

        If NetMode = "NET" Then
            MDI.Timer_Net.Interval = Timer_Net_Interval
            MDI.Timer_Net.Enabled = True
            MDI.Timer_Net.Start()
            Call MDI.Timer_Net_Tick(MDI.Timer_Net, New EventArgs)
        ElseIf NetMode = "TCP" Then
            '      gVarInstanceID = "V-" & FunG_GetMACAddress()
            gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel
            fill_token()
            'Write_Log("gVarInstanceID=" & gVarInstanceID)
            MDI.Timer_Sql.Interval = Timer_Sql_Interval
            MDI.Timer_Sql.Enabled = True
            MDI.Timer_Sql.Start()
        End If

        '   MDI.Show()
        Me.Cursor = Cursors.Default

        Dim obj_DelSetDisableMenubar As New GDelegate_MdiStatus(AddressOf MDI.Sub_Disable_Manu_n_Tool_bar)
        obj_DelSetDisableMenubar.Invoke(Boolean.TrueString, "Connected..")

        If ChkRemLogin.Checked Then
            Dim FW As New IO.StreamWriter(Application.StartupPath & "\LoginInfo.txt")
            FW.WriteLine("LoginId ::" & txtUserName.Text)
            FW.WriteLine("Password ::" & txtPassword.Text)
            FW.Close()
        Else
            IO.File.Delete(Application.StartupPath & "\LoginInfo.txt")
        End If

        '  Me.Hide()
    End Sub
    Private Sub btnLogIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogIn.Click




        Me.Cursor = Cursors.WaitCursor
        '        Call PictureBox3_Click(sender, e)

lbl:
        Try
            Dim client As New Net.Sockets.TcpClient(RegServerIP.Split(",")(0), RegServerIP.Split(",")(1))
        Catch ex As Exception
            If MsgBox("Internet is Down.       " & vbCrLf & "Do you want to retry?", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                GoTo lbl
            Else
                Application.Exit()
                End
            End If
        End Try


        If ObjLoginData.GetTodayDate().ToString("dd/MMM/yyyy") <> Today.Date.ToString("dd/MMM/yyyy") Then
            MsgBox("Please Set Your System Date.", MsgBoxStyle.OkOnly, "VolHedge")
            'Application.Exit()
            'End
            Exit Sub
        End If


        REM Check Expiry date Check with System data
        If Today >= CDate(clsGlobal.Expire_Date) Then
            MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
            Call clsGlobal.Sub_Get_Version_TextFile()
            Application.Exit()
            End
            Exit Sub
        End If
        REM End

        'DTUserMasterde = ObjLoginData.Select_User_Master(False)
        If txtUserName.Text.Trim.Length <= 0 Then
            MsgBox("Invalid UserName!!!")
            txtUserName.Focus()
            Exit Sub
        End If
        If txtPassword.Text.Trim.Length <= 0 Then
            MsgBox("Invalid Password!!!")
            txtPassword.Focus()
            Exit Sub
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
        If Not DTUserMasterde.Select("F2='" & txtUserName.Text & "'").Length > 0 Then

            MsgBox("Invalid User!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
            WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
            txtUserName.Focus()
            'Return False
            Exit Sub
        End If
        If Not DTUserMasterde.Select(" F3='" & txtPassword.Text & "'").Length > 0 Then

            MsgBox("Invalid Password!!!" & vbCrLf & "Please Contact Vendor. " & vbCrLf & "")
            WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
            txtUserName.Focus()
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
            If MsgBox("           User not active!!!                     " & vbCrLf & "           Do you want to activate user?       ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                GBoxActivate.Visible = True
                txtActivationCode.Focus()

            End If
            'Application.Exit()
            Exit Sub
        End If
        'DTUserMasterde = ObjLoginData.Select_User_Master(False)

        DTUserMasterde = ObjLoginData.Select_User_Master1(False, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(MothrBoardSrNo), clsUEnDe.FEnc(HDDSrNoStr), clsUEnDe.FEnc(ProcessorSrNoStr))

        REM This block check generated user code exist in Licence file. If exist then continue otherwise display Key.txt file
        If Varify_User1(False) = False Then
            'Application.Exit()
            'Exit Sub
            Call StartInd()
        End If
        REM End

        'REM This block check whether version is AMC then display AMC version Text
        'If VarIsAMC = True Then
        '    lblAMCText.Text = "AMC Active"
        'Else
        '    lblAMCText.Text = "AMC Not Active"
        'End If
        'lblAMCText.Refresh()
        'REM End
        If clsGlobal.RagisterFlag = False Then

            clsGlobal.RagisterFlag = True
            Dim strOTP As String = generateOTP()
            ObjLoginData.PreOTP = strOTP
            ObjLoginData.Update_User_Master()
            SendOtpmail()
            SendOTPmessage()
            MessageBox.Show("OTP Send On your Register Mobileno And EmailAddress SucessFully..")
            GBOTP.Location = New Point(397, 60)
            GBLOGIN1.Visible = False
            GBOTP.Visible = True
            'ObjLoginData.SetLoginState("in")
        End If

        Me.Cursor = Cursors.Default
        Me.Show()
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
            Dim strmessage As String = "Dear Customer. FinIdeas VolHegde OTP is " + "" + ObjLoginData.PreOTP
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
            Dim strmessage As String = "Dear Customer. FinIdeas VolHegde OTP is " + "" + ObjLoginData.PreOTP

            'Dim strmessage As String = ObjLoginData.PreOTP + " " + "Is Your Login OTP."
            Dim str As String = "http://173.45.76.226:81/send.aspx?username=flnideas&pass=flnideas123&route=premium&senderid=FINIDE&numbers=" + ObjLoginData.Mobile + "&message=" + strmessage + ""
            Dim myWebRequest As WebRequest = WebRequest.Create(str)
            'Dim myWebResponse As WebResponse = myWebRequest.GetResponse()
            Dim wbResp As HttpWebResponse = DirectCast(myWebRequest.GetResponse(), HttpWebResponse)
        Catch ex As Exception
        End Try
    End Function
    Function SendOtpmailreg()
        Dim Str As String

        Str = "<!DOCTYPE HTML>" & vbCrLf
        Str = Str & "<html lang='en-US'>" & vbCrLf
        Str = Str & "<head>" & vbCrLf
        Str = Str & "<meta charset='UTF-8'>" & vbCrLf
        Str = Str & "<title></title>" & vbCrLf
        Str = Str & "</head>" & vbCrLf
        Str = Str & "<body>" & vbCrLf
        Str = Str & "<p>Dear Customer, </p>" & vbCrLf
        Str = Str & "<p><B>FinIdeas VolHegde OTP is  :</B>" & ObjLoginData.PreOTP & " </p>" & vbCrLf
        Str = Str & "</body>" & vbCrLf
        Str = Str & "</html>" & vbCrLf

        Dim email As String = txtEmail.Text
        send_emailOTP("Software@finideas.com", "Finideas123", email, "VolHedge OTP Varification", Str)
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
        Str = Str & "<p><B>FinIdeas VolHegde OTP is  :</B>" & ObjLoginData.PreOTP & " </p>" & vbCrLf
        Str = Str & "</body>" & vbCrLf
        Str = Str & "</html>" & vbCrLf
        Dim email As String = ObjLoginData.Email
        send_emailOTP("Software@finideas.com", "Finideas123", email, "VolHedge OTP Varification", Str)
    End Function
    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GBoxUserMaster.Enter

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If txtUserId.Text.Trim.Length <= 0 Or txtUserId.Text.Trim.ToUpper = "ADMIN" Then
            MsgBox("Invalid UserId!")
            txtUserId.Focus()
            Exit Sub
        End If

        If TxtPwd.Text.Trim.Length <= 0 Then
            MsgBox("Invalid Passwod!")
            TxtPwd.Focus()
            Exit Sub
        End If

        If TxtPwd.Text <> TxtPwdConfirm.Text Then
            MsgBox("Password Not Match With Confirm Password!")
            Exit Sub
        End If

        If TxtName.Text.Trim.Length <= 0 Then
            MsgBox("Invalid User Name!")
            TxtName.Focus()
            Exit Sub
        End If

        If TxtAddress.Text.Trim.Length <= 0 Then
            MsgBox("Invalid User Address!")
            TxtAddress.Focus()
            Exit Sub
        End If

        If txtEmail.Text.Trim.Length <= 0 Then
            MsgBox("Invalid Email !")
            txtEmail.Focus()
            Exit Sub
        End If

        If TxtCity.Text.Trim.Length <= 0 Then
            MsgBox("Invalid User City!")
            TxtCity.Focus()
            Exit Sub
        End If

        If TxtMobNo.Text.Trim.Length <= 0 Then
            MsgBox("Invalid User Mobile No.!")
            TxtMobNo.Focus()
            Exit Sub
        End If

        If TxtFirm.Text.Trim.Length <= 0 Then
            MsgBox("Invalid Organisation!")
            TxtFirm.Focus()
            Exit Sub
        End If

        'If MothrBoardSrNo = " " Or HDDSrNoStr = " " Or ProcessorSrNoStr = " " Or gstr_ProductName = " " Then
        '    MessageBox.Show("Code")
        '    Exit Sub
        'End If
        If CheckValidation() = False Then
            Exit Sub
        End If

        If GB1.Enabled = True Then
            If clsGlobal.RagisterFlag = False Then
                clsGlobal.RagisterFlag = True
                Dim strOTP As String = generateOTP()
                ObjLoginData.PreOTP = strOTP
                SendOTPmessagereg()
                SendOtpmailreg()
                btnresend.Enabled = True
                GB1.Enabled = False
                GBOTP1.Visible = True
                MessageBox.Show("OTP Send on Your MobileNo And EmailId sucessfully..")
                GBOTP1.Visible = True
                Exit Sub
            End If
        End If

        If txtonetimepwd.Text.Trim.Length <= 0 Then
            MsgBox("Invalid OTP!")
            txtonetimepwd.Focus()
            Exit Sub
        End If
        If clsGlobal.RagisterOTP = False Then
            MsgBox("Please Verify Your Register Mobile No:")
            Exit Sub
        End If
        'If TxtPwd.Text.Trim.Length <= 0 Or TxtName.Text.Trim.Length <= 0 Or TxtCity.Text.Trim.Length <= 0 Or TxtAddress.Text.Trim.Length <= 0 Or TxtMobNo.Text.Trim.Length <= 0 Or TxtFirm.Text.Trim.Length <= 0 Then
        '    MsgBox("please fill all mandatory(*) fields")
        'End If

    End Sub
    Public Sub loadregisterdata(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.WaitCursor
        SetData()


        ObjLoginData.Insert_User_Master()

        WriteLog("Save/Edit User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")

        txtUserName.Text = txtUserId.Text
        txtPassword.Text = TxtPwd.Text
        btnDemoActivation_Click(sender, e)
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
        'Application.Restart()
        'If ChkRemLogin.Checked Then

        If IO.File.Exists(Application.StartupPath & "\LoginInfo.txt") Then
            IO.File.Delete(Application.StartupPath & "\LoginInfo.txt")
        End If

        Dim FW As New IO.StreamWriter(Application.StartupPath & "\LoginInfo.txt")
        FW.WriteLine("LoginId ::" & txtUserId.Text)
        FW.WriteLine("Password ::" & TxtPwd.Text)
        FW.Close()

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
            ChkRemLogin.Checked = False
        End If




        Dim FWmail As New IO.StreamWriter(Application.StartupPath & "\MailInfo.txt")
        FWmail.WriteLine("MailDate ::" & Date.Now())
        FWmail.WriteLine("MailFlag ::" & "False")
        FWmail.WriteLine("ClientMail ::" & txtEmail.Text)

        FWmail.Close()



        ObjLoginData.Update_User_Master()
        MsgBox("Registered Successfully ", MsgBoxStyle.Information)
        MsgBox("For More Details Visit Your Register EmailID.. ", MsgBoxStyle.Information)
        Me.Cursor = Cursors.Default

    End Sub



    Public Sub send_emailOTP(ByVal senderemail As String, ByVal senderpassword As String, ByVal receiveremail As String, ByVal subject As String, ByVal message As String)

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
            'MessageBox.Show(Convert.ToString("Email Sent to ") & receiveremail)
        Catch ex As Exception
            'MessageBox.Show(ex.ToString())
            MessageBox.Show("Email Is Invalid..")
        End Try

    End Sub
    Public Sub send_email(ByVal senderemail As String, ByVal senderpassword As String, ByVal receiveremail As String, ByVal subject As String, ByVal message As String)
        '  If MessageBox.Show((Convert.ToString("This will send an email to ") & receiveremail) + " are you sure ?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
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
            'MessageBox.Show(Convert.ToString("Email Sent to ") & receiveremail)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        '  End If
    End Sub
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

        ObjLoginData.M = MothrBoardSrNo
        ObjLoginData.H = HDDSrNoStr
        ObjLoginData.P = ProcessorSrNoStr

        ObjLoginData.city = TxtCity.Text

    End Sub

    ''' <summary>
    ''' CheckValidation
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This function use to check validation</remarks>
    ''' Private Function CheckValidation() As Boolean

    REM This block use to Check Same Dealer Name Already Exist or not
    Private Function CheckValidation() As Boolean
    
        If DTUserMasterde.Select("F21='" & MothrBoardSrNo & "' And F22='" & HDDSrNoStr & "' And F23='" & ProcessorSrNoStr & "' And F6 ='" & gstr_ProductName & "'").Length > 0 Then 'F2='" & txtUserId.Text & "' And 
            MsgBox("Someone already use this PC.  !!!" & vbCrLf & "" & vbCrLf & "")
            WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
            txtUserName.Focus()
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GBoxUserMaster.Visible = True
        GBoxUserMaster.Top = 5
        GBoxUserMaster.Left = 13
        GBLOGIN1.Visible = False
        GBOTP.Visible = False
        txtUserId.Focus()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        End
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        End
    End Sub


    Private Function ChkSQLConn() As Boolean

        Try
            Dim telnetServerIp As String = ""
            Dim telnetPort As Integer = 23

            telnetServerIp = RegServerIP.Split(",")(0)
            If RegServerIP.Split(",").Length <= 1 Then
                telnetPort = 1433
            Else
                telnetPort = RegServerIP.Split(",")(1)
            End If



            Dim client As New TcpClient(telnetServerIp, telnetPort)
            'MessageBox.Show("Server is reachable")
            'Return True
        Catch ex As Exception
            Return False

        End Try
        '====================================================
        Dim Result As Boolean
        Result = True

        If ConState = "SQLCON" Then
            Dim StrConn As String = ""
            StrConn = " Data Source=" & RegServerIP & ";Network Library=DBMSSOCN;Initial Catalog=" & "finideas" & ";User ID=" & "finideas" & ";Password=" & "finideas#123" & ";"

            'Data Source=190.190.200.100,1433;Network Library=DBMSSOCN;Initial Catalog=myDataBase;User ID=myUsername;Password=myPassword;
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
        ElseIf ConState = "WEBCON" Then
            Return ObjWebCon.CheckCon()
        End If


    End Function


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
        Application.Restart()

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
        If ObjLoginData.PreOTP = txtotp.Text Then
            Call PictureBox3_Click(sender, e)
            Dim DTUserMasterde As New DataTable
            DTUserMasterde = ObjLoginData.Select_User_Master1(True, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(MothrBoardSrNo), clsUEnDe.FEnc(HDDSrNoStr), clsUEnDe.FEnc(ProcessorSrNoStr))


            Dim otp As String = DTUserMasterde.Rows(0).Item("PreOTP").ToString().Trim()
            If ObjLoginData.PreOTP <> otp Then
                MessageBox.Show("Invalid OTP..")
                txtotp.Focus()
                Return
            End If


            ObjLoginData.SetLoginState("in")
            CheckTelNet_Connection()

            If NetMode = "NET" Then
                MDI.Timer_Net.Interval = Timer_Net_Interval
                MDI.Timer_Net.Enabled = True
                MDI.Timer_Net.Start()
                Call MDI.Timer_Net_Tick(MDI.Timer_Net, New EventArgs)
            ElseIf NetMode = "TCP" Then
                '      gVarInstanceID = "V-" & FunG_GetMACAddress()
                gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel
                fill_token()
                'Write_Log("gVarInstanceID=" & gVarInstanceID)
                MDI.Timer_Sql.Interval = Timer_Sql_Interval
                MDI.Timer_Sql.Enabled = True
                MDI.Timer_Sql.Start()
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
            Else
                IO.File.Delete(Application.StartupPath & "\LoginInfo.txt")
            End If

            Me.Hide()
        Else
            MsgBox("Invalid OTP..")
        End If
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOtp1.Click
        'Dim DTUserMasterde As New DataTable
        'DTUserMasterde = ObjLoginData.Select_User_Master1(True, clsUEnDe.FEnc(txtUserName.Text), clsUEnDe.FEnc(MothrBoardSrNo), clsUEnDe.FEnc(HDDSrNoStr), clsUEnDe.FEnc(ProcessorSrNoStr))
        'Dim otp As String = DTUserMasterde.Rows(0).Item("PreOTP").ToString().Trim()
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

    Private Sub TxtMobNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtMobNo.Leave

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
        GB1.Enabled = True
        GBOTP1.Visible = False
    End Sub



    Private Sub btnresend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnresend.Click
        SendOTPmessagereg()
        SendOtpmailreg()
        MessageBox.Show("Resend OTP SuccessFully on your Mobileno And EmailId")
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
        MessageBox.Show("Resend OTP SuccessFully on your Mobileno And EmailId")
    End Sub

    Private Sub TxtMobNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtMobNo.KeyDown


    End Sub
End Class
