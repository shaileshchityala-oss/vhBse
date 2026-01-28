Imports System.IO
Imports System

Module AdminModule


    'Public FSTimerLogFile As System.IO.StreamWriter

    'Public G_VarIsTrailVersion As Boolean = True ' If Version Is Trail Version then True else False
    Public AppLicMode As String = ""
    REM These variable declare to store version title and Master Expiry
    Public GVar_Master_Expiry As Date
    Public GVar_Version_Title As String
    REM End

    Public G_VarExpiryDate As Date = clsGlobal.Expire_Date
    Public G_VarExpiryDate1 As Long = DateDiff(DateInterval.Second, CDate("1-1-1980"), clsGlobal.Expire_Date)

    Public IsVersionExpire As Boolean = False

    
    REM Used For store Tokens of BCast And Sql DataReceive 
    Public futall As New ArrayList
    Public Currfutall As New ArrayList
    Public eqfutall As New ArrayList

    REM Commented By Viral Because Of  Decelared in Global Class This Virable Move To ClsGlobal
    'Public AcessPassWord As String = "FintEstpwD"
    REM End

    ''' <summary>
    ''' G_VarDealerCount
    ''' </summary>
    ''' <remarks>No. of Dealer License assign to Client</remarks>
    Public G_VarNoOfDealer As Integer = 1
    Public G_VarIsAMC As Boolean = False

    Public G_VarCurrBDate As Date = CDate("1-1-1980")

    Dim tptable As DataTable

    Dim objAna As New analysisprocess
    Dim tempdate As Date

    Dim lActivityTime As Long

    Public Gtbl_Summary_Analysis As DataTable
    Public Gtbl_Summary_Analysis_ExpiryWise As DataTable
    Dim acomp As New DataTable


    Public hashOrder As New Hashtable
    Public VarImportInserted As Boolean = False REM it's Value true then Import Data From Database or Textfile
    Public VarFileImport As Boolean = False REM it's value true then file imported manualy

    Dim VarIsCurrency As Boolean = False
    Dim objimport As import_Data = New import_Data

    REM Global Hash table Declare to Store LTP Price, Buy Price & Sales Price
    Public buyprice As New Hashtable
    Public saleprice As New Hashtable
    Public ltpprice As New Hashtable
    Public MKTltpprice As New Hashtable
    Public volumeprice As New Hashtable
    Public OpenInterestprice As New Hashtable
    Public OpenInterestbhavcopy As New Hashtable

    Public ss_buyprice As New Hashtable
    Public ss_saleprice As New Hashtable
    Public ss_ltpprice As New Hashtable
    Public ss_MKTltpprice As New Hashtable
    Public ss_volumeprice As New Hashtable

    Public closeprice As New Hashtable

    Public ss_closeprice As New Hashtable

    Public TrendStatus As New Hashtable
    Public TrendStopLoss As New Hashtable

    Public fbuyprice As New Hashtable
    Public fsaleprice As New Hashtable
    Public fltpprice As New Hashtable
    Public udpselsymbol As New Hashtable

    Public ss_fbuyprice As New Hashtable
    Public ss_fsaleprice As New Hashtable
    Public ss_fltpprice As New Hashtable

    Public ebuyprice As New Hashtable
    Public esaleprice As New Hashtable
    Public eltpprice As New Hashtable
    Public equityVol As New Hashtable

    Public eIdxprice As New Hashtable
    Public eIdxClosingprice As New Hashtable

    Public Currbuyprice As New Hashtable
    Public Currsaleprice As New Hashtable
    Public Currltpprice As New Hashtable
    Public Currfbuyprice As New Hashtable
    Public Currfsaleprice As New Hashtable
    Public Currfltpprice As New Hashtable
    Public currMKTltpprice As New Hashtable
    REM End

    Dim objTrad As New trading
    Public DTAllTableMerge As New DataTable

    Public Godj_Frm_Analysis_Summary As New allcompany

    Public VarCurrentDate As Integer

    Public VarFoBCurrentDate As Integer   'Check CAL_GREEK_WITH_BCASTDATE For Currency 
    Public VarEQBCurrentDate As Integer
    Public VarCurBCurrentDate As Integer

    Public GPatchExpDiff As Double

    Public obj_CLS_UDP As New CLS_Check_UDP_Licence

    Public Delegate Sub GDelegate_with_Double_Para(ByVal obj As Boolean, ByVal obj1 As String)
    Public Delegate Sub GDelegate_MdiStatus(ByVal obj As String, ByVal obj1 As String)

