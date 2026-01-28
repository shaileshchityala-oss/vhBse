
Imports System.Management
Imports System.IO
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Module BCastModule
    Dim endpoint1 As IPEndPoint
    Dim temp As Integer = 0
    Public nseindia As String = "nseindia.com"
    Public bool_IsServerConnected As Boolean
    Public bool_IsTelNet As Boolean
    Public G_bool_IsAuthanticated As Boolean = False

    Public G_ServerIsOn As Boolean = False
    Public G_BCastFoIsOn As Boolean = False
    Public G_BCastCmIsOn As Boolean = False
    Public G_BCastCurrIsOn As Boolean = False
    Public G_BCastSqlFoIsOn As Boolean = False
    Public G_BCastSqlCmIsOn As Boolean = False
    Public G_BCastSqlCurrIsOn As Boolean = False
    Public G_BCastNetFoIsOn As Boolean = False
    Public G_BCastNetCmIsOn As Boolean = False
    Public G_BCastNetCurrIsOn As Boolean = False

    Public GFO_UDP_IP As String
    Public GCM_UDP_IP As String
    Public GCurr_UDP_IP As String
    Public GFO_UDP_Port As String
    Public GCM_UDP_Port As String
    Public GCurr_UDP_Port As String

    Public multicastListener_fo As Socket
    Public multicastListener_cm As Socket
    Public multicastListener_cm_MTS As Socket
    Public multicastListener_Currency As Socket
    'Public multicastListener_fo1 As UdpClient
    'Public multicastListener_cm1As UdpClient
    'Public multicastListener_Currency1 As UdpClient
    'Public foflag As Integer = 0
    'Public cmflag As Integer = 0
    'Public currflag As Integer = 0

    Public ThreadReceive_fo As System.Threading.Thread
    Public ThreadReceive_cm As System.Threading.Thread
    Public ThreadReceive_cm_MTS As System.Threading.Thread
    Public ThreadReceive_Currency As System.Threading.Thread

    Dim lzo_fo As New decompress.algorithm()
    Dim lzo_cm As New decompress.algorithm()
    Dim lzo_Currency As New decompress.algorithm()

    'MsgType = "H"
    '{
    'Dim MessageType As String 'CHAR MessageType
    Dim ReportDate As Long 'LONG ReportDate
    Dim UserType As Integer 'SHORT UserType
    Dim BrokerId As String 'CHAR BrokerId [5]
    Dim FirmName As String 'CHAR FirmName [25]
    Dim BrokerName As String 'CHAR FirmName [25]
    Dim TraderNumber As Long 'LONG TraderNumber
    Dim TraderName As String ' CHAR TraderName [26]
    '}

    Public bIsfoBcopyComplete As Boolean = False
    Public bIscmBcopyComplete As Boolean = False
    Public bIscurBcopyComplete As Boolean = False


    'Public dtBCopy As New DataTable
    'Public dtfoBCopy As New DataTable
    'Public dtcmBCopy As New DataTable
    'Public dtcurBCopy As New DataTable

    Dim ifoBcopyRec As Integer = 0
    Dim icmBcopyRec As Integer = 0
    Dim icurBcopyRec As Integer = 0



    ''' <summary>
    ''' initialize_fo_broadcast
    ''' </summary>
    ''' <remarks>This method call to initialize FO Socket and FO Receiving Thread start </remarks>
    Public Sub initialize_fo_broadcast()
        Try
            multicastListener_fo = New Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
            multicastListener_fo.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1)
            multicastListener_fo.Bind(New IPEndPoint(IPAddress.Any, GdtSettings.Compute("max(SettingKey)", "SettingName='FO_UDP_Port'").ToString))
            multicastListener_fo.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(IPAddress.Parse(GdtSettings.Compute("max(SettingKey)", "SettingName='FO_UDP_IP'").ToString), IPAddress.Any))


            ThreadReceive_fo = New System.Threading.Thread(AddressOf ReceiveMessages_fo)
            ThreadReceive_fo.Name = "thr_Rec_fo"
            ThreadReceive_fo.Start()

        Catch x As Exception
            MsgBox(x.ToString)
        End Try

    End Sub




    ''' <summary>
    ''' initialize_cm_broadcast
    ''' </summary>
    ''' <remarks>This method call to initialize CM Socket and CM Receiving Thread start </remarks>
    Public Sub initialize_cm_broadcast()
        Try

            'Dim port1 As Integer = GdtSettings.Compute("max(SettingKey)", "SettingName='CM_UDP_Port'").ToString
            'Dim multicastAddress1 As IPAddress = IPAddress.Parse(GdtSettings.Compute("max(SettingKey)", "SettingName='CM_UDP_IP'").ToString)

            'Dim endPoint = New System.Net.IPEndPoint(0, port1)
            'multicastListener_cm = New UdpClient()
            'multicastListener_cm.ExclusiveAddressUse = False
            'multicastListener_cm.Client.SetSocketOption(Net.Sockets.SocketOptionLevel.Socket, Net.Sockets.SocketOptionName.ReuseAddress, True)
            'multicastListener_cm.Client.Bind(endPoint)


            'multicastListener_cm = New UdpClient(New IPEndPoint(IPAddress.Any, port))

            ' join multicast group on all available network interfaces
            'Dim networkInterfaces() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()

            'Dim NI As NetworkInterface

            'For Each NI In networkInterfaces
            '    If (Not NI.Supports(NetworkInterfaceComponent.IPv4)) Then
            '        Continue For
            '    End If

            '    Dim adapterProperties As IPInterfaceProperties = NI.GetIPProperties()
            '    Dim unicastIPAddresses As UnicastIPAddressInformationCollection = adapterProperties.UnicastAddresses
            '    Dim ipAddress As IPAddress = Nothing
            '    Dim unicastIPAddress As UnicastIPAddressInformation
            '    For Each unicastIPAddress In unicastIPAddresses
            '        If unicastIPAddress.Address.AddressFamily <> AddressFamily.InterNetwork Then
            '            Continue For
            '        End If

            '        ipAddress = unicastIPAddress.Address
            '        Exit For
            '    Next

            '    If ipAddress Is Nothing Then
            '        Continue For
            '    End If

            '    multicastListener_cm.JoinMulticastGroup(multicastAddress1, ipAddress)

            '    Dim sendClient As UdpClient = New UdpClient(New IPEndPoint(ipAddress, port))
            '    sendClients.Add(sendClient)
            'Next






            multicastListener_cm = New Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
            multicastListener_cm.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1)
            multicastListener_cm.Bind(New IPEndPoint(IPAddress.Any, GdtSettings.Compute("max(SettingKey)", "SettingName='CM_UDP_Port'").ToString))
            multicastListener_cm.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(IPAddress.Parse(GdtSettings.Compute("max(SettingKey)", "SettingName='CM_UDP_IP'").ToString), IPAddress.Any))

            ThreadReceive_cm = New System.Threading.Thread(AddressOf ReceiveMessages_cm)
            ThreadReceive_cm.Name = "thr_Rec_CM"
            ThreadReceive_cm.Start()


        Catch x As Exception
            'Console.WriteLine(x.Message)
            MsgBox(x.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' initialize_Currency_broadcast
    ''' </summary>
    ''' <remarks>This method call to initialize Currency Socket and Currency Receiving Thread start </remarks>
    Public Sub initialize_Currency_broadcast()
        Try

            'Dim port2 As Integer = GdtSettings.Compute("max(SettingKey)", "SettingName='Currency_UDP_Port'").ToString
            'Dim multicastAddress2 As IPAddress = IPAddress.Parse(GdtSettings.Compute("max(SettingKey)", "SettingName='Currency_UDP_IP'").ToString)



            'Dim endPoint = New System.Net.IPEndPoint(0, port2)
            'multicastListener_Currency = New UdpClient()
            'multicastListener_Currency.ExclusiveAddressUse = False
            'multicastListener_Currency.Client.SetSocketOption(Net.Sockets.SocketOptionLevel.Socket, Net.Sockets.SocketOptionName.ReuseAddress, True)
            'multicastListener_Currency.Client.Bind(endPoint)

            ''multicastListener_Currency = New UdpClient(New IPEndPoint(IPAddress.Any, port))

            '' join multicast group on all available network interfaces
            'Dim networkInterfaces() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()

            'Dim NI As NetworkInterface

            'For Each NI In networkInterfaces
            '    If (Not NI.Supports(NetworkInterfaceComponent.IPv4)) Then
            '        Continue For
            '    End If

            '    Dim adapterProperties As IPInterfaceProperties = NI.GetIPProperties()
            '    Dim unicastIPAddresses As UnicastIPAddressInformationCollection = adapterProperties.UnicastAddresses
            '    Dim ipAddress As IPAddress = Nothing
            '    Dim unicastIPAddress As UnicastIPAddressInformation
            '    For Each unicastIPAddress In unicastIPAddresses
            '        If unicastIPAddress.Address.AddressFamily <> AddressFamily.InterNetwork Then
            '            Continue For
            '        End If

            '        ipAddress = unicastIPAddress.Address
            '        Exit For
            '    Next

            '    If ipAddress Is Nothing Then
            '        Continue For
            '    End If

            '    multicastListener_Currency.JoinMulticastGroup(multicastAddress2, ipAddress)

            '    'Dim sendClient As UdpClient = New UdpClient(New IPEndPoint(ipAddress, port))
            '    'sendClients.Add(sendClient)
            'Next

            multicastListener_Currency = New Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
            multicastListener_Currency.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1)
            multicastListener_Currency.Bind(New IPEndPoint(IPAddress.Any, GdtSettings.Compute("max(SettingKey)", "SettingName='Currency_UDP_Port'").ToString))
            multicastListener_Currency.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(IPAddress.Parse(GdtSettings.Compute("max(SettingKey)", "SettingName='Currency_UDP_IP'").ToString), IPAddress.Any))

            ThreadReceive_Currency = New System.Threading.Thread(AddressOf ReceiveMessages_Currency)
            ThreadReceive_Currency.Name = "thr_Rec_Curr"
            ThreadReceive_Currency.Start()
        Catch x As Exception
            MsgBox(x.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' ReceiveMessages_fo
    ''' </summary>
    ''' <remarks>This method call to Receive FO broadcast and store LTP price into hashtable</remarks>
    Public Sub ReceiveMessages_fo()

        Try
            'Dim port As Integer = GdtSettings.Compute("max(SettingKey)", "SettingName='FO_UDP_Port'").ToString
            'Dim multicastAddress As IPAddress = IPAddress.Parse(GdtSettings.Compute("max(SettingKey)", "SettingName='FO_UDP_IP'").ToString)
            ''   multicastListener_fo = New UdpClient(New IPEndPoint(IPAddress.Any, port))

            ''Dim client = New UdpClient(New IPEndPoint(IPAddress.Any, port))
            'While True
            '    Dim bteReceiveData_fo(511) As Byte
            '    bteReceiveData_fo = multicastListener_fo.Receive(New IPEndPoint(multicastAddress, port))
            '    Call process_fo_data(bteReceiveData_fo)
            '    'Threading.Thread.Sleep(2000)
            '    NewInitialize_fo()
            'End While


            While True

                Dim bteReceiveData_fo(511) As Byte
                If multicastListener_fo Is Nothing Then
                    Continue While
                End If
                multicastListener_fo.Receive(bteReceiveData_fo)
                '  multicastListener_fo.ReceiveFrom(bteReceiveData_fo, GroupEP)
                'If thrworking = True Then
                'noPackage += 1
                '  process_fo_data(bteReceiveData_fo)

                process_fo_data(bteReceiveData_fo)
                'End If
                'lblmcount.Invoke(mtest)
            End While


        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        Catch ex1 As Exception
            'MsgBox(ex1.ToString)
            NewInitialize_fo()
        End Try
    End Sub
    Public Function CheckUDPfo_Connection(ByVal flagfo As Boolean) As Boolean
        Try
            multicastListener_fo = New Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
            multicastListener_fo.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1)
            multicastListener_fo.Bind(New IPEndPoint(IPAddress.Any, GdtSettings.Compute("max(SettingKey)", "SettingName='FO_UDP_Port'").ToString))
            multicastListener_fo.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(IPAddress.Parse(GdtSettings.Compute("max(SettingKey)", "SettingName='FO_UDP_IP'").ToString), IPAddress.Any))



            Dim bteReceiveData_fo(511) As Byte
            multicastListener_fo.ReceiveTimeout = 1000
            multicastListener_fo.Receive(bteReceiveData_fo)
            If bteReceiveData_fo.Length > 0 Then
                ' MsgBox("Test Connection Succeeded  ", MsgBoxStyle.Information)
                flagfo = True
                Return flagfo
            Else
                '    MsgBox("Test Connection Fail !! ", MsgBoxStyle.Critical)
                flagfo = False
                Return flagfo
            End If
        Catch ex As Exception
            flagfo = False
            Return flagfo
        End Try
    End Function
    Public Function CheckUDPcm_Connection(ByVal flagcm As Boolean) As Boolean
        Try
            multicastListener_cm = New Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
            multicastListener_cm.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1)
            multicastListener_cm.Bind(New IPEndPoint(IPAddress.Any, GdtSettings.Compute("max(SettingKey)", "SettingName='CM_UDP_Port'").ToString))
            multicastListener_cm.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(IPAddress.Parse(GdtSettings.Compute("max(SettingKey)", "SettingName='CM_UDP_IP'").ToString), IPAddress.Any))



            Dim bteReceiveData_cm(511) As Byte
            multicastListener_cm.ReceiveTimeout = 1000
            multicastListener_cm.Receive(bteReceiveData_cm)
            If bteReceiveData_cm.Length > 0 Then
                ' MsgBox("Test Connection Succeeded  ", MsgBoxStyle.Information)
                flagcm = True
                Return flagcm
            Else
                '    MsgBox("Test Connection Fail !! ", MsgBoxStyle.Critical)
                flagcm = False
                Return flagcm
            End If
        Catch ex As Exception
            flagcm = False
            Return flagcm
        End Try
    End Function
    Public Function CheckUDPcurr_Connection(ByVal flagcurr As Boolean) As Boolean
        Try

            multicastListener_Currency = New Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
            multicastListener_Currency.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1)
            multicastListener_Currency.Bind(New IPEndPoint(IPAddress.Any, GdtSettings.Compute("max(SettingKey)", "SettingName='Currency_UDP_Port'").ToString))
            multicastListener_Currency.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(IPAddress.Parse(GdtSettings.Compute("max(SettingKey)", "SettingName='Currency_UDP_IP'").ToString), IPAddress.Any))


            Dim bteReceiveData_Currency(511) As Byte
            multicastListener_Currency.ReceiveTimeout = 1000
            multicastListener_Currency.Receive(bteReceiveData_Currency)
            If bteReceiveData_Currency.Length > 0 Then
                ' MsgBox("Test Connection Succeeded  ", MsgBoxStyle.Information)
                flagcurr = True
                Return flagcurr
            Else
                '    MsgBox("Test Connection Fail !! ", MsgBoxStyle.Critical)
                flagcurr = False
                Return flagcurr
            End If
        Catch ex As Exception
            flagcurr = False
            Return flagcurr
        End Try
    End Function

    ''' <summary>
    ''' ReceiveMessages_cm
    ''' </summary>
    ''' <remarks>This method call to Receive CM broadcast and store LTP price into hashtable</remarks>
    Public Sub ReceiveMessages_cm()
        Try

            'Dim port1 As Integer = GdtSettings.Compute("max(SettingKey)", "SettingName='CM_UDP_Port'").ToString
            'Dim multicastAddress1 As IPAddress = IPAddress.Parse(GdtSettings.Compute("max(SettingKey)", "SettingName='CM_UDP_IP'").ToString)
            '' multicastListener_fo = New UdpClient(New IPEndPoint(IPAddress.Any, port))

            ''Dim client = New UdpClient(New IPEndPoint(IPAddress.Any, port))
            'While True
            '    Dim bteReceiveData_cm(511) As Byte
            '    bteReceiveData_cm = multicastListener_cm.Receive(New IPEndPoint(multicastAddress1, port1))
            '    Call process_cm_data(bteReceiveData_cm)
            '    'Threading.Thread.Sleep(2000)
            '    NewInitialize_cm()
            'End While


            While True

                Dim bteReceiveData_cm(511) As Byte
                If multicastListener_cm IsNot Nothing Then
                    multicastListener_cm.Receive(bteReceiveData_cm)
                End If

                'multicastListener_cm.Receive(bteReceiveData_cm)
                'If thrworking = True Then
                '  If NEW_CM_BROADCAST_MT = 1 Then

                '  process_cm_7208_MTS(bteReceiveData_cm)
                ' Else
                process_cm_data(bteReceiveData_cm)
                '  End If

                'End If
            End While



        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        Catch ex1 As Exception
            'MsgBox(ex1.ToString)
            NewInitialize_cm()
        End Try
    End Sub
    ''' <summary>
    ''' ReceiveMessages_Currency
    ''' </summary>
    ''' <remarks>This method call to Receive Currency broadcast and store LTP price into hashtable</remarks>
    Public Sub ReceiveMessages_Currency()
        ' Try

        'Dim port2 As Integer = GdtSettings.Compute("max(SettingKey)", "SettingName='Currency_UDP_Port'").ToString
        'Dim multicastAddress2 As IPAddress = IPAddress.Parse(GdtSettings.Compute("max(SettingKey)", "SettingName='Currency_UDP_IP'").ToString)


        ''Dim client = New UdpClient(New IPEndPoint(IPAddress.Any, port))
        'While True
        '    Dim bteReceiveData_Currency(511) As Byte
        '    bteReceiveData_Currency = multicastListener_Currency.Receive(New IPEndPoint(multicastAddress2, port2))
        '    Call process_cm_data(bteReceiveData_Currency)
        '    'Threading.Thread.Sleep(2000)
        '    NewInitialize_Currency()
        'End While

        While True



            Dim bteReceiveData_Currency(511) As Byte
            multicastListener_Currency.Receive(bteReceiveData_Currency)
            'If thrworking = True Then
            'noPackage += 1
            Call process_Currency_data(bteReceiveData_Currency)
            'End If
            'lblmcount.Invoke(mtest)
        End While

        ' Catch ex As Threading.ThreadAbortException
        '  Threading.Thread.ResetAbort()
        ' Catch ex1 As Exception
        '   ThreadReceive_Currency.Abort()
        ' Threading.Thread.ResetAbort()
        'MsgBox("Analysis :: ReceiveMessages_Currency() ::" & ex1.ToString)
        ' NewInitialize_Currency()
        ' End Try
    End Sub
    ''' <summary>
    ''' NewInitialize_fo
    ''' </summary>
    ''' <remarks>This method call to Init. FO thread</remarks>
    Public Sub NewInitialize_fo()
        Try

            ThreadReceive_fo = New System.Threading.Thread(AddressOf ReceiveMessages_fo)
            ThreadReceive_fo.Name = "thr_ReRec_Fo"
            ThreadReceive_fo.Start()
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub
    ''' <summary>
    ''' NewInitialize_cm
    ''' </summary>
    ''' <remarks>This method call to Init. CM thread</remarks>
    Public Sub NewInitialize_cm()
        Try
            ThreadReceive_cm = New System.Threading.Thread(AddressOf ReceiveMessages_cm)
            ThreadReceive_cm.Name = "thr_ReRec_CM"
            ThreadReceive_cm.Start()
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub
    ''' <summary>
    ''' NewInitialize_Currency
    ''' </summary>
    ''' <remarks>This method call to Init. currency thread</remarks>
    Public Sub NewInitialize_Currency()
        Try
            ThreadReceive_Currency = New System.Threading.Thread(AddressOf ReceiveMessages_Currency)
            ThreadReceive_Currency.Name = "thr_ReRec_Curr"
            ThreadReceive_Currency.Start()
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub

    ''' <summary>
    ''' process_fo_6501
    ''' </summary>
    ''' <param name="data"></param>
    ''' <remarks>This method call to decompress FO receiving byte</remarks>
    Private Sub process_fo_6501(ByVal data() As Byte)

        Dim i_Msg_Length As Integer = 0
        Dim sSymbol As String
        Dim lAlertLimit As Long
        Dim iCurrPos As Integer = 0
        Dim iCount As Integer = 0
        Dim ActionCode As String '3
        Dim cmp_data As String
        Dim j As Integer


        i_Msg_Length = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 63 + j))
        ActionCode = HxTOStr(System.BitConverter.ToString(data, (95) + j, 512 - 95))
        '//read string from 92 upto message lengh
        cmp_data = HxTOStr(System.BitConverter.ToString(data, (63) + j, 512 - 96))

        Dim strData As String
        Dim csDelimeter As String = " "
        strData = cmp_data

        If ActionCode = "SYS" Then
            strData = "Sys:" & strData
        ElseIf ActionCode.Substring(0, 3) = "MWL" Then '//MWL
            '//if(strData.Left(6) == "MARKET")
            While (-1 <> (iCurrPos = strData.LastIndexOf(csDelimeter)))
                Select Case iCount
                    Case 4
                        sSymbol = strData.Substring(0, iCurrPos)
                    Case 13
                        lAlertLimit = Val(strData.Substring(0, iCurrPos))
                End Select
                strData = Microsoft.VisualBasic.Right(strData, (strData.Length - iCurrPos - csDelimeter.Length))
                iCount = iCount + 1
            End While
        End If

        '//csSymbol = "NIFTY     ";
        '//	
        '//	lAlertLimit = 20;

        ' ''      If (sSymbol <> "") Then
        ' ''	for(int i=0;i<10;i++)
        ' ''		st_mkt_pos.cSymbol[i] = ' ';

        ' ''	for(int i=0;i<csSymbol.GetLength();i++)
        ' ''		st_mkt_pos.cSymbol[i] = csSymbol[i];

        ' ''	if(pApp->cmap_marketpositionLimit_info.PLookup(CString(st_mkt_pos.cSymbol,10)) != NULL) then

        ' ''		st_mkt_pos = pApp->cmap_marketpositionLimit_info[CString(st_mkt_pos.cSymbol,10)];
        ' ''		stTempdata = new ST_MARKET_POSITON_LIMIT;
        ' ''		memcpy(stTempdata,&st_mkt_pos,sizeof(ST_MARKET_POSITON_LIMIT));

        ' ''		if(lAlertLimit >= stTempdata->lAlgoStopLimit) then

        ' ''			st_mkt_pos.sTriggered = 1;
        ' ''			pApp->cmap_marketpositionLimit_info[CString(st_mkt_pos.cSymbol,10)] = st_mkt_pos;

        ' ''			//SMessage *sMessage = new SMessage;
        ' ''			//__time64_t ltime;
        ' ''			//_time64( &ltime ); 

        ' ''			//sMessage->Time = ltime;
        ' ''			//sMessage->Server = "Algorithm";
        ' ''			//sMessage->Message = "MWPL exceeded for Symbol: " + csSymbol;
        ' ''			//pCoreEngine->UpdateMessageList(sMessage);				
        ' ''			//delete sMessage;

        ' ''			//IV Spread
        ' ''			//if((pCoreEngine->bIVSpread_Algo == true) && (pCoreEngine->bF_F_IOC_IL == true) && (pCoreEngine->bF_F_IOC_MWPL == true))	
        ' ''			//	::PostThreadMessage(pApp->m_IVSpreadThreadId,WM_MWPL_ALERT,(WPARAM)stTempdata,(LPARAM)0);

        ' ''			//IV
        ' ''			//if((pCoreEngine->bIV_Algo == true) && (pCoreEngine->bF_F_IOC_IL == true) && (pCoreEngine->bF_F_IOC_MWPL == true))	
        ' ''				//::PostThreadMessage(pApp->m_IVThreadId,WM_MWPL_ALERT,(WPARAM)stTempdata,(LPARAM)0);

        ' ''		else if(lAlertLimit >= stTempdata->lAlertLimit)

        ' ''			CString strMsg;
        ' ''			strMsg = "Alert Limit for Symbol " + csSymbol.Trim();
        ' ''			pApp->MDIMessageBox(strMsg);
        ' ''                      End If

        ' ''		stTempdata = NULL;
        ' ''		delete stTempdata;
        ' ''	}

        ' ''CString sAlerLimit;
        ' ''sAlerLimit.Format("Symbol %s, Alert Limit: %d \r\n",csSymbol,lAlertLimit);
        ' ''CFile ftest;
        ' ''ftest.Open(theApp.AppFolderPath + "\\MWPLtest.txt",CFile::modeCreate|CFile::modeReadWrite|CFile::modeNoTruncate);
        ' ''ftest.SeekToEnd();
        ' ''ftest.Write(sAlerLimit, sAlerLimit.GetLength());
        ' ''ftest.Close();
        ' ''                  End If


    End Sub

    ''' <summary>
    ''' process_fo_1833
    ''' </summary>
    ''' <param name="data"></param>
    ''' <remarks>This method call to decompress FO receiving byte</remarks>
    'Private Sub process_fo_1833(ByVal data() As Byte)

    '    ''MsgType = "H"
    '    ''{
    '    ''Dim MessageType As String 'CHAR MessageType
    '    'Dim ReportDate As Long 'LONG ReportDate
    '    'Dim UserType As Integer 'SHORT UserType
    '    'Dim BrokerId As String 'CHAR BrokerId [5]
    '    'Dim FirmName As String 'CHAR FirmName [25]
    '    'Dim TraderNumber As Long 'LONG TraderNumber
    '    'Dim TraderName As String ' CHAR TraderName [26]
    '    ''}


    '    'MsgTyp = "R"
    '    'STRUCT(CONTRACT_DESC)
    '    '{
    '    Dim InstrumentName As String 'CHAR InstrumentName [6]
    '    Dim Symbol As String 'CHAR Symbol [10]
    '    Dim ExpiryDate As Long 'LONG ExpiryDate
    '    Dim StrikePrice As Long 'LONG StrikePrice
    '    Dim OptionType As String 'CHAR OptionType [2]
    '    Dim CALevel As Integer 'SHORT CALevel
    '    '}

    '    Dim MarketType As Integer 'SHORT MarketType
    '    Dim OpenPrice As Long 'LONG OpenPrice
    '    Dim HighPrice As Long 'LONG HighPrice
    '    Dim LowPrice As Long 'LONG LowPrice
    '    Dim ClosingPrice As Long 'LONG ClosingPrice
    '    Dim TotalQuantityTraded As Long 'LONG TotalQuantityTraded
    '    Dim TotalValueTraded As Double 'DOUBLE TotalValueTraded
    '    Dim PreviousClosePrice As Long 'LONG PreviousClosePrice
    '    Dim OpenInterest As Long 'LONG OpenInterest
    '    Dim ChgOpenInterest As Long 'LONG ChgOpenInterest
    '    Dim Indicator As String 'CHAR Indicator [4]



    '    'MsgTyp = "T"
    '    '{
    '    'CHAR MessageType
    '    Dim NumberOfPackets As Long 'LONG NumberOfPackets
    '    'Reserved 1 byte
    '    '}
    '    Dim MsgType As String = ""
    '    Dim NoRec As Integer = 0

    '    'Dim intt As Integer = 7
    '    'Dim inx() As String
    '    'Dim InxName As String = System.BitConverter.ToString(data, 48 + 6, 1)
    '    'inx = InxName.Split("-")
    '    'MsgType = ""
    '    'For h As Integer = 0 To inx.Length - 1
    '    '    MsgType = MsgType & Chr(System.Convert.ToInt32(inx(h), 16))
    '    'Next
    '    MsgType = HxTOStr(System.BitConverter.ToString(data, 48 + 6, 1))

    '    NoRec = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 49 + 6))

    '    If MsgType = "H" Then
    '        Dim j As Integer = 0
    '        'for J as Integer = 0 74 *5 Step 74 
    '        'For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 49 + 6)) - 1) * 67 Step 67
    '        Try
    '            'LONG ReportDate
    '            ReportDate = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (49 + 6) + j))
    '            'SHORT UserType
    '            UserType = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, (53 + 6) + j))
    '            'CHAR BrokerId [5]
    '            BrokerId = HxTOStr(System.BitConverter.ToString(data, (55 + 6) + j, 5))
    '            'CHAR FirmName [25]
    '            FirmName = HxTOStr(System.BitConverter.ToString(data, (60 + 6) + j, 25))
    '            'LONG TraderNumber
    '            TraderNumber = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (86 + 6) + j))
    '            'CHAR TraderName [26]
    '            TraderName = HxTOStr(System.BitConverter.ToString(data, (90 + 6) + j, 26))




    '        Catch ex As Threading.ThreadAbortException
    '            Threading.Thread.ResetAbort()
    '        Catch ex1 As Exception
    '            MsgBox(ex1.Message)
    '        End Try
    '        'Next
    '    ElseIf MsgType <> "H" And MsgType <> "T" Then
    '        'For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 49 + 6)) - 1) * 74 Step 74
    '        For j As Integer = 0 To 74 * 5 Step 74
    '            Try
    '                'token_no = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 50 + j))


    '                InstrumentName = HxTOStr(System.BitConverter.ToString(data, (52 + 6) + j, 6))
    '                Symbol = HxTOStr(System.BitConverter.ToString(data, (58 + 6) + j, 10))
    '                ExpiryDate = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (68 + 6) + j))
    '                StrikePrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (72 + 6) + j)) / 100
    '                OptionType = HxTOStr(System.BitConverter.ToString(data, (76 + 6) + j, 2))
    '                CALevel = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, (78 + 6) + j))

    '                MarketType = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, (80 + 6) + j))
    '                OpenPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (82 + 6) + j)) / 100
    '                HighPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (86 + 6) + j)) / 100
    '                LowPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (90 + 6) + j)) / 100
    '                ClosingPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (94 + 6) + j)) / 100
    '                TotalQuantityTraded = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (98 + 6) + j))

    '                'DOUBLE TotalValueTraded
    '                TotalValueTraded = System.BitConverter.Int64BitsToDouble(IPAddress.HostToNetworkOrder(System.BitConverter.ToInt64(data, (102 + 6) + j)))

    '                PreviousClosePrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (110 + 6) + j)) / 100
    '                OpenInterest = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (114 + 6) + j)) / 100
    '                ChgOpenInterest = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (118 + 6) + j)) / 100

    '                Indicator = HxTOStr(System.BitConverter.ToString(data, (122 + 6) + j, 4))



    '                'MsgBox("InstrumentName - " & InstrumentName & vbCrLf & _
    '                '       "Symbol  -  " & Symbol & vbCrLf & _
    '                '"ExpiryDate  -  " & ExpiryDate & vbCrLf & _
    '                '"StrikePrice  - " & StrikePrice & vbCrLf & _
    '                '"OptionType  -  " & OptionType & vbCrLf & _
    '                '"CALevel  -  " & CALevel & vbCrLf & _
    '                '"MarketType  -  " & MarketType & vbCrLf & _
    '                '"OpenPrice  -  " & OpenPrice & vbCrLf & _
    '                '"HighPrice  -  " & HighPrice & vbCrLf & _
    '                '"LowPrice  -  " & LowPrice & vbCrLf & _
    '                '"ClosingPrice  -  " & ClosingPrice & vbCrLf & _
    '                '"TotalQuantityTraded  -  " & TotalQuantityTraded & vbCrLf & _
    '                '"TotalValueTraded  -  " & TotalValueTraded & vbCrLf & _
    '                '"PreviousClosePrice  -  " & PreviousClosePrice & vbCrLf & _
    '                '"OpenInterest  -  " & OpenInterest & vbCrLf & _
    '                '"ChgOpenInterest  -  " & ChgOpenInterest & vbCrLf & _
    '                '"Indicator  -  " & Indicator)

    '                Dim Dr As DataRow = dtfoBCopy.NewRow
    '                Dr("InstrumentName") = InstrumentName.ToString
    '                Dr("Symbol") = Symbol.ToString
    '                Dr("ExpiryDate") = ExpiryDate.ToString
    '                Dr("StrikePrice") = StrikePrice.ToString
    '                Dr("OptionType") = OptionType.ToString
    '                Dr("CALevel") = CALevel.ToString
    '                Dr("MarketType") = MarketType.ToString
    '                Dr("OpenPrice") = OpenPrice.ToString
    '                Dr("HighPrice") = HighPrice.ToString
    '                Dr("LowPrice") = LowPrice.ToString
    '                Dr("ClosingPrice") = ClosingPrice.ToString
    '                Dr("TotalQuantityTraded") = TotalQuantityTraded.ToString
    '                Dr("TotalValueTraded") = TotalValueTraded.ToString
    '                Dr("PreviousClosePrice") = PreviousClosePrice.ToString
    '                Dr("OpenInterest") = OpenInterest.ToString
    '                Dr("ChgOpenInterest") = ChgOpenInterest.ToString
    '                Dr("Indicator") = Indicator.ToString
    '                Dr("BCAST") = "FO"
    '                Dr("MsgTyp") = MsgType
    '                dtfoBCopy.Rows.Add(Dr)
    '                dtfoBCopy.AcceptChanges()

    '            Catch ex As Threading.ThreadAbortException
    '                Threading.Thread.ResetAbort()
    '            Catch ex1 As Exception
    '                MsgBox(ex1.Message)
    '            End Try
    '        Next
    '    ElseIf MsgType = "T" Then
    '        'For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 49 + 6)) - 1) * 8 Step 8
    '        Try
    '            'LONG NumberOfPackets
    '            NumberOfPackets = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (49 + 6)))
    '            bIsfoBcopyComplete = True
    '            ifoBcopyRec = NumberOfPackets
    '        Catch ex As Threading.ThreadAbortException
    '            Threading.Thread.ResetAbort()
    '        Catch ex1 As Exception
    '            MsgBox(ex1.Message)
    '        End Try
    '        'Next
    '    End If



    'End Sub

    'Private Sub process_cm_1833(ByVal data() As Byte)

    '    ''MsgType = "H"
    '    ''{
    '    ''Dim MessageType As String 'CHAR MessageType
    '    'Dim ReportDate As Long 'LONG ReportDate
    '    'Dim UserType As Integer 'SHORT UserType
    '    'Dim BrokerId As String 'CHAR BrokerId [5]
    '    'Dim BrokerName As String 'CHAR BrokerName [25]
    '    'Dim TraderNumber As Integer 'Short TraderNumber
    '    'Dim TraderName As String ' CHAR TraderName [26]
    '    ''}


    '    'MsgTyp = "R"
    '    'STRUCT(Sec_INFO)
    '    '{
    '    Dim Symbol As String 'CHAR Symbol [10]
    '    Dim Serics As String 'CHAR Serics [2]
    '    '}

    '    Dim MarketType As Integer 'SHORT MarketType
    '    Dim OpenPrice As Long 'LONG OpenPrice
    '    Dim HighPrice As Long 'LONG HighPrice
    '    Dim LowPrice As Long 'LONG LowPrice
    '    Dim ClosingPrice As Long 'LONG ClosingPrice
    '    Dim TotalQuantityTraded As Long 'LONG TotalQuantityTraded
    '    Dim TotalValueTraded As Double 'DOUBLE TotalValueTraded
    '    Dim PreviousClosePrice As Long 'LONG PreviousClosePrice

    '    Dim High52 As Long 'LONG OpenInterest
    '    Dim Low52 As Long 'LONG ChgOpenInterest
    '    Dim Indicator As String 'CHAR Indicator [4]


    '    'MsgTyp = "T"
    '    '{
    '    'CHAR MessageType
    '    Dim NumberOfPackets As Long 'LONG NumberOfPackets
    '    'Reserved 1 byte
    '    '}
    '    Dim MsgType As String = ""
    '    Dim NoRec As Integer = 0

    '    'Dim intt As Integer = 7
    '    'Dim inx() As String
    '    'Dim InxName As String = System.BitConverter.ToString(data, 48 + 6, 1)
    '    'inx = InxName.Split("-")
    '    'MsgType = ""
    '    'For h As Integer = 0 To inx.Length - 1
    '    '    MsgType = MsgType & Chr(System.Convert.ToInt32(inx(h), 16))
    '    'Next
    '    MsgType = HxTOStr(System.BitConverter.ToString(data, 48 + 6, 1))

    '    NoRec = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 49 + 6))

    '    If MsgType = "H" Then
    '        Dim j As Integer = 0
    '        'for J as Integer = 0 74 *5 Step 74 
    '        'For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 49 + 6)) - 1) * 65 Step 65
    '        Try
    '            'LONG ReportDate
    '            ReportDate = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (49 + 6) + j))
    '            'SHORT UserType
    '            UserType = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, (53 + 6) + j))
    '            'CHAR BrokerId [5]
    '            BrokerId = HxTOStr(System.BitConverter.ToString(data, (55 + 6) + j, 5))
    '            'CHAR FirmName [25]
    '            BrokerName = HxTOStr(System.BitConverter.ToString(data, (60 + 6) + j, 25))
    '            'LONG TraderNumber
    '            TraderNumber = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (86 + 6) + j))
    '            'CHAR TraderName [26]
    '            TraderName = HxTOStr(System.BitConverter.ToString(data, (90 + 6) + j, 26))




    '        Catch ex As Threading.ThreadAbortException
    '            Threading.Thread.ResetAbort()
    '        Catch ex1 As Exception
    '            MsgBox(ex1.Message)
    '        End Try
    '        'Next
    '    ElseIf MsgType <> "H" And MsgType <> "T" Then
    '        'For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 49 + 6)) - 1) * 74 Step 74
    '        For j As Integer = 0 To 58 * 6 Step 58
    '            Try
    '                'token_no = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 50 + j))


    '                'InstrumentName = HxTOStr(System.BitConverter.ToString(data, (52 + 6) + j, 6))
    '                Symbol = HxTOStr(System.BitConverter.ToString(data, (52 + 6) + j, 10))
    '                Serics = HxTOStr(System.BitConverter.ToString(data, (62 + 6) + j, 2))
    '                'ExpiryDate = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (62 + 6) + j))
    '                'StrikePrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (72 + 6) + j))
    '                'OptionType = HxTOStr(System.BitConverter.ToString(data, (76 + 6) + j, 2))
    '                'CALevel = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, (78 + 6) + j))

    '                MarketType = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, (64 + 6) + j))
    '                OpenPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (66 + 6) + j)) / 100
    '                HighPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (70 + 6) + j)) / 100
    '                LowPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (74 + 6) + j)) / 100
    '                ClosingPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (78 + 6) + j)) / 100
    '                TotalQuantityTraded = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (82 + 6) + j))

    '                'DOUBLE TotalValueTraded
    '                TotalValueTraded = System.BitConverter.Int64BitsToDouble(IPAddress.HostToNetworkOrder(System.BitConverter.ToInt64(data, (86 + 6) + j)))

    '                PreviousClosePrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (94 + 6) + j)) / 100
    '                High52 = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (98 + 6) + j)) / 100
    '                Low52 = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (102 + 6) + j)) / 100

    '                Indicator = HxTOStr(System.BitConverter.ToString(data, (106 + 6) + j, 4))



    '                'MsgBox("InstrumentName - " & InstrumentName & vbCrLf & _
    '                '       "Symbol  -  " & Symbol & vbCrLf & _
    '                '"ExpiryDate  -  " & ExpiryDate & vbCrLf & _
    '                '"StrikePrice  - " & StrikePrice & vbCrLf & _
    '                '"OptionType  -  " & OptionType & vbCrLf & _
    '                '"CALevel  -  " & CALevel & vbCrLf & _
    '                '"MarketType  -  " & MarketType & vbCrLf & _
    '                '"OpenPrice  -  " & OpenPrice & vbCrLf & _
    '                '"HighPrice  -  " & HighPrice & vbCrLf & _
    '                '"LowPrice  -  " & LowPrice & vbCrLf & _
    '                '"ClosingPrice  -  " & ClosingPrice & vbCrLf & _
    '                '"TotalQuantityTraded  -  " & TotalQuantityTraded & vbCrLf & _
    '                '"TotalValueTraded  -  " & TotalValueTraded & vbCrLf & _
    '                '"PreviousClosePrice  -  " & PreviousClosePrice & vbCrLf & _
    '                '"OpenInterest  -  " & OpenInterest & vbCrLf & _
    '                '"ChgOpenInterest  -  " & ChgOpenInterest & vbCrLf & _
    '                '"Indicator  -  " & Indicator)

    '                Dim Dr As DataRow = dtcmBCopy.NewRow
    '                Dr("InstrumentName") = "cm" 'InstrumentName.ToString
    '                Dr("Symbol") = Symbol.ToString
    '                Dr("ExpiryDate") = "" 'ExpiryDate.ToString
    '                Dr("StrikePrice") = "" 'StrikePrice.ToString
    '                Dr("OptionType") = Serics 'OptionType.ToString
    '                Dr("CALevel") = "" 'CALevel.ToString
    '                Dr("MarketType") = MarketType.ToString
    '                Dr("OpenPrice") = OpenPrice.ToString
    '                Dr("HighPrice") = HighPrice.ToString
    '                Dr("LowPrice") = LowPrice.ToString
    '                Dr("ClosingPrice") = ClosingPrice.ToString
    '                Dr("TotalQuantityTraded") = TotalQuantityTraded.ToString
    '                Dr("TotalValueTraded") = TotalValueTraded.ToString
    '                Dr("PreviousClosePrice") = PreviousClosePrice.ToString
    '                Dr("OpenInterest") = High52.ToString 'OpenInterest.ToString
    '                Dr("ChgOpenInterest") = Low52.ToString 'ChgOpenInterest.ToString
    '                Dr("Indicator") = Indicator.ToString
    '                Dr("BCAST") = "CM"
    '                Dr("MsgTyp") = MsgType
    '                dtcmBCopy.Rows.Add(Dr)
    '                dtcmBCopy.AcceptChanges()

    '            Catch ex As Threading.ThreadAbortException
    '                Threading.Thread.ResetAbort()
    '            Catch ex1 As Exception
    '                MsgBox(ex1.Message)
    '            End Try
    '        Next
    '    ElseIf MsgType = "T" Then

    '        'For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 49 + 6)) - 1) * 8 Step 8
    '        Try
    '            'LONG NumberOfPackets
    '            NumberOfPackets = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (49 + 6)))

    '            bIscmBcopyComplete = True
    '            icmBcopyRec = NumberOfPackets

    '        Catch ex As Threading.ThreadAbortException
    '            Threading.Thread.ResetAbort()
    '        Catch ex1 As Exception
    '            MsgBox(ex1.Message)
    '        End Try
    '        'Next
    '    End If



    'End Sub

    'Private Sub process_cur_1833(ByVal data() As Byte)

    '    'MsgTyp = "R"
    '    'STRUCT(CONTRACT_DESC)
    '    '{
    '    Dim InstrumentName As String 'CHAR InstrumentName [6]
    '    Dim Symbol As String 'CHAR Symbol [10]
    '    Dim ExpiryDate As Long 'LONG ExpiryDate
    '    Dim StrikePrice As Long 'LONG StrikePrice
    '    Dim OptionType As String 'CHAR OptionType [2]
    '    Dim CALevel As Integer 'SHORT CALevel
    '    '}

    '    Dim MarketType As Integer 'SHORT MarketType
    '    Dim OpenPrice As Long 'LONG OpenPrice
    '    Dim HighPrice As Long 'LONG HighPrice
    '    Dim LowPrice As Long 'LONG LowPrice
    '    Dim ClosingPrice As Long 'LONG ClosingPrice
    '    Dim TotalQuantityTraded As Long 'LONG TotalQuantityTraded
    '    Dim TotalValueTraded As Double 'DOUBLE TotalValueTraded
    '    Dim PreviousClosePrice As Long 'LONG PreviousClosePrice
    '    Dim OpenInterest As Long 'LONG OpenInterest
    '    Dim ChgOpenInterest As Long 'LONG ChgOpenInterest
    '    Dim Indicator As String 'CHAR Indicator [4]

    '    ''MsgType = "H"
    '    ''{
    '    ''Dim MessageType As String 'CHAR MessageType
    '    'Dim ReportDate As Long 'LONG ReportDate
    '    'Dim UserType As Integer 'SHORT UserType
    '    'Dim BrokerId As String 'CHAR BrokerId [5]
    '    'Dim FirmName As String 'CHAR FirmName [25]
    '    'Dim TraderNumber As Long 'LONG TraderNumber
    '    'Dim TraderName As String ' CHAR TraderName [26]
    '    ''}

    '    'MsgTyp = "T"
    '    '{
    '    'CHAR MessageType
    '    Dim NumberOfPackets As Long 'LONG NumberOfPackets
    '    'Reserved 1 byte
    '    '}
    '    Dim MsgType As String = ""
    '    Dim NoRec As Integer = 0

    '    'Dim intt As Integer = 7
    '    'Dim inx() As String
    '    'Dim InxName As String = System.BitConverter.ToString(data, 48 + 6, 1)
    '    'inx = InxName.Split("-")
    '    'MsgType = ""
    '    'For h As Integer = 0 To inx.Length - 1
    '    '    MsgType = MsgType & Chr(System.Convert.ToInt32(inx(h), 16))
    '    'Next
    '    MsgType = HxTOStr(System.BitConverter.ToString(data, 48 + 6, 1))

    '    NoRec = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 49 + 6))

    '    If MsgType = "H" Then
    '        Dim j As Integer = 0
    '        'for J as Integer = 0 74 *5 Step 74 
    '        'For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 49 + 6)) - 1) * 67 Step 67
    '        Try
    '            'LONG ReportDate
    '            ReportDate = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (49 + 6) + j))
    '            'SHORT UserType
    '            UserType = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, (53 + 6) + j))
    '            'CHAR BrokerId [5]
    '            BrokerId = HxTOStr(System.BitConverter.ToString(data, (55 + 6) + j, 5))
    '            'CHAR FirmName [25]
    '            FirmName = HxTOStr(System.BitConverter.ToString(data, (60 + 6) + j, 25))
    '            'LONG TraderNumber
    '            TraderNumber = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (86 + 6) + j))
    '            'CHAR TraderName [26]
    '            TraderName = HxTOStr(System.BitConverter.ToString(data, (90 + 6) + j, 26))




    '        Catch ex As Threading.ThreadAbortException
    '            Threading.Thread.ResetAbort()
    '        Catch ex1 As Exception
    '            MsgBox(ex1.Message)
    '        End Try
    '        'Next
    '    ElseIf MsgType <> "H" And MsgType <> "T" Then
    '        'For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 49 + 6)) - 1) * 74 Step 74
    '        For j As Integer = 0 To 74 * 5 Step 74
    '            Try
    '                'token_no = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 50 + j))


    '                InstrumentName = HxTOStr(System.BitConverter.ToString(data, (52 + 6) + j, 6))
    '                Symbol = HxTOStr(System.BitConverter.ToString(data, (58 + 6) + j, 10))
    '                ExpiryDate = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (68 + 6) + j))
    '                StrikePrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (72 + 6) + j))
    '                OptionType = HxTOStr(System.BitConverter.ToString(data, (76 + 6) + j, 2))
    '                CALevel = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, (78 + 6) + j))

    '                MarketType = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, (80 + 6) + j))
    '                OpenPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (82 + 6) + j)) / 100
    '                HighPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (86 + 6) + j)) / 100
    '                LowPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (90 + 6) + j)) / 100
    '                ClosingPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (94 + 6) + j)) / 100
    '                TotalQuantityTraded = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (98 + 6) + j))

    '                'DOUBLE TotalValueTraded
    '                TotalValueTraded = System.BitConverter.Int64BitsToDouble(IPAddress.HostToNetworkOrder(System.BitConverter.ToInt64(data, (102 + 6) + j)))

    '                PreviousClosePrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (110 + 6) + j)) / 100
    '                OpenInterest = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (114 + 6) + j)) / 100
    '                ChgOpenInterest = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (118 + 6) + j)) / 100

    '                Indicator = HxTOStr(System.BitConverter.ToString(data, (122 + 6) + j, 4))



    '                'MsgBox("InstrumentName - " & InstrumentName & vbCrLf & _
    '                '       "Symbol  -  " & Symbol & vbCrLf & _
    '                '"ExpiryDate  -  " & ExpiryDate & vbCrLf & _
    '                '"StrikePrice  - " & StrikePrice & vbCrLf & _
    '                '"OptionType  -  " & OptionType & vbCrLf & _
    '                '"CALevel  -  " & CALevel & vbCrLf & _
    '                '"MarketType  -  " & MarketType & vbCrLf & _
    '                '"OpenPrice  -  " & OpenPrice & vbCrLf & _
    '                '"HighPrice  -  " & HighPrice & vbCrLf & _
    '                '"LowPrice  -  " & LowPrice & vbCrLf & _
    '                '"ClosingPrice  -  " & ClosingPrice & vbCrLf & _
    '                '"TotalQuantityTraded  -  " & TotalQuantityTraded & vbCrLf & _
    '                '"TotalValueTraded  -  " & TotalValueTraded & vbCrLf & _
    '                '"PreviousClosePrice  -  " & PreviousClosePrice & vbCrLf & _
    '                '"OpenInterest  -  " & OpenInterest & vbCrLf & _
    '                '"ChgOpenInterest  -  " & ChgOpenInterest & vbCrLf & _
    '                '"Indicator  -  " & Indicator)

    '                Dim Dr As DataRow = dtcurBCopy.NewRow
    '                Dr("InstrumentName") = InstrumentName.ToString
    '                Dr("Symbol") = Symbol.ToString
    '                Dr("ExpiryDate") = ExpiryDate.ToString
    '                Dr("StrikePrice") = StrikePrice.ToString
    '                Dr("OptionType") = OptionType.ToString
    '                Dr("CALevel") = CALevel.ToString
    '                Dr("MarketType") = MarketType.ToString
    '                Dr("OpenPrice") = OpenPrice.ToString
    '                Dr("HighPrice") = HighPrice.ToString
    '                Dr("LowPrice") = LowPrice.ToString
    '                Dr("ClosingPrice") = ClosingPrice.ToString
    '                Dr("TotalQuantityTraded") = TotalQuantityTraded.ToString
    '                Dr("TotalValueTraded") = TotalValueTraded.ToString
    '                Dr("PreviousClosePrice") = PreviousClosePrice.ToString
    '                Dr("OpenInterest") = OpenInterest.ToString
    '                Dr("ChgOpenInterest") = ChgOpenInterest.ToString
    '                Dr("Indicator") = Indicator.ToString
    '                Dr("BCAST") = "Cur"
    '                Dr("MsgTyp") = MsgType
    '                dtcurBCopy.Rows.Add(Dr)
    '                dtcurBCopy.AcceptChanges()

    '            Catch ex As Threading.ThreadAbortException
    '                Threading.Thread.ResetAbort()
    '            Catch ex1 As Exception
    '                MsgBox(ex1.Message)
    '            End Try
    '        Next
    '    ElseIf MsgType = "T" Then
    '        'For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 49 + 6)) - 1) * 8 Step 8
    '        Try
    '            'LONG NumberOfPackets
    '            NumberOfPackets = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, (49 + 6)))
    '            bIscurBcopyComplete = True
    '            icurBcopyRec = NumberOfPackets

    '        Catch ex As Threading.ThreadAbortException
    '            Threading.Thread.ResetAbort()
    '        Catch ex1 As Exception
    '            MsgBox(ex1.Message)
    '        End Try
    '        'Next
    '    End If
    'End Sub

    ''' <summary>
    ''' process_fo_7208
    ''' </summary>
    ''' <param name="data"></param>
    ''' <remarks>This method call to decompress FO receiving byte</remarks>
    Private Sub process_fo_7208(ByVal data() As Byte, ByVal flgiscompress As Boolean)
        Dim token_no As Long
        Dim buy_price As Double
        Dim sale_price As Double
        Dim last_trade_price As Double
        Dim VolumeTradedToday As Double
        Dim ClosingPrice As Double

        Dim pktinc As Integer
        If flgiscompress = True Then
            pktinc = 0
        Else
            pktinc = 6
        End If


        If GVarIsNewBhavcopy = True Then
            GVarIsNewBhavcopy = False
        End If
        'If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 44)) = 468 Then
        For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 48 + pktinc)) - 1) * 214 Step 214
            Try
                token_no = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 50 + j + pktinc))

                If Not clsGlobal.H_All_token_FO.ContainsKey(token_no) Then
                    Continue For
                End If


                '  Debug.WriteLine(token_no.ToString())

                buy_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 110 + j + pktinc)) / 100
                sale_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 170 + j + pktinc)) / 100
                last_trade_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 62 + j + pktinc)) / 100

                If VarFoBCurrentDate < IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 76 + j + pktinc)) Then
                    VarFoBCurrentDate = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 76 + j + pktinc))
                End If

                'VolumeTradedToday = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 54 + j + pktinc))
                VolumeTradedToday = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 58 + j + pktinc))
                'VolumeTradedToday = IPAddress.HostToNetworkOrder(System.BitConverter.ToUInt32(data, 58 + j + pktinc))

                ClosingPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 248 + j + pktinc)) / 100

                'If DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")) >= CDate(G_VarExpiryDate) Then
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

                    If FlgBcastStop = False Then
                        'Dim fltppr As Double

                        If UDP_SELECTED_TOKEN = 1 Then

                            If udpselsymbol.Contains(token_no) Then
                                If fltpprice.Contains(token_no) Then
                                    fbuyprice.Item(token_no) = buy_price
                                    fsaleprice.Item(token_no) = sale_price
                                    fltpprice.Item(token_no) = last_trade_price
                                Else
                                    fbuyprice.Add(token_no, buy_price)
                                    fsaleprice.Add(token_no, sale_price)
                                    fltpprice.Add(token_no, last_trade_price)
                                End If

                                If closeprice.Contains(token_no) Then
                                    closeprice.Item(token_no) = ClosingPrice
                                Else
                                    closeprice.Add(token_no, ClosingPrice)
                                End If

                            End If

                        Else
                            If fltpprice.Contains(token_no) Then
                                fbuyprice.Item(token_no) = buy_price
                                fsaleprice.Item(token_no) = sale_price
                                fltpprice.Item(token_no) = last_trade_price
                            Else
                                fbuyprice.Add(token_no, buy_price)
                                fsaleprice.Add(token_no, sale_price)
                                fltpprice.Add(token_no, last_trade_price)
                            End If

                            If closeprice.Contains(token_no) Then
                                closeprice.Item(token_no) = ClosingPrice
                            Else
                                closeprice.Add(token_no, ClosingPrice)
                            End If
                        End If

                    Else
                        If NetMode = "UDP" Then

                            If UDP_SELECTED_TOKEN = 1 Then
                                If udpselsymbol.Contains(token_no) Then
                                    If ss_fltpprice.Contains(token_no) Then
                                        ss_fbuyprice.Item(token_no) = buy_price
                                        ss_fsaleprice.Item(token_no) = sale_price
                                        ss_fltpprice.Item(token_no) = last_trade_price
                                    Else
                                        ss_fbuyprice.Add(token_no, buy_price)
                                        ss_fsaleprice.Add(token_no, sale_price)
                                        ss_fltpprice.Add(token_no, last_trade_price)
                                    End If

                                    If ss_closeprice.Contains(token_no) Then
                                        ss_closeprice.Item(token_no) = ClosingPrice
                                    Else
                                        ss_closeprice.Add(token_no, ClosingPrice)
                                    End If
                                End If



                            Else
                                If ss_fltpprice.Contains(token_no) Then
                                    ss_fbuyprice.Item(token_no) = buy_price
                                    ss_fsaleprice.Item(token_no) = sale_price
                                    ss_fltpprice.Item(token_no) = last_trade_price
                                Else
                                    ss_fbuyprice.Add(token_no, buy_price)
                                    ss_fsaleprice.Add(token_no, sale_price)
                                    ss_fltpprice.Add(token_no, last_trade_price)
                                End If

                                If ss_closeprice.Contains(token_no) Then
                                    ss_closeprice.Item(token_no) = ClosingPrice
                                Else
                                    ss_closeprice.Add(token_no, ClosingPrice)
                                End If
                            End If

                        End If
                    End If



                Else
                    If FlgBcastStop = False Then
                        If UDP_SELECTED_TOKEN = 1 Then
                            If udpselsymbol.Contains(token_no) Then
                                If ltpprice.Contains(token_no) Then
                                    buyprice.Item(token_no) = buy_price
                                    saleprice.Item(token_no) = sale_price
                                    ltpprice.Item(token_no) = last_trade_price
                                    MKTltpprice.Item(token_no) = last_trade_price
                                Else
                                    buyprice.Add(token_no, buy_price)
                                    saleprice.Add(token_no, sale_price)
                                    ltpprice.Add(token_no, last_trade_price)
                                    MKTltpprice.Add(token_no, last_trade_price)
                                End If

                                If volumeprice.Contains(token_no) Then
                                    volumeprice.Item(token_no) = VolumeTradedToday
                                Else
                                    volumeprice.Add(token_no, VolumeTradedToday)
                                End If

                                If closeprice.Contains(token_no) Then
                                    closeprice.Item(token_no) = ClosingPrice
                                Else
                                    closeprice.Add(token_no, ClosingPrice)
                                End If
                            End If
                        Else
                            If ltpprice.Contains(token_no) Then
                                buyprice.Item(token_no) = buy_price
                                saleprice.Item(token_no) = sale_price
                                ltpprice.Item(token_no) = last_trade_price
                                MKTltpprice.Item(token_no) = last_trade_price
                            Else
                                buyprice.Add(token_no, buy_price)
                                saleprice.Add(token_no, sale_price)
                                ltpprice.Add(token_no, last_trade_price)
                                MKTltpprice.Add(token_no, last_trade_price)
                            End If

                            If volumeprice.Contains(token_no) Then
                                volumeprice.Item(token_no) = VolumeTradedToday
                            Else
                                volumeprice.Add(token_no, VolumeTradedToday)
                            End If

                            If closeprice.Contains(token_no) Then
                                closeprice.Item(token_no) = ClosingPrice
                            Else
                                closeprice.Add(token_no, ClosingPrice)
                            End If

                        End If
                    Else
                        If NetMode = "UDP" Then
                            If UDP_SELECTED_TOKEN = 1 Then
                                If udpselsymbol.Contains(token_no) Then
                                    If ss_ltpprice.Contains(token_no) Then
                                        ss_buyprice.Item(token_no) = buy_price
                                        ss_saleprice.Item(token_no) = sale_price
                                        ss_ltpprice.Item(token_no) = last_trade_price
                                        ss_MKTltpprice.Item(token_no) = last_trade_price
                                        MKTltpprice.Item(token_no) = last_trade_price
                                    Else
                                        ss_buyprice.Add(token_no, buy_price)
                                        ss_saleprice.Add(token_no, sale_price)
                                        ss_ltpprice.Add(token_no, last_trade_price)
                                        ss_MKTltpprice.Add(token_no, last_trade_price)
                                        MKTltpprice.Add(token_no, last_trade_price)
                                    End If

                                    If ss_volumeprice.Contains(token_no) Then
                                        ss_volumeprice.Item(token_no) = VolumeTradedToday
                                    Else
                                        ss_volumeprice.Add(token_no, VolumeTradedToday)
                                    End If

                                    If ss_closeprice.Contains(token_no) Then
                                        ss_closeprice.Item(token_no) = ClosingPrice
                                    Else
                                        ss_closeprice.Add(token_no, ClosingPrice)
                                    End If
                                End If
                            Else
                                If ss_ltpprice.Contains(token_no) Then
                                    ss_buyprice.Item(token_no) = buy_price
                                    ss_saleprice.Item(token_no) = sale_price
                                    ss_ltpprice.Item(token_no) = last_trade_price
                                    ss_MKTltpprice.Item(token_no) = last_trade_price
                                    MKTltpprice.Item(token_no) = last_trade_price
                                Else
                                    ss_buyprice.Add(token_no, buy_price)
                                    ss_saleprice.Add(token_no, sale_price)
                                    ss_ltpprice.Add(token_no, last_trade_price)
                                    ss_MKTltpprice.Add(token_no, last_trade_price)
                                    MKTltpprice.Add(token_no, last_trade_price)
                                End If

                                If ss_volumeprice.Contains(token_no) Then
                                    ss_volumeprice.Item(token_no) = VolumeTradedToday
                                Else
                                    ss_volumeprice.Add(token_no, VolumeTradedToday)
                                End If

                                If ss_closeprice.Contains(token_no) Then
                                    ss_closeprice.Item(token_no) = ClosingPrice
                                Else
                                    ss_closeprice.Add(token_no, ClosingPrice)
                                End If

                            End If
                        End If
                    End If


                End If
            Catch ex As Threading.ThreadAbortException
                Threading.Thread.ResetAbort()
            End Try
        Next

        'End If
    End Sub

    Private Sub process_Inx_7207(ByVal data() As Byte)
        REM SyncLock SyncLock SyncHndOIQQue
        Dim IndexName As String
        Dim IndexValue As Long
        Dim iHighPrice As Long
        Dim iLowPrice As Long
        Dim iOpenPrice As Long
        Dim iClosingPrice As Long
        Dim PrecentageChange As Long
        Dim iNetChangeIndicator As Char

        'Dim StrInxSql As String = ""
        'If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 46)) = 466 Then
        For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, (48 + 6))) - 1) * 72 Step 72
            Try
                Dim inx() As String
                Dim InxName As String = System.BitConverter.ToString(data, 56 + j, 21)
                inx = InxName.Split("-")
                IndexName = ""
                For h As Integer = 0 To inx.Length - 1
                    IndexName = IndexName & Chr(System.Convert.ToInt32(inx(h), 16))
                    If (IndexName = "Nifty Fin Service") Then
                        IndexName = "NiftyFinService"

                    End If
                    If (IndexName = "NIFTY MID SELECT") Then
                        IndexName = "NIFTYMIDSELECT"

                    End If
                Next



                IndexValue = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 78 + j))
                iHighPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 82 + j))
                iLowPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 86 + j))
                iOpenPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 90 + j))
                iClosingPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 94 + j))
                PrecentageChange = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 98 + j))
                iNetChangeIndicator = Convert.ToChar(data(126 + j))

                Try

                    'If CMconn.State = ADODB.ObjectStateEnum.adStateClosed Then CM_open_connection()
                    'Dim a As Integer
                    'While Not CMconn.State = ObjectStateEnum.adStateOpen
                    '    a = a + 1
                    'End While
                    'conn.Execute("Sp_InsertInx '" & IndexName & "'," & IndexValue & "," & iHighPrice & "," & iLowPrice & "," & iOpenPrice & "," & iClosingPrice & "," & PrecentageChange & ",'" & iNetChangeIndicator & "'")

                    'CMconn.Execute

                    'OIQQue.Enqueue("Exec Sp_016 '" & IndexName & "'," & ED.Einx(IndexValue) & "," & ED.Einx(iHighPrice) & "," & ED.Einx(iLowPrice) & "," & ED.Einx(iOpenPrice) & "," & ED.Einx(iClosingPrice) & "," & ED.Einx(PrecentageChange) & ",'" & iNetChangeIndicator & "'" & ";" & vbCrLf)

                    If eIdxprice.Contains(IndexName.Trim()) Then
                        eIdxprice(IndexName.Trim()) = IndexValue / 100
                    Else
                        eIdxprice.Add(IndexName.Trim(), IndexValue / 100)
                    End If

                    If eIdxClosingprice.Contains(IndexName.Trim()) Then
                        eIdxClosingprice(IndexName.Trim()) = iClosingPrice / 100
                    Else
                        eIdxClosingprice.Add(IndexName.Trim(), iClosingPrice / 100)
                    End If

                Catch ex As Exception
                    WriteLog("Proc::process_Inx_7207::-->" & vbCrLf & ex.Message)
                End Try
            Catch ex As Threading.ThreadAbortException
                WriteLog("Proc::process_Inx_7207::-->" & vbCrLf & ex.Message)
                Threading.Thread.ResetAbort()
            End Try
        Next
        'End If
        'InxUpd(StrInxSql)
        'OIQQue.Enqueue(StrInxSql)
        REM SyncLock End SyncLock
    End Sub
    Private Sub process_cm_7208_MTS(ByVal data() As Byte)
        Dim token_no As Long
        Dim buy_price As Double
        Dim sale_price As Double
        Dim last_trade_price As Double
        Dim closing_price As Double


        Try

            Dim msg_Code As Integer
            Dim strIdx As String
            Dim IndexName As String

            msg_Code = System.BitConverter.ToInt16(data, 0) '1 = Normal || 2 = Index

            If msg_Code = 1 Then


                token_no = System.BitConverter.ToInt32(data, 2)

                buy_price = System.BitConverter.ToInt32(data, 6) / 100
                sale_price = System.BitConverter.ToInt32(data, 10) / 100
                last_trade_price = System.BitConverter.ToInt32(data, 14) / 100
                'LastTradeTime = System.BitConverter.ToInt32(data, 18)

                'OpenPrice = System.BitConverter.ToInt32(data, 22) / 100
                'HighPrice = System.BitConverter.ToInt32(data, 28) / 100
                'LowPrice = System.BitConverter.ToInt32(data, 30) / 100

                'VolumeTradedToday = System.BitConverter.ToInt32(data, 34)

                'If VarBCurrentDate < System.BitConverter.ToInt32(data, 18) Then
                '    VarBCurrentDate = System.BitConverter.ToInt32(data, 18)
                'End If
                'If VarBCurrentDate >= G_VarExpiryDate1 Then
                '    IsVersionExpire = True
                '    Application.Exit()
                'End If

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
                End If

            Else
                strIdx = System.BitConverter.ToString(data, 42)


                Dim inx() As String
                Dim InxName As String = System.BitConverter.ToString(data, 42, 21)
                inx = InxName.Split("-")
                IndexName = ""
                For h As Integer = 0 To inx.Length - 1
                    IndexName = IndexName & Chr(System.Convert.ToInt32(inx(h), 16)).ToString().Trim()
                Next
                If IndexName = "Nifty50" Then
                    IndexName = "Nifty 50"
                End If
                If IndexName = "NiftyBank" Then
                    IndexName = "Nifty Bank"
                End If
                If IndexName = "NiftyFinService" Then
                    IndexName = "Nifty Fin Service"
                End If

                'buy_price = System.BitConverter.ToInt32(data, 6) / 100
                'sale_price = System.BitConverter.ToInt32(data, 10) / 100
                last_trade_price = System.BitConverter.ToInt32(data, 14) / 100
                'LastTradeTime = System.BitConverter.ToInt32(data, 18)

                'OpenPrice = System.BitConverter.ToInt32(data, 22) / 100
                'HighPrice = System.BitConverter.ToInt32(data, 28) / 100
                'LowPrice = System.BitConverter.ToInt32(data, 30) / 100
                closing_price = System.BitConverter.ToInt32(data, 34) / 100
                If eIdxprice.Contains(IndexName.Trim()) Then
                    eIdxprice(IndexName.Trim()) = last_trade_price
                Else
                    eIdxprice.Add(IndexName.Trim(), last_trade_price)
                End If

                If eIdxClosingprice.Contains(IndexName.Trim()) Then
                    eIdxClosingprice(IndexName.Trim()) = closing_price
                Else
                    eIdxClosingprice.Add(IndexName.Trim(), closing_price)
                End If

            End If







        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub
    'Private Sub process_cm_7208_MTS(ByVal data() As Byte)
    '    Dim token_no As Long
    '    Dim buy_price As Double
    '    Dim sale_price As Double
    '    Dim last_trade_price As Double


    '    Try

    '        Dim msg_Code As Integer
    '        Dim strIdx As String
    '        Dim IndexName As String

    '        msg_Code = System.BitConverter.ToInt16(data, 0) '1 = Normal || 2 = Index

    '        If msg_Code = 1 Then


    '            token_no = System.BitConverter.ToInt32(data, 2)

    '            buy_price = System.BitConverter.ToInt32(data, 6) / 100
    '            sale_price = System.BitConverter.ToInt32(data, 10) / 100
    '            last_trade_price = System.BitConverter.ToInt32(data, 14) / 100
    '            'LastTradeTime = System.BitConverter.ToInt32(data, 18)

    '            'OpenPrice = System.BitConverter.ToInt32(data, 22) / 100
    '            'HighPrice = System.BitConverter.ToInt32(data, 28) / 100
    '            'LowPrice = System.BitConverter.ToInt32(data, 30) / 100

    '            'VolumeTradedToday = System.BitConverter.ToInt32(data, 34)

    '            'If VarBCurrentDate < System.BitConverter.ToInt32(data, 18) Then
    '            '    VarBCurrentDate = System.BitConverter.ToInt32(data, 18)
    '            'End If
    '            'If VarBCurrentDate >= G_VarExpiryDate1 Then
    '            '    IsVersionExpire = True
    '            '    Application.Exit()
    '            'End If

    '            If eqfutall.Contains(token_no) Then
    '                If eltpprice.Contains(token_no) Then
    '                    ebuyprice.Item(token_no) = buy_price
    '                    esaleprice.Item(token_no) = sale_price
    '                    eltpprice.Item(token_no) = last_trade_price
    '                Else
    '                    ebuyprice.Add(token_no, buy_price)
    '                    esaleprice.Add(token_no, sale_price)
    '                    eltpprice.Add(token_no, last_trade_price)
    '                End If
    '            End If

    '        Else
    '            strIdx = System.BitConverter.ToString(data, 38)


    '            Dim inx() As String
    '            Dim InxName As String = System.BitConverter.ToString(data, 38, 21)
    '            inx = InxName.Split("-")
    '            IndexName = ""
    '            For h As Integer = 0 To inx.Length - 1
    '                IndexName = IndexName & Chr(System.Convert.ToInt32(inx(h), 16)).ToString().Trim()
    '            Next
    '            If IndexName = "Nifty50" Then
    '                IndexName = "Nifty 50"
    '            End If
    '            If IndexName = "NiftyBank" Then
    '                IndexName = "Nifty Bank"
    '            End If
    '            If IndexName = "NiftyFinService" Then
    '                IndexName = "Nifty Fin Service"
    '            End If

    '            'buy_price = System.BitConverter.ToInt32(data, 6) / 100
    '            'sale_price = System.BitConverter.ToInt32(data, 10) / 100
    '            last_trade_price = System.BitConverter.ToInt32(data, 14)
    '            'LastTradeTime = System.BitConverter.ToInt32(data, 18)

    '            'OpenPrice = System.BitConverter.ToInt32(data, 22) / 100
    '            'HighPrice = System.BitConverter.ToInt32(data, 28) / 100
    '            'LowPrice = System.BitConverter.ToInt32(data, 30) / 100

    '            If eIdxprice.Contains(IndexName.Trim()) Then
    '                eIdxprice(IndexName.Trim()) = last_trade_price
    '            Else
    '                eIdxprice.Add(IndexName.Trim(), last_trade_price)
    '            End If

    '            If eIdxClosingprice.Contains(IndexName.Trim()) Then
    '                eIdxClosingprice(IndexName.Trim()) = last_trade_price
    '            Else
    '                eIdxClosingprice.Add(IndexName.Trim(), last_trade_price)
    '            End If

    '        End If



    '        If token_no = "16787" Then
    '            WriteLogbast("token 16787 LTP : " & last_trade_price)
    '        End If



    '    Catch ex As Threading.ThreadAbortException
    '        Threading.Thread.ResetAbort()
    '    End Try
    'End Sub
    ''' <summary>
    ''' process_cm_7208
    ''' </summary>
    ''' <param name="data"></param>
    ''' <remarks>This method call to decompress CM receiving byte</remarks>
    Private Sub process_cm_7208(ByVal data() As Byte, ByVal flgiscompress As Boolean)
        Dim token_no As Long
        Dim buy_price As Double
        Dim sale_price As Double
        Dim last_trade_price As Double
        Dim pktinc As Integer
        If flgiscompress = True Then
            pktinc = 0
        Else
            pktinc = 6
        End If
        'If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 46)) = 466 Then
        For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 48 + pktinc)) - 1) * 212 Step 212
            Try
                token_no = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 50 + j + pktinc))
                buy_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 108 + j + pktinc)) / 100
                sale_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 168 + j + pktinc)) / 100
                last_trade_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 60 + j + pktinc)) / 100


                If VarEQBCurrentDate < IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 74 + j + pktinc)) Then
                    VarEQBCurrentDate = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 74 + j + pktinc))
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
                If UDP_SELECTED_TOKEN = 1 Then
                    If udpselsymbol.Contains(token_no) Then
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
                    End If
                Else
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
                End If

            Catch ex As Threading.ThreadAbortException
                Threading.Thread.ResetAbort()
            End Try

        Next

        'Return False
        'Else
        'Return False
        'End If
    End Sub
    ''' <summary>
    ''' process_Currency_7208
    ''' </summary>
    ''' <param name="data"></param>
    ''' <remarks>This method call to decompress Currency receiving byte</remarks>
    ''' 

    Private Sub process_cm_7208_new(ByVal data() As Byte, ByVal flgiscompress As Boolean)
        Dim token_no As Long
        Dim buy_price As Double
        Dim sale_price As Double
        Dim last_trade_price As Double
        Dim pktinc As Integer
        If flgiscompress = True Then
            pktinc = 0
        Else
            pktinc = 6
            'pktinc = 2
        End If
        'pktinc = 2
        'If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 46)) = 466 Then
        For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 48 + pktinc)) - 1) * 262 Step 262
            Try
                token_no = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 50 + j + pktinc))
                buy_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 118 + j + pktinc)) / 100.0
                sale_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 198 + j + pktinc)) / 100.0
                last_trade_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 66 + j + pktinc)) / 100.0


                If VarEQBCurrentDate < IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 80 + j + pktinc)) Then
                    VarEQBCurrentDate = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 80 + j + pktinc))
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
                If UDP_SELECTED_TOKEN = 1 Then
                    If udpselsymbol.Contains(token_no) Then
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
                    End If
                Else
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
                End If

            Catch ex As Threading.ThreadAbortException
                Threading.Thread.ResetAbort()
            End Try

        Next

        'Return False
        'Else
        'Return False
        'End If
    End Sub
    Private Sub process_Currency_7208(ByVal data() As Byte, ByVal flgiscompress As Boolean)
        Dim token_no As Long
        Dim buy_price As Double
        Dim sale_price As Double
        Dim last_trade_price As Double
        Dim iAdd As Integer = 2
        Dim pktinc As Integer = 0
        If flgiscompress = True Then
            pktinc = 0
        Else
            pktinc = 6
        End If
        'If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 44)) = 468 Then
        For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 46 + iAdd + pktinc)) - 1) * 214 Step 214
            Try
                token_no = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 48 + j + iAdd + pktinc))

                If Not clsGlobal.H_All_token_EQ.ContainsKey(token_no) Then
                    Continue For
                End If
                buy_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 108 + j + iAdd + pktinc)) / 10000000
                sale_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 168 + j + iAdd + pktinc)) / 10000000
                last_trade_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 60 + j + iAdd + pktinc)) / 10000000

                If VarCurBCurrentDate < IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 74 + j + iAdd + pktinc)) Then
                    VarCurBCurrentDate = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 74 + j + iAdd + pktinc))
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
                If UDP_SELECTED_TOKEN = 1 Then
                    If udpselsymbol.Contains(token_no) Then
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
                                currMKTltpprice.Item(token_no) = last_trade_price
                            Else
                                Currbuyprice.Add(token_no, buy_price)
                                Currsaleprice.Add(token_no, sale_price)
                                Currltpprice.Add(token_no, last_trade_price)
                                currMKTltpprice.Add(token_no, last_trade_price)
                            End If
                        End If
                    End If
                Else

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
                            currMKTltpprice.Item(token_no) = last_trade_price
                        Else
                            Currbuyprice.Add(token_no, buy_price)
                            Currsaleprice.Add(token_no, sale_price)
                            Currltpprice.Add(token_no, last_trade_price)
                            currMKTltpprice.Add(token_no, last_trade_price)
                        End If
                    End If
                End If


            Catch ex As Threading.ThreadAbortException
                Threading.Thread.ResetAbort()
            End Try
        Next
        'End If
    End Sub

    ''' <summary>
    ''' process_fo_data
    ''' </summary>
    ''' <param name="temp_data">Receiving byte</param>
    ''' <remarks>This method call to decompress FO broadcast byte and after that byte pass to process_fo_7208 method</remarks>
    Public Sub process_fo_data(ByVal temp_data() As Byte)
        Try
            Dim decompress_data() As Byte
            Dim compressed_length_old As Int16 = 0
            Dim compressed_length As Int16 = 0
            Dim packet_counter As Int16 = 0
            If temp_data(0) = 2 Then
                While packet_counter < IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 2))
                    'Debug.WriteLine(IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 2)))
                    compressed_length = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 4 + compressed_length_old + (packet_counter * 2)))
                    If compressed_length > 0 And compressed_length <= 512 Then
                        Dim compressed_data(compressed_length - 1) As Byte
                        Try
                            Array.Copy(temp_data, 6 + compressed_length_old + (packet_counter * 2), compressed_data, 0, compressed_length)
                        Catch ex As Exception
                            Exit While
                        End Try
                        Try
                            decompress_data = lzo_fo.Decompress(compressed_data)
                            If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(decompress_data, 18)) = 7208 Then
                                Call process_fo_7208(decompress_data, True)
                            ElseIf IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(decompress_data, 18)) = 7202 Then
                                Call process_OI_7202(decompress_data)  ' Obsolute to be remove in future shailesh
                            ElseIf IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(decompress_data, 18)) = 17202 Then
                                process_OI_17202(decompress_data)
                            End If
                        Catch ex As Exception
                            Exit While
                        End Try
                    ElseIf compressed_length = 0 Then
                        'MsgBox(IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))))

                        If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 7208 Then
                            Call process_fo_7208(temp_data, False)
                        End If
                        'ElseIf IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 6501 Then
                        '    Call process_fo_6501(temp_data)
                        'End If
                    End If
                    If compressed_length > 504 Then Exit While
                    compressed_length_old += compressed_length
                    packet_counter += 1
                End While
            End If

        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub

    ''' <summary>
    ''' process_CM_data
    ''' </summary>
    ''' <param name="temp_data">Receiving byte</param>
    ''' <remarks>This method call to decompress CM broadcast byte and after that byte pass to process_cm_7208 method</remarks>
    Public Sub process_cm_data(ByVal temp_data() As Byte)
        Try
            Dim decompress_data() As Byte
            Dim compressed_length_old As Int16 = 0
            Dim compressed_length As Int16 = 0
            Dim packet_counter As Int16 = 0

            If temp_data(0) = 4 Then

                While packet_counter < IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 2))
                    compressed_length = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 4 + compressed_length_old + (packet_counter * 2)))

                    If compressed_length > 0 And compressed_length <= 512 Then
                        Dim compressed_data(compressed_length - 1) As Byte
                        Try
                            Array.Copy(temp_data, 6 + compressed_length_old + (packet_counter * 2), compressed_data, 0, compressed_length)
                        Catch ex As Exception
                            Exit While
                        End Try

                        Try
                            decompress_data = lzo_cm.Decompress(compressed_data)


                            If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(decompress_data, 18)) = 18705 Then
                                'process_cm_7208_new(decompress_data, True)
                                'MessageBox.Show(18705)

                            ElseIf IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(decompress_data, 18)) = 7208 Then
                                process_cm_7208_new(decompress_data, True)
                            End If
                        Catch ex As Exception
                            Exit While
                        End Try
                    ElseIf compressed_length = 0 Then
                        'MsgBox(IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))))

                        'If NEW_CM_BROADCAST = 1 Then

                        '    If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 18705 Then
                        '        Call process_cm_7208_new(temp_data, False)
                        '    ElseIf IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 7207 Then
                        '        Call process_Inx_7207(temp_data)
                        '        'InxQue.Enqueue(temp_data)
                        '    End If
                        'Else
                        '    If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 7208 Then
                        '        Call process_cm_7208(temp_data, False)
                        '    ElseIf IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 7207 Then
                        '        Call process_Inx_7207(temp_data)
                        '        'InxQue.Enqueue(temp_data)
                        '    End If
                        'End If



                        If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 18705 Then
                            Call process_cm_7208_new(temp_data, False)
                        ElseIf IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 7207 Then
                            Call process_Inx_7207(temp_data)
                            'InxQue.Enqueue(temp_data)
                        ElseIf IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 7208 Then
                            Call process_cm_7208_new(temp_data, False)

                        End If

                        'If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 7208 Then
                        '    Call process_cm_7208(temp_data, False)
                        'ElseIf IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 7207 Then
                        '    Call process_Inx_7207(temp_data)
                        '    'InxQue.Enqueue(temp_data)
                        'End If
                        'ElseIf IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 6501 Then
                        '    'Call process_fo_6501(temp_data)
                        'End If
                    End If
                    If compressed_length > 504 Then Exit While
                    compressed_length_old += compressed_length
                    packet_counter += 1
                End While
            End If

        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub
    ''' <summary>
    ''' process_Currency_data
    ''' </summary>
    ''' <param name="temp_data">Receiving byte</param>
    ''' <remarks>This method call to decompress Currency broadcast byte and after that byte pass to process_Currency_7208 method</remarks>
    Public Sub process_Currency_data(ByVal temp_data() As Byte)
        '   Try
        Dim decompress_data() As Byte
        Dim compressed_length_old As Int16 = 0
        Dim compressed_length As Int16 = 0
        Dim packet_counter As Int16 = 0
        If temp_data(0) = 6 Then
            While packet_counter < IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 2))
                'Debug.WriteLine(IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 2)))
                compressed_length = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 4 + compressed_length_old + (packet_counter * 2)))
                If compressed_length > 0 And compressed_length <= 512 Then
                    Dim compressed_data(compressed_length - 1) As Byte
                    Try
                        Array.Copy(temp_data, 6 + compressed_length_old + (packet_counter * 2), compressed_data, 0, compressed_length)
                    Catch ex As Exception
                        Exit While
                    End Try
                    Try
                        decompress_data = lzo_Currency.Decompress(compressed_data)
                        If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(decompress_data, 18)) = 7208 Then
                            Call process_Currency_7208(decompress_data, True)
                        End If

                    Catch ex As Exception
                        Exit While
                    End Try
                ElseIf compressed_length = 0 Then
                    'MsgBox(IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))))
                    If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (18 + 6) + compressed_length_old + (packet_counter * 2))) = 7208 Then
                        Call process_Currency_7208(temp_data, False)
                    End If
                    'ElseIf IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, (16 + 6) + compressed_length_old + (packet_counter * 2))) = 6501 Then
                    '    'Call process_fo_6501(temp_data)
                    'End If
                End If
                If compressed_length > 504 Then Exit While
                compressed_length_old += compressed_length
                packet_counter += 1
            End While
        End If

        'Catch ex As Threading.ThreadAbortException
        '    Threading.Thread.ResetAbort()
        'End Try
    End Sub

    Private Function HxTOStr(ByVal InxName As String) As String
        Dim result As String
        Dim inx() As String
        inx = InxName.Split("-")
        result = ""
        For h As Integer = 0 To inx.Length - 1
            result = result & Chr(System.Convert.ToInt32(inx(h), 16))
        Next
        Return result
    End Function

    Public Function hton(ByVal data() As Byte, ByVal int As Integer, ByVal s As String) As String
        Return System.BitConverter.ToChar(data, int).ToString
    End Function

    Public Function hton(ByVal data() As Byte, ByVal int As Integer) As String
        Return IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, int)).ToString
    End Function

    ''' <summary>
    ''' GSub_Stop_Broadcast
    ''' </summary>
    ''' <remarks>This function is used to stop thread and close socket connection</remarks>
    Public Sub GSub_Stop_Broadcast()
        Try
            ThreadReceive_fo.Abort()
            ThreadReceive_cm.Abort()
            ThreadReceive_Currency.Abort()
        Catch ex As Exception
        End Try
        Try
            multicastListener_fo.Close()
            multicastListener_fo = Nothing
        Catch ex As Threading.ThreadAbortException
            'Threading.Thread.ResetAbort()
        Catch ex As Exception
        End Try
        Try
            multicastListener_cm.Close()
            multicastListener_cm = Nothing
        Catch ex As Threading.ThreadAbortException
            'Threading.Thread.ResetAbort()
        Catch ex As Exception
        End Try
        Try
            multicastListener_Currency.Close()
            multicastListener_Currency = Nothing
        Catch ex As Threading.ThreadAbortException
            'Threading.Thread.ResetAbort()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub GSub_Start_Broadcast()
        Try
            Call initialize_fo_broadcast()
        Catch ex As Exception
            MsgBox("Cannot connect to F&O UDP server.", MsgBoxStyle.Exclamation)
        End Try
        Try
            Call initialize_cm_broadcast()
        Catch ex As Exception
            MsgBox("Cannot connect to Cash UDP server.", MsgBoxStyle.Exclamation)
        End Try
        Try
            Call initialize_Currency_broadcast()
        Catch ex As Exception
            MsgBox("Cannot connect to Currency UDP server.", MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Public Sub CheckJL_Connection()
        Dim telnetServerIp As String
        Dim telnetPort As Integer
        telnetServerIp = "192.168.98.11"

        telnetPort = 1400
        If NetMode = "JL" Then
            Try
                Dim Cclient As New Net.Sockets.TcpClient(RegServerIP.Split(",")(0), RegServerIP.Split(",")(1))
                Cclient.Client.Disconnect(True)
                Cclient.Close()
                Cclient = Nothing
            Catch ex As Exception
                Dim analysis1 As New frmSettings
                For Each frm As Form In MDI.MdiChildren
                    frm.Close()
                Next
                'analysis1.ShowDialog()
                analysis1.ShowForm(3)
            End Try
        End If
    End Sub
    Public Sub CheckTelNet_Connection()
        Dim starttik As Long = System.Environment.TickCount
        If FLG_REG_SERVER_CONN = True Then
            Exit Sub
        End If



        If bool_IsTelNet = True Then Exit Sub
        Try
            Dim telnetServerIp As String = ""
            Dim telnetPort As Integer = 23
            If NetMode = "UDP" Then
                bool_IsTelNet = True
                Exit Sub
            ElseIf NetMode = "TCP" Then

                telnetServerIp = sSQLSERVER.Split(",")(0)
                If sSQLSERVER.Split(",").Length <= 1 Then
                    telnetPort = 1433 ' 1433
                Else
                    telnetPort = sSQLSERVER.Split(",")(1)
                End If
            ElseIf NetMode = "JL" Then

                telnetServerIp = "192.168.98.11"

                telnetPort = 1400 ' 1433

            ElseIf NetMode = "NET" Then
                telnetServerIp = nseindia '123.201.167.42
                telnetPort = 80
            ElseIf NetMode = "API" And flgAPI_K <> "TCP" Then
                Try
                    'If My.Computer.Network.IsAvailable Then
                    '    Dim clienta As New TcpClient("nimblestream.lisuns.com", "4525")
                    '    clienta.Client.Disconnect(True)
                    '    clienta.Close()
                    '    clienta = Nothing
                    bool_IsTelNet = True
                    'End If
                Catch ex As Exception
                    bool_IsTelNet = False
                End Try
            ElseIf NetMode = "API" And (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") Then
                Try
                    telnetServerIp = "103.228.79.44"
                    If gstr_ProductName = "OMI" Then
                        telnetPort = 1400 ' 1433
                    Else

                        telnetPort = 1403 ' 1433
                    End If

                    bool_IsTelNet = True
                    'End If
                Catch ex As Exception
                    bool_IsTelNet = False
                End Try
                Exit Sub
            End If


            Dim client As New TcpClient(telnetServerIp, telnetPort)
            client.Client.Disconnect(True)
            client.Close()
            client = Nothing

            'MessageBox.Show("Server is reachable")
            If NetMode = "NET" And AppLicMode = "NETLIC" Then
lbl:
                Try
                    Dim Cclient As New Net.Sockets.TcpClient(RegServerIP.Split(",")(0), RegServerIP.Split(",")(1))
                    Cclient.Client.Disconnect(True)
                    Cclient.Close()
                    Cclient = Nothing
                Catch ex As Exception
                    If Not ex.Message.Contains("No connection could be made because the target machine actively refused it") Then
                        If MsgBox("Internet is Down.       " & vbCrLf & "Do you want to retry?", MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                            bool_IsTelNet = False
                            GoTo lbl
                        Else
                            bool_IsTelNet = False
                            End
                        End If
                    End If
                End Try
            End If

            bool_IsTelNet = True
        Catch ex As Exception
            '10061
            If ex.Message.Contains("No connection could be made because the target machine actively refused it") Then
                bool_IsTelNet = True
            Else
                bool_IsTelNet = False
            End If


            ' temp = temp + 1
            'If temp = 1 And NetMode = "TCP" Then

            '    MsgBox("TCP Connection Lost")

            'End If
            ''MessageBox.Show("Could not reach server")
        End Try

        Dim Endtik As Long = System.Environment.TickCount

    End Sub

    Public Function CheckTelNet() As Boolean
        Dim Result As Boolean = True
        Try
            Dim telnetServerIp As String = ""
            Dim telnetPort As Integer = 23
            If NetMode = "UDP" Then
                bool_IsTelNet = True
                Exit Function
            ElseIf NetMode = "TCP" Then
                telnetServerIp = sSQLSERVER.Split(",")(0)
                If sSQLSERVER.Split(",").Length <= 1 Then
                    telnetPort = 1433 ' 1433
                Else
                    telnetPort = sSQLSERVER.Split(",")(1)
                End If

            ElseIf NetMode = "NET" Then
                telnetServerIp = nseindia
                telnetPort = 80
            ElseIf NetMode = "JL" Then
                telnetServerIp = sSQLSERVER.Split(",")(0)
                If sSQLSERVER.Split(",").Length <= 1 Then
                    telnetPort = 1433 ' 1433
                Else
                    telnetPort = sSQLSERVER.Split(",")(1)
                End If
            ElseIf NetMode = "API" And (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") Then
                telnetServerIp = sSQLSERVER.Split(",")(0)
                If sSQLSERVER.Split(",").Length <= 1 Then
                    telnetPort = 1433 ' 1433
                Else
                    telnetPort = sSQLSERVER.Split(",")(1)
                End If

            End If


            Dim client As New TcpClient(telnetServerIp, telnetPort)
            client.Client.Disconnect(True)
            client.Close()
            client = Nothing

            'MessageBox.Show("Server is reachable")
            If NetMode = "TCP" Or NetMode = "JL" Then
                bSqlValidated = ChkSQLConn(sSQLSERVER, sDATABASE, sUSERNAME, sPASSWORD)
                Result = bSqlValidated
            ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
                bSqlValidated = ChkSQLConn(sSQLSERVER, sDATABASE, sUSERNAME, sPASSWORD)
                Result = bSqlValidated
                bool_IsTelNet = Result
            End If



            Return Result
        Catch ex As Exception
            If ex.Message.Contains("No connection could be made because the target machine actively refused it") Then
                Return True
            Else
                Return False
            End If


        End Try

    End Function
    Private Function ChkSQLConn(ByVal strTCPServerName As String, ByVal strDb As String, ByVal strUser As String, ByVal strPass As String) As Boolean
        Dim StrConn As String = ""
        StrConn = " Data Source=" & strTCPServerName & ";Initial Catalog=" & strDb & ";User ID=" & strUser & ";Password=" & strPass & ";Application Name=" & "VH_" & strTCPServerName & "_Test"
        Dim ConTest As New System.Data.SqlClient.SqlConnection(StrConn)
        Try
            ConTest.Open()
            ConTest.Close()
            ConTest.Dispose()
            Return True
        Catch ex As Exception
            WriteLog("Proc::CheckSqlConnection:-->" & vbCrLf & ex.Message)
            ConTest.Dispose()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' process_OI_7202
    ''' </summary>
    ''' <param name="data"></param>
    ''' <remarks>This method call to decompress Open-Interest receiving byte</remarks>
    Private Sub process_OI_7202(ByVal data() As Byte)

        'If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 44)) = 468 Then
        Dim token_no As Long, MarketType As Short, FillPrice As Long, FillVolume As Long, OpenInterest As Long, DayHiOl As Long, DayLoOl As Long, LTT As Long
        'Dim StrOISql As String = ""

        For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 48)) - 1) * 26 Step 26
            Try
                token_no = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 50 + j))
                'MarketType = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 54 + j))
                'FillPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 56 + j))
                'FillVolume = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 60 + j))
                'OpenInterest = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 64 + j))
                OpenInterest = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt64(data, 64 + j))
                'DayHiOl = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 68 + j))
                'DayLoOl = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 72 + j))
                'LTT = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 76 + j))

                Try
                    'If FOconn.State = ADODB.ObjectStateEnum.adStateClosed Then FO_open_connection()
                    'Dim a As Integer
                    'While Not FOconn.State = ObjectStateEnum.adStateOpen
                    '    a = a + 1
                    'End While
                    'conn.Execute("Sp_InsertOIFo " & token_no & "," & MarketType & "," & FillPrice & "," & FillVolume & "," & OpenInterest & "," & DayHiOl & "," & DayLoOl)
                    'FOconn.Execute()

                    'OIQQue.Enqueue("Exec Sp_014 " & token_no & "," & ED.EOI(MarketType) & "," & ED.EOI(FillPrice) & "," & ED.EOI(FillVolume) & "," & ED.EOI(OpenInterest) & "," & ED.EOI(DayHiOl) & "," & ED.EOI(DayLoOl) & "," & ED.EOI(LTT) & ";" & vbCrLf)

                    If OpenInterestprice.Contains(token_no) Then
                        OpenInterestprice.Item(token_no) = OpenInterest
                    Else
                        OpenInterestprice.Add(token_no, OpenInterest)
                    End If

                Catch ex As Exception
                    WriteLog("Proc::process_OI_7202::-->" & vbCrLf & ex.Message)
                End Try
            Catch ex As Threading.ThreadAbortException
                WriteLog("Proc::process_OI_7202::-->" & vbCrLf & ex.Message)
                Threading.Thread.ResetAbort()
            End Try
        Next
        'OIUpd(StrOISql)
        'OIQQue.Enqueue(StrOISql)
        'End If
    End Sub


    Private Sub process_OI_17202(ByVal data() As Byte)

        'If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 44)) = 468 Then
        Dim token_no As Long, MarketType As Short, FillPrice As Long, FillVolume As Long, OpenInterest As Long, DayHiOl As Long, DayLoOl As Long, LTT As Long
        'Dim StrOISql As String = ""

        For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 48)) - 1) * 26 Step 26
            Try
                token_no = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 50 + j))
                'MarketType = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 54 + j))
                'FillPrice = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 56 + j))
                FillVolume = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 60 + j))
                'OpenInterest = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 64 + j))
                OpenInterest = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt64(data, 64 + j))
                'DayHiOl = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 68 + j))
                'DayLoOl = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 72 + j))
                'LTT = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 76 + j))

                Try
                    'If FOconn.State = ADODB.ObjectStateEnum.adStateClosed Then FO_open_connection()
                    'Dim a As Integer
                    'While Not FOconn.State = ObjectStateEnum.adStateOpen
                    '    a = a + 1
                    'End While
                    'conn.Execute("Sp_InsertOIFo " & token_no & "," & MarketType & "," & FillPrice & "," & FillVolume & "," & OpenInterest & "," & DayHiOl & "," & DayLoOl)
                    'FOconn.Execute()

                    'OIQQue.Enqueue("Exec Sp_014 " & token_no & "," & ED.EOI(MarketType) & "," & ED.EOI(FillPrice) & "," & ED.EOI(FillVolume) & "," & ED.EOI(OpenInterest) & "," & ED.EOI(DayHiOl) & "," & ED.EOI(DayLoOl) & "," & ED.EOI(LTT) & ";" & vbCrLf)

                    If OpenInterestprice.Contains(token_no) Then
                        OpenInterestprice.Item(token_no) = OpenInterest
                        If token_no = 45248 Then
                            clsGlobal.mPerf.SetFileName("udpOpenOI")
                            clsGlobal.mPerf.WriteLogStr("UDP LiveOi : " & token_no.ToString() & " : " & OpenInterest.ToString() & ": FV :" & FillVolume.ToString())
                        End If
                    Else
                        OpenInterestprice.Add(token_no, OpenInterest)
                    End If

                Catch ex As Exception
                    WriteLog("Proc::process_OI_17202::-->" & vbCrLf & ex.Message)
                End Try
            Catch ex As Threading.ThreadAbortException
                WriteLog("Proc::process_OI_17202::-->" & vbCrLf & ex.Message)
                Threading.Thread.ResetAbort()
            End Try
        Next
        'OIUpd(StrOISql)
        'OIQQue.Enqueue(StrOISql)
        'End If
    End Sub

End Module
