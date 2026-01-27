Imports System.Data.SqlClient
Namespace DAL
    Public Class DA_SQL
        Shared GVarSQLServerConnStr As String
        Private Shared ExecutenonQueryLock As New Object
        Private Shared ExecuteQueryLock As New Object
        Private Shared ExecuteQuery2Lock As New Object
        Private Shared ExecuteLock As New Object
        Private Shared ExecuteMultipleLock As New Object
#Region "variable"
        Shared _con As New SqlConnection
        Public Shared _conTrend As New SqlConnection
        Public Shared _conFo As New SqlConnection
        Public Shared _conspan As New SqlConnection
        Public Shared _con_idx As New SqlConnection
        Public Shared _conEq As New SqlConnection
        Public Shared _conCur As New SqlConnection
        Shared _adp As SqlDataAdapter
        Shared _cmd As SqlCommand
        Shared _cmdFo As SqlCommand
        Shared _cmdEq As SqlCommand
        Shared _cmdCur As SqlCommand

        Shared _cmd_type As CommandType = CommandType.StoredProcedure
        Shared _cmd_text As String
        Shared _connection_string As String
        Shared _paramtable As New DataTable
        Shared _DtPrimaryExp As New DataTable
        'Shared _tra As SqlTransaction  Viral05Apr2019
        Public Shared IsConClose As Boolean = False
#End Region

#Region "Method"
        Public Shared Property Cmd_Text() As String
            Get
                Return _cmd_text
            End Get
            Set(ByVal value As String)
                _cmd_text = value
            End Set
        End Property
        Private Shared ReadOnly Property Connection_string() As String
            Get
                If GVarSQLServerConnStr = "" Then
                    Try
                        Dim StrConn As String = ""
                        'Dim FR As New IO.StreamReader(Application.StartupPath & "\SQL Server Connection.txt")
                        'Dim Str As String = ""
                        'Str = FR.ReadLine()
                        StrConn &= "Data Source=" & sSQLSERVER 'Str.Substring("Server Name ::".Length)
                        'Str = FR.ReadLine()
                        StrConn &= ";Initial Catalog=" & sDATABASE 'Str.Substring("Database Name ::".Length)
                        'Str = FR.ReadLine()
                        If sAUTHANTICATION = "WINDOWS" Then 'If Str.Substring("Authentication ::".Length) = "Windows" Then
                            StrConn &= ";Integrated Security=True"
                            'StrConn &= ";Connect Timeout=500"
                        Else
                            'Str = FR.ReadLine()
                            StrConn &= ";User ID=" & sUSERNAME 'Str.Substring("User Name ::".Length)
                            'Str = FR.ReadLine()
                            StrConn &= ";Password=" & sPASSWORD 'Str.Substring("Password ::".Length)
                            'StrConn &= ";Connect Timeout=500"
                        End If
                        'FR.Close()
                        StrConn &= ";Application Name=" & "VH_" & sSQLSERVER & ""
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

        Public Shared Sub open_connection()
            Try
lbl:
                If _con.ConnectionString = "" Then
                    _con = New SqlConnection(Connection_string)
                    If _con.State = ConnectionState.Closed Then _con.Open()
                Else
                    '_con = New OleDbConnection(Connection_string)
                    If _con.State = ConnectionState.Closed Then _con.Open()
                End If
                bool_IsTelNet = True
            Catch ex As Exception
                'If ex.ToString.ToUpper.Contains("UNSPECIFIED ERROR") = True Then
                '    GoTo lbl
                'End If
                '  Throw New ApplicationException("SQL Server Connection Problem")
                ' MsgBox("SQL Server Connection Problem")
                bool_IsTelNet = False
            End Try

        End Sub

        Public Shared Sub open_Trend_connection()
            Try
