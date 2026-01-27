Imports System.Data
Imports System.IO
Imports Sylvan.Data.Csv
Imports System.Globalization
Imports System.Threading.Tasks
Imports VolHedge.DAL
Imports System.Collections
Imports System.Collections.Concurrent
Imports System.Configuration
Imports Microsoft.Win32
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.Net.Sockets.Socket
Imports System.Net.NetworkInformation
Imports System.Management
Imports System.Net.Sockets.NetworkStream

Public Class CBseContractsBase

    Public Sub CopyToData(ByVal sFile As String, ByVal fType As String)
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

                Case "CONTRACTBSE"
                    If FLGCSVCONTRACT = True Then
                        VarFileName = "contractcsv.csv"
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

    Public Sub Update_NewTokenBse()

        Dim StrSql As String

        'Private Updat_tok_Trading As String = "UPDATE trading INNER JOIN contract ON trading.script = contract.script SET trading.token1 =contract.token;"
        'Private Update_tok_Analysis As String = "UPDATE analysis INNER JOIN contract ON analysis.script = contract.script SET analysis.token1 =contract.token"

        StrSql = "UPDATE trading INNER JOIN contract ON trading.script = contract.script SET trading.token1 =contract.token;"
        data_access.ParamClear()
        data_access.ExecuteNonQuery(StrSql, CommandType.Text)
        data_access.cmd_type = CommandType.StoredProcedure



        StrSql = "UPDATE analysis INNER JOIN contract ON analysis.script = contract.script SET analysis.token1 =contract.token"
        data_access.ParamClear()
        data_access.ExecuteNonQuery(StrSql, CommandType.Text)
        data_access.cmd_type = CommandType.StoredProcedure

    End Sub

    Public Function DownloadFile(ByVal FtpUrl As String, ByVal FileNameToDownload As String, ByVal userName As String, ByVal password As String, ByVal tempDirPath As String) As String
        Dim PureFileName As String = New FileInfo(FileNameToDownload).Name
        Try
            Dim url As String = ""

            Dim filename As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName

            '   url = Convert.ToString("https://support.finideas.com/contract/") & FileNameToDownload

            If FLGCSVCONTRACT = True Then
                url = Convert.ToString("https://support.finideas.com/contractcsv/") & FileNameToDownload
            Else
                url = Convert.ToString("https://support.finideas.com/contract/") & FileNameToDownload
            End If

            Dim client As New WebClient()
            Dim uri As New Uri(url)
            client.Headers.Add("Accept: text/html, application/xhtml+xml, */*")
            client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)")
            client.DownloadFileAsync(uri, filename)

            While client.IsBusy
                System.Threading.Thread.Sleep(1000)

            End While
            'Console.WriteLine(ex.Message);

            Return "success"
        Catch ex As UriFormatException
            Return "error"
        End Try

    End Function

End Class
