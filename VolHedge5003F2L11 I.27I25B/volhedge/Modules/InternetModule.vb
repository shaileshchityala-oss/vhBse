Imports System.Threading
Imports System.IO

Imports System.Net


Module InternetModule
    Dim MyAccessSQL As New UDAL.data_access_sql
    Public ObjLoginData As New ClsLoginData
    Dim ED As New clsEnDe

    'Public ObjWebCon As New ClsWebCon

    Public Timer_Net_Interval As Integer = 10000
    Public Timer_Index_Interval As Integer = 30000
    Public bool_Thr_GetInterNetDat As Boolean
    Public resetEvents As ManualResetEvent()


    Public ConState As String = "SQLCON" ',"WEBCON""WEBCON" '
    Public gstr_ProductName As String = "VolHedge"

    Public gstr_Internet_Server As String = getIpaddress("volhedgeAPI.ddnsfree.com")  '"103.108.220.145,1433" '"edu.finideas.com,1433"
    Public gstr_Internet_DB As String = "finideasext"
    'Public gstr_Internet_Uid As String = "finideas"
    '    Public gstr_Internet_Uid As String = "sa"
    Public gstr_Internet_Uid As String = "volhedge"
    Public gstr_Internet_Pwd As String = getpassword(gstr_Internet_Server) '"db@321"

    'Public gstr_Internet_Server As String = "edu.finideas.com,1433"
    ''"116.74.94.242,1403" '"volhedge.ddnsfree.com,1403" '"edu.finideas.com,1433"
    'Public gstr_Internet_DB As String = "finideasext"
    'Public gstr_Internet_Uid As String = "finideas"
    'Public gstr_Internet_Pwd As String = "db@321" '"123456" '


    'Public strApplicationName As String = "VH_" + TCP_CON_NAME '"VolHedge"
    Public RegServerIP As String = "" '"finideas.com,1433" '"198.64.251.89,1433""203.109.66.198,1403" '
    Public RegServerUserid As String = "" '"finideas"
    Public RegServerpwd As String = "" '"finideas"
    Public RegServerdbnm As String = "" '"finideas"
    'Public RegServerIP As String = "172.16.12.56,1433"
    Public GVar_LogIn_User As String
    Public GVarSQLServerConnStr As String
    Dim frmsetting As New frmSettings
    Public Flg_SetRegServer As Boolean = False

    Public Function getpassword(ByVal ips As String) As String
        Dim result As String
        'If ips.Contains("43.239.79.39") Or ips.Contains("43.239.79.40") Or ips.Contains("116.74.94.242") Then

        '    ' result = "Fintest@123456"
        '    result = "123456"
        'Else
        '    '            result = "db@321"
        '    ' result = "Fintest@123456"
        '    result = "123456"
        'End If

        If ips.Contains("43.239.79.39") Or ips.Contains("43.239.79.40") Or ips.Contains("116.74.94.242") Then
            result = "Fintest@123456"
        Else
            '            result = "db@321"
            result = "Fintest@123456"
        End If
        Return result
        Return result
    End Function


    Public Function getIpaddress(ByVal servername As String) As String
        Try
            Dim result As String
            Dim domain As String = servername
            Dim ip_Addresses As IPAddress() = Dns.GetHostAddresses(domain)
            Dim ips As String = String.Empty
            For Each ipAddress As IPAddress In ip_Addresses
                ips += ipAddress.ToString()
            Next

            If ips.Contains("43.239.79.39") Or ips.Contains("43.239.79.40") Or ips.Contains("116.74.94.242") Then

                result = ips + ",1403"
            Else
                '             result = ips + ",1433"
                result = ips + ",1401"
            End If

            Return result
        Catch ex As Exception
            Return "edu.finideas.com,1433"

        End Try

    End Function
    Public Sub SetRegServer()

        Dim ttik As Int64 = DateTime.Now.Ticks
        Write_TimeLog1("Start SetRegServer")
        Dim dtable As DataTable
        Dim drconn As DataRow()
        'If IO.File.Exists(Application.StartupPath & "\SqlServerConnection.txt") Then

        dtable = New DataTable
        'With dtable.Columns
        '    .Add("SERVERNAME", GetType(String))

        '    .Add("DBNAME", GetType(String))

        '    .Add("USERNAME", GetType(String))
        '    .Add("PASSWORD", GetType(String))

        'End With
        Dim drow As DataRow


        'If File.Exists(Application.StartupPath & "\SqlServerConnection.txt") Then
        'Dim iline As New StreamReader(Application.StartupPath & "\SqlServerConnection.txt")
        Dim chk As Boolean
        chk = False
        'Dim i As Integer = 0
        'While iline.EndOfStream = False
        'If i > 0 Then
        chk = True

        'Dim iline1 As String = "103.228.79.44,1403|finideas|finideas|123456"
        'Dim iline2 As String = "202.71.8.227,1403|finideas|finideas|123456"
        'Dim iline3 As String = "60.254.95.18,1403|finideas|finideas|123456"




        'Dim line As String()
        'line = Split(iline1, "|")
        'If line(2) <> "" Then
        '    drow = dtable.NewRow
        '    drow("SERVERNAME") = CStr(line(0))
        '    '======================keval(16-2-10)
        '    drow("DBNAME") = CStr(line(1))
        '    '=======================
        '    drow("USERNAME") = CStr(line(2))
        '    drow("PASSWORD") = CStr(line(3))
        '    dtable.Rows.Add(drow)
        'End If

        'line = Split(iline2, "|")
        'If line(2) <> "" Then
        '    drow = dtable.NewRow
        '    drow("SERVERNAME") = CStr(line(0))
        '    '======================keval(16-2-10)
        '    drow("DBNAME") = CStr(line(1))
        '    '=======================
        '    drow("USERNAME") = CStr(line(2))
        '    drow("PASSWORD") = CStr(line(3))
        '    dtable.Rows.Add(drow)
        'End If


        'line = Split(iline3, "|")
        'If line(2) <> "" Then
        '    drow = dtable.NewRow
        '    drow("SERVERNAME") = CStr(line(0))
        '    '======================keval(16-2-10)
        '    drow("DBNAME") = CStr(line(1))
        '    '=======================
        '    drow("USERNAME") = CStr(line(2))
        '    drow("PASSWORD") = CStr(line(3))
        '    dtable.Rows.Add(drow)
        'End If



        'End If
        'i = i + 1
        'End While
        'iline.Close()

        If ChkSQLConnNew(gstr_Internet_Server, gstr_Internet_DB, gstr_Internet_Uid, gstr_Internet_Pwd) = False Then



            Dim result As DialogResult = MessageBox.Show("Do you want to set your connection..? ", "Connection Not Found..", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Application.Exit()
            ElseIf result = DialogResult.OK Then

                Dim analysis1 As New frmSettings

                For Each frm As Form In MDI.MdiChildren
                    frm.Close()
                Next
                analysis1.ShowForm(3)
                analysis1.Close()



            End If
            End

            '    Application.Restart()
            '    Dim _proceses As Process()
            '    Try
            '        _proceses = Process.GetProcessesByName("VolHedge")
            '        For Each proces As Process In _proceses
            '            proces.Kill()
            '            Process.Start(Application.StartupPath + "\VolHedge.exe")
            '        Next
            '    Catch ex As Exception
            '        MessageBox.Show("Error in process")
            '    End Try
            '    'Dim plist As Process() = Process.GetProcesses()
            '    'For Each p As Process In plist
            '    '    Try
            '    '        If p.MainModule.ModuleName = "VolHedge.exe" Then
            '    '            p.Kill()
            '    '            Process.Start(Application.StartupPath + "\VolHedge.exe")
            '    '        End If

            '    '    Catch
            '    '        'seems listing modules for some processes fails, so better ignore any exceptions here
            '    '    End Try
            '    'Next

            '    '=========================REM:Coding For NETMODE SETTING IN SETTING TABLE  17/06/2014===============


            'Else
            '    End
            'End If


        End If





        dtable = frmsetting.SelectdataofREGConnection()

        If dtable Is Nothing = True Then


            Exit Sub

        End If
        drconn = dtable.Select()

        Dim servernm As String = ""
        Dim db As String = ""
        Dim unm As String = ""
        Dim pwd As String = ""

        If drconn.Length > 0 Then
            For Each DR1 As DataRow In drconn
                servernm = DR1("SERVER") 'DR1("SERVERNAME")
                db = DR1("DBNAME")
                unm = DR1("USERNAME")
                pwd = DR1("PASSWORD")

                If ChkSQLConnNew(servernm, db, unm, pwd) = True Then
                    RegServerIP = servernm
                    RegServerdbnm = db
                    RegServerUserid = unm
                    RegServerpwd = pwd
                    Flg_SetRegServer = True
                    Exit For
                End If
            Next
        Else
            RegServerIP = GetSetting("GOT", "Load", "REGSERIP", "") 'REGSERIP()

            RegServerUserid = GetSetting("GOT", "Load", "REGSERUSER", "")
            RegServerpwd = GetSetting("GOT", "Load", "REGSERPWD", "")
            RegServerdbnm = GetSetting("GOT", "Load", "REGSERDB", "")

            Flg_SetRegServer = True
        End If
        'End If

        'If IO.File.Exists(Application.StartupPath & "\SqlServerConnection.txt") Then

        'End If

        'End If
        Write_TimeLog1("InternetModule-> End Fun-SetRegServer" + "|" + TimeSpan.FromTicks(DateTime.Now.Ticks - ttik).TotalSeconds.ToString())
    End Sub
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

        Try
            Dim client As New Net.Sockets.TcpClient(servername.Split(",")(0), servername.Split(",")(1))
            client.Client.Disconnect(True)
            client.Close()
            client = Nothing
        Catch ex As Exception
            If Not ex.Message.Contains("No connection could be made because the target machine actively refused it") Then
                MsgBox(ex.ToString())
                Return False
            End If
        End Try


        StrConn = " Data Source=" & servername & ";Initial Catalog=" & DATABASE & ";User ID=" & USERNAME & ";Password=" & PASSWORD & ";Connect Timeout=500;Application Name=" & "VH_" & servername & "_Test"
        'End If
        Dim ConTest As New System.Data.SqlClient.SqlConnection(StrConn)
        Try
            ConTest.Open()
            ConTest.Close()
            ConTest.Dispose()
            Return True

        Catch ex As Exception
            MsgBox(ex.ToString())
            ConTest.Dispose()
            Return False
        End Try
        'Else
        'Return False
        'End If
    End Function
    Public Sub GetInternetData(ByVal strComp As Object)
        Try


            Dim isCurr As Boolean = False
            Dim strCompany As String = ""
            Try
                strCompany = CType(strComp, String).Split(",")(1)
                strCompany = GetSymbol(strCompany.ToString())
            Catch ex As Exception
                strCompany = strComp
            End Try


            If strCompany.ToUpper.Contains("USDINR") Or strCompany.ToUpper.Contains("JPYINR") Or strCompany.ToUpper.Contains("GBPINR") Or strCompany.ToUpper.Contains("EURINR") Then
                isCurr = True
            End If

            Dim token_no As Long
            Dim buy_price As Double
            Dim sale_price As Double
            Dim last_trade_price As Double

            Dim eqtoken_no As Long
            Dim eqbuy_price As Double
            Dim eqsale_price As Double
            Dim eqlast_trade_price As Double

            Dim VolumeTradedToday As Double
            Dim ClosingPrice As Double

            Dim dttime As Date
            dttime = Now
            'For Each drow As DataRow In comptable.Select("", "company")
            If isCurr = True Then
                Dim DtCur As New DataTable
                If NetDataMode = "NSE" Then
                    DtCur = HtmlPars.GetCurInterNetData(GetSymbol(strCompany))
                ElseIf NetDataMode = "MCX" Then
                    DtCur = HtmlPars.GetMCXCurInterNetData(strCompany)
                End If

                For Each row As DataRow In DtCur.Rows
                    token_no = Val(row("Token") & "")
                    buy_price = 0
                    sale_price = 0
                    last_trade_price = Val(row("LastPrice") & "")
                    If last_trade_price.ToString() = "0" Then
                        Continue For
                    End If

                    VolumeTradedToday = Val(row("traded") & "")
                    ClosingPrice = Val(row("PrevClose") & "")

                    VarCurrentDate = DateDiff(DateInterval.Second, CDate("1-1-1980"), Now)


                    If Currfutall.Contains(token_no) Then
                        'Dim fltppr As Double
                        If Currfltpprice.Contains(token_no) Then
                            If FlgBcastStop = False Then
                                Currfbuyprice.Item(token_no) = buy_price
                                Currfsaleprice.Item(token_no) = sale_price
                                Currfltpprice.Item(token_no) = last_trade_price
                            End If
                        Else
                            Currfbuyprice.Add(token_no, buy_price)
                            Currfsaleprice.Add(token_no, sale_price)
                            Currfltpprice.Add(token_no, last_trade_price)
                        End If

                        If closeprice.Contains(token_no) Then
                            closeprice.Item(token_no) = ClosingPrice
                        Else
                            closeprice.Add(token_no, ClosingPrice)
                        End If

                    Else
                        If Currltpprice.Contains(token_no) Then
                            If FlgBcastStop = False Then


                                Currbuyprice.Item(token_no) = buy_price
                                Currsaleprice.Item(token_no) = sale_price
                                Currltpprice.Item(token_no) = last_trade_price
                                currMKTltpprice.Item(token_no) = last_trade_price
                            End If
                        Else
                            Currbuyprice.Add(token_no, buy_price)
                            Currsaleprice.Add(token_no, sale_price)
                            Currltpprice.Add(token_no, last_trade_price)
                            currMKTltpprice.Add(token_no, last_trade_price)
                        End If

                        If volumeprice.Contains(token_no) Then
                            If FlgBcastStop = False Then
                                volumeprice.Item(token_no) = VolumeTradedToday
                            End If
                        Else
                            volumeprice.Add(token_no, VolumeTradedToday)
                        End If

                        If closeprice.Contains(token_no) Then
                            If FlgBcastStop = False Then
                                closeprice.Item(token_no) = ClosingPrice
                            End If
                        Else
                            closeprice.Add(token_no, ClosingPrice)
                        End If

                    End If

                    'REM Equity
                    'If eqfutall.Contains(eqtoken_no) Then
                    '    If eltpprice.Contains(eqtoken_no) Then
                    '        ebuyprice.Item(eqtoken_no) = eqbuy_price
                    '        esaleprice.Item(eqtoken_no) = eqsale_price
                    '        eltpprice.Item(eqtoken_no) = eqlast_trade_price
                    '    Else
                    '        ebuyprice.Add(eqtoken_no, eqbuy_price)
                    '        esaleprice.Add(eqtoken_no, eqsale_price)
                    '        eltpprice.Add(eqtoken_no, eqlast_trade_price)
                    '    End If
                    '    'If eqarray.Contains(token_no) Then
                    '    '    cal_eq(token_no)
                    '    'End If
                    '    'process cm data
                    'End If
                Next
            Else
                'Dim DtIndex As New DataTable
                'DtIndex = HtmlPars.GetFoIndexNetData(strCompany)
                Dim DtFo As New DataTable

                '      HtmlPars.GetFoInterNetDataL2(strCompany)
                DtFo = HtmlPars.GetFoInterNetData(strCompany)
                For Each row As DataRow In DtFo.Rows
                    token_no = Val(row("Token") & "")
                    buy_price = 0
                    sale_price = 0
                    last_trade_price = Val(row("LastPrice") & "")
                    If last_trade_price.ToString() = "0" Then
                        Continue For
                    End If
                    eqtoken_no = Val(row("eqToken") & "")
                    eqbuy_price = 0
                    eqsale_price = 0
                    eqlast_trade_price = Val(row("UnderlyingValue") & "")
                    VolumeTradedToday = Val(row("traded") & "")
                    ClosingPrice = Val(row("PrevClose") & "")

                    VarCurrentDate = DateDiff(DateInterval.Second, CDate("1-1-1980"), Now)


                    If futall.Contains(token_no) Then
                        'Dim fltppr As Double
                        If fltpprice.Contains(token_no) Then

                            If FlgBcastStop = False Then


                                fbuyprice.Item(token_no) = buy_price
                                fsaleprice.Item(token_no) = sale_price

                                fltpprice.Item(token_no) = last_trade_price
                            End If
                        Else
                            fbuyprice.Add(token_no, buy_price)
                            fsaleprice.Add(token_no, sale_price)
                            fltpprice.Add(token_no, last_trade_price)
                        End If

                        If closeprice.Contains(token_no) Then
                            If FlgBcastStop = False Then


                                closeprice.Item(token_no) = ClosingPrice
                            End If
                        Else
                            closeprice.Add(token_no, ClosingPrice)
                        End If
                    Else
                        If ltpprice.Contains(token_no) Then
                            If FlgBcastStop = False Then

                                fltpprice.Item(token_no) = last_trade_price
                                buyprice.Item(token_no) = buy_price
                                saleprice.Item(token_no) = sale_price
                                ltpprice.Item(token_no) = last_trade_price
                                MKTltpprice(token_no) = last_trade_price
                            End If
                        Else
                            buyprice.Add(token_no, buy_price)
                            saleprice.Add(token_no, sale_price)
                            ltpprice.Add(token_no, last_trade_price)
                            MKTltpprice.Add(token_no, last_trade_price)
                            fltpprice.Add(token_no, last_trade_price)
                        End If

                        If volumeprice.Contains(token_no) Then
                            If FlgBcastStop = False Then


                                volumeprice.Item(token_no) = VolumeTradedToday
                            End If
                        Else
                            volumeprice.Add(token_no, VolumeTradedToday)
                        End If

                        If closeprice.Contains(token_no) Then
                            If FlgBcastStop = False Then


                                closeprice.Item(token_no) = ClosingPrice
                            End If
                        Else
                            closeprice.Add(token_no, ClosingPrice)
                        End If

                    End If

                    REM Equity
                    If eqfutall.Contains(eqtoken_no) Then
                        If eltpprice.Contains(eqtoken_no) Then
                            If FlgBcastStop = False Then


                                ebuyprice.Item(eqtoken_no) = eqbuy_price
                                esaleprice.Item(eqtoken_no) = eqsale_price
                                eltpprice.Item(eqtoken_no) = eqlast_trade_price
                            End If
                        Else
                            ebuyprice.Add(eqtoken_no, eqbuy_price)
                            esaleprice.Add(eqtoken_no, eqsale_price)
                            eltpprice.Add(eqtoken_no, eqlast_trade_price)
                        End If
                        'If eqarray.Contains(token_no) Then
                        '    cal_eq(token_no)
                        'End If
                        'process cm data
                    End If
                Next
            End If

            'Next

            If CType(strComp, String).Split(",")(0) <> "-" Then
                If CType(strComp, String).Split(",")(0) Is Nothing = False And CType(strComp, String).Split(",")(0) <> " " Then
                    '  MsgBox("From Date " & dttime.ToString & " To date " & Now.ToString & vbCrLf & DateDiff(DateInterval.Second, dttime, Now))
                    Dim index As Integer = CInt(CType(strComp, String).Split(",")(0))
                    resetEvents(index).[Set]()
                End If
            End If

        Catch ex As Exception
            ' MessageBox.Show(ex.ToString())
            WriteLog("Error In Get Internetdata" & vbCrLf & ex.ToString)
            If CType(strComp, String).Split(",")(0) <> "-" Then
                If CType(strComp, String).Split(",")(0) Is Nothing = False And CType(strComp, String).Split(",")(0) <> " " Then
                    '  MsgBox("From Date " & dttime.ToString & " To date " & Now.ToString & vbCrLf & DateDiff(DateInterval.Second, dttime, Now))
                    Dim index As Integer = CInt(CType(strComp, String).Split(",")(0))
                    resetEvents(index).[Set]()
                End If
            End If
        End Try

    End Sub
    Public Sub GetIndexData(ByVal Indexhtml As Object)
        Try


            'Dim isCurr As Boolean = False
            'Dim strCompany As String = ""
            'strCompany = CType(StrComp(), String).Split(",")(1)

            'If strCompany.ToUpper.Contains("USDINR") Or strCompany.ToUpper.Contains("JPYINR") Or strCompany.ToUpper.Contains("GBPINR") Or strCompany.ToUpper.Contains("EURINR") Then
            '    isCurr = True
            'End If


            'Dim IndexName As String
            'Dim IndexValue As Long

            'Dim iClosingPrice As Long
            ' Dim dtindex As New DataTable
            HtmlPars.GetFoIndexNetData(Indexhtml)

            'Next

            'If CType(StrComp(), String).Split(",")(0) <> "-" Then
            '    If CType(StrComp(), String).Split(",")(0) Is Nothing = False And CType(StrComp(), String).Split(",")(0) <> " " Then
            '        '  MsgBox("From Date " & dttime.ToString & " To date " & Now.ToString & vbCrLf & DateDiff(DateInterval.Second, dttime, Now))
            '        Dim index As Integer = CInt(CType(StrComp(), String).Split(",")(0))
            '        resetEvents(index).[Set]()
            '    End If
            'End If

        Catch ex As Exception
            ' MessageBox.Show(ex.ToString())
        End Try

    End Sub
    ''' <summary>
    ''' process_fo_Net
    ''' </summary>
    ''' <remarks>This method call to receiving Data From Internet</remarks>
    Private Sub process_fo_Net()
        Dim token_no As Long
        Dim buy_price As Double
        Dim sale_price As Double
        Dim last_trade_price As Double

        If GVarIsNewBhavcopy = True Then
            GVarIsNewBhavcopy = False
        End If
        'ED_fo = New clsEnDe
        Dim Cmd As SqlClient.SqlCommand
        Dim Dr As SqlClient.SqlDataReader
        'Dr = DAL.DA_SQL.FillListFo("dbo.Sp_SelectFo '" & gVarInstanceID & "'") 'Objsql.SelectFoToken()

        If DAL.DA_SQL._conFo.State = ConnectionState.Closed Then DAL.DA_SQL.open_Fo_connection()
        Cmd = New SqlClient.SqlCommand("dbo.Sp_004 '" & gVarInstanceID & "'", DAL.DA_SQL._conFo)
        Dr = Cmd.ExecuteReader()

        While Dr.Read()
            Try
                'token_no = Val(Dr("token_no") & "")
                'buy_price = Val(Dr("bprice") & "")
                'sale_price = Val(Dr("sprice") & "")
                'last_trade_price = Val(Dr("ltp") & "")

                'If VarBCurrentDate < Val(Dr("cdate") & "") Then
                '    VarBCurrentDate = Val(Dr("cdate") & "")
                'End If
                token_no = Val(Dr("F1") & "")
                buy_price = ED.DFo(Dr("F2"))
                sale_price = ED.DFo(Dr("F3"))
                last_trade_price = ED.DFo(Dr("F4"))

                If VarFoBCurrentDate < Val(ED.DFo(Dr("F6")) & "") Then
                    VarFoBCurrentDate = Val(ED.DFo(Dr("F6")) & "")
                End If

                If VarFoBCurrentDate >= G_VarExpiryDate1 Then
                    'ThreadReceive_cm.Abort()
                    'ThreadReceive_Currency.Abort()
                    'MsgBox("Please Contact Vendor, Trial Version Expired.", MsgBoxStyle.Exclamation)
                    'Call SplashScreen1.Sub_Get_Version_TextFile()
                    ''Application.Exit()
                    'End
                    'Exit Sub
                    IsVersionExpire = True
                    'ThreadReceive_fo.Abort()
                    Application.Exit()
                End If

                If futall.Contains(token_no) Then

                    'Dim fltppr As Double
                    If fltpprice.Contains(token_no) Then
                        fbuyprice.Item(token_no) = buy_price
                        fsaleprice.Item(token_no) = sale_price
                        fltpprice.Item(token_no) = last_trade_price
                    Else
                        fbuyprice.Add(token_no, buy_price)
                        fsaleprice.Add(token_no, sale_price)
                        fltpprice.Add(token_no, last_trade_price)
                    End If

                Else

                    If ltpprice.Contains(token_no) Then
                        buyprice.Item(token_no) = buy_price
                        saleprice.Item(token_no) = sale_price
                        ltpprice.Item(token_no) = last_trade_price
                    Else
                        buyprice.Add(token_no, buy_price)
                        saleprice.Add(token_no, sale_price)
                        ltpprice.Add(token_no, last_trade_price)
                    End If

                End If
            Catch ex As Threading.ThreadAbortException
                Threading.Thread.ResetAbort()
            End Try
        End While
        Dr.Close()
        Dr.Dispose()
        'End If
    End Sub
    ''' <summary>
    ''' process_cm_Net
    ''' </summary>
    ''' <remarks>This method call to receiving Data From SqlDb</remarks>
    Private Sub process_cm_Net()
        Dim token_no As Long
        Dim buy_price As Double
        Dim sale_price As Double
        Dim last_trade_price As Double

        'ED_cm = New clsEnDe
        Dim Cmd As SqlClient.SqlCommand
        Dim Dr As SqlClient.SqlDataReader
        'Dr = DAL.DA_SQL.FillListEq("dbo.Sp_005 '" & gVarInstanceID & "'") 'Objsql.SelectEqToken()
        If DAL.DA_SQL._conEq.State = ConnectionState.Closed Then DAL.DA_SQL.open_Eq_connection()
        Cmd = New SqlClient.SqlCommand("dbo.Sp_013 '" & gVarInstanceID & "'", DAL.DA_SQL._conEq)
        Dr = Cmd.ExecuteReader

        While Dr.Read()
            Try
                If Dr("Typ") = "EQ" Then
                    'token_no = Val(Dr("token_no") & "")
                    'buy_price = Val(Dr("bprice") & "")
                    'sale_price = Val(Dr("sprice") & "")
                    'last_trade_price = Val(Dr("ltp") & "")
                    token_no = Val(Dr("F1") & "")
                    buy_price = ED.DEq(Dr("F2"))
                    sale_price = ED.DEq(Dr("F3"))
                    last_trade_price = ED.DEq(Dr("F4"))

                    If VarEQBCurrentDate < Val(ED.DCur(Dr("F38")) & "") Then
                        VarEQBCurrentDate = Val(ED.DCur(Dr("F38")) & "")
                    End If

                    If VarEQBCurrentDate >= G_VarExpiryDate1 Then
                        'ThreadReceive_fo.Abort()
                        'ThreadReceive_Currency.Abort()
                        'MsgBox("Please Contact Vendor, Trial Version Expired.", MsgBoxStyle.Exclamation)
                        'Call SplashScreen1.Sub_Get_Version_TextFile()
                        ''Application.Exit()
                        'End
                        'Exit Sub
                        IsVersionExpire = True
                        Application.Exit()
                        'ThreadReceive_cm.Abort()
                    End If

                    If eqfutall.Contains(token_no) Then
                        If eltpprice.Contains(token_no) Then
                            ebuyprice.Item(token_no) = buy_price
                            esaleprice.Item(token_no) = sale_price
                            eltpprice.Item(token_no) = last_trade_price
                        Else
                            ebuyprice.Add(token_no, buy_price)
                            esaleprice.Add(token_no, sale_price)
                            eltpprice.Add(token_no, last_trade_price)
                        End If
                        'If eqarray.Contains(token_no) Then
                        '    cal_eq(token_no)
                        'End If
                        'process cm data
                    End If
                Else
                    '=======================
                    'token_no = Val(Dr("token_no") & "")
                    'buy_price = Val(Dr("bprice") & "")
                    'sale_price = Val(Dr("sprice") & "")
                    'last_trade_price = Val(Dr("ltp") & "")

                    token_no = Val(Dr("F1") & "")
                    buy_price = ED.DCur(Dr("F2"))
                    sale_price = ED.DCur(Dr("F3"))
                    last_trade_price = ED.DCur(Dr("F4"))

                    If VarCurBCurrentDate < Val(ED.DCur(Dr("F38")) & "") Then
                        VarCurBCurrentDate = Val(ED.DCur(Dr("F38")) & "")
                    End If

                    If VarCurBCurrentDate >= G_VarExpiryDate1 Then
                        'ThreadReceive_fo.Abort()
                        'ThreadReceive_cm.Abort()
                        'MsgBox("Please Contact Vendor, Trial Version Expired.", MsgBoxStyle.Exclamation)
                        'Call SplashScreen1.Sub_Get_Version_TextFile()
                        ''Application.Exit()
                        'End
                        'Exit Sub
                        IsVersionExpire = True
                        'ThreadReceive_Currency.Abort()
                        Application.Exit()
                    End If

                    If Currfutall.Contains(token_no) Then
                        'Dim fltppr As Double
                        If Currfltpprice.Contains(token_no) Then
                            Currfbuyprice.Item(token_no) = buy_price
                            Currfsaleprice.Item(token_no) = sale_price
                            Currfltpprice.Item(token_no) = last_trade_price
                        Else
                            Currfbuyprice.Add(token_no, buy_price)
                            Currfsaleprice.Add(token_no, sale_price)
                            Currfltpprice.Add(token_no, last_trade_price)
                        End If
                    Else
                        If Currltpprice.Contains(token_no) Then
                            Currbuyprice.Item(token_no) = buy_price
                            Currsaleprice.Item(token_no) = sale_price
                            Currltpprice.Item(token_no) = last_trade_price
                        Else
                            Currbuyprice.Add(token_no, buy_price)
                            Currsaleprice.Add(token_no, sale_price)
                            Currltpprice.Add(token_no, last_trade_price)
                        End If
                    End If
                    '=====================
                End If
            Catch ex As Threading.ThreadAbortException
                Threading.Thread.ResetAbort()
            End Try
        End While
        Dr.Close()
        Dr.Dispose()
        'Return False
        'Else
        'Return False
        'End If
    End Sub
    ''' <summary>
    ''' process_Currency_Net
    ''' </summary>
    ''' <remarks>This method call to receiving SqlDB</remarks>
    Private Sub process_Currency_Net()
        Dim token_no As Long
        Dim buy_price As Double
        Dim sale_price As Double
        Dim last_trade_price As Double

        'ED_Currency = New clsEnDe
        Dim Cmd As SqlClient.SqlCommand
        Dim Dr As SqlClient.SqlDataReader
        'Try
        'Dr = DAL.DA_SQL.FillListCur("dbo.Sp_006 '" & gVarInstanceID & "'") 'Objsql.SelectCurToken()
        If DAL.DA_SQL._conCur.State = ConnectionState.Closed Then DAL.DA_SQL.open_cur_connection()
        Cmd = New SqlClient.SqlCommand("dbo.Sp_006 '" & gVarInstanceID & "'", DAL.DA_SQL._conCur)
        Dr = Cmd.ExecuteReader

        'Catch ex As Exception
        '    MsgBox("Error")
        'End Try
        While Dr.Read()
            Try
                'token_no = Val(Dr("token_no") & "")
                'buy_price = Val(Dr("bprice") & "")
                'sale_price = Val(Dr("sprice") & "")
                'last_trade_price = Val(Dr("ltp") & "")
                token_no = Val(Dr("F1") & "")
                buy_price = ED.DCur(Dr("F2"))
                sale_price = ED.DCur(Dr("F3"))
                last_trade_price = ED.DCur(Dr("F4"))

                If VarCurBCurrentDate < Val(ED.DCur(Dr("F38")) & "") Then
                    VarCurBCurrentDate = Val(ED.DCur(Dr("F38")) & "")
                End If


                If VarCurBCurrentDate >= G_VarExpiryDate1 Then
                    'ThreadReceive_fo.Abort()
                    'ThreadReceive_cm.Abort()
                    'MsgBox("Please Contact Vendor, Trial Version Expired.", MsgBoxStyle.Exclamation)
                    'Call SplashScreen1.Sub_Get_Version_TextFile()
                    ''Application.Exit()
                    'End
                    'Exit Sub
                    IsVersionExpire = True
                    'ThreadReceive_Currency.Abort()
                    Application.Exit()
                End If

                If Currfutall.Contains(token_no) Then
                    'Dim fltppr As Double
                    If Currfltpprice.Contains(token_no) Then
                        Currfbuyprice.Item(token_no) = buy_price
                        Currfsaleprice.Item(token_no) = sale_price
                        Currfltpprice.Item(token_no) = last_trade_price
                    Else
                        Currfbuyprice.Add(token_no, buy_price)
                        Currfsaleprice.Add(token_no, sale_price)
                        Currfltpprice.Add(token_no, last_trade_price)
                    End If
                Else
                    If Currltpprice.Contains(token_no) Then
                        Currbuyprice.Item(token_no) = buy_price
                        Currsaleprice.Item(token_no) = sale_price
                        Currltpprice.Item(token_no) = last_trade_price
                    Else
                        Currbuyprice.Add(token_no, buy_price)
                        Currsaleprice.Add(token_no, sale_price)
                        Currltpprice.Add(token_no, last_trade_price)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
                Threading.Thread.ResetAbort()
            End Try
        End While
        Dr.Close()
        Dr.Dispose()
        'End If
    End Sub

    'Public Function decTable(ByVal entable As DataTable) As DataTable
    '    Dim dt As New DataTable
    '    dt = entable.Clone
    '    Dim dr As DataRow
    '    Try


    '        For Each edr As DataRow In entable.Rows
    '            dr = dt.NewRow

    '            For Each edc As DataColumn In entable.Columns
    '                ''If edc.ColumnName = "Id" Then
    '                'If edc.ColumnName = "F1" Then
    '                '    dr(edc.ColumnName) = edr(edc.ColumnName)
    '                '    'ElseIf edc.ColumnName.ToUpper = "ALLOWED" Or edc.ColumnName.ToUpper = "LIMITED" Then
    '                'ElseIf edc.ColumnName.ToUpper = "F7" Or edc.ColumnName.ToUpper = "F8" Then
    '                '    Try
    '                '        dr(edc.ColumnName) = CBool(clsUEnDe.FDec(edr(edc.ColumnName).ToString))
    '                '    Catch ex As Exception
    '                '        dr(edc.ColumnName) = CBool("FALSE")
    '                '    End Try

    '                'Else
    '                '    dr(edc.ColumnName) = clsUEnDe.FDec(edr(edc.ColumnName).ToString)
    '                'End If
    '                'If edc.ColumnName = "Id" Then
    '                If edc.ColumnName = "F1" Then
    '                    dr(edc.ColumnName) = edr(edc.ColumnName)
    '                    'ElseIf edc.ColumnName.ToUpper = "ALLOWED" Or edc.ColumnName.ToUpper = "LIMITED" Then
    '                ElseIf edc.ColumnName.ToUpper = "F7" Or edc.ColumnName.ToUpper = "F8" Then
    '                    Try
    '                        dr(edc.ColumnName) = CBool(clsUEnDe.FDec(edr(edc.ColumnName).ToString))
    '                    Catch ex As Exception
    '                        dr(edc.ColumnName) = CBool("FALSE")
    '                    End Try
    '                ElseIf edc.ColumnName.ToUpper = "TCP" Then
    '                    dr(edc.ColumnName.ToUpper) = CBool(edr(edc.ColumnName).ToString)
    '                ElseIf edc.ColumnName.ToUpper = "PreOTP".ToUpper Then
    '                    dr(edc.ColumnName.ToUpper) = edr(edc.ColumnName).ToString
    '                ElseIf edc.ColumnName.ToUpper = "SupportId".ToUpper Then
    '                    dr(edc.ColumnName.ToUpper) = edr(edc.ColumnName).ToString
    '                Else
    '                    dr(edc.ColumnName) = clsUEnDe.FDec(edr(edc.ColumnName).ToString)
    '                End If

    '            Next
    '            dt.Rows.Add(dr)
    '        Next
    '    Catch ex As Exception

    '    End Try
    '    Return dt
    'End Function

    Public Function decTable(ByVal entable As DataTable) As DataTable
        Dim dt As New DataTable
        dt = entable.Clone
        Dim dr As DataRow
        Try


            For Each edr As DataRow In entable.Rows
                dr = dt.NewRow

                For Each edc As DataColumn In entable.Columns

                    If edc.ColumnName = "F1" Then
                        dr(edc.ColumnName) = edr(edc.ColumnName)
                        'ElseIf edc.ColumnName.ToUpper = "ALLOWED" Or edc.ColumnName.ToUpper = "LIMITED" Then
                    ElseIf edc.ColumnName.ToUpper = "F7" Or edc.ColumnName.ToUpper = "F8" Then
                        Try
                            dr(edc.ColumnName) = CBool(clsUEnDe.FDec(edr(edc.ColumnName).ToString))
                        Catch ex As Exception
                            dr(edc.ColumnName) = CBool("FALSE")
                        End Try
                    ElseIf edc.ColumnName.ToUpper = "TCP" Then
                        dr(edc.ColumnName.ToUpper) = CBool(edr(edc.ColumnName).ToString)
                    ElseIf edc.ColumnName.ToUpper = "PreOTP".ToUpper Then
                        dr(edc.ColumnName.ToUpper) = edr(edc.ColumnName).ToString
                    ElseIf edc.ColumnName.ToUpper = "SupportId".ToUpper Then
                        dr(edc.ColumnName.ToUpper) = edr(edc.ColumnName).ToString
                    ElseIf edc.ColumnName.ToUpper = "LastLogin".ToUpper Then

                    ElseIf edc.ColumnName.ToUpper = "LoginId".ToUpper Then

                    Else
                        dr(edc.ColumnName) = clsUEnDe.FDec(edr(edc.ColumnName).ToString)
                    End If

                Next
                dt.Rows.Add(dr)
            Next
        Catch ex As Exception

        End Try
        Return dt
    End Function

    Public Sub WriteLogbastlog_ltp(ByVal str As String)
        Try
            If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
            End If
            'FSErrorLogFile = New StreamWriter("C:\ErrorLog.txt", True)
            ' FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\ErrorLog.txt", True)
            Dim FSPrcTikLogFile As IO.StreamWriter
            FSPrcTikLogFile = New IO.StreamWriter(Application.StartupPath & "\VolhedgeLog\VolhedgeLog_LTP.txt", True)
            FSPrcTikLogFile.WriteLine(Date.Now.ToString & "|" & GVar_LogIn_User & "|" & str)
            FSPrcTikLogFile.WriteLine(Date.Now.ToString & "|" & GVar_LogIn_User & "|" & Strings.StrDup(str.Length, "-"))
            FSPrcTikLogFile.Close()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub WriteLogbast(ByVal str As String)
        Try
            If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
            End If
            'FSErrorLogFile = New StreamWriter("C:\ErrorLog.txt", True)
            ' FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\ErrorLog.txt", True)
            Dim FSPrcTikLogFile As IO.StreamWriter
            FSPrcTikLogFile = New IO.StreamWriter(Application.StartupPath & "\VolhedgeLog\InternetbcastLog.txt", True)
            FSPrcTikLogFile.WriteLine(Date.Now.ToString & "|" & GVar_LogIn_User & "|" & str)
            FSPrcTikLogFile.WriteLine(Date.Now.ToString & "|" & GVar_LogIn_User & "|" & Strings.StrDup(str.Length, "-"))
            FSPrcTikLogFile.Close()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub WriteLog(ByVal str As String)
        Try
            If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
            End If
            'FSErrorLogFile = New StreamWriter("C:\ErrorLog.txt", True)
            ' FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\ErrorLog.txt", True)
            Dim FSPrcTikLogFile As IO.StreamWriter
            FSPrcTikLogFile = New IO.StreamWriter(Application.StartupPath & "\VolhedgeLog\InternetbcastLog.txt", True)
            FSPrcTikLogFile.WriteLine(Date.Now.ToString & "|" & GVar_LogIn_User & "|" & str)
            FSPrcTikLogFile.WriteLine(Date.Now.ToString & "|" & GVar_LogIn_User & "|" & Strings.StrDup(str.Length, "-"))
            FSPrcTikLogFile.Close()
        Catch ex As Exception

        End Try

    End Sub

    Public Sub WriteLogMarketwatchlog(ByVal str As String)
        Try
            If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
            End If
            'FSErrorLogFile = New StreamWriter("C:\ErrorLog.txt", True)
            ' FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\ErrorLog.txt", True)
            Dim FSPrcTikLogFile As IO.StreamWriter
            FSPrcTikLogFile = New IO.StreamWriter(Application.StartupPath & "\VolhedgeLog\MarketwatchLog.txt", True)
            FSPrcTikLogFile.WriteLine(Date.Now.ToString & "|" & GVar_LogIn_User & "|" & str)
            FSPrcTikLogFile.WriteLine(Date.Now.ToString & "|" & GVar_LogIn_User & "|" & Strings.StrDup(str.Length, "-"))
            FSPrcTikLogFile.Close()
        Catch ex As Exception

        End Try

    End Sub

    Public Sub WriteLogSynthFutlog(ByVal str As String)
        Try
            If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
            End If
            'FSErrorLogFile = New StreamWriter("C:\ErrorLog.txt", True)
            ' FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\ErrorLog.txt", True)
            Dim FSPrcTikLogFile As IO.StreamWriter
            FSPrcTikLogFile = New IO.StreamWriter(Application.StartupPath & "\VolhedgeLog\SynthFutLog.txt", True)
            FSPrcTikLogFile.WriteLine(Date.Now.ToString & "|" & GVar_LogIn_User & "|" & str)
            'FSPrcTikLogFile.WriteLine(Date.Now.ToString & "|" & GVar_LogIn_User & "|" & Strings.StrDup(str.Length, "-"))
            FSPrcTikLogFile.Close()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub WriteLogMW(ByVal str As String)
        Try
            'If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
            '    Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
            'End If
            ''FSErrorLogFile = New StreamWriter("C:\ErrorLog.txt", True)
            '' FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\ErrorLog.txt", True)
            'Dim FSPrcTikLogFile As IO.StreamWriter
            'FSPrcTikLogFile = New IO.StreamWriter(Application.StartupPath & "\VolhedgeLog\SynthFut.txt", True)
            'FSPrcTikLogFile.WriteLine(Date.Now.ToString & "|" & GVar_LogIn_User & "|" & str)
            'FSPrcTikLogFile.WriteLine(Date.Now.ToString & "|" & GVar_LogIn_User & "|" & Strings.StrDup(str.Length, "-"))
            'FSPrcTikLogFile.Close()
        Catch ex As Exception

        End Try

    End Sub

End Module
