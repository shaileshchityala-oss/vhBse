Imports System.IO
Imports System.Xml
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks

Public Class CSpanReader

    Public mXmlInput As String
    Public mSPAN_path As String
    Public mRiskReporterExe As String

    Public mTbl_exposure_database As DataTable
    Public mTbl_exposure_database_new As DataTable
    Public mTbl_ael_contracts As DataTable
    Public mTbl_ael_Additional_Expo As DataTable
    Public mTblSpanOutput As DataTable
    Public mTbl_AEL_Expo_calc As DataTable
    Public mDtTrades As DataTable

    Public mTbl_exposure_comp As DataTable
    Public mTbl_span_calc As DataTable
    Public mTblPicMargin As DataTable

    Public mFlgThr_Span As Boolean
    Public mDouExpMrg As Double
    Public mDouIntMrg As Double

    Public mCompname As String
    Public mMonthName As String

    Public mCurrent_SPAN_file As String
    Public mCurrent_CurSPAN_file As String
    Public mEexp_latest_Curspn_file As String
    Public mExp_latest_spn_file As String
    Public mExp_latest_zip_file As String
    Public mExp_latest_Curzip_file As String


    Public mSPANFile_time1 As DateTime
    Public mSPANFile_time2 As DateTime
    Public mSPANFile_time3 As DateTime
    Public mSPANFile_time4 As DateTime
    Public mSPANFile_time5 As DateTime
    Public mSPANFile_time6 As DateTime
    Public objTrad As trading

    Public mExchange As String
    Public mExchaneSpanFilePath As String

    Dim mPerf As New CPerfCheck
    Public mEqValue As Double
    Public mTotalValue As Double
    Public mCurTable As DataTable
    Public mPicMargin As Double

    Public Sub New()
        mTbl_exposure_database = New DataTable()
        mTbl_exposure_database_new = New DataTable()
        mTbl_ael_contracts = New DataTable()
        mTbl_ael_Additional_Expo = New DataTable()
        mTblSpanOutput = New DataTable()
        mDtTrades = New DataTable()
        mTbl_exposure_comp = New DataTable()
        mTbl_span_calc = New DataTable()
        mTbl_AEL_Expo_calc = New DataTable()
        InitSpanTables()
        objTrad = New trading()

        mTbl_exposure_database = objTrad.Exposure_margin()
        mTbl_exposure_database_new = objTrad.Exposure_Margin_New()
        mTbl_ael_contracts = objTrad.AEL_Contracts()
    End Sub
    Private Sub Thr_Span()
        'Try
        '    mFlgThr_Span = True
        '    If maintable.Rows.Count > 0 Then
        '        If NetMode = CNetworkMode.mNetMode_UDP Or NetMode = CNetworkMode.mNetMode_TCP Or NetMode = CNetworkMode.mNetMode_JL Then
        '            If Directory.Exists(mSPAN_path) Then
        '                generate_SPAN_data_BSE(mExchange, mExchaneSpanFilePath)
        '            End If
        '        Else
        '            If CALMARGINSPAN = 1 Then
        '                If Directory.Exists(mSPAN_path) Then
        '                    generate_SPAN_data_BSE(mExchange, mExchaneSpanFilePath) '(maintable)
        '                End If
        '            Else
        '                generate_SPAN_data_internet()
        '            End If
        '        End If
        '    End If
        'txtexpmrg.Text = 0
        'txtintmrg.Text = 0
        REM Because of Margin show in PopUp Window By Viral

        'txtexpmrg.Invoke(mexpMarg, 0)
        'txtintmrg.Invoke(mintMarg, 0)

        'mDouExpMrg = 0
        '    mDouIntMrg = 0
        '    REM End

        '    If mTblSpanOutput.Rows.Count > 0 Then
        '        'For Each drow As DataRow In mTbl_SPAN_output.Select("ClientCode='" & compname & "'")
        '        For Each drow As DataRow In mTblSpanOutput.Select("ClientCode='" & mCompname & "/" & mMonthName & "'")
        '            REM Because of Margin show in PopUp Window By Viral
        '            'txtexpmrg.Invoke(mexpMarg, CDbl(Format(drow("exposure_margin"), exmargstr)))
        '            'txtintmrg.Invoke(mintMarg, CDbl(Format(Val(txtintmrg.Text) + Val(drow("spanreq").ToString) - Val(drow("anov").ToString), inmargstr)))

        '            'txtexpmrg.Text = CDbl(Format(drow("exposure_margin"), exmargstr))
        '            'txtintmrg.Text = CDbl(Format(Val(txtintmrg.Text) + Val(drow("spanreq").ToString) - Val(drow("anov").ToString), inmargstr))


        '            mDouExpMrg = CDbl(Format(drow("exposure_margin"), exmargstr))
        '            mDouIntMrg = CDbl(Format(mDouIntMrg + Val(drow("spanreq").ToString) - Val(drow("anov").ToString), inmargstr))
        '            'mPerf.SetFileName("MarginTestLog")
        '            'mPerf.WriteLogStr("ExpMarg:" + DouExpMrg.ToString() + " InpMarg:" + DouIntMrg.ToString())
        '            REM End
        '            'txttotmarg.Invoke(mtotMarg)
        '            'txttotmarg.Text = Math.Round((Val(txtintmrg.Text) + Val(txtexpmrg.Text) + Val(txtEquity.Text)) / 100000, 2)
        '        Next

        '    End If

        'Catch ex As Exception
        '    ' MsgBox("Span Calculation error: " & ex.ToString)
        '    mFlgThr_Span = False
        'End Try
        mFlgThr_Span = False
    End Sub
    Private Sub execute_Cur_batch_file()
        'run the batch file
        Try
            Shell(mSPAN_path & "\curgenerate.bat", AppWinStyle.Hide)

            'Process.Start(mSPAN_path & "\generate.bat")
            'MsgBox("job_going_on")

            'wait until generate output.xml file
            While System.IO.File.Exists(mSPAN_path & "\curoutput.xml") = False
                System.Threading.Thread.Sleep(1000)
            End While
        Catch ex As Exception

        End Try
    End Sub

    Private Sub execute_batch_file()
        'run the batch file
        Shell(mSPAN_path & "\generate.bat", AppWinStyle.Hide, True)
        Shell(mSPAN_path & "\curgenerate.bat", AppWinStyle.Hide, True)
        'Process.Start(mSPAN_path & "\generate.bat")
        'MsgBox("job_going_on")

        'wait until generate output.xml file
        While System.IO.File.Exists(mSPAN_path & "\" + mExchange + "output.xml") = False And System.IO.File.Exists(mSPAN_path & "\curoutput.xml") = False
            System.Threading.Thread.Sleep(1000)
        End While

    End Sub


    Public mIsOutputFileGenerated
    Private Sub execute_FO_batch_file()
        'run the batch file
        Try
            mIsOutputFileGenerated = False

            Shell(mSPAN_path & "\generate.bat", AppWinStyle.Hide)

            'Process.Start(mSPAN_path & "\generate.bat")
            'MsgBox("job_going_on")

            ''wait until generate output.xml file
            'While System.IO.File.Exists(mSPAN_path & "\output.xml") = False 'And System.IO.File.Exists(mSPAN_path & "\curoutput.xml") = False
            '    System.Threading.Thread.Sleep(1000)
            'End While

            Dim timeoutMs As Integer = 600000 ' 30 seconds timeout
            Dim waitInterval As Integer = 1000 ' check every 1 second
            Dim waited As Integer = 0

            While Not System.IO.File.Exists(mSPAN_path & "\" + mExchange + "output.xml") AndAlso waited < timeoutMs
                System.Threading.Thread.Sleep(waitInterval)
                waited += waitInterval
            End While

            If System.IO.File.Exists(mSPAN_path & "\" + mExchange + "output.xml") Then
                ' File exists → continue processing
                mIsOutputFileGenerated = True
            Else
                mIsOutputFileGenerated = False
                MessageBox.Show("Timeout: output.xml not found within 30 seconds.")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Function get_expected_latest_spn_file(ByVal current_span_file As String, ByVal sType As String)
        If sType = "FO" Then
            If Today.Now < mSPANFile_time1 Or Today.Now > mSPANFile_time6 Then ''previous day last file
                If Today.DayOfWeek() = DayOfWeek.Saturday Then
                    mExp_latest_spn_file = "nsccl." & Format(DateAdd(DateInterval.Day, -1, Today), "yyyyMMdd") & ".s.spn"
                    mExp_latest_zip_file = "nsccl." & Format(DateAdd(DateInterval.Day, -1, Today), "yyyyMMdd") & ".s.zip"
                ElseIf Today.DayOfWeek() = DayOfWeek.Sunday Then
                    mExp_latest_spn_file = "nsccl." & Format(DateAdd(DateInterval.Day, -2, Today), "yyyyMMdd") & ".s.spn"
                    mExp_latest_zip_file = "nsccl." & Format(DateAdd(DateInterval.Day, -2, Today), "yyyyMMdd") & ".s.zip"
                Else
                    mExp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".s.spn" 'nsccl.20070302.s_1.spn
                    mExp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".s.zip"
                End If
            End If

            If Date.Now >= mSPANFile_time1 And Date.Now < mSPANFile_time2 Then
                mExp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i01.spn" 'nsccl.20070316.i01_1.spn
                mExp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i1.zip"
            ElseIf Date.Now >= mSPANFile_time2 And Date.Now < mSPANFile_time3 Then
                mExp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i02.spn"
                mExp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i2.zip"
            ElseIf Date.Now >= mSPANFile_time3 And Date.Now < mSPANFile_time4 Then
                mExp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i03.spn"
                mExp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i3.zip"
            ElseIf Date.Now >= mSPANFile_time4 And Date.Now < mSPANFile_time5 Then
                mExp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i04.spn"
                mExp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i4.zip"
            ElseIf Date.Now >= mSPANFile_time5 And Date.Now < mSPANFile_time6 Then
                mExp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i05.spn"
                mExp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i5.zip"
            End If
        Else
            If Today.Now < mSPANFile_time1 Or Today.Now > mSPANFile_time6 Then ''previous day last file
                If Today.DayOfWeek() = DayOfWeek.Saturday Then
                    mEexp_latest_Curspn_file = "nsccl_x." & Format(DateAdd(DateInterval.Day, -1, Today), "yyyyMMdd") & ".s.spn"
                    mExp_latest_Curzip_file = "nsccl_x." & Format(DateAdd(DateInterval.Day, -1, Today), "yyyyMMdd") & ".s.zip"
                ElseIf Today.DayOfWeek() = DayOfWeek.Sunday Then
                    mEexp_latest_Curspn_file = "nsccl_x." & Format(DateAdd(DateInterval.Day, -2, Today), "yyyyMMdd") & ".s.spn"
                    mExp_latest_Curzip_file = "nsccl_x." & Format(DateAdd(DateInterval.Day, -2, Today), "yyyyMMdd") & ".s.zip"
                Else
                    mEexp_latest_Curspn_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".s.spn" 'nsccl.20070302.s_1.spn
                    mExp_latest_Curzip_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".s.zip"
                End If
            End If

            If Date.Now >= mSPANFile_time1 And Date.Now < mSPANFile_time2 Then
                mEexp_latest_Curspn_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i01.spn" 'nsccl.20070316.i01_1.spn
                mExp_latest_Curzip_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i1.zip"
            ElseIf Date.Now >= mSPANFile_time2 And Date.Now < mSPANFile_time3 Then
                mEexp_latest_Curspn_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i02.spn"
                mExp_latest_Curzip_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i2.zip"
            ElseIf Date.Now >= mSPANFile_time3 And Date.Now < mSPANFile_time4 Then
                mEexp_latest_Curspn_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i03.spn"
                mExp_latest_Curzip_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i3.zip"
            ElseIf Date.Now >= mSPANFile_time4 And Date.Now < mSPANFile_time5 Then
                mEexp_latest_Curspn_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i04.spn"
                mExp_latest_Curzip_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i4.zip"
            ElseIf Date.Now >= mSPANFile_time5 And Date.Now < mSPANFile_time6 Then
                mEexp_latest_Curspn_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i05.spn"
                mExp_latest_Curzip_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i5.zip"
            End If
        End If

        'If current_span_file <> exp_latest_spn_file And mDownload_inprogess = False Then
        '    Dim worker As New System.Threading.Thread(AddressOf download_file_from_nse)
        '    worker.Start()
        'End If
    End Function


    Public Function GetExpectedLatestSpanFile1(ByVal sType As String) As (spn As String, zip As String)

        Dim prefix As String = If(sType = "FO", "nsccl.", "nsccl_x.")
        Dim dt As Date = Today
        Dim now As Date = Date.Now

        '-----------------------------------------
        ' 1. HANDLE OUTSIDE TIME WINDOW (BEFORE 1ST OR AFTER LAST)
        '-----------------------------------------
        If now < mSPANFile_time1 OrElse now > mSPANFile_time6 Then

            'Weekend adjustment (get previous working day)
            Dim prevDate As Date = dt
            If dt.DayOfWeek = DayOfWeek.Saturday Then
                prevDate = dt.AddDays(-1)
            ElseIf dt.DayOfWeek = DayOfWeek.Sunday Then
                prevDate = dt.AddDays(-2)
            End If

            Dim d As String = prevDate.ToString("yyyyMMdd")
            Return ($"{prefix}{d}.s.spn", $"{prefix}{d}.s.zip")
        End If

        '-----------------------------------------
        ' 2. HANDLE TIME WINDOWS (.i01 to .i05)
        '-----------------------------------------
        Dim todayStr As String = dt.ToString("yyyyMMdd")

        If now >= mSPANFile_time1 AndAlso now < mSPANFile_time2 Then
            Return ($"{prefix}{todayStr}.i01.spn", $"{prefix}{todayStr}.i1.zip")

        ElseIf now >= mSPANFile_time2 AndAlso now < mSPANFile_time3 Then
            Return ($"{prefix}{todayStr}.i02.spn", $"{prefix}{todayStr}.i2.zip")

        ElseIf now >= mSPANFile_time3 AndAlso now < mSPANFile_time4 Then
            Return ($"{prefix}{todayStr}.i03.spn", $"{prefix}{todayStr}.i3.zip")

        ElseIf now >= mSPANFile_time4 AndAlso now < mSPANFile_time5 Then
            Return ($"{prefix}{todayStr}.i04.spn", $"{prefix}{todayStr}.i4.zip")

        Else 'mSPANFile_time5 to mSPANFile_time6
            Return ($"{prefix}{todayStr}.i05.spn", $"{prefix}{todayStr}.i5.zip")
        End If

    End Function


    Public Function get_latest_spn_file(ByVal path As String, ByVal sTyp As String) As String
        Dim di As New IO.DirectoryInfo(path)
        Dim fi As IO.FileInfo
        Dim max As Date
        Dim current As Date
        Dim max_file As String = ""

        REM 2:span filename format: nsccl.20100503.i01.spn (nsccl_X.yyyymmdd.i01.spn) For Currency SpanFile
        REM 1:span filename format: nsccl.20100503.i01.spn (nsccl.yyyymmdd.i01.spn)
        'length of spanfile name: 20chars

        If sTyp = "FO" Then
            Dim aryFi As IO.FileInfo() = di.GetFiles("nsccl*.spn")
            For Each fi In aryFi 'check for available span file
                Debug.WriteLine(fi.Name)
                If Mid(fi.Name(), 16, 1) = "s" Then
                    'If fi.Name.Length > 20 Then
                    '    current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " 23:59:59")
                    'Else
                    If fi.Name.Length = 20 Then
                        current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " 22:59:59")
                    End If
                    'End If
                Else 'filename format:nsccl.yyyymmdd.i01.spn 
                    'If fi.Name.Length > 22 Then
                    '    current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " " & Mid(fi.Name, 18, 1) & ":" & Mid(fi.Name, 20, 1) & ":00")
                    'Else
                    If fi.Name.Length = 22 Then
                        current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " " & Mid(fi.Name, 18, 1) & ":00:00")
                    End If
                    'End If
                End If

                If current > max Then 'if find span file
                    max = current
                    max_file = fi.Name() 'return span file name
                End If
            Next
        Else
            Dim aryFi As IO.FileInfo() = di.GetFiles("nsccl_x*.spn")
            For Each fi In aryFi 'check for available span file
                Debug.WriteLine(fi.Name)
                If Mid(fi.Name(), 18, 1) = "s" Then
                    'If fi.Name.Length > 20 Then
                    '    current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " 23:59:59")
                    'Else
                    If fi.Name.Length = 22 Then
                        current = CDate(Mid(fi.Name(), 9, 4) & "/" & Mid(fi.Name(), 13, 2) & "/" & Mid(fi.Name(), 15, 2) & " 22:59:59")
                    End If
                    'End If
                Else 'filename format:nsccl.yyyymmdd.i01.spn 
                    'If fi.Name.Length > 22 Then
                    '    current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " " & Mid(fi.Name, 18, 1) & ":" & Mid(fi.Name, 20, 1) & ":00")
                    'Else
                    If fi.Name.Length = 24 Then
                        current = CDate(Mid(fi.Name(), 9, 4) & "/" & Mid(fi.Name(), 13, 2) & "/" & Mid(fi.Name(), 15, 2) & " " & Mid(fi.Name, 20, 1) & ":00:00")
                    End If
                    'End If
                End If
                If current > max Then 'if find span file
                    max = current
                    max_file = fi.Name() 'return span file name
                End If
            Next
        End If
        Return max_file
    End Function
    Private Function Additional_AEL_extract_exposure_margin(pExch As String) As Boolean
        Dim sr As StreamReader
        Dim srcur As StreamReader

        Dim drow_output As DataRow
        Dim temp_data As String

        Dim comp_name As String
        Dim exp_date As String
        Dim strike_price As String
        Dim cal_put_fut As String

        Dim fut_comp_name As String

        Try
            mTbl_exposure_comp.Rows.Clear()
            mTblSpanOutput.Rows.Clear()
            mTbl_span_calc.Rows.Clear()
            'check if output.xml exists

            Dim outputXmlFile As String = mSPAN_path & "\" + pExch + "output.xml"

            '   outputXmlFile = "c:\shailesh\nseoutput.xml"

            If System.IO.File.Exists(outputXmlFile) = True Then
                Try
Read_spn_output:
                    sr = New IO.StreamReader(outputXmlFile)
                Catch ex As Exception
                    System.Threading.Thread.Sleep(100)
                    GoTo Read_spn_output
                End Try

                Dim s As New Xml.XmlTextReader(sr)
                Dim got_span_req As Boolean = False
                's.WhitespaceHandling = Xml.WhitespaceHandling.None

                While s.Read
                    If s.Name = "phyPf" Then
                        Exit While
                    End If
                End While
                Try


                    While True
                        Select Case s.Name
                            Case "phyPf" 'within <phyPf> contains Equity's LTP
                                s.Read()
                                temp_data = s.ReadElementString("pfId").ToString
                                drow_output = mTbl_exposure_comp.NewRow
                                drow_output("CompName") = s.ReadElementString("pfCode").ToString
                                drow_output("fut_opt") = "OPT"

                                While True
                                    s.Read()
                                    Select Case s.Name
                                        Case "phy" 'Equity
                                            s.Read()

                                            temp_data = s.ReadElementString("cId").ToString
                                            temp_data = s.ReadElementString("pe").ToString
                                            drow_output("p") = Val(s.ReadElementString("p"))
                                            mTbl_exposure_comp.Rows.Add(drow_output)

                                            While True
                                                s.Read()

                                                Select Case s.Name
                                                    Case "phy"
                                                        Exit While
                                                End Select
                                            End While
                                        Case "phyPf"
                                            Exit While
                                    End Select
                                End While
                            Case "futPf" 'for future
                                s.Read()
                                temp_data = s.ReadElementString("pfId").ToString

                                'got_span_req = False
                                fut_comp_name = s.ReadElementString("pfCode").ToString

                                While True
                                    s.Read()

                                    Select Case s.Name
                                        Case "fut" 'future's LTP
                                            s.Read()

                                            'If got_span_req = False Then
                                            drow_output = mTbl_exposure_comp.NewRow
                                            drow_output("CompName") = fut_comp_name
                                            drow_output("fut_opt") = "FUT"

                                            temp_data = s.ReadElementString("cId").ToString
                                            temp_data = s.ReadElementString("pe").ToString
                                            drow_output("mat_month") = CInt(Mid(temp_data, 5, 2))
                                            drow_output("p") = Val(s.ReadElementString("p"))
                                            mTbl_exposure_comp.Rows.Add(drow_output)
                                            'got_span_req = True
                                            'End If
                                            While True
                                                s.Read()

                                                Select Case s.Name
                                                    Case "fut"
                                                        Exit While
                                                End Select
                                            End While
                                        Case "futPf"
                                            Exit While
                                    End Select
                                End While
                            Case "oopPf" 'options
                                s.Read()
                                temp_data = s.ReadElementString("pfId").ToString
                                comp_name = s.ReadElementString("pfCode").ToString
                                While True
                                    s.Read()

                                    Select Case s.Name
                                        Case "series"
                                            s.Read()

                                            temp_data = s.ReadElementString("pe").ToString
                                            temp_data = s.ReadElementString("v").ToString
                                            temp_data = s.ReadElementString("volSrc").ToString
                                            temp_data = s.ReadElementString("setlDate").ToString

                                            exp_date = Mid(temp_data, 7, 2) & Format(CDate(Mid(temp_data, 5, 2) & "/01/2000"), "MMM") & Mid(temp_data, 1, 4)
                                            cal_put_fut = "FUT"
                                            strike_price = ""

                                            drow_output = mTbl_span_calc.NewRow
                                            drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                            drow_output("compname") = comp_name
                                            drow_output("cal_put_fut") = cal_put_fut
                                            drow_output("strike_price") = strike_price
                                            drow_output("exp_date") = exp_date
                                            mTbl_span_calc.Rows.Add(drow_output)

                                            While True
                                                s.Read()

                                                Select Case s.Name
                                                    Case "opt"
                                                        s.Read()

                                                        temp_data = s.ReadElementString("cId").ToString
                                                        temp_data = s.ReadElementString("o").ToString
                                                        If temp_data = "C" Then
                                                            strike_price = s.ReadElementString("k").ToString

                                                            cal_put_fut = "CAL"
                                                            drow_output = mTbl_span_calc.NewRow
                                                            drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                                            drow_output("compname") = comp_name
                                                            drow_output("cal_put_fut") = cal_put_fut
                                                            drow_output("strike_price") = strike_price
                                                            drow_output("exp_date") = exp_date
                                                            mTbl_span_calc.Rows.Add(drow_output)

                                                            cal_put_fut = "PUT"
                                                            drow_output = mTbl_span_calc.NewRow
                                                            drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                                            drow_output("compname") = comp_name
                                                            drow_output("cal_put_fut") = cal_put_fut
                                                            drow_output("strike_price") = strike_price
                                                            drow_output("exp_date") = exp_date
                                                            mTbl_span_calc.Rows.Add(drow_output)
                                                        End If

                                                        While True
                                                            s.Read()

                                                            Select Case s.Name
                                                                Case "opt"
                                                                    Exit While
                                                            End Select
                                                        End While
                                                    Case "series"
                                                        Exit While
                                                End Select
                                            End While
                                        Case "oopPf"
                                            Exit While
                                    End Select
                                End While
                            Case "portfolio"

                                While True
                                    Select Case s.Name
                                        Case "portfolio"
                                            While True
                                                s.Read()

                                                Select Case s.Name
                                                    Case "firm"
                                                        got_span_req = False
                                                        drow_output = mTblSpanOutput.NewRow
                                                        temp_data = s.ReadElementString("firm")
                                                        drow_output("ClientCode") = temp_data '//Viral  instade of accid
                                                        temp_data = s.ReadElementString("acctId").ToString()
                                                        temp_data = s.ReadElementString("acctType")
                                                        temp_data = s.ReadElementString("isCust")
                                                        temp_data = s.ReadElementString("seg")
                                                        '  Try

                                                        'REM:For new Span 4.5
                                                        s.Read()

                                                        If s.Name = "acctSubType" Then
                                                            's.Read()
                                                            While s.Read
                                                                If s.Name = "isNew" Then

                                                                    Exit While
                                                                Else
                                                                    '    s.Read()
                                                                End If
                                                            End While

                                                        End If


                                                        'end

                                                        temp_data = s.ReadElementString("isNew")
                                                        '                                        Catch ex As Exception

                                                        'End Try
                                                        'REM:For new Span 4.5
                                                        'temp_data = s.ReadElementString("pclient")
                                                        temp_data = s.ReadElementString("custPortUseLov")
                                                        'end
                                                        temp_data = s.ReadElementString("currency")
                                                        temp_data = s.ReadElementString("ledgerBal")
                                                        temp_data = s.ReadElementString("ote")
                                                        temp_data = s.ReadElementString("securities")
                                                        'REM:For new Span 4.5
                                                        temp_data = s.ReadElementString("lue")
                                                        'end

                                                        drow_output("lfv") = Val(s.ReadElementString("lfv"))
                                                        drow_output("sfv") = Val(s.ReadElementString("sfv"))
                                                        drow_output("lov") = Val(s.ReadElementString("lov"))
                                                        drow_output("sov") = Val(s.ReadElementString("sov"))
                                                    Case "spanReq"
                                                        If got_span_req = False Then
                                                            drow_output("spanreq") = Val(s.ReadElementString("spanReq"))
                                                            drow_output("anov") = Val(s.ReadElementString("anov"))
                                                            If (drow_output("spanreq") - drow_output("anov")) <= 0 Then
                                                                drow_output("spanreq") = 0
                                                                drow_output("anov") = 0
                                                            End If
                                                            got_span_req = True
                                                        End If
                                                    Case "portfolio"
                                                        drow_output("exposure_margin") = 0
                                                        mTblSpanOutput.Rows.Add(drow_output)
                                                        Exit While
                                                End Select
                                            End While
                                        Case "spanFile"
                                            Exit While
                                    End Select
                                    s.Read()

                                End While

                                Exit While
                        End Select
                        s.Read()

                    End While
                Catch ex As Exception

                End Try
            End If
            If Not IsNothing(sr) Then sr.Close()




            REM Currency By Viral
            '==============================================================
            If System.IO.File.Exists(mSPAN_path & "\curoutput.xml") = True Then
                Try
Read_curspn_output:
                    srcur = New IO.StreamReader(mSPAN_path & "\curoutput.xml")
                Catch ex As Exception
                    System.Threading.Thread.Sleep(100)
                    GoTo Read_curspn_output
                End Try

                Dim s As New Xml.XmlTextReader(srcur)
                Dim got_span_req As Boolean = False
                's.WhitespaceHandling = Xml.WhitespaceHandling.None

                While s.Read
                    If s.Name = "phyPf" Then
                        Exit While
                    End If
                End While

                While True
                    Select Case s.Name
                        Case "phyPf" 'within <phyPf> contains Equity's LTP
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString
                            drow_output = mTbl_exposure_comp.NewRow
                            drow_output("CompName") = s.ReadElementString("pfCode").ToString
                            drow_output("fut_opt") = "OPT"


                            While True
                                s.Read()
                                Select Case s.Name
                                    Case "phy" 'Equity
                                        s.Read()

                                        temp_data = s.ReadElementString("cId").ToString
                                        temp_data = s.ReadElementString("pe").ToString
                                        drow_output("p") = Val(s.ReadElementString("p"))
                                        mTbl_exposure_comp.Rows.Add(drow_output)

                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "phy"
                                                    Exit While
                                            End Select
                                        End While
                                    Case "phyPf"
                                        Exit While
                                End Select
                            End While
                        Case "futPf" 'for future
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString

                            'got_span_req = False
                            fut_comp_name = s.ReadElementString("pfCode").ToString

                            While True
                                s.Read()

                                Select Case s.Name
                                    Case "fut" 'future's LTP
                                        s.Read()

                                        'If got_span_req = False Then
                                        drow_output = mTbl_exposure_comp.NewRow
                                        drow_output("CompName") = fut_comp_name
                                        drow_output("fut_opt") = "FUT"

                                        temp_data = s.ReadElementString("cId").ToString
                                        temp_data = s.ReadElementString("pe").ToString
                                        drow_output("mat_month") = CInt(Mid(temp_data, 5, 2))
                                        drow_output("p") = Val(s.ReadElementString("p"))
                                        mTbl_exposure_comp.Rows.Add(drow_output)
                                        'got_span_req = True
                                        'End If
                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "fut"
                                                    Exit While
                                            End Select
                                        End While
                                    Case "futPf"
                                        Exit While
                                End Select
                            End While
                        Case "oopPf" 'options
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString
                            comp_name = s.ReadElementString("pfCode").ToString
                            While True
                                s.Read()

                                Select Case s.Name
                                    Case "series"
                                        s.Read()

                                        temp_data = s.ReadElementString("pe").ToString
                                        temp_data = s.ReadElementString("v").ToString
                                        temp_data = s.ReadElementString("volSrc").ToString
                                        temp_data = s.ReadElementString("setlDate").ToString

                                        exp_date = Mid(temp_data, 7, 2) & Format(CDate(Mid(temp_data, 5, 2) & "/01/2000"), "MMM") & Mid(temp_data, 1, 4)
                                        cal_put_fut = "FUT"
                                        strike_price = ""

                                        drow_output = mTbl_span_calc.NewRow
                                        drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                        drow_output("compname") = comp_name
                                        drow_output("cal_put_fut") = cal_put_fut
                                        drow_output("strike_price") = strike_price
                                        drow_output("exp_date") = exp_date
                                        mTbl_span_calc.Rows.Add(drow_output)

                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "opt"
                                                    s.Read()

                                                    temp_data = s.ReadElementString("cId").ToString
                                                    temp_data = s.ReadElementString("o").ToString
                                                    If temp_data = "C" Then
                                                        strike_price = s.ReadElementString("k").ToString

                                                        cal_put_fut = "CAL"
                                                        drow_output = mTbl_span_calc.NewRow
                                                        drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                                        drow_output("compname") = comp_name
                                                        drow_output("cal_put_fut") = cal_put_fut
                                                        drow_output("strike_price") = strike_price
                                                        drow_output("exp_date") = exp_date
                                                        mTbl_span_calc.Rows.Add(drow_output)

                                                        cal_put_fut = "PUT"
                                                        drow_output = mTbl_span_calc.NewRow
                                                        drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                                        drow_output("compname") = comp_name
                                                        drow_output("cal_put_fut") = cal_put_fut
                                                        drow_output("strike_price") = strike_price
                                                        drow_output("exp_date") = exp_date
                                                        mTbl_span_calc.Rows.Add(drow_output)
                                                    End If

                                                    While True
                                                        s.Read()

                                                        Select Case s.Name
                                                            Case "opt"
                                                                Exit While
                                                        End Select
                                                    End While
                                                Case "series"
                                                    Exit While
                                            End Select
                                        End While
                                    Case "oopPf"
                                        Exit While
                                End Select
                            End While
                        Case "portfolio"

                            While True
                                Select Case s.Name
                                    Case "portfolio"
                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "firm"
                                                    got_span_req = False
                                                    drow_output = mTblSpanOutput.NewRow
                                                    temp_data = s.ReadElementString("firm")
                                                    drow_output("ClientCode") = temp_data '//Viral Instade of acctId
                                                    temp_data = s.ReadElementString("acctId").ToString()
                                                    temp_data = s.ReadElementString("acctType")
                                                    temp_data = s.ReadElementString("isCust")
                                                    temp_data = s.ReadElementString("seg")
                                                    'REM:For new Span 4.5
                                                    s.Read()

                                                    If s.Name = "acctSubType" Then
                                                        's.Read()
                                                        While s.Read
                                                            If s.Name = "isNew" Then

                                                                Exit While
                                                            Else
                                                                '  s.Read()
                                                            End If
                                                        End While

                                                    End If
                                                    'end
                                                    temp_data = s.ReadElementString("isNew")
                                                    'REM:For new Span 4.5
                                                    'temp_data = s.ReadElementString("pclient")
                                                    temp_data = s.ReadElementString("custPortUseLov")
                                                    'end
                                                    temp_data = s.ReadElementString("currency")
                                                    temp_data = s.ReadElementString("ledgerBal")
                                                    temp_data = s.ReadElementString("ote")


                                                    temp_data = s.ReadElementString("securities")
                                                    'REM:For new Span 4.5
                                                    temp_data = s.ReadElementString("lue")
                                                    'end


                                                    drow_output("lfv") = Val(s.ReadElementString("lfv"))
                                                    drow_output("sfv") = Val(s.ReadElementString("sfv"))
                                                    drow_output("lov") = Val(s.ReadElementString("lov"))
                                                    drow_output("sov") = Val(s.ReadElementString("sov"))
                                                Case "spanReq"
                                                    If got_span_req = False Then
                                                        drow_output("spanreq") = Val(s.ReadElementString("spanReq"))
                                                        drow_output("anov") = Val(s.ReadElementString("anov"))
                                                        If (drow_output("spanreq") - drow_output("anov")) <= 0 Then
                                                            drow_output("spanreq") = 0
                                                            drow_output("anov") = 0
                                                        End If
                                                        got_span_req = True
                                                    End If
                                                Case "portfolio"
                                                    drow_output("exposure_margin") = 0
                                                    mTblSpanOutput.Rows.Add(drow_output)
                                                    Exit While
                                            End Select
                                        End While
                                    Case "spanFile"
                                        Exit While
                                End Select
                                s.Read()

                            End While

                            Exit While
                    End Select
                    s.Read()

                End While

            End If
            sr?.Dispose()
            srcur?.Close()
            srcur?.Dispose()

            For Each dr As DataRow In mTblSpanOutput.Rows

                Dim expoper As Double = 0
                If mTbl_AEL_Expo_calc.Columns.Contains("Company") Then
                    Dim clientCode As String = dr("ClientCode".Trim()).ToString()

                    Dim matchingRows As DataRow() = mTbl_AEL_Expo_calc.Select("Company = '" & clientCode & "'")

                    For Each row As DataRow In matchingRows
                        expoper += Convert.ToDouble(row("AEL_EXPOSURE"))
                    Next
                End If

                If mTblSpanOutput.Columns.Contains("exposure_margin") Then
                    dr("exposure_margin") = Convert.ToDouble(dr("exposure_margin")) + expoper
                End If
            Next
            mTblSpanOutput.AcceptChanges()

            Return True
        Catch ex As Exception
            MsgBox("Error in Function 'extract_exposure_margin' ::" & ex.Message)
            Return False
        End Try
    End Function


    Private Sub InitSpanTables()

        With mTbl_exposure_comp
            .Columns.Add("CompName", GetType(String))
            .Columns.Add("mat_month", GetType(Integer))
            .Columns.Add("p", GetType(Double))
            .Columns.Add("fut_opt", GetType(String))
        End With

        With mTblSpanOutput
            .Columns.Add("ClientCode", GetType(String)) ' company name
            .Columns.Add("lfv", GetType(Double))
            .Columns.Add("sfv", GetType(Double))
            .Columns.Add("lov", GetType(Double))
            .Columns.Add("sov", GetType(Double))
            .Columns.Add("spanreq", GetType(Double)) ' for initial margin  spanreq-anvo
            .Columns.Add("anov", GetType(Double))
            .Columns.Add("exposure_margin", GetType(Double))
        End With

        With mTbl_span_calc
            .Columns.Add("description", GetType(String))
            .Columns.Add("compname", GetType(String))
            .Columns.Add("exp_date", GetType(String))
            .Columns.Add("cal_put_fut", GetType(String))
            .Columns.Add("strike_price", GetType(String))
        End With

        With mTbl_AEL_Expo_calc
            .Columns.Add("AEL_EXPOSURE", GetType(String))
            .Columns.Add("Company", GetType(String))
        End With


        'use to calculate pic margin
        mTblPicMargin = New DataTable
        With mTblPicMargin.Columns
            .Add("compname")
            .Add("Margin", GetType(Double))
            .Add("Date", GetType(Date))

        End With

    End Sub

    Private Function extract_exposure_margin() As Boolean
        Dim sr As StreamReader
        Dim srcur As StreamReader

        Dim drow_output As DataRow
        Dim temp_data As String

        Dim comp_name As String
        Dim exp_date As String
        Dim strike_price As String
        Dim cal_put_fut As String

        Dim fut_comp_name As String

        Try

            mTbl_exposure_comp.Rows.Clear()
            mTblSpanOutput.Rows.Clear()
            mTbl_span_calc.Rows.Clear()
            'check if output.xml exists
            If System.IO.File.Exists(mSPAN_path & "\" + mExchange + "output.xml") = True Then
                Try
Read_spn_output:
                    Dim fileOutxml As String = mSPAN_path & "\" + mExchange + "output.xml"
                    If Not File.Exists(fileOutxml) Then Return 0
                    sr = New IO.StreamReader(fileOutxml)
                Catch ex As Exception
                    System.Threading.Thread.Sleep(100)
                    GoTo Read_spn_output
                End Try

                Dim s As New Xml.XmlTextReader(sr)
                Dim got_span_req As Boolean = False
                's.WhitespaceHandling = Xml.WhitespaceHandling.None

                While s.Read
                    If s.Name = "phyPf" Then
                        Exit While
                    End If
                End While
                Try


                    While True
                        Select Case s.Name
                            Case "phyPf" 'within <phyPf> contains Equity's LTP
                                s.Read()
                                temp_data = s.ReadElementString("pfId").ToString
                                drow_output = mTbl_exposure_comp.NewRow
                                drow_output("CompName") = s.ReadElementString("pfCode").ToString
                                drow_output("fut_opt") = "OPT"



                                While True
                                    s.Read()
                                    Select Case s.Name
                                        Case "phy" 'Equity
                                            s.Read()

                                            temp_data = s.ReadElementString("cId").ToString
                                            temp_data = s.ReadElementString("pe").ToString
                                            drow_output("p") = Val(s.ReadElementString("p"))
                                            mTbl_exposure_comp.Rows.Add(drow_output)

                                            While True
                                                s.Read()

                                                Select Case s.Name
                                                    Case "phy"
                                                        Exit While
                                                End Select
                                            End While
                                        Case "phyPf"
                                            Exit While
                                    End Select
                                End While
                            Case "futPf" 'for future
                                s.Read()
                                temp_data = s.ReadElementString("pfId").ToString

                                'got_span_req = False
                                fut_comp_name = s.ReadElementString("pfCode").ToString

                                While True
                                    s.Read()

                                    Select Case s.Name
                                        Case "fut" 'future's LTP
                                            s.Read()

                                            'If got_span_req = False Then
                                            drow_output = mTbl_exposure_comp.NewRow
                                            drow_output("CompName") = fut_comp_name
                                            drow_output("fut_opt") = "FUT"

                                            temp_data = s.ReadElementString("cId").ToString
                                            temp_data = s.ReadElementString("pe").ToString
                                            drow_output("mat_month") = CInt(Mid(temp_data, 5, 2))
                                            drow_output("p") = Val(s.ReadElementString("p"))
                                            mTbl_exposure_comp.Rows.Add(drow_output)
                                            'got_span_req = True
                                            'End If
                                            While True
                                                s.Read()

                                                Select Case s.Name
                                                    Case "fut"
                                                        Exit While
                                                End Select
                                            End While
                                        Case "futPf"
                                            Exit While
                                    End Select
                                End While
                            Case "oopPf" 'options
                                s.Read()
                                temp_data = s.ReadElementString("pfId").ToString
                                comp_name = s.ReadElementString("pfCode").ToString
                                While True
                                    s.Read()

                                    Select Case s.Name
                                        Case "series"
                                            s.Read()

                                            temp_data = s.ReadElementString("pe").ToString
                                            temp_data = s.ReadElementString("v").ToString
                                            temp_data = s.ReadElementString("volSrc").ToString
                                            temp_data = s.ReadElementString("setlDate").ToString

                                            exp_date = Mid(temp_data, 7, 2) & Format(CDate(Mid(temp_data, 5, 2) & "/01/2000"), "MMM") & Mid(temp_data, 1, 4)
                                            cal_put_fut = "FUT"
                                            strike_price = ""

                                            drow_output = mTbl_span_calc.NewRow
                                            drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                            drow_output("compname") = comp_name
                                            drow_output("cal_put_fut") = cal_put_fut
                                            drow_output("strike_price") = strike_price
                                            drow_output("exp_date") = exp_date
                                            mTbl_span_calc.Rows.Add(drow_output)

                                            While True
                                                s.Read()

                                                Select Case s.Name
                                                    Case "opt"
                                                        s.Read()

                                                        temp_data = s.ReadElementString("cId").ToString
                                                        temp_data = s.ReadElementString("o").ToString
                                                        If temp_data = "C" Then
                                                            strike_price = s.ReadElementString("k").ToString

                                                            cal_put_fut = "CAL"
                                                            drow_output = mTbl_span_calc.NewRow
                                                            drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                                            drow_output("compname") = comp_name
                                                            drow_output("cal_put_fut") = cal_put_fut
                                                            drow_output("strike_price") = strike_price
                                                            drow_output("exp_date") = exp_date
                                                            mTbl_span_calc.Rows.Add(drow_output)

                                                            cal_put_fut = "PUT"
                                                            drow_output = mTbl_span_calc.NewRow
                                                            drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                                            drow_output("compname") = comp_name
                                                            drow_output("cal_put_fut") = cal_put_fut
                                                            drow_output("strike_price") = strike_price
                                                            drow_output("exp_date") = exp_date
                                                            mTbl_span_calc.Rows.Add(drow_output)
                                                        End If

                                                        While True
                                                            s.Read()

                                                            Select Case s.Name
                                                                Case "opt"
                                                                    Exit While
                                                            End Select
                                                        End While
                                                    Case "series"
                                                        Exit While
                                                End Select
                                            End While
                                        Case "oopPf"
                                            Exit While
                                    End Select
                                End While
                            Case "portfolio"

                                While True
                                    Select Case s.Name
                                        Case "portfolio"
                                            While True
                                                s.Read()

                                                Select Case s.Name
                                                    Case "firm"
                                                        got_span_req = False
                                                        drow_output = mTblSpanOutput.NewRow
                                                        temp_data = s.ReadElementString("firm")
                                                        drow_output("ClientCode") = temp_data '//Viral  instade of accid
                                                        temp_data = s.ReadElementString("acctId").ToString()
                                                        temp_data = s.ReadElementString("acctType")
                                                        temp_data = s.ReadElementString("isCust")
                                                        temp_data = s.ReadElementString("seg")
                                                        '  Try

                                                        'REM:For new Span 4.5
                                                        s.Read()

                                                        If s.Name = "acctSubType" Then
                                                            's.Read()
                                                            While s.Read
                                                                If s.Name = "isNew" Then

                                                                    Exit While
                                                                Else
                                                                    '    s.Read()
                                                                End If
                                                            End While

                                                        End If


                                                        'end

                                                        temp_data = s.ReadElementString("isNew")
                                                        '                                        Catch ex As Exception

                                                        'End Try
                                                        'REM:For new Span 4.5
                                                        'temp_data = s.ReadElementString("pclient")
                                                        temp_data = s.ReadElementString("custPortUseLov")
                                                        'end
                                                        temp_data = s.ReadElementString("currency")
                                                        temp_data = s.ReadElementString("ledgerBal")
                                                        temp_data = s.ReadElementString("ote")
                                                        temp_data = s.ReadElementString("securities")
                                                        'REM:For new Span 4.5
                                                        temp_data = s.ReadElementString("lue")
                                                        'end

                                                        drow_output("lfv") = Val(s.ReadElementString("lfv"))
                                                        drow_output("sfv") = Val(s.ReadElementString("sfv"))
                                                        drow_output("lov") = Val(s.ReadElementString("lov"))
                                                        drow_output("sov") = Val(s.ReadElementString("sov"))
                                                    Case "spanReq"
                                                        If got_span_req = False Then
                                                            drow_output("spanreq") = Val(s.ReadElementString("spanReq"))
                                                            drow_output("anov") = Val(s.ReadElementString("anov"))
                                                            If (drow_output("spanreq") - drow_output("anov")) <= 0 Then
                                                                drow_output("spanreq") = 0
                                                                drow_output("anov") = 0
                                                            End If
                                                            got_span_req = True
                                                        End If
                                                    Case "portfolio"
                                                        drow_output("exposure_margin") = 0
                                                        mTblSpanOutput.Rows.Add(drow_output)
                                                        Exit While
                                                End Select
                                            End While
                                        Case "spanFile"
                                            Exit While
                                    End Select
                                    s.Read()

                                End While

                                Exit While
                        End Select
                        s.Read()

                    End While
                Catch ex As Exception

                End Try
            End If
            If Not IsNothing(sr) Then sr.Close()
            REM Currency By Viral
            '==============================================================
            If System.IO.File.Exists(mSPAN_path & "\curoutput.xml") = True Then
                Try
Read_curspn_output:
                    srcur = New IO.StreamReader(mSPAN_path & "\curoutput.xml")
                Catch ex As Exception
                    System.Threading.Thread.Sleep(100)
                    GoTo Read_curspn_output
                End Try

                Dim s As New Xml.XmlTextReader(srcur)
                Dim got_span_req As Boolean = False
                's.WhitespaceHandling = Xml.WhitespaceHandling.None

                While s.Read
                    If s.Name = "phyPf" Then
                        Exit While
                    End If
                End While

                While True
                    Select Case s.Name
                        Case "phyPf" 'within <phyPf> contains Equity's LTP
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString
                            drow_output = mTbl_exposure_comp.NewRow
                            drow_output("CompName") = s.ReadElementString("pfCode").ToString
                            drow_output("fut_opt") = "OPT"


                            While True
                                s.Read()
                                Select Case s.Name
                                    Case "phy" 'Equity
                                        s.Read()

                                        temp_data = s.ReadElementString("cId").ToString
                                        temp_data = s.ReadElementString("pe").ToString
                                        drow_output("p") = Val(s.ReadElementString("p"))
                                        mTbl_exposure_comp.Rows.Add(drow_output)

                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "phy"
                                                    Exit While
                                            End Select
                                        End While
                                    Case "phyPf"
                                        Exit While
                                End Select
                            End While
                        Case "futPf" 'for future
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString

                            'got_span_req = False
                            fut_comp_name = s.ReadElementString("pfCode").ToString

                            While True
                                s.Read()

                                Select Case s.Name
                                    Case "fut" 'future's LTP
                                        s.Read()

                                        'If got_span_req = False Then
                                        drow_output = mTbl_exposure_comp.NewRow
                                        drow_output("CompName") = fut_comp_name
                                        drow_output("fut_opt") = "FUT"

                                        temp_data = s.ReadElementString("cId").ToString
                                        temp_data = s.ReadElementString("pe").ToString
                                        drow_output("mat_month") = CInt(Mid(temp_data, 5, 2))
                                        drow_output("p") = Val(s.ReadElementString("p"))
                                        mTbl_exposure_comp.Rows.Add(drow_output)
                                        'got_span_req = True
                                        'End If
                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "fut"
                                                    Exit While
                                            End Select
                                        End While
                                    Case "futPf"
                                        Exit While
                                End Select
                            End While
                        Case "oopPf" 'options
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString
                            comp_name = s.ReadElementString("pfCode").ToString
                            While True
                                s.Read()

                                Select Case s.Name
                                    Case "series"
                                        s.Read()

                                        temp_data = s.ReadElementString("pe").ToString
                                        temp_data = s.ReadElementString("v").ToString
                                        temp_data = s.ReadElementString("volSrc").ToString
                                        temp_data = s.ReadElementString("setlDate").ToString

                                        exp_date = Mid(temp_data, 7, 2) & Format(CDate(Mid(temp_data, 5, 2) & "/01/2000"), "MMM") & Mid(temp_data, 1, 4)
                                        cal_put_fut = "FUT"
                                        strike_price = ""

                                        drow_output = mTbl_span_calc.NewRow
                                        drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                        drow_output("compname") = comp_name
                                        drow_output("cal_put_fut") = cal_put_fut
                                        drow_output("strike_price") = strike_price
                                        drow_output("exp_date") = exp_date
                                        mTbl_span_calc.Rows.Add(drow_output)

                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "opt"
                                                    s.Read()

                                                    temp_data = s.ReadElementString("cId").ToString
                                                    temp_data = s.ReadElementString("o").ToString
                                                    If temp_data = "C" Then
                                                        strike_price = s.ReadElementString("k").ToString

                                                        cal_put_fut = "CAL"
                                                        drow_output = mTbl_span_calc.NewRow
                                                        drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                                        drow_output("compname") = comp_name
                                                        drow_output("cal_put_fut") = cal_put_fut
                                                        drow_output("strike_price") = strike_price
                                                        drow_output("exp_date") = exp_date
                                                        mTbl_span_calc.Rows.Add(drow_output)

                                                        cal_put_fut = "PUT"
                                                        drow_output = mTbl_span_calc.NewRow
                                                        drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                                        drow_output("compname") = comp_name
                                                        drow_output("cal_put_fut") = cal_put_fut
                                                        drow_output("strike_price") = strike_price
                                                        drow_output("exp_date") = exp_date
                                                        mTbl_span_calc.Rows.Add(drow_output)
                                                    End If

                                                    While True
                                                        s.Read()

                                                        Select Case s.Name
                                                            Case "opt"
                                                                Exit While
                                                        End Select
                                                    End While
                                                Case "series"
                                                    Exit While
                                            End Select
                                        End While
                                    Case "oopPf"
                                        Exit While
                                End Select
                            End While
                        Case "portfolio"

                            While True
                                Select Case s.Name
                                    Case "portfolio"
                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "firm"
                                                    got_span_req = False
                                                    drow_output = mTblSpanOutput.NewRow
                                                    temp_data = s.ReadElementString("firm")
                                                    drow_output("ClientCode") = temp_data '//Viral Instade of acctId
                                                    temp_data = s.ReadElementString("acctId").ToString()
                                                    temp_data = s.ReadElementString("acctType")
                                                    temp_data = s.ReadElementString("isCust")
                                                    temp_data = s.ReadElementString("seg")
                                                    'REM:For new Span 4.5
                                                    s.Read()

                                                    If s.Name = "acctSubType" Then
                                                        's.Read()
                                                        While s.Read
                                                            If s.Name = "isNew" Then

                                                                Exit While
                                                            Else
                                                                '  s.Read()
                                                            End If
                                                        End While

                                                    End If
                                                    'end
                                                    temp_data = s.ReadElementString("isNew")
                                                    'REM:For new Span 4.5
                                                    'temp_data = s.ReadElementString("pclient")
                                                    temp_data = s.ReadElementString("custPortUseLov")
                                                    'end
                                                    temp_data = s.ReadElementString("currency")
                                                    temp_data = s.ReadElementString("ledgerBal")
                                                    temp_data = s.ReadElementString("ote")


                                                    temp_data = s.ReadElementString("securities")
                                                    'REM:For new Span 4.5
                                                    temp_data = s.ReadElementString("lue")
                                                    'end


                                                    drow_output("lfv") = Val(s.ReadElementString("lfv"))
                                                    drow_output("sfv") = Val(s.ReadElementString("sfv"))
                                                    drow_output("lov") = Val(s.ReadElementString("lov"))
                                                    drow_output("sov") = Val(s.ReadElementString("sov"))
                                                Case "spanReq"
                                                    If got_span_req = False Then
                                                        drow_output("spanreq") = Val(s.ReadElementString("spanReq"))
                                                        drow_output("anov") = Val(s.ReadElementString("anov"))
                                                        If (drow_output("spanreq") - drow_output("anov")) <= 0 Then
                                                            drow_output("spanreq") = 0
                                                            drow_output("anov") = 0
                                                        End If
                                                        got_span_req = True
                                                    End If
                                                Case "portfolio"
                                                    drow_output("exposure_margin") = 0
                                                    mTblSpanOutput.Rows.Add(drow_output)
                                                    Exit While
                                            End Select
                                        End While
                                    Case "spanFile"
                                        Exit While
                                End Select
                                s.Read()

                            End While

                            Exit While
                    End Select
                    s.Read()

                End While

            End If
            srcur?.Dispose()
            Return True
        Catch ex As Exception
            sr?.Dispose()
            srcur?.Dispose()
            MsgBox("Error in Function 'extract_exposure_margin' ::" & ex.Message)
            Return False
        End Try
    End Function

    Public Function GetExposureObject(ByVal CalcOption As Integer, ByVal mREt_obj1 As Object, ByVal Script As String, ByVal Symbol As String, ByVal Expiry As Date, ByVal StrikePrice As Double _
                                      , ByVal type As String) As Object
        Dim mRes_Obj1 As New Object
        Dim isOTM As Int16

        Dim strExpMarginLog As String = ""

        Try
            Write_ExposureMarginLog(" === Function Start ==== ")

            strExpMarginLog &= " CalcOption: '" & CalcOption.ToString & "'"

            If CalcOption = 1 Then
                mRes_Obj1 = mREt_obj1
            ElseIf CalcOption = 2 Then
                If GdtBhavcopy.Rows.Count = 0 Then
                    Exit Function
                End If

                LastBhavcopyDate = (GdtBhavcopy.Compute("MAX(entry_date)", ""))

                strExpMarginLog &= " LastBhavcopyDate: '" & LastBhavcopyDate.ToString & "'"
                strExpMarginLog &= " Script: '" & Script.ToString & "'"

                If type = "F" Then
                    If Script.ToString.Substring(3, 3) = "IDX" Then
                        mRes_Obj1 = INDEX_OTH_OPTION  'OTH
                    Else
                        mRes_Obj1 = mTbl_exposure_database_new.Select("symbol='" & Symbol & "' and InsType='OTH'")(0).Item("Total_Margin") 'OTH
                    End If

                Else
                    If DateDiff(DateInterval.Month, Expiry, Today) >= 9 Then
                        isOTM = 1
                    Else
                        isOTM = 0 'OTH
                    End If

                    mPerf.SetFileName("ExpoMargin")
                    mPerf.WriteLogStr(Script)
                    If Script.ToString.Substring(3, 3) = "IDX" Then
                        If type = "C" Then

                            'If isOTM = 1 And StrikePrice >= GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp") * 1.1 Then
                            strExpMarginLog &= " isOTM: '" & isOTM.ToString & "'"
                            If isOTM = 1 Then

                                mRes_Obj1 = INDEX_FAR_MONTH_OPTION 'mTbl_exposure_database_new.Select("symbol='" & Symbol & "' and InsType='OTM'")(0).Item("Total_Margin")
                            Else

                                Dim bvData1 As String = "0"
                                Dim bvData2 As String = "0"
                                Dim filterStr As String = $"symbol='{Symbol}' AND option_type='XX' AND entry_date='{LastBhavcopyDate}'"
                                Dim rows = GdtBhavcopy.Select(filterStr)
                                If rows.Length > 0 Then
                                    Dim ltp As Decimal = Convert.ToDecimal(rows(0)("ltp"))

                                    bvData1 = ltp.ToString()
                                    bvData2 = (ltp * 1.1D).ToString()

                                End If

                                'Dim bvData1 = GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp").ToString()
                                'Dim bvData2 = (GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp") * 1.1).ToString()

                                'strExpMarginLog &= " isOTM: '" & isOTM.ToString & "', Close =  ' " & bvData1 & "', Close * 1.1 = ' " & bvData2 & "'"
                                'If StrikePrice >= GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp") * 1.1 Then
                                '    mRes_Obj1 = INDEX_OTM_OPTION 'mTbl_exposure_database_new.Select("symbol='" & Symbol & "' and InsType='OTM'")(0).Item("Total_Margin")
                                'Else
                                '    mRes_Obj1 = INDEX_OTH_OPTION 'mTbl_exposure_database_new.Select("symbol='" & Symbol & "'  and InsType='OTH'")(0).Item("Total_Margin")
                                'End If

                                rows = GdtBhavcopy.Select($"symbol='{Symbol}' AND option_type='XX' AND entry_date='{LastBhavcopyDate}'")
                                If rows.Length > 0 Then
                                    Dim ltp As Decimal = Convert.ToDecimal(rows(0)("ltp"))
                                    Dim ltp11 As Decimal = ltp * 1.1D

                                    bvData1 = ltp.ToString()
                                    bvData2 = ltp11.ToString()

                                    strExpMarginLog &= $" isOTM: '{isOTM}', Close = '{bvData1}', Close * 1.1 = '{bvData2}'"

                                End If

                                If StrikePrice >= Val(bvData2) Then
                                    mRes_Obj1 = INDEX_OTM_OPTION
                                Else
                                    mRes_Obj1 = INDEX_OTH_OPTION
                                End If

                            End If

                        ElseIf type = "P" Then
                            Dim ltpValue As Double = 0
                            'strExpMarginLog &= " isOTM: '" & isOTM.ToString & "', Close =  ' " & GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp").ToString & "', Close * 0.9 = ' " & (GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp") * 0.9).ToString & "'"
                            Dim rows() As DataRow = GdtBhavcopy.Select("symbol='" & Symbol & "' AND option_type='XX' AND entry_date='" & LastBhavcopyDate & "'")

                            If rows.Length > 0 Then
                                ltpValue = CDbl(rows(0)("ltp"))
                            Else
                                ltpValue = 0 ' or handle differently
                            End If
                            strExpMarginLog &=
                            " isOTM: '" & isOTM.ToString() & "'," &
                            " Close = '" & ltpValue.ToString() & "'," &
                            " Close * 0.9 = '" & (ltpValue * 0.9).ToString() & "'"

                            'If isOTM = 1 And StrikePrice <= GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX'  and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp") * 0.9 Then
                            If isOTM = 1 Then
                                strExpMarginLog &= " isOTM: '" & isOTM.ToString & "'"
                                mRes_Obj1 = INDEX_FAR_MONTH_OPTION 'mTbl_exposure_database_new.Select("symbol='" & Symbol & "'  and InsType='OTM'")(0).Item("Total_Margin")
                            Else
                                strExpMarginLog &= " isOTM: '" & isOTM.ToString & "', Close =  ' " & GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp").ToString & "', Close * 0.9 = ' " & (GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp") * 0.9).ToString & "'"
                                If StrikePrice <= GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX'  and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp") * 0.9 Then
                                    mRes_Obj1 = INDEX_OTM_OPTION 'mTbl_exposure_database_new.Select("symbol='" & Symbol & "'  and InsType='OTM'")(0).Item("Total_Margin")
                                Else
                                    mRes_Obj1 = INDEX_OTH_OPTION 'mTbl_exposure_database_new.Select("symbol='" & Symbol & "'  and InsType='OTH'")(0).Item("Total_Margin")
                                End If
                            End If
                        End If
                    ElseIf Script.ToString.Substring(3, 3) = "STK" Then
                        If type = "C" Then
                            strExpMarginLog &= " isOTM: '" & isOTM.ToString & "', Close =  ' " & GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp").ToString & "', Close * 1.3 = ' " & (GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp") * 1.3).ToString & "'"
                            If StrikePrice >= GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "'")(0).Item("ltp") * 1.3 Then
                                mRes_Obj1 = mTbl_exposure_database_new.Select("symbol='" & Symbol & "' and InsType='OTM'")(0).Item("Total_Margin")
                            Else
                                mRes_Obj1 = mTbl_exposure_database_new.Select("symbol='" & Symbol & "' and  InsType='OTH'")(0).Item("Total_Margin")
                            End If

                        ElseIf type = "P" Then
                            strExpMarginLog &= " isOTM: '" & isOTM.ToString & "', Close =  ' " & GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp").ToString & "', Close * 0.7 = ' " & (GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX' and entry_date='" & LastBhavcopyDate & "' ")(0).Item("ltp") * 0.7).ToString & "'"
                            If StrikePrice <= GdtBhavcopy.Select("symbol='" & Symbol & "' and option_type='XX'  and entry_date='" & LastBhavcopyDate & "'")(0).Item("ltp") * 0.7 Then
                                mRes_Obj1 = mTbl_exposure_database_new.Select("symbol='" & Symbol & "' and InsType='OTM'")(0).Item("Total_Margin")
                            Else
                                mRes_Obj1 = mTbl_exposure_database_new.Select("symbol='" & Symbol & "' and InsType='OTH'")(0).Item("Total_Margin")
                            End If
                        End If
                    End If

                End If

            ElseIf CalcOption = 3 Then
                If type = "C" Then
                    type = "CE"
                ElseIf type = "P" Then
                    type = "PE"
                ElseIf type = "F" Then
                    type = "XX"
                End If

                mRes_Obj1 = mTbl_ael_contracts.Select("InsType = '" & Script.ToString.Substring(0, 6) & "' and symbol='" & Symbol & "' and StrikePrice = '" & StrikePrice & "' and option_type='" & type & "' and ExpDate = #" & Expiry & "#")(0).Item("ELMPer")
            End If

            strExpMarginLog &= " ResultObject '" & mRes_Obj1.ToString & "'"
            Write_ExposureMarginLog(strExpMarginLog)
            Write_ExposureMarginLog(" === Function End ==== ")
            Return mRes_Obj1


        Catch ex As Exception
            mPerf.WriteLogStr("Err" + Script)
            MsgBox("Error in Calculate Exposure Margin")
            Return mRes_Obj1
        End Try


    End Function

    Private Sub set_exact_exposure_margin()
        Dim client_code_list As String = ""
        Dim dv As DataView
        dv = mCurTable.DefaultView
        dv.RowFilter = "cp='F'" 'select only FUTURE positions
        dv.Sort = "company,mdate"
        Dim i As Integer = 0
        Dim drow As DataRowView
        Dim mRet_Obj As Object
        Dim mDatabase_margin As Double

        ' Dim prv_clientcode As String
        Dim prv_comp As String
        Dim qty1 As Integer
        Dim set1 As Integer
        Dim rem1 As Integer
        Dim mon1 As Integer

        Dim qty2 As Integer
        Dim set2 As Integer
        Dim rem2 As Integer
        Dim mon2 As Integer

        Dim qty3 As Integer
        Dim set3 As Integer
        Dim rem3 As Integer
        Dim mon3 As Integer

        Dim temp_rem As Integer
        Dim mat_date As Boolean = False

        While i < dv.Count
            drow = dv.Item(i)
            i += 1

