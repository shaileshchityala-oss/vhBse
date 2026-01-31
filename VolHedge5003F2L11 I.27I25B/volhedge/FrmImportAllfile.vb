Imports System.Windows.Forms
Imports System.IO
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration
Imports Microsoft.Win32
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.Net.Sockets.Socket
Imports VolHedge.DAL
Imports System.Net.NetworkInformation
Imports System.Management
Imports System.Net.Sockets.NetworkStream
Imports System.Data.OleDb
Imports VolHedge.OptionG
Imports ICSharpCode.SharpZipLib.Zip
Imports System

Imports VolHedge.UpdaterCore
Imports OpenFileDialog = System.Windows.Forms.OpenFileDialog
Imports System.Threading.Tasks

Public Class FrmImportAllfile
    Dim ObjIO As ImportData.ImportOperation
    Dim objMast As Mastergenrate = New Mastergenrate
    Private downloader As FileDownloader
    Dim spanfiledownload As String
    Dim spanfiledownloadzip As String
    Dim aelfiledownloadzip As String
    Dim downloadbhavcopy As String = ""
    Public AutoFilemsg As String = ""
    Dim trd As New trading
    Dim Mrateofinterast As Double = 0
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
        FLGCSVCONTRACT = False
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
        FLGCSVCONTRACT = False
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
        tbCtrlMain.SelectedIndex = 0
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
        CBSPAN.SelectedIndex = 0
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

    Public Async Function Testnew() As Task
        Try
            'Dim filename As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & FileNameToDownload
            Dim FileNameToDownload As String = "contract.zip"
            Dim tempDirPath As String = Application.StartupPath + "\Contract\" & FileNameToDownload

            ' Dim url As String = Convert.ToString("https://support.finideas.com/contract/") & FileNameToDownload

            Dim url As String = Convert.ToString("https://freetestdata.com/wp-content/uploads/2022/02/Free_Test_Data_5MB_AVI.avi")

            DownloadUsingMrg(PMgrKey, url, tempDirPath, lblcount)
            While Not File.Exists(tempDirPath)
                Task.Delay(500)
            End While
        Catch ex As Exception

        End Try



    End Function

    Public Async Function DownloadFile(ByVal FileNameToDownload As String) As Task(Of String)

        'Dim ftpURL As String = "ftp://strategybuilder.finideas.com/Contract"
        ''Host URL or address of the FTP serve
        'Dim userName As String = "strategybuilder" ' "FI-strategybuilder"
        ''User Name of the FTP server
        'Dim password As String = "finideas#123"
        Dim tempDirPath As String = ""
        If FLGCSVCONTRACT = True Then
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

            Dim filename As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName

            'url = Convert.ToString("https://support.finideas.com/contract/") & FileNameToDownload

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

        Catch ex As UriFormatException
            WriteLog(ex.Message)

        End Try
        Return ""
    End Function

    Public Async Function DownloadUsingMrg(pMgrKey As String, pUrl As String, ByVal pFileNameToDownload As String, pLabelProcess As Label) As Task
        Try
            clsGlobal.mDownloadMgr.EnqueueDownload(pMgrKey, pUrl, pFileNameToDownload, lblcount, 60)
            Dim res As DownloadResult = Nothing
            While Not clsGlobal.mDownloadMgr.TryGetResult("ContractFo", res)
                Await Task.Delay(500)
            End While

            MessageBox.Show("test")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Private Const PMgrKey As String = "NseContractAllImport"
    Private Sub cmdsave2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave2.Click
        FLGCSVCONTRACT = False
        If Not CUtils.IsConnectedToInternet() Then
            MessageBox.Show("Internet Not Available")
        End If

        'Testnew()
        ' MessageBox.Show("test")
        'Return

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
        'If Not Directory.Exists(Application.StartupPath & "\Contract\") Then
        '    'Directory.Delete(Application.StartupPath & "\Contract\", True)
        '    Directory.CreateDirectory(Application.StartupPath & "\Contract\")
        'End If


        'Dim StrContract As String = Application.StartupPath & "\Contract\contract.zip"
        'Dim strUrl As String = "http://www.finideas.com/FinIdeasBackUp/contract.zip"
        'Dim StrContracttxt As String = Application.StartupPath & "\Contract\contract.txt"
        Dim StrContract As String = ""
        Dim strUrl As String = ""
        Dim StrContracttxt As String = ""
        If FLGCSVCONTRACT = True Then
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
        '-----------------------------------------------------------------

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
            flgimportContract = True
            UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
            flgimportContract = False
        Catch ex As Exception
            GoTo back
        End Try
        Dim info2 As New FileInfo(StrContracttxt)
        Dim length2 As Long = info2.Length ' Check Size
        Dim lineCnt As Integer = CUtils.GetCsvRowCount(StrContracttxt)

        If File.Exists(StrContracttxt) And length2 > 10 And lineCnt > 4 Then
            txtpath.Text = StrContracttxt
            Call cmdsave_Click(sender, e)
        ElseIf length2 < 1 Or lineCnt < 3 Then
            MsgBox("Contract not process. Please try again later.")
            Me.Close()
            Exit Sub
        Else
            GoTo back2
        End If
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
        FLGCSVCONTRACT = False
        If Not CUtils.IsConnectedToInternet() Then
            MessageBox.Show("Internet Not Available")
            Return
        End If

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
        'If Not Directory.Exists(Application.StartupPath & "\Contract\") Then
        '    'Directory.Delete(Application.StartupPath & "\Contract\", True)
        '    Directory.CreateDirectory(Application.StartupPath & "\Contract\")
        'End If

        'Dim StrContract As String = Application.StartupPath & "\Contract\security.zip"
        'Dim strUrl As String = "http://www.finideas.com/FinIdeasBackUp/security.zip"
        'Dim StrContracttxt As String = Application.StartupPath & "\Contract\security.txt"
        Dim StrContract As String = ""
        Dim strUrl As String = ""
        Dim StrContracttxt As String = ""
        If FLGCSVCONTRACT = True Then
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


        Dim info2 As New FileInfo(StrContracttxt)
        Dim length2 As Long = info2.Length
        Dim lineCnt As Integer = CUtils.GetCsvRowCount(StrContracttxt)
        'If File.Exists(StrContracttxt) Then
        If File.Exists(StrContracttxt) And length2 > 10 And lineCnt > 4 Then
            txteqpath.Text = StrContracttxt
            Call cmdeqimport_Click(sender, e)
        ElseIf length2 < 1 Or lineCnt < 3 Then
            MsgBox("Contract not process. Please try again later.")
            Me.Close()
            Exit Sub
        Else
            GoTo back2
        End If

        Me.Cursor = Cursors.Default


    End Sub

    Private Sub btnImportCurrency2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportCurrency2.Click

        If Not CUtils.IsConnectedToInternet() Then
            MessageBox.Show("Internet Not Available")
            Return
        End If

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
        'If Not Directory.Exists(Application.StartupPath & "\Contract\") Then
        '    Directory.CreateDirectory(Application.StartupPath & "\Contract\")
        'End If

        'Dim StrContract As String = Application.StartupPath & "\Contract\cd_contract.zip"
        'Dim strUrl As String = "http://www.finideas.com/FinIdeasBackUp/cd_contract.zip"
        'Dim StrContracttxt As String = Application.StartupPath & "\Contract\cd_contract.txt"
        Dim StrContract As String = ""
        Dim strUrl As String = ""
        Dim StrContracttxt As String = ""
        If FLGCSVCONTRACT = True Then
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

        Dim info2 As New FileInfo(StrContracttxt)
        Dim length2 As Long = info2.Length
        Dim lineCnt As Integer = CUtils.GetCsvRowCount(StrContracttxt)

        If File.Exists(StrContracttxt) And length2 > 0 And lineCnt > 2 Then
            txtCurrencyPath.Text = StrContracttxt
            Call btnImportCurrency_Click(sender, e)
        ElseIf length2 = 0 Or lineCnt < 2 Then
            MsgBox("Currency Contract not process. Please try again later.")
            Me.Close()
            Exit Sub
        Else
            GoTo back2
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Label2_Click(sender As System.Object, e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim trd As New trading
            'Dim fnamecsv As String = "ael_" + DateTime.Now.ToString("ddMMyyyy") + ".csv"
            'Dim filepath As String = Application.StartupPath + "\" + "DownloadAELFile\" + fnamecsv
            'aelfiledownloadzip = fnamecsv
            'DownloadSpanFile(fnamecsv)
            'fnamecsv = aelfiledownloadzip
            'filepath = Application.StartupPath + "\" + "DownloadAELFile\" + fnamecsv
            'Dim fi As New FileInfo(filepath)


            If txtaelpath.Text.Trim <> "" And File.Exists(txtaelpath.Text) Then

                Dim filepath As String = txtaelpath.Text
                Dim fi As New FileInfo(filepath)
                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName

                Dim objConn As New OleDbConnection(sConnectionString)

                objConn.Open()

                'Dim objCmdSelect As New OleDbCommand
                Dim objAdapter1 As New OleDbDataAdapter("SELECT * FROM " & fi.Name, objConn)
                'objAdapter1.SelectCommand = objCmdSelect
                Dim tempdata As DataTable
                tempdata = New DataTable
                objAdapter1.Fill(tempdata)
                objConn.Close()

                '   margin_table_new.Rows.Clear()
                trd.delete_Exposure_margin_new()


                'Dim msrno As Integer
                Dim mSymbol As String
                Dim mInsType As String
                Dim mNorm_Margin As String
                Dim mAdd_Margin As String
                Dim mTotal_Margin As String

                If tempdata.Rows.Count > 0 Then
                    For Each drow As DataRow In tempdata.Rows

                        mSymbol = CStr(drow(1))
                        mInsType = CStr(drow(2))
                        mNorm_Margin = Val(drow(3))
                        mAdd_Margin = Val(drow(4))
                        mTotal_Margin = Val(drow(5))

                        'Dim chk As Boolean = False
                        'For Each mrow As DataRow In margin_table_new.Select("Symbol='" & mSymbol & "'")
                        '    chk = True
                        '    Exit For
                        'Next
                        'If chk = False Then
                        trd.Insert_Exposure_margin_new(mSymbol, mInsType, Val(mNorm_Margin), Val(mAdd_Margin), Val(mTotal_Margin))
                        'Else
                        ' objTrad.update_Exposure_margin_new(mSymbol, mInsType, Val(mNorm_Margin), Val(mAdd_Margin), Val(mTotal_Margin))
                        ' End If

                    Next
                    'fill_grid()


                    ' MsgBox("AEL Import Completed.", MsgBoxStyle.Information)
                    MsgBox("AEL Import Completed.")
                    Me.Cursor = Cursors.Default
                Else
                    'MsgBox("AEL Import Failed.", MsgBoxStyle.Critical)
                    MsgBox("AEL Import Failed.")
                End If
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub DownloadSpanFile(ByVal Fname As String, ByVal Type As String)
        Dim url As String = ""
        'If Type = "FO" Then
        '    '//https://www1.nseindia.com/archives/nsccl/span/nsccl.20200106.i4.zip
        '    url = Convert.ToString("https://www1.nseindia.com/archives/nsccl/span/") & Fname
        'ElseIf Type = "CURR" Then
        '    url = Convert.ToString("https://www1.nseindia.com/archives/cd/span/") & Fname
        'End If


        If Type = "FO" Then

            'url = "ftp://strategybuilder.finideas.com/FOSpan"

            url = "https://support.finideas.com/FOSpan/"

        ElseIf Type = "CURR" Then
            'url = "ftp://strategybuilder.finideas.com/CurrSpan/"
            url = "https://support.finideas.com/CurrSpan/"

        End If


        Dim filepath As String = Application.StartupPath + "\" + "DownloadSpanFile\"


        Dim filename As String = filepath & Fname


        Dim filepathdir As String = Application.StartupPath + "\" + "DownloadSpanFile\"
        If System.IO.Directory.Exists(filepathdir) Then
            Dim directory As New System.IO.DirectoryInfo(filepathdir)


            For Each file As System.IO.FileInfo In directory.GetFiles()
                Try
                    file.Delete()

                Catch ex As Exception

                End Try

            Next
        End If

        If Not System.IO.Directory.Exists(filepathdir) Then
            System.IO.Directory.CreateDirectory(filepathdir)
        End If
        Dim i As Integer = 0
aa:
        DownloadFileFTP(Fname, url, Application.StartupPath + "\" + "DownloadSpanFile\")
        Dim info2 As New FileInfo(Application.StartupPath + "\" + "DownloadSpanFile\" + Fname)
        Dim length2 As Long = info2.Length

        If Not System.IO.File.Exists(Application.StartupPath + "\" + "DownloadSpanFile\" + Fname) Or length2 = 0 Then
            i = i + 1
            If Type = "FO" Then
                Fname = "nsccl." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i1.zip"
                spanfiledownloadzip = Fname
                'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
                spanfiledownload = "nsccl." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i01.spn"
            ElseIf Type = "CURR" Then
                Fname = "nsccl_x." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i1.zip"
                spanfiledownloadzip = Fname
                'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
                spanfiledownload = "nsccl_x." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i01.spn"
            End If
            clsGlobal.mPerf.Push("testspannsccl")
            clsGlobal.mPerf.WriteLogStr(spanfiledownload + " - -" + spanfiledownloadzip)
            clsGlobal.mPerf.Pop()
            GoTo aa
        End If

        'Dim client As New WebClient()

        'Try
        '    Dim uri As New Uri(url)
        '    'client.Headers.Add("Accept: text/html, application/xhtml+xml, */*")
        '    'client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)")
        '    client.DownloadFileAsync(uri, filename)

        '    While client.IsBusy
        '        System.Threading.Thread.Sleep(1000)

        '    End While
        '    'Console.WriteLine(ex.Message);
        'Catch eex As Exception
        '    MsgBox("Error")
        '    'Catch ex As UriFormatException
        'End Try




    End Sub
    Private Sub Button25_Click(sender As System.Object, e As System.EventArgs) Handles Button25.Click
        Try

            Me.Cursor = Cursors.WaitCursor
            Dim mSPAN_path As String
            If CBSPAN.SelectedItem = "FO" Then
                mSPAN_path = GdtSettings.Compute("max(SettingKey)", "SettingName='SPAN_path'").ToString
            Else
                mSPAN_path = GdtSettings.Compute("max(SettingKey)", "SettingName='CURRENCY SPAN PATH'").ToString
            End If



            'If CBSPAN.SelectedItem = "FO" Then
            '    mSPAN_path = GdtSettings.Compute("max(SettingKey)", "SettingName='SPAN_path'").ToString
            'Else
            '    mSPAN_path = GdtSettings.Compute("max(SettingKey)", "SettingName='CURRENCY SPAN PATH'").ToString
            'End If

            'If System.IO.Directory.Exists(mSPAN_path) Then
            '    MsgBox("Install span software..Because" + mSPAN_path + " Not Found")
            '    Return
            'End If
            If Not Directory.Exists(mSPAN_path) Then 'if not correct span software path
                MsgBox("Enter Correct Path for span in setting.")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            Dim fnamecsv As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i1.zip"
            Dim sourcefname As String = TXTSPAN.Text ' "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"

            spanfiledownload = sourcefname
            spanfiledownloadzip = fnamecsv


            'If CBSPAN.SelectedItem = "FO" Then
            '    DownloadSpanFile(fnamecsv, "FO")
            'Else
            '    DownloadSpanFile(fnamecsv, "CURR")
            'End If


            sourcefname = spanfiledownload
            fnamecsv = spanfiledownloadzip
            Dim filepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv
            Dim sourcepath As String = sourcefname ' Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
            'If File.Exists(Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv) Then 'if not correct span software path
            '    File.Delete(Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv)
            'End If
            '   ZipHelp.UnZip(filepath, Path.GetDirectoryName(filepath), 4096)
            'UnZip1(filepath, Path.GetDirectoryName(filepath), 4096)
            'sourcefname = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            ' sourcepath = Path.GetFileName(sourcefname)  ' Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
            File.Copy(sourcepath, mSPAN_path & "\" & Path.GetFileName(sourcefname), True)
            '  MsgBox("Span Fo File Updated Successfully on path=" & mSPAN_path)

            MsgBox("Span  File Updated Successfully on path=" & mSPAN_path)
            Me.Cursor = Cursors.Default

        Catch ex As Exception

            'MsgBox("Fo Span  File Update Error..")
            MsgBox("Fo Span  File Update Error..")
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub Button24_Click(sender As System.Object, e As System.EventArgs) Handles Button24.Click

        If Not CUtils.IsConnectedToInternet() Then
            MessageBox.Show("Internet Not Available")
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim mSPAN_path As String = GdtSettings.Compute("max(SettingKey)", "SettingName='SPAN_path'").ToString
            If CBSPAN.SelectedItem = "FO" Then
                mSPAN_path = GdtSettings.Compute("max(SettingKey)", "SettingName='SPAN_path'").ToString()
            Else
                mSPAN_path = GdtSettings.Compute("max(SettingKey)", "SettingName='CURRENCY SPAN PATH'").ToString
            End If
            'If System.IO.Directory.Exists(mSPAN_path) Then
            '    MsgBox("Install span software..Because" + mSPAN_path + " Not Found")
            '    Return
            'End If
            If Not Directory.Exists(mSPAN_path) Then 'if not correct span software path
                MsgBox("Enter Correct Path for span in setting.")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            Dim fnamecsv As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i1.zip"
            'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
            Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            spanfiledownload = sourcefname
            spanfiledownloadzip = fnamecsv
            If CBSPAN.SelectedItem = "FO" Then
                DownloadSpanFile(fnamecsv, "FO")
            Else
                DownloadSpanFile(fnamecsv, "CURR")
            End If
            sourcefname = spanfiledownload
            fnamecsv = spanfiledownloadzip
            Dim filepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv
            Dim sourcepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
            'If File.Exists(Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv) Then 'if not correct span software path
            '    File.Delete(Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv)
            'End If
            '   ZipHelp.UnZip(filepath, Path.GetDirectoryName(filepath), 4096)
            UnZip1(filepath, Path.GetDirectoryName(filepath), 4096)
            'sourcefname = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            sourcepath = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
            File.Copy(sourcepath, mSPAN_path & "\" & sourcefname, True)
            '  MsgBox("Span Fo File Updated Successfully on path=" & mSPAN_path)

            MsgBox("Span  File Updated Successfully on path=" & mSPAN_path)
            Me.Cursor = Cursors.Default

        Catch ex As Exception

            'MsgBox("Fo Span  File Update Error..")
            MsgBox(" Span  File Update Error..")
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.SPN)|*.spn"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TXTSPAN.Text = opfile.FileName
        End If
    End Sub

    Private Sub Button18_Click(sender As System.Object, e As System.EventArgs) Handles Button18.Click
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim fnamecsvbhav As String = TXTBHAVCOPY.Text   '"fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"
            downloadbhavcopy = fnamecsvbhav
            'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            'fo16JUN2021bhav.csv.zip()
            '  DownloadSpanFilebhavcopy(fnamecsvbhav)

            removebhavcopy()
            ' Dim str As String = Application.StartupPath + "\" + "DownloadBhavcopy\" + "fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv"


            read_filebhav(downloadbhavcopy)
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try

        Me.Cursor = Cursors.Default
    End Sub
    Public Sub DownloadSpanFile(ByVal Fname As String)
        Dim url As String = ""
        'If Type = "FO" Then
        '    '//https://www1.nseindia.com/archives/nsccl/span/nsccl.20200106.i4.zip
        '    url = Convert.ToString("https://www1.nseindia.com/archives/nsccl/span/") & Fname
        'ElseIf Type = "CURR" Then
        '    url = Convert.ToString("https://www1.nseindia.com/archives/cd/span/") & Fname
        'End If



        'url = "ftp://strategybuilder.finideas.com/AEL/"
        url = "https://support.finideas.com/AEL/"



        Dim filepath As String = Application.StartupPath + "\" + "DownloadAELFile\"


        Dim filename As String = filepath & Fname


        Dim filepathdir As String = Application.StartupPath + "\" + "DownloadAELFile\"
        If System.IO.Directory.Exists(filepathdir) Then
            Dim directory As New System.IO.DirectoryInfo(filepathdir)


            For Each file As System.IO.FileInfo In directory.GetFiles()
                Try
                    file.Delete()

                Catch ex As Exception

                End Try

            Next
        End If

        If Not System.IO.Directory.Exists(filepathdir) Then
            System.IO.Directory.CreateDirectory(filepathdir)
        End If

        Dim i As Integer = 0
aa:
        DownloadFileFTP(Fname, url, Application.StartupPath + "\" + "DownloadAELFile\")
        Dim info2 As New FileInfo(Application.StartupPath + "\" + "DownloadAELFile\" + Fname)
        Dim length2 As Long = info2.Length

        If Not System.IO.File.Exists(Application.StartupPath + "\" + "DownloadAELFile\" + Fname) Or length2 = 0 Then
            i = i + 1

            'Fname = "nsccl." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i1.zip"
            Fname = "ael_" + DateTime.Now.AddDays(-1 * i).ToString("ddMMyyyy") + ".csv"
            aelfiledownloadzip = Fname
            'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
            'spanfiledownload = "nsccl." + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd") + ".i01.spn"


            GoTo aa
        End If





    End Sub
    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click

        If Not CUtils.IsConnectedToInternet() Then
            MessageBox.Show("Internet Not Available")
            Return
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim trd As New trading
            Dim fnamecsv As String = "ael_" + DateTime.Now.ToString("ddMMyyyy") + ".csv"
            Dim filepath As String = Application.StartupPath + "\" + "DownloadAELFile\" + fnamecsv
            aelfiledownloadzip = fnamecsv
            DownloadSpanFile(fnamecsv)
            fnamecsv = aelfiledownloadzip
            filepath = Application.StartupPath + "\" + "DownloadAELFile\" + fnamecsv
            Dim fi As New FileInfo(filepath)

            Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName

            Dim objConn As New OleDbConnection(sConnectionString)

            objConn.Open()

            'Dim objCmdSelect As New OleDbCommand
            Dim objAdapter1 As New OleDbDataAdapter("SELECT * FROM " & fi.Name, objConn)
            'objAdapter1.SelectCommand = objCmdSelect
            Dim tempdata As DataTable
            tempdata = New DataTable
            objAdapter1.Fill(tempdata)
            objConn.Close()

            '   margin_table_new.Rows.Clear()
            trd.delete_Exposure_margin_new()
            'Dim msrno As Integer
            Dim mSymbol As String
            Dim mInsType As String
            Dim mNorm_Margin As String
            Dim mAdd_Margin As String
            Dim mTotal_Margin As String

            If tempdata.Rows.Count > 0 Then
                For Each drow As DataRow In tempdata.Rows

                    mSymbol = CStr(drow(1))
                    mInsType = CStr(drow(2))
                    mNorm_Margin = Val(drow(3))
                    mAdd_Margin = Val(drow(4))
                    mTotal_Margin = Val(drow(5))

                    'Dim chk As Boolean = False
                    'For Each mrow As DataRow In margin_table_new.Select("Symbol='" & mSymbol & "'")
                    '    chk = True
                    '    Exit For
                    'Next
                    'If chk = False Then
                    trd.Insert_Exposure_margin_new(mSymbol, mInsType, Val(mNorm_Margin), Val(mAdd_Margin), Val(mTotal_Margin))
                    'Else
                    ' objTrad.update_Exposure_margin_new(mSymbol, mInsType, Val(mNorm_Margin), Val(mAdd_Margin), Val(mTotal_Margin))
                    ' End If

                Next
                'fill_grid()


                ' MsgBox("AEL Import Completed.", MsgBoxStyle.Information)
                MsgBox("AEL Import Completed.")
            Else
                'MsgBox("AEL Import Failed.", MsgBoxStyle.Critical)
                MsgBox("AEL Import Failed.")
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.CSV)|*.csv"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtaelpath.Text = opfile.FileName
        End If
    End Sub

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.CSV)|*.csv"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TXTBHAVCOPY.Text = opfile.FileName
        End If
    End Sub
    Public Sub DownloadSpanFilebhavcopy(ByVal Fname As String)
        Try


            Dim url As String = ""
            'If Type = "FO" Then
            '    '//https://www1.nseindia.com/archives/nsccl/span/nsccl.20200106.i4.zip
            '    url = Convert.ToString("https://www1.nseindia.com/archives/nsccl/span/") & Fname
            'ElseIf Type = "CURR" Then
            '    url = Convert.ToString("https://www1.nseindia.com/archives/cd/span/") & Fname
            'End If


            'url = "ftp://strategybuilder.finideas.com/Bhavcopy"
            url = "https://support.finideas.com/Bhavcopy/"



            Dim filepath As String = Application.StartupPath + "\" + "DownloadBhavcopy\"


            Dim filename As String = filepath & Fname


            Dim filepathdir As String = Application.StartupPath + "\" + "DownloadBhavcopy\"
            If System.IO.Directory.Exists(filepathdir) Then
                Dim directory As New System.IO.DirectoryInfo(filepathdir)


                For Each file As System.IO.FileInfo In directory.GetFiles()
                    Try
                        file.Delete()

                    Catch ex As Exception

                    End Try

                Next
            End If

            If Not System.IO.Directory.Exists(filepathdir) Then
                System.IO.Directory.CreateDirectory(filepathdir)
            End If
            Dim i As Integer = 1
            Dim sourcefname As String = ""
            If NEW_BHAVCOPY = 1 Then
                sourcefname = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"
            Else
                sourcefname = "fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"

            End If

aa:
            DownloadFileFTP(Fname, url, Application.StartupPath + "\" + "DownloadBhavcopy\")
            Dim info2 As New FileInfo(Application.StartupPath + "\" + "DownloadBhavcopy\" + sourcefname)
            Dim length2 As Long = info2.Length
            'Dim filepath1 As String = Application.StartupPath + "\" + "DownloadBhavcopy\" + fnamecsv
            Dim sourcepath As String = Application.StartupPath + "\" + "DownloadBhavcopy\" + sourcefname
            If Not System.IO.File.Exists(sourcepath) Or length2 = 0 Then
                i = i + 1
                If NEW_BHAVCOPY = 1 Then
                    Fname = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"
                    sourcefname = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"

                Else

                    Fname = "fo" + DateTime.Now.AddDays(-1 * i).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"
                    sourcefname = "fo" + DateTime.Now.AddDays(-1 * i).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"
                End If
                GoTo aa


                'Else
                '    Dim info2 As New FileInfo(Application.StartupPath + "\" + "DownloadBhavcopy\" + sourcefname)
                '    Dim length2 As Long = info2.Length
                '    If length2 = 0 Then
                '        i = i + 1
                '        Fname = "fo" + DateTime.Now.AddDays(-1 * i).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"
                '        sourcefname = "fo" + DateTime.Now.AddDays(-1 * i).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"

                '        GoTo aa
                '    End If
            End If

            downloadbhavcopy = Fname.Replace(".zip", "")
            ' ZipHelp.UnZip(filepath, Path.GetDirectoryName(filepath), 4096)
            UnZip1(sourcepath, Path.GetDirectoryName(sourcepath), 4096)
            sourcepath = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname

            'Dim client As New WebClient()

            'Try
            '    Dim uri As New Uri(url)
            '    'client.Headers.Add("Accept: text/html, application/xhtml+xml, */*")
            '    'client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)")
            '    client.DownloadFileAsync(uri, filename)

            '    While client.IsBusy
            '        System.Threading.Thread.Sleep(1000)

            '    End While
            '    'Console.WriteLine(ex.Message);
            'Catch eex As Exception
            '    MsgBox("Error")
            '    'Catch ex As UriFormatException
            'End Try


        Catch ex As Exception
            MsgBox("Bhavcopy Update Error..")
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub Button17_Click(sender As System.Object, e As System.EventArgs) Handles Button17.Click

        If Not CUtils.IsConnectedToInternet() Then
            MessageBox.Show("Internet Not Available")
            Return
        End If

        Try

            Me.Cursor = Cursors.WaitCursor
            Dim fnamecsvbhav As String
            If NEW_BHAVCOPY = 1 Then
                fnamecsvbhav = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"
            Else
                fnamecsvbhav = "fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"

            End If
            downloadbhavcopy = fnamecsvbhav
            'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
            'fo16JUN2021bhav.csv.zip()
            DownloadSpanFilebhavcopy(fnamecsvbhav)

            removebhavcopy()
            ' Dim str As String = Application.StartupPath + "\" + "DownloadBhavcopy\" + "fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv"


            read_filebhav(Application.StartupPath + "\" + "DownloadBhavcopy\" + downloadbhavcopy)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try


    End Sub
    Public Sub removebhavcopy()
        Dim Objbhavcopy1 As New bhav_copy
        GdtBhavcopy = Objbhavcopy1.select_data()
        Dim dt As DataTable = New DataView(GdtBhavcopy, "", "entry_date ASC", DataViewRowState.CurrentRows).ToTable(True, "entry_date")
        If dt.Rows.Count >= BHAVCOPYPROCESSDAY Then

            For i As Integer = 0 To dt.Rows.Count - BHAVCOPYPROCESSDAY - 1
                Dim entrydatebhav As Date = CDate(dt.Rows(i)(0))
                RemoveBhavcopy(entrydatebhav)
            Next

        End If
    End Sub
    Public Function RemoveBhavcopy(ByVal entry_date As Date) As DataTable
        Dim SP_delete_bhavcopy_Date As String = "delete_bhavcopy_Date"
        Try
            data_access.ParamClear()
            data_access.AddParam("@date1", OleDbType.Date, 50, CDate(entry_date))
            data_access.Cmd_Text = SP_delete_bhavcopy_Date
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            '  MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Private Sub read_filebhav(ByVal path As String)
        Me.Cursor = Cursors.WaitCursor
        REM Change In processing Bhavcopy and selecting parameters, Decimal Strike Prices are properly displayed for the records 
        Dim tempdata As DataTable
        Dim objTrad As trading = New trading
        Dim Mrateofinterast As Double = 0
        Dim DtBCP As DataTable
        Try
            Dim fpath As String
            fpath = CStr(path)
            Mrateofinterast = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
            If fpath <> "" Then
                Dim fi As New FileInfo(fpath)
                Dim dv As DataView
                tempdata = New DataTable
                DtBCP = New DataTable
                tempdata.Columns.Add("script")
                tempdata.Columns.Add("vol")
                tempdata.Columns.Add("futval")
                tempdata.Columns.Add("mt")
                tempdata.Columns.Add("iscall")
                tempdata.AcceptChanges()

                'Call Proc_Data_FromBhavCopyCsv(fpath)
                Dim impBHav As ImportData.ImportOperation
                impBHav = New ImportData.ImportOperation
                If NEW_BHAVCOPY = 1 Then
                    import_Data.CopyToData(fpath, "BHAVCOPYNEW")
                Else
                    import_Data.CopyToData(fpath, "BHAVCOPY")
                End If
                Call impBHav.ImportBhavCopy()

                impBHav = Nothing
                Dim Objbhavcopy1 As New bhav_copy
                DtBCP = Objbhavcopy1.select_TblBhavCopy()

                tempdata.Merge(DtBCP)
                '-----------------------------------------------------------------------
                '' ''Dim fi As New FileInfo(fpath)
                ' ''Dim dv As DataView
                ' ''Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
                '' ''Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;IMEX=1;Data Source=" & fi.DirectoryName
                '' '';HDR=Yes;FMT=Delimited

                ' ''Dim objConn As New OleDbConnection(sConnectionString)

                ' ''objConn.Open()

                ' ''Dim objCmdSelect As New OleDbCommand("SELECT INSTRUMENT,SYMBOL,EXPIRY_DT,STRIKE_PR,OPTION_TYP,SETTLE_PR,CONTRACTS,VAL_INLAKH,TIMESTAMP FROM " & fi.Name, objConn)

                ' ''Dim objAdapter1 As New OleDbDataAdapter

                ' ''objAdapter1.SelectCommand = objCmdSelect

                ' ''tempdata = New DataTable
                ' ''tempdata.Columns.Add("script")
                ' ''tempdata.Columns.Add("vol")
                ' ''tempdata.Columns.Add("futval")
                ' ''tempdata.Columns.Add("mt")
                ' ''tempdata.Columns.Add("iscall")
                ' ''tempdata.AcceptChanges()

                '-----------------------------------------------------------------------

                Dim mt As Double
                Dim futval As Double
                Dim iscall As Boolean
                Dim drow As DataRow
                'objAdapter1.Fill(tempdata)
                'objConn.Close()

                'dv = New DataView(tempdata, "OPTION_TYP='XX'", "", DataViewRowState.OriginalRows)
                dv = New DataView(tempdata, "option_typ='XX'", "", DataViewRowState.CurrentRows)
                'dv = New DataView(Dt, "OPTION_TYP='XX'", "", DataViewRowState.OriginalRows)
                Dim str(2) As String
                str(0) = "EXPIRY_DT"
                str(1) = "SETTLE_PR"
                str(2) = "SYMBOL"
                Dim tdata As New DataTable
                tdata = dv.ToTable(True, str)
                Dim row As DataRow
                Dim script As String
                For Each drow In tempdata.Rows
                    If drow("option_typ") = "XX" Then
                        script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy")
                        drow("script") = UCase(script.Trim)
                        drow("vol") = 0
                        drow("futval") = 0
                        drow("mt") = 0
                    Else
                        script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy") & "  " & Format(Val(drow("STRIKE_PR")), "###0.00") & "  " & drow("OPTION_TYP")
                        'script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(drow("EXPIRY_DT"), "ddMMMyyyy") & "  " & Format(Val(drow("STRIKE_PR")), "####.##") & "  " & drow("OPTION_TYP")
                        drow("script") = UCase(script.Trim)
                        futval = 0
                        drow("vol") = 0
                        For Each row In tdata.Select(" EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' ")
                            futval = row("SETTLE_PR")
                        Next
                        'futval = Val(tempdata.Compute("Max(SETTLE_PR)", " EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' And option_typ='XX'").ToString() & "")
                        'row("SETTLE_PR")

                        If Mid(drow("OPTION_TYP"), 1, 1) = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        Dim ccdate As Date = CDate(drow("TIMESTAMP").ToString.Replace("-", "/"))
                        mt = DateDiff(DateInterval.Day, ccdate.Date, CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")).Date)
                        If ccdate.Date = CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")).Date Then
                            mt = 0.5
                        End If
                        If mt = 0 Then
                            mt = 0.0001
                        Else
                            mt = (mt) / 365
                        End If
                        If futval > 0 Then
                            drow("vol") = Vol(futval, Val(drow("STRIKE_PR")), Val(drow("SETTLE_PR")), mt, iscall, True) * 100
                        End If
                        drow("futval") = futval
                        drow("mt") = mt
                        drow("iscall") = iscall
                    End If
                Next
                tempdata.AcceptChanges()
                Objbhavcopy1.insertNew(tempdata)

                GdtBhavcopy = Objbhavcopy1.select_data()
                GVarIsNewBhavcopy = True
                BhavCopyFlag = True

                Dim Item As DictionaryEntry
                Dim ArrFKey As New ArrayList
                Dim ArrCPKey As New ArrayList
                Dim ArrEKey As New ArrayList
                Dim VaLLTPPrice As New Double
                For Each Item In fltpprice
                    ArrFKey.Add(Item.Key)
                Next
                For Each Item In ltpprice
                    ArrCPKey.Add(Item.Key)
                Next
                For Each Item In eltpprice
                    ArrEKey.Add(Item.Key)
                Next
                For i As Integer = 0 To ArrFKey.Count - 1
                    If cpfmaster.Select("token=" & ArrFKey(i) & "").Length > 0 Then
                        Dim VarScript As String = cpfmaster.Select("token=" & ArrFKey(i) & "")(0).Item("Script")
                        If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                            fltpprice(ArrFKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        End If
                    End If
                Next
                For i As Integer = 0 To ArrCPKey.Count - 1
                    If cpfmaster.Select("token=" & ArrCPKey(i) & "").Length > 0 Then
                        Dim VarScript As String = cpfmaster.Select("token=" & ArrCPKey(i) & "")(0).Item("Script")
                        If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                            fltpprice(ArrCPKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        End If
                    End If
                Next
                For i As Integer = 0 To ArrEKey.Count - 1
                    If eqmaster.Select("token=" & ArrEKey(i) & "").Length > 0 Then
                        Dim VarScript As String = eqmaster.Select("token=" & ArrEKey(i) & "")(0).Item("Script")
                        If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                            fltpprice(ArrEKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        End If
                    End If
                Next

                If analysis.chkanalysis = True Then
                    Call analysis.AssignBhavcopyLTP(True)
                End If
                'MsgBox("Bhavcopy Processed Successfully.", MsgBoxStyle.Information)
                MsgBox("Bhavcopy Processed Successfully.")
            End If
        Catch ex As Exception

            'MsgBox("Bhavcopy Not Processed.", MsgBoxStyle.Information)
            MsgBox("Bhavcopy Not Processed.")
            'MsgBox("Select valid file")
            '/MsgBox(ex.ToString)
        End Try
    End Sub
    Private Function Vol(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal _mt As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean) As Double

        Dim tmpcpprice As Double = 0
        Dim mVolatility As Double
        tmpcpprice = cpprice
        'IF MATURITY IS 0 THEN _MT = 0.00001 
        mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
        Return mVolatility
    End Function

    Private Sub Button26_Click(sender As System.Object, e As System.EventArgs) Handles Button26.Click
        FLGCSVCONTRACT = False
        Try

            If Not CUtils.IsConnectedToInternet() Then
                MessageBox.Show("Internet Not Available")
                Return
            End If

            AutoFilemsg = ""
            Me.Cursor = Cursors.WaitCursor
            DownloadContract()

            If chkspan.Checked = True Then
                Dim mSPAN_path As String = GdtSettings.Compute("max(SettingKey)", "SettingName='SPAN_path'").ToString
                'If System.IO.Directory.Exists(mSPAN_path) Then
                '    MsgBox("Install span software..Because" + mSPAN_path + " Not Found")
                '    Return
                'End If
                If Not Directory.Exists(mSPAN_path) Then 'if not correct span software path
                    MsgBox("Enter Correct Path for span in setting.")
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                Dim fnamecsv As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i1.zip"
                'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
                Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
                spanfiledownload = sourcefname
                spanfiledownloadzip = fnamecsv
                DownloadSpanFile(fnamecsv, "FO")
                sourcefname = spanfiledownload
                fnamecsv = spanfiledownloadzip
                Dim filepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv
                Dim sourcepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
                'If File.Exists(Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv) Then 'if not correct span software path
                '    File.Delete(Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv)
                'End If
                '   ZipHelp.UnZip(filepath, Path.GetDirectoryName(filepath), 4096)
                UnZip1(filepath, Path.GetDirectoryName(filepath), 4096)
                'sourcefname = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
                sourcepath = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
                File.Copy(sourcepath, mSPAN_path & "\" & sourcefname, True)
                '  MsgBox("Span Fo File Updated Successfully on path=" & mSPAN_path)

                AutoFilemsg = AutoFilemsg + vbNewLine + "Span Fo File Updated Successfully on path=" & mSPAN_path
                Me.Cursor = Cursors.Default
            End If

        Catch ex As Exception

            'MsgBox("Fo Span  File Update Error..")
            AutoFilemsg = AutoFilemsg + vbNewLine + "Fo Span  File Update Error.."
            Me.Cursor = Cursors.Default
        End Try


        If chkspan.Checked = True Then
            Try

                Me.Cursor = Cursors.WaitCursor
                'nsccl.20160302.i03.spn
                Dim mSPAN_path As String = GdtSettings.Compute("max(SettingKey)", "SettingName='CURRENCY SPAN PATH'").ToString
                'If System.IO.Directory.Exists(mSPAN_path) Then
                '    MsgBox("Install span software..Because" + mSPAN_path + " Not Found")
                '    Return
                'End If
                If Not Directory.Exists(mSPAN_path) Then 'if not correct span software path
                    ' MsgBox("Enter Correct Path for span in setting.")
                    AutoFilemsg = AutoFilemsg + vbNewLine + "Enter Correct Path for span in setting."
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                Dim fnamecsv As String = "nsccl_x." + DateTime.Now.ToString("yyyyMMdd") + ".i1.zip"
                'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.span"
                'Dim sourcefname As String = "nsccl.20160303.i01.spn"
                Dim sourcefname As String = "nsccl_x." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
                spanfiledownload = sourcefname
                spanfiledownloadzip = fnamecsv
                DownloadSpanFile(fnamecsv, "CURR")
                sourcefname = spanfiledownload
                fnamecsv = spanfiledownloadzip
                Dim filepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + fnamecsv
                Dim sourcepath As String = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
                ' ZipHelp.UnZip(filepath, Path.GetDirectoryName(filepath), 4096)
                UnZip1(filepath, Path.GetDirectoryName(filepath), 4096)
                ' sourcefname = "nsccl_x." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
                sourcepath = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname
                File.Copy(sourcepath, mSPAN_path & "\" & sourcefname, True)
                ' MsgBox("Currency File Updated Successfully on path=" & mSPAN_path)
                AutoFilemsg = AutoFilemsg + vbNewLine + "Currency File Updated Successfully on path=" & mSPAN_path

            Catch ex As Exception
                ' MsgBox("Currency Span  File Update Error")
                AutoFilemsg = AutoFilemsg + vbNewLine + "Currency Span  File Update Error"

            End Try
        End If


        Try
            Me.Cursor = Cursors.WaitCursor
            If chkAEL.Checked = True Then

                Dim fnamecsv As String = "ael_" + DateTime.Now.ToString("ddMMyyyy") + ".csv"
                Dim filepath As String = Application.StartupPath + "\" + "DownloadAELFile\" + fnamecsv
                aelfiledownloadzip = fnamecsv
                DownloadSpanFile(fnamecsv)
                fnamecsv = aelfiledownloadzip
                filepath = Application.StartupPath + "\" + "DownloadAELFile\" + fnamecsv
                Dim fi As New FileInfo(filepath)

                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName

                Dim objConn As New OleDbConnection(sConnectionString)

                objConn.Open()

                'Dim objCmdSelect As New OleDbCommand
                Dim objAdapter1 As New OleDbDataAdapter("SELECT * FROM " & fi.Name, objConn)
                'objAdapter1.SelectCommand = objCmdSelect
                Dim tempdata As DataTable
                tempdata = New DataTable
                objAdapter1.Fill(tempdata)
                objConn.Close()

                '   margin_table_new.Rows.Clear()
                trd.delete_Exposure_margin_new()


                'Dim msrno As Integer
                Dim mSymbol As String
                Dim mInsType As String
                Dim mNorm_Margin As String
                Dim mAdd_Margin As String
                Dim mTotal_Margin As String

                If tempdata.Rows.Count > 0 Then
                    For Each drow As DataRow In tempdata.Rows

                        mSymbol = CStr(drow(1))
                        mInsType = CStr(drow(2))
                        mNorm_Margin = Val(drow(3))
                        mAdd_Margin = Val(drow(4))
                        mTotal_Margin = Val(drow(5))

                        'Dim chk As Boolean = False
                        'For Each mrow As DataRow In margin_table_new.Select("Symbol='" & mSymbol & "'")
                        '    chk = True
                        '    Exit For
                        'Next
                        'If chk = False Then
                        trd.Insert_Exposure_margin_new(mSymbol, mInsType, Val(mNorm_Margin), Val(mAdd_Margin), Val(mTotal_Margin))
                        'Else
                        ' objTrad.update_Exposure_margin_new(mSymbol, mInsType, Val(mNorm_Margin), Val(mAdd_Margin), Val(mTotal_Margin))
                        ' End If

                    Next
                    'fill_grid()


                    ' MsgBox("AEL Import Completed.", MsgBoxStyle.Information)
                    AutoFilemsg = AutoFilemsg + vbNewLine + "AEL Import Completed."
                Else
                    'MsgBox("AEL Import Failed.", MsgBoxStyle.Critical)
                    AutoFilemsg = AutoFilemsg + vbNewLine + "AEL Import Failed."
                End If
            End If

            Try

                Me.Cursor = Cursors.WaitCursor
                If chkbhav.Checked = True Then

                    Dim fnamecsvbhav As String = ""
                    If NEW_BHAVCOPY = 1 Then
                        fnamecsvbhav = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"
                    Else
                        fnamecsvbhav = "fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"
                    End If
                    downloadbhavcopy = fnamecsvbhav
                    'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
                    'fo16JUN2021bhav.csv.zip()
                    DownloadSpanFilebhavcopy(fnamecsvbhav)
                    removebhavcopy()
                    ' Dim str As String = Application.StartupPath + "\" + "DownloadBhavcopy\" + "fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv"
                    read_filebhav(Application.StartupPath + "\" + "DownloadBhavcopy\" + downloadbhavcopy)
                End If
            Catch ex As Exception
                Me.Cursor = Cursors.Default
            End Try
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
        Me.Cursor = Cursors.Default
        MsgBox(AutoFilemsg, MsgBoxStyle.Information)



    End Sub
    Public Shared Function DownloadFile(ByVal FtpUrl As String, ByVal FileNameToDownload As String, ByVal userName As String, ByVal password As String, ByVal tempDirPath As String) As String
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

            Dim filename As String = Convert.ToString(tempDirPath & Convert.ToString("/")) & PureFileName

            'url = Convert.ToString("https://support.finideas.com/contract/") & FileNameToDownload

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


        Catch ex As UriFormatException
        End Try
        Return ""
    End Function
    Private Function DownloadContract()
        Dim ObjImpData As New import_Data
        Dim ObjIO As New ImportData.ImportOperation
        Me.Cursor = Cursors.WaitCursor
        'Host URL or address of the FTP serve
        Dim userName As String = "" ' "strategybuilder" '"FI-strategybuilder"
        'User Name of the FTP server
        Dim password As String = "" ' "finideas#123"
        'Password of the FTP server
        'string _ftpDirectory = "FinTesterCSV"; 
        Dim FileNameToDownload As String = ""               'The directory in FTP server where the files are present
        Dim StrContracttxt As String = ""
        Dim StrContract As String = ""
        Dim StrContractfotxt As String = ""
        Dim ftpURL As String = ""

        Dim tempDirPath As String = ""
        If chkEQ.Checked = True Then


            '  ftpURL = "ftp://strategybuilder.finideas.com/Contract"

            ftpURL = "https://support.finideas.com/Contract"
            FileNameToDownload = "Contract.zip"
            tempDirPath = Application.StartupPath + "\Contract\"
            If Not System.IO.Directory.Exists(Application.StartupPath + "\" + "Contract\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\" + "Contract")
            End If
            '-----Download file Fo---
            StrContract = Application.StartupPath & "\Contract\contract.zip"
            StrContractfotxt = Application.StartupPath & "\Contract\contract.txt"
            If File.Exists(StrContract) Then
                File.Delete(StrContract)
            End If
            If File.Exists(StrContractfotxt) Then
                File.Delete(StrContractfotxt)
            End If

            DownloadFile(ftpURL, FileNameToDownload, userName, password, tempDirPath)


back:
            Threading.Thread.Sleep(1000)
            If File.Exists(StrContract) Then
                'txtpath.Text = "Zip Downloaded."
            Else
                GoTo back
            End If
back2:
            Threading.Thread.Sleep(1000)


            Try
                'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
                UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
            Catch ex As Exception
                GoTo back
            End Try
            StrContracttxt = Application.StartupPath & "\Contract\contract.txt"

            Dim info2 As New FileInfo(StrContract)
            Dim length2 As Long = info2.Length
            If File.Exists(StrContracttxt) Then
                'txtpath.Text = StrContracttxt

                Dim bolfocon As Boolean

                flgimportContract = True
                bolfocon = import_Data.ContractImport(StrContracttxt, ObjIO, True)
                flgimportContract = False
                If bolfocon = True Then
                    AutoFilemsg = AutoFilemsg + "Fo Contract Process Successfully"
                Else
                    AutoFilemsg = AutoFilemsg + "Fo Contract Process Error"
                End If
                ' objimpmaster.fo(StrContracttxt)

            ElseIf length2 = 0 Then
                MsgBox("Contract not process. ")
                GoTo security
            Else
                GoTo back2
            End If
        End If

security:

        If chkcurr.Checked = True Then


            '-----Download file Eq---
            StrContracttxt = Application.StartupPath & "\Contract\security.txt"
            FileNameToDownload = "security.zip"
            StrContract = Application.StartupPath & "\Contract\security.zip"
            If File.Exists(StrContract) Then
                File.Delete(StrContract)
            End If
            If File.Exists(StrContracttxt) Then
                File.Delete(StrContracttxt)
            End If
            DownloadFile(ftpURL, FileNameToDownload, userName, password, tempDirPath)



            Try

                'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
                UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
            Catch ex As Exception
                GoTo back
            End Try

            Dim info2 As New FileInfo(StrContract)
            Dim length2 As Long = info2.Length
            If File.Exists(StrContracttxt) Then
                Dim bolEQcon As Boolean
                flgimportContract = True
                bolEQcon = import_Data.SecurityImport(StrContracttxt, ObjIO, True)
                flgimportContract = False
                If bolEQcon = True Then
                    AutoFilemsg = AutoFilemsg + vbNewLine + "EQ Contract Process Successfully"
                Else
                    AutoFilemsg = AutoFilemsg + vbNewLine + "EQ Contract Process Error.."
                End If
                'objimpmaster.EQ(StrContracttxt)
            ElseIf length2 = 0 Then
                MsgBox("Contract not process. ")
                GoTo currency
            Else
                GoTo back2
            End If
        End If
        Me.Cursor = Cursors.Default

currency:
        If chkAEL.Checked = True Then


            '-----Download file Curr---
            StrContracttxt = Application.StartupPath & "\Contract\cd_contract.txt"
            FileNameToDownload = "cd_contract.zip"
            StrContract = Application.StartupPath & "\Contract\cd_contract.zip"
            If File.Exists(StrContract) Then
                File.Delete(StrContract)
            End If
            If File.Exists(StrContracttxt) Then
                File.Delete(StrContracttxt)
            End If
            DownloadFile(ftpURL, FileNameToDownload, userName, password, tempDirPath)

            Try

                'ZipHelp.UnZip(StrContract, Path.GetDirectoryName(StrContract), 4096)
                UnZip1(StrContract, Path.GetDirectoryName(StrContract), 4096)
            Catch ex As Exception
                GoTo back
            End Try

            Dim info2 As New FileInfo(StrContract)
            Dim length2 As Long = info2.Length
            'Dim FileNameToDownload1 As String = "csv_Data_Put_" + Fromdate1 + ".rar"
            If File.Exists(StrContracttxt) Then
                Dim bolcurrcon As Boolean
                flgimportContract = True
                bolcurrcon = import_Data.CurrencyImport(StrContracttxt, ObjIO, True)
                If bolcurrcon = True Then
                    AutoFilemsg = AutoFilemsg + vbNewLine + "curr Contract Process Successfully"
                Else
                    AutoFilemsg = AutoFilemsg + vbNewLine + "curr Contract Process Error"
                End If
                flgimportContract = False
                'objimpmaster.CURR(StrContracttxt)
            ElseIf length2 = 0 Then
                MsgBox("curr Contract not process. ")
                Exit Function
            Else
                GoTo back2
            End If
        End If
        fill_token()
        Me.Cursor = Cursors.Default

    End Function

    Private Sub chkspan_CheckedChanged(sender As Object, e As EventArgs) Handles chkspan.CheckedChanged

    End Sub

    Private Sub chkcsv_CheckedChanged(sender As System.Object, e As System.EventArgs)
        If FLGCSVCONTRACT = True Then
            FLGCSVCONTRACT = True
        Else
            FLGCSVCONTRACT = False
        End If
    End Sub

    Private Sub btnBseContractBrowse_Click(sender As Object, e As EventArgs) Handles btnBseContractBrowse.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.csv)|*.csv"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtBseContractPath.Text = opfile.FileName
        End If
    End Sub
    Private Sub btnBseContractClear_Click(sender As Object, e As EventArgs) Handles btnBseContractClear.Click
        txtBseContractPath.Text = ""
    End Sub
    Private Sub btnBseContractImport_Click(sender As Object, e As EventArgs) Handles btnBseContractImport.Click
        FLGCSVCONTRACT = True
        If Not File.Exists(txtBseContractPath.Text) Then
            MsgBox("Invalid File Path.", MsgBoxStyle.Exclamation)
        End If

        Dim flg As Boolean = validate_contract_csv_file(txtBseContractPath.Text, "FOBSE")
        If flg = True Then
            MsgBox("Invalid CSV File..", MsgBoxStyle.Information)
            Return
        End If
        Me.Cursor = Cursors.WaitCursor
        If clsGlobal.mBseExchange.mContractFo.BSEContractImport(txtBseContractPath.Text) = True Then
            fill_token()
            Me.Cursor = Cursors.Default
            MsgBox("File Imported Successfully.", MsgBoxStyle.Information)
        Else
            Me.Cursor = Cursors.Default
            MsgBox("File Not Processed.", MsgBoxStyle.Information)
        End If
        If IsUnMatchTrade() Then
            Dim frm As New FrmMatchContract
            frm.ShowForm("FO")
        End If
    End Sub

    Private Sub btnBseEqBrowse_Click(sender As Object, e As EventArgs) Handles btnBseEqBrowse.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.csv)|*.csv"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtBseEqPath.Text = opfile.FileName
        End If
    End Sub

    Private Sub btnBseEqImport_Click(sender As Object, e As EventArgs) Handles btnBseEqImport.Click
        FLGCSVCONTRACT = True
        Dim flg As Boolean = validate_contract_csv_file(txtBseEqPath.Text, "EQBSE")
        If flg = True Then
            MsgBox("Invalid CSV File..", MsgBoxStyle.Information)
            Return
        End If
        Me.Cursor = Cursors.WaitCursor
        If txtBseEqPath.Text.Trim = "" Or (Not File.Exists(txtBseEqPath.Text)) Then
            Me.Cursor = Cursors.Default
            MsgBox("Invalid File Path.", MsgBoxStyle.Exclamation)
            Return
        End If
        Me.Cursor = Cursors.WaitCursor
        If clsGlobal.mBseExchange.mContractEq.BSESecurityImport(txtBseEqPath.Text) = True Then
            fill_token()
            Me.Cursor = Cursors.Default
            MsgBox("File Imported Successfully.", MsgBoxStyle.Information)
            txteqpath.Text = ""
        Else
            Me.Cursor = Cursors.Default
            MsgBox("File Not Processed.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub btnBseBhavCopyBrowse_Click(sender As Object, e As EventArgs) Handles btnBseBhavCopyBrowse.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.csv)|*.csv"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtBseBhavcopy.Text = opfile.FileName
        End If
    End Sub

    Private Sub btnBseBhavcopyImport_Click(sender As Object, e As EventArgs) Handles btnBseBhavcopyImport.Click
        If Not File.Exists(txtBseBhavcopy.Text) Then
            Me.Cursor = Cursors.Default
            MsgBox("Invalid File Path.", MsgBoxStyle.Exclamation)
            Return
        End If
        Me.Cursor = Cursors.WaitCursor
        clsGlobal.mBseExchange.mBhavCopy.removebhavcopyBSe()
        clsGlobal.mBseExchange.mBhavCopy.Read_FilebhavBSe(txtBseBhavcopy.Text)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub tbCtrlMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbCtrlMain.SelectedIndexChanged
        If tbCtrlMain.SelectedIndex = 0 Then
            FLGCSVCONTRACT = False
        Else
            FLGCSVCONTRACT = True
        End If
    End Sub
End Class
Public Class ZipHelp

    '/// <summary>
    '/// Zip a file
    '/// </summary>
    '/// <param name="SrcFile">source file path</param>
    '/// <param name="DstFile">zipped file path</param>
    '/// <param name="BufferSize">buffer to use</param>
    Public Shared Sub Zip(ByVal SrcFile As String, ByVal DstFile As String, ByVal BufferSize As Integer)

        Dim fileStreamIn As FileStream = New FileStream(SrcFile, FileMode.Open, FileAccess.Read)
        Dim fileStreamOut As FileStream = New FileStream(DstFile, FileMode.Create, FileAccess.Write)
        Dim zipOutStream As ZipOutputStream = New ZipOutputStream(fileStreamOut)


        Dim buffer(BufferSize) As Byte
        '= new byte[BufferSize]

        Dim entry As ZipEntry = New ZipEntry(Path.GetFileName(SrcFile))
        zipOutStream.PutNextEntry(entry)

        Dim size As Integer

        Do While (size > 0)
            size = fileStreamIn.Read(buffer, 0, buffer.Length)
            zipOutStream.Write(buffer, 0, size)
        Loop


        zipOutStream.Close()
        fileStreamOut.Close()
        fileStreamIn.Close()
    End Sub

    '/// <summary>
    '/// UnZip a file
    '/// </summary>
    '/// <param name="SrcFile">source file path</param>
    '/// <param name="DstFile">unzipped file path</param>
    '/// <param name="BufferSize">buffer to use</param>
    Public Shared Sub UnZip(ByVal SrcFile As String, ByVal DstFile As String, ByVal BufferSize As Integer)

        Dim fileStreamIn As FileStream = New FileStream(SrcFile, FileMode.Open, FileAccess.Read)
        Dim zipInStream As ZipInputStream = New ZipInputStream(fileStreamIn)
        Dim entry As ZipEntry = zipInStream.GetNextEntry()
        Dim fileStreamOut As FileStream = New FileStream(DstFile + "\" + entry.Name, FileMode.Create, FileAccess.Write)

        Dim size As Integer

        Dim buffer(BufferSize) As Byte

        Do
            size = zipInStream.Read(buffer, 0, buffer.Length)
            fileStreamOut.Write(buffer, 0, size)
        Loop While (size > 0)


        zipInStream.Close()
        fileStreamOut.Close()
        fileStreamIn.Close()

    End Sub



End Class
