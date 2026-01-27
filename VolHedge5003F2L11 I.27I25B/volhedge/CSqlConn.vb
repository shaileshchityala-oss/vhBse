Imports System.Data.SqlClient
Public Class CSqlConnection
    Public _con As SqlConnection
    Public _adp As SqlDataAdapter
    Public _cmd As SqlCommand
    Public _cmd_type As CommandType = CommandType.StoredProcedure
    Public _cmd_text As String
    Public _connection_string As String
    Public _DtParamsPrimary As DataTable
    Private mThreadLock As Int16
    'Public Shared mStr As String = "Data Source=139.5.190.161,1401;Initial Catalog=finideas;MultipleActiveResultSets = True;User ID=finideas;Password=VCf#v!FiNE+qBID!Z;Application Name=VH_REG_139.5.190.161,1401;Asynchronous Processing=True;Connection Timeout=600"
    Public Shared mStr As String = "Data Source=139.5.190.161,1401;Initial Catalog=SQLDB01B;MultipleActiveResultSets = True;User ID=finideas;Password=VCf#v!FiNE+qBID!Z;Application Name=VH_REG_139.5.190.161,1401;Asynchronous Processing=True;Connection Timeout=600"
    Public Shared mStr1 As String = "Data Source=139.5.190.161,1401;Initial Catalog=SQLDB01B;MultipleActiveResultSets = True;User ID=finideas;Password=VCf#v!FiNE+qBID!Z;Application Name=VH_REG_139.5.190.161,1401;Asynchronous Processing=True;Connection Timeout=600"
    Public Shared mStrTest As String = "Data Source=139.5.190.161,1401;Initial Catalog=test;MultipleActiveResultSets = True;User ID=payal;Password=Admin@789456;Application Name=VH_REG_139.5.190.161,1401;Asynchronous Processing=True;Connection Timeout=600"

    Public Shared Sub CreateConStr(pServer As String, pUser As String, pPwd As String)
        Dim str As String = "Data Source=" & pServer &
            ";Initial Catalog=SQLDB01B;MultipleActiveResultSets = True;User ID=" & pUser &
            ";Password=" & pPwd &
            ";Application Name=finideas;Asynchronous Processing=True;Connection Timeout=600"

        mStr = str

    End Sub


    Public Sub New(pStrConn As String)
        _DtParamsPrimary = New DataTable()
        AddCol(_DtParamsPrimary)
        _connection_string = pStrConn
        mThreadLock = 0
        _con = New SqlConnection(_connection_string)
    End Sub

    Private Sub AddCol(pDt As DataTable)
        If _DtParamsPrimary.Columns.Count = 0 Then
            _DtParamsPrimary.Columns.Add("ParameterName", GetType(String))
            _DtParamsPrimary.Columns.Add("SqlDbType", GetType(SqlDbType))
            _DtParamsPrimary.Columns.Add("Size", GetType(Integer))
            _DtParamsPrimary.Columns.Add("Value", GetType(Object))
        End If
    End Sub
    Public Sub ParamClear()
        _DtParamsPrimary.Rows.Clear()
    End Sub

    Public Sub AddParam(ByVal param_id As String, ByVal sqldbtype As SqlDbType, ByVal size As Integer, ByVal value As Object)
        Dim drow As DataRow = _DtParamsPrimary.NewRow()
        drow("ParameterName") = param_id
        drow("SqlDbType") = sqldbtype
        drow("Size") = size
        If Not value Is Nothing Then
            drow("Value") = value
        End If
        _DtParamsPrimary.Rows.Add(drow)
    End Sub
    Public Sub AddParam(ByRef pDt As DataTable, ByVal param_id As String, ByVal sqldbtype As SqlDbType, ByVal size As Integer, ByVal value As Object)
        Dim drow As DataRow = pDt.NewRow()
        drow("ParameterName") = param_id
        drow("SqlDbType") = sqldbtype
        drow("Size") = size
        If Not value Is Nothing Then
            drow("Value") = value
        End If
        _DtParamsPrimary.Rows.Add(drow)
    End Sub

