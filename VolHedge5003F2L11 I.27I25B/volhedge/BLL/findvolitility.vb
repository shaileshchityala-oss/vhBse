Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL
Public Class findvolitility
#Region "variable"
    Dim _intrumentName As String
    Dim _company As String
    Dim _mdate As Date
    Dim _strikerate As Double
    Dim _script As String
    Dim _cp As String
    Dim _token As Double
    Dim _portfolio As String
    Dim _uid As Long
#End Region
#Region "Property"
    Public Property InstrumentName() As String
        Get
            Return _intrumentName
        End Get
        Set(ByVal value As String)
            _intrumentName = value
        End Set
    End Property
    Public Property Company() As String
        Get
            Return _company
        End Get
        Set(ByVal value As String)
            _company = value
        End Set
    End Property
    Public Property Mdate() As Date
        Get
            Return _mdate
        End Get
        Set(ByVal value As Date)
            _mdate = value
        End Set
    End Property
    Public Property StrikeRate() As Double
        Get
            Return _strikerate
        End Get
        Set(ByVal value As Double)
            _strikerate = value
        End Set
    End Property
  
    Public Property CP() As String
        Get
            Return _cp
        End Get
        Set(ByVal value As String)
            _cp = value
        End Set
    End Property
    Public Property Script() As String
        Get
            Return _script
        End Get
        Set(ByVal value As String)
            _script = value
        End Set
    End Property
    Public Property Token() As Double
        Get
            Return _token
        End Get
        Set(ByVal value As Double)
            _token = value
        End Set
    End Property
    Public Property Portfolio() As String
        Get
            Return _portfolio
        End Get
        Set(ByVal value As String)
            _portfolio = value
        End Set
    End Property
    Public Property Uid() As Long
        Get
            Return _uid
        End Get
        Set(ByVal value As Long)
            _uid = value
        End Set
    End Property
#End Region
#Region "SP"
    Private Const SP_findvol_Insert As String = "insert_findvol"
    Private Const SP_findvol_select As String = "select_findvol"
    Private Const SP_delete_portfolio_token As String = "delete_portfolio_token"
    Private Const SP_delete_portfolio_name As String = "delete_marketwatch_byname"
    Private Const Sp_select_portfolio As String = "select_portfolio"
    Private Const SP_update_findvol As String = "update_findvol"
