Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text
Imports System.IO
Imports VolHedge.DAL
Imports VolHedge.ImportData
Imports System.Threading
Imports System.Data.OleDb
Public Class import_Data

    Dim objTrad As New trading
    Dim ObjAna As New analysisprocess
    Dim Thr_InsertFistDataToDB As New Thread(AddressOf InsertFisFoTrd)
    REM Notis
    Dim Select_NotFoTrd As String = "SELECT   QNotFoTrd.entryno, QNotFoTrd.instrumentname, QNotFoTrd.company, QNotFoTrd.mdate, QNotFoTrd.strikerate, QNotFoTrd.cp, QNotFoTrd.script, QNotFoTrd.Dealer, QNotFoTrd.buysell, QNotFoTrd.Qty, QNotFoTrd.Rate, QNotFoTrd.entrydate, QNotFoTrd.orderno, QNotFoTrd.Dealer1, QNotFoTrd.IsLiq, QNotFoTrd.lActivityTime, QNotFoTrd.entry_date, QNotFoTrd.Tot, QNotFoTrd.FileFlag, Contract.Token AS Tokenno,IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1,QNotFoTrd.[OrgDealer],0 AS token, Format(mdate,'mm/yyyy') AS mo, (IIf(qty=0,1,qty)*rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([qty]<0,[qty],0) AS SaleQty, IIf([qty]>0,[qty]*rate,0) AS BuyVal, IIf([qty]<0,[qty]*rate,0) AS SaleVal   " &
                          " FROM QNotFoTrd INNER JOIN Contract ON QNotFoTrd.script = Contract.script Where 1 = 1 "



    Dim Select_CustomFoTrd As String = "SELECT   " + custFormateName + ".entryno, " + custFormateName + ".instrumentname, " + custFormateName + ".company, " + custFormateName + ".mdate, " + custFormateName + ".strikerate, " + custFormateName + ".cp, " + custFormateName + ".script, " + custFormateName + ".Dealer, " + custFormateName + ".buysell, " + custFormateName + ".Qty, " + custFormateName + ".Rate, " + custFormateName + ".entrydate" &
                        ", " + custFormateName + ".orderno, " + custFormateName + ".Dealer1, " + custFormateName + ".IsLiq, " + custFormateName + ".lActivityTime, " + custFormateName + ".entry_date, " + custFormateName + ".Tot, " + custFormateName + ".FileFlag, Contract.Token AS Tokenno,IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1," + custFormateName + ".[OrgDealer],0 AS token, Format(mdate,'mm/yyyy') AS mo, (IIf(qty=0,1,qty)*rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([qty]<0,[qty],0) AS SaleQty, IIf([qty]>0,[qty]*rate,0) AS BuyVal, IIf([qty]<0,[qty]*rate,0) AS SaleVal   " &
                        " FROM " + custFormateName + " INNER JOIN Contract ON " + custFormateName + ".script = Contract.script Where 1 = 1 "

    Dim Select_CustomcurrTrd As String = "SELECT   " + custFormateName + ".company, " + custFormateName + ".entryno, " + custFormateName + ".buysell, " + custFormateName + ".orderno, " + custFormateName + ".script, " + custFormateName + ".instrumentname, " + custFormateName + ".strikerate, " + custFormateName + ".cp, " + custFormateName + ".mdate, " + custFormateName + ".Dealer, " + custFormateName + ".entrydate, " + custFormateName + ".Qty * multiplier as qty, " + custFormateName + ".Rate, " + custFormateName + ".IsLiq" &
                        ", " + custFormateName + ".lActivityTime," + custFormateName + ".[OrgDealer], " + custFormateName + ".entry_date, (" + custFormateName + ".qty * multiplier)*rate as tot, " + custFormateName + ".FileFlag, Contract.Token AS Tokenno,IIf(([cp]='F'),0,IIf(IsNull([contract].[Token]),0,[contract].[Token])) AS Token1 " &
                        " FROM " + custFormateName + " INNER JOIN Currency_Contract Contract ON " + custFormateName + ".script = Contract.script Where 1 = 1 "




    Dim Select_CustomEQTrd As String = "SELECT    " + custFormateName + ".entryno,  " + custFormateName + ".company,  " + custFormateName + ".cp, " + custFormateName + ".Script, " + custFormateName + ".Dealer, " + custFormateName + ".buysell, " + custFormateName + ".Qty, " + custFormateName + ".Rate, " + custFormateName + ".tot, " + custFormateName + ".entrydate, " + custFormateName + ".orderno, " + custFormateName + ".IsLiq, " + custFormateName + ".lActivityTime, " + custFormateName + ".entry_date, " + custFormateName + ".FileFlag, " + custFormateName + ".eq, Security.token AS tokenno," + custFormateName + ".OrgDealer  " &
                        " FROM " + custFormateName + " INNER JOIN Security ON " + custFormateName + ".Script = Security.script;"




    'Dim Select_NotCurTrd As String = "SELECT QNotCurTrd.entryno, QNotCurTrd.instrumentname, QNotCurTrd.company, QNotCurTrd.mdate, QNotCurTrd.strikerate, QNotCurTrd.cp, QNotCurTrd.script, QNotCurTrd.Dealer, QNotCurTrd.buysell, QNotCurTrd.Qty, QNotCurTrd.Rate, QNotCurTrd.entrydate, QNotCurTrd.orderno, QNotCurTrd.Dealer1, QNotCurTrd.IsLiq, QNotCurTrd.lActivityTime, QNotCurTrd.entry_date, QNotCurTrd.Tot, QNotCurTrd.FileFlag, Currency_Contract.Token AS Tokenno, IIf(([cp]='F'),0,IIf(IsNull([contract].[Token]),0,[contract].[Token])) AS Token1, QNotCurTrd.[OrgDealer]" & _
    '                                    "FROM (QNotCurTrd INNER JOIN Currency_Contract ON QNotCurTrd.script = Currency_Contract.script) LEFT JOIN Contract ON QNotCurTrd.script = Contract.OScript WHERE (((1)=1))"
    Dim Select_NotCurTrd As String = "SELECT QNotCurTrd.entryno, QNotCurTrd.instrumentname, QNotCurTrd.company, QNotCurTrd.mdate, QNotCurTrd.strikerate, QNotCurTrd.cp, QNotCurTrd.script, QNotCurTrd.Dealer, QNotCurTrd.buysell, QNotCurTrd.Qty, QNotCurTrd.Rate, QNotCurTrd.entrydate, QNotCurTrd.orderno, QNotCurTrd.Dealer1, QNotCurTrd.IsLiq, QNotCurTrd.lActivityTime, QNotCurTrd.entry_date, QNotCurTrd.Tot, QNotCurTrd.FileFlag, Currency_Contract.Token AS Tokenno, IIf(([cp]='F'),0,IIf(IsNull([contract].[Token]),0,[contract].[Token])) AS Token1, QNotCurTrd.[OrgDealer]" &
                                        "FROM (QNotCurTrd INNER JOIN Currency_Contract ON QNotCurTrd.script = Currency_Contract.script) LEFT JOIN Contract ON QNotCurTrd.script = Contract.OScript WHERE QNotCurTrd.instrumentname IN('FUTCUR','OPTCUR','FUTIRC','FUTIRT')"

    Dim Select_NotEqTrd As String = "SELECT   QNotEqTrd.entryno, QNotEqTrd.company, QNotEqTrd.cp, QNotEqTrd.Script, QNotEqTrd.Dealer, QNotEqTrd.buysell, QNotEqTrd.Qty, QNotEqTrd.Rate, QNotEqTrd.tot, QNotEqTrd.entrydate, QNotEqTrd.orderno, QNotEqTrd.IsLiq, QNotEqTrd.lActivityTime, QNotEqTrd.entry_date, QNotEqTrd.FileFlag, QNotEqTrd.eq,QNotEqTrd.OrgDealer, Security.token AS tokenno " &
                           " FROM QNotEqTrd INNER JOIN Security ON QNotEqTrd.Script = Security.script Where 1 = 1 "

    REM Fist
    Dim Select_FisFoTrd As String = "SELECT   QFisFoTrd.company, QFisFoTrd.entryno, QFisFoTrd.buysell, QFisFoTrd.orderno, QFisFoTrd.script, QFisFoTrd.instrumentname, QFisFoTrd.strikerate, QFisFoTrd.cp, QFisFoTrd.mdate, QFisFoTrd.Dealer, QFisFoTrd.entrydate, QFisFoTrd.Qty, QFisFoTrd.Rate, QFisFoTrd.IsLiq, QFisFoTrd.lActivityTime, QFisFoTrd.entry_date, QFisFoTrd.Tot,  QFisFoTrd.FileFlag, Contract.Token AS Tokenno, IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1,QFisFoTrd.OrgDealer,0 AS token, Format(QFisFoTrd.mdate,'mm/yyyy') AS mo, (IIf(QFisFoTrd.qty=0,1,QFisFoTrd.qty)*QFisFoTrd.rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([QFisFoTrd.qty]<0,[QFisFoTrd.qty],0) AS SaleQty, IIf([qty]>0,[QFisFoTrd.qty]*QFisFoTrd.rate,0) AS BuyVal, IIf([QFisFoTrd.qty]<0,[QFisFoTrd.qty]*QFisFoTrd.rate,0) AS SaleVal " &
                          " FROM QFisFoTrd INNER JOIN Contract ON QFisFoTrd.script = Contract.script;"

    REM FistEQ
    Dim Select_FisEQTrd As String = "SELECT   QFisEqTrd.entryno, QFisEqTrd.company, QFisEqTrd.cp, QFisEqTrd.Script, QFisEqTrd.Dealer, QFisEqTrd.buysell, QFisEqTrd.Qty, QFisEqTrd.Rate, QFisEqTrd.tot, QFisEqTrd.entrydate, QFisEqTrd.orderno, QFisEqTrd.IsLiq, QFisEqTrd.lActivityTime, QFisEqTrd.entry_date, QFisEqTrd.FileFlag, QFisEqTrd.eq, Security.token AS tokenno,QFisEqTrd.OrgDealer " &
                          " FROM QFisEqTrd INNER JOIN Security ON QFisEqTrd.Script = Security.script;"
    REM Direct Fist:By payal patel
    Dim Select_FisDirectTrd As String = "SELECT   QFistrades.company, QFistrades.entryno, CDbl(QFistrades.buysell) AS buysell, QFistrades.orderno, QFistrades.script, QFistrades.instrumentname, QFistrades.strikerate, QFistrades.cp, QFistrades.mdate, QFistrades.Dealer, QFistrades.entrydate, QFistrades.Qty, QFistrades.Rate, QFistrades.IsLiq, QFistrades.lActivityTime, QFistrades.entry_date, QFistrades.Tot,  QFistrades.FileFlag, Contract.Token AS Tokenno, IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1,QFistrades.OrgDealer,0 AS token, Format(mdate,'mm/yyyy') AS mo, (IIf(qty=0,1,qty)*rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([qty]<0,[qty],0) AS SaleQty, IIf([qty]>0,[qty]*rate,0) AS BuyVal, IIf([qty]<0,[qty]*rate,0) AS SaleVal  " &
                        " FROM QFistrades INNER JOIN Contract ON QFistrades.script = Contract.script;"
    Dim Select_FadFoTrd As String = "SELECT   QFadFoTrd.company, QFadFoTrd.entryno, QFadFoTrd.buysell, QFadFoTrd.orderno, QFadFoTrd.script, QFadFoTrd.instrumentname, QFadFoTrd.strikerate, QFadFoTrd.cp, QFadFoTrd.mdate, QFadFoTrd.Dealer, QFadFoTrd.entrydate, QFadFoTrd.Qty, QFadFoTrd.Rate, QFadFoTrd.IsLiq, QFadFoTrd.lActivityTime, QFadFoTrd.entry_date, QFadFoTrd.Tot,  QFadFoTrd.FileFlag, Contract.Token AS Tokenno, IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1,QFadFoTrd.OrgDealer,0 AS token, Format(mdate,'mm/yyyy') AS mo, (IIf(qty=0,1,qty)*rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([qty]<0,[qty],0) AS SaleQty, IIf([qty]>0,[qty]*rate,0) AS BuyVal, IIf([qty]<0,[qty]*rate,0) AS SaleVal   " &
                          " FROM QFadFoTrd INNER JOIN Contract ON QFadFoTrd.script = Contract.script Where 1 = 1 "

    REM Gets
    Public Select_GetFoTrd As String = "SELECT   QGetFoTrd.company, QGetFoTrd.entryno, QGetFoTrd.buysell, QGetFoTrd.orderno, QGetFoTrd.script, QGetFoTrd.instrumentname, QGetFoTrd.strikerate, QGetFoTrd.cp, QGetFoTrd.mdate, QGetFoTrd.Dealer, QGetFoTrd.entrydate, QGetFoTrd.Qty, QGetFoTrd.Rate, QGetFoTrd.IsLiq, QGetFoTrd.lActivityTime, QGetFoTrd.entry_date, QGetFoTrd.Tot,  QGetFoTrd.FileFlag, Contract.Token AS Tokenno, IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1,QGetFoTrd.OrgDealer,0 AS token, Format(mdate,'mm/yyyy') AS mo, (IIf(qty=0,1,qty)*rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([qty]<0,[qty],0) AS SaleQty, IIf([qty]>0,[qty]*rate,0) AS BuyVal, IIf([qty]<0,[qty]*rate,0) AS SaleVal,Contract.exchange as exchange" &
                          " FROM QGetFoTrd INNER JOIN Contract ON QGetFoTrd.script = Contract.script;"

    'Public Select_GetBseFoTrd As String = "SELECT   qBseGetFoTrd.company, qBseGetFoTrd.entryno, qBseGetFoTrd.buysell, qBseGetFoTrd.orderno, QGetFoTrd.script, QGetFoTrd.instrumentname, QGetFoTrd.strikerate, QGetFoTrd.cp, QGetFoTrd.mdate, QGetFoTrd.Dealer, QGetFoTrd.entrydate, QGetFoTrd.Qty, QGetFoTrd.Rate, QGetFoTrd.IsLiq, QGetFoTrd.lActivityTime, QGetFoTrd.entry_date, QGetFoTrd.Tot,  QGetFoTrd.FileFlag, Contract.Token AS Tokenno, IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1,QGetFoTrd.OrgDealer,0 AS token, Format(mdate,'mm/yyyy') AS mo, (IIf(qty=0,1,qty)*rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([qty]<0,[qty],0) AS SaleQty, IIf([qty]>0,[qty]*rate,0) AS BuyVal, IIf([qty]<0,[qty]*rate,0) AS SaleVal,Contract.exchange as exchange" &
    '                      " FROM QGetFoTrd INNER JOIN Contract ON QGetFoTrd.script = Contract.script;"

    Public Select_GetBseFoTrd As String =
