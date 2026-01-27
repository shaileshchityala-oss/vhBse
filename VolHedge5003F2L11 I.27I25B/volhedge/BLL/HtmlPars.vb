Imports System.IO
Imports System.Data
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Net
'Imports Newtonsoft.Json
'Imports System.Net.Cache


Imports System.Windows.Forms
Imports System.Threading

Class HtmlPars
    ' Private Shared completed As Boolean = False
    Private Shared wb As WebBrowser
    
    Public Shared Function GetFoIndexNetData(ByVal strIndex As String) As DataTable
        'textBox1.Multiline = True
        'textBox1.ScrollBars = ScrollBars.Both
        'above only for showing the sample
        Dim dt As New DataTable()
        Try



            Dim IndexName As String
            Dim IndexValue As Long

            Dim iClosingPrice As Long

            dt.Columns.Add("Index", GetType(String))
            dt.Columns.Add("Current", GetType(Double))
            dt.Columns.Add("Change", GetType(Double))
            dt.Columns.Add("Open", GetType(Double))
            dt.Columns.Add("High", GetType(Double))
            dt.Columns.Add("Low", GetType(Double))
            dt.Columns.Add("PrevClose", GetType(String))
            'dt.Columns.Add("Today", GetType(String))
            'dt.Columns.Add("52wHigh", GetType(Double))
            'dt.Columns.Add("52wLow", GetType(Double))

            Dim StrStrim As [String]

            StrStrim = ""

            StrStrim = ""

            StrStrim = strIndex



            'Dim str2 As String = strIndex
            'Dim strArr() As String
            'Dim count As Integer
            'str2 = "<TR>"
            'strArr = str2.Split("<TR")
            'Dim stresam As String = strArr(1)
            'For count = 0 To strArr.Length - 1
            '    MsgBox(strArr(count))
            'Next

            If StrStrim Is Nothing Or StrStrim = "" Then
                dt.AcceptChanges()
                Return dt
                Exit Function
            End If
            StrStrim = StrStrim.Replace(" class=banktitle colSpan=10>Broad Market Indices :</TD></TR>*<TR sizcache=""39"" sizset=""237"">", "")
            StrStrim = StrStrim.Replace(" style=""PADDING-BOTTOM: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px; VERTICAL-ALIGN: middle; PADDING-TOP: 1px"" sizcache=""39"" sizset=""237"">", "")
            'StrStrim = StrStrim.Replace(" style=""PADDING-BOTTOM: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px; VERTICAL-ALIGN: middle; PADDING-TOP: 1px"" class=status_map", "")
            ''   StrStrim = StrStrim.Replace("<A href=""https://nseindia.com/ChartApp/install/charts/mainpageall1.jsp?symbol=Segment=OI&amp;CDSymbol=NIFTY%20MID100%20FREE""", "")

            ''StrStrim = StrStrim.Replace("<A href=""https://nseindia.com/ChartApp/install/charts/mainpageall1.jsp?symbol=Segment=OI&amp;CDSymbol=NIFTY50%20TR%202X%20LEV""><IMG src=""https://nseindia.com/images/sparks/NIFTYTR2XLEV.png""></A>", "")
            'StrStrim = StrStrim.Replace(" style=""PADDING-BOTTOM: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px; VERTICAL-ALIGN: middle; PADDING-TOP: 1px"" class=""number green""", "")

            'StrStrim = StrStrim.Replace(" style=""PADDING-BOTTOM: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px; VERTICAL-ALIGN: middle; PADDING-TOP: 1px"" sizcache=""39"" sizset=""241""", "")
            '' StrStrim = StrStrim.Replace("<A href=""https://nseindia.com/products/content/equities/indices/nifty_500.htm"">", "")
            'StrStrim = StrStrim.Replace(" style=""PADDING-BOTTOM: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px; VERTICAL-ALIGN: middle; PADDING-TOP: 1px"" class=""number red""", "")
            'StrStrim = StrStrim.Replace(" style=""PADDING-BOTTOM: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px; VERTICAL-ALIGN: middle; PADDING-TOP: 1px"" sizcache=""39"" sizset=""254""", "")
            'StrStrim = StrStrim.Replace(" style=""PADDING-BOTTOM: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px; VERTICAL-ALIGN: middle; PADDING-TOP: 1px"" sizcache=""39"" sizset=""242""", "")

            StrStrim = StrStrim.Replace("<A href=""#""", "")
            StrStrim = StrStrim.Replace(" style=""PADDING-BOTTOM: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px; VERTICAL-ALIGN: middle; PADDING-TOP: 1px"" class=""number red"">", "")
            StrStrim = StrStrim.Replace("</TD>", "")

            StrStrim = StrStrim.Replace(" style=""PADDING-BOTTOM: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px; VERTICAL-ALIGN: middle; PADDING-TOP: 1px"" class=number>", "")
            StrStrim = StrStrim.Replace("</A>", "")
            StrStrim = StrStrim.Replace("<A href=""https://nseindia.com/live_market/dynaContent/live_watch/equities_stock_watch.htm?cat=N"">", "")


            StrStrim = StrStrim.Replace(" style=""VERTICAL-ALIGN: middle; PADDING-BOTTOM: 1px; PADDING-TOP: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px"" sizcache=""39"" sizset=""246""><A href=""https://nseindia.com/live_market/dynaContent/live_watch/equities_stock_watch.htm?cat=B"">", "")
            StrStrim = StrStrim.Replace(" style=""PADDING-BOTTOM: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px; VERTICAL-ALIGN: middle; PADDING-TOP: 1px"" class=""number green"">", "")

            StrStrim = StrStrim.Replace(" class=""number red"" style=""VERTICAL-ALIGN: middle; PADDING-BOTTOM: 1px; PADDING-TOP: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px"">", "")
            StrStrim = StrStrim.Replace(" style=""VERTICAL-ALIGN: middle; PADDING-BOTTOM: 1px; PADDING-TOP: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px"" sizcache=""39"" sizset=""237"">", "")
            StrStrim = StrStrim.Replace("style=""PADDING-BOTTOM: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px; VERTICAL-ALIGN: middle; PADDING-TOP: 1px"" sizcache=""39"" sizset=""246""><A href=""https://nseindia.com/live_market/dynaContent/live_watch/equities_stock_watch.htm?cat=B"">", "")
            StrStrim = StrStrim.Replace(" class=number style=""VERTICAL-ALIGN: middle; PADDING-BOTTOM: 1px; PADDING-TOP: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px"">", "")
            StrStrim = StrStrim.Replace(" class=""number green"" style=""VERTICAL-ALIGN: middle; PADDING-BOTTOM: 1px; PADDING-TOP: 1px; PADDING-LEFT: 4px; PADDING-RIGHT: 4px"">", "")
            'StrStrim = StrStrim.Replace("<A href=""https://nseindia.com/ChartApp/install/charts/mainpageall1.jsp?symbol=Segment=OI&amp;CDSymbol=NIFTY%2050""><IMG src=""https://nseindia.com/images/sparks/NIFTY.png"">", "")
            Dim strdata1 As [String]()
            Dim str As String() = New String(0) {}
            str(0) = "<TABLE"
            strdata1 = StrStrim.Split(str, StringSplitOptions.None)



            Dim strdata2 As [String]()
            Dim str2 As String() = New String(0) {}
            str2(0) = "<TD"
            strdata2 = StrStrim.Split(str2, StringSplitOptions.None)

            ' For Each Item As String In strdata2
            '  strdata = Item.Split(str, StringSplitOptions.None)
            Dim dr As DataRow

            If strdata2(2).Trim() = "NIFTY 50" Then
                dr = dt.NewRow()
                dr("Index") = "Nifty 50" 'strdata2(2).Trim()
                dr("Current") = strdata2(3).Trim()
                dr("Change") = strdata2(4).Trim()
                dr("Open") = strdata2(5).Trim()
                dr("High") = strdata2(6).Trim()
                dr("Low") = strdata2(7).Trim()
                dr("PrevClose") = strdata2(8).Trim()

                dt.Rows.Add(dr)
                dt.AcceptChanges()
            End If

            If strdata2(93).Trim() = "NIFTY BANK" Then
                dr = dt.NewRow()

                dr("Index") = "Nifty Bank" 'strdata2(93).Trim()
                dr("Current") = strdata2(94).Trim()
                dr("Change") = strdata2(95).Trim()
                dr("Open") = strdata2(96).Trim()
                dr("High") = strdata2(97).Trim()
                dr("Low") = strdata2(98).Trim()
                dr("PrevClose") = strdata2(99).Trim()

                dt.Rows.Add(dr)
                dt.AcceptChanges()
            End If


            For Each Dr1 As DataRow In dt.Rows
                IndexName = Dr1("Index").ToString.Trim()


                IndexValue = CLng(Dr1("Current"))
                iClosingPrice = CLng(Dr1("PrevClose"))



                If eIdxprice.Contains(IndexName.Trim()) Then
                    eIdxprice(IndexName.Trim()) = IndexValue '/ 100
                Else
                    eIdxprice.Add(IndexName.Trim(), IndexValue) '/ 100)
                End If
                '  Dr1.Close()
                If eIdxClosingprice.Contains(IndexName.Trim()) Then
                    eIdxClosingprice(IndexName.Trim()) = iClosingPrice ' / 100
                Else
                    eIdxClosingprice.Add(IndexName.Trim(), iClosingPrice) ' / 100)
                End If


            Next

            ' Next

            ''Sectoral Indices :
            'If strdata1.Length <= 1 Then
            '    dt.AcceptChanges()
            '    Return dt
            '    Exit Function
            'End If

            'Dim strdata As [String]()
            'str(0) = "</DIV>"
            'strdata = strdata1(1).Split(str, StringSplitOptions.None)



            'If strdata.Length <= 4 Then
            '    dt.AcceptChanges()
            '    Return dt
            '    Exit Function
            'End If
            'Try

            '    Dim dataSet As DataSet = HtmlTableParserIndex.ParseDataSet("<TABLE" & strsam & "</TABLE>")
            '    'Dim dataSet As DataSet = HtmlTableParserFO.ParseDataSet("<TABLE" & strdata(0) & "</TABLE>")
            '    dt.Merge(dataSet.Tables(0))
            'Catch ex As Exception

            'End Try

            'For Each drow As DataRow In dt.Rows
            '    If drow("ExpiryDate").ToString.Length > 0 Then
            '        drow("mdate") = DateSerial(Val(Microsoft.VisualBasic.Right(drow("ExpiryDate").ToString, 4)), Mnth(drow("ExpiryDate").ToString.Substring(2, 3)), Val(Microsoft.VisualBasic.Left(drow("ExpiryDate").ToString, 2)))
            '        Dim cp As String
            '        cp = Microsoft.VisualBasic.Left(drow("OptionType").ToString, 1)
            '        Dim script As String
            '        If cp = "-" Then
            '            script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
            '        Else
            '            script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice").ToString), "###0.00") & "  " & drow("OptionType").ToString
            '        End If
            '        drow("script") = script.Trim.ToUpper
            '        Try
            '            drow("Token") = Val(cpfmaster.Compute("max(token)", "script='" & script.Trim & "'").ToString)
            '            drow("eqToken") = Val(eqmaster.Compute("max(token)", "script='" & Symbol.Trim & "  EQ'").ToString)
            '        Catch ex As Exception
            '            REM By Viral
            '            'While Importing Contract cpfmaster or eqmaster Will Blank & Error Generate 
            '            drow("Token") = 0
            '            drow("eqToken") = 0
            '        End Try
            '    End If
            'Next
            '  dt.AcceptChanges()
            ' MsgBox(dt.Compute("max(LastPrice)", "Token=48223").ToString)


        Catch ex As Exception
            Return dt
        End Try
        Return dt

    End Function
    Public Shared Function Get_Index_InterNetData(ByVal Symbol As String) As DataTable
        'textBox1.Multiline = True
        'textBox1.ScrollBars = ScrollBars.Both
        'above only for showing the sample

        Dim dt As New DataTable()

        dt.Columns.Add("Index", GetType(String))
        dt.Columns.Add("Current", GetType(String))
        dt.Columns.Add("% Change", GetType(String))
        dt.Columns.Add("Open", GetType(String))
        dt.Columns.Add("High", GetType(Double))
        dt.Columns.Add("Low", GetType(Double))
        dt.Columns.Add("Prev. Close", GetType(Double))
        dt.Columns.Add("Today", GetType(Double))
        dt.Columns.Add("52w High", GetType(Double))
        dt.Columns.Add("52w Low", GetType(Double))


        Dim StrStrim As [String]

        StrStrim = ""
        Try

            'Dim Doc As mshtml.IHTMLDocument2 = New mshtml.HTMLDocumentClass()
            'HttpCacheAgeControl.None, TimeSpan.FromDays(1)
            'Dim requestPolicy As New HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore)


            'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fo/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
            Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("https://nseindia.com/live_market/dynaContent/live_watch/live_index_watch.htm"), HttpWebRequest)
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

        ''

        ''for (int i = 0; Doc.all.length - 1 > i; i++)
        ''{
        ''    mshtml.IHTMLElement hElm = (mshtml.IHTMLElement)Doc.all.item(i, i);
        ''    string hE = hElm.tagName.ToLower();
        ''    if (hE == "body") //|| hE == "html" || hE == "head")
        ''    {
        ''        if (hE != "")
        ''        {
        ''            sb.Append(hElm.innerText + Environment.NewLine);
        ''        }
        ''    }
        ''}


        'Dim strdata1 As [String]()
        'Dim str As String() = New String(0) {}
        'str(0) = "<TABLE"
        'strdata1 = StrStrim.Split(str, StringSplitOptions.None)

        If strdata1.Length <= 1 Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If

        '  Dim strdata As [String]()
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

        Dim dataSet As DataSet = HtmlTableParserFO.ParseDataSet("<TABLE" & strdata(0) & "</TABLE>")
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
        'Try
        '    Dim Doc As mshtml.IHTMLDocument2 = New mshtml.HTMLDocumentClass()

        '    Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fo/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
        '    'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
        '    'http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=NIFTY
        '    '("http://msdn.microsoft.com/");
        '    wbReq.ProtocolVersion = HttpVersion.Version11
        '    wbReq.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:9.0.1) Gecko/20100101 Firefox/9.0.1"
        '    'wbReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)"
        '    wbReq.Timeout = 100000
        '    wbReq.Accept = "*/*"
        '    wbReq.Headers.Add("Accept-Language: en-us,en;q=0.5")
        '    'wbReq.ContentType = "text/html";

        '    Dim wbResp As HttpWebResponse = DirectCast(wbReq.GetResponse(), HttpWebResponse)
        '    Dim wbHCol As WebHeaderCollection = wbResp.Headers
        '    Dim myStream As Stream = wbResp.GetResponseStream()
        '    Dim myreader As New StreamReader(myStream)
        '    'textBox2.Text = ""
        '    StrStrim = myreader.ReadToEnd()
        'Catch ex As Exception
        '    bool_IsTelNet = False
        '    dt.AcceptChanges()
        '    Return dt
        '    Exit Function
        '    'MsgBox(ex.ToString)
        'End Try


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

        Dim dataSet As DataSet = HtmlTableParserFO.ParseDataSet("<TABLE>" & strdata(5))
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
    Public Shared Function GetMCXCurInterNetData(ByVal Symbol As String) As DataTable
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
            Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.mcx-sx.com/webservice/MarketCurrency.asmx/GetPriceWatchData"), HttpWebRequest)
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
        StrStrim = StrStrim.Replace("},{", "~")
        StrStrim = StrStrim.Replace("{""d"":[{""", "")
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
            strdata = Item.Replace(",", "").Split(str, StringSplitOptions.None)
            If Symbol <> strdata(3).Split("""".ToCharArray(), StringSplitOptions.None)(1) Then
                Continue For
            End If
            Dim dr As DataRow
            dr = dt.NewRow()

            dr("Instrument") = "FUTCUR" 'strdata(1).Split(":".ToCharArray(), StringSplitOptions.None)(0)
            dr("Underlying") = 0
            Dim Expdate As String = strdata(2).Split(" ".ToCharArray(), StringSplitOptions.None)(1).Split("""".ToCharArray(), StringSplitOptions.None)(0)
            dr("ExpiryDate") = Expdate.Substring(0, 2) & MonthName(Expdate.Substring(2, 2)).ToUpper().Substring(0, 3) & "20" & Expdate.Substring(4, 2)
            dr("OptionType") = "F"
            dr("StrikePrice") = 0
            dr("HighPrice") = 0
            dr("LowPrice") = 0
            dr("PrevClose") = 0 ', GetType(Double))
            dr("LastPrice") = Val(strdata(9).Split("""".ToCharArray(), StringSplitOptions.None)(1)) ', GetType(Double))
            dr("traded") = Val(strdata(15).Split("""".ToCharArray(), StringSplitOptions.None)(1)) ', GetType(Double))
            dr("Turnover") = Val(strdata(14).Split("""".ToCharArray(), StringSplitOptions.None)(1)) ', GetType(Double))
            dr("UnderlyingValue") = 0 ', GetType(Double))
            dr("mdate") = CDate(dr("ExpiryDate")) ', GetType(Date))
            dr("script") = "" ', GetType(String))
            dr("Token") = 0 ', GetType(Long))
            dr("eqToken") = 0 ', GetType(Long))
            dr("OI") = Val(strdata(13).Split("""".ToCharArray(), StringSplitOptions.None)(1)) ', GetType(Double))

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

        'For Each drow As DataRow In ddt.Select("", "mdate")
        dt.Merge(GetMCXCurOptInterNetData(Symbol, ddt.Rows(0)("ExpiryDate")))
        dt.AcceptChanges()
        'Next



        For Each drow As DataRow In dt.Rows
            If drow("ExpiryDate").ToString.Length > 0 Then
                drow("mdate") = DateSerial(Val(Microsoft.VisualBasic.Right(drow("ExpiryDate").ToString, 4)), Mnth(drow("ExpiryDate").ToString.Substring(2, 3)), Val(Microsoft.VisualBasic.Left(drow("ExpiryDate").ToString, 2)))
                Dim cp As String
                cp = Microsoft.VisualBasic.Left(drow("OptionType").ToString, 1)
                Dim script As String
                If cp = "-" Or cp = "F" Then
                    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
                Else
                    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice").ToString), "###0.0000") & "  " & drow("OptionType").ToString
                End If
                drow("script") = script.Trim.ToUpper
                Try
                    drow("Token") = Val(Currencymaster.Compute("max(token)", "script='" & script.Trim & "'").ToString)
                    drow("eqToken") = 0 'Val(eqmaster.Compute("max(token)", "script='" & Symbol.Trim & "  EQ'").ToString)
                Catch ex As Exception
                    REM By Viral
                    'While Importing Contract cpfmaster or eqmaster Will Blank & Error Generate 
                    drow("Token") = 0
                    drow("eqToken") = 0
                End Try

                If OpenInterestprice.Contains(CLng(drow("Token"))) Then
                    OpenInterestprice.Item(CLng(drow("Token"))) = Val(drow("OI") & "")
                Else
                    OpenInterestprice.Add(CLng(drow("Token")), Val(drow("OI") & ""))
                End If

            End If
        Next
        dt.AcceptChanges()
        ' MsgBox(dt.Compute("max(LastPrice)", "Token=48223").ToString)
        Return dt
    End Function

    Public Shared Function GetMCXCurOptInterNetData(ByVal Symbol As String, ByVal Exp As String) As DataTable
        'textBox1.Multiline = True
        'textBox1.ScrollBars = ScrollBars.Both
        'above only for showing the sample

        Dim dt As New DataTable()

        dt.Columns.Add("Instrument", GetType(String)) 'ok
        dt.Columns.Add("Underlying", GetType(String))
        dt.Columns.Add("ExpiryDate", GetType(String))
        dt.Columns.Add("OptionType", GetType(String))
        dt.Columns.Add("StrikePrice", GetType(Double))
        dt.Columns.Add("HighPrice", GetType(Double))
        dt.Columns.Add("LowPrice", GetType(Double))
        dt.Columns.Add("PrevClose", GetType(Double))
        dt.Columns.Add("LastPrice", GetType(Double))
        dt.Columns.Add("traded", GetType(Double)) 'Volume
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
            Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.mcx-sx.com/markets/Currency/Live-Report/Pages/Option-Chain.aspx"), HttpWebRequest)
            ' & Symbol & "&instrument=OPTCUR&expiryDt=" & Exp
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


        StrStrim = StrStrim.Replace(Chr(13), "")
        StrStrim = StrStrim.Replace(Chr(10), "")
        StrStrim = StrStrim.Replace("<a class='nodecoration'>-</a>", "")
        'StrStrim = StrStrim.Replace(" class=""ylwbg"" align=right", "")
        'StrStrim = StrStrim.Replace("<nobr>", "")
        'StrStrim = StrStrim.Replace("</nobr>", "")
        '' class="grybg" align="right" width=75
        'StrStrim = StrStrim.Replace(" class=""grybg"" align=""right"" width=75", "")
        'StrStrim = StrStrim.Replace("&nbsp;", "")
        'StrStrim = StrStrim.Replace(" class=""nobg""  align=""right""", "")
        'StrStrim = StrStrim.Replace(" class=""ylwbg"" align=""right""", "")
        'StrStrim = StrStrim.Replace(" class=""nobg"" align=right", "")
        'StrStrim = StrStrim.Replace(" class=""nobg"" align=""right""", "")
        'StrStrim = StrStrim.Replace(" class=""ylwbg""  align=""right""", "")
        'StrStrim = StrStrim.Replace("/live_market/dynaContent/live_watch/get_quote/GetQuoteCID.jsp?", "")
        'StrStrim = StrStrim.Replace("/live_market/dynaContent/live_watch/fxTracker/optChainDataByStrikePrice.jsp?", "")
        'StrStrim = StrStrim.Replace("</a>", "")
        'StrStrim = StrStrim.Replace("<a", "")

        'StrStrim = StrStrim.Replace("target=""_blank""", "")
        'StrStrim = StrStrim.Replace(" class="""" align=""center""", "")
        'StrStrim = StrStrim.Replace("<img src=""/live_market/resources/images/grficon.gif"" border=""0"">", "")
        'StrStrim = StrStrim.Replace("class=""", "")
        'StrStrim = StrStrim.Replace("align=""right""", "")



        'StrStrim = StrStrim.Replace(" class=tablehead", "")


        'StrStrim = StrStrim.Replace("</a>", "")
        'StrStrim = StrStrim.Replace("<a", "")


        Dim strdata1 As [String]()
        Dim str As String() = New String(0) {}
        str(0) = "<table"
        strdata1 = StrStrim.Split(str, StringSplitOptions.None)
        '7
        If strdata1.Length <= 1 Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If

        'Dim strdata As [String]()
        'str(0) = "<table"
        'strdata = strdata1(16).Split(str, StringSplitOptions.None)

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
        If strdata1.Length <= 1 Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If

        Dim strdata2 As [String]()
        str(0) = "</table>"
        strdata2 = strdata1(9).Split(str, StringSplitOptions.None)

        'Dim strdata3 As [String]()
        'str(0) = "<!--"
        'strdata3 = strdata2(0).Split(str, StringSplitOptions.None)

        'str(0) = "<!--"
        'Dim FinalStr As String = ""
        'For integer i = 1 to strdata3.Length -1
        '    FinalStr &= strdata3(i).Split(str, StringSplitOptions.None)
        'Next


        Dim dataSet As DataSet = HtmlTableParserMCXCur.ParseDataSet("<table" & strdata2(0).Replace("</div>", "") & "</table>", Exp)
        dt.Merge(dataSet.Tables(0))

        Return dt
    End Function

    'Public Function ToDataTable(ByVal t As String) As DataTable
    '    'PropertyDescriptorCollection props =
    '    'TypeDescriptor.GetProperties(typeof(T));
    '    'DataTable table = new DataTable();
    '    'for(int i = 0 ; i < props.Count ; i++)
    '    '{
    '    'PropertyDescriptor prop = props[i];
    '    'table.Columns.Add(prop.Name, prop.PropertyType);
    '    '}
    '    'object[] values = new object[props.Count];
    '    'foreach (T item in data)
    '    '{
    '    'for (int i = 0; i < values.Length; i++)
    '    '{
    '    '    values[i] = props[i].GetValue(item);
    '    '}
    '    'table.Rows.Add(values);
    '    '}
    '    'return table;        


    '    Dim list As List(Of Dictionary(Of String, String)) = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, String)))(jsonText)
    '    Dim dTable As DataTable
    '    'dTable = (From p In listp).CopyToDataTable()

    'End Function
    Public Shared Function GetCInterNetData(ByVal Symbol As String) As DataTable
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
            dr("LastPrice") = Val(strdata(10).Replace(",", "")) ', GetType(Double))
            dr("traded") = Val(strdata(14).Replace(",", "")) ', GetType(Double))
            dr("Turnover") = Val(strdata(13).Replace(",", "")) ', GetType(Double))
            dr("UnderlyingValue") = 0 ', GetType(Double))
            dr("mdate") = CDate(strdata(3)) ', GetType(Date))
            dr("script") = "" ', GetType(String))
            dr("Token") = 0 ', GetType(Long))
            dr("eqToken") = 0 ', GetType(Long))
            dr("OI") = Val(strdata(12).Replace(",", ""))

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
                If cp = "-" Or cp = "F" Then
                    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
                Else
                    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice").ToString), "###0.0000") & "  " & drow("OptionType").ToString
                End If
                drow("script") = script.Trim.ToUpper
                Try
                    drow("Token") = Val(Currencymaster.Compute("max(token)", "script='" & script.Trim & "'").ToString)
                    drow("eqToken") = 0 'Val(eqmaster.Compute("max(token)", "script='" & Symbol.Trim & "  EQ'").ToString)
                Catch ex As Exception
                    REM By Viral
                    'While Importing Contract cpfmaster or eqmaster Will Blank & Error Generate 
                    drow("Token") = 0
                    drow("eqToken") = 0
                End Try


                If CLng(drow("Token")) <> 0 Then
                    If OpenInterestprice.Contains(CLng(drow("Token"))) Then
                        OpenInterestprice.Item(CLng(drow("Token"))) = Val(drow("OI"))
                    Else
                        OpenInterestprice.Add(CLng(drow("Token")), Val(drow("OI")))
                    End If
                End If

            End If
        Next
        dt.AcceptChanges()
        ' MsgBox(dt.Compute("max(LastPrice)", "Token=48223").ToString)
        Return dt
    End Function
    Public Shared Function GetFoInterNetDataLINK2_Future(ByVal Symbol As String) As DataTable
        'textBox1.Multiline = True
        'textBox1.ScrollBars = ScrollBars.Both
        'above only for showing the sample
        'Dim isCurr As Boolean = False
        'Dim strCompany As String = ""
        ''   strCompany = CType(Symbol, String).Split(",")(1)
        'strCompany = GetSymbol(strCompany.ToString())
        'If strCompany.ToUpper.Contains("USDINR") Or strCompany.ToUpper.Contains("JPYINR") Or strCompany.ToUpper.Contains("GBPINR") Or strCompany.ToUpper.Contains("EURINR") Then
        '    isCurr = True
        'End If
        'If isCurr = True Then
        '    GetInternetData(Symbol)
        'End If
        Dim dt As New DataTable()

        dt.Columns.Add("Instrument", GetType(String))
        dt.Columns.Add("Underlying", GetType(String))
        dt.Columns.Add("ExpiryDate", GetType(String))
        dt.Columns.Add("OptionType", GetType(String))
        dt.Columns.Add("StrikePrice", GetType(Double))
        dt.Columns.Add("OpenPrice", GetType(Double))
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
            'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest) comment by payal
            Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("https://www.nseindia.com/live_market/dynaContent/live_watch/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString() & "&Fut_Opt=Futures"), HttpWebRequest)
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
            WriteLog("Error In GetInternetdata" & vbCrLf & ex.ToString)
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

        StrStrim = StrStrim.Replace(" class=t1", "")
        StrStrim = StrStrim.Replace(" class=t2", "")
        StrStrim = StrStrim.Replace(" class=t3", "")
        StrStrim = StrStrim.Replace(" class=t4", "")
        StrStrim = StrStrim.Replace(" class=t5", "")
        StrStrim = StrStrim.Replace(" class=""normalText""", "")
        StrStrim = StrStrim.Replace(" class=""date""", "")
        StrStrim = StrStrim.Replace(" class=""number""", "")
        StrStrim = StrStrim.Replace("<br/>", "")

        'StrStrim = StrStrim.Replace("style="text-align:center", "")

        StrStrim = StrStrim.Replace("/live_market/dynaContent/live_watch/get_quote/GetQuoteFO.jsp?", "")
        StrStrim = StrStrim.Replace("</a>", "")
        StrStrim = StrStrim.Replace("<a", "")
        StrStrim = StrStrim.Replace("class=""alt""", "")

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
        strdata = strdata1(2).Split(str, StringSplitOptions.None)


        Dim strdatarefresh As [String]()

        strdatarefresh = strdata1(1).Split(">")
        strdatarefresh = strdatarefresh(8).Split("<")
        Dim strrefresh As String = strdatarefresh(0).Trim()

        If strdata.Length <= 4 Then
            dt.AcceptChanges()
            Return dt
            Exit Function
        End If
        Try


            Dim dataSet As DataSet = HtmlTableParserFOFUTURE.ParseDataSet("<TABLE" & strdata(0) & "</TABLE>")
            dt.Merge(dataSet.Tables(0))
        Catch ex As Exception

        End Try
        For Each drow As DataRow In dt.Rows
            If drow("ExpiryDate").ToString.Length > 0 Then
                Try


                    drow("mdate") = DateSerial(Val(Microsoft.VisualBasic.Right(drow("ExpiryDate").ToString, 4)), Mnth(drow("ExpiryDate").ToString.Substring(2, 3)), Val(Microsoft.VisualBasic.Left(drow("ExpiryDate").ToString, 2)))
                    Dim cp As String
                    cp = Microsoft.VisualBasic.Left(drow("OptionType").ToString, 1)
                    Dim script As String
                    If cpfmaster.Compute("max(InstrumentName)", "Symbol='" & Symbol.Trim & "' and option_type='XX'").ToString().Length > 0 Then
                        Dim Instrument As String = cpfmaster.Compute("max(InstrumentName)", "Symbol='" & Symbol.Trim & "' and option_type='XX'").ToString
                        If cp = "-" Then
                            script = Instrument.ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
                            If dt.Rows.Count > 0 Then
                                Try
                                    internerRefreshtime = dt.Rows(0)("Instrument")

                                    '  MDI.Refresh_intermet_time()
                                Catch ex As Exception

                                End Try
                            End If
                        Else
                            script = Instrument.ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice").ToString), "###0.00") & "  " & drow("OptionType").ToString
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
                    Else
                        Continue For
                    End If


                Catch ex As Exception
                    drow("Token") = 0
                    drow("eqToken") = 0
                End Try
            End If
        Next
        If dt.Rows.Count > 0 Then
            Try
                internerRefreshtime = strrefresh
            Catch ex As Exception
                internerRefreshtime = ""
            End Try

        End If

        dt.AcceptChanges()

        Dim token_no As Long
        Dim buy_price As Double
        Dim sale_price As Double
        Dim last_trade_price As Double

        Dim eqtoken_no As Long
        Dim eqbuy_price As Double
        Dim eqsale_price As Double
        Dim eqlast_trade_price As Double

        Dim VolumeTradedToday As Double
        Dim ClosingPrice As Double

        Dim dttime As Date
        dttime = Now
        For Each row As DataRow In dt.Rows
            token_no = Val(row("Token") & "")
            buy_price = 0
            sale_price = 0
            last_trade_price = Val(row("LastPrice") & "")
            'last_trade_price = Val(row("UnderlyingValue") & "")

            If last_trade_price.ToString() = 0 Then
                Continue For
            End If
            eqtoken_no = Val(row("eqToken") & "")
            eqbuy_price = 0
            eqsale_price = 0
            eqlast_trade_price = Val(row("UnderlyingValue") & "")
            VolumeTradedToday = Val(row("traded") & "")
            ClosingPrice = Val(row("PrevClose") & "")

            VarFoBCurrentDate = DateDiff(DateInterval.Second, CDate("1-1-1980"), Now)


            If futall.Contains(token_no) Then
                'Dim fltppr As Double
                If fltpprice.Contains(token_no) Then

                    If FlgBcastStop = False Then


                        fbuyprice.Item(token_no) = buy_price
                        fsaleprice.Item(token_no) = sale_price

                        fltpprice.Item(token_no) = last_trade_price
                    End If
                Else
                    fbuyprice.Add(token_no, buy_price)
                    fsaleprice.Add(token_no, sale_price)
                    fltpprice.Add(token_no, last_trade_price)
                End If

                If closeprice.Contains(token_no) Then
                    If FlgBcastStop = False Then


                        closeprice.Item(token_no) = ClosingPrice
                    End If
                Else
                    closeprice.Add(token_no, ClosingPrice)
                End If
            Else
                If ltpprice.Contains(token_no) Then
                    If FlgBcastStop = False Then

                        fltpprice.Item(token_no) = last_trade_price
                        buyprice.Item(token_no) = buy_price
                        saleprice.Item(token_no) = sale_price
                        ltpprice.Item(token_no) = last_trade_price
                        MKTltpprice(token_no) = last_trade_price
                    End If
                Else
                    buyprice.Add(token_no, buy_price)
                    saleprice.Add(token_no, sale_price)
                    ltpprice.Add(token_no, last_trade_price)
                    MKTltpprice.Add(token_no, last_trade_price)
                    fltpprice.Add(token_no, last_trade_price)
                End If

                If volumeprice.Contains(token_no) Then
                    If FlgBcastStop = False Then


                        volumeprice.Item(token_no) = VolumeTradedToday
                    End If
                Else
                    volumeprice.Add(token_no, VolumeTradedToday)
                End If

                If closeprice.Contains(token_no) Then
                    If FlgBcastStop = False Then


                        closeprice.Item(token_no) = ClosingPrice
                    End If
                Else
                    closeprice.Add(token_no, ClosingPrice)
                End If

            End If

            REM Equity
            If eqfutall.Contains(eqtoken_no) Then
                If eltpprice.Contains(eqtoken_no) Then
                    If FlgBcastStop = False Then


                        ebuyprice.Item(eqtoken_no) = eqbuy_price
                        esaleprice.Item(eqtoken_no) = eqsale_price
                        eltpprice.Item(eqtoken_no) = eqlast_trade_price
                    End If
                Else
                    ebuyprice.Add(eqtoken_no, eqbuy_price)
                    esaleprice.Add(eqtoken_no, eqsale_price)
                    eltpprice.Add(eqtoken_no, eqlast_trade_price)
                End If
                'If eqarray.Contains(token_no) Then
                '    cal_eq(token_no)
                'End If
                'process cm data
            End If
        Next
        '  End If

        ' MsgBox(dt.Compute("max(LastPrice)", "Token=48223").ToString)
        ' Return dt
    End Function
    Public Shared Function GetFoInterNetDataL2(ByVal Symbol As String, ByVal exp As String, ByVal instrumentName As String) As DataTable
        'textBox1.Multiline = True
        'textBox1.ScrollBars = ScrollBars.Both
        'above only for showing the sample
        Try



            Dim dt As New DataTable()

            dt.Columns.Add("CE_Chart", GetType(String))
            dt.Columns.Add("CE_OI", GetType(String))
            dt.Columns.Add("CE_Chg_OI", GetType(String))
            dt.Columns.Add("CE_Volume", GetType(String))
            dt.Columns.Add("CE_IV", GetType(String))
            dt.Columns.Add("CE_LTP", GetType(String))
            dt.Columns.Add("CE_NetChange", GetType(String))
            dt.Columns.Add("CE_BidQty", GetType(String))
            dt.Columns.Add("CE_BidPrice", GetType(String))
            dt.Columns.Add("CE_AskPrice", GetType(String))
            dt.Columns.Add("CE_AskQty", GetType(String))
            dt.Columns.Add("Strike", GetType(String))
            dt.Columns.Add("PE_BidQty", GetType(String))
            dt.Columns.Add("PE_BidPrice", GetType(String))
            dt.Columns.Add("PE_AskPrice", GetType(String))
            dt.Columns.Add("PE_AskQty", GetType(String))
            dt.Columns.Add("PE_NetChange", GetType(String))
            dt.Columns.Add("PE_LTP", GetType(String))
            dt.Columns.Add("PE_IV", GetType(String))
            dt.Columns.Add("PE_Volume", GetType(String))
            dt.Columns.Add("PE_Chg_OI", GetType(String))
            dt.Columns.Add("PE_OI", GetType(String))
            dt.Columns.Add("PE_Chart", GetType(String))

            dt.Columns.Add("CE_script", GetType(String))
            dt.Columns.Add("PE_script", GetType(String))

            dt.Columns.Add("CE_Token", GetType(Long))
            dt.Columns.Add("PE_Token", GetType(Long))

            dt.Columns.Add("eqToken", GetType(Long))

            'Dim exp As String
            'Dim dtexp As DataTable = New DataView(cpfmaster, "symbol='" & GetSymbol(Symbol) & "' and option_type<>'XX'", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "instrumentName")
            '     For Each dr As DataRow In dtexp.Rows

            dt.Rows.Clear()
            ' exp = CDate(dr("expdate1")).ToString("ddMMMyyyy").ToUpper() '"28SEP2017"





            Dim StrStrim As [String]

            StrStrim = ""
            Try

                'Dim Doc As mshtml.IHTMLDocument2 = New mshtml.HTMLDocumentClass()
                'HttpCacheAgeControl.None, TimeSpan.FromDays(1)
                'Dim requestPolicy As New HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore)


                'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fo/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
                'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
                'http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=NIFTY
                '("http://msdn.microsoft.com/");
                Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("https://www.nseindia.com/live_market/dynaContent/live_watch/option_chain/optionKeys.jsp?symbol=" & GetSymbol(Symbol) & "&date=" & exp), HttpWebRequest)
                'https://www.nseindia.com/live_market/dynaContent/live_watch/option_chain/optionKeys.jsp?symbol=NIFTY
                'https://www.nseindia.com/live_market/dynaContent/live_watch/option_chain/optionKeys.jsp?symbol=NIFTY&date=26OCT2017
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
            StrStrim = StrStrim.Replace(" class=""ylwbg""", "")
            StrStrim = StrStrim.Replace(" class=""grybg""", "")
            StrStrim = StrStrim.Replace("href=""/live_market/dynaContent/live_watch/option_chain/optionDates.jsp?symbol=", "")
            StrStrim = StrStrim.Replace("" & Symbol & "&instrument=" & instrumentName & "&strike=", "")
            StrStrim = StrStrim.Replace("href=""/live_market/dynaContent/live_watch/option_chain/optionDates.jsp?symbol=NIFTY&instrument=OPTIDX&strike=", "")

            ' href="/live_market/dynaContent/live_watch/option_chain/optionDates.jsp?symbol=NIFTY&instrument=OPTIDX&strike=5500.00"><b>5500.00</b>

            'StrStrim = StrStrim.Replace(" class=tablehead", "")


            'StrStrim = StrStrim.Replace("href=""/live_market/dynaContent/live_watch/option_chain/optionDates.jsp?symbol=NIFTY&instrument=OPTIDX&strike=5500.00""><b>", "")
            StrStrim = StrStrim.Replace("</b>", "")
            StrStrim = StrStrim.Replace("<b>", "")


            Dim strdata1 As [String]()
            Dim strdataUL As [String]()
            Dim underlying As String
            Dim str As String() = New String(0) {}
            str(0) = "<div"
            strdata1 = StrStrim.Split(str, StringSplitOptions.None)
            strdataUL = strdata1(11).Split(">")

            underlying = strdataUL(3).Split(" ")(1).Split("<")(0).ToString()

            If StrStrim Is Nothing Or StrStrim = "" Then
                dt.AcceptChanges()
                Return dt
                Exit Function
            End If
            If strdata1.Length <= 1 Then
                dt.AcceptChanges()
                Return dt
                Exit Function
            End If

            Dim strdata As [String]()
            str(0) = "<table"
            strdata = strdata1(16).Split(str, StringSplitOptions.None)
            If strdata.Length <= 1 Then
                dt.AcceptChanges()
                Return dt
                Exit Function
            End If

            Dim strdata2 As [String]()
            str(0) = "</table>"
            strdata2 = strdata(1).Split(str, StringSplitOptions.None)

            Dim dataSet As DataSet = HtmlTableParserFOL2.ParseDataSet("<table" & strdata2(0).Replace("</div>", "") & "</table>")
            dt.Merge(dataSet.Tables(0))

            If dt.Rows.Count = 0 Then
                GetInternetData("-," & Symbol)
            Else
                For Each drow As DataRow In dt.Rows
                    Dim cetoken As Long
                    Dim petoken As Long
                    ' Dim strike As Double = Val(drow("Strike").ToString())

                    ' val(dt.Rows(1)("Strike").ToString())
                    Dim scriptce, scriptpe As String


                    Dim script As String
                    'If cp = "-" Or cp = "F" Then
                    '    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
                    'Else
                    '    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice").ToString), "###0.0000") & "  " & drow("OptionType").ToString
                    'End If
                    'If cp = "-" Or cp = "F" Then
                    '    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
                    'Else
                    'Dim instrument As String
                    'If Symbol = "NIFTY" Or Symbol = "BANKNIFTY" Then
                    '    instrument = "OPTIDX"
                    'Else
                    '    instrument = "OPTSTK"
                    'End If
                    If Val(drow("Strike").ToString) = 0 Then
                        Continue For
                    End If
                    scriptce = instrumentName & "  " & GetSymbol(Symbol) & "  " & Format(CDate(exp), "ddMMMyyyy") & "  " & Format(Val(drow("Strike").ToString), "###0.00") & "  " & "CE"
                    scriptpe = instrumentName & "  " & GetSymbol(Symbol) & "  " & Format(CDate(exp), "ddMMMyyyy") & "  " & Format(Val(drow("Strike").ToString), "###0.00") & "  " & "PE"

                    'End If
                    'drow("script") = script.Trim.ToUpper
                    Try
                        cetoken = Val(cpfmaster.Compute("max(token)", "script='" & scriptce.Trim & "'").ToString)
                        petoken = Val(cpfmaster.Compute("max(token)", "script='" & scriptpe.Trim & "'").ToString)
                        '  drow("eqToken") = 0 'Val(eqmaster.Compute("max(token)", "script='" & Symbol.Trim & "  EQ'").ToString)
                    Catch ex As Exception
                        REM By Viral
                        'While Importing Contract cpfmaster or eqmaster Will Blank & Error Generate 
                        cetoken = 0
                        petoken = 0
                    End Try
                    If drow("CE_OI") = "-" Then
                        drow("CE_OI") = "0"
                    End If
                    If drow("PE_OI") = "-" Then
                        drow("PE_OI") = "0"
                    End If

                    If CLng(petoken) <> 0 Then
                        If OpenInterestprice.Contains(CLng(petoken)) Then
                            OpenInterestprice.Item(CLng(petoken)) = Val(Convert.ToDouble(drow("PE_OI")))
                        Else
                            OpenInterestprice.Add(CLng(petoken), Val(Convert.ToDouble(drow("PE_OI"))))
                        End If
                    End If
                    If CLng(cetoken) <> 0 Then
                        If OpenInterestprice.Contains(CLng(cetoken)) Then
                            OpenInterestprice.Item(CLng(cetoken)) = Val(Convert.ToDouble(drow("CE_OI")))
                        Else
                            OpenInterestprice.Add(CLng(cetoken), Val(Convert.ToDouble(drow("CE_OI"))))
                        End If
                    End If

                    Dim token_noCE As Long
                    Dim token_noPE As Long

                    Dim buy_price As Double
                    Dim sale_price As Double
                    Dim last_trade_priceCE As Double
                    Dim last_trade_pricePE As Double

                    Dim eqtoken_no As Long
                    Dim eqbuy_price As Double
                    Dim eqsale_price As Double
                    Dim eqlast_trade_price As Double

                    Dim VolumeTradedTodayCE As Double
                    Dim ClosingPriceCE As Double

                    Dim VolumeTradedTodayPE As Double
                    Dim ClosingPricePE As Double

                    Dim dttime As Date
                    Try
                        ' drow("Token") = Val(cpfmaster.Compute("max(token)", "script='" & script.Trim & "'").ToString)
                        eqtoken_no = Val(eqmaster.Compute("max(token)", "script='" & GetSymbol(Symbol) & "  EQ'").ToString)
                    Catch ex As Exception
                        REM By Viral
                        'While Importing Contract cpfmaster or eqmaster Will Blank & Error Generate 

                        eqtoken_no = 0
                    End Try

                    dttime = Now
                    token_noCE = Val(cetoken & "")
                    token_noPE = Val(petoken & "")
                    buy_price = 0
                    sale_price = 0
                    last_trade_priceCE = Val(drow("CE_LTP") & "")
                    last_trade_pricePE = Val(drow("PE_LTP") & "")
                    
                    eqtoken_no = Val(eqtoken_no & "")
                    eqbuy_price = 0
                    eqsale_price = 0
                    eqlast_trade_price = Val(underlying & "")
                    VolumeTradedTodayCE = Val(drow("CE_VOLUME") & "")
                    ClosingPriceCE = (Val(drow("CE_NetChange") & "") * -1) + last_trade_priceCE

                    VarFoBCurrentDate = DateDiff(DateInterval.Second, CDate("1-1-1980"), Now)
                    'token_noCE = 3115190
                    'If ltpprice.Contains(token_noCE) Then
                    '    last_trade_priceCE = ltpprice.Item(token_noCE)
                    'End If
                    VolumeTradedTodayPE = Val(drow("PE_VOLUME") & "")
                    ClosingPricePE = (Val(drow("PE_NetChange") & "") * -1) + last_trade_pricePE

                    If futall.Contains(token_noCE) Then
                        'Dim fltppr As Double
                        If fltpprice.Contains(token_noCE) Then

                            If FlgBcastStop = False Then


                                fbuyprice.Item(token_noCE) = buy_price
                                fsaleprice.Item(token_noCE) = sale_price

                                fltpprice.Item(token_noCE) = last_trade_priceCE
                            End If
                        Else
                            fbuyprice.Add(token_noCE, buy_price)
                            fsaleprice.Add(token_noCE, sale_price)
                            fltpprice.Add(token_noCE, last_trade_priceCE)
                        End If

                        If closeprice.Contains(token_noCE) Then
                            If FlgBcastStop = False Then


                                closeprice.Item(token_noCE) = ClosingPriceCE
                            End If
                        Else
                            closeprice.Add(token_noCE, ClosingPriceCE)
                        End If
                    Else
                        If ltpprice.Contains(token_noCE) Then
                            If FlgBcastStop = False Then

                                fltpprice.Item(token_noCE) = last_trade_priceCE
                                buyprice.Item(token_noCE) = buy_price
                                saleprice.Item(token_noCE) = sale_price
                                ltpprice.Item(token_noCE) = last_trade_priceCE
                                MKTltpprice(token_noCE) = last_trade_priceCE
                            End If
                        Else
                            buyprice.Add(token_noCE, buy_price)
                            saleprice.Add(token_noCE, sale_price)
                            ltpprice.Add(token_noCE, last_trade_priceCE)
                            MKTltpprice.Add(token_noCE, last_trade_priceCE)
                            fltpprice.Add(token_noCE, last_trade_priceCE)
                        End If

                        If volumeprice.Contains(token_noCE) Then
                            If FlgBcastStop = False Then


                                volumeprice.Item(token_noCE) = VolumeTradedTodayCE
                            End If
                        Else
                            volumeprice.Add(token_noCE, VolumeTradedTodayCE)
                        End If

                        If closeprice.Contains(token_noCE) Then
                            If FlgBcastStop = False Then


                                closeprice.Item(token_noCE) = ClosingPriceCE
                            End If
                        Else
                            closeprice.Add(token_noCE, ClosingPriceCE)
                        End If

                    End If

                    REM Equity
                    If eqfutall.Contains(eqtoken_no) Then
                        If eltpprice.Contains(eqtoken_no) Then
                            If FlgBcastStop = False Then


                                ebuyprice.Item(eqtoken_no) = eqbuy_price
                                esaleprice.Item(eqtoken_no) = eqsale_price
                                eltpprice.Item(eqtoken_no) = eqlast_trade_price
                            End If
                        Else
                            ebuyprice.Add(eqtoken_no, eqbuy_price)
                            esaleprice.Add(eqtoken_no, eqsale_price)
                            eltpprice.Add(eqtoken_no, eqlast_trade_price)
                        End If
                        'If eqarray.Contains(token_no) Then
                        '    cal_eq(token_no)
                        'End If
                        'process cm data
                    End If
                    If futall.Contains(token_noPE) Then
                        'Dim fltppr As Double
                        If fltpprice.Contains(token_noPE) Then

                            If FlgBcastStop = False Then


                                fbuyprice.Item(token_noPE) = buy_price
                                fsaleprice.Item(token_noPE) = sale_price

                                fltpprice.Item(token_noPE) = last_trade_pricePE
                            End If
                        Else
                            fbuyprice.Add(token_noPE, buy_price)
                            fsaleprice.Add(token_noPE, sale_price)
                            fltpprice.Add(token_noPE, last_trade_pricePE)
                        End If

                        If closeprice.Contains(token_noPE) Then
                            If FlgBcastStop = False Then


                                closeprice.Item(token_noPE) = ClosingPricePE
                            End If
                        Else
                            closeprice.Add(token_noPE, ClosingPricePE)
                        End If
                    Else
                        If ltpprice.Contains(token_noPE) Then
                            If FlgBcastStop = False Then

                                fltpprice.Item(token_noPE) = last_trade_pricePE
                                buyprice.Item(token_noPE) = buy_price
                                saleprice.Item(token_noPE) = sale_price
                                ltpprice.Item(token_noPE) = last_trade_pricePE
                                MKTltpprice(token_noPE) = last_trade_pricePE
                            End If
                        Else
                            buyprice.Add(token_noPE, buy_price)
                            saleprice.Add(token_noPE, sale_price)
                            ltpprice.Add(token_noPE, last_trade_pricePE)
                            MKTltpprice.Add(token_noPE, last_trade_pricePE)
                            fltpprice.Add(token_noPE, last_trade_pricePE)
                        End If

                        If volumeprice.Contains(token_noPE) Then
                            If FlgBcastStop = False Then


                                volumeprice.Item(token_noPE) = VolumeTradedTodayPE
                            End If
                        Else
                            volumeprice.Add(token_noPE, VolumeTradedTodayPE)
                        End If

                        If closeprice.Contains(token_noPE) Then
                            If FlgBcastStop = False Then


                                closeprice.Item(token_noPE) = ClosingPricePE
                            End If
                        Else
                            closeprice.Add(token_noPE, ClosingPricePE)
                        End If

                    End If
                Next
            End If


            
            'For Each drow As DataRow In dt.Rows
            '    If CLng(drow("CE_Token")) <> 0 Then
            '        If OpenInterestprice.Contains(CLng(drow("CE_Token"))) Then
            '            OpenInterestprice.Item(CLng(drow("CE_Token"))) = Val(drow("CE_OI"))
            '        Else
            '            OpenInterestprice.Add(CLng(drow("CE_Token")), Val(drow("CE_OI")))
            '        End If
            '    End If
            '    If CLng(drow("PE_Token")) <> 0 Then
            '        If OpenInterestprice.Contains(CLng(drow("PE_Token"))) Then
            '            OpenInterestprice.Item(CLng(drow("PE_Token"))) = Val(drow("PE_OI"))
            '        Else
            '            OpenInterestprice.Add(CLng(drow("PE_Token")), Val(drow("PE_OI")))
            '        End If
            '    End If
            'Next
            '    If drow("ExpiryDate").ToString.Length > 0 Then
            '        drow("mdate") = DateSerial(Val(Microsoft.VisualBasic.Right(drow("ExpiryDate").ToString, 4)), Mnth(drow("ExpiryDate").ToString.Substring(2, 3)), Val(Microsoft.VisualBasic.Left(drow("ExpiryDate").ToString, 2)))
            '        Dim cp As String
            '        cp = Microsoft.VisualBasic.Left(drow("OptionType").ToString, 1)
            '        Dim script As String
            '        If cp = "-" Then
            '            script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
            '        Else
            '            script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice").ToString), "###0.00") & "  " & drow("OptionType").ToString
            '        End If
            '        drow("script") = script.Trim.ToUpper
            '        Try
            '            drow("Token") = Val(cpfmaster.Compute("max(token)", "script='" & script.Trim & "'").ToString)
            '            drow("eqToken") = Val(eqmaster.Compute("max(token)", "script='" & Symbol.Trim & "  EQ'").ToString)
            '        Catch ex As Exception
            '            REM By Viral
            '            'While Importing Contract cpfmaster or eqmaster Will Blank & Error Generate 
            '            drow("Token") = 0
            '            drow("eqToken") = 0
            '        End Try
            '    End If
            'Next


            dt.AcceptChanges()
            ' Next
            ' MsgBox(dt.Compute("max(LastPrice)", "Token=48223").ToString)
            '  Return dt
        Catch ex As Exception

        End Try
    End Function
    Public Shared Function GetFoInterNetDataL2_backup(ByVal Symbol As String) As DataTable
        'textBox1.Multiline = True
        'textBox1.ScrollBars = ScrollBars.Both
        'above only for showing the sample
        Try



            Dim dt As New DataTable()

            dt.Columns.Add("CE_Chart", GetType(String))
            dt.Columns.Add("CE_OI", GetType(String))
            dt.Columns.Add("CE_Chg_OI", GetType(String))
            dt.Columns.Add("CE_Volume", GetType(String))
            dt.Columns.Add("CE_IV", GetType(String))
            dt.Columns.Add("CE_LTP", GetType(String))
            dt.Columns.Add("CE_NetChange", GetType(String))
            dt.Columns.Add("CE_BidQty", GetType(String))
            dt.Columns.Add("CE_BidPrice", GetType(String))
            dt.Columns.Add("CE_AskPrice", GetType(String))
            dt.Columns.Add("CE_AskQty", GetType(String))
            dt.Columns.Add("Strike", GetType(String))
            dt.Columns.Add("PE_BidQty", GetType(String))
            dt.Columns.Add("PE_BidPrice", GetType(String))
            dt.Columns.Add("PE_AskPrice", GetType(String))
            dt.Columns.Add("PE_AskQty", GetType(String))
            dt.Columns.Add("PE_NetChange", GetType(String))
            dt.Columns.Add("PE_LTP", GetType(String))
            dt.Columns.Add("PE_IV", GetType(String))
            dt.Columns.Add("PE_Volume", GetType(String))
            dt.Columns.Add("PE_Chg_OI", GetType(String))
            dt.Columns.Add("PE_OI", GetType(String))
            dt.Columns.Add("PE_Chart", GetType(String))

            dt.Columns.Add("CE_script", GetType(String))
            dt.Columns.Add("PE_script", GetType(String))

            dt.Columns.Add("CE_Token", GetType(Long))
            dt.Columns.Add("PE_Token", GetType(Long))

            dt.Columns.Add("eqToken", GetType(Long))

            Dim exp As String
            Dim dtexp As DataTable = New DataView(cpfmaster, "symbol='" & GetSymbol(Symbol) & "' and option_type<>'XX'", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "instrumentName")
            For Each dr As DataRow In dtexp.Rows

                dt.Rows.Clear()
                exp = CDate(dr("expdate1")).ToString("ddMMMyyyy").ToUpper() '"28SEP2017"





                Dim StrStrim As [String]

                StrStrim = ""
                Try

                    'Dim Doc As mshtml.IHTMLDocument2 = New mshtml.HTMLDocumentClass()
                    'HttpCacheAgeControl.None, TimeSpan.FromDays(1)
                    'Dim requestPolicy As New HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore)


                    'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fo/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
                    'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
                    'http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=NIFTY
                    '("http://msdn.microsoft.com/");
                    Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("https://www.nseindia.com/live_market/dynaContent/live_watch/option_chain/optionKeys.jsp?symbol=" & GetSymbol(Symbol) & "&date=" & exp), HttpWebRequest)
                    'https://www.nseindia.com/live_market/dynaContent/live_watch/option_chain/optionKeys.jsp?symbol=NIFTY
                    'https://www.nseindia.com/live_market/dynaContent/live_watch/option_chain/optionKeys.jsp?symbol=NIFTY&date=26OCT2017
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
                StrStrim = StrStrim.Replace(" class=""ylwbg""", "")
                StrStrim = StrStrim.Replace(" class=""grybg""", "")
                StrStrim = StrStrim.Replace("href=""/live_market/dynaContent/live_watch/option_chain/optionDates.jsp?symbol=", "")
                StrStrim = StrStrim.Replace("" & Symbol & "&instrument=" & dr("instrumentName") & "&strike=", "")
                StrStrim = StrStrim.Replace("href=""/live_market/dynaContent/live_watch/option_chain/optionDates.jsp?symbol=NIFTY&instrument=OPTIDX&strike=", "")

                ' href="/live_market/dynaContent/live_watch/option_chain/optionDates.jsp?symbol=NIFTY&instrument=OPTIDX&strike=5500.00"><b>5500.00</b>

                'StrStrim = StrStrim.Replace(" class=tablehead", "")


                'StrStrim = StrStrim.Replace("href=""/live_market/dynaContent/live_watch/option_chain/optionDates.jsp?symbol=NIFTY&instrument=OPTIDX&strike=5500.00""><b>", "")
                'StrStrim = StrStrim.Replace("</b>", "")


                Dim strdata1 As [String]()
                Dim str As String() = New String(0) {}
                str(0) = "<div"
                strdata1 = StrStrim.Split(str, StringSplitOptions.None)



                If StrStrim Is Nothing Or StrStrim = "" Then
                    dt.AcceptChanges()
                    Return dt
                    Exit Function
                End If
                If strdata1.Length <= 1 Then
                    dt.AcceptChanges()
                    Return dt
                    Exit Function
                End If

                Dim strdata As [String]()
                str(0) = "<table"
                strdata = strdata1(16).Split(str, StringSplitOptions.None)
                If strdata.Length <= 1 Then
                    dt.AcceptChanges()
                    Return dt
                    Exit Function
                End If

                Dim strdata2 As [String]()
                str(0) = "</table>"
                strdata2 = strdata(1).Split(str, StringSplitOptions.None)

                Dim dataSet As DataSet = HtmlTableParserFOL2.ParseDataSet("<table" & strdata2(0).Replace("</div>", "") & "</table>")
                dt.Merge(dataSet.Tables(0))
                'StrStrim = StrStrim.Replace(" class=t1", "")
                'StrStrim = StrStrim.Replace(" class=t2", "")
                'StrStrim = StrStrim.Replace(" class=t3", "")
                'StrStrim = StrStrim.Replace(" class=t4", "")
                'StrStrim = StrStrim.Replace(" class=t5", "")
                'StrStrim = StrStrim.Replace(" class=""normalText""", "")
                'StrStrim = StrStrim.Replace(" class=""date""", "")
                'StrStrim = StrStrim.Replace(" class=""number""", "")

                ''StrStrim = StrStrim.Replace(" class=tablehead", "")

                'StrStrim = StrStrim.Replace("/live_market/dynaContent/live_watch/get_quote/GetQuoteFO.jsp?", "")
                'StrStrim = StrStrim.Replace("</a>", "")
                'StrStrim = StrStrim.Replace("<a", "")


                'Dim strdata1 As [String]()
                'Dim str As String() = New String(0) {}
                'str(0) = "<TABLE"
                'strdata1 = StrStrim.Split(str, StringSplitOptions.None)

                'If strdata1.Length <= 1 Then
                '    dt.AcceptChanges()
                '    Return dt
                '    Exit Function
                'End If

                'Dim strdata As [String]()
                'str(0) = "</div>"
                'strdata = strdata1(1).Split(str, StringSplitOptions.None)

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

                'If strdata.Length <= 4 Then
                '    dt.AcceptChanges()
                '    Return dt
                '    Exit Function
                'End If

                'Dim dataSet As DataSet = HtmlTableParserFOL2.ParseDataSet("<TABLE" & strdata(0) & "</TABLE>")
                'dt.Merge(dataSet.Tables(0))
                For Each drow As DataRow In dt.Rows
                    Dim cetoken As Long
                    Dim petoken As Long
                    ' Dim strike As Double = Val(drow("Strike").ToString())

                    ' val(dt.Rows(1)("Strike").ToString())
                    Dim scriptce, scriptpe As String

                    Dim script As String
                    'If cp = "-" Or cp = "F" Then
                    '    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
                    'Else
                    '    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice").ToString), "###0.0000") & "  " & drow("OptionType").ToString
                    'End If
                    'If cp = "-" Or cp = "F" Then
                    '    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
                    'Else
                    'Dim instrument As String
                    'If Symbol = "NIFTY" Or Symbol = "BANKNIFTY" Then
                    '    instrument = "OPTIDX"
                    'Else
                    '    instrument = "OPTSTK"
                    'End If
                    If Val(drow("Strike").ToString) = 0 Then
                        Continue For
                    End If
                    scriptce = dr("instrumentName") & "  " & GetSymbol(Symbol) & "  " & Format(CDate(exp), "ddMMMyyyy") & "  " & Format(Val(drow("Strike").ToString), "###0.00") & "  " & "CE"
                    scriptpe = dr("instrumentName") & "  " & GetSymbol(Symbol) & "  " & Format(CDate(exp), "ddMMMyyyy") & "  " & Format(Val(drow("Strike").ToString), "###0.00") & "  " & "PE"

                    'End If
                    'drow("script") = script.Trim.ToUpper
                    Try
                        cetoken = Val(cpfmaster.Compute("max(token)", "script='" & scriptce.Trim & "'").ToString)
                        petoken = Val(cpfmaster.Compute("max(token)", "script='" & scriptpe.Trim & "'").ToString)
                        '  drow("eqToken") = 0 'Val(eqmaster.Compute("max(token)", "script='" & Symbol.Trim & "  EQ'").ToString)
                    Catch ex As Exception
                        REM By Viral
                        'While Importing Contract cpfmaster or eqmaster Will Blank & Error Generate 
                        cetoken = 0
                        petoken = 0
                    End Try
                    If drow("CE_OI") = "-" Then
                        drow("CE_OI") = "0"
                    End If
                    If drow("PE_OI") = "-" Then
                        drow("PE_OI") = "0"
                    End If

                    If CLng(petoken) <> 0 Then
                        If OpenInterestprice.Contains(CLng(petoken)) Then
                            OpenInterestprice.Item(CLng(petoken)) = Val(Convert.ToDouble(drow("PE_OI")))
                        Else
                            OpenInterestprice.Add(CLng(petoken), Val(Convert.ToDouble(drow("PE_OI"))))
                        End If
                    End If
                    If CLng(cetoken) <> 0 Then
                        If OpenInterestprice.Contains(CLng(cetoken)) Then
                            OpenInterestprice.Item(CLng(cetoken)) = Val(Convert.ToDouble(drow("CE_OI")))
                        Else
                            OpenInterestprice.Add(CLng(cetoken), Val(Convert.ToDouble(drow("CE_OI"))))
                        End If
                    End If

                Next
                'For Each drow As DataRow In dt.Rows
                '    If CLng(drow("CE_Token")) <> 0 Then
                '        If OpenInterestprice.Contains(CLng(drow("CE_Token"))) Then
                '            OpenInterestprice.Item(CLng(drow("CE_Token"))) = Val(drow("CE_OI"))
                '        Else
                '            OpenInterestprice.Add(CLng(drow("CE_Token")), Val(drow("CE_OI")))
                '        End If
                '    End If
                '    If CLng(drow("PE_Token")) <> 0 Then
                '        If OpenInterestprice.Contains(CLng(drow("PE_Token"))) Then
                '            OpenInterestprice.Item(CLng(drow("PE_Token"))) = Val(drow("PE_OI"))
                '        Else
                '            OpenInterestprice.Add(CLng(drow("PE_Token")), Val(drow("PE_OI")))
                '        End If
                '    End If
                'Next
                '    If drow("ExpiryDate").ToString.Length > 0 Then
                '        drow("mdate") = DateSerial(Val(Microsoft.VisualBasic.Right(drow("ExpiryDate").ToString, 4)), Mnth(drow("ExpiryDate").ToString.Substring(2, 3)), Val(Microsoft.VisualBasic.Left(drow("ExpiryDate").ToString, 2)))
                '        Dim cp As String
                '        cp = Microsoft.VisualBasic.Left(drow("OptionType").ToString, 1)
                '        Dim script As String
                '        If cp = "-" Then
                '            script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
                '        Else
                '            script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice").ToString), "###0.00") & "  " & drow("OptionType").ToString
                '        End If
                '        drow("script") = script.Trim.ToUpper
                '        Try
                '            drow("Token") = Val(cpfmaster.Compute("max(token)", "script='" & script.Trim & "'").ToString)
                '            drow("eqToken") = Val(eqmaster.Compute("max(token)", "script='" & Symbol.Trim & "  EQ'").ToString)
                '        Catch ex As Exception
                '            REM By Viral
                '            'While Importing Contract cpfmaster or eqmaster Will Blank & Error Generate 
                '            drow("Token") = 0
                '            drow("eqToken") = 0
                '        End Try
                '    End If
                'Next


                dt.AcceptChanges()
            Next
            ' MsgBox(dt.Compute("max(LastPrice)", "Token=48223").ToString)
            Return dt
        Catch ex As Exception

        End Try
    End Function
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
            'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest) comment by payal
            Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("https://www.nseindia.com/products/dynaContent/derivatives/equities/fomwatchsymbol.jsp?key=" & Symbol.Replace("&", "%26").ToString()), HttpWebRequest)
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
            WriteLog("Error In GetInternetdata" & vbCrLf & ex.ToString)
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

        Dim dataSet As DataSet = HtmlTableParserFO.ParseDataSet("<TABLE" & strdata(0) & "</TABLE>")
        dt.Merge(dataSet.Tables(0))
        If dt.Rows.Count = 0 Then
            Dim exp As String
            '  For Each drow As DataRow In dt.Select("company='" + Symbol + "'", "company")

            Dim isCurr As Boolean = False
            Dim strCompany As String = ""
            '   strCompany = CType(Symbol, String).Split(",")(1)
            strCompany = GetSymbol(GetSymbol(Symbol).ToString.ToString())
            If strCompany.ToUpper.Contains("USDINR") Or strCompany.ToUpper.Contains("JPYINR") Or strCompany.ToUpper.Contains("GBPINR") Or strCompany.ToUpper.Contains("EURINR") Then
                isCurr = True
            End If
            If isCurr = True Then
                GetInternetData("-," & GetSymbol(Symbol))
            End If
            Dim dtexp As DataTable = New DataView(cpfmaster, "symbol='" & GetSymbol(Symbol).ToString & "' and option_type<>'XX'", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "instrumentName")
            For Each dr As DataRow In dtexp.Rows
                HtmlPars.GetFoInterNetDataLINK2_Future(Symbol.ToString)
                'dt.Rows.Clear()
                exp = CDate(dr("expdate1")).ToString("ddMMMyyyy").ToUpper() '"28SEP2017"
                HtmlPars.GetFoInterNetDataL2(Symbol.ToString, exp, dr("instrumentName"))
            Next
            '  Next
        Else

           
          



            For Each drow As DataRow In dt.Rows

                If drow("ExpiryDate").ToString.Length > 0 Then
                    drow("mdate") = DateSerial(Val(Microsoft.VisualBasic.Right(drow("ExpiryDate").ToString, 4)), Mnth(drow("ExpiryDate").ToString.Substring(2, 3)), Val(Microsoft.VisualBasic.Left(drow("ExpiryDate").ToString, 2)))
                    Dim cp As String
                    cp = Microsoft.VisualBasic.Left(drow("OptionType").ToString, 1)
                    Dim script As String
                    If cp = "-" Then
                        script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
                        If drow("LastPrice").ToString() = "0" Then
                            If CDate(drow("mdate")).Month = Date.Now.Month Then
                                Dim ddt As New DataTable
                                'internerRefreshtime = ""
                                Return ddt
                            End If

                        End If

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
            If dt.Rows.Count > 0 Then
                Try
                    internerRefreshtime = dt.Rows(0)("Instrument")

                    '  MDI.Refresh_intermet_time()
                Catch ex As Exception

                End Try
            End If
        End If
        
        dt.AcceptChanges()
        ' MsgBox(dt.Compute("max(LastPrice)", "Token=48223").ToString)
        Return dt
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
            'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fxTracker/priceWatchData.jsp?instrument=FUTCUR&currency=" & Left(Symbol, 3)), HttpWebRequest)

            Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fxTracker/priceWatchData.jsp?instrument=FUTCUR&currency=" & Symbol), HttpWebRequest)
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

        internerRefreshtime = ""
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
            dr("LastPrice") = Val(strdata(10).Replace(",", "")) ', GetType(Double))
            dr("traded") = Val(strdata(14).Replace(",", "")) ', GetType(Double))
            dr("Turnover") = Val(strdata(13).Replace(",", "")) ', GetType(Double))
            dr("UnderlyingValue") = 0 ', GetType(Double))
            dr("mdate") = CDate(strdata(3)) ', GetType(Date))
            dr("script") = "" ', GetType(String))
            dr("Token") = 0 ', GetType(Long))
            dr("eqToken") = 0 ', GetType(Long))
            dr("OI") = Val(strdata(12).Replace(",", ""))

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


        dt.Merge(GetCurOptInterNetData_Weeklydate(Symbol))
        dt.AcceptChanges()

        ddt = dt.Copy()








        For Each drow As DataRow In ddt.Select("", "mdate")
            dt.Merge(GetCurOptInterNetData(Symbol, drow("ExpiryDate")))
            dt.AcceptChanges()
        Next

        'If dt.Rows.Count = 0 Then

        '    Dim exp As String
        '    For Each drow As DataRow In dt.Select("company='" + GetSymbol(Symbol) + "'", "company")

        '        Dim isCurr As Boolean = False
        '        Dim strCompany As String = ""
        '        '   strCompany = CType(Symbol, String).Split(",")(1)
        '        strCompany = GetSymbol(GetSymbol(drow("company")).ToString.ToString())
        '        If strCompany.ToUpper.Contains("USDINR") Or strCompany.ToUpper.Contains("JPYINR") Or strCompany.ToUpper.Contains("GBPINR") Or strCompany.ToUpper.Contains("EURINR") Then
        '            isCurr = True
        '        End If
        '        If isCurr = True Then
        '            GetInternetData("-," & GetSymbol(drow("company")))
        '        End If
        '        Dim dtexp As DataTable = New DataView(cpfmaster, "symbol='" & GetSymbol(drow("company")).ToString & "' and option_type<>'XX'", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "instrumentName")
        '        For Each dr As DataRow In dtexp.Rows
        '            HtmlPars.GetFoInterNetDataLINK2_Future(drow("company").ToString)
        '            'dt.Rows.Clear()
        '            exp = CDate(dr("expdate1")).ToString("ddMMMyyyy").ToUpper() '"28SEP2017"
        '            HtmlPars.GetFoInterNetDataL2(drow("company").ToString, exp, dr("instrumentName"))
        '        Next
        '    Next
        'Else

        For Each drow As DataRow In dt.Rows
            If drow("ExpiryDate").ToString.Length > 0 Then
                Try

               
                '  If drow("ExpiryDate").ToString() = "8MAR2019" Then
                drow("mdate") = drow("ExpiryDate") 'DateSerial(Val(Microsoft.VisualBasic.Right(drow("ExpiryDate").ToString, 4)), Mnth(drow("ExpiryDate").ToString.Substring(2, 3)), Val(Microsoft.VisualBasic.Left(drow("ExpiryDate").ToString, 2)))
                'End If
                ' drow("mdate") = DateSerial(Val(Microsoft.VisualBasic.Right(drow("ExpiryDate").ToString, 4)), Mnth(drow("ExpiryDate").ToString.Substring(2, 3)), Val(Microsoft.VisualBasic.Left(drow("ExpiryDate").ToString, 2)))
                Dim cp As String
                cp = Microsoft.VisualBasic.Left(drow("OptionType").ToString, 1)
                Dim script As String
                If cp = "-" Or cp = "F" Then
                    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy")
                Else
                    script = drow("Instrument").ToString & "  " & Symbol & "  " & Format(CDate(drow("mdate")), "ddMMMyyyy") & "  " & Format(Val(drow("StrikePrice").ToString), "###0.0000") & "  " & drow("OptionType").ToString
                End If
                drow("script") = script.Trim.ToUpper
                Try
                    drow("Token") = Val(Currencymaster.Compute("max(token)", "script='" & script.Trim & "'").ToString)
                    drow("eqToken") = 0 'Val(eqmaster.Compute("max(token)", "script='" & Symbol.Trim & "  EQ'").ToString)
                Catch ex As Exception
                    REM By Viral
                    'While Importing Contract cpfmaster or eqmaster Will Blank & Error Generate 
                    drow("Token") = 0
                    drow("eqToken") = 0
                End Try


                If CLng(drow("Token")) <> 0 Then
                    If OpenInterestprice.Contains(CLng(drow("Token"))) Then
                        OpenInterestprice.Item(CLng(drow("Token"))) = Val(drow("OI"))
                    Else
                        OpenInterestprice.Add(CLng(drow("Token")), Val(drow("OI")))
                    End If
                End If
                Catch ex As Exception

                End Try
            End If
            
        Next
        'End If



        dt.AcceptChanges()
        ' MsgBox(dt.Compute("max(LastPrice)", "Token=48223").ToString)
        Return dt
    End Function
    Public Shared Function GetFOOptInterNetData(ByVal Symbol As String, ByVal Exp As String) As DataTable
        'textBox1.Multiline = True
        'textBox1.ScrollBars = ScrollBars.Both
        'above only for showing the sample
        ' If Symbol = "USDINR" And Exp = "26SEP2013" Then
        Exp = "28EP2017"
        ' End If
        Dim dt As New DataTable()

        dt.Columns.Add("Instrument", GetType(String)) 'ok
        dt.Columns.Add("Underlying", GetType(String))
        dt.Columns.Add("ExpiryDate", GetType(String))
        dt.Columns.Add("OptionType", GetType(String))
        dt.Columns.Add("StrikePrice", GetType(Double))
        dt.Columns.Add("HighPrice", GetType(Double))
        dt.Columns.Add("LowPrice", GetType(Double))
        dt.Columns.Add("PrevClose", GetType(Double))
        dt.Columns.Add("LastPrice", GetType(Double))
        dt.Columns.Add("traded", GetType(Double)) 'Volume
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


        Dim dataSet As DataSet = HtmlTableParserCur.ParseDataSet("<table" & strdata2(0).Replace("</div>", "") & "</table>")
        dt.Merge(dataSet.Tables(0))

        Return dt
    End Function
    Public Shared Function GetCurOptInterNetData(ByVal Symbol As String, ByVal Exp As String) As DataTable
        'textBox1.Multiline = True
        'textBox1.ScrollBars = ScrollBars.Both
        'above only for showing the sample
        'If Symbol = "USDINR" And Exp = "26SEP2013" Then
        '    Exp = "25SEP2013"
        'End If
        Dim dt As New DataTable()

        dt.Columns.Add("Instrument", GetType(String)) 'ok
        dt.Columns.Add("Underlying", GetType(String))
        dt.Columns.Add("ExpiryDate", GetType(String))
        dt.Columns.Add("OptionType", GetType(String))
        dt.Columns.Add("StrikePrice", GetType(Double))
        dt.Columns.Add("HighPrice", GetType(Double))
        dt.Columns.Add("LowPrice", GetType(Double))
        dt.Columns.Add("PrevClose", GetType(Double))
        dt.Columns.Add("LastPrice", GetType(Double))
        dt.Columns.Add("traded", GetType(Double)) 'Volume
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
        Try
            Dim strrefreshtime1 As [String]() = strdata1(11).Split(">")
            Dim strrefreshtime As String = strrefreshtime1(1).Replace("</div", "")
            internerRefreshtime = strrefreshtime
        Catch ex As Exception
            internerRefreshtime = ""
        End Try
        
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


        Dim dataSet As DataSet = HtmlTableParserCur.ParseDataSet("<table" & strdata2(0).Replace("</div>", "") & "</table>")
        dt.Merge(dataSet.Tables(0))

        Return dt
    End Function
    Public Shared Function GetCurOptInterNetData_Weeklydate(ByVal Symbol As String) As DataTable
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
            'Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fxTracker/priceWatchData.jsp?instrument=FUTCUR&currency=" & Left(Symbol, 3)), HttpWebRequest)

            Dim wbReq As HttpWebRequest = DirectCast(WebRequest.Create("http://www.nseindia.com/marketinfo/fxTracker/priceWatchData.jsp?instrument=OPTCUR&currency=" & Symbol), HttpWebRequest)
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

        internerRefreshtime = ""
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
            dr("LastPrice") = Val(strdata(10).Replace(",", "")) ', GetType(Double))
            dr("traded") = Val(strdata(14).Replace(",", "")) ', GetType(Double))
            dr("Turnover") = Val(strdata(13).Replace(",", "")) ', GetType(Double))
            dr("UnderlyingValue") = 0 ', GetType(Double))
            dr("mdate") = CDate(strdata(3)) ', GetType(Date))
            dr("script") = "" ', GetType(String))
            dr("Token") = 0 ', GetType(Long))
            dr("eqToken") = 0 ', GetType(Long))
            dr("OI") = Val(strdata(12).Replace(",", ""))

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

        Return dt
    End Function
End Class




''' <summary>
''' HtmlTableParser parses the contents of an html string into a System.Data DataSet or DataTable.
''' </summary>
Public Class HtmlTableParserCur
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
                Dim dataRow2 As DataRow = dataTable.NewRow()

                Dim cellMatches As MatchCollection = Regex.Matches(rowMatch.Value, CellPattern, ExpressionOptions)

                For columnIndex As Integer = 0 To cellMatches.Count - 1
                    Dim str As String = cellMatches(columnIndex).Groups(1).ToString()


                    'Instrument
                    dataRow("Instrument") = "OPTCUR"
                    dataRow2("Instrument") = "OPTCUR"
                    'OptionType
                    dataRow("OptionType") = "CE"
                    dataRow2("OptionType") = "PE"

                    'HighPrice
                    dataRow("HighPrice") = Val("0")
                    dataRow2("HighPrice") = Val("0")

                    'LowPrice
                    dataRow("LowPrice") = Val("0")
                    dataRow2("LowPrice") = Val("0")

                    'PrevClose
                    dataRow("PrevClose") = Val("0")
                    dataRow2("PrevClose") = Val("0")

                    'Turnover
                    dataRow("Turnover") = Val("0")
                    dataRow2("Turnover") = Val("0")

                    'UnderlyingValue
                    dataRow("UnderlyingValue") = Val("0")
                    dataRow2("UnderlyingValue") = Val("0")

                    'script
                    'dataRow("script") = ""
                    'dataRow2("script") = ""


                    '
                    '
                    '

                    Select Case columnIndex
                        Case 0
                            'Call - Chart
                            If str.Contains(",") = True Then
                                str = str.Split(",".ToCharArray())(2)
                            End If
                            dataRow("ExpiryDate") = str.Replace("'", "").Trim
                            dataRow2("ExpiryDate") = str.Replace("'", "").Trim
                            'dataRow("mdate") = CDate(str.Replace("'", "").Trim)
                            'dataRow2("mdate") = CDate(str.Replace("'", "").Trim)
                        Case 1
                            'Call - OI
                            dataRow("OI") = Val(str.Replace(",", ""))
                        Case 2
                            'Change in OI
                            'dataRow("ChangeInOI") = Val(str.Replace(",", ""))
                        Case 3
                            'Call - Volume
                            dataRow("traded") = Val(str.Replace(",", ""))
                        Case 4
                            'Call - IV

                        Case 5
                            'Call - LTP
                            If str.Contains(">") = True Then
                                str = str.Split(">".ToCharArray())(1)
                            End If
                            dataRow("LastPrice") = Val(str.Replace(",", ""))
                        Case 6
                            'Call - BidQty
                            'MsgBox(str)
                        Case 7
                            'Call - BidPrice
                        Case 8
                            'Call - AskPrice
                        Case 9
                            'Call - AskQty
                        Case 10
                            'Call - StrikePrice
                            If str.Contains("<b>") = True Then
                                str = str.Split("<b>".ToCharArray())(5).Replace("""", "")
                            End If
                            dataRow("StrikePrice") = Val(str.Replace(",", ""))
                            dataRow2("StrikePrice") = Val(str.Replace(",", ""))
                        Case 11
                            'Put - BidQty
                        Case 12
                            'Put - BidPrice
                        Case 13
                            'Put - AskPrice
                        Case 14
                            'Put - AskQty
                        Case 15
                            'Put - LTP
                            If str.Contains(">") = True Then
                                str = str.Split(">".ToCharArray())(1)
                            End If
                            dataRow2("LastPrice") = Val(str.Replace(",", ""))
                        Case 16
                            'Put - IV
                        Case 17
                            'Put - Volume
                            dataRow2("traded") = Val(str.Replace(",", ""))
                        Case 18
                            'Put - Change in OI

                        Case 19
                            'Put - OI
                            dataRow2("OI") = Val(str.Replace(",", ""))
                        Case 20
                            'Put - Chart
                        Case Else
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
                    End Select



                Next

                dataTable.Rows.Add(dataRow)
                dataTable.Rows.Add(dataRow2)
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
        Dim dtcolumn As DataColumn() = New DataColumn(12) {}
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
        dtcolumn(12) = New DataColumn("OI", GetType(Double))



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



''' <summary>
''' HtmlTableParser parses the contents of an html string into a System.Data DataSet or DataTable.
''' </summary>
Public Class HtmlTableParserMCXCur
    Private Const ExpressionOptions As RegexOptions = RegexOptions.Singleline Or RegexOptions.Multiline Or RegexOptions.IgnoreCase

    Private Const CommentPattern As String = "<!--(.*?)-->"
    Private Const TablePattern As String = "<table[^>]*>(.*?)</table>"
    Private Const HeaderPattern As String = "<th[^>]*>(.*?)</th>"
    Private Const RowPattern As String = "<tr[^>]*>(.*?)</tr>"
    Private Const CellPattern As String = "<td[^>]*>(.*?)</td>"

    Public Shared PriveteExpiryDate As String = ""

    ''' <summary>
    ''' Given an HTML string containing n table tables, parse them into a DataSet containing n DataTables.
    ''' </summary>
    ''' <param name="html">An HTML string containing n HTML tables</param>
    ''' <returns>A DataSet containing a DataTable for each HTML table in the input HTML</returns>
    Public Shared Function ParseDataSet(ByVal html As String, ByVal exp As String) As DataSet
        PriveteExpiryDate = exp
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
                Dim dataRow2 As DataRow = dataTable.NewRow()

                Dim cellMatches As MatchCollection = Regex.Matches(rowMatch.Value, CellPattern, ExpressionOptions)

                For columnIndex As Integer = 0 To cellMatches.Count - 1
                    'If cellMatches(7).Groups(1).ToString().Length > 100 Then
                    '    Continue For
                    'End If
                    Dim str As String = cellMatches(columnIndex).Groups(1).ToString()


                    'Instrument
                    dataRow("Instrument") = "OPTCUR"
                    dataRow2("Instrument") = "OPTCUR"
                    'OptionType
                    dataRow("OptionType") = "CE"
                    dataRow2("OptionType") = "PE"

                    'HighPrice
                    dataRow("HighPrice") = Val("0")
                    dataRow2("HighPrice") = Val("0")

                    'LowPrice
                    dataRow("LowPrice") = Val("0")
                    dataRow2("LowPrice") = Val("0")

                    'PrevClose
                    dataRow("PrevClose") = Val("0")
                    dataRow2("PrevClose") = Val("0")

                    'Turnover
                    dataRow("Turnover") = Val("0")
                    dataRow2("Turnover") = Val("0")

                    'UnderlyingValue
                    dataRow("UnderlyingValue") = Val("0")
                    dataRow2("UnderlyingValue") = Val("0")

                    'script
                    'dataRow("script") = ""
                    'dataRow2("script") = ""


                    '
                    '
                    '
                    'If str.Contains(",") = True Then
                    '    str = str.Split(",".ToCharArray())(2)
                    'End If
                    dataRow("ExpiryDate") = PriveteExpiryDate
                    dataRow2("ExpiryDate") = PriveteExpiryDate

                    Select Case columnIndex
                        Case 0
                            'Call - Chart
                            dataRow("OI") = Val(str.Replace(",", "").Replace("-", ""))
                        Case 1
                            'Call - LTP
                            If str.Contains(">") = True Then
                                str = str.Split(">".ToCharArray())(1)
                            End If
                            dataRow("LastPrice") = Val(str.Replace(",", "").Replace("-", ""))
                        Case 2
                            'Call - Volume
                            dataRow("traded") = Val(str.Replace(",", ""))
                        Case 3
                            'Call - IV

                        Case 4
                            
                        Case 5
                            'Call - BidQty
                        Case 6
                            'Call - BidPrice
                        Case 7
                            'Call - StrikePrice
                            'If str.Contains(">") = True Then
                            '    str = str.Split(">".ToCharArray())(1).Replace("</a", "")
                            'End If
                            dataRow("StrikePrice") = Val(str.Split(">".ToCharArray())(1).Replace("</a", ""))
                            dataRow2("StrikePrice") = Val(str.Split(">".ToCharArray())(1).Replace("</a", ""))

                        Case 8
                            'Call - AskQty
                        Case 9
                            
                        Case 10
                            'Put - BidQty
                        Case 11
                            'Put - BidPrice
                        Case 12
                            'Put - Volume
                            dataRow2("traded") = Val(str.Replace(",", ""))
                        Case 13
                            'Put - LTP
                            If str.Contains(">") = True Then
                                str = str.Split(">".ToCharArray())(1)
                            End If
                            dataRow2("LastPrice") = Val(str.Replace(",", ""))
                        Case 14
                            'Put - OI
                            dataRow2("OI") = Val(str.Replace(",", ""))
                            
                        Case 15
                            'Put - IV
                        Case 16
                            
                        Case 17
                            
                        Case 18
                            'Put - Chart
                        Case Else
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
                    End Select



                Next

                dataTable.Rows.Add(dataRow)
                dataTable.Rows.Add(dataRow2)
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
        Dim dtcolumn As DataColumn() = New DataColumn(12) {}
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
        dtcolumn(12) = New DataColumn("OI", GetType(Double))



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
Public Class HtmlTableParserFOL2
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


                        If dataTable.Columns(columnIndex).ColumnName = "Strike" Then
                            If str.Contains(">") = True Then
                                str = str.Split(">".ToCharArray())(1)
                            End If
                            dataRow(columnIndex) = Format(Val(str.Replace("-", "0")), "#0.00")
                        ElseIf dataTable.Columns(columnIndex).ColumnName = "PE_LTP" Then
                            If str.Contains(">") = True Then
                                str = str.Split(">".ToCharArray())(1)
                            End If
                            dataRow(columnIndex) = Format(Val(str.Replace("-", "0")), "#0.00")
                        ElseIf dataTable.Columns(columnIndex).ColumnName = "CE_LTP" Then
                            If str.Contains(">") = True Then
                                str = str.Split(">".ToCharArray())(1)
                            End If
                            dataRow(columnIndex) = Format(Val(str.Replace("-", "0")), "#0.00")
                        Else
                            dataRow(columnIndex) = str.Replace("-", "0")
                        End If


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
        Dim dtcolumn As DataColumn() = New DataColumn(22) {}
       
        dtcolumn(0) = New DataColumn("CE_Chart", GetType(String))
        dtcolumn(1) = New DataColumn("CE_OI", GetType(String))
        dtcolumn(2) = New DataColumn("CE_Chg_OI", GetType(String))
        dtcolumn(3) = New DataColumn("CE_Volume", GetType(String))
        dtcolumn(4) = New DataColumn("CE_IV", GetType(String))
        dtcolumn(5) = New DataColumn("CE_LTP", GetType(String))
        dtcolumn(6) = New DataColumn("CE_NetChange", GetType(String))
        dtcolumn(7) = New DataColumn("CE_BidQty", GetType(String))
        dtcolumn(8) = New DataColumn("CE_BidPrice", GetType(String))
        dtcolumn(9) = New DataColumn("CE_AskPrice", GetType(String))
        dtcolumn(10) = New DataColumn("CE_AskQty", GetType(String))
        dtcolumn(11) = New DataColumn("Strike", GetType(String))
        dtcolumn(12) = New DataColumn("PE_BidQty", GetType(String))
        dtcolumn(13) = New DataColumn("PE_BidPrice", GetType(String))
        dtcolumn(14) = New DataColumn("PE_AskPrice", GetType(String))
        dtcolumn(15) = New DataColumn("PE_AskQty", GetType(String))
        dtcolumn(16) = New DataColumn("PE_NetChange", GetType(String))
        dtcolumn(17) = New DataColumn("PE_LTP", GetType(String))
        dtcolumn(18) = New DataColumn("PE_IV", GetType(String))
        dtcolumn(19) = New DataColumn("PE_Volume", GetType(String))
        dtcolumn(20) = New DataColumn("PE_Chg_OI", GetType(String))
        dtcolumn(21) = New DataColumn("PE_OI", GetType(String))
        dtcolumn(22) = New DataColumn("PE_Chart", GetType(String))
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

        Dim dtcolumn As DataColumn() = New DataColumn(22) {}
        dtcolumn(0) = New DataColumn("CE_Chart", GetType(String))
        dtcolumn(1) = New DataColumn("CE_OI", GetType(String))
        dtcolumn(2) = New DataColumn("CE_Chg_OI", GetType(String))
        dtcolumn(3) = New DataColumn("CE_Volume", GetType(String))
        dtcolumn(4) = New DataColumn("CE_IV", GetType(String))
        dtcolumn(5) = New DataColumn("CE_LTP", GetType(String))
        dtcolumn(6) = New DataColumn("CE_NetChange", GetType(String))
        dtcolumn(7) = New DataColumn("CE_BidQty", GetType(String))
        dtcolumn(8) = New DataColumn("CE_BidPrice", GetType(String))
        dtcolumn(9) = New DataColumn("CE_AskPrice", GetType(String))
        dtcolumn(10) = New DataColumn("CE_AskQty", GetType(String))
        dtcolumn(11) = New DataColumn("Strike", GetType(String))
        dtcolumn(12) = New DataColumn("PE_BidQty", GetType(String))
        dtcolumn(13) = New DataColumn("PE_BidPrice", GetType(String))
        dtcolumn(14) = New DataColumn("PE_AskPrice", GetType(String))
        dtcolumn(15) = New DataColumn("PE_AskQty", GetType(String))
        dtcolumn(16) = New DataColumn("PE_NetChange", GetType(String))
        dtcolumn(17) = New DataColumn("PE_LTP", GetType(String))
        dtcolumn(18) = New DataColumn("PE_IV", GetType(String))
        dtcolumn(19) = New DataColumn("PE_Volume", GetType(String))
        dtcolumn(20) = New DataColumn("PE_Chg_OI", GetType(String))
        dtcolumn(21) = New DataColumn("PE_OI", GetType(String))
        dtcolumn(22) = New DataColumn("PE_Chart", GetType(String))

        Return dtcolumn
        'return (from index in Enumerable.Range(0, columnCount)
        '        select new DataColumn("Column " + Convert.ToString(index))).ToArray();

    End Function
End Class
''' <summary>
''' HtmlTableParser parses the contents of an html string into a System.Data DataSet or DataTable.
''' </summary>
Public Class HtmlTableParserFO
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
                        Try
                            dataRow(columnIndex) = str.Replace("-", "0")
                        Catch ex As Exception

                        End Try

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
    Private Shared Function ParseColumnsIndex(ByVal tableHtml As String) As DataColumn()
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
Public Class HtmlTableParserFOFUTURE
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
                        Try
                            dataRow(columnIndex) = str.Replace("-", "0")
                        Catch ex As Exception

                        End Try

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
        Dim dtcolumn As DataColumn() = New DataColumn(12) {}
        dtcolumn(0) = New DataColumn("Instrument", GetType(String))
        dtcolumn(1) = New DataColumn("Underlying", GetType(String))
        dtcolumn(2) = New DataColumn("ExpiryDate", GetType(String))
        dtcolumn(3) = New DataColumn("OptionType", GetType(String))
        dtcolumn(4) = New DataColumn("StrikePrice", GetType(Double))
        dtcolumn(5) = New DataColumn("OpenPrice", GetType(Double))
        dtcolumn(6) = New DataColumn("HighPrice", GetType(Double))
        dtcolumn(7) = New DataColumn("LowPrice", GetType(Double))
        dtcolumn(8) = New DataColumn("PrevClose", GetType(Double))
        dtcolumn(9) = New DataColumn("LastPrice", GetType(Double))
        dtcolumn(10) = New DataColumn("traded", GetType(Double))
        dtcolumn(11) = New DataColumn("Turnover", GetType(Double))
        dtcolumn(12) = New DataColumn("UnderlyingValue", GetType(Double))
        Return dtcolumn
        'return (from Match headerMatch in headerMatches  
        '    select new DataColumn(headerMatch.Groups[1].ToString())).ToArray();
    End Function
    Private Shared Function ParseColumnsIndex(ByVal tableHtml As String) As DataColumn()
        Dim headerMatches As MatchCollection = Regex.Matches(tableHtml, HeaderPattern, ExpressionOptions)
        Dim dtcolumn As DataColumn() = New DataColumn(12) {}
        dtcolumn(0) = New DataColumn("Instrument", GetType(String))
        dtcolumn(1) = New DataColumn("Underlying", GetType(String))
        dtcolumn(2) = New DataColumn("ExpiryDate", GetType(String))
        dtcolumn(3) = New DataColumn("OptionType", GetType(String))
        dtcolumn(4) = New DataColumn("StrikePrice", GetType(Double))
        dtcolumn(5) = New DataColumn("OpenPrice", GetType(Double))
        dtcolumn(6) = New DataColumn("HighPrice", GetType(Double))
        dtcolumn(7) = New DataColumn("LowPrice", GetType(Double))
        dtcolumn(8) = New DataColumn("PrevClose", GetType(Double))
        dtcolumn(9) = New DataColumn("LastPrice", GetType(Double))
        dtcolumn(10) = New DataColumn("traded", GetType(Double))
        dtcolumn(11) = New DataColumn("Turnover", GetType(Double))
        dtcolumn(12) = New DataColumn("UnderlyingValue", GetType(Double))
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

        Dim dtcolumn As DataColumn() = New DataColumn(12) {}
        dtcolumn(0) = New DataColumn("Instrument", GetType(String))
        dtcolumn(1) = New DataColumn("Underlying", GetType(String))
        dtcolumn(2) = New DataColumn("ExpiryDate", GetType(String))
        dtcolumn(3) = New DataColumn("OptionType", GetType(String))
        dtcolumn(4) = New DataColumn("StrikePrice", GetType(Double))
        dtcolumn(5) = New DataColumn("OpenPrice", GetType(Double))
        dtcolumn(6) = New DataColumn("HighPrice", GetType(Double))
        dtcolumn(7) = New DataColumn("LowPrice", GetType(Double))
        dtcolumn(8) = New DataColumn("PrevClose", GetType(Double))
        dtcolumn(9) = New DataColumn("LastPrice", GetType(Double))
        dtcolumn(10) = New DataColumn("traded", GetType(Double))
        dtcolumn(11) = New DataColumn("Turnover", GetType(Double))
        dtcolumn(12) = New DataColumn("UnderlyingValue", GetType(Double))

        Return dtcolumn
        'return (from index in Enumerable.Range(0, columnCount)
        '        select new DataColumn("Column " + Convert.ToString(index))).ToArray();

    End Function
End Class
Public Class HtmlTableParserIndex
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
        Dim dtcolumn As DataColumn() = New DataColumn(9) {}
        dtcolumn(0) = New DataColumn("Index", GetType(String))
        dtcolumn(1) = New DataColumn("Current", GetType(Double))
        dtcolumn(2) = New DataColumn("Change", GetType(Double))
        dtcolumn(3) = New DataColumn("Open", GetType(Double))
        dtcolumn(4) = New DataColumn("High", GetType(Double))
        dtcolumn(5) = New DataColumn("Low", GetType(Double))
        dtcolumn(6) = New DataColumn("PrevClose", GetType(String))
        dtcolumn(7) = New DataColumn("Today", GetType(String))
        dtcolumn(8) = New DataColumn("52wHigh", GetType(Double))
        dtcolumn(9) = New DataColumn("52wLow", GetType(Double))
     


        Return dtcolumn
        'return (from Match headerMatch in headerMatches  
        '    select new DataColumn(headerMatch.Groups[1].ToString())).ToArray();
    End Function
    Private Shared Function ParseColumnsIndex(ByVal tableHtml As String) As DataColumn()
        Dim headerMatches As MatchCollection = Regex.Matches(tableHtml, HeaderPattern, ExpressionOptions)
        Dim dtcolumn As DataColumn() = New DataColumn(9) {}
        dtcolumn(0) = New DataColumn("Index", GetType(String))
        dtcolumn(1) = New DataColumn("Current", GetType(Double))
        dtcolumn(2) = New DataColumn("Change", GetType(Double))
        dtcolumn(3) = New DataColumn("Open", GetType(Double))
        dtcolumn(4) = New DataColumn("High", GetType(Double))
        dtcolumn(5) = New DataColumn("Low", GetType(Double))
        dtcolumn(6) = New DataColumn("PrevClose", GetType(Double))
        dtcolumn(7) = New DataColumn("Today", GetType(String))
        dtcolumn(8) = New DataColumn("52wHigh", GetType(Double))
        dtcolumn(9) = New DataColumn("52wLow", GetType(Double))



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

