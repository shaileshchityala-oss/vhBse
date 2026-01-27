Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL
Public Class alertentry
#Region "variable"

    Dim _comp_script As String
    Dim _opt As String
    Dim _field As String
    Dim _value1 As Double
    Dim _value2 As Double
    Dim _units As Double
    Dim _status As Integer
    Dim _iscurrency As Integer
    Dim _entrydate As Date
#End Region
#Region "Property"
    Public Property Comp_Script() As String
        Get
            Return _comp_script
        End Get
        Set(ByVal value As String)
            _comp_script = value
        End Set
    End Property
    Public Property Opt() As String
        Get
            Return _opt
        End Get
        Set(ByVal value As String)
            _opt = value
        End Set
    End Property
    Public Property Field() As String
        Get
            Return _field
        End Get
        Set(ByVal value As String)
            _field = value
        End Set
    End Property
    Public Property Value1() As Double
        Get
            Return _value1
        End Get
        Set(ByVal value As Double)
            _value1 = value
        End Set
    End Property
    Public Property Value2() As Double
        Get
            Return _value2
        End Get
        Set(ByVal value As Double)
            _value2 = value
        End Set
    End Property
    Public Property Units() As Double
        Get
            Return _units
        End Get
        Set(ByVal value As Double)
            _units = value
        End Set
    End Property
    Public Property Status() As Integer
        Get
            Return _status
        End Get
        Set(ByVal value As Integer)
            _status = value
        End Set
    End Property
    Public Property Entrydate() As Date
        Get
            Return _entrydate
        End Get
        Set(ByVal value As Date)
            _entrydate = value
        End Set
    End Property
    Public Property IsCurrency() As Integer
        Get
            Return _iscurrency
        End Get
        Set(ByVal value As Integer)
            _iscurrency = value
        End Set
    End Property


#End Region
#Region "SP"
    Private Const SP_alert_Insert As String = "insert_alert"
    Private Const SP_alert_select As String = "select_alert"
    Private Const SP_alert_Delete As String = "delete_alert"
    Private Const sp_update_alert As String = "update_alert"
    Private Const sp_update_alert_entrydate As String = "update_alert_entrydate"
#End Region
#Region "Method"
    Public Sub Insert()
        data_access.ParamClear()
        data_access.AddParam("@comp_script", OleDbType.VarChar, 50, Comp_Script)
        data_access.AddParam("@opt", OleDbType.VarChar, 50, Opt)
        data_access.AddParam("@field", OleDbType.VarChar, 50, Field)
        data_access.AddParam("@status", OleDbType.Integer, 18, Status)
        data_access.AddParam("@value1", OleDbType.Double, 18, Value1)
        data_access.AddParam("@value2", OleDbType.Double, 18, Value2)
        data_access.AddParam("@entrydate", OleDbType.Date, 18, Entrydate)
        data_access.AddParam("@units", OleDbType.Double, 18, Units)
        data_access.Cmd_Text = SP_alert_Insert
        data_access.ExecuteNonQuery()
    End Sub
    Public Sub update(ByVal uid As Integer)

        data_access.ParamClear()
        data_access.AddParam("@comp_script", OleDbType.VarChar, 50, Comp_Script)
        data_access.AddParam("@opt", OleDbType.VarChar, 50, Opt)
        data_access.AddParam("@field", OleDbType.VarChar, 50, Field)
        data_access.AddParam("@status", OleDbType.Integer, 18, Status)
        data_access.AddParam("@value1", OleDbType.Double, 18, Value1)
        data_access.AddParam("@value2", OleDbType.Double, 18, Value2)
        data_access.AddParam("@entrydate", OleDbType.Date, 18, Entrydate)
        data_access.AddParam("@units", OleDbType.Double, 18, Units)
        data_access.AddParam("@uid", OleDbType.Integer, 18, uid)
        data_access.Cmd_Text = sp_update_alert
        data_access.ExecuteNonQuery()

    End Sub

    Public Sub update(ByVal uid As Integer, ByVal entrydate As Date)

        data_access.ParamClear()
        data_access.AddParam("@entrydate", OleDbType.Date, 18, entrydate)
        data_access.AddParam("@uid", OleDbType.Integer, 18, uid)
        data_access.Cmd_Text = sp_update_alert_entrydate
        data_access.ExecuteNonQuery()

    End Sub
    Public Function select_data() As DataTable
        data_access.ParamClear()
        data_access.AddParam("@status", OleDbType.Integer, 18, Status)
        data_access.Cmd_Text = SP_alert_select
        Return data_access.FillList
    End Function
    Public Sub delete(ByVal dtable As DataTable, ByVal grdalert As DataGridView)
        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            Dim index As Integer = dtable.Rows.IndexOf(drow)
            If CBool(grdalert.Rows(index).Cells(0).Value) = True Then
                data_access.AddParam("@comp_script", OleDbType.VarChar, 50, drow("Comp_Script"))
                data_access.AddParam("@field", OleDbType.VarChar, 50, drow("Field"))
                data_access.AddParam("@status", OleDbType.Integer, 18, CLng(drow("cs")))
                data_access.AddParam("@uid", OleDbType.Integer, 18, CLng(drow("uid")))
            End If
        Next
        data_access.Cmd_Text = SP_alert_Delete
        data_access.ExecuteMultiple(4)

    End Sub
#End Region
End Class