lbl:
                If _conTrend.ConnectionString = "" Then
                    _conTrend = New SqlConnection(Connection_string)
                    If _conTrend.State = ConnectionState.Closed Then _conTrend.Open()
                Else
                    '_con = New OleDbConnection(Connection_string)
                    If _conTrend.State = ConnectionState.Closed Then _conTrend.Open()
                End If
            Catch ex As Exception
                'If ex.ToString.ToUpper.Contains("UNSPECIFIED ERROR") = True Then
                '    GoTo lbl
                'End If
                bool_IsTelNet = False
                ' Throw New ApplicationException("SQL Server Connection Problem")'change by payalpatel
            End Try
        End Sub


        Public Shared Sub open_Idx_connection()
            Try
lbl:
                If _con_idx.ConnectionString = "" Then
                    _con_idx = New SqlConnection(Connection_string)
                    If _con_idx.State = ConnectionState.Closed Then _con_idx.Open()
                Else
                    '_con = New OleDbConnection(Connection_string)
                    If _con_idx.State = ConnectionState.Closed Then _con_idx.Open()
                End If
            Catch ex As Exception
                'If ex.ToString.ToUpper.Contains("UNSPECIFIED ERROR") = True Then
                '    GoTo lbl
                'End If
                bool_IsTelNet = False
                ' Throw New ApplicationException("SQL Server Connection Problem")'change by payalpatel
            End Try
        End Sub

        Public Shared Sub open_Fo_connection()
            Try
lbl:

                If _conFo.ConnectionString = "" Then
                    _conFo = New SqlConnection(Connection_string)
                    If _conFo.State = ConnectionState.Closed Then _conFo.Open()
                Else
                    '_con = New OleDbConnection(Connection_string)
                    If _conFo.State = ConnectionState.Closed Then _conFo.Open()
                End If
            Catch ex As Exception
                'If ex.ToString.ToUpper.Contains("UNSPECIFIED ERROR") = True Then
                '    GoTo lbl
                'End If
                bool_IsTelNet = False
                ' Throw New ApplicationException("SQL Server Connection Problem")'change by payalpatel
            End Try
        End Sub
        Public Shared Sub open_Eq_connection()
            Try
lbl:
                If _conEq.ConnectionString = "" Then
                    _conEq = New SqlConnection(Connection_string)
                    If _conEq.State = ConnectionState.Closed Then _conEq.Open()
                Else
                    '_con = New OleDbConnection(Connection_string)
                    If _conEq.State = ConnectionState.Closed Then _conEq.Open()
                End If
            Catch ex As Exception
                'If ex.ToString.ToUpper.Contains("UNSPECIFIED ERROR") = True Then
                '    GoTo lbl
                'End If
                bool_IsTelNet = False
                ' Throw New ApplicationException("SQL Server Connection Problem")'change by payalpatel
            End Try
        End Sub
        Public Shared Sub open_cur_connection()
            Try