#Region "Import Trade Files"

    Private Function GSub_ImporttxtFileToTrading(ByRef DTData As DataTable, ByVal VarFileFlag As String, ByVal VarDealerPrefix As String, ByVal VarImportType As String) As Boolean
        Dim VarTokenNo As Integer
        DTData.Columns.Add("Token1", GetType(Int32))
        DTData.Columns.Add("IsLiq", GetType(Int16))
        DTData.Columns.Add("lActivityTime", GetType(Int32))
        DTData.Columns.Add("entry_date", GetType(Date)) REM this column store only Date value of entry date
        Dim status As Boolean
        If VarImportType = "FO" Then
            Dim DTTrade As New DataTable
            REM 1: create datatable for future and option
            With DTTrade.Columns
                .Add("entryno", GetType(Integer))
                .Add("instrumentname")
                .Add("company")
                .Add("mdate", GetType(Date))
                .Add("strikerate", GetType(Double))
                .Add("cp")
                .Add("qty", GetType(Double))
                .Add("rate", GetType(Double))
                .Add("tot", GetType(Double))
                .Add("tot2", GetType(Double))
                .Add("entrydate", GetType(Date))
                .Add("script")
                .Add("token1", GetType(Long))
                .Add("isliq")
                .Add("orderno", GetType(Long))
                .Add("lActivityTime", GetType(Long))
                .Add("FileFlag")
                .Add("Dealer")
            End With
            REM 1: END
            Dim tempdate As Date
            tempdate = CDate(DTData.Rows(0)("entrydate").ToString)
            Dim VarOptionType As String = ""
            For Each DR As DataRow In DTData.Rows 'Select("Dealer = '" & dealer & "'")
                If hashOrder.ContainsKey(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim) = True Then
                    Continue For
                Else
                    hashOrder.Add(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim, "1")
                End If
                DR("lActivityTime") = DateDiff(DateInterval.Second, CDate("1-1-1980"), CDate(DR("entrydate")))
                If DR("instrumentname") = "FUTSTK" Or DR("instrumentname") = "FUTIDX" Or DR("instrumentname") = "FUTIVX" Then
                    DR("cp") = "F"
                End If
                If DR("cp").ToString.Length > 0 Then
                    VarOptionType = Mid(DR("cp"), 1, 1)
                Else
                    VarOptionType = "F"
                End If
                If VarOptionType = "C" Or VarOptionType = "P" Then
                    Dim VarScript As String = DR("instrumentname") & Space(2) & GetSymbol(DR("company")) & Space(2) & Format(CDate(DR("mdate")), "ddMMMyyyy") & Space(2) & Format(Val(DR("strikerate")), "#0.00") & Space(2) & DR("cp")
                    REM Select Scrip From cpfmaster changes on 17-12-2010 by Divyesh
                    Dim DrScriptROw() As DataRow = cpfmaster.Select("Script='" & VarScript & "'")
                    If DrScriptROw.Length > 0 Then
                        DR("Script") = DrScriptROw(0)("Script").ToString.ToUpper
                        VarTokenNo = DrScriptROw(0)("token")
                    Else
                        VarTokenNo = 0
                    End If
                    REM End
                ElseIf DR("cp") = "F" Then
                    DR("Script") = (DR("instrumentname") & Space(2) & GetSymbol(DR("company")) & Space(2) & Format(CDate(DR("mdate")), "ddMMMyyyy")).ToString.ToUpper
                    REM changes on 17-12-2010 by Divyesh
                    VarTokenNo = Val(cpfmaster.Compute("MAX(Token)", "Script='" & DR("Script") & "'").ToString)
                    REM End
                End If
                If VarTokenNo > 0 And (VarOptionType = "F" Or VarOptionType = "P" Or VarOptionType = "C") Then
                    DR("Token1") = VarTokenNo
                    REM 3.1: Process for future and option
                    Dim DrTr As DataRow = DTTrade.NewRow
                    DrTr("instrumentname") = DR("instrumentname")
                    DrTr("company") = DR("company")
                    DrTr("mdate") = DR("mdate")
                    DrTr("strikerate") = Val(DR("strikerate").ToString)
                    DrTr("cp") = VarOptionType
                    DrTr("Script") = DR("Script").ToString.ToUpper
                    DrTr("Rate") = DR("Rate")
                    If CInt(DR("buysell")) = 1 Then
                        DrTr("Qty") = DR("Qty")
                        DrTr("tot") = Val(DR("Qty")) * Val(DR("Rate"))
                        DrTr("tot2") = Val(DR("Qty")) * (Val(DR("Rate")) + Val(DR("strikerate").ToString))
                    Else
                        DR("Qty") = -Math.Abs(DR("Qty"))
                        DrTr("Qty") = DR("Qty")
                        DrTr("tot") = (Val(DR("Qty")) * Val(DR("Rate")))
                        DrTr("tot2") = (Val(DR("Qty")) * (Val(DR("Rate"))) + Val(DR("strikerate").ToString))
                    End If
                    DR("entry_date") = CDate(DR("entrydate")).Date
                    DrTr("entrydate") = DR("entrydate")
                    DrTr("entryno") = Val(DR("entryno").ToString)
                    If VarOptionType = "F" Then
                        DrTr("Token1") = 0
                    Else
                        Dim a, a1, script1 As String
                        a = Mid(DR("Script").ToString, Len(DR("Script").ToString) - 1, 1)
                        a1 = Mid(DR("Script").ToString, Len(DR("Script").ToString), 1)
                        If a = "C" Then
                            script1 = Mid(DR("Script").ToString, 1, Len(DR("Script").ToString) - 2) & "P" & a1
                        Else
                            script1 = Mid(DR("Script").ToString, 1, Len(DR("Script").ToString) - 2) & "C" & a1
                        End If
                        VarTokenNo = CLng(Val(cpfmaster.Compute("max(token)", "script='" & script1 & "'").ToString))
                        DrTr("Token1") = VarTokenNo
                    End If
                    DrTr("IsLiq") = False

                    DrTr("OrderNo") = Val(DR("OrderNo").ToString)
                    DrTr("lActivityTime") = Val(DR("lActivityTime").ToString)
                    DrTr("FileFlag") = VarFileFlag
                    DrTr("dealer") = DR("Dealer").ToString
                    DTTrade.Rows.Add(DrTr)
                End If
            Next
            Dim ti1 As Long = System.Environment.TickCount
            REM Insert trade into trading table
            'Dim STime As Date
            'Dim ETime As Date
            'STime = Date.Now
            objTrad.Insert_FOTrading(DTTrade)
            'ETime = Date.Now

            'MsgBox("objTrad.Insert_FOTrading Time : " & DateDiff(DateInterval.Second, STime, ETime))

            REM End
            REM insert to global dtFotrades datatable
            Call insert_FOTradeToGlobalTable(DTTrade)
            REM End
            'FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Record Insert in Database and Global Table :(" & System.Environment.TickCount - ti1 & ")")
            Write_ErrorLog3(Now.ToString() & "-" & "Record Insert in Database and Global Table :(" & System.Environment.TickCount - ti1 & ")")
            status = False
            If (DTTrade.Rows.Count > 0) Then
                VarImportInserted = True
                status = True
                Dim ti As Long = System.Environment.TickCount
                'Dim ST As Date
                'Dim ET As Date
                'ST = Date.Now
                objAna.process_data_FO(DTTrade)
                'ET = Date.Now
                'MsgBox("objAna.process_data_FO Time : " & DateDiff(DateInterval.Second, ST, ET))
                ' FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Prosess On FO Trades(MS) :" & System.Environment.TickCount - ti)
                Write_ErrorLog3(Now.ToString() & "-" & "Prosess On FO Trades(MS) :" & System.Environment.TickCount - ti)
            End If
        ElseIf VarImportType = "EQ" Then
            Dim DTEquity As New DataTable
            REM 2: create datatable for equity
            With DTEquity.Columns
                .Add("script")
                .Add("company")
                .Add("eq")
                .Add("qty", GetType(Double))
                .Add("rate", GetType(Double))
                .Add("tot", GetType(Double))
                .Add("tot2", GetType(Double))

                .Add("entrydate", GetType(Date))
                .Add("entryno", GetType(Integer))
                .Add("orderno", GetType(Long))
                .Add("lActivityTime", GetType(Long))
                .Add("FileFlag")
                .Add("Dealer")
            End With
            REM 2: END
            tempdate = CDate(DTData.Rows(0)("entrydate").ToString)
            For Each DR As DataRow In DTData.Rows 'Select("dealer = " & dealer)
                If hashOrder.ContainsKey(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim) = True Then
                    Continue For
                Else
                    hashOrder.Add(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim, "1")
                End If
                DR("lActivityTime") = DateDiff(DateInterval.Second, CDate("1-1-1980"), CDate(DR("entrydate")))
                DR("Script") = GetSymbol(DR("company")) & Space(2) & DR("cp")
                DR("Dealer") = VarDealerPrefix & DR("Dealer")
                VarTokenNo = Val(eqmaster.Compute("MAX(token)", "Script='" & DR("Script") & "'").ToString)
                If VarTokenNo > 0 Then
                    DR("Token1") = VarTokenNo 'Update Token In Datatable
                    Dim DrEq As DataRow = DTEquity.NewRow
                    DrEq("Script") = DR("Script")
                    DrEq("company") = DR("company")
                    DrEq("Eq") = DR("cp")
                    DrEq("Rate") = DR("Rate")
                    If CInt(DR("buysell")) = 1 Then
                        DrEq("Qty") = DR("Qty")
                        DrEq("tot") = Val(DR("Qty")) * Val(DR("Rate"))
                        DrEq("tot2") = Val(DR("Qty")) * Val(DR("Rate"))
                    Else
                        DrEq("Qty") = -DR("Qty")
                        DrEq("tot") = -(Val(DR("Qty")) * Val(DR("Rate")))
                        DrEq("tot2") = -(Val(DR("Qty")) * Val(DR("Rate")))
                    End If
                    DR("entry_date") = CDate(DR("entrydate")).Date
                    DrEq("entrydate") = DR("entrydate")
                    DrEq("entryno") = Val(DR("entryno").ToString)
                    DrEq("OrderNo") = Val(DR("OrderNo").ToString)
                    DrEq("lActivityTime") = Val(DR("lActivityTime").ToString)
                    'DrEq("Dealer") = DR("Dealer")
                    DrEq("FileFlag") = VarFileFlag
                    DrEq("Dealer") = DR("Dealer").ToString
                    DTEquity.Rows.Add(DrEq)
                End If
            Next
            REM Insert trade into Equity_trading table
            objTrad.insert_EQTrading(DTEquity)
            REM End
            REM insert equity to global Equity trade datatable
            insert_EQTradeToGlobalTable(DTEquity)
            REM End
            status = False
            If (DTEquity.Rows.Count > 0) Then
                VarImportInserted = True
                status = True
                Dim ti As Long = System.Environment.TickCount
                objAna.process_data_EQ(DTEquity)
                ' FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Prosess On EQ Trades(MS) :" & System.Environment.TickCount - ti)
                Write_ErrorLog3(Now.ToString() & "-" & "Prosess On EQ Trades(MS) :" & System.Environment.TickCount - ti)
                'MsgBox("File imported Successfully", MsgBoxStyle.Information)
            End If
        ElseIf VarImportType = "CURRENCY" Then
            Dim DTCurr As New DataTable
            REM 1: create datatable for currency
            With DTCurr.Columns
                .Add("entryno", GetType(Integer))
                .Add("instrumentname")
                .Add("company")
                .Add("mdate", GetType(Date))
                .Add("strikerate", GetType(Double))
                .Add("cp")
                .Add("qty", GetType(Double))
                .Add("rate", GetType(Double))
                .Add("tot", GetType(Double))
                .Add("tot2", GetType(Double))
                .Add("entrydate", GetType(Date))
                .Add("script")
                .Add("token1", GetType(Long))
                .Add("isliq")
                .Add("orderno", GetType(Long))
                .Add("lActivityTime", GetType(Long))
                '   .Add("dealer", GetType(String))
                .Add("FileFlag")
                .Add("Dealer")
            End With
            REM 1: END
            Dim tempdate As Date
            tempdate = CDate(DTData.Rows(0)("entrydate").ToString)
            For Each DR As DataRow In DTData.Rows 'Select("Dealer = '" & dealer & "'")
                Dim orderno As String = DR("orderno").ToString.Trim
                Dim entryno As String = DR("entryno").ToString.Trim
                If GdtCurrencyTrades.Select("orderno='" & orderno & "' and entryno ='" & entryno & "'").Length > 0 Then
                    Continue For
                End If

                DR("lActivityTime") = DateDiff(DateInterval.Second, CDate("1-1-1980"), CDate(DR("entrydate")))
                If DR("instrumentname") <> "OPTCUR" Then
                    DR("cp") = "F"
                End If
                If DR("cp").ToString.Length > 0 Then
                    DR("cp") = Mid(DR("cp"), 1, 1)
                End If
                If DR("cp") = "C" Or DR("cp") = "P" Then
                    If DR("instrumentname") = "OPTCUR" Then
                        DR("Script") = DR("instrumentname") & Space(2) & GetSymbol(DR("company")) & Space(2) & Format(CDate(DR("mdate")), "ddMMMyyyy") & Space(2) & DR("strikerate") & Space(2) + DR("cp") & "E"
                    Else
                        'DR("Script") = DR("instrumentname") & Space(2) & DR("company") & Space(2) & Format(CDate(DR("mdate")), "ddMMMyyyy") & Space(2) & DR("strikerate") & Space(2) + DR("cp") & "A"
                        DR("Script") = DR("instrumentname") & Space(2) & GetSymbol(DR("company")) & Space(2) & Format(CDate(DR("mdate")), "ddMMMyyyy") & Space(2) & DR("strikerate") & Space(2) + DR("cp") & "E"
                    End If
                ElseIf DR("cp") = "F" Then
                    DR("Script") = DR("instrumentname") & Space(2) & GetSymbol(DR("company")) & Space(2) & Format(CDate(DR("mdate")), "ddMMMyyyy")
                End If
                'DR("Dealer") = VarDealerPrefix & DR("Dealer")
                Dim DRCurrVal() As DataRow = Currencymaster.Select("Script='" & DR("Script") & "'")

                'VarTokenNo = Val(Currencymaster.Compute("MAX(Token)", "Script='" & DR("Script") & "'").ToString)
                If DRCurrVal.Length > 0 And (DR("cp") = "F" Or DR("cp") = "P" Or DR("cp") = "C") Then
                    DR("Token1") = DRCurrVal(0).Item("Token")
                    REM 3.1: Process for currency
                    Dim DrCurr As DataRow = DTCurr.NewRow
                    DrCurr("instrumentname") = DR("instrumentname")
                    DrCurr("company") = DR("company")
                    DrCurr("mdate") = DR("mdate")
                    DrCurr("strikerate") = Val(DR("strikerate").ToString)
                    DrCurr("cp") = DR("cp")
                    DrCurr("Script") = DR("Script")
                    DrCurr("Rate") = DR("Rate")

                    If CInt(DR("buysell")) = 1 Then
                        DrCurr("Qty") = DR("Qty") * DRCurrVal(0).Item("multiplier")
                        DrCurr("tot") = Val(DrCurr("Qty")) * Val(DR("Rate"))
                        DrCurr("tot2") = Val(DrCurr("Qty")) * (Val(DR("Rate")) + Val(DR("strikerate").ToString))
                    Else
                        'DrCurr("Qty") = -DR("Qty")
                        DrCurr("Qty") = -Math.Abs(DR("Qty") * DRCurrVal(0).Item("multiplier"))
                        DrCurr("tot") = -Math.Abs(Val(DrCurr("Qty")) * Val(DR("Rate")))
                        DrCurr("tot2") = -Math.Abs(Val(DrCurr("Qty")) * (Val(DR("Rate")) + Val(DR("strikerate").ToString)))
                    End If
                    DR("entry_date") = CDate(DR("entrydate")).Date
                    DrCurr("entrydate") = DR("entrydate")
                    DrCurr("entryno") = Val(DR("entryno").ToString)
                    If DR("cp") = "F" Then
                        DrCurr("Token1") = 0
                    Else
                        Dim a, a1, script1 As String
                        a = Mid(DR("Script").ToString, Len(DR("Script").ToString) - 1, 1)
                        a1 = Mid(DR("Script").ToString, Len(DR("Script").ToString), 1)
                        If a = "C" Then
                            script1 = Mid(DR("Script").ToString, 1, Len(DR("Script").ToString) - 2) & "P" & a1
                        Else
                            script1 = Mid(DR("Script").ToString, 1, Len(DR("Script").ToString) - 2) & "C" & a1
                        End If
                        VarTokenNo = CLng(Val(cpfmaster.Compute("max(token)", "script='" & script1 & "'").ToString))
                        DrCurr("Token1") = VarTokenNo
                    End If
                    DrCurr("IsLiq") = False
                    DrCurr("OrderNo") = Val(DR("OrderNo").ToString)
                    DrCurr("lActivityTime") = Val(DR("lActivityTime").ToString)
                    DrCurr("FileFlag") = VarFileFlag
                    DrCurr("Dealer") = DR("Dealer").ToString
                    DTCurr.Rows.Add(DrCurr)
                End If
            Next
            objTrad.insert_Currency_Trading(DTCurr)
            insert_CurrencyTradeToGlobalTable(DTCurr)
            status = False
            If (DTCurr.Rows.Count > 0) Then
                VarImportInserted = True
                status = True
                Dim ti As Long = System.Environment.TickCount
                objAna.process_data_Currency(DTCurr)
                ' FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Prosess On Currency Trades(MS) :" & System.Environment.TickCount - ti)
                Write_ErrorLog3(Now.ToString() & "-" & "Prosess On Currency Trades(MS) :" & System.Environment.TickCount - ti)
            End If
        End If

        DTData.AcceptChanges()
        DTData = New DataView(DTData, "Token1 > 0", "", DataViewRowState.CurrentRows).ToTable

        Return status
    End Function
    Private Function GetDTFromTextFile(ByVal SeparatorChar As String, ByVal FilePath As String, ByVal ColList As Hashtable, ByVal Var As String, Optional ByRef StartLineNo As Long = 0) As DataTable
        Dim DtMy As New DataTable
        Dim Item As DictionaryEntry
        REM Add Column into datatable according to Parameter Hashtable
        For Each Item In ColList
            Dim VarIndex As Integer = Val(Item.Value)
            Dim VarType As String = Item.Value.ToString.Substring(VarIndex.ToString.Length)
            Select Case UCase(VarType.Trim)
                Case ""
                    DtMy.Columns.Add(Item.Key)
                Case "STRING"
                    DtMy.Columns.Add(Item.Key, GetType(String))
                Case "INTEGER"
                    DtMy.Columns.Add(Item.Key, GetType(Integer))
                Case "LONG"
                    DtMy.Columns.Add(Item.Key, GetType(Long))
                Case "DOUBLE"
                    DtMy.Columns.Add(Item.Key, GetType(Double))
                Case "DATE"
                    DtMy.Columns.Add(Item.Key, GetType(Date))
                Case "DATETIME"
                    DtMy.Columns.Add(Item.Key, GetType(DateTime))
                Case Else
                    DtMy.Columns.Add(Item.Key)
            End Select
        Next

        ' ''DtMy.Columns.Add("entryno", GetType(Integer))
        ' ''DtMy.Columns.Add(2, GetType(Integer))
        ' ''DtMy.Columns.Add("instrumentname")               'string
        ' ''DtMy.Columns.Add("company")                      'string
        ' ''DtMy.Columns.Add("mdate", GetType(Date))
        ' ''DtMy.Columns.Add("strikerate")
        ' ''DtMy.Columns.Add("cp")                          'string
        ' ''DtMy.Columns.Add("script")
        ' ''DtMy.Columns.Add(9, GetType(Integer))
        ' ''DtMy.Columns.Add(10, GetType(String))
        ' ''DtMy.Columns.Add(11, GetType(Integer))
        ' ''DtMy.Columns.Add("dealer")                     'integer 
        ' ''DtMy.Columns.Add(13, GetType(Integer))
        ' ''DtMy.Columns.Add("buysell")                    'integer
        ' ''DtMy.Columns.Add("qty", GetType(Integer))
        ' ''DtMy.Columns.Add("rate", GetType(Double))
        ' ''DtMy.Columns.Add(17, GetType(Integer))
        ' ''DtMy.Columns.Add(18, GetType(Integer))
        ' ''DtMy.Columns.Add(19, GetType(Integer))
        ' ''DtMy.Columns.Add(20, GetType(String))
        ' ''DtMy.Columns.Add(21, GetType(String))
        ' ''DtMy.Columns.Add("entrydate", GetType(DateTime))
        ' ''DtMy.Columns.Add(23, GetType(DateTime))
        ' ''DtMy.Columns.Add("orderno", GetType(Long))
        ' ''DtMy.Columns.Add(25, GetType(String))
        ' ''DtMy.Columns.Add(26, GetType(DateTime))

        REM Check File exist from Path
        If File.Exists(FilePath) = False Then
            Throw New ApplicationException("Following file path " & FilePath & " Not Found !!")
            Return DtMy
            Exit Function
        End If
        Dim VarFileName As String = Path.GetFileName(FilePath)
        Dim VarFileCounter As Integer = 0

        File.Copy(FilePath, Application.StartupPath & "\" & VarFileName, True)
        FilePath = Application.StartupPath & "\" & VarFileName
        Dim RStream As New System.IO.StreamReader(FilePath)

        Dim Str As String
        Str = RStream.ReadLine()

        If Str Is Nothing Or Str = "" Then
            RStream.Close()
            File.Delete(FilePath)
            Return DtMy
            Exit Function
        End If
        REM This block check whether trade file format Miss-match or not
        Dim StrData1 As String() = Split(Str, SeparatorChar)
        If Var = "FO" Then
            If IsNumeric(StrData1(ColList("Script"))) = True Then
                MsgBox(FilePath & " Traded File Type Miss-Match !!!", MsgBoxStyle.Critical)
                RStream.Close()
                File.Delete(FilePath)
                Return DtMy
                Exit Function
            End If
        ElseIf Var = "EQ" Then
            If Not IsNumeric(StrData1(ColList("Script"))) = True Then
                MsgBox(FilePath & " Traded File Type Miss-Match !!!", MsgBoxStyle.Critical)
                RStream.Close()
                File.Delete(FilePath)
                Return DtMy
                Exit Function
            End If
        ElseIf Var = "CURRENCY" Then
            If IsNumeric(StrData1(ColList("Script"))) = True Then
                MsgBox(FilePath & " Traded File Type Miss-Match !!!", MsgBoxStyle.Critical)
                RStream.Close()
                File.Delete(FilePath)
                Return DtMy
                Exit Function
            End If
        End If
        REM ENd

        REM Line by line row Added into Datatable
        'Dim StartTime As Date
        'Dim EndTime As Date
        'StartTime = Date.Now
        'StrTime = "StartTime : - " & Date.Now

        'New Code For File Read Alpesh 04/05/2011
        '' ''Dim StrAll As String = RStream.ReadToEnd
        '' ''Dim StrLines As String() = StrAll.Split(New [Char]() {CChar(vbCrLf)})
        '' ''For Each StrLine As String In StrLines
        '' ''    Dim Dr As DataRow = DtMy.NewRow
        '' ''    'StrLine.Replace("", CChar(vbCrLf))
        '' ''    If StrLine.Length > 1 Then
        '' ''        Dim StrData As String() = Split(StrLine, SeparatorChar)
        '' ''        For Each Item In ColList
        '' ''            Dr(Item.Key) = Trim(StrData(Val(Item.Value)))
        '' ''        Next
        '' ''        DtMy.Rows.Add(Dr)
        '' ''        StartLineNo += 1
        '' ''    End If
        '' ''Next

        Do Until Str Is Nothing
            Dim Dr As DataRow = DtMy.NewRow
            If Str <> "" Then
                Dim StrData As String() = Split(Str, SeparatorChar)
                For Each Item In ColList
                    Dr(Item.Key) = Trim(StrData(Val(Item.Value)))
                Next
                'For i As Integer = 0 To StrData.Length - 1
                '    Dr(i) = Trim(StrData(i))
                'Next
                DtMy.Rows.Add(Dr)
                StartLineNo += 1
            End If
            Str = RStream.ReadLine()
        Loop

        'StrTime = StrTime & " || EndTime : -" & Date.Now
        'EndTime = Date.Now
        REM End


        'MsgBox("Import Trade File Time : " & DateDiff(DateInterval.Second, StartTime, EndTime) & " Seconds")

        RStream.Close()
        File.Delete(FilePath)

        Dim TradeDate As Date
        TradeDate = DtMy.Compute("Max(entrydate)", "")
        REM Check Expiry date againest Tradedate
        If TradeDate >= CDate(G_VarExpiryDate) Then
            MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
            Call clsGlobal.Sub_Get_Version_TextFile()
            'Application.Exit()
            End
            Exit Function
        End If

        Return DtMy
    End Function

    Public Function proc_data_FromNeatFOTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "FO"
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("Script", 7)
        Hs.Add("Dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("Qty", "14double")
        Hs.Add("Rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23long")
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try
        Dtr = New DataView(Dtr, "Dealer IN (" & DealerCode & ")", "", DataViewRowState.CurrentRows).ToTable
        If Dtr.Rows.Count = 0 Then
            Exit Function
        End If
        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtFOTrades.Compute("MAX(lActivityTime)", "FileFlag='NEATFOTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtFOTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "NEATFOTEXT", "NEAT-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeFOData.Merge(Dtr)
        DTMargeFOData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromGetsFOTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If

        Dim Hs As New Hashtable
        Dim Var As String = "FO"
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("Script", 7)
        Hs.Add("Dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("Qty", "14double")
        Hs.Add("Rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23long")
        Dim Dtr As New DataTable
        Try
            Dim ti1 As Long = System.Environment.TickCount
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
            'FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Text File Reading(MS) :" & System.Environment.TickCount - ti1)
            Write_ErrorLog3(Now.ToString() & "-" & "Text File Reading(MS) :" & System.Environment.TickCount - ti1)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If

        Dim ti As Long = System.Environment.TickCount
        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtFOTrades.Compute("MAX(lActivityTime)", "FileFlag='GETSFOTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If

        Dtr = Dv.ToTable
        ''remove duplicate trade
        'Dim STime As Date
        'Dim ETime As Date
        'STime = Date.Now
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtFOTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        'ETime = Date.Now

        'MsgBox("Remove Duplicate Time : " & DateDiff(DateInterval.Second, STime, ETime))

        '  FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Record Filteration(MS) :" & System.Environment.TickCount - ti)
        Write_ErrorLog3(Now.ToString() & "-" & "Record Filteration(MS) :" & System.Environment.TickCount - ti)
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If

        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "GETSFOTEXT", "GETS-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeFOData.Merge(Dtr)
        DTMargeFOData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromNSEFOTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If

        Dim Hs As New Hashtable
        Dim Var As String = "FO"
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("Script", 7)
        Hs.Add("Dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("Qty", "14double")
        Hs.Add("Rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23long")
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If

        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtFOTrades.Compute("MAX(lActivityTime)", "FileFlag='NSEFOTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtFOTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "NSEFOTEXT", "NSE-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeFOData.Merge(Dtr)
        DTMargeFOData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromODINFOTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "FO"
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("Script", 7)
        Hs.Add("Dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("Qty", "14double")
        Hs.Add("Rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23long")
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        If Dtr.Rows.Count = 0 Then
            Exit Function
        End If
        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtFOTrades.Compute("MAX(lActivityTime)", "FileFlag='ODINFOTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtFOTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "ODINFOTEXT", "ODIN-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeFOData.Merge(Dtr)
        DTMargeFOData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromNowFOTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "FO"
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("Script", 7)
        Hs.Add("Dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("Qty", "14double")
        Hs.Add("Rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23long")
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If

        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtFOTrades.Compute("MAX(lActivityTime)", "FileFlag='NOWFOTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtFOTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        'Dim status As Boolean
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "NOWFOTEXT", "NOW-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeFOData.Merge(Dtr)
        DTMargeFOData.AcceptChanges()
        Dtr.Dispose()
        Return status
        'Dim status As Boolean
        'Return GSub_ImporttxtFileToTrading(Dtr, "NOWFOTEXT", "NOW-", Var)
    End Function
    Public Function proc_data_FromNotisFOTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "FO"
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("Script", 7)
        Hs.Add("Dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("Qty", "14double")
        Hs.Add("Rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23long")
        Hs.Add("Dealer1", 26)
        Dim Dtr As New DataTable
        'Try
        Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)

        If Dtr.Rows.Count = 0 Then
            Exit Function
        End If

        For Each DRow As DataRow In Dtr.Select("Dealer1<>'0'")
            If Val(DRow("Dealer1").ToString) <> 0 Then
                If DRow("Dealer1").ToString.Length > 12 Then If DRow("Dealer1").ToString.Length > 12 Then DRow("Dealer") = DRow("Dealer1").ToString.Substring(0, 12)
            End If
        Next

        Dtr = New DataView(Dtr, "Dealer IN (" & DealerCode & ")", "entrydate", DataViewRowState.Added).ToTable
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If

        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtFOTrades.Compute("MAX(lActivityTime)", "FileFlag='NOTICEFOTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtFOTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        For Each DRow As DataRow In Dtr.Select("Dealer1<>'0'")
            If Val(DRow("Dealer1").ToString) <> 0 Then
                If DRow("Dealer1").ToString.Length > 12 Then If DRow("Dealer1").ToString.Length > 12 Then DRow("Dealer") = DRow("Dealer1").ToString.Substring(0, 12)
            End If
        Next
        Dtr = New DataView(Dtr, "Dealer IN (" & DealerCode & ")", "entrydate", DataViewRowState.Added).ToTable
        If Dtr.Rows.Count = 0 Then Exit Function
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "NOTICEFOTEXT", "NOTIS-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeFOData.Merge(Dtr)
        DTMargeFOData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function

    Public Function proc_data_FromGETSEqTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "EQ"
        Hs.Add("entryno", "0integer")
        Hs.Add("company", 2)
        Hs.Add("cp", 3)
        Hs.Add("Script", 7) 'temporary field assing
        Hs.Add("Dealer", 8)
        Hs.Add("buysell", 10)
        Hs.Add("Qty", "11double")
        Hs.Add("Rate", "12double")
        Hs.Add("entrydate", "19datetime")
        Hs.Add("orderno", "21long")
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtEQTrades.Compute("MAX(lActivityTime)", "FileFlag='GETSEQTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtEQTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "GETSEQTEXT", "GETS-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeEQData.Merge(Dtr)
        DTMargeEQData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromNSEEqTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "EQ"
        Hs.Add("entryno", "0Double")
        Hs.Add("company", 2)
        Hs.Add("cp", 3)
        Hs.Add("Script", 7) 'temporary field assing
        Hs.Add("Dealer", 8)
        Hs.Add("buysell", "10Double")
        Hs.Add("Qty", "11double")
        Hs.Add("Rate", "12double")
        Hs.Add("entrydate", "19datetime")
        Hs.Add("orderno", 21)

        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtEQTrades.Compute("MAX(lActivityTime)", "FileFlag='NSEEQTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtEQTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "NSEEQTEXT", "NSE-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeEQData.Merge(Dtr)
        DTMargeEQData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromODINEqTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "EQ"
        Hs.Add("entryno", "0integer")
        Hs.Add("company", 2)
        Hs.Add("cp", 3)
        Hs.Add("Script", 7) 'temporary field assing
        Hs.Add("Dealer", 8)
        Hs.Add("buysell", 10)
        Hs.Add("Qty", "11double")
        Hs.Add("Rate", "12double")
        Hs.Add("entrydate", "19datetime")
        Hs.Add("orderno", "21long")
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtEQTrades.Compute("MAX(lActivityTime)", "FileFlag='ODINEQTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtEQTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "ODINEQTEXT", "ODIN-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeEQData.Merge(Dtr)
        DTMargeEQData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromNowEQTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "EQ"
        Hs.Add("entryno", "0integer")
        Hs.Add("company", 2)
        Hs.Add("cp", 3)
        Hs.Add("Script", 7) 'temporary field assing
        Hs.Add("Dealer", 8)
        Hs.Add("buysell", 10)
        Hs.Add("Qty", "11double")
        Hs.Add("Rate", "12double")
        Hs.Add("entrydate", "19datetime")
        Hs.Add("orderno", "21long")
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If

        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtEQTrades.Compute("MAX(lActivityTime)", "FileFlag='NOWEQTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtEQTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "NOWEQTEXT", "NOW-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeEQData.Merge(Dtr)
        DTMargeEQData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromNotisEQTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        tptable = New DataTable
        tptable = objTrad.select_equity
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "EQ"
        Hs.Add("entryno", "0integer")
        Hs.Add("company", 2)
        Hs.Add("cp", 3)
        Hs.Add("Script", 7) 'temporary field assing
        Hs.Add("Dealer", 8)
        Hs.Add("buysell", 10)
        Hs.Add("Qty", "11double")
        Hs.Add("Rate", "12double")
        Hs.Add("entrydate", "19datetime")
        Hs.Add("orderno", "21long")
        Hs.Add("Dealer1", 24)
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox("NOTIS Text File Eq :: " & ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim VarlActivityTime As Long
        VarlActivityTime = Val(tptable.Compute("max(lactivitytime)", "fileflag='NOTICEEQTEXT'").ToString)
        Dim MyLastActivityDate As Date = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
        Dim Dv As New DataView(Dtr, "EntryDate > #" & Format(MyLastActivityDate, "dd-MMM-yyyy hh:mm:ss tt") & "#", "entrydate", DataViewRowState.Added)

        Dtr = Dv.ToTable()

        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If

        For Each DRow As DataRow In Dtr.Select("Dealer1<>'0'")
            If Val(DRow("Dealer1").ToString) <> 0 Then
                If DRow("Dealer1").ToString.Length > 12 Then DRow("Dealer") = DRow("Dealer1").ToString.Substring(0, 12)
            End If
        Next
        Dtr = New DataView(Dtr, "Dealer IN (" & DealerCode & ")", "entrydate", DataViewRowState.Added).ToTable
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If

        Dim fdv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtEQTrades.Compute("MAX(lActivityTime)", "FileFlag='NOTICEEQTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            fdv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = fdv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In fdv.ToTable(True, ColList).Rows
            If GdtEQTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "NOTICEEQTEXT", "NOTICE-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeEQData.Merge(Dtr)
        DTMargeEQData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function

    Public Function proc_data_FromNeatCurrTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "CURRENCY"
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("Script", 7)
        Hs.Add("Dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("Qty", "14double")
        Hs.Add("Rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23long")
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try
        Dtr = New DataView(Dtr, "Dealer IN (" & DealerCode & ")", "", DataViewRowState.CurrentRows).ToTable
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtCurrencyTrades.Compute("MAX(lActivityTime)", "FileFlag='NEATCURRENCYTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtCurrencyTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "NEATCURRENCYTEXT", "NEAT-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeCurrData.Merge(Dtr)
        DTMargeCurrData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromNotisCurrTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean

        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath 'GNeatFoFilePath & "\Trade" & GNeatFoFileCode & Format(Today, VarFileNameFormat) & ".txt"
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "CURRENCY"
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("Script", 7)
        Hs.Add("Dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("Qty", "14double")
        Hs.Add("Rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23long")
        Hs.Add("Dealer1", 26)
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try
        If Dtr.Rows.Count = 0 Then
            Exit Function
        End If

        For Each DRow As DataRow In Dtr.Select("Dealer1<>'0'")
            If Val(DRow("Dealer1").ToString) <> 0 Then
                If DRow("Dealer1").ToString.Length > 12 Then If DRow("Dealer1").ToString.Length > 12 Then DRow("Dealer") = DRow("Dealer1").ToString.Substring(0, 12)
            End If
        Next
        Dtr = New DataView(Dtr, "Dealer IN (" & DealerCode & ")", "entrydate", DataViewRowState.Added).ToTable
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If

        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtCurrencyTrades.Compute("MAX(lActivityTime)", "FileFlag='NOTICECURRENCYTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtCurrencyTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "NOTICECURRENCYTEXT", "NOTIS-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeCurrData.Merge(Dtr)
        DTMargeCurrData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromNowCurrTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath 'GNeatFoFilePath & "\Trade" & GNeatFoFileCode & Format(Today, VarFileNameFormat) & ".txt"
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "CURRENCY"
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("Script", 7)
        Hs.Add("Dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("Qty", "14double")
        Hs.Add("Rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23long")
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If

        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtCurrencyTrades.Compute("MAX(lActivityTime)", "FileFlag='NOWCURRENCYTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtCurrencyTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "NOWCURRENCYTEXT", "NOW-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeCurrData.Merge(Dtr)
        DTMargeCurrData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromODINCurrTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "CURRENCY"
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("Script", 7)
        Hs.Add("Dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("Qty", "14double")
        Hs.Add("Rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23long")
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If

        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtCurrencyTrades.Compute("MAX(lActivityTime)", "FileFlag='ODINCURRENCYTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtCurrencyTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "ODINCURRENCYTEXT", "ODIN-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeCurrData.Merge(Dtr)
        DTMargeCurrData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromGetsCurrTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If

        Dim Hs As New Hashtable
        Dim Var As String = "CURRENCY"
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("Script", 7)
        Hs.Add("Dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("Qty", "14double")
        Hs.Add("Rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23long")
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtCurrencyTrades.Compute("MAX(lActivityTime)", "FileFlag='GETSCURRENCYTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtCurrencyTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "GETSCURRENCYTEXT", "GETS-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeCurrData.Merge(Dtr)
        DTMargeCurrData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function
    Public Function proc_data_FromNSECurrTEXT(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal DTMargeCurrData As DataTable, ByVal ObjTradTMP As trading, ByVal DealerCode As String, ByVal CurrentFilePath As String, ByVal PrevFilePath As String) As Boolean
        Dim VarFilePath As String
        If PrevFilePath = "" Then
            VarFilePath = CurrentFilePath
        Else
            VarFilePath = PrevFilePath
        End If
        Dim Hs As New Hashtable
        Dim Var As String = "CURRENCY"
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("Script", 7)
        Hs.Add("Dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("Qty", "14double")
        Hs.Add("Rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23long")
        Dim Dtr As New DataTable
        Try
            Dtr = GetDTFromTextFile(",", VarFilePath, Hs, Var)
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If

        Dim Dv As New DataView(Dtr)
        If PrevFilePath = "" Then
            Dim VarlActivityTime As Integer
            Dim LastEnterDate As Date
            VarlActivityTime = Val(GdtCurrencyTrades.Compute("MAX(lActivityTime)", "FileFlag='NSECURRENCYTEXT'").ToString)
            LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))
            Dv.RowFilter = "EntryDate >= # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #"
        End If
        Dtr = Dv.ToTable
        ''remove duplicate trade
        Dim ColList() As String = {"entryno", "orderno"}
        For Each DR As DataRow In Dv.ToTable(True, ColList).Rows
            If GdtCurrencyTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                Dtr.Rows.Remove(Dtr.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        If Dtr.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Dim status As Boolean
        status = GSub_ImporttxtFileToTrading(Dtr, "NSECURRENCYTEXT", "NSE-", Var)
        Dtr.Columns("token1").ColumnName = "tokenno"
        Dtr.Columns("qty").ColumnName = "unit"
        DTMargeCurrData.Merge(Dtr)
        DTMargeCurrData.AcceptChanges()
        Dtr.Dispose()
        Return status
    End Function

    Public Function proc_data_FromGETSdb(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal ObjTradTMP As trading, Optional ByVal VarServerName As String = "", Optional ByVal VarDatabaseName As String = "", Optional ByVal VarUserName As String = "", Optional ByVal VarPassword As String = "", Optional ByVal VarTableName As String = "") As Boolean
        Dim VarConnectionString As String = "Data Source=" & VarServerName & ";Initial Catalog=" & VarDatabaseName & ";User ID=" & VarUserName & ";Password=" & VarPassword & ";Application Name=" & "VH_" & VarServerName & "_GETSDB"
        DAL.data_access_sql.Connection_string = VarConnectionString
        Try
            DAL.data_access_sql.open_connection()
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(" GETS Database connection Problem !!!", MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        REM 1: To get the last activity time.
        'If Not Gtbl_TradeLog Is Nothing Then

        Dim EQTime As Long = Val(GdtEQTrades.Compute("max(lActivityTime)", "FileFlag='GETSdb'").ToString)
        Dim FOTime As Long = Val(GdtFOTrades.Compute("max(lActivityTime)", "FileFlag='GETSdb'").ToString)
        lActivityTime = IIf(EQTime > FOTime, EQTime, FOTime)

        REM 1: END
        'Try
        'If Not lActivityTime = 0 Then 'If Update Then
        Dim Gtbl_Update_TradeLog As New DataTable()
        REM To get the new trades form database

        Gtbl_Update_TradeLog = objimport.Selecttradelog(GVar_DealerCode, lActivityTime)
        If Gtbl_Update_TradeLog Is Nothing Then Exit Function
        Dim DtTmpTradeLog As DataTable = New DataView(Gtbl_Update_TradeLog, "lActivityTime=" & lActivityTime & "", "", DataViewRowState.CurrentRows).ToTable
        'Dim DtTmpTradeLog As DataTable = New DataView(Gtbl_Update_TradeLog, "lActivityTime=" & lActivityTime & "", "", DataViewRowState.CurrentRows).ToTable
        For Each Dr As DataRow In DtTmpTradeLog.Rows
            If GdtFOTrades.Select("entryno=" & Dr("entryno") & " AND OrderNo='" & Dr("OrderNo").ToString & "'").Length > 0 Then
                Gtbl_Update_TradeLog.Rows.Remove(Gtbl_Update_TradeLog.Select("entryno=" & Dr("entryno") & " AND OrderNo='" & Dr("OrderNo") & "'")(0))
            ElseIf GdtEQTrades.Select("entryno=" & Dr("entryno") & " AND OrderNo='" & Dr("OrderNo").ToString & "'").Length > 0 Then
                Gtbl_Update_TradeLog.Rows.Remove(Gtbl_Update_TradeLog.Select("entryno=" & Dr("entryno") & " AND OrderNo='" & Dr("OrderNo") & "'")(0))
            Else
                If hashOrder.Contains(Dr("orderno").ToString.Trim & "-" & Dr("entryno").ToString.Trim) = False Then
                    hashOrder.Add(Dr("orderno").ToString.Trim & "-" & Dr("entryno").ToString.Trim, "1")
                Else
                    Gtbl_Update_TradeLog.Rows.Remove(Gtbl_Update_TradeLog.Select("entryno=" & Dr("entryno") & " AND OrderNo='" & Dr("OrderNo") & "'")(0))
                End If
            End If
        Next
        DtTmpTradeLog.Dispose()

        Dim DTFOTrading As DataTable
        Dim DTEQTrading As DataTable

        DTFOTrading = New DataTable
        REM 1: create datatable for future and option
        With DTFOTrading.Columns
            .Add("tokenno", GetType(Long))
            .Add("entryno", GetType(Integer))
            .Add("instrumentname")
            .Add("company")
            .Add("mdate", GetType(Date))
            .Add("strikerate", GetType(String))
            .Add("cp")
            .Add("buysell")
            .Add("qty", GetType(Double))
            .Add("rate", GetType(Double))
            .Add("netvalue", GetType(Double))
            .Add("entrydate", GetType(Date))
            .Add("current", GetType(String))
            .Add("script")
            .Add("token1", GetType(Long))
            .Add("isliq")
            .Add("orderno", GetType(Long))
            .Add("lactivitytime", GetType(Long))

            .Add("fileflag", GetType(String))
        End With
        REM 1: end
        DTEQTrading = New DataTable
        REM 2: create datatable for equity
        With DTEQTrading.Columns
            .Add("tokenno", GetType(Long))
            .Add("script")
            .Add("company")
            .Add("eq")
            .Add("buysell")
            .Add("qty", GetType(Double))
            .Add("rate", GetType(Double))
            .Add("netvalue", GetType(Double))
            .Add("entrydate", GetType(Date))
            .Add("current", GetType(String))
            .Add("entryno", GetType(Integer))
            .Add("orderno", GetType(Long))
            .Add("lactivitytime", GetType(Long))
            .Add("fileflag", GetType(String))
        End With
        REM 2: end
        For Each DR As DataRow In Gtbl_Update_TradeLog.Rows
            Dim Dr1 As DataRow()
            Dr1 = cpfmaster.Select("Token=" & DR("Tokanno"))
            If Dr1.Length <> 0 Then
                If Dr1.Length > 0 Then
                    Dim DrTr As DataRow = DTFOTrading.NewRow
                    DrTr("instrumentname") = Dr1(0).Item("instrumentname")
                    DrTr("company") = Dr1(0)("Symbol")
                    DrTr("mdate") = CDate(Format(DateAdd(DateInterval.Second, Val(Dr1(0).Item("expiry_date")), CDate("1-1-1980")), "dd-MMM-yyyy"))
                    DrTr("strikerate") = Val(Dr1(0).Item("strike_price").ToString)
                    DrTr("cp") = IIf(Dr1(0).Item("option_type") = "XX", "F", Mid(Dr1(0).Item("option_type"), 1, 1))
                    DrTr("Script") = Dr1(0).Item("Script").ToString.ToUpper
                    DrTr("TokenNo") = DR("Tokanno")
                    DrTr("BuySell") = DR("BuySell")
                    If CInt(DR("BuySell")) = 1 Then
                        DrTr("Qty") = Math.Abs(DR("Unit"))
                    Else
                        DrTr("Qty") = -Math.Abs(DR("Unit"))
                    End If
                    DrTr("Rate") = DR("Rate")
                    DrTr("netvalue") = DrTr("Qty") * DR("Rate")

                    DrTr("entrydate") = DateAdd(DateInterval.Second, Val(DR("lActivityTime")), CDate("1-1-1980"))
                    If Format(DrTr("entrydate"), "dd-MMM-yyyy") = Format(Today, "dd-MMM-yyyy") Then
                        DrTr("Current") = "T"
                    Else
                        DrTr("Current") = "P"
                    End If
                    DrTr("entryno") = Val(DR("entryno").ToString)
                    If DrTr("cp") = "F" Then
                        DrTr("Token1") = 0
                    Else
                        DrTr("Token1") = DR("Tokanno")
                    End If
                    DrTr("IsLiq") = False
                    DrTr("OrderNo") = Val(DR("OrderNo").ToString)
                    DrTr("lActivityTime") = Val(DR("lActivityTime"))
                    DrTr("FileFlag") = "GETSdb"
                    DTFOTrading.Rows.Add(DrTr)
                End If
            Else
                Dr1 = eqmaster.Select("Token=" & DR("Tokanno"))
                If Dr1.Length > 0 Then
                    Dim DrEq As DataRow = DTEQTrading.NewRow
                    DrEq("Script") = Dr1(0).Item("Script").ToString.ToUpper
                    DrEq("company") = Dr1(0).Item("Symbol")
                    DrEq("Eq") = Dr1(0).Item("Series")
                    DrEq("TokenNo") = DR("Tokanno")
                    DrEq("BuySell") = DR("BuySell")
                    If CInt(DR("BuySell")) = 1 Then
                        DrEq("Qty") = DR("Unit")
                    Else
                        DrEq("Qty") = -DR("Unit")
                    End If
                    DrEq("Rate") = DR("Rate")
                    DrEq("netvalue") = DrEq("Qty") * DR("Rate")
                    DrEq("entrydate") = DateAdd(DateInterval.Second, Val(DR("lActivityTime")), CDate("1-1-1980"))
                    If Format(DrEq("entrydate"), "dd-MMM-yyyy") = Format(Today, "dd-MMM-yyyy") Then
                        DrEq("Current") = "T"
                    Else
                        DrEq("Current") = "P"
                    End If
                    DrEq("entryno") = Val(DR("entryno").ToString)
                    DrEq("OrderNo") = Val(DR("orderno").ToString)
                    DrEq("lActivityTime") = Val(DR("lActivityTime"))

                    DrEq("FileFlag") = "GETSdb"
                    DTEQTrading.Rows.Add(DrEq)
                End If
            End If
        Next

        If DTFOTrading.Rows.Count > 0 Then
            objTrad.Insert_FOTrading(DTFOTrading)
            Call insert_FOTradeToGlobalTable(DTFOTrading)
            DTFOTrading.Columns("netvalue").ColumnName = "tot"
            objAna.process_data_FO(DTFOTrading)
        End If
        If DTEQTrading.Rows.Count > 0 Then
            objTrad.insert_EQTrading(DTEQTrading)
            Call insert_EQTradeToGlobalTable(DTEQTrading)
            DTEQTrading.Columns("netvalue").ColumnName = "tot"
            objAna.process_data_EQ(DTEQTrading)
        End If
        If DTFOTrading.Rows.Count > 0 Or DTEQTrading.Rows.Count > 0 Then
            VarImportInserted = True
        End If
        DTMargeFOData.Merge(DTFOTrading)
        DTMargeFOData.AcceptChanges()

        DTMargeEQData.Merge(DTEQTrading)
        DTMargeEQData.AcceptChanges()
        REM 6: END
        Return True
    End Function
    Public Function proc_data_FromODINdb(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal ObjTradTMP As trading, Optional ByVal VarServerName As String = "", Optional ByVal VarDatabaseName As String = "", Optional ByVal VarUserName As String = "", Optional ByVal VarPassword As String = "", Optional ByVal VarTableName As String = "") As Boolean
        Dim VarConnectionString As String = "Data Source=" & VarServerName & ";Initial Catalog=" & VarDatabaseName & ";User ID=" & VarUserName & ";Password=" & VarPassword & ";Application Name=" & "VH_" & VarServerName & "_ODINDB"
        Dim DTTrade As New DataTable
        Dim DTTrading As New DataTable
        Dim VarlActivityTime As Long
        Dim EQTime As Long = Val(objTrad.select_equity.Compute("max(lActivityTime)", "FileFlag='ODINdb'").ToString)
        Dim FOTime As Long = Val(GdtFOTrades.Compute("max(lActivityTime)", "FileFlag='ODINdb'").ToString)
        VarlActivityTime = IIf(EQTime > FOTime, EQTime, FOTime)
        REM 1: create datatable for future and option
        With DTTrade.Columns
            .Add("TokenNo", GetType(Long))
            .Add("entryno", GetType(Integer))
            .Add("instrumentname")
            .Add("company")
            .Add("mdate", GetType(Date))
            .Add("strikerate", GetType(String))
            .Add("cp")
            .Add("BuySell")
            .Add("qty", GetType(Double))
            .Add("rate", GetType(Double))
            .Add("netvalue", GetType(Double))
            .Add("entrydate", GetType(Date))
            .Add("script")
            .Add("token1", GetType(Long))
            .Add("isliq")
            .Add("orderno", GetType(Long))
            .Add("lActivityTime", GetType(Long))
            .Add("FileFlag", GetType(String))
        End With
        REM 1: END
        Dim DTEquity As New DataTable
        REM 2: create datatable for equity
        With DTEquity.Columns
            .Add("TokenNo", GetType(Long))
            .Add("script")
            .Add("company")
            .Add("eq")
            .Add("BuySell")
            .Add("qty", GetType(Double))
            .Add("rate", GetType(Double))
            .Add("netvalue", GetType(Double))
            .Add("entrydate", GetType(Date))
            .Add("entryno", GetType(Integer))
            .Add("orderno", GetType(Long))
            .Add("lActivityTime", GetType(Long))
            .Add("FileFlag", GetType(String))
        End With
        REM 2: END
        Dim DTodinFOTrad As New DataTable
        Dim DTodinEQTrad As New DataTable
        DAL.data_access_sql.Connection_stringODIN = VarConnectionString
        Try
            DAL.data_access_sql.open_connectionODIN()
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(" ODIN Database connection problem!", MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        'Data Processing For Future and Equity
        '  DAL.data_access_sql.Cmd_Text = "SELECT * FROM tbl_DailyTradesMR  WHERE NTradedTime > " & VarlActivityTime & " and NUserID=" & NeatClient
        DAL.data_access_sql.Cmd_Text = "SELECT RTrim(tbl_DealerMaster.SDealerID) AS SDealerID,Cast(nOrderNumber As decimal(38)) AS OrderNo,Trades.* FROM " & VarTableName & " AS Trades" & _
                                        ",(SELECT Distinct Cast(nOrderNumber As decimal(38)) AS nOrderNumber,sDealerCode,NExpiryDate FROM tbl_DailyOrderResponsesMR) AS OrderMR " & _
                                        ",tbl_DealerMaster" & _
                                        " WHERE Cast(OrderMR.nOrderNumber As decimal(38))=Cast(Trades.nTradedOrderNumber As decimal(38))" & _
                                        " AND OrderMR.NExpiryDate=Trades.NExpiryDate" & _
                                        " AND OrderMR.SDealerCode=tbl_DealerMaster.SDealerCode " & _
                                        " AND Trades.NTradedTime >= " & VarlActivityTime & " AND SDealerID IN (" & GVar_DealerCode & ")"
        DTTrading = DAL.data_access_sql.FillList(True)

        If DTTrading Is Nothing Then
            MsgBox(" ODIN Database connection problem!", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        Dim DtTmpTradeLog As DataTable = New DataView(DTTrading, "NTradedTime = " & VarlActivityTime, "", DataViewRowState.CurrentRows).ToTable
        For Each Dr As DataRow In DtTmpTradeLog.Rows
            If GdtFOTrades.Select("entryno=" & Dr("NTradeNumber") & " AND OrderNo='" & Dr("OrderNo").ToString & "'").Length > 0 Then
                DTTrading.Rows.Remove(DTTrading.Select("NTradeNumber=" & Dr("NTradeNumber") & " AND OrderNo=" & Dr("OrderNo") & "")(0))
            ElseIf GdtEQTrades.Select("entryno=" & Dr("NTradeNumber") & " AND OrderNo='" & Dr("OrderNo").ToString & "'").Length > 0 Then
                DTTrading.Rows.Remove(DTTrading.Select("NTradeNumber=" & Dr("NTradeNumber") & " AND OrderNo=" & Dr("OrderNo") & "")(0))
            Else
                If hashOrder.Contains(Dr("orderno").ToString.Trim & "-" & Dr("NTradeNumber").ToString.Trim) = False Then
                    hashOrder.Add(Dr("orderno").ToString.Trim & "-" & Dr("NTradeNumber").ToString.Trim, "1")
                End If
            End If
        Next
        DtTmpTradeLog.Dispose()

        For Each DR As DataRow In DTTrading.Rows

            DR("SOptionType") = DR("SOptionType").ToString.Trim
            Dim Dr1 As DataRow()
            If DR("SOptionType") <> "" Then
                Dr1 = cpfmaster.Select("Token=" & DR("NTokenNumber"))
                If Dr1.Length > 0 Then
                    Dim DrTr As DataRow = DTTrade.NewRow
                    DrTr("instrumentname") = Dr1(0).Item("instrumentname")
                    DrTr("company") = Dr1(0)("Symbol")
                    DrTr("mdate") = DateAdd(DateInterval.Second, Val(Dr1(0).Item("expiry_date")), CDate("1-1-1980"))
                    DrTr("strikerate") = Val(Dr1(0).Item("strike_price").ToString)
                    If Dr1(0).Item("option_type") = "XX" Then
                        Dr1(0)("option_type") = "F"
                    End If
                    DrTr("cp") = Mid(Dr1(0).Item("option_type"), 1, 1)
                    DrTr("Script") = Dr1(0).Item("Script").ToString.ToUpper
                    DrTr("TokenNo") = DR("NTokenNumber")
                    DrTr("BuySell") = DR("nBuyOrSell")
                    If CInt(DR("nBuyOrSell")) = 1 Then
                        DrTr("Qty") = DR("NQuantityTraded")
                    Else
                        DrTr("Qty") = -DR("NQuantityTraded")
                    End If
                    DrTr("Rate") = DR("NTradedPrice") / 100
                    DrTr("netvalue") = DrTr("Qty") * DrTr("Rate")
                    DrTr("entrydate") = DateAdd(DateInterval.Second, Val(DR("NTradedTime")), CDate("1-1-1980"))
                    'changes done by nasima on 22 Oct
                    DrTr("entryno") = Val(DR("NTradeNumber").ToString)
                    If DrTr("cp") = "F" Then
                        DrTr("Token1") = 0
                    Else
                        DrTr("Token1") = DR("NTokenNumber")
                    End If
                    DrTr("IsLiq") = False
                    DrTr("OrderNo") = Val(DR("OrderNo").ToString)
                    DrTr("lActivityTime") = Val(DR("NTradedTime"))
                    DrTr("FileFlag") = "ODINdb"
                    DTTrade.Rows.Add(DrTr)
                End If
            Else
                Dr1 = eqmaster.Select("Token=" & DR("NTokenNumber"))
                If Dr1.Length > 0 Then
                    Dim DrEq As DataRow = DTEquity.NewRow
                    DrEq("Script") = Dr1(0).Item("Script").ToString.ToUpper
                    DrEq("company") = Dr1(0).Item("Symbol")
                    DrEq("Eq") = DR("sSeries")
                    DrEq("TokenNo") = DR("NTokenNumber")
                    DrEq("BuySell") = DR("nBuyOrSell")
                    If CInt(DR("nBuyOrSell")) = 1 Then
                        DrEq("Qty") = DR("NQuantityTraded")
                    Else
                        DrEq("Qty") = -DR("NQuantityTraded")
                    End If
                    DrEq("Rate") = DR("NTradedPrice") / 100
                    DrEq("netvalue") = DrEq("Qty") * DrEq("Rate")
                    DrEq("entrydate") = DateAdd(DateInterval.Second, Val(DR("NTradedTime")), CDate("1-1-1980"))
                    DrEq("entryno") = Val(DR("NTradeNumber").ToString)
                    DrEq("OrderNo") = Val(DR("OrderNo").ToString)
                    DrEq("lActivityTime") = Val(DR("NTradedTime"))
                    DrEq("FileFlag") = "ODINdb"
                    DTEquity.Rows.Add(DrEq)
                End If
            End If
        Next

        If DTTrade.Rows.Count > 0 Then
            objTrad.Insert_FOTrading(DTTrade)
            Call insert_FOTradeToGlobalTable(DTTrade)
            DTTrade.Columns("netvalue").ColumnName = "tot"
            objAna.process_data_FO(DTTrade)
            VarImportInserted = True
        End If
        If DTEquity.Rows.Count > 0 Then
            objTrad.insert_EQTrading(DTEquity)
            Call insert_EQTradeToGlobalTable(DTEquity)
            DTEquity.Columns("netvalue").ColumnName = "tot"
            objAna.process_data_EQ(DTEquity)
            VarImportInserted = True
        End If

        DTMargeFOData.Merge(DTTrade)
        DTMargeFOData.AcceptChanges()

        DTMargeEQData.Merge(DTEquity)
        DTMargeEQData.AcceptChanges()
        Return True
    End Function
    'CHANGE BY PAYAL PATEL cOPY fROM iNTERNETvERSION (VolHedge5002E4H)
    Public Function proc_data_FromNotisEQdb(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal ObjTradTMP As trading, Optional ByVal VarServerName As String = "", Optional ByVal VarDatabaseName As String = "", Optional ByVal VarUserName As String = "", Optional ByVal VarPassword As String = "", Optional ByVal VarTableName As String = "") As Boolean
        Dim VarConnectionString As String = "Data Source=" & VarServerName & ";Initial Catalog=" & VarDatabaseName & ";User ID=" & VarUserName & ";Password=" & VarPassword & ";Application Name=" & "VH_" & VarServerName & "_NOTISDB"
        Dim Hs As New Hashtable
        Dim VarIsEquity As Boolean = True
        Hs.Add("entryno", "0integer")
        Hs.Add("company", 2)
        Hs.Add("cp", 3)
        Hs.Add("script", 7) 'temporary field assing
        Hs.Add("dealer", 8)
        Hs.Add("buysell", 10)
        Hs.Add("qty", "11double")
        Hs.Add("rate", "12double")
        Hs.Add("entrydate", "19datetime")
        Hs.Add("orderno", "21Long")
        Hs.Add("dealer1", 24)
        Dim DTEQTrading As New DataTable
        Dim VarlActivityTime As Integer
        Dim LastEnterDate As Date
        VarlActivityTime = Val(GdtEQTrades.Compute("MAX(lActivityTime)", "FileFlag='NOTICEEQTEXT'").ToString)
        LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))

        DAL.data_access_sql.Connection_string = VarConnectionString
        Try
            DAL.data_access_sql.open_connection()
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(" NOTIS EQ Database connection problem!", MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        Dim StrColList As String = ""
        For Each Item As DictionaryEntry In Hs
            If StrColList <> "" Then StrColList &= ","
            If Item.Key = "company" Or Item.Key = "dealer1" Or Item.Key = "eq" Then
                StrColList &= "rtrim(ltrim(t" & Val(Item.Value) + 1 & ")) AS " & Item.Key
            ElseIf Item.Key = "dealer" Then
                StrColList &= "CASE WHEN (RTRIM(t" & Val(Hs("dealer1")) + 1 & ")='0') THEN RTRIM(t" & Val(Hs("dealer")) + 1 & ") ELSE RTRIM(t" & Val(Hs("dealer")) + 1 & ")+CAST(RTRIM(t" & Val(Hs("dealer1")) + 1 & ") As Varchar(12)) END AS dealer"
            ElseIf Item.Key = "orderno" Then
                'StrColList &= "cast(t" & Val(Item.Value) + 1 & " As BigInt) AS " & Item.Key
                StrColList &= "t" & Val(Item.Value) + 1 & " AS " & Item.Key
            ElseIf Item.Key = "entryno" Then
                StrColList &= "cast(t" & Val(Item.Value) + 1 & " As float) AS " & Item.Key
            ElseIf Item.Key = "buysell" Then
                StrColList &= "cast(t" & Val(Item.Value) + 1 & " As float) AS " & Item.Key
            Else
                StrColList &= "t" & Val(Item.Value) + 1 & " AS " & Item.Key
            End If
        Next
        Dim VarStrQuery As String = "SELECT " & StrColList & " FROM " & VarTableName & " WHERE t" & Val(Hs("entrydate")) + 1 & " >= '" & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & "'"
        VarStrQuery &= " AND CASE WHEN (RTRIM(t" & Val(Hs("dealer1")) + 1 & ")='0') THEN RTRIM(t" & Val(Hs("dealer")) + 1 & ") ELSE RTRIM(t" & Val(Hs("dealer")) + 1 & ")+CAST(RTRIM(t" & Val(Hs("dealer1")) + 1 & ") As Varchar(12)) END IN (" & GVar_DealerCode & ")"
        VarStrQuery &= " And t15  in (" & GVar_DealerCode & ") "

        VarStrQuery &= " ORDER BY t" & Val(Hs("entrydate")) + 1
        DAL.data_access_sql.Cmd_Text = VarStrQuery
        DTEQTrading = DAL.data_access_sql.FillList(ISTimer)

        If DTEQTrading Is Nothing OrElse DTEQTrading.Rows.Count = 0 Then
            Exit Function
        End If
        Dim Dv As New DataView(DTEQTrading, "EntryDate = # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #", "", DataViewRowState.CurrentRows)
        For Each DR As DataRow In Dv.ToTable.Rows
            If GdtEQTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                DTEQTrading.Rows.Remove(DTEQTrading.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        DTEQTrading.AcceptChanges()
        Dv.Dispose()
        If DTEQTrading.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Call GSub_ImporttxtFileToTrading(DTEQTrading, "NOTICEEQTEXT", "NOTICE-", "EQ")
        DTEQTrading.Columns("token1").ColumnName = "tokenno"
        DTEQTrading.Columns("qty").ColumnName = "unit"

        DTEQTrading.AcceptChanges()
        'Dim col As Integer
        'DTMargeEQData()
        Try
            DTMargeEQData.Merge(DTEQTrading) ', True, MissingSchemaAction.Ignore
        Catch ex As Exception
            MsgBox("Error" & ex.ToString)
        End Try

        DTEQTrading.Dispose()
        Return True
    End Function
    'CHANGE BY PAYAL PATEL cOPY fROM iNTERNETvERSION (VolHedge5002E4H)
    Public Function proc_data_FromNotisFOdb(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal ObjTradTMP As trading, Optional ByVal VarServerName As String = "", Optional ByVal VarDatabaseName As String = "", Optional ByVal VarUserName As String = "", Optional ByVal VarPassword As String = "", Optional ByVal VarTableName As String = "") As Boolean
        Dim VarConnectionString As String = "Data Source=" & VarServerName & ";Initial Catalog=" & VarDatabaseName & ";User ID=" & VarUserName & ";Password=" & VarPassword & ";Application Name=" & "VH_" & VarServerName & "_NOTISDB"

        Dim Hs As New Hashtable
        Dim VarIsEquity As Boolean = False
        Hs.Add("entryno", "0integer")
        Hs.Add("instrumentname", 2)
        Hs.Add("company", 3)
        Hs.Add("mdate", "4date")
        Hs.Add("strikerate", 5)
        Hs.Add("cp", 6)
        Hs.Add("script", 7)
        Hs.Add("dealer", 11)
        Hs.Add("buysell", 13)
        Hs.Add("qty", "14double")
        Hs.Add("rate", "15double")
        Hs.Add("entrydate", "21datetime")
        Hs.Add("orderno", "23Long")
        Hs.Add("dealer1", 26)
        Dim DTFOTrading As New DataTable

        Dim VarlActivityTime As Integer
        Dim LastEnterDate As Date
        VarlActivityTime = Val(GdtFOTrades.Compute("MAX(lActivityTime)", "FileFlag='NOTICEFOTEXT'").ToString)
        LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))

        DAL.data_access_sql.Connection_string = VarConnectionString
        Try
            DAL.data_access_sql.open_connection()
        Catch ex As Exception
            If ISTimer = False Then
                MsgBox(" NOTIS FO Database connection problem!", MsgBoxStyle.Exclamation)
            End If
            Exit Function
        End Try

        Dim StrColList As String = ""
        For Each Item As DictionaryEntry In Hs
            If StrColList <> "" Then StrColList &= ","
            If Item.Key = "company" Or Item.Key = "instrumentname" Or Item.Key = "dealer1" Or Item.Key = "strikerate" Then
                StrColList &= "rtrim(ltrim(t" & Val(Item.Value) + 1 & ")) AS " & Item.Key
            ElseIf Item.Key = "dealer" Then
                'StrColList &= "rtrim(ltrim(t" & Val(Item.Value) + 1 & ")) AS " & Item.Key
                StrColList &= "CASE WHEN (RTRIM(t" & Val(Hs("dealer1")) + 1 & ")='0') THEN RTRIM(t" & Val(Hs("dealer")) + 1 & ") ELSE RTRIM(t" & Val(Hs("dealer")) + 1 & ")+CAST(RTRIM(t" & Val(Hs("dealer1")) + 1 & ") As Varchar(12)) END AS dealer"
            ElseIf Item.Key = "orderno" Then
                'StrColList &= "cast(t" & Val(Item.Value) + 1 & " As BigInt) AS " & Item.Key
                StrColList &= "t" & Val(Item.Value) + 1 & " AS " & Item.Key
            ElseIf Item.Key = "entryno" Then
                StrColList &= "cast(t" & Val(Item.Value) + 1 & " As float) AS " & Item.Key
            ElseIf Item.Key = "buysell" Then
                StrColList &= "cast(t" & Val(Item.Value) + 1 & " As float) AS " & Item.Key
            Else
                If Item.Key = "script" Then
                    StrColList &= "Upper(t" & Val(Item.Value) + 1 & ") AS " & Item.Key
                Else
                    StrColList &= "t" & Val(Item.Value) + 1 & " AS " & Item.Key
                End If
            End If
        Next
        Dim VarStrQuery As String = "SELECT " & StrColList & " FROM " & VarTableName & " WHERE t" & Val(Hs("entrydate")) + 1 & " >= '" & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & "'"
        VarStrQuery &= " AND CASE WHEN (RTRIM(t" & Val(Hs("dealer1")) + 1 & ")='0') THEN RTRIM(t" & Val(Hs("dealer")) + 1 & ") ELSE RTRIM(t" & Val(Hs("dealer")) + 1 & ")+CAST(RTRIM(t" & Val(Hs("dealer1")) + 1 & ") AS Varchar(12)) END IN (" & GVar_DealerCode & ") "
        'VarStrQuery &= " And t18  in (" & GVar_DealerCode & ") "

        VarStrQuery &= " ORDER BY t" & Val(Hs("entrydate")) + 1
        DAL.data_access_sql.Cmd_Text = VarStrQuery
        DTFOTrading = DAL.data_access_sql.FillList(ISTimer)
        If DTFOTrading Is Nothing OrElse DTFOTrading.Rows.Count = 0 Then
            Exit Function
        End If
        Dim strFilter As String = ""
        If VarlActivityTime <> 0 Then
            strFilter = "EntryDate = #" & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & "#"
        End If
        Dim Dv As New DataView(DTFOTrading, strFilter, "", DataViewRowState.CurrentRows)
        For Each DR As DataRow In Dv.ToTable.Rows
            If GdtFOTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
                DTFOTrading.Rows.Remove(DTFOTrading.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
            End If
        Next
        Dv.Dispose()
        If DTFOTrading.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        Call GSub_ImporttxtFileToTrading(DTFOTrading, "NOTICEFOTEXT", "NOTICE-", "FO")

        DTFOTrading.Columns("token1").ColumnName = "tokenno"
        DTFOTrading.Columns("qty").ColumnName = "unit"
        DTFOTrading.AcceptChanges()
        DTMargeFOData.Merge(DTFOTrading)
        DTFOTrading.Dispose()
        Return True
    End Function
    'OLD FUNCTION dEALER fILTERATION pROBLEM
    'Public Function proc_data_FromNotisFOdb(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal ObjTradTMP As trading, Optional ByVal VarServerName As String = "", Optional ByVal VarDatabaseName As String = "", Optional ByVal VarUserName As String = "", Optional ByVal VarPassword As String = "", Optional ByVal VarTableName As String = "") As Boolean
    '    Dim VarConnectionString As String = "Data Source=" & VarServerName & ";Initial Catalog=" & VarDatabaseName & ";User ID=" & VarUserName & ";Password=" & VarPassword

    '    Dim Hs As New Hashtable
    '    Dim VarIsEquity As Boolean = False
    '    Hs.Add("entryno", "0integer")
    '    Hs.Add("instrumentname", 2)
    '    Hs.Add("company", 3)
    '    Hs.Add("mdate", "4date")
    '    Hs.Add("strikerate", 5)
    '    Hs.Add("cp", 6)
    '    Hs.Add("script", 7)
    '    Hs.Add("dealer", 11)
    '    Hs.Add("buysell", 13)
    '    Hs.Add("qty", "14double")
    '    Hs.Add("rate", "15double")
    '    Hs.Add("entrydate", "21datetime")
    '    Hs.Add("orderno", "23Long")
    '    Hs.Add("dealer1", 26)
    '    Dim DTFOTrading As New DataTable

    '    Dim VarlActivityTime As Integer
    '    Dim LastEnterDate As Date
    '    VarlActivityTime = Val(GdtFOTrades.Compute("MAX(lActivityTime)", "FileFlag='NOTICEFOTEXT'").ToString)
    '    LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))

    '    DAL.data_access_sql.Connection_string = VarConnectionString
    '    Try
    '        DAL.data_access_sql.open_connection()
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(" NOTIS FO Database connection problem!", MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    Dim StrColList As String = ""
    '    For Each Item As DictionaryEntry In Hs
    '        If StrColList <> "" Then StrColList &= ","
    '        If Item.Key = "company" Or Item.Key = "instrumentname" Or Item.Key = "dealer1" Or Item.Key = "strikerate" Then
    '            StrColList &= "rtrim(ltrim(t" & Val(Item.Value) + 1 & ")) AS " & Item.Key
    '        ElseIf Item.Key = "dealer" Then
    '            'StrColList &= "rtrim(ltrim(t" & Val(Item.Value) + 1 & ")) AS " & Item.Key
    '            StrColList &= "CASE WHEN (RTRIM(t" & Val(Hs("dealer1")) + 1 & ")='0') THEN RTRIM(t" & Val(Hs("dealer")) + 1 & ") ELSE RTRIM(t" & Val(Hs("dealer")) + 1 & ")+CAST(RTRIM(t" & Val(Hs("dealer1")) + 1 & ") As Varchar(12)) END AS dealer"
    '        ElseIf Item.Key = "orderno" Then
    '            StrColList &= "cast(t" & Val(Item.Value) + 1 & " As BigInt) AS " & Item.Key
    '        Else
    '            If Item.Key = "script" Then
    '                StrColList &= "Upper(t" & Val(Item.Value) + 1 & ") AS " & Item.Key
    '            Else
    '                StrColList &= "t" & Val(Item.Value) + 1 & " AS " & Item.Key
    '            End If
    '        End If
    '    Next
    '    Dim VarStrQuery As String = "SELECT " & StrColList & " FROM " & VarTableName & " WHERE t" & Val(Hs("entrydate")) + 1 & " >= '" & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & "'"
    '    VarStrQuery &= " AND CASE WHEN (RTRIM(t" & Val(Hs("dealer1")) + 1 & ")='0') THEN RTRIM(t" & Val(Hs("dealer")) + 1 & ") ELSE RTRIM(t" & Val(Hs("dealer")) + 1 & ")+CAST(RTRIM(t" & Val(Hs("dealer1")) + 1 & ") AS Varchar(12)) END IN (" & GVar_DealerCode & ")"

    '    VarStrQuery &= " ORDER BY t" & Val(Hs("entrydate")) + 1
    '    DAL.data_access_sql.Cmd_Text = VarStrQuery
    '    DTFOTrading = DAL.data_access_sql.FillList(ISTimer)
    '    If DTFOTrading Is Nothing OrElse DTFOTrading.Rows.Count = 0 Then
    '        Exit Function
    '    End If

    '    Dim Dv As New DataView(DTFOTrading, "EntryDate = #" & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & "#", "", DataViewRowState.CurrentRows)
    '    For Each DR As DataRow In Dv.ToTable.Rows
    '        If GdtFOTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
    '            DTFOTrading.Rows.Remove(DTFOTrading.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
    '        End If
    '    Next
    '    Dv.Dispose()
    '    If DTFOTrading.Rows.Count = 0 Then
    '        Return False
    '        Exit Function
    '    End If
    '    Call GSub_ImporttxtFileToTrading(DTFOTrading, "NOTICEFOTEXT", "NOTICE-", "FO")

    '    DTFOTrading.Columns("token1").ColumnName = "tokenno"
    '    DTFOTrading.Columns("qty").ColumnName = "unit"
    '    DTMargeFOData.Merge(DTFOTrading)
    '    DTFOTrading.Dispose()
    '    Return True
    'End Function
    'OLD FUNCTION dEALER fILTERATION pROBLEM
    'Public Function proc_data_FromNotisEQdb(ByVal ISTimer As Boolean, ByVal DTMargeFOData As DataTable, ByVal DTMargeEQData As DataTable, ByVal ObjTradTMP As trading, Optional ByVal VarServerName As String = "", Optional ByVal VarDatabaseName As String = "", Optional ByVal VarUserName As String = "", Optional ByVal VarPassword As String = "", Optional ByVal VarTableName As String = "") As Boolean
    '    Dim VarConnectionString As String = "Data Source=" & VarServerName & ";Initial Catalog=" & VarDatabaseName & ";User ID=" & VarUserName & ";Password=" & VarPassword
    '    Dim Hs As New Hashtable
    '    Dim VarIsEquity As Boolean = True
    '    Hs.Add("entryno", "0integer")
    '    Hs.Add("company", 2)
    '    Hs.Add("cp", 3)
    '    Hs.Add("script", 7) 'temporary field assing
    '    Hs.Add("dealer", 8)
    '    Hs.Add("buysell", 10)
    '    Hs.Add("qty", "11double")
    '    Hs.Add("rate", "12double")
    '    Hs.Add("entrydate", "19datetime")
    '    Hs.Add("orderno", "21Long")
    '    Hs.Add("dealer1", 24)
    '    Dim DTEQTrading As New DataTable
    '    Dim VarlActivityTime As Integer
    '    Dim LastEnterDate As Date
    '    VarlActivityTime = Val(GdtEQTrades.Compute("MAX(lActivityTime)", "FileFlag='NOTICEEQTEXT'").ToString)
    '    LastEnterDate = DateAdd(DateInterval.Second, VarlActivityTime, CDate("1-1-1980"))

    '    DAL.data_access_sql.Connection_string = VarConnectionString
    '    Try
    '        DAL.data_access_sql.open_connection()
    '    Catch ex As Exception
    '        If ISTimer = False Then
    '            MsgBox(" NOTIS EQ Database connection problem!", MsgBoxStyle.Exclamation)
    '        End If
    '        Exit Function
    '    End Try

    '    Dim StrColList As String = ""
    '    For Each Item As DictionaryEntry In Hs
    '        If StrColList <> "" Then StrColList &= ","
    '        If Item.Key = "company" Or Item.Key = "dealer1" Or Item.Key = "eq" Then
    '            StrColList &= "rtrim(ltrim(t" & Val(Item.Value) + 1 & ")) AS " & Item.Key
    '        ElseIf Item.Key = "dealer" Then
    '            StrColList &= "CASE WHEN (RTRIM(t" & Val(Hs("dealer1")) + 1 & ")='0') THEN RTRIM(t" & Val(Hs("dealer")) + 1 & ") ELSE RTRIM(t" & Val(Hs("dealer")) + 1 & ")+CAST(RTRIM(t" & Val(Hs("dealer1")) + 1 & ") As Varchar(12)) END AS dealer"
    '        ElseIf Item.Key = "orderno" Then
    '            StrColList &= "cast(t" & Val(Item.Value) + 1 & " As BigInt) AS " & Item.Key
    '        Else
    '            StrColList &= "t" & Val(Item.Value) + 1 & " AS " & Item.Key
    '        End If
    '    Next
    '    Dim VarStrQuery As String = "SELECT " & StrColList & " FROM " & VarTableName & " WHERE t" & Val(Hs("entrydate")) + 1 & " >= '" & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & "'"
    '    VarStrQuery &= " AND CASE WHEN (RTRIM(t" & Val(Hs("dealer1")) + 1 & ")='0') THEN RTRIM(t" & Val(Hs("dealer")) + 1 & ") ELSE RTRIM(t" & Val(Hs("dealer")) + 1 & ")+CAST(RTRIM(t" & Val(Hs("dealer1")) + 1 & ") As Varchar(12)) END IN (" & GVar_DealerCode & ")"
    '    VarStrQuery &= " ORDER BY t" & Val(Hs("entrydate")) + 1
    '    DAL.data_access_sql.Cmd_Text = VarStrQuery
    '    DTEQTrading = DAL.data_access_sql.FillList(ISTimer)

    '    If DTEQTrading Is Nothing OrElse DTEQTrading.Rows.Count = 0 Then
    '        Exit Function
    '    End If
    '    Dim Dv As New DataView(DTEQTrading, "EntryDate = # " & Format(LastEnterDate, "dd-MMM-yyyy hh:mm:ss tt") & " #", "", DataViewRowState.CurrentRows)
    '    For Each DR As DataRow In Dv.ToTable.Rows
    '        If GdtEQTrades.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "'").Length > 0 Then
    '            DTEQTrading.Rows.Remove(DTEQTrading.Select("entryno=" & DR("entryno") & " AND orderno='" & DR("orderno") & "' ")(0))
    '        End If
    '    Next
    '    Dv.Dispose()
    '    If DTEQTrading.Rows.Count = 0 Then
    '        Return False
    '        Exit Function
    '    End If
    '    Call GSub_ImporttxtFileToTrading(DTEQTrading, "NOTICEEQTEXT", "NOTICE-", "EQ")
    '    DTEQTrading.Columns("token1").ColumnName = "tokenno"
    '    DTEQTrading.Columns("qty").ColumnName = "unit"
    '    DTMargeEQData.Merge(DTEQTrading)
    '    DTEQTrading.Dispose()
    '    Return True
    'End Function

#End Region
#Region "Summary Global Table"
    Public Sub allcompany1()
        Dim hDiscompnay As New Hashtable
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dv As DataView
        Dim dtFilterComp As New DataTable
        dtFilterComp = objTrad.Comapany_summary
        If RefreshsummaryExpirywise = False Then
            dt = objTrad.Comapany_summary
        Else

            Dim dtdtableView As DataView = New DataView(maintable.Copy, "", "Script", DataViewRowState.CurrentRows)
            dt = dtdtableView.ToTable(True, "company", "mdate", "cp")
            dt1 = dt.Copy
            dt.Clear()
            Dim drdt As DataRow
            For Each drow As DataRow In dt1.Rows
                Dim str As String
                If drow("CP") = "E" And drow("mdate") = Date.Today Then
                    str = drow("Company") + drow("mdate") + drow("CP")
                    If hDiscompnay.Contains(str) = False Then
                        drdt = dt.NewRow()
                        drdt("Company") = drow("Company")
                        drdt("mdate") = drow("mdate")
                        drdt("cp") = drow("cp")
                        dt.Rows.Add(drdt)
                        hDiscompnay.Add(str, "")
                    End If
                Else
                    str = drow("Company") + drow("mdate")

                    If hDiscompnay.Contains(str) = False Then
                        drdt = dt.NewRow()
                        drdt("Company") = drow("Company")
                        drdt("mdate") = drow("mdate")
                        drdt("cp") = drow("cp")
                        dt.Rows.Add(drdt)
                        hDiscompnay.Add(str, "")
                    End If
                End If


            Next
        End If
        dv = New DataView(maintable.Copy, "", "company", DataViewRowState.CurrentRows)

        acomp = New DataTable
        acomp = dv.ToTable()
        acomp.Columns.Add("deltaRs", GetType(Double))

        acomp.Columns.Add("lasts1", GetType(Double))
        acomp.Columns.Add("lasts2", GetType(Double))

        acomp.Columns.Add("Scenario1", GetType(Double))
        acomp.Columns.Add("Scenario2", GetType(Double))


        acomp.AcceptChanges()
        Dim row As DataRow
        Gtbl_Summary_Analysis.Rows.Clear()
        row = Gtbl_Summary_Analysis.NewRow
        row("company") = "Total"
        row("mdate") = "Total"
        row("Expiry") = "Total"
        row("Month") = "Total"
        row("CP") = ""
        row("delta") = 0
        row("theta") = 0
        row("gamma") = 0
        row("vega") = 0
        row("volga") = 0
        row("vanna") = 0
        row("deltaRs") = 0
        row("grossmtm") = 0
        row("grossmtm") = 0
        row("Scenario1") = 0
        row("Scenario2") = 0
        row("initMargin") = 0
        row("ExpoMargin") = 0
        row("TotalMargin") = 0
        row("Expense") = 0
        row("NetMTM") = 0
        Dim hcompnay As New Hashtable
        Gtbl_Summary_Analysis.Rows.Add(row)
        Dim dtsort As DataTable
        If RefreshsummaryExpirywise = False Then
            dtsort = New DataView(dt.Copy, "", "Company", DataViewRowState.CurrentRows).ToTable()
        Else
            dtsort = New DataView(dt.Copy, "", "Company,mdate", DataViewRowState.CurrentRows).ToTable()
        End If

        For Each drow As DataRow In dtsort.Rows
            Dim dtrow() As DataRow = dtFilterComp.Select("Company='" + drow("Company") + "'")
            If dtrow.Length = 0 Then
                Continue For
            End If
            Dim trow() As DataRow
            If RefreshsummaryExpirywise = False Then
                trow = acomp.Select("company='" & drow("company") & "'")
            Else
                If drow("Mdate").ToString().Length = 0 Then
                    trow = acomp.Select("company='" & drow("company") & "' and CP='E'")
                Else
                    trow = acomp.Select("company='" & drow("company") & "' and mdate='" & drow("mdate") & "'")
                End If

            End If

            If trow.Length > 0 Then
                If RefreshsummaryExpirywise = True Then
                    '  If drow("cp").ToString() <> "E" Then
                    If hcompnay.Contains(drow("company")) = False Then
                        row = Gtbl_Summary_Analysis.NewRow
                        row("company") = drow("company")

                        row("Month") = drow("company") + "-" + drow("mdate")
                        row("Expiry") = "ALL"
                        row("delta") = 0
                        row("theta") = 0
                        row("gamma") = 0
                        row("vega") = 0
                        row("volga") = 0
                        row("vanna") = 0
                        row("deltaRs") = 0
                        row("grossmtm") = 0
                        row("grossmtm") = 0
                        row("Scenario1") = 0
                        row("Scenario2") = 0

                        row("initMargin") = 0
                        row("ExpoMargin") = 0
                        row("TotalMargin") = 0
                        row("Expense") = 0
                        row("NetMTM") = 0
                        row("CP") = drow("CP")
                        row("IsSelected") = True
                        Gtbl_Summary_Analysis.Rows.Add(row)
                        hcompnay.Add(drow("company"), "")

                    End If
                    'End If
                End If

                row = Gtbl_Summary_Analysis.NewRow
                row("company") = drow("company")
                If RefreshsummaryExpirywise = False Then
                    row("mdate") = drow("company")
                    row("Expiry") = "ALL"
                    row("Month") = "ALL"
                Else

                    If drow("mdate").ToString().Length = 0 Then
                        row("mdate") = drow("mdate")
                        row("Expiry") = "ALL"
                        row("Month") = "ALL"
                    Else
                        row("mdate") = CDate(drow("mdate")).ToString("dd-MMM-yyyy")
                        row("Expiry") = CDate(drow("mdate")).ToString("dd-MMM-yyyy")
                        'row("Expiry") = drow("company") + "-" + drow("mdate")
                    End If

                End If

                If RefreshsummaryExpirywise = True Then
                    row("CP") = drow("CP")
                Else
                    row("CP") = ""
                End If


                row("delta") = 0
                row("theta") = 0
                row("gamma") = 0
                row("vega") = 0
                row("volga") = 0
                row("vanna") = 0
                row("deltaRs") = 0
                row("grossmtm") = 0
                row("grossmtm") = 0
                row("Scenario1") = 0
                row("Scenario2") = 0

                row("initMargin") = 0
                row("ExpoMargin") = 0
                row("TotalMargin") = 0
                row("Expense") = 0
                row("NetMTM") = 0
                If RefreshsummaryExpirywise = False Then

                    row("IsSelected") = True
                End If

                Gtbl_Summary_Analysis.Rows.Add(row)
            End If
        Next
        'If RefreshsummaryExpirywise = True Then
        '    Gtbl_Summary_Analysis = New DataView(Gtbl_Summary_Analysis, "", "company,Expiry", DataViewRowState.CurrentRows).ToTable()
        'Else

        '    Gtbl_Summary_Analysis = New DataView(Gtbl_Summary_Analysis, "", "company", DataViewRowState.CurrentRows).ToTable()
        'End If


        '  Gtbl_Summary_Analysis.AcceptChanges()

        analysis.Cal_DeltaGammaVegaThetaSummary()
    End Sub
    Public Sub allcompany_Summary()
        REM Add Volga,Vanna Greeks in Summary(F9) Button Click show in grid
        'flgcalsummarythrdstart = False
        'Timer_Calculation.Stop()

        Dim hDiscompnay As New Hashtable
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dv As DataView
        Dim dtFilterComp As New DataTable
        dtFilterComp = objTrad.Comapany_summary
        'If RefreshsummaryExpirywise = False Then
        '    dt = objTrad.Comapany_summary
        'Else

        Dim dtdtableView As DataView = New DataView(maintable, "", "Script", DataViewRowState.CurrentRows)
        dt = dtdtableView.ToTable(True, "company", "mdate", "cp")
        dt1 = dt.Copy
        dt.Clear()
        Dim drdt As DataRow
        For Each drow As DataRow In dt1.Rows
            Dim str As String = drow("Company") + drow("mdate")
            If hDiscompnay.Contains(str) = False Then
                drdt = dt.NewRow()
                drdt("Company") = drow("Company")
                drdt("mdate") = drow("mdate")
                drdt("cp") = drow("cp")
                dt.Rows.Add(drdt)
                hDiscompnay.Add(str, "")
            End If

        Next
        '  Dim dtdtableView1 As DataView = New DataView(dt, "", "Month", DataViewRowState.CurrentRows)
        ' dt = dtdtableView.ToTable(True, "company", "Month")
        '  End If

        'For Each drow As DataRow In dt.Rows
        '    If drow("Month").ToString().Length() = 0 Then
        '        drow("Month") = Format(Date.Now, "MMM yy").ToString()
        '    End If
        'Next

        'If zero_analysis <> 0 Then
        'dv = New DataView(maintable, "units <> 0", "company", DataViewRowState.CurrentRows)
        'Else
        dv = New DataView(maintable, "", "company", DataViewRowState.CurrentRows)
        'End If
        acomp = New DataTable
        acomp = dv.ToTable
        acomp.Columns.Add("deltaRs", GetType(Double))

        acomp.Columns.Add("lasts1", GetType(Double))
        acomp.Columns.Add("lasts2", GetType(Double))

        acomp.Columns.Add("Scenario1", GetType(Double))
        acomp.Columns.Add("Scenario2", GetType(Double))


        acomp.AcceptChanges()
        Dim row As DataRow
        Gtbl_Summary_Analysis_ExpiryWise.Rows.Clear()
        row = Gtbl_Summary_Analysis_ExpiryWise.NewRow
        row("company") = "Total"
        row("mdate") = "Total"
        row("Expiry") = "Total"
        row("Month") = "Total"
        row("CP") = ""
        row("delta") = 0
        row("theta") = 0
        row("gamma") = 0
        row("vega") = 0
        row("volga") = 0
        row("vanna") = 0
        row("deltaRs") = 0
        row("grossmtm") = 0
        row("grossmtm") = 0
        row("Scenario1") = 0
        row("Scenario2") = 0
        row("initMargin") = 0
        row("ExpoMargin") = 0
        row("TotalMargin") = 0
        row("Expense") = 0
        row("NetMTM") = 0
        Dim hcompnay As New Hashtable
        Gtbl_Summary_Analysis_ExpiryWise.Rows.Add(row)
        For Each drow As DataRow In dt.Rows
            Dim dtrow() As DataRow = dtFilterComp.Select("Company='" + drow("Company") + "'")
            If dtrow.Length = 0 Then
                Continue For
            End If
            Dim trow() As DataRow
            'If RefreshsummaryExpirywise = False Then
            '    trow = acomp.Select("company='" & drow("company") & "'")
            'Else
            'For Each drow As DataRow In dt.Rows
            '    If drow("Month").ToString().Length() = 0 Then
            '        drow("Month") = Format(Date.Now, "MMM yy").ToString()
            '    End If
            'Next
            If drow("Mdate").ToString().Length = 0 Then
                trow = acomp.Select("company='" & drow("company") & "' and CP='E'")
            Else
                trow = acomp.Select("company='" & drow("company") & "' and mdate='" & drow("mdate") & "'")
            End If

            'End If

            If trow.Length > 0 Then
                If RefreshsummaryExpirywise = True Then
                    If drow("cp").ToString() <> "E" Then
                        If hcompnay.Contains(drow("company")) = False Then
                            row = Gtbl_Summary_Analysis_ExpiryWise.NewRow
                            row("company") = drow("company")
                            'If RefreshsummaryExpirywise = False Then
                            row("mdate") = drow("mdate")
                            row("Expiry") = "ALL"
                            'Else
                            '    row("Month") = drow("Month")
                            'End If

                            row("delta") = 0
                            row("theta") = 0
                            row("gamma") = 0
                            row("vega") = 0
                            row("volga") = 0
                            row("vanna") = 0
                            row("deltaRs") = 0
                            row("grossmtm") = 0
                            row("grossmtm") = 0
                            row("Scenario1") = 0
                            row("Scenario2") = 0

                            row("initMargin") = 0
                            row("ExpoMargin") = 0
                            row("TotalMargin") = 0
                            row("Expense") = 0
                            row("NetMTM") = 0
                            row("CP") = drow("CP")
                            Gtbl_Summary_Analysis_ExpiryWise.Rows.Add(row)
                            hcompnay.Add(drow("company"), "")
                        End If
                    End If
                End If

                row = Gtbl_Summary_Analysis_ExpiryWise.NewRow
                row("company") = drow("company")
                'If RefreshsummaryExpirywise = False Then
                '    row("mdate") = drow("company")
                '    row("Expiry") = "ALL"
                '    row("Month") = "ALL"
                'Else
                If drow("mdate").ToString().Length = 0 Then
                    row("mdate") = drow("mdate")
                    row("Expiry") = "ALL"
                    row("Month") = "ALL"
                Else
                    row("mdate") = CDate(drow("mdate")).ToString("dd-MMM-yyyy")
                    row("Expiry") = drow("company") + "-" + drow("mdate")
                    'row("Expiry") = drow("company") + "-" + drow("mdate")
                End If

                'End If
                ' If RefreshsummaryExpirywise = True Then
                row("CP") = drow("CP")
                'Else
                '    row("CP") = ""
                'End If

                row("delta") = 0
                row("theta") = 0
                row("gamma") = 0
                row("vega") = 0
                row("volga") = 0
                row("vanna") = 0
                row("deltaRs") = 0
                row("grossmtm") = 0
                row("grossmtm") = 0
                row("Scenario1") = 0
                row("Scenario2") = 0

                row("initMargin") = 0
                row("ExpoMargin") = 0
                row("TotalMargin") = 0
                row("Expense") = 0
                row("NetMTM") = 0
                Gtbl_Summary_Analysis_ExpiryWise.Rows.Add(row)
            End If
        Next
        'If RefreshsummaryExpirywise = True Then

        '    Gtbl_Summary_Analysis.Columns("Month").ColumnMapping = MappingType.Hidden
        '    Gtbl_Summary_Analysis.Columns("Expiry").ColumnMapping = MappingType.Hidden
        'Else

        '    Gtbl_Summary_Analysis.Columns("Month").ColumnMapping = MappingType.SimpleContent
        '    Gtbl_Summary_Analysis.Columns("Expiry").ColumnMapping = MappingType.SimpleContent


        'End If
        'Gtbl_Summary_Analysis.Columns("CP").ColumnMapping = MappingType.Hidden

        Gtbl_Summary_Analysis_ExpiryWise.AcceptChanges()

        'If RefreshsummaryExpirywise = True Then
        '    If flgcalsummarythrdstart = False Then
        '        Timer_Calculation.Stop()
        '        'txtVarexpense = txttotexp.Text
        '        'txtVarcurrent = txttotnetmtm.Text
        '        'txtVarProjectMTM = txttotsqmtm.Text
        '        'Thr_CalculateDatatable.Abort()
        '        flgcalsummarythrdstart = False
        '        'Write_Log2("Step2A:analysis_Cal_DeltaGammaVegaThetaSummary  Process Start..")
        '        Thr_CalculateDatatable = New Thread(AddressOf Cal_DeltaGammaVegaThetaSummary)
        '        'Write_Log2("Step2A:analysis_Cal_DeltaGammaVegaThetaSummary  Process Start..")
        '        Thr_CalculateDatatable.Start()
        '        Timer_Calculation.Start()
        '    End If
        'End If
        'RefreshsummaryExpirywise = False
    End Sub
    Public Sub Init_Gtbl_Summary_Analysis_ExpiryWise()
        REM Add Volga,Vanna Greeks in Summary(F9) Button Click show in grid
        Gtbl_Summary_Analysis_ExpiryWise = New DataTable
        With Gtbl_Summary_Analysis_ExpiryWise.Columns
            .Add("company")
            .Add("MDate")
            .Add("Month")
            .Add("Expiry")
            .Add("delta", GetType(Double))
            .Add("gamma", GetType(Double))
            .Add("vega", GetType(Double))
            .Add("theta", GetType(Double))
            .Add("volga", GetType(Double))
            .Add("vanna", GetType(Double))
            .Add("grossMTM", GetType(Double))
            .Add("deltaRS", GetType(Double))

            .Add("Scenario1", GetType(Double))
            .Add("Scenario2", GetType(Double))

            .Add("initMargin", GetType(Double))
            .Add("ExpoMargin", GetType(Double))
            .Add("TotalMargin", GetType(Double))
            .Add("Expense", GetType(Double))
            .Add("NetMTM", GetType(Double))
            .Add("CP", GetType(String))
            '.Add("current", GetType(Double))
            '.Add("ProjMTM", GetType(Double))
        End With
    End Sub

    Public Sub Init_Gtbl_Summary_Analysis()
        REM Add Volga,Vanna Greeks in Summary(F9) Button Click show in grid
        Gtbl_Summary_Analysis = New DataTable
        With Gtbl_Summary_Analysis.Columns
            .Add("company")
            .Add("MDate")
            .Add("Month")
            .Add("Expiry")
            .Add("LTP", GetType(Double))
            .Add("delta", GetType(Double))
            .Add("gamma", GetType(Double))
            .Add("vega", GetType(Double))
            .Add("theta", GetType(Double))
            .Add("volga", GetType(Double))
            .Add("vanna", GetType(Double))
            .Add("grossMTM", GetType(Double))
            .Add("deltaRS", GetType(Double))

            .Add("Scenario1", GetType(Double))
            .Add("Scenario2", GetType(Double))

            .Add("initMargin", GetType(Double))
            .Add("ExpoMargin", GetType(Double))
            .Add("TotalMargin", GetType(Double))
            .Add("Expense", GetType(Double))
            .Add("NetMTM", GetType(Double))
            .Add("CP", GetType(String))
            .Add("IsSelected", GetType(Boolean))
            .Add("Qty", GetType(Double))
            '.Add("current", GetType(Double))
            '.Add("ProjMTM", GetType(Double))
        End With
    End Sub
    'Public Sub Init_Gtbl_Summary_Analysis()
    '    REM Add Volga,Vanna Greeks in Summary(F9) Button Click show in grid
    '    Gtbl_Summary_Analysis = New DataTable
    '    With Gtbl_Summary_Analysis.Columns
    '        .Add("company")
    '        .Add("Mdate")
    '        .Add("delta", GetType(Double))
    '        .Add("gamma", GetType(Double))
    '        .Add("vega", GetType(Double))
    '        .Add("theta", GetType(Double))
    '        .Add("volga", GetType(Double))
    '        .Add("vanna", GetType(Double))
    '        .Add("grossMTM", GetType(Double))
    '        .Add("deltaRS", GetType(Double))

    '        .Add("Scenario1", GetType(Double))
    '        .Add("Scenario2", GetType(Double))

    '        .Add("initMargin", GetType(Double))
    '        .Add("ExpoMargin", GetType(Double))
    '        .Add("TotalMargin", GetType(Double))
    '        .Add("Expense", GetType(Double))
    '        .Add("NetMTM", GetType(Double))
    '        .Add("CP", GetType(String))
    '        '.Add("current", GetType(Double))
    '        '.Add("ProjMTM", GetType(Double))
    '    End With
    'End Sub
#End Region

#Region " Expense Calculation"
    Public G_DTExpenseData As DataTable
    ''' <summary>
    ''' Init_G_DTExpenseData
    ''' </summary>
    ''' <remarks>This method call to init. G_DTExpenseData datatable</remarks>
    Public Sub Init_G_DTExpenseData()
        G_DTExpenseData = New DataTable
        With G_DTExpenseData.Columns
            .Add("entry_date", GetType(Date))
            .Add("company", GetType(String))
            .Add("script", GetType(String))
            .Add("exp_date", GetType(Date))
            .Add("expense", GetType(Double))
        End With
        G_DTExpenseData.DefaultView.Sort = "company,exp_date,entry_date"
    End Sub
    Public G_DTExpenseData_Importdata As DataTable
    ''' <summary>
    ''' Init_G_DTExpenseData
    ''' </summary>
    ''' <remarks>This method call to init. G_DTExpenseData datatable</remarks>
    Public Sub Init_G_DTExpenseDataa_Importdata()
        G_DTExpenseData_Importdata = New DataTable
        With G_DTExpenseData_Importdata.Columns
            .Add("entry_date", GetType(Date))
            .Add("company", GetType(String))
            .Add("script", GetType(String))
            .Add("exp_date", GetType(Date))
            .Add("expense", GetType(Double))
        End With
        G_DTExpenseData_Importdata.DefaultView.Sort = "company,exp_date,entry_date"
    End Sub

    Public Sub GSub_CalculateExpense_ImportData(ByVal DtTrad As DataTable, ByVal VarTradeType As String, ByVal VarIsAddRow As Boolean)
        Try


            Dim dtbuy As DataTable
            Dim dtSale As DataTable
            Dim dtbuyEQ As DataTable
            Dim dtSaleEQ As DataTable
            Dim dtbuyCurr As DataTable
            Dim dtSaleCurr As DataTable
            If VarTradeType = "FO" Then
                dtbuy = New DataView(DtTrad, "qty > 0", "", DataViewRowState.CurrentRows).ToTable
                dtSale = New DataView(DtTrad, "qty < 0", "", DataViewRowState.CurrentRows).ToTable
            ElseIf VarTradeType = "EQ" Then
                dtbuyEQ = New DataView(DtTrad, "qty > 0", "", DataViewRowState.CurrentRows).ToTable
                dtSaleEQ = New DataView(DtTrad, "qty < 0", "", DataViewRowState.CurrentRows).ToTable
            ElseIf VarTradeType = "CURR" Then
                dtbuyCurr = New DataView(DtTrad, "qty > 0", "", DataViewRowState.CurrentRows).ToTable
                dtSaleCurr = New DataView(DtTrad, "qty < 0", "", DataViewRowState.CurrentRows).ToTable
            End If


            Dim VarExpenseAmount As Double = 0
            If DtTrad.Rows.Count <= 0 Then Exit Sub

            If VarTradeType = "FO" Then REM F&O trade expense calc. and store in G_DTExpenseData datatable
                For Each Dr As DataRow In DtTrad.DefaultView.ToTable(True, "entrydate", "Security", "script", "Exp# Date", "cpF").Rows
                    'If VarIsAddRow = False Then
                    '    If GdtFOTrades.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length = 0 Then
                    '        If G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length > 0 Then
                    '            G_DTExpenseData.Rows.Remove(G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'")(0))
                    '        End If
                    '        Continue For
                    '    End If
                    'End If
                    Dim VarBuyValue As Double = 0
                    Dim VarSaleValue As Double = 0


                    'VarBuyValue = Val(GdtFOTrades.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entry_date = #" & fdate(Dr("entry_date")) & "#").ToString)
                    Try

                        If Dr("cpf") = "XX" Or Dr("cpf") = "F" Or Dr("cpf") = "X" Or Dr("cpf") = "" Then
                            VarBuyValue = Math.Abs(Val(dtbuy.Compute("SUM(tot)", " script='" & Dr("script") & "'  and entrydate = #" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                            VarSaleValue = Math.Abs(Val(dtSale.Compute("sum(tot)", "script='" & Dr("script") & "'  and entrydate = #" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                            'If Dr("cp") = "XX" Or Dr("cp") = "F" Then
                            VarExpenseAmount = Val((VarBuyValue * futl) / futlp) + Val((VarSaleValue * futs) / futsp)
                        Else
                            If Val(spl) <> 0 Then
                                VarBuyValue = Math.Abs(Val(dtbuy.Compute("SUM(tot2)", " script='" & Dr("script") & "'  and entrydate = #" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                                VarSaleValue = Math.Abs(Val(dtSale.Compute("sum(tot2)", "script='" & Dr("script") & "'  and entrydate = #" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                                VarExpenseAmount = Val((VarBuyValue * spl) / splp) + Val((VarSaleValue * sps) / spsp)
                            Else
                                VarBuyValue = Math.Abs(Val(dtbuy.Compute("SUM(tot)", " script='" & Dr("script") & "'  and entrydate = #" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                                VarSaleValue = Math.Abs(Val(dtSale.Compute("sum(tot)", "script='" & Dr("script") & "'  and entrydate = #" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                                VarExpenseAmount = Val((VarBuyValue * prel) / prelp) + Val((VarSaleValue * pres) / presp)
                            End If
                        End If
                        Call AddUpdateExpenseRow_ImportData(Dr("entrydate"), Dr("Security"), Dr("script"), Dr("Exp# Date"), VarExpenseAmount)

                    Catch ex As Exception

                    End Try
                Next


            ElseIf VarTradeType = "CURR" Then REM Currency trade expense calc. and store in G_DTExpenseData datatable
                For Each Dr As DataRow In DtTrad.DefaultView.ToTable(True, "entrydate", "Security", "script", "Exp# Date", "cpF").Rows
                    'If VarIsAddRow = False Then
                    '    If GdtCurrencyTrades.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length = 0 Then
                    '        If G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length > 0 Then
                    '            G_DTExpenseData.Rows.Remove(G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'")(0))
                    '        End If
                    '        Continue For
                    '    End If
                    'End If
                    Dim VarBuyValue As Double = 0
                    Dim VarSaleValue As Double = 0
                    Dim Tmp As Double = 0
                    Tmp = Val(DtTrad.Compute("SUM(tot)", "").ToString)
                    'VarBuyValue = Val(GdtCurrencyTrades.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entry_date=#" & Dr("entry_date") & "#").ToString)
                    VarBuyValue = Math.Abs(Val(dtbuyCurr.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entrydate=#" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                    VarSaleValue = Math.Abs(Val(DtTrad.Compute("sum(tot)", "script='" & Dr("script") & "' and qty < 0 and entrydate=#" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                    REM For Currency - IIf(UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "X", "F", UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1))) 
                    If Dr("cpf") = "XX" Or Dr("cpf") = "F" Or Dr("cpf") = "X" Or Dr("cpf") = "" Then
                        VarBuyValue = Math.Abs(Val(dtbuyCurr.Compute("SUM(tot)", " script='" & Dr("script") & "'  and entrydate=#" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                        VarSaleValue = Math.Abs(Val(dtSaleCurr.Compute("sum(tot)", "script='" & Dr("script") & "'  and entrydate=#" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                        VarExpenseAmount = Val((VarBuyValue * currfutl) / currfutlp) + Val((VarSaleValue * currfuts) / currfutsp)
                    Else
                        If Val(currspl) <> 0 Then
                            VarBuyValue = Math.Abs(Val(dtbuyCurr.Compute("SUM(tot2)", " script='" & Dr("script") & "'  and entrydate=#" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                            VarSaleValue = Math.Abs(Val(dtSaleCurr.Compute("sum(tot2)", "script='" & Dr("script") & "'  and entrydate=#" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                            VarExpenseAmount = Val((VarBuyValue * currspl) / currsplp) + Val((VarSaleValue * currsps) / currspsp)
                        Else
                            VarBuyValue = Math.Abs(Val(dtbuyCurr.Compute("SUM(tot)", " script='" & Dr("script") & "'  and entrydate=#" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                            VarSaleValue = Math.Abs(Val(dtSaleCurr.Compute("sum(tot)", "script='" & Dr("script") & "' and entrydate=#" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString))
                            VarExpenseAmount = Val((VarBuyValue * currprel) / currprelp) + Val((VarSaleValue * currpres) / currpresp)
                        End If
                    End If
                    Call AddUpdateExpenseRow_ImportData(Dr("entrydate"), Dr("Security"), Dr("script"), Dr("Exp# Date"), VarExpenseAmount)
                Next


            ElseIf VarTradeType = "EQ" Then REM EQ trade expense calc. and store in G_DTExpenseData datatable
                For Each Dr As DataRow In DtTrad.DefaultView.ToTable(True, "entrydate", "Security", "script").Rows
                    'If VarIsAddRow = False Then
                    '    If GdtEQTrades.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length = 0 Then
                    '        If G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length > 0 Then
                    '            G_DTExpenseData.Rows.Remove(G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'")(0))
                    '        End If
                    '        Continue For
                    '    End If
                    'End If
                    Dim VarBuyValue As Double = 0
                    Dim VarSaleValue As Double = 0
                    Dim VarDiffValue As Double = 0
                    'VarBuyValue = Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "script='" & Dr("script") & "' and qty > 0 and entry_date = #" & Dr("entry_date") & "#").ToString), 2)
                    VarBuyValue = Math.Abs(Math.Round(Val(dtbuyEQ.Compute("sum(tot)", "script='" & Dr("script") & "'  and entrydate = #" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString), 2))
                    VarSaleValue = Math.Abs(Math.Round(Val(dtSaleEQ.Compute("sum(tot)", "script='" & Dr("script") & "'  and entrydate = #" & fDate(Dr("entrydate")) & "# And Security='" & Dr("Security") & "'").ToString), 2))
                    VarDiffValue = VarBuyValue - VarSaleValue
                    If VarDiffValue > 0 Then
                        VarExpenseAmount = Val((VarDiffValue * dbl) / dblp) + Val((VarSaleValue * ndbs) / ndbsp) + Val((VarSaleValue * ndbl) / ndblp)
                    Else
                        VarDiffValue = -VarDiffValue
                        VarExpenseAmount = Val((VarDiffValue * dbs) / dbsp) + Val((VarBuyValue * ndbs) / ndbsp) + Val((VarBuyValue * ndbl) / ndblp)
                    End If
                    Try
                        Call AddUpdateExpenseRow_ImportData(Dr("entrydate"), Dr("Security"), Dr("script"), CDate("1-1-1980"), VarExpenseAmount)
                    Catch ex As Exception

                    End Try

                Next


            End If

            '   objTrad.Delete_Expense_Data_All()
            ' objTrad.Insert_Expense_Data(G_DTExpenseData)
        Catch ex As Exception

        End Try


    End Sub
    ''' <summary>
    ''' GSub_CalculateExpense
    ''' </summary>
    ''' <param name="DtTrad">Trades datatable</param>
    ''' <param name="VarTradeType">FO:: Future and Option Trades Expense calculate, EQ :: equity trades expense calculate, CURR :: Currency trade expense calculate</param>
    ''' <param name="VarIsAddRow">TRUE: If New row Added , FALSE :: If row remove from table </param>
    ''' <remarks>This method call to Expense calculate</remarks>
    ''' 

    Public Sub GSub_CalculateExpense(ByVal DtTrad As DataTable, ByVal VarTradeType As String, ByVal VarIsAddRow As Boolean)
        Try


            Dim dtbuy As DataTable
            Dim dtSale As DataTable
            Dim dtbuyEQ As DataTable
            Dim dtSaleEQ As DataTable
            Dim dtbuyCurr As DataTable
            Dim dtSaleCurr As DataTable
            If VarTradeType = "FO" Then
                dtbuy = New DataView(GdtFOTrades, "qty > 0", "", DataViewRowState.CurrentRows).ToTable
                dtSale = New DataView(GdtFOTrades, "qty < 0", "", DataViewRowState.CurrentRows).ToTable
            ElseIf VarTradeType = "EQ" Then
                dtbuyEQ = New DataView(GdtEQTrades, "qty > 0", "", DataViewRowState.CurrentRows).ToTable
                dtSaleEQ = New DataView(GdtEQTrades, "qty < 0", "", DataViewRowState.CurrentRows).ToTable
            ElseIf VarTradeType = "CURR" Then
                dtbuyCurr = New DataView(GdtCurrencyTrades, "qty > 0", "", DataViewRowState.CurrentRows).ToTable
                dtSaleCurr = New DataView(GdtCurrencyTrades, "qty < 0", "", DataViewRowState.CurrentRows).ToTable
            End If


            Dim VarExpenseAmount As Double = 0
            If DtTrad.Rows.Count <= 0 Then Exit Sub

            If VarTradeType = "FO" Then REM F&O trade expense calc. and store in G_DTExpenseData datatable
                For Each Dr As DataRow In DtTrad.DefaultView.ToTable(True, "entry_date", "company", "script", "mdate", "cp").Rows
                    If VarIsAddRow = False Then
                        If GdtFOTrades.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length = 0 Then
                            If G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length > 0 Then
                                G_DTExpenseData.Rows.Remove(G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'")(0))
                            End If
                            Continue For
                        End If
                    End If
                    Dim VarBuyValue As Double = 0
                    Dim VarSaleValue As Double = 0


                    'VarBuyValue = Val(GdtFOTrades.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entry_date = #" & fdate(Dr("entry_date")) & "#").ToString)
                    Try

                        If Dr("cp") = "XX" Or Dr("cp") = "F" Or Dr("cp") = "X" Or Dr("cp") = "" Then
                            VarBuyValue = Math.Abs(Val(dtbuy.Compute("SUM(tot)", " script='" & Dr("script") & "'  and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                            VarSaleValue = Math.Abs(Val(dtSale.Compute("sum(tot)", "script='" & Dr("script") & "'  and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                            'If Dr("cp") = "XX" Or Dr("cp") = "F" Then
                            VarExpenseAmount = Val((VarBuyValue * futl) / futlp) + Val((VarSaleValue * futs) / futsp)
                        Else
                            If Val(spl) <> 0 Then
                                VarBuyValue = Math.Abs(Val(dtbuy.Compute("SUM(tot2)", " script='" & Dr("script") & "'  and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                                VarSaleValue = Math.Abs(Val(dtSale.Compute("sum(tot2)", "script='" & Dr("script") & "'  and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                                VarExpenseAmount = Val((VarBuyValue * spl) / splp) + Val((VarSaleValue * sps) / spsp)
                            Else
                                VarBuyValue = Math.Abs(Val(dtbuy.Compute("SUM(tot)", " script='" & Dr("script") & "'  and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                                VarSaleValue = Math.Abs(Val(dtSale.Compute("sum(tot)", "script='" & Dr("script") & "'  and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                                VarExpenseAmount = Val((VarBuyValue * prel) / prelp) + Val((VarSaleValue * pres) / presp)
                            End If
                        End If
                        Call AddUpdateExpenseRow(Dr("entry_date"), Dr("company"), Dr("script"), Dr("mdate"), VarExpenseAmount)

                    Catch ex As Exception

                    End Try
                Next
            ElseIf VarTradeType = "CURR" Then REM Currency trade expense calc. and store in G_DTExpenseData datatable
                For Each Dr As DataRow In DtTrad.DefaultView.ToTable(True, "entry_date", "company", "script", "mdate", "cp").Rows
                    If VarIsAddRow = False Then
                        If GdtCurrencyTrades.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length = 0 Then
                            If G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length > 0 Then
                                G_DTExpenseData.Rows.Remove(G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'")(0))
                            End If
                            Continue For
                        End If
                    End If
                    Dim VarBuyValue As Double = 0
                    Dim VarSaleValue As Double = 0
                    Dim Tmp As Double = 0
                    Tmp = Val(GdtCurrencyTrades.Compute("SUM(tot)", "").ToString)
                    'VarBuyValue = Val(GdtCurrencyTrades.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entry_date=#" & Dr("entry_date") & "#").ToString)
                    VarBuyValue = Math.Abs(Val(dtbuyCurr.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                    VarSaleValue = Math.Abs(Val(GdtCurrencyTrades.Compute("sum(tot)", "script='" & Dr("script") & "' and qty < 0 and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                    REM For Currency - IIf(UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "X", "F", UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1))) 
                    If Dr("cp") = "XX" Or Dr("cp") = "F" Or Dr("cp") = "X" Or Dr("cp") = "" Then
                        VarBuyValue = Math.Abs(Val(dtbuyCurr.Compute("SUM(tot)", " script='" & Dr("script") & "'  and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                        VarSaleValue = Math.Abs(Val(dtSaleCurr.Compute("sum(tot)", "script='" & Dr("script") & "'  and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                        VarExpenseAmount = Val((VarBuyValue * currfutl) / currfutlp) + Val((VarSaleValue * currfuts) / currfutsp)
                    Else
                        If Val(currspl) <> 0 Then
                            VarBuyValue = Math.Abs(Val(dtbuyCurr.Compute("SUM(tot2)", " script='" & Dr("script") & "'  and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                            VarSaleValue = Math.Abs(Val(dtSaleCurr.Compute("sum(tot2)", "script='" & Dr("script") & "'  and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                            VarExpenseAmount = Val((VarBuyValue * currspl) / currsplp) + Val((VarSaleValue * currsps) / currspsp)
                        Else
                            VarBuyValue = Math.Abs(Val(dtbuyCurr.Compute("SUM(tot)", " script='" & Dr("script") & "'  and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                            VarSaleValue = Math.Abs(Val(dtSaleCurr.Compute("sum(tot)", "script='" & Dr("script") & "' and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
                            VarExpenseAmount = Val((VarBuyValue * currprel) / currprelp) + Val((VarSaleValue * currpres) / currpresp)
                        End If
                    End If
                    Call AddUpdateExpenseRow(Dr("entry_date"), Dr("company"), Dr("script"), Dr("mdate"), VarExpenseAmount)
                Next
            ElseIf VarTradeType = "EQ" Then REM EQ trade expense calc. and store in G_DTExpenseData datatable
                For Each Dr As DataRow In DtTrad.DefaultView.ToTable(True, "entry_date", "company", "script").Rows
                    If VarIsAddRow = False Then
                        If GdtEQTrades.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length = 0 Then
                            If G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length > 0 Then
                                G_DTExpenseData.Rows.Remove(G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'")(0))
                            End If
                            Continue For
                        End If
                    End If
                    Dim VarBuyValue As Double = 0
                    Dim VarSaleValue As Double = 0
                    Dim VarDiffValue As Double = 0
                    'VarBuyValue = Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "script='" & Dr("script") & "' and qty > 0 and entry_date = #" & Dr("entry_date") & "#").ToString), 2)
                    VarBuyValue = Math.Abs(Math.Round(Val(dtbuyEQ.Compute("sum(tot)", "script='" & Dr("script") & "'  and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString), 2))
                    VarSaleValue = Math.Abs(Math.Round(Val(dtSaleEQ.Compute("sum(tot)", "script='" & Dr("script") & "'  and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString), 2))
                    VarDiffValue = VarBuyValue - VarSaleValue
                    If VarDiffValue > 0 Then
                        VarExpenseAmount = Val((VarDiffValue * dbl) / dblp) + Val((VarSaleValue * ndbs) / ndbsp) + Val((VarSaleValue * ndbl) / ndblp)
                    Else
                        VarDiffValue = -VarDiffValue
                        VarExpenseAmount = Val((VarDiffValue * dbs) / dbsp) + Val((VarBuyValue * ndbs) / ndbsp) + Val((VarBuyValue * ndbl) / ndblp)
                    End If
                    Try
                        Call AddUpdateExpenseRow(Dr("entry_date"), Dr("company"), Dr("script"), CDate("1-1-1980"), VarExpenseAmount)
                    Catch ex As Exception

                    End Try

                Next
            End If
            '   objTrad.Delete_Expense_Data_All()
            ' objTrad.Insert_Expense_Data(G_DTExpenseData)
        Catch ex As Exception

        End Try

    End Sub
    'Public Sub GSub_CalculateExpense(ByVal DtTrad As DataTable, ByVal VarTradeType As String, ByVal VarIsAddRow As Boolean)
    '    Dim VarExpenseAmount As Double = 0
    '    If DtTrad.Rows.Count <= 0 Then Exit Sub
    '    If VarTradeType = "FO" Then REM F&O trade expense calc. and store in G_DTExpenseData datatable
    '        For Each Dr As DataRow In DtTrad.DefaultView.ToTable(True, "entry_date", "company", "script", "mdate", "cp").Rows
    '            If VarIsAddRow = False Then
    '                If GdtFOTrades.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length = 0 Then
    '                    If G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length > 0 Then
    '                        G_DTExpenseData.Rows.Remove(G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'")(0))
    '                    End If
    '                    Continue For
    '                End If
    '            End If
    '            Dim VarBuyValue As Double = 0
    '            Dim VarSaleValue As Double = 0
    '            'VarBuyValue = Val(GdtFOTrades.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entry_date = #" & fdate(Dr("entry_date")) & "#").ToString)

    '            If Dr("cp") = "XX" Or Dr("cp") = "F" Or Dr("cp") = "X" Or Dr("cp") = "" Then
    '                VarBuyValue = Math.Abs(Val(GdtFOTrades.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '                VarSaleValue = Math.Abs(Val(GdtFOTrades.Compute("sum(tot)", "script='" & Dr("script") & "' and qty < 0 and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '            'If Dr("cp") = "XX" Or Dr("cp") = "F" Then
    '                VarExpenseAmount = Val((VarBuyValue * futl) / futlp) + Val((VarSaleValue * futs) / futsp)
    '            Else
    '                If Val(spl) <> 0 Then
    '                    VarBuyValue = Math.Abs(Val(GdtFOTrades.Compute("SUM(tot2)", " script='" & Dr("script") & "' and qty > 0 and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '                    VarSaleValue = Math.Abs(Val(GdtFOTrades.Compute("sum(tot2)", "script='" & Dr("script") & "' and qty < 0 and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '                    VarExpenseAmount = Val((VarBuyValue * spl) / splp) + Val((VarSaleValue * sps) / spsp)
    '                Else
    '                    VarBuyValue = Math.Abs(Val(GdtFOTrades.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '                    VarSaleValue = Math.Abs(Val(GdtFOTrades.Compute("sum(tot)", "script='" & Dr("script") & "' and qty < 0 and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '                    VarExpenseAmount = Val((VarBuyValue * prel) / prelp) + Val((VarSaleValue * pres) / presp)
    '                End If
    '            End If
    '            Call AddUpdateExpenseRow(Dr("entry_date"), Dr("company"), Dr("script"), Dr("mdate"), VarExpenseAmount)
    '        Next
    '    ElseIf VarTradeType = "CURR" Then REM Currency trade expense calc. and store in G_DTExpenseData datatable
    '        For Each Dr As DataRow In DtTrad.DefaultView.ToTable(True, "entry_date", "company", "script", "mdate", "cp").Rows
    '            If VarIsAddRow = False Then
    '                If GdtCurrencyTrades.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length = 0 Then
    '                    If G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length > 0 Then
    '                        G_DTExpenseData.Rows.Remove(G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'")(0))
    '                    End If
    '                    Continue For
    '                End If
    '            End If
    '            Dim VarBuyValue As Double = 0
    '            Dim VarSaleValue As Double = 0
    '            Dim Tmp As Double = 0
    '            Tmp = Val(GdtCurrencyTrades.Compute("SUM(tot)", "").ToString)
    '            'VarBuyValue = Val(GdtCurrencyTrades.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entry_date=#" & Dr("entry_date") & "#").ToString)
    '            VarBuyValue = Math.Abs(Val(GdtCurrencyTrades.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '            VarSaleValue = Math.Abs(Val(GdtCurrencyTrades.Compute("sum(tot)", "script='" & Dr("script") & "' and qty < 0 and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '            REM For Currency - IIf(UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1)) = "X", "F", UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1))) 
    '            If Dr("cp") = "XX" Or Dr("cp") = "F" Or Dr("cp") = "X" Or Dr("cp") = "" Then
    '                VarBuyValue = Math.Abs(Val(GdtCurrencyTrades.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '                VarSaleValue = Math.Abs(Val(GdtCurrencyTrades.Compute("sum(tot)", "script='" & Dr("script") & "' and qty < 0 and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '                VarExpenseAmount = Val((VarBuyValue * currfutl) / currfutlp) + Val((VarSaleValue * currfuts) / currfutsp)
    '            Else
    '                If Val(currspl) <> 0 Then
    '                    VarBuyValue = Math.Abs(Val(GdtCurrencyTrades.Compute("SUM(tot2)", " script='" & Dr("script") & "' and qty > 0 and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '                    VarSaleValue = Math.Abs(Val(GdtCurrencyTrades.Compute("sum(tot2)", "script='" & Dr("script") & "' and qty < 0 and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '                    VarExpenseAmount = Val((VarBuyValue * currspl) / currsplp) + Val((VarSaleValue * currsps) / currspsp)
    '                Else
    '                    VarBuyValue = Math.Abs(Val(GdtCurrencyTrades.Compute("SUM(tot)", " script='" & Dr("script") & "' and qty > 0 and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '                    VarSaleValue = Math.Abs(Val(GdtCurrencyTrades.Compute("sum(tot)", "script='" & Dr("script") & "' and qty < 0 and entry_date=#" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString))
    '                    VarExpenseAmount = Val((VarBuyValue * currprel) / currprelp) + Val((VarSaleValue * currpres) / currpresp)
    '                End If
    '            End If
    '            Call AddUpdateExpenseRow(Dr("entry_date"), Dr("company"), Dr("script"), Dr("mdate"), VarExpenseAmount)
    '        Next
    '    ElseIf VarTradeType = "EQ" Then REM EQ trade expense calc. and store in G_DTExpenseData datatable
    '        For Each Dr As DataRow In DtTrad.DefaultView.ToTable(True, "entry_date", "company", "script").Rows
    '            If VarIsAddRow = False Then
    '                If GdtEQTrades.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length = 0 Then
    '                    If G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").Length > 0 Then
    '                        G_DTExpenseData.Rows.Remove(G_DTExpenseData.Select("script='" & Dr("script") & "' AND entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'")(0))
    '                    End If
    '                    Continue For
    '                End If
    '            End If
    '            Dim VarBuyValue As Double = 0
    '            Dim VarSaleValue As Double = 0
    '            Dim VarDiffValue As Double = 0
    '            'VarBuyValue = Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "script='" & Dr("script") & "' and qty > 0 and entry_date = #" & Dr("entry_date") & "#").ToString), 2)
    '            VarBuyValue = Math.Abs(Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "script='" & Dr("script") & "' and qty > 0 and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString), 2))
    '            VarSaleValue = Math.Abs(Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "script='" & Dr("script") & "' AND qty < 0 and entry_date = #" & fDate(Dr("entry_date")) & "# And company='" & Dr("company") & "'").ToString), 2))
    '            VarDiffValue = VarBuyValue - VarSaleValue
    '            If VarDiffValue > 0 Then
    '                VarExpenseAmount = Val((VarDiffValue * dbl) / dblp) + Val((VarSaleValue * ndbs) / ndbsp) + Val((VarSaleValue * ndbl) / ndblp)
    '            Else
    '                VarDiffValue = -VarDiffValue
    '                VarExpenseAmount = Val((VarDiffValue * dbs) / dbsp) + Val((VarBuyValue * ndbs) / ndbsp) + Val((VarBuyValue * ndbl) / ndblp)
    '            End If
    '            Call AddUpdateExpenseRow(Dr("entry_date"), Dr("company"), Dr("script"), CDate("1-1-1980"), VarExpenseAmount)
    '        Next
    '    End If
    'End Sub

    Public Function fDate(ByVal dt As Date) As String
        Return Format(dt, "MMM/dd/yyyy")
    End Function
    Private Sub AddUpdateExpenseRow_ImportData(ByVal VarEntryDate As Date, ByVal VarCompany As String, ByVal VarScript As String, ByVal Exp_Date As Date, ByVal VarExpense As Double)

        Dim ExpDr() As DataRow = G_DTExpenseData_Importdata.Select("entry_date=#" & Format(VarEntryDate, "dd-MMM-yyyy") & "# AND company='" & VarCompany & "' AND script='" & VarScript & "'")
        If ExpDr.Length > 0 Then
            ExpDr(0).Item("expense") = VarExpense
        Else
            ReDim ExpDr(0)
            ExpDr(0) = G_DTExpenseData_Importdata.NewRow
            ExpDr(0).Item("Entry_Date") = Format(VarEntryDate, "dd-MMM-yyyy")
            ExpDr(0).Item("Company") = VarCompany
            ExpDr(0).Item("Script") = VarScript
            ExpDr(0).Item("exp_date") = Exp_Date
            ExpDr(0).Item("Expense") = VarExpense
            G_DTExpenseData_Importdata.Rows.Add(ExpDr(0))
        End If
    End Sub

    ''' <summary>
    ''' AddUpdateExpenseRow
    ''' </summary>
    ''' <param name="VarEntryDate">To Pass Entry Date</param>
    ''' <param name="VarCompany">To Pass Company Name</param>
    ''' <param name="VarScript">To Pass Script Name</param>
    ''' <param name="Exp_Date">To Pass Exp Date</param>
    ''' <param name="VarExpense">To Pass Expense Amount</param>
    ''' <remarks>This method call to Add or Update datarow in G_DTExpenseData</remarks>
    Private Sub AddUpdateExpenseRow(ByVal VarEntryDate As Date, ByVal VarCompany As String, ByVal VarScript As String, ByVal Exp_Date As Date, ByVal VarExpense As Double)

        Dim ExpDr() As DataRow = G_DTExpenseData.Select("entry_date=#" & Format(VarEntryDate, "dd-MMM-yyyy") & "# AND company='" & VarCompany & "' AND script='" & VarScript & "'")
        If ExpDr.Length > 0 Then
            ExpDr(0).Item("expense") = VarExpense
        Else
            ReDim ExpDr(0)
            ExpDr(0) = G_DTExpenseData.NewRow
            ExpDr(0).Item("Entry_Date") = Format(VarEntryDate, "dd-MMM-yyyy")
            ExpDr(0).Item("Company") = VarCompany
            ExpDr(0).Item("Script") = VarScript
            ExpDr(0).Item("exp_date") = Exp_Date
            ExpDr(0).Item("Expense") = VarExpense
            G_DTExpenseData.Rows.Add(ExpDr(0))
        End If
    End Sub
#End Region

    Public Sub Sub_Set_Conn_Msg(ByVal flg As Boolean, ByVal sMsg As String)
        If flg = True Then
            ' MDI.ToolStripTextBoxMsg.Text = sMsg
        Else
            SplashScreenMnUsrLic.lblAMCText.Text = sMsg
            SplashScreenMnUsrLic.lblAMCText.Refresh()
        End If

        If ("UDP server connection not establish." = sMsg) Then
            Dim obj_Frm_UDPSetting As New Frm_UDPSetting
            If obj_Frm_UDPSetting.ShowDialog() = DialogResult.Cancel Then
                End
            Else
                obj_CLS_UDP.GFun_Get_UDP_IP_Port()
            End If
        End If
    End Sub

    Public Function GFun_Get_Svr_UDP_IP_Port() As String()
        Dim str_IP_Port() As String = {"", ""}
        If IO.File.Exists(Application.StartupPath & "\UDP Connection.txt") = False Then
            Return str_IP_Port
            Exit Function
        End If
        Dim FR As New IO.StreamReader(Application.StartupPath & "\UDP Connection.txt")
        Try
            Dim StrTemp As String = ""
            StrTemp = FR.ReadLine()
            str_IP_Port(0) = StrTemp.Substring("UDP IP ::".Length)
            StrTemp = FR.ReadLine()
            str_IP_Port(1) = StrTemp.Substring("UDP Port ::".Length)
            StrTemp = FR.ReadLine()
        Catch ex As Exception
        Finally
            FR.Close()
            FR = Nothing
        End Try
        Return str_IP_Port
    End Function
    Public Sub GSub_Write_synfutLog(ByVal Message As String)
        Dim FSErrorLogFile As System.IO.StreamWriter
        Try
            If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
            End If
            'FSErrorLogFile = New StreamWriter("C:\ErrorLog.txt", True)
            FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\synfutLog" + DateTime.Now.ToString("ddMMMyyyy") + ".txt", True)
            FSErrorLogFile.WriteLine(Now.ToString & "==>" & Message)
            FSErrorLogFile.Close()
        Catch ex As Exception
            ' FSErrorLogFile = New StreamWriter(Application.StartupPath & "\ErrorLog.txt", True)
        End Try

    End Sub
    Public Sub GSub_Write_ErrorLog(ByVal Message As String)
        Dim FSErrorLogFile As System.IO.StreamWriter
        Try
            If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
            End If
            'FSErrorLogFile = New StreamWriter("C:\ErrorLog.txt", True)
            FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\ErrorLog.txt", True)
            FSErrorLogFile.WriteLine(Now.ToString & "==>" & Message)
            FSErrorLogFile.Close()
        Catch ex As Exception
            ' FSErrorLogFile = New StreamWriter(Application.StartupPath & "\ErrorLog.txt", True)
        End Try
        
    End Sub

    Public Sub Write_Log1(ByVal Message As String)
        Dim FSErrorLogFile As System.IO.StreamWriter
        Try
            'FSErrorLogFile = New StreamWriter("C:\VolhedgeLog.txt", True)
            If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
            End If
            FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\VolhedgeLog.txt", True)
            FSErrorLogFile.WriteLine(Now.ToString & "==>" & Message)
            FSErrorLogFile.Close()
        Catch ex As Exception
            '  FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog.txt", True)
        End Try
       
    End Sub
    Public Sub Write_Log_Startup(ByVal Message As String)
        Dim FSErrorLogFile As System.IO.StreamWriter
        Try
            If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
            End If
            'FSErrorLogFile = New StreamWriter("C:\VolhedgeLog.txt", True)
            FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\VolhedgeLogStartup.txt", True)
            FSErrorLogFile.WriteLine(Now.ToString & "==>" & Message)
            FSErrorLogFile.Close()
        Catch ex As Exception
            '  FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog.txt", True)
        End Try
     
    End Sub
    Dim sloc As New Object
    Public Sub Write_TradeLog2(ByVal Message As String, ByVal tik As Long)
        SyncLock sloc
            Dim FSTimerLogFile As System.IO.StreamWriter
            'If FSTimerLogFile Is Nothing = True Then
            Try

                If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                    Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
                End If
                '  FSTimerLogFile = New StreamWriter(Now.ToString & "==>" & tik & "==>" & Message, True)
                FSTimerLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\VolHedgeTradeErrorLog.txt", True)
                FSTimerLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message, True)
                FSTimerLogFile.Close()
            Catch ex As Exception

            End Try
           
            'Else
            'FSTimerLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
            'End If
            'Dim FSErrorLogFile As System.IO.StreamWriter
            'Try
            '    FSErrorLogFile = New StreamWriter("C:\VolhedgeLogTrd.txt", True)
            'Catch ex As Exception
            '    FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLogTrd.txt", True)
            'End Try
            'FSErrorLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
            'FSErrorLogFile.Close()

            ' FSTimerLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
        End SyncLock
    End Sub

    Public Sub Write_TradeLog_viral(ByVal Message As String, ByVal fromtik As Long, ByVal tik As Long)

        Dim FSTimerLogFile As System.IO.StreamWriter

        Try

            'If Not Directory.Exists(Application.StartupPath & "\ViralLog") Then
            '    Directory.CreateDirectory(Application.StartupPath & "\ViralLog")
            'End If
            If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
            End If
            ' FSTimerLogFile = New StreamWriter(Application.StartupPath & "\ViralLog\VolHedgeTradeErrorLog_trade.txt", True)
            FSTimerLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\VolHedgeTradeErrorLog_trade.txt", True)
            FSTimerLogFile.WriteLine(Now.ToString & "==>" & Message & "==>" & ((System.Environment.TickCount - fromtik) / 1000), True)
            FSTimerLogFile.Close()


        Catch ex As Exception

        End Try


    End Sub
    Dim sloc1 As New Object
    Public Sub Write_TradeLog3(ByVal Message As String, ByVal tik As Long)
        SyncLock sloc1
            Dim FSTimerLogFile As System.IO.StreamWriter
            'If FSTimerLogFile Is Nothing = True Then
            Try

                If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                    Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
                End If
                '  FSTimerLogFile = New StreamWriter(Now.ToString & "==>" & tik & "==>" & Message, True)
                FSTimerLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\VolHedgeTradeErrorLog_trade.txt", True)
                FSTimerLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message, True)
                FSTimerLogFile.Close()
            Catch ex As Exception

            End Try

            'Else
            'FSTimerLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
            'End If
            'Dim FSErrorLogFile As System.IO.StreamWriter
            'Try
            '    FSErrorLogFile = New StreamWriter("C:\VolhedgeLogTrd.txt", True)
            'Catch ex As Exception
            '    FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLogTrd.txt", True)
            'End Try
            'FSErrorLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
            'FSErrorLogFile.Close()

            ' FSTimerLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
        End SyncLock
    End Sub
    Dim sloctimelog1 As New Object
    Public Sub Write_TimeLog1(ByVal msg As String)

        SyncLock sloctimelog1
            Dim FSErrorLogFile As System.IO.StreamWriter
            If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
            End If
            FSErrorLogFile = New System.IO.StreamWriter(Application.StartupPath & "\VolhedgeLog\" + "VH_LOG_" + DateTime.Now.ToString("ddMMMyyyy") + ".txt", True)
            FSErrorLogFile.WriteLine(Convert.ToString(DateTime.Now.ToString() + "|") & msg)
            FSErrorLogFile.Close()
        End SyncLock
    End Sub

    
    'Public Sub Write_TimeLog(ByVal Message As String, ByVal tik As Long)
    '    SyncLock sloc
    '        Dim FSTimerLogFile As System.IO.StreamWriter
    '        ' If FSTimerLogFile Is Nothing = True Then
    '        Try
    '            If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
    '                Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
    '            End If


    '            FSTimerLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\VolHedgeTimeLog.txt", True)
    '            FSTimerLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message, True)
    '            FSTimerLogFile.Close()
    '        Catch ex As Exception

    '        End Try


    '        'Else
    '        'FSTimerLogFile.WriteLine(Now.ToString & "==>" & Message)
    '        'End If
    '        'Dim FSErrorLogFile As System.IO.StreamWriter
    '        'Try
    '        '    FSErrorLogFile = New StreamWriter("C:\VolhedgeLogTrd.txt", True)
    '        'Catch ex As Exception
    '        '    FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLogTrd.txt", True)
    '        'End Try
    '        'FSErrorLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
    '        'FSErrorLogFile.Close()

    '        ' FSTimerLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
    '    End SyncLock
    'End Sub
    Public Sub Write_ErrorLogScenario(ByVal Message As String)
        SyncLock sloc
            Dim FSTimerLogFile As System.IO.StreamWriter
            ' If FSTimerLogFile Is Nothing = True Then
            Try
                If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                    Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
                End If

                FSTimerLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\VolHedgeErrorLogScenario.txt", True)
                FSTimerLogFile.WriteLine(Now.ToString & "==>" & Message)

                FSTimerLogFile.Close()
            Catch ex As Exception

            End Try


            'Else
            'FSTimerLogFile.WriteLine(Now.ToString & "==>" & Message)
            'End If
            'Dim FSErrorLogFile As System.IO.StreamWriter
            'Try
            '    FSErrorLogFile = New StreamWriter("C:\VolhedgeLogTrd.txt", True)
            'Catch ex As Exception
            '    FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLogTrd.txt", True)
            'End Try
            'FSErrorLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
            'FSErrorLogFile.Close()

            ' FSTimerLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
        End SyncLock
    End Sub
    Public Sub Write_Error_scenario(ByVal Message As String)
        SyncLock sloc
            Dim FSTimerLogFile As System.IO.StreamWriter
            ' If FSTimerLogFile Is Nothing = True Then
            Try
                If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                    Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
                End If

                FSTimerLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\VolHedge_Error_scenario.txt", True)
                FSTimerLogFile.WriteLine(Now.ToString & "==>" & Message)

                FSTimerLogFile.Close()
            Catch ex As Exception

            End Try


            'Else
            'FSTimerLogFile.WriteLine(Now.ToString & "==>" & Message)
            'End If
            'Dim FSErrorLogFile As System.IO.StreamWriter
            'Try
            '    FSErrorLogFile = New StreamWriter("C:\VolhedgeLogTrd.txt", True)
            'Catch ex As Exception
            '    FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLogTrd.txt", True)
            'End Try
            'FSErrorLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
            'FSErrorLogFile.Close()

            ' FSTimerLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
        End SyncLock
    End Sub
    Public Sub Write_ErrorLog3(ByVal Message As String)
        SyncLock sloc
            Dim FSTimerLogFile As System.IO.StreamWriter
            ' If FSTimerLogFile Is Nothing = True Then
            Try
                If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                    Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
                End If

                FSTimerLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLog\VolHedgeErrorLog.txt", True)
                FSTimerLogFile.WriteLine(Now.ToString & "==>" & Message)

                FSTimerLogFile.Close()
            Catch ex As Exception

            End Try
           
           
            'Else
            'FSTimerLogFile.WriteLine(Now.ToString & "==>" & Message)
            'End If
            'Dim FSErrorLogFile As System.IO.StreamWriter
            'Try
            '    FSErrorLogFile = New StreamWriter("C:\VolhedgeLogTrd.txt", True)
            'Catch ex As Exception
            '    FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLogTrd.txt", True)
            'End Try
            'FSErrorLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
            'FSErrorLogFile.Close()

            ' FSTimerLogFile.WriteLine(Now.ToString & "==>" & tik & "==>" & Message)
        End SyncLock
    End Sub

    'Public Sub 'Write_Log2(ByVal Message As String)
    '    SyncLock sloc
    '        Dim FSErrorLogFile As System.IO.StreamWriter
    '        Try
    '            FSErrorLogFile = New StreamWriter("C:\VolhedgeLogTCPCRASH.txt", True)
    '        Catch ex As Exception
    '            FSErrorLogFile = New StreamWriter(Application.StartupPath & "\VolhedgeLogTCPCRASH.txt", True)
    '        End Try
    '        FSErrorLogFile.WriteLine(Now.ToString & "==>" & Message)
    '        FSErrorLogFile.Close()
    '    End SyncLock
    'End Sub
    Public Function GFun_Date_From_MMddYYYY(ByVal str_Date As String) As Date
        str_Date = str_Date.Trim
        Dim strarr() As String = str_Date.Split("/")
        If strarr.Length = 0 Then strarr = str_Date.Split("\")
        If strarr.Length > 0 Then
            Return Date.Parse(strarr(2) & "-" & strarr(0) & "-" & strarr(1))
        Else
            Return Nothing
        End If
    End Function

    Public Sub GSub_Check_Application_Expiry(ByVal Curr_Date As Date)
        If Curr_Date >= CDate(clsGlobal.Expire_Date) Then
            MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
            'Call Sub_Get_Version_TextFile()
            Application.Exit()
            End
            Exit Sub
        End If
    End Sub

    Public Sub GSub_Save_Srv_UDP_IP_Port(ByVal IP As String, ByVal Port As Integer)
        Dim FW As New IO.StreamWriter(Application.StartupPath & "\UDP Connection.txt")
        FW.WriteLine("UDP IP ::" & IP)
        FW.WriteLine("UDP Port ::" & Port)
        FW.Close()
        FW = Nothing
    End Sub

    Public Sub Write_ExposureMarginLog(ByVal Message As String)
        SyncLock sloc
            Dim FSExpMarginLog As System.IO.StreamWriter
            ' If FSTimerLogFile Is Nothing = True Then
            Try
                If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                    Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
                End If

                FSExpMarginLog = New StreamWriter(Application.StartupPath & "\VolhedgeLog\VolHedge_ExposureMarginLog.txt", True)
                FSExpMarginLog.WriteLine(Now.ToString & "==>" & Message)

                FSExpMarginLog.Close()
            Catch ex As Exception

            End Try
        End SyncLock
    End Sub

End Module
