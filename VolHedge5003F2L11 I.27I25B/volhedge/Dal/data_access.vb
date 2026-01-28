Imports System
Imports System.Data
Imports System.Collections
Imports System.Data.OleDb
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration

Namespace DAL

    Public MustInherit Class data_access

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
        'Shared _jro As New JRO.JetEngine
        Shared _tempdr As OleDbDataReader
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

        'Public Shared Property Conn() As OleDbConnection
        '    Get
        '        open_connection()
        '        Return _con
        '    End Get
        '    Set(ByVal value As OleDbConnection)
        '        _con = value
        '    End Set
        'End Property


        Public Shared ReadOnly Property Connection_string() As String
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
        Private Shared Sub open_connection()
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
            Try


                If _con.State = ConnectionState.Open Then
                    _con.Close()
                End If

            Catch ex As Exception

            End Try
        End Sub
        'Public Shared Sub ParamClear()
        '    _paramtable = New DataTable()
        '    _paramtable.Columns.Add("ParameterName", GetType(String))
        '    _paramtable.Columns.Add("oleDbType", GetType(OleDbType))
        '    _paramtable.Columns.Add("Size", GetType(Integer))
        '    _paramtable.Columns.Add("Value", GetType(Object))
        'End Sub
        'Public Shared Sub AddParam(ByVal param_id As String, ByVal oledbtype As OleDbType, ByVal size As Integer, ByVal value As Object)
        '    Dim drow As DataRow = _paramtable.NewRow()
        '    drow("ParameterName") = param_id
        '    drow("oledbtype") = oledbtype
        '    drow("Size") = size
        '    If value Is Nothing Then
        '        drow("Value") = DBNull.Value
        '    Else
        '        drow("Value") = value
        '    End If

        '    _paramtable.Rows.Add(drow)
        'End Sub
        Public Shared Function ExecuteNonQuery_AEL() As Boolean
            Dim sqlstr As String = _cmd_text
            Try
                _cmd_type = CommandType.Text  ' Set command type to Text for regular SQL queries
                _cmd = New OleDbCommand()
                _cmd.CommandType = _cmd_type
                _cmd.CommandText = _cmd_text  ' The CREATE TABLE query

                open_connection()
                Con_Transaction()

                _cmd.Connection = _con
                _cmd.Transaction = _tra

                ' Execute the query as a non-query
                _cmd.ExecuteNonQuery()

                ' Commit transaction and close connection
                Con_Commit()
                Close_Connection()

                Return True  ' Successfully executed the query
            Catch ex As Exception
                'Write_ErrorLog3(Now.ToString() & "-" & "Data_access::ExecuteNonQuery:-" & sqlstr & "-" & ex.ToString())
                'MsgBox("data_access :: ExecuteNonQuery() :: " & sqlstr & "-" & ex.ToString())
                'Con_Rollback()  ' Rollback in case of error
                Return False  ' Failed to execute the query
            Finally
                ' Reset command type after execution
                If _cmd_type = CommandType.Text Then
                    _cmd_type = CommandType.StoredProcedure
                End If
                Close_Connection()
            End Try
        End Function
        Public Shared Function FillList_sql() As DataTable
            Dim sqlstr As String = _cmd_text
            Try
                Dim list As DataTable = New DataTable()
                list.Rows.Clear()

                ' Set the command type to Text for executing regular SQL queries
                _cmd_type = CommandType.Text  ' This ensures we're using a regular SQL query (not a stored procedure)

                _adp = New OleDbDataAdapter()
                open_connection()
                Con_Transaction()

                ' Create the command object
                _cmd = New OleDbCommand()
                _cmd.CommandType = _cmd_type
                _cmd.CommandText = _cmd_text  ' This should be your SELECT query

                _cmd.Connection = _con
                _cmd.Transaction = _tra

                ' Add parameters if any (optional)
                If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                    AddParamToSQLCmd()
                End If

                ' Set the adapter and execute the query
                _adp.SelectCommand = _cmd
                _adp.Fill(list)  ' This will execute the query and fill the DataTable with results

                ' Dispose of the adapter and commit the transaction
                _adp.Dispose()
                Con_Commit()
                Close_Connection()

                ' Return the filled DataTable
                Return list
            Catch ex As Exception
                ' Handle any errors and log them
                Write_ErrorLog3(Now.ToString() & "-" & "Data_access::FillList:-" & sqlstr & "-" & ex.ToString())
                MsgBox("data_access :: FillList() :: " & sqlstr & "-" & ex.ToString())
                Con_Rollback()  ' Rollback in case of an error
                Return Nothing
            Finally
                ' Reset command type for future use
                If cmd_type = CommandType.Text Then
                    cmd_type = CommandType.StoredProcedure
                End If
                Close_Connection()  ' Ensure connection is closed after operation
            End Try
        End Function
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
        Public Shared Function ExecuteNonQuery() As DataTable

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
                'If FSTimerLogFile Is Nothing = False Then
                Write_ErrorLog3(Now.ToString() & "-" & "Data_access::ExecuteNonQuery:-" & sqlstr & "-" & ex.ToString)

                'FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access::ExecuteNonQuery:-" & sqlstr & "-" & ex.ToString)
                'End If
                'MsgBox(ex.ToString)
                Con_Rollback()
                Return Nothing
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
                'MsgBox(ex.ToString)
                Con_Rollback()
            Finally
                Close_Connection()
            End Try
        End Function
        Public Shared Sub CreateView(ByVal ViewName As String, ByVal view As String)
            Dim str As String
            Dim dr As OleDbDataReader
            Try
                _con.Open()
                str = "create proc " & ViewName & " as " & view & ""
                _cmd = New OleDbCommand(str, _con)
                _cmd.ExecuteNonQuery()
                _con.Close()
            Catch ex As Exception
            Finally
                _con.Close()
            End Try


        End Sub
        REM By Viral on 10-July-11
        'Public Shared Sub ImportContract(ByRef sFile As String)
        '    Dim date1 As Date = "1/1/1980"
        '    Dim strScript As String

        '    ParamClear()
        '    Cmd_Text = "contract_delete"
        '    ExecuteNonQuery()

        '    Try
        '        Dim i As Integer = 0
        '        Dim result As String = ""
        '        open_connection()
        '        Dim iline As New System.IO.StreamReader(sFile)
        '        While iline.EndOfStream = False
        '            If i > 0 Then
        '                Dim items As String()
        '                items = Split(iline.ReadLine, "|")
        '                If items(2) <> "" Then
        '                    strScript = ""
        '                    '8 = OptType
        '                    '2 =InstrumentName
        '                    '3 = Symbol
        '                    '6 = Exp_Date
        '                    If items(8) <> "" Then
        '                        If Mid(UCase(items(8)), 1, 1) = "C" Or Mid(UCase(items(8)), 1, 1) = "P" Then
        '                            strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6) & ""), date1), "ddMMMyyyy") & "  " & CStr(Format(Val(items(7)), "###0.00")) & "  " & items(8)
        '                        Else
        '                            strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6) & ""), date1), "ddMMMyyyy")
        '                        End If
        '                    Else
        '                        strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6) & ""), date1), "ddMMMyyyy")
        '                    End If

        '                    Dim StrSql As String = "INSERT INTO contract (token, asset_tokan, instrumentname, symbol, series, expiry_date, strike_price, option_type, script, lotsize )"
        '                    StrSql = StrSql & "VALUES (" & Val(items(0)) & ", " & Val(items(1)) & ", '" & items(2) & "', '" & items(3) & "', '" & items(4) & "', " & Val(items(6)) & ", " & Val(items(7)) & ",'" & items(8) & "', '" & strScript & "', " & Val(items(31)) & ")"

        '                    _cmd = New OleDbCommand()
        '                    _cmd.CommandType = CommandType.Text
        '                    _cmd.CommandText = StrSql
        '                    _cmd.Connection = _con
        '                    _cmd.ExecuteNonQuery()
        '                End If
        '            End If
        '            i = i + 1
        '        End While
        '        iline.Close()
        '        Close_Connection()
        '    Catch ex As Exception
        '        FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
        '        'MsgBox(ex.ToString)
        '    Finally
        '        Close_Connection()
        '    End Try
        'End Sub

        REM By Viral on 10-July-11
        'Public Shared Sub ImportSecurity(ByRef sFile As String)
        '    Dim date1 As Date = "1/1/1980"
        '    Dim strScript As String

        '    ParamClear()
        '    Cmd_Text = "delete_security"
        '    ExecuteNonQuery()

        '    Try
        '        Dim i As Integer = 0
        '        Dim result As String = ""
        '        open_connection()
        '        Dim iline As New System.IO.StreamReader(sFile)
        '        Dim items As String()
        '        items = Split(iline.ReadLine, "|")
        '        While iline.EndOfStream = False
        '            If i > 0 Then

        '                items = Split(iline.ReadLine, "|")
        '                If items(2) <> "" Then
        '                    strScript = ""
        '                    '0 = Token
        '                    '1 = Symbol
        '                    '2 = Series
        '                    '46 = Isin
        '                    'If items(8) <> "" Then
        '                    '    If Mid(UCase(items(8)), 1, 1) = "C" Or Mid(UCase(items(8)), 1, 1) = "P" Then
        '                    '        strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6) & ""), date1), "ddMMMyyyy") & "  " & CStr(Format(Val(items(7)), "###0.00")) & "  " & items(8)
        '                    '    Else
        '                    '        strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6) & ""), date1), "ddMMMyyyy")
        '                    '    End If
        '                    'Else
        '                    '    strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6) & ""), date1), "ddMMMyyyy")
        '                    'End If

        '                    Dim StrSql As String = "INSERT INTO Security (token, symbol, series, isin, script )"
        '                    StrSql = StrSql & "VALUES (" & Val(items(0)) & ", '" & items(1) & "', '" & items(2) & "', '" & items(46) & "', '" & items(1) & "  " & items(2) & "')"

        '                    _cmd = New OleDbCommand()
        '                    _cmd.CommandType = CommandType.Text
        '                    _cmd.CommandText = StrSql
        '                    _cmd.Connection = _con
        '                    _cmd.ExecuteNonQuery()
        '                End If
        '            End If
        '            i = i + 1
        '        End While
        '        iline.Close()
        '        Close_Connection()
        '    Catch ex As Exception
        '        FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
        '        'MsgBox(ex.ToString)
        '    Finally
        '        Close_Connection()
        '    End Try
        'End Sub

        REM By Viral on 10-July-11
        'Public Shared Sub ImportCurrency(ByRef sFile As String)
        '    Dim date1 As Date = "1/1/1980"
        '    Dim strScript As String
        '    Dim douStrikePrice As Double
        '    ParamClear()
        '    Cmd_Text = "Delete_Currency_Contract"
        '    ExecuteNonQuery()

        '    Try
        '        Dim i As Integer = 0
        '        Dim result As String = ""
        '        open_connection()
        '        Dim iline As New System.IO.StreamReader(sFile)
        '        Dim items As String()
        '        items = Split(iline.ReadLine, "|")
        '        While iline.EndOfStream = False
        '            If i > 0 Then
        '                items = Split(iline.ReadLine, "|")
        '                If items(2) <> "" Then
        '                    strScript = ""
        '                    douStrikePrice = 0
        '                    '0 = token
        '                    '1 = asset_Token
        '                    '2 = InstrumentName
        '                    '3 = Symbol
        '                    '4 = Series
        '                    '6 = Exp_Date
        '                    '7 = Strike Price
        '                    '8 = Option Type
        '                    '31 = LotSize
        '                    '51 = Multiplier
        '                    douStrikePrice = Format(CDbl(items(7)) / 10000000, "#0.0000")
        '                    If Not IsDBNull(items(8)) Then
        '                        If Mid(UCase(items(8)), 1, 1) = "C" Or Mid(UCase(items(8)), 1, 1) = "P" Then
        '                            strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6)), date1), "ddMMMyyyy") & "  " & CStr(Format(Val(douStrikePrice), "###0.0000")) & "  " & items(8)
        '                        Else
        '                            strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6)), date1), "ddMMMyyyy")
        '                        End If
        '                    Else
        '                        strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6)), date1), "ddMMMyyyy")
        '                    End If

        '                    Dim StrSql As String = "INSERT INTO Currency_Contract ( token, asset_tokan, instrumentname, symbol, series, expiry_date, strike_price, option_type, script, lotsize, multiplier )"
        '                    StrSql = StrSql & "VALUES (" & Val(items(0)) & ", " & Val(items(1)) & ", '" & items(2) & "', '" & items(3) & "', '" & items(4) & "', " & Val(items(6)) & ", " & Val(douStrikePrice) & ", '" & items(8) & "', '" & strScript & "', " & Val(items(31)) & ", " & Val(items(51)) & ")"
        '                    _cmd = New OleDbCommand()
        '                    _cmd.CommandType = CommandType.Text
        '                    _cmd.CommandText = StrSql
        '                    _cmd.Connection = _con
        '                    _cmd.ExecuteNonQuery()
        '                End If
        '                    End If
        '                    i = i + 1
        '        End While
        '        iline.Close()
        '        Close_Connection()
        '    Catch ex As Exception
        '        FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
        '        'MsgBox(ex.ToString)
        '    Finally
        '        Close_Connection()
        '    End Try
        'End Sub

        'Public Shared Sub ExecuteNonQuery(ByRef Rows() As String, ByVal s123 As String)
        '    Dim date1 As Date = "1/1/1980"
        '    Dim strScript As String
        '    Try
        '        Dim result As String = ""
        '        open_connection()

        '        For Each r As String In Rows
        '            Dim items As String() = r.Split("|".ToCharArray())
        '            strScript = ""
        '            '8 = OptType
        '            '2 =InstrumentName
        '            '3 = Symbol
        '            '6 = Exp_Date
        '            If items(8) <> "" Then
        '                If Mid(UCase(items(8)), 1, 1) = "C" Or Mid(UCase(items(8)), 1, 1) = "P" Then
        '                    strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6) & ""), date1), "ddMMMyyyy") & "  " & CStr(Format(Val(items(7)), "###0.00")) & "  " & items(8)
        '                Else
        '                    strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6) & ""), date1), "ddMMMyyyy")
        '                End If
        '            Else
        '                strScript = items(2) & "  " & items(3) & "  " & Format(DateAdd(DateInterval.Second, Val(items(6) & ""), date1), "ddMMMyyyy")
        '            End If

        '            Dim StrSql As String = "INSERT INTO contract (token, asset_tokan, instrumentname, symbol, series, expiry_date, strike_price, option_type, script, lotsize )"
        '            StrSql = StrSql & "VALUES (" & Val(items(0)) & ", " & Val(items(1)) & ", '" & items(2) & "', '" & items(3) & "', '" & items(4) & "', " & Val(items(6)) & ", " & Val(items(7)) & ",'" & items(8) & "', '" & strScript & "', " & Val(items(31)) & ")"

        '            _cmd = New OleDbCommand()
        '            _cmd.CommandType = CommandType.Text
        '            _cmd.CommandText = StrSql
        '            _cmd.Connection = _con
        '            _cmd.ExecuteNonQuery()
        '        Next
        '        Close_Connection()
        '    Catch ex As Exception
        '        FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
        '        'MsgBox(ex.ToString)
        '    Finally
        '        Close_Connection()
        '    End Try
        'End Sub

        Public Shared Function ExecuteMultiple(ByVal parmcount As Integer) As DataTable
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
        Public Shared Function ExecuteScalar() As String
            Dim sqlstr As String = _cmd_text
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
                '  Close_Connection()
                Return result.ToString()
            Catch ex As Exception
                ' FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access::ExecuteScalar:-" & sqlstr & "-" & ex.ToString)
                Write_ErrorLog3(Now.ToString() & "-" & "Data_access::ExecuteScalar:-" & sqlstr & "-" & ex.ToString)
                'MsgBox(ex.ToString)
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


            Dim sqlstr As String = _cmd_text
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
                'FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access::FillList:-" & sqlstr & "-" & ex.ToString)
                Write_ErrorLog3(Now.ToString() & "-" & "Data_access::FillList:-" & sqlstr & "-" & ex.ToString)
                MsgBox("data_access :: FillList() :: " & sqlstr & "-" & ex.ToString)
                Con_Rollback()
                Return Nothing
                '  MsgBox(ex.ToString)
            Finally
                If cmd_type = CommandType.Text Then
                    cmd_type = CommandType.StoredProcedure
                End If
                Close_Connection()
            End Try

        End Function

        ''' <summary>
        ''' FillList With ByRef Expression Od Datatable is use to Fatch record 
        ''' it Is little bit Faster Then Othere FillList Function
        ''' By Viral 
        ''' </summary>
        ''' <param name="dt"></param>
        ''' <remarks></remarks>
        Public Shared Sub FillList(ByRef dt As DataTable)
            Dim sqlstr As String = _cmd_text
            Try
                dt.Rows.Clear()
                _adp = New OleDbDataAdapter
                open_connection()
                'Con_Transaction()
                _cmd = New OleDbCommand()
                _cmd.CommandType = _cmd_type
                _cmd.CommandText = _cmd_text
                _cmd.Connection = _con
                '_cmd.Transaction = _tra
                If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                    AddParamToSQLCmd()
                End If
                _adp.SelectCommand = _cmd
                _adp.Fill(dt)
                _adp.Dispose()
                'Con_Commit()

                Close_Connection()
                If cmd_type = CommandType.Text Then
                    cmd_type = CommandType.StoredProcedure
                End If
            Catch ex As Exception
                '  FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access::FillTable:-" & sqlstr & "-" & ex.ToString)
                'Write_ErrorLog3(Now.ToString() & "-" & "Data_access::FillTable:-" & sqlstr & "-" & ex.ToString)
                'MsgBox("data_access :: FillList() :: " & sqlstr & "-" & ex.ToString)
                'Con_Rollback()

                '  MsgBox(ex.ToString)
            Finally
                Close_Connection()
            End Try

        End Sub

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
                'FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access::ExecuteReturn:-" & ex.ToString)
                Write_ErrorLog3(Now.ToString() & "-" & "Data_access::ExecuteReturn:-" & ex.ToString)
                Return ""
                Con_Rollback()
            Finally
                Close_Connection()
            End Try
        End Function

        Public Shared Sub ExecuteQuery(ByVal Str As String)
            Try

                If _con.State = ConnectionState.Closed Then open_connection()
                Dim cmd As New OleDbCommand(Str, _con)
                cmd.ExecuteNonQuery()

                Return
            Catch ex As Exception
                'FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access::ExecuteQuery:-" & ex.ToString)
                Write_ErrorLog3(Now.ToString() & "-" & "Data_access::ExecuteQuery:-" & ex.ToString)
                Return
                Con_Rollback()
            Finally
                Close_Connection()
            End Try
        End Sub
#End Region

    End Class


End Namespace
