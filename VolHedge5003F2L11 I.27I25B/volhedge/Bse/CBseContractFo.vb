Imports System.Data
Imports System.IO
Imports Sylvan.Data.Csv
Imports System.Globalization
Imports System.Threading.Tasks
Imports VolHedge.DAL
Imports System.Collections
Imports System.Collections.Concurrent
Imports System.Configuration
Imports System.Data.OleDb
'Class Created By Viral on 19 Jul-11 

Public Class CBseContractFo
    Inherits CBseContractsBase

    Public mMdbConn As CMdbConnection
    Public Sub New()
        Dim str As String = ConfigurationSettings.AppSettings("Importdb")
        Dim constr As String = ConfigurationSettings.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str & ";Jet OLEDB:Database Password=" & clsGlobal.glbAcessPassWord & ""
        mMdbConn = New CMdbConnection(constr)
    End Sub

    Public Function BSEContractImport(ByVal sFile As String, Optional ByVal CheckDate As Boolean = False) As Boolean

        If CheckDate = True Then
            Dim ObjTrad As New trading
            Dim sLWT As String = ""
            Try
                If flgimportContract = True Then
                    Call CopyToData(sFile, "CONTRACT")
                    Call ImportContract_BSe()
                    Dim DR As DataRow = GdtSettings.Select("SettingName='CONTRACT_LWT'")(0)
                    ObjTrad.SettingName = "CONTRACT_LWT"
                    ObjTrad.SettingKey = sLWT
                    ObjTrad.Uid = CInt(DR("uid"))
                    ObjTrad.Update_setting()
                    ObjTrad.Update_NewToken()
                    Return True
                Else
                    sLWT = Format(File.GetLastWriteTime(sFile), "yyyyMMddHHmmss")
                    If Val(sLWT) > Val(CONTRACT_LWT) Then
                        Call CopyToData(sFile, "CONTRACT")
                        Call ImportContract_BSe()
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
                ImportContract_BSe()
                ObjTrad.Update_NewToken()
                Return True
            Catch ex As Exception
                MsgBox("Invalid Data..")
                Return False
            End Try
        End If
    End Function
    Public Sub ImportContract_BSe()
        Try
            Dim DbPath As String = Application.StartupPath & ConfigurationSettings.AppSettings("dbname") & ";PWD=" & clsGlobal.glbAcessPassWord & ""

            Dim Delete_Contract_Bse As String = "Delete * From  [" & DbPath & "].Contract WHERE Exchange='BSE';"
            Dim Insert_Contract_Bse As String = "INSERT INTO [" & DbPath & "].Contract ( Token, Asset_Tokan, InstrumentName, Symbol, series, expiry_date, strike_price, option_type, script, lotsize,OScript,expdate1) " &
                                         " SELECT Contract.Field1 , Contract.Field2 , Contract.Field3 , Contract.Field4, Contract.Field5, Contract.Field7, Val([Contract].[Field8])/100, Contract.Field9, IIf((Trim(Left([contract].[Field9],1))='C' Or Trim(Left([contract].[Field9],1))='P'),[Contract].[Field3] & '  ' & [Contract].[Field4] & '  ' & Format(DateAdd('s',Val([Contract].[Field7]),CDate('1/1/1980')),'ddmmmyyyy') & '  ' & Format(Val([Contract].[Field8])/100,'0.00') & '  ' & [Contract].[Field9],[Contract].[Field3] & '  ' & [Contract].[Field4] & '  ' & Format(DateAdd('s',Val([Contract].[Field7]),CDate('1/1/1980')),'ddmmmyyyy')) AS Script, Contract.Field32 , " &
                                         " IIf((Trim(Left([contract].[Field9],1))='C' Or Trim(Left([contract].[Field9],1))='P'),[Contract].[Field3] & '  ' & [Contract].[Field4] & '  ' & Format(DateAdd('s',Val([Contract].[Field7]),CDate('1/1/1980')),'ddmmmyyyy') & '  ' & Format(Val([Contract].[Field8]/100),'0.00') & '  ' &  iif((Trim(Left([contract].[Field9],1))='C'),'PE','CE') ,'') AS OScript,CDate(Format(DateAdd('s',[Contract].[Field7],'1/1/1980'),'mmm/dd/yyyy')) as expdate1" &
                                         " FROM Contract " &
                                         " WHERE (((Trim([Contract].[Field3]))<>''));"


            Dim Insert_Contractcsv_Bse As String = "INSERT INTO [" & DbPath & "].Contract ( Token, Asset_Tokan, InstrumentName, Symbol, series, expiry_date, strike_price, option_type, script, lotsize,OScript,expdate1,Exchange) " &
            "SELECT 
    Contractcsv.Field1,
    0,
    IIF(Contractcsv.Field3 LIKE 'SF', 'FUTSTK', IIF(Contractcsv.Field3 LIKE 'IO', 'OPTIDX', IIF(Contractcsv.Field3 LIKE 'IF', 'FUTIDX', IIF(Contractcsv.Field3 LIKE 'SO', 'OPTSTK', 'error')))), 
    Contractcsv.Field4,
    Contractcsv.Field18,
    CDate([Contractcsv].[Field5]),
    IIf(IsNumeric([Contractcsv].[Field6]), Val([Contractcsv].[Field6])/100, 0),
