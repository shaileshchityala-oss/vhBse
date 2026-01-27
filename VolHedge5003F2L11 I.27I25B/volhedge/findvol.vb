Imports System
Imports System.Net.Sockets
Imports System.Net.Sockets.Socket
Imports System.Net
Imports System.IO
Imports Volhedge.OptionG
Public Class findvol
    Public Shared chkfindvol As Boolean
    'Dim mObjData As New DataAnalysis.AnalysisData
    Dim objVol As New findvolitility
    Dim masterdata As DataTable = New DataTable
    Dim objTrad As New trading
    Dim cmbheight As Boolean = False
    Dim cmbh As Integer
    Dim voltable As New DataTable
    Dim drow As DataRow

    'Dim obj_get_price As New get_price.get_price("M8HUM1T3Q15R9L")
    'Public multicastListener_fo As Socket
    'Public multicastListener_cm As Socket
    'Public ThreadReceive_fo As System.Threading.Thread
    'Public ThreadReceive_cm As System.Threading.Thread
    Dim Mrateofinterast As Double = 0
    Dim buyprice1 As New Hashtable
    Dim saleprice1 As New Hashtable
    Dim ltpprice As New Hashtable
    Dim fbuyprice As New Hashtable
    Dim fsaleprice As New Hashtable
    Dim fltpprice As New Hashtable
    Dim futarray As New ArrayList
    Dim futall As New ArrayList
    Dim cparray As New ArrayList
    Dim scripttable As New DataTable
    Dim fdv As DataView
    Dim maxmin As New DataTable
    Dim thrworking As Boolean = True
    'for tcp clietn -start
    Dim localEndPoint As IPEndPoint
    'Dim server As Socket
    'Public Thread_data As System.Threading.Thread
    'for tcp client -end
    Dim buy_price As Double
    Dim sale_price As Double
    Dim last_trade_price As Double
    Dim token_no As Long
    'Dim lzo As New decompress.algorithm()
    Dim sport As String
    Private Delegate Sub grddel()
    Dim mdel As grddel
    Dim futtable As DataTable
    Dim farr As New ArrayList

    'Public multicastListener_focm As Socket
    Public multicastListener_fo As Socket
    Public ThreadReceive_fo As System.Threading.Thread
    Dim lzo_fo As New decompress.algorithm()
    Private Sub grdindex()
        Try
            grdvol.FirstDisplayedScrollingRowIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub findvol_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Maximized
        'Me.Refresh()
    End Sub
    Private Sub findvol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If Not objanalysis Is Nothing Then
        '    objanalysis.refreshstarted = False
        'End If
        Me.Cursor = Cursors.WaitCursor
        chkfindvol = True
        'for tcp client start
        'Try
        '    localEndPoint = New IPEndPoint(IPAddress.Parse(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='TCP_server_ip'")), "", objTrad.Settings.Compute("max(SettingKey)", "SettingName='TCP_server_ip'"))), CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='TCP_server_port'")), "", objTrad.Settings.Compute("max(SettingKey)", "SettingName='TCP_server_port'"))))

        '    server = New Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp)
        '    server.Connect(localEndPoint)

        '    'Thread_data = New System.Threading.Thread(AddressOf Receive_data)
        '    'Thread_data.Start()
        'Catch ex As Exception
        '    MsgBox("TCP Server not connected....." & vbCr & "Software can not update LTP values", MsgBoxStyle.Exclamation)
        '    'MsgBox(ex.ToString)
        'End Try

        'for tcp client end

        'System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        'masterdata = New DataTable
        'masterdata = objTrad.Script
        scripttable = New DataTable
        scripttable = cpfmaster

        fdv = New DataView(scripttable, "option_type not in ('CA','CE','PA','PE')", "symbol", DataViewRowState.CurrentRows)
        futall = New ArrayList
        futall = futtoken

        cmbport_fill()
        mdel = AddressOf grdindex
        'grdvol.DataSource = voltable

        'If server.Connected Then
        '    initialize_focm_broadcast()
        '    Thread_data = New System.Threading.Thread(AddressOf Receive_data)
        '    Thread_data.Start()
        'End If
        Try
            initialize_fo_broadcast()
        Catch ex As Exception
            MsgBox("Cannot connect to F&O UDP server.", MsgBoxStyle.Exclamation)
        End Try
        Dim btime As Double
        btime = CDbl(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Timer_Calculation_Interval'")), 1, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Timer_Calculation_Interval'")))
        If btime = 0 Then
            btime = 1
        End If
        Timer_Calculation.Interval = btime * 1000
        Timer_Calculation.Enabled = True
        Me.Cursor = Cursors.Default
        'Me.WindowState = FormWindowState.Maximized
        'Me.Refresh()
    End Sub
    'Public Sub initialize_focm_broadcast()
    '    Try
    '        multicastListener_focm = New Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
    '        multicastListener_focm.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1)
    '        multicastListener_focm.Bind(New IPEndPoint(IPAddress.Any, IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='UDP_multicast_port'")), "", objTrad.Settings.Compute("max(SettingKey)", "SettingName='UDP_multicast_port'"))))
    '        multicastListener_focm.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(IPAddress.Parse(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='UDP_multicast_ip'")), "", objTrad.Settings.Compute("max(SettingKey)", "SettingName='UDP_multicast_ip'"))), IPAddress.Any))

    '        'ThreadReceive_focm = New System.Threading.Thread(AddressOf ReceiveMessages_fo)
    '        'ThreadReceive_focm.Start()

    '    Catch x As Exception
    '        'Console.WriteLine(x.Message)
    '        MsgBox(x.ToString)
    '    End Try

    'End Sub
    Private Sub findvol_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        chkfindvol = False
        'Thread_data.Abort()
        'server.Close()
        'If Not objanalysis Is Nothing Then
        '    objanalysis.refreshstarted = True
        'End If
    End Sub
    Private Sub findvol_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Try


                chkfindvol = False
                'If server.Connected Then
                '    server.Close()
                'End If
                'Thread_data.Abort()
                Try
                    'multicastListener_fo.Disconnect(False)
                    multicastListener_fo.Close()
                    ThreadReceive_fo.Abort()
                Catch ex As Threading.ThreadAbortException
                    Threading.Thread.ResetAbort()
                End Try
            Catch ex As Exception

            End Try
            'If Not objanalysis Is Nothing Then
            '    objanalysis.refreshstarted = True
            'End If
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub
    Private Sub init_table()
        voltable = New DataTable
        With voltable.Columns
            .Add("company")
            .Add("cpf")
            .Add("mdate", GetType(Date))
            .Add("strike", GetType(Double))
            .Add("script")
            .Add("token", GetType(Integer))
            .Add("buyprice", GetType(Double))
            .Add("buyvol", GetType(Double))
            .Add("futbuy", GetType(Double))
            .Add("saleprice", GetType(Double))
            .Add("salevol", GetType(Double))
            .Add("futsale", GetType(Double))
            .Add("ltp", GetType(Double))
            .Add("ltpvol", GetType(Double))
            .Add("futltp", GetType(Double))
            .Add("ftoken", GetType(Double))
            .Add("portfolio")
            .Add("instrument")
            .Add("uid", GetType(Integer))
            .Add("ordseq", GetType(Integer))
        End With

        futtable = New DataTable
        With futtable.Columns
            .Add("company")
            .Add("cpf")
            .Add("mdate", GetType(Date))
            .Add("strike", GetType(Double))
            .Add("script")
            .Add("token", GetType(Integer))
            .Add("buyprice", GetType(Double))
            .Add("buyvol", GetType(Double))
            .Add("futbuy", GetType(Double))
            .Add("saleprice", GetType(Double))
            .Add("salevol", GetType(Double))
            .Add("futsale", GetType(Double))
            .Add("ltp", GetType(Double))
            .Add("ltpvol", GetType(Double))
            .Add("futltp", GetType(Double))
            .Add("ftoken", GetType(Double))
            .Add("portfolio")
            .Add("instrument")
            .Add("uid", GetType(Integer))
            .Add("ordseq", GetType(Integer))
        End With

    End Sub
    Private Sub portfoliodata()
        Try
            futarray = New ArrayList
            cparray = New ArrayList
            farr = New ArrayList
            Dim buffer_temp(4) As Byte
            Dim buffer(5) As Byte
            init_table()
            Dim porttable As New DataTable
            If chkport.Checked = True And txtportfolio.Text.Trim <> "" Then
                porttable = objVol.Selectvol(txtportfolio.Text.Trim)
            ElseIf chkport.Checked = False And cmbport.SelectedIndex > 0 Then
                porttable = objVol.Selectvol(cmbport.SelectedItem)
            End If
            voltable.Rows.Clear()
            grdvol.DataSource = voltable

            If porttable.Rows.Count > 0 Then
                fill_fut(porttable, True)
                'voltable = porttable
                Dim row As DataRow
                For Each drow As DataRow In porttable.Select("", "ordseq")
                    row = voltable.NewRow
                    row("instrument") = drow("instrument")
                    row("company") = UCase(drow("company"))
                    row("cpf") = drow("cpf")
                    row("mdate") = drow("mdate")
                    row("strike") = CDbl(drow("strike"))
                    row("script") = drow("script")
                    row("token") = drow("token")
                    row("uid") = drow("uid")
                    row("portfolio") = drow("portfolio")
                    row("buyprice") = 0
                    row("buyvol") = 0
                    row("futbuy") = 0
                    row("saleprice") = 0
                    row("salevol") = 0
                    row("futsale") = 0
                    row("ltp") = 0
                    row("ltpvol") = 0
                    row("futltp") = 0
                    row("ordseq") = drow("ordseq")
                    'cal_cp(CLng(drow("token")))
                    cparray.Add(CLng(drow("token")))
                    'If server.Connected = True Then

                    '    buffer_temp = System.BitConverter.GetBytes(CLng(drow("token")))

                    '    buffer(0) = buffer_temp(0)
                    '    buffer(1) = buffer_temp(1)
                    '    buffer(2) = buffer_temp(2)
                    '    buffer(3) = buffer_temp(3)
                    '    buffer(4) = 1
                    '    server.Send(buffer, 5, SocketFlags.None)
                    '    System.Threading.Thread.Sleep(10)
                    'End If

                    For Each row1 As DataRow In fdv.ToTable().Select("symbol='" & drow("company") & "' and expdate1=#" & Format(drow("mdate"), "dd-MMM-yyyy") & "#")
                        row("ftoken") = CLng(row1("token"))
                        If Not futarray.Contains(CLng(row1("token"))) Then

                            futarray.Add(CLng(row1("token")))
                            'If server.Connected = True Then

                            '    buffer_temp = System.BitConverter.GetBytes(CLng(row1("token")))

                            '    buffer(0) = buffer_temp(0)
                            '    buffer(1) = buffer_temp(1)
                            '    buffer(2) = buffer_temp(2)
                            '    buffer(3) = buffer_temp(3)
                            '    buffer(4) = 1
                            '    server.Send(buffer, 5, SocketFlags.None)
                            '    System.Threading.Thread.Sleep(10)
                            'End If
                        End If

                    Next
                    voltable.Rows.Add(row)
                Next
            End If
            ' fill_fut()
            'Dim buffer_temp(4) As Byte
            'Dim buffer(5) As Byte
            'For Each row As DataRow In masterdata.Select("script='" & txtscript.Text.Trim & "'")
            '    drow("token") = CLng(row("token"))
            '    cparray.Add(CLng(row("token")))

            '    buffer_temp = System.BitConverter.GetBytes(CLng(row("token")))

            '    'If drow("tokanno") = 53025 Then
            '    '    MsgBox("test")

            '    'End If

            '    buffer(0) = buffer_temp(0)
            '    buffer(1) = buffer_temp(1)
            '    buffer(2) = buffer_temp(2)
            '    buffer(3) = buffer_temp(3)
            '    buffer(4) = 1
            '    server.Send(buffer, 5, SocketFlags.None)
            '    System.Threading.Thread.Sleep(100)
            'Next
            'voltable.Rows.Add(drow)
            ''grdvol.Refresh()
            'cal_cp(CLng(drow("token")))
            'For Each row As DataRow In fdv.ToTable().Select("symbol='" & drow("company") & "' and expdate1='" & drow("mdate") & "'")
            '    If Not futarray.Contains(CLng(row("token"))) Then
            '        futarray.Add(CLng(row("token")))

            '        buffer_temp = System.BitConverter.GetBytes(CLng(row("token")))

            '        buffer(0) = buffer_temp(0)
            '        buffer(1) = buffer_temp(1)
            '        buffer(2) = buffer_temp(2)
            '        buffer(3) = buffer_temp(3)
            '        buffer(4) = 1
            '        server.Send(buffer, 5, SocketFlags.None)
            '        System.Threading.Thread.Sleep(100)

            '    End If
            'Next
            grdvol.DataSource = voltable
            'grdmvol.DataSource = voltable
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub fill_fut(ByVal dtable As DataTable, Optional ByVal check As Boolean = False)
        Dim dv As New DataView(dtable)
        futtable.Rows.Clear()
        Dim porttable As New DataTable
        If chkport.Checked = True And txtportfolio.Text.Trim <> "" Then
            porttable = objVol.Selectvol(txtportfolio.Text.Trim)
        ElseIf chkport.Checked = False And cmbport.SelectedIndex > 0 Then
            porttable = objVol.Selectvol(cmbport.SelectedItem)
        End If
        Dim str As String = ""
        Dim i As Integer = 0
        If check = True Then
            Dim st(1) As String
            st(0) = "company"
            st(1) = "mdate"
            For Each drow As DataRow In dv.ToTable(True, st).Rows
                For Each row1 As DataRow In fdv.ToTable().Select("symbol='" & drow("company") & "' and expdate1=#" & fDate(drow("mdate")) & "#")
                    If Not farr.Contains(CLng(row1("token"))) Then
                        farr.Add(CLng(row1("token")))
                        If i = 0 Then
                            str = CStr(row1("token"))
                        Else
                            str = str & "," & CStr(row1("token"))
                        End If
                        i = i + 1
                    End If
                Next
            Next
        Else
            For Each drow As DataRow In dv.ToTable(True, "ftoken").Rows
                If Not farr.Contains(CLng(drow("ftoken"))) Then
                    farr.Add(CLng(drow("ftoken")))
                    If i = 0 Then
                        str = CStr(drow("ftoken"))
                    Else
                        str = str & "," & CStr(drow("ftoken"))
                    End If
                    i = i + 1
                End If
            Next
        End If

        If str <> "" Then
            str = "(" & str & ")"

            dv = New DataView(scripttable, "token in " & str, "symbol", DataViewRowState.CurrentRows)

            If dv.ToTable.Rows.Count > 0 Then

                'voltable = porttable
                Dim row As DataRow
                For Each drow As DataRow In dv.ToTable.Rows
                    row = futtable.NewRow
                    row("instrument") = drow("InstrumentName")
                    row("company") = UCase(drow("symbol"))
                    row("cpf") = "F"
                    row("mdate") = drow("expdate1")
                    row("strike") = CDbl(drow("strike_price"))
                    row("script") = drow("script")
                    row("token") = drow("token")
                    row("ftoken") = drow("token")
                    row("uid") = 0
                    row("portfolio") = porttable
                    row("buyprice") = 0
                    row("buyvol") = 0
                    row("futbuy") = 0
                    row("saleprice") = 0
                    row("salevol") = 0
                    row("futsale") = 0
                    row("ltp") = 0
                    row("ltpvol") = 0
                    row("futltp") = 0
                    row("ordseq") = 0
                    futtable.Rows.Add(row)
                Next
                voltable.Merge(futtable)
            End If
        End If
    End Sub
#Region "Thread"
    Public Sub initialize_fo_broadcast()
        Try
            'SocketNO_fo = 34330
            'GroupIP_fo = IPAddress.Parse("233.1.2.5")

            multicastListener_fo = New Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp)
            multicastListener_fo.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1)
            multicastListener_fo.Bind(New IPEndPoint(IPAddress.Any, IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='FO_UDP_Port'")), "", objTrad.Settings.Compute("max(SettingKey)", "SettingName='FO_UDP_Port'"))))
            multicastListener_fo.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(IPAddress.Parse(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='FO_UDP_IP'")), "", objTrad.Settings.Compute("max(SettingKey)", "SettingName='FO_UDP_IP'"))), IPAddress.Any))

            'multicastListener_fo.Bind(New IPEndPoint(IPAddress.Any, 34330))
            'multicastListener_fo.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, New MulticastOption(IPAddress.Parse("233.1.2.5"), IPAddress.Any))

            'GroupEP_fo = New IPEndPoint(GroupIP_fo, SocketNO_fo)
            'receivingUdpClient_fo = New UdpClient(SocketNO_fo)
            'receivingUdpClient_fo.JoinMulticastGroup(GroupIP_fo)

            'receivingUdpClient = New System.Net.Sockets.UdpClient(SocketNO)
            ThreadReceive_fo = New System.Threading.Thread(AddressOf ReceiveMessages_fo)
            ThreadReceive_fo.Start()

        Catch x As Exception
            'Console.WriteLine(x.Message)
            MsgBox(x.ToString)
        End Try

    End Sub
    Public Sub ReceiveMessages_fo()
        Try
            Dim bteReceiveData_fo(511) As Byte
            multicastListener_fo.Receive(bteReceiveData_fo)
            If thrworking = True Then
                process_fo_data(bteReceiveData_fo)
            End If

            NewInitialize_fo()
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        Catch ex1 As Exception
            'MsgBox(ex1.ToString)
        End Try
    End Sub
    Public Sub NewInitialize_fo()
        'Console.WriteLine("Thread *Thread Receive* reinitialized")
        Try
            ThreadReceive_fo = New System.Threading.Thread(AddressOf ReceiveMessages_fo)
            ThreadReceive_fo.Start()
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try

    End Sub
    Public Sub process_fo_data(ByVal temp_data() As Byte)
        Try
            Dim decompress_data() As Byte
            Dim compressed_length_old As Int16 = 0
            Dim compressed_length As Int16 = 0
            Dim packet_counter As Int16 = 0

            While packet_counter < IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 2))
                compressed_length = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(temp_data, 4 + compressed_length_old + (packet_counter * 2)))
                If compressed_length <> 0 Then
                    Dim compressed_data(compressed_length - 1) As Byte
                    Try
                        Array.Copy(temp_data, 6 + compressed_length_old + (packet_counter * 2), compressed_data, 0, compressed_length)
                    Catch ex As Exception
                        Exit While
                    End Try
                    Try
                        decompress_data = lzo_fo.Decompress(compressed_data)
                        If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(decompress_data, 16)) = 7208 Then
                            process_fo_7208(decompress_data)
                        End If
                    Catch ex As Exception
                        Exit While
                    End Try
                End If
                compressed_length_old += compressed_length
                packet_counter += 1
            End While
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub
    Private Sub process_fo_7208(ByVal data() As Byte)
        '  If IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 44)) = 468 Then
        For j As Integer = 0 To (IPAddress.HostToNetworkOrder(System.BitConverter.ToInt16(data, 46)) - 1) * 214 Step 214
            Try
                token_no = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 48 + j))
                buy_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 108 + j)) / 100
                sale_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 168 + j)) / 100
                last_trade_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 60 + j)) / 100

                If futall.Contains(token_no) Then


                    If fltpprice.Contains(token_no) Then
                        fbuyprice.Item(token_no) = buy_price
                        fsaleprice.Item(token_no) = sale_price
                        fltpprice.Item(token_no) = last_trade_price
                    Else
                        fbuyprice.Add(token_no, buy_price)
                        fsaleprice.Add(token_no, sale_price)
                        fltpprice.Add(token_no, last_trade_price)
                    End If
                    'If futarray.Contains(token_no) Then
                    '    'Dim futthread As Threading.Thread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf cal_future))
                    '    'futthread.Start(token_no)
                    '    cal_future(token_no)
                    'End If

                Else

                    If ltpprice.Contains(token_no) Then

                        buyprice1.Item(token_no) = buy_price
                        saleprice1.Item(token_no) = sale_price
                        ltpprice.Item(token_no) = last_trade_price
                    Else
                        buyprice1.Add(token_no, buy_price)
                        saleprice1.Add(token_no, sale_price)
                        ltpprice.Add(token_no, last_trade_price)
                    End If

                    'If cparray.Contains(token_no) Then

                    '    'Dim cpthread As Threading.Thread
                    '    'cpthread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf cal_cp))
                    '    'cpthread.Start(token_no)
                    '    'MsgBox(token_no)
                    '    cal_cp(token_no)
                    'End If
                    'End If


                End If
            Catch ex As Threading.ThreadAbortException
                Threading.Thread.ResetAbort()
            End Try
        Next
        ' End If
    End Sub

    'Private Sub Receive_data()

    '    'Try
    '    Try
    '        If server.Connected = True Then
    '            Try
    '                Dim buffer(16) As Byte
    '                'server.Receive(buffer)
    '                multicastListener_focm.Receive(buffer)
    '                'buffer(4) = 0 then it means cm data
    '                If thrworking = True Then
    '                    If buffer(0) = 2 Then 'fo
    '                        process_fo_7208(buffer)
    '                        ' process_fo_data(buffer)

    '                        'ElseIf buffer(0) = 4 Then 'cm
    '                        '    process_cm_data(buffer)

    '                    End If
    '                End If
    '                Thread_data = New System.Threading.Thread(AddressOf Receive_data)
    '                Thread_data.Start()
    '            Catch ex As Exception
    '                Exit Sub
    '            End Try
    '        End If
    '    Catch ex As Threading.ThreadAbortException
    '        Threading.Thread.ResetAbort()
    '        'MsgBox(ex.ToString)
    '        'Catch ex1 As Exception
    '        '    MsgBox(ex1.ToString)
    '    End Try
    '    'Catch ex As Threading.ThreadAbortException
    '    '    Threading.Thread.ResetAbort()
    '    '    'MsgBox(ex.ToString)
    '    '    'ListBox1.Items.Insert(0, ex.ToString)
    '    'End Try



    'End Sub
    'Private Function process_fo_7208(ByVal data() As Byte) As Integer


    '    'Dim best_buy_price As Long
    '    'Dim best_sale_price As Long
    '    Try

    '        token_no = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 1))
    '        buy_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 9)) / 100
    '        sale_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 13)) / 100
    '        last_trade_price = IPAddress.HostToNetworkOrder(System.BitConverter.ToInt32(data, 5)) / 100


    '        If futall.Contains(token_no) Then


    '            If fltpprice.Contains(token_no) Then
    '                fbuyprice.Item(token_no) = buy_price
    '                fsaleprice.Item(token_no) = sale_price
    '                fltpprice.Item(token_no) = last_trade_price
    '            Else
    '                fbuyprice.Add(token_no, buy_price)
    '                fsaleprice.Add(token_no, sale_price)
    '                fltpprice.Add(token_no, last_trade_price)
    '            End If
    '            If futarray.Contains(token_no) Then
    '                'Dim futthread As Threading.Thread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf cal_future))
    '                'futthread.Start(token_no)
    '                cal_future(token_no)
    '            End If

    '        Else

    '            If ltpprice.Contains(token_no) Then

    '                buyprice1.Item(token_no) = buy_price
    '                saleprice1.Item(token_no) = sale_price
    '                ltpprice.Item(token_no) = last_trade_price
    '            Else
    '                buyprice1.Add(token_no, buy_price)
    '                saleprice1.Add(token_no, sale_price)
    '                ltpprice.Add(token_no, last_trade_price)
    '            End If

    '            If cparray.Contains(token_no) Then

    '                'Dim cpthread As Threading.Thread
    '                'cpthread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf cal_cp))
    '                'cpthread.Start(token_no)
    '                'MsgBox(token_no)
    '                cal_cp(token_no)
    '            End If
    '            'End If


    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.ToString)

    '    End Try
    'End Function
    'Private Sub cal_cp(ByVal token As Object)
    '    'Try
    '    Try


    '        If (ltpprice.Count > 0) Then
    '            Dim buypr As Double = 0
    '            Dim salepr As Double = 0
    '            Dim ltppr As Double = 0

    '            Dim fbuypr As Double = 0
    '            Dim fsalepr As Double = 0
    '            Dim fltppr As Double = 0
    '            Dim mt As Integer = 0
    '            Dim isfut As Boolean = False
    '            Dim iscall As Boolean = False
    '            'For i As Integer = 0 To futarray.Count - 1
    '            '    If fltpprice.Contains(futarray(i)) Then
    '            '        fltppr = CDbl(fltpprice(futarray(i)))
    '            '        isfut = True
    '            '        Exit For
    '            '    End If
    '            'Next

    '            For Each drow As DataRow In voltable.Select("token=" & CLng(token) & "")

    '                If ltpprice.Contains(CLng(drow("token"))) Then
    '                    fltppr = 0
    '                    'For Each row As DataRow In fdv.ToTable().Select("symbol='" & drow("company") & "' and expdate1='" & CDate(drow("mdate")).Date & "'")
    '                    'MsgBox(drow("ftoken"))
    '                    If fltpprice.Contains(CLng(drow("ftoken"))) Then
    '                        fltppr = CDbl(fltpprice(CLng(drow("ftoken"))))
    '                        fbuypr = CDbl(fbuyprice(CLng(drow("ftoken"))))
    '                        fsalepr = CDbl(fsaleprice(CLng(drow("ftoken"))))
    '                        isfut = True
    '                        'Exit For
    '                    End If
    '                    'Next
    '                    ltppr = CDbl(ltpprice(CLng(drow("token"))))
    '                    buypr = CDbl(buyprice1(CLng(drow("token"))))
    '                    salepr = CDbl(saleprice1(CLng(drow("token"))))
    '                    mt = UDDateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
    '                    If Now.Date = CDate(drow("mdate")).Date Then
    '                        mt = 0.5
    '                    End If
    '                    'mt = UDDateDiff(DateInterval.Day, CDate(drow("mdate")), Now())
    '                    If Mid(drow("cpf"), 1, 1) = "C" Then
    '                        iscall = True
    '                    Else
    '                        iscall = False
    '                    End If
    '                    If fltppr > 0 And fbuypr > 0 And fsalepr > 0 Then
    '                        CalData(fltppr, fbuypr, fsalepr, CDbl(drow("strike")), ltppr, buypr, salepr, mt, iscall, isfut, drow, 0)
    '                    End If
    '                End If
    '            Next
    '        End If
    '    Catch ex As Threading.ThreadAbortException
    '        Threading.Thread.ResetAbort()
    '        'MsgBox(ex.ToString)
    '    Catch ex1 As Exception
    '        MsgBox(ex1.ToString)
    '    End Try
    '    'Catch ex As Threading.ThreadAbortException
    '    '    Threading.Thread.ResetAbort()
    '    '    ' MsgBox(ex.ToString)
    '    'End Try
    'End Sub
    Private Sub cal_future()
        'Try
        Try
            If (ltpprice.Count > 0) Then
                Dim buypr As Double = 0
                Dim salepr As Double = 0
                Dim ltppr As Double = 0

                Dim fbuypr As Double = 0
                Dim fsalepr As Double = 0
                Dim fltppr As Double = 0
                Dim mt As Integer = 0
                Dim isfut As Boolean = False
                Dim iscall As Boolean = False
                'If fltpprice.Contains(CLng(token)) Then
                '    fltppr = CDbl(fltpprice(token))
                '    fbuypr = CDbl(fbuyprice(CLng(token)))
                '    fsalepr = CDbl(fsaleprice(CLng(token)))
                '    isfut = True
                'End If
                'For Each drow As DataRow In voltable.Select("token=" & token & "")
                '    drow("buyprice") = Math.Round(fbuypr, 2)
                '    drow("futbuy") = Math.Round(fbuypr, 2)
                '    drow("saleprice") = Math.Round(fsalepr, 2)
                '    drow("futsale") = Math.Round(fsalepr, 2)
                '    drow("ltp") = Math.Round(fsalepr, 2)
                '    drow("futltp") = Math.Round(fsalepr, 2)
                'Next
                For Each drow As DataRow In voltable.Select("cpf = 'F' ")
                    drow("buyprice") = Math.Round(CDbl(fbuyprice(CLng(Val(drow("token").ToString)))), 2)
                    drow("futbuy") = Math.Round(CDbl(fbuyprice(CLng(Val(drow("token").ToString)))), 2)
                    drow("saleprice") = Math.Round(CDbl(fsaleprice(CLng(Val(drow("token").ToString)))), 2)
                    drow("futsale") = Math.Round(CDbl(fsaleprice(CLng(Val(drow("token").ToString)))), 2)
                    drow("ltp") = Math.Round(CDbl(fltpprice(drow("token"))), 2)
                    drow("futltp") = Math.Round(CDbl(fltpprice(drow("token"))), 2)
                Next
                ' For Each row As DataRow In fdv.ToTable.Select("token=" & token & "")
                For Each drow As DataRow In voltable.Select("cpf <> 'F' ")
                    fltppr = CDbl(fltpprice(CLng(Val(drow("ftoken").ToString))))
                    fbuypr = CDbl(fbuyprice(CLng(Val(drow("ftoken").ToString))))
                    fsalepr = CDbl(fsaleprice(CLng(Val(drow("ftoken").ToString))))
                    If ltpprice.Contains(CLng(Val(drow("token").ToString))) Then
                        ltppr = CDbl(ltpprice(Val(CLng(drow("token").ToString))))
                        buypr = CDbl(buyprice1(Val(CLng(drow("token").ToString))))
                        salepr = CDbl(saleprice1(Val(CLng(drow("token").ToString))))
                        mt = UDDateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                        If Now.Date = CDate(drow("mdate")).Date Then
                            mt = 0.5
                        End If
                        If Mid(drow("cpf"), 1, 1) = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        If fltppr <> 0 And fbuypr <> 0 And fsalepr <> 0 Then
                            CalData(fltppr, fbuypr, fsalepr, CDbl(drow("strike")), ltppr, buypr, salepr, mt, iscall, True, drow, 0)
                        End If
                    End If
                Next
                ' Next

            End If
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
            'MsgBox(ex.ToString)
        Catch ex1 As Exception
            MsgBox(ex1.ToString)
        End Try
        'Catch ex As Threading.ThreadAbortException
        '    Threading.Thread.ResetAbort()
        'End Try
    End Sub
    Private Sub CalData(ByVal futval As Double, ByVal fbuypr As Double, ByVal fsalepr As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal buypr As Double, ByVal salepr As Double, ByVal mT As Integer, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal drow As DataRow, ByVal qty As Double)
        'Try
        Try
            Try
                Dim mDelta As Double
                Dim mGama As Double
                Dim mVega As Double
                Dim mThita As Double
                Dim mRah As Double
                Dim mVolatility As Double
                Dim mVolatilityb As Double
                Dim mVolatilitys As Double
                Dim tmpcpprice As Double = 0
                Dim tmpbuypr As Double = 0
                Dim tmpsalepr As Double = 0
                tmpcpprice = cpprice
                tmpbuypr = buypr
                tmpsalepr = salepr
                'Dim mIsCal As Boolean
                'Dim mIsPut As Boolean
                'Dim mIsFut As Boolean

                mDelta = 0
                mGama = 0
                mVega = 0
                mThita = 0
                mRah = 0
                Dim _mt As Double
                'IF MATURITY IS 0 THEN _MT = 0.00001 
                If mT = 0 Then
                    _mt = 0.00001
                Else
                    _mt = (mT) / 365

                End If
                'If futval > 0 Then
                mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
                'End If
                mVolatilityb = Greeks.Black_Scholes(fbuypr, stkprice, Mrateofinterast, 0, tmpbuypr, _mt, mIsCall, mIsFut, 0, 6)
                mVolatilitys = Greeks.Black_Scholes(fsalepr, stkprice, Mrateofinterast, 0, tmpsalepr, _mt, mIsCall, mIsFut, 0, 6)

                'Try

                '    'If Not mIsFut Then
                '    mDelta = mDelta + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))

                '    mGama = mGama + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))

                '    mVega = mVega + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))

                '    mThita = mThita + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))

                '    mRah = mRah + (mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))

                'Else
                ''mDelta = mDelta + (1 * drow("Qty"))
                'End If


                drow("buyprice") = Math.Round(buypr, 2)
                drow("buyvol") = Math.Round(mVolatilityb * 100, 2)
                drow("futbuy") = Math.Round(fbuypr, 2)
                drow("saleprice") = Math.Round(salepr, 2)
                drow("salevol") = Math.Round(mVolatilitys * 100, 2)
                drow("futsale") = Math.Round(fsalepr, 2)
                drow("ltp") = Math.Round(cpprice, 2)
                drow("ltpvol") = Math.Round(mVolatility * 100, 2)
                drow("futltp") = Math.Round(futval, 2)
                voltable.AcceptChanges()
                grdvol.Invoke(mdel)


            Catch ex As Exception
            End Try
            'grdmvol.FirstDisplayedScrollingRowIndex = 0


        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
            'MsgBox(ex.ToString)
        Catch ex1 As Exception
            Exit Sub
            'MsgBox(ex1.ToString)
        End Try
        'Catch ex As Threading.ThreadAbortException
        '    Threading.Thread.ResetAbort()
        '    'MsgBox(ex.ToString & "--- " & drow.RowError)
        'End Try

    End Sub
