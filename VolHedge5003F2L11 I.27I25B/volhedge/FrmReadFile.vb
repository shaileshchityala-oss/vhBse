Imports System.IO
Public Class FrmReadFile

    Private Sub ButReadFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButReadSecFile.Click
        If txtpath.Text <> "" Then
            Me.Cursor = Cursors.WaitCursor
            ' Try
            Dim dtable As DataTable
            dtable = New DataTable
            'With dtable.Columns
            '    .Add("token", GetType(Integer))
            '    '============================keval(16-2-10)
            '    .Add("asset_tokan", GetType(Integer))
            '    '==============================
            '    .Add("InstrumentName", GetType(String))
            '    .Add("Symbol", GetType(String))
            '    .Add("Siries", GetType(String))
            '    .Add("ExpiryDate", GetType(Integer))
            '    .Add("StrikePrice", GetType(Double))
            '    .Add("OptionType", GetType(String))
            '    .Add("script", GetType(String))
            '    .Add("lotsize", GetType(Integer))
            'End With
            'Dim drow As DataRow
            Dim date1 As Date = "1/1/1980"
            If txtpath.Text.Trim <> "" Then
                If File.Exists(txtpath.Text) Then
                    Dim iline As New StreamReader(txtpath.Text)
                    Dim chk As Boolean
                    chk = False
                    Dim i As Integer = 0
                    Dim OrgArrayStr() As Char
                    Dim StrByte As Byte()
                    Dim Transaction_Code As String = ""
                    Dim Timestamp As String = ""
                    Dim Message_Length As String = ""
                    Dim Filler As String = ""
                    Dim Security_Token As String = ""
                    Dim Instrument_Name As String = ""
                    Dim Symbol As String = ""
                    Dim Expiry_Date As String = ""
                    Dim Strike_Price As String = ""
                    Dim Option_Type As String = ""
                    Dim Market_Type As String = ""
                    Dim Best_Buy_Price As String = ""
                    Dim Best_Buy_Quantity As String = ""
                    Dim Best_Sell_Price As String = ""
                    Dim Best_Sell_Quantity As String = ""
                    Dim Last_Traded_Price As String = ""
                    Dim Total_Traded_Quantity As String = ""
                    Dim Average_Traded_Price As String = ""
                    Dim Security_Status As String = ""
                    Dim Open_Price As String = ""
                    Dim High_Price As String = ""
                    Dim Low_Price As String = ""
                    Dim Close_Price As String = ""


                    'Dim StrByte() As Byte
                    Dim Linestr As String
                    'Dim SelCharRead As String
                    'While iline.EndOfStream = False
                    '    Debug.Print(iline.ReadLine)
                    'End While
                    While iline.EndOfStream = False
                        If i > 0 Then
                            'Debug.Print(iline.ReadLine)
                            chk = True
                            'Dim LStr As String
                            Dim line As String()
                            'LStr = iline.ReadLine
                            'MsgBox(LStr)
                            'LStr = iline.ReadLine
                            'MsgBox(LStr)
                            Linestr = iline.ReadLine
                            'SelCharRead = iline.Read(Linestr, 0, 173)
                            line = Split(iline.ReadLine, " ")

                            'OrgArrayStr = line(0).ToCharArray
                            OrgArrayStr = Linestr.ToCharArray
                            StrByte = System.Text.Encoding.Unicode.GetBytes(OrgArrayStr)
                            ''For J As Integer = 0 To 1
                            ''    Transaction_Code &= Trim(OrgArrayStr(J))
                            ''Next
                            ''For J As Integer = 2 To 5
                            ''    Timestamp &= Trim(OrgArrayStr(J))
                            ''Next
                            ''For J As Integer = 6 To 7
                            ''    Message_Length &= Trim(OrgArrayStr(J))
                            ''Next
                            ''Filler = Trim(OrgArrayStr(8))

                            ''For J As Integer = 9 To 12
                            ''    Security_Token &= Trim(OrgArrayStr(J))
                            ''Next

                            ''For J As Integer = 12 To 17
                            ''    Instrument_Name = Instrument_Name & "" & OrgArrayStr(J).ToString
                            ''Next

                            ''For J As Integer = 18 To 27
                            ''    Symbol &= Trim(OrgArrayStr(J))
                            ''Next

                            ''For J As Integer = 28 To 37
                            ''    Expiry_Date &= Trim(OrgArrayStr(J))
                            ''Next

                            Transaction_Code = Trim(OrgArrayStr(0) & "" & OrgArrayStr(1))
                            Timestamp = Trim(OrgArrayStr(2) & "" & OrgArrayStr(3) & "" & OrgArrayStr(4) & "" & OrgArrayStr(5))
                            Message_Length = Trim(OrgArrayStr(6) & "" & OrgArrayStr(7))
                            Filler = Trim(OrgArrayStr(8))
                            Security_Token = Trim(OrgArrayStr(9) & "" & OrgArrayStr(10) & "" & OrgArrayStr(11) & "" & OrgArrayStr(12))
                            'Instrument_Name = Trim(OrgArrayStr(13) & "" & OrgArrayStr(14) & "" & OrgArrayStr(15) & "" & OrgArrayStr(16) & "" & OrgArrayStr(17) & "" & OrgArrayStr(18))
                            Instrument_Name = Trim(OrgArrayStr(12) & "" & OrgArrayStr(13) & "" & OrgArrayStr(14) & "" & OrgArrayStr(15) & "" & OrgArrayStr(16) & "" & OrgArrayStr(17))
                            'Symbol = Trim(OrgArrayStr(19) & "" & OrgArrayStr(20) & "" & OrgArrayStr(21) & "" & OrgArrayStr(22) & "" & OrgArrayStr(23) & "" & OrgArrayStr(24) & "" & OrgArrayStr(25) & "" & OrgArrayStr(26) & "" & OrgArrayStr(27) & "" & OrgArrayStr(28))
                            Symbol = Trim(OrgArrayStr(18) & "" & OrgArrayStr(19) & "" & OrgArrayStr(20) & "" & OrgArrayStr(21) & "" & OrgArrayStr(22) & "" & OrgArrayStr(23) & "" & OrgArrayStr(24) & "" & OrgArrayStr(25) & "" & OrgArrayStr(26) & "" & OrgArrayStr(27))
                            'Expiry_Date = Trim(OrgArrayStr(29) & "" & OrgArrayStr(30) & "" & OrgArrayStr(31) & "" & OrgArrayStr(32) & "" & OrgArrayStr(33) & "" & OrgArrayStr(34) & "" & OrgArrayStr(35) & "" & OrgArrayStr(36) & "" & OrgArrayStr(37) & "" & OrgArrayStr(38))
                            Expiry_Date = Trim(OrgArrayStr(28) & "" & OrgArrayStr(29) & "" & OrgArrayStr(30) & "" & OrgArrayStr(31) & "" & OrgArrayStr(32) & "" & OrgArrayStr(33) & "" & OrgArrayStr(34) & "" & OrgArrayStr(35) & "" & OrgArrayStr(36) & "" & OrgArrayStr(37))
                            'Strike_Price = Trim(OrgArrayStr(38) & "" & OrgArrayStr(39) & "" & OrgArrayStr(40) & "" & OrgArrayStr(41) & "" & OrgArrayStr(42) & "" & OrgArrayStr(43) & "" & OrgArrayStr(44) & "" & OrgArrayStr(45) & "" & OrgArrayStr(46))
                            Strike_Price = Trim(OrgArrayStr(55) & "" & OrgArrayStr(56) & "" & OrgArrayStr(57) & "" & OrgArrayStr(58) & "" & OrgArrayStr(59) & "" & OrgArrayStr(60) & "" & OrgArrayStr(61) & "" & OrgArrayStr(62) & "" & OrgArrayStr(63))
                            Option_Type = Trim(OrgArrayStr(64) & "" & OrgArrayStr(65))
                            Market_Type = Trim(OrgArrayStr(66))
                            Best_Buy_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76))

                            Best_Buy_Quantity = Trim(OrgArrayStr(59) & "" & OrgArrayStr(60) & "" & OrgArrayStr(61) & "" & OrgArrayStr(62) & "" & OrgArrayStr(63) & "" & OrgArrayStr(64) & "" & OrgArrayStr(65) & "" & OrgArrayStr(66) & "" & OrgArrayStr(67) & "" & OrgArrayStr(68) & "" & OrgArrayStr(69))

                            Best_Sell_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            Best_Sell_Quantity = Trim(OrgArrayStr(79) & "" & OrgArrayStr(80) & "" & OrgArrayStr(81) & "" & OrgArrayStr(82) & "" & OrgArrayStr(83) & "" & OrgArrayStr(84) & "" & OrgArrayStr(85) & "" & OrgArrayStr(86) & "" & OrgArrayStr(87) & "" & OrgArrayStr(88) & "" & OrgArrayStr(89))

                            Last_Traded_Price = Trim(OrgArrayStr(90) & "" & OrgArrayStr(91) & "" & OrgArrayStr(92) & "" & OrgArrayStr(93) & "" & OrgArrayStr(94) & "" & OrgArrayStr(95) & "" & OrgArrayStr(96) & "" & OrgArrayStr(97) & "" & OrgArrayStr(98))
                            Total_Traded_Quantity = Trim(OrgArrayStr(99) & "" & OrgArrayStr(100) & "" & OrgArrayStr(101) & "" & OrgArrayStr(102) & "" & OrgArrayStr(103) & "" & OrgArrayStr(104) & "" & OrgArrayStr(105) & "" & OrgArrayStr(106) & "" & OrgArrayStr(107) & "" & OrgArrayStr(108) & "" & OrgArrayStr(109))

                            Average_Traded_Price = Trim(OrgArrayStr(110) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            Security_Status = Trim(OrgArrayStr(70))

                            Open_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            High_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            Low_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            Close_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))



                            'line(0)
                            ' ''If line(2) <> "" Then
                            ' ''    drow = dtable.NewRow
                            ' ''    drow("Token") = CInt(line(0))
                            ' ''    '======================keval(16-2-10)
                            ' ''    drow("Asset_Tokan") = CInt(line(1))
                            ' ''    '=======================
                            ' ''    drow("InstrumentName") = CStr(line(2))
                            ' ''    drow("Symbol") = CStr(line(3))
                            ' ''    drow("Siries") = CStr(line(4))
                            ' ''    drow("ExpiryDate") = CInt(line(6))
                            ' ''    drow("StrikePrice") = CDbl(line(7)) / 100
                            ' ''    drow("OptionType") = CStr(line(8))
                            ' ''    If Not IsDBNull(drow("OptionType")) Then
                            ' ''        If Mid(UCase(drow("OptionType")), 1, 1) = "C" Or Mid(UCase(drow("OptionType")), 1, 1) = "P" Then
                            ' ''            drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy") & "  " & CStr(Format(Val(drow("StrikePrice")), "###0.00")) & "  " & drow("OptionType")
                            ' ''        Else
                            ' ''            drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy")
                            ' ''        End If
                            ' ''    Else
                            ' ''        drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy")
                            ' ''    End If

                            ' ''    drow("lotsize") = CStr(line(31))
                            ' ''    dtable.Rows.Add(drow)
                            ' ''End If
                            'lblcount.Text = CInt(lblcount.Text) + 1
                            'lblcount.Refresh()
                        End If
                        i = i + 1
                    End While
                    iline.Close()
                    'objMast.insert(dtable)
                    If chk = True Then
                        fill_token()
                        MsgBox("File Imported Successfully.", MsgBoxStyle.Information)
                        txtpath.Text = ""
                        'lblcount.Text = 0
                        'lblcount.Refresh()
                    End If
                End If
            End If
            Me.Cursor = Cursors.Default
        Else
            MsgBox("Please Select File Path.", MsgBoxStyle.Exclamation)
        End If
    End Sub
    Private Sub ButReadDatFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButReadDatFile.Click
        txtpath.Text = "D:\FinIdeas Projects\Securities.dat"
        If txtpath.Text <> "" Then
            Me.Cursor = Cursors.WaitCursor
            ' Try
            Dim dtable As DataTable
            dtable = New DataTable
            'With dtable.Columns
            '    .Add("token", GetType(Integer))
            '    '============================keval(16-2-10)
            '    .Add("asset_tokan", GetType(Integer))
            '    '==============================
            '    .Add("InstrumentName", GetType(String))
            '    .Add("Symbol", GetType(String))
            '    .Add("Siries", GetType(String))
            '    .Add("ExpiryDate", GetType(Integer))
            '    .Add("StrikePrice", GetType(Double))
            '    .Add("OptionType", GetType(String))
            '    .Add("script", GetType(String))
            '    .Add("lotsize", GetType(Integer))
            'End With
            'Dim drow As DataRow
            Dim date1 As Date = "1/1/1980"
            If txtpath.Text.Trim <> "" Then
                If File.Exists(txtpath.Text) Then
                    Dim iline As New StreamReader(txtpath.Text)
                    Dim chk As Boolean
                    chk = False
                    Dim i As Integer = 0
                    Dim OrgArrayStr() As Char
                    Dim StrByte(131) As Byte
                    'Dim FullStrByte() As Byte

                    Dim Transaction_Code As String = ""
                    Dim Timestamp As String = ""
                    Dim Message_Length As String = ""
                    Dim Security_Token As String = ""
                    Dim Instrument_Name As String = ""
                    Dim Symbol As String = ""
                    Dim Series As String = ""
                    Dim Option_Type As String = ""
                    Dim Expiry_Date As String = ""
                    Dim Strike_Price As String = ""
                    Dim Issue_Start_Date As String = ""
                    Dim Issue_Maturity_Date As String = ""
                    Dim Board_Lot_Quantity As String = ""
                    Dim Tick_Size As String = ""
                    Dim Security_Name As String = ""
                    Dim Record_Date As String = ""
                    Dim Ex_Date As String = ""
                    Dim No_Delivery_Start_Date As String = ""
                    Dim No_Delivery_End_Date As String = ""
                    Dim Book_Closure_Start_Date As String = ""
                    Dim Book_Closure_End_Date As String = ""
                    Dim Remarks As String = ""
                    Dim Filler As String = ""



                    'Dim StrByte() As Byte
                    Dim Linestr As String
                    'Dim SelCharRead As String
                    'Dim Fulldatastr As String
                    'While iline.EndOfStream = False
                    '    Debug.Print(iline.ReadLine)
                    'End While
                    While iline.EndOfStream = False
                        If i > 0 Then
                            'Debug.Print(iline.ReadLine)
                            chk = True
                            'Dim LStr As String
                            Dim line As String()
                            'LStr = iline.ReadLine
                            'MsgBox(LStr)
                            'LStr = iline.ReadLine
                            'MsgBox(LStr)
                            'Fulldatastr = iline.ReadToEnd
                            'FullStrByte = System.Text.Encoding.Unicode.GetBytes(Fulldatastr)

                            Linestr = iline.ReadLine
                            'SelCharRead = iline.Read(Linestr, 0, 173)
                            line = Split(iline.ReadLine, " ")

                            'OrgArrayStr = line(0).ToCharArray
                            OrgArrayStr = Linestr.ToCharArray
                            StrByte = System.Text.Encoding.Unicode.GetBytes(Linestr)
                            ''For J As Integer = 0 To 1
                            ''    Transaction_Code &= Trim(OrgArrayStr(J))
                            ''Next
                            ''For J As Integer = 2 To 5
                            ''    Timestamp &= Trim(OrgArrayStr(J))
                            ''Next
                            ''For J As Integer = 6 To 7
                            ''    Message_Length &= Trim(OrgArrayStr(J))
                            ''Next
                            ''Filler = Trim(OrgArrayStr(8))

                            ''For J As Integer = 9 To 12
                            ''    Security_Token &= Trim(OrgArrayStr(J))
                            ''Next

                            ''For J As Integer = 12 To 17
                            ''    Instrument_Name = Instrument_Name & "" & OrgArrayStr(J).ToString
                            ''Next

                            ''For J As Integer = 18 To 27
                            ''    Symbol &= Trim(OrgArrayStr(J))
                            ''Next

                            ''For J As Integer = 28 To 37
                            ''    Expiry_Date &= Trim(OrgArrayStr(J))
                            ''Next

                            Transaction_Code = Trim(OrgArrayStr(0) & "" & OrgArrayStr(1))
                            Transaction_Code = System.Text.Encoding.Unicode.GetString(StrByte)

                            Timestamp = Trim(OrgArrayStr(2) & "" & OrgArrayStr(3) & "" & OrgArrayStr(4) & "" & OrgArrayStr(5))
                            Message_Length = Trim(OrgArrayStr(6) & "" & OrgArrayStr(7))
                            Filler = Trim(OrgArrayStr(8))
                            Security_Token = Trim(OrgArrayStr(9) & "" & OrgArrayStr(10) & "" & OrgArrayStr(11) & "" & OrgArrayStr(12))
                            'Instrument_Name = Trim(OrgArrayStr(13) & "" & OrgArrayStr(14) & "" & OrgArrayStr(15) & "" & OrgArrayStr(16) & "" & OrgArrayStr(17) & "" & OrgArrayStr(18))
                            Instrument_Name = Trim(OrgArrayStr(12) & "" & OrgArrayStr(13) & "" & OrgArrayStr(14) & "" & OrgArrayStr(15) & "" & OrgArrayStr(16) & "" & OrgArrayStr(17))
                            'Symbol = Trim(OrgArrayStr(19) & "" & OrgArrayStr(20) & "" & OrgArrayStr(21) & "" & OrgArrayStr(22) & "" & OrgArrayStr(23) & "" & OrgArrayStr(24) & "" & OrgArrayStr(25) & "" & OrgArrayStr(26) & "" & OrgArrayStr(27) & "" & OrgArrayStr(28))
                            Symbol = Trim(OrgArrayStr(18) & "" & OrgArrayStr(19) & "" & OrgArrayStr(20) & "" & OrgArrayStr(21) & "" & OrgArrayStr(22) & "" & OrgArrayStr(23) & "" & OrgArrayStr(24) & "" & OrgArrayStr(25) & "" & OrgArrayStr(26) & "" & OrgArrayStr(27))
                            'Expiry_Date = Trim(OrgArrayStr(29) & "" & OrgArrayStr(30) & "" & OrgArrayStr(31) & "" & OrgArrayStr(32) & "" & OrgArrayStr(33) & "" & OrgArrayStr(34) & "" & OrgArrayStr(35) & "" & OrgArrayStr(36) & "" & OrgArrayStr(37) & "" & OrgArrayStr(38))
                            Expiry_Date = Trim(OrgArrayStr(28) & "" & OrgArrayStr(29) & "" & OrgArrayStr(30) & "" & OrgArrayStr(31) & "" & OrgArrayStr(32) & "" & OrgArrayStr(33) & "" & OrgArrayStr(34) & "" & OrgArrayStr(35) & "" & OrgArrayStr(36) & "" & OrgArrayStr(37))
                            'Strike_Price = Trim(OrgArrayStr(38) & "" & OrgArrayStr(39) & "" & OrgArrayStr(40) & "" & OrgArrayStr(41) & "" & OrgArrayStr(42) & "" & OrgArrayStr(43) & "" & OrgArrayStr(44) & "" & OrgArrayStr(45) & "" & OrgArrayStr(46))
                            Strike_Price = Trim(OrgArrayStr(55) & "" & OrgArrayStr(56) & "" & OrgArrayStr(57) & "" & OrgArrayStr(58) & "" & OrgArrayStr(59) & "" & OrgArrayStr(60) & "" & OrgArrayStr(61) & "" & OrgArrayStr(62) & "" & OrgArrayStr(63))
                            Option_Type = Trim(OrgArrayStr(64) & "" & OrgArrayStr(65))
                            'Market_Type = Trim(OrgArrayStr(66))
                            'Best_Buy_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76))

                            'Best_Buy_Quantity = Trim(OrgArrayStr(59) & "" & OrgArrayStr(60) & "" & OrgArrayStr(61) & "" & OrgArrayStr(62) & "" & OrgArrayStr(63) & "" & OrgArrayStr(64) & "" & OrgArrayStr(65) & "" & OrgArrayStr(66) & "" & OrgArrayStr(67) & "" & OrgArrayStr(68) & "" & OrgArrayStr(69))

                            'Best_Sell_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            'Best_Sell_Quantity = Trim(OrgArrayStr(79) & "" & OrgArrayStr(80) & "" & OrgArrayStr(81) & "" & OrgArrayStr(82) & "" & OrgArrayStr(83) & "" & OrgArrayStr(84) & "" & OrgArrayStr(85) & "" & OrgArrayStr(86) & "" & OrgArrayStr(87) & "" & OrgArrayStr(88) & "" & OrgArrayStr(89))

                            'Last_Traded_Price = Trim(OrgArrayStr(90) & "" & OrgArrayStr(91) & "" & OrgArrayStr(92) & "" & OrgArrayStr(93) & "" & OrgArrayStr(94) & "" & OrgArrayStr(95) & "" & OrgArrayStr(96) & "" & OrgArrayStr(97) & "" & OrgArrayStr(98))
                            'Total_Traded_Quantity = Trim(OrgArrayStr(99) & "" & OrgArrayStr(100) & "" & OrgArrayStr(101) & "" & OrgArrayStr(102) & "" & OrgArrayStr(103) & "" & OrgArrayStr(104) & "" & OrgArrayStr(105) & "" & OrgArrayStr(106) & "" & OrgArrayStr(107) & "" & OrgArrayStr(108) & "" & OrgArrayStr(109))

                            'Average_Traded_Price = Trim(OrgArrayStr(110) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            'Security_Status = Trim(OrgArrayStr(70))

                            'Open_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            'High_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            'Low_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            'Close_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))



                            'line(0)
                            ' ''If line(2) <> "" Then
                            ' ''    drow = dtable.NewRow
                            ' ''    drow("Token") = CInt(line(0))
                            ' ''    '======================keval(16-2-10)
                            ' ''    drow("Asset_Tokan") = CInt(line(1))
                            ' ''    '=======================
                            ' ''    drow("InstrumentName") = CStr(line(2))
                            ' ''    drow("Symbol") = CStr(line(3))
                            ' ''    drow("Siries") = CStr(line(4))
                            ' ''    drow("ExpiryDate") = CInt(line(6))
                            ' ''    drow("StrikePrice") = CDbl(line(7)) / 100
                            ' ''    drow("OptionType") = CStr(line(8))
                            ' ''    If Not IsDBNull(drow("OptionType")) Then
                            ' ''        If Mid(UCase(drow("OptionType")), 1, 1) = "C" Or Mid(UCase(drow("OptionType")), 1, 1) = "P" Then
                            ' ''            drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy") & "  " & CStr(Format(Val(drow("StrikePrice")), "###0.00")) & "  " & drow("OptionType")
                            ' ''        Else
                            ' ''            drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy")
                            ' ''        End If
                            ' ''    Else
                            ' ''        drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy")
                            ' ''    End If

                            ' ''    drow("lotsize") = CStr(line(31))
                            ' ''    dtable.Rows.Add(drow)
                            ' ''End If
                            'lblcount.Text = CInt(lblcount.Text) + 1
                            'lblcount.Refresh()
                        End If
                        i = i + 1
                    End While
                    iline.Close()
                    'objMast.insert(dtable)
                    If chk = True Then
                        fill_token()
                        MsgBox("File Imported Successfully.", MsgBoxStyle.Information)
                        txtpath.Text = ""
                        'lblcount.Text = 0
                        'lblcount.Refresh()
                    End If
                End If
            End If
            Me.Cursor = Cursors.Default
        Else
            MsgBox("Please Select File Path.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub ButReadFullDataFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButReadFullDataFile.Click
        txtpath.Text = "D:\FinIdeas Projects\Securities.dat"
        If txtpath.Text <> "" Then
            Me.Cursor = Cursors.WaitCursor
            ' Try
            Dim dtable As DataTable
            dtable = New DataTable
            'With dtable.Columns
            '    .Add("token", GetType(Integer))
            '    '============================keval(16-2-10)
            '    .Add("asset_tokan", GetType(Integer))
            '    '==============================
            '    .Add("InstrumentName", GetType(String))
            '    .Add("Symbol", GetType(String))
            '    .Add("Siries", GetType(String))
            '    .Add("ExpiryDate", GetType(Integer))
            '    .Add("StrikePrice", GetType(Double))
            '    .Add("OptionType", GetType(String))
            '    .Add("script", GetType(String))
            '    .Add("lotsize", GetType(Integer))
            'End With
            'Dim drow As DataRow
            Dim date1 As Date = "1/1/1980"
            If txtpath.Text.Trim <> "" Then
                If File.Exists(txtpath.Text) Then

                    Dim Fs As New FileStream(txtpath.Text, FileMode.Open)
                    Dim br As New BinaryReader(Fs)
                    Dim sb() As Byte
                    sb = bR.ReadBytes(132)
                    'Dim constr As String = ""
                    'For J As Integer = 0 To sb.Length - 1
                    '    constr &= Convert.ToString(sb(J))
                    'Next

                    MsgBox(System.Text.ASCIIEncoding.ASCII.GetString(sb))


                    'MsgBox(constr)
                    Dim iline As New StreamReader(txtpath.Text, True)
                    Dim chk As Boolean
                    chk = False
                    Dim i As Integer = 0
                    'Dim OrgArrayStr() As Char
                    Dim StrByte(131) As Byte
                    Dim FullStrByte() As Byte

                    Dim Transaction_Code As String = ""
                    Dim Timestamp As String = ""
                    Dim Message_Length As String = ""
                    Dim Security_Token As String = ""
                    Dim Instrument_Name As String = ""
                    Dim Symbol As String = ""
                    Dim Series As String = ""
                    Dim Option_Type As String = ""
                    Dim Expiry_Date As String = ""
                    Dim Strike_Price As String = ""
                    Dim Issue_Start_Date As String = ""
                    Dim Issue_Maturity_Date As String = ""
                    Dim Board_Lot_Quantity As String = ""
                    Dim Tick_Size As String = ""
                    Dim Security_Name As String = ""
                    Dim Record_Date As String = ""
                    Dim Ex_Date As String = ""
                    Dim No_Delivery_Start_Date As String = ""
                    Dim No_Delivery_End_Date As String = ""
                    Dim Book_Closure_Start_Date As String = ""
                    Dim Book_Closure_End_Date As String = ""
                    Dim Remarks As String = ""
                    Dim Filler As String = ""



                    'Dim StrByte() As Byte
                    'Dim Linestr As String
                    'Dim SelCharRead As String
                    Dim Fulldatastr As String
                    'While iline.EndOfStream = False
                    '    Debug.Print(iline.ReadLine)
                    'End While
                    While iline.EndOfStream = False
                        If i > 0 Then
                            'Debug.Print(iline.ReadLine)
                            chk = True
                            'Dim LStr As String
                            'Dim line As String()
                            'LStr = iline.ReadLine
                            'MsgBox(LStr)
                            'LStr = iline.ReadLine
                            'MsgBox(LStr)
                            Fulldatastr = iline.ReadToEnd
                            FullStrByte = System.Text.Encoding.Unicode.GetBytes(Fulldatastr)


                            'Linestr = iline.ReadLine
                            'SelCharRead = iline.Read(Linestr, 0, 173)
                            'line = Split(iline.ReadLine, " ")

                            'OrgArrayStr = line(0).ToCharArray
                            'OrgArrayStr = Linestr.ToCharArray
                            'StrByte = System.Text.Encoding.Unicode.GetBytes(Linestr)
                            ''For J As Integer = 0 To 1
                            ''    Transaction_Code &= Trim(OrgArrayStr(J))
                            ''Next
                            ''For J As Integer = 2 To 5
                            ''    Timestamp &= Trim(OrgArrayStr(J))
                            ''Next
                            ''For J As Integer = 6 To 7
                            ''    Message_Length &= Trim(OrgArrayStr(J))
                            ''Next
                            ''Filler = Trim(OrgArrayStr(8))

                            ''For J As Integer = 9 To 12
                            ''    Security_Token &= Trim(OrgArrayStr(J))
                            ''Next

                            ''For J As Integer = 12 To 17
                            ''    Instrument_Name = Instrument_Name & "" & OrgArrayStr(J).ToString
                            ''Next

                            ''For J As Integer = 18 To 27
                            ''    Symbol &= Trim(OrgArrayStr(J))
                            ''Next

                            ''For J As Integer = 28 To 37
                            ''    Expiry_Date &= Trim(OrgArrayStr(J))
                            ''Next

                            Transaction_Code = Trim(FullStrByte(0) & "" & FullStrByte(1))
                            Timestamp = Trim(FullStrByte(2) & "" & FullStrByte(3) & "" & FullStrByte(4) & "" & FullStrByte(5))
                            Message_Length = Trim(FullStrByte(6) & "" & FullStrByte(7))
                            Filler = Trim(FullStrByte(8))
                            Security_Token = Trim(FullStrByte(9) & "" & FullStrByte(10) & "" & FullStrByte(11) & "" & FullStrByte(12))
                            'Instrument_Name = Trim(FullStrByte(13) & "" & FullStrByte(14) & "" & FullStrByte(15) & "" & FullStrByte(16) & "" & FullStrByte(17) & "" & FullStrByte(18))
                            Instrument_Name = Trim(FullStrByte(12) & "" & FullStrByte(13) & "" & FullStrByte(14) & "" & FullStrByte(15) & "" & FullStrByte(16) & "" & FullStrByte(17))
                            'Symbol = Trim(FullStrByte(19) & "" & FullStrByte(20) & "" & FullStrByte(21) & "" & FullStrByte(22) & "" & FullStrByte(23) & "" & FullStrByte(24) & "" & FullStrByte(25) & "" & FullStrByte(26) & "" & FullStrByte(27) & "" & FullStrByte(28))
                            Symbol = Trim(FullStrByte(18) & "" & FullStrByte(19) & "" & FullStrByte(20) & "" & FullStrByte(21) & "" & FullStrByte(22) & "" & FullStrByte(23) & "" & FullStrByte(24) & "" & FullStrByte(25) & "" & FullStrByte(26) & "" & FullStrByte(27))
                            'Expiry_Date = Trim(FullStrByte(29) & "" & FullStrByte(30) & "" & FullStrByte(31) & "" & FullStrByte(32) & "" & FullStrByte(33) & "" & FullStrByte(34) & "" & FullStrByte(35) & "" & FullStrByte(36) & "" & FullStrByte(37) & "" & FullStrByte(38))
                            Expiry_Date = Trim(FullStrByte(28) & "" & FullStrByte(29) & "" & FullStrByte(30) & "" & FullStrByte(31) & "" & FullStrByte(32) & "" & FullStrByte(33) & "" & FullStrByte(34) & "" & FullStrByte(35) & "" & FullStrByte(36) & "" & FullStrByte(37))
                            'Strike_Price = Trim(FullStrByte(38) & "" & FullStrByte(39) & "" & FullStrByte(40) & "" & FullStrByte(41) & "" & FullStrByte(42) & "" & FullStrByte(43) & "" & FullStrByte(44) & "" & FullStrByte(45) & "" & FullStrByte(46))
                            Strike_Price = Trim(FullStrByte(55) & "" & FullStrByte(56) & "" & StrByte(57) & "" & StrByte(58) & "" & StrByte(59) & "" & StrByte(60) & "" & StrByte(61) & "" & StrByte(62) & "" & StrByte(63))
                            Option_Type = Trim(StrByte(64) & "" & StrByte(65))

                            'Market_Type = Trim(StrByte(66))
                            'Best_Buy_Price = Trim(StrByte(70) & "" & StrByte(71) & "" & StrByte(72) & "" & StrByte(73) & "" & StrByte(74) & "" & StrByte(75) & "" & StrByte(76))

                            'Best_Buy_Quantity = Trim(StrByte(59) & "" & StrByte(60) & "" & OrgArrayStr(61) & "" & OrgArrayStr(62) & "" & OrgArrayStr(63) & "" & OrgArrayStr(64) & "" & OrgArrayStr(65) & "" & OrgArrayStr(66) & "" & OrgArrayStr(67) & "" & OrgArrayStr(68) & "" & OrgArrayStr(69))

                            'Best_Sell_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            'Best_Sell_Quantity = Trim(OrgArrayStr(79) & "" & OrgArrayStr(80) & "" & OrgArrayStr(81) & "" & OrgArrayStr(82) & "" & OrgArrayStr(83) & "" & OrgArrayStr(84) & "" & OrgArrayStr(85) & "" & OrgArrayStr(86) & "" & OrgArrayStr(87) & "" & OrgArrayStr(88) & "" & OrgArrayStr(89))

                            'Last_Traded_Price = Trim(OrgArrayStr(90) & "" & OrgArrayStr(91) & "" & OrgArrayStr(92) & "" & OrgArrayStr(93) & "" & OrgArrayStr(94) & "" & OrgArrayStr(95) & "" & OrgArrayStr(96) & "" & OrgArrayStr(97) & "" & OrgArrayStr(98))
                            'Total_Traded_Quantity = Trim(OrgArrayStr(99) & "" & OrgArrayStr(100) & "" & OrgArrayStr(101) & "" & OrgArrayStr(102) & "" & OrgArrayStr(103) & "" & OrgArrayStr(104) & "" & OrgArrayStr(105) & "" & OrgArrayStr(106) & "" & OrgArrayStr(107) & "" & OrgArrayStr(108) & "" & OrgArrayStr(109))

                            'Average_Traded_Price = Trim(OrgArrayStr(110) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            'Security_Status = Trim(OrgArrayStr(70))

                            'Open_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            'High_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            'Low_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))
                            'Close_Price = Trim(OrgArrayStr(70) & "" & OrgArrayStr(71) & "" & OrgArrayStr(72) & "" & OrgArrayStr(73) & "" & OrgArrayStr(74) & "" & OrgArrayStr(75) & "" & OrgArrayStr(76) & "" & OrgArrayStr(77) & "" & OrgArrayStr(78))



                            'line(0)
                            ' ''If line(2) <> "" Then
                            ' ''    drow = dtable.NewRow
                            ' ''    drow("Token") = CInt(line(0))
                            ' ''    '======================keval(16-2-10)
                            ' ''    drow("Asset_Tokan") = CInt(line(1))
                            ' ''    '=======================
                            ' ''    drow("InstrumentName") = CStr(line(2))
                            ' ''    drow("Symbol") = CStr(line(3))
                            ' ''    drow("Siries") = CStr(line(4))
                            ' ''    drow("ExpiryDate") = CInt(line(6))
                            ' ''    drow("StrikePrice") = CDbl(line(7)) / 100
                            ' ''    drow("OptionType") = CStr(line(8))
                            ' ''    If Not IsDBNull(drow("OptionType")) Then
                            ' ''        If Mid(UCase(drow("OptionType")), 1, 1) = "C" Or Mid(UCase(drow("OptionType")), 1, 1) = "P" Then
                            ' ''            drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy") & "  " & CStr(Format(Val(drow("StrikePrice")), "###0.00")) & "  " & drow("OptionType")
                            ' ''        Else
                            ' ''            drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy")
                            ' ''        End If
                            ' ''    Else
                            ' ''        drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy")
                            ' ''    End If

                            ' ''    drow("lotsize") = CStr(line(31))
                            ' ''    dtable.Rows.Add(drow)
                            ' ''End If
                            'lblcount.Text = CInt(lblcount.Text) + 1
                            'lblcount.Refresh()
                        End If
                        i = i + 1
                    End While
                    iline.Close()
                    'objMast.insert(dtable)
                    If chk = True Then
                        fill_token()
                        MsgBox("File Imported Successfully.", MsgBoxStyle.Information)
                        txtpath.Text = ""
                        'lblcount.Text = 0
                        'lblcount.Refresh()
                    End If
                End If
            End If
            Me.Cursor = Cursors.Default
        Else
            MsgBox("Please Select File Path.", MsgBoxStyle.Exclamation)
        End If
    End Sub
End Class