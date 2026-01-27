Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text

Public Class CLS_Check_UDP_Licence
#Region "LocalVariable"
    Dim obj_UDP_IP_Address As IPAddress
    Dim int_UDP_Port As Integer

    Dim obj_IP_Address_TCP As IPAddress
    Dim int_Port_TCP As Integer
    Dim Str_Master_Key As String = "askesquarebusehs"

    Public Var_Trans_Code As String = ""
    Dim Var_Retry_Connection_Cnt As Integer = 0
    Dim Var_Rec_Trans_Code As String

#End Region

#Region "ObjDeligets"
    Dim obj_DelSet_Svr_Conn_Status As New GDelegate_with_Double_Para(AddressOf MDI.Sub_Set_ServerConnect_Status)
    Dim obj_DelSet_Conn_Msg As New GDelegate_with_Double_Para(AddressOf Sub_Set_Conn_Msg)
#End Region

    <DllImport("EncryptDecript.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Function Encrypt(ByVal Encryptstr() As Char, ByVal EncryptKey() As Char, ByVal size As Integer) As String
    End Function
    <DllImport("EncryptDecript.dll", CallingConvention:=CallingConvention.Cdecl)> _
   Private Shared Function Decrypt(ByVal Decryptchr() As Char, ByVal EncryptKey() As Char, ByVal size As Integer) As String
    End Function

    Public Sub GSub_Chk_Lic_From_Server()
        REM Check Recursive then until UDP data not receive from Server
lbl_Rec_UDP_Data:
        REM Receive Tcp Server Ip Port
        Dim obj_Rec_Data As Object = Sub_Receive_Broadcast()
        If obj_Rec_Data Is Nothing Then
            obj_DelSet_Conn_Msg.Invoke(False, "UDP server connection not establish.")
            Threading.Thread.Sleep(1000)
            GoTo lbl_Rec_UDP_Data
        Else
            obj_DelSet_Conn_Msg.Invoke(False, "Connecting...")
        End If
        REM ENd

        REM Decrypt UDP data and Get TCP  IP Address and Port
        If Sub_Decrypt_UDP_Receive_Data(CType(obj_Rec_Data, Byte())) = False Then
            obj_DelSet_Conn_Msg.Invoke(False, "UDP Server Data Invalid !!")
            GoTo lbl_Rec_UDP_Data
        End If


        _Client_Key = DateDiff(DateInterval.Second, CDate("01-01-1980"), Now).ToString
        REM End

        '2300 For Connection Test
        '2302 For verification
        '2303 For Disconnection

        REM Set Connection Transaction Code and Try to Connection TCP Listener
        Var_Trans_Code = "2300"
        If Sub_TCP_Client_Connection() = False Then
            If Var_Retry_Connection_Cnt = 0 Then Call sub_TCP_Retry_Connection() REM Recuresive check is TCP connection Establish or not
        Else

            Dim state As New StateObject()
            state.workSocket = _Client
            REM Receive TCP Server Data Async
            _Client.BeginReceive(obj_Rec_byteBuffer, 0, obj_Rec_byteBuffer.Length, SocketFlags.None, AddressOf DoRead_TCP_Data, state)
            REM ENd

            Call sub_Send_Client_Info_to_Server(Var_Trans_Code) REM Client Information to Server
        End If
        REM End
    End Sub

#Region "UDP Connection & Receiving Data"

    ''' <summary>
    ''' GetUDP IP From Text Read Array
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GFun_Get_UDP_IP_Port() As Boolean
        Dim str() As String = GFun_Get_Svr_UDP_IP_Port()
        Try
            If IPAddress.TryParse(str(0).Replace(" ", ""), obj_UDP_IP_Address) = False Then
                Return False
            End If
            int_UDP_Port = CInt(str(1))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Receive Tcp Server Ip Port
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Sub_Receive_Broadcast() As Byte()
        Dim skt_multicastListener_bc As Socket
        Try
            skt_multicastListener_bc = New Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
            skt_multicastListener_bc.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1)
            skt_multicastListener_bc.Bind(New IPEndPoint(IPAddress.Any, int_UDP_Port))
            skt_multicastListener_bc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(obj_UDP_IP_Address, IPAddress.Any))
        Catch EX1 As Exception
            GSub_Write_ErrorLog("CLS_Check_UDP_Licence :: Sub_Receive_Broadcast ::" & EX1.Message & vbCrLf & "Network Connection unplugged")
            Return Nothing
        End Try

        Try
            Dim bteReceiveData(95) As Byte
            skt_multicastListener_bc.ReceiveTimeout = 5000
            skt_multicastListener_bc.Receive(bteReceiveData)
            If bteReceiveData.Length > 0 Then
                Return bteReceiveData
            Else
                Return Nothing
            End If
        Catch EX1 As Exception
            GSub_Write_ErrorLog("CLS_Check_UDP_Licence :: Sub_Receive_Broadcast ::" & EX1.Message)
            Return Nothing
        Finally
            If skt_multicastListener_bc.Connected = True Then
                skt_multicastListener_bc.Disconnect(True)
                skt_multicastListener_bc = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' REM Decrypt UDP data and Get TCP  IP Address and Port
    ''' </summary>
    ''' <param name="bt_Arr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Sub_Decrypt_UDP_Receive_Data(ByVal bt_Arr() As Byte) As Boolean
        Try
            Dim str_Encry_Data As String = ""
            For Each bt As Byte In bt_Arr
                str_Encry_Data &= Chr(bt).ToString()
            Next
            Dim str_Encry_TCP_IP As String = str_Encry_Data.Substring(0, 16)
            Dim str_Encry_TCP_Port As String = str_Encry_Data.Substring(16, 16)
            Dim str_Encry_Key As String = str_Encry_Data.Substring(32, 32)

            Dim Decry_Key As String = Decrypt(str_Encry_Key, Str_Master_Key, str_Encry_Key.Length) 'Str_Master_Key.Length) 
            'Decry_Key = Decry_Key.PadRight(32)
            Dim Decry_Key_Chars(31) As Char
            If Decry_Key = "0" Then
                For Tempi As Integer = 0 To 31
                    If Tempi > Decry_Key.ToCharArray.Length - 1 Then
                        Decry_Key_Chars(Tempi) = Chr(32)
                    Else
                        Decry_Key_Chars(Tempi) = Decry_Key.ToCharArray()(Tempi)
                    End If
                Next
            Else
                Decry_Key_Chars = Decry_Key.ToCharArray
            End If

            Dim str_Dec_TCP_IP As String = Decrypt(str_Encry_TCP_IP, Decry_Key_Chars, str_Encry_TCP_IP.Length)
            Dim Str_Dec_TCP_Port As String = Decrypt(str_Encry_TCP_Port, Decry_Key_Chars, str_Encry_TCP_Port.Length)

            str_Dec_TCP_IP = str_Dec_TCP_IP.Substring(0, Math.Min(str_Dec_TCP_IP.Length, 15)).Trim
            Str_Dec_TCP_Port = Val(Str_Dec_TCP_Port.Substring(0, Math.Max(Str_Dec_TCP_Port.Length - 2, 4)).Trim) + 1

            obj_IP_Address_TCP = IPAddress.Parse(str_Dec_TCP_IP)
            int_Port_TCP = CInt(Str_Dec_TCP_Port)
            Return True
        Catch ex As Exception
            MsgBox(" CLS_Check_UDP_Licence :: Sub_Decrypt_UDP_Receive_Data :: " & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "TCP Connection"

    Dim _Client As Socket
    Dim _Client_Key As String
    Dim obj_Rec_byteBuffer(1023) As Byte
    Dim obj_Encrypt As New CLS_Encrypt_Data

    Dim Thr_Varify_TCP_Connection As New Threading.Thread(AddressOf GSub_Varify_TCP_Conn_Cont)

    Private Function Sub_TCP_Client_Connection() As Boolean
        Try
            Dim ipendpoint As IPEndPoint = New IPEndPoint(obj_IP_Address_TCP, int_Port_TCP) ' Assign port and host
            _Client = New Socket(Sockets.AddressFamily.InterNetwork, Sockets.SocketType.Stream, Sockets.ProtocolType.Tcp)
            _Client.Connect(ipendpoint)
            Return True
        Catch ex As Exception
            GSub_Write_ErrorLog("CLS_Check_UDP_Licence :: Sub_TCP_Client_Connection ::" & ex.Message)
            Call Sub_Set_Server_Disconnect("Remote server connection closed")
            Return False
        End Try
    End Function

    Private Sub DoRead_TCP_Data(ByVal asyn As IAsyncResult)
        Try
            Dim state As StateObject = CType(asyn.AsyncState, StateObject)
            Dim client As Socket = state.workSocket

            Dim int_doread As Integer = _Client.EndReceive(asyn)
            If (int_doread > 0) Then
                REM Receive Data
                Call Sub_TCP_Receive_Data(obj_Rec_byteBuffer)
                REM End
                _Client.BeginReceive(obj_Rec_byteBuffer, 0, obj_Rec_byteBuffer.Length, SocketFlags.None, New AsyncCallback(AddressOf DoRead_TCP_Data), state)
            Else

            End If
            'CType(obj_state, Socket).Close()
        Catch ex As Exception
            Try
                GSub_Write_ErrorLog("CLS_Check_UDP_Licence :: DoRead_TCP_Data ::" & ex.Message)
                Call Sub_Set_Server_Disconnect("Remote server connection closed")
                If Var_Retry_Connection_Cnt = 0 Then Call sub_TCP_Retry_Connection() REM Recuresive check is TCP connection Establish or not
                Exit Sub
            Catch ex1 As Exception
                MsgBox("Error" & ex1.ToString)
            End Try
        End Try
    End Sub

   

    Private Sub Sub_TCP_Client_Disconnect()
        Try
            If _Client.Connected = True Then
                _Client.Close()
                _Client.Disconnect(True)
                'bool_IsServerConnected = False
            End If
            If Not _Client Is Nothing Then _Client = Nothing
        Catch ex As Exception
            GSub_Write_ErrorLog("CLS_Check_UDP_Licence :: Sub_TCP_Client_Disconnect ::" & ex.Message)
        End Try
    End Sub

    Private Sub sub_TCP_Retry_Connection()
        If Var_Rec_Trans_Code = "2303" Then Exit Sub
        Var_Retry_Connection_Cnt = 1
        Call Sub_TCP_Client_Disconnect()
        If Sub_TCP_Client_Connection() = False Then
            Threading.Thread.Sleep(30000)
            Call sub_TCP_Retry_Connection() REM Recuresive check is TCP connection Establish or not
        Else
            'bool_IsServerConnected = True
            Dim state As New StateObject()
            state.workSocket = _Client
            _Client.BeginReceive(obj_Rec_byteBuffer, 0, obj_Rec_byteBuffer.Length, SocketFlags.None, AddressOf DoRead_TCP_Data, state)
            Call sub_Send_Client_Info_to_Server(Var_Trans_Code)
            Var_Retry_Connection_Cnt = 0
        End If
    End Sub
    Private Function Sub_Check_Is_TCP_DataReceive() As Boolean
        REM Waiting for TCP Message receive
        Try
            Dim cnt_Check As Integer = 0
            While _Client.Available = 0
                Threading.Thread.Sleep(1000)
                cnt_Check += 1
                If cnt_Check > 50 Then
                    Return False
                End If
            End While
            Return True
        Catch ex As Exception
            Throw New Exception("CLS_Check_UDP_Licence :: Sub_Check_Is_TCP_DataReceive ::" & ex.Message)
        End Try
        REM End
    End Function

    Private Sub sub_Send_Client_Info_to_Server(ByVal Str_Trans_Code As String)
        Dim encoding As New System.Text.UTF8Encoding()
        Dim Lng_Length As Long = 36
        Dim Str_SoftName As String = "Vol Hedge"
        'Dim Str_SoftName As String = "EasyRMS"
        Dim Str_Client_Key As String = ""

        Dim BLen() As Byte = Fun_GetBytes(Lng_Length.ToString().PadRight(4))
        Dim BSname() As Byte = Fun_GetBytes(Str_SoftName.PadRight(50))
        Dim BClientKey() As Byte = Fun_GetBytes(Str_Client_Key.PadRight(74))
        Dim byte_Soft_Data(BLen.Length + BSname.Length + BClientKey.Length - 1) As Byte
        Dim i As Integer = 0
        For Each bt As Byte In BLen
            byte_Soft_Data(i) = bt
            i += 1
        Next
        For Each bt As Byte In BSname
            byte_Soft_Data(i) = bt
            i += 1
        Next
        For Each bt As Byte In BClientKey
            byte_Soft_Data(i) = bt
            i += 1
        Next
        Dim Str_Org_Data As String = ""
        For Each bt As Byte In byte_Soft_Data
            If bt = 32 Then
                Str_Org_Data += Chr(32)
            Else
                Str_Org_Data += Chr(bt)
            End If
        Next

        Dim str_Encrypt_Data As String = obj_Encrypt.Encrypt_Data(Str_Org_Data)
        Dim byt_Encrypt_Data() As Byte = Fun_GetBytes(str_Encrypt_Data)
        Dim str_Encrypt_Data1 As String = obj_Encrypt.Decrypt_Data(str_Encrypt_Data)

        'str_Encrypt_Data = str_Encrypt_Data.Substring(0, str_Encrypt_Data.Length - 2)
        'Dim strtemp2 As String = Decrypt(str_Encrypt_Data, _Client_Key, _Client_Key.Length)

        Dim str_Encrypt_Trans_Code As String = obj_Encrypt.Encrypt_Data(Str_Trans_Code)
        Dim byt_Encrypt_Trans_Code() As Byte = Fun_GetBytes(str_Encrypt_Trans_Code)

        Dim str_Encrypt_Key As String = obj_Encrypt.Encrypt_Data(_Client_Key.PadRight(50))
        Dim byt_Encrypt_Key() As Byte = Fun_GetBytes(str_Encrypt_Key)

        Dim md5Hasher As New MD5CryptoServiceProvider
        Dim byt_Check_Sum_Chunk() As Byte = md5Hasher.ComputeHash(byt_Encrypt_Data)

        Dim Packlen = IPAddress.HostToNetworkOrder(512)
        Dim byt_Length() As Byte = System.BitConverter.GetBytes(Packlen)

        Dim byt_Send_Data(595) As Byte
        Dim int_byte_cnt As Integer = 0

        REM Total Length Update 
        For int_i As Integer = 0 To 3
            If int_i > byt_Length.Length - 1 Then
                byt_Send_Data(int_byte_cnt) = 0 'encoding.GetBytes(chr(0))(0)
            Else
                byt_Send_Data(int_byte_cnt) = byt_Length(int_i)
            End If
            int_byte_cnt += 1
        Next
        REM End

        REM Check Sum Update 
        For int_i As Integer = 0 To 15
            If int_i > byt_Check_Sum_Chunk.Length - 1 Then
                byt_Send_Data(int_byte_cnt) = 0 'encoding.GetBytes(" ")(0)
            Else
                byt_Send_Data(int_byte_cnt) = byt_Check_Sum_Chunk(int_i)
            End If
            int_byte_cnt += 1
        Next
        REM End

        REM Encrypted Key Update 
        For int_i As Integer = 0 To 31
            If int_i > byt_Encrypt_Key.Length - 1 Then
                byt_Send_Data(int_byte_cnt) = 0 'encoding.GetBytes(" ")(0)
            Else
                byt_Send_Data(int_byte_cnt) = byt_Encrypt_Key(int_i)
            End If
            int_byte_cnt += 1
        Next
        REM End

        REM Check Sum Update 
        For int_i As Integer = 0 To 31
            If int_i > byt_Encrypt_Trans_Code.Length - 1 Then
                byt_Send_Data(int_byte_cnt) = 0 'encoding.GetBytes(" ")(0)
            Else
                byt_Send_Data(int_byte_cnt) = byt_Encrypt_Trans_Code(int_i)
            End If
            int_byte_cnt += 1
        Next
        REM End

        REM Check Sum Update 
        For int_i As Integer = 0 To 511
            If int_i > byt_Encrypt_Data.Length - 1 Then
                byt_Send_Data(int_byte_cnt) = 0 'encoding.GetBytes(" ")(0)
            Else
                byt_Send_Data(int_byte_cnt) = byt_Encrypt_Data(int_i)
            End If
            int_byte_cnt += 1
        Next
        REM End

        _Client.Send(byt_Send_Data, byt_Send_Data.Length, SocketFlags.None)

    End Sub
    Private Sub Sub_TCP_Receive_Data(ByVal pbyt_Data_Receive() As Byte)
        Dim str_Encry_Data As String = ""
        Dim unc As New System.Text.UTF8Encoding
        Dim chr_arr(pbyt_Data_Receive.Length) As Char
        unc.GetString(pbyt_Data_Receive)
        Dim int_pos As Integer = 0
        For Each bt As Byte In pbyt_Data_Receive
            If bt = 0 Then
                chr_arr(int_pos) = Chr(32)
            Else
                chr_arr(int_pos) = Chr(bt)
            End If
            int_pos += 1
        Next

        str_Encry_Data = Convert.ToString(chr_arr)
        Dim int_start_Chr_cnt As Integer = 0

        int_start_Chr_cnt = 4
        Dim str_Encry_Trans_Code As String = str_Encry_Data.Substring(int_start_Chr_cnt, 32)
        int_start_Chr_cnt += str_Encry_Trans_Code.Length
        Dim str_Encry_Rec_Data As String = str_Encry_Data.Substring(int_start_Chr_cnt, 542)
        int_start_Chr_cnt += str_Encry_Rec_Data.Length

        Dim str_Decrypt_Trans_Code As String = obj_Encrypt.Decrypt_Data(str_Encry_Trans_Code).Trim
        Dim str_Decrypt_Receive_Data As String = obj_Encrypt.Decrypt_Data(str_Encry_Rec_Data)

        int_start_Chr_cnt = 0
        Dim str_IsStatus As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 4)
        int_start_Chr_cnt += str_IsStatus.Length
        Dim str_Message As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 255)

        'If str_Decrypt_Trans_Code <> "2303" And str_IsStatus.Trim = "0000" Then
        '    If Var_Trans_Code = "2300" Then Var_Trans_Code = "2302"
        'Else
        '    Call obj_DelSet_Svr_Conn_Status.Invoke(False, str_Message)
        '    If str_Decrypt_Trans_Code = "2303" Then
        '        GSub_Write_ErrorLog("Transaction Code (Disconnect from Server) :: " & str_Decrypt_Trans_Code)
        '        MsgBox(str_Message, MsgBoxStyle.Exclamation)
        '        Return
        '    ElseIf bool_IsServerConnected = False Then
        '        MsgBox(str_Message, MsgBoxStyle.Exclamation)
        '        End
        '        Return
        '    End If
        'End If

        If str_Decrypt_Trans_Code <> "2303" And str_IsStatus.Trim = "0000" Then
            If Var_Trans_Code = "2300" Then Var_Trans_Code = "2302"
        Else
            If str_Decrypt_Trans_Code = "2303" Then REM Client Disconnected by Server
                Var_Rec_Trans_Code = str_Decrypt_Trans_Code
                str_Message = "Client disconnected by Server !!"
                GSub_Write_ErrorLog("Transaction Code (Disconnect from Server) :: " & str_Decrypt_Trans_Code)
                Call obj_DelSet_Svr_Conn_Status.Invoke(False, str_Message)
                Call Sub_TCP_Client_Disconnect()
                MsgBox(str_Message, MsgBoxStyle.Exclamation)
                Exit Sub
            ElseIf bool_IsServerConnected = False Then
                Call obj_DelSet_Svr_Conn_Status.Invoke(False, str_Message)
                MsgBox(str_Message, MsgBoxStyle.Information)
                End
                Exit Sub
            End If
        End If

        int_start_Chr_cnt += str_Message.Length
        str_Message = str_Message.Trim
        Dim str_Exp_Date As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 20)
        int_start_Chr_cnt += str_Exp_Date.Length
        Dim str_Max_UDP_Date As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 20)
        int_start_Chr_cnt += str_Max_UDP_Date.Length
        Dim str_Min_UDP_Date As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 20)
        int_start_Chr_cnt += str_Min_UDP_Date.Length
        If G_VarExpiryDate <> GFun_Date_From_MMddYYYY(str_Exp_Date) Then
            G_VarExpiryDate = GFun_Date_From_MMddYYYY(str_Exp_Date)
            Call GSub_Check_Application_Expiry(Today)
        End If

        If G_bool_IsAuthanticated = False Then
            If G_bool_IsAuthanticated = False Then G_bool_IsAuthanticated = True
            REM Assign FO, CM and Curr UPD IP and Port
            Dim str_FO_UDP_IP As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 16)
            int_start_Chr_cnt += str_FO_UDP_IP.Length
            Dim str_CM_UDP_IP As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 16)
            int_start_Chr_cnt += str_CM_UDP_IP.Length
            Dim str_Curr_UDP_IP As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 16)
            int_start_Chr_cnt += str_Curr_UDP_IP.Length
            Dim Str_FO_UDP_Port As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 5)
            int_start_Chr_cnt += Str_FO_UDP_Port.Length
            Dim str_CM_UDP_Port As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 5)
            int_start_Chr_cnt += str_CM_UDP_Port.Length
            Dim str_Curr_UDP_Port As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 5)
            int_start_Chr_cnt += str_Curr_UDP_Port.Length
            Dim str_Broker_ID As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 30)
            int_start_Chr_cnt += str_Broker_ID.Length
            Dim str_Dealers As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 5)
            int_start_Chr_cnt += str_Broker_ID.Length
            Dim str_AMC As String = str_Decrypt_Receive_Data.Substring(int_start_Chr_cnt, 4)
            int_start_Chr_cnt += str_Broker_ID.Length

            If str_IsStatus.Trim = "0000" Then
                If G_bool_IsAuthanticated = False Then G_bool_IsAuthanticated = True
                Var_Trans_Code = "2302"
            Else
                'bool_IsServerConnected = False
                Call obj_DelSet_Svr_Conn_Status.Invoke(False, str_Message)
                'obj_DelCallAuthantication.Invoke(G_bool_IsAuthanticated)
                If G_bool_IsAuthanticated = False Then
                    MsgBox(str_Message, MsgBoxStyle.Exclamation)
                    End
                End If
            End If
            If G_VarExpiryDate <> GFun_Date_From_MMddYYYY(str_Exp_Date) Then
                G_VarExpiryDate = GFun_Date_From_MMddYYYY(str_Exp_Date)
                Call GSub_Check_Application_Expiry(Today)
            End If

            If str_IsStatus.Trim = "0000" And GFO_UDP_IP = "" Then
                GFO_UDP_IP = str_FO_UDP_IP.Trim
                GCM_UDP_IP = str_CM_UDP_IP.Trim
                GCurr_UDP_IP = str_Curr_UDP_IP.Trim
                GFO_UDP_Port = Fun_Small_To_End_Endien(Convert.ToInt16(Str_FO_UDP_Port.Trim))
                GCM_UDP_Port = Fun_Small_To_End_Endien(Convert.ToInt16(str_CM_UDP_Port.Trim))
                GCurr_UDP_Port = Fun_Small_To_End_Endien(Convert.ToInt16(str_Curr_UDP_Port.Trim))


                G_VarNoOfDealer = Val(str_Dealers)
                G_VarIsAMC = Convert.ToBoolean(Val(str_AMC))
            End If
            'If GVar_BrokerId <> "" Then
            '    GVar_BrokerId &= ","
            'End If
            'GVar_BrokerId &= "'" & ArrBrokerId(I).Replace("'", "") & "'"

            REM End
            obj_DelSet_Svr_Conn_Status.Invoke(True, "Server Connected") REM Set Connection Status to MDI form
        End If

        GSub_Write_ErrorLog("Receive Data From Server.")

        REM Thread Start for TCP Connection Verify 
        Thr_Varify_TCP_Connection = New Threading.Thread(AddressOf GSub_Varify_TCP_Conn_Cont)
        Thr_Varify_TCP_Connection.Name = "thr_verifyTcpCon"
        Thr_Varify_TCP_Connection.Start()
        REM End
    End Sub

    Private Sub GSub_Varify_TCP_Conn_Cont()
        If Var_Rec_Trans_Code = "2303" Then Exit Sub
        Try
            Threading.Thread.Sleep(60000)
            Call sub_Send_Client_Info_to_Server(Var_Trans_Code)
        Catch ex As Exception
            GSub_Write_ErrorLog("CLS_Check_UDP_Licence :: GSub_Varify_TCP_Conn_Cont ::" & ex.Message)
            Call Sub_Set_Server_Disconnect("Remove Server Connection Close ")
            If Var_Retry_Connection_Cnt = 0 Then Call sub_TCP_Retry_Connection() REM Recuresive check is TCP connection Establish or not
        End Try
    End Sub

