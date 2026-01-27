Imports System
Imports System.Data
Imports System.Collections
Imports System.Data.SqlClient
'Imports System.Data.Odbc
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration
Namespace UDAL
    Public Class data_access_sql
        Private ExecuteNonQueryLock As New Object
        Private ExecuteQuery_backupLock As New Object
        Private ExecuteQueryLock As New Object
        Private ExecuteQuery2Lock As New Object
        Private ExecuteMultipleRCLock As New Object
        Private ExecuteMultipleLock As New Object
        Private ExecuteScalarLock As New Object

#Region "variable"
        Public _con As New SqlConnection
        Public _conDW As New SqlConnection
        Public _conCW As New SqlConnection
        Public _conGW As New SqlConnection
        Public _conGSW As New SqlConnection
        Public _conMW As New SqlConnection
        Public _conMSW As New SqlConnection


        Dim _adp As New SqlDataAdapter
        Dim _cmd As New SqlCommand
        Dim _cmd_type As CommandType = CommandType.StoredProcedure
        Dim _cmd_text As String
        Shared _connection_string As String
        Dim _paramtable As New DataTable
        Dim _DtPrimaryExp As New DataTable
        Dim _tra As SqlTransaction

        Dim _adpDW As New SqlDataAdapter
        Dim _cmdDW As New SqlCommand
        Dim _cmd_typeDW As CommandType = CommandType.StoredProcedure
        Dim _cmd_textDW As String
        Dim _paramtableDW As New DataTable
        Dim _DtPrimaryExpDW As New DataTable
        Dim _traDw As SqlTransaction

        Dim _adpCW As New SqlDataAdapter
        Dim _cmdCW As New SqlCommand
        Dim _cmd_typeCW As CommandType = CommandType.StoredProcedure
        Dim _cmd_textCW As String
        Dim _paramtableCW As New DataTable
        Dim _DtPrimaryExpCW As New DataTable
        Dim _traCW As SqlTransaction

#End Region
#Region "Method"
        Public Property Cmd_Text() As String
            Get
                Return _cmd_text
            End Get
            Set(ByVal value As String)
                _cmd_text = value
            End Set
        End Property
        Public Property Cmd_TextDW() As String
            Get
                Return _cmd_textDW
            End Get
            Set(ByVal value As String)
                _cmd_textDW = value
            End Set
        End Property
        Public Property Cmd_TextCW() As String
            Get
                Return _cmd_textCW
            End Get
            Set(ByVal value As String)
                _cmd_textCW = value
            End Set
        End Property
        Private ReadOnly Property Connection_string() As String
            Get
                If GVarSQLServerConnStr = "" Then
                    Try
                        Dim StrConn As String = ""


                        'StrConn &= "Data Source=" & RegServerIP
                        'StrConn &= ";Initial Catalog=" & "finideas"
                        'StrConn &= ";MultipleActiveResultSets = True"
                        'StrConn &= ";User ID=" & RegServerUserid
                        'StrConn &= ";Password=" & "finideas#123"
                        'StrConn &= ";Connect Timeout=500"
                        StrConn &= "Data Source=" & RegServerIP
                        StrConn &= ";Initial Catalog=" & RegServerdbnm '"finideas"
                        StrConn &= ";MultipleActiveResultSets = True"
                        StrConn &= ";User ID=" & RegServerUserid
                        StrConn &= ";Password=" & RegServerpwd '"finideas#123"
                        StrConn &= ";Application Name=" & "VH_REG_" & RegServerIP & ""
                        StrConn &= ";Asynchronous Processing=True"
                        StrConn &= ";Connection Timeout=600"

                        GVarSQLServerConnStr = StrConn 'ConfigurationSettings.AppSettings("ConnStringSQLServer")
                        If GVarSQLServerConnStr = "" Then
                            Throw New ApplicationException("Connection String is not initialize")
                        End If
                        Return GVarSQLServerConnStr
                    Catch ex As Exception
                        Throw New ApplicationException("Connection String is not initialize")
                    End Try
                Else
                    Return GVarSQLServerConnStr
                End If
            End Get
        End Property
        Private ReadOnly Property Connection_stringRC() As String
            Get

                Try
                    Dim StrConn As String = ""


                    'StrConn &= "Data Source=" & "finideas.com,1092"
                    'StrConn &= ";Initial Catalog=" & "finideas"
                    'StrConn &= ";MultipleActiveResultSets = True"
                    'StrConn &= ";User ID=" & "finideas"
                    'StrConn &= ";Password=" & "finideas#123"
                    'StrConn &= ";Application Name=" & "VH_" & "Finideas.com" & ""

                    'StrConn &= "Data Source=" & "finideas.com,1433" ',1092"
                    StrConn &= "Data Source=" & gstr_Internet_Server

                    StrConn &= ";Initial Catalog=" & gstr_Internet_DB
                    StrConn &= ";MultipleActiveResultSets = True"
                    StrConn &= ";User ID=" & gstr_Internet_Uid
                    StrConn &= ";Password=" & gstr_Internet_Pwd
                    StrConn &= ";Application Name=" & "VH_" & gstr_Internet_Server & ""
                    If StrConn = "" Then
                        Throw New ApplicationException("Connection String is not initialize")
                    End If
                    Return StrConn
                Catch ex As Exception
                    Throw New ApplicationException("Connection String is not initialize")
                End Try

            End Get
        End Property
        Public Sub open_connection()
            Try
