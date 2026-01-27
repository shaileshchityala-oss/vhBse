Imports System.Net
Imports System.Net.Sockets
Public Class Frm_UDPSetting
    Private skt_multicastListener_bc As Socket
    Private thr_ThreadReceive_bc As System.Threading.Thread
    Dim Var_UDPIP As String = ""
    Dim Var_UDPPort As String = ""
    Private Delegate Sub Delegate_with_Signle_Para(ByVal valMsg As Boolean)

    Private obj_Del_Disable_Form As New Delegate_with_Signle_Para(AddressOf Sub_Disable_Form)

    ''' <summary>
    ''' Reade UDP Connection file and assign IP Address and Port from File
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_UDPSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
               
            Dim Str() As String = GFun_Get_Svr_UDP_IP_Port()
        If Not Str Is Nothing Then
            txtUDPIPAddress.IPaddress = Str(0) & ""
            txtUDPPort.Text = Str(1) & ""
        End If

    End Sub

    ''' <summary>
    ''' Connect UDP Socket and Check whether broadcast receiving or not
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        If Check_Validate() = False Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Try
            skt_multicastListener_bc = New Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
            skt_multicastListener_bc.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1)
            skt_multicastListener_bc.Bind(New IPEndPoint(IPAddress.Any, Var_UDPPort))
            skt_multicastListener_bc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(IPAddress.Parse(Var_UDPIP), IPAddress.Any))
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox("Please enter valid UDP IP Address Or Port !!", MsgBoxStyle.Exclamation)
            txtUDPIPAddress.Focus()
            Exit Sub
        End Try

        thr_ThreadReceive_bc = New System.Threading.Thread(AddressOf Sub_ReceiveMessages_Broadcast)
        thr_ThreadReceive_bc.Name = "thr_UdpTest_RecBcast"
        thr_ThreadReceive_bc.Start()

    End Sub

    ''' <summary>
    ''' Use for Test whether data receiving from UDP 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Sub_ReceiveMessages_Broadcast()
        Try
            Me.Invoke(obj_Del_Disable_Form, False)
            Dim bteReceiveData_UDP(512) As Byte
            skt_multicastListener_bc.ReceiveTimeout = 1000
            skt_multicastListener_bc.Receive(bteReceiveData_UDP)
            If bteReceiveData_UDP.Length > 0 Then
                MsgBox("Test Connection Successful  ", MsgBoxStyle.Information)
            Else
                MsgBox("Test Connection Fail !! ", MsgBoxStyle.Critical)
            End If
        Catch EX1 As Exception
            MsgBox("Test Connection Fail !! ", MsgBoxStyle.Critical)
        End Try
        Try

        
            Me.Invoke(obj_Del_Disable_Form, True)

            If skt_multicastListener_bc.Connected = True Then
                skt_multicastListener_bc.Disconnect(True)
                skt_multicastListener_bc = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Enable and Disable Form Control
    ''' </summary>
    ''' <param name="Bool"></param>
    ''' <remarks></remarks>
    Private Sub Sub_Disable_Form(ByVal Bool As Boolean)
        On Error Resume Next
        txtUDPIPAddress.Enabled = Bool
        txtUDPPort.Enabled = Bool
        btnTest.Enabled = Bool
        btnOK.Enabled = Bool
        btnExit.Enabled = Bool
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Application from UDP setting
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' Check validation of Text box
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Check_Validate() As Boolean
        Var_UDPIP = ""
        Dim Str_Arr() As String = txtUDPIPAddress.IPaddress.Split(".")
        For Each str As String In Str_Arr
            If str.Trim = "" Or Val(str) > 255 Then
                MsgBox("Please enter valid UDP IP Address !!", MsgBoxStyle.Exclamation)
                txtUDPIPAddress.Focus()
                Return False
            Else
                If Var_UDPIP <> "" Then Var_UDPIP &= "."
                Var_UDPIP &= Val(str.Trim)
            End If
        Next
        If Val(txtUDPPort.Text).ToString.Length < 4 Then
            MsgBox("Please enter valid UDP Port !!", MsgBoxStyle.Exclamation)
            txtUDPPort.Focus()
            Return False
        Else
            Var_UDPPort = Val(txtUDPPort.Text).ToString
        End If

        Return True
    End Function

    ''' <summary>
    ''' Apply UDP Setting
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If Check_Validate() = False Then Exit Sub

        Call GSub_Save_Srv_UDP_IP_Port(txtUDPIPAddress.IPaddress, txtUDPPort.Text)

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

End Class