#End Region

    Private Function Fun_GetBytes(ByVal str As String) As Byte()
        Dim byt(str.Length - 1) As Byte
        Dim cnt As Integer = 0
        For Each ch As Char In str.ToCharArray
            byt(cnt) = Asc(ch)
            cnt += 1
        Next
        Return byt
    End Function

    Private Function Fun_Small_To_End_Endien(ByVal port As Short) As Integer
        Dim longValue As Short = IPAddress.HostToNetworkOrder(port)
        Dim buffer() As Byte = New Byte(5) {}
        Dim buffer1() As Byte = BitConverter.GetBytes(port)
        Array.Copy(buffer1, buffer, BitConverter.GetBytes(port).Length)
        Dim num3 As Byte = buffer(0)
        buffer(0) = buffer(1)
        buffer(1) = num3
        buffer(2) = 0
        buffer(3) = 0

        Return BitConverter.ToInt32(buffer, 0).ToString()
    End Function

    Private Sub Sub_Set_Server_Disconnect(ByVal Error_Message As String)
        'bool_IsServerConnected = False
        If Var_Rec_Trans_Code = "2303" Then Exit Sub
        obj_DelSet_Svr_Conn_Status.Invoke(False, Error_Message)
        Var_Trans_Code = "2300"
    End Sub

End Class

Public Class StateObject
    Public workSocket As Socket = Nothing
    Public BufferSize As Integer = 32767
    Public buffer(32767) As Byte
    Public sb As New StringBuilder()
End Class