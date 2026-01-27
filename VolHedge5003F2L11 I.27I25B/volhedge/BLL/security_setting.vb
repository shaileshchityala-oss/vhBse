Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL
Imports System.Security.Cryptography
Imports System.Xml
Imports System.IO
Imports Microsoft.Win32
Public Class security_setting
#Region "variable"
    Dim _lower As String
    Dim _upper As String
    Dim _mid As String
    Dim _tday As String
    Dim _tdate As Date

#End Region
#Region "Property"
    Public Property lower() As String
        Get
            Return _lower
        End Get
        Set(ByVal value As String)
            _lower = value
        End Set
    End Property
    Public Property Upper() As String
        Get
            Return _upper
        End Get
        Set(ByVal value As String)
            _upper = value
        End Set
    End Property
    Public Property Mid() As String
        Get
            Return _mid
        End Get
        Set(ByVal value As String)
            _mid = value
        End Set
    End Property
    Public Property Tday() As String
        Get
            Return _tday
        End Get
        Set(ByVal value As String)
            _tday = value
        End Set
    End Property
    Public Property Tdate() As Date
        Get
            Return _tdate
        End Get
        Set(ByVal value As Date)
            _tdate = value
        End Set
    End Property
#End Region
#Region "SP"
    Private Const SP_select_important As String = "select_important"
    Private Const SP_insert_important As String = "insert_important"
    Private Const SP_change_important As String = "change_important"
    Private Const SP_select_td As String = "select_td"
