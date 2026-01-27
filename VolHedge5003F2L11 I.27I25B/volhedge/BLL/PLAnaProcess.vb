Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL

Public Class PLAnaProcess
#Region "Variable"


#End Region

#Region "SP"
    Private Const Sp_Select_Sum_PnL As String = "Select_Sum_PnL"
    Private Const Sp_Select_PnL As String = "Select_PnL"
#End Region

#Region "Method"
    Public Function Select_Sum_PnL() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Sp_Select_Sum_PnL
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function Select_PnL() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Sp_Select_PnL
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

#End Region
End Class