lbl:
                If _conCur.ConnectionString = "" Then
                    _conCur = New SqlConnection(Connection_string)
                    If _conCur.State = ConnectionState.Closed Then _conCur.Open()
                Else
                    '_con = New OleDbConnection(Connection_string)
                    If _conCur.State = ConnectionState.Closed Then _conCur.Open()
                End If
            Catch ex As Exception
                'If ex.ToString.ToUpper.Contains("UNSPECIFIED ERROR") = True Then
                '    GoTo lbl
                'End If
                bool_IsTelNet = False
                ' Throw New ApplicationException("SQL Server Connection Problem")'change by payalpatel
            End Try
        End Sub
        Private Shared Sub Con_Transaction()
            ' 05-Apr-19 Viral 
            'If _con.State = ConnectionState.Open Then
            '    _tra = _con.BeginTransaction(IsolationLevel.Unspecified)
            'End If
        End Sub
        Private Shared Sub Con_Commit()
            ' 05-Apr-19 Viral 
            '_tra.Commit()
        End Sub
        Private Shared Sub Con_Rollback()
            ' 05-Apr-19 Viral 
            'Try
            '    _tra.Rollback()
            'Catch ex As Exception
            'End Try
        End Sub
        Public Shared Sub Close_Connection()
            If _con.State = ConnectionState.Open Then
                _con.Close()
                _con.ConnectionString = ""
                GVarSQLServerConnStr = ""
            End If
            _con.ConnectionString = ""
            GVarSQLServerConnStr = ""
        End Sub

        Public Shared Sub Close_Trend_Connection()
            If _conTrend.State = ConnectionState.Open Then
                _conTrend.Close()
            End If
            _conTrend.ConnectionString = ""
            GVarSQLServerConnStr = ""
        End Sub

        Public Shared Sub Close_Fo_Connection()
            If _conFo.State = ConnectionState.Open Then
                _conFo.Close()
            End If
            _conFo.ConnectionString = ""
            GVarSQLServerConnStr = ""
        End Sub


        Public Shared Sub Close_Eq_Connection()
            If _conEq.State = ConnectionState.Open Then
                _conEq.Close()
            End If
            _conEq.ConnectionString = ""
            GVarSQLServerConnStr = ""

        End Sub
        Public Shared Sub Close_Cur_Connection()
            If _conCur.State = ConnectionState.Open Then
                _conCur.Close()
            End If
            _conCur.ConnectionString = ""
            GVarSQLServerConnStr = ""
        End Sub

        Public Shared Sub ParamClear()
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
        Public Shared Sub AddParam(ByVal param_id As String, ByVal sqldbtype As SqlDbType, ByVal size As Integer, ByVal value As Object)
            Dim drow As DataRow = _paramtable.NewRow()
            drow("ParameterName") = param_id
            drow("SqlDbType") = sqldbtype
            drow("Size") = size
            If Not value Is Nothing Then
                drow("Value") = value
            End If
            _paramtable.Rows.Add(drow)
        End Sub
        Public Shared Function IsValidConnection() As Boolean
            Try
                If _con.State = ConnectionState.Closed Then open_connection()
                'IsValidConnection = True
                Return True
            Catch ex As Exception
                'IsValidConnection = False
                Return False
            End Try

            'Return True 'CheckTelNet()
        End Function
        Public Shared Function ExecuteNonQuery() As String
            SyncLock ExecutenonQueryLock
                Try
                    Dim result As String = ""
                    If _con.State = ConnectionState.Closed Then open_connection()
                    _cmd = New SqlCommand()
                    _cmd.CommandType = _cmd_type
                    _cmd.CommandText = _cmd_text
                    Con_Transaction()
                    _cmd.Connection = _con
                    _cmd.CommandTimeout = 0
                    ' 05-Apr-19 Viral _cmd.Transaction = _tra
                    If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                        AddParamToSQLCmd(_cmd)
                    End If
                    result = Convert.ToString(_cmd.ExecuteNonQuery())
                    Con_Commit()
                    'Close_Connection()
                    If IsConClose = True Then
                        Close_Connection()
                    End If
                    Return result.ToString()
                Catch ex As Exception
                    Con_Rollback()
                    'MsgBox("DA_SQL :: ExecuteNonQuery() ::" & "commandtext=" & _cmd_text & ex.Message)
                    'Write_Log2("Error In:Data_Access_SQL :: ExecuteNonQuery() ::")
                    Return ""
                    bool_IsTelNet = False
                Finally
                    If IsConClose = True Then
                        Close_Connection()
                    End If
                    'Close_Connection()
                End Try
            End SyncLock
        End Function
        Public Shared Function ExecuteQuery(ByVal VarQuery As String) As String
            SyncLock ExecuteQueryLock
                Try
                    Dim result As String = ""
                    If _con.State = ConnectionState.Closed Then open_connection()
                    _cmd = New SqlCommand()
                    _cmd.CommandText = VarQuery
                    Con_Transaction()
                    _cmd.Connection = _con
                    ' 05-Apr-19 Viral _cmd.Transaction = _tra
                    If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                        AddParamToSQLCmd(_cmd)
                    End If
                    result = Convert.ToString(_cmd.ExecuteNonQuery())
                    Con_Commit()
                    'Close_Connection()
                    Return result.ToString()
                Catch ex As Exception
                    Con_Rollback()
                    'MsgBox("DA_SQL :: ExecuteQuery(VarQuery) ::" & ex.Message)
                    'Write_Log2("Data_Access_SQL :: ExecuteQuery(VarQuery) ::")
                    Return ""
                Finally
                    'Close_Connection()
                End Try
            End SyncLock
        End Function
        Public Shared Function ExecuteQuery(ByVal VarQuery As String, ByVal ParaColumn() As String, ByVal ParaValue() As String) As String
            SyncLock ExecuteQuery2Lock
                Try
                    Dim result As String = ""
                    If _con.State = ConnectionState.Closed Then open_connection()
                    _cmd = New SqlCommand()
                    _cmd.CommandText = VarQuery
                    Con_Transaction()
                    _cmd.Connection = _con
                    ' 05-Apr-19 Viral _cmd.Transaction = _tra
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
                    MsgBox("DA_SQL :: ExecuteQuery(VarQuery,ParaColumn,ParaValue) ::" & ex.Message)
                    Return ""
                Finally
                    'Close_Connection()
                End Try
            End SyncLock
        End Function
        Public Shared Sub Execute(ByVal VarQuery As String)
            SyncLock ExecuteLock
                Try
                    Dim count As Integer = 0
