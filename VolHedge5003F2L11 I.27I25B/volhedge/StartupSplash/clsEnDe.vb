Imports System.Security.Cryptography
Imports System.Text
'Imports System.Globalization
'Imports System.Drawing
'Imports System.IO


Public Class clsUEnDe

    Public Function EFo(ByVal usr As Double) As Double
        'On Error GoTo encodeerror
        'Try
        'Dim lngTik As Integer = System.Environment.TickCount
        Dim result As Double = usr
        result = result * 100
        result = result / 17
        result = result + 9527
        Return Math.Round(result, 2)
        'MsgBox("Encode Time : " & (System.Environment.TickCount - lngTik))
        '        Exit Function
        'encodeerror:
        '        MsgBox(Err.Description)
        '        Exit Function
        'Catch ex As Exception
        'WriteLog("Proc::FormClosing:MulticastListner_currency:-->" & vbCrLf & ex.Message)
        'End Try

    End Function
    Public Function EEq(ByVal usr As Double) As Double
        'On Error GoTo encodeerror

        'Dim lngTik As Integer = System.Environment.TickCount
        Dim result As Double = usr
        result = result * 100
        result = result / 17
        result = result + 9527
        Return Math.Round(result, 2)
        'MsgBox("Encode Time : " & (System.Environment.TickCount - lngTik))
        '        Exit Function
        'encodeerror:
        '        MsgBox(Err.Description)
        '        Exit Function
    End Function
    Public Function ECur(ByVal usr As Double) As Double
        'On Error GoTo encodeerror

        'Dim lngTik As Integer = System.Environment.TickCount
        Dim result As Double = usr
        result = result * 100
        result = result / 17
        result = result + 9527
        Return Math.Round(result, 4)
        'MsgBox("Encode Time : " & (System.Environment.TickCount - lngTik))
        '        Exit Function
        'encodeerror:
        '        MsgBox(Err.Description)
        '        Exit Function
    End Function
    Public Function EOI(ByVal usr As Double) As Double
        'On Error GoTo encodeerror

        'Dim lngTik As Integer = System.Environment.TickCount
        Dim result As Double = usr
        result = result * 100
        result = result / 17
        result = result + 9527
        Return Math.Round(result, 2)
        'MsgBox("Encode Time : " & (System.Environment.TickCount - lngTik))
        '        Exit Function
        'encodeerror:
        '        MsgBox(Err.Description)
        '        Exit Function
    End Function
    Public Function Einx(ByVal usr As Double) As Double
        'On Error GoTo encodeerror

        'Dim lngTik As Integer = System.Environment.TickCount
        Dim result As Double = usr
        result = result * 100
        result = result / 17
        result = result + 9527
        Return Math.Round(result, 2)
        'MsgBox("Encode Time : " & (System.Environment.TickCount - lngTik))
        '        Exit Function
        'encodeerror:
        '        MsgBox(Err.Description)
        '        Exit Function
    End Function

    Public Function D(ByVal usr As Double) As Double
        'On Error GoTo decodeerror

        'Dim lngTik As Integer = System.Environment.TickCount
        Dim result As Double = usr
        result = result - 9527
        result = result * 17
        result = result / 100
        Return Math.Round(result, 2)

        'MsgBox("Decode Time : " & (System.Environment.TickCount - lngTik))
        '        Exit Function
        'decodeerror:
        '        MsgBox(Err.Description)
        '        Exit Function
    End Function

    Public Shared Function FEnc(ByVal usr As String) As String
        'On Error GoTo encodeerror
        Dim key As Integer = 9
        'Dim lngTik As Integer = System.Environment.TickCount
        Dim result As String = ""
        ' Dim aasc As Integer
        'Dim easc As Integer
        For count As Integer = 1 To Len(usr)
            result = result + Chr(LTrim(Str(Asc(Mid(usr, count, 1)) + key)))
            'easc = Asc(Mid(usr, count, 1)) + key
            'aasc = Str(easc)
            'result = result + Chr(aasc)
        Next count
        Return result

        'MsgBox("Encode Time : " & (System.Environment.TickCount - lngTik))
        '        Exit Function
        'encodeerror:
        '        MsgBox(Err.Description)
        '        Exit Function
        'Return usr
    End Function

    Public Shared Function FDec(ByVal usr As String) As String
        'On Error GoTo decodeerror
        Dim key As Integer = 9
        'Dim lngTik As Integer = System.Environment.TickCount
        Dim result As String = ""
        For count As Integer = 1 To Len(usr)
            result = result + Chr(LTrim(Str(Asc(Mid(usr, count, 1)) - key)))
        Next count
        Return result

        'MsgBox("Decode Time : " & (System.Environment.TickCount - lngTik))
        '        Exit Function
        'decodeerror:
        '        MsgBox(Err.Description)
        '        Exit Function
        'Return usr
    End Function
    Public Function DFo(ByVal usr As Double) As Double
        'On Error GoTo decodeerror

        'Dim lngTik As Integer = System.Environment.TickCount
        Dim result As Double = usr
        result = result - 9527
        result = result * 17
        result = result / 100
        Return Math.Round(result, 2)

        'MsgBox("Decode Time : " & (System.Environment.TickCount - lngTik))
        '        Exit Function
        'decodeerror:
        '        MsgBox(Err.Description)
        '        Exit Function
    End Function

    Public Function DEq(ByVal usr As Double) As Double
        'On Error GoTo decodeerror

        'Dim lngTik As Integer = System.Environment.TickCount
        Dim result As Double = usr
        result = result - 9527
        result = result * 17
        result = result / 100
        Return Math.Round(result, 2)

        'MsgBox("Decode Time : " & (System.Environment.TickCount - lngTik))
        '        Exit Function
        'decodeerror:
        '        MsgBox(Err.Description)
        '        Exit Function
    End Function

    Public Function DCur(ByVal usr As Double) As Double
        'On Error GoTo decodeerror

        'Dim lngTik As Integer = System.Environment.TickCount
        Dim result As Double = usr
        result = result - 9527
        result = result * 17
        result = result / 100
        Return Math.Round(result, 4)

        'MsgBox("Decode Time : " & (System.Environment.TickCount - lngTik))
        '        Exit Function
        'decodeerror:
        '        MsgBox(Err.Description)
        '        Exit Function
    End Function


    'Public Shared Function MD5Hash(ByVal value As String) As [Byte]()
    '    Return MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value))
    'End Function

    'Public Shared Function Encrypt(ByVal stringToEncrypt As String) As String
    '    DES.Key = clsEncription.MD5Hash(clsEncription.Key)
    '    DES.Mode = CipherMode.ECB
    '    Dim Buffer As [Byte]() = ASCIIEncoding.ASCII.GetBytes(stringToEncrypt)
    '    Return Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length))
    'End Function


    'Public Shared Function Decrypt(ByVal encryptedString As String) As String
    '    Try
    '        DES.Key = MD5Hash(clsEncription.Key)
    '        DES.Mode = CipherMode.ECB
    '        Dim Buffer As [Byte]() = Convert.FromBase64String(encryptedString)
    '        Return ASCIIEncoding.ASCII.GetString(DES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length))
    '    Catch ex As Exception
    '        '        MessageBox.Show("Invalid Key", "Decryption Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Return ""
    '    End Try
    'End Function

End Class
