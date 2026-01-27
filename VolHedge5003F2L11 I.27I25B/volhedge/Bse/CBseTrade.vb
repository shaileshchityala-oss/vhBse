Option Strict On
Option Explicit On

Imports System.IO
Imports System.Globalization


'Imports System.IO
'Imports VolHedge.ImportData
'Imports System.Configuration


'Public Class CTradeFileCsv

'    Public Sub New()
'        Init()
'    End Sub

'    ' ============ CSV COLUMN POSITION VARIABLES ============
'    Public Shared mPreTradeNumber As Integer
'    Public Shared mPreTradeStatus As Integer
'    Public Shared mPreSeriesInstrument As Integer
'    Public Shared mPreSymbol As Integer
'    Public Shared mPreExpiryDate As Integer
'    Public Shared mPreStrikePrice As Integer
'    Public Shared mPreOptionType As Integer
'    Public Shared mPreSecurityDesc As Integer
'    Public Shared mPreBookType As Integer
'    Public Shared mPreBookTypeName As Integer
'    Public Shared mPreMarketType As Integer
'    Public Shared mPreTraderId As Integer
'    Public Shared mPreBranchId As Integer
'    Public Shared mPreBuySell As Integer
'    Public Shared mPreTradeQty As Integer
'    Public Shared mPreTradePrice As Integer
'    Public Shared mPreProClient As Integer
'    Public Shared mPreAccountId As Integer
'    Public Shared mPreParticipantBrokerId As Integer
'    Public Shared mPreOpenCloseFlag As Integer
'    Public Shared mPreCoverUncoverFlag As Integer
'    Public Shared mPreTradeEntryDateTime As Integer
'    Public Shared mPreTradeModifiedDateTime As Integer
'    Public Shared mPreExchangeOrderNumber As Integer
'    Public Shared mPreStrategyName As Integer
'    Public Shared mPreExchRetailerId As Integer

'    Public Sub Init()

'        mPreTradeNumber = 0
'        mPreTradeStatus = 1
'        mPreSeriesInstrument = 2
'        mPreSymbol = 3
'        mPreExpiryDate = 4
'        mPreStrikePrice = 5
'        mPreOptionType = 6
'        mPreSecurityDesc = 7
'        mPreBookType = 8
'        mPreBookTypeName = 9
'        mPreMarketType = 10
'        mPreTraderId = 11
'        mPreBranchId = 12
'        mPreBuySell = 13
'        mPreTradeQty = 14
'        mPreTradePrice = 15
'        mPreProClient = 16
'        mPreAccountId = 17
'        mPreParticipantBrokerId = 18
'        mPreOpenCloseFlag = 19
'        mPreCoverUncoverFlag = 20
'        mPreTradeEntryDateTime = 21
'        mPreTradeModifiedDateTime = 22
'        mPreExchangeOrderNumber = 23
'        '" → intentionally skipped
'        mPreTradeModifiedDateTime = 25   ' second occurrence
'        mPreExchangeOrderNumber = 26     ' second occurrence
'        mPreStrategyName = 27
'        mPreExchRetailerId = 28

'    End Sub

'End Class


Public Class CBseEqTradeRecord

    ' ===== CORE =====
    Public EntryNo As Integer          ' entryno
    Public Company As String           ' company
    Public CP As String                ' cp
    Public Script As String            ' script (UCase)
    Public Dealer As String            ' dealer
    Public BuySell As String           ' buysell

    ' ===== TRADE =====
    Public Qty As Integer              ' Qty
    Public Rate As Decimal             ' rate
    Public Total As Decimal            ' tot

    ' ===== ORDER =====
    Public EntryDate As Date            ' entrydate
    Public OrderNo As String            ' orderno
    Public IsLiq As Integer             ' IsLiq
    Public ActivityTime As Date         ' lActivityTime
    Public Entry_Date As Date           ' entry_date
    Public FileFlag As String           ' FileFlag
    Public Eq As String                 ' eq

    ' ===== SECURITY =====
    Public TokenNo As Integer           ' Security.token
    Public OrgDealer As String          ' OrgDealer

End Class






'Public Class CBseTrades