IIf([Contractcsv].[Field3] = 'SF', 'XX', IIf([Contractcsv].[Field3] = 'IF', 'XX', [Contractcsv].[Field7])),

IIf(
    (Trim(Left([Contractcsv].[Field7],1))='C' Or Trim(Left([Contractcsv].[Field7],1))='P'),
    IIF([Contractcsv].[Field3] LIKE 'SF', 'FUTSTK', IIF([Contractcsv].[Field3] LIKE 'IO', 'OPTIDX', IIF([Contractcsv].[Field3] LIKE 'IF', 'FUTIDX', IIF(Contractcsv.Field3 LIKE 'SO', 'OPTSTK','')))) & 
        '  ' & [Contractcsv].[Field4] & '  ' & Format(CDate([Contractcsv].[Field5]),'ddmmmyyyy') & '  ' & 
        Format(IIf(IsNumeric([Contractcsv].[Field6]), Val([Contractcsv].[Field6])/100, 0),'0.00') & '  ' & [Contractcsv].[Field7],
    IIF([Contractcsv].[Field3] LIKE 'SF', 'FUTSTK', IIF([Contractcsv].[Field3] LIKE 'IO', 'OPTIDX', IIF([Contractcsv].[Field3] LIKE 'IF', 'FUTIDX',IIF(Contractcsv.Field3 LIKE 'SO', 'OPTSTK', '')))) & 
        '  ' & [Contractcsv].[Field4] & '  ' & Format(CDate([Contractcsv].[Field5]),'ddmmmyyyy')
) AS Script,
    Contractcsv.Field9,
    IIf((Trim(Left([Contractcsv].[Field7],1))='C' Or Trim(Left([Contractcsv].[Field7],1))='P'),
        IIF([Contractcsv].[Field3] LIKE 'SF', 'FUTSTK', IIF([Contractcsv].[Field3] LIKE 'IO', 'OPTIDX', IIF([Contractcsv].[Field3] LIKE 'IF', 'FUTIDX', IIF(Contractcsv.Field3 LIKE 'SO', 'OPTSTK','')))) &  '  ' & [Contractcsv].[Field4] & '  ' & Format(CDate([Contractcsv].[Field5]),'ddmmmyyyy') & '  ' &
        Format(IIf(IsNumeric([Contractcsv].[Field6]), Val([Contractcsv].[Field6])/100, 0),'0.00') & '  ' &
        IIf((Trim(Left([Contractcsv].[Field7],1))='C'),'PE','CE'),
        ''
    ) AS OScript,
    CDate([Contractcsv].[Field5]) AS expdate1,
    'BSE'
FROM 
    Contractcsv
WHERE 
    Trim([Contractcsv].[Field3]) <> 'FinInstrmNm';"
            REM Delete Contract
            'DaFo.ParamClear()
            'DaFo.Cmd_Text = Delete_Contract_Bse
            'DaFo.ExecuteNonQuery(CommandType.Text)

            'DaFo.ParamClear()
            'If FLGCSVCONTRACT = True Then
            '    DaFo.Cmd_Text = Insert_Contractcsv_Bse

            'Else
            '    DaFo.Cmd_Text = Insert_Contract_Bse
            'End If

            'DaFo.ExecuteNonQuery(CommandType.Text)

            mMdbConn.ParamClear()
            mMdbConn.mCmd_Text = Delete_Contract_Bse
            mMdbConn.ExecuteNonQuery(CommandType.Text)

            mMdbConn.ParamClear()
            mMdbConn.mCmd_Text = Insert_Contractcsv_Bse
            'If FLGCSVCONTRACT = True Then
            '    mMdbConn.Cmd_Text = Insert_Contractcsv_Bse

            'Else
            '    mMdbConn.Cmd_Text = Insert_Contract_Bse
            'End If

            mMdbConn.ExecuteNonQuery(CommandType.Text)


        Catch ex As Exception
            'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Shared Sub CopyToDatafO(ByVal sFile As String, ByVal fType As String)
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
                Case "BSESECURITY"
                    If FLGCSVCONTRACT = True Then
                        VarFileName = "Securitycsv.csv"
                    Else
                        VarFileName = "Securitycsv.txt"
                    End If

                Case "BHAVCOPY"
                    VarFileName = "Bhavcopy.txt"
                Case "BHAVCOPYNEW"
                    VarFileName = "Bhavcopyfo.txt"
                Case "BHAVCOPYBSE"
                    VarFileName = "BhavcopyBse.csv"
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
                File.Copy(sFile, "C:\Data\" & VarFileName, True)
            End If
            Write_TimeLog1("CopyToData:" + VarFileName)

        Catch ex As Exception
            MsgBox("Error in Copy File to Data Folder.", MsgBoxStyle.Information)
        End Try


        Write_TradeLog_viral("CopyToData", tik, System.Environment.TickCount)
    End Sub
End Class