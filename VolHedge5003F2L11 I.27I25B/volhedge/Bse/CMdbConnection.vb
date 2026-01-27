Imports System.Data
Imports System.IO
Imports Sylvan.Data.Csv
Imports System.Globalization
Imports System.Threading.Tasks
Imports VolHedge.DAL
Imports System.Collections
Imports System.Collections.Concurrent
Imports System.Configuration
Imports System.Data.OleDb

Public Class CMdbConnection

#Region "variable"
    Private _con As OleDbConnection
    Private _adp As OleDbDataAdapter
    Private _cmd As OleDbCommand
    Private _cmd_type As CommandType
    Private _cmd_text As String
    Public mConStr As String
    Private _paramtable As DataTable
    Private _DtPrimaryExp As DataTable
    Private _tra As OleDbTransaction
#End Region

#Region "Method"

    Public Sub New(pStrConn As String)
        _DtPrimaryExp = New DataTable()
        _cmd_type = CommandType.StoredProcedure
        mConStr = pStrConn
    End Sub

    Public Property mCmd_Text() As String
        Get
            Return _cmd_text
        End Get
        Set(ByVal value As String)
            _cmd_text = value
        End Set
    End Property

    Public Property mCmd_type() As CommandType
        Get
            Return _cmd_type
        End Get
        Set(ByVal value As CommandType)
            _cmd_type = value
        End Set
    End Property

    Public Function ReadGreekConnStr() As String

        Try
            Dim str As String = ConfigurationSettings.AppSettings("Importdb")
            '_connection_string = ConfigurationSettings.AppSettings("constr") & "" & System.Windows.Forms.Application.StartupPath() & "" & str
            'mGreekConn = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=True;Data Source=greek.mdb;Jet OLEDB:Database Password=Admin"
            mConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=True;Data Source=greek.mdb;Jet OLEDB:Database Password=" & clsGlobal.glbAcessPassWord & ""
            Return mConStr

            If mConStr = "" Then
                Throw New ApplicationException("Connection String is not initialize")
            End If
            Return mConStr
        Catch ex As Exception
            'MsgBox(ex.ToString)
            'FSTimerLogFile.WriteLine("Data_access::Connection_string:-" & ex.ToString)
            Throw New ApplicationException("Connection String is not initialize")
        End Try

    End Function

    Public Function ReadImportMdbConnStr() As String
        Dim res As String = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=True;Data Source=ImportData.mdb;Jet OLEDB:Database Password=" & clsGlobal.glbAcessPassWord & ""
        Return res
    End Function

    Private Sub open_connection()
        Try
            If mConStr <> "" Or (_con Is Nothing) Then
                _con = New OleDbConnection(mConStr)
                _con.Open()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            'FSTimerLogFile.WriteLine("Data_access::open_connection:-" & ex.ToString)
            Throw New ApplicationException("Connection Error")
        End Try
    End Sub

    Private Sub Con_Transaction()
        If _con.State = ConnectionState.Open Then
            _tra = _con.BeginTransaction()
        End If
    End Sub

    Private Sub Con_Commit()
        _tra.Commit()
    End Sub

    Private Sub Con_Rollback()
        _tra.Rollback()
    End Sub

    Private Sub Close_Connection()
        If _con.State = ConnectionState.Open Then
            _con.Close()
        End If
    End Sub

    Public Sub ParamClear()
        _paramtable = New DataTable()
        _paramtable.Columns.Add("ParameterName", GetType(String))
        _paramtable.Columns.Add("oleDbType", GetType(OleDbType))
        _paramtable.Columns.Add("Size", GetType(Integer))
        _paramtable.Columns.Add("Value", GetType(Object))
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

    Public Function ExecuteNonQuery(ByVal cmd_type As CommandType) As DataTable
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

    Public Function ExecuteQuery(ByVal cmd_Text As String, ByVal cmd_type As CommandType)
        'Try
        '    Dim result As String = ""
        '    open_connection()
        '    _cmd = New OleDbCommand()
        '    _cmd.CommandType = cmd_type
        '    _cmd.CommandText = cmd_Text
        '    Con_Transaction()
        '    _cmd.Connection = _con
        '    _cmd.Transaction = _tra
        '    Con_Commit()
        '    Close_Connection()
        '    Return _DtPrimaryExp 'result.ToString()
        'Catch ex As Exception
        '    'FSTimerLogFile.WriteLine("Data_access::ExecuteNonQuery:-" & ex.ToString)
        '    MsgBox(ex.ToString)
        '    Con_Rollback()
        'Finally
        '    Close_Connection()
        'End Try

        Try
            Dim dt As DataTable = New DataTable()
            dt.Rows.Clear()
            _adp = New OleDbDataAdapter
            open_connection()
            Con_Transaction()
            _cmd = New OleDbCommand()
            _cmd.CommandType = cmd_type
            _cmd.CommandText = cmd_Text
            _cmd.Connection = _con
            _cmd.Transaction = _tra
            _adp.SelectCommand = _cmd
            _adp.Fill(dt)
            _adp.Dispose()
            Con_Commit()
            Close_Connection()
            Return dt
        Catch ex As Exception
            Close_Connection()
            Con_Rollback()
            Return Nothing
        End Try
    End Function

    Public Function ExecuteMultiple(ByVal parmcount As Integer) As DataTable
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

    Public Function ExecuteScalar() As String
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

    Public Function FillList() As DataTable
        Try
            Dim list As DataTable = New DataTable()
            list.Rows.Clear()
            _adp = New OleDbDataAdapter
            open_connection()
            Con_Transaction()
            _cmd = New OleDbCommand()
            _cmd.CommandType = _cmd_type
            _cmd.CommandText = _cmd_text
            _cmd.Connection = _con
            _cmd.Transaction = _tra
            'If (Not IsDBNull(_paramtable) And _paramtable.Rows.Count > 0) Then
            '    AddParamToSQLCmd()
            'End If
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

    Public Function ExecuteReturn(ByVal Str As String) As Object
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

    Public Function GetValueStr(pSql As String) As String
        Return GetValueDb(pSql)
    End Function

    Public Function GetValueInt(pSql As String) As String
        Dim res As String = GetValueDb(pSql)
        Return CUtils.StringToInt(res)
    End Function

    Private Function GetValueDb(pSql As String) As String
        Try
            open_connection()

            Using cmd As New OleDbCommand(pSql, _con)
                Using rdr As OleDbDataReader = cmd.ExecuteReader()
                    If rdr.Read() Then
                        If Not IsDBNull(rdr(0)) Then
                            Return rdr(0).ToString()
                        End If
                    End If
                End Using
            End Using

            Return ""

        Catch ex As Exception
            Return ""
        Finally
            Close_Connection()
        End Try
    End Function

#End Region

End Class