'    Public mDtTrades As DataTable
'    Private mImportData As import_Data
'    Private mImportOps As ImportOperation
'    Private mMdbConn As CMdbConnection
'    Private mObjAna As analysisprocess
'    Public Sub New()
'        mImportData = New import_Data()
'        mObjAna = New analysisprocess()
'        'ConfigurationManager.AppSettings()
'        'Dim str As String = ConfigurationSettings.AppSettings("dbname")
'        'Dim constr As String = ConfigurationSettings.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str & ";Jet OLEDB:Database Password=" & clsGlobal.glbAcessPassWord & ""

'        Dim str As String = ConfigurationManager.AppSettings("dbname")
'        Dim constr As String = ConfigurationManager.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str & ";Jet OLEDB:Database Password=" & clsGlobal.glbAcessPassWord & ""

'        mMdbConn = New CMdbConnection(constr)

'    End Sub

'    Private mSelect_GetFoTrd As String = "SELECT   QGetFoTrd.company, QGetFoTrd.entryno, QGetFoTrd.buysell, QGetFoTrd.orderno, QGetFoTrd.script, QGetFoTrd.instrumentname, QGetFoTrd.strikerate, QGetFoTrd.cp, QGetFoTrd.mdate, QGetFoTrd.Dealer, QGetFoTrd.entrydate, QGetFoTrd.Qty, QGetFoTrd.Rate, QGetFoTrd.IsLiq, QGetFoTrd.lActivityTime, QGetFoTrd.entry_date, QGetFoTrd.Tot,  QGetFoTrd.FileFlag, Contract.Token AS Tokenno, IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1,QGetFoTrd.OrgDealer,0 AS token, Format(mdate,'mm/yyyy') AS mo, (IIf(qty=0,1,qty)*rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([qty]<0,[qty],0) AS SaleQty, IIf([qty]>0,[qty]*rate,0) AS BuyVal, IIf([qty]<0,[qty]*rate,0) AS SaleVal   " &
'                          " FROM QGetFoTrd INNER JOIN Contract ON QGetFoTrd.script = Contract.script;"
'    Private mSelect_GetCurTrd As String = "SELECT   QGetCurTrd.company, QGetCurTrd.entryno, QGetCurTrd.buysell, QGetCurTrd.orderno, QGetCurTrd.script, QGetCurTrd.instrumentname, QGetCurTrd.strikerate, QGetCurTrd.cp, QGetCurTrd.mdate, QGetCurTrd.Dealer, QGetCurTrd.entrydate, [Q]*[multiplier] AS Qty, QGetCurTrd.Rate, QGetCurTrd.IsLiq, QGetCurTrd.lActivityTime, QGetCurTrd.entry_date, ([Q]*[multiplier])*[Rate] AS Tot, QGetCurTrd.FileFlag, Currency_Contract.Token AS Tokenno, IIf(([cp]='F'),0,IIf(IsNull([contract].[Token]),0,[contract].[Token])) AS Token1,QGetCurTrd.OrgDealer " &
'                           " FROM (QGetCurTrd INNER JOIN Currency_Contract ON QGetCurTrd.script = Currency_Contract.script) LEFT JOIN Contract ON QGetCurTrd.script = Contract.OScript Where 1 = 1"
'    Private mSelect_GetEqTrd As String = "SELECT   QGetEqTrd.entryno, QGetEqTrd.company, QGetEqTrd.cp, QGetEqTrd.Script, QGetEqTrd.Dealer, QGetEqTrd.buysell, QGetEqTrd.Qty, QGetEqTrd.Rate, QGetEqTrd.tot, QGetEqTrd.entrydate, QGetEqTrd.orderno, QGetEqTrd.IsLiq, QGetEqTrd.lActivityTime, QGetEqTrd.entry_date, QGetEqTrd.FileFlag, QGetEqTrd.eq, Security.token AS tokenno,QGetEqTrd.OrgDealer " &
'                          " FROM QGetEqTrd INNER JOIN Security ON QGetEqTrd.Script = Security.script;"


'#Region "FO"

'    Private Function DTGetsFoTrd() As DataTable
'        Try
'            mMdbConn.ParamClear()
'            mMdbConn.mCmd_Text = mSelect_GetFoTrd
'            mMdbConn.mCmd_type = CommandType.Text
'            Return mMdbConn.FillList()
'        Catch ex As Exception
'            MsgBox(ex.ToString)
'            Return Nothing
'        End Try
'    End Function

'    Private Sub InsertFoTrd(ByVal sSql As String)
'        Dim StrSql As String
'        Try

