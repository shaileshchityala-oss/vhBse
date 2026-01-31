Imports System.IO
Imports System.Data.OleDb
Imports VolHedge.OptionG
Imports VolHedge.DAL

Public Class clsGlobal
    REM Database Password
    Public Const glbAcessPassWord As String = "FintEstpwD"
    Public Shared Expire_Date As Date
    Public Shared FlagTCP As Integer
    Public Shared PreOTP As String
    Public Shared RagisterOTP As Boolean = False
    Public Shared RagisterFlag As Boolean = False
    Public Shared InternetVersionFlag As Boolean = False
    Public Shared Mrateofinterast As Double = 0

    Public Shared H_PendingTokens_EQ As New Hashtable()
    Public Shared H_PendingTokens_FO As New Hashtable()
    Public Shared H_All_token_EQ As New Hashtable()
    Public Shared H_All_token_FO As New Hashtable()

    Public Shared EmailVerified As Boolean = False
    Public Shared PhVerified As Boolean = False
    Public Shared LoginUser As String
    Public Shared Password As String
    Public Shared UserName1 As String
    Public Shared LoginId As String

    Public Shared ForceShutdown As Boolean = False
    Public Shared smtpUser As String
    Public Shared smtpPasword As String
    Public Shared mSettingObj As CSetting
    Public Shared mNetworkMode As New CNetworkMode
    Public Shared mDownloadMgr As New CDownloadManager

    Public Shared mContract As New CContract
    Public Shared mPerf As New CPerfCheck
    Public Shared mBseExchange As CBseExchange
    Public Shared mNseSr As CSpanReader
    Public Shared mBseSr As CSpanReader
    Public Shared mFrmExchangeMargin As FrmMarginBse


    Public Shared Function SetExpDate(ByVal Exdate As Date) As Date
        Expire_Date = Exdate
        Return Expire_Date
    End Function

    ''' <summary>
    ''' Sub_Get_Version_TextFile
    ''' </summary>
    ''' <remarks>This method call to create Text file which has information About VolHedge in application directory</remarks>
    Public Shared Sub Sub_Get_Version_TextFile()
        Dim FSVersionDetail As System.IO.StreamWriter
        FSVersionDetail = New IO.StreamWriter(Application.StartupPath & "\VolHedge Version Detail.txt", False)
        Try
            GVar_Version_Title = MDI.Text.Trim
            FSVersionDetail.WriteLine("Version : " & GVar_Version_Title.Trim)
            FSVersionDetail.WriteLine("MX      : " & Format(GVar_Master_Expiry, "ddMMyyyy").Trim)
            FSVersionDetail.WriteLine("LX      : " & Format(clsGlobal.Expire_Date, "ddMMyyyy").Trim)
            Dim VarDay As Integer = DateDiff(DateInterval.Day, Today, IIf(clsGlobal.Expire_Date > GVar_Master_Expiry, GVar_Master_Expiry, clsGlobal.Expire_Date))
            If VarDay >= 0 Then
                FSVersionDetail.WriteLine("DX      : " & VarDay)
            Else
                FSVersionDetail.WriteLine("DX      : 0")
            End If

            FSVersionDetail.Close()
        Catch ex As Exception
            MessageBox.Show("" & GVar_Version_Title.Trim & "" & Format(GVar_Master_Expiry, "ddMMyyyy").Trim & " " & Format(clsGlobal.Expire_Date, "ddMMyyyy").Trim & ex.ToString())
            FSVersionDetail.Close()
        End Try
    End Sub

    Public Shared Sub LoadInitializeData()

        Dim ttik As Int64 = DateTime.Now.Ticks
        Write_TimeLog1("LoadInitializeData Start..")
        Dim ObjIO As ImportData.ImportOperation
        Dim Objbhavcopy As New bhav_copy
        Dim objTrad As New trading

        mSettingObj = New CSetting(GdtSettings)
        'mSettingObj.mDtSettings = GdtSettings
        'mSettingObj.mDtRef.Value = GdtSettings

        If INSTANCEname = "PRIMARY" Then
            CDataPath = "C:\Data\"

            outputxml = "output.xml"
            curoutputxml = "curoutput.xml"
            spanspn = "span.spn"
            curspanspn = "curspan.spn"
            inputtxt = "input.txt"
            curinputtxt = "curinput.txt"
            generatebat = "generate.bat"
            curgeneratebat = "curgenerate.bat"

        ElseIf INSTANCEname = "SECONDARY" Then
            CDataPath = "C:\Data2\"
            outputxml = "output2.xml"
            curoutputxml = "curoutput2.xml"
            spanspn = "span2.spn"
            curspanspn = "curspan2.spn"
            inputtxt = "input2.txt"
            curinputtxt = "curinput2.txt"
            generatebat = "generate2.bat"
            curgeneratebat = "curgenerate2.bat"
        ElseIf INSTANCEname = "THIRD" Then
            CDataPath = "C:\Data3\"
            outputxml = "output3.xml"
            curoutputxml = "curoutput3.xml"
            spanspn = "span3.spn"
            curspanspn = "curspan3.spn"
            inputtxt = "input3.txt"
            curinputtxt = "curinput3.txt"
            generatebat = "generate3.bat"
            curgeneratebat = "curgenerate3.bat"
        ElseIf INSTANCEname = "FOURTH" Then
            CDataPath = "C:\Data4\"
            outputxml = "output4.xml"
            curoutputxml = "curoutput4.xml"
            spanspn = "span4.spn"
            curspanspn = "curspan4.spn"
            inputtxt = "input4.txt"
            curinputtxt = "curinput4.txt"
            generatebat = "generate4.bat"
            curgeneratebat = "curgenerate4.bat"

        ElseIf INSTANCEname = "FIFTH" Then
            CDataPath = "C:\Data5\"
            outputxml = "output5.xml"
            curoutputxml = "curoutput5.xml"
            spanspn = "span5.spn"
            curspanspn = "curspan5.spn"
            inputtxt = "input5.txt"
            curinputtxt = "curinput5.txt"
            generatebat = "generate5.bat"
            curgeneratebat = "curgenerate5.bat"
            '
        ElseIf INSTANCEname = "SIXTH" Then
            CDataPath = "C:\Data6\"
            outputxml = "output6.xml"
            curoutputxml = "curoutput6.xml"
            spanspn = "span6.spn"
            curspanspn = "curspan6.spn"
            inputtxt = "input6.txt"
            curinputtxt = "curinput6.txt"
            generatebat = "generate6.bat"
            curgeneratebat = "curgenerate6.bat"
            '
        ElseIf INSTANCEname = "SEVENTH" Then
            CDataPath = "C:\Data7\"
            outputxml = "output7.xml"
            curoutputxml = "curoutput7.xml"
            spanspn = "span7.spn"
            curspanspn = "curspan7.spn"
            inputtxt = "input7.txt"
            curinputtxt = "curinput7.txt"
            generatebat = "generate7.bat"
            curgeneratebat = "curgenerate7.bat"
        ElseIf INSTANCEname = "EIGHT" Then
            CDataPath = "C:\Data8\"
            outputxml = "output8.xml"
            curoutputxml = "curoutput8.xml"
            spanspn = "span8.spn"
            curspanspn = "curspan8.spn"
            inputtxt = "input8.txt"
            curinputtxt = "curinput8.txt"
            generatebat = "generate8.bat"
            curgeneratebat = "curgenerate8.bat"
        End If
        GdtBhavcopy = New DataTable
        GdtBhavcopy = Objbhavcopy.select_data()
        If GdtBhavcopy.Rows.Count > 0 Then
            GVarIsNewBhavcopy = True
        End If

        GdtCompanyAnalysis = New DataTable
        GdtCompanyAnalysis = objTrad.select_company_ana()

        'ThrdLoadBhavCopy.Priority = ThreadPriority.Lowest
        'ThrdLoadBhavCopy.Start()

        Call Init_G_DTExpenseData() REM this method call to init. Expense table Alpesh 10/05/2011   
        'Call Rounddata() 'Commented By Viral
        '11july2017
        'If NetMode = "TCP" Then 'Rem For Get Tcp Connection Data ' Commented By Viral (29-June-2017)
        '    Call AuthTcp(sSQLSERVER, sDATABASE, sUSERNAME, sPASSWORD, gvarInstanceCode, G_VarExpiryDate)
        'End If

        If NetMode = "JL" Or NetMode = "TCP" Then 'Rem For Get Tcp Connection Data ' Commented By Viral (29-June-2017)
            Call AuthTcp(sSQLSERVER, sDATABASE, sUSERNAME, sPASSWORD, gvarInstanceCode, G_VarExpiryDate)
        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then 'Rem For Get Tcp Connection Data ' Commented By Viral (29-June-2017)
            Call AuthTcp(sSQLSERVER, sDATABASE, sUSERNAME, sPASSWORD, gvarInstanceCode, G_VarExpiryDate)
        End If
        'If flgAPI_K = "TRUEDATA" And NetMode = "API" Then
        '    gVarInstanceID = "C-" & G_GetMACAddress
        '    gvarInstanceCode = gVarInstanceID & "|" & My.Computer.Name & "|" & My.User.Name
        'End If
        REM by Viral 11-07-11 For Auto Fill Contracts
        Dim StrFile As String = ""


        If import_Data.AutoFillBhavCopy(StrFile) = True Then
            Dim bhav1 As New bhavcopyprocess
            'bhav1.removebhavcopy()
            read_bhavcopyfile(StrFile)
        End If

        StrFile = ""
        If import_Data.AutoFillCont(StrFile) = True Then
            If CONTRACT_NOTIFICATION = 1 Then

                ObjIO = New ImportData.ImportOperation(1)
                import_Data.ContractImport(StrFile, ObjIO, True)
            End If
        End If
        StrFile = ""
        If import_Data.AutoFillSec(StrFile) = True Then
            If CONTRACT_NOTIFICATION = 1 Then


                ObjIO = New ImportData.ImportOperation(1)
                import_Data.SecurityImport(StrFile, ObjIO, True)
            End If
        End If
        StrFile = ""
        If import_Data.AutoFillCurr(StrFile) = True Then
            If CONTRACT_NOTIFICATION = 1 Then

                ObjIO = New ImportData.ImportOperation(1)
                import_Data.CurrencyImport(StrFile, ObjIO, True)
            End If
        End If

        REM End
        ObjIO = Nothing

        Call fill_token() 'Viral: Why it should Commented?(29-June-2017))

        Call init_datatable() 'initaialize all global datatable of analysis
        Call fill_trades() REM Fill Trade into Datatable
        Fill_HT_RefreshTrde()
        'aa:
        '        If filltokenflg = False Then   'Viral: it Will Gone To DeadEnd (29-June-2017))
        Rounddata()
        Call fill_equity_dtable()
        'Else '
        'GoTo aa
        'End If

        smtpUser = "Software@finideas.com"
        smtpPasword = "uvfygwnphadomcoq"

        Write_TimeLog1("ClsGlobal-> End Fun-LoadInitializeData" + "|" + TimeSpan.FromTicks(DateTime.Now.Ticks - ttik).TotalSeconds.ToString())
    End Sub

    Public Shared Sub CreateBseExchange()
        mBseExchange = New CBseExchange()
    End Sub




   Public Shared Sub SkAddTokkensFromCurTable(currtable As DataTable)
        clsGlobal.H_All_token_FO.Clear()
        clsGlobal.H_All_token_EQ.Clear()

        For Each row As DataRow In currtable.Rows
            Dim tok As String = row("tokanno").ToString()
            Dim cp As String = row("CP").ToString()
            Dim ftoken As String = row("ftoken").ToString()

            ' Add only if not already in Hashtable
            If cp = "C" Or cp = "P" Or cp = "F" Then
                If Not clsGlobal.H_All_token_FO.ContainsKey(CLng(tok)) Then
                    clsGlobal.H_All_token_FO.Add(CLng(tok), 0)
                End If

                If Not clsGlobal.H_All_token_FO.ContainsKey(CLng(ftoken)) Then
                    clsGlobal.H_All_token_FO.Add(CLng(ftoken), 0)
                End If

            ElseIf cp = "E" Then
                If Not clsGlobal.H_All_token_EQ.ContainsKey(CLng(tok)) Then
                    clsGlobal.H_All_token_EQ.Add(CLng(tok), 0)
                End If
            End If
        Next
    End Sub

    Public Shared Sub read_bhavcopyfile(ByVal strPath1 As String)
        Dim tempdata As DataTable
        Dim objTrad As trading = New trading
        Dim DtBCP As DataTable
        Dim objBh As New bhav_copy
        REM Change In processing Bhavcopy and selecting parameters, Decimal Strike Prices are properly displayed for the records 
        Try
            Dim fpath As String
            fpath = strPath1.Trim
            Mrateofinterast = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
            If fpath <> "" Then
                Dim fi As New FileInfo(fpath)
                Dim dv2 As DataView
                tempdata = New DataTable
                Try

                    Dim fName As String = ""
                    '  Dim fi As New FileInfo(fpath)
                    Dim strdata As [String]()
                    strdata = fpath.Split(New Char() {"\"c})
                    'strdata = dr("script").Split("  ", StringSplitOptions.None)


                    Dim filename As String = strdata(strdata.Length - 1)
                    Dim sConnectionStringz As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
                    Dim objConn As New OleDbConnection(sConnectionStringz)
                    objConn.Open()
                    Dim objCmdSelect As New OleDbCommand("SELECT * FROM [" & filename & "] where option_typ='xx'", objConn)
                    'Dim objCmdSelect As New OleDbCommand("SELECT strike,option_type,Exp. Date,Company,Qty,Rate,Instrument,entrydate FROM " & fi.Name, objConn)
                    Dim objAdapter1 As New OleDbDataAdapter
                    objAdapter1.SelectCommand = objCmdSelect
                    objAdapter1.Fill(tempdata)
                    objConn.Close()
                    'fi.Delete()
                Catch ex As Exception
                    MsgBox("Bhavcopy Not Processed.", MsgBoxStyle.Information)
                    Exit Sub
                End Try
                Dim dv As DataView
                tempdata = New DataTable
                DtBCP = New DataTable
                tempdata.Columns.Add("script")
                tempdata.Columns.Add("vol")
                tempdata.Columns.Add("futval")
                tempdata.Columns.Add("mt")
                tempdata.Columns.Add("iscall")
                tempdata.AcceptChanges()

                'Call Proc_Data_FromBhavCopyCsv(fpath)
                Dim impBHav As ImportData.ImportOperation
                impBHav = New ImportData.ImportOperation
                If NEW_BHAVCOPY = 1 Then
                    import_Data.CopyToData(fpath, "BHAVCOPYNEW")
                Else
                    import_Data.CopyToData(fpath, "BHAVCOPY")
                End If

                Call impBHav.ImportBhavCopy()

                impBHav = Nothing
                DtBCP = objBh.select_TblBhavCopy()

                tempdata.Merge(DtBCP)

                Dim mt As Double
                Dim futval As Double
                Dim iscall As Boolean
                Dim drow As DataRow
                'objAdapter1.Fill(tempdata)
                'objConn.Close()

                'dv = New DataView(tempdata, "OPTION_TYP='XX'", "", DataViewRowState.OriginalRows)
                dv = New DataView(tempdata, "option_typ='XX'", "", DataViewRowState.CurrentRows)
                'dv = New DataView(Dt, "OPTION_TYP='XX'", "", DataViewRowState.OriginalRows)
                Dim str(2) As String
                str(0) = "EXPIRY_DT"
                str(1) = "SETTLE_PR"
                str(2) = "SYMBOL"
                Dim tdata As New DataTable
                tdata = dv.ToTable(True, str)
                Dim row As DataRow
                Dim script As String
                For Each drow In tempdata.Rows
                    If drow("option_typ") = "XX" Then
                        script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy")
                        drow("script") = UCase(script.Trim)
                        drow("vol") = 0
                        drow("futval") = 0
                        drow("mt") = 0
                    Else
                        script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy") & "  " & Format(Val(drow("STRIKE_PR")), "###0.00") & "  " & drow("OPTION_TYP")
                        'script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(drow("EXPIRY_DT"), "ddMMMyyyy") & "  " & Format(Val(drow("STRIKE_PR")), "####.##") & "  " & drow("OPTION_TYP")
                        drow("script") = UCase(script.Trim)
                        futval = 0
                        drow("vol") = 0
                        For Each row In tdata.Select(" EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' ")
                            futval = row("SETTLE_PR")
                        Next
                        'futval = Val(tempdata.Compute("Max(SETTLE_PR)", " EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' And option_typ='XX'").ToString() & "")
                        'row("SETTLE_PR")

                        If Mid(drow("OPTION_TYP"), 1, 1) = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        Dim ccdate As Date = CDate(drow("TIMESTAMP").ToString.Replace("-", "/"))
                        mt = UDDateDiff(DateInterval.Day, ccdate.Date, CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")).Date)
                        If ccdate.Date = CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")).Date Then
                            mt = 0.5
                        End If
                        If mt = 0 Then
                            mt = 0.0001
                        Else
                            mt = (mt) / 365
                        End If
                        If futval > 0 Then
                            drow("vol") = Vol(futval, Val(drow("STRIKE_PR")), Val(drow("SETTLE_PR")), mt, iscall, True) * 100
                        End If
                        drow("futval") = futval
                        drow("mt") = mt
                        drow("iscall") = iscall
                    End If
                Next
                tempdata.AcceptChanges()
                objBh.insertNew(tempdata)

                GdtBhavcopy = objBh.select_data()
                GVarIsNewBhavcopy = True
                BhavCopyFlag = True

                Dim Item As DictionaryEntry
                Dim ArrFKey As New ArrayList
                Dim ArrCPKey As New ArrayList
                Dim ArrEKey As New ArrayList
                Dim VaLLTPPrice As New Double
                For Each Item In fltpprice
                    ArrFKey.Add(Item.Key)
                Next
                For Each Item In ltpprice
                    ArrCPKey.Add(Item.Key)
                Next
                For Each Item In eltpprice
                    ArrEKey.Add(Item.Key)
                Next
                For i As Integer = 0 To ArrFKey.Count - 1
                    If cpfmaster.Select("token=" & ArrFKey(i) & "").Length > 0 Then
                        Dim VarScript As String = cpfmaster.Select("token=" & ArrFKey(i) & "")(0).Item("Script")
                        If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                            fltpprice(ArrFKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        End If
                    End If
                Next
                For i As Integer = 0 To ArrCPKey.Count - 1
                    If cpfmaster.Select("token=" & ArrCPKey(i) & "").Length > 0 Then
                        Dim VarScript As String = cpfmaster.Select("token=" & ArrCPKey(i) & "")(0).Item("Script")
                        If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                            fltpprice(ArrCPKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        End If
                    End If
                Next
                For i As Integer = 0 To ArrEKey.Count - 1
                    If eqmaster.Select("token=" & ArrEKey(i) & "").Length > 0 Then
                        Dim VarScript As String = eqmaster.Select("token=" & ArrEKey(i) & "")(0).Item("Script")
                        If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                            fltpprice(ArrEKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        End If
                    End If
                Next

                If analysis.chkanalysis = True Then

                    Call analysis.AssignBhavcopyLTP(True)
                End If

                'MsgBox("Bhavcopy Processed Successfully.", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            deletebhavcopy()
            MsgBox("Bhavcopy Not Processed.", MsgBoxStyle.Information)

        End Try
    End Sub

    Public Shared Function deletebhavcopy()
        Dim qry As String
        Try

            qry = "Delete From TblBhavcopy"
            data_access.ParamClear()
            data_access.Cmd_Text = qry
            data_access.cmd_type = CommandType.Text
            data_access.ExecuteNonQuery()
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
        End Try
    End Function

    Public Shared Function Vol(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal _mt As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean) As Double
        Dim tmpcpprice As Double = 0
        Dim mVolatility As Double
        tmpcpprice = cpprice
        'IF MATURITY IS 0 THEN _MT = 0.00001
        mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
        Return mVolatility
    End Function

    Public Shared Function SetEmailVerified(ByVal verified As String) As String

        EmailVerified = verified
        Return EmailVerified
    End Function
    Public Shared Function SetPhVerified(ByVal verified As String) As String

        PhVerified = verified
        Return PhVerified
    End Function


    Public Shared Function SetLoginUser(ByVal LoginUser1 As String) As String
        LoginUser = LoginUser1
        Return LoginUser
    End Function
    Public Shared Function SetLoginId(ByVal UniqueId As String) As String
        LoginId = UniqueId
        Return LoginId
    End Function
    Public Shared Function SetUsername(ByVal Username As String) As String
        UserName1 = Username
        Return UserName1
    End Function
    Public Shared Function SetPassword(ByVal Password1 As String) As String
        Password = Password1
        Return Password
    End Function

    Public Shared Sub AddFoTokenToHt(pToken As Long)
        If Not H_All_token_FO.Contains(pToken) Then
            H_All_token_FO.Add(pToken, 0)
        End If
    End Sub

End Class


