Imports System.IO
Imports System.Data
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Net
'Imports System.Net.Cache


Class HtmlPars
    Public Shared Function GetFoInterNetData(ByVal Symbol As String) As DataTable
        'textBox1.Multiline = True
        'textBox1.ScrollBars = ScrollBars.Both
        'above only for showing the sample

        Dim dt As New DataTable()

        dt.Columns.Add("Instrument", GetType(String))
        dt.Columns.Add("Underlying", GetType(String))
        dt.Columns.Add("ExpiryDate", GetType(String))
        dt.Columns.Add("OptionType", GetType(String))
        dt.Columns.Add("StrikePrice", GetType(Double))
        dt.Columns.Add("HighPrice", GetType(Double))
        dt.Columns.Add("LowPrice", GetType(Double))
        dt.Columns.Add("PrevClose", GetType(Double))
        dt.Columns.Add("LastPrice", GetType(Double))
        dt.Columns.Add("traded", GetType(Double))
        dt.Columns.Add("Turnover", GetType(Double))
        dt.Columns.Add("UnderlyingValue", GetType(Double))
        dt.Columns.Add("mdate", GetType(Date))
        dt.Columns.Add("script", GetType(String))
        dt.Columns.Add("Token", GetType(Long))
        dt.Columns.Add("eqToken", GetType(Long))

        Dim StrStrim As [String]

        StrStrim = ""
        Try

            'Dim Doc As mshtml.IHTMLDocument2 = New mshtml.HTMLDocumentClass()
            'HttpCacheAgeControl.None, TimeSpan.FromDays(1)
            'Dim requestPolicy As New HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore)


            'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fo/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
            Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
            'http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=NIFTY
            '("http://msdn.microsoft.com/");
            wbReq.ProtocolVersion = HttpVersion.Version11
            wbReq.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:9.0.1) Gecko/20100101 Firefox/9.0.1"
            'wbReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)"
            wbReq.Timeout = 100000
            wbReq.Accept = "*/*"
            wbReq.Headers.Add("Accept-Language: en-us,en;q=0.5")
            'wbReq.ContentType = "text/html";
            'wbReq.CachePolicy = requestPolicy
            Dim wbResp As HttpWebResponse = DirectCast(wbReq.GetResponse(), HttpWebResponse)
            Dim wbHCol As WebHeaderCollection = wbResp.Headers
            Dim myStream As Stream = wbResp.GetResponseStream()
            Dim myreader As New StreamReader(myStream)
            'textBox2.Text = ""
            StrStrim = myreader.ReadToEnd()
        Catch ex As Exception
            bool_IsTelNet = False
            dt.AcceptChanges()
            Return dt
            Exit Function
            'MsgBox(ex.ToString)
        End Try


        'textBox2.Text = StrStrim

        'Doc.write(StrStrim)
        'Doc.close()
        'wbResp.Close()

        'the part below is not completly done for all tags.
        'it can (will be for sure) necessary to tailor that to your needs.

        'Dim sb As New System.Text.StringBuilder()

        'Dim i As Integer = 0
        'While Doc.all.length - 1 > i
        '    Dim hElm As mshtml.IHTMLElement = DirectCast(Doc.all.item(i, i), mshtml.IHTMLElement)
        '    Dim hE As String = hElm.tagName.ToLower()
        '    'MessageBox.Show(hElm.tagName);
        '    If hE = "tr" Then
        '        'if (hE == "body" || hE == "html" || hE == "head")
        '        If hE <> "" Then

        '            'sb.Append(hElm.innerText + Environment.NewLine);

        '            sb.Append(hElm.innerHTML + Environment.NewLine)
        '        End If
        '    End If
        '    i += 1
        'End While

        'richTextBox1.Clear()
        'richTextBox1.Text = sb.ToString()




        If StrStrim Is Nothing Or StrStrim = "" Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If

        StrStrim = StrStrim.Replace(" class=t1", "")
        StrStrim = StrStrim.Replace(" class=t2", "")
        StrStrim = StrStrim.Replace(" class=t3", "")
        StrStrim = StrStrim.Replace(" class=t4", "")
        StrStrim = StrStrim.Replace(" class=t5", "")
        StrStrim = StrStrim.Replace(" class=""normalText""", "")
        StrStrim = StrStrim.Replace(" class=""date""", "")
        StrStrim = StrStrim.Replace(" class=""number""", "")

        'StrStrim = StrStrim.Replace(" class=tablehead", "")

        StrStrim = StrStrim.Replace("/live_market/dynaContent/live_watch/get_quote/GetQuoteFO.jsp?", "")
        StrStrim = StrStrim.Replace("</a>", "")
        StrStrim = StrStrim.Replace("<a", "")


        Dim strdata1 As [String]()
        Dim str As String() = New String(0) {}
        str(0) = "<TABLE"
        strdata1 = StrStrim.Split(str, StringSplitOptions.None)

        If strdata1.Length <= 1 Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If

        Dim strdata As [String]()
        str(0) = "</div>"
        strdata = strdata1(1).Split(str, StringSplitOptions.None)

        '

        'for (int i = 0; Doc.all.length - 1 > i; i++)
        '{
        '    mshtml.IHTMLElement hElm = (mshtml.IHTMLElement)Doc.all.item(i, i);
        '    string hE = hElm.tagName.ToLower();
        '    if (hE == "body") //|| hE == "html" || hE == "head")
        '    {
        '        if (hE != "")
        '        {
        '            sb.Append(hElm.innerText + Environment.NewLine);
        '        }
        '    }
        '}


        'Dim client As New WebClient()
        ' string html = client.DownloadString(@"http://www.nseindia.com/marketinfo/fo/fomwatchsymbol.jsp?key=NIFTY");

        If strdata.Length <= 4 Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If

        Dim dataSet As DataSet = HtmlTableParser.ParseDataSet("<TABLE" & strdata(0) & "</TABLE>")
        dt.Merge(dataSet.Tables(0))

        For Each drow As DataRow In dt.Rows
            If drow("ExpiryDate").ToString.Length > 0 Then
                drow("mdate") = DateSerial(Val(Microsoft.VisualBasic.Right(drow("ExpiryDate").ToString, 4)), Mnth(drow("ExpiryDate").ToString.Substring(2, 3)), Val(Microsoft.VisualBasic.Left(drow("ExpiryDate").ToString, 2)))
                Dim cp As String
                cp = Microsoft.VisualBasic.Left(drow("OptionType").ToString, 1)
                Dim script As String
                If cp = "-" Then
                    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
                Else
                    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice").ToString), "###0.00") & "  " & drow("OptionType").ToString
                End If
                drow("script") = script.Trim.ToUpper
                Try
                    drow("Token") = Val(cpfmaster.Compute("max(token)", "script='" & script.Trim & "'").ToString)
                    drow("eqToken") = Val(eqmaster.Compute("max(token)", "script='" & Symbol.Trim & "  EQ'").ToString)
                Catch ex As Exception
                    REM By Viral
                    'While Importing Contract cpfmaster or eqmaster Will Blank & Error Generate 
                    drow("Token") = 0
                    drow("eqToken") = 0
                End Try
            End If
        Next
        dt.AcceptChanges()
        ' MsgBox(dt.Compute("max(LastPrice)", "Token=48223").ToString)
        Return dt
    End Function
    Public Shared Function GetFoInterNetDataOld(ByVal Symbol As String) As DataTable
        'textBox1.Multiline = True
        'textBox1.ScrollBars = ScrollBars.Both
        'above only for showing the sample

        Dim dt As New DataTable()

        dt.Columns.Add("Instrument", GetType(String))
        dt.Columns.Add("Underlying", GetType(String))
        dt.Columns.Add("ExpiryDate", GetType(String))
        dt.Columns.Add("OptionType", GetType(String))
        dt.Columns.Add("StrikePrice", GetType(Double))
        dt.Columns.Add("HighPrice", GetType(Double))
        dt.Columns.Add("LowPrice", GetType(Double))
        dt.Columns.Add("PrevClose", GetType(Double))
        dt.Columns.Add("LastPrice", GetType(Double))
        dt.Columns.Add("traded", GetType(Double))
        dt.Columns.Add("Turnover", GetType(Double))
        dt.Columns.Add("UnderlyingValue", GetType(Double))
        dt.Columns.Add("mdate", GetType(Date))
        dt.Columns.Add("script", GetType(String))
        dt.Columns.Add("Token", GetType(Long))
        dt.Columns.Add("eqToken", GetType(Long))

        Dim StrStrim As [String]

        StrStrim = ""
        Try
            Dim Doc As mshtml.IHTMLDocument2 = New mshtml.HTMLDocumentClass()

            Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fo/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
            'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
            'http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=NIFTY
            '("http://msdn.microsoft.com/");
            wbReq.ProtocolVersion = HttpVersion.Version11
            wbReq.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:9.0.1) Gecko/20100101 Firefox/9.0.1"
            'wbReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)"
            wbReq.Timeout = 100000
            wbReq.Accept = "*/*"
            wbReq.Headers.Add("Accept-Language: en-us,en;q=0.5")
            'wbReq.ContentType = "text/html";

            Dim wbResp As HttpWebResponse = DirectCast(wbReq.GetResponse(), HttpWebResponse)
            Dim wbHCol As WebHeaderCollection = wbResp.Headers
            Dim myStream As Stream = wbResp.GetResponseStream()
            Dim myreader As New StreamReader(myStream)
            'textBox2.Text = ""
            StrStrim = myreader.ReadToEnd()
        Catch ex As Exception
            bool_IsTelNet = False
            dt.AcceptChanges()
            Return dt
            Exit Function
            'MsgBox(ex.ToString)
        End Try


        'textBox2.Text = StrStrim

        'Doc.write(StrStrim)
        'Doc.close()
        'wbResp.Close()

        'the part below is not completly done for all tags.
        'it can (will be for sure) necessary to tailor that to your needs.

        'Dim sb As New System.Text.StringBuilder()

        'Dim i As Integer = 0
        'While Doc.all.length - 1 > i
        '    Dim hElm As mshtml.IHTMLElement = DirectCast(Doc.all.item(i, i), mshtml.IHTMLElement)
        '    Dim hE As String = hElm.tagName.ToLower()
        '    'MessageBox.Show(hElm.tagName);
        '    If hE = "tr" Then
        '        'if (hE == "body" || hE == "html" || hE == "head")
        '        If hE <> "" Then

        '            'sb.Append(hElm.innerText + Environment.NewLine);

        '            sb.Append(hElm.innerHTML + Environment.NewLine)
        '        End If
        '    End If
        '    i += 1
        'End While

        'richTextBox1.Clear()
        'richTextBox1.Text = sb.ToString()




        If StrStrim Is Nothing Or StrStrim = "" Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If

        StrStrim = StrStrim.Replace(" class=t1", "")
        StrStrim = StrStrim.Replace(" class=t2", "")
        StrStrim = StrStrim.Replace(" class=t3", "")
        StrStrim = StrStrim.Replace(" class=t4", "")
        StrStrim = StrStrim.Replace(" class=t5", "")
        StrStrim = StrStrim.Replace("/marketinfo/fo/foquote.jsp?key=", "")
        StrStrim = StrStrim.Replace("</a>", "")
        StrStrim = StrStrim.Replace("<a", "")


        Dim strdata As [String]()
        Dim str As String() = New String(0) {}
        str(0) = "<TABLE"
        strdata = StrStrim.Split(str, StringSplitOptions.None)


        'for (int i = 0; Doc.all.length - 1 > i; i++)
        '{
        '    mshtml.IHTMLElement hElm = (mshtml.IHTMLElement)Doc.all.item(i, i);
        '    string hE = hElm.tagName.ToLower();
        '    if (hE == "body") //|| hE == "html" || hE == "head")
        '    {
        '        if (hE != "")
        '        {
        '            sb.Append(hElm.innerText + Environment.NewLine);
        '        }
        '    }
        '}


        'Dim client As New WebClient()
        ' string html = client.DownloadString(@"http://www.nseindia.com/marketinfo/fo/fomwatchsymbol.jsp?key=NIFTY");

        If strdata.Length <= 4 Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If

        Dim dataSet As DataSet = HtmlTableParser.ParseDataSet("<TABLE>" & strdata(5))
        dt.Merge(dataSet.Tables(0))

        For Each drow As DataRow In dt.Rows
            drow("mdate") = DateSerial(Val(Microsoft.VisualBasic.Right(drow("ExpiryDate").ToString, 4)), Mnth(drow("ExpiryDate").ToString.Substring(2, 3)), Val(Microsoft.VisualBasic.Left(drow("ExpiryDate").ToString, 2)))
            Dim cp As String
            cp = Microsoft.VisualBasic.Left(drow("OptionType").ToString, 1)
            Dim script As String
            If cp = "-" Then
                script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
            Else
                script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice").ToString), "###0.00") & "  " & drow("OptionType").ToString
            End If
            drow("script") = script.Trim.ToUpper
            Try
                drow("Token") = Val(cpfmaster.Compute("max(token)", "script='" & script.Trim & "'").ToString)
                drow("eqToken") = Val(eqmaster.Compute("max(token)", "script='" & Symbol.Trim & "  EQ'").ToString)
            Catch ex As Exception
                REM By Viral
                'While Importing Contract cpfmaster or eqmaster Will Blank & Error Generate 
                drow("Token") = 0
                drow("eqToken") = 0
            End Try
        Next
        dt.AcceptChanges()
        ' MsgBox(dt.Compute("max(LastPrice)", "Token=48223").ToString)
        Return dt
    End Function

    Private Shared Function Mnth(ByVal sM As String) As Integer

        Select Case sM
            Case "JAN"
                Return 1
            Case "FEB"
                Return 2
            Case "MAR"
                Return 3
            Case "APR"
                Return 4
            Case "MAY"
                Return 5
            Case "JUN"
                Return 6
            Case "JUL"
                Return 7
            Case "AUG"
                Return 8
            Case "SEP"
                Return 9
            Case "OCT"
                Return 10
            Case "NOV"
                Return 11
            Case "DEC"
                Return 12
        End Select
    End Function

    Public Shared Function GetCurInterNetData(ByVal Symbol As String) As DataTable
        'textBox1.Multiline = True
        'textBox1.ScrollBars = ScrollBars.Both
        'above only for showing the sample

        Dim dt As New DataTable()

        dt.Columns.Add("Instrument", GetType(String))
        dt.Columns.Add("Underlying", GetType(String))
        dt.Columns.Add("ExpiryDate", GetType(String))
        dt.Columns.Add("OptionType", GetType(String))
        dt.Columns.Add("StrikePrice", GetType(Double))
        dt.Columns.Add("HighPrice", GetType(Double))
        dt.Columns.Add("LowPrice", GetType(Double))
        dt.Columns.Add("PrevClose", GetType(Double))
        dt.Columns.Add("LastPrice", GetType(Double))
        dt.Columns.Add("traded", GetType(Double))
        dt.Columns.Add("Turnover", GetType(Double))
        dt.Columns.Add("UnderlyingValue", GetType(Double))
        dt.Columns.Add("mdate", GetType(Date))
        dt.Columns.Add("script", GetType(String))
        dt.Columns.Add("Token", GetType(Long))
        dt.Columns.Add("eqToken", GetType(Long))
        dt.Columns.Add("OI", GetType(Double))

        Dim StrStrim As [String]

        StrStrim = ""
        Try

            'Dim Doc As mshtml.IHTMLDocument2 = New mshtml.HTMLDocumentClass()
            'HttpCacheAgeControl.None, TimeSpan.FromDays(1)
            'Dim requestPolicy As New HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore)


            'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fo/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
            Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fxTracker/priceWatchData.jsp?instrument=FUTCUR&currency=" & Left(Symbol, 3)), HttpWebRequest)
            'http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=NIFTY
            '("http://msdn.microsoft.com/");
            wbReq.ProtocolVersion = HttpVersion.Version11
            wbReq.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:9.0.1) Gecko/20100101 Firefox/9.0.1"
            'wbReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)"
            wbReq.Timeout = 100000
            wbReq.Accept = "*/*"
            wbReq.Headers.Add("Accept-Language: en-us,en;q=0.5")
            'wbReq.ContentType = "text/html";
            'wbReq.CachePolicy = requestPolicy
            Dim wbResp As HttpWebResponse = DirectCast(wbReq.GetResponse(), HttpWebResponse)
            Dim wbHCol As WebHeaderCollection = wbResp.Headers
            Dim myStream As Stream = wbResp.GetResponseStream()
            Dim myreader As New StreamReader(myStream)
            'textBox2.Text = ""
            StrStrim = myreader.ReadToEnd()
        Catch ex As Exception
            bool_IsTelNet = False
            dt.AcceptChanges()
            Return dt
            Exit Function
            'MsgBox(ex.ToString)
        End Try


        If StrStrim Is Nothing Or StrStrim = "" Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If

        StrStrim = StrStrim.Replace(Chr(10), "")
        StrStrim = StrStrim.Replace("SUCCESS-;-", "")
        'StrStrim = StrStrim.Replace(" class=t3", "")
        'StrStrim = StrStrim.Replace(" class=t4", "")
        'StrStrim = StrStrim.Replace(" class=t5", "")
        'StrStrim = StrStrim.Replace(" class=""normalText""", "")
        'StrStrim = StrStrim.Replace(" class=""date""", "")
        'StrStrim = StrStrim.Replace(" class=""number""", "")

        'StrStrim = StrStrim.Replace(" class=tablehead", "")

        'StrStrim = StrStrim.Replace("/live_market/dynaContent/live_watch/get_quote/GetQuoteFO.jsp?", "")
        'StrStrim = StrStrim.Replace("</a>", "")
        'StrStrim = StrStrim.Replace("<a", "")


        Dim strdata1 As [String]()
        Dim str As String() = New String(0) {}
        str(0) = "~"
        strdata1 = StrStrim.Split(str, StringSplitOptions.None)

        If strdata1.Length <= 1 Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If

        Dim strdata As [String]()
        str(0) = ":"


        For Each Item As String In strdata1
            strdata = Item.Split(str, StringSplitOptions.None)
            Dim dr As DataRow
            dr = dt.NewRow()

            dr("Instrument") = strdata(1)
            dr("Underlying") = 0
            dr("ExpiryDate") = strdata(3)
            dr("OptionType") = "F"
            dr("StrikePrice") = 0
            dr("HighPrice") = 0
            dr("LowPrice") = 0
            dr("PrevClose") = 0 ', GetType(Double))
            dr("LastPrice") = strdata(10) ', GetType(Double))
            dr("traded") = strdata(14) ', GetType(Double))
            dr("Turnover") = strdata(13) ', GetType(Double))
            dr("UnderlyingValue") = 0 ', GetType(Double))
            dr("mdate") = CDate(strdata(3)) ', GetType(Date))
            dr("script") = "" ', GetType(String))
            dr("Token") = 0 ', GetType(Long))
            dr("eqToken") = 0 ', GetType(Long))
            dr("OI") = strdata(12)

            'Volume = strdata(11)
            'Change = 15
            'Change % = 16
            'CurrentDate = strdata(17)
            '-' 4,5
            '"" 18
            '6,7,8,9 = BestBid - BestAsk
            'Symbol = strdata(2)

            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next


        Dim ddt As New DataTable
        ddt = dt.Copy()

        For Each drow As DataRow In ddt.Select("", "mdate")
            dt.Merge(GetCurOptInterNetData(Symbol, drow("ExpiryDate")))
            dt.AcceptChanges()
        Next

        

        For Each drow As DataRow In dt.Rows
            If drow("ExpiryDate").ToString.Length > 0 Then
                drow("mdate") = DateSerial(Val(Microsoft.VisualBasic.Right(drow("ExpiryDate").ToString, 4)), Mnth(drow("ExpiryDate").ToString.Substring(2, 3)), Val(Microsoft.VisualBasic.Left(drow("ExpiryDate").ToString, 2)))
                Dim cp As String
                cp = Microsoft.VisualBasic.Left(drow("OptionType").ToString, 1)
                Dim script As String
                If cp = "-" Then
                    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
                Else
                    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice").ToString), "###0.00") & "  " & drow("OptionType").ToString
                End If
                drow("script") = script.Trim.ToUpper
                Try
                    drow("Token") = Val(cpfmaster.Compute("max(token)", "script='" & script.Trim & "'").ToString)
                    drow("eqToken") = Val(eqmaster.Compute("max(token)", "script='" & Symbol.Trim & "  EQ'").ToString)
                Catch ex As Exception
                    REM By Viral
                    'While Importing Contract cpfmaster or eqmaster Will Blank & Error Generate 
                    drow("Token") = 0
                    drow("eqToken") = 0
                End Try
            End If
        Next
        dt.AcceptChanges()
        ' MsgBox(dt.Compute("max(LastPrice)", "Token=48223").ToString)
        Return dt
    End Function

    Public Shared Function GetCurOptInterNetData(ByVal Symbol As String, ByVal Exp As String) As DataTable
        'textBox1.Multiline = True
        'textBox1.ScrollBars = ScrollBars.Both
        'above only for showing the sample

        Dim dt As New DataTable()

        dt.Columns.Add("Instrument", GetType(String))
        dt.Columns.Add("Underlying", GetType(String))
        dt.Columns.Add("ExpiryDate", GetType(String))
        dt.Columns.Add("OptionType", GetType(String))
        dt.Columns.Add("StrikePrice", GetType(Double))
        dt.Columns.Add("HighPrice", GetType(Double))
        dt.Columns.Add("LowPrice", GetType(Double))
        dt.Columns.Add("PrevClose", GetType(Double))
        dt.Columns.Add("LastPrice", GetType(Double))
        dt.Columns.Add("traded", GetType(Double))
        dt.Columns.Add("Turnover", GetType(Double))
        dt.Columns.Add("UnderlyingValue", GetType(Double))
        dt.Columns.Add("mdate", GetType(Date))
        dt.Columns.Add("script", GetType(String))
        dt.Columns.Add("Token", GetType(Long))
        dt.Columns.Add("eqToken", GetType(Long))
        dt.Columns.Add("OI", GetType(Double))

        Dim StrStrim As [String]

        StrStrim = ""
        Try

            'Dim Doc As mshtml.IHTMLDocument2 = New mshtml.HTMLDocumentClass()
            'HttpCacheAgeControl.None, TimeSpan.FromDays(1)
            'Dim requestPolicy As New HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore)


            'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fo/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
            Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/live_market/dynaContent/live_watch/fxTracker/optChainDataByExpDates.jsp?symbol=" & Symbol & "&instrument=OPTCUR&expiryDt=" & Exp), HttpWebRequest)
            'http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=NIFTY
            '("http://msdn.microsoft.com/");
            wbReq.ProtocolVersion = HttpVersion.Version11
            wbReq.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:9.0.1) Gecko/20100101 Firefox/9.0.1"
            'wbReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)"
            wbReq.Timeout = 100000
            wbReq.Accept = "*/*"
            wbReq.Headers.Add("Accept-Language: en-us,en;q=0.5")
            'wbReq.ContentType = "text/html";
            'wbReq.CachePolicy = requestPolicy
            Dim wbResp As HttpWebResponse = DirectCast(wbReq.GetResponse(), HttpWebResponse)
            Dim wbHCol As WebHeaderCollection = wbResp.Headers
            Dim myStream As Stream = wbResp.GetResponseStream()
            Dim myreader As New StreamReader(myStream)
            'textBox2.Text = ""
            StrStrim = myreader.ReadToEnd()
        Catch ex As Exception
            bool_IsTelNet = False
            dt.AcceptChanges()
            Return dt
            Exit Function
            'MsgBox(ex.ToString)
        End Try


        If StrStrim Is Nothing Or StrStrim = "" Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If
        

        StrStrim = StrStrim.Replace(" class=""ylwbg"" align=right", "")
        StrStrim = StrStrim.Replace("<nobr>", "")
        StrStrim = StrStrim.Replace("</nobr>", "")
        ' class="grybg" align="right" width=75
        StrStrim = StrStrim.Replace(" class=""grybg"" align=""right"" width=75", "")
        StrStrim = StrStrim.Replace("&nbsp;", "")
        StrStrim = StrStrim.Replace(" class=""nobg""  align=""right""", "")
        StrStrim = StrStrim.Replace(" class=""ylwbg"" align=""right""", "")
        StrStrim = StrStrim.Replace(" class=""nobg"" align=right", "")
        StrStrim = StrStrim.Replace(" class=""nobg"" align=""right""", "")
        StrStrim = StrStrim.Replace(" class=""ylwbg""  align=""right""", "")
        StrStrim = StrStrim.Replace("/live_market/dynaContent/live_watch/get_quote/GetQuoteCID.jsp?", "")
        StrStrim = StrStrim.Replace("/live_market/dynaContent/live_watch/fxTracker/optChainDataByStrikePrice.jsp?", "")
        StrStrim = StrStrim.Replace("</a>", "")
        StrStrim = StrStrim.Replace("<a", "")

        StrStrim = StrStrim.Replace("target=""_blank""", "")
        StrStrim = StrStrim.Replace(" class="""" align=""center""", "")
        StrStrim = StrStrim.Replace("<img src=""/live_market/resources/images/grficon.gif"" border=""0"">", "")
        'StrStrim = StrStrim.Replace("class=""", "")
        'StrStrim = StrStrim.Replace("align=""right""", "")



        'StrStrim = StrStrim.Replace(" class=tablehead", "")


        'StrStrim = StrStrim.Replace("</a>", "")
        'StrStrim = StrStrim.Replace("<a", "")


        Dim strdata1 As [String]()
        Dim str As String() = New String(0) {}
        str(0) = "<div"
        strdata1 = StrStrim.Split(str, StringSplitOptions.None)

        If strdata1.Length <= 1 Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If

        Dim strdata As [String]()
        str(0) = "<table"
        strdata = strdata1(16).Split(str, StringSplitOptions.None)

        '

        'for (int i = 0; Doc.all.length - 1 > i; i++)
        '{
        '    mshtml.IHTMLElement hElm = (mshtml.IHTMLElement)Doc.all.item(i, i);
        '    string hE = hElm.tagName.ToLower();
        '    if (hE == "body") //|| hE == "html" || hE == "head")
        '    {
        '        if (hE != "")
        '        {
        '            sb.Append(hElm.innerText + Environment.NewLine);
        '        }
        '    }
        '}


        'Dim client As New WebClient()
        ' string html = client.DownloadString(@"http://www.nseindia.com/marketinfo/fo/fomwatchsymbol.jsp?key=NIFTY");
        If strdata.Length <= 1 Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If

        Dim strdata2 As [String]()
        str(0) = "</table>"
        strdata2 = strdata(1).Split(str, StringSplitOptions.None)

        'Dim strdata3 As [String]()
        'str(0) = "<!--"
        'strdata3 = strdata2(0).Split(str, StringSplitOptions.None)

        'str(0) = "<!--"
        'Dim FinalStr As String = ""
        'For integer i = 1 to strdata3.Length -1
        '    FinalStr &= strdata3(i).Split(str, StringSplitOptions.None)
        'Next


        Dim dataSet As DataSet = HtmlTableParser.ParseDataSet("<table" & strdata2(0).Replace("</div>", "") & "</table>")
        dt.Merge(dataSet.Tables(0))

        Return dt
    End Function
