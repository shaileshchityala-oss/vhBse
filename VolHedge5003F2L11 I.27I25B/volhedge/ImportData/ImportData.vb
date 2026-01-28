Imports System.IO
Imports System.Configuration
Imports System.Windows.Forms
Imports VolHedge
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text
Imports VolHedge.DAL


Namespace ImportData

    Public Class ImportOperation
        'Public dbName As String = ConfigurationSettings.AppSettings("dbname")
        Private ImportDb As String = ConfigurationSettings.AppSettings("Importdb")
        Private DbPath As String = Application.StartupPath & ConfigurationSettings.AppSettings("dbname") & ";PWD=" & clsGlobal.glbAcessPassWord & ""

#Region "Insert Query"
        REM Contract
        Private Insert_Currency_Contract As String = "INSERT INTO [" & DbPath & "].Currency_Contract (Token, Asset_Tokan, InstrumentName, Symbol, series, expiry_date, strike_price, option_type, script, lotsize, multiplier,expdate1 ) " & _
                                                    " SELECT Cd_contract.Field1, Cd_contract.Field2, Cd_contract.Field3, Cd_contract.Field4, Cd_contract.Field5, Cd_contract.Field7, format(Val([Cd_contract].[Field8])/10000000,'#0.0000'), Cd_contract.Field9, IIf((Trim(Left([Cd_contract].[Field9],1))='C' Or Trim(Left([Cd_contract].[Field9],1))='P'),[Cd_contract].[Field3] & '  ' & [Cd_contract].[Field4] & '  ' & Format(DateAdd('s',Val([Cd_contract].[Field7]),CDate('1/1/1980')),'ddmmmyyyy') & '  ' & Format(Val([Cd_contract].[Field8])/10000000,'0.0000') & '  ' & [Cd_contract].[Field9],[Cd_contract].[Field3] & '  ' & [Cd_contract].[Field4] & '  ' & Format(DateAdd('s',Val([Cd_contract].[Field7]),CDate('1/1/1980')),'ddmmmyyyy')) AS Script, Cd_contract.Field32, Cd_contract.Field52 ,CDate(Format(DateAdd('s',[Cd_contract].[Field7],'1/1/1980'),'mmm/dd/yyyy')) as expdate1" & _
                                                    " FROM Cd_contract " & _
                                                    " WHERE TRIM(Cd_contract.Field3)<>'' And TRIM(Cd_contract.Field7)<>'-1' ;"

        Private Insert_Currency_Contractcsv As String = "INSERT INTO [" & DbPath & "].Currency_Contract (Token, Asset_Tokan, InstrumentName, Symbol, series, expiry_date, strike_price, option_type, script, lotsize, multiplier,expdate1 ) " & _
                                                   " SELECT Cd_contractcsv.Field1, Cd_contractcsv.Field2, Cd_contractcsv.Field3, Cd_contractcsv.Field4, Cd_contractcsv.Field14, Cd_contractcsv.Field5, format(Val([Cd_contractcsv].[Field6])/10000000,'#0.0000'), Cd_contractcsv.Field7, IIf((Trim(Left([Cd_contractcsv].[Field7],1))='C' Or Trim(Left([Cd_contractcsv].[Field7],1))='P'),[Cd_contractcsv].[Field3] & '  ' & [Cd_contractcsv].[Field4] & '  ' & Format(DateAdd('s',Val([Cd_contractcsv].[Field5]),CDate('1/1/1980')),'ddmmmyyyy') & '  ' & Format(Val([Cd_contractcsv].[Field6])/10000000,'0.0000') & '  ' & [Cd_contractcsv].[Field7],[Cd_contractcsv].[Field3] & '  ' & [Cd_contractcsv].[Field4] & '  ' & Format(DateAdd('s',Val([Cd_contractcsv].[Field5]),CDate('1/1/1980')),'ddmmmyyyy')) AS Script, Cd_contractcsv.Field8, Cd_contractcsv.Field71 ,CDate(Format(DateAdd('s',[Cd_contractcsv].[Field5],'1/1/1980'),'mmm/dd/yyyy')) as expdate1" & _
                                                   " FROM Cd_contractcsv " & _
                                                   " WHERE TRIM(Cd_contractcsv.Field3)<>'' and  TRIM(Cd_contractcsv.Field3)<>'FinInstrmNm' And TRIM(Cd_contractcsv.Field5)<>'-1' ;"

        '" WHERE ((TRIM(Cd_contract.Field3)<>''));"
        'And (Val([Cd_Contract].[Field7]) > 0)); "
        '" & DateDiff(DateInterval.Second, CDate("1-1-1980"), Today) & "));"
        'Private Insert_Contract As String = "INSERT INTO [" & DbPath & "].Contract ( Token, Asset_Tokan, InstrumentName, Symbol, series, expiry_date, strike_price, option_type, script, lotsize,OScript ) " & _
        '                                   " SELECT Contract.Field1 , Contract.Field2 , Contract.Field3 , Contract.Field4, Contract.Field5, Contract.Field7, Val([Contract].[Field8])/100, Contract.Field9, IIf((Trim(Left([contract].[Field9],1))='C' Or Trim(Left([contract].[Field9],1))='P'),[Contract].[Field3] & '  ' & [Contract].[Field4] & '  ' & Format(DateAdd('s',Val([Contract].[Field7]),CDate('1/1/1980')),'ddmmmyyyy') & '  ' & Format(Val([Contract].[Field8])/100,'0.00') & '  ' & [Contract].[Field9],[Contract].[Field3] & '  ' & [Contract].[Field4] & '  ' & Format(DateAdd('s',Val([Contract].[Field7]),CDate('1/1/1980')),'ddmmmyyyy')) AS Script, Contract.Field32 , " & _
        '                                   " IIf((Trim(Left([contract].[Field9],1))='C' Or Trim(Left([contract].[Field9],1))='P'),[Contract].[Field3] & '  ' & [Contract].[Field4] & '  ' & Format(DateAdd('s',Val([Contract].[Field7]),CDate('1/1/1980')),'ddmmmyyyy') & '  ' & Format(Val([Contract].[Field8]/100),'0.00') & '  ' &  iif((Trim(Left([contract].[Field9],1))='C'),'PE','CE') ,'') AS OScript" & _
        '                                   " FROM Contract " & _
        '                                   " WHERE (((Trim([Contract].[Field3]))<>''));"
        'Private Insert_Contractcsv As String = "INSERT INTO [" & DbPath & "].Contract ( Token, Asset_Tokan, InstrumentName, Symbol, series, expiry_date, strike_price, option_type, script, lotsize,OScript,expdate1) " &
        '    "SELECT Contractcsv.Field1 , Contractcsv.Field2 , Contractcsv.Field3 , Contractcsv.Field4, Contractcsv.Field16 , Contractcsv.Field5, Val([Contractcsv].[Field6])/100, Contractcsv.Field7, IIf((Trim(Left([Contractcsv].[Field7],1))='C' Or Trim(Left([Contractcsv].[Field7],1))='P'),[Contractcsv].[Field3] & '  ' & [Contractcsv].[Field4] & '  ' & Format(DateAdd('s',Val([Contractcsv].[Field5]),CDate('1/1/1980')),'ddmmmyyyy') & '  ' & Format(Val([Contractcsv].[Field6])/100,'0.00') & '  ' & [Contractcsv].[Field7],[Contractcsv].[Field3] & '  ' & [Contractcsv].[Field4] & '  ' & Format(DateAdd('s',Val([Contractcsv].[Field5]),CDate('1/1/1980')),'ddmmmyyyy')) AS Script " &
        '    ", Contractcsv.Field9 ,  IIf((Trim(Left([Contractcsv].[Field7],1))='C' Or Trim(Left([Contractcsv].[Field7],1))='P'),[Contractcsv].[Field3] & '  ' & [Contractcsv].[Field4] & '  ' & Format(DateAdd('s',Val([Contractcsv].[Field5]),CDate('1/1/1980')),'ddmmmyyyy') & '  ' & Format(Val([Contractcsv].[Field6]/100),'0.00') & '  ' &  iif((Trim(Left([Contractcsv].[Field7],1))='C'),'PE','CE') ,'') AS OScript,CDate(Format(DateAdd('s',[Contractcsv].[Field5],'1/1/1980'),'mmm/dd/yyyy')) as expdate1  " &
        '    "FROM Contractcsv   " &
        '    "WHERE (((Trim([Contractcsv].[Field3]))<>'FinInstrmNm'));"
        Private Insert_Contractcsv As String =
