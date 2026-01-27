Imports System
Imports System.Data
Imports System.Collections
Imports System.Data.SqlClient
'Imports System.Data.Odbc
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration

Namespace DAL
    Public MustInherit Class data_access_sql

#Region "variable"
        Shared _con As SqlConnection
        Shared _adp As SqlDataAdapter
        Shared _cmd As SqlCommand
        Shared _cmd_type As CommandType = CommandType.Text
        Shared _cmd_text As String
        Shared _connection_string As String
        Shared _paramtable As DataTable
        Shared _tra As SqlTransaction
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
        Public Shared Property Connection_string() As String
            Get
                Try
                    'Dim str As String = ConfigurationSettings.AppSettings("dbname")
                    '_connection_string = ConfigurationSettings.AppSettings("ConnectionString") '& " " & System.Windows.Forms.Application.StartupPath() & "" & str
                    'If _connection_string = "" Then
                    '    Throw New ApplicationException("Connection String is not initialize")
                    'End If
                    Return _connection_string
                Catch ex As Exception
                    ' FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access_sql::Connection_string:-" & ex.ToString)
                    Write_ErrorLog3(Now.ToString() & "-" & "Data_access_sql::Connection_string:-" & ex.ToString)
                    Throw New ApplicationException("Connection String is not initialize")
                End Try
            End Get
            Set(ByVal value As String)
                _connection_string = value
            End Set
        End Property
        Public Shared Sub open_connectionfin()
            Try
                'Dim Connection_stringFin As String = "Data Source=finideas.com,1433;Initial Catalog=finideas;User ID=finideas;Password=finideas#123"
                Dim Connection_stringFin As String = " Data Source=" & RegServerIP & ";Network Library=DBMSSOCN;Initial Catalog=" & RegServerdbnm & ";User ID=" & RegServerUserid & ";Password=" & RegServerpwd & ";Application Name=" & "VH_" & RegServerIP & "_REG" & ";"
                'Dim Connection_stringFin As String = "Data Source=60.254.95.18,1403;Initial Catalog=Finideas;User ID=sa;Password=123456"
                'If Connection_string <> "" Then
                _con = New SqlConnection(Connection_stringFin) 'New SqlConnection(Connection_string)
                _con.Open()
                'End If
            Catch ex As Exception
                'MsgBox(ex.ToString)
                '  FSTimerLogFile.WriteLine("Data_access_sql::open_connection:-" & ex.ToString)
                Write_ErrorLog3("Data_access_sql::open_connection:-" & ex.ToString)
                Throw New ApplicationException("Connection Error")
            End Try
        End Sub
        Public Shared Sub open_connectioREG()
            Try
                'Dim Connection_stringFin As String = "Data Source=finideas.com,1433;Initial Catalog=finideas;User ID=finideas;Password=finideas#123"
                'Dim Connection_stringFin As String = " Data Source=" & RegServerIP & ";Network Library=DBMSSOCN;Initial Catalog=" & RegServerdbnm & ";User ID=" & RegServerUserid & ";Password=" & RegServerpwd & ";Application Name=" & "VH_" & RegServerIP & "_REG" & ";"
                'Dim Connection_stringFin As String = "Data Source=finideas.com;Initial Catalog=Finideasext;User ID=finideas;Password=db@321"
                Dim Connection_stringFin As String = "Data Source=" + gstr_Internet_Server + ";Initial Catalog=" + gstr_Internet_DB + ";User ID=" + gstr_Internet_Uid + ";Password=" + gstr_Internet_Pwd + ""

                'If Connection_string <> "" Then
                _con = New SqlConnection(Connection_stringFin) 'New SqlConnection(Connection_string)
                _con.Open()
                'End If
            Catch ex As Exception
                'MsgBox(ex.ToString)
                '  FSTimerLogFile.WriteLine("Data_access_sql::open_connection:-" & ex.ToString)
                Write_ErrorLog3("Data_access_sql::open_connection:-" & ex.ToString)
                Throw New ApplicationException("Connection Error")
            End Try
        End Sub
        Public Shared Function FillListfin() As DataTable
            Try
                Dim list As DataTable = New DataTable()
                list.Rows.Clear()
                _adp = New SqlDataAdapter() 'New SqlDataAdapter

                Call open_connectionfin()
                'DAL.DA_SQL.open_connectionFin()

                Call Con_Transaction()
                '  _jro.RefreshCache(_con)
                _cmd = New SqlCommand() 'New SqlCommand()
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
                Call Con_Commit()
                _con.Close()
                Call Close_Connection()
                Return list
            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access_sql::FillList:-" & ex.ToString)
                Write_ErrorLog3("Data_access_sql::FillList:-" & ex.ToString)
                ' Throw New ApplicationException("Trade Data Receving Problem. !!")
                ' Call Con_Rollback()
                ' MsgBox(ex.ToString)
                '  Return Nothing
                ' Finally
                Call Close_Connection()
                _con.Close()
            End Try
            Call Close_Connection()
            _con.Close()
        End Function
        Public Shared Function FillListfinREG() As DataTable
            Try
                Dim list As DataTable = New DataTable()
                list.Rows.Clear()
                _adp = New SqlDataAdapter() 'New SqlDataAdapter

                Call open_connectioREG()
                'DAL.DA_SQL.open_connectionFin()

                Call Con_Transaction()
                '  _jro.RefreshCache(_con)
                _cmd = New SqlCommand() 'New SqlCommand()
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
                Call Con_Commit()
                _con.Close()
                Call Close_Connection()
                Return list
            Catch ex As Exception
                'FSTimerLogFile.WriteLine("Data_access_sql::FillList:-" & ex.ToString)
                Write_ErrorLog3("Data_access_sql::FillList:-" & ex.ToString)
                ' Throw New ApplicationException("Trade Data Receving Problem. !!")
                ' Call Con_Rollback()
                ' MsgBox(ex.ToString)
                '  Return Nothing
                ' Finally
                Call Close_Connection()
                _con.Close()
            End Try
            Call Close_Connection()
            _con.Close()
        End Function
        Public Shared Property Connection_stringODIN() As String
            Get
                Try
                    'Dim str As String = ConfigurationSettings.AppSettings("dbname")
                    '_connection_string = ConfigurationSettings.AppSettings("ConnectionStringODIN")
                    'If _connection_string = "" Then
                    '    Throw New ApplicationException("Connection String is not initialize")
                    'End If
                    Return _connection_string
                Catch ex As Exception
                    '  FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access_sql::Connection_stringODIN:-" & ex.ToString)
                    Write_ErrorLog3(Now.ToString() & "-" & "Data_access_sql::Connection_stringODIN:-" & ex.ToString)
                    Throw New ApplicationException("Connection String is not initialize")
                End Try
            End Get
            Set(ByVal value As String)
                _connection_string = value
            End Set
        End Property

        Public Shared Sub open_connection()
            Try
                If Connection_string <> "" Then
                    _con = New SqlConnection(Connection_string) 'New SqlConnection(Connection_string)
                    _con.Open()
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString)
                Write_ErrorLog3(Now.ToString() & "-" & "Data_access_sql::open_connection:-" & ex.ToString)
                ' FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access_sql::open_connection:-" & ex.ToString)
                Throw New ApplicationException("Connection Error")
            End Try
        End Sub
        Public Shared Sub open_connectionODIN()
            Try
                If Connection_stringODIN <> "" Then
                    _con = New SqlConnection(Connection_stringODIN) 'New SqlConnection(Connection_string)
                    _con.Open()
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString)
                Write_ErrorLog3(Now.ToString() & "-" & "Data_access_sql::Open_connectionODIN:-" & ex.ToString)
                ' FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access_sql::Open_connectionODIN:-" & ex.ToString)
                ' Throw New ApplicationException("Connection Error")
                bool_IsTelNet = False
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
        End Sub
        
        Public Shared Function FillList(Optional ByVal IsODINdb As Boolean = False) As DataTable
            Try
                Dim list As DataTable = New DataTable()
                list.Rows.Clear()
                _adp = New SqlDataAdapter() 'New SqlDataAdapter
                If IsODINdb = False Then
                    Call open_connection()
                Else
                    Call open_connectionODIN()
                End If

                Call Con_Transaction()
                '  _jro.RefreshCache(_con)
                _cmd = New SqlCommand() 'New SqlCommand()
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
                Call Con_Commit()
                Call Close_Connection()
                Return list
            Catch ex As Exception
                ' FSTimerLogFile.WriteLine(Now.ToString() & "-" & "Data_access_sql::FillList:-" & ex.ToString)
                Write_ErrorLog3(Now.ToString() & "-" & "Data_access_sql::FillList:-" & ex.ToString)
                Throw New ApplicationException("Trade Data Receving Problem. !!")
                ' Call Con_Rollback()
                ' MsgBox(ex.ToString)
                '  Return Nothing
                ' Finally
                Call Close_Connection()
            End Try
        End Function
#End Region
    End Class

End Namespace