#End Region
    Private Sub grdvol_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvol.CellEndEdit
        Try


            If e.ColumnIndex = 0 Then
                grdvol.Rows(e.RowIndex).Cells(0).Value = UCase(grdvol.Rows(e.RowIndex).Cells(0).Value)
                grdvol.EndEdit()
            ElseIf e.ColumnIndex = 1 Then
                Dim grow As DataGridViewRow
                grow = grdvol.CurrentRow
                If IsDBNull(grow.Cells(1).Value) Or grow.Cells(1).Value Is Nothing Then
                    grow.Cells(1).Value = "CE"
                Else
                    Dim arr As New ArrayList
                    arr.Add("CE")
                    arr.Add("PE")
                    'arr.Add("CA")
                    'arr.Add("PA")
                    If Not arr.Contains(UCase(grow.Cells(1).Value)) Then
                        'MsgBox("Enter 'CE','PE','CA','PA'")
                        MsgBox("Enter 'CE','PE'.")
                        grdvol.Rows(e.RowIndex).Cells(1).Selected = True
                        Exit Sub
                    End If
                    grow.Cells(1).Value = UCase(grow.Cells(1).Value)

                End If
                grdvol.EndEdit()
            ElseIf e.ColumnIndex = 3 Then
                Dim buffer_temp(4) As Byte
                Dim buffer(5) As Byte
                Dim token As Long = 0
                Dim ftoken As Long = 0
                Dim script As String = ""
                Dim instrument As String = ""
                For Each drow As DataRow In scripttable.Select("symbol='" & UCase(grdvol.Rows(e.RowIndex).Cells(0).Value) & "' and option_type = '" & UCase(grdvol.Rows(e.RowIndex).Cells(1).Value) & "' and expdate1=#" & fDate(CDate(grdvol.Rows(e.RowIndex).Cells(2).Value)) & "# and strike_price=  " & CLng(grdvol.Rows(e.RowIndex).Cells(3).Value) & "")
                    token = drow("token")
                    script = drow("script")
                    instrument = drow("instrumentname")
                    Exit For
                Next
                For Each row1 As DataRow In fdv.ToTable().Select("symbol='" & UCase(grdvol.Rows(e.RowIndex).Cells(0).Value) & "' and expdate1='" & UCase(grdvol.Rows(e.RowIndex).Cells(2).Value) & "'")
                    ftoken = row1("token")
                Next
                If token <> 0 Then
                    Dim check As Boolean = False
                    For Each drow As DataRow In voltable.Select("token=" & token & "")
                        check = True
                        Exit For
                    Next
                    If check = True Then
                        If MsgBox(script & " Script alerady exist in portfilio,Would you like to add ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            grdvol.EndEdit()
                            voltable.Rows(e.RowIndex)("instrument") = instrument
                            voltable.Rows(e.RowIndex)("company") = UCase(grdvol.Rows(e.RowIndex).Cells(0).Value)
                            voltable.Rows(e.RowIndex)("cpf") = UCase(grdvol.Rows(e.RowIndex).Cells(1).Value)
                            voltable.Rows(e.RowIndex)("mdate") = UCase(grdvol.Rows(e.RowIndex).Cells(2).Value)
                            voltable.Rows(e.RowIndex)("strike") = CDbl(grdvol.Rows(e.RowIndex).Cells(3).Value)
                            voltable.Rows(e.RowIndex)("script") = script
                            voltable.Rows(e.RowIndex)("token") = token
                            For Each row1 As DataRow In fdv.ToTable().Select("symbol='" & UCase(grdvol.Rows(e.RowIndex).Cells(0).Value) & "' and expdate1='" & CDate(grdvol.Rows(e.RowIndex).Cells(2).Value) & "'")
                                voltable.Rows(e.RowIndex)("ftoken") = row1("token")
                            Next
                            voltable.AcceptChanges()
                            objVol.InstrumentName = voltable.Rows(e.RowIndex)("instrument")
                            objVol.Company = voltable.Rows(e.RowIndex)("company")
                            objVol.CP = voltable.Rows(e.RowIndex)("cpf")
                            objVol.Mdate = CDate(voltable.Rows(e.RowIndex)("mdate"))
                            objVol.StrikeRate = voltable.Rows(e.RowIndex)("strike")
                            objVol.Script = voltable.Rows(e.RowIndex)("script")
                            objVol.Token = voltable.Rows(e.RowIndex)("token")
                            objVol.Portfolio = voltable.Rows(e.RowIndex)("portfolio")
                            objVol.Uid = voltable.Rows(e.RowIndex)("uid")
                            objVol.update()
                            cparray.Add(CLng(token))
                            'If server.Connected = True Then
                            '    buffer_temp = System.BitConverter.GetBytes(CLng(token))
                            '    buffer(0) = buffer_temp(0)
                            '    buffer(1) = buffer_temp(1)
                            '    buffer(2) = buffer_temp(2)
                            '    buffer(3) = buffer_temp(3)
                            '    buffer(4) = 1
                            '    server.Send(buffer, 5, SocketFlags.None)
                            '    System.Threading.Thread.Sleep(10)
                            'End If
                            futarray.Add(CLng(ftoken))
                            'If server.Connected = True Then
                            '    buffer_temp = System.BitConverter.GetBytes(CLng(ftoken))
                            '    buffer(0) = buffer_temp(0)
                            '    buffer(1) = buffer_temp(1)
                            '    buffer(2) = buffer_temp(2)
                            '    buffer(3) = buffer_temp(3)
                            '    buffer(4) = 1
                            '    server.Send(buffer, 5, SocketFlags.None)
                            '    System.Threading.Thread.Sleep(10)
                            'End If
                        Else
                            grdvol.CancelEdit()
                            grdvol.Refresh()
                        End If
                    Else
                        'Dim row As DataRow
                        Dim porttable As New DataTable
                        If chkport.Checked = True And txtportfolio.Text.Trim <> "" Then
                            porttable = objVol.Selectvol(txtportfolio.Text.Trim)
                        ElseIf chkport.Checked = False And cmbport.SelectedIndex > 0 Then
                            porttable = objVol.Selectvol(cmbport.SelectedItem)
                        End If
                        grdvol.EndEdit()
                        voltable.Rows(e.RowIndex)("instrument") = instrument
                        voltable.Rows(e.RowIndex)("company") = UCase(grdvol.Rows(e.RowIndex).Cells(0).Value)
                        voltable.Rows(e.RowIndex)("cpf") = UCase(grdvol.Rows(e.RowIndex).Cells(1).Value)
                        voltable.Rows(e.RowIndex)("mdate") = CDate(grdvol.Rows(e.RowIndex).Cells(2).Value)
                        voltable.Rows(e.RowIndex)("strike") = CDbl(grdvol.Rows(e.RowIndex).Cells(3).Value)
                        voltable.Rows(e.RowIndex)("script") = script
                        voltable.Rows(e.RowIndex)("token") = token
                        For Each row1 As DataRow In fdv.ToTable().Select("symbol='" & UCase(grdvol.Rows(e.RowIndex).Cells(0).Value) & "' and expdate1='" & CDate(grdvol.Rows(e.RowIndex).Cells(2).Value) & "'")
                            voltable.Rows(e.RowIndex)("ftoken") = row1("token")
                            'row = voltable.NewRow
                            'row("instrument") = row1("InstrumentName")
                            'row("company") = UCase(row1("symbol"))
                            'row("cpf") = "F"
                            'row("mdate") = row1("expdate1")
                            'row("strike") = CDbl(row1("strike_price"))
                            'row("script") = row1("script")
                            'row("token") = row1("token")
                            'row("ftoken") = row1("token")
                            'row("uid") = 0
                            'row("portfolio") = porttable
                            'row("buyprice") = 0
                            'row("buyvol") = 0
                            'row("futbuy") = 0
                            'row("saleprice") = 0
                            'row("salevol") = 0
                            'row("futsale") = 0
                            'row("ltp") = 0
                            'row("ltpvol") = 0
                            'row("futltp") = 0
                            'row("ordseq") = 0

                            'voltable.Rows.Add(row)
                        Next
                        voltable.AcceptChanges()
                        objVol.InstrumentName = voltable.Rows(e.RowIndex)("instrument")
                        objVol.Company = voltable.Rows(e.RowIndex)("company")
                        objVol.CP = voltable.Rows(e.RowIndex)("cpf")
                        objVol.Mdate = CDate(voltable.Rows(e.RowIndex)("mdate"))
                        objVol.StrikeRate = voltable.Rows(e.RowIndex)("strike")
                        objVol.Script = voltable.Rows(e.RowIndex)("script")
                        objVol.Token = voltable.Rows(e.RowIndex)("token")
                        objVol.Portfolio = voltable.Rows(e.RowIndex)("portfolio")
                        objVol.Uid = voltable.Rows(e.RowIndex)("uid")
                        objVol.update()
                        cparray.Add(CLng(token))
                        'If server.Connected = True Then

                        '    buffer_temp = System.BitConverter.GetBytes(CLng(token))

                        '    buffer(0) = buffer_temp(0)
                        '    buffer(1) = buffer_temp(1)
                        '    buffer(2) = buffer_temp(2)
                        '    buffer(3) = buffer_temp(3)
                        '    buffer(4) = 1
                        '    server.Send(buffer, 5, SocketFlags.None)
                        '    System.Threading.Thread.Sleep(10)
                        'End If
                        futarray.Add(CLng(ftoken))
                        'If server.Connected = True Then

                        '    buffer_temp = System.BitConverter.GetBytes(CLng(ftoken))

                        '    buffer(0) = buffer_temp(0)
                        '    buffer(1) = buffer_temp(1)
                        '    buffer(2) = buffer_temp(2)
                        '    buffer(3) = buffer_temp(3)
                        '    buffer(4) = 1
                        '    server.Send(buffer, 5, SocketFlags.None)
                        '    System.Threading.Thread.Sleep(10)
                        'End If
                    End If
                    fill_fut(voltable)
                Else
                    MsgBox("Script dose not exist in Master data.", MsgBoxStyle.Information)
                    grdvol.CancelEdit()
                    grdvol.Refresh()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
    End Sub

    Private Sub grdvol_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdvol.KeyDown
        Try
            If thrworking = False Then
                If e.KeyCode = Keys.Delete Then
                    If MsgBox("Are you sure to delete selected script?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        objVol.Token = voltable.Rows(grdvol.CurrentRow.Index)("token")
                        objVol.Portfolio = voltable.Rows(grdvol.CurrentRow.Index)("portfolio")
                        objVol.Uid = voltable.Rows(grdvol.CurrentRow.Index)("uid")
                        objVol.deletevol()
                        voltable.Rows.RemoveAt(grdvol.CurrentRow.Index)
                        voltable.AcceptChanges()
                        grdvol.Refresh()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
    End Sub
    Private Sub grdvol_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdvol.DataError
        Try

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub txtportfolio_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtportfolio.Validating
        Try
            If chkport.Checked = True And txtportfolio.Text <> "" Then
                portfoliodata()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub chkport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkport.CheckedChanged
        Try
            txtportfolio.Text = ""
            If cmbport.Items.Count > 0 Then
                cmbport.SelectedIndex = 0
            End If
            If chkport.Checked = True Then
                txtportfolio.Enabled = True
                cmbport.Enabled = False

                cmdport.Text = "Add Portfolio"
            Else
                txtportfolio.Enabled = False
                cmbport.Enabled = True
                cmdport.Text = "Edit Portfolio"
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub cmdport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdport.Click
        Try
            If chkport.Checked = True And txtportfolio.Text.Trim = "" Then
                MsgBox("Enter Portfolio Name.", MsgBoxStyle.Information)
                txtportfolio.Focus()
                Exit Sub
            ElseIf chkport.Checked = False And cmbport.SelectedIndex = 0 Then
                MsgBox("Select Portfolio Name.", MsgBoxStyle.Information)
                cmbport.Focus()
                Exit Sub
            End If

            Dim master As New display_master
            If chkport.Checked = True Then
                master.portfolio = txtportfolio.Text
                sport = txtportfolio.Text
            Else
                If cmbport.SelectedIndex > 0 Then
                    master.portfolio = cmbport.SelectedItem
                    sport = cmbport.SelectedItem
                End If
            End If
            master.ShowDialog()
            cmbport_fill()
            'portfoliodata()
            chkport.Checked = False
            txtportfolio.Text = ""
            If cmbport.Items.Count > 0 Then
                cmbport.SelectedIndex = 0
            End If
            If chkport.Checked = True Then
                txtportfolio.Enabled = True
                cmbport.Enabled = False
            Else
                txtportfolio.Enabled = False
                cmbport.Enabled = True
            End If
            If sport <> "" Then
                cmbport.SelectedItem = sport
            Else
                cmbport.SelectedIndex = 0
            End If
            If cmbport.Items.Count <= 0 Then
                cmdport.Text = "Add Portfolio"
            End If
            cmbport_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub cmbport_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbport.SelectedIndexChanged
        Try
            If chkport.Checked = False And cmbport.SelectedIndex > 0 Then
                portfoliodata()
                cmdport.Text = "Edit Portfolio"
            Else
                portfoliodata()

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub cmbport_fill()
        Try
            Dim dtable As New DataTable
            dtable = objVol.Selectportfolio
            cmbport.Items.Clear()
            If dtable.Rows.Count > 0 Then
                cmbport.Items.Add("Select Portfolio")
                For Each drow As DataRow In dtable.Rows
                    cmbport.Items.Add(drow("portfolio"))
                Next

            Else
                cmbport.Items.Add("NA")
            End If
            cmbport.SelectedIndex = 0
            If sport <> "" Then
                cmbport.SelectedItem = sport
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub cmdord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdord.Click
        If voltable.Rows.Count > 0 Then
            Dim portfolio As String = ""
            If chkport.Checked = True And txtportfolio.Text.Trim <> "" Then
                portfolio = txtportfolio.Text.Trim
            ElseIf chkport.Checked = False And cmbport.SelectedIndex > 0 Then
                portfolio = cmbport.SelectedItem
            End If
            If portfolio <> "" Then
                objVol.deletevol(portfolio)
                objVol.Insert(voltable, grdvol, "", 0, True)
            Else

            End If
            'portfoliodata()
        End If
    End Sub
    Private Sub cmdstart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdstart.Click
        If thrworking = True Then
            cmdstart.Text = "Start"
            thrworking = False
            grdvol.Columns(0).ReadOnly = False
            grdvol.Columns(1).ReadOnly = False
            grdvol.Columns(2).ReadOnly = False
            grdvol.Columns(3).ReadOnly = False
            Timer_Calculation.Stop()

        Else
            cmdstart.Text = "Stop"
            thrworking = True
            grdvol.Columns(0).ReadOnly = True
            grdvol.Columns(1).ReadOnly = True
            grdvol.Columns(2).ReadOnly = True
            grdvol.Columns(3).ReadOnly = True
            Timer_Calculation.Start()
        End If
    End Sub


    Private Sub Timer_Calculation_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Calculation.Tick
        If thrworking = True Then
            cal_future()
            'cal_eq()
            'cal_future()
            'Me.Invoke(mval)
            'lblcount.Text = CInt(lblcount.Text) + 1
            ' timer_counter += 1
            ' txtprexp.Text = timer_counter
        End If
    End Sub
End Class