'            StrSql = "INSERT INTO trading (instrumentname, company, mdate, strikerate, cp, script, qty, rate, entrydate, entryno, token1, isliq, orderno, lActivityTime, FileFlag,Dealer, token,mo, tot, entry_date, tot2, cpf, BuyQty, SaleQty, BuyVal, SaleVal,exchange)" &
'                     " SELECT instrumentname,company,mdate,strikerate,cp,script,Qty,Rate,entrydate,entryno,Token1,IsLiq,orderno,lActivityTime,FileFlag,OrgDealer, token,mo, tot, entry_date, tot2, cpf, BuyQty, SaleQty, BuyVal, SaleVal,'BSE' " &
'                    " FROM (" & sSql & ") as tlb; "
'            mMdbConn.ParamClear()
'            mMdbConn.mCmd_Text = StrSql
'            mMdbConn.mCmd_type = CommandType.Text
'            mMdbConn.ExecuteNonQuery(CommandType.Text)
'            mMdbConn.mCmd_type = CommandType.StoredProcedure
'        Catch ex As Exception
'            MsgBox("Error in inserting fo. trade." & vbCrLf & ex.Message)
'        End Try
'    End Sub

'    Public Function FromGetsFOTEXT(ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String) As Boolean
'        Try
'            Dim VarFilePath As String = CurrentFilePath
'            'If PrevFilePath = "" Then
'            '    VarFilePath = CurrentFilePath
'            'Else
'            '    VarFilePath = PrevFilePath
'            'End If

'            If import_Data.ValidateTxtFile(VarFilePath, "FO") = False Then
'                Return False
'                Exit Function
'            End If

'            Call import_Data.CopyToData(VarFilePath, "GETS FO")
'            Dim Dtr As DataTable
'            mImportOps.ImportGetFoTrd()
'            Dtr = DTGetsFoTrd()
'            If Dtr.Rows.Count = 0 Then
'                Return False
'                Exit Function
'            End If
'            InsertFoTrd(Replace(mImportData.Select_GetFoTrd, ";", ""))
'            Dim status As Boolean
'            status = mImportData.SetImportFoTrdDt(Dtr, DTMargeFOData)
'            Return status
'        Catch ex As Exception
'            MsgBox("Error in Procedure : FromGetsFoText() " & vbCrLf & ex.Message)
'            Return False
'        End Try
'    End Function