"SELECT " &
" qBseGetFoTrd.company, qBseGetFoTrd.entryno, qBseGetFoTrd.buysell, qBseGetFoTrd.orderno," &
" qBseGetFoTrd.script, qBseGetFoTrd.instrumentname, qBseGetFoTrd.strikerate, qBseGetFoTrd.cp," &
" qBseGetFoTrd.mdate, qBseGetFoTrd.Dealer, qBseGetFoTrd.entrydate, qBseGetFoTrd.Qty," &
" qBseGetFoTrd.Rate, qBseGetFoTrd.IsLiq, qBseGetFoTrd.lActivityTime," &
" qBseGetFoTrd.entry_date, qBseGetFoTrd.Tot, qBseGetFoTrd.FileFlag," &
" Contract.Token AS Tokenno," &
" IIf([Contract].[OScript] <> '', [Contract].[Token], 0) AS Token1," &
" qBseGetFoTrd.OrgDealer, 0 AS token," &
" Format(mdate,'mm/yyyy') AS mo," &
" (IIf(qty=0,1,qty)*rate) AS tot," &
" CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date," &
" (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2," &
" IIf((cp='X' OR cp=''),'F',cp) AS cpf," &
" IIf([qty]>0,[qty],0) AS BuyQty," &
" IIf([qty]<0,[qty],0) AS SaleQty," &
" IIf([qty]>0,[qty]*rate,0) AS BuyVal," &
" IIf([qty]<0,[qty]*rate,0) AS SaleVal," &
" Contract.exchange AS exchange" &
" FROM qBseGetFoTrd INNER JOIN Contract" &
" ON qBseGetFoTrd.script = Contract.script;"


    Public Select_GetCurTrd As String = "SELECT   QGetCurTrd.company, QGetCurTrd.entryno, QGetCurTrd.buysell, QGetCurTrd.orderno, QGetCurTrd.script, QGetCurTrd.instrumentname, QGetCurTrd.strikerate, QGetCurTrd.cp, QGetCurTrd.mdate, QGetCurTrd.Dealer, QGetCurTrd.entrydate, [Q]*[multiplier] AS Qty, QGetCurTrd.Rate, QGetCurTrd.IsLiq, QGetCurTrd.lActivityTime, QGetCurTrd.entry_date, ([Q]*[multiplier])*[Rate] AS Tot, QGetCurTrd.FileFlag, Currency_Contract.Token AS Tokenno, IIf(([cp]='F'),0,IIf(IsNull([contract].[Token]),0,[contract].[Token])) AS Token1,QGetCurTrd.OrgDealer" &
                           " FROM (QGetCurTrd INNER JOIN Currency_Contract ON QGetCurTrd.script = Currency_Contract.script) LEFT JOIN Contract ON QGetCurTrd.script = Contract.OScript Where 1 = 1"

    Public Select_GetEqTrd As String = "SELECT   QGetEqTrd.entryno, QGetEqTrd.company, QGetEqTrd.cp, QGetEqTrd.Script, QGetEqTrd.Dealer, QGetEqTrd.buysell, QGetEqTrd.Qty, QGetEqTrd.Rate, QGetEqTrd.tot, QGetEqTrd.entrydate, QGetEqTrd.orderno, QGetEqTrd.IsLiq, QGetEqTrd.lActivityTime, QGetEqTrd.entry_date, QGetEqTrd.FileFlag, QGetEqTrd.eq, Security.token AS tokenno,QGetEqTrd.OrgDealer,Contract.exchange as exchange " &
                          " FROM QGetEqTrd INNER JOIN Security ON QGetEqTrd.Script = Security.script;"



    Dim Select_BseEqTrd As String = "SELECT  QBseEqTrd.entryno, QBseEqTrd.company, QBseEqTrd.cp, UCase(QBseEqTrd.script) as script, QBseEqTrd.dealer, QBseEqTrd.buysell, QBseEqTrd.Qty, QBseEqTrd.rate, QBseEqTrd.tot, QBseEqTrd.entrydate, QBseEqTrd.orderno, QBseEqTrd.IsLiq, QBseEqTrd.lActivityTime, QBseEqTrd.entry_date, QBseEqTrd.FileFlag, QBseEqTrd.eq, Security.token AS tokenno, QBseEqTrd.OrgDealer " &
                  " FROM QBseEqTrd INNER JOIN Security ON UCase(QBseEqTrd.script) = UCase(Security.script)  Where 1 = 1  "


    REM Now
    Dim Select_NowFoTrd1 As String = "SELECT   * from NowFoTrd"
    Dim Select_NowFoTrd As String = "SELECT   QNowFoTrd.company, QNowFoTrd.entryno, QNowFoTrd.buysell, QNowFoTrd.orderno, QNowFoTrd.script, QNowFoTrd.instrumentname, QNowFoTrd.strikerate, QNowFoTrd.cp, QNowFoTrd.mdate, QNowFoTrd.Dealer, QNowFoTrd.entrydate, QNowFoTrd.Qty, QNowFoTrd.Rate, QNowFoTrd.IsLiq, QNowFoTrd.lActivityTime, QNowFoTrd.entry_date, QNowFoTrd.Tot,  QNowFoTrd.FileFlag, Contract.Token AS Tokenno, IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1,QNowFoTrd.OrgDealer,0 AS token, Format(mdate,'mm/yyyy') AS mo, (IIf(QNowFoTrd.qty=0,1,qty)*rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([qty]<0,[qty],0) AS SaleQty, IIf([qty]>0,[qty]*rate,0) AS BuyVal, IIf([qty]<0,[qty]*rate,0) AS SaleVal   " &
                         " FROM QNowFoTrd INNER JOIN Contract ON QNowFoTrd.script = Contract.script;"
    Dim Select_NowCurTrd As String = "SELECT   QNowCurTrd.company, QNowCurTrd.entryno, QNowCurTrd.buysell, QNowCurTrd.orderno, QNowCurTrd.script, QNowCurTrd.instrumentname, QNowCurTrd.strikerate, QNowCurTrd.cp, QNowCurTrd.mdate, QNowCurTrd.Dealer, QNowCurTrd.entrydate, [Q]*[multiplier] AS Qty, QNowCurTrd.Rate, QNowCurTrd.IsLiq, QNowCurTrd.lActivityTime, QNowCurTrd.entry_date, ([Q]*[multiplier])*[Rate] AS Tot, QNowCurTrd.FileFlag, Currency_Contract.Token AS Tokenno, IIf(([cp]='F'),0,IIf(IsNull([contract].[Token]),0,[contract].[Token])) AS Token1,QNowCurTrd.OrgDealer " &
                          " FROM (QNowCurTrd INNER JOIN Currency_Contract ON QNowCurTrd.script = Currency_Contract.script) LEFT JOIN Contract ON QNowCurTrd.script = Contract.OScript;"
    Dim Select_NowEqTrd As String = "SELECT   QNowEqTrd.entryno, QNowEqTrd.company, QNowEqTrd.cp, QNowEqTrd.Script, QNowEqTrd.Dealer, QNowEqTrd.buysell, QNowEqTrd.Qty, QNowEqTrd.Rate, QNowEqTrd.tot, QNowEqTrd.entrydate, QNowEqTrd.orderno, QNowEqTrd.IsLiq, QNowEqTrd.lActivityTime, QNowEqTrd.entry_date, QNowEqTrd.FileFlag, QNowEqTrd.eq, Security.token AS tokenno,orgdealer " &
                          " FROM QNowEqTrd INNER JOIN Security ON QNowEqTrd.Script = Security.script;"

    REM Odin
    Dim Select_OdiFoTrd As String = "SELECT   QOdiFoTrd.company, QOdiFoTrd.entryno, QOdiFoTrd.buysell, QOdiFoTrd.orderno, QOdiFoTrd.script, QOdiFoTrd.instrumentname, QOdiFoTrd.strikerate, QOdiFoTrd.cp, QOdiFoTrd.mdate, QOdiFoTrd.Dealer, QOdiFoTrd.entrydate, QOdiFoTrd.Qty, QOdiFoTrd.Rate, QOdiFoTrd.IsLiq, QOdiFoTrd.lActivityTime, QOdiFoTrd.entry_date, QOdiFoTrd.Tot,  QOdiFoTrd.FileFlag,QOdiFoTrd.OrgDealer, Contract.Token AS Tokenno, IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1,0 AS token, Format(mdate,'mm/yyyy') AS mo, (IIf(qty=0,1,qty)*rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([qty]<0,[qty],0) AS SaleQty, IIf([qty]>0,[qty]*rate,0) AS BuyVal, IIf([qty]<0,[qty]*rate,0) AS SaleVal   " &
                          " FROM QOdiFoTrd INNER JOIN Contract ON QOdiFoTrd.script = Contract.script;"
    Dim Select_OdiCurTrd As String = "SELECT   QOdiCurTrd.company, QOdiCurTrd.entryno, QOdiCurTrd.buysell, QOdiCurTrd.orderno, QOdiCurTrd.script, QOdiCurTrd.instrumentname, QOdiCurTrd.strikerate, QOdiCurTrd.cp, QOdiCurTrd.mdate, QOdiCurTrd.Dealer, QOdiCurTrd.entrydate, [Q]*[multiplier] AS Qty, QOdiCurTrd.Rate, QOdiCurTrd.IsLiq, QOdiCurTrd.lActivityTime, QOdiCurTrd.entry_date, ([Q]*[multiplier])*[Rate] AS Tot, QOdiCurTrd.FileFlag,QOdiCurTrd.OrgDealer, Currency_Contract.Token AS Tokenno, IIf(([cp]='F'),0,IIf(IsNull([contract].[Token]),0,[contract].[Token])) AS Token1 " &
                          " FROM (QOdiCurTrd INNER JOIN Currency_Contract ON QOdiCurTrd.script = Currency_Contract.script) LEFT JOIN Contract ON QOdiCurTrd.script = Contract.OScript;"
    Dim Select_OdiEqTrd As String = "SELECT   QOdiEqTrd.entryno, QOdiEqTrd.company, QOdiEqTrd.cp, QOdiEqTrd.Script, QOdiEqTrd.Dealer, QOdiEqTrd.buysell, QOdiEqTrd.Qty, QOdiEqTrd.Rate, QOdiEqTrd.tot, QOdiEqTrd.entrydate, QOdiEqTrd.orderno, QOdiEqTrd.IsLiq, QOdiEqTrd.lActivityTime, QOdiEqTrd.entry_date, QOdiEqTrd.FileFlag, QOdiEqTrd.eq, Security.token AS tokenno,QOdiEqTrd.OrgDealer  " &
                          " FROM QOdiEqTrd INNER JOIN Security ON QOdiEqTrd.Script = Security.script;"

    REM Odim MCX
    Dim Select_OdiMcxCurTrd As String = "SELECT   QMOdiCurTrd.company, QMOdiCurTrd.entryno, QMOdiCurTrd.buysell, QMOdiCurTrd.orderno, QMOdiCurTrd.script, QMOdiCurTrd.instrumentname, QMOdiCurTrd.strikerate, QMOdiCurTrd.cp, QMOdiCurTrd.mdate, QMOdiCurTrd.Dealer, QMOdiCurTrd.entrydate, [Q]*[multiplier] AS Qty, QMOdiCurTrd.Rate, QMOdiCurTrd.IsLiq, QMOdiCurTrd.lActivityTime, QMOdiCurTrd.entry_date, ([Q]*[multiplier])*[Rate] AS Tot, QMOdiCurTrd.FileFlag,QMOdiCurTrd.OrgDealer, Currency_Contract.Token AS Tokenno, IIf(([cp]='F'),0,IIf(IsNull([contract].[Token]),0,[contract].[Token])) AS Token1 " &
                          " FROM (QMOdiCurTrd INNER JOIN Currency_Contract ON QMOdiCurTrd.script = Currency_Contract.script) LEFT JOIN Contract ON QMOdiCurTrd.script = Contract.OScript;"

    REM Nse
    Dim Select_NseFoTrd As String = "SELECT   QNseFoTrd.company, QNseFoTrd.entryno, QNseFoTrd.buysell, QNseFoTrd.orderno, QNseFoTrd.script, QNseFoTrd.instrumentname, QNseFoTrd.strikerate, QNseFoTrd.cp, QNseFoTrd.mdate, QNseFoTrd.Dealer, QNseFoTrd.entrydate, QNseFoTrd.Qty, QNseFoTrd.Rate, QNseFoTrd.IsLiq, QNseFoTrd.lActivityTime, QNseFoTrd.entry_date, QNseFoTrd.Tot,  QNseFoTrd.FileFlag,QNseFoTrd.OrgDealer, Contract.Token AS Tokenno, IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1,0 AS token, Format(mdate,'mm/yyyy') AS mo, (IIf(qty=0,1,qty)*rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([qty]<0,[qty],0) AS SaleQty, IIf([qty]>0,[qty]*rate,0) AS BuyVal, IIf([qty]<0,[qty]*rate,0) AS SaleVal   " &
                         " FROM QNseFoTrd INNER JOIN Contract ON QNseFoTrd.script = Contract.script;"
    Dim Select_NseCurTrd As String = "SELECT   QNseCurTrd.company, QNseCurTrd.entryno, QNseCurTrd.buysell, QNseCurTrd.orderno, QNseCurTrd.script, QNseCurTrd.instrumentname, QNseCurTrd.strikerate, QNseCurTrd.cp, QNseCurTrd.mdate, QNseCurTrd.Dealer, QNseCurTrd.entrydate, [Q]*[multiplier] AS Qty, QNseCurTrd.Rate, QNseCurTrd.IsLiq, QNseCurTrd.lActivityTime, QNseCurTrd.entry_date, ([Q]*[multiplier])*[Rate] AS Tot, QNseCurTrd.FileFlag,QNseCurTrd.OrgDealer, Currency_Contract.Token AS Tokenno, IIf(([cp]='F'),0,IIf(IsNull([contract].[Token]),0,[contract].[Token])) AS Token1 " &
                         " FROM (QNseCurTrd INNER JOIN Currency_Contract ON QNseCurTrd.script = Currency_Contract.script) LEFT JOIN Contract ON QNseCurTrd.script = Contract.OScript;"

    REM Neat
    Dim Select_NeaFoTrd As String = "SELECT   QNeaFoTrd.company, QNeaFoTrd.entryno, QNeaFoTrd.buysell, QNeaFoTrd.orderno, QNeaFoTrd.script, QNeaFoTrd.instrumentname, QNeaFoTrd.strikerate, QNeaFoTrd.cp, QNeaFoTrd.mdate, QNeaFoTrd.Dealer, QNeaFoTrd.entrydate, QNeaFoTrd.Qty, QNeaFoTrd.Rate, QNeaFoTrd.IsLiq, QNeaFoTrd.lActivityTime, QNeaFoTrd.entry_date, QNeaFoTrd.Tot,  QNeaFoTrd.FileFlag,QNeaFoTrd.OrgDealer, Contract.Token AS Tokenno, IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1,0 AS token, Format(mdate,'mm/yyyy') AS mo, (IIf(qty=0,1,qty)*rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([qty]<0,[qty],0) AS SaleQty, IIf([qty]>0,[qty]*rate,0) AS BuyVal, IIf([qty]<0,[qty]*rate,0) AS SaleVal  " &
                         " FROM QNeaFoTrd INNER JOIN Contract ON QNeaFoTrd.script = Contract.script Where 1 = 1 "
    Dim Select_NeaCurTrd As String = "SELECT   QNeaCurTrd.company, QNeaCurTrd.entryno, QNeaCurTrd.buysell, QNeaCurTrd.orderno, QNeaCurTrd.script, QNeaCurTrd.instrumentname, QNeaCurTrd.strikerate, QNeaCurTrd.cp, QNeaCurTrd.mdate, QNeaCurTrd.Dealer, QNeaCurTrd.entrydate, [Q]*[multiplier] AS Qty, QNeaCurTrd.Rate, QNeaCurTrd.IsLiq, QNeaCurTrd.lActivityTime, QNeaCurTrd.entry_date, ([Q]*[multiplier])*[Rate] AS Tot, QNeaCurTrd.FileFlag,QNeaCurTrd.OrgDealer, Currency_Contract.Token AS Tokenno, IIf(([cp]='F'),0,IIf(IsNull([contract].[Token]),0,[contract].[Token])) AS Token1  " &
                         " FROM (QNeaCurTrd INNER JOIN Currency_Contract ON QNeaCurTrd.script = Currency_Contract.script) LEFT JOIN Contract ON QNeaCurTrd.script = Contract.OScript Where 1 = 1 "

    'Public Sub New()

    'End Sub

    Public Function SelectdataofDealer() As DataTable
        Try
            Dim Query As String = "SELECT tl.cGreekClientId AS dealer, t.lNseBseToken AS tokanno, t.cSymbol AS company, " &
            "t.cSecurityDesc AS script, t.dStrikePrice AS strikes, SUM(tl.lFillQuantity) AS units, " &
            "ABS(SUM(tl.dFillPrice * (CASE tl.iBuySell WHEN 1 THEN -tl.lFillQuantity ELSE tl.lFillQuantity END))/SUM(CASE tl.iBuySell WHEN 1 THEN -tl.lFillQuantity ELSE tl.lFillQuantity END)) AS netprice, " &
            "SUM(tl.lFillQuantity)*ABS(SUM(tl.dFillPrice * (CASE tl.iBuySell WHEN 1 THEN -tl.lFillQuantity ELSE tl.lFillQuantity END))/SUM(CASE tl.iBuySell WHEN 1 THEN -tl.lFillQuantity ELSE tl.lFillQuantity END)) AS netvalue, " &
            "t.lExpiryDt AS mdate, t.tokan1, " &
            "CASE ISNULL(SUBSTRING(t.cOptionType,1,1),'E') WHEN 'X' THEN 'F' ELSE ISNULL(SUBSTRING(t.cOptionType,1,1),'E') END AS CP " &
            "FROM (SELECT *,0 AS tokan1 FROM SecurityEQMaster " &
            "UNION " &
            "SELECT *, CASE SUBSTRING(cfom.cOptionType,1,1) " &
            "WHEN 'C' THEN (SELECT lNseBseToken FROM ContractFOMaster WHERE cSecurityDesc = SUBSTRING(cfom.cSecurityDesc,1,len(cfom.cSecurityDesc)-2)+'P'+SUBSTRING(cfom.cSecurityDesc,LEN(cfom.cSecurityDesc),1) AND SUBSTRING(cOptionType,1,1) = 'P') " &
            "WHEN 'P' THEN (SELECT lNseBseToken FROM ContractFOMaster WHERE cSecurityDesc = SUBSTRING(cfom.cSecurityDesc,1,len(cfom.cSecurityDesc)-2)+'C'+SUBSTRING(cfom.cSecurityDesc,LEN(cfom.cSecurityDesc),1) AND SUBSTRING(cOptionType,1,1) = 'C') " &
            "ELSE 0 END AS tokan1 " &
            "FROM ContractFOMaster cfom)t,tradelog tl " &
            "WHERE tl.lOurToken=t.lOurToken " &
            "GROUP BY tl.cGreekClientId,t.lNseBseToken,t.cSymbol,t.cSecurityDesc,t.dStrikePrice,t.cOptionType,t.lExpiryDt,t.tokan1 " &
            "HAVING SUM(CASE tl.iBuySell WHEN 1 THEN -tl.lFillQuantity ELSE tl.lFillQuantity END) <> 0 " &
            "ORDER BY tl.cGreekClientId"
            data_access_sql.Cmd_Text = Query
            Return data_access_sql.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function SelectdataofScript() As DataTable
        Try
            Dim Query As String = "SELECT t.cSecurityDesc AS script, t.dStrikePrice AS strikes, " &
            "CASE ISNULL(SUBSTRING(t.cOptionType,1,1),'E') WHEN 'X' THEN 'F' ELSE ISNULL(SUBSTRING(t.cOptionType,1,1),'E') END AS CP, " &
            "t.lNseBseToken AS tokanno, t.tokan1, t.lExpiryDt AS mdate " &
            "FROM (SELECT *,0 AS tokan1 FROM SecurityEQMaster " &
            "UNION " &
            "SELECT *, Case SUBSTRING(cfom.cOptionType, 1, 1) " &
            "WHEN 'C' THEN (SELECT lNseBseToken FROM ContractFOMaster WHERE cSecurityDesc = SUBSTRING(cfom.cSecurityDesc,1,len(cfom.cSecurityDesc)-2)+'P'+SUBSTRING(cfom.cSecurityDesc,LEN(cfom.cSecurityDesc),1) AND SUBSTRING(cOptionType,1,1) = 'P') " &
            "WHEN 'P' THEN (SELECT lNseBseToken FROM ContractFOMaster WHERE cSecurityDesc = SUBSTRING(cfom.cSecurityDesc,1,len(cfom.cSecurityDesc)-2)+'C'+SUBSTRING(cfom.cSecurityDesc,LEN(cfom.cSecurityDesc),1) AND SUBSTRING(cOptionType,1,1) = 'C') " &
            "ELSE 0 END AS tokan1 " &
            "FROM ContractFOMaster cfom)t,TradeLog tl " &
            "WHERE t.lOurToken=tl.lOurToken GROUP BY t.cSecurityDesc,t.dStrikePrice,t.cOptionType,t.lNseBseToken,t.tokan1,t.lExpiryDt " &
            "ORDER BY t.cSecurityDesc "
            data_access_sql.Cmd_Text = Query
            Return data_access_sql.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function SelectContracttokenno() As DataTable
        Try
            Dim Query As String = "SELECT lNseBseToken AS tokenno FROM contractFOMaster WHERE cOptionType in ('XX','',null)"
            data_access_sql.Cmd_Text = Query
            Return data_access_sql.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function SelectSecuritytokenno() As DataTable
        Try
            Dim Query As String = "SELECT lNseBseToken AS tokenno FROM SecurityEQMaster"
            data_access_sql.Cmd_Text = Query
            Return data_access_sql.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function Selecttradelog(ByVal dealer As String, Optional ByVal lActivityTime As Long = 0) As DataTable
        Try
            Dim Query As String '= "SELECT * FROM TradeLog " & IIf(lActivityTime = 0, String.Empty, "WHERE lActivityTime > " & lActivityTime)

            Query = "SELECT t.lNseBseToken AS tokanno, " &
            "tl.iBuySell AS buysell, tl.lFillQuantity AS unit ,tl.dFillPrice AS rate, " &
            "tl.dFillPrice AS netvalue, tl.lActivityTime, " &
            "lFillNumber AS entryno,dResponseOrderNumber AS orderno " &
            "FROM (SELECT lOurToken,lNseBseToken FROM SecurityEQMaster " &
            "UNION SELECT lOurToken,lNseBseToken FROM ContractFOMaster) t, tradelog tl " &
            "WHERE tl.lOurToken = t.lOurToken AND tl.lActivityTime >= " & lActivityTime.ToString() &
            "and tl.cGreekClientId IN (" & dealer & ") ORDER BY tl.lActivityTime"

            '"and tl.cGreekClientId='" & dealer & "' ORDER BY tl.lActivityTime"

            data_access_sql.Cmd_Text = Query
            Return data_access_sql.FillList()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            Return Nothing
        End Try
    End Function

    '''<summary>
    ''' BY Viral 09-July-11
    ''' Converts a given delimited file into a DataTable. 
    ''' Assumes that the first line    
    ''' of the text file contains the column names.
    ''' </summary>
    ''' <param name="File">The name of the file to open</param>    
    ''' <param name="TableName">The name of the Table to be made within the DataSet returned</param>
    ''' <param name="delimiter">The string to delimit by</param>
    ''' <returns></returns>  
    Public Shared Function ToConvertDataTable(ByVal File As String, ByVal TableName As String, ByVal delimiter As String, Optional ByVal isFirstRowSkip As Boolean = True) As DataTable
        Dim result As New DataSet
        Try
            ''The DataSet to Return

            ''Open the file in a stream reader.
            Dim s As StreamReader
            s = New StreamReader(File)
            Dim columns As String()
            ''Split the first line into the columns       
            If isFirstRowSkip = True Then
                Dim Tcolumns As String() = s.ReadLine().Split(delimiter.ToCharArray())
                columns = s.ReadLine().Split(delimiter.ToCharArray())
            Else
                columns = s.ReadLine().Split(delimiter.ToCharArray())
            End If

            ''Add the new DataTable to the RecordSet
            result.Tables.Add(TableName)

            ''Cycle the colums, adding those that don't exist yet 
            ''and sequencing the one that do.
            For Each col As String In columns
                Dim added As Boolean = False
                Dim nxt As String = ""
                Dim i As Integer = 0
                While (added = False)
                    ''Build the column name and remove any unwanted characters.
                    Dim columnname As String = col + nxt
                    columnname = columnname.Replace("#", "")
                    columnname = columnname.Replace("'", "")
                    columnname = columnname.Replace("&", "")

                    ''See if the column already exists
                    If (result.Tables(TableName).Columns.Contains(columnname) = False) Then
                        ''if it doesn't then we add it here and mark it as added
                        result.Tables(TableName).Columns.Add(columnname)
                        added = True
                    Else
                        ''if it did exist then we increment the sequencer and try again.
                        i = i + 1
                        nxt = "_" + i.ToString()
                    End If
                End While
            Next
            ''Read the rest of the data in the file.        

            Dim AllData As String = s.ReadToEnd()
            ''Split off each row at the Carriage Return/Line Feed
            ''Default line ending in most windows exports.  
            ''You may have to edit this to match your particular file.
            ''This will work for Excel, Access, etc. default exports.
            'AllData.Replace(Chr("13"), "`")
            'Dim rows As String() = AllData.Split("\r\n".ToCharArray())
            Dim rows As String() = AllData.Split(Chr(13), Chr(10))
            ''Now add each row to the DataSet        
            For Each r As String In rows
                ''Split the row at the delimiter.
                If r.Length > 2 Then
                    Dim items As String() = r.Split(delimiter.ToCharArray())
                    ''Add the item
                    result.Tables(TableName).Rows.Add(items)
                End If
            Next
            ''Return the imported data.        
            'Dim asd As String()
            'SaveBCToExl(result.Tables(TableName), Left(File, Len(File) - 3) & "xls", asd, "other")
            'grd(0).DataSource = result.Tables(TableName)
            'exporttoHtml(grd, Left(File, Len(File) - 3) & "xls", asd, "other")
            Return result.Tables(TableName)


        Catch ex As Exception
            MsgBox("File Not Convert To Data..")
            Return result.Tables(0)
        End Try
    End Function

    ''' <summary>
    ''' ValidateTxtFile it use To Check File if it is Exist on location Or Not 
    ''' Or It is in Required Format And Return Flag
    ''' </summary>
    ''' <param name="FilePath"></param>
    ''' <param name="Var"></param>
    ''' <returns>Return Type Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function ValidateTxtFile(ByVal FilePath As String, ByVal Var As String) As Boolean
        REM Check File exist from Path
        Try
            If File.Exists(FilePath) = False Then
                'Throw New ApplicationException("Following file path " & FilePath & " Not Found !!")
                MsgBox("Following file path " & FilePath & " Not Found !!", MsgBoxStyle.Exclamation)
                Return False
                Exit Function
            End If
        Catch ex As Exception
            MsgBox("Following file path " & FilePath & " Not Found !!", MsgBoxStyle.Exclamation)
            Return False
            Exit Function
        End Try

        Try
            Dim RStream As New System.IO.StreamReader(FilePath)

            Dim Str As String
            Str = RStream.ReadLine()
            'Write_TimeLog1("ValidateTxtFile() str:" + Str)
            If Str Is Nothing Or Str = "" Then
                MsgBox("Traded File Type Miss-Match !!!", MsgBoxStyle.Critical, Var)
                RStream.Close()
                Return False
                Exit Function
            End If
            REM This block check whether trade file format Miss-match or not
            Dim StrData1 As String() = Split(Str, ",")
            If Var = "FO" Then
                If IsNumeric(StrData1(7)) = True Then
                    MsgBox(FilePath & " Traded File Type Miss-Match !!!", MsgBoxStyle.Critical, Var)
                    RStream.Close()
                    Return False
                    Exit Function
                End If
            ElseIf Var = "EQ" Then
                If Not IsNumeric(StrData1(7)) = True Then
                    MsgBox(FilePath & " Traded File Type Miss-Match !!!", MsgBoxStyle.Critical, Var)
                    RStream.Close()
                    Return False
                    Exit Function
                End If
            ElseIf Var = "CURRENCY" Then
                If FilePath.Contains("MCX") Then
                    If IsNumeric(StrData1(6)) = True Then
                        MsgBox(FilePath & " Traded File Type Miss-Match !!!", MsgBoxStyle.Critical, Var)
                        RStream.Close()
                        Return False
                        Exit Function
                    End If
                Else
                    If IsNumeric(StrData1(7)) = True Then
                        MsgBox(FilePath & " Traded File Type Miss-Match !!!", MsgBoxStyle.Critical, Var)
                        RStream.Close()
                        Return False
                        Exit Function
                    End If
                End If
            End If
            RStream.Close()
            Return True
        Catch ex As Exception
            Write_TimeLog1("ValidateTxtFile() Error:" + ex.Message)
            MsgBox("Invalid File Format." & vbCrLf & " Traded File Type Miss-Match !!!", MsgBoxStyle.Exclamation)
            Return False
        End Try

    End Function
    ''' <summary>
    ''' CopyToData() 
    ''' This Function To Copy File (e.g Contract.txt || Bhavcopy.csv || TradeFile.txt) To C:\Data Folder
    ''' By Viral
    ''' </summary>
    ''' <param name="sFile">
    ''' FilePath 
    ''' </param>
    ''' <param name="fType">
    ''' File Type Like (e.g GETS FO || GETS EQ || GETS CURRENCY || CONTRACT || BHAVCOPY)
    ''' </param>
    ''' <remarks></remarks>
    Public Shared Sub CopyToData(ByVal sFile As String, ByVal fType As String)
        Dim tik As Long = System.Environment.TickCount
        Try
            Dim VarFileName As String = ""
            Select Case fType
                Case "Custom Fo"
                    VarFileName = "CustomFoTrd.txt" ' frmTradefilesetting.FormateName + ".txt"
                Case "BSE EQ"
                    VarFileName = "BseEqTrd.csv"
                    '=======================================================
                Case "NEAT FO"
                    VarFileName = "NeaFoTrd.txt"
                Case "NEAT CURRENCY"
                    VarFileName = "NeaCurTrd.txt"
                    '===================================================
                Case "FIST FO"
                    VarFileName = "FisFoTrd.txt"
                Case "FIST EQ"
                    VarFileName = "FisEqTrd.txt"
                Case "FADM FO"
                    VarFileName = "FadFoTrd.txt"
                    '===================================================
                Case "BSE GETS FO"
                    VarFileName = "BseGetFoTrd.txt"
                Case "GETS FO"
                    VarFileName = "GetFoTrd.txt"
                Case "GETS EQ"
                    VarFileName = "GetEqTrd.txt"
                Case "GETS CURRENCY"
                    VarFileName = "GetCurTrd.txt"
                    '==================================================
                Case "ODIN FO"
                    VarFileName = "OdiFoTrd.txt"
                Case "ODIN EQ"
                    VarFileName = "OdiEqTrd.txt"
                Case "ODIN CURRENCY"
                    VarFileName = "OdiCurTrd.txt"
                    '==================================================
                Case "ODIN MCX CURRENCY"
                    VarFileName = "MOdiCurTrd.txt"
                    '==================================================
                Case "NOW FO"
                    VarFileName = "NowFoTrd.txt"
                Case "NOW EQ"
                    VarFileName = "NowEqTrd.txt"
                Case "NOW CURRENCY"
                    VarFileName = "NowCurTrd.txt"
                    '==================================================
                Case "NOTICE FO"
                    VarFileName = "NotFoTrd.txt"
                Case "NOTICE EQ"
                    VarFileName = "NotEqTrd.txt"
                Case "NOTICE CURRENCY"
                    VarFileName = "NotCurTrd.txt"
                    '==================================================   
                Case "NSE FO"
                    VarFileName = "NseFoTrd.txt"
                Case "NSE EQ"
                    VarFileName = "NseEqTrd.txt"
                Case "NSE CURRENCY"
                    VarFileName = "NseCurTrd.txt"
                    '==================================================
                Case "CONTRACT"
                    If FLGCSVCONTRACT = True Then
                        VarFileName = "contract.csv"
                    Else
                        VarFileName = "contract.txt"
                    End If


                Case "CD_CONTRACT"
                    If FLGCSVCONTRACT = True Then
                        VarFileName = "cd_contractcsv.csv"
                    Else
                        VarFileName = "cd_contract.txt"
                    End If
                    'VarFileName = "cd_contract.txt"
                Case "SECURITY"
                    If FLGCSVCONTRACT = True Then
                        VarFileName = "Securitycsv.csv"
                    Else
                        VarFileName = "Security.txt"
                    End If

                Case "BHAVCOPY"
                    VarFileName = "Bhavcopy.txt"
                Case "BHAVCOPYNEW"
                    VarFileName = "Bhavcopyfo.txt"
            End Select
            'FileSystem.Kill("C:\Data\" & VarFileName)
            If verVersion = "MI" Then


                If INSTANCEname = "PRIMARY" Then
                    File.Copy(sFile, "C:\Data\" & VarFileName, True)
                ElseIf INSTANCEname = "SECONDARY" Then
                    File.Copy(sFile, "C:\Data2\" & VarFileName, True)
                ElseIf INSTANCEname = "THIRD" Then
                    File.Copy(sFile, "C:\Data3\" & VarFileName, True)
                ElseIf INSTANCEname = "FOURTH" Then
                    File.Copy(sFile, "C:\Data4\" & VarFileName, True)
                ElseIf INSTANCEname = "FIFTH" Then
                    File.Copy(sFile, "C:\Data5\" & VarFileName, True)
                ElseIf INSTANCEname = "SIXTH" Then
                    File.Copy(sFile, "C:\Data6\" & VarFileName, True)
                ElseIf INSTANCEname = "SEVENTH" Then
                    File.Copy(sFile, "C:\Data7\" & VarFileName, True)
                ElseIf INSTANCEname = "EIGHT" Then
                    File.Copy(sFile, "C:\Data8\" & VarFileName, True)
                End If
            Else

                Dim str As String = "C:\Data\" & VarFileName
                '  Dim filePath As String = CUtils.ToUNC(sFile)

                File.Copy(sFile, str, True)
                'File.Copy(filePath, str, True)

            End If
            Write_TimeLog1("CopyToData:" + VarFileName)
        Catch ex As Exception
            MsgBox("Error in Copy File to Data Folder.", MsgBoxStyle.Information)
        End Try
        Write_TradeLog_viral("CopyToData", tik, System.Environment.TickCount)
    End Sub

    '''<summary>
    ''' BY Viral 09-July-11
    ''' first check LWT of File Then
    ''' Copy Contract File To Data
    ''' import data From Linktable(importdb.mdb) in To Contract(greek.mdb) table  
    ''' </summary>
    ''' <param name="sFile">The name of the file to open</param>    
    ''' <returns></returns>  
    Public Shared Function ContractImport(ByVal sFile As String, ByVal objIO As ImportOperation, Optional ByVal CheckDate As Boolean = False) As Boolean

        If CheckDate = True Then
            Dim ObjTrad As New trading
            Dim sLWT As String = ""
            Try
                If flgimportContract = True Then
                    Call CopyToData(sFile, "CONTRACT")
                    Call objIO.ImportContract()
                    Dim DR As DataRow = GdtSettings.Select("SettingName='CONTRACT_LWT'")(0)
                    ObjTrad.SettingName = "CONTRACT_LWT"
                    ObjTrad.SettingKey = sLWT
                    ObjTrad.Uid = CInt(DR("uid"))
                    ObjTrad.Update_setting()
                    ObjTrad.Update_NewToken()
                    ObjTrad.Update_NewToken()
                    Return True
                Else
                    sLWT = Format(File.GetLastWriteTime(sFile), "yyyyMMddHHmmss")
                    If Val(sLWT) > Val(CONTRACT_LWT) Then
                        Call CopyToData(sFile, "CONTRACT")
                        Call objIO.ImportContract()
                        Dim DR As DataRow = GdtSettings.Select("SettingName='CONTRACT_LWT'")(0)
                        ObjTrad.SettingName = "CONTRACT_LWT"
                        ObjTrad.SettingKey = sLWT
                        ObjTrad.Uid = CInt(DR("uid"))
                        ObjTrad.Update_setting()
                        ObjTrad.Update_NewToken()
                        Return True
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
                Return False
            End Try
        Else
            Try
                Dim ObjTrad As New trading
                Call CopyToData(sFile, "CONTRACT")
                Call objIO.ImportContract()
                ObjTrad.Update_NewToken()
                Return True
            Catch ex As Exception
                MsgBox("Invalid Data..")
                Return False
            End Try
        End If
    End Function
    '''<summary>
    ''' BY Viral 09-July-11
    ''' first check LWT of File Then
    ''' Copy Security File To Data
    ''' import data From Linktable(importdb.mdb) in To Security(greek.mdb) table  
    ''' </summary>
    ''' <param name="sFile">The name of the file to open</param>    
    ''' <returns></returns>  
    Public Shared Function SecurityImport(ByVal sFile As String, ByVal objIO As ImportOperation, Optional ByVal CheckDate As Boolean = False) As Boolean
        If CheckDate = True Then
            Dim ObjTrad As New trading
            Dim sLWT As String = ""
            Try
                If flgimportContract = True Then

                    Call CopyToData(sFile, "SECURITY")
                    Call objIO.ImportSecurity()
                    Dim DR As DataRow = GdtSettings.Select("SettingName='SECURITY_LWT'")(0)
                    ObjTrad.SettingName = "SECURITY_LWT"
                    ObjTrad.SettingKey = sLWT
                    ObjTrad.Uid = CInt(DR("uid"))
                    ObjTrad.Update_setting()
                    Return True

                Else
                    sLWT = Format(File.GetLastWriteTime(sFile), "yyyyMMddHHmmss")
                    If Val(sLWT) > Val(SECURITY_LWT) Then
                        Call CopyToData(sFile, "SECURITY")
                        Call objIO.ImportSecurity()
                        Dim DR As DataRow = GdtSettings.Select("SettingName='SECURITY_LWT'")(0)
                        ObjTrad.SettingName = "SECURITY_LWT"
                        ObjTrad.SettingKey = sLWT
                        ObjTrad.Uid = CInt(DR("uid"))
                        ObjTrad.Update_setting()
                        Return True
                    End If
                End If

            Catch ex As Exception
                MsgBox(ex.ToString)
                Return False
            End Try
        Else
            Try
                Call CopyToData(sFile, "SECURITY")
                Call objIO.ImportSecurity()
                Return True
            Catch ex As Exception
                MsgBox("Invalid Data..")
                Return False
            End Try
        End If
    End Function
    '''<summary>
    ''' BY Viral 09-July-11
    ''' first check LWT of File Then
    ''' Copy CD_Contract File To Data
    ''' import data From Linktable(importdb.mdb) in To Currency_Contract(greek.mdb) table  
    ''' </summary>
    ''' <param name="sFile">The name of the file to open</param>    
    ''' <returns></returns>  
    Public Shared Function CurrencyImport(ByVal sFile As String, ByVal objIO As ImportOperation, Optional ByVal CheckDate As Boolean = False) As Boolean
        If CheckDate = True Then
            Dim ObjTrad As New trading
            Dim sLWT As String = ""
            Try
                If flgimportContract = True Then
                    Call CopyToData(sFile, "CD_CONTRACT")
                    Call objIO.ImportCurrency()
                    Dim DR As DataRow = GdtSettings.Select("SettingName='CURR_CONTRACT_LWT'")(0)
                    ObjTrad.SettingName = "CURR_CONTRACT_LWT"
                    ObjTrad.SettingKey = sLWT
                    ObjTrad.Uid = CInt(DR("uid"))
                    ObjTrad.Update_setting()
                    Return True
                Else
                    sLWT = Format(File.GetLastWriteTime(sFile), "yyyyMMddHHmmss")
                    If Val(sLWT) > Val(CURR_CONTRACT_LWT) Then
                        Call CopyToData(sFile, "CD_CONTRACT")
                        Call objIO.ImportCurrency()
                        Dim DR As DataRow = GdtSettings.Select("SettingName='CURR_CONTRACT_LWT'")(0)
                        ObjTrad.SettingName = "CURR_CONTRACT_LWT"
                        ObjTrad.SettingKey = sLWT
                        ObjTrad.Uid = CInt(DR("uid"))
                        ObjTrad.Update_setting()
                        Return True
                    End If
                End If

            Catch ex As Exception
                MsgBox(ex.ToString)
                Return False
            End Try
        Else
            Try
                Call CopyToData(sFile, "CD_CONTRACT")
                Call objIO.ImportCurrency()
                Return True
            Catch ex As Exception
                MsgBox("Invalid Data..")
                Return False
            End Try
        End If
    End Function

    ''' <summary>
    ''' AutoFillCont is Check Flag in Setting For Auto import Contrtact
    ''' By Viral
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns> Return Boolean Value</returns>
    ''' <remarks></remarks>
    Public Shared Function AutoFillCont(ByRef str As String) As Boolean
        Try
            AutoFillCont = Boolean.Parse(GdtSettings.Compute("Max(SettingKey)", "SettingName='AUTO CONTRACT REFRESH'"))
            If AutoFillCont = True Then
                str = GdtSettings.Compute("Max(SettingKey)", "SettingName='CONTRACT FILE PATH'").ToString & "\" & GdtSettings.Compute("Max(SettingKey)", "SettingName='CONTRACT FILE NAME'").ToString
                If File.Exists(str) Then
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            MsgBox("Error in geting Setting of Autofill Contract information " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function
    ''' <summary>
    ''' AutoFillSec is Check Flag in Setting For Auto import Security
    ''' By Viral
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AutoFillSec(ByRef str As String) As Boolean
        Try
            AutoFillSec = Boolean.Parse(GdtSettings.Compute("Max(SettingKey)", "SettingName='AUTO SECURITY REFRESH'"))
            If AutoFillSec = True Then
                str = GdtSettings.Compute("Max(SettingKey)", "SettingName='SECURITY FILE PATH'").ToString & "\" & GdtSettings.Compute("Max(SettingKey)", "SettingName='SECURITY FILE NAME'").ToString
                If File.Exists(str) Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            MsgBox("Error in geting Setting of Autofill Security information" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function

    Public Shared Function AutoFillBhavCopy(ByRef str As String) As Boolean
        Try
            If IsDBNull(GdtSettings.Compute("Max(SettingKey)", "SettingName='AUTO_BHAVCOPY_REFRESH'")) Then
                Return False
            End If

            AutoFillBhavCopy = Boolean.Parse(GdtSettings.Compute("Max(SettingKey)", "SettingName='AUTO_BHAVCOPY_REFRESH'"))

            If AutoFillBhavCopy = True Then
                Dim StrFile As String

                str = GdtSettings.Compute("Max(SettingKey)", "SettingName='BHAVCOPY_FILE_PATH'").ToString
                '& "\" & GdtSettings.Compute("Max(SettingKey)", "SettingName='BHAVCOPY_FILE_NAME'").ToString
                StrFile = get_latest_bhavcopy_file(str)

                str = str & "\" & StrFile

                Dim objBh As New bhav_copy
                Dim dt As DataTable

                GdtBhavcopy = objBh.select_data()
                If (GdtBhavcopy.Rows.Count > 0) Then
                    dt = New DataView(GdtBhavcopy, "", "entry_date ASC", DataViewRowState.CurrentRows).ToTable(True, "entry_date")

                    Dim entrydate As Date = CDate(dt.Rows(0)(0))
                    Dim current As Date = CDate(Mid(StrFile, 3, 2) & Format(Mid(StrFile, 5, 3)) & Mid(StrFile, 8, 4))

                    If Not IsDBNull(dt.Compute("Max(entry_date)", "entry_date = '" & current & "' ")) Then
                        Return False
                    ElseIf File.Exists(str) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    If File.Exists(str) Then
                        Return True
                    Else
                        Return False
                    End If
                End If

            End If

        Catch ex As Exception
            MsgBox("Error in geting Setting of Autofill BhavCopy information " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function
    'Public Shared Function AutoFillAnalysis(ByRef str As String) As Boolean
    '    Try
    '        AutoFillAnalysis = Boolean.Parse(GdtSettings.Compute("Max(SettingKey)", "SettingName='AUTO SECURITY REFRESH'"))
    '        If AutoFillAnalysis = True Then
    '            str = GdtSettings.Compute("Max(SettingKey)", "SettingName='SECURITY FILE PATH'").ToString & "\" & GdtSettings.Compute("Max(SettingKey)", "SettingName='SECURITY FILE NAME'").ToString
    '        End If
    '    Catch ex As Exception
    '        MsgBox("Error in geting Setting of Autofill Security information" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
    '    End Try
    'End Function
    ''' <summary>
    ''' AutoFillCurr is Check Flag in Setting For Auto import Currency_Contrtact
    ''' By Viral
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AutoFillCurr(ByRef str As String) As Boolean
        Try
            AutoFillCurr = Boolean.Parse(GdtSettings.Compute("Max(SettingKey)", "SettingName='AUTO CURRENCY CONTRACT REFRESH'"))
            If AutoFillCurr = True Then
                str = GdtSettings.Compute("Max(SettingKey)", "SettingName='CURRECNY CONTRACT PATH'").ToString & "\" & GdtSettings.Compute("Max(SettingKey)", "SettingName='CURRECNY CONTRACT NAME'").ToString
                If File.Exists(str) Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            MsgBox("Error in geting Setting of Autofill Security information" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function
    Private Sub InsertFisDirectTrd()
        Dim StrSql As String
        Try


            StrSql = "INSERT INTO trading (instrumentname, company, mdate, strikerate, cp, script, qty, rate, entrydate, entryno, token1, isliq, orderno, lActivityTime, FileFlag,Dealer, token,mo, tot, entry_date, tot2, cpf, BuyQty, SaleQty, BuyVal, SaleVal  )" &
                     " SELECT instrumentname,company,mdate,strikerate,cp,script,Qty,Rate,entrydate,entryno,Token1,IsLiq,orderno,lActivityTime,FileFlag,OrgDealer , token,mo, tot, entry_date, tot2, cpf, BuyQty, SaleQty, BuyVal, SaleVal  " &
                     " FROM (" & Replace(Select_FisDirectTrd, ";", "") & ") as tlb; "

            data_access.ParamClear()
            data_access.ExecuteNonQuery(StrSql, CommandType.Text)
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox("Error in inserting fo. trade." & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Sub InsertFisFoTrdNew()
        Dim StrSql As String
        Try


            StrSql = "INSERT INTO trading (instrumentname, company, mdate, strikerate, cp, script, qty, rate, entrydate, entryno, token1, isliq, orderno, lActivityTime, FileFlag,Dealer, token,mo, tot, entry_date, tot2, cpf, BuyQty, SaleQty, BuyVal, SaleVal )" &
                     " SELECT instrumentname,company,mdate,strikerate,cp,script,Qty,Rate,entrydate,entryno,Token1,IsLiq,orderno,lActivityTime,FileFlag,OrgDealer, token,mo, tot, entry_date, tot2, cpf, BuyQty, SaleQty, BuyVal, SaleVal  " &
                     " FROM (" & Replace(Select_FisFoTrd, ";", "") & ") as tlb; "

            ExecuteQuery(StrSql)
            'data_access.ExecuteNonQuery(StrSql, CommandType.Text)
            'data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox("Error in inserting EQ. trade." & vbCrLf & ex.Message)
        End Try
    End Sub

    Public Shared Sub ExecuteQuery(ByVal Str As String)
        Dim cmd_type As CommandType = CommandType.StoredProcedure
        Dim cmd_text As String
        Dim con As OleDbConnection
        Dim tra As OleDbTransaction
        Try
            con = New OleDbConnection(data_access.Connection_string)
            con.Open()

            Dim cmd As New OleDbCommand(Str, con)
            cmd.ExecuteNonQuery()

            Return
        Catch ex As Exception
            ' FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access::ExecuteQuery:-InsertFisFoTrdNew" & ex.ToString)
            Write_ErrorLog3(Now.ToString() & "-" & "Data_access::ExecuteQuery:-InsertFisFoTrdNew" & ex.ToString)
            Return
            tra.Rollback()
            'Con_Rollback()
        Finally
            con.Close()
            ' Close_Connection()
        End Try
    End Sub
    Private Sub InsertFisFoTrd()
        Dim StrSql As String
        Try


            StrSql = "INSERT INTO trading (instrumentname, company, mdate, strikerate, cp, script, qty, rate, entrydate, entryno, token1, isliq, orderno, lActivityTime, FileFlag,Dealer )" &
                     " SELECT instrumentname,company,mdate,strikerate,cp,script,Qty,Rate,entrydate,entryno,Token1,IsLiq,orderno,lActivityTime,FileFlag,OrgDealer  " &
                     " FROM (" & Replace(Select_FisFoTrd, ";", "") & ") as tlb; "

            data_access.ParamClear()
            data_access.ExecuteNonQuery(StrSql, CommandType.Text)
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox("Error in inserting EQ. trade." & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Sub InsertgetsFoTrd()
        Dim StrSql As String
        Try


            StrSql = "INSERT INTO trading (instrumentname, company, mdate, strikerate, cp, script, qty, rate, entrydate, entryno, token1, isliq, orderno, lActivityTime, FileFlag,Dealer )" &
                     " SELECT instrumentname,company,mdate,strikerate,cp,script,Qty,Rate,entrydate,entryno,Token1,IsLiq,orderno,lActivityTime,FileFlag,OrgDealer  " &
                     " FROM (" & Replace(Select_GetFoTrd, ";", "") & ") as tlb; "

            data_access.ParamClear()
            data_access.ExecuteNonQuery(StrSql, CommandType.Text)
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox("Error in inserting EQ. trade." & vbCrLf & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' InsertFoTrd This Function is use to insert FO trade which data is imported by ImportOperation
    ''' by Viral
    ''' </summary>
    ''' <param name="sSql"></param>
    ''' <remarks></remarks>
    Public Sub InsertFoTrd(ByVal sSql As String)
        sSql = sSql.Replace(";", "")
        Dim StrSql As String
        Try

            'StrSql = "INSERT INTO trading (instrumentname, company, mdate, strikerate, cp, script, qty, rate, entrydate, entryno, token1, isliq, orderno, lActivityTime, FileFlag )" & _
            '         " SELECT instrumentname,company,mdate,strikerate,cp,script,Qty,Rate,entrydate,entryno,Token1,IsLiq,orderno,lActivityTime,FileFlag  " & _
            '         " FROM (" & sSql & ") as tlb; "

            StrSql = "INSERT INTO trading (instrumentname, company, mdate, strikerate, cp, script, qty, rate, entrydate, entryno, token1, isliq, orderno, lActivityTime, FileFlag,Dealer, token,mo, tot, entry_date, tot2, cpf, BuyQty, SaleQty, BuyVal, SaleVal,exchange )" &
                     " SELECT instrumentname,company,mdate,strikerate,cp,script,Qty,Rate,entrydate,entryno,Token1,IsLiq,orderno,lActivityTime,FileFlag,OrgDealer, token,mo, tot, entry_date, tot2, cpf, BuyQty, SaleQty, BuyVal, SaleVal,'NSE'" &
                    " FROM (" & sSql & ") as tlb; "
            data_access.ParamClear()
            data_access.ExecuteNonQuery(StrSql, CommandType.Text)
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox("Error in inserting fo. trade." & vbCrLf & ex.Message)
        End Try
    End Sub


    Public Sub InsertBseFoTrd(pSql As String)
        Dim StrSql As String
        Try

            StrSql = "INSERT INTO trading (instrumentname, company, mdate, strikerate, cp, script, qty, rate, entrydate, entryno, token1, isliq, orderno, lActivityTime, FileFlag,Dealer,exchange )" &
                     " SELECT instrumentname,company,mdate,strikerate,cp,script,Qty,Rate,entrydate,entryno,Token1,IsLiq,orderno,lActivityTime,FileFlag,OrgDealer,'BSE'  " &
                     " FROM (" & Replace(pSql, ";", "") & ") as tlb; "

            data_access.ParamClear()
            data_access.ExecuteNonQuery(StrSql, CommandType.Text)
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox("Error in inserting EQ. trade." & vbCrLf & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' InsertCurrTrd This Function is use to insert Currency trade which data is imported by ImportOperation
    ''' by Viral
    ''' </summary>
    ''' <param name="sSql"></param>
    ''' <remarks></remarks>
    Private Sub InsertCurrTrd(ByVal sSql As String)
        Dim StrSql As String
        Try
            'StrSql = "INSERT INTO Currency_Trading (instrumentname, company, mdate, strikerate, cp, script, qty, rate, entrydate, entryno, token1, isliq, orderno, lActivityTime, FileFlag )" & _
            '         " SELECT instrumentname,company,mdate,strikerate,cp,script,Qty,Rate,entrydate,entryno,Token1,IsLiq,orderno,lActivityTime,FileFlag  " & _
            '         " FROM (" & sSql & ") as tlb; "
            StrSql = "INSERT INTO Currency_Trading (instrumentname, company, mdate, strikerate, cp, script, qty, rate, entrydate, entryno, token1, isliq, orderno, lActivityTime, FileFlag,Dealer )" &
                  " SELECT instrumentname,company,mdate,strikerate,cp,script,Qty,Rate,entrydate,entryno,Token1,IsLiq,orderno,lActivityTime,FileFlag,OrgDealer  " &
                  " FROM (" & sSql & ") as tlb; "

            data_access.ParamClear()
            data_access.ExecuteNonQuery(StrSql, CommandType.Text)
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox("Error in inserting currency trade." & vbCrLf & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' InsertEqTrd This Function is use to insert Equity trade which data is imported by ImportOperation
    ''' by Viral
    ''' </summary>
    ''' <param name="sSql"></param>
    ''' <remarks></remarks>
    Private Sub InsertEqTrd(ByVal sSql As String)
        Dim StrSql As String
        Try
            'StrSql = "INSERT INTO equity_trading ( script, company, eq, qty, rate, entrydate, entryno, orderno, lActivityTime, FileFlag ) " & _
            '         " SELECT Script,company,eq,Qty,Rate,entrydate,entryno,orderno,lActivityTime,FileFlag  " & _
            '         " FROM (" & sSql & ") as tlb; "
            StrSql = "INSERT INTO equity_trading ( script, company, eq, qty, rate, entrydate, entryno, orderno, lActivityTime, FileFlag,Dealer ) " &
                  " SELECT Script,company,eq,Qty,Rate,entrydate,entryno,orderno,lActivityTime,FileFlag,OrgDealer  " &
                  " FROM (" & sSql & ") as tlb; "

            data_access.ParamClear()
            data_access.ExecuteNonQuery(StrSql, CommandType.Text)
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
            MsgBox("Error in inserting eq. trade." & vbCrLf & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' DTBSEEqTrd
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DTBseEqTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_BseEqTrd & GetDealerStrBSE()
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function Select_Fist_test1() As DataTable
        Try
            Const SP_Select_DataGrid_Column_Setting As String = "test1"
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Select_DataGrid_Column_Setting
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Expense :: Select_Client_Expenses ::" & ex.ToString)
            Return Nothing
        End Try
    End Function
    Private Function GetDealerStrBSE() As String
        Dim Result As String = ""
        If GVar_DealerCode = "" Then
            Return Result
        Else

            'If GVar_DealerCode.Trim.Length > 0 Then
            Result = Result & " And  Clid IN (" & GVar_DealerCode & ")"
            'End If
            'If GVar_ClientCode.Trim.Length > 0 Then
            '    Result = Result & " And ClientCode IN (" & GVar_ClientCode & ")"
            'End If
            Result = Result & "; "

        End If
        Return Result
    End Function

    ''' <summary>
    ''' SetImportFoTrdDt This Function is use to Process On Adopted Data From importDb  
    ''' </summary>
    ''' <param name="Dtr"></param>
    ''' <param name="DTMargeFOData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetImportFoTrdDt(ByVal Dtr As DataTable, ByRef DTMargeFOData As DataTable) As Boolean
        Dim tik As Long = System.Environment.TickCount
        Try
            'For Each DR As DataRow In Dtr.Rows
            '    If hashOrder.ContainsKey(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim) = True Then
            '        Continue For
            '    Else
            '        hashOrder.Add(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim, "1")
            '    End If
            'Next
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
                .Add("exchange")
            End With
            Dim Dv As DataView
            'Dim Dtm As DataTable
            'Dtm = Dtr
            Dv = New DataView(Dtr, "", "", DataViewRowState.CurrentRows)
            DTTrade = Dv.ToTable(False, "entryno", "instrumentname", "company", "mdate", "strikerate", "cp", "qty", "rate", "tot", "entrydate", "script", "token1", "isliq", "orderno", "lActivityTime", "FileFlag", "Dealer", "exchange")
            Call insert_FOTradeToGlobalTable(DTTrade)
            'Write_TradeLog2("End   UnMatch insert in local table(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            'Etik = System.Environment.TickCount
            Dim status As Boolean
            status = False
            If (DTTrade.Rows.Count > 0) Then
                VarImportInserted = True
                status = True
                'Write_TradeLog2("Start UnMatch Process For MainTable(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
                'Etik = System.Environment.TickCount
                ObjAna.process_data_FO(DTTrade)
                'Write_TradeLog2("End   UnMatch Process For MainTable(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
                'Etik = System.Environment.TickCount
            End If

            'Write_TradeLog2("Start Prepare DTMargeFOData for Exp(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            'Etik = System.Environment.TickCount
            Dim Dtmrg As DataTable
            Dv = New DataView(Dtr, "tokenno > 0", "", DataViewRowState.CurrentRows)
            Dtmrg = Dv.ToTable(False, "entryno", "instrumentname", "company", "mdate", "strikerate", "cp", "Script", "Dealer", "buysell", "Qty", "Rate", "entrydate", "orderno", "Tokenno", "IsLiq", "lActivityTime", "entry_date")
            status = True
            Dtmrg.Columns("Qty").ColumnName = "unit"
            DTMargeFOData.Merge(Dtmrg)
            DTMargeFOData.AcceptChanges()
            Dtmrg.Dispose()
            Write_TradeLog_viral("SetImportFoTrdDT", tik, System.Environment.TickCount)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : SetImportFoTrdDt." & vbCrLf & ex.Message)
            Return False
        End Try

        Write_TradeLog_viral("setimportfotrddt", tik, System.Environment.TickCount)
    End Function
    ''' <summary>
    ''' SetImportCurrTrdDt This Function is use to Process On Adopted Data From importDb  
    ''' </summary>
    ''' <param name="Dtr"></param>
    ''' <param name="DTMargeCurrData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetImportCurrTrdDt(ByVal Dtr As DataTable, ByRef DTMargeCurrData As DataTable) As Boolean
        Try
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
            End With
            Dim Dv As DataView
            'Dim Dtm As DataTable
            'Dtm = Dtr
            Dv = New DataView(Dtr, "", "", DataViewRowState.CurrentRows)
            DTTrade = Dv.ToTable(False, "entryno", "instrumentname", "company", "mdate", "strikerate", "cp", "qty", "rate", "tot", "entrydate", "script", "token1", "isliq", "orderno", "lActivityTime", "FileFlag", "Dealer")

            'Call objTrad.insert_Currency_Trading(DTTrade)
            Call insert_CurrencyTradeToGlobalTable(DTTrade)

            Dim status As Boolean
            status = False
            If (DTTrade.Rows.Count > 0) Then
                VarImportInserted = True
                status = True
                ObjAna.process_data_Currency(DTTrade)
            End If

            Dim Dtmrg As DataTable
            Dv = New DataView(Dtr, "tokenno > 0", "", DataViewRowState.CurrentRows)
            Dtmrg = Dv.ToTable(False, "entryno", "instrumentname", "company", "mdate", "strikerate", "cp", "Script", "Dealer", "buysell", "Qty", "Rate", "entrydate", "orderno", "Tokenno", "IsLiq", "lActivityTime", "entry_date")
            status = True
            Dtmrg.Columns("Qty").ColumnName = "unit"
            DTMargeCurrData.Merge(Dtmrg)
            DTMargeCurrData.AcceptChanges()
            Dtmrg.Dispose()

            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : SetImportCurrTrdDt." & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' SetImportEqTrdDt This Function is use to Process On Adopted Data From importDb  
    ''' </summary>
    ''' <param name="Dtr"></param>
    ''' <param name="DTMargeEQData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetImportEqTrdDt(ByVal Dtr As DataTable, ByRef DTMargeEQData As DataTable) As Boolean

        'For Each DR As DataRow In Dtr.Rows 'Select("dealer = " & dealer)
        '    If hashOrder.ContainsKey(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim) = True Then
        '        Continue For
        '    Else
        '        hashOrder.Add(DR("orderno").ToString.Trim & "-" & DR("entryno").ToString.Trim, "1")
        '    End If
        'Next
        Try
            Dim DTTrade As New DataTable
            REM 1: create datatable for future and option
            With DTTrade.Columns
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
            End With
            Dim Dv As DataView
            'Dim Dtm As DataTable
            'Dtm = Dtr
            Dv = New DataView(Dtr, "", "", DataViewRowState.CurrentRows)
            DTTrade = Dv.ToTable(False, "script", "company", "eq", "qty", "rate", "tot", "entrydate", "entryno", "orderno", "lActivityTime", "FileFlag", "Dealer")

            'Call objTrad.insert_EQTrading(DTTrade)
            Call insert_EQTradeToGlobalTable(DTTrade)

            Dim status As Boolean
            status = False
            If (DTTrade.Rows.Count > 0) Then
                VarImportInserted = True
                status = True
                ObjAna.process_data_EQ(DTTrade)
            End If

            Dim Dtmrg As DataTable
            Dv = New DataView(Dtr, "tokenno > 0", "", DataViewRowState.CurrentRows)
            Dtmrg = Dv.ToTable(False, "entryno", "company", "cp", "Script", "Dealer", "buysell", "Qty", "Rate", "entrydate", "orderno", "Tokenno", "IsLiq", "lActivityTime", "entry_date")
            status = True
            Dtmrg.Columns("Qty").ColumnName = "unit"
            DTMargeEQData.Merge(Dtmrg)
            DTMargeEQData.AcceptChanges()
            Dtmrg.Dispose()

            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : SetImportEqTrdDt." & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    'Private Function GetDealerStr_notisEQ() As String

    '    Dim Result As String = ""
    '    Dim newstring As String = ""
    '    If GVar_DealerCode = "" Then
    '        Return Result
    '    Else
    '        If GVar_DealerCode.Contains("'") Then
    '            'GVar_DealerCode.Remove (")
    '            'Dim newstring As String
    '            GVar_DealerCode = GVar_DealerCode.Replace("'", "")
    '        End If

    '        If GVar_DealerCode.Contains(",") = True Then
    '            Dim words As String() = GVar_DealerCode.Split(New Char() {","c})

    '            ' Use For Each loop over words and display them.
    '            Dim word As String
    '            For Each word In words
    '                If newstring = "" Then
    '                    newstring = "'" & word.Replace("'", "") & "'"
    '                Else
    '                    newstring = newstring + "," + "'" & word.Replace("'", "") & "'"
    '                End If

    '            Next
    '            'GVar_DealerCode = newstring
    '        End If



    '        'If GVar_DealerCode.Trim.Length > 0 Then
    '        '    Result = Result & " And  OrgDealer IN (" & GVar_DealerCode & ")"
    '        If newstring = "" Then
    '            Result = Result & " And  OrgDealer IN ('" & GVar_DealerCode & "')"
    '        Else
    '            Result = Result & " And  OrgDealer IN (" & newstring & ")"
    '        End If


    '        'End If
    '        'If GVar_ClientCode.Trim.Length > 0 Then
    '        '    Result = Result & " And ClientCode IN (" & GVar_ClientCode & ")"
    '        'End If
    '        Result = Result & "; "
    '    End If


    '    Return Result
    'End Function
    'Private Function GetDealerStr_notisCURR() As String

    '    Dim Result As String = ""
    '    If GVar_DealerCode = "" Then
    '        Return Result
    '    Else
    '        Dim newstring As String = ""
    '        If GVar_DealerCode.Contains("'") Then
    '            'GVar_DealerCode.Remove (")

    '            GVar_DealerCode = GVar_DealerCode.Replace("'", "")
    '        End If
    '        'If GVar_DealerCode.Trim.Length > 0 Then

    '        If GVar_DealerCode.Contains(",") = True Then
    '            Dim words As String() = GVar_DealerCode.Split(New Char() {","c})

    '            ' Use For Each loop over words and display them.
    '            Dim word As String
    '            For Each word In words
    '                If newstring = "" Then
    '                    newstring = "'" & word.Replace("'", "") & "'"
    '                Else
    '                    newstring = newstring +"," + "'" & word.Replace("'", "") & "'"
    '                End If

    '            Next
    '            'GVar_DealerCode = newstring
    '        End If



    '        If newstring = "" Then
    '            Result = Result & " And  OrgDealer IN ('" & GVar_DealerCode & "')"
    '        Else
    '            Result = Result & " And  OrgDealer IN (" & newstring & ")"
    '        End If

    '        'Result = Result & " And  OrgDealer IN (" & GVar_DealerCode & ")"
    '        '    Result = Result & " And  OrgDealer IN (" & newstring & ")"
    '        'End If
    '        'If GVar_ClientCode.Trim.Length > 0 Then
    '        '    Result = Result & " And ClientCode IN (" & GVar_ClientCode & ")"
    '        'End If
    '        Result = Result & "; "
    '    End If


    '    Return Result
    'End Function

    Private Function GetDealerStr() As String

        Dim Result As String = ""
        If GVar_DealerCode = "" Then
            Return Result


        Else
            'If GVar_DealerCode.Contains("'") Then
            '    'GVar_DealerCode.Remove (")
            '    Dim newstring As String
            '    GVar_DealerCode = GVar_DealerCode.Replace("'", "")
            'End If
            'If GVar_DealerCode.Trim.Length > 0 Then
            Result = Result & " And  OrgDealer IN (" & GVar_DealerCode & ")"
            'End If
            'If GVar_ClientCode.Trim.Length > 0 Then
            '    Result = Result & " And ClientCode IN (" & GVar_ClientCode & ")"
            'End If
            Result = Result & ";"
        End If


        Return Result
    End Function



    ''' <summary>
    ''' DTNotFoTrd This Function is use to Get Data of Notis Fo TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTNotFoTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_NotFoTrd & GetDealerStr()
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' DTNotCurTrd This Function is use To Get Data of Notis Currency TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTNotCurTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_NotCurTrd & GetDealerStr() 'GetDealerStr_notisCURR() ' GetDealerStr()
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' DTNotEqTrd This Function is use To Get Data of Notis Equity TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTNotEqTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_NotEqTrd & GetDealerStr() ' GetDealerStr_notisEQ()
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Private Function DTFistDirectFoTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_FisDirectTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Private Function DTFistEQTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_FisEQTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' DTFisFoTrd This Function is use To Get Data of Fist FO TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTFistFoTrd() As DataTable
        Dim tik As Long = System.Environment.TickCount
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_FisFoTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try

        Write_TradeLog_viral("DTFistFoTrd", tik, System.Environment.TickCount)
    End Function

    ''' <summary>
    ''' DTFadFoTrd This Function is use To Get Data of Fist FO Admin TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTFadmFoTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_FadFoTrd & GetDealerStr()
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function



    ''' <summary>
    ''' DTGetsFoTrd This Function is use To Get Data of Gets FO TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTGetsFoTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_GetFoTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function


    Public Function DTGetsBSEFoTrd(pSql As String) As DataTable

        Try
            data_access.ParamClear()
            data_access.Cmd_Text = pSql
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' DTGetsCurTrd This Function is use To Get Data of Gets Currency TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTGetsCurTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_GetCurTrd '& GetDealerStr()
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' DTGetsEqTrd This Function is use To Get Data of Gets Equity TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTGetsEqTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_GetEqTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function DTNowFo() As DataTable

        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_NowFoTrd1
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' DTNowFoTrd This Function is use To Get Data of Now FO TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTNowFoTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_NowFoTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' DTNowCurTrd This Function is use To Get Data of Now Currency TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTNowCurTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_NowCurTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' DTNowEqTrd This Function is use To Get Data of Now Equity TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTNowEqTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_NowEqTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' DTOdiFoTrd This Function is use To Get Data of Odin FO TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTOdiFoTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_OdiFoTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' DTOdiCurTrd This Function is use To Get Data of Odin Currency TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTOdiCurTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_OdiCurTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Private Function DTOdiMcxCurTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_OdiMcxCurTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' DTOdiEqTrd This Function is use To Get Data of Odin Equity TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTOdiEqTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_OdiEqTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' DTNseFoTrd This Function is use To Get Data of NSE FO TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTNseFoTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_NseFoTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' DTNseCurTrd This Function is use To Get Data of Nse Currency TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTNseCurTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_NseCurTrd
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' DTNeaFoTrd This Function is use To Get Data of Neat FO TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function DTNeaFoTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_NeaFoTrd '& GetDealerStr()
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' DTNeaCurTrd This Function is use To Get Data of Neat Currency TradeFile Which is Imported by ImportOperation class
    ''' By Viral
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Private Function DTNeaCurTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_NeaCurTrd '& GetDealerStr()
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function FromFistDirectTEXT(ByVal ISTimer As Boolean, ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try


            Dim Dtr As DataTable


            'Call objIO.ImportFisFoTrd()

            Dtr = DTFistDirectFoTrd()

            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            InsertFisDirectTrd()
            Dim status As Boolean
            status = SetImportFoTrdDt(Dtr, DTMargeFOData)

            Return status

        Catch ex As Exception
            MsgBox("Error in Procedure : FromFistFoText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Public Function FromFistEQTEXT(ByVal ISTimer As Boolean, ByRef DTMargeEQData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim Stik As Long = System.Environment.TickCount
            Write_TradeLog2("Start Validate & Copy To C:\data\..(" & Stik & ")", Stik)

            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "EQ") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "FIST EQ")
            Dim Etik As Long = System.Environment.TickCount
            Write_TradeLog2("End Validate & Copy To C:\data\..(" & Etik - Stik & ")", Etik)

            Dim Dtr As DataTable

            'Write_TradeLog2("Start Import FisFoTrd(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            'Etik = System.Environment.TickCount
            Call objIO.ImportFisEqTrd()
            'Write_TradeLog2("End   Import FisFoTrd(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            'Etik = System.Environment.TickCount

            'Write_TradeLog2("Start GetUnMatch FisFoTrd(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            'Etik = System.Environment.TickCount
            Dtr = DTFistEQTrd()
            'Write_TradeLog2("End   GetUnMatch FisFoTrd(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            'Etik = System.Environment.TickCount
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            ' Write_TradeLog2("Start GetUnMatch insert in Trading(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            Etik = System.Environment.TickCount
            'Dim thr As New Threading.Thread(AddressOf InsertFisFoTrd)
            'thr.Start()
            Call InsertEqTrd(Replace(Select_FisEQTrd, ";", ""))
            'Call (InsertFisFoTrd())()
            '  Write_TradeLog2("End   GetUnMatch insert in Trading(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            '   Etik = System.Environment.TickCount

            Dim status As Boolean
            status = SetImportEqTrdDt(Dtr, DTMargeEQData)


            Return status

        Catch ex As Exception
            MsgBox("Error in Procedure : FromFistFoText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Private Function DTCustomEQTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_CustomEQTrd & GetDealerStr()
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Private Function DTCustomFoTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_CustomFoTrd & GetDealerStr()
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Private Function DTCustomcurrTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_CustomcurrTrd & GetDealerStr()
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function FromcustomEQTEXT(ByVal ISTimer As Boolean, ByRef DTMargeEQData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            Write_TimeLog1("FromcustomEQTEXT VarFilePath:" + VarFilePath)
            If ValidateTxtFile(VarFilePath, "EQ") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "Custom Fo")

            Dim Dtr As DataTable
            '  Call objIO.ImportOdiEQTrd()
            'Dtr = DTOdiEqTrd()
            Dtr = DTCustomEQTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertEqTrd(Replace(Select_CustomEQTrd, ";", ""))

            Dim status As Boolean
            status = SetImportEqTrdDt(Dtr, DTMargeEQData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromcustomEQTEXT() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Public Function FromCustomCurrTEXT(ByVal ISTimer As Boolean, ByRef DTMargeCurrData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            'Dim strdealer As String = GetDealerStr()
            'If strdealer = "" Then
            '    MessageBox.Show("DealerCode Not Exits in Setting Form For Notis Currency Trade")
            '    Exit Function
            'End If
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "CURRENCY") = False Then
                Return False
                Exit Function
            End If
            Write_TimeLog1("FromCustomCurrTEXT")
            Call CopyToData(VarFilePath, "Custom Fo")

            Dim Dtr As DataTable
            'Call objIO.ImportNotCurTrd()
            Dtr = DTCustomcurrTrd()
            'Dtr = DTNotCurTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If

            Call InsertCurrTrd(Replace(Select_CustomcurrTrd & GetDealerStr(), ";", ""))
            Dim status As Boolean
            status = SetImportCurrTrdDt(Dtr, DTMargeCurrData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromNotisCurrText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Public Function FromCustomFOTEXT(ByVal ISTimer As Boolean, ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean

        Try
            'Dim strdealer As String = GetDealerStr()
            'If strdealer = "" Then
            '    MessageBox.Show("DealerCode Not Exits in Setting Form For Notis Fo Trade")
            '    Exit Function
            'End If
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            '   Write_TimeLog1("FromCustomFOTEXT VarFilePath:" + VarFilePath)
            'If ValidateTxtFile(VarFilePath, "FO") = False Then
            '    Return False
            '    Exit Function
            'End If
            Write_TimeLog1("call CopyToData")
            Call CopyToData(VarFilePath, "Custom Fo")

            Dim Dtr As DataTable
            'Call objIO.ImportCustomFoTrd()
            Dtr = DTCustomFoTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If


            Call InsertFoTrd(Replace(Select_CustomFoTrd & GetDealerStr(), ";", ""))

            Dim status As Boolean
            status = SetImportFoTrdDt(Dtr, DTMargeFOData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromNotisFoText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' FromFistFOTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeFOData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromFistFOTEXT(ByVal ISTimer As Boolean, ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Dim tik As Long = Environment.TickCount
        Try
            Dim Stik As Long = System.Environment.TickCount
            Write_TradeLog2("Start Validate & Copy To C:\data\..(" & Stik & ")", Stik)

            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "FO") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "FIST FO")
            'Dim Etik As Long = System.Environment.TickCount
            'Write_TradeLog2("End Validate & Copy To C:\data\..(" & Etik - Stik & ")", Etik)

            Dim Dtr As DataTable

            'Write_TradeLog2("Start Import FisFoTrd(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            'Etik = System.Environment.TickCount
            'Call objIO.ImportFisFoTrd()
            'Write_TradeLog2("End   Import FisFoTrd(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            'Etik = System.Environment.TickCount

            'Write_TradeLog2("Start GetUnMatch FisFoTrd(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            'Etik = System.Environment.TickCount
            Dtr = DTFistFoTrd()
            'Dtr = Select_Fist_test1()
            'Write_TradeLog2("End   GetUnMatch FisFoTrd(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            'Etik = System.Environment.TickCount
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            ' Write_TradeLog2("Start GetUnMatch insert in Trading(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)

            'Dim thr As New Threading.Thread(AddressOf InsertFisFoTrd)
            'thr.Start()
            '     InsertFisFoTrd()
            Thr_InsertFistDataToDB = New Thread(AddressOf InsertFisFoTrdNew)
            Thr_InsertFistDataToDB.Name = "ThrFinserFistfo"
            Thr_InsertFistDataToDB.Start()

            'Call (InsertFisFoTrd())()
            '  Write_TradeLog2("End   GetUnMatch insert in Trading(" & System.Environment.TickCount - Etik & ")", System.Environment.TickCount)
            '   Etik = System.Environment.TickCount

            Dim status As Boolean
            status = SetImportFoTrdDt(Dtr, DTMargeFOData)

            Write_TradeLog_viral("FromFistFOTEXT", tik, System.Environment.TickCount)
            Return status

        Catch ex As Exception
            MsgBox("Error in Procedure : FromFistFoText() " & vbCrLf & ex.Message)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' FromFistFOTEXT (Admin File)This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeFOData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromFadmFOTEXT(ByVal ISTimer As Boolean, ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim strdealer As String = GetDealerStr()
            If strdealer = "" Then
                MessageBox.Show("DealerCode Not Exits in Setting Form For Fad FO Trade")
                Exit Function
            End If
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "FO") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "FADM FO")

            Dim Dtr As DataTable
            Call objIO.ImportFadFoTrd()
            Dtr = DTFadmFoTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            'Call InsertFoTrd(Replace(Select_FadFoTrd, ";", ""))

            Call InsertFoTrd(Replace(Select_FadFoTrd & GetDealerStr(), ";", ""))
            Dim status As Boolean
            status = SetImportFoTrdDt(Dtr, DTMargeFOData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromFadmFoText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function


    ''' <summary>
    ''' FromBSEEQTEXT
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeEQData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromBSEEQTEXT(ByVal ISTimer As Boolean, ByRef DTMargeEQData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim strdealer As String = GetDealerStrBSE()
            If strdealer = "" Then
                MessageBox.Show("DealerCode Not Exits in Setting Form For BSE EQ Trade")
                Exit Function
            End If

            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If

            'Call CopyToData(VarFilePath, "BSE EQ", 1)
            If ValidateTxtFile(VarFilePath, "EQ") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "BSE EQ")

            Dim Dtr As DataTable
            Call objIO.ImportBSEEQTrd()
            Dtr = DTBseEqTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If


            Call InsertEqTrd(Replace(Select_BseEqTrd & GetDealerStrBSE(), ";", ""))

            Dim status As Boolean
            status = SetImportEqTrdDt(Dtr, DTMargeEQData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromGetsEqText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function



    ''' <summary>
    ''' FromGetsFOTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeFOData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Public Function FromGetsBSEFOTEXT(ByVal ISTimer As Boolean, ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "FO") = False Then
                Return False
                Exit Function
            End If
            'mPerfTime.SetFileName("TradeFileImportDataLog")
            'mPerfTime.Write_DiffMs("MainEnd", "Start Everything-----> :")
            Call CopyToData(VarFilePath, "BSE GETS FO")
            Dim Dtr As DataTable
            Call objIO.ImportGetBseFoTrd()

            Dim strSql As String = Select_GetBseFoTrd 'GetBseFoSql()

            Dtr = DTGetsBSEFoTrd(strSql)
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            'Call InsertFoTrd(Replace(Select_GetFoTrd, ";", ""))
            Call InsertBseFoTrd(Replace(strSql, ";", ""))
            Dim status As Boolean
            status = SetImportFoTrdDt(Dtr, DTMargeFOData)
            'mPerfTime.Write_DiffMs("MainEnd", "End Completed Everything-----> :")
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromGetsFoText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function GetBseFoSql() As String
        Dim sb As New System.Text.StringBuilder()
        sb.AppendLine("SELECT")
        sb.AppendLine(" Val([Field1]) AS entryno,")
        sb.AppendLine(" NotFo.Field3 AS instrumentname,")
        sb.AppendLine(" NotFo.Field4 AS company,")
        sb.AppendLine(" IIf(IsDate([Field5]), CDate([Field5]), Null) AS mdate,")
        sb.AppendLine(" IIf(IsNull([Field6]), 0, [Field6]) AS strikerate,")
        sb.AppendLine(" IIf(Left([Field3], 3)='FUT','F',Left([Field7],1)) AS cp,")

        sb.AppendLine(" UCase(")
        sb.AppendLine("   IIf(")
        sb.AppendLine("     Left([Field7],1) In ('C','P'),")
        sb.AppendLine("     [Field3] & ' ' & [Field4] & ' ' &")
        sb.AppendLine("     Format(IIf(IsDate([Field5]),CDate([Field5]),Null),'ddmmmyyyy') & ' ' &")
        sb.AppendLine("     Format(Val(IIf(IsNull([Field6]),0,[Field6])),'Fixed') & ' ' & [Field7],")
        sb.AppendLine("     [Field3] & ' ' & [Field4] & ' ' &")
        sb.AppendLine("     Format(IIf(IsDate([Field5]),CDate([Field5]),Null),'ddmmmyyyy')")
        sb.AppendLine("   )")
        sb.AppendLine(" ) AS script,")

        sb.AppendLine(" 'NOTIS-' & IIf(Len([Field18])>12, Left([Field18],12), [Field17]) AS Dealer,")
        sb.AppendLine(" NotFo.Field13 AS buysell,")

        sb.AppendLine(" Val(")
        sb.AppendLine("   IIf(")
        sb.AppendLine("     Left([Field13],1)='B' OR Val([Field13])=1,")
        sb.AppendLine("     Val([Field14]),")
        sb.AppendLine("     -Val([Field14])")
        sb.AppendLine("   )")
        sb.AppendLine(" ) AS Qty,")

        sb.AppendLine(" Val([Field15]) AS Rate,")
        sb.AppendLine(" CDate([Field22]) AS entrydate,")
        sb.AppendLine(" NotFo.Field23 AS orderno,")
        sb.AppendLine(" NotFo.Field18 AS Dealer1,")
        sb.AppendLine(" False AS IsLiq,")
        sb.AppendLine(" DateDiff('s', #01/01/1980#, CDate([Field22])) AS lActivityTime,")
        sb.AppendLine(" DateValue(CDate([Field22])) AS entry_date,")

        sb.AppendLine(" Val(")
        sb.AppendLine("   IIf(")
        sb.AppendLine("     Left([Field13],1)='B',")
        sb.AppendLine("     Val([Field14]),")
        sb.AppendLine("     -Val([Field14])")
        sb.AppendLine("   )")
        sb.AppendLine(" ) * Val([Field15]) AS Tot,")

        sb.AppendLine(" 'NOTICEFOTEXT' AS FileFlag,")
        sb.AppendLine(" IIf(Len([Field18])>12, [Field17] & Left([Field18],12), [Field17]) AS OrgDealer")

        sb.AppendLine("FROM BSEGETFOTRD AS NotFo")
        sb.AppendLine("LEFT JOIN trading")
        sb.AppendLine(" ON Val(NotFo.Field1)=trading.entryno")
        sb.AppendLine(" AND NotFo.Field23=trading.orderno;")

        Dim strSql As String = sb.ToString()
        Return strSql
    End Function

    Public Function FromGetsFOTEXTAll(ByVal ISTimer As Boolean, ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation, pExchange As String) As Boolean
        If pExchange.ToUpper() = "BSE" Then
            Return FromGetsBSEFOTEXT(ISTimer, DTMargeFOData, CurrentFilePath, PrevFilePath, objIO)
        ElseIf pExchange.ToUpper() = "NSE" Then
            Return FromGetsFOTEXT(ISTimer, DTMargeFOData, CurrentFilePath, PrevFilePath, objIO)
        End If
        Return False
    End Function

    Public Function FromGetsFOTEXT(ByVal ISTimer As Boolean, ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "FO") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "GETS FO")

            Dim Dtr As DataTable
            Call objIO.ImportGetFoTrd()
            Dtr = DTGetsFoTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertFoTrd(Replace(Select_GetFoTrd, ";", ""))
            Dim status As Boolean
            status = SetImportFoTrdDt(Dtr, DTMargeFOData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromGetsFoText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' FromGetsCurrTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeCurrData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromGetsCurrTEXT(ByVal ISTimer As Boolean, ByRef DTMargeCurrData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "CURRENCY") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "GETS CURRENCY")

            Dim Dtr As DataTable
            Call objIO.ImportGetCurTrd()
            Dtr = DTGetsCurTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertCurrTrd(Replace(Select_GetCurTrd, ";", ""))

            Dim status As Boolean
            status = SetImportCurrTrdDt(Dtr, DTMargeCurrData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromGetsCurrText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' FromGetsEQTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeEQData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromGetsEQTEXT(ByVal ISTimer As Boolean, ByRef DTMargeEQData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "EQ") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "GETS EQ")

            Dim Dtr As DataTable
            Call objIO.ImportGetEQTrd()
            Dtr = DTGetsEqTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertEqTrd(Replace(Select_GetEqTrd, ";", ""))

            Dim status As Boolean
            status = SetImportEqTrdDt(Dtr, DTMargeEQData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromGetsEqText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' FromNotisFOTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeFOData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromNotisFOTEXT(ByVal ISTimer As Boolean, ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean

        Try

            Dim strdealer As String = GVar_DealerCode ' GetDealerStr()
            If strdealer = "" Then
                MessageBox.Show("DealerCode Not Exits in Setting Form For Notis Fo Trade")
                Exit Function
            End If
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "FO") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "NOTICE FO")

            Dim Dtr As DataTable
            Call objIO.ImportNotFoTrd()
            Dtr = DTNotFoTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If


            Call InsertFoTrd(Replace(Select_NotFoTrd & GetDealerStr(), ";", ""))

            Dim status As Boolean
            status = SetImportFoTrdDt(Dtr, DTMargeFOData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromNotisFoText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' FromNotisCurrTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeCurrData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromNotisCurrTEXT(ByVal ISTimer As Boolean, ByRef DTMargeCurrData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim strdealer As String = GetDealerStr()
            If strdealer = "" Then
                MessageBox.Show("DealerCode Not Exits in Setting Form For Notis Currency Trade")
                Exit Function
            End If
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "CURRENCY") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "NOTICE CURRENCY")

            Dim Dtr As DataTable
            Call objIO.ImportNotCurTrd()
            Dtr = DTNotCurTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If

            Call InsertCurrTrd(Replace(Select_NotCurTrd & GetDealerStr(), ";", ""))
            Dim status As Boolean
            status = SetImportCurrTrdDt(Dtr, DTMargeCurrData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromNotisCurrText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' FromNotisEQTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeEQData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromNotisEQTEXT(ByVal ISTimer As Boolean, ByRef DTMargeEQData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim strdealer As String = GetDealerStr() '_notisEQ()
            If strdealer = "" Then
                MessageBox.Show("DealerCode Not Exits in Setting Form For Notis EQ Trade")
                Exit Function
            End If
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "EQ") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "NOTICE EQ")

            Dim Dtr As DataTable
            Call objIO.ImportNotEQTrd()
            Dtr = DTNotEqTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            'Call InsertEqTrd(Replace(Select_NotEqTrd, ";", ""))

            'Call InsertEqTrd(Replace(Select_NotEqTrd & GetDealerStr_notisEQ(), ";", ""))
            Call InsertEqTrd(Replace(Select_NotEqTrd & GetDealerStr(), ";", ""))

            Dim status As Boolean
            status = SetImportEqTrdDt(Dtr, DTMargeEQData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromNotisEqText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' FromNowFOTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeFOData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromNowFOTEXT(ByVal ISTimer As Boolean, ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "FO") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "NOW FO")

            Dim Dtr As DataTable


            Call objIO.ImportNowFoTrd()
            Dtr = DTNowFoTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertFoTrd(Replace(Select_NowFoTrd, ";", ""))

            Dim status As Boolean
            status = SetImportFoTrdDt(Dtr, DTMargeFOData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromNowFoText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' FromNowCurrTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeCurrData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromNowCurrTEXT(ByVal ISTimer As Boolean, ByRef DTMargeCurrData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "CURRENCY") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "NOW CURRENCY")

            Dim Dtr As DataTable
            Call objIO.ImportNowCurTrd()
            Dtr = DTNowCurTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertCurrTrd(Replace(Select_NowCurTrd, ";", ""))

            Dim status As Boolean
            status = SetImportCurrTrdDt(Dtr, DTMargeCurrData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromNowCurrText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' FromNowEQTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeEQData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromNowEQTEXT(ByVal ISTimer As Boolean, ByRef DTMargeEQData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If

            If ValidateTxtFile(VarFilePath, "EQ") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "NOW EQ")

            Dim Dtr As DataTable
            Call objIO.ImportNowEQTrd()
            Dtr = DTNowEqTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertEqTrd(Replace(Select_NowEqTrd, ";", ""))

            Dim status As Boolean
            status = SetImportEqTrdDt(Dtr, DTMargeEQData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromNowEqText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' FromOdinFOTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeFOData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromOdinFOTEXT(ByVal ISTimer As Boolean, ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "FO") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "ODIN FO")

            Dim Dtr As DataTable
            Call objIO.ImportOdiFoTrd()
            Dtr = DTOdiFoTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Dim status As Boolean
            Call InsertFoTrd(Replace(Select_OdiFoTrd, ";", ""))

            status = SetImportFoTrdDt(Dtr, DTMargeFOData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromOdinFoText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' FromOdinCurrTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeCurrData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromOdinCurrTEXT(ByVal ISTimer As Boolean, ByRef DTMargeCurrData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "CURRENCY") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "ODIN CURRENCY")

            Dim Dtr As DataTable
            Call objIO.ImportOdiCurTrd()
            Dtr = DTOdiCurTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertCurrTrd(Replace(Select_OdiCurTrd, ";", ""))

            Dim status As Boolean
            status = SetImportCurrTrdDt(Dtr, DTMargeCurrData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromOdinCurrText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    Public Function FromMCXOdinCurrTEXT(ByVal ISTimer As Boolean, ByRef DTMargeCurrData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "CURRENCY") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "ODIN MCX CURRENCY")

            Dim Dtr As DataTable
            Call objIO.ImportOdiMcxCurTrd()
            Dtr = DTOdiMcxCurTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertCurrTrd(Replace(Select_OdiMcxCurTrd, ";", ""))

            Dim status As Boolean
            status = SetImportCurrTrdDt(Dtr, DTMargeCurrData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromMCXOdinCurrText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' FromOdinEQTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeEQData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromOdinEQTEXT(ByVal ISTimer As Boolean, ByRef DTMargeEQData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "EQ") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "ODIN EQ")

            Dim Dtr As DataTable
            Call objIO.ImportOdiEQTrd()
            Dtr = DTOdiEqTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertEqTrd(Replace(Select_OdiEqTrd, ";", ""))

            Dim status As Boolean
            status = SetImportEqTrdDt(Dtr, DTMargeEQData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromOdinEqText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' FromNseFOTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeFOData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromNseFOTEXT(ByVal ISTimer As Boolean, ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "FO") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "NSE FO")

            Dim Dtr As DataTable
            Call objIO.ImportNseFoTrd()
            Dtr = DTNseFoTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertFoTrd(Replace(Select_NseFoTrd, ";", ""))

            Dim status As Boolean
            status = SetImportFoTrdDt(Dtr, DTMargeFOData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromNseFoText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' FromNseCurrTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeCurrData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromNseCurrTEXT(ByVal ISTimer As Boolean, ByRef DTMargeCurrData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "CURRENCY") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "NSE CURRENCY")

            Dim Dtr As DataTable
            Call objIO.ImportNseCurTrd()
            Dtr = DTNseCurTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertCurrTrd(Replace(Select_NseCurTrd, ";", ""))

            Dim status As Boolean
            status = SetImportCurrTrdDt(Dtr, DTMargeCurrData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromNseCurrText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' FromNeaFOTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeFOData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromNeaFOTEXT(ByVal ISTimer As Boolean, ByRef DTMargeFOData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "FO") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "NEAT FO")

            Dim Dtr As DataTable
            Call objIO.ImportNeaFoTrd()
            Dtr = DTNeaFoTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertFoTrd(Replace(Select_NeaFoTrd, ";", ""))
            'Call InsertFoTrd(Replace(Select_NeaFoTrd & GetDealerStr(), ";", ""))
            Dim status As Boolean
            status = SetImportFoTrdDt(Dtr, DTMargeFOData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromNeatFoText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' FromNeaCurrTEXT This Function is use to process on trade file 
    ''' Get data from trade file 
    ''' import in greek.mdb
    ''' process on adopted data and merge to current trade table
    ''' </summary>
    ''' <param name="ISTimer"></param>
    ''' <param name="DTMargeCurrData"></param>
    ''' <param name="CurrentFilePath"></param>
    ''' <param name="PrevFilePath"></param>
    ''' <param name="objIO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FromNeaCurrTEXT(ByVal ISTimer As Boolean, ByRef DTMargeCurrData As DataTable, ByVal CurrentFilePath As String, ByVal PrevFilePath As String, ByVal objIO As ImportOperation) As Boolean
        Try
            Dim VarFilePath As String
            If PrevFilePath = "" Then
                VarFilePath = CurrentFilePath
            Else
                VarFilePath = PrevFilePath
            End If
            If ValidateTxtFile(VarFilePath, "CURRENCY") = False Then
                Return False
                Exit Function
            End If
            Call CopyToData(VarFilePath, "NEAT CURRENCY")

            Dim Dtr As DataTable
            Call objIO.ImportNeaCurTrd()
            Dtr = DTNeaCurTrd()
            If Dtr.Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            Call InsertCurrTrd(Replace(Select_NeaCurTrd, ";", ""))
            'Call InsertCurrTrd(Replace(Select_NeaCurTrd & GetDealerStr(), ";", ""))

            Dim status As Boolean
            status = SetImportCurrTrdDt(Dtr, DTMargeCurrData)
            Return status
        Catch ex As Exception
            MsgBox("Error in Procedure : FromNeatCurrText() " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function get_latest_bhavcopy_file(ByVal path As String) As String
        Dim di As New IO.DirectoryInfo(path)
        Dim fi As IO.FileInfo
        Dim max As Date
        Dim current As Date
        Dim max_file As String = ""

        If System.IO.Directory.Exists(path) Then
            Dim directory As New System.IO.DirectoryInfo(path)
            For Each file As System.IO.FileInfo In directory.GetFiles()
                Try

                    If Mid(file.Name(), 1, 2) = "fo" And file.Name().Substring(file.Name().Length - 4) = ".csv" Then
                        If file.Name.Length = 19 Then
                            Dim str As String
                            'str = file.Name().Replace("fo", "")
                            'str = str.Replace("bhav.csv", "")
                            current = CDate(Mid(file.Name(), 3, 2) & Format(Mid(file.Name(), 5, 3)) & Mid(file.Name(), 8, 4))
                            If current > max Then 'if find span file
                                max = current
                                max_file = file.Name() 'return span file name
                            End If

                        End If

                    End If
                Catch ex As Exception

                End Try
            Next
        End If
        Return max_file
    End Function

    Public Sub New()

    End Sub
End Class