lbl:


                If _con.ConnectionString = "" Then
                    _con = New SqlConnection(Connection_string)
                    If _con.State = ConnectionState.Closed Then _con.Open()
                Else
                    '_con = New OleDbConnection(Connection_string)
                    If _con.State = ConnectionState.Closed Then _con.Open()
                End If
            Catch ex As Exception
                'If ex.ToString.ToUpper.Contains("UNSPECIFIED ERROR") = True Then
                '    GoTo lbl
                'End If
                'Throw New ApplicationException("SQL Server Connection Problem")
            End Try
        End Sub
        Public Sub open_connectionRC(ByRef con As SqlConnection)
            Try
                con = New SqlConnection(Connection_stringRC)
                If con.State = ConnectionState.Closed Then con.Open()

            Catch ex As Exception
                'If ex.ToString.ToUpper.Contains("UNSPECIFIED ERROR") = True Then
                '    GoTo lbl
                'End If
                'Throw New ApplicationException("SQL Server Connection Problem")
            End Try
        End Sub
        Private Sub Con_Transaction()
            If _con.State = ConnectionState.Open Then
                _tra = _con.BeginTransaction(IsolationLevel.Unspecified)
            End If
        End Sub

        Private Sub Con_Transaction(ByRef con As SqlConnection)
            If con.State = ConnectionState.Open Then
                _tra = con.BeginTransaction(IsolationLevel.Unspecified)
            End If
        End Sub

        Private Sub Con_Commit()
            _tra.Commit()
        End Sub
        Private Sub Con_Rollback()
            Try
                _tra.Rollback()
            Catch ex As Exception
            End Try
        End Sub
        Public Sub Close_Connection()
            If _con.State = ConnectionState.Open Then
                _con.Close()
            End If
        End Sub
        '---------------
        Public Sub open_connectionDW()
            Try
lbl:
                If _conDW.ConnectionString = "" Then
                    _conDW = New SqlConnection(Connection_string)
                    If _conDW.State = ConnectionState.Closed Then _conDW.Open()
                Else
                    '_con = New OleDbConnection(Connection_string)
                    If _conDW.State = ConnectionState.Closed Then _conDW.Open()
                End If
            Catch ex As Exception
                'If ex.ToString.ToUpper.Contains("UNSPECIFIED ERROR") = True Then
                '    GoTo lbl
                'End If
                ' Throw New ApplicationException("SQL Server Connection Problem")'change by payalpatel
            End Try
        End Sub
        Private Sub ConDw_Transaction()
            If _conDW.State = ConnectionState.Open Then
                _traDw = _conDW.BeginTransaction(IsolationLevel.Unspecified)
            End If
        End Sub
        Private Sub ConDw_Commit()
            _traDw.Commit()
        End Sub
        Private Sub Condw_Rollback()
            Try
                _traDw.Rollback()
            Catch ex As Exception
            End Try
        End Sub
        Public Sub Closedw_Connection()
            If _conDW.State = ConnectionState.Open Then
                _conDW.Close()
            End If
        End Sub
        '---------------
        '-----------------Cw
        Public Sub open_connectionCW()
            Try
