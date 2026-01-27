Imports System
Imports System.IO
Imports VolHedge.UpdaterCore
Imports System.Net
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Data.OleDb



Public Class import_master
    Dim ObjIO As ImportData.ImportOperation
    Dim objMast As Mastergenrate = New Mastergenrate
    Private downloader As FileDownloader

    ''' <summary>
    ''' open Browser for only text File Extention and store file path textbox.text as file name for FO Trade File
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        If FLGCSVCONTRACT = True Then
            opfile.Filter = "Files(*.csv)|*.csv"
        Else
            opfile.Filter = "Files(*.txt)|*.txt"
        End If

        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtpath.Text = opfile.FileName
        End If
    End Sub
    ''' <summary>
    ''' this click even close the Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    ''' <summary>
    ''' this click event clear the FO Text box File Path
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        txtpath.Text = ""

    End Sub

    ''' <summary>
    ''' this Click event first Initilise the dtable data table
    ''' then fill this data table from text file
    ''' and after calculation on it stors data to data base for only FO Trades
    ''' and then Load Data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        'Dim fName As String = ""

        'Dim fpath As String
        'Dim Payalcsv As String

        'fpath = CStr(txtpath.Text.Trim)
        'Dim fi As New FileInfo(txtpath.Text)
        'Dim strdata As [String]()
        'strdata = fpath.Split(New Char() {"\"c})
        ''strdata = dr("script").Split("  ", StringSplitOptions.None)
        'Dim tempdata As New DataTable

        'Dim filename As String = strdata(strdata.Length - 1)



        'Dim sConnectionStringz As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
        'Dim objConn As New OleDbConnection(sConnectionStringz)
        'objConn.Open()


        'Dim objCmdSelect As New OleDbCommand("SELECT * FROM [" & filename & "] ", objConn)
        ''Dim objCmdSelect As New OleDbCommand("SELECT strike,option_type,Exp. Date,Company,Qty,Rate,Instrument,entrydate FROM " & fi.Name, objConn)
        'Dim objAdapter1 As New OleDbDataAdapter
        'objAdapter1.SelectCommand = objCmdSelect
        'objAdapter1.Fill(tempdata)
        'objConn.Close()

        'MsgBox(Now)
        'If txtpath.Text <> "" Then
        '    Me.Cursor = Cursors.WaitCursor
        '    ' Try
        '    Dim dtable As DataTable
        '    dtable = New DataTable
        '    With dtable.Columns
        '        .Add("token", GetType(Integer))
        '        '============================keval(16-2-10)
        '        .Add("asset_tokan", GetType(Integer))
        '        '==============================
        '        .Add("InstrumentName", GetType(String))
        '        .Add("Symbol", GetType(String))
        '        .Add("Siries", GetType(String))
        '        .Add("ExpiryDate", GetType(Integer))
        '        .Add("StrikePrice", GetType(Double))
        '        .Add("OptionType", GetType(String))
        '        .Add("script", GetType(String))
        '        .Add("lotsize", GetType(Integer))
        '    End With
        '    Dim drow As DataRow
        '    Dim date1 As Date = "1/1/1980"
        '    If txtpath.Text.Trim <> "" Then
        '        If File.Exists(txtpath.Text) Then
        '            Dim iline As New StreamReader(txtpath.Text)
        '            Dim chk As Boolean
        '            chk = False
        '            Dim i As Integer = 0
        '            While iline.EndOfStream = False
        '                If i > 0 Then
        '                    chk = True
        '                    Dim line As String()
        '                    line = Split(iline.ReadLine, "|")
        '                    If line(2) <> "" Then
        '                        drow = dtable.NewRow
        '                        drow("Token") = CInt(line(0))
        '                        '======================keval(16-2-10)
        '                        drow("Asset_Tokan") = CInt(line(1))
        '                        '=======================
        '                        drow("InstrumentName") = CStr(line(2))
        '                        drow("Symbol") = CStr(line(3))
        '                        drow("Siries") = CStr(line(4))
        '                        drow("ExpiryDate") = CInt(line(6))
        '                        drow("StrikePrice") = CDbl(line(7)) / 100
        '                        drow("OptionType") = CStr(line(8))
        '                        If Not IsDBNull(drow("OptionType")) Then
        '                            If Mid(UCase(drow("OptionType")), 1, 1) = "C" Or Mid(UCase(drow("OptionType")), 1, 1) = "P" Then
        '                                drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy") & "  " & CStr(Format(Val(drow("StrikePrice")), "###0.00")) & "  " & drow("OptionType")
        '                            Else
        '                                drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy")
        '                            End If
        '                        Else
        '                            drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy")
        '                        End If

        '                        drow("lotsize") = CStr(line(31))
        '                        dtable.Rows.Add(drow)
        '                    End If
        '                    lblcount.Text = CInt(lblcount.Text) + 1
        '                    lblcount.Refresh()
        '                End If
        '                i = i + 1
        '            End While
        '            iline.Close()
        'objMast.insert(dtable)
        '            If chk = True Then
        '                fill_token()
        '                MsgBox("File Import Successfully")
        '                txtpath.Text = ""
        '                lblcount.Text = 0
        '                lblcount.Refresh()
        '            End If
        '        End If
        '    End If
        '    Me.Cursor = Cursors.Default
        'Else
        '    MsgBox("Please Select File Path....")
        'End If
        '' Catch ex As Exception
        ''MsgBox("File Not Processed")
        ''MsgBox(ex.ToString)
        '' End Try
        'MsgBox(Now)
        Dim flg As Boolean = validate_contract_csv_file(txtpath.Text, "FO")
        If flg = True Then
            MsgBox("Invalid CSV File..", MsgBoxStyle.Information)
            Return
        End If

        Me.Cursor = Cursors.WaitCursor
        If txtpath.Text.Trim <> "" And File.Exists(txtpath.Text) Then
            If import_Data.ContractImport(txtpath.Text, ObjIO) = True Then
                fill_token()

                Me.Cursor = Cursors.Default
                MsgBox("File Imported Successfully.", MsgBoxStyle.Information)
                txtpath.Text = ""
            Else
                Me.Cursor = Cursors.Default
                MsgBox("File Not Processed.", MsgBoxStyle.Information)
            End If
        Else
            Me.Cursor = Cursors.Default
            MsgBox("Invalid File Path.", MsgBoxStyle.Exclamation)
        End If
        If IsUnMatchTrade() Then
            Dim frm As New FrmMatchContract
            frm.ShowForm("FO")
        End If
        'MsgBox(Now)
    End Sub
    Private Function IsUnMatchTrade() As Boolean
        'Dim dt As DataTable
        'Try
        '    'Dim Str As String = "SELECT trading.uid, trading.script, trading.mdate, Contract.script, Contract.OScript " & _
        '    '    " FROM trading LEFT JOIN Contract ON trading.script = Contract.script " & _
        '    '    " WHERE (((Contract.script) Is Null));"

        '    Dim Str As String = "SELECT trading.uid, trading.script, trading.mdate, " & _
        '    " IIf(Left(trading.instrumentname,3)='FUT',trading.instrumentname + '  ' + trading.company + '  ' + UCase(Format([mdate],'ddmmmyyyy')), " & _
        '    " trading.instrumentname + '  ' + trading.company + '  ' + UCase(Format([mdate],'ddmmmyyyy') + '  ' + Format(trading.strikerate,'0.00') + '  ' + iif(trading.cp='C','CE','PE')  ))  AS NewScrip, trading.instrumentname, trading.company, UCase(Format([mdate],'ddmmmyyyy')) AS M, Format(trading.strikerate,'0.00') AS Expr1, trading.cp, Contract.script, Contract.OScript " & _
        '    " FROM trading LEFT JOIN Contract ON trading.script = Contract.script WHERE (((Contract.script) Is Null));"


        '    Dim objTrad As trading = New trading
        '    dt = objTrad.select_Query(Str)
        'Catch ex As Exception
        '    MsgBox("Error to Find UnMatchTrade ")
        '    Return False
        'End Try

        'If dt.Rows.Count > 0 Then
        '    Return True
        'Else
        Return False
        'End If
    End Function

    ''' <summary>
    ''' open Browser for only text File Extention and store file path textbox.text as file name for EQ Trade File
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdeqbr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqbr.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        'opfile.Filter = "Files(*.txt)|*.txt"
        If FLGCSVCONTRACT = True Then
            opfile.Filter = "Files(*.csv)|*.csv"
        Else
            opfile.Filter = "Files(*.txt)|*.txt"
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txteqpath.Text = opfile.FileName
        End If
    End Sub

    ''' <summary>
    '''  this Click event first Initilise the dtable data table
    ''' then fill this data table from text file
    ''' and after calculation on it stors data to data base for only EQ Trades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdeqimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqimport.Click
        'If txteqpath.Text <> "" Then
        '    Me.Cursor = Cursors.WaitCursor
        '    Try

        '        Dim dtable As DataTable
        '        dtable = New DataTable
        '        With dtable.Columns
        '            .Add("token", GetType(Integer))
        '            .Add("symbol", GetType(String))
        '            .Add("series", GetType(String))
        '            .Add("isin", GetType(String))
        '            .Add("script", GetType(String))
        '        End With
        '        Dim drow As DataRow
        '        If txteqpath.Text.Trim <> "" Then
        '            If File.Exists(txteqpath.Text) Then
        '                Dim iline As New StreamReader(txteqpath.Text)
        '                Dim chk As Boolean
        '                chk = False
        '                Dim i As Integer = 0
        '                iline.ReadLine() '=============================keval chakalasiya(15-02-2010)
        '                While iline.EndOfStream = False

        '                    If i > 0 Then
        '                        chk = True
        '                        Dim line As String()
        '                        line = Split(iline.ReadLine, "|")
        '                        drow = dtable.NewRow
        '                        drow("Token") = CInt(line(0))
        '                        drow("symbol") = CStr(line(1))
        '                        drow("series") = CStr(line(2))
        '                        drow("isin") = CStr(line(46))
        '                        drow("script") = CStr(line(1)) & "  " & CStr(line(2))
        '                        dtable.Rows.Add(drow)
        '                        lblcount1.Text = CInt(lblcount1.Text) + 1
        '                        lblcount1.Refresh()
        '                    End If
        '                    i = i + 1
        '                End While
        '                iline.Close()
        '                objMast.Equity_insert(dtable)
        '                If chk = True Then
        '                    fill_token()
        '                    MsgBox("File Import Successfully")

        '                    txteqpath.Text = ""
        '                    lblcount1.Text = 0
        '                    lblcount1.Refresh()
        '                End If
        '            End If
        '        End If
        '    Catch ex As Exception
        '        MsgBox("File Not Processed")
        '        'MsgBox(ex.ToString)
        '    End Try
        '    Me.Cursor = Cursors.Default
        'Else
        '    MsgBox("Please Select File Path....")
        'End If

        'MsgBox(Now)
        Dim flg As Boolean = validate_contract_csv_file(txteqpath.Text, "EQ")
        If flg = True Then
            MsgBox("Invalid CSV File..", MsgBoxStyle.Information)
            Return
        End If
        Me.Cursor = Cursors.WaitCursor
        If txteqpath.Text.Trim <> "" And File.Exists(txteqpath.Text) Then
            If import_Data.SecurityImport(txteqpath.Text, ObjIO) = True Then
                fill_token()
                Me.Cursor = Cursors.Default
                MsgBox("File Imported Successfully.", MsgBoxStyle.Information)
                txteqpath.Text = ""
            Else
                Me.Cursor = Cursors.Default
                MsgBox("File Not Processed.", MsgBoxStyle.Information)
            End If
        Else
            Me.Cursor = Cursors.Default
            MsgBox("Invalid File Path.", MsgBoxStyle.Exclamation)
        End If
        'MsgBox(Now)
    End Sub

    ''' <summary>
    ''' Clear Eq Text File Path Text Box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdeqclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqclear.Click
        txteqpath.Text = ""
    End Sub

    Private Sub import_master_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub

    Private Sub import_master_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ObjIO = New ImportData.ImportOperation
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub

    ''' <summary>
    ''' on escap key press close the Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub import_master_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' open Browser for only text File Extention and store file path textbox.text as file name for Currency Trade File
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseCurrency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseCurrency.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        If FLGCSVCONTRACT = True Then
            opfile.Filter = "Files(*.csv)|*.csv"
        Else
            opfile.Filter = "Files(*.txt)|*.txt"
        End If
        'opfile.Filter = "Files(*.txt)|*.txt"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCurrencyPath.Text = opfile.FileName
        End If
    End Sub

    ''' <summary>
    '''  this Click event first Initilise the dtable data table
    ''' then fill this data table from text file
    ''' and after calculation on it stors data to data base for only Currency Trades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnImportCurrency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportCurrency.Click
        'If txtCurrencyPath.Text <> "" Then
        '    Me.Cursor = Cursors.WaitCursor
        '    Dim dtable As DataTable
        '    dtable = New DataTable
        '    With dtable.Columns
        '        .Add("token", GetType(Integer))
        '        '============================keval(16-2-10)
        '        .Add("asset_tokan", GetType(Integer))
        '        '==============================
        '        .Add("InstrumentName", GetType(String))
        '        .Add("Symbol", GetType(String))
        '        .Add("Siries", GetType(String))
        '        .Add("ExpiryDate", GetType(Integer))
        '        .Add("StrikePrice", GetType(Double))
        '        .Add("OptionType", GetType(String))
        '        .Add("script", GetType(String))
        '        .Add("lotsize", GetType(Integer))
        '        .Add("multiplier", GetType(Integer))
        '    End With
        '    Dim drow As DataRow
        '    Dim date1 As Date = "1/1/1980"
        '    If txtCurrencyPath.Text.Trim <> "" Then
        '        If File.Exists(txtCurrencyPath.Text) Then
        '            Dim iline As New StreamReader(txtCurrencyPath.Text)
        '            Dim chk As Boolean
        '            chk = False
        '            Dim i As Integer = 0
        '            While iline.EndOfStream = False
        '                If i > 0 Then
        '                    chk = True
        '                    Dim line As String()
        '                    line = Split(iline.ReadLine, "|")
        '                    If line(2) <> "" Then
        '                        drow = dtable.NewRow
        '                        drow("Token") = CInt(line(0))
        '                        '======================keval(16-2-10)
        '                        drow("Asset_Tokan") = CInt(line(1))
        '                        '=======================
        '                        drow("InstrumentName") = CStr(line(2))
        '                        drow("Symbol") = CStr(line(3))
        '                        drow("Siries") = CStr(line(4))
        '                        drow("ExpiryDate") = CInt(line(6))
        '                        'drow("ExpiryDate") = DateDiff(DateInterval.Second, date1, CDate(Format(DateAdd(DateInterval.Second, drow("ExpiryDate"), date1), "dd-MMM-yyyy")))
        '                        drow("StrikePrice") = Format(CDbl(line(7)) / 10000000, "#0.0000")
        '                        drow("OptionType") = CStr(line(8))
        '                        If Not IsDBNull(drow("OptionType")) Then
        '                            If Mid(UCase(drow("OptionType")), 1, 1) = "C" Or Mid(UCase(drow("OptionType")), 1, 1) = "P" Then
        '                                drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy") & "  " & CStr(Format(Val(drow("StrikePrice")), "###0.0000")) & "  " & drow("OptionType")
        '                            Else
        '                                drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy")
        '                            End If
        '                        Else
        '                            drow("script") = drow("InstrumentName") & "  " & drow("Symbol") & "  " & Format(DateAdd(DateInterval.Second, CInt(drow("ExpiryDate")), date1), "ddMMMyyyy")
        '                        End If
        '                        drow("lotsize") = CStr(line(31))
        '                        drow("multiplier") = CStr(line(51))
        '                        If drow("ExpiryDate") >= DateDiff(DateInterval.Second, date1, Today) Then
        '                            dtable.Rows.Add(drow)
        '                            lblCurrencyCounter.Text = Val(lblCurrencyCounter.Text) + 1
        '                            lblCurrencyCounter.Refresh()
        '                        End If
        '                    End If
        '                End If
        '                i = i + 1
        '            End While
        '            iline.Close()
        '            objMast.Insert_Currency_Contract(dtable)
        '            If chk = True Then
        '                fill_token()
        '                MsgBox("File Import Successfully")
        '                txtCurrencyPath.Text = ""
        '                lblCurrencyCounter.Text = 0
        '                lblCurrencyCounter.Refresh()
        '            End If
        '        End If
        '    End If
        '    Me.Cursor = Cursors.Default
        'Else
        '    MsgBox("Please Select File Path....")
        'End If
        Dim flg As Boolean = validate_contract_csv_file(txtCurrencyPath.Text, "CURR")
        If flg = True Then
            MsgBox("Invalid CSV File..", MsgBoxStyle.Information)
            Return
        End If
        Me.Cursor = Cursors.WaitCursor
        If txtCurrencyPath.Text.Trim <> "" And File.Exists(txtCurrencyPath.Text) Then
            If import_Data.CurrencyImport(txtCurrencyPath.Text, ObjIO) = True Then
                fill_token()
                Me.Cursor = Cursors.Default
                MsgBox("File Imported Successfully.", MsgBoxStyle.Information)
                txtCurrencyPath.Text = ""
            Else
                Me.Cursor = Cursors.Default
                MsgBox("File Not Processed.", MsgBoxStyle.Information)
            End If
        Else
            Me.Cursor = Cursors.Default
            MsgBox("Invalid File Path.", MsgBoxStyle.Exclamation)
        End If

        If IsUnMatchTrade() Then
            Dim frm As New FrmMatchContract
            frm.ShowForm("CUR")
        End If

    End Sub

    ''' <summary>
    ''' Clear Text Box File Path FOr Currency Text Box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClearCurrency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearCurrency.Click
        txtCurrencyPath.Text = ""
    End Sub

    Private Sub import_master_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ObjIO = Nothing
    End Sub
    Public Shared Function DownloadFile(ByVal FileNameToDownload As String) As String

        'Dim ftpURL As String = "ftp://strategybuilder.finideas.com/Contract"
        ''Host URL or address of the FTP serve
        'Dim userName As String = "strategybuilder" ' "FI-strategybuilder"
        ''User Name of the FTP server
        'Dim password As String = "finideas#123"
        Dim tempDirPath As String = "" 'Application.StartupPath + "\Contract\"
        If flgcsvcontract = True Then
            tempDirPath = Application.StartupPath + "\Contractcsv\"
        Else
            tempDirPath = Application.StartupPath + "\Contract\"
        End If

        'Dim ResponseDescription As String = ""
        Dim PureFileName As String = New FileInfo(FileNameToDownload).Name
        'Dim DownloadedFilePath As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName
        'Dim downloadUrl As String = [String].Format("{0}/{1}", FtpUrl, FileNameToDownload)
        'Dim req As FtpWebRequest = DirectCast(FtpWebRequest.Create(downloadUrl), FtpWebRequest)
        'req.Method = WebRequestMethods.Ftp.DownloadFile
        'req.Credentials = New NetworkCredential(userName, password)
        'req.UseBinary = True
        'req.Proxy = Nothing
        'Try
        '    Dim response As FtpWebResponse = DirectCast(req.GetResponse(), FtpWebResponse)
        '    Dim stream As Stream = response.GetResponseStream()
        '    Dim buffer As Byte() = New Byte(2047) {}
        '    Dim fs As New FileStream(DownloadedFilePath, FileMode.Create)
        '    Dim ReadCount As Integer = stream.Read(buffer, 0, buffer.Length)
        '    While ReadCount > 0
        '        fs.Write(buffer, 0, ReadCount)
        '        ReadCount = stream.Read(buffer, 0, buffer.Length)
        '    End While
        '    ResponseDescription = response.StatusDescription
        '    fs.Close()
        '    stream.Close()
        'Catch e As Exception
        '    Console.WriteLine(e.Message)
        'End Try
        'Return ResponseDescription







        Try
            Dim url As String = ""

            'Dim filename As String = Convert.ToString(tempDirPath) & PureFileName
            Dim filename As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName
            If flgcsvcontract = True Then
                url = Convert.ToString("https://support.finideas.com/contractcsv/") & FileNameToDownload
            Else
                url = Convert.ToString("https://support.finideas.com/contract/") & FileNameToDownload
            End If

            WriteLog("contract URL..." + url)
            WriteLog("File name..." + filename)



            Dim client As New WebClient()
            Dim uri As New Uri(url)
            client.Headers.Add("Accept: text/html, application/xhtml+xml, */*")
            client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)")
            client.DownloadFileAsync(uri, filename)

            While client.IsBusy
                System.Threading.Thread.Sleep(1000)

            End While
            'Console.WriteLine(ex.Message);


        Catch ex As UriFormatException
            WriteLog("contract error..." + ex.ToString())
        End Try
        Return ""
    End Function



    Private Sub cmdsave2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave2.Click
        '        'Dim downloader As New FileDownloader()
        '        Me.Cursor = Cursors.WaitCursor
        '        If Not Directory.Exists(Application.StartupPath & "\Contract\") Then
        '            'Directory.Delete(Application.StartupPath & "\Contract\", True)
        '            Directory.CreateDirectory(Application.StartupPath & "\Contract\")
        '        End If


        '        Dim StrContract As String = Application.StartupPath & "\Contract\contract.zip"
        '        Dim strUrl As String = "http://www.finideas.com/FinIdeasBackUp/contract.zip"
        '        Dim StrContracttxt As String = Application.StartupPath & "\Contract\contract.txt"

        '        If File.Exists(StrContract) Then
        '            File.Delete(StrContract)
        '        End If

        '        'downloader.Download(strUrl, Application.StartupPath & "\Contract\")
        '        DownloadAsyncCore(New System.Uri(strUrl), Application.StartupPath & "\Contract\")

        '        'Dim sc As New Shell32.Shell()
        '        ''Declare the folder where the files will be extracted
        '        'Dim output As Shell32.Folder = sc.NameSpace(Application.StartupPath & "\Contract")
        '        ''Declare your input zip file as folder  .

        'back:
        '        Threading.Thread.Sleep(1000)
        '        If File.Exists(StrContract) Then
        '            txtpath.Text = "Zip Downloaded."
        '        Else
        '            GoTo back
        '        End If
        'back2:
        '        Threading.Thread.Sleep(1000)
        '        Try
        '            'Dim input As Shell32.Folder = sc.NameSpace(StrContract)
        '            ''Extract the files from the zip file using the CopyHere command .
        '            'output.CopyHere(input.Items, 4)

        '            ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)




        '        Catch ex As Exception
        '            GoTo back
        '        End Try

        '        If File.Exists(StrContracttxt) Then
        '            txtpath.Text = StrContracttxt
        '            Call cmdsave_Click(sender, e)
        '        Else
        '            GoTo back2
        '        End If
        '        Me.Cursor = Cursors.Default






        'Dim downloader As New FileDownloader()
        Me.Cursor = Cursors.WaitCursor
        Dim StrContract As String = ""
        Dim strUrl As String = ""
        Dim StrContracttxt As String = ""
        If flgcsvcontract = True Then
            If Not Directory.Exists(Application.StartupPath & "\Contractcsv\") Then
                'Directory.Delete(Application.StartupPath & "\Contract\", True)
                Directory.CreateDirectory(Application.StartupPath & "\Contractcsv\")
            End If
            StrContract = Application.StartupPath & "\Contractcsv\contract.zip"
            strUrl = "http://www.finideas.com/FinIdeasBackUp/contract.zip"
            StrContracttxt = Application.StartupPath & "\Contractcsv\contract.csv"

        Else
            If Not Directory.Exists(Application.StartupPath & "\Contract\") Then
                'Directory.Delete(Application.StartupPath & "\Contract\", True)
                Directory.CreateDirectory(Application.StartupPath & "\Contract\")
            End If
            StrContract = Application.StartupPath & "\Contract\contract.zip"
            strUrl = "http://www.finideas.com/FinIdeasBackUp/contract.zip"
            StrContracttxt = Application.StartupPath & "\Contract\contract.txt"

        End If
       


      
        If File.Exists(StrContract) Then
            File.Delete(StrContract)
        End If

        If File.Exists(StrContracttxt) Then
            File.Delete(StrContracttxt)
        End If
        'DownloadAsyncCore(New System.Uri(strUrl), Application.StartupPath & "\Contract\")
        Dim FileNameToDownload As String = "contract.zip"
        DownloadFile(FileNameToDownload)

        WriteLog("contract downloaded...")


back:
        Threading.Thread.Sleep(1000)
        If File.Exists(StrContract) Then
            txtpath.Text = "Zip Downloaded."
        Else
            GoTo back
        End If
back2:
        Threading.Thread.Sleep(1000)
        Try


            'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
            flgimportContract = True
            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
            flgimportContract = False

        Catch ex As Exception
            GoTo back
        End Try
        Dim info2 As New FileInfo(StrContracttxt)
        Dim length2 As Long = info2.Length
        Dim lineCnt As Integer = CUtils.GetCsvRowCount(StrContracttxt)

        If File.Exists(StrContracttxt) And length2 > 10 And lineCnt > 4 Then
            'If File.Exists(StrContracttxt) Then
            txtpath.Text = StrContracttxt
            Call cmdsave_Click(sender, e)
        ElseIf length2 = 0 Then
            MsgBox("Contract not process. Please try again later.")
            'Me.Close()
            Exit Sub
        Else
            GoTo back2
        End If
        Me.Cursor = Cursors.Default


    End Sub
    'Private Sub asdf()
    '    '//This stores the path where the file should be unzipped to,
    '    '//including any subfolders that the file was originally in.
    '    Dim fileUnzipFullPath As String

    '    '//This is the full name of the destination file including
    '    '//the path
    '    Dim fileUnzipFullName As String


    '    '//Opens the zip file up to be read
    '    using (ZipArchive archive = ZipFile.OpenRead(zipName))

    '        '//Loops through each file in the zip file
    '        For Each file As ZipArchiveEntry In archive.Entries

    '            '//Outputs relevant file information to the console
    '            Console.WriteLine("File Name: {0}", file.Name)
    '            Console.WriteLine("File Size: {0} bytes", file.Length)
    '            Console.WriteLine("Compression Ratio: {0}", (Convert.ToDouble(file.CompressedLength / file.Length).ToString("0.0%")))

    '            '//Identifies the destination file name and path
    '            fileUnzipFullName = Path.Combine(dirToUnzipTo, file.FullName)

    '            '//Extracts the files to the output folder in a safer manner
    '            If (!System.IO.File.Exists(fileUnzipFullName)) Then

    '                '//Calculates what the new full path for the unzipped file should be
    '                fileUnzipFullPath = Path.GetDirectoryName(fileUnzipFullName)

    '                '//Creates the directory (if it doesn't exist) for the new path
    '                Directory.CreateDirectory(fileUnzipFullPath)

    '                '//Extracts the file to (potentially new) path
    '                file.ExtractToFile(fileUnzipFullName)
    '            End If
    '        Next
    '    End Using
    'End Sub
    Private m_proxy As IWebProxy
    Private sdisposed As Boolean
    Private m_credentials As ICredentials
    Private m_preAuthenticate As Boolean
    Private m_downloadHtml As Boolean
    Private m_userAgent As String = Nothing
    Private m_downloadingTo As String
    Private m_overwrite As Boolean
    Private m_blockSize As Integer = 1024

    Public Property Proxy() As IWebProxy
        Get
            Me.CheckDisposed()
            Return Me.m_proxy
        End Get
        Set(ByVal value As IWebProxy)
            Me.CheckDisposed()
            Me.m_proxy = value
        End Set
    End Property
    Public Property Credentials() As ICredentials
        Get
            Me.CheckDisposed()
            Return Me.m_credentials
        End Get
        Set(ByVal value As ICredentials)
            Me.CheckDisposed()
            Me.m_credentials = value
        End Set
    End Property

    Public Property PreAuthenticate() As Boolean
        Get
            Me.CheckDisposed()
            Return Me.m_preAuthenticate
        End Get
        Set(ByVal value As Boolean)
            Me.CheckDisposed()
            Me.m_preAuthenticate = value
        End Set
    End Property

    Private Sub CheckDisposed()
        If Me.sdisposed Then
            Throw New ObjectDisposedException(MyBase.[GetType]().FullName)
        End If
    End Sub

    Public Property DownloadHtml() As Boolean
        Get
            Me.CheckDisposed()
            Return Me.m_downloadHtml
        End Get
        Set(ByVal value As Boolean)
            Me.CheckDisposed()
            Me.m_downloadHtml = value
        End Set
    End Property

    Public ReadOnly Property DownloadingTo() As String
        Get
            Me.CheckDisposed()
            Return Me.m_downloadingTo
        End Get
    End Property


    Private Sub DownloadAsyncCore(ByVal strUrl As Uri, ByVal strDest As String)
        Dim buffer As Byte() = Nothing
        Dim url As Uri = strUrl 'TryCast("{" + strUrl + "}", Uri)
        Dim destinationFolder As String = TryCast(strDest, String)

        Using downloadInfo As New DownloadInfo()
            downloadInfo.Proxy = Me.Proxy
            downloadInfo.Credentials = Me.Credentials
            downloadInfo.PreAuthenticate = Me.PreAuthenticate
            downloadInfo.DownloadHtml = Me.DownloadHtml
            downloadInfo.UserAgent = Me.m_userAgent

            downloadInfo.Initialize(url, destinationFolder)

            ' Find out the name of the file that the web server gave us.
            Dim destFileName As String = Path.GetFileName(downloadInfo.Response.ResponseUri.ToString())
            Me.m_downloadingTo = Path.GetFullPath(Path.Combine(destinationFolder, destFileName))

            If Me.m_overwrite Then
                ' This is safe because no exceptions will be thrown
                ' if the file doesn't exist.
                File.Delete(Me.m_downloadingTo)
            End If

            ' Create the file on disk here, so even if we don't receive any data of the file
            ' it's still on disk. This allows us to download 0-byte files.
            If Not File.Exists(Me.m_downloadingTo) Then
                Dim fs As FileStream = File.Create(Me.m_downloadingTo)
                fs.Close()
            End If

            ' create the download buffer
            buffer = New Byte(Me.m_blockSize - 1) {}

            Dim readCount As Integer

            ' update how many bytes have already been read
            Dim totalDownloaded As Long = downloadInfo.StartPoint

            While CInt(InlineAssignHelper(readCount, downloadInfo.DownloadStream.Read(buffer, 0, Me.m_blockSize))) > 0
                ' break on cancel
                'If worker.CancellationPending Then
                '    e.Cancel = True
                '    downloadInfo.Close()

                '    Exit While
                'End If

                ' update total bytes read
                totalDownloaded += readCount

                ' save block to end of file
                SaveToFile(buffer, readCount, Me.m_downloadingTo)

                ' send progress info
                If downloadInfo.IsProgressKnown Then
                    Dim progress As Integer = CInt(Math.Truncate((CDbl(totalDownloaded) / downloadInfo.Length) * 100))
                    'worker.ReportProgress(progress, New Long() {totalDownloaded, downloadInfo.Length})
                End If
            End While
        End Using
        ' e.Result = m_downloadingTo

    End Sub
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function
    Private Shared Sub SaveToFile(ByVal buffer As Byte(), ByVal count As Integer, ByVal fileName As String)
        Using stream As FileStream = File.Open(fileName, FileMode.Append, FileAccess.Write)
            stream.Write(buffer, 0, count)
        End Using
    End Sub

    Private Sub cmdeqimport2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdeqimport2.Click
        'Dim downloader As New FileDownloader()
        '        Me.Cursor = Cursors.WaitCursor
        '        If Not Directory.Exists(Application.StartupPath & "\Contract\") Then
        '            'Directory.Delete(Application.StartupPath & "\Contract\", True)
        '            Directory.CreateDirectory(Application.StartupPath & "\Contract\")
        '        End If

        '        Dim StrContract As String = Application.StartupPath & "\Contract\security.zip"
        '        Dim strUrl As String = "http://www.finideas.com/FinIdeasBackUp/security.zip"
        '        Dim StrContracttxt As String = Application.StartupPath & "\Contract\security.txt"

        '        If File.Exists(StrContract) Then
        '            File.Delete(StrContract)
        '        End If

        '        'downloader.Download(strUrl, Application.StartupPath & "\Contract\")
        '        DownloadAsyncCore(New System.Uri(strUrl), Application.StartupPath & "\Contract\")

        '        'Dim sc As New Shell32.Shell()
        '        ''Declare the folder where the files will be extracted
        '        'Dim output As Shell32.Folder = sc.NameSpace(Application.StartupPath & "\Contract")
        '        ''Declare your input zip file as folder  .
        'back:
        '        Threading.Thread.Sleep(1000)
        '        If File.Exists(StrContract) Then
        '            txteqpath.Text = "Zip Downloaded."
        '        Else
        '            GoTo back
        '        End If
        'back2:
        '        Threading.Thread.Sleep(1000)
        '        Try
        '            'Dim input As Shell32.Folder = sc.NameSpace(StrContract)
        '            ''Extract the files from the zip file using the CopyHere command .
        '            'output.CopyHere(input.Items, 4)
        '            ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)

        '        Catch ex As Exception
        '            GoTo back
        '        End Try

        '        If File.Exists(StrContracttxt) Then
        '            txteqpath.Text = StrContracttxt
        '            Call cmdeqimport_Click(sender, e)
        '        Else
        '            GoTo back2
        '        End If

        '        Me.Cursor = Cursors.Default

        'objimpmaster.EQ(StrContracttxt)




        Dim downloader As New FileDownloader()
        Me.Cursor = Cursors.WaitCursor
        Dim StrContract As String = ""
        Dim strUrl As String = ""
        Dim StrContracttxt As String = ""
        If flgcsvcontract = True Then
            If Not Directory.Exists(Application.StartupPath & "\Contractcsv\") Then
                'Directory.Delete(Application.StartupPath & "\Contract\", True)
                Directory.CreateDirectory(Application.StartupPath & "\Contractcsv\")
            End If

            StrContract = Application.StartupPath & "\Contractcsv\security.zip"
            strUrl = "http://www.finideas.com/FinIdeasBackUp/security.zip"
            StrContracttxt = Application.StartupPath & "\Contractcsv\security.csv"
        Else
            If Not Directory.Exists(Application.StartupPath & "\Contract\") Then
                'Directory.Delete(Application.StartupPath & "\Contract\", True)
                Directory.CreateDirectory(Application.StartupPath & "\Contract\")
            End If

            StrContract = Application.StartupPath & "\Contract\security.zip"
            strUrl = "http://www.finideas.com/FinIdeasBackUp/security.zip"
            StrContracttxt = Application.StartupPath & "\Contract\security.txt"
        End If

       

        If File.Exists(StrContract) Then
            File.Delete(StrContract)
        End If
        If File.Exists(StrContracttxt) Then
            File.Delete(StrContracttxt)
        End If


        'DownloadAsyncCore(New System.Uri(strUrl), Application.StartupPath & "\Contract\")
        Dim FileNameToDownload As String = "security.zip"
        DownloadFile(FileNameToDownload)


back:
        Threading.Thread.Sleep(1000)
        If File.Exists(StrContract) Then
            txteqpath.Text = "Zip Downloaded."
        Else
            GoTo back
        End If
back2:
        Threading.Thread.Sleep(1000)
        Try

            'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
            flgimportContract = True
            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
            flgimportContract = False
        Catch ex As Exception
            GoTo back
        End Try

        Dim info2 As New FileInfo(StrContract)
        Dim length2 As Long = info2.Length

        If File.Exists(StrContracttxt) Then

            txteqpath.Text = StrContracttxt
            Call cmdeqimport_Click(sender, e)
        ElseIf length2 = 0 Then
            MsgBox("Contract not process. Please try again later.")
            Me.Close()
            Exit Sub
        Else
            GoTo back2
        End If

        Me.Cursor = Cursors.Default


    End Sub

    Private Sub btnImportCurrency2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportCurrency2.Click
        '        'Dim downloader As New FileDownloader()
        '        Me.Cursor = Cursors.WaitCursor
        '        If Not Directory.Exists(Application.StartupPath & "\Contract\") Then
        '            'Directory.Delete(Application.StartupPath & "\Contract\", True)
        '            Directory.CreateDirectory(Application.StartupPath & "\Contract\")
        '        End If

        '        Dim StrContract As String = Application.StartupPath & "\Contract\cd_contract.zip"
        '        Dim strUrl As String = "http://www.finideas.com/FinIdeasBackUp/cd_contract.zip"
        '        Dim StrContracttxt As String = Application.StartupPath & "\Contract\cd_contract.txt"

        '        If File.Exists(StrContract) Then
        '            File.Delete(StrContract)
        '        End If

        '        'downloader.Download(strUrl, Application.StartupPath & "\Contract\")
        '        DownloadAsyncCore(New System.Uri(strUrl), Application.StartupPath & "\Contract\")

        '        'Dim sc As New Shell32.Shell()
        '        ''Declare the folder where the files will be extracted
        '        'Dim output As Shell32.Folder = sc.NameSpace(Application.StartupPath & "\Contract")
        '        ''Declare your input zip file as folder  .
        'back:
        '        Threading.Thread.Sleep(1000)
        '        If File.Exists(StrContract) Then
        '            txtCurrencyPath.Text = "Zip Downloaded."
        '        Else
        '            GoTo back
        '        End If
        'back2:
        '        Threading.Thread.Sleep(1000)
        '        Try
        '            'Dim input As Shell32.Folder = sc.NameSpace(StrContract)
        '            ''Extract the files from the zip file using the CopyHere command .
        '            'output.CopyHere(input.Items, 4)
        '            ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
        '        Catch ex As Exception
        '            GoTo back
        '        End Try

        '        If File.Exists(StrContracttxt) Then
        '            txtCurrencyPath.Text = StrContracttxt
        '            Call btnImportCurrency_Click(sender, e)
        '        Else
        '            GoTo back2
        '        End If

        '        Me.Cursor = Cursors.Default






        Me.Cursor = Cursors.WaitCursor

        Dim StrContract As String = ""
        Dim strUrl As String = ""
        Dim StrContracttxt As String = ""
        If flgcsvcontract = True Then
            If Not Directory.Exists(Application.StartupPath & "\Contractcsv\") Then
                Directory.CreateDirectory(Application.StartupPath & "\Contractcsv\")
            End If

            StrContract = Application.StartupPath & "\Contractcsv\cd_contract.zip"
            strUrl = "http://www.finideas.com/FinIdeasBackUp/cd_contract.zip"
            StrContracttxt = Application.StartupPath & "\Contractcsv\cd_contract.csv"
        Else
            If Not Directory.Exists(Application.StartupPath & "\Contract\") Then
                Directory.CreateDirectory(Application.StartupPath & "\Contract\")
            End If

            StrContract = Application.StartupPath & "\Contract\cd_contract.zip"
            strUrl = "http://www.finideas.com/FinIdeasBackUp/cd_contract.zip"
            StrContracttxt = Application.StartupPath & "\Contract\cd_contract.txt"

        End If

      
        If File.Exists(StrContract) Then
            File.Delete(StrContract)
        End If
        If File.Exists(StrContracttxt) Then
            File.Delete(StrContracttxt)
        End If

        'DownloadAsyncCore(New System.Uri(strUrl), Application.StartupPath & "\Contract\")
        Dim FileNameToDownload As String = "cd_contract.zip"
        DownloadFile(FileNameToDownload)


back:
        Threading.Thread.Sleep(1000)
        If File.Exists(StrContract) Then
            txtCurrencyPath.Text = "Zip Downloaded."
        Else
            GoTo back
        End If
back2:
        Threading.Thread.Sleep(1000)
        Try

            'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
            flgimportContract = True
            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
            flgimportContract = False
        Catch ex As Exception
            GoTo back
        End Try

        Dim info2 As New FileInfo(StrContract)
        Dim length2 As Long = info2.Length
        If File.Exists(StrContracttxt) Then
            txtCurrencyPath.Text = StrContracttxt
            Call btnImportCurrency_Click(sender, e)
        ElseIf length2 = 0 Then
            MsgBox("Currency Contract not process. Please try again later.")
            Me.Close()
            Exit Sub
        Else
            GoTo back2
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Panel6_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub chkcsv_CheckedChanged(sender As System.Object, e As System.EventArgs)
        If FLGCSVCONTRACT = True Then
            FLGCSVCONTRACT = True
        Else
            FLGCSVCONTRACT = False
        End If
    End Sub
End Class
'Public Class ZipHelp

'    '/// <summary>
'    '/// Zip a file
'    '/// </summary>
'    '/// <param name="SrcFile">source file path</param>
'    '/// <param name="DstFile">zipped file path</param>
'    '/// <param name="BufferSize">buffer to use</param>
'    Public Shared Sub Zip(ByVal SrcFile As String, ByVal DstFile As String, ByVal BufferSize As Integer)

'        Dim fileStreamIn As FileStream = New FileStream(SrcFile, FileMode.Open, FileAccess.Read)
'        Dim fileStreamOut As FileStream = New FileStream(DstFile, FileMode.Create, FileAccess.Write)
'        Dim zipOutStream As ZipOutputStream = New ZipOutputStream(fileStreamOut)


'        Dim buffer(BufferSize) As Byte
'        '= new byte[BufferSize]

'        Dim entry As ZipEntry = New ZipEntry(Path.GetFileName(SrcFile))
'        zipOutStream.PutNextEntry(entry)

'        Dim size As Integer

'        Do While (size > 0)
'            size = fileStreamIn.Read(Buffer, 0, Buffer.Length)
'            zipOutStream.Write(Buffer, 0, size)
'        Loop


'        zipOutStream.Close()
'        fileStreamOut.Close()
'        fileStreamIn.Close()
'    End Sub

'    '/// <summary>
'    '/// UnZip a file
'    '/// </summary>
'    '/// <param name="SrcFile">source file path</param>
'    '/// <param name="DstFile">unzipped file path</param>
'    '/// <param name="BufferSize">buffer to use</param>
'    Public Shared Sub UnZip(ByVal SrcFile As String, ByVal DstFile As String, ByVal BufferSize As Integer)

'        Dim fileStreamIn As FileStream = New FileStream(SrcFile, FileMode.Open, FileAccess.Read)
'        Dim zipInStream As ZipInputStream = New ZipInputStream(fileStreamIn)
'        Dim entry As ZipEntry = zipInStream.GetNextEntry()
'        Dim fileStreamOut As FileStream = New FileStream(DstFile + "\" + entry.Name, FileMode.Create, FileAccess.Write)

'        Dim size As Integer

'        Dim buffer(BufferSize) As Byte

'        Do
'            size = zipInStream.Read(buffer, 0, buffer.Length)
'            fileStreamOut.Write(buffer, 0, size)
'        Loop While (size > 0)


'        zipInStream.Close()
'        fileStreamOut.Close()
'        fileStreamIn.Close()

'    End Sub



'End Class
