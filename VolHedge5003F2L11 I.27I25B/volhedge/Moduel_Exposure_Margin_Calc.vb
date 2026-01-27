Imports System.IO

Module Moduel_Exposure_Margin_Calc

#Region "Margin Variable"
    Public mSPAN_path As String
    Public GSPAN_Timer As Double
    Public GVar_IsMargin_Generate As Boolean = False

    Private mSPANFile_time1 As Date
    Private mSPANFile_time2 As Date
    Private mSPANFile_time3 As Date
    Private mSPANFile_time4 As Date
    Private mSPANFile_time5 As Date
    Private mSPANFile_time6 As Date

    Private mCurrent_SPAN_file As String
    Private exp_latest_spn_file As String
    Private exp_latest_zip_file As String

    Private mTbl_exposure_comp As New DataTable
    Public GTbl_exposure_database As DataTable
    Private mTbl_SPAN_output As New DataTable
    Private mTbl_span_calc As New DataTable

    Public G_DtPositionwise_ExpMargin As DataTable

#End Region

#Region "Margin"

    'Dim ObjTrade As New CLS_trading


    ''' <summary>
    ''' init_span_tables
    ''' </summary>
    ''' <remarks>This method call to initialize SPAN related datatable</remarks>
    Public Sub init_span_tables()
        mTbl_exposure_comp = New DataTable
        With mTbl_exposure_comp
            .Columns.Add("CompName", GetType(String))
            .Columns.Add("mat_month", GetType(Integer))
            .Columns.Add("p", GetType(Double))
            .Columns.Add("fut_opt", GetType(String))
        End With
        mTbl_SPAN_output = New DataTable
        With mTbl_SPAN_output
            .Columns.Add("Dealer", GetType(String)) ' Dealer
            .Columns.Add("lfv", GetType(Double))
            .Columns.Add("sfv", GetType(Double))
            .Columns.Add("lov", GetType(Double))
            .Columns.Add("sov", GetType(Double))
            .Columns.Add("spanreq", GetType(Double)) ' for initial margin  spanreq-anvo
            .Columns.Add("anov", GetType(Double))
            .Columns.Add("exposure_margin", GetType(Double))
        End With
        mTbl_span_calc = New DataTable
        With mTbl_span_calc
            .Columns.Add("description", GetType(String))
            .Columns.Add("compname", GetType(String))
            .Columns.Add("exp_date", GetType(String))
            .Columns.Add("cal_put_fut", GetType(String))
            .Columns.Add("strike_price", GetType(String))
        End With
    End Sub

    ''' <summary>
    ''' generate_SPAN_date
    ''' </summary>
    ''' <param name="dtPositionScript"></param>
    ''' <returns></returns>
    ''' <remarks>This method call to create Input.txt file according to passed position datatable and Generate Output.xml file and fill datatable from Output.xml file</remarks>
    Public Function generate_SPAN_data(ByVal dtPositionScript As DataTable) As Boolean
        If dtPositionScript Is Nothing Then Exit Function
        If dtPositionScript.Rows.Count = 0 Then
            Exit Function
        End If
        Try
            Dim Company_Code As String = ""
            Dim Prefix_code As String = ""
            Dim temp_comp_name As String
            Dim comp_name As String
            Dim option_type As String 'OOP or FUT
            Dim mat_date As String 'yyyymm
            Dim CAL_PUT As String 'C or P
            Dim strike_price As String
            Dim qty As String

            Dim ht_comp As New Hashtable
            Dim ht_Prefix As New Hashtable

            'Dim drow_position As DataRow = Nothing
            Dim ret_data As String = ""
            For Each drow As DataRow In dtPositionScript.DefaultView.ToTable(True, "Prefix", "company").Rows
                If ht_comp.ContainsKey(drow("company")) = False Then
                    ht_comp.Add(drow("company"), 1)
                End If
                If ht_Prefix.ContainsKey(drow("Prefix")) = False Then
                    ht_Prefix.Add(drow("Prefix"), 1)
                End If
            Next
            Dim ar_comp(ht_comp.Count - 1) As String
            Dim ar_Prefix(ht_Prefix.Count - 1) As String
            ht_comp.Keys.CopyTo(ar_comp, 0)
            ht_Prefix.Keys.CopyTo(ar_Prefix, 0)

            Dim fs_spn As New IO.FileStream(mSPAN_path & "\span.spn", FileMode.Create)
            Dim fs_input As New FileStream(mSPAN_path & "\input.txt", FileMode.Create)
            Dim fs_batchfile As New FileStream(mSPAN_path & "\generate.bat", FileMode.Create)

            Dim sw As StreamWriter
            sw = New StreamWriter(fs_spn)

            'CType(Me.MdiParent, mdiMain).StatusBar1.Panels(2).Text = ""
            If Not Directory.Exists(mSPAN_path) Then
                MsgBox("Enter Correct Path for span in setting !!", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            mCurrent_SPAN_file = get_latest_spn_file(mSPAN_path)
            Call get_expected_latest_spn_file(mCurrent_SPAN_file)

            'CType(Me.MdiParent, mdiMain).StatusBar1.Panels(2).Text = mCurrent_SPAN_file

            sw.WriteLine("LOAD " & mSPAN_path & "\" & mCurrent_SPAN_file)
            sw.WriteLine("Load " & mSPAN_path & "\input.txt" & ",USEXTLAYOUT")
            sw.WriteLine("Calc")
            sw.WriteLine("Save " & mSPAN_path & "\output.xml")
            sw.Close()
            fs_spn.Close()

            sw = New StreamWriter(fs_batchfile)
            sw.WriteLine(mSPAN_path & "\spanit " & mSPAN_path & "\span.spn")
            'sw.WriteLine("pause")
            sw.Close()
            fs_batchfile.Close()

            ' Dim temp As Integer

            sw = New StreamWriter(fs_input)
            sw.WriteLine("<?xml version=""" & "1.0""" & "?>")
            sw.WriteLine("<posFile>")
            sw.WriteLine("<fileFormat>4.00</fileFormat>")
            sw.WriteLine("<created>" & Format(Today.Year, "####") & Format(Today.Month, "##") & Format(Today.Day, "##") & "</created>")
            sw.WriteLine("<pointInTime>")
            sw.WriteLine("<date></date>")
            sw.WriteLine("<isSetl>0</isSetl>")
            sw.WriteLine("<time>:::::</time>")
            sw.WriteLine("<run>0</run>")
            sw.WriteLine("<pointInTime>")
            sw.WriteLine("<date></date>")
            sw.WriteLine("<isSetl>0</isSetl>")
            sw.WriteLine("<time>:::::</time>")
            sw.WriteLine("<run>0</run>")

            For i As Integer = 0 To ar_Prefix.Length - 1
                Prefix_code = ar_Prefix(i)
                temp_comp_name = ""
                sw.WriteLine("<portfolio>")
                sw.WriteLine("<firm>SQ0</firm>")
                sw.WriteLine("<acctId>" & Prefix_code & "</acctId>")
                'sw.WriteLine("<acctId>" & Company_Code  & "</acctId>")
                sw.WriteLine("<acctType>S</acctType>")
                sw.WriteLine("<isCust>1</isCust>")
                sw.WriteLine("<seg>N/A</seg>")
                sw.WriteLine("<isNew>1</isNew>")
                sw.WriteLine("<pclient>0</pclient>")
                sw.WriteLine("<currency>INR</currency>")
                sw.WriteLine("<ledgerBal>0.00</ledgerBal>")
                sw.WriteLine("<ote>0.00</ote>")
                sw.WriteLine("<securities>0.00</securities>")

                sw.WriteLine("<ecPort>")
                sw.WriteLine("<ec>NSCCL</ec>")

                ''loop for each position
                For Each drow_position As DataRow In dtPositionScript.Select("Prefix = '" & Prefix_code & "' AND totalunits<>0", "company,strikes")
                    comp_name = drow_position("company")
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
                    mat_date = Format(IIf(IsDBNull(drow_position("mdate")), Now, drow_position("mdate")), "yyyyMM")
                    strike_price = FormatNumber(drow_position("strikes"), 2, TriState.False, TriState.False, TriState.False)
                    qty = drow_position("totalunits")

                    If temp_comp_name <> comp_name Then
                        If temp_comp_name <> "" Then
                            sw.WriteLine("</ccPort>")
                        End If
                        sw.WriteLine("<ccPort>")
                        sw.WriteLine("<cc>" & comp_name & "</cc>")
                        sw.WriteLine("<r>1</r>")
                        sw.WriteLine("<currency>INR</currency>")
                        sw.WriteLine("<pss>0</pss>")
                    End If

                    sw.WriteLine("<np>")
                    sw.WriteLine("<exch>NSE</exch>")
                    sw.WriteLine("<pfCode>" & comp_name & "</pfCode>")
                    sw.WriteLine("<pfType>" & option_type & "</pfType>")
                    sw.WriteLine("<pe>" & mat_date & "</pe>")
                    If option_type = "OOP" Then
                        sw.WriteLine("<undPe>000000</undPe>")
                        sw.WriteLine("<o>" & CAL_PUT & "</o>")
                        sw.WriteLine("<k>" & strike_price & "</k>")
                    End If
                    sw.WriteLine("<net>" & qty & "</net>")
                    sw.WriteLine("</np>")

                    temp_comp_name = comp_name
                Next
                sw.WriteLine("</ccPort>")
                'end of loop for each position

                sw.WriteLine("</ecPort>")
                sw.WriteLine("</portfolio>")
            Next
            sw.WriteLine("</pointInTime>")
            sw.WriteLine("</pointInTime>")
            sw.WriteLine("</posFile>")
            sw.Close()
            fs_input.Close()

            If System.IO.File.Exists(mSPAN_path & "\output.xml") = True Then
                Try
                    System.IO.File.Delete(mSPAN_path & "\output.xml")
                Catch ex As Exception
                    MsgBox("Software can not access SPAN Output file !!", MsgBoxStyle.Exclamation)
                    Return False
                    Exit Function
                End Try
            End If

            'Shell(mSPAN_path & "\generate.bat", AppWinStyle.MinimizedNoFocus)
            Dim worker As New System.Threading.Thread(AddressOf execute_batch_file)
            worker.Start()
            worker.Join()

            If extract_exposure_margin() = False Then
                Return False
                Exit Function
            End If

            Dim drow_span As DataRow
            Dim fut_opt As String
            Dim mRet_Obj As Object
            Dim mDatabase_margin As Double
            For Each drow As DataRow In dtPositionScript.Select("(cp='F') or (cp<>'F' and Totalunits < 0)")
                If LCase(drow("cp")) = "F" Then
                    fut_opt = "FUT"
                    For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & drow("company") & "' and fut_opt='" & fut_opt & "' and mat_month = " & CDate(drow("mdate")).Month)
                        For Each drow_span In mTbl_SPAN_output.Select("Dealer='" & drow("prefix") & "'")
                            mRet_Obj = GTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & drow("company") & "'")
                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                mDatabase_margin = 0
                            End If
                            If drow("Totalunits") < 0 Then
                                drow_span("exposure_margin") += ((drow1("p") * (-drow("Totalunits")) * mDatabase_margin))
                            Else
                                drow_span("exposure_margin") += ((drow1("p") * drow("Totalunits") * mDatabase_margin))
                            End If
                        Next
                    Next
                Else
                    fut_opt = "OPT"
                    For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & drow("company") & "' and fut_opt='" & fut_opt & "'")
                        For Each drow_span In mTbl_SPAN_output.Select("Dealer='" & drow("prefix") & "'")
                            mRet_Obj = GTbl_exposure_database.Compute("SUM(exposure_margin)", "compname='" & drow("company") & "'")
                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                mDatabase_margin = 0
                            End If
                            If drow("Totalunits") < 0 Then
                                drow_span("exposure_margin") += ((drow1("p") * (-drow("Totalunits")) * mDatabase_margin))
                            Else
                                drow_span("exposure_margin") += ((drow1("p") * drow("Totalunits") * mDatabase_margin))
                            End If
                        Next
                    Next
                End If
            Next

            Call set_exact_exposure_margin(dtPositionScript)
            Return True
        Catch ex As Exception
            MsgBox("Exposure_Margin_Dealerwise :: generate_SPAN_Date :: " & ex.ToString)
            Return False
        End Try
    End Function

    Private Function get_latest_spn_file(ByVal path As String) As String
        Dim di As New IO.DirectoryInfo(path)
        Dim aryFi As IO.FileInfo() = di.GetFiles("nsccl*.spn")
        Dim fi As IO.FileInfo
        Dim max As Date
        Dim current As Date
        Dim max_file As String = ""

        For Each fi In aryFi
            Debug.WriteLine(fi.Name)
            If Mid(fi.Name(), 16, 1) = "s" Then
                'If fi.Name.Length > 20 Then
                '    current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " 23:59:59")
                'Else
                If fi.Name.Length = 20 Then
                    current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " 22:59:59")
                End If
                'End If
            Else
                'If fi.Name.Length > 22 Then
                '    current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " " & Mid(fi.Name, 18, 1) & ":" & Mid(fi.Name, 20, 1) & ":00")
                'Else
                If fi.Name.Length = 22 Then
                    current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " " & Mid(fi.Name, 18, 1) & ":00:00")
                End If
                'End If
            End If

            If current > max Then
                max = current
                max_file = fi.Name()
            End If
        Next

        Return max_file
    End Function

    Private Sub set_exact_exposure_margin(ByVal dtDlrPos As DataTable)
        Dim client_code_list As String = ""

        Dim dvDlrPos As DataView
        'Dim DtData As DataTable = Gtbl_dealerWisepos.Select("cp='F'", "company,mdate")
        dvDlrPos = New DataView(dtDlrPos) '.DefaultView

        dvDlrPos.RowFilter = "cp='F'"

        dvDlrPos.Sort = "company,mdate"
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

        While i < dvDlrPos.Count
            drow = dvDlrPos.Item(i)
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

            prv_comp = drow("company")
            qty1 = drow("totalunits")
            mon1 = CDate(drow("mdate")).Month

            If DateDiff(DateInterval.Day, Today.Date, CDate(drow("mdate"))) <= 2 Then
                mat_date = True
            End If
            set1 = 0
            rem1 = drow("totalunits")


            If i = dvDlrPos.Count Then
                Exit While
            End If
            drow = dvDlrPos.Item(i)
            i += 1

            If prv_comp <> drow("company") Then
                GoTo a1
            Else
                prv_comp = drow("company")
                qty2 = drow("totalunits")
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


                If i = dvDlrPos.Count Then
                    GoTo a2
                End If
                drow = dvDlrPos.Item(i)
                i += 1

                If prv_comp <> drow("company") Then
                    GoTo a2
                Else
                    prv_comp = drow("company")
                    qty3 = drow("totalunits")
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

                    If i = dvDlrPos.Count Then
                        GoTo a2
                    End If
                    drow = dvDlrPos.Item(i)
                    i += 1

                End If

