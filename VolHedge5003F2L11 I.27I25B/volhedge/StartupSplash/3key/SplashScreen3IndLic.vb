Imports Microsoft.Win32
Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports system.Threading
Imports VolHedge.DAL
Imports System.Data


Public NotInheritable Class SplashScreen3IndLic
    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).

    Dim VarIsAMC As Boolean = False
    Dim VarAppVersion As String = ""

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

    ''' <summary>
    ''' SplashScreen1_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>WHen this method call to Timer Enable</remarks>
    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'wait(1000)
        Write_Log1("Load============================================================")
        Timer1.Enabled = True
        lblAMCText.Text = ""
        lblAMCText.Refresh()
    End Sub

    Public Shared StartUpExpire_Date As Date = clsGlobal.SetExpDate(DateSerial(2026, 12, 31)) ' CDate("2012-12-31") 'CDate("2011-04-30")
    'Public TmpExpDate As Date = New Date(2011, 9, 9)

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

        Dim hd As New ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia")
        For Each dvs As ManagementObject In hd.Get()
            Dim serial As String = dvs("SerialNumber").ToString()
            Return serial.Trim
        Next

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
    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        Write_Log1("_____________________________________________")
        '  gVarInstanceID = "V-" & FunG_GetMACAddress()
        gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel

        'gVarInstanceID = "C-" & FunG_GetMACAddress() 'change by payal patel
        Write_Log1("gVarInstanceID:" & gVarInstanceID)
        AppLicMode = "INDLIC"
        Timer1.Enabled = False


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

            Write_Log1("MB:")
            Dim MothrBoardSrNo As String = Microsoft.VisualBasic.Left(Trim(getmbserial() & ""), 20)
            If MothrBoardSrNo.Length <= 3 Then
                MothrBoardSrNo = MothrBoardSrNo & "01" & FunG_GetMACAddress()
            End If

            'Dim MothrBoardSrNo As String = Trim(getmbserial())
            Write_Log1("MB:" & MothrBoardSrNo)
            Write_Log1("HD:")
            Dim HDDSrNoStr As String = LoadDiskInfo()
            If HDDSrNoStr.Length <= 3 Then
                HDDSrNoStr = HDDSrNoStr & "02" & FunG_GetMACAddress()
            End If
            Write_Log1("HD:" & HDDSrNoStr)
            Write_Log1("PS:")
            Dim ProcessorSrNoStr As String = GetProcessorSeialNumber(False)
            If ProcessorSrNoStr.Length <= 3 Then
                ProcessorSrNoStr = ProcessorSrNoStr & "03" & FunG_GetMACAddress()
            End If
            Write_Log1("PS:" & ProcessorSrNoStr)







            'Dim VarUserCode As String = ""
            Dim VarMDUserCode As String = ""
            Dim VarHDUserCode As String = ""
            Dim VarPSUserCode As String = ""

            'Write_Log("MothrBoardSrNo -> Begin")
            'Dim MothrBoardSrNo As String = Microsoft.VisualBasic.Left(Trim(getmbserial()), 20)
            ''Dim MothrBoardSrNo As String = Trim(getmbserial())
            'Write_Log("MothrBoardSrNo ->" & MothrBoardSrNo)
            'Write_Log("HDDSrNoStr -> Begin")
            'Dim HDDSrNoStr As String = LoadDiskInfo()
            'Write_Log("HDDSrNoStr ->" & HDDSrNoStr)
            'Write_Log("ProcessorSrNoStr -> Begin")
            'Dim ProcessorSrNoStr As String = GetProcessorSeialNumber(False)
            'Write_Log("ProcessorSrNoStr ->" & ProcessorSrNoStr)



            'InputBox("", "", MothrBoardSrNo & " || " & ProcessorSrNoStr & " || " & HDDSrNoStr)
            'End
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

            'REM Generate User code using DSS.dll
            'Write_Log1("UC:")
            'VarUserCode = Client_encry.Client_encry.get_client_key(HDDSrNoStr, MothrBoardSrNo, ProcessorSrNoStr, "S10XQ6BJ4G8S26JSN")
            'gvarInstanceCode = VarUserCode & "|" & My.Computer.Name & "|" & My.User.Name
            'Write_Log1("UC:" & gvarInstanceCode)
            'REM End

            REM Generate User code using DSS.dll
            Write_Log1("MDUC:")
            VarMDUserCode = Client_encry.Client_encry.get_client_key("", MothrBoardSrNo, "", "S10XQ6BJ4G8S26JSN")
            gvarInstanceCode = VarMDUserCode & "|" & My.Computer.Name & "|" & My.User.Name
            Write_Log1("MDUC:" & gvarInstanceCode)
            REM End
            '============================================================================
            REM Generate User code using DSS.dll
            Write_Log1("HDUC:")
            VarHDUserCode = Client_encry.Client_encry.get_client_key(HDDSrNoStr, "", "", "S10XQ6BJ4G8S26JSN")
            gvarInstanceCode = VarHDUserCode & "|" & My.Computer.Name & "|" & My.User.Name
            Write_Log1("HDUC:" & gvarInstanceCode)
            REM End
            '===========================================================================
            REM Generate User code using DSS.dll
            Write_Log1("PSUC:")
            VarPSUserCode = Client_encry.Client_encry.get_client_key("", "", ProcessorSrNoStr, "S10XQ6BJ4G8S26JSN")
            gvarInstanceCode = VarPSUserCode & "|" & My.Computer.Name & "|" & My.User.Name
            Write_Log1("PSUC:" & gvarInstanceCode)
            REM End



            REM This block check generated user code exist in Licence file. If exist then continue otherwise display Key.txt file
            'Write_Log1("VK:")
            Write_Log1("MD|HD|PSVK:")
            Dim MDVK As Boolean = Varify_Act_Key(VarMDUserCode)
            Dim HDVK As Boolean = Varify_Act_Key(VarHDUserCode)
            Dim PSVK As Boolean = Varify_Act_Key(VarPSUserCode)


            'If Varify_Act_Key(VarUserCode) = False Then
            If Not ((MDVK = True And HDVK = True) Or (MDVK = True And PSVK = True) Or (HDVK = True And PSVK = True)) Then
                'If Today.Date >= TmpExpDate Then
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

                'FSKeyFile.WriteLine("Client Key:" & VarUserCode)
                FSKeyFile.WriteLine("Client Key1:" & VarMDUserCode)
                FSKeyFile.WriteLine("Client Key2:" & VarHDUserCode)
                FSKeyFile.WriteLine("Client Key3:" & VarPSUserCode)
                FSKeyFile.WriteLine("Version:" & GVar_Version_Title)
                FSKeyFile.Close()
                Dim VarExplorerArg As String = "/select, " & Application.StartupPath & "\Key.txt"
                Shell("Explorer.exe " + VarExplorerArg, AppWinStyle.NormalFocus, True, -1)
                Application.Exit()
                End
                'Else
                '    clsGlobal.Expire_Date = TmpExpDate
                'End If
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

            REM To Check Single instance working
            If Check_SingleInstance("VolHedge") = False Then
                End
            End If
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

        MDI.Show()
        'Me.Hide()
        'FrmMatchContract.ShowForm("FO")
        Me.Cursor = Cursors.Default
        Dim obj_DelSetDisableMenubar As New GDelegate_MdiStatus(AddressOf MDI.Sub_Disable_Manu_n_Tool_bar)
        obj_DelSetDisableMenubar.Invoke(Boolean.TrueString, "Connected..")

        CheckTelNet_Connection()

        If NetMode = "NET" Then
            MDI.Timer_Net.Interval = Timer_Net_Interval
            MDI.Timer_Index.Interval = Timer_Index_Interval
            MDI.Timer_Net.Enabled = True
            MDI.Timer_Net.Start()
            Call MDI.Timer_Net_Tick(MDI.Timer_Net, New EventArgs)
        ElseIf NetMode = "TCP" Then
            'gVarInstanceID = "V-" & FunG_GetMACAddress()
            gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel
            'gVarInstanceID = "C-" & FunG_GetMACAddress() 'change by payal patel
            'If (gVarInstanceID <> "V-") Then
            Write_Log1("TgVarInstanceID:" & gVarInstanceID)

            MDI.Timer_Sql.Interval = Timer_Sql_Interval
            MDI.Timer_Sql.Enabled = True
            MDI.Timer_Sql.Start()
            'Else
            '   MsgBox("TCP NOT CONNECTED")
            'End If
        End If
        Me.Hide()

    End Sub
    Private Function Check_SingleInstance(ByVal AppName As String) As Boolean
        If Check_EXEName(AppName) = False Then
            Return False
        End If
        Dim cnt As Integer = 0
        Dim proc As System.Diagnostics.Process
        For Each proc In System.Diagnostics.Process.GetProcessesByName(AppName)
            cnt += 1
        Next

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
            Write_Log1("VK:NF")
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
                Write_Log1("VK:NM")
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

        Return True
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        '    Write_Log("Varify_Act_Key -> End = True")
        '    Return False
        'End Try

    End Function

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
        Call PictureBox3_Click(sender, e)
        Timer1.Enabled = False
        'MsgBox(DateDiff(DateInterval.Second, Dt, Now))
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