"INSERT INTO [" & DbPath & "].Contract (
    Token, Asset_Tokan, InstrumentName, Symbol, series, expiry_date,
    strike_price, option_type, script, lotsize, OScript, expdate1,exchange
)
SELECT 
    Field1,
    Field2,
    Field3,
    Field4,
    Field16,

    Val(Field5)  AS expiry_date,        -- NUMBER 100% OK

    Val(Field6)/100 AS strike_price,
    Field7 AS option_type,

    IIf(
        Left(Trim(Field7),1)='C' OR Left(Trim(Field7),1)='P',
        Field3 & '  ' & Field4 & '  ' &
        Format(DateAdd('s', Val(Field5), #1/1/1980#),'ddmmmyyyy') & '  ' &
        Format(Val(Field6)/100, '0') & '  ' & Field7,
        Field3 & '  ' & Field4 & '  ' &
        Format(DateAdd('s', Val(Field5), #1/1/1980#),'ddmmmyyyy')
    ) AS Script,

    Val(Field9) AS lotsize,

    IIf(
        Left(Trim(Field7),1)='C' OR Left(Trim(Field7),1)='P',
        Field3 & '  ' & Field4 & '  ' &
        Format(DateAdd('s', Val(Field5), #1/1/1980#),'ddmmmyyyy') & '  ' &
        Format(Val(Field6)/100,'0') & '  ' &
        IIf(Left(Field7,1)='C','PE','CE'),
        ''
    ) AS OScript,

    DateAdd('s', Val(Field5), #1/1/1980#) AS expdate1,

    'NSE'
FROM Contractcsv
WHERE Trim(Field3) <> 'FinInstrmNm';"



        'Private Insert_Currency_Contractcsv As String = "INSERT INTO [" & DbPath & "].Currency_Contract (Token, Asset_Tokan, InstrumentName, Symbol, series, expiry_date, strike_price, option_type, script, lotsize, multiplier,expdate1 ) " &
        '                                           " SELECT Cd_contractcsv.Field1, Cd_contractcsv.Field2, Cd_contractcsv.Field3, Cd_contractcsv.Field4, Cd_contractcsv.Field14, Cd_contractcsv.Field5, format(Val([Cd_contractcsv].[Field6])/10000000,'#0.0000'), Cd_contractcsv.Field7, IIf((Trim(Left([Cd_contractcsv].[Field7],1))='C' Or Trim(Left([Cd_contractcsv].[Field7],1))='P'),[Cd_contractcsv].[Field3] & '  ' & [Cd_contractcsv].[Field4] & '  ' & Format(DateAdd('s',Val([Cd_contractcsv].[Field5]),CDate('1/1/1980')),'ddmmmyyyy') & '  ' & Format(Val([Cd_contractcsv].[Field6])/10000000,'0.0000') & '  ' & [Cd_contractcsv].[Field7],[Cd_contractcsv].[Field3] & '  ' & [Cd_contractcsv].[Field4] & '  ' & Format(DateAdd('s',Val([Cd_contractcsv].[Field5]),CDate('1/1/1980')),'ddmmmyyyy')) AS Script, Cd_contractcsv.Field8, Cd_contractcsv.Field71 ,CDate(Format(DateAdd('s',[Cd_contractcsv].[Field5],'1/1/1980'),'mmm/dd/yyyy')) as expdate1" &
        '                                           " FROM Cd_contractcsv " &
        '                                           " WHERE TRIM(Cd_contractcsv.Field3)<>'' and  TRIM(Cd_contractcsv.Field3)<>'FinInstrmNm' And TRIM(Cd_contractcsv.Field5)<>'-1' ;"
        Private Insert_Securitycsv As String = "INSERT INTO [" & DbPath & "].Security ( token, symbol, series, isin, script ) " &
                                           " SELECT Securitycsv.Field1, Securitycsv.Field2, Securitycsv.Field3, Securitycsv.Field5, [Field2] & '  ' & [Field3] AS Script " &
                                           " FROM Securitycsv " &
                                           " WHERE ((([Field3])<>'SctySrs'));"

        Private Insert_Contract As String = "INSERT INTO [" & DbPath & "].Contract ( Token, Asset_Tokan, InstrumentName, Symbol, series, expiry_date, strike_price, option_type, script, lotsize,OScript,expdate1,exchange) " &
                                         " SELECT Contract.Field1 , Contract.Field2 , Contract.Field3 , Contract.Field4, Contract.Field5, Contract.Field7, Val([Contract].[Field8])/100, Contract.Field9, IIf((Trim(Left([contract].[Field9],1))='C' Or Trim(Left([contract].[Field9],1))='P'),[Contract].[Field3] & '  ' & [Contract].[Field4] & '  ' & Format(DateAdd('s',Val([Contract].[Field7]),CDate('1/1/1980')),'ddmmmyyyy') & '  ' & Format(Val([Contract].[Field8])/100,'0.00') & '  ' & [Contract].[Field9],[Contract].[Field3] & '  ' & [Contract].[Field4] & '  ' & Format(DateAdd('s',Val([Contract].[Field7]),CDate('1/1/1980')),'ddmmmyyyy')) AS Script, Contract.Field32 , " &
                                         " IIf((Trim(Left([contract].[Field9],1))='C' Or Trim(Left([contract].[Field9],1))='P'),[Contract].[Field3] & '  ' & [Contract].[Field4] & '  ' & Format(DateAdd('s',Val([Contract].[Field7]),CDate('1/1/1980')),'ddmmmyyyy') & '  ' & Format(Val([Contract].[Field8]/100),'0.00') & '  ' &  iif((Trim(Left([contract].[Field9],1))='C'),'PE','CE') ,'') AS OScript,CDate(Format(DateAdd('s',[Contract].[Field7],'1/1/1980'),'mmm/dd/yyyy')) as expdate1,'NSE'" &
                                         " FROM Contract " &
                                         " WHERE (((Trim([Contract].[Field3]))<>''));"


        Private Insert_Security As String = "INSERT INTO [" & DbPath & "].Security ( token, symbol, series, isin, script,exchange ) " &
                                           " SELECT Security.Field1, Security.Field2, Security.Field3, Security.Field46, [Field2] & '  ' & [Field3] AS Script,'NSE' " &
                                           " FROM Security " &
                                           " WHERE (((val([Field1]))<>0));"

        Private Insert_Bhavcopy As String = "INSERT INTO [" & DbPath & "].TblBhavcopy  " &
                                                " SELECT * FROM Bhavcopy "

        Private Insert_BhavcopyNew As String = "INSERT INTO [" & DbPath & "].TblBhavcopy  " &
                                                " Select  iif(FinInstrmTp ='STO','OPTSTK',iif(FinInstrmTp='STF','FUTSTK',iif( FinInstrmTp='IDF' ,'FUTIDX',iif( FinInstrmTp='IDO' ,'OPTIDX','')))) as INSTRUMENT  , TckrSymb as [SYMBOL],XpryDt as EXPIRY_DT,iif(FinInstrmTp='STF','0',iif( FinInstrmTp='IDF' ,'0' , StrkPric))  as STRIKE_PR,iif(FinInstrmTp='STF','XX',iif( FinInstrmTp='IDF' ,'XX' , OptnTp)) as OPTION_TYP,OpnPric as [OPEN],HghPric AS [HIGH],LwPric as [LOW],ClsPric as [CLOSE],SttlmPric as SETTLE_PR,0 as CONTRACTS,TtlTrfVal as VAL_INLAKH,OpnIntrst as OPEN_INT,ChngInOpnIntrst as CHG_IN_OI,TradDt  as [TIMESTAMP],'NSE' AS exchange from Bhavcopyfo;"




        Public ReadOnly Insert_BhavcopyNewNse As String =
        "INSERT INTO [" & DbPath & "].TblBhavcopy " &
        "(" &
        " INSTRUMENT, SYMBOL, EXPIRY_DT, STRIKE_PR, OPTION_TYP, " &
        " [OPEN], [HIGH], [LOW], [CLOSE], SETTLE_PR, " &
        " CONTRACTS, VAL_INLAKH, OPEN_INT, CHG_IN_OI, [TIMESTAMP], exchange " &
        ") " &
        "SELECT " &
        " IIf(FinInstrmTp='STO','OPTSTK', " &
        "     IIf(FinInstrmTp='STF','FUTSTK', " &
        "         IIf(FinInstrmTp='IDF','FUTIDX', " &
        "             IIf(FinInstrmTp='IDO','OPTIDX','')))) AS INSTRUMENT, " &
        " TckrSymb AS [SYMBOL], " &
        " XpryDt AS EXPIRY_DT, " &
        " IIf(FinInstrmTp IN ('STF','IDF'),0,StrkPric) AS STRIKE_PR, " &
        " IIf(FinInstrmTp IN ('STF','IDF'),'XX',OptnTp) AS OPTION_TYP, " &
        " OpnPric AS [OPEN], " &
        " HghPric AS [HIGH], " &
        " LwPric AS [LOW], " &
        " ClsPric AS [CLOSE], " &
        " SttlmPric AS SETTLE_PR, " &
        " 0 AS CONTRACTS, " &
        " TtlTrfVal AS VAL_INLAKH, " &
        " OpnIntrst AS OPEN_INT, " &
        " ChngInOpnIntrst AS CHG_IN_OI, " &
        " TradDt AS [TIMESTAMP], " &
        " 'NSE' AS exchange " &
        "FROM Bhavcopyfo;"





#End Region

#Region "Delete Query"
        REM Contract
        Private Delete_Currency_Contract As String = "Delete * From [" & DbPath & "].Currency_Contract;"
        Private Delete_Contract As String = "Delete * From  [" & DbPath & "].Contract WHERE exchange='NSE';"
        Private Delete_Security As String = "Delete * From  [" & DbPath & "].Security WHERE exchange='NSE';"
        Private Delete_Bhavcopy As String = "Delete * From  [" & DbPath & "].TblBhavcopy WHERE exchange='NSE';"
#End Region

        Private Function Insert_Trd(ByVal sTrd As String) As String


            Return "INSERT INTO [" & DbPath & "]." & sTrd & " " & _
                                       " SELECT *  FROM " & sTrd & "; "



        End Function

        Private Function Delete_Trd(ByVal sTrd As String) As String
            Return "Delete * From  [" & DbPath & "]." & sTrd & ";"
        End Function


        Public Sub FlushAllTrd()
            DA.ParamClear()
            Dim TableList() As String
            Dim i As Integer
            TableList = "BseEqTrd,GetFoTrd,GetCurTrd,GetEqTrd,NowFoTrd,NowCurTrd,NowEqTrd,NotFoTrd,NotCurTrd,NotEqTrd,OdiFoTrd,OdiCurTrd,OdiEqTrd,NseFoTrd,NseCurTrd,NeaFoTrd,NeaCurTrd,TblBhavcopy,FisFoTrd,FadFoTrd,FisEqTrd,FadEqTrd,FisCurTrd,FadCurTrd".Split(",")
            For i = 0 To UBound(TableList)
                DA.ExecuteNonQuery(Delete_Trd(TableList(i)), CommandType.Text)
            Next
        End Sub

#Region "Notis_Methid"
        Public Sub ImportNotFoTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("NotFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("NotFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Sub ImportNotCurTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("NotCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)
                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("NotCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)
            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Sub ImportNotEQTrd()
            Try
                REM Delete 
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("NotEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import 
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("NotEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
#End Region

#Region "Fist_Methods"
        Public Sub ImportFisEqTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("FisEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("FisEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Function DeleteFisFoTrd() As Integer
            Try

                Dim qry As String
                qry = "DELETE  FROM FisFoTrd;"
                
                data_access.ExecuteQuery(qry)

            Catch ex As Exception
                MsgBox("Error to Delete Data")

            End Try


        End Function
        Public Function InsertFisFoTrd() As Integer
            Try
                Dim qry As String
                'qry = "INSERT INTO FisFoTrd  SELECT *  FROM " & sTrd & "; "
                Dim PATH As String = "C:\Data\"
                'qry = "SELECT * INTO [FisFoTrd] FROM [Text;Database='" + PATH + "';Hdr=No].[FisFoTrd.txt]"
                'qry = "INSERT INTO FisFoTrd  SELECT *  FROM [Text;Database='" + PATH + "';Hdr=No].[FisFoTrd.txt]; "
                '            qry = "INSERT INTO FisFoTrd " &
                '"SELECT * " &
                '"FROM [Text;FMT=Delimited;HDR=No;CharacterSet=850;DATABASE=C:\Data].FisFoTrd.txt;"
                qry = "INSERT INTO FisFoTrd SELECT cstr(F1 & '') as Field1,cstr(F2 & '') as Field2,cstr(F3 & '') as Field3,cstr(F4 & '') as Field4,cstr(F5 & '') as Field5,format(val(F6),'#.00') as Field6,cstr(F7 & '') as Field7 ,cstr(F8 & '') as Field8,cstr(F9 & '') as Field9,cstr(F10 & '') as Field10,cstr(F11 & '') as Field11,cstr(F12 & '')  as Field12,cstr(F13 & '') as Field13,cstr(F14 & '') as Field14,cstr(F15 & '') as Field15,cstr(F16 & '') as Field16,cstr(F17 & '') as Field18,cstr(F19 & '') as Field19,cstr(F20 & '') as Field20,cstr(F21 & '') as Field21,cstr(F22 & '') as Field22,cstr(F23 & '') as Field23,cstr(F24 & '') as Field24,cstr(F25 & '') as Field25,cstr(F26 & '') as Field26 FROM [Text;FMT=Delimited;HDR=No;CharacterSet=850;DATABASE=C:\Data].FisFoTrd.txt;"

                data_access.ParamClear()
                data_access.Cmd_Text = qry
                data_access.cmd_type = CommandType.Text
                data_access.ExecuteNonQuery()
                'data_access.cmd_type = CommandType.StoredProcedure

               
              
            Catch ex As Exception
                MsgBox("Error to Delete Data")

            End Try


        End Function
        Public Sub ImportFisFoTrd()
            Try
                REM Delete Contract
                'DA.ParamClear()
                'DA.Cmd_Text = Delete_Trd("FisFoTrd")
                'DA.ExecuteNonQuery(CommandType.Text)
                DeleteFisFoTrd()
                REM import Contract
                'InsertFisFoTrd()
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("FisFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub

        Public Sub ImportFadFoTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("FadFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("FadFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub



        Public Sub ImportFadEqTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("FadEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("FadEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub


        Public Sub ImportFisCurTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("FisCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("FisCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub

        Public Sub ImportFadCurTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("FadCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("FadCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
#End Region

#Region "Gets_Methods"

        Public Sub ImportGetFoTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("GetFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("GetFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub

        Public Sub ImportGetBseFoTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("BSEGetFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("BSEGetFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub

        Public Sub ImportGetCurTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("GetCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("GetCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Sub ImportGetEQTrd()
            Try
                REM Delete 
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("GetEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import 
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("GetEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Sub ImportBSEEQTrd()
            Try
                REM Delete 
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("BseEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import 
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("BSEEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
#End Region

#Region "Now_Methods"

        Public Sub ImportNowFoTrd()
            Try
              
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("NowFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("NowFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)




            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Sub ImportNowCurTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("NowCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("NowCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Sub ImportNowEQTrd()
            Try
                REM Delete 
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("NowEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import 
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("NowEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
#End Region

#Region "Odin_Methods"

        Public Sub ImportOdiFoTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("OdiFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("OdiFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Sub ImportOdiCurTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("OdiCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("OdiCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Sub ImportOdiMcxCurTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("MOdiCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("MOdiCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Sub ImportOdiEQTrd()
            Try
                REM Delete 
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("OdiEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import 
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("OdiEqTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
#End Region

#Region "Nse_Methods"

        Public Sub ImportNseFoTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("NseFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("NseFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Sub ImportNseCurTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("NseCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("NseCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
#End Region

#Region "Neat_Methods"

        Public Sub ImportNeaFoTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("NeaFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("NeaFoTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
     
        Public Sub ImportNeaCurTrd()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Trd("NeaCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                DA.Cmd_Text = Insert_Trd("NeaCurTrd")
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
#End Region

#Region "Method"
        Public Sub ImportContract()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Contract
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                If flgcsvcontract = True Then
                    DA.Cmd_Text = Insert_Contractcsv
                Else
                    DA.Cmd_Text = Insert_Contract
                End If

                DA.ExecuteNonQuery(CommandType.Text)


              


            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Sub ImportSecurity()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Security
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                If flgcsvcontract = True Then
                    DA.Cmd_Text = Insert_Securitycsv
                Else
                    DA.Cmd_Text = Insert_Security
                End If

                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub
        Public Sub ImportCurrency()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Currency_Contract
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()
                If flgcsvcontract = True Then
                    DA.Cmd_Text = Insert_Currency_Contractcsv
                Else
                    DA.Cmd_Text = Insert_Currency_Contract
                End If
                ' DA.Cmd_Text = Insert_Currency_Contract
                DA.ExecuteNonQuery(CommandType.Text)

            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub

        ''' <summary>
        ''' ImportBhavCopy() To import Bhavcopy Table From ImportDB To Greek database
        ''' Previous Records Delete And New Record Will be Append in TblBhavCopy 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ImportBhavCopy()
            Try
                REM Delete Contract
                DA.ParamClear()
                DA.Cmd_Text = Delete_Bhavcopy
                DA.ExecuteNonQuery(CommandType.Text)

                REM import Contract
                DA.ParamClear()

                If NEW_BHAVCOPY = 1 Then

                    DA.Cmd_Text = Insert_BhavcopyNewNse
                    DA.ExecuteNonQuery(CommandType.Text)
                Else

                    DA.Cmd_Text = Insert_Bhavcopy
                    DA.ExecuteNonQuery(CommandType.Text)
                End If


            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
            End Try
        End Sub


#End Region

        Public Sub New()
            Dim FileList() As String
            FileList = "cd_contract.txt,contract.txt,security.txt,GetCurTrd.txt,GetEqTrd.txt,GetFoTrd.txt,NeaCurTrd.txt,NeaFoTrd.txt,NotCurTrd.txt,NotEqTrd.txt,NotFoTrd.txt,NowCurTrd.txt,NowEqTrd.txt,NowFoTrd.txt,NseCurTrd.txt,NseFoTrd.txt,OdiCurTrd.txt,MOdiCurTrd.txt,OdiEqTrd.txt,OdiFoTrd.txt,Bhavcopy.txt,FisFoTrd.txt,FadFoTrd.txt,FisEqTrd.txt,FadEqTrd.txt,FisCurTrd.txt,FadCurTrd.txt".Split(",")
            ValidateDirnFile(FileList)
        End Sub
        Public Sub New(ByVal i As Single)
            Dim FileList() As String
            FileList = "cd_contract.txt,contract.txt,security.txt".Split(",")
            ValidateDirnFile(FileList)
        End Sub

        Private Sub ValidateDirnFile(ByVal FileList() As String)
            Try
                Dim i As Integer
                'If My.Computer.FileSystem.DirectoryExists("C:\Data") = False Then
                '    Call My.Computer.FileSystem.CreateDirectory("C:\Data")
                'End If
                'For i = 0 To UBound(FileList)
                '    If File.Exists("C:\Data\" & FileList(i)) Then
                '        File.Delete("C:\Data\" & FileList(i))
                '        FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data\" & FileList(i), OpenMode.Append)
                '        FileSystem.Reset()
                '        'File.Create("C:\Data\" & FileList(i))

                '    Else
                '        'File.Create("C:\Data\" & FileList(i))
                '        FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data\" & FileList(i), OpenMode.Append)
                '        FileSystem.Reset()
                '    End If

                'Next

                If verVersion = "MI" Then
                    If INSTANCEname = "PRIMARY" Then
                        If My.Computer.FileSystem.DirectoryExists("C:\Data") = False Then
                            Call My.Computer.FileSystem.CreateDirectory("C:\Data")
                        End If
                        For i = 0 To UBound(FileList)
                            If File.Exists("C:\Data\" & FileList(i)) Then
                                File.Delete("C:\Data\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                                'File.Create("C:\Data\" & FileList(i))

                            Else
                                'File.Create("C:\Data\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                            End If

                        Next
                    ElseIf INSTANCEname = "SECONDARY" Then
                        i = 0
                        If My.Computer.FileSystem.DirectoryExists("C:\Data2") = False Then
                            Call My.Computer.FileSystem.CreateDirectory("C:\Data2")
                        End If
                        For i = 0 To UBound(FileList)
                            If File.Exists("C:\Data2\" & FileList(i)) Then
                                File.Delete("C:\Data2\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data2\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                                'File.Create("C:\Data\" & FileList(i))

                            Else
                                'File.Create("C:\Data\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data2\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                            End If

                        Next
                    ElseIf INSTANCEname = "THIRD" Then
                        i = 0
                        If My.Computer.FileSystem.DirectoryExists("C:\Data3") = False Then
                            Call My.Computer.FileSystem.CreateDirectory("C:\Data3")
                        End If
                        For i = 0 To UBound(FileList)
                            If File.Exists("C:\Data3\" & FileList(i)) Then
                                File.Delete("C:\Data3\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data3\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                                'File.Create("C:\Data\" & FileList(i))

                            Else
                                'File.Create("C:\Data\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data3\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                            End If

                        Next
                    ElseIf INSTANCEname = "FOURTH" Then
                        i = 0
                        If My.Computer.FileSystem.DirectoryExists("C:\Data4") = False Then
                            Call My.Computer.FileSystem.CreateDirectory("C:\Data4")
                        End If
                        For i = 0 To UBound(FileList)
                            If File.Exists("C:\Data4\" & FileList(i)) Then
                                File.Delete("C:\Data4\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data4\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                                'File.Create("C:\Data\" & FileList(i))

                            Else
                                'File.Create("C:\Data\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data4\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                            End If

                        Next
                    ElseIf INSTANCEname = "FIFTH" Then
                        i = 0
                        If My.Computer.FileSystem.DirectoryExists("C:\Data5") = False Then
                            Call My.Computer.FileSystem.CreateDirectory("C:\Data5")
                        End If
                        For i = 0 To UBound(FileList)
                            If File.Exists("C:\Data5\" & FileList(i)) Then
                                File.Delete("C:\Data5\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data5\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                                'File.Create("C:\Data\" & FileList(i))

                            Else
                                'File.Create("C:\Data\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data5\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                            End If

                        Next
                    ElseIf INSTANCEname = "SIXTH" Then
                        i = 0
                        If My.Computer.FileSystem.DirectoryExists("C:\Data6") = False Then
                            Call My.Computer.FileSystem.CreateDirectory("C:\Data6")
                        End If
                        For i = 0 To UBound(FileList)
                            If File.Exists("C:\Data6\" & FileList(i)) Then
                                File.Delete("C:\Data6\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data6\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                                'File.Create("C:\Data\" & FileList(i))

                            Else
                                'File.Create("C:\Data\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data6\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                            End If

                        Next
                    ElseIf INSTANCEname = "SEVENTH" Then
                        i = 0
                        If My.Computer.FileSystem.DirectoryExists("C:\Data7") = False Then
                            Call My.Computer.FileSystem.CreateDirectory("C:\Data7")
                        End If
                        For i = 0 To UBound(FileList)
                            If File.Exists("C:\Data7\" & FileList(i)) Then
                                File.Delete("C:\Data7\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data7\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                                'File.Create("C:\Data\" & FileList(i))

                            Else
                                'File.Create("C:\Data\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data7\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                            End If

                        Next
                    ElseIf INSTANCEname = "EIGHT" Then
                        i = 0
                        If My.Computer.FileSystem.DirectoryExists("C:\Data8") = False Then
                            Call My.Computer.FileSystem.CreateDirectory("C:\Data8")
                        End If
                        For i = 0 To UBound(FileList)
                            If File.Exists("C:\Data8\" & FileList(i)) Then
                                File.Delete("C:\Data8\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data8\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                                'File.Create("C:\Data\" & FileList(i))

                            Else
                                'File.Create("C:\Data\" & FileList(i))
                                FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data8\" & FileList(i), OpenMode.Append)
                                FileSystem.Reset()
                            End If

                        Next

                    End If
                Else


                    If My.Computer.FileSystem.DirectoryExists("C:\Data") = False Then
                        Call My.Computer.FileSystem.CreateDirectory("C:\Data")
                    End If
                    For i = 0 To UBound(FileList)
                        If File.Exists("C:\Data\" & FileList(i)) Then
                            File.Delete("C:\Data\" & FileList(i))
                            FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data\" & FileList(i), OpenMode.Append)
                            FileSystem.Reset()
                            'File.Create("C:\Data\" & FileList(i))

                        Else
                            'File.Create("C:\Data\" & FileList(i))
                            FileSystem.FileOpen(FileSystem.FreeFile, "C:\Data\" & FileList(i), OpenMode.Append)
                            FileSystem.Reset()
                        End If

                    Next

                End If


            Catch ex As Exception
                ', FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.ThrowException
                MsgBox(ex.Message)
            End Try
        End Sub


        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class


    'Class Created By Viral on 19 Jul-11 
    Public MustInherit Class DA

#Region "variable"
        Shared _con As OleDbConnection
        Shared _adp As OleDbDataAdapter
        Shared _cmd As OleDbCommand
        Shared _cmd_type As CommandType = CommandType.StoredProcedure
        Shared _cmd_text As String
        Shared _connection_string As String
        Shared _paramtable As DataTable
        Shared _DtPrimaryExp As New DataTable
        Shared _tra As OleDbTransaction
#End Region

#Region "Method"

        Public Sub New()

        End Sub

        Public Shared Property Cmd_Text() As String
            Get
                Return _cmd_text
            End Get
            Set(ByVal value As String)
                _cmd_text = value
            End Set
        End Property

        Public Shared Property cmd_type() As CommandType
            Get
                Return _cmd_type
            End Get
            Set(ByVal value As CommandType)
                _cmd_type = value
            End Set
        End Property

        Private Shared ReadOnly Property Connection_string() As String
            Get
                Try
                    Dim str As String = ConfigurationSettings.AppSettings("Importdb")
                    '_connection_string = ConfigurationSettings.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str
                    '_connection_string = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=True;Data Source=D:\FinIdeas Projects\VolHedge 4.0.0.48 H13\volhedge\bin\x86\Debug/greekP.mdb;Jet OLEDB:Database Password=Admin"
                    _connection_string = ConfigurationSettings.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str & ";Jet OLEDB:Database Password=" & clsGlobal.glbAcessPassWord & ""

                    If _connection_string = "" Then
                        Throw New ApplicationException("Connection String is not initialize")
                    End If
                    Return _connection_string
                Catch ex As Exception
                    'MsgBox(ex.ToString)
                    'FSTimerLogFile.WriteLine("Data_access::Connection_string:-" & ex.ToString)
                    Throw New ApplicationException("Connection String is not initialize")
                End Try
            End Get
        End Property
        Private Shared Sub open_connection()
            Try
                If Connection_string <> "" Or (_con Is Nothing) Then
                    _con = New OleDbConnection(Connection_string)
                    _con.Open()
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
                'FSTimerLogFile.WriteLine("Data_access::open_connection:-" & ex.ToString)
                Throw New ApplicationException("Connection Error")
            End Try
        End Sub
        Private Shared Sub Con_Transaction()
            If _con.State = ConnectionState.Open Then
                _tra = _con.BeginTransaction()
            End If
        End Sub
        Private Shared Sub Con_Commit()
            _tra.Commit()
        End Sub
        Private Shared Sub Con_Rollback()
            _tra.Rollback()
        End Sub
        Private Shared Sub Close_Connection()
            If _con.State = ConnectionState.Open Then
                _con.Close()
            End If
            cmd_type = CommandType.StoredProcedure
        End Sub

        Public Shared Sub ParamClear()
            _paramtable = New DataTable()
            _paramtable.Columns.Add("ParameterName", GetType(String))
            _paramtable.Columns.Add("oleDbType", GetType(OleDbType))
            _paramtable.Columns.Add("Size", GetType(Integer))
            _paramtable.Columns.Add("Value", GetType(Object))
        End Sub

        Public Shared Sub AddParam(ByVal param_id As String, ByVal oledbtype As OleDbType, ByVal size As Integer, ByVal value As Object)
            Dim drow As DataRow = _paramtable.NewRow()
            drow("ParameterName") = param_id
            drow("oledbtype") = oledbtype
            drow("Size") = size
            If value Is Nothing Then
                drow("Value") = DBNull.Value
            Else
                drow("Value") = value
            End If
            _paramtable.Rows.Add(drow)
        End Sub
   

        Public Shared Function ExecuteNonQuery(ByVal cmd_type As CommandType) As DataTable
            If _DtPrimaryExp.Columns.Count = 0 Then
                _DtPrimaryExp.Columns.Add("ParameterName", GetType(String))
                _DtPrimaryExp.Columns.Add("oleDbType", GetType(OleDbType))
                _DtPrimaryExp.Columns.Add("Size", GetType(Integer))
                _DtPrimaryExp.Columns.Add("Value", GetType(Object))
            End If
            _DtPrimaryExp.Rows.Clear()
            Try
                Dim result As String = ""
                open_connection()
                _cmd = New OleDbCommand()
                _cmd.CommandType = cmd_type
                _cmd.CommandText = _cmd_text
                Con_Transaction()
                _cmd.Connection = _con
                _cmd.Transaction = _tra
                If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                    AddParamToSQLCmd()
                End If
                'divyesh
                Try
                    result = Convert.ToString(_cmd.ExecuteNonQuery())
                Catch ex1 As Exception
                    If ex1.Message.ToUpper.Contains("DUPLICATE VALUES IN THE INDEX, PRIMARY KEY, OR RELATIONSHIP") = True Then
                        For cnt As Integer = 0 To _paramtable.Rows.Count - 1
                            _DtPrimaryExp.ImportRow(_paramtable.Rows(cnt))
                        Next
                    Else
                        Throw New ApplicationException(ex1.Message)
                    End If
                End Try

                'result = Convert.ToString(_cmd.ExecuteNonQuery())
                Con_Commit()
                Close_Connection()
                Return _DtPrimaryExp 'result.ToString()
            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
                Con_Rollback()
            Finally
                Close_Connection()
            End Try
        End Function

        Public Shared Function ExecuteNonQuery(ByVal cmd_Text As String, ByVal cmd_type As CommandType) As DataTable
            If _DtPrimaryExp.Columns.Count = 0 Then
                _DtPrimaryExp.Columns.Add("ParameterName", GetType(String))
                _DtPrimaryExp.Columns.Add("oleDbType", GetType(OleDbType))
                _DtPrimaryExp.Columns.Add("Size", GetType(Integer))
                _DtPrimaryExp.Columns.Add("Value", GetType(Object))
            End If
            _DtPrimaryExp.Rows.Clear()
            Try
                Dim result As String = ""
                open_connection()
                _cmd = New OleDbCommand()
                _cmd.CommandType = cmd_type
                _cmd.CommandText = cmd_Text
                Con_Transaction()
                _cmd.Connection = _con
                _cmd.Transaction = _tra
                If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                    AddParamToSQLCmd()
                End If
                'divyesh
                Try
                    result = Convert.ToString(_cmd.ExecuteNonQuery())
                Catch ex1 As Exception
                    If ex1.Message.ToUpper.Contains("DUPLICATE VALUES IN THE INDEX, PRIMARY KEY, OR RELATIONSHIP") = True Then
                        For cnt As Integer = 0 To _paramtable.Rows.Count - 1
                            _DtPrimaryExp.ImportRow(_paramtable.Rows(cnt))
                        Next
                    Else
                        Throw New ApplicationException(ex1.Message)
                    End If
                End Try

                'result = Convert.ToString(_cmd.ExecuteNonQuery())
                Con_Commit()
                Close_Connection()
                Return _DtPrimaryExp 'result.ToString()
            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
                MsgBox(ex.ToString)
                Con_Rollback()
            Finally
                Close_Connection()
            End Try
        End Function

        ''REM By Viral on 10-July-11
        ''Private Shared Sub ImportContract(ByRef sFile As String)
        ''    Dim date1 As Date = "1/1/1980"
        ''    Dim strScript As String
        ''    ParamClear()
        ''    Cmd_Text = "contract_delete"
        ''    ExecuteNonQuery()
        ''    Try
        ''        Dim i As Integer = 0
        ''        Dim result As String = ""
        ''        open_connection()
        ''        Dim iline As New System.IO.StreamReader(sFile)
        ''        While iline.EndOfStream = False
        ''            If i > 0 Then
        ''                Dim items As String()
        ''                items = Split(iline.ReadLine, "|")
        ''                If items(2) <> "" Then
        ''                    strScript = ""
        ''                    '8 = OptType
        ''                    '2 =InstrumentName
        ''                    '3 = Symbol
        ''                    '6 = Exp_Date
        ''                    If items(8) <> "" Then
        ''                        If Mid(UCase(items(8)), 1, 1) = "C" Or Mid(UCase(items(8)), 1, 1) = "P" Then
        ''                            strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6) & ""), date1), "ddMMMyyyy") & "  " & CStr(Format(Val(items(7)), "###0.00")) & "  " & items(8)
        ''                        Else
        ''                            strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6) & ""), date1), "ddMMMyyyy")
        ''                        End If
        ''                    Else
        ''                        strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6) & ""), date1), "ddMMMyyyy")
        ''                    End If

        ''                    Dim StrSql As String = "INSERT INTO contract (token, asset_tokan, instrumentname, symbol, series, expiry_date, strike_price, option_type, script, lotsize )"
        ''                    StrSql = StrSql & "VALUES (" & Val(items(0)) & ", " & Val(items(1)) & ", '" & items(2) & "', '" & items(3) & "', '" & items(4) & "', " & Val(items(6)) & ", " & Val(items(7)) & ",'" & items(8) & "', '" & strScript & "', " & Val(items(31)) & ")"
        ''                    _cmd = New OleDbCommand()
        ''                    _cmd.CommandType = CommandType.Text
        ''                    _cmd.CommandText = StrSql
        ''                    _cmd.Connection = _con
        ''                    _cmd.ExecuteNonQuery()
        ''                End If
        ''            End If
        ''            i = i + 1
        ''        End While
        ''        iline.Close()
        ''        Close_Connection()
        ''    Catch ex As Exception
        ''        'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
        ''        'MsgBox(ex.ToString)
        ''    Finally
        ''        Close_Connection()
        ''    End Try
        ''End Sub
        ''REM By Viral on 10-July-11
        ''Private Shared Sub ImportSecurity(ByRef sFile As String)
        ''    Dim date1 As Date = "1/1/1980"
        ''    Dim strScript As String
        ''    ParamClear()
        ''    Cmd_Text = "delete_security"
        ''    ExecuteNonQuery()
        ''    Try
        ''        Dim i As Integer = 0
        ''        Dim result As String = ""
        ''        open_connection()
        ''        Dim iline As New System.IO.StreamReader(sFile)
        ''        Dim items As String()
        ''        items = Split(iline.ReadLine, "|")
        ''        While iline.EndOfStream = False
        ''            If i > 0 Then
        ''                items = Split(iline.ReadLine, "|")
        ''                If items(2) <> "" Then
        ''                    strScript = ""
        ''                    '0 = Token
        ''                    '1 = Symbol
        ''                    '2 = Series
        ''                    '46 = Isin
        ''                    Dim StrSql As String = "INSERT INTO Security (token, symbol, series, isin, script )"
        ''                    StrSql = StrSql & "VALUES (" & Val(items(0)) & ", '" & items(1) & "', '" & items(2) & "', '" & items(46) & "', '" & items(1) & "  " & items(2) & "')"
        ''                    _cmd = New OleDbCommand()
        ''                    _cmd.CommandType = CommandType.Text
        ''                    _cmd.CommandText = StrSql
        ''                    _cmd.Connection = _con
        ''                    _cmd.ExecuteNonQuery()
        ''                End If
        ''            End If
        ''            i = i + 1
        ''        End While
        ''        iline.Close()
        ''        Close_Connection()
        ''    Catch ex As Exception
        ''        'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
        ''        'MsgBox(ex.ToString)
        ''    Finally
        ''        Close_Connection()
        ''    End Try
        ''End Sub
        ''REM By Viral on 10-July-11
        ''Private Shared Sub ImportCurrency(ByRef sFile As String)
        ''    Dim date1 As Date = "1/1/1980"
        ''    Dim strScript As String
        ''    Dim douStrikePrice As Double
        ''    ParamClear()
        ''    Cmd_Text = "Delete_Currency_Contract"
        ''    ExecuteNonQuery()

        ''    Try
        ''        Dim i As Integer = 0
        ''        Dim result As String = ""
        ''        open_connection()
        ''        Dim iline As New System.IO.StreamReader(sFile)
        ''        Dim items As String()
        ''        items = Split(iline.ReadLine, "|")
        ''        While iline.EndOfStream = False
        ''            If i > 0 Then
        ''                items = Split(iline.ReadLine, "|")
        ''                If items(2) <> "" Then
        ''                    strScript = ""
        ''                    douStrikePrice = 0
        ''                    '0 = token
        ''                    '1 = asset_Token
        ''                    '2 = InstrumentName
        ''                    '3 = Symbol
        ''                    '4 = Series
        ''                    '6 = Exp_Date
        ''                    '7 = Strike Price
        ''                    '8 = Option Type
        ''                    '31 = LotSize
        ''                    '51 = Multiplier
        ''                    douStrikePrice = Format(CDbl(items(7)) / 10000000, "#0.0000")
        ''                    If Not IsDBNull(items(8)) Then
        ''                        If Mid(UCase(items(8)), 1, 1) = "C" Or Mid(UCase(items(8)), 1, 1) = "P" Then
        ''                            strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6)), date1), "ddMMMyyyy") & "  " & CStr(Format(Val(douStrikePrice), "###0.0000")) & "  " & items(8)
        ''                        Else
        ''                            strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6)), date1), "ddMMMyyyy")
        ''                        End If
        ''                    Else
        ''                        strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6)), date1), "ddMMMyyyy")
        ''                    End If

        ''                    Dim StrSql As String = "INSERT INTO Currency_Contract ( token, asset_tokan, instrumentname, symbol, series, expiry_date, strike_price, option_type, script, lotsize, multiplier )"
        ''                    StrSql = StrSql & "VALUES (" & Val(items(0)) & ", " & Val(items(1)) & ", '" & items(2) & "', '" & items(3) & "', '" & items(4) & "', " & Val(items(6)) & ", " & Val(douStrikePrice) & ", '" & items(8) & "', '" & strScript & "', " & Val(items(31)) & ", " & Val(items(51)) & ")"
        ''                    _cmd = New OleDbCommand()
        ''                    _cmd.CommandType = CommandType.Text
        ''                    _cmd.CommandText = StrSql
        ''                    _cmd.Connection = _con
        ''                    _cmd.ExecuteNonQuery()
        ''                End If
        ''            End If
        ''            i = i + 1
        ''        End While
        ''        iline.Close()
        ''        Close_Connection()
        ''    Catch ex As Exception
        ''        'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
        ''        'MsgBox(ex.ToString)
        ''    Finally
        ''        Close_Connection()
        ''    End Try
        ''End Sub

        Public Shared Function ExecuteMultiple(ByVal parmcount As Integer) As DataTable
            If _DtPrimaryExp.Columns.Count = 0 Then
                _DtPrimaryExp.Columns.Add("ParameterName", GetType(String))
                _DtPrimaryExp.Columns.Add("oleDbType", GetType(OleDbType))
                _DtPrimaryExp.Columns.Add("Size", GetType(Integer))
                _DtPrimaryExp.Columns.Add("Value", GetType(Object))
            End If
            _DtPrimaryExp.Rows.Clear()
            Try
                Dim result As String = ""
                open_connection()
                Con_Transaction()
                If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                    Dim j As Integer
                    j = 0
                    For i As Integer = 0 To _paramtable.Rows.Count - 1 Step parmcount
                        _cmd = New OleDbCommand()
                        _cmd.CommandType = _cmd_type
                        _cmd.CommandText = _cmd_text
                        _cmd.Connection = _con
                        _cmd.Transaction = _tra
                        j = 0
                        For k As Integer = i To _paramtable.Rows.Count - 1
                            AddParamToSQLCmd(_paramtable.Rows(k))
                            j = j + 1
                            If j = parmcount Then
                                Exit For
                            End If
                        Next
                        'result = Convert.ToString(_cmd.ExecuteNonQuery())
                        Try
                            result = Convert.ToString(_cmd.ExecuteNonQuery())
                        Catch ex1 As Exception
                            If ex1.Message.ToUpper.Contains("DUPLICATE VALUES IN THE INDEX, PRIMARY KEY, OR RELATIONSHIP") = True Then
                                For cnt As Integer = i To (i + parmcount - 1)
                                    _DtPrimaryExp.ImportRow(_paramtable.Rows(cnt))
                                Next
                            Else
                                Throw New ApplicationException(ex1.Message)
                            End If
                        End Try
                    Next
                End If
                Con_Commit()
                Close_Connection()
                Return _DtPrimaryExp
            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteMultiple:-" & ex.ToString)
                MsgBox(ex.ToString)
                Con_Rollback()
            Finally
                Close_Connection()
            End Try
        End Function
        Public Shared Function ExecuteScalar() As String
            Try
                Dim result As String = ""
                open_connection()
                Con_Transaction()
                '_jro.RefreshCache(_con)
                _cmd = New OleDbCommand()
                _cmd.CommandType = _cmd_type
                _cmd.CommandText = _cmd_text
                _cmd.Connection = _con
                _cmd.Transaction = _tra

                If (IsDBNull(_paramtable) = False And _paramtable.Rows.Count > 0) Then
                    AddParamToSQLCmd()
                End If
                result = Convert.ToString(_cmd.ExecuteScalar())
                Con_Commit()
                Close_Connection()
                Return result.ToString()
            Catch ex As Exception
                ' FSTimerLogFile.WriteLine("Data_access::ExecuteScalar:-" & ex.ToString)
                MsgBox(ex.ToString)
                Con_Rollback()
            Finally
                Close_Connection()
            End Try
        End Function
        Private Shared Sub AddParamToSQLCmd(ByVal drow As DataRow)
            If Not IsDBNull(_cmd) = False Then
                Throw (New ApplicationException("Command not Initialized."))
            End If
            Dim newSqlParam As OleDbParameter = New OleDbParameter
            newSqlParam = New OleDbParameter()
            newSqlParam.ParameterName = drow("ParameterName").ToString()
            newSqlParam.OleDbType = CType(drow("oleDbType"), OleDbType)
            newSqlParam.Direction = ParameterDirection.Input
            If (Convert.ToInt16(drow("Size")) > 0) Then
                newSqlParam.Size = Convert.ToInt16(drow("Size"))
            End If
            If Not IsDBNull(drow("Value")) Then
                newSqlParam.Value = drow("Value")
            End If
            _cmd.Parameters.Add(newSqlParam)

        End Sub
        Private Shared Sub AddParamToSQLCmd()
            If Not IsDBNull(_cmd) = False Then
                Throw (New ApplicationException("Command not Initialized."))
            End If
            Dim newSqlParam As OleDbParameter = New OleDbParameter

            For Each drow As DataRow In _paramtable.Rows

                newSqlParam = New OleDbParameter()
                newSqlParam.ParameterName = drow("ParameterName").ToString()
                newSqlParam.OleDbType = CType(drow("oleDbType"), OleDbType)
                newSqlParam.Direction = ParameterDirection.Input
                If (Convert.ToInt16(drow("Size")) > 0) Then
                    newSqlParam.Size = Convert.ToInt16(drow("Size"))
                End If
                If Not IsDBNull(drow("Value")) Then
                    newSqlParam.Value = drow("Value")
                End If
                _cmd.Parameters.Add(newSqlParam)
            Next
        End Sub
        Public Shared Function FillList() As DataTable
            Try
                Dim list As DataTable = New DataTable()
                list.Rows.Clear()
                _adp = New OleDbDataAdapter
                open_connection()
                Con_Transaction()
                '  _jro.RefreshCache(_con)
                _cmd = New OleDbCommand()
                _cmd.CommandType = _cmd_type
                _cmd.CommandText = _cmd_text
                _cmd.Connection = _con
                _cmd.Transaction = _tra
                If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                    AddParamToSQLCmd()
                End If
                _adp.SelectCommand = _cmd
                _adp.Fill(list)
                _adp.Dispose()
                Con_Commit()
                Close_Connection()
                Return list
            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::FillList:-" & ex.ToString)
                MsgBox("data_access :: FillList() :: " & ex.ToString)
                Con_Rollback()
                Return Nothing
                '  MsgBox(ex.ToString)
                'Finally
                Close_Connection()
            End Try

        End Function
        Public Shared Function ExecuteReturn(ByVal Str As String) As Object
            Try
                open_connection()
                If _con.State = ConnectionState.Closed Then Con_Transaction()
                _cmd = New OleDbCommand()
                _cmd.CommandText = Str
                _cmd.Connection = _con
                _cmd.Transaction = _tra
                Str = _cmd.ExecuteScalar
                _cmd.Dispose()
                Con_Commit()
                Return Str
            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access::ExecuteReturn:-" & ex.ToString)
                Return ""
                Con_Rollback()
            Finally
                Close_Connection()
            End Try
        End Function
#End Region

    End Class
End Namespace