a1:
            qty1 = 0
            set1 = 0
            rem1 = 0
            mon1 = 0

            qty2 = 0
            set2 = 0
            rem2 = 0
            mon2 = 0

            qty3 = 0
            set3 = 0
            rem3 = 0
            mon3 = 0

            temp_rem = 0
            mat_date = False

            prv_comp = GetSymbol(drow("company"))
            qty1 = drow("units")
            mon1 = CDate(drow("mdate")).Month

            If UDDateDiff(DateInterval.Day, Today.Date, CDate(drow("mdate"))) <= 2 Then
                mat_date = True
            End If
            set1 = 0
            rem1 = drow("units")


            If i = dv.Count Then
                Exit While
            End If
            drow = dv.Item(i)
            i += 1

            If prv_comp <> GetSymbol(drow("company")) Then
                GoTo a1
            Else
                prv_comp = GetSymbol(drow("company"))
                qty2 = drow("units")
                mon2 = CDate(drow("mdate")).Month

                If (rem1 < 0 And qty2 < 0) Or (rem1 > 0 And qty2 > 0) Or mat_date = True Then
                    set2 = 0
                    rem2 = qty2
                Else
                    set2 = Math.Min(Math.Abs(rem1), Math.Abs(qty2))
                    If rem1 < 0 Then
                        rem1 = rem1 + set2
                    Else
                        rem1 = rem1 - set2
                    End If
                    If qty2 < 0 Then
                        rem2 = qty2 + set2
                    Else
                        rem2 = qty2 - set2
                    End If
                End If


                If i = dv.Count Then
                    GoTo a2
                End If
                drow = dv.Item(i)
                i += 1

                If prv_comp <> GetSymbol(drow("company")) Then
                    GoTo a2
                Else
                    prv_comp = GetSymbol(drow("company"))
                    qty3 = drow("units")
                    mon3 = CDate(drow("mdate")).Month

                    If (rem2 < 0 And qty3 < 0) Or (rem2 > 0 And qty3 > 0) Then
                        set3 = 0
                        rem3 = qty3
                    Else
                        If mat_date = False Then
                            set3 = Math.Min(Math.Abs(rem1 + rem2), Math.Abs(qty3))
                        Else
                            set3 = Math.Min(Math.Abs(rem2), Math.Abs(qty3))
                        End If

                        If rem1 <> 0 And mat_date = False Then
                            If Math.Abs(rem1) > set3 Then
                                If rem1 < 0 Then
                                    rem1 = rem1 + set3
                                Else
                                    rem1 = rem1 - set3
                                End If
                            Else
                                rem1 = 0
                            End If
                            temp_rem = Math.Abs(set3 - Math.Abs(rem1))

                            If temp_rem > 0 Then
                                If rem2 < 0 Then
                                    rem2 = rem2 + temp_rem
                                Else
                                    rem2 = rem2 - temp_rem
                                End If
                            End If
                        Else
                            If rem2 < 0 Then
                                rem2 = rem2 + set3
                            Else
                                rem2 = rem2 - set3
                            End If
                        End If


                        If qty3 < 0 Then
                            rem3 = qty3 + set3
                        Else
                            rem3 = qty3 - set3
                        End If
                    End If

                    If i = dv.Count Then
                        GoTo a2
                    End If
                    drow = dv.Item(i)
                    i += 1

                End If

a2:
                If qty1 <> rem1 Then
                    'For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(prv_comp) & "' and fut_opt='fut' and mat_month = " & mon1)
                    Dim drow1 As DataRow = mTbl_exposure_comp.Select("CompName='" & GetSymbol(prv_comp) & "' and fut_opt='fut' and mat_month = " & mon1)(0)

                    For Each drow_span As DataRow In mTblSpanOutput.Select("clientcode='" & drow("company") & "'")

                        mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(prv_comp) & "'")

                        If Convert.ToBoolean(drow("isCurrency")) = False Then
                            mRet_Obj = GetExposureObject(AELOPTION, mRet_Obj, drow("script").ToString, GetSymbol(prv_comp), CDate(drow("mdate")), Val(drow("strikes")), UCase(drow("cp")))
                        End If


                        If Not IsDBNull(mRet_Obj) Then
                            mDatabase_margin = Val(mRet_Obj) / 100
                        Else
                            'mDatabase_margin = 0
                            DEFAULT_EXPO_MARGIN = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='DEFAULT_EXPO_MARGIN'").ToString)
                            mDatabase_margin = DEFAULT_EXPO_MARGIN / 100
                        End If

                        drow_span("exposure_margin") -= (drow1("p") * Math.Abs(qty1) * mDatabase_margin)

                        drow_span("exposure_margin") += (drow1("p") * Math.Abs(rem1) * mDatabase_margin)
                    Next
                    ' Next
                End If

                If (qty2 <> rem2) Or set2 <> 0 Then
                    ' For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(prv_comp) & "' and fut_opt='fut' and mat_month = " & mon2)

                    Dim drow1 As DataRow = mTbl_exposure_comp.Select("CompName='" & GetSymbol(prv_comp) & "' and fut_opt='fut' and mat_month = " & mon2)(0)

                    For Each drow_span As DataRow In mTblSpanOutput.Select("clientcode='" & drow("company") & "'")

                        mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(prv_comp) & "'")

                        If Convert.ToBoolean(drow("isCurrency")) = False Then
                            mRet_Obj = GetExposureObject(AELOPTION, mRet_Obj, drow("script").ToString, GetSymbol(prv_comp), CDate(drow("mdate")), Val(drow("strikes")), UCase(drow("cp")))
                        End If


                        If Not IsDBNull(mRet_Obj) Then
                            mDatabase_margin = Val(mRet_Obj) / 100
                        Else
                            'mDatabase_margin = 0
                            DEFAULT_EXPO_MARGIN = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='DEFAULT_EXPO_MARGIN'").ToString)
                            mDatabase_margin = DEFAULT_EXPO_MARGIN / 100
                        End If

                        drow_span("exposure_margin") -= (drow1("p") * Math.Abs(qty2) * mDatabase_margin)

                        drow_span("exposure_margin") += (drow1("p") * Math.Abs(rem2) * mDatabase_margin)
                        drow_span("exposure_margin") += (1 / 3) * (drow1("p") * Math.Abs(set2) * mDatabase_margin)
                    Next
                    ' Next
                End If

                If (qty3 <> rem3) Or set3 <> 0 Then
                    ' For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(prv_comp) & "' and fut_opt='fut' and mat_month = " & mon3)

                    Dim drow1 As DataRow = mTbl_exposure_comp.Select("CompName='" & GetSymbol(prv_comp) & "' and fut_opt='fut' and mat_month = " & mon3)(0)

                    For Each drow_span As DataRow In mTblSpanOutput.Select("clientcode='" & drow("company") & "'")

                        mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(prv_comp) & "'")

                        If Convert.ToBoolean(drow("isCurrency")) = False Then
                            mRet_Obj = GetExposureObject(AELOPTION, mRet_Obj, drow("script").ToString, GetSymbol(prv_comp), CDate(drow("mdate")), Val(drow("strikes")), UCase(drow("cp")))
                        End If

                        If Not IsDBNull(mRet_Obj) Then
                            mDatabase_margin = Val(mRet_Obj) / 100
                        Else
                            'mDatabase_margin = 0
                            DEFAULT_EXPO_MARGIN = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='DEFAULT_EXPO_MARGIN'").ToString)
                            mDatabase_margin = DEFAULT_EXPO_MARGIN / 100
                        End If

                        drow_span("exposure_margin") -= (drow1("p") * Math.Abs(qty3) * mDatabase_margin)

                        drow_span("exposure_margin") += (drow1("p") * Math.Abs(rem3) * mDatabase_margin)
                        drow_span("exposure_margin") += (1 / 3) * (drow1("p") * Math.Abs(set3) * mDatabase_margin)
                    Next
                    ' Next
                End If

                GoTo a1
            End If

        End While


    End Sub


#Region "New code"

    Private Function extract_exposure_margin2(pExchange As String) As Boolean
        Try
            '==========================
            ' RESET PREVIOUS DATA
            '==========================
            mTbl_exposure_comp.Clear()
            mTblSpanOutput.Clear()
            mTbl_span_calc.Clear()

            '==========================
            ' PROCESS PRIMARY SPAN OUTPUT
            '==========================
            ProcessSpanFile(IO.Path.Combine(mSPAN_path, "BseOutput.xml"))

            '==========================
            ' PROCESS CURRENCY SPAN FILE (IF EXISTS)
            '==========================
            Dim curFile = IO.Path.Combine(mSPAN_path, "curoutput.xml")
            If IO.File.Exists(curFile) Then ProcessSpanFile(curFile)

            Return True

        Catch ex As Exception
            MsgBox("Error In extract_exposure_margin():  " & ex.Message)
            Return False
        End Try
    End Function


    '#####################################################################
    ' MAIN FILE PARSER
    '#####################################################################
    Private Sub ProcessSpanFile(filePath As String)


        CUtils.WaitUntilFree(filePath, 5000)

        If Not IO.File.Exists(filePath) Then Exit Sub

        Using reader As New StreamReader(filePath)
            Using xml As New Xml.XmlTextReader(reader)

                While xml.Read
                    ' Console.WriteLine("xmlName: " & xml.Name)
                    Select Case xml.Name
                        Case "phyPf" : ParseEquity(xml)
                        Case "futPf" : ParseFuture(xml)
                        Case "oopPf" : ParseOptions(xml)
                        Case "portfolio" : ParsePortfolio(xml)
                    End Select
                End While

            End Using
        End Using

    End Sub



    '#####################################################################
    ' BLOCK: EQUITY PRICE <phyPf>
    '#####################################################################
    'Private Sub ParseEquity(xml As Xml.XmlTextReader)
    '    Dim row = mTbl_exposure_comp.NewRow
    '    Dim compName As String = ""

    '    ' Read until we exit this <phyPf> block
    '    Do While xml.Read
    '        If xml.NodeType = System.Xml.XmlNodeType.EndElement AndAlso xml.Name = "phyPf" Then Exit Do

    '        Select Case xml.Name

    '            Case "pfId"
    '                xml.Read() 'move to value

    '            Case "pfCode"
    '                xml.Read()
    '                compName = xml.ReadString()
    '                row("CompName") = compName
    '                row("fut_opt") = "OPT"

    '            'sometimes blank, ignore

    '            Case "name"
    '                ' Actual symbol comes here
    '                '     compName = xml.ReadString()
    '                '     row("CompName") = compName
    '                'row("fut_opt") = "OPT"

    '            Case "phy"
    '                ' Now inside price section, read values
    '                ParseEquityPrice(xml, row)

    '        End Select
    '    Loop

    '    If Not String.IsNullOrEmpty(compName) Then
    '        mTbl_exposure_comp.Rows.Add(row)
    '    End If
    'End Sub

    Private Sub ParseEquity(xml As Xml.XmlTextReader)

        Dim row As DataRow = mTbl_exposure_comp.NewRow()
        Dim compName As String = ""

        Do While xml.Read()

            If xml.NodeType = System.Xml.XmlNodeType.EndElement AndAlso xml.Name = "phyPf" Then
                Exit Do
            End If

            If xml.NodeType <> System.Xml.XmlNodeType.Element Then Continue Do

            Select Case xml.Name

                Case "pfCode"
                    compName = xml.ReadElementContentAsString()
                    row("CompName") = compName
                    row("fut_opt") = "OPT"

                Case "phy"
                    ParseEquityPrice(xml, row)

            End Select
        Loop

        If Not String.IsNullOrWhiteSpace(compName) Then
            mTbl_exposure_comp.Rows.Add(row)
        End If

    End Sub


    Private Sub ParseEquityPrice(xml As Xml.XmlTextReader, row As DataRow)
        Do While xml.Read
            If xml.NodeType = System.Xml.XmlNodeType.EndElement AndAlso xml.Name = "phy" Then Exit Do

            Select Case xml.Name
                Case "cId" : xml.Read()
                Case "pe" : xml.Read()
                Case "p" : row("p") = Val(xml.ReadString())  ' store price
            End Select
        Loop
    End Sub


    '#####################################################################
    ' BLOCK: FUTURES <futPf>
    '#####################################################################
    Private Sub ParseFuture(xml As System.Xml.XmlTextReader)

        Dim compName As String = ""
        Dim row As DataRow

        ' Loop until </futPf>
        Do While xml.Read

            If xml.NodeType = System.Xml.XmlNodeType.Element Then
                Select Case xml.Name

                    Case "pfCode"
                        xml.Read()
                        compName = xml.Value.Trim()

                    Case "name"
                        xml.Read()
                        If compName = "" Then compName = xml.Value.Trim()

                    Case "fut"

                        row = mTbl_exposure_comp.NewRow
                        row("CompName") = compName
                        row("fut_opt") = "FUT"

                        ' Read <fut> block
                        Do While xml.Read

                            If xml.NodeType = System.Xml.XmlNodeType.EndElement AndAlso xml.Name = "fut" Then
                                mTbl_exposure_comp.Rows.Add(row)
                                Exit Do
                            End If

                            If xml.NodeType = System.Xml.XmlNodeType.Element Then
                                Select Case xml.Name

                                    Case "cId"
                                        xml.Read()
                                        'row("CompName") = xml.Value
                                        row("CompName") = compName' xml.Value

                                    Case "pe"
                                        xml.Read()
                                        'row("ExpiryRaw") = xml.Value
                                        If xml.Value.Length >= 6 Then
                                            row("mat_month") = Val(Mid(xml.Value, 5, 2))
                                        End If

                                    Case "p"
                                        xml.Read()
                                        row("p") = Val(xml.Value)

                                End Select
                            End If

                        Loop

                End Select
            End If

            ' exit if block ends
            If xml.NodeType = System.Xml.XmlNodeType.EndElement AndAlso xml.Name = "futPf" Then Exit Do

        Loop

    End Sub


    '#####################################################################
    ' BLOCK: OPTIONS <oopPf> (series → opt → strike)
    '#####################################################################
    Private Sub ParseOptions(xml As System.Xml.XmlTextReader)

        Dim compName As String = ""
        Dim expiryRaw As String = ""
        Dim expiry As String = ""

        ' Read until </oopPf>
        Do While xml.Read

            If xml.NodeType = System.Xml.XmlNodeType.Element Then
                Select Case xml.Name

                '=========================
                ' CONTRACT IDENTIFICATION
                '=========================
                    Case "pfCode"
                        xml.Read()
                        compName = xml.Value.Trim()

                    Case "name"
                        xml.Read()
                        If compName = "" Then compName = xml.Value.Trim()

                '=========================
                ' SERIES (Expiry block)
                '=========================
                    Case "series"

                        ' Reset expiry for each series
                        expiryRaw = ""
                        expiry = ""

                        ' Read series block until </series>
                        Do While xml.Read

                            If xml.NodeType = System.Xml.XmlNodeType.EndElement AndAlso xml.Name = "series" Then Exit Do

                            If xml.NodeType = System.Xml.XmlNodeType.Element Then
                                Select Case xml.Name
                                    Case "pe"   ' raw expiry format: YYYYMMDD
                                        xml.Read()
                                        expiryRaw = xml.Value.Trim()

                                        ' Format expiry (e.g., 20251125 → 25-Nov-2025)
                                        If expiryRaw.Length = 8 Then
                                            Dim y = expiryRaw.Substring(0, 4)
                                            Dim m = expiryRaw.Substring(4, 2)
                                            Dim d = expiryRaw.Substring(6, 2)
                                            expiry = $"{d}-{Format(CDate($"{m}/01/{y}"), "MMM")}-{y}"
                                        Else
                                            expiry = expiryRaw
                                        End If

                                '===========================
                                ' OPTION RECORDS
                                '===========================
                                    Case "opt"

                                        Dim strike As String = ""
                                        Dim optType As String = ""
                                        Dim price As Double = 0

                                        ' Read values inside <opt>
                                        Do While xml.Read

                                            If xml.NodeType = System.Xml.XmlNodeType.EndElement AndAlso xml.Name = "opt" Then Exit Do

                                            If xml.NodeType = System.Xml.XmlNodeType.Element Then
                                                Select Case xml.Name
                                                    Case "o" ' C or P
                                                        xml.Read()
                                                        optType = xml.Value.Trim()

                                                    Case "k" ' Strike
                                                        xml.Read()
                                                        strike = xml.Value.Trim()

                                                    Case "p" ' LTP
                                                        xml.Read()
                                                        price = Val(xml.Value)

                                                End Select
                                            End If
                                        Loop

                                        '===========================
                                        ' STORE OUTPUT
                                        '===========================
                                        Dim row = mTbl_span_calc.NewRow
                                        Dim strDesc As String = concat_scrip(compName, If(optType = "C", "CAL", "PUT"), expiry, strike)

                                        If strDesc.IndexOf("System.Data") > 0 Then
                                            MessageBox.Show("tesat")
                                        End If

                                        row("description") = strDesc
                                        'row("description") = concat_scrip(compName, If(optType = "C", "CAL", "PUT"), expiry, strike)
                                        row("compname") = compName
                                        row("cal_put_fut") = If(optType = "C", "CAL", "PUT")
                                        row("strike_price") = strike
                                        row("exp_date") = expiry
                                        '  row("price") = price
                                        mTbl_span_calc.Rows.Add(row)

                                End Select
                            End If

                        Loop ' end series

                End Select
            End If

            ' Stop when option block is finished
            If xml.NodeType = System.Xml.XmlNodeType.EndElement AndAlso xml.Name = "oopPf" Then Exit Do

        Loop

    End Sub


    '#####################################################################
    ' BLOCK: CLIENT MARGIN SUMMARY <portfolio>
    '#####################################################################
    'Private Sub ParsePortfolio(xml As Xml.XmlTextReader)

    '    Dim row = mTblSpanOutput.NewRow
    '    Dim isSpanRead As Boolean = False

    '    Do While xml.Read
    '        Select Case xml.Name

    '            Case "firm"
    '                row("ClientCode") = xml.ReadElementString("firm")

    '                xml.ReadElementString("acctId")
    '                xml.ReadElementString("acctType")
    '                xml.ReadElementString("isCust")
    '                xml.ReadElementString("seg")

    '                xml.Read()
    '                If xml.Name = "acctSubType" Then SkipUntil(xml, "isNew")

    '                xml.ReadElementString("isNew")
    '                xml.ReadElementString("custPortUseLov")
    '                xml.ReadElementString("currency")
    '                xml.ReadElementString("ledgerBal")
    '                xml.ReadElementString("ote")
    '                xml.ReadElementString("securities")

    '                row("lfv") = Val(xml.ReadElementString("lfv"))
    '                row("sfv") = Val(xml.ReadElementString("sfv"))
    '                row("lov") = Val(xml.ReadElementString("lov"))
    '                row("sov") = Val(xml.ReadElementString("sov"))

    '            Case "spanReq"
    '                If Not isSpanRead Then
    '                    row("spanreq") = Val(xml.ReadElementString("spanReq"))
    '                    row("anov") = Val(xml.ReadElementString("anov"))
    '                    If (row("spanreq") - row("anov")) <= 0 Then
    '                        row("spanreq") = 0
    '                        row("anov") = 0
    '                    End If
    '                    isSpanRead = True
    '                End If

    '            Case "portfolio"
    '                row("exposure_margin") = 0
    '                mTblSpanOutput.Rows.Add(row)
    '                Exit Do

    '        End Select
    '    Loop

    'End Sub

    Private Sub ParsePortfolio(xml As System.Xml.XmlTextReader)

        Dim row As DataRow = mTblSpanOutput.NewRow()
        Dim spanCaptured As Boolean = False

        Do While xml.Read()

            ' Exit condition
            If xml.NodeType = System.Xml.XmlNodeType.EndElement AndAlso xml.Name = "portfolio" Then
                row("exposure_margin") = 0
                mTblSpanOutput.Rows.Add(row)
                Exit Do
            End If

            If xml.NodeType <> System.Xml.XmlNodeType.Element Then Continue Do

            Select Case xml.Name

                Case "acctId"
                    row("ClientCode") = xml.ReadElementContentAsString().Trim()

                Case "lfv"
                    row("lfv") = CDbl(xml.ReadElementContentAsString())

                Case "sfv"
                    row("sfv") = CDbl(xml.ReadElementContentAsString())

                Case "lov"
                    row("lov") = CDbl(xml.ReadElementContentAsString())

                Case "sov"
                    row("sov") = CDbl(xml.ReadElementContentAsString())

                Case "spanReq"
                    If Not spanCaptured Then
                        row("spanreq") = CDbl(xml.ReadElementContentAsString())
                    Else
                        xml.Skip()
                    End If

                Case "anov"
                    row("anov") = CDbl(xml.ReadElementContentAsString())

                    ' Normalize values
                    If CDbl(row("spanreq")) - CDbl(row("anov")) <= 0 Then
                        row("spanreq") = 0
                        row("anov") = 0
                    End If

                    spanCaptured = True

            End Select

        Loop

    End Sub


    'Private Sub ParsePortfolio(xml As System.Xml.XmlTextReader)

    '    Dim row = mTblSpanOutput.NewRow
    '    Dim spanCaptured As Boolean = False

    '    Do While xml.Read()

    '        If xml.NodeType = System.Xml.XmlNodeType.Element Then

    '            Select Case xml.Name

    '                Case "firm"
    '                    xml.Read()
    '                    row("ClientCode") = xml.Value.Trim()

    '                Case "lfv"
    '                    xml.Read()
    '                    row("lfv") = Val(xml.Value)

    '                Case "sfv"
    '                    xml.Read()
    '                    row("sfv") = Val(xml.Value)

    '                Case "lov"
    '                    xml.Read()
    '                    row("lov") = Val(xml.Value)

    '                Case "sov"
    '                    xml.Read()
    '                    row("sov") = Val(xml.Value)

    '                Case "spanReq"
    '                    If Not spanCaptured Then
    '                        xml.Read()
    '                        row("spanreq") = Val(xml.Value)
    '                    End If

    '                Case "anov"
    '                    xml.Read()
    '                    row("anov") = Val(xml.Value)

    '                    ' Normalize values to avoid negative margin
    '                    If Val(row("spanreq")) - Val(row("anov")) <= 0 Then
    '                        row("spanreq") = 0
    '                        row("anov") = 0
    '                    End If

    '                    spanCaptured = True

    '            End Select

    '        End If

    '        ' Closing tag = end of record
    '        If xml.NodeType = System.Xml.XmlNodeType.EndElement AndAlso xml.Name = "portfolio" Then
    '            row("exposure_margin") = 0
    '            mTblSpanOutput.Rows.Add(row)
    '            Exit Do
    '        End If

    '    Loop

    'End Sub


    '#####################################################################
    ' UTILITY: Advance XML until a tag appears
    '#####################################################################
    Private Sub SkipUntil(xml As Xml.XmlTextReader, nodeName As String)
        While xml.Read
            If xml.Name = nodeName Then Exit While
        End While
    End Sub


