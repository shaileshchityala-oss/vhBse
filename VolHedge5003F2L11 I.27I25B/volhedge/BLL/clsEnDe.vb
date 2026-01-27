Public Class clsEnDe

    Public Function E(ByVal usr As Double) As Double
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




    Public Function E_Old(ByVal usr As String) As String
        ''On Error GoTo encodeerror
        'Dim key As Integer = 9
        ''Dim lngTik As Integer = System.Environment.TickCount
        'Dim result As String = ""
        'For count As Integer = 1 To Len(usr)
        '    result = result + Chr(LTrim(Str(Asc(Mid(usr, count, 1)) + key)))
        'Next count
        'Return result

        ''MsgBox("Encode Time : " & (System.Environment.TickCount - lngTik))
        ''        Exit Function
        ''encodeerror:
        ''        MsgBox(Err.Description)
        ''        Exit Function
        Return usr
    End Function

    Public Function D_Old(ByVal usr As String) As String
        ''On Error GoTo decodeerror
        'Dim key As Integer = 9
        ''Dim lngTik As Integer = System.Environment.TickCount
        'Dim result As String = ""
        'For count As Integer = 1 To Len(usr)
        '    result = result + Chr(LTrim(Str(Asc(Mid(usr, count, 1)) - key)))
        'Next count
        'Return result

        ''MsgBox("Decode Time : " & (System.Environment.TickCount - lngTik))
        ''        Exit Function
        ''decodeerror:
        ''        MsgBox(Err.Description)
        ''        Exit Function
        Return usr
    End Function
End Class