a2:
                If qty1 <> rem1 Then
                    For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & prv_comp & "' and fut_opt='fut' and mat_month = " & mon1)

                        For Each drow_span As DataRow In mTbl_SPAN_output.Select("Dealer='" & drow("prefix") & "'")

                            mRet_Obj = GTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & prv_comp & "'")
                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                mDatabase_margin = 0
                            End If

                            drow_span("exposure_margin") -= (drow1("p") * Math.Abs(qty1) * mDatabase_margin)

                            drow_span("exposure_margin") += (drow1("p") * Math.Abs(rem1) * mDatabase_margin)
                        Next
                    Next
                End If

                If (qty2 <> rem2) Or set2 <> 0 Then
                    For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & prv_comp & "' and fut_opt='fut' and mat_month = " & mon2)

                        For Each drow_span As DataRow In mTbl_SPAN_output.Select("Dealer='" & drow("prefix") & "'")

                            mRet_Obj = GTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & prv_comp & "'")
                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                mDatabase_margin = 0
                            End If
                            drow_span("exposure_margin") -= (drow1("p") * Math.Abs(qty2) * mDatabase_margin)
                            drow_span("exposure_margin") += (drow1("p") * Math.Abs(rem2) * mDatabase_margin)
                            drow_span("exposure_margin") += (1 / 3) * (drow1("p") * Math.Abs(set2) * mDatabase_margin)
                        Next
                    Next
                End If

                If (qty3 <> rem3) Or set3 <> 0 Then
                    For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & prv_comp & "' and fut_opt='fut' and mat_month = " & mon3)
                        For Each drow_span As DataRow In mTbl_SPAN_output.Select("Dealer='" & drow("prefix") & "'")
                            mRet_Obj = GTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & prv_comp & "'")
                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                mDatabase_margin = 0
                            End If
                            drow_span("exposure_margin") -= (drow1("p") * Math.Abs(qty3) * mDatabase_margin)
                            drow_span("exposure_margin") += (drow1("p") * Math.Abs(rem3) * mDatabase_margin)
                            drow_span("exposure_margin") += (1 / 3) * (drow1("p") * Math.Abs(set3) * mDatabase_margin)
                        Next
                    Next
                End If

                GoTo a1
            End If
        End While
    End Sub

    ''' <summary>
    ''' get_expected_latest_spn_file
    ''' </summary>
    ''' <param name="current_span_file"></param>
    ''' <returns></returns>
    ''' <remarks>This method call to Pass Latest SPAN file to SPAN software</remarks>
    Private Function get_expected_latest_spn_file(ByVal current_span_file As String)
        If Today < mSPANFile_time1 Or Today > mSPANFile_time6 Then ''previous day last file
            If Today.DayOfWeek() = DayOfWeek.Saturday Then
                exp_latest_spn_file = "nsccl." & Format(DateAdd(DateInterval.Day, -1, Today), "yyyyMMdd") & ".s.spn"
                exp_latest_zip_file = "nsccl." & Format(DateAdd(DateInterval.Day, -1, Today), "yyyyMMdd") & ".s.zip"
            ElseIf Today.DayOfWeek() = DayOfWeek.Sunday Then
                exp_latest_spn_file = "nsccl." & Format(DateAdd(DateInterval.Day, -2, Today), "yyyyMMdd") & ".s.spn"
                exp_latest_zip_file = "nsccl." & Format(DateAdd(DateInterval.Day, -2, Today), "yyyyMMdd") & ".s.zip"
            Else
                exp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".s.spn" 'nsccl.20070302.s_1.spn
                exp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".s.zip"
            End If
        End If

        If Today >= mSPANFile_time1 And Today < mSPANFile_time2 Then
            exp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i01.spn" 'nsccl.20070316.i01_1.spn
            exp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i1.zip"
        ElseIf Today >= mSPANFile_time2 And Today < mSPANFile_time3 Then
            exp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i02.spn"
            exp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i2.zip"
        ElseIf Today >= mSPANFile_time3 And Today < mSPANFile_time4 Then
            exp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i03.spn"
            exp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i3.zip"
        ElseIf Today >= mSPANFile_time4 And Today < mSPANFile_time5 Then
            exp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i04.spn"
            exp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i4.zip"
        ElseIf Today >= mSPANFile_time5 And Today < mSPANFile_time6 Then
            exp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i05.spn"
            exp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i5.zip"
        End If
        'If current_span_file <> exp_latest_spn_file And mDownload_inprogess = False Then
        '    Dim worker As New System.Threading.Thread(AddressOf download_file_from_nse)
        '    worker.Start()
        'End If
    End Function

    ''' <summary>
    ''' execute_batch_file
    ''' </summary>
    ''' <remarks>This method call to Pass Input.txt file to SPAN software and Generate Output.xml file using execute batch file</remarks>
    Private Sub execute_batch_file()
        'System.Diagnostics.Process.Start(mSPAN_path & "\generate.bat", AppWinStyle.Hide)
        Call Shell(mSPAN_path & "\generate.bat", AppWinStyle.Hide)
        'Process.Start(mSPAN_path & "\generate.bat")
        'MsgBox("job_going_on")
        While System.IO.File.Exists(mSPAN_path & "\output.xml") = False
            System.Threading.Thread.Sleep(1000)
        End While
    End Sub

    ''' <summary>
    ''' extract_exposure_margin
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This method call to fill mTbl_SPAN_output datatable from Output.xml file</remarks>
    Private Function extract_exposure_margin() As Boolean
        Dim sr As StreamReader
        Dim drow_output As DataRow
        Dim temp_data As String
        Dim comp_name As String
        Dim exp_date As String
        Dim strike_price As String
        Dim cal_put_fut As String
        Dim fut_comp_name As String
        Try
            mTbl_exposure_comp.Rows.Clear()
            mTbl_SPAN_output.Rows.Clear()
            mTbl_span_calc.Rows.Clear()
            If System.IO.File.Exists(mSPAN_path & "\output.xml") = True Then
                Try