#End Region


    'Private Async Sub RunSpanProcessCur() As Task
    '    If mCurrent_CurSPAN_file <> "" Then
    '        Await Task.Run(Sub() execute_Cur_batch_file())
    '    End If
    'End Sub

    Private Async Function RunSpanProcessCur() As Task
        If mCurrent_CurSPAN_file <> "" Then
            Await Task.Run(Sub() execute_Cur_batch_file())
        End If
    End Function

    Private Async Function RunSpanProcessFo() As Task
        If mCurrent_SPAN_file <> "" Then
            Await Task.Run(Sub() execute_FO_batch_file())
        End If
    End Function
    Public mMainTable As DataTable

    Dim mDtTmp As DataTable
    Dim mDtPortFo = New DataTable
    Public Async Function generate_SPAN_data_BSE(pExchange As String, pSpanFile As String) As Task
        Try
            mSPAN_path = SPAN_PATH
            If Not Directory.Exists(mSPAN_path) Then 'if not correct span software path
                MsgBox("Enter Correct Path For span In setting.")
                Return
            End If

            mDtPortFo = New DataTable
            mDtPortFo = mCurTable.Copy()

            Dim dtableall As New DataTable
            dtableall = mCurTable.Copy()

            'Dim dtable2 As New DataTable

            ' Try
            For Each item As DataRow In dtableall.Rows

                If item("month").ToString <> "" Then
                    'Dim MONTH As String = item("month").ToString.Substring(0, 3)
                    Dim str As String = CDate(item("mdate")).ToString("MMMdd")
                    Dim MONTH As String = str.Replace(" ", "").ToString().ToUpper()
                    item("company") = item("company").ToString & "/" & "All"
                Else
                    item("company") = item("company").ToString & "/" & item("month").ToString
                End If

                If item("month").ToString <> "" Then
                    item("month") = "All"
                End If
            Next

            For Each item As DataRow In mDtPortFo.Rows
                If item("month").ToString <> "" Then
                    '  Dim MONTH As String = item("month").ToString.Substring(0, 3)
                    Dim str As String = CDate(item("mdate")).ToString("MMMdd")
                    Dim MONTH As String = str.Replace(" ", "").ToString().ToUpper()
                    item("company") = item("company").ToString & "/" & MONTH
                Else
                    item("company") = item("company").ToString & "/" & item("month").ToString
                End If
            Next
            'Catch ex As Exception

            'End Try
            mDtPortFo.Merge(dtableall, True)

            mTbl_exposure_comp.Clear()
            mTblSpanOutput.Clear()
            mTbl_span_calc.Clear()



            'mCurrent_SPAN_file = get_latest_spn_file(mSPAN_path, "FO")
            'mCurrent_CurSPAN_file = get_latest_spn_file(mSPAN_path, "CUR")
            'get_expected_latest_spn_file(mCurrent_SPAN_file, "FO")
            'get_expected_latest_spn_file(mCurrent_CurSPAN_file, "CUR")

            'CType(Me.MdiParent, mdiMain).StatusBar1.Panels(2).Text = mCurrent_SPAN_file
            'If CALMARGINSPAN = 1 Then

            '    If mCurrent_SPAN_file = "" And mCurrent_CurSPAN_file = "" Then
            '        MsgBox("Invalid Span File..!")
            '        mFlgThr_Span = False
            '        Return
            '    End If
            'End If

            DeleteExistingFiles()
            CreateBseSpan()
            '    CreateCurSpan()
            CreateBseBatchFiles()

            'Dim client_code As String = Nothing
            'Dim temp_comp_name As String = Nothing
            'Dim comp_name As String = Nothing
            'Dim option_type As String = Nothing
            'Dim mat_date As String = Nothing
            'Dim CAL_PUT As String = Nothing
            'Dim strike_price As String = Nothing
            'Dim qty As String = Nothing
            'Dim drow_position As DataRow = Nothing
            'Dim ar_comp As String() = Nothing

            Dim ht_comp As New Hashtable

            'add analysis companies to hashtable
            For Each drow As DataRow In mDtPortFo.DefaultView.ToTable(True, "company").Rows
                If ht_comp.ContainsKey(drow("company")) = False Then
                    ht_comp.Add(drow("company"), 1)
                End If
            Next

            Dim ar_comp(ht_comp.Count - 1) As String
            ht_comp.Keys.CopyTo(ar_comp, 0)
            'ar_comp(0) = "BSXOPT/ALL"
            'ar_comp(1) = "BSXOPT/DEC11"
            'inpFo(ar_comp)

            Dim bseSpanBuilder As New CPosXmlBuilder()
            mHtBseComp = bseSpanBuilder.mHtBseComp
            inpFo_UsingBuilder(bseSpanBuilder, ar_comp)
            inpCur(ar_comp)

            '   Dim fs_Curinput As New FileStream(, FileMode.Create)


            'If mCurrent_SPAN_file <> "" Then
            '    Dim worker As New System.Threading.Thread(AddressOf execute_FO_batch_file)
            '    worker.Name = "thr_exefoSpan"
            '    worker.Start()
            '    worker.Join()
            'End If


            'If mCurrent_CurSPAN_file <> "" Then
            '    Dim worker2 As New System.Threading.Thread(AddressOf execute_Cur_batch_file)
            '    worker2.Name = "thr_execurSpan"
            '    worker2.Start()
            '    worker2.Join()
            'End If

            Await RunSpanProcessFo()
            Await RunSpanProcessCur()

            If mIsOutputFileGenerated = False Then
                Return
            End If

            CALMARGINWITH_AEL_EXPO = 0
            If CALMARGINWITH_AEL_EXPO = 1 Then


                'Dim extractor As New CSpanAelExposureExtractor(
                '    mSPAN_path,
                '    mTbl_exposure_comp,
                '    mTbl_span_calc,
                '    mTblSpanOutput,
                '    mTbl_AEL_Expo_calc
                ')

                'Dim ok As Boolean = extractor.ExtractExposureMargin("BSE")

                If Additional_AEL_extract_exposure_margin(mExchange) = False Then
                    'Return False
                    mFlgThr_Span = False
                    Return
                End If
            Else
                If extract_exposure_margin2(mExchange) = False Then
                    'If extract_exposure_margin() = False Then
                    'Return False
                    mFlgThr_Span = False
                    Return
                End If
            End If

            Dim drow_span As DataRow
            Dim fut_opt As String
            Dim mRet_Obj As Object
            Dim mDatabase_margin As Double

            Dim Dv As DataView

            mPerf.SetFileName("mTbl_exposure_comp")
            mPerf.PrintDataTable(mTbl_exposure_comp)


            Dv = New DataView(mTbl_exposure_comp.Copy)
            mTbl_exposure_comp = Dv.ToTable(True, "CompName", "mat_MOnth", "P", "fut_opt")

            Dim tblSpn As New DataTable
            Dim column As DataColumn

            column = New DataColumn()
            column.DataType = GetType(String)
            column.ColumnName = "ClientCode"
            column.DefaultValue = ""
            tblSpn.Columns.Add(column) ' company name

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "lfv"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "sfv"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "lov"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "sov"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "spanreq"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column) ' for initial margin  spanreq-anvo

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "anov"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "exposure_margin"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            Dv = New DataView(mTblSpanOutput.Copy)
            Dim row As DataRow
            For Each Drow As DataRow In Dv.ToTable(True, "ClientCode").Rows
                row = tblSpn.NewRow
                row("ClientCode") = Drow("ClientCode")
                row("lfv") = mTblSpanOutput.Compute("Max(lfv)", "ClientCode='" & Drow("ClientCode") & "'")
                row("sfv") = mTblSpanOutput.Compute("Max(sfv)", "ClientCode='" & Drow("ClientCode") & "'")
                row("lov") = mTblSpanOutput.Compute("Max(lov)", "ClientCode='" & Drow("ClientCode") & "'")
                row("sov") = mTblSpanOutput.Compute("Max(sov)", "ClientCode='" & Drow("ClientCode") & "'")
                row("spanreq") = mTblSpanOutput.Compute("Max(spanreq)", "ClientCode='" & Drow("ClientCode") & "'")
                row("anov") = mTblSpanOutput.Compute("Max(anov)", "ClientCode='" & Drow("ClientCode") & "' and anov<>0")
                row("exposure_margin") = mTblSpanOutput.Compute("Max(exposure_margin)", "ClientCode='" & Drow("ClientCode") & "'")
                tblSpn.Rows.Add(row)
                tblSpn.AcceptChanges()
            Next

            mTblSpanOutput = tblSpn
            If AELOPTION = 2 Then
                If GdtBhavcopy.Rows.Count = 0 Then
                    MsgBox("Please Process  Bhavcopy For Exposure Margin Calculatoin..")
                    Return
                End If
            End If

            For Each drow As DataRow In mCurTable.Select("(cp='F') or (cp<>'F' and units < 0) and cp <> 'E'")


                If UCase(drow("cp")) = "F" Then
                    fut_opt = "FUT"
                    ' For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(drow("company")) & "' and fut_opt='" & fut_opt & "' and mat_month = " & CDate(drow("mdate")).Month)
                    If mTbl_exposure_comp.Select("CompName='" & GetSymbol(drow("company")) & "' and fut_opt='" & fut_opt & "' and mat_month = " & CDate(drow("mdate")).Month).Length > 0 Then


                        Dim drow1 As DataRow = mTbl_exposure_comp.Select("CompName='" & GetSymbol(drow("company")) & "' and fut_opt='" & fut_opt & "' and mat_month = " & CDate(drow("mdate")).Month)(0)

                        For Each drow_span In mTblSpanOutput.Select("clientcode='" & drow("company") & "'")
                            mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(drow("company")) & "'")
                            If Convert.ToBoolean(drow("isCurrency")) = False Then
                                mRet_Obj = GetExposureObject(AELOPTION, mRet_Obj, drow("script").ToString(), GetSymbol(drow("company")), CDate(drow("mdate")), Val(drow("strikes")), UCase(drow("cp")))
                            End If


                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                DEFAULT_EXPO_MARGIN = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='DEFAULT_EXPO_MARGIN'").ToString)
                                ' mDatabase_margin = 0

                                mDatabase_margin = DEFAULT_EXPO_MARGIN / 100
                            End If
                            If drow("units") < 0 Then
                                drow_span("exposure_margin") += ((drow1("p") * (-drow("units")) * mDatabase_margin))
                            Else
                                drow_span("exposure_margin") += ((drow1("p") * drow("units") * mDatabase_margin))
                            End If
                        Next
                    End If
                    ' Next
                Else
                    fut_opt = "OPT"

                    Dim compBseName As String = GetBseComp(drow("company"))
                    'compBseName = GetSymbol(drow("company"))

                    mDtTmp = CUtils.GetFilteredTable(mTbl_exposure_comp, "CompName='" & compBseName & "' and fut_opt='" & fut_opt & "'")
                    For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & compBseName & "' and fut_opt='" & fut_opt & "'")

                        Dim filter As String = "clientcode Like '%" & drow("company") & "%'"

                        mDtTmp = CUtils.GetFilteredTable(mTblSpanOutput, filter)

                        'For Each drow_span In mTblSpanOutput.Select("clientcode='" & drow("company") & "'")
                        For Each drow_span In mTblSpanOutput.Select(filter)

                            mRet_Obj = 0
                            If mTbl_exposure_database.Rows.Count > 0 Then
                                mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & compBseName & "'")
                            End If

                            If Convert.ToBoolean(drow("isCurrency")) = False Then
                                mRet_Obj = GetExposureObject(AELOPTION, mRet_Obj, drow("script").ToString(), GetSymbol(drow("company")), CDate(drow("mdate")), Val(drow("strikes")), UCase(drow("cp")))
                            End If

                            If Not IsDBNull(mRet_Obj) Then
                                Try
                                    mDatabase_margin = Val(mRet_Obj) / 100
                                Catch ex As Exception
                                    mDatabase_margin = 0
                                End Try

                            Else
                                DEFAULT_EXPO_MARGIN = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='DEFAULT_EXPO_MARGIN'").ToString)
                                ' mDatabase_margin = 0
                                mDatabase_margin = DEFAULT_EXPO_MARGIN / 100
                            End If
                            If drow("units") < 0 Then
                                drow_span("exposure_margin") += ((drow1("p") * (-drow("units")) * mDatabase_margin))
                            Else
                                drow_span("exposure_margin") += ((drow1("p") * drow("units") * mDatabase_margin))
                            End If
                        Next
                    Next
                End If
            Next

            set_exact_exposure_margin()
            'DataGridView1.DataSource = mTbl_SPAN_output
            'MsgBox("exposure margin: - " & mTbl_SPAN_output.Rows(0)("exposure_margin"))
            'MsgBox("exposure margin: - " & mTbl_SPAN_output.Rows(0)("spanreq") - mTbl_SPAN_output.Rows(0)("anov"))
            'DataGridView1.DataSource = mTbl_exposure_comp
            'DataGridView2.DataSource = mTbl_SPAN_output
            'Return True
        Catch ex As Exception
            MsgBox(ex.ToString)
            'Return False
            mFlgThr_Span = False
        End Try
    End Function

    Private Sub inpFo_UsingBuilder(pSb As CPosXmlBuilder, ar_comp As String())

        ' ---------- HEADER ----------
        pSb.AddHeader("4.00", Today.ToString("yyyyMMdd"))
        pSb.StartPointInTime("", 0, ":::::", 0)

        ' ---------- EACH CLIENT ----------
        For Each client_code As String In ar_comp

            pSb.StartPortfolio(
            firm:=client_code.Replace("&", "&amp;"),
            acctId:=GetSymbol(client_code).Replace("&", "&amp;"),
            acctType:="S",
            isCust:=1,
            seg:="N/A",
            currency:="INR",
            isNew:=1,
            custPortUseLov:=0,
            ledgerBal:=0D,
            ote:=0D,
            securities:=0D,
            lue:=0D
        )

            ' ---------- EC PORT ----------
            Dim cc As String = GetSymbol(client_code)

            cc = pSb.GetCompCode(cc)
            pSb.StartEcPort("ICCL", cc, 1, "INR", 0)

            Dim lastComp As String = ""

            For Each dr As DataRow In mDtPortFo.Select(
            "company='" & client_code & "' AND units<>0",
            "company,strikes")

                Dim comp_name As String = dr("company").ToString()
                Dim pfCode As String = GetSymbol(comp_name)

                ' ---------- EXPIRY ----------
                Dim expiry As String
                If comp_name.Contains("INR") Then
                    expiry = Format(dr("mdate"), "yyyyMM")
                Else
                    expiry = Format(dr("mdate"), "yyyyMMdd")
                End If

                ' ---------- QTY ----------
                Dim qty As Integer
                If CBool(dr("IsCurrency")) Then
                    qty = 0
                Else
                    qty = CInt(dr("units"))
                End If

                ' ---------- FUT / OPTION ----------
                If UCase(dr("cp").ToString()) = "F" Then

                    pSb.AddPositionFuture(
                    exch:=mExchange,
                    pfCode:=pfCode,
                    expiry:=expiry,
                    qty:=qty
                )

                Else
                    Dim optType As String = Left(UCase(dr("cp").ToString()), 1)
                    Dim strike As Decimal = CDec(dr("strikes"))

                    Dim undPe As String =
                    If(comp_name.Contains("INR"), "000000", "000000")

                    If optType = "C" Then
                        pSb.AddPositionCall(
                        exch:=mExchange,
                        pfCode:=pfCode,
                        expiry:=expiry,
                        undExpiry:=undPe,
                        strike:=strike,
                        qty:=qty
                    )
                    Else
                        pSb.AddPositionPut(
                        exch:=mExchange,
                        pfCode:=pfCode,
                        expiry:=expiry,
                        undExpiry:=undPe,
                        strike:=strike,
                        qty:=qty
                    )
                    End If
                End If

                ' ---------- AEL EXPOSURE ----------
                If CALMARGINWITH_AEL_EXPO = 1 Then
                    CalculateAelExposure(dr, client_code)
                End If

            Next

            pSb.EndEcPort()
            pSb.EndPortfolio()

        Next

        pSb.EndPointInTime()

        ' ---------- SAVE ----------
        pSb.Save(mSPAN_path & "\input.txt")

    End Sub

    Private Sub CalculateAelExposure(dr As DataRow, client_code As String)

        If dr("toqty") >= 0 Then Exit Sub

        Dim script() As String =
        dr("script").ToString().Split(New String() {"  "}, StringSplitOptions.None)

        Dim cp As String =
        If(dr("cp") = "C", "CE",
           If(dr("cp") = "P", "PE", "XX"))

        Dim value As Double = CDbl(dr("toqty")) * CDbl(dr("last"))
        Dim elmPer As Double = 0

        If mTbl_ael_Additional_Expo IsNot Nothing AndAlso
       mTbl_ael_Additional_Expo.Rows.Count > 0 Then

            elmPer = CDbl(
            mTbl_ael_Additional_Expo.Select(
                "InsType='" & script(1) &
                "' AND Symbol='" & script(0) &
                "' AND StrikePrice='" & script(3).Replace(".00", "") &
                "' AND OptType='" & cp & "'")(0)("ELMPer"))
        End If

        Dim drNew As DataRow = mTbl_AEL_Expo_calc.NewRow()
        drNew("AEL_EXPOSURE") = (value * elmPer) / 100
        drNew("Company") = client_code
        mTbl_AEL_Expo_calc.Rows.Add(drNew)

    End Sub


    Private Sub inpCur(ar_comp As String())

        Dim client_code As String = Nothing
        Dim temp_comp_name As String = Nothing
        Dim comp_name As String = Nothing
        Dim option_type As String = Nothing
        Dim mat_date As String = Nothing
        Dim CAL_PUT As String = Nothing
        Dim strike_price As String = Nothing
        Dim qty As String = Nothing
        Dim drow_position As DataRow = Nothing




        Using fs_Curinput As New FileStream(mSPAN_path & "\curinput.txt", FileMode.Create, FileAccess.Write, FileShare.None)
            Using swCur As New StreamWriter(fs_Curinput)
                '''''Currinpitfile
                '   sw = New StreamWriter(fs_Curinput)
                swCur.WriteLine("<?xml version=""" & "1.0""" & "?>")
                swCur.WriteLine("<posFile>")
                swCur.WriteLine("<fileFormat>4.00</fileFormat>")
                'sw.WriteLine("<created>" & Format(Today.Year, "####") & "0" & Format(Today.Month, "##") & Format(Today.Day, "##") & "</created>")
                swCur.WriteLine("<created>" & Today.ToString("yyyyMMdd") & "</created>")
                swCur.WriteLine("<pointInTime>")
                'sw.WriteLine("<date>" & Format(Today.Year, "####") & "0" & Format(Today.Month, "##") & Format(Today.Day, "##") & "</date>")
                swCur.WriteLine("<date>" & Today.ToString("yyyyMMdd") & "</date>")
                swCur.WriteLine("<isSetl>0</isSetl>")
                swCur.WriteLine("<time>:::::</time>")
                swCur.WriteLine("<run>0</run>")
                'sw.WriteLine("<pointInTime>")
                'sw.WriteLine("<date></date>")
                'sw.WriteLine("<isSetl>0</isSetl>")
                'sw.WriteLine("<time>:::::</time>")
                'sw.WriteLine("<run>0</run>")

                'loop for each client

                'Debug.WriteLine(cur_position_client_list)


                'For Each drow_client In mTbl_ledger.Select(cur_position_client_list)
                For i As Integer = 0 To ar_comp.Length - 1
                    client_code = ar_comp(i)
                    temp_comp_name = ""

                    swCur.WriteLine("<portfolio>")
                    swCur.WriteLine("<firm>" & client_code.Replace("&", "&amp;") & "</firm>")
                    swCur.WriteLine("<acctId>" & GetSymbol(client_code).Replace("&", "&amp;") & "</acctId>")
                    swCur.WriteLine("<acctType>S</acctType>")
                    swCur.WriteLine("<isCust>1</isCust>")
                    swCur.WriteLine("<seg>N/A</seg>")

                    swCur.WriteLine("<acctSubType>")
                    swCur.WriteLine("<acctSubTypeCode>GSCIER</acctSubTypeCode>")
                    swCur.WriteLine("<value>0</value>")
                    swCur.WriteLine("</acctSubType>")

                    swCur.WriteLine("<acctSubType>")
                    swCur.WriteLine("<acctSubTypeCode>TRAKRS</acctSubTypeCode>")
                    swCur.WriteLine("<value>0</value>")
                    swCur.WriteLine("</acctSubType>")

                    swCur.WriteLine("<isNew>1</isNew>")
                    swCur.WriteLine("<custPortUseLov>0</custPortUseLov>")
                    swCur.WriteLine("<currency>INR</currency>")
                    swCur.WriteLine("<ledgerBal>0.00</ledgerBal>")
                    swCur.WriteLine("<ote>0.00</ote>")
                    swCur.WriteLine("<securities>0.00</securities>")
                    swCur.WriteLine("<lue>0.00</lue>")

                    swCur.WriteLine("<ecPort>")
                    swCur.WriteLine("<ec>NSCCL</ec>")

                    ''loop for each position
                    For Each drow_position In mDtPortFo.Select("company = '" & client_code & "' and units <> 0", "company,strikes")
                        If CBool(drow_position("IsCurrency")) = True Then
                            comp_name = drow_position("company")
                            'comment for testing
                            If InStr(comp_name, "&") > 0 Then
                                comp_name = Replace(comp_name, "&", "&amp;")
                            End If

                            If UCase(drow_position("cp")) = "F" Then
                                option_type = "FUT"
                                CAL_PUT = ""
                            Else
                                option_type = "OOP"
                                CAL_PUT = Mid(UCase(drow_position("cp")), 1, 1)
                            End If
                            'If client_code.Contains("INR") Then
                            'mat_date = Format(drow_position("mdate"), "yyyyMM")
                            'Else
                            mat_date = Format(drow_position("mdate"), "yyyyMMdd")
                            'End If

                            strike_price = FormatNumber(drow_position("strikes"), 2, TriState.False, TriState.False, TriState.False)
                            If CBool(drow_position("IsCurrency")) = True Then
                                If CURRENCY_MARGIN_QTY = 1 Then
                                    qty = drow_position("Units")
                                Else
                                    qty = drow_position("Lots")
                                End If

                            Else
                                qty = 0 'drow_position("units")
                            End If


                            If temp_comp_name <> comp_name Then
                                If temp_comp_name <> "" Then
                                    swCur.WriteLine("</ccPort>")
                                End If
                                swCur.WriteLine("<ccPort>")
                                swCur.WriteLine("<cc>" & GetSymbol(comp_name) & "</cc>")
                                swCur.WriteLine("<r>1</r>")
                                swCur.WriteLine("<currency>INR</currency>")
                                swCur.WriteLine("<pss>0</pss>")
                            End If

                            swCur.WriteLine("<np>")
                            swCur.WriteLine("<exch>" + mExchange + "</exch>")

                            swCur.WriteLine("<pfCode>" & GetSymbol(comp_name) & "</pfCode>") '//Viral
                            swCur.WriteLine("<pfType>" & option_type & "</pfType>")
                            swCur.WriteLine("<pe>" & mat_date & "</pe>")
                            If option_type = "OOP" Then
                                'If client_code.Contains("INR") Then
                                'sw.WriteLine("<undPe>000000</undPe>")
                                'Else
                                swCur.WriteLine("<undPe>00000000</undPe>")
                                'End If

                                swCur.WriteLine("<o>" & CAL_PUT & "</o>")
                                swCur.WriteLine("<k>" & strike_price & "00" & "</k>")
                            End If
                            swCur.WriteLine("<net>" & qty & "</net>")
                            swCur.WriteLine("</np>")

                            temp_comp_name = comp_name

                        End If
                    Next
                    swCur.WriteLine("</ccPort>")
                    'end of loop for each position


                    swCur.WriteLine("</ecPort>")
                    swCur.WriteLine("</portfolio>")
                Next
                swCur.WriteLine("</pointInTime>")
                'sw.WriteLine("</pointInTime>")
                swCur.WriteLine("</posFile>")
                swCur.Close()
                fs_Curinput.Close()

            End Using
        End Using
    End Sub

    Private Sub inpFo(ar_comp As String())
        ' Dim temp As Integer

        '  Dim fs_input As New FileStream(mSPAN_path & "\input.txt", FileMode.Create)

        Dim client_code As String = Nothing
        Dim temp_comp_name As String = Nothing
        Dim comp_name As String = Nothing
        Dim option_type As String = Nothing
        Dim mat_date As String = Nothing
        Dim CAL_PUT As String = Nothing
        Dim strike_price As String = Nothing
        Dim qty As String = Nothing
        Dim drow_position As DataRow = Nothing
        'Dim ar_comp As String() = Nothing

        'Dim ht_comp As New Hashtable

        ''add analysis companies to hashtable
        'For Each drow As DataRow In dtable.DefaultView.ToTable(True, "company").Rows
        '    If ht_comp.ContainsKey(drow("company")) = False Then
        '        ht_comp.Add(drow("company"), 1)
        '    End If
        'Next
        'ht_comp.Keys.CopyTo(ar_comp, 0)

        Using fs_input As New FileStream(mSPAN_path & "\input.txt", FileMode.Create, FileAccess.Write, FileShare.None)
            Using swInput As New StreamWriter(fs_input)
                '      sw = New StreamWriter(fs_input)
                swInput.WriteLine("<?xml version=""" & "1.0""" & "?>")
                swInput.WriteLine("<posFile>")
                swInput.WriteLine("<fileFormat>4.00</fileFormat>")
                swInput.WriteLine("<created>" & Format(Today.Year, "####") & Format(Today.Month, "##") & Format(Today.Day, "##") & "</created>")
                swInput.WriteLine("<pointInTime>")
                swInput.WriteLine("<date></date>")
                swInput.WriteLine("<isSetl>0</isSetl>")
                swInput.WriteLine("<time>:::::</time>")
                swInput.WriteLine("<run>0</run>")
                swInput.WriteLine("<pointInTime>")
                swInput.WriteLine("<date></date>")
                swInput.WriteLine("<isSetl>0</isSetl>")
                swInput.WriteLine("<time>:::::</time>")
                swInput.WriteLine("<run>0</run>")

                'loop for each client

                'Debug.WriteLine(cur_position_client_list)


                'For Each drow_client In mTbl_ledger.Select(cur_position_client_list)
                For i As Integer = 0 To ar_comp.Length - 1
                    client_code = ar_comp(i)
                    temp_comp_name = ""

                    swInput.WriteLine("<portfolio>")
                    swInput.WriteLine("<firm>" & client_code.Replace("&", "&amp;") & "</firm>") 'Viral20Oct16 //.Replace("&", "")
                    swInput.WriteLine("<acctId>" & GetSymbol(client_code).Replace("&", "&amp;") & "</acctId>") 'Viral20Oct16 //.Replace("&", "")
                    swInput.WriteLine("<acctType>S</acctType>")
                    swInput.WriteLine("<isCust>1</isCust>")
                    swInput.WriteLine("<seg>N/A</seg>")
                    swInput.WriteLine("<isNew>1</isNew>")
                    swInput.WriteLine("<pclient>0</pclient>")
                    swInput.WriteLine("<currency>INR</currency>")
                    swInput.WriteLine("<ledgerBal>0.00</ledgerBal>")
                    swInput.WriteLine("<ote>0.00</ote>")
                    swInput.WriteLine("<securities>0.00</securities>")

                    swInput.WriteLine("<ecPort>")
                    swInput.WriteLine("<ec>NSCCL</ec>")

                    ''loop for each position
                    For Each drow_position In mDtPortFo.Select("company = '" & client_code & "' and units <> 0", "company,strikes")
                        comp_name = drow_position("company")
                        'M&amp;MFIN
                        If InStr(comp_name, "&") > 0 Then
                            comp_name = Replace(comp_name, "&", "&amp;")
                        End If

                        'If InStr(comp_name, "&") > 0 Then
                        'comp_name = Replace(comp_name, "&", "")
                        'End If

                        If UCase(drow_position("cp")) = "F" Then
                            option_type = "FUT"
                            CAL_PUT = ""
                        Else
                            option_type = "OOP"
                            CAL_PUT = Mid(UCase(drow_position("cp")), 1, 1)
                        End If
                        If comp_name.Contains("INR") Then
                            mat_date = Format(drow_position("mdate"), "yyyyMM")
                        Else
                            mat_date = Format(drow_position("mdate"), "yyyyMMdd")
                        End If

                        strike_price = FormatNumber(drow_position("strikes"), 2, TriState.False, TriState.False, TriState.False)
                        If CBool(drow_position("IsCurrency")) = True Then
                            qty = 0 'drow_position("Lots")
                        Else
                            qty = drow_position("units")
                        End If


                        If temp_comp_name <> comp_name Then
                            If temp_comp_name <> "" Then
                                swInput.WriteLine("</ccPort>")
                            End If
                            swInput.WriteLine("<ccPort>")
                            swInput.WriteLine("<cc>" & GetSymbol(comp_name) & "</cc>")
                            swInput.WriteLine("<r>1</r>")
                            swInput.WriteLine("<currency>INR</currency>")
                            swInput.WriteLine("<pss>0</pss>")
                        End If

                        swInput.WriteLine("<np>")
                        swInput.WriteLine("<exch>" + mExchange + "</exch>")

                        swInput.WriteLine("<pfCode>" & GetSymbol(comp_name) & "</pfCode>")
                        swInput.WriteLine("<pfType>" & option_type & "</pfType>")
                        swInput.WriteLine("<pe>" & mat_date & "</pe>")
                        If option_type = "OOP" Then
                            If comp_name.Contains("INR") Then
                                swInput.WriteLine("<undPe>000000</undPe>")
                            Else
                                swInput.WriteLine("<undPe>00000000</undPe>")
                            End If

                            swInput.WriteLine("<o>" & CAL_PUT & "</o>")
                            swInput.WriteLine("<k>" & strike_price & "</k>")
                        End If
                        swInput.WriteLine("<net>" & qty & "</net>")
                        swInput.WriteLine("</np>")

                        temp_comp_name = comp_name
                        Dim drrow As DataRow
                        Dim cp As String
                        Dim result As String() = drow_position("script").Split(New String() {"  "}, StringSplitOptions.None)
                        Dim ael_expo As Double
                        If CALMARGINWITH_AEL_EXPO = 1 Then

                            If drow_position("cp") = "C" Then
                                cp = "CE"
                            ElseIf drow_position("cp") = "P" Then
                                cp = "PE"
                            ElseIf drow_position("cp") = "F" Then
                                cp = "XX"
                            End If

                            Dim value As Double = 0
                            Dim ELRPR As Double = 0
                            If drow_position("toqty") < 0 Then

                                value = Convert.ToDouble(drow_position("toqty").ToString()) * Convert.ToDouble(drow_position("last").ToString())
                                If mTbl_ael_Additional_Expo Is Nothing Then
                                    ELRPR = mTbl_ael_Additional_Expo.Select("InsType = '" & result(1) & "' and Symbol='" & result(0) & "' and StrikePrice = '" & result(3).Replace(".00", "") & "' and OptType='" & cp & "' and ExpDate = #" & result(2) & "#")(0).Item("ELMPer")

                                Else
                                    ELRPR = 0
                                End If

                                ael_expo = (value * ELRPR) / 100
                            End If


                            drrow = mTbl_AEL_Expo_calc.NewRow
                            drrow("AEL_EXPOSURE") = ael_expo
                            drrow("Company") = client_code

                            mTbl_AEL_Expo_calc.Rows.Add(drrow)

                        End If
                    Next
                    swInput.WriteLine("</ccPort>")
                    'end of loop for each position


                    swInput.WriteLine("</ecPort>")
                    swInput.WriteLine("</portfolio>")
                Next
                swInput.WriteLine("</pointInTime>")
                swInput.WriteLine("</pointInTime>")
                swInput.WriteLine("</posFile>")
                '   sw.Close()
                '  fs_input.Close()
            End Using
        End Using
    End Sub

    Public mHtBseComp As Hashtable

    Public Function GetBseComp(pComp As String) As String
        Dim inpStr As String = GetSymbol(pComp)
        If mHtBseComp.ContainsKey(inpStr) Then
            Return mHtBseComp(inpStr)
        Else
            Return ""
        End If
    End Function

    Private Sub CreateBseBatchFiles()
        ' ==== generate.bat ====
        Dim batchPath As String = System.IO.Path.Combine(mSPAN_path, "generate.bat")
        Dim spanCmd As String = """" & System.IO.Path.Combine(mSPAN_path, "spanit") & """ """ &
                    System.IO.Path.Combine(mSPAN_path, "BseSpan.spn") & """"

        Using fs_batchfile As New FileStream(batchPath, FileMode.Create, FileAccess.Write, FileShare.None)
            Using sw As New StreamWriter(fs_batchfile)
                sw.WriteLine(spanCmd)
            End Using
        End Using



        ' ==== curgenerate.bat ====
        Dim curBatchPath As String = System.IO.Path.Combine(mSPAN_path, "curgenerate.bat")
        Dim curSpanCmd As String = """" & System.IO.Path.Combine(mSPAN_path, "spanit") & """ """ &
                       System.IO.Path.Combine(mSPAN_path, "curspan.spn") & """"

        Using fs_Curbatchfile As New FileStream(curBatchPath, FileMode.Create, FileAccess.Write, FileShare.None)
            Using swCur As New StreamWriter(fs_Curbatchfile)
                swCur.WriteLine(curSpanCmd)
            End Using
        End Using
    End Sub

    Private Sub CreateCurSpan()
        Dim curSpnPath As String = System.IO.Path.Combine(mSPAN_path, "curspan.spn")
        Dim curInput As String = System.IO.Path.Combine(mSPAN_path, "curinput.txt")
        Dim curOutput As String = System.IO.Path.Combine(mSPAN_path, "curoutput.xml")
        Dim curSPANFile As String = System.IO.Path.Combine(mSPAN_path, mCurrent_CurSPAN_file)

        Using fs_Curspn As New FileStream(curSpnPath, FileMode.Create, FileAccess.Write, FileShare.None)
            Using cursw As New StreamWriter(fs_Curspn)

                ' Quote all paths for safety
                cursw.WriteLine("LOAD " & curSPANFile)
                cursw.WriteLine("LOAD " & curInput & ",USEXTLAYOUT")
                cursw.WriteLine("CALC")
                cursw.WriteLine("SAVE " & curOutput)
            End Using
        End Using
    End Sub

    Private Sub CreateBseSpan()
        If mExchaneSpanFilePath.Length > 0 Then
            If (File.Exists(mSPAN_path & "\" & mExchaneSpanFilePath)) Then
                mCurrent_SPAN_file = mExchaneSpanFilePath
            End If
        End If

        Dim fullSpnPath As String = System.IO.Path.Combine(mSPAN_path, "BseSpan.spn")
        Dim inputFile As String = System.IO.Path.Combine(mSPAN_path, "input.txt")
        Dim outputFile As String = System.IO.Path.Combine(mSPAN_path, mExchange + "output.xml")

        Using fs_spn As New FileStream(fullSpnPath, FileMode.Create, FileAccess.Write, FileShare.None)
            Using swSpn As New StreamWriter(fs_spn)

                ' Quote file paths in case of spaces
                swSpn.WriteLine("LOAD " & mSPAN_path & "\" & mCurrent_SPAN_file)
                swSpn.WriteLine("Load " & inputFile & ",USEXTLAYOUT")
                swSpn.WriteLine("Calc")
                swSpn.WriteLine("Save " & outputFile)
            End Using ' StreamWriter auto closes and flushes
        End Using ' FileStream auto closes
    End Sub

    Private Sub DeleteExistingFiles()
        Try

            'If File.Exists(mSPAN_path & "\" + mExchange + "Output.xml") Then
            '    System.IO.File.Delete(mSPAN_path & "\curoutput.xml")
            '    System.IO.File.Delete(mSPAN_path & "\" + mExchange + "output.xml")
            'End If

            'System.IO.File.Delete(mSPAN_path & "\span.spn")
            'System.IO.File.Delete(mSPAN_path & "\curspan.spn")

            'System.IO.File.Delete(mSPAN_path & "\input.txt")
            'System.IO.File.Delete(mSPAN_path & "\curinput.txt")

            'System.IO.File.Delete(mSPAN_path & "\generate.bat")
            'System.IO.File.Delete(mSPAN_path & "\curgenerate.bat")


            CUtils.DeleteWithTimeout(mSPAN_path & "\curoutput.xml")
            CUtils.DeleteWithTimeout(mSPAN_path & "\" & mExchange & "output.xml")
            CUtils.DeleteWithTimeout(mSPAN_path & "\" & mExchange & "Span.spn")
            CUtils.DeleteWithTimeout(mSPAN_path & "\curspan.spn")
            CUtils.DeleteWithTimeout(mSPAN_path & "\input.txt")
            CUtils.DeleteWithTimeout(mSPAN_path & "\curinput.txt")
            CUtils.DeleteWithTimeout(mSPAN_path & "\generate.bat")
            CUtils.DeleteWithTimeout(mSPAN_path & "\curgenerate.bat")



        Catch ex As Exception
            MsgBox("Error In generate_Span_data Method..")
            mFlgThr_Span = False
        End Try
    End Sub

    'Public dtable As DataTable
    Public Sub generate_SPAN_data_NSE() 'As Boolean
        Try
            Dim dtable = New DataTable
            dtable = mMainTable.Copy()

            Dim dtableall As New DataTable
            dtableall = mMainTable.Copy()

            'Dim dtable2 As New DataTable

            ' Try
            For Each item As DataRow In dtableall.Rows

                If item("month").ToString <> "" Then
                    'Dim MONTH As String = item("month").ToString.Substring(0, 3)
                    Dim str As String = CDate(item("mdate")).ToString("MMMdd")
                    Dim MONTH As String = str.Replace(" ", "").ToString().ToUpper()
                    item("company") = item("company").ToString & "/" & "All"
                Else
                    item("company") = item("company").ToString & "/" & item("month").ToString
                End If

                If item("month").ToString <> "" Then
                    item("month") = "All"
                End If
            Next

            For Each item As DataRow In dtable.Rows
                If item("month").ToString <> "" Then
                    '  Dim MONTH As String = item("month").ToString.Substring(0, 3)
                    Dim str As String = CDate(item("mdate")).ToString("MMMdd")
                    Dim MONTH As String = str.Replace(" ", "").ToString().ToUpper()
                    item("company") = item("company").ToString & "/" & MONTH
                Else
                    item("company") = item("company").ToString & "/" & item("month").ToString
                End If
            Next
            'Catch ex As Exception

            'End Try
            dtable.Merge(dtableall, True)

            mTbl_exposure_comp.Clear()
            mTblSpanOutput.Clear()
            mTbl_span_calc.Clear()

            Dim client_code As String
            Dim temp_comp_name As String
            Dim comp_name As String
            Dim option_type As String 'OOP or FUT
            Dim mat_date As String 'yyyymmdd
            Dim CAL_PUT As String 'C or P
            Dim strike_price As String
            Dim qty As String

            Dim ht_comp As New Hashtable
            Dim drow_position As DataRow

            'add analysis companies to hashtable
            For Each drow As DataRow In dtable.DefaultView.ToTable(True, "company").Rows
                If ht_comp.ContainsKey(drow("company")) = False Then
                    ht_comp.Add(drow("company"), 1)
                End If
            Next

            'analysis companies copy to string array
            Dim ar_comp(ht_comp.Count - 1) As String
            ht_comp.Keys.CopyTo(ar_comp, 0)

            'Try
            '    System.IO.File.Delete(mSPAN_path & "\" + mExchange + "output.xml")
            '    If File.Exists(mSPAN_path & "\" + mExchange + "output.xml") Then
            '        System.IO.File.Delete(mSPAN_path & "\curoutput.xml")
            '    End If

            '    System.IO.File.Delete(mSPAN_path & "\span.spn")
            '    System.IO.File.Delete(mSPAN_path & "\curspan.spn")
            '    System.IO.File.Delete(mSPAN_path & "\input.txt")
            '    System.IO.File.Delete(mSPAN_path & "\curinput.txt")
            '    System.IO.File.Delete(mSPAN_path & "\generate.bat")
            '    System.IO.File.Delete(mSPAN_path & "\curgenerate.bat")
            'Catch ex As Exception
            '    MsgBox("Error In generate_Span_data Method..")
            '    mFlgThr_Span = False
            'End Try

            '  wait(3000)

            'create file stream
            DeleteExistingFiles()

            mSPAN_path = SPAN_PATH
            'CType(Me.MdiParent, mdiMain).StatusBar1.Panels(2).Text = ""
            If Not Directory.Exists(mSPAN_path) Then 'if not correct span software path
                MsgBox("Enter Correct Path For span In setting.")
                Exit Sub
            End If
            'get latest span file
            mCurrent_SPAN_file = get_latest_spn_file(mSPAN_path, "FO")
            mCurrent_CurSPAN_file = get_latest_spn_file(mSPAN_path, "CUR")
            get_expected_latest_spn_file(mCurrent_SPAN_file, "FO")
            get_expected_latest_spn_file(mCurrent_CurSPAN_file, "CUR")

            'CType(Me.MdiParent, mdiMain).StatusBar1.Panels(2).Text = mCurrent_SPAN_file
            If CALMARGINSPAN = 1 Then

                If mCurrent_SPAN_file = "" And mCurrent_CurSPAN_file = "" Then
                    MsgBox("Invalid Span File..!")
                    mFlgThr_Span = False
                    Exit Sub
                End If
            End If


            'Dim fs_spn As New FileStream(mSPAN_path & "\span.spn", FileMode.Create)

            'Dim sw As StreamWriter
            'sw = New StreamWriter(fs_spn)
            'sw.WriteLine("Load " & mSPAN_path & "\input.txt" & ", USEXTLAYOUT")
            'sw.WriteLine("Calc")
            'sw.WriteLine("Save " & mSPAN_path & "\output.xml")
            'sw.Close()
            'fs_spn.Close()


            Dim fullSpnPath As String = System.IO.Path.Combine(mSPAN_path, "span.spn")
            Dim inputFile As String = System.IO.Path.Combine(mSPAN_path, "input.txt")
            Dim outputFile As String = System.IO.Path.Combine(mSPAN_path, mExchange + "output.xml")

            Using fs_spn As New FileStream(fullSpnPath, FileMode.Create, FileAccess.Write, FileShare.None)
                Using swSpn As New StreamWriter(fs_spn)

                    ' Quote file paths in case of spaces
                    swSpn.WriteLine("LOAD " & mSPAN_path & "\" & mCurrent_SPAN_file)
                    swSpn.WriteLine("Load " & inputFile & ", USEXTLAYOUT")
                    swSpn.WriteLine("Calc")
                    swSpn.WriteLine("Save " & outputFile)
                End Using ' StreamWriter auto closes and flushes
            End Using ' FileStream auto closes


            'Dim fs_Curspn As New FileStream(mSPAN_path & "\curspan.spn", FileMode.Create)
            'Dim cursw As StreamWriter
            'cursw = New StreamWriter(fs_Curspn)
            'cursw.WriteLine("LOAD " & mSPAN_path & "\" & mCurrent_CurSPAN_file)
            'cursw.WriteLine("Load " & mSPAN_path & "\curinput.txt" & ", USEXTLAYOUT")
            'cursw.WriteLine("Calc")
            'cursw.WriteLine("Save " & mSPAN_path & "\curoutput.xml")
            'cursw.Close()
            'fs_Curspn.Close()


            Dim curSpnPath As String = System.IO.Path.Combine(mSPAN_path, "curspan.spn")
            Dim curInput As String = System.IO.Path.Combine(mSPAN_path, "curinput.txt")
            Dim curOutput As String = System.IO.Path.Combine(mSPAN_path, "curoutput.xml")
            Dim curSPANFile As String = System.IO.Path.Combine(mSPAN_path, mCurrent_CurSPAN_file)

            Using fs_Curspn As New FileStream(curSpnPath, FileMode.Create, FileAccess.Write, FileShare.None)
                Using cursw As New StreamWriter(fs_Curspn)

                    ' Quote all paths for safety
                    cursw.WriteLine("LOAD " & curSPANFile)
                    cursw.WriteLine("LOAD " & curInput & ", USEXTLAYOUT")
                    cursw.WriteLine("CALC")
                    cursw.WriteLine("SAVE " & curOutput)
                End Using
            End Using

            'Dim fs_batchfile As New FileStream(mSPAN_path & "\generate.bat", FileMode.Create)

            'sw = New StreamWriter(fs_batchfile)
            'sw.WriteLine(mSPAN_path & "\spanit " & mSPAN_path & "\span.spn")
            'sw.Close()
            'fs_batchfile.Close()

            'Dim fs_Curbatchfile As New FileStream(mSPAN_path & "\curgenerate.bat", FileMode.Create)

            'sw = New StreamWriter(fs_Curbatchfile)
            'sw.WriteLine(mSPAN_path & "\spanit " & mSPAN_path & "\curspan.spn")
            'sw.Close()
            'fs_Curbatchfile.Close()

            ' ==== generate.bat ====
            Dim batchPath As String = System.IO.Path.Combine(mSPAN_path, "generate.bat")
            Dim spanCmd As String = """" & System.IO.Path.Combine(mSPAN_path, "spanit") & """ """ &
                        System.IO.Path.Combine(mSPAN_path, "span.spn") & """"

            Using fs_batchfile As New FileStream(batchPath, FileMode.Create, FileAccess.Write, FileShare.None)
                Using sw As New StreamWriter(fs_batchfile)
                    sw.WriteLine(spanCmd)
                End Using
            End Using



            ' ==== curgenerate.bat ====
            Dim curBatchPath As String = System.IO.Path.Combine(mSPAN_path, "curgenerate.bat")
            Dim curSpanCmd As String = """" & System.IO.Path.Combine(mSPAN_path, "spanit") & """ """ &
                           System.IO.Path.Combine(mSPAN_path, "curspan.spn") & """"

            Using fs_Curbatchfile As New FileStream(curBatchPath, FileMode.Create, FileAccess.Write, FileShare.None)
                Using swCur As New StreamWriter(fs_Curbatchfile)
                    swCur.WriteLine(curSpanCmd)
                End Using
            End Using


            ' Dim temp As Integer

            '  Dim fs_input As New FileStream(mSPAN_path & "\input.txt", FileMode.Create)
            Using fs_input As New FileStream(mSPAN_path & "\input.txt", FileMode.Create, FileAccess.Write, FileShare.None)
                Using swInput As New StreamWriter(fs_input)
                    '      sw = New StreamWriter(fs_input)
                    swInput.WriteLine("<?xml version=""" & "1.0""" & "?>")
                    swInput.WriteLine("<posFile>")
                    swInput.WriteLine("<fileFormat>4.00</fileFormat>")
                    swInput.WriteLine("<created>" & Format(Today.Year, "####") & Format(Today.Month, "##") & Format(Today.Day, "##") & "</created>")
                    swInput.WriteLine("<pointInTime>")
                    swInput.WriteLine("<date></date>")
                    swInput.WriteLine("<isSetl>0</isSetl>")
                    swInput.WriteLine("<time>:: </time>")
                    swInput.WriteLine("<run>0</run>")
                    swInput.WriteLine("<pointInTime>")
                    swInput.WriteLine("<date></date>")
                    swInput.WriteLine("<isSetl>0</isSetl>")
                    swInput.WriteLine("<time>:::::</time>")
                    swInput.WriteLine("<run>0</run>")

                    'loop for each client

                    'Debug.WriteLine(cur_position_client_list)


                    'For Each drow_client In mTbl_ledger.Select(cur_position_client_list)
                    For i As Integer = 0 To ar_comp.Length - 1
                        client_code = ar_comp(i)
                        temp_comp_name = ""

                        swInput.WriteLine("<portfolio>")
                        swInput.WriteLine("<firm>" & client_code.Replace("&", "&amp;") & "</firm>") 'Viral20Oct16 //.Replace("&", "")
                        swInput.WriteLine("<acctId>" & GetSymbol(client_code).Replace("&", "&amp;") & "</acctId>") 'Viral20Oct16 //.Replace("&", "")
                        swInput.WriteLine("<acctType>S</acctType>")
                        swInput.WriteLine("<isCust>1</isCust>")
                        swInput.WriteLine("<seg>N/A</seg>")
                        swInput.WriteLine("<isNew>1</isNew>")
                        swInput.WriteLine("<pclient>0</pclient>")
                        swInput.WriteLine("<currency>INR</currency>")
                        swInput.WriteLine("<ledgerBal>0.00</ledgerBal>")
                        swInput.WriteLine("<ote>0.00</ote>")
                        swInput.WriteLine("<securities>0.00</securities>")

                        swInput.WriteLine("<ecPort>")
                        swInput.WriteLine("<ec>NSCCL</ec>")

                        ''loop for each position
                        For Each drow_position In dtable.Select("company = '" & client_code & "' and units <> 0", "company,strikes")
                            comp_name = drow_position("company")
                            'M&amp;MFIN
                            If InStr(comp_name, "&") > 0 Then
                                comp_name = Replace(comp_name, "&", "&amp;")
                            End If

                            'If InStr(comp_name, "&") > 0 Then
                            'comp_name = Replace(comp_name, "&", "")
                            'End If



                            If UCase(drow_position("cp")) = "F" Then
                                option_type = "FUT"
                                CAL_PUT = ""
                            Else
                                option_type = "OOP"
                                CAL_PUT = Mid(UCase(drow_position("cp")), 1, 1)
                            End If
                            If comp_name.Contains("INR") Then
                                mat_date = Format(drow_position("mdate"), "yyyyMM")
                            Else
                                mat_date = Format(drow_position("mdate"), "yyyyMMdd")
                            End If

                            strike_price = FormatNumber(drow_position("strikes"), 2, TriState.False, TriState.False, TriState.False)
                            If CBool(drow_position("IsCurrency")) = True Then
                                qty = 0 'drow_position("Lots")
                            Else
                                qty = drow_position("units")
                            End If


                            If temp_comp_name <> comp_name Then
                                If temp_comp_name <> "" Then
                                    swInput.WriteLine("</ccPort>")
                                End If
                                swInput.WriteLine("<ccPort>")
                                swInput.WriteLine("<cc>" & GetSymbol(comp_name) & "</cc>")
                                swInput.WriteLine("<r>1</r>")
                                swInput.WriteLine("<currency>INR</currency>")
                                swInput.WriteLine("<pss>0</pss>")
                            End If

                            swInput.WriteLine("<np>")
                            swInput.WriteLine("<exch>NSE</exch>")

                            swInput.WriteLine("<pfCode>" & GetSymbol(comp_name) & "</pfCode>")
                            swInput.WriteLine("<pfType>" & option_type & "</pfType>")
                            swInput.WriteLine("<pe>" & mat_date & "</pe>")
                            If option_type = "OOP" Then
                                If comp_name.Contains("INR") Then
                                    swInput.WriteLine("<undPe>000000</undPe>")
                                Else
                                    swInput.WriteLine("<undPe>00000000</undPe>")
                                End If

                                swInput.WriteLine("<o>" & CAL_PUT & "</o>")
                                swInput.WriteLine("<k>" & strike_price & "</k>")
                            End If
                            swInput.WriteLine("<net>" & qty & "</net>")
                            swInput.WriteLine("</np>")

                            temp_comp_name = comp_name
                            Dim drrow As DataRow
                            Dim cp As String
                            Dim result As String() = drow_position("script").Split(New String() {"  "}, StringSplitOptions.None)
                            Dim ael_expo As Double
                            If CALMARGINWITH_AEL_EXPO = 1 Then

                                If drow_position("cp") = "C" Then
                                    cp = "CE"
                                ElseIf drow_position("cp") = "P" Then
                                    cp = "PE"
                                ElseIf drow_position("cp") = "F" Then
                                    cp = "XX"
                                End If

                                Dim value As Double = 0
                                Dim ELRPR As Double = 0
                                If drow_position("toqty") < 0 Then

                                    value = Convert.ToDouble(drow_position("toqty").ToString()) * Convert.ToDouble(drow_position("last").ToString())
                                    If mTbl_ael_Additional_Expo Is Nothing Then
                                        ELRPR = mTbl_ael_Additional_Expo.Select("InsType = '" & result(1) & "' and Symbol='" & result(0) & "' and StrikePrice = '" & result(3).Replace(".00", "") & "' and OptType='" & cp & "' and ExpDate = #" & result(2) & "#")(0).Item("ELMPer")

                                    Else
                                        ELRPR = 0
                                    End If

                                    ael_expo = (value * ELRPR) / 100
                                End If


                                drrow = mTbl_AEL_Expo_calc.NewRow
                                drrow("AEL_EXPOSURE") = ael_expo
                                drrow("Company") = client_code

                                mTbl_AEL_Expo_calc.Rows.Add(drrow)

                            End If
                        Next
                        swInput.WriteLine("</ccPort>")
                        'end of loop for each position


                        swInput.WriteLine("</ecPort>")
                        swInput.WriteLine("</portfolio>")
                    Next
                    swInput.WriteLine("</pointInTime>")
                    swInput.WriteLine("</pointInTime>")
                    swInput.WriteLine("</posFile>")
                    '   sw.Close()
                    '  fs_input.Close()
                End Using
            End Using

            '   Dim fs_Curinput As New FileStream(, FileMode.Create)

            Using fs_Curinput As New FileStream(mSPAN_path & "\curinput.txt", FileMode.Create, FileAccess.Write, FileShare.None)
                Using swCur As New StreamWriter(fs_Curinput)
                    '''''Currinpitfile
                    '   sw = New StreamWriter(fs_Curinput)
                    swCur.WriteLine("<?xml version=""" & "1.0""" & "?>")
                    swCur.WriteLine("<posFile>")
                    swCur.WriteLine("<fileFormat>4.00</fileFormat>")
                    'sw.WriteLine("<created>" & Format(Today.Year, "####") & "0" & Format(Today.Month, "##") & Format(Today.Day, "##") & "</created>")
                    swCur.WriteLine("<created>" & Today.ToString("yyyyMMdd") & "</created>")
                    swCur.WriteLine("<pointInTime>")
                    'sw.WriteLine("<date>" & Format(Today.Year, "####") & "0" & Format(Today.Month, "##") & Format(Today.Day, "##") & "</date>")
                    swCur.WriteLine("<date>" & Today.ToString("yyyyMMdd") & "</date>")
                    swCur.WriteLine("<isSetl>0</isSetl>")
                    swCur.WriteLine("<time>:::::</time>")
                    swCur.WriteLine("<run>0</run>")
                    'sw.WriteLine("<pointInTime>")
                    'sw.WriteLine("<date></date>")
                    'sw.WriteLine("<isSetl>0</isSetl>")
                    'sw.WriteLine("<time>:::::</time>")
                    'sw.WriteLine("<run>0</run>")

                    'loop for each client

                    'Debug.WriteLine(cur_position_client_list)


                    'For Each drow_client In mTbl_ledger.Select(cur_position_client_list)
                    For i As Integer = 0 To ar_comp.Length - 1
                        client_code = ar_comp(i)
                        temp_comp_name = ""

                        swCur.WriteLine("<portfolio>")
                        swCur.WriteLine("<firm>" & client_code.Replace("&", "&amp;") & "</firm>")
                        swCur.WriteLine("<acctId>" & GetSymbol(client_code).Replace("&", "&amp;") & "</acctId>")
                        swCur.WriteLine("<acctType>S</acctType>")
                        swCur.WriteLine("<isCust>1</isCust>")
                        swCur.WriteLine("<seg>N/A</seg>")

                        swCur.WriteLine("<acctSubType>")
                        swCur.WriteLine("<acctSubTypeCode>GSCIER</acctSubTypeCode>")
                        swCur.WriteLine("<value>0</value>")
                        swCur.WriteLine("</acctSubType>")

                        swCur.WriteLine("<acctSubType>")
                        swCur.WriteLine("<acctSubTypeCode>TRAKRS</acctSubTypeCode>")
                        swCur.WriteLine("<value>0</value>")
                        swCur.WriteLine("</acctSubType>")

                        swCur.WriteLine("<isNew>1</isNew>")
                        swCur.WriteLine("<custPortUseLov>0</custPortUseLov>")
                        swCur.WriteLine("<currency>INR</currency>")
                        swCur.WriteLine("<ledgerBal>0.00</ledgerBal>")
                        swCur.WriteLine("<ote>0.00</ote>")
                        swCur.WriteLine("<securities>0.00</securities>")
                        swCur.WriteLine("<lue>0.00</lue>")

                        swCur.WriteLine("<ecPort>")
                        swCur.WriteLine("<ec>NSCCL</ec>")

                        ''loop for each position
                        For Each drow_position In dtable.Select("company = '" & client_code & "' and units <> 0", "company,strikes")
                            If CBool(drow_position("IsCurrency")) = True Then
                                comp_name = drow_position("company")
                                'comment for testing
                                If InStr(comp_name, "&") > 0 Then
                                    comp_name = Replace(comp_name, "&", "&amp;")
                                End If

                                If UCase(drow_position("cp")) = "F" Then
                                    option_type = "FUT"
                                    CAL_PUT = ""
                                Else
                                    option_type = "OOP"
                                    CAL_PUT = Mid(UCase(drow_position("cp")), 1, 1)
                                End If
                                'If client_code.Contains("INR") Then
                                'mat_date = Format(drow_position("mdate"), "yyyyMM")
                                'Else
                                mat_date = Format(drow_position("mdate"), "yyyyMMdd")
                                'End If

                                strike_price = FormatNumber(drow_position("strikes"), 2, TriState.False, TriState.False, TriState.False)
                                If CBool(drow_position("IsCurrency")) = True Then
                                    If CURRENCY_MARGIN_QTY = 1 Then
                                        qty = drow_position("Units")
                                    Else
                                        qty = drow_position("Lots")
                                    End If

                                Else
                                    qty = 0 'drow_position("units")
                                End If


                                If temp_comp_name <> comp_name Then
                                    If temp_comp_name <> "" Then
                                        swCur.WriteLine("</ccPort>")
                                    End If
                                    swCur.WriteLine("<ccPort>")
                                    swCur.WriteLine("<cc>" & GetSymbol(comp_name) & "</cc>")
                                    swCur.WriteLine("<r>1</r>")
                                    swCur.WriteLine("<currency>INR</currency>")
                                    swCur.WriteLine("<pss>0</pss>")
                                End If

                                swCur.WriteLine("<np>")
                                swCur.WriteLine("<exch>NSE</exch>")

                                swCur.WriteLine("<pfCode>" & GetSymbol(comp_name) & "</pfCode>") '//Viral
                                swCur.WriteLine("<pfType>" & option_type & "</pfType>")
                                swCur.WriteLine("<pe>" & mat_date & "</pe>")
                                If option_type = "OOP" Then
                                    'If client_code.Contains("INR") Then
                                    'sw.WriteLine("<undPe>000000</undPe>")
                                    'Else
                                    swCur.WriteLine("<undPe>00000000</undPe>")
                                    'End If

                                    swCur.WriteLine("<o>" & CAL_PUT & "</o>")
                                    swCur.WriteLine("<k>" & strike_price & "00" & "</k>")
                                End If
                                swCur.WriteLine("<net>" & qty & "</net>")
                                swCur.WriteLine("</np>")

                                temp_comp_name = comp_name

                            End If
                        Next
                        swCur.WriteLine("</ccPort>")
                        'end of loop for each position


                        swCur.WriteLine("</ecPort>")
                        swCur.WriteLine("</portfolio>")
                    Next
                    swCur.WriteLine("</pointInTime>")
                    'sw.WriteLine("</pointInTime>")
                    swCur.WriteLine("</posFile>")
                    swCur.Close()
                    fs_Curinput.Close()

                End Using
            End Using


            Dim outFilePath As String = mSPAN_path & "\output.xml"
            'DeleteFileWithWait(outFilePath, "Output", FlgThr_Span)
            If System.IO.File.Exists(outFilePath) = True Then
                'If System.IO.File.Exists(mSPAN_path & "\output.xml") = True Then
                Try
                    '     System.IO.File.Delete(mSPAN_path & "\output.xml")
                    System.IO.File.Delete(outFilePath)
                Catch ex As Exception
                    MsgBox("Software cannot access SPAN Output file.", MsgBoxStyle.Exclamation)
                    'Return False
                    mFlgThr_Span = False
                    Exit Sub
                End Try
            End If

            Dim outFilePathCur As String = mSPAN_path & "\curoutput.xml"
            'DeleteFileWithWait(outFilePath, "curoutput", FlgThr_Span)

            If System.IO.File.Exists(outFilePathCur) = True Then
                Try
                    System.IO.File.Delete(outFilePathCur)
                Catch ex As Exception
                    MsgBox("Software cannot access SPAN Currency Output file.", MsgBoxStyle.Exclamation)
                    'Return False
                    mFlgThr_Span = False
                    Exit Sub
                End Try
            End If

            'Shell(mSPAN_path & "\generate.bat", AppWinStyle.MinimizedNoFocus)
            'Dim worker As New System.Threading.Thread(AddressOf execute_batch_file)
            'worker.Start()
            'worker.Join()

            If mCurrent_SPAN_file <> "" Then
                Dim worker As New System.Threading.Thread(AddressOf execute_FO_batch_file)
                worker.Name = "thr_exefoSpan"
                worker.Start()
                worker.Join()
            End If

            If mCurrent_CurSPAN_file <> "" Then
                Dim worker2 As New System.Threading.Thread(AddressOf execute_Cur_batch_file)
                worker2.Name = "thr_execurSpan"
                worker2.Start()
                worker2.Join()
            End If


            'execute_batch_file()
            'Process.Start(mSPAN_path & "\spanit", mSPAN_path & "\span.spn")
            'MsgBox("job_completed")

            'If extract_span_req() = False Then
            '    Return False
            '    Exit Function
            'End If
            If mIsOutputFileGenerated = False Then
                Return
            End If

            If CALMARGINWITH_AEL_EXPO = 1 Then
                If Additional_AEL_extract_exposure_margin(mExchange) = False Then
                    'Return False
                    mFlgThr_Span = False
                    Exit Sub

                End If

                'mPerf.PushFileName("ael test")
                'mPerf.WriteLogStr("Old Code")
                'mPerf.WriteLogStr("Table:mTblSpanOutput")

                '   mPerf.PrintDataTable(mTblSpanOutput)
                '   mPerf.PrintDataTable(mTbl_exposure_comp)
                '   mPerf.PrintDataTable(mTbl_span_calc)


                mPerf.PrintTopRecords(mTblSpanOutput, 10)
                mPerf.WriteLogStr("Table:mTbl_exposure_comp")
                mPerf.PrintTopRecords(mTbl_exposure_comp, 10)
                mPerf.WriteLogStr("Table:mTbl_span_calc")
                mPerf.PrintTopRecords(mTbl_span_calc, 10)



                Dim ax As AelSpanXmlReader = New AelSpanXmlReader(mTbl_exposure_comp, mTbl_span_calc, mTblSpanOutput)
                Dim outputXmlFile As String = mSPAN_path & "\" + mExchange + "output.xml"

                CUtils.WaitUntilFree(outputXmlFile, 5000)

                ax.ClearTables()
                ax.ReadFile(outputXmlFile)
                mPerf.WriteLogStr("New Code")
                mPerf.WriteLogStr("Table:mTblSpanOutput")
                mPerf.PrintTopRecords(mTblSpanOutput, 10)
                mPerf.WriteLogStr("Table:mTbl_exposure_comp")
                mPerf.PrintTopRecords(mTbl_exposure_comp, 10)
                mPerf.WriteLogStr("Table:mTbl_span_calc")
                mPerf.PrintTopRecords(mTbl_span_calc, 10)
                mPerf.Pop()

            Else
                If extract_exposure_margin() = False Then
                    'Return False
                    mFlgThr_Span = False
                    Exit Sub
                End If
            End If

            Dim drow_span As DataRow
            Dim fut_opt As String
            Dim mRet_Obj As Object
            Dim mDatabase_margin As Double

            Dim Dv As DataView
            Dv = New DataView(mTbl_exposure_comp.Copy)
            mTbl_exposure_comp = Dv.ToTable(True, "CompName", "mat_MOnth", "P", "fut_opt")

            Dim tblSpn As New DataTable
            Dim column As DataColumn

            column = New DataColumn()
            column.DataType = GetType(String)
            column.ColumnName = "ClientCode"
            column.DefaultValue = ""
            tblSpn.Columns.Add(column) ' company name

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "lfv"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "sfv"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "lov"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "sov"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "spanreq"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column) ' for initial margin  spanreq-anvo

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "anov"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "exposure_margin"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            Dv = New DataView(mTblSpanOutput.Copy)
            Dim row As DataRow
            For Each Drow As DataRow In Dv.ToTable(True, "ClientCode").Rows
                row = tblSpn.NewRow
                row("ClientCode") = Drow("ClientCode")
                row("lfv") = mTblSpanOutput.Compute("Max(lfv)", "ClientCode='" & Drow("ClientCode") & "'")
                row("sfv") = mTblSpanOutput.Compute("Max(sfv)", "ClientCode='" & Drow("ClientCode") & "'")
                row("lov") = mTblSpanOutput.Compute("Max(lov)", "ClientCode='" & Drow("ClientCode") & "'")
                row("sov") = mTblSpanOutput.Compute("Max(sov)", "ClientCode='" & Drow("ClientCode") & "'")
                row("spanreq") = mTblSpanOutput.Compute("Max(spanreq)", "ClientCode='" & Drow("ClientCode") & "'")
                row("anov") = mTblSpanOutput.Compute("Max(anov)", "ClientCode='" & Drow("ClientCode") & "' and anov<>0")
                row("exposure_margin") = mTblSpanOutput.Compute("Max(exposure_margin)", "ClientCode='" & Drow("ClientCode") & "'")
                tblSpn.Rows.Add(row)
                tblSpn.AcceptChanges()
            Next

            mTblSpanOutput = tblSpn
            If AELOPTION = 2 Then
                If GdtBhavcopy.Rows.Count = 0 Then
                    MsgBox("Please Process  Bhavcopy For Exposure Margin Calculatoin..")
                    Return
                End If
            End If

            For Each drow As DataRow In dtable.Select("(cp='F') or (cp<>'F' and units < 0) and cp <> 'E'")


                If UCase(drow("cp")) = "F" Then
                    fut_opt = "FUT"
                    ' For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(drow("company")) & "' and fut_opt='" & fut_opt & "' and mat_month = " & CDate(drow("mdate")).Month)
                    If mTbl_exposure_comp.Select("CompName='" & GetSymbol(drow("company")) & "' and fut_opt='" & fut_opt & "' and mat_month = " & CDate(drow("mdate")).Month).Length > 0 Then


                        Dim drow1 As DataRow = mTbl_exposure_comp.Select("CompName='" & GetSymbol(drow("company")) & "' and fut_opt='" & fut_opt & "' and mat_month = " & CDate(drow("mdate")).Month)(0)

                        For Each drow_span In mTblSpanOutput.Select("clientcode='" & drow("company") & "'")
                            mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(drow("company")) & "'")
                            If Convert.ToBoolean(drow("isCurrency")) = False Then
                                mRet_Obj = GetExposureObject(AELOPTION, mRet_Obj, drow("script").ToString(), GetSymbol(drow("company")), CDate(drow("mdate")), Val(drow("strikes")), UCase(drow("cp")))
                            End If


                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                DEFAULT_EXPO_MARGIN = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='DEFAULT_EXPO_MARGIN'").ToString)
                                ' mDatabase_margin = 0

                                mDatabase_margin = DEFAULT_EXPO_MARGIN / 100
                            End If
                            If drow("units") < 0 Then
                                drow_span("exposure_margin") += ((drow1("p") * (-drow("units")) * mDatabase_margin))
                            Else
                                drow_span("exposure_margin") += ((drow1("p") * drow("units") * mDatabase_margin))
                            End If
                        Next
                    End If
                    ' Next
                Else
                    fut_opt = "OPT"
                    For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(drow("company")) & "' and fut_opt='" & fut_opt & "'")

                        For Each drow_span In mTblSpanOutput.Select("clientcode='" & drow("company") & "'")
                            mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(drow("company")) & "'")

                            If Convert.ToBoolean(drow("isCurrency")) = False Then
                                mRet_Obj = GetExposureObject(AELOPTION, mRet_Obj, drow("script").ToString(), GetSymbol(drow("company")), CDate(drow("mdate")), Val(drow("strikes")), UCase(drow("cp")))
                            End If

                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                DEFAULT_EXPO_MARGIN = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='DEFAULT_EXPO_MARGIN'").ToString)
                                ' mDatabase_margin = 0
                                mDatabase_margin = DEFAULT_EXPO_MARGIN / 100
                            End If
                            If drow("units") < 0 Then
                                drow_span("exposure_margin") += ((drow1("p") * (-drow("units")) * mDatabase_margin))
                            Else
                                drow_span("exposure_margin") += ((drow1("p") * drow("units") * mDatabase_margin))
                            End If
                        Next
                    Next
                End If
            Next

            set_exact_exposure_margin()
            'DataGridView1.DataSource = mTbl_SPAN_output
            'MsgBox("exposure margin: - " & mTbl_SPAN_output.Rows(0)("exposure_margin"))
            'MsgBox("exposure margin: - " & mTbl_SPAN_output.Rows(0)("spanreq") - mTbl_SPAN_output.Rows(0)("anov"))
            'DataGridView1.DataSource = mTbl_exposure_comp
            'DataGridView2.DataSource = mTbl_SPAN_output
            'Return True
        Catch ex As Exception
            'MsgBox(ex.ToString)
            MsgBox("Wait for sum Time")
            'Return False
            mFlgThr_Span = False
        End Try
    End Sub


    Private Function concat_scrip(ByVal comp_name As String, ByVal cal_put_fut As String, ByVal exp_date As String, ByVal strike_price As String) As String
        Dim index_name As String
        Dim a_e As String

        If cal_put_fut = "FUT" Then
            If UCase(comp_name) = "NIFTY" Or UCase(comp_name) = "BANKNIFTY" Or UCase(comp_name) = "CNXIT" Or UCase(comp_name) = "MINIFTY" Or UCase(comp_name) = "CNX100" Or UCase(comp_name) = "JUNIOR" Or UCase(comp_name) = "NFTYMCAP50" Then
                index_name = "FUTIDX"
            ElseIf UCase(comp_name) = "INDIAVIX" Then
                index_name = "FUTIVX"
            Else
                index_name = "FUTSTK"
            End If
        Else
            ' Debug.WriteLine(comp_name)
            If UCase(comp_name) = "NIFTY" Or UCase(comp_name) = "BANKNIFTY" Or UCase(comp_name) = "CNXIT" Or UCase(comp_name) = "MINIFTY" Or UCase(comp_name) = "CNX100" Or UCase(comp_name) = "JUNIOR" Or UCase(comp_name) = "NFTYMCAP50" Then
                index_name = "OPTIDX"
                a_e = "E"
            ElseIf UCase(comp_name) = "INDIAVIX" Then
                index_name = "OPTIVX"
                a_e = "E"
            Else
                index_name = "OPTSTK"
                'a_e = "A"
                a_e = "E"
            End If


            Dim idxNames() As String = {
    "NIFTY",
    "BANKNIFTY",
    "CNXIT",
    "MINIFTY",
    "CNX100",
    "JUNIOR",
    "NFTYMCAP50",
    "BSX",
    "BN"
}

            Dim cname As String = UCase(comp_name)
            index_name = cname
            If idxNames.Contains(cname) Then
                index_name = "OPTIDX"
                a_e = "E"

            ElseIf cname = "INDIAVIX" Then
                index_name = "OPTIVX"
                a_e = "E"

            Else
                index_name = "OPTSTK"
                a_e = "E"
            End If

        End If
        'generate script
        If cal_put_fut = "FUT" Then
            Return index_name & Space(2) & comp_name & Space(2) & exp_date & "  "
        Else
            Return index_name & Space(2) & comp_name & Space(2) & exp_date & "  " & strike_price & "  " & UCase(Mid(cal_put_fut, 1, 1)) & UCase(Mid(a_e, 1, 1))
        End If

    End Function



    Public Sub cal_exp_Margin(compname As String, pTabName As String)
        'calulcate grossMTM and expense
        mDouExpMrg = 0
        mDouIntMrg = 0
        'DouEquity = 0
        'txtexpmrg.Text = 0
        'txtintmrg.Text = 0
        Dim monthName As String
        mEqValue = 0
        'Exposure Magin ##############################################
        'Monthname = CDate(TabStrategy.SelectedTab.Tag).ToString("MMMdd").ToUpper()  'TabStrategy.SelectedTab.Text
        If pTabName.ToUpper() <> "ALL" Then
            monthName = CDate(pTabName).ToString("MMMdd").ToUpper() 'TabStrategy.SelectedTab.Text
        Else
            monthName = pTabName
        End If
        If mTblSpanOutput.Rows.Count > 0 Then
            'For Each drow As DataRow In mTbl_SPAN_output.Select("ClientCode='" & compname & "'")
            For Each drow As DataRow In mTblSpanOutput.Select("ClientCode='" & compname & "/" & monthName & "'")
                mDouExpMrg = Format(mDouExpMrg + Val(drow("exposure_margin").ToString), exmargstr)
                mDouIntMrg = Format(mDouIntMrg + Val(Val(drow("spanreq").ToString) - Val(drow("anov").ToString)), inmargstr)

                'txtexpmrg.Text = Format(Val(txtexpmrg.Text) + Val(drow("exposure_margin").ToString), exmargstr)
                'txtintmrg.Text = Format(Val(txtintmrg.Text) + Val(Val(drow("spanreq").ToString) - Val(drow("anov").ToString)), inmargstr)
            Next
        End If


        'equity ###########################################################
        Dim equity As Double = 0
        Dim strFilter As String = "cp='E' AND company='" + compname + "'" + " AND exchange='" + mExchange + "'"
        'For Each drow As DataRow In mCurTable.Select("cp='E'")
        For Each drow As DataRow In mCurTable.Select(strFilter)
            equity = equity + Val(Val(drow("units")) * Val(drow("traded")))
        Next


        'txtEquity.Text = Format(equity, equitystr)
        'mEqTxt = Format(equity, equitystr)
        mEqValue = equity

        'txttotmarg.Text = Math.Round((DouIntMrg + DouExpMrg + Val(txtEquity.Text)) / 100000, 2)
        mTotalValue = Math.Round((mDouIntMrg + mDouExpMrg + mEqValue) / 100000, 2)

        For Each drow As DataRow In mTblSpanOutput.Rows
            Dim DouExpMrgtemp As Double = 0 ''
            Dim DouIntMrgtemp As Double = 0 ''
            DouExpMrgtemp = Format(DouExpMrgtemp + Val(drow("exposure_margin").ToString), exmargstr)
            DouIntMrgtemp = Format(DouIntMrgtemp + Val(Val(drow("spanreq").ToString) - Val(drow("anov").ToString)), inmargstr)

            Dim equity1 As Double = 0
            For Each drow1 As DataRow In maintable.Select("cp='E' and company='" & drow("ClientCode").Split("/"c)(0) & "'")
                equity1 = equity1 + Val(Val(drow1("units")) * Val(drow1("traded")))
            Next
            If mTblPicMargin.Select("compname = '" & drow("ClientCode") & "'").Length = 0 Then
                Dim drpicmargin As DataRow
                drpicmargin = mTblPicMargin.NewRow

                drpicmargin("compname") = drow("ClientCode")
                drpicmargin("Margin") = Math.Round((DouIntMrgtemp + DouExpMrgtemp + Val(equity1)) / 100000, 2)
                drpicmargin("Date") = System.DateTime.Now

                mTblPicMargin.Rows.Add(drpicmargin)
                mTblPicMargin.AcceptChanges()
            Else
                Dim tmmpicmargin As Double = Convert.ToDouble(mTblPicMargin.Compute("sum(Margin)", "compname='" & drow("ClientCode") & "'"))
                For Each mrow As DataRow In mTblPicMargin.Select("compname = '" & drow("ClientCode") & "'")
                    If tmmpicmargin < Convert.ToDouble(Math.Round((DouIntMrgtemp + DouExpMrgtemp + Val(equity1)) / 100000, 2)) Then
                        mrow("Margin") = Math.Round((DouIntMrgtemp + DouExpMrgtemp + Val(equity1)) / 100000, 2)
                    End If

                Next
            End If
        Next

        If mTblPicMargin.Rows.Count > 0 Then
            ' DataTable_To_Text(mTblPicMargin)
        End If

        If mTblPicMargin.Select("compname='" + compname & "/" + monthName + "'").Length > 0 Then

            mPicMargin = Convert.ToDouble(mTblPicMargin.Compute("sum(Margin)", "compname='" & compname & "/" & monthName & "'"))
        Else
            mPicMargin = 0
        End If

    End Sub

End Class