recon:
                    If _con.State = ConnectionState.Closed Then open_connection()
                    If count <= 2 Then


                        If bool_IsTelNet = False Then
                            count = count + 1
                            _con.Close()
                            GoTo recon
                        End If
                    Else
                        Exit Sub
                    End If
                    Dim cmd As New SqlCommand()
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = VarQuery
                    Con_Transaction()
                    cmd.Connection = _con
                    ' 05-Apr-19 Viral cmd.Transaction = _tra
                    cmd.ExecuteNonQuery()
                    Con_Commit()
                    'Close_Connection()
                    If IsConClose = True Then
                        Close_Connection()
                    End If
                Catch ex As Exception
                    Con_Rollback()
                    Write_TimeLog1("DA_SQL :: Execute(VarQuery) ::Qyery=" & VarQuery & " " & ex.Message)
                    ' MsgBox("DA_SQL :: Execute(VarQuery) ::" & ex.Message)

                Finally
                    'Close_Connection()
                    If IsConClose = True Then
                        Close_Connection()
                    End If
                End Try
            End SyncLock
        End Sub
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
                        ' 05-Apr-19 Viral _cmd.Transaction = _tra
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
        Public Shared Function ExecuteScalar(ByVal Qry As String) As String
            Try
                Dim result As String = ""
                If _con.State = ConnectionState.Closed Then open_connection()
                Con_Transaction()
                '_jro.RefreshCache(_con)
                _cmd = New SqlCommand()
                _cmd.CommandType = CommandType.Text
                _cmd.CommandText = Qry
                _cmd.Connection = _con
                ' 05-Apr-19 Viral _cmd.Transaction = _tra

                If (IsDBNull(_paramtable) = False And _paramtable.Rows.Count > 0) Then
                    AddParamToSQLCmd(_cmd)
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
        End Function
        Public Shared Function ExecuteScalar_openposition(ByVal Qry As String) As String
            Try
                Dim result As String = ""
                
                If _con.State = ConnectionState.Closed Then open_connection()
                Con_Transaction()
                '_jro.RefreshCache(_con)
                _cmd = New SqlCommand()
                _cmd.CommandType = CommandType.Text
                _cmd.CommandText = Qry
                _cmd.Connection = _con
                ' 05-Apr-19 Viral _cmd.Transaction = _tra

                If (IsDBNull(_paramtable) = False And _paramtable.Rows.Count > 0) Then
                    AddParamToSQLCmd(_cmd)
                End If
                result = Convert.ToString(_cmd.ExecuteScalar())
                Con_Commit()
                'Close_Connection()
                Return result.ToString()
            Catch ex As Exception
                Con_Rollback()
                'MsgBox("Data_Access_SQL :: ExecuteScalar() ::" & ex.Message)
            Finally
                'Close_Connection()
            End Try
        End Function

        Public Function ExecuteScalar() As String
            Try
                Dim result As String = ""
                If _con.State = ConnectionState.Closed Then open_connection()
                Con_Transaction()
                '_jro.RefreshCache(_con)
                _cmd = New SqlCommand()
                _cmd.CommandType = _cmd_type
                _cmd.CommandText = _cmd_text
                _cmd.Connection = _con
                ' 05-Apr-19 Viral _cmd.Transaction = _tra

                If (IsDBNull(_paramtable) = False And _paramtable.Rows.Count > 0) Then
                    AddParamToSQLCmd(_cmd)
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
        Private Shared Sub AddParamToSQLCmd(ByVal cmd As SqlCommand)
            If Not IsDBNull(cmd) = False Then
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
        Public Shared Function FillList() As DataTable
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
                ' 05-Apr-19 Viral _cmd.Transaction = _tra
                If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
                    AddParamToSQLCmd(_cmd)
                End If
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
        'Public Shared Function FillListFo(ByVal scmdtxt As String) As SqlDataReader
        '    Try
        '        'Dim sdr As SqlDataReader
        '        If _conFo.State = ConnectionState.Closed Then open_Fo_connection()
        '        _cmdFo = New SqlCommand(scmdtxt, _conFo)
        '        Return _cmdFo.ExecuteReader()
        '    Catch ex As Exception
        '        MsgBox("Data_Access_SQL :: FillList Fo :: " & ex.ToString)
        '        Return Nothing
        '    Finally
        '        'Close_Connection()
        '    End Try
        'End Function
        'Public Shared Function FillListEq(ByVal scmdtxt As String) As SqlDataReader
        '    Try
        '        If _conEq.State = ConnectionState.Closed Then open_Eq_connection()
        '        _cmdEq = New SqlCommand(scmdtxt, _conEq)
        '        Return _cmdEq.ExecuteReader
        '    Catch ex As Exception
        '        MsgBox("Data_Access_SQL :: FillList Eq:: " & ex.ToString)
        '        Return Nothing
        '    Finally
        '        'Close_Connection()
        '    End Try
        'End Function
        'Public Shared Function FillListCur(ByVal scmdtxt As String) As SqlDataReader
        '    Try
        '        If _conCur.State = ConnectionState.Closed Then open_cur_connection()
        '        _cmdCur = New SqlCommand(scmdtxt, _conCur)
        '        Return _cmdCur.ExecuteReader
        '    Catch ex As Exception
        '        MsgBox("Data_Access_SQL :: FillList Cur :: " & ex.ToString)
        '        Return Nothing
        '    Finally
        '        'Close_Connection()
        '    End Try
        'End Function
        Public Shared Function FillDatatable(ByVal VarQuery As String) As DataTable
            Try
                Dim list As New DataTable
                _adp = New SqlDataAdapter
                If _con.State = ConnectionState.Closed Then open_connection()
                Call Con_Transaction()
                _cmd = New SqlCommand()
                _cmd.CommandText = VarQuery
                _cmd.Connection = _con
                ' 05-Apr-19 Viral _cmd.Transaction = _tra
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
                'MsgBox("Data_Access_SQL :: FillDatatable(VarQuery) :: " & ex.ToString)
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
                ' 05-Apr-19 Viral _cmd.Transaction = _tra
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
                ' 05-Apr-19 Viral _cmd.Transaction = _tra
                Str = IIf(IsDBNull(_cmd.ExecuteScalar) = True, "", _cmd.ExecuteScalar)
                Con_Commit()
                _cmd.Dispose()
                If Str Is Nothing Then Str = ""
                Return Str
            Catch ex As Exception
                Con_Rollback()
                MsgBox("DA_SQL :: ExecuteReturn(VarQuery) :: " & ex.ToString)
                Return ""
            Finally
                'Close_Connection()
            End Try
        End Function
#End Region

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace

