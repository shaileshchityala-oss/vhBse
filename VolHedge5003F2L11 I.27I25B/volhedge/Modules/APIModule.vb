
Module APIModule
    'Public ObjGDT As New GlobalDT.ClsGlobalDT("VolHedge", flgAPI_K)
    'Public ObjGDT As GlobalDT.ClsGlobalDT  'comment By payal Patel  24-oct-2018
    Public HT_GetTokanFromIdentifier As New Hashtable
    Public HT_GetIdentifierFromTokan As New Hashtable
    Public HT_RegIdentifier As New Hashtable
    Public HT_UnRegIdentifier As New Hashtable
    Public flg_UnRegIdentifier As Boolean = False
    Dim ThreadProcFoData_Exe As System.Threading.Thread
    Dim ThreadProcFoID_Exe As System.Threading.Thread
    Dim ThreadProcFoID_Unreg_Exe As System.Threading.Thread

    Public flgApiReStart As Boolean = False

    Public FoIdentifierQueue As New Queue(Of String)
    Public flgAPI As Boolean = False
    Public flgAPI_Exp As Date
    Public flgAPI_Expint As Integer

    Public flgAPI_ExpCheck As Boolean


    Public flgAPI_K As String = ""
    Private isThreadStarted As Boolean = False
    Private Sub RegFoScript(ByVal Identifier As String, ByVal unRegister As Boolean)
        ' ObjGDT.RegScript("NFO", Identifier, unRegister)  'comment By payal Patel  24-oct-2018
    End Sub

    'Private Sub LastFOQuoteResult(ByVal Identifier As String)
    '    ObjGDT.getl("NFO", Identifier)
    'End Sub

    Private Sub process_RegIdentifier()
        Try
            While True
                While flg_UnRegIdentifier = True
                    Threading.Thread.Sleep(900)
                End While
                'While ((FoIdentifierQueue.Count = 0) Or (ObjGDT.IsAuthanticate = False))
                '    Threading.Thread.Sleep(900)
                'End While
                Dim str As String
                Dim cnt As Integer = 0
                cnt = 0
                cnt = Math.Min(FoIdentifierQueue.Count, 10)
                For i As Integer = 0 To cnt - 1
                    str = FoIdentifierQueue.Dequeue
                    If Not HT_RegIdentifier.Contains(str) Then
                        HT_RegIdentifier.Add(str, True)

                        'ObjGDT.LastQuoteResult("NFO", str)  'comment By payal Patel  24-oct-2018
                        'ObjGDT.RegScript("NFO", str, False)  'comment By payal Patel  24-oct-2018
                    End If
                Next
            End While

        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        Catch ex As Exception
            WriteLog("Error In Process_Regidentifier" & vbCrLf & ex.ToString)
            ExecRegFOIdentifier()
        End Try
    End Sub
    Public Sub manageIdentifier()
        'dv = New DataView(maintable, "", "", DataViewRowState.CurrentRows)
        For Each dr As DataRow In maintable.Rows

            Dim str As String = HT_GetIdentifierFromTokan(CLng(dr("Tokanno")))
            UnRegIdentifier(str)
        Next
    End Sub
    Public Sub UnRegIdentifier(ByVal str As String)
        Try

            'Dim str As String

            '        str = entry.Key
            '   ObjGDT.RegScript("NFO", str, True)
            HT_RegIdentifier.Remove(str)
            FoRegTokens.Clear()
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        Catch ex As Exception
            WriteLog("Error In Process_unRegidentifier" & vbCrLf & ex.ToString)
            '     ExecRegFOIdentifier()
        End Try
        flg_UnRegIdentifier = False
    End Sub
    Private Sub process_UnRegIdentifier()
        Try
            'While True
            '    While ((FoIdentifierQueue.Count = 0) Or (ObjGDT.IsAuthanticate = False))
            '        Threading.Thread.Sleep(900)
            '    End While
            flg_UnRegIdentifier = True
            Dim str As String
            '    Dim cnt As Integer = 0
            '    cnt = 0
            '    cnt = Math.Min(FoIdentifierQueue.Count, 10)
            '    For i As Integer = 0 To cnt - 1
            ' str = FoIdentifierQueue.Dequeue
            If HT_RegIdentifier.Count > 0 Then
                '  HT_RegIdentifier.Add(str, True)
                HT_UnRegIdentifier.Clear()
                Dim entry As DictionaryEntry
                For Each entry In HT_RegIdentifier
                    If Not HT_UnRegIdentifier.Contains(entry.Key) Then
                        HT_UnRegIdentifier.Add(entry.Key, True)
                    End If

                    'str = entry.Key
                    'ObjGDT.RegScript("NFO", str, True)
                Next
                For Each entry In HT_UnRegIdentifier

                    str = entry.Key
                    '  ObjGDT.RegScript("NFO", str, True)  'comment By payal Patel  24-oct-2018
                Next
                HT_RegIdentifier.Clear()
            End If
            '    Next
            'End While
            flg_UnRegIdentifier = False
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        Catch ex As Exception
            WriteLog("Error In Process_unRegidentifier" & vbCrLf & ex.ToString)
            '     ExecRegFOIdentifier()
        End Try
        flg_UnRegIdentifier = False
    End Sub
    'comment By payal Patel  24-oct-2018
    'Private Sub process_fo_Api(ByVal obj As Object)
    '    Try
    '        'Write_Log2("Step3:process_fo_SQL Process Start..")
    '        'If flgApiConn = True Then
    '        'End If
    '        Dim token_no As Long
    '        Dim buy_price As Double
    '        Dim sale_price As Double
    '        Dim last_trade_price As Double
    '        Dim VolumeTradedToday As Double
    '        Dim ClosingPrice As Double
    '        Dim iStatus As Integer
    '        Dim dblStopLoss As Double
    '        Dim OpenInterest As Double

    '        If GVarIsNewBhavcopy = True Then
    '            GVarIsNewBhavcopy = False
    '        End If
    '        'ED_fo = New clsEnDe
    '        'Dr = DAL.DA_SQL.FillListFo("dbo.Sp_SelectFo '" & gVarInstanceID & "'") 'Objsql.SelectFoToken()


    '        Try
    '            Dim Objapi As New GlobalDT.ClsGlobalDT.ApiData()

    '            While True

    '                While ((ObjGDT.FoQueue.Count = 0) Or (ObjGDT.IsAuthanticate = False))
    '                    Threading.Thread.Sleep(900)
    '                End While


    '                Dim cnt As Integer = 0
    '                cnt = 0
    '                cnt = Math.Min(ObjGDT.FoQueue.Count, 10)
    '                For i As Integer = 0 To cnt - 1
    '                    Objapi = ObjGDT.FoQueue.Dequeue

    '                    If Objapi.Identifier = "FLG" Then
    '                        flgApiReStart = True
    '                        HT_RegIdentifier.Clear()
    '                    Else


    '                        token_no = HT_GetTokanFromIdentifier(Objapi.Identifier)
    '                        buy_price = Objapi.buy_price  'bid
    '                        sale_price = Objapi.sale_price 'ask
    '                        last_trade_price = Objapi.last_trade_price
    '                        VarBCurrentDate = DateDiff(DateInterval.Second, CDate("1-1-1980"), Objapi.LastTradeTime)

    '                        'If VarBCurrentDate < Val(ED.DFo(Dr("F6")) & "") Then
    '                        'VarBCurrentDate = Val(ED.DFo(Dr("F6")) & "")
    '                        'End If

    '                        VolumeTradedToday = Objapi.VolumeTradedToday
    '                        ClosingPrice = Objapi.ClosingPrice

    '                        OpenInterest = Objapi.OpenInterest
    '                        If VarBCurrentDate >= G_VarExpiryDate1 Then
    '                            IsVersionExpire = True
    '                            Application.Exit()
    '                        End If


    '                        If futall.Contains(token_no) Then
    '                            'Dim fltppr As Double
    '                            If fltpprice.Contains(token_no) Then
    '                                If FlgBcastStop = False Then
    '                                    fbuyprice.Item(token_no) = buy_price
    '                                    fsaleprice.Item(token_no) = sale_price
    '                                    If token_no = 52685 Then
    '                                        fltpprice.Item(token_no) = last_trade_price
    '                                    End If
    '                                    fltpprice.Item(token_no) = last_trade_price
    '                                End If
    '                            Else
    '                                fbuyprice.Add(token_no, buy_price)
    '                                fsaleprice.Add(token_no, sale_price)
    '                                fltpprice.Add(token_no, last_trade_price)
    '                            End If

    '                            If closeprice.Contains(token_no) Then
    '                                If FlgBcastStop = False Then
    '                                    closeprice.Item(token_no) = ClosingPrice
    '                                End If
    '                            Else
    '                                closeprice.Add(token_no, ClosingPrice)
    '                            End If
    '                            If OpenInterestprice.Contains(token_no) Then
    '                                '   If FlgBcastStop = False Then
    '                                OpenInterestprice.Item(token_no) = OpenInterest
    '                                'End If
    '                            Else
    '                                OpenInterestprice.Add(token_no, OpenInterest)
    '                            End If
    '                        Else
    '                            If ltpprice.Contains(token_no) Then
    '                                If FlgBcastStop = False Then
    '                                    buyprice.Item(token_no) = buy_price
    '                                    saleprice.Item(token_no) = sale_price
    '                                    ltpprice.Item(token_no) = last_trade_price
    '                                    MKTltpprice.Item(token_no) = last_trade_price
    '                                End If
    '                            Else
    '                                buyprice.Add(token_no, buy_price)
    '                                saleprice.Add(token_no, sale_price)
    '                                ltpprice.Add(token_no, last_trade_price)
    '                                MKTltpprice.Add(token_no, last_trade_price)
    '                            End If

    '                            If volumeprice.Contains(token_no) Then
    '                                If FlgBcastStop = False Then
    '                                    volumeprice.Item(token_no) = VolumeTradedToday
    '                                End If
    '                            Else
    '                                volumeprice.Add(token_no, VolumeTradedToday)
    '                            End If

    '                            If closeprice.Contains(token_no) Then
    '                                If FlgBcastStop = False Then
    '                                    closeprice.Item(token_no) = ClosingPrice
    '                                End If
    '                            Else
    '                                closeprice.Add(token_no, ClosingPrice)
    '                            End If
    '                            If OpenInterestprice.Contains(token_no) Then
    '                                '   If FlgBcastStop = False Then
    '                                OpenInterestprice.Item(token_no) = OpenInterest
    '                                'End If
    '                            Else
    '                                OpenInterestprice.Add(token_no, OpenInterest)
    '                            End If
    '                        End If
    '                    End If
    '                Next
    '            End While
    '        Catch ex As Threading.ThreadAbortException

    '            Threading.Thread.ResetAbort()
    '        Catch ex As Exception
    '            WriteLog("Error In ExecFoIQry" & vbCrLf & ex.ToString)
    '            ExecFoDatapull()
    '        End Try



    '        'Write_Log2("Step3:process_fo_SQL Process Stop..")
    '    Catch ex As Exception

    '    End Try

    'End Sub
    Public Sub ApiThreadreset()
        isThreadStarted = False
        ' ObjGDT = Nothing  'comment By payal Patel  24-oct-2018

        ThreadProcFoID_Exe.Abort()
        ThreadProcFoID_Exe = Nothing

        ThreadProcFoData_Exe.Abort()
        ThreadProcFoData_Exe = Nothing
        HT_RegIdentifier.Clear()
    End Sub
    Public Sub InitApiThread()
        Try
            'comment By payal Patel  24-oct-2018

            'If isThreadStarted = False Then
            '    isThreadStarted = True
            '    ObjGDT = New GlobalDT.ClsGlobalDT("VolHedge", flgAPI_K, True)
            '    ExecRegFOIdentifier()
            '    ExecFoDatapull()
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ExecFoDatapull()

        'comment By payal Patel  24-oct-2018
        'ThreadProcFoData_Exe = New System.Threading.Thread(AddressOf process_fo_Api)
        'ThreadProcFoData_Exe.Start()
    End Sub

    Private Sub ExecRegFOIdentifier()
        ThreadProcFoID_Exe = New System.Threading.Thread(AddressOf process_RegIdentifier)
        ThreadProcFoID_Exe.Start()
    End Sub
    Public Sub ExecUnRegFOIdentifier()
        ThreadProcFoID_Unreg_Exe = New System.Threading.Thread(AddressOf process_UnRegIdentifier)
        ThreadProcFoID_Unreg_Exe.Start()
    End Sub
    Public Sub Api_UnRegFO_Threadreset()


        ThreadProcFoID_Unreg_Exe.Abort()
        ThreadProcFoID_Unreg_Exe = Nothing

    End Sub
End Module