#End Region
    Public Function Decry(ByVal TextToBeDecrypted As String) As String

        Dim RijndaelCipher As RijndaelManaged = New RijndaelManaged()

        Dim Password As String = "CSC"
        Dim DecryptedData As String

        Try

            Dim EncryptedData() As Byte = Convert.FromBase64String(TextToBeDecrypted)

            Dim Salt() As Byte = Encoding.ASCII.GetBytes(Password.Length.ToString())
            'Making of the key for decryption
            Dim SecretKey As PasswordDeriveBytes = New PasswordDeriveBytes(Password, Salt)
            'Creates a symmetric Rijndael decryptor object.
            Dim Decryptor As ICryptoTransform = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16))

            Dim memoryStream As MemoryStream = New MemoryStream(EncryptedData)
            'Defines the cryptographics stream for decryption.THe stream contains decrpted data
            Dim CryptoStream As CryptoStream = New CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read)

            Dim PlainText(EncryptedData.Length) As Byte
            Dim DecryptedCount As Integer = CryptoStream.Read(PlainText, 0, PlainText.Length)
            memoryStream.Close()
            CryptoStream.Close()

            'Converting to string
            DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount)

        Catch ex As Exception
            DecryptedData = TextToBeDecrypted
        End Try
        Return DecryptedData
    End Function
    Public Function Encry(ByVal TextToBeEncrypted As String) As String

        Dim RijndaelCipher As RijndaelManaged = New RijndaelManaged()

        Dim Password As String = "CSC"
        Dim PlainText() As Byte = System.Text.Encoding.Unicode.GetBytes(TextToBeEncrypted)
        Dim Salt() As Byte = Encoding.ASCII.GetBytes(Password.Length.ToString())
        Dim SecretKey As PasswordDeriveBytes = New PasswordDeriveBytes(Password, Salt)
        'Creates a symmetric encryptor object. 
        Dim Encryptor As ICryptoTransform = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16))
        Dim memoryStream As MemoryStream = New MemoryStream()
        'Defines a stream that links data streams to cryptographic transformations
        Dim cryptoStream As CryptoStream = New CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write)
        cryptoStream.Write(PlainText, 0, PlainText.Length)
        'Writes the final state and clears the buffer
        cryptoStream.FlushFinalBlock()
        Dim CipherBytes() As Byte = memoryStream.ToArray()
        memoryStream.Close()
        cryptoStream.Close()
        Dim EncryptedData As String = Convert.ToBase64String(CipherBytes)

        Return EncryptedData
    End Function

    Public Function checking_client() As Boolean
        Try
            Dim check As Boolean
            check = False

            Dim regKey As RegistryKey
            Dim regKeyCompany As RegistryKey
            Dim regKeyProduct As RegistryKey

            'Dim strstartdate As String
            'Dim strenddate As String
            Dim count As String
            'Dim startdate As DateTime
            'Dim enddate As DateTime
            Dim dllpath As String
            Dim txtpath As String
            If System.Environment.OSVersion.Version.Minor.ToString = 1 Then
                dllpath = "C:\WINDOWS\system32\msys.dll"
                txtpath = "C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\dotnet.txt"
            Else
                dllpath = "C:\WINNT\system32\msys.dll"
                txtpath = "C:\WINNT\Microsoft.NET\Framework\v2.0.50727\dotnet.txt"
            End If
            regKey = Registry.CurrentUser.OpenSubKey("Software", True)
            If Not regKey Is Nothing Then
                regKeyCompany = regKey.OpenSubKey("server", True)
                If Not regKeyCompany Is Nothing Then

                    regKeyProduct = regKeyCompany.OpenSubKey("Taskman", True)
                    If Not regKeyProduct Is Nothing Then
                        '  MsgBox("regKeyProduct")
                        If regKeyProduct.ValueCount > 0 Then
                            'strstartdate = regKeyProduct.GetValue("_system").ToString()
                            'strenddate = regKeyProduct.GetValue("system").ToString()
                            count = regKeyProduct.GetValue("_cot").ToString()

                            'If strstartdate = "" And strenddate = "" And count = "" Then
                            '    If File.Exists(txtpath) Then
                            '        Dim iline1 As New StreamReader(txtpath)
                            '        Dim li As String()
                            '        li = Split(iline1.ReadLine, ",")
                            '        iline1.Close()
                            '        strstartdate = li(0)
                            '        strenddate = li(1)
                            '        count = li(2)
                            '        regKeyProduct.SetValue("_system", strstartdate)
                            '        regKeyProduct.SetValue("system", strenddate)
                            '        regKeyProduct.SetValue("_cot", count)
                            '        check = True
                            '    End If
                            'Else

                            'If CDate(Decry(strstartdate)).Date <> Today.Date And CDate(Decry(strenddate)).Date < Today.Date And CInt(Decry(count)) < 30 Then
                            '    count = Encry(CInt(Decry(count)) + 1)
                            '    strstartdate = Encry(DateAdd(DateInterval.Day, 1, CDate(Decry(strstartdate))))
                            '    regKeyProduct.SetValue("_cot", count)
                            '    regKeyProduct.SetValue("_system", strstartdate)
                            '    If File.Exists(txtpath) Then
                            '        File.Delete(txtpath)
                            '    End If
                            '    Dim oWrite As System.IO.StreamWriter = File.CreateText(txtpath)
                            '    oWrite.WriteLine(strstartdate & "," & strenddate & "," & count)
                            '    oWrite.Close()
                            '    check = True
                            'ElseIf CInt(Decry(count)) < 30 And (CDate(Decry(strenddate)).Date = Today.Date Or CDate(Decry(strenddate)).Date > Today.Date) Then
                            '    check = True
                            'End If

                            If Now.Date >= CDate("07/10/2010") Then

                                count = Encry(CInt(Decry(count)) + 1)
                                regKeyProduct.SetValue("_cot", count)
                                check = True

                                If CInt(Decry(count)) > 40 Then
                                    check = False
                                End If
                            Else
                                If CInt(Decry(count)) > 40 Then
                                    check = False
                                Else
                                    check = True
                                End If

                            End If


                            'If (CDate("03/15/2010") > Now.Date) Or (CDate("03/15/2010") <= Now.Date And CInt(Decry(count)) < 40) Then
                            '    count = Encry(CInt(Decry(count)) + 1)
                            '    regKeyProduct.SetValue("_cot", count)

                            '    check = True
                            'Else
                            '    check = False
                            'End If

                            'If CDate(Decry(strstartdate)).Date <> Today.Date And CDate(Decry(strenddate)).Date < Today.Date And CInt(Decry(count)) < 30 Then
                            '    count = Encry(CInt(Decry(count)) + 1)
                            '    strstartdate = Encry(DateAdd(DateInterval.Day, 1, CDate(Decry(strstartdate))))
                            '    regKeyProduct.SetValue("_cot", count)
                            '    regKeyProduct.SetValue("_system", strstartdate)
                            '    If File.Exists(txtpath) Then
                            '        File.Delete(txtpath)
                            '    End If
                            '    Dim oWrite As System.IO.StreamWriter = File.CreateText(txtpath)
                            '    oWrite.WriteLine(strstartdate & "," & strenddate & "," & count)
                            '    oWrite.Close()
                            '    check = True
                            'ElseIf CInt(Decry(count)) < 30 And (CDate(Decry(strenddate)).Date = Today.Date Or CDate(Decry(strenddate)).Date > Today.Date) Then
                            '    check = True
                            'End If

                        End If

                    End If
                    'End If
                    ' MsgBox("regKeyProduct")
                End If
                'MsgBox("regkey")
            End If

            Return check
        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
    End Function
    Public Sub clientsetting()

        Dim regKey As RegistryKey
        Dim regKeyCompany As RegistryKey
        Dim regKeyProduct As RegistryKey
        Dim strstartdate As String
        Dim strenddate As String
        Dim count As String
        'Dim _volhedge As RegistryKey
        Dim dllpath As String
        Dim txtpath As String
        Dim dlludipth As String
        If System.Environment.OSVersion.Version.Minor.ToString = 1 Then
            dllpath = "C:\WINDOWS\system32\msys.dll"
            txtpath = "C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\dotnet.txt"
            dlludipth = "C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\msyviptm.dll"
        Else
            dllpath = "C:\WINNT\system32\msys.dll"
            txtpath = "C:\WINNT\Microsoft.NET\Framework\v2.0.50727\dotnet.txt"
            dlludipth = "C:\WINNT\Microsoft.NET\Framework\v2.0.50727\msyviptm.dll"
        End If
        Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MMM/dd/yyyy")
        regKey = Registry.CurrentUser.OpenSubKey("Software", True)
        If Not File.Exists(dllpath) Or Not File.Exists(dlludipth) Then
            Dim objSec As New security_setting
            If Not regKey Is Nothing Then
                'regKey.CreateSubKey("Volhedge")
                '_volhedge = regKey.OpenSubKey("Volhedge")
                '_volhedge.SetValue("Version", "1.0.0.1")

                regKey.CreateSubKey("server")
                regKeyCompany = regKey.OpenSubKey("server", True)
                If Not regKeyCompany Is Nothing Then
                    regKeyCompany.CreateSubKey("Taskman")
                    regKeyProduct = regKeyCompany.OpenSubKey("Taskman", True)
                    If Not regKeyProduct Is Nothing Then
                        If regKeyProduct.ValueCount > 0 Then
                            If Not File.Exists(dlludipth) Then
                                strstartdate = regKeyProduct.GetValue("_system", Nothing)
                                strenddate = regKeyProduct.GetValue("system", Nothing)
                                count = regKeyProduct.GetValue("_cot", Nothing)
                                If Not strstartdate Is Nothing Then
                                    regKeyProduct.DeleteValue("_system")
                                End If
                                If Not strenddate Is Nothing Then
                                    regKeyProduct.DeleteValue("system")
                                End If
                                If Not count Is Nothing Then
                                    regKeyProduct.DeleteValue("_cot")
                                End If
                            End If
                            strstartdate = regKeyProduct.GetValue("_system", Nothing)
                            strenddate = regKeyProduct.GetValue("system", Nothing) 'regKeyProduct.GetValue("system").ToString()
                            count = regKeyProduct.GetValue("_cot", Nothing)
                            If (strstartdate Is Nothing And strenddate Is Nothing) Then
                                regKeyProduct.SetValue("_system", "")
                                regKeyProduct.SetValue("system", "")
                                regKeyProduct.SetValue("_cot", "0")
                            ElseIf (strstartdate = "" And strenddate = "") Then
                                regKeyProduct.SetValue("_system", "")
                                regKeyProduct.SetValue("system", "")
                                regKeyProduct.SetValue("_cot", "0")
                            End If
                        Else
                            regKeyProduct.SetValue("_system", "")
                            regKeyProduct.SetValue("system", "")
                            regKeyProduct.SetValue("_cot", "0")
                        End If
                    End If
                End If
            End If
            File.Create(dllpath)
            File.Create(dlludipth)
            Dim oWrite As System.IO.StreamWriter = File.CreateText(txtpath)
            Dim ccdate As Date = ("07/10/2010")
            oWrite.WriteLine(Encry(ccdate) & "," & Encry(ccdate) & "," & Encry("0"))
            oWrite.Close()
        End If
    End Sub

    Public Function checking() As Boolean
        Try
            Dim check As Boolean
            check = False

            Dim regKey As RegistryKey
            Dim regKeyCompany As RegistryKey
            Dim regKeyProduct As RegistryKey

            Dim strstartdate As String
            Dim strenddate As String
            'Dim startdate As DateTime
            'Dim enddate As DateTime
            Dim dllpath As String
            Dim txtpath As String
            If System.Environment.OSVersion.Version.Minor.ToString = 1 Then
                dllpath = "C:\WINDOWS\system32\msys_sys.dll"
                txtpath = "C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\dotnet_sys.txt"
            Else
                dllpath = "C:\WINNT\system32\msys_sys.dll"
                txtpath = "C:\WINNT\Microsoft.NET\Framework\v2.0.50727\dotnet_sys.txt"
            End If
            regKey = Registry.CurrentUser.OpenSubKey("Software", True)
            If Not regKey Is Nothing Then
                regKeyCompany = regKey.OpenSubKey("server", True)
                If Not regKeyCompany Is Nothing Then

                    regKeyProduct = regKeyCompany.OpenSubKey("Taskman", True)
                    If Not regKeyProduct Is Nothing Then
                        '  MsgBox("regKeyProduct")
                        If regKeyProduct.ValueCount > 0 Then
                            strstartdate = regKeyProduct.GetValue("_system").ToString()
                            strenddate = regKeyProduct.GetValue("system").ToString()
                            If strstartdate = "" And strenddate = "" Then
                                If File.Exists(txtpath) Then
                                    Dim iline1 As New StreamReader(txtpath)
                                    Dim li As String()
                                    li = Split(iline1.ReadLine, ",")
                                    iline1.Close()
                                    'strstartdate = Encry(Now.ToString)
                                    'strenddate = Encry(DateAdd(DateInterval.Day, 7, Now).ToString)
                                    strstartdate = li(0)
                                    strenddate = li(1)
                                    regKeyProduct.SetValue("_system", strstartdate)
                                    regKeyProduct.SetValue("system", strenddate)
                                    check = True
                                End If
                            Else
                                If CDate(Decry(strstartdate)) < CDate(Decry(strenddate)) And CDate(Decry(strstartdate)).Date < Today.Date Then
                                    strstartdate = Encry(DateAdd(DateInterval.Day, 1, CDate(Decry(strstartdate))))
                                    regKeyProduct.SetValue("_system", strstartdate)
                                    If File.Exists(txtpath) Then
                                        File.Delete(txtpath)
                                    End If
                                    Dim oWrite As System.IO.StreamWriter = File.CreateText(txtpath)
                                    oWrite.WriteLine(strstartdate & "," & strenddate)
                                    oWrite.Close()
                                    check = True
                                ElseIf CDate(Decry(strstartdate)) < CDate(Decry(strenddate)) And CDate(Decry(strstartdate)).Date = Today.Date Then
                                    check = True
                                End If

                            End If

                        End If
                    End If
                    ' MsgBox("regKeyProduct")
                End If
                'MsgBox("regkey")
            End If
            'Dim temptable As DataTable
            'temptable = New DataTable
            'data_access.ParamClear()
            'data_access.Cmd_Text = SP_select_td
            'Tdate = data_access.ExecuteScalar
            'data_access.ParamClear()
            'data_access.Cmd_Text = SP_select_important
            'temptable = data_access.FillList
            'If temptable.Rows.Count > 0 Then
            '    For Each drow As DataRow In temptable.Rows
            '        If (val(Decry(drow("mid"))) + 1) >= "15" Then
            '            check = False
            '        Else
            '            If CDate(Decry(drow("td"))).Date < Tdate.Date Then
            '                data_access.ParamClear()
            '                data_access.AddParam("@mid", OleDbType.VarChar, 100, Encry(CStr(val(Decry(drow("mid"))) + 1)))
            '                data_access.AddParam("@td", OleDbType.VarChar, 100, Encry(Tdate.Date))
            '                data_access.Cmd_Text = SP_change_important
            '                data_access.ExecuteNonQuery()
            '            End If
            '            check = True
            '        End If
            '    Next
            'Else
            '    ''data_access.ParamClear()
            '    ''data_access.AddParam("@lower", OleDbType.VarChar, 100, Encry(Tdate.Date))
            '    ''data_access.AddParam("@mid", OleDbType.VarChar, 100, Encry("0"))
            '    ''data_access.AddParam("@upper", OleDbType.VarChar, 100, Encry(DateAdd(DateInterval.Day, 15, Tdate.Date)))
            '    ''data_access.AddParam("@td", OleDbType.VarChar, 100, Encry(Tdate.Date))
            '    ''data_access.Cmd_Text = SP_insert_important
            '    ''data_access.ExecuteNonQuery()
            '    ''check = True
            'End If
            Return check
        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
    End Function
    Public Function checking_14_days() As Boolean
        Try
            Dim check As Boolean
            check = False

            Dim regKey As RegistryKey
            Dim regKeyCompany As RegistryKey
            Dim regKeyProduct As RegistryKey

            Dim strstartdate As String
            Dim strenddate As String
            'Dim startdate As DateTime
            'Dim enddate As DateTime
            Dim dllpath As String
            Dim txtpath As String
            If System.Environment.OSVersion.Version.Minor.ToString = 1 Then
                dllpath = "C:\WINDOWS\system32\msys.dll"
                txtpath = "C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\dotnet.txt"
            Else
                dllpath = "C:\WINNT\system32\msys.dll"
                txtpath = "C:\WINNT\Microsoft.NET\Framework\v2.0.50727\dotnet.txt"
            End If
            regKey = Registry.CurrentUser.OpenSubKey("Software", True)
            If Not regKey Is Nothing Then
                regKeyCompany = regKey.OpenSubKey("server", True)
                If Not regKeyCompany Is Nothing Then

                    regKeyProduct = regKeyCompany.OpenSubKey("Taskman", True)
                    If Not regKeyProduct Is Nothing Then
                        '  MsgBox("regKeyProduct")
                        If regKeyProduct.ValueCount > 0 Then
                            strstartdate = regKeyProduct.GetValue("_system").ToString()
                            strenddate = regKeyProduct.GetValue("system").ToString()
                            If strstartdate = "" And strenddate = "" Then
                                If File.Exists(txtpath) Then
                                    Dim iline1 As New StreamReader(txtpath)
                                    Dim li As String()
                                    li = Split(iline1.ReadLine, ",")
                                    iline1.Close()
                                    'strstartdate = Encry(Now.ToString)
                                    'strenddate = Encry(DateAdd(DateInterval.Day, 7, Now).ToString)
                                    strstartdate = li(0)
                                    strenddate = li(1)
                                    regKeyProduct.SetValue("_system", strstartdate)
                                    regKeyProduct.SetValue("system", strenddate)
                                    check = True
                                End If
                            Else
                                If CDate(Decry(strstartdate)) < CDate(Decry(strenddate)).AddDays(7) And CDate(Decry(strstartdate)).Date < Today.Date Then
                                    strstartdate = Encry(DateAdd(DateInterval.Day, 1, CDate(Decry(strstartdate))))
                                    regKeyProduct.SetValue("_system", strstartdate)
                                    If File.Exists(txtpath) Then
                                        File.Delete(txtpath)
                                    End If
                                    Dim oWrite As System.IO.StreamWriter = File.CreateText(txtpath)
                                    oWrite.WriteLine(strstartdate & "," & strenddate)
                                    oWrite.Close()
                                    check = True
                                ElseIf CDate(Decry(strstartdate)) < CDate(Decry(strenddate)).AddDays(7) And CDate(Decry(strstartdate)).Date = Today.Date Then
                                    check = True
                                End If

                            End If

                        End If
                    End If
                    ' MsgBox("regKeyProduct")
                End If
                'MsgBox("regkey")
            End If
            'Dim temptable As DataTable
            'temptable = New DataTable
            'data_access.ParamClear()
            'data_access.Cmd_Text = SP_select_td
            'Tdate = data_access.ExecuteScalar
            'data_access.ParamClear()
            'data_access.Cmd_Text = SP_select_important
            'temptable = data_access.FillList
            'If temptable.Rows.Count > 0 Then
            '    For Each drow As DataRow In temptable.Rows
            '        If (val(Decry(drow("mid"))) + 1) >= "15" Then
            '            check = False
            '        Else
            '            If CDate(Decry(drow("td"))).Date < Tdate.Date Then
            '                data_access.ParamClear()
            '                data_access.AddParam("@mid", OleDbType.VarChar, 100, Encry(CStr(val(Decry(drow("mid"))) + 1)))
            '                data_access.AddParam("@td", OleDbType.VarChar, 100, Encry(Tdate.Date))
            '                data_access.Cmd_Text = SP_change_important
            '                data_access.ExecuteNonQuery()
            '            End If
            '            check = True
            '        End If
            '    Next
            'Else
            '    ''data_access.ParamClear()
            '    ''data_access.AddParam("@lower", OleDbType.VarChar, 100, Encry(Tdate.Date))
            '    ''data_access.AddParam("@mid", OleDbType.VarChar, 100, Encry("0"))
            '    ''data_access.AddParam("@upper", OleDbType.VarChar, 100, Encry(DateAdd(DateInterval.Day, 15, Tdate.Date)))
            '    ''data_access.AddParam("@td", OleDbType.VarChar, 100, Encry(Tdate.Date))
            '    ''data_access.Cmd_Text = SP_insert_important
            '    ''data_access.ExecuteNonQuery()
            '    ''check = True
            'End If
            Return check
        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
    End Function
    Public Function checking_25_days() As Boolean
        Try
            Dim check As Boolean
            check = False

            Dim regKey As RegistryKey
            Dim regKeyCompany As RegistryKey
            Dim regKeyProduct As RegistryKey

            Dim strstartdate As String
            Dim strenddate As String
            'Dim startdate As DateTime
            'Dim enddate As DateTime
            Dim dllpath As String
            Dim txtpath As String
            If System.Environment.OSVersion.Version.Minor.ToString = 1 Then
                dllpath = "C:\WINDOWS\system32\msys.dll"
                txtpath = "C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\dotnet.txt"
            Else
                dllpath = "C:\WINNT\system32\msys.dll"
                txtpath = "C:\WINNT\Microsoft.NET\Framework\v2.0.50727\dotnet.txt"
            End If
            regKey = Registry.CurrentUser.OpenSubKey("Software", True)
            If Not regKey Is Nothing Then
                regKeyCompany = regKey.OpenSubKey("server", True)
                If Not regKeyCompany Is Nothing Then

                    regKeyProduct = regKeyCompany.OpenSubKey("Taskman", True)
                    If Not regKeyProduct Is Nothing Then
                        '  MsgBox("regKeyProduct")
                        If regKeyProduct.ValueCount > 0 Then
                            strstartdate = regKeyProduct.GetValue("_system").ToString()
                            strenddate = regKeyProduct.GetValue("system").ToString()
                            If strstartdate = "" And strenddate = "" Then
                                If File.Exists(txtpath) Then
                                    Dim iline1 As New StreamReader(txtpath)
                                    Dim li As String()
                                    li = Split(iline1.ReadLine, ",")
                                    iline1.Close()
                                    'strstartdate = Encry(Now.ToString)
                                    'strenddate = Encry(DateAdd(DateInterval.Day, 7, Now).ToString)
                                    strstartdate = li(0)
                                    strenddate = li(1)
                                    regKeyProduct.SetValue("_system", strstartdate)
                                    regKeyProduct.SetValue("system", strenddate)
                                    check = True
                                End If
                            Else
                                If CDate(Decry(strstartdate)) < CDate(Decry(strenddate)).AddDays(18) And CDate(Decry(strstartdate)).Date < Today.Date Then
                                    strstartdate = Encry(DateAdd(DateInterval.Day, 1, CDate(Decry(strstartdate))))
                                    regKeyProduct.SetValue("_system", strstartdate)
                                    If File.Exists(txtpath) Then
                                        File.Delete(txtpath)
                                    End If
                                    Dim oWrite As System.IO.StreamWriter = File.CreateText(txtpath)
                                    oWrite.WriteLine(strstartdate & "," & strenddate)
                                    oWrite.Close()
                                    check = True
                                ElseIf CDate(Decry(strstartdate)) < CDate(Decry(strenddate)).AddDays(18) And CDate(Decry(strstartdate)).Date = Today.Date Then
                                    check = True
                                End If

                            End If

                        End If
                    End If
                    ' MsgBox("regKeyProduct")
                End If
                'MsgBox("regkey")
            End If
            'Dim temptable As DataTable
            'temptable = New DataTable
            'data_access.ParamClear()
            'data_access.Cmd_Text = SP_select_td
            'Tdate = data_access.ExecuteScalar
            'data_access.ParamClear()
            'data_access.Cmd_Text = SP_select_important
            'temptable = data_access.FillList
            'If temptable.Rows.Count > 0 Then
            '    For Each drow As DataRow In temptable.Rows
            '        If (val(Decry(drow("mid"))) + 1) >= "15" Then
            '            check = False
            '        Else
            '            If CDate(Decry(drow("td"))).Date < Tdate.Date Then
            '                data_access.ParamClear()
            '                data_access.AddParam("@mid", OleDbType.VarChar, 100, Encry(CStr(val(Decry(drow("mid"))) + 1)))
            '                data_access.AddParam("@td", OleDbType.VarChar, 100, Encry(Tdate.Date))
            '                data_access.Cmd_Text = SP_change_important
            '                data_access.ExecuteNonQuery()
            '            End If
            '            check = True
            '        End If
            '    Next
            'Else
            '    ''data_access.ParamClear()
            '    ''data_access.AddParam("@lower", OleDbType.VarChar, 100, Encry(Tdate.Date))
            '    ''data_access.AddParam("@mid", OleDbType.VarChar, 100, Encry("0"))
            '    ''data_access.AddParam("@upper", OleDbType.VarChar, 100, Encry(DateAdd(DateInterval.Day, 15, Tdate.Date)))
            '    ''data_access.AddParam("@td", OleDbType.VarChar, 100, Encry(Tdate.Date))
            '    ''data_access.Cmd_Text = SP_insert_important
            '    ''data_access.ExecuteNonQuery()
            '    ''check = True
            'End If
            Return check
        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
    End Function
    Public Sub setting()
        Dim regKey As RegistryKey
        Dim regKeyCompany As RegistryKey
        Dim regKeyProduct As RegistryKey
        Dim strstartdate As String
        Dim strenddate As String
        'Dim _volhedge As RegistryKey
        Dim dllpath As String
        Dim txtpath As String
        If System.Environment.OSVersion.Version.Minor.ToString = 1 Then
            dllpath = "C:\WINDOWS\system32\msys_sys.dll"
            txtpath = "C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\dotnet_sys.txt"
        Else
            dllpath = "C:\WINNT\system32\msys.dll"
            txtpath = "C:\WINNT\Microsoft.NET\Framework\v2.0.50727\dotnet_sys.txt"
        End If
        Registry.SetValue("HKEY_CURRENT_USER\Control Panel\International", "sShortDate", "MMM/dd/yyyy")
        regKey = Registry.CurrentUser.OpenSubKey("Software", True)
        If Not File.Exists(dllpath) Then
            Dim objSec As New security_setting
            If Not regKey Is Nothing Then
                'regKey.CreateSubKey("Volhedge")
                '_volhedge = regKey.OpenSubKey("Volhedge")
                '_volhedge.SetValue("Version", "1.0.0.1")

                regKey.CreateSubKey("server")
                regKeyCompany = regKey.OpenSubKey("server", True)
                If Not regKeyCompany Is Nothing Then
                    regKeyCompany.CreateSubKey("Taskman")
                    regKeyProduct = regKeyCompany.OpenSubKey("Taskman", True)
                    If Not regKeyProduct Is Nothing Then
                        If regKeyProduct.ValueCount > 0 Then
                            strstartdate = regKeyProduct.GetValue("_system").ToString()
                            strenddate = regKeyProduct.GetValue("system").ToString()
                            If (strstartdate Is Nothing And strenddate Is Nothing) Then
                                regKeyProduct.SetValue("_system", "")
                                regKeyProduct.SetValue("system", "")
                            ElseIf (strstartdate = "" And strenddate = "") Then
                                regKeyProduct.SetValue("_system", "")
                                regKeyProduct.SetValue("system", "")
                            End If
                        Else
                            regKeyProduct.SetValue("_system", "")
                            regKeyProduct.SetValue("system", "")
                        End If
                    End If
                End If
            End If
            File.Create(dllpath)
            Dim oWrite As System.IO.StreamWriter = File.CreateText(txtpath)
            oWrite.WriteLine(objSec.Encry(Now.ToString) & "," & objSec.Encry(DateAdd(DateInterval.Day, 7, Now).ToString))
            oWrite.Close()
        End If
    End Sub

End Class