#End Region
#Region "Method"
    Public Sub Insert()

        data_access.ParamClear()
        data_access.AddParam("@instrument", OleDbType.VarChar, 50, InstrumentName)
        data_access.AddParam("@company", OleDbType.VarChar, 50, Company)
        data_access.AddParam("@cpf", OleDbType.VarChar, 18, CP)
        data_access.AddParam("@mdate", OleDbType.Date, 18, Mdate)
        data_access.AddParam("@strike", OleDbType.Double, 18, StrikeRate)
        data_access.AddParam("@script", OleDbType.VarChar, 100, Script)
        data_access.AddParam("@token", OleDbType.Double, 18, token)
        data_access.Cmd_Text = SP_findvol_Insert
        data_access.ExecuteNonQuery()

    End Sub
    Public Sub update()

        data_access.ParamClear()
        data_access.AddParam("@instrument", OleDbType.VarChar, 50, InstrumentName)
        data_access.AddParam("@company", OleDbType.VarChar, 50, Company)
        data_access.AddParam("@cpf", OleDbType.VarChar, 18, CP)
        data_access.AddParam("@mdate", OleDbType.Date, 18, Mdate)
        data_access.AddParam("@strike", OleDbType.Double, 18, StrikeRate)
        data_access.AddParam("@script", OleDbType.VarChar, 100, Script)
        data_access.AddParam("@token", OleDbType.Double, 18, Token)
        data_access.AddParam("@portfolio", OleDbType.VarChar, 150, Portfolio)
        data_access.AddParam("@uid", OleDbType.Integer, 18, Uid)
        data_access.Cmd_Text = SP_update_findvol
        data_access.ExecuteNonQuery()

    End Sub
    Public Sub Insert(ByVal dtable As DataTable, ByVal grdvol As DataGridView, ByVal portfolio As String, ByVal count As Integer, Optional ByVal chk As Boolean = False)
      If chk = False Then
            data_access.ParamClear()
            'Dim i As Integer
            For Each grow As DataGridViewRow In grdvol.Rows
                ' For Each drow As DataRow In dtable.Rows
                If CBool(grow.Cells(7).Value) = True Then
                    For Each drow As DataRow In dtable.Select("token=" & CLng(grow.Cells(1).Value))
                        data_access.AddParam("@instrument", OleDbType.VarChar, 50, drow("InstrumentName"))
                        data_access.AddParam("@company", OleDbType.VarChar, 50, drow("Symbol"))
                        data_access.AddParam("@cpf", OleDbType.VarChar, 18, drow("option_type"))
                        data_access.AddParam("@mdate", OleDbType.Date, 18, CDate(drow("expdate1")).Date)
                        data_access.AddParam("@strike", OleDbType.Double, 18, Val(drow("strike_price")))
                        data_access.AddParam("@script", OleDbType.VarChar, 150, drow("Script"))
                        data_access.AddParam("@token", OleDbType.Double, 18, Val(drow("Token")))
                        data_access.AddParam("@portfolio", OleDbType.VarChar, 150, portfolio)
                        data_access.AddParam("@ordseq", OleDbType.Integer, 18, count)
                        count += 1
                    Next

                End If
                'Next
            Next



            data_access.Cmd_Text = SP_findvol_Insert

            data_access.ExecuteMultiple(9)
        Else
            data_access.ParamClear()
            Dim i As Integer
            i = 1
            For Each drow As DataRow In dtable.Select("cpf<>'F'")
                data_access.AddParam("@instrument", OleDbType.VarChar, 50, drow("instrument"))
                data_access.AddParam("@company", OleDbType.VarChar, 50, drow("company"))
                data_access.AddParam("@cpf", OleDbType.VarChar, 18, drow("cpf"))
                data_access.AddParam("@mdate", OleDbType.Date, 18, CDate(drow("mdate")).Date)
                data_access.AddParam("@strike", OleDbType.Double, 18, Val(drow("strike")))
                data_access.AddParam("@script", OleDbType.VarChar, 150, drow("Script"))
                data_access.AddParam("@token", OleDbType.Double, 18, Val(drow("Token")))
                data_access.AddParam("@portfolio", OleDbType.VarChar, 150, CStr(drow("portfolio")))
                data_access.AddParam("@ordseq", OleDbType.Integer, 18, i)
                i += 1
            Next
            data_access.Cmd_Text = SP_findvol_Insert
            data_access.ExecuteMultiple(9)
        End If

    End Sub
    Public Sub Insert(ByVal grdvol As DataGridView, ByVal portfolio As String, ByVal count As Integer, Optional ByVal chk As Boolean = False)
        data_access.ParamClear()
        'Dim i As Integer
        For Each grow As DataGridViewRow In grdvol.Rows
            ' For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@instrument", OleDbType.VarChar, 50, grow.Cells("Instrument").Value)
            data_access.AddParam("@company", OleDbType.VarChar, 50, grow.Cells("company").Value)
            data_access.AddParam("@cpf", OleDbType.VarChar, 18, grow.Cells("cpf").Value)
            data_access.AddParam("@mdate", OleDbType.Date, 18, CDate(grow.Cells("expdate1").Value).Date)
            data_access.AddParam("@strike", OleDbType.Double, 18, Val(grow.Cells("strike").Value))
            data_access.AddParam("@script", OleDbType.VarChar, 150, grow.Cells("Script").Value)
            data_access.AddParam("@token", OleDbType.Double, 18, Val(grow.Cells("Token").Value))
            data_access.AddParam("@portfolio", OleDbType.VarChar, 150, portfolio)
            data_access.AddParam("@ordseq", OleDbType.Integer, 18, count)
            count += 1
        Next
        data_access.Cmd_Text = SP_findvol_Insert
        data_access.ExecuteMultiple(9)
    End Sub
    Public Function Selectvol(ByVal portfolio As String) As DataTable
        Try
            data_access.ParamClear()
            data_access.AddParam("@portfolio", OleDbType.VarChar, 150, portfolio)
            data_access.Cmd_Text = SP_findvol_select
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function Selectportfolio() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Sp_select_portfolio
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Sub deletevol(ByVal portfolio As String, ByVal dtable As DataTable, ByVal grdvol As DataGridView)
        Try
            data_access.ParamClear()
            Dim i As Integer
            For Each drow As DataRow In dtable.Rows
                i = dtable.Rows.IndexOf(drow)
                If CBool(grdvol.Rows(i).Cells(0).Value) = True Then

                    data_access.AddParam("@token", OleDbType.Double, 18, val(drow("Token")))
                    data_access.AddParam("@portfolio", OleDbType.VarChar, 150, portfolio)
                    data_access.AddParam("@uid", OleDbType.Integer, 18, CInt(drow("uid")))

                End If
            Next
            data_access.Cmd_Text = SP_delete_portfolio_token
            data_access.ExecuteMultiple(3)
        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
    End Sub
    Public Sub deletevol()
        Try
            data_access.ParamClear()
            data_access.AddParam("@token", OleDbType.Double, 18, Token)
            data_access.AddParam("@portfolio", OleDbType.VarChar, 150, portfolio)
            data_access.AddParam("@uid", OleDbType.Integer, 18, Uid)
            data_access.Cmd_Text = SP_delete_portfolio_token
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
    End Sub
    Public Sub deletevol(ByVal portfolio As String)
        Try
            data_access.ParamClear()
            'For Each drow As DataRow In dtable.Rows
            '    data_access.AddParam("@token", OleDbType.Double, 18, val(drow("Token")))
            '    data_access.AddParam("@portfolio", OleDbType.VarChar, 150, CStr(drow("portfolio")))
            '    data_access.AddParam("@uid", OleDbType.Integer, 18, CInt(drow("uid")))
            'Next
            data_access.AddParam("@portfolio", OleDbType.VarChar, 150, portfolio)
            data_access.Cmd_Text = SP_delete_portfolio_name
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
    End Sub
#End Region
End Class
