Imports System.Windows.Forms
Imports System.IO
Imports System.Threading.Tasks
Imports VolHedge.DAL
Imports System.Data.OleDb

Public Class CNseImportBhavcopyV2
    Public mContext As CNetFileDownloadContext
    Public mDnMrg As CDownloadManager
    Sub New()
        mContext = New CNetFileDownloadContext("", "", "")
        mContext.mDownIDKey = "NseBhavCopyV2"
        Dim fileName As String = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"
        mContext.mUrl = Convert.ToString("https://support.finideas.com/Bhavcopy/") + fileName
        mContext.mDownloadFolder = Application.StartupPath + "\DownloadBhavcopy"
        mContext.mDownloadFile = fileName
    End Sub

    Public Function PreProcess()
        mContext.mIsSuccessPreProcess = False
        'mDnMrg.CheckAndDeleteFile(mContext.mDownloadFolder, mContext.mDownloadFile)
        mDnMrg.DeleteFilesByExtension(mContext.mDownloadFolder, "*.*")
        Dim fileName As String
        fileName = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"
        Dim inc As Integer = 1
        While True
            fileName = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1 * inc).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"
            mContext.mUrl = "https://support.finideas.com/Bhavcopy/" + fileName
            If mDnMrg.CheckRemoteFileExists(mContext.mUrl) Then
                mContext.mIsSuccessPreProcess = True
                mContext.mDownloadFile = fileName
                Exit While
            End If
            inc += 1
        End While
    End Function

    Public mLabel As Label
    Public Async Function DownloadProcess(pLbl As Label) As Task(Of Boolean)

        If Not mContext.mIsSuccessPreProcess Then
            'MessageBox.Show(mContext.mErrorMessage)
            If pLbl IsNot Nothing Then
                pLbl.Text = mContext.mErrorMessage
            End If
        End If
        mContext.mIsSuccessDownload = False
        mLabel = pLbl
        Try
            Dim fileDest As String = mContext.mDownloadFolder + "\" + mContext.mDownloadFile
            mContext.mDownloadedFilePath = fileDest
            mDnMrg.EnqueueDownload(mContext.mDownIDKey, mContext.mUrl, fileDest, pLbl)
            Dim res As DownloadResult = Nothing
            While Not mDnMrg.TryGetResult(mContext.mDownIDKey, res)
                Await Task.Delay(500)
            End While

            mContext.mIsSuccessDownload = True
            Return True
        Catch ex As Exception
            mContext.mIsSuccessDownload = False
            Return False
        End Try
    End Function

    Public Function PostProcess() As Boolean
        If Not mContext.mIsSuccessPreProcess Then
            Return False
        End If

        If File.Exists(mContext.mDownloadedFilePath) Then
            CZip.ExtractZipWithOverwrite(mContext.mDownloadedFilePath, mContext.mDownloadFolder)
            mLabel.Text = "Extract Done."

            Dim contractFile As String = mContext.mDownloadFolder + "\" + Path.GetFileNameWithoutExtension(mContext.mDownloadFile)

            If File.Exists(contractFile) Then
                mLabel.Text = "BhavCopy File Found"
                'If import_Data.ContractImport(contractFile, objIO) = True Then
                '    fill_token()
                '    mLabel.Text = "File Imported Successfully."
                'Else
                '    mLabel.Text = "File Not Processed."
                'End If
            Else
                mLabel.Text = "BhavCopy File Not Found"
            End If
        End If
        Try
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Async Function DoAll(pLbl As Label) As Task
        mLabel = pLbl
        mLabel.Text = ""
        mLabel.ForeColor = Color.Black
        Me.PreProcess()
        System.Threading.Thread.Sleep(200)
        Await Me.DownloadProcess(pLbl)
        System.Threading.Thread.Sleep(100)
        Me.PostProcess()
        mLabel.ForeColor = Color.Green
        System.Threading.Thread.Sleep(100)

    End Function

    Public Sub RemoveBhavCopy()
        Dim Objbhavcopy1 As New bhav_copy
        GdtBhavcopy = Objbhavcopy1.select_data()
        Dim dt As DataTable = New DataView(GdtBhavcopy, "", "entry_date ASC", DataViewRowState.CurrentRows).ToTable(True, "entry_date")
        If dt.Rows.Count >= BHAVCOPYPROCESSDAY Then
            For i As Integer = 0 To dt.Rows.Count - BHAVCOPYPROCESSDAY - 1
                Dim entrydatebhav As Date = CDate(dt.Rows(i)(0))
                RemoveRecords(entrydatebhav)
            Next
        End If
    End Sub

    Public Function RemoveRecords(ByVal entry_date As Date) As DataTable
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

    Public Sub ProcesFile(ByVal path As String)
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
                            drow("vol") = OptionG.Greeks.Vol(futval, Val(drow("STRIKE_PR")), Val(drow("SETTLE_PR")), mt, 0, iscall, True) * 100
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
                MsgBox("Bhavcopy Processed Successfully.")
            End If
        Catch ex As Exception
            MsgBox("Bhavcopy Not Processed.")
        End Try
    End Sub
End Class