#Region "Method"

    Public Property Cmd_Text() As String
        Get
            Return _cmd_text
        End Get
        Set(ByVal value As String)
            _cmd_text = value
        End Set
    End Property

    Public Property Con_Str() As String
        Get
            Return _connection_string
        End Get
        Set(ByVal value As String)
            _connection_string = value
        End Set
    End Property

    Public Sub Con_Open()
        If _con.State = ConnectionState.Open Then
            _con.Close()
        End If
        _con.Open()
    End Sub

    Dim _tra As SqlTransaction
    Private Sub Con_Transaction()
        If _con.State = ConnectionState.Open Then
            _tra = _con.BeginTransaction(IsolationLevel.Serializable)
        End If
    End Sub
    Private Sub Con_Commit()
        _tra.Commit()
    End Sub
    Private Sub Con_Rollback()
        _tra.Rollback()
    End Sub
    Public Sub Close_Connection()
        If _con.State = ConnectionState.Open Then
            _con.Close()
        End If
    End Sub
    Public Function IsValidConnection() As Boolean
        Con_Open()
        If _con.State = ConnectionState.Open Then
            _con.Close()
            Return True
        Else
            Return False
        End If

    End Function


    Public Sub ExecuteQuery(ByVal VarQuery As String)
        ' Validate input parameters
        If String.IsNullOrEmpty(VarQuery) Then
            Return
        End If

        ' Validate connection string
        If String.IsNullOrEmpty(_connection_string) Then
            Return
        End If

        Dim transactionStarted As Boolean = False
        Try
            ' Ensure connection is open before starting transaction
            If _con.State = ConnectionState.Closed Then Con_Open()

            ' Start transaction
            Con_Transaction()
            transactionStarted = True

            ' Use Using statement for proper resource disposal
            Using cmd As New SqlCommand(VarQuery, _con, _tra)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
            End Using

            Con_Commit()
            transactionStarted = False
        Catch ex As Exception
            ' Only rollback if transaction was actually started
            If transactionStarted AndAlso _tra IsNot Nothing Then
                Try
                    Con_Rollback()
                Catch rollbackEx As Exception
                    ' Log rollback error but don't mask original exception
                End Try
            End If
            Throw ' Re-throw exception to allow caller to handle it
        Finally
            Close_Connection()
        End Try
    End Sub


    Public Sub ExecuteQuery1(ByVal VarQuery As String)
        ' Validate input parameters
        If String.IsNullOrEmpty(VarQuery) Then
            Return
        End If

        ' Validate connection string
        If String.IsNullOrEmpty(_connection_string) Then
            Return
        End If

        Try
            ' Ensure connection is open before starting transaction
            If _con.State = ConnectionState.Closed Then Con_Open()
            Con_Transaction()

            ' Use Using statement for proper resource disposal
            Using cmd As New SqlCommand(VarQuery, _con, _tra)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
            End Using

            Con_Commit()
        Catch ex As Exception
            Con_Rollback()
            Throw ' Re-throw exception to allow caller to handle it
        Finally
            Close_Connection()
        End Try
    End Sub

    Public Function ExecuteScalar(ByVal Qry As String) As String
        ' Validate input parameters
        If String.IsNullOrEmpty(Qry) Then
            Return ""
        End If

        ' Validate connection string
        If String.IsNullOrEmpty(_connection_string) Then
            Return ""
        End If

        Try
            ' Ensure connection is open
            If _con.State = ConnectionState.Closed Then
                _con.Open()
            End If

            Dim result As Object
            ' Use Using statement for proper resource disposal
            Using cmd As New SqlCommand(Qry, _con)
                cmd.CommandType = CommandType.Text
                result = cmd.ExecuteScalar()
            End Using

            ' Handle null result properly
            If result Is Nothing OrElse result Is DBNull.Value Then
                Return ""
            Else
                Return Convert.ToString(result)
            End If

        Catch ex As Exception
            ' Return empty string on error
            Return ""
        Finally
            Close_Connection()
        End Try
    End Function


    Private Sub AddParamToSQLCmd(pCmd As SqlCommand)
        If Not IsDBNull(pCmd) = False Then
            Throw (New ApplicationException("Command not Initialized."))
        End If
        Dim newSqlParam As SqlParameter = New SqlParameter
        For Each drow As DataRow In _DtParamsPrimary.Rows
            newSqlParam = New SqlParameter()
            newSqlParam.ParameterName = drow("ParameterName").ToString()
            newSqlParam.SqlDbType = CType(drow("SqlDbType"), SqlDbType)
            If Not IsDBNull(drow("Value")) Then
                newSqlParam.Value = drow("Value")
            Else
                newSqlParam.Value = System.DBNull.Value
            End If
            pCmd.Parameters.Add(newSqlParam)
        Next
    End Sub

    Public Function FillDatatable(ByVal pQuery As String) As DataTable
        Return FillDatatable(pQuery, _DtParamsPrimary)
    End Function

    Public Function FillDatatable(ByVal pQuery As String, pDtParam As DataTable) As DataTable
        ' Validate input parameters
        If String.IsNullOrEmpty(pQuery) Then
            Return Nothing
        End If

        ' Validate connection string
        If String.IsNullOrEmpty(_connection_string) Then
            Return Nothing
        End If

        Try
            Dim list As New DataTable

            ' Ensure connection is open
            If _con.State = ConnectionState.Closed Then Con_Open()

            ' Use Using statements for proper resource disposal
            Using cmd As New SqlCommand(pQuery, _con)
                cmd.CommandType = CommandType.StoredProcedure

                ' Add parameters from DataTable if provided
                If pDtParam IsNot Nothing Then
                    AddParamsToCommand(cmd, pDtParam)
                End If

                Using adp As New SqlDataAdapter(cmd)
                    list.BeginLoadData()
                    adp.Fill(list)
                    list.EndLoadData()
                    Return list
                End Using
            End Using

        Catch ex As Exception
            ' Return Nothing on error
            Return Nothing
        Finally
            Close_Connection()
        End Try
    End Function

    Private Sub AddParamsToCommand(pCmd As SqlCommand, pDtParam As DataTable)
        ' Validate parameter DataTable structure
        If pDtParam Is Nothing OrElse pDtParam.Rows.Count = 0 Then
            Return
        End If

        ' Add each parameter from the DataTable to the SqlCommand
        For Each row As DataRow In pDtParam.Rows
            Dim paramName As String = Convert.ToString(row("ParameterName"))
            Dim sqlDbType As SqlDbType = CType(row("SqlDbType"), SqlDbType)
            Dim size As Integer = Convert.ToInt32(row("Size"))
            Dim value As Object = row("Value")

            ' Create and add the parameter
            Dim param As New SqlParameter(paramName, sqlDbType, size)
            If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                param.Value = value
            Else
                param.Value = DBNull.Value
            End If

            pCmd.Parameters.Add(param)
        Next
    End Sub

#End Region

End Class