'    Private Function SetImportFoTrdDt(ByVal Dtr As DataTable, ByRef DTMargeFOData As DataTable) As Boolean
'        Dim tik As Long = System.Environment.TickCount
'        Try
'            Dim DTTrade As New DataTable
'            REM 1: create datatable for future and option
'            With DTTrade.Columns
'                .Add("entryno", GetType(Integer))
'                .Add("instrumentname")
'                .Add("company")
'                .Add("mdate", GetType(Date))
'                .Add("strikerate", GetType(Double))
'                .Add("cp")
'                .Add("qty", GetType(Double))
'                .Add("rate", GetType(Double))
'                .Add("tot", GetType(Double))
'                .Add("tot2", GetType(Double))
'                .Add("entrydate", GetType(Date))
'                .Add("script")
'                .Add("token1", GetType(Long))
'                .Add("isliq")
'                .Add("orderno", GetType(Long))
'                .Add("lActivityTime", GetType(Long))
'                .Add("FileFlag")
'            End With
'            Dim Dv As DataView
'            Dv = New DataView(Dtr, "", "", DataViewRowState.CurrentRows)
'            DTTrade = Dv.ToTable(False, "entryno", "instrumentname", "company", "mdate", "strikerate", "cp", "qty", "rate", "tot", "entrydate", "script", "token1", "isliq", "orderno", "lActivityTime", "FileFlag", "Dealer")
'            Call insert_FOTradeToGlobalTable(DTTrade)
'            Dim status As Boolean
'            status = False
'            If (DTTrade.Rows.Count > 0) Then
'                VarImportInserted = True
'                status = True
'                'Write_TradeLog2("Start UnMatch Process For MainTable(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
'                'Etik = System.Environment.TickCount
'                mObjAna.process_data_FO(DTTrade)
'                'Write_TradeLog2("End   UnMatch Process For MainTable(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
'                'Etik = System.Environment.TickCount
'            End If

'            'Write_TradeLog2("Start Prepare DTMargeFOData for Exp(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
'            'Etik = System.Environment.TickCount
'            Dim Dtmrg As DataTable
'            Dv = New DataView(Dtr, "tokenno > 0", "", DataViewRowState.CurrentRows)
'            Dtmrg = Dv.ToTable(False, "entryno", "instrumentname", "company", "mdate", "strikerate", "cp", "Script", "Dealer", "buysell", "Qty", "Rate", "entrydate", "orderno", "Tokenno", "IsLiq", "lActivityTime", "entry_date")
'            status = True
'            Dtmrg.Columns("Qty").ColumnName = "unit"
'            DTMargeFOData.Merge(Dtmrg)
'            DTMargeFOData.AcceptChanges()
'            Dtmrg.Dispose()
'            'Write_TradeLog_viral("SetImportFoTrdDT", tik, System.Environment.TickCount)
'            Return status
'        Catch ex As Exception
'            MsgBox("Error in Procedure : SetImportFoTrdDt." & vbCrLf & ex.Message)
'            Return False
'        End Try

'        'Write_TradeLog_viral("setimportfotrddt", tik, System.Environment.TickCount)
'    End Function


'#End Region

'#Region "EQ"

'    Public Function FromGetsEQTEXT(ByRef DTMargeEQData As DataTable, ByVal CurrentFilePath As String) As Boolean
'        Try
'            Dim VarFilePath As String = CurrentFilePath

'            If import_Data.ValidateTxtFile(VarFilePath, "EQ") = False Then
'                Return False
'                Exit Function
'            End If
'            Call import_Data.CopyToData(VarFilePath, "GETS EQ")

'            Dim Dtr As DataTable
'            Call mImportOps.ImportGetEQTrd()
'            Dtr = DTGetsEqTrd()
'            If Dtr.Rows.Count = 0 Then
'                Return False
'                Exit Function
'            End If
'            InsertEqTrd(Replace(mSelect_GetEqTrd, ";", ""))

'            Dim status As Boolean
'            status = SetImportEqTrdDt(Dtr, DTMargeEQData)
'            Return status
'        Catch ex As Exception
'            MsgBox("Error in Procedure : FromGetsEqText() " & vbCrLf & ex.Message)
'            Return False
'        End Try
'    End Function

'    Private Sub InsertEqTrd(ByVal sSql As String)
'        Dim StrSql As String
'        Try
'            StrSql = "INSERT INTO equity_trading ( script, company, eq, qty, rate, entrydate, entryno, orderno, lActivityTime, FileFlag,Dealer ) " &
'                  " SELECT Script,company,eq,Qty,Rate,entrydate,entryno,orderno,lActivityTime,FileFlag,OrgDealer  " &
'                  " FROM (" & sSql & ") as tlb; "

'            mMdbConn.ParamClear()
'            mMdbConn.mCmd_Text = StrSql
'            mMdbConn.mCmd_type = CommandType.Text
'            mMdbConn.ExecuteNonQuery(CommandType.Text)
'        Catch ex As Exception
'            MsgBox("Error in inserting eq. trade." & vbCrLf & ex.Message)
'        End Try
'    End Sub

'    Private Function DTGetsEqTrd() As DataTable
'        Try
'            mMdbConn.ParamClear()
'            mMdbConn.mCmd_Text = mSelect_GetEqTrd
'            mMdbConn.mCmd_type = CommandType.Text
'            Return mMdbConn.FillList()
'        Catch ex As Exception
'            MsgBox(ex.ToString)
'            Return Nothing
'        End Try
'    End Function

'    Private Function SetImportEqTrdDt(ByVal Dtr As DataTable, ByRef DTMargeEQData As DataTable) As Boolean
'        Try
'            Dim DTTrade As New DataTable
'            REM 1: create datatable for future and option
'            With DTTrade.Columns
'                .Add("script")
'                .Add("company")
'                .Add("eq")
'                .Add("qty", GetType(Double))
'                .Add("rate", GetType(Double))
'                .Add("tot", GetType(Double))
'                .Add("tot2", GetType(Double))
'                .Add("entrydate", GetType(Date))
'                .Add("entryno", GetType(Integer))
'                .Add("orderno", GetType(Long))
'                .Add("lActivityTime", GetType(Long))
'                .Add("FileFlag")
'            End With
'            Dim Dv As DataView
'            'Dim Dtm As DataTable
'            'Dtm = Dtr
'            Dv = New DataView(Dtr, "", "", DataViewRowState.CurrentRows)
'            DTTrade = Dv.ToTable(False, "script", "company", "eq", "qty", "rate", "tot", "entrydate", "entryno", "orderno", "lActivityTime", "FileFlag", "Dealer")

'            'Call objTrad.insert_EQTrading(DTTrade)
'            Call insert_EQTradeToGlobalTable(DTTrade)

'            Dim status As Boolean
'            status = False
'            If (DTTrade.Rows.Count > 0) Then
'                VarImportInserted = True
'                status = True
'                mObjAna.process_data_EQ(DTTrade)
'            End If

'            Dim Dtmrg As DataTable
'            Dv = New DataView(Dtr, "tokenno > 0", "", DataViewRowState.CurrentRows)
'            Dtmrg = Dv.ToTable(False, "entryno", "company", "cp", "Script", "Dealer", "buysell", "Qty", "Rate", "entrydate", "orderno", "Tokenno", "IsLiq", "lActivityTime", "entry_date")
'            status = True
'            Dtmrg.Columns("Qty").ColumnName = "unit"
'            DTMargeEQData.Merge(Dtmrg)
'            DTMargeEQData.AcceptChanges()
'            Dtmrg.Dispose()

'            Return status
'        Catch ex As Exception
'            MsgBox("Error in Procedure : SetImportEqTrdDt." & vbCrLf & ex.Message)
'            Return False
'        End Try
'    End Function


'#End Region

'End Class

Public Class CBseEqTradeCsvReader

    Public Shared Function ReadFile(filePath As String) As List(Of CBseEqTradeRecord)

        Dim result As New List(Of CBseEqTradeRecord)

        Using sr As New StreamReader(filePath)
            ' Skip header
            If Not sr.EndOfStream Then sr.ReadLine()

            While Not sr.EndOfStream
                Dim line As String = sr.ReadLine()
                If String.IsNullOrWhiteSpace(line) Then Continue While

                Dim cols() As String = line.Split(","c)
                result.Add(MapRow(cols))
            End While
        End Using

        Return result

    End Function

    ' ---------------- MAP ONE ROW ----------------
    Private Shared Function MapRow(cols() As String) As CBseEqTradeRecord

        Dim r As New CBseEqTradeRecord()

        r.EntryNo = ToInt(cols, 0)
        r.Company = ToStr(cols, 1)
        r.CP = ToStr(cols, 2)
        r.Script = ToStr(cols, 3)
        r.Dealer = ToStr(cols, 4)
        r.BuySell = ToStr(cols, 5)

        r.Qty = ToInt(cols, 6)
        r.Rate = ToDec(cols, 7)
        r.Total = ToDec(cols, 8)

        r.EntryDate = ToDate(cols, 9)
        r.OrderNo = ToStr(cols, 10)
        r.IsLiq = ToInt(cols, 11)
        r.ActivityTime = ToDate(cols, 12)
        r.Entry_Date = ToDate(cols, 13)
        r.FileFlag = ToStr(cols, 14)
        r.Eq = ToStr(cols, 15)

        r.TokenNo = ToInt(cols, 16)
        r.OrgDealer = ToStr(cols, 17)

        Return r

    End Function

    ' ---------------- STRICT SAFE CONVERTERS ----------------
    Private Shared Function ToStr(c() As String, i As Integer) As String
        If i < 0 OrElse i >= c.Length Then Return String.Empty
        Return c(i).Trim()
    End Function

    Private Shared Function ToInt(c() As String, i As Integer) As Integer
        If i < 0 OrElse i >= c.Length Then Return 0
        Dim v As Integer
        If Integer.TryParse(c(i), NumberStyles.Integer, CultureInfo.InvariantCulture, v) Then
            Return v
        End If
        Return 0
    End Function

    Private Shared Function ToDec(c() As String, i As Integer) As Decimal
        If i < 0 OrElse i >= c.Length Then Return 0D
        Dim v As Decimal
        If Decimal.TryParse(c(i), NumberStyles.Any, CultureInfo.InvariantCulture, v) Then
            Return v
        End If
        Return 0D
    End Function

    Private Shared Function ToDate(c() As String, i As Integer) As Date
        If i < 0 OrElse i >= c.Length OrElse String.IsNullOrWhiteSpace(c(i)) Then
            Return Date.MinValue
        End If

        Dim d As Date
        If Date.TryParse(c(i), CultureInfo.InvariantCulture, DateTimeStyles.None, d) Then
            Return d
        End If

        Return Date.MinValue
    End Function

End Class