Read_spn_output:
                    sr = New IO.StreamReader(mSPAN_path & "\output.xml")
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

                While True
                    Select Case s.Name
                        Case "phyPf"
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString
                            drow_output = mTbl_exposure_comp.NewRow
                            drow_output("CompName") = s.ReadElementString("pfCode").ToString
                            drow_output("fut_opt") = "OPT"

                            While True
                                s.Read()
                                Select Case s.Name
                                    Case "phy"
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
                        Case "futPf"
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString

                            'got_span_req = False
                            fut_comp_name = s.ReadElementString("pfCode").ToString

                            While True
                                s.Read()

                                Select Case s.Name
                                    Case "fut"
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
                        Case "oopPf"
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
                                                    drow_output = mTbl_SPAN_output.NewRow
                                                    temp_data = s.ReadElementString("firm")
                                                    drow_output("Dealer") = s.ReadElementString("acctId").ToString
                                                    temp_data = s.ReadElementString("acctType")
                                                    temp_data = s.ReadElementString("isCust")
                                                    temp_data = s.ReadElementString("seg")
                                                    temp_data = s.ReadElementString("isNew")
                                                    temp_data = s.ReadElementString("pclient")
                                                    temp_data = s.ReadElementString("currency")
                                                    temp_data = s.ReadElementString("ledgerBal")
                                                    temp_data = s.ReadElementString("ote")
                                                    temp_data = s.ReadElementString("securities")
                                                    drow_output("lfv") = Val(s.ReadElementString("lfv"))
                                                    drow_output("sfv") = Val(s.ReadElementString("sfv"))
                                                    drow_output("lov") = Val(s.ReadElementString("lov"))
                                                    drow_output("sov") = Val(s.ReadElementString("sov"))
                                                Case "spanReq"
                                                    If got_span_req = False Then
                                                        drow_output("spanreq") = Val(s.ReadElementString("spanReq"))
                                                        drow_output("anov") = Val(s.ReadElementString("anov"))
                                                        'If (drow_output("spanreq") - drow_output("anov")) <= 0 Then
                                                        '    drow_output("spanreq") = 0
                                                        '    drow_output("anov") = 0
                                                        'End If
                                                        got_span_req = True
                                                    End If
                                                Case "portfolio"
                                                    drow_output("exposure_margin") = 0
                                                    mTbl_SPAN_output.Rows.Add(drow_output)
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
            If Not IsNothing(sr) Then sr.Close()

            Return True
        Catch ex As Exception
            MsgBox("Exposure_Margin_Dealerwise :: extract_exposure_margin :: " & ex.ToString)
            Return False
        End Try
    End Function

    Private Function concat_scrip(ByVal comp_name As String, ByVal cal_put_fut As String, ByVal exp_date As String, ByVal strike_price As String) As String
        Dim index_name As String
        Dim a_e As String

        If cal_put_fut = "FUT" Then
            If UCase(comp_name) = "NIFTY" Or UCase(comp_name) = "BANKNIFTY" Or UCase(comp_name) = "CNXIT" Or UCase(comp_name) = "MINIFTY" Or UCase(comp_name) = "CNX100" Or UCase(comp_name) = "JUNIOR" Or UCase(comp_name) = "NFTYMCAP50" Then
                index_name = "FUTIDX"
            Else
                index_name = "FUTSTK"
            End If
        Else
            If UCase(comp_name) = "NIFTY" Or UCase(comp_name) = "BANKNIFTY" Or UCase(comp_name) = "CNXIT" Or UCase(comp_name) = "MINIFTY" Or UCase(comp_name) = "CNX100" Or UCase(comp_name) = "JUNIOR" Or UCase(comp_name) = "NFTYMCAP50" Then
                index_name = "OPTIDX"
                a_e = "E"
            Else
                index_name = "OPTSTK"
                a_e = "A"
            End If
        End If

        If cal_put_fut = "FUT" Then
            Return index_name & "  " & comp_name & "  " & exp_date & "  "
        Else
            Return index_name & "  " & comp_name & "  " & exp_date & "  " & strike_price & "  " & UCase(Mid(cal_put_fut, 1, 1)) & UCase(Mid(a_e, 1, 1))
        End If

    End Function

    ''' <summary>
    ''' cal_exp_Margin
    ''' </summary>
    ''' <remarks>This method call to Fill Export Margin datatable and Updaete Margin value into position datatable</remarks>
    Public Sub cal_exp_Margin()
        G_DtPositionwise_ExpMargin.Rows.Clear()
        For Each Row As DataRow In mTbl_SPAN_output.Rows
            If G_DtPositionwise_ExpMargin.Select("prefix='" & Row("Dealer") & "'").Length > 0 Then Continue For

            Dim DRSpan As DataRow = G_DtPositionwise_ExpMargin.NewRow
            DRSpan.BeginEdit()
            DRSpan("prefix") = Row("Dealer")
            REM This block execute to check whether is Dealer Company or Group Company
            Dim StrPrefix As String = DRSpan("prefix")
            Dim Arr() As String = StrPrefix.Substring(3).Split(",")

            'DRSpan("Dealer_ID") = Arr(0)
            DRSpan("Dealer") = Row("company") 'G_DTDealerMaster.Compute("MAX(Dealer)", "Dealer_ID=" & Arr(0) & "")
            
            'DRSpan("Company_ID") = Arr(1)
            DRSpan("Company") = GetSymbol(Row("company")) 'G_DTCompanyMaster.Compute("MAX(Company)", "Company_ID=" & Arr(1) & "")
            REM End
            DRSpan("Exp_Margin") = Val(mTbl_SPAN_output.Compute("SUM(Exposure_Margin)", "Dealer='" & StrPrefix & "'").ToString)
            DRSpan("init_margin") = Val(mTbl_SPAN_output.Compute("SUM(SpanReq)", "Dealer='" & StrPrefix & "'").ToString) - Val(mTbl_SPAN_output.Compute("SUM(anov)", "Dealer='" & StrPrefix & "'").ToString)
            DRSpan("Eqt_Margin") = Val(Gtbl_EqTradedInfo.Compute("SUM(Equity_Margin)", "Dealer='" & StrPrefix & "'  AND cp='E' ").ToString)
            DRSpan("Total_Margin") = Math.Round(Val(DRSpan("Exp_Margin") + DRSpan("Init_Margin") + DRSpan("Eqt_Margin")) / 100000, 2)
            DRSpan.EndEdit()
            G_DtPositionwise_ExpMargin.Rows.Add(DRSpan)
            DRSpan = Nothing
        Next
        Dim intCurrDate As Integer = DateDiff(DateInterval.Second, CDate("01-01-1980"), Now)

        Dim dt_Chart_Margin As DataTable = G_DtPositionwise_ExpMargin.DefaultView.ToTable(True, "dealer_id", "dealer_group_id", "company_id", "Exp_Margin", "init_margin")
        dt_Chart_Margin.Columns.Add("lupdatetime", GetType(Integer))
        For Each Dr As DataRow In dt_Chart_Margin.Rows
            Dr("lupdatetime") = intCurrDate
        Next
        Call ObjTrade.Insert_Chart_Data_Position_Margin(dt_Chart_Margin)
        Call ObjTrade.Insert_Update_Chart_Margin_Last_UpdateTime(intCurrDate)

        'While GVarIsImportRowProcess = True
        '    Threading.Thread.Sleep(500)
        'End While
        'While GVarIsGapOpeningAnaCalculate = True
        '    Threading.Thread.Sleep(500)
        'End While

        'GVarIsMarginCalculate = True
        'REM -------------------------------------------------------------------------
        'REM Update Margin value into Position datatable
        'REM -------------------------------------------------------------------------
        'GVarSTOPDealerScriptCalc = True
        'GVarSTOPDealerCompanyCalc = True
        'GVarSTOPCompanyCalc = True
        'GVarSTOPDealerCalc = True
        'GVarSTOPGroupScriptCalc = True
        'GVarSTOPGroupCompanyCalc = True
        'GVarSTOPDealerGroupCalc = True

        'For cnt As Integer = 0 To 10
        '    If GVarIsCalculateAgain = False Then
        '        Exit For
        '    Else
        '        Threading.Thread.Sleep(2000)
        '    End If
        'Next

        'Dim VarExpMargin As Double = 0
        'Dim VarInitMargin As Double = 0
        'Dim VarCondition As String = ""
        'REM Update margin in Dealer company wise position datatable
        'G_DtPositionwise_ExpMargin.DefaultView.Sort = "dealer_id,company_id"
        'For Each DRDlrComp As DataRow In Gtbl_DealerCompany.Rows
        '    VarCondition = "dealer_id=" & DRDlrComp("dealer_id") & " and company_id=" & DRDlrComp("company_id")
        '    If G_DtPositionwise_ExpMargin.Select(VarCondition).Length > 0 Then
        '        VarExpMargin = Val(G_DtPositionwise_ExpMargin.Compute("sum(Exp_Margin)", VarCondition).ToString)
        '        VarInitMargin = Val(G_DtPositionwise_ExpMargin.Compute("sum(init_margin)", VarCondition).ToString)
        '        If VarInitMargin < 0 Then VarInitMargin = 0
        '        DRDlrComp.BeginEdit()
        '        DRDlrComp("exposuremargin") = (VarExpMargin + VarInitMargin) / 100000
        '        DRDlrComp.EndEdit()
        '    End If
        'Next
        'REM ENd

        'REM Update margin in Company wise position datatable
        'G_DtPositionwise_ExpMargin.DefaultView.Sort = "company_id"
        'For Each DrComp As DataRow In Gtbl_companywiseDet.Rows
        '    VarCondition = "dealer_id > 0 AND company_id=" & DrComp("company_id")
        '    If G_DtPositionwise_ExpMargin.Select(VarCondition).Length > 0 Then
        '        VarExpMargin = Val(G_DtPositionwise_ExpMargin.Compute("sum(Exp_Margin)", VarCondition).ToString)
        '        VarInitMargin = Val(G_DtPositionwise_ExpMargin.Compute("sum(init_margin)", VarCondition).ToString)
        '        If VarInitMargin < 0 Then VarInitMargin = 0
        '        DrComp.BeginEdit()
        '        DrComp("exposuremargin") = (VarExpMargin + VarInitMargin) / 100000
        '        DrComp.EndEdit()
        '    End If
        'Next
        'REM ENd

        'REM Update margin in Dealer wise position datatable
        'G_DtPositionwise_ExpMargin.DefaultView.Sort = "dealer_id"
        'For Each DrDlr As DataRow In Gtbl_DealerwiseDet.Rows
        '    VarCondition = "dealer_id=" & DrDlr("dealer_id")
        '    If G_DtPositionwise_ExpMargin.Select(VarCondition).Length > 0 Then
        '        VarExpMargin = Val(G_DtPositionwise_ExpMargin.Compute("sum(Exp_Margin)", VarCondition).ToString)
        '        VarInitMargin = Val(G_DtPositionwise_ExpMargin.Compute("sum(init_margin)", VarCondition).ToString)
        '        If VarInitMargin < 0 Then VarInitMargin = 0
        '        DrDlr.BeginEdit()
        '        DrDlr("exposuremargin") = (VarExpMargin + VarInitMargin) / 100000
        '        If DrDlr("totalmargin") = 0 Then
        '            DrDlr("exposuremarginper") = 0
        '        Else
        '            DrDlr("exposuremarginper") = (DrDlr("exposuremargin") * 100) / DrDlr("totalmargin")
        '        End If
        '        DrDlr.EndEdit()
        '    End If
        'Next
        'REM ENd

        'REM Update Margin in Groupwise comapny position datatable
        'G_DtPositionwise_ExpMargin.DefaultView.Sort = "dealer_group_id,company_id"
        'For Each DrGroupComp As DataRow In Gtbl_GroupCompany.Rows
        '    VarCondition = "dealer_group_id=" & DrGroupComp("dealer_group_id") & " AND company_id=" & DrGroupComp("company_id")
        '    If G_DtPositionwise_ExpMargin.Select(VarCondition).Length > 0 Then
        '        VarExpMargin = Val(G_DtPositionwise_ExpMargin.Compute("sum(Exp_Margin)", VarCondition).ToString)
        '        VarInitMargin = Val(G_DtPositionwise_ExpMargin.Compute("sum(init_margin)", VarCondition).ToString)
        '        If VarInitMargin < 0 Then VarInitMargin = 0
        '        DrGroupComp.BeginEdit()
        '        DrGroupComp("exposuremargin") = (VarExpMargin + VarInitMargin) / 100000
        '        DrGroupComp.EndEdit()
        '    End If
        'Next
        'REM ENd

        'REM Update margin in Group wise Position datatable
        'For Each DrGroup As DataRow In Gtbl_GroupwiseDet.Rows
        '    VarCondition = "dealer_group_id=" & DrGroup("dealer_group_id")
        '    If G_DtPositionwise_ExpMargin.Select(VarCondition).Length > 0 Then
        '        VarExpMargin = Val(G_DtPositionwise_ExpMargin.Compute("sum(Exp_Margin)", VarCondition).ToString)
        '        VarInitMargin = Val(G_DtPositionwise_ExpMargin.Compute("sum(init_margin)", VarCondition).ToString)
        '        If VarInitMargin < 0 Then VarInitMargin = 0
        '        DrGroup.BeginEdit()
        '        DrGroup("exposuremargin") = (VarExpMargin + VarInitMargin) / 100000
        '        If DrGroup("totalmargin") = 0 Then
        '            DrGroup("exposuremarginper") = 0
        '        Else
        '            DrGroup("exposuremarginper") = (DrGroup("exposuremargin") * 100) / DrGroup("totalmargin")
        '        End If
        '        DrGroup.EndEdit()
        '    End If
        'Next
        'REM ENd


        'GVarSTOPDealerScriptCalc = False
        'GVarSTOPDealerCompanyCalc = False
        'GVarSTOPCompanyCalc = False
        'GVarSTOPDealerCalc = False
        'GVarSTOPGroupScriptCalc = False
        'GVarSTOPGroupCompanyCalc = False
        'GVarSTOPDealerGroupCalc = False

        REM -------------------------------------------------------------------------
        REM In Position Table Margin update finished
        REM -------------------------------------------------------------------------
        GVarIsMarginCalculate = False
    End Sub

#End Region

End Module