lbl:
                If _conCW.ConnectionString = "" Then
                    _conCW = New SqlConnection(Connection_string)
                    If _conCW.State = ConnectionState.Closed Then _conCW.Open()
                Else
                    '_con = New OleDbConnection(Connection_string)
                    If _conCW.State = ConnectionState.Closed Then _conCW.Open()
                End If
            Catch ex As Exception
                'If ex.ToString.ToUpper.Contains("UNSPECIFIED ERROR") = True Then
                '    GoTo lbl
                'End If
                ' Throw New ApplicationException("SQL Server Connection Problem")'change by payalpatel
            End Try
        End Sub
        Private Sub ConCW_Transaction()
            If _conCW.State = ConnectionState.Open Then
                _traCW = _conCW.BeginTransaction(IsolationLevel.Unspecified)
            End If
        End Sub
        Private Sub ConCW_Commit()
            _traCW.Commit()
        End Sub
        Private Sub ConCW_Rollback()
            Try
                _traCW.Rollback()
            Catch ex As Exception
            End Try
        End Sub
        Public Sub CloseCW_Connection()
            If _conCW.State = ConnectionState.Open Then
                _conCW.Close()
            End If
        End Sub
        '------------------Cw
        Public Sub open_connectionGW()
            Try
lbl:
                If _conGW.ConnectionString = "" Then
                    _conGW = New SqlConnection(Connection_string)
                    If _conGW.State = ConnectionState.Closed Then _conGW.Open()
                Else
                    '_con = New OleDbConnection(Connection_string)
                    If _conGW.State = ConnectionState.Closed Then _conGW.Open()
                End If
            Catch ex As Exception
                'If ex.ToString.ToUpper.Contains("UNSPECIFIED ERROR") = True Then
                '    GoTo lbl
                'End If
                ' Throw New ApplicationException("SQL Server Connection Problem")'change by payalpatel
            End Try
        End Sub
        Public Sub open_connectionGSW()
            Try
