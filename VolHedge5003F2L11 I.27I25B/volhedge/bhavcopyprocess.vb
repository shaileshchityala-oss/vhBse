Imports System
Imports System.io
Imports System.Data
Imports System.Data.OleDb
Imports VolHedge.OptionG
Imports VolHedge.DAL

Public Class bhavcopyprocess
    Dim tempdata As DataTable
    Dim objTrad As trading = New trading
    Dim Mrateofinterast As Double = 0
    Dim DtBCP As DataTable

    '    Dim mObjData As New DataAnalysis.AnalysisData
    'Dim obj_get_price As New get_price.get_price("M8HUM1T3Q15R9L")
    Dim objBh As New bhav_copy

    ''' <summary>
    ''' read_file()
    ''' To Readbhavcopy csv File 
    ''' And Calculation of_Vol FutVal 
    ''' And Merge With Bhavcopy
    ''' Modified By Viral
    ''' </summary>
    ''' <remarks>
    ''' To Readbhavcopy csv File 
    ''' And Calculation of_Vol FutVal 
    ''' And Merge With Bhavcopy</remarks>
    Private Sub read_file()
        REM Change In processing Bhavcopy and selecting parameters, Decimal Strike Prices are properly displayed for the records 
        Try
            Dim fpath As String
            fpath = CStr(txtpath.Text.Trim)
            Mrateofinterast = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
            If fpath <> "" Then
                Dim fi As New FileInfo(fpath)
                Dim dv2 As DataView
                tempdata = New DataTable
                '  Try

                '      Dim fName As String = ""


                '      '  Dim fi As New FileInfo(fpath)
                '      Dim strdata As [String]()
                '      strdata = fpath.Split(New Char() {"\"c})
                '      'strdata = dr("script").Split("  ", StringSplitOptions.None)


                '      Dim filename As String = strdata(strdata.Length - 1)
                'Dim sConnectionStringz As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
                '      Dim objConn As New OleDbConnection(sConnectionStringz)
                '      objConn.Open()
                '      Dim objCmdSelect As New OleDbCommand("SELECT * FROM [" & filename & "] where option_typ='xx'", objConn)
                '      'Dim objCmdSelect As New OleDbCommand("SELECT strike,option_type,Exp. Date,Company,Qty,Rate,Instrument,entrydate FROM " & fi.Name, objConn)
                '      Dim objAdapter1 As New OleDbDataAdapter
                '      objAdapter1.SelectCommand = objCmdSelect
                '      objAdapter1.Fill(tempdata)
                '      objConn.Close()
                '      'fi.Delete()
                '  Catch ex As Exception
                '      Label1.Text = ""
                '      MsgBox("Bhavcopy Not Processed.", MsgBoxStyle.Information)
                '      Exit Sub
                '  End Try
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
                DtBCP = objBh.select_TblBhavCopy()

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
                        mt = UDDateDiff(DateInterval.Day, ccdate.Date, CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")).Date)
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
                objBh.insertNew(tempdata)

                GdtBhavcopy = objBh.select_data()
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

                Label1.Text = "Bhavcopy Processed Successfully."
                MsgBox("Bhavcopy Processed Successfully.", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            Label1.Text = "Bhavcopy Not Processed."
            deletebhavcopy()
            MsgBox("Bhavcopy Not Processed.", MsgBoxStyle.Information)
            'MsgBox("Select valid file")
            '/MsgBox(ex.ToString)
        End Try
    End Sub
    Private Function deletebhavcopy()
        Dim qry As String
        Try

            qry = "Delete From TblBhavcopy"
            data_access.ParamClear()
            data_access.Cmd_Text = qry
            data_access.cmd_type = CommandType.Text
            data_access.ExecuteNonQuery()
            data_access.cmd_type = CommandType.StoredProcedure
        Catch ex As Exception
        End Try
    End Function
    Private Function Vol(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal _mt As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean) As Double
        Dim tmpcpprice As Double = 0
        Dim mVolatility As Double
        tmpcpprice = cpprice
        'IF MATURITY IS 0 THEN _MT = 0.00001
        mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
        Return mVolatility
    End Function

    ''' <summary>
    ''' cmdprocess_Click
    ''' Read File using linktable
    ''' and import Form ImportDB mdb And Calculate Vol
    ''' And insert in To Bhavcopy in Greek mdb
    ''' modified By Viral
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdprocess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdprocess.Click
        If txtpath.Text <> "" Then
            Label2.Text = "Status :"
            Label1.Text = "Bhavcopy in Process...."
            Label1.Refresh()
            Label2.Refresh()
            Me.Cursor = Cursors.WaitCursor
            removebhavcopy()
            read_file()
        Else
            MsgBox("File Path Not Selected.")
        End If
        Me.Cursor = Cursors.Default
    End Sub
    Public Sub removebhavcopy()
        GdtBhavcopy = objBh.select_data()
        Dim dt As DataTable = New DataView(GdtBhavcopy, "", "entry_date ASC", DataViewRowState.CurrentRows).ToTable(True, "entry_date")
        If dt.Rows.Count >= BHAVCOPYPROCESSDAY Then
            Dim entrydate As Date = CDate(dt.Rows(0)(0))
            removebhavcopy(entrydate)
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
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.csv)|*.csv"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtpath.Text = opfile.FileName
        End If
    End Sub

    Private Sub bhavcopyprocess_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub

    Private Sub bhavcopyprocess_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
        Label1.Text = ""
        Label2.Text = ""
    End Sub
    Private Sub bhavcopyprocess_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Proc_Data_FromBhavCopyCsv(ByVal FilePath As String)
        REM Change In processing Bhavcopy and selecting parameters, Decimal Strike Prices are properly displayed for the records  Use Hash Table

        Dim Hs As New Hashtable

        Hs.Add("INSTRUMENT", 0)
        Hs.Add("SYMBOL", 1)
        Hs.Add("EXPIRY_DT", "2date")
        Hs.Add("STRIKE_PR", "3double")
        Hs.Add("OPTION_TYP", 4)
        Hs.Add("OPEN", "5double")
        Hs.Add("HIGH", "6double")
        Hs.Add("LOW", "7double")
        Hs.Add("CLOSE", "8double")
        Hs.Add("SETTLE_PR", "9double")
        Hs.Add("CONTRACTS", "10double")
        Hs.Add("VAL_INLAKH", "11double")
        Hs.Add("OPEN_INT", "12double")
        Hs.Add("CHG_IN_OI", "13double")
        Hs.Add("TIMESTAMP", "14date")

        DtBCP = GetDTFromCSVFile(",", FilePath, Hs)

    End Sub
    Private Function GetDTFromCSVFile(ByVal SeparatorChar As String, ByVal FilePath As String, ByVal ColList As Hashtable, Optional ByRef StartLineNo As Long = 0) As DataTable

        Dim DtMy As New DataTable
        Dim Item As DictionaryEntry
        REM Add Column into datatable according to Parameter Hashtable
        For Each Item In ColList
            Dim VarIndex As Integer = Val(Item.Value)
            Dim VarType As String = Item.Value.ToString.Substring(VarIndex.ToString.Length)
            Select Case UCase(VarType.Trim)
                Case ""
                    DtMy.Columns.Add(Item.Key)
                Case "STRING"
                    DtMy.Columns.Add(Item.Key, GetType(String))
                Case "INTEGER"
                    DtMy.Columns.Add(Item.Key, GetType(Integer))
                Case "LONG"
                    DtMy.Columns.Add(Item.Key, GetType(Long))
                Case "DOUBLE"
                    DtMy.Columns.Add(Item.Key, GetType(Double))
                Case "DATE"
                    DtMy.Columns.Add(Item.Key, GetType(Date))
                Case "DATETIME"
                    DtMy.Columns.Add(Item.Key, GetType(DateTime))
                Case Else
                    DtMy.Columns.Add(Item.Key)
            End Select
        Next

        REM Check File exist from Path
        If File.Exists(FilePath) = False Then
            Throw New ApplicationException("Following file path " & FilePath & " Not Found !!")
            Return DtMy
            Exit Function
        End If
        Dim VarFileName As String = Path.GetFileName(FilePath)
        Dim VarFileCounter As Integer = 0

        File.Copy(FilePath, Application.StartupPath & "\" & VarFileName, True)
        FilePath = Application.StartupPath & "\" & VarFileName
        Dim RStream As New System.IO.StreamReader(FilePath)

        Dim Str As String
        Str = RStream.ReadLine()

        If Str Is Nothing Or Str = "" Then
            RStream.Close()
            File.Delete(FilePath)
            Return DtMy
            Exit Function
        End If

        Str = RStream.ReadLine()
        REM Line by line row Added into Datatable
        Do Until Str Is Nothing
            Dim Dr As DataRow = DtMy.NewRow
            If Str <> "" Then
                Dim StrData As String() = Split(Str, SeparatorChar)
                For Each Item In ColList
                    Dr(Item.Key) = Trim(StrData(Val(Item.Value)))
                Next
                DtMy.Rows.Add(Dr)
                StartLineNo += 1
            End If
            Str = RStream.ReadLine()
        Loop
        REM End

        RStream.Close()
        File.Delete(FilePath)
        Return DtMy
    End Function

End Class