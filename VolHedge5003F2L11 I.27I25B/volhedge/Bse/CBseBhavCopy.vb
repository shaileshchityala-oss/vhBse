Imports System.Data
Imports System.IO
Imports Sylvan.Data.Csv
Imports System.Globalization
Imports System.Threading.Tasks
Imports VolHedge.DAL
Imports System.Collections
Imports System.Collections.Concurrent
Imports System.Data.OleDb
Imports System.Configuration
Imports VolHedge
Imports VolHedge.OptionG
Public Class CBseBhavCopy

    Private DbPath As String = Application.StartupPath & ConfigurationSettings.AppSettings("dbname") & ";PWD=" & clsGlobal.glbAcessPassWord & ""
    Private Delete_Contract_Bse As String = "Delete * From  [" & DbPath & "].Contract WHERE Exchange='Bse';"
    Private Delete_Bhavcopy_Bse As String = "Delete * From  [" & DbPath & "].TblBhavcopy WHERE Exchange='Bse';"
    Private Insert_BhavcopyBSE As String = "INSERT INTO [" & DbPath & "].TblBhavcopy  " &
                                                        " Select  iif(FinInstrmTp ='STO','OPTSTK',iif(FinInstrmTp='STF','FUTSTK',
            iif( FinInstrmTp='IDF' ,'FUTIDX',iif( FinInstrmTp='IDO' ,'OPTIDX','')))) as INSTRUMENT  , 
            TckrSymb as [SYMBOL],XpryDt as EXPIRY_DT,iif(FinInstrmTp='STF','0',iif( FinInstrmTp='IDF' ,'0' , StrkPric))  as STRIKE_PR,
            iif(FinInstrmTp='STF','XX',iif( FinInstrmTp='IDF' ,'XX' , OptnTp)) as OPTION_TYP,OpnPric as [OPEN],HghPric AS [HIGH],LwPric as [LOW],
            ClsPric as [CLOSE],SttlmPric as SETTLE_PR,0 as CONTRACTS,TtlTrfVal as VAL_INLAKH,OpnIntrst as OPEN_INT,ChngInOpnIntrst as CHG_IN_OI,TradDt  as [TIMESTAMP]  ,'Bse' as Exchange
            from Bhavcopyfo;"

    Private Const SP_bhavcopy_selectBSe As String = "Select_bhavcopy_Bse"
    Private Const SP_bhavcopy_Insert_Bse As String = "insert_bhavcopy_Bse"
    Private Const SP_select_TblBHavCopyBse As String = "Select_TblBhavcopyBse"
    Private Const SP_delete_bhavcopy_Date_Bse As String = "delete_bhavcopy_Date_Bse"

    Public Sub insertNewBse(ByVal dtable As DataTable)
        Try
            Dim ddate As Date
            ddate = CDate(dtable.Compute("Max(TIMESTAMP)", "").ToString.Replace("-", "/"))
            DABhav.ParamClear()
            DABhav.AddParam("@date1", OleDbType.Date, 50, (ddate))
            DABhav.Cmd_Text = SP_delete_bhavcopy_Date_Bse
            DABhav.cmd_type = CommandType.StoredProcedure
            DABhav.ExecuteNonQuery(CommandType.StoredProcedure)
        Catch ex As Exception

        End Try


        DABhav.ParamClear()
        For Each drow As DataRow In dtable.Rows
            DABhav.AddParam("@script", OleDbType.VarChar, 50, CStr(drow("script")).Trim)
            DABhav.AddParam("@INSTRUMENT", OleDbType.VarChar, 50, CStr(drow("INSTRUMENT")))
            DABhav.AddParam("@symbol", OleDbType.VarChar, 50, CStr(drow("symbol")))

            DABhav.AddParam("@exp_date", OleDbType.Date, 18, CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")))
            DABhav.AddParam("@strike", OleDbType.Double, 18, Val(drow("STRIKE_PR")))
            DABhav.AddParam("@option_type", OleDbType.VarChar, 20, CStr(drow("OPTION_TYP")))
            DABhav.AddParam("@ltp", OleDbType.Double, 20, Val(drow("SETTLE_PR")))
            DABhav.AddParam("@contract", OleDbType.Double, 18, Val(drow("CONTRACTS")))
            DABhav.AddParam("@val_inlakh", OleDbType.Double, 18, Val(drow("VAL_INLAKH")))
            DABhav.AddParam("@vol", OleDbType.Double, 18, Val(drow("vol")))
            DABhav.AddParam("@entry_date", OleDbType.Date, 18, CDate(drow("TIMESTAMP").ToString.Replace("-", "/")))
            DABhav.AddParam("@futval", OleDbType.Double, 18, Val(drow("futval")))
            DABhav.AddParam("@mt", OleDbType.Double, 18, Val(drow("mt")))
        Next
        DABhav.Cmd_Text = SP_bhavcopy_Insert_Bse
        DABhav.cmd_type = CommandType.StoredProcedure
        DABhav.ExecuteMultiple(13)
    End Sub

    Public Sub select_data_BSeMdb(ByRef Dt As DataTable)
        DABhav.ParamClear()
        DABhav.Cmd_Text = SP_bhavcopy_selectBSe
        DABhav.cmd_type = CommandType.StoredProcedure
        Dt = DABhav.FillList()
    End Sub

    Public Function select_data_Bse() As DataTable
        data_access.ParamClear()
        data_access.Cmd_Text = SP_bhavcopy_selectBSe
        data_access.cmd_type = CommandType.StoredProcedure
        Return data_access.FillList
    End Function

    Public Function select_TblBhavCopyBse() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_select_TblBHavCopyBse
            data_access.cmd_type = CommandType.StoredProcedure
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("bhavcopy :: select_TblBhavCopy() ::" & ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Sub ImportBhavCopyBse()
        Try
            REM Delete Contract
            DABhav.ParamClear()
            DABhav.Cmd_Text = Delete_Bhavcopy_Bse
            DABhav.cmd_type = CommandType.Text
            DABhav.ExecuteNonQuery(CommandType.Text)

            REM import Contract
            DABhav.ParamClear()


            DABhav.Cmd_Text = Insert_BhavcopyBSE
            DABhav.cmd_type = CommandType.Text
            DABhav.ExecuteNonQuery2(CommandType.Text)



        Catch ex As Exception
            'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub removebhavcopyBSe()
        'Dim Objbhavcopy1 As New bhav_copy
        GdtBhavcopy = select_data_Bse()
        Dim dt As DataTable = New DataView(GdtBhavcopy, "", "entry_date ASC", DataViewRowState.CurrentRows).ToTable(True, "entry_date")
        If dt.Rows.Count >= BHAVCOPYPROCESSDAY Then

            For i As Integer = 0 To dt.Rows.Count - BHAVCOPYPROCESSDAY - 1
                Dim entrydatebhav As Date = CDate(dt.Rows(i)(0))
                RemoveBhavcopyMdb(entrydatebhav)
            Next

        End If
    End Sub
    Public Function RemoveBhavcopyMdb(ByVal entry_date As Date) As DataTable
        Dim SP_delete_bhavcopy_Date As String = "delete_bhavcopy_Date"
        Try
            DABhav.ParamClear()
            DABhav.AddParam("@date1", OleDbType.Date, 50, CDate(entry_date))
            DABhav.Cmd_Text = SP_delete_bhavcopy_Date
            DABhav.cmd_type = CommandType.StoredProcedure
            DABhav.ExecuteNonQuery(CommandType.Text)
        Catch ex As Exception
            '  MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    'Public Sub ImportBhavCopyBse()
    '    Try
    '        REM Delete Contract
    '        data_access.ParamClear()
    '        data_access.Cmd_Text = Delete_Bhavcopy_Bse
    '        data_access.cmd_type = CommandType.Text
    '        data_access.ExecuteNonQuery()

    '        REM import Contract
    '        data_access.ParamClear()

    '        data_access.Cmd_Text = Insert_BhavcopyBSE
    '        data_access.cmd_type = CommandType.Text
    '        data_access.ExecuteNonQuery()



    '    Catch ex As Exception
    '        'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
    '        MsgBox(ex.ToString)
    '    End Try
    'End Sub

    Public Sub Read_FilebhavBSe(ByVal path As String)
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

                import_Data.CopyToData(fpath, "BHAVCOPYNEW")

                Call ImportBhavCopyBse()

                impBHav = Nothing
                Dim Objbhavcopy1 As New bhav_copy
                DtBCP = select_TblBhavCopyBse()

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

                    Dim optType As String = drow("option_typ").ToString()

                    If optType = "XX" Then
                        script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy")
                        drow("script") = UCase(script.Trim)
                        drow("vol") = 0
                        drow("futval") = 0
                        drow("mt") = 0
                    Else
                        script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy") & "  " & Format(Val(drow("STRIKE_PR")), "###0.00") & "  " & optType
                        'script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(drow("EXPIRY_DT"), "ddMMMyyyy") & "  " & Format(Val(drow("STRIKE_PR")), "####.##") & "  " & drow("OPTION_TYP")
                        drow("script") = UCase(script.Trim)
                        futval = 0
                        drow("vol") = 0
                        For Each row In tdata.Select(" EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' ")
                            futval = row("SETTLE_PR")
                        Next
                        'futval = Val(tempdata.Compute("Max(SETTLE_PR)", " EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' And option_typ='XX'").ToString() & "")
                        'row("SETTLE_PR")

                        If Mid(optType, 1, 1) = "C" Then
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
                insertNewBse(tempdata)

                GdtBhavcopy = select_data_Bse()
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

    Dim Mrateofinterast As Double = 0
    Private Function Vol(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal _mt As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean) As Double

        Dim tmpcpprice As Double = 0
        Dim mVolatility As Double
        tmpcpprice = cpprice
        'IF MATURITY IS 0 THEN _MT = 0.00001 
        mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
        Return mVolatility
    End Function

End Class
Public MustInherit Class DABhav

#Region "variable"
    Shared _con As OleDbConnection
    Shared _adp As OleDbDataAdapter
    Shared _cmd As OleDbCommand
    Shared _cmd_type As CommandType = CommandType.StoredProcedure
    Shared _cmd_text As String
    Shared _connection_string As String
    Shared _paramtable As DataTable
    Shared _DtPrimaryExp As New DataTable
    Shared _tra As OleDbTransaction
#End Region

#Region "Method"

    Public Sub New()

    End Sub

    Public Shared Property Cmd_Text() As String
        Get
            Return _cmd_text
        End Get
        Set(ByVal value As String)
            _cmd_text = value
        End Set
    End Property

    Public Shared Property cmd_type() As CommandType
        Get
            Return _cmd_type
        End Get
        Set(ByVal value As CommandType)
            _cmd_type = value
        End Set
    End Property

    Private Shared ReadOnly Property Connection_string() As String
        Get
            Try
                Dim str As String = ConfigurationSettings.AppSettings("dbname")
                '_connection_string = ConfigurationSettings.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str
                '_connection_string = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=True;Data Source=D:\FinIdeas Projects\VolHedge 4.0.0.48 H13\volhedge\bin\x86\Debug/greekP.mdb;Jet OLEDB:Database Password=Admin"
                _connection_string = ConfigurationSettings.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str & ";Jet OLEDB:Database Password=" & clsGlobal.glbAcessPassWord & ""

                If _connection_string = "" Then
                    Throw New ApplicationException("Connection String is not initialize")
                End If
                Return _connection_string
            Catch ex As Exception
                'MsgBox(ex.ToString)
                'FSTimerLogFile.WriteLine("Data_access::Connection_string:-" & ex.ToString)
                Throw New ApplicationException("Connection String is not initialize")
            End Try
        End Get
    End Property

    Private Shared ReadOnly Property Connection_string2() As String
        Get
            Try
                Dim str As String = ConfigurationSettings.AppSettings("Importdb")
                '_connection_string = ConfigurationSettings.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str
                '_connection_string = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=True;Data Source=D:\FinIdeas Projects\VolHedge 4.0.0.48 H13\volhedge\bin\x86\Debug/greekP.mdb;Jet OLEDB:Database Password=Admin"
                _connection_string = ConfigurationSettings.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str & ";Jet OLEDB:Database Password=" & clsGlobal.glbAcessPassWord & ""

                If _connection_string = "" Then
                    Throw New ApplicationException("Connection String is not initialize")
                End If
                Return _connection_string
            Catch ex As Exception
                'MsgBox(ex.ToString)
                'FSTimerLogFile.WriteLine("Data_access::Connection_string:-" & ex.ToString)
                Throw New ApplicationException("Connection String is not initialize")
            End Try
        End Get
    End Property

    Private Shared Sub open_connection2()
        Try
            If Connection_string <> "" Or (_con Is Nothing) Then
                _con = New OleDbConnection(Connection_string2)
                _con.Open()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            'FSTimerLogFile.WriteLine("Data_access::open_connection:-" & ex.ToString)
            Throw New ApplicationException("Connection Error")
        End Try
    End Sub
    Private Shared Sub open_connection()
        Try
            If Connection_string <> "" Or (_con Is Nothing) Then
                _con = New OleDbConnection(Connection_string)
                _con.Open()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            'FSTimerLogFile.WriteLine("Data_access::open_connection:-" & ex.ToString)
            Throw New ApplicationException("Connection Error")
        End Try
    End Sub
    Private Shared Sub Con_Transaction()
        If _con.State = ConnectionState.Open Then
            _tra = _con.BeginTransaction()
        End If
    End Sub
    Private Shared Sub Con_Commit()
        _tra.Commit()
    End Sub
    Private Shared Sub Con_Rollback()
        _tra.Rollback()
    End Sub
    Private Shared Sub Close_Connection()
        If _con.State = ConnectionState.Open Then
            _con.Close()
        End If
        cmd_type = CommandType.StoredProcedure
    End Sub

    Public Shared Sub ParamClear()
        _paramtable = New DataTable()
        _paramtable.Columns.Add("ParameterName", GetType(String))
        _paramtable.Columns.Add("oleDbType", GetType(OleDbType))
        _paramtable.Columns.Add("Size", GetType(Integer))
        _paramtable.Columns.Add("Value", GetType(Object))
    End Sub

    Public Shared Sub AddParam(ByVal param_id As String, ByVal oledbtype As OleDbType, ByVal size As Integer, ByVal value As Object)
        Dim drow As DataRow = _paramtable.NewRow()
        drow("ParameterName") = param_id
        drow("oledbtype") = oledbtype
        drow("Size") = size
        If value Is Nothing Then
            drow("Value") = DBNull.Value
        Else
            drow("Value") = value
        End If
        _paramtable.Rows.Add(drow)
    End Sub

    Public Shared Function ExecuteNonQuery2(ByVal cmd_type As CommandType) As DataTable
        If _DtPrimaryExp.Columns.Count = 0 Then
            _DtPrimaryExp.Columns.Add("ParameterName", GetType(String))
            _DtPrimaryExp.Columns.Add("oleDbType", GetType(OleDbType))
            _DtPrimaryExp.Columns.Add("Size", GetType(Integer))
            _DtPrimaryExp.Columns.Add("Value", GetType(Object))
        End If
        _DtPrimaryExp.Rows.Clear()
        Try
            Dim result As String = ""
            open_connection2()
            _cmd = New OleDbCommand()
            _cmd.CommandType = cmd_type
            _cmd.CommandText = _cmd_text
            Con_Transaction()
            _cmd.Connection = _con
            _cmd.Transaction = _tra
            If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                AddParamToSQLCmd()
            End If
            'divyesh
            Try
                result = Convert.ToString(_cmd.ExecuteNonQuery())
            Catch ex1 As Exception
                If ex1.Message.ToUpper.Contains("DUPLICATE VALUES IN THE INDEX, PRIMARY KEY, OR RELATIONSHIP") = True Then
                    For cnt As Integer = 0 To _paramtable.Rows.Count - 1
                        _DtPrimaryExp.ImportRow(_paramtable.Rows(cnt))
                    Next
                Else
                    Throw New ApplicationException(ex1.Message)
                End If
            End Try

            'result = Convert.ToString(_cmd.ExecuteNonQuery())
            Con_Commit()
            Close_Connection()
            Return _DtPrimaryExp 'result.ToString()
        Catch ex As Exception
            'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
            MsgBox(ex.ToString)
            Con_Rollback()
        Finally
            Close_Connection()
        End Try
    End Function

    Public Shared Function ExecuteNonQuery(ByVal cmd_type As CommandType) As DataTable
        If _DtPrimaryExp.Columns.Count = 0 Then
            _DtPrimaryExp.Columns.Add("ParameterName", GetType(String))
            _DtPrimaryExp.Columns.Add("oleDbType", GetType(OleDbType))
            _DtPrimaryExp.Columns.Add("Size", GetType(Integer))
            _DtPrimaryExp.Columns.Add("Value", GetType(Object))
        End If
        _DtPrimaryExp.Rows.Clear()
        Try
            Dim result As String = ""
            open_connection()
            _cmd = New OleDbCommand()
            _cmd.CommandType = cmd_type
            _cmd.CommandText = _cmd_text
            Con_Transaction()
            _cmd.Connection = _con
            _cmd.Transaction = _tra
            If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                AddParamToSQLCmd()
            End If
            'divyesh
            Try
                result = Convert.ToString(_cmd.ExecuteNonQuery())
            Catch ex1 As Exception
                If ex1.Message.ToUpper.Contains("DUPLICATE VALUES IN THE INDEX, PRIMARY KEY, OR RELATIONSHIP") = True Then
                    For cnt As Integer = 0 To _paramtable.Rows.Count - 1
                        _DtPrimaryExp.ImportRow(_paramtable.Rows(cnt))
                    Next
                Else
                    Throw New ApplicationException(ex1.Message)
                End If
            End Try

            'result = Convert.ToString(_cmd.ExecuteNonQuery())
            Con_Commit()
            Close_Connection()
            Return _DtPrimaryExp 'result.ToString()
        Catch ex As Exception
            'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
            MsgBox(ex.ToString)
            Con_Rollback()
        Finally
            Close_Connection()
        End Try
    End Function

    Public Shared Function ExecuteNonQuery(ByVal cmd_Text As String, ByVal cmd_type As CommandType) As DataTable
        If _DtPrimaryExp.Columns.Count = 0 Then
            _DtPrimaryExp.Columns.Add("ParameterName", GetType(String))
            _DtPrimaryExp.Columns.Add("oleDbType", GetType(OleDbType))
            _DtPrimaryExp.Columns.Add("Size", GetType(Integer))
            _DtPrimaryExp.Columns.Add("Value", GetType(Object))
        End If
        _DtPrimaryExp.Rows.Clear()
        Try
            Dim result As String = ""
            open_connection()
            _cmd = New OleDbCommand()
            _cmd.CommandType = cmd_type
            _cmd.CommandText = cmd_Text
            Con_Transaction()
            _cmd.Connection = _con
            _cmd.Transaction = _tra
            If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                AddParamToSQLCmd()
            End If
            'divyesh
            Try
                result = Convert.ToString(_cmd.ExecuteNonQuery())
            Catch ex1 As Exception
                If ex1.Message.ToUpper.Contains("DUPLICATE VALUES IN THE INDEX, PRIMARY KEY, OR RELATIONSHIP") = True Then
                    For cnt As Integer = 0 To _paramtable.Rows.Count - 1
                        _DtPrimaryExp.ImportRow(_paramtable.Rows(cnt))
                    Next
                Else
                    Throw New ApplicationException(ex1.Message)
                End If
            End Try

            'result = Convert.ToString(_cmd.ExecuteNonQuery())
            Con_Commit()
            Close_Connection()
            Return _DtPrimaryExp 'result.ToString()
        Catch ex As Exception
            'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
            MsgBox(ex.ToString)
            Con_Rollback()
        Finally
            Close_Connection()
        End Try
    End Function



    Public Shared Function ExecuteMultiple(ByVal parmcount As Integer) As DataTable
        If _DtPrimaryExp.Columns.Count = 0 Then
            _DtPrimaryExp.Columns.Add("ParameterName", GetType(String))
            _DtPrimaryExp.Columns.Add("oleDbType", GetType(OleDbType))
            _DtPrimaryExp.Columns.Add("Size", GetType(Integer))
            _DtPrimaryExp.Columns.Add("Value", GetType(Object))
        End If
        _DtPrimaryExp.Rows.Clear()
        Try
            Dim result As String = ""
            open_connection()
            Con_Transaction()
            If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                Dim j As Integer
                j = 0
                For i As Integer = 0 To _paramtable.Rows.Count - 1 Step parmcount
                    _cmd = New OleDbCommand()
                    _cmd.CommandType = _cmd_type
                    _cmd.CommandText = _cmd_text
                    _cmd.Connection = _con
                    _cmd.Transaction = _tra
                    j = 0
                    For k As Integer = i To _paramtable.Rows.Count - 1
                        AddParamToSQLCmd(_paramtable.Rows(k))
                        j = j + 1
                        If j = parmcount Then
                            Exit For
                        End If
                    Next
                    'result = Convert.ToString(_cmd.ExecuteNonQuery())
                    Try
                        result = Convert.ToString(_cmd.ExecuteNonQuery())
                    Catch ex1 As Exception
                        If ex1.Message.ToUpper.Contains("DUPLICATE VALUES IN THE INDEX, PRIMARY KEY, OR RELATIONSHIP") = True Then
                            For cnt As Integer = i To (i + parmcount - 1)
                                _DtPrimaryExp.ImportRow(_paramtable.Rows(cnt))
                            Next
                        Else
                            Throw New ApplicationException(ex1.Message)
                        End If
                    End Try
                Next
            End If
            Con_Commit()
            Close_Connection()
            Return _DtPrimaryExp
        Catch ex As Exception
            'FSTimerLogFile.WriteLine("Data_access::ExecuteMultiple:-" & ex.ToString)
            MsgBox(ex.ToString)
            Con_Rollback()
        Finally
            Close_Connection()
        End Try
    End Function
    Public Shared Function ExecuteScalar() As String
        Try
            Dim result As String = ""
            open_connection()
            Con_Transaction()
            '_jro.RefreshCache(_con)
            _cmd = New OleDbCommand()
            _cmd.CommandType = _cmd_type
            _cmd.CommandText = _cmd_text
            _cmd.Connection = _con
            _cmd.Transaction = _tra

            If (IsDBNull(_paramtable) = False And _paramtable.Rows.Count > 0) Then
                AddParamToSQLCmd()
            End If
            result = Convert.ToString(_cmd.ExecuteScalar())
            Con_Commit()
            Close_Connection()
            Return result.ToString()
        Catch ex As Exception
            ' FSTimerLogFile.WriteLine("Data_access::ExecuteScalar:-" & ex.ToString)
            MsgBox(ex.ToString)
            Con_Rollback()
        Finally
            Close_Connection()
        End Try
    End Function
    Private Shared Sub AddParamToSQLCmd(ByVal drow As DataRow)
        If Not IsDBNull(_cmd) = False Then
            Throw (New ApplicationException("Command not Initialized."))
        End If
        Dim newSqlParam As OleDbParameter = New OleDbParameter
        newSqlParam = New OleDbParameter()
        newSqlParam.ParameterName = drow("ParameterName").ToString()
        newSqlParam.OleDbType = CType(drow("oleDbType"), OleDbType)
        newSqlParam.Direction = ParameterDirection.Input
        If (Convert.ToInt16(drow("Size")) > 0) Then
            newSqlParam.Size = Convert.ToInt16(drow("Size"))
        End If
        If Not IsDBNull(drow("Value")) Then
            newSqlParam.Value = drow("Value")
        End If
        _cmd.Parameters.Add(newSqlParam)

    End Sub
    Private Shared Sub AddParamToSQLCmd()
        If Not IsDBNull(_cmd) = False Then
            Throw (New ApplicationException("Command not Initialized."))
        End If
        Dim newSqlParam As OleDbParameter = New OleDbParameter

        For Each drow As DataRow In _paramtable.Rows

            newSqlParam = New OleDbParameter()
            newSqlParam.ParameterName = drow("ParameterName").ToString()
            newSqlParam.OleDbType = CType(drow("oleDbType"), OleDbType)
            newSqlParam.Direction = ParameterDirection.Input
            If (Convert.ToInt16(drow("Size")) > 0) Then
                newSqlParam.Size = Convert.ToInt16(drow("Size"))
            End If
            If Not IsDBNull(drow("Value")) Then
                newSqlParam.Value = drow("Value")
            End If
            _cmd.Parameters.Add(newSqlParam)
        Next
    End Sub
    Public Shared Function FillList() As DataTable
        Try
            Dim list As DataTable = New DataTable()
            list.Rows.Clear()
            _adp = New OleDbDataAdapter
            open_connection()
            Con_Transaction()
            '  _jro.RefreshCache(_con)
            _cmd = New OleDbCommand()
            _cmd.CommandType = _cmd_type
            _cmd.CommandText = _cmd_text
            _cmd.Connection = _con
            _cmd.Transaction = _tra
            If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                AddParamToSQLCmd()
            End If
            _adp.SelectCommand = _cmd
            _adp.Fill(list)
            _adp.Dispose()
            Con_Commit()
            Close_Connection()
            Return list
        Catch ex As Exception
            'FSTimerLogFile.WriteLine("Data_access::FillList:-" & ex.ToString)
            MsgBox("data_access :: FillList() :: " & ex.ToString)
            Con_Rollback()
            Return Nothing
            '  MsgBox(ex.ToString)
            'Finally
            Close_Connection()
        End Try

    End Function
    Public Shared Function ExecuteReturn(ByVal Str As String) As Object
        Try
            open_connection()
            If _con.State = ConnectionState.Closed Then Con_Transaction()
            _cmd = New OleDbCommand()
            _cmd.CommandText = Str
            _cmd.Connection = _con
            _cmd.Transaction = _tra
            Str = _cmd.ExecuteScalar
            _cmd.Dispose()
            Con_Commit()
            Return Str
        Catch ex As Exception
            'FSTimerLogFile.WriteLine("Data_access::ExecuteReturn:-" & ex.ToString)
            Return ""
            Con_Rollback()
        Finally
            Close_Connection()
        End Try
    End Function
#End Region

End Class