lbl:
                If _conGSW.ConnectionString = "" Then
                    _conGSW = New SqlConnection(Connection_string)
                    If _conGSW.State = ConnectionState.Closed Then _conGSW.Open()
                Else
                    '_con = New OleDbConnection(Connection_string)
                    If _conGSW.State = ConnectionState.Closed Then _conGSW.Open()
                End If
            Catch ex As Exception
                'If ex.ToString.ToUpper.Contains("UNSPECIFIED ERROR") = True Then
                '    GoTo lbl
                'End If
                ' Throw New ApplicationException("SQL Server Connection Problem")'change by payalpatel
            End Try
        End Sub
        Public Sub ParamClear()
            If _paramtable.Columns.Count = 0 Then
                _paramtable.Columns.Add("ParameterName", GetType(String))
                _paramtable.Columns.Add("SqlDbType", GetType(SqlDbType))
                _paramtable.Columns.Add("Size", GetType(Integer))
                _paramtable.Columns.Add("Value", GetType(Object))

                _DtPrimaryExp.Columns.Add("orderno", GetType(String))
                _DtPrimaryExp.Columns.Add("entryno", GetType(Integer))
            Else
                _paramtable.Rows.Clear()
                _DtPrimaryExp.Rows.Clear()
            End If
        End Sub
        Public Sub ParamCleardw()
            If _paramtableDW.Columns.Count = 0 Then
                _paramtableDW.Columns.Add("ParameterName", GetType(String))
                _paramtableDW.Columns.Add("SqlDbType", GetType(SqlDbType))
                _paramtableDW.Columns.Add("Size", GetType(Integer))
                _paramtableDW.Columns.Add("Value", GetType(Object))

                _DtPrimaryExpDW.Columns.Add("orderno", GetType(String))
                _DtPrimaryExpDW.Columns.Add("entryno", GetType(Integer))
            Else
                _paramtableDW.Rows.Clear()
                _DtPrimaryExpDW.Rows.Clear()
            End If
        End Sub
        Public Sub ParamClearCw()
            If _paramtableCW.Columns.Count = 0 Then
                _paramtableCW.Columns.Add("ParameterName", GetType(String))
                _paramtableCW.Columns.Add("SqlDbType", GetType(SqlDbType))
                _paramtableCW.Columns.Add("Size", GetType(Integer))
                _paramtableCW.Columns.Add("Value", GetType(Object))

                _DtPrimaryExpCW.Columns.Add("orderno", GetType(String))
                _DtPrimaryExpCW.Columns.Add("entryno", GetType(Integer))
            Else
                _paramtableCW.Rows.Clear()
                _DtPrimaryExpCW.Rows.Clear()
            End If
        End Sub
        Public Sub AddParam(ByVal param_id As String, ByVal sqldbtype As SqlDbType, ByVal size As Integer, ByVal value As Object)
            Dim drow As DataRow = _paramtable.NewRow()
            drow("ParameterName") = param_id
            drow("SqlDbType") = sqldbtype
            drow("Size") = size
            If Not value Is Nothing Then
                drow("Value") = value
            End If
            _paramtable.Rows.Add(drow)
        End Sub
        Public Sub AddParamDw(ByVal param_id As String, ByVal sqldbtype As SqlDbType, ByVal size As Integer, ByVal value As Object)
            Dim drow As DataRow = _paramtableDW.NewRow()
            drow("ParameterName") = param_id
            drow("SqlDbType") = sqldbtype
            drow("Size") = size
            If Not value Is Nothing Then
                drow("Value") = value
            End If
            _paramtableDW.Rows.Add(drow)
        End Sub
        Public Sub AddParamCw(ByVal param_id As String, ByVal sqldbtype As SqlDbType, ByVal size As Integer, ByVal value As Object)
            Dim drow As DataRow = _paramtableCW.NewRow()
            drow("ParameterName") = param_id
            drow("SqlDbType") = sqldbtype
            drow("Size") = size
            If Not value Is Nothing Then
                drow("Value") = value
            End If
            _paramtableCW.Rows.Add(drow)
        End Sub
        Public Function ExecuteNonQuery() As String
            SyncLock ExecuteNonQueryLock
                Try
                    Dim result As String = ""
                    If _con.State = ConnectionState.Closed Then open_connection()
                    _cmd = New SqlCommand()
                    _cmd.CommandType = _cmd_type
                    _cmd.CommandText = _cmd_text
                    Con_Transaction()
                    _cmd.Connection = _con
                    _cmd.CommandTimeout = 0
                    _cmd.Transaction = _tra
                    If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                        AddParamToSQLCmd()
                    End If
                    result = Convert.ToString(_cmd.ExecuteNonQuery())
                    Con_Commit()
                    'Close_Connection()
                    Return result.ToString()
                Catch ex As Exception
                    Con_Rollback()
                    MsgBox("Data_Access_SQL :: ExecuteNonQuery() ::" & ex.Message)
                    Return ""
                Finally
                    'Close_Connection()
                End Try
            End SyncLock
        End Function
        Public Function ExecuteQuery_backup(ByVal VarQuery As String) As String
            SyncLock ExecuteQuery_backupLock
                Try
                    Dim _con1 As New SqlConnection
                    _con1 = New SqlConnection(Connection_string)
                    If _con1.State = ConnectionState.Closed Then _con.Open()
                    Dim result As String = ""
                    If _con.State = ConnectionState.Closed Then open_connection()
                    _cmd = New SqlCommand()
                    _cmd.CommandText = VarQuery
                    Con_Transaction()
                    _cmd.Connection = _con
                    _cmd.CommandTimeout = 600
                    _cmd.Transaction = _tra
                    If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                        AddParamToSQLCmd()
                    End If
                    result = Convert.ToString(_cmd.ExecuteNonQuery())
                    Con_Commit()
                    'Close_Connection()
                    Return result.ToString()
                Catch ex As Exception
                    Con_Rollback()
                    MsgBox("Data_Access_SQL :: ExecuteQuery(VarQuery) ::" & ex.Message)
                    Return ""
                Finally
                    'Close_Connection()
                End Try
            End SyncLock
        End Function
        Public Function ExecuteQuery(ByVal VarQuery As String) As String
            SyncLock ExecuteQueryLock
                Try
                    Dim _con1 As New SqlConnection
                    _con1 = New SqlConnection(Connection_string)
                    If _con1.State = ConnectionState.Closed Then _con1.Open()
                    Dim result As String = ""
                    ' If _con.State = ConnectionState.Closed Then open_connection()
                    _cmd = New SqlCommand()
                    _cmd.CommandText = VarQuery
                    'Con_Transaction()
                    If _con1.State = ConnectionState.Open Then
                        _tra = _con1.BeginTransaction(IsolationLevel.Unspecified)
                    End If
                    _cmd.Connection = _con1
                    _cmd.CommandTimeout = 600
                    _cmd.Transaction = _tra
                    If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                        AddParamToSQLCmd()
                    End If
                    result = Convert.ToString(_cmd.ExecuteNonQuery())
                    Con_Commit()
                    _con1.Close()
                    'Close_Connection()
                    Return result.ToString()
                Catch ex As Exception
                    Con_Rollback()
                    Write_TimeLog1("Data_Access_SQL :: ExecuteQuery(VarQuery) ::Qyery=" & VarQuery & " " & ex.Message)
                    MsgBox("Data_Access_SQL :: ExecuteQuery(VarQuery) ::" & ex.Message)
                    Return ""
                Finally
                    'Close_Connection()
                End Try
            End SyncLock
        End Function
        Public Function ExecuteQuery(ByVal VarQuery As String, ByVal ParaColumn() As String, ByVal ParaValue() As String) As String
            SyncLock ExecuteQuery2Lock
                Try
                    Dim result As String = ""
                    If _con.State = ConnectionState.Closed Then open_connection()
                    _cmd = New SqlCommand()
                    _cmd.CommandText = VarQuery
                    Con_Transaction()
                    _cmd.Connection = _con
                    _cmd.Transaction = _tra
                    Dim I As Integer

                    'If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                    '    AddParamToSQLCmd()
                    'End If
                    For I = 0 To ParaColumn.Length - 1
                        _cmd.Parameters.AddWithValue(ParaColumn(I), ParaValue(I))
                    Next
                    result = Convert.ToString(_cmd.ExecuteNonQuery())
                    Con_Commit()
                    'Close_Connection()
                    Return result.ToString()
                Catch ex As Exception
                    Con_Rollback()
                    MsgBox("Data_Access_SQL :: ExecuteQuery(VarQuery,ParaColumn,ParaValue) ::" & ex.Message)
                    Return ""
                Finally
                    'Close_Connection()
                End Try
            End SyncLock
        End Function

        Public Function ExecuteMultipleRC(ByVal parmcount As Integer) As DataTable
            SyncLock ExecuteMultipleRCLock
                Try
                    Dim result As String = ""
                    Dim con As New SqlConnection
                    If con.State = ConnectionState.Closed Then open_connectionRC(con)
                    If _paramtable Is Nothing Then
                        Return _DtPrimaryExp
                        Exit Function
                    End If

                    If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                        Call Con_Transaction(con)
                        Dim j As Integer = 0
                        _cmd = New SqlCommand()
                        _cmd.CommandType = _cmd_type
                        _cmd.CommandText = _cmd_text
                        _cmd.Connection = con
                        _cmd.Transaction = _tra
                        For i As Integer = 0 To _paramtable.Rows.Count - 1 Step parmcount
                            j = 0
                            _cmd.Parameters.Clear()
                            For k As Integer = i To _paramtable.Rows.Count - 1
                                AddParamToSQLCmd(_paramtable.Rows(k))
                                j = j + 1
                                If j = parmcount Then
                                    Exit For
                                End If
                            Next
                            Try
                                If con.State = ConnectionState.Closed Then open_connectionRC(con)
                                result = Convert.ToString(_cmd.ExecuteNonQuery())
                            Catch ex1 As Exception
                                If ex1.Message.ToUpper.StartsWith("VIOLATION OF PRIMARY KEY CONSTRAINT") = True Then
                                    Dim DrDupli As DataRow = _DtPrimaryExp.NewRow
                                    For cnt As Integer = i To (i + parmcount - 1)
                                        If UCase(_paramtable.Rows(cnt).Item("ParameterName")) = "@ENTRYNO" Then
                                            DrDupli("entryno") = _paramtable.Rows(cnt).Item("Value")
                                        ElseIf UCase(_paramtable.Rows(cnt).Item("ParameterName")) = "@ORDERNO" Then
                                            DrDupli("orderno") = _paramtable.Rows(cnt).Item("Value")
                                        End If
                                    Next
                                    _DtPrimaryExp.Rows.Add(DrDupli)
                                Else
                                    Throw New ApplicationException(ex1.Message)
                                End If
                            End Try
                        Next
                        Call Con_Commit()
                    End If
                    'Close_Connection()
                    Return _DtPrimaryExp
                Catch ex As Exception
                    Call Con_Rollback()
                    MsgBox("Data_Access_SQL :: ExcuteMultiple ::  " & ex.ToString)
                Finally
                    'Close_Connection()
                End Try
            End SyncLock
        End Function

        Public Function ExecuteMultiple(ByVal parmcount As Integer) As DataTable
            SyncLock ExecuteMultipleLock
                Try
                    Dim result As String = ""
                    If _con.State = ConnectionState.Closed Then open_connection()
                    If _paramtable Is Nothing Then
                        Return _DtPrimaryExp
                        Exit Function
                    End If

                    If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                        Call Con_Transaction()
                        Dim j As Integer = 0
                        _cmd = New SqlCommand()
                        _cmd.CommandType = _cmd_type
                        _cmd.CommandText = _cmd_text
                        _cmd.Connection = _con
                        _cmd.Transaction = _tra
                        For i As Integer = 0 To _paramtable.Rows.Count - 1 Step parmcount
                            j = 0
                            _cmd.Parameters.Clear()
                            For k As Integer = i To _paramtable.Rows.Count - 1
                                AddParamToSQLCmd(_paramtable.Rows(k))
                                j = j + 1
                                If j = parmcount Then
                                    Exit For
                                End If
                            Next
                            Try
                                If _con.State = ConnectionState.Closed Then open_connection()
                                result = Convert.ToString(_cmd.ExecuteNonQuery())
                            Catch ex1 As Exception
                                If ex1.Message.ToUpper.StartsWith("VIOLATION OF PRIMARY KEY CONSTRAINT") = True Then
                                    Dim DrDupli As DataRow = _DtPrimaryExp.NewRow
                                    For cnt As Integer = i To (i + parmcount - 1)
                                        If UCase(_paramtable.Rows(cnt).Item("ParameterName")) = "@ENTRYNO" Then
                                            DrDupli("entryno") = _paramtable.Rows(cnt).Item("Value")
                                        ElseIf UCase(_paramtable.Rows(cnt).Item("ParameterName")) = "@ORDERNO" Then
                                            DrDupli("orderno") = _paramtable.Rows(cnt).Item("Value")
                                        End If
                                    Next
                                    _DtPrimaryExp.Rows.Add(DrDupli)
                                Else
                                    Throw New ApplicationException(ex1.Message)
                                End If
                            End Try
                        Next
                        Call Con_Commit()
                    End If
                    'Close_Connection()
                    Return _DtPrimaryExp
                Catch ex As Exception
                    Call Con_Rollback()
                    MsgBox("Data_Access_SQL :: ExcuteMultiple ::  " & ex.ToString)
                Finally
                    'Close_Connection()
                End Try
            End SyncLock
        End Function
        Public Function ExecuteScalar() As String
            SyncLock ExecuteScalarLock
                Try
                    Dim result As String = ""
                    If _con.State = ConnectionState.Closed Then open_connection()
                    Con_Transaction()
                    '_jro.RefreshCache(_con)
                    _cmd = New SqlCommand()
                    _cmd.CommandType = _cmd_type
                    _cmd.CommandText = _cmd_text
                    _cmd.Connection = _con
                    _cmd.Transaction = _tra

                    If (IsDBNull(_paramtable) = False And _paramtable.Rows.Count > 0) Then
                        AddParamToSQLCmd()
                    End If
                    result = Convert.ToString(_cmd.ExecuteScalar())
                    Con_Commit()
                    'Close_Connection()
                    Return result.ToString()
                Catch ex As Exception
                    Con_Rollback()
                    MsgBox("Data_Access_SQL :: ExecuteScalar() ::" & ex.Message)
                Finally
                    'Close_Connection()
                End Try
            End SyncLock
        End Function

        Private Sub AddParamToSQLCmd(ByVal drow As DataRow)
            If Not IsDBNull(_cmd) = False Then
                Throw (New ApplicationException("Command not Initialized."))
            End If
            Dim newSqlParam As SqlParameter = New SqlParameter
            newSqlParam = New SqlParameter()
            newSqlParam.ParameterName = drow("ParameterName").ToString()
            newSqlParam.SqlDbType = CType(drow("SqlDbType"), SqlDbType)
            'newSqlParam.Direction = ParameterDirection.Input
            'If (Convert.ToInt16(drow("Size")) > 0) Then
            '    newSqlParam.Size = Convert.ToInt16(drow("Size"))
            'End If
            If Not IsDBNull(drow("Value")) Then
                newSqlParam.Value = drow("Value")
            Else
                newSqlParam.Value = System.DBNull.Value
            End If
            _cmd.Parameters.Add(newSqlParam)
        End Sub
        Private Sub AddParamToSQLCmd()
            If Not IsDBNull(_cmd) = False Then
                Throw (New ApplicationException("Command not Initialized."))
            End If
            Dim newSqlParam As SqlParameter = New SqlParameter
            For Each drow As DataRow In _paramtable.Rows
                newSqlParam = New SqlParameter()
                newSqlParam.ParameterName = drow("ParameterName").ToString()
                newSqlParam.SqlDbType = CType(drow("SqlDbType"), SqlDbType)
                'newSqlParam.Direction = ParameterDirection.Input
                'If (Convert.ToInt16(drow("Size")) > 0) Then
                '    newSqlParam.Size = Convert.ToInt16(drow("Size"))
                'End If
                If Not IsDBNull(drow("Value")) Then
                    newSqlParam.Value = drow("Value")
                Else
                    newSqlParam.Value = System.DBNull.Value
                End If
                _cmd.Parameters.Add(newSqlParam)
            Next
        End Sub
        Public Function FillList() As DataTable
            Try
                Dim list As DataTable = New DataTable()
                _adp = New SqlDataAdapter
                If _con.State = ConnectionState.Closed Then open_connection()
                Con_Transaction()
                _cmd = New SqlCommand()
                _cmd.CommandType = _cmd_type
                _cmd.CommandText = _cmd_text
                _cmd.Connection = _con
                _cmd.CommandTimeout = 0
                _cmd.Transaction = _tra
                If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                    AddParamToSQLCmd()
                End If
                'clsGlobal.mPerf.SetFileName("SqlFillSQlCnt")
                'clsGlobal.mPerf.Write_Counter("DataFillSQL")
                'clsGlobal.mPerf.WriteLogStr("SQLSeverFill : " + _cmd_text + " : ConStr: " + _cmd.Connection.ConnectionString)

                _adp.SelectCommand = _cmd
                list.BeginLoadData()
                _adp.Fill(list)
                list.EndLoadData()
                _adp.Dispose()
                _cmd.Dispose()
                Con_Commit()
                'Close_Connection()
                Return list
            Catch ex As Exception
                Con_Rollback()
                MsgBox("Data_Access_SQL :: FillList :: " & ex.ToString)
                Return Nothing
            Finally
                'Close_Connection()
            End Try
        End Function
        Public Function FillDatatable(ByVal VarQuery As String) As DataTable
            Try
                Dim list As New DataTable
                _adp = New SqlDataAdapter
                If _con.State = ConnectionState.Closed Then open_connection()
                Call Con_Transaction()
                _cmd = New SqlCommand()
                _cmd.CommandText = VarQuery
                _cmd.Connection = _con
                _cmd.Transaction = _tra
                _adp.SelectCommand = _cmd
                list.BeginLoadData()
                _adp.Fill(list)
                list.EndLoadData()
                _adp.Dispose()
                _cmd.Dispose()
                Call Con_Commit()
                'Close_Connection()
                Return list
            Catch ex As Exception
                Call Con_Rollback()
                MsgBox("Data_Access_SQL :: FillDatatable(VarQuery) :: " & ex.ToString)
                Return Nothing
            Finally
                'Close_Connection()
            End Try
        End Function
        Public Function FillDatatable(ByVal VarQuery As String, ByVal ParaColumn() As String, ByVal ParaValue() As String) As DataTable
            Try
                Dim list As New DataTable
                _adp = New SqlDataAdapter
                If _con.State = ConnectionState.Closed Then open_connection()
                _cmd = New SqlCommand()
                _cmd.CommandText = VarQuery
                Con_Transaction()
                _cmd.Connection = _con
                _cmd.Transaction = _tra
                Dim I As Integer
                For I = 0 To ParaColumn.Length - 1
                    _cmd.Parameters.AddWithValue(ParaColumn(I), ParaValue(I))
                Next
                _adp.SelectCommand = _cmd
                list.BeginLoadData()
                _adp.Fill(list)
                list.EndLoadData()
                _adp.Dispose()
                _cmd.Dispose()
                Call Con_Commit()
                'Close_Connection()
                Return list
            Catch ex As Exception
                Con_Rollback()
                MsgBox("Data_Access_SQL :: FillDatatable(VarQuery,ParaColumn,ParaValue) :: " & ex.ToString)
                Return Nothing
            Finally
                'Close_Connection()
            End Try
        End Function
        Public Function ExecuteReturn(ByVal Str As String) As Object
            Try
                If _con.State = ConnectionState.Closed Then open_connection()
                _cmd = New SqlCommand()
                Con_Transaction()
                _cmd.CommandText = Str
                _cmd.Connection = _con
                _cmd.Transaction = _tra
                Str = IIf(IsDBNull(_cmd.ExecuteScalar) = True, "", _cmd.ExecuteScalar)
                Con_Commit()
                _cmd.Dispose()
                If Str Is Nothing Then Str = ""
                Return Str
            Catch ex As Exception
                Con_Rollback()
                Write_TimeLog1("Data_Access_SQL :: ExecuteReturn(VarQuery) ::Qyery=" & Str & " " & ex.Message)
                MsgBox("Data_Access_SQL :: ExecuteReturn(VarQuery) :: " & ex.ToString)

                Return ""
            Finally
                'Close_Connection()
            End Try
        End Function
        Public Function FillListDW() As DataTable
            Try
                Dim list As DataTable = New DataTable()
                _adpDW = New SqlDataAdapter
                If _conDW.State = ConnectionState.Closed Then open_connectionDW()
                ConDw_Transaction()
                _cmdDW = New SqlCommand()
                _cmdDW.CommandType = _cmd_type
                _cmdDW.CommandText = _cmd_textDW
                _cmdDW.Connection = _conDW
                _cmdDW.CommandTimeout = 0
                _cmdDW.Transaction = _traDw
                If (Not IsDBNull(_paramtableDW) And _paramtableDW.Rows.Count > 0) Then
                    AddParamToSQLCmdDw()
                End If
                _adpDW.SelectCommand = _cmdDW
                list.BeginLoadData()
                _adpDW.Fill(list)
                list.EndLoadData()
                _adpDW.Dispose()
                _cmdDW.Dispose()
                ConDw_Commit()
                'Close_Connection()
                Return list
            Catch ex As Exception
                Condw_Rollback()
                MsgBox("Data_Access_SQL :: FillList :: " & ex.ToString)
                Return Nothing
            Finally
                'Close_Connection()
            End Try
        End Function
        Public Function FillListCW() As DataTable
            Try
                Dim list As DataTable = New DataTable()
                _adpCW = New SqlDataAdapter
                If _conCW.State = ConnectionState.Closed Then open_connectionCW()
                ConCW_Transaction()
                _cmdCW = New SqlCommand()
                _cmdCW.CommandType = _cmd_type
                _cmdCW.CommandText = _cmd_textCW
                _cmdCW.Connection = _conCW
                _cmdCW.CommandTimeout = 0
                _cmdCW.Transaction = _traCW
                If (Not IsDBNull(_paramtableCW) And _paramtableCW.Rows.Count > 0) Then
                    AddParamToSQLCmdCw()
                End If
                _adpCW.SelectCommand = _cmdCW
                list.BeginLoadData()
                _adpCW.Fill(list)
                list.EndLoadData()
                _adpCW.Dispose()
                _cmdCW.Dispose()
                ConCW_Commit()
                'Close_Connection()
                Return list
            Catch ex As Exception
                ConCW_Rollback()
                MsgBox("Data_Access_SQL :: FillList :: " & ex.ToString)
                Return Nothing
            Finally
                'Close_Connection()
            End Try
        End Function
        Private Sub AddParamToSQLCmdDw()
            If Not IsDBNull(_cmdDW) = False Then
                Throw (New ApplicationException("Command not Initialized."))
            End If
            Dim newSqlParam As SqlParameter = New SqlParameter
            For Each drow As DataRow In _paramtableDW.Rows
                newSqlParam = New SqlParameter()
                newSqlParam.ParameterName = drow("ParameterName").ToString()
                newSqlParam.SqlDbType = CType(drow("SqlDbType"), SqlDbType)
                'newSqlParam.Direction = ParameterDirection.Input
                'If (Convert.ToInt16(drow("Size")) > 0) Then
                '    newSqlParam.Size = Convert.ToInt16(drow("Size"))
                'End If
                If Not IsDBNull(drow("Value")) Then
                    newSqlParam.Value = drow("Value")
                Else
                    newSqlParam.Value = System.DBNull.Value
                End If
                _cmdDW.Parameters.Add(newSqlParam)
            Next
        End Sub
        Private Sub AddParamToSQLCmdCw()
            If Not IsDBNull(_cmdCW) = False Then
                Throw (New ApplicationException("Command not Initialized."))
            End If
            Dim newSqlParam As SqlParameter = New SqlParameter
            For Each drow As DataRow In _paramtableCW.Rows
                newSqlParam = New SqlParameter()
                newSqlParam.ParameterName = drow("ParameterName").ToString()
                newSqlParam.SqlDbType = CType(drow("SqlDbType"), SqlDbType)
                'newSqlParam.Direction = ParameterDirection.Input
                'If (Convert.ToInt16(drow("Size")) > 0) Then
                '    newSqlParam.Size = Convert.ToInt16(drow("Size"))
                'End If
                If Not IsDBNull(drow("Value")) Then
                    newSqlParam.Value = drow("Value")
                Else
                    newSqlParam.Value = System.DBNull.Value
                End If
                _cmdCW.Parameters.Add(newSqlParam)
            Next
        End Sub

#End Region

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class

End Namespace

