Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL
Public Class user_master
#Region "variable"
    Dim _loginid As String
    Dim _pass As String


#End Region
#Region "Property"
    Public Property Loginid() As String
        Get
            Return _loginid
        End Get
        Set(ByVal value As String)
            _loginid = value
        End Set
    End Property
    Public Property Pass() As String
        Get
            Return _pass
        End Get
        Set(ByVal value As String)
            _pass = value
        End Set
    End Property
    
#End Region
#Region "SP"
    Private Const SP_Insert_usermaster As String = "insert_user_master"
    Private Const SP_Select_usermaster As String = "select_user_master"
    Private Const SP_Update_usermaster As String = "update_user_master"
#End Region
#Region "Method"
    Public Sub Insert()

        data_access.ParamClear()
        data_access.AddParam("@login", OleDbType.VarChar, 50, (Loginid))
        data_access.AddParam("@pass", OleDbType.VarChar, 50, (Pass))
        data_access.Cmd_Text = SP_Insert_usermaster
        data_access.ExecuteNonQuery()

    End Sub

    Public Sub Update(ByVal mLoginid As String, ByVal mPass As String)

        data_access.ParamClear()
        data_access.AddParam("@pass", OleDbType.VarChar, 50, mPass)
        data_access.AddParam("@loginid", OleDbType.VarChar, 50, mLoginid)
        data_access.Cmd_Text = SP_Update_usermaster
        data_access.ExecuteNonQuery()

    End Sub

    Public Function Selectdata() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_Select_usermaster
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
       

    End Function
#End Region
End Class
