Imports Microsoft.Win32
Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
'Imports ImportData
Imports system.Threading
Imports VolHedge.DAL
Imports System.Data

Public NotInheritable Class SplashScreenSerLic

    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).
    Dim VarIsAMC As Boolean = False
    Dim VarAppVersion As String = ""

    ''' <summary>
    ''' SplashScreen1_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>WHen this method call to Timer Enable</remarks>
    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Timer1.Enabled = True
        lblAMCText.Text = ""
        lblAMCText.Refresh()
    End Sub

    Public Shared StartUpExpire_Date As Date = clsGlobal.SetExpDate(DateSerial(2020, 12, 31)) ' CDate("2012-12-31") 'CDate("2011-04-30")




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
        AppLicMode = "SERLIC"
        Timer1.Enabled = False
        '        gVarInstanceID = "V-" & FunG_GetMACAddress()
        gVarInstanceID = "B-" & FunG_GetMACAddress() 'change by payal patel
        'gVarInstanceID = "C-" & FunG_GetMACAddress() 'change by payal patel

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

            Call clsGlobal.LoadInitializeData()

            Me.Cursor = Cursors.WaitCursor

            
lblGet_UDP_IP_Port:
            If obj_CLS_UDP.GFun_Get_UDP_IP_Port() = False Then
                Dim obj_Frm_UDPSetting As New Frm_UDPSetting
                If obj_Frm_UDPSetting.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                    End
                Else
                    GoTo lblGet_UDP_IP_Port
                End If
            End If
            obj_CLS_UDP.GSub_Chk_Lic_From_Server()



            REM End
Auth:
            If G_bool_IsAuthanticated = True Then
                CheckTelNet_Connection()
                MDI.Show()
                Me.Cursor = Cursors.Default
                Me.Hide()
            Else
                GoTo Auth
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            Application.Exit()
        End Try
        
    End Sub

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
