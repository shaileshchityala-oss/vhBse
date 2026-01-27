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
Imports System.Data.OleDb

Public Class CBseContractEq
    Inherits CBseContractsBase

    Public mMdbConn As CMdbConnection
    Public Sub New()
        Dim str As String = ConfigurationSettings.AppSettings("Importdb")
        Dim constr As String = ConfigurationSettings.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str & ";Jet OLEDB:Database Password=" & clsGlobal.glbAcessPassWord & ""
        mMdbConn = New CMdbConnection(constr)
    End Sub

    'Public Function BSESecurityImport(ByVal sFile As String, Optional ByVal CheckDate As Boolean = False) As Boolean
    '    Try
    '        Call CopyToData(sFile, "BSESECURITY")
    '        Call ImportBSESecurity()
    '        Return True
    '    Catch ex As Exception
    '        MsgBox("Invalid Data..")
    '        Return False
    '    End Try
    'End Function
    Public Function BSESecurityImport(ByVal sFile As String, Optional ByVal CheckDate As Boolean = False) As Boolean
        If CheckDate = True Then
            Dim ObjTrad As New trading
            Dim sLWT As String = ""
            Try
                If flgimportContract = True Then

                    Call CopyToData(sFile, "BSESECURITY")
                    Call ImportBSESecurity()
                    Dim DR As DataRow = GdtSettings.Select("SettingName='SECURITY_LWT'")(0)
                    ObjTrad.SettingName = "SECURITY_LWT"
                    ObjTrad.SettingKey = sLWT
                    ObjTrad.Uid = CInt(DR("uid"))
                    ObjTrad.Update_setting()
                    Return True

                Else
                    sLWT = Format(File.GetLastWriteTime(sFile), "yyyyMMddHHmmss")
                    If Val(sLWT) > Val(SECURITY_LWT) Then
                        Call CopyToData(sFile, "BSESECURITY")
                        Call ImportBSESecurity()
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
                Call CopyToData(sFile, "BSESECURITY")
                Call ImportBSESecurity()
                Return True
            Catch ex As Exception
                MsgBox("Invalid Data..")
                Return False
            End Try
        End If
    End Function
    Public Sub ImportBSESecurity()
        Try
            Dim DbPath As String = Application.StartupPath & ConfigurationSettings.AppSettings("dbname") & ";PWD=" & clsGlobal.glbAcessPassWord & ""

            Dim Delete_Contract_Bse As String = "Delete * From  [" & DbPath & "].Security WHERE Exchange='Bse';"
            Dim Insert_Security As String = "INSERT INTO [" & DbPath & "].Security ( token, symbol, series, isin, script ) " &
                                           " SELECT Security.Field1, Security.Field2, Security.Field3, Security.Field46, [Field2] & '  ' & [Field3] AS Script " &
                                           " FROM Security " &
                                           " WHERE (((val([Field1]))<>0));"

            Dim Delete_Security As String = "Delete * From  [" & DbPath & "].Security WHERE Exchange='Bse';"

            Dim Insert_BSESecuritycsv As String = "INSERT INTO [" & DbPath & "].Security ( token, symbol, series, isin, script,Exchange ) " &
                                           " SELECT 
                                              Securitycsv.Field1,
                                              Securitycsv.Field2,
                                              Securitycsv.Field8,
                                              Securitycsv.Field5,
                                              [Field2] & '  ' & [Field8] AS Script,
                                              'BSE'  
                                            FROM Securitycsv  
                                            WHERE ((([Field3])<>'SctySrs'));"

            REM Delete Contract
            'DAEQ.ParamClear()
            'DAEQ.Cmd_Text = Delete_Security
            'DAEQ.cmd_type = CommandType.Text
            'DAEQ.ExecuteNonQuery(CommandType.Text)

            'REM import Contract
            'DAEQ.ParamClear()
            'If FLGCSVCONTRACT = True Then
            '    DAEQ.Cmd_Text = Insert_BSESecuritycsv
            'Else
            '    DAEQ.Cmd_Text = Insert_Security
            'End If

            'DAEQ.cmd_type = CommandType.Text
            'DAEQ.ExecuteNonQuery(CommandType.Text)

            mMdbConn.ParamClear()
            mMdbConn.mCmd_Text = Delete_Security
            mMdbConn.mCmd_type = CommandType.Text
            mMdbConn.ExecuteNonQuery(CommandType.Text)

            REM import Contract
            mMdbConn.ParamClear()
            If FLGCSVCONTRACT = True Then
                mMdbConn.mCmd_Text = Insert_BSESecuritycsv
            Else
                mMdbConn.mCmd_Text = Insert_Security
            End If

            mMdbConn.mCmd_type = CommandType.Text
            mMdbConn.ExecuteNonQuery(CommandType.Text)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

End Class
