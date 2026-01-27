Imports Microsoft.Win32
Imports System.Management
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports VolHedge.DAL
Imports System.Data
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.IO
Imports System.Globalization
Imports System.Runtime.InteropServices
Public Class CUserLicenseKey

    Dim mHardwarePrint As String
    Dim mLicenseData As String
    Dim mIsValidLicense As Boolean
    Dim mDealers As Integer
    Dim mIsAmc As Boolean
    Dim mStrExpDate As String
    Dim mDateExpiry As DateTime
    Dim mPerf As CPerfCheck
    Dim mAppVersion As String
    Dim MyAccessSQL As CSqlConnection
    Public Sub New(pVer As String)
        mPerf = New CPerfCheck()
        mPerf.SetFileName("lxlic")
        mAppVersion = pVer
        MyAccessSQL = New CSqlConnection(CSqlConnection.mStr)
    End Sub

    Private Function Decrypt(Text As String, keyBytes As Byte(), VectorBytes As Byte()) As String
        Try
            Dim TextBytes() As Byte = Convert.FromBase64String(Text)
            Dim rijKey As New RijndaelManaged()
            rijKey.Mode = CipherMode.CBC
            Dim decryptor = rijKey.CreateDecryptor(keyBytes, VectorBytes)
            Dim memoryStream As New MemoryStream(TextBytes)
            Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)

            Dim pTextBytes(TextBytes.Length - 1) As Byte
            Dim decryptedByteCount As Integer = cryptoStream.Read(pTextBytes, 0, pTextBytes.Length)
            memoryStream.Close()
            cryptoStream.Close()
            Dim plainText As String = Encoding.UTF8.GetString(pTextBytes, 0, decryptedByteCount)
            Return plainText
        Catch ex As Exception
            'ClsLog.Reference.WriteLogFile("RegisterLogKeyDecrypt", Me.ToString() & "Decrypt:" & "" & ":Error:" & ex.ToString() & "")
            'MessageBox.Show("Falsches Passwort " + a.Message.ToString());
            Return String.Empty
        End Try
    End Function

    Private Function Check_LicenseKey(VarUserCode As String, pLicFolder As String, pLicFile As String) As Boolean
        Dim FR As StreamReader = Nothing

        Try
            Dim salt() As Byte = {0, 0, 0, 0, 0, 0, 0, 0}
            Dim V() As Byte = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            Dim cdk As New PasswordDeriveBytes("asasjhdTGVsVolHedgeAMC", salt)
            Dim kex() As Byte = cdk.CryptDeriveKey("RC2", "SHA1", 128, salt)

            Dim Key As String = VarUserCode ' VarUserCode(0) & VarUserCode.Substring(2, 1) & VarUserCode.Substring(4, 1) & VarUserCode.Substring(6, 1) & VarUserCode.Substring(7)
            ' Key = "FBBFFFE0B0A2B88FE63FDefaultstr"


            'Dim filePath As String = Path.Combine(Application.StartupPath, "licence.lic")
            Dim filePath As String = Path.Combine(pLicFolder, pLicFile)

            If File.Exists(filePath) Then
                Using FR1 As New StreamReader(filePath)
                    Dim lines As New List(Of String)()

                    ' Read all lines from the file
                    While Not FR1.EndOfStream
                        Dim line As String = FR1.ReadLine()
                        lines.Add(line)
                    End While

                    ' Process each line
                    For Each answer As String In lines
                        Dim Lline As String = Decrypt(answer, kex, V)
                        Dim lic As String = Lline(0) & Lline.Substring(2, 1) & Lline.Substring(4, 1) & Lline.Substring(6, 1) & Lline.Substring(7)
                        '"FBBFFFE0B0A2B88FE63FDefaultstr"
                        If lic IsNot Nothing Then
                            Dim parts() As String = Lline.Split("|"c)

                            If parts.Length >= 5 Then
                                Dim keyPart As String = parts(0)
                                Dim expiryDatePart As String = parts(1)
                                Dim originalDate As DateTime
                                Dim currentDateAtMidnight As DateTime = Date.Now.Date

                                If DateTime.TryParseExact(expiryDatePart, "ddMMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, originalDate) Then
                                    If originalDate < currentDateAtMidnight Then
                                        MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
                                        Call clsGlobal.Sub_Get_Version_TextFile()
                                        Application.Exit()
                                        End
                                    Else
                                        G_VarExpiryDate = originalDate
                                    End If
                                Else
                                    ' Handle the case where the original date string is not in the expected format
                                End If

                                Dim ClientName As String = parts(3)
                                Dim version As String = parts(4)
                                Dim NoOfDealer As String = parts(2)
                                Dim VarIsAMC As Boolean = Convert.ToBoolean(parts(5))
                                Dim G_VarNoOfDealer As Integer = Integer.Parse(NoOfDealer)
                                Dim VarAppVersion As String = version
                                Dim last5CharsKeyPart As String = keyPart.Substring(keyPart.Length - 18)
                                Dim last5CharsKey As String = Key.Substring(Key.Length - 18)
                                'B88FE63FDefaultstr ''I1ZHT4821408S26JSN
                                mIsAmc = VarIsAMC
                                mDealers = G_VarNoOfDealer
                                mStrExpDate = expiryDatePart
                                If String.Equals(last5CharsKeyPart, last5CharsKey, StringComparison.Ordinal) Then
                                    clsGlobal.Expire_Date = Date.Parse(expiryDatePart)
                                    Dim G_LExpiryDate As DateTime = Date.Parse(expiryDatePart)
                                    mDateExpiry = G_LExpiryDate
                                    Return True
                                End If
                            End If
                        End If
                    Next
                End Using
                Return False
            Else
                Return False
            End If
        Catch ex As Exception
            'ClsLog.Reference.WriteLogFile("RegisterLogKey", Me.ToString() & "Veryfy_Key:" & "" & ":Error:" & ex.ToString() & "")

            Return False
        End Try
    End Function

    Public Function GetHddID0() As String
        Dim res As String
        Try
            Dim devID As String
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive")
            For Each obj As ManagementObject In searcher.Get()
                '"DRIVE0";
                devID = If(obj("DeviceID")?.ToString().Trim(), "")
                If devID.Contains("DRIVE0") Then
                    res = If(obj("SerialNumber")?.ToString().Trim(), "")
                    res = Fung_TrimLicString(res)
                    Return res
                End If

            Next
        Catch ex As Exception
            Return "00000000"
        End Try

        Return "000000"
    End Function
    Public Function GetProcID() As String
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor")

            For Each obj As ManagementObject In searcher.Get()
                Dim Res As String = If(obj("ProcessorId")?.ToString(), "")
                Res = TrimLicString(Res)
                If (Res.Length > 10) Then
                    Res = Res.Substring(0, 10)
                End If
                Return Res
            Next
        Catch ex As Exception
            Return ""
        End Try

        Return ""
    End Function

    Public Function GetBoardID() As String
        Dim res As String
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard")
            For Each obj As ManagementObject In searcher.Get()
                res = If(obj("SerialNumber")?.ToString().Trim(), "")
                res = TrimLicString(res)
                If (res.Length > 10) Then
                    res = res.Substring(0, 10)
                End If
                Return res
            Next
        Catch ex As Exception
            Return ""
        End Try

        Return ""
    End Function
    Public Function GetHddID() As String
        Try
            Dim res As String = TrimLicString(GetHddID0)
            If (res.Length > 10) Then
                res = res.Substring(0, 10)
            End If
            Return res

        Catch ex As Exception
            Return "00000000"
        End Try

        Return "000000"
    End Function

    Public Function GetHddID2() As String
        Try
            Dim res As String = TrimLicString(GetHddID0)
            res = res.Replace("0", "")
            If (res.Length > 10) Then
                res = res.Substring(0, 10)
            End If
            Return res

        Catch ex As Exception
            Return "00000000"
        End Try

        Return "000000"


    End Function

    Public Function TrimLicString(ByVal pStr As String) As String
        Dim res As String = ""
        res = pStr.Replace("-", "")
        res = res.Replace(".", "")
        res = res.Replace(":", "")
        res = res.Replace(" ", "")
        res = res.Replace("_", "")
        Return res
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function GetVolumeInformation(ByVal lpRootPathName As String,
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

    Public Function GenerateLicenseKeyV2() As String
        mHardwarePrint = GetVolStr() + GetProcID() + GetBoardID()
        Return mHardwarePrint
    End Function

    Public Function IsLicenseOkV2() As Boolean
        GenerateLicenseKeyV2()
        'Dim filePath As String = Path.Combine(Application.StartupPath, "licence.lic")
        mIsValidLicense = Check_LicenseKey(mHardwarePrint, Application.StartupPath, "licence.lic")
        Return mIsValidLicense
    End Function

    Private Sub WriteKeyFileV2(VarUserCodeOld1 As String)

        Dim licBoardID As String = ""
        Dim licProcID As String = ""
        Dim licHddID As String = ""

        licBoardID = GetBoardID()
        licHddID = GetHddID()
        licProcID = GetProcID()

        Dim licDriveVolID As String = GetVolStr()

        Dim hardwareIDVer2 As String = licDriveVolID & licBoardID & licProcID
        Dim keyOutVer2 As String = SecureCrypto256.Shuffle(hardwareIDVer2)
        mPerf.WriteLogStr(licBoardID + " : " + licHddID + ":" + licProcID + ":" + licDriveVolID)
        Dim host1 As String = System.Net.Dns.GetHostName
        Dim iPs1 As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(host1)
        Dim ip1 As String = iPs1.AddressList(0).ToString()

        Dim FSKeyFile As System.IO.StreamWriter = New IO.StreamWriter(Application.StartupPath & "\KeyV2.txt", False)
        FSKeyFile.WriteLine("Computer Name:" & My.Computer.Name)
        FSKeyFile.WriteLine("IP:" & ip1)
        FSKeyFile.WriteLine("Client Key:" & keyOutVer2)
        FSKeyFile.WriteLine("Version:" & mAppVersion)
        FSKeyFile.Close()
    End Sub

End Class