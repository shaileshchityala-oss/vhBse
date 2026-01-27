Imports System.Data.OleDb
Imports System.Configuration

Public Class Cls_AEL

    Private _cmd_type As CommandType = CommandType.StoredProcedure

    Public Property Cmd_Text() As String
        Get
            Return _cmd_text
        End Get
        Set(ByVal value As String)
            _cmd_text = value
        End Set
    End Property
    Public Property cmd_type() As CommandType
        Get
            Return _cmd_type
        End Get
        Set(ByVal value As CommandType)
            _cmd_type = value
        End Set
    End Property


    Private _con As OleDbConnection
    Private _adp As OleDbDataAdapter
    Private _cmd As OleDbCommand
    'Private _cmd_type As CommandType = CommandType.StoredProcedure
    Private _cmd_text As String
    Private _connection_string As String
    Private _paramtable As DataTable
    Private _DtPrimaryExp As New DataTable
    Private _tra As OleDbTransaction
    'Private _jro As New JRO.JetEngine
    Private _tempdr As OleDbDataReader

    Public countAEL As Integer

    Public Sub insert_additional_expo(ByVal dtable As DataTable)
        ParamClear()  ' Clear any previous parameters
        Dim sConnectionString As String = Connection_string

        ' Create connection object using the connection string
        Dim objConn As New OleDbConnection(sConnectionString)

        ' Open connection with the database
        objConn.Open()

        ' Loop through each row in the DataTable
        For Each drow As DataRow In dtable.Rows
            ' Create the insert command with parameters
            Dim objCmdIns As New OleDbCommand("INSERT INTO Additional_AEL_expo (InsType, Symbol, ExpDate, StrikePrice, OptType, CALevel, ELMPer) " &
                                              "VALUES (@InsType, @Symbol, @ExpDate, @StrikePrice, @OptType, @CALevel, @ELMPer)", objConn)

            ' Add parameters to the command, ensuring correct data types
            objCmdIns.Parameters.Add("@InsType", OleDbType.VarChar, 50).Value = CStr(drow("InsType"))
            objCmdIns.Parameters.Add("@Symbol", OleDbType.VarChar, 50).Value = CStr(drow("Symbol"))
            objCmdIns.Parameters.Add("@ExpDate", OleDbType.Date).Value = CDate(drow("ExpDate"))
            objCmdIns.Parameters.Add("@StrikePrice", OleDbType.Double).Value = Val(drow("StrikePrice"))
            objCmdIns.Parameters.Add("@OptType", OleDbType.VarChar, 50).Value = CStr(drow("OptType"))
            objCmdIns.Parameters.Add("@CALevel", OleDbType.Double).Value = Val(drow("CALevel"))
            objCmdIns.Parameters.Add("@ELMPer", OleDbType.Double).Value = Val(drow("ELMPer"))

            ' Execute the insert query for each row
            objCmdIns.ExecuteNonQuery()
            countAEL = countAEL + 1

            ' Clear the parameters for the next iteration
            objCmdIns.Parameters.Clear()
        Next

        ' Close the connection
        objConn.Close()

        ' Reset the command text for any subsequent operations
        Cmd_Text = "insert_ael_Additional_Expo"

        ' Execute any additional steps (e.g., ExecuteMultiple)
        ExecuteMultiple(7)

    End Sub
    Public Sub Insert_AEL_Contracts(ByVal Symbol As String, ByVal InsType As String, ByVal ExpDate As Date, ByVal StrikePrice As Double, ByVal OptType As String, ByVal CALevel As Double, ByVal ELM_Per As Double)
        Try
            cmd_type = CommandType.StoredProcedure
            ParamClear()
            AddParam("@Symbol", OleDbType.VarChar, 50, CStr(Symbol))
            AddParam("@InsType", OleDbType.VarChar, 50, CStr(InsType))

            AddParam("@ExpDate", OleDbType.Date, 18, CDate(ExpDate))
            AddParam("@StrikePrice", OleDbType.Double, 18, Val(StrikePrice))
            AddParam("@OptType", OleDbType.VarChar, 50, CStr(OptType))
            AddParam("@CALevel", OleDbType.Double, 18, Val(CALevel))
            AddParam("@ELM_Per", OleDbType.Double, 18, Val(ELM_Per))

            Cmd_Text = "insert_ael_contracts"
            ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Trading :: Insert_Ael_Contracts() ::" & ex.ToString)
        End Try
    End Sub

    Public Sub AddParam(ByVal param_id As String, ByVal oledbtype As OleDbType, ByVal size As Integer, ByVal value As Object)
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

    Public Sub ParamClear()
        _paramtable = New DataTable()
        _paramtable.Columns.Add("ParameterName", GetType(String))
        _paramtable.Columns.Add("oleDbType", GetType(OleDbType))
        _paramtable.Columns.Add("Size", GetType(Integer))
        _paramtable.Columns.Add("Value", GetType(Object))
    End Sub


    Public ReadOnly Property Connection_string() As String
        Get
            Try
                Dim str As String = ConfigurationSettings.AppSettings("dbname")
                '_connection_string = ConfigurationSettings.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str
                '_connection_string = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=True;Data Source=D:\FinIdeas Projects\VolHedge 4.0.0.48 H13\volhedge\bin\x86\Debug/greekP.mdb;Jet OLEDB:Database Password=Admin"

                _connection_string = ConfigurationSettings.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str & ";Jet OLEDB:Database Password=" & clsGlobal.glbAcessPassWord & ""
                '"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="

                If _connection_string = "" Then
                    Throw New ApplicationException("Connection String is not initialize")
                End If
                Return _connection_string
            Catch ex As Exception
                'MsgBox(ex.ToString)
                'FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access::Connection_string:-" & ex.ToString)
                Write_ErrorLog3(Now.ToString() & "-" & "Data_access::Connection_string:-" & ex.ToString)
                Throw New ApplicationException("Connection String is not initialize")
            End Try
        End Get
    End Property

    Private Sub open_connection()
        Try
            If Connection_string <> "" Or (_con Is Nothing) Then
                _con = New OleDbConnection(Connection_string)
                _con.Open()
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString)
            '  FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access::open_connection:-" & ex.ToString)
            Write_ErrorLog3(Now.ToString() & "-" & "Data_access::open_connection:-" & ex.ToString)
            Throw New ApplicationException("Connection Error")
        End Try
    End Sub
    Private Sub Con_Transaction()
        If _con.State = ConnectionState.Open Then
            _tra = _con.BeginTransaction()
        End If
    End Sub

    Private Sub AddParamToSQLCmd()
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

    Private Sub AddParamToSQLCmd(ByVal drow As DataRow)
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

    Public Function ExecuteNonQuery() As DataTable

        Dim sqlstr As String = _cmd_text
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
            _cmd.CommandType = _cmd_type
            _cmd.CommandText = _cmd_text
            ' Con_Transaction()
            _cmd.Connection = _con
            '_cmd.Transaction = _tra
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
            'Con_Commit()
            Close_Connection()
            Return _DtPrimaryExp 'result.ToString()
        Catch ex As Exception
            'If FSTimerLogFile Is Nothing = False Then
            Write_ErrorLog3(Now.ToString() & "-" & "Data_access::ExecuteNonQuery:-" & sqlstr & "-" & ex.ToString)

            'FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access::ExecuteNonQuery:-" & sqlstr & "-" & ex.ToString)
            'End If
            'MsgBox(ex.ToString)
            ''Con_Rollback()
            Return Nothing
        Finally
            Close_Connection()
        End Try
    End Function

    Private Sub Con_Commit()
        _tra.Commit()
    End Sub
    Private Sub Con_Rollback()
        _tra.Rollback()
    End Sub
    Private Sub Close_Connection()
        Try


            If _con.State = ConnectionState.Open Then
                _con.Close()
            End If

        Catch ex As Exception

        End Try
    End Sub


    Public Sub UpDataDB(ByVal DT As DataTable)
        Dim ConnString As String = Connection_string
        Dim SQL As String = "SELECT *  FROM AEL_Contracts WHERE 1<>1"
        'Dim INSERT As String = "INSERT INTO MeterReadings(Meter1, Meter2, Meter3, Meter4) " +
        '               "VALUES (@Meter1, @Meter2, @Meter3, @Meter4)"
        Dim INSERT As String = "INSERT INTO ael_contracts ( InsType, Symbol, ExpDate, StrikePrice, OptType, CALevel, ELMPer ) " +
                        "VALUES (@InsType, @Symbol, @ExpDate, @StrikePrice, @OptType, @CALevel, @ELMPer);"

        Dim OleConn As New OleDbConnection(ConnString)
        Dim OleAdp As New OleDbDataAdapter(SQL, OleConn)
        OleAdp.InsertCommand = New OleDbCommand(INSERT)
        'OleAdp.InsertCommand.Parameters.Add("@Meter1", OleDbType.Integer, 8, "Meter1")
        'OleAdp.InsertCommand.Parameters.Add("@Meter2", OleDbType.Integer, 8, "Meter2")
        'OleAdp.InsertCommand.Parameters.Add("@Meter3", OleDbType.Integer, 8, "Meter3")
        'OleAdp.InsertCommand.Parameters.Add("@Meter4", OleDbType.Integer, 8, "Meter4")

        OleAdp.InsertCommand.Parameters.Add("@Symbol", OleDbType.VarChar, 50, "Symbol")
        OleAdp.InsertCommand.Parameters.Add("@InsType", OleDbType.VarChar, 50, "InsType")

        OleAdp.InsertCommand.Parameters.Add("@ExpDate", OleDbType.Date, 18, "ExpDate")
        OleAdp.InsertCommand.Parameters.Add("@StrikePrice", OleDbType.Double, 18, "StrikePrice")
        OleAdp.InsertCommand.Parameters.Add("@OptType", OleDbType.VarChar, 50, "OptType")
        OleAdp.InsertCommand.Parameters.Add("@CALevel", OleDbType.Double, 18, "CALevel")
        OleAdp.InsertCommand.Parameters.Add("@ELMPer", OleDbType.Double, 18, "ELMPer")

        OleAdp.InsertCommand.Connection = OleConn
        OleAdp.InsertCommand.Connection.Open()
        OleAdp.Update(DT)
        OleAdp.InsertCommand.Connection.Close()
    End Sub

    Public Sub CopyDatatableToAccess(ByVal dt As DataTable)
        Try
            Dim connString As String = Connection_string '"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & AccsessAddress & ";Persist Security Info=False;"
            Dim accConnection As New OleDb.OleDbConnection(connString)
            Dim selectCommand As String = "SELECT UId, InsType, Symbol, ExpDate, StrikePrice, OptType, CALevel, ELMPer FROM ael_contracts"
            Dim accDataAdapter As New OleDb.OleDbDataAdapter(selectCommand, accConnection)
            Dim accCommandBuilder As New OleDb.OleDbCommandBuilder(accDataAdapter)
            accDataAdapter.InsertCommand = accCommandBuilder.GetInsertCommand()
            accDataAdapter.UpdateCommand = accCommandBuilder.GetUpdateCommand()
            Dim accDataTable As DataTable = dt.Copy()
            ''Just to make sure, set the RowState to added to make sure an Insert is performed'
            For Each row As DataRow In accDataTable.Rows '
                If row.RowState = DataRowState.Added Or DataRowState.Unchanged Then '
                    row.SetAdded() '
                End If '
            Next '
            accDataAdapter.Update(accDataTable)
        Catch ex As Exception
            MsgBox("Error")
        End Try
    End Sub



    Public Sub insert(ByVal dtable As DataTable)
        ParamClear()
        
        For Each drow As DataRow In dtable.Rows


            AddParam("@Symbol", OleDbType.VarChar, 50, CStr(drow("Symbol")))
            AddParam("@InsType", OleDbType.VarChar, 50, CStr(drow("InsType")))

            AddParam("@ExpDate", OleDbType.Date, 18, CDate(drow("ExpDate")))
            AddParam("@StrikePrice", OleDbType.Double, 18, Val(drow("StrikePrice")))
            AddParam("@OptType", OleDbType.VarChar, 50, CStr(drow("OptType")))
            AddParam("@CALevel", OleDbType.Double, 18, Val(drow("CALevel")))
            AddParam("@ELMPer", OleDbType.Double, 18, Val(drow("ELMPer")))


            
        Next
        Cmd_Text = "insert_ael_contracts"
        '=================keval(16-2-10) changed(9-10) no in function
        ExecuteMultiple(7)
    End Sub


    Public Function ExecuteMultiple(ByVal parmcount As Integer) As DataTable
        Dim sqlstr As String = _cmd_text
        If _DtPrimaryExp.Columns.Count = 0 Then
            _DtPrimaryExp.Columns.Add("ParameterName", GetType(String))
            _DtPrimaryExp.Columns.Add("oleDbType", GetType(OleDbType))
            _DtPrimaryExp.Columns.Add("Size", GetType(Integer))
            _DtPrimaryExp.Columns.Add("Value", GetType(Object))
        End If
        _DtPrimaryExp.Rows.Clear()
        Try
            Dim result As String = ""
            countAEL = 0
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
                        countAEL = countAEL + 1
                    Catch ex1 As Exception
                        If ex1.Message.ToUpper.Contains("DUPLICATE VALUES IN THE INDEX, PRIMARY KEY, OR RELATIONSHIP") = True Then
                            For cnt As Integer = i To (i + parmcount - 1)
                                _DtPrimaryExp.ImportRow(_paramtable.Rows(cnt))
                            Next
                        Else
                            If IsScriptMapper = True Then
                            Else
                                Throw New ApplicationException(ex1.Message)
                            End If

                        End If
                    End Try
                Next
            End If
            If cmd_type = CommandType.Text Then
                cmd_type = CommandType.StoredProcedure
            End If
            Con_Commit()
            Close_Connection()
            Return _DtPrimaryExp
        Catch ex As Exception
            'FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access::ExecuteMultiple:-" & sqlstr & "-" & ex.ToString)
            Write_ErrorLog3(Now.ToString() & "-" & "Data_access::ExecuteMultiple:-" & sqlstr & "-" & ex.ToString)
            MsgBox(ex.ToString)
            Con_Rollback()
        Finally
            Close_Connection()
        End Try
    End Function

End Class
