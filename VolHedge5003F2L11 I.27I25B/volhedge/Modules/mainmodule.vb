'Imports Microsoft.Office.Interop
Imports System.Management
Imports System.Net
Imports System.DirectoryServices
Imports System.Net.Sockets
Imports System.Net.Sockets.Socket
Imports System.IO
Imports System.Threading
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Net.Mail
Imports VolHedge.DAL
Imports System.Xml
Imports VolHedge.OptionG
Imports System.Net.NetworkInformation
Imports System.Runtime.InteropServices
Module mainmodule
    Public flgFillTCPConnectionToSqlTOAcess As Boolean = False
    Public dtAccess As DataTable
    Public verVersion As String = ""
    Public flg_TabChanging As Boolean = False
    Public Objsql As SqlDbProcess = New SqlDbProcess
    Public hashsyn As New Hashtable
    Dim trd As New trading
    Dim frmsetting As New frmSettings
    Public filltokenflg As Boolean = False
    Public CFnprofit As Double
    Public CFgprofit As Double
    Public CFexpense As Double
    Public TCP_CON_NAME As String
    Public FLG_REG_SERVER_CONN As Boolean = False
    Public internerRefreshtime As String = ""
    Public SpanTimerCheck As Integer
    Public EXPORT_IMPORT_POSITION As Integer
    Public CAL_USING_EQ_WITHINDEX As Boolean
    Public DAYTIME_VOLANDGREEK_CAL As Integer
    Public INCLUDEEXPIRY_VOLANDGREEK_CAL As Integer
    Public FIXVOL_BACKCOLOR As String

    Public GAMMAMULTI_BACKCOLOR As String

    Public FLGCSVCONTRACT As Boolean = False

    Public flgApiConn As String = False

    Public dtsyncpf As DataTable

    Public CDataPath As String

    Public custFormateName As String

    Public LastPatchUpdate As String
    Public patchUpdate As String

    Public outputxml As String
    Public curoutputxml As String
    Public spanspn As String
    Public curspanspn As String
    Public inputtxt As String
    Public curinputtxt As String
    Public generatebat As String
    Public curgeneratebat As String

    Public INSTANCEname As String
    Public SYNTH_ATM_UPDOWNSTRIKE As Integer
    '1 = ATM Strike
    '0 = On;yUpdown
    '2 = On;yUpdown With Skeep nifty

    Public BEPfinalSpot1 As String
    Public BEPfinalSpot2 As String
    Public BEPfinalSpot3 As String
    Public BEPfinalSpot4 As String

    'Public strIndex As String = ""

    Public GMTM_BACKCOLOR As String
    Public NMTM_BACKCOLOR As String

    Public DB_BACKUP_ON_EXIT As Integer
    Public OPEN_ANALYSIS As Integer
    Public POSITION_BACKUP_ON_EXIT As Integer
    Public DEFAULT_EXPO_MARGIN As Integer
    Public CHANGE_IN_OI As Integer
    Public OPPOS_ENTRYDATE As Integer
    Public GVarMAXFOTradingOrderNo As Long
    Public GVarMAXEQTradingOrderNo As Long
    Public GVarMAXCURRTradingOrderNo As Long
    Public RECALCULATE_POSITION As Integer
    Public BEP_INTERVAL As Integer
    Public MULTIPLIER As Integer
    Public BEP_STRIKE As Integer
    Public BHAVCOPYPROCESSDAY As Integer
    Public SET_BHAVCOPY_ON_LTP_STOP As Integer
    Public BEP_GMTM As Integer
    Public BEP_VISIBLE As Integer
    Public BEP_STIKEDIFF As Integer
    Public BEP_MESSAGE As String

    Public CALMARGINWITH_AEL_EXPO As Integer

    Public varnoofday As String

    Public BEP_Process As Integer


    Public CAL_GREEK_WITH_BCASTDATE As Integer
    Public IsScriptMapper As Boolean = False
    Public dtanalysisData As New DataTable
    Public ISIMPORTPOSITION As Integer
    Public G_DTImportSetting As DataTable
    Public FlgCFBalance As Boolean = False
    Public FlgFixvolgreekComp As Boolean = False

    Public MaxLotSize As Double = 0
    Public flgprocesssummary As Boolean = False

    Public Scenario_synFut As Boolean = False
    Public Scenario_synFutCurr As Double = 0
    Public Scenario_synFutNext As Double = 0
    Public Scenario_synFutFar As Double = 0

    Public MW_CURRENT As Boolean = True
    Public MW_NEXT As Boolean = False
    Public MW_FAR As Boolean = False
    Public MW_COLOR_FORMAT As Boolean = False


    Public mmDTCFBalance As DataTable
    Public HT_RefreshTrde As New Hashtable
    Public RefreshAll As Boolean = False
    Public ThrdLoadBhavCopy As Thread = New Thread(New ThreadStart(AddressOf LoadBhavCopy))
    Public RefreshsummaryScenario As Boolean = False
    Public RefreshsummaryExpirywise As Boolean
    Public Refreshsummary As Boolean
    Public RefreshsummaryFixVol As Boolean
    Public GREEK_NEUTRAL As Integer
    Public flgimportContract As Boolean = False
    Public flgimportBhavCopy As Boolean = False

    Public flgTcpfillconn As Boolean = False

    Public flgFoTCPBcast As Boolean = False
    Public flgidxTCPBcast As Boolean = False
    Public flgEQTCPBcast As Boolean = False
    Public flgCurrTCPBcast As Boolean = False

    Public FlgBcastStop As Boolean = False
    Public sceallcompanyflag As Boolean = False

    Public SAVE_DATA_AUTO As Integer
    Public AUTO_BHAVCOPY_REFRESH As Integer
    Public WAVV_SETTING As Integer
    'Public NEW_CM_BROADCAST As Integer
    Public NEW_CM_BROADCAST_MT As Integer
    Public CAL_SYN_ON_EXPIRY As Integer

    Public TRADINGVOL_SETTING As Integer
    Public NEW_BHAVCOPY As Integer

    Public G_DTExpenseData_summary As DataTable

    Public dt_REG_Server As DataTable

    Public SAVE_ANA_DATA_FILE As Integer
    Public ht_PlusMinuseLTPVol As New Hashtable


#Region "Variable"
    Public mode As String
    Public objanalysis As Object 'for anaylsis activate or not
    Public DtAnalysis As DataTable 'For Store Analysis table
    Public scenariotable As New DataTable
    Public BEPtable As New DataTable
    Public BEPtableNEG As New DataTable

    'Public ht_tcp_token_list As New Hashtable
    Public alertmsg As Boolean = False
    Public flgupdatetradevol As Boolean = False

    Public G_GetMACAddress As String
    Public gVarInstanceID As String
    Public gvarInstanceCode As String

    Public futtoken As New ArrayList
    Public eqtoken As New ArrayList
    Public Currfuttoken As New ArrayList
    Public cpfmaster As New DataTable
    Public eqmaster As New DataTable
    Public Currencymaster As New DataTable

    Public HTfoNextStrilke As Hashtable = New Hashtable()
    Public HTfoPrevStrilke As Hashtable = New Hashtable()
    Public HtableCurSymbol As Hashtable = New Hashtable()

    Public tmpCpfmaster As New DataTable

    Public GRID_BACKCOLOR As String
    Public FONT_COLOR As String
    Public GRID_FONTSIZE As Double
    Public GRID_FONTSTYLE As String
    Public GRID_FONTTYPE As String

    Dim objTrad As trading = New trading
    Dim objBhavcopy As bhav_copy = New bhav_copy

    Public LTP_PLUS_GAP As Double
    Public LTP_MINUS_GAP As Double
    Public VOL_PLUS_GAP As Double
    Public VOL_MINUS_GAP As Double

    Public Rateofinterest As Double
    Public CALMARGINSPAN As Integer
    Public IsExclude As Integer
    Public AELOPTION As Integer
    Public INDEX_FAR_MONTH_OPTION As Double
    Public INDEX_OTM_OPTION As Double
    Public INDEX_OTH_OPTION As Double
    'Public CALMARGINWITHOLDMETHOD As Integer
    Public zero_qty_analysis As Integer
    Public NoofDay As Integer
    Public SPAN_PATH As String
    Public TIMER_CALCULATION_INTERVAL As Double
    Public REFRESH_TIME As Double
    Public SPAN_TIMER As Double

    Public roundGamma As Integer
    Public roundTheta As Integer
    Public roundDelta As Integer
    Public roundVega As Integer
    Public roundVolga As Integer
    Public roundVanna As Integer
    Public roundGamma_Val As Integer
    Public roundTheta_Val As Integer
    Public roundDelta_Val As Integer
    Public roundVega_Val As Integer
    Public roundVolga_Val As Integer
    Public roundVanna_Val As Integer
    Public RoundGrossMTM As Integer
    Public RoundExpense As Integer
    Public RoundInterest As Integer
    Public RoundNetMTM As Integer
    Public RoundSquareMTM As Integer
    Public Roundinmarg As Integer
    Public Roundexmarg As Integer
    Public Roundrealmarg As Integer
    Public Roundunmarg As Integer
    Public Roundrealtot As Integer
    Public Roundequity As Integer

    Public fifo_avg As String
    Public Deltastr As String
    Public Gammastr As String
    Public Thetastr As String
    Public Vegastr As String
    Public Volgastr As String
    Public Vannastr As String
    Public Gammastr_Val As String
    Public Thetastr_Val As String
    Public Deltastr_Val As String
    Public Vegastr_Val As String
    Public Volgastr_Val As String
    Public Vannastr_Val As String
    Public GrossMTMstr As String
    Public Expensestr As String
    Public Intereststr As String
    Public NetMTMstr As String
    Public SquareMTMstr As String
    Public inmargstr As String
    Public exmargstr As String
    Public realmargstr As String
    Public unmargstr As String
    Public realtotstr As String
    Public equitystr As String
    Public addanovmargin As Boolean

    Public RoundCurrencyLTP As Integer
    Public RoundCurrencyNetPrice As Integer
    Public RoundCurrencyExpense As Integer
    Public RoundCurrencyStrikes As String
    Public CurrencyLTPStr As String
    Public CurrencyNetPriceStr As String
    Public CurrencyExpenseStr As String
    Public CurrencyStrikesStr As String
    Public CurrencyRateOfInterest As Double
    Public RoundNeutralize As Integer
    Public NeutralizeStr As String


    ''''''''''''Devang start'''''''''''''
    Public RoundOpenInt As Integer
    Public RoundCalender As Integer
    Public RoundBF As Integer
    Public RoundVol As Integer
    Public RoundBF2 As Integer
    Public RoundPCP As Integer
    Public RoundRatio As Integer
    Public RoundStraddle As Integer
    Public RoundVolume As Integer
    Public Roundltp As Integer
    Public RoundBullSpread As Integer
    Public RoundBearSpread As Integer
    Public RoundC2C As Integer
    Public RoundC2P As Integer
    Public RoundP2P As Integer
    ''''''''''''Devang end'''''''''''''

    Public Maturity_Far_month As Date
    Public GVarMaturity_Far_month As String
    Public GNEUTRALIZE_MONTH As String
    Public INTERNETDATA_LINK As String
    ''Import File Setting Global Variable
    Public GVar_DealerCode As String
    Public UDP_SELECTED_TOKEN As Integer

    Public HIDEVOLGA As Integer
    Public HIDEVanna As Integer

    Public COMPACTEDDATE As Date
    Public DayTOEXP As Integer

    Public projmtom1 As String
    Public projmtom2 As String
    Public projmtom3 As String
    Public projmtom4 As String
    Public projmtom5 As String
    Public projmtom6 As String
    Public projmtom7 As String
    Public projmtom8 As String

    'Last imported position
    Public LastOpenPosition As String
    Public client_name As String
    Public client_mobile As String

    'Last Imported Bhavcopy Date
    Public LastBhavcopyDate As Date
    Public GVarIsNewBhavcopy As Boolean = False
    Public BhavCopyFlag As Boolean = False
    Public SaveAnaDataFlag As Boolean = False

    REM Net Mode UDP  OR TCP
    Public NetMode As String
    Public originalpath As Integer
    Public ALLCOMPANYLOCATION As String
    Public ALLCOMPANYSIZE As String
    Public SCE_DAYS As Int16
    Public OPEN_EXCEL As Int16
    Public CONTRACT_NOTIFICATION As Int16
    Public NetDataMode As String
    Public ifNetModeTcp As Boolean = False

    Public AnalysisSortedColumn As String
    Public MarketwatchSaveProfile As String


    Public sSQLSERVER As String
    Public sDATABASE As String
    Public sAUTHANTICATION As String
    Public sUSERNAME As String
    Public sPASSWORD As String

    Public JL_sSQLSERVER As String
    Public JL_sDATABASE As String
    Public JL_sAUTHANTICATION As String
    Public JL_sUSERNAME As String
    Public JL_sPASSWORD As String

    Public iCDelay As Integer
    Public iNDelay As Integer
    Public iFDelay As Integer
    Public CURRENCY_MARGIN_QTY As Integer

    Public dCDate As String
    Public dNDate As String
    Public dFDate As String

    Public Analysisfilepath As String
    Public HT_FOContrct As New Hashtable
    Public HT_CURRContrct As New Hashtable
    Public HT_AssetToken As New Hashtable
    Public HT_EQContrct As New Hashtable
    Public HT_FOLotContrct As New Hashtable
    Public HT_CurrNextFar As New Hashtable
    Public HT_strikediff As New Hashtable
    Public HT_FOTOKCEX As New Hashtable
    Public Struct_LTPVol As New Struct_PlusMinuseLTPVol
    Public HT_CurrNextFarcurr As New Hashtable

    Public BhavCopyfilepath, BhavCopyfileName As String


    Public HT_CurrLotContrct As New Hashtable
    Public HT_symboltoken As New Hashtable
    Public HT_TokenSymbol As New Dictionary(Of Long, String)

    'Public HT_eqsymboltoken As New Hashtable
    'Public HT_currsymboltoken As New Hashtable
    'Public chkfindMkt As Boolean
    'Public chkfindanalysis As Boolean
    Public strcompmkt As String
    Public Delegate Sub GDelegate_Index(ByVal obj As String)
    Public obj_DelIndex As New GDelegate_Index(AddressOf GetIndexData)
    Public Structure Struct_PlusMinuseLTPVol
        Dim VarLTP1 As Double
        Dim VarLTP2 As Double
        Dim VarVol1 As Double
        Dim VarVol2 As Double

        Dim VarLTP3 As Double
        Dim VarLTP4 As Double
        Dim VarVol3 As Double
        Dim VarVol4 As Double

        Dim VarLTP5 As Double
        Dim VarLTP6 As Double

        Dim VarVol5 As Double
        Dim VarVol6 As Double


        Dim VarLTP7 As Double
        Dim VarLTP8 As Double
        Dim VarVol7 As Double
        Dim VarVol8 As Double

    End Structure

    Public Structure Struct_FOContract
        Dim Token As Integer
        Dim script As String
        Dim lotsize As Integer
        Dim asset_tokan As Integer
        Dim InstrumentType As String
        Dim symbol As String
        Dim exhange As String

    End Structure
    Public Structure Struct_EQContract
        Dim Token As Integer
        Dim script As String
        Dim symbol As String
        Dim exhange As String

    End Structure
    Public Structure Struct_CURRContract
        Dim Token As Integer
        Dim script As String
        Dim InstrumentType As String
        Dim symbol As String
        Dim exhange As String
    End Structure

    REM implimenting of Atm General Setting  By viral 27-06-11
    Public Enum Setting_SELECTION_TYPE
        STRIKE = 1
        ITM_ATM_OTM = 2
    End Enum
    Public ATMDELTA_MIN As Double
    Public ATMDELTA_MAX As Double
    Public SELECTION_TYPE As Setting_SELECTION_TYPE
    Public VOLATITLIY As Double
    Public gVOLATITLIY As Double
    Public PREDATA_AUTO_OFF As Integer
    REM End




    REM implimenting of Dalta Calc Base On Lot or Unit Setting  By viral 8-07-11
    Public Enum Setting_Greeks_BASE
        Lot = 1
        Unit = 2
    End Enum
    Public EQ_DELTA_BASE As Setting_Greeks_BASE
    Public CURR_DELTA_BASE As Setting_Greeks_BASE

    Public EQ_GAMMA_BASE As Setting_Greeks_BASE
    Public CURR_GAMMA_BASE As Setting_Greeks_BASE

    Public EQ_VEGA_BASE As Setting_Greeks_BASE
    Public CURR_VEGA_BASE As Setting_Greeks_BASE

    Public EQ_THETA_BASE As Setting_Greeks_BASE
    Public CURR_THETA_BASE As Setting_Greeks_BASE

    Public EQ_VOLGA_BASE As Setting_Greeks_BASE
    Public CURR_VOLGA_BASE As Setting_Greeks_BASE

    Public EQ_VANNA_BASE As Setting_Greeks_BASE
    Public CURR_VANNA_BASE As Setting_Greeks_BASE


    REM End

    REM Get Contract & Curr & Security LWT(Last Write Time)  Setting  By viral 11-07-11
    Public CONTRACT_LWT As String
    Public SECURITY_LWT As String
    Public CURR_CONTRACT_LWT As String
    REM End

#End Region


#Region "GlobalTable"
    Public altemp As New DataTable
    Public alerttable As New DataTable
    Public allcomp As New DataTable
    Public dtable As New DataTable
    Public maintable As New DataTable
    Public eqtable As New DataTable
    Public tblpicmargin As New DataTable
    Public GdtFOTrades As New DataTable
    Public GdtEQTrades As New DataTable
    Public GdtCurrencyTrades As New DataTable
    Public GdtSettings As New DataTable
    Public GdtImportSetting As New DataTable
    Public comptable As New DataTable
    Public comptable_Net As New DataSet
    Public prebal As New DataTable
    ''divyesh
    Public GdtBhavcopy As New DataTable
    Public GdtCompanyAnalysis As New DataTable
    Public profit As New DataTable
    Private Const SP_select_TblBHavCopy As String = "Select_TblBhavcopy"
#End Region
#Region "Gapupdown"
    ''' <remarks>This method call to calculate LTP price according to passing Vol % using Black_Scholes function</remarks>
    Public Function Sub_cal_ltpOn_vol(ByVal futval As Double, ByVal stkprice As Double, ByVal mT As Integer, ByVal mmT As Integer, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal vol As Double, Oltp As Double) As Double
        Try
            'Dim _mt, _mt1 As Double
            'Dim ltp As Double
            'If mT = 0 Then
            '    _mt = 0.00001
            '    _mt1 = 0.00001
            'Else
            '    _mt = (mT) / 365
            '    _mt1 = (mmT) / 365
            'End If
            'If futval = 0 Then futval = 0.00001
            'If vol = 0 Then
            '    vol = 0.00001
            '    If stkprice > 0 Then
            '        Return Oltp
            '    End If
            '    'vol = (mObjData.Black_Scholes(futval, stkprice, Gvar_RateOfInterast, 0, Oltp, _mt, mIsCall, True, 0, 6))
            'End If


            Dim _mt, _mt1 As Double
            Dim ltp As Double

            If mT = 0 Then
                _mt = 0.00001
                _mt1 = 0.00001
            Else
                _mt = (mT) / 365
                '_mt1 = ((mT + 1) - CInt(txtnoofday.Text)) / 365
                _mt1 = (mmT) / 365
            End If

            If stkprice > 0 Then
                If vol <> 0 Then
                    ltp = (Greeks.Black_Scholes(futval, stkprice, Rateofinterest, 0, CDbl(vol / 100), _mt, mIsCall, False, 0, 0))

                    Return ltp
                End If
            Else
                Return futval
            End If
        Catch ex As Exception
        End Try
    End Function

#End Region



    Private Sub LoadBhavCopy()
        GdtBhavcopy = New DataTable
        Try
            Dim _adp As OleDb.OleDbDataAdapter
            GdtBhavcopy.Rows.Clear()
            _adp = New OleDb.OleDbDataAdapter("Select * From select_bhavcopy", DAL.data_access.Connection_string)
            _adp.Fill(GdtBhavcopy)
            _adp.Dispose()
        Catch ex As Exception

        End Try


        If GdtBhavcopy.Rows.Count > 0 Then
            GVarIsNewBhavcopy = True
        End If

        GdtCompanyAnalysis = New DataTable
        objTrad.select_company_ana(GdtCompanyAnalysis)

        If ThrdLoadBhavCopy.IsAlive Then
            ThrdLoadBhavCopy.Abort()
        End If
    End Sub
    Public Sub send_email(ByVal senderemail As String, ByVal senderpassword As String, ByVal receiveremail As String, ByVal subject As String, ByVal message As String)
        '  If MessageBox.Show((Convert.ToString("This will send an email to ") & receiveremail) + " are you sure ?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
        Try
            Dim mail As New MailMessage()
            Dim SmtpServer As New SmtpClient("smtp.gmail.com")

            mail.From = New MailAddress(senderemail)
            mail.[To].Add(receiveremail)
            mail.Subject = subject
            mail.IsBodyHtml = True
            mail.Body = message

            SmtpServer.Port = 587
            SmtpServer.Credentials = New System.Net.NetworkCredential(senderemail, senderpassword)
            SmtpServer.EnableSsl = True

            SmtpServer.Send(mail)
            'MessageBox.Show(Convert.ToString("Email Sent to ") & receiveremail)
        Catch ex As Exception
            ' MessageBox.Show(ex.ToString())
        End Try
        '  End If
    End Sub
    Public Function DownloadFile(ByVal FtpUrl As String, ByVal FileNameToDownload As String, ByVal userName As String, ByVal password As String, ByVal tempDirPath As String) As String
        Dim ResponseDescription As String = ""
        Dim PureFileName As String = New FileInfo(FileNameToDownload).Name
        Dim DownloadedFilePath As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName
        Dim downloadUrl As String = [String].Format("{0}/{1}", FtpUrl, FileNameToDownload)
        Dim req As FtpWebRequest = DirectCast(FtpWebRequest.Create(downloadUrl), FtpWebRequest)
        req.Method = WebRequestMethods.Ftp.DownloadFile
        req.Credentials = New NetworkCredential(userName, password)
        req.UseBinary = True
        req.Proxy = Nothing
        Try
            Dim response As FtpWebResponse = DirectCast(req.GetResponse(), FtpWebResponse)
            Dim stream As Stream = response.GetResponseStream()
            Dim buffer As Byte() = New Byte(2047) {}
            Dim fs As New FileStream(DownloadedFilePath, FileMode.Create)
            Dim ReadCount As Integer = stream.Read(buffer, 0, buffer.Length)
            While ReadCount > 0
                fs.Write(buffer, 0, ReadCount)
                ReadCount = stream.Read(buffer, 0, buffer.Length)
            End While
            ResponseDescription = response.StatusDescription
            fs.Close()
            stream.Close()
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try
        Return ResponseDescription
    End Function
    '    Public Function DownloadContract()
    '        Dim ObjImpData As New import_Data
    '        Dim ObjIO As New ImportData.ImportOperation

    '        Dim ftpURL As String = "ftp://strategybuilder.finideas.com/Contract"
    '        'Host URL or address of the FTP serve
    '        Dim userName As String = "strategybuilder" '"FI-strategybuilder"
    '        'User Name of the FTP server
    '        Dim password As String = "finideas#123"
    '        'Password of the FTP server
    '        'string _ftpDirectory = "FinTesterCSV";          //The directory in FTP server where the files are present
    '        Dim FileNameToDownload As String = "Contract.zip"
    '        Dim tempDirPath As String = Application.StartupPath + "\Contract\"
    '        If Not System.IO.Directory.Exists(Application.StartupPath + "\" + "Contract\") Then
    '            System.IO.Directory.CreateDirectory(Application.StartupPath + "\" + "Contract")
    '        End If
    '        '-----Download file Fo---
    '        Dim StrContract As String = Application.StartupPath & "\Contract\contract.zip"
    '        Dim StrContractfotxt As String = Application.StartupPath & "\Contract\contract.txt"
    '        If File.Exists(StrContract) Then
    '            File.Delete(StrContract)
    '        End If
    '        If File.Exists(StrContractfotxt) Then
    '            File.Delete(StrContractfotxt)
    '        End If

    '        DownloadFile(ftpURL, FileNameToDownload, userName, password, tempDirPath)


    'back:
    '        Threading.Thread.Sleep(1000)
    '        If File.Exists(StrContract) Then
    '            'txtpath.Text = "Zip Downloaded."
    '        Else
    '            GoTo back
    '        End If
    'back2:
    '        Threading.Thread.Sleep(1000)


    '        Try
    '            'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
    '            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
    '        Catch ex As Exception
    '            GoTo back
    '        End Try
    '        Dim StrContracttxt As String = Application.StartupPath & "\Contract\contract.txt"
    '        If File.Exists(StrContracttxt) Then
    '            'txtpath.Text = StrContracttxt



    '            flgimportContract = True
    '            import_Data.ContractImport(StrContracttxt, ObjIO, True)
    '            flgimportContract = False


    '            ' objimpmaster.fo(StrContracttxt)
    '        Else
    '            GoTo back2
    '        End If

    '        '-----Download file Eq---
    '        StrContracttxt = Application.StartupPath & "\Contract\security.txt"
    '        FileNameToDownload = "security.zip"
    '        StrContract = Application.StartupPath & "\Contract\security.zip"
    '        If File.Exists(StrContract) Then
    '            File.Delete(StrContract)
    '        End If
    '        If File.Exists(StrContracttxt) Then
    '            File.Delete(StrContracttxt)
    '        End If
    '        DownloadFile(ftpURL, FileNameToDownload, userName, password, tempDirPath)



    '        Try

    '            'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
    '            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
    '        Catch ex As Exception
    '            GoTo back
    '        End Try

    '        If File.Exists(StrContracttxt) Then

    '            flgimportContract = True
    '            import_Data.SecurityImport(StrContracttxt, ObjIO, True)
    '            flgimportContract = False

    '            'objimpmaster.EQ(StrContracttxt)
    '        Else
    '            GoTo back2
    '        End If





    '        '-----Download file Curr---
    '        StrContracttxt = Application.StartupPath & "\Contract\cd_contract.txt"
    '        FileNameToDownload = "cd_contract.zip"
    '        StrContract = Application.StartupPath & "\Contract\cd_contract.zip"
    '        If File.Exists(StrContract) Then
    '            File.Delete(StrContract)
    '        End If
    '        If File.Exists(StrContracttxt) Then
    '            File.Delete(StrContracttxt)
    '        End If
    '        DownloadFile(ftpURL, FileNameToDownload, userName, password, tempDirPath)

    '        Try

    '            'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
    '            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
    '        Catch ex As Exception
    '            GoTo back
    '        End Try
    '        'Dim FileNameToDownload1 As String = "csv_Data_Put_" + Fromdate1 + ".rar"
    '        If File.Exists(StrContracttxt) Then
    '            flgimportContract = True
    '            import_Data.CurrencyImport(StrContracttxt, ObjIO, True)
    '            flgimportContract = False
    '            'objimpmaster.CURR(StrContracttxt)
    '        Else
    '            GoTo back2
    '        End If
    '        fill_token()


    '    End Function
    Public Sub clearIEhistory()
        Try

            If NetMode = "NET" Then


                'Clear_Temp_Files
                Shell("RunDll32.exe  InetCpl.cpl,ClearMyTracksByProcess 8", AppWinStyle.Hide, False, -1)
                'Clear_History
                Shell("RunDll32.exe  InetCpl.cpl,ClearMyTracksByProcess 1", AppWinStyle.Hide, False, -1)
            End If
            ' System.Diagnostics.Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 1")
            '    Sub Clear_Form_Data()
            '    Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 16")
            'End Sub

            'Sub Clear_Saved_Passwords()
            '    Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 32")
            'End Sub

            'Sub Clear_All()
            '    Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 255")
            'End Sub

            'Sub Clear_Clear_Add_ons_Settings()
            '    Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 4351")
            'End Sub
        Catch ex As Exception

        End Try
    End Sub
#Region "Init Table & fill Table"
    Public Sub Fill_HT_RefreshTrde()
        Dim DtImportType As DataTable
        DtImportType = objTrad.Select_Import_type
        HT_RefreshTrde.Clear()
        For Each dr As DataRow In DtImportType.Rows
            If dr("Text_Type").ToString() <> "" Then
                If HT_RefreshTrde.Contains(dr("Text_Type")) = False Then
                    HT_RefreshTrde.Add(dr("Text_Type"), "")
                End If
            End If


        Next
    End Sub
    Public Sub init_datatable()
        'use for alert message
        altemp = New DataTable
        With altemp.Columns
            .Add("comp_script")
            .Add("opt")
            .Add("field")
            .Add("value1")
            .Add("value2")
            .Add("current")
        End With
        alerttable = New DataTable
        With alerttable.Columns
            .Add("comp_script")
            .Add("opt")
            .Add("field")
            .Add("value1")
            .Add("value2")
            .Add("status")
            .Add("status1")
            .Add("delta", GetType(Double))
            .Add("theta", GetType(Double))
            .Add("vega", GetType(Double))
            .Add("gamma", GetType(Double))
            .Add("volga", GetType(Double))
            .Add("vanna", GetType(Double))
            .Add("grossMTM", GetType(Double))

            .Add("vol", GetType(Double))
            .Add("token", GetType(Long))
            .Add("ftoken", GetType(Long))
            .Add("strike")
            .Add("mdate")
            .Add("cp")
            .Add("units")
            .Add("uid")
            .Add("entrydate", GetType(Date))
        End With

        'use for summary
        allcomp = New DataTable
        With allcomp.Columns
            .Add("company")
            .Add("delta", GetType(Double))
            .Add("deltaRS", GetType(Double))
            .Add("gamma", GetType(Double))
            .Add("vega", GetType(Double))
            .Add("theta", GetType(Double))
            .Add("volga", GetType(Double))
            .Add("vanna", GetType(Double))
            .Add("grossMTM", GetType(Double))

            .Add("vol", GetType(Double))
            .Add("expense", GetType(Double))
            .Add("current", GetType(Double))
            .Add("projMTM", GetType(Double))
        End With
        'use to fill maintable with available position of FO and Eq
        dtable = New DataTable
        With dtable.Columns
            .Add("month")
            .Add("strikes", GetType(Double))
            .Add("cp")
            .Add("prqty", GetType(Double))
            .Add("toqty", GetType(Double))
            .Add("units", GetType(Double))
            .Add("Lots", GetType(Double))
            .Add("traded", GetType(Double))
            .Add("last", GetType(Double))
            .Add("lv", GetType(Double))
            .Add("Timevalue", GetType(Double))
            .Add("MktVol", GetType(Double))
            .Add("delta", GetType(Double))
            .Add("deltaval", GetType(Double))
            .Add("gamma", GetType(Double))
            .Add("gmval", GetType(Double))
            .Add("vega", GetType(Double))
            .Add("vgval", GetType(Double))
            .Add("theta", GetType(Double))
            .Add("thetaval", GetType(Double))
            .Add("volga", GetType(Double))
            .Add("volgaval", GetType(Double))
            .Add("vanna", GetType(Double))
            .Add("vannaval", GetType(Double))
            .Add("company")
            .Add("mdate", GetType(Date))
            .Add("tokanno", GetType(Long))
            '=======================keval (17-2-10)
            'add  asset_tokanno column 
            .Add("asset_tokan", GetType(Long))
            '==========================
            .Add("entrydate", GetType(Date))
            .Add("script")
            .Add("isliq", GetType(Boolean))
            .Add("token1", GetType(Long))
            .Add("ftoken", GetType(Long))
            .Add("pratp", GetType(Double))
            .Add("toatp", GetType(Double))
            .Add("deltaval1", GetType(Double))
            .Add("gmval1", GetType(Double))
            .Add("vgval1", GetType(Double))
            .Add("thetaval1", GetType(Double))
            .Add("volgaval1", GetType(Double))
            .Add("Vannaval1", GetType(Double))
            .Add("status")
            .Add("last1", GetType(Double))
            .Add("lv1", GetType(Double))
            .Add("mdate_months", GetType(Long))
            .Add("fut_mdate", GetType(Date))
            .Add("prExp", GetType(Double)) 'previous expense
            .Add("toExp", GetType(Double)) 'today expense
            .Add("totExp", GetType(Double)) 'total expense
            .Add("grossmtm", GetType(Double)) 'grossmtm
            .Add("netmtm", GetType(Double)) 'grossmtm
            .Add("Remarks")
            .Add("IsCurrency", GetType(Boolean))
            .Add("IsVolFix", GetType(Boolean))
            .Add("IsCalc", GetType(Boolean))
            .Add("deltavval", GetType(Double))

            .Add("preQty", GetType(Double))
            .Add("preDate", GetType(Date))
            .Add("preSpot", GetType(Double))
            .Add("preVol", GetType(Double))
            .Add("preDelVal", GetType(Double))
            .Add("preVegVal", GetType(Double))
            .Add("preTheVal", GetType(Double))

            .Add("curSpot", GetType(Double))
            .Add("curVol", GetType(Double))
            .Add("curDelVal", GetType(Double))
            .Add("curVegVal", GetType(Double))
            .Add("curTheVal", GetType(Double))

            .Add("preTotalMTM", GetType(Double))
            .Add("preGrossMTM", GetType(Double))

            .Add("curTotalMTM", GetType(Double))
            .Add("curGrossMTM", GetType(Double))

            .Add("DeltaN", GetType(Double))
            .Add("GammaN", GetType(Double))
            .Add("VegaN", GetType(Double))
            .Add("ThetaN", GetType(Double))
            .Add("VolgaN", GetType(Double))
            .Add("VannaN", GetType(Double))
            .Add("flast", GetType(Double))
            .Add("Synfut", GetType(Double))
            .Add("Payinout", GetType(Double))

            .Add("Distance", GetType(Double))
            .Add("WAVV", GetType(Double))
            .Add("BuyPr", GetType(Double))
            .Add("SalePr", GetType(Double))
            .Add("TradingVol", GetType(Double))
            .Add("exchange")
        End With
        maintable = dtable.Clone
        'use to calculate Eq position
        eqtable = New DataTable
        With eqtable
            .Columns.Add("script")
            .Columns.Add("company")
            .Columns.Add("eq")
            .Columns.Add("prqty", GetType(Double))
            .Columns.Add("toqty", GetType(Double))
            .Columns.Add("qty", GetType(Double))
            .Columns.Add("rate", GetType(Double))
            .Columns.Add("entrydate", GetType(Date))
            .Columns.Add("entryno", GetType(Double))
            .Columns.Add("uid", GetType(Double))
            .Columns.Add("tokanno")
            .Columns.Add("asset_tokan")
            .Columns.Add("pratp", GetType(Double))
            .Columns.Add("toatp", GetType(Double))
            .Columns.Add("prExp", GetType(Double))
            .Columns.Add("toExp", GetType(Double))
            .Columns.Add("IsCalc", GetType(Boolean))
            .Columns.Add("exchange")
        End With

        'use to calculate pic margin
        tblpicmargin = New DataTable
        With tblpicmargin.Columns
            .Add("compname")
            .Add("Margin", GetType(Double))
            .Add("Date", GetType(Date))

        End With



    End Sub

    ''' <summary>
    ''' fill_trades this Procedure use to fill All type of trades From database
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub fill_trades()
        'get all trades of equity
        objTrad.select_equity(GdtEQTrades)



        'get all trades of fo
        objTrad.Trading(GdtFOTrades)
        'fill expense settings
        objTrad.select_Currency_Trading(GdtCurrencyTrades)

        fill_expense()

        GdtEQTrades.Select("script=''")
        GdtFOTrades.Select("script=''")
        GdtCurrencyTrades.Select("script=''")
    End Sub


    ''' <summary>
    ''' fill_trades this Procedure use to Fill Trades With Passing TradeType
    ''' eg(FO,EQ,CURR) 
    ''' this Function fill at a time one type of trade it will be fo or eq or curr
    ''' </summary>
    ''' <param name="sTradeType">eg(FO,EQ,CURR)</param>
    ''' <remarks>because Of in Some case There r only one type of trade need to Load so... </remarks>
    Public Sub fill_trades(ByVal sTradeType As String)

        If sTradeType = "EQ" Then
            'get all trades of equity
            objTrad.select_equity(GdtEQTrades)
            GdtEQTrades.Select("script=''")
        ElseIf sTradeType = "FO" Then
            'get all trades of fo
            objTrad.Trading(GdtFOTrades)
            GdtFOTrades.Select("script=''")
        ElseIf sTradeType = "CURR" Then
            'fill expense settings
            objTrad.select_Currency_Trading(GdtCurrencyTrades)
            GdtCurrencyTrades.Select("script=''")
        End If
        fill_expense()
    End Sub

    Public Function applicationCrash()
        Dim objAna As New analysisprocess
        Dim detable As New DataTable
        Dim dr As DataRow
        Call SetPreData()

        GoTo lblAppCrash
        'If GdtSettings.Select("SettingName='APPLICATION_CLOSING_FLAG'").Length = 0 Then
        '    objTrad.SettingName = "APPLICATION_CLOSING_FLAG"
        '    objTrad.SettingKey = "FALSE"
        '    objTrad.Insert_setting()
        '    GdtSettings = objTrad.Settings
        '    GoTo lblAppCrash
        'ElseIf GdtSettings.Select("SettingName='APPLICATION_CLOSING_FLAG'")(0).Item("SettingKey") = "FALSE" Then
        '    GoTo lblAppCrash
        'Else
        '    objTrad.Uid = GdtSettings.Select("SettingName='APPLICATION_CLOSING_FLAG'")(0).Item("Uid")
        '    objTrad.SettingName = "APPLICATION_CLOSING_FLAG"
        '    objTrad.SettingKey = "FALSE"
        '    objTrad.Update_setting()
        'End If

        'G_DTExpenseData = objTrad.Select_Expense_Data

        G_DTExpenseData = objTrad.Select_Expense_Data
        'if todays net position is stored 
        detable = objAna.sel_analysis
        If detable.Compute("count(tokanno)", "entrydate=#" & Format(Today, "dd-MMM-yyyy") & "#") > 0 Then
            maintable.Rows.Clear()
            For Each drow As DataRow In detable.Select("status=1", "company")
                dr = maintable.NewRow()
                dr("strikes") = drow("strikerate")
                dr("cp") = drow("CP")
                dr("prqty") = drow("prqty")
                dr("toqty") = drow("toqty")
                dr("units") = Val(drow("qty"))
                dr("traded") = Val(drow("rate"))
                dr("last") = drow("ltp")
                dr("flast") = drow("fltp")
                dr("last1") = drow("ltp1")
                dr("lv") = drow("vol")
                dr("MktVol") = drow("MktVol")
                'dr("Vol_Diff") = drow("MktVol") - drow("vol")
                dr("exchange") = drow("exchange")
                dr("lv1") = 0 ' drow("vol1")
                If drow("Cp") = "F" Then
                    dr("delta") = 1
                    dr("deltaval") = Val(drow("qty"))
                    dr("deltaval1") = Val(drow("qty"))
                ElseIf drow("Cp") = "E" Then
                    dr("delta") = 1
                    dr("deltaval") = Val(drow("qty"))
                    dr("deltaval1") = Val(drow("qty"))
                Else
                    dr("delta") = 0
                    dr("deltaval") = 0
                    dr("deltaval1") = 0
                End If

                If GdtCurrencyTrades.Select("Script='" & drow("script") & "' And company='" & drow("company") & "'").Length > 0 Then
                    dr("IsCurrency") = True
                    dr("Lots") = dr("units") / Currencymaster.Compute("MAX(multiplier)", "Script='" & drow("script") & "'")
                ElseIf cpfmaster.Select("script='" & drow("script") & "'").Length > 0 Then
                    dr("IsCurrency") = False
                    'divyesh
                    'dr("Lots") = Val(dr("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                    Dim objStrfo As Struct_FOContract
                    objStrfo = New Struct_FOContract
                    objStrfo = HT_FOContrct(drow("script").ToString().ToUpper())
                    Dim dblltsize As Double = objStrfo.lotsize
                    dr("Lots") = Val(dr("units")) / dblltsize

                    REM Change By Alpesh 
                    'ElseIf GdtFOTrades.Select("script='" & drow("script") & "'").Length > 0 Then
                    '    dr("IsCurrency") = False
                    '    'divyesh
                    '    dr("Lots") = Val(dr("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                    '    REM End 
                Else
                    dr("IsCurrency") = False
                    dr("LOts") = 0
                End If

                dr("theta") = 0
                dr("thetaval") = 0
                dr("vega") = 0
                dr("vgval") = 0
                dr("gamma") = 0
                dr("gmval") = 0
                dr("volga") = 0
                dr("volgaval") = 0
                dr("vanna") = 0
                dr("vannaval") = 0
                dr("company") = CStr(drow("company"))
                dr("script") = CStr(drow("script"))
                dr("mdate") = drow("mdate")
                dr("fut_mdate") = drow("mdate")
                dr("mdate_months") = (CDate(drow("mdate")).Year * 12) + (CDate(drow("mdate")).Month)
                If Not drow("cp") = "E" Then
                    dr("month") = (Format(CDate(drow("mdate")), "MMM yy"))
                End If
                dr("entrydate") = CDate(drow("entrydate")).Date
                dr("token1") = drow("token1")
                dr("isliq") = drow("isliq")
                dr("tokanno") = CStr(drow("tokanno"))
                '========================================keval
                dr("asset_tokan") = Val(cpfmaster.Compute("max(Asset_tokan)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# and option_type='XX'").ToString)
                '===============================================
                'modified by mahesh, becuase in case of far month token we can not get the ftoken
                If dr("IsCurrency") = False Then
                    'Jignesh
                    If drow("cp") = "E" Then
                        If cpfmaster.Select("Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX' AND expdate1>=#" & Format(Today, "dd-MMM-yyyy") & "#").Length > 0 Then
                            dr("fut_mdate") = cpfmaster.Compute("MIN(expdate1)", "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'")
                            dr("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                        Else
                            dr("fut_mdate") = Today
                            dr("ftoken") = 0
                        End If
                    Else
                        dr("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                        If dr("ftoken") = 0 Then

                            Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'and expdate1>=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If DtFMonthDate1.Rows.Count > 0 Then
                                dr("ftoken") = DtFMonthDate1.Rows(0)("token")
                                dr("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                            Else
                                Dim DtFMonthDate As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                                If DtFMonthDate.Rows.Count > 0 Then
                                    If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(0)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(1)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(2)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    End If
                                End If
                            End If


                        End If
                    End If
                ElseIf dr("IsCurrency") = True Then REM Is Currency Position
                    'Jignesh
                    dr("ftoken") = Val(Currencymaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                    If dr("ftoken") = 0 Then

                        Dim DtFMonthDate1 As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'and expdate1>=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                        If DtFMonthDate1.Rows.Count > 0 Then
                            dr("ftoken") = DtFMonthDate1.Rows(0)("token")
                            dr("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                        Else

                            Dim DtFMonthDate As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If DtFMonthDate.Rows.Count > 0 Then
                                If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                                    dr("ftoken") = DtFMonthDate.Rows(0)("token")
                                    dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                                ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                                    dr("ftoken") = DtFMonthDate.Rows(1)("token")
                                    dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                                ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                                    dr("ftoken") = DtFMonthDate.Rows(2)("token")
                                    dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                End If
                            End If
                        End If


                    End If
                End If
                dr("toatp") = 0
                dr("pratp") = 0
                dr("thetaval1") = 0
                dr("vgval1") = 0
                dr("gmval1") = 0
                dr("volgaval1") = 0
                dr("vannaval1") = 0
                dr("status") = drow("status")
                If dr("mdate") = Today.Date And dr("cp") <> "E" Then
                    dr("deltaval1") = 0
                    dr("thetaval1") = 0
                    dr("vgval1") = 0
                    dr("gmval1") = 0
                    dr("volgaval1") = 0
                    dr("vannaval1") = 0
                End If
                dr("remarks") = drow("remarks")
                dr("prExp") = drow("prExp")
                dr("toExp") = drow("toExp")
                dr("totExp") = -(Val(drow("prExp").ToString) + Val(drow("toExp").ToString))
                dr("IsVolFix") = CBool(drow("IsVolFix"))

                dr("DeltaN") = 0
                dr("GammaN") = 0
                dr("VegaN") = 0
                dr("ThetaN") = 0
                dr("VolgaN") = 0
                dr("VannaN") = 0

                dr("IsCalc") = drow("IsCalc")

                REM For ProffitDiff    By Viral 01-07-11
                dr("preQty") = drow("preQty")
                dr("preDate") = drow("preDate")
                dr("preSpot") = drow("preSpot")
                dr("preVol") = drow("preVol")
                dr("preDelVal") = drow("preDelVal")
                dr("preVegVal") = drow("preVegVal")
                dr("preTheVal") = drow("preTheVal")
                dr("curSpot") = drow("curSpot")
                dr("curVol") = drow("curVol")
                dr("curDelVal") = drow("curDelVal")
                dr("curVegVal") = drow("curVegVal")
                dr("curTheVal") = drow("curTheVal")

                dr("preTotalMTM") = drow("preTotalMTM")
                dr("preGrossMTM") = drow("preGrossMTM")
                dr("curTotalMTM") = drow("curTotalMTM")
                dr("curGrossMTM") = drow("curGrossMTM")

                maintable.Rows.Add(dr)
            Next
            'if yesterday net position and ltp stored
        ElseIf detable.Compute("count(tokanno)", "entrydate='" & CDate(DateAdd(DateInterval.Day, -1, Now.Date)) & "'") > 0 Then
            maintable.Rows.Clear()
            For Each drow As DataRow In detable.Select("status=1", "company")
                dr = maintable.NewRow()
                dr("strikes") = drow("strikerate")
                dr("cp") = drow("CP")
                dr("prqty") = drow("prqty")
                dr("toqty") = drow("toqty")
                dr("units") = Val(drow("qty"))
                dr("traded") = Val(drow("rate"))
                dr("last") = drow("ltp")
                dr("last1") = drow("ltp1")
                dr("lv") = drow("vol")
                dr("MktVol") = drow("MktVol")
                dr("exchange") = drow("exchange")
                dr("lv1") = 0 'drow("vol1")
                If drow("Cp") = "F" Then
                    dr("delta") = 1
                    dr("deltaval") = Val(drow("qty"))
                    dr("deltaval1") = Val(drow("qty"))
                ElseIf drow("Cp") = "E" Then
                    dr("delta") = 1
                    dr("deltaval") = Val(drow("qty"))
                    dr("deltaval1") = Val(drow("qty"))
                Else
                    dr("delta") = 0
                    dr("deltaval") = 0
                    dr("deltaval1") = 0
                End If

                dr("theta") = 0
                dr("thetaval") = 0
                dr("vega") = 0
                dr("vgval") = 0
                dr("gamma") = 0
                dr("gmval") = 0
                dr("volga") = 0
                dr("volgaval") = 0
                dr("vanna") = 0
                dr("vannaval") = 0
                dr("company") = CStr(drow("company"))
                dr("script") = CStr(drow("script"))
                dr("mdate") = CDate(drow("mdate"))
                dr("fut_mdate") = CDate(drow("mdate"))
                dr("mdate_months") = (CDate(drow("mdate")).Year * 12) + (CDate(drow("mdate")).Month)
                If (dr("cp") <> "E") Then
                    dr("month") = Format(CDate(drow("mdate")), "MMM yy")
                End If
                dr("entrydate") = CDate(drow("entrydate")).Date
                dr("token1") = drow("token1")
                dr("isliq") = drow("isliq")
                dr("tokanno") = CStr(drow("tokanno"))
                ''divyesh

                If GdtCurrencyTrades.Select("Script='" & drow("script") & "' And company='" & drow("company") & "'").Length > 0 Then
                    dr("IsCurrency") = True
                    dr("Lots") = dr("units") / Currencymaster.Compute("MAX(multiplier)", "Script='" & drow("script") & "'")
                ElseIf cpfmaster.Select("script='" & drow("script") & "'").Length > 0 Then
                    dr("IsCurrency") = False
                    dr("Lots") = Val(dr("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                    REM Change By Alpesh
                    'ElseIf GdtFOTrades.Select("script='" & drow("script") & "'").Length > 0 Then
                    '    dr("IsCurrency") = False
                    '    dr("Lots") = Val(dr("units")) / GdtFOTrades.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                Else
                    dr("IsCurrency") = False
                    dr("LOts") = 0
                End If

                '===================================keval
                dr("asset_tokan") = CStr(drow("asset_tokan"))
                '=======================================================
                'modified by mahesh, becuase in case of far month token we can not get the ftoken
                If dr("IsCurrency") = False Then
                    'Jignesh
                    If drow("CP") = "E" Then
                        If cpfmaster.Select("Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'").Length > 0 Then
                            dr("fut_mdate") = cpfmaster.Compute("MIN(expdate1)", "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'")
                            dr("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                        Else
                            dr("fut_mdate") = Today
                            dr("ftoken") = 0
                        End If
                    Else
                        dr("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                        dr("fut_mdate") = dr("mdate")
                        If dr("ftoken") = 0 Then

                            Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'and expdate1>=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If DtFMonthDate1.Rows.Count > 0 Then
                                dr("ftoken") = DtFMonthDate1.Rows(0)("token")
                                dr("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                            Else
                                Dim DtFMonthDate As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                                If DtFMonthDate.Rows.Count > 0 Then
                                    If GVarMaturity_Far_month = "CURRENT MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(0)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    ElseIf GVarMaturity_Far_month = "NEXT MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(1)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    ElseIf GVarMaturity_Far_month = "FAR MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(2)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    End If
                                End If
                            End If



                        End If

                    End If
                Else

                    'Jignesh
                    dr("ftoken") = Val(Currencymaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                    dr("fut_mdate") = dr("mdate")
                    If dr("ftoken") = 0 Then

                        Dim DtFMonthDate1 As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'and expdate1>=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                        If DtFMonthDate1.Rows.Count > 0 Then
                            dr("ftoken") = DtFMonthDate1.Rows(0)("token")
                            dr("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                        Else
                            Dim DtFMonthDate As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If DtFMonthDate.Rows.Count > 0 Then
                                If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                                    dr("ftoken") = DtFMonthDate.Rows(0)("token")
                                    dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                                ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                                    dr("ftoken") = DtFMonthDate.Rows(1)("token")
                                    dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                                ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                                    dr("ftoken") = DtFMonthDate.Rows(2)("token")
                                    dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                End If
                            End If
                        End If


                        'Else
                        'mrow("ftokan") = mrow("ftokan")
                    End If
                End If

                dr("toatp") = 0
                dr("pratp") = 0
                dr("thetaval1") = 0
                dr("vgval1") = 0
                dr("gmval1") = 0
                dr("volgaval1") = 0
                dr("vannaval1") = 0
                dr("status") = drow("status")
                If dr("mdate") = Today.Date And dr("cp") <> "E" Then
                    dr("deltaval1") = 0
                    dr("thetaval1") = 0
                    dr("vgval1") = 0
                    dr("gmval1") = 0
                    dr("volgaval1") = 0
                    dr("vannaval1") = 0
                End If
                dr("remarks") = drow("remarks")
                dr("prExp") = drow("prExp")
                dr("toExp") = drow("toExp")
                dr("totExp") = -(Val(drow("prExp").ToString) + Val(drow("toExp").ToString))
                dr("IsVolFix") = CBool(drow("IsVolFix"))

                dr("DeltaN") = 0
                dr("GammaN") = 0
                dr("VegaN") = 0
                dr("ThetaN") = 0
                dr("VolgaN") = 0
                dr("VannaN") = 0

                dr("IsCalc") = drow("IsCalc")

                REM For ProffitDiff    By Viral 01-07-11
                dr("preQty") = drow("preQty")
                dr("preDate") = drow("preDate")
                dr("preSpot") = drow("preSpot")
                dr("preVol") = drow("preVol")
                dr("preDelVal") = drow("preDelVal")
                dr("preVegVal") = drow("preVegVal")
                dr("preTheVal") = drow("preTheVal")
                dr("curSpot") = drow("curSpot")
                dr("curVol") = drow("curVol")
                dr("curDelVal") = drow("curDelVal")
                dr("curVegVal") = drow("curVegVal")
                dr("curTheVal") = drow("curTheVal")

                dr("preTotalMTM") = drow("preTotalMTM")
                dr("preGrossMTM") = drow("preGrossMTM")
                dr("curTotalMTM") = drow("curTotalMTM")
                dr("curGrossMTM") = drow("curGrossMTM")


                maintable.Rows.Add(dr)
            Next
        Else
lblAppCrash:

            Dim dtanalysis As DataTable = objAna.sel_analysis


            Call fill_table()
            Call fill_equity()
            Call fill_Currency()
            objTrad.Delete_Expense_Data_All()
            REM Calculate Expense  Alpesh 11/05/2011
            Call GSub_CalculateExpense(GdtFOTrades, "FO", True)
            'objTrad.Delete_Expense_Data_All()

            'objTrad.Insert_Expense_Data(G_DTExpenseData)
            Call GSub_CalculateExpense(GdtEQTrades, "EQ", True)
            'objTrad.Delete_Expense_Data_All()
            'objTrad.Insert_Expense_Data(G_DTExpenseData)
            Call GSub_CalculateExpense(GdtCurrencyTrades, "CURR", True)
            objTrad.Delete_Expense_Data_All()
            objTrad.Insert_Expense_Data(G_DTExpenseData)
            REM End
            objAna.process_data()

            'For Each erow As DataRow In New DataView(dtanalysis, "script='" & drow("script") & "' and cp not in('C','P') And company='" & drow("company") & "'", "", DataViewRowState.CurrentRows).ToTable(True, "entry_date").Rows   'dtdate.Rows

            'Next


            maintable = dtable


            For Each erow As DataRow In dtanalysis.Rows
                Dim str As String = erow("Script") + "_" + erow("Company")

                For Each erow1 As DataRow In maintable.Rows
                    Dim str1 As String = erow1("Script") + "_" + erow1("Company")
                    If (str = str1) Then

                        erow1("last") = erow("ltp")
                    End If

                Next
            Next

            maintable.AcceptChanges()


            If dtanalysis.Select("IsVolFix='" & True & "'").Length > 0 Then
                For Each erow As DataRow In dtanalysis.Rows
                    Dim str As String = erow("Script") + "_" + erow("Company")
                    Dim Isvol As String = erow("IsVolFix")
                    Dim vol As Double = erow("Vol")

                    For Each erow1 As DataRow In maintable.Rows
                        Dim str1 As String = erow1("Script") + "_" + erow1("Company")
                        If (str = str1) Then
                            erow1("IsVolFix") = Isvol
                            erow1("lv") = vol
                            erow1("MktVol") = erow("MktVol")
                            ' erow1("last") = erow("ltp")
                        End If

                    Next
                Next

                maintable.AcceptChanges()
            End If
            'For Each erow As DataRow In New DataView(dtanalysis, "script='" & drow("script") & "' and cp not in('C','P') And company='" & drow("company") & "'", "", DataViewRowState.CurrentRows).ToTable(True, "entry_date").Rows   'dtdate.Rows

            'Next
            For Each drow As DataRow In eqtable.Rows
                dr = maintable.NewRow()
                dr("strikes") = 0
                dr("cp") = "E"
                dr("prqty") = drow("prqty")
                dr("toqty") = drow("toqty")
                dr("units") = Val(drow("qty"))
                dr("traded") = Val(drow("rate"))
                dr("exchange") = drow("exchange")
                ''divyesh

                If GdtCurrencyTrades.Select("Script='" & drow("script") & "' And company='" & drow("company") & "'").Length > 0 Then
                    dr("IsCurrency") = True
                    dr("Lots") = dr("units") / Currencymaster.Compute("MAX(multiplier)", "Script='" & drow("script") & "'")
                ElseIf cpfmaster.Select("script='" & drow("script") & "'").Length > 0 Then
                    dr("IsCurrency") = False
                    dr("Lots") = Val(dr("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                Else
                    dr("IsCurrency") = False
                    dr("LOts") = 0
                End If

                dr("last") = 0

                dr("last1") = 0
                dr("lv") = 0
                dr("MktVol") = 0
                dr("lv1") = 0
                dr("delta") = 1
                dr("deltaval") = Val(drow("qty"))
                dr("theta") = 0
                dr("thetaval") = 0
                dr("vega") = 0
                dr("vgval") = 0
                dr("gamma") = 0
                dr("gmval") = 0
                dr("volga") = 0
                dr("volgaval") = 0
                dr("vanna") = 0
                dr("vannaval") = 0
                dr("company") = CStr(drow("company"))
                dr("script") = CStr(drow("script"))
                dr("mdate") = CDate(Format(CDate(Now.Date), "MMM/dd/yyyy"))
                dr("fut_mdate") = CDate(Format(CDate(Now.Date), "MMM/dd/yyyy"))
                'dr("mdate_months") = 0
                dr("mdate_months") = (CDate(dr("mdate")).Year * 12) + (CDate(dr("mdate")).Month)
                If (dr("cp") <> "E") Then dr("month") = (Format(CDate(Now.Date), "MMM yy"))
                dr("entrydate") = CDate(Format(CDate(drow("entrydate")), "MMM/dd/yyyy"))
                dr("token1") = 0
                dr("isliq") = False
                dr("tokanno") = CStr(drow("tokanno"))
                '=================================================
                dr("asset_tokan") = 0
                '===============================================
                dr("ftoken") = 0
                If (dr("cp") = "E") Then
                    Dim Drcpf() As DataRow = cpfmaster.Select("Symbol='" & GetSymbol(dr("company")) & "' AND InstrumentName IN ('FUTSTK','FUTIDX','FUTIVX')", "expDate1")
                    If Drcpf.Length > 0 Then
                        dr("ftoken") = Drcpf(0)("token")
                        dr("fut_mdate") = Drcpf(0)("expDate1")
                    Else
                        dr("ftoken") = 0
                        dr("fut_mdate") = Today
                    End If
                End If

                dr("toatp") = Val(drow("toatp"))
                dr("pratp") = Val(drow("pratp"))
                dr("deltaval1") = Val(drow("qty"))
                dr("thetaval1") = 0
                dr("vgval1") = 0
                dr("gmval1") = 0
                dr("volgaval1") = 0
                dr("vannaval1") = 0
                If dr("mdate") = Today.Date And dr("cp") <> "E" Then
                    dr("deltaval1") = 0
                    dr("thetaval1") = 0
                    dr("vgval1") = 0
                    dr("gmval1") = 0
                    dr("volgaval1") = 0
                    dr("vannaval1") = 0
                End If
                dr("totExp") = -(Val(drow("prExp").ToString) + Val(drow("toExp").ToString))
                dr("IsVolFix") = False

                dr("DeltaN") = 0
                dr("GammaN") = 0
                dr("VegaN") = 0
                dr("ThetaN") = 0
                dr("VolgaN") = 0
                dr("VannaN") = 0

                dr("IsCalc") = CBool(IIf(IsDBNull(drow("IsCalc")), "True", drow("IsCalc")))

                REM For ProffitDiff    By Viral 01-07-11
                If dtanalysis.Rows.Count > 0 Then
                    dr("preQty") = Val(dtanalysis.Compute("Max(preQty)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("preDate") = CDate(IIf(IsDBNull(dtanalysis.Compute("Max(PreDate)", "tokanno=" & dr("tokanno") & " And CP='E'")) = True, Today, dtanalysis.Compute("Max(PreDate)", "tokanno=" & dr("tokanno") & " And CP='E'")))
                    dr("preSpot") = Val(dtanalysis.Compute("Max(PreSpot)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("preVol") = Val(dtanalysis.Compute("Max(PreVol)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("preDelVal") = Val(dtanalysis.Compute("Max(PreDelVal)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("preVegVal") = Val(dtanalysis.Compute("Max(PreVegVal)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("preTheVal") = Val(dtanalysis.Compute("Max(PreTheVal)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")

                    dr("curSpot") = Val(dtanalysis.Compute("Max(curSpot)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("curVol") = Val(dtanalysis.Compute("Max(curVol)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("curDelVal") = Val(dtanalysis.Compute("Max(curDelVal)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("curVegVal") = Val(dtanalysis.Compute("Max(curVegVal)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("curTheVal") = Val(dtanalysis.Compute("Max(curTheVal)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")

                    dr("preTotalMTM") = Val(dtanalysis.Compute("Max(preTotalMTM)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("preGrossMTM") = Val(dtanalysis.Compute("Max(preGrossMTM)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("curTotalMTM") = Val(dtanalysis.Compute("Max(curTotalMTM)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("curGrossMTM") = Val(dtanalysis.Compute("Max(curGrossMTM)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                Else
                    dr("preQty") = 0
                    dr("preDate") = Now.Date
                    dr("preSpot") = 0
                    dr("preVol") = 0
                    dr("preDelVal") = 0
                    dr("preVegVal") = 0
                    dr("preTheVal") = 0

                    dr("curSpot") = 0
                    dr("curVol") = 0
                    dr("curDelVal") = 0
                    dr("curVegVal") = 0
                    dr("curTheVal") = 0

                    dr("preTotalMTM") = 0
                    dr("preGrossMTM") = 0
                    dr("curTotalMTM") = 0
                    dr("curGrossMTM") = 0
                End If
                If maintable.Select("Script='" & drow("script") & "' And company='" & drow("company") & "' AND exchange='" & drow("exchange") & "'").Length = 0 Then

                    maintable.Rows.Add(dr)
                End If
            Next

            REM Currency Position add into Maintable

        End If


        For Each DrMaintable As DataRow In maintable.Select("IsCurrency Is Null")
            If GdtCurrencyTrades.Select("Script='" & DrMaintable("script") & "' And company='" & DrMaintable("company") & "'").Length > 0 Then
                DrMaintable("IsCurrency") = True
                DrMaintable("Lots") = DrMaintable("units") / Currencymaster.Compute("MAX(multiplier)", "Script='" & DrMaintable("script") & "'")
            Else
                DrMaintable("IsCurrency") = False
                DrMaintable("Lots") = 0
            End If
        Next


        If mode = "Offline" Then
            If maintable.Rows.Count > 0 Then
                If maintable.Compute("count(tokanno)", "last=0") > 0 Then
                    Dim objanalysisprocess As New analysisprocess
                    dtanalysisData = objanalysisprocess.sel_analysis()
                    objAna.get_ltp(maintable)
                End If
                'If Directory.Exists(mSPAN_path) Then
                '    'generate_SPAN_data(maintable)
                'End If
            End If
        End If
        MessageBox.Show("Position Recalculate Successfully...")
    End Function
    ''' <summary>
    ''' While Application Cresh  To Creating New maintable 
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub fill_equity()
    '    Try
    '        Dim prExp, toExp As Double
    '        ' If UCase(fifo_avg) = "FALSE" Then
    '        Dim count As Integer
    '        Dim dr As DataRow
    '        Dim ar As New ArrayList
    '        eqtable.Clear()
    '        For Each drow As DataRow In GdtEQTrades.Rows
    '            count = CInt(GdtEQTrades.Compute("count(script)", "script='" & drow("script") & "' And company='" & drow("company") & "'"))
    '            If count > 1 Then
    '                If Not ar.Contains(drow("script").ToString.Trim.ToUpper & drow("company").ToString.Trim.ToUpper) Then
    '                    Dim brate As Double = 0
    '                    Dim srate As Double = 0
    '                    ar.Add(drow("script").ToString.Trim.ToUpper & drow("company").ToString.Trim.ToUpper)
    '                    dr = eqtable.NewRow()
    '                    drow("script") = drow("script").ToString.Trim
    '                    drow("company") = drow("company").ToString.Trim
    '                    dr("script") = drow("script")
    '                    dr("company") = drow("company")
    '                    dr("eq") = CStr(drow("eq"))
    '                    dr("prqty") = Val(GdtEQTrades.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date < #" & fDate(Now.Date) & "# And company='" & drow("company") & "'").ToString)
    '                    dr("toqty") = Val(GdtEQTrades.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date >= #" & fDate(Now.Date) & "# And company='" & drow("company") & "'").ToString)
    '                    dr("qty") = Val(GdtEQTrades.Compute("sum(qty)", "script='" & drow("script") & "' And company='" & drow("company") & "'"))

    '                    srate = Math.Abs(Val(GdtEQTrades.Compute("sum(tot)", "script='" & drow("script") & "' and tot < 0 And company='" & drow("company") & "'").ToString))
    '                    brate = Val(GdtEQTrades.Compute("sum(tot)", "script='" & drow("script") & "' and tot > 0 And company='" & drow("company") & "'").ToString)


    '                    'For Each row As DataRow In temtable.Select("script='" & drow("script") & "'")
    '                    '    If Val(row("qty")) < 0 Then
    '                    '        srate = srate + (-Val(row("tot")))
    '                    '    Else
    '                    '        brate = brate + Val(row("tot"))
    '                    '    End If
    '                    'Next
    '                    If Val(dr("qty")) = 0 Then
    '                        dr("rate") = Math.Round(Val((brate - srate)), 2)
    '                    Else
    '                        dr("rate") = Math.Round(Val((brate - srate) / Val(dvdata = New DataView(dtdtable, "script='" & drow("script").ToString.Trim & "'", "", DataViewRowState.CurrentRows).ToTabledr("qty"))), 2)
    '                    End If

    '                    'dr("uid") = CInt(drow("uid"))
    '                    dr("entrydate") = CDate(Format(CDate(drow("entrydate")), "MMM/dd/yyyy"))
    '                    dr("entryno") = Val(drow("entryno"))
    '                    dr("toatp") = 0
    '                    dr("pratp") = 0
    '                    prExp = 0
    '                    toExp = 0
    '                    'Call cal_position_expense(drow, prExp, toExp, "EQ")
    '                    dr("prExp") = prExp
    '                    dr("toExp") = toExp
    '                    For Each row As DataRow In eqmaster.Select("script='" & drow("script") & "'")
    '                        dr("tokanno") = CStr(row("token"))
    '                        eqtable.Rows.Add(dr)
    '                    Next
    '                End If
    '            Else
    '                dr = eqtable.NewRow()
    '                drow("script") = drow("script").ToString.Trim
    '                drow("company") = drow("company").ToString.Trim
    '                dr("script") = drow("script")
    '                dr("company") = drow("company")
    '                dr("eq") = CStr(drow("eq"))
    '                dr("prqty") = Val(GdtEQTrades.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date < #" & fDate(Now.Date) & "# And company='" & drow("company") & "'").ToString)
    '                dr("toqty") = Val(GdtEQTrades.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date >= #" & fDate(Now.Date) & "# And company='" & drow("company") & "'").ToString)

    '                dr("qty") = Val(drow("qty"))
    '                dr("rate") = Val(drow("rate"))
    '                'dr("uid") = CInt(drow("uid"))

    '                dr("entrydate") = CDate(Format(CDate(drow("entrydate")), "MMM/dd/yyyy"))
    '                dr("entryno") = Val(drow("entryno"))
    '                dr("toatp") = 0
    '                dr("pratp") = 0
    '                prExp = 0
    '                toExp = 0
    '                'Call cal_position_expense(drow, prExp, toExp, "EQ")
    '                dr("prExp") = prExp
    '                dr("toExp") = toExp
    '                For Each row As DataRow In eqmaster.Select("script='" & drow("script") & "'")
    '                    dr("tokanno") = CStr(row("token"))
    '                    eqtable.Rows.Add(dr)
    '                Next
    '            End If
    '        Next

    '        'End If

    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    'End Sub
    Private Sub fill_equity()
        Try
            Dim prExp, toExp As Double
            ' If UCase(fifo_avg) = "FALSE" Then
            Dim count As Integer
            Dim dr As DataRow
            Dim ar As New ArrayList
            eqtable.Clear()


            Dim dtdtable As DataTable = New DataView(GdtEQTrades, " ", "Script", DataViewRowState.CurrentRows).ToTable
            Dim dtdtableView As DataView = New DataView(GdtEQTrades, "", "Script", DataViewRowState.CurrentRows)
            Dim dtScriptdtable As DataTable = dtdtableView.ToTable(True, "Script", "Company", "exchange")

            For Each drow As DataRow In dtScriptdtable.Rows
                Dim drcount() As DataRow = dtdtable.Select("script='" & drow("script").ToString.Trim & "' and Company='" & drow("Company").ToString.Trim & "'and exchange='" & drow("exchange").ToString.Trim & "'", "")
                count = drcount.Length
                Dim Token As Integer
                Dim objStr As New Struct_EQContract

                Dim strScript As String = drow("script").ToString().ToUpper() & "  " & drow("exchange")
                If Not HT_EQContrct(strScript) Is Nothing Then
                    objStr = HT_EQContrct(strScript)
                    Token = objStr.Token
                Else
                    Token = 0
                End If

                Dim dvdata As DataTable
                If count > 1 Then
                    Dim brate As Double = 0
                    Dim srate As Double = 0
                    dvdata = New DataView(dtdtable, "script='" & drow("script").ToString.Trim & "' and Company='" & drow("Company").ToString.Trim & "'", "", DataViewRowState.CurrentRows).ToTable
                    dr = eqtable.NewRow()
                    dr("exchange") = drow("exchange").ToString()
                    dr("script") = drow("script").ToString.Trim
                    dr("company") = drcount(0)("company").ToString.Trim
                    dr("eq") = CStr(drcount(0)("eq"))
                    dr("prqty") = Val(dvdata.Compute("sum(qty)", "script='" & drow("script") & "' and  Company='" & drow("Company").ToString.Trim & "' and entry_date < #" & fDate(Now.Date) & "# ").ToString)
                    dr("toqty") = Val(dvdata.Compute("sum(qty)", "script='" & drow("script") & "' and Company='" & drow("Company").ToString.Trim & "' and entry_date >= #" & fDate(Now.Date) & "#").ToString)
                    dr("qty") = Val(dvdata.Compute("sum(qty)", "script='" & drow("script") & "'and Company='" & drow("Company").ToString.Trim & "' "))

                    srate = Math.Abs(Val(dvdata.Compute("sum(tot)", "script='" & drow("script") & "' and Company='" & drow("Company").ToString.Trim & "'and tot < 0").ToString))
                    brate = Val(dvdata.Compute("sum(tot)", "script='" & drow("script") & "' and Company='" & drow("Company").ToString.Trim & "' and tot > 0 ").ToString)

                    If Val(dr("qty")) = 0 Then
                        dr("rate") = Math.Round(Val((brate - srate)), 2)
                    Else
                        dr("rate") = Math.Round(Val((brate - srate) / Val(dr("qty"))), 2)
                    End If

                    'dr("uid") = CInt(drow("uid"))
                    dr("entrydate") = CDate(Format(CDate(drcount(0)("entrydate")), "MMM/dd/yyyy"))
                    dr("entryno") = Val(drcount(0)("entryno"))
                    dr("toatp") = 0
                    dr("pratp") = 0
                    prExp = 0
                    toExp = 0
                    'Call cal_position_expense(drow, prExp, toExp, "EQ")
                    dr("prExp") = prExp
                    dr("toExp") = toExp
                    dr("exchange") = drow("exchange").ToString()



                    'For Each row As DataRow In eqmaster.Select("script='" & drow("script") & "'")
                    '    dr("tokanno") = CStr(row("token"))


                    If Token <> 0 Then
                        dr("tokanno") = Token
                        eqtable.Rows.Add(dr)
                    End If

                    'Next

                Else
                    dvdata = New DataView(dtdtable, "script='" & drow("script").ToString.Trim & "'and Company='" & drow("Company").ToString.Trim & "'", "", DataViewRowState.CurrentRows).ToTable
                    dr = eqtable.NewRow()

                    dr("script") = drow("script").ToString.Trim
                    dr("company") = drcount(0)("company").ToString.Trim
                    dr("eq") = CStr(drcount(0)("eq"))
                    dr("prqty") = Val(GdtEQTrades.Compute("sum(qty)", "script='" & drow("script") & "' and Company='" & drow("Company").ToString.Trim & "'and entry_date < #" & fDate(Now.Date) & "# ").ToString)
                    dr("toqty") = Val(GdtEQTrades.Compute("sum(qty)", "script='" & drow("script") & "'and Company='" & drow("Company").ToString.Trim & "' and entry_date >= #" & fDate(Now.Date) & "# ").ToString)

                    dr("qty") = Val(drcount(0)("qty"))
                    dr("rate") = Val(drcount(0)("rate"))
                    'dr("uid") = CInt(drow("uid"))

                    dr("entrydate") = CDate(Format(CDate(drcount(0)("entrydate")), "MMM/dd/yyyy"))
                    dr("entryno") = Val(drcount(0)("entryno"))
                    dr("toatp") = 0
                    dr("pratp") = 0
                    prExp = 0
                    toExp = 0
                    'Call cal_position_expense(drow, prExp, toExp, "EQ")
                    dr("prExp") = prExp
                    dr("toExp") = toExp
                    'For Each row As DataRow In eqmaster.Select("script='" & drow("script") & "'")
                    '    dr("tokanno") = CStr(row("token"))
                    '    eqtable.Rows.Add(dr)
                    'Next
                    dr("exchange") = drow("exchange").ToString()
                    If Token <> 0 Then
                        dr("tokanno") = CStr(Token)
                        eqtable.Rows.Add(dr)
                    End If

                End If

            Next


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub fill_table()
        Try
            If UCase(fifo_avg) = "FALSE" Then
                ''AVG #########################################################################################
                Dim dr As DataRow
                Dim count As Integer
                Dim ar As New ArrayList
                Dim prExp, toExp As Double
                dtable.Rows.Clear()

                Dim dtdtable As DataTable = New DataView(GdtFOTrades, " ", "Script", DataViewRowState.CurrentRows).ToTable
                Dim dtdtableView As DataView = New DataView(GdtFOTrades, "", "Script", DataViewRowState.CurrentRows)
                Dim dtScriptdtable As DataTable = dtdtableView.ToTable(True, "Script", "Company", "exchange")

                For Each drow As DataRow In dtScriptdtable.Rows
                    Dim dvdata As DataTable
                    Dim DVcpfmaster As DataTable
                    Dim DVDtAnalysis As DataTable
                    Dim exchange As String = drow("exchange")

                    Dim drcount As DataRow() = GdtFOTrades.Select("script='" & drow("script") & "' and Company='" & drow("Company") & "' and exchange='" & exchange & "'")

                    Dim objStr As New Struct_FOContract

                    Dim token As Double
                    Dim asset_tokan As Double
                    Dim strScript As String = drow("script").ToString().ToUpper() & Space(2) & exchange

                    If Not HT_FOContrct(strScript) Is Nothing Then
                        objStr = HT_FOContrct(strScript)
                        token = objStr.Token
                        asset_tokan = objStr.asset_tokan
                    Else
                        token = 0
                        asset_tokan = 0
                    End If

                    'If Not HT_FOContrct(dr("script").ToString().ToUpper()) Is Nothing Then
                    '    objStr = HT_FOContrct(dr("script").ToString().ToUpper())
                    '    asset_tokan = objStr.asset_tokan
                    'Else
                    '    asset_tokan = 0
                    'End If
                    If drcount.Length > 1 Then
                        Dim strFilter As String = "script='" & drow("script").ToString.Trim & "' and Company='" & drow("Company") & "' and exchange='" & exchange & "'"
                        dvdata = New DataView(dtdtable, strFilter, "", DataViewRowState.CurrentRows).ToTable
                        'DVcpfmaster = New DataView(cpfmaster, "script='" & drow("script").ToString.Trim & "'", "", DataViewRowState.CurrentRows).ToTable

                        Dim brate As Double = 0
                        Dim srate As Double = 0
                        dr = dtable.NewRow()
                        dr("strikes") = Val(drcount(0)("strikerate").ToString)
                        dr("exchange") = drow("exchange")
                        dr("prqty") = Val(dvdata.Compute("sum(qty)", "entry_date < #" & Format(Today, "dd-MMM-yyyy") & "#").ToString)

                        dr("toqty") = Val(dvdata.Compute("sum(qty)", "entry_date >= #" & Format(Today, "dd-MMM-yyyy") & "# ").ToString)

                        dr("units") = Val(dvdata.Compute("sum(qty)", "script='" & drow("script") & "'and Company='" & drow("Company") & "'"))
                        srate = Math.Abs(Val(dvdata.Compute("sum(tot)", "script='" & drow("script") & "'and Company='" & drow("Company") & "' and tot < 0 ").ToString))
                        brate = Val(dvdata.Compute("sum(tot)", "script='" & drow("script") & "'and Company='" & drow("Company") & "' and tot > 0").ToString)
                        ' dr("traded") = 0
                        If Val(dr("units")) = 0 Then
                            dr("traded") = Val((brate - srate))
                        Else
                            dr("traded") = Val((brate - srate) / Val(dr("units"))) ' Math.Round(val((brate - srate) / val(dr("units"))), 2)
                        End If
                        dr("last") = 0
                        dr("lv") = 0
                        dr("MktVol") = 0
                        dr("delta") = 0
                        dr("deltaval") = 0
                        dr("deltaval1") = 0
                        If Not drcount(0)("cp").ToString = "" Then
                            If drcount(0)("cp") = "C" Or drcount(0)("CP") = "P" Then
                                dr("cp") = CStr(drcount(0)("cp"))
                            Else
                                dr("cp") = "F"
                                dr("delta") = 1
                                dr("deltaval") = dr("units")
                                dr("deltaval1") = dr("units")
                            End If
                        Else
                            dr("cp") = "F"
                            dr("delta") = 1
                            dr("deltaval") = dr("units")
                            dr("deltaval1") = dr("units")
                        End If

                        dr("theta") = 0
                        dr("thetaval") = 0
                        dr("thetaval1") = 0
                        dr("vega") = 0
                        dr("vgval") = 0
                        dr("vgval1") = 0
                        dr("gamma") = 0
                        dr("gmval") = 0
                        dr("gmval1") = 0
                        'Alpesh 20/04/2011
                        dr("volga") = 0
                        dr("volgaval") = 0
                        dr("volgaval1") = 0
                        dr("vanna") = 0
                        dr("vannaval") = 0
                        dr("vannaval1") = 0

                        dr("company") = CStr(drcount(0)("company"))
                        dr("script") = CStr(drow("script"))
                        'dr("mo") = drow("mo")
                        dr("mdate") = CDate(drcount(0)("mdate"))
                        dr("fut_mdate") = CDate(drcount(0)("mdate"))
                        dr("mdate_months") = (CDate(drcount(0)("mdate")).Year * 12) + (CDate(drcount(0)("mdate")).Month)
                        dr("month") = Format(CDate(drcount(0)("mdate")), "MMM yy")

                        'dr("uid") = CInt(drow("uid"))
                        dr("entrydate") = CDate(drcount(0)("entrydate")).Date
                        dr("token1") = drcount(0)("token1")
                        dr("isliq") = CBool(drcount(0)("isliq"))

                        '===========================================keval

                        dr("asset_tokan") = asset_tokan
                        '==========================================
                        'dr("tokanno") = Val(cpfmaster.Compute("max(token)", "script='" & drow("script") & "'").ToString)
                        dr("tokanno") = Val(token)
                        dr("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX' AND exchange='" & exchange & "'").ToString)
                        dr("fut_mdate") = dr("mdate")
                        If dr("ftoken") = 0 Then
                            Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'and expdate1>=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If DtFMonthDate1.Rows.Count > 0 Then
                                dr("ftoken") = DtFMonthDate1.Rows(0)("token")
                                dr("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                            Else
                                Dim DtFMonthDate As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                                If DtFMonthDate.Rows.Count > 0 Then
                                    If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(0)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(1)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(2)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    End If
                                End If
                            End If


                        End If

                        dr("toatp") = 0
                        dr("pratp") = 0
                        prExp = 0
                        toExp = 0
                        Call cal_position_expense(drcount(0), prExp, toExp, "FO")
                        dr("prExp") = prExp
                        dr("toExp") = toExp
                        dr("totExp") = -(prExp + toExp)

                        dr("IsCurrency") = False
                        dr("IsVolFix") = False

                        dr("DeltaN") = 0
                        dr("GammaN") = 0
                        dr("VegaN") = 0
                        dr("ThetaN") = 0
                        dr("VolgaN") = 0
                        dr("VannaN") = 0

                        dr("IsCalc") = True

                        REM For ProffitDiff    By Viral 01-07-11

                        If DtAnalysis.Rows.Count > 0 Then
                            dr("preQty") = Val(DtAnalysis.Compute("Max(preQty)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preDate") = CDate(IIf(IsDBNull(DtAnalysis.Compute("Max(PreDate)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'")) = True, Now.Date, DtAnalysis.Compute("Max(PreDate)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'")))
                            dr("preSpot") = Val(DtAnalysis.Compute("Max(PreSpot)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preVol") = Val(DtAnalysis.Compute("Max(PreVol)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preDelVal") = Val(DtAnalysis.Compute("Max(PreDelVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preVegVal") = Val(DtAnalysis.Compute("Max(PreVegVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preTheVal") = Val(DtAnalysis.Compute("Max(PreTheVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")

                            dr("curSpot") = Val(DtAnalysis.Compute("Max(curSpot)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curVol") = Val(DtAnalysis.Compute("Max(curVol)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curDelVal") = Val(DtAnalysis.Compute("Max(curDelVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curVegVal") = Val(DtAnalysis.Compute("Max(curVegVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curTheVal") = Val(DtAnalysis.Compute("Max(curTheVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")

                            dr("preTotalMTM") = Val(DtAnalysis.Compute("Max(preTotalMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preGrossMTM") = Val(DtAnalysis.Compute("Max(preGrossMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curTotalMTM") = Val(DtAnalysis.Compute("Max(curTotalMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curGrossMTM") = Val(DtAnalysis.Compute("Max(curGrossMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")


                        Else
                            dr("preQty") = 0
                            dr("preDate") = Now.Date
                            dr("preSpot") = 0
                            dr("preVol") = 0
                            dr("preDelVal") = 0
                            dr("preVegVal") = 0
                            dr("preTheVal") = 0

                            dr("curSpot") = 0
                            dr("curVol") = 0
                            dr("curDelVal") = 0
                            dr("curVegVal") = 0
                            dr("curTheVal") = 0

                            dr("preTotalMTM") = 0
                            dr("preGrossMTM") = 0
                            dr("curTotalMTM") = 0
                            dr("curGrossMTM") = 0
                        End If
                        If CLng(dr("tokanno")) <> 0 Then
                            dtable.Rows.Add(dr)
                        End If
                    Else
                        dvdata = New DataView(dtdtable, "script='" & drow("script").ToString.Trim & "' and  Company='" & drow("Company") & "'", "", DataViewRowState.CurrentRows).ToTable
                        dr = dtable.NewRow()
                        dr("strikes") = Val(drcount(0)("strikerate"))
                        dr("prqty") = Val(dvdata.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date < #" & fDate(Now.Date) & "# And company='" & drcount(0)("company") & "'").ToString)
                        dr("toqty") = Val(dvdata.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date >= #" & fDate(Now.Date) & "# And company='" & drcount(0)("company") & "'").ToString)
                        dr("units") = Val(drcount(0)("qty").ToString)
                        dr("traded") = Math.Round(Val(drcount(0)("rate").ToString), 2)
                        dr("last") = 0
                        dr("lv") = 0
                        dr("MktVol") = 0
                        dr("delta") = 0
                        dr("deltaval") = 0
                        dr("deltaval1") = 0
                        If Not drcount(0)("cp").ToString = "" Then
                            If drcount(0)("cp") = "C" Or drcount(0)("CP") = "P" Then
                                dr("cp") = CStr(drcount(0)("cp"))
                            Else
                                dr("cp") = "F"
                                dr("delta") = 1
                                dr("deltaval") = dr("units")
                                dr("deltaval1") = dr("units")
                            End If
                        Else
                            dr("cp") = "F"
                            dr("delta") = 1
                            dr("deltaval") = dr("units")
                            dr("deltaval1") = dr("units")
                        End If

                        dr("theta") = 0
                        dr("thetaval") = 0
                        dr("vega") = 0
                        dr("vgval") = 0
                        dr("vgval1") = 0
                        dr("gamma") = 0
                        dr("gmval") = 0
                        dr("gmval1") = 0

                        dr("volga") = 0
                        dr("volgaval") = 0
                        dr("volgaval1") = 0
                        dr("vanna") = 0
                        dr("vannaval") = 0
                        dr("vannaval1") = 0

                        dr("company") = CStr(drcount(0)("company"))
                        dr("script") = CStr(drow("script"))
                        'dr("mo") = drow("mo")
                        dr("mdate") = CDate(drcount(0)("mdate"))
                        dr("fut_mdate") = CDate(drcount(0)("mdate"))
                        dr("mdate_months") = (CDate(drcount(0)("mdate")).Year * 12) + (CDate(drcount(0)("mdate")).Month)
                        '  dr("month") = ""
                        dr("month") = Format(CDate(drcount(0)("mdate")), "MMM yy")
                        ' dr("uid") = CInt(drow("uid"))
                        dr("entrydate") = CDate(drcount(0)("entrydate")).Date
                        dr("token1") = drcount(0)("token1")
                        dr("isliq") = CBool(drcount(0)("isliq"))
                        '===========================================keval



                        dr("asset_tokan") = asset_tokan 'IIf(IsDBNull(scripttable.Compute("max(asset_tokan)", "script='" & drow("script") & "'")), 0, scripttable.Compute("max(asset_tokan)", "script='" & drow("script") & "'"))
                        '=========================================
                        'dr("tokanno") = Val(cpfmaster.Compute("max(token)", "script='" & drow("script") & "'").ToString)
                        dr("tokanno") = Val(token)
                        dr("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND InstrumentName IN ('FUTSTK','FUTIDX','FUTIVX')").ToString)
                        'if far month maturity
                        If dr("ftoken") = 0 Then

                            Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'and expdate1>=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If DtFMonthDate1.Rows.Count > 0 Then
                                dr("ftoken") = DtFMonthDate1.Rows(0)("token")
                                dr("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                            Else
                                Dim DtFMonthDate As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(dr("Company")) & "' AND InstrumentName IN ('FUTSTK','FUTIDX','FUTIVX')", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                                If DtFMonthDate.Rows.Count > 0 Then
                                    If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(0)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(1)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(2)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    End If
                                End If
                            End If


                        End If
                        dr("toatp") = 0
                        dr("pratp") = 0
                        prExp = 0
                        toExp = 0
                        Call cal_position_expense(drcount(0), prExp, toExp, "FO")
                        dr("prExp") = prExp
                        dr("toExp") = toExp
                        dr("totExp") = -(prExp + toExp)

                        dr("IsCurrency") = False
                        dr("IsVolFix") = False

                        dr("DeltaN") = 0
                        dr("GammaN") = 0
                        dr("VegaN") = 0
                        dr("ThetaN") = 0
                        dr("VolgaN") = 0
                        dr("VannaN") = 0

                        dr("IsCalc") = True
                        dr("exchange") = exchange
                        REM For ProffitDiff    By Viral 01-07-11
                        If DtAnalysis.Rows.Count > 0 Then
                            dr("preQty") = Val(DtAnalysis.Compute("Max(preQty)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preDate") = CDate(IIf(IsDBNull(DtAnalysis.Compute("Max(PreDate)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'")) = True, Now.Date, DtAnalysis.Compute("Max(PreDate)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & ""))
                            dr("preSpot") = Val(DtAnalysis.Compute("Max(PreSpot)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preVol") = Val(DtAnalysis.Compute("Max(PreVol)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preDelVal") = Val(DtAnalysis.Compute("Max(PreDelVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preVegVal") = Val(DtAnalysis.Compute("Max(PreVegVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preTheVal") = Val(DtAnalysis.Compute("Max(PreTheVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")

                            dr("curSpot") = Val(DtAnalysis.Compute("Max(curSpot)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curVol") = Val(DtAnalysis.Compute("Max(curVol)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curDelVal") = Val(DtAnalysis.Compute("Max(curDelVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curVegVal") = Val(DtAnalysis.Compute("Max(curVegVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curTheVal") = Val(DtAnalysis.Compute("Max(curTheVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")

                            dr("preTotalMTM") = Val(DtAnalysis.Compute("Max(preTotalMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preGrossMTM") = Val(DtAnalysis.Compute("Max(preGrossMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curTotalMTM") = Val(DtAnalysis.Compute("Max(curTotalMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curGrossMTM") = Val(DtAnalysis.Compute("Max(curGrossMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                        Else
                            dr("preQty") = 0
                            dr("preDate") = Now.Date
                            dr("preSpot") = 0
                            dr("preVol") = 0
                            dr("preDelVal") = 0
                            dr("preVegVal") = 0
                            dr("preTheVal") = 0

                            dr("curSpot") = 0
                            dr("curVol") = 0
                            dr("curDelVal") = 0
                            dr("curVegVal") = 0
                            dr("curTheVal") = 0

                            dr("preTotalMTM") = 0
                            dr("preGrossMTM") = 0
                            dr("curTotalMTM") = 0
                            dr("curGrossMTM") = 0
                        End If


                        If CLng(dr("tokanno")) <> 0 Then
                            dtable.Rows.Add(dr)
                        End If
                    End If
                Next




            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    'Public Sub fill_Currency()
    '    Try
    '        Dim dr As DataRow
    '        Dim count As Integer
    '        Dim ar As New ArrayList
    '        Dim table As New DataTable
    '        'table = New DataTable
    '        GdtCurrencyTrades = objTrad.select_Currency_Trading
    '        table = GdtCurrencyTrades 'objTrad.Trading


    '        Dim prExp, toExp As Double
    '        For Each drow As DataRow In table.Rows
    '            If Not ar.Contains(drow("script")) Then
    '                ar.Add(drow("script"))
    '                count = CInt(table.Compute("count(script)", "script='" & drow("script") & "'"))
    '                If count > 1 Then
    '                    Dim brate As Double = 0
    '                    Dim srate As Double = 0
    '                    dr = dtable.NewRow()
    '                    dr("strikes") = Val(drow("strikerate"))
    '                    dr("prqty") = Val(table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date < '" & Now.Date & "'").ToString)
    '                    dr("toqty") = Val(table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date >= '" & Now.Date & "' ").ToString)
    '                    dr("units") = Val(table.Compute("sum(qty)", "script='" & drow("script") & "'"))

    '                    srate = Math.Abs(Val(IIf(IsDBNull(table.Compute("sum(tot)", "script='" & drow("script") & "' and tot < 0 ")), 0, table.Compute("sum(tot)", "script='" & drow("script") & "' and tot < 0  "))))
    '                    brate = Val(IIf(IsDBNull(table.Compute("sum(tot)", "script='" & drow("script") & "' and tot > 0 ")), 0, table.Compute("sum(tot)", "script='" & drow("script") & "' and tot > 0  ")))
    '                    If Val(dr("units")) = 0 Then
    '                        dr("traded") = Val((brate - srate))
    '                    Else
    '                        dr("traded") = Val((brate - srate) / Val(dr("units"))) ' Math.Round(val((brate - srate) / val(dr("units"))), 2)
    '                    End If
    '                    dr("last") = 0
    '                    dr("last1") = 0
    '                    dr("lv") = 0
    '                    dr("lv1") = 0
    '                    dr("cp") = drow("cp")
    '                    If Not IsDBNull(drow("cp")) Then
    '                        If drow("cp") = "C" Then
    '                            dr("Cp") = "C"
    '                        ElseIf drow("cp") = "P" Then
    '                            dr("Cp") = "P"
    '                        Else
    '                            dr("Cp") = "F"
    '                        End If
    '                    Else
    '                        dr("Cp") = "F"
    '                    End If
    '                    dr("company") = CStr(drow("company"))
    '                    dr("script") = CStr(drow("script"))
    '                    dr("mdate") = CDate(drow("mdate"))
    '                    dr("fut_mdate") = CDate(drow("mdate"))
    '                    dr("entrydate") = Today
    '                    dr("token1") = drow("token1")
    '                    dr("isliq") = CBool(drow("isliq"))
    '                    dr("tokanno") = IIf(IsDBNull(Currencymaster.Compute("max(token)", "script='" & drow("script") & "'")), 0, Currencymaster.Compute("max(token)", "script='" & drow("script") & "'"))
    '                    dr("ftoken") = IIf(IsDBNull(Currencymaster.Compute("max(token)", "Symbol='" & dr("company") & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# and option_type='XX'")), 0, Currencymaster.Compute("max(token)", "Symbol='" & dr("company") & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# and option_type='XX'"))
    '                    If dr("ftoken") = 0 Then
    '                        Dim DtFMonthDate As DataTable = New DataView(Currencymaster, "Symbol='" & drow("company") & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '                        If DtFMonthDate.Rows.Count > 0 Then
    '                            If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" And DtFMonthDate.Rows.Count = 1 Then
    '                                dr("ftoken") = DtFMonthDate.Rows(0)("token")
    '                                dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
    '                            ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" And DtFMonthDate.Rows.Count = 2 Then
    '                                dr("ftoken") = DtFMonthDate.Rows(1)("token")
    '                                dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
    '                            ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" And DtFMonthDate.Rows.Count = 3 Then
    '                                dr("ftoken") = DtFMonthDate.Rows(2)("token")
    '                                dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
    '                            End If
    '                        End If
    '                    End If
    '                    dr("month") = (Format(CDate(dr("fut_mdate")), "MMM yy"))

    '                    dr("status") = 1
    '                    dr("theta") = 0
    '                    dr("thetaval") = 0
    '                    dr("vega") = 0
    '                    dr("vgval") = 0
    '                    dr("vgval1") = 0
    '                    dr("gamma") = 0
    '                    dr("gmval") = 0
    '                    dr("gmval1") = 0
    '                    dr("volga") = 0
    '                    dr("volgaval") = 0
    '                    dr("volgaval1") = 0
    '                    dr("vanna") = 0
    '                    dr("vannaval") = 0
    '                    dr("vannaval1") = 0
    '                    If CLng(dr("tokanno")) <> 0 Then
    '                        prExp = 0
    '                        toExp = 0
    '                        Call cal_position_expense(dr, prExp, toExp, "CURR")
    '                        dr("prExp") = prExp
    '                        dr("toExp") = toExp
    '                        dr("totExp") = -(prExp + toExp)
    '                        dtable.Rows.Add(dr)
    '                    End If
    '                Else
    '                    dr = dtable.NewRow()
    '                    dr("strikes") = Val(drow("strikerate"))
    '                    dr("prqty") = Val(IIf(IsDBNull(table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date < '" & Now.Date & "'")), 0, table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date < '" & Now.Date & "'")))
    '                    dr("toqty") = Val(IIf(IsDBNull(table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date >= '" & Now.Date & "' ")), 0, table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date >= '" & Now.Date & "' ")))
    '                    dr("units") = Val(drow("qty"))
    '                    dr("traded") = Val(drow("rate"))
    '                    dr("last") = 0
    '                    dr("last1") = 0
    '                    dr("lv") = 0
    '                    dr("lv1") = 0
    '                    dr("cp") = drow("cp")
    '                    If Not IsDBNull(drow("cp")) Then
    '                        If drow("cp") = "C" Then
    '                            dr("Cp") = "C"
    '                        ElseIf drow("cp") = "P" Then
    '                            dr("Cp") = "P"
    '                        Else
    '                            dr("Cp") = "F"
    '                        End If
    '                    Else
    '                        dr("Cp") = "F"
    '                    End If
    '                    dr("company") = CStr(drow("company"))
    '                    dr("script") = CStr(drow("script"))
    '                    dr("mdate") = CDate(drow("mdate"))
    '                    dr("fut_mdate") = CDate(drow("mdate"))
    '                    dr("entrydate") = Today
    '                    dr("token1") = drow("token1")
    '                    dr("isliq") = CBool(drow("isliq"))
    '                    dr("tokanno") = Val(Currencymaster.Compute("max(token)", "script='" & drow("script") & "'").ToString)
    '                    dr("ftoken") = Val(Currencymaster.Compute("max(token)", "Symbol='" & dr("company") & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# and option_type='XX'").ToString)
    '                    If dr("ftoken") = 0 Then
    '                        Dim DtFMonthDate As DataTable = New DataView(Currencymaster, "Symbol='" & drow("company") & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '                        If DtFMonthDate.Rows.Count > 0 Then
    '                            If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" And DtFMonthDate.Rows.Count = 1 Then
    '                                dr("ftoken") = DtFMonthDate.Rows(0)("token")
    '                                dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
    '                            ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" And DtFMonthDate.Rows.Count = 2 Then
    '                                dr("ftoken") = DtFMonthDate.Rows(1)("token")
    '                                dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
    '                            ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" And DtFMonthDate.Rows.Count = 3 Then
    '                                dr("ftoken") = DtFMonthDate.Rows(2)("token")
    '                                dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
    '                            End If
    '                        End If
    '                    End If
    '                    dr("month") = (Format(CDate(dr("fut_mdate")), "MMM yy"))
    '                    dr("status") = 1
    '                    dr("theta") = 0
    '                    dr("thetaval") = 0
    '                    dr("vega") = 0
    '                    dr("vgval") = 0
    '                    dr("vgval1") = 0
    '                    dr("gamma") = 0
    '                    dr("gmval") = 0
    '                    dr("gmval1") = 0
    '                    dr("volga") = 0
    '                    dr("volgaval") = 0
    '                    dr("volgaval1") = 0
    '                    dr("vanna") = 0
    '                    dr("vannaval") = 0
    '                    dr("vannaval1") = 0
    '                    If CLng(dr("tokanno")) <> 0 Then
    '                        prExp = 0
    '                        toExp = 0
    '                        Call cal_position_expense(dr, prExp, toExp, "CURR")
    '                        dr("prExp") = prExp
    '                        dr("toExp") = toExp
    '                        dr("totExp") = -(prExp + toExp)
    '                        dtable.Rows.Add(dr)
    '                    End If
    '                End If
    '            End If
    '        Next
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    'End Sub

    ''Public Sub fill_Currency()
    ''    Try
    ''        Dim dr As DataRow
    ''        Dim count As Integer
    ''        Dim ar As New ArrayList
    ''        Dim table As New DataTable
    ''        'table = New DataTable
    ''        GdtCurrencyTrades = objTrad.select_Currency_Trading
    ''        table = GdtCurrencyTrades 'objTrad.Trading


    ''        Dim prExp, toExp As Double
    ''        For Each drow As DataRow In table.Rows
    ''            If Not ar.Contains(drow("script")) Then
    ''                ar.Add(drow("script"))
    ''                count = CInt(table.Compute("count(script)", "script='" & drow("script") & "'"))
    ''                If count > 1 Then
    ''                    Dim brate As Double = 0
    ''                    Dim srate As Double = 0
    ''                    dr = dtable.NewRow()
    ''                    dr("strikes") = Val(drow("strikerate"))
    ''                    dr("prqty") = Val(table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date < '" & Now.Date & "'").ToString)
    ''                    dr("toqty") = Val(table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date >= '" & Now.Date & "' ").ToString)
    ''                    dr("units") = Val(table.Compute("sum(qty)", "script='" & drow("script") & "'"))

    ''                    srate = Math.Abs(Val(IIf(IsDBNull(table.Compute("sum(tot)", "script='" & drow("script") & "' and tot < 0 ")), 0, table.Compute("sum(tot)", "script='" & drow("script") & "' and tot < 0  "))))
    ''                    brate = Val(IIf(IsDBNull(table.Compute("sum(tot)", "script='" & drow("script") & "' and tot > 0 ")), 0, table.Compute("sum(tot)", "script='" & drow("script") & "' and tot > 0  ")))
    ''                    If Val(dr("units")) = 0 Then
    ''                        dr("traded") = Val((brate - srate))
    ''                    Else
    ''                        dr("traded") = Val((brate - srate) / Val(dr("units"))) ' Math.Round(val((brate - srate) / val(dr("units"))), 2)
    ''                    End If
    ''                    dr("last") = 0
    ''                    dr("last1") = 0
    ''                    dr("lv") = 0
    ''                    dr("lv1") = 0
    ''                    dr("cp") = drow("cp")
    ''                    REM Chnage By Alpesh For Delta
    ''                    dr("delta") = 0
    ''                    dr("deltaval") = 0
    ''                    dr("deltaval1") = 0
    ''                    If Not IsDBNull(drow("cp")) Then
    ''                        If drow("cp") = "C" Then
    ''                            dr("Cp") = "C"
    ''                        ElseIf drow("cp") = "P" Then
    ''                            dr("Cp") = "P"
    ''                        Else
    ''                            dr("Cp") = "F"
    ''                            REM Chnage By Alpesh For Currency         
    ''                            dr("delta") = 1
    ''                            dr("deltaval") = Val(drow("qty"))
    ''                            dr("deltaval1") = Val(drow("qty"))
    ''                        End If
    ''                    Else
    ''                        dr("Cp") = "F"
    ''                        REM Chnage By Alpesh For Currency         
    ''                        dr("delta") = 1
    ''                        dr("deltaval") = Val(drow("qty"))
    ''                        dr("deltaval1") = Val(drow("qty"))
    ''                    End If
    ''                    dr("company") = CStr(drow("company"))
    ''                    dr("script") = CStr(drow("script"))
    ''                    dr("mdate") = CDate(drow("mdate"))
    ''                    dr("fut_mdate") = CDate(drow("mdate"))
    ''                    dr("entrydate") = Today
    ''                    dr("token1") = drow("token1")
    ''                    dr("isliq") = CBool(drow("isliq"))
    ''                    dr("tokanno") = IIf(IsDBNull(Currencymaster.Compute("max(token)", "script='" & drow("script") & "'")), 0, Currencymaster.Compute("max(token)", "script='" & drow("script") & "'"))
    ''                    dr("ftoken") = IIf(IsDBNull(Currencymaster.Compute("max(token)", "Symbol='" & dr("company") & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# and option_type='XX'")), 0, Currencymaster.Compute("max(token)", "Symbol='" & dr("company") & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# and option_type='XX'"))
    ''                    If dr("ftoken") = 0 Then
    ''                        Dim DtFMonthDate As DataTable = New DataView(Currencymaster, "Symbol='" & drow("company") & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    ''                        If DtFMonthDate.Rows.Count > 0 Then
    ''                            If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" And DtFMonthDate.Rows.Count = 1 Then
    ''                                dr("ftoken") = DtFMonthDate.Rows(0)("token")
    ''                                dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
    ''                            ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" And DtFMonthDate.Rows.Count = 2 Then
    ''                                dr("ftoken") = DtFMonthDate.Rows(1)("token")
    ''                                dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
    ''                            ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" And DtFMonthDate.Rows.Count = 3 Then
    ''                                dr("ftoken") = DtFMonthDate.Rows(2)("token")
    ''                                dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
    ''                            End If
    ''                        End If
    ''                    End If
    ''                    dr("month") = (Format(CDate(dr("fut_mdate")), "MMM yy"))

    ''                    dr("status") = 1
    ''                    dr("theta") = 0
    ''                    dr("thetaval") = 0
    ''                    dr("vega") = 0
    ''                    dr("vgval") = 0
    ''                    dr("vgval1") = 0
    ''                    dr("gamma") = 0
    ''                    dr("gmval") = 0
    ''                    dr("gmval1") = 0
    ''                    dr("volga") = 0
    ''                    dr("volgaval") = 0
    ''                    dr("volgaval1") = 0
    ''                    dr("vanna") = 0
    ''                    dr("vannaval") = 0
    ''                    dr("vannaval1") = 0
    ''                    If CLng(dr("tokanno")) <> 0 Then
    ''                        prExp = 0
    ''                        toExp = 0
    ''                        Call cal_position_expense(dr, prExp, toExp, "CURR")
    ''                        dr("prExp") = prExp
    ''                        dr("toExp") = toExp
    ''                        dr("totExp") = -(prExp + toExp)
    ''                        dtable.Rows.Add(dr)
    ''                    End If
    ''                Else
    ''                    dr = dtable.NewRow()
    ''                    dr("strikes") = Val(drow("strikerate"))
    ''                    dr("prqty") = Val(IIf(IsDBNull(table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date < '" & Now.Date & "'")), 0, table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date < '" & Now.Date & "'")))
    ''                    dr("toqty") = Val(IIf(IsDBNull(table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date >= '" & Now.Date & "' ")), 0, table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date >= '" & Now.Date & "' ")))
    ''                    dr("units") = Val(drow("qty"))
    ''                    dr("traded") = Val(drow("rate"))
    ''                    dr("last") = 0
    ''                    dr("last1") = 0
    ''                    dr("lv") = 0
    ''                    dr("lv1") = 0
    ''                    dr("cp") = drow("cp")
    ''                    REM Chnage By Alpesh For Delta
    ''                    dr("delta") = 0
    ''                    dr("deltaval") = 0
    ''                    dr("deltaval1") = 0
    ''                    If Not IsDBNull(drow("cp")) Then
    ''                        If drow("cp") = "C" Then
    ''                            dr("Cp") = "C"
    ''                        ElseIf drow("cp") = "P" Then
    ''                            dr("Cp") = "P"
    ''                        Else
    ''                            dr("Cp") = "F"
    ''                            REM Chnage By Alpesh For Currency         
    ''                            dr("delta") = 1
    ''                            dr("deltaval") = Val(drow("qty"))
    ''                            dr("deltaval1") = Val(drow("qty"))
    ''                        End If
    ''                    Else
    ''                        dr("Cp") = "F"
    ''                        REM Chnage By Alpesh For Currency         
    ''                        dr("delta") = 1
    ''                        dr("deltaval") = Val(drow("qty"))
    ''                        dr("deltaval1") = Val(drow("qty"))
    ''                    End If
    ''                    dr("company") = CStr(drow("company"))
    ''                    dr("script") = CStr(drow("script"))
    ''                    dr("mdate") = CDate(drow("mdate"))
    ''                    dr("fut_mdate") = CDate(drow("mdate"))
    ''                    dr("entrydate") = Today
    ''                    dr("token1") = drow("token1")
    ''                    dr("isliq") = CBool(drow("isliq"))
    ''                    dr("tokanno") = Val(Currencymaster.Compute("max(token)", "script='" & drow("script") & "'").ToString)
    ''                    dr("ftoken") = Val(Currencymaster.Compute("max(token)", "Symbol='" & dr("company") & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# and option_type='XX'").ToString)
    ''                    If dr("ftoken") = 0 Then
    ''                        Dim DtFMonthDate As DataTable = New DataView(Currencymaster, "Symbol='" & drow("company") & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    ''                        If DtFMonthDate.Rows.Count > 0 Then
    ''                            If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" And DtFMonthDate.Rows.Count = 1 Then
    ''                                dr("ftoken") = DtFMonthDate.Rows(0)("token")
    ''                                dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
    ''                            ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" And DtFMonthDate.Rows.Count = 2 Then
    ''                                dr("ftoken") = DtFMonthDate.Rows(1)("token")
    ''                                dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
    ''                            ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" And DtFMonthDate.Rows.Count = 3 Then
    ''                                dr("ftoken") = DtFMonthDate.Rows(2)("token")
    ''                                dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
    ''                            End If
    ''                        End If
    ''                    End If
    ''                    dr("month") = (Format(CDate(dr("fut_mdate")), "MMM yy"))
    ''                    dr("status") = 1
    ''                    dr("theta") = 0
    ''                    dr("thetaval") = 0
    ''                    dr("vega") = 0
    ''                    dr("vgval") = 0
    ''                    dr("vgval1") = 0
    ''                    dr("gamma") = 0
    ''                    dr("gmval") = 0
    ''                    dr("gmval1") = 0
    ''                    dr("volga") = 0
    ''                    dr("volgaval") = 0
    ''                    dr("volgaval1") = 0
    ''                    dr("vanna") = 0
    ''                    dr("vannaval") = 0
    ''                    dr("vannaval1") = 0
    ''                    If CLng(dr("tokanno")) <> 0 Then
    ''                        prExp = 0
    ''                        toExp = 0
    ''                        Call cal_position_expense(dr, prExp, toExp, "CURR")
    ''                        dr("prExp") = prExp
    ''                        dr("toExp") = toExp
    ''                        dr("totExp") = -(prExp + toExp)
    ''                        dtable.Rows.Add(dr)
    ''                    End If
    ''                End If
    ''            End If
    ''        Next
    ''    Catch ex As Exception
    ''        MsgBox(ex.ToString)
    ''    End Try
    ''End Sub

    ''' <summary>
    ''' While Application Cresh  To Creating New maintable 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub fill_Currency()
        Try
            Dim dr As DataRow
            Dim count As Integer
            Dim ar As New ArrayList
            Dim table As New DataTable
            'table = New DataTable
            GdtCurrencyTrades = objTrad.select_Currency_Trading
            table = GdtCurrencyTrades 'objTrad.Trading


            Dim prExp, toExp As Double
            For Each drow As DataRow In table.Rows
                If Not ar.Contains(drow("script").ToString.Trim.ToUpper & drow("company").ToString.Trim.ToUpper) Then
                    ar.Add(drow("script").ToString.Trim.ToUpper & drow("company").ToString.Trim.ToUpper)
                    count = CInt(table.Compute("count(script)", "script='" & drow("script") & "' And company='" & drow("company") & "'"))
                    drow("script") = drow("script").ToString.Trim
                    drow("company") = drow("company").ToString.Trim
                    If count > 1 Then
                        Dim brate As Double = 0
                        Dim srate As Double = 0
                        dr = dtable.NewRow()
                        dr("strikes") = Val(drow("strikerate"))
                        dr("prqty") = Val(table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date < #" & fDate(Now.Date) & "# And company='" & drow("company") & "'").ToString)
                        dr("toqty") = Val(table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date >= #" & fDate(Now.Date) & "# And company='" & drow("company") & "'").ToString)
                        dr("units") = Val(table.Compute("sum(qty)", "script='" & drow("script") & "' And company='" & drow("company") & "'"))


                        srate = Math.Abs(Val(IIf(IsDBNull(table.Compute("sum(tot)", "script='" & drow("script") & "' and tot < 0 And company='" & drow("company") & "'")), 0, table.Compute("sum(tot)", "script='" & drow("script") & "' and tot < 0  And company='" & drow("company") & "'"))))
                        brate = Val(IIf(IsDBNull(table.Compute("sum(tot)", "script='" & drow("script") & "' and tot > 0 And company='" & drow("company") & "'")), 0, table.Compute("sum(tot)", "script='" & drow("script") & "' and tot > 0  And company='" & drow("company") & "'")))
                        If Val(dr("units")) = 0 Then
                            dr("traded") = Val((brate - srate))
                        Else
                            dr("traded") = Val((brate - srate) / Val(dr("units"))) ' Math.Round(val((brate - srate) / val(dr("units"))), 2)
                        End If
                        dr("last") = 0
                        dr("last1") = 0
                        dr("lv") = 0
                        dr("MktVol") = 0
                        dr("lv1") = 0
                        dr("cp") = drow("cp")
                        REM Chnage By Alpesh For Delta
                        dr("delta") = 0
                        dr("deltaval") = 0
                        dr("deltaval1") = 0
                        If Not IsDBNull(drow("cp")) Then
                            If drow("cp") = "C" Then
                                dr("Cp") = "C"
                            ElseIf drow("cp") = "P" Then
                                dr("Cp") = "P"
                            Else
                                dr("Cp") = "F"
                                REM Chnage By Alpesh For Currency         
                                dr("delta") = 1
                                dr("deltaval") = Val(dr("units"))
                                dr("deltaval1") = Val(dr("units"))
                            End If
                        Else
                            dr("Cp") = "F"
                            REM Chnage By Alpesh For Currency         
                            dr("delta") = 1
                            dr("deltaval") = Val(dr("units"))
                            dr("deltaval1") = Val(dr("units"))
                        End If
                        dr("company") = CStr(drow("company"))
                        dr("script") = CStr(drow("script"))
                        dr("mdate") = CDate(drow("mdate"))
                        dr("fut_mdate") = CDate(drow("mdate"))
                        dr("entrydate") = Today
                        dr("token1") = drow("token1")
                        dr("isliq") = CBool(drow("isliq"))
                        dr("tokanno") = IIf(IsDBNull(Currencymaster.Compute("max(token)", "script='" & drow("script") & "'")), 0, Currencymaster.Compute("max(token)", "script='" & drow("script") & "'"))
                        dr("ftoken") = IIf(IsDBNull(Currencymaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# and option_type='XX'")), 0, Currencymaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# and option_type='XX'"))
                        If dr("ftoken") = 0 Then

                            Dim DtFMonthDate1 As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'and expdate1>=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If DtFMonthDate1.Rows.Count > 0 Then
                                dr("ftoken") = DtFMonthDate1.Rows(0)("token")
                                dr("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                            Else
                                Dim DtFMonthDate As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(drow("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                                If DtFMonthDate.Rows.Count > 0 Then
                                    If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" And DtFMonthDate.Rows.Count = 1 Then
                                        dr("ftoken") = DtFMonthDate.Rows(0)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" And DtFMonthDate.Rows.Count = 2 Then
                                        dr("ftoken") = DtFMonthDate.Rows(1)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" And DtFMonthDate.Rows.Count = 3 Then
                                        dr("ftoken") = DtFMonthDate.Rows(2)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    End If
                                End If
                            End If


                        End If
                        dr("month") = (Format(CDate(dr("fut_mdate")), "MMM yy"))

                        dr("status") = 1
                        dr("theta") = 0
                        dr("thetaval") = 0
                        dr("vega") = 0
                        dr("vgval") = 0
                        dr("vgval1") = 0
                        dr("gamma") = 0
                        dr("gmval") = 0
                        dr("gmval1") = 0
                        dr("volga") = 0
                        dr("volgaval") = 0
                        dr("volgaval1") = 0
                        dr("vanna") = 0
                        dr("vannaval") = 0
                        dr("vannaval1") = 0
                        dr("IsVolFix") = False

                        dr("DeltaN") = 0
                        dr("GammaN") = 0
                        dr("VegaN") = 0
                        dr("ThetaN") = 0
                        dr("VolgaN") = 0
                        dr("VannaN") = 0

                        dr("IsCalc") = True

                        REM For ProffitDiff    By Viral 01-07-11

                        If DtAnalysis.Rows.Count > 0 Then
                            dr("preQty") = Val(DtAnalysis.Compute("Max(preQty)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preDate") = CDate(IIf(IsDBNull(DtAnalysis.Compute("Max(PreDate)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'")) = True, Now.Date, DtAnalysis.Compute("Max(PreDate)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'")))
                            dr("preSpot") = Val(DtAnalysis.Compute("Max(PreSpot)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preVol") = Val(DtAnalysis.Compute("Max(PreVol)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preDelVal") = Val(DtAnalysis.Compute("Max(PreDelVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preVegVal") = Val(DtAnalysis.Compute("Max(PreVegVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preTheVal") = Val(DtAnalysis.Compute("Max(PreTheVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")

                            dr("curSpot") = Val(DtAnalysis.Compute("Max(curSpot)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curVol") = Val(DtAnalysis.Compute("Max(curVol)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curDelVal") = Val(DtAnalysis.Compute("Max(curDelVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curVegVal") = Val(DtAnalysis.Compute("Max(curVegVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curTheVal") = Val(DtAnalysis.Compute("Max(curTheVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")

                            dr("preTotalMTM") = Val(DtAnalysis.Compute("Max(preTotalMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preGrossMTM") = Val(DtAnalysis.Compute("Max(preGrossMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curTotalMTM") = Val(DtAnalysis.Compute("Max(curTotalMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curGrossMTM") = Val(DtAnalysis.Compute("Max(curGrossMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")

                        Else
                            dr("preQty") = 0
                            dr("preDate") = Now.Date
                            dr("preSpot") = 0
                            dr("preVol") = 0
                            dr("preDelVal") = 0
                            dr("preVegVal") = 0
                            dr("preTheVal") = 0

                            dr("curSpot") = 0
                            dr("curVol") = 0
                            dr("curDelVal") = 0
                            dr("curVegVal") = 0
                            dr("curTheVal") = 0

                            dr("preTotalMTM") = 0
                            dr("preGrossMTM") = 0
                            dr("curTotalMTM") = 0
                            dr("curGrossMTM") = 0

                        End If
                        If CLng(dr("tokanno")) <> 0 Then
                            prExp = 0
                            toExp = 0
                            Call cal_position_expense(dr, prExp, toExp, "CURR")
                            dr("prExp") = prExp
                            dr("toExp") = toExp
                            dr("totExp") = -(prExp + toExp)
                            dtable.Rows.Add(dr)
                        End If
                    Else
                        dr = dtable.NewRow()
                        dr("strikes") = Val(drow("strikerate"))
                        dr("prqty") = Val(IIf(IsDBNull(table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date < #" & fDate(Now.Date) & "# And company='" & drow("company") & "'")), 0, table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date < #" & fDate(Now.Date) & "# And company='" & drow("company") & "'")))
                        dr("toqty") = Val(IIf(IsDBNull(table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date >= #" & fDate(Now.Date) & "#  And company='" & drow("company") & "'")), 0, table.Compute("sum(qty)", "script='" & drow("script") & "' and entry_date >= #" & fDate(Now.Date) & "# And company='" & drow("company") & "'")))
                        dr("units") = Val(drow("qty"))
                        dr("traded") = Val(drow("rate"))
                        dr("last") = 0
                        dr("last1") = 0
                        dr("lv") = 0
                        dr("MktVol") = 0
                        dr("lv1") = 0
                        dr("cp") = drow("cp")
                        REM Chnage By Alpesh For Delta
                        dr("delta") = 0
                        dr("deltaval") = 0
                        dr("deltaval1") = 0
                        If Not IsDBNull(drow("cp")) Then
                            If drow("cp") = "C" Then
                                dr("Cp") = "C"
                            ElseIf drow("cp") = "P" Then
                                dr("Cp") = "P"
                            Else
                                dr("Cp") = "F"
                                REM Chnage By Alpesh For Currency         
                                dr("delta") = 1
                                dr("deltaval") = Val(dr("units"))
                                dr("deltaval1") = Val(dr("units"))
                            End If
                        Else
                            dr("Cp") = "F"
                            REM Chnage By Alpesh For Currency         
                            dr("delta") = 1
                            dr("deltaval") = Val(dr("units"))
                            dr("deltaval1") = Val(dr("units"))
                        End If
                        dr("company") = CStr(drow("company"))
                        dr("script") = CStr(drow("script"))
                        dr("mdate") = CDate(drow("mdate"))
                        dr("mdate_months") = (CDate(dr("mdate")).Year * 12) + (CDate(dr("mdate")).Month)
                        dr("fut_mdate") = CDate(drow("mdate"))
                        dr("entrydate") = Today
                        dr("token1") = drow("token1")
                        dr("isliq") = CBool(drow("isliq"))
                        dr("tokanno") = Val(Currencymaster.Compute("max(token)", "script='" & drow("script") & "'").ToString)
                        dr("ftoken") = Val(Currencymaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# and option_type='XX'").ToString)
                        If dr("ftoken") = 0 Then


                            Dim DtFMonthDate1 As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'and expdate1>=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If DtFMonthDate1.Rows.Count > 0 Then
                                dr("ftoken") = DtFMonthDate1.Rows(0)("token")
                                dr("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                            Else
                                Dim DtFMonthDate As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(drow("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                                If DtFMonthDate.Rows.Count > 0 Then
                                    If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" And DtFMonthDate.Rows.Count = 1 Then
                                        dr("ftoken") = DtFMonthDate.Rows(0)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" And DtFMonthDate.Rows.Count = 2 Then
                                        dr("ftoken") = DtFMonthDate.Rows(1)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" And DtFMonthDate.Rows.Count = 3 Then
                                        dr("ftoken") = DtFMonthDate.Rows(2)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    End If
                                End If
                            End If



                        End If
                        dr("month") = (Format(CDate(dr("fut_mdate")), "MMM yy"))
                        dr("status") = 1
                        dr("theta") = 0
                        dr("thetaval") = 0
                        dr("vega") = 0
                        dr("vgval") = 0
                        dr("vgval1") = 0
                        dr("gamma") = 0
                        dr("gmval") = 0
                        dr("gmval1") = 0
                        dr("volga") = 0
                        dr("volgaval") = 0
                        dr("volgaval1") = 0
                        dr("vanna") = 0
                        dr("vannaval") = 0
                        dr("vannaval1") = 0
                        dr("IsVolFix") = False

                        dr("DeltaN") = 0
                        dr("GammaN") = 0
                        dr("VegaN") = 0
                        dr("ThetaN") = 0
                        dr("VolgaN") = 0
                        dr("VannaN") = 0

                        dr("IsCalc") = True
                        REM For ProffitDiff    By Viral 01-07-11
                        If DtAnalysis.Rows.Count > 0 Then
                            dr("preQty") = Val(DtAnalysis.Compute("Max(preQty)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preDate") = CDate(IIf(IsDBNull(DtAnalysis.Compute("Max(PreDate)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'")) = True, Now.Date, DtAnalysis.Compute("Max(PreDate)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'")))
                            dr("preSpot") = Val(DtAnalysis.Compute("Max(PreSpot)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preVol") = Val(DtAnalysis.Compute("Max(PreVol)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preDelVal") = Val(DtAnalysis.Compute("Max(PreDelVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preVegVal") = Val(DtAnalysis.Compute("Max(PreVegVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preTheVal") = Val(DtAnalysis.Compute("Max(PreTheVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")

                            dr("curSpot") = Val(DtAnalysis.Compute("Max(curSpot)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curVol") = Val(DtAnalysis.Compute("Max(curVol)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curDelVal") = Val(DtAnalysis.Compute("Max(curDelVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curVegVal") = Val(DtAnalysis.Compute("Max(curVegVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curTheVal") = Val(DtAnalysis.Compute("Max(curTheVal)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")

                            dr("preTotalMTM") = Val(DtAnalysis.Compute("Max(preTotalMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("preGrossMTM") = Val(DtAnalysis.Compute("Max(preGrossMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curTotalMTM") = Val(DtAnalysis.Compute("Max(curTotalMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")
                            dr("curGrossMTM") = Val(DtAnalysis.Compute("Max(curGrossMTM)", "tokanno=" & dr("tokanno") & " And company='" & dr("company") & "'") & "")

                        Else
                            dr("preQty") = 0
                            dr("preDate") = Now.Date
                            dr("preSpot") = 0
                            dr("preVol") = 0
                            dr("preDelVal") = 0
                            dr("preVegVal") = 0
                            dr("preTheVal") = 0

                            dr("curSpot") = 0
                            dr("curVol") = 0
                            dr("curDelVal") = 0
                            dr("curVegVal") = 0
                            dr("curTheVal") = 0

                            dr("preTotalMTM") = 0
                            dr("preGrossMTM") = 0
                            dr("curTotalMTM") = 0
                            dr("curGrossMTM") = 0
                        End If

                        If CLng(dr("tokanno")) <> 0 Then
                            prExp = 0
                            toExp = 0
                            Call cal_position_expense(dr, prExp, toExp, "CURR")
                            dr("prExp") = prExp
                            dr("toExp") = toExp
                            dr("totExp") = -(prExp + toExp)
                            dtable.Rows.Add(dr)
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub SetPreData()
        Dim objAna As New analysisprocess
        DtAnalysis = New DataTable
        objAna.sel_analysis(DtAnalysis)
        If PREDATA_AUTO_OFF = 1 Then Exit Sub
        Dim curDate As Date, sysDate As Date
        sysDate = Date.Now.Date
        If DtAnalysis.Rows.Count > 0 Then
            curDate = CDate(DtAnalysis.Compute("Max(entrydate)", ""))
        Else
            curDate = Date.Now.Date
        End If
        If curDate < sysDate Then
            objAna.UpdPreData_analysis()
        End If
    End Sub
    Public Sub set_importposition_flag()
        objTrad.Uid = GdtSettings.Select("SettingName='ISIMPORTPOSITION'")(0).Item("Uid")
        objTrad.SettingName = "ISIMPORTPOSITION"
        objTrad.SettingKey = 1
        objTrad.Update_setting()
        GdtSettings = objTrad.Settings
    End Sub

    Public Sub fill_equity_dtable()

        Dim ttik As Int64 = DateTime.Now.Ticks
        Write_TimeLog1("mainmodule-> Start Function fill_equity_dtable")
        Dim objAna As New analysisprocess
        Dim detable As New DataTable
        Dim dr As DataRow
        Call SetPreData()
        RECALCULATE_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RECALCULATE_POSITION'").ToString) '0
        ISIMPORTPOSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='ISIMPORTPOSITION'").ToString)
        If ISIMPORTPOSITION = 1 And RECALCULATE_POSITION = 0 Then
            objTrad.Uid = GdtSettings.Select("SettingName='ISIMPORTPOSITION'")(0).Item("Uid")
            objTrad.SettingName = "ISIMPORTPOSITION"
            objTrad.SettingKey = 0
            objTrad.Update_setting()
            GdtSettings = objTrad.Settings
            GoTo lblAppCrash

        End If
        If RECALCULATE_POSITION = 1 Then
            If GdtSettings.Select("SettingName='APPLICATION_CLOSING_FLAG'").Length = 0 Then
                objTrad.SettingName = "APPLICATION_CLOSING_FLAG"
                objTrad.SettingKey = "FALSE"
                objTrad.Insert_setting()
                GdtSettings = objTrad.Settings
                GoTo lblAppCrash
            ElseIf GdtSettings.Select("SettingName='APPLICATION_CLOSING_FLAG'")(0).Item("SettingKey") = "FALSE" Then
                GoTo lblAppCrash
            Else
                objTrad.Uid = GdtSettings.Select("SettingName='APPLICATION_CLOSING_FLAG'")(0).Item("Uid")
                objTrad.SettingName = "APPLICATION_CLOSING_FLAG"
                objTrad.SettingKey = "FALSE"
                objTrad.Update_setting()
                GdtSettings = objTrad.Settings
            End If
        Else
            objTrad.Uid = GdtSettings.Select("SettingName='APPLICATION_CLOSING_FLAG'")(0).Item("Uid")
            objTrad.SettingName = "APPLICATION_CLOSING_FLAG"
            objTrad.SettingKey = "FALSE"
            objTrad.Update_setting()
            GdtSettings = objTrad.Settings
        End If
        G_DTExpenseData = objTrad.Select_Expense_Data

        'if todays net position is stored 
        detable = objAna.sel_analysis
        Dim drdetable As DataRow() = detable.Select("status=1", "company")
        If detable.Compute("count(tokanno)", "entrydate=#" & Format(Today, "dd-MMM-yyyy") & "#") > 0 Then
            maintable.Rows.Clear()
            For Each drow As DataRow In drdetable
                dr = maintable.NewRow()
                dr("strikes") = drow("strikerate")
                dr("cp") = drow("CP")
                dr("prqty") = drow("prqty")
                dr("toqty") = drow("toqty")
                dr("units") = Val(drow("qty"))
                dr("traded") = Val(drow("rate"))
                dr("last") = drow("ltp")
                dr("last1") = drow("ltp1")
                dr("lv") = drow("vol")
                dr("MktVol") = drow("MktVol")
                dr("exchange") = drow("exchange")
                dr("lv1") = 0 ' drow("vol1")
                If drow("Cp") = "F" Then
                    dr("delta") = 1
                    dr("deltaval") = Val(drow("qty"))
                    dr("deltaval1") = Val(drow("qty"))
                ElseIf drow("Cp") = "E" Then
                    dr("delta") = 1
                    dr("deltaval") = Val(drow("qty"))
                    dr("deltaval1") = Val(drow("qty"))
                Else
                    dr("delta") = 0
                    dr("deltaval") = 0
                    dr("deltaval1") = 0
                End If
                Dim drGdtCurrencyTrades As DataRow() = GdtCurrencyTrades.Select("Script='" & drow("script") & "' And company='" & drow("company") & "'")
                Dim drcpfmaster As DataRow() = cpfmaster.Select("script='" & drow("script") & "'")

                'If GdtCurrencyTrades.Select("Script='" & drow("script") & "' And company='" & drow("company") & "'").Length > 0 Then
                If drGdtCurrencyTrades.Length > 0 Then
                    dr("IsCurrency") = True
                    dr("Lots") = dr("units") / Currencymaster.Compute("MAX(multiplier)", "Script='" & drow("script") & "'")
                    'ElseIf cpfmaster.Select("script='" & drow("script") & "'").Length > 0 Then
                ElseIf drcpfmaster.Length > 0 Then
                    dr("IsCurrency") = False
                    'divyesh
                    dr("Lots") = Val(dr("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                    REM Change By Alpesh 
                    'ElseIf GdtFOTrades.Select("script='" & drow("script") & "'").Length > 0 Then
                    '    dr("IsCurrency") = False
                    '    'divyesh
                    '    dr("Lots") = Val(dr("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                    '    REM End 
                Else
                    dr("IsCurrency") = False
                    dr("LOts") = 0
                End If

                dr("theta") = 0
                dr("thetaval") = 0
                dr("vega") = 0
                dr("vgval") = 0
                dr("gamma") = 0
                dr("gmval") = 0
                dr("volga") = 0
                dr("volgaval") = 0
                dr("vanna") = 0
                dr("vannaval") = 0
                dr("company") = CStr(drow("company"))
                dr("script") = CStr(drow("script"))
                dr("mdate") = drow("mdate")
                dr("fut_mdate") = drow("mdate")
                dr("mdate_months") = (CDate(drow("mdate")).Year * 12) + (CDate(drow("mdate")).Month)
                If Not drow("cp") = "E" Then
                    dr("month") = (Format(CDate(drow("mdate")), "MMM yy"))
                End If
                dr("entrydate") = CDate(drow("entrydate")).Date
                dr("token1") = drow("token1")
                dr("isliq") = drow("isliq")
                dr("tokanno") = CStr(drow("tokanno"))
                '========================================keval
                dr("asset_tokan") = Val(cpfmaster.Compute("max(Asset_tokan)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# and option_type='XX'").ToString)
                '===============================================
                'modified by mahesh, becuase in case of far month token we can not get the ftoken
                If dr("IsCurrency") = False Then
                    'Jignesh
                    If drow("cp") = "E" Then
                        If cpfmaster.Select("Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX' AND expdate1>=#" & Format(Today, "dd-MMM-yyyy") & "#").Length > 0 Then
                            dr("fut_mdate") = cpfmaster.Compute("MIN(expdate1)", "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'")
                            dr("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                        Else
                            dr("fut_mdate") = Today
                            dr("ftoken") = 0
                        End If
                    Else
                        dr("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "MM/dd/yyyy") & "# AND option_type='XX'").ToString)
                        If dr("ftoken") = 0 Then
                            Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'and expdate1>=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If DtFMonthDate1.Rows.Count > 0 Then
                                dr("ftoken") = DtFMonthDate1.Rows(0)("token")
                                dr("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                            Else
                                Dim DtFMonthDate As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                                If DtFMonthDate.Rows.Count > 0 Then
                                    If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(0)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(1)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(2)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    End If
                                End If
                            End If


                        End If
                    End If
                ElseIf dr("IsCurrency") = True Then REM Is Currency Position
                    'Jignesh
                    dr("ftoken") = Val(Currencymaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                    If dr("ftoken") = 0 Then
                        Dim DtFMonthDate1 As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'and expdate1>=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                        If DtFMonthDate1.Rows.Count > 0 Then
                            dr("ftoken") = DtFMonthDate1.Rows(0)("token")
                            dr("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                        Else
                            Dim DtFMonthDate As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If DtFMonthDate.Rows.Count > 0 Then
                                If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                                    dr("ftoken") = DtFMonthDate.Rows(0)("token")
                                    dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                                ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                                    dr("ftoken") = DtFMonthDate.Rows(1)("token")
                                    dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                                ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                                    dr("ftoken") = DtFMonthDate.Rows(2)("token")
                                    dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                End If
                            End If
                        End If

                    End If
                End If
                dr("toatp") = 0
                dr("pratp") = 0
                dr("thetaval1") = 0
                dr("vgval1") = 0
                dr("gmval1") = 0
                dr("volgaval1") = 0
                dr("vannaval1") = 0
                dr("status") = drow("status")
                If dr("mdate") = Today.Date And dr("cp") <> "E" Then
                    dr("deltaval1") = 0
                    dr("thetaval1") = 0
                    dr("vgval1") = 0
                    dr("gmval1") = 0
                    dr("volgaval1") = 0
                    dr("vannaval1") = 0
                End If
                dr("remarks") = drow("remarks")
                dr("prExp") = drow("prExp")
                dr("toExp") = drow("toExp")
                dr("totExp") = -(Val(drow("prExp").ToString) + Val(drow("toExp").ToString))
                dr("IsVolFix") = CBool(drow("IsVolFix"))

                dr("DeltaN") = 0
                dr("GammaN") = 0
                dr("VegaN") = 0
                dr("ThetaN") = 0
                dr("VolgaN") = 0
                dr("VannaN") = 0

                dr("IsCalc") = drow("IsCalc")

                REM For ProffitDiff    By Viral 01-07-11
                dr("preQty") = drow("preQty")
                dr("preDate") = drow("preDate")
                dr("preSpot") = drow("preSpot")
                dr("preVol") = drow("preVol")
                dr("preDelVal") = drow("preDelVal")
                dr("preVegVal") = drow("preVegVal")
                dr("preTheVal") = drow("preTheVal")
                dr("curSpot") = drow("curSpot")
                dr("curVol") = drow("curVol")
                dr("curDelVal") = drow("curDelVal")
                dr("curVegVal") = drow("curVegVal")
                dr("curTheVal") = drow("curTheVal")

                dr("preTotalMTM") = drow("preTotalMTM")
                dr("preGrossMTM") = drow("preGrossMTM")
                dr("curTotalMTM") = drow("curTotalMTM")
                dr("curGrossMTM") = drow("curGrossMTM")

                maintable.Rows.Add(dr)
            Next
            'if yesterday net position and ltp stored
        ElseIf detable.Compute("count(tokanno)", "entrydate='" & CDate(DateAdd(DateInterval.Day, -1, Now.Date)) & "'") > 0 Then
            maintable.Rows.Clear()
            For Each drow As DataRow In detable.Select("status=1", "company")
                dr = maintable.NewRow()
                dr("strikes") = drow("strikerate")
                dr("cp") = drow("CP")
                dr("prqty") = drow("prqty")
                dr("toqty") = drow("toqty")
                dr("units") = Val(drow("qty"))
                dr("traded") = Val(drow("rate"))
                dr("last") = drow("ltp")
                dr("last1") = drow("ltp1")
                dr("lv") = drow("vol")
                dr("MktVol") = drow("MktVol")
                'dr("Vol_Diff") = drow("MktVol") - drow("vol")
                dr("exchange") = drow("exchange")
                dr("lv1") = 0 'drow("vol1")
                If drow("Cp") = "F" Then
                    dr("delta") = 1
                    dr("deltaval") = Val(drow("qty"))
                    dr("deltaval1") = Val(drow("qty"))
                ElseIf drow("Cp") = "E" Then
                    dr("delta") = 1
                    dr("deltaval") = Val(drow("qty"))
                    dr("deltaval1") = Val(drow("qty"))
                Else
                    dr("delta") = 0
                    dr("deltaval") = 0
                    dr("deltaval1") = 0
                End If

                dr("theta") = 0
                dr("thetaval") = 0
                dr("vega") = 0
                dr("vgval") = 0
                dr("gamma") = 0
                dr("gmval") = 0
                dr("volga") = 0
                dr("volgaval") = 0
                dr("vanna") = 0
                dr("vannaval") = 0
                dr("company") = CStr(drow("company"))
                dr("script") = CStr(drow("script"))
                dr("mdate") = CDate(drow("mdate"))
                dr("fut_mdate") = CDate(drow("mdate"))
                dr("mdate_months") = (CDate(drow("mdate")).Year * 12) + (CDate(drow("mdate")).Month)
                If (dr("cp") <> "E") Then
                    dr("month") = Format(CDate(drow("mdate")), "MMM yy")
                End If
                dr("entrydate") = CDate(drow("entrydate")).Date
                dr("token1") = drow("token1")
                dr("isliq") = drow("isliq")
                dr("tokanno") = CStr(drow("tokanno"))
                ''divyesh

                If GdtCurrencyTrades.Select("Script='" & drow("script") & "' And company='" & drow("company") & "'").Length > 0 Then
                    dr("IsCurrency") = True
                    dr("Lots") = dr("units") / Currencymaster.Compute("MAX(multiplier)", "Script='" & drow("script") & "'")
                ElseIf cpfmaster.Select("script='" & drow("script") & "'").Length > 0 Then
                    dr("IsCurrency") = False
                    dr("Lots") = Val(dr("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                    REM Change By Alpesh
                    'ElseIf GdtFOTrades.Select("script='" & drow("script") & "'").Length > 0 Then
                    '    dr("IsCurrency") = False
                    '    dr("Lots") = Val(dr("units")) / GdtFOTrades.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                Else
                    dr("IsCurrency") = False
                    dr("LOts") = 0
                End If

                '===================================keval
                dr("asset_tokan") = CStr(drow("asset_tokan"))
                '=======================================================
                'modified by mahesh, becuase in case of far month token we can not get the ftoken
                If dr("IsCurrency") = False Then
                    'Jignesh
                    If drow("CP") = "E" Then
                        If cpfmaster.Select("Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'").Length > 0 Then
                            dr("fut_mdate") = cpfmaster.Compute("MIN(expdate1)", "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'")
                            dr("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                        Else
                            dr("fut_mdate") = Today
                            dr("ftoken") = 0
                        End If
                    Else
                        dr("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                        dr("fut_mdate") = dr("mdate")
                        If dr("ftoken") = 0 Then


                            Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'and expdate1>=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

                            If DtFMonthDate1.Rows.Count > 0 Then

                                If DtFMonthDate1.Rows.Count > 0 Then
                                    dr("ftoken") = DtFMonthDate1.Rows(0)("token")
                                    dr("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                                Else
                                    Dim DtFMonthDate As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                                    If GVarMaturity_Far_month = "CURRENT MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(0)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    ElseIf GVarMaturity_Far_month = "NEXT MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(1)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    ElseIf GVarMaturity_Far_month = "FAR MONTH" Then
                                        dr("ftoken") = DtFMonthDate.Rows(2)("token")
                                        dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    End If
                                End If

                            End If
                        End If

                    End If
                Else

                    'Jignesh
                    dr("ftoken") = Val(Currencymaster.Compute("max(token)", "Symbol='" & GetSymbol(dr("company")) & "' and expdate1=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                    dr("fut_mdate") = dr("mdate")
                    If dr("ftoken") = 0 Then
                        Dim DtFMonthDate1 As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'and expdate1>=#" & Format(dr("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                        If DtFMonthDate1.Rows.Count > 0 Then
                            If DtFMonthDate1.Rows.Count > 0 Then
                                dr("ftoken") = DtFMonthDate1.Rows(0)("token")
                                dr("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                            Else
                                Dim DtFMonthDate As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(dr("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                                If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                                    dr("ftoken") = DtFMonthDate.Rows(0)("token")
                                    dr("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                                ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                                    dr("ftoken") = DtFMonthDate.Rows(1)("token")
                                    dr("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                                ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                                    dr("ftoken") = DtFMonthDate.Rows(2)("token")
                                    dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                End If
                            End If

                        End If
                        'Else
                        'mrow("ftokan") = mrow("ftokan")
                    End If
                End If

                dr("toatp") = 0
                dr("pratp") = 0
                dr("thetaval1") = 0
                dr("vgval1") = 0
                dr("gmval1") = 0
                dr("volgaval1") = 0
                dr("vannaval1") = 0
                dr("status") = drow("status")
                If dr("mdate") = Today.Date And dr("cp") <> "E" Then
                    dr("deltaval1") = 0
                    dr("thetaval1") = 0
                    dr("vgval1") = 0
                    dr("gmval1") = 0
                    dr("volgaval1") = 0
                    dr("vannaval1") = 0
                End If
                dr("remarks") = drow("remarks")
                dr("prExp") = drow("prExp")
                dr("toExp") = drow("toExp")
                dr("totExp") = -(Val(drow("prExp").ToString) + Val(drow("toExp").ToString))
                dr("IsVolFix") = CBool(drow("IsVolFix"))

                dr("DeltaN") = 0
                dr("GammaN") = 0
                dr("VegaN") = 0
                dr("ThetaN") = 0
                dr("VolgaN") = 0
                dr("VannaN") = 0

                dr("IsCalc") = drow("IsCalc")

                REM For ProffitDiff    By Viral 01-07-11
                dr("preQty") = drow("preQty")
                dr("preDate") = drow("preDate")
                dr("preSpot") = drow("preSpot")
                dr("preVol") = drow("preVol")
                dr("preDelVal") = drow("preDelVal")
                dr("preVegVal") = drow("preVegVal")
                dr("preTheVal") = drow("preTheVal")
                dr("curSpot") = drow("curSpot")
                dr("curVol") = drow("curVol")
                dr("curDelVal") = drow("curDelVal")
                dr("curVegVal") = drow("curVegVal")
                dr("curTheVal") = drow("curTheVal")

                dr("preTotalMTM") = drow("preTotalMTM")
                dr("preGrossMTM") = drow("preGrossMTM")
                dr("curTotalMTM") = drow("curTotalMTM")
                dr("curGrossMTM") = drow("curGrossMTM")


                maintable.Rows.Add(dr)
            Next
        Else
lblAppCrash:

            Dim dtanalysis As DataTable = objAna.sel_analysis


            Call fill_table()
            Call fill_equity()
            Call fill_Currency()

            REM Calculate Expense  Alpesh 11/05/2011
            objTrad.Delete_Expense_Data_All()
            Call GSub_CalculateExpense(GdtFOTrades, "FO", True)
            'objTrad.Delete_Expense_Data_All()
            'objTrad.Insert_Expense_Data(G_DTExpenseData)
            Call GSub_CalculateExpense(GdtEQTrades, "EQ", True)
            'objTrad.Delete_Expense_Data_All()
            'objTrad.Insert_Expense_Data(G_DTExpenseData)
            Call GSub_CalculateExpense(GdtCurrencyTrades, "CURR", True)
            objTrad.Delete_Expense_Data_All()
            objTrad.Insert_Expense_Data(G_DTExpenseData)
            REM End
            '  objAna.process_data()

            'For Each erow As DataRow In New DataView(dtanalysis, "script='" & drow("script") & "' and cp not in('C','P') And company='" & drow("company") & "'", "", DataViewRowState.CurrentRows).ToTable(True, "entry_date").Rows   'dtdate.Rows

            'Next


            maintable = dtable


            For Each erow As DataRow In dtanalysis.Rows
                Dim str As String = erow("Script") + "_" + erow("Company")

                For Each erow1 As DataRow In maintable.Rows
                    Dim str1 As String = erow1("Script") + "_" + erow1("Company")
                    If (str = str1) Then

                        erow1("last") = erow("ltp")
                    End If

                Next
            Next

            maintable.AcceptChanges()


            If dtanalysis.Select("IsVolFix='" & True & "'").Length > 0 Then
                For Each erow As DataRow In dtanalysis.Rows
                    Dim str As String = erow("Script") + "_" + erow("Company")
                    Dim Isvol As String = erow("IsVolFix")
                    Dim vol As Double = erow("Vol")

                    For Each erow1 As DataRow In maintable.Rows
                        Dim str1 As String = erow1("Script") + "_" + erow1("Company")
                        If (str = str1) Then
                            erow1("IsVolFix") = Isvol
                            erow1("lv") = vol
                            erow1("MktVol") = erow("MktVol")
                            'erow1("last") = erow("ltp")
                        End If

                    Next
                Next

                maintable.AcceptChanges()
            End If
            'For Each erow As DataRow In New DataView(dtanalysis, "script='" & drow("script") & "' and cp not in('C','P') And company='" & drow("company") & "'", "", DataViewRowState.CurrentRows).ToTable(True, "entry_date").Rows   'dtdate.Rows

            'Next
            For Each drow As DataRow In eqtable.Rows
                dr = maintable.NewRow()
                dr("strikes") = 0
                dr("cp") = "E"
                dr("prqty") = drow("prqty")
                dr("toqty") = drow("toqty")
                dr("units") = Val(drow("qty"))
                dr("traded") = Val(drow("rate"))
                ''divyesh
                dr("exchange") = drow("exchange")
                If GdtCurrencyTrades.Select("Script='" & drow("script") & "' And company='" & drow("company") & "'").Length > 0 Then
                    dr("IsCurrency") = True
                    dr("Lots") = dr("units") / Currencymaster.Compute("MAX(multiplier)", "Script='" & drow("script") & "'")
                ElseIf cpfmaster.Select("script='" & drow("script") & "'").Length > 0 Then
                    dr("IsCurrency") = False
                    dr("Lots") = Val(dr("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                Else
                    dr("IsCurrency") = False
                    dr("LOts") = 0
                End If

                dr("last") = 0

                dr("last1") = 0
                dr("lv") = 0
                dr("MktVol") = 0
                dr("lv1") = 0
                dr("delta") = 1
                dr("deltaval") = Val(drow("qty"))
                dr("theta") = 0
                dr("thetaval") = 0
                dr("vega") = 0
                dr("vgval") = 0
                dr("gamma") = 0
                dr("gmval") = 0
                dr("volga") = 0
                dr("volgaval") = 0
                dr("vanna") = 0
                dr("vannaval") = 0
                dr("company") = CStr(drow("company"))
                dr("script") = CStr(drow("script"))
                dr("mdate") = CDate(Format(CDate(Now.Date), "MMM/dd/yyyy"))
                dr("fut_mdate") = CDate(Format(CDate(Now.Date), "MMM/dd/yyyy"))
                'dr("mdate_months") = 0
                dr("mdate_months") = (CDate(dr("mdate")).Year * 12) + (CDate(dr("mdate")).Month)
                If (dr("cp") <> "E") Then dr("month") = (Format(CDate(Now.Date), "MMM yy"))
                dr("entrydate") = CDate(Format(CDate(drow("entrydate")), "MMM/dd/yyyy"))
                dr("token1") = 0
                dr("isliq") = False
                dr("tokanno") = CStr(drow("tokanno"))
                '=================================================
                dr("asset_tokan") = 0
                '===============================================
                dr("ftoken") = 0
                If (dr("cp") = "E") Then
                    Dim Drcpf() As DataRow = cpfmaster.Select("Symbol='" & GetSymbol(dr("company")) & "' AND InstrumentName IN ('FUTSTK','FUTIDX','FUTIVX')", "expDate1")
                    If Drcpf.Length > 0 Then
                        dr("ftoken") = Drcpf(0)("token")
                        dr("fut_mdate") = Drcpf(0)("expDate1")
                    Else
                        dr("ftoken") = 0
                        dr("fut_mdate") = Today
                    End If
                End If

                dr("toatp") = Val(drow("toatp"))
                dr("pratp") = Val(drow("pratp"))
                dr("deltaval1") = Val(drow("qty"))
                dr("thetaval1") = 0
                dr("vgval1") = 0
                dr("gmval1") = 0
                dr("volgaval1") = 0
                dr("vannaval1") = 0
                If dr("mdate") = Today.Date And dr("cp") <> "E" Then
                    dr("deltaval1") = 0
                    dr("thetaval1") = 0
                    dr("vgval1") = 0
                    dr("gmval1") = 0
                    dr("volgaval1") = 0
                    dr("vannaval1") = 0
                End If
                dr("totExp") = -(Val(drow("prExp").ToString) + Val(drow("toExp").ToString))
                dr("IsVolFix") = False

                dr("DeltaN") = 0
                dr("GammaN") = 0
                dr("VegaN") = 0
                dr("ThetaN") = 0
                dr("VolgaN") = 0
                dr("VannaN") = 0

                dr("IsCalc") = CBool(IIf(IsDBNull(drow("IsCalc")), "True", drow("IsCalc")))

                REM For ProffitDiff    By Viral 01-07-11
                If dtanalysis.Rows.Count > 0 Then
                    dr("preQty") = Val(dtanalysis.Compute("Max(preQty)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("preDate") = CDate(IIf(IsDBNull(dtanalysis.Compute("Max(PreDate)", "tokanno=" & dr("tokanno") & " And CP='E'")) = True, Now.Date, dtanalysis.Compute("Max(PreDate)", "tokanno=" & dr("tokanno") & " And CP='E'")))
                    dr("preSpot") = Val(dtanalysis.Compute("Max(PreSpot)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("preVol") = Val(dtanalysis.Compute("Max(PreVol)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("preDelVal") = Val(dtanalysis.Compute("Max(PreDelVal)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("preVegVal") = Val(dtanalysis.Compute("Max(PreVegVal)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("preTheVal") = Val(dtanalysis.Compute("Max(PreTheVal)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")

                    dr("curSpot") = Val(dtanalysis.Compute("Max(curSpot)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("curVol") = Val(dtanalysis.Compute("Max(curVol)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("curDelVal") = Val(dtanalysis.Compute("Max(curDelVal)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("curVegVal") = Val(dtanalysis.Compute("Max(curVegVal)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("curTheVal") = Val(dtanalysis.Compute("Max(curTheVal)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")

                    dr("preTotalMTM") = Val(dtanalysis.Compute("Max(preTotalMTM)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("preGrossMTM") = Val(dtanalysis.Compute("Max(preGrossMTM)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("curTotalMTM") = Val(dtanalysis.Compute("Max(curTotalMTM)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                    dr("curGrossMTM") = Val(dtanalysis.Compute("Max(curGrossMTM)", "tokanno=" & dr("tokanno") & " And CP='E'") & "")
                Else
                    dr("preQty") = 0
                    dr("preDate") = Now.Date
                    dr("preSpot") = 0
                    dr("preVol") = 0
                    dr("preDelVal") = 0
                    dr("preVegVal") = 0
                    dr("preTheVal") = 0

                    dr("curSpot") = 0
                    dr("curVol") = 0
                    dr("curDelVal") = 0
                    dr("curVegVal") = 0
                    dr("curTheVal") = 0

                    dr("preTotalMTM") = 0
                    dr("preGrossMTM") = 0
                    dr("curTotalMTM") = 0
                    dr("curGrossMTM") = 0
                End If
                If maintable.Select("Script='" & drow("script") & "' And company='" & drow("company") & "' AND exchange='" & drow("exchange") & "'").Length = 0 Then
                    maintable.Rows.Add(dr)
                End If

            Next

            REM Currency Position add into Maintable

        End If


        For Each DrMaintable As DataRow In maintable.Select("IsCurrency Is Null")
            If GdtCurrencyTrades.Select("Script='" & DrMaintable("script") & "' And company='" & DrMaintable("company") & "'").Length > 0 Then
                DrMaintable("IsCurrency") = True
                DrMaintable("Lots") = DrMaintable("units") / Currencymaster.Compute("MAX(multiplier)", "Script='" & DrMaintable("script") & "'")
            Else
                DrMaintable("IsCurrency") = False
                DrMaintable("Lots") = 0
            End If
        Next


        If mode = "Offline" Then
            If maintable.Rows.Count > 0 Then
                Dim objanalysisprocess As New analysisprocess
                If maintable.Compute("count(tokanno)", "last=0") > 0 Then

                    dtanalysisData = objanalysisprocess.sel_analysis()
                    objAna.get_ltp(maintable)
                End If
                'If Directory.Exists(mSPAN_path) Then
                '    'generate_SPAN_data(maintable)
                'End If
            End If
        End If
        Write_TimeLog1("mainmodule-> End Fun-Fil_equity_dtable" + "|" + TimeSpan.FromTicks(DateTime.Now.Ticks - ttik).TotalSeconds.ToString())
    End Sub
    Dim shObj As Object = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"))
    Sub UnZip1(ByVal SrcFile As String, ByVal DstFile As String, ByVal BufferSize As Integer)
        Dim outputZip As String = "output zip file path"
        Dim inputZip As String = SrcFile
        Dim inputFolder As String = "input folder path"
        Dim outputFolder As String = DstFile
        'Create directory in which you will unzip your items.
        IO.Directory.CreateDirectory(outputFolder)

        'Declare the folder where the items will be extracted.
        Dim output As Object = shObj.NameSpace((outputFolder))

        'Declare the input zip file.
        Dim input As Object = shObj.NameSpace((inputZip))

        'Extract the items from the zip file.
        output.CopyHere((input.Items), 4)

    End Sub
    'Public Function DownloadFile(ByVal FileNameToDownload As String) As String

    '    Dim ftpURL As String = "ftp://strategybuilder.finideas.com/Contract"
    '    'Host URL or address of the FTP serve
    '    Dim userName As String = "strategybuilder" ' "FI-strategybuilder"
    '    'User Name of the FTP server
    '    Dim password As String = "finideas#123"
    '    Dim tempDirPath As String = Application.StartupPath + "\Contract\"
    '    Dim ResponseDescription As String = ""
    '    Dim PureFileName As String = New FileInfo(FileNameToDownload).Name
    '    Dim DownloadedFilePath As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName
    '    Dim downloadUrl As String = [String].Format("{0}/{1}", ftpURL, FileNameToDownload)
    '    Dim req As FtpWebRequest = DirectCast(FtpWebRequest.Create(downloadUrl), FtpWebRequest)
    '    req.Method = WebRequestMethods.Ftp.DownloadFile
    '    req.Credentials = New NetworkCredential(userName, password)
    '    req.UseBinary = True
    '    req.Proxy = Nothing
    '    Try
    '        Dim response As FtpWebResponse = DirectCast(req.GetResponse(), FtpWebResponse)
    '        Dim stream As Stream = response.GetResponseStream()
    '        Dim buffer As Byte() = New Byte(2047) {}
    '        Dim fs As New FileStream(DownloadedFilePath, FileMode.Create)
    '        Dim ReadCount As Integer = stream.Read(buffer, 0, buffer.Length)
    '        While ReadCount > 0
    '            fs.Write(buffer, 0, ReadCount)
    '            ReadCount = stream.Read(buffer, 0, buffer.Length)
    '        End While
    '        ResponseDescription = response.StatusDescription
    '        fs.Close()
    '        stream.Close()
    '    Catch e As Exception
    '        Console.WriteLine(e.Message)
    '    End Try
    '    Return ResponseDescription
    'End Function


    Public Sub insert_EQTradeToGlobalTable(ByVal tedTable As DataTable)
        Dim dtrow As DataRow
        For Each tedrow As DataRow In tedTable.Rows
            dtrow = GdtEQTrades.NewRow
            dtrow("script") = tedrow("script")
            dtrow("company") = tedrow("company")
            dtrow("eq") = tedrow("eq")
            dtrow("qty") = tedrow("qty")
            dtrow("rate") = tedrow("rate")
            dtrow("entrydate") = tedrow("entrydate")
            dtrow("tot") = tedrow("qty") * tedrow("rate")
            dtrow("tot2") = tedrow("qty") * tedrow("rate")

            dtrow("entry_date") = CDate(tedrow("entrydate")).Date 'Format(CDate(tedrow("entrydate")), "MMM/dd/yyyy")
            dtrow("entryno") = tedrow("entryno")
            dtrow("orderno") = tedrow("orderno")
            dtrow("issummary") = True
            dtrow("isdisplay") = True
            dtrow("Dealer") = tedrow("Dealer")
            dtrow("exchange") = tedrow("exchange")
            GdtEQTrades.Rows.Add(dtrow)
        Next

        GdtEQTrades.AcceptChanges()
        REM Change By ALPESH After Refresh Trade Navigate Tab take time 
        GdtEQTrades.Select("script=''")
    End Sub
    Public Sub insert_FOTradeToGlobalTable(ByVal tedTable As DataTable)
        Dim tik As Long = System.Environment.TickCount
        Dim DRtedTable() As DataRow = tedTable.Select("")

        'For Each drow As DataRow In DRmaintable
        Dim temprow As DataRow

        'For Each tdrow As DataRow In tedTable.Rows
        For Each tdrow As DataRow In DRtedTable
            temprow = GdtFOTrades.NewRow
            temprow("entryno") = tdrow("EntryNo")
            temprow("instrumentname") = tdrow("Instrumentname")
            temprow("company") = tdrow("Company")
            temprow("mdate") = tdrow("MDate")
            temprow("strikerate") = tdrow("StrikeRate")
            temprow("cp") = tdrow("CP")
            temprow("qty") = tdrow("Qty")
            temprow("rate") = tdrow("rate")
            temprow("entrydate") = tdrow("entrydate")
            temprow("entry_date") = CDate(tdrow("entrydate")).Date 'Format(CDate(tdrow("entrydate")), "MMM/dd/yyyy")
            temprow("script") = tdrow("Script")
            temprow("token1") = tdrow("Token1")
            temprow("isliq") = tdrow("IsLiq")
            temprow("orderno") = tdrow("OrderNo")
            temprow("lActivityTime") = tdrow("lActivityTime")
            temprow("FileFlag") = tdrow("FileFlag")
            temprow("tot") = tdrow("Qty") * tdrow("rate")
            temprow("tot2") = tdrow("Qty") * (Val(tdrow("rate")) + Val(tdrow("StrikeRate")))
            temprow("issummary") = True
            temprow("isdisplay") = True
            temprow("Dealer") = tdrow("Dealer")
            temprow("exchange") = tdrow("exchange")

            GdtFOTrades.Rows.Add(temprow)
        Next
        GdtFOTrades.AcceptChanges()
        REM Change By ALPESH After Refresh Trade Navigate Tab take time 
        GdtFOTrades.Select("script=''")
        Write_TradeLog_viral("insert_FOTradeToGlobalTable", tik, System.Environment.TickCount)
    End Sub
    Public Sub insert_CurrencyTradeToGlobalTable(ByVal tedTable As DataTable)
        Dim temprow As DataRow
        For Each tdrow As DataRow In tedTable.Rows
            temprow = GdtCurrencyTrades.NewRow
            temprow("entryno") = tdrow("EntryNo")
            temprow("instrumentname") = tdrow("Instrumentname")
            temprow("company") = tdrow("Company")
            temprow("mdate") = tdrow("MDate")
            temprow("strikerate") = tdrow("StrikeRate")
            temprow("cp") = tdrow("CP")
            temprow("qty") = tdrow("Qty")
            temprow("rate") = tdrow("rate")
            temprow("entrydate") = tdrow("entrydate")
            temprow("entry_date") = CDate(tdrow("entrydate")).Date 'Format(CDate(tdrow("entrydate")), "MMM/dd/yyyy")
            temprow("script") = tdrow("Script")
            temprow("token1") = tdrow("Token1")
            temprow("isliq") = tdrow("IsLiq")
            temprow("orderno") = tdrow("OrderNo")
            temprow("lActivityTime") = tdrow("lActivityTime")
            temprow("FileFlag") = tdrow("FileFlag")
            temprow("tot") = tdrow("Qty") * tdrow("rate")
            temprow("tot2") = tdrow("Qty") * (Val(tdrow("rate")) + Val(tdrow("StrikeRate")))
            temprow("issummary") = True
            temprow("isdisplay") = True
            temprow("Dealer") = tdrow("Dealer")
            GdtCurrencyTrades.Rows.Add(temprow)
        Next
        GdtCurrencyTrades.AcceptChanges()
        REM Change By ALPESH After Refresh Trade Navigate Tab take time 
        GdtCurrencyTrades.Select("script=''")
    End Sub

    REM Calculate Previous balance 19-11-2010
    Public Sub cal_prebal_Outdated(ByVal date1 As Date, ByVal company As DataTable)
        Dim addprebal As New DataTable
        addprebal = New DataTable
        With addprebal.Columns
            .Add("tdate", GetType(Date))
            .Add("stbal", GetType(Double))
            .Add("futbal", GetType(Double))
            .Add("optbal", GetType(Double))
            .Add("company", GetType(String))
        End With
        Dim prow As DataRow

        Dim cpf As New DataTable
        Dim stk As New DataTable
        Dim currtrd As New DataTable
        Dim exptable As New DataTable

        ''divyesh 03-11-2010
        'Dim company As New DataTable
        'company = objTrad.Comapany

        ' exptable = objExp.select_exp

        cpf = GdtFOTrades 'objTrad.Trading
        stk = GdtEQTrades 'objTrad.select_equity
        ''divyesh
        currtrd = GdtCurrencyTrades

        'Dim dv As DataView = New DataView(cpf)
        'dv.RowFilter = "entry_date = #" & date1.Date & "#"
        'Dim dv1 As DataView = New DataView(stk)
        'dv1.RowFilter = "entry_date = #" & date1.Date & "#"
        Dim stexp, stexp1, ndst, dst, exppr, expto As Double

        For Each crow As DataRow In company.Rows
            'dv.RowFilter = " entry_date = #" & date1.Date & "# and company='" & crow("company") & "'"
            'dv.Sort = "entry_date"
            'Dim ttable As New DataTable
            'ttable = dv.ToTable(True, "entry_date")
            'If ttable.Rows.Count <= 0 Then
            '    dv1.RowFilter = "entry_date = #" & date1.Date & "# and company='" & crow("company") & "'"
            '    dv1.Sort = "entry_date"
            '    ttable = dv1.ToTable(True, "entry_date")
            'End If
            ' For Each row As DataRow In ttable.Rows
            prow = addprebal.NewRow
            prow("tdate") = date1 ' CDate(row("entry_date")).Date
            prow("stbal") = 0
            prow("futbal") = 0
            prow("optbal") = 0
            prow("company") = crow("company")
            addprebal.Rows.Add(prow)
            stexp = 0
            stexp1 = 0
            dst = 0
            ndst = 0
            exppr = 0
            expto = 0

            'Equity ##################################################################
            stexp = Math.Round(Val(stk.Compute("sum(tot)", "company='" & crow("company") & "' and qty > 0 and entry_date = #" & Format(date1.Date, "dd-MMM-yyyy") & "#").ToString), 2)
            stexp1 = Math.Abs(Math.Round(Val(stk.Compute("sum(tot)", "company='" & crow("company") & "' and qty < 0 and entry_date = #" & Format(date1.Date, "dd-MMM-yyyy") & "#").ToString), 2))
            dst = stexp - stexp1
            If dst > 0 Then
                ndst = stexp1
                prow("stbal") = Val(prow("stbal")) + Val((dst * dbl) / dblp) + Val((ndst * ndbs) / ndbsp) + Val((ndst * ndbl) / ndblp)
            Else
                ndst = stexp
                dst = -dst
                prow("stbal") = Val(prow("stbal")) + Val((dst * dbs) / dbsp) + Val((ndst * ndbs) / ndbsp) + Val((ndst * ndbl) / ndblp)
            End If

            'Future #################################################################
            stexp = 0
            stexp1 = 0
            stexp = Val(cpf.Compute("sum(tot)", "cp not in ('C','P') and company='" & crow("company") & "' and qty > 0 and entry_date = '#" & Format(date1, "dd-MMM-yyyy") & "#'").ToString)
            stexp1 = Math.Abs(Val(cpf.Compute("sum(tot)", "cp not in ('C','P') and company='" & crow("company") & "' and qty < 0 and entry_date = '#" & Format(date1, "dd-MMM-yyyy") & "#'").ToString))
            prow("futbal") = Val(prow("futbal")) + Val((stexp * futl) / futlp) + Val((stexp1 * futs) / futsp)
            'Option ####################################################################
            stexp = 0
            stexp1 = 0
            'stexp = Val(cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & crow("company") & "' and qty > 0 and entry_date = '#" & Format(date1.Date, "dd-MMM-yyyy") & "#'").ToString)
            'stexp1 = Math.Abs(Val(cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & crow("company") & "' and qty < 0 and entry_date = '#" & Format(date1.Date, "dd-MMM-yyyy") & "#'").ToString))
            If Val(spl) <> 0 Then
                REM tot2 Field  = (Rate + StrikePrice) * Qty Update By Viral
                stexp = Val(cpf.Compute("sum(tot2)", "cp  in ('C','P') and company='" & crow("company") & "' and qty > 0 and entry_date = '#" & Format(date1.Date, "dd-MMM-yyyy") & "#'").ToString)
                stexp1 = Math.Abs(Val(cpf.Compute("sum(tot2)", "cp  in ('C','P') and company='" & crow("company") & "' and qty < 0 and entry_date = '#" & Format(date1.Date, "dd-MMM-yyyy") & "#'").ToString))
                prow("optbal") = Val(prow("optbal")) + Val((stexp * spl) / splp) + Val((stexp1 * sps) / spsp)
            Else
                stexp = Val(cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & crow("company") & "' and qty > 0 and entry_date = '#" & Format(date1.Date, "dd-MMM-yyyy") & "#'").ToString)
                stexp1 = Math.Abs(Val(cpf.Compute("sum(tot)", "cp  in ('C','P') and company='" & crow("company") & "' and qty < 0 and entry_date = '#" & Format(date1.Date, "dd-MMM-yyyy") & "#'").ToString))
                prow("optbal") = Val(prow("optbal")) + Val((stexp * prel) / prelp) + Val((stexp1 * pres) / presp)
            End If

            ''divyesh
            'Currency Futre #################################################################
            stexp = 0
            stexp1 = 0
            stexp = Val(currtrd.Compute("sum(tot)", "cp not in ('C','P') and company='" & crow("company") & "' and qty > 0 and entry_date = '#" & Format(date1, "dd-MMM-yyyy") & "#'").ToString)
            stexp1 = Math.Abs(Val(currtrd.Compute("sum(tot)", "cp not in ('C','P') and company='" & crow("company") & "' and qty < 0 and entry_date = '#" & Format(date1, "dd-MMM-yyyy") & "#'").ToString))
            prow("futbal") = Val(prow("futbal")) + Val((stexp * currfutl) / currfutlp) + Val((stexp1 * currfuts) / currfutsp)
            'Currency Option ####################################################################
            stexp = 0
            stexp1 = 0
            If Val(currspl) <> 0 Then
                REM tot2 Field  = (Rate + StrikePrice) * Qty Update By Viral
                stexp = Val(currtrd.Compute("sum(tot2)", "cp  in ('C','P') and company='" & crow("company") & "' and qty > 0 and entry_date = '#" & Format(date1.Date, "dd-MMM-yyyy") & "#'").ToString)
                stexp1 = Math.Abs(Val(currtrd.Compute("sum(tot2)", "cp  in ('C','P') and company='" & crow("company") & "' and qty < 0 and entry_date = '#" & Format(date1.Date, "dd-MMM-yyyy") & "#'").ToString))
                prow("optbal") = Val(prow("optbal")) + Val((stexp * currspl) / currsplp) + Val((stexp1 * currsps) / currspsp)
            Else
                stexp = Val(currtrd.Compute("sum(tot)", "cp  in ('C','P') and company='" & crow("company") & "' and qty > 0 and entry_date = '#" & Format(date1.Date, "dd-MMM-yyyy") & "#'").ToString)
                stexp1 = Math.Abs(Val(currtrd.Compute("sum(tot)", "cp  in ('C','P') and company='" & crow("company") & "' and qty < 0 and entry_date = '#" & Format(date1.Date, "dd-MMM-yyyy") & "#'").ToString))
                prow("optbal") = Val(prow("optbal")) + Val((stexp * currprel) / currprelp) + Val((stexp1 * currpres) / currpresp)
            End If


            'Next
        Next
        objTrad.insert_prebal(addprebal)
    End Sub

    Public Function UpdateCFBALNCE(ByVal balance As Double, ByVal compname As String) As Integer


        Dim qry As String
        Try

            qry = "Delete From TblCFBalance where Symbol='" & compname & "';"
            data_access.ParamClear()
            data_access.Cmd_Text = qry
            data_access.cmd_type = CommandType.Text
            data_access.ExecuteNonQuery()
            data_access.cmd_type = CommandType.StoredProcedure


            qry = "Insert into TblCFBalance (Symbol,Balance) values('" & compname & "'," & balance & ");"
            data_access.ParamClear()
            data_access.Cmd_Text = qry
            data_access.cmd_type = CommandType.Text
            data_access.ExecuteNonQuery()
            data_access.cmd_type = CommandType.StoredProcedure




            'qry = "UPDATE TblCFBalance SET Balance = " & balance & " where Symbol='" & compname & "';"
            'data_access.ParamClear()
            'data_access.Cmd_Text = qry
            'data_access.cmd_type = CommandType.Text
            'data_access.ExecuteNonQuery()
            'data_access.cmd_type = CommandType.StoredProcedure
            ' MsgBox("Successfully Updated...")
        Catch ex As Exception
            ' MsgBox("Error to Update Data")

        End Try
    End Function
    Public Function get_SFutCurr(ByVal compname As String, ByVal date1 As Date, ByVal fltppr As Double) As Double
        Dim BCast As Date = Now
        compname = GetSymbol(compname)
        Dim midstrikelower, tokencelower, tokenpelower, cepricelower, pepricelower, indexlower As Double
        Dim dtcpfmaster As DataTable = Currencymaster.Copy
        'If Now.Date = date1 and Now.Date > Then

        'End If
        Dim str As String = date1.ToString("dd-MMM-yyyy") ' "2014/08/15 19:45"
        'Dim dt1 = Date.ParseExact(str, "yyyy/MM/dd HH:mm", System.Globalization.CultureInfo.InvariantCulture)



        If date1.ToString("dd-MMM-yyyy") = Now.Date.ToString("dd-MMM-yyyy") Then

            Dim myCompareTime As DateTime = DateTime.Parse("12:00:01 PM")

            If myCompareTime.TimeOfDay > DateTime.Now.TimeOfDay Then

                Dim nextmdate As DataRow = dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1>#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type")(0)
                date1 = nextmdate.Item("expdate1")
            End If
        End If

        If SYNTH_ATM_UPDOWNSTRIKE = 0 Or SYNTH_ATM_UPDOWNSTRIKE = 2 Then



            dtcpfmaster = New DataView(dtcpfmaster, "Symbol='" & compname & "'", "", DataViewRowState.CurrentRows).ToTable()

            If fltppr = 0 Then
                Exit Function
            End If
            WriteLogMarketwatchlog("viral compname=" + compname.ToString() + "date1=" + date1.ToString("dd/MMM/yyyy") + "fltppr=" + fltppr.ToString())
            Dim midstrikeupper, tokenceupper, tokenpeupper, cepriceupper, pepriceupper, indexupper As Double

            compname = GetSymbol(compname)
            WriteLogMarketwatchlog("viral AftGetSymbol compname=" + compname.ToString() + "date1=" + date1.ToString("dd/MMM/yyyy") + "fltppr=" + fltppr.ToString())
            Dim drmidstrike As DataRow
            Try
                If dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type").Length > 0 Then
                    drmidstrike = dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type")(0)
                    midstrikeupper = drmidstrike.Item("strike_price")
                End If

            Catch ex As Exception
                MsgBox("midstrikeupper" + ex.ToString)
            End Try
            Try
                tokenceupper = dtcpfmaster.Select("symbol='" & compname & "' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type")(0).Item("token")
                tokenpeupper = dtcpfmaster.Select("symbol='" & compname & "'and option_Type='PE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrikeupper) & " ", "strike_price,option_Type")(0).Item("token")


                cepriceupper = Val(Currltpprice(CLng(tokenceupper)))
                pepriceupper = Val(Currltpprice(CLng(tokenpeupper)))
                indexupper = midstrikeupper + (cepriceupper - pepriceupper)


                If (ltpprice(CLng(tokenceupper)) Is Nothing Or ltpprice(CLng(tokenceupper)) = 0 Or (Not FoRegTokens.Contains(tokenceupper))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                    Objsql.AppendCurTokens(CLng(tokenceupper).ToString)
                End If
                If (ltpprice(CLng(tokenpeupper)) Is Nothing Or ltpprice(CLng(tokenpeupper)) = 0 Or (Not FoRegTokens.Contains(tokenpeupper))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                    Objsql.AppendCurTokens(CLng(tokenpelower).ToString)
                End If
                If cepriceupper = 0 Or pepriceupper = 0 Then
                    indexupper = 0
                End If

                'Dim midstrikelower, tokencelower, tokenpelower, cepricelower, pepricelower, indexlower As Double
                WriteLogMarketwatchlog("midstrikeupper=" + midstrikeupper.ToString() + "cepriceupper=" + cepriceupper.ToString() + "pepriceupper=" + pepriceupper.ToString())
            Catch ex As Exception
                'MsgBox(ex.ToString())
                WriteLogMarketwatchlog("Error in get_CurrsFut" + ex.ToString)
                Return 0
            End Try

            Try
                midstrikelower = dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price <= " & Convert.ToDouble(fltppr) & " ", "strike_price DESC,option_Type")(0).Item("strike_price")
                tokencelower = dtcpfmaster.Select("symbol='" & compname & "'and option_Type='CE'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price <= " & Convert.ToDouble(fltppr) & " ", "strike_price DESC,option_Type")(0).Item("token")
                tokenpelower = dtcpfmaster.Select("symbol='" & compname & "'and option_Type='PE'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrikelower) & " ", "strike_price DESC,option_Type")(0).Item("token")

                cepricelower = Val(Currltpprice(CLng(tokencelower)))
                pepricelower = Val(Currltpprice(CLng(tokenpelower)))
                indexlower = midstrikelower + (cepricelower - pepricelower)
                If (ltpprice(CLng(tokencelower)) Is Nothing Or ltpprice(CLng(tokencelower)) = 0 Or (Not FoRegTokens.Contains(tokencelower))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                    Objsql.AppendCurTokens(CLng(tokencelower).ToString)
                End If
                If (ltpprice(CLng(tokenpelower)) Is Nothing Or ltpprice(CLng(tokenpelower)) = 0 Or (Not FoRegTokens.Contains(tokenpelower))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                    Objsql.AppendCurTokens(CLng(tokenpelower).ToString)
                End If
                If cepricelower = 0 Or pepricelower = 0 Then
                    indexlower = 0
                End If

                WriteLogMarketwatchlog("midstrikelower=" + midstrikelower.ToString() + "cepricelower=" + cepricelower.ToString() + "pepricelower=" + pepricelower.ToString())
                WriteLogMW("SF=" & indexlower & "|Setting-UpDown|Upperstrike=" & midstrikeupper & "+UpperCallPrice=" & Format(cepriceupper, "#0.0000") & "-UpperPutPrice=" & Format(pepriceupper, "#0.0000") & "")
                WriteLogMW("          +|Lowerstrike=" & midstrikelower & "+LowerCallPrice=" & Format(cepricelower, "#0.0000") & "-LowerPutPrice=" & Format(pepricelower, "#0.0000") & "")

                If indexupper = 0 Or indexlower = 0 Then
                    Return 0
                Else
                    Return Math.Round(((indexupper + indexlower) / 2) * 10000 / 25, 0) * 25 / 10000
                End If

                ' Return 0
            Catch ex As Exception
                WriteLogMarketwatchlog("Error in get_CurrsFut" + ex.ToString)
                'MsgBox(ex.ToString)
                Return 0
            End Try


        ElseIf SYNTH_ATM_UPDOWNSTRIKE = 1 Then


            If fltppr = 0 Then
                Exit Function
            End If
            WriteLogMarketwatchlog("viral compname=" + compname.ToString() + "date1=" + date1.ToString("dd/MMM/yyyy") + "fltppr=" + fltppr.ToString())
            Dim midstrikeupper, tokenceupper, tokenpeupper, cepriceupper, pepriceupper, indexupper As Double

            compname = GetSymbol(compname)
            WriteLogMarketwatchlog("viral AftGetSymbol compname=" + compname.ToString() + "date1=" + date1.ToString("dd/MMM/yyyy") + "fltppr=" + fltppr.ToString())
            Dim drmidstrike As DataRow
            Try
                If dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price > " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type").Length > 0 Then
                    drmidstrike = dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type")(0)
                    midstrikeupper = drmidstrike.Item("strike_price")

                    midstrikelower = dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price <= " & Convert.ToDouble(fltppr) & " ", "strike_price DESC,option_Type")(0).Item("strike_price")


                    Dim strdiffuppar As Double = midstrikeupper - Convert.ToDouble(fltppr)
                    Dim strdifflower As Double = Convert.ToDouble(fltppr) - midstrikelower

                    Dim midstrike As Double
                    Dim minval As Double = Math.Min(strdiffuppar, strdifflower)
                    If minval = strdiffuppar Then
                        midstrike = midstrikeupper
                    Else
                        midstrike = midstrikelower
                    End If

                    tokenceupper = dtcpfmaster.Select("symbol='" & compname & "' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrike) & " ", "strike_price,option_Type")(0).Item("token")
                    tokenpeupper = dtcpfmaster.Select("symbol='" & compname & "'and option_Type='PE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrike) & " ", "strike_price,option_Type")(0).Item("token")


                    cepriceupper = Val(Currltpprice(CLng(tokenceupper)))
                    pepriceupper = Val(Currltpprice(CLng(tokenpeupper)))
                    indexupper = midstrike + (cepriceupper - pepriceupper)

                    If (ltpprice(CLng(tokenceupper)) Is Nothing Or ltpprice(CLng(tokenceupper)) = 0 Or (Not FoRegTokens.Contains(tokenceupper))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                        Objsql.AppendCurTokens(CLng(tokenceupper).ToString)
                    End If
                    If (ltpprice(CLng(tokenpeupper)) Is Nothing Or ltpprice(CLng(tokenpeupper)) = 0 Or (Not FoRegTokens.Contains(tokenpeupper))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                        Objsql.AppendCurTokens(CLng(tokenpeupper).ToString)
                    End If

                    WriteLogMW("SF=" & indexupper & "|Setting-ATMStrike=" & Format(midstrike, "#0.0000") & "+(CallPrice=" & Format(cepriceupper, "#0.0000") & "-PutPrice=" & Format(pepriceupper, "#0.0000") & ")")

                    If cepriceupper = 0 Or pepriceupper = 0 Then
                        Return 0
                    Else
                        Return Math.Round(indexupper * 10000 / 25, 0) * 25 / 10000
                    End If

                End If

            Catch ex As Exception

            End Try



        End If



    End Function
    Public Function get_SFuttblcurr(ByVal compname As String, ByVal date1 As Date, ByVal fltppr As Double, ByVal dtcpfmaster As DataTable) As Double
        If flg_TabChanging = True Then
            Return 0.0
        End If

        Dim FuturePrice As Double = fltppr
        compname = GetSymbol(compname)
        Dim midstrikelower, tokencelower, tokenpelower, cepricelower, pepricelower, indexlower As Double
        'Dim dtcpfmaster As DataTable = cpfmaster.Copy

        If SYNTH_ATM_UPDOWNSTRIKE = 0 Or SYNTH_ATM_UPDOWNSTRIKE = 2 Then


            If compname.Contains("NIFTY") And SYNTH_ATM_UPDOWNSTRIKE = 2 Then

                'If compname = "NIFTY" Then
                '    If dtcpfmaster.Columns.Contains("StrikeMod") = False Then
                '        dtcpfmaster.Columns.Add("StrikeMod", GetType(Double))
                '    End If

                '    For Each dr As DataRow In dtcpfmaster.Rows
                '        If dr("symbol") = "NIFTY" Then


                '            If dr("InstrumentName").ToString() = "FUTIDX" Then
                '                dr("StrikeMod") = 0
                '            Else
                '                dr("StrikeMod") = (dr("Strike_Price") / 100) Mod 1
                '            End If
                '        End If
                '    Next
                'End If  And StrikeMod = 0

                fltppr = Math.Round(fltppr / 100) * 100
                'dtcpfmaster = dtcpfmaster 'New DataView(dtcpfmaster, "Symbol='" & compname & "'", "", DataViewRowState.CurrentRows).ToTable()
            Else
                'dtcpfmaster = dtcpfmaster 'New DataView(dtcpfmaster, "Symbol='" & compname & "'", "", DataViewRowState.CurrentRows).ToTable()
            End If
            If fltppr = 0 Then
                Exit Function
            End If

            'WriteLogMarketwatchlog("viral compname=" + compname.ToString() + "date1=" + date1.ToString("dd/MMM/yyyy") + "fltppr=" + fltppr.ToString())
            Dim midstrikeupper, tokenceupper, tokenpeupper, cepriceupper, pepriceupper, indexupper As Double

            Dim StrikeCEupper As Double
            Dim StrikePEupper As Double

            Dim StrikeCELower As Double
            Dim StrikePELower As Double

            compname = GetSymbol(compname)
            'WriteLogMarketwatchlog("viral AftGetSymbol compname=" + compname.ToString() + "date1=" + date1.ToString("dd/MMM/yyyy") + "fltppr=" + fltppr.ToString())
            'Dim drmidstrike As DataRow
            Dim drmidstrike() As DataRow = dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type")
            Try
                If drmidstrike.Length > 0 Then 'dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type").Length > 0 Then
                    'drmidstrike = dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type")(0)
                    midstrikeupper = drmidstrike(0).Item("strike_price") 'drmidstrike.Item("strike_price")
                End If

            Catch ex As Exception
                MsgBox("midstrikeupper" + ex.ToString)
            End Try
            Try
                If drmidstrike.Length > 0 Then 'dtcpfmaster.Select("symbol='" & compname & "' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type").Length > 0 Then

                    If compname.Contains("NIFTY") And SYNTH_ATM_UPDOWNSTRIKE = 2 Then
                        StrikeCEupper = midstrikeupper
                        StrikePEupper = midstrikeupper
                    Else
                        StrikeCEupper = drmidstrike(0).Item("strike_price") ' dtcpfmaster.Select("symbol='" & compname & "' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type")(0).Item("strike_price")
                        StrikePEupper = StrikeCEupper 'dtcpfmaster.Select("symbol='" & compname & "'and option_Type='PE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrikeupper) & " ", "strike_price,option_Type")(0).Item("strike_price")

                    End If

                    '  tokenceupper = dtcpfmaster.Select("symbol='" & compname & "' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & StrikeCEupper & " ", "strike_price,option_Type")(0).Item("token")
                    tokenceupper = HT_FOTOKCEX(compname.ToString() & CDate(date1).ToString("ddMMMyyyy") & "CE" & StrikeCEupper)
                    'tokenpeupper = dtcpfmaster.Select("symbol='" & compname & "'and option_Type='PE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & StrikePEupper & " ", "strike_price,option_Type")(0).Item("token")
                    tokenpeupper = HT_FOTOKCEX(compname.ToString() & CDate(date1).ToString("ddMMMyyyy") & "PE" & StrikePEupper)

                    Objsql.AppendFoTokens(CLng(tokenceupper).ToString)
                    Objsql.AppendFoTokens(CLng(tokenpeupper).ToString)



                    'If (ltpprice(CLng(tokenceupper)) Is Nothing Or ltpprice(CLng(tokenceupper)) = 0 Or (Not FoRegTokens.Contains(tokenceupper))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                    '    Objsql.AppendFoTokens(CLng(tokenceupper).ToString)
                    'End If
                    'If (ltpprice(CLng(tokenpeupper)) Is Nothing Or ltpprice(CLng(tokenpeupper)) = 0 Or (Not FoRegTokens.Contains(tokenpeupper))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                    '    Objsql.AppendFoTokens(CLng(tokenpeupper).ToString)
                    'End If

                    cepriceupper = Val(ltpprice(CLng(tokenceupper)))
                    pepriceupper = Val(ltpprice(CLng(tokenpeupper)))

                    If (cepriceupper = "0" Or pepriceupper = "0") Then
                        indexupper = fltppr
                    Else
                        indexupper = midstrikeupper + (cepriceupper - pepriceupper)

                    End If

                Else
                    indexupper = 0
                End If

                WriteLogSynthFutlog("01Setting :" & SYNTH_ATM_UPDOWNSTRIKE & "|FutPrice:" & FuturePrice & "|UpperStrike:" & midstrikeupper & "|CEStrikeUpper:" & StrikeCEupper & "|PEStrikeUpper:" & StrikePEupper & "|CEPrice:" & cepriceupper & "|PEPrice:" & pepriceupper & "|Equation1:" & indexupper)

                'Dim midstrikelower, tokencelower, tokenpelower, cepricelower, pepricelower, indexlower As Double
                'WriteLogMarketwatchlog("midstrikeupper=" + midstrikeupper.ToString() + "cepriceupper=" + cepriceupper.ToString() + "pepriceupper=" + pepriceupper.ToString())
            Catch ex As Exception
                MsgBox(ex.ToString())
            End Try
            '//Lower <= Replace To <
            Try
                If dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price < " & Convert.ToDouble(fltppr) & " ", "strike_price DESC,option_Type").Length > 0 Then

                    If compname.Contains("NIFTY") And SYNTH_ATM_UPDOWNSTRIKE = 2 Then
                        midstrikelower = Convert.ToDouble(fltppr) - 100
                        ' StrikeCELower = midstrikelower
                        'StrikePELower = midstrikelower
                    Else

                        midstrikelower = dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price < " & Convert.ToDouble(fltppr) & " ", "strike_price DESC,option_Type")(0).Item("strike_price")

                        'StrikeCELower = midstrikelower 'dtcpfmaster.Select("symbol='" & compname & "'and option_Type='CE'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrikelower) & " ", "strike_price DESC,option_Type")(0).Item("strike_price")
                        ' StrikePELower = midstrikelower 'dtcpfmaster.Select("symbol='" & compname & "'and option_Type='PE'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrikelower) & " ", "strike_price DESC,option_Type")(0).Item("strike_price")

                    End If

                    StrikeCELower = midstrikelower
                    StrikePELower = midstrikelower

                    'tokencelower = dtcpfmaster.Select("symbol='" & compname & "'and option_Type='CE'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(StrikeCELower) & " ", "strike_price DESC,option_Type")(0).Item("token")
                    tokencelower = HT_FOTOKCEX(compname.ToString() & CDate(date1).ToString("ddMMMyyyy") & "CE" & StrikeCELower)
                    ' tokenpelower = dtcpfmaster.Select("symbol='" & compname & "'and option_Type='PE'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrikelower) & " ", "strike_price DESC,option_Type")(0).Item("token")
                    tokenpelower = HT_FOTOKCEX(compname.ToString() & CDate(date1).ToString("ddMMMyyyy") & "PE" & StrikeCELower)

                    If (ltpprice(CLng(tokencelower)) Is Nothing Or ltpprice(CLng(tokencelower)) = 0 Or (Not FoRegTokens.Contains(tokencelower))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                        Objsql.AppendFoTokens(CLng(tokencelower).ToString)
                    End If
                    If (ltpprice(CLng(tokenpelower)) Is Nothing Or ltpprice(CLng(tokenpelower)) = 0 Or (Not FoRegTokens.Contains(tokenpelower))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                        Objsql.AppendFoTokens(CLng(tokenpelower).ToString)
                    End If

                    cepricelower = Val(ltpprice(CLng(tokencelower)))
                    pepricelower = Val(ltpprice(CLng(tokenpelower)))

                    If (cepricelower = "0" Or pepricelower = "0") Then
                        indexlower = fltppr
                    Else
                        indexlower = midstrikelower + (cepricelower - pepricelower)


                    End If

                    WriteLogSynthFutlog("02Setting :" & SYNTH_ATM_UPDOWNSTRIKE & "|FutPrice:" & FuturePrice & "|LowerStrike:" & midstrikelower & "|CEStrikeLower:" & StrikeCELower & "|PEStrikeLower:" & StrikePELower & "|CEPrice:" & cepricelower & "|PEPrice:" & pepricelower & "|Equation1:" & indexlower)

                    'WriteLogMarketwatchlog("midstrikelower=" + midstrikelower.ToString() + "cepricelower=" + cepricelower.ToString() + "pepricelower=" + pepricelower.ToString())
                Else
                    indexupper = 0
                    indexlower = 0
                End If

                Return (indexupper + indexlower) / 2
                ' Return 0
            Catch ex As Exception
                'WriteLogMarketwatchlog("Error in get_sFut" + ex.ToString)
                MsgBox(ex.ToString)
                Return 0
            End Try


        ElseIf SYNTH_ATM_UPDOWNSTRIKE = 1 Then


            If fltppr = 0 Then
                Exit Function
            End If
            'WriteLogMarketwatchlog("viral compname=" + compname.ToString() + "date1=" + date1.ToString("dd/MMM/yyyy") + "fltppr=" + fltppr.ToString())
            Dim midstrikeupper, tokenceupper, tokenpeupper, cepriceupper, pepriceupper, indexupper As Double
            Dim StrikeCEupper As Double
            Dim StrikePEupper As Double

            compname = GetSymbol(compname)
            'WriteLogMarketwatchlog("viral AftGetSymbol compname=" + compname.ToString() + "date1=" + date1.ToString("dd/MMM/yyyy") + "fltppr=" + fltppr.ToString())
            'Dim drmidstrike As DataRow
            Dim drmidstrike() As DataRow = cpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price > " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type")
            Try
                If drmidstrike.Length > 0 Then ' cpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price > " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type").Length > 0 Then
                    'drmidstrike = cpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type")(0)
                    midstrikeupper = drmidstrike(0).Item("strike_price") ' drmidstrike.Item("strike_price")

                    midstrikelower = cpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price <= " & Convert.ToDouble(fltppr) & " ", "strike_price DESC,option_Type")(0).Item("strike_price")


                    Dim strdiffuppar As Double = midstrikeupper - Convert.ToDouble(fltppr)
                    Dim strdifflower As Double = Convert.ToDouble(fltppr) - midstrikelower

                    Dim midstrike As Double
                    Dim minval As Double = Math.Min(strdiffuppar, strdifflower)
                    If minval = strdiffuppar Then
                        midstrike = midstrikeupper
                    Else
                        midstrike = midstrikelower
                    End If

                    StrikeCEupper = cpfmaster.Select("symbol='" & compname & "' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrike) & " ", "strike_price,option_Type")(0).Item("strike_price")
                    StrikePEupper = StrikeCEupper 'cpfmaster.Select("symbol='" & compname & "'and option_Type='PE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrike) & " ", "strike_price,option_Type")(0).Item("strike_price")

                    Dim objStrfo As Struct_FOContract
                    objStrfo = New Struct_FOContract




                    tokenceupper = HT_FOTOKCEX(compname.ToString() & CDate(date1).ToString("ddMMMyyyy") & "CE" & midstrike)
                    'cpfmaster.Select("symbol='" & compname & "' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrike) & " ", "strike_price,option_Type")(0).Item("token")
                    tokenpeupper = HT_FOTOKCEX(compname.ToString() & CDate(date1).ToString("ddMMMyyyy") & "PE" & midstrike)
                    'cpfmaster.Select("symbol='" & compname & "'and option_Type='PE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrike) & " ", "strike_price,option_Type")(0).Item("token")

                    If (ltpprice(CLng(tokenceupper)) Is Nothing Or ltpprice(CLng(tokenceupper)) = 0 Or (Not FoRegTokens.Contains(tokenceupper))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                        Objsql.AppendFoTokens(CLng(tokenceupper).ToString)
                    End If
                    If (ltpprice(CLng(tokenpeupper)) Is Nothing Or ltpprice(CLng(tokenpeupper)) = 0 Or (Not FoRegTokens.Contains(tokenpeupper))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                        Objsql.AppendFoTokens(CLng(tokenpeupper).ToString)
                    End If


                    cepriceupper = Val(ltpprice(CLng(tokenceupper)))
                    pepriceupper = Val(ltpprice(CLng(tokenpeupper)))

                    If (cepriceupper = 0 Or pepriceupper = 0) Then
                        indexlower = fltppr
                    Else
                        indexupper = midstrike + (cepriceupper - pepriceupper)

                    End If
                    WriteLogSynthFutlog("Setting :" & SYNTH_ATM_UPDOWNSTRIKE & "|FutPrice:" & FuturePrice & "|AtmStrike:" & midstrike & "|CEStrike:" & StrikeCEupper & "|PEStrike:" & StrikePEupper & "|CEPrice:" & cepriceupper & "|PEPrice:" & pepriceupper & "|Equation:" & "indexupper = midstrike & (cepriceupper - pepriceupper)" & "Synth:" & indexupper)
                    Return indexupper
                End If

            Catch ex As Exception
                'MsgBox(ex.ToString)
                'Return 0
            End Try



        End If



    End Function
    Public Function get_SFut(ByVal compname As String, ByVal date1 As Date, ByVal fltppr As Double) As Double
        'If flg_TabChanging = True Then
        '    Return 0.0
        'End If


        compname = GetSymbol(compname)
        Dim midstrikelower, tokencelower, tokenpelower, cepricelower, pepricelower, indexlower As Double
        Dim dtcpfmaster As DataTable = cpfmaster.Copy
        If SYNTH_ATM_UPDOWNSTRIKE = 0 Or SYNTH_ATM_UPDOWNSTRIKE = 2 Then


            If compname = "NIFTY" And SYNTH_ATM_UPDOWNSTRIKE = 2 Then

                'If compname = "NIFTY" Then
                '    If dtcpfmaster.Columns.Contains("StrikeMod") = False Then
                '        dtcpfmaster.Columns.Add("StrikeMod", GetType(Double))
                '    End If

                '    For Each dr As DataRow In dtcpfmaster.Rows
                '        If dr("symbol") = "NIFTY" Then


                '            If dr("InstrumentName").ToString() = "FUTIDX" Then
                '                dr("StrikeMod") = 0
                '            Else
                '                dr("StrikeMod") = (dr("Strike_Price") / 100) Mod 1
                '            End If
                '        End If
                '    Next
                'End If  And StrikeMod = 0
                fltppr = Math.Round(fltppr / 100) * 100
                dtcpfmaster = New DataView(dtcpfmaster, "Symbol='" & compname & "'", "", DataViewRowState.CurrentRows).ToTable()
            Else
                dtcpfmaster = New DataView(dtcpfmaster, "Symbol='" & compname & "'", "", DataViewRowState.CurrentRows).ToTable()
            End If
            If fltppr = 0 Then
                Exit Function
            End If
            WriteLogMarketwatchlog("viral compname=" + compname.ToString() + "date1=" + date1.ToString("dd/MMM/yyyy") + "fltppr=" + fltppr.ToString())
            Dim midstrikeupper, tokenceupper, tokenpeupper, cepriceupper, pepriceupper, indexupper As Double

            'compname = GetSymbol(compname)
            WriteLogMarketwatchlog("viral AftGetSymbol compname=" + compname.ToString() + "date1=" + date1.ToString("dd/MMM/yyyy") + "fltppr=" + fltppr.ToString())
            Dim drmidstrike As DataRow
            Try
                If dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type").Length > 0 Then
                    drmidstrike = dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type")(0)
                    midstrikeupper = drmidstrike.Item("strike_price")
                End If

            Catch ex As Exception
                MsgBox("midstrikeupper" + ex.ToString)
            End Try
            Try

                If dtcpfmaster.Select("symbol='" & compname & "' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type").Length > 0 Then
                    tokenceupper = dtcpfmaster.Select("symbol='" & compname & "' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type")(0).Item("token")
                    tokenpeupper = dtcpfmaster.Select("symbol='" & compname & "' and option_Type='PE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrikeupper) & " ", "strike_price,option_Type")(0).Item("token")
                End If
                If ltpprice(CLng(tokenceupper)) Is Nothing And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                    Objsql.AppendFoTokens(CLng(tokenceupper).ToString)
                End If
                If ltpprice(CLng(tokenpeupper)) Is Nothing And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                    Objsql.AppendFoTokens(CLng(tokenpeupper).ToString)
                End If

                cepriceupper = Val(ltpprice(CLng(tokenceupper)))
                pepriceupper = Val(ltpprice(CLng(tokenpeupper)))


                indexupper = midstrikeupper + (cepriceupper - pepriceupper)
                'Dim midstrikelower, tokencelower, tokenpelower, cepricelower, pepricelower, indexlower As Double
                WriteLogMarketwatchlog("midstrikeupper=" + midstrikeupper.ToString() + "cepriceupper=" + cepriceupper.ToString() + "pepriceupper=" + pepriceupper.ToString())
            Catch ex As Exception
                MsgBox(ex.ToString())
            End Try

            Try
                If dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price <= " & Convert.ToDouble(fltppr) & " ", "strike_price DESC,option_Type").Length > 0 Then


                    midstrikelower = dtcpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price <= " & Convert.ToDouble(fltppr) & " ", "strike_price DESC,option_Type")(0).Item("strike_price")
                    tokencelower = dtcpfmaster.Select("symbol='" & compname & "'and option_Type='CE'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price <= " & Convert.ToDouble(fltppr) & " ", "strike_price DESC,option_Type")(0).Item("token")
                    tokenpelower = dtcpfmaster.Select("symbol='" & compname & "'and option_Type='PE'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrikelower) & " ", "strike_price DESC,option_Type")(0).Item("token")
                End If
                If ltpprice(CLng(tokencelower)) Is Nothing And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                    Objsql.AppendFoTokens(CLng(tokencelower).ToString)
                End If
                If ltpprice(CLng(tokenpelower)) Is Nothing And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                    Objsql.AppendFoTokens(CLng(tokenpelower).ToString)
                End If

                cepricelower = Val(ltpprice(CLng(tokencelower)))
                pepricelower = Val(ltpprice(CLng(tokenpelower)))
                indexlower = midstrikelower + (cepricelower - pepricelower)
                WriteLogMarketwatchlog("midstrikelower=" + midstrikelower.ToString() + "cepricelower=" + cepricelower.ToString() + "pepricelower=" + pepricelower.ToString())
                Return (indexupper + indexlower) / 2
                ' Return 0
            Catch ex As Exception
                WriteLogMarketwatchlog("Error in get_sFut" + ex.ToString)
                MsgBox(ex.ToString)
                Return 0
            End Try


        ElseIf SYNTH_ATM_UPDOWNSTRIKE = 1 Then


            If fltppr = 0 Then
                Exit Function
            End If
            WriteLogMarketwatchlog("viral compname=" + compname.ToString() + "date1=" + date1.ToString("dd/MMM/yyyy") + "fltppr=" + fltppr.ToString())
            Dim midstrikeupper, tokenceupper, tokenpeupper, cepriceupper, pepriceupper, indexupper As Double

            compname = GetSymbol(compname)
            WriteLogMarketwatchlog("viral AftGetSymbol compname=" + compname.ToString() + "date1=" + date1.ToString("dd/MMM/yyyy") + "fltppr=" + fltppr.ToString())
            Dim drmidstrike As DataRow
            Try
                If cpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price > " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type").Length > 0 Then
                    drmidstrike = cpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price >= " & Convert.ToDouble(fltppr) & " ", "strike_price,option_Type")(0)
                    midstrikeupper = drmidstrike.Item("strike_price")

                    midstrikelower = cpfmaster.Select("symbol='" & compname & "'and option_Type<>'XX'  and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price <= " & Convert.ToDouble(fltppr) & " ", "strike_price DESC,option_Type")(0).Item("strike_price")


                    Dim strdiffuppar As Double = midstrikeupper - Convert.ToDouble(fltppr)
                    Dim strdifflower As Double = Convert.ToDouble(fltppr) - midstrikelower

                    Dim midstrike As Double
                    Dim minval As Double = Math.Min(strdiffuppar, strdifflower)
                    If minval = strdiffuppar Then
                        midstrike = midstrikeupper
                    Else
                        midstrike = midstrikelower
                    End If

                    tokenceupper = cpfmaster.Select("symbol='" & compname & "' and option_Type='CE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrike) & " ", "strike_price,option_Type")(0).Item("token")
                    tokenpeupper = cpfmaster.Select("symbol='" & compname & "'and option_Type='PE' and expdate1=#" & date1.ToString("dd/MMM/yyyy") & "# And strike_price = " & Convert.ToDouble(midstrike) & " ", "strike_price,option_Type")(0).Item("token")


                    cepriceupper = Val(ltpprice(CLng(tokenceupper)))
                    pepriceupper = Val(ltpprice(CLng(tokenpeupper)))
                    indexupper = midstrike + (cepriceupper - pepriceupper)
                    Return indexupper
                End If

            Catch ex As Exception

            End Try



        End If



    End Function

    Public Sub cal_position_expense(ByVal drow As DataRow, ByRef prExp As Double, ByRef toExp As Double, ByVal optype As String)
        Dim stexp, stexp1, exp As Double
        Dim dtdate As New DataTable
        Dim dv As DataView
        If optype = "EQ" Then 'for equity
            Dim dst, ndst As Double
            dv = New DataView(GdtEQTrades, "script='" & drow("script") & "'", "", DataViewRowState.CurrentRows)
            dtdate = dv.ToTable(True, "entry_date")
            Dim gdteq As DataTable = New DataView(GdtEQTrades, "Script ='" & drow("script") & "'", "Script", DataViewRowState.CurrentRows).ToTable
            'Dim dteq As DataTable = new DataView(GdtEQTrades, "script='" & drow("script") & "' And company='" & drow("company") & "'", "", DataViewRowState.CurrentRows).ToTable(True, "entry_date")
            For Each erow As DataRow In dtdate.Rows
                exp = 0
                stexp = Math.Round(Val(gdteq.Compute("sum(tot)", "qty > 0 and script = '" & drow("script") & "' AND entry_date =  #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString), 2)
                stexp1 = Math.Abs(Math.Round(Val(gdteq.Compute("sum(tot)", "qty < 0 and script = '" & drow("script") & "' AND entry_date =  #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString), 2))
                dst = stexp - stexp1
                If dst > 0 Then
                    ndst = stexp1
                    exp += ((dst * dbl) / dblp)
                    exp += ((stexp1 * ndbs) / ndbsp)
                    exp += ((stexp1 * ndbl) / ndblp)
                Else
                    ndst = stexp
                    dst = -dst
                    exp += ((dst * dbs) / dbsp)
                    exp += ((stexp * ndbl) / ndblp)
                    exp += ((stexp * ndbs) / ndbsp)
                End If
                If erow("entry_date") = Today.Date Then
                    toExp = exp
                Else
                    prExp += exp
                End If
            Next
        ElseIf optype = "FO" Then 'for future and option
            Dim gdtfo As DataTable
            gdtfo = New DataView(GdtFOTrades, "script='" & drow("script") & "'", "", DataViewRowState.CurrentRows).ToTable
            If drow("cp") = "F" Or drow("cp") = "X" Or drow("cp") = "" Then
                dv = New DataView(GdtFOTrades, "script='" & drow("script") & "' and cp not in('C','P') ", "", DataViewRowState.CurrentRows)
                dtdate = dv.ToTable(True, "entry_date")


                'dv = New DataView(GdtFOTrades, "script='" & drow("script") & "' and cp not in('C','P')", "", DataViewRowState.CurrentRows)
                'dtdate = dv.ToTable(True, "entry_date")
                'For Each erow As DataRow In New DataView(GdtFOTrades, "script='" & drow("script") & "' and cp not in('C','P') And company='" & drow("company") & "'", "", DataViewRowState.CurrentRows).ToTable(True, "entry_date").Rows   'dtdate.Rows
                For Each erow As DataRow In dtdate.Rows
                    exp = 0
                    stexp = Val(gdtfo.Compute("sum(tot)", "cp not in ('C','P') and qty > 0 and script = '" & drow("script") & "' and entry_date = #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString)
                    stexp1 = Math.Abs(Val(gdtfo.Compute("sum(tot)", "cp not in ('C','P')  and qty < 0 and script = '" & drow("script") & "' and entry_date = #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString))
                    exp += ((stexp * futl) / futlp)
                    exp += ((stexp1 * futs) / futsp)
                    If erow("entry_date") = Today.Date Then
                        toExp = exp
                    Else
                        prExp += exp
                    End If
                Next
            Else 'for options
                stexp = 0
                stexp1 = 0
                'dv = New DataView(GdtFOTrades, "script='" & drow("script") & "' and cp in ('C','P')", "", DataViewRowState.CurrentRows)
                'dtdate = dv.ToTable(True, "entry_date")
                'For Each erow As DataRow In New DataView(GdtFOTrades, "script='" & drow("script") & "' and cp in ('C','P') And company='" & drow("company") & "'", "", DataViewRowState.CurrentRows).ToTable(True, "entry_date").Rows  'dtdate.Rows

                For Each erow As DataRow In dtdate.Rows
                    dv = New DataView(gdtfo, "script='" & drow("script") & "' and cp  in('C','P') ", "", DataViewRowState.CurrentRows)
                    dtdate = dv.ToTable(True, "entry_date")
                    exp = 0
                    If Val(spl) <> 0 Then
                        stexp = 0
                        stexp1 = 0
                        REM tot2 Field  = (Rate + StrikePrice) * Qty Update By Viral
                        stexp = Val(gdtfo.Compute("sum(tot2)", "cp  in ('C','P')  and qty > 0 and script = '" & drow("script") & "' and entry_date = #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString)
                        stexp1 = Math.Abs(Val(gdtfo.Compute("sum(tot2)", "cp  in ('C','P') and qty < 0 and script = '" & drow("script") & "' and entry_date = #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString))
                        exp += ((stexp * spl) / splp)
                        exp += ((stexp1 * sps) / spsp)
                    Else
                        stexp = 0
                        stexp1 = 0
                        stexp = Val(gdtfo.Compute("sum(tot)", "cp IN ('C','P') and qty > 0 and script = '" & drow("script") & "' and entry_date = #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString)
                        stexp1 = Math.Abs(Val(gdtfo.Compute("sum(tot)", "cp  in ('C','P') and  qty < 0 and script = '" & drow("script") & "' and entry_date = #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString))
                        exp += ((stexp * prel) / prelp)
                        exp += ((stexp1 * pres) / presp)
                    End If
                    If erow("entry_date") = Today.Date Then
                        toExp = exp
                    Else
                        prExp += exp
                    End If
                Next
            End If

            ''divyesh
        ElseIf optype = "CURR" Then ''for currency
            Dim gdtcurr As DataTable
            gdtcurr = New DataView(GdtCurrencyTrades, "script='" & drow("script") & "'", "Script", DataViewRowState.CurrentRows).ToTable
            If drow("cp") = "F" Or drow("cp") = "X" Or drow("cp") = "" Then
                'dv = New DataView(GdtCurrencyTrades, "script='" & drow("script") & "' and cp not in('C','P')", "", DataViewRowState.CurrentRows)
                'dtdate = dv.ToTable(True, "entry_date")
                dv = New DataView(gdtcurr, "script='" & drow("script") & "' and cp not in('C','P')", "", DataViewRowState.CurrentRows)
                dtdate = dv.ToTable(True, "entry_date")
                'For Each erow As DataRow In New DataView(GdtCurrencyTrades, "script='" & drow("script") & "' and cp not in('C','P') And company='" & drow("company") & "'", "", DataViewRowState.CurrentRows).ToTable(True, "entry_date").Rows 'dtdate.Rows
                For Each erow As DataRow In dtdate.Rows
                    exp = 0
                    stexp = Val(gdtcurr.Compute("sum(tot)", "cp not in ('C','P') and qty > 0 and script = '" & drow("script") & "' and entry_date = #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString)
                    stexp1 = Math.Abs(Val(gdtcurr.Compute("sum(tot)", "cp not in ('C','P')  and qty < 0 and script = '" & drow("script") & "' and entry_date = #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString))
                    exp += ((stexp * currfutl) / currfutlp)
                    exp += ((stexp1 * currfuts) / currfutsp)
                    If erow("entry_date") = Today.Date Then
                        toExp = exp
                    Else
                        prExp += exp
                    End If
                Next
            Else 'for options
                stexp = 0
                stexp1 = 0
                'dv = New DataView(GdtCurrencyTrades, "script='" & drow("script") & "' and cp in ('C','P')", "", DataViewRowState.CurrentRows)
                'dtdate = dv.ToTable(True, "entry_date")

                dv = New DataView(gdtcurr, "script='" & drow("script") & "' and cp  in('C','P')", "", DataViewRowState.CurrentRows)
                dtdate = dv.ToTable(True, "entry_date")
                'For Each erow As DataRow In New DataView(GdtCurrencyTrades, "script='" & drow("script") & "' and cp in ('C','P') And company='" & drow("company") & "'", "", DataViewRowState.CurrentRows).ToTable(True, "entry_date").Rows 'dtdate.Rows
                For Each erow As DataRow In dtdate.Rows
                    exp = 0
                    If Val(currspl) <> 0 Then
                        stexp = 0
                        stexp1 = 0
                        REM tot2 Field  = (Rate + StrikePrice) * Qty Update By Viral
                        stexp = Val(gdtcurr.Compute("sum(tot2)", "cp  in ('C','P')  and qty > 0 and script = '" & drow("script") & "' and entry_date = #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString)
                        stexp1 = Math.Abs(Val(gdtcurr.Compute("sum(tot2)", "cp  in ('C','P') and qty < 0 and script = '" & drow("script") & "' and entry_date = #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString))
                        exp += ((stexp * currspl) / currsplp)
                        exp += ((stexp1 * currsps) / currspsp)
                    Else
                        stexp = 0
                        stexp1 = 0
                        stexp = Val(gdtcurr.Compute("sum(tot)", "cp  in ('C','P') and qty > 0 and script = '" & drow("script") & "' and entry_date = #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString)
                        stexp1 = Math.Abs(Val(gdtcurr.Compute("sum(tot)", "cp  in ('C','P') and  qty < 0 and script = '" & drow("script") & "' and entry_date = #" & Format(CDate(erow("entry_date")), "dd-MMM-yyyy") & "# And company='" & drow("company") & "'").ToString))
                        exp += ((stexp * currprel) / currprelp)
                        exp += ((stexp1 * currpres) / currpresp)
                    End If
                    If erow("entry_date") = Today.Date Then
                        toExp = exp
                    Else
                        prExp += exp
                    End If
                Next
            End If
        End If
    End Sub

#End Region
    ''' <summary>
    ''' expense variables are used for expense calculation
    ''' if they update then this variables are updated
    ''' </summary>
#Region "expense"
    Dim objExp As New expenses
    Public exptable As New DataTable
    Public dbl, dbs, ndbl, ndbs, futs, futl, spl, sps, prel, pres, currfutl, currfuts, currspl, currsps, currprel, currpres As Double
    Public dblp, dbsp, ndblp, ndbsp, futsp, futlp, splp, spsp, prelp, presp, currfutlp, currfutsp, currsplp, currspsp, currprelp, currpresp As Double
    Public eqInterest, FoInterest, sttRate As Double
    Public Sub fill_expense()
        'fill expense settings
        exptable = objExp.Select_Expenses
        If exptable.Rows.Count = 0 Then
            Dim qry As String = "Insert Into expenses_setting (ndbl, ndblp, ndbs, ndbsp, dbl, dblp, dbs, dbsp, futl, futlp, futs, futsp, spl, splp, sps , spsp, prel, prelp, pres, presp, currfutl , currfutlp , currfuts, currfutsp , currspl , currsplp , currsps , currspsp , currprel , currprelp , currpres , currpresp , equity , fo, sttrate) values (0, 0,0,0, 0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 ,0 ,0 ,0,0 ,0 ,0 ,0, 0 ,0,0,0);"
            data_access.ParamClear()
            data_access.Cmd_Text = qry
            data_access.cmd_type = CommandType.Text
            data_access.ExecuteNonQuery()
            data_access.cmd_type = CommandType.StoredProcedure
            exptable = objExp.Select_Expenses
            MessageBox.Show("Set Your Expense From Setting form...")
            Dim analysis1 As New frmSettings

            For Each frm As Form In MDI.MdiChildren
                frm.Close()
            Next
            analysis1.ShowForm(1)
            analysis1.Close()
        End If
        If exptable.Rows.Count > 0 Then
            'delivery base
            dbl = Val(exptable.Rows(0).Item("dbl").ToString)
            dblp = Val(exptable.Rows(0).Item("dblp").ToString)
            dbs = Val(exptable.Rows(0).Item("dbs").ToString)
            dbsp = Val(exptable.Rows(0).Item("dbsp").ToString)

            'non delivery base
            ndbl = Val(exptable.Rows(0).Item("ndbl").ToString)
            ndblp = Val(exptable.Rows(0).Item("ndblp").ToString)
            ndbs = Val(exptable.Rows(0).Item("ndbs").ToString)
            ndbsp = Val(exptable.Rows(0).Item("ndbsp").ToString)

            'future
            futl = Val(exptable.Rows(0).Item("futl").ToString)
            futs = Val(exptable.Rows(0).Item("futs").ToString)
            futlp = Val(exptable.Rows(0).Item("futlp").ToString)
            futsp = Val(exptable.Rows(0).Item("futsp").ToString)

            'option
            spl = Val(exptable.Rows(0).Item("spl").ToString)
            splp = Val(exptable.Rows(0).Item("splp").ToString)
            sps = Val(exptable.Rows(0).Item("sps").ToString)
            spsp = Val(exptable.Rows(0).Item("spsp").ToString)
            prel = Val(exptable.Rows(0).Item("prel").ToString)
            prelp = Val(exptable.Rows(0).Item("prelp").ToString)
            pres = Val(exptable.Rows(0).Item("pres").ToString)
            presp = Val(exptable.Rows(0).Item("presp").ToString)

            'currency future
            currfutl = Val(exptable.Rows(0).Item("currfutl").ToString)
            currfuts = Val(exptable.Rows(0).Item("currfuts").ToString)
            currfutlp = Val(exptable.Rows(0).Item("currfutlp").ToString)
            currfutsp = Val(exptable.Rows(0).Item("currfutsp").ToString)
            'currency option
            currspl = Val(exptable.Rows(0).Item("currspl").ToString)
            currsplp = Val(exptable.Rows(0).Item("currsplp").ToString)
            currsps = Val(exptable.Rows(0).Item("currsps").ToString)
            currspsp = Val(exptable.Rows(0).Item("currspsp").ToString)
            currprel = Val(exptable.Rows(0).Item("currprel").ToString)
            currprelp = Val(exptable.Rows(0).Item("currprelp").ToString)
            currpres = Val(exptable.Rows(0).Item("currpres").ToString)
            currpresp = Val(exptable.Rows(0).Item("currpresp").ToString)

            'interest
            eqInterest = Val(exptable.Rows(0).Item("equity").ToString)
            FoInterest = Val(exptable.Rows(0).Item("fo").ToString)
            sttRate = Val(exptable.Rows(0).Item("sttrate").ToString)
        End If
    End Sub


    'Public Sub fill_expense()
    '    'fill expense settings
    '    exptable = objExp.Select_Expenses
    '    If exptable.Rows.Count > 0 Then
    '        'delivery base
    '        dbl = exptable.Rows(0).Item("dbl")
    '        dblp = exptable.Rows(0).Item("dblp")
    '        dbs = exptable.Rows(0).Item("dbs")
    '        dbsp = exptable.Rows(0).Item("dbsp")

    '        'non delivery base
    '        ndbl = exptable.Rows(0).Item("ndbl")
    '        ndblp = exptable.Rows(0).Item("ndblp")
    '        ndbs = exptable.Rows(0).Item("ndbs")
    '        ndbsp = exptable.Rows(0).Item("ndbsp")

    '        'future
    '        futl = exptable.Rows(0).Item("futl")
    '        futs = exptable.Rows(0).Item("futs")
    '        futlp = exptable.Rows(0).Item("futlp")
    '        futsp = exptable.Rows(0).Item("futsp")

    '        'option
    '        spl = exptable.Rows(0).Item("spl")
    '        splp = exptable.Rows(0).Item("splp")
    '        sps = exptable.Rows(0).Item("sps")
    '        spsp = exptable.Rows(0).Item("spsp")
    '        prel = exptable.Rows(0).Item("prel")
    '        prelp = exptable.Rows(0).Item("prelp")
    '        pres = exptable.Rows(0).Item("pres")
    '        presp = exptable.Rows(0).Item("presp")

    '        'currency future
    '        currfutl = exptable.Rows(0).Item("currfutl")
    '        currfuts = exptable.Rows(0).Item("currfuts")
    '        currfutlp = exptable.Rows(0).Item("currfutlp")
    '        currfutsp = exptable.Rows(0).Item("currfutsp")
    '        'currency option
    '        currspl = exptable.Rows(0).Item("currspl")
    '        currsplp = exptable.Rows(0).Item("currsplp")
    '        currsps = exptable.Rows(0).Item("currsps")
    '        currspsp = exptable.Rows(0).Item("currspsp")
    '        currprel = exptable.Rows(0).Item("currprel")
    '        currprelp = exptable.Rows(0).Item("currprelp")
    '        currpres = exptable.Rows(0).Item("currpres")
    '        currpresp = exptable.Rows(0).Item("currpresp")

    '        'interest
    '        eqInterest = exptable.Rows(0).Item("equity")
    '        FoInterest = exptable.Rows(0).Item("fo")
    '        sttRate = exptable.Rows(0).Item("sttrate")
    '    End If

    'End Sub
#End Region

    Public Sub init_scenario()

        scenariotable = New DataTable
        With scenariotable.Columns
            .Add("status", GetType(Boolean))
            .Add("time1", GetType(Date))
            .Add("time2", GetType(Date))
            .Add("cpf")
            .Add("spot", GetType(Double))
            .Add("strikes", GetType(Double))
            .Add("qty", GetType(Double))
            .Add("rate", GetType(Double))
            .Add("ltp", GetType(Double))
            .Add("vol", GetType(Double))
            .Add("delta", GetType(Double))
            .Add("deltaval", GetType(Double))
            .Add("theta", GetType(Double))
            .Add("thetaval", GetType(Double))
            .Add("vega", GetType(Double))
            .Add("vgval", GetType(Double))
            .Add("gamma", GetType(Double))
            .Add("gmval", GetType(Double))
            .Add("volga", GetType(Double))
            .Add("volgaval", GetType(Double))
            .Add("vanna", GetType(Double))
            .Add("vannaval", GetType(Double))

            ''divyesh

            .Add("deltaval1", GetType(Double))
            .Add("thetaval1", GetType(Double))
            .Add("vgval1", GetType(Double))
            .Add("gmval1", GetType(Double))
            .Add("volgaval1", GetType(Double))
            .Add("vannaval1", GetType(Double))
            .Add("ltp1", GetType(Double))

            .Add("uid", GetType(Integer))
            .Add("grossMTM", GetType(Double))
            .Add("DifFactor", GetType(Double))
            REM DifFactor  By Viral 1Oct

        End With
    End Sub
    Public Sub init_table_BEPNEG()



        BEPtableNEG = New DataTable
        With BEPtableNEG.Columns
            .Add("Symbol", GetType(String))
            .Add("Spot", GetType(Double))
            .Add("Value", GetType(Double))


        End With


    End Sub
    Public Sub init_table_BEP()

        BEPtable = New DataTable
        With BEPtable.Columns
            .Add("Symbol", GetType(String))
            .Add("Spot", GetType(Double))
            .Add("Value", GetType(Double))


        End With




    End Sub
    Public Sub numonly(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        If Char.IsDigit(e.KeyChar) = False Then
            If Char.IsLetter(e.KeyChar) = True Then
                e.Handled = True
            End If

            If Char.IsWhiteSpace(e.KeyChar) = False Then
                Dim arr As New ArrayList
                arr.Add(Asc("-"))
                arr.Add(Asc("+"))
                arr.Add(Asc("."))
                arr.Add(8)
                If Not arr.Contains(Asc(e.KeyChar)) Then
                    e.Handled = True
                End If

            End If
        End If
    End Sub
    Public Sub dateonly(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        If Char.IsLetterOrDigit(e.KeyChar) Or e.KeyChar = Chr(Keys.Back) Then
            e.Handled = False
        Else
            Dim arr As New ArrayList
            arr.Add(Asc("-"))
            arr.Add(Asc("/"))
            If Not arr.Contains(Asc(e.KeyChar)) Then
                e.Handled = True
            End If
        End If
        'If Char.IsDigit(e.KeyChar) = False Then
        '    If Char.IsLetter(e.KeyChar) = True Then
        '        e.Handled = True
        '    End If

        '    If Char.IsWhiteSpace(e.KeyChar) = False Then
        '        Dim arr As New ArrayList
        '        arr.Add(Asc("/"))
        '        arr.Add(8)
        '        If Not arr.Contains(Asc(e.KeyChar)) Then
        '            e.Handled = True
        '        End If

        '    End If
        'End If
        'If Not Char.IsControl(e.KeyChar) AndAlso _
        '               ((e.KeyChar < "0" And e.KeyChar > "9") Or (e.KeyChar > "A" And e.Char < "z")) Then
        '    e.Handled = True
        'End If
    End Sub
    Public Function UDDateDiff(ByVal DI As DateInterval, ByVal Date1 As Date, ByVal Date2 As Date) As Long
        If Date2 = CDate(dCDate) Then
            Date2 = DateAdd(DateInterval.Day, iCDelay, Date2)
        ElseIf Date2 = CDate(dNDate) Then
            Date2 = DateAdd(DateInterval.Day, iNDelay, Date2)
        ElseIf Date2 = CDate(dFDate) Then
            Date2 = DateAdd(DateInterval.Day, iFDelay, Date2)
        End If



        'Dim date1 As Date = Date.Now
        'Dim date2 As Date = date1.AddDays(4.0#)

        'Dim span = date2 - date1

        'Dim days As Double = span.TotalDays '=4
        Dim daycount As Double = 0
        If INCLUDEEXPIRY_VOLANDGREEK_CAL = 1 Then
            daycount = DateDiff(DI, Date1, Date2) + 1
        Else
            daycount = DateDiff(DI, Date1, Date2)
        End If

        Return daycount
        'Return DateDiff(DI, Date1, Date2)
    End Function
    Public Sub Update_TradingVol()

        If flgupdatetradevol = False Then
            flgupdatetradevol = True
            For Each drow As DataRow In GdtFOTrades.Select("tradingvol is null")
                Dim ftoken As Long = 0
                Dim tk As Long = 0
                Dim IsCurrency As Boolean = False
                Try


                    Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(drow("company")) & "' AND option_type='XX'and expdate1>=#" & Format(drow("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                    If DtFMonthDate1.Rows.Count > 0 Then
                        ftoken = DtFMonthDate1.Rows(0)("token")

                    Else
                        Dim DtFMonthDate As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(drow("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                        If DtFMonthDate.Rows.Count > 0 Then
                            If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                                ftoken = DtFMonthDate.Rows(0)("token")

                            ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" And DtFMonthDate.Rows.Count > 1 Then
                                ftoken = DtFMonthDate.Rows(1)("token")

                            ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" And DtFMonthDate.Rows.Count > 2 Then
                                ftoken = DtFMonthDate.Rows(2)("token")

                            End If
                        End If
                    End If
                    If cpfmaster.Compute("max(token)", "script='" & drow("script") & "'").ToString() = "" Then
                        tk = 0
                    Else
                        tk = CLng(cpfmaster.Compute("max(token)", "script='" & drow("script") & "'").ToString)
                    End If

                    If GdtCurrencyTrades.Select("Script='" & drow("script") & "' And company='" & drow("company") & "'").Length > 0 Then
                        IsCurrency = True
                    Else
                        IsCurrency = False
                    End If
                Catch ex As Exception

                End Try

                Dim VOL As Double = 0
                VOL = Get_Vol(ftoken, tk, drow("token1"), drow("cp"), IsCurrency, drow("mdate"), drow("strikerate"))
                If VOL <> 0 And VOL <> 360 Then
                    objTrad.Exec_Qry("Update trading Set TradingVol = " & VOL & "  Where mdate=#" & CDate(drow("mdate")) & "# and script='" & drow("script") & "' and TradingVol is null ")
                    drow("tradingvol") = VOL
                End If


            Next
            flgupdatetradevol = False

        End If

    End Sub
    Public Function Get_Vol(ByVal ftoken As Long, ByVal tokanno As Long, ByVal token1 As Long, ByVal cp As String, ByVal IsCurrency As Boolean, ByVal Mdate As Date, ByVal stkprice As Double) As Double
        Try


            Dim tokenvol As Double = 0
            Dim tokenvol1 As Double = 0
            Dim vol As Double = 0
            Dim isfut As Boolean = False
            Dim iscall As Boolean = False

            Dim ltpprvol As Double = 0
            Dim fltppr As Double = 0

            Dim ltpprvol1 As Double = 0
            If ((Not FoRegTokens.Contains(ftoken))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                Objsql.AppendFoTokens(ftoken)
                FoRegTokens.Add(ftoken, 0)
                ' FoTokens.Add(ftoken, 0)
            End If
            If ((Not FoRegTokens.Contains(tokanno))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                Objsql.AppendFoTokens(tokanno)
                FoRegTokens.Add(tokanno, 0)
                'FoTokens.Add(ftoken, 0)
            End If
            If ((Not FoRegTokens.Contains(token1))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                Objsql.AppendFoTokens(token1)
                FoRegTokens.Add(token1, 0)
                ' FoTokens.Add(ftoken, 0)
            End If
            Dim i As Integer = 0


            ' tokenvol = CLng(dr("tokanno"))
            'tokenvol1 = CLng(dr("token1"))
            fltppr = Val(fltpprice(CLng(ftoken)))



            ltpprvol = Val(ltpprice(tokanno))
            ltpprvol1 = 0 'Val(ltpprice(CLng(token1)))
            Dim _mt, _mt1 As Double
            If cp = "C" Then
                iscall = True
            ElseIf cp = "P" Then
                iscall = False
            Else
                isfut = True
            End If

            Dim noday As Integer
            noday = NoofDay
            If noday > 1 Then
                noday = noday
                'ElseIf noday = 1 And UCase(WeekdayName(Weekday(Now))) = "FRIDAY" Then
            ElseIf noday = 1 And UCase(DateTime.Now.DayOfWeek.ToString()) = "FRIDAY" Then
                noday = 3
            Else
                noday = 1
            End If
            Dim mt As Double = 0
            Dim mmt As Double = 0
            mt = UDDateDiff(DateInterval.Day, Now.Date, CDate(Mdate).Date)
            mmt = UDDateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, CInt(noday), Now())).Date, CDate(Mdate).Date)
            If Now.Date = CDate(Mdate).Date Then
                mt += 0.5
            End If
            If CDate(DateAdd(DateInterval.Day, CInt(noday), Now())).Date = CDate(Mdate).Date Then
                mmt += 0.5
            End If


            If DAYTIME_VOLANDGREEK_CAL = 1 Then
                _mt = Get_DayTime_mt(CDate(Mdate).Date, _mt1)
            Else
                If mt = 0 Then
                    _mt = 0.00001
                    _mt1 = 0.00001
                Else
                    _mt = (mt) / 365
                    '_mt1 = ((mT + 1) - CInt(txtnoofday.Text)) / 365
                    _mt1 = (mmt) / 365
                End If
            End If
            If ltpprvol1 = 0 Then 'if token1's LTP is zero when isliq=false
                If IsCurrency = False Then
                    vol = Greeks.Black_Scholes(fltppr, stkprice, Rateofinterest, 0, ltpprvol, _mt, iscall, isfut, 0, 6)
                Else
                    vol = Greeks.Black_Scholes(fltppr, stkprice, CurrencyRateOfInterest, 0, ltpprvol, _mt, iscall, isfut, 0, 6)
                End If
                '  mVolatility1 = 0
            Else 'if token1's LTP<>0 when isliq=True
                'mVolatility1 = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, mIsCall, mIsFut, 0, 6)
                If IsCurrency = False Then
                    If iscall = True Then
                        vol = Greeks.Black_Scholes(fltppr, stkprice, Rateofinterest, 0, ltpprvol1, _mt, False, isfut, 0, 6)
                        ' mVolatility1 = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, True, mIsFut, 0, 6)
                    Else
                        vol = Greeks.Black_Scholes(fltppr, stkprice, Rateofinterest, 0, ltpprvol1, _mt, True, isfut, 0, 6)
                        ' mVolatility1 = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, False, mIsFut, 0, 6)
                    End If
                Else
                    If iscall = True Then
                        vol = Greeks.Black_Scholes(fltppr, stkprice, CurrencyRateOfInterest, 0, ltpprvol1, _mt, False, isfut, 0, 6)
                        ' mVolatility1 = Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, tmpcpprice, _mt, True, mIsFut, 0, 6)
                    Else
                        vol = Greeks.Black_Scholes(fltppr, stkprice, CurrencyRateOfInterest, 0, ltpprvol1, _mt, True, isfut, 0, 6)
                        'mVolatility1 = Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, tmpcpprice, _mt, False, mIsFut, 0, 6)
                    End If
                End If

            End If
            Return Math.Round(vol * 100, 2)
        Catch ex As Exception
            Return 0
        End Try
    End Function



    Public Function Get_DayTime_mt(ByVal Mdate As Date, ByRef _mt1 As Double) As Double
        Dim BCast As Date
        'DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss")
        If CAL_GREEK_WITH_BCASTDATE = 1 Then
            BCast = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
        Else
            If VarFoBCurrentDate = 0 Or VarFoBCurrentDate < 0 Then
                BCast = Now
            Else
                BCast = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
            End If
        End If


        Dim startDate As String = "9:15"
        Dim tsStart = TimeSpan.Parse(startDate)
        Dim mt As Double
        Dim mmt As Double
        Dim endDate As String = "15:30"
        Dim tsEnd = TimeSpan.Parse(endDate)
        If BCast.TimeOfDay >= tsStart AndAlso BCast.TimeOfDay <= tsEnd Then
            Dim mt1 As Double = 0
            mt1 = UDDateDiff(DateInterval.Day, BCast, CDate(Mdate).Date)
            'mt1 = 0
            Dim myDate = New DateTime(BCast.Year, BCast.Month, BCast.Day, 9, 15, 0, 0)
            Dim difference As TimeSpan = BCast - CDate(myDate)
            'round down total hours to integer'
            'Dim hours = Math.Floor(difference.TotalHours)
            Dim minutes = Math.Abs(difference.Minutes)
            ' Dim seconds = difference.Seconds

            'If mt1 = 0 Then
            '    mt = mt1 + (((hours + (minutes / 100)) * ((mt1 + 1))) / 6.15)

            'Else
            'Dim ss As Double = (6.15 - (hours + (minutes / 100))) / 6.15
            '100-(26*100/(6.25*60))
            Dim ss As Double = (100 - (minutes * 100 / (6.15 * 60))) / 100
            mt = mt1 + ss

            ' End If
        Else
            If BCast.TimeOfDay <= tsStart Then
                mt = UDDateDiff(DateInterval.Day, BCast, CDate(Mdate).Date)
                If mt = 0 Then
                    mt = 1

                Else
                    mt = UDDateDiff(DateInterval.Day, BCast, CDate(Mdate).Date)

                End If
            Else
                mt = UDDateDiff(DateInterval.Day, BCast, CDate(Mdate).Date)

            End If


        End If
        If mt > 0 Then
            mmt = mt - 1
            mt = mt / 365
            _mt1 = mmt / 365
        ElseIf mt <= 0 Then
            mt = 0.00001
            _mt1 = 0.00001
        End If



        Return mt
    End Function
    Private Declare Function ShellEx Lib "shell32.dll" Alias "ShellExecuteA" (
     ByVal hWnd As Integer, ByVal lpOperation As String,
     ByVal lpFile As String, ByVal lpParameters As String,
     ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer
    Dim tcpListener As TcpListener
    Public Sub exportExcel(ByVal dgv As DataGridView, ByVal FN As String)

        Dim R As Integer = 0, C As Integer = 0, rcount As Integer = 0, Buffer As String = Nothing, wStream As IO.StreamWriter = Nothing, m_RowsWritten As Integer = 0
        If dgv.AllowUserToAddRows = True Then
            rcount = 2
        Else
            rcount = 1
        End If
        Try
            wStream = New IO.StreamWriter(FN, False, System.Text.Encoding.ASCII)
            Try
                Buffer = ""
                For C = 0 To dgv.ColumnCount() - 1
                    Buffer += dgv.Columns(C).HeaderText.ToString.Replace(CChar(","), "")
                    Buffer += ","
                Next
                wStream.WriteLine(Buffer)

                For R = 0 To dgv.RowCount() - rcount
                    Buffer = ""
                    For C = 0 To dgv.ColumnCount() - 1
                        Buffer += dgv.Rows(R).Cells(C).Value.ToString.Replace(CChar(","), "")
                        If (C < dgv.ColumnCount() - 1) Then Buffer += ","
                    Next
                    wStream.WriteLine(Buffer)
                Next
            Catch ex As Exception
                MsgBox(String.Format("{0} Error while exporting Row {1} to {2}.", ex.Message(), R, FN), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Exporting Results to CSV")
            Finally
                m_RowsWritten = R
            End Try
        Catch ex As Exception
            MsgBox(String.Format("Unable to Open {0} for Writing.", FN), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Export Data to CSV File")
        Finally
            If (wStream IsNot Nothing) Then
                wStream.Close()
                wStream.Dispose()
            End If
        End Try



    End Sub
    Public Function GetFileName(ByVal mFilName As String, Optional ByVal today_bhavecopy_yes_no As Boolean = True) As String
        Dim mFileName As String = ""

        If today_bhavecopy_yes_no = True Then
            mFileName = Replace(mFilName, "DD", Format(Now.Day, "00"))
            mFileName = Replace(mFileName, "MM", Format(Now.Month, "00"))
            mFileName = Replace(mFileName, "YYYY", Format(Now.Year, "0000"))
        Else

            Dim temp_file_date As Date
            temp_file_date = DateAdd(DateInterval.Day, 1, Today.Date) 'Today.Date

            For i As Integer = 0 To 5
                temp_file_date = DateAdd(DateInterval.Day, -1, temp_file_date.Date)


                If temp_file_date.Date.DayOfWeek = DayOfWeek.Monday Then
                    mFileName = Replace(mFilName, "DD", Format(DateAdd(DateInterval.Day, -3, temp_file_date.Date).Day, "00"))
                    mFileName = Replace(mFileName, "MM", Format(DateAdd(DateInterval.Day, -3, temp_file_date.Date).Month, "00"))
                    mFileName = Replace(mFileName, "YYYY", Format(DateAdd(DateInterval.Day, -3, temp_file_date.Date).Year, "0000"))
                Else
                    mFileName = Replace(mFilName, "DD", Format(DateAdd(DateInterval.Day, -1, temp_file_date.Date).Day, "00"))
                    mFileName = Replace(mFileName, "MM", Format(DateAdd(DateInterval.Day, -1, temp_file_date.Date).Month, "00"))
                    mFileName = Replace(mFileName, "YYYY", Format(DateAdd(DateInterval.Day, -1, temp_file_date.Date).Year, "0000"))
                End If

                If (System.IO.File.Exists(mFileName)) Then
                    Exit For
                End If
            Next

        End If


        GetFileName = mFileName
    End Function
    Public Function Encry(ByVal str As String) As String
        Dim Tmp As String = ""
        Dim i As Integer

        For i = 1 To Len(str)
            Tmp = Tmp + Chr(Asc(Mid(str, i, 1)) + 7)
        Next i
        Encry = Tmp
    End Function
    Public Function Decry(ByVal str As String) As String
        Dim Tmp As String = ""
        Dim i As Integer

        For i = 1 To Len(str)
            Tmp = Tmp + Chr(Asc(Mid(str, i, 1)) - 7)
        Next i
        Decry = Tmp
    End Function

    Public Function GetFutToken(ByVal Token As Long) As Long
        Dim result As Long = 0
        For Each drow As DataRow In cpfmaster.Select("TOKEN =" & Token & "")
            For Each dr As DataRow In cpfmaster.Select("SCRIPT ='FUTSTK  " & drow("SYMBOL").ToString() & "  " & Format(drow("EXPDATE1"), "ddMMMyyyy") & "'")
                result = dr("TOKEN").ToString
            Next
        Next
        Return result
    End Function

    Public Function GetCurFutToken(ByVal Token As Long) As Long
        Dim result As Long = 0
        For Each drow As DataRow In Currencymaster.Select("TOKEN =" & Token & "")
            For Each dr As DataRow In Currencymaster.Select("SCRIPT ='FUTSTK  " & drow("SYMBOL").ToString() & "  " & Format(drow("EXPDATE1"), "ddMMMyyyy") & "'")
                result = dr("TOKEN").ToString
            Next
        Next
        Return result
    End Function
    Public Function FillTCPConnectionToSqlTOAcess()
        Try
            Dim trd1 As New trading
            Dim DtTCPConnSetting As DataTable
            '    Dim DtTCPConnSetting As DataTable = frmsetting.SelectdataofTcpConnection()
            Dim DtTCPConnSetting_maxno As DataTable = frmsetting.SelectdataofTcpConnection_maxno()
            Dim maxnosql, maxnoaccess, minnoaccess, totsqlcount, totaccesscount As Integer
            If dtAccess Is Nothing = True Then
                dtAccess = trd1.select_TCP_Connection
            End If

            If dtAccess.Rows.Count > 0 Then
                maxnoaccess = dtAccess.Compute("max(Maxno)", "")
                totaccesscount = dtAccess.Compute("count(Maxno)", "")
            Else
                maxnoaccess = 0
                totaccesscount = 0
            End If


            If DtTCPConnSetting_maxno Is Nothing = False Then
                maxnosql = DtTCPConnSetting_maxno.Compute("max(Maxno)", "")
                totsqlcount = DtTCPConnSetting_maxno.Compute("Count(Maxno)", "")

            Else
                totsqlcount = 0
                'maxnoaccess = dtAccess.Compute("max(Maxno)", "")
                If dtAccess.Rows.Count > 0 Then
                    maxnoaccess = dtAccess.Compute("max(Maxno)", "")
                    minnoaccess = dtAccess.Compute("min(Maxno)", "")
                    totaccesscount = dtAccess.Compute("count(Maxno)", "")

                Else
                    maxnoaccess = 0
                    minnoaccess = 0
                    totaccesscount = 0
                End If
            End If
            If minnoaccess <> maxnoaccess Or totaccesscount <> totsqlcount Then
                If DtTCPConnSetting_maxno Is Nothing = False Then
                    ' trd.Delete_TCPConnection()
                    ' Dim trd2 As New trading
                    DtTCPConnSetting = frmsetting.SelectdataofTcpConnection()
                    trd1.Insert_TCP_Connection(DtTCPConnSetting)
                    dtAccess = trd1.select_TCP_Connection()
                    maxnoaccess = dtAccess.Compute("max(Maxno)", "")
                End If
            End If

            If maxnoaccess <> maxnosql Then
                If DtTCPConnSetting_maxno Is Nothing = False Then
                    ' trd.Delete_TCPConnection()
                    DtTCPConnSetting = frmsetting.SelectdataofTcpConnection()
                    trd1.Insert_TCP_Connection(DtTCPConnSetting)
                    dtAccess = trd1.select_TCP_Connection()
                End If
            End If
            flgFillTCPConnectionToSqlTOAcess = True
        Catch ex As Exception

        End Try
    End Function
    Public Sub RefreshTokenfromcontract()
        'Try


        '    For Each drow As DataRow In maintable.Select("")
        '        Dim DtFMonthDate As DataTable = New DataView(cpfmaster, "Symbol='" & drow("company") & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
        '        Dim ftoken As Long
        '        If DtFMonthDate.Rows.Count > 0 Then
        '            If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
        '                drow("ftoken") = DtFMonthDate.Rows(0)("token")


        '                'dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
        '            ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
        '                drow("ftoken") = DtFMonthDate.Rows(1)("token")
        '                'dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
        '            ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
        '                drow("ftoken") = DtFMonthDate.Rows(2)("token")
        '                'dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
        '            End If
        '        End If

        '        Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "script='" & drow("script") & "' ", "expdate1", DataViewRowState.CurrentRows).ToTable()

        '        If DtFMonthDate1.Rows.Count > 0 Then
        '            drow("tokanno") = DtFMonthDate1.Rows(0)("token")
        '        End If



        '    Next
        'Catch ex As Exception

        'End Try

        Try

            For Each drow As DataRow In maintable.Select("")

                If drow("IsCurrency") = False Then
                    'Jignesh
                    If drow("cp") = "E" Then
                        If cpfmaster.Select("Symbol='" & GetSymbol(drow("company")) & "' AND option_type='XX' AND expdate1>=#" & Format(Today, "dd-MMM-yyyy") & "#").Length > 0 Then
                            drow("fut_mdate") = cpfmaster.Compute("MIN(expdate1)", "Symbol='" & GetSymbol(drow("company")) & "' AND option_type='XX'")
                            drow("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(drow("company")) & "' and expdate1=#" & Format(drow("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                        Else
                            drow("fut_mdate") = Today
                            drow("ftoken") = 0
                        End If
                    Else
                        drow("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(drow("company")) & "' and expdate1=#" & Format(drow("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                        If drow("ftoken") = 0 Then

                            Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(drow("company")) & "' AND option_type='XX'and expdate1>=#" & Format(drow("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If DtFMonthDate1.Rows.Count > 0 Then
                                drow("ftoken") = DtFMonthDate1.Rows(0)("token")
                                drow("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                            Else
                                Dim DtFMonthDate As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(drow("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                                If DtFMonthDate.Rows.Count > 0 Then
                                    If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                                        drow("ftoken") = DtFMonthDate.Rows(0)("token")
                                        drow("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                                        drow("ftoken") = DtFMonthDate.Rows(1)("token")
                                        drow("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                                    ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                                        drow("ftoken") = DtFMonthDate.Rows(2)("token")
                                        drow("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                    End If
                                End If
                            End If


                        End If
                    End If
                ElseIf drow("IsCurrency") = True Then REM Is Currency Position
                    'Jignesh
                    drow("ftoken") = Val(Currencymaster.Compute("max(token)", "Symbol='" & GetSymbol(drow("company")) & "' and expdate1=#" & Format(drow("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)
                    If drow("ftoken") = 0 Then

                        Dim DtFMonthDate1 As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(drow("company")) & "' AND option_type='XX'and expdate1>=#" & Format(drow("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                        If DtFMonthDate1.Rows.Count > 0 Then
                            drow("ftoken") = DtFMonthDate1.Rows(0)("token")
                            drow("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
                        Else

                            Dim DtFMonthDate As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(drow("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                            If DtFMonthDate.Rows.Count > 0 Then
                                If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                                    drow("ftoken") = DtFMonthDate.Rows(0)("token")
                                    drow("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                                ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                                    drow("ftoken") = DtFMonthDate.Rows(1)("token")
                                    drow("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                                ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                                    drow("ftoken") = DtFMonthDate.Rows(2)("token")
                                    drow("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                                End If
                            End If
                        End If


                    End If
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub
    Public Sub fill_token()
        filltokenflg = True
        objTrad.Exec_Qry("Update Contract Set script = replace(script,',','.')")
        objTrad.Exec_Qry("Update Currency_Contract Set script = replace(script,',','.')")

        'cpfmaster = New DataTable
        'eqmaster = New DataTable
        'Currencymaster = New DataTable

        'cpfmaster = objTrad.Script()
        'eqmaster = objTrad.select_security()
        'Currencymaster = objTrad.SELECTED_Currency_Contract()

        'FillDataTable(cpfmaster, "Select * From generate_script")
        'FillDataTable(eqmaster, "Select * From selected_security")
        'FillDataTable(Currencymaster, "Select * From SELECTED_Currency_Contract")


        'futtoken = New ArrayList
        'For Each drow As DataRow In cpfmaster.Select("(option_type ='XX' or option_type='' or option_type=null)")
        '    futtoken.Add(CLng(drow("token")))
        'Next

        'Currfuttoken = New ArrayList
        'For Each drow As DataRow In Currencymaster.Select("(option_type ='XX' or option_type='' OR option_type=null)")
        '    Currfuttoken.Add(CLng(drow("token")))
        'Next

        'eqtoken = New ArrayList
        'For Each drow As DataRow In eqmaster.Rows
        '    eqtoken.Add(CLng(drow("token")))
        'Next
        Dim ttik As Int64 = DateTime.Now.Ticks
        Write_TimeLog1("Fill_token Start..")
        Dim objSt As Struct_FOContract
        Dim objEQ As Struct_EQContract
        Dim objCURR As Struct_CURRContract

        cpfmaster = New DataTable
        objTrad.Script(cpfmaster)


        eqmaster = New DataTable
        'eqmaster = objTrad.select_security
        objTrad.select_security(eqmaster)


        Currencymaster = New DataTable
        '  objTrad.SELECTED_Currency_Contract(Currencymaster)

        Currencymaster = objTrad.SELECTED_Currency_Contract


        If Currencymaster.Columns.Contains("ftoken") = False Then
            Currencymaster.Columns.Add("ftoken", GetType(Long))
        End If

        HT_FOLotContrct.Clear()
        Dim HT_CurrNextFar1 As New Hashtable
        Dim HT_CurrNextFarcurr1 As New Hashtable
        HT_CurrNextFar.Clear()
        futtoken = New ArrayList

        Dim Dtstrikediff As DataTable = New DataView(cpfmaster, "option_type='CE'", "Symbol,STRIKE_PRICE Desc", DataViewRowState.CurrentRows).ToTable(True, "Symbol")

        Try



            For Each dr As DataRow In Dtstrikediff.Rows
                Dim expstrike As DataRow = cpfmaster.Select("option_type ='XX' and symbol='" & dr("symbol") & "' ", "expdate1")(0)
                Dim nextmdate As DataRow = cpfmaster.Select("symbol='" & dr("symbol") & "' and expdate1='" + expstrike("expdate1") + "' and option_Type='CE' ", "strike_price desc")(1)
                Dim st1 As Double = nextmdate.Item("strike_price")



                nextmdate = cpfmaster.Select("symbol='" & dr("symbol") & "' and option_Type='CE' and expdate1='" + expstrike("expdate1") + "'  and strike_price <'" & st1 & "' ", "strike_price desc")(0)

                Dim st2 As Double = nextmdate.Item("strike_price")
                If HT_strikediff.Contains(dr("symbol")) = False Then
                    'If dr("symbol") = "NIFTY" Then
                    '    HT_strikediff.Add(dr("symbol"), Math.Abs(st2 - st1))
                    'End If

                    HT_strikediff.Add(dr("symbol"), Math.Abs(st2 - st1))
                End If


            Next
        Catch ex As Exception
        End Try


        'Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "option_type='XX'", "Symbol,Expdate Desc", DataViewRowState.CurrentRows).ToTable(True, "expdate", "token", "Symbol")
        'Dim i As Integer = 1
        'Dim str As String = ""
        'For Each drow As DataRow In DtFMonthDate1.Select("")
        '    If HT_CurrNextFar.Contains(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy")) = False Then
        '        If i = 1 Then
        '            str = drow("Symbol")
        '        Else
        '            If drow("Symbol") <> str Then
        '                i = 1
        '                str = drow("Symbol")
        '            End If
        '        End If

        '        HT_CurrNextFar.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy"), i)
        '        HT_CurrNextFar1.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy"), drow("Token"))
        '        i = i + 1
        '        str = drow("Symbol")
        '    End If

        'Next


        'TODO

        Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "option_type='XX'", "Symbol,Expdate Desc", DataViewRowState.CurrentRows).ToTable(True, "expdate", "token", "Symbol", "exchange")
        Dim i As Integer = 1
        Dim str As String = ""
        Dim strScriptNextFar As String
        For Each drow As DataRow In DtFMonthDate1.Select("")
            strScriptNextFar = drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy") & "  " & drow("exchange")
            If HT_CurrNextFar.Contains(strScriptNextFar) = False Then
                If i = 1 Then
                    str = drow("Symbol")
                Else
                    If drow("Symbol") <> str Then
                        i = 1
                        str = drow("Symbol")
                    End If
                End If
                Dim sym As String = drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy")
                Dim sym1 As String = drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy")
                If Not HT_CurrNextFar.Contains(sym) Then
                    HT_CurrNextFar.Add(sym, i)
                    HT_CurrNextFar1.Add(sym1, drow("Token"))
                End If


                i = i + 1
                str = drow("Symbol")
                End If

        Next



        Dim DtFMonthDatecurr As DataTable = New DataView(Currencymaster, "option_type='XX'", "Symbol,Expdate Desc", DataViewRowState.CurrentRows).ToTable(True, "expdate", "token", "Symbol")
        Dim icurr As Integer = 1
        Dim strcurr As String = ""
        For Each drow As DataRow In DtFMonthDatecurr.Select("")
            If HT_CurrNextFarcurr.Contains(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy")) = False Then
                If i = 1 Then
                    str = drow("Symbol")
                Else
                    If drow("Symbol") <> str Then
                        i = 1
                        str = drow("Symbol")
                    End If
                End If

                HT_CurrNextFarcurr.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy"), i)
                HT_CurrNextFarcurr1.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy"), drow("Token"))
                i = i + 1
                str = drow("Symbol")
            End If

        Next
        '   Dim drcpfmastercurr() As DataRow = Currencymaster.Select("")
        For Each drow As DataRow In Currencymaster.Rows
            If drow("Ftoken").ToString() = "" Then
                drow("ftoken") = Convert.ToDouble(HT_CurrNextFarcurr1(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy")))
            End If
        Next
        Dim drcpfmaster() As DataRow = cpfmaster.Select("")
        For Each drow As DataRow In drcpfmaster
            Dim strApiIdentifier As String = ""
            Dim exchange As String = drow("exchange").ToString()
            If drow("option_type") = "XX" Then
                If CDate(drow("ExpDate").ToString()).Month = Now.Month Then
                    If CDate(drow("ExpDate").ToString()) >= Now.Date Then
                        strApiIdentifier = drow("Symbol").ToString().ToUpper() & "-I"
                    Else
                        strApiIdentifier = drow("Symbol").ToString().ToUpper() & "-"
                    End If
                ElseIf CDate(drow("ExpDate").ToString()).Month = Now.AddMonths(1).Month Then
                    strApiIdentifier = drow("Symbol").ToString().ToUpper() & "-II"
                ElseIf CDate(drow("ExpDate").ToString()).Month = Now.AddMonths(2).Month Then
                    strApiIdentifier = drow("Symbol").ToString().ToUpper() & "-III"
                End If
            Else
                strApiIdentifier = drow("InstrumentName").ToString().ToUpper() & "_" & drow("Symbol").ToString().ToUpper() & "_" & drow("ExpDate").ToString().ToUpper().Replace("/", "") & "_" & drow("option_type").ToString().ToUpper() & "_" & drow("Strike_Price").ToString()
            End If



            If strApiIdentifier.Trim.Length > 0 Then
                If Not HT_GetTokanFromIdentifier.Contains(strApiIdentifier) Then
                    HT_GetTokanFromIdentifier.Add(strApiIdentifier, CLng(drow("Token")))
                End If

                If Not HT_GetIdentifierFromTokan.Contains(CLng(drow("Token"))) Then
                    HT_GetIdentifierFromTokan.Add(CLng(drow("Token")), strApiIdentifier)
                End If
            End If


            Dim strFoContractScript As String = drow("Script").ToString().ToUpper() & "  " & exchange
            If Not HT_FOContrct.Contains(strFoContractScript) Then
                objSt = New Struct_FOContract
                objSt.lotsize = drow("lotsize")
                objSt.Token = drow("token")
                objSt.script = drow("script")
                objSt.asset_tokan = drow("asset_tokan")
                objSt.InstrumentType = drow("InstrumentName")
                objSt.symbol = drow("Symbol")
                objSt.exhange = exchange
                HT_FOContrct.Add(strFoContractScript, objSt)
            End If

            Dim strLotScript As String = drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("ddMMMyyyy") & "  " & exchange


            If HT_FOLotContrct.Contains(strLotScript) = False Then
                HT_FOLotContrct.Add(strLotScript, drow("lotsize").ToString())
            End If
            If HT_FOLotContrct.Contains(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("ddMMMyyyy")) = False Then
                HT_FOLotContrct.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("ddMMMyyyy"), drow("lotsize").ToString())
            End If
            If HT_FOTOKCEX.Contains(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("ddMMMyyyy") & drow("Option_type") & drow("strike_price")) = False Then
                HT_FOTOKCEX.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("ddMMMyyyy") & drow("Option_type") & drow("strike_price"), drow("token").ToString())
            End If

            If drow("Ftoken").ToString() = "" Then
                drow("ftoken") = Convert.ToDouble(HT_CurrNextFar1(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy")))
            End If
            If drow("option_type") = "XX" Then
                futtoken.Add(CLng(drow("token")))
            End If
            If Not HT_AssetToken.Contains(CLng(drow("Token"))) Then
                HT_AssetToken.Add(CLng(drow("Token")), CLng(drow("Asset_Tokan")))
            End If

            Dim strSymbolToken As String = drow("Symbol").ToString() & "  " & exchange
            Dim token As Long = CLng(drow("token"))

            If HT_symboltoken.Contains(strSymbolToken) = False Then
                HT_symboltoken.Add(strSymbolToken, drow("token"))
            End If

            If HT_TokenSymbol.ContainsKey(token) = False Then
                HT_TokenSymbol.Add(token, strSymbolToken)
            End If

        Next


        'Dim drcpfmaster1() As DataRow
        'drcpfmaster1 = cpfmaster.Select("(option_type ='XX' or option_type='' or option_type=null)")

        ''For Each drow As DataRow In cpfmaster.Select("(option_type ='XX' or option_type='' or option_type=null)")
        'For Each drow As DataRow In drcpfmaster1
        '    futtoken.Add(CLng(drow("token")))
        'Next

        Dim drCurrencymaster() As DataRow
        drCurrencymaster = Currencymaster.Select("(option_type ='XX' or option_type='' or option_type=null)")
        'For Each drow As DataRow In Currencymaster.Select("(option_type ='XX' or option_type='' OR option_type=null)")
        For Each drow As DataRow In drCurrencymaster
            Currfuttoken.Add(CLng(drow("token")))
        Next

        Dim strScript As String
        Dim strSymbol As String
        eqtoken = New ArrayList
        Dim dreqmaster() As DataRow = eqmaster.Select("")
        For Each drow As DataRow In dreqmaster
            eqtoken.Add(CLng(drow("token")))
            strScript = drow("Script").ToString().ToUpper() & "  " & drow("exchange").ToString()
            If Not HT_EQContrct.Contains(strScript) Then
                objEQ = New Struct_EQContract
                objEQ.Token = drow("token")
                objEQ.script = drow("script")
                objEQ.symbol = drow("Symbol")
                objEQ.exhange = drow("exchange").ToString()
                HT_EQContrct.Add(strScript, objEQ)
            End If
            strSymbol = drow("Symbol").ToString() & "  " & drow("exchange")
            If HT_symboltoken.Contains(strSymbol) = False Then
                HT_symboltoken.Add(strSymbol, drow("token"))
            End If

            If HT_TokenSymbol.ContainsKey(objEQ.Token) = False Then
                HT_TokenSymbol.Add(objEQ.Token, objEQ.symbol)
            End If
        Next
        Dim drCurrmaster() As DataRow = Currencymaster.Select("")
        For Each drow As DataRow In drCurrmaster
            'HT_CurrLotContrct.Add(drow("token"), drow("lotsize"))
            If HT_CurrLotContrct.Contains(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("ddMMMyyyy")) = False Then
                HT_CurrLotContrct.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("ddMMMyyyy"), drow("multiplier").ToString())
            End If
            If Not HT_CURRContrct.Contains(drow("Script").ToString().ToUpper()) Then
                objCURR = New Struct_CURRContract
                objCURR.Token = drow("token")
                objCURR.script = drow("script")
                objCURR.InstrumentType = drow("InstrumentName")
                objCURR.symbol = drow("Symbol")
                HT_CURRContrct.Add(drow("Script").ToString().ToUpper(), objCURR)
            End If

            If HT_symboltoken.Contains(drow("Symbol").ToString()) = False Then
                HT_symboltoken.Add(drow("Symbol").ToString(), drow("token"))
            End If

            Dim lnToken As Long = objEQ.Token
            If HT_TokenSymbol.ContainsKey(lnToken) = False Then
                HT_TokenSymbol.Add(lnToken, objEQ.symbol)
            End If

        Next




        Dim afonext() As DataRow = cpfmaster.Select("option_type <>'XX'", "Symbol,expdate1,option_type,strike_price")





        For i = 1 To afonext.Length - 1

            If HTfoNextStrilke.Contains(afonext(i)("script").ToString()) = False Then 'Because of Historical Contract
                Try
                    HTfoNextStrilke.Add(afonext(i)("script").ToString(), afonext(i + 1)("script").ToString())
                Catch ex As Exception

                End Try

            End If

            If HTfoPrevStrilke.Contains(afonext(i)("script").ToString()) = False Then 'Because of Historical Contract
                Try
                    HTfoPrevStrilke.Add(afonext(i)("script").ToString(), afonext(i - 1)("script").ToString())

                Catch ex As Exception

                End Try

            End If

        Next



        HtableCurSymbol = New Hashtable
        HtableCurSymbol.Add("EURINR", "EURINR")
        HtableCurSymbol.Add("EURUSD", "EURUSD")
        HtableCurSymbol.Add("GBPINR", "GBPINR")
        HtableCurSymbol.Add("GBPUSD", "GBPUSD")
        HtableCurSymbol.Add("JPYINR", "JPYINR")
        HtableCurSymbol.Add("USDINR", "USDINR")
        HtableCurSymbol.Add("USDJPY", "USDJPY")

        filltokenflg = False


        RefreshTokenfromcontract()


        'Dim dv As New DataView
        'dv = New DataView(cpfmaster1, "symbol='BANKNIFTY'", "", DataViewRowState.CurrentRows)
        Write_TimeLog1("Mainodule-> End Fun-Fill_token" + "|" + TimeSpan.FromTicks(DateTime.Now.Ticks - ttik).TotalSeconds.ToString())
    End Sub

    Public Sub fill_token_thr()
        filltokenflg = True
        Dim objTradt As trading = New trading
        'cpfmaster = New DataTable
        'eqmaster = New DataTable
        'Currencymaster = New DataTable

        'cpfmaster = objTrad.Script()
        'eqmaster = objTrad.select_security()
        'Currencymaster = objTrad.SELECTED_Currency_Contract()

        'FillDataTable(cpfmaster, "Select * From generate_script")
        'FillDataTable(eqmaster, "Select * From selected_security")
        'FillDataTable(Currencymaster, "Select * From SELECTED_Currency_Contract")


        'futtoken = New ArrayList
        'For Each drow As DataRow In cpfmaster.Select("(option_type ='XX' or option_type='' or option_type=null)")
        '    futtoken.Add(CLng(drow("token")))
        'Next

        'Currfuttoken = New ArrayList
        'For Each drow As DataRow In Currencymaster.Select("(option_type ='XX' or option_type='' OR option_type=null)")
        '    Currfuttoken.Add(CLng(drow("token")))
        'Next

        'eqtoken = New ArrayList
        'For Each drow As DataRow In eqmaster.Rows
        '    eqtoken.Add(CLng(drow("token")))
        'Next
        Dim ttik As Int64 = DateTime.Now.Ticks
        Write_TimeLog1("Fill_token Start..")
        Dim objSt As Struct_FOContract
        Dim objEQ As Struct_EQContract

        cpfmaster = New DataTable
        objTradt.Script(cpfmaster)


        eqmaster = New DataTable
        'eqmaster = objTrad.select_security
        objTradt.select_security(eqmaster)


        Currencymaster = New DataTable
        objTradt.SELECTED_Currency_Contract(Currencymaster)

        ' Currencymaster = objTrad.SELECTED_Currency_Contract

        HT_FOLotContrct.Clear()
        Dim HT_CurrNextFar1 As New Hashtable
        HT_CurrNextFar.Clear()
        futtoken = New ArrayList




        Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "option_type='XX'", "Symbol,Expdate Desc", DataViewRowState.CurrentRows).ToTable(True, "expdate", "token", "Symbol")
        Dim i As Integer = 1
        Dim str As String = ""
        For Each drow As DataRow In DtFMonthDate1.Select("")
            If HT_CurrNextFar.Contains(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy")) = False Then
                If i = 1 Then
                    str = drow("Symbol")
                Else
                    If drow("Symbol") <> str Then
                        i = 1
                        str = drow("Symbol")
                    End If
                End If

                HT_CurrNextFar.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy"), i)
                HT_CurrNextFar1.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy"), drow("Token"))
                i = i + 1
                str = drow("Symbol")
            End If

        Next



        Dim drcpfmaster() As DataRow = cpfmaster.Select("")
        For Each drow As DataRow In drcpfmaster
            If Not HT_FOContrct.Contains(drow("Script").ToString().ToUpper()) Then
                objSt = New Struct_FOContract
                objSt.lotsize = drow("lotsize")
                objSt.Token = drow("token")
                objSt.script = drow("script")
                objSt.asset_tokan = drow("asset_tokan")
                objSt.InstrumentType = drow("InstrumentType")
                HT_FOContrct.Add(drow("Script").ToString().ToUpper(), objSt)



            End If
            If HT_FOLotContrct.Contains(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("ddMMMyyyy")) = False Then
                HT_FOLotContrct.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("ddMMMyyyy"), drow("lotsize").ToString())
            End If
            If drow("Ftoken").ToString() = "" Then
                drow("ftoken") = Convert.ToDouble(HT_CurrNextFar1(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("MMMyyyy")))
            End If
            If drow("option_type") = "XX" Then
                futtoken.Add(CLng(drow("token")))
            End If
            If Not HT_AssetToken.Contains(CLng(drow("Token"))) Then
                HT_AssetToken.Add(CLng(drow("Token")), CLng(drow("Asset_Tokan")))
            End If

        Next


        'Dim drcpfmaster1() As DataRow
        'drcpfmaster1 = cpfmaster.Select("(option_type ='XX' or option_type='' or option_type=null)")

        ''For Each drow As DataRow In cpfmaster.Select("(option_type ='XX' or option_type='' or option_type=null)")
        'For Each drow As DataRow In drcpfmaster1
        '    futtoken.Add(CLng(drow("token")))
        'Next

        Dim drCurrencymaster() As DataRow
        drCurrencymaster = Currencymaster.Select("(option_type ='XX' or option_type='' or option_type=null)")
        'For Each drow As DataRow In Currencymaster.Select("(option_type ='XX' or option_type='' OR option_type=null)")
        For Each drow As DataRow In drCurrencymaster
            Currfuttoken.Add(CLng(drow("token")))
        Next

        eqtoken = New ArrayList
        Dim dreqmaster() As DataRow = eqmaster.Select("")
        For Each drow As DataRow In dreqmaster
            eqtoken.Add(CLng(drow("token")))
            If Not HT_EQContrct.Contains(drow("Script").ToString().ToUpper()) Then
                objEQ = New Struct_EQContract
                objEQ.Token = drow("token")
                objEQ.script = drow("script")
                HT_EQContrct.Add(drow("Script").ToString().ToUpper(), objEQ)
            End If
        Next
        Dim drCurrmaster() As DataRow = Currencymaster.Select("")
        For Each drow As DataRow In drCurrmaster
            'HT_CurrLotContrct.Add(drow("token"), drow("lotsize"))
            If HT_CurrLotContrct.Contains(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("ddMMMyyyy")) = False Then
                HT_CurrLotContrct.Add(drow("Symbol").ToString() & CDate(drow("Expdate")).ToString("ddMMMyyyy"), drow("multiplier").ToString())
            End If
        Next




        objTradt = Nothing

        filltokenflg = False
        'Dim dv As New DataView
        'dv = New DataView(cpfmaster1, "symbol='BANKNIFTY'", "", DataViewRowState.CurrentRows)
        Write_TimeLog1("Mainodule-> End Fun-Fill_token" + "|" + TimeSpan.FromTicks(DateTime.Now.Ticks - ttik).TotalSeconds.ToString())
    End Sub
    ''' <summary>
    ''' ExporttoHtml Data Export To Excel File in Html File Format
    ''' By Viral
    ''' </summary>
    ''' <param name="grid"> 
    ''' Object Of DataGridView it is Datasource
    ''' </param>
    ''' <param name="fname">
    ''' FileName With Full Path
    ''' </param>
    ''' <remarks></remarks>
    Public Sub ExporttoHtml(ByVal grid As DataGridView, ByVal fname As String)
        'create the DataGrid and perform the databinding
        'MsgBox(Now.ToString)
        'Dim grd As New System.Web.UI.WebControls.DataGrid()
        'grd.HeaderStyle.Font.Bold = True
        'grd.DataSource = grid.DataSource
        ''grd.DataMember = Data.Stats.TableName
        'grd.DataMember = "DT"

        'grd.DataBind()
        '' render the DataGrid control to a file
        'Dim sw As StreamWriter
        'sw = New StreamWriter(fname)
        'Dim hw As HtmlTextWriter
        'hw = New HtmlTextWriter(sw)
        'grd.RenderControl(hw)

        ''MsgBox(Now.ToString)
        'MsgBox("Exported Successfully.", MsgBoxStyle.Information)
        'sw.Close()
        'hw.Close()
        'sw = Nothing
        'hw = Nothing
    End Sub

    Public Sub SaveBCToExl(ByVal Dt As DataTable, ByVal fname As String, ByVal sname() As String, ByVal isEq As String, Optional ByVal ArrColList As ArrayList = Nothing)

        Try
            If Dt.Rows.Count <= 0 Then
                Exit Sub
            End If

            ' Create connection string variable. Modify the "Data Source"
            ' parameter as appropriate for your environment.
            Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;" &
             "Data Source=" & fname & ";" &
             "Extended Properties=Excel 8.0;"
            'MsgBox(Now.ToString)
            ' Create connection object by using the preceding connection string.
            Dim objConn As New OleDbConnection(sConnectionString)
            ' Open connection with the database.
            objConn.Open()
            ' The code to follow uses a SQL SELECT command to display the data from the worksheet.


            DropExTbl(objConn, "Bhavcopy")

            ' Create new OleDbCommand to return data from worksheet.
            Dim objCmdSelect As New OleDbCommand("Create Table Bhavcopy (uid int,script Varchar(50),INSTRUMENT Varchar(50),symbol Varchar(50),exp_date Varchar(50),strike int,option_type Varchar(50),ltp int,contract int,val_inlakh int,vol int,entry_date Varchar(50),futval int,mt int)", objConn)
            objCmdSelect.ExecuteNonQuery()
            ' Create new OleDbDataAdapter that is used to build a DataSet
            ' based on the preceding SQL SELECT statement.
            'Dim objAdapter1 = New OleDbDataAdapter()

            ' Pass the Select command to the adapter.
            'objAdapter1.SelectCommand = objCmdSelect

            ' Create new DataSet to hold information from the worksheet.
            'Dim objDataset1 = New DataSet()

            '// Fill the DataSet with the information from the worksheet.
            'objAdapter1.Fill(objDataset1, "XLData")

            ' Bind data to DataGrid control.
            'DataGridView1.DataSource = objDataset1.Tables(0).DefaultView()
            'DataGridView1.DataBind()


            For Each Item As DataRow In Dt.Rows
                Dim objCmdIns As New OleDbCommand("insert into Bhavcopy (uid, script, INSTRUMENT,symbol, exp_date, strike,option_type, ltp, contract, val_inlakh, vol, entry_date, futval, mt) " &
                                                    " Values(" & Item("uid") & ",'" & Item("script") & "', '" & Item("INSTRUMENT") & "','" & Item("symbol") & "','" & Item("exp_date") & "'," & Item("strike") & ",'" & Item("option_type") & "'," & Item("ltp") & "," & Item("contract") & "," & Item("val_inlakh") & "," & Item("vol") & ",'" & Item("entry_date") & "'," & Item("futval") & "," & Item("mt") & ")", objConn)
                objCmdIns.ExecuteNonQuery()
            Next

            '// Clean up objects.
            objConn.Close()
            'MsgBox(Now.ToString)
            MsgBox("Exported Successfully.")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub DropExTbl(ByVal objConn As OleDbConnection, ByVal sTbl As String)
        On Error Resume Next
        Dim objCmdSelect As New OleDbCommand("Drop Table " & sTbl & " ", objConn)
        objCmdSelect.ExecuteNonQuery()
    End Sub
    Public Sub exporttoexcel(ByVal grid() As DataGridView, ByVal fname As String, ByVal sname() As String, ByVal isEq As String, Optional ByVal ArrColList As ArrayList = Nothing)
        '   MsgBox(Now.ToString)
        Try
            Dim xlApp As Excel.Application
            Dim xlWorkBook As Excel.Workbook

            Dim misValue As Object = System.Reflection.Missing.Value
            'xlApp = New Excel.ApplicationClass()  //Typically, in .NET 4 you just need to remove the 'Class' suffix and compile the code
            xlApp = New Excel.Application()
            xlWorkBook = xlApp.Workbooks.Add(misValue)
            Dim xlWorkSheet As Excel.Worksheet
            If xlWorkBook.Worksheets.Count < grid.Length Then
                Dim temp As Integer
                temp = grid.Length - xlWorkBook.Worksheets.Count
                For i As Integer = 0 To temp
                    Dim xlWorkSheet1 As Excel.Worksheet
                    xlWorkSheet1 = xlWorkBook.Worksheets.Add ' xlWorkBook.Worksheets.Add(xlWorkSheet)
                Next
            End If

            For l As Integer = 0 To grid.Length - 1
                xlWorkSheet = CType(xlWorkBook.Worksheets.Item(l + 1), Excel.Worksheet)
                xlWorkSheet.Name = sname(l)

                Dim col As Integer = 0
                Dim row As Integer = 0
                Dim columnIndex As Integer = 0
                If (isEq = "FO") Then
                    If ArrColList Is Nothing Then
                        For col = 1 To grid(l).Columns.Count - 2
                            xlWorkSheet.Cells(1, col) = grid(l).Columns(col).HeaderText
                        Next
                        For row = 0 To grid(l).Rows.Count - 1
                            For col = 1 To grid(l).Columns.Count - 2
                                If grid(l).Columns(col).HeaderText = "Dealer" Then
                                    xlWorkSheet.Cells(row + 2, col) = "'" & grid(l).Rows(row).Cells(col).Value
                                Else
                                    xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(col).Value
                                End If
                                '   xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(col).Value
                            Next
                        Next
                    Else
                        For col = 1 To ArrColList.Count
                            xlWorkSheet.Cells(1, col) = grid(l).Columns(ArrColList(col - 1).ToString).HeaderText
                        Next
                        For row = 0 To grid(l).Rows.Count - 1
                            For col = 1 To ArrColList.Count
                                xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(ArrColList(col - 1)).Value
                            Next
                        Next
                    End If
                ElseIf (isEq = "EQ") Then
                    If ArrColList Is Nothing Then
                        For col = 2 To grid(l).Columns.Count - 1
                            xlWorkSheet.Cells(1, col - 1) = grid(l).Columns(col).HeaderText
                        Next
                        For row = 0 To grid(l).Rows.Count - 1
                            For col = 2 To grid(l).Columns.Count - 1
                                If grid(l).Columns(col).HeaderText = "Dealer" Then
                                    xlWorkSheet.Cells(row + 2, col - 1) = "'" & grid(l).Rows(row).Cells(col).Value
                                Else
                                    xlWorkSheet.Cells(row + 2, col - 1) = grid(l).Rows(row).Cells(col).Value
                                End If

                            Next
                        Next
                    Else
                        For col = 1 To ArrColList.Count
                            xlWorkSheet.Cells(1, col) = grid(l).Columns(ArrColList(col - 1).ToString).HeaderText
                        Next
                        For row = 0 To grid(l).Rows.Count - 1
                            For col = 1 To ArrColList.Count
                                xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(ArrColList(col - 1)).Value
                            Next
                        Next
                    End If

                ElseIf (isEq = "scenario") Then
                    If ArrColList Is Nothing Then
                        For col = 0 To grid(l).Columns.Count - 1
                            xlWorkSheet.Cells(1, col + 1) = grid(l).Columns(col).HeaderText
                        Next
                        For row = 0 To grid(l).Rows.Count - 2
                            For col = 1 To grid(l).Columns.Count
                                xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(col - 1).Value
                            Next
                        Next
                    Else
                        For col = 1 To ArrColList.Count
                            xlWorkSheet.Cells(1, col) = grid(l).Columns(ArrColList(col - 1).ToString).HeaderText
                        Next
                        For row = 0 To grid(l).Rows.Count - 1
                            For col = 1 To ArrColList.Count
                                xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(ArrColList(col - 1)).Value
                            Next
                        Next
                    End If
                Else
                    If ArrColList Is Nothing Then
                        For col = 0 To grid(l).Columns.Count - 1
                            xlWorkSheet.Cells(1, col + 1) = grid(l).Columns(col).HeaderText
                        Next
                        For row = 0 To grid(l).Rows.Count - 1
                            For col = 1 To grid(l).Columns.Count - 1
                                Try


                                    If grid(l).Columns(col).HeaderText = "Dealer" Then
                                        xlWorkSheet.Cells(row + 2, col) = "'" & grid(l).Rows(row).Cells(col - 1).Value
                                    Else

                                        xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(col - 1).Value
                                    End If
                                Catch ex As Exception

                                End Try
                                'xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(col - 1).Value
                            Next
                        Next
                    Else

                        For col = 1 To ArrColList.Count
                            xlWorkSheet.Cells(1, col) = grid(l).Columns(ArrColList(col - 1).ToString).HeaderText
                        Next
                        For row = 0 To grid(l).Rows.Count - 1
                            For col = 1 To ArrColList.Count
                                xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(ArrColList(col - 1)).Value
                            Next
                        Next
                    End If
                End If

                releaseObject(xlWorkSheet)
            Next
            'xlWorkBook.Save()

            xlWorkBook.SaveAs(fname, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            xlWorkBook.Close(True, misValue, misValue)
            xlApp.Quit()
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            'Dim proc As System.Diagnostics.Process
            'For Each proc In System.Diagnostics.Process.GetProcessesByName("EXCEL")
            '    proc.Kill()
            'Next
        End Try
        'MsgBox("Export Successfully")
        'Try
        '    If OPEN_EXCEL = 1 Then


        '        Dim strresult As Integer = MsgBox("You Want to Open File?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question)
        '        If strresult = DialogResult.Yes Then
        '            '  Process.Start("EXCEL.EXE", mPath)
        '            Dim VarExplorerArg As String = "/open, " & fname
        '            Shell("Explorer.exe " + VarExplorerArg, AppWinStyle.NormalFocus, True, -1)

        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
        'MsgBox(Now.ToString)
        '    MsgBox("Exported Successfully.")
    End Sub
    'Public Sub exporttoexcel(ByVal grid() As DataGridView, ByVal fname As String, ByVal sname() As String, ByVal isEq As String, Optional ByVal ArrColList As ArrayList = Nothing)
    '    '   MsgBox(Now.ToString)
    '    Try
    '        Dim xlApp As Excel.Application
    '        Dim xlWorkBook As Excel.Workbook

    '        Dim misValue As Object = System.Reflection.Missing.Value
    '        xlApp = New Excel.ApplicationClass()
    '        xlWorkBook = xlApp.Workbooks.Add(misValue)
    '        Dim xlWorkSheet As Excel.Worksheet
    '        If xlWorkBook.Worksheets.Count < grid.Length Then
    '            Dim temp As Integer
    '            temp = grid.Length - xlWorkBook.Worksheets.Count
    '            For i As Integer = 0 To temp
    '                Dim xlWorkSheet1 As Excel.Worksheet
    '                xlWorkSheet1 = xlWorkBook.Worksheets.Add ' xlWorkBook.Worksheets.Add(xlWorkSheet)
    '            Next
    '        End If

    '        For l As Integer = 0 To grid.Length - 1
    '            xlWorkSheet = CType(xlWorkBook.Worksheets.Item(l + 1), Excel.Worksheet)
    '            xlWorkSheet.Name = sname(l)

    '            Dim col As Integer = 0
    '            Dim row As Integer = 0
    '            Dim columnIndex As Integer = 0
    '            If (isEq = "FO") Then
    '                If ArrColList Is Nothing Then
    '                    For col = 1 To grid(l).Columns.Count - 2
    '                        xlWorkSheet.Cells(1, col) = grid(l).Columns(col).HeaderText
    '                    Next
    '                    For row = 0 To grid(l).Rows.Count - 1
    '                        For col = 1 To grid(l).Columns.Count - 2
    '                            xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(col).Value
    '                        Next
    '                    Next
    '                Else
    '                    For col = 1 To ArrColList.Count
    '                        xlWorkSheet.Cells(1, col) = grid(l).Columns(ArrColList(col - 1).ToString).HeaderText
    '                    Next
    '                    For row = 0 To grid(l).Rows.Count - 1
    '                        For col = 1 To ArrColList.Count
    '                            xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(ArrColList(col - 1)).Value
    '                        Next
    '                    Next
    '                End If
    '            ElseIf (isEq = "EQ") Then
    '                If ArrColList Is Nothing Then
    '                    For col = 2 To grid(l).Columns.Count - 2
    '                        xlWorkSheet.Cells(1, col - 1) = grid(l).Columns(col).HeaderText
    '                    Next
    '                    For row = 0 To grid(l).Rows.Count - 1
    '                        For col = 2 To grid(l).Columns.Count - 1
    '                            xlWorkSheet.Cells(row + 2, col - 1) = grid(l).Rows(row).Cells(col).Value
    '                        Next
    '                    Next
    '                Else
    '                    For col = 1 To ArrColList.Count
    '                        xlWorkSheet.Cells(1, col) = grid(l).Columns(ArrColList(col - 1).ToString).HeaderText
    '                    Next
    '                    For row = 0 To grid(l).Rows.Count - 1
    '                        For col = 1 To ArrColList.Count
    '                            xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(ArrColList(col - 1)).Value
    '                        Next
    '                    Next
    '                End If

    '            ElseIf (isEq = "scenario") Then
    '                If ArrColList Is Nothing Then
    '                    For col = 0 To grid(l).Columns.Count - 1
    '                        xlWorkSheet.Cells(1, col + 1) = grid(l).Columns(col).HeaderText
    '                    Next
    '                    For row = 0 To grid(l).Rows.Count - 2
    '                        For col = 1 To grid(l).Columns.Count
    '                            xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(col - 1).Value
    '                        Next
    '                    Next
    '                Else
    '                    For col = 1 To ArrColList.Count
    '                        xlWorkSheet.Cells(1, col) = grid(l).Columns(ArrColList(col - 1).ToString).HeaderText
    '                    Next
    '                    For row = 0 To grid(l).Rows.Count - 1
    '                        For col = 1 To ArrColList.Count
    '                            xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(ArrColList(col - 1)).Value
    '                        Next
    '                    Next
    '                End If
    '            Else
    '                If ArrColList Is Nothing Then
    '                    For col = 0 To grid(l).Columns.Count - 1
    '                        xlWorkSheet.Cells(1, col + 1) = grid(l).Columns(col).HeaderText
    '                    Next
    '                    For row = 0 To grid(l).Rows.Count - 1
    '                        For col = 1 To grid(l).Columns.Count
    '                            xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(col - 1).Value
    '                        Next
    '                    Next
    '                Else

    '                    For col = 1 To ArrColList.Count
    '                        xlWorkSheet.Cells(1, col) = grid(l).Columns(ArrColList(col - 1).ToString).HeaderText
    '                    Next
    '                    For row = 0 To grid(l).Rows.Count - 1
    '                        For col = 1 To ArrColList.Count
    '                            xlWorkSheet.Cells(row + 2, col) = grid(l).Rows(row).Cells(ArrColList(col - 1)).Value
    '                        Next
    '                    Next
    '                End If
    '            End If

    '            releaseObject(xlWorkSheet)
    '        Next
    '        'xlWorkBook.Save()

    '        xlWorkBook.SaveAs(fname, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
    '        xlWorkBook.Close(True, misValue, misValue)
    '        xlApp.Quit()
    '        releaseObject(xlWorkBook)
    '        releaseObject(xlApp)
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    Finally
    '        'Dim proc As System.Diagnostics.Process
    '        'For Each proc In System.Diagnostics.Process.GetProcessesByName("EXCEL")
    '        '    proc.Kill()
    '        'Next
    '    End Try
    '    'MsgBox(Now.ToString)
    '    MsgBox("Exported Successfully.")
    'End Sub
    Public Sub exporttocsv(ByVal dtgrd As DataTable, ByVal fname As String, ByVal isEq As String, Optional ByVal ArrColList As ArrayList = Nothing)
        Dim strValue As String = String.Empty
        Dim strHAD As String = String.Empty
        '  Dim flag As Integer = 1
        Dim strValue1 As String = String.Empty
        Dim sv1 As String = String.Empty
        Dim strHAD1 As String = String.Empty
        Dim str As String
        strValue = ""
        Dim Dt As New DataTable
        Dim j As Integer = 0
        Dim name(dtgrd.Columns.Count) As String


        Dim flg As Integer = 1
        For Each column As DataColumn In dtgrd.Columns

            If (flg = 1) Then
                flg = flg + 1
                name(j) = column.ColumnName
                str = name(j)
                strHAD = str
                j += 1

            ElseIf (flg = 2) Then

                name(j) = column.ColumnName
                str = name(j)
                sv1 = strHAD & "," & str
                strHAD = sv1
                j += 1
            End If


        Next

        Dim flag As Integer = 1
        Dim g As Integer = 1
        For Each dr1 As DataRow In dtgrd.Rows
            If (flag = 1) Then
                flag = flag + 1
                strValue = dr1(0).ToString()
            End If


            For i As Integer = 1 To (dtgrd.Columns.Count - 1)
                If (i >= 1) Then
                    ' If IsDate(dr1(i).ToString()) Then
                    If dtgrd.Columns.Item(i).ColumnName = "entrydate" Or dtgrd.Columns.Item(i).ColumnName = "preDate" Or dtgrd.Columns.Item(i).ColumnName = "mdate" Or dtgrd.Columns.Item(i).ColumnName = "fut_mdate" Then
                        sv1 = strValue & "," & CDate(dr1(i).ToString()).ToString("dd-MMM-yyyy")
                    Else
                        sv1 = strValue & "," & dr1(i).ToString()
                    End If

                    ' sv1 = strValue & "," & IIf((dr1(i).GetType.FullName = "System.String"), "'" & dr1(i).ToString, dr1(i).ToString)
                    strValue = sv1
                End If

            Next
            If (g = 1) Then
                strValue1 = strValue
                g = g + 1
            Else
                strValue1 = strValue1 + Environment.NewLine + strValue
            End If
            flag = 1
        Next
        If File.Exists(fname) = True Then
            File.Delete(fname)
        End If
        If File.Exists(fname) = False AndAlso Not String.IsNullOrEmpty(strValue) Then
            File.WriteAllText(fname, strHAD & vbNewLine & strValue1)
        End If
        ' MsgBox("Export Successfully")

    End Sub
    Public Sub OPEN_Export_File(ByVal fname As String)
        Try
            If OPEN_EXCEL = 1 Then


                Dim strresult As Integer = MsgBox("You Want to Open File?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question)
                If strresult = DialogResult.Yes Then
                    '  Process.Start("EXCEL.EXE", mPath)
                    Dim VarExplorerArg As String = "/open, " & fname
                    Shell("Explorer.exe " + VarExplorerArg, AppWinStyle.NormalFocus, True, -1)

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub exporttocsv(ByVal grid As DataGridView, ByVal fname As String, ByVal isEq As String, Optional ByVal ArrColList As ArrayList = Nothing)
        Dim strValue As String = String.Empty
        Dim strHAD As String = String.Empty
        Dim flag As Integer = 1

        For i As Integer = 0 To grid.Rows.Count - 1

            For j As Integer = 0 To grid.Rows(i).Cells.Count - 1
                If Not String.IsNullOrEmpty(grid.Rows(i).Cells(j).Value.ToString()) Then
                    grid.Rows(i).Cells(j).ToString()

                    If j > 1 And j <> 10 Then
                        strHAD = strHAD & "," & grid.Columns(j).HeaderText


                    ElseIf j <> 0 And j <> 10 Then
                        If String.IsNullOrEmpty(strValue) Then
                            strHAD = grid.Columns(j).HeaderText


                        ElseIf j <> 10 Then



                            strHAD = Environment.NewLine + strHAD + Environment.NewLine & grid.Columns(j).HeaderText.ToString()


                        End If
                    End If
                End If

            Next
            Exit For
        Next



        For i As Integer = 0 To grid.Rows.Count - 1

            For j As Integer = 0 To grid.Rows(i).Cells.Count - 1
                If Not String.IsNullOrEmpty(grid.Rows(i).Cells(j).Value.ToString()) Then
                    grid.Rows(i).Cells(j).ToString()

                    If j > 1 And j <> 10 Then

                        strValue = strValue & "," & grid.Rows(i).Cells(j).Value.ToString()

                    ElseIf j <> 0 And j <> 10 Then
                        If String.IsNullOrEmpty(strValue) Then

                            strValue = grid.Rows(i).Cells(j).Value.ToString()

                        ElseIf j <> 10 Then

                            strValue = strValue + Environment.NewLine & grid.Rows(i).Cells(j).Value.ToString()

                        End If
                    End If
                End If

            Next

        Next

        'If File.Exists(fname) = False AndAlso Not String.IsNullOrEmpty(strHAD) Then
        '    File.WriteAllText(fname, strHAD)

        'End If
        If File.Exists(fname) = False AndAlso Not String.IsNullOrEmpty(strValue) Then
            File.WriteAllText(fname, strHAD & vbNewLine & strValue)
        End If
        Try
            If OPEN_EXCEL = 1 Then


                Dim strresult As Integer = MsgBox("You Want to Open File?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question)
                If strresult = DialogResult.Yes Then
                    '  Process.Start("EXCEL.EXE", mPath)
                    Dim VarExplorerArg As String = "/open, " & fname
                    Shell("Explorer.exe " + VarExplorerArg, AppWinStyle.NormalFocus, True, -1)

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub exporttocsvEQ(ByVal grid As DataGridView, ByVal fname As String, ByVal isEq As String, Optional ByVal ArrColList As ArrayList = Nothing)
        Dim strValue As String = String.Empty
        Dim strHAD As String = String.Empty
        Dim flag As Integer = 1

        For i As Integer = 0 To grid.Rows.Count - 1

            For j As Integer = 0 To grid.Rows(i).Cells.Count - 1
                If Not String.IsNullOrEmpty(grid.Rows(i).Cells(j).Value.ToString()) Then
                    grid.Rows(i).Cells(j).ToString()

                    If j > 2 Then

                        '      strHAD = grid.Columns(j).HeaderText


                        strHAD = strHAD & "," & grid.Columns(j).HeaderText


                    ElseIf j = 2 Then
                        If String.IsNullOrEmpty(strValue) Then
                            strHAD = grid.Columns(j).HeaderText


                        Else

                            strHAD = Environment.NewLine + strHAD + Environment.NewLine & grid.Columns(j).HeaderText.ToString()


                        End If
                    End If
                End If

            Next
            Exit For
        Next


        Dim flag1 As Integer = 1
        For i As Integer = 0 To grid.Rows.Count - 1

            For j As Integer = 0 To grid.Rows(i).Cells.Count - 1
                If Not String.IsNullOrEmpty(grid.Rows(i).Cells(j).Value.ToString()) Then
                    grid.Rows(i).Cells(j).ToString()

                    If j > 2 Then
                        strValue = strValue & "," & grid.Rows(i).Cells(j).Value.ToString()
                    ElseIf j = 2 Then
                        If String.IsNullOrEmpty(strValue) Then

                            strValue = grid.Rows(i).Cells(j).Value.ToString()

                        Else

                            strValue = strValue + Environment.NewLine & grid.Rows(i).Cells(j).Value.ToString()

                        End If


                    End If
                End If

            Next

        Next

        If File.Exists(fname) = False AndAlso Not String.IsNullOrEmpty(strValue) Then
            File.WriteAllText(fname, strHAD & vbNewLine & strValue)
        End If
    End Sub


    Public Sub exporttocsvcurr(ByVal grid As DataGridView, ByVal fname As String, ByVal isEq As String, Optional ByVal ArrColList As ArrayList = Nothing)
        Dim strValue As String = String.Empty
        Dim strHAD As String = String.Empty
        Dim flag As Integer = 1

        For i As Integer = 0 To grid.Rows.Count - 1

            For j As Integer = 0 To grid.Rows(i).Cells.Count - 1
                If Not String.IsNullOrEmpty(grid.Rows(i).Cells(j).Value.ToString()) Then
                    grid.Rows(i).Cells(j).ToString()

                    If j > 0 Then
                        strHAD = strHAD & "," & grid.Columns(j).HeaderText


                    Else
                        If String.IsNullOrEmpty(strValue) Then
                            strHAD = grid.Columns(j).HeaderText


                        Else


                            strHAD = Environment.NewLine + strHAD + Environment.NewLine & grid.Columns(j).HeaderText.ToString()


                        End If
                    End If
                End If

            Next
            Exit For
        Next



        For i As Integer = 0 To grid.Rows.Count - 1

            For j As Integer = 0 To grid.Rows(i).Cells.Count - 1
                If Not String.IsNullOrEmpty(grid.Rows(i).Cells(j).Value.ToString()) Then
                    grid.Rows(i).Cells(j).ToString()

                    If j > 0 Then

                        strValue = strValue & "," & grid.Rows(i).Cells(j).Value.ToString()

                    Else
                        If String.IsNullOrEmpty(strValue) Then

                            strValue = grid.Rows(i).Cells(j).Value.ToString()

                        Else

                            strValue = strValue + Environment.NewLine & grid.Rows(i).Cells(j).Value.ToString()

                        End If
                    End If
                End If

            Next

        Next

        'If File.Exists(fname) = False AndAlso Not String.IsNullOrEmpty(strHAD) Then
        '    File.WriteAllText(fname, strHAD)

        'End If
        If File.Exists(fname) = False AndAlso Not String.IsNullOrEmpty(strValue) Then
            File.WriteAllText(fname, strHAD & vbNewLine & strValue)
        End If
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing

        Catch ex As Exception
            obj = Nothing
            MessageBox.Show("Exception Occured while releasing object " + ex.ToString())
        Finally
            GC.Collect()
        End Try
    End Sub
    Public Function validate_contract_csv_file(ByVal strpath As String, ByVal filetype As String) As Boolean
        Dim flg As Boolean = False
        If FLGCSVCONTRACT = True Then
            If File.Exists(strpath) Then
                If Path.GetFileName(strpath).Contains("_") Then


                    Dim result As String = Path.GetFileName(strpath).Split("_")(0) + Path.GetFileName(strpath).Split("_")(1) + Path.GetFileName(strpath).Split("_")(2)
                    If filetype = "FO" Then
                        If result <> "NSEFOcontract" Then
                            flg = True
                        End If
                    ElseIf filetype = "EQ" Then
                        If result <> "NSECMsecurity" Then
                            flg = True
                        End If
                    ElseIf filetype = "CURR" Then
                        If result <> "NSECDcontract" Then
                            flg = True
                        End If
                    End If
                End If
            End If
            Return flg
        End If
    End Function
    Public Function GetSymbol1(ByVal sname As String) As String
        ' Dim result() As String
        'result = sname.Split("/")
        'Dim i As Integer = result.Length - 2

        Dim firstpart As String = sname.Substring(0, sname.LastIndexOf("/"))


        Return firstpart

    End Function
    Public Function GetSymbol(ByVal sname As String) As String
        Dim result() As String
        result = sname.Split("/")
        Return result(0)
    End Function

    'Public Function GetSymbol_MarketWatch(ByVal sname As String) As String
    '    Dim result() As String
    '    result = sname.Split("/")
    '    Return result(0)
    'End Function

    Public Function GetTabName(ByVal sname As String) As String
        Dim result() As String
        result = sname.Split("/")
        Return result(UBound(result))
    End Function
    Public Function AuthTcp(ByVal strTCPServerName As String, ByVal strDb As String, ByVal strUser As String, ByVal strPass As String, ByVal UCode As String, ByVal edate As Date) As Boolean
        Try
            Dim StrConn As String = ""

            StrConn = " Data Source=" & strTCPServerName & ";Initial Catalog=" & strDb & ";User ID=" & strUser & ";Password=" & strPass & ";Timeout=15;Application Name=" & "VH_" & strTCPServerName & ""

            Dim ConTest As New System.Data.SqlClient.SqlConnection(StrConn)
            ConTest.Open()

            Dim cmd As New System.Data.SqlClient.SqlCommand("Sp_Auth '" & UCode & "','" & edate.ToString("dd/MMM/yyyy") & "'", ConTest)
            Dim i As Integer = cmd.ExecuteNonQuery()



            ConTest.Close()
            ConTest.Dispose()
            Return True
        Catch ex As Exception
            WriteLog("Proc::s Authantication Fail:-->" & vbCrLf & ex.Message)
            'ConTest.Dispose()
            Return False
        End Try
    End Function
    Public Function getIntrinsivval_synfut(ByVal tokenno As Long, ByVal cp As String, ByVal strikes As Double, ByVal company As String, ByVal mdate As String) As Double
        Try
            Dim synfut As Double
            If fltpprice.Contains(CLng(tokenno)) Then   'select LTP from fltpprice hashtable                                        
                Dim fltpprsynfut As Double = Val(fltpprice(CLng(tokenno)))
                If hashsyn.Contains(mdate & "_" & GetSymbol(company)) = True Then
                    synfut = Val(hashsyn(mdate & "_" & GetSymbol(company)) & "")
                End If
                ' get_SFut(drow("company"), CDate(drow("mdate")), Convert.ToDouble(fltpprsynfut))
            Else
                synfut = 0
            End If

            Dim fltppr As Double = 0
            Dim Intrinsivval As Double = 0
            If fltpprice.Contains(tokenno) Then
                fltppr = Val(fltpprice(tokenno))
                Intrinsivval = Math.Max(0, IIf(cp = "C", (Val(synfut) - Val(strikes)), (Val(strikes) - Val(synfut))))
                Return Intrinsivval
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function getIntrinsivval(ByVal tokenno As Long, ByVal cp As String, ByVal strikes As Double) As Double
        Try


            Dim fltppr As Double = 0
            Dim Intrinsivval As Double = 0
            If fltpprice.Contains(tokenno) Then
                fltppr = Val(fltpprice(tokenno))
                Intrinsivval = Math.Max(0, IIf(cp = "C", (Val(fltppr) - Val(strikes)), (Val(strikes) - Val(fltppr))))
                Return Intrinsivval
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function DownloadFileFTP(ByVal FileNameToDownload As String, ByVal URL As String, ByVal dirpath As String) As String

        'Dim ftpURL As String = URL
        ''Host URL or address of the FTP serve
        'Dim userName As String = "strategybuilder" '"FI-strategybuilder"
        ''User Name of the FTP server
        'Dim password As String = "finideas#123"
        Dim tempDirPath As String = dirpath
        'Dim ResponseDescription As String = ""
        Dim PureFileName As String = New FileInfo(FileNameToDownload).Name
        'Dim DownloadedFilePath As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName
        'Dim downloadUrl As String = [String].Format("{0}/{1}", ftpURL, FileNameToDownload)
        'Dim req As FtpWebRequest = DirectCast(FtpWebRequest.Create(downloadUrl), FtpWebRequest)
        'req.Method = WebRequestMethods.Ftp.DownloadFile
        'req.Credentials = New NetworkCredential(userName, password)
        'req.UseBinary = True
        'req.Proxy = Nothing
        'Try
        '    Dim response As FtpWebResponse = DirectCast(req.GetResponse(), FtpWebResponse)
        '    Dim stream As Stream = response.GetResponseStream()
        '    Dim buffer As Byte() = New Byte(2047) {}
        '    Dim fs As New FileStream(DownloadedFilePath, FileMode.Create)
        '    Dim ReadCount As Integer = stream.Read(buffer, 0, buffer.Length)
        '    While ReadCount > 0
        '        fs.Write(buffer, 0, ReadCount)
        '        ReadCount = stream.Read(buffer, 0, buffer.Length)
        '    End While
        '    ResponseDescription = response.StatusDescription
        '    fs.Close()
        '    stream.Close()
        'Catch e As Exception
        '    Console.WriteLine(e.Message)
        'End Try
        'Return ResponseDescription
        Try


            Dim filename As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName

            URL = URL & FileNameToDownload

            Dim client As New WebClient()
            Dim uri As New Uri(URL)
            client.Headers.Add("Accept: text/html, application/xhtml+xml, */*")
            client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)")
            client.DownloadFileAsync(uri, filename)

            While client.IsBusy
                System.Threading.Thread.Sleep(1000)

            End While
            'Console.WriteLine(ex.Message);


        Catch ex As UriFormatException
        End Try
        Return ""
    End Function

    Public Sub ReadJLConnection()
        If IO.File.Exists(Application.StartupPath & "\SQL Server Connection.txt") = False Then
            JL_sSQLSERVER = "192.168.98.11,1400"
            JL_sDATABASE = "SQLDB01B"
            JL_sAUTHANTICATION = "SQL"
            JL_sUSERNAME = "finideas"
            JL_sPASSWORD = "147258"
            Exit Sub
        End If
        Dim FR As New IO.StreamReader(Application.StartupPath & "\SQL Server Connection.txt")
        Dim Str As String = ""

        Str = FR.ReadLine()
        JL_sSQLSERVER = Str.Substring("Server Name ::".Length)


        Str = FR.ReadLine()
        JL_sDATABASE = Str.Substring("Database Name ::".Length)


        JL_sAUTHANTICATION = "SQL"


        Str = FR.ReadLine()
        If Str.Substring("Authentication ::".Length) = "Windows" Then
            'rbtnWindows.Checked = True
        Else
            'rbtnSQLServer.Checked = True
            Str = FR.ReadLine()
            JL_sUSERNAME = Str.Substring("User Name ::".Length)

            Str = FR.ReadLine()
            JL_sPASSWORD = Str.Substring("Password ::".Length)

        End If

        FR.Close()

    End Sub
    Public Function Update_TradingVol_column() As Integer


        Dim qry As String
        Try

            qry = "ALTER TABLE Trading ADD TradingVol Number"
            data_access.ParamClear()
            data_access.Cmd_Text = qry
            data_access.cmd_type = CommandType.Text
            data_access.ExecuteNonQuery()
            data_access.cmd_type = CommandType.StoredProcedure



        Catch ex As Exception
            ' MsgBox("Error to Update Data")

        End Try
    End Function

    Public Sub writePatchFile()
        If IO.File.Exists(Application.StartupPath & "\LastPatch.txt") = True Then
            File.Delete(Application.StartupPath & "\LastPatch.txt")
        End If


        Dim FSTimerLogFile As System.IO.StreamWriter
        FSTimerLogFile = New StreamWriter(Application.StartupPath & "\LastPatch.txt", True)

        FSTimerLogFile.WriteLine("LastPatchUpdate ::" & Now.Date.ToString("dd-MMM-yyyy"))
        FSTimerLogFile.WriteLine("patchUpdate ::" & "NO")

        FSTimerLogFile.Close()



        Dim FR As New IO.StreamReader(Application.StartupPath & "\LastPatch.txt")
        Dim Str As String = ""

        Str = FR.ReadLine()
        LastPatchUpdate = Convert.ToDateTime(Str.Substring("LastPatchUpdate ::".Length))


        Str = FR.ReadLine()
        patchUpdate = Str.Substring("patchUpdate ::".Length)


        FR.Close()

    End Sub
    Public Sub ReadPatchFile()
        If IO.File.Exists(Application.StartupPath & "\LastPatch.txt") = False Then
            LastPatchUpdate = ""
            patchUpdate = ""
            Exit Sub
        End If
        Dim FR As New IO.StreamReader(Application.StartupPath & "\LastPatch.txt")
        Dim Str As String = ""

        Str = FR.ReadLine()
        LastPatchUpdate = Convert.ToDateTime(Str.Substring("LastPatchUpdate ::".Length))


        Str = FR.ReadLine()
        patchUpdate = Str.Substring("patchUpdate ::".Length)


        FR.Close()

    End Sub
    Function IsInternetConnected() As Boolean

        Return New Ping().Send("www.google.com").Status = IPStatus.Success

    End Function
    Public Sub getdate()
        Try
            Dim FileNameToDownload As String = "patch.zip"
            Dim req As WebRequest = HttpWebRequest.Create(Convert.ToString("https://support.finideas.com/volhedge/") & FileNameToDownload)
            'url = Convert.ToString("https://support.finideas.com/Volhedge/Patch.zip") & FileNameToDownload
            req.Method = "HEAD"
            Dim resp As WebResponse = req.GetResponse()
            Dim remoteFileLastModified As String = resp.Headers.Get("Last-Modified")
            Dim remoteFileLastModifiedDateTime As DateTime
            Dim StrMsg As String = "Volhedge Update Available..." & vbCrLf & " " & vbCrLf & " " & vbCrLf & "" & vbCrLf & " " & vbCrLf & "Do you want to Update With Latest Version?" & vbCrLf & " " & vbCrLf & "It Will take few minuts "
            If LastPatchUpdate = "" Then
                Dim strresult As Integer = MsgBox(StrMsg, MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "VolHedge Patch Update.")
                Try


                    If strresult = DialogResult.Yes Then

                        'MDI.AutoUpdate("ftp://strategybuilder.finideas.com/VolHedge", "ftp://strategybuilder.finideas.com/OnlineUpdate", "VolHedge")
                        MDI.AutoUpdate("https://support.finideas.com/VolHedge", "https://support.finideas.com/OnlineUpdate", "VolHedge")

                    Else
                        Dim strresult1 As Integer = MsgBox("Remind Me Later?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "VolHedge Patch Update.")
                        If strresult1 = DialogResult.Yes Then

                        Else
                            writePatchFile()
                        End If
                    End If
                Catch ex As Exception

                End Try
            Else
                If DateTime.TryParse(remoteFileLastModified, remoteFileLastModifiedDateTime) Then
                    If Convert.ToDateTime(remoteFileLastModifiedDateTime.ToString("dd/MMM/yyyy")) > Convert.ToDateTime(LastPatchUpdate) Then

                        Dim strresult As Integer = MsgBox(StrMsg, MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "VolHedge Patch Update.")

                        Try


                            If strresult = DialogResult.Yes Then

                                'MDI.AutoUpdate("ftp://strategybuilder.finideas.com/VolHedge", "ftp://strategybuilder.finideas.com/OnlineUpdate", "VolHedge")
                                MDI.AutoUpdate("https://support.finideas.com/VolHedge", "https://support.finideas.com/OnlineUpdate", "VolHedge")
                            Else
                                Dim strresult1 As Integer = MsgBox("Remind Me Later?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
                                If strresult1 = DialogResult.Yes Then

                                Else
                                    writePatchFile()
                                End If
                            End If
                        Catch ex As Exception

                        End Try

                    End If

                Else
                    'MsgBox("could not determine")
                End If
            End If


        Catch ex As Exception
            Write_TimeLog1("getdate error:" & ex.ToString)

        End Try

    End Sub

    Public Sub Rounddata()

        Try
            ReadPatchFile()
            Dim insertflg As Boolean = False
            GdtSettings = objTrad.Settings

            Dim s As Integer
            s = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Update_Contract_Security'").ToString)

            If s = 1 Then MsgBox("Please update Contract & Security master.", MsgBoxStyle.Information, "Alert")

            roundDelta = GdtSettings.Compute("max(SettingKey)", "SettingName='RoundDelta'").ToString
            Deltastr = roundstr(roundDelta)

            addanovmargin = GdtSettings.Compute("max(SettingKey)", "SettingName='addanovmargin'").ToString

            roundTheta = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundTheta'").ToString)
            Thetastr = roundstr(roundTheta)

            roundVega = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundVega'").ToString)
            Vegastr = roundstr(roundVega)

            roundGamma = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundGamma'").ToString)
            Gammastr = roundstr(roundGamma)

            roundVolga = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundVolga'").ToString)
            Volgastr = roundstr(roundVolga)

            roundVanna = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundVanna'").ToString)
            Vannastr = roundstr(roundVanna)

            roundTheta_Val = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundTheta_Val'").ToString)
            Thetastr_Val = roundstr(roundTheta_Val)

            roundDelta_Val = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundDelta_Val'").ToString)
            Deltastr_Val = roundstr(roundDelta_Val)

            roundVega_Val = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundVega_Val'").ToString)
            Vegastr_Val = roundstr(roundVega_Val)

            roundGamma_Val = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundGamma_Val'").ToString)
            Gammastr_Val = roundstr(roundGamma_Val)

            roundVolga_Val = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundVolga_Val'").ToString)
            Volgastr_Val = roundstr(roundVolga_Val)

            roundVanna_Val = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundVanna_Val'").ToString)
            Vannastr_Val = roundstr(roundVanna_Val)

            RoundGrossMTM = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundGrossMTM'").ToString)
            GrossMTMstr = roundstr(RoundGrossMTM)

            RoundExpense = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundExpense'").ToString)
            Expensestr = roundstr(RoundExpense)

            RoundNetMTM = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundNetMTM'").ToString)
            NetMTMstr = roundstr(RoundNetMTM)

            RoundSquareMTM = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundSquareMTM'").ToString)
            SquareMTMstr = roundstr(RoundSquareMTM)

            Roundinmarg = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Roundinmarg'").ToString)
            inmargstr = roundstr(Roundinmarg)

            Roundexmarg = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Roundexmarg'").ToString)
            exmargstr = roundstr(Roundexmarg)

            Roundrealmarg = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Roundrealmarg'").ToString)
            realmargstr = roundstr(Roundrealmarg)

            Roundunmarg = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Roundunmarg'").ToString)
            unmargstr = roundstr(Roundunmarg)

            Roundrealtot = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Roundrealtot'").ToString)
            realtotstr = roundstr(Roundrealtot)

            GVarMaturity_Far_month = GdtSettings.Compute("max(SettingKey)", "SettingName='Maturity_Far_month'").ToString

            REM Currency Global Variable Setting
            RoundCurrencyLTP = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundCurrencyLTP'").ToString)
            CurrencyLTPStr = roundstr(RoundCurrencyLTP)


            REM Neutralize Global Variable Setting
            RoundNeutralize = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='ROUNDNEUTRALIZE'").ToString)
            NeutralizeStr = roundstr(RoundNeutralize)

            RoundCurrencyNetPrice = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundCurrencyNetPrice'").ToString)
            CurrencyNetPriceStr = roundstr(RoundCurrencyNetPrice)

            RoundCurrencyExpense = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundCurrencyExpense'").ToString)
            CurrencyExpenseStr = roundstr(RoundCurrencyExpense)

            RoundCurrencyStrikes = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RoundCurrencyStrikes'").ToString)
            CurrencyStrikesStr = roundstr(RoundCurrencyStrikes)

            CurrencyRateOfInterest = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Currency Rate Of Interest'").ToString)

            ''''''''''''''''''''Devang start'''''''''''''''''''''''''' 
            RoundOpenInt = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundopenint'").ToString)
            RoundCalender = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundcalender'").ToString)
            RoundBF = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundbf'").ToString)
            RoundVol = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundvol'").ToString)
            RoundBF2 = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundbf2'").ToString)
            RoundPCP = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundpcp'").ToString)
            RoundRatio = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundratio'").ToString)
            RoundStraddle = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundstraddle'").ToString)
            RoundVolume = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundvolume'").ToString)
            Roundltp = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundltp'").ToString)
            RoundBullSpread = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundbullspread'").ToString)
            RoundBearSpread = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundbearspread'").ToString)

            RoundC2C = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundC2C'").ToString)
            RoundC2P = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundC2P'").ToString)
            RoundP2P = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='roundP2P'").ToString)

            ''''''''''''''''''''Devang end''''''''''''''''''''''''''

            REM End
            Roundequity = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Roundequity'").ToString)
            equitystr = roundstr(Roundequity)
            mode = GdtSettings.Compute("max(SettingKey)", "SettingName='MODE'").ToString

            REM Get Atm Setting  By viral 27-06-11
            ATMDELTA_MIN = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='ATMDELTA_MIN'").ToString)
            ATMDELTA_MAX = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='ATMDELTA_MAX'").ToString)
            SELECTION_TYPE = CType(Val(GdtSettings.Compute("max(SettingKey)", "SettingName='SELECTION_TYPE'").ToString), Setting_SELECTION_TYPE)
            VOLATITLIY = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='VOLATITLIY'").ToString)
            gVOLATITLIY = VOLATITLIY / 100
            REM End

            REM Get If Sys data Change Setting  By viral 4-07-11
            PREDATA_AUTO_OFF = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='PREDATA_AUTO_OFF'").ToString)
            REM End

            REM Get Delta Calc Setting (Base on Lot Or Unit) By viral 8-07-11
            If Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EQ_DELTA_BASE'").ToString) = 0 Then
                EQ_DELTA_BASE = Setting_Greeks_BASE.Unit
            Else
                EQ_DELTA_BASE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EQ_DELTA_BASE'").ToString)
            End If

            If Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_DELTA_BASE'").ToString) = 0 Then
                CURR_DELTA_BASE = Setting_Greeks_BASE.Unit
            Else
                CURR_DELTA_BASE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_DELTA_BASE'").ToString)
            End If

            'Gamma
            If Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EQ_GAMMA_BASE'").ToString) = 0 Then
                EQ_GAMMA_BASE = Setting_Greeks_BASE.Unit
            Else
                EQ_GAMMA_BASE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EQ_GAMMA_BASE'").ToString)
            End If

            If Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_GAMMA_BASE'").ToString) = 0 Then
                CURR_GAMMA_BASE = Setting_Greeks_BASE.Unit
            Else
                CURR_GAMMA_BASE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_GAMMA_BASE'").ToString)
            End If

            'Vega
            If Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EQ_VEGA_BASE'").ToString) = 0 Then
                EQ_VEGA_BASE = Setting_Greeks_BASE.Unit
            Else
                EQ_VEGA_BASE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EQ_VEGA_BASE'").ToString)
            End If

            If Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_VEGA_BASE'").ToString) = 0 Then
                CURR_VEGA_BASE = Setting_Greeks_BASE.Unit
            Else
                CURR_VEGA_BASE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_VEGA_BASE'").ToString)
            End If

            'Theta
            If Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EQ_THETA_BASE'").ToString) = 0 Then
                EQ_THETA_BASE = Setting_Greeks_BASE.Unit
            Else
                EQ_THETA_BASE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EQ_THETA_BASE'").ToString)
            End If

            If Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_THETA_BASE'").ToString) = 0 Then
                CURR_THETA_BASE = Setting_Greeks_BASE.Unit
            Else
                CURR_THETA_BASE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_THETA_BASE'").ToString)
            End If

            'Volga
            If Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EQ_VOLGA_BASE'").ToString) = 0 Then
                EQ_VOLGA_BASE = Setting_Greeks_BASE.Unit
            Else
                EQ_VOLGA_BASE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EQ_VOLGA_BASE'").ToString)
            End If

            If Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_VOLGA_BASE'").ToString) = 0 Then
                CURR_VOLGA_BASE = Setting_Greeks_BASE.Unit
            Else
                CURR_VOLGA_BASE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_VOLGA_BASE'").ToString)
            End If

            'Vanna
            If Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EQ_VANNA_BASE'").ToString) = 0 Then
                EQ_VANNA_BASE = Setting_Greeks_BASE.Unit
            Else
                EQ_VANNA_BASE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EQ_VANNA_BASE'").ToString)
            End If

            If Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_VANNA_BASE'").ToString) = 0 Then
                CURR_VANNA_BASE = Setting_Greeks_BASE.Unit
            Else
                CURR_VANNA_BASE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_VANNA_BASE'").ToString)
            End If



            REM End

            REM Get Contract & Curr & Security LWT(Last Write Time)  Setting  By viral 11-07-11
            CONTRACT_LWT = GdtSettings.Compute("max(SettingKey)", "SettingName='CONTRACT_LWT'").ToString.Trim
            SECURITY_LWT = GdtSettings.Compute("max(SettingKey)", "SettingName='SECURITY_LWT'").ToString.Trim
            CURR_CONTRACT_LWT = GdtSettings.Compute("max(SettingKey)", "SettingName='CURR_CONTRACT_LWT'").ToString.Trim

            fifo_avg = GdtSettings.Compute("max(SettingKey)", "SettingName='fifo_avg'").ToString
            ''==========================================
            ''importdatabase variable
            GVar_DealerCode = GdtSettings.Compute("max(SettingKey)", "SettingName='Nse_ccode'").ToString




            'Dim StrArrNeatClient() As String = GVar_DealerCode.Split(",")
            'GVar_DealerCode = ""
            'For Each Str As String In StrArrNeatClient
            '    If Str Is Nothing OrElse Str = "" Then Continue For
            '    If GVar_DealerCode <> "" Then
            '        GVar_DealerCode &= ","
            '    End If
            '    GVar_DealerCode &= "'" & Str & "'"
            'Next
            Dim GVar_DealerCodenew As String = ""
            If GVar_DealerCode.Contains(",") Then
                Dim StrArrNeatClient() As String = GVar_DealerCode.Split(",")
                For Each Str As String In StrArrNeatClient
                    If Str Is Nothing OrElse Str = "" Then Continue For
                    If GVar_DealerCodenew <> "" Then
                        GVar_DealerCodenew &= ","
                    End If
                    GVar_DealerCodenew &= "'" & Str & "'"
                Next
                GVar_DealerCode = GVar_DealerCodenew
            Else
                If GVar_DealerCode <> "" Then
                    GVar_DealerCode = "'" & GVar_DealerCode.ToString() & "'"
                Else
                    GVar_DealerCode = ""
                End If

            End If

            If GdtSettings.Select("SettingName='INTERNETDATA_LINK'").Length > 0 Then
                INTERNETDATA_LINK = "LINK1" 'GdtSettings.Compute("max(SettingKey)", "SettingName='INTERNETDATA_LINK'").ToString.Trim
            Else
                INTERNETDATA_LINK = "LINK1"
                objTrad.SettingName = "INTERNETDATA_LINK"
                objTrad.SettingKey = "LINK1"
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='UDP_SELECTED_TOKEN'").Length > 0 Then
                UDP_SELECTED_TOKEN = 0 'GdtSettings.Compute("max(SettingKey)", "SettingName='INTERNETDATA_LINK'").ToString.Trim
            Else
                UDP_SELECTED_TOKEN = 0
                objTrad.SettingName = "UDP_SELECTED_TOKEN"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If



            If GdtSettings.Select("SettingName='HIDEVOLGA'").Length > 0 Then
                HIDEVOLGA = GdtSettings.Compute("max(SettingKey)", "SettingName='HIDEVOLGA'").ToString.Trim
            Else
                HIDEVOLGA = 1
                objTrad.SettingName = "HIDEVOLGA"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If


            If GdtSettings.Select("SettingName='HIDEVANNA'").Length > 0 Then
                HIDEVanna = GdtSettings.Compute("max(SettingKey)", "SettingName='HIDEVANNA'").ToString.Trim
            Else
                HIDEVanna = 1
                objTrad.SettingName = "HIDEVANNA"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='COMPACTEDDATE'").Length > 0 Then
                Try
                    Dim strDt As String = GdtSettings.Compute("max(SettingKey)", "SettingName='COMPACTEDDATE'").ToString.Trim
                    COMPACTEDDATE = strDt
                Catch ex As Exception
                    objTrad.SettingName = "COMPACTEDDATE"
                    objTrad.SettingKey = Date.Now.ToString("dd-MMM-yyyy")
                    objTrad.Insert_setting()
                    COMPACTEDDATE = Date.Now.ToString("dd-MMM-yyyy")
                    insertflg = True
                End Try
            Else
                COMPACTEDDATE = Date.Now.ToString("dd-MMM-yyyy")
                objTrad.SettingName = "COMPACTEDDATE"
                objTrad.SettingKey = Date.Now.ToString("dd-MMM-yyyy")
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='DayTOEXP'").Length > 0 Then
                DayTOEXP = GdtSettings.Compute("max(SettingKey)", "SettingName='DayTOEXP'").ToString.Trim
            Else
                DayTOEXP = 0
                objTrad.SettingName = "DayTOEXP"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='NEUTRALIZE_MONTH'").Length > 0 Then
                GNEUTRALIZE_MONTH = GdtSettings.Compute("max(SettingKey)", "SettingName='NEUTRALIZE_MONTH'").ToString
            Else
                GNEUTRALIZE_MONTH = 0
                objTrad.SettingName = "NEUTRALIZE_MONTH"
                objTrad.SettingKey = "CURRENT MONTH"
                objTrad.Insert_setting()
                insertflg = True
            End If




            udpselsymbol.Clear()
            Dim dtsymbol As DataTable = objTrad.select_chklistbox_symbol()

            For Each item As DataRow In dtsymbol.Select()
                If HT_symboltoken.Contains(item("Symbol").ToString()) = True Then
                    If udpselsymbol.Contains(CLng(HT_symboltoken(item("Symbol")).ToString())) = False Then
                        udpselsymbol.Add(CLng(HT_symboltoken(item("Symbol")).ToString()), item("Symbol").ToString())
                    End If

                End If

            Next

            If GdtSettings.Select("SettingName='originalpath'").Length > 0 Then
                originalpath = GdtSettings.Compute("max(SettingKey)", "SettingName='originalpath'").ToString.Trim

            Else


                objTrad.SettingName = "originalpath"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='NETMODE'").Length > 0 Then
                NetMode = GdtSettings.Compute("max(SettingKey)", "SettingName='NETMODE'").ToString.Trim
                If NetMode = "NET" Then NetMode = "TCP"
            Else

                NetMode = "UDP"
                objTrad.SettingName = "NETMODE"
                objTrad.SettingKey = NetMode
                objTrad.Insert_setting()
                insertflg = True
            End If
            'If NetMode = "TCP" Then
            '    NetMode = "NET"
            'End If

            If GdtSettings.Select("SettingName='ALLCOMPANYLOCATION'").Length > 0 Then
                ALLCOMPANYLOCATION = GdtSettings.Compute("max(SettingKey)", "SettingName='ALLCOMPANYLOCATION'").ToString.Trim
            Else
                ALLCOMPANYLOCATION = ""
                objTrad.SettingName = "ALLCOMPANYLOCATION"
                objTrad.SettingKey = ALLCOMPANYLOCATION
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='ALLCOMPANYSIZE'").Length > 0 Then
                ALLCOMPANYSIZE = GdtSettings.Compute("max(SettingKey)", "SettingName='ALLCOMPANYSIZE'").ToString.Trim
            Else
                ALLCOMPANYSIZE = ""
                objTrad.SettingName = "ALLCOMPANYSIZE"
                objTrad.SettingKey = ALLCOMPANYSIZE
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='OPEN_EXCEL'").Length > 0 Then
                OPEN_EXCEL = GdtSettings.Compute("max(SettingKey)", "SettingName='OPEN_EXCEL'").ToString.Trim
            Else
                '  SCE_DAYS = 10
                objTrad.SettingName = "OPEN_EXCEL"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='CONTRACT_NOTIFICATION'").Length > 0 Then
                CONTRACT_NOTIFICATION = GdtSettings.Compute("max(SettingKey)", "SettingName='CONTRACT_NOTIFICATION'").ToString.Trim
            Else
                '  SCE_DAYS = 10
                CONTRACT_NOTIFICATION = 1
                objTrad.SettingName = "CONTRACT_NOTIFICATION"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If

            'OPEN_EXCEL
            If GdtSettings.Select("SettingName='SCE_DAYS'").Length > 0 Then
                SCE_DAYS = GdtSettings.Compute("max(SettingKey)", "SettingName='SCE_DAYS'").ToString.Trim
            Else
                SCE_DAYS = 10
                objTrad.SettingName = "SCE_DAYS"
                objTrad.SettingKey = 10
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='GRID_BACKCOLOR '").Length > 0 Then
                '  Dim MyColor As System.Drawing.Color

                GRID_BACKCOLOR = GdtSettings.Compute("max(SettingKey)", "SettingName='GRID_BACKCOLOR '").ToString()
                'FIXVOL_BACKCOLOR =Color.FromName("FIXVOL_BACKCOLOR") MyColor
            Else

                objTrad.SettingName = "GRID_BACKCOLOR "
                objTrad.SettingKey = ""
                objTrad.Insert_setting()
            End If



            If GdtSettings.Select("SettingName='CALMARGINWITH_AEL_EXPO'").Length > 0 Then
                CALMARGINWITH_AEL_EXPO = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CALMARGINWITH_AEL_EXPO'").ToString)
            Else
                CALMARGINWITH_AEL_EXPO = 1
                objTrad.SettingName = "CALMARGINWITH_AEL_EXPO"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If


            If GdtSettings.Select("SettingName='FONT_COLOR'").Length > 0 Then
                '  Dim MyColor As System.Drawing.Color

                FONT_COLOR = GdtSettings.Compute("max(SettingKey)", "SettingName='FONT_COLOR'").ToString()
                'FIXVOL_BACKCOLOR =Color.FromName("FIXVOL_BACKCOLOR") MyColor
            Else

                objTrad.SettingName = "FONT_COLOR"
                objTrad.SettingKey = ""
                objTrad.Insert_setting()
            End If

            If GdtSettings.Select("SettingName='GRID_FONTSIZE'").Length > 0 Then
                '  Dim MyColor As System.Drawing.Color

                GRID_FONTSIZE = GdtSettings.Compute("max(SettingKey)", "SettingName='GRID_FONTSIZE'")
                'FIXVOL_BACKCOLOR =Color.FromName("FIXVOL_BACKCOLOR") MyColor
            Else

                objTrad.SettingName = "GRID_FONTSIZE"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
            End If

            If GdtSettings.Select("SettingName='GRID_FONTSTYLE'").Length > 0 Then
                '  Dim MyColor As System.Drawing.Color

                GRID_FONTSTYLE = GdtSettings.Compute("max(SettingKey)", "SettingName='GRID_FONTSTYLE'").ToString()
                'FIXVOL_BACKCOLOR =Color.FromName("FIXVOL_BACKCOLOR") MyColor
            Else

                objTrad.SettingName = "GRID_FONTSTYLE"
                objTrad.SettingKey = ""
                objTrad.Insert_setting()
            End If

            If GdtSettings.Select("SettingName='GRID_FONTTYPE '").Length > 0 Then
                '  Dim MyColor As System.Drawing.Color

                GRID_FONTTYPE = GdtSettings.Compute("max(SettingKey)", "SettingName='GRID_FONTTYPE '").ToString()
                'FIXVOL_BACKCOLOR =Color.FromName("FIXVOL_BACKCOLOR") MyColor
            Else

                objTrad.SettingName = "GRID_FONTTYPE "
                objTrad.SettingKey = ""
                objTrad.Insert_setting()
            End If
            If GdtSettings.Select("SettingName='NETDATAMODE'").Length > 0 Then
                NetDataMode = GdtSettings.Compute("max(SettingKey)", "SettingName='NETDATAMODE'").ToString.Trim
            Else
                NetDataMode = "NSE"
                objTrad.SettingName = "NETDATAMODE"
                objTrad.SettingKey = NetDataMode
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='AnalysisSortedColumn'").Length > 0 Then
                AnalysisSortedColumn = GdtSettings.Compute("max(SettingKey)", "SettingName='AnalysisSortedColumn'").ToString.Trim
            Else
                AnalysisSortedColumn = ""
                objTrad.SettingName = "AnalysisSortedColumn"
                objTrad.SettingKey = ""
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='MarketwatchSaveProfile'").Length > 0 Then
                MarketwatchSaveProfile = GdtSettings.Compute("max(SettingKey)", "SettingName='MarketwatchSaveProfile'").ToString.Trim
            Else
                MarketwatchSaveProfile = "Default"
                objTrad.SettingName = "MarketwatchSaveProfile"
                objTrad.SettingKey = "Default"
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='RefreshsummaryExpirywise'").Length > 0 Then
                RefreshsummaryExpirywise = GdtSettings.Compute("max(SettingKey)", "SettingName='RefreshsummaryExpirywise'").ToString.Trim
            Else
                RefreshsummaryExpirywise = "False"
                objTrad.SettingName = "RefreshsummaryExpirywise"
                objTrad.SettingKey = "False"
                objTrad.Insert_setting()
                insertflg = True
            End If


            If GdtSettings.Select("SettingName='RefreshsummaryFixVol'").Length > 0 Then
                RefreshsummaryFixVol = GdtSettings.Compute("max(SettingKey)", "SettingName='RefreshsummaryFixVol'").ToString.Trim
            Else
                RefreshsummaryFixVol = "True"
                objTrad.SettingName = "RefreshsummaryFixVol"
                objTrad.SettingKey = "True"
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='CDELAY'").Length > 0 Then
                iCDelay = CInt(GdtSettings.Compute("max(SettingKey)", "SettingName='CDELAY'").ToString.Trim)
            Else
                iCDelay = "0"
                objTrad.SettingName = "CDELAY"
                objTrad.SettingKey = iCDelay
                objTrad.Insert_setting()
                insertflg = True
            End If


            If GdtSettings.Select("SettingName='CURRENCY_MARGIN_QTY'").Length > 0 Then
                CURRENCY_MARGIN_QTY = CInt(GdtSettings.Compute("max(SettingKey)", "SettingName='CURRENCY_MARGIN_QTY'").ToString.Trim)
            Else
                CURRENCY_MARGIN_QTY = "0"
                objTrad.SettingName = "CURRENCY_MARGIN_QTY"
                objTrad.SettingKey = iCDelay
                objTrad.Insert_setting()
                insertflg = True
            End If


            If GdtSettings.Select("SettingName='NDELAY'").Length > 0 Then
                iNDelay = CInt(GdtSettings.Compute("max(SettingKey)", "SettingName='NDELAY'").ToString.Trim)
            Else
                iNDelay = "0"
                objTrad.SettingName = "NDELAY"
                objTrad.SettingKey = iNDelay
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='FDELAY'").Length > 0 Then
                iFDelay = CInt(GdtSettings.Compute("max(SettingKey)", "SettingName='FDELAY'").ToString.Trim)
            Else
                iFDelay = "0"
                objTrad.SettingName = "FDELAY"
                objTrad.SettingKey = iFDelay
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='CDATE'").Length > 0 Then
                dCDate = GdtSettings.Compute("max(SettingKey)", "SettingName='CDATE'").ToString.Trim
            Else
                dCDate = "1/Jan/1980"
                objTrad.SettingName = "CDATE"
                objTrad.SettingKey = dCDate
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='NDATE'").Length > 0 Then
                dNDate = GdtSettings.Compute("max(SettingKey)", "SettingName='NDATE'").ToString.Trim
            Else
                dNDate = "1/Jan/1980"
                objTrad.SettingName = "NDATE"
                objTrad.SettingKey = dNDate
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='FDATE'").Length > 0 Then
                dFDate = GdtSettings.Compute("max(SettingKey)", "SettingName='FDATE'").ToString.Trim
            Else
                dFDate = "1/Jan/1980"
                objTrad.SettingName = "FDATE"
                objTrad.SettingKey = dFDate
                objTrad.Insert_setting()
                insertflg = True
            End If



            If GdtSettings.Select("SettingName='SQLSERVER'").Length > 0 Then
                If NetMode = "JL" Then

                    ReadJLConnection()
                    sSQLSERVER = JL_sSQLSERVER
                    sDATABASE = JL_sDATABASE
                    sAUTHANTICATION = JL_sAUTHANTICATION
                    sUSERNAME = JL_sUSERNAME
                    sPASSWORD = JL_sPASSWORD

                ElseIf NetMode = "API" And (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") Then
                    Dim drconn As DataRow()

                    If gstr_ProductName = "OMI" Then


                        sSQLSERVER = RegServerIP.Split(",")(0) + ",1400"  ' "103.228.79.44,1403"
                        sAUTHANTICATION = "SQL"
                        sDATABASE = ObjLoginData.DatabaseName
                        sUSERNAME = ObjLoginData.UsernameServer
                        sPASSWORD = ObjLoginData.Password


                    Else
                        Dim dtApi As DataTable
                        dtApi = frmsetting.SelectdataofAPIConnection()


                        drconn = dtApi.Select()
                        Dim servernm As String = ""
                        Dim db As String = ""
                        Dim unm As String = ""
                        Dim pwd As String = ""

                        If drconn.Length > 0 Then
                            For Each DR1 As DataRow In drconn
                                servernm = DR1("SERVER")  'DR1("SERVERNAME")
                                db = DR1("DBNAME")
                                unm = DR1("USERNAME")
                                pwd = DR1("PASSWORD")

                                If ChkSQLConnNew(servernm, db, unm, pwd) = True Then
                                    sSQLSERVER = servernm
                                    sDATABASE = db
                                    sUSERNAME = unm
                                    sPASSWORD = pwd
                                    Exit For
                                End If
                            Next
                        End If



                        'sSQLSERVER = RegServerIP ' "103.228.79.44,1403"
                        'sDATABASE = "SQLDB01B"
                        'sAUTHANTICATION = "SQL"
                        'sUSERNAME = "finideas"
                        'sPASSWORD = "123456"

                    End If

                    'Else
                    '    sSQLSERVER = GdtSettings.Compute("max(SettingKey)", "SettingName='SQLSERVER'").ToString.Trim
                    '    sDATABASE = GdtSettings.Compute("max(SettingKey)", "SettingName='DATABASE'").ToString.Trim
                    '    sAUTHANTICATION = GdtSettings.Compute("max(SettingKey)", "SettingName='AUTHANTICATION'").ToString.Trim
                    '    sUSERNAME = GdtSettings.Compute("max(SettingKey)", "SettingName='USERNAME'").ToString.Trim
                    '    sPASSWORD = GdtSettings.Compute("max(SettingKey)", "SettingName='PASSWORD'").ToString.Trim
                ElseIf NetMode = "TCP" Then  'And (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") 
                    'Dim drconn As DataRow()

                    'Dim dtApi As DataTable
                    'dtApi = frmsetting.SelectdataofAPIConnection()
                    'drconn = dtApi.Select()
                    'Dim servernm As String = ""
                    'Dim db As String = ""
                    'Dim unm As String = ""
                    'Dim pwd As String = ""
                    'If drconn.Length > 0 Then
                    '    For Each DR1 As DataRow In drconn
                    'servernm = DR1("SERVER") 'DR1("SERVERNAME")
                    'db = DR1("DBNAME")
                    'unm = DR1("USERNAME")
                    'pwd = DR1("PASSWORD")
                    'If ChkSQLConnNew(servernm, db, unm, pwd) = True Then



                    sSQLSERVER = gstr_Internet_Server
                    sDATABASE = gstr_Internet_DB
                    sUSERNAME = gstr_Internet_Uid
                    sPASSWORD = gstr_Internet_Pwd



                    'sSQLSERVER = gstr_Internet_Server
                    'sDATABASE = gstr_Internet_DB
                    'sUSERNAME = gstr_Internet_Uid
                    'sPASSWORD = gstr_Internet_Pwd




                    '    Exit For
                    'End If
                    '        Next
                    'End If



                    'sSQLSERVER = RegServerIP ' "103.228.79.44,1403"
                    'sDATABASE = "SQLDB01B"
                    'sAUTHANTICATION = "SQL"
                    'sUSERNAME = "finideas"
                    'sPASSWORD = "123456"


                End If

            Else
                sSQLSERVER = "127.0.0.1"
                objTrad.SettingName = "SQLSERVER"
                objTrad.SettingKey = sSQLSERVER
                objTrad.Insert_setting()

                sDATABASE = "SQLDB01B"
                objTrad.SettingName = "DATABASE"
                objTrad.SettingKey = sDATABASE
                objTrad.Insert_setting()

                sAUTHANTICATION = "SQL"
                objTrad.SettingName = "AUTHANTICATION"
                objTrad.SettingKey = sAUTHANTICATION
                objTrad.Insert_setting()

                sUSERNAME = "sa"
                objTrad.SettingName = "USERNAME"
                objTrad.SettingKey = sUSERNAME
                objTrad.Insert_setting()

                sPASSWORD = "123456"
                objTrad.SettingName = "PASSWORD"
                objTrad.SettingKey = sPASSWORD
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='INSTANCE'").Length > 0 Then
                INSTANCEname = GdtSettings.Compute("max(SettingKey)", "SettingName='INSTANCE'").ToString.Trim
            Else
                INSTANCEname = "PRIMARY"
                objTrad.SettingName = "INSTANCE"
                objTrad.SettingKey = INSTANCEname
                objTrad.Insert_setting()
            End If


            If GdtSettings.Select("SettingName='SYNTH_ATM_UPDOWNSTRIKE'").Length > 0 Then
                SYNTH_ATM_UPDOWNSTRIKE = GdtSettings.Compute("max(SettingKey)", "SettingName='SYNTH_ATM_UPDOWNSTRIKE'").ToString.Trim
            Else
                SYNTH_ATM_UPDOWNSTRIKE = 2
                objTrad.SettingName = "SYNTH_ATM_UPDOWNSTRIKE"
                objTrad.SettingKey = 2
                objTrad.Insert_setting()
            End If



            If GdtSettings.Select("SettingName='ANALYSIS FILE PATH'").Length > 0 Then
                Analysisfilepath = GdtSettings.Compute("max(SettingKey)", "SettingName='ANALYSIS FILE PATH'").ToString.Trim
            Else
                Analysisfilepath = "ANALYSIS FILE PATH"
                objTrad.SettingName = "ANALYSIS FILE NAME"
                objTrad.SettingKey = Analysisfilepath
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='ANALYSIS FILE NAME'").Length > 0 Then
                Analysisfilepath = GdtSettings.Compute("max(SettingKey)", "SettingName='ANALYSIS FILE NAME'").ToString.Trim
            Else
                Analysisfilepath = "ANALYSIS FILE NAME"
                objTrad.SettingName = "ANALYSIS FILE PATH"
                objTrad.SettingKey = Analysisfilepath
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='SpanTimerCheck'").Length > 0 Then
                SpanTimerCheck = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='SpanTimerCheck'").ToString)
            Else

                objTrad.SettingName = "SPANTIMERCHECK"
                objTrad.SettingKey = SpanTimerCheck
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='EXPORT_IMPORT_POSITION'").Length > 0 Then
                EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)
            Else

                objTrad.SettingName = "EXPORT_IMPORT_POSITION"
                objTrad.SettingKey = "5"
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='CAL_USING_EQ_WITHINDEX'").Length > 0 Then
                CAL_USING_EQ_WITHINDEX = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CAL_USING_EQ_WITHINDEX'").ToString)
            Else

                objTrad.SettingName = "CAL_USING_EQ_WITHINDEX"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='DAYTIME_VOLANDGREEK_CAL'").Length > 0 Then
                DAYTIME_VOLANDGREEK_CAL = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='DAYTIME_VOLANDGREEK_CAL'").ToString)
            Else
                DAYTIME_VOLANDGREEK_CAL = 0
                objTrad.SettingName = "DAYTIME_VOLANDGREEK_CAL"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If


            If GdtSettings.Select("SettingName='INCLUDEEXPIRY_VOLANDGREEK_CAL'").Length > 0 Then
                INCLUDEEXPIRY_VOLANDGREEK_CAL = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='INCLUDEEXPIRY_VOLANDGREEK_CAL'").ToString)
            Else
                INCLUDEEXPIRY_VOLANDGREEK_CAL = 0
                objTrad.SettingName = "INCLUDEEXPIRY_VOLANDGREEK_CAL"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='FIXVOL_BACKCOLOR'").Length > 0 Then
                '  Dim MyColor As System.Drawing.Color

                FIXVOL_BACKCOLOR = GdtSettings.Compute("max(SettingKey)", "SettingName='FIXVOL_BACKCOLOR'").ToString()
                'FIXVOL_BACKCOLOR =Color.FromName("FIXVOL_BACKCOLOR") MyColor
            Else

                objTrad.SettingName = "FIXVOL_BACKCOLOR"
                objTrad.SettingKey = ""
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='GAMMAMULTI_BACKCOLOR'").Length > 0 Then
                '  Dim MyColor As System.Drawing.Color

                GAMMAMULTI_BACKCOLOR = GdtSettings.Compute("max(SettingKey)", "SettingName='GAMMAMULTI_BACKCOLOR'").ToString()
                'FIXVOL_BACKCOLOR =Color.FromName("FIXVOL_BACKCOLOR") MyColor
            Else

                objTrad.SettingName = "GAMMAMULTI_BACKCOLOR"
                objTrad.SettingKey = ""
                objTrad.Insert_setting()
                insertflg = True
            End If



            If GdtSettings.Select("SettingName='DB_BACKUP_ON_EXIT'").Length > 0 Then
                DB_BACKUP_ON_EXIT = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='DB_BACKUP_ON_EXIT'").ToString)
            Else
                DB_BACKUP_ON_EXIT = 0
                objTrad.SettingName = "DB_BACKUP_ON_EXIT"
                objTrad.SettingKey = "0"
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='OPEN_ANALYSIS'").Length > 0 Then
                OPEN_ANALYSIS = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='OPEN_ANALYSIS'").ToString)
            Else
                OPEN_ANALYSIS = 0
                objTrad.SettingName = "OPEN_ANALYSIS"
                objTrad.SettingKey = "0"
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='POSITION_BACKUP_ON_EXIT'").Length > 0 Then
                POSITION_BACKUP_ON_EXIT = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='POSITION_BACKUP_ON_EXIT'").ToString)
            Else
                POSITION_BACKUP_ON_EXIT = "0"
                objTrad.SettingName = "POSITION_BACKUP_ON_EXIT"
                objTrad.SettingKey = "0"
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='FLGCSVCONTRACT'").Length > 0 Then
                Dim flg As Integer = 0
                flg = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='FLGCSVCONTRACT'").ToString)
                If flg = 0 Then
                    FLGCSVCONTRACT = False
                Else
                    FLGCSVCONTRACT = True
                End If
            Else
                FLGCSVCONTRACT = False
                objTrad.SettingName = "FLGCSVCONTRACT"
                objTrad.SettingKey = "0"
                objTrad.Insert_setting()
                insertflg = True

            End If

            'DB_BACKUP_ON_EXIT = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='DB_BACKUP_ON_EXIT'").ToString)
            ' POSITION_BACKUP_ON_EXIT = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='POSITION_BACKUP_ON_EXIT'").ToString)


            If GdtSettings.Select("SettingName='CHANGE_IN_OI'").Length > 0 Then
                CHANGE_IN_OI = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CHANGE_IN_OI'").ToString)
            Else

                objTrad.SettingName = "CHANGE_IN_OI"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If
            CHANGE_IN_OI = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CHANGE_IN_OI'").ToString)
            If GdtSettings.Select("SettingName='OPPOS_ENTRYDATE'").Length > 0 Then
                OPPOS_ENTRYDATE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='OPPOS_ENTRYDATE'").ToString)
            Else

                objTrad.SettingName = "OPPOS_ENTRYDATE"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='DEFAULT_EXPO_MARGIN'").Length > 0 Then
                DEFAULT_EXPO_MARGIN = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='DEFAULT_EXPO_MARGIN'").ToString)
            Else

                objTrad.SettingName = "DEFAULT_EXPO_MARGIN"
                objTrad.SettingKey = 5
                objTrad.Insert_setting()
                insertflg = True
            End If


            If GdtSettings.Select("SettingName='TCP_CON_NAME'").Length > 0 Then
                TCP_CON_NAME = GdtSettings.Compute("max(SettingKey)", "SettingName='TCP_CON_NAME'").ToString.Trim
            Else
                TCP_CON_NAME = ""
                objTrad.SettingName = "TCP_CON_NAME"
                objTrad.SettingKey = ""
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='RECALCULATE_POSITION'").Length > 0 Then
                RECALCULATE_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='RECALCULATE_POSITION'").ToString())
            Else
                RECALCULATE_POSITION = 0
                objTrad.SettingName = "RECALCULATE_POSITION"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If


            If GdtSettings.Select("SettingName='BEP_STRIKE'").Length > 0 Then
                BEP_STRIKE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='BEP_STRIKE'").ToString())
            Else
                BEP_STRIKE = 50
                objTrad.SettingName = "BEP_STRIKE"
                objTrad.SettingKey = 50
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='BEP_INTERVAL'").Length > 0 Then
                BEP_INTERVAL = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='BEP_INTERVAL'").ToString())
            Else
                BEP_INTERVAL = 100
                objTrad.SettingName = "BEP_INTERVAL"
                objTrad.SettingKey = 100
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='MULTIPLIER'").Length > 0 Then
                MULTIPLIER = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='MULTIPLIER'").ToString())
            Else
                MULTIPLIER = 0
                objTrad.SettingName = "MULTIPLIER"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='BHAVCOPYPROCESSDAY'").Length > 0 Then
                BHAVCOPYPROCESSDAY = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='BHAVCOPYPROCESSDAY'").ToString())
            Else

                objTrad.SettingName = "BHAVCOPYPROCESSDAY"
                objTrad.SettingKey = 10
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='SET_BHAVCOPY_ON_LTP_STOP'").Length > 0 Then
                SET_BHAVCOPY_ON_LTP_STOP = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='SET_BHAVCOPY_ON_LTP_STOP'").ToString)
            Else

                objTrad.SettingName = "SET_BHAVCOPY_ON_LTP_STOP"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If


            If GdtSettings.Select("SettingName='BEP_GMTM'").Length > 0 Then
                BEP_GMTM = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='BEP_GMTM'").ToString)
            Else

                objTrad.SettingName = "BEP_GMTM"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If


            If GdtSettings.Select("SettingName='BEP_VISIBLE'").Length > 0 Then
                BEP_VISIBLE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='BEP_VISIBLE'").ToString)
            Else

                objTrad.SettingName = "BEP_VISIBLE"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='BEP_STIKEDIFF'").Length > 0 Then
                BEP_STIKEDIFF = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='BEP_STIKEDIFF'").ToString)
            Else

                objTrad.SettingName = "BEP_STIKEDIFF"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='CAL_GREEK_WITH_BCASTDATE'").Length > 0 Then
                CAL_GREEK_WITH_BCASTDATE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CAL_GREEK_WITH_BCASTDATE'").ToString)
            Else

                objTrad.SettingName = "CAL_GREEK_WITH_BCASTDATE"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='ISIMPORTPOSITION'").Length > 0 Then
                ISIMPORTPOSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='ISIMPORTPOSITION'").ToString)
            Else
                ISIMPORTPOSITION = 0
                objTrad.SettingName = "ISIMPORTPOSITION"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If


            'Scenario setting
            If GdtSettings.Select("SettingName='LTP_PLUS_GAP'").Length = 0 Then
                objTrad.SettingName = "LTP_PLUS_GAP"
                objTrad.SettingKey = "10"
                objTrad.Insert_setting()
                insertflg = True
                'GdtSettings = objTrad.Settings
            End If
            If GdtSettings.Select("SettingName='LTP_MINUS_GAP'").Length = 0 Then
                objTrad.SettingName = "LTP_MINUS_GAP"
                objTrad.SettingKey = "-10"
                objTrad.Insert_setting()
                insertflg = True
                'GdtSettings = objTrad.Settings
            End If

            If GdtSettings.Select("SettingName='VOL_PLUS_GAP'").Length = 0 Then
                objTrad.SettingName = "VOL_PLUS_GAP"
                objTrad.SettingKey = "5"
                objTrad.Insert_setting()
                insertflg = True
                'GdtSettings = objTrad.Settings
            End If
            If GdtSettings.Select("SettingName='VOL_MINUS_GAP'").Length = 0 Then
                objTrad.SettingName = "VOL_MINUS_GAP"
                objTrad.SettingKey = "-5"
                objTrad.Insert_setting()
                insertflg = True
                'GdtSettings = objTrad.Settings
            End If

            If GdtSettings.Select("SettingName='GREEK_NEUTRAL'").Length > 0 Then
                GREEK_NEUTRAL = GdtSettings.Compute("max(SettingKey)", "SettingName='GREEK_NEUTRAL'").ToString.Trim
            Else
                GREEK_NEUTRAL = 0
                objTrad.SettingName = "GREEK_NEUTRAL"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='CALMARGINSPAN'").Length > 0 Then
                CALMARGINSPAN = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CALMARGINSPAN'").ToString)
            Else
                CALMARGINSPAN = 1
                objTrad.SettingName = "CALMARGINSPAN"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='ISEXCLUDE'").Length > 0 Then
                IsExclude = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='ISEXCLUDE'").ToString)
            Else
                IsExclude = 0
                objTrad.SettingName = "ISEXCLUDE"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='AELOPTION'").Length > 0 Then
                AELOPTION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='AELOPTION'").ToString)
            Else
                AELOPTION = 1
                objTrad.SettingName = "AELOPTION"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='INDEX_FAR_MONTH_OPTION'").Length > 0 Then
                INDEX_FAR_MONTH_OPTION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='INDEX_FAR_MONTH_OPTION'").ToString)
            Else
                INDEX_FAR_MONTH_OPTION = 5.0
                objTrad.SettingName = "INDEX_FAR_MONTH_OPTION"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='INDEX_OTM_OPTION'").Length > 0 Then
                INDEX_OTM_OPTION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='INDEX_OTM_OPTION'").ToString)
            Else
                INDEX_OTM_OPTION = 3.0
                objTrad.SettingName = "INDEX_OTM_OPTION"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='INDEX_OTH_OPTION'").Length > 0 Then
                INDEX_OTH_OPTION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='INDEX_OTH_OPTION'").ToString)
            Else
                INDEX_OTH_OPTION = 2.0
                objTrad.SettingName = "INDEX_OTH_OPTION"
                objTrad.SettingKey = 1
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='SAVE_DATA_AUTO'").Length > 0 Then
                SAVE_DATA_AUTO = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='SAVE_DATA_AUTO'").ToString)
            Else
                objTrad.SettingName = "SAVE_DATA_AUTO"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='SAVE_ANA_DATA_FILE'").Length > 0 Then
                SAVE_ANA_DATA_FILE = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='SAVE_ANA_DATA_FILE'").ToString)
            Else
                objTrad.SettingName = "SAVE_ANA_DATA_FILE"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If


            If GdtSettings.Select("SettingName='BHAVCOPY_FILE_PATH'").Length > 0 Then
                BhavCopyfilepath = GdtSettings.Compute("max(SettingKey)", "SettingName='BHAVCOPY_FILE_PATH'").ToString.Trim
            Else
                BhavCopyfilepath = ""
                objTrad.SettingName = "BHAVCOPY_FILE_PATH"
                objTrad.SettingKey = BhavCopyfilepath
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='AUTO_BHAVCOPY_REFRESH'").Length > 0 Then
                AUTO_BHAVCOPY_REFRESH = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='AUTO_BHAVCOPY_REFRESH'").ToString)
            Else
                objTrad.SettingName = "AUTO_BHAVCOPY_REFRESH"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='WAVV_SETTING'").Length > 0 Then
                WAVV_SETTING = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='WAVV_SETTING'").ToString)
            Else
                objTrad.SettingName = "WAVV_SETTING"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If
            'If GdtSettings.Select("SettingName='NEW_CM_BROADCAST'").Length > 0 Then
            '    NEW_CM_BROADCAST = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='NEW_CM_BROADCAST'").ToString)
            'Else
            '    objTrad.SettingName = "NEW_CM_BROADCAST"
            '    objTrad.SettingKey = 0
            '    objTrad.Insert_setting()
            '    insertflg = True
            'End If


            If GdtSettings.Select("SettingName='NEW_CM_BROADCAST_MT'").Length > 0 Then
                NEW_CM_BROADCAST_MT = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='NEW_CM_BROADCAST_MT'").ToString)
            Else
                objTrad.SettingName = "NEW_CM_BROADCAST_MT"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If
            NEW_CM_BROADCAST_MT = 0
            If GdtSettings.Select("SettingName='CAL_SYN_ON_EXPIRY'").Length > 0 Then
                CAL_SYN_ON_EXPIRY = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CAL_SYN_ON_EXPIRY'").ToString)
            Else
                objTrad.SettingName = "CAL_SYN_ON_EXPIRY"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If

            If GdtSettings.Select("SettingName='TRADINGVOL_SETTING'").Length > 0 Then
                TRADINGVOL_SETTING = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='TRADINGVOL_SETTING'").ToString)
            Else
                objTrad.SettingName = "TRADINGVOL_SETTING"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If
            If GdtSettings.Select("SettingName='NEW_BHAVCOPY'").Length > 0 Then
                NEW_BHAVCOPY = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='NEW_BHAVCOPY'").ToString)
            Else
                objTrad.SettingName = "NEW_BHAVCOPY"
                objTrad.SettingKey = 0
                objTrad.Insert_setting()
                insertflg = True
            End If


            'If GdtSettings.Select("SettingName='CALMARGINWITHOLDMETHOD'").Length > 0 Then
            '    CALMARGINWITHOLDMETHOD = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CALMARGINWITHOLDMETHOD'").ToString)
            'Else
            '    CALMARGINSPAN = 1
            '    objTrad.SettingName = "CALMARGINWITHOLDMETHOD"
            '    objTrad.SettingKey = 1
            '    objTrad.Insert_setting()
            '    insertflg = True
            'End If


            If GdtSettings.Select("SettingName='projmtom1'").Length > 0 Then
                projmtom1 = GdtSettings.Compute("max(SettingKey)", "SettingName='projmtom1'").ToString.Trim
            Else
                projmtom1 = "5,0"
                objTrad.SettingName = "projmtom1"
                objTrad.SettingKey = "5,0"
                objTrad.Insert_setting()

            End If

            If GdtSettings.Select("SettingName='projmtom2'").Length > 0 Then
                projmtom2 = GdtSettings.Compute("max(SettingKey)", "SettingName='projmtom2'").ToString.Trim
            Else
                projmtom2 = "-5,0"
                objTrad.SettingName = "projmtom2"
                objTrad.SettingKey = "-5,0"
                objTrad.Insert_setting()

            End If

            If GdtSettings.Select("SettingName='projmtom3'").Length > 0 Then
                projmtom3 = GdtSettings.Compute("max(SettingKey)", "SettingName='projmtom3'").ToString.Trim
            Else
                projmtom3 = "10,0"
                objTrad.SettingName = "projmtom3"
                objTrad.SettingKey = "10,0"
                objTrad.Insert_setting()

            End If

            If GdtSettings.Select("SettingName='projmtom4'").Length > 0 Then
                projmtom4 = GdtSettings.Compute("max(SettingKey)", "SettingName='projmtom4'").ToString.Trim
            Else
                projmtom4 = "-10,0"
                objTrad.SettingName = "projmtom4"
                objTrad.SettingKey = "-10,0"
                objTrad.Insert_setting()

            End If

            If GdtSettings.Select("SettingName='projmtom5'").Length > 0 Then
                projmtom5 = GdtSettings.Compute("max(SettingKey)", "SettingName='projmtom5'").ToString.Trim
            Else
                projmtom5 = "15,0"
                objTrad.SettingName = "projmtom5"
                objTrad.SettingKey = "15,0"
                objTrad.Insert_setting()

            End If
            If GdtSettings.Select("SettingName='projmtom6'").Length > 0 Then
                projmtom6 = GdtSettings.Compute("max(SettingKey)", "SettingName='projmtom6'").ToString.Trim
            Else
                projmtom6 = "-15,0"
                objTrad.SettingName = "projmtom6"
                objTrad.SettingKey = "-15,0"
                objTrad.Insert_setting()

            End If
            If GdtSettings.Select("SettingName='projmtom7'").Length > 0 Then
                projmtom7 = GdtSettings.Compute("max(SettingKey)", "SettingName='projmtom7'").ToString.Trim
            Else
                projmtom7 = "20,0"
                objTrad.SettingName = "projmtom7"
                objTrad.SettingKey = "20,0"
                objTrad.Insert_setting()

            End If
            If GdtSettings.Select("SettingName='projmtom8'").Length > 0 Then
                projmtom8 = GdtSettings.Compute("max(SettingKey)", "SettingName='projmtom8'").ToString.Trim
            Else
                projmtom8 = "-20,0"
                objTrad.SettingName = "projmtom8"
                objTrad.SettingKey = "-20,0"
                objTrad.Insert_setting()

            End If

            Try


                LTP_PLUS_GAP = GdtSettings.Select("SettingName='LTP_PLUS_GAP'")(0).Item("SettingKey").ToString()
                LTP_MINUS_GAP = GdtSettings.Select("SettingName='LTP_MINUS_GAP'")(0).Item("SettingKey").ToString()
                VOL_PLUS_GAP = GdtSettings.Select("SettingName='VOL_PLUS_GAP'")(0).Item("SettingKey").ToString()
                VOL_MINUS_GAP = GdtSettings.Select("SettingName='VOL_MINUS_GAP'")(0).Item("SettingKey").ToString()
            Catch ex As Exception

            End Try
            Rateofinterest = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Rateofinterest'").ToString)

            zero_qty_analysis = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='zero_qty_analysis'").ToString)
            NoofDay = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='NoofDay'").ToString)

            SPAN_PATH = GdtSettings.Compute("max(SettingKey)", "SettingName='SPAN_path'").ToString
            SPAN_TIMER = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='SPAN_TIMER'").ToString)
            TIMER_CALCULATION_INTERVAL = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Timer_Calculation_Interval'").ToString)
            REFRESH_TIME = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Refresh_time'").ToString)


            SET_BHAVCOPY_ON_LTP_STOP = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='SET_BHAVCOPY_ON_LTP_STOP'").ToString) '0
            G_DTImportSetting = objTrad.Select_Import_Setting()
            If insertflg = True Then
                GdtSettings = objTrad.Settings
            End If


            Update_TradingVol_column()

            ''    Str = GdtSettings.Compute("Max(SettingKey)", "SettingName='SECURITY FILE PATH'").ToString & "\" & GdtSettings.Compute("Max(SettingKey)", "SettingName='SECURITY FILE NAME'").ToString
            '   EXPORT_IMPORT_POSITION()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Function Delete_DataGrid_SummaryColumn_Setting() As Integer
        Try

            Dim qry As String

            qry = "DELETE  FROM DataGrid_SummaryColumn_Setting Where FormName='allcompany';"

            data_access.ExecuteQuery(qry)

        Catch ex As Exception


        End Try


    End Function
    Public Sub numonlywithoutdot(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        If Char.IsDigit(e.KeyChar) = False Then
            If Char.IsLetter(e.KeyChar) = True Then
                e.Handled = True
            End If

            If Char.IsWhiteSpace(e.KeyChar) = False Then
                Dim arr As New ArrayList
                arr.Add(Asc(" "))
                'arr.Add(Asc("+"))
                'arr.Add(Asc("."))
                arr.Add(8)
                If Not arr.Contains(Asc(e.KeyChar)) Then
                    e.Handled = True
                End If
                'If e.KeyChar <> "." And Asc(e.KeyChar) <> 8 Then
                '    e.Handled = True
                'End If
            Else
                e.Handled = True
            End If
        End If
    End Sub
    Private Function roundstr(ByVal round As Integer) As String
        Dim str As String
        str = "##0."
        If round > 0 Then
            For i As Integer = 0 To round - 1
                str = str & "0"
            Next
        Else
            str = "##0"
        End If
        Return str

    End Function


    Private Function GetAllIP() As ArrayList

        Dim arr As IPAddress
        Dim arrip As New ArrayList
        Dim childEntry As DirectoryEntry
        Dim ParentEntry As New DirectoryEntry
        Try
            ParentEntry.Path = "WinNT:"
            For Each childEntry In ParentEntry.Children
                Select Case childEntry.SchemaClassName
                    Case "Domain"

                        Dim SubChildEntry As DirectoryEntry
                        Dim SubParentEntry As New DirectoryEntry
                        SubParentEntry.Path = "WinNT://" & childEntry.Name
                        For Each SubChildEntry In SubParentEntry.Children

                            Select Case SubChildEntry.SchemaClassName
                                Case "Computer"
                                    Try
                                        If SubChildEntry.Name <> Dns.GetHostName() Then
                                            Dim ipEntry As IPHostEntry = Dns.GetHostByName(SubChildEntry.Name)
                                            arr = New IPAddress(ipEntry.AddressList(0).Address)
                                            arrip.Add(arr.ToString)
                                        End If
                                    Catch ex As Exception
                                    End Try

                            End Select
                        Next
                End Select
            Next

        Catch Excep As Exception
            'MsgBox("Error While Reading Directories")
        Finally
            ParentEntry = Nothing
        End Try
        Return arrip
    End Function
    Public Function ping_count() As Integer
        Dim count As Integer = 0
        Try
            Dim arr As New ArrayList
            arr = GetAllIP()
            For i As Integer = 0 To arr.Count - 1
                Try
                    Dim myIP As IPAddress = IPAddress.Parse(arr(i).ToString)
                    Dim tcpClient As New TcpClient()
                    tcpClient.Connect(myIP, 11003)
                    count += 1
                    tcpClient.Close()
                Catch e As Exception
                End Try
            Next
        Catch e As Exception
            'count = 0
            'Do what you want in here if the ping is unsuccessful
        End Try
        Return count
    End Function
    Public Sub openPort()
        tcpListener = New TcpListener(11003)
        tcpListener.Start()
    End Sub
    Public Sub closePort()
        Try
            tcpListener.Stop()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub start_count()
        Dim i As Integer
        i = ping_count()
        openPort()
        If i >= 10 Then
            '    'MsgBox(i)
            'Else
            Application.Exit()
        End If
    End Sub
    Dim Obj_Script As New script
    Public Sub GSub_Fill_TradingTables(ByVal DTTemp As DataTable)
        For Each DRow As DataRow In DTTemp.Rows
            If (DRow("cp") = "F" Or DRow("cp") = "C" Or DRow("cp") = "P") And (DRow("IsCurrency") = True) Then
                Obj_Script.InstrumentName = Currencymaster.Compute("Max(instrumentname)", "script='" & DRow("script") & "'")
                Obj_Script.Company = DRow("company")
                Obj_Script.Mdate = DRow("mdate")
                Obj_Script.StrikeRate = DRow("strikes")
                Obj_Script.CP = DRow("cp")
                Obj_Script.Script = DRow("script")
                Obj_Script.Units = DRow("units")
                Obj_Script.Rate = DRow("traded")
                Obj_Script.EntryDate = Now.AddDays(-1)
                Obj_Script.Token = DRow("tokanno")
                Obj_Script.Isliq = DRow("isliq")
                Obj_Script.orderno = 0
                Obj_Script.Dealer = "OP"
                Obj_Script.Insert_Currency_Trading()
            ElseIf DRow("cp") = "F" Or DRow("cp") = "C" Or DRow("cp") = "P" Then
                Obj_Script.InstrumentName = cpfmaster.Compute("Max(instrumentname)", "script='" & DRow("script") & "'")
                Obj_Script.Company = DRow("company")
                Obj_Script.Mdate = DRow("mdate")
                Obj_Script.StrikeRate = DRow("strikes")
                Obj_Script.CP = DRow("cp")
                Obj_Script.Script = DRow("script")
                Obj_Script.Units = DRow("units")
                Obj_Script.Rate = DRow("traded")
                Obj_Script.EntryDate = Now.AddDays(-1)
                Obj_Script.Token = DRow("tokanno")
                Obj_Script.Isliq = DRow("isliq")
                Obj_Script.orderno = 0
                Obj_Script.Dealer = "OP"
                Obj_Script.Insert()
            ElseIf DRow("cp") = "E" Then
                Obj_Script.CP = DRow("cp")
                Obj_Script.Company = DRow("company")
                Obj_Script.Script = DRow("script")
                Obj_Script.Units = DRow("units")
                Obj_Script.Rate = DRow("traded")
                Obj_Script.EntryDate = Now.AddDays(-1)
                Obj_Script.orderno = 0
                Obj_Script.Dealer = "OP"
                Obj_Script.insert_equity()
            End If
        Next
        objTrad.select_equity(GdtEQTrades)
        objTrad.Trading(GdtFOTrades)
        GdtCurrencyTrades = objTrad.select_Currency_Trading
    End Sub
    Public Function CalD1(ByVal mfutval As Double, ByVal mstkprice As Double, ByVal Mrateofinterast As Double, ByVal _mVolatility As Double, ByVal mDte As Double) As Double
        Try
            If mDte > 0 Then
                CalD1 = (((Math.Log(mfutval / mstkprice) + ((Mrateofinterast + (_mVolatility) ^ 2) / 2) * mDte)) / (_mVolatility * Math.Sqrt(mDte)))
            Else
                CalD1 = 0
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Function CalD2(ByVal mfutval As Double, ByVal mstkprice As Double, ByVal Mrateofinterast As Double, ByVal _mVolatility As Double, ByVal mDte As Double) As Double
        Try
            If mDte > 0 Then
                CalD2 = ((Math.Log(mfutval / mstkprice) + ((Mrateofinterast - (_mVolatility) ^ 2) / 2) * mDte)) / (_mVolatility * Math.Sqrt(mDte))
            Else
                CalD2 = 0
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Function CalVolga(ByVal mVega As Double, ByVal mD1 As Double, ByVal mD2 As Double, ByVal _mVolatility As Double) As Double
        Try
            CalVolga = (mVega * mD1 * mD2 / _mVolatility / 100)
        Catch ex As Exception

        End Try

    End Function
    Public Function CalVanna(ByVal mfutval As Double, ByVal mVega As Double, ByVal mD1 As Double, ByVal mD2 As Double, ByVal _mVolatility As Double, ByVal mDte As Double) As Double
        Try
            If mDte > 0 Then
                CalVanna = (mVega / mfutval * (1 - (mD1 / (_mVolatility * Math.Sqrt(mDte)))))
            Else
                CalVanna = 0
            End If
        Catch ex As Exception

        End Try
    End Function
    ''' <summary>
    ''' GFun_CheckLicDealerCount
    ''' </summary>
    ''' <param name="VarDlrCount">Total Dealer Count</param>
    ''' <returns></returns>
    ''' <remarks>This method return False if No.Of Dealer Limit Exceed else return true</remarks>
    Public Function GFun_CheckLicDealerCount(ByVal VarDlrCount As Integer) As Boolean
        If VarDlrCount > G_VarNoOfDealer Then REM Check Dealer Count againest License Dealer Count
            MsgBox("According to License your Dealer Limit is :: " & G_VarNoOfDealer & vbCrLf & " Dealer limit can't be Exceed!!", MsgBoxStyle.Exclamation)
            Return False
        Else
            Return True
        End If
    End Function

    Public Function GFun_CheckLicDealerCountFOEQCURR(ByVal VarDlrstr As String) As Boolean
        Try


            Dim dt As New DataTable
            dt = New DataTable
            Dim dr As DataRow
            With dt.Columns
                .Add("type", GetType(String))
                .Add("code", GetType(String))
            End With

            Dim words As String() = VarDlrstr.Split(New Char() {","c})

            ' Use For Each loop over words and display them.
            Dim word As String
            For Each word In words

                Dim words1 As String() = word.Split(New Char() {"-"c})
                If words1.Length >= 2 Then
                    dr = dt.NewRow()
                    dr("type") = words1(0)
                    dr("Code") = words1(1)
                    dt.Rows.Add(dr)
                End If

                'Console.WriteLine(word)
            Next
            If dt.Select("type='FO' ").Length > G_VarNoOfDealer Or dt.Select("type='EQ' ").Length > G_VarNoOfDealer Or dt.Select("type='CURR' ").Length > G_VarNoOfDealer Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            MessageBox.Show("GFun_CheckLicDealerCountFOEQCURR ERROR....")
        End Try


        'If VarDlrCount > G_VarNoOfDealer Then REM Check Dealer Count againest License Dealer Count
        '    MsgBox("According to License your Dealer Limit is :: " & G_VarNoOfDealer & vbCrLf & " Dealer limit can't be Exceed!!", MsgBoxStyle.Exclamation)
        '    Return False
        'Else
        '    Return True
        'End If
    End Function
    Public Sub Read_picmargin()
        If System.IO.File.Exists(Application.StartupPath + "/PicMargin.xml") Then     ' check if XML file Exists
            Try



                Dim xmldoc As New XmlDataDocument()
                Dim xmlnode As XmlNodeList
                Dim i As Integer
                Dim str As String
                Dim fs As New FileStream("PicMargin.xml", FileMode.Open, FileAccess.Read)
                xmldoc.Load(fs)
                xmlnode = xmldoc.GetElementsByTagName("PicMargin")
                For i = 0 To xmlnode.Count - 1
                    xmlnode(i).ChildNodes.Item(0).InnerText.Trim()
                    str = xmlnode(i).ChildNodes.Item(0).InnerText.Trim() & "  " & xmlnode(i).ChildNodes.Item(1).InnerText.Trim() & "  " & xmlnode(i).ChildNodes.Item(2).InnerText.Trim()
                    If tblpicmargin.Select("compname = '" & xmlnode(i).ChildNodes.Item(0).InnerText.Trim() & "'").Length = 0 Then
                        Dim drpicmargin As DataRow
                        drpicmargin = tblpicmargin.NewRow

                        drpicmargin("compname") = xmlnode(i).ChildNodes.Item(0).InnerText.Trim()
                        drpicmargin("Margin") = xmlnode(i).ChildNodes.Item(1).InnerText.Trim()
                        drpicmargin("Date") = xmlnode(i).ChildNodes.Item(2).InnerText.Trim()

                        tblpicmargin.Rows.Add(drpicmargin)
                    Else
                        Dim tmmpicmargin As Double = Convert.ToDouble(tblpicmargin.Compute("sum(Margin)", "compname='" & xmlnode(i).ChildNodes.Item(0).InnerText.Trim() & "'"))
                        For Each mrow As DataRow In tblpicmargin.Select("compname = '" & xmlnode(i).ChildNodes.Item(0).InnerText.Trim() & "'")
                            If tmmpicmargin < Convert.ToDouble(xmlnode(i).ChildNodes.Item(1).InnerText.Trim()) Then
                                mrow("Margin") = Convert.ToDouble(xmlnode(i).ChildNodes.Item(1).InnerText.Trim())
                            End If

                        Next
                    End If

                Next
            Catch ex As Exception

            End Try
        End If
    End Sub
    Public Sub DataTable_To_Text(ByVal table As DataTable)
        Try


            ' If Not System.IO.File.Exists(Application.StartupPath + "/PicMargin.xml") Then     ' check if XML file Exists
            Dim writer As New XmlTextWriter("PicMargin.xml", System.Text.Encoding.UTF8)
            writer.WriteStartDocument(True)
            writer.Formatting = Formatting.Indented
            writer.Indentation = 2
            writer.WriteStartElement("Table")

            For Each DRow As DataRow In table.Rows
                createNode(DRow("compname"), Convert.ToDouble(DRow("Margin")), Convert.ToDateTime(DRow("Date").ToString("dd/MMM/yyyy")), writer)
            Next


            'createNode(2, "Product 2", "2000", writer)
            'createNode(3, "Product 3", "3000", writer)
            'createNode(4, "Product 4", "4000", writer)
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()

            ' End If



        Catch ex As Exception

        End Try



    End Sub

    Private Sub createNode(ByVal compname As String, ByVal Margin As Double, ByVal Date1 As DateTime, ByVal writer As XmlTextWriter)
        writer.WriteStartElement("PicMargin")
        writer.WriteStartElement("compname")
        writer.WriteString(compname)
        writer.WriteEndElement()
        writer.WriteStartElement("Margin")
        writer.WriteString(Margin)
        writer.WriteEndElement()
        writer.WriteStartElement("Date")
        writer.WriteString(Date1)
        writer.WriteEndElement()
        writer.WriteEndElement()
    End Sub
    Public Function FunG_GetMACAddress() As String
        Try
            Dim result As String = ""
            Dim mc As System.Management.ManagementClass
            Dim mo As ManagementObject
            mc = New ManagementClass("Win32_NetworkAdapterConfiguration")
            Dim moc As ManagementObjectCollection = mc.GetInstances()
            For Each mo In moc
                If mo.Item("IPEnabled") = True Then
                    Dim strAdapter As String
                    strAdapter = mo.Item("Caption").ToString().Substring(11)
                    result = Replace(mo.Item("MacAddress").ToString, ":", "-")
                End If
            Next
            Return result
        Catch ex As Exception
            Write_TimeLog1("FunG_GetMACAddress :" + ex.Message)
        End Try
    End Function

    'Public Function FunG_ProcID() As String
    '    Try
    '        Dim searcher As New ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor")

    '        For Each obj As ManagementObject In searcher.Get()
    '            Dim Res As String = If(obj("ProcessorId")?.ToString(), "")
    '            Res = Fung_TrimLicString(Res)
    '            If (Res.Length > 10) Then
    '                Res = Res.Substring(0, 10)
    '            End If
    '            Return Res
    '        Next
    '    Catch ex As Exception
    '        Return ""
    '    End Try

    '    Return ""
    'End Function

    'Public Function FunG_BoardID() As String
    '    Dim res As String
    '    Try
    '        Dim searcher As New ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard")
    '        For Each obj As ManagementObject In searcher.Get()
    '            res = If(obj("SerialNumber")?.ToString().Trim(), "")
    '            res = Fung_TrimLicString(res)
    '            If (res.Length > 10) Then
    '                res = res.Substring(0, 10)
    '            End If
    '            Return res
    '        Next
    '    Catch ex As Exception
    '        Return ""
    '    End Try

    '    Return ""
    'End Function

    'Public Function FunG_HddID0() As String
    '    Dim res As String
    '    Try
    '        Dim devID As String
    '        Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive")
    '        For Each obj As ManagementObject In searcher.Get()
    '            '"DRIVE0";
    '            devID = If(obj("DeviceID")?.ToString().Trim(), "")
    '            If devID.Contains("DRIVE0") Then
    '                res = If(obj("SerialNumber")?.ToString().Trim(), "")
    '                res = Fung_TrimLicString(res)
    '                Return res
    '            End If

    '        Next
    '    Catch ex As Exception
    '        Return "00000000"
    '    End Try

    '    Return "000000"
    'End Function

    'Public Function FunG_HddID() As String
    '    Dim res As String
    '    Try
    '        Dim devID As String
    '        Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive")
    '        For Each obj As ManagementObject In searcher.Get()
    '            '"DRIVE0";
    '            devID = If(obj("DeviceID")?.ToString().Trim(), "")
    '            If devID.Contains("DRIVE0") Then
    '                res = If(obj("SerialNumber")?.ToString().Trim(), "")
    '                res = Fung_TrimLicString(res)
    '                If (res.Length > 10) Then
    '                    res = res.Substring(0, 10)
    '                End If
    '                Return res
    '            End If

    '        Next
    '    Catch ex As Exception
    '        Return "00000000"
    '    End Try

    '    Return "000000"
    'End Function

    'Public Function FunG_HddID2() As String
    '    Dim res As String
    '    Try
    '        Dim devID As String
    '        Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive")
    '        For Each obj As ManagementObject In searcher.Get()
    '            '"DRIVE0";
    '            devID = If(obj("DeviceID")?.ToString().Trim(), "")
    '            If devID.Contains("DRIVE0") Then
    '                res = If(obj("SerialNumber")?.ToString().Trim(), "")
    '                res = res.Replace("0", "")
    '                res = Fung_TrimLicString(res)
    '                If (res.Length > 10) Then
    '                    res = res.Substring(0, 10)
    '                End If
    '                Return res
    '            End If

    '        Next
    '    Catch ex As Exception
    '        Return "00000000"
    '    End Try

    '    Return "000000"
    'End Function

    Public Function Fung_TrimLicString(ByVal pStr As String) As String
        Dim res As String = ""
        res = pStr.Replace("-", "")
        res = res.Replace(".", "")
        res = res.Replace(":", "")
        res = res.Replace(" ", "")
        res = res.Replace("_", "")
        Return res
    End Function

    Public Function Fung_Lic_Enc(ByVal pStr As String, ByVal pK As String) As String
        Dim res As String


        Return res
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
    Private Function GetVolumeInformation(ByVal lpRootPathName As String,
                                          ByVal lpVolumeNameBuffer As String,
                                          ByVal nVolumeNameSize As Integer,
                                          ByRef lpVolumeSerialNumber As UInteger,
                                          ByRef lpMaximumComponentLength As UInteger,
                                          ByRef lpFileSystemFlags As UInteger,
                                          ByVal lpFileSystemNameBuffer As String,
                                          ByVal nFileSystemNameSize As Integer) As Boolean
    End Function

    Public Function GetVolStr() As String

        Dim serial As UInteger
        Dim maxCompLen As UInteger
        Dim fileSysFlags As UInteger

        GetVolumeInformation("C:\", Nothing, 0, serial, maxCompLen, fileSysFlags, Nothing, 0)

        Return serial.ToString()

    End Function


    Public Function GetSystemUUID() As String
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT UUID FROM Win32_ComputerSystemProduct")
            For Each obj As ManagementObject In searcher.Get()
                If obj("UUID") IsNot Nothing Then
                    Return obj("UUID").ToString()
                End If
            Next
        Catch ex As Exception
            ' Optional: log or handle exception
        End Try
        Return String.Empty
    End Function


End Module