End Class



''' <summary>
''' HtmlTableParser parses the contents of an html string into a System.Data DataSet or DataTable.
''' </summary>
Public Class HtmlTableParser
    Private Const ExpressionOptions As RegexOptions = RegexOptions.Singleline Or RegexOptions.Multiline Or RegexOptions.IgnoreCase

    Private Const CommentPattern As String = "<!--(.*?)-->"
    Private Const TablePattern As String = "<table[^>]*>(.*?)</table>"
    Private Const HeaderPattern As String = "<th[^>]*>(.*?)</th>"
    Private Const RowPattern As String = "<tr[^>]*>(.*?)</tr>"
    Private Const CellPattern As String = "<td[^>]*>(.*?)</td>"

    ''' <summary>
    ''' Given an HTML string containing n table tables, parse them into a DataSet containing n DataTables.
    ''' </summary>
    ''' <param name="html">An HTML string containing n HTML tables</param>
    ''' <returns>A DataSet containing a DataTable for each HTML table in the input HTML</returns>
    Public Shared Function ParseDataSet(ByVal html As String) As DataSet
        Dim dataSet As New DataSet()
        Dim tableMatches As MatchCollection = Regex.Matches(WithoutComments(html), TablePattern, ExpressionOptions)

        For Each tableMatch As Match In tableMatches
            dataSet.Tables.Add(ParseTable(tableMatch.Value))
        Next

        Return dataSet
    End Function

    ''' <summary>
    ''' Given an HTML string containing a single table, parse that table to form a DataTable.
    ''' </summary>
    ''' <param name="tableHtml">An HTML string containing a single HTML table</param>
    ''' <returns>A DataTable which matches the input HTML table</returns>
    Public Shared Function ParseTable(ByVal tableHtml As String) As DataTable
        Dim tableHtmlWithoutComments As String = WithoutComments(tableHtml)

        Dim dataTable As New DataTable()

        Dim rowMatches As MatchCollection = Regex.Matches(tableHtmlWithoutComments, RowPattern, ExpressionOptions)

        dataTable.Columns.AddRange(IIf(tableHtmlWithoutComments.Contains("<th"), ParseColumns(tableHtml), GenerateColumns(rowMatches)))

        ParseRows(rowMatches, dataTable)

        Return dataTable
    End Function

    ''' <summary>
    ''' Strip comments from an HTML stirng
    ''' </summary>
    ''' <param name="html">An HTML string potentially containing comments</param>
    ''' <returns>The input HTML string with comments removed</returns>
    Private Shared Function WithoutComments(ByVal html As String) As String
        Return Regex.Replace(html, CommentPattern, String.Empty, ExpressionOptions)
    End Function

    ''' <summary>
    ''' Add a row to the input DataTable for each row match in the input MatchCollection
    ''' </summary>
    ''' <param name="rowMatches">A collection of all the rows to add to the DataTable</param>
    ''' <param name="dataTable">The DataTable to which we add rows</param>
    Private Shared Sub ParseRows(ByVal rowMatches As MatchCollection, ByVal dataTable As DataTable)
        For Each rowMatch As Match In rowMatches
            ' if the row contains header tags don't use it - it is a header not a row
            If Not rowMatch.Value.Contains("<th") Then
                Dim dataRow As DataRow = dataTable.NewRow()

                Dim cellMatches As MatchCollection = Regex.Matches(rowMatch.Value, CellPattern, ExpressionOptions)

                For columnIndex As Integer = 0 To cellMatches.Count - 1
                    Dim str As String = cellMatches(columnIndex).Groups(1).ToString()
                    If columnIndex <= 4 Then

                        If str.Contains(">") = True Then
                            str = str.Split(">".ToCharArray())(1)
                        End If
                        If columnIndex = 4 Then
                            dataRow(columnIndex) = str.Replace("-", "0")
                        Else
                            dataRow(columnIndex) = str
                        End If

                    Else
                        'cellMatches[columnIndex].Groups[1].ToString();
                        'dataRow(columnIndex) = "0"
                        dataRow(columnIndex) = str.Replace("-", "0")
                    End If
                Next

                dataTable.Rows.Add(dataRow)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Given a string containing an HTML table, parse the header cells to create a set of DataColumns
    ''' which define the columns in a DataTable.
    ''' </summary>
    ''' <param name="tableHtml">An HTML string containing a single HTML table</param>
    ''' <returns>A set of DataColumns based on the HTML table header cells</returns>
    Private Shared Function ParseColumns(ByVal tableHtml As String) As DataColumn()
        Dim headerMatches As MatchCollection = Regex.Matches(tableHtml, HeaderPattern, ExpressionOptions)
        Dim dtcolumn As DataColumn() = New DataColumn(11) {}
        dtcolumn(0) = New DataColumn("Instrument", GetType(String))
        dtcolumn(1) = New DataColumn("Underlying", GetType(String))
        dtcolumn(2) = New DataColumn("ExpiryDate", GetType(String))
        dtcolumn(3) = New DataColumn("OptionType", GetType(String))
        dtcolumn(4) = New DataColumn("StrikePrice", GetType(Double))
        dtcolumn(5) = New DataColumn("HighPrice", GetType(Double))
        dtcolumn(6) = New DataColumn("LowPrice", GetType(Double))
        dtcolumn(7) = New DataColumn("PrevClose", GetType(Double))
        dtcolumn(8) = New DataColumn("LastPrice", GetType(Double))
        dtcolumn(9) = New DataColumn("traded", GetType(Double))
        dtcolumn(10) = New DataColumn("Turnover", GetType(Double))
        dtcolumn(11) = New DataColumn("UnderlyingValue", GetType(Double))
        Return dtcolumn
        'return (from Match headerMatch in headerMatches  
        '    select new DataColumn(headerMatch.Groups[1].ToString())).ToArray();
    End Function

    ''' <summary>
    ''' For tables which do not specify header cells we must generate DataColumns based on the number
    ''' of cells in a row (we assume all rows have the same number of cells).
    ''' </summary>
    ''' <param name="rowMatches">A collection of all the rows in the HTML table we wish to generate columns for</param>
    ''' <returns>A set of DataColumns based on the number of celss in the first row of the input HTML table</returns>
    Private Shared Function GenerateColumns(ByVal rowMatches As MatchCollection) As DataColumn()
        Dim columnCount As Integer = Regex.Matches(rowMatches(0).ToString(), CellPattern, ExpressionOptions).Count

        Dim dtcolumn As DataColumn() = New DataColumn(11) {}
        dtcolumn(0) = New DataColumn("Instrument", GetType(String))
        dtcolumn(1) = New DataColumn("Underlying", GetType(String))
        dtcolumn(2) = New DataColumn("ExpiryDate", GetType(String))
        dtcolumn(3) = New DataColumn("OptionType", GetType(String))
        dtcolumn(4) = New DataColumn("StrikePrice", GetType(Double))
        dtcolumn(5) = New DataColumn("HighPrice", GetType(Double))
        dtcolumn(6) = New DataColumn("LowPrice", GetType(Double))
        dtcolumn(7) = New DataColumn("PrevClose", GetType(Double))
        dtcolumn(8) = New DataColumn("LastPrice", GetType(Double))
        dtcolumn(9) = New DataColumn("traded", GetType(Double))
        dtcolumn(10) = New DataColumn("Turnover", GetType(Double))
        dtcolumn(11) = New DataColumn("UnderlyingValue", GetType(Double))

        Return dtcolumn
        'return (from index in Enumerable.Range(0, columnCount)
        '        select new DataColumn("Column " + Convert.ToString(index))).ToArray();

    End Function
End Class

