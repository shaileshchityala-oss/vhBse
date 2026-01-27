Public Class ClsWebCon

    Public Function CheckCon() As Boolean
        Dim Result As Boolean
        Dim fin As New com.finideas.strategybuilder.FinIdeas
        Result = fin.CheckCon()
        Return Result
    End Function

    Public Function Select_User_Master(ByVal sUserid As String, ByVal sM As String, ByVal sH As String, ByVal sP As String, ByVal sProduct As String, ByVal isEnc As Boolean) As DataTable
        Try
            Dim fin As New com.finideas.strategybuilder.FinIdeas
            Dim ds As New DataSet
            Dim StrQry As String
            StrQry = "SELECT     F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21, F22, F23, F24, F25, F26, F27"
            StrQry = StrQry & " FROM dbo.tbl001  Where(1 = 1) "

            If sUserid <> "" Then
                StrQry = StrQry & " And (F2='" & sUserid & "') "
            End If

            If sM <> "" Then
                StrQry = StrQry & " And (F21='" & sM & "') "
            End If

            If sH <> "" Then
                StrQry = StrQry & " And (F22='" & sH & "') "
            End If

            If sP <> "" Then
                StrQry = StrQry & " And (F23='" & sP & "') "
            End If

            If sProduct <> "" Then
                StrQry = StrQry & " And (F6='" & sProduct & "') "
            End If

            StrQry = StrQry & "Order By F6"

            ds = fin.SelectData(StrQry)
            If isEnc Then
                Return ds.Tables(0)
            Else
                Return decTable(ds.Tables(0))
            End If

        Catch ex As Exception
            MsgBox("ClsWebCon::Select_User_Master() :: " & ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function Select_Data(ByVal StrSelect As String) As DataTable
        Try
            Dim fin As New com.finideas.strategybuilder.FinIdeas
            Dim ds As New DataSet

            ds = fin.SelectData(StrSelect)
            Return ds.Tables(0)

        Catch ex As Exception
            MsgBox("ClsWebCon::Select_Data() :: " & ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Sub Update_Data(ByVal StrUpdate As String)
        Try
            Dim fin As New com.finideas.strategybuilder.FinIdeas
            fin.UpdateData(StrUpdate.Replace("Update", ""))

        Catch ex As Exception
            MsgBox("ClsWebCon::Select_Data() :: " & ex.ToString)
        End Try
    End Sub

    
    Public Function ExecuteScalar(ByVal StrQry As String) As String
        Try
            Dim result As String = ""
            Dim fin As New com.finideas.strategybuilder.FinIdeas
            result = fin.ExecuteScalar(StrQry)
            Return result
        Catch ex As Exception
            Return ex.ToString()
        End Try
    End Function

    Public Sub Insert_User_Data(ByVal _Userid As String, ByVal _pwd As String, ByVal _Username As String, ByVal _Firm As String, ByVal _Product As String, ByVal _Allowed As String, ByVal _Limited As String, ByVal _ExDate As String, ByVal _Status As String, ByVal _Address As String, ByVal _Mobile As String, ByVal _Email As String, ByVal _DOB As String, ByVal _FirmAddress As String, ByVal _FirmContactNo As String, ByVal _Reference As String, ByVal _M As String, ByVal _H As String, ByVal _P As String, ByVal _City As String, ByVal _BillNo As String)

        Try
            Dim fin As New com.finideas.strategybuilder.FinIdeas
            fin.UpdateCustomers(_Userid, _pwd, _Username, _Firm, _Product, _Allowed, _Limited, _ExDate, _Status, _Address, _Mobile, _Email, _DOB, _FirmAddress, _FirmContactNo, _Reference, _M, _H, _P, _City, _BillNo)

        Catch ex As Exception
            MsgBox("ClsWebCon::Insert_User_Data() :: " & ex.ToString)
        End Try
    End Sub

    Public Sub Delete_User_Data(ByVal _uid As String, ByVal _sProduct As String, ByVal _sM As String, ByVal _sH As String, ByVal _sP As String)

        Try
            Dim fin As New com.finideas.strategybuilder.FinIdeas
            fin.DeleteCustomers(_uid, _sProduct, _sM, _sH, _sP)

        Catch ex As Exception
            MsgBox("ClsWebCon::Delete_User_Data() :: " & ex.ToString)
        End Try
    End Sub

    Public Sub Insert_Range_Data(ByVal dt As DataTable)
        dt.TableName = "RangeDT"
        Dim ds As New DataSet("RangeDS")
        ds.Tables.Add(dt)
        Try
            Dim fin As New com.finideas.strategybuilder.FinIdeas
            fin.InsertRangeData(ds)

        Catch ex As Exception
            MsgBox("ClsWebCon::Insert_Range_Data() :: " & ex.ToString)
        End Try
    End Sub

    Public Function SelectRangeData(ByRef dt As DataTable, ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
        dt.TableName = "RangeDT"
        Dim ds As New DataSet("RangeDS")
        ds.Tables.Add(dt)
        Try
            Dim fin As New com.finideas.strategybuilder.FinIdeas
            ds = fin.SelectRangeData(FromDate, ToDate)
            Return ds.Tables(0)
        Catch ex As Exception
            MsgBox("ClsWebCon::Insert_Range_Data() :: " & ex.ToString)
        End Try

    End Function

    Public Function SelectDailyTip(ByRef dt As DataTable) As DataTable
        dt.TableName = "RangeDT"
        Dim ds As New DataSet("RangeDS")
        ds.Tables.Add(dt)
        Try
            Dim fin As New com.finideas.strategybuilder.FinIdeas
            ds = fin.SelectDailyTip()
            Return ds.Tables(0)
        Catch ex As Exception
            MsgBox("ClsWebCon::Insert_Range_Data() :: " & ex.ToString)
        End Try

    End Function

    Public Function SelectSetting(ByRef dt As DataTable) As DataTable
        dt.TableName = "RangeDT"
        Dim ds As New DataSet("RangeDS")
        ds.Tables.Add(dt)
        Try
            Dim fin As New com.finideas.strategybuilder.FinIdeas
            ds = fin.SelectSetting()
            Return ds.Tables(0)
        Catch ex As Exception
            MsgBox("ClsWebCon::Insert_Range_Data() :: " & ex.ToString)
        End Try

    End Function



End Class